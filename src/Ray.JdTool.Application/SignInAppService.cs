using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http.Json;

namespace Ray.JdTool
{
    public class SignInAppService : JdToolAppService, ISignInAppService
    {
        //private const string jd_ua = "jdapp;android;10.0.8;10;8363837323630343938383239353-53D2438313336633030343333326;network/wifi;model/V1824BA;addressid/1851732777;aid/f9e0651eb65d39fe;oaid/d613ff725e4e2b2e59180b81a677f7434a333885bf0104a37b04910eed7c9c98;osVer/29;appBuild/86560;partner/vivo;eufv/1;jdSupportDarkMode/0;Mozilla/5.0 (Linux; Android 10; V1824BA Build/QP1A.190711.020; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/77.0.3865.120 MQQBrowser/6.2 TBS/045230 Mobile Safari/537.36";
        private string jd_ua = "Mozilla/5.0 (iPhone; U; CPU iPhone OS 4_3_2 like Mac OS X; en-us) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8H7 Safari/6533.18.5 UCBrowser/13.4.2.1122 TM/{0}";

        public void ScanCodeLogin()
        {
            var s_token = GetSToken();

            var tokenTuple = GetToken(s_token);
        }

        private string GetSToken()
        {
            HttpClient _httpClient = new HttpClient();

            var t = DateTimeOffset.Now.ToUnixTimeSeconds();

            var referer = $"https://plogin.m.jd.com/cgi-bin/mm/new_login_entrance?lang=chs&appid=300&returnurl=https://wq.jd.com/passport/LoginRedirect?state={t}&returnurl=https://home.m.jd.com/myJd/newhome.action?sceneval=2&ufc=&/myJd/home.action&source=wq_passport";
            var ua = string.Format(jd_ua, t);
            bool suc = _httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd(ua);
            if (!suc)
                _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", ua);//https://stackoverflow.com/questions/19039450/adding-authorization-to-the-headers/24575721#24575721
            _httpClient.DefaultRequestHeaders.Referrer = new Uri(referer);

            t = DateTimeOffset.Now.ToUnixTimeSeconds();
            var url = $"https://plogin.m.jd.com/cgi-bin/mm/new_login_entrance?lang=chs&appid=300&returnurl=https://wq.jd.com/passport/LoginRedirect?state={t}&returnurl=https://home.m.jd.com/myJd/newhome.action?sceneval=2&ufc=&/myJd/home.action&source=wq_passport";
            var response = _httpClient.GetAsync(url).Result;

            var json = response.Content.ReadAsStringAsync().Result;
            var dic = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
            return dic.TryGetValue("s_token", out object re) ? re.ToString() : "";
        }

        private Tuple<string, string> GetToken(string s_token)
        {
            HttpClient _httpClient = new HttpClient();

            var t = DateTimeOffset.Now.ToUnixTimeSeconds();

            var ua = string.Format(jd_ua, t);
            bool suc = _httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd(ua);
            if (!suc)
                _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", ua);//https://stackoverflow.com/questions/19039450/adding-authorization-to-the-headers/24575721#24575721

            _httpClient.DefaultRequestHeaders.Referrer = new Uri($"https://plogin.m.jd.com/login/login?appid=300&returnurl=https://wqlogin2.jd.com/passport/LoginRedirect?state={t}&returnurl=//home.m.jd.com/myJd/newhome.action?sceneval=2&ufc=&/myJd/home.action&source=wq_passport");

            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded; Charset=UTF-8");

            var url = $"https://plogin.m.jd.com/cgi-bin/m/tmauthreflogurl";
            var contentDic = new Dictionary<string, string>
            {
                { "s_token",s_token},
                {"v",t.ToString() },
                { "remember","true"},
                { "lang","chs"},
                { "appid","300"},
                {"returnurl",$"https://wqlogin2.jd.com/passport/LoginRedirect?state={t}returnurl=//home.m.jd.com/myJd/newhome.action?sceneval=2&ufc=&/myJd/home.action&source=wq_passport" }
            };

            HttpContent content = new FormUrlEncodedContent(contentDic);
            var response = _httpClient.PostAsync(url, content).Result;

            var json = response.Content.ReadAsStringAsync().Result;
            var dic = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
            var token = dic.TryGetValue("token", out object re) ? re.ToString() : "";

            return Tuple.Create(token, "");
        }
    }
}
