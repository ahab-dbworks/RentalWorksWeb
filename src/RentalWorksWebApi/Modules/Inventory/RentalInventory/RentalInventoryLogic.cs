using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.Models;
using FwStandard.SqlServer;
using System.Collections.Generic;
using WebApi.Modules.Containers.Container;
using WebApi.Modules.HomeControls.ContainerItem;
using WebApi.Modules.HomeControls.Inventory;
using WebApi;
using FwStandard.SqlServer.Attributes;

namespace WebApi.Modules.Inventory.RentalInventory
{
    public class RentalInventoryLogic : InventoryLogic
    {
        //------------------------------------------------------------------------------------ 
        RentalInventoryLoader inventoryLoader = new RentalInventoryLoader();
        public RentalInventoryLogic()
        {
            dataLoader = inventoryLoader;
            //((InventoryBrowseLoader)browseLoader).AvailFor = RwConstants.INVENTORY_AVAILABLE_FOR_RENT;
            ((InventoryLoader)dataLoader).AvailFor = RwConstants.INVENTORY_AVAILABLE_FOR_RENT;
            AvailFor = RwConstants.INVENTORY_AVAILABLE_FOR_RENT;
            ForceSave = true;  //justin hoffman 12/29/2019
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "EMtstFgQO6Apj")]
        public bool? ExcludeFromReturnOnAsset { get { return master.ExcludeFromReturnOnAsset; } set { master.ExcludeFromReturnOnAsset = value; } }

        [FwLogicProperty(Id: "En3Gom0JH00QP")]
        public bool? IsFixedAsset { get { return master.IsFixedAsset; } set { master.IsFixedAsset = value; } }

        [FwLogicProperty(Id: "fTPSSNiM1sPUu")]
        public bool? MultiAssignRFIDs { get { return master.MultiAssignRFIDs; } set { master.MultiAssignRFIDs = value; } }


        //set/wall
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "75MyM14LdkPlR")]
        public string SetOpeningId { get { return master.SetOpeningId; } set { master.SetOpeningId = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "4KRtq3eIXEBIZ", IsReadOnly: true)]
        public string SetOpening { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "IpK5TJo7JhCx6")]
        public string WallTypeId { get { return master.WallTypeId; } set { master.WallTypeId = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "L6tPPjBy2C3O7", IsReadOnly: true)]
        public string WallType { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "Vjkz8rPVpslfR")]
        public string SetSurfaceId { get { return master.SetSurfaceId; } set { master.SetSurfaceId = value; } }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "HbO97rimlK10l", IsReadOnly: true)]
        public string SetSurface { get; set; }
        //------------------------------------------------------------------------------------ 

        [FwLogicProperty(Id: "i7M6ls3sIfXd")]
        public int? WallWidthFt { get { return primaryDimension.WidthFt; } set { primaryDimension.WidthFt = value; } }

        [FwLogicProperty(Id: "YxFp5SoHqEwK")]
        public int? WallWidthIn { get { return primaryDimension.WidthIn; } set { primaryDimension.WidthIn = value; } }

        [FwLogicProperty(Id: "VbpLzz0ZjAP5")]
        public int? WallHeightFt { get { return primaryDimension.HeightFt; } set { primaryDimension.HeightFt = value; } }

        [FwLogicProperty(Id: "kuoyDXtfuSwN")]
        public int? WallHeightIn { get { return primaryDimension.HeightIn; } set { primaryDimension.HeightIn = value; } }

        [FwLogicProperty(Id: "x6sLTfuTnTkO")]
        public int? WallLengthFt { get { return primaryDimension.LengthFt; } set { primaryDimension.LengthFt = value; } }

        [FwLogicProperty(Id: "nibxo6MVaiWt")]
        public int? WallLengthIn { get { return primaryDimension.LengthIn; } set { primaryDimension.LengthIn = value; } }



        // for cusomizing browse 
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Decimal)]
        public decimal? DailyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Decimal)]
        public decimal? WeeklyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Decimal)]
        public decimal? Week2Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Decimal)]
        public decimal? Week3Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Decimal)]
        public decimal? Week4Rate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Decimal)]
        public decimal? MonthlyRate { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Decimal)]
        public decimal? UnitValue { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwSqlDataField(calculatedColumnSql: "null", modeltype: FwDataTypes.Decimal)]
        public decimal? ReplacementCost { get; set; }
        //------------------------------------------------------------------------------------ 


        //------------------------------------------------------------------------------------ 
        protected override void SetDefaultAvailFor()
        {
            AvailFor = RwConstants.INVENTORY_AVAILABLE_FOR_RENT;
        }
        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = base.Validate(saveMode, original, ref validateMsg);

            RentalInventoryLogic lOrig = null;
            if (original != null)
            {
                lOrig = ((RentalInventoryLogic)original);
            }

            if (isValid)
            {
                if (!string.IsNullOrEmpty(ContainerId))
                {
                    isValid = false;
                    validateMsg = "Cannot specify a Container Id when saving Rental Inventory.";
                }
            }

            if (isValid)
            {
                bool isFixedAsset = false;
                string classification = string.Empty;
                if (saveMode.Equals(TDataRecordSaveMode.smInsert))
                {
                    isFixedAsset = IsFixedAsset.GetValueOrDefault(false);
                    classification = Classification;
                }
                else if ((saveMode.Equals(TDataRecordSaveMode.smUpdate)) && (lOrig != null))
                {
                    isFixedAsset = IsFixedAsset ?? lOrig.IsFixedAsset.GetValueOrDefault(false);
                    classification = Classification ?? lOrig.Classification;
                }

                if ((isFixedAsset) && (!(classification.Equals(RwConstants.INVENTORY_CLASSIFICATION_ITEM) || classification.Equals(RwConstants.INVENTORY_CLASSIFICATION_ACCESSORY))))
                {
                    isValid = false;
                    validateMsg = "Only Items or Accessories can be Fixed Assets.";
                }
            }

            if (isValid)
            {
                if ((saveMode.Equals(TDataRecordSaveMode.smUpdate)) && (lOrig != null))
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

