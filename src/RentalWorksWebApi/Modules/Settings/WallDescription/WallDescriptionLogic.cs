using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Settings.WallDescription
{
    public class WallDescriptionLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WallDescriptionRecord wallDescription = new WallDescriptionRecord();
        public WallDescriptionLogic()
        {
            dataRecords.Add(wallDescription);
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string WallDescriptionId { get { return wallDescription.WallDescriptionId; } set { wallDescription.WallDescriptionId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string WallDescription { get { return wallDescription.WallDescription; } set { wallDescription.WallDescription = value; } }
        public bool Inactive { get { return wallDescription.Inactive; } set { wallDescription.Inactive = value; } }
        public string DateStamp { get { return wallDescription.DateStamp; } set { wallDescription.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}