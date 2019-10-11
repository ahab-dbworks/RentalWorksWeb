using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.Models;
using System.Threading.Tasks;
using WebApi.Logic;

namespace WebApi.Modules.Warehouse.PickList
{
    [FwLogic(Id:"5FhsIXWKwx14")]
    public class PickListLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PickListRecord pickList = new PickListRecord();
        PickListLoader pickListLoader = new PickListLoader();
        PickListBrowseLoader pickListBrowseLoader = new PickListBrowseLoader();
        public PickListLogic()
        {
            dataRecords.Add(pickList);
            dataLoader = pickListLoader;
            browseLoader = pickListBrowseLoader;
            AfterSave += OnAfterSave;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"2E6750mRpWk5", IsPrimaryKey:true)]
        public string PickListId { get { return pickList.PickListId; } set { pickList.PickListId = value; } }

        [FwLogicProperty(Id:"YY87Fs5rhX9H", IsRecordTitle:true)]
        public string PickListNumber { get { return pickList.PickListNumber; } set { pickList.PickListNumber = value; } }

        [FwLogicProperty(Id:"LaIWBayQ0abj", IsReadOnly:true)]
        public string PickType { get; set; }

        [FwLogicProperty(Id:"u4wBnF2iUCad", IsReadOnly:true)]
        public string PickDate { get; set; }

        [FwLogicProperty(Id:"Nv85nhFf6RSY", IsReadOnly:true)]
        public string PickTime { get; set; }

        [FwLogicProperty(Id:"VfyIlQfa56Rc")]
        public string Status { get { return pickList.Status; } set { pickList.Status = value; } }

        [FwLogicProperty(Id:"8zwOhuBJ2xjM")]
        public bool? Completed { get { return pickList.Completed; } set { pickList.Completed = value; } }

        [FwLogicProperty(Id:"TKvx8alO52AF", IsReadOnly:true)]
        public string OfficeLocationId { get; set; }

        [FwLogicProperty(Id:"CYz4ySY1AMOf", IsReadOnly:true)]
        public string DepartmentId { get; set; }

        [FwLogicProperty(Id:"CYz4ySY1AMOf", IsReadOnly:true)]
        public string Department { get; set; }

        [FwLogicProperty(Id:"YoZyvgdhYuFq")]
        public string WarehouseId { get { return pickList.WarehouseId; } set { pickList.WarehouseId = value; } }

        [FwLogicProperty(Id:"MAtBYxOG1KKX", IsReadOnly:true)]
        public string WarehouseCode { get; set; }

        [FwLogicProperty(Id:"MAtBYxOG1KKX", IsReadOnly:true)]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id:"wCvA1O65BA6o", IsReadOnly:true)]
        public string DeliverType { get; set; }

        [FwLogicProperty(Id:"HF2Pr8qryk9V", IsReadOnly:true)]
        public string DeliverDate { get; set; }

        [FwLogicProperty(Id:"fk33vpH9QU5g", IsReadOnly:true)]
        public string DeliverTime { get; set; }

        [FwLogicProperty(Id:"zcWv6Cl68aqR")]
        public string OrderId { get { return pickList.OrderId; } set { pickList.OrderId = value; } }

        [FwLogicProperty(Id:"nLYHIVhecSFx", IsReadOnly:true)]
        public string OrderNumber { get; set; }

        [FwLogicProperty(Id:"08KwhrLgLnpg", IsReadOnly:true)]
        public string OrderDescription { get; set; }

        [FwLogicProperty(Id:"vjo12eGxMY3p", IsReadOnly:true)]
        public string OrderType { get; set; }

        [FwLogicProperty(Id:"v8psCXdzuD93", IsReadOnly:true)]
        public string SubmittedDate { get; set; }

        [FwLogicProperty(Id:"x0yfRnOWZaMo", IsReadOnly:true)]
        public string SubmittedTime { get; set; }

        [FwLogicProperty(Id:"v8psCXdzuD93", IsReadOnly:true)]
        public string SubmittedDateTime { get; set; }

        [FwLogicProperty(Id:"VbVkWxJtYkPf", IsReadOnly:true)]
        public string OrderedBy { get; set; }

        [FwLogicProperty(Id:"VbVkWxJtYkPf", IsReadOnly:true)]
        public string OrderedById { get; set; }

        [FwLogicProperty(Id:"buCb09p6H0XN", IsReadOnly:true)]
        public string RequestedBy { get; set; }

        [FwLogicProperty(Id:"zQtwMNgrvr0S")]
        public string InputDate { get { return pickList.InputDate; } set { pickList.InputDate = value; } }

        [FwLogicProperty(Id:"PK0Ga6VvU8ZG")]
        public string InputTime { get { return pickList.InputTime; } set { pickList.InputTime = value; } }

        [FwLogicProperty(Id:"9rbWOicDniot", IsReadOnly:true)]
        public string InputDateTime { get; set; }

        [FwLogicProperty(Id:"5PxlEy5XvCuw", IsReadOnly:true)]
        public int? TotalItemQuantity { get; set; }

        [FwLogicProperty(Id:"s85diE9Xg9RQ", IsReadOnly:true)]
        public string DealId { get; set; }

        [FwLogicProperty(Id:"s85diE9Xg9RQ", IsReadOnly:true)]
        public string Deal { get; set; }

        [FwLogicProperty(Id:"KJRfbCc8mmiB", IsReadOnly:true)]
        public string AgentId { get; set; }

        [FwLogicProperty(Id:"KJRfbCc8mmiB", IsReadOnly:true)]
        public string Agent { get; set; }

        [FwLogicProperty(Id:"TKdQYECGNE9e", IsReadOnly:true)]
        public string AssignedToId { get; set; }

        [FwLogicProperty(Id:"TKdQYECGNE9e", IsReadOnly:true)]
        public string AssignedTo { get; set; }

        [FwLogicProperty(Id:"PhDScLVUsd9z")]
        public string DueDate { get { return pickList.DueDate; } set { pickList.DueDate = value; } }

        [FwLogicProperty(Id:"rz3J2xjSD681")]
        public string DueTime { get { return pickList.DueTime; } set { pickList.DueTime = value; } }

        [FwLogicProperty(Id:"E99iOwMkqRex", IsReadOnly:true)]
        public string Note { get; set; }

        [FwLogicProperty(Id:"H8LBhnIHTXRc")]
        public string DateStamp { get { return pickList.DateStamp; } set { pickList.DateStamp = value; } }

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
                PickListLogic orig = (PickListLogic)e.Original;
                doSaveNote = (!orig.Note.Equals(Note));
            }
            if (doSaveNote)
            {
                bool saved = pickList.SaveNoteASync(Note).Result;
                if (saved)
                {
                    e.RecordsAffected++;
                }
            }
        }
        //------------------------------------------------------------------------------------
        public async Task<bool> LoadFromSession(BrowseRequest browseRequest)
        {
            string id = await pickList.LoadFromSession(browseRequest);
            PickListId = id;
            bool x = await LoadAsync<PickListLogic>();
            return x;
        }
        //------------------------------------------------------------------------------------
    }
}
