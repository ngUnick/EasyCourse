using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;
using Google.Apis.Calendar.v3;
using System.Web;
using System.Security.Cryptography.X509Certificates;

namespace Last
{
    public class OAuthController1 : Controller
    {
        public void Callback(string code, string error, string state)
        {
            if (string.IsNullOrEmpty(error))
            {
                this.GetTokens(code);
            }
        }

            public ActionResult GetTokens(string code)
            {
                var tokenFile = "C:\\Users\\Vasilis\\source\\repos\\Last\\Files\\tokens.json";
                var credentialsFile = "C:\\Users\\Vasilis\\source\\repos\\Last\\Files\\Credentials.json";
                var credentials= JObject.Parse(System.IO.File.ReadAllText(credentialsFile));
              
                RestClient restClient = new RestClient();
              RestRequest request = new RestRequest();

                request.AddQueryParameter("client_id", credentials["client_id"].ToString());
                request.AddQueryParameter("client_secret", credentials["client_secret"].ToString());
                request.AddQueryParameter("code", code);
                request.AddQueryParameter("grant_type", "authorization_code");
                request.AddQueryParameter("redirect_uri", "https://localhost:7297/oauth/callback"); // tha mpei edw to redirect url

                restClient.BaseUrl = new System.Uri("https://oauth2.googleapis.com/token");
                var response = restClient.Post(request);

                if (response.StatusCode== System.Net.HttpStatusCode.OK)
            {
                System.IO.File.WriteAllText(tokenFile, response.Content);
                return RedirectToAction("Index","Home");
            }
            return View("Error");
            }
        
            public ActionResult RefreshToken()
        {
            var tokenFile = "C:\\Users\\Vasilis\\source\\repos\\Last\\Files\\tokens.json";
            var credentialsFile = "C:\\Users\\Vasilis\\source\\repos\\Last\\Files\\Credentials.json";
            var credentials = JObject.Parse(System.IO.File.ReadAllText(credentialsFile));
            var tokens=JObject.Parse(System.IO.File.ReadAllText(tokenFile));
            RestClient restClient = new RestClient();
            RestRequest request = new RestRequest();
            request.AddQueryParameter("client_id", credentials["client_id"].ToString());
            request.AddQueryParameter("client_secret", credentials["client_secret"].ToString());
            request.AddQueryParameter("grant_type", "refresh_token");
            request.AddQueryParameter("refresh_token", tokens["refresh_token"].ToString());

            restClient.BaseUrl = new System.Uri("\"https://oauth2.googleapis.com/token");
            var response = restClient.Post(request);

                if (response.StatusCode== System.Net.HttpStatusCode.OK)
            {
                JObject newTokens =JObject.Parse(response.Content);
                newTokens["refresh_token"] = tokens["refresh_token"].ToString();
                System.IO.File.WriteAllText(tokenFile, newTokens.ToString());   
                return RedirectToAction("Index","Home",new {status="succes"});

            }
                return View("Error");

        }
        public ActionResult RevokeToken()
        {
            var tokenFile = "C:\\Users\\Vasilis\\source\\repos\\Last\\Files\\tokens.json";
            var tokens = JObject.Parse(System.IO.File.ReadAllText(tokenFile));
            
            RestClient restClient = new RestClient();
            RestRequest request = new RestRequest();
          
            request.AddQueryParameter("refresh_token", tokens["refresh_token"].ToString());
            restClient.BaseUrl = new System.Uri("\"https://oauth2.googleapis.com/revoke");
            var response = restClient.Post(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("Index","Home",new {status= "success"});
            }
                return View("Error");

        }






    }
        
   
    }

