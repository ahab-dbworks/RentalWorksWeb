using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using System;
using WebApi.Data;
namespace WebApi.Modules.Settings.AvailabilityKeepFreshLog
{
    [FwSqlTable("availkeepfreshlogview")]
    public class AvailabilityKeepFreshLogLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "id", modeltype: FwDataTypes.Integer)]
        public int? Id { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "batchsize", modeltype: FwDataTypes.Integer)]
        public int? BatchSize { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "startdatetime", modeltype: FwDataTypes.DateTime)]
        public DateTime? StartDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "startdatetime", modeltype: FwDataTypes.DateTime)]
        public string StartDateTimeString { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enddatetime", modeltype: FwDataTypes.DateTime)]
        public DateTime? EndDateTime { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "enddatetime", modeltype: FwDataTypes.DateTime)]
        public string EndDateTimeString { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "durationinseconds", modeltype: FwDataTypes.Decimal)]
        public decimal? DurationInSeconds { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "durationinminutes", modeltype: FwDataTypes.Decimal)]
        public decimal? DurationInMinutes { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            //string paramString = GetUniqueIdAsString("ParamString", request) ?? ""; 
            //DateTime paramDate = GetUniqueIdAsDate("ParamDate", request) ?? DateTime.MinValue; 
            //bool paramBoolean = GetUniqueIdAsBoolean("ParamBoolean", request) ?? false; 
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
            //select.AddParameter("@paramstring", paramString); 
            //select.AddParameter("@paramdate", paramDate); 
            //select.AddParameter("@paramboolean", paramBoolean); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
