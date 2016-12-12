using Fw.Json.SqlServer;
using RentalWorksAPI.api.v1.Models;
using System.Collections.Generic;
using System.Data;

namespace RentalWorksAPI.api.v1.Data
{
    public class InvoiceData
    {
        //----------------------------------------------------------------------------------------------------
        public static List<Invoice> GetInvoicesFrom(string asofdate)
        {
            FwSqlCommand qry;
            List<Invoice> result = new List<Invoice>();
            DataTable dt = new DataTable();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select *");
            qry.Add("  from api_invoiceview");
            qry.Add("where datestamp > @asofdate");
            qry.Add("order by datestamp");
            qry.AddParameter("@asofdate", asofdate);
            dt = qry.QueryToTable();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Invoice invoice = new Invoice();

                invoice.invoiceid   = dt.Rows[i]["invoiceid"].ToString().TrimEnd();
                invoice.invoiceno   = dt.Rows[i]["invoiceno"].ToString().TrimEnd();
                invoice.rentaltotal = dt.Rows[i]["rentaltotal"].ToString().TrimEnd();
                invoice.datestamp   = dt.Rows[i]["datestamp"].ToString().TrimEnd();

                invoice.deal          = new Deal();
                invoice.deal.dealid   = dt.Rows[i]["dealid"].ToString().TrimEnd();
                invoice.deal.dealname = dt.Rows[i]["deal"].ToString().TrimEnd();

                result.Add(invoice);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
    }
}