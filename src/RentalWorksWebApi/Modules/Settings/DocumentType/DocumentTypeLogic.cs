using FwStandard.AppManager;
using WebApi.Logic;

namespace WebApi.Modules.Settings.DocumentType
{
    [FwLogic(Id:"92OpQBW9eHuJ")]
    public class DocumentTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------
        DocumentTypeRecord documentType = new DocumentTypeRecord();
        public DocumentTypeLogic()
        {
            dataRecords.Add(documentType);
        }
        //------------------------------------------------------------------------------------
        [FwLogicProperty(Id:"D1dq7rpeHRgb", IsPrimaryKey:true)]
        public string DocumentTypeId { get { return documentType.DocumentTypeId; } set { documentType.DocumentTypeId = value; } }

        [FwLogicProperty(Id:"D1dq7rpeHRgb", IsRecordTitle:true)]
        public string DocumentType { get { return documentType.DocumentType; } set { documentType.DocumentType = value; } }

        [FwLogicProperty(Id:"HhgINLF9ueSr")]
        public bool? Floorplan { get { return documentType.Floorplan;  } set { documentType.Floorplan = value; } }

        [FwLogicProperty(Id:"Uexs1gsazZGj")]
        public bool? Videos { get { return documentType.Videos; } set { documentType.Videos = value; } }

        [FwLogicProperty(Id:"08JkMzTHpoDV")]
        public bool? Panoramic { get { return documentType.Panoramic; } set { documentType.Panoramic = value; } }

        [FwLogicProperty(Id:"5Ytg1yk4rFWy")]
        public bool? AutomaticallyAttachToEmail { get { return documentType.AutomaticallyAttachToEmail; } set { documentType.AutomaticallyAttachToEmail = value; } }

        [FwLogicProperty(Id:"Bn4vWqxGgXvr")]
        public bool? Inactive { get { return documentType.Inactive; } set { documentType.Inactive = value; } }

        [FwLogicProperty(Id:"UdYLPEtflHuG")]
        public string DateStamp { get { return documentType.DateStamp; } set { documentType.DateStamp = value; } }

        //------------------------------------------------------------------------------------
    }

}
