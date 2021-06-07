using DayPilot.Web.Mvc;
using DayPilot.Web.Mvc.Events.Calendar;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Test2Admin.Models;

namespace Test2Admin.Controllers
{

    public class StudentController : Controller
    {
        AdminPageEntities db = new AdminPageEntities();
        public List<CalendarEvent> GoogleEvents = new List<CalendarEvent>();
        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string ApplicationName = "Google Calendar API .NET Quickstart";
        // GET: Student
        public ActionResult Index(string searching)

        {
            int userID = Convert.ToInt32(Session["UserID"]);
            if (userID != 0)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
   

        public void CalendarEvents()
        {

            UserCredential credential;
            string path = Server.MapPath("~/Credentials.json");
            using (var stream =
                new FileStream(path, FileMode.Open, FileAccess.Read))
            {

                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;

            }


            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            Events events = request.Execute();
            Console.WriteLine("Nadchodzące wydarzenia:");
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    var calendarEvent = new CalendarEvent();
                    calendarEvent.Organizer = eventItem.Organizer.Email;
                    calendarEvent.Summary = eventItem.Summary;
                    calendarEvent.StartTime = eventItem.Start.DateTime.ToString();
                    calendarEvent.EndTime = eventItem.End.DateTime.ToString();
                    calendarEvent.Description = eventItem.Description;
                    GoogleEvents.Add(calendarEvent);
                }
            }




        }
    }
}
