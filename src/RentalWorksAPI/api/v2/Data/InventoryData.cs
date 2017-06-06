using Fw.Json.SqlServer;
using RentalWorksAPI.api.v2.Models;
using RentalWorksAPI.api.v2.Models.InventoryModels.ItemStatusModels;
using RentalWorksAPI.api.v2.Models.InventoryModels.WarehouseAddToOrder;
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

                if (transactionhistoryqty.GetValueOrDefault() != 0)
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
            qry.Add("select *");
            qry.Add("  from apirest_icodestatushistoryfunc(@masterid, @warehouseid, @transactionhistoryqty)");
            qry.AddParameter("@masterid",              masterid);
            qry.AddParameter("@warehouseid",           warehouseid);
            qry.AddParameter("@transactionhistoryqty", transactionhistoryqty);

            transactions = qry.QueryToDynamicList2();

            for (int i = 0; i < transactions.Count; i++)
            {
                Transaction transaction = new Transaction();

                transaction.type     = transactions[i].type;
                transaction.datetime = transactions[i].datetime;
                transaction.orderid  = transactions[i].orderid;
                transaction.orderno  = transactions[i].orderno;
                transaction.deal     = transactions[i].deal;
                transaction.qty      = transactions[i].qty;

                result.Add(transaction);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static ItemStatus GetItemStatus(string barcode, string serialno, string rfid)
        {
            ItemStatus result = new ItemStatus();

            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.AddColumn("masterid", false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("masterno", false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("master", false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("mfgserial", false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("rfid", false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("barcode", false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("status", false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("rentalitemid", false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.Text);
                qry.Add("select *");
                qry.Add("from apirest_itemstatusfunc(@barcode, @serialno, @rfid)");
                qry.AddParameter("@barcode", barcode);
                qry.AddParameter("@serialno", serialno);
                qry.AddParameter("@rfid", rfid);
                result = qry.QueryToTypedObject<ItemStatus>();
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static List<ItemStatusHistory> GetItemStatusHistory(string rentalitemid, int days)
        {
            List<ItemStatusHistory> result = new List<ItemStatusHistory>();

            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.AddColumn("datetime", false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.Date);
                qry.AddColumn("qty", false, Fw.Json.Services.FwJsonDataTableColumn.DataTypes.Integer);
                qry.Add("select *");
                qry.Add("from apirest_itemstatushistoryfunc(@rentalitemid, @days)");
                qry.AddParameter("@rentalitemid", rentalitemid);
                qry.AddParameter("@days", days);
                result = qry.QueryToTypedList<ItemStatusHistory>();
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public static List<WarehouseAddToOrderItem> GetWarehouseAddToOrder(string warehouseid)
        {
            List<WarehouseAddToOrderItem> result = new List<WarehouseAddToOrderItem>();

            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("select *");
                qry.Add("from apirest_warehouseaddtoorderview");
                qry.Add("where warehouseid = @warehouseid");
                qry.AddParameter("@warehouseid", warehouseid);
                result = qry.QueryToTypedList<WarehouseAddToOrderItem>();
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
    }
}