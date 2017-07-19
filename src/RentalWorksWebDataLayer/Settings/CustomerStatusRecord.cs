using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;
using FwStandard.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RentalWorksWebDataLayer.Settings
{
    [FwSqlTable("custstatus")]
    public class CustomerStatusRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "custstatusid", dataType: FwDataTypes.Text, length: 8, isPrimaryKey: true)]
        public string CustomerStatusId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "custstatus", dataType: FwDataTypes.Text, length: 12)]
        public string CustomerStatus { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "statustype", dataType: FwDataTypes.Text, length: 1)]
        public string StatusType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "creditstatusid", dataType: FwDataTypes.Text, length: 8)]
        public string CreditStatusId { get; set; }
        //------------------------------------------------------------------------------------
        [JsonIgnore]
        public string CreditStatus { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "inactive", dataType: FwDataTypes.Boolean)]
        public string Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(columnName: "datestamp", dataType: FwDataTypes.UTCDateTime)]
        public DateTime? DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        public override async Task<FwJsonDataTable> BrowseAsync(BrowseRequestDto request)
        {
            using (FwSqlConnection conn = new FwSqlConnection(this._dbConfig.ConnectionString))
            {
                using (FwSqlCommand qry = new FwSqlCommand(conn, this._dbConfig.QueryTimeout))
                {
                    qry.AddColumn("", "CustomerStatusId", FwDataTypes.Text, false);
                    qry.AddColumn("", "CustomerStatus", FwDataTypes.Text, false);
                    qry.AddColumn("", "StatusType", FwDataTypes.Text, false);
                    qry.Add("select custstatusid as CustomerStatusId, custstatus as CustomerStatus, statustype as StatusType");
                    qry.Add("from custstatus with (nolock)");
                    var dt = await qry.QueryToFwJsonTableAsync(false);
                    return dt;
                }
            }
        }
        //------------------------------------------------------------------------------------
        public override async Task<bool> LoadAsync<T>(string[] primaryKeyValues)
        {
            bool loaded = await base.LoadAsync<T>(primaryKeyValues);
            using (FwSqlConnection conn = new FwSqlConnection(this._dbConfig.ConnectionString))
            {
                this.CreditStatus = await FwSqlCommand.GetStringDataAsync(conn, this._dbConfig.QueryTimeout, "creditstatus", "creditstatusid", this.CreditStatusId, "creditstatus");
            }
            return loaded;
        }
        //------------------------------------------------------------------------------------
    }
}
