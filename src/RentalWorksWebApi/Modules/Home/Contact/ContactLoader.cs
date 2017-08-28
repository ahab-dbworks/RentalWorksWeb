using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Home.Contact
{
    [FwSqlTable("contactview")]
    public class ContactLoader : RwDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "contactid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string ContactId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "companyid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8, precision: 0, scale: 0)]
        public string CompanyId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "compcontactid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 8, precision: 0, scale: 0)]
        public string CompanyContactId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "company", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, precision: 0, scale: 0)]
        public string Company { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "salutation", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10, precision: 0, scale: 0)]
        public string Salutation { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "namefml", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 65, precision: 0, scale: 0)]
        public string NameFirstMiddleLast { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "person", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 65, precision: 0, scale: 0)]
        public string Person { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "lname", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30, precision: 0, scale: 0)]
        public string LastName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "fname", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30, precision: 0, scale: 0)]
        public string FirstName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "add1", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30, precision: 0, scale: 0)]
        public string Address1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "mi", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 1, precision: 0, scale: 0)]
        public string MiddleInitial { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "add2", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30, precision: 0, scale: 0)]
        public string Address2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "city", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 30, precision: 0, scale: 0)]
        public string City { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "zip", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10, precision: 0, scale: 0)]
        public string Zipcode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "state", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20, precision: 0, scale: 0)]
        public string State { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "countryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string CountryId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "country", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20, precision: 0, scale: 0)]
        public string Country { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "info", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, precision: 0, scale: 0)]
        public string Info { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "website", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100, precision: 0, scale: 0)]
        public string Website { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "email", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, precision: 0, scale: 0)]
        public string Email { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "phone", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20, precision: 0, scale: 0)]
        public string Phone { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "cellular", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20, precision: 0, scale: 0)]
        public string MobilePhone { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "officephone", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20, precision: 0, scale: 0)]
        public string OfficePhone { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ext", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 6, precision: 0, scale: 0)]
        public string Extension { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "pager", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20, precision: 0, scale: 0)]
        public string Pager { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "pagerpin", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string PagerPin { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "fax", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20, precision: 0, scale: 0)]
        public string Fax { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "faxext", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 6, precision: 0, scale: 0)]
        public string FaxeExtension { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "directphone", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20, precision: 0, scale: 0)]
        public string DirectPhone { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "directext", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 6, precision: 0, scale: 0)]
        public string DirectExtension { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "activedate", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime", maxlength: 4, precision: 16, scale: 0)]
        public string ActiveDate { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "inactivedate", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime", maxlength: 4, precision: 16, scale: 0)]
        public string InactiveDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "primaryflag", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 1, precision: 0, scale: 0)]
        public string PrimaryFlag { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "authorized", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 1, precision: 0, scale: 0)]
        public string Authorized { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "contacttitleid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string ContactTitleId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "contacttitle", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50, precision: 0, scale: 0)]
        public string ContactTitle { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "webaccess", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 1, precision: 0, scale: 0)]
        public string WebAccess { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "webstatus", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20, precision: 0, scale: 0)]
        public string WebStatus { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "contactrecordtype", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, precision: 0, scale: 0)]
        public string ContactRecordType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inputdate", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime", maxlength: 4, precision: 16, scale: 0)]
        public string InputDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "moddate", modeltype: FwDataTypes.UTCDateTime, sqltype: "smalldatetime", maxlength: 4, precision: 16, scale: 0)]
        public string ModifiedDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20, precision: 0, scale: 0)]
        public string Barcode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "ID", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, precision: 0, scale: 0)]
        public string ID { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rwnetenabled", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, precision: 0, scale: 0)]
        public string RwNetEnabled { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rwnetquoterequest", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, precision: 0, scale: 0)]
        public string RwNetQuoteRequest { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rwnetwebreports", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, precision: 0, scale: 0)]
        public string RwNetWebReports { get; set; }
    }
}
