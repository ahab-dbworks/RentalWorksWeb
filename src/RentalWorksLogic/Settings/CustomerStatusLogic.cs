using FwStandard.ConfigSection;
using FwStandard.Models;
using FwStandard.SqlServer;
using System;
using System.Collections.Generic;

namespace RentalWorksLogic.Settings
{
    public class CustomerStatusLogic
    {
        [FwPrimaryKey]
        public string custstatusid { get; set; } = string.Empty;
        public string custstatus { get; set; } = string.Empty;
        public string statustype { get; set; } = string.Empty;
        public string creditstatusid { get; set; } = string.Empty;
        public DateTime? datestamp { get; set; } = null;
        public string inactive { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------
        private DatabaseConfig _dbConfig { get; set; }
        public CustomerStatusLogic(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }
        //------------------------------------------------------------------------------------
        public FwJsonDataTable Browse(BrowseRequestDto request)
        {
            FwJsonDataTable dt = null;
            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                qry.AddColumn("custstatusid", "custstatusid", FwJsonDataTableColumn.DataTypes.Text, false, true, false);
                qry.AddColumn("custstatus", "custstatus", FwJsonDataTableColumn.DataTypes.Text, true, false, false);
                qry.AddColumn("statustype", "statustype", FwJsonDataTableColumn.DataTypes.Text, true, false, false);
                qry.AddColumn("creditstatusid", "creditstatusid", FwJsonDataTableColumn.DataTypes.Text, true, false, false);
                qry.AddColumn("inactive", "inactive", FwJsonDataTableColumn.DataTypes.Text, true, false, false);
                qry.AddColumn("datestamp", "datestamp", FwJsonDataTableColumn.DataTypes.UTCDateTime, true, false, false);
                qry.Add("select *");
                qry.Add("from custstatus with (nolock)");
                dt = qry.QueryToFwJsonTable(false);
            }
            return dt;
        }
        //------------------------------------------------------------------------------------
        public List<CustomerStatusLogic> Select(BrowseRequestDto request)
        {
            List<CustomerStatusLogic> results;
            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                qry.AddColumn("custstatusid", "custstatusid", FwJsonDataTableColumn.DataTypes.Text, false, true, false);
                qry.AddColumn("custstatus", "custstatus", FwJsonDataTableColumn.DataTypes.Text, true, false, false);
                qry.AddColumn("statustype", "statustype", FwJsonDataTableColumn.DataTypes.Text, true, false, false);
                qry.AddColumn("creditstatusid", "creditstatusid", FwJsonDataTableColumn.DataTypes.Text, true, false, false);
                qry.AddColumn("inactive", "inactive", FwJsonDataTableColumn.DataTypes.Text, true, false, false);
                qry.AddColumn("datestamp", "datestamp", FwJsonDataTableColumn.DataTypes.UTCDateTime, true, false, false);
                qry.Add("select *");
                qry.Add("from custstatus with (nolock)");
                results = qry.Select<CustomerStatusLogic>(true, request.pageno, request.pagesize);
            }
            return results;
        }
        //------------------------------------------------------------------------------------
        public List<CustomerStatusLogic> Get(string custstatusid)
        {
            List<CustomerStatusLogic> records = null;
            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                qry.AddColumn("custstatusid", "custstatusid", FwJsonDataTableColumn.DataTypes.Text, false, true, false);
                qry.AddColumn("custstatus", "custstatus", FwJsonDataTableColumn.DataTypes.Text, true, false, false);
                qry.AddColumn("statustype", "statustype", FwJsonDataTableColumn.DataTypes.Text, true, false, false);
                qry.AddColumn("creditstatusid", "creditstatusid", FwJsonDataTableColumn.DataTypes.Text, true, false, false);
                qry.AddColumn("inactive", "inactive", FwJsonDataTableColumn.DataTypes.Text, true, false, false);
                qry.AddColumn("datestamp", "datestamp", FwJsonDataTableColumn.DataTypes.UTCDateTime, true, false, false);
                qry.Add("select top 1 *");
                qry.Add("from custstatus with (nolock)");
                qry.Add("where custstatusid = @custstatusid");
                qry.Add("order by custstatus");
                qry.AddParameter("@custstatusid", custstatusid);
                records = qry.Select<CustomerStatusLogic>(true, 1, 1);
            }
            return records;
        }
        //------------------------------------------------------------------------------------
        public void Save(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
            if (string.IsNullOrEmpty(custstatusid))
            {
                //insert
                using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
                {
                    this.custstatusid = FwSqlData.GetNextId(conn, _dbConfig);
                    FwSqlCommand cmd = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                    cmd.Insert(true, "custstatus", this);
                }
            }
            else
            {
                // update
                using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
                {
                    FwSqlCommand cmd = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                    cmd.Update(true, "custstatus", this);
                }
            }
        }
        //------------------------------------------------------------------------------------
        public void Delete()
        {
            using (FwSqlConnection conn = new FwSqlConnection(_dbConfig.ConnectionString))
            {
                FwSqlCommand cmd = new FwSqlCommand(conn, _dbConfig.QueryTimeout);
                cmd.Add("delete from custstatus");
                cmd.Add("where custstatusid = @custstatusid");
                cmd.AddParameter("@custstatusid", custstatusid);
                cmd.ExecuteNonQuery();
            }
        }
        //------------------------------------------------------------------------------------
    }
}
