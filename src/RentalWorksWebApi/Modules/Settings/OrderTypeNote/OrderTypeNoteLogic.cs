using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
using static FwStandard.DataLayer.FwDataReadWriteRecord;

namespace WebApi.Modules.Settings.OrderTypeNote
{
    public class OrderTypeNoteLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderTypeNoteRecord orderTypeNote = new OrderTypeNoteRecord();
        OrderTypeNoteLoader orderTypeNoteLoader = new OrderTypeNoteLoader();
        public OrderTypeNoteLogic()
        {
            dataRecords.Add(orderTypeNote);
            dataLoader = orderTypeNoteLoader;
            orderTypeNote.AfterSave += OnAfterSaveOrderTypeNote;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string OrderTypeNoteId { get { return orderTypeNote.OrderTypeNoteId; } set { orderTypeNote.OrderTypeNoteId = value; } }
        public string OrderTypeId { get { return orderTypeNote.OrderTypeId; } set { orderTypeNote.OrderTypeId = value; } }
        public string Description { get { return orderTypeNote.Description; } set { orderTypeNote.Description = value; } }
        public bool? Billing { get { return orderTypeNote.Billing; } set { orderTypeNote.Billing = value; } }
        public bool? PrintOnOrder { get { return orderTypeNote.PrintOnOrder; } set { orderTypeNote.PrintOnOrder = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Notes { get; set; }
        public string DateStamp { get { return orderTypeNote.DateStamp; } set { orderTypeNote.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        public void OnAfterSaveOrderTypeNote(object sender, AfterSaveEventArgs e)
        {
            bool saved = false;
            if (e.SavePerformed)
            {
                saved = orderTypeNote.SaveNoteASync(Notes).Result;
            }
        }
        //------------------------------------------------------------------------------------
    }
}