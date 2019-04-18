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
        bool   verboseLogging              = false;
        string rentalworksConnectionString = string.Empty;
        string syncConnectionString        = string.Empty;
        DateTime syncDate;

        //---------------------------------------------------------------------------------------------
        public async Task Execute(IJobExecutionContext context)
        {
            dynamic synclist;

            Console.Out.WriteLine();
            Console.Out.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.Out.WriteLine("Starting CustomerSyncIntegration");
            try
            {
                GetDatabaseConnection();
                DefaultControlSync();
                GetSyncDate();
                synclist = GetData();
                ProcessData(synclist);
                SaveSyncDate();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine();
                Console.Error.WriteLine(ex.Message + ex.StackTrace);
            }
            Console.Error.WriteLine();
            Console.Out.WriteLine("Finished CustomerSyncIntegration");

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
            if (ConfigurationManager.ConnectionStrings["customerdb"] == null || ConfigurationManager.ConnectionStrings["customerdb"].ConnectionString.Length == 0)
            {
                Console.Error.WriteLine("QuartzScheduler.exe.config is missing a ConnectionString for `customerdb`");
                return;
            }
            verboseLogging = ((string)ConfigurationManager.AppSettings["verboseLogging"]).ToLower() == "true";
            rentalworksConnectionString = ConfigurationManager.ConnectionStrings["rentalworks"].ConnectionString;
            syncConnectionString= ConfigurationManager.ConnectionStrings["customerdb"].ConnectionString;
            if (verboseLogging)
            {
                Console.Out.WriteLine();
                Console.Out.WriteLine("SyncConnectionString: " + syncConnectionString);
                Console.Out.WriteLine("rentalworksConnectionString: " + rentalworksConnectionString);
            }
        }
        //---------------------------------------------------------------------------------------------
        public void DefaultControlSync()
        {
            using (QuartzSqlCommand qry = new QuartzSqlCommand(rentalworksConnectionString, "dbo.defaultcontrolsync"))
            {
                qry.Execute();
            }
        }
        //---------------------------------------------------------------------------------------------
        public void GetSyncDate()
        {
            using (QuartzSqlCommand qry = new QuartzSqlCommand(rentalworksConnectionString))
            {
                qry.Add("select top 1 synccustomerdate");
                qry.Add("from   controlsync");
                qry.Execute();
                syncDate = qry.GetField("synccustomerdate").ToDateTime();
                //default controlsync table
            }
        }
        //---------------------------------------------------------------------------------------------
        public dynamic GetData()
        {
            dynamic result;
            using (QuartzSqlCommand qry = new QuartzSqlCommand(syncConnectionString))
            {
                qry.Add("select *");
                qry.Add("from   customerexportview");
                if (syncDate != null)
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
        public void ProcessData(dynamic syncList)
        {
            for (int i = 0; i < syncList.Count; i++)
            {
                using (QuartzSqlCommand qry = new QuartzSqlCommand(rentalworksConnectionString, "dbo.processsynccustomer"))
                {
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
                    qry.AddParameter("billtoatt", syncList[i].billtoadd);
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
                }
            }
            syncDate = syncList[syncList.Count-1].processdate;
        }
        //---------------------------------------------------------------------------------------------
        public void SaveSyncDate()
        {
            Console.Error.WriteLine();
            Console.Out.WriteLine("Executing: update controlsync in rentalworks database.");
            if (verboseLogging)
            {
                Console.Out.WriteLine("  @synccustomerdate: " + syncDate);
            }

            using (QuartzSqlCommand qry = new QuartzSqlCommand(rentalworksConnectionString))
            {
                qry.Add("update controlsync");
                qry.Add("   set synccustomerdate=@synccustomerdate");
                qry.AddParameter("@synccustomerdate", syncDate);
                qry.Execute();
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
