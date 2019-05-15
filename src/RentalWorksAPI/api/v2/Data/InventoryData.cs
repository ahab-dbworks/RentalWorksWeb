using Fw.Json.SqlServer;
using RentalWorksAPI.api.v2.Models;
using RentalWorksAPI.api.v2.Models.InventoryModels.ItemStatus;
using RentalWorksAPI.api.v2.Models.InventoryModels.ItemStatusByICode;
using RentalWorksAPI.api.v2.Models.InventoryModels.WarehouseAddToOrder;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace RentalWorksAPI.api.v2.Data
{
    public class InventoryData
    {
        //----------------------------------------------------------------------------------------------------
        public static List<ICode> GetICodes(string masterid, string warehouseid, int? transactionhistoryqty)
        {
            FwSqlCommand qry;
            List<ICode> result = new List<ICode>();
            dynamic icodes = new ExpandoObject();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select *");
            qry.Add("  from apirest_icodestatusfunc(@masterid, @warehouseid)");
            qry.AddParameter("@masterid",    masterid);
            qry.AddParameter("@warehouseid", warehouseid);

            icodes = qry.QueryToDynamicList2();

            for (int i = 0; i < icodes.Count; i++)
            {
                ICode icode = new ICode();

                icode.masterid       = icodes[i].masterid;
                icode.warehouseid    = icodes[i].warehouseid;
                icode.qty            = icodes[i].qty;
                icode.qtyconsigned   = icodes[i].qtyconsigned;
                icode.qtyin          = icodes[i].qtyin;
                icode.qtyincontainer = icodes[i].qtyincontainer;
                icode.qtyqcrequired  = icodes[i].qtyqcrequired;
                icode.qtystaged      = icodes[i].qtystaged;
                icode.qtyout         = icodes[i].qtyout;
                icode.qtyinrepair    = icodes[i].qtyinrepair;

                if (transactionhistoryqty.GetValueOrDefault() > 0)
                {
                    icode.transactions = GetTransactions(masterid, warehouseid, transactionhistoryqty.Value);
                }

                result.Add(icode);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static List<Transaction> GetTransactions(string masterid, string warehouseid, int transactionhistoryqty)
        {
            FwSqlCommand qry;
            List<Transaction> result = new List<Transaction>();
            dynamic transactions = new ExpandoObject();

            qry = new FwSqlCommand(FwSqlConnection.RentalWorks);
            qry.Add("select top " + transactionhistoryqty + " *");
            qry.Add("  from apirest_icodestatushistoryfunc(@masterid, @warehouseid)");
            qry.Add("order by activitydatetime desc");
            qry.AddParameter("@masterid",              masterid);
            qry.AddParameter("@warehouseid",           warehouseid);

            transactions = qry.QueryToDynamicList2();

            for (int i = 0; i < transactions.Count; i++)
            {
                Transaction transaction = new Transaction();

                transaction.type             = transactions[i].type;
                transaction.activitydatetime = transactions[i].activitydatetime;
                transaction.orderid          = transactions[i].orderid;
                transaction.orderno          = transactions[i].orderno;
                transaction.deal             = transactions[i].deal;
                transaction.qty              = transactions[i].qty;

                result.Add(transaction);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static ItemStatusResponse GetItemStatus(string barcode, string serialno, string rfid, int? days)
        {
            ItemStatusResponse result = new ItemStatusResponse();
            dynamic qryresult         = new ExpandoObject();

            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("select *");
                qry.Add("from apirest_itemstatusfunc(@barcode, @serialno, @rfid)");
                qry.AddParameter("@barcode",  barcode);
                qry.AddParameter("@serialno", serialno);
                qry.AddParameter("@rfid",     rfid);
                qryresult = qry.QueryToDynamicObject2();
            }

            if (Object.ReferenceEquals(null, qryresult))
            {
                result.status = "Item not found.";
            }
            else if (qryresult.masterid != "")
            {
                result.rentalitemid = qryresult.rentalitemid;
                result.masterid     = qryresult.masterid;
                result.masterno     = qryresult.masterno;
                result.master       = qryresult.master;
                result.mfgserial    = qryresult.mfgserial;
                result.rfid         = qryresult.rfid;
                result.barcode      = qryresult.barcode;
                result.status       = qryresult.status;

                if (days.GetValueOrDefault() != 0)
                {
                    result.transactions = GetItemStatusHistory(qryresult.rentalitemid, days.Value);
                }
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static List<ItemStatusHistory> GetItemStatusHistory(string rentalitemid, int days)
        {
            List<ItemStatusHistory> result = new List<ItemStatusHistory>();
            dynamic qryresult              = new ExpandoObject();

            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.AddColumn("transactiondatetime", false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.DateTime);
                qry.AddColumn("qty",                 false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.Integer);
                qry.Add("select *");
                qry.Add("  from apirest_itemstatushistoryfunc(@rentalitemid, @days)");
                qry.Add("order by transactiondatetime desc");
                qry.AddParameter("@rentalitemid", rentalitemid);
                qry.AddParameter("@days",         days);
                qryresult = qry.QueryToDynamicList2();
            }

            for (int i = 0; i < qryresult.Count; i++)
            {
                ItemStatusHistory transaction = new ItemStatusHistory();

                transaction.type     = qryresult[i].type;
                transaction.datetime = qryresult[i].transactiondatetime;
                transaction.orderid  = qryresult[i].orderid;
                transaction.orderno  = qryresult[i].orderno;
                transaction.dealname = qryresult[i].dealname;
                transaction.qty      = qryresult[i].qty;

                result.Add(transaction);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static List<WarehouseAddToOrderItem> GetWarehouseAddToOrder(string warehouseid)
        {
            List<WarehouseAddToOrderItem> result = new List<WarehouseAddToOrderItem>();
            dynamic qryresult                    = new ExpandoObject();

            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("select *");
                qry.Add("from apirest_warehouseaddtoorderview");
                qry.Add("where warehouseid = @warehouseid");
                qry.AddParameter("@warehouseid", warehouseid);
                qryresult = qry.QueryToDynamicList2();
            }

            for (int i = 0; i < qryresult.Count; i++)
            {
                WarehouseAddToOrderItem item = new WarehouseAddToOrderItem();

                item.masterid     = qryresult[i].masterid;
                item.masterno     = qryresult[i].masterno;
                item.master       = qryresult[i].master;
                item.departmentid = qryresult[i].inventorytypeid;
                item.department   = qryresult[i].inventorytype;

                result.Add(item);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static List<WarehouseAddToOrderItem> GetWarehousesAddToOrder(List<string> warehouseids)
        {
            List<WarehouseAddToOrderItem> result = new List<WarehouseAddToOrderItem>();
            dynamic qryresult                    = new ExpandoObject();

            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("select *");
                qry.Add("from apirest_warehouseaddtoorderfunc(@warehouseids)");
                qry.AddParameter("@warehouseids", string.Join(",", warehouseids));
                qryresult = qry.QueryToDynamicList2();
            }

            for (int i = 0; i < qryresult.Count; i++)
            {
                WarehouseAddToOrderItem item = new WarehouseAddToOrderItem();

                item.masterid     = qryresult[i].masterid;
                item.masterno     = qryresult[i].masterno;
                item.master       = qryresult[i].master;
                item.departmentid = qryresult[i].inventorytypeid;
                item.department   = qryresult[i].inventorytype;
                item.warehouseid  = qryresult[i].warehouseid;

                result.Add(item);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static ItemStatusByICodeResponse GetItemStatusByICode(string masterid, string warehouseid)
        {
            ItemStatusByICodeResponse result = new ItemStatusByICodeResponse();
            dynamic qryresult                = new ExpandoObject();

            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("select top 1 *");
                qry.Add("from apirest_rentalinventoryview");
                qry.Add("where masterid    = @masterid");
                qry.Add("  and warehouseid = @warehouseid");
                qry.AddParameter("@masterid",    masterid);
                qry.AddParameter("@warehouseid", warehouseid);
                qryresult = qry.QueryToDynamicObject2();
            }

            if (Object.ReferenceEquals(null, qryresult))
            {
                
            }
            else if (qryresult.masterid != "")
            {
                WarehouseAddToOrderItem item = new WarehouseAddToOrderItem();

                result.masterid     = qryresult.masterid;
                result.masterno     = qryresult.masterno;
                result.master       = qryresult.master;
                result.warehouseid  = qryresult.warehouseid;

                result.barcodes = GetItemStatusByICodeBarcodes(masterid, warehouseid);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static List<Barcode> GetItemStatusByICodeBarcodes(string masterid, string warehouseid)
        {
            List<Barcode> result = new List<Barcode>();
            dynamic qryresult    = new ExpandoObject();

            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("select *");
                qry.Add("from apirest_rentalitemview");
                qry.Add("where masterid    = @masterid");
                qry.Add("  and warehouseid = @warehouseid");
                qry.AddParameter("@masterid",    masterid);
                qry.AddParameter("@warehouseid", warehouseid);
                qryresult = qry.QueryToDynamicList2();
            }

            for (int i = 0; i < qryresult.Count; i++)
            {
                Barcode item = new Barcode();

                item.barcode = qryresult[i].barcode;
                item.status  = qryresult[i].rentalstatus;
                item.orderid = qryresult[i].orderid;
                item.orderno = qryresult[i].orderno;

                result.Add(item);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
    }
}