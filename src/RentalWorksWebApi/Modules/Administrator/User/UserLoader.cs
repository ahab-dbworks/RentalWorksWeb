using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using RentalWorksWebApi.Data; 
using System.Collections.Generic;
namespace RentalWorksWebApi.Modules.Administrator.User
{
    [FwSqlTable("webusersview")]
    public class UserLoader : RwDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "webusersid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        //public string UserId { get; set; } = "";
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usersid", modeltype: FwDataTypes.Text)]
        public string UserId { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "contactid", modeltype: FwDataTypes.Text)]
        //public string ContactId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        //public string DealId { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "name", modeltype: FwDataTypes.Text)]
        public string Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "username", modeltype: FwDataTypes.Text)]
        public string UserName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fullname", modeltype: FwDataTypes.Text)]
        public string FullName { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "webaccess", modeltype: FwDataTypes.Boolean)]
        //public bool WebAccess { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "webadministrator", modeltype: FwDataTypes.Boolean)]
        //public bool WebAdministrator { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "webadministratortext", modeltype: FwDataTypes.Text)]
        //public string Webadministratortext { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "webpassword", modeltype: FwDataTypes.Text)]
        //public string Webpassword { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "lockaccount", modeltype: FwDataTypes.Boolean)]
        //public bool Lockaccount { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "registered", modeltype: FwDataTypes.Boolean)]
        //public bool Registered { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "registerdate", modeltype: FwDataTypes.UTCDateTime)]
        //public string Registerdate { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "email", modeltype: FwDataTypes.Text)]
        public string Email { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "userloginname", modeltype: FwDataTypes.Text)]
        //public string Userloginname { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "userpassword", modeltype: FwDataTypes.Text)]
        //public string Userpassword { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "tmppassword", modeltype: FwDataTypes.Text)]
        //public string Tmppassword { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "changepasswordatlogin", modeltype: FwDataTypes.Boolean)]
        //public bool Changepasswordatlogin { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "resetpassword", modeltype: FwDataTypes.Boolean)]
        //public bool Resetpassword { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "primarydepartmentid", modeltype: FwDataTypes.Text)]
        //public string PrimarydepartmentId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "rentaldepartmentid", modeltype: FwDataTypes.Text)]
        //public string RentaldepartmentId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "salesdepartmentid", modeltype: FwDataTypes.Text)]
        //public string SalesdepartmentId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "rentalagentusersid", modeltype: FwDataTypes.Text)]
        //public string RentalagentusersId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "salesagentusersid", modeltype: FwDataTypes.Text)]
        //public string SalesagentusersId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "partsdepartmentid", modeltype: FwDataTypes.Text)]
        //public string PartsdepartmentId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "labordepartmentid", modeltype: FwDataTypes.Text)]
        //public string LabordepartmentId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "miscdepartmentid", modeltype: FwDataTypes.Text)]
        //public string MiscdepartmentId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "spacedepartmentid", modeltype: FwDataTypes.Text)]
        //public string SpacedepartmentId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "titletype", modeltype: FwDataTypes.Text)]
        //public string Titletype { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "title", modeltype: FwDataTypes.Text)]
        //public string Title { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "sourceid1", modeltype: FwDataTypes.Text)]
        //public string Sourceid1 { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "groupsid", modeltype: FwDataTypes.Text)]
        //public string GroupsId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        //public string Department { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        //public string DepartmentId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        //public string LocationId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        //public string Location { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        //public string WarehouseId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        //public string Warehouse { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "webreports", modeltype: FwDataTypes.Boolean)]
        //public bool Webreports { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "webquoterequest", modeltype: FwDataTypes.Boolean)]
        //public bool Webquoterequest { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "defaultpoordertypeid", modeltype: FwDataTypes.Text)]
        //public string DefaultpoordertypeId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "webcatalogid", modeltype: FwDataTypes.Text)]
        //public string WebcatalogId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "office", modeltype: FwDataTypes.Text)]
        //public string Office { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "phoneextension", modeltype: FwDataTypes.Text)]
        //public string Phoneextension { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "fax", modeltype: FwDataTypes.Text)]
        //public string Fax { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "iscrew", modeltype: FwDataTypes.Boolean)]
        //public bool Iscrew { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "usertype", modeltype: FwDataTypes.Text)]
        //public string Usertype { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddWhere("(username > '')");
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
        }
        //------------------------------------------------------------------------------------    } 
    }
}