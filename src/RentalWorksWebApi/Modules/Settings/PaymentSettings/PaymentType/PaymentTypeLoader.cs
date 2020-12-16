using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;

namespace WebApi.Data.Settings.PaymentSettings.PaymentType
{
    [FwSqlTable("paytypeview")]
    public class PaymentTypeLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "paytypeid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string PaymentTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "paytype", modeltype: FwDataTypes.Text, required: true)]
        public string PaymentType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "short", modeltype: FwDataTypes.Text, required: true)]
        public string ShortName { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "pmttype", modeltype: FwDataTypes.Text, required: true)]
        public string PaymentTypeType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "glaccountid", modeltype: FwDataTypes.Text)]
        public string GlAccountId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "glno", modeltype: FwDataTypes.Text)]
        public string GlAccountNo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "glacctdesc", modeltype: FwDataTypes.Text)]
        public string GlAccountDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "accttran", modeltype: FwDataTypes.Boolean)]
        public bool? AccountingTransaction { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "exportpaymentmethod", modeltype: FwDataTypes.Text)]
        public string ExportPaymentMethod { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "exportpaymenttype", modeltype: FwDataTypes.Text)]
        public string ExportPaymentType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "includeinrwnet", modeltype: FwDataTypes.Boolean)]
        public bool? IncludeInRentalWorksNet { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rwnetcaption", modeltype: FwDataTypes.Text)]
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
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            this.AddFilterFieldToSelect("PaymentTypeType", "pmttype", select, request);
        }
        //------------------------------------------------------------------------------------
    }
}
