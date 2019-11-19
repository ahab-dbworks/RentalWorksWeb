using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.FacilitySettings.SpaceType
{
    [FwLogic(Id:"91xWWU1Ykt3G5")]
    public class SpaceTypeLogic : AppBusinessLogic
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
        [FwLogicProperty(Id:"3ULj85xwPBM4A", IsPrimaryKey:true)]
        public string SpaceTypeId { get { return spaceType.SpaceTypeId; } set { spaceType.SpaceTypeId = value; } }

        [FwLogicProperty(Id:"3ULj85xwPBM4A", IsRecordTitle:true)]
        public string SpaceType { get { return spaceType.SpaceType; } set { spaceType.SpaceType = value; } }

        [FwLogicProperty(Id:"nnfwP7KeqSGg")]
        public string SpaceTypeClassification { get { return spaceType.SpaceTypeClassification; } set { spaceType.SpaceTypeClassification = value; } }

        [FwLogicProperty(Id:"N2IyzSeNho9k")]
        public string FacilityTypeId { get { return spaceType.FacilityTypeId; } set { spaceType.FacilityTypeId = value; } }

        [FwLogicProperty(Id:"2KvQj3ZTD8LRb", IsReadOnly:true)]
        public string FacilityType { get; set; }

        [FwLogicProperty(Id:"gs34R6cVWVVu")]
        public string RateId { get { return spaceType.RateId; } set { spaceType.RateId = value; } }

        [FwLogicProperty(Id:"cGT0xnU7969Mx", IsReadOnly:true)]
        public string RateICode { get; set; }

        [FwLogicProperty(Id:"Roswf9z6I39hu", IsReadOnly:true)]
        public string RateDescription { get; set; }

        [FwLogicProperty(Id:"4LJ8a8fNRuA1I", IsReadOnly:true)]
        public string RateUnitId { get; set; }

        [FwLogicProperty(Id:"4LJ8a8fNRuA1I", IsReadOnly:true)]
        public string RateUnit { get; set; }

        [FwLogicProperty(Id:"seX7hiSnxOFL")]
        public string Color { get { return spaceType.Color; } set { spaceType.Color = value; } }

        [FwLogicProperty(Id:"8nWd5hUJqQgM")]
        public bool? WhiteText { get { return spaceType.WhiteText; } set { spaceType.WhiteText = value; } }

        [FwLogicProperty(Id:"qPNCTZavqIAj")]
        public int? OrderBy { get { return spaceType.OrderBy; } set { spaceType.OrderBy = value; } }

        [FwLogicProperty(Id:"QlV0hENPylNS")]
        public bool? NonBillable { get { return spaceType.NonBillable; } set { spaceType.NonBillable = value; } }

        [FwLogicProperty(Id:"IqLfoGZVJlmc")]
        public bool? ForReportsOnly { get { return spaceType.ForReportsOnly; } set { spaceType.ForReportsOnly = value; } }

        [FwLogicProperty(Id:"KN7FguOyOOn1")]
        public bool? AddToDescription { get { return spaceType.AddToDescription; } set { spaceType.AddToDescription = value; } }

        [FwLogicProperty(Id:"Ks4q0K6Oe1d0")]
        public bool? Inactive { get { return spaceType.Inactive; } set { spaceType.Inactive = value; } }

        [FwLogicProperty(Id:"WMACTpZ0DDuq")]
        public string DateStamp { get { return spaceType.DateStamp; } set { spaceType.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
