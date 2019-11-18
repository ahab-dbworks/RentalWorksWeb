using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
namespace WebApi.Modules.Settings.Sound
{
    [FwSqlTable("soundview")]
    public class SoundLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "soundid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string SoundId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "sound", modeltype: FwDataTypes.Text)]
        public string Sound { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "filename", modeltype: FwDataTypes.Text)]
        public string FileName { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "systemsound", modeltype: FwDataTypes.Boolean)]
        public bool? SystemSound { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "soundcolor", modeltype: FwDataTypes.OleToHtmlColor)]
        public string SoundColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
