using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Modules.Reports.OrderReports.OrderReport;

namespace WebApi.Modules.Reports.OrderReports.QuoteReport
{

    //------------------------------------------------------------------------------------ 
    public class QuoteReportLoader : OrderReportLoader
    {

        //------------------------------------------------------------------------------------ 
        public async Task<OrderReportLoader> RunReportAsync(QuoteReportRequest request)
        {
            OrderReportRequest orderRequest = new OrderReportRequest();
            orderRequest.OrderId = request.QuoteId;
            return await RunReportAsync(orderRequest);

        }
        //------------------------------------------------------------------------------------ 

    }



}
