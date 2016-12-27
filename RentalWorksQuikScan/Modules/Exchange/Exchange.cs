using Fw.Json.Services.Common;
using Fw.Json.SqlServer;
using Fw.Json.Utilities;
using RentalWorksQuikScan.Modules.ExchangeModels;
using RentalWorksQuikScan.Source;

namespace RentalWorksQuikScan.Modules
{
    public class Exchange
    {
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize,searchmode,searchvalue")]
        public static void OrderSearch(dynamic request, dynamic response, dynamic session)
        {
            response.searchresults = ExchangeDm.OrderSearch(conn: FwSqlConnection.RentalWorks,
                                                            pageno: request.pageno,
                                                            pagesize: request.pagesize,
                                                            searchmode: request.searchmode,
                                                            searchvalue: request.searchvalue,
                                                            usersid: RwAppData.GetUsersId(session),
                                                            locationid: RwAppData.GetLocationId(session));
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize,searchmode,searchvalue")]
        public static void DealSearch(dynamic request, dynamic response, dynamic session)
        {
            response.searchresults = ExchangeDm.DealSearch(conn: FwSqlConnection.RentalWorks,
                                                           pageno: request.pageno,
                                                           pagesize: request.pagesize,
                                                           searchmode: request.searchmode,
                                                           searchvalue: request.searchvalue,
                                                           locationid: RwAppData.GetLocationId(session));
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize,searchmode,searchvalue")]
        public static void CompanyDepartmentSearch(dynamic request, dynamic response, dynamic session)
        {
            response.searchresults = ExchangeDm.CompanyDepartmentSearch(conn: FwSqlConnection.RentalWorks,
                                                                        pageno: request.pageno,
                                                                        pagesize: request.pagesize,
                                                                        searchmode: request.searchmode,
                                                                        searchvalue: request.searchvalue);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize,searchmode,searchvalue")]
        public static void PendingExchangeSearch(dynamic request, dynamic response, dynamic session)
        {
            response.searchresults = ExchangeDm.PendingExchangeSearch(conn: FwSqlConnection.RentalWorks,
                                                                      pageno: request.pageno,
                                                                      pagesize: request.pagesize,
                                                                      searchmode: request.searchmode,
                                                                      searchvalue: request.searchvalue,
                                                                      locationid: RwAppData.GetLocationId(session));
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize", OptionalParameters="orderid,dealid,departmentid")]
        public static void SuspendedSessionSearch(dynamic request, dynamic response, dynamic session)
        {
            string orderid      = FwValidate.IsPropertyDefined(request, "orderid")      ? request.orderid      : null;
            string dealid       = FwValidate.IsPropertyDefined(request, "dealid")       ? request.dealid       : null;
            string departmentid = FwValidate.IsPropertyDefined(request, "departmentid") ? request.departmentid : null;
            response.searchresults = ExchangeDm.SuspendedSessionSearch(conn: FwSqlConnection.RentalWorks,
                                                                       pageno: request.pageno,
                                                                       pagesize: request.pagesize,
                                                                       orderid: orderid,
                                                                       dealid: dealid,
                                                                       departmentid: departmentid,
                                                                       locationid: RwAppData.GetLocationId(session));
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize,exchangecontractid,pendingonly")]
        public static void ExchangeSessionSearch(dynamic request, dynamic response, dynamic session)
        {
            response.searchresults = ExchangeDm.ExchangeSessionSearch(conn: FwSqlConnection.RentalWorks,
                                                                      pageno: request.pageno,
                                                                      pagesize: request.pagesize,
                                                                      exchangecontractid: request.exchangecontractid,
                                                                      pendingonly: request.pendingonly);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize,exchangecontractid")]
        public static void ExchangeRepairSearch(dynamic request, dynamic response, dynamic session)
        {
            response.searchresults = ExchangeDm.ExchangeRepairSearch(conn: FwSqlConnection.RentalWorks,
                                                                     pageno: request.pageno,
                                                                     pagesize: request.pagesize,
                                                                     exchangecontractid: request.exchangecontractid);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize,exchangecontractid")]
        public static void ExchangeTransferSearch(dynamic request, dynamic response, dynamic session)
        {
            response.searchresults = ExchangeDm.ExchangeTransferSearch(conn: FwSqlConnection.RentalWorks,
                                                                       pageno: request.pageno,
                                                                       pagesize: request.pagesize,
                                                                       exchangecontractid: request.exchangecontractid);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize,exchange", OptionalParameters ="searchmode,searchvalue")]
        public static void ValidDlgInNonBc(dynamic request, dynamic response, dynamic session)
        {
            string searchmode  = FwValidate.IsPropertyDefined(request, "searchmode")  ? request.searchmode  : string.Empty;
            string searchvalue = FwValidate.IsPropertyDefined(request, "searchvalue") ? request.searchvalue : string.Empty;
            ExchangeModel exchange = GetExchangeRequest(request.exchange);
            response.searchresults = ExchangeValidDlg.InNonBC(conn: FwSqlConnection.RentalWorks,
                                                              pageno: request.pageno,
                                                              pagesize: request.pagesize,
                                                              searchmode: searchmode,
                                                              searchvalue: searchvalue,
                                                              exchange: exchange,
                                                              user: GetUser(session));
            response.exchange = GetExchangeResponse(exchange);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize,exchange", OptionalParameters ="searchmode,searchvalue")]
        public static void ValidDlgInSerial(dynamic request, dynamic response, dynamic session)
        {
            string searchmode  = FwValidate.IsPropertyDefined(request, "searchmode")  ? request.searchmode  : string.Empty;
            string searchvalue = FwValidate.IsPropertyDefined(request, "searchvalue") ? request.searchvalue : string.Empty;
            ExchangeModel exchange = GetExchangeRequest(request.exchange);
            response.searchresults = ExchangeValidDlg.InSerial(conn: FwSqlConnection.RentalWorks,
                                                               pageno: request.pageno,
                                                               pagesize: request.pagesize,
                                                               searchmode: searchmode,
                                                               searchvalue: searchvalue,
                                                               exchange: exchange,
                                                               user: GetUser(session));
            response.exchange = GetExchangeResponse(exchange);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize,exchange", OptionalParameters ="searchmode,searchvalue")]
        public static void ValidDlgOutNonBc(dynamic request, dynamic response, dynamic session)
        {
            string searchmode  = FwValidate.IsPropertyDefined(request, "searchmode")  ? request.searchmode  : string.Empty;
            string searchvalue = FwValidate.IsPropertyDefined(request, "searchvalue") ? request.searchvalue : string.Empty;
            ExchangeModel exchange = GetExchangeRequest(request.exchange);
            response.searchresults = ExchangeValidDlg.OutNonBc(conn: FwSqlConnection.RentalWorks,
                                                               pageno: request.pageno,
                                                               pagesize: request.pagesize,
                                                               searchmode: searchmode,
                                                               searchvalue: searchvalue,
                                                               exchange: exchange,
                                                               user: GetUser(session));
            response.exchange = GetExchangeResponse(exchange);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize,exchange", OptionalParameters ="searchmode,searchvalue")]
        public static void ValidDlgPendingInBC(dynamic request, dynamic response, dynamic session)
        {
            string searchmode  = FwValidate.IsPropertyDefined(request, "searchmode")  ? request.searchmode  : string.Empty;
            string searchvalue = FwValidate.IsPropertyDefined(request, "searchvalue") ? request.searchvalue : string.Empty;
            ExchangeModel exchange = GetExchangeRequest(request.exchange);
            response.searchresults = ExchangeValidDlg.PendingInBC(conn: FwSqlConnection.RentalWorks,
                                                                  pageno: request.pageno,
                                                                  pagesize: request.pagesize,
                                                                  searchmode: searchmode,
                                                                  searchvalue: searchvalue,
                                                                  exchange: exchange,
                                                                  user: GetUser(session));
            response.exchange = GetExchangeResponse(exchange);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize,exchange", OptionalParameters ="searchmode,searchvalue")]
        public static void ValidDlgPendingInNonBc(dynamic request, dynamic response, dynamic session)
        {
            string searchmode  = FwValidate.IsPropertyDefined(request, "searchmode")  ? request.searchmode  : string.Empty;
            string searchvalue = FwValidate.IsPropertyDefined(request, "searchvalue") ? request.searchvalue : string.Empty;
            ExchangeModel exchange = GetExchangeRequest(request.exchange);
            response.searchresults = ExchangeValidDlg.PendingInNonBc(conn: FwSqlConnection.RentalWorks,
                                                                     pageno: request.pageno,
                                                                     pagesize: request.pagesize,
                                                                     searchmode: searchmode,
                                                                     searchvalue: searchvalue,
                                                                     exchange: exchange,
                                                                     user: GetUser(session));
            response.exchange = GetExchangeResponse(exchange);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize,exchange", OptionalParameters ="searchmode,searchvalue")]
        public static void ValidDlgPendingOutNonBc(dynamic request, dynamic response, dynamic session)
        {
            string searchmode  = FwValidate.IsPropertyDefined(request, "searchmode")  ? request.searchmode  : string.Empty;
            string searchvalue = FwValidate.IsPropertyDefined(request, "searchvalue") ? request.searchvalue : string.Empty;
            ExchangeModel exchange = GetExchangeRequest(request.exchange);
            response.searchresults = ExchangeValidDlg.PendingOutNonBc(conn: FwSqlConnection.RentalWorks,
                                                                      pageno: request.pageno,
                                                                      pagesize: request.pagesize,
                                                                      searchmode: searchmode,
                                                                      searchvalue: searchvalue,
                                                                      exchange: exchange,
                                                                      user: GetUser(session));
            response.exchange = GetExchangeResponse(exchange);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="pageno,pagesize,exchange", OptionalParameters ="searchmode,searchvalue")]
        public static void ValidDlgPendingOutSerial(dynamic request, dynamic response, dynamic session)
        {
            string searchmode  = FwValidate.IsPropertyDefined(request, "searchmode")  ? request.searchmode  : string.Empty;
            string searchvalue = FwValidate.IsPropertyDefined(request, "searchvalue") ? request.searchvalue : string.Empty;
            ExchangeModel exchange = GetExchangeRequest(request.exchange);
            response.searchresults = ExchangeValidDlg.PendingOutSerial(conn: FwSqlConnection.RentalWorks,
                                                                       pageno: request.pageno,
                                                                       pagesize: request.pagesize,
                                                                       searchmode: searchmode,
                                                                       searchvalue: searchvalue,
                                                                       exchange: exchange,
                                                                       user: GetUser(session));
            response.exchange = GetExchangeResponse(exchange);
        }

        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="orderid,dealid,departmentid")]
        public static void CreateExchangeContract(dynamic request, dynamic response, dynamic session)
        {
            response.createexchangecontract = ExchangeDm.createexchangecontract(conn: FwSqlConnection.RentalWorks,
                                                                                orderid: request.orderid,
                                                                                dealid: request.dealid,
                                                                                departmentid: request.departmentid,
                                                                                locationid: RwAppData.GetLocationId(session),
                                                                                warehouseid: RwAppData.GetWarehouseId(session),
                                                                                usersid: RwAppData.GetUsersId(session),
                                                                                notes: string.Empty);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="exchange")]
        public static void ExchangeItem(dynamic request, dynamic response, dynamic session)
        {
            ExchangeModel exchange = GetExchangeRequest(request.exchange);
            ExchangeDm.ExchangeItem(conn: FwSqlConnection.RentalWorks,
                                    exchange: exchange,
                                    user: GetUser(session));
            response.exchange = GetExchangeResponse(exchange);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="exchangecontractid")]
        public static void CancelContract(dynamic request, dynamic response, dynamic session)
        {
            RwAppData.CancelContract(conn: FwSqlConnection.RentalWorks,
                                     contractid: request.exchangecontractid, 
                                     usersid: RwAppData.GetUsersId(session), 
                                     failSilentlyOnOwnershipErrors: true);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="exchange")]
        public static void GetItemInfo(dynamic request, dynamic response, dynamic session)
        {
            ExchangeModel exchange = GetExchangeRequest(request.exchange);
            ExchangeDm.GetItemInfo(conn: FwSqlConnection.RentalWorks,
                                   itemstatus: exchange.request.itemstatus,
                                   exchange: exchange,
                                   user: GetUser(session));
            response.exchange = GetExchangeResponse(exchange);
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod(RequiredParameters="")]
        public static void GetNewExchangeModel(dynamic request, dynamic response, dynamic session)
        {
            ExchangeModel exchange = new ExchangeModel();
            response.exchange = exchange;
        }
        //---------------------------------------------------------------------------------------------
        public static ExchangeModel GetExchangeRequest(dynamic exchange)
        {
            ExchangeModel exchangeModel = ExchangeModel.ConvertToExchangeModel(exchange);
            exchangeModel.response = new ExchangeResponse();
            return exchangeModel;
        }
        //---------------------------------------------------------------------------------------------
        public static ExchangeModel GetExchangeResponse(ExchangeModel exchange)
        {
            exchange.request = new ExchangeRequest();
            return exchange;
        }
        //---------------------------------------------------------------------------------------------
        public static UserContext GetUser(dynamic session)
        {
            UserContext user = new UserContext();
            user.usersid = RwAppData.GetUsersId(session);
            user.primarylocationid = RwAppData.GetLocationId(session);
            user.primarywarehouseid = RwAppData.GetWarehouseId(session);
            user.rentalinventorydepartmentid = FwSqlCommand.GetStringData(conn: FwSqlConnection.RentalWorks, 
                                                                          tablename: "users", 
                                                                          wherecolumn: "usersid", 
                                                                          wherecolumnvalue: user.usersid, 
                                                                          selectcolumn: "rentalinventorydepartmentid");
            return user;
        }
        //---------------------------------------------------------------------------------------------
    }
}
