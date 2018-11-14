using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.Home.OrderNote;

namespace WebApi.Modules.Home.ProjectNote
{
    [FwLogic(Id:"uTPuKRxiRNO7l")]
    public class ProjectNoteLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderNoteRecord projectNote = new OrderNoteRecord();
        ProjectNoteLoader projectNoteLoader = new ProjectNoteLoader();
        public ProjectNoteLogic()
        {
            dataRecords.Add(projectNote);
            dataLoader = projectNoteLoader;
            projectNote.AfterSave += OnAfterSaveProjectNote;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"VpegZ8ChXtajH", IsPrimaryKey:true)]
        public string ProjectNoteId { get { return projectNote.OrderNoteId; } set { projectNote.OrderNoteId = value; } }

        [FwLogicProperty(Id:"726gBOXbOa10")]
        public string ProjectId { get { return projectNote.OrderId; } set { projectNote.OrderId = value; } }

        [FwLogicProperty(Id:"xDLYnY8u6vyL")]
        public string NoteDate { get { return projectNote.NoteDate; } set { projectNote.NoteDate = value; } }

        [FwLogicProperty(Id:"z722MfSEcDJP")]
        public string UserId { get { return projectNote.UserId; } set { projectNote.UserId = value; } }

        [FwLogicProperty(Id:"kMvQau8vMfV2")]
        public string NotesDescription { get { return projectNote.NotesDescription; } set { projectNote.NotesDescription = value; } }

        [FwLogicProperty(Id:"3VqmmtCh3Iyz")]
        public bool? PrintOnProject { get { return projectNote.PrintOnOrder; } set { projectNote.PrintOnOrder = value; } }

        [FwLogicProperty(Id:"llnRpetBsWxPq", IsReadOnly:true)]
        public string UserName { get; set; }

        [FwLogicProperty(Id:"ABpYkbtePaipN", IsReadOnly:true)]
        public string Notes { get; set; }

        [FwLogicProperty(Id:"cE2sxHxgOzhg")]
        public string DateStamp { get { return projectNote.DateStamp; } set { projectNote.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
        public void OnAfterSaveProjectNote(object sender, AfterSaveDataRecordEventArgs e)
        {
            bool saved = projectNote.SaveNoteASync(Notes).Result;
        }
        //------------------------------------------------------------------------------------
    }
}
