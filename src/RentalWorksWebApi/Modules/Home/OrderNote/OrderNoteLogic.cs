using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Home.OrderNote
{
    public class OrderNoteLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderNoteRecord orderNote = new OrderNoteRecord();
        OrderNoteLoader orderNoteLoader = new OrderNoteLoader();
        public OrderNoteLogic()
        {
            dataRecords.Add(orderNote);
            dataLoader = orderNoteLoader;
            orderNote.AfterSave += OnAfterSaveOrderNote;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string OrderNoteId { get { return orderNote.OrderNoteId; } set { orderNote.OrderNoteId = value; } }
        public string OrderId { get { return orderNote.OrderId; } set { orderNote.OrderId = value; } }
        public string NoteDate { get { return orderNote.NoteDate; } set { orderNote.NoteDate = value; } }
        public string UserId { get { return orderNote.UserId; } set { orderNote.UserId = value; } }
        public string NotesDescription { get { return orderNote.NotesDescription; } set { orderNote.NotesDescription = value; } }
        public bool? Billing { get { return orderNote.Billing; } set { orderNote.Billing = value; } }
        public bool? PrintOnOrder { get { return orderNote.PrintOnOrder; } set { orderNote.PrintOnOrder = value; } }
        public bool? CopyToInvoice { get { return orderNote.CopyToInvoice; } set { orderNote.CopyToInvoice = value; } }
        public bool? ScheduleNote { get { return orderNote.ScheduleNote; } set { orderNote.ScheduleNote = value; } }
        public string ScheduleNoteDate { get { return orderNote.ScheduleNoteDate; } set { orderNote.ScheduleNoteDate = value; } }
        public bool? PickList { get { return orderNote.PickList; } set { orderNote.PickList = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string UserName { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Notes { get; set; }
        public string DateStamp { get { return orderNote.DateStamp; } set { orderNote.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        public void OnAfterSaveOrderNote(object sender, AfterSaveEventArgs e)
        {
            bool saved = false;
            if (e.SavePerformed)
            {
                saved = orderNote.SaveNoteASync(Notes).Result;
            }
        }
        //------------------------------------------------------------------------------------
    }
}