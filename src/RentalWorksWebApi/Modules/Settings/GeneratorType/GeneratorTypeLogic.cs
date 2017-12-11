using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Home.Master;
using WebApi.Modules.Settings.InventoryCategory;
using System;
using static FwStandard.DataLayer.FwDataReadWriteRecord;
using WebApi.Modules.Settings.VehicleType;

namespace WebApi.Modules.Settings.GeneratorType
{
    public class GeneratorTypeLogic: VehicleTypeBaseLogic
    {
        //------------------------------------------------------------------------------------
        protected GeneratorTypeLoader generatorTypeLoader = new GeneratorTypeLoader();
        public GeneratorTypeLogic() : base()
        {
            dataLoader = generatorTypeLoader;
            inventoryCategory.BeforeSaves += OnBeforeSavesCategory;
            inventoryCategory.AfterSaves += OnAfterSavesCategory;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string GeneratorTypeId { get { return inventoryCategory.InventoryCategoryId; } set { inventoryCategory.InventoryCategoryId = value; inventoryCategory.InventoryCategory = value; masterRecord.CategoryId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string GeneratorType { get { return masterRecord.Description; } set { masterRecord.Description = value; } }
        public int? PreventiveMaintenanceCycleHours { get { return inventoryCategory.PreventiveMaintenanceCyclePeriod; } set { inventoryCategory.PreventiveMaintenanceCyclePeriod = value; } }
        [JsonIgnore]
        public string InventoryCategoryId { get { return GeneratorTypeId; } set { GeneratorTypeId = value; } }
        //------------------------------------------------------------------------------------
        public override void BeforeSave()
        {
            RecType = "V";
            InternalVehicleType = "GENERATOR";
            HasMaintenance = true;
            Classification = "V";
            AvailableFrom = "W";
            AvailFor = "V";
        }
        //------------------------------------------------------------------------------------
        public void OnBeforeSavesCategory(object sender, SaveEventArgs e)
        {
            InventoryCategory = GeneratorTypeId;  // jh - remove TEMP value
        }
        //------------------------------------------------------------------------------------
        public void OnAfterSavesCategory(object sender, SaveEventArgs e)
        {
            InventoryCategoryId = GeneratorTypeId;
            if ((e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smUpdate) && (MasterId == null))
            {
                GeneratorTypeLogic l2 = new GeneratorTypeLogic();
                l2.SetDbConfig(inventoryCategory.GetDbConfig());
                string[] pk = GetPrimaryKeys();
                bool b = l2.LoadAsync<GeneratorTypeLogic>(pk).Result;
                MasterId = l2.MasterId;
            }
        }
        //------------------------------------------------------------------------------------
    }

}
