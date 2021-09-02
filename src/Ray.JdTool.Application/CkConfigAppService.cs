using Microsoft.Extensions.FileProviders;
using Ray.JdTool.CkConfig;
using Ray.JdTool.JdCkHistories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ray.JdTool
{
    public class CkConfigAppService : JdToolAppService, ICkConfigAppService
    {
        private readonly IFileProvider _fileProvider;
        private string _configStr;
        private IFileInfo _fileInfo;

        public CkConfigAppService(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
            _fileInfo = _fileProvider.GetFileInfo("/cookie.sh");
            _configStr = GetConfigFileContent();
        }

        public string GetConfigFileContent()
        {
            byte[] buffer;
            using (var stream = _fileInfo.CreateReadStream())
            {
                buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
            }

            return Encoding.Default.GetString(buffer);
        }

        public string CreateUpdateCookie(CreateUpdateJdCkHistoryDto ck)
        {
            var result = "";

            JdCkHistory jdCk = ObjectMapper.Map<CreateUpdateJdCkHistoryDto, JdCkHistory>(ck);

            var pattern = $".*Cookie[0-9]+\\s*=\\s*\".*{jdCk.PtPin}.*";
            var match = new Regex(pattern).Match(_configStr);

            //已存在，则更新替换
            if (match.Success)
            {
                var line = match.Value;
                var pre = line.Substring(0, line.IndexOf('='));
                result = $"{pre}=\"{jdCk.SimpleStr}\"";

                _configStr = _configStr.Replace(match.Value, result);
            }
            else//不存在，则新增
            {
                //找到Cookie的最后一行
                var lastCookieMatches = new Regex(".*Cookie[0-9]+\\s*=\\s*.*").Matches(_configStr);
                //取最后一个编号
                var lastLine = lastCookieMatches.LastOrDefault().Value;
                var numStr = lastLine.Substring(lastLine.IndexOf("Cookie") + 6, lastLine.IndexOf('=') - lastLine.IndexOf("Cookie") - 6)
                    .Trim();

                var newNum = int.Parse(numStr) + 1;

                result = $"Cookie{newNum}=\"{jdCk.SimpleStr}\"";
                _configStr = _configStr.Replace(lastLine, lastLine + Environment.NewLine + result);
            }

            SaveToConfigFile();
            return result;
        }

        private void SaveToConfigFile()
        {
            File.WriteAllText(_fileInfo.PhysicalPath, _configStr);
        }
    }
}
