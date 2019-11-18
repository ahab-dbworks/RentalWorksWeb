using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Settings.Unit
{
    [FwSqlTable("unit")]
    public class UnitRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "unitid", modeltype: FwDataTypes.Text, maxlength: 8, isPrimaryKey: true)]
        public string UnitId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "unit", modeltype: FwDataTypes.Text, maxlength: 6, required: true)]
        public string Unit { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "unitdesc", modeltype: FwDataTypes.Text, maxlength: 20, required: true)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "unittype", modeltype: FwDataTypes.Text, maxlength: 8, required: true)]
        public string UnitType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "unitdescplural", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string PluralDescription { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
