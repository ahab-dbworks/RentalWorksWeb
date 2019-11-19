using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.HomeControls.MasterLocation
{
    [FwSqlTable("masterlocationview")]
    public abstract class MasterLocationLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "id", modeltype: FwDataTypes.Integer, identity: true, isPrimaryKey: true)]
        public int? Id { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "internalchar", modeltype: FwDataTypes.Text, isPrimaryKey: true, isPrimaryKeyOptional: true)]
        public string InternalChar { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "location", modeltype: FwDataTypes.Text)]
        public string Location { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "taxable", modeltype: FwDataTypes.Boolean)]
        public bool? Taxable { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modbyusersid", modeltype: FwDataTypes.Text)]
        public string ModByUsersId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "modbyuser", modeltype: FwDataTypes.Text)]
        public string ModByUser { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}