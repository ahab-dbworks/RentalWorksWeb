using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Home.CompanyContact
{
    [FwSqlTable("compcontact")]
    public class CompanyContactRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "compcontactid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string CompanyContactId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "companyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string CompanyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contactid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string ContactId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "jobtitle", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string JobTitle { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "contacttitleid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string ContactTitleId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primaryflag", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsPrimary { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "activedate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string ActiveDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactivedate", modeltype: FwDataTypes.Date, sqltype: "smalldatetime")]
        public string InactiveDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "authorized", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Authorized { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ordernotify", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? OrderNotify { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "officephone", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string OfficePhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ext", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 6)]
        public string OfficeExtension { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fax", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        public string Fax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "faxext", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 6)]
        public string FaxExtension { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "email", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string Email { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "directphone", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string DirectPhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "directext", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 6)]
        public string DirectExtension { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pager", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string Pager { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pagerpin", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string PagerPin { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "printflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Printable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mobilephone", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 15)]
        public string MobilePhone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}