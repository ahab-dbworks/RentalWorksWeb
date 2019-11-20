using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Collections.Generic;
using WebApi.Data;

namespace WebApi.Modules.Settings.PoSettings.PoApprover
{
    [FwSqlTable("poapproverview")]
    public class PoApproverLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poapproverid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string PoApproverId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "projectid", modeltype: FwDataTypes.Text)]
        public string ProjectId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usersid", modeltype: FwDataTypes.Text)]
        public string UsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "username", modeltype: FwDataTypes.Text)]
        public string UserName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "approleid", modeltype: FwDataTypes.Text)]
        public string AppRoleId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "approle", modeltype: FwDataTypes.Text)]
        public string AppRole { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "poapprovertype", modeltype: FwDataTypes.Text)]
        public string PoApproverType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "backupflg", modeltype: FwDataTypes.Boolean)]
        public bool? IsBackup { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitflg", modeltype: FwDataTypes.Boolean)]
        public bool? HasLimit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitrental", modeltype: FwDataTypes.Decimal)]
        public decimal? LimitRental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitsales", modeltype: FwDataTypes.Decimal)]
        public decimal? LimitSales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitparts", modeltype: FwDataTypes.Decimal)]
        public decimal? LimitParts { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitvehicle", modeltype: FwDataTypes.Decimal)]
        public decimal? LimitVehicle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitmisc", modeltype: FwDataTypes.Decimal)]
        public decimal? LimitMisc { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitlabor", modeltype: FwDataTypes.Decimal)]
        public decimal? LimitLabor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitsubrent", modeltype: FwDataTypes.Decimal)]
        public decimal? LimitSubRent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitsubsale", modeltype: FwDataTypes.Decimal)]
        public decimal? LimitSubSale { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitsubmisc", modeltype: FwDataTypes.Decimal)]
        public decimal? LimitSubMisc { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitsublabor", modeltype: FwDataTypes.Decimal)]
        public decimal? LimitSubLabor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitsubvehicle", modeltype: FwDataTypes.Decimal)]
        public decimal? LimitSubVehicle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "limitrepair", modeltype: FwDataTypes.Decimal)]
        public decimal? LimitRepair { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            string projectFilterId = "";
            if ((request != null) && (request.uniqueids != null))
            {
                IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                if (uniqueIds.ContainsKey("ProjectId"))
                {
                    projectFilterId = uniqueIds["ProjectId"].ToString();
                }
            }

            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            if (!string.IsNullOrEmpty(projectFilterId))
            {
                select.AddWhere("projectid = @projectid");
                select.AddParameter("@projectid", projectFilterId);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}