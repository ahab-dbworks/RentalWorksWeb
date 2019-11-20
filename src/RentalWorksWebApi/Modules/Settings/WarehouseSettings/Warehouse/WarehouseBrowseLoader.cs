using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Settings.WarehouseSettings.Warehouse
{
    [FwSqlTable("warehouseview")]
    public class WarehouseBrowseLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string WarehouseId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
       [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            string locationId = "";

            if (request != null)
            {
                if (request.uniqueids != null)
                {
                    IDictionary<string, object> uniqueIds = ((IDictionary<string, object>)request.uniqueids);
                    if (uniqueIds.ContainsKey("LocationId"))
                    {
                        locationId = uniqueIds["LocationId"].ToString();
                    }
                    if (uniqueIds.ContainsKey("OfficeLocationId"))
                    {
                        locationId = uniqueIds["OfficeLocationId"].ToString();
                    }
                }
            }

            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 

            if (!string.IsNullOrEmpty(locationId))
            {
                select.AddWhere("exists (select* from warehouselocation wl where wl.warehouseid = " + TableAlias + ".warehouseid and wl.locationid = @locationid)");
                select.AddParameter("@locationid", locationId);
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}