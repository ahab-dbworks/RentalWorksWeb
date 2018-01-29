using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Home.CompanyContact
{
    [FwSqlTable("compcontactview")]
    public class CompanyContactLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "compcontactid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string CompanyContactId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "companyid", modeltype: FwDataTypes.Text)]
        public string CompanyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "company", modeltype: FwDataTypes.Text)]
        public string Company { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "companytype", modeltype: FwDataTypes.Text)]
        public string CompanyType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "companytypecolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string CompanyTypeColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contactid", modeltype: FwDataTypes.Text)]
        public string ContactId { get; set; }
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
        [FwSqlDataField(column: "mi", modeltype: FwDataTypes.Text)]
        public string MiddleInitial { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "jobtitle", modeltype: FwDataTypes.Text)]
        public string JobTitle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contacttitleid", modeltype: FwDataTypes.Text)]
        public string ContactTitleId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contacttitle", modeltype: FwDataTypes.Text)]
        public string ContactTitle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primaryflag", modeltype: FwDataTypes.Boolean)]
        public bool? IsPrimary { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activedate", modeltype: FwDataTypes.Date)]
        public string ActiveDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactivedate", modeltype: FwDataTypes.Date)]
        public string InactiveDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "authorized", modeltype: FwDataTypes.Boolean)]
        public bool? Authorized { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordernotify", modeltype: FwDataTypes.Boolean)]
        public bool? OrderNotify { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "officephone", modeltype: FwDataTypes.Text)]
        public string OfficePhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ext", modeltype: FwDataTypes.Text)]
        public string OfficeExtension { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fax", modeltype: FwDataTypes.Text)]
        public string Fax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "faxext", modeltype: FwDataTypes.Text)]
        public string FaxExtension { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "email", modeltype: FwDataTypes.Text)]
        public string Email { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "directphone", modeltype: FwDataTypes.Text)]
        public string DirectPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "directext", modeltype: FwDataTypes.Text)]
        public string DirectExtension { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pager", modeltype: FwDataTypes.Text)]
        public string Pager { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pagerpin", modeltype: FwDataTypes.Text)]
        public string PagerPin { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "printflg", modeltype: FwDataTypes.Boolean)]
        public bool? Printable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mobilephone", modeltype: FwDataTypes.Text)]
        public string MobilePhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("CompanyId", "companyid", select, request); 
            addFilterToSelect("ContactId", "contactid", select, request);


            if ((request != null) && (request.activeview != null) && (!request.activeview.Equals(string.Empty)))
            {
                switch (request.activeview)
                {
                    case "ALL":
                        break;
                    default:
                        select.AddWhere("(companytype = @companytype)");
                        select.AddParameter("@companytype", request.activeview);
                        break;
                }
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}