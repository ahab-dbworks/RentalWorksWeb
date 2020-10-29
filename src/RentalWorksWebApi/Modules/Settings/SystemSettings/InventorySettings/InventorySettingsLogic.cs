using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.Administrator.Control;

namespace WebApi.Modules.Settings.SystemSettings.InventorySettings
{
    [FwLogic(Id: "9tcMsqDFxmN11")]
    public class InventorySettingsLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        SysControlRecord sysControl = new SysControlRecord();
        InventorySettingsLoader inventorySettingsLoader = new InventorySettingsLoader();

        //------------------------------------------------------------------------------------ 
        public InventorySettingsLogic()
        {
            dataRecords.Add(sysControl);
            dataLoader = inventorySettingsLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "GRycCnSNyPO18", IsPrimaryKey: true)]
        public string InventorySettingsId { get { return sysControl.ControlId; } set { sysControl.ControlId = value; } }

        [FwLogicProperty(Id: "oUiejSCAshZ26", IsRecordTitle: true)]
        public string InventorySettingsName { get { return "Inventory Settings"; } }

        [FwLogicProperty(Id: "d6hQNE6E9Sxag")]
        public string ICodeMask { get { return sysControl.Invmask; } set { sysControl.Invmask = value; } }

        [FwLogicProperty(Id: "v007HOEK0GyW4")]
        public bool? UserAssignedICodes { get { return sysControl.Userassignmasterno; } set { sysControl.Userassignmasterno = value; } }

        [FwLogicProperty(Id: "FTFg0VWQ9Iy6y")]
        public int? LastICode { get { return sysControl.Masterno; } set { sysControl.Masterno = value; } }

        [FwLogicProperty(Id: "ntSppR4GkjOc3")]
        public string ICodePrefix { get { return sysControl.Icodeprefix; } set { sysControl.Icodeprefix = value; } }

        [FwLogicProperty(Id: "yzYEXi36QjGvF")]
        public bool? Enable3WeekPricing { get { return sysControl.Enable3WeekPricing; } set { sysControl.Enable3WeekPricing = value; } }

        [FwLogicProperty(Id: "WiSc1DOZ71ece")]
        public string SalesCheckOutRetiredReasonId { get { return sysControl.SalesCheckOutRetiredReasonId; } set { sysControl.SalesCheckOutRetiredReasonId = value; } }

        [FwLogicProperty(Id: "DaBzieb9GHfz4", IsReadOnly: true)]
        public string SalesCheckOutRetiredReason { get; set; }

        [FwLogicProperty(Id: "R8E0vYoDXddCz")]
        public string SalesCheckInUnretiredReasonId { get { return sysControl.SalesCheckInUnretiredReasonId; } set { sysControl.SalesCheckInUnretiredReasonId = value; } }

        [FwLogicProperty(Id: "2Q3BFsIa7kqz4", IsReadOnly: true)]
        public string SalesCheckInUnretiredReason { get; set; }

        [FwLogicProperty(Id: "XtEwwCdWQQ76s")]
        public string DefaultRentalSaleRetiredReasonId { get { return sysControl.DefaultRentalSaleRetiredReasonId; } set { sysControl.DefaultRentalSaleRetiredReasonId = value; } }

        [FwLogicProperty(Id: "RRl022PnhCz5j", IsReadOnly: true)]
        public string DefaultRentalSaleRetiredReason { get; set; }

        [FwLogicProperty(Id: "EDJa0L7okJrCj")]
        public string DefaultLossAndDamageRetiredReasonId { get { return sysControl.DefaultLossAndDamageRetiredReasonId; } set { sysControl.DefaultLossAndDamageRetiredReasonId = value; } }

        [FwLogicProperty(Id: "g2a8lhg3PRpoS", IsReadOnly: true)]
        public string DefaultLossAndDamageRetiredReason { get; set; }

        [FwLogicProperty(Id: "PY0Na99fW6CMj")]
        public bool? StartDepreciatingFixedAssetsTheMonthAfterTheyAreReceived { get { return sysControl.StartDepreciatingFixedAssetsTheMonthAfterTheyAreReceived; } set { sysControl.StartDepreciatingFixedAssetsTheMonthAfterTheyAreReceived = value; } }

        [FwLogicProperty(Id: "l5fLvzCpHrDf")]
        public string DefaultRentalQuantityInventoryCostCalculation { get { return sysControl.DefaultRentalQuantityInventoryCostCalculation; } set { sysControl.DefaultRentalQuantityInventoryCostCalculation = value; } }

        [FwLogicProperty(Id: "P3zXTZxunprL")]
        public string DefaultSalesQuantityInventoryCostCalculation { get { return sysControl.DefaultSalesQuantityInventoryCostCalculation; } set { sysControl.DefaultSalesQuantityInventoryCostCalculation = value; } }

        [FwLogicProperty(Id: "LwSmXksF727e")]
        public string DefaultPartsQuantityInventoryCostCalculation { get { return sysControl.DefaultPartsQuantityInventoryCostCalculation; } set { sysControl.DefaultPartsQuantityInventoryCostCalculation = value; } }

        [FwLogicProperty(Id: "of86azr3dnOr")]
        public bool? EnableConsignment { get { return sysControl.EnableConsignment; } set { sysControl.EnableConsignment = value; } }

        [FwLogicProperty(Id: "rwEZZ44IiF6U")]
        public bool? EnableLease { get { return sysControl.EnableLease; } set { sysControl.EnableLease = value; } }

        [FwLogicProperty(Id: "E0PTF3DZXRoNX")]
        public string DateStamp { get { return sysControl.DateStamp; } set { sysControl.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;

            if (saveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                isValid = false;
                validateMsg = "Cannot add new records to " + this.BusinessLogicModuleName;
            }
            //else // dmUpdate
            //{
            //    InventorySettingsLogic orig = (InventorySettingsLogic)original;
            //
            //    if (RentalQuantityInventoryValueMethod != null)
            //    {
            //        if (!RentalQuantityInventoryValueMethod.Equals(orig.RentalQuantityInventoryValueMethod))
            //        {
            //            bool purchasesExist = AppFunc.DataExistsAsync(AppConfig, "purchasewebview", new string[] { "availfor" }, new string[] { RwConstants.INVENTORY_AVAILABLE_FOR_RENT }).Result;
            //            if (purchasesExist)
            //            {
            //                isValid = false;
            //                validateMsg = "Cannot change the Rental Quantity Inventory Valuation Method once Rental Inventory has been purchased.";
            //            }
            //        }
            //    }
            //
            //
            //    if (SalesQuantityInventoryValueMethod != null)
            //    {
            //        if (!SalesQuantityInventoryValueMethod.Equals(orig.SalesQuantityInventoryValueMethod))
            //        {
            //            bool purchasesExist = AppFunc.DataExistsAsync(AppConfig, "purchasewebview", new string[] { "availfor" }, new string[] { RwConstants.INVENTORY_AVAILABLE_FOR_SALE }).Result;
            //            if (purchasesExist)
            //            {
            //                isValid = false;
            //                validateMsg = "Cannot change the Sales Quantity Inventory Valuation Method once Sales Inventory has been purchased.";
            //            }
            //        }
            //    }
            //
            //    if (PartsQuantityInventoryValueMethod != null)
            //    {
            //        if (!PartsQuantityInventoryValueMethod.Equals(orig.PartsQuantityInventoryValueMethod))
            //        {
            //            bool purchasesExist = AppFunc.DataExistsAsync(AppConfig, "purchasewebview", new string[] { "availfor" }, new string[] { RwConstants.INVENTORY_AVAILABLE_FOR_PARTS }).Result;
            //            if (purchasesExist)
            //            {
            //                isValid = false;
            //                validateMsg = "Cannot change the Parts Quantity Inventory Valuation Method once Parts Inventory has been purchased.";
            //            }
            //        }
            //    }
            //
            //}


            return isValid;
        }
        //------------------------------------------------------------------------------------    
    }
}
