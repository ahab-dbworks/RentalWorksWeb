using FwStandard.ConfigSection;
using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;

namespace RentalWorksLogic.Settings
{
    public class CustomerStatusLogic

    {
        public string CustomerStatusId { get; set; }
        public string CustomerStatus { get; set; }
        public string StatusType { get; set; }
        public string CreditStatusId { get; set; }
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        private readonly DatabaseConfig _dbConfig;
        public CustomerStatusLogic(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }
        //------------------------------------------------------------------------------------
        public List<CustomerStatusLogic> Browse(BrowseRequestDto request)
        {
            List<CustomerStatusLogic> results;
            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                FwSqlCommand2 qry = new FwSqlCommand2(conn, _dbConfig.QueryTimeout);
                qry.Add("select CustomerStatusId = custstatusid,");
                qry.Add("       CustomerStatus   = rtrim(custstatus),");
                qry.Add("       StatusType       = statustype,");
                qry.Add("       CreditStatusId   = rtrim(creditstatusid),");
                qry.Add("       DateStamp        = convert(varchar(33), datestamp, 126)");
                qry.Add("from custstatus with (nolock)");
                switch (request.orderby)
                {
                    case "CustomerStatus, StatusType":
                        qry.Add("order by custstatus, statustype " + request.orderbydirection.ToString());
                        break;
                    case "CustomerStatus":
                    case "StatusType":
                        qry.Add("order by " + request.orderby + " " + request.orderbydirection.ToString());
                        break;
                    default:
                        throw new Exception("Unsupported order by " + request.orderby);
                }
                results = qry.Select<CustomerStatusLogic>(true, request.pageno, request.pagesize);
            }
            return results;
        }
        //------------------------------------------------------------------------------------
        public CustomerStatusLogic Get(string custstatusid)
        {
            List<CustomerStatusLogic> records = null;
            CustomerStatusLogic result = null;
            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                FwSqlCommand2 qry = new FwSqlCommand2(conn, _dbConfig.QueryTimeout);
                qry.Add("select CustomerStatusId = custstatusid,");
                qry.Add("       CustomerStatus   = rtrim(custstatus),");
                qry.Add("       StatusType       = statustype,");
                qry.Add("       CreditStatusId   = creditstatusid");
                qry.Add("from custstatus with (nolock)");
                qry.Add("where custstatusid = @custstatusid");
                qry.Add("order by custstatus");
                qry.AddParameter("@custstatusid", custstatusid);
                records = qry.Select<CustomerStatusLogic>(true, 1, 1);
                if (records.Count > 0)
                {
                    result = records[0];
                }
            }
            return result;
        }
        //------------------------------------------------------------------------------------
        public void Insert(InsertRequestDto request)
        {
            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                FwSqlCommand2 cmd = new FwSqlCommand2(conn, _dbConfig.QueryTimeout);
                cmd.Insert(true, "custstatus", request);
            }
        }
        //------------------------------------------------------------------------------------
        public void Update(UpdateRequestDto request)
        {
            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                FwSqlCommand2 cmd = new FwSqlCommand2(conn, _dbConfig.QueryTimeout);
                cmd.Update(true, "custstatus", request);
            }
        }
        //------------------------------------------------------------------------------------
        public void Delete(DeleteRequestDto request)
        {
            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                FwSqlCommand2 cmd = new FwSqlCommand2(conn, _dbConfig.QueryTimeout);
                cmd.Delete(true, "custstatus", "where custstatusid = @custstatusid", request);
            }
        }
        //------------------------------------------------------------------------------------
    }
}
