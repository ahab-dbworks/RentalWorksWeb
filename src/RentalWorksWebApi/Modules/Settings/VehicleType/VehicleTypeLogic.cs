using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
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

        [FwLogicProperty(Id:"w2uFguixgEUkt", IsPrimaryKey:true)]
        public string VehicleTypeId { get { return masterRecord.MasterId; } set { masterRecord.MasterId = value; } }

        [FwLogicProperty(Id:"w2uFguixgEUkt", IsRecordTitle:true)]
        public string VehicleType { get { return masterRecord.Description; } set { masterRecord.Description = value; } }

        [FwLogicProperty(Id:"oMyBTM3VYqxm")]
        public string PreventiveMaintenanceCycle { get { return inventoryCategory.PreventiveMaintenanceCycle; } set { inventoryCategory.PreventiveMaintenanceCycle = value; } }

        [FwLogicProperty(Id:"wzYuVzt8t5Oc")]
        public int? PreventiveMaintenanceCyclePeriod { get { return inventoryCategory.PreventiveMaintenanceCyclePeriod; } set { inventoryCategory.PreventiveMaintenanceCyclePeriod = value; } }

        [FwLogicProperty(Id:"1MP8fM59iujV")]
        public int? DotPeriod { get { return inventoryCategory.DotPeriod; } set { inventoryCategory.DotPeriod = value; } }

        [FwLogicProperty(Id:"cbisJAMcc2sn")]
        public string LicenseClassId { get { return inventoryCategory.LicenseClassId; } set { inventoryCategory.LicenseClassId = value; } }

        [FwLogicProperty(Id:"DGb5nhHMufwpp", IsReadOnly:true)]
        public string LicenseClass { get; set; }

        [FwLogicProperty(Id:"omRLj0ZQ2QKG")]
        public bool? Regulated { get { return inventoryCategory.Regulated; } set { inventoryCategory.Regulated = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id:"2Cj8aJX7GnAK")]
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
