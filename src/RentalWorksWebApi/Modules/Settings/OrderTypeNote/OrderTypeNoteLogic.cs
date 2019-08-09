using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using WebApi.Logic;
using static FwStandard.Data.FwDataReadWriteRecord;

namespace WebApi.Modules.Settings.OrderTypeNote
{
    [FwLogic(Id:"dWxjXdmoe4FK")]
    public class OrderTypeNoteLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderTypeNoteRecord orderTypeNote = new OrderTypeNoteRecord();
        OrderTypeNoteLoader orderTypeNoteLoader = new OrderTypeNoteLoader();
        public OrderTypeNoteLogic()
        {
            dataRecords.Add(orderTypeNote);
            dataLoader = orderTypeNoteLoader;
            AfterSave += OnAfterSave;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"PujUDTh53MsO", IsPrimaryKey:true)]
        public string OrderTypeNoteId { get { return orderTypeNote.OrderTypeNoteId; } set { orderTypeNote.OrderTypeNoteId = value; } }

        [FwLogicProperty(Id:"LLWyc5lwrTQ")]
        public string OrderTypeId { get { return orderTypeNote.OrderTypeId; } set { orderTypeNote.OrderTypeId = value; } }

        [FwLogicProperty(Id:"eMMrgt9O1aZ")]
        public string Description { get { return orderTypeNote.Description; } set { orderTypeNote.Description = value; } }

        [FwLogicProperty(Id:"JBqrk2V2Fhf")]
        public bool? Billing { get { return orderTypeNote.Billing; } set { orderTypeNote.Billing = value; } }

        [FwLogicProperty(Id:"H7ROxGxYFfj")]
        public bool? PrintOnOrder { get { return orderTypeNote.PrintOnOrder; } set { orderTypeNote.PrintOnOrder = value; } }

        [FwLogicProperty(Id:"273B7IVcWyMf", IsReadOnly:true)]
        public string Notes { get; set; }

        [FwLogicProperty(Id:"qltZmJ1fIou")]
        public string DateStamp { get { return orderTypeNote.DateStamp; } set { orderTypeNote.DateStamp = value; } }

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
                OrderTypeNoteLogic orig = (OrderTypeNoteLogic)e.Original;
                doSaveNote = (!orig.Notes.Equals(Notes));
            }
            if (doSaveNote)
            {
                bool saved = orderTypeNote.SaveNoteASync(Notes).Result;
                if (saved)
                {
                    e.RecordsAffected++;
                }
            }
        }
        //------------------------------------------------------------------------------------
    }
}
