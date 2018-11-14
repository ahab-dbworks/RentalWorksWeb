using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using Newtonsoft.Json;
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
            inventoryCategory.BeforeSave += OnBeforeSaveCategory;
            inventoryCategory.AfterSave += OnAfterSaveCategory;
            masterRecord.AfterSave += OnAfterSaveMasterRecord;
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"PgUSszjdYyKw", IsPrimaryKey:true)]
        public string GeneratorTypeId { get { return masterRecord.MasterId; } set {masterRecord.MasterId = value; } }

        [FwLogicProperty(Id:"PgUSszjdYyKw", IsRecordTitle:true)]
        public string GeneratorType { get { return masterRecord.Description; } set { masterRecord.Description = value; } }

        [FwLogicProperty(Id:"N6OMYhFREJs")]
        public int? PreventiveMaintenanceCycleHours { get { return inventoryCategory.PreventiveMaintenanceCyclePeriod; } set { inventoryCategory.PreventiveMaintenanceCyclePeriod = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"pkQ8bO3iIy1")]
        public string CategoryId { get { return inventoryCategory.CategoryId; } set { inventoryCategory.CategoryId = value; } }

        //------------------------------------------------------------------------------------
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            RecType = "V";
            InternalVehicleType = "GENERATOR";
            HasMaintenance = true;
            Classification = "V";
            AvailableFrom = "W";
            AvailFor = "V";
        }
        //------------------------------------------------------------------------------------
        public void OnBeforeSaveCategory(object sender, BeforeSaveDataRecordEventArgs e)
        {
            Category = GeneratorTypeId;  // jh removing the TEMP value here
        }
        //------------------------------------------------------------------------------------
        public void OnAfterSaveMasterRecord(object sender, AfterSaveDataRecordEventArgs e)
        {
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smUpdate)
            {
                GeneratorTypeLogic l2 = new GeneratorTypeLogic();
                l2.AppConfig = masterRecord.AppConfig;
                object[] pk = GetPrimaryKeys();
                bool b = l2.LoadAsync<GeneratorTypeLogic>(pk).Result;
                CategoryId = l2.CategoryId;
            }
        }
        //------------------------------------------------------------------------------------
        public void OnAfterSaveCategory(object sender, AfterSaveDataRecordEventArgs e)
        {
            if ((e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert) && (masterRecord.CategoryId == null))
            {
                masterRecord.CategoryId = inventoryCategory.CategoryId;
                int i = masterRecord.SaveAsync(null).Result;
            }
        }
        //------------------------------------------------------------------------------------
    }

}
