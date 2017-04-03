using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fw.Json.SqlServer;
using RentalWorksMiddleTier.Models;

namespace RentalWorksMiddleTier
{
    class SqlCacheManager
    {
        //----------------------------------------------------------------------------------------------------
        public static Dictionary<string, TableCache> Cache = new Dictionary<string, TableCache>();
        //----------------------------------------------------------------------------------------------------
        public async static Task<DataTable> QueryToTableAsync(StringBuilder qry, List<SqlParameter> parameters, CancellationToken cancellationToken)
        {
            DataTable dt = null;
            using (SqlConnection conn = new SqlConnection("Server=sqldemo01.dbworkscloud.com;Database=rentalworksdev;User Id=dbworks;Password=db2424;Persist Security Info=True;Connect Timeout=15;Max Pool Size=100;Workstation Id=RentalWorksMiddleTier;Packet Size=4096;"))
            {
                using (SqlCommand cmd = new SqlCommand(qry.ToString(), conn))
                {
                    if (parameters != null)
                    {
                        foreach (SqlParameter parameter in parameters)
                        {
                            cmd.Parameters.Add(parameter);
                        }
                    }
                    await cmd.Connection.OpenAsync(cancellationToken);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (reader.HasRows)
                        {
                            DataTable schema = reader.GetSchemaTable();
                            dt = new DataTable();
                            for (int i = 0; i < schema.Rows.Count; i++)
                            {
                                dt.Columns.Add(schema.Rows[i]["ColumnName"].ToString());
                            }
                            while (await reader.ReadAsync(cancellationToken))
                            {
                                DataRow row = dt.NewRow();
                                for (int i = 0; i < schema.Rows.Count; i++)
                                {
                                    row[i] = reader.GetValue(i);
                                }
                                dt.Rows.Add(row);
                            }
                        }
                    }
                }
            }
            return dt;
        }
        //----------------------------------------------------------------------------------------------------
        public async static Task<TableCache> CacheTableAsync(string table, CancellationToken cancellationToken)
        {
            TableCache cache;
            cache = new TableCache();
            try
            {
                cache.Loading = true;
                cache.Table = table;
                SqlCacheManager.Cache[table] = cache;
                StringBuilder qry = new StringBuilder();
                qry.AppendLine("select *");
                qry.AppendLine("from " + table + " with(nolock)");
                DataTable dt = await QueryToTableAsync(qry, null, cancellationToken);
                for (int colno = 0; colno < dt.Columns.Count; colno++)
                {
                    cache.ColumnIndex[dt.Columns[colno].ColumnName] = colno;
                }
                for (int rowno = 0; rowno < dt.Rows.Count; rowno++)
                {
                    List<string> fields = new List<string>();
                    for (int colno = 0; colno < dt.Columns.Count; colno++)
                    {
                        fields.Add(dt.Rows[rowno][colno].ToString().TrimEnd());
                    }
                    cache.Rows.Add(fields);
                }
            }
            finally
            {
                cache.Loading = false;
            }
            return cache;
        }
        //----------------------------------------------------------------------------------------------------
        public static TableCache CacheTable(string table)
        {
            TableCache cache;
            cache = new TableCache();
            try
            {
                cache.Loading = true;
                cache.Table = table;
                SqlCacheManager.Cache[table] = cache;

                using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
                {
                    qry.Add("select *");
                    qry.Add("from " + table + " with(nolock)");
                    DataTable dt = qry.QueryToTable();
                    for (int colno = 0; colno < dt.Columns.Count; colno++)
                    {
                        cache.ColumnIndex[dt.Columns[colno].ColumnName] = colno;
                    }
                    for (int rowno = 0; rowno < dt.Rows.Count; rowno++)
                    {
                        List<string> fields = new List<string>();
                        for (int colno = 0; colno < dt.Columns.Count; colno++)
                        {
                            fields.Add(dt.Rows[rowno][colno].ToString().TrimEnd());
                        }
                        cache.Rows.Add(fields);
                    }
                }
            }
            finally
            {
                cache.Loading = false;
            }
            return cache;
        }
        //----------------------------------------------------------------------------------------------------
        public static void IndexCacheTable(string table, string uniqueid)
        {
            using (FwSqlCommand qry = new FwSqlCommand(FwSqlConnection.RentalWorks))
            {
                TableCache cache = null;
                if (!SqlCacheManager.Cache.ContainsKey(table))
                {
                    cache = CacheTable(table);
                }
                else
                {
                    cache = SqlCacheManager.Cache[table];
                }
                int colnouniqueid = cache.ColumnIndex[uniqueid];
                cache.DataIndex[uniqueid] = new Dictionary<string, List<List<string>>>();
                for (int rowno = 0; rowno < cache.Rows.Count; rowno++)
                {
                    string uniqueidcolvalue = cache.Rows[rowno][colnouniqueid];
                    if (!cache.DataIndex[uniqueid].ContainsKey(uniqueidcolvalue))
                    {
                            cache.DataIndex[uniqueid][uniqueidcolvalue] = new List<List<string>>();
                    }
                    cache.DataIndex[uniqueid][uniqueidcolvalue].Add(cache.Rows[rowno]);
                }
                SqlCacheManager.Cache[table] = cache;
            }
        }
        //----------------------------------------------------------------------------------------------------
        public static SqlCacheModels.GetDataResponse GetData(SqlCacheModels.GetDataRequest request)
        {
            SqlCacheModels.GetDataResponse response = new SqlCacheModels.GetDataResponse();
            TableCache cache = null;
            if (!SqlCacheManager.Cache.ContainsKey(request.table))
            {
                cache = CacheTable(request.table);
            }
            else
            {
                cache = SqlCacheManager.Cache[request.table];
            }
            //if the table cache exists, we need to block if the cache is in the middle of loading
            if (cache != null)
            {
                int waitTimeout = 30000;
                int sleepTimeout = 5;
                int waitTimeoutCounter = 0;
                while (cache.Loading)
                {
                    if (waitTimeoutCounter >= waitTimeout)
                    {
                        throw new Exception("Timeout expired while waiting for table " + request.table + " to finish loading.");
                    }
                    Thread.Sleep(sleepTimeout);
                    waitTimeoutCounter += sleepTimeout;
                }
            }
            if ((cache != null) && (!cache.DataIndex.ContainsKey(request.uniqueids[0])))
            {
                IndexCacheTable(request.table, request.uniqueids[0]);
            }
            // if the index contains the first uniqueid
            if ((cache != null) && (cache.DataIndex.ContainsKey(request.uniqueids[0])))
            {
                if (cache.DataIndex[request.uniqueids[0]].ContainsKey(request.uniqueidvalues[0]) && (cache.DataIndex[request.uniqueids[0]][request.uniqueidvalues[0]].Count > 0))
                {
                    for (int rowno = 0; rowno < cache.DataIndex[request.uniqueids[0]][request.uniqueidvalues[0]].Count; rowno++)
                    {
                        bool found = true;
                        Dictionary<string, string> objrow = new Dictionary<string, string>();
                        foreach (KeyValuePair<string, int> item in cache.ColumnIndex)
                        {
                            objrow[item.Key] = cache.DataIndex[request.uniqueids[0]][request.uniqueidvalues[0]][rowno][item.Value];
                        }
                        for (int uniqueidno = 1; uniqueidno < request.uniqueids.Count; uniqueidno++)
                        {
                            string uniqueid = request.uniqueids[uniqueidno];
                            if (!objrow.ContainsKey(uniqueid) || objrow[uniqueid] != request.uniqueidvalues[uniqueidno])
                            {
                                found = false;
                            }
                        }
                        if (found)
                        {
                            Dictionary<string, string> filteredrow = new Dictionary<string, string>();
                            if (request.columns.Count == 0)
                            {
                                filteredrow = objrow;
                            }
                            else
                            {
                                foreach (string column in request.columns)
                                {
                                    filteredrow[column] = objrow[column];
                                }
                            }
                            response.rows.Add(filteredrow);
                            if (response.rows.Count > 1)
                            {
                                break;
                            }
                        }
                    }
                }
                //else
                //{
                //    response.Message.StatusCode = HttpStatusCode.NotFound;
                //    string uniqueids      = "[\"" + String.Join("\", \"", request.uniqueids) + "\"]";
                //    string uniqueidvalues = "[\"" + String.Join(",", request.uniqueidvalues) + "\"]";
                //    response.Message.ReasonPhrase = String.Format("Unable to find table: {0}, uniqueids: {1}, uniqueidvalues: {2} (SqlCacheManager.GetData)", request.table, uniqueids, uniqueidvalues);
                //}
            }
            if (cache == null)
            {
                throw new Exception("Cache should not be null.");
            }

            return response;
        }
        //----------------------------------------------------------------------------------------------------
        public static SqlCacheModels.GetCountResponse GetCount(SqlCacheModels.GetCountRequest request)
        {
            SqlCacheModels.GetCountResponse response = new SqlCacheModels.GetCountResponse();
            TableCache cache = null;
            if (!SqlCacheManager.Cache.ContainsKey(request.table))
            {
                cache = CacheTable(request.table);
            }
            else
            {
                cache = SqlCacheManager.Cache[request.table];
            }
            //if the table cache exists, we need to block if the cache is in the middle of loading
            if (cache != null)
            {
                int waitTimeout = 30000;
                int sleepTimeout = 5;
                int waitTimeoutCounter = 0;
                while (cache.Loading)
                {
                    if (waitTimeoutCounter >= waitTimeout)
                    {
                        throw new Exception("Timeout expired while waiting for table " + request.table + " to finish loading.");
                    }
                    Thread.Sleep(sleepTimeout);
                    waitTimeoutCounter += sleepTimeout;
                }
            }
            if (cache != null && !request.includeinactive && !cache.DataIndex.ContainsKey("inactive"))
            {
                IndexCacheTable(request.table, "inactive");
            }
            
            if (cache != null && request.includeinactive)
            {
                response.count = cache.Rows.Count;
            }
            else if (cache != null && !request.includeinactive && cache.DataIndex.ContainsKey("inactive"))
            {
                response.count = cache.Rows.Count;
                if (cache.DataIndex["inactive"].ContainsKey("T"))
                {
                    response.count -= cache.DataIndex["inactive"]["T"].Count;
                }
            }
            else if (cache == null)
            {
                throw new Exception("Cache should not be null.");
            }

            return response;
        }
        //----------------------------------------------------------------------------------------------------
    }

    class TableCache
    {
        public bool Loading { get; set; } = false;
        public string Table { get; set; } = string.Empty;
        //public string UniqueId { get; set; } = string.Empty;
        public List<List<string>> Rows { get; set; } = new List<List<string>>();
        public Dictionary<string, int> ColumnIndex { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, Dictionary<string, List<List<string>>>> DataIndex { get; set; } = new Dictionary<string, Dictionary<string, List<List<string>>>>();
    }
}
