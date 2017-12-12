using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Settings.WardrobeSource
{
    public class WardrobeSourceLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        WardrobeSourceRecord wardrobeSource = new WardrobeSourceRecord();
        public WardrobeSourceLogic()
        {
            dataRecords.Add(wardrobeSource);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string WardrobeSourceId { get { return wardrobeSource.WardrobeSourceId; } set { wardrobeSource.WardrobeSourceId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string WardrobeSource { get { return wardrobeSource.WardrobeSource; } set { wardrobeSource.WardrobeSource = value; } }
        public bool? Inactive { get { return wardrobeSource.Inactive; } set { wardrobeSource.Inactive = value; } }
        public string DateStamp { get { return wardrobeSource.DateStamp; } set { wardrobeSource.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }
}
