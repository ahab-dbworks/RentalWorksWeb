using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
namespace WebApi.Modules.HomeControls.OrderNote
{
    [FwLogic(Id:"M6Jx6yMvDuFz")]
    public class OrderNoteLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderNoteRecord orderNote = new OrderNoteRecord();
        OrderNoteLoader orderNoteLoader = new OrderNoteLoader();
        public OrderNoteLogic()
        {
            dataRecords.Add(orderNote);
            dataLoader = orderNoteLoader;
            AfterSave += OnAfterSave;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"KT9nCiwJUIPo", IsPrimaryKey:true)]
        public string OrderNoteId { get { return orderNote.OrderNoteId; } set { orderNote.OrderNoteId = value; } }

        [FwLogicProperty(Id:"HGOvj5ew1exw")]
        public string OrderId { get { return orderNote.OrderId; } set { orderNote.OrderId = value; } }

        [FwLogicProperty(Id:"s7nOTLQqTmSs")]
        public string NoteDate { get { return orderNote.NoteDate; } set { orderNote.NoteDate = value; } }

        [FwLogicProperty(Id:"nHTRj8gqzu85")]
        public string UserId { get { return orderNote.UserId; } set { orderNote.UserId = value; } }

        [FwLogicProperty(Id:"U7Wgg1thvuGF")]
        public string NotesDescription { get { return orderNote.NotesDescription; } set { orderNote.NotesDescription = value; } }

        [FwLogicProperty(Id:"7Vb5zJcs5lm5")]
        public bool? Billing { get { return orderNote.Billing; } set { orderNote.Billing = value; } }

        [FwLogicProperty(Id:"HlvhwwwE533d")]
        public bool? PrintOnOrder { get { return orderNote.PrintOnOrder; } set { orderNote.PrintOnOrder = value; } }

        [FwLogicProperty(Id:"YhZGq5zRyKiZ")]
        public bool? CopyToInvoice { get { return orderNote.CopyToInvoice; } set { orderNote.CopyToInvoice = value; } }

        [FwLogicProperty(Id:"w1dtBYv2zelP")]
        public bool? ScheduleNote { get { return orderNote.ScheduleNote; } set { orderNote.ScheduleNote = value; } }

        [FwLogicProperty(Id:"BLxJnqVuTHNm")]
        public string ScheduleNoteDate { get { return orderNote.ScheduleNoteDate; } set { orderNote.ScheduleNoteDate = value; } }

        [FwLogicProperty(Id:"bMSDWXasfehG")]
        public bool? PickList { get { return orderNote.PickList; } set { orderNote.PickList = value; } }

        [FwLogicProperty(Id:"xHp1mVNvA8TU", IsReadOnly:true)]
        public string UserName { get; set; }

        [FwLogicProperty(Id:"AJygnsJgaPzK", IsReadOnly:true)]
        public string Notes { get; set; }

        [FwLogicProperty(Id:"jGhz4yuH6RFU")]
        public string DateStamp { get { return orderNote.DateStamp; } set { orderNote.DateStamp = value; } }

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
                OrderNoteLogic orig = (OrderNoteLogic)e.Original;
                doSaveNote = (!orig.Notes.Equals(Notes));
            }
            if (doSaveNote)
            {
                bool saved = orderNote.SaveNoteASync(Notes).Result;
                if (saved)
                {
                    e.RecordsAffected++;
                }
            }
        }
        //------------------------------------------------------------------------------------
    }
}
