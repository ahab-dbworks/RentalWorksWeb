using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.Home.Container;
using WebApi.Modules.Home.Inventory;
using WebLibrary;

namespace WebApi.Modules.Home.RentalInventory
{
    public class RentalInventoryLogic : InventoryLogic
    {
        //------------------------------------------------------------------------------------ 
        RentalInventoryLoader inventoryLoader = new RentalInventoryLoader();
        public RentalInventoryLogic()
        {
            dataLoader = inventoryLoader;
            ((InventoryBrowseLoader)browseLoader).AvailFor = RwConstants.INVENTORY_AVAILABLE_FOR_RENT;
        }
        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = base.Validate(saveMode, original, ref validateMsg);

            if (!string.IsNullOrEmpty(ContainerId))
            {
                isValid = false;
                validateMsg = "Cannot specify a Container Id when saving Rental Inventory.";
            }

            if (saveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smUpdate)
            { 
                if (original != null)
                {
                    RentalInventoryLogic lOrig = ((RentalInventoryLogic)original);

                    if (ContainerScannableInventoryId != null)
                    {
                        if (lOrig.ContainerScannableInventoryId == null)
                        {
                            lOrig.ContainerScannableInventoryId = "";
                        }
                        if (!ContainerScannableInventoryId.Equals(lOrig.ContainerScannableInventoryId))  // changing the Scannable I-Code on this Container
                        {
                            //if (lOrig.HasContainers.GetValueOrDefault(false))
                            //{
                                isValid = false;
                                validateMsg = "Cannot change the Scannable Item on this " + BusinessLogicModuleName + ".";  // because Container Bar Codes exist...
                            //}
                        }
                    }
                }
            }

            return isValid;
        }
        //------------------------------------------------------------------------------------ 
        public override void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            base.OnBeforeSave(sender, e);
            AvailFor = RwConstants.INVENTORY_AVAILABLE_FOR_RENT;

            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                if (Classification.Equals(RwConstants.ITEMCLASS_CONTAINER))
                {
                    //PackagePrice = ?
                    ContainerLogic c = new ContainerLogic();
                    c.SetDependencies(AppConfig, UserSession);
                    int i = c.SaveAsync(null, e.SqlConnection).Result;

                    this.ContainerId = c.ContainerId;
                }
            }
        }
        //------------------------------------------------------------------------------------ 
        public override void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
            base.OnAfterSave(sender, e);
            string classification = Classification;
            string containerId = ContainerId;

            if (e.Original != null)
            {
                RentalInventoryLogic orig = (RentalInventoryLogic)e.Original;
                classification = Classification ?? orig.Classification;
                containerId = ContainerId ?? orig.ContainerId;
            }

            if (classification.Equals(RwConstants.ITEMCLASS_CONTAINER))
            {
                ContainerLogic c = new ContainerLogic();
                c.SetDependencies(AppConfig, UserSession);
                c.ContainerId = containerId;
                c.ScannableInventoryId = ContainerScannableInventoryId;
                int i = c.SaveAsync(null, e.SqlConnection).Result;
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}

