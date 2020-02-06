using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Home.MasterItemDetail
{
    [FwSqlTable("masteritemdetail")]
    public class MasterItemDetailRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masteritemid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string MasterItemId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "orderid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, required: true)]
        public string OrderId { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "ldsubbarcode", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 40)]
        //public string LossAndDamageBarCode { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "discountoverrideusersid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string DiscountoverrideusersId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "discountoverridedate", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        //public string Discountoverridedate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "location", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 30)]
        //public string Location { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "consignorfeeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string ConsignorfeeId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "conflictread", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Conflictread { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "stagemanager1contactid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string Stagemanager1contactId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "stagemanager2contactid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string Stagemanager2contactId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "valetservice", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Valetservice { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "valetserviceqty", modeltype: FwDataTypes.Integer, sqltype: "int")]
        //public int? ValetserviceQuantity { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "valetservicestarttime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5)]
        //public string Valetservicestarttime { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "valetserviceendtime", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5)]
        //public string Valetserviceendtime { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "valetservicemasterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string ValetservicemasterId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "supportspacemasterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string SupportspacemasterId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "supportspace", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Supportspace { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "contactid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string ContactId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "spacemaintenance", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 50)]
        //public string Spacemaintenance { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "excludeweeks100discount", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Excludeweeks100discount { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "originalshowid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string OriginalshowId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "totimeestimated", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Totimeestimated { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "firstoutdatetime", modeltype: FwDataTypes.Date, sqltype: "datetime")]
        //public string Firstoutdatetime { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "otreason", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        //public string Otreason { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "dtreason", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        //public string Dtreason { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "issubstitute", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Issubstitute { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "substituteformasteritemid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string SubstituteformasteritemId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "separate", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Separate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "confirmcrewgear", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Confirmcrewgear { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "consignoragreementid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string ConsignorAgreementId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "consignorid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string ConsignorId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "crewgear", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Crewgear { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "hascheckoutaudit", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Hascheckoutaudit { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "mute", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Mute { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
