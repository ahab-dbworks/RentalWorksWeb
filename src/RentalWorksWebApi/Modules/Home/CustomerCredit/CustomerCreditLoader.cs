using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System.Collections.Generic;
using WebApi.Data;
using WebApi.Logic;
using WebLibrary;

namespace WebApi.Modules.Home.CustomerCredit
{
    [FwSqlTable("customerdealcreditview")]
    public class CustomerCreditLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        public CustomerCreditLoader()
        {
            AfterBrowse += OnAfterBrowse;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "arid", modeltype: FwDataTypes.Text)]
        public string ReceiptId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string OfficeLocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paymentby", modeltype: FwDataTypes.Text)]
        public string PaymentBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectypedisplay", modeltype: FwDataTypes.Text)]
        public string RecTypeDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string RecTypeColor
        {
            get { return determineRecTypeColor(RecType); }
            set { }
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "ardate", modeltype: FwDataTypes.Date)]
        public string ReceiptDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paytypeid", modeltype: FwDataTypes.Text)]
        public string PaymentTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "paytype", modeltype: FwDataTypes.Text)]
        public string PaymentType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "checkno", modeltype: FwDataTypes.Text)]
        public string CheckNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pmtamt", modeltype: FwDataTypes.Decimal)]
        public decimal? Amount { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "applied", modeltype: FwDataTypes.Decimal)]
        public decimal? Applied { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "refunded", modeltype: FwDataTypes.Decimal)]
        public decimal? Refunded { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "remaining", modeltype: FwDataTypes.Decimal)]
        public decimal? Remaining { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddWhere("paymentby = '" + RwConstants.RECEIPT_PAYMENT_BY_CUSTOMER + "'");

            addFilterToSelect("CustomerId", "customerid", select, request);
            AddActiveViewFieldToSelect("LocationId", "locationid", select, request);
            AddActiveViewFieldToSelect("RecType", "rectype", select, request);

            //Status=R (Remaining Only)
            if ((request != null) && (request.activeviewfields != null))
            {
                if (request.activeviewfields.ContainsKey("Status"))
                {
                    List<string> values = request.activeviewfields["Status"];

                    if (values.Count == 1)
                    {
                        string value = values[0];
                        if (value.ToUpper().Equals("R"))
                        {
                            select.AddWhere("(remaining <> 0)");
                        }
                    }
                }
            }

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
