using FwStandard.Models;
using FwStandard.SqlServer;
using RentalWorksQuikScan.Modules.ExchangeModels;
using System.Threading.Tasks;

namespace RentalWorksQuikScan.Modules
{
    public class ExchangeValidDlg
    {
        FwApplicationConfig ApplicationConfig;
        //---------------------------------------------------------------------------------------------
        public ExchangeValidDlg(FwApplicationConfig applicationConfig)
        {
            this.ApplicationConfig = applicationConfig;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> InNonBCAsync(FwSqlConnection conn, int pageno, int pagesize, string searchmode, string searchvalue, ExchangeModel exchange, UserContext user)
        {
            FwJsonDataTable searchresults = null;
            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.PageNo   = pageno;
                select.PageSize = pagesize;
                select.Add("select *");
                select.Add("from exchangenonbcview with (nolock)");
                select.Parse();
                select.AddWhere("qtyout > 0");
                if (!string.IsNullOrEmpty(exchange.validDlg.locationid))
                {
                   select.AddWhere("locationid = @locationid");
                   select.AddParameter("@locationid", exchange.validDlg.locationid);
                }
                if (!string.IsNullOrEmpty(exchange.validDlg.dealid))
                {
                    select.AddWhere("dealid = @dealid");
                    select.AddParameter("@dealid", exchange.validDlg.dealid);
                }
                if (!string.IsNullOrEmpty(exchange.validDlg.orderid))
                {
                    select.AddWhere("orderid = @orderid");
                    select.AddParameter("@orderid", exchange.validDlg.orderid);
                }
                if (!string.IsNullOrEmpty(exchange.validDlg.departmentid))
                {
                    select.AddWhere("departmentid = @departmentid");
                    select.AddParameter("@departmentid", exchange.validDlg.departmentid);
                }
                if (!string.IsNullOrEmpty(exchange.validDlg.masterid))
                {
                    select.AddWhere("masterid = @masterid");
                    select.AddParameter("@masterid", exchange.validDlg.masterid);
                }
                select.AddOrderBy("description");
                searchresults = await qry.QueryToFwJsonTableAsync(select, true);
            }
            return searchresults;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> InSerialAsync(FwSqlConnection conn, int pageno, int pagesize, string searchmode, string searchvalue, ExchangeModel exchange, UserContext user)
        {
            FwJsonDataTable searchresults = null;
            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.PageNo   = pageno;
                select.PageSize = pagesize;
                select.Add("select *");
                select.Add("from exchangeserialview with (nolock) ");
                select.Parse();
                if (!string.IsNullOrEmpty(exchange.validDlg.locationid))
                {
                    select.AddWhere("locationid = @locationid");
                    select.AddParameter("@locationid", exchange.validDlg.locationid);
                }
                if (!string.IsNullOrEmpty(exchange.validDlg.dealid))
                {
                    select.AddWhere("dealid = @dealid");
                    select.AddParameter("@dealid", exchange.validDlg.dealid);
                }
                if (!string.IsNullOrEmpty(exchange.validDlg.departmentid))
                {
                    select.AddWhere("departmentid = @departmentid");
                    select.AddParameter("@departmentid", exchange.validDlg.departmentid);
                }
                if (!string.IsNullOrEmpty(exchange.validDlg.serialno))
                {
                    select.AddWhere("serialno = @serialno");
                    select.AddParameter("@serialno", exchange.validDlg.serialno);
                }
                select.AddOrderBy("description");
                searchresults = await qry.QueryToFwJsonTableAsync(select, true);
            }
            return searchresults;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> OutNonBcAsync(FwSqlConnection conn, int pageno, int pagesize, string searchmode, string searchvalue, ExchangeModel exchange, UserContext user)
        {
            FwJsonDataTable searchresults = null;
            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.PageNo   = pageno;
                select.PageSize = pagesize;
                select.Add("select *");
                select.Add("from exchangenonbcview with (nolock)");
                select.Parse();
                if (!string.IsNullOrEmpty(exchange.validDlg.locationid))
                {
                   select.AddWhere("locationid = @locationid");
                   select.AddParameter("@dealid", exchange.validDlg);
                }
                if (!string.IsNullOrEmpty(exchange.validDlg.dealid))
                {
                    select.AddWhere("dealid = @dealid");
                    select.AddParameter("@dealid", exchange.validDlg.dealid);
                }
                if (!string.IsNullOrEmpty(exchange.validDlg.departmentid))
                {
                    select.AddWhere("departmentid = @departmentid");
                    select.AddParameter("@departmentid", exchange.validDlg.departmentid);
                }
                if (!string.IsNullOrEmpty(exchange.validDlg.masterid))
                {
                    select.AddWhere("masterid = @masterid");
                    select.AddParameter("@masterid", exchange.validDlg.masterid);
                }
                if (!string.IsNullOrEmpty(exchange.validDlg.orderid))
                {
                    select.AddWhere("orderid = @orderid");
                    select.AddParameter("@orderid", exchange.validDlg.orderid);
                }
                if (exchange.validDlg.vendoronly)
                {
                    select.AddWhere("vendorid > ''");
                }
                if (!string.IsNullOrEmpty(exchange.frmOutExchangeItem.item.vendorid))
                {
                    select.AddWhere("vendorid = @vendorid");
                    select.AddParameter("@vendorid", exchange.frmOutExchangeItem.item.vendorid);
                }
                //select.AddWhere(getInventoryDepartmentFilter("inventorydepartmentid", getUsersPrimaryDepartmentid, hasWhere)); //jh 12/09/09 CAS-6696-YZVS
                select.AddOrderBy("description");
                searchresults = await qry.QueryToFwJsonTableAsync(select, true);
            }
            return searchresults;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> PendingInBCAsync(FwSqlConnection conn, int pageno, int pagesize, string searchmode, string searchvalue, ExchangeModel exchange, UserContext user)
        {
            FwJsonDataTable searchresults = null;
            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.PageNo   = pageno;
                select.PageSize = pagesize;
                select.Add("select *");
                select.Add("from exchangependinginbcview with (nolock) ");
                select.Parse();
                if (!string.IsNullOrEmpty(exchange.validDlg.locationid))
                {
                    select.AddWhere("locationid = @locationid");
                    select.AddParameter("@locationid", exchange.validDlg.locationid);
                }
                if (!string.IsNullOrEmpty(exchange.validDlg.dealid))
                {
                    select.AddWhere("dealid = @dealid");
                    select.AddParameter("@dealid", exchange.validDlg.dealid);
                }
                if (!string.IsNullOrEmpty(exchange.validDlg.departmentid))
                {
                    select.AddWhere("departmentid = @departmentid");
                    select.AddParameter("@departmentid", exchange.validDlg.departmentid);
                }
                if (!string.IsNullOrEmpty(exchange.validDlg.masterid))
                {
                    select.AddWhere("masterid = @masterid");
                    select.AddParameter("@masterid", exchange.validDlg.masterid);
                }
                //select.AddWhere(getInventoryDepartmentFilter("inventorydepartmentid", getUsersPrimaryDepartmentid, hasWhere)); //jh 12/09/09 CAS-6696-YZVS
                select.AddOrderBy("description");
                searchresults = await qry.QueryToFwJsonTableAsync(select, true);
            }
            return searchresults;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> PendingInNonBcAsync(FwSqlConnection conn, int pageno, int pagesize, string searchmode, string searchvalue, ExchangeModel exchange, UserContext user)
        {
            FwJsonDataTable searchresults = null;
            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.PageNo   = pageno;
                select.PageSize = pagesize;
                select.Add("select *");
                select.Add("from exchangependinginnonbcview with (nolock) ");
                select.Parse();
                if (!string.IsNullOrEmpty(exchange.validDlg.locationid))
                {
                    select.AddWhere("locationid = @locationid");
                    select.AddParameter("@locationid", exchange.validDlg.locationid);
                }
                if (!string.IsNullOrEmpty(exchange.validDlg.dealid))
                {
                    select.AddWhere("dealid = @dealid");
                    select.AddParameter("@dealid", exchange.validDlg.dealid);
                }
                if (!string.IsNullOrEmpty(exchange.validDlg.departmentid))
                {
                    select.AddWhere("departmentid = @departmentid");
                    select.AddParameter("departmentid", exchange.validDlg.departmentid);
                }
                if (!string.IsNullOrEmpty(exchange.validDlg.masterid))
                {
                    select.AddWhere("masterid = @masterid");
                    select.AddParameter("@masterid", exchange.validDlg.masterid);
                }
                //select.AddWhere(getInventoryDepartmentFilter("inventorydepartmentid", getUsersPrimaryDepartmentid, hasWhere)); //jh 12/09/09 CAS-6696-YZVS
                select.AddOrderBy("description");
                searchresults = await qry.QueryToFwJsonTableAsync(select, true);
            }
            return searchresults;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> PendingOutNonBcAsync(FwSqlConnection conn, int pageno, int pagesize, string searchmode, string searchvalue, ExchangeModel exchange, UserContext user)
        {
            FwJsonDataTable searchresults = null;
            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.PageNo   = pageno;
                select.PageSize = pagesize;
                select.Add("select *");
                select.Add("from exchangependingoutnonbcview with (nolock) ");
                select.Parse();
                if (!string.IsNullOrEmpty(exchange.validDlg.locationid))
                {
                    select.AddWhere("locationid = @locationid");
                    select.AddParameter("@locationid", exchange.validDlg.locationid);
                }
                if (!string.IsNullOrEmpty(exchange.validDlg.dealid))
                {
                    select.AddWhere("dealid = @dealid");
                    select.AddParameter("@dealid", exchange.validDlg.dealid);
                }
                if (!string.IsNullOrEmpty(exchange.validDlg.departmentid))
                {
                    select.AddWhere("departmentid = @departmentid");
                    select.AddParameter("@departmentid", exchange.validDlg.departmentid);
                }
                if (!string.IsNullOrEmpty(exchange.validDlg.masterid))
                {
                    select.AddWhere("masterid = @masterid");
                    select.AddParameter("@masterid", exchange.validDlg.masterid);
                }
                //select.AddWhere(getInventoryDepartmentFilter("inventorydepartmentid", getUsersPrimaryDepartmentid, hasWhere)); //jh 12/09/09 CAS-6696-YZVS
                select.AddOrderBy("description");
                searchresults = await qry.QueryToFwJsonTableAsync(select, true);
            }
            return searchresults;
        }
        //---------------------------------------------------------------------------------------------
        public async Task<FwJsonDataTable> PendingOutSerialAsync(FwSqlConnection conn, int pageno, int pagesize, string searchmode, string searchvalue, ExchangeModel exchange, UserContext user)
        {
            FwJsonDataTable searchresults = null;
            using (FwSqlCommand qry = new FwSqlCommand(conn, this.ApplicationConfig.DatabaseSettings.QueryTimeout))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.PageNo   = pageno;
                select.PageSize = pagesize;
                select.Add("select *");
                select.Add("from exchangependingoutserialview with (nolock)");
                select.Parse();
                if (!string.IsNullOrEmpty(exchange.validDlg.locationid))
                {
                    select.AddWhere("locationid = @locationid");
                    select.AddParameter("@locationid", exchange.validDlg.locationid);
                }
                if (!string.IsNullOrEmpty(exchange.validDlg.dealid))
                {
                    select.AddWhere("dealid = @dealid");
                    select.AddParameter("@dealid", exchange.validDlg.dealid);
                }
                if (!string.IsNullOrEmpty(exchange.validDlg.departmentid))
                {
                    select.AddWhere("departmentid = @departmentid");
                    select.AddParameter("@departmentid", exchange.validDlg.departmentid);
                }
                if (!string.IsNullOrEmpty(exchange.validDlg.serialno))
                {
                    select.AddWhere("serialno = @serialno");
                    select.AddParameter("@serialno", exchange.validDlg.serialno);
                }
                //select.AddWhere(getInventoryDepartmentFilter("inventorydepartmentid", getUsersPrimaryDepartmentid, hasWhere)); //jh 12/09/09 CAS-6696-YZVS
                select.AddOrderBy("description");
                searchresults = await qry.QueryToFwJsonTableAsync(select, true);
            }
            return searchresults;
        }
        //---------------------------------------------------------------------------------------------
    }
}
