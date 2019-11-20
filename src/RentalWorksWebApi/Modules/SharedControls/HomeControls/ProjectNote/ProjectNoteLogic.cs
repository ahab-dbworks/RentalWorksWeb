using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using WebApi.Modules.HomeControls.OrderNote;

namespace WebApi.Modules.HomeControls.ProjectNote
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
            AfterSave += OnAfterSave;
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
        public void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
            bool doSaveNote = false;
            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                doSaveNote = true;
            }
            else if (e.Original != null)
            {
                ProjectNoteLogic orig = (ProjectNoteLogic)e.Original;
                doSaveNote = (!orig.Notes.Equals(Notes));
            }
            if (doSaveNote)
            {
                bool saved = projectNote.SaveNoteASync(Notes).Result;
                if (saved)
                {
                    e.RecordsAffected++;
                }
            }
        }
        //------------------------------------------------------------------------------------
    }
}
