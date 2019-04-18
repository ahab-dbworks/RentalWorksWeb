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
        bool   verboseLogging              = false;
        string rentalworksConnectionString = string.Empty;
        string syncConnectionString        = string.Empty;

        //---------------------------------------------------------------------------------------------
        public async Task Execute(IJobExecutionContext context)
        {
            dynamic syncInvoices;

            Console.Out.WriteLine();
            Console.Out.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.Out.WriteLine("Starting InvoiceSyncIntegration");
            try
            {
                GetDatabaseConnection();
                syncInvoices = GetInvoices();
                //need to create a batch
                // dbo.createchargebatch2
                //processinvoice  

                ProcessInvoices(syncInvoices);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine();
                Console.Error.WriteLine(ex.Message + ex.StackTrace);
            }
            Console.Error.WriteLine();
            Console.Out.WriteLine("Finished InvoiceSyncIntegration");

            await Task.CompletedTask;
        }
        //---------------------------------------------------------------------------------------------
        public void GetDatabaseConnection()
        {
            // please note in development the file is in app.config
            if (ConfigurationManager.ConnectionStrings["rentalworks"] == null || ConfigurationManager.ConnectionStrings["rentalworks"].ConnectionString.Length == 0)
            {
                Console.Error.WriteLine("QuartzScheduler.exe.config is missing a ConnectionString for `rentalworks`");
                return;
            }
            if (ConfigurationManager.ConnectionStrings["Invoicedb"] == null || ConfigurationManager.ConnectionStrings["Invoicedb"].ConnectionString.Length == 0)
            {
                Console.Error.WriteLine("QuartzScheduler.exe.config is missing a ConnectionString for `Invoicedb`");
                return;
            }
            verboseLogging = ((string)ConfigurationManager.AppSettings["verboseLogging"]).ToLower() == "true";
            rentalworksConnectionString = ConfigurationManager.ConnectionStrings["rentalworks"].ConnectionString;
            syncConnectionString= ConfigurationManager.ConnectionStrings["invoicedb"].ConnectionString;
            if (verboseLogging)
            {
                Console.Out.WriteLine();
                Console.Out.WriteLine("SyncConnectionString: " + syncConnectionString);
                Console.Out.WriteLine("rentalworksConnectionString: " + rentalworksConnectionString);
            }
        }
        //---------------------------------------------------------------------------------------------
        public dynamic GetInvoices()
        {
            dynamic result;
            using (QuartzSqlCommand qry = new QuartzSqlCommand(rentalworksConnectionString))
            {
                qry.Add("select distinct invoiceid");
                qry.Add("from   syncinvoice");
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
                syncInvoice = ProcessInvoice(syncInvoices[i].invoiceId);
                ProcessData(syncInvoice);
                UpdateSyncInvoice(syncInvoices[i].invoiceId);
            }
        }
        //---------------------------------------------------------------------------------------------
        public dynamic ProcessInvoice(string invoiceId)
        {
            dynamic syncInvoice;
            using (QuartzSqlCommand qry = new QuartzSqlCommand(rentalworksConnectionString))
            {
                qry.Add("select *");
                qry.Add("from   syncinvoice");
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
                using (QuartzSqlCommand qry = new QuartzSqlCommand(syncConnectionString))
                {
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public void UpdateSyncInvoice(string invoiceId)
        {
            Console.Error.WriteLine();
            Console.Out.WriteLine("Executing: update Sync Invoice in rentalworks database.");
            if (verboseLogging)
            {
                Console.Out.WriteLine("  @invoiceid: " + invoiceId);
            }

            using (QuartzSqlCommand qry = new QuartzSqlCommand(rentalworksConnectionString))
            {
                qry.Add("update syncinvoice");
                qry.Add("   set processdate = getdate()");
                qry.Add("where @invoiceid = @invoiceid");
                qry.AddParameter("@invoiceid", invoiceId);
                qry.Execute();
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
