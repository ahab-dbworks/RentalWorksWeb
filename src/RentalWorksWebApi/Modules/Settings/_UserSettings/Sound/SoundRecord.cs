using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Settings.UserSettings.Sound
{
    [FwSqlTable("sound")]
    public class SoundRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "soundid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string SoundId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sound", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255, required: true)]
        public string Sound { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "filename", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        public string FileName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "systemsound", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? SystemSound { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
