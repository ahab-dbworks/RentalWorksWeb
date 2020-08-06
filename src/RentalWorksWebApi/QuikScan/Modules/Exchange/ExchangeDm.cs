using FwStandard.Models;
using FwStandard.SqlServer;
using RentalWorksQuikScan.Modules.ExchangeModels;
using RentalWorksQuikScan.Source;
using System;
using System.Data;
using System.Dynamic;
using System.Threading.Tasks;

namespace RentalWorksQuikScan.Modules
{
    class ExchangeDm
    {
        FwApplicationConfig ApplicationConfig;
        //---------------------------------------------------------------------------------------------
        public ExchangeDm(FwApplicationConfig applicationConfig)
        {
            this.ApplicationConfig = applicationConfig;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> OrderSearchAsync(FwSqlConnection conn, int pageno, int pagesize, string searchmode, string searchvalue, string usersid, string locationid)
        {
            string orderid = string.Empty;
            FwJsonDataTable searchresults = null;

            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                qry.AddColumn("orderdate", false, FwDataTypes.Date);
                qry.AddColumn("statusdate", false, FwDataTypes.Date);
                if (searchmode == "barcode")
                {
                    RwAppData appData = new RwAppData(this.ApplicationConfig);
                    dynamic itemstatus = await appData.WebGetItemStatusAsync(conn, usersid, searchvalue);
                    orderid = itemstatus.orderid;
                }

                FwSqlSelect select = new FwSqlSelect();
                select.PageNo   = pageno;
                select.PageSize = pagesize;
                select.Add("select *");
                select.Add("from dbo.funcorder('O','F') o");
                select.Add("where o.locationid = @locationid");
                select.Add("  and o.status not in ('CANCELLED','CLOSED','NEW','CANCELLED','CLOSED','ORDERED')");
                select.Add("  and o.status in ('ACTIVE')");
                await DepartmentFilter.SetDepartmentFilterAsync(this.ApplicationConfig, usersid, select);
                select.Parse();
                switch (searchmode)
                {
                    case "orderdesc":
                        select.AddWhere("orderdesc like @searchvalue");
                        select.AddOrderBy("orderdate desc");
                        select.AddOrderBy("orderno desc");
                        select.AddParameter("@searchvalue", "%" + searchvalue + "%");
                        break;
                    case "orderno":
                        select.AddWhere("orderno = @searchvalue");
                        select.AddOrderBy("orderno");
                        select.AddParameter("@searchvalue", searchvalue);
                        break;
                    case "deal":
                        select.AddWhere("deal like @searchvalue");
                        select.AddOrderBy("deal");
                        select.AddOrderBy("orderdate desc");
                        select.AddOrderBy("orderno desc");
                        select.AddParameter("@searchvalue", searchvalue + "%");
                        break;
                    case "barcode":
                        select.AddWhere("orderid = @orderid");
                        select.AddOrderBy("orderdate desc");
                        select.AddOrderBy("orderno desc");
                        select.AddParameter("@orderid", orderid);
                        break;
                }
                select.AddParameter("@locationid", locationid);
                searchresults = await qry.QueryToFwJsonTableAsync(select, true);
            }

            return searchresults;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> DealSearchAsync(FwSqlConnection conn, int pageno, int pagesize, string searchmode, string searchvalue, string locationid)
        {
            FwJsonDataTable searchresults = null;

            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.PageNo   = pageno;
                select.PageSize = pagesize;
                select.Add("select *");
                select.Add("from  dbo.funcdeal(@locationid) d");
                select.Add("where dealinactive <> 'T'");
                select.Parse();
                switch (searchmode)
                {
                    case "deal":
                        select.AddWhere("deal like @searchvalue");
                        select.AddOrderBy("deal");
                        select.AddParameter("@searchvalue", "%" + searchvalue + "%");
                        break;
                    case "customer":
                        select.AddWhere("customer like @searchvalue");
                        select.AddOrderBy("customer");
                        select.AddParameter("@searchvalue", "%" + searchvalue + "%");
                        break;
                    case "dealno":
                        select.AddWhere("dealno = @searchvalue");
                        select.AddOrderBy("dealno");
                        select.AddParameter("@searchvalue", searchvalue);
                        break;
                }
                select.AddParameter("@locationid", locationid);
                searchresults = await qry.QueryToFwJsonTableAsync(select, true);
            }

            return searchresults;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> CompanyDepartmentSearchAsync(FwSqlConnection conn, int pageno, int pagesize, string searchmode, string searchvalue)
        {
            FwJsonDataTable searchresults = null;

            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.PageNo   = pageno;
                select.PageSize = pagesize;
                select.Add("select *");
                select.Add("from department d with (nolock)");
                select.Add("where d.inactive <> 'T'");
                select.Parse();
                switch (searchmode)
                {
                    case "department":
                        select.AddWhere("department like @searchvalue");
                        select.AddOrderBy("department");
                        select.AddParameter("@searchvalue", "%" + searchvalue + "%");
                        break;
                    case "deptcode":
                        select.AddWhere("deptcode = @searchvalue");
                        select.AddOrderBy("deptcode");
                        select.AddParameter("@searchvalue", searchvalue);
                        break;
                }
                searchresults = await qry.QueryToFwJsonTableAsync(select, true);
            }

            return searchresults;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> PendingExchangeSearchAsync(FwSqlConnection conn, int pageno, int pagesize, string searchmode, string searchvalue, string locationid)
        {
            FwJsonDataTable searchresults = null;

            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                qry.AddColumn("contractdate", false, FwDataTypes.Date);
                FwSqlSelect select = new FwSqlSelect();
                select.PageNo   = pageno;
                select.PageSize = pagesize;
                select.Add("select c.contractid, c.sessionno, o.orderid, o.orderno, o.orderdesc, d.dealid, d.dealno, d.deal, c.departmentid, cd.department,");
                select.Add("       c.warehouseid, w.warehouse, c.contractdate, c.contracttime, c.locationid");
                select.Add("from contract c with (nolock) left outer join ordercontract oc with (nolock) on (c.contractid = oc.contractid)");
                select.Add("                              left outer join dealorder o with (nolock) on (oc.orderid = o.orderid)");
                select.Add("                              left outer join deal d with (nolock) on (c.dealid = d.dealid)");
                select.Add("                              left outer join department cd with (nolock) on (c.departmentid = cd.departmentid)");
                select.Add("                              left outer join warehouse w with (nolock) on (c.warehouseid = w.warehouseid)");
                select.Add("where c.contracttype = 'EXCHANGE'");
                select.Add("  and c.pendingexchange = 'T'");
                select.Add("  and c.locationid = @locationid");
                select.Parse();
                switch (searchmode)
                {
                    case "orderdesc":
                        select.AddWhere("orderdesc like @searchvalue");
                        select.AddOrderBy("orderdesc");
                        select.AddOrderBy("contractdate desc");
                        select.AddOrderBy("contracttime desc");
                        select.AddOrderBy("orderno desc");
                        select.AddParameter("@searchvalue", "%" + searchvalue + "%");
                        break;
                    case "orderno":
                        select.AddWhere("orderno = @searchvalue");
                        select.AddOrderBy("orderno");
                        select.AddParameter("@searchvalue", searchvalue);
                        break;
                    case "deal":
                        select.AddWhere("deal like @searchvalue");
                        select.AddOrderBy("deal");
                        select.AddOrderBy("contractdate desc");
                        select.AddOrderBy("contracttime desc");
                        select.AddOrderBy("orderno desc");
                        select.AddParameter("@searchvalue", searchvalue + "%");
                        break;
                }
                select.AddParameter("@locationid", locationid);
                searchresults = await qry.QueryToFwJsonTableAsync(select, true);
            }

            return searchresults;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> SuspendedSessionSearchAsync(FwSqlConnection conn, int pageno, int pagesize, string orderid, string dealid, string departmentid, string locationid)
        {
            FwJsonDataTable searchresults = null;

            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.PageNo   = pageno;
                select.PageSize = pagesize;
                select.Add("select sv.*,");
                select.Add("       orderid    = (select top 1 oc.orderid");
                select.Add("                     from ordercontract oc with (nolock)");
                select.Add("                     where oc.contractid = sv.contractid),");
                select.Add("       department = (select top 1 d.department");
                select.Add("                     from department d with (nolock)");
                select.Add("                     where sv.departmentid = d.departmentid)");
                select.Add("from suspendview sv with (nolock)");
                select.Add("where sv.contracttype = 'EXCHANGE'");
                select.Add("  and sv.locationid   = @locationid");
                select.Add("  and sv.ordertype    = 'O'");
                if (!string.IsNullOrEmpty(orderid))
                {
                    string orderno = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "dealorder", "orderid", orderid, "orderno");
                    select.Add("  and sv.orderno = @orderno");
                    select.AddParameter("@orderno", orderno);
                }
                else if (!string.IsNullOrEmpty(dealid))
                {
                    select.Add("  and sv.dealid = @dealid");
                    select.AddParameter("@dealid", dealid);
                }
                else if (!string.IsNullOrEmpty(departmentid))
                {
                    select.Add("  and sv.departmentid = @departmentid");
                    select.AddParameter("@departmentid", departmentid);
                }
                select.Add("  and sv.contractid not in (select c.exchangecontractid");
                select.Add("                            from  contract c with (nolock))");
                select.Parse();
                select.AddOrderBy("statusdate desc");
                select.AddParameter("@locationid", locationid);
                searchresults = await qry.QueryToFwJsonTableAsync(select, true);
            }

            return searchresults;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> ExchangeSessionSearchAsync(FwSqlConnection conn, int pageno, int pagesize, string exchangecontractid, bool pendingonly)
        {
            FwJsonDataTable searchresults = null;

            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.PageNo   = pageno;
                select.PageSize = pagesize;
                select.Add("select *");
                select.Add("from  dbo.funcgetexchangedata2line(@exchangecontractid, @pendingonly)");
                select.Parse();
                select.AddOrderBy("orderby");
                select.AddParameter("@exchangecontractid", exchangecontractid);
                select.AddParameter("@pendingonly", FwConvert.LogicalToCharacter(pendingonly));
                searchresults = await qry.QueryToFwJsonTableAsync(select, true);
            }

            return searchresults;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> ExchangeRepairSearchAsync(FwSqlConnection conn, int pageno, int pagesize, string exchangecontractid)
        {
            FwJsonDataTable searchresults = null;

            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.PageNo   = pageno;
                select.PageSize = pagesize;
                select.Add("select barcode = (select top 1 ri.barcode");
                select.Add("                   from  rentalitem ri  with (nolock),");
                select.Add("                         repairitem rpi with (nolock)");
                select.Add("                   where ri.rentalitemid = rpi.rentalitemid");
                select.Add("                   and   rpi.repairid    = r.repairid),");
                select.Add("       mfgserial = (select top 1 ri.mfgserial");
                select.Add("                    from  rentalitem ri  with (nolock),");
                select.Add("                          repairitem rpi with (nolock)");
                select.Add("                    where ri.rentalitemid = rpi.rentalitemid");
                select.Add("                    and   rpi.repairid    = r.repairid),");
                select.Add("       r.qty,");
                select.Add("       m.masterno,");
                select.Add("       m.master,");
                select.Add("       r.billable,");
                select.Add("       damage = substring(r.damage, 1, 255),");
                select.Add("       r.repairno,");
                select.Add("       r.repairid");
                select.Add("from contractrepair cr with (nolock),");
                select.Add("     repair         r  with (nolock),");
                select.Add("     master         m  with (nolock)");
                select.Add("where cr.repairid      = r.repairid");
                select.Add("  and r.masterid       = m.masterid");
                select.Add("  and cr.contractid in (@exchangecontractid)");
                select.Parse();
                select.AddOrderBy("barcode");
                select.AddParameter("@exchangecontractid", exchangecontractid);
                searchresults = await qry.QueryToFwJsonTableAsync(select, true);
            }

            return searchresults;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> ExchangeTransferSearchAsync(FwSqlConnection conn, int pageno, int pagesize, string exchangecontractid)
        {
            FwJsonDataTable searchresults = null;

            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.PageNo   = pageno;
                select.PageSize = pagesize;
                select.Add("select *");
                select.Add("from dbo.functransferhistory(@masterid, @contractid)");
                select.Parse();
                select.AddOrderBy("fromwhcode");
                select.AddParameter("@masterid", string.Empty);
                select.AddParameter("@contractid", exchangecontractid);
                searchresults = await qry.QueryToFwJsonTableAsync(select, true);
            }

