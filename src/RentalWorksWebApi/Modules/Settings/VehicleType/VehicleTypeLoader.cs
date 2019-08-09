using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.VehicleType
{
    public class VehicleTypeLoader : VehicleTypeBaseLoader 
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string VehicleTypeId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string VehicleType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "pmcycle", modeltype: FwDataTypes.Text)]
        public string PreventiveMaintenanceCycle { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "pmcycleperiod", modeltype: FwDataTypes.Integer)]
        public int? PreventiveMaintenanceCyclePeriod { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "dotperiod", modeltype: FwDataTypes.Integer)]
        public int? DotPeriod { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "licclassid", modeltype: FwDataTypes.Text)]
        public string LicenseClassId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "licclass", modeltype: FwDataTypes.Text)]
        public string LicenseClass { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "regulated", modeltype: FwDataTypes.Boolean)]
        public bool? Regulated { get; set; }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddWhere("(vehicletype = 'VEHICLE')");
        }
        //------------------------------------------------------------------------------------
    }
}
