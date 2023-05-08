using System.Web;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using System;
using Google.Apis.Calendar.v3;
using Google.Apis;
using Newtonsoft.Json.Serialization;
using Google.Apis.Calendar.v3.Data;


namespace Last
{
   
    public class CalendarEventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateEvent(Event calendarEvent)
        {
            var tokenFile = "C:\\Users\\Vasilis\\source\\repos\\Last\\Files\\tokens.json";
            var tokens = JObject.Parse(System.IO.File.ReadAllText(tokenFile));

            RestClient restClient = new RestClient();
            RestRequest request = new RestRequest();

            calendarEvent.Start.DateTime = DateTime.Parse(calendarEvent.Start.DateTime).ToString("yyyy-MM-dd'T'HH:mm:ss.fffK");
            calendarEvent.End.DateTime = DateTime.Parse(calendarEvent.End.DateTime).ToString("yyyy-MM-dd'T'HH:mm:ss.fffK");

            var model = JsonConvert.SerializeObject(calendarEvent, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            request.AddQueryParameter("key", "AIzaSyAx8awdq97MVxgBKEjI8-SNIpLDwV9T50g");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer" + tokens["access_token"]);
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", model, ParameterType.RequestBody);
            restClient.BaseUrl = new System.Uri("https://www.googleapis.com/calendar/v3/calendars/primary/events");

            var response = restClient.Post(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("Index", "Home", new { status = "success" }); //Sto Index kai sto home tha allaxoun analoga tin selida sto Front-End to idio isxeiei kai se alla paromoiia
            }
            return View("Error");
        }
        public ActionResult Event(string identifier)
        {
            var tokenFile = "C:\\Users\\Vasilis\\source\\repos\\Last\\Files\\tokens.json";
            var tokens = JObject.Parse(System.IO.File.ReadAllText(tokenFile));

            RestClient restClient = new RestClient();
            RestRequest request = new RestRequest();
            request.AddQueryParameter("key", "AIzaSyAx8awdq97MVxgBKEjI8-SNIpLDwV9T50g");
            request.AddHeader("Authorization", "Bearer" + tokens["access_token"]);
            request.AddHeader("Accept", "application/json");

            restClient.BaseUrl = new System.Uri("https://www.googleapis.com/calendar/v3/calendars/primary/events");
            var response = restClient.Post(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                JObject calendarEvent = JObject.Parse(response.Content);
                return View(calendarEvent.ToObject<Event>());
            }
            return View("Error");
        }
        public ActionResult AllEvents()
        {
            var tokenFile = "C:\\Users\\Vasilis\\source\\repos\\Last\\Files\\tokens.json";
            var tokens = JObject.Parse(System.IO.File.ReadAllText(tokenFile));

            RestClient restClient = new RestClient();
            RestRequest request = new RestRequest();
            request.AddQueryParameter("key", "AIzaSyAx8awdq97MVxgBKEjI8-SNIpLDwV9T50g");
            request.AddHeader("Authorization", "Bearer" + tokens["access_token"]);
            request.AddHeader("Accept", "application/json");

            restClient.BaseUrl = new System.Uri("https://www.googleapis.com/calendar/v3/calendars/primary/events");
            var response = restClient.Get(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                JObject calendarEvent = JObject.Parse(response.Content);
                var allEvents = calendarEvent["items"].ToObject<IEnumerable<Events>>();
                return View(allEvents);
            }
            return View("Error");
        }

        public ActionResult UpdateEvent(string identifier)
        {
            var tokenFile = "C:\\Users\\Vasilis\\source\\repos\\Last\\Files\\tokens.json";
            var tokens = JObject.Parse(System.IO.File.ReadAllText(tokenFile));

            RestClient restClient = new RestClient();
            RestRequest request = new RestRequest();
            request.AddQueryParameter("key", "AIzaSyAx8awdq97MVxgBKEjI8-SNIpLDwV9T50g");
            request.AddHeader("Authorization", "Bearer" + tokens["access_token"]);
            request.AddHeader("Accept", "application/json");
            restClient.BaseUrl = new System.Uri("https://www.googleapis.com/calendar/v3/calendars/primary/events");
            var response = restClient.Get(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                JObject calendarEvent = JObject.Parse(response.Content);
                return View(calendarEvent.ToObject<Event>());
            }
            return View("Error");

        } 
   

        public ActionResult UpdateEvent(string identifier,Event calendarEvent)
        {
            var tokenFile = "C:\\Users\\Vasilis\\source\\repos\\Last\\Files\\tokens.json";
            var tokens = JObject.Parse(System.IO.File.ReadAllText(tokenFile));

            RestClient restClient = new RestClient();
            RestRequest request = new RestRequest();

            calendarEvent.Start.DateTime = DateTime.Parse(calendarEvent.Start.DateTime).ToString("yyyy-MM-dd'T'HH:mm:ss.fffK");
            calendarEvent.End.DateTime = DateTime.Parse(calendarEvent.End.DateTime).ToString("yyyy-MM-dd'T'HH:mm:ss.fffK");

            var model = JsonConvert.SerializeObject(calendarEvent, new JsonSerializerSettings
            { ContractResolver = new CamelCasePropertyNamesContractResolver()

            }) ;
            request.AddQueryParameter("key", "AIzaSyAx8awdq97MVxgBKEjI8-SNIpLDwV9T50g");
            request.AddHeader("Authorization", "Bearer" + tokens["access_token"]);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", model, ParameterType.RequestBody);
            restClient.BaseUrl = new System.Uri("https://www.googleapis.com/calendar/v3/calendars/primary/events");
            var response = restClient.Patch(request);

            if(response.StatusCode==System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("AllEvents","calendarEvent",new {status="updated"});
            }
            return View("Error");

        }


        public ActionResult DeleteEvent(string identifier)
        {
            var tokenFile = "C:\\Users\\Vasilis\\source\\repos\\Last\\Files\\tokens.json";
            var tokens = JObject.Parse(System.IO.File.ReadAllText(tokenFile));

            RestClient restClient = new RestClient();
            RestRequest request = new RestRequest();
            
            request.AddQueryParameter("key", "AIzaSyAx8awdq97MVxgBKEjI8-SNIpLDwV9T50g");
            request.AddHeader("Authorization", "Bearer" + tokens["access_token"]);
            request.AddHeader("Accept", "application/json");
            
            restClient.BaseUrl = new System.Uri("https://www.googleapis.com/calendar/v3/calendars/primary/events");
            var response = restClient.Delete(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return RedirectToAction("AllEvents", "calendarEvent", new { status = "Deleted" }); //tha allaksei to allevents

            }
            return View("Error");

        }



    }

}

