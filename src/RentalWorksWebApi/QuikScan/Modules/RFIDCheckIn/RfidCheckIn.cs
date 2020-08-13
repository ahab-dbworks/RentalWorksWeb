using FwStandard.Mobile;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.Utilities;
using RentalWorksQuikScan.Source;
using System.Data;
using System.Threading.Tasks;
using WebApi.QuikScan;

namespace RentalWorksQuikScan.Modules
{
    public class RfidCheckIn : MobileModule
    {
        RwAppData AppData;
        //----------------------------------------------------------------------------------------------------
        public RfidCheckIn(FwApplicationConfig applicationConfig) : base(applicationConfig)
        {
            this.AppData = new RwAppData(applicationConfig);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task ProcessBatch(dynamic request, dynamic response, dynamic session)
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
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                await conn.OpenAsync();
                // log the batch of tags in the scannedtag table
                using (FwSqlCommand sp = new FwSqlCommand(conn, "logrfidtags", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    sp.AddParameter("@sessionid", orderid);
                    sp.AddParameter("@portal", portal);
                    sp.AddParameter("@tags", tags);
                    sp.AddParameter("@rentalitemid", rentalitemid);
                    sp.AddParameter("@usersid", usersid);
                    sp.AddParameter("@batchid", SqlDbType.VarChar, ParameterDirection.Output);
                    await sp.ExecuteAsync();
                    batchid = sp.GetParameter("@batchid").ToString();
                }

                // process the batch in the scannedtag table
                using (FwSqlCommand sp = new FwSqlCommand(conn, "processscannedtags", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    sp.AddParameter("@sessionid", orderid);
                    sp.AddParameter("@portal", portal);
                    sp.AddParameter("@batchid", batchid);
                    sp.AddParameter("@usersid", usersid);
                    sp.AddParameter("@rfidmode", rfidmode);
                    await sp.ExecuteAsync();
                }
                conn.CommitTransaction();
                conn.Close();
            }
            response.batchid = batchid;
            response.funcscannedtag = await GetProcessedAsync(orderid, usersid, portal, batchid);
            response.funcscannedtagexception = await GetExceptionsAsync(orderid, usersid, portal);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task GetExceptions(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "RfidCheckIn.GetExceptions";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "portal");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "batchid");
            string orderid = request.orderid;
            string portal  = request.portal;
            string batchid = request.batchid;
            string usersid = session.security.webUser.usersid;
            response.funcscannedtag = await GetProcessedAsync(orderid, usersid, portal, batchid);
            response.funcscannedtagexception = await GetExceptionsAsync(orderid, usersid, portal);
        }
        //---------------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> GetProcessedAsync(string orderid, string usersid, string portal, string batchid)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                FwJsonDataTable dt;
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select tag, master, status");
                    qry.AddColumn("tag", false, FwDataTypes.Text);
                    qry.AddColumn("master", false, FwDataTypes.Text);
                    qry.AddColumn("status", false, FwDataTypes.Text);
                    qry.Add("from funcscannedtag(@sessionid, @orderid, @usersid, @portal, @batchid, @rfidmode)");
                    qry.AddParameter("@sessionid", orderid);
                    qry.AddParameter("@orderid", "");
                    qry.AddParameter("@usersid", usersid);
                    qry.AddParameter("@portal", portal);
                    qry.AddParameter("@batchid", batchid);
                    qry.AddParameter("@rfidmode", "CHECKIN");
                    dt = await qry.QueryToFwJsonTableAsync();
                }
                return dt; 
            }
        }
        //---------------------------------------------------------------------------------------------
        private async Task<FwJsonDataTable> GetExceptionsAsync(string contractid, string usersid, string portal)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                FwJsonDataTable dt;
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select tag, master, message, exceptiontype");
                    qry.AddColumn("tag", false, FwDataTypes.Text);
                    qry.AddColumn("master", false, FwDataTypes.Text);
                    qry.AddColumn("message", false, FwDataTypes.Text);
                    qry.AddColumn("exceptiontype", false, FwDataTypes.Text);
                    qry.Add("from funcscannedtagexception(@sessionid, @usersid, @portal)");
                    qry.AddParameter("@sessionid", contractid);
                    qry.AddParameter("@usersid", usersid);
                    qry.AddParameter("@portal", portal);
                    dt = await qry.QueryToFwJsonTableAsync();
                }
                return dt; 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task ClearException(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "RfidCheckIn.ReleaseFromRepair";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "portal");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "tag");
            string orderid = request.orderid;
            string portal  = request.portal;
            string usersid = session.security.webUser.usersid;
            string tag     = request.tag;
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand sp = new FwSqlCommand(conn, "scannedtagclearexception", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    sp.AddParameter("@sessionid", orderid);
                    sp.AddParameter("@tag", tag);
                    await sp.ExecuteAsync();
                } 
            }
            response.status = 0;
            response.message = "";
            //response.funcscannedtagexception = LoadExceptions(orderid, usersid, portal);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task ClearAllExceptions(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "RfidCheckIn.ClearAllExceptions";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "portal");
            string orderid = request.orderid;
            string portal  = request.portal;
            string usersid = session.security.webUser.usersid;
            FwJsonDataTable dt = await GetExceptionsAsync(orderid, usersid, portal);
            int col_tag = dt.ColumnIndex["tag"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string tag     = dt.Rows[i][col_tag].ToString();
                using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
                {
                    using (FwSqlCommand sp = new FwSqlCommand(conn, "scannedtagclearexception", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                    {
                        sp.AddParameter("@sessionid", orderid);
                        sp.AddParameter("@tag", tag);
                        await sp.ExecuteAsync();
                    } 
                }
            }
            response.funcscannedtagexception = GetExceptions(orderid, usersid, portal);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task GetPendingItems(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "RfidCheckIn.GetPendingItems";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
            string contractid      = request.contractid;
            string rectype         = request.rectype;
            string containeritemid = request.containeritemid;
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                session.userLocation = await this.AppData.GetUserLocationAsync(conn: conn,
                                                                           usersId: session.security.webUser.usersid);
                string warehouseid = session.userLocation.warehouseId;
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddColumn("orderid", false, FwDataTypes.Text);
                    qry.AddColumn("masterid", false, FwDataTypes.Text);
                    qry.AddColumn("parentid", false, FwDataTypes.Text);
                    qry.AddColumn("masteritemid", false, FwDataTypes.Text);
                    qry.AddColumn("exceptionflg", false, FwDataTypes.Boolean);
                    qry.AddColumn("somein", false, FwDataTypes.Text);
                    qry.AddColumn("masterno", false, FwDataTypes.Text);
                    qry.AddColumn("description", false, FwDataTypes.Text);
                    qry.AddColumn("vendor", false, FwDataTypes.Text);
                    //qry.AddColumn("vendorid",           false, FwDataTypes.Text);
                    qry.AddColumn("qtyordered", false, FwDataTypes.Decimal);
                    qry.AddColumn("qtystagedandout", false, FwDataTypes.Decimal);
                    qry.AddColumn("qtyout", false, FwDataTypes.Decimal);
                    qry.AddColumn("qtysub", false, FwDataTypes.Decimal);
                    qry.AddColumn("qtysubstagedandout", false, FwDataTypes.Decimal);
                    qry.AddColumn("qtyin", false, FwDataTypes.Decimal);
                    qry.AddColumn("qtysubout", false, FwDataTypes.Decimal);
                    qry.AddColumn("qtystillout", false, FwDataTypes.Decimal);
                    qry.AddColumn("missingflg", false, FwDataTypes.Boolean);
                    qry.AddColumn("missingqty", false, FwDataTypes.Decimal);
                    qry.AddColumn("trackedby", false, FwDataTypes.Text);
                    qry.AddColumn("rectype", false, FwDataTypes.Text);
                    qry.AddColumn("itemclass", false, FwDataTypes.Text);
                    qry.AddColumn("itemorder", false, FwDataTypes.Text);
                    qry.AddColumn("orderby", false, FwDataTypes.Text);
                    qry.AddColumn("optioncolor", false, FwDataTypes.Text);
                    qry.AddColumn("warehouseid", false, FwDataTypes.Text);
                    qry.AddColumn("whcode", false, FwDataTypes.Decimal);
                    qry.AddColumn("orderno", false, FwDataTypes.Text);
                    qry.AddColumn("isbarcode", false, FwDataTypes.Boolean);
                    qry.AddColumn("contractid", false, FwDataTypes.Text);
                    qry.AddColumn("subbyquantity", false, FwDataTypes.Boolean);
                    qry.AddColumn("nestedmasteritemid", false, FwDataTypes.Text);
                    qry.Add("select *");
                    qry.Add("  from dbo.funccheckinexception(@contractid, @rectype, @containeritemid, 'T')");
                    qry.Add(" where exceptionflg = 'T'");
                    qry.Add("order by orderno, itemorder, masterno");
                    qry.AddParameter("@contractid", contractid);
                    qry.AddParameter("@rectype", rectype);
                    qry.AddParameter("@containeritemid", containeritemid);
                    response.funccheckoutexception = await qry.QueryToFwJsonTableAsync(true);
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task GetSessionItems(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "RfidCheckIn.GetSessionItems";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
            string orderid = request.orderid;

            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                session.userLocation = await this.AppData.GetUserLocationAsync(conn: conn,
                                                                         usersId: session.security.webUser.usersid);
                bool summary = false;
                string warehouseid = session.userLocation.warehouseId;
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddColumn("rectype", false, FwDataTypes.Text);
                    qry.AddColumn("masteritemid", false, FwDataTypes.Text);
                    qry.AddColumn("description", false, FwDataTypes.Text);
                    qry.AddColumn("masterno", false, FwDataTypes.Text);
                    qry.AddColumn("barcode", false, FwDataTypes.Text);
                    qry.AddColumn("quantity", false, FwDataTypes.Decimal);
                    qry.AddColumn("vendorid", false, FwDataTypes.Text);
                    qry.AddColumn("vendor", false, FwDataTypes.Text);
                    qry.AddColumn("itemclass", false, FwDataTypes.Text);
                    qry.AddColumn("trackedby", false, FwDataTypes.Text);
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
                    response.funcstageditemsweb = await qry.QueryToFwJsonTableAsync();
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task UnstageItem(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "RfidCheckIn.GetStagedItems";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
            string orderid = request.orderid;
            string barcode = request.barcode;
            string contractid = "";
            string usersid = session.security.webUser.usersid;
            int movemode = 2; //staged to inventory

            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand sp = new FwSqlCommand(conn, "dbo.advancedmovebarcode", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    sp.AddParameter("@orderid", orderid);
                    sp.AddParameter("@barcode", barcode);
                    sp.AddParameter("@contractid", contractid);
                    sp.AddParameter("@usersid", usersid);
                    sp.AddParameter("@movemode", movemode);
                    sp.AddParameter("@status", SqlDbType.Decimal, ParameterDirection.Output);
                    sp.AddParameter("@msg", SqlDbType.VarChar, ParameterDirection.Output);
                    await sp.ExecuteAsync();
                    response.status = sp.GetParameter("@status").ToDecimal();
                    response.msg = sp.GetParameter("@msg").ToString();
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
