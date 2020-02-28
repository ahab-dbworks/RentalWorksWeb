using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.Models;
using FwStandard.SqlServer;
using System.Collections.Generic;
using WebApi.Modules.Containers.Container;
using WebApi.Modules.HomeControls.ContainerItem;
using WebApi.Modules.HomeControls.Inventory;
using WebApi;

namespace WebApi.Modules.Inventory.RentalInventory
{
    public class RentalInventoryLogic : InventoryLogic
    {
        //------------------------------------------------------------------------------------ 
        RentalInventoryLoader inventoryLoader = new RentalInventoryLoader();
        public RentalInventoryLogic()
        {
            dataLoader = inventoryLoader;
            ((InventoryBrowseLoader)browseLoader).AvailFor = RwConstants.INVENTORY_AVAILABLE_FOR_RENT;
            ForceSave = true;  //justin hoffman 12/29/2019
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "EMtstFgQO6Apj")]
        public bool? ExcludeFromReturnOnAsset { get { return master.ExcludeFromReturnOnAsset; } set { master.ExcludeFromReturnOnAsset = value; } }



        // for cusomizing browse 
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "4uavj41MT821z", IsReadOnly: true)]
        public decimal? DailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "m4WEKzqJQ2d4Y", IsReadOnly: true)]
        public decimal? WeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "dn4W1P4WmTBRV", IsReadOnly: true)]
        public decimal? Week2Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "P63A3BnGTm7Cl", IsReadOnly: true)]
        public decimal? Week3Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "Qg6UVsl5AOxtP", IsReadOnly: true)]
        public decimal? Week4Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "qNHW0afj0g9QY", IsReadOnly: true)]
        public decimal? MonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "UNA3nQeCQUBF1", IsReadOnly: true)]
        public decimal? UnitValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "25fptZQprTbeV", IsReadOnly: true)]
        public decimal? ReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 


        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = base.Validate(saveMode, original, ref validateMsg);

            RentalInventoryLogic lOrig = null;
            if (original != null)
            {
                lOrig = ((RentalInventoryLogic)original);
            }

            if (!string.IsNullOrEmpty(ContainerId))
            {
                isValid = false;
                validateMsg = "Cannot specify a Container Id when saving Rental Inventory.";
            }

            if (saveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smUpdate)
            {
                if (lOrig != null)
                {
                    if (ContainerScannableInventoryId != null)  //attempting to change the scannable i-code
                    {
                        if ((!lOrig.ContainerScannableInventoryId.Equals(string.Empty)) && (!ContainerScannableInventoryId.Equals(lOrig.ContainerScannableInventoryId)))  // prior value was not blank, and also changing the value at this time
                        {
                            // check to see if any Container bar codes are instantiated yet on this Container definition
                            BrowseRequest br = new BrowseRequest();
                            br.uniqueids = new Dictionary<string, object>();
                            br.uniqueids.Add("ContainerId", lOrig.ContainerId);
                            ContainerItemLogic cil = new ContainerItemLogic();
                            cil.SetDependencies(AppConfig, UserSession);
                            FwJsonDataTable dt = cil.BrowseAsync(br).Result;

                            bool hasContainerBarCodes = (dt.Rows.Count > 0);
                            if (hasContainerBarCodes)
                            {
                                isValid = false;
                                validateMsg = "Cannot change the Scannable Item on this Container because Container Bar Codes exist."; 
                            }
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

