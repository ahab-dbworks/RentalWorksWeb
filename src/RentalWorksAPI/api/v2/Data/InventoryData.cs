using Fw.Json.SqlServer;
using RentalWorksAPI.api.v2.Models;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

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

                icode.masterid    = icodes[i].masterid;
                icode.warehouseid = icodes[i].warehouseid;
                icode.total       = icodes[i].total;
                icode.consigned   = icodes[i].consigned;
                icode.inqty       = icodes[i].qtyin;
                icode.incontainer = icodes[i].incontainer;
                icode.qcrequired  = icodes[i].qcrequired;
                icode.staged      = icodes[i].staged;
                icode.outqty      = icodes[i].qtyout;
                icode.inrepair    = icodes[i].inrepair;

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
                transaction.dealname = transactions[i].deal;
                transaction.qty      = transactions[i].qty;

                result.Add(transaction);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
    }
}