using FwStandard.Data; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
using WebApi;

namespace WebApi.Modules.Inventory.Asset
{
    [FwSqlTable("rentalitemwebbrowseview")]
    public class ItemBrowseLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        public ItemBrowseLoader()
        {
            AfterBrowse += OnAfterBrowse;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public virtual string ItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterid", modeltype: FwDataTypes.Text)]
        public string InventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "masterno", modeltype: FwDataTypes.Text)]
        public string ICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "master", modeltype: FwDataTypes.Text)]
        public string Description { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string DescriptionColor
        {
            get { return getDescriptionColor(QcRequired); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "statusdate", modeltype: FwDataTypes.Date)]
        public string StatusDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalstatus", modeltype: FwDataTypes.Text)]
        public string InventoryStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "color", modeltype: FwDataTypes.OleToHtmlColor)]
        public string Color { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "textcolor", modeltype: FwDataTypes.Text)]
        public string TextColor { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "barcode", modeltype: FwDataTypes.Text)]
        public string BarCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string BarCodeColor
        {
            get { return getBarCodeColor(IsSuspend); }
            set { }
        }
        //------------------------------------------------------------------------------------
        [FwSqlDataField(column: "mfgserial", modeltype: FwDataTypes.Text)]
        public string SerialNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rfid", modeltype: FwDataTypes.Text)]
        public string RfId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "trackedby", modeltype: FwDataTypes.Text)]
        public string TrackedBy { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouseid", modeltype: FwDataTypes.Text)]
        public string WarehouseId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "warehouse", modeltype: FwDataTypes.Text)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "whcode", modeltype: FwDataTypes.Text)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "currentlocation", modeltype: FwDataTypes.Text)]
        public string CurrentLocation { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "qcrequired", modeltype: FwDataTypes.Boolean)]
        public bool? QcRequired { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "lastcontractsuspend", modeltype: FwDataTypes.Boolean)]
        public bool? IsSuspend { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            //select.AddWhere("(xxxtype = 'ABCDEF')"); 
            addFilterToSelect("InventoryId", "masterid", select, request);
            addFilterToSelect("WarehouseId", "warehouseid", select, request);
            addFilterToSelect("TrackedBy", "trackedby", select, request);

            AddActiveViewFieldToSelect("WarehouseId", "warehouseid", select, request);
            AddActiveViewFieldToSelect("TrackedBy", "trackedby", select, request);

        }
        //------------------------------------------------------------------------------------ 
        public void OnAfterBrowse(object sender, AfterBrowseEventArgs e)
        {
            if (e.DataTable != null)
            {
                FwJsonDataTable dt = e.DataTable;
                if (dt.Rows.Count > 0)
                {
                    foreach (List<object> row in dt.Rows)
                    {
                        row[dt.GetColumnNo("BarCodeColor")] = getBarCodeColor(FwConvert.ToBoolean(row[dt.GetColumnNo("IsSuspend")].ToString()));
                        row[dt.GetColumnNo("DescriptionColor")] = getDescriptionColor(FwConvert.ToBoolean(row[dt.GetColumnNo("QcRequired")].ToString()));
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------       
        private string getDescriptionColor(bool? qcRequired)
        {
            string descriptionColor = null;
            if (qcRequired.GetValueOrDefault(false))
            {
                descriptionColor = RwGlobals.QC_REQUIRED_COLOR;
            }
            return descriptionColor;
        }
        //------------------------------------------------------------------------------------ 
        private string getBarCodeColor(bool? isSuspend)
        {
            string barCodeColor = null;
            if (isSuspend.GetValueOrDefault(false))
            {
                barCodeColor = RwGlobals.SUSPEND_COLOR;
            }
            return barCodeColor;
        }
        //------------------------------------------------------------------------------------ 
    }
}