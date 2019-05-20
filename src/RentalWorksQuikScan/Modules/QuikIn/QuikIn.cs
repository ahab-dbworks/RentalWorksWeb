using Fw.Json.Services.Common;
using Fw.Json.SqlServer;
using RentalWorksQuikScan.Source;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;

namespace RentalWorksQuikScan.Modules
{
    public class QuikIn
    {
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public void QuikInSessionSearch(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = FwSqlConnection.RentalWorks)
            {
                using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
                {
                    var userLocation = RwAppData.GetUserLocation(FwSqlConnection.RentalWorks, session.security.webUser.usersid);
                    FwSqlSelect select = new FwSqlSelect();
                    select.PageNo = request.pageno;
                    select.PageSize = request.pagesize;
                    select.Add("select contractid, sessionno, deal, username, status, statusdate");
                    select.Add("from suspendview");
                    select.Add("where contracttype = 'QUIKIN'");
                    select.Add("  and warehouseid = @warehouseid");
                    select.Add("order by sessionno desc");
                    select.AddParameter("@warehouseid", userLocation.warehouseId);
                    response.searchresults = qry.QueryToFwJsonTable(select, true);
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "sessionno")]
        public void PdaSelectSession(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = FwSqlConnection.RentalWorks)
            {
                using (FwSqlCommand sp = new FwSqlCommand(conn, "dbo.pdaselectsession"))
                {
                    sp.AddParameter("@sessionno", request.sessionno);
                    sp.AddParameter("@moduletype", "Q");
                    sp.AddParameter("@usersid", RwAppData.GetUsersId(session));
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
                    sp.Execute();
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
        public void PdaQuikInItem(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = FwSqlConnection.RentalWorks)
            {
                using (FwSqlCommand sp = new FwSqlCommand(conn, "dbo.pdaquikinitem"))
                {
                    sp.AddParameter("@code", request.code);
                    sp.AddParameter("@usersid", RwAppData.GetUsersId(session));
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
                    sp.Execute();
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
        public void QuikInAddItem(dynamic request, dynamic response, dynamic session)
        {
            string inContractId = request.contractId;
            int qty = request.qty;
            string usersid = RwAppData.GetUsersId(session);
            const string ACTION_PROMPT_FOR_QTY = "ACTION_PROMPT_FOR_QTY";
            const string ACTION_DISPLAY_ERROR = "ACTION_DISPLAY_ERROR";
            const string ACTION_QUIKINADDITEM = "ACTION_QUIKINADDITEM";
            using (FwSqlConnection conn = FwSqlConnection.RentalWorks)
            {
                if (string.IsNullOrEmpty(inContractId))
                {
                    response.action = ACTION_DISPLAY_ERROR;
                    response.status = 1020;
                    response.msg = "Invalid Session";
                    return;
                }
                using (FwSqlCommand qry = new FwSqlCommand(conn))
                {
                    qry.Add("select contractby = dbo.getcontractcancelledby(@incontractid)");
                    qry.AddParameter("@incontractid", inContractId);
                    qry.Execute();
                    string contractby = qry.GetField("contractby").ToString().TrimEnd();
                    if (!string.IsNullOrEmpty(contractby))
                    {
                        response.action = ACTION_DISPLAY_ERROR;
                        response.status = 1020;
                        response.msg = "Session Cancelled By " + contractby;
                        return;
                    }
                }
                using (FwSqlCommand qry = new FwSqlCommand(conn))
                {
                    qry.Add("select contractby = dbo.getcontractcreatedby(@incontractid)");
                    qry.AddParameter("@incontractid", inContractId);
                    qry.Execute();
                    string contractby = qry.GetField("contractby").ToString().TrimEnd();
                    if (!string.IsNullOrEmpty(contractby))
                    {
                        response.action = ACTION_DISPLAY_ERROR;
                        response.status = 1021;
                        response.msg = "A Contract for this session was already created by " + contractby;
                        return;
                    }
                }
                dynamic itemInfo = RwAppData.WebGetItemStatus(conn: FwSqlConnection.RentalWorks,
                                                              usersId: usersid,
                                                              barcode: request.code);
                string trackedby = itemInfo.trackedby;
                string code = request.code;
                string description = itemInfo.description;
                string masterno = itemInfo.masterNo;
                string internalchar = itemInfo.internalchar;
                string masterid = itemInfo.masterId;
                string rentalitemid = itemInfo.rentalitemid;
                string itemclass = itemInfo.itemclass;
                string vendorid = itemInfo.vendorid;
                string orderby = string.Empty;
                string outputmasterid = string.Empty;

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
                    else if (trackedby == "BARCODE" || trackedby == "SERIALNO")
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
                using (FwSqlCommand sp = new FwSqlCommand(conn, "dbo.quikinadditem"))
                {
                    sp.AddParameter("@code", code);
                    sp.AddParameter("@contractid", request.contractId);
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
                    sp.Execute();
                    response.action = ACTION_QUIKINADDITEM;
                    response.status = sp.GetParameter("@status").ToInt32();
                    response.msg = sp.GetParameter("@msg").ToString().TrimEnd();
                }

                List<dynamic> orders;
                using (FwSqlCommand qry = new FwSqlCommand(conn))
                {
                    qry.Add("select orderid");
                    qry.Add("from checkinorderpriorityview op with (nolock)");
                    qry.Add("where op.contractid = @contractid");
                    qry.AddParameter("@contractid", inContractId);
                    orders = qry.QueryToDynamicList2();
                }
                using (FwSqlCommand qry = new FwSqlCommand(conn))
                {
                    qry.Add(";with QuikInOrderStatus_CTE(qtyordered, stilloutqty, inqty)");
                    qry.Add("as");
                    qry.Add("(");
                    for  (int i = 0; i < orders.Count; i++)
                    {
                        string orderid = orders[i].orderid;
                        if (i > 0)
                        {
                            qry.Add("union");
                        }
                        qry.Add("select qtyordered, stilloutqty, inqty");
                        qry.Add($"from dbo.getorderstatussummary(@orderid{i})");
                        qry.Add("where masterid = @masterid");
                        qry.AddParameter($"@orderid{i}", orderid);
                       
                    }
                    if (orders.Count == 0)
                    {
                        qry.Add("select qtyordered=0, stilloutqty=0, inqty=0");
                    }
                    qry.Add(")");
                    qry.Add("select top 1");
                    qry.Add("  qtyordered  = sum(os.qtyordered),");
                    qry.Add("  stilloutqty = sum(os.stilloutqty),");
                    qry.Add("  inqty       = sum(os.inqty),");
                    qry.Add("  sessionin   = (select top 1 sum(counted) from dbo.funcqiitem(@contractid) where masterid = @masterid)");
                    qry.Add("from QuikInOrderStatus_CTE os");
                    qry.AddParameter("@masterid", masterid);
                    qry.AddParameter("@contractid", inContractId);
                    qry.Execute();
                    response.qtyOrdered = qry.GetField("qtyordered").ToInt32();
                    response.stillOut = qry.GetField("stilloutqty").ToInt32();
                    response.inQty = qry.GetField("inqty").ToInt32();
                    response.sessionIn = qry.GetField("sessionin").ToInt32();
                }
            }
        }
        //----------------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "contractId")]
        public static void SessionInSearch(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.PageNo = request.pageno;
                select.PageSize = request.pagesize;
                select.Add("select *");
                select.Add("from dbo.funcqiitem(@contractid)");
                select.Add("order by scannedbydatetime desc");
                select.AddParameter("@contractid", request.contractId);
                response.searchresults = qry.QueryToFwJsonTable(select, true);
            }
        }
        //----------------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "internalchar,quikinitemid")]
        public static void CancelItem(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                qry.Add("delete");
                qry.Add("from quikinitem");
                qry.Add("where internalchar = @internalchar and quikinitemid = @quikinitemid");
                qry.AddParameter("@internalchar", request.internalchar);
                qry.AddParameter("@quikinitemid", request.quikinitemid);
                qry.ExecuteNonQuery();
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}