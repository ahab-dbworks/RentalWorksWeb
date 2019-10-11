using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;

namespace WebApi.Modules.Agent.Customer
{
    [FwSqlTable("customerwebbrowseview")]
    public class CustomerBrowseLoader: AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "customerid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string CustomerId { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "custno", modeltype: FwDataTypes.Text)]
        public string CustomerNumber { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "customer", modeltype: FwDataTypes.Text)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "custtypeid", modeltype: FwDataTypes.Text)]
        public string CustomerTypeId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "custtype", modeltype: FwDataTypes.Text)]
        public string CustomerType { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "custstatus", modeltype: FwDataTypes.Text)]
        public string CustomerStatus { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
    }
}
