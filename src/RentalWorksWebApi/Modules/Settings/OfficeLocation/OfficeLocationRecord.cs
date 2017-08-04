using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Settings.OfficeLocation
{
    [FwSqlTable("location")]
    public class OfficeLocationRecord : RwDataReadWriteRecord
    {
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string LocationId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "loccode", modeltype: FwDataTypes.Text, maxlength: 4)]
        public string LocationCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text, maxlength: 30)]
        public string Location { get; set; } = "";
        //------------------------------------------------------------------------------------        
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool Inactive { get; set; }

    }
}
