using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
using static FwStandard.DataLayer.FwDataReadWriteRecord;

namespace RentalWorksWebApi.Modules.Settings.OrderTypeNote
{
    public class OrderTypeNoteLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderTypeNoteRecord orderTypeNote = new OrderTypeNoteRecord();
        OrderTypeNoteLoader orderTypeNoteLoader = new OrderTypeNoteLoader();
        public OrderTypeNoteLogic()
        {
            dataRecords.Add(orderTypeNote);
            dataLoader = orderTypeNoteLoader;
            orderTypeNote.AfterSaves += OnAfterSavesOrderTypeNote;
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
        public void OnAfterSavesOrderTypeNote(object sender, SaveEventArgs e)
        {
            bool saved = false;
            saved = orderTypeNote.SaveNoteASync(Notes).Result;
        }
        //------------------------------------------------------------------------------------
    }
}