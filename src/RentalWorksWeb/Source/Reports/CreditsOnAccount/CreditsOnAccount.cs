using Fw.Json.Services;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Source.Reports
{
    class CreditsOnAccount : RwReport
    {
        //---------------------------------------------------------------------------------------------
        protected override string getReportName() { return "Credits On Account"; }
        //---------------------------------------------------------------------------------------------
        protected override string renderHeaderHtml(string styletemplate, string headertemplate, FwReport.PrintOptions printOptions)
        {
            StringBuilder sb;
            string html;

            sb = new StringBuilder(base.renderHeaderHtml(styletemplate, headertemplate, printOptions));

            sb.Replace("[CURRENTDATE]", DateTime.Today.ToString("MMMM d, yyyy"));


            html = sb.ToString();

            return html;
        }
        //---------------------------------------------------------------------------------------------
        protected override string renderBodyHtml(string styletemplate, string bodytemplate, PrintOptions printOptions)
        {
            string html;
            FwJsonDataTable dtCreditsOnAccount;
            StringBuilder sb;

            dtCreditsOnAccount = GetCreditsOnAccount();            
         
            //for (int i = 0; i < dtCreditsOnAccount.Rows.Count; i++)
            //{
            //    dtCreditsOnAccount.Rows[i][dtCreditsOnAccount.ColumnIndex["remaining"]] = FwConvert.ToCurrencyStringNoDollarSign(Convert.ToDecimal(dtCreditsOnAccount.Rows[i][dtCreditsOnAccount.ColumnIndex["totaldeposit"]]) - Convert.ToDecimal(dtCreditsOnAccount.Rows[i][dtCreditsOnAccount.ColumnIndex["totalapplied"]].ToString()) - Convert.ToDecimal(dtCreditsOnAccount.Rows[i][dtCreditsOnAccount.ColumnIndex["totalrefunded"]]));
            //}

            //List<object> totalRow = dtCreditsOnAccount.Rows[dtCreditsOnAccount.Rows.Count];
            html = base.renderBodyHtml(styletemplate, bodytemplate, printOptions);
            sb = new StringBuilder(base.renderBodyHtml(styletemplate, bodytemplate, printOptions));
            sb.Replace("[TotalRows]", "Total Rows: " + dtCreditsOnAccount.Rows.Count);
            html = sb.ToString();
            html = this.applyTableToTemplate(html, "details", dtCreditsOnAccount);

            return html;
        }
        //---------------------------------------------------------------------------------------------
        protected FwJsonDataTable GetCreditsOnAccount()
        {
            FwSqlSelect select;
            FwSqlCommand qry;
            FwJsonDataTable dtDetails;// , dtTotals;
            //List<object> totalsRow;

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks, FwQueryTimeouts.Report);

            qry.AddColumn("rowtype");
            qry.AddColumn("location");
            qry.AddColumn("customer");
            qry.AddColumn("deal");

            qry.AddColumn("totaldepdep",    false, FwJsonDataTableColumn.DataTypes.Decimal);
            qry.AddColumn("totalcredit",    false, FwJsonDataTableColumn.DataTypes.Decimal);
            qry.AddColumn("totalover",      false, FwJsonDataTableColumn.DataTypes.Decimal);
            qry.AddColumn("totaldeposit",   false, FwJsonDataTableColumn.DataTypes.Decimal);
            qry.AddColumn("totalapplied",   false, FwJsonDataTableColumn.DataTypes.Decimal);
            qry.AddColumn("totalrefunded",  false, FwJsonDataTableColumn.DataTypes.Decimal);
            qry.AddColumn("remaining",      false, FwJsonDataTableColumn.DataTypes.Decimal);

            select = new FwSqlSelect();

            select.Add("select rowtype='detail', location, customer, deal, totaldepdep, totalcredit, totalover, totaldeposit, totalapplied, totalrefunded, remaining=(totaldeposit - totalapplied - totalrefunded)");
            select.Add("from creditsonaccountview");
            if (request.parameters.IncludeRemainingBalance == "T")
            {
                select.Add("where ((totaldeposit - totalapplied - totalrefunded) > 0)");
            }
            select.Add("order by location, customer, deal");


            select.Parse();
            dtDetails = qry.QueryToFwJsonTable(select, true);

            dtDetails.InsertSubTotalRows("location", "rowtype", new string[] { "totaldepdep", "totalcredit", "totalover", "totaldeposit", "totalapplied", "totalrefunded", "remaining" });
            dtDetails.InsertSubTotalRows("customer", "rowtype", new string[] { "totaldepdep", "totalcredit", "totalover", "totaldeposit", "totalapplied", "totalrefunded", "remaining" });
            dtDetails.InsertTotalRow("rowtype", "detail", "grandtotal", new string[] { "totaldepdep", "totalcredit", "totalover", "totaldeposit", "totalapplied", "totalrefunded", "remaining" });

            //qry = new FwSqlCommand(FwSqlConnection.RentalWorks, FwQueryTimeouts.Report);
            //qry.AddColumn("totaldepdep", false, FwJsonDataTableColumn.DataTypes.CurrencyStringNoDollarSign);
            //qry.AddColumn("totalcredit", false, FwJsonDataTableColumn.DataTypes.CurrencyStringNoDollarSign);
            //qry.AddColumn("totalover", false, FwJsonDataTableColumn.DataTypes.CurrencyStringNoDollarSign);
            //qry.AddColumn("totaldeposit", false, FwJsonDataTableColumn.DataTypes.CurrencyStringNoDollarSign);
            //qry.AddColumn("totalapplied", false, FwJsonDataTableColumn.DataTypes.CurrencyStringNoDollarSign);
            //qry.AddColumn("totalrefunded", false, FwJsonDataTableColumn.DataTypes.CurrencyStringNoDollarSign);

            //select = new FwSqlSelect();
            //if (request.parameters.IncludeRemainingBalance == "T")
            //{
            //    select.Add("select top 1 locationid = '',location = '',customerid = '',customer = '',dealid = '',deal = 'Grand Total:',paymentby = '', totaldepdep = sum(totaldepdep), totalcredit = sum(totalcredit), totalover = sum(totalover), totaldeposit = sum(totaldeposit), totalapplied = sum(totalapplied), totalrefunded = sum(totalrefunded), remaining = 0");
            //    select.Add("from creditsonaccountview");
            //    select.Add("where ((totaldeposit - totalapplied - totalrefunded) > 0)");
            //}
            //else
            //{
            //    select.Add("select top 1 locationid = '',location = '',customerid = '',customer = '',dealid = '',deal = 'Grand Total:',paymentby = '', totaldepdep = sum(totaldepdep), totalcredit = sum(totalcredit), totalover = sum(totalover), totaldeposit = sum(totaldeposit), totalapplied = sum(totalapplied), totalrefunded = sum(totalrefunded), remaining = 0");
            //    select.Add("from creditsonaccountview");
            //}

            //dtTotals = qry.QueryToFwJsonTable(select, true);
            //totalsRow = new List<object>();
            //if (dtTotals.Rows.Count != 1) throw new Exception("Report returned no records.");
            //for (int colNo = 0; colNo < dtTotals.Columns.Count; colNo++)
            //{
            //    totalsRow.Add(dtTotals.Rows[0][colNo]);
            //}
            //dtDetails.Rows.Add(totalsRow);

            return dtDetails;
        }
        //---------------------------------------------------------------------------------------------
        public string GetCommaListDecrypt(string encryptedlist)
        {
            string[] values;
            string result = string.Empty;

            if (!string.IsNullOrWhiteSpace(encryptedlist))
            {
                values = encryptedlist.Split(new char[] { ',' }, StringSplitOptions.None);
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = FwCryptography.AjaxDecrypt(values[i]);
                }
                result = string.Join(",", values);
            }

            return result;
        }
        //---------------------------------------------------------------------------------------------
        public override void GetData()
        {
            //switch ((string)request.method)
            //{
            //    case "LoadForm":
            //        response.statuslist  = GetStatusList();
            //        response.orderbylist = GetOrderByList();
            //        break;
            //}
        }
        //---------------------------------------------------------------------------------------------
    }
}
