using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Agent.Contact
{
    [FwSqlTable("contact")]
    public class ContactRecord : AppDataReadWriteRecord
    {
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "contactid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0, isPrimaryKey: true)]
        public string ContactId { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "salutation", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10, precision: 0, scale: 0)]
        public string Salutation { get; set; }
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
        public string ZipCode { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "phone", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20, precision: 0, scale: 0)]
        public string HomePhone { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "state", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20, precision: 0, scale: 0)]
        public string State { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "fax", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20, precision: 0, scale: 0)]
        public string Fax { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "email", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, precision: 0, scale: 0)]
        public string Email { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "officephone", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20, precision: 0, scale: 0)]
        public string OfficePhone { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "pager", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20, precision: 0, scale: 0)]
        public string Pager { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "pagerpin", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string PagerPin { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "faxext", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 6, precision: 0, scale: 0)]
        public string FaxExtension { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "cellular", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20, precision: 0, scale: 0)]
        public string MobilePhone { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "countryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string CountryId { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "directphone", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20, precision: 0, scale: 0)]
        public string DirectPhone { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "ext", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 6, precision: 0, scale: 0)]
        public string OfficeExtension { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "directext", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 6, precision: 0, scale: 0)]
        public string DirectExtension { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public bool? Inactive { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "info", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, precision: 0, scale: 0)]
        public string Info { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "website", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100, precision: 0, scale: 0)]
        public string Website { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "activedate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string ActiveDate { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "inactivedate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string InactiveDate { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "inputbyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string InputById { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "modbyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string LastModifiedByUserId { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "inputdate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string InputDate { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "moddate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string ModifiedDate { get; set; }
	    //------------------------------------------------------------------------------------
	    //[FwSqlDataField(column: "company", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50, precision: 0, scale: 0)]
     //   public string Company { get; set; }
	    ////------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "contactrecordtype", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20, precision: 0, scale: 0)]
        public string ContactRecordType { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "contacttitleid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string ContactTitleId { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "contacttype", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10, precision: 0, scale: 0)]
        public string ContactType { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string DealId { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "jobtitle", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50, precision: 0, scale: 0)]
        public string JobTitle { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string LocationId { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string WarehouseId { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "webstatus", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20, precision: 0, scale: 0)]
        public string WebStatus { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "webstatusasof", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string WebStatusAsOf { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "webstatusupdatebyusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string WebStatusUpdateByUsersId { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "persontype", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10, precision: 0, scale: 0)]
        public string PersonType { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "overridepastdue", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 1, precision: 0, scale: 0)]
        public string OverridePastDue { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "fname", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30, precision: 0, scale: 0)]
        public string FirstName { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "lname", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30, precision: 0, scale: 0)]
        public string LastName { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "person", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 65, precision: 0, scale: 0)]
        public string Person { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "namefml", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 65, precision: 0, scale: 0)]
        public string NameFirstMiddleLast { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20, precision: 0, scale: 0)]
        public string Barcode { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "sourceid", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, precision: 0, scale: 0)]
        public string SourceId { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "webcatalogid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string WebCatalogId { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "poordertypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string PoOrderTypeId { get; set; }
	    //------------------------------------------------------------------------------------
	    [FwSqlDataField(column: "contactnametype", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10, precision: 0, scale: 0)]
        public string ContactNameType { get; set; }
	    //------------------------------------------------------------------------------------
		[FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, precision: 0, scale: 0)]
        public string DepartmentId { get; set; }
	    
    }
}
