using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using WebLibrary;

namespace WebApi.Modules.Settings.VehicleType
{
    public class VehicleTypeLogic: VehicleTypeBaseLogic
    {
        //------------------------------------------------------------------------------------
        protected VehicleTypeLoader vehicleTypeLoader = new VehicleTypeLoader();
        public VehicleTypeLogic() : base()
        {
            dataLoader = vehicleTypeLoader;
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------

        [FwLogicProperty(Id:"w2uFguixgEUkt", IsPrimaryKey:true)]
        public string VehicleTypeId { get { return masterRecord.MasterId; } set { masterRecord.MasterId = value; } }

        [FwLogicProperty(Id:"w2uFguixgEUkt", IsRecordTitle:true)]
        public string VehicleType { get { return masterRecord.Description; } set { inventoryCategory.Category = value; masterRecord.Description = value; } }

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
        
        //------------------------------------------------------------------------------------
        public override void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            base.OnBeforeSave(sender, e);
            RecType = RwConstants.RECTYPE_VEHICLE;
            InternalVehicleType = RwConstants.VEHICLE_TYPE_VEHICLE;
            HasMaintenance = true;
            Classification = RwConstants.INVENTORY_CLASSIFICATION_VEHICLE;
            AvailableFrom = RwConstants.INVENTORY_AVAILABLE_FROM_WAREHOUSE;
            AvailFor = RwConstants.INVENTORY_AVAILABLE_FOR_VEHICLE;
        }
        //------------------------------------------------------------------------------------
    }

}
