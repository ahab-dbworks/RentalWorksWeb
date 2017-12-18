using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Administrator.User
{
    [FwSqlTable("webusersview")]
    public class UserLoader : AppDataLoadRecord
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
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "groupsid", modeltype: FwDataTypes.Text)]
        public string GroupId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "groups", modeltype: FwDataTypes.Text)]
        public string GroupName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "schedulecolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ScheduleColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contacttitleid", modeltype: FwDataTypes.Text)]
        public string UserTitleId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contacttitle", modeltype: FwDataTypes.Text)]
        public string UserTitle { get; set; }
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


        [FwSqlDataField(column: "add1", modeltype: FwDataTypes.Text)]
        public string Address1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "add2", modeltype: FwDataTypes.Text)]
        public string Address2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "city", modeltype: FwDataTypes.Text)]
        public string City { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "state", modeltype: FwDataTypes.Text)]
        public string State { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "zip", modeltype: FwDataTypes.Text)]
        public string ZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "countryid", modeltype: FwDataTypes.Text)]
        public string CountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "country", modeltype: FwDataTypes.Text)]
        public string Country { get; set; }
        //------------------------------------------------------------------------------------ 



        [FwSqlDataField(column: "office", modeltype: FwDataTypes.Text)]
        public string OfficePhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phoneextension", modeltype: FwDataTypes.Text)]
        public string OfficeExtension { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fax", modeltype: FwDataTypes.Text)]
        public string Fax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "directphone", modeltype: FwDataTypes.Text)]
        public string DirectPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pager", modeltype: FwDataTypes.Text)]
        public string Pager { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pagerpin", modeltype: FwDataTypes.Text)]
        public string PagerPin { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cellular", modeltype: FwDataTypes.Text)]
        public string Cellular { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "home", modeltype: FwDataTypes.Text)]
        public string HomePhone { get; set; }
        //------------------------------------------------------------------------------------ 


        [FwSqlDataField(column: "defaultdepttype", modeltype: FwDataTypes.Text)]
        public string DefaultDepartmentType { get; set; }
        //------------------------------------------------------------------------------------ 

        [FwSqlDataField(column: "rentaldepartmentid", modeltype: FwDataTypes.Text)]
        public string RentalDepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaldepartment", modeltype: FwDataTypes.Text)]
        public string RentalDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesdepartmentid", modeltype: FwDataTypes.Text)]
        public string SalesDepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesdepartment", modeltype: FwDataTypes.Text)]
        public string SalesDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partsdepartmentid", modeltype: FwDataTypes.Text)]
        public string PartsDepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "partsdepartment", modeltype: FwDataTypes.Text)]
        public string PartsDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscdepartmentid", modeltype: FwDataTypes.Text)]
        public string MiscDepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscdepartment", modeltype: FwDataTypes.Text)]
        public string MiscDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labordepartmentid", modeltype: FwDataTypes.Text)]
        public string LaborDepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labordepartment", modeltype: FwDataTypes.Text)]
        public string LaborDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacedepartmentid", modeltype: FwDataTypes.Text)]
        public string FacilityDepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacedepartment", modeltype: FwDataTypes.Text)]
        public string FacilityDepartment { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "transportationdepartmentid", modeltype: FwDataTypes.Text)]
        public string TransportationDepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "transportationdepartment", modeltype: FwDataTypes.Text)]
        public string TransportationDepartment { get; set; }
        //------------------------------------------------------------------------------------ 

        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "webaccess", modeltype: FwDataTypes.Boolean)]
        //public bool? WebAccess { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "webadministrator", modeltype: FwDataTypes.Boolean)]
        //public bool? WebAdministrator { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "webadministratortext", modeltype: FwDataTypes.Text)]
        //public string Webadministratortext { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "webpassword", modeltype: FwDataTypes.Text)]
        //public string Webpassword { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "lockaccount", modeltype: FwDataTypes.Boolean)]
        //public bool? Lockaccount { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "registered", modeltype: FwDataTypes.Boolean)]
        //public bool? Registered { get; set; }
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
        //public bool? Changepasswordatlogin { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "resetpassword", modeltype: FwDataTypes.Boolean)]
        //public bool? Resetpassword { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "primarydepartmentid", modeltype: FwDataTypes.Text)]
        //public string PrimarydepartmentId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "rentalagentusersid", modeltype: FwDataTypes.Text)]
        //public string RentalagentusersId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "salesagentusersid", modeltype: FwDataTypes.Text)]
        //public string SalesagentusersId { get; set; }
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
        //[FwSqlDataField(column: "department", modeltype: FwDataTypes.Text)]
        //public string Department { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text)]
        //public string DepartmentId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "webreports", modeltype: FwDataTypes.Boolean)]
        //public bool? Webreports { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "webquoterequest", modeltype: FwDataTypes.Boolean)]
        //public bool? Webquoterequest { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "defaultpoordertypeid", modeltype: FwDataTypes.Text)]
        //public string DefaultpoordertypeId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "webcatalogid", modeltype: FwDataTypes.Text)]
        //public string WebcatalogId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "iscrew", modeltype: FwDataTypes.Boolean)]
        //public bool? Iscrew { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "usertype", modeltype: FwDataTypes.Text)]
        //public string Usertype { get; set; }
        ////------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddWhere("(username > '')");
            addFilterToSelect("LocationId", "locationid", select, request); 
            addFilterToSelect("WarehouseId", "warehouseid", select, request); 
            addFilterToSelect("GroupId", "groupsid", select, request); 
        }
        //------------------------------------------------------------------------------------    } 
    }
}