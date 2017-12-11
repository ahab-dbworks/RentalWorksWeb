using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;

namespace WebApi.Modules.Settings.PhotographyType
{
    public class PhotographyTypeLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        PhotographyTypeRecord photographyType = new PhotographyTypeRecord();
        public PhotographyTypeLogic()
        {
            dataRecords.Add(photographyType);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string PhotographyTypeId { get { return photographyType.PhotographyTypeId; } set { photographyType.PhotographyTypeId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string PhotographyType { get { return photographyType.PhotographyType; } set { photographyType.PhotographyType = value; } }
        public bool? Inactive { get { return photographyType.Inactive; } set { photographyType.Inactive = value; } }
        public string DateStamp { get { return photographyType.DateStamp; } set { photographyType.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
