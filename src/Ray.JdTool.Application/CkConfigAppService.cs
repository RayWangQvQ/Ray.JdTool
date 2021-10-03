using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Ray.JdTool.CkConfig;
using Ray.JdTool.JdCkHistories;
using Ray.JdTool.Permissions;
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
        private readonly IWebHostEnvironment _env;
        private string _configStr;

        public CkConfigAppService(IWebHostEnvironment env)
        {
            _env = env;
            _configStr = DoGetConfigFileContent();
        }

        [Authorize(JdToolPermissions.GetAllCookieConfig)]
        public async Task<string> GetConfigFileContent()
        {
            return DoGetConfigFileContent();
        }

        [Authorize(JdToolPermissions.CommitCookie)]
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

                //去除注释
                pre = pre.Replace("#", "");

                //更新Cookie
                result = new CommitResult
                {
                    Success = true,
                    CommitResultType = CommitResultType.Update,
                    ResultStr = $"{pre}=\"{jdCk.FullStr}\""
                };

                _configStr = _configStr.Replace(match.Value, result.ResultStr);

                //去除阻止配置
                var num = pre.Replace(" ", "")
                    .Replace("Cookie", "");
                RemoveFromTempBlockStr(num);
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
                    ResultStr = $"Cookie{newNum}=\"{jdCk.FullStr}\""
                };
                _configStr = _configStr.Replace(lastLine, lastLine + Environment.NewLine + result.ResultStr);
            }

            SaveToConfigFile();
            return result;
        }

        private string DoGetConfigFileContent()
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
            var re = File.ReadAllText(path);

            //return Encoding.Default.GetString(buffer);
            return re;
        }

        private void SaveToConfigFile()
        {
            string path = Path.Combine(_env.WebRootPath, "cookie.sh");
            File.WriteAllText(path, _configStr);
        }

        private void RemoveFromTempBlockStr(string num)
        {
            var pattern = $"TempBlockCookie=.*#屏蔽";
            var match = new Regex(pattern).Match(_configStr);

            if (match.Success)
            {
                var line = match.Value;
                if (line.Contains("\"\"")) return;

                var blockNums = line.Substring(line.IndexOf("\"") + 1, line.LastIndexOf("\"") - line.IndexOf("\"") - 1);
                var list = blockNums.Split(' ').ToList();

                if (list.Contains(num))
                {
                    list.Remove(num);
                }

                var newLine = $"TempBlockCookie=\"{string.Join(" ", list)}\"#屏蔽";

                _configStr = _configStr.Replace(line, newLine);
            }
        }
    }
}
