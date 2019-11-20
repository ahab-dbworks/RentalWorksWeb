using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.HomeControls.Tax
{
    [FwSqlTable("tax")]
    public class TaxRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string TaxId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxoptionid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string TaxOptionId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labortaxrate1", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 6, scale: 4)]
        public decimal? LaborTaxRate1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "labortaxrate2", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 6, scale: 4)]
        public decimal? LaborTaxRate2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaltaxrate1", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 6, scale: 4)]
        public decimal? RentalTaxRate1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentaltaxrate2", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 6, scale: 4)]
        public decimal? RentalTaxRate2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salestaxrate1", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 6, scale: 4)]
        public decimal? SalesTaxRate1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "salestaxrate2", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 6, scale: 4)]
        public decimal? SalesTaxRate2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
