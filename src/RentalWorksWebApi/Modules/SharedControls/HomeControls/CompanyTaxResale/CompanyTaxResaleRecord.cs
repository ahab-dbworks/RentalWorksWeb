using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.HomeControls.CompanyTaxResale
{
    [FwSqlTable("dealtaxresale")]
    public class CompanyTaxResaleRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "dealtaxresaleid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string CompanyTaxResaleId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text, maxlength: 8, required: true)]
        public string CompanyId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "resaleno", modeltype: FwDataTypes.Text, maxlength: 30, required: true)]
        public string ResaleNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "stateid", modeltype: FwDataTypes.Text, maxlength: 8, required: true)]
        public string StateId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}