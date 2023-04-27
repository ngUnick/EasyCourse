using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;
using Google.Apis.Calendar.v3;
using System.Web;
using static System.Net.WebRequestMethods;

namespace Last
{
    public class HomeController : Controller 
    {

        public ActionResult OauthRedirect()
        {
            var credentialsFile = "C:\\Users\\Vasilis\\source\\repos\\Last\\Files\\Credentials.json";

            JObject credentials = JObject.Parse(System.IO.File.ReadAllText(credentialsFile));
            var client_id = credentials["client_id"];
            
            var redirectUrl = "https://accounts.google.com/o/oauth2/v2/auth?" +
                "scope=https://www.googleapis.com/auth/calendar+https://www.googleapis.com/auth/calendar.events&" +
                "access_type=online&" +
                "include_granted_scopes=true&" +
                "response_type=code&" +
                "state=hellothere& " + //maybe hello there to state
                "redirect_uri=https://localhost:7297/oauth/callback& " +   //Το redirect θα μπει της σελιδας τα login 
                "client_id="+client_id;

 

            return Redirect(redirectUrl);


        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
