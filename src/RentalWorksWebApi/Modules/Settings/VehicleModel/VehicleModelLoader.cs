using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;

namespace WebApi.Modules.Settings.VehicleModel
{
    [FwSqlTable("vehiclemodelview")]
    public class VehicleModelLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehiclemodelid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string VehicleModelId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehiclemodel", modeltype: FwDataTypes.Text, required: true)]
        public string VehicleModel { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehiclemakeid", modeltype: FwDataTypes.Text, required: true)]
        public string VehicleMakeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehiclemake", modeltype: FwDataTypes.Text)]
        public string VehicleMake { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            if ((request != null) && (request.miscfields != null))
            {
                if (((IDictionary<string, object>)request.miscfields).ContainsKey("VehicleMakeId"))
                {
                    select.Parse();
                    select.AddWhere("vehiclemakeid = @vehiclemakeid");
                    select.AddParameter("@vehiclemakeid", request.miscfields.VehicleMakeId.value);
                }
            }
        }
        //------------------------------------------------------------------------------------
    }
}
