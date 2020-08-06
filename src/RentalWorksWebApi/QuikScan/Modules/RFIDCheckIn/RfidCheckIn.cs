using Fw.Json.Services;
using Fw.Json.Services.Common;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using RentalWorksQuikScan.Source;
using System.Data;
using System.Data.SqlClient;

namespace RentalWorksQuikScan.Modules
{
    public class RfidCheckIn
    {
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void ProcessBatch(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "RfidCheckIn.ProcessBatch";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "portal");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "tags");
            string orderid      = request.orderid;
            string portal       = request.portal;
            string tags         = request.tags;
            string rentalitemid = string.Empty;
            string usersid      = session.security.webUser.usersid;
            string batchid      = null;
            string rfidmode     = "Check-In";
            using (FwSqlConnection conn = FwSqlConnection.RentalWorks)
            {
                conn.Open();
                using (SqlTransaction transaction = conn.GetConnection().BeginTransaction())
                {
                    // log the batch of tags in the scannedtag table
                    using (FwSqlCommand sp = new FwSqlCommand(conn, "logrfidtags"))
                    {
                        sp.Transaction = transaction;
                        sp.AddParameter("@sessionid", orderid);
                        sp.AddParameter("@portal", portal);
                        sp.AddParameter("@tags", tags);
                        sp.AddParameter("@rentalitemid", rentalitemid);
                        sp.AddParameter("@usersid", usersid);
                        sp.AddParameter("@batchid", SqlDbType.VarChar, ParameterDirection.Output);
                        sp.Execute(false);
                        batchid = sp.GetParameter("@batchid").ToString();
                    }

                    // process the batch in the scannedtag table
                    using (FwSqlCommand sp = new FwSqlCommand(conn, "processscannedtags"))
                    {
                        sp.Transaction = transaction;
                        sp.AddParameter("@sessionid", orderid);
                        sp.AddParameter("@portal", portal);
                        sp.AddParameter("@batchid", batchid);
                        sp.AddParameter("@usersid", usersid);
                        sp.AddParameter("@rfidmode", rfidmode);
                        sp.Execute(false);
                    }
                    transaction.Commit();
                }
                conn.Close();
            }
            response.batchid = batchid;
            response.funcscannedtag = GetProcessed(orderid, usersid, portal, batchid);
            response.funcscannedtagexception = GetExceptions(orderid, usersid, portal);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void GetExceptions(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "RfidCheckIn.GetExceptions";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "portal");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "batchid");
            string orderid = request.orderid;
            string portal  = request.portal;
            string batchid = request.batchid;
            string usersid = session.security.webUser.usersid;
            response.funcscannedtag = GetProcessed(orderid, usersid, portal, batchid);
            response.funcscannedtagexception = GetExceptions(orderid, usersid, portal);
        }
        //---------------------------------------------------------------------------------------------
        public static FwJsonDataTable GetProcessed(string orderid, string usersid, string portal, string batchid)
        {
            FwJsonDataTable dt;
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("select tag, master, status");
                qry.AddColumn("tag",    false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("master", false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("status", false, FwJsonDataTableColumn.DataTypes.Text);
                qry.Add("from funcscannedtag(@sessionid, @orderid, @usersid, @portal, @batchid, @rfidmode)");
                qry.AddParameter("@sessionid", orderid);
                qry.AddParameter("@orderid",   "");
                qry.AddParameter("@usersid",   usersid);
                qry.AddParameter("@portal",    portal);
                qry.AddParameter("@batchid",   batchid);
                qry.AddParameter("@rfidmode",  "CHECKIN");
                dt = qry.QueryToFwJsonTable();
            }
            return dt;
        }
        //---------------------------------------------------------------------------------------------
        private static FwJsonDataTable GetExceptions(string contractid, string usersid, string portal)
        {
            FwJsonDataTable dt;
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("select tag, master, message, exceptiontype");
                qry.AddColumn("tag",           false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("master",        false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("message",       false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("exceptiontype", false, FwJsonDataTableColumn.DataTypes.Text);
                qry.Add("from funcscannedtagexception(@sessionid, @usersid, @portal)");
                qry.AddParameter("@sessionid", contractid);
                qry.AddParameter("@usersid", usersid);
                qry.AddParameter("@portal", portal);
                dt = qry.QueryToFwJsonTable();
            }
            return dt;
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void ClearException(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "RfidCheckIn.ReleaseFromRepair";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "portal");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "tag");
            string orderid = request.orderid;
            string portal  = request.portal;
            string usersid = session.security.webUser.usersid;
            string tag     = request.tag;
            FwSqlConnection conn = FwSqlConnection.RentalWorks;
            using (FwSqlCommand sp = new FwSqlCommand(conn, "scannedtagclearexception"))
            {
                sp.AddParameter("@sessionid", orderid);
                sp.AddParameter("@tag", tag);
                sp.Execute();
            }
            response.status = 0;
            response.message = "";
            //response.funcscannedtagexception = LoadExceptions(orderid, usersid, portal);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void ClearAllExceptions(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "RfidCheckIn.ClearAllExceptions";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "portal");
            string orderid = request.orderid;
            string portal  = request.portal;
            string usersid = session.security.webUser.usersid;
            FwJsonDataTable dt = GetExceptions(orderid, usersid, portal);
            int col_tag = dt.ColumnIndex["tag"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string tag     = dt.Rows[i][col_tag].ToString();
                FwSqlConnection conn = FwSqlConnection.RentalWorks;
                using (FwSqlCommand sp = new FwSqlCommand(conn, "scannedtagclearexception"))
                {
                    sp.AddParameter("@sessionid", orderid);
                    sp.AddParameter("@tag", tag);
                    sp.Execute();
                }
            }
            response.funcscannedtagexception = GetExceptions(orderid, usersid, portal);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void GetPendingItems(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "RfidCheckIn.GetPendingItems";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
            string contractid      = request.contractid;
            string rectype         = request.rectype;
            string containeritemid = request.containeritemid;
            FwSqlConnection conn   = FwSqlConnection.RentalWorks;
            session.userLocation   = RwAppData.GetUserLocation(conn:    conn,
                                                               usersId: session.security.webUser.usersid);
            string warehouseid     = session.userLocation.warehouseId;
            using (FwSqlCommand qry = new FwSqlCommand(conn))
            {
                qry.AddColumn("orderid",            false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("masterid",           false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("parentid",           false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("masteritemid",       false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("exceptionflg",       false, FwJsonDataTableColumn.DataTypes.Boolean);
                qry.AddColumn("somein",             false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("masterno",           false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("description",        false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("vendor",             false, FwJsonDataTableColumn.DataTypes.Text);
                //qry.AddColumn("vendorid",           false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("qtyordered",         false, FwJsonDataTableColumn.DataTypes.Decimal);
                qry.AddColumn("qtystagedandout",    false, FwJsonDataTableColumn.DataTypes.Decimal);
                qry.AddColumn("qtyout",             false, FwJsonDataTableColumn.DataTypes.Decimal);
                qry.AddColumn("qtysub",             false, FwJsonDataTableColumn.DataTypes.Decimal);
                qry.AddColumn("qtysubstagedandout", false, FwJsonDataTableColumn.DataTypes.Decimal);
                qry.AddColumn("qtyin",              false, FwJsonDataTableColumn.DataTypes.Decimal);
                qry.AddColumn("qtysubout",          false, FwJsonDataTableColumn.DataTypes.Decimal);
                qry.AddColumn("qtystillout",        false, FwJsonDataTableColumn.DataTypes.Decimal);
                qry.AddColumn("missingflg",         false, FwJsonDataTableColumn.DataTypes.Boolean);
                qry.AddColumn("missingqty",         false, FwJsonDataTableColumn.DataTypes.Decimal);
                qry.AddColumn("trackedby",          false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("rectype",            false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("itemclass",          false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("itemorder",          false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("orderby",            false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("optioncolor",        false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("warehouseid",        false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("whcode",             false, FwJsonDataTableColumn.DataTypes.Decimal);
                qry.AddColumn("orderno",            false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("isbarcode",          false, FwJsonDataTableColumn.DataTypes.Boolean);
                qry.AddColumn("contractid",         false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("subbyquantity",      false, FwJsonDataTableColumn.DataTypes.Boolean);
                qry.AddColumn("nestedmasteritemid", false, FwJsonDataTableColumn.DataTypes.Text);
                qry.Add("select *");
                qry.Add("  from dbo.funccheckinexception(@contractid, @rectype, @containeritemid, 'T')");
                qry.Add(" where exceptionflg = 'T'");
                qry.Add("order by orderno, itemorder, masterno");
                qry.AddParameter("@contractid", contractid);
                qry.AddParameter("@rectype", rectype);
                qry.AddParameter("@containeritemid", containeritemid);
                response.funccheckoutexception = qry.QueryToFwJsonTable(true);
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void GetSessionItems(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "RfidCheckIn.GetSessionItems";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
            string orderid = request.orderid;
            FwSqlConnection conn = FwSqlConnection.RentalWorks;
            session.userLocation = RwAppData.GetUserLocation(conn:    conn,
                                                             usersId: session.security.webUser.usersid);
            bool summary = false;
            string warehouseid = session.userLocation.warehouseId;
            using (FwSqlCommand qry = new FwSqlCommand(conn))
            {
                qry.AddColumn("rectype",      false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("masteritemid", false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("description",  false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("masterno",     false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("barcode",      false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("quantity",     false, FwJsonDataTableColumn.DataTypes.Decimal);
                qry.AddColumn("vendorid",     false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("vendor",       false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("itemclass",    false, FwJsonDataTableColumn.DataTypes.Text);
                qry.AddColumn("trackedby",    false, FwJsonDataTableColumn.DataTypes.Text);
                qry.Add("select");
                qry.Add("  rectype,");
                qry.Add("  masteritemid,");
                qry.Add("  description,");
                qry.Add("  masterno,");
                qry.Add("  barcode,");
                qry.Add("  quantity,");
                qry.Add("  vendorid,");
                qry.Add("  vendor,");
                qry.Add("  itemclass,");
                qry.Add("  trackedby");
                qry.Add("from dbo.funcstageditemsweb(@orderid, @summary, @warehouseid)");
                qry.Add("where trackedby = 'RFID'");
                qry.Add("order by orderby");
                qry.AddParameter("@orderid", orderid);
                qry.AddParameter("@summary", FwConvert.LogicalToCharacter(summary));
                qry.AddParameter("@warehouseid", warehouseid);
                response.funcstageditemsweb = qry.QueryToFwJsonTable();
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void UnstageItem(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "RfidCheckIn.GetStagedItems";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
            string orderid = request.orderid;
            string barcode = request.barcode;
            string contractid = "";
            string usersid = session.security.webUser.usersid;
            int movemode = 2; //staged to inventory
            FwSqlConnection conn = FwSqlConnection.RentalWorks;
            using (FwSqlCommand sp = new FwSqlCommand(conn, "dbo.advancedmovebarcode"))
            {
                sp.AddParameter("@orderid",    orderid);
                sp.AddParameter("@barcode",    barcode);
                sp.AddParameter("@contractid", contractid);
                sp.AddParameter("@usersid",    usersid);
                sp.AddParameter("@movemode",   movemode);
                sp.AddParameter("@status", SqlDbType.Decimal, ParameterDirection.Output);
                sp.AddParameter("@msg",    SqlDbType.VarChar, ParameterDirection.Output);
                sp.Execute();
                response.status = sp.GetParameter("@status").ToDecimal();
                response.msg    = sp.GetParameter("@msg").ToString();
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
