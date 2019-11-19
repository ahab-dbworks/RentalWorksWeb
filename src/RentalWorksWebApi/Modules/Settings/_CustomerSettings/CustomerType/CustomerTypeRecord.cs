using FwStandard.BusinessLogic;
using FwStandard.Data;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Threading.Tasks;
using WebApi.Data;

namespace WebApi.Modules.Settings.CustomerSettings.CustomerType
{
    [FwSqlTable("custtype")]
    public class CustomerTypeRecord : AppDataReadWriteRecord
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
        [FwSqlDataField(column: "color", modeltype: FwDataTypes.OleToHtmlColor)]
        public string Color { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        protected override async Task<FwValidateResult> ValidateAsync(TDataRecordSaveMode saveMode, FwDataReadWriteRecord original, FwValidateResult result)
        {
            if (result.IsValid)
            {
                if (DefaultRentalDiscountPercent > 100)
                {
                    result.ValidateMsg = "Rental Discount Percent cannot be greater than 100.";
                    result.IsValid = false;
                }
            }
            if (result.IsValid)
            {
                if (DefaultSalesDiscountPercent > 100)
                {
                    result.ValidateMsg = "Sales Discount Percent cannot be greater than 100.";
                    result.IsValid = false;
                }
            }
            if (result.IsValid)
            {
                if (DefaultFacilitiesDiscountPercent > 100)
                {
                    result.ValidateMsg = "Sales Facilities Percent cannot be greater than 100.";
                    result.IsValid = false;
                }
            }
            await Task.CompletedTask;
            return result;
        }
        //------------------------------------------------------------------------------------
    }
}
