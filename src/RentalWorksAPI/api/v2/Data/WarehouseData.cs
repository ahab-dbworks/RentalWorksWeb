using Fw.Json.SqlServer;
using RentalWorksAPI.api.v2.Models.WarehouseModels.MoveToContract;
using RentalWorksAPI.api.v2.Models.WarehouseModels.StageItemModels;
using RentalWorksAPI.api.v2.Models.WarehouseModels.UnstageItemModels;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;

namespace RentalWorksAPI.api.v2.Data
{
    public class WarehouseData
    {
        //----------------------------------------------------------------------------------------------------
        public static StageItemResponse StageItem(string orderid, string webusersid, List<StageItem> items)
        {
            StageItemResponse result = new StageItemResponse();

            result.orderid = orderid;
            result.items   = new List<StageItemResponseItem>();

            for (int i = 0; i < items.Count; i++)
            {
                using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks, "apirest_stageitem"))
                {
                    qry.AddParameter("@orderid",       orderid);
                    qry.AddParameter("@code",          items[i].barcode);
                    qry.AddParameter("@webusersid",    webusersid);
                    qry.AddParameter("@qty",           items[i].qty);
                    qry.AddParameter("@masteritemid",  SqlDbType.Char,    ParameterDirection.InputOutput, items[i].masteritemid);
                    qry.AddParameter("@statuscode",    SqlDbType.VarChar, ParameterDirection.Output);
                    qry.AddParameter("@statusmessage", SqlDbType.VarChar, ParameterDirection.Output);
                    qry.AddParameter("@barcode",       SqlDbType.VarChar, ParameterDirection.Output);
                    qry.AddParameter("@masterid",      SqlDbType.VarChar, ParameterDirection.Output);
                    qry.AddParameter("@quantity",      SqlDbType.VarChar, ParameterDirection.Output);
                    qry.AddParameter("@datetimestamp", SqlDbType.VarChar, ParameterDirection.Output);
                    qry.Execute();

                    StageItemResponseItem item = new StageItemResponseItem();
                    item.statuscode    = qry.GetParameter("@statuscode").ToString().TrimEnd();
                    item.statusmessage = qry.GetParameter("@statusmessage").ToString().TrimEnd();
                    item.barcode       = qry.GetParameter("@barcode").ToString().TrimEnd();
                    item.masteritemid  = qry.GetParameter("@masteritemid").ToString().TrimEnd();
                    item.masterid      = qry.GetParameter("@masterid").ToString().TrimEnd();
                    item.quantity      = qry.GetParameter("@quantity").ToInt32();
                    item.datetimestamp = qry.GetParameter("@datetimestamp").ToString().TrimEnd();

                    result.items.Add(item);
                }
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static UnstageItemResponse UnstageItem(string orderid, string webusersid, List<UnstageItem> items)
        {
            UnstageItemResponse result = new UnstageItemResponse();

            result.orderid = orderid;
            result.items   = new List<UnstageItemResponseItem>();

            for (int i = 0; i < items.Count; i++)
            {
                using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks, "apirest_unstageitem"))
                {
                    qry.AddParameter("@orderid",       orderid);
                    qry.AddParameter("@code",          items[i].barcode);
                    qry.AddParameter("@webusersid",    webusersid);
                    qry.AddParameter("@qty",           items[i].qty);
                    qry.AddParameter("@masteritemid",  SqlDbType.Char,    ParameterDirection.InputOutput, items[i].masteritemid);
                    qry.AddParameter("@statuscode",    SqlDbType.VarChar, ParameterDirection.Output);
                    qry.AddParameter("@statusmessage", SqlDbType.VarChar, ParameterDirection.Output);
                    qry.AddParameter("@barcode",       SqlDbType.VarChar, ParameterDirection.Output);
                    qry.AddParameter("@masterid",      SqlDbType.VarChar, ParameterDirection.Output);
                    qry.AddParameter("@quantity",      SqlDbType.VarChar, ParameterDirection.Output);
                    qry.AddParameter("@datetimestamp", SqlDbType.VarChar, ParameterDirection.Output);
                    qry.Execute();

                    UnstageItemResponseItem item = new UnstageItemResponseItem();
                    item.statuscode    = qry.GetParameter("@statuscode").ToString().TrimEnd();
                    item.statusmessage = qry.GetParameter("@statusmessage").ToString().TrimEnd();
                    item.barcode       = qry.GetParameter("@barcode").ToString().TrimEnd();
                    item.masteritemid  = qry.GetParameter("@masteritemid").ToString().TrimEnd();
                    item.masterid      = qry.GetParameter("@masterid").ToString().TrimEnd();
                    item.quantity      = qry.GetParameter("@quantity").ToInt32();
                    item.datetimestamp = qry.GetParameter("@datetimestamp").ToString().TrimEnd();

                    result.items.Add(item);
                }
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static MoveToContractResponse MoveToContract(string orderid, string webusersid, List<MoveToContactItem> items)
        {
            MoveToContractResponse result = new MoveToContractResponse();
            string contractid             = string.Empty;

            result.orderid = orderid;
            result.items   = new List<MoveToContractResponseItem>();

            for (int i = 0; i < items.Count; i++)
            {
                using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks, "apirest_movetocontract"))
                {
                    qry.AddColumn("datetimestamp", false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.DateTime);
                    qry.AddParameter("@webusersid",    webusersid); //required
                    qry.AddParameter("@orderid",       orderid);    //required
                    qry.AddParameter("@code",          items[i].barcode);
                    qry.AddParameter("@qty",           items[i].qty);
                    qry.AddParameter("@masteritemid",  SqlDbType.Char,    ParameterDirection.InputOutput, items[i].masteritemid);
                    qry.AddParameter("@contractid",    SqlDbType.Char,    ParameterDirection.InputOutput, contractid);
                    qry.AddParameter("@statuscode",    SqlDbType.Int,     ParameterDirection.Output);
                    qry.AddParameter("@statusmessage", SqlDbType.VarChar, ParameterDirection.Output);
                    qry.AddParameter("@barcode",       SqlDbType.VarChar, ParameterDirection.Output);
                    qry.AddParameter("@masterid",      SqlDbType.VarChar, ParameterDirection.Output);
                    qry.AddParameter("@quantity",      SqlDbType.Int,     ParameterDirection.Output);
                    qry.AddParameter("@datetimestamp", SqlDbType.VarChar, ParameterDirection.Output);
                    qry.Execute();

                    contractid = qry.GetParameter("@contractid").ToString().TrimEnd();

                    MoveToContractResponseItem item = new MoveToContractResponseItem();
                    item.statuscode    = qry.GetParameter("@statuscode").ToString().TrimEnd();
                    item.statusmessage = qry.GetParameter("@statusmessage").ToString().TrimEnd();
                    item.barcode       = qry.GetParameter("@barcode").ToString().TrimEnd();
                    item.masteritemid  = qry.GetParameter("@masteritemid").ToString().TrimEnd();
                    item.masterid      = qry.GetParameter("@masterid").ToString().TrimEnd();
                    item.quantity      = qry.GetParameter("@quantity").ToInt32();
                    item.datetimestamp = qry.GetParameter("@datetimestamp").ToString().TrimEnd();

                    result.items.Add(item);
                }
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
    }
}