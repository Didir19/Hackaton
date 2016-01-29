using ITCWebAPI.Models;
using ITCWebAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Http;

namespace ITCWebAPI
{
    public static class WebApiConfig
    {
        private const string consumer_key = "hkasi8MKO64xM4XD4vDaKilDl";
        private const string consumer_secret = "i0tpEaN2BxonZOsjFQ6RwR97QWMVpTpKeLu7qbklLPhrumIrTf";
        private const string auth_url = "https://api.twitter.com/oauth2/token";
        public static string bearer_token = "";

        public static void Register(HttpConfiguration config)
        {

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static string GetEncodedKey()
        {
            string signingKey = Uri.EscapeDataString(consumer_key) + ":" + Uri.EscapeDataString(consumer_secret);
            var tempKey = Encoding.UTF8.GetBytes(signingKey);
            return Convert.ToBase64String(tempKey);
        }

        private static WebRequest BuildWebRequest(string key)
        {
            var request = WebRequest.Create(auth_url);
            var dataLength = 0;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            request.Headers.Add("Authorization", "Basic " + key);
            using (var requestStream = request.GetRequestStream())
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                var stringData = "grant_type=client_credentials";
                var data = encoding.GetBytes(stringData);
                dataLength = data.Length;
                requestStream.Write(data, 0, dataLength);
            }
            //request.ContentLength = dataLength;
            return request;
        }


        public static int TwitterAuthentication()
        {
            try
            {
                var key = GetEncodedKey();
                var request = BuildWebRequest(key);
                var response = request.GetResponse();
                AuthResponse authResponse = RequestUtils.AnalyzeResponse<AuthResponse>(response);
                if (authResponse.token_type == "bearer")
                {
                    bearer_token = authResponse.access_token;
                }
                Console.WriteLine("Authenticated");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
        }
    }
}
