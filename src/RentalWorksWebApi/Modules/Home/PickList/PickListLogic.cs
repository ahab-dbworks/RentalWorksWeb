using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
using static FwStandard.DataLayer.FwDataReadWriteRecord;

namespace WebApi.Modules.Home.PickList
{
    public class PickListLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PickListRecord pickList = new PickListRecord();
        PickListLoader pickListLoader = new PickListLoader();
        public PickListLogic()
        {
            dataRecords.Add(pickList);
            dataLoader = pickListLoader;
            pickList.AfterSaves += OnAfterSavesPickList;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string PickListId { get { return pickList.PickListId; } set { pickList.PickListId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string PickListNumber { get { return pickList.PickListNumber; } set { pickList.PickListNumber = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PickType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PickDate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PickTime { get; set; }
        public string Status { get { return pickList.Status; } set { pickList.Status = value; } }
        public bool? Completed { get { return pickList.Completed; } set { pickList.Completed = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OfficeLocationId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DepartmentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Department { get; set; }
        public string WarehouseId { get { return pickList.WarehouseId; } set { pickList.WarehouseId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WarehouseCode { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Warehouse { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DeliverType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DeliverDate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DeliverTime { get; set; }
        public string OrderId { get { return pickList.OrderId; } set { pickList.OrderId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderDescription { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderType { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SubmittedDate { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SubmittedTime { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SubmittedDateTime { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderedBy { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderedById { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RequestedBy { get; set; }
        public string InputDate { get { return pickList.InputDate; } set { pickList.InputDate = value; } }
        public string InputTime { get { return pickList.InputTime; } set { pickList.InputTime = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string InputDateTime { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? TotalItemQuantity { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DealId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Deal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AgentId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Agent { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AssignedToId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AssignedTo { get; set; }
        public string DueDate { get { return pickList.DueDate; } set { pickList.DueDate = value; } }
        public string DueTime { get { return pickList.DueTime; } set { pickList.DueTime = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Note { get; set; }
        public string DateStamp { get { return pickList.DateStamp; } set { pickList.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 

        public void OnAfterSavesPickList(object sender, SaveEventArgs e)
        {
            bool saved = false;
            saved = pickList.SaveNoteASync(Note).Result;
        }
        //------------------------------------------------------------------------------------
    }
}