﻿using Oracle.ManagedDataAccess.Client;
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
    public class SalesItemSyncIntegration : IJob
    {
        const string JOB_NAME = "SalesItemSyncIntegration";
        bool verboseLogging = false;
        string rentalworksdbConnectionString = string.Empty;
        string syncdbConnectionString = string.Empty;
        FwDateTime syncDate;

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
            if (ConfigurationManager.ConnectionStrings["salesitemdb"] == null || ConfigurationManager.ConnectionStrings["salesitemdb"].ConnectionString.Length == 0)
            {
                Console.Error.WriteLine("QuartzScheduler.exe.config is missing a ConnectionString for \"salesitemdb\"");
                return;
            }
            verboseLogging = ((string)ConfigurationManager.AppSettings["verboseLogging"]).ToLower() == "true";
            rentalworksdbConnectionString = ConfigurationManager.ConnectionStrings["rentalworksdb"].ConnectionString;
            syncdbConnectionString= ConfigurationManager.ConnectionStrings["salesitemdb"].ConnectionString;
            if (verboseLogging)
            {
                Console.Out.WriteLine();
                Console.Out.WriteLine($"ConnectionStrings[rentalworksdb]: {rentalworksdbConnectionString}");
                Console.Out.WriteLine($"ConnectionStrings[salesitemdb]: {syncdbConnectionString}");
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
        public void GetSyncDate()
        {
            using (QuartzSqlCommand qry = new QuartzSqlCommand(rentalworksdbConnectionString))
            {
                qry.Add("select top 1 syncsalesitemdate");
                qry.Add("from controlsync with(nolock)");
                qry.Execute();
                syncDate = qry.GetField("syncsalesitemdate").ToFwDateTime();
            }
        }
        //---------------------------------------------------------------------------------------------
        public dynamic GetData()
        {
            dynamic result;
            Console.Error.WriteLine();
            Console.Out.WriteLine("Executing: GetData.");
            if (verboseLogging)
            {
                Console.Out.WriteLine($"  @syncdate: {syncDate}");
            }

            using (QuartzSqlCommand qry = new QuartzSqlCommand(syncdbConnectionString))
            {
                qry.Add("select *");
                qry.Add("from salesitemexportview with(nolock)");
                if (!syncDate.IsNull())
                { 
                    qry.Add("where  processdate > @syncdate");
                    qry.AddParameter("@syncdate", syncDate.GetSqlValue());
                }
                qry.Add("order by processdate");
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
                using (QuartzSqlCommand qry = new QuartzSqlCommand(rentalworksdbConnectionString, "dbo.processsyncsalesitem"))
                {
                    if (verboseLogging)
                    {
                        Console.Out.WriteLine($"  @processing sales item record i: {i}");
                    }

                    qry.AddParameter("@location", syncList[i].location);
                    qry.AddParameter("@masterno", syncList[i].masterno);
                    qry.AddParameter("@master", syncList[i].master);
                    qry.AddParameter("@category", syncList[i].category);
                    qry.AddParameter("@cost", syncList[i].cost);
                    qry.AddParameter("@price", syncList[i].price);
                    qry.AddParameter("@unit", syncList[i].unit);
                    qry.AddParameter("@nodiscount", syncList[i].nodiscount);
                    qry.AddParameter("@taxable", syncList[i].taxable);
                    qry.AddParameter("@inventorydepartment", syncList[i].inventorydepartment);
                    qry.AddParameter("@noavail", syncList[i].noavail);
                    qry.AddParameter("@restockpercent", syncList[i].restockpercent);
                    qry.AddParameter("@inactive", syncList[i].inactive);
                    qry.AddParameter("@inputdate", syncList[i].inputdate);
                    qry.AddParameter("@moddate", syncList[i].moddate);
                    qry.AddParameter("@processdate", syncList[i].processdate);
                    qry.Execute();
                    syncDate = syncList[i].processdate;
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public void SaveSyncDate()
        {
            FwDateTime asof;

            asof = FwDateTime.Now;

            Console.Error.WriteLine();
            Console.Out.WriteLine("Executing: update controlsync in rentalworks database.");
            if (verboseLogging)
            {
                Console.Out.WriteLine($"  @syncsalesitemdate: {syncDate}");
            }

            if (!syncDate.IsNull())
            {
                using (QuartzSqlCommand qry = new QuartzSqlCommand(rentalworksdbConnectionString))
                {
                    qry.Add("update controlsync");
                    qry.Add("   set syncsalesitemdate = @syncdate,");
                    qry.Add("       asof              = @asof");
                    qry.AddParameter("@syncdate", syncDate.GetSqlValue());
                    qry.AddParameter("@asof",     asof.GetSqlValue());
                    qry.Execute();
                }
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
