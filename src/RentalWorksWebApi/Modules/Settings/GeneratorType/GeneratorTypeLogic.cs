using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string GeneratorTypeId { get { return masterRecord.MasterId; } set {masterRecord.MasterId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string GeneratorType { get { return masterRecord.Description; } set { masterRecord.Description = value; } }
        public int? PreventiveMaintenanceCycleHours { get { return inventoryCategory.PreventiveMaintenanceCyclePeriod; } set { inventoryCategory.PreventiveMaintenanceCyclePeriod = value; } }
        [JsonIgnore]
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
        public void OnBeforeSaveCategory(object sender, BeforeSaveEventArgs e)
        {
            Category = GeneratorTypeId;  // jh removing the TEMP value here
        }
        //------------------------------------------------------------------------------------
        public void OnAfterSaveMasterRecord(object sender, AfterSaveEventArgs e)
        {
            if ((e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smUpdate) && (e.SavePerformed))
            {
                GeneratorTypeLogic l2 = new GeneratorTypeLogic();
                l2.AppConfig = masterRecord.AppConfig;
                object[] pk = GetPrimaryKeys();
                bool b = l2.LoadAsync<GeneratorTypeLogic>(pk).Result;
                CategoryId = l2.CategoryId;
            }
        }
        //------------------------------------------------------------------------------------
        public void OnAfterSaveCategory(object sender, AfterSaveEventArgs e)
        {
            if ((e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert) && (e.SavePerformed) && (masterRecord.CategoryId == null))
            {
                masterRecord.CategoryId = inventoryCategory.CategoryId;
                int i = masterRecord.SaveAsync().Result;
            }
        }
        //------------------------------------------------------------------------------------
    }

}
