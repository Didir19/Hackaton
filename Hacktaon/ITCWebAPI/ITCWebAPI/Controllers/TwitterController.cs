using ITCWebAPI.Models;
using ITCWebAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace ITCWebAPI.Controllers
{
    public class TwitterController : ApiController
    {
        private const string consumer_key = "hkasi8MKO64xM4XD4vDaKilDl";
        private const string consumer_secret = "i0tpEaN2BxonZOsjFQ6RwR97QWMVpTpKeLu7qbklLPhrumIrTf";
        private const string getUserId_url = "https://api.twitter.com/1.1/users/show.json?screen_name=";
        private const string stream_url = "https://api.twitter.com/1.1/statuses/user_timeline.json?user_id=";

        private string GetUserId(string username)
        {
            var request = WebRequest.Create(getUserId_url + username);
            request.Headers.Add("Authorization", "Bearer " + WebApiConfig.bearer_token);
            var response = request.GetResponse();
            TwitterUser user = RequestUtils.AnalyzeResponse<TwitterUser>(response);
            return user.id;
        }

        [Route("api/twitter/{name}")]
        public HttpResponseMessage Get(string name)
        {
            try
            {
                var userId = GetUserId(name);
                var request = WebRequest.Create(stream_url + userId);
                request.Headers.Add("Authorization", "Bearer " + WebApiConfig.bearer_token);
                var response = request.GetResponse();
                List<Timeline> timeline = RequestUtils.AnalyzeResponse<List<Timeline>>(response);

                HttpResponseMessage result = new HttpResponseMessage()
                {
                    Content = new StringContent(timeline[0].text, Encoding.UTF8, "text/html")
                };

                return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
