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
    public class SalesItemTransactionSyncIntegration : IJob
    {
        const string JOB_NAME = "SalesItemTransactionSyncIntegration";
        bool verboseLogging = false;
        string rentalworksdbConnectionString = string.Empty;
        string syncdbConnectionString = string.Empty;

        //---------------------------------------------------------------------------------------------
        public async Task Execute(IJobExecutionContext context)
        {
            dynamic synclist;

            try
            {
                Console.Out.WriteLine();
                Console.Out.WriteLine("-----------------------------------------------------------------------------------------------");
                Console.Out.WriteLine($"Starting {JOB_NAME}");

                GetDatabaseConnection();
                synclist = GetData();
                ProcessData(synclist);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine();
                Console.Error.WriteLine(ex.Message + ex.StackTrace);
            }
            finally
            {
                Console.Error.WriteLine();
                Console.Out.WriteLine($"Finished {JOB_NAME}");
            }

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
            if (ConfigurationManager.ConnectionStrings["salesitemtransactiondb"] == null || ConfigurationManager.ConnectionStrings["salesitemtransactiondb"].ConnectionString.Length == 0)
            {
                Console.Error.WriteLine("QuartzScheduler.exe.config is missing a ConnectionString for \"salesitemdb\"");
                return;
            }
            verboseLogging = ((string)ConfigurationManager.AppSettings["verboseLogging"]).ToLower() == "true";
            rentalworksdbConnectionString = ConfigurationManager.ConnectionStrings["rentalworksdb"].ConnectionString;
            syncdbConnectionString= ConfigurationManager.ConnectionStrings["salesitemtransactiondb"].ConnectionString;
            if (verboseLogging)
            {
                Console.Out.WriteLine();
                Console.Out.WriteLine($"ConnectionStrings[rentalworksdb]: {rentalworksdbConnectionString}");
                Console.Out.WriteLine($"ConnectionStrings[salesitemtransactiondb]: {syncdbConnectionString}");
            }
        }
        //---------------------------------------------------------------------------------------------
        public dynamic GetData()
        {
            dynamic result;
            Console.Error.WriteLine();
            Console.Out.WriteLine("Executing: GetData.");
            
            using (QuartzSqlCommand qry = new QuartzSqlCommand(syncdbConnectionString))
            {
                qry.Add("select *");
                qry.Add("from salesitemtranexportview with(nolock)");
                qry.Add("order by id");
                result = qry.QueryToDynamicList();
            }
            return result;
        }
        //---------------------------------------------------------------------------------------------
        public void ProcessData(dynamic syncList)
        {
            if (verboseLogging)
            {
                Console.Out.WriteLine($"  " + JOB_NAME + " @count: {syncList.Count}");
            }
            for (int i = 0; i < syncList.Count; i++)
            {
                using (QuartzSqlCommand qry = new QuartzSqlCommand(rentalworksdbConnectionString, "dbo.processsyncsalesitemtran"))
                {
                    if (verboseLogging)
                    {
                        Console.Out.WriteLine($"  @processing sales item trans record i: {i}");
                    }

                    try
                    {
                        qry.AddParameter("@location", syncList[i].location);
                        qry.AddParameter("@masterno", syncList[i].masterno);
                        qry.AddParameter("@transactiondate", syncList[i].transactiondate);
                        qry.AddParameter("@quantity", syncList[i].quantity);
                        qry.Execute();
                        SaveProcessDate(syncList[i].id);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine(ex.Message + ex.StackTrace);
                    }
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public void SaveProcessDate(int id)
        {
            Console.Error.WriteLine();
            Console.Out.WriteLine("Executing: SaveProcessDate.");
            if (verboseLogging)
            {
                Console.Out.WriteLine($"  @id: {id}");
            }

            using (QuartzSqlCommand qry = new QuartzSqlCommand(syncdbConnectionString, "dbo.processupdatesalestransdate"))
            {
                qry.AddParameter("@id", id);
                qry.Execute();
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
