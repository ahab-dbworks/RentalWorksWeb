using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
using WebApi.Modules.Home.OrderNote;

namespace WebApi.Modules.Home.ProjectNote
{
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string ProjectNoteId { get { return projectNote.OrderNoteId; } set { projectNote.OrderNoteId = value; } }
        public string ProjectId { get { return projectNote.OrderId; } set { projectNote.OrderId = value; } }
        public string NoteDate { get { return projectNote.NoteDate; } set { projectNote.NoteDate = value; } }
        public string UserId { get { return projectNote.UserId; } set { projectNote.UserId = value; } }
        public string NotesDescription { get { return projectNote.NotesDescription; } set { projectNote.NotesDescription = value; } }
        public bool? PrintOnProject { get { return projectNote.PrintOnOrder; } set { projectNote.PrintOnOrder = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string UserName { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Notes { get; set; }
        public string DateStamp { get { return projectNote.DateStamp; } set { projectNote.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        public void OnAfterSaveProjectNote(object sender, AfterSaveEventArgs e)
        {
            bool saved = false;
            if (e.SavePerformed)
            {
                saved = projectNote.SaveNoteASync(Notes).Result;
            }
        }
        //------------------------------------------------------------------------------------
    }
}