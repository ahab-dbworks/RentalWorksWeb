using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Settings.LaborSettings.Crew
{
    [FwSqlTable("crewview")]
    public class CrewLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contactid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string CrewId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usersid", modeltype: FwDataTypes.Text)]
        public string UserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webusersid", modeltype: FwDataTypes.Text)]
        public string WebUserId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "isuser", modeltype: FwDataTypes.Boolean)]
        public bool? IsUser { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salutation", modeltype: FwDataTypes.Text)]
        public string Salutation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "namefml", modeltype: FwDataTypes.Text)]
        public string NameFirstMiddleLast { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "person", modeltype: FwDataTypes.Text)]
        public string Person { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lname", modeltype: FwDataTypes.Text)]
        public string LastName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fname", modeltype: FwDataTypes.Text)]
        public string FirstName { get; set; }
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
        [FwSqlDataField(column: "countryid", modeltype: FwDataTypes.Text)]
        public string CountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "country", modeltype: FwDataTypes.Text)]
        public string Country { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "zip", modeltype: FwDataTypes.Text)]
        public string ZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mi", modeltype: FwDataTypes.Text)]
        public string MiddleInitial { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "position", modeltype: FwDataTypes.Text)]
        public string Position { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "officephone", modeltype: FwDataTypes.Text)]
        public string OfficePhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ext", modeltype: FwDataTypes.Text)]
        public string OfficeExtension { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "directphone", modeltype: FwDataTypes.Text)]
        public string DirectPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "directext", modeltype: FwDataTypes.Text)]
        public string DirectExtension { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fax", modeltype: FwDataTypes.Text)]
        public string Fax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "faxext", modeltype: FwDataTypes.Text)]
        public string FaxExtension { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pager", modeltype: FwDataTypes.Text)]
        public string Pager { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pagerpin", modeltype: FwDataTypes.Text)]
        public string PagerPin { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "cellular", modeltype: FwDataTypes.Text)]
        public string MobilePhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phone", modeltype: FwDataTypes.Text)]
        public string HomePhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "email", modeltype: FwDataTypes.Text)]
        public string Email { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contacttitleid", modeltype: FwDataTypes.Text)]
        public string ContactTitleId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contacttitle", modeltype: FwDataTypes.Text)]
        public string ContactTitle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contractemployee", modeltype: FwDataTypes.Boolean)]
        public bool? ContractEmployee { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contactrecordtype", modeltype: FwDataTypes.Text)]
        public string ContactRecordType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activedate", modeltype: FwDataTypes.Date)]
        public string ActiveDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactivedate", modeltype: FwDataTypes.Date)]
        public string InactiveDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webaccess", modeltype: FwDataTypes.Boolean)]
        public bool? WebAccess { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lockaccount", modeltype: FwDataTypes.Boolean)]
        public bool? LockAccount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webpassword", modeltype: FwDataTypes.Text)]
        public string WebPassword { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "webadministrator", modeltype: FwDataTypes.Boolean)]
        public bool? WebAdministrator { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "changepasswordatlogin", modeltype: FwDataTypes.Boolean)]
        public bool? ChangePasswordAtNextLogin { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "expireflg", modeltype: FwDataTypes.Boolean)]
        public bool? ExpirePassword { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "expiredays", modeltype: FwDataTypes.Integer)]
        public int? ExpireDays { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pwupdated", modeltype: FwDataTypes.Date)]
        public string PasswordLastUpdated { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}