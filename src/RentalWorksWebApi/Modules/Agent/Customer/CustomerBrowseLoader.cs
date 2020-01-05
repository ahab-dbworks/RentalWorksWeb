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
        [FwSqlDataField(column: "add1", modeltype: FwDataTypes.Text)]
        public string Address1 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "add2", modeltype: FwDataTypes.Text)]
        public string Address2 { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "city", modeltype: FwDataTypes.Text)]
        public string City { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "state", modeltype: FwDataTypes.Text)]
        public string State { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "countryid", modeltype: FwDataTypes.Text)]
        public string CountryId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "country", modeltype: FwDataTypes.Text)]
        public string Country { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "zip", modeltype: FwDataTypes.Text)]
        public string ZipCode { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------
    }
}
