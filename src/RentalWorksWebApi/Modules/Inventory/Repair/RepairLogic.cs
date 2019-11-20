using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using System;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.HomeControls.InventoryAvailability;
using WebApi.Modules.HomeControls.RepairItem;
using WebApi.Modules.HomeControls.Tax;
using WebApi;

namespace WebApi.Modules.Inventory.Repair
{
    [FwLogic(Id: "eQEPZrRi01LzW")]
    public class RepairLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        RepairRecord repair = new RepairRecord();
        TaxRecord tax = new TaxRecord();
        RepairLoader repairLoader = new RepairLoader();
        RepairBrowseLoader repairBrowseLoader = new RepairBrowseLoader();

        private string tmpTaxId = "";

        public RepairLogic()
        {
            dataRecords.Add(repair);
            dataRecords.Add(tax);
            dataLoader = repairLoader;
            browseLoader = repairBrowseLoader;

            repair.BeforeSave += OnBeforeSaveRepair;
            repair.AfterSave += OnAfterSaveRepair;

            tax.AssignPrimaryKeys += TaxAssignPrimaryKeys;
            tax.AfterSave += OnAfterSaveTax;
            UseTransactionToSave = true;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "mD3GkEIjJcjlt", IsPrimaryKey: true)]
        public string RepairId { get { return repair.RepairId; } set { repair.RepairId = value; } }

        [FwLogicProperty(Id: "PW1aZC7K768Z", DisableDirectModify: true)]
        public string LocationId { get { return repair.LocationId; } set { repair.LocationId = value; } }

        [FwLogicProperty(Id: "gM13Bzg7LAand", IsReadOnly: true)]
        public string Location { get; set; }

        [FwLogicProperty(Id: "XZLC4i4T7cZ1")]
        public string BillingLocationId { get { return repair.BillingLocationId; } set { repair.BillingLocationId = value; } }

        [FwLogicProperty(Id: "qQVibe5BCbKDw", IsReadOnly: true)]
        public string BillingLocation { get; set; }

        [FwLogicProperty(Id: "xhKzfNoDKAdj", DisableDirectModify: true)]
        public string WarehouseId { get { return repair.WarehouseId; } set { repair.WarehouseId = value; } }

        [FwLogicProperty(Id: "7q34E1LMVlwM2", IsReadOnly: true)]
        public string ItemWarehouseId { get; set; }

        [FwLogicProperty(Id: "U9VuLeZofY9ow", IsReadOnly: true)]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id: "U9VuLeZofY9ow", IsReadOnly: true)]
        public string WarehouseCode { get; set; }

        [FwLogicProperty(Id: "e9G3UV2AaJaw")]
        public string BillingWarehouseId { get { return repair.BillingWarehouseId; } set { repair.BillingWarehouseId = value; } }

        [FwLogicProperty(Id: "uUysNzdfUCio5", IsReadOnly: true)]
        public string BillingWarehouse { get; set; }

        [FwLogicProperty(Id: "Klsqbpd9hLvt")]
        public string DepartmentId { get { return repair.DepartmentId; } set { repair.DepartmentId = value; } }

        [FwLogicProperty(Id: "fmet9QfVZ71yH", IsReadOnly: true)]
        public string Department { get; set; }

        [FwLogicProperty(Id: "5Pe7MAe4ydezV", IsReadOnly: true)]
        public string InventoryTypeId { get; set; }

        [FwLogicProperty(Id: "5Pe7MAe4ydezV", IsReadOnly: true)]
        public string InventoryType { get; set; }

        [FwLogicProperty(Id: "ToBTTYQUnNZE")]
        public bool? PendingRepair { get { return repair.PendingRepair; } set { repair.PendingRepair = value; } }

        [FwLogicProperty(Id: "QWRGXatCbG6fh", IsRecordTitle: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public string RepairNumber { get { return repair.RepairNumber; } set { repair.RepairNumber = value; } }

        [FwLogicProperty(Id: "QWRGXatCbG6fh", IsReadOnly: true)]
        public string RepairNumberColor { get; set; }

        [FwLogicProperty(Id: "wSax3slfYElkq", IsReadOnly: true)]
        public string RepairDate { get { return repair.RepairDate; } set { repair.RepairDate = value; } }

        [FwLogicProperty(Id: "8vz9Y16dL0L7")]
        public bool? OutsideRepair { get { return repair.OutsideRepair; } set { repair.OutsideRepair = value; } }

        [FwLogicProperty(Id: "2fMiHJ1D3LBdc", IsReadOnly: true)]
        public string OutsideRepairPoNumber { get; set; }

        [FwLogicProperty(Id: "jhQu8TxBrZxki", IsReadOnly: true)]
        public string ItemId { get; set; }

        [FwLogicProperty(Id: "Nn3y6NWfMy7Ue", IsReadOnly: true)]
        public string BarCode { get; set; }

        [FwLogicProperty(Id: "MNnQ2k8XeJHCI", IsReadOnly: true)]
        public string SerialNumber { get; set; }

        [FwLogicProperty(Id: "w8v4Vccf7BmdS", IsReadOnly: true)]
        public string RfId { get; set; }

        [FwLogicProperty(Id: "Nn3y6NWfMy7Ue", IsReadOnly: true)]
        public string BarCodeColor { get; set; }

        [FwLogicProperty(Id: "2NTKfwOlDBqv")]
        public string InventoryId { get { return repair.InventoryId; } set { repair.InventoryId = value; } }

        [FwLogicProperty(Id: "iE29lhAK4GVi2", IsReadOnly: true)]
        public string AvailFor { get; set; }

        [FwLogicProperty(Id: "iE29lhAK4GVi2", IsReadOnly: true)]
        public string AvailForDisplay { get; set; }

        [FwLogicProperty(Id: "Yp4gljsVCsqUB", IsReadOnly: true)]
        public string ICode { get; set; }

        [FwLogicProperty(Id: "Yp4gljsVCsqUB", IsReadOnly: true)]
        public string ICodeColor { get; set; }

        [FwLogicProperty(Id: "pxcaM2UGEmS7s", IsReadOnly: true)]
        public string ItemDescription { get { return repair.ItemDescription; } set { repair.ItemDescription = value; } }

        [FwLogicProperty(Id: "cujeKfMv8h88", DisableDirectModify: true)]
        public int? Quantity { get { return repair.Quantity; } set { repair.Quantity = value; } }

        [FwLogicProperty(Id: "AaD37MCZAdWrB", IsReadOnly: true)]
        public string QuantityColor { get; set; }

        [FwLogicProperty(Id: "KgYqHIsiGpEP")]
        public string DamageDealId { get { return repair.DamageDealId; } set { repair.DamageDealId = value; } }

        [FwLogicProperty(Id: "0CdtPTkIudcIv", IsReadOnly: true)]
        public string DamageDeal { get; set; }

        [FwLogicProperty(Id: "0CdtPTkIudcIv", IsReadOnly: true)]
        public string DamageDealColor { get; set; }

        [FwLogicProperty(Id: "XXMO3UIFjWT1")]
        public string DamageOrderId { get { return repair.DamageOrderId; } set { repair.DamageOrderId = value; } }

        [FwLogicProperty(Id: "0CdtPTkIudcIv", IsReadOnly: true)]
        public string DamageOrderNumber { get; set; }

        [FwLogicProperty(Id: "0CdtPTkIudcIv", IsReadOnly: true)]
        public string DamageOrderDescription { get; set; }

        [FwLogicProperty(Id: "UkOYhmfJQiXw")]
        public string DamageContractId { get { return repair.DamageContractId; } set { repair.DamageContractId = value; } }

        [FwLogicProperty(Id: "0CdtPTkIudcIv", IsReadOnly: true)]
        public string DamageContractNumber { get; set; }

        [FwLogicProperty(Id: "0CdtPTkIudcIv", IsReadOnly: true)]
        public string DamageContractDate { get; set; }


        [FwLogicProperty(Id: "0nj5Sn8BL2Qa")]
        public string DamageScannedById { get { return repair.DamageScannedById; } set { repair.DamageScannedById = value; } }

        [FwLogicProperty(Id: "0CdtPTkIudcIv", IsReadOnly: true)]
        public string DamageScannedBy { get; set; }


        [FwLogicProperty(Id: "vxeQn1bPPPHf")]
        public string LossAndDamageOrderId { get { return repair.LossAndDamageOrderId; } set { repair.LossAndDamageOrderId = value; } }

        [FwLogicProperty(Id: "0CdtPTkIudcIv", IsReadOnly: true)]
        public string LossAndDamageOrderNumber { get; set; }

        [FwLogicProperty(Id: "0CdtPTkIudcIv", IsReadOnly: true)]
        public string LossAndDamageOrderDescription { get; set; }

        [FwLogicProperty(Id: "alcyA7N2tOdR")]
        public string ChargeOrderId { get { return repair.ChargeOrderId; } set { repair.ChargeOrderId = value; } }

        [FwLogicProperty(Id: "wsLiVL8YCJkmz", IsReadOnly: true)]
        public string ChargeOrderNumber { get; set; }

        [FwLogicProperty(Id: "dGQoc0psmkHU2", IsReadOnly: true)]
        public string ChargeOrderDescription { get; set; }

        [FwLogicProperty(Id: "UrugZmDw9KkgV", IsReadOnly: true)]
        public string ChargeInvoiceId { get; set; }

        [FwLogicProperty(Id: "ERfYjekONDwjP", IsReadOnly: true)]
        public string ChargeInvoiceNumber { get; set; }

        [FwLogicProperty(Id: "zjm001TpOyfr", IsReadOnly: true)]
        public string ChargeInvoiceDescription { get; set; }



        [FwLogicProperty(Id: "HO9l1ejOjqXR")]
        public string TaxOptionId { get { return tax.TaxOptionId; } set { tax.TaxOptionId = value; } }

        [FwLogicProperty(Id: "FDlNVc3vLrWba", IsReadOnly: true)]
        public string TaxOption { get; set; }



        [FwLogicProperty(Id: "qtqhN6tF6Wy4", DisableDirectAssign: true, DisableDirectModify: true)]
        public string TaxId { get { return repair.TaxId; } set { repair.TaxId = value; } }

        [FwLogicProperty(Id: "vltcbayDnlgb")]
        public decimal? RentalTaxRate1 { get { return tax.RentalTaxRate1; } set { tax.RentalTaxRate1 = value; } }

        [FwLogicProperty(Id: "48a5oNfTvqPI")]
        public decimal? SalesTaxRate1 { get { return tax.SalesTaxRate1; } set { tax.SalesTaxRate1 = value; } }

        [FwLogicProperty(Id: "p5qOVJkHXZPE")]
        public decimal? LaborTaxRate1 { get { return tax.LaborTaxRate1; } set { tax.LaborTaxRate1 = value; } }

        [FwLogicProperty(Id: "fVgitcpaSHGC")]
        public decimal? RentalTaxRate2 { get { return tax.RentalTaxRate2; } set { tax.RentalTaxRate2 = value; } }

        [FwLogicProperty(Id: "oZPKEZRZv4cI")]
        public decimal? SalesTaxRate2 { get { return tax.SalesTaxRate2; } set { tax.SalesTaxRate2 = value; } }

        [FwLogicProperty(Id: "kNlVTpBVGeft")]
        public decimal? LaborTaxRate2 { get { return tax.LaborTaxRate2; } set { tax.LaborTaxRate2 = value; } }


        [FwLogicProperty(Id: "7gBhhI2LZZSZ", DisableDirectAssign: true, DisableDirectModify: true)]
        public string Status { get { return repair.Status; } set { repair.Status = value; } }

        [FwLogicProperty(Id: "xFJ3ZADD2bQ8n", IsReadOnly: true)]
        public string StatusColor { get; set; }

        [FwLogicProperty(Id: "fxjvsvnm47Pp", DisableDirectAssign: true, DisableDirectModify: true)]
        public string StatusDate { get { return repair.StatusDate; } set { repair.StatusDate = value; } }

        [FwLogicProperty(Id: "47EzD22tiJRH")]
        public bool? Billable { get { return repair.Billable; } set { repair.Billable = value; } }

        [FwLogicProperty(Id: "QQStv6rW4I5ya", IsReadOnly: true)]
        public string BillableDisplay { get; set; }

        [FwLogicProperty(Id: "2POLShGKSuzuU", IsReadOnly: true)]
        public bool? NotBilled { get; set; }

        [FwLogicProperty(Id: "nW0oQ6g6nYMP")]
        public string Priority { get { return repair.Priority; } set { repair.Priority = value; } }

        [FwLogicProperty(Id: "xE6Ea4j4Zk7es", IsReadOnly: true)]
        public string PriorityDescription { get; set; }

        [FwLogicProperty(Id: "QlQrje6jVNpI")]
        public string PriorityColor { get; set; }

        [FwLogicProperty(Id: "Y4eEMAM9iP2S")]
        public string RepairType { get { return repair.RepairType; } set { repair.RepairType = value; } }

        [FwLogicProperty(Id: "gXVmyyn4Xlpx")]
        public bool? PoPending { get { return repair.PoPending; } set { repair.PoPending = value; } }

        [FwLogicProperty(Id: "NIcbhQGJd9Dn")]
        public string PoNumber { get { return repair.PoNumber; } set { repair.PoNumber = value; } }

        [FwLogicProperty(Id: "PJ0jEXCv5BxJ")]
        public string Damage { get { return repair.Damage; } set { repair.Damage = value; } }

        [FwLogicProperty(Id: "fQS7qSHiQaff")]
        public string Correction { get { return repair.Correction; } set { repair.Correction = value; } }

        [FwLogicProperty(Id: "azWydrWk4ItAW", IsReadOnly: true)]
        public bool? Released { get; set; }

        [FwLogicProperty(Id: "AaD37MCZAdWrB", IsReadOnly: true)]
        public decimal? ReleasedQuantity { get; set; }

        [FwLogicProperty(Id: "fl7PO8fhDrW7")]
        public string TransferId { get { return repair.TransferId; } set { repair.TransferId = value; } }

        [FwLogicProperty(Id: "4XA6LePF7djx1", IsReadOnly: true)]
        public string TransferredFromWarehouseId { get; set; }

        [FwLogicProperty(Id: "qgrmpM970GY2")]
        public string DueDate { get { return repair.DueDate; } set { repair.DueDate = value; } }

        [FwLogicProperty(Id: "HIGUSAxOZhOBm", IsReadOnly: true)]
        public string EstimateByUserId { get; set; }

        [FwLogicProperty(Id: "HIGUSAxOZhOBm", IsReadOnly: true)]
        public string EstimateBy { get; set; }

        [FwLogicProperty(Id: "3zPBvLv1jjHNf", IsReadOnly: true)]
        public string EstimateDate { get; set; }

        [FwLogicProperty(Id: "cAdqIY19xpl7q", IsReadOnly: true)]
        public string CompleteByUserId { get; set; }

        [FwLogicProperty(Id: "cAdqIY19xpl7q", IsReadOnly: true)]
        public string CompleteBy { get; set; }

        [FwLogicProperty(Id: "ZkciVYs6kaYpg", IsReadOnly: true)]
        public string CompleteDate { get; set; }

        [FwLogicProperty(Id: "TzVNoAJt0SQu")]
        public string InputDate { get { return repair.InputDate; } set { repair.InputDate = value; } }

        [FwLogicProperty(Id: "lO1hQfPUDV8g")]
        public string InputByUserId { get { return repair.InputByUserId; } set { repair.InputByUserId = value; } }

        [FwLogicProperty(Id: "yzJCkR3FnNtX")]
        public string RepairItemStatusId { get { return repair.RepairItemStatusId; } set { repair.RepairItemStatusId = value; } }

        [FwLogicProperty(Id: "0V4CiBp0rbpq5", IsReadOnly: true)]
        public string RepairItemStatus { get; set; }

        [FwLogicProperty(Id: "1aV2Kr7OFOHQx", IsReadOnly: true)]
        public decimal? Cost { get; set; }

        [FwLogicProperty(Id: "Yag1fTKZ5BSB")]
        public string CurrencyId { get { return repair.CurrencyId; } set { repair.CurrencyId = value; } }

        [FwLogicProperty(Id: "iqmM0bBU3XGhJ", IsReadOnly: true)]
        public string OfficeLocationDefaultCurrencyId { get; set; }

        [FwLogicProperty(Id: "FNMEbmfeCtmwr", IsReadOnly: true)]
        public string CurrencyCode { get; set; }

        [FwLogicProperty(Id: "hUwDMUDvHLNnC", IsReadOnly: true)]
        public string CurrencyColor { get; set; }

        [FwLogicProperty(Id: "I70kSMt2dxPB")]
        public string Notes { get { return repair.Notes; } set { repair.Notes = value; } }

        [FwLogicProperty(Id: "oIvhtpVN4x8oz", IsReadOnly: true)]
        public bool? Inactive { get; set; }

        [FwLogicProperty(Id: "P9UwssvixT0jy", IsReadOnly: true)]
        public bool? QcRequired { get; set; }

        [FwLogicProperty(Id: "7SiSsbDi7VSzK")]
        public bool? AutoCompleteQC { get { return repair.AutoCompleteQC; } set { repair.AutoCompleteQC = value; } }

        [FwLogicProperty(Id: "krgHtYtSB2cdU")]
        public string QcNote { get { return repair.QcNote; } set { repair.QcNote = value; } }

        [FwLogicProperty(Id: "z2dVlxktieYcV")]
        public string ConditionId { get { return repair.ConditionId; } set { repair.ConditionId = value; } }

        [FwLogicProperty(Id: "9k7XCxykxQHoI", IsReadOnly: true)]
        public string Condition { get; set; }

        [FwLogicProperty(Id: "UuN9hsMjftqY")]
        public string DateStamp { get { return repair.DateStamp; } set { repair.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
        public void TaxAssignPrimaryKeys(object sender, EventArgs e)
        {
            ((TaxRecord)sender).TaxId = tmpTaxId;
        }
        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;
            if (saveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                if ((ItemId != null) && (!ItemId.Equals(string.Empty)))
                {
                    string statusType = AppFunc.GetStringDataAsync(AppConfig, "rentalitemview", "rentalitemid", ItemId, "statustype").Result;
                    if (PendingRepair.Value)
                    {
                        if (!statusType.Equals(RwConstants.INVENTORY_STATUS_TYPE_OUT))
                        {
                            isValid = false;
                            validateMsg = "Cannot create a Pending Repair for an item that is " + statusType;
                        }
                    }
                    else
                    {
                        if (!statusType.Equals(RwConstants.INVENTORY_STATUS_TYPE_IN))
                        {
                            isValid = false;
                            validateMsg = "Cannot create a Repair for an item that is " + statusType;
                        }
                    }
                }
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------
        public void OnBeforeSaveRepair(object sender, BeforeSaveDataRecordEventArgs e)
        {
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                RepairNumber = AppFunc.GetNextModuleCounterAsync(AppConfig, UserSession, RwConstants.MODULE_REPAIR, LocationId, e.SqlConnection).Result;
                Status = RwConstants.REPAIR_STATUS_NEW;
                StatusDate = FwConvert.ToString(DateTime.Today);
                InputDate = FwConvert.ToString(DateTime.Today);

                if ((RepairDate == null) || (RepairDate.Equals(string.Empty)))
                {
                    RepairDate = FwConvert.ToString(DateTime.Today);
                }
                if ((Priority == null) || (Priority.Equals(string.Empty)))
                {
                    Priority = RwConstants.REPAIR_PRIORITY_MEDIUM;
                }
                if ((ItemId != null) && (!ItemId.Equals(string.Empty)))
                {
                    InventoryId = AppFunc.GetStringDataAsync(AppConfig, "rentalitem", "rentalitemid", ItemId, "masterid").Result;
                }
                if ((InventoryId == null) || (InventoryId.Equals(string.Empty)))
                {
                    RepairType = RwConstants.REPAIR_TYPE_OUTSIDE;
                }
                if ((TaxOptionId == null) || (TaxOptionId.Equals(string.Empty)))
                {
                    TaxOptionId = AppFunc.GetDepartmentLocationAsync(AppConfig, UserSession, DepartmentId, LocationId, "repairtaxoptionid").Result;
                }
                tmpTaxId = AppFunc.GetNextIdAsync(AppConfig, conn: e.SqlConnection).Result;
                TaxId = tmpTaxId;
            }
            else
            {
                if ((tax.TaxId == null) || (tax.TaxId.Equals(string.Empty)))
                {
                    //RepairLogic l2 = new RepairLogic();
                    //l2.SetDependencies(this.AppConfig, this.UserSession);
                    //object[] pk = GetPrimaryKeys();
                    //bool b = l2.LoadAsync<RepairLogic>(pk).Result;
                    //tax.TaxId = l2.TaxId;
                    tax.TaxId = ((RepairRecord)e.Original).TaxId;
                }
            }
        }
        //------------------------------------------------------------------------------------
        public void OnAfterSaveRepair(object sender, AfterSaveDataRecordEventArgs e)
        {
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                if ((ItemId != null) && (!ItemId.Equals(string.Empty)))
                {
                    RepairItemRecord repairItem = new RepairItemRecord();
                    repairItem.SetDependencies(this.AppConfig, this.UserSession);
                    repairItem.RepairId = RepairId;
                    repairItem.ItemId = ItemId;
                    int i = repairItem.SaveAsync(null, e.SqlConnection).Result;
                }
            }

            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smUpdate)
            {
                if ((TaxOptionId != null) && (!TaxOptionId.Equals(string.Empty)))
                {
                    RepairLogic l2 = new RepairLogic();
                    l2.SetDependencies(this.AppConfig, this.UserSession);
                    object[] pk = GetPrimaryKeys();
                    bool b = l2.LoadAsync<RepairLogic>(pk).Result;
                    TaxId = l2.TaxId;

                    if ((TaxId != null) && (!TaxId.Equals(string.Empty)))
                    {
                        b = AppFunc.UpdateTaxFromTaxOptionASync(this.AppConfig, this.UserSession, TaxOptionId, TaxId, e.SqlConnection).Result;
                    }
                }
            }

            string invId = InventoryId;
            string whId = WarehouseId;
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smUpdate)
            {
                RepairRecord orig = (RepairRecord)e.Original;
                if (invId == null)
                {
                    invId = orig.InventoryId;
                }
                if (whId == null)
                {
                    whId = orig.WarehouseId;
                }
            }
            if (!string.IsNullOrEmpty(InventoryId) && !string.IsNullOrEmpty(WarehouseId))
            {
                InventoryAvailabilityFunc.RequestRecalc(invId, whId, "");  // #jhtodo: classification?
            }

        }
        //------------------------------------------------------------------------------------
        public void OnAfterSaveTax(object sender, AfterSaveDataRecordEventArgs e)
        {
            if ((TaxOptionId != null) && (!TaxOptionId.Equals(string.Empty)))
            {
                if ((TaxId == null) || (TaxId.Equals(string.Empty)))
                {
                    RepairLogic l2 = new RepairLogic();
                    l2.SetDependencies(this.AppConfig, this.UserSession);
                    object[] pk = GetPrimaryKeys();
                    bool b = l2.LoadAsync<RepairLogic>(pk).Result;
                    TaxId = l2.TaxId;
                }

                if ((TaxId != null) && (!TaxId.Equals(string.Empty)))
                {
                    bool b = AppFunc.UpdateTaxFromTaxOptionASync(this.AppConfig, this.UserSession, TaxOptionId, TaxId, e.SqlConnection).Result;
                }
            }
        }
        //------------------------------------------------------------------------------------
        public async Task<ToggleRepairEstimateResponse> ToggleEstimate()
        {
            return await repair.ToggleEstimate();
        }
        //------------------------------------------------------------------------------------ 
        public async Task<ToggleRepairCompleteResponse> ToggleComplete()
        {
            return await repair.ToggleComplete();
        }
        //------------------------------------------------------------------------------------ 
        public async Task<RepairReleaseItemsResponse> ReleaseItems(int quantity)
        {
            return await repair.ReleaseItems(quantity);
        }
        //------------------------------------------------------------------------------------ 
        public async Task<VoidRepairResponse> Void()
        {
            return await repair.Void();
        }
        //------------------------------------------------------------------------------------ 
    }
}