            return searchresults;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<dynamic> createexchangecontractAsync(FwSqlConnection conn, string orderid, string dealid, string departmentid, string locationid, string warehouseid, string usersid, string notes)
        {
            FwSqlCommand sp = new FwSqlCommand(conn, "createexchangecontract", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@orderid", orderid);
            sp.AddParameter("@dealid", dealid);
            sp.AddParameter("@departmentid", departmentid);
            sp.AddParameter("@locationid", locationid);
            sp.AddParameter("@warehouseid", warehouseid);
            sp.AddParameter("@usersid", usersid);
            sp.AddParameter("@notes", notes);
            sp.AddParameter("@exchangecontractid", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@outcontractid",      SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@incontractid",       SqlDbType.Char, ParameterDirection.Output);
            await sp.ExecuteAsync();
            dynamic result = new ExpandoObject();
            result.exchangecontractid = sp.GetParameter("@exchangecontractid").ToString().TrimEnd();
            result.outcontractid      = sp.GetParameter("@outcontractid").ToString().TrimEnd();
            result.incontractid       = sp.GetParameter("@incontractid").ToString().TrimEnd();
            return result;
        }
        //---------------------------------------------------------------------------------------------
        public class IsRentalICodeResponse
        {
            public bool found { get;set; } = false;
            public string masterid { get;set; } = string.Empty;
            public string description { get;set; } = string.Empty;
        }
        public async Task<IsRentalICodeResponse> isrentalicodeAsync(FwSqlConnection conn, string masterno, string availfrom, bool quantityonly)
        {
            FwSqlCommand sp = new FwSqlCommand(conn, "isrentalicode", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@masterno", masterno);
            sp.AddParameter("@availfrom", availfrom);
            sp.AddParameter("@quantityonly", FwConvert.LogicalToCharacter(quantityonly));
            sp.AddParameter("@description", SqlDbType.VarChar, ParameterDirection.Output);
            sp.AddParameter("@masterid", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@found", SqlDbType.Char, ParameterDirection.Output);
            await sp.ExecuteAsync();
            IsRentalICodeResponse result = new IsRentalICodeResponse();
            result.description = sp.GetParameter("@description").ToString().TrimEnd();
            result.masterid    = sp.GetParameter("@masterid").ToString().TrimEnd();
            result.found       = sp.GetParameter("@found").ToBoolean();
            return result;
        }
        //---------------------------------------------------------------------------------------------
        public class IsSerialNoResponse
        {
            public bool found { get;set; } = false;
            public string rentalitemid { get;set; } = string.Empty;
            public string masterid { get;set; } = string.Empty;
            public string masterno { get;set; } = string.Empty;
            public string description { get;set; } = string.Empty;
            public int matches { get;set; } = 0;
        }
        public async Task<IsSerialNoResponse> isserialnoAsync(FwSqlConnection conn, string serialno, string availfrom)
        {
            FwSqlCommand sp = new FwSqlCommand(conn, "isserialno", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@serialno", serialno);
            sp.AddParameter("@availfrom", availfrom);
            sp.AddParameter("@found", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@rentalitemid", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@masterid", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@masterno", SqlDbType.VarChar, ParameterDirection.Output);
            sp.AddParameter("@description", SqlDbType.VarChar, ParameterDirection.Output);
            sp.AddParameter("@matches", SqlDbType.Int, ParameterDirection.Output);
            await sp.ExecuteAsync();
            IsSerialNoResponse result = new IsSerialNoResponse();
            result.found        = sp.GetParameter("@found").ToBoolean();
            result.rentalitemid = sp.GetParameter("@rentalitemid").ToString().TrimEnd();
            result.masterid     = sp.GetParameter("@masterid").ToString().TrimEnd();
            result.masterno     = sp.GetParameter("@masterno").ToString().TrimEnd();
            result.description  = sp.GetParameter("@description").ToString().TrimEnd();
            result.matches      = sp.GetParameter("@matches").ToInt32();
            return result;
        }
        //---------------------------------------------------------------------------------------------
        public class IsBarcodeResponse
        {
            public bool found { get;set; } = false;
            public string rentalitemid { get;set; } = string.Empty;
            public string masterid { get;set; } = string.Empty;
            public string masterno { get;set; } = string.Empty;
            public string description { get;set; } = string.Empty;
        }
        public async Task<IsBarcodeResponse> isbarcodeAsync(FwSqlConnection conn, string barcode)
        {
            FwSqlCommand sp = new FwSqlCommand(conn, "isbarcode", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@barcode", barcode);
            sp.AddParameter("@masterid", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@rentalitemid", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@masterno", SqlDbType.VarChar, ParameterDirection.Output);
            sp.AddParameter("@description", SqlDbType.VarChar, ParameterDirection.Output);
            sp.AddParameter("@found", SqlDbType.Char, ParameterDirection.Output);
            await sp.ExecuteAsync();
            IsBarcodeResponse result = new IsBarcodeResponse();
            result.masterid     = sp.GetParameter("@masterid").ToString().TrimEnd();
            result.rentalitemid = sp.GetParameter("@rentalitemid").ToString().TrimEnd();
            result.masterno     = sp.GetParameter("@masterno").ToString().TrimEnd();
            result.description  = sp.GetParameter("@description").ToString().TrimEnd();
            result.found        = sp.GetParameter("@found").ToBoolean();
            return result;
        }
        //---------------------------------------------------------------------------------------------
        public class IsRfidResponse
        {
            public bool found { get;set; } = false;
            public string rentalitemid { get;set; } = string.Empty;
            public string masterid { get;set; } = string.Empty;
            public string masterno { get;set; } = string.Empty;
            public string description { get;set; } = string.Empty;
        }
        public async Task<IsRfidResponse> isrfidAsync(FwSqlConnection conn, string rfid)
        {
            FwSqlCommand sp = new FwSqlCommand(conn, "isrfid", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@rfid", rfid);
            sp.AddParameter("@masterid", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@rentalitemid", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@masterno", SqlDbType.VarChar, ParameterDirection.Output);
            sp.AddParameter("@description", SqlDbType.VarChar, ParameterDirection.Output);
            sp.AddParameter("@found", SqlDbType.Char, ParameterDirection.Output);
            await sp.ExecuteAsync();
            IsRfidResponse result = new IsRfidResponse();
            result.masterid     = sp.GetParameter("@masterid").ToString().TrimEnd();
            result.rentalitemid = sp.GetParameter("@rentalitemid").ToString().TrimEnd();
            result.masterno     = sp.GetParameter("@masterno").ToString().TrimEnd();
            result.description  = sp.GetParameter("@description").ToString().TrimEnd();
            result.found        = sp.GetParameter("@found").ToBoolean();
            return result;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<dynamic> getinbarcodeexchangeiteminfoAsync(FwSqlConnection conn, string barcode, string orderid, string dealid, string departmentid, string warehouseid, string ordertype)
        {
            FwSqlCommand sp = new FwSqlCommand(conn, "getinbarcodeexchangeiteminfo", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@barcode", barcode);
            sp.AddParameter("@orderid", orderid);
            sp.AddParameter("@dealid", dealid);
            sp.AddParameter("@departmentid", departmentid);
            sp.AddParameter("@warehouseid", warehouseid);
            sp.AddParameter("@ordertype", ordertype);
            sp.AddParameter("@returnitemorderid", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitemdealid", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitemdepartmentid", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitemmasterid", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitemtrackedby", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitemrentalitemid", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitemvendorid", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitempoid", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitemwarehouseid", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitemreturntowarehouseid", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitemordertranid", SqlDbType.Int, ParameterDirection.Output);
            sp.AddParameter("@returniteminternalchar", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitemmasteritemid", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitemdescription", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitemorderno", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitemavailfromdatetime", SqlDbType.DateTime, ParameterDirection.Output);
            sp.AddParameter("@returnitemavailtodatetime", SqlDbType.DateTime, ParameterDirection.Output);
            sp.AddParameter("@returnitempendingrepairid", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnmsg", SqlDbType.VarChar, ParameterDirection.Output);
            await sp.ExecuteAsync();
            dynamic result = new ExpandoObject();
            result.returnitemorderid             = sp.GetParameter("@returnitemorderid").ToString().TrimEnd();
            result.returnitemorderdesc           = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "dealorder", "orderid", result.returnitemorderid, "orderdesc");
            result.returnitemdealid              = sp.GetParameter("@returnitemdealid").ToString().TrimEnd();
            result.returnitemdepartmentid        = sp.GetParameter("@returnitemdepartmentid").ToString().TrimEnd();
            result.returnitemmasterid            = sp.GetParameter("@returnitemmasterid").ToString().TrimEnd();
            result.returnitemmasterno            = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "master", "masterid", result.returnitemmasterid, "masterno");
            result.returnitemtrackedby           = sp.GetParameter("@returnitemtrackedby").ToString().TrimEnd();
            result.returnitemrentalitemid        = sp.GetParameter("@returnitemrentalitemid").ToString().TrimEnd();
            result.returnitemvendorid            = sp.GetParameter("@returnitemvendorid").ToString().TrimEnd();
            result.returnitemvendor              = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "vendor", "vendorid", result.returnitemvendorid, "vendor");
            result.returnitempoid                = sp.GetParameter("@returnitempoid").ToString().TrimEnd();
            result.returnitempono                = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "dealorder", "orderid", result.returnitempoid, "orderno");
            result.returnitemwarehouseid         = sp.GetParameter("@returnitemwarehouseid").ToString().TrimEnd();
            result.returnitemwarehouse           = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "warehouse", "warehouseid", result.returnitemwarehouseid, "warehouse");
            result.returnitemreturntowarehouseid = sp.GetParameter("@returnitemreturntowarehouseid").ToString().TrimEnd();
            result.returnitemordertranid         = sp.GetParameter("@returnitemordertranid").ToInt32();
            result.returniteminternalchar        = sp.GetParameter("@returniteminternalchar").ToString().TrimEnd();
            result.returnitemmasteritemid        = sp.GetParameter("@returnitemmasteritemid").ToString().TrimEnd();
            result.returnitemdescription         = sp.GetParameter("@returnitemdescription").ToString().TrimEnd();
            result.returnitemorderno             = sp.GetParameter("@returnitemorderno").ToString().TrimEnd();
            result.returnitemavailfromdatetime   = sp.GetParameter("@returnitemavailfromdatetime").ToFwDateTime();
            result.returnitemavailtodatetime     = sp.GetParameter("@returnitemavailtodatetime").ToFwDateTime();
            result.returnitempendingrepairid     = sp.GetParameter("@returnitempendingrepairid").ToString().TrimEnd();
            result.returnmsg                     = sp.GetParameter("@returnmsg").ToString().TrimEnd();
            return result;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> exchangenonbcviewAsync(FwSqlConnection conn, string masterid, string dealid, string orderid, int qty, string vendorid)
        {
            FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            qry.Add("select *");
            qry.Add("from exchangenonbcview with (nolock)");
            qry.Add("where masterid = @masterid");
            qry.Add("  and dealid   = @dealid");
            qry.Add("  and orderid  = @orderid");
            qry.Add("  and qtyout   > @qtyout");
            qry.Add("  and vendorid > @vendorid");
            FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();
            return dt;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<TBCItem> getoutbarcodeexchangeiteminfoAsync(FwSqlConnection conn, string barcode, string masterid, string warehouseid, string ordertype, string usersid, bool removefromcontainer, FwDateTime availthrough)
        {
            FwSqlCommand sp = new FwSqlCommand(conn, "getoutbarcodeexchangeiteminfo", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@barcode", barcode);
            sp.AddParameter("@masterid", masterid);
            sp.AddParameter("@warehouseid", warehouseid);
            sp.AddParameter("@ordertype", ordertype);
            sp.AddParameter("@usersid", usersid);
            sp.AddParameter("@removefromcontainer", FwConvert.LogicalToCharacter(removefromcontainer));
            sp.AddParameter("@availthrough", availthrough);
            sp.AddParameter("@returnitemmasterid", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitemtrackedby", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitemrentalitemid", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitemvendorid", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitempoid", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitemwarehouseid", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitemordertranid", SqlDbType.Int, ParameterDirection.Output);
            sp.AddParameter("@returniteminternalchar", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitemdescription", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitemqcrequired", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitemincontainer", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnmsg", SqlDbType.VarChar, ParameterDirection.Output);
            await sp.ExecuteNonQueryAsync();
            TBCItem item = new TBCItem();
            item.barcode         = barcode;
            item.trackedby       = RwConstants.TrackedBy.BARCODE;
            item.masterid        = sp.GetParameter("@returnitemmasterid").ToString().TrimEnd();
            item.trackedby       = sp.GetParameter("@returnitemtrackedby").ToString().TrimEnd();
            item.rentalitemid    = sp.GetParameter("@returnitemrentalitemid").ToString().TrimEnd();
            item.vendorid        = sp.GetParameter("@returnitemvendorid").ToString().TrimEnd();
            item.vendor          = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "vendor", "vendorid", item.vendorid, "vendor");
            item.poid            = sp.GetParameter("@returnitempoid").ToString().TrimEnd();
            item.pono            = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "dealorder", "orderid", item.poid, "orderno");
            item.warehouseid     = sp.GetParameter("@returnitemwarehouseid").ToString().TrimEnd();
            item.whcode          = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "warehouse", "warehouseid", item.warehouseid, "whcode");
            item.description     = sp.GetParameter("@returnitemdescription").ToString().TrimEnd();
            item.qcrequired      = sp.GetParameter("@returnitemqcrequired").ToBoolean();
            item.itemincontainer = sp.GetParameter("@returnitemincontainer").ToBoolean();
            item.msg             = sp.GetParameter("@returnmsg").ToString().TrimEnd();
            if (!string.IsNullOrEmpty(item.msg))
            {
                item.status  = TBCItem.StatusTypes.Error;
            }
            return item;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<TBCItem> getoutserialexchangeiteminfoAsync(FwSqlConnection conn, string itemstatus, ExchangeModel exchange, UserContext user, string serialno, IsSerialNoResponse isSerialNoResponse)
        {
            TBCItem item;
            item = await getoutbarcodeexchangeiteminfoAsync(conn: conn,
                                                            barcode: serialno, 
                                                            masterid: isSerialNoResponse.masterid, 
                                                            warehouseid: user.primarywarehouseid, 
                                                            ordertype: ConvertOrderModeToOrderType(exchange.ordermode),
                                                            usersid: user.usersid, 
                                                            removefromcontainer: exchange.removingFromContainer, 
                                                            availthrough: exchange.frmInExchangeItem.item.availtodatetime);
            item.serialno  = serialno;
            item.trackedby = RwConstants.TrackedBy.SERIALNO;
            return item;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<TBCItem> getpendingoutbarcodeexchangeiteminfoAsync(FwSqlConnection conn, string barcode, string usersid, string dealid, string warehouseid, string ordertype, bool removefromcontainer)
        {
            FwSqlCommand sp = new FwSqlCommand(conn, "getpendingoutbarcodeexchangeiteminfo", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@barcode", barcode);
            sp.AddParameter("@usersid", usersid);
            sp.AddParameter("@dealid", dealid);
            sp.AddParameter("@warehouseid", warehouseid);
            sp.AddParameter("@ordertype", ordertype);
            sp.AddParameter("@removefromcontainer", FwConvert.LogicalToCharacter(removefromcontainer));
            sp.AddParameter("@returnitemmasterid", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitemtrackedby", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitemrentalitemid", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitemwarehouseid", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitemordertranid", SqlDbType.Int, ParameterDirection.Output);
            sp.AddParameter("@returniteminternalchar", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitemdescription", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitemqcrequired", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnitemincontainer", SqlDbType.Char, ParameterDirection.Output);
            sp.AddParameter("@returnmsg", SqlDbType.VarChar, ParameterDirection.Output);
            await sp.ExecuteAsync();
            TBCItem item = new TBCItem();
            item.barcode         = barcode;
            item.trackedby       = RwConstants.TrackedBy.BARCODE;
            item.masterid        = sp.GetParameter("@returnitemmasterid").ToString().TrimEnd();
            item.trackedby       = sp.GetParameter("@returnitemtrackedby").ToString().TrimEnd();
            item.rentalitemid    = sp.GetParameter("@returnitemrentalitemid").ToString().TrimEnd();
            item.warehouseid     = sp.GetParameter("@returnitemwarehouseid").ToString().TrimEnd();
            item.whcode          = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "warehouse", "warehouseid", item.warehouseid, "whcode");
            item.description     = sp.GetParameter("@returnitemdescription").ToString().TrimEnd();
            item.qcrequired      = sp.GetParameter("@returnitemqcrequired").ToBoolean();
            item.itemincontainer = sp.GetParameter("@returnitemincontainer").ToBoolean();
            item.msg             = sp.GetParameter("@returnmsg").ToString().TrimEnd();
            if (!string.IsNullOrEmpty(item.msg))
            {
               item.status = TBCItem.StatusTypes.Error;
            }

            return item;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<StatusMsgResponse> exchangebcAsync(FwSqlConnection conn, string exchangecontractid, string inbarcode, string outbarcode, string pendingorderid, string pendingmasteritemid, bool allowcrossicode, /*string torepair,*/ string usersid)
        {
            FwSqlCommand sp = new FwSqlCommand(conn, "exchangebc", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@exchangecontractid", exchangecontractid);
            sp.AddParameter("@inbarcode", inbarcode);
            sp.AddParameter("@outbarcode", outbarcode);
            sp.AddParameter("@pendingorderid", pendingorderid);
            sp.AddParameter("@pendingmasteritemid", pendingmasteritemid);
            sp.AddParameter("@allowcrossicode", FwConvert.LogicalToCharacter(allowcrossicode));
            //sp.AddParameter("@torepair", torepair);
            sp.AddParameter("@usersid", usersid);
            sp.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
            sp.AddParameter("@msg", SqlDbType.VarChar, ParameterDirection.Output);
            await sp.ExecuteAsync();
            StatusMsgResponse result = new StatusMsgResponse();
            result.status = sp.GetParameter("@status").ToInt32();
            result.msg    = sp.GetParameter("@msg").ToString().TrimEnd();
            return result;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<StatusMsgResponse> exchangenonbcAsync(FwSqlConnection conn, string exchangecontractid, string outmasterid, string inorderid, string inmasteritemid, string inbarcode, /*string inrentalitemid,*/ string outorderid, string outmasteritemid, string outbarcode, string outserialno, /*string outrentalitemid,*/ int qty, bool allowcrossicode, /*string torepair, */string usersid)
        {
            FwSqlCommand sp = new FwSqlCommand(conn, "exchangenonbc", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@exchangecontractid", exchangecontractid);
            sp.AddParameter("@outmasterid", outmasterid);
            sp.AddParameter("@inorderid", inorderid);
            sp.AddParameter("@inmasteritemid", inmasteritemid);
            sp.AddParameter("@inbarcode", inbarcode);
            //sp.AddParameter("@inrentalitemid", inrentalitemid);
            sp.AddParameter("@outorderid", outorderid);
            sp.AddParameter("@outmasteritemid", outmasteritemid);
            sp.AddParameter("@outbarcode", outbarcode);
            sp.AddParameter("@outserialno", outserialno);
            //sp.AddParameter("@outrentalitemid", outrentalitemid);
            sp.AddParameter("@qty", qty);
            sp.AddParameter("@allowcrossicode", FwConvert.LogicalToCharacter(allowcrossicode));
            //sp.AddParameter("@torepair", torepair);
            sp.AddParameter("@usersid", usersid);
            sp.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
            sp.AddParameter("@msg", SqlDbType.VarChar, ParameterDirection.Output);
            await sp.ExecuteAsync();
            StatusMsgResponse result = new StatusMsgResponse();
            result.status = sp.GetParameter("@status").ToInt32();
            result.msg    = sp.GetParameter("@msg").ToString().TrimEnd();
            return result;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<StatusMsgResponse> exchangeserialAsync(FwSqlConnection conn, string exchangecontractid, /*string masterid,*/ string inserialno, string outserialno, string pendingorderid, string pendingmasteritemid, bool allowcrossicode, /*string torepair,*/ string usersid)
        {
            FwSqlCommand sp = new FwSqlCommand(conn, "exchangeserial", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@exchangecontractid", exchangecontractid);
            //sp.AddParameter("@masterid", masterid);
            sp.AddParameter("@inserialno", inserialno);
            sp.AddParameter("@outserialno", outserialno);
            sp.AddParameter("@pendingorderid", pendingorderid);
            sp.AddParameter("@pendingmasteritemid", pendingmasteritemid);
            sp.AddParameter("@allowcrossicode", FwConvert.LogicalToCharacter(allowcrossicode));
            //sp.AddParameter("@torepair", torepair);
            sp.AddParameter("@usersid", usersid);
            sp.AddParameter("@status", SqlDbType.Int, ParameterDirection.Output);
            sp.AddParameter("@msg", SqlDbType.VarChar, ParameterDirection.Output);
            await sp.ExecuteAsync();
            StatusMsgResponse result = new StatusMsgResponse();
            result.status = sp.GetParameter("@status").ToInt32();
            result.msg    = sp.GetParameter("@msg").ToString().TrimEnd();
            return result;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<string> splitmasteritemAsync(FwSqlConnection conn, string orderid, string masteritemid, int splitqty, int splitsubqty)
        {
            FwSqlCommand sp = new FwSqlCommand(conn, "splitmasteritem", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@orderid", orderid);
            sp.AddParameter("@masteritemid", masteritemid);
            sp.AddParameter("@splitqty", splitqty);
            sp.AddParameter("@splitsubqty", splitsubqty);
            sp.AddParameter("@newmasteritemid", SqlDbType.Char, ParameterDirection.Output);
            await sp.ExecuteAsync();
            string newmasteritemid = sp.GetParameter("@newmasteritemid").ToString();
            return newmasteritemid;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<string> insertmasteritemAsync(FwSqlConnection conn, String orderid, String masterid, String warehouseid, int qty)
        {
            FwSqlCommand sp = new FwSqlCommand(conn, "insertmasteritem", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@orderid", orderid);
            sp.AddParameter("@masterid", masterid);
            sp.AddParameter("@warehouseid", warehouseid);
            //sp.AddParameter("@description", description);
            //sp.AddParameter("@catalogid", catalogid);
            //sp.AddParameter("@vendorid", vendorid);
            sp.AddParameter("@qty", qty);
            //sp.AddParameter("@spacerateid", spacerateid);
            //sp.AddParameter("@schedulestatusid", schedulestatusid);
            //sp.AddParameter("@updatebottomline", updatebottomline);
            //sp.AddParameter("@rentfromdate", rentfromdate);
            //sp.AddParameter("@rentfromtime", rentfromtime);
            //sp.AddParameter("@renttodate", renttodate);
            //sp.AddParameter("@renttotime", renttotime);
            //sp.AddParameter("@note", note);
            //sp.AddParameter("@forcenewrecord", forcenewrecord);
            sp.AddParameter("@masteritemid", SqlDbType.Char, ParameterDirection.Output);
            await sp.ExecuteAsync();
            string masteritemid = sp.GetParameter("@masteritemid").ToString();
            return masteritemid;
        }
        //---------------------------------------------------------------------------------------------
        public class StatusMsgResponse
        {
            public int status { get;set; } = 0;
            public string msg { get;set; } = string.Empty;
        }
        public async Task<dynamic> insertcheckoutauditAsync(FwSqlConnection conn, String orderid, String masteritemid, String usersid, int priorqty, int newqty)
        {
            FwSqlCommand sp = new FwSqlCommand(conn, "insertcheckoutaudit", this.ApplicationConfig.DatabaseSettings.QueryTimeout);
            sp.AddParameter("@orderid", orderid);
            sp.AddParameter("@masteritemid", masteritemid);
            sp.AddParameter("@usersid", usersid);
            sp.AddParameter("@priorqty", priorqty);
            sp.AddParameter("@newqty", newqty);
            sp.AddParameter("@status", SqlDbType.Decimal, ParameterDirection.Output);
            sp.AddParameter("@msg", SqlDbType.VarChar, ParameterDirection.Output);
            await sp.ExecuteAsync();
            StatusMsgResponse result = new StatusMsgResponse();
            result.status = sp.GetParameter("@status").ToInt32();
            result.msg    = sp.GetParameter("@msg").ToString();
            return result;
        }
        //---------------------------------------------------------------------------------------------
        public TBCItem CopyBCItem(TBCItem sourceBcItem)
        {
            TBCItem destBcItem = new TBCItem();
            destBcItem.barcode      = sourceBcItem.barcode;
            destBcItem.bctype       = sourceBcItem.bctype;
            destBcItem.orderid      = sourceBcItem.orderid;
            destBcItem.dealid       = sourceBcItem.dealid;
            destBcItem.deptid       = sourceBcItem.deptid;
            destBcItem.masterid     = sourceBcItem.masterid;
            destBcItem.masteritemid = sourceBcItem.masteritemid;
            destBcItem.rentalitemid = sourceBcItem.rentalitemid;
            destBcItem.warehouseid  = sourceBcItem.warehouseid;
            destBcItem.ordertranid  = sourceBcItem.ordertranid;
            destBcItem.internalchar = sourceBcItem.internalchar;
            destBcItem.masterno     = sourceBcItem.masterno;
            destBcItem.description  = sourceBcItem.description;
            destBcItem.vendor       = sourceBcItem.vendor;
            destBcItem.vendorid     = sourceBcItem.vendorid;
            destBcItem.pono         = sourceBcItem.pono;
            destBcItem.poid         = sourceBcItem.poid;
            destBcItem.orderno      = sourceBcItem.orderno;
            destBcItem.qtyordered   = sourceBcItem.qtyordered;
            destBcItem.subqty       = sourceBcItem.subqty;
            destBcItem.totalin      = sourceBcItem.totalin;
            destBcItem.sessionin    = sourceBcItem.sessionin;
            destBcItem.stillout     = sourceBcItem.stillout;
            destBcItem.statusno     = sourceBcItem.statusno;
            destBcItem.neworder     = sourceBcItem.neworder;
            destBcItem.trackedby    = sourceBcItem.trackedby;
            destBcItem.qty          = sourceBcItem.qty;
            destBcItem.pending      = sourceBcItem.pending;
            destBcItem.msg          = sourceBcItem.msg;
            destBcItem.status       = sourceBcItem.status;
            destBcItem.statuscode   = sourceBcItem.statuscode;
            destBcItem.isbarcode    = sourceBcItem.isbarcode;
            destBcItem.contractid   = sourceBcItem.contractid;
            destBcItem.spaceid      = sourceBcItem.spaceid;
            return destBcItem;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<bool> ShouldConfirmAllowCrossIcodeAsync(FwSqlConnection conn, int status, string usersid)
        {
            bool result = (
                (
                    ((status == RwConstants.Exchange.EXCHANGE_STATUS_ITEM_DIFFERENT_ICODE_BARCODE) || (status == RwConstants.Exchange.EXCHANGE_STATUS_ITEM_DIFFERENT_ICODE_QUANTITY))
                    &&
                    (await FwSqlCommand.GetDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "appsusersview", "usersid", usersid, "allowcrossicodeexchange")).ToBoolean()
                )
                    ||
                (
                    (status == RwConstants.Exchange.EXCHANGE_STATUS_ITEM_DIFFERENT_ICODE_PENDING)
                    &&
                    (await FwSqlCommand.GetDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "appsusersview", "usersid", usersid, "allowcrossicodependingexchange")).ToBoolean()
                )
            );
            return result;
        }
        //---------------------------------------------------------------------------------------------
        public async Task ExchangeQuantityAsync(FwSqlConnection conn, ExchangeModel exchange, UserContext user)
        {
            //ExchangeItemResponse response = new ExchangeItemResponse();
            //response.success = true;
            //response.msg = string.Empty;
            //response.resetall = false;

            if ((exchange.qty > 0)
               && (
                      (!string.IsNullOrWhiteSpace(exchange.frmOutExchangeItem.barcode)// && validExchangeHeader(outExchangeItemInfo.barcode))
                      ||
                      (string.IsNullOrWhiteSpace(exchange.frmOutExchangeItem.barcode) && exchange.completingpending))
                  )
               )
            {
                StatusMsgResponse r = await exchangenonbcAsync(conn: conn,
                                                               exchangecontractid: exchange.exchangecontractid,
                                                               outmasterid: exchange.completingpending ? exchange.frmInExchangeItem.item.masterid : exchange.frmOutExchangeItem.item.masterid,
                                                               inorderid:  exchange.frmInExchangeItem.item.orderid,
                                                               inmasteritemid:  exchange.frmInExchangeItem.item.masteritemid,
                                                               inbarcode:  exchange.frmInExchangeItem.item.barcode,
                                                               //inrentalitemid:  exchange.frmInExchangeItem.item.rentalitemid,
                                                               outorderid: exchange.frmOutExchangeItem.item.orderid,
                                                               outmasteritemid: exchange.frmOutExchangeItem.item.masteritemid,
                                                               outbarcode: exchange.frmOutExchangeItem.item.barcode,
                                                               outserialno: exchange.frmOutExchangeItem.item.serialno,
                                                               //outrentalitemid: exchange.frmOutExchangeItem.item.rentalitemid,
                                                               qty: exchange.qty,
                                                               allowcrossicode: exchange.allowcrossicode,
                                                               //torepair: torepair,
                                                               usersid: user.usersid);
                if (r.status == 0)
                {
                    //response.hideerror = true;
                    exchange.response.success = true;
                }
                else
                {
                    exchange.response.msg = r.msg;
                    //edtQty.active := true;
                    //edtQty.text   := '1';
                    //edtQty.setFocus;
                    //if (frmInExchangeItem.edtBarCode.active)
                    //{
                    //    frmInExchangeItem.edtBarCode.setFocus;
                    //}
                    //else if frmOutExchangeItem.edtBarCode.active
                    //{
                    //    frmOutExchangeItem.edtBarCode.setFocus;
                    //}
                    exchange.response.confirmallowcrossicode = await ShouldConfirmAllowCrossIcodeAsync(conn: conn, 
                                                                                                       status: r.status, 
                                                                                                       usersid: user.usersid);
                    //enableCrossICodeExchangeButton(status);
                }
            }
            if (exchange.response.success)
            {
                exchange.response.resetall = true;
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task ExchangeBarcodeAsync(FwSqlConnection conn, ExchangeModel exchange, UserContext user)
        {
            //ExchangeItemResponse response = new ExchangeItemResponse();
            //response.success = true;
            //response.msg = string.Empty;
            //response.resetall = false;
            StatusMsgResponse r = await exchangebcAsync(conn: conn,
                                                        exchangecontractid: exchange.exchangecontractid,
                                                        inbarcode: exchange.frmInExchangeItem.item.barcode,
                                                        outbarcode: exchange.frmOutExchangeItem.item.barcode,
                                                        pendingorderid: exchange.completingpending ? exchange.frmInExchangeItem.item.orderid : string.Empty,
                                                        pendingmasteritemid: exchange.completingpending ? exchange.frmInExchangeItem.item.masteritemid : string.Empty,
                                                        allowcrossicode: exchange.allowcrossicode,
                                                        //torepair: torepair,
                                                        usersid: user.usersid);
            if (r.status == 0)
            {
                exchange.response.success = true;
            }
            else
            {
                exchange.response.success = false;
                exchange.response.msg = r.msg;
                exchange.response.confirmallowcrossicode = await ShouldConfirmAllowCrossIcodeAsync(conn: conn, 
                                                                                                   status: r.status, 
                                                                                                   usersid: user.usersid);
            }
            if (exchange.response.success)
            {
                exchange.response.resetall = true;
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task ExchangeRfidAsync(FwSqlConnection conn, ExchangeModel exchange, UserContext user)
        {
            //ExchangeItemResponse response = new ExchangeItemResponse();
            //response.success = true;
            //response.msg = string.Empty;
            //response.resetall = false;
            StatusMsgResponse r = await exchangebcAsync(conn: conn,
                                                        exchangecontractid: exchange.exchangecontractid,
                                                        inbarcode: exchange.frmInExchangeItem.barcode,
                                                        outbarcode: exchange.frmOutExchangeItem.barcode,
                                                        pendingorderid: exchange.completingpending ? exchange.frmInExchangeItem.item.orderid : string.Empty,
                                                        pendingmasteritemid: exchange.completingpending ? exchange.frmInExchangeItem.item.masteritemid : string.Empty,
                                                        allowcrossicode: exchange.allowcrossicode,
                                                        //torepair: torepair,
                                                        usersid: user.usersid);
            if (r.status == 0)
            {
                exchange.response.success = true;
            }
            else
            {
                exchange.response.success = false;
                exchange.response.msg = r.msg;
                exchange.response.confirmallowcrossicode = await ShouldConfirmAllowCrossIcodeAsync(conn: conn,
                                                                                                   status: r.status, 
                                                                                                   usersid: user.usersid);
            }
            if (exchange.response.success)
            {
                exchange.response.resetall = true;
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task ExchangeSerialAsync(FwSqlConnection conn, ExchangeModel exchange, UserContext user)
        {
            //dynamic response = new ExpandoObject();
            //response.success = true;
            //response.msg = string.Empty;
            //response.resetall = false;
            StatusMsgResponse r = await exchangeserialAsync(conn: conn,
                                                            exchangecontractid: exchange.exchangecontractid,
                                                            //masterid: response.exchange.frmInExchangeItem.item.masterid,
                                                            inserialno: exchange.frmInExchangeItem.item.serialno,
                                                            outserialno: exchange.frmOutExchangeItem.item.serialno,
                                                            pendingorderid: exchange.completingpending ? exchange.frmOutExchangeItem.item.orderid : string.Empty,
                                                            pendingmasteritemid: exchange.completingpending ? exchange.frmOutExchangeItem.item.masteritemid : string.Empty,
                                                            allowcrossicode: exchange.allowcrossicode,
                                                            //torepair: torepair,
                                                            usersid: user.usersid);
            if (r.status == 0)
            {
                exchange.response.success = true;
            }
            else
            {
                exchange.response.success = false;
                exchange.response.msg = r.msg;
                exchange.response.confirmallowcrossicode = await ShouldConfirmAllowCrossIcodeAsync(conn: conn,
                                                                                                   status: r.status, 
                                                                                                   usersid: user.usersid);
            }
            if (exchange.response.success)
            {
                exchange.response.resetall = true;
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task ExchangeItemAsync(FwSqlConnection conn, ExchangeModel exchange, UserContext user)
        {
            bool proceed = true;
            ExchangeResponse response = new ExchangeResponse();
            if (proceed)
            {
                await GetItemInfoAsync(conn: conn,
                                       itemstatus: RwConstants.ItemStatus.IN,
                                       exchange: exchange,
                                       user: user);
                if (exchange.frmInExchangeItem.item.status == TBCItem.StatusTypes.Error)
                {
                    proceed = false;
                }
            }
            if (proceed)
            {
                await GetItemInfoAsync(conn: conn,
                            itemstatus: RwConstants.ItemStatus.OUT,
                            exchange: exchange,
                            user: user);
                if (exchange.frmInExchangeItem.item.status == TBCItem.StatusTypes.Error)
                {
                    proceed = false;
                }
            }

            if (proceed && exchange.frmOutExchangeItem.item.trackedby != exchange.frmInExchangeItem.item.trackedby)
            {
                proceed = false;
                throw new Exception("Tracked by types don't match!");
            }

            if (proceed && exchange.frmOutExchangeItem.item.trackedby == RwConstants.TrackedBy.BARCODE)
            {
                await ExchangeBarcodeAsync(conn: conn,
                                           exchange: exchange,
                                           user: user);
            }
            else if (proceed && exchange.frmOutExchangeItem.item.trackedby == RwConstants.TrackedBy.SERIALNO)
            {
                await ExchangeSerialAsync(conn: conn,
                                          exchange: exchange,
                                          user: user);
            }
            else if (proceed && exchange.frmOutExchangeItem.item.trackedby == RwConstants.TrackedBy.RFID)
            {
                await ExchangeRfidAsync(conn: conn,
                                        exchange: exchange,
                                        user: user);
            }
            else if (proceed && exchange.frmOutExchangeItem.item.trackedby == RwConstants.TrackedBy.QUANTITY)
            {
                await ExchangeQuantityAsync(conn: conn,
                                            exchange: exchange,
                                            user: user);
            }
            //if (proceed)
            //{
            //    if (!string.IsNullOrEmpty(item.pendingRepairId))
            //    {
            //        sleep(500);  // let the success sound finish
            //        msg := 'Item ' + edtBarCode.text + ' is on Pending Repair ' + getData('repair', 'repairid', item.pendingRepairId, 'repairno');
            //        TffExchange(owner).showWarning(msg);
            //        warningShown := true;
            //    }
            //}
        } 
        //---------------------------------------------------------------------------------------------
        public async Task GetItemInfoAsync(FwSqlConnection conn, string itemstatus, ExchangeModel exchange, UserContext user)
        {
            string possibleserialno, barcode;
            possibleserialno = "";
            if (itemstatus == RwConstants.ItemStatus.IN)
            {
                barcode = exchange.frmInExchangeItem.barcode;
            }
            else //if (itemstatus == RwConstants.ItemStatus.OUT)
            {
                barcode = exchange.frmOutExchangeItem.barcode;
            }
            if (barcode.IndexOf('|') > 0)  //jh 03/28/2013 | can be used as a serial/I-Code separator
            {
                possibleserialno = barcode.Split('|')[0];
            }
            IsSerialNoResponse isSerialNoResponse = await isserialnoAsync(conn: conn,
                                                                          serialno: barcode, 
                                                                          availfrom: RwConstants.AvailFrom.WAREHOUSE);
            if (isSerialNoResponse.found)
            {
                await GetSerialItemInfoAsync(conn: conn,
                                  itemstatus: itemstatus,
                                  exchange: exchange,
                                  user: user,
                                  serialno: barcode,
                                  isSerialNoResponse: isSerialNoResponse);
            }
            IsRentalICodeResponse isRentalICodeResponse = await isrentalicodeAsync(conn: conn, 
                                                                                   masterno: barcode, 
                                                                                   availfrom: RwConstants.AvailFrom.WAREHOUSE, 
                                                                                   quantityonly: false);
            if (isRentalICodeResponse.found)
            {
               await GetQuantityItemInfoAsync(conn: conn,
                                   itemstatus: itemstatus,
                                   exchange: exchange,
                                   user: user,
                                   isRentalICodeResponse: isRentalICodeResponse);
            }

            IsBarcodeResponse isBarcodeResponse = await isbarcodeAsync(conn: conn, barcode: barcode);
            if (isBarcodeResponse.found)
            {
                await GetBarcodeItemInfoAsync(conn: conn,
                                   itemstatus: itemstatus,
                                   exchange: exchange,
                                   user: user,
                                   barcode: barcode,
                                   isBarcodeResponse: isBarcodeResponse);
            }
        }
        //---------------------------------------------------------------------------------------------
        private async Task GetQuantityItemInfoAsync(FwSqlConnection conn, string itemstatus, ExchangeModel exchange, UserContext user, IsRentalICodeResponse isRentalICodeResponse)
        {
            if (itemstatus == RwConstants.ItemStatus.OUT && string.IsNullOrEmpty(exchange.frmInExchangeItem.barcode))
            {
               await GetPendingOutQuantityItemInfoAsync(conn: conn, 
                                                        itemstatus: itemstatus,
                                                        exchange: exchange,
                                                        user: user,
                                                        isRentalICodeResponse: isRentalICodeResponse);
            }
            else if (itemstatus == RwConstants.ItemStatus.IN && exchange.completingpending)
            {
               await GetPendingInQuantityItemInfoAsync(conn: conn, 
                                                       itemstatus: itemstatus,
                                                       exchange: exchange,
                                                       user: user,
                                                       isRentalICodeResponse: isRentalICodeResponse);
            }
            else if (itemstatus == RwConstants.ItemStatus.IN || itemstatus == RwConstants.ItemStatus.OUT)
            {
               await GetInOutQuantityItemInfoAsync(conn: conn,
                                                   itemstatus: itemstatus,
                                                   exchange: exchange,
                                                   user: user,
                                                   isRentalICodeResponse: isRentalICodeResponse);
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task GetPendingOutQuantityItemInfoAsync(FwSqlConnection conn, string itemstatus, ExchangeModel exchange, UserContext user, IsRentalICodeResponse isRentalICodeResponse)
        {
            ExchangeItem frmExchangeItem;
            if (itemstatus == RwConstants.ItemStatus.IN)
            {
                frmExchangeItem = exchange.frmInExchangeItem;
            }
            else //if (request.itemstatus == RwConstants.ItemStatus.OUT)
            {
                frmExchangeItem = exchange.frmOutExchangeItem;
            }
            string masterno = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "master", "masterid", isRentalICodeResponse.masterid, "masterno");
            string master   = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "master", "masterid", isRentalICodeResponse.masterid, "master");
            frmExchangeItem.item.status  = TBCItem.StatusTypes.Error;
            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                qry.Add("select *");
                qry.Add("from  exchangependingoutnonbcview with (nolock)");
                qry.Add("where masterid   = @masterid");
                qry.Add("  and locationid = @locationid");
                qry.Add("  and dealid     = @dealid");
                qry.Add("  and inventorydepartmentid = @inventorydepartmentid");  //jh 12/09/09 CAS-6696-YZVS
                qry.AddParameter("@masterid", isRentalICodeResponse.masterid);
                qry.AddParameter("@locationid", user.primarylocationid);
                qry.AddParameter("@dealid", exchange.dealid);
                qry.AddParameter("@inventorydepartmentid", user.rentalinventorydepartmentid); 
                await qry.ExecuteAsync();
                switch(qry.RowCount)
                {
                   case 0:
                        frmExchangeItem.item.msg = "I-Code " + masterno + " is not OUT on this Deal.";
                        break;
                   case 1:
                        frmExchangeItem.item.status       = TBCItem.StatusTypes.Found;
                        frmExchangeItem.item.barcode      = string.Empty;
                        frmExchangeItem.item.trackedby    = RwConstants.TrackedBy.QUANTITY;
                        frmExchangeItem.item.masterno     = qry.GetField("masterno").ToString().TrimEnd();
                        frmExchangeItem.item.masterid     = isRentalICodeResponse.masterid;
                        frmExchangeItem.item.rentalitemid = string.Empty;
                        frmExchangeItem.item.orderid      = string.Empty;
                        frmExchangeItem.item.dealid       = qry.GetField("dealid").ToString().TrimEnd();
                        frmExchangeItem.item.deptid       = qry.GetField("departmentid").ToString().TrimEnd();
                        frmExchangeItem.item.warehouseid  = qry.GetField("warehouseid").ToString().TrimEnd();
                        frmExchangeItem.item.whcode       = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "warehouse", "warehouseid", frmExchangeItem.item.warehouseid, "whcode");
                        frmExchangeItem.item.ordertranid  = 0;
                        frmExchangeItem.item.internalchar = string.Empty;
                        frmExchangeItem.item.masteritemid = string.Empty;
                        frmExchangeItem.item.description  = qry.GetField("description").ToString().TrimEnd();
                        frmExchangeItem.item.orderno      = string.Empty;
                        frmExchangeItem.item.qty          = 0;
                        frmExchangeItem.item.pending      = false;
                        frmExchangeItem.item.vendorid     = qry.GetField("vendorid").ToString().TrimEnd();
                        frmExchangeItem.item.vendor       = qry.GetField("vendor").ToString().TrimEnd();
                        frmExchangeItem.item.poid         = qry.GetField("poid").ToString().TrimEnd();
                        frmExchangeItem.item.pono         = qry.GetField("pono").ToString().TrimEnd();
                        break;
                   default:
                        frmExchangeItem.item.msg             = "Select a Vendor for I-Code " + masterno;
                        if (string.IsNullOrEmpty(exchange.validDlgResult.vendorid))
                        {
                            exchange.validDlgType          = ValidDlgTypes.PendingOutNonBc;
                            exchange.validDlg              = new ValidDlg();
                            exchange.validDlgResult        = new ValidDlgResults();
                            exchange.validDlg.title        = "Select Vendor...";
                            exchange.validDlg.message      = "Select a Vendor for I-Code " + masterno;
                            exchange.validDlg.dealid       = exchange.dealid;
                            exchange.validDlg.departmentid = exchange.departmentid;
                            exchange.validDlg.masterid     = isRentalICodeResponse.masterid;
                            exchange.validDlg.locationid   = user.primarylocationid;
                        }
                        else
                        {
                            frmExchangeItem.item.msg          = "";
                            frmExchangeItem.item.status       = TBCItem.StatusTypes.Found;
                            frmExchangeItem.item.barcode      = "";
                            frmExchangeItem.item.trackedby    = RwConstants.TrackedBy.QUANTITY;
                            frmExchangeItem.item.masterno     = exchange.validDlgResult.masterno;
                            frmExchangeItem.item.masterid     = isRentalICodeResponse.masterid;
                            frmExchangeItem.item.rentalitemid = "";
                            frmExchangeItem.item.orderid      = "";
                            frmExchangeItem.item.dealid       = exchange.validDlgResult.dealid;
                            frmExchangeItem.item.deptid       = exchange.validDlgResult.departmentid;
                            frmExchangeItem.item.warehouseid  = exchange.validDlgResult.warehouseid;
                            frmExchangeItem.item.whcode       = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "warehouse", "warehouseid", frmExchangeItem.item.warehouseid, "whcode");
                            frmExchangeItem.item.ordertranid  = 0;
                            frmExchangeItem.item.internalchar = "";
                            frmExchangeItem.item.masteritemid = "";
                            frmExchangeItem.item.description  = exchange.validDlgResult.description;
                            frmExchangeItem.item.orderno      = "";
                            frmExchangeItem.item.qty          = 0;
                            frmExchangeItem.item.pending      = false;
                            frmExchangeItem.item.vendorid     = exchange.validDlgResult.vendorid;
                            frmExchangeItem.item.vendor       = exchange.validDlgResult.vendor;
                            frmExchangeItem.item.poid         = exchange.validDlgResult.poid;
                            frmExchangeItem.item.pono         = exchange.validDlgResult.pono;
                            exchange.validDlgType             = ValidDlgTypes.None;
                            exchange.validDlg                 = new ValidDlg();
                            exchange.validDlgResult           = new ValidDlgResults();
                        }
                        break;
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task GetPendingInQuantityItemInfoAsync(FwSqlConnection conn, string itemstatus, ExchangeModel exchange, UserContext user, IsRentalICodeResponse isRentalICodeResponse)
        {
            ExchangeItem frmExchangeItem;
            if (itemstatus == RwConstants.ItemStatus.IN)
            {
                frmExchangeItem = exchange.frmInExchangeItem;
            }
            else //if (request.itemstatus == RwConstants.ItemStatus.OUT)
            {
                frmExchangeItem = exchange.frmOutExchangeItem;
            }
            string masterno = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "master", "masterid", isRentalICodeResponse.masterid, "masterno");
            string master   = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "master", "masterid", isRentalICodeResponse.masterid, "master");
            frmExchangeItem.item.status  = TBCItem.StatusTypes.Error;
            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                qry.Add("select *");
                qry.Add("from exchangependinginnonbcview with(nolock)");
                qry.Add("where masterid = @masterid");
                if (!user.hasquiklocate)
                {
                    qry.Add("  and locationid = @locationid");
                    qry.AddParameter("@locationid", user.primarylocationid);
                }
                qry.Add("  and dealid = @dealid");
                qry.Add("  and inventorydepartmentid = @usersprimaryinventorydepartmentid"); //jh 12/09/09 CAS-6696-YZVS 
                qry.AddParameter("@masterid", isRentalICodeResponse.masterid);
                qry.AddParameter("@dealid", exchange.dealid);
                qry.AddParameter("@usersprimaryinventorydepartmentid", user.rentalinventorydepartmentid);
                await qry.ExecuteAsync();
                switch(qry.RowCount)
                {
                    case 0:
                        frmExchangeItem.item.msg = "I-Code " + masterno + " is not OUT on this Deal.";
                        break;
                    case 1:
                        frmExchangeItem.item.status       = TBCItem.StatusTypes.Found;
                        frmExchangeItem.item.barcode      = "";
                        frmExchangeItem.item.trackedby    = RwConstants.TrackedBy.QUANTITY;
                        frmExchangeItem.item.masterno     = qry.GetField("masterno").ToString().TrimEnd();
                        frmExchangeItem.item.masterid     = isRentalICodeResponse.masterid;
                        frmExchangeItem.item.rentalitemid = "";
                        frmExchangeItem.item.orderid      = qry.GetField("orderid").ToString().TrimEnd();
                        frmExchangeItem.item.dealid       = qry.GetField("dealid").ToString().TrimEnd();
                        frmExchangeItem.item.deptid       = qry.GetField("departmentid").ToString().TrimEnd();
                        frmExchangeItem.item.warehouseid  = qry.GetField("warehouseid").ToString().TrimEnd();
                        frmExchangeItem.item.whcode       = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "warehouse", "warehouseid", frmExchangeItem.item.warehouseid, "whcode");
                        frmExchangeItem.item.ordertranid  = 0;
                        frmExchangeItem.item.internalchar = "";
                        frmExchangeItem.item.masteritemid = qry.GetField("masteritemid").ToString().TrimEnd();
                        frmExchangeItem.item.description  = qry.GetField("description").ToString().TrimEnd();
                        frmExchangeItem.item.orderno      = "";
                        frmExchangeItem.item.qty          = 0;
                        frmExchangeItem.item.pending      = false;
                        frmExchangeItem.item.vendorid     = qry.GetField("vendorid").ToString().TrimEnd();
                        frmExchangeItem.item.vendor       = qry.GetField("vendor").ToString().TrimEnd();
                        frmExchangeItem.item.poid         = qry.GetField("poid").ToString().TrimEnd();
                        frmExchangeItem.item.pono         = qry.GetField("pono").ToString().TrimEnd();
                        break;
                    default:
                        if (string.IsNullOrWhiteSpace(exchange.validDlgResult.orderid))
                        {
                            //frmExchangeItem.item.msg              = "Select an Order for I-Code " + masterno;
                            exchange.validDlgType          = ValidDlgTypes.PendingInNonBc;
                            exchange.validDlg              = new ValidDlg();
                            exchange.validDlgResult        = new ValidDlgResults();
                            exchange.validDlg.title        = "Select Order...";
                            exchange.validDlg.message      = "Select an Order for I-Code " + masterno;
                            exchange.validDlg.dealid       = exchange.dealid;
                            exchange.validDlg.departmentid = exchange.departmentid;
                            exchange.validDlg.masterid     = isRentalICodeResponse.masterid;
                            if (!user.hasquiklocate)
                            {
                                exchange.validDlg.locationid = user.primarylocationid;
                            }
                        }
                        else
                        {
                            frmExchangeItem.item.msg          = "";
                            frmExchangeItem.item.status       = TBCItem.StatusTypes.Found;
                            frmExchangeItem.item.barcode      = "";
                            frmExchangeItem.item.trackedby    = RwConstants.TrackedBy.QUANTITY;
                            frmExchangeItem.item.masterno     = exchange.validDlgResult.masterno;
                            frmExchangeItem.item.masterid     = isRentalICodeResponse.masterid;
                            frmExchangeItem.item.rentalitemid = "";
                            frmExchangeItem.item.orderid      = exchange.validDlgResult.orderid;
                            frmExchangeItem.item.dealid       = exchange.validDlgResult.dealid;
                            frmExchangeItem.item.deptid       = exchange.validDlgResult.departmentid;
                            frmExchangeItem.item.warehouseid  = exchange.validDlgResult.warehouseid;
                            frmExchangeItem.item.whcode       = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "warehouse", "warehouseid", frmExchangeItem.item.warehouseid, "whcode");
                            frmExchangeItem.item.ordertranid  = 0;
                            frmExchangeItem.item.internalchar = "";
                            frmExchangeItem.item.masteritemid = exchange.validDlgResult.masteritemid;
                            frmExchangeItem.item.description  = exchange.validDlgResult.description;
                            frmExchangeItem.item.orderno      = "";
                            frmExchangeItem.item.qty          = 0;
                            frmExchangeItem.item.pending      = false;
                            frmExchangeItem.item.vendorid     = exchange.validDlgResult.vendorid;
                            frmExchangeItem.item.vendor       = exchange.validDlgResult.vendor;
                            frmExchangeItem.item.poid         = exchange.validDlgResult.poid;
                            frmExchangeItem.item.pono         = exchange.validDlgResult.pono;
                            exchange.validDlgType             = ValidDlgTypes.None;
                            exchange.validDlg                 = new ValidDlg();
                            exchange.validDlgResult           = new ValidDlgResults();
                        }
                        break;
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task GetInOutQuantityItemInfoAsync(FwSqlConnection conn, string itemstatus, ExchangeModel exchange, UserContext user, IsRentalICodeResponse isRentalICodeResponse)
        {
            ExchangeItem frmExchangeItem;
            if (itemstatus == RwConstants.ItemStatus.IN)
            {
                frmExchangeItem = exchange.frmInExchangeItem;
            }
            else //if (request.itemstatus == RwConstants.ItemStatus.OUT)
            {
                frmExchangeItem = exchange.frmOutExchangeItem;
            }
            int count;
            string masterno, master, trackedby, newmasteritemid;
            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                qry.Add("select masterno, master, trackedby");
                qry.Add("from master with (nolock)");
                qry.Add("where masterid = @masterid");
                qry.AddParameter("@masterid", isRentalICodeResponse.masterid);
                await qry.ExecuteAsync();
                masterno  = qry.GetField("masterno").ToString().TrimEnd();
                master    = qry.GetField("master").ToString().TrimEnd();
                trackedby = qry.GetField("trackedby").ToString().TrimEnd();
            }
            frmExchangeItem.item.status = TBCItem.StatusTypes.Error;
            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                qry.Add("select *");
                qry.Add("from  exchangenonbcview with(nolock)");
                qry.Add("where masterid = @masterid");
                qry.AddParameter("@masterid", isRentalICodeResponse.masterid);
                if (!user.hasquiklocate || itemstatus == RwConstants.ItemStatus.OUT)
                {
                    qry.Add("  and locationid = @locationid");
                    qry.AddParameter("@locationid", user.primarylocationid);
                }
                qry.Add(" and dealid = @dealid");
                qry.AddParameter("@dealid", exchange.dealid);
                if (!string.IsNullOrEmpty(exchange.orderid))
                {
                   qry.Add("  and orderid = @orderid");
                }
                if (itemstatus == RwConstants.ItemStatus.OUT)
                {
                    if (!string.IsNullOrEmpty(exchange.frmInExchangeItem.item.orderid))
                    {
                        qry.Add("  and orderid = @initemorderid");
                        qry.AddParameter("@initemorderid", exchange.frmInExchangeItem.item.orderid);
                    }
                }
                else
                {
                   qry.Add("  and qtyout > 0");
                }
                if (trackedby == RwConstants.TrackedBy.BARCODE || trackedby == RwConstants.TrackedBy.SERIALNO)
                {
                   qry.Add("  and vendorid > ''");
                }
                qry.Add("  and inventorydepartmentid = @usersprimaryinventorydepartmentid"); //jh 12/09/09 CAS-6696-YZVS
                qry.AddParameter("@usersprimaryinventorydepartmentid", user.rentalinventorydepartmentid);
                await qry.ExecuteAsync();
                count = qry.RowCount;
                if (count == 1 && (trackedby == RwConstants.TrackedBy.BARCODE || trackedby == RwConstants.TrackedBy.SERIALNO || trackedby == RwConstants.TrackedBy.RFID))
                {
                   count = 100; // jh - to force the scroller to open
                }
                switch(count)
                {
                    case 0:
                        if (itemstatus == RwConstants.ItemStatus.IN)
                        {
                            if (string.IsNullOrEmpty(exchange.orderid))
                            {
                                frmExchangeItem.item.msg = "I-Code " + masterno + " is not OUT on this Deal.";
                            }
                            else
                            {
                                frmExchangeItem.item.msg = "I-Code " + masterno + " is not OUT on this Order.";
                            }
                        }
                        else  // (itemstatus == RwConstants.ItemStatus.OUT)
                        {
                           if (isRentalICodeResponse.masterid == exchange.frmInExchangeItem.item.masterid)
                           {
                                if (!exchange.allowcrosswarehouse)
                                {
                                    //msgYesNo('Perform cross-Warehouse Exchange?')
                                    exchange.response.confirmallowcrosswarehouse = true;
                                }
                                else  //Perform cross-Warehouse Exchange
                                {
                                    frmExchangeItem.item.status  = TBCItem.StatusTypes.Found;
                                    newmasteritemid = await splitmasteritemAsync(conn: conn,
                                                                                 orderid: exchange.frmInExchangeItem.item.orderid,
                                                                                 masteritemid: exchange.frmInExchangeItem.item.masteritemid,
                                                                                 splitqty: 0, 
                                                                                 splitsubqty: 0);
                                    frmExchangeItem.item = CopyBCItem(exchange.frmInExchangeItem.item);
                                    frmExchangeItem.item.masteritemid = newmasteritemid;
                                    using (FwSqlCommand cmd = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                                    {
                                        cmd.Add("update mi");
                                        cmd.Add("set warehouseid = @defuserwhid");
                                        cmd.Add("from masteritem mi");
                                        cmd.Add("where mi.orderid = @initemorderid");
                                        cmd.Add("  and mi.masteritemid = @initemmasteritemid");
                                        cmd.AddParameter("@defuserwhid", user.primarywarehouseid);
                                        cmd.AddParameter("@initemorderid", exchange.frmInExchangeItem.item.orderid);
                                        cmd.AddParameter("@initemmasteritemid", exchange.frmInExchangeItem.item.masteritemid);
                                        await cmd.ExecuteNonQueryAsync();
                                    }
                                    frmExchangeItem.item.warehouseid  = user.primarywarehouseid;
                                    frmExchangeItem.item.whcode       = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "warehouse", "warehouseid", frmExchangeItem.item.warehouseid, "whcode");
                                    frmExchangeItem.item.ordertranid  = 0;
                                    frmExchangeItem.item.internalchar = "";
                                    frmExchangeItem.item.qty          = 0;
                                }
                                //else
                                //{
                                //    tmpItem.msg = "Cross-Warehouse Exchange cancelled.";
                                //}
                            }
                            else
                            {
                                if (!exchange.allowcrossicode)
                                {
                                    //msgYesNo('Item being Checked-In (' + getData('master', 'masterid', TffExchange(owner).frmInExchangeItem.item.masterId, 'master') + ') is not the same I-Code as the item being Checked-Out (' + master + ').' + #13 + 'Proceed?')
                                    exchange.response.confirmallowcrossicode = true;
                                }
                                else
                                {
                                    frmExchangeItem.item.status       = TBCItem.StatusTypes.Found;
                                    newmasteritemid      = await insertmasteritemAsync(conn: conn,
                                                                                       orderid: exchange.frmInExchangeItem.item.orderid, 
                                                                                       masterid: isRentalICodeResponse.masterid, 
                                                                                       warehouseid: user.primarywarehouseid, 
                                                                                       qty: 0);
                                    await insertcheckoutauditAsync(conn: conn,
                                                                   orderid: exchange.frmInExchangeItem.item.orderid, 
                                                                   masteritemid: newmasteritemid,
                                                                   usersid: user.usersid, 
                                                                   priorqty: 0, 
                                                                   newqty: 0);
                                    using (FwSqlCommand qry2 = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
                                    {
                                        qry2.Add("update mi");
                                        qry2.Add(" set   price       = mi2.price,");
                                        qry2.Add("       discountpct = mi2.discountpct,");
                                        qry2.Add("       daysinwk    = mi2.daysinwk,");
                                        qry2.Add("       marginpct   = mi2.marginpct,");
                                        qry2.Add("       markuppct   = mi2.markuppct,");
                                        qry2.Add("       cost        = mi2.cost,");
                                        qry2.Add("       parentid    = mi2.parentid,");
                                        qry2.Add("       itemclass   = mi2.itemclass,");
                                        qry2.Add("       itemorder   = ''");
                                        qry2.Add("from  masteritem mi, masteritem mi2");
                                        qry2.Add("where mi.orderid       = @initemorderid");
                                        qry2.Add("  and mi.masteritemid  = @newmasteritemid");
                                        qry2.Add("  and mi2.orderid      = @initemorderid");
                                        qry2.Add("  and mi2.masteritemid = @initemmasteritemid");
                                        await qry2.ExecuteNonQueryAsync();
                                    }
                                    frmExchangeItem.item              = CopyBCItem(exchange.frmInExchangeItem.item);
                                    frmExchangeItem.item.masteritemid = newmasteritemid;
                                    frmExchangeItem.item.masterid     = isRentalICodeResponse.masterid;
                                    frmExchangeItem.item.warehouseid  = user.primarywarehouseid;
                                    frmExchangeItem.item.whcode       = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "warehouse", "warehouseid", frmExchangeItem.item.warehouseid, "whcode");
                                    frmExchangeItem.item.ordertranid  = 0;
                                    frmExchangeItem.item.internalchar = "";
                                    frmExchangeItem.item.qty          = 0;
                                    //TffExchange(owner).allowCrossIcode = true;
                                }
                                //else // this case maybe should be implemented on the front end
                                //{
                                //    tmpItem.msg = "Cross I-Code Exchange cancelled.";
                                //    tmpItem.resetall = true;
                                //}
                            }
                        }
                        break;
                    case 1:
                        frmExchangeItem.item.status       = TBCItem.StatusTypes.Found;
                        frmExchangeItem.item.barcode      = "";
                        frmExchangeItem.item.trackedby    = RwConstants.TrackedBy.QUANTITY;
                        frmExchangeItem.item.masterno     = qry.GetField("masterno").ToString().TrimEnd();
                        frmExchangeItem.item.masterid     = isRentalICodeResponse.masterid;
                        frmExchangeItem.item.rentalitemid = "";
                        frmExchangeItem.item.orderid      = qry.GetField("orderid").ToString().TrimEnd();
                        frmExchangeItem.item.dealid       = qry.GetField("dealid").ToString().TrimEnd();
                        frmExchangeItem.item.deptid       = qry.GetField("departmentid").ToString().TrimEnd();
                        frmExchangeItem.item.warehouseid  = qry.GetField("warehouseid").ToString().TrimEnd();
                        frmExchangeItem.item.whcode       = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "warehouse", "warehouseid", frmExchangeItem.item.warehouseid, "whcode");
                        frmExchangeItem.item.ordertranid  = 0;
                        frmExchangeItem.item.internalchar = "";
                        frmExchangeItem.item.masteritemid = qry.GetField("masteritemid").ToString().TrimEnd();
                        frmExchangeItem.item.description  = qry.GetField("description").ToString().TrimEnd();
                        frmExchangeItem.item.orderno      = qry.GetField("orderno").ToString().TrimEnd();
                        frmExchangeItem.item.qty          = 0;
                        frmExchangeItem.item.pending      = false;
                        frmExchangeItem.item.vendorid     = qry.GetField("vendorid").ToString().TrimEnd();
                        frmExchangeItem.item.vendor       = qry.GetField("vendor").ToString().TrimEnd();
                        frmExchangeItem.item.poid         = qry.GetField("poid").ToString().TrimEnd();
                        frmExchangeItem.item.pono         = qry.GetField("pono").ToString().TrimEnd();
                        break;
                    default:
                        if (string.IsNullOrEmpty(exchange.validDlgResult.orderid))
                        {
                            //frmExchangeItem.item.msg = "Select an Order for I-Code " + masterno;
                            if (itemstatus == RwConstants.ItemStatus.IN)
                            {
                                exchange.validDlgType          = ValidDlgTypes.InNonBC;
                                exchange.validDlg              = new ValidDlg();
                                exchange.validDlgResult        = new ValidDlgResults();
                                exchange.validDlg.title        = "Select Order...";
                                exchange.validDlg.message      = "Select an Order for I-Code " + masterno;
                                exchange.validDlg.dealid       = exchange.dealid;
                                exchange.validDlg.departmentid = exchange.departmentid;
                                exchange.validDlg.orderid      = exchange.orderid;
                                exchange.validDlg.masterid     = isRentalICodeResponse.masterid;
                                if (!user.hasquiklocate)
                                {
                                    exchange.validDlg.locationid = user.primarylocationid;
                                }
                            }
                            else
                            {
                                exchange.validDlgType          = ValidDlgTypes.OutNonBc;
                                exchange.validDlg              = new ValidDlg();
                                exchange.validDlgResult        = new ValidDlgResults();
                                exchange.validDlg.title        = "Select Order...";
                                exchange.validDlg.message      = "Select an Order for I-Code " + masterno;
                                exchange.validDlg.dealid       = exchange.dealid;
                                exchange.validDlg.departmentid = exchange.departmentid;
                                exchange.validDlg.masterid     = isRentalICodeResponse.masterid;
                                exchange.validDlg.locationid   = user.primarylocationid;
                                if (!string.IsNullOrEmpty(exchange.frmInExchangeItem.item.orderid))
                                {
                                    exchange.validDlg.orderid = exchange.frmInExchangeItem.item.orderid;
                                }
                                if (trackedby == RwConstants.TrackedBy.BARCODE || trackedby == RwConstants.TrackedBy.SERIALNO || trackedby == RwConstants.TrackedBy.RFID)
                                {
                                    exchange.validDlg.vendoronly = true;
                                }
                            }
                        }
                        else
                        {
                            frmExchangeItem.item.msg          = "";
                            frmExchangeItem.item.status       = TBCItem.StatusTypes.Found;
                            frmExchangeItem.item.barcode      = "";
                            frmExchangeItem.item.trackedby    = RwConstants.TrackedBy.QUANTITY;
                            frmExchangeItem.item.masterno     = exchange.validDlgResult.masterno;
                            frmExchangeItem.item.masterid     = isRentalICodeResponse.masterid;
                            frmExchangeItem.item.rentalitemid = "";
                            frmExchangeItem.item.orderid      = exchange.validDlgResult.orderid;
                            frmExchangeItem.item.dealid       = exchange.validDlgResult.dealid;
                            frmExchangeItem.item.deptid       = exchange.validDlgResult.departmentid;
                            frmExchangeItem.item.warehouseid  = exchange.validDlgResult.warehouseid;
                            frmExchangeItem.item.whcode       = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "warehouse", "warehouseid", frmExchangeItem.item.warehouseid, "whcode");
                            frmExchangeItem.item.ordertranid  = 0;
                            frmExchangeItem.item.internalchar = "";
                            frmExchangeItem.item.masteritemid = exchange.validDlgResult.masteritemid;
                            frmExchangeItem.item.description  = exchange.validDlgResult.description;
                            frmExchangeItem.item.orderno      = exchange.validDlgResult.orderno;
                            frmExchangeItem.item.qty          = 0;;
                            frmExchangeItem.item.pending      = false;
                            frmExchangeItem.item.vendorid     = exchange.validDlgResult.vendorid;
                            frmExchangeItem.item.vendor       = exchange.validDlgResult.vendor;
                            frmExchangeItem.item.poid         = exchange.validDlgResult.poid;
                            frmExchangeItem.item.pono         = exchange.validDlgResult.pono;
                            exchange.validDlgType             = ValidDlgTypes.None;
                            exchange.validDlg                 = new ValidDlg();
                            exchange.validDlgResult           = new ValidDlgResults();
                        }
                        break;
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task GetSerialItemInfoAsync(FwSqlConnection conn, string itemstatus, ExchangeModel exchange, UserContext user, string serialno, IsSerialNoResponse isSerialNoResponse)
        {
            if (itemstatus == RwConstants.ItemStatus.OUT && string.IsNullOrEmpty(exchange.frmInExchangeItem.barcode))
            {
               await GetPendingOutSerialItemInfoAsync(conn: conn,
                                                      itemstatus: itemstatus,
                                                      exchange: exchange,
                                                      user: user,
                                                      serialno: serialno,
                                                      isSerialNoResponse: isSerialNoResponse);
            }
            else if (itemstatus == RwConstants.ItemStatus.IN)
            {
               await GetInSerialItemInfoAsync(conn: conn,
                                              itemstatus: itemstatus,
                                              exchange: exchange,
                                              user: user,
                                              serialno: serialno,
                                              isSerialNoResponse: isSerialNoResponse);
            }
            else if (itemstatus == RwConstants.ItemStatus.OUT)
            {
               await GetOutSerialItemInfoAsync(conn,
                                               itemstatus: itemstatus,
                                               exchange: exchange,
                                               user: user,
                                               serialno: serialno,
                                               isSerialNoResponse: isSerialNoResponse);
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task GetPendingOutSerialItemInfoAsync(FwSqlConnection conn, string itemstatus, ExchangeModel exchange, UserContext user, string serialno, IsSerialNoResponse isSerialNoResponse)
        {
            ExchangeItem frmExchangeItem;
            if (itemstatus == RwConstants.ItemStatus.IN)
            {
                frmExchangeItem = exchange.frmInExchangeItem;
            }
            else //if (request.itemstatus == RwConstants.ItemStatus.OUT)
            {
                frmExchangeItem = exchange.frmOutExchangeItem;
            }
            //validDlg: TfsExchangePendingOutSerial;
            frmExchangeItem.item.status = TBCItem.StatusTypes.Error;
            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                qry.Add("select *");
                qry.Add("from  exchangependingoutserialview with(nolock)");
                qry.Add("where serialno   = @serialno");
                qry.Add("  and locationid = @defuserlocid");
                qry.Add("  and dealid     = @dealid");
                qry.Add("  and inventorydepartmentid = @usersprimaryinventorydepartmentid"); //jh 12/09/09 CAS-6696-YZVS
                await qry.ExecuteAsync();
                switch(qry.RowCount)
                {
                    case 0:
                        frmExchangeItem.item.msg = "Serial Item " + serialno + " cannot be checked out Pending on this Deal.";
                        break;
                    case 1:
                        frmExchangeItem.item.status       = TBCItem.StatusTypes.Found;
                        frmExchangeItem.item.barcode      = "";
                        frmExchangeItem.item.serialno     = serialno;
                        frmExchangeItem.item.trackedby    = RwConstants.TrackedBy.SERIALNO;
                        frmExchangeItem.item.masterno     = qry.GetField("masterno").ToString().TrimEnd();
                        frmExchangeItem.item.masterid     = qry.GetField("masterid").ToString().TrimEnd();
                        frmExchangeItem.item.rentalitemid = qry.GetField("rentalitemid").ToString().TrimEnd();
                        frmExchangeItem.item.orderid      = "";
                        frmExchangeItem.item.dealid       = "";
                        frmExchangeItem.item.deptid       = "";
                        frmExchangeItem.item.warehouseid  = qry.GetField("warehouseid").ToString().TrimEnd();
                        frmExchangeItem.item.whcode       = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "warehouse", "warehouseid", frmExchangeItem.item.warehouseid, "whcode");
                        frmExchangeItem.item.ordertranid  = 0;
                        frmExchangeItem.item.internalchar = "";
                        frmExchangeItem.item.masteritemid = "";
                        frmExchangeItem.item.description  = qry.GetField("description").ToString().TrimEnd();
                        frmExchangeItem.item.orderno      = "";
                        frmExchangeItem.item.qty          = 1;
                        frmExchangeItem.item.pending      = false;
                        frmExchangeItem.item.vendorid     = qry.GetField("vendorid").ToString().TrimEnd();
                        frmExchangeItem.item.vendor       = qry.GetField("vendor").ToString().TrimEnd();
                        frmExchangeItem.item.poid         = qry.GetField("poid").ToString().TrimEnd();
                        frmExchangeItem.item.pono         = qry.GetField("pono").ToString().TrimEnd();
                        break;
                    default:
                        if (string.IsNullOrEmpty(exchange.validDlgResult.masterid))
                        {
                            //frmExchangeItem.item.msg       = "Select an I-Code for Serial No. " + serialno;
                            exchange.validDlgType          = ValidDlgTypes.PendingOutSerial;
                            exchange.validDlg              = new ValidDlg();
                            exchange.validDlgResult        = new ValidDlgResults();
                            exchange.validDlg.title        = "Select I-Code...";
                            exchange.validDlg.message      = "Select an I-Code for Serial No. " + serialno;
                            exchange.validDlg.dealid       = exchange.dealid;
                            exchange.validDlg.departmentid = exchange.departmentid;
                            exchange.validDlg.serialno     = serialno;
                            exchange.validDlg.locationid   = user.primarylocationid;
                        }
                        else
                        {
                            frmExchangeItem.item.msg          = "";
                            frmExchangeItem.item.status       = TBCItem.StatusTypes.Found;
                            frmExchangeItem.item.barcode      = "";
                            frmExchangeItem.item.serialno     = serialno;
                            frmExchangeItem.item.trackedby    = RwConstants.TrackedBy.SERIALNO;
                            frmExchangeItem.item.masterno     = exchange.validDlgResult.masterno;
                            frmExchangeItem.item.masterid     = exchange.validDlgResult.masterid;
                            frmExchangeItem.item.rentalitemid = exchange.validDlgResult.rentalitemid;
                            frmExchangeItem.item.orderid      = "";
                            frmExchangeItem.item.dealid       = "";
                            frmExchangeItem.item.deptid       = "";
                            frmExchangeItem.item.warehouseid  = exchange.validDlgResult.warehouseid;
                            frmExchangeItem.item.whcode       = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "warehouse", "warehouseid", frmExchangeItem.item.warehouseid, "whcode");
                            frmExchangeItem.item.ordertranid  = 0;
                            frmExchangeItem.item.internalchar = "";
                            frmExchangeItem.item.masteritemid = "";
                            frmExchangeItem.item.description  = exchange.validDlgResult.description;
                            frmExchangeItem.item.orderno      = "";
                            frmExchangeItem.item.qty          = 1;
                            //TffExchange(owner).edtQty.text          := '0';//'1';
                            frmExchangeItem.item.pending      = false;
                            frmExchangeItem.item.vendorid     = exchange.validDlgResult.vendorid;
                            frmExchangeItem.item.vendor       = exchange.validDlgResult.vendor;
                            frmExchangeItem.item.poid         = exchange.validDlgResult.poid;
                            frmExchangeItem.item.pono         = exchange.validDlgResult.pono;
                            exchange.validDlgType             = ValidDlgTypes.None;
                            exchange.validDlg                 = new ValidDlg();
                            exchange.validDlgResult           = new ValidDlgResults();
                        }
                        break;
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task GetInSerialItemInfoAsync(FwSqlConnection conn, string itemstatus, ExchangeModel exchange, UserContext user, string serialno, IsSerialNoResponse isSerialNoResponse)
        {
            ExchangeItem frmExchangeItem;
            if (itemstatus == RwConstants.ItemStatus.IN)
            {
                frmExchangeItem = exchange.frmInExchangeItem;
            }
            else //if (request.itemstatus == RwConstants.ItemStatus.OUT)
            {
                frmExchangeItem = exchange.frmOutExchangeItem;
            }
            //validDlg: TfsExchangeInSerial;
            frmExchangeItem.item.status = TBCItem.StatusTypes.Error;
            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                qry.Add("select *");
                qry.Add("from exchangeserialview with (nolock)");
                qry.Add("where serialno = @serialno");
                qry.AddParameter("@serialno", serialno);
                if (!user.hasquiklocate)
                {
                    qry.Add("  and locationid = @defuserlocid");
                    qry.AddParameter("@defuserlocid", user.primarylocationid);
                }
                if (!string.IsNullOrEmpty(exchange.dealid))
                {
                    qry.Add("  and dealid = @dealid");
                    qry.AddParameter("@dealid", exchange.dealid);
                }
                if (!string.IsNullOrEmpty(exchange.orderid))
                {
                    qry.Add("  and orderid = @orderid");
                    qry.AddParameter("@orderid", exchange.orderid);
                }
                qry.Add("inventorydepartmentid = @usersprimarydepartmentid"); //jh 12/09/09 CAS-6696-YZVS
                qry.AddParameter("@usersprimarydepartmentid", user.rentalinventorydepartmentid);
                await qry.ExecuteAsync();
                switch(qry.RowCount)
                {
                    case 0:
                        if (string.IsNullOrEmpty(exchange.orderid))
                        {
                           frmExchangeItem.item.msg = "Serial Item " + serialno + " is not OUT on this Deal.";
                        }
                        else
                        {
                           frmExchangeItem.item.msg = "Serial Item " + serialno + " is not OUT on this Order.";
                        }
                        break;
                    case 1:
                        frmExchangeItem.item.status       = TBCItem.StatusTypes.Found;
                        frmExchangeItem.item.barcode      = "";
                        frmExchangeItem.item.serialno     = serialno;
                        frmExchangeItem.item.trackedby    = RwConstants.TrackedBy.SERIALNO;
                        frmExchangeItem.item.masterno     = qry.GetField("masterno").ToString().TrimEnd();
                        frmExchangeItem.item.masterid     = qry.GetField("masterid").ToString().TrimEnd();
                        frmExchangeItem.item.rentalitemid = qry.GetField("rentalitemid").ToString().TrimEnd();
                        frmExchangeItem.item.orderid      = qry.GetField("orderid").ToString().TrimEnd();
                        frmExchangeItem.item.dealid       = qry.GetField("dealid").ToString().TrimEnd();
                        frmExchangeItem.item.deptid       = qry.GetField("departmentid").ToString().TrimEnd();
                        frmExchangeItem.item.warehouseid  = qry.GetField("warehouseid").ToString().TrimEnd();
                        frmExchangeItem.item.whcode       = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "warehouse", "warehouseid", frmExchangeItem.item.warehouseid, "whcode");
                        frmExchangeItem.item.ordertranid  = qry.GetField("ordertranid").ToInt32();
                        frmExchangeItem.item.internalchar = qry.GetField("internalchar").ToString().TrimEnd();
                        frmExchangeItem.item.masteritemid = qry.GetField("masteritemid").ToString().TrimEnd();
                        frmExchangeItem.item.description  = qry.GetField("description").ToString().TrimEnd();
                        frmExchangeItem.item.orderno      = qry.GetField("orderno").ToString().TrimEnd();
                        frmExchangeItem.item.qty          = 1;
                        //TffExchange(owner).edtQty.text          := '0';//'1';
                        frmExchangeItem.item.pending      = false;
                        frmExchangeItem.item.vendorid     = qry.GetField("vendorid").ToString().TrimEnd();
                        frmExchangeItem.item.vendor       = qry.GetField("vendor").ToString().TrimEnd();
                        frmExchangeItem.item.poid         = qry.GetField("poid").ToString().TrimEnd();
                        frmExchangeItem.item.pono         = qry.GetField("pono").ToString().TrimEnd();
                        frmExchangeItem.item.availfromdatetime = qry.GetField("availfromdatetime").ToString().TrimEnd();
                        frmExchangeItem.item.availtodatetime   = qry.GetField("availtodatetime").ToString().TrimEnd();
                        frmExchangeItem.item.pendingrepairid   = qry.GetField("pendingrepairid").ToString().TrimEnd();
                        break;
                    default:
                        if (string.IsNullOrEmpty(exchange.validDlgResult.masterid))
                        {
                            //frmExchangeItem.item.msg       = "Select an I-Code for Serial No. " + serialno;
                            exchange.validDlgType          = ValidDlgTypes.InSerial;
                            exchange.validDlg              = new ValidDlg();
                            exchange.validDlgResult        = new ValidDlgResults();
                            exchange.validDlg.title        = "Select I-Code...";
                            exchange.validDlg.message      = "Select an I-Code for Serial No. " + serialno;
                            exchange.validDlg.dealid       = exchange.dealid;
                            exchange.validDlg.departmentid = exchange.departmentid;
                            exchange.validDlg.serialno     = serialno;
                            if(!user.hasquiklocate)
                            {
                                exchange.validDlg.locationid   = user.primarylocationid;
                            };
                        }
                        else
                        {
                            frmExchangeItem.item.msg               = "";
                            frmExchangeItem.item.status            = TBCItem.StatusTypes.Found;
                            frmExchangeItem.item.barcode           = "";
                            frmExchangeItem.item.serialno          = serialno;
                            frmExchangeItem.item.trackedby         = RwConstants.TrackedBy.SERIALNO;
                            frmExchangeItem.item.masterno          = exchange.validDlgResult.masterno;
                            frmExchangeItem.item.masterid          = exchange.validDlgResult.masterid;
                            frmExchangeItem.item.rentalitemid      = exchange.validDlgResult.rentalitemid;
                            frmExchangeItem.item.orderid           = exchange.validDlgResult.orderid;
                            frmExchangeItem.item.dealid            = exchange.validDlgResult.dealid;
                            frmExchangeItem.item.deptid            = exchange.validDlgResult.departmentid;
                            frmExchangeItem.item.warehouseid       = exchange.validDlgResult.warehouseid;
                            frmExchangeItem.item.whcode            = await FwSqlCommand.GetStringDataAsync(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout, "warehouse", "warehouseid", frmExchangeItem.item.warehouseid, "whcode");
                            frmExchangeItem.item.ordertranid       = exchange.validDlgResult.ordertranid;
                            frmExchangeItem.item.internalchar      = exchange.validDlgResult.internalchar;
                            frmExchangeItem.item.masteritemid      = exchange.validDlgResult.masteritemid;
                            frmExchangeItem.item.description       = exchange.validDlgResult.description;
                            frmExchangeItem.item.orderno           = exchange.validDlgResult.orderno;
                            frmExchangeItem.item.qty               = 1;
                            //TffExchange(owner).edtQty.text          := '0';//'1';
                            frmExchangeItem.item.pending           = false;
                            frmExchangeItem.item.vendorid          = exchange.validDlgResult.vendorid;
                            frmExchangeItem.item.vendor            = exchange.validDlgResult.vendor;
                            frmExchangeItem.item.poid              = exchange.validDlgResult.poid;
                            frmExchangeItem.item.pono              = exchange.validDlgResult.pono;
                            frmExchangeItem.item.availfromdatetime = exchange.validDlgResult.availfromdatetime;
                            frmExchangeItem.item.availtodatetime   = exchange.validDlgResult.availtodatetime;
                            frmExchangeItem.item.pendingrepairid   = exchange.validDlgResult.pendingrepairid;
                            exchange.validDlgType             = ValidDlgTypes.None;
                            exchange.validDlg                 = new ValidDlg();
                            exchange.validDlgResult           = new ValidDlgResults();
                        }
                        break;
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task GetOutSerialItemInfoAsync(FwSqlConnection conn, string itemstatus, ExchangeModel exchange, UserContext user, string serialno, IsSerialNoResponse isSerialNoResponse)
        {
            ExchangeItem frmExchangeItem;
            if (itemstatus == RwConstants.ItemStatus.IN)
            {
                frmExchangeItem = exchange.frmInExchangeItem;
            }
            else //if (request.itemstatus == RwConstants.ItemStatus.OUT)
            {
                frmExchangeItem = exchange.frmOutExchangeItem;
            }
            frmExchangeItem.item.serialno = serialno;
            frmExchangeItem.item.status   = TBCItem.StatusTypes.Error;
            frmExchangeItem.item          = await getoutserialexchangeiteminfoAsync(conn: conn,
                                                                                    itemstatus: itemstatus,
                                                                                    exchange: exchange,
                                                                                    user: user,
                                                                                    serialno: serialno,
                                                                                    isSerialNoResponse: isSerialNoResponse);
            frmExchangeItem.showbtnremovefromcontainer = frmExchangeItem.item.itemincontainer;
        }
        //---------------------------------------------------------------------------------------------
        public async Task GetBarcodeItemInfoAsync(FwSqlConnection conn, string itemstatus, ExchangeModel exchange, UserContext user, string barcode, IsBarcodeResponse isBarcodeResponse)
        {
            if (itemstatus == RwConstants.ItemStatus.OUT && string.IsNullOrEmpty(exchange.frmInExchangeItem.barcode))
            {
               await GetPendingOutBarCodeItemInfoAsync(conn: conn,
                                                       itemstatus: itemstatus,
                                                       exchange: exchange,
                                                       user: user,
                                                       barcode: barcode,
                                                       isBarcodeResponse: isBarcodeResponse);
            }
            else if (itemstatus == RwConstants.ItemStatus.IN)
            {
               await GetInBarCodeItemInfoAsync(conn: conn,
                                               itemstatus: itemstatus,
                                               exchange: exchange,
                                               user: user,
                                               barcode: barcode,
                                               isBarcodeResponse: isBarcodeResponse);
            }
            else if (itemstatus == RwConstants.ItemStatus.OUT)
            {
               await GetOutBarCodeItemInfoAsync(conn: conn,
                                                itemstatus: itemstatus,
                                                exchange: exchange,
                                                user: user,
                                                barcode: barcode,
                                                isBarcodeResponse: isBarcodeResponse);
            }
        }
        //---------------------------------------------------------------------------------------------
        public string ConvertOrderModeToOrderType(string ordermode)
        {
            RwConstants.OrderModes mode = (RwConstants.OrderModes)Enum.Parse(typeof(RwConstants.OrderModes), ordermode);
            string ordertype = string.Empty;
            if (mode == RwConstants.OrderModes.Order)
            {
                ordertype = RwConstants.OrderType.ORDER;
            }
            else if (mode == RwConstants.OrderModes.Truck)
            {
                ordertype = RwConstants.OrderType.TRUCK;
            }
            else if (mode == RwConstants.OrderModes.Container)
            {
                ordertype = RwConstants.OrderType.CONTAINER;
            }
            else
            {
                throw new Exception("Invalid Order Mode.");
            }
            return ordertype;
        }
        //---------------------------------------------------------------------------------------------
        public async Task GetPendingOutBarCodeItemInfoAsync(FwSqlConnection conn, string itemstatus, ExchangeModel exchange, UserContext user, string barcode, IsBarcodeResponse isBarcodeResponse)
        {
            ExchangeItem frmExchangeItem;
            if (itemstatus == RwConstants.ItemStatus.IN)
            {
                frmExchangeItem = exchange.frmInExchangeItem;
            }
            else //if (request.itemstatus == RwConstants.ItemStatus.OUT)
            {
                frmExchangeItem = exchange.frmOutExchangeItem;
            }
            frmExchangeItem.item.barcode = barcode;
            frmExchangeItem.item.status  = TBCItem.StatusTypes.Error;
            frmExchangeItem.item         = await getpendingoutbarcodeexchangeiteminfoAsync(conn: conn,
                                                                                           barcode: barcode,
                                                                                           usersid: user.usersid,
                                                                                           dealid: exchange.dealid,
                                                                                           warehouseid: user.primarywarehouseid,
                                                                                           ordertype: ConvertOrderModeToOrderType(exchange.ordermode),
                                                                                           removefromcontainer: exchange.removingFromContainer);
            frmExchangeItem.showbtnremovefromcontainer = frmExchangeItem.item.itemincontainer;
        }
        //---------------------------------------------------------------------------------------------
        public async Task GetInBarCodeItemInfoAsync(FwSqlConnection conn, string itemstatus, ExchangeModel exchange, UserContext user, string barcode, IsBarcodeResponse isBarcodeResponse)
        {
            ExchangeItem frmExchangeItem;
            if (itemstatus == RwConstants.ItemStatus.IN)
            {
                frmExchangeItem = exchange.frmInExchangeItem;
            }
            else //if (request.itemstatus == RwConstants.ItemStatus.OUT)
            {
                frmExchangeItem = exchange.frmOutExchangeItem;
            }
            frmExchangeItem.item.status    = TBCItem.StatusTypes.Error;
            frmExchangeItem.item           = await getinbarcodeexchangeiteminfoAsync(conn: conn,
                                                                                     barcode: barcode,
                                                                                     orderid: exchange.orderid,
                                                                                     dealid: exchange.dealid,
                                                                                     departmentid: exchange.departmentid,
                                                                                     warehouseid: "", //jh 01/14/09 (to allow cross-warehouse exchange)
                                                                                     ordertype: ConvertOrderModeToOrderType(exchange.ordermode));
            if (exchange.completingpending && 
                frmExchangeItem.item.status == TBCItem.StatusTypes.Error && 
                frmExchangeItem.item.msg == "Item is Out Pending, select an Order to associate Out/In transaction with.")
            {
                if (string.IsNullOrEmpty(exchange.validDlgResult.orderid))
                {
                    exchange.validDlgType          = ValidDlgTypes.PendingInBC;
                    exchange.validDlg              = new ValidDlg();
                    exchange.validDlgResult        = new ValidDlgResults();
                    exchange.validDlg.title        = "Select Order...";
                    exchange.validDlg.message      = "Item is Out Pending, select an Order to associate Out/In transaction with.";
                    exchange.validDlg.dealid       = exchange.dealid;
                    exchange.validDlg.departmentid = exchange.departmentid;
                    exchange.validDlg.masterid     = frmExchangeItem.item.masterid;
                    if (!user.hasquiklocate)
                    {
                        exchange.validDlg.locationid   = user.primarylocationid;
                    }
                    
                }
                else
                {
                    frmExchangeItem.item.msg                 = "";
                    frmExchangeItem.item.status              = TBCItem.StatusTypes.Found;
                    frmExchangeItem.item.pendingorderid      = exchange.validDlgResult.orderid;
                    frmExchangeItem.item.pendingmasteritemid = exchange.validDlgResult.masteritemid;
                    exchange.validDlgType             = ValidDlgTypes.None;
                    exchange.validDlg                 = new ValidDlg();
                    exchange.validDlgResult           = new ValidDlgResults();
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        public async Task GetOutBarCodeItemInfoAsync(FwSqlConnection conn, string itemstatus, ExchangeModel exchange, UserContext user, string barcode, IsBarcodeResponse isBarcodeResponse)
        {
            ExchangeItem frmExchangeItem;
            if (itemstatus == RwConstants.ItemStatus.IN)
            {
                frmExchangeItem = exchange.frmInExchangeItem;
            }
            else //if (request.itemstatus == RwConstants.ItemStatus.OUT)
            {
                frmExchangeItem = exchange.frmOutExchangeItem;
            }
            frmExchangeItem.item.barcode = barcode;
            frmExchangeItem.item.status  = TBCItem.StatusTypes.Error;
            frmExchangeItem.item         = await getoutbarcodeexchangeiteminfoAsync(conn: conn,
                                                                                    barcode: barcode,
                                                                                    masterid: isBarcodeResponse.masterid,
                                                                                    warehouseid: user.primarywarehouseid,
                                                                                    ordertype: ConvertOrderModeToOrderType(exchange.ordermode),
                                                                                    usersid: user.usersid,
                                                                                    removefromcontainer: exchange.removingFromContainer,
                                                                                    availthrough: exchange.frmInExchangeItem.item.availtodatetime);
            frmExchangeItem.showbtnremovefromcontainer = frmExchangeItem.item.itemincontainer;
        }
        //---------------------------------------------------------------------------------------------
        public void resetall(ExchangeModel exchange)
        {
            exchange.allowcrossicode = false;
            exchange.allowcrosswarehouse = false;
            clearinitem(exchange);
            clearoutitem(exchange);
        }
        //---------------------------------------------------------------------------------------------
        public void clearinitem(ExchangeModel exchange)
        {
            exchange.frmInExchangeItem = new ExchangeModels.ExchangeItem();
            exchange.frmInExchangeItem.clearitem = true;
        }
        //---------------------------------------------------------------------------------------------
        public void clearoutitem(ExchangeModel exchange)
        {
            exchange.frmOutExchangeItem = new ExchangeModels.ExchangeItem();
            exchange.frmOutExchangeItem.clearitem = true;
        }
        //---------------------------------------------------------------------------------------------
    }
}
