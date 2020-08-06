using FwStandard.Mobile;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.Utilities;
using RentalWorksQuikScan.Source;
using System;
using System.Data;
using System.Dynamic;
using System.Threading.Tasks;
using WebApi.QuikScan;

namespace RentalWorksQuikScan.Modules
{
    public class AssetDisposition : QuikScanModule
    {
        RwAppData AppData;
        //----------------------------------------------------------------------------------------------------
        public AssetDisposition(FwApplicationConfig applicationConfig) : base(applicationConfig) 
        { 
            this.AppData = new RwAppData(applicationConfig); 
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task GetSearchResults(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                string searchmode = request.searchmode;
                var userLocation = await this.AppData.GetUserLocationAsync(conn, session.security.webUser.usersid);
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddColumn("thumbnail", false, FwDataTypes.JpgDataUrl);
                    FwSqlSelect select = new FwSqlSelect();
                    select.PageNo = request.pageno;
                    select.PageSize = request.pagesize;
                    select.Add("select distinct masterno, master, department, production, setcharacter, setno, thumbnail = cast(thumbnail as varbinary(max)), barcode, manufacturer, refnobox, refnopallet");
                    select.Add("from funcitemdispositionweb(@warehouseid)");
                    select.Parse();
                    switch (searchmode)
                    {
                        case "code":
                            select.AddWhere("(masterno = @searchvalue) or (barcode = @searchvalue)");
                            select.AddOrderBy("masterno");
                            select.AddParameter("@searchvalue", request.searchvalue);
                            break;
                        case "description":
                            select.AddWhere("master like @searchvalue");
                            select.AddOrderBy("master");
                            select.AddOrderBy("masterno");
                            select.AddParameter("@searchvalue", "%" + request.searchvalue + "%");
                            break;
                        case "department":
                            select.AddWhere("department like @searchvalue");
                            select.AddOrderBy("department");
                            select.AddOrderBy("masterno");
                            select.AddParameter("@searchvalue", request.searchvalue + "%");
                            break;
                        case "production":
                            select.AddWhere("production like @searchvalue");
                            select.AddOrderBy("production");
                            select.AddOrderBy("masterno");
                            select.AddParameter("@searchvalue", request.searchvalue + "%");
                            break;
                        case "setcharacter":
                            select.AddWhere("setcharacter like @searchvalue");
                            select.AddOrderBy("setcharacter");
                            select.AddOrderBy("masterno");
                            select.AddParameter("@searchvalue", request.searchvalue + "%");
                            break;
                        case "setno":
                            select.AddWhere("setno like @searchvalue");
                            select.AddOrderBy("setno");
                            select.AddOrderBy("masterno");
                            select.AddParameter("@searchvalue", request.searchvalue);
                            break;
                    }
                    select.AddParameter("@warehouseid", userLocation.warehouseId);
                    response.searchresults = await qry.QueryToFwJsonTableAsync(select, true);
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task GetItemStatus(dynamic request, dynamic response, dynamic session)
        {
            const string METHOD_NAME = "GetItemStatus";
            string masterid = "";
            FwValidate.TestIsNullOrEmpty(METHOD_NAME, "usersid", session.security.webUser.usersid);
            FwValidate.TestPropertyDefined(METHOD_NAME, request, "barcode");
            string rentalitemid = string.Empty;

            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                using (FwSqlCommand sp = new FwSqlCommand(conn, "dbo.webgetitemstatus", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    sp.AddParameter("@code",                 request.barcode);
                    sp.AddParameter("@usersid",              session.security.webUser.usersid);
                    sp.AddParameter("@masterno",             SqlDbType.Char,     ParameterDirection.Output);
                    sp.AddParameter("@masterid",             SqlDbType.Char,     ParameterDirection.Output);
                    sp.AddParameter("@description",          SqlDbType.Char,     ParameterDirection.Output);
                    sp.AddParameter("@orderid",              SqlDbType.Char,     ParameterDirection.Output);
                    sp.AddParameter("@orderno",              SqlDbType.Char,     ParameterDirection.Output);
                    sp.AddParameter("@orderdesc",            SqlDbType.Char,     ParameterDirection.Output);
                    sp.AddParameter("@dealid",               SqlDbType.Char,     ParameterDirection.Output);
                    sp.AddParameter("@dealno",               SqlDbType.VarChar,  ParameterDirection.Output);
                    sp.AddParameter("@deal",                 SqlDbType.VarChar,  ParameterDirection.Output);
                    sp.AddParameter("@warehouseid",          SqlDbType.Char,     ParameterDirection.Output);
                    sp.AddParameter("@warehouse",            SqlDbType.Char,     ParameterDirection.Output);
                    sp.AddParameter("@vendorid",             SqlDbType.Char,     ParameterDirection.Output);
                    sp.AddParameter("@vendor",               SqlDbType.Char,     ParameterDirection.Output);
                    sp.AddParameter("@masteritemid",         SqlDbType.Char,     ParameterDirection.Output);
                    sp.AddParameter("@rentalitemid",         SqlDbType.Char,     ParameterDirection.Output);
                    sp.AddParameter("@departmentid",         SqlDbType.Char,     ParameterDirection.Output);
                    sp.AddParameter("@ordertranid",          SqlDbType.Int,      ParameterDirection.Output);
                    sp.AddParameter("@internalchar",         SqlDbType.Char,     ParameterDirection.Output);
                    sp.AddParameter("@itemstatus",           SqlDbType.Char,     ParameterDirection.Output);
                    sp.AddParameter("@statuscolor",          SqlDbType.Int,      ParameterDirection.Output);
                    sp.AddParameter("@statustextcolor",      SqlDbType.Char,     ParameterDirection.Output);
                    sp.AddParameter("@rentalstatus",         SqlDbType.Char,     ParameterDirection.Output);
                    sp.AddParameter("@statusdate",           SqlDbType.DateTime, ParameterDirection.Output);
                    sp.AddParameter("@retiredreason",        SqlDbType.Char,     ParameterDirection.Output);
                    sp.AddParameter("@isicode",              SqlDbType.Char,     ParameterDirection.Output);
                    sp.AddParameter("@availfor",             SqlDbType.Char,     ParameterDirection.Output);
                    sp.AddParameter("@trackedby",            SqlDbType.Char,     ParameterDirection.Output);
                    sp.AddParameter("@aisleloc",             SqlDbType.Char,     ParameterDirection.Output);
                    sp.AddParameter("@shelfloc",             SqlDbType.Char,     ParameterDirection.Output);
                    sp.AddParameter("@location",             SqlDbType.VarChar,  ParameterDirection.Output);
                    sp.AddParameter("@setcharacter",         SqlDbType.VarChar,  ParameterDirection.Output);
                    sp.AddParameter("@manifestvalue",        SqlDbType.Decimal,  ParameterDirection.Output);
                    sp.Parameters["@manifestvalue"].Precision = 12;
                    sp.Parameters["@manifestvalue"].Scale     = 3;
                    sp.AddParameter("@detail01",             SqlDbType.VarChar,  ParameterDirection.Output);
                    sp.AddParameter("@detail02",             SqlDbType.VarChar,  ParameterDirection.Output);
                    sp.AddParameter("@detail03",             SqlDbType.VarChar,  ParameterDirection.Output);
                    sp.AddParameter("@genericerror",         SqlDbType.VarChar,  ParameterDirection.Output);
                    sp.AddParameter("@buyer",                SqlDbType.VarChar,  ParameterDirection.Output);
                    sp.AddParameter("@manufacturer",         SqlDbType.VarChar,  ParameterDirection.Output);
                    sp.AddParameter("@pono",                 SqlDbType.VarChar,  ParameterDirection.Output);
                    sp.AddParameter("@productiondepartment", SqlDbType.Char,     ParameterDirection.Output);
                    sp.AddParameter("@itemclass",            SqlDbType.VarChar,  ParameterDirection.Output);
                    sp.AddParameter("@barcode",              SqlDbType.VarChar,  ParameterDirection.Output);
                    sp.AddParameter("@mfgserial",            SqlDbType.VarChar,  ParameterDirection.Output);
                    sp.AddParameter("@rfid",                 SqlDbType.VarChar,  ParameterDirection.Output);
                    sp.AddParameter("@status",               SqlDbType.Int,      ParameterDirection.Output);
                    sp.AddParameter("@msg",                  SqlDbType.NVarChar, ParameterDirection.Output);
                    await sp.ExecuteAsync();
                    dynamic result       = new ExpandoObject();
                    result.isICode       = sp.GetParameter("@isicode").ToBoolean();
                    result.warehouseId   = FwCryptography.AjaxEncrypt(sp.GetParameter("@warehouseid").ToString().TrimEnd());
                    masterid             = sp.GetParameter("@masterid").ToString().TrimEnd();
                    result.masterId      = FwCryptography.AjaxEncrypt(masterid);
                    result.masterNo      = sp.GetParameter("@masterno").ToString().TrimEnd();
                    rentalitemid         = sp.GetParameter("@rentalitemid").ToString().TrimEnd();
                    result.rentalitemid  = FwCryptography.AjaxEncrypt(rentalitemid);
                    result.description   = sp.GetParameter("@description").ToString().TrimEnd();
                    result.rentalStatus  = sp.GetParameter("@rentalstatus").ToString().TrimEnd();
                    result.color         = sp.GetParameter("@statuscolor").ToHtmlColor();
                    result.textcolor     = (sp.GetParameter("@statustextcolor").ToString().TrimEnd() == "B") ? "#000000" : "#FFFFFF";
                    result.dealid        = FwCryptography.AjaxEncrypt(sp.GetParameter("@dealid").ToString().TrimEnd());
                    result.dealNo        = sp.GetParameter("@dealno").ToString().TrimEnd();
                    result.deal          = sp.GetParameter("@deal").ToString().TrimEnd();
                    result.departmentid  = FwCryptography.AjaxEncrypt(sp.GetParameter("@departmentid").ToString().TrimEnd());
                    result.department    = sp.GetParameter("@productiondepartment").ToString().TrimEnd();
                    result.orderid       = FwCryptography.AjaxEncrypt(sp.GetParameter("@orderid").ToString().TrimEnd());
                    result.orderNo       = sp.GetParameter("@orderno").ToString().TrimEnd();
                    result.orderDesc     = sp.GetParameter("@orderdesc").ToString().TrimEnd();
                    result.statusDate    = sp.GetParameter("@statusdate").ToShortDateString();
                    result.aisleloc      = sp.GetParameter("@aisleloc").ToString().TrimEnd();
                    result.shelfloc      = sp.GetParameter("@shelfloc").ToString().TrimEnd();
                    result.location      = sp.GetParameter("@location").ToString().TrimEnd();
                    result.setcharacter  = sp.GetParameter("@setcharacter").ToString().TrimEnd();
                    result.manifestvalue = sp.GetParameter("@manifestvalue").ToDecimal();
                    result.detail01      = sp.GetParameter("@detail01").ToString().TrimEnd();
                    result.detail02      = sp.GetParameter("@detail02").ToString().TrimEnd();
                    result.detail03      = sp.GetParameter("@detail03").ToString().TrimEnd();
                    result.genericError  = sp.GetParameter("@genericerror").ToString().TrimEnd();
                    result.buyer         = sp.GetParameter("@buyer").ToString().TrimEnd();
                    result.manufacturer  = sp.GetParameter("@manufacturer").ToString().TrimEnd();
                    result.pono          = sp.GetParameter("@pono").ToString().TrimEnd();
                    result.barcode       = sp.GetParameter("@barcode").ToString().TrimEnd();
                    result.mfgserial     = sp.GetParameter("@mfgserial").ToString().TrimEnd();
                    result.rfid          = sp.GetParameter("@rfid").ToString().TrimEnd();
                    result.itemclass     = sp.GetParameter("@itemclass").ToString().TrimEnd();
                    result.vendor        = sp.GetParameter("@vendor").ToString().TrimEnd();
                    result.trackedby     = sp.GetParameter("@trackedby").ToString().TrimEnd();
                    result.status        = sp.GetParameter("@status").ToInt32();
                    result.msg           = sp.GetParameter("@msg").ToString().TrimEnd();
                    response.webGetItemStatus = result;
                }

                //response.webGetItemStatus = RwAppData.WebGetItemStatus(conn: conn,
                //                                                       usersId: session.security.webUser.usersid,
                //                                                       barcode: request.barcode);
                dynamic userLocation = await this.AppData.GetUserLocationAsync(conn, session.security.webUser.usersid);
                response.funcMasterWh = await this.AppData.FuncMasterWhAsync(conn: conn,
                                                               masterid: masterid,
                                                               userswarehouseid: userLocation.warehouseId,
                                                               filterwarehouseid: string.Empty,
                                                               currencyid: string.Empty);

                if (response.webGetItemStatus.trackedby != "QUANTITY")
                {
                    response.appImages = await this.AppData.GetPrimaryAppImageThumbnailAsync(conn: conn
                                                                             , uniqueid1: rentalitemid
                                                                             , uniqueid2: string.Empty
                                                                             , uniqueid3: string.Empty
                                                                             , description: string.Empty
                                                                             , rectype: string.Empty);

                }
                if ((response.webGetItemStatus.trackedby == "QUANTITY") || (response.appImages.Length == 0))
                {
                    response.appImages = await this.AppData.GetPrimaryAppImageThumbnailAsync(conn: conn
                                                                             , uniqueid1: masterid
                                                                             , uniqueid2: string.Empty
                                                                             , uniqueid3: string.Empty
                                                                             , description: string.Empty
                                                                             , rectype: string.Empty);
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task GetMaxRetireQty(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                const string METHOD_NAME = "GetMaxRetireQty";
                FwValidate.TestPropertyDefined(METHOD_NAME, request, "masterid");
                FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
                dynamic userLocation = await this.AppData.GetUserLocationAsync(conn, session.security.webUser.usersid);
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select top 1 qtyout");
                    qry.Add("from funcicodeoutonorders(@masterid)");
                    qry.Add("where orderid = @orderid");
                    qry.AddParameter("@masterid", FwCryptography.AjaxDecrypt(request.masterid));
                    qry.AddParameter("@orderid", FwCryptography.AjaxDecrypt(request.orderid));
                    qry.AddParameter("@warehouseid", userLocation.warehouseId);
                    await qry.ExecuteAsync();
                    response.maxretireqty = qry.GetField("qtyout").ToInt32();
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
        [FwJsonServiceMethod]
        public async Task RetireItem(dynamic request, dynamic response, dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                const string METHOD_NAME = "RetireItem";

                FwValidate.TestPropertyDefined(METHOD_NAME, request, "isicode");
                FwValidate.TestPropertyDefined(METHOD_NAME, request, "barcode");
                FwValidate.TestPropertyDefined(METHOD_NAME, request, "orderid");
                FwValidate.TestPropertyDefined(METHOD_NAME, request, "masterno");
                FwValidate.TestPropertyDefined(METHOD_NAME, request, "qty");
                FwValidate.TestPropertyDefined(METHOD_NAME, request, "retiredreasonid");
                FwValidate.TestPropertyDefined(METHOD_NAME, request, "lossamount");
                FwValidate.TestPropertyDefined(METHOD_NAME, request, "notes");
                session.userLocation = await this.AppData.GetUserLocationAsync(conn, session.security.webUser.usersid);
                string orderid = FwCryptography.AjaxDecrypt(request.orderid);
                response.assetDisposition = await this.AppData.AssetDispositionAsync(
                    conn: conn,
                    isicode: request.isicode,
                    barcode: request.barcode,
                    orderid: orderid,
                    masterno: request.masterno,
                    qty: FwConvert.ToDecimal(request.qty),
                    retiredreasonid: FwCryptography.AjaxDecrypt(request.retiredreasonid),
                    lossamount: FwConvert.ToDecimal(request.lossamount),
                    notes: request.notes,
                    usersid: session.security.webUser.usersid,
                    warehouseid: string.IsNullOrEmpty(orderid) ? session.userLocation.warehouseId : string.Empty);
                if (response.assetDisposition.errno != 0)
                {
                    throw new Exception(response.assetDisposition.errmsg);
                } 
            }
        }
        //---------------------------------------------------------------------------------------------
    }
}
