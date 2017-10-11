using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Settings.SpaceType
{
    public class SpaceTypeLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        SpaceTypeRecord spaceType = new SpaceTypeRecord();
        SpaceTypeLoader spaceTypeLoader = new SpaceTypeLoader();
        public SpaceTypeLogic()
        {
            dataRecords.Add(spaceType);
            dataLoader = spaceTypeLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string SpaceTypeId { get { return spaceType.SpaceTypeId; } set { spaceType.SpaceTypeId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string SpaceType { get { return spaceType.SpaceType; } set { spaceType.SpaceType = value; } }
        public string SpaceTypeClassification { get { return spaceType.SpaceTypeClassification; } set { spaceType.SpaceTypeClassification = value; } }
        public string FacilityTypeId { get { return spaceType.FacilityTypeId; } set { spaceType.FacilityTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string FacilityType { get; set; }
        public string RateId { get { return spaceType.RateId; } set { spaceType.RateId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RateICode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RateDescription { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RateUnitId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RateUnit { get; set; }
        public int? Color { get { return spaceType.Color; } set { spaceType.Color = value; } }
        public bool WhiteText { get { return spaceType.WhiteText; } set { spaceType.WhiteText = value; } }
        public int? OrderBy { get { return spaceType.OrderBy; } set { spaceType.OrderBy = value; } }
        public bool NonBillable { get { return spaceType.NonBillable; } set { spaceType.NonBillable = value; } }
        public bool ForReportsOnly { get { return spaceType.ForReportsOnly; } set { spaceType.ForReportsOnly = value; } }
        public bool AddToDescription { get { return spaceType.AddToDescription; } set { spaceType.AddToDescription = value; } }
        public bool Inactive { get { return spaceType.Inactive; } set { spaceType.Inactive = value; } }
        public string DateStamp { get { return spaceType.DateStamp; } set { spaceType.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}