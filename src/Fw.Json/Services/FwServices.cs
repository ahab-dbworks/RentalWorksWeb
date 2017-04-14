using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using Fw.Json.ValueTypes;

namespace Fw.Json.Services
{
    public class FwServices
    {
        //---------------------------------------------------------------------------------------------
        public static void GetHolidayEvents(FwSqlConnection conn, dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "GetHolidayEvents";
            FwDateTime fromDate;
            double days;
            string countryid;
            List<dynamic> resources;
            dynamic holidayResource;
            
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "start");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "days");
            fromDate  = new FwDateTime(request.start);
            days      = (double)request.days;
            countryid = string.Empty;
            resources = new List<dynamic>();
            holidayResource = new ExpandoObject();
            holidayResource.id   = "1";
            holidayResource.name = "Holidays";
            resources.Add(holidayResource);
            response.resources = resources;
            response.events    = FwSqlData.GetHolidayEvents(conn, fromDate, days, countryid, holidayResource.id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
