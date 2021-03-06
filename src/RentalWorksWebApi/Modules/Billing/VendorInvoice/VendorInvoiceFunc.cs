﻿using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Data;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Billing.VendorInvoice
{

    public class UpdateVendorInvoiceItemsRequest
    {
        public string VendorInvoiceId { get; set; }
        public string PurchaseOrderId { get; set; }
        public DateTime? BillingStartDate { get; set; }
        public DateTime? BillingEndDate { get; set; }
    }


    public class UpdateVendorInvoiceItemsResponse : TSpStatusResponse
    {
    }

    public class ToggleVendorInvoiceApprovedRequest
    {
        public string VendorInvoiceId { get; set; }
        public bool? PushChangesToPurchaseOrder { get; set; }
    }

    public class ToggleVendorInvoiceApprovedResponse : TSpStatusResponse
    {
        public bool PromptToUpdatePurchaseOrder { get; set; } = false;
    }

    public static class VendorInvoiceFunc
    {
        //-------------------------------------------------------------------------------------------------------
        public static async Task<UpdateVendorInvoiceItemsResponse> UpdateVendorInvoiceItem(FwApplicationConfig appConfig, FwUserSession userSession, UpdateVendorInvoiceItemsRequest request)
        {
            UpdateVendorInvoiceItemsResponse response = new UpdateVendorInvoiceItemsResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "updatevendorinvoiceitem", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@vendorinvoiceid", SqlDbType.NVarChar, ParameterDirection.Input, request.VendorInvoiceId);
                qry.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Input, request.PurchaseOrderId);
                qry.AddParameter("@billingstart", SqlDbType.Date, ParameterDirection.Input, request.BillingStartDate);
                qry.AddParameter("@billingend", SqlDbType.Date, ParameterDirection.Input, request.BillingEndDate);
                qry.AddParameter("@chargetype", SqlDbType.NVarChar, ParameterDirection.Input, null);
                qry.AddParameter("@showall", SqlDbType.NVarChar, ParameterDirection.Input, "F");
                //qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                //qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                //response.status = qry.GetParameter("@status").ToInt32();
                //response.success = (response.status == 0);
                //response.msg = qry.GetParameter("@msg").ToString();
                response.success = true;
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------    
        public static async Task<ToggleVendorInvoiceApprovedResponse> ToggleVendorInvoiceApproved(FwApplicationConfig appConfig, FwUserSession userSession, ToggleVendorInvoiceApprovedRequest request)
        {
            ToggleVendorInvoiceApprovedResponse response = new ToggleVendorInvoiceApprovedResponse();
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, "vendorinvoiceapproved", appConfig.DatabaseSettings.QueryTimeout);
                qry.AddParameter("@vendorinvoiceid", SqlDbType.NVarChar, ParameterDirection.Input, request.VendorInvoiceId);
                qry.AddParameter("@approvedusersid", SqlDbType.NVarChar, ParameterDirection.Input, userSession.UsersId);
                if (request.PushChangesToPurchaseOrder == null)
                {
                    qry.AddParameter("@pushchangestopo", SqlDbType.NVarChar, ParameterDirection.Input, "?");
                }
                else
                {
                    qry.AddParameter("@pushchangestopo", SqlDbType.NVarChar, ParameterDirection.Input, request.PushChangesToPurchaseOrder);
                }
                //qry.AddParameter("@approveifdealinvisnew", SqlDbType.NVarChar, ParameterDirection.Input, "F");
                qry.AddParameter("@prompttopushchangestopo", SqlDbType.NVarChar, ParameterDirection.Output);
                qry.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                response.status = qry.GetParameter("@status").ToInt32();
                response.success = (response.status == 0);
                response.msg = qry.GetParameter("@msg").ToString();
                response.PromptToUpdatePurchaseOrder = FwConvert.ToBoolean(qry.GetParameter("@prompttopushchangestopo").ToString());
            }
            return response;
        }
        //-------------------------------------------------------------------------------------------------------    
    }
}
