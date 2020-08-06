using FwStandard.Mobile;
using FwStandard.Models;
using FwStandard.SqlServer;
using RentalWorksQuikScan.Source;
using System.Data;
using System.Threading.Tasks;
using WebApi.QuikScan;

namespace RentalWorksQuikScan.Modules
{
    public class QuikIn : QuikScanModule
    {
        RwAppData AppData;
        //----------------------------------------------------------------------------------------------------
        public QuikIn(FwApplicationConfig applicationConfig) : base(applicationConfig)
        {
            this.AppData = new RwAppData(applicationConfig);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task QuikInSessionSearch(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    var userLocation = await this.AppData.GetUserLocationAsync(conn, session.security.webUser.usersid);
                    FwSqlSelect select = new FwSqlSelect();
                    select.PageNo = request.pageno;
                    select.PageSize = request.pagesize;
                    select.Add("select contractid, sessionno, deal, username, status, statusdate");
                    select.Add("from suspendview");
                    select.Add("where contracttype = 'QUIKIN'");
                    select.Add("  and warehouseid = @warehouseid");
                    select.Add("order by sessionno desc");
                    select.AddParameter("@warehouseid", userLocation.warehouseId);
                    response.searchresults = await qry.QueryToFwJsonTableAsync(select, true);
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "sessionno")]
        public async Task PdaSelectSession(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand sp = new FwSqlCommand(conn, "dbo.pdaselectsession", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    sp.AddParameter("@sessionno", request.sessionno);
                    sp.AddParameter("@moduletype", "Q");
                    sp.AddParameter("@usersid", this.AppData.GetUsersId(session));
                    sp.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Output, 08);
                    sp.AddParameter("@orderid", SqlDbType.NVarChar, ParameterDirection.Output, 08);
                    sp.AddParameter("@dealid", SqlDbType.NVarChar, ParameterDirection.Output, 08);
                    sp.AddParameter("@departmentid", SqlDbType.NVarChar, ParameterDirection.Output, 08);
                    sp.AddParameter("@orderno", SqlDbType.NVarChar, ParameterDirection.Output, 255);
                    sp.AddParameter("@orderdesc", SqlDbType.NVarChar, ParameterDirection.Output, 255);
                    sp.AddParameter("@dealno", SqlDbType.NVarChar, ParameterDirection.Output, 255);
                    sp.AddParameter("@deal", SqlDbType.NVarChar, ParameterDirection.Output, 255);
                    sp.AddParameter("@department", SqlDbType.NVarChar, ParameterDirection.Output, 255);
                    sp.AddParameter("@warehouseid", SqlDbType.NVarChar, ParameterDirection.Output, 08);
                    sp.AddParameter("@warehouse", SqlDbType.NVarChar, ParameterDirection.Output, 255);
                    sp.AddParameter("@fromwarehouse", SqlDbType.NVarChar, ParameterDirection.Output, 255);
                    sp.AddParameter("@username", SqlDbType.NVarChar, ParameterDirection.Output, 255);
                    sp.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output, 64);
                    sp.AddParameter("@msg", SqlDbType.NVarChar, ParameterDirection.Output, 255);
                    await sp.ExecuteAsync();
                    response.contractId = sp.GetParameter("@contractid").ToString();
                    response.orderId = sp.GetParameter("@orderid").ToString();
                    response.dealId = sp.GetParameter("@dealid").ToString();
                    response.departmentId = sp.GetParameter("@departmentid").ToString();
                    response.orderNo = sp.GetParameter("@orderno").ToString();
                    response.orderDesc = sp.GetParameter("@orderdesc").ToString();
                    response.dealNo = sp.GetParameter("@dealno").ToString();
                    response.deal = sp.GetParameter("@deal").ToString();
                    response.warehouse = sp.GetParameter("@warehouse").ToString();
                    response.fromWarehouse = sp.GetParameter("@fromwarehouse").ToString();
                    response.userName = sp.GetParameter("@username").ToString();
                    response.status = sp.GetParameter("@status").ToInt32();
                    response.msg = sp.GetParameter("@msg").ToString();
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "code,contractId")]
        public async Task PdaQuikInItem(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand sp = new FwSqlCommand(conn, "dbo.pdaquikinitem", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    sp.AddParameter("@code", request.code);
                    sp.AddParameter("@usersid", this.AppData.GetUsersId(session));
                    sp.AddParameter("@incontractid", SqlDbType.Char, ParameterDirection.InputOutput, request.contractId);
                    sp.AddParameter("@orderno", SqlDbType.VarChar, ParameterDirection.Output, 255);
                    sp.AddParameter("@masterno", SqlDbType.VarChar, ParameterDirection.Output, 255);
                    sp.AddParameter("@description", SqlDbType.VarChar, ParameterDirection.Output, 255);
                    sp.AddParameter("@qtyordered", SqlDbType.Int, ParameterDirection.Output);
                    sp.AddParameter("@subqty", SqlDbType.Int, ParameterDirection.Output);
                    sp.AddParameter("@stillout", SqlDbType.Int, ParameterDirection.Output);
                    sp.AddParameter("@totalin", SqlDbType.Int, ParameterDirection.Output);
                    sp.AddParameter("@sessionin", SqlDbType.Int, ParameterDirection.Output);
                    sp.AddParameter("@aisleloc", SqlDbType.VarChar, ParameterDirection.Output, 255);
                    sp.AddParameter("@shelfloc", SqlDbType.VarChar, ParameterDirection.Output, 255);
                    sp.AddParameter("@genericmsg", SqlDbType.VarChar, ParameterDirection.Output, 255);
                    sp.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                    sp.AddParameter("@msg", SqlDbType.VarChar, ParameterDirection.Output, 255);
                    await sp.ExecuteNonQueryAsync();
                    response.contractId = sp.GetParameter("@incontractid").ToString().Trim();
                    response.orderNo = sp.GetParameter("@orderno").ToString().Trim();
                    response.masterNo = sp.GetParameter("@masterno").ToString().Trim();
                    response.description = sp.GetParameter("@description").ToString().Trim();
                    response.qtyOrdered = sp.GetParameter("@qtyordered").ToInt32();
                    response.subQty = sp.GetParameter("@subqty").ToInt32();
                    response.stillOut = sp.GetParameter("@stillout").ToInt32();
                    response.totalIn = sp.GetParameter("@totalin").ToInt32();
                    response.sessionIn = sp.GetParameter("@sessionin").ToInt32();
                    response.aisle = sp.GetParameter("@aisleloc").ToString().Trim();
                    response.shelf = sp.GetParameter("@shelfloc").ToString().Trim();
                    response.genericMsg = sp.GetParameter("@genericmsg").ToString().Trim();
                    response.status = sp.GetParameter("@status").ToInt32();
                    response.msg = sp.GetParameter("@msg").ToString().Trim();
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "code,contractId,qty")]
        public async Task QuikInAddItem(dynamic request, dynamic response, dynamic session)
        {
            string inContractId = request.contractId;
            int qty = request.qty;
            string usersid = this.AppData.GetUsersId(session);
            const string ACTION_PROMPT_FOR_QTY = "ACTION_PROMPT_FOR_QTY";
            const string ACTION_DISPLAY_ERROR = "ACTION_DISPLAY_ERROR";
            const string ACTION_QUIKINADDITEM = "ACTION_QUIKINADDITEM";
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                if (string.IsNullOrEmpty(inContractId))
                {
                    response.action = ACTION_DISPLAY_ERROR;
                    response.status = 1020;
                    response.msg = "Invalid Session";
                    return;
                }
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select contractby = dbo.getcontractcancelledby(@incontractid)");
                    qry.AddParameter("@incontractid", inContractId);
                    await qry.ExecuteAsync();
                    string contractby = qry.GetField("contractby").ToString().TrimEnd();
                    if (!string.IsNullOrEmpty(contractby))
                    {
                        response.action = ACTION_DISPLAY_ERROR;
                        response.status = 1020;
                        response.msg = "Session Cancelled By " + contractby;
                        return;
                    }
                }
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select contractby = dbo.getcontractcreatedby(@incontractid)");
                    qry.AddParameter("@incontractid", inContractId);
                    await qry.ExecuteAsync();
                    string contractby = qry.GetField("contractby").ToString().TrimEnd();
                    if (!string.IsNullOrEmpty(contractby))
                    {
                        response.action = ACTION_DISPLAY_ERROR;
                        response.status = 1021;
                        response.msg = "A Contract for this session was already created by " + contractby;
                        return;
                    }
                }
                dynamic itemInfo = await this.AppData.WebGetItemStatusAsync(conn: conn,
                                                              usersId: usersid,
                                                              barcode: request.code);
                string trackedby = itemInfo.trackedby;
                string code = string.Empty;
                string description = string.Empty;
                string masterno = string.Empty;
                string internalchar = string.Empty;
                string masterid = string.Empty;
                string rentalitemid = string.Empty;
                string itemclass = string.Empty;
                string vendorid = string.Empty;
                string orderby = string.Empty;
                string outputmasterid = string.Empty;
                if (trackedby == "BARCODE" || trackedby == "SERIALNO" || trackedby == "RFID")
                {
                    code = request.code;
                    internalchar = itemInfo.internalchar;
                }
                else 
                if (trackedby == "QUANTITY")
                {
                    //using (FwSqlCommand qry = new FwSqlCommand(conn))
                    //{
                    //    qry.Add("select top 1 *");
                    //    qry.Add("from dbo.funcqiquantity(@contractid, @dealid, @departmentid, @rectype, @groupitems @warehouseid)");
                    //    qry.Add("where rectype = 'R'");
                    //    qry.Add("  and masterid = @masterid");
                    //    qry.Add("order by itemclassorderby, orderby, masterno, description, vendor");
                    //    qry.AddParameter("@contractid", inContractId);
                    //    qry.AddParameter("@dealid", );
                    //    qry.AddParameter("@departmentid", );
                    //    qry.AddParameter("@rectype", );
                    //    qry.AddParameter("@groupitems", );
                    //    qry.AddParameter("@warehouseid", );
                    //}
                    code = string.Empty;
                    description = itemInfo.description;
                    masterno = itemInfo.masterNo;
                    internalchar = itemInfo.internalchar;
                    masterid = itemInfo.masterId;
                    rentalitemid = itemInfo.rentalitemid;
                    itemclass = itemInfo.itemclass;
                    vendorid = itemInfo.vendorid;
                }

                response.status = itemInfo.status;
                response.msg = itemInfo.msg;
                response.trackedby = trackedby;
                if (!string.IsNullOrEmpty(description))
                {
                    response.description = description;
                }
                if (!string.IsNullOrEmpty(masterno))
                {
                    response.masterno = masterno;
                }
                if (itemInfo.status == 301 && string.IsNullOrEmpty(itemInfo.trackedby))
                {
                    response.action = ACTION_DISPLAY_ERROR;
                    response.msg = "INVALID BARCODE";
                    return;
                }
                if (qty == -1)
                {
                    if (trackedby == "QUANTITY")
                    {
                        response.action = ACTION_PROMPT_FOR_QTY;
                        return;
                    }
                    else if (trackedby == "BARCODE" || trackedby == "SERIALNO" || trackedby == "RFID")
                    {
                        qty = 1;
                    }
                    else
                    {
                        response.action = ACTION_DISPLAY_ERROR;
                        response.status = -1;
                        response.msg = $"QuikIn does not support items that are tracked by: '{trackedby}'";
                        return;
                    }
                }
                if (trackedby == "QUANTITY")
                {
                    code = string.Empty;
                    orderby = "999999";
                }
                using (FwSqlCommand sp = new FwSqlCommand(conn, "dbo.quikinadditem", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    sp.AddParameter("@code", code);
                    sp.AddParameter("@contractid", inContractId);
                    sp.AddParameter("@internalchar", internalchar);
                    sp.AddParameter("@usersid", usersid);
                    sp.AddParameter("@masterid", masterid);
                    sp.AddParameter("@rentalitemid", rentalitemid);
                    sp.AddParameter("@description", description);
                    sp.AddParameter("@itemclass", itemclass);
                    sp.AddParameter("@packageid", string.Empty);
                    sp.AddParameter("@packageitemid", string.Empty);
                    sp.AddParameter("@orderby", orderby);
                    sp.AddParameter("@vendorid", vendorid);
                    sp.AddParameter("@nestedmasteritemid", string.Empty);
                    sp.AddParameter("@qty", qty);
                    sp.AddParameter("@outputmasterid", SqlDbType.Char, ParameterDirection.Output, 8);
                    sp.AddParameter("@outputrentalitemid", SqlDbType.Char, ParameterDirection.Output, 8);
                    sp.AddParameter("@trackedby", SqlDbType.VarChar, ParameterDirection.Output, 255);
                    sp.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                    sp.AddParameter("@msg", SqlDbType.VarChar, ParameterDirection.Output, 255);
                    await sp.ExecuteAsync();
                    response.action = ACTION_QUIKINADDITEM;
                    response.status = sp.GetParameter("@status").ToInt32();
                    response.msg = sp.GetParameter("@msg").ToString().TrimEnd();
                }

                //List<dynamic> orders;
                //using (FwSqlCommand qry = new FwSqlCommand(conn))
                //{
                //    qry.Add("select orderid");
                //    qry.Add("from checkinorderpriorityview op with (nolock)");
                //    qry.Add("where op.contractid = @contractid");
                //    qry.AddParameter("@contractid", inContractId);
                //    orders = qry.QueryToDynamicList2();
                //}
                //using (FwSqlCommand qry = new FwSqlCommand(conn))
                //{
                //    qry.Add(";with QuikInOrderStatus_CTE(qtyordered, stilloutqty, inqty)");
                //    qry.Add("as");
                //    qry.Add("(");
                //    for  (int i = 0; i < orders.Count; i++)
                //    {
                //        string orderid = orders[i].orderid;
                //        if (i > 0)
                //        {
                //            qry.Add("union");
                //        }
                //        qry.Add("select qtyordered, stilloutqty, inqty");
                //        qry.Add($"from dbo.getorderstatussummary(@orderid{i})");
                //        qry.Add("where masterid = @masterid");
                //        qry.AddParameter($"@orderid{i}", orderid);
                       
                //    }
                //    if (orders.Count == 0)
                //    {
                //        qry.Add("select qtyordered=0, stilloutqty=0, inqty=0");
                //    }
                //    qry.Add(")");
                //    qry.Add("select top 1");
                //    qry.Add("  qtyordered  = sum(os.qtyordered),");
                //    qry.Add("  stilloutqty = sum(os.stilloutqty),");
                //    qry.Add("  inqty       = sum(os.inqty),");
                //    qry.Add("  sessionin   = (select top 1 sum(counted) from dbo.funcqiitem(@contractid) where masterid = @masterid)");
                //    qry.Add("from QuikInOrderStatus_CTE os");
                //    qry.AddParameter("@masterid", masterid);
                //    qry.AddParameter("@contractid", inContractId);
                //    qry.Execute();
                //    response.qtyOrdered = qry.GetField("qtyordered").ToInt32();
                //    response.stillOut = qry.GetField("stilloutqty").ToInt32();
                //    response.inQty = qry.GetField("inqty").ToInt32();
                //    response.sessionIn = qry.GetField("sessionin").ToInt32();
                //}
            }
        }
        //----------------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "contractId")]
        public async Task SessionInSearch(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    FwSqlSelect select = new FwSqlSelect();
                    select.PageNo = request.pageno;
                    select.PageSize = request.pagesize;
                    select.Add("select *");
                    select.Add("from dbo.funcqiitem(@contractid)");
                    select.Add("order by scannedbydatetime desc");
                    select.AddParameter("@contractid", request.contractId);
                    response.searchresults = await qry.QueryToFwJsonTableAsync(select, true);
                } 
            }
        }
        //----------------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "internalchar,quikinitemid")]
        public async Task CancelItem(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("delete");
                    qry.Add("from quikinitem");
                    qry.Add("where internalchar = @internalchar and quikinitemid = @quikinitemid");
                    qry.AddParameter("@internalchar", request.internalchar);
                    qry.AddParameter("@quikinitemid", request.quikinitemid);
                    await qry.ExecuteNonQueryAsync();
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}