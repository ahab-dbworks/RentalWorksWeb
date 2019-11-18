using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.PhotographyType
{
    [FwLogic(Id:"pSgs7Z6fOEkW")]
    public class PhotographyTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        PhotographyTypeRecord photographyType = new PhotographyTypeRecord();
        public PhotographyTypeLogic()
        {
            dataRecords.Add(photographyType);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"rBXNXKWfrGwn", IsPrimaryKey:true)]
        public string PhotographyTypeId { get { return photographyType.PhotographyTypeId; } set { photographyType.PhotographyTypeId = value; } }

        [FwLogicProperty(Id:"rBXNXKWfrGwn", IsRecordTitle:true)]
        public string PhotographyType { get { return photographyType.PhotographyType; } set { photographyType.PhotographyType = value; } }

        [FwLogicProperty(Id:"qHNl5r2xziGV")]
        public bool? Inactive { get { return photographyType.Inactive; } set { photographyType.Inactive = value; } }

        [FwLogicProperty(Id:"U8VpENRTi8jl")]
        public string DateStamp { get { return photographyType.DateStamp; } set { photographyType.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
