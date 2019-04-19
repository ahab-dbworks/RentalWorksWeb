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
    public class CustomerSyncIntegration : IJob
    {
        const string JOB_NAME = "CustomerSyncIntegration";
        bool verboseLogging = false;
        string rentalworksdbConnectionString = string.Empty;
        string customerdbConnectionString = string.Empty;

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
                DefaultControlSync();
                DateTime syncDate = GetSyncDate();
                synclist = GetData(syncDate);
                ProcessData(syncDate, synclist);
                SaveSyncDate(syncDate);
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
            if (ConfigurationManager.ConnectionStrings["customerdb"] == null || ConfigurationManager.ConnectionStrings["customerdb"].ConnectionString.Length == 0)
            {
                Console.Error.WriteLine("QuartzScheduler.exe.config is missing a ConnectionString for \"customerdb\"");
                return;
            }
            verboseLogging = ((string)ConfigurationManager.AppSettings["verboseLogging"]).ToLower() == "true";
            rentalworksdbConnectionString = ConfigurationManager.ConnectionStrings["rentalworksdb"].ConnectionString;
            customerdbConnectionString= ConfigurationManager.ConnectionStrings["customerdb"].ConnectionString;
            if (verboseLogging)
            {
                Console.Out.WriteLine();
                Console.Out.WriteLine($"ConnectionStrings[rentalworksdb]: {rentalworksdbConnectionString}");
                Console.Out.WriteLine($"ConnectionStrings[customerdb]: {customerdbConnectionString}");
            }
        }
        //---------------------------------------------------------------------------------------------
        public void DefaultControlSync()
        {
            using (QuartzSqlCommand qry = new QuartzSqlCommand(rentalworksdbConnectionString, "dbo.defaultcontrolsync"))
            {
                qry.Execute();
            }
        }
        //---------------------------------------------------------------------------------------------
        public DateTime GetSyncDate()
        {
            using (QuartzSqlCommand qry = new QuartzSqlCommand(rentalworksdbConnectionString))
            {
                qry.Add("select top 1 synccustomerdate");
                qry.Add("from controlsync with(nolock)");
                qry.Execute();
                DateTime syncDate = qry.GetField("synccustomerdate").ToDateTime();
                return syncDate;
            }
        }
        //---------------------------------------------------------------------------------------------
        public dynamic GetData(DateTime syncDate)
        {
            dynamic result;
            Console.Error.WriteLine();
            Console.Out.WriteLine("Executing: GetData.");
            if (verboseLogging)
            {
                Console.Out.WriteLine($"  @synccustomerdate: {syncDate}");
            }

            using (QuartzSqlCommand qry = new QuartzSqlCommand(customerdbConnectionString))
            {
                qry.Add("select top 1000 *");
                qry.Add("from customerexportview with(nolock)");
                if (syncDate != DateTime.MinValue)
                { 
                    qry.Add("where  processdate >= @synccustomerdate");
                    qry.AddParameter("@synccustomerdate", syncDate);
                }
                qry.Add("order by processdate");
                result = qry.QueryToDynamicList();
            }
            return result;
        }
        //---------------------------------------------------------------------------------------------
        public void ProcessData(FwDateTime syncDate, dynamic syncList)
        {
            if (verboseLogging)
            {
                Console.Out.WriteLine($"  @customer count: {syncList.Count}");
            }
            for (int i = 0; i < syncList.Count; i++)
            {
                using (QuartzSqlCommand qry = new QuartzSqlCommand(rentalworksdbConnectionString, "dbo.processsynccustomer"))
                {
                    if (verboseLogging)
                    {
                        Console.Out.WriteLine($"  @processing record i: {i}");
                    }

                    qry.AddParameter("@companydivision", syncList[i].companydivision);
                    qry.AddParameter("custno", syncList[i].custno);
                    qry.AddParameter("custstatus", syncList[i].custstatus);
                    qry.AddParameter("location", syncList[i].location);
                    qry.AddParameter("custcat", syncList[i].custcat);
                    qry.AddParameter("customer", syncList[i].customer);
                    qry.AddParameter("add1", syncList[i].add1);
                    qry.AddParameter("add2", syncList[i].add2);
                    qry.AddParameter("city", syncList[i].city);
                    qry.AddParameter("state", syncList[i].state);
                    qry.AddParameter("zip", syncList[i].zip);
                    qry.AddParameter("phone", syncList[i].phone);
                    qry.AddParameter("faxno", syncList[i].faxno);
                    qry.AddParameter("payterms", syncList[i].payterms);
                    qry.AddParameter("taxoption", syncList[i].taxoption);
                    qry.AddParameter("billtoatt", syncList[i].billtoatt);
                    qry.AddParameter("termsandconditiononfile", syncList[i].termsandconditiononfile);
                    qry.AddParameter("certonins", syncList[i].certonins);
                    qry.AddParameter("insurancecompany", syncList[i].insurancecompany);
                    qry.AddParameter("inscompagent", syncList[i].inscompagent);
                    qry.AddParameter("invalidthru", syncList[i].invalidthru);
                    qry.AddParameter("inscovpropvalue", syncList[i].inscovpropvalue);
                    qry.AddParameter("customeremail", syncList[i].customeremail);
                    qry.AddParameter("billmethod", syncList[i].billmethod);
                    qry.AddParameter("creditstatus", syncList[i].creditstatus);
                    qry.AddParameter("inputdate", syncList[i].inputdate);
                    qry.AddParameter("moddate", syncList[i].moddate);
                    qry.AddParameter("processdate", syncList[i].processdate);
                    qry.Execute();
                    syncDate = syncList[i].processdate;
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public void SaveSyncDate(FwDateTime syncDate)
        {
            Console.Error.WriteLine();
            Console.Out.WriteLine("Executing: update controlsync in rentalworks database.");
            if (verboseLogging)
            {
                Console.Out.WriteLine($"  @synccustomerdate: {syncDate}");
            }

            if (syncDate != DateTime.MinValue)
            {
                using (QuartzSqlCommand qry = new QuartzSqlCommand(rentalworksdbConnectionString))
                {
                    qry.Add("update controlsync");
                    qry.Add("set synccustomerdate = @synccustomerdate");
                    qry.AddParameter("@synccustomerdate", syncDate);
                    qry.Execute();
                }
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
