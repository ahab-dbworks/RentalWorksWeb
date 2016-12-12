using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace RentalWorksQuikScanLibrary.DataWarehouse
{
    class RwDataWarehouseReportService
    {
        //----------------------------------------------------------------------------------------------------
        public static void GetCustomerRevenueByMonth(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "CustomerRevenueByMonth";
            DataTable dt;
            string report;
            string fileName;

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "fromDate");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "toDate");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "activityTypes");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "customers");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "departments");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "locations");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "deals");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "dealTypes");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "categories");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "iCodes");
            
            dt = RwDataWarehouseReportData.GetDwCustomerRevenueByMonth(
                  conn:          FwSqlConnection.RentalWorksDW
                , fromDate:      request.fromDate
                , toDate:        request.toDate
                , activityTypes: FwConvert.ToStringList(request.activityTypes)
                , customers:     FwConvert.ToStringList(request.customers)
                , departments:   FwConvert.ToStringList(request.departments)
                , locations:     FwConvert.ToStringList(request.locations)
                , deals:         FwConvert.ToStringList(request.deals)
                , dealTypes:     FwConvert.ToStringList(request.dealTypes)
                , categories:    FwConvert.ToStringList(request.categories)
                , iCodes:        FwConvert.ToStringList(request.iCodes)
            );
            report = FwReport.ToCSV(dt);
            fileName = Guid.NewGuid().ToString() + ".csv";
            File.WriteAllText(HttpContext.Current.Server.MapPath("~/temp/" + fileName), report);
            response.url = System.Web.VirtualPathUtility.ToAbsolute("~/temp/" + fileName);
        }
        //----------------------------------------------------------------------------------------------------
    }
}
