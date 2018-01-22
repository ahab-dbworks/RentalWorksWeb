using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Home.Master;
using WebApi.Modules.Settings.Category;
using System;
using static FwStandard.DataLayer.FwDataReadWriteRecord;

namespace WebApi.Modules.Settings.VehicleType
{
    public class VehicleTypeLogic: VehicleTypeBaseLogic
    {
        //------------------------------------------------------------------------------------
        protected VehicleTypeLoader vehicleTypeLoader = new VehicleTypeLoader();
        public VehicleTypeLogic() : base()
        {
            dataLoader = vehicleTypeLoader;
            inventoryCategory.BeforeSaves += OnBeforeSavesCategory;
            inventoryCategory.AfterSaves += OnAfterSavesCategory;
        }
        //------------------------------------------------------------------------------------

        [FwBusinessLogicField(isPrimaryKey: true)]
        public string VehicleTypeId { get { return inventoryCategory.CategoryId; } set { inventoryCategory.CategoryId = value; inventoryCategory.Category = value; masterRecord.CategoryId = value; } }
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
        public string CategoryId { get { return VehicleTypeId; } set { VehicleTypeId = value; } }
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
        public void OnBeforeSavesCategory(object sender, BeforeSaveEventArgs e)
        {
            Category = VehicleTypeId;  // jh - remove TEMP value
        }
        //------------------------------------------------------------------------------------
        public void OnAfterSavesCategory(object sender, AfterSaveEventArgs e)
        {
            CategoryId = VehicleTypeId;
            if ((e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smUpdate) && (e.SavePerformed) && (MasterId == null))
            {
                VehicleTypeLogic l2 = new VehicleTypeLogic();
                l2.SetDbConfig(inventoryCategory.GetDbConfig());
                string[] pk = GetPrimaryKeys();
                bool b = l2.LoadAsync<VehicleTypeLogic>(pk).Result;
                MasterId = l2.MasterId;
            }
        }
        //------------------------------------------------------------------------------------
    }

}
