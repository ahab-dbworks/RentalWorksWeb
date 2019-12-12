using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Exports.InvoiceExport
{

    public class InvoiceExportLoader : AppDataLoadRecord
    {
        protected string recType = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "invoiceid", modeltype: FwDataTypes.Text)]
        public string InvoiceId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text, isVisible: false)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectype", modeltype: FwDataTypes.Text)]
        public string RecType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rectypedisplay", modeltype: FwDataTypes.Text)]
        public string RecTypeDisplay { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qty", modeltype: FwDataTypes.Decimal)]
        public decimal? Quantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "extended", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Extended { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderby", modeltype: FwDataTypes.Text)]
        public string OrderBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rate", modeltype: FwDataTypes.CurrencyStringNoDollarSign)]
        public decimal? Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "discountpct", modeltype: FwDataTypes.DecimalString2Digits)]
        public decimal? DiscountPercent { get; set; }
        //------------------------------------------------------------------------------------ 
        //public async Task<List<T>> LoadItems<T>(InvoiceReportRequest request)
        //{
        //    FwJsonDataTable dt = null;
        //    using (FwSqlConnection conn = new FwSqlConnection(AppConfig.DatabaseSettings.ConnectionString))
        //    {
        //        using (FwSqlCommand qry = new FwSqlCommand(conn, "webgetinvoiceprintdetails", this.AppConfig.DatabaseSettings.ReportTimeout))
        //        {
        //            qry.AddParameter("@invoiceid", SqlDbType.Text, ParameterDirection.Input, request.InvoiceId);
        //            qry.AddParameter("@rectype", SqlDbType.Text, ParameterDirection.Input, recType);
        //            AddPropertiesAsQueryColumns(qry);
        //            dt = await qry.QueryToFwJsonTableAsync(false, 0);
        //        }
        //        //--------------------------------------------------------------------------------- 
        //    }
        //    dt.Columns[dt.GetColumnNo("RowType")].IsVisible = true;
        //    string[] totalFields = new string[] { "Extended" };
        //    dt.InsertSubTotalRows("RecTypeDisplay", "RowType", totalFields);
        //    dt.InsertTotalRow("RowType", "detail", "grandtotal", totalFields);

        //    List<T> items = new List<T>();
        //    foreach (List<object> row in dt.Rows)
        //    {
        //        T item = (T)Activator.CreateInstance(typeof(T));
        //        PropertyInfo[] properties = item.GetType().GetProperties();
        //        foreach (var property in properties)
        //        {
        //            string fieldName = property.Name;
        //            int columnIndex = dt.GetColumnNo(fieldName);
        //            if (!columnIndex.Equals(-1))
        //            {
        //                FwDataTypes propType = dt.Columns[columnIndex].DataType;
        //                if (AppFunc.FwDataTypeIsDecimal(propType))
        //                {
        //                    property.SetValue(item, FwConvert.ToDecimal((row[dt.GetColumnNo(fieldName)] ?? "").ToString()));
        //                }
        //                else
        //                {
        //                    property.SetValue(item, (row[dt.GetColumnNo(fieldName)] ?? "").ToString());
        //                }
        //            }
        //        }
        //        items.Add(item);
        //    }
        //    return items;
        //}
        //------------------------------------------------------------------------------------ 
    }
}
