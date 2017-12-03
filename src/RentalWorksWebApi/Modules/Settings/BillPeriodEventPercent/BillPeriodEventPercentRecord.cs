using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.BillPeriodEventPercent
{
    [FwSqlTable("billperiodeventpercent")]
    public class BillPeriodEventPercentRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodid", modeltype: FwDataTypes.Text, maxlength: 8, required: true)]
        public string BillPeriodId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodeventpercentid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string BillPeriodEventPercentId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billperiodevent", modeltype: FwDataTypes.Text, maxlength: 50, required: true)]
        public string BillPeriodEvent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "billpercent", modeltype: FwDataTypes.Integer)]
        public int? BillPercent { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Integer)]
        public int? OrderBy { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "revenue", modeltype: FwDataTypes.Boolean)]
        public bool? Revenue { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
