using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
using System;
using WebApi;
namespace WebApi.Modules.Administrator.Plugin
{
    [FwSqlTable("plugin")]
    public class PluginLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "pluginid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string PluginId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "description", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "settings", modeltype: FwDataTypes.Text)]
        public string Settings { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            //string filterId = ""; 
            //DateTime filterDate = DateTime.MinValue; 
            //bool filterBoolean = false; 
            // 
            //if ((request != null) && (request.uniqueids != null)) 
            //{ 
            //    IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids); 
            //    if (uniqueIds.ContainsKey("FilterId")) 
            //    { 
            //        filterId = uniqueIds["FilterId"].ToString(); 
            //    } 
            //    if (uniqueIds.ContainsKey("FilterDate")) 
            //    { 
            //        filterDate = FwConvert.ToDateTime(uniqueIds["FilterDate"].ToString()); 
            //    } 
            //    if (uniqueIds.ContainsKey("FilterBoolean")) 
            //    { 
            //        filterBoolean = FwConvert.ToBoolean(uniqueIds["FilterBoolean"].ToString()); 
            //    } 
            //} 
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
            //select.AddParameter("@filterid", filterId); 
            //select.AddParameter("@filterdate", filterDate); 
            //select.AddParameter("@filterboolean", filterBoolean); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
