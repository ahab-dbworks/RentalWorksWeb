using FwStandard.Data;
using FwStandard.Models;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
using System.Collections.Generic;
namespace WebApi.Modules.HomeControls.RepairRelease
{
    [FwSqlTable("repairreleaseview")]
    public class RepairReleaseLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairreleaseid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string RepairReleaseId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "releasedate", modeltype: FwDataTypes.Date)]
        public string ReleaseDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "usersid", modeltype: FwDataTypes.Text)]
        public string UsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "releasedby", modeltype: FwDataTypes.Text)]
        public string ReleasedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "releaseqty", modeltype: FwDataTypes.Decimal)]
        public decimal? ReleaseQuantity { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "repairid", modeltype: FwDataTypes.Text)]
        public string RepairId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            addFilterToSelect("RepairId", "repairid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}
