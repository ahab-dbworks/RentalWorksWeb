using Oracle.ManagedDataAccess.Client;
using Quartz;
using QuartzScheduler.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzScheduler.Jobs
{
    public class InvoiceSyncIntegration : IJob
    {
        bool verboseLogging = false;
        string rentalworksdbConnectionString = string.Empty;
        string invoicedbConnectionString = string.Empty;
        const string JOB_NAME = "InvoiceSyncIntegration";
        //---------------------------------------------------------------------------------------------
        public async Task Execute(IJobExecutionContext context)
        {
            dynamic syncInvoices;

            Console.Out.WriteLine();
            Console.Out.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.Out.WriteLine($"Starting {JOB_NAME}");
            try
            {
                GetDatabaseConnection();
                syncInvoices = GetInvoices();
                ProcessInvoices(syncInvoices);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine();
                Console.Error.WriteLine(ex.Message + ex.StackTrace);
            }
            Console.Error.WriteLine();
            Console.Out.WriteLine($"Finished {JOB_NAME}");

            await Task.CompletedTask;
        }
        //---------------------------------------------------------------------------------------------
        public void GetDatabaseConnection()
        {
            // please note in development the file is in app.config
            if (ConfigurationManager.ConnectionStrings["rentalworksdb"] == null || ConfigurationManager.ConnectionStrings["rentalworksdb"].ConnectionString.Length == 0)
            {
                Console.Error.WriteLine("QuartzScheduler.exe.config is missing a ConnectionString for \"rentalworksdb\"");
                return;
            }
            if (ConfigurationManager.ConnectionStrings["invoicedb"] == null || ConfigurationManager.ConnectionStrings["invoicedb"].ConnectionString.Length == 0)
            {
                Console.Error.WriteLine("QuartzScheduler.exe.config is missing a ConnectionString for \"invoicedb\"");
                return;
            }
            verboseLogging = ((string)ConfigurationManager.AppSettings["verboseLogging"]).ToLower() == "true";
            rentalworksdbConnectionString = ConfigurationManager.ConnectionStrings["rentalworksdb"].ConnectionString;
            invoicedbConnectionString= ConfigurationManager.ConnectionStrings["invoicedb"].ConnectionString;
            if (verboseLogging)
            {
                Console.Out.WriteLine();
                Console.Out.WriteLine($"ConnectionString[rentalworksdb]: {rentalworksdbConnectionString}");
                Console.Out.WriteLine($"ConnectionString[invoicedb]: {invoicedbConnectionString}");
            }
        }
        //---------------------------------------------------------------------------------------------
        public dynamic GetInvoices()
        {
            dynamic result;
            using (QuartzSqlCommand qry = new QuartzSqlCommand(rentalworksdbConnectionString))
            {
                qry.Add("select distinct invoiceid");
                qry.Add("from   syncinvoiceview with (nolock)");
                qry.Add("where  processdate is null");
                result = qry.QueryToDynamicList();
            }
            return result;
        }
        //---------------------------------------------------------------------------------------------
        public void ProcessInvoices(dynamic syncInvoices)
        {
            dynamic syncInvoice;
            for (int i = 0; i < syncInvoices.Count; i++)
            {
                if (verboseLogging)
                {
                    Console.Out.WriteLine($"  @processing invoice record id: {syncInvoices[i].invoiceid}");
                }
                syncInvoice = ProcessInvoice(syncInvoices[i].invoiceid);
                ProcessData(syncInvoice);
                UpdateSyncInvoice(syncInvoices[i].invoiceid);
            }
        }
        //---------------------------------------------------------------------------------------------
        public dynamic ProcessInvoice(string invoiceId)
        {
            dynamic syncInvoice;
            using (QuartzSqlCommand qry = new QuartzSqlCommand(rentalworksdbConnectionString))
            {
                qry.Add("select *");
                qry.Add("from   syncinvoiceview with (nolock)");
                qry.Add("where  invoiceid = @invoiceid");
                qry.AddParameter("@invoiceid", invoiceId);
                qry.Add("order by syncinvoiceid");
                syncInvoice  = qry.QueryToDynamicList();
            }
            return syncInvoice;
        }
        //---------------------------------------------------------------------------------------------
        public void ProcessData(dynamic syncInvoice)
        {
            for (int i = 0; i < syncInvoice.Count; i++)
            {
                using (QuartzSqlCommand qry = new QuartzSqlCommand(invoicedbConnectionString, "dbo.processinvoicetransaction"))
                {
                    if (verboseLogging)
                    {
                        Console.Out.WriteLine($"  @processing invoice line item i: {i}");
                    }

                    try
                    {
                        qry.AddParameter("@syncinvoiceid", syncInvoice[i].syncinvoiceid.ToString().Trim());
                        qry.AddParameter("@invoicetype", syncInvoice[i].invoicetype.ToString().Trim());
                        qry.AddParameter("@invoiceno", syncInvoice[i].invoiceno.ToString().Trim());
                        qry.AddParameter("@custno", syncInvoice[i].custno.ToString().Trim());
                        qry.AddParameter("@invoicedate", syncInvoice[i].invoicedate);
                        qry.AddParameter("@locationcode", syncInvoice[i].locationcode.ToString().Trim());
                        qry.AddParameter("@location", syncInvoice[i].location.ToString().Trim());
                        qry.AddParameter("@warehouse", syncInvoice[i].warehouse.ToString().Trim());
                        qry.AddParameter("@currencycode", syncInvoice[i].currencycode.ToString().Trim());
                        qry.AddParameter("@invoiceitemid", syncInvoice[i].invoiceitemid.ToString().Trim());
                        qry.AddParameter("@masterno", syncInvoice[i].masterno.ToString().Trim());
                        qry.AddParameter("@description", syncInvoice[i].description.ToString().Trim());
                        qry.AddParameter("@unit", syncInvoice[i].unit.ToString().Trim());
                        qry.AddParameter("@qty", syncInvoice[i].qty);
                        qry.AddParameter("@cost", syncInvoice[i].cost);
                        qry.AddParameter("@rate", syncInvoice[i].rate);
                        qry.AddParameter("@revenuebase", syncInvoice[i].revenuebase);
                        qry.AddParameter("@costbase", syncInvoice[i].costbase);
                        qry.AddParameter("@taxable", syncInvoice[i].taxable.ToString().Trim());
                        qry.AddParameter("@total", syncInvoice[i].total);
                        qry.AddParameter("@discountamt", syncInvoice[i].discountamt);
                        qry.Execute();

                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine(ex.Message + ex.StackTrace);
                    }
                }

            }
        }
        //---------------------------------------------------------------------------------------------
        public void UpdateSyncInvoice(string invoiceId)
        {
            FwDateTime asof;
            asof = FwDateTime.Now;

            Console.Error.WriteLine();
            Console.Out.WriteLine("Executing: update Sync Invoice in rentalworks database.");
            if (verboseLogging)
            {
                Console.Out.WriteLine($"  @invoiceid: {invoiceId}");
            }

            using (QuartzSqlCommand qry = new QuartzSqlCommand(rentalworksdbConnectionString, "dbo.processupdatesyncinvoice"))
            {
                qry.AddParameter("@invoiceid", invoiceId);
                qry.Execute();
            }

            using (QuartzSqlCommand qry = new QuartzSqlCommand(rentalworksdbConnectionString))
            {
                qry.Add("update controlsync");
                qry.Add("set    asof = @asof");
                qry.AddParameter("@asof", asof.GetSqlValue());
                qry.Execute();
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
