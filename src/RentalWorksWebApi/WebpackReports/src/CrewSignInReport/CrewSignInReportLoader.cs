using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System;
using WebLibrary;

namespace WebApi.Modules.Reports.CrewSignInReport
{
    [FwSqlTable("funccrewsigninrpt(@rentfromdate, @renttodate)")]
    public class CrewSignInReportLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text)]
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
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            DateTime rentFromDate = DateTime.MinValue;
            DateTime rentToDate = DateTime.MaxValue;
            
            useWithNoLock = false;

            if ((request != null) && (request.uniqueids != null))
            {
                IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                if (uniqueIds.ContainsKey("FromDate"))
                {
                    rentFromDate = FwConvert.ToDateTime(uniqueIds["FromDate"].ToString());
                }
                if (uniqueIds.ContainsKey("ToDate"))
                {
                    rentToDate = FwConvert.ToDateTime(uniqueIds["ToDate"].ToString());
                }
            }

            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("LocationId", "locationid", select, request);
            addFilterToSelect("DepartmentId", "departmentid", select, request);
            addFilterToSelect("CustomerId", "customerid", select, request);
            addFilterToSelect("DealId", "dealid", select, request);
            addFilterToSelect("OrderId", "orderid", select, request);

            select.AddParameter("@rentfromdate", rentFromDate);
            select.AddParameter("@renttodate", rentToDate);

        }
        //------------------------------------------------------------------------------------ 
    }
}
