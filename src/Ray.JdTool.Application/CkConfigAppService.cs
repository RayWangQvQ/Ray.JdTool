using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _env;
        private string _configStr;
        private IFileInfo _fileInfo;

        //public CkConfigAppService(IFileProvider fileProvider)
        public CkConfigAppService(IWebHostEnvironment env)
        {
            this._env = env;
            //_fileProvider = fileProvider;
            //_fileInfo = _fileProvider.GetFileInfo("/cookie.sh");
            _configStr = GetConfigFileContent().Result;
        }

        public async Task<string> GetConfigFileContent()
        {
            /*
            byte[] buffer;
            using (var stream = _fileInfo.CreateReadStream())
            {
                buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
            }
            */
            //string path = _env.ContentRootPath;
            string path = Path.Combine(_env.WebRootPath, "cookie.sh");
            var re = await File.ReadAllTextAsync(path);

            //return Encoding.Default.GetString(buffer);
            return re;
        }

        public async Task<CommitResult> CreateUpdateCookie(CreateUpdateJdCkHistoryDto ck)
        {
            var result = new CommitResult();

            JdCkHistory jdCk = ObjectMapper.Map<CreateUpdateJdCkHistoryDto, JdCkHistory>(ck);

            var pattern = $".*Cookie[0-9]+\\s*=\\s*\".*{jdCk.PtPin}.*";
            var match = new Regex(pattern).Match(_configStr);

            //已存在，则更新替换
            if (match.Success)
            {
                var line = match.Value;
                var pre = line.Substring(0, line.IndexOf('='));

                result = new CommitResult
                {
                    Success = true,
                    CommitResultType = CommitResultType.Update,
                    ResultStr = $"{pre}=\"{jdCk.SimpleStr}\""
                };

                _configStr = _configStr.Replace(match.Value, result.ResultStr);
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

                result = new CommitResult
                {
                    Success = true,
                    CommitResultType = CommitResultType.Add,
                    ResultStr = $"Cookie{newNum}=\"{jdCk.SimpleStr}\""
                };
                _configStr = _configStr.Replace(lastLine, lastLine + Environment.NewLine + result.ResultStr);
            }

            SaveToConfigFile();
            return result;
        }

        private void SaveToConfigFile()
        {
            string path = Path.Combine(_env.WebRootPath, "cookie.sh");
            File.WriteAllText(path, _configStr);
        }
    }
}
