﻿using SalesForceIntegration.DAL;
using SalesForceIntegration.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SalesForceIntegration.Controllers
{
    public class EventController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Calendar()
        {
            return View();
        }

        public ActionResult CreateEvent()
        {
            return View(new SalesForceEvent());
        }

        // GET: Event
        public JsonResult GetEvents()
        {
          
            List<SalesForceEvent> listEvents = new List<SalesForceEvent>();
            EventDAL eventdal = new EventDAL();
            listEvents = eventdal.GetEvents();
            return Json(listEvents, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCalendarEvents()
        {

            List<SalesForceEvent> listEvents = new List<SalesForceEvent>();
            EventDAL eventdal = new EventDAL();
            listEvents = eventdal.GetEvents();
            List<CalendarEvent> calendarevents = new List<CalendarEvent>();
            foreach(SalesForceEvent sfe in listEvents)
            {
                CalendarEvent calaevent = new CalendarEvent();
                calaevent.id = sfe.Id;
                calaevent.title = sfe.Subject;
                calaevent.start = sfe.StartDate;
                calaevent.end = sfe.EndDate;
                calaevent.location = sfe.Location;
                calaevent.description = sfe.Description;

                calendarevents.Add(calaevent);
            }
            return Json(calendarevents, JsonRequestBehavior.AllowGet);
        }
    }
}