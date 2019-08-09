using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
namespace WebApi.Modules.Home.DealShipper
{
    [FwSqlTable("dealshipperview")]
    public class DealShipperLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealshipperid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string DealShipperId { get; set; } = "";
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "dealid", modeltype: FwDataTypes.Text)]
        public string DealId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "vendorid", modeltype: FwDataTypes.Text)]
        public string VendorId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "carrierid", modeltype: FwDataTypes.Text)]
        public string CarrierId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "carrier", modeltype: FwDataTypes.Text)]
        public string Carrier { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shipperacct", modeltype: FwDataTypes.Text)]
        public string ShipperAcct { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "primaryflg", modeltype: FwDataTypes.Boolean)]
        public bool? IsPrimary { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shipviaid", modeltype: FwDataTypes.Text)]
        public string ShipViaId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "shipvia", modeltype: FwDataTypes.Text)]
        public string ShipVia { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "datestamp", modeltype: FwDataTypes.UTCDateTime)]
        public string DateStamp { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("DealId", "dealid", select, request); 
        }
        //------------------------------------------------------------------------------------ 
    }
}