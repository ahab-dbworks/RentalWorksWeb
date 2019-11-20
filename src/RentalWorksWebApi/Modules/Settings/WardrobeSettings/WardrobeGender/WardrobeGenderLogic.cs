using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.WardrobeSettings.WardrobeGender
{
    [FwLogic(Id:"26gtbpqV1ejf6")]
    public class WardrobeGenderLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        WardrobeGenderRecord wardrobeGender = new WardrobeGenderRecord();
        public WardrobeGenderLogic()
        {
            dataRecords.Add(wardrobeGender);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"dxPGd4PKOFMFS", IsPrimaryKey:true)]
        public string WardrobeGenderId { get { return wardrobeGender.WardrobeGenderId; } set { wardrobeGender.WardrobeGenderId = value; } }

        [FwLogicProperty(Id:"dxPGd4PKOFMFS", IsRecordTitle:true)]
        public string WardrobeGender { get { return wardrobeGender.WardrobeGender; } set { wardrobeGender.WardrobeGender = value; } }

        [FwLogicProperty(Id:"UuHORTJuIUVH")]
        public bool? Inactive { get { return wardrobeGender.Inactive; } set { wardrobeGender.Inactive = value; } }

        [FwLogicProperty(Id:"VsauwQTHFmtY")]
        public string DateStamp { get { return wardrobeGender.DateStamp; } set { wardrobeGender.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
