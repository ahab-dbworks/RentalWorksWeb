using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.Home.Contract;
using WebApi.Modules.Home.DealOrder;
using WebApi.Modules.Home.DealOrderDetail;
using WebLibrary;

namespace WebApi.Modules.Home.PurchaseOrder
{
    public class PurchaseOrderLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        DealOrderRecord purchaseOrder = new DealOrderRecord();
        DealOrderDetailRecord purchaseOrderDetail = new DealOrderDetailRecord();
        PurchaseOrderLoader purchaseOrderLoader = new PurchaseOrderLoader();
        PurchaseOrderBrowseLoader purchaseOrderBrowseLoader = new PurchaseOrderBrowseLoader();
        public PurchaseOrderLogic()
        {
            dataRecords.Add(purchaseOrder);
            dataLoader = purchaseOrderLoader;
            browseLoader = purchaseOrderBrowseLoader;

            Type = RwConstants.ORDER_TYPE_PURCHASE_ORDER;

        }
        //------------------------------------------------------------------------------------ 
        public string PurchaseOrderId { get { return purchaseOrder.OrderId; } set { purchaseOrder.OrderId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string PurchaseOrderNumber { get { return purchaseOrder.OrderNumber; } set { purchaseOrder.OrderNumber = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Description { get { return purchaseOrder.Description; } set { purchaseOrder.Description = value; } }
        [JsonIgnore]
        public string Type { get { return purchaseOrder.Type; } set { purchaseOrder.Type = value; } }
        public string PurchaseOrderDate { get { return purchaseOrder.OrderDate; } set { purchaseOrder.OrderDate = value; } }
        public string RequisitionNumber { get { return purchaseOrder.RequisitionNumber; } set { purchaseOrder.RequisitionNumber = value; } }
        public string RequisitionDate { get { return purchaseOrder.RequisitionDate; } set { purchaseOrder.RequisitionDate = value; } }
        public string VendorId { get { return purchaseOrder.VendorId; } set { purchaseOrder.VendorId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Vendor { get; set; }
        public string AgentId { get { return purchaseOrder.AgentId; } set { purchaseOrder.AgentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Agent { get; set; }
        public string Status { get { return purchaseOrder.Status; } set { purchaseOrder.Status = value; } }
        public string StatusDate { get { return purchaseOrder.StatusDate; } set { purchaseOrder.StatusDate = value; } }
        public string ReferenceNumber { get { return purchaseOrder.ReferenceNumber; } set { purchaseOrder.ReferenceNumber = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? NeedsApproval { get; set; }
        public string ApprovedByUserId { get { return purchaseOrder.ApprovedByUserId; } set { purchaseOrder.ApprovedByUserId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        //public string ApprovedBySecondUserId { get; set; }
        public string DepartmentId { get { return purchaseOrder.DepartmentId; } set { purchaseOrder.DepartmentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Department { get; set; }
        public string OfficeLocationId { get { return purchaseOrder.OfficeLocationId; } set { purchaseOrder.OfficeLocationId = value; } }
        public string WarehouseId { get { return purchaseOrder.WarehouseId; } set { purchaseOrder.WarehouseId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? QuantityHolding { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public int? QuantityToBarCode { get; set; }
        public bool? Rental { get { return purchaseOrder.Rental; } set { purchaseOrder.Rental = value; } }
        public bool? Sales { get { return purchaseOrder.Sales; } set { purchaseOrder.Sales = value; } }
        public bool? Parts { get { return purchaseOrder.Parts; } set { purchaseOrder.Parts = value; } }
        public bool? Labor { get { return purchaseOrder.Labor; } set { purchaseOrder.Labor = value; } }
        public bool? Miscellaneous { get { return purchaseOrder.Miscellaneous; } set { purchaseOrder.Miscellaneous = value; } }
        public bool? Vehicle { get { return purchaseOrder.Vehicle; } set { purchaseOrder.Vehicle = value; } }
        public bool? SubRent { get { return purchaseOrder.SubRent; } set { purchaseOrder.SubRent = value; } }
        public bool? SubSale { get { return purchaseOrder.SubSale; } set { purchaseOrder.SubSale = value; } }
        public bool? SubLabor { get { return purchaseOrder.SubLabor; } set { purchaseOrder.SubLabor = value; } }
        public bool? SubMiscellaneous { get { return purchaseOrder.SubMiscellaneous; } set { purchaseOrder.SubMiscellaneous = value; } }
        public bool? SubVehicle { get { return purchaseOrder.SubVehicle; } set { purchaseOrder.SubVehicle = value; } }
        public bool? Repair { get { return purchaseOrder.Repair; } set { purchaseOrder.Repair = value; } }
        public bool? Consignment { get { return purchaseOrder.Consignment; } set { purchaseOrder.Consignment = value; } }
        public string ConsignorAgreementId { get { return purchaseOrder.ConsignorAgreementId; } set { purchaseOrder.ConsignorAgreementId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ConsignorAgreementNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DealNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Deal { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TaxOptionId { get; set; }
        public string RateType { get { return purchaseOrder.RateType; } set { purchaseOrder.RateType = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? DepartmentLocationRequiresApproval { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? Total { get; set; }
        public string PoTypeId { get { return purchaseOrder.OrderTypeId; } set { purchaseOrder.OrderTypeId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PoType { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string RequiredByDate { get; set; }
        public string PoClassificationId { get { return purchaseOrder.PoClassificationId; } set { purchaseOrder.PoClassificationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PoClassification { get; set; }
        public string EstimatedStartDate { get { return purchaseOrder.EstimatedStartDate; } set { purchaseOrder.EstimatedStartDate = value; } }
        public string EstimatedStopDate { get { return purchaseOrder.EstimatedStopDate; } set { purchaseOrder.EstimatedStopDate = value; } }
        public string BillingStartDate { get { return purchaseOrder.BillingStartDate; } set { purchaseOrder.BillingStartDate = value; } }
        public string BillingEndDate { get { return purchaseOrder.BillingEndDate; } set { purchaseOrder.BillingEndDate = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public decimal? InvoicedAmount { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public decimal? WeeklyExtended { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string PoApprovalStatusId { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string PoApprovalStatus { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string PoApprovalStatustype { get; set; }
        public string ProjectManagerId { get { return purchaseOrder.ProjectManagerId; } set { purchaseOrder.ProjectManagerId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ProjectManager { get; set; }
        public string OutDeliveryId { get { return purchaseOrder.OutDeliveryId; } set { purchaseOrder.OutDeliveryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool? DropShip { get; set; }
        public string InDeliveryId { get { return purchaseOrder.InDeliveryId; } set { purchaseOrder.InDeliveryId = value; } }
        public string ProjectId { get { return purchaseOrder.ProjectId; } set { purchaseOrder.ProjectId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ProjectNumber { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ProjectDescription { get; set; }
        public string Location { get { return purchaseOrder.Location; } set { purchaseOrder.Location = value; } }
        public string CurrencyId { get { return purchaseOrder.CurrencyId; } set { purchaseOrder.CurrencyId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string CurrencyCode { get; set; }
        //------------------------------------------------------------------------------------ 
        public async Task<string> CreateReceiveContract()
        {
            string contractId = await purchaseOrder.CreateReceiveContract();
            //string[] keys = { contractId };

            //ContractLogic c = new ContractLogic();
            //c.SetDependencies(AppConfig, UserSession);
            //bool x = await c.LoadAsync<ContractLogic>(keys);
            //return c;
            return contractId;
        }
        //------------------------------------------------------------------------------------
    }
}
