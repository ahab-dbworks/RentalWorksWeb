using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.Models;
using FwStandard.SqlServer;
using System.Collections.Generic;
using WebApi.Logic;

namespace WebApi.Modules.HomeControls.InventoryPackageInventory
{
    [FwLogic(Id: "yxgqhnhEbck7")]
    public class InventoryPackageInventoryLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        InventoryPackageInventoryRecord inventoryPackageInventory = new InventoryPackageInventoryRecord();
        InventoryPackageInventoryLoader inventoryPackageInventoryLoader = new InventoryPackageInventoryLoader();
        public InventoryPackageInventoryLogic()
        {
            dataRecords.Add(inventoryPackageInventory);
            dataLoader = inventoryPackageInventoryLoader;
            BeforeSave += OnBeforeSave;
            BeforeDelete += OnBeforeDelete;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "rlGmgpMiAAj0", IsPrimaryKey: true)]
        public string InventoryPackageInventoryId { get { return inventoryPackageInventory.InventoryPackageInventoryId; } set { inventoryPackageInventory.InventoryPackageInventoryId = value; } }

        [FwLogicProperty(Id: "aZAuFHstrnKn", IsReadOnly: true)]
        public string PackageId { get { return inventoryPackageInventory.PackageId; } set { inventoryPackageInventory.PackageId = value; } }

        [FwLogicProperty(Id: "xbbTLnI7FpiY")]
        public string InventoryId { get { return inventoryPackageInventory.InventoryId; } set { inventoryPackageInventory.InventoryId = value; } }

        [FwLogicProperty(Id: "dCtkGPLLjgPD", IsReadOnly: true)]
        public string ICode { get; set; }

        [FwLogicProperty(Id: "dCtkGPLLjgPD", IsReadOnly: true)]
        public string ICodeColor { get; set; }

        [FwLogicProperty(Id: "L0udDi4O2U73", IsReadOnly: true)]
        public string LineColor { get; set; }

        [FwLogicProperty(Id: "L9bTIhBjhr6N")]
        public string Description { get { return inventoryPackageInventory.Description; } set { inventoryPackageInventory.Description = value; } }

        [FwLogicProperty(Id: "xvbYgKdZ2HKu")]
        public bool? IsPrimary { get { return inventoryPackageInventory.IsPrimary; } set { inventoryPackageInventory.IsPrimary = value; } }

        [FwLogicProperty(Id: "c3Skk9STYIML")]
        public decimal? DefaultQuantity { get { return inventoryPackageInventory.DefaultQuantity; } set { inventoryPackageInventory.DefaultQuantity = value; } }

        [FwLogicProperty(Id: "TgmUarD5X2Sl")]
        public bool? IsOption { get { return inventoryPackageInventory.IsOption; } set { inventoryPackageInventory.IsOption = value; } }

        [FwLogicProperty(Id: "sacV6ooCwAsJ")]
        public bool? Charge { get { return inventoryPackageInventory.Charge; } set { inventoryPackageInventory.Charge = value; } }

        [FwLogicProperty(Id: "knTBi4qo5KgB")]
        public bool? IsRequired { get { return inventoryPackageInventory.IsRequired; } set { inventoryPackageInventory.IsRequired = value; } }

        [FwLogicProperty(Id: "K6MSEinNBbdq", IsReadOnly: true)]
        public string OptionColor { get; set; }

        [FwLogicProperty(Id: "RhcvftoF0jE2", IsReadOnly: true)]
        public string ItemClass { get; set; }

        [FwLogicProperty(Id: "uN9xvyk9KeUc", IsReadOnly: true)]
        public string ItemTrackedBy { get; set; }

        [FwLogicProperty(Id: "XLrjraRt1orM", IsReadOnly: true)]
        public string AvailFor { get; set; }

        [FwLogicProperty(Id: "Oz4z7azl9H8p", IsReadOnly: true)]
        public string AvailFrom { get; set; }

        [FwLogicProperty(Id: "iRWP9Kc9tzh8", IsReadOnly: true)]
        public decimal? CategoryOrderBy { get; set; }

        [FwLogicProperty(Id: "LgvkLAhaLGNH")]
        public int? OrderBy { get { return inventoryPackageInventory.OrderBy; } set { inventoryPackageInventory.OrderBy = value; } }

        [FwLogicProperty(Id: "0Ad2wjKYSCCj")]
        public int? ItemColor { get { return inventoryPackageInventory.ItemColor; } set { inventoryPackageInventory.ItemColor = value; } }

        [FwLogicProperty(Id: "vxNJdF9imoMc", IsReadOnly: true)]
        public bool? IsNestedComplete { get; set; }

        [FwLogicProperty(Id: "C0GIIpQG95Yti")]
        public bool? Inactive { get { return inventoryPackageInventory.Inactive; } set { inventoryPackageInventory.Inactive = value; } }

        [FwLogicProperty(Id: "Ax1iKPLAqQpX", IsReadOnly: true)]
        public string ItemInactive { get; set; }

        [FwLogicProperty(Id: "g7k0UsAoBDz2")]
        public string WarehouseId { get { return inventoryPackageInventory.WarehouseId; } set { inventoryPackageInventory.WarehouseId = value; } }

        [FwLogicProperty(Id: "ZDrk5oiym8hO")]
        public string ParentId { get { return inventoryPackageInventory.ParentId; } set { inventoryPackageInventory.ParentId = value; } }

        [FwLogicProperty(Id: "RhcvftoF0jE2", IsReadOnly: true)]
        public string PackageItemClass { get; set; }

        [FwLogicProperty(Id: "7Tv9W0v8pL7T", IsReadOnly: true)]
        public bool? ItemNonDiscountable { get; set; }

        [FwLogicProperty(Id: "rlGmgpMiAAj0", IsReadOnly: true)]
        public string PrimaryInventoryId { get; set; }

        [FwLogicProperty(Id: "n8jHKGRMX5Ep", IsReadOnly: true)]
        public string UnitId { get; set; }

        [FwLogicProperty(Id: "xWu6Lpbd2IFU", IsReadOnly: true)]
        public decimal? DailyRate { get; set; }

        [FwLogicProperty(Id: "5sivjlkTE8uj", IsReadOnly: true)]
        public decimal? WeeklyRate { get; set; }

        [FwLogicProperty(Id: "dE3z1QedlNKm", IsReadOnly: true)]
        public decimal? MonthlyRate { get; set; }

        [FwLogicProperty(Id: "UZG6tS55HmCK")]
        public string DateStamp { get { return inventoryPackageInventory.DateStamp; } set { inventoryPackageInventory.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;

            if (saveMode == TDataRecordSaveMode.smUpdate)
            {
                if (((InventoryPackageInventoryLogic)original).IsPrimary.GetValueOrDefault(false))
                {
                    isValid = false;
                    validateMsg = "Cannot modify the Primary item.";
                }
            }

            return isValid;
        }
        //------------------------------------------------------------------------------------ 
        public virtual void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                FwSqlConnection conn = e.SqlConnection;
                if (conn == null)
                {
                    conn = new FwSqlConnection(this.AppConfig.DatabaseSettings.ConnectionString);
                }
                string invClassification = FwSqlCommand.GetStringDataAsync(conn, AppConfig.DatabaseSettings.QueryTimeout, "master", "masterid", PackageId, "class").Result;
                
                if (invClassification.Equals(RwConstants.INVENTORY_CLASSIFICATION_COMPLETE))
                {
                    BrowseRequest req = new BrowseRequest();
                    InventoryPackageInventoryLogic p = new InventoryPackageInventoryLogic();
                    p.AppConfig = this.AppConfig;
                    req.uniqueids = new Dictionary<string, object>();
                    req.uniqueids.Add("PackageId", this.PackageId);
                    List<InventoryPackageInventoryLogic> inv = p.SelectAsync<InventoryPackageInventoryLogic>(req).Result;

                    if (inv.Count.Equals(0))
                    {
                        IsPrimary = true;
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------
        public void OnBeforeDelete(object sender, BeforeDeleteEventArgs e)
        {
            if (IsPrimary.GetValueOrDefault(false))
            {
                e.PerformDelete = false;
                e.ErrorMessage = "Primary Item cannot be deleted.";
            }
        }
        //------------------------------------------------------------------------------------

    }
}
