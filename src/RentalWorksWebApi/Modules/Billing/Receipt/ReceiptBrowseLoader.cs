using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Collections.Generic;
using WebApi.Data;
using WebApi.Logic;

namespace WebApi.Modules.Billing.Receipt
{
    [FwSqlTable("arwebview")]
    public class ReceiptBrowseLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        public ReceiptBrowseLoader()
        {
            AfterBrowse += OnAfterBrowse;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "arid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string ReceiptId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ardate", modeltype: FwDataTypes.Date)]
        public string ReceiptDate { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "customerdeal", modeltype: FwDataTypes.Text)]
        public string CustomerDeal { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "checkno", modeltype: FwDataTypes.Text)]
        public string CheckNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "loccode", modeltype: FwDataTypes.Text)]
        public string LocationCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "paytype", modeltype: FwDataTypes.Text)]
        public string PaymentType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "appliedby", modeltype: FwDataTypes.Text)]
        public string AppliedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pmtmemo", modeltype: FwDataTypes.Text)]
        public string PaymentMemo { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "pmtamt", modeltype: FwDataTypes.Decimal)]
        public decimal? PaymentAmount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "chgbatchno", modeltype: FwDataTypes.Text)]
        public string ChargeBatchNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencyid", modeltype: FwDataTypes.Text)]
        public string CurrencyId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencycode", modeltype: FwDataTypes.Text)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currencysymbol", modeltype: FwDataTypes.Text)]
        public string CurrencySymbol { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string RecTypeColor
        {
            get { return determineRecTypeColor(RecType); }
            set { }
        }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("DealId", "dealid", select, request);
            //addFilterToSelect("CustomerId", "customerid", select, request);

            string customerId = GetUniqueIdAsString("CustomerId", request) ?? "";

            if (!string.IsNullOrEmpty(customerId))
            {
                select.AddWhere("exists (select * from deal d where d.dealid = " + TableAlias + ".dealid and d.customerid = @customerid)");
                select.AddParameter("@customerid", customerId);
            }

            AddActiveViewFieldToSelect("LocationId", "locationid", select, request);

        }
        //------------------------------------------------------------------------------------ 
        private string determineRecTypeColor(string recType)
        {
            return AppFunc.GetReceiptRecTypeColor(recType);
        }
        //------------------------------------------------------------------------------------    
        public void OnAfterBrowse(object sender, AfterBrowseEventArgs e)
        {
            if (e.DataTable != null)
            {
                FwJsonDataTable dt = e.DataTable;
                if (dt.Rows.Count > 0)
                {
                    foreach (List<object> row in dt.Rows)
                    {
                        row[dt.GetColumnNo("RecTypeColor")] = determineRecTypeColor(row[dt.GetColumnNo("RecType")].ToString());
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------    
    }
}
