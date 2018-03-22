using System;

namespace SalesForceIntegration.Models
{
    public class SalesForceEvent
    {
        public int Id;
        public string Subject;
        public DateTime? StartDate;
        public DateTime? EndDate;
        public string Location;
        public string Description;
    }

    public class CalendarEvent
    {

        public int id;
        public DateTime? start;
        public string title;
        public DateTime? end;
        public string location;
        public string description;
    }
}
