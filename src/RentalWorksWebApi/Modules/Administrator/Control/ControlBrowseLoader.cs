//using FwStandard.Data; 
//using FwStandard.Models; 
//using FwStandard.SqlServer; 
//using FwStandard.SqlServer.Attributes; 
//using WebApi.Data;

//namespace WebApi.Modules.Administrator.Control
//{
//    [FwSqlTable("controlview")]
//    public class ControlBrowseLoader : AppDataLoadRecord
//    {
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "controlid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
//        public string ControlId { get; set; } = "";
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "company", modeltype: FwDataTypes.Text)]
//        public string CompanyName { get; set; }
//        //------------------------------------------------------------------------------------ 
//        [FwSqlDataField(column: "system", modeltype: FwDataTypes.Text)]
//        public string SystemName { get; set; }
//        //------------------------------------------------------------------------------------ 
//        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
//        {
//            base.SetBaseSelectQuery(select, qry, customFields, request);
//            select.Parse();
//            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
//            //addFilterToSelect("UniqueId", "uniqueid", select, request); 
//        }
//        //------------------------------------------------------------------------------------ 
//    }
//}