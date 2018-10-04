using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;

namespace WebApi.Modules.Settings.VehicleType
{
    public class VehicleTypeLogic: VehicleTypeBaseLogic
    {
        //------------------------------------------------------------------------------------
        protected VehicleTypeLoader vehicleTypeLoader = new VehicleTypeLoader();
        public VehicleTypeLogic() : base()
        {
            dataLoader = vehicleTypeLoader;
            inventoryCategory.BeforeSave += OnBeforeSaveCategory;
            inventoryCategory.AfterSave += OnAfterSaveCategory;
            masterRecord.AfterSave += OnAfterSaveMasterRecord;
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------

        [FwBusinessLogicField(isPrimaryKey: true)]
        public string VehicleTypeId { get { return masterRecord.MasterId; } set { masterRecord.MasterId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string VehicleType { get { return masterRecord.Description; } set { masterRecord.Description = value; } }
        public string PreventiveMaintenanceCycle { get { return inventoryCategory.PreventiveMaintenanceCycle; } set { inventoryCategory.PreventiveMaintenanceCycle = value; } }
        public int? PreventiveMaintenanceCyclePeriod { get { return inventoryCategory.PreventiveMaintenanceCyclePeriod; } set { inventoryCategory.PreventiveMaintenanceCyclePeriod = value; } }
        public int? DotPeriod { get { return inventoryCategory.DotPeriod; } set { inventoryCategory.DotPeriod = value; } }
        public string LicenseClassId { get { return inventoryCategory.LicenseClassId; } set { inventoryCategory.LicenseClassId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LicenseClass { get; set; }
        public bool? Regulated { get { return inventoryCategory.Regulated; } set { inventoryCategory.Regulated = value; } }
        [JsonIgnore]
        public string CategoryId { get { return inventoryCategory.CategoryId; } set { inventoryCategory.CategoryId = value; } }
        //------------------------------------------------------------------------------------
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            RecType = "V";
            InternalVehicleType = "VEHICLE";
            HasMaintenance = true;
            Classification = "V";
            AvailableFrom = "W";
            AvailFor = "V";
        }
        //------------------------------------------------------------------------------------
        public void OnBeforeSaveCategory(object sender, BeforeSaveDataRecordEventArgs e)
        {
            Category = VehicleTypeId;  // jh removing the TEMP value here
        }
        //------------------------------------------------------------------------------------
        public void OnAfterSaveMasterRecord(object sender, AfterSaveDataRecordEventArgs e)
        {
            if ((e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smUpdate) && (MasterId == null))
            {
                VehicleTypeLogic l2 = new VehicleTypeLogic();
                l2.AppConfig = masterRecord.AppConfig;
                object[] pk = GetPrimaryKeys();
                bool b = l2.LoadAsync<VehicleTypeLogic>(pk).Result;
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
