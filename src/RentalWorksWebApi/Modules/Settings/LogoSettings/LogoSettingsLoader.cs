using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data;

namespace WebApi.Modules.Settings.LogoSettings
{
    [FwSqlTable("controlview")]
    public class LogoSettingsLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "controlid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string LogoSettingsId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "reportlogoimageid", modeltype: FwDataTypes.Text)]
        public string LogoImageId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "reportlogoimage", modeltype: FwDataTypes.JpgDataUrl)]
        public string LogoImage { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "reportlogoimageheight", modeltype: FwDataTypes.Integer)]
        public int? LogoImageHeight { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "reportlogoimagewidth", modeltype: FwDataTypes.Integer)]
        public int? LogoImageWidth { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
        }
        //------------------------------------------------------------------------------------ 
    }
}
 