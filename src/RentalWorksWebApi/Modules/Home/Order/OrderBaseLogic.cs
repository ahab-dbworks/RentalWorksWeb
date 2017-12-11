using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using WebApi.Logic;
using WebApi.Modules.Home.DealOrder;
using WebApi.Modules.Home.DealOrderDetail;

namespace WebApi.Modules.Home.Order
{
    public class OrderBaseLogic : RwBusinessLogic
    {
        protected DealOrderRecord dealOrder = new DealOrderRecord();
        protected DealOrderDetailRecord dealOrderDetail = new DealOrderDetailRecord();
        //------------------------------------------------------------------------------------
        public OrderBaseLogic()
        {
            dataRecords.Add(dealOrder);
            dataRecords.Add(dealOrderDetail);
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Description { get { return dealOrder.Description; } set { dealOrder.Description = value; } }
        //------------------------------------------------------------------------------------
        public string OfficeLocationId { get { return dealOrder.OfficeLocationId; } set { dealOrder.OfficeLocationId = value; } }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public string OfficeLocation { get; set; }
        //------------------------------------------------------------------------------------
        public string WarehouseId { get { return dealOrder.WarehouseId; } set { dealOrder.WarehouseId = value; } }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public string Warehouse { get; set; }
        //------------------------------------------------------------------------------------
        public string DepartmentId { get { return dealOrder.DepartmentId; } set { dealOrder.DepartmentId = value; } }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public string Department { get; set; }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public string CustomerId { get; set; }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public string Customer { get; set; }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public string CustomerNumber { get; set; }
        //------------------------------------------------------------------------------------
        public string DealId { get { return dealOrder.DealId; } set { dealOrder.DealId = value; } }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public string Deal { get; set; }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public string DealNumber { get; set; }
        //------------------------------------------------------------------------------------
        public string RateType { get { return dealOrder.RateType; } set { dealOrder.RateType = value; } }
        //------------------------------------------------------------------------------------

        public bool? Rental { get { return dealOrder.Rental; } set { dealOrder.Rental = value; } }
        //------------------------------------------------------------------------------------ 
        public bool? Sales { get { return dealOrder.Sales; } set { dealOrder.Sales = value; } }
        //------------------------------------------------------------------------------------ 
        public bool? Miscellaneous { get { return dealOrder.Miscellaneous; } set { dealOrder.Miscellaneous = value; } }
        //------------------------------------------------------------------------------------ 
        public bool? Labor { get { return dealOrder.Labor; } set { dealOrder.Labor = value; } }
        //------------------------------------------------------------------------------------ 
        public bool? Facilities { get { return dealOrder.Facilities; } set { dealOrder.Facilities = value; } }
        //------------------------------------------------------------------------------------ 
        public bool? Transportation { get { return dealOrder.Transportation; } set { dealOrder.Transportation = value; } }
        //------------------------------------------------------------------------------------ 


        public string PickDate { get { return dealOrder.PickDate; } set { dealOrder.PickDate = value; } }
        //------------------------------------------------------------------------------------
        public string PickTime { get { return dealOrder.PickTime; } set { dealOrder.PickTime = value; } }
        //------------------------------------------------------------------------------------
        public string EstimatedStartDate { get { return dealOrder.EstimatedStartDate; } set { dealOrder.EstimatedStartDate = value; } }
        //------------------------------------------------------------------------------------
        public string EstimatedStartTime { get { return dealOrder.EstimatedStartTime; } set { dealOrder.EstimatedStartTime = value; } }
        //------------------------------------------------------------------------------------
        public string EstimatedStopDate { get { return dealOrder.EstimatedStopDate; } set { dealOrder.EstimatedStopDate = value; } }
        //------------------------------------------------------------------------------------
        public string EstimatedStopTime { get { return dealOrder.EstimatedStopTime; } set { dealOrder.EstimatedStopTime = value; } }
        //------------------------------------------------------------------------------------
        public string OrderTypeId { get { return dealOrder.OrderTypeId; } set { dealOrder.OrderTypeId = value; } }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderType { get; set; }
        //------------------------------------------------------------------------------------
        public bool? FlatPo { get { return dealOrder.FlatPo; } set { dealOrder.FlatPo = value; } }
        //------------------------------------------------------------------------------------
        public bool? PendingPo { get { return dealOrder.PendingPo; } set { dealOrder.PendingPo = value; } }
        //------------------------------------------------------------------------------------
        public string Location { get { return dealOrder.Location; } set { dealOrder.Location = value; } }
        //------------------------------------------------------------------------------------
        public string ReferenceNumber { get { return dealOrder.ReferenceNumber; } set { dealOrder.ReferenceNumber = value; } }
        //------------------------------------------------------------------------------------


        //------------------------------------------------------------------------------------
        public string Status { get { return dealOrder.Status; } set { dealOrder.Status = value; } }
        //------------------------------------------------------------------------------------
        public string StatusDate { get { return dealOrder.StatusDate; } set { dealOrder.StatusDate = value; } }
        //------------------------------------------------------------------------------------
        [JsonIgnore]
        public string Type { get { return dealOrder.Type; } set { dealOrder.Type = value; } }
        //------------------------------------------------------------------------------------
        public decimal? MaximumCumulativeDiscount { get { return dealOrderDetail.MaximumCumulativeDiscount; } set { dealOrderDetail.MaximumCumulativeDiscount = value; } }
        //------------------------------------------------------------------------------------
        public string PoApprovalStatusId { get { return dealOrderDetail.PoApprovalStatusId; } set { dealOrderDetail.PoApprovalStatusId = value; } }
        //------------------------------------------------------------------------------------
        public string DateStamp { get { return dealOrder.DateStamp; } set { dealOrder.DateStamp = value; dealOrderDetail.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }
}
