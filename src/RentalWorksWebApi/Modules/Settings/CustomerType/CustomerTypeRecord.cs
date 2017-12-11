using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.CustomerType
{
    [FwSqlTable("custtype")]
    public class CustomerTypeRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "custtypeid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string CustomerTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "custtype", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string CustomerType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "defaultrentaldiscountpct", modeltype: FwDataTypes.Decimal, precision: 5, scale: 2)]
        public decimal? DefaultRentalDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "defaultsalesdiscountpct", modeltype: FwDataTypes.Decimal, precision: 5, scale: 2)]
        public decimal? DefaultSalesDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "defaultspacediscountpct", modeltype: FwDataTypes.Decimal, precision: 5, scale: 2)]
        public decimal? DefaultFacilitiesDiscountPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        protected override bool Validate(TDataRecordSaveMode saveMode, ref string validateMsg)
        {
            bool isValid = true;
            if (isValid)
            {
                if (DefaultRentalDiscountPercent > 100)
                {
                    validateMsg = "Rental Discount Percent cannot be greater than 100.";
                    isValid = false;
                }
            }
            if (isValid)
            {
                if (DefaultSalesDiscountPercent > 100)
                {
                    validateMsg = "Sales Discount Percent cannot be greater than 100.";
                    isValid = false;
                }
            }
            if (isValid)
            {
                if (DefaultFacilitiesDiscountPercent > 100)
                {
                    validateMsg = "Sales Facilities Percent cannot be greater than 100.";
                    isValid = false;
                }
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------
    }
}
