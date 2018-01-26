using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Home.Master;
using WebApi.Modules.Settings.Category;
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
            inventoryCategory.BeforeSave += OnBeforeSaveCategory;
            inventoryCategory.AfterSave += OnAfterSaveCategory;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string GeneratorTypeId { get { return inventoryCategory.CategoryId; } set { inventoryCategory.CategoryId = value; inventoryCategory.Category = value; masterRecord.CategoryId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string GeneratorType { get { return masterRecord.Description; } set { masterRecord.Description = value; } }
        public int? PreventiveMaintenanceCycleHours { get { return inventoryCategory.PreventiveMaintenanceCyclePeriod; } set { inventoryCategory.PreventiveMaintenanceCyclePeriod = value; } }
        [JsonIgnore]
        public string CategoryId { get { return GeneratorTypeId; } set { GeneratorTypeId = value; } }
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
        public void OnBeforeSaveCategory(object sender, BeforeSaveEventArgs e)
        {
            Category = GeneratorTypeId;  // jh - remove TEMP value
        }
        //------------------------------------------------------------------------------------
        public void OnAfterSaveCategory(object sender, AfterSaveEventArgs e)
        {
            CategoryId = GeneratorTypeId;
            if ((e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smUpdate) && (e.SavePerformed) && (MasterId == null))
            {
                GeneratorTypeLogic l2 = new GeneratorTypeLogic();
                l2.SetDbConfig(inventoryCategory.GetDbConfig());
                object[] pk = GetPrimaryKeys();
                bool b = l2.LoadAsync<GeneratorTypeLogic>(pk).Result;
                MasterId = l2.MasterId;
            }
        }
        //------------------------------------------------------------------------------------
    }

}
