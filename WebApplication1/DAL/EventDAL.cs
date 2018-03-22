﻿using SalesForceIntegration.Models;
using SalesForceIntegration.SalesForceService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SalesForceIntegration.DAL
{
    public class EventDAL
    {
        private static SforceService SfdcBinding;

        static EventDAL()
        {

            if (SfdcBinding == null)
            {
                string userName;

                string password;
                userName = ConfigurationManager.AppSettings["SalesForceUserName"];
                password = ConfigurationManager.AppSettings["SalesForcePassword"];
                string securityToken = ConfigurationManager.AppSettings["SalesForceSecurityToken"];
                LoginResult CurrentLoginResult = null;

                SfdcBinding = new SforceService();
                try
                {

                    CurrentLoginResult = SfdcBinding.login(userName, password + securityToken);

                    //Change the binding to the new endpoint

                    SfdcBinding.Url = CurrentLoginResult.serverUrl;

                    //Create a new session header object and set the session id to that returned by the login

                    SessionHeader sessionHeader = new SessionHeader();
                    SfdcBinding.SessionHeaderValue = sessionHeader;

                    SfdcBinding.SessionHeaderValue.sessionId = CurrentLoginResult.sessionId;


                }
                catch (Exception e)
                {
                    
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<SalesForceEvent> GetEvents()
        {
            List<SalesForceEvent> listEvents = new List<SalesForceEvent>();
            try
            {
                QueryResult queryResult = null;

                String SOQL = "";

                SOQL = "select subject,StartDateTime ,EndDateTime,Location, Description from event";

                queryResult = SfdcBinding.query(SOQL);


                for (int i = 0; i < queryResult.size; i++)
                {
                    SalesForceEvent sfEvent = new SalesForceEvent();
                    Event salesforceEvent = (Event)queryResult.records[i];
                    sfEvent.Subject = salesforceEvent.Subject;
                    sfEvent.StartDate = salesforceEvent.StartDateTime;
                    sfEvent.EndDate = salesforceEvent.EndDateTime; ;
                    sfEvent.Location = salesforceEvent.Location;
                    sfEvent.Description = salesforceEvent.Description;
                    sfEvent.Id = i;
                    listEvents.Add(sfEvent);
                }

                return listEvents;
            }
            catch (Exception e)
            {

                return listEvents;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<SalesForceEvent> CreateEvent()
        {
            List<SalesForceEvent> listEvents = new List<SalesForceEvent>();
            try
            {
                QueryResult queryResult = null;

                String SOQL = "";

                SOQL = "select subject,StartDateTime ,EndDateTime from event";

                queryResult = SfdcBinding.query(SOQL);


                for (int i = 0; i < queryResult.size; i++)
                {
                    SalesForceEvent sfEvent = new SalesForceEvent();
                    Event salesforceEvent = (Event)queryResult.records[i];
                    sfEvent.Subject = salesforceEvent.Subject;
                    sfEvent.StartDate = salesforceEvent.StartDateTime;
                    sfEvent.EndDate = salesforceEvent.EndDateTime; ;
                    sfEvent.Id = i;
                    listEvents.Add(sfEvent);
                }

                return listEvents;
            }
            catch (Exception e)
            {

                return listEvents;
            }

        }
    }
}