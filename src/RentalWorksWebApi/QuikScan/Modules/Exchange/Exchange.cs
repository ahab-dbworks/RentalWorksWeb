using FwStandard.Mobile;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.Utilities;
using RentalWorksQuikScan.Modules.ExchangeModels;
using RentalWorksQuikScan.Source;
using System.Threading.Tasks;
using WebApi.QuikScan;

namespace RentalWorksQuikScan.Modules
{
    public class Exchange : MobileModule
    {
        RwAppData AppData;
        ExchangeDm exchangeDm;
        ExchangeValidDlg exchangeValidDlg;
        //----------------------------------------------------------------------------------------------------
        public Exchange(FwApplicationConfig applicationConfig) : base(applicationConfig)
        {
            this.AppData = new RwAppData(applicationConfig);
            this.exchangeDm = new ExchangeDm(applicationConfig);
            this.exchangeValidDlg = new ExchangeValidDlg(applicationConfig);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize,searchmode,searchvalue")]
        public async Task OrderSearch(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.searchresults = await exchangeDm.OrderSearchAsync(conn: conn,
                                                                                   pageno: request.pageno,
                                                                                   pagesize: request.pagesize,
                                                                                   searchmode: request.searchmode,
                                                                                   searchvalue: request.searchvalue,
                                                                                   usersid: this.AppData.GetUsersId(session),
                                                                                   locationid: await this.AppData.GetLocationIdAsync(session)); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize,searchmode,searchvalue")]
        public async Task DealSearch(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.searchresults = await exchangeDm.DealSearchAsync(conn: conn,
                                                                                  pageno: request.pageno,
                                                                                  pagesize: request.pagesize,
                                                                                  searchmode: request.searchmode,
                                                                                  searchvalue: request.searchvalue,
                                                                                  locationid: await this.AppData.GetLocationIdAsync(session)); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize,searchmode,searchvalue")]
        public async Task CompanyDepartmentSearch(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.searchresults = await exchangeDm.CompanyDepartmentSearchAsync(conn: conn,
                                                                                               pageno: request.pageno,
                                                                                               pagesize: request.pagesize,
                                                                                               searchmode: request.searchmode,
                                                                                               searchvalue: request.searchvalue); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize,searchmode,searchvalue")]
        public async Task PendingExchangeSearch(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.searchresults = await exchangeDm.PendingExchangeSearchAsync(conn: conn,
                                                                                             pageno: request.pageno,
                                                                                             pagesize: request.pagesize,
                                                                                             searchmode: request.searchmode,
                                                                                             searchvalue: request.searchvalue,
                                                                                             locationid: await this.AppData.GetLocationIdAsync(session)); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize", OptionalParameters="orderid,dealid,departmentid")]
        public async Task SuspendedSessionSearch(dynamic request, dynamic response, dynamic session)
        {
            string orderid      = FwValidate.IsPropertyDefined(request, "orderid")      ? request.orderid      : null;
            string dealid       = FwValidate.IsPropertyDefined(request, "dealid")       ? request.dealid       : null;
            string departmentid = FwValidate.IsPropertyDefined(request, "departmentid") ? request.departmentid : null;
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.searchresults = await exchangeDm.SuspendedSessionSearchAsync(conn: conn,
                                                                                   pageno: request.pageno,
                                                                                   pagesize: request.pagesize,
                                                                                   orderid: orderid,
                                                                                   dealid: dealid,
                                                                                   departmentid: departmentid,
                                                                                   locationid: this.AppData.GetLocationIdAsync(session)); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize,exchangecontractid,pendingonly")]
        public async Task ExchangeSessionSearch(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.searchresults = await exchangeDm.ExchangeSessionSearchAsync(conn: conn,
                                                                                  pageno: request.pageno,
                                                                                  pagesize: request.pagesize,
                                                                                  exchangecontractid: request.exchangecontractid,
                                                                                  pendingonly: request.pendingonly); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize,exchangecontractid")]
        public async Task ExchangeRepairSearch(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.searchresults = await exchangeDm.ExchangeRepairSearchAsync(conn: conn,
                                                                                 pageno: request.pageno,
                                                                                 pagesize: request.pagesize,
                                                                                 exchangecontractid: request.exchangecontractid); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize,exchangecontractid")]
        public async Task ExchangeTransferSearch(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.searchresults = await exchangeDm.ExchangeTransferSearchAsync(conn: conn,
                                                                                   pageno: request.pageno,
                                                                                   pagesize: request.pagesize,
                                                                                   exchangecontractid: request.exchangecontractid); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize,exchange", OptionalParameters ="searchmode,searchvalue")]
        public async Task ValidDlgInNonBc(dynamic request, dynamic response, dynamic session)
        {
            string searchmode  = FwValidate.IsPropertyDefined(request, "searchmode")  ? request.searchmode  : string.Empty;
            string searchvalue = FwValidate.IsPropertyDefined(request, "searchvalue") ? request.searchvalue : string.Empty;
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                ExchangeModel exchange = GetExchangeRequest(request.exchange);
                response.searchresults = await exchangeValidDlg.InNonBCAsync(conn: conn,
                                                                             pageno: request.pageno,
                                                                             pagesize: request.pagesize,
                                                                             searchmode: searchmode,
                                                                             searchvalue: searchvalue,
                                                                             exchange: exchange,
                                                                             user: GetUser(session));
                response.exchange = GetExchangeResponse(exchange); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize,exchange", OptionalParameters ="searchmode,searchvalue")]
        public async Task ValidDlgInSerial(dynamic request, dynamic response, dynamic session)
        {
            string searchmode  = FwValidate.IsPropertyDefined(request, "searchmode")  ? request.searchmode  : string.Empty;
            string searchvalue = FwValidate.IsPropertyDefined(request, "searchvalue") ? request.searchvalue : string.Empty;
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                ExchangeModel exchange = GetExchangeRequest(request.exchange);
                response.searchresults = await exchangeValidDlg.InSerialAsync(conn: conn,
                                                                              pageno: request.pageno,
                                                                              pagesize: request.pagesize,
                                                                              searchmode: searchmode,
                                                                              searchvalue: searchvalue,
                                                                              exchange: exchange,
                                                                              user: GetUser(session));
                response.exchange = GetExchangeResponse(exchange); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize,exchange", OptionalParameters ="searchmode,searchvalue")]
        public async Task ValidDlgOutNonBc(dynamic request, dynamic response, dynamic session)
        {
            string searchmode  = FwValidate.IsPropertyDefined(request, "searchmode")  ? request.searchmode  : string.Empty;
            string searchvalue = FwValidate.IsPropertyDefined(request, "searchvalue") ? request.searchvalue : string.Empty;
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                ExchangeModel exchange = GetExchangeRequest(request.exchange);
                response.searchresults = await exchangeValidDlg.OutNonBcAsync(conn: conn,
                                                                              pageno: request.pageno,
                                                                              pagesize: request.pagesize,
                                                                              searchmode: searchmode,
                                                                              searchvalue: searchvalue,
                                                                              exchange: exchange,
                                                                              user: GetUser(session));
                response.exchange = GetExchangeResponse(exchange); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize,exchange", OptionalParameters ="searchmode,searchvalue")]
        public async Task ValidDlgPendingInBC(dynamic request, dynamic response, dynamic session)
        {
            string searchmode  = FwValidate.IsPropertyDefined(request, "searchmode")  ? request.searchmode  : string.Empty;
            string searchvalue = FwValidate.IsPropertyDefined(request, "searchvalue") ? request.searchvalue : string.Empty;
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                ExchangeModel exchange = GetExchangeRequest(request.exchange);
                response.searchresults = await exchangeValidDlg.PendingInBCAsync(conn: conn,
                                                                                 pageno: request.pageno,
                                                                                 pagesize: request.pagesize,
                                                                                 searchmode: searchmode,
                                                                                 searchvalue: searchvalue,
                                                                                 exchange: exchange,
                                                                                 user: GetUser(session));
                response.exchange = GetExchangeResponse(exchange); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize,exchange", OptionalParameters ="searchmode,searchvalue")]
        public async Task ValidDlgPendingInNonBc(dynamic request, dynamic response, dynamic session)
        {
            string searchmode  = FwValidate.IsPropertyDefined(request, "searchmode")  ? request.searchmode  : string.Empty;
            string searchvalue = FwValidate.IsPropertyDefined(request, "searchvalue") ? request.searchvalue : string.Empty;
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                ExchangeModel exchange = GetExchangeRequest(request.exchange);
                response.searchresults = await exchangeValidDlg.PendingInNonBcAsync(conn: conn,
                                                                                    pageno: request.pageno,
                                                                                    pagesize: request.pagesize,
                                                                                    searchmode: searchmode,
                                                                                    searchvalue: searchvalue,
                                                                                    exchange: exchange,
                                                                                    user: GetUser(session));
                response.exchange = GetExchangeResponse(exchange); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize,exchange", OptionalParameters ="searchmode,searchvalue")]
        public async Task ValidDlgPendingOutNonBc(dynamic request, dynamic response, dynamic session)
        {
            string searchmode  = FwValidate.IsPropertyDefined(request, "searchmode")  ? request.searchmode  : string.Empty;
            string searchvalue = FwValidate.IsPropertyDefined(request, "searchvalue") ? request.searchvalue : string.Empty;
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                ExchangeModel exchange = GetExchangeRequest(request.exchange);
                response.searchresults = await exchangeValidDlg.PendingOutNonBcAsync(conn: conn,
                                                                                     pageno: request.pageno,
                                                                                     pagesize: request.pagesize,
                                                                                     searchmode: searchmode,
                                                                                     searchvalue: searchvalue,
                                                                                     exchange: exchange,
                                                                                     user: GetUser(session));
                response.exchange = GetExchangeResponse(exchange); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize,exchange", OptionalParameters ="searchmode,searchvalue")]
        public async Task ValidDlgPendingOutSerial(dynamic request, dynamic response, dynamic session)
        {
            string searchmode  = FwValidate.IsPropertyDefined(request, "searchmode")  ? request.searchmode  : string.Empty;
            string searchvalue = FwValidate.IsPropertyDefined(request, "searchvalue") ? request.searchvalue : string.Empty;
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                ExchangeModel exchange = GetExchangeRequest(request.exchange);
                response.searchresults = await exchangeValidDlg.PendingOutSerialAsync(conn: conn,
                                                                                      pageno: request.pageno,
                                                                                      pagesize: request.pagesize,
                                                                                      searchmode: searchmode,
                                                                                      searchvalue: searchvalue,
                                                                                      exchange: exchange,
                                                                                      user: GetUser(session));
                response.exchange = GetExchangeResponse(exchange); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="orderid,dealid,departmentid")]
        public async Task CreateExchangeContract(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                response.createexchangecontract = await exchangeDm.createexchangecontractAsync(conn: conn,
                                                                                                       orderid: request.orderid,
                                                                                                       dealid: request.dealid,
                                                                                                       departmentid: request.departmentid,
                                                                                                       locationid: await this.AppData.GetLocationIdAsync(session),
                                                                                                       warehouseid: await this.AppData.GetWarehouseIdAsync(session),
                                                                                                       usersid: this.AppData.GetUsersId(session),
                                                                                                       notes: string.Empty); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="exchange")]
        public async Task ExchangeItem(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                ExchangeModel exchange = GetExchangeRequest(request.exchange);
                await exchangeDm.ExchangeItemAsync(conn: conn,
                                        exchange: exchange,
                                        user: GetUser(session));
                response.exchange = GetExchangeResponse(exchange); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="exchangecontractid")]
        public async Task CancelContract(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                await this.AppData.CancelContractAsync(conn: conn,
                                                 contractid: request.exchangecontractid,
                                                 usersid: this.AppData.GetUsersId(session),
                                                 failSilentlyOnOwnershipErrors: true); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="exchange")]
        public async Task GetItemInfo(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                ExchangeModel exchange = GetExchangeRequest(request.exchange);
                await exchangeDm.GetItemInfoAsync(conn: conn,
                                       itemstatus: exchange.request.itemstatus,
                                       exchange: exchange,
                                       user: GetUser(session));
                response.exchange = GetExchangeResponse(exchange); 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="")]
        public async Task GetNewExchangeModel(dynamic request, dynamic response, dynamic session)
        {
            ExchangeModel exchange = new ExchangeModel();
            response.exchange = exchange;
            await Task.CompletedTask;
        }
        //---------------------------------------------------------------------------------------------
        public ExchangeModel GetExchangeRequest(dynamic exchange)
        {
            ExchangeModel exchangeModel = ExchangeModel.ConvertToExchangeModel(exchange);
            exchangeModel.response = new ExchangeResponse();
            return exchangeModel;
        }
        //---------------------------------------------------------------------------------------------
        public ExchangeModel GetExchangeResponse(ExchangeModel exchange)
        {
            exchange.request = new ExchangeRequest();
            return exchange;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<UserContext> GetUser(dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                UserContext user = new UserContext();
                user.usersid = this.AppData.GetUsersId(session);
                user.primarylocationid = await this.AppData.GetLocationIdAsync(session);
                user.primarywarehouseid = await this.AppData.GetWarehouseIdAsync(session);
                user.rentalinventorydepartmentid = await FwSqlCommand.GetStringDataAsync(conn: conn,
                                                                              timeout: this.ApplicationConfig.DatabaseSettings.QueryTimeout,
                                                                              tablename: "users",
                                                                              wherecolumn: "usersid",
                                                                              wherecolumnvalue: user.usersid,
                                                                              selectcolumn: "rentalinventorydepartmentid");
                return user; 
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
