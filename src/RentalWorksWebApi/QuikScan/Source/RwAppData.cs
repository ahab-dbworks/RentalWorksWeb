using FwStandard.Mobile;
using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;

namespace RentalWorksQuikScan.Source
{
    public class RwAppData
    {
        public enum ItemType     { BarCoded, NonBarCoded, None };
        public enum ModuleType   { Order, PhyInv, Transfer, Truck, Repair, Exchange, SubReceive, SubReturn, MoveToLocation, QC, None };
        public enum ActivityType { CheckIn, Staging, SubReceive, SubReturn };//SubRental, SubSale, SubRentalReceive, SubSaleReceive };
        public enum CheckInMode  { SingleOrder, MultiOrder, Session, Deal }
        public enum RepairMode   { Select, Release, Complete }
        //----------------------------------------------------------------------------------------------------
        public const string CONTRACT_TYPE_OUT = "OUT";
        public const string CONTRACT_TYPE_IN  = "IN";
        FwApplicationConfig ApplicationConfig;
        //----------------------------------------------------------------------------------------------------
        public RwAppData(FwApplicationConfig applicationConfig) 
        {
            this.ApplicationConfig = applicationConfig;
        }
        //----------------------------------------------------------------------------------------------------
        public string GetUsersId(dynamic session)
        {
            return session.security.webUser.usersid;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<string> GetLocationIdAsync(dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                var userLocation = await this.GetUserLocationAsync(conn, this.GetUsersId(session));
                return userLocation.locationId; 
            }
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<string> GetWarehouseIdAsync(dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                var userLocation = await this.GetUserLocationAsync(conn, this.GetUsersId(session));
                return userLocation.warehouseId; 
            }
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> GetDepartmentLocationAsync(FwSqlConnection conn, string departmentId, string locationId)
        {
            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                qry.Add("select useresponsibleperson");
                qry.Add("from deptloc with (nolock)");
                qry.Add("where locationid   = @locationid");
                qry.Add("  and departmentid = @departmentid");
                qry.AddParameter("@locationid", locationId);
                qry.AddParameter("@departmentid", departmentId);
                await qry.ExecuteAsync();
                dynamic result = new ExpandoObject();
                result.useresponsibleperson = qry.GetField("useresponsibleperson").ToBoolean();
                return result;
            }
        }
        //----------------------------------------------------------------------------------------------------
        public static string GetSqlModuleType(ModuleType moduleType)
        {
            string sqlModuleType = string.Empty;
            switch (moduleType)
            {
                case ModuleType.Order:      { sqlModuleType = "O"; break; }
                case ModuleType.Transfer:   { sqlModuleType = "T"; break; }
                case ModuleType.Truck:      { sqlModuleType = "P"; break; }
                case ModuleType.SubReceive: { sqlModuleType = "RECEIVE"; break; }
                case ModuleType.SubReturn:  { sqlModuleType = "RETURN"; break; }
                default:                    { sqlModuleType = "";  break; }
            }
            return sqlModuleType;
        }
        //----------------------------------------------------------------------------------------------------
        public static string GetSqlActivityType(ActivityType activityType)
        {
            string sqlActivityType = string.Empty;
            switch (activityType)
            {
                case ActivityType.CheckIn:    { sqlActivityType = "I";          break; }
                case ActivityType.Staging:    { sqlActivityType = "O";          break; }
                case ActivityType.SubReceive: { sqlActivityType = "SubReceive"; break; }
                case ActivityType.SubReturn:  { sqlActivityType = "SubReturn";  break; }
                default:                      { sqlActivityType = "";           break; }
            }
            return sqlActivityType;
        }
        //----------------------------------------------------------------------------------------------------
        public static string GetSqlCheckInMode(CheckInMode checkInMode)
        {
            string sqlCheckInMode;
            switch (checkInMode)
            {
                case CheckInMode.SingleOrder: { sqlCheckInMode = "O"; break; }
                case CheckInMode.MultiOrder:  { sqlCheckInMode = "M"; break; }
                case CheckInMode.Session:     { sqlCheckInMode = "S"; break; }
                case CheckInMode.Deal:        { sqlCheckInMode = "D"; break; }
                default:                      { sqlCheckInMode = "";  break; }
            }
            return sqlCheckInMode;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task CheckInQtyAsync(FwSqlConnection conn, string contractId, string orderId, string usersId, string vendorId, string consignorId, string masterItemId, string masterId, 
            string parentId, string description, int orderTranId, string internalChar, string aisle, string shelf, decimal qty, string containeritemid, string containeroutcontractid)
        {
            using (FwSqlCommand sp = new FwSqlCommand(conn, "dbo.checkinqty", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                sp.AddParameter("@contractid", contractId);
                sp.AddParameter("@orderid", orderId);
                sp.AddParameter("@usersid", usersId);
                sp.AddParameter("@vendorid", vendorId);
                sp.AddParameter("@consignorid", consignorId);
                sp.AddParameter("@masteritemid", masterItemId);
                sp.AddParameter("@masterid", masterId);
                sp.AddParameter("@parentid", parentId);
                sp.AddParameter("@description", description);
                sp.AddParameter("@ordertranid", orderTranId);
                sp.AddParameter("@internalchar", internalChar);
                sp.AddParameter("@aisle", aisle);
                sp.AddParameter("@shelf", shelf);
                sp.AddParameter("@qty", qty);
                sp.AddParameter("@containeritemid", containeritemid);
                sp.AddParameter("@containeroutcontractid", containeroutcontractid);
                await sp.ExecuteNonQueryAsync();
            }
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> CheckInGetCounts(FwSqlConnection conn, string inContractId, string orderId, string masterId, string masterItemId, string warehouseId)
        {
            dynamic result;
            FwSqlCommand sp;
            sp = new FwSqlCommand(conn, "dbo.checkingetcounts", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@incontractid", inContractId);
            sp.AddParameter("@orderid",      orderId);
            sp.AddParameter("@masterid",     masterId);
            sp.AddParameter("@masteritemid", masterItemId);
            sp.AddParameter("@warehouseid",  warehouseId);
            sp.AddParameter("@qtyordered",   SqlDbType.Decimal, ParameterDirection.Output);
            sp.AddParameter("@subqty",       SqlDbType.Decimal, ParameterDirection.Output);
            sp.AddParameter("@stillout",     SqlDbType.Decimal, ParameterDirection.Output);
            sp.AddParameter("@totalin",      SqlDbType.Decimal, ParameterDirection.Output);
            sp.AddParameter("@sessionin",    SqlDbType.Decimal, ParameterDirection.Output);
            await sp.ExecuteNonQueryAsync();
            result = new ExpandoObject();
            result.qtyOrdered = sp.GetParameter("@qtyordered").ToInt32();
            result.subQty     = sp.GetParameter("@subqty").ToInt32();
            result.stillOut   = sp.GetParameter("@stillout").ToInt32();
            result.totalIn    = sp.GetParameter("@totalin").ToInt32();
            result.sessionIn  = sp.GetParameter("@sessionin").ToInt32();
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> PdaCheckInGetNonBcInfoAsync(FwSqlConnection conn, string code, string inContractId, string usersId, string orderId, string dealId, string departmentId)
        {
            dynamic result;
            FwSqlCommand sp;
            sp = new FwSqlCommand(conn, "dbo.pdacheckingetnonbcinfo", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@code",         code);
            sp.AddParameter("@incontractid", inContractId);
            sp.AddParameter("@usersid",      usersId);
            sp.AddParameter("@orderid",      orderId);
            sp.AddParameter("@dealid",       dealId);
            sp.AddParameter("@departmentid", departmentId);
            sp.AddParameter("@masterid",     SqlDbType.Char,    ParameterDirection.Output);
            sp.AddParameter("@masteritemid", SqlDbType.Char,    ParameterDirection.Output);
            sp.AddParameter("@warehouseid",  SqlDbType.Char,    ParameterDirection.Output);
            sp.AddParameter("@masterno",     SqlDbType.Char,    ParameterDirection.Output);
            sp.AddParameter("@description",  SqlDbType.Char,    ParameterDirection.Output);
            sp.AddParameter("@issales",      SqlDbType.Char,    ParameterDirection.Output);
            sp.AddParameter("@status",       SqlDbType.Int,     ParameterDirection.Output);
            sp.AddParameter("@msg",          SqlDbType.VarChar, ParameterDirection.Output);
            await sp.ExecuteNonQueryAsync();
            result = new ExpandoObject();
            result.masterId     = sp.GetParameter("@masterid").ToString().TrimEnd();
            result.masterItemId = sp.GetParameter("@masteritemid").ToString().TrimEnd();
            result.warehouseId  = sp.GetParameter("@warehouseid").ToString().TrimEnd();
            result.masterNo     = sp.GetParameter("@masterno").ToString().TrimEnd();
            result.description  = sp.GetParameter("@description").ToString().TrimEnd();
            result.isSales      = sp.GetParameter("@issales").ToString().TrimEnd();
            result.status       = sp.GetParameter("@status").ToInt32();
            result.msg          = sp.GetParameter("@msg").ToString().TrimEnd();
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> WebCheckInItemAsync(FwSqlConnection conn, string usersId, ModuleType moduleType, CheckInMode checkInMode, string code, string masterItemId, decimal qty, string newOrderAction, string containeritemid, string containeroutcontractid, string aisle, string shelf, string parentid, string vendorId, bool disablemultiorder, string contractId, string orderId, string dealId, string departmentId, string trackedby, string spaceid, string spacetypeid, string facilitiestypeid)
        {
            dynamic result;
            FwSqlCommand sp;
            sp = new FwSqlCommand(conn, "dbo.pdacheckinitem", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@code",                     code);
            sp.AddParameter("@usersid",                  usersId);
            sp.AddParameter("@qty",                      qty);
            sp.AddParameter("@neworderaction",           newOrderAction);
            sp.AddParameter("@moduletype",               RwAppData.GetSqlModuleType(moduleType));
            sp.AddParameter("@checkinmode",              RwAppData.GetSqlCheckInMode(checkInMode));
            sp.AddParameter("@containeritemid",          containeritemid);
            sp.AddParameter("@containeroutcontractid",   containeroutcontractid);
            sp.AddParameter("@trackedby",                trackedby);
            sp.AddParameter("@aisle",                    aisle);
            sp.AddParameter("@shelf",                    shelf);
            sp.AddParameter("@parentid",                 parentid);
            sp.AddParameter("@disablemultiorder",        FwConvert.LogicalToCharacter(disablemultiorder));
            sp.AddParameter("@spaceid",                  spaceid);
            sp.AddParameter("@spacetypeid",              spacetypeid);
            sp.AddParameter("@facilitiestypeid",         facilitiestypeid);
            sp.AddParameter("@vendorid",                 vendorId);
            sp.AddParameter("@vendor",                   SqlDbType.Char,       ParameterDirection.Output);
            sp.AddParameter("@incontractid",             SqlDbType.NVarChar,   ParameterDirection.InputOutput, contractId);
            sp.AddParameter("@orderid",                  SqlDbType.NVarChar,   ParameterDirection.InputOutput, orderId);
            sp.AddParameter("@dealid",                   SqlDbType.NVarChar,   ParameterDirection.InputOutput, dealId);
            sp.AddParameter("@departmentid",             SqlDbType.NVarChar,   ParameterDirection.InputOutput, departmentId);
            sp.AddParameter("@itemorderid",              SqlDbType.NVarChar,   ParameterDirection.Output);
            sp.AddParameter("@masteritemid",             SqlDbType.NVarChar,   ParameterDirection.InputOutput, masterItemId);
            sp.AddParameter("@masterid",                 SqlDbType.NVarChar,   ParameterDirection.Output);
            sp.AddParameter("@warehouseid",              SqlDbType.NVarChar,   ParameterDirection.Output);
            sp.AddParameter("@ordertranid",              SqlDbType.Int,        ParameterDirection.Output);
            sp.AddParameter("@internalchar",             SqlDbType.NVarChar,   ParameterDirection.Output);
            sp.AddParameter("@rentalitemid",             SqlDbType.NVarChar,   ParameterDirection.Output);
            sp.AddParameter("@isicode",                  SqlDbType.NVarChar,   ParameterDirection.Output);
            sp.AddParameter("@orderno",                  SqlDbType.NVarChar,   ParameterDirection.Output);
            sp.AddParameter("@masterno",                 SqlDbType.NVarChar,   ParameterDirection.Output);
            sp.AddParameter("@description",              SqlDbType.NVarChar,   ParameterDirection.Output);
            sp.AddParameter("@allowswap",                SqlDbType.NVarChar,   ParameterDirection.Output);
            sp.AddParameter("@qtyordered",               SqlDbType.Decimal,    ParameterDirection.Output);
            sp.AddParameter("@subqty",                   SqlDbType.Decimal,    ParameterDirection.Output);
            sp.AddParameter("@stillout",                 SqlDbType.Decimal,    ParameterDirection.Output);
            sp.AddParameter("@totalin",                  SqlDbType.Decimal,    ParameterDirection.Output);
            sp.AddParameter("@sessionin",                SqlDbType.Decimal,    ParameterDirection.Output);
            sp.AddParameter("@genericmsg",               SqlDbType.NVarChar,   ParameterDirection.Output);
            sp.AddParameter("@status",                   SqlDbType.Decimal,    ParameterDirection.Output);
            sp.AddParameter("@msg",                      SqlDbType.NVarChar,   ParameterDirection.Output);
            await sp.ExecuteNonQueryAsync();
            result = new ExpandoObject();
            result.contractId   = sp.GetParameter("@incontractid").ToString().TrimEnd();
            result.dealId       = sp.GetParameter("@dealid").ToString().TrimEnd();
            result.departmentId = sp.GetParameter("@departmentid").ToString().TrimEnd();
            result.itemOrderId  = sp.GetParameter("@itemorderid").ToString().TrimEnd();
            result.masterItemId = sp.GetParameter("@masteritemid").ToString().TrimEnd();
            result.masterId     = sp.GetParameter("@masterid").ToString().TrimEnd();
            result.warehouseId  = sp.GetParameter("@warehouseid").ToString().TrimEnd();
            result.rentalItemId = sp.GetParameter("@rentalitemid").ToString().TrimEnd();
            result.isICode      = sp.GetParameter("@isicode").ToBoolean();
            result.itemType     = (result.isICode ? ItemType.NonBarCoded : ItemType.BarCoded);
            result.orderNo      = sp.GetParameter("@orderno").ToString().TrimEnd();
            result.masterNo     = sp.GetParameter("@masterno").ToString().TrimEnd();
            result.description  = sp.GetParameter("@description").ToString().TrimEnd();
            result.allowSwap    = sp.GetParameter("@allowswap").ToBoolean();
            result.vendorId     = sp.GetParameter("@vendorid").ToString().TrimEnd();
            result.vendor       = sp.GetParameter("@vendor").ToString().TrimEnd();
            result.contractId   = sp.GetParameter("@incontractid").ToString().TrimEnd();
            result.qtyOrdered   = sp.GetParameter("@qtyordered").ToDecimal();
            result.subQty       = sp.GetParameter("@subqty").ToDecimal();
            result.stillOut     = sp.GetParameter("@stillout").ToDecimal();
            result.totalIn      = sp.GetParameter("@totalin").ToDecimal();
            result.sessionIn    = sp.GetParameter("@sessionin").ToDecimal();
            result.genericMsg   = sp.GetParameter("@genericmsg").ToString().TrimEnd();
            result.status       = sp.GetParameter("@status").ToDecimal();
            result.msg          = sp.GetParameter("@msg").ToString().TrimEnd();
            result.found        = !result.masterItemId.Equals("");
            result.showNewOrder = (result.status == 1005);
            result.sessionNo    = await this.GetSessionNoAsync(conn, result.contractId);
            result.parentid     = parentid;
            if (!string.IsNullOrEmpty(containeritemid))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select outqty = dbo.calmasteritemqtyout(@orderid, @masteritemid)");
                    qry.AddParameter("@orderid", orderId);
                    qry.AddParameter("@masteritemid", result.masterItemId);
                    await qry.ExecuteNonQueryAsync();
                    result.outqty = qry.GetField("outqty").ToDecimal();
                }
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> WebMoveBCLocationAsync(FwSqlConnection conn, string usersId, string barcode, string aisle, string shelf)
        {
            dynamic result;
            bool isLocation;
            FwSqlCommand sp;
            isLocation = false;
            sp = new FwSqlCommand(conn, "dbo.webmovebclocation", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@value",        barcode);
            sp.AddParameter("@usersid",      usersId);
            sp.AddParameter("@rentalitemid", "");
            sp.AddParameter("@aisle",        SqlDbType.NVarChar, ParameterDirection.InputOutput, aisle);
            sp.AddParameter("@shelf",        SqlDbType.NVarChar, ParameterDirection.InputOutput, shelf);
            sp.AddParameter("@islocation",   SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@isbarcode",    SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@status",       SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@msg",          SqlDbType.NVarChar, ParameterDirection.Output);
            await sp.ExecuteNonQueryAsync();
            result = new ExpandoObject();
            result.isLocation = sp.GetParameter("@islocation").ToBoolean();
            result.isBarcode = sp.GetParameter("@isbarcode").ToBoolean();
            if (isLocation)
            {
                result.aisle = sp.GetParameter("@aisle").ToString().TrimEnd();
                result.shelf = sp.GetParameter("@shelf").ToString().TrimEnd();
            }
            result.status = sp.GetParameter("@status").ToInt32();
            result.msg    = sp.GetParameter("@msg").ToString().TrimEnd();
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> WebGetItemStatusAsync(FwSqlConnection conn, string usersId, string barcode)
        {
            dynamic result;
            FwSqlCommand sp;
            sp = new FwSqlCommand(conn, "dbo.webgetitemstatus", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@code",            barcode);
            sp.AddParameter("@usersid",         usersId);
            sp.AddParameter("@masterno",        SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@masterid",        SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@description",     SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@orderid",         SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@orderno",         SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@orderdesc",       SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@dealid",          SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@dealno",          SqlDbType.VarChar,  ParameterDirection.Output);
            sp.AddParameter("@deal",            SqlDbType.VarChar,  ParameterDirection.Output);
            sp.AddParameter("@warehouseid",     SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@warehouse",       SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@vendorid",        SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@vendor",          SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@masteritemid",    SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@rentalitemid",    SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@departmentid",    SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@ordertranid",     SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@internalchar",    SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@itemstatus",      SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@statuscolor",     SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@statustextcolor", SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@rentalstatus",    SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@statusdate",      SqlDbType.DateTime, ParameterDirection.Output);
            sp.AddParameter("@retiredreason",   SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@isicode",         SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@availfor",        SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@trackedby",       SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@aisleloc",        SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@shelfloc",        SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@location",        SqlDbType.VarChar,  ParameterDirection.Output);
            sp.AddParameter("@setcharacter",    SqlDbType.VarChar,  ParameterDirection.Output);
            sp.AddParameter("@manifestvalue",   SqlDbType.Decimal,  ParameterDirection.Output);
            sp.Parameters["@manifestvalue"].Precision = 12;
            sp.Parameters["@manifestvalue"].Scale     = 3;
            sp.AddParameter("@detail01",        SqlDbType.VarChar,  ParameterDirection.Output);
            sp.AddParameter("@detail02",        SqlDbType.VarChar,  ParameterDirection.Output);
            sp.AddParameter("@detail03",        SqlDbType.VarChar,  ParameterDirection.Output);
            sp.AddParameter("@genericerror",    SqlDbType.VarChar,  ParameterDirection.Output);
            sp.AddParameter("@buyer",           SqlDbType.VarChar,  ParameterDirection.Output);
            sp.AddParameter("@manufacturer",    SqlDbType.VarChar,  ParameterDirection.Output);
            sp.AddParameter("@pono",            SqlDbType.VarChar,  ParameterDirection.Output);
            sp.AddParameter("@barcode",         SqlDbType.VarChar,  ParameterDirection.Output);
            sp.AddParameter("@mfgserial",       SqlDbType.VarChar,  ParameterDirection.Output);
            sp.AddParameter("@rfid",            SqlDbType.VarChar,  ParameterDirection.Output);
            sp.AddParameter("@itemclass",       SqlDbType.VarChar,  ParameterDirection.Output);
            sp.AddParameter("@conditionid",     SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@status",          SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@msg",             SqlDbType.NVarChar, ParameterDirection.Output);
            await sp.ExecuteNonQueryAsync();
            result              = new ExpandoObject();
            result.code         = barcode;
            result.isICode      = sp.GetParameter("@isicode").ToBoolean();
            result.warehouseId  = sp.GetParameter("@warehouseid").ToString().TrimEnd();
            result.masterId     = sp.GetParameter("@masterid").ToString().TrimEnd();
            result.masterNo     = sp.GetParameter("@masterno").ToString().TrimEnd();
            result.rentalitemid = sp.GetParameter("@rentalitemid").ToString().TrimEnd();
            result.description  = sp.GetParameter("@description").ToString().TrimEnd();
            result.rentalStatus = sp.GetParameter("@rentalstatus").ToString().TrimEnd();
            result.color        = sp.GetParameter("@statuscolor").ToHtmlColor();
            result.textcolor    = (sp.GetParameter("@statustextcolor").ToString().TrimEnd() == "B") ? "#000000" : "#FFFFFF";
            result.dealid       = sp.GetParameter("@dealid").ToString().TrimEnd();
            result.dealNo       = sp.GetParameter("@dealno").ToString().TrimEnd();
            result.deal         = sp.GetParameter("@deal").ToString().TrimEnd();
            result.departmentid = sp.GetParameter("@departmentid").ToString().TrimEnd();
            result.ordertranid  = sp.GetParameter("@ordertranid").ToInt32();
            result.internalchar = sp.GetParameter("@internalchar").ToString().TrimEnd();
            result.orderid      = sp.GetParameter("@orderid").ToString().TrimEnd();
            result.orderNo      = sp.GetParameter("@orderno").ToString().TrimEnd();
            result.orderDesc    = sp.GetParameter("@orderdesc").ToString().TrimEnd();
            result.statusDate   = sp.GetParameter("@statusdate").ToShortDateString();
            result.trackedby    = sp.GetParameter("@trackedby").ToString().TrimEnd();
            result.aisleloc     = sp.GetParameter("@aisleloc").ToString().TrimEnd();
            result.shelfloc     = sp.GetParameter("@shelfloc").ToString().TrimEnd();
            result.location     = sp.GetParameter("@location").ToString().TrimEnd();
            result.setcharacter = sp.GetParameter("@setcharacter").ToString().TrimEnd();
            result.manifestvalue= sp.GetParameter("@manifestvalue").ToDecimal();
            result.detail01     = sp.GetParameter("@detail01").ToString().TrimEnd();
            result.detail02     = sp.GetParameter("@detail02").ToString().TrimEnd();
            result.detail03     = sp.GetParameter("@detail03").ToString().TrimEnd();
            result.genericError = sp.GetParameter("@genericerror").ToString().TrimEnd();
            result.buyer        = sp.GetParameter("@buyer").ToString().TrimEnd();
            result.manufacturer = sp.GetParameter("@manufacturer").ToString().TrimEnd();
            result.pono         = sp.GetParameter("@pono").ToString().TrimEnd();
            result.barcode      = sp.GetParameter("@barcode").ToString().TrimEnd();
            result.mfgserial    = sp.GetParameter("@mfgserial").ToString().TrimEnd();
            result.rfid         = sp.GetParameter("@rfid").ToString().TrimEnd();
            result.itemclass    = sp.GetParameter("@itemclass").ToString().TrimEnd();
            result.conditionid  = sp.GetParameter("@conditionid").ToString().TrimEnd();
            result.vendorid     = sp.GetParameter("@vendorid").ToString().TrimEnd();
            result.vendor       = sp.GetParameter("@vendor").ToString().TrimEnd();
            result.status       = sp.GetParameter("@status").ToInt32();
            result.msg          = sp.GetParameter("@msg").ToString().TrimEnd();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> WebSelectOrderAsync(FwSqlConnection conn, string usersId, string orderNo, ActivityType activityType, ModuleType moduleType)
        {
            dynamic result;
            FwSqlCommand sp;
            
            sp = new FwSqlCommand(conn, "dbo.webselectorder", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@orderno",         orderNo);
            sp.AddParameter("@activitytype",    GetSqlActivityType(activityType));
            sp.AddParameter("@ordertype",       GetSqlModuleType(moduleType));
            sp.AddParameter("@usersid",         usersId);
            sp.AddParameter("@orderid",         SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@dealid",          SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@departmentid",    SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@orderdesc",       SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@dealno",          SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@deal",            SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@fromwarehouseid", SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@fromwarehouse",   SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@warehouseid",     SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@warehouse",       SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@agent",           SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@status",          SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@msg",             SqlDbType.NVarChar, ParameterDirection.Output);
            await sp.ExecuteNonQueryAsync();
            result = new ExpandoObject();
            result.orderNo       = orderNo;
            result.orderId       = sp.GetParameter("@orderid").ToString().TrimEnd();
            result.dealId        = sp.GetParameter("@dealid").ToString().TrimEnd();
            result.departmentId  = sp.GetParameter("@departmentid").ToString().TrimEnd();
            result.orderDesc     = sp.GetParameter("@orderdesc").ToString().TrimEnd();
            result.dealNo        = sp.GetParameter("@dealno").ToString().TrimEnd();
            result.deal          = sp.GetParameter("@deal").ToString().TrimEnd();
            result.warehouse     = sp.GetParameter("@warehouse").ToString().TrimEnd();
            result.agent         = sp.GetParameter("@agent").ToString().TrimEnd();
            result.fromWarehouse = sp.GetParameter("@fromwarehouse").ToString().TrimEnd();
            result.status        = sp.GetParameter("@status").ToInt32();
            result.msg           = sp.GetParameter("@msg").ToString().TrimEnd();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> WebSelectPOAsync(FwSqlConnection conn, string usersId, string poNo, ModuleType moduleType)
        {
            dynamic result;
            FwSqlCommand sp;
            sp = new FwSqlCommand(conn, "dbo.webselectpo", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@pono",         poNo);
            sp.AddParameter("@potype",       GetSqlModuleType(moduleType));
            sp.AddParameter("@usersid",      usersId);
            sp.AddParameter("@poid",         SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@podesc",       SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@activitytype", SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@orderid",      SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@orderno",      SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@orderdesc",    SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@dealid",       SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@dealno",       SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@deal",         SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@vendorid",     SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@vendor",       SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@warehouseid",  SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@warehouse",    SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@status",       SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@msg",          SqlDbType.NVarChar, ParameterDirection.Output);
            await sp.ExecuteNonQueryAsync();
            result = new ExpandoObject();
            result.poId               = sp.GetParameter("@poid").ToString().TrimEnd();
            result.poNo               = poNo;
            result.poDesc             = sp.GetParameter("@podesc").ToString().TrimEnd();
            result.activityType       = sp.GetParameter("@activitytype").ToString().TrimEnd();
            result.orderId            = sp.GetParameter("@orderid").ToString().TrimEnd();
            result.orderNo            = sp.GetParameter("@orderno").ToString().TrimEnd();
            result.orderDesc          = sp.GetParameter("@orderdesc").ToString().TrimEnd();
            result.dealId             = sp.GetParameter("@dealid").ToString().TrimEnd();
            result.dealNo             = sp.GetParameter("@dealno").ToString().TrimEnd();
            result.deal               = sp.GetParameter("@deal").ToString().TrimEnd();
            result.warehouseId        = sp.GetParameter("@warehouseid").ToString().TrimEnd();
            result.warehouse          = sp.GetParameter("@warehouse").ToString().TrimEnd();
            result.vendorId           = sp.GetParameter("@vendorid").ToString().TrimEnd();
            result.vendor             = sp.GetParameter("@vendor").ToString().TrimEnd();
            result.status             = sp.GetParameter("@status").ToInt32();
            result.msg                = sp.GetParameter("@msg").ToString().TrimEnd();
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> WebSelectDealAsync(FwSqlConnection conn, string usersId, string dealNo)
        {
            dynamic result;
            FwSqlCommand sp;
            sp = new FwSqlCommand(conn, "dbo.webselectdeal", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@dealno",       dealNo);
            sp.AddParameter("@usersid",      usersId);
            sp.AddParameter("@dealid",       SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@departmentid", SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@dealdesc",     SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@department",   SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@status",       SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@msg",          SqlDbType.NVarChar, ParameterDirection.Output);
            await sp.ExecuteNonQueryAsync();
            result = new ExpandoObject();
            result.dealId       = sp.GetParameter("@dealid").ToString().TrimEnd();
            result.departmentId = sp.GetParameter("@departmentid").ToString().TrimEnd();
            result.dealNo       = dealNo;
            result.dealdesc     = sp.GetParameter("@dealdesc").ToString().TrimEnd();
            result.department   = sp.GetParameter("@department").ToString().TrimEnd();
            result.status       = sp.GetParameter("@status").ToInt32();
            result.msg          = sp.GetParameter("@msg").ToString().TrimEnd();
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> WebSelectSessionAsync(FwSqlConnection conn, string usersId, string sessionNo, ModuleType moduleType)
        {
            dynamic result;
            FwSqlCommand sp;
            sp = new FwSqlCommand(conn, "dbo.webselectsession", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@sessionno",     sessionNo);
            sp.AddParameter("@moduletype",    GetSqlModuleType(moduleType));
            sp.AddParameter("@usersid",       usersId);
            sp.AddParameter("@contractid",    SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@orderid",       SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@dealid",        SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@departmentid",  SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@orderno",       SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@orderdesc",     SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@dealno",        SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@deal",          SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@department",    SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@warehouseid",   SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@warehouse",     SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@fromwarehouse", SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@username",      SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@status",        SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@msg",           SqlDbType.NVarChar, ParameterDirection.Output);
            await sp.ExecuteNonQueryAsync();
            result = new ExpandoObject();
            result.sessionno     = sessionNo;
            result.contractid    = sp.GetParameter("@contractid").ToString().TrimEnd();
            result.orderid       = sp.GetParameter("@orderid").ToString().TrimEnd();
            result.dealid        = sp.GetParameter("@dealid").ToString().TrimEnd();
            result.departmentid  = sp.GetParameter("@departmentid").ToString().TrimEnd();
            result.orderno       = sp.GetParameter("@orderno").ToString().TrimEnd();
            result.orderdesc     = sp.GetParameter("@orderdesc").ToString().TrimEnd();
            result.dealno        = sp.GetParameter("@dealno").ToString().TrimEnd();
            result.deal          = sp.GetParameter("@deal").ToString().TrimEnd();
            result.warehouse     = sp.GetParameter("@warehouse").ToString().TrimEnd();
            result.fromwarehouse = sp.GetParameter("@fromwarehouse").ToString().TrimEnd();
            result.username      = sp.GetParameter("@username").ToString().TrimEnd();
            result.status        = sp.GetParameter("@status").ToInt32();
            result.msg           = sp.GetParameter("@msg").ToString().TrimEnd();
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        //public dynamic WebStageItem(FwSqlConnection conn, string orderId, string code, string masterItemId, string usersId, decimal qty, bool addItemToOrder, bool addCompleteToOrder, bool releaseFromRepair, bool unstageMode)
        //{
        //    dynamic result;
        //    FwSqlCommand sp;
        //    sp = new FwSqlCommand(conn, "dbo.webstageitem");
        //    sp.AddParameter("@orderid",                orderId);
        //    sp.AddParameter("@code",                   code);
        //    sp.AddParameter("@usersid",                usersId);
        //    sp.AddParameter("@qty",                    qty);
        //    sp.AddParameter("@additemtoorder",         ((addItemToOrder)     ? "T" : "F"));
        //    sp.AddParameter("@addcompletetoorder",     ((addCompleteToOrder) ? "T" : "F"));
        //    sp.AddParameter("@releasefromrepair",      ((releaseFromRepair)  ? "T" : "F"));
        //    sp.AddParameter("@unstage",                ((unstageMode)        ? "T" : "F"));
        //    sp.AddParameter("@isicode",                SqlDbType.Char,     ParameterDirection.Output);
        //    sp.AddParameter("@masterid",               SqlDbType.Char,     ParameterDirection.Output);
        //    sp.AddParameter("@masteritemid",           SqlDbType.Char,     ParameterDirection.InputOutput, masterItemId);
        //    sp.AddParameter("@rentalitemid",           SqlDbType.Char,     ParameterDirection.Output);
        //    sp.AddParameter("@masterno",               SqlDbType.Char,     ParameterDirection.Output);
        //    sp.AddParameter("@description",            SqlDbType.Char,     ParameterDirection.Output);
        //    sp.AddParameter("@qtyordered",             SqlDbType.Int,      ParameterDirection.Output);
        //    sp.AddParameter("@qtysub",                 SqlDbType.Int,      ParameterDirection.Output);
        //    sp.AddParameter("@qtystaged",              SqlDbType.Int,      ParameterDirection.Output);
        //    sp.AddParameter("@qtyout",                 SqlDbType.Int,      ParameterDirection.Output);
        //    sp.AddParameter("@qtyin",                  SqlDbType.Int,      ParameterDirection.Output);
        //    sp.AddParameter("@qtyremaining",           SqlDbType.Int,      ParameterDirection.Output);
        //    sp.AddParameter("@showadditemtoorder",     SqlDbType.Char,     ParameterDirection.Output);
        //    sp.AddParameter("@showaddcompletetoorder", SqlDbType.Char,     ParameterDirection.Output);
        //    sp.AddParameter("@showunstage",            SqlDbType.Char,     ParameterDirection.Output);
        //    sp.AddParameter("@genericmsg",             SqlDbType.Char,     ParameterDirection.Output);
        //    sp.AddParameter("@status",                 SqlDbType.Int,      ParameterDirection.Output);
        //    sp.AddParameter("@msg",                    SqlDbType.VarChar,  ParameterDirection.Output);
        //    sp.Execute();
        //    result = new ExpandoObject();
        //    result.isICode                = sp.GetParameter("@isicode").ToBoolean();
        //    result.masterId               = sp.GetParameter("@masterid").ToString().TrimEnd();
        //    result.masterItemId           = sp.GetParameter("@masteritemid").ToString().TrimEnd();
        //    result.rentalItemId           = sp.GetParameter("@rentalitemid").ToString().TrimEnd();
        //    result.masterNo               = sp.GetParameter("@masterno").ToString().Trim();
        //    result.description            = sp.GetParameter("@description").ToString().TrimEnd();
        //    result.qtyOrdered             = sp.GetParameter("@qtyordered").ToDecimal();
        //    result.qtySub                 = sp.GetParameter("@qtysub").ToDecimal();
        //    result.qtyStaged              = sp.GetParameter("@qtystaged").ToDecimal();
        //    result.qtyOut                 = sp.GetParameter("@qtyout").ToDecimal();
        //    result.qtyIn                  = sp.GetParameter("@qtyin").ToDecimal();
        //    result.qtyRemaining           = sp.GetParameter("@qtyremaining").ToDecimal() - sp.GetParameter("@qtysub").ToDecimal();
        //    result.showUnstage            = sp.GetParameter("@showunstage").ToBoolean();
        //    result.genericMsg             = sp.GetParameter("@genericmsg").ToString().TrimEnd();
        //    result.showAddItemToOrder     = sp.GetParameter("@showadditemtoorder").ToBoolean();
        //    result.showAddCompleteToOrder = sp.GetParameter("@showaddcompletetoorder").ToBoolean();
        //    result.itemType               = (result.isICode ? ItemType.NonBarCoded.ToString() : ItemType.BarCoded.ToString());
        //    result.status                 = sp.GetParameter("@status").ToInt32();
        //    result.msg                    = sp.GetParameter("@msg").ToString().TrimEnd();
        //    result.found                  = (!result.masterItemId.Equals(""));
        //    return result;
        //}
        public async Task<dynamic> PdaStageItemAsync(FwSqlConnection conn, string orderid, string code, string masteritemid, string usersid, decimal qty, bool additemtoorder, 
            bool addcompletetoorder, bool releasefromrepair, bool unstage, string vendorid, decimal meter, string location, string spaceid, bool addcontainertoorder, 
            bool overridereservation, bool stageconsigned, bool transferrepair, bool removefromcontainer, string contractid, bool ignoresuspendedin, string consignorid, 
            string consignoragreementid, string spacetypeid, string facilitiestypeid)
        {
            dynamic result;
            FwSqlCommand sp;
            sp = new FwSqlCommand(conn, "dbo.pdastageitem", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@orderid",                 orderid);
            sp.AddParameter("@code",                    code);
            sp.AddParameter("@usersid",                 usersid);
            sp.AddParameter("@qty",                     qty);
            sp.AddParameter("@additemtoorder",          FwConvert.LogicalToCharacter(additemtoorder));
            sp.AddParameter("@addcompletetoorder",      FwConvert.LogicalToCharacter(addcompletetoorder));
            sp.AddParameter("@releasefromrepair",       FwConvert.LogicalToCharacter(releasefromrepair));
            sp.AddParameter("@unstage",                 FwConvert.LogicalToCharacter(unstage));
            sp.AddParameter("@vendorid",                vendorid);                                                                 //mv 08/09/2015 CAS-16066-F6P9
            sp.AddParameter("@meter",                   meter);                                                                    //mv 08/09/2015 CAS-16066-F6P9
            sp.AddParameter("@location",                location);                                                                 //mv 08/09/2015 CAS-16066-F6P9
            sp.AddParameter("@spaceid",                 spaceid);                                                                  //mv 08/09/2015 CAS-16066-F6P9
            sp.AddParameter("@spacetypeid",             spacetypeid);
            sp.AddParameter("@facilitiestypeid",        facilitiestypeid);
            sp.AddParameter("@addcontainertoorder",     FwConvert.LogicalToCharacter(addcontainertoorder));                        //mv 08/09/2015 CAS-16066-F6P9
            sp.AddParameter("@overridereservation",     FwConvert.LogicalToCharacter(overridereservation));                        //mv 08/09/2015 CAS-16066-F6P9
            sp.AddParameter("@stageconsigned",          FwConvert.LogicalToCharacter(stageconsigned));                             //mv 08/09/2015 CAS-16066-F6P9
            sp.AddParameter("@transferrepair",          FwConvert.LogicalToCharacter(transferrepair));                             //mv 08/09/2015 CAS-16066-F6P9
            sp.AddParameter("@removefromcontainer",     FwConvert.LogicalToCharacter(removefromcontainer));                        //mv 08/09/2015 CAS-16066-F6P9
            sp.AddParameter("@contractid",              contractid);                                                               //mv 08/09/2015 CAS-16066-F6P9 only supply a value if item should go out immediately
            sp.AddParameter("@ignoresuspendedin",       FwConvert.LogicalToCharacter(ignoresuspendedin));                          //mv 08/09/2015 CAS-16066-F6P9
            sp.AddParameter("@consignorid",             SqlDbType.Char,     ParameterDirection.InputOutput, consignorid);          //mv 08/09/2015 CAS-16066-F6P9 also used as an input value for quantity items
            sp.AddParameter("@consignoragreementid",    SqlDbType.Char,     ParameterDirection.InputOutput, consignoragreementid); //mv 08/09/2015 CAS-16066-F6P9 also used as an input value for quantity items
            sp.AddParameter("@exceptionbatchid",        SqlDbType.Char,     ParameterDirection.Output);                            //mv 08/09/2015 CAS-16066-F6P9
            sp.AddParameter("@isicode",                 SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@masterid",                SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@masteritemid",            SqlDbType.Char,     ParameterDirection.InputOutput, masteritemid);
            sp.AddParameter("@rentalitemid",            SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@masterno",                SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@description",             SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@qtyordered",              SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@qtysub",                  SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@qtystaged",               SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@qtyout",                  SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@qtyin",                   SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@qtyremaining",            SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@showadditemtoorder",      SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@showaddcompletetoorder",  SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@showunstage",             SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@showoverridereservation", SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@showstageconsigneditem",  SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@showtransferrepair",      SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@showaddcontainertoorder", SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@showsubstituteitem",      SqlDbType.Char,     ParameterDirection.Output); //my 04/25/2016 CAS-16752-C6M1
            sp.AddParameter("@showsubstitutecomplete",  SqlDbType.Char,     ParameterDirection.Output); //my 04/25/2016 CAS-16752-C6M1
            sp.AddParameter("@genericmsg",              SqlDbType.Char,     ParameterDirection.Output);
            sp.AddParameter("@status",                  SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@msg",                     SqlDbType.VarChar,  ParameterDirection.Output);
            await sp.ExecuteNonQueryAsync();
            result = new ExpandoObject();
            result.isICode                 = sp.GetParameter("@isicode").ToBoolean();
            result.masterId                = sp.GetParameter("@masterid").ToString().TrimEnd();
            result.masterItemId            = sp.GetParameter("@masteritemid").ToString().TrimEnd();
            result.rentalItemId            = sp.GetParameter("@rentalitemid").ToString().TrimEnd();
            result.masterNo                = sp.GetParameter("@masterno").ToString().Trim();
            result.description             = sp.GetParameter("@description").ToString().TrimEnd();
            result.qtyOrdered              = sp.GetParameter("@qtyordered").ToDecimal();
            result.qtySub                  = sp.GetParameter("@qtysub").ToDecimal();
            result.qtyStaged               = sp.GetParameter("@qtystaged").ToDecimal();
            result.qtyOut                  = sp.GetParameter("@qtyout").ToDecimal();
            result.qtyIn                   = sp.GetParameter("@qtyin").ToDecimal();
            result.qtyRemaining            = sp.GetParameter("@qtyremaining").ToDecimal() - sp.GetParameter("@qtysub").ToDecimal();
            result.genericMsg              = sp.GetParameter("@genericmsg").ToString().TrimEnd();
            result.showAddItemToOrder      = sp.GetParameter("@showadditemtoorder").ToBoolean();
            result.showAddCompleteToOrder  = sp.GetParameter("@showaddcompletetoorder").ToBoolean();
            result.showUnstage             = sp.GetParameter("@showunstage").ToBoolean();
            result.showoverridereservation = sp.GetParameter("@showoverridereservation").ToBoolean();
            result.showstageconsigneditem  = sp.GetParameter("@showstageconsigneditem").ToBoolean();
            result.showtransferrepair      = sp.GetParameter("@showtransferrepair").ToBoolean();
            result.showaddcontainertoorder = sp.GetParameter("@showaddcontainertoorder").ToBoolean();
            result.showsubstituteitem      = sp.GetParameter("@showsubstituteitem").ToBoolean();
            result.showsubstitutecomplete  = sp.GetParameter("@showsubstitutecomplete").ToBoolean();
            result.itemType                = (result.isICode ? ItemType.NonBarCoded.ToString() : ItemType.BarCoded.ToString());
            result.status                  = sp.GetParameter("@status").ToInt32();
            result.msg                     = sp.GetParameter("@msg").ToString().TrimEnd().Replace("Item(s) have successfully been removed from the Container.", string.Empty);
            result.found                   = (!result.masterItemId.Equals(""));
            result.consignorid             = sp.GetParameter("@consignorid").ToString().TrimEnd();
            result.consignoragreementid    = sp.GetParameter("@consignoragreementid").ToString().TrimEnd();
            result.exceptionbatchid        = sp.GetParameter("@exceptionbatchid").ToString().TrimEnd();
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> WebSubReceiveReturnItemAsync(FwSqlConnection conn, string poId, string code, string barcode, string usersId, decimal qty, string moduleType, bool assignBC, string contractId)
        {
            dynamic result;
            FwSqlCommand sp;
            sp = new FwSqlCommand(conn, "dbo.websubreceivereturnitem", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@poid",    poId);
            sp.AddParameter("@code",    code);
            sp.AddParameter("@barcode", barcode);
            sp.AddParameter("@usersid", usersId);
            sp.AddParameter("@qty",     qty);
            switch (moduleType)
            {
                case RwConstants.ModuleTypes.SubReceive:
                    sp.AddParameter("@activitytype", "RECEIVE"); 
                    break;
                case RwConstants.ModuleTypes.SubReturn: 
                    sp.AddParameter("@activitytype", "RETURN"); 
                    break;
            }
            sp.AddParameter("@assignbc",     ((assignBC) ? "T" : "F"));
            sp.AddParameter("@contractid",   SqlDbType.NVarChar, ParameterDirection.InputOutput, contractId);
            sp.AddParameter("@isbarcode",    SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@poitemid",     SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@description",  SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@qtyordered",   SqlDbType.Decimal,  ParameterDirection.Output);
            sp.AddParameter("@qtyinsession", SqlDbType.Decimal,  ParameterDirection.Output);
            sp.AddParameter("@qtyreceived",  SqlDbType.Decimal,  ParameterDirection.Output);
            sp.AddParameter("@qtyreturned",  SqlDbType.Decimal,  ParameterDirection.Output);
            sp.AddParameter("@qtyremaining", SqlDbType.Decimal,  ParameterDirection.Output);
            sp.AddParameter("@genericmsg",   SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@status",       SqlDbType.Decimal,  ParameterDirection.Output);
            sp.AddParameter("@msg",          SqlDbType.NVarChar, ParameterDirection.Output);
            await sp.ExecuteNonQueryAsync();
            result = new ExpandoObject();
            result.poItemId     = sp.GetParameter("@poitemid").ToString().TrimEnd();
            result.contractId   = sp.GetParameter("@contractid").ToString().TrimEnd();
            result.description  = sp.GetParameter("@description").ToString().TrimEnd();
            result.qtyOrdered   = sp.GetParameter("@qtyordered").ToDecimal();
            result.qtySession   = sp.GetParameter("@qtyinsession").ToDecimal();
            result.qtyReceived  = sp.GetParameter("@qtyreceived").ToDecimal();
            result.qtyReturned  = sp.GetParameter("@qtyreturned").ToDecimal();
            result.qtyRemaining = sp.GetParameter("@qtyremaining").ToDecimal();
            result.genericMsg   = sp.GetParameter("@genericmsg").ToString().TrimEnd();
            result.status       = sp.GetParameter("@status").ToDecimal();
            result.msg          = sp.GetParameter("@msg").ToString().TrimEnd();
            result.isBarCode    = sp.GetParameter("@isbarcode").ToBoolean();
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> GetPOSubReceiveReturnPendingListAsync(FwSqlConnection conn, string moduleType, string poId, string warehouseId, string contractId, bool showAll)
        {
            string qtyremaining = string.Empty;
            FwJsonDataTable result = null;

            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                qry.Add("select  ");
                qry.Add("  masteritemid  = p.masteritemid");
                qry.Add(", masterno      = p.masterno");
                qry.Add(", description   = p.description");
                qry.Add(", linetype      = p.linetype");
                qry.Add(", poorderno     = p.poorderno");
                qry.Add(", trackedby     = p.trackedby");
                qry.Add(", netqtyordered = p.netqtyordered");
                qry.Add(", qtyreceived   = p.qtyreceived");
                qry.Add(", qtyreturned   = p.qtyreturned");
                qry.Add(", whcode        = p.whcode");
                qry.Add(", warehouse     = p.warehouse");
                if (moduleType == RwConstants.ModuleTypes.SubReceive) 
                {
                    qtyremaining = "(p.netqtyordered - p.qtyreceived)";
                    qry.Add(", qtyremaining  = " + qtyremaining);
                    qry.Add(", qtysession    = dbo.calqtyreceived(p.orderid, p.masteritemid, @contractid)");
                    qry.Add(" from   dbo.funcpoitem(@poid) p");
                    qry.Add(" where  p.rectype in ('R','S','P')");
                    qry.Add(" and    p.orderid     = @poid");
                    qry.Add(" and    p.warehouseid = @warehouseid");
                    qry.Add(" and (p.netqtyordered > (p.qtyreceived");
                    qry.Add("                            - dbo.calqtyreceived (p.orderid, p.masteritemid, @contractid)");
                    qry.Add("                              ))");
                    //if (!showAll) {
                    //    qry.Add("  and " + qtyremaining + " > 0");
                    //}
                    qry.Add(" order by p.itemorder");
                    qry.AddParameter("@poid", poId);
                    qry.AddParameter("@warehouseid", warehouseId);
                    qry.AddParameter("@contractid", contractId);
                } 
                else if (moduleType == RwConstants.ModuleTypes.SubReturn) 
                {
                    qtyremaining = "p.qtyreturnable";
                    qry.Add(", qtyremaining  = " + qtyremaining);
                    qry.Add(", qtysession    = dbo.calqtyreturned(p.orderid, p.masteritemid, @contractid)");
                    qry.Add(" from   dbo.funcpoitem(@poid) p");
                    qry.Add(" where  p.rectype in ('R','S','P')");
                    qry.Add(" and    p.warehouseid = @warehouseid");
                    qry.Add(" and (p.qtyreceived > (p.qtyreturned");
                    qry.Add("                            - isnull((select sum(ot.qty)");
                    qry.Add("                                      from  ordertran ot");
                    qry.Add("                                      where ot.orderid           = p.orderid");
                    qry.Add("                                        and ot.masteritemid      = p.masteritemid");
                    qry.Add("                                        and ot.inreturncontractid  = @contractid");
                    qry.Add("                                     ), 0.0)))");
                    //if (!showAll) {
                    //    qry.Add("  and " + qtyremaining + " > 0");
                    //}
                    qry.Add(" order by p.itemorder");
                    qry.AddParameter("@poid", poId);
                    qry.AddParameter("@warehouseid", warehouseId);
                    qry.AddParameter("@contractid", contractId);
                }
               
                result = await qry.QueryToFwJsonTableAsync(true);
            }
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> GetPoItemAsync(FwSqlConnection conn, string poId, string masterItemId, string contractId)
        {
            dynamic result = null;
            FwSqlCommand qry;
            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.AddColumn("masteritemid",  false);
            qry.AddColumn("masterno",      false);
            qry.AddColumn("description",   false);
            qry.AddColumn("linetype",      false);
            qry.AddColumn("poorderno",     false);
            qry.AddColumn("trackedby",     false);
            qry.AddColumn("netqtyordered", false);
            qry.AddColumn("qtyreceived",   false, FwDataTypes.Decimal);
            qry.AddColumn("qtyreturned",   false, FwDataTypes.Decimal);
            qry.AddColumn("qtyremaining",  false, FwDataTypes.Decimal);
            qry.AddColumn("qtysession",    false, FwDataTypes.Decimal);
            qry.AddColumn("whcode",        false);
            qry.AddColumn("warehouse",     false);
            qry.Add("select top 1");
            qry.Add("  masteritemid  = p.masteritemid");
            qry.Add(", masterno      = p.masterno");
            qry.Add(", description   = p.description");
            qry.Add(", linetype      = p.linetype");
            qry.Add(", poorderno     = p.poorderno");
            qry.Add(", trackedby     = p.trackedby");
            qry.Add(", netqtyordered = p.netqtyordered");
            qry.Add(", qtyreceived   = p.qtyreceived");
            qry.Add(", qtyreturned   = p.qtyreturned");
            qry.Add(", qtyremaining  = p.netqtyordered - p.qtyreceived");
            qry.Add(", qtysession    = dbo.calqtyreceived(p.orderid, p.masteritemid, @contractid)");
            qry.Add(", whcode        = p.whcode");
            qry.Add(", warehouse     = p.warehouse");
            qry.Add("from dbo.funcpoitem(@poid) p");
            qry.Add("where p.rectype in ('R','S','P')");
            qry.Add("  and p.orderid      = @poid");
            qry.Add("  and p.masteritemid = @masteritemid");
            qry.AddParameter("@poid",         poId);
            qry.AddParameter("@masteritemid", masterItemId);
            qry.AddParameter("@contractid",   contractId);
            await qry.ExecuteAsync();
            if (qry.RowCount > 0)
            {
                result = new ExpandoObject();
                result.masteritemid  = qry.GetField("masteritemid").ToString().TrimEnd();
                result.masterno      = qry.GetField("masterno").ToString().TrimEnd();
                result.description   = qry.GetField("description").ToString().TrimEnd();
                result.linetype      = qry.GetField("linetype").ToString().TrimEnd();
                result.poorderno     = qry.GetField("poorderno").ToString().TrimEnd();
                result.trackedby     = qry.GetField("trackedby").ToString().TrimEnd();
                result.netqtyordered = qry.GetField("netqtyordered").ToDecimal();
                result.qtyreceived   = qry.GetField("qtyreceived").ToDecimal();
                result.qtyreturned   = qry.GetField("qtyreturned").ToDecimal();
                result.qtyremaining  = qry.GetField("qtyremaining").ToDecimal();
                result.qtysession    = qry.GetField("qtysession").ToDecimal();
                result.whcode        = qry.GetField("whcode").ToString().TrimEnd();
                result.warehouse     = qry.GetField("warehouse").ToString().TrimEnd();
            }
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> WebGetPhyItemInfoAsync(FwSqlConnection conn, string usersId, string physicalId, string code)
        {
            dynamic result;
            FwSqlCommand sp;
            sp = new FwSqlCommand(conn, "dbo.webgetphyiteminfo", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@usersid",        usersId);
            sp.AddParameter("@physicalid",     physicalId);
            sp.AddParameter("@code",           code);
            sp.AddParameter("@masterno",       SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@master",         SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@isicode",        SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@showaddrep",     SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@masterid",       SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@rentalitemid",   SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@physicalitemid", SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@countedqty",     SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@genericmsg",     SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@status",         SqlDbType.Int,      ParameterDirection.Output);
            sp.AddParameter("@msg",            SqlDbType.NVarChar, ParameterDirection.Output);
            await sp.ExecuteNonQueryAsync();
            result = new ExpandoObject();
            result.isICode      = sp.GetParameter("@isicode").ToBoolean();
            result.showAddRep   = sp.GetParameter("@showaddrep").ToBoolean();
            result.masterNo     = sp.GetParameter("@masterno").ToString().TrimEnd();
            result.master       = sp.GetParameter("@master").ToString().TrimEnd();
            result.masterId     = sp.GetParameter("@masterid").ToString().TrimEnd();
            result.rentalItemId = sp.GetParameter("@rentalitemid").ToString().TrimEnd();
            if (sp.GetParameter("@physicalitemid").IsDbNull())
            {
                result.physicalItemId = -1;
            }
            else
            {
                result.physicalItemId = sp.GetParameter("@physicalitemid").ToInt32();
            }
            result.qty        = sp.GetParameter("@countedqty").ToInt32();
            result.genericMsg = sp.GetParameter("@genericmsg").ToString().TrimEnd();
            result.status     = sp.GetParameter("@status").ToInt32();
            result.msg        = sp.GetParameter("@msg").ToString().TrimEnd();
            result.itemType   = (result.isICode ? ItemType.NonBarCoded.ToString() : ItemType.BarCoded.ToString());
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> WebPhyCountItemAsync(FwSqlConnection conn, string physicalId, int physicalItemId, string rentalItemId, string masterId, bool isICode, string usersId, string addReplace, decimal qty)
        {
            dynamic result;
            FwSqlCommand sp;
            sp = new FwSqlCommand(conn, "dbo.webphycountitem", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@physicalid",     physicalId);
            sp.AddParameter("@physicalitemid", physicalItemId);
            sp.AddParameter("@rentalitemid",   rentalItemId);
            sp.AddParameter("@countedspaceid", "");
            sp.AddParameter("@masterid",       masterId);
            sp.AddParameter("@isicode",        FwConvert.LogicalToCharacter(isICode));
            sp.AddParameter("@usersid",        usersId);
            sp.AddParameter("@addreplace",     addReplace);
            sp.AddParameter("@qty",            SqlDbType.Int,      ParameterDirection.InputOutput, qty);
            sp.AddParameter("@genericmsg",     SqlDbType.NVarChar, ParameterDirection.Output,      40);
            sp.AddParameter("@status",         SqlDbType.Int,      ParameterDirection.Output,      64);
            sp.AddParameter("@msg",            SqlDbType.NVarChar, ParameterDirection.Output,      255);
            await sp.ExecuteNonQueryAsync();
            result = new ExpandoObject();
            result.qty        = sp.GetParameter("@qty").ToDecimal();
            result.status     = sp.GetParameter("@status").ToInt32();
            result.genericMsg = sp.GetParameter("@genericmsg").ToString().TrimEnd();
            result.msg        = sp.GetParameter("@msg").ToString().TrimEnd();
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<string> GetRepairIdAsync(FwSqlConnection conn, string repairno)
        {
            string repairid = string.Empty;
            FwSqlCommand qry;
            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select top 1 repairid");
            qry.Add("from repair with (nolock)");
            qry.Add("where repairno = @repairno");
            qry.AddParameter("@repairno", repairno);
            await qry.ExecuteNonQueryAsync();
            if (qry.RowCount > 0)
            {
                repairid = qry.GetField("repairid").ToString().TrimEnd();
            }
            return repairid;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> WebRepairItemAsync(FwSqlConnection conn, string code, RepairMode repairMode, string usersId, decimal qty) 
        {
            dynamic result;
            FwSqlCommand sp;
            sp = new FwSqlCommand(conn, "dbo.webrepairitem", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@code"        , code);
            sp.AddParameter("@repairmode"  , ((repairMode == RepairMode.Complete) ? "C" : (repairMode == RepairMode.Release) ? "R" : ""));
            sp.AddParameter("@usersid"     , usersId);
            sp.AddParameter("@qty"         , qty);
            sp.AddParameter("@repairno"    , SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@masterno"    , SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@master"      , SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@barcode"     , SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@rfid"        , SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@orderno"     , SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@orderdesc"   , SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@deal"        , SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@repairstatus", SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@qtyinrepair" , SqlDbType.Int     , ParameterDirection.Output);
            sp.AddParameter("@qtyreleased" , SqlDbType.Int     , ParameterDirection.Output);
            sp.AddParameter("@genericmsg"  , SqlDbType.NVarChar, ParameterDirection.Output);
            sp.AddParameter("@status"      , SqlDbType.Int     , ParameterDirection.Output);
            sp.AddParameter("@msg"         , SqlDbType.NVarChar, ParameterDirection.Output);
            await sp.ExecuteNonQueryAsync();
            result = new ExpandoObject();
            result.repairNo     = sp.GetParameter("@repairno").ToString().TrimEnd();
            result.repairId     = await this.GetRepairIdAsync(conn, result.repairNo);
            result.masterNo     = sp.GetParameter("@masterno").ToString().TrimEnd();
            result.master       = sp.GetParameter("@master").ToString().TrimEnd();
            result.barcode      = sp.GetParameter("@barcode").ToString().TrimEnd();
            result.orderNo      = sp.GetParameter("@orderno").ToString().TrimEnd();
            result.orderDesc    = sp.GetParameter("@orderdesc").ToString().TrimEnd();
            result.deal         = sp.GetParameter("@deal").ToString().TrimEnd();
            result.repairStatus = sp.GetParameter("@repairstatus").ToString().TrimEnd();
            result.isICode      = (result.barcode.Equals("")) && (!result.masterNo.Equals(""));
            if (sp.GetParameter("@qtyinrepair").IsDbNull()) 
            {
                result.qtyInRepair = 0;
            }
            else 
            {
                result.qtyInRepair = sp.GetParameter("@qtyinrepair").ToDecimal();
            }
            if (sp.GetParameter("@qtyreleased").IsDbNull()) 
            {
                result.qtyReleased = 0;
            }
            else 
            {
                result.qtyReleased = sp.GetParameter("@qtyreleased").ToDecimal();
            }
            result.genericMsg = sp.GetParameter("@genericmsg").ToString().TrimEnd();
            if (sp.GetParameter("@status").IsDbNull())
            {
                result.status = 1;
            }
            else 
            {
                result.status = sp.GetParameter("@status").ToInt32();
            }
            result.msg      = sp.GetParameter("@msg").ToString().TrimEnd();
            result.itemType = (result.isICode ? ItemType.NonBarCoded.ToString() : ItemType.BarCoded.ToString());
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> UpdateResponsiblePersonAsync(FwSqlConnection conn, string responsiblePersonId, string orderId)
        {
            dynamic result;
            FwSqlCommand qry;
            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("update dealorder");
            qry.Add("set responsiblepersonid = @responsiblepersonid");
            qry.Add("where orderid = @orderid");
            qry.AddParameter("@responsiblepersonid", responsiblePersonId);
            qry.AddParameter("@orderid", orderId);
            result = new ExpandoObject();
            result.rowsAffected = await qry.ExecuteNonQueryAsync();
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<List<dynamic>> GetResponsiblePersonsAsync(FwSqlConnection conn)
        {
            List<dynamic> rows;
            FwSqlCommand qry;
            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.AddColumn("person", false);
            qry.AddColumn("contactid", false);
            qry.Add("select text=person, value=contactid");
            qry.Add("from inventorycontactview");
            qry.Add("where responsibleperson = 'T'");
            qry.Add("order by person");
            rows = await qry.QueryToDynamicList2Async();
            return rows;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> GetUserLocationAsync(FwSqlConnection conn, string usersId)
        {
            dynamic result;
            FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select top 1 u.warehouseid, u.locationid, l.location");
            qry.Add("from users u with (nolock) join location l with (nolock) on (u.locationid = l.locationid)");
            qry.Add("where u.usersid = @usersid");
            qry.AddParameter("@usersid", usersId);
            await qry.ExecuteAsync();
            result = new ExpandoObject();
            result.warehouseId = qry.GetField("warehouseid").ToString().TrimEnd();
            result.locationId  = qry.GetField("locationid").ToString().TrimEnd();
            result.location    = qry.GetField("location").ToString().TrimEnd();
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> GetUserLocationAsync(dynamic session)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
            {
                return await this.GetUserLocationAsync(conn, session.security.webUser.usersid); 
            }
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> GetStagingPendingItemsAsync(FwSqlConnection conn, string orderId, string warehouseId, string contractId, string searchMode, string searchValue, int pageNo, int pageSize)
        {
            dynamic result;

            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.PageNo = pageNo;
                select.PageSize = pageSize;
                qry.AddColumn("exceptionflg",       false, FwDataTypes.Boolean);
                qry.AddColumn("someout",            false, FwDataTypes.Boolean);
                qry.AddColumn("qtyordered",         false, FwDataTypes.Decimal);
                qry.AddColumn("qtystagedandout",    false, FwDataTypes.Decimal);
                qry.AddColumn("qtyout",             false, FwDataTypes.Decimal);
                qry.AddColumn("qtysub",             false, FwDataTypes.Decimal);
                qry.AddColumn("qtysubstagedandout", false, FwDataTypes.Decimal);
                qry.AddColumn("qtysubout",          false, FwDataTypes.Decimal);
                qry.AddColumn("missingflg",         false, FwDataTypes.Boolean);
                qry.AddColumn("missingqty",         false, FwDataTypes.Decimal);
                select.Add("select *");
                select.Add("from dbo.funccheckoutexception2(@orderid, @warehouseid, @contractid)");
                select.Parse();
                select.AddWhere("exceptionflg = 'T'");
                select.AddWhere("(qtyordered > 0 or qtystagedandout > 0)");
                select.AddWhere("((itemclass = 'C') or (itemclass = 'K') or (itemclass = 'S') or (itemclass = 'N') or missingflg = 'T')");
                if (searchMode == "description" && searchValue != null && searchValue.Length > 0)
                {
                    string[] searchValues = searchValue.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < searchValues.Length; i++)
                    {
                        select.AddWhere($"description like @searchvalue{i}");
                        select.AddParameter($"@searchvalue{i}", $"%{searchValues[i]}%");
                    }
                }
                select.AddOrderBy("orderby");
                select.AddParameter("@orderid", orderId);
                select.AddParameter("@warehouseid", warehouseId);
                select.AddParameter("@contractid", contractId);
                result = new ExpandoObject();
                result = await qry.QueryToFwJsonTableAsync(select, true);
                return result;
            }
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> FuncCheckoutExceptionAsync(FwSqlConnection conn, string orderId, string warehouseId, string contractId, string masterItemId)
        {
            dynamic result;
            FwSqlCommand qry;
            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.AddColumn("orderid",            false);
            qry.AddColumn("masterid",           false);
            qry.AddColumn("parentid",           false);
            qry.AddColumn("masteritemid",       false);
            qry.AddColumn("exceptionflg",       false, FwDataTypes.Boolean);
            qry.AddColumn("someout",            false, FwDataTypes.Boolean);
            qry.AddColumn("masterno",           false);
            qry.AddColumn("description",        false);
            qry.AddColumn("vendor",             false);
            //qry.AddColumn("vendorid",           false);
            qry.AddColumn("qtyordered",         false, FwDataTypes.Decimal);
            qry.AddColumn("qtystagedandout",    false, FwDataTypes.Decimal);
            qry.AddColumn("qtyout",             false, FwDataTypes.Decimal);
            qry.AddColumn("qtyin",              false, FwDataTypes.Decimal);
            qry.AddColumn("qtysub",             false, FwDataTypes.Decimal);
            qry.AddColumn("qtysubstagedandout", false, FwDataTypes.Decimal);
            qry.AddColumn("qtysubout",          false, FwDataTypes.Decimal);
            qry.AddColumn("missingflg",         false, FwDataTypes.Boolean);
            qry.AddColumn("missingqty",         false, FwDataTypes.Decimal);
            qry.AddColumn("trackedby",          false);
            qry.AddColumn("rectype",            false);
            qry.AddColumn("itemclass",          false);
            qry.AddColumn("itemorder",          false);
            qry.AddColumn("orderby",            false);
            qry.AddColumn("optioncolor",        false);
            qry.AddColumn("warehouseid",        false);
            qry.AddColumn("whcode",             false);
            qry.Add("select top 1 *");
            qry.Add(", qtyin = dbo.calmasteritemqtyin2(@orderid, @masteritemid, '')");
            qry.Add("from dbo.funccheckoutexception2(@orderid, @warehouseid, @contractid)");
            qry.Add("where masteritemid = @masteritemid");
            qry.AddParameter("@orderid", orderId);
            qry.AddParameter("@warehouseid", warehouseId);
            qry.AddParameter("@contractid", contractId);
            qry.AddParameter("@masteritemid", masterItemId);
            result = new ExpandoObject();
            await qry.ExecuteAsync();
            result.orderId            = qry.GetField("orderid").ToString().TrimEnd();
            result.masterId           = qry.GetField("masterid").ToString().TrimEnd();
            result.parentId           = qry.GetField("parentid").ToString().TrimEnd();
            result.masterItemId       = qry.GetField("masteritemid").ToString().TrimEnd();
            result.exceptionFlg       = qry.GetField("exceptionflg").ToBoolean();
            result.someOut            = qry.GetField("someout").ToBoolean();
            result.masterNo           = qry.GetField("masterno").ToString().TrimEnd();
            result.description        = qry.GetField("description").ToString().TrimEnd();
            result.vendor             = qry.GetField("vendor").ToString().TrimEnd();
            //result.vendorId           = qry.GetField("vendorid").ToString().TrimEnd();
            result.subVendorId        = qry.GetField("subvendorid").ToString().TrimEnd();
            result.consignorId        = qry.GetField("consignorid").ToString().TrimEnd();
            result.qtyOrdered         = qry.GetField("qtyordered").ToDecimal();
            result.qtyStagedAndOut    = qry.GetField("qtystagedandout").ToDecimal();
            result.qtyOut             = qry.GetField("qtyout").ToDecimal();
            result.qtyIn              = qry.GetField("qtyin").ToDecimal();
            result.qtySub             = qry.GetField("qtysub").ToDecimal();
            result.qtySubStagedAndOut = qry.GetField("qtysubstagedandout").ToDecimal();
            result.qtySubOut          = qry.GetField("qtysubout").ToDecimal();
            result.missingFlg         = qry.GetField("missingflg").ToBoolean();
            result.missingQty         = qry.GetField("missingqty").ToDecimal();
            result.trackedBy          = qry.GetField("trackedby").ToString().TrimEnd();
            result.rectype            = qry.GetField("rectype").ToString().TrimEnd();
            result.itemClass          = qry.GetField("itemclass").ToString().TrimEnd();
            result.itemOrder          = qry.GetField("itemorder").ToString().TrimEnd();
            result.orderBy            = qry.GetField("orderby").ToString().TrimEnd();
            result.optionColor        = qry.GetField("optioncolor").ToString().TrimEnd();
            result.warehouseId        = qry.GetField("warehouseid").ToString().TrimEnd();
            result.whcode             = qry.GetField("whcode").ToString().TrimEnd();
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> GetContainerItemsFillContainerAsync(FwSqlConnection conn, string containeritemid)
        {
            dynamic result;
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.AddColumn("rentalitemid", false);
            qry.AddColumn("masteritemid", false);
            qry.AddColumn("masterid", false);
            qry.AddColumn("description", false);
            qry.AddColumn("masterno", false);
            qry.AddColumn("barcode", false);
            qry.AddColumn("qtyordered", false, FwDataTypes.Decimal);
            qry.AddColumn("outqty", false, FwDataTypes.Decimal);
            qry.AddColumn("vendorid", false);
            qry.AddColumn("vendor", false);
            qry.AddColumn("itemclass", false);
            qry.AddColumn("trackedby", false);
            qry.AddColumn("outcontractid", false);
            qry.Add("select");
            qry.Add("  v.rentalitemid,");
            qry.Add("  v.masteritemid,");
            qry.Add("  v.masterid,");
            qry.Add("  v.description,");
            qry.Add("  v.masterno,");
            qry.Add("  v.barcode,");
            qry.Add("  os.qtyordered,");
            qry.Add("  os.outqty,");
            qry.Add("  v.vendorid,");
            qry.Add("  v.vendor,");
            qry.Add("  v.itemclass,");
            qry.Add("  v.trackedby,");
            qry.Add("  v.outcontractid");
            qry.Add("from dbo.funcgetorderstatusdetail(@orderid, 'ORDER') v join dbo.funcorderstatus(@orderid) os on (v.masteritemid = os.masteritemid)");
            qry.Add("where  v.rectype = 'R' and ((v.itemstatus = 'O') or (v.itemstatus = 'S') or (v.issuspendin = 'T') or (v.itemclass = 'K'))");
            qry.Add("  and ((os.qtyordered = 0) or");
            qry.Add("       ((os.qtyordered > 0) and (os.outqty > 0)))");
            qry.Add("order by v.orderby, v.outdatetime, v.indatetime");
            qry.AddParameter("@orderid", containeritemid);
            result = await qry.QueryToFwJsonTableAsync();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> GetContainerItemsCheckInAsync(FwSqlConnection conn, string contractid, string containeritemid)
        {
            dynamic result;
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.AddColumn("description", false);
            qry.AddColumn("masterno", false);
            qry.AddColumn("barcode", false);
            qry.AddColumn("qtyordered", false, FwDataTypes.Decimal);
            qry.AddColumn("sessionin", false, FwDataTypes.Decimal);
            qry.AddColumn("vendor", false);
            qry.AddColumn("itemclass", false);
            qry.AddColumn("trackedby", false);
            qry.Add("select");
            qry.Add("  description,");
            qry.Add("  masterno,");
            qry.Add("  barcode,");
            qry.Add("  qtyordered,");
            qry.Add("  sessionin,");
            qry.Add("  vendor,");
            qry.Add("  itemclass,");
            qry.Add("  trackedby");
            qry.Add("from dbo.funccheckincontract(@contractid, 'CONTAINER')");
            qry.Add("where containeritemid = @containeritemid");
            qry.Add("order by orderby");
            qry.AddParameter("@contractid", contractid);
            qry.AddParameter("@containeritemid", containeritemid);
            result = await qry.QueryToFwJsonTableAsync();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        //public dynamic GetStagingStagedItem(FwSqlConnection conn, string orderId, string warehouseId, bool summary, string masterItemId)
        //{
        //    dynamic result;
        //    FwSqlCommand qry;
            
        //    qry = new FwSqlCommand(conn);
        //    qry.AddColumn("rectype", false);
        //    qry.AddColumn("masteritemid", false);
        //    qry.AddColumn("description", false);
        //    qry.AddColumn("masterno", false);
        //    qry.AddColumn("barcode", false);
        //    qry.AddColumn("quantity", false, FwJsonDataTableColumn.DataTypes.Decimal);
        //    qry.AddColumn("vendorid", false);
        //    qry.Add("select");
        //    qry.Add("  rectype,");
        //    qry.Add("  masteritemid,");
        //    qry.Add("  description,");
        //    qry.Add("  masterno,");
        //    qry.Add("  barcode,");
        //    qry.Add("  quantity,");
        //    qry.Add("  vendorid");
        //    qry.Add("from dbo.funcstageditemsweb(@orderid, @summary, @warehouseid)");
        //    qry.Add("where masteritemid = @masteritemid");
        //    qry.Add("order by orderby");
        //    qry.AddParameter("@orderid", orderId);
        //    qry.AddParameter("@summary", FwConvert.LogicalToCharacter(summary));
        //    qry.AddParameter("@warehouseid", warehouseId);
        //    qry.AddParameter("@masteritemid", masterItemId);
        //    qry.Execute();
        //    result = new ExpandoObject();
        //    result.masteritemid = qry.GetField("masteritemid").ToString().TrimEnd();
        //    result.description  = qry.GetField("description").ToString().TrimEnd();
        //    result.masterno     = qry.GetField("masterid").ToString().TrimEnd();
        //    result.barcode      = qry.GetField("barcode").ToString().TrimEnd();
        //    result.quantity     = qry.GetField("quantity").ToString().TrimEnd();
        //    result.vendorid     = qry.GetField("quantity").ToString().TrimEnd();
            
        //    return result;
        //}
        //----------------------------------------------------------------------------------------------------
        public async Task<bool> HasCheckinFillContainerButtonAsync(FwSqlConnection conn, string contractid)
        {
            bool showButton;
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select containercount = count(*)");
            qry.Add("from dbo.funccheckinexception(@contractid, @rectype, @containeritemid, @showall)");
            qry.Add("where itemclass='N'");
            qry.AddParameter("@contractid", contractid);
            qry.AddParameter("@rectype", "R");
            qry.AddParameter("@containeritemid", "");
            qry.AddParameter("@showall",         "F");
            await qry.ExecuteAsync();
            showButton = (qry.GetField("containercount").ToInt32() > 0);

            return showButton;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<bool> OrdertranExistsAsync(FwSqlConnection conn, string contractId, RwAppData.ActivityType activityType)
        {
            bool result;
            FwSqlCommand qry;
            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select ordertranexists = (case");
            switch(activityType)
            {
                case ActivityType.CheckIn:
                case ActivityType.SubReturn:
                    qry.Add("  when exists (select * from ordertran where inreturncontractid   = @contractid) then 'T'");
                    break;
                case ActivityType.Staging:
                case ActivityType.SubReceive:
                    qry.Add("  when exists (select * from ordertran where outreceivecontractid = @contractid) then 'T'");
                    break;
                default:
                    throw new Exception("Activity type not supported!");
            }
            qry.Add("  else 'F'");
            qry.Add("end)");
            qry.AddParameter("@contractid", contractId);
            await qry.ExecuteAsync();
            result = qry.GetField("ordertranexists").ToBoolean();
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> CreateOutContractAsync(FwSqlConnection conn, string usersid, string orderid, string notes)
        {
            dynamic result;
            FwSqlCommand sp;
            sp = new FwSqlCommand(conn, "dbo.createoutcontract", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@orderid", orderid);
            sp.AddParameter("@usersid", usersid);
            sp.AddParameter("@notes",   notes);
            sp.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Output);
            await sp.ExecuteAsync();
            result            = new ExpandoObject();
            result.contractId = sp.GetParameter("@contractid").ToString().Trim();
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> CreateInContractAsync(FwSqlConnection conn, string orderid, string dealid, string departmentid, string usersid)
        {
            dynamic result;
            FwSqlCommand sp;
            sp = new FwSqlCommand(conn, "dbo.createincontract", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@orderid",      orderid);
            sp.AddParameter("@dealid",       dealid);
            sp.AddParameter("@departmentid", departmentid);
            sp.AddParameter("@usersid",      usersid);
            sp.AddParameter("@contractid",   SqlDbType.NVarChar, ParameterDirection.Output);
            await sp.ExecuteAsync();
            result            = new ExpandoObject();
            result.contractId = sp.GetParameter("@contractid").ToString().Trim();
            result.sessionNo  = this.GetSessionNoAsync(conn, result.contractId);
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> CreateReceiveContractAsync(FwSqlConnection conn, string poid, string usersid)
        {
            dynamic result;
            FwSqlCommand sp;
            sp = new FwSqlCommand(conn, "dbo.createreceivecontract", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@poid",       poid);
            sp.AddParameter("@usersid",    usersid);
            sp.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Output);
            await sp.ExecuteAsync();
            result            = new ExpandoObject();
            result.contractId = sp.GetParameter("@contractid").ToString().Trim();
            result.sessionNo  = await this.GetSessionNoAsync(conn, result.contractId);
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> CreateReturnContractAsync(FwSqlConnection conn, string poid, string usersid)
        {
            dynamic result;
            FwSqlCommand sp;
            sp = new FwSqlCommand(conn, "dbo.createreturncontract", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@poid", poid);
            sp.AddParameter("@usersid",   usersid);
            sp.AddParameter("@contractid", SqlDbType.NVarChar, ParameterDirection.Output);
            await sp.ExecuteAsync();
            result            = new ExpandoObject();
            result.contractId = sp.GetParameter("@contractid").ToString().Trim();
            result.sessionNo  = await this.GetSessionNoAsync(conn, result.contractId);
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task SuspendContractAsync(FwSqlConnection conn, string contractid)
        {
            FwSqlCommand qry;
            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("update contract");
            qry.Add("set forcedsuspend = 'G'"); //created from guns
            qry.Add("where  contractid = @contractid");
            qry.AddParameter("@contractid", contractid);
            await qry.ExecuteAsync();
        }
        //----------------------------------------------------------------------------------------------------
        public async Task CancelContractAsync(FwSqlConnection conn, string contractid, string usersid, bool failSilentlyOnOwnershipErrors)
        {
            FwSqlCommand sp, qry;
            string inputbyusersid, namefml;

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select c.inputbyusersid, u.namefml");
            qry.Add("from contract c join usersview u on (c.inputbyusersid = u.usersid)");
            qry.Add("where contractid = @contractid");
            qry.AddParameter("@contractid", contractid);
            await qry.ExecuteAsync();
            inputbyusersid = qry.GetField("inputbyusersid").ToString().TrimEnd();
            namefml        = qry.GetField("namefml").ToString().TrimEnd();
            if (usersid != inputbyusersid)
            {
                if(!failSilentlyOnOwnershipErrors)
                {
                    throw new Exception("Unable to cancel contract created by another user.  Contract was created by: " + namefml);
                }
            } 
            else 
            {
                sp = new FwSqlCommand(conn, "dbo.cancelcontract", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                sp.AddParameter("@contractid", contractid);
                sp.AddParameter("@usersid", usersid);
                await sp.ExecuteAsync();
            }
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<string> GetSessionNoAsync(FwSqlConnection conn, string contractId)
        {
            string sessionNo = string.Empty;
            FwSqlCommand qry;
            if (!string.IsNullOrEmpty(contractId))
            {
                qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select top 1 sessionno");
                qry.Add("from contract with (nolock)");
                qry.Add("where contractid = @contractid");
                qry.AddParameter("@contractid", contractId);
                await qry.ExecuteAsync();
                sessionNo = qry.GetField("sessionno").ToString().TrimEnd();
            }
            return sessionNo;
        } 
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> FuncMasterWhAsync(FwSqlConnection conn, string masterid, string userswarehouseid, string filterwarehouseid, string currencyid)
        {
            dynamic result;
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select *");
            qry.Add("from funcmasterwh2(@masterid, @userswarehouseid, @filterwareouseid, @currencyid, 'T', 'T')");
            qry.Add("order by orderby");
            qry.AddParameter("@masterid",         masterid);
            qry.AddParameter("@userswarehouseid", userswarehouseid);
            qry.AddParameter("@filterwareouseid", filterwarehouseid);  // you can pass null if you want all warehouses
            qry.AddParameter("@currencyid",       currencyid);
            result = await qry.QueryToDynamicList2Async();
            
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        //public string GetMasterIdFromICode(string masterNo)
        //{
        //    string masterId = string.Empty;
        //    FwSqlCommand qry;
        //    qry = new FwSqlCommand();
        //    qry.Add("select masterid");
        //    qry.Add("from master");
        //    qry.Add("where masterno = @masterno");
        //    qry.AddParameter("@masterno", masterNo);
        //    qry.Execute();
        //    masterId = qry.GetField("masterid").ToString().TrimEnd();
        //    return masterId;
        //}
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> ReceiveItemAsync(FwSqlConnection conn, string contractId, string orderId, string masterItemId, string usersId, string barcode, decimal qty)
        {
            dynamic result;
            FwSqlCommand sp;
            sp = new FwSqlCommand(conn, "dbo.receiveitem", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@contractid",   contractId);
            sp.AddParameter("@orderid",      orderId);
            sp.AddParameter("@masteritemid", masterItemId);
            sp.AddParameter("@usersid",      usersId);
            sp.AddParameter("@barcode",      barcode);
            sp.AddParameter("@qty",          qty);
            sp.AddParameter("@status", SqlDbType.Int,     ParameterDirection.Output);
            sp.AddParameter("@msg",    SqlDbType.VarChar, ParameterDirection.Output);
            await sp.ExecuteAsync();
            result = new ExpandoObject();
            result.status = sp.GetParameter("@status").ToInt32();
            result.msg    = sp.GetParameter("@msg").ToString();
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> ReturnItemAsync(FwSqlConnection conn, string contractId, string orderId, string masterItemId, string usersId, string barcode, decimal qty)
        {
            dynamic result;
            FwSqlCommand sp;
            sp = new FwSqlCommand(conn, "dbo.returnitem", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@contractid",   contractId);
            sp.AddParameter("@orderid",      orderId);
            sp.AddParameter("@masteritemid", masterItemId);
            sp.AddParameter("@usersid",      usersId);
            sp.AddParameter("@barcode",      barcode);
            sp.AddParameter("@qty",          qty);
            sp.AddParameter("@outmasteritemid", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@status", SqlDbType.Int,     ParameterDirection.Output);
            sp.AddParameter("@msg",    SqlDbType.VarChar, ParameterDirection.Output);
            await sp.ExecuteAsync();
            result = new ExpandoObject();
            result.status = sp.GetParameter("@status").ToInt32();
            result.msg    = sp.GetParameter("@msg").ToString();
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> GetDealOrderByOrderNoAsync(FwSqlConnection conn, string orderNo)
        {
            FwJsonDataTable result;
            FwSqlCommand qry;
            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.AddColumn("orderid",  false);
            qry.Add("select top 1");
            qry.Add("  orderid       = do.orderid");
            qry.Add("from dealorder do");
            qry.Add("where do.orderno = @orderno");
            qry.AddParameter("@orderno", orderNo);
            result = await qry.QueryToFwJsonTableAsync();
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        //public FwJsonDataTable GetPoItemTotals(string poId, string poItemId, string masterItemId, string contractId)
        //{
        //    FwJsonDataTable result;
        //    FwSqlCommand qry;
        //    qry = new FwSqlCommand();
        //    qry.AddColumn("qtyreceived",  false);
        //    qry.AddColumn("qtyreturned",  false);
        //    qry.AddColumn("qtyremaining",  false);
        //    qry.AddColumn("qtyinsession",  false);
        //    qry.Add("select top 1");
        //    qry.Add(", qtyreceived  = do.qtyreceived");
        //    qry.Add(", qtyreturned  = do.qtyreturned");
        //    qry.Add(", qtyremaining = do.qtyremaining");

        //    qry.Add(", qtyinsession = (select sum(ot.qty)");
        //    qry.Add("                  from   ordertran  ot with (nolock),");
        //    qry.Add("                  masteritem mi with (nolock)");
        //    qry.Add("                  where  ot.orderid              = mi.orderid");
        //    qry.Add("                    and  ot.masteritemid         = mi.masteritemid");
        //    qry.Add("                    and  mi.poorderid            = @poid");
        //    qry.Add("                    and  mi.pomasteritemid       = @poitemid");
        //    qry.Add("                    and  ot.outreceivecontractid = @contractid)");

        //    qry.Add("from poitemtotalview p with (nolock)");
        //    qry.Add("where p.orderid      = @orderid");
        //    qry.Add("  and p.masteritemid = @masterItemId");
        //    qry.AddParameter("@poid",      poId);
        //    qry.AddParameter("@masterItemId", masterItemId);
        //    qry.AddParameter("@poitemid", poItemId);
        //    qry.AddParameter("@contractid", contractId);
        //    result = qry.QueryToFwJsonTable(0, 0, "");
        //    return result;
        //}
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> GetPrimaryAppImageThumbnailAsync(FwSqlConnection conn, string uniqueid1, string uniqueid2, string uniqueid3, string description, string rectype)
        {
            dynamic result;
            FwSqlCommand qry;
            FwJsonDataTable dt;

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.AddColumn("thumbnail", false, FwDataTypes.JpgDataUrl);
            qry.Add("select top 1 appimageid, thumbnail");
            qry.Add("from appimage");
            qry.Add("where uniqueid1   = @uniqueid1");
            qry.Add("  and uniqueid2   = @uniqueid2");
            qry.Add("  and uniqueid3   = @uniqueid3");
            qry.Add("  and description = @description");
            qry.Add("  and rectype     = @rectype");
            qry.Add("order by orderby");
            qry.AddParameter("@uniqueid1",   uniqueid1);
            qry.AddParameter("@uniqueid2",   uniqueid2);
            qry.AddParameter("@uniqueid3",   uniqueid3);
            qry.AddParameter("@description", description);
            qry.AddParameter("@rectype",     rectype);
            dt = await qry.QueryToFwJsonTableAsync();
            result = new ExpandoObject[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                result[i] = new ExpandoObject();
                result[i].appimageid = FwCryptography.AjaxEncrypt(dt.GetValue(i, "appimageid").ToString().TrimEnd());
                result[i].thumbnail = dt.GetValue(i, "thumbnail").ToString();
            }
            
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> GetUserAsync(FwSqlConnection conn, string usersId)
        {
            FwSqlCommand qry;
            List<dynamic> dataSet;
            dynamic result;

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select top 1 *");
            qry.Add("from users with(nolock)");
            qry.Add("where usersid = @usersid");
            qry.AddParameter("@usersid", usersId);
            dataSet = await qry.QueryToDynamicList2Async();
            if (dataSet.Count == 0)
            {
                throw new Exception("Can't find user.");
            }
            result = dataSet[0];

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> GetContactAsync(FwSqlConnection conn, string contactId)
        {
            FwSqlCommand qry;
            List<dynamic> rows;
            dynamic result;

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select top 1 *");
            qry.Add("from contact with(nolock)");
            qry.Add("where contactid = @contactid");
            qry.AddParameter("@contactid", contactId);
            rows = await qry.QueryToDynamicList2Async();
            if (rows.Count == 0)
            {
                throw new Exception("Can't find contact.");
            }
            result = rows[0];

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> AdvancedMoveNonBCAsync(FwSqlConnection conn, string orderid, string masteritemid, string vendorid, string contractid, string usersid, decimal qty, decimal movemode)
        {
            dynamic result;
            FwSqlCommand sp;
            sp = new FwSqlCommand(conn, "dbo.advancedmovenonbc", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@orderid",      orderid);
            sp.AddParameter("@masteritemid", masteritemid);
            sp.AddParameter("@vendorid",     vendorid);
            sp.AddParameter("@contractid",   contractid);
            sp.AddParameter("@usersid",      usersid);
            sp.AddParameter("@qty",          qty);
            sp.AddParameter("@movemode",     movemode);
            sp.AddParameter("@status", SqlDbType.Decimal, ParameterDirection.Output);
            sp.AddParameter("@msg",    SqlDbType.VarChar, ParameterDirection.Output);
            await sp.ExecuteAsync();
            result = new ExpandoObject();
            result.status = sp.GetParameter("@status").ToDecimal();
            result.msg    = sp.GetParameter("@msg").ToString();
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// //@movemode  0: to contract, 1: to staged, 2: staged to inventory, 3: contract to inventory, 4: either to inventory
        /// </summary>
        public async Task<dynamic> AdvancedMoveBarcodeAsync(FwSqlConnection conn, string orderid, string barcode, string contractid, string usersid, decimal movemode)
        {
            dynamic result;
            FwSqlCommand sp;
            sp = new FwSqlCommand(conn, "dbo.advancedmovebarcode", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@orderid",    orderid);
            sp.AddParameter("@barcode",    barcode);
            sp.AddParameter("@contractid", contractid);
            sp.AddParameter("@usersid",    usersid);
            sp.AddParameter("@movemode",   movemode);
            sp.AddParameter("@status", SqlDbType.Decimal, ParameterDirection.Output);
            sp.AddParameter("@msg",    SqlDbType.VarChar, ParameterDirection.Output);
            await sp.ExecuteAsync();
            result = new ExpandoObject();
            result.status = sp.GetParameter("@status").ToDecimal();
            result.msg    = sp.GetParameter("@msg").ToString();
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> AdvancedMoveSerialItemAsync(FwSqlConnection conn, string orderid, string serialno, string contractid, string usersid, decimal movemode)
        {
            dynamic result;
            FwSqlCommand sp;
            sp = new FwSqlCommand(conn, "dbo.advancedmoveserialitem", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@orderid",    orderid);
            sp.AddParameter("@serialno",   serialno);
            sp.AddParameter("@contractid", contractid);
            sp.AddParameter("@usersid",    usersid);
            sp.AddParameter("@movemode",   movemode);
            sp.AddParameter("@status", SqlDbType.Decimal, ParameterDirection.Output);
            sp.AddParameter("@msg",    SqlDbType.VarChar, ParameterDirection.Output);
            await sp.ExecuteAsync();
            result = new ExpandoObject();
            result.status = sp.GetParameter("@status").ToDecimal();
            result.msg    = sp.GetParameter("@msg").ToString();
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        // movemode: 0: to contract, 1: to staged, 2: staged to inventory, 3: contract to inventory, 4: either to inventory
        public async Task<dynamic> AdvancedMoveMasterItemIdAsync(FwSqlConnection conn, string contractid, string vendorid, string masteritemid, decimal qty, string usersid, decimal movemode)
        {
            dynamic result, masteritem;
            string trackedby, orderid, barcode, mfgserial;

            masteritem = await this.GetMasterItemViewByMasterItemIdAsync(conn, masteritemid);
            trackedby  = masteritem.trackedby;
            orderid    = masteritem.orderid;
            barcode    = masteritem.barcode;
            mfgserial  = masteritem.mfgserial;
            if (masteritem.rectype == "R")
            {
                switch(trackedby)
                {
                    case "BARCODE":
                        result = await this.AdvancedMoveBarcodeAsync(conn, orderid, barcode, contractid, usersid, movemode);
                        break;
                    case "QUANTITY":
                        result = await this.AdvancedMoveNonBCAsync(conn, orderid, masteritemid, vendorid, contractid, usersid, qty, movemode);
                        break;
                    case "SERIALNO":
                        result = await this.AdvancedMoveSerialItemAsync(conn, orderid, mfgserial, contractid, usersid, movemode);
                        break;
                    default:
                        throw new Exception("AdvancedMove doesn't support items that are tracked by '" + trackedby + "'.");
                }
            }
            else
            {
                result = await this.AdvancedMoveNonBCAsync(conn, orderid, masteritemid, vendorid, contractid, usersid, qty, movemode);
            }
            
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> GetMasterItemViewByMasterItemIdAsync(FwSqlConnection conn, string masteritemid)
        {
            dynamic result;
            List<dynamic> list;
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select top 1 *");
            qry.Add("from masteritemview with(nolock)");
            qry.Add("where masteritemid = @masteritemid");
            qry.AddParameter("@masteritemid", masteritemid);
            list = await qry.QueryToDynamicList2Async();
            if (list.Count.Equals(0))
            {
                throw new Exception("GetMasterItemViewByMasterItemId: masteritem '" + masteritemid + "' not found.");
            }
            else
            {
                result = list[0];
            }
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> IsQuantityAsync(FwSqlConnection conn, string masterno)
        {
            dynamic result;
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select isquantity = dbo.qsisquantity(@masterno)");
            qry.AddParameter("@masterno", masterno);
            await qry.ExecuteAsync();
            result = new ExpandoObject();
            result.isquantity = qry.GetParameter("@isquantity").ToString();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> GetItemAsync(FwSqlConnection conn, string masterno, string warehouseid)
        {
            dynamic result;
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select *");
            qry.Add("from   qsmaster(@masterno, @warehouseid)");
            qry.AddParameter("@masterno", masterno);
            qry.AddParameter("@warehouseid", warehouseid);
            result = await qry.QueryToDynamicObject2Async();
            if (Object.ReferenceEquals(null, result))
            {
                throw new Exception("GetItem: masterno " + masterno + " was not found!");
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> GetBarcodeSkipPrefixesAsync(FwSqlConnection conn, string warehouseid)
        {
            dynamic result;
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.AddColumn("barcodeskip", false);
            qry.Add("select barcodeskip");
            qry.Add("from  barcodeskip");
            // MV 2015-04-08 RW Mobile does it this way, but this would fail when scanning items from another warehouse
            // probably a non-issue if only USLG does this, but just in case I wanted to fix this, I confirmed this with Emil before making the change
            //qry.Add("where warehouseid = @warehouseid");
            //qry.AddParameter("@warehouseid", warehouseid);
            result = await qry.QueryToFwJsonTableAsync();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> GetSuspendedInContractsAsync(FwSqlConnection conn, RwAppData.ModuleType moduleType, string orderid, string usersid)
        {
            FwSqlCommand qry;
            dynamic location;
            string locationid;
            FwJsonDataTable result;

            location   = await GetUserLocationAsync(conn, usersid);
            locationid = location.locationId;
            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.AddColumn("statusdate", false, FwDataTypes.Date);
            qry.AddColumn("sessionno",  false, FwDataTypes.Text);
            qry.AddColumn("orderno",    false, FwDataTypes.Text);
            qry.AddColumn("orderdesc",  false, FwDataTypes.Text);
            qry.AddColumn("deal",       false, FwDataTypes.Text);
            qry.AddColumn("username",   false, FwDataTypes.Text);
            qry.AddColumn("status",     false, FwDataTypes.Text);
            qry.Add("select *");
            qry.Add("from suspendview v with (nolock)");
            switch(moduleType) 
            {
                case ModuleType.Order:
                    qry.Add("where v.contracttype = 'IN'");
                    qry.Add("  and v.ordertype    = 'O'");
                    qry.Add("  and v.locationid   = @locationid");
                    qry.Add("  and v.contractid in (select distinct contractid");
                    qry.Add("                       from ordercontractsuspendedview oc with (nolock)");
                    qry.Add("                       where oc.orderid in (@orderid))");
                    break;
                case ModuleType.Truck:
                    qry.Add("where v.contracttype = 'IN'");
                    qry.Add("  and v.ordertype    = 'P'");
                    qry.Add("  and v.locationid   = 'B0027KVM'");
                    qry.Add("  and v.dealid       = ''");
                    break;
                case ModuleType.Transfer:
                    qry.Add("where v.contracttype = 'RECEIPT'");
                    qry.Add("  and v.ordertype    = 'T'");
                    qry.Add("  and v.locationid   = @locationid");
                    qry.Add("  and v.contractid in (select distinct contractid");
                    qry.Add("                       from ordercontractsuspendedview oc with (nolock)");
                    qry.Add("                       where oc.orderid in (@orderid))");
                    break;
            }
            qry.Add("order by sessionno desc");
            qry.AddParameter("@locationid", locationid);
            qry.AddParameter("@orderid", orderid);
            result = await qry.QueryToFwJsonTableAsync(true);

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> CreateNewInContractAndSuspendAsync(FwSqlConnection conn, string orderid, string dealid, string departmentid, string usersid)
        {
            dynamic contract;
            
            contract = new ExpandoObject();
            contract = await this.CreateInContractAsync(conn, orderid, dealid, departmentid, usersid);
            await this.SuspendContractAsync(conn, contract.contractId);
            contract.sessionNo = await this.GetSessionNoAsync(conn, contract.contractId);
            
            return contract;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> GetResponsiblePersonAsync(FwSqlConnection conn, string orderid)
        {
            dynamic result;
            FwSqlCommand sp;

            sp = new FwSqlCommand(conn, "dbo.getresponsibleperson", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@orderid", orderid);
            sp.AddParameter("@showresponsibleperson", SqlDbType.Char,    ParameterDirection.Output);
            sp.AddParameter("@responsibleperson",     SqlDbType.VarChar, ParameterDirection.Output);
            sp.AddParameter("@responsiblepersonid",   SqlDbType.Char,    ParameterDirection.Output);
            await sp.ExecuteNonQueryAsync();
            result                       = new ExpandoObject();
            result.showresponsibleperson = sp.GetParameter("@showresponsibleperson").ToString().Trim();
            result.responsibleperson     = sp.GetParameter("@responsibleperson").ToString().Trim();
            result.responsiblepersonid   = sp.GetParameter("@responsiblepersonid").ToString().Trim();
            result.responsiblepersons    = await GetResponsiblePersonsAsync(conn);

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> CheckoutExceptionRFIDAsync(FwSqlConnection conn, string orderid, string warehouseid)
        {
            dynamic result;
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select *");
            qry.Add("from   dbo.funccheckoutexceptionrfid2(@orderid, @warehouseid)");
            qry.AddParameter("@orderid", orderid);
            qry.AddParameter("@warehouseid", warehouseid);
            result = await qry.QueryToDynamicList2Async();

            return result;
        }
        //------------------------------------------------------------------------------
        public async Task<string> LogRFIDTagsAsync(FwSqlConnection conn, string portal, string sessionid, string tags, string usersid, string rfidmode)
        {
            FwSqlCommand sp;
            string batchid;

            sp = new FwSqlCommand(conn, "dbo.logrfidtags", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@sessionid", sessionid);
            sp.AddParameter("@portal",    portal);
            sp.AddParameter("@tags",      tags);
            sp.AddParameter("@rfidmode",  rfidmode);
            sp.AddParameter("@usersid",   usersid);
            sp.AddParameter("@batchid",   System.Data.SqlDbType.VarChar, System.Data.ParameterDirection.Output, 30);
            await sp.ExecuteNonQueryAsync();
            batchid = sp.GetParameter("@batchid").ToString().TrimEnd();
            return batchid;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task ProcessScannedTagsAsync(FwSqlConnection conn, string portal, string sessionid, string batchid, string usersid, string rfidmode)
        {
            FwSqlCommand sp;

            sp = new FwSqlCommand(conn, "dbo.processscannedtags", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@sessionid", sessionid);
            sp.AddParameter("@portal",    portal);
            sp.AddParameter("@batchid",   batchid);
            sp.AddParameter("@usersid",   usersid);
            sp.AddParameter("@rfidmode",  rfidmode);
            await sp.ExecuteNonQueryAsync();
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> GetRFIDExceptionsAsync(FwSqlConnection conn, string sessionid, string portal, string usersid)
        {
            dynamic result;
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select *");
            qry.Add("from   dbo.funcscannedtagexception(@sessionid, @usersid, @portal)");
            qry.AddParameter("@sessionid", sessionid);
            qry.AddParameter("@usersid",   usersid);
            qry.AddParameter("@portal",    portal);
            result = await qry.QueryToDynamicList2Async();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task ClearRFIDExceptionAsync(FwSqlConnection conn, string sessionid, string tag)
        {
            FwSqlCommand sp;

            sp = new FwSqlCommand(conn, "dbo.scannedtagclearexception", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@sessionid", sessionid);
            sp.AddParameter("@tag",       tag);
            await sp.ExecuteNonQueryAsync();
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> CheckInExceptionRFIDAsync(FwSqlConnection conn, string sessionid)
        {
            dynamic result;
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select *");
            qry.Add("from   dbo.funccheckinexception(@sessionid, 'R')");
            qry.Add("where  exceptionflg = 'T'");
            qry.Add("order by orderno, itemorder, masterno");
            qry.AddParameter("@sessionid", sessionid);
            result = await qry.QueryToDynamicList2Async();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> ItemStatusRFIDAsync(FwSqlConnection conn, string tags)
        {
            dynamic result;
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select *");
            qry.Add("from   dbo.funcitemstatusrfid(@tags)");
            qry.AddParameter("@tags", tags);
            result = await qry.QueryToDynamicList2Async();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task RFIDClearSessionAsync(FwSqlConnection conn, string sessionid, string usersid)
        {
            FwSqlCommand sp;

            sp = new FwSqlCommand(conn, "dbo.scannedtagclearsession", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@sessionid", sessionid);
            sp.AddParameter("@usersid",   usersid);
            await sp.ExecuteNonQueryAsync();
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> ProcessAddItemToOrderAsync(FwSqlConnection conn, string orderid, string tag, string usersid)
        {
            dynamic result;
            FwSqlCommand sp;

            sp = new FwSqlCommand(conn, "dbo.processadditemtoorder", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@orderid", orderid);
            sp.AddParameter("@tag",     tag);
            sp.AddParameter("@usersid", usersid);
            sp.AddParameter("@status",  SqlDbType.Int,     ParameterDirection.Output);
            sp.AddParameter("@msg",     SqlDbType.VarChar, ParameterDirection.Output);
            await sp.ExecuteAsync();
            result        = new ExpandoObject();
            result.status = sp.GetParameter("@status").ToInt32();
            result.msg    = sp.GetParameter("@msg").ToString();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> ProcessAddCompleteToOrderAsync(FwSqlConnection conn, string orderid, string tag, string usersid, string autostageacc)
        {
            dynamic result;
            FwSqlCommand sp;

            sp = new FwSqlCommand(conn, "dbo.processaddcompletetoorder", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@orderid",      orderid);
            sp.AddParameter("@tag",          tag);
            sp.AddParameter("@usersid",      usersid);
            sp.AddParameter("@autostageacc", autostageacc);
            sp.AddParameter("@status",       SqlDbType.Int,     ParameterDirection.Output);
            sp.AddParameter("@msg",          SqlDbType.VarChar, ParameterDirection.Output);
            await sp.ExecuteAsync();
            result        = new ExpandoObject();
            result.status = sp.GetParameter("@status").ToInt32();
            result.msg    = sp.GetParameter("@msg").ToString();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> ProcessOverrideAvailabilityConflictAsync(FwSqlConnection conn, string orderid, string tag, string usersid)
        {
            dynamic result;
            FwSqlCommand sp;

            sp = new FwSqlCommand(conn, "dbo.processoverrideavailabilityconflict", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@orderid", orderid);
            sp.AddParameter("@tag",     tag);
            sp.AddParameter("@usersid", usersid);
            sp.AddParameter("@status",  SqlDbType.Int,     ParameterDirection.Output);
            sp.AddParameter("@msg",     SqlDbType.VarChar, ParameterDirection.Output);
            await sp.ExecuteAsync();
            result        = new ExpandoObject();
            result.status = sp.GetParameter("@status").ToInt32();
            result.msg    = sp.GetParameter("@msg").ToString();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> ProcessTransferItemInRepairAsync(FwSqlConnection conn, string orderid, string tag, string usersid)
        {
            dynamic result;
            FwSqlCommand sp;

            sp = new FwSqlCommand(conn, "dbo.processtransferiteminrepair", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@orderid", orderid);
            sp.AddParameter("@tag",     tag);
            sp.AddParameter("@usersid", usersid);
            sp.AddParameter("@status",  SqlDbType.Int,     ParameterDirection.Output);
            sp.AddParameter("@msg",     SqlDbType.VarChar, ParameterDirection.Output);
            await sp.ExecuteAsync();
            result        = new ExpandoObject();
            result.status = sp.GetParameter("@status").ToInt32();
            result.msg    = sp.GetParameter("@msg").ToString();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> CheckInBCAsync(FwSqlConnection conn, string contractid, string orderid, string masteritemid, int ordertranid, string internalchar, string vendorid, 
            string usersid, bool exchange, string location, string spaceid, string spacetypeid, string facilitiestypeid, string containeritemid, string containeroutcontractid, string aisle, string shelf) {
            dynamic result;
            FwSqlCommand sp;

            sp = new FwSqlCommand(conn, "dbo.checkinbc", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@contractid", contractid);
            sp.AddParameter("@orderid", orderid);
            sp.AddParameter("@masteritemid", masteritemid);
            sp.AddParameter("@ordertranid", ordertranid);
            sp.AddParameter("@internalchar", internalchar);
            sp.AddParameter("@vendorid", vendorid);
            sp.AddParameter("@usersid", usersid);
            sp.AddParameter("@exchange", FwConvert.LogicalToCharacter(exchange));
            sp.AddParameter("@location", location);
            sp.AddParameter("@spaceid", spaceid);
            sp.AddParameter("@spacetypeid", spacetypeid);
            sp.AddParameter("@facilitiestypeid", facilitiestypeid);
            sp.AddParameter("@containeritemid", containeritemid);
            sp.AddParameter("@containeroutcontractid", containeroutcontractid);
            sp.AddParameter("@aisle", aisle);
            sp.AddParameter("@shelf", shelf);
            await sp.ExecuteAsync();
            result = new ExpandoObject();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        //public static void CheckInBC(FwSqlConnection conn, string contractid, string orderid, string masteritemid, string ordertranid, 
        //    string internalchar, string vendorid, string usersid, bool exchange, string location, string spaceid, string spacetypeid,
        //    string facilitiestypeid, string containeritemid, string containeroutcontractid, string aisle, string shelf)
        //{
        //    FwSqlCommand sp;
        //    sp = new FwSqlCommand(conn, "dbo.checkinbc");
        //    sp.AddParameter("@contractid", contractid);
        //    sp.AddParameter("@orderid", orderid);
        //    sp.AddParameter("@masteritemid", masteritemid);
        //    sp.AddParameter("@ordertranid", ordertranid);
        //    sp.AddParameter("@internalchar", internalchar);
        //    sp.AddParameter("@vendorid", vendorid);
        //    sp.AddParameter("@usersid", usersid);
        //    sp.AddParameter("@exchange", exchange);
        //    sp.AddParameter("@location", location);
        //    sp.AddParameter("@spaceid", spaceid); // @spaceid takes precedence over @location
        //    sp.AddParameter("@spacetypeid", spacetypeid);
        //    sp.AddParameter("@facilitiestypeid", facilitiestypeid);
        //    sp.AddParameter("@containeritemid", containeritemid);
        //    sp.AddParameter("@containeroutcontractid", containeroutcontractid);
        //    sp.AddParameter("@aisle", aisle);
        //    sp.AddParameter("@shelf", shelf);
        //    sp.Execute();
        //}
        ////----------------------------------------------------------------------------------------------------
        //public static dynamic CheckInGetItemInfo(FwSqlConnection conn, string barcode, string incontractid, string usersid, string bctype, 
        //    string containeritemid, string orderid, string dealid, string departmentid, string masterid, string masteritemid, string rentalitemid)
        //{
        //    dynamic result;   
        //    FwSqlCommand sp;
        //    sp = new FwSqlCommand(conn, "dbo.checkingetiteminfo");
        //    sp.AddParameter("@barcode",            barcode);           // can be barcode, rfid, serial number, or rentalitemid
        //    sp.AddParameter("@incontractid",       incontractid);
        //    sp.AddParameter("@usersid",            usersid);
        //    sp.AddParameter("@bctype",             bctype);             //O=Order, T=Transfer, P=Package Truck
        //    sp.AddParameter("@containeritemid",    containeritemid);
        //    sp.AddParameter("@orderid",            SqlDbType.Char,    ParameterDirection.InputOutput, orderid);   //input/output
        //    sp.AddParameter("@dealid",             SqlDbType.Char,    ParameterDirection.InputOutput, dealid); //input/output
        //    sp.AddParameter("@departmentid",       SqlDbType.Char,    ParameterDirection.InputOutput, departmentid); //input/output
        //    sp.AddParameter("@masterid",           masterid);
        //    sp.AddParameter("@warehouseid",        SqlDbType.Char,    ParameterDirection.Output);
        //    sp.AddParameter("@ordertranid",        SqlDbType.Int,     ParameterDirection.Output);
        //    sp.AddParameter("@internalchar",       SqlDbType.Char,    ParameterDirection.Output);
        //    sp.AddParameter("@masteritemid",       SqlDbType.Char,    ParameterDirection.InputOutput, masteritemid);   //input/output
        //    sp.AddParameter("@parentid",           SqlDbType.Char,    ParameterDirection.Output);
        //    sp.AddParameter("@rentalitemid",       SqlDbType.Char,    ParameterDirection.InputOutput, rentalitemid); //input/output
        //    sp.AddParameter("@vendorid",           SqlDbType.Char,    ParameterDirection.Output);
        //    sp.AddParameter("@masterno",           SqlDbType.Char,    ParameterDirection.Output);
        //    sp.AddParameter("@description",        SqlDbType.VarChar, ParameterDirection.Output);
        //    sp.AddParameter("@vendor",             SqlDbType.Char,    ParameterDirection.Output);
        //    sp.AddParameter("@orderno",            SqlDbType.VarChar, ParameterDirection.Output);
        //    sp.AddParameter("@orderdesc",          SqlDbType.VarChar, ParameterDirection.Output);
        //    sp.AddParameter("@packageitemid",      SqlDbType.Char,    ParameterDirection.Output);
        //    sp.AddParameter("@itemclass",          SqlDbType.VarChar, ParameterDirection.Output);
        //    sp.AddParameter("@nestedmasteritemid", SqlDbType.Char,    ParameterDirection.Output);
        //    sp.AddParameter("@orderby",            SqlDbType.VarChar, ParameterDirection.Output);
        //    sp.AddParameter("@msg",                SqlDbType.VarChar, ParameterDirection.Output);
        //    sp.AddParameter("@status",             SqlDbType.Char,    ParameterDirection.Output);
        //    sp.Execute();
        //    result = new ExpandoObject();
        //    result.orderid            = sp.GetParameter("@orderid");
        //    result.dealid             = sp.GetParameter("@dealid");
        //    result.departmentid       = sp.GetParameter("@departmentid");
        //    result.warehouseid        = sp.GetParameter("@warehouseid");
        //    result.ordertranid        = sp.GetParameter("@ordertranid");
        //    result.internalchar       = sp.GetParameter("@internalchar");
        //    result.masteritemid       = sp.GetParameter("@masteritemid");
        //    result.parentid           = sp.GetParameter("@parentid");
        //    result.rentalitemid       = sp.GetParameter("@rentalitemid");
        //    result.vendorid           = sp.GetParameter("@vendorid");
        //    result.masterno           = sp.GetParameter("@masterno");
        //    result.description        = sp.GetParameter("@description");
        //    result.vendor             = sp.GetParameter("@vendor");
        //    result.orderno            = sp.GetParameter("@orderno");
        //    result.orderdesc          = sp.GetParameter("@orderdesc");
        //    result.packageitemid      = sp.GetParameter("@packageitemid");
        //    result.itemclass          = sp.GetParameter("@itemclass");
        //    result.nestedmasteritemid = sp.GetParameter("@nestedmasteritemid");
        //    result.orderby            = sp.GetParameter("@orderby");
        //    result.msg                = sp.GetParameter("@msg");
        //    result.status             = sp.GetParameter("@status");

        //    return result;
        //}
        ////----------------------------------------------------------------------------------------------------
        //public static dynamic CheckInGetCounts(FwSqlConnection conn, string incontractid, string orderid, string masterid, string masteritemid, 
        //    string warehouseid, string vendorid, bool quikin)
        //{
        //    dynamic result;
        //    FwSqlCommand sp;

        //    sp = new FwSqlCommand(conn, "dbo.checkingetcounts");
        //    sp.AddParameter("@incontractid", incontractid);
        //    sp.AddParameter("@orderid",      orderid);
        //    sp.AddParameter("@masterid",     masterid);
        //    sp.AddParameter("@masteritemid", masteritemid);
        //    sp.AddParameter("@warehouseid",  warehouseid);
        //    sp.AddParameter("@vendorid",     vendorid);
        //    sp.AddParameter("@quikin",       FwConvert.LogicalToCharacter(quikin));
        //    sp.AddParameter("@qtyordered",   SqlDbType.Decimal, ParameterDirection.Output);
        //    sp.AddParameter("@subqty",       SqlDbType.Decimal, ParameterDirection.Output);
        //    sp.AddParameter("@stillout",     SqlDbType.Decimal, ParameterDirection.Output);
        //    sp.AddParameter("@totalin",      SqlDbType.Decimal, ParameterDirection.Output);
        //    sp.AddParameter("@sessionin",    SqlDbType.Decimal, ParameterDirection.Output);
        //    sp.Execute();
        //    result = new ExpandoObject();
        //    result.qtyordered = sp.GetParameter("@qtyordered");
        //    result.subqty     = sp.GetParameter("@subqty");
        //    result.stillout   = sp.GetParameter("@stillout");
        //    result.totalin    = sp.GetParameter("@totalin");
        //    result.sessionin  = sp.GetParameter("@sessionin");

        //    return result;
        //}
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> RepairStatusRFIDAsync(FwSqlConnection conn, string tags)
        {
            dynamic result;
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select *");
            qry.Add("from   dbo.repairstatusrfid(@tags)");
            qry.Add("order by inrepair desc");
            qry.AddParameter("@tags", tags);
            result = await qry.QueryToDynamicList2Async();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> QCStatusRFIDAsync(FwSqlConnection conn, string tags)
        {
            dynamic result;
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select fis.*, ri.conditionid");
            qry.Add("from   dbo.funcitemstatusrfid(@tags) fis join rentalitem ri with (nolock) on (fis.rentalitemid = ri.rentalitemid)");
            qry.Add("order by qcrequired desc");
            qry.AddParameter("@tags", tags);
            result = await qry.QueryToDynamicList2Async();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> FillContainer_SelectContainerForFillContainerAsync(FwSqlConnection conn, string usersid, string user_warehouseid, string barcode, bool createcontract, string incontractid, dynamic checkingetiteminfo, string containerdesc)
        {
            dynamic result, createoutcontractresult, container;
            string notes, containeritem_warehouseid;
            bool hascontainerdefinition, hascontaineritem;

            result = new ExpandoObject();
            result.errormessage = string.Empty;
            // check if container instance (dealorder) exists for this barcode
            container = await this.FillContainer_FuncGetContainerAsync(conn, barcode, user_warehouseid);
            
            hascontainerdefinition   = (container != null);
            hascontaineritem =  hascontainerdefinition && (!string.IsNullOrEmpty(container.containeritemid));

            // if found containeritem (dealorder)
            if (hascontainerdefinition && hascontaineritem)
            {
                if (((container.statustype == RwConstants.RentalStatusType.INCONTAINER) && (container.containeritemid == container.outorderid)) ||
                    ((container.statustype == RwConstants.RentalStatusType.STAGED)      && (container.containeritemid == container.outorderid)) ||
                    ((container.statustype == RwConstants.RentalStatusType.IN)))
                {
                    //mv adaptation from jh 09/26/2012 CAS-10114-2YFM (item was transferred to this warehouse, but setup as a container in a different warehouse)
                    containeritem_warehouseid = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "dealorder", "orderid", container.containeritemid, "warehouseid");
                    if (containeritem_warehouseid != user_warehouseid)
                    {
                        await FwSqlCommand.SetDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "dealorder",  "orderid", container.containeritemid, "warehouseid", user_warehouseid);
                        await FwSqlCommand.SetDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "masteritem", "orderid", container.containeritemid, "warehouseid", user_warehouseid);
                        await FwSqlCommand.SetDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "masteritem", "orderid", container.containeritemid, "returntowarehouseid", user_warehouseid);
                    }
                    notes = string.Empty;
                    result.container = new ExpandoObject();
                    result.container.containerid           = container.containerid;
                    result.container.rentalitemid          = container.rentalitemid;
                    result.container.containeritemid       = container.containeritemid;
                    result.container.statustype            = container.statustype;
                    result.container.outorderid            = container.outorderid;
                    result.container.barcode               = barcode;
                    result.container.description           = container.description;
                    result.container.containerno           = container.containerno;
                    result.container.usecontainerno        = (container.usecontainerno == "T");
                    if (createcontract)
                    {
                        // look for an existing suspended session first
                        FwJsonDataTable dt = null;
                        using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                        {
                            qry.Add("select v.contractid");
                            qry.Add("from suspendview v with (nolock) join ordercontract oc on (v.contractid = oc.contractid)");
                            qry.Add("where v.contracttype = 'OUT'");
                            qry.Add("  and v.locationid = 'B001WH00'");
                            qry.Add("  and v.dealid     = ''");
                            qry.Add("  and v.ordertype  = 'N'");
                            qry.Add("  and v.contractid not in (select cs.containeroutcontractid from checkincontainersession cs with (nolock))");
                            qry.Add("  and oc.orderid = @orderid");
                            qry.Add("order by contractdate desc, contracttime desc");
                            qry.AddParameter("@orderid", container.containeritemid);
                            dt = await qry.QueryToFwJsonTableAsync();
                            if (dt.Rows.Count > 0)
                            {
                                result.container.outcontractid = dt.Rows[0][0].ToString().TrimEnd();
                            }
                        }
                        // if no suspended sessions than create an out contract
                        if ((dt == null || dt.Rows.Count == 0))
                        {
                            createoutcontractresult = await this.CreateOutContractAsync(conn, usersid, container.containeritemid, notes);
                            result.container.outcontractid = createoutcontractresult.contractId;
                        }
                        await this.PdaStageItemAsync(conn: conn,
                                                          orderid: result.container.containeritemid,
                                                          code: result.container.barcode,
                                                          masteritemid: string.Empty,
                                                          usersid: usersid,
                                                          qty: 1,
                                                          additemtoorder: false,
                                                          addcompletetoorder: false,
                                                          releasefromrepair: false,
                                                          unstage: false,
                                                          vendorid: string.Empty,
                                                          meter: 0,
                                                          location: string.Empty,
                                                          spaceid: string.Empty,
                                                          addcontainertoorder: false,
                                                          overridereservation: false,
                                                          stageconsigned: false,
                                                          transferrepair: false,
                                                          removefromcontainer: false,
                                                          contractid: result.container.outcontractid,
                                                          ignoresuspendedin: false,
                                                          consignorid: string.Empty,
                                                          consignoragreementid: string.Empty,
                                                          spacetypeid: string.Empty,
                                                          facilitiestypeid: string.Empty);
                    }
                }
                else
                {
                    //throw new Exception(string.Format("Item {0} cannot be used as a Container because its status is {1}.", barcode, container.statustype));
                    result.errormessage = string.Format("Item {0} cannot be used as a Container because its status is {1}.", barcode, container.statustype);
                }
            }
            // else didn't find container definition or containeritem (dealorder)
            else
            {
                // search for container definitions (master) for this barcode
                using (FwSqlCommand qrycontainerdefinitions = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qrycontainerdefinitions.Add("select ri.rentalitemid,");
                    qrycontainerdefinitions.Add("       ri.barcode,");
                    qrycontainerdefinitions.Add("       rs.statustype,");
                    qrycontainerdefinitions.Add("       ris.outorderid,");
                    qrycontainerdefinitions.Add("       containerid     = c.orderid,");
                    qrycontainerdefinitions.Add("       description     = c.orderdesc,");
                    qrycontainerdefinitions.Add("       cv.masterno");
                    qrycontainerdefinitions.Add("from  rentalitem ri  with (nolock) join rentalitemstatus ris with (nolock) on (ris.rentalitemid = ri.rentalitemid)");
                    qrycontainerdefinitions.Add("                                   join rentalstatus     rs  with (nolock) on (rs.rentalstatusid = ris.rentalstatusid)");
                    qrycontainerdefinitions.Add("                                   join master           m   with (nolock) on (m.masterid = ri.masterid)");
                    qrycontainerdefinitions.Add("                                   join dealorder        c   with (nolock) on (c.scannablemasterid = m.masterid)");
                    qrycontainerdefinitions.Add("                                   join containerview    cv  with (nolock) on ((cv.orderid = c.orderid) and (cv.warehouseid = ris.warehouseid))");
                    qrycontainerdefinitions.Add("where ris.warehouseid = @warehouseid");
                    qrycontainerdefinitions.Add("  and ri.barcode      = @barcode");
                    qrycontainerdefinitions.Add("order by description");
                    qrycontainerdefinitions.AddParameter("@warehouseid", user_warehouseid);
                    qrycontainerdefinitions.AddParameter("@barcode", barcode);
                    result.selectcontainers = await qrycontainerdefinitions.QueryToDynamicList2Async();
                    for(int i = 0; i < result.selectcontainers.Count; i++)
                    {
                        result.selectcontainers[0].containerid = FwCryptography.AjaxEncrypt(result.selectcontainers[0].containerid);
                    }
                }
                // no container definitions (master) found
                if ((result.selectcontainers as List<dynamic>).Count == 0)
                {
                    //throw new Exception("No containers have been configured to use this barcode.");
                    result.errormessage = "No containers have been configured to use this barcode.";
                }
                // 1 container definition (master) found
                else if ((result.selectcontainers as List<dynamic>).Count == 1)
                {
                    dynamic containerdefinition = (result.selectcontainers as List<dynamic>)[0];
                    if (containerdefinition.statustype == RwConstants.RentalStatusType.IN)
                    {
                        result.promptmakecontainer = "Item " + barcode + " is not yet established as a container.  Do you want to make it a container?";
                    }
                    else
                    {
                        result.errormessage = "Item " + barcode + " cannot be established as a Container because its status is " + containerdefinition.statustype + ".";
                    }
                }
                // // multiple container definitions (master) found
                else if ((result.selectcontainers as List<dynamic>).Count > 1)
                {
                    // response returns with a list of containers for the user to select
                    result.scannablemasterid = (await FwSqlCommand.GetDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "rentalitem", "barcode", barcode, "masterid")).ToString().TrimEnd();
                }
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> FillContainer_GetDefaultContainerDescCheckInAsync(FwSqlConnection conn, string barcode, string warehouseid)
        {
            var container = await this.FillContainer_FuncGetContainerAsync(conn, barcode, warehouseid);
            List<dynamic> containerdescs;
            dynamic result;

            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                qry.Add("select m.masterid, m.master");
                qry.Add("from  masterview m with (nolock)");
                qry.Add("where m.warehouseid  = @warehouseid");
                qry.Add("  and m.availfor in ('R')");
                qry.Add("  and m.availfrom in ('W')");
                qry.Add("  and m.class in ('N')");
                qry.Add("  and m.inactive   <> 'T'");
                qry.Add("  and m.scannablemasterid = @scannablemasteritem");
                qry.Add("order by master");
                qry.AddParameter("@warehouseid", warehouseid);
                qry.AddParameter("@scannablemasteritem", container.scannablemasterid);
                containerdescs = await qry.QueryToDynamicList2Async();
            }
            result = new ExpandoObject();
            result.scannablemasterid = container.scannablemasterid;
            result.masterid = string.Empty;
            result.master   = string.Empty;
            for (int rowno = 0; rowno < containerdescs.Count; rowno++)
            {
                if (containerdescs[rowno].master == container.description)
                {
                    result.masterid = containerdescs[rowno].masterid;
                    result.master   = containerdescs[rowno].master;
                    break;
                }
            }
            if (string.IsNullOrEmpty(result.masterid))
            {
                throw new Exception("masterid should not be blank! {1CCB6244-C6B1-4A19-89B8-FA2839BEB53C}");
            }
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> FillContainer_SelectContainerForCheckInAsync(FwSqlConnection conn, string usersid, string user_warehouseid, string barcode, bool createcontract, string incontractid, dynamic checkingetiteminfo, string selected_containerid, string selected_containerdesc, string orderid, string dealid, string departmentid)
        {
            dynamic result, createoutcontractresult, container,  emptycontainerresult, instantiatecontainerresult=null;
            string notes, containeritem_warehouseid;
            List<dynamic> containerdescs = null;
            bool hascontainerdesc, containernotnull, hascontaineritemid, changecontainertype;

            result = new ExpandoObject();
            result.errormessage = string.Empty;
            createoutcontractresult = null;

            // check if container instance (dealorder) exists for this barcode
            container = await this.FillContainer_FuncGetContainerAsync(conn, barcode, user_warehouseid);

            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                qry.Add("select m.containerid, m.master");
                qry.Add("from  masterview m with (nolock)");
                qry.Add("where m.warehouseid = @warehouseid");
                qry.Add("  and m.availfor in ('R')");
                qry.Add("  and m.availfrom in ('W')");
                qry.Add("  and m.class in ('N')");
                qry.Add("  and m.inactive <> 'T'");
                qry.Add("  and m.scannablemasterid = @scannablemasteritem");
                qry.Add("order by master");
                qry.AddParameter("@warehouseid", user_warehouseid);
                qry.AddParameter("@scannablemasteritem", container.scannablemasterid);
                containerdescs = await qry.QueryToDynamicList2Async();
            }
            hascontainerdesc    = !string.IsNullOrEmpty(selected_containerdesc);
            changecontainertype = (hascontainerdesc && (container.description != selected_containerdesc));
            containernotnull    = (container != null);
            hascontaineritemid  =  (!string.IsNullOrEmpty(container.containeritemid));

            if ((containerdescs.Count == 1) || hascontainerdesc)
            {
                if (!containernotnull)   throw new Exception("container not found! {D3194E78-A53F-4985-AA3A-3DDEE58012D5}");
                if (!hascontaineritemid) throw new Exception("containeritemid is blank! {A503CF6A-F7DC-44F8-98A8-3AED4A229031}"); 
                
                if (changecontainertype)
                {
                    using(FwSqlConnection conn2 = new FwSqlConnection(this.ApplicationConfig.DatabaseSettings.ConnectionString))
                    {
                        await conn2.OpenAsync();
                        using (SqlTransaction transaction = conn2.GetConnection().BeginTransaction())
                        {
                            using (FwSqlCommand sp = new FwSqlCommand(conn2, "emptycontainer", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                            {
                                sp.Transaction = transaction;
                                sp.AddParameter("@containerrentalitemid", container.rentalitemid);
                                sp.AddParameter("@deleteall", "T");
                                sp.AddParameter("@usersid", usersid);
                                sp.AddParameter("@incontractid", SqlDbType.Char, ParameterDirection.Output);
                                sp.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
                                sp.AddParameter("@msg", SqlDbType.VarChar, ParameterDirection.Output);
                                await sp.ExecuteNonQueryAsync();
                                emptycontainerresult = new ExpandoObject();
                                emptycontainerresult.incontractid = sp.GetParameter("@incontractid").ToString();
                                emptycontainerresult.status       = sp.GetParameter("@status").ToInt32();
                                emptycontainerresult.msg          = sp.GetParameter("@msg").ToString();
                            }
                            if (emptycontainerresult.status != 0)
                            {
                                result.errormessage = emptycontainerresult.msg;
                                transaction.Rollback();
                            }
                            else
                            {
                                using (FwSqlCommand qry = new FwSqlCommand(conn2, "dbo.instantiatecontainer", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                                {
                                    qry.Transaction = transaction;
                                    qry.AddParameter("@containerid", selected_containerid);
                                    qry.AddParameter("@rentalitemid", container.rentalitemid);
                                    qry.AddParameter("@autostageacc", "F");
                                    qry.AddParameter("@usersid",      usersid);
                                    qry.AddParameter("@fromcheckin",  "T");
                                    qry.AddParameter("@containeritemid", SqlDbType.VarChar, ParameterDirection.Output);
                                    qry.AddParameter("@contractid",      SqlDbType.VarChar, ParameterDirection.Output);
                                    await qry.ExecuteNonQueryAsync();
                                    instantiatecontainerresult = new ExpandoObject();
                                    instantiatecontainerresult.containeritemid = qry.GetParameter("@containeritemid").ToString().TrimEnd();
                                    instantiatecontainerresult.outcontractid   = qry.GetParameter("@contractid").ToString().TrimEnd();
                                }
                            }
                            transaction.Commit();
                        }
                    }
                    if (string.IsNullOrEmpty(result.errormessage))
                    {
                        container = await this.FillContainer_FuncGetContainerAsync(conn, barcode, user_warehouseid);
                        checkingetiteminfo = await this.CheckInGetItemInfoAsync(conn:            conn,
                                                                                     barcode:         barcode,
                                                                                     incontractid:    incontractid,
                                                                                     usersid:         usersid,
                                                                                     bctype:          "O",
                                                                                     containeritemid: container.containeritemid,
                                                                                     orderid:         orderid,
                                                                                     dealid:          dealid,
                                                                                     departmentid:    departmentid,
                                                                                     masteritemid:    "",
                                                                                     rentalitemid:    "");
                    }
                }

                if ((string.IsNullOrEmpty(result.errormessage)) &&
                    ((container.statustype == RwConstants.RentalStatusType.OUT) || 
                    (container.statustype == RwConstants.RentalStatusType.INCONTAINER)))
                {
                    //mv adaptation from jh 09/26/2012 CAS-10114-2YFM (item was transferred to this warehouse, but setup as a container in a different warehouse)
                    containeritem_warehouseid = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "dealorder", "orderid", container.containeritemid, "warehouseid");
                    if (containeritem_warehouseid != user_warehouseid)
                    {
                        await FwSqlCommand.SetDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "dealorder",  "orderid", container.containeritemid, "warehouseid", user_warehouseid);
                        await FwSqlCommand.SetDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "masteritem", "orderid", container.containeritemid, "warehouseid", user_warehouseid);
                        await FwSqlCommand.SetDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "masteritem", "orderid", container.containeritemid, "returntowarehouseid", user_warehouseid);
                    }
                    notes = string.Empty;
                    result.container = new ExpandoObject();
                    result.container.containerid           = container.containerid;
                    result.container.rentalitemid          = container.rentalitemid;
                    result.container.containeritemid       = container.containeritemid;
                    result.container.statustype            = container.statustype;
                    result.container.outorderid            = container.outorderid;
                    result.container.barcode               = barcode;
                    result.container.description           = container.description;
                    result.container.containerno           = container.containerno;
                    result.container.usecontainerno        = (container.usecontainerno == "T");
                    if (createcontract)
                    {
                        // create out contract
                        createoutcontractresult = this.CreateOutContractAsync(conn, usersid, container.containeritemid, notes);
                        result.container.outcontractid = createoutcontractresult.contractId;
                        if (changecontainertype)
                        {
                            using (FwSqlCommand sp = new FwSqlCommand(conn, "insertcheckincontainersession", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                            {
                                sp.AddParameter("@contractid", incontractid);
                                sp.AddParameter("@containeritemid", container.containeritemid);
                                sp.AddParameter("@rentalitemid", container.rentalitemid);
                                sp.AddParameter("@containeroutcontractid", createoutcontractresult.contractId);
                                await sp.ExecuteNonQueryAsync();
                            }
                        }
                        if ((checkingetiteminfo.status != 1002) && // Already in this Session
                            (checkingetiteminfo.status != 1011)) // primary item is already in the container
                        {
                            await this.CheckInBCAsync(conn:                   conn,
                                                           contractid:             incontractid,
                                                           orderid:                checkingetiteminfo.orderid,
                                                           masteritemid:           checkingetiteminfo.masteritemid,
                                                           ordertranid:            checkingetiteminfo.ordertranid,
                                                           internalchar:           checkingetiteminfo.internalchar,
                                                           vendorid:               checkingetiteminfo.vendorid,
                                                           usersid:                usersid,
                                                           exchange:               false,
                                                           location:               "",
                                                           spaceid:                "",
                                                           spacetypeid:            "",
                                                           facilitiestypeid:       "",
                                                           containeritemid:        result.container.containeritemid,
                                                           containeroutcontractid: result.container.outcontractid,
                                                           aisle:                  "",
                                                           shelf:                  "");
                        }
                    }
                }
                else
                {
                    result.errormessage = string.Format("Item {0} cannot be used as a Container because its status is {1}.", barcode, container.statustype);
                }
            }
            else
            {
                result.checkincontainers = containerdescs;
                result.defaultcontainerdesc = new ExpandoObject();
                result.scannablemasterid = container.scannablemasterid;
                result.defaultcontainerdesc.containerid = string.Empty;
                result.defaultcontainerdesc.master   = string.Empty;
                for (int rowno = 0; rowno < containerdescs.Count; rowno++)
                {
                    if (containerdescs[rowno].master == container.description)
                    {
                        result.defaultcontainerdesc.containerid = FwCryptography.AjaxEncrypt(containerdescs[rowno].containerid);
                        result.defaultcontainerdesc.master   = containerdescs[rowno].master;
                        break;
                    }
                }
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task MoveAllStagedItemsToContractAsync(FwSqlConnection conn, string orderid, string warehouseid, string contractid, string usersid)
        {
            FwSqlCommand qry;
            FwJsonDataTable dt;
            FwSqlSelect select;

            // get all the staged items on the order
            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            select = new FwSqlSelect();
            select.Add("select *");
            select.Add("from  dbo.funcstaged(@orderid,'F')");
            select.Add("where warehouseid = @warehouseid");
            select.Add("order by orderby");
            select.AddParameter("@orderid", orderid);
            select.AddParameter("@warehouseid", warehouseid);
            dt = await qry.QueryToFwJsonTableAsync(select, true);
            // Move any staged items to the contract (this was developed for fill container since you should never have staged items)
            for (int rowno = 0; rowno < dt.Rows.Count; rowno++)
            {
                string vendorid, masteritemid;
                decimal qty;

                vendorid     = dt.GetValue(rowno, "vendorid").ToString().TrimEnd();
                masteritemid = dt.GetValue(rowno, "masteritemid").ToString().TrimEnd();
                qty          = dt.GetValue(rowno, "quantity").ToDecimal();
                await AdvancedMoveMasterItemIdAsync(conn, contractid, vendorid, masteritemid, qty, usersid, 0);
            }

        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> InstantiateContainerAsync(FwSqlConnection conn, string containerid, string rentalitemid, bool autostageacc, string usersid, bool fromcheckin)
        {
            dynamic result;
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn, "dbo.instantiatecontainer", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.AddParameter("@containerid",  containerid);
            qry.AddParameter("@rentalitemid", rentalitemid);
            qry.AddParameter("@autostageacc", FwConvert.LogicalToCharacter(autostageacc));
            qry.AddParameter("@usersid",      usersid);
            qry.AddParameter("@fromcheckin",  FwConvert.LogicalToCharacter(fromcheckin));
            qry.AddParameter("@containeritemid", SqlDbType.VarChar, ParameterDirection.Output);
            qry.AddParameter("@contractid",      SqlDbType.VarChar, ParameterDirection.Output);
            await qry.ExecuteAsync();
            result = new ExpandoObject();
            result.containeritemid = qry.GetParameter("@containeritemid").ToString().TrimEnd();
            result.outcontractid   = qry.GetParameter("@contractid").ToString().TrimEnd();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> StageItemAsync(FwSqlConnection conn, string orderid, string masteritemid, string code, string vendorid, decimal qty, decimal meter, string location,
            string spaceid, bool additemtoorder, bool addcompletetoorder, bool addcontainertoorder, bool overridereservation, bool stageconsigned, bool transferrepair,
            bool removefromcontainer, bool releasefromrepair, bool autostageacc, string usersid, string contractid, bool ignoresuspendedin)
        {
            dynamic result;
            FwSqlCommand sp;

            sp = new FwSqlCommand(conn, "dbo.stageitem", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@orderid",              orderid);
            sp.AddParameter("@masteritemid",         SqlDbType.Char, ParameterDirection.InputOutput, masteritemid);
            sp.AddParameter("@code",                 code);
            sp.AddParameter("@vendorid",             vendorid);
            sp.AddParameter("@qty",                  qty);
            sp.AddParameter("@meter",                meter);
            sp.AddParameter("@location",             location);
            sp.AddParameter("@spaceid",              spaceid);
            sp.AddParameter("@additemtoorder",       FwConvert.LogicalToCharacter(additemtoorder));
            sp.AddParameter("@addcompletetoorder",   FwConvert.LogicalToCharacter(addcompletetoorder));
            sp.AddParameter("@addcontainertoorder",  FwConvert.LogicalToCharacter(addcontainertoorder));
            sp.AddParameter("@overridereservation",  FwConvert.LogicalToCharacter(overridereservation));
            sp.AddParameter("@stageconsigned",       FwConvert.LogicalToCharacter(stageconsigned));
            sp.AddParameter("@transferrepair",       FwConvert.LogicalToCharacter(transferrepair));
            sp.AddParameter("@removefromcontainer",  FwConvert.LogicalToCharacter(removefromcontainer));
            sp.AddParameter("@releasefromrepair",    FwConvert.LogicalToCharacter(releasefromrepair));  //jh 06/30/2015 CAS-15904-IVQS
            sp.AddParameter("@autostageacc",         FwConvert.LogicalToCharacter(autostageacc));
            sp.AddParameter("@usersid",              usersid);
            sp.AddParameter("@contractid",           contractid); // only supply a value if item should go out immediately
            sp.AddParameter("@ignoresuspendedin",    FwConvert.LogicalToCharacter(ignoresuspendedin));
            sp.AddParameter("@masterid",             SqlDbType.Char,    ParameterDirection.Output);
            sp.AddParameter("@rentalitemid",         SqlDbType.Char,    ParameterDirection.Output);
            sp.AddParameter("@consignorid",          SqlDbType.Char,    ParameterDirection.Output);
            sp.AddParameter("@consignoragreementid", SqlDbType.Char,    ParameterDirection.Output);
            sp.AddParameter("@qtystaged",            SqlDbType.Decimal, ParameterDirection.Output);
            sp.AddParameter("@exceptionbatchid",     SqlDbType.Char,    ParameterDirection.Output);
            sp.AddParameter("@status",               SqlDbType.Decimal, ParameterDirection.Output);
            sp.AddParameter("@msg",                  SqlDbType.VarChar, ParameterDirection.Output);
            await sp.ExecuteAsync();
            result = new ExpandoObject();
            result.masteritemid         = sp.GetParameter("@masteritemid").ToString().TrimEnd();
            result.masterid             = sp.GetParameter("@masterid").ToString().TrimEnd();
            result.rentalitemid         = sp.GetParameter("@rentalitemid").ToString().TrimEnd();
            result.consignorid          = sp.GetParameter("@consignorid").ToString().TrimEnd();
            result.consignoragreementid = sp.GetParameter("@consignoragreementid").ToString().TrimEnd();
            result.qtystaged            = sp.GetParameter("@qtystaged").ToDecimal();
            result.exceptionbatchid     = sp.GetParameter("@exceptionbatchid").ToString().TrimEnd();
            result.status               = sp.GetParameter("@status").ToDecimal();
            result.msg                  = sp.GetParameter("@msg").ToString().TrimEnd();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> StagingGetMasterInfoAsync(FwSqlConnection conn, string orderid, string masterid, string usersid)
        {
            dynamic result;
            FwSqlCommand sp;

            sp = new FwSqlCommand(conn, "dbo.staginggetmasterinfo", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@orderid",      orderid);
            sp.AddParameter("@masterid",     masterid);
            sp.AddParameter("@usersid",      usersid);
            sp.AddParameter("@masterno",     SqlDbType.Char,    ParameterDirection.Output);
            sp.AddParameter("@description",  SqlDbType.Char,    ParameterDirection.Output);
            sp.AddParameter("@qtyordered",   SqlDbType.Decimal, ParameterDirection.Output);
            sp.AddParameter("@qtysub",       SqlDbType.Decimal, ParameterDirection.Output);
            sp.AddParameter("@qtystaged",    SqlDbType.Decimal, ParameterDirection.Output);
            sp.AddParameter("@qtyout",       SqlDbType.Decimal, ParameterDirection.Output);
            sp.AddParameter("@qtyin",        SqlDbType.Decimal, ParameterDirection.Output);
            sp.AddParameter("@qtyremaining", SqlDbType.Decimal, ParameterDirection.Output);
            await sp.ExecuteAsync();
            result = new ExpandoObject();
            result.masterno     = sp.GetParameter("@masteritemid").ToString().TrimEnd();
            result.description  = sp.GetParameter("@masterid").ToString().TrimEnd();
            result.qtyordered   = sp.GetParameter("@rentalitemid").ToDecimal();
            result.qtysub       = sp.GetParameter("@consignorid").ToDecimal();
            result.qtystaged    = sp.GetParameter("@consignoragreementid").ToDecimal();
            result.qtyout       = sp.GetParameter("@qtystaged").ToDecimal();
            result.qtyin        = sp.GetParameter("@exceptionbatchid").ToDecimal();
            result.qtyremaining = sp.GetParameter("@status").ToDecimal();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> FillContainer_GetContainerPendingItemsFillContainerAsync(FwSqlConnection conn, string containeritemid)
        {
            dynamic result;
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.AddColumn("orderid", false);
            qry.AddColumn("masteritemid", false);
            qry.AddColumn("masterid", false);
            qry.AddColumn("description", false);
            qry.AddColumn("masterno", false);
            qry.AddColumn("itemclass", false);
            qry.AddColumn("trackedby", false);
            qry.AddColumn("requiredqty", false, FwDataTypes.Decimal);
            qry.AddColumn("incontainerqty", false, FwDataTypes.Decimal);
            qry.AddColumn("missingqty", false, FwDataTypes.Decimal);
            qry.AddColumn("ispackage", false, FwDataTypes.Boolean);
            qry.Add("select");
            qry.Add("  orderid        = os.orderid,");
            qry.Add("  masteritemid   = os.masteritemid,");
            qry.Add("  masterid       = os.masterid,");
            qry.Add("  description    = os.description,");
            qry.Add("  masterno       = os.masterno,");
            qry.Add("  itemclass      = os.itemclass,");
            qry.Add("  trackedby      = m.trackedby,");
            qry.Add("  requiredqty    = os.qtyordered,");
            qry.Add("  incontainerqty = os.outqty,");
            qry.Add("  missingqty     = os.qtyordered - os.outqty,");
            qry.Add("  ispackage      = dbo.ispackage(itemclass)");
            qry.Add("from funcorderstatus(@orderid) os join master m on (os.masterid = m.masterid)");
            qry.Add("  and ((os.qtyordered - os.outqty) > 0)");
            qry.Add("order by os.rectype, os.itemorder, os.masterno");
            qry.AddParameter("@orderid", containeritemid);
            result = await qry.QueryToFwJsonTableAsync();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> FillContainer_GetContainerPendingItemsCheckInAsync(FwSqlConnection conn, string contractid, string containeritemid, string dealid, 
            string departmentid, string orderid, string ordertype, string tab, string calculatecounted, string groupitems, string warehouseid)
        {
            dynamic result = null;
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.AddColumn("orderid", false);
            qry.AddColumn("masteritemid", false);
            qry.AddColumn("masterid", false);
            qry.AddColumn("description", false);
            qry.AddColumn("masterno", false);
            qry.AddColumn("trackedby", false);
            qry.AddColumn("requiredqty", false, FwDataTypes.Decimal);
            qry.AddColumn("incontainerqty", false, FwDataTypes.Decimal);
            qry.AddColumn("missingqty", false, FwDataTypes.Decimal);
            qry.AddColumn("itemclass", false);
            qry.AddColumn("ispackage", false, FwDataTypes.Boolean);
            qry.AddColumn("parentid", false);
            qry.Add("select");
            qry.Add("  orderid,");
            qry.Add("  masteritemid,");
            qry.Add("  masterid,");
            qry.Add("  description,");
            qry.Add("  masterno,");
            qry.Add("  trackedby,");
            qry.Add("  requiredqty    = qtyout,");
            qry.Add("  incontainerqty = qtyin,");
            qry.Add("  missingqty     = qtystillout,");
            qry.Add("  itemclass,");
            qry.Add("  itemorder,");
            qry.Add("  ispackage      = dbo.ispackage(itemclass),");
            qry.Add("  parentid");
            qry.Add("from funccheckinexception(@contractid, '', @containeritemid, 'T')");
            qry.Add("where exceptionflg = 'T'");
            qry.Add("  and ((qtystillout > 0) or");
            qry.Add("       (dbo.ispackage(itemclass) = 'T'))");
            qry.Add("order by itemorder");
            qry.AddParameter("@contractid", contractid);
            qry.AddParameter("@containeritemid", containeritemid);
            result = await qry.QueryToFwJsonTableAsync();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> FillContainer_GetContainerCheckInBCDataAsync(FwSqlConnection conn, string contractid, string containeritemid, string dealid, 
            string departmentid, string orderid, string ordertype, string tab, string calculatecounted, string groupitems, string warehouseid, string masteritemid)
        {
            dynamic result = null;
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.AddColumn("orderid", false);
            qry.AddColumn("masteritemid", false);
            qry.AddColumn("masterid", false);
            qry.AddColumn("description", false);
            qry.AddColumn("masterno", false);
            qry.AddColumn("trackedby", false);
            qry.AddColumn("requiredqty", false, FwDataTypes.Decimal);
            qry.AddColumn("incontainerqty", false, FwDataTypes.Decimal);
            qry.AddColumn("missingqty", false, FwDataTypes.Decimal);
            qry.AddColumn("itemclass", false);
            qry.Add("select fbc.orderid, fbc.masteritemid, fbc.masterid, fbc.description, fbc.masterno, trackedby = 'BARCODE', requiredqty = fos.qtyordered, incontainerqty = fos.outqty, missingqty = fos.qtyordered - fos.outqty, fbc.itemclass, orderby = '0'");
            qry.Add("from  dbo.funccheckinbcdata(@contractid) fbc join dbo.funcorderstatus(@containeritemid) fos on (fbc.masterid = fos.masterid)");
            qry.Add("where containeritemid = @containeritemid");
            qry.Add("  and fbc.masteritemid = @masteritemid");
            qry.AddParameter("@contractid", contractid);
            qry.AddParameter("@containeritemid", containeritemid);
            qry.AddParameter("@dealid", dealid);
            qry.AddParameter("@departmentid", departmentid);
            qry.AddParameter("@orderid", orderid);
            qry.AddParameter("@ordertype", ordertype);
            qry.AddParameter("@tab", tab);
            qry.AddParameter("@calculatecounted", calculatecounted);
            qry.AddParameter("@groupitems", groupitems);
            qry.AddParameter("@warehouseid", warehouseid);
            qry.AddParameter("@masteritemid", masteritemid);
            result = await qry.QueryToDynamicObject2Async();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> FillContainer_GetContainerCheckInQuantity2Async(FwSqlConnection conn, string contractid, string containeritemid, string dealid, 
            string departmentid, string orderid, string ordertype, string tab, string calculatecounted, string groupitems, string warehouseid, string masteritemid)
        {
            dynamic result = null;
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.AddColumn("orderid", false);
            qry.AddColumn("masteritemid", false);
            qry.AddColumn("masterid", false);
            qry.AddColumn("description", false);
            qry.AddColumn("masterno", false);
            qry.AddColumn("trackedby", false);
            qry.AddColumn("requiredqty", false, FwDataTypes.Decimal);
            qry.AddColumn("incontainerqty", false, FwDataTypes.Decimal);
            qry.AddColumn("missingqty", false, FwDataTypes.Decimal);
            qry.AddColumn("itemclass", false);
            qry.Add("select top 1");
            qry.Add("  fqty.orderid,");
            qry.Add("  fqty.masteritemid,");
            qry.Add("  fqty.masterid,");
            qry.Add("  fqty.description,");
            qry.Add("  fqty.masterno,");
            qry.Add("  trackedby = 'QUANTITY',");
            qry.Add("  requiredqty = fos.qtyordered,");
            qry.Add("  incontainerqty = outqty,");
            qry.Add("  missingqty = qtyordered - outqty,");
            qry.Add("  fqty.itemclass, fqty.orderby");
            qry.Add("from dbo.funccheckinquantity2(@contractid, @dealid, @departmentid, @orderid, @ordertype, @tab, @calculatecounted, @groupitems, @warehouseid, @containeritemid) fqty");
            qry.Add("  join dbo.funcorderstatus(@containeritemid) fos on (fqty.masterid = fos.masterid)");
            qry.Add("where fqty.masteritemid = @masteritemid");
            qry.AddParameter("@contractid", contractid);
            qry.AddParameter("@containeritemid", containeritemid);
            qry.AddParameter("@dealid", dealid);
            qry.AddParameter("@departmentid", departmentid);
            qry.AddParameter("@orderid", orderid);
            qry.AddParameter("@ordertype", ordertype);
            qry.AddParameter("@tab", tab);
            qry.AddParameter("@calculatecounted", calculatecounted);
            qry.AddParameter("@groupitems", groupitems);
            qry.AddParameter("@warehouseid", warehouseid);
            qry.AddParameter("@masteritemid", masteritemid);
            result = await qry.QueryToDynamicObject2Async();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<string> FillContainer_SetContainerNoAsync(FwSqlConnection conn, string rentalitemid, string containerno)
        {
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("update rentalitem");
            qry.Add("set containerno = @containerno");
            qry.Add("where rentalitemid = @rentalitemid");
            qry.AddParameter("@rentalitemid", rentalitemid);
            qry.AddParameter("@containerno",  containerno);
            await qry.ExecuteAsync();

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select containerno");
            qry.Add("from rentalitem");
            qry.Add("where rentalitemid = @rentalitemid");
            qry.AddParameter("@rentalitemid", rentalitemid);
            await qry.ExecuteAsync();
            containerno = qry.GetField("containerno").ToString().TrimEnd();

            return containerno;
        }

        //public static string CheckInGetItemInfo(FwSqlConnection conn, string barcode, string incontractid, string usersid, string bctype, string containeritemid, string orderid, 
        //    string dealid, string departmentid, string masterid, string warehouseid, string ordertranid, string internalchar, string masteritemid, string parentid, string rentalitemid)
        //{
        //    dynamic result;

        //    result = new ExpandoObject();
        //    using (FwSqlCommand qry = new FwSqlCommand(conn, "dbo.checkingetiteminfo"))
        //    {
        //        qry.AddParameter("@barcode",         barcode);
        //        qry.AddParameter("@incontractid",    incontractid);
        //        qry.AddParameter("@usersid",         usersid);
        //        qry.AddParameter("@bctype",          bctype); //"O"
        //        qry.AddParameter("@containeritemid", containeritemid);
        //        qry.AddParameter("@orderid",         orderid);
        //        qry.AddParameter("@dealid",          dealid);
        //        qry.AddParameter("@departmentid",    departmentid);
        //        qry.AddParameter("@masterid",        SqlDbType.Char, ParameterDirection.InputOutput);
        //        qry.AddParameter("@warehouseid",     SqlDbType.Char, ParameterDirection.InputOutput);
        //        qry.AddParameter("@ordertranid",     SqlDbType.Char, ParameterDirection.InputOutput);
        //        qry.AddParameter("@internalchar",    SqlDbType.Char, ParameterDirection.InputOutput);
        //        qry.AddParameter("@masteritemid",    SqlDbType.Char, ParameterDirection.InputOutput);
        //        qry.AddParameter("@parentid",        SqlDbType.Char, ParameterDirection.InputOutput);
        //        qry.AddParameter("@rentalitemid",    string.Empty);
        //        qry.AddParameter("@vendorid",        SqlDbType.Char, ParameterDirection.Output);
        //        qry.AddParameter("@masterno",        SqlDbType.Char, ParameterDirection.Output);
        //        qry.AddParameter("@description",     SqlDbType.Char, ParameterDirection.Output);
        //        qry.AddParameter("@vendor",          SqlDbType.Char, ParameterDirection.Output);
        //        qry.AddParameter("@orderno",         SqlDbType.Char, ParameterDirection.Output);
        //        qry.AddParameter("@orderdesc",       SqlDbType.Char, ParameterDirection.Output);
        //        qry.AddParameter("@msg",             SqlDbType.VarChar, ParameterDirection.Output);
        //        qry.AddParameter("@status",          SqlDbType.Decimal, ParameterDirection.Output);
        //        qry.Execute();
        //        result.masterid     = qry.GetParameter("@masterid").ToString().TrimEnd();
        //        result.warehouseid  = qry.GetParameter("@warehouseid").ToString().TrimEnd();
        //        result.ordertranid  = qry.GetParameter("@ordertranid").ToString().TrimEnd();
        //        result.internalchar = qry.GetParameter("@internalchar").ToString().TrimEnd();
        //        result.masteritemid = qry.GetParameter("@masteritemid").ToString().TrimEnd();
        //        result.parentid     = qry.GetParameter("@parentid").ToString().TrimEnd();
        //        result.vendorid     = qry.GetParameter("@vendorid").ToString().TrimEnd();
        //        result.masterno     = qry.GetParameter("@masterno").ToString().TrimEnd();
        //        result.description  = qry.GetParameter("@description").ToString().TrimEnd();
        //        result.vendor       = qry.GetParameter("@vendor").ToString().TrimEnd();
        //        result.orderno      = qry.GetParameter("@orderno").ToString().TrimEnd();
        //        result.orderdesc    = qry.GetParameter("@orderdesc").ToString().TrimEnd();
        //        result.msg          = qry.GetParameter("@msg").ToString().TrimEnd();
        //        result.status       = qry.GetParameter("@status").ToString().TrimEnd();
        //    }

        //    return result;
        // }
        //----------------------------------------------------------------------------------------------------
        public async Task<bool> FillContainer_IsScannableItemOfAContainerAsync(FwSqlConnection conn, string code)
        {
            bool result = false;

            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                qry.Add("select top 1 ri.rentalitemid");
                qry.Add("from rentalitem ri with (nolock) join container c with (nolock) on (ri.rentalitemid = c.rentalitemid)");
                qry.Add("where barcode = @code");
                qry.AddParameter("@code", code);
                await qry.ExecuteAsync();
                result = (qry.RowCount > 0);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<bool> FillContainer_IsScannableItemOfAContainerPendingOnOrderAsync(FwSqlConnection conn, string orderid, string code)
        {
            bool result = false;

            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                qry.Add("select top 1 1");
                qry.Add("from funcgetorderstatusdetail(@orderid, 'ORDER')");
                qry.Add("where rentalitemid = (select rentalitemid");
                qry.Add("                      from rentalitem ri with (nolock)");
                qry.Add("                      where ri.barcode = @code");
                qry.AddParameter("@code", code);
                await qry.ExecuteAsync();
                result = (qry.RowCount > 0);
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task CheckInItemUnassignAsync(FwSqlConnection conn, string contractid, string orderid, string masteritemid, string masterid, string description,
            string vendorid, string consignorid, string usersid, string aisle, string shelf, decimal qty)
        {
            using (FwSqlCommand qry = new FwSqlCommand(conn, "dbo.chkinitemunassign", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                qry.AddParameter("@contractid",   contractid);
                qry.AddParameter("@orderid",      orderid);
                qry.AddParameter("@masteritemid", masteritemid);
                qry.AddParameter("@masterid",     masterid);
                qry.AddParameter("@description",  description);
                qry.AddParameter("@vendorid",     vendorid);
                qry.AddParameter("@consignorid",  consignorid);
                qry.AddParameter("@usersid",      usersid);
                qry.AddParameter("@aisle",        aisle);
                qry.AddParameter("@shelf",        shelf);
                qry.AddParameter("@qty",          qty);
                await qry.ExecuteAsync();
            }
        }
        //----------------------------------------------------------------------------------------------------
        public async Task CheckInItemCancelAsync(FwSqlConnection conn, string contractid, string orderid, string masteritemid, string masterid, string vendorid, string consignorid,
            string usersid, string description, int ordertranid, string internalchar, decimal qty)
        {
            using (FwSqlCommand qry = new FwSqlCommand(conn, "dbo.chkinitemcancel", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                qry.AddParameter("@contractid",   contractid);
                qry.AddParameter("@orderid",      orderid);
                qry.AddParameter("@masteritemid", masteritemid);
                qry.AddParameter("@masterid",     masterid);
                qry.AddParameter("@vendorid",     vendorid);
                qry.AddParameter("@consignorid",  consignorid);
                qry.AddParameter("@usersid",      usersid);
                qry.AddParameter("@description",  description);
                qry.AddParameter("@ordertranid",  ordertranid);
                qry.AddParameter("@internalchar", internalchar);
                qry.AddParameter("@qty",          qty);
                await qry.ExecuteNonQueryAsync();
            }
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<string> CreateRepairAsync(FwSqlConnection conn, string contractid, string orderid, string masteritemid, string rentalitemid, decimal qty, string usersid)
        {
            string repairid = string.Empty;

            using (FwSqlCommand qry = new FwSqlCommand(conn, "dbo.createrepair", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                qry.AddParameter("@contractid",   contractid);
                qry.AddParameter("@orderid",      orderid);
                qry.AddParameter("@masteritemid", masteritemid);
                qry.AddParameter("@rentalitemid", rentalitemid);
                qry.AddParameter("@qty",          qty);
                qry.AddParameter("usersid",       usersid);
                qry.AddParameter("@repairid",     SqlDbType.Char, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                repairid = qry.GetParameter("@repairid").ToString(); 
            }

            return repairid;
        }
        //----------------------------------------------------------------------------------------------------
        // doesn't seem like this can be used until after a container has been filled and contract finalized
        public async Task<dynamic> FillContainer_RemoveItemFromContainerAsync(FwSqlConnection conn, string rentalitemid, string containeritemid, string masterid, decimal qty, string usersid,
            bool finalizecontract, string contractid) 
        {
            dynamic result = new ExpandoObject();

            using (FwSqlCommand qry = new FwSqlCommand(conn, "dbo.removeitemfromcontainer", this.ApplicationConfig.DatabaseSettings.QueryTimeout)) {
                qry.AddParameter("@rentalitemid", rentalitemid);
                qry.AddParameter("@containeritemid", containeritemid);
                qry.AddParameter("@masterid", masterid);
                qry.AddParameter("@qty", qty);
                qry.AddParameter("@usersid", usersid);
                qry.AddParameter("@finalizecontract", FwConvert.LogicalToCharacter(finalizecontract));
                qry.AddParameter("@contractid", "");
                qry.AddParameter("@status", SqlDbType.Decimal, ParameterDirection.Output);
                qry.AddParameter("@msg", SqlDbType.VarChar, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                result.status = qry.GetParameter("@status").ToDecimal();
                result.msg = qry.GetParameter("@msg").ToString().TrimEnd();
                if (result.status != 0) {
                    throw new Exception(result.msg);
                }
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task AssignContractAsync(FwSqlConnection conn, string contractid) 
        {
            using (FwSqlCommand qry = new FwSqlCommand(conn, "dbo.assigncontract", this.ApplicationConfig.DatabaseSettings.QueryTimeout)) 
            {
                qry.AddParameter("@contractid", contractid);
                await qry.ExecuteAsync();
            }
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<bool> ContractIsEmptyAsync(FwSqlConnection conn, string contractid) 
        {
            bool result;

            using (FwSqlCommand qry = new FwSqlCommand(conn, "dbo.contractisempty", this.ApplicationConfig.DatabaseSettings.QueryTimeout)) 
            {
                qry.AddParameter("@contractid", contractid);
                qry.AddParameter("@isempty", SqlDbType.Char, ParameterDirection.Output);
                await qry.ExecuteAsync();
                result = qry.GetParameter("@isempty").ToBoolean();
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> GetRetiredReasonAsync(FwSqlConnection conn)
        {
            FwSqlCommand qry;
            dynamic results;

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select retiredreason, reasontype, retiredreasonid, inactive");
            qry.Add("from retiredreason with (nolock)");
            qry.Add("where inactive <> 'T'");
            qry.Add("order by retiredreason");
            results = await qry.QueryToDynamicList2Async();

            return results;
        }
        //----------------------------------------------------------------------------------------------------
        /// <summary>
        /// Proposal 64733 for Marvel Studios.  Ability to retire an item (create an L&amp;D order) from QuikScan.
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="isicode">Is the item a qty item rather than a 'rentalitem'.</param>
        /// <param name="barcode">If the items is a rentalitem, need to pass the barcode field.</param>
        /// <param name="orderid">Pass if doing isretireonorder, otherwise blank.</param>
        /// <param name="masterno">Pass if retiring a qty item.</param>
        /// <param name="qty">Pass 1 for rentalitems or the qty for qty items.</param>
        /// <param name="retiredreasonid">The retirereason.</param>
        /// <param name="lossamount">The loss amount.</param>
        /// <param name="notes">Notes about retiring the item.</param>
        /// <param name="usersid"></param>
        /// <param name="warehouseid">The user's warehouse id.</param>
        public async Task<dynamic> AssetDispositionAsync(FwSqlConnection conn, bool isicode, string barcode, string orderid, string masterno, decimal qty, string retiredreasonid, 
                                                                decimal lossamount, string notes, string usersid, string warehouseid)
        {
            FwSqlCommand sp;
            dynamic result;

            sp = new FwSqlCommand(conn, "dbo.assetdisposition", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@isicode",             FwConvert.LogicalToCharacter(isicode));
            sp.AddParameter("@barcode",             barcode);
            sp.AddParameter("@orderid",             orderid);
            sp.AddParameter("@masterno",            masterno);
            sp.AddParameter("@qty",                 qty);
            sp.AddParameter("@retiredreasonid",     retiredreasonid);
            sp.AddParameter("@lossamount",          lossamount);
            sp.Parameters["@lossamount"].Precision = 12;
            sp.Parameters["@lossamount"].Scale = 3;
            sp.AddParameter("@notes",               notes);
            sp.AddParameter("@usersid",             usersid);
            sp.AddParameter("@warehouseid",         warehouseid);
            sp.AddParameter("@errno",  SqlDbType.Int, ParameterDirection.Output);
            sp.AddParameter("@errmsg", SqlDbType.VarChar, ParameterDirection.Output);
            await sp.ExecuteAsync();
            result = new ExpandoObject();
            result.errno  = sp.GetParameter("@errno").ToInt32();
            result.errmsg = sp.GetParameter("@errmsg").ToString().TrimEnd();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> TimeLogSearchOrdersAsync(FwSqlConnection conn, string webusersid, string dealid, string orderno)
        {
            FwSqlCommand qry;
            dynamic result;

            result = null;
            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select *");
            qry.Add("from  dbo.funccrewdealorder(@webusersid, @dealid)");
            if (!string.IsNullOrEmpty(orderno))
            {
                qry.Add("where orderno = @orderno");
                qry.AddParameter("@orderno", orderno);
            }
            qry.Add("order by orderdate desc, orderno desc");
            qry.AddParameter("@webusersid", webusersid);
            qry.AddParameter("@dealid", dealid);
            result = await qry.QueryToDynamicList2Async();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> TimeLogSearchDealsAsync(FwSqlConnection conn, string webusersid, string dealno)
        {
            FwSqlCommand qry;
            dynamic result;

            result = null;
            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select *");
            qry.Add("from  dbo.funccrewdeal(@webusersid)");
            if (!string.IsNullOrEmpty(dealno))
            {
                qry.Add("where dealno = @dealno");
                qry.AddParameter("@dealno", dealno);
            }
            qry.Add("order by deal asc");
            qry.AddParameter("@webusersid", webusersid);
            result = await qry.QueryToDynamicList2Async();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> TimeLogSearchEventsAsync(FwSqlConnection conn, string webusersid, string eventno)
        {
            FwSqlCommand qry;
            dynamic result;

            result = null;
            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select top 1 *");
            qry.Add("from  dbo.funccrewevent(@webusersid)");
            if (!string.IsNullOrEmpty(eventno))
            {
                qry.Add("where eventno = @eventno");
                qry.AddParameter("@eventno", eventno);
            }
            qry.Add("order by eventno desc");
            qry.AddParameter("@webusersid", webusersid);
            result = await qry.QueryToDynamicList2Async();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> TimeLogSearchProjectsAsync(FwSqlConnection conn, string orderno)
        {
            FwSqlCommand qry;
            dynamic result;

            result = null;
            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select orderid, ordertypeid, orderno, orderdesc, dealid, dealno, deal, status, warehouseid, locationid, orderdate, estrentfrom, estrentto");
            qry.Add("from  orderview");
            qry.Add("where status in ('ACTIVE', 'COMPLETE')");
            qry.Add("  and ordertype = 'O'");
            if (!string.IsNullOrEmpty(orderno))
            {
                qry.Add("and orderno = @orderno");
                qry.AddParameter("@orderno", orderno);
            }
            qry.Add("order by orderdate");
            result = await qry.QueryToDynamicList2Async();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> GetCrewTimeEntriesAsync(FwSqlConnection conn, string webusersid, string orderid, string dealid, string eventid)
        {
            FwSqlCommand qry;
            dynamic result;

            result = null;
            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select *");
            qry.Add("from  dbo.funcmasteritemcrewtime2(@orderid, @dealid, @eventid, @webusersid)");
            qry.Add("order by thedate, rowtypeorderby");
            qry.AddParameter("@webusersid", webusersid);
            qry.AddParameter("@orderid",    orderid);
            qry.AddParameter("@dealid",     dealid);
            qry.AddParameter("@eventid",    eventid);
            result = await qry.QueryToDynamicList2Async();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> GetUserTimeEntriesAsync(FwSqlConnection conn, string usersid, string orderid)
        {
            FwSqlCommand qry;
            dynamic result;

            result = null;
            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select *");
            qry.Add("from  usertimeview");
            qry.Add("where usersid = @usersid");
            qry.Add("  and orderid = @orderid");
            qry.Add("order by usertimeid");
            qry.AddParameter("@usersid", usersid);
            qry.AddParameter("@orderid", orderid);
            result = await qry.QueryToDynamicList2Async();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> InsertUserTimeAsync(FwSqlConnection conn, string usersid, FwDateTime workdate, string orderid, string description, string workhours)
        {
            FwSqlCommand sp;
            dynamic result;

            sp = new FwSqlCommand(conn, "dbo.insertusertime", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@usersid",     usersid);
            sp.AddParameter("@workdate",    workdate.GetSqlValue());
            sp.AddParameter("@orderid",     orderid);
            sp.AddParameter("@description", description);
            sp.AddParameter("@workhours",   workhours);
            sp.AddParameter("@status",      SqlDbType.Int,     ParameterDirection.Output);
            sp.AddParameter("@msg",         SqlDbType.VarChar, ParameterDirection.Output);
            await sp.ExecuteNonQueryAsync();
            result        = new ExpandoObject();
            result.status = sp.GetParameter("@status").ToInt32();
            result.msg    = sp.GetParameter("@msg").ToString().TrimEnd();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> UpdateUserTimeAsync(FwSqlConnection conn, int usertimeid, string internalchar, string usersid, FwDateTime workdate, string orderid, string description, string workhours)
        {
            FwSqlCommand sp;
            dynamic result;

            sp = new FwSqlCommand(conn, "dbo.updateusertime", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@usertimeid",   usertimeid.ToString());
            sp.AddParameter("@internalchar", internalchar);
            sp.AddParameter("@usersid",      usersid);
            sp.AddParameter("@workdate",     workdate.GetSqlValue());
            sp.AddParameter("@orderid",      orderid);
            sp.AddParameter("@description",  description);
            sp.AddParameter("@workhours",    workhours);
            sp.AddParameter("@status",       SqlDbType.Int,     ParameterDirection.Output);
            sp.AddParameter("@msg",          SqlDbType.VarChar, ParameterDirection.Output);
            await sp.ExecuteNonQueryAsync();
            result        = new ExpandoObject();
            result.status = sp.GetParameter("@status").ToInt32();
            result.msg    = sp.GetParameter("@msg").ToString().TrimEnd();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> TimeLogSubmitAsync(FwSqlConnection conn, string webusersid, FwDateTime thedate, string orderid, string masteritemid, string starttime, string stoptime, string break1starttime, string break1stoptime, string break2starttime, string break2stoptime, string break3starttime, string break3stoptime, string notes, string inputbywebusersid)
        {
            FwSqlCommand sp;
            dynamic result;

            sp = new FwSqlCommand(conn, "dbo.applymasteritemcrewtime", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@webusersid",          webusersid);
            sp.AddParameter("@thedate",             thedate.GetSqlValue());
            sp.AddParameter("@orderid",             orderid);
            sp.AddParameter("@masteritemid",        masteritemid);
            sp.AddParameter("@starttime",           starttime);
            sp.AddParameter("@stoptime",            stoptime);
            sp.AddParameter("@break1starttime",     break1starttime);
            sp.AddParameter("@break1stoptime",      break1stoptime);
            sp.AddParameter("@break2starttime",     break2starttime);
            sp.AddParameter("@break2stoptime",      break2stoptime);
            sp.AddParameter("@break3starttime",     break3starttime);
            sp.AddParameter("@break3stoptime",      break3stoptime);
            sp.AddParameter("@notes",               notes);
            sp.AddParameter("@inputbywebusersid",   inputbywebusersid);
            sp.AddParameter("@status",              SqlDbType.Int,     ParameterDirection.Output);
            sp.AddParameter("@msg",                 SqlDbType.VarChar, ParameterDirection.Output);
            await sp.ExecuteAsync();
            result = new ExpandoObject();
            result.status = sp.GetParameter("@status").ToInt32();
            result.msg    = sp.GetParameter("@msg").ToString().TrimEnd();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task FillContainer_CreateContainerAsync(FwSqlConnection conn, string containeritemid, string loggedin_usersid)
        {
            FwSqlSelect select;
            FwJsonDataTable dt;

            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                select = new FwSqlSelect();
                select.Add("select contractid, status, usersid");
                select.Add("from  suspendview");
                select.Add("where contractid in (select oc.contractid");
                select.Add("                     from  ordercontract oc with (nolock)");
                select.Add("                     where oc.orderid = @containeritemid)");
                select.AddParameter("@containeritemid", containeritemid);
                dt = await qry.QueryToFwJsonTableAsync(select, true);
            }
            for (int rowno = 0; rowno < dt.Rows.Count; rowno++)
            {
                string contractid = dt.GetValue(rowno, "contractid").ToString().TrimEnd();
                string status     = dt.GetValue(rowno, "status").ToString().TrimEnd();
                string usersid    = dt.GetValue(rowno, "usersid").ToString().TrimEnd();
                switch(status)
                {
                    case "NEW":
                        await CancelContractAsync(conn, contractid, usersid, true);
                        break;
                    case "ACTIVE":
                        await AssignContractAsync(conn, contractid);
                        break;
                }
            }
            using (FwSqlCommand qry = new FwSqlCommand(conn, "dbo.updatecontainerstatus", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                qry.AddParameter("@containerid", containeritemid);
                qry.AddParameter("@usersid", loggedin_usersid);
                await qry.ExecuteNonQueryAsync();
            }
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> FillContainer_FuncGetContainerAsync(FwSqlConnection conn, string barcode, string warehouseid)
        {
            dynamic result;

            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                qry.Add("select top 1 *");
                qry.Add("from dbo.funcgetcontainer(@barcode, @warehouseid)");
                qry.AddParameter("@barcode", barcode);
                qry.AddParameter("@warehouseid", warehouseid);
                result = await qry.QueryToDynamicObject2Async();
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> CheckInGetItemInfoAsync(FwSqlConnection conn, string barcode, string incontractid, string usersid, string bctype, string containeritemid, string orderid, string dealid,
            string departmentid, string masteritemid, string rentalitemid)
        {
            dynamic result = new ExpandoObject();

            using (FwSqlCommand qry = new FwSqlCommand(conn, "dbo.checkingetiteminfo", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                qry.AddParameter("@barcode",            barcode);       // can be barcode, rfid, serial number, or rentalitemid
                qry.AddParameter("@incontractid",       incontractid);
                qry.AddParameter("@usersid",            usersid);
                qry.AddParameter("@bctype",             bctype);        //O=Order, T=Transfer, P=Package Truck
                qry.AddParameter("@containeritemid",    containeritemid);
                qry.AddParameter("@orderid",            orderid);
                qry.AddParameter("@dealid",             dealid);
                qry.AddParameter("@departmentid",       departmentid);
                qry.AddParameter("@masterid",           SqlDbType.Char,    ParameterDirection.Output);
                qry.AddParameter("@warehouseid",        SqlDbType.Char,    ParameterDirection.Output);
                qry.AddParameter("@ordertranid",        SqlDbType.Int,     ParameterDirection.Output);
                qry.AddParameter("@internalchar",       SqlDbType.Char,    ParameterDirection.Output);
                qry.AddParameter("@masteritemid",       SqlDbType.Char,    ParameterDirection.InputOutput, masteritemid);  //input/output
                qry.AddParameter("@parentid",           SqlDbType.Char,    ParameterDirection.Output);
                qry.AddParameter("@rentalitemid",       SqlDbType.Char,    ParameterDirection.InputOutput, rentalitemid);  //input/output
                qry.AddParameter("@vendorid",           SqlDbType.Char,    ParameterDirection.Output);
                qry.AddParameter("@masterno",           SqlDbType.Char,    ParameterDirection.Output);
                qry.AddParameter("@description",        SqlDbType.VarChar, ParameterDirection.Output);
                qry.AddParameter("@vendor",             SqlDbType.Char,    ParameterDirection.Output);
                qry.AddParameter("@orderno",            SqlDbType.VarChar, ParameterDirection.Output);
                qry.AddParameter("@orderdesc",          SqlDbType.VarChar, ParameterDirection.Output);
                qry.AddParameter("@packageid",          SqlDbType.Char,    ParameterDirection.Output);
                qry.AddParameter("@packageitemid",      SqlDbType.Char,    ParameterDirection.Output);
                qry.AddParameter("@itemclass",          SqlDbType.VarChar, ParameterDirection.Output);
                qry.AddParameter("@nestedmasteritemid", SqlDbType.Char,    ParameterDirection.Output);
                qry.AddParameter("@orderby",            SqlDbType.VarChar, ParameterDirection.Output);
                qry.AddParameter("@pendingrepairid",    SqlDbType.Char,    ParameterDirection.Output);
                qry.AddParameter("@outputorderid",      SqlDbType.Char,    ParameterDirection.Output);
                qry.AddParameter("@outputmasteritemid", SqlDbType.Char,    ParameterDirection.Output);
                qry.AddParameter("@outputdealid",       SqlDbType.Char,    ParameterDirection.Output);
                qry.AddParameter("@outputdepartmentid", SqlDbType.Char,    ParameterDirection.Output);
                qry.AddParameter("@outputrentalitemid", SqlDbType.Char,    ParameterDirection.Output);
                qry.AddParameter("@canswap",            SqlDbType.Char,    ParameterDirection.Output);
                qry.AddParameter("@exchangecontractid", SqlDbType.Char,    ParameterDirection.Output);
                qry.AddParameter("@retiredid",          SqlDbType.Char,    ParameterDirection.Output);
                qry.AddParameter("@msg",                SqlDbType.VarChar, ParameterDirection.Output);
                qry.AddParameter("@status",             SqlDbType.Decimal, ParameterDirection.Output);
                await qry.ExecuteNonQueryAsync();
                result.orderid            = qry.GetParameter("@outputorderid").ToString().TrimEnd();
                result.dealid             = qry.GetParameter("@outputdealid").ToString().TrimEnd();
                result.departmentid       = qry.GetParameter("@outputdepartmentid").ToString().TrimEnd();
                result.masterid           = qry.GetParameter("@masterid").ToString().TrimEnd();
                result.warehouseid        = qry.GetParameter("@warehouseid").ToString().TrimEnd();
                result.ordertranid        = qry.GetParameter("@ordertranid").ToInt32();
                result.internalchar       = qry.GetParameter("@internalchar").ToString().TrimEnd();
                result.masteritemid       = qry.GetParameter("@outputmasteritemid").ToString().TrimEnd();
                result.parentid           = qry.GetParameter("@parentid").ToString().TrimEnd();
                result.rentalitemid       = qry.GetParameter("@outputrentalitemid").ToString().TrimEnd();
                result.vendorid           = qry.GetParameter("@vendorid").ToString().TrimEnd();
                result.masterno           = qry.GetParameter("@masterno").ToString().TrimEnd();
                result.description        = qry.GetParameter("@description").ToString().TrimEnd();
                result.vendor             = qry.GetParameter("@vendor").ToString().TrimEnd();
                result.orderno            = qry.GetParameter("@orderno").ToString().TrimEnd();
                result.orderdesc          = qry.GetParameter("@orderdesc").ToString().TrimEnd();
                result.packageitemid      = qry.GetParameter("@packageitemid").ToString().TrimEnd();
                result.itemclass          = qry.GetParameter("@itemclass").ToString().TrimEnd();
                result.nestedmasteritemid = qry.GetParameter("@nestedmasteritemid").ToString().TrimEnd();
                result.orderby            = qry.GetParameter("@orderby").ToString().TrimEnd();
                result.msg                = qry.GetParameter("@msg").ToString().TrimEnd();
                result.status             = qry.GetParameter("@status").ToInt32();
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> FillContainer_EmptyContainerAsync(FwSqlConnection conn, String containerrentalitemid, String deleteall, String usersid)
        {
            FwSqlCommand sp = new FwSqlCommand(conn, "emptycontainer", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@containerrentalitemid", containerrentalitemid);
            sp.AddParameter("@deleteall", deleteall);
            sp.AddParameter("@usersid", usersid);
            sp.AddParameter("@incontractid", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
            sp.AddParameter("@msg", SqlDbType.VarChar, ParameterDirection.Output);
            await sp.ExecuteAsync();
            dynamic result = new ExpandoObject();
            result.incontractid = sp.GetParameter("@incontractid").ToString();
            result.status       = sp.GetParameter("@status").ToInt32();
            result.msg          = sp.GetParameter("@msg").ToString();
            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> GetOrderItemsToSubAsync(FwSqlConnection conn, string usersid)
        {
            dynamic result = new ExpandoObject();
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select * from dbo.funcstagingsubstituteitem(@usersid)");
            qry.AddParameter("@usersid", usersid);
            result = await qry.QueryToDynamicList2Async();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> GetOrderCompletesToSubAsync(FwSqlConnection conn, string usersid)
        {
            dynamic result = new ExpandoObject();
            FwSqlCommand qry;

            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select * from dbo.funcstagingsubstitutecomplete(@usersid)");
            qry.AddParameter("@usersid", usersid);
            result = await qry.QueryToDynamicList2Async();

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> SubstituteAtStagingAsync(FwSqlConnection conn, string orderid, string reducemasteritemid, string replacewithmasterid, string usersid, string qtytosubstitute, string substitutecomplete)
        {
            dynamic result = new ExpandoObject();

            using (FwSqlCommand qry = new FwSqlCommand(conn, "dbo.substituteatstaging", this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                qry.AddParameter("@orderid",                orderid);
                qry.AddParameter("@reducemasteritemid",     reducemasteritemid);
                qry.AddParameter("@replacewithmasterid",    replacewithmasterid);
                qry.AddParameter("@usersid",                usersid);
                qry.AddParameter("@qtytosubstitute",        qtytosubstitute);
                qry.AddParameter("@substitutecomplete",     substitutecomplete);
                qry.AddParameter("@substitutemasteritemid", SqlDbType.Char,    ParameterDirection.Output);
                qry.AddParameter("@status",                 SqlDbType.Int,     ParameterDirection.Output);
                qry.AddParameter("@msg",                    SqlDbType.VarChar, ParameterDirection.Output);
                await qry.ExecuteAsync();
                result.substitutemasteritemid = qry.GetParameter("@substitutemasteritemid").ToString();
                result.status                 = qry.GetParameter("@status").ToString();
                result.msg                    = qry.GetParameter("@msg").ToString();
            }

            return result;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<string> GetDepartmentFilterWhereClauseAsync(FwSqlConnection conn, string usersid)
        {
            StringBuilder deptfilter = new StringBuilder();
            dynamic applicationOptions = await FwSqlData.GetApplicationOptionsAsync(this.ApplicationConfig.DatabaseSettings);
            if (applicationOptions.departmentfilter.enabled)
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.Add("select d.departmentid");
                    qry.Add("from  department d join departmentaccess da on (d.departmentid = da.departmentaccessid)");
                    qry.Add("where da.departmentid = dbo.getusersprimarydepartmentid(@usersid)");
                    qry.Add("  and d.inactive <> 'T'");
                    qry.Add("  and orderaccess = 'T'");
                    qry.AddParameter("@usersid", usersid);
                    FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();
                    deptfilter.Append("departmentid in (''");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        deptfilter.Append(",'");
                        deptfilter.Append(dt.GetValue(i, "departmentid").ToString());
                        deptfilter.Append("'");
                    }
                    deptfilter.Append(")");
                }
            }
            return deptfilter.ToString();
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> GetLastSetImageIdAsync(FwSqlConnection conn, string masterid)
        {
            string appdocumentid;
            FwSqlCommand qry;
            qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select dbo.getlastsetimageid(@masterid) as appdocumentid");
            qry.AddParameter("@masterid", masterid);
            await qry.ExecuteAsync();
            appdocumentid = qry.GetField("appdocumentid").ToString().TrimEnd();
            return appdocumentid;
        }
        //----------------------------------------------------------------------------------------------------
        public async Task<dynamic> GetBarcodeFromRfidAsync(FwSqlConnection conn, string rfid)
        {
            FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select top 1 barcode");
            qry.Add("from rentalitem with (nolock)");
            qry.Add("where rfid = @rfid");
            qry.AddParameter("@rfid", rfid);
            await qry.ExecuteAsync();
            string barcode = qry.GetField("barcode").ToString().TrimEnd();
            if (string.IsNullOrEmpty(barcode))
            {
                barcode = rfid;
            }
            return barcode;
        }
        //----------------------------------------------------------------------------------------------------
    }
}