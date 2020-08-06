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
    public class TransferInMenu : QuikScanModule
    {
        RwAppData AppData;
        //----------------------------------------------------------------------------------------------------
        public TransferInMenu(FwApplicationConfig applicationConfig) : base(applicationConfig)
        {
            this.AppData = new RwAppData(applicationConfig);
        }
        //----------------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task SessionSearch(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic userLocation = await this.AppData.GetUserLocationAsync(conn, session.security.webUser.usersid);
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    FwSqlSelect select = new FwSqlSelect();
                    select.PageNo = request.pageno;
                    select.PageSize = request.pagesize;

                    select.Add("select *");
                    select.Add("from suspendview with (nolock)");
                    select.Add("where contracttype = 'RECEIPT'");
                    select.Add("  and ordertype    = 'T'");
                    select.Add("  and warehouseid  = @warehouseid");
                    if (!string.IsNullOrEmpty(request.searchvalue))
                    {
                        select.Add("  and sessionno = @sessionno");
                        select.AddParameter("@sessionno", request.searchvalue);
                    }
                    if (!string.IsNullOrEmpty(request.orderid))
                    {
                        select.Add("  and orderid = @orderid");
                        select.AddParameter("@orderid", request.orderid);
                    }
                    select.Parse();
                    select.AddOrderBy("contractdate desc, contracttime desc");
                    select.AddParameter("@warehouseid", userLocation.warehouseId);
                    response.searchresults = await qry.QueryToFwJsonTableAsync(select, true);
                }
            }
        }
        //----------------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task OrderSearch(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "OrderSearch";
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "searchvalue");
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                dynamic userLocation = await this.AppData.GetUserLocationAsync(conn, session.security.webUser.usersid);
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    FwSqlSelect select = new FwSqlSelect();
                    select.PageNo = request.pageno;
                    select.PageSize = request.pagesize;
                    qry.AddColumn("orderdate", false, FwDataTypes.Date);
                    qry.AddColumn("pickdate", false, FwDataTypes.Date);
                    qry.AddColumn("shipdate", false, FwDataTypes.Date);
                    qry.AddColumn("receivedate", false, FwDataTypes.Date);
                    select.Add("select *");
                    select.Add("from  transferview with (nolock)");
                    select.Add("where status       ='ACTIVE'");
                    select.Add("  and warehouseid  = @warehouseid");
                    if (!string.IsNullOrEmpty(request.searchvalue))
                    {
                        switch ((string)request.searchmode)
                        {
                            case "ORDERNO":
                                select.Add("and orderno like @orderno");
                                select.AddParameter("@orderno", request.searchvalue + "%");
                                break;
                            case "DESCRIPTION":
                                select.Add("and orderdesc like @orderdesc");
                                select.AddParameter("@orderdesc", "%" + request.searchvalue + "%");
                                break;
                            case "DEAL":
                                select.Add("and deal like @deal");
                                select.AddParameter("@deal", "%" + request.searchvalue + "%");
                                break;
                        }
                    }
                    select.Add("order by orderdate desc, orderno desc");
                    select.AddParameter("@warehouseid", userLocation.warehouseId);
                    response.searchresults = await qry.QueryToFwJsonTableAsync(select, true);
                }
            }
        }
        //----------------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters = "")]
        public async Task CreateSuspendedSession(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand sp = new FwSqlCommand(conn, "dbo.createincontract", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    sp.AddParameter("@orderid", request.orderid);
                    sp.AddParameter("@dealid", request.dealid);
                    sp.AddParameter("@departmentid", request.departmentid);
                    sp.AddParameter("@usersid", session.security.webUser.usersid);
                    sp.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Output);
                    await sp.ExecuteAsync();
                    response.contractid = sp.GetParameter("@contractid").ToString().Trim();
                }
                response.sessionno = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "contract", "contractid", response.contractid, "sessionno");
                if (!string.IsNullOrEmpty(response.contractid))
                {
                    using (FwSqlCommand cmd = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                    {
                        cmd.Add("update contract");
                        cmd.Add("set forcedsuspend = 'G'");
                        cmd.Add("where contractid = @contractid");
                        cmd.AddParameter("@contractid", response.contractid);
                        await cmd.ExecuteAsync();
                    }
                }
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}