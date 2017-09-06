using FwStandard.DataLayer;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;
using System.Collections.Generic;

namespace RentalWorksWebApi.Modules.Settings.GeneratorModel
{
    [FwSqlTable("generatormodelview")]
    public class GeneratorModelLoader : RwDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehiclemodelid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string GeneratorModelId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehiclemodel", modeltype: FwDataTypes.Text, required: true)]
        public string GeneratorModel { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehiclemakeid", modeltype: FwDataTypes.Text, required: true)]
        public string GeneratorMakeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "vehiclemake", modeltype: FwDataTypes.Text)]
        public string GeneratorMake { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "rowtype", modeltype: FwDataTypes.Text)]
        public string RowType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }
        //------------------------------------------------------------------------------------
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequestDto request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            if ((request != null) && (request.miscfields != null))
            {
                if (((IDictionary<string, object>)request.miscfields).ContainsKey("GeneratorMakeId"))
                {
                    select.Parse();
                    select.AddWhere("vehiclemakeid = @vehiclemakeid");
                    select.AddParameter("@vehiclemakeid", request.miscfields.GeneratorMakeId.value);
                }
            }
        }
        //------------------------------------------------------------------------------------
    }
}
