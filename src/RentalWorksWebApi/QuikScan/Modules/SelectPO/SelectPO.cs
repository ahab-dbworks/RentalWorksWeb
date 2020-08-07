using FwStandard.Mobile;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.Utilities;
using RentalWorksQuikScan.Source;
using System;
using System.Dynamic;
using System.Threading.Tasks;
using WebApi.QuikScan;

namespace RentalWorksQuikScan.Modules
{
    public class SelectPO : QuikScanModule
    {
        RwAppData AppData;
        //----------------------------------------------------------------------------------------------------
        public SelectPO(FwApplicationConfig applicationConfig) : base(applicationConfig)
        {
            this.AppData = new RwAppData(applicationConfig);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
       public async Task GetPurchaseOrders(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "SelectPO.GetPurchaseOrders";
            string usersid, moduletype;
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "pageno");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "pagesize");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "moduletype");

            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                usersid = session.security.webUser.usersid;
                var userLocation = await this.AppData.GetUserLocationAsync(conn, usersid);
                moduletype = request.moduletype;
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddColumn("PO No", "orderno", FwDataTypes.Text, false);
                    qry.AddColumn("Desc", "orderdesc", FwDataTypes.Text, false);
                    qry.AddColumn("Date", "orderdate", FwDataTypes.Date, false);
                    qry.AddColumn("Deal", "deal", FwDataTypes.Text, false);
                    qry.AddColumn("Status", "status", FwDataTypes.Text, false);
                    qry.AddColumn("As Of", "statusdate", FwDataTypes.Date, false);
                    switch (moduletype)
                    {
                        case "SubReceive":
                            qry.Add("select top 1000 orderno, orderdesc, orderdate, deal, status, statusdate");
                            qry.Add("from poview o with (nolock)");
                            qry.Add("where o.status in ('NEW','OPEN')");
                            qry.Add("  and o.orderno > ''");
                            qry.Add("  and (");
                            qry.Add("       o.subrent     = 'T'");
                            qry.Add("    or o.subsale     = 'T'");
                            //qry.Add("    or o.parts       = 'T'");
                            //qry.Add("    or o.sales       = 'T'");
                            //qry.Add("    or o.rental      = 'T'");
                            //qry.Add("    or o.consignment = 'T'");
                            qry.Add("  )");
                            qry.Add("  and exists (select *");
                            qry.Add("              from  masteritem mi with (nolock)");
                            qry.Add("              where mi.orderid     = o.orderid");
                            qry.Add("              and   mi.warehouseid = @warehouseid");
                            qry.Add("             )");
                            qry.Add("order by orderdate desc, orderno desc");
                            qry.AddParameter("@locationid", userLocation.locationId);
                            qry.AddParameter("@warehouseid", userLocation.warehouseId);
                            break;
                        case "SubReturn":
                            qry.Add("select top 1000 orderno, orderdesc, orderdate, deal, status, statusdate");
                            qry.Add("from poview o with (nolock)");
                            qry.Add("where o.status in ('OPEN','RECEIVED','COMPLETE')");
                            //qry.Add("  and o.locationid = @locationid");
                            qry.Add("  and o.orderno > ''");
                            qry.Add("  and (");
                            qry.Add("         o.subrent     = 'T'");
                            qry.Add("      or o.subsale     = 'T'");
                            //qry.Add("      or o.parts       = 'T'");
                            //qry.Add("      or o.sales       = 'T'");
                            //qry.Add("      or o.rental      = 'T'");
                            //qry.Add("      or o.consignment = 'T'");
                            qry.Add("  )");
                            qry.Add("order by orderdate desc, orderno desc");
                            qry.AddParameter("@locationid", userLocation.locationId);
                            break;
                    }
                    response.datatable = await qry.QueryToFwJsonTableAsync(true, request.pageno, request.pagesize);
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task WebSelectPO(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "WebSelectPO";
            string usersid, pono, poid;
            RwAppData.ModuleType moduleType;
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "poNo");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "moduleType");
            moduleType = (RwAppData.ModuleType)Enum.Parse(typeof(RwAppData.ModuleType), request.moduleType);
            usersid    = session.security.webUser.usersid;
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                var userLocation = await this.AppData.GetUserLocationAsync(conn, usersid);
                string locationid = userLocation.locationId;

                pono = request.poNo;
                response.webSelectPO = await this.AppData.WebSelectPOAsync(conn: conn
                                                           , usersId: usersid
                                                           , poNo: pono
                                                           , moduleType: moduleType);
                if (response.webSelectPO.status == 0)
                {
                    FwJsonDataTable dt = await this.AppData.GetDealOrderByOrderNoAsync(conn: conn
                                                                       , orderNo: pono);
                    poid = dt.Rows[0][dt.ColumnIndex["orderid"]].ToString().TrimEnd();
                    if (dt.Rows.Count > 0)
                    {
                        if (FwValidate.IsPropertyDefined(request, "sessionNo"))
                        {
                            response.contract = new ExpandoObject();
                            response.contract.contractId = request.contractId;
                            response.contract.sessionNo = request.sessionNo;
                        }
                        else
                        {
                            FwJsonDataTable dtSuspendedContracts = null;
                            switch (moduleType)
                            {
                                case RwAppData.ModuleType.SubReceive:
                                    bool createreceivecontract = request.forcecreatecontract;
                                    if (!createreceivecontract)
                                    {
                                        using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                                        {
                                            qry.Add("select *");
                                            qry.Add("from suspendview v with (nolock)");
                                            qry.Add("where v.contracttype = 'RECEIVE'");
                                            qry.Add("  and v.locationid = @locationid");
                                            qry.Add("  and v.orderno = @orderno");
                                            qry.Add("order by statusdate desc");
                                            qry.AddParameter("@locationid", locationid);
                                            qry.AddParameter("@orderno", pono);
                                            dtSuspendedContracts = await qry.QueryToFwJsonTableAsync(true);
                                        }
                                        if (dtSuspendedContracts.Rows.Count > 0)
                                        {
                                            response.suspendedContracts = dtSuspendedContracts;
                                        }
                                        else
                                        {
                                            createreceivecontract = true;
                                        }
                                    }
                                    if (createreceivecontract)
                                    {
                                        response.contract = await this.AppData.CreateReceiveContractAsync(conn: conn,
                                                                                            poid: poid,
                                                                                            usersid: usersid);
                                    }
                                    break;
                                case RwAppData.ModuleType.SubReturn:
                                    bool createreturncontract = request.forcecreatecontract;
                                    if (!createreturncontract)
                                    {
                                        using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                                        {
                                            qry.Add("select *");
                                            qry.Add("from suspendview v with (nolock)");
                                            qry.Add("where v.contracttype = 'RETURN'");
                                            qry.Add("  and v.locationid = @locationid");
                                            qry.Add("  and v.orderno = @orderno");
                                            qry.Add("order by statusdate desc");
                                            qry.AddParameter("@locationid", locationid);
                                            qry.AddParameter("@orderno", pono);
                                            dtSuspendedContracts = await qry.QueryToFwJsonTableAsync(true);
                                        }
                                        if (dtSuspendedContracts.Rows.Count > 0)
                                        {
                                            response.suspendedContracts = dtSuspendedContracts;
                                        }
                                        else
                                        {
                                            createreturncontract = true;
                                        }
                                    }
                                    if (createreturncontract)
                                    {
                                        response.contract = await this.AppData.CreateReturnContractAsync(conn: conn,
                                                                                           poid: poid,
                                                                                           usersid: usersid);
                                    }
                                    break;
                            }
                        }
                    }
                    else
                    {
                        throw new Exception(METHOD_NAME + ": Can't find PO.");
                    }
                } 
            }
        }
        //----------------------------------------------------------------------------------------------------
    }
}
