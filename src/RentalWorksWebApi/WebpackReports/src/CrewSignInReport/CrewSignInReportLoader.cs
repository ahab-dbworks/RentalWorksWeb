using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System;
using WebLibrary;
using System.Threading.Tasks;

namespace WebApi.Modules.Reports.CrewSignInReport
{
    [FwSqlTable("funccrewsigninrpt(@rentfromdate, @renttodate)")]
    public class CrewSignInReportLoader : AppReportLoader
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "reccount", modeltype: FwDataTypes.Integer)]
        public int? RecCount { get; set; } = 1;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text)]
        public string OrderId { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string LocationId { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string MasterId { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderlocation", modeltype: FwDataTypes.Text)]
        public string OrderLocation { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentfromdate", modeltype: FwDataTypes.Date)]
        public string RentFromDate { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "renttodate", modeltype: FwDataTypes.Date)]
        public string RentToDate { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentfromtime", modeltype: FwDataTypes.Text)]
        public string RentFromTime { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "renttotime", modeltype: FwDataTypes.Text)]
        public string RentToTime { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderno", modeltype: FwDataTypes.Text)]
        public string OrderNo { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderdesc", modeltype: FwDataTypes.Text)]
        public string OrderDescription { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealno", modeltype: FwDataTypes.Text)]
        public string DealNo { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "deal", modeltype: FwDataTypes.Text)]
        public string Deal { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "position", modeltype: FwDataTypes.Text)]
        public string Position { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "employeeid", modeltype: FwDataTypes.Text)]
        public string EmployeeId { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "person", modeltype: FwDataTypes.Text)]
        public string Person { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "crewcontactid", modeltype: FwDataTypes.Text)]
        public string CrewContactId { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        public async Task<FwJsonDataTable> RunReportAsync(CrewSignInReportRequest request)
        {
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
            {
                useWithNoLock = false;
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
				select.UseOptionRecompile = true;
                using (FwSqlCommand qry = new FwSqlCommand(conn, AppConfig.DatabaseSettings.ReportTimeout))
                {
                    SetBaseSelectQuery(select, qry);
                    select.Parse();
                    select.AddWhereIn("locationid", request.OfficeLocationId);
                    select.AddWhereIn("departmentid", request.DepartmentId);
                    select.AddWhereIn("customerid", request.CustomerId);
                    select.AddWhereIn("dealid", request.DealId);
                    select.AddWhereIn("orderid", request.OrderId);
                    select.AddParameter("@rentfromdate", request.FromDate);
                    select.AddParameter("@renttodate", request.ToDate);
                    select.AddOrderBy("location,deal,rentfromdate");

                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }
            }

            if (request.IncludeSubHeadingsAndSubTotals)
            {
                dt.Columns[dt.GetColumnNo("RowType")].IsVisible = true;
                string[] totalFields = new string[] { "RecCount" };
                dt.InsertSubTotalRows("Location", "RowType", totalFields);
                dt.InsertSubTotalRows("Deal", "RowType", totalFields);
                dt.InsertSubTotalRows("RentFromDate", "RowType", totalFields);
                dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);
            }

            return dt;
        }
        //------------------------------------------------------------------------------------    
    }
}
