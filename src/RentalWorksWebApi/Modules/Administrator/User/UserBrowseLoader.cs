using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Administrator.User
{
    [FwSqlTable("webusersview")]
    public class UserBrowseLoader : AppDataLoadRecord
    {
        [FwSqlDataField(column: "usersid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string UserId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "name", modeltype: FwDataTypes.Text)]
        public string Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "username", modeltype: FwDataTypes.Text)]
        public string UserName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fullname", modeltype: FwDataTypes.Text)]
        public string FullName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "firstname", modeltype: FwDataTypes.Text)]
        public string FirstName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "middleinitial", modeltype: FwDataTypes.Text)]
        public string MiddleInitial { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lastname", modeltype: FwDataTypes.Text)]
        public string LastName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loginname", modeltype: FwDataTypes.Text)]
        public string LoginName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "groupsid", modeltype: FwDataTypes.Text)]
        public string GroupId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "groups", modeltype: FwDataTypes.Text)]
        public string GroupName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydepartmentid", modeltype: FwDataTypes.Text)]
        public string PrimaryDepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primarydepartment", modeltype: FwDataTypes.Text)]
        public string PrimaryDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddWhere("(username > '')");
            select.AddWhere("(groupsid > '')");
            addFilterToSelect("LocationId", "locationid", select, request); 
            addFilterToSelect("WarehouseId", "warehouseid", select, request); 
            addFilterToSelect("GroupId", "groupsid", select, request);


            if (request.activeview.Contains("WarehouseId="))
            {
                string whId = request.activeview.Replace("WarehouseId=", "");
                if (!whId.Equals("ALL"))
                {
                    select.AddWhere("(warehouseid = @whid)");
                    select.AddParameter("@whid", whId);
                }
            }

            string locId = "ALL";
            if (request.activeview.Contains("OfficeLocationId="))
            {
                locId = request.activeview.Replace("OfficeLocationId=", "");
            }
            else if (request.activeview.Contains("LocationId="))
            {
                locId = request.activeview.Replace("LocationId=", "");
            }
            if (!locId.Equals("ALL"))
            {
                select.AddWhere("(locationid = @locid)");
                select.AddParameter("@locid", locId);
            }
        }
        //------------------------------------------------------------------------------------     
    }
}