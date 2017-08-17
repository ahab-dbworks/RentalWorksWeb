using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.WardrobeGender
{
    public class WardrobeGenderLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        WardrobeGenderRecord wardrobeGender = new WardrobeGenderRecord();
        public WardrobeGenderLogic()
        {
            dataRecords.Add(wardrobeGender);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string WardrobeGenderId { get { return wardrobeGender.WardrobeGenderId; } set { wardrobeGender.WardrobeGenderId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string WardrobeGender { get { return wardrobeGender.WardrobeGender; } set { wardrobeGender.WardrobeGender = value; } }
        public bool Inactive { get { return wardrobeGender.Inactive; } set { wardrobeGender.Inactive = value; } }
        public string DateStamp { get { return wardrobeGender.DateStamp; } set { wardrobeGender.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
