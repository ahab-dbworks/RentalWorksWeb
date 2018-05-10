using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using FwStandard.SqlServer.Attributes;
using WebApi.Data;
namespace WebApi.Modules.Settings.DepartmentLocation
{
    [FwSqlTable("deptloc")]
    public class DepartmentLocationRecord : AppDataReadWriteRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "departmentid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string DepartmentId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "locationid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8, isPrimaryKey: true)]
        public string LocationId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "defaultordertypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        public string DefaultOrderTypeId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime, sqltype: "datetime")]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 

        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "deptheadid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string DeptheadId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "repairbillable", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Repairbillable { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "daysforweekly", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        //public int? Daysforweekly { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "autoaddtoorder", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Autoaddtoorder { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "daysformonthly", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        //public int? Daysformonthly { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "chargenoinsfee", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Chargenoinsfee { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "chargensf", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Chargensf { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "contractcopies", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        //public int? Contractcopies { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "flatmasterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string FlatmasterId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "freightmasterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string FreightmasterId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "invcreditmasterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string InvcreditmasterId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "noinsfeemasterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string NoinsfeemasterId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "noinsfeepct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 7, scale: 4)]
        //public decimal? Noinsfeepct { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "nsfamount", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 9, scale: 2)]
        //public decimal? Nsfamount { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "nsfmasterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string NsfmasterId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "restockingfeemasterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string RestockingfeemasterId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "supplementalmasterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string SupplementalmasterId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "autocheckoutsubsale", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Autocheckoutsubsale { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "copyrentalnotes", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Copyrentalnotes { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "copysalesnotes", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Copysalesnotes { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "requireworksheet", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Requireworksheet { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "porequireapprove", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Porequireapprove { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "poapproveamount", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        //public int? Poapproveamount { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "itemorderdefault", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 15)]
        //public string Itemorderdefault { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "closepoinvoiceamt", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 10, scale: 2)]
        //public decimal? Closepoinvoiceamt { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "returninventoryretiredreasonid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string ReturninventoryretiredreasonId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "quikentrymasterorderby", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 5)]
        //public string Quikentrymasterorderby { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "setinvoicedate", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        //public string Setinvoicedate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "autocreditonsalesreturn", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Autocreditonsalesreturn { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "billingdates", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        //public string Billingdates { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "depositfeemasterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string DepositfeemasterId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "enablemanualrefresh", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Enablemanualrefresh { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "accshowdefaultqty", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? AccshowdefaultQuantity { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "accshowdescription", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Accshowdescription { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "accshowmasterno", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Accshowmasterno { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "accshownotes", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Accshownotes { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "accshowpartno", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Accshowpartno { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "accwidthdescription", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        //public int? Accwidthdescription { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "accwidthpartno", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        //public int? Accwidthpartno { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "assigned", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Assigned { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "autocheckavail", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Autocheckavail { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "autosplitconflicts", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Autosplitconflicts { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "itemshowdescription", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Itemshowdescription { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "itemshowmasterno", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Itemshowmasterno { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "itemshownotes", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Itemshownotes { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "itemshowpartno", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Itemshowpartno { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "itemshowqtyin", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? ItemshowQuantityin { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "itemshowrate", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Itemshowrate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "itemshowunit", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Itemshowunit { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "itemwidthdescription", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        //public int? Itemwidthdescription { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "itemwidthpartno", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        //public int? Itemwidthpartno { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qeshowavailqty", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? QeshowavailQuantity { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qeshowconflictdate", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Qeshowconflictdate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "realtimeavail", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Realtimeavail { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "picklistselectall", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Picklistselectall { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "defaultbillingdates", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Defaultbillingdates { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "totalbuttondefault", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Totalbuttondefault { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "costcenter", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        //public string Costcenter { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "invadjustmentmasterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string InvadjustmentmasterId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "invnolineitemmasterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string InvnolineitemmasterId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "noinsurancefeebasedon", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 15)]
        //public string Noinsurancefeebasedon { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "orderactivewithsubs", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Orderactivewithsubs { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "repairtaxoptiondefault", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10)]
        //public string Repairtaxoptiondefault { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "repairtaxoptionid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string RepairtaxoptionId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "voidreturnunretiredreasonid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string VoidreturnunretiredreasonId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "promptforavailcheck", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Promptforavailcheck { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "onlyorderposonworksheet", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Onlyorderposonworksheet { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "creditinvoicenofrom", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        //public string Creditinvoicenofrom { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "disabledatewarnings", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Disabledatewarnings { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "disablependingpowarnings", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Disablependingpowarnings { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "requireinvoicevoidreason", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Requireinvoicevoidreason { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "picklistonquote", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Picklistonquote { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "autoaddtoordertype", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Autoaddtoordertype { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "chargesurcharge", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Chargesurcharge { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "kitbillingrule", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        //public string Kitbillingrule { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "picklistautoselectcomplete", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Picklistautoselectcomplete { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "picklistautoselectkit", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Picklistautoselectkit { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "picklistautoselectrands", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Picklistautoselectrands { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "poitemshowqtyin", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? PoitemshowQuantityin { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qeshowavailqtycompletekit", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? QeshowavailQuantitycompletekit { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qeshowconflictdatecompletekit", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Qeshowconflictdatecompletekit { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "scheduleautorefresheverymin", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        //public int? Scheduleautorefresheverymin { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "stagingautoselectcomplete", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Stagingautoselectcomplete { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "stagingautoselectkit", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Stagingautoselectkit { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "surchargemasterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string SurchargemasterId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "surchargepct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 7, scale: 4)]
        //public decimal? Surchargepct { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "updateoutdeliverydatewithpick", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Updateoutdeliverydatewithpick { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "updateindeliverydatewithpickup", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Updateindeliverydatewithpickup { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "orderdateprint", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 20)]
        //public string Orderdateprint { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "inactivatewallhistory", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Inactivatewallhistory { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "invoiceprintcustomer", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Invoiceprintcustomer { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "orderprintcustomer", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Orderprintcustomer { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "quoteprintcustomer", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Quoteprintcustomer { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "useresponsibleperson", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Useresponsibleperson { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "updatesubacc", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Updatesubacc { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "snapshotonprint", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Snapshotonprint { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "checkinsortordersbydeal", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Checkinsortordersbydeal { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "promptaisleshelf", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Promptaisleshelf { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qeshowqcrequired", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Qeshowqcrequired { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qehideaccessorypane", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Qehideaccessorypane { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "usenoinsfeethreshold", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Usenoinsfeethreshold { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "noinsfeethreshold", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        //public int? Noinsfeethreshold { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "snapshotonsave", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Snapshotonsave { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "snapshotdeletedays", modeltype: FwDataTypes.Integer, sqltype: "int")]
        //public int? Snapshotdeletedays { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "itemorderdefaultlabor", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 15)]
        //public string Itemorderdefaultlabor { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "vendorinvoicepushtopo", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 15)]
        //public string Vendorinvoicepushtopo { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "versiononprint", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Versiononprint { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "versiononsave", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Versiononsave { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "autocompleterepaironrelease", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Autocompleterepaironrelease { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "creditcardfeeflg", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Creditcardfeeflg { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "creditcardfeemasterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string CreditcardfeemasterId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "creditcardfeepct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 6, scale: 2)]
        //public decimal? Creditcardfeepct { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "maxbillingsearchdelaydays", modeltype: FwDataTypes.Integer, sqltype: "int")]
        //public int? Maxbillingsearchdelaydays { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "porequiresecondapprove", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Porequiresecondapprove { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "posecondapproveamount", modeltype: FwDataTypes.Integer, sqltype: "numeric")]
        //public int? Posecondapproveamount { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "posnapshotdeletedays", modeltype: FwDataTypes.Integer, sqltype: "int")]
        //public int? Posnapshotdeletedays { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "posnapshotonprint", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Posnapshotonprint { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "posnapshotonsave", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Posnapshotonsave { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "presentationlayerid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string PresentationlayerId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "promptupdatepobillingdates", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Promptupdatepobillingdates { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "promptupdatepoestimateddates", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Promptupdatepoestimateddates { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "unapprovepodecreaseamt", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        //public decimal? Unapprovepodecreaseamt { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "unapprovepodecreasepct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 6, scale: 2)]
        //public decimal? Unapprovepodecreasepct { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "unapprovepoincreaseamt", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        //public decimal? Unapprovepoincreaseamt { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "unapprovepoincreasepct", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 6, scale: 2)]
        //public decimal? Unapprovepoincreasepct { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "updatepobillingdates", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Updatepobillingdates { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "updatepoestimateddates", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Updatepoestimateddates { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "syncindelondatewithpickup", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Syncindelondatewithpickup { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "syncindelreqdatewitheststop", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Syncindelreqdatewitheststop { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "syncoutdelondatewitheststart", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Syncoutdelondatewitheststart { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "syncoutdelreqdatewithloadin", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Syncoutdelreqdatewithloadin { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "orbitsapexportfilename", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100)]
        //public string Orbitsapexportfilename { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qeshowavailqtyallwh", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? QeshowavailQuantityallwh { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qeshowavailqtyallwhcompletekit", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? QeshowavailQuantityallwhcompletekit { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "completerentalwithlanddorder", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Completerentalwithlanddorder { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "preventreceiveunapprovedpo", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Preventreceiveunapprovedpo { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "closepoinvoicepromptreason", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Closepoinvoicepromptreason { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "preventinvoiceunapprovedpo", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Preventinvoiceunapprovedpo { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "exchangetermsconditionsid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string ExchangetermsconditionsId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qeallowsorting", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Qeallowsorting { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qeashowavailqtyallwh", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? QeashowavailQuantityallwh { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qeashowavailqtyallwhpackage", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? QeashowavailQuantityallwhpackage { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "consignorfeepomasterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string ConsignorfeepomasterId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "rwnetpoordertypeid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string RwnetpoordertypeId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "finalldretiredreasonid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string FinalldretiredreasonId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "rentalsaleretiredreasonid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string RentalsaleretiredreasonId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "repairautocompleteqc", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Repairautocompleteqc { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "orderspecificcontacts", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Orderspecificcontacts { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "pospecificcontacts", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Pospecificcontacts { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "duplicatevendorinvoicenumber", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Duplicatevendorinvoicenumber { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "copydeliverynotestosubpo", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Copydeliverynotestosubpo { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "posnapshotonapproval", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Posnapshotonapproval { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "usedepartmentlogo", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Usedepartmentlogo { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "logofilename", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100)]
        //public string Logofilename { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "logoheight", modeltype: FwDataTypes.Integer, sqltype: "int")]
        //public int? Logoheight { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "logowidth", modeltype: FwDataTypes.Integer, sqltype: "int")]
        //public int? Logowidth { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "disablelogoexchange", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Disablelogoexchange { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "disablelogoin", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Disablelogoin { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "disablelogolost", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Disablelogolost { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "disablelogoout", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Disablelogoout { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "disablelogoreceive", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Disablelogoreceive { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "disablelogoreconcile", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Disablelogoreconcile { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "disablelogoreturn", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Disablelogoreturn { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "watermarkfilename", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 100)]
        //public string Watermarkfilename { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "watermarkheight", modeltype: FwDataTypes.Integer, sqltype: "int")]
        //public int? Watermarkheight { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "watermarkwidth", modeltype: FwDataTypes.Integer, sqltype: "int")]
        //public int? Watermarkwidth { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "watermarkoninvoice", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Watermarkoninvoice { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "excludecompanysignature", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Excludecompanysignature { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "syncoutdelreqdatewitheststart", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Syncoutdelreqdatewitheststart { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "syncindelondatewitheststop", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Syncindelondatewitheststop { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "adjustcostofsubsoncredits", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Adjustcostofsubsoncredits { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "containerconfirminenable", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Containerconfirminenable { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "containerconfirminusedate", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Containerconfirminusedate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "containerconfirmoutenable", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Containerconfirmoutenable { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "containerconfirmoutusedate", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Containerconfirmoutusedate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "picklistautoselectcontainer", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Picklistautoselectcontainer { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qeshowavailqtyinroom", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? QeshowavailQuantityinroom { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "updateconsignacc", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Updateconsignacc { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "creditvendorinvoicenofrom", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        //public string Creditvendorinvoicenofrom { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "vendorinvcreditmasterid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string VendorinvcreditmasterId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "invoiceprebilladdons", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Invoiceprebilladdons { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "creditprebillearlyreturns", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Creditprebillearlyreturns { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "autocreditprebillearlyreturns", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Autocreditprebillearlyreturns { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "creatediscountnotesoninvoice", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Creatediscountnotesoninvoice { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "promptfororderevent", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Promptfororderevent { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "laborautosubtotalbyday", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Laborautosubtotalbyday { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "summarizequikaddcrew", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Summarizequikaddcrew { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "hiatusdiscfrom", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 10)]
        //public string Hiatusdiscfrom { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "laborhoursentryby", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        //public string Laborhoursentryby { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "laborhoursbilledby", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        //public string Laborhoursbilledby { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "overtimecalculatedby", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        //public string Overtimecalculatedby { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "overtimestartsafter", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        //public decimal? Overtimestartsafter { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "doubletimestartsafter", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        //public decimal? Doubletimestartsafter { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "seventhconsecutiveday", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Seventhconsecutiveday { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "overtimehoursseventhay", modeltype: FwDataTypes.Decimal, sqltype: "numeric", precision: 12, scale: 2)]
        //public decimal? Overtimehoursseventhay { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "timelogmethod", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 20)]
        //public string Timelogmethod { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "includeinbillinganalysis", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Includeinbillinganalysis { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "prepfeesinrentalrate", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Prepfeesinrentalrate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "hideconsignonsubtab", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Hideconsignonsubtab { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "substitutesamerate", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Substitutesamerate { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "defaultpobillcyclefromorder", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Defaultpobillcyclefromorder { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "qecolorifinpackge", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Qecolorifinpackge { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "vinoapproveoutsidepoperiod", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Vinoapproveoutsidepoperiod { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "vinoapproveifgreaterthanpo", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Vinoapproveifgreaterthanpo { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "defaultcrewquikaddby", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 255)]
        //public string Defaultcrewquikaddby { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "ordercompleteswithunstageditems", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Ordercompleteswithunstageditems { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "outsiderepairkeepopenwithpo", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Outsiderepairkeepopenwithpo { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "defaultblankestdates", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Defaultblankestdates { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "defaultvidatetoday", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Defaultvidatetoday { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "snapshotonpdf", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Snapshotonpdf { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "fullconsfeesonorderdates", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Fullconsfeesonorderdates { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "allowdecreaseorderwhenstaged", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Allowdecreaseorderwhenstaged { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "assignconsignorbeforestaging", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Assignconsignorbeforestaging { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "consignaccessories", modeltype: FwDataTypes.Text, sqltype: "varchar", maxlength: 10)]
        //public string Consignaccessories { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "copyinactiveitems", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Copyinactiveitems { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "defaultcheckindeptfromuser", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Defaultcheckindeptfromuser { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "salescheckoutretiredreasonid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string SalescheckoutretiredreasonId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "salescheckoutunretiredreasonid", modeltype: FwDataTypes.Text, sqltype: "char", maxlength: 8)]
        //public string SalescheckoutunretiredreasonId { get; set; }
        ////------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "splitwhencheckedinearly", modeltype: FwDataTypes.Boolean, sqltype: "char")]
        //public bool? Splitwhencheckedinearly { get; set; }
        ////------------------------------------------------------------------------------------ 
    }
}
