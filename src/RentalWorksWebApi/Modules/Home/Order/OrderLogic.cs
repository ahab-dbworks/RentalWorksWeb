using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Home.DealOrder;
using RentalWorksWebApi.Modules.Home.DealOrderDetail;

namespace RentalWorksWebApi.Modules.Home.Order
{
    public class OrderLogic : RwBusinessLogic
    {
        DealOrderRecord dealOrder = new DealOrderRecord();
        DealOrderDetailRecord dealOrderDetail = new DealOrderDetailRecord();
        OrderLoader orderLoader = new OrderLoader();
        //------------------------------------------------------------------------------------
        public OrderLogic()
        {
            dataRecords.Add(dealOrder);
            dataRecords.Add(dealOrderDetail);
            dataLoader = orderLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string OrderId { get { return dealOrder.OrderId; } set { dealOrder.OrderId = value; dealOrderDetail.OrderId = value; } }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isRecordTitle: true)]
        public string OrderNumber { get { return dealOrder.OrderNumber; } set { dealOrder.OrderNumber = value; } }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Description { get { return dealOrder.Description; } set { dealOrder.Description = value; } }
        //------------------------------------------------------------------------------------
        public string OrderDate { get { return dealOrder.OrderDate; } set { dealOrder.OrderDate = value; } }
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
        public string Status { get { return dealOrder.Status; } set { dealOrder.Status = value; } }
        //------------------------------------------------------------------------------------
        public string StatusDate { get { return dealOrder.StatusDate; } set { dealOrder.StatusDate = value; } }
        //------------------------------------------------------------------------------------
        [JsonIgnore]
        public string OrderType { get { return dealOrder.OrderType; } set { dealOrder.OrderType = value; } }
        //------------------------------------------------------------------------------------
        public decimal? MaximumCumulativeDiscount { get { return dealOrderDetail.MaximumCumulativeDiscount; } set { dealOrderDetail.MaximumCumulativeDiscount = value; } }
        //------------------------------------------------------------------------------------
        public string PoApprovalStatusId { get { return dealOrderDetail.PoApprovalStatusId; } set { dealOrderDetail.PoApprovalStatusId = value; } }
        //------------------------------------------------------------------------------------
        public string DateStamp { get { return dealOrder.DateStamp; } set { dealOrder.DateStamp = value; dealOrderDetail.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }
}
