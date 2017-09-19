using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using RentalWorksWebApi.Data;

namespace RentalWorksWebApi.Modules.Home.CompanyTaxOption
{
    [FwSqlTable("deallocation")]
    public class DealLocationRecord : RwDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "id", modeltype: FwDataTypes.Text, identity: true, isPrimaryKey: true)]
        public string Id { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "internalchar", modeltype: FwDataTypes.Text, maxlength: 1, isPrimaryKey: true, isPrimaryKeyOptional: true)]
        public string InternalChar { get; set; } = "";
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string CompanyId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "taxoptionid", modeltype: FwDataTypes.Text, maxlength: 8)]
        public string TaxOptionId { get; set; }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------
    }
}
