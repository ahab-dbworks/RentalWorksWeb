using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.WardrobeLabel
{
    public class WardrobeLabelLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        WardrobeLabelRecord wardrobeLabel = new WardrobeLabelRecord();
        public WardrobeLabelLogic()
        {
            dataRecords.Add(wardrobeLabel);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string WardrobeLabelId { get { return wardrobeLabel.WardrobeLabelId; } set { wardrobeLabel.WardrobeLabelId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string WardrobeLabel { get { return wardrobeLabel.WardrobeLabel; } set { wardrobeLabel.WardrobeLabel = value; } }
        public bool Inactive { get { return wardrobeLabel.Inactive; } set { wardrobeLabel.Inactive = value; } }
        public string DateStamp { get { return wardrobeLabel.DateStamp; } set { wardrobeLabel.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
