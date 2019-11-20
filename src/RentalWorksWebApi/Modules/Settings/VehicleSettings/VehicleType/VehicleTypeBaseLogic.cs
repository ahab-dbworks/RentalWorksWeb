using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using Newtonsoft.Json;
using System;
using WebApi.Logic;
using WebApi.Modules.HomeControls.Master;
using WebApi.Modules.Settings.Category;

namespace WebApi.Modules.Settings.VehicleSettings.VehicleType
{
    [FwLogic(Id:"YPhuIYmoOkt")]
    public abstract class VehicleTypeBaseLogic: AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        protected CategoryRecord inventoryCategory = new CategoryRecord();
        protected MasterRecord masterRecord = new MasterRecord();
        public VehicleTypeBaseLogic()
        {
            dataRecords.Add(inventoryCategory);
            dataRecords.Add(masterRecord);
            DeleteRecordsInReverseSequence = true;
            masterRecord.AssignPrimaryKeys += MasterAssignPrimaryKeys;
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"oAkN18doD9h")]
        public string InventoryTypeId { get { return inventoryCategory.TypeId; } set { inventoryCategory.TypeId = value; } }

        [FwLogicProperty(Id:"kUpdKNteIwR", IsReadOnly:true)]
        public string InventoryType { get; set; }

        [JsonIgnore]
        [FwLogicProperty(Id:"8bklgQfU0q1")]
        public string RecType { get { return inventoryCategory.RecType; } set { inventoryCategory.RecType = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"L5opzYKanza")]
        public string MasterId { get { return masterRecord.MasterId; } set { masterRecord.MasterId = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"2Cj8aJX7GnAK")]
        public string Category { get { return inventoryCategory.Category; } set { /*inventoryCategory.CategoryId = value; */inventoryCategory.Category = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id: "2Cj8aJX7GnAK")]
        public string CategoryId { get { return inventoryCategory.CategoryId; } set { inventoryCategory.CategoryId = value; masterRecord.CategoryId = value; } }


        [JsonIgnore]
        [FwLogicProperty(Id:"ctpV5KsW9xP")]
        public string Classification { get { return masterRecord.Classification; } set { masterRecord.Classification = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"Cbd15c4pXpq")]
        public string AvailableFrom { get { return masterRecord.AvailableFrom; } set { masterRecord.AvailableFrom = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"rU0VJtI5Jl9")]
        public string AvailFor { get { return masterRecord.AvailFor; } set { masterRecord.AvailFor = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"13uR8s1JeUT")]
        public bool? HasMaintenance { get { return inventoryCategory.HasMaintenance; } set { inventoryCategory.HasMaintenance = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"mUNXVcgLVKB")]
        public string InternalVehicleType { get { return inventoryCategory.VehicleType; } set { inventoryCategory.VehicleType = value; } }

        [FwLogicProperty(Id:"uKyHgD2tfcy")]
        public string UnitId { get { return masterRecord.UnitId; } set { masterRecord.UnitId = value; } }

        [FwLogicProperty(Id:"dMKLBFHZssU", IsReadOnly:true)]
        public string Unit { get; set; }



        [FwLogicProperty(Id:"KoFkVqAuvIv")]
        public string AssetAccountId { get { return inventoryCategory.AssetAccountId; } set { inventoryCategory.AssetAccountId = value; } }

        [FwLogicProperty(Id:"TNkTJvQG2Ml", IsReadOnly:true)]
        public string AssetAccountNo { get; set; }

        [FwLogicProperty(Id:"JNdbFE6sK5q", IsReadOnly:true)]
        public string AssetAccountDescription { get; set; }

        [FwLogicProperty(Id:"tDbHvFh4MpZ")]
        public string IncomeAccountId { get { return inventoryCategory.IncomeAccountId; } set { inventoryCategory.IncomeAccountId = value; } }

        [FwLogicProperty(Id:"vJzLUT2vhbq", IsReadOnly:true)]
        public string IncomeAccountNo { get; set; }

        [FwLogicProperty(Id:"Xkf3B4FJppO", IsReadOnly:true)]
        public string IncomeAccountDescription { get; set; }

        [FwLogicProperty(Id:"SqnId6YDI2Q")]
        public string SubIncomeAccountId { get { return inventoryCategory.SubIncomeAccountId; } set { inventoryCategory.SubIncomeAccountId = value; } }

        [FwLogicProperty(Id:"4AbNhDjsaIj", IsReadOnly:true)]
        public string SubIncomeAccountNo { get; set; }

        [FwLogicProperty(Id:"Bt4ZrcbDCB9", IsReadOnly:true)]
        public string SubIncomeAccountDescription { get; set; }

        [FwLogicProperty(Id:"QyeaF7rQ3vD")]
        public string LdIncomeAccountId { get { return inventoryCategory.LdIncomeAccountId; } set { inventoryCategory.LdIncomeAccountId = value; } }

        [FwLogicProperty(Id:"PHDdQGH2F4s", IsReadOnly:true)]
        public string LdIncomeAccountNo { get; set; }

        [FwLogicProperty(Id:"tfeeo7zyt2d", IsReadOnly:true)]
        public string LdIncomeAccountDescription { get; set; }

        [FwLogicProperty(Id:"NiTQhANo90O")]
        public string EquipmentSaleIncomeAccountId { get { return inventoryCategory.EquipmentSaleIncomeAccountId; } set { inventoryCategory.EquipmentSaleIncomeAccountId = value; } }

        [FwLogicProperty(Id:"2rdNrpomTSN", IsReadOnly:true)]
        public string EquipmentSaleIncomeAccountNo { get; set; }

        [FwLogicProperty(Id:"YLAAFoKTOUH", IsReadOnly:true)]
        public string EquipmentSaleIncomeAccountDescription { get; set; }

        [FwLogicProperty(Id:"8uKICodh2mL")]
        public string ExpenseAccountId { get { return inventoryCategory.ExpenseAccountId; } set { inventoryCategory.ExpenseAccountId = value; } }

        [FwLogicProperty(Id:"ky7QCwAO7qf", IsReadOnly:true)]
        public string ExpenseAccountNo { get; set; }

        [FwLogicProperty(Id:"GAepnSdoCvu", IsReadOnly:true)]
        public string ExpenseAccountDescription { get; set; }

        [FwLogicProperty(Id:"VjE3NhQcdRc")]
        public string CostOfGoodsSoldExpenseAccountId { get { return inventoryCategory.CostOfGoodsSoldExpenseAccountId; } set { inventoryCategory.CostOfGoodsSoldExpenseAccountId = value; } }

        [FwLogicProperty(Id:"5N83dEQpaZu", IsReadOnly:true)]
        public string CostOfGoodsSoldExpenseAccountNo { get; set; }

        [FwLogicProperty(Id:"U7QaYLHWXqV", IsReadOnly:true)]
        public string CostOfGoodsSoldExpenseAccountDescription { get; set; }

        [FwLogicProperty(Id:"YS9WeDNZ59w")]
        public string CostOfGoodsRentedExpenseAccountId { get { return inventoryCategory.CostOfGoodsRentedExpenseAccountId; } set { inventoryCategory.CostOfGoodsRentedExpenseAccountId = value; } }

        [FwLogicProperty(Id:"a4eB5BsSC6h", IsReadOnly:true)]
        public string CostOfGoodsRentedExpenseAccountNo { get; set; }

        [FwLogicProperty(Id:"ANFi8Dcde75", IsReadOnly:true)]
        public string CostOfGoodsRentedExpenseAccountDescription { get; set; }

        [FwLogicProperty(Id:"D6CkbLaeihY")]
        public decimal? OrderBy { get { return inventoryCategory.OrderBy; } set { inventoryCategory.OrderBy = value; } }

        [FwLogicProperty(Id:"cREaCvKpNpy")]
        public int? PickListOrderBy { get { return inventoryCategory.PickListOrderBy; } set { inventoryCategory.PickListOrderBy = value; } }

        [FwLogicProperty(Id:"uDkEgpv6UEq")]
        public bool? Inactive { get { return inventoryCategory.Inactive; } set { inventoryCategory.Inactive = value; } }

        [FwLogicProperty(Id:"9gJq8DzKmkJ")]
        public string DateStamp { get { return inventoryCategory.DateStamp; } set { inventoryCategory.DateStamp = value; } }

        //------------------------------------------------------------------------------------
        public void MasterAssignPrimaryKeys(object sender, EventArgs e)
        {
            ((MasterRecord)sender).MasterId = CategoryId;
            ((MasterRecord)sender).CategoryId = CategoryId;
        }
        //------------------------------------------------------------------------------------ 
        public virtual void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                if (e.Original != null)
                {
                    VehicleTypeBaseLogic orig = ((VehicleTypeBaseLogic)e.Original);
                    CategoryId = orig.CategoryId;
                }
            }
        }
        //------------------------------------------------------------------------------------
    }

}
