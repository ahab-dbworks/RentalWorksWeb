using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;

namespace WebApi.Modules.Settings.VehicleModel
{
    [FwSqlTable("vehiclemodel")]
    public class VehicleModelRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehiclemodelid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string VehicleModelId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehiclemodel", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string VehicleModel { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehiclemakeid", modeltype: FwDataTypes.Text, maxlength: 8, required: true)]
        public string VehicleMakeId { get; set; }
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
