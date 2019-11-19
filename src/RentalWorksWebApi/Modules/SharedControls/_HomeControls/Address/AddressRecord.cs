using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.HomeControls.Address
{
    [FwSqlTable("appaddress")]
    public class AddressRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "appaddressid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string AddressId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "attention", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100)]
        public string Attention { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "city", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string City { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "countryid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string CountryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "name", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100)]
        public string Name { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "state", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string State { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid1", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string UniqueId1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid2", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string UniqueId2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "uniqueid3", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string UniqueId3 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "zip", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10)]
        public string ZipCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phone", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string Phone { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "fax", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string Fax { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "attention2", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100)]
        public string Attention2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phone800", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string PhoneTollFree { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "phoneother", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        public string PhoneOther { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "add1", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string Address1 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "add2", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        public string Address2 { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
