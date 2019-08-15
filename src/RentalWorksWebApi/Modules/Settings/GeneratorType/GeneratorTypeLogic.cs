using FwStandard.AppManager;
﻿using FwStandard.BusinessLogic;
using WebApi.Modules.Settings.VehicleType;
using WebLibrary;

namespace WebApi.Modules.Settings.GeneratorType
{
    public class GeneratorTypeLogic: VehicleTypeBaseLogic
    {
        //------------------------------------------------------------------------------------
        protected GeneratorTypeLoader generatorTypeLoader = new GeneratorTypeLoader();
        public GeneratorTypeLogic() : base()
        {
            dataLoader = generatorTypeLoader;
            BeforeSave += OnBeforeSave;
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"PgUSszjdYyKw", IsPrimaryKey:true)]
        public string GeneratorTypeId { get { return masterRecord.MasterId; } set {masterRecord.MasterId = value; } }

        [FwLogicProperty(Id:"PgUSszjdYyKw", IsRecordTitle:true)]
        public string GeneratorType { get { return masterRecord.Description; } set { inventoryCategory.Category = value; masterRecord.Description = value; } }

        [FwLogicProperty(Id:"N6OMYhFREJs")]
        public int? PreventiveMaintenanceCycleHours { get { return inventoryCategory.PreventiveMaintenanceCyclePeriod; } set { inventoryCategory.PreventiveMaintenanceCyclePeriod = value; } }

               //------------------------------------------------------------------------------------
        public override void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            base.OnBeforeSave(sender, e);
            RecType = RwConstants.RECTYPE_VEHICLE;
            InternalVehicleType = RwConstants.VEHICLE_TYPE_GENERATOR;
            HasMaintenance = true;
            Classification = RwConstants.INVENTORY_CLASSIFICATION_VEHICLE;
            AvailableFrom = RwConstants.INVENTORY_AVAILABLE_FROM_WAREHOUSE;
            AvailFor = RwConstants.INVENTORY_AVAILABLE_FOR_VEHICLE;
        }
        //------------------------------------------------------------------------------------
    }

}
