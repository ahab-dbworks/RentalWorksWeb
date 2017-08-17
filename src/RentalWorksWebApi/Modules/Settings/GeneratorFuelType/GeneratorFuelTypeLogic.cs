using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Settings.FuelType;

namespace RentalWorksWebApi.Modules.Settings.GeneratorFuelType
{
    public class GeneratorFuelTypeLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        FuelTypeRecord fuelType = new FuelTypeRecord();
        GeneratorFuelTypeLoader fuelTypeLoader = new GeneratorFuelTypeLoader();
        public GeneratorFuelTypeLogic()
        {
            dataRecords.Add(fuelType);
            dataLoader = fuelTypeLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string GeneratorFuelTypeId { get { return fuelType.FuelTypeId; } set { fuelType.FuelTypeId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string GeneratorFuelType { get { return fuelType.FuelType; } set { fuelType.FuelType = value; } }
        public string RowType { get { return fuelType.RowType; } set { fuelType.RowType = value; } }
        public bool Inactive { get { return fuelType.Inactive; } set { fuelType.Inactive = value; } }
        public string DateStamp { get { return fuelType.DateStamp; } set { fuelType.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        public override void BeforeSave()
        {
            RowType = "GENERATOR";
        }
        //------------------------------------------------------------------------------------

    }

}
