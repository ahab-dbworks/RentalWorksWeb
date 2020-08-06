using Fw.Json.Services;
using Fw.Json.Services.Common;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using RentalWorksQuikScan.Source;
using System.Data;
using System.Data.SqlClient;

namespace RentalWorksQuikScan.Modules
{
    public class RfidStaging
    {
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void ProcessBatch(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "RfidStaging.ProcessBatch";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "portal");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "tags");
            string orderid      = request.orderid;
            string portal       = request.portal;
            string tags         = request.tags;
            string rentalitemid = string.Empty;
            string usersid      = session.security.webUser.usersid;
            string batchid      = null;
            string rfidmode     = "STAGING";
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
            const string METHOD_NAME = "RfidStaging.GetExceptions";
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
                qry.Add("where status in ('PROCESSED', 'NEW')");
                qry.AddParameter("@sessionid", orderid);
                qry.AddParameter("@orderid",   "");
                qry.AddParameter("@usersid",   usersid);
                qry.AddParameter("@portal",    portal);
                qry.AddParameter("@batchid",   batchid);
                qry.AddParameter("@rfidmode",  "STAGING");
                dt = qry.QueryToFwJsonTable();
            }
            return dt;
        }
        //---------------------------------------------------------------------------------------------
        private static FwJsonDataTable GetExceptions(string orderid, string usersid, string portal)
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
                qry.AddParameter("@sessionid", orderid);
                qry.AddParameter("@usersid", usersid);
                qry.AddParameter("@portal", portal);
                dt = qry.QueryToFwJsonTable();
            }
            return dt;
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void AddItemToOrder(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "RfidStaging.AddItemToOrder";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "portal");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "tag");
            string orderid = request.orderid;
            string portal  = request.portal;
            string usersid = session.security.webUser.usersid;
            string tag     = request.tag;
            FwSqlConnection conn = FwSqlConnection.RentalWorks;
            using (FwSqlCommand sp = new FwSqlCommand(conn, "processadditemtoorder"))
            {
                sp.AddParameter("@orderid", orderid);
                sp.AddParameter("@tag", tag);
                sp.AddParameter("@usersid", usersid);
                sp.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                sp.AddParameter("@msg", SqlDbType.VarChar, ParameterDirection.Output);
                sp.Execute();
                response.status = sp.GetParameter("@status").ToInt32();
                response.msg    = sp.GetParameter("@msg").ToString();
            }
            //response.funcscannedtagexception = LoadExceptions(orderid, usersid, portal);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void AddCompleteToOrder(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "RfidStaging.AddCompleteToOrder";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "portal");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "tag");
            string orderid = request.orderid;
            string portal  = request.portal;
            string usersid = session.security.webUser.usersid;
            string tag     = request.tag;
            FwSqlConnection conn = FwSqlConnection.RentalWorks;
            using (FwSqlCommand sp = new FwSqlCommand(conn, "processaddcompletetoorder"))
            {
                sp.AddParameter("@orderid", orderid);
                sp.AddParameter("@tag", tag);
                sp.AddParameter("@usersid", usersid);
                sp.AddParameter("@autostageacc", "F");
                sp.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                sp.AddParameter("@msg", SqlDbType.VarChar, ParameterDirection.Output);
                sp.Execute();
                response.status = sp.GetParameter("@status").ToInt32();
                response.msg    = sp.GetParameter("@msg").ToString();
            }
            //response.funcscannedtagexception = LoadExceptions(orderid, usersid, portal);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void OverrideAvailabilityConflict(dynamic request, dynamic response, dynamic session) {
            const string METHOD_NAME = "RfidStaging.OverrideAvailabilityConflict";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "portal");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "tag");
            string orderid = request.orderid;
            string portal  = request.portal;
            string usersid = session.security.webUser.usersid;
            string tag     = request.tag;
            FwSqlConnection conn = FwSqlConnection.RentalWorks;
            using (FwSqlCommand sp = new FwSqlCommand(conn, "processoverrideavailabilityconflict"))
            {
                sp.AddParameter("@orderid", orderid);
                sp.AddParameter("@tag", tag);
                sp.AddParameter("@usersid", usersid);
                sp.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                sp.AddParameter("@msg", SqlDbType.VarChar, ParameterDirection.Output);
                sp.Execute();
                response.status = sp.GetParameter("@status").ToInt32();
                response.msg    = sp.GetParameter("@msg").ToString();
            }
            //response.funcscannedtagexception = LoadExceptions(orderid, usersid, portal);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void TransferItemInRepair(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "RfidStaging.TransferItemInRepair";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "portal");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "tag");
            string orderid = request.orderid;
            string portal  = request.portal;
            string usersid = session.security.webUser.usersid;
            string tag     = request.tag;
            FwSqlConnection conn = FwSqlConnection.RentalWorks;
            using (FwSqlCommand sp = new FwSqlCommand(conn, "processtransferiteminrepair"))
            {
                sp.AddParameter("@orderid", orderid);
                sp.AddParameter("@tag", tag);
                sp.AddParameter("@usersid", usersid);
                sp.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                sp.AddParameter("@msg", SqlDbType.VarChar, ParameterDirection.Output);
                sp.Execute();
                response.status = sp.GetParameter("@status").ToInt32();
                response.msg    = sp.GetParameter("@msg").ToString();
            }
            //response.funcscannedtagexception = LoadExceptions(orderid, usersid, portal);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void ReleaseFromRepair(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "RfidStaging.ReleaseFromRepair";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "portal");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "tag");
            string orderid = request.orderid;
            string portal  = request.portal;
            string usersid = session.security.webUser.usersid;
            string tag     = request.tag;
            FwSqlConnection conn = FwSqlConnection.RentalWorks;
            using (FwSqlCommand sp = new FwSqlCommand(conn, "processreleasefromrepair"))
            {
                sp.AddParameter("@orderid", orderid);
                sp.AddParameter("@tag", tag);
                sp.AddParameter("@usersid", usersid);
                sp.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                sp.AddParameter("@msg", SqlDbType.VarChar, ParameterDirection.Output);
                sp.Execute();
                response.status = sp.GetParameter("@status").ToInt32();
                response.msg    = sp.GetParameter("@msg").ToString();
            }
            //response.funcscannedtagexception = LoadExceptions(orderid, usersid, portal);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void ClearException(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "RfidStaging.ReleaseFromRepair";
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
            const string METHOD_NAME = "RfidStaging.ClearAllExceptions";
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
            const string METHOD_NAME = "RfidStaging.GetPendingItems";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
            string orderid = request.orderid;
            FwSqlConnection conn = FwSqlConnection.RentalWorks;
            session.userLocation = RwAppData.GetUserLocation(conn:    conn,
                                                             usersId: session.security.webUser.usersid);
            string warehouseid = session.userLocation.warehouseId;
            using (FwSqlCommand qry = new FwSqlCommand(conn))
            {
                qry.AddColumn("orderid", false);
                qry.AddColumn("masterid",           false);
                qry.AddColumn("parentid",           false);
                qry.AddColumn("masteritemid",       false);
                qry.AddColumn("exceptionflg",       false, FwJsonDataTableColumn.DataTypes.Boolean);
                qry.AddColumn("someout",            false, FwJsonDataTableColumn.DataTypes.Boolean);
                qry.AddColumn("masterno",           false);
                qry.AddColumn("description",        false);
                qry.AddColumn("vendor",             false);
                //qry.AddColumn("vendorid",           false);
                qry.AddColumn("qtyordered",         false, FwJsonDataTableColumn.DataTypes.Decimal);
                qry.AddColumn("qtystagedandout",    false, FwJsonDataTableColumn.DataTypes.Decimal);
                qry.AddColumn("qtyout",             false, FwJsonDataTableColumn.DataTypes.Decimal);
                qry.AddColumn("qtysub",             false, FwJsonDataTableColumn.DataTypes.Decimal);
                qry.AddColumn("qtysubstagedandout", false, FwJsonDataTableColumn.DataTypes.Decimal);
                qry.AddColumn("qtysubout",          false, FwJsonDataTableColumn.DataTypes.Decimal);
                qry.AddColumn("missingflg",         false, FwJsonDataTableColumn.DataTypes.Boolean);
                qry.AddColumn("missingqty",         false, FwJsonDataTableColumn.DataTypes.Decimal);
                qry.AddColumn("trackedby",          false);
                qry.AddColumn("rectype",            false);
                qry.AddColumn("itemclass",          false);
                qry.AddColumn("itemorder",          false);
                qry.AddColumn("orderby",            false);
                qry.AddColumn("optioncolor",        false);
                qry.AddColumn("warehouseid",        false);
                qry.AddColumn("whcode",             false);
                qry.AddColumn("scannablemasterid",  false);
                qry.Add("select *");
                qry.Add("from dbo.funccheckoutexceptionrfid2(@orderid, @warehouseid)");
                qry.Add("order by orderby");
                qry.AddParameter("@orderid", orderid);
                qry.AddParameter("@warehouseid", warehouseid);
                response.funccheckoutexception = qry.QueryToFwJsonTable(true);
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public static void GetStagedItems(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "RfidStaging.GetStagedItems";
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
            const string METHOD_NAME = "RfidStaging.GetStagedItems";
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
        [FwJsonServiceMethod]
        public static void UnstageAll(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "RfidStaging.UnstageAll";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
            bool summary = false;
            FwSqlConnection conn = FwSqlConnection.RentalWorks;
            session.userLocation = RwAppData.GetUserLocation(conn: conn,
                                                             usersId: session.security.webUser.usersid);
            string warehouseid = session.userLocation.warehouseId;
            string orderid = request.orderid;
            string contractid = "";
            string usersid = session.security.webUser.usersid;
            int movemode = 2; //staged to inventory
            FwJsonDataTable dt = null;
            using (FwSqlCommand qry = new FwSqlCommand(conn))
            {
                qry.AddColumn("barcode", false, FwJsonDataTableColumn.DataTypes.Text);
                qry.Add("select barcode");
                qry.Add("from dbo.funcstageditemsweb(@orderid, @summary, @warehouseid)");
                qry.Add("where trackedby = 'RFID'");
                qry.Add("order by orderby");
                qry.AddParameter("@orderid", orderid);
                qry.AddParameter("@summary", FwConvert.LogicalToCharacter(summary));
                qry.AddParameter("@warehouseid", warehouseid);
                dt = qry.QueryToFwJsonTable();
            }
            int col_barcode = dt.ColumnIndex["barcode"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string barcode = dt.Rows[i][col_barcode].ToString();
                using (FwSqlCommand sp = new FwSqlCommand(conn, "dbo.advancedmovebarcode"))
                {
                    sp.AddParameter("@orderid", orderid);
                    sp.AddParameter("@barcode", barcode);
                    sp.AddParameter("@contractid", contractid);
                    sp.AddParameter("@usersid", usersid);
                    sp.AddParameter("@movemode", movemode);
                    sp.AddParameter("@status", SqlDbType.Decimal, ParameterDirection.Output);
                    sp.AddParameter("@msg", SqlDbType.VarChar, ParameterDirection.Output);
                    sp.Execute();
                    //response.status = sp.GetParameter("@status").ToDecimal();
                    //response.msg = sp.GetParameter("@msg").ToString();
                }
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
