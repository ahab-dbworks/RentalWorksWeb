using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Settings.NumberFormat
{
    [FwSqlTable("numberformatview")]
    public class NumberFormatLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "numberformatid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string NumberFormatId { get; set; }
        [FwSqlDataField(column: "numberformat", modeltype: FwDataTypes.Text)]
        public string NumberFormat { get; set; }
        [FwSqlDataField(column: "mask", modeltype: FwDataTypes.Text)]
        public string Mask { get; set; }
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