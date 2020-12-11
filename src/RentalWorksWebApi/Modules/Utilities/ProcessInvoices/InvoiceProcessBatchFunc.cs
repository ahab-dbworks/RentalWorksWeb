using FwCore.Controllers;
using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi;
using WebApi.Modules.Settings.FiscalYear;
using WebApi.Modules.Billing.Receipt;
using System.Collections.Generic;
using FwStandard.BusinessLogic;

namespace WebApi.Modules.Utilities.InvoiceProcessBatch
{
    public static class InvoiceProcessBatchFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<InvoiceProcessBatchResponse> CreateBatch(FwApplicationConfig appConfig, FwUserSession userSession, InvoiceProcessBatchRequest request)
        {
            string batchId = "";
            InvoiceProcessBatchResponse response = new InvoiceProcessBatchResponse();

            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, "createchargebatch2", appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddParameter("@sessionid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                    qry.AddParameter("@usersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                    qry.AddParameter("@asof", SqlDbType.DateTime, ParameterDirection.Input, request.AsOfDate);
                    qry.AddParameter("@locationid", SqlDbType.NVarChar, ParameterDirection.Input, request.LocationId);
                    qry.AddParameter("@chgbatchid", SqlDbType.NVarChar, ParameterDirection.Output);
                    qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                    qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                    await qry.ExecuteNonQueryAsync();
                    batchId = qry.GetParameter("@chgbatchid").ToString();
                    response.success = (qry.GetParameter("@status").ToInt32() == 0);
                    response.msg = qry.GetParameter("@msg").ToString();
                }
            }
            if (!string.IsNullOrEmpty(batchId))
            {
                response.Batch = new InvoiceProcessBatchLogic();
                response.Batch.SetDependencies(appConfig, userSession);
                response.Batch.BatchId = batchId;
                await response.Batch.LoadAsync<InvoiceProcessBatchLogic>();
            }
            if (response.success)
            {
                await InvoiceProcessBatchFunc.AutoProcessDepletingDeposit(appConfig, userSession, request, response);
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task AutoProcessDepletingDeposit(FwApplicationConfig appConfig, FwUserSession userSession, InvoiceProcessBatchRequest request, InvoiceProcessBatchResponse response)
        {
            response.AutoProcessDepeletingDeposit = true;
            bool autoapplydepletingdeposittoinvoice = false;
            string invoiceId = string.Empty;

            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select top 1 autoapplydepletingdeposittoinvoice");//syscontrol
                    qry.Add("from location with (nolock)");
                    qry.Add("where locationid = @locationid");
                    qry.AddParameter("@locationid", request.LocationId);
                    await qry.ExecuteAsync();
                    autoapplydepletingdeposittoinvoice = qry.GetField("autoapplydepletingdeposittoinvoice").ToBoolean();
                }
                if (autoapplydepletingdeposittoinvoice)
                {
                    using (FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                    {
                        qry.Add("select invoiceid");
                        qry.Add("from   invoicechgbatch with (nolock)");
                        qry.Add("where  chgbatchid = @chgbatchid");
                        qry.AddParameter("@chgbatchid", response.Batch.BatchId);
                        var dt = await qry.QueryToFwJsonTableAsync();
                        for (int x = 0; x < dt.Rows.Count; x++)
                        {
                            invoiceId = dt.GetValue(x, "invoiceid").ToString();
                            await InvoiceProcessBatchFunc.ApplyAutoProcessDepletingDeposit(appConfig, userSession, request, response, invoiceId, conn);

                        }
                    }
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task ApplyAutoProcessDepletingDeposit(FwApplicationConfig appConfig, FwUserSession userSession, InvoiceProcessBatchRequest request, InvoiceProcessBatchResponse response, string invoiceId, FwSqlConnection conn)
        {
            string orderId = string.Empty;
            string dealdepositId = string.Empty;
            string currencyId = string.Empty;
            decimal pmtAmt = 0.0m;
            string paymentTypeId = string.Empty;
            string dealId = string.Empty;
            string locationId = string.Empty;
            decimal applyAmount = 0.0m;
            decimal remaining = 0.0m;
            FwJsonDataTable orderInvoice;
            decimal orderInvoiceTotal = 0.0m;

            using (FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
            {
                qry.Add("select orderid, orderinvoicetotal"); 
                qry.Add("from   orderinvoice with (nolock)");
                qry.Add("where  invoiceid = @invoiceid");
                qry.AddParameter("@invoiceid", invoiceId);
                orderInvoice = await qry.QueryToFwJsonTableAsync();
                for (int i = 0; i < orderInvoice.Rows.Count; i++)
                {
                    orderId = orderInvoice.GetValue(i, "orderid").ToString().TrimEnd();
                    orderInvoiceTotal = orderInvoice.GetValue(i, "orderinvoicetotal").ToDecimal();


                    using (FwSqlCommand qryar = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                    {
                        //what if more than one deposit?
                        qryar.Add("select top 1 dealdepositid = arid, currencyid, pmtamt, locationid,             ");
                        qryar.Add("             remaining = ar.pmtamt - isnull((select sum(a.applied)             ");
                        qryar.Add("                                             from   ardepositpmt a             ");
                        qryar.Add("                                             where  a.depositid = ar.arid), 0) ");
                        qryar.Add("from   ar with (nolock)");
                        qryar.Add("where  orderid = @orderid");
                        qryar.AddParameter("@orderid", orderId);
                        qryar.AddParameter("@rectype", "D");
                        await qryar.ExecuteAsync();
                        dealdepositId = qryar.GetField("dealdepositid").ToString().TrimEnd();
                        currencyId = qryar.GetField("currencyid").ToString().TrimEnd();
                        pmtAmt = qryar.GetField("pmtamt").ToDecimal();
                        remaining = qryar.GetField("remaining").ToDecimal();  
                        locationId = qryar.GetField("locationid").ToString().TrimEnd();
                        applyAmount = Math.Min(orderInvoiceTotal, remaining);
                        paymentTypeId = (await FwSqlCommand.GetDataAsync(conn, appConfig.DatabaseSettings.QueryTimeout, "paytype", "pmttype", RwConstants.PAYMENT_TYPE_DEPLETING_DEPOSIT, "paytypeid")).ToString();
                        dealId = (await FwSqlCommand.GetDataAsync(conn, appConfig.DatabaseSettings.QueryTimeout, "dealorder", "orderid", orderId, "dealid")).ToString();

                        ReceiptLogic receipt = FwBusinessLogic.CreateBusinessLogic<ReceiptLogic>(appConfig, userSession);
                        receipt.OrderId = orderId;
                        receipt.AppliedById = userSession.UsersId;
                        receipt.ChargeBatchId = string.Empty;
                        receipt.CheckNumber = "";
                        receipt.CreateDepletingDeposit = false;
                        receipt.CreateOverpayment = false;
                        receipt.CurrencyId = currencyId;
                        receipt.CustomerDepositCheckNumber = string.Empty;
                        receipt.CustomerDepositId = string.Empty;
                        receipt.CustomerId = string.Empty;
                        receipt.DealDepositCheckNumber = string.Empty;
                        receipt.DealDepositId = dealdepositId;
                        receipt.DealId = dealId;
                        receipt.LocationId = locationId;
                        receipt.PaymentAmount = applyAmount;
                        receipt.PaymentBy = "DEAL";
                        receipt.PaymentMemo = string.Empty;
                        receipt.PaymentTypeId = paymentTypeId;
                        receipt.PaymentTypeType = "";
                        receipt.RecType = "P";
                        receipt.ReceiptDate = FwConvert.ToShortDate(DateTime.Now);
                        receipt.ReceiptId = string.Empty;
                        receipt.ModifiedById = userSession.UsersId;

                        ReceiptInvoice receiptInvoice = new ReceiptInvoice();
                        receiptInvoice.InvoiceReceiptId = "";
                        receiptInvoice.InvoiceId = invoiceId;
                        receiptInvoice.Amount = applyAmount; 

                        receipt.InvoiceDataList = new List<ReceiptInvoice>();
                        receipt.InvoiceDataList.Add(receiptInvoice);

                        await receipt.SaveAsync(null, null, TDataRecordSaveMode.smInsert);

                    }
                }
            }
        }
        //-------------------------------------------------------------------------------------------------------
        //public static async Task<ExportInvoiceResponse> Export(FwApplicationConfig appConfig, FwUserSession userSession, InvoiceProcessBatchLogic batch)
        //{
        //    FwJsonDataTable dt = null;
        //    FwJsonDataTable exportFields = null;
        //    //FwJsonDataTable exportSettings = null;
        //    //string exportString = "";
        //    StringBuilder sb = new StringBuilder();
        //    ExportInvoiceResponse response = new ExportInvoiceResponse();
        //    using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
        //    {
        //        //using (FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
        //        //{
        //        //    qry.Add("select exportstring from dataexportsetting where settingname = 'DEAL INVOICE G/L SUMMARY'");
        //        //    exportSettings = await qry.QueryToFwJsonTableAsync(true, 0);
        //        //    exportString = exportSettings.Rows[0][0].ToString();
        //        //}
        //
        //        //this is just a simpler way to grab a single field from a single table
        //        string exportString = await AppFunc.GetStringDataAsync(appConfig, "dataexportsetting", "settingname", RwConstants.DATA_EXPORT_SETTINGS_TYPE_DEAL_INVOICE_GL_SUMMARY, "exportstring");  //hard-coded for 4Wall
        //
        //
        //        using (FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
        //        {
        //            qry.Add("select * from dataexportfield");
        //            exportFields = await qry.QueryToFwJsonTableAsync(true, 0);
        //        }
        //
        //        using (FwSqlCommand qry = new FwSqlCommand(conn, "exportinvoices", appConfig.DatabaseSettings.QueryTimeout))
        //        {
        //            qry.AddParameter("@chgbatchid", SqlDbType.NVarChar, ParameterDirection.Input, batch.BatchId);
        //            qry.AddColumn("invoiceid", "invoiceid", FwDataTypes.Text, true, false, false);
        //            dt = await qry.QueryToFwJsonTableAsync(false, 0);
        //        }
        //
        //        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        //        {
        //            FwJsonDataTable dt2 = null;
        //            using (FwSqlCommand qry = new FwSqlCommand(conn, "exportinvoiceglsummary", appConfig.DatabaseSettings.QueryTimeout))
        //            {
        //                qry.AddParameter("@invoiceid", SqlDbType.NVarChar, ParameterDirection.Input, dt.Rows[i][0]);
        //                dt2 = await qry.QueryToFwJsonTableAsync(true, 0);
        //                string lineText = string.Empty;
        //                for (int j = 0; j <= dt2.Rows.Count - 1; j++)
        //                {
        //                    lineText = exportString;
        //                    char[] delimiterChars = { ' ', ',' };
        //                    string[] fields = lineText.Split(delimiterChars);
        //                    string fieldName = "";
        //                    foreach (string s in fields)
        //                    {
        //                        if (s.StartsWith('@'))
        //                        {
        //                            for (int k = 0; k <= exportFields.Rows.Count; k++)
        //                            {
        //                                if (exportFields.Rows[k][7].ToString().Equals(s))
        //                                {
        //                                    fieldName = exportFields.Rows[k][4].ToString();
        //                                    int fieldIndex = dt2.ColumnIndex[fieldName];
        //                                    string fieldValue = dt2.Rows[j][fieldIndex].ToString();
        //
        //                                    //below is how we could maybe get the dates formatting correctly, but it's not working because "DataType" is currently always Text
        //                                    //string fieldValue = "";
        //                                    //if (dt2.Columns[fieldIndex].DataType.Equals(FwDataTypes.Date))
        //                                    //{
        //                                    //    fieldValue = FwConvert.ToUSShortDate(FwConvert.ToDateTime(dt2.Rows[j][fieldIndex].ToString()));
        //                                    //}
        //                                    //else
        //                                    //{
        //                                    //    fieldValue = dt2.Rows[j][fieldIndex].ToString();
        //                                    //}
        //
        //                                    lineText = lineText.Replace(s, fieldValue);
        //                                    break;
        //                                }
        //                            }
        //                        }
        //                    }
        //                    sb.AppendLine(lineText);
        //                }
        //
        //            }
        //        }
        //        //string path = @"C:\TEMP\Batch" + batch.BatchNumber + ".csv";
        //
        //        // here we are creating a downloadable file that will live in the API "downloads" directory
        //        string downloadFileName = batch.BatchNumber + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".csv";
        //        string filename = userSession.WebUsersId + "_" + batch.BatchNumber + "_" + Guid.NewGuid().ToString().Replace("-", string.Empty) + "_csv";
        //        string directory = FwDownloadController.GetDownloadsDirectory();
        //        string path = Path.Combine(directory, filename);
        //
        //        using (var tw = new StreamWriter(path, /*true */false)) // false here will initialize the file fresh, no appending
        //        {
        //            tw.Write(sb);
        //            tw.Flush();
        //            tw.Close();
        //        }
        //
        //
        //        response.batch = batch;
        //        response.downloadUrl = $"api/v1/download/{filename}?downloadasfilename={downloadFileName}";
        //        response.success = true;
        //    }
        //    return response;
        //}
        //-------------------------------------------------------------------------------------------------------
    }
    //-------------------------------------------------------------------------------------------------------
}
