using FwStandard.BusinessLogic; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;
namespace WebApi.Modules.Settings.DiscountTemplateSettings.DiscountTemplate
{
    [FwSqlTable("discounttemplate")]
    public class DiscountTemplateRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discounttemplateid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string DiscountTemplateId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discounttemplate", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 40, required: true)]
        public string DiscountTemplate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "iscompany", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? IsCompany { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labor", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Labor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "misc", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Misc { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rental", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Rental { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaldiscountpct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 2)]
        public decimal? RentalDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaldw", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 3)]
        public decimal? RentalDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sales", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Sales { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesdiscountpct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 2)]
        public decimal? SalesDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "space", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Space { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacediscountpct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 2)]
        public decimal? SpaceDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "laborasof", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string LaborAsOf { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "miscasof", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string MiscAsOf { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalasof", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string RentalAsOf { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salesasof", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string SalesAsOf { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spaceasof", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        public string SpaceAsOf { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "spacedw", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 5, scale: 3)]
        public decimal? SpaceDaysPerWeek { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "companyid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CompanyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "applydiscounttocustomrate", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? ApplyDiscountToCustomRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}