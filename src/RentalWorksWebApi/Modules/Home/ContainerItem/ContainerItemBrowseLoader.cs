using FwStandard.DataLayer; 
using FwStandard.Models; 
using FwStandard.SqlServer; 
using FwStandard.SqlServer.Attributes; 
using WebApi.Data; 
using System.Collections.Generic;
using WebLibrary;

namespace WebApi.Modules.Home.ContainerItem
{
    [FwSqlTable("rentalitemview")]
    public class ContainerItemBrowseLoader : AppDataLoadRecord
    {
        //------------------------------------------------------------------------------------ 
        public ContainerItemBrowseLoader()
        {
            AfterBrowse += OnAfterBrowse;
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rentalitemid", modeltype: FwDataTypes.Text, isPrimaryKey: true)]
        public string ItemId { get; set; } = "";
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
        [FwSqlDataField(column: "mfgserial", modeltype: FwDataTypes.Text)]
        public string SerialNumber { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "rfid", modeltype: FwDataTypes.Text)]
        public string RfId { get; set; }
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
        [FwSqlDataField(column: "containerid", modeltype: FwDataTypes.Text)]
        public string ContainerId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "containermasterid", modeltype: FwDataTypes.Text)]
        public string ContainerInventoryId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "containermasterno", modeltype: FwDataTypes.Text)]
        public string ContainerICode { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "container", modeltype: FwDataTypes.Text)]
        public string ContainerDescription { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "containerstatus", modeltype: FwDataTypes.Text)]
        public string ContainerStatus { get; set; }
        //------------------------------------------------------------------------------------ 
        //[FwSqlDataField(column: "containerstatuscolor", modeltype: FwDataTypes.OleToHtmlColor)]
        //public string ContainerStatusColor { get; set; }
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.OleToHtmlColor)]
        public string ContainerStatusColor
        {
            get { return determineContainerStatusColor(ContainerStatus); }
            set { }
        }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "containerstatusdate", modeltype: FwDataTypes.Date)]
        public string ContainerStatusDate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "containeritemid", modeltype: FwDataTypes.Text)]
        public string ContainerItemId { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(column: "inactive", modeltype: FwDataTypes.Boolean)]
        public bool? Inactive { get; set; }
        //------------------------------------------------------------------------------------ 
        protected override void SetBaseSelectQuery(FwSqlSelect select, FwSqlCommand qry, FwCustomFields customFields = null, BrowseRequest request = null)
        {
            base.SetBaseSelectQuery(select, qry, customFields, request);
            select.Parse();
            select.AddWhere("exists (select * from container c where c.rentalitemid = " + TableAlias + ".rentalitemid)");
            addFilterToSelect("InventoryId", "masterid", select, request);
            addFilterToSelect("WarehouseId", "warehouseid", select, request);
            addFilterToSelect("TrackedBy", "trackedby", select, request);
            addFilterToSelect("ContainerId", "containerid", select, request);

            AddActiveViewFieldToSelect("WarehouseId", "warehouseid", select, request);
            AddActiveViewFieldToSelect("TrackedBy", "trackedby", select, request);
        }
        //------------------------------------------------------------------------------------ 
        private string determineContainerStatusColor(string containerStatus)
        {
            return (containerStatus.Equals(RwConstants.CONTAINER_STATUS_READY) ? RwGlobals.CONTAINER_READY_COLOR : (containerStatus.Equals(RwConstants.CONTAINER_STATUS_INCOMPLETE) ? RwGlobals.CONTAINER_INCOMPLETE_COLOR : null));
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
                        row[dt.GetColumnNo("ContainerStatusColor")] = determineContainerStatusColor(row[dt.GetColumnNo("ContainerStatus")].ToString());
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------    
    }
}