using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.DocumentType
{
    public class DocumentTypeLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        DocumentTypeRecord documentType = new DocumentTypeRecord();
        public DocumentTypeLogic()
        {
            dataRecords.Add(documentType);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string DocumentTypeId { get { return documentType.DocumentTypeId; } set { documentType.DocumentTypeId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string DocumentType { get { return documentType.DocumentType; } set { documentType.DocumentType = value; } }
        public bool? Floorplan { get { return documentType.Floorplan;  } set { documentType.Floorplan = value; } }
        public bool? Videos { get { return documentType.Videos; } set { documentType.Videos = value; } }
        public bool? Panoramic { get { return documentType.Panoramic; } set { documentType.Panoramic = value; } }
        public bool? AutomaticallyAttachToEmail { get { return documentType.AutomaticallyAttachToEmail; } set { documentType.AutomaticallyAttachToEmail = value; } }
        public bool? Inactive { get { return documentType.Inactive; } set { documentType.Inactive = value; } }
        public string DateStamp { get { return documentType.DateStamp; } set { documentType.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
