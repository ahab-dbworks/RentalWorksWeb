using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;

namespace WebApi.Data.Settings.PaymentSettings.PaymentType
{
    [FwSqlTable("paytype")]
    public class PaymentTypeRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "paytypeid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string PaymentTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "paytype", modeltype: FwDataTypes.Text, maxlength: 50, required: true)]
        public string PaymentType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "short", modeltype: FwDataTypes.Text, maxlength: 11, required: true)]
        public string ShortName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "pmttype", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string PaymentTypeType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "glaccountid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string GlAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "accttran", modeltype: FwDataTypes.Boolean)]
        public bool? AccountingTransaction { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "exportpaymentmethod", modeltype: FwDataTypes.Text, maxlength: 30)]
        public string ExportPaymentMethod { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "exportpaymenttype", modeltype: FwDataTypes.Text, maxlength: 30)]
        public string ExportPaymentType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "includeinrwnet", modeltype: FwDataTypes.Boolean)]
        public bool? IncludeInRentalWorksNet { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rwnetcaption", modeltype: FwDataTypes.Text, maxlength: 20)]
        public string RentalWorksNetCaption { get; set; }
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
    }
}
