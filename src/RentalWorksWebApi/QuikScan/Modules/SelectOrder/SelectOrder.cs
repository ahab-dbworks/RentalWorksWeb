using FwStandard.Mobile;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.Utilities;
using RentalWorksQuikScan.Source;
using System;
using System.Threading.Tasks;
using WebApi.QuikScan;

namespace RentalWorksQuikScan.Modules
{
    public class SelectOrder : QuikScanModule
    {
        RwAppData AppData;
        //----------------------------------------------------------------------------------------------------
        public SelectOrder(FwApplicationConfig applicationConfig) : base(applicationConfig)
        {
            this.AppData = new RwAppData(applicationConfig);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task WebSelectOrder(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "SelectOrder.WebSelectOrder";
            RwAppData.ActivityType activityType;
            RwAppData.ModuleType moduleType;
            FwJsonDataTable dtSuspendedInContracts;
            string usersid, orderno, orderid, dealid, departmentid;

            
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderNo");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "moduleType");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "activityType");

            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                usersid = session.security.webUser.usersid;
                orderno = request.orderNo;
                activityType = (RwAppData.ActivityType)Enum.Parse(typeof(RwAppData.ActivityType), request.activityType);
                moduleType = (RwAppData.ModuleType)Enum.Parse(typeof(RwAppData.ModuleType), request.moduleType);

                response.webSelectOrder = await this.AppData.WebSelectOrderAsync(conn, usersid, orderno, activityType, moduleType);
                orderid = response.webSelectOrder.orderId;
                dealid = response.webSelectOrder.dealId;
                departmentid = response.webSelectOrder.departmentId;
                switch (activityType)
                {
                    case RwAppData.ActivityType.CheckIn:
                        if (!string.IsNullOrEmpty(response.webSelectOrder.orderId))
                        {
                            switch (moduleType)
                            {
                                case RwAppData.ModuleType.Order:
                                case RwAppData.ModuleType.Truck:
                                case RwAppData.ModuleType.Transfer:
                                    dtSuspendedInContracts = await this.AppData.GetSuspendedInContractsAsync(conn, moduleType, response.webSelectOrder.orderId, session.security.webUser.usersid);
                                    if (dtSuspendedInContracts.Rows.Count > 0)
                                    {
                                        response.suspendedInContracts = dtSuspendedInContracts;
                                    }
                                    else
                                    {
                                        response.contract = this.AppData.CreateNewInContractAndSuspendAsync(conn, orderid, dealid, departmentid, usersid);
                                    }
                                    break;
                                default:
                                    response.contract = this.AppData.CreateNewInContractAndSuspendAsync(conn, orderid, dealid, departmentid, usersid);
                                    break;
                            }
                        }
                        break;
                    case RwAppData.ActivityType.Staging:
                        response.responsibleperson = await this.AppData.GetResponsiblePersonAsync(conn, orderid);
                        if (response.responsibleperson.showresponsibleperson == "T")
                        {
                            response.responsibleperson.responsiblepersons = await this.AppData.GetResponsiblePersonsAsync(conn: conn);
                        }
                        break;
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task GetOrders(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "GetOrders";
            string masterno = string.Empty;
            string orderno = string.Empty;
            string searchstring = string.Empty;

            FwValidate.TestPropertyDefined(METHOD_NAME, request, "moduletype");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "activitytype");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "pageno");
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "pagesize");
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                session.userLocation = await this.AppData.GetUserLocationAsync(conn, session.security.webUser.usersid);
                if (FwValidate.IsPropertyDefined(request, "masterno"))
                {
                    masterno = request.masterno;
                }
                if (FwValidate.IsPropertyDefined(request, "orderno"))
                {
                    orderno = request.orderno;
                }
                if (FwValidate.IsPropertyDefined(request, "searchstring"))
                {
                    searchstring = request.searchstring;
                }
                response.GetOrders = await GetOrdersQueryAsync(conn: conn,
                                                    moduletype: request.moduletype,
                                                    activitytype: request.activitytype,
                                                    pageno: request.pageno,
                                                    pagesize: request.pagesize,
                                                    warehouseid: session.userLocation.warehouseId,
                                                    locationid: session.userLocation.locationId,
                                                    masterno: masterno,
                                                    orderno: orderno,
                                                    searchstring: searchstring,
                                                    usersid: session.security.webUser.usersid); 
            }
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> GetOrdersQueryAsync(FwSqlConnection conn, string moduletype, string activitytype, int pageno, int pagesize, string warehouseid, string locationid, string masterno, string orderno, string searchstring, string usersid)
        {
            FwSqlCommand qry;
            FwJsonDataTable result;

            result = null;
            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            if (activitytype == RwConstants.ActivityTypes.Staging)
            {
                if (moduletype == RwConstants.ModuleTypes.Order)
                {
                    qry.AddColumn("Desc",        "orderdesc",   FwDataTypes.Text, false);
                    qry.AddColumn("Status",      "status",      FwDataTypes.Text, false);
                    qry.AddColumn("Order No",    "orderno",     FwDataTypes.Text, false);
                    qry.AddColumn("Date",        "orderdate",   FwDataTypes.Date, false);
                    qry.AddColumn("Deal",        "deal",        FwDataTypes.Text, false);
                    qry.AddColumn("Est",         "estrentfrom", FwDataTypes.Date, false);
                    qry.AddColumn("",            "estrentto",   FwDataTypes.Date, false);
                    qry.AddColumn("As Of",       "statusdate",  FwDataTypes.Date, false);
                    qry.Add("select orderdesc, status, orderno, orderdate, deal, estrentfrom, estrentto, statusdate");
                    qry.Add("from  dbo.funcorder('O' ,'F') o");
                    qry.Add("where o.ordertype = 'O'");
                    qry.Add("  and o.locationid = @locationid");
                    qry.Add("  and o.status not in ('NEW','CANCELLED','CLOSED','ORDERED','CANCELLED','CLOSED')");
                    qry.Add("  and o.orderid not in (select distinct dod.purchaseinternalorderid from dealorderdetail dod with (nolock))");
                    //qry.Add("  and o.orderid not in (select os.sessionid");
                    //qry.Add("                        from  ordersession os with (nolock))");
                    await DepartmentFilter.SetDepartmentFilterAsync(this.ApplicationConfig, usersid, qry);
                    if (!string.IsNullOrEmpty(searchstring))
                    {
                        qry.Add("  and ((orderno = @searchstring) or (orderdesc like @searchstring2) or (deal like @searchstring2))");
                        qry.AddParameter("@searchstring", searchstring);
                        qry.AddParameter("@searchstring2", "%" + searchstring + "%");
                    }
                    qry.Add("order by orderdate desc, orderno desc");
                    qry.AddParameter("@locationid", locationid);
                }
                else if (moduletype == RwConstants.ModuleTypes.Truck)
                {
                    qry.AddColumn("orderno",    false, FwDataTypes.Text);
                    qry.AddColumn("orderdesc",  false, FwDataTypes.Text);
                    qry.AddColumn("department", false, FwDataTypes.Text);
                    qry.AddColumn("asof",       false, FwDataTypes.Date);
                    qry.AddColumn("status",     false, FwDataTypes.Text);
                    qry.Add("select orderno, orderdesc, department, asof, status");
                    qry.Add("from dbo.functruck() o");
                    qry.Add("where o.locationid = @locationid");
                    qry.Add(" and o.status not in ('INACTIVE')");
                    if (!string.IsNullOrEmpty(searchstring))
                    {
                        qry.Add("  and ((orderno = @searchstring) or (orderdesc like @searchstring2))");
                        qry.AddParameter("@searchstring", searchstring);
                        qry.AddParameter("@searchstring2", "%" + searchstring + "%");
                    }
                    await DepartmentFilter.SetDepartmentFilterAsync(this.ApplicationConfig, usersid, qry);
                    qry.Add("order by orderno");
                    qry.AddParameter("@locationid", locationid);
                }
                result = await qry.QueryToFwJsonTableAsync(true, pageNo:pageno, pageSize:pagesize);
            }
            else if (activitytype == RwConstants.ActivityTypes.CheckIn)
            {
                if (moduletype == RwConstants.ModuleTypes.Order)
                {
                    qry.AddColumn("orderdesc", false);
                    qry.AddColumn("status", false);
                    qry.AddColumn("orderno", false);
                    qry.AddColumn("orderdate", false, FwDataTypes.Date);
                    qry.AddColumn("deal", false);
                    qry.AddColumn("estrentfrom", false, FwDataTypes.Date);
                    qry.AddColumn("estrentto", false, FwDataTypes.Date);
                    qry.AddColumn("As Of",       "statusdate",  FwDataTypes.Date, false);
                    qry.Add("select orderdesc, status, orderno, orderdate, deal, estrentfrom, estrentto, statusdate");
                    qry.Add("from  dbo.funcorder('O','F') o");
                    qry.Add("where o.ordertype = 'O'");
                    qry.Add("  and o.locationid = @locationid");
                    qry.Add("  and o.rentalsale <> 'T'");
                    qry.Add("  and o.ordertype = 'O'");
                    qry.Add("  and (o.status not in ('CANCELLED', 'SNAPSHOT'))");
                    qry.Add("  and o.hasrentalout = 'T'");
                    qry.Add("  and o.orderid not in (select distinct dod.purchaseinternalorderid from dealorderdetail dod with (nolock))");
                    //qry.Add("  and o.orderid not in (select os.sessionid from  ordersession os with (nolock))");
                    if (!string.IsNullOrEmpty(searchstring))
                    {
                        qry.Add("  and ((orderno = @searchstring) or (orderdesc like @searchstring2) or (deal like @searchstring2))");
                        qry.AddParameter("@searchstring", searchstring);
                        qry.AddParameter("@searchstring2", "%" + searchstring + "%");
                    }
                    await DepartmentFilter.SetDepartmentFilterAsync(this.ApplicationConfig, usersid, qry);
                    qry.Add("order by orderdate desc, orderno desc");
                    qry.AddParameter("@locationid", locationid);
                }
                else if (moduletype == RwConstants.ModuleTypes.Truck)
                {
                    qry.AddColumn("orderno",    false, FwDataTypes.Text);
                    qry.AddColumn("orderdesc",  false, FwDataTypes.Text);
                    qry.AddColumn("department", false, FwDataTypes.Text);
                    qry.AddColumn("asof",       false, FwDataTypes.Date);
                    qry.AddColumn("status",     false, FwDataTypes.Text);
                    qry.Add("select orderno, orderdesc, department, asof, status");
                    qry.Add("from dbo.functruck() o");
                    qry.Add("where o.locationid = @locationid");
                    qry.Add("  and o.status not in ('INACTIVE')");
                    qry.Add("  and o.warehouseid = @warehouseid");
                    if (!string.IsNullOrEmpty(searchstring))
                    {
                        qry.Add("  and ((orderno = @searchstring) or (orderdesc like @searchstring2))");
                        qry.AddParameter("@searchstring", searchstring);
                        qry.AddParameter("@searchstring2", "%" + searchstring + "%");
                    }
                    await DepartmentFilter.SetDepartmentFilterAsync(this.ApplicationConfig, usersid, qry);
                    qry.Add("order by orderno");
                    qry.AddParameter("@locationid", locationid);
                    qry.AddParameter("@warehouseid", warehouseid);
                }
                result = await qry.QueryToFwJsonTableAsync(true, pageNo:pageno, pageSize:pagesize);
            }
            else if (activitytype == RwConstants.ActivityTypes.AssetDisposition)
            {
                qry.AddColumn("orderid", false);    
                qry.AddColumn("orderdesc", false);
                qry.AddColumn("orderno", false);
                qry.AddColumn("orderdate", false, FwDataTypes.Date);
                qry.AddColumn("deal", false);
                qry.AddColumn("estrentfrom", false, FwDataTypes.Date);
                qry.AddColumn("estrentto", false, FwDataTypes.Date);
                qry.Add("select o.orderid, o.orderdesc, o.orderno, o.orderdate, d.deal, o.estrentfrom, o.estrentto");
                qry.Add("from dealorder o with (nolock) join deal d on (o.dealid = d.dealid)");
                qry.Add("where o.ordertype = 'O'");
                qry.Add("  and (exists (select *");
                qry.Add("               from masteritem mi with (nolock) join master m with (nolock) on (mi.masterid = m.masterid)");
                qry.Add("               where mi.orderid     = o.orderid");
                qry.Add("                 and mi.rectype     = 'R'");
                qry.Add("                 and mi.warehouseid = @warehouseid");
                qry.Add("                 and m.masterno     = @masterno))");
                qry.Add("  and o.rental = 'T'");
                qry.Add("  and o.status = 'ACTIVE'");
                if (!string.IsNullOrEmpty(orderno)) {
                    qry.Add("  and o.orderno = @orderno");
                    qry.AddParameter("@orderno", orderno);
                }
                await DepartmentFilter.SetDepartmentFilterAsync(this.ApplicationConfig, usersid, qry);
                qry.Add("order by o.orderdate desc, o.orderno desc");
                qry.AddParameter("@warehouseid", warehouseid);
                qry.AddParameter("@masterno", masterno);
                result = await qry.QueryToFwJsonTableAsync();
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
    }
}
