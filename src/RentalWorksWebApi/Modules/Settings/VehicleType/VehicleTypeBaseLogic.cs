﻿using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Home.Master;
using RentalWorksWebApi.Modules.Settings.InventoryCategory;
using System;
using static FwStandard.DataLayer.FwDataReadWriteRecord;

namespace RentalWorksWebApi.Modules.Settings.VehicleType
{
    public abstract class VehicleTypeBaseLogic: RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        protected InventoryCategoryRecord inventoryCategory = new InventoryCategoryRecord();
        protected MasterRecord masterRecord = new MasterRecord();
        //protected VehicleTypeLoader vehicleTypeLoader = new VehicleTypeLoader();
        public VehicleTypeBaseLogic()
        {
            dataRecords.Add(inventoryCategory);
            dataRecords.Add(masterRecord);
            //dataLoader = vehicleTypeLoader;
            inventoryCategory.InventoryCategory = "TEMP";  //jh - temporary value because the field is required
            inventoryCategory.BeforeValidate += OnBeforeValidateCategory;
        }
        //------------------------------------------------------------------------------------
        public string InventoryTypeId { get { return inventoryCategory.InventoryTypeId; } set { inventoryCategory.InventoryTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InventoryType { get; set; }
        [JsonIgnore]
        public string RecType { get { return inventoryCategory.RecType; } set { inventoryCategory.RecType = value; } }
        [JsonIgnore]
        public string MasterId { get { return masterRecord.MasterId; } set { masterRecord.MasterId = value; } }
        [JsonIgnore]
        public string InventoryCategory { get { return inventoryCategory.InventoryCategory; } set { inventoryCategory.InventoryCategoryId = value; inventoryCategory.InventoryCategory = value; masterRecord.CategoryId = value; } }
        [JsonIgnore]
        public string Classification { get { return masterRecord.Classification; } set { masterRecord.Classification = value; } }
        [JsonIgnore]
        public string AvailableFrom { get { return masterRecord.AvailableFrom; } set { masterRecord.AvailableFrom = value; } }
        [JsonIgnore]
        public string AvailFor { get { return masterRecord.AvailFor; } set { masterRecord.AvailFor = value; } }
        [JsonIgnore]
        public bool? HasMaintenance { get { return inventoryCategory.HasMaintenance; } set { inventoryCategory.HasMaintenance = value; } }
        [JsonIgnore]
        public string InternalVehicleType { get { return inventoryCategory.VehicleType; } set { inventoryCategory.VehicleType = value; } }
        public string UnitId { get { return masterRecord.UnitId; } set { masterRecord.UnitId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Unit { get; set; }


        public string AssetAccountId { get { return inventoryCategory.AssetAccountId; } set { inventoryCategory.AssetAccountId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AssetAccountNo { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AssetAccountDescription { get; set; }
        public string IncomeAccountId { get { return inventoryCategory.IncomeAccountId; } set { inventoryCategory.IncomeAccountId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string IncomeAccountNo { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string IncomeAccountDescription { get; set; }
        public string SubIncomeAccountId { get { return inventoryCategory.SubIncomeAccountId; } set { inventoryCategory.SubIncomeAccountId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SubIncomeAccountNo { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SubIncomeAccountDescription { get; set; }
        public string LdIncomeAccountId { get { return inventoryCategory.LdIncomeAccountId; } set { inventoryCategory.LdIncomeAccountId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LdIncomeAccountNo { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LdIncomeAccountDescription { get; set; }
        public string EquipmentSaleIncomeAccountId { get { return inventoryCategory.EquipmentSaleIncomeAccountId; } set { inventoryCategory.EquipmentSaleIncomeAccountId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string EquipSaleIncomeAccountNo { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string EquipSaleIncomeAccountDescription { get; set; }
        public string ExpenseAccountId { get { return inventoryCategory.ExpenseAccountId; } set { inventoryCategory.ExpenseAccountId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ExpenseAccountNo { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ExpenseAccountDescription { get; set; }
        public string CostOfGoodsSoldExpenseAccountId { get { return inventoryCategory.CostOfGoodsSoldExpenseAccountId; } set { inventoryCategory.CostOfGoodsSoldExpenseAccountId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CostOfGoodsSoldExpenseAccountNo { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CostOfGoodsSoldExpenseAccountDescription { get; set; }
        public string CostOfGoodsRentedExpenseAccountId { get { return inventoryCategory.CostOfGoodsRentedExpenseAccountId; } set { inventoryCategory.CostOfGoodsRentedExpenseAccountId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CostOfGoodsRentedExpenseAccountNo { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CostOfGoodsRentedExpenseAccountDescription { get; set; }
        public decimal? OrderBy { get { return inventoryCategory.OrderBy; } set { inventoryCategory.OrderBy = value; } }
        public int? PickListOrderBy { get { return inventoryCategory.PickListOrderBy; } set { inventoryCategory.PickListOrderBy = value; } }
        public bool? Inactive { get { return inventoryCategory.Inactive; } set { inventoryCategory.Inactive = value; } }
        public string DateStamp { get { return inventoryCategory.DateStamp; } set { inventoryCategory.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        public override void BeforeSave()
        {
            RecType = "V";
            InternalVehicleType = "VEHICLE";
            HasMaintenance = true;
            Classification = "V";
            AvailableFrom = "W";
            AvailFor = "V";
        }
        //------------------------------------------------------------------------------------
        public void OnBeforeValidateCategory(object sender, SaveEventArgs e)
        {
            if (inventoryCategory.InventoryCategory.Equals(string.Empty))
            {
                inventoryCategory.InventoryCategory = masterRecord.Description;
            }
        }
        //------------------------------------------------------------------------------------
    }

}
