using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using WebApi.Logic;
using WebApi.Modules.Agent.Vendor;
using WebApi.Modules.HomeControls.DealOrder;
using WebApi.Modules.HomeControls.DealOrderDetail;
using WebApi.Modules.HomeControls.Tax;
using WebApi.Modules.Settings.SystemSettings.DefaultSettings;
using static WebApi.Modules.HomeControls.DealOrder.DealOrderRecord;
using WebApi.Modules.HomeControls.Delivery;
using WebApi.Modules.HomeControls.OrderDates;
using System.Collections.Generic;
using WebApi.Modules.Agent.Order;
using WebApi.Modules.Settings.OrderTypeDateType;

namespace WebApi.Modules.Agent.PurchaseOrder
{
    [FwLogic(Id: "XRpbTRGJFX88z")]
    public class PurchaseOrderLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        DealOrderRecord purchaseOrder = new DealOrderRecord();
        DealOrderDetailRecord purchaseOrderDetail = new DealOrderDetailRecord();
        TaxRecord tax = new TaxRecord();
        protected DeliveryRecord receiveDelivery = new DeliveryRecord();
        protected DeliveryRecord returnDelivery = new DeliveryRecord();

        PurchaseOrderLoader purchaseOrderLoader = new PurchaseOrderLoader();
        PurchaseOrderBrowseLoader purchaseOrderBrowseLoader = new PurchaseOrderBrowseLoader();

        private bool _changeRatesToNewCurrency = false;

        public PurchaseOrderLogic()
        {
            dataRecords.Add(purchaseOrder);
            dataRecords.Add(purchaseOrderDetail);
            dataRecords.Add(tax);
            dataRecords.Add(receiveDelivery);
            dataRecords.Add(returnDelivery);

            dataLoader = purchaseOrderLoader;
            browseLoader = purchaseOrderBrowseLoader;

            BeforeSave += OnBeforeSave;
            AfterSave += OnAfterSave;
            purchaseOrder.BeforeSave += OnBeforeSavePurchaseOrder;
            purchaseOrder.AfterSave += OnAfterSavePurchaseOrder;
            tax.AfterSave += OnAfterSaveTax;
            purchaseOrderDetail.AssignPrimaryKeys += OrderDetailAssignPrimaryKeys;

            Type = RwConstants.ORDER_TYPE_PURCHASE_ORDER;
            ForceSave = true;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "VIVLmYhhFSMVm", IsPrimaryKey: true)]
        public string PurchaseOrderId { get { return purchaseOrder.OrderId; } set { purchaseOrder.OrderId = value; purchaseOrderDetail.OrderId = value; } }

        [FwLogicProperty(Id: "yq3FBSQUmjuzX", IsRecordTitle: true)]
        public string PurchaseOrderNumber { get { return purchaseOrder.OrderNumber; } set { purchaseOrder.OrderNumber = value; } }

        [FwLogicProperty(Id: "fDewVmifAdGcP", IsRecordTitle: true)]
        public string Description { get { return purchaseOrder.Description; } set { purchaseOrder.Description = value; } }

        [JsonIgnore]
        [FwLogicProperty(Id: "R3qdAmIZf7SZ", DisableDirectModify: true)]
        public string Type { get { return purchaseOrder.Type; } set { purchaseOrder.Type = value; } }

        [FwLogicProperty(Id: "eMypPac6XH9t")]
        public string PurchaseOrderDate { get { return purchaseOrder.OrderDate; } set { purchaseOrder.OrderDate = value; } }

        [FwLogicProperty(Id: "eS25roYPI2hU")]
        public string RequisitionNumber { get { return purchaseOrder.RequisitionNumber; } set { purchaseOrder.RequisitionNumber = value; } }

        [FwLogicProperty(Id: "5My8t08kcKoZ")]
        public string RequisitionDate { get { return purchaseOrder.RequisitionDate; } set { purchaseOrder.RequisitionDate = value; } }

        [FwLogicProperty(Id: "UP6Z8mbFg6oB")]
        public string VendorId { get { return purchaseOrder.VendorId; } set { purchaseOrder.VendorId = value; } }

        [FwLogicProperty(Id: "QEgN82QcXGeOm", IsReadOnly: true)]
        public string Vendor { get; set; }

        [FwLogicProperty(Id: "gIEzxsRPhre2")]
        public string AgentId { get { return purchaseOrder.AgentId; } set { purchaseOrder.AgentId = value; } }

        [FwLogicProperty(Id: "oDM7VaQjF3HKd", IsReadOnly: true)]
        public string Agent { get; set; }

        [FwLogicProperty(Id: "GeBFSHGC85Hf", DisableDirectModify: true)]
        public string Status { get { return purchaseOrder.Status; } set { purchaseOrder.Status = value; } }

        [FwLogicProperty(Id: "cpHP83yfqMvW", DisableDirectModify: true)]
        public string StatusDate { get { return purchaseOrder.StatusDate; } set { purchaseOrder.StatusDate = value; } }

        [FwLogicProperty(Id: "xGr0YxadipyP")]
        public string ReferenceNumber { get { return purchaseOrder.ReferenceNumber; } set { purchaseOrder.ReferenceNumber = value; } }

        [FwLogicProperty(Id: "xLqYZa4B3iPaD", IsReadOnly: true)]
        public bool? NeedsApproval { get; set; }

        [FwLogicProperty(Id: "k6URS6OVU8PH")]
        public string ApprovedByUserId { get { return purchaseOrder.ApprovedByUserId; } set { purchaseOrder.ApprovedByUserId = value; } }

        //[FwLogicProperty(Id:"MKsyx4k08raf")]
        //public string ApprovedBySecondUserId { get; set; }

        [FwLogicProperty(Id: "xdSSezDjZXS4")]
        public string DepartmentId { get { return purchaseOrder.DepartmentId; } set { purchaseOrder.DepartmentId = value; } }

        [FwLogicProperty(Id: "TKeJIOFmtxvQa", IsReadOnly: true)]
        public string Department { get; set; }

        [FwLogicProperty(Id: "wMLGaz491jp3", DisableDirectModify: true)]
        public string OfficeLocationId { get { return purchaseOrder.OfficeLocationId; } set { purchaseOrder.OfficeLocationId = value; } }

        [FwLogicProperty(Id: "01yCQOud66XnC", IsReadOnly: true)]
        public string OfficeLocation { get; set; }

        [FwLogicProperty(Id: "uxqpq3ELNy79", DisableDirectModify: true)]
        public string WarehouseId { get { return purchaseOrder.WarehouseId; } set { purchaseOrder.WarehouseId = value; } }

        [FwLogicProperty(Id: "8XKphJFdmXUxA", IsReadOnly: true)]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id: "wWkK0CLM6YpTm", IsReadOnly: true)]
        public string WarehouseCode { get; set; }

        [FwLogicProperty(Id: "hk4TKLe87oH1y", IsReadOnly: true)]
        public int? QuantityHolding { get; set; }

        [FwLogicProperty(Id: "NltrEXqqTj8W4", IsReadOnly: true)]
        public int? QuantityToBarCode { get; set; }

        [FwLogicProperty(Id: "z7uT5aQyqM9D")]
        public bool? Rental { get { return purchaseOrder.Rental; } set { purchaseOrder.Rental = value; } }

        [FwLogicProperty(Id: "7antlUuZwtHZ")]
        public bool? Sales { get { return purchaseOrder.Sales; } set { purchaseOrder.Sales = value; } }

        [FwLogicProperty(Id: "WdBpgBgw2dGI")]
        public bool? Parts { get { return purchaseOrder.Parts; } set { purchaseOrder.Parts = value; } }

        [FwLogicProperty(Id: "IwSkzn5q2hfR")]
        public bool? Labor { get { return purchaseOrder.Labor; } set { purchaseOrder.Labor = value; } }

        [FwLogicProperty(Id: "ilnWoyy1JHKo")]
        public bool? Miscellaneous { get { return purchaseOrder.Miscellaneous; } set { purchaseOrder.Miscellaneous = value; } }

        [FwLogicProperty(Id: "PUjCaGyz57x6")]
        public bool? Vehicle { get { return purchaseOrder.Vehicle; } set { purchaseOrder.Vehicle = value; } }

        [FwLogicProperty(Id: "vxXkqkwR6xfP")]
        public bool? SubRent { get { return purchaseOrder.SubRent; } set { purchaseOrder.SubRent = value; } }

        [FwLogicProperty(Id: "hTjmsPcl5OeJ")]
        public bool? SubSale { get { return purchaseOrder.SubSale; } set { purchaseOrder.SubSale = value; } }

        [FwLogicProperty(Id: "DaqP1h98caC4")]
        public bool? SubLabor { get { return purchaseOrder.SubLabor; } set { purchaseOrder.SubLabor = value; } }

        [FwLogicProperty(Id: "yoR4lgPGziH7")]
        public bool? SubMiscellaneous { get { return purchaseOrder.SubMiscellaneous; } set { purchaseOrder.SubMiscellaneous = value; } }

        [FwLogicProperty(Id: "BHQEuizKJNds")]
        public bool? SubVehicle { get { return purchaseOrder.SubVehicle; } set { purchaseOrder.SubVehicle = value; } }

        [FwLogicProperty(Id: "ZpOB5ktmEwcc")]
        public bool? Repair { get { return purchaseOrder.Repair; } set { purchaseOrder.Repair = value; } }

        [FwLogicProperty(Id: "yHNCT4jIh6nO")]
        public bool? Consignment { get { return purchaseOrder.Consignment; } set { purchaseOrder.Consignment = value; } }

        [FwLogicProperty(Id: "eV8MfEqoXS5C")]
        public string ConsignorAgreementId { get { return purchaseOrder.ConsignorAgreementId; } set { purchaseOrder.ConsignorAgreementId = value; } }

        [FwLogicProperty(Id: "qOWXcLyuUQfsM", IsReadOnly: true)]
        public string ConsignorAgreementNumber { get; set; }

        [FwLogicProperty(Id: "VIVLmYhhFSMVm", IsReadOnly: true)]
        public string OrderId { get; set; }

        [FwLogicProperty(Id: "yq3FBSQUmjuzX", IsReadOnly: true)]
        public string OrderNumber { get; set; }

        [FwLogicProperty(Id: "n7knYwXQ91Mbr", IsReadOnly: true)]
        public string DealNumber { get; set; }

        [FwLogicProperty(Id: "e3NTOWFojYz9E", IsReadOnly: true)]
        public string DealId { get; set; }

        [FwLogicProperty(Id: "n7knYwXQ91Mbr", IsReadOnly: true)]
        public string Deal { get; set; }

        [FwLogicProperty(Id: "RYGMA4WYuYz2")]
        public string RateType { get { return purchaseOrder.RateType; } set { purchaseOrder.RateType = value; } }

        [FwLogicProperty(Id: "TKeJIOFmtxvQa", IsReadOnly: true)]
        public bool? DepartmentLocationRequiresApproval { get; set; }

        [FwLogicProperty(Id: "7Hp7w9aCw0tx4", IsReadOnly: true)]
        public decimal? Total { get; set; }

        [FwLogicProperty(Id: "96fIFyhQ47HF")]
        public string PoTypeId { get { return purchaseOrder.OrderTypeId; } set { purchaseOrder.OrderTypeId = value; } }

        [FwLogicProperty(Id: "c8aP7WsydAV18", IsReadOnly: true)]
        public string PoType { get; set; }

        //[FwLogicProperty(Id:"NLXl0UdgPZ2P")]
        //public string RequiredByDate { get; set; }

        [FwLogicProperty(Id: "x1hGzge6hHZH")]
        public string PoClassificationId { get { return purchaseOrder.PoClassificationId; } set { purchaseOrder.PoClassificationId = value; } }

        [FwLogicProperty(Id: "pSJOEHHADRcU5", IsReadOnly: true)]
        public string PoClassification { get; set; }

        [FwLogicProperty(Id: "fwPSBW0fcJ5TN", IsNotAudited: true)]
        public List<OrderDatesLogic> ActivityDatesAndTimes { get; set; } = new List<OrderDatesLogic>();

        [FwLogicProperty(Id: "HJ8GhcEZYu3a")]
        public string EstimatedStartDate { get { return purchaseOrder.EstimatedStartDate; } set { purchaseOrder.EstimatedStartDate = value; } }

        [FwLogicProperty(Id: "XHjoZKNARXFss")]
        public string EstimatedStartTime { get { return purchaseOrder.EstimatedStartTime; } set { purchaseOrder.EstimatedStartTime = value; } }

        [FwLogicProperty(Id: "5Gfpglv62U6q")]
        public string EstimatedStopDate { get { return purchaseOrder.EstimatedStopDate; } set { purchaseOrder.EstimatedStopDate = value; } }

        [FwLogicProperty(Id: "xpxpm8LY7aRe5")]
        public string EstimatedStopTime { get { return purchaseOrder.EstimatedStopTime; } set { purchaseOrder.EstimatedStopTime = value; } }

        [FwLogicProperty(Id: "Z578OE4eTaty")]
        public string BillingStartDate { get { return purchaseOrder.BillingStartDate; } set { purchaseOrder.BillingStartDate = value; } }

        [FwLogicProperty(Id: "Bzgy8WpYM2zf")]
        public string BillingEndDate { get { return purchaseOrder.BillingEndDate; } set { purchaseOrder.BillingEndDate = value; } }

        [FwLogicProperty(Id: "Ln3Q2qwd4L3r", IsReadOnly: true)]
        public decimal? BillingWeeks { get; set; }

        [FwLogicProperty(Id: "iS6KJYLduxcP", IsReadOnly: true)]
        public decimal? BillingMonths { get; set; }

        [FwLogicProperty(Id: "Qkr2CvGb90kyC", IsReadOnly: true)]
        public decimal? InvoicedAmount { get; set; }

        //[FwLogicProperty(Id:"nxS9XuVwOUk4")]
        //public decimal? WeeklyExtended { get; set; }

        //[FwLogicProperty(Id:"WLMKXVtN42K7a", IsReadOnly:true)]
        //public string PoApprovalStatusId { get; set; }

        //[FwLogicProperty(Id:"WLMKXVtN42K7a", IsReadOnly:true)]
        //public string PoApprovalStatus { get; set; }

        //[FwLogicProperty(Id:"WLMKXVtN42K7a", IsReadOnly:true)]
        //public string PoApprovalStatustype { get; set; }

        [FwLogicProperty(Id: "hY8fUmCOOAn9")]
        public string ProjectManagerId { get { return purchaseOrder.ProjectManagerId; } set { purchaseOrder.ProjectManagerId = value; } }

        [FwLogicProperty(Id: "37LvI2DfgGPrz", IsReadOnly: true)]
        public string ProjectManager { get; set; }

        //[FwLogicProperty(Id: "RBduUGu4mQ9s")]
        //public string OutDeliveryId { get { return purchaseOrder.OutDeliveryId; } set { purchaseOrder.OutDeliveryId = value; } }

        //[FwLogicProperty(Id: "apaJjUkKu4UIk", IsReadOnly: true)]
        //public bool? DropShip { get; set; }

        //[FwLogicProperty(Id: "Y9sD3wDUch7o")]
        //public string InDeliveryId { get { return purchaseOrder.InDeliveryId; } set { purchaseOrder.InDeliveryId = value; } }


        [FwLogicProperty(Id: "VbzfiyaOOxpVz", DisableDirectAssign: true, DisableDirectModify: true)]
        public string ReceiveDeliveryId { get { return purchaseOrder.OutDeliveryId; } set { purchaseOrder.OutDeliveryId = value; receiveDelivery.DeliveryId = value; } }

        [FwLogicProperty(Id: "KBuwJdGyxbnXc")]
        public string ReceiveDeliveryDeliveryType { get { return receiveDelivery.DeliveryType; } set { receiveDelivery.DeliveryType = value; } }

        [FwLogicProperty(Id: "atX65xHYlpl7c")]
        public string ReceiveDeliveryRequiredDate { get { return receiveDelivery.RequiredDate; } set { receiveDelivery.RequiredDate = value; } }

        [FwLogicProperty(Id: "DDzdH8BAzfqZU")]
        public string ReceiveDeliveryRequiredTime { get { return receiveDelivery.RequiredTime; } set { receiveDelivery.RequiredTime = value; } }

        [FwLogicProperty(Id: "KG7cAIBNaJQmw")]
        public string ReceiveDeliveryTargetShipDate { get { return receiveDelivery.TargetShipDate; } set { receiveDelivery.TargetShipDate = value; } }

        [FwLogicProperty(Id: "2qgTYAjHHc4ia")]
        public string ReceiveDeliveryTargetShipTime { get { return receiveDelivery.TargetShipTime; } set { receiveDelivery.TargetShipTime = value; } }

        [FwLogicProperty(Id: "1BSbdr987Fp0B")]
        public string ReceiveDeliveryDirection { get { return receiveDelivery.Direction; } set { receiveDelivery.Direction = value; } }

        [FwLogicProperty(Id: "XFkWoGbAjnwqp")]
        public string ReceiveDeliveryAddressType { get { return receiveDelivery.AddressType; } set { receiveDelivery.AddressType = value; } }

        [FwLogicProperty(Id: "vfAZRRNxnhnmQ")]
        public string ReceiveDeliveryFromLocation { get { return receiveDelivery.FromLocation; } set { receiveDelivery.FromLocation = value; } }

        [FwLogicProperty(Id: "yFreEMRR6H0TF")]
        public string ReceiveDeliveryFromContact { get { return receiveDelivery.FromContact; } set { receiveDelivery.FromContact = value; } }

        [FwLogicProperty(Id: "AWwIY5d10frqc")]
        public string ReceiveDeliveryFromContactPhone { get { return receiveDelivery.FromContactPhone; } set { receiveDelivery.FromContactPhone = value; } }

        [FwLogicProperty(Id: "bKKHr75SDOaJM")]
        public string ReceiveDeliveryFromAlternateContact { get { return receiveDelivery.FromAlternateContact; } set { receiveDelivery.FromAlternateContact = value; } }

        [FwLogicProperty(Id: "pwBg4ipOYl7GY")]
        public string ReceiveDeliveryFromAlternateContactPhone { get { return receiveDelivery.FromAlternateContactPhone; } set { receiveDelivery.FromAlternateContactPhone = value; } }

        [FwLogicProperty(Id: "FjGdS9yqss26O")]
        public string ReceiveDeliveryFromAttention { get { return receiveDelivery.FromAttention; } set { receiveDelivery.FromAttention = value; } }

        [FwLogicProperty(Id: "tNYfEztYi4nzv")]
        public string ReceiveDeliveryFromAddress1 { get { return receiveDelivery.FromAddress1; } set { receiveDelivery.FromAddress1 = value; } }

        [FwLogicProperty(Id: "47ge1qsw7qw6Z")]
        public string ReceiveDeliveryFromAdd2ress { get { return receiveDelivery.FromAdd2ress; } set { receiveDelivery.FromAdd2ress = value; } }

        [FwLogicProperty(Id: "aPq9UMBGkce85")]
        public string ReceiveDeliveryFromCity { get { return receiveDelivery.FromCity; } set { receiveDelivery.FromCity = value; } }

        [FwLogicProperty(Id: "heiqkICHKnm1X")]
        public string ReceiveDeliveryFromState { get { return receiveDelivery.FromState; } set { receiveDelivery.FromState = value; } }

        [FwLogicProperty(Id: "irM9I4a8PVQ7U")]
        public string ReceiveDeliveryFromZipCode { get { return receiveDelivery.FromZipCode; } set { receiveDelivery.FromZipCode = value; } }

        [FwLogicProperty(Id: "QhKx5DT9rvnN0", IsReadOnly: true)]
        public string ReceiveDeliveryFromCountry { get; set; }

        [FwLogicProperty(Id: "Gr8h1yq2NZrf0")]
        public string ReceiveDeliveryFromCountryId { get { return receiveDelivery.FromCountryId; } set { receiveDelivery.FromCountryId = value; } }

        [FwLogicProperty(Id: "3Kwreds37Nteu")]
        public string ReceiveDeliveryFromCrossStreets { get { return receiveDelivery.FromCrossStreets; } set { receiveDelivery.FromCrossStreets = value; } }

        [FwLogicProperty(Id: "JrNBOenz20mgV")]
        public string ReceiveDeliveryToLocation { get { return receiveDelivery.Location; } set { receiveDelivery.Location = value; } }

        [FwLogicProperty(Id: "4B9teVhfJV6QQ")]
        public string ReceiveDeliveryToContact { get { return receiveDelivery.Contact; } set { receiveDelivery.Contact = value; } }

        [FwLogicProperty(Id: "nCZ5rJ3cpwebr")]
        public string ReceiveDeliveryToContactPhone { get { return receiveDelivery.ContactPhone; } set { receiveDelivery.ContactPhone = value; } }

        [FwLogicProperty(Id: "qK1apI9RpEcT2")]
        public string ReceiveDeliveryToAlternateContact { get { return receiveDelivery.AlternateContact; } set { receiveDelivery.AlternateContact = value; } }

        [FwLogicProperty(Id: "YAKAYpa2gwwUE")]
        public string ReceiveDeliveryToAlternateContactPhone { get { return receiveDelivery.AlternateContactPhone; } set { receiveDelivery.AlternateContactPhone = value; } }

        [FwLogicProperty(Id: "u8jW0DZPWP7U8")]
        public string ReceiveDeliveryToAttention { get { return receiveDelivery.Attention; } set { receiveDelivery.Attention = value; } }

        [FwLogicProperty(Id: "JRfmyDhX9OP2v")]
        public string ReceiveDeliveryToAddress1 { get { return receiveDelivery.Address1; } set { receiveDelivery.Address1 = value; } }

        [FwLogicProperty(Id: "giQZkNNHssed5")]
        public string ReceiveDeliveryToAddress2 { get { return receiveDelivery.Address2; } set { receiveDelivery.Address2 = value; } }

        [FwLogicProperty(Id: "kEUB4FtxIIzHI")]
        public string ReceiveDeliveryToCity { get { return receiveDelivery.City; } set { receiveDelivery.City = value; } }

        [FwLogicProperty(Id: "2ecxHlZpjwsTq")]
        public string ReceiveDeliveryToState { get { return receiveDelivery.State; } set { receiveDelivery.State = value; } }

        [FwLogicProperty(Id: "iqWPgqZ1Gem05")]
        public string ReceiveDeliveryToZipCode { get { return receiveDelivery.ZipCode; } set { receiveDelivery.ZipCode = value; } }

        [FwLogicProperty(Id: "9OVkZt4B9NEb2")]
        public string ReceiveDeliveryToCountryId { get { return receiveDelivery.CountryId; } set { receiveDelivery.CountryId = value; } }

        [FwLogicProperty(Id: "DmhXWw6R55ao6", IsReadOnly: true)]
        public string ReceiveDeliveryToCountry { get; set; }

        [FwLogicProperty(Id: "373fH74e44uDG")]
        public string ReceiveDeliveryToContactFax { get { return receiveDelivery.ContactFax; } set { receiveDelivery.ContactFax = value; } }

        [FwLogicProperty(Id: "lacnVzgJnrsI8")]
        public string ReceiveDeliveryToCrossStreets { get { return receiveDelivery.CrossStreets; } set { receiveDelivery.CrossStreets = value; } }

        [FwLogicProperty(Id: "N5X7WeYEgjSko")]
        public string ReceiveDeliveryDeliveryNotes { get { return receiveDelivery.DeliveryNotes; } set { receiveDelivery.DeliveryNotes = value; } }

        [FwLogicProperty(Id: "6CtMjzlLosFZk")]
        public string ReceiveDeliveryCarrierId { get { return receiveDelivery.CarrierId; } set { receiveDelivery.CarrierId = value; } }

        [FwLogicProperty(Id: "2DmQ0SV7GesDr", IsReadOnly: true)]
        public string ReceiveDeliveryCarrier { get; set; }

        [FwLogicProperty(Id: "8v23t8hnBzZml")]
        public string ReceiveDeliveryCarrierAccount { get { return receiveDelivery.CarrierAccount; } set { receiveDelivery.CarrierAccount = value; } }

        [FwLogicProperty(Id: "QLextWePKEp6i")]
        public string ReceiveDeliveryShipViaId { get { return receiveDelivery.ShipViaId; } set { receiveDelivery.ShipViaId = value; } }

        [FwLogicProperty(Id: "xRCrL5qNdeA2R", IsReadOnly: true)]
        public string ReceiveDeliveryShipVia { get; set; }

        [FwLogicProperty(Id: "XlpcobgGgzNQQ")]
        public string ReceiveDeliveryInvoiceId { get { return receiveDelivery.InvoiceId; } set { receiveDelivery.InvoiceId = value; } }

        [FwLogicProperty(Id: "jtXD9RSIaAYxy")]
        public string ReceiveDeliveryVendorInvoiceId { get { return receiveDelivery.VendorInvoiceId; } set { receiveDelivery.VendorInvoiceId = value; } }

        [FwLogicProperty(Id: "RZhq8E7Z3tDlq")]
        public decimal? ReceiveDeliveryEstimatedFreight { get { return receiveDelivery.EstimatedFreight; } set { receiveDelivery.EstimatedFreight = value; } }

        [FwLogicProperty(Id: "7cSJsNb3D0Jlr")]
        public decimal? ReceiveDeliveryFreightInvoiceAmount { get { return receiveDelivery.FreightInvoiceAmount; } set { receiveDelivery.FreightInvoiceAmount = value; } }

        [FwLogicProperty(Id: "A890J05VGAOEb")]
        public string ReceiveDeliveryChargeType { get { return receiveDelivery.ChargeType; } set { receiveDelivery.ChargeType = value; } }

        [FwLogicProperty(Id: "BfmE8ADxjYQBh")]
        public string ReceiveDeliveryFreightTrackingNumber { get { return receiveDelivery.FreightTrackingNumber; } set { receiveDelivery.FreightTrackingNumber = value; } }

        [FwLogicProperty(Id: "mCEZJYR1C6QeS", IsReadOnly: true)]
        public string ReceiveDeliveryFreightTrackingUrl { get; set; }

        [FwLogicProperty(Id: "nx9s1akP7WLO9")]
        public bool? ReceiveDeliveryDropShip { get { return receiveDelivery.DropShip; } set { receiveDelivery.DropShip = value; } }

        [FwLogicProperty(Id: "MnD8L4iIQhaAs")]
        public string ReceiveDeliveryPackageCode { get { return receiveDelivery.PackageCode; } set { receiveDelivery.PackageCode = value; } }

        [FwLogicProperty(Id: "L1nrYLiK6bMki")]
        public bool? ReceiveDeliveryBillPoFreightOnOrder { get { return receiveDelivery.BillPoFreightOnOrder; } set { receiveDelivery.BillPoFreightOnOrder = value; } }

        [FwLogicProperty(Id: "SCLJcw3l4lhUp")]
        public string ReceiveDeliveryOnlineOrderNumber { get { return receiveDelivery.OnlineOrderNumber; } set { receiveDelivery.OnlineOrderNumber = value; } }

        [FwLogicProperty(Id: "MNXlQ7XoB1ptF")]
        public string ReceiveDeliveryOnlineOrderStatus { get { return receiveDelivery.OnlineOrderStatus; } set { receiveDelivery.OnlineOrderStatus = value; } }

        [FwLogicProperty(Id: "eeovdgmAZ2080")]
        public string ReceiveDeliveryDateStamp { get { return receiveDelivery.DateStamp; } set { receiveDelivery.DateStamp = value; } }










        [FwLogicProperty(Id: "Z1dGQSClQXZ3A", DisableDirectAssign: true, DisableDirectModify: true)]
        public string ReturnDeliveryId { get { return purchaseOrder.InDeliveryId; } set { purchaseOrder.InDeliveryId = value; returnDelivery.DeliveryId = value; } }

        [FwLogicProperty(Id: "1EiJO5WYBJHKc")]
        public string ReturnDeliveryDeliveryType { get { return returnDelivery.DeliveryType; } set { returnDelivery.DeliveryType = value; } }

        [FwLogicProperty(Id: "KXSDw9RdlYDJD")]
        public string ReturnDeliveryRequiredDate { get { return returnDelivery.RequiredDate; } set { returnDelivery.RequiredDate = value; } }

        [FwLogicProperty(Id: "4PiTpvj0ZHLvk")]
        public string ReturnDeliveryRequiredTime { get { return returnDelivery.RequiredTime; } set { returnDelivery.RequiredTime = value; } }

        [FwLogicProperty(Id: "Ve9yRBXEtn8Sg")]
        public string ReturnDeliveryTargetShipDate { get { return returnDelivery.TargetShipDate; } set { returnDelivery.TargetShipDate = value; } }

        [FwLogicProperty(Id: "JD0AOezpyKz92")]
        public string ReturnDeliveryTargetShipTime { get { return returnDelivery.TargetShipTime; } set { returnDelivery.TargetShipTime = value; } }

        [FwLogicProperty(Id: "FWEe1evT89EfZ")]
        public string ReturnDeliveryDirection { get { return returnDelivery.Direction; } set { returnDelivery.Direction = value; } }

        [FwLogicProperty(Id: "VlVqD2Yt0SpWh")]
        public string ReturnDeliveryAddressType { get { return returnDelivery.AddressType; } set { returnDelivery.AddressType = value; } }

        [FwLogicProperty(Id: "dgIuNGBgFnhrA")]
        public string ReturnDeliveryFromLocation { get { return returnDelivery.FromLocation; } set { returnDelivery.FromLocation = value; } }

        [FwLogicProperty(Id: "ztpmhpbgWZvK3")]
        public string ReturnDeliveryFromContact { get { return returnDelivery.FromContact; } set { returnDelivery.FromContact = value; } }

        [FwLogicProperty(Id: "vf32SrblWCYzs")]
        public string ReturnDeliveryFromContactPhone { get { return returnDelivery.FromContactPhone; } set { returnDelivery.FromContactPhone = value; } }

        [FwLogicProperty(Id: "P1NpRsLqsONhb")]
        public string ReturnDeliveryFromAlternateContact { get { return returnDelivery.FromAlternateContact; } set { returnDelivery.FromAlternateContact = value; } }

        [FwLogicProperty(Id: "AFngcs1VAa7rb")]
        public string ReturnDeliveryFromAlternateContactPhone { get { return returnDelivery.FromAlternateContactPhone; } set { returnDelivery.FromAlternateContactPhone = value; } }

        [FwLogicProperty(Id: "qkElmmsitLQW5")]
        public string ReturnDeliveryFromAttention { get { return returnDelivery.FromAttention; } set { returnDelivery.FromAttention = value; } }

        [FwLogicProperty(Id: "K43AdTBdiCeOx")]
        public string ReturnDeliveryFromAddress1 { get { return returnDelivery.FromAddress1; } set { returnDelivery.FromAddress1 = value; } }

        [FwLogicProperty(Id: "nB5161R0gQEQh")]
        public string ReturnDeliveryFromAdd2ress { get { return returnDelivery.FromAdd2ress; } set { returnDelivery.FromAdd2ress = value; } }

        [FwLogicProperty(Id: "uFRfalXVuTdGN")]
        public string ReturnDeliveryFromCity { get { return returnDelivery.FromCity; } set { returnDelivery.FromCity = value; } }

        [FwLogicProperty(Id: "wjHw1O4T5QCFM")]
        public string ReturnDeliveryFromState { get { return returnDelivery.FromState; } set { returnDelivery.FromState = value; } }

        [FwLogicProperty(Id: "JhdC1cihQ9vSr")]
        public string ReturnDeliveryFromZipCode { get { return returnDelivery.FromZipCode; } set { returnDelivery.FromZipCode = value; } }

        [FwLogicProperty(Id: "lcO2Ecatxw9h2", IsReadOnly: true)]
        public string ReturnDeliveryFromCountry { get; set; }

        [FwLogicProperty(Id: "NUZkhn1cINilN")]
        public string ReturnDeliveryFromCountryId { get { return returnDelivery.FromCountryId; } set { returnDelivery.FromCountryId = value; } }

        [FwLogicProperty(Id: "BdHtcNh0prtQ7")]
        public string ReturnDeliveryFromCrossStreets { get { return returnDelivery.FromCrossStreets; } set { returnDelivery.FromCrossStreets = value; } }

        [FwLogicProperty(Id: "ABo61n9d3riek")]
        public string ReturnDeliveryToLocation { get { return returnDelivery.Location; } set { returnDelivery.Location = value; } }

        [FwLogicProperty(Id: "ycjMc0bNwxX4W")]
        public string ReturnDeliveryToContact { get { return returnDelivery.Contact; } set { returnDelivery.Contact = value; } }

        [FwLogicProperty(Id: "sBIVcAgl0idSF")]
        public string ReturnDeliveryToContactPhone { get { return returnDelivery.ContactPhone; } set { returnDelivery.ContactPhone = value; } }

        [FwLogicProperty(Id: "wyDvxjtfZcfeo")]
        public string ReturnDeliveryToAlternateContact { get { return returnDelivery.AlternateContact; } set { returnDelivery.AlternateContact = value; } }

        [FwLogicProperty(Id: "XWzTek1UhTYdB")]
        public string ReturnDeliveryToAlternateContactPhone { get { return returnDelivery.AlternateContactPhone; } set { returnDelivery.AlternateContactPhone = value; } }

        [FwLogicProperty(Id: "tiMYs2AFzJJrp")]
        public string ReturnDeliveryToAttention { get { return returnDelivery.Attention; } set { returnDelivery.Attention = value; } }

        [FwLogicProperty(Id: "Qp73E13QXoh8h")]
        public string ReturnDeliveryToAddress1 { get { return returnDelivery.Address1; } set { returnDelivery.Address1 = value; } }

        [FwLogicProperty(Id: "Zx79ZUdbhlpkn")]
        public string ReturnDeliveryToAddress2 { get { return returnDelivery.Address2; } set { returnDelivery.Address2 = value; } }

        [FwLogicProperty(Id: "aetwOyjJv79Mx")]
        public string ReturnDeliveryToCity { get { return returnDelivery.City; } set { returnDelivery.City = value; } }

        [FwLogicProperty(Id: "Q77Jsdazree0Y")]
        public string ReturnDeliveryToState { get { return returnDelivery.State; } set { returnDelivery.State = value; } }

        [FwLogicProperty(Id: "Oxgb5hqp3T5Ry")]
        public string ReturnDeliveryToZipCode { get { return returnDelivery.ZipCode; } set { returnDelivery.ZipCode = value; } }

        [FwLogicProperty(Id: "yqMTEZWAXKu7e")]
        public string ReturnDeliveryToCountryId { get { return returnDelivery.CountryId; } set { returnDelivery.CountryId = value; } }

        [FwLogicProperty(Id: "yEyEmZvSDQafX", IsReadOnly: true)]
        public string ReturnDeliveryToCountry { get; set; }

        [FwLogicProperty(Id: "JBMr8Ic5cpPPs")]
        public string ReturnDeliveryToContactFax { get { return returnDelivery.ContactFax; } set { returnDelivery.ContactFax = value; } }

        [FwLogicProperty(Id: "1aaxoOLAxMxIC")]
        public string ReturnDeliveryToCrossStreets { get { return returnDelivery.CrossStreets; } set { returnDelivery.CrossStreets = value; } }

        [FwLogicProperty(Id: "9RUEofSWa3XuD")]
        public string ReturnDeliveryDeliveryNotes { get { return returnDelivery.DeliveryNotes; } set { returnDelivery.DeliveryNotes = value; } }

        [FwLogicProperty(Id: "RubhJNNvpwrSU")]
        public string ReturnDeliveryCarrierId { get { return returnDelivery.CarrierId; } set { returnDelivery.CarrierId = value; } }

        [FwLogicProperty(Id: "IHuIIaxHqgW4B", IsReadOnly: true)]
        public string ReturnDeliveryCarrier { get; set; }

        [FwLogicProperty(Id: "4r3aJWCiCDwGO")]
        public string ReturnDeliveryCarrierAccount { get { return returnDelivery.CarrierAccount; } set { returnDelivery.CarrierAccount = value; } }

        [FwLogicProperty(Id: "MedYJ6nl9XU22")]
        public string ReturnDeliveryShipViaId { get { return returnDelivery.ShipViaId; } set { returnDelivery.ShipViaId = value; } }

        [FwLogicProperty(Id: "jAaI7tAbEjKtM", IsReadOnly: true)]
        public string ReturnDeliveryShipVia { get; set; }

        [FwLogicProperty(Id: "er92XmfiaNDw8")]
        public string ReturnDeliveryInvoiceId { get { return returnDelivery.InvoiceId; } set { returnDelivery.InvoiceId = value; } }

        [FwLogicProperty(Id: "0Y7GQSaLJ82i3")]
        public string ReturnDeliveryVendorInvoiceId { get { return returnDelivery.VendorInvoiceId; } set { returnDelivery.VendorInvoiceId = value; } }

        [FwLogicProperty(Id: "qLQGnmBQPFPiR")]
        public decimal? ReturnDeliveryEstimatedFreight { get { return returnDelivery.EstimatedFreight; } set { returnDelivery.EstimatedFreight = value; } }

        [FwLogicProperty(Id: "zOTA7JNAsjqvJ")]
        public decimal? ReturnDeliveryFreightInvoiceAmount { get { return returnDelivery.FreightInvoiceAmount; } set { returnDelivery.FreightInvoiceAmount = value; } }

        [FwLogicProperty(Id: "uEMMSdjH2ewSB")]
        public string ReturnDeliveryChargeType { get { return returnDelivery.ChargeType; } set { returnDelivery.ChargeType = value; } }

        [FwLogicProperty(Id: "tMXK7XFgMcmoY")]
        public string ReturnDeliveryFreightTrackingNumber { get { return returnDelivery.FreightTrackingNumber; } set { returnDelivery.FreightTrackingNumber = value; } }

        [FwLogicProperty(Id: "stWEHA0ipF1Dp", IsReadOnly: true)]
        public string ReturnDeliveryFreightTrackingUrl { get; set; }

        [FwLogicProperty(Id: "CedfxyziQuCOU")]
        public bool? ReturnDeliveryDropShip { get { return returnDelivery.DropShip; } set { returnDelivery.DropShip = value; } }

        [FwLogicProperty(Id: "jyxgobQ9OMPuh")]
        public string ReturnDeliveryPackageCode { get { return returnDelivery.PackageCode; } set { returnDelivery.PackageCode = value; } }

        [FwLogicProperty(Id: "M3QqR1NDphrKi")]
        public bool? ReturnDeliveryBillPoFreightOnOrder { get { return returnDelivery.BillPoFreightOnOrder; } set { returnDelivery.BillPoFreightOnOrder = value; } }

        [FwLogicProperty(Id: "BLjosdbeQi2BP")]
        public string ReturnDeliveryOnlineOrderNumber { get { return returnDelivery.OnlineOrderNumber; } set { returnDelivery.OnlineOrderNumber = value; } }

        [FwLogicProperty(Id: "WG1awnsh71Kmw")]
        public string ReturnDeliveryOnlineOrderStatus { get { return returnDelivery.OnlineOrderStatus; } set { returnDelivery.OnlineOrderStatus = value; } }

        [FwLogicProperty(Id: "g1K0q6Vn89Oj9")]
        public string ReturnDeliveryDateStamp { get { return returnDelivery.DateStamp; } set { returnDelivery.DateStamp = value; } }

        [FwLogicProperty(Id: "D1kNjKr1ss4M", IsReadOnly: true)]
        public bool? EnableProjects { get; set; }

        [FwLogicProperty(Id: "TJnlyuYOArN3")]
        public string ProjectId { get { return purchaseOrder.ProjectId; } set { purchaseOrder.ProjectId = value; } }

        [FwLogicProperty(Id: "JcMVlViabrzP4", IsReadOnly: true)]
        public string ProjectNumber { get; set; }

        [FwLogicProperty(Id: "fDewVmifAdGcP", IsReadOnly: true)]
        public string Project { get; set; }

        [FwLogicProperty(Id: "qifE9Fpol3kbD")]
        public string ProjectDrawingsId { get { return purchaseOrderDetail.ProjectDrawingsId; } set { purchaseOrderDetail.ProjectDrawingsId = value; } }

        [FwLogicProperty(Id: "lZ7XAv6I7SM2U", IsReadOnly: true)]
        public string ProjectDrawings { get; set; }

        [FwLogicProperty(Id: "qWbnL8tuLZKR3")]
        public string ProjectItemsOrderedId { get { return purchaseOrderDetail.ProjectItemsOrderedId; } set { purchaseOrderDetail.ProjectItemsOrderedId = value; } }

        [FwLogicProperty(Id: "OBKur8yDK9bUa", IsReadOnly: true)]
        public string ProjectItemsOrdered { get; set; }

        [FwLogicProperty(Id: "CplodohkNG3uC")]
        public string ProjectDropShipId { get { return purchaseOrderDetail.ProjectDropShipId; } set { purchaseOrderDetail.ProjectDropShipId = value; } }

        [FwLogicProperty(Id: "b4YHj5yb5ok1d", IsReadOnly: true)]
        public string ProjectDropShip { get; set; }

        [FwLogicProperty(Id: "BcsNgFIq8NBwT")]
        public string ProjectAsBuildId { get { return purchaseOrderDetail.ProjectAsBuildId; } set { purchaseOrderDetail.ProjectAsBuildId = value; } }

        [FwLogicProperty(Id: "sX2V4Ny8OmWLQ", IsReadOnly: true)]
        public string ProjectAsBuild { get; set; }

        [FwLogicProperty(Id: "X1hxMuiJPOUQA")]
        public string ProjectCommissioningId { get { return purchaseOrderDetail.ProjectCommissioningId; } set { purchaseOrderDetail.ProjectCommissioningId = value; } }

        [FwLogicProperty(Id: "O09JQOtkGmhv4", IsReadOnly: true)]
        public string ProjectCommissioning { get; set; }

        [FwLogicProperty(Id: "tjkMiXRRRIZ3T")]
        public string ProjectDepositId { get { return purchaseOrderDetail.ProjectDepositId; } set { purchaseOrderDetail.ProjectDepositId = value; } }

        [FwLogicProperty(Id: "6dyaSrL0NSBws", IsReadOnly: true)]
        public string ProjectDeposit { get; set; }

        [FwLogicProperty(Id: "WYP8xqoGbI6M")]
        public string Location { get { return purchaseOrder.Location; } set { purchaseOrder.Location = value; } }

        [FwLogicProperty(Id: "e2zOESzvmQPY")]
        public string CurrencyId { get { return purchaseOrder.CurrencyId; } set { purchaseOrder.CurrencyId = value; } }

        [FwLogicProperty(Id: "6rVDoG5iNriMw")]
        public bool? UpdateAllRatesToNewCurrency { get; set; }

        [FwLogicProperty(Id: "KdH86jJDvTKUd", IsNotAudited: true)]
        public string ConfirmUpdateAllRatesToNewCurrency { get; set; }

        [FwLogicProperty(Id: "T6x3UjlxoKwxj", IsReadOnly: true)]
        public string CurrencyCode { get; set; }

        [FwLogicProperty(Id: "qUfpc2ERE8Kxy", IsReadOnly: true)]
        public string Currency { get; set; }

        [FwLogicProperty(Id: "zRw3qmweehM2Z", IsReadOnly: true)]
        public string CurrencySymbol { get; set; }

        [FwLogicProperty(Id: "uApGEj8iolr8")]
        public string BillingCycleId { get { return purchaseOrder.BillingCycleId; } set { purchaseOrder.BillingCycleId = value; } }

        [FwLogicProperty(Id: "QRNhl3XsQMUmi", IsReadOnly: true)]
        public string BillingCycle { get; set; }

        [FwLogicProperty(Id: "wW6cre9T4eosh")]
        public string RemitToAttention1 { get { return purchaseOrder.IssuedToAttention; } set { purchaseOrder.IssuedToAttention = value; } }

        [FwLogicProperty(Id: "Rg0zsblCgefCb")]
        public string RemitToAttention2 { get { return purchaseOrder.IssuedToAttention2; } set { purchaseOrder.IssuedToAttention2 = value; } }

        [FwLogicProperty(Id: "PA2Facncfq9MJ")]
        public string RemitToAddress1 { get { return purchaseOrder.IssuedToAddress1; } set { purchaseOrder.IssuedToAddress1 = value; } }

        [FwLogicProperty(Id: "VIU7lNauU5UXz")]
        public string RemitToAddress2 { get { return purchaseOrder.IssuedToAddress2; } set { purchaseOrder.IssuedToAddress2 = value; } }

        [FwLogicProperty(Id: "VA772stcN1yL5")]
        public string RemitToCity { get { return purchaseOrder.IssuedToCity; } set { purchaseOrder.IssuedToCity = value; } }

        [FwLogicProperty(Id: "6U3S98f738p5A")]
        public string RemitToState { get { return purchaseOrder.IssuedToState; } set { purchaseOrder.IssuedToState = value; } }

        [FwLogicProperty(Id: "0W3PTnyhvCaum")]
        public string RemitToCountryId { get { return purchaseOrder.IssuedToCountryId; } set { purchaseOrder.IssuedToCountryId = value; } }

        [FwLogicProperty(Id: "ndc6mguzChPmz", IsReadOnly: true)]
        public string RemitToCountry { get; set; }

        [FwLogicProperty(Id: "fzVEyYTUcyrvb")]
        public string RemitToZipCode { get { return purchaseOrder.IssuedToZipCode; } set { purchaseOrder.IssuedToZipCode = value; } }

        [FwLogicProperty(Id: "31KVsaJic391U")]
        public string RemitToEmail { get { return purchaseOrder.IssuedToEmail; } set { purchaseOrder.IssuedToEmail = value; } }

        [FwLogicProperty(Id: "hAhc3b9qpN75d")]
        public string RemitToPhone { get { return purchaseOrder.IssuedToPhone; } set { purchaseOrder.IssuedToPhone = value; } }

        [FwLogicProperty(Id: "t86a4sDnXfJX7")]
        public string PaymentTypeId { get { return purchaseOrder.PaymentTypeId; } set { purchaseOrder.PaymentTypeId = value; } }

        [FwLogicProperty(Id: "enPU3AuYFjj8d", IsReadOnly: true)]
        public string PaymentType { get; set; }

        [FwLogicProperty(Id: "XfP2lmt73Pt")]
        public string PaymentTermsId { get { return purchaseOrder.PaymentTermsId; } set { purchaseOrder.PaymentTermsId = value; } }

        [FwLogicProperty(Id: "oivJq1BqYTc", IsReadOnly: true)]
        public string PaymentTerms { get; set; }

        [FwLogicProperty(Id: "r0nQNfNwLAu", IsReadOnly: true)]
        public int? PaymentTermsDueInDays { get; set; }

        [FwLogicProperty(Id: "D60yNs4DNU1R")]
        public string TaxOptionId { get { return tax.TaxOptionId; } set { tax.TaxOptionId = value; } }

        [FwLogicProperty(Id: "oafJa8FREKfAu", IsReadOnly: true)]
        public string TaxOption { get; set; }

        [FwLogicProperty(Id: "gNNYzTTDz8v8", DisableDirectModify: true)]
        public string TaxId { get { return purchaseOrder.TaxId; } set { purchaseOrder.TaxId = value; } }

        [FwLogicProperty(Id: "gLEVVNQ8Daeu", IsReadOnly: true)]
        public string Tax1Name { get; set; }

        [FwLogicProperty(Id: "IeKfALQLLweJ", IsReadOnly: true)]
        public string Tax2Name { get; set; }

        [FwLogicProperty(Id: "krjw2wDrUP6R")]
        public decimal? RentalTaxRate1 { get { return tax.RentalTaxRate1; } set { tax.RentalTaxRate1 = value; } }

        [FwLogicProperty(Id: "kAEEeWXhtxzg")]
        public decimal? SalesTaxRate1 { get { return tax.SalesTaxRate1; } set { tax.SalesTaxRate1 = value; } }

        [FwLogicProperty(Id: "nyqIGqlupZes")]
        public decimal? LaborTaxRate1 { get { return tax.LaborTaxRate1; } set { tax.LaborTaxRate1 = value; } }

        [FwLogicProperty(Id: "NmUaCdYkAKC5")]
        public decimal? RentalTaxRate2 { get { return tax.RentalTaxRate2; } set { tax.RentalTaxRate2 = value; } }

        [FwLogicProperty(Id: "Aa6GJdEpQTxq")]
        public decimal? SalesTaxRate2 { get { return tax.SalesTaxRate2; } set { tax.SalesTaxRate2 = value; } }

        [FwLogicProperty(Id: "vpxC9SS88U27")]
        public decimal? LaborTaxRate2 { get { return tax.LaborTaxRate2; } set { tax.LaborTaxRate2 = value; } }

        [FwLogicProperty(Id: "HFIgsyl9ZgM2r", IsReadOnly: true)]
        public bool? HasRentalItem { get; set; }

        [FwLogicProperty(Id: "0TNYdnneENoGn", IsReadOnly: true)]
        public bool? HasSalesItem { get; set; }

        [FwLogicProperty(Id: "HGMYcHXNfJkjw", IsReadOnly: true)]
        public bool? HasMiscellaneousItem { get; set; }

        [FwLogicProperty(Id: "tBLuzGVzghpES", IsReadOnly: true)]
        public bool? HasLaborItem { get; set; }

        [FwLogicProperty(Id: "UeBCOJHXtP3oz", IsReadOnly: true)]
        public bool? HasFacilitiesItem { get; set; }

        [FwLogicProperty(Id: "zBYEvIYJxoWr0", IsReadOnly: true)]
        public bool? HasLossAndDamageItem { get; set; }

        [FwLogicProperty(Id: "DpKgFis6AxoL7", IsReadOnly: true)]
        public bool? HasRentalSaleItem { get; set; }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "t4giqsPWsD65K", IsReadOnly: true)]
        public decimal? RentalDiscountPercent { get; set; }

        [FwLogicProperty(Id: "t4giqsPWsD65K", IsReadOnly: true)]
        public decimal? RentalTotal { get; set; }

        [FwLogicProperty(Id: "t4giqsPWsD65K", IsReadOnly: true)]
        public bool? RentalTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "BvhjiMz2RirLb", IsReadOnly: true)]
        public decimal? SalesDiscountPercent { get; set; }

        [FwLogicProperty(Id: "BvhjiMz2RirLb", IsReadOnly: true)]
        public decimal? SalesTotal { get; set; }

        [FwLogicProperty(Id: "BvhjiMz2RirLb", IsReadOnly: true)]
        public bool? SalesTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "DHbrVrf7qwF8d", IsReadOnly: true)]
        public decimal? PartsDiscountPercent { get; set; }

        [FwLogicProperty(Id: "DHbrVrf7qwF8d", IsReadOnly: true)]
        public decimal? PartsTotal { get; set; }

        [FwLogicProperty(Id: "DHbrVrf7qwF8d", IsReadOnly: true)]
        public bool? PartsTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "WRIFQEwFJ1TeJ", IsReadOnly: true)]
        public decimal? VehicleDiscountPercent { get; set; }

        [FwLogicProperty(Id: "7Hp7w9aCw0tx4", IsReadOnly: true)]
        public decimal? VehicleTotal { get; set; }

        [FwLogicProperty(Id: "7Hp7w9aCw0tx4", IsReadOnly: true)]
        public bool? VehicleTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "3LltCBVzUG7a")]
        public decimal? MiscDiscountPercent { get; set; }

        [FwLogicProperty(Id: "7Hp7w9aCw0tx4", IsReadOnly: true)]
        public decimal? MiscTotal { get; set; }

        [FwLogicProperty(Id: "7Hp7w9aCw0tx4", IsReadOnly: true)]
        public bool? MiscTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "DN7hr95RFHCXk", IsReadOnly: true)]
        public decimal? LaborDiscountPercent { get; set; }

        [FwLogicProperty(Id: "DN7hr95RFHCXk", IsReadOnly: true)]
        public decimal? LaborTotal { get; set; }

        [FwLogicProperty(Id: "DN7hr95RFHCXk", IsReadOnly: true)]
        public bool? LaborTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "t4giqsPWsD65K", IsReadOnly: true)]
        public decimal? SubRentalDaysPerWeek { get; set; }

        [FwLogicProperty(Id: "t4giqsPWsD65K", IsReadOnly: true)]
        public decimal? SubRentalDiscountPercent { get; set; }

        [FwLogicProperty(Id: "t4giqsPWsD65K", IsReadOnly: true)]
        public decimal? WeeklySubRentalTotal { get; set; }

        [FwLogicProperty(Id: "t4giqsPWsD65K", IsReadOnly: true)]
        public decimal? MonthlySubRentalTotal { get; set; }

        [FwLogicProperty(Id: "t4giqsPWsD65K", IsReadOnly: true)]
        public decimal? PeriodSubRentalTotal { get; set; }

        [FwLogicProperty(Id: "t4giqsPWsD65K", IsReadOnly: true)]
        public bool? WeeklySubRentalTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "t4giqsPWsD65K", IsReadOnly: true)]
        public bool? MonthlySubRentalTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "t4giqsPWsD65K", IsReadOnly: true)]
        public bool? PeriodSubRentalTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "BvhjiMz2RirLb", IsReadOnly: true)]
        public decimal? SubSalesDiscountPercent { get; set; }

        [FwLogicProperty(Id: "BvhjiMz2RirLb", IsReadOnly: true)]
        public decimal? SubSalesTotal { get; set; }

        [FwLogicProperty(Id: "BvhjiMz2RirLb", IsReadOnly: true)]
        public bool? SubSalesTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "Bb2UKU0DH2xKL", IsReadOnly: true)]
        public decimal? SubVehicleDaysPerWeek { get; set; }

        [FwLogicProperty(Id: "VxpP62n9VOim")]
        public decimal? SubvehicleDiscountPercent { get; set; }

        [FwLogicProperty(Id: "Bb2UKU0DH2xKL", IsReadOnly: true)]
        public decimal? WeeklySubVehicleTotal { get; set; }

        [FwLogicProperty(Id: "Bb2UKU0DH2xKL", IsReadOnly: true)]
        public decimal? MonthlySubVehicleTotal { get; set; }

        [FwLogicProperty(Id: "Bb2UKU0DH2xKL", IsReadOnly: true)]
        public decimal? PeriodSubVehicleTotal { get; set; }

        [FwLogicProperty(Id: "Bb2UKU0DH2xKL", IsReadOnly: true)]
        public bool? WeeklySubVehicleTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "Bb2UKU0DH2xKL", IsReadOnly: true)]
        public bool? MonthlySubVehicleTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "Bb2UKU0DH2xKL", IsReadOnly: true)]
        public bool? PeriodSubVehicleTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "2XE5e6UFQ8Xu")]
        public decimal? SubMiscDiscountPercent { get; set; }

        [FwLogicProperty(Id: "7Hp7w9aCw0tx4", IsReadOnly: true)]
        public decimal? WeeklySubMiscTotal { get; set; }

        [FwLogicProperty(Id: "7Hp7w9aCw0tx4", IsReadOnly: true)]
        public decimal? MonthlySubMiscTotal { get; set; }

        [FwLogicProperty(Id: "7Hp7w9aCw0tx4", IsReadOnly: true)]
        public decimal? PeriodSubMiscTotal { get; set; }

        [FwLogicProperty(Id: "7Hp7w9aCw0tx4", IsReadOnly: true)]
        public bool? WeeklySubMiscTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "7Hp7w9aCw0tx4", IsReadOnly: true)]
        public bool? MonthlySubMiscTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "7Hp7w9aCw0tx4", IsReadOnly: true)]
        public bool? PeriodSubMiscTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "DN7hr95RFHCXk", IsReadOnly: true)]
        public decimal? SubLaborDiscountPercent { get; set; }

        [FwLogicProperty(Id: "DN7hr95RFHCXk", IsReadOnly: true)]
        public decimal? WeeklySubLaborTotal { get; set; }

        [FwLogicProperty(Id: "DN7hr95RFHCXk", IsReadOnly: true)]
        public decimal? MonthlySubLaborTotal { get; set; }

        [FwLogicProperty(Id: "DN7hr95RFHCXk", IsReadOnly: true)]
        public decimal? PeriodSubLaborTotal { get; set; }

        [FwLogicProperty(Id: "DN7hr95RFHCXk", IsReadOnly: true)]
        public bool? WeeklySubLaborTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "DN7hr95RFHCXk", IsReadOnly: true)]
        public bool? MonthlySubLaborTotalIncludesTax { get; set; }

        [FwLogicProperty(Id: "DN7hr95RFHCXk", IsReadOnly: true)]
        public bool? PeriodSubLaborTotalIncludesTax { get; set; }


        [FwLogicProperty(Id: "5fV4aZWQ5inMt")]
        public bool? MiscellaneousIsComplete { get { return purchaseOrderDetail.MiscellaneousIsComplete; } set { purchaseOrderDetail.MiscellaneousIsComplete = value; } }
        [FwLogicProperty(Id: "YqKPCQjxzvWsQ")]
        public bool? SubMiscellaneousIsComplete { get { return purchaseOrderDetail.SubMiscellaneousIsComplete; } set { purchaseOrderDetail.SubMiscellaneousIsComplete = value; } }
        [FwLogicProperty(Id: "ymgrPlOuhOdEp")]
        public bool? LaborIsComplete { get { return purchaseOrderDetail.LaborIsComplete; } set { purchaseOrderDetail.LaborIsComplete = value; } }
        [FwLogicProperty(Id: "4vB85neZs8r99")]
        public bool? SubLaborIsComplete { get { return purchaseOrderDetail.SubLaborIsComplete; } set { purchaseOrderDetail.SubLaborIsComplete = value; } }
        //------------------------------------------------------------------------------------


        [FwLogicProperty(Id: "EJHdP3FGL75fJ", IsReadOnly: true)]
        public string CurrencyColor { get; set; }
        [FwLogicProperty(Id: "mbfCIKAk0BI79", IsReadOnly: true)]
        public string StatusColor { get; set; }
        [FwLogicProperty(Id: "WHeeHTlDkTL7P", IsReadOnly: true)]
        public string PurchaseOrderNumberColor { get; set; }
        [FwLogicProperty(Id: "7Np3MnmdfipuR", IsReadOnly: true)]
        public string VendorColor { get; set; }
        [FwLogicProperty(Id: "4RDOWSMI7rlpm", IsReadOnly: true)]
        public string DescriptionColor { get; set; }

        [FwLogicProperty(Id: "RIko5TlkLtnHf")]
        public string DateStamp { get { return purchaseOrderDetail.DateStamp; } set { purchaseOrderDetail.DateStamp = value; purchaseOrderDetail.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 

        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;
            string rateType = string.Empty;
            bool misc = false, labor = false, subRent = false, subSale = false, repair = false, subMisc = false, subLabor = false;

            if (saveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                rateType = RateType;
                misc = Miscellaneous.GetValueOrDefault(false);
                labor = Labor.GetValueOrDefault(false);
                subRent = SubRent.GetValueOrDefault(false);
                subSale = SubSale.GetValueOrDefault(false);
                repair = Repair.GetValueOrDefault(false);
                subMisc = SubMiscellaneous.GetValueOrDefault(false);
                subLabor = SubLabor.GetValueOrDefault(false);
            }
            else
            {
                if (original != null)
                {
                    PurchaseOrderLogic lOrig = ((PurchaseOrderLogic)original);

                    rateType = RateType ?? lOrig.RateType;
                    misc = (Miscellaneous ?? lOrig.Miscellaneous).GetValueOrDefault(false);
                    labor = (Labor ?? lOrig.Labor).GetValueOrDefault(false);
                    subRent = (SubRent ?? lOrig.SubRent).GetValueOrDefault(false);
                    subSale = (SubSale ?? lOrig.SubSale).GetValueOrDefault(false);
                    repair = (Repair ?? lOrig.Repair).GetValueOrDefault(false);
                    subMisc = (SubMiscellaneous ?? lOrig.SubMiscellaneous).GetValueOrDefault(false);
                    subLabor = (SubLabor ?? lOrig.SubLabor).GetValueOrDefault(false);

                    if (isValid)
                    {
                        if (lOrig.Status.Equals(RwConstants.PURCHASE_ORDER_STATUS_CLOSED) || lOrig.Status.Equals(RwConstants.PURCHASE_ORDER_STATUS_SNAPSHOT) || lOrig.Status.Equals(RwConstants.PURCHASE_ORDER_STATUS_VOID))
                        {
                            isValid = false;
                            validateMsg = "Cannot modify a " + lOrig.Status + " " + BusinessLogicModuleName + ".";
                        }
                    }
                }
            }

            if (isValid)
            {
                if (string.IsNullOrEmpty(rateType) && (misc || labor || subRent || subMisc || subLabor))
                {
                    isValid = false;
                    validateMsg = "Rate Type is required for this " + BusinessLogicModuleName + ".";
                }
            }

            return isValid;
        }
        //------------------------------------------------------------------------------------
        public void OrderDetailAssignPrimaryKeys(object sender, EventArgs e)
        {
            ((DealOrderDetailRecord)sender).OrderId = GetPrimaryKeys()[0].ToString();
        }
        //------------------------------------------------------------------------------------ 
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            PurchaseOrderLogic orig = null;
            if (e.Original != null)
            {
                orig = ((PurchaseOrderLogic)e.Original);
            }
            if (e.SaveMode == TDataRecordSaveMode.smInsert)
            {
                Status = RwConstants.PURCHASE_ORDER_STATUS_NEW;
                if (string.IsNullOrEmpty(PurchaseOrderDate))
                {
                    PurchaseOrderDate = FwConvert.ToShortDate(DateTime.Today);
                }

                if (string.IsNullOrEmpty(BillingCycleId))
                {
                    if (string.IsNullOrEmpty(VendorId))
                    {
                        DefaultSettingsLogic defaults = new DefaultSettingsLogic();
                        defaults.SetDependencies(AppConfig, UserSession);
                        defaults.DefaultSettingsId = RwConstants.CONTROL_ID;
                        bool b = defaults.LoadAsync<DefaultSettingsLogic>().Result;
                        BillingCycleId = defaults.DefaultDealBillingCycleId;
                    }
                    else
                    {
                        VendorLogic vendor = new VendorLogic();
                        vendor.SetDependencies(AppConfig, UserSession);
                        vendor.VendorId = VendorId;
                        bool b = vendor.LoadAsync<VendorLogic>().Result;
                        BillingCycleId = vendor.BillingCycleId;
                    }
                }

                if (string.IsNullOrEmpty(AgentId))
                {
                    AgentId = UserSession.UsersId;
                }
                if (string.IsNullOrEmpty(ProjectManagerId))
                {
                    ProjectManagerId = UserSession.UsersId;
                }

            }

            if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                if (orig != null)
                {
                    ReceiveDeliveryId = orig.ReceiveDeliveryId;
                    ReturnDeliveryId = orig.ReturnDeliveryId;
                    //BillToAddressId = orig.BillToAddressId;
                    TaxId = orig.TaxId;

                    if ((!string.IsNullOrEmpty(CurrencyId)) && (!CurrencyId.Equals(orig.CurrencyId)))
                    {
                        if ((!string.IsNullOrEmpty(ConfirmUpdateAllRatesToNewCurrency)) && (ConfirmUpdateAllRatesToNewCurrency.ToUpper().Equals(RwConstants.UPDATE_RATES_CONFIRMATION)))
                        {
                            _changeRatesToNewCurrency = true;
                        }
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------ 
        public void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
            string newEstimatedStartDate = "", newEstimatedStopDate = "", newBillingStartDate = "", newBillingEndDate = "";//, newEstimatedStartTime = "", newEstimatedStopTime = "";
            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                // this is a newPO.  ReceiveDeliveryId, ReturnDeliveryId, and TaxId were not known at time of insert.  Need to re-update the data with the known ID's
                purchaseOrder.OutDeliveryId = receiveDelivery.DeliveryId;
                purchaseOrder.InDeliveryId = returnDelivery.DeliveryId;
                purchaseOrder.TaxId = tax.TaxId;
                int i = purchaseOrder.SaveAsync(null, e.SqlConnection).Result;
            }
            else // updating
            {
                PurchaseOrderLogic orig = ((PurchaseOrderLogic)e.Original);

                newEstimatedStartDate = EstimatedStartDate ?? orig.EstimatedStartDate;
                //newEstimatedStartTime = EstimatedStartTime ?? orig.EstimatedStartTime;
                newEstimatedStopDate = EstimatedStopDate ?? orig.EstimatedStopDate;
                //newEstimatedStopTime = EstimatedStopTime ?? orig.EstimatedStopTime;
                newBillingStartDate = BillingStartDate ?? orig.BillingStartDate;
                newBillingEndDate = BillingEndDate ?? orig.BillingEndDate;
            }

            if (ActivityDatesAndTimes.Count > 0)
            {
                ApplyOrderDatesAndTimesRequest request = new ApplyOrderDatesAndTimesRequest();
                request.OrderId = GetPrimaryKeys()[0].ToString();
                foreach (OrderDatesLogic d in ActivityDatesAndTimes)
                {
                    OrderDateAndTime dt = new OrderDateAndTime();
                    dt.OrderTypeDateTypeId = d.OrderTypeDateTypeId;

                    //get the Activity Type from the true source: OrderTypeDateType
                    OrderTypeDateTypeLogic otdt = new OrderTypeDateTypeLogic();
                    otdt.SetDependencies(AppConfig, UserSession);
                    otdt.OrderTypeDateTypeId = dt.OrderTypeDateTypeId;
                    bool b = otdt.LoadAsync<OrderTypeDateTypeLogic>(e.SqlConnection).Result;

                    if (!string.IsNullOrEmpty(d.Date))
                    {
                        dt.Date = FwConvert.ToDateTime(d.Date);
                    }
                    dt.Time = d.Time;
                    //dt.IsMilestone = d.IsMilestone;
                    //dt.IsProductionActivity = d.IsProductionActivity;
                    request.DatesAndTimes.Add(dt);
                    //if (otdt.ActivityType.Equals(RwConstants.ACTIVITY_TYPE_PICK))
                    //{
                    //    newPickDate = d.Date;
                    //    newPickTime = d.Time;
                    //}
                    if (otdt.ActivityType.Equals(RwConstants.ACTIVITY_TYPE_RECEIVE))
                    {
                        newEstimatedStartDate = d.Date;
                        //newEstimatedStartTime = d.Time;
                    }
                    else if (otdt.ActivityType.Equals(RwConstants.ACTIVITY_TYPE_RETURN))
                    {
                        newEstimatedStopDate = d.Date;
                        //newEstimatedStopTime = d.Time;
                    }
                }
                ApplyOrderDatesAndTimesResponse response = OrderFunc.ApplyOrderDatesAndTimes(AppConfig, UserSession, request, e.SqlConnection).Result;
            }


            if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                PurchaseOrderLogic orig = ((PurchaseOrderLogic)e.Original);

                if (((newEstimatedStartDate != orig.EstimatedStartDate)) ||
                    //((newEstimatedStartTime != orig.EstimatedStartTime)) ||
                    ((newEstimatedStopDate != orig.EstimatedStopDate)) //||
                                                                       //((newEstimatedStopTime != orig.EstimatedStopTime))
                    )
                {
                    OrderDatesAndTimesChange change = new OrderDatesAndTimesChange();
                    change.OrderId = this.GetPrimaryKeys()[0].ToString();
                    change.OldEstimatedStartDate = orig.EstimatedStartDate;
                    change.NewEstimatedStartDate = newEstimatedStartDate;
                    //change.OldEstimatedStartTime = orig.EstimatedStartTime;
                    //change.NewEstimatedStartTime = newEstimatedStartTime;
                    change.OldEstimatedStopDate = orig.EstimatedStopDate;
                    change.NewEstimatedStopDate = newEstimatedStopDate;
                    //change.OldEstimatedStopTime = orig.EstimatedStopTime;
                    //change.NewEstimatedStopTime = newEstimatedStopTime;
                    bool b = OrderFunc.UpdateOrderItemDatesAndTimes(AppConfig, UserSession, change, e.SqlConnection).Result;
                }

            }

            //if dates are changed, update line-item extendeds
            if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                bool datesChanged = false;

                if (e.Original != null)
                {
                    PurchaseOrderLogic orig = ((PurchaseOrderLogic)e.Original);
                    datesChanged = ((newEstimatedStartDate != orig.EstimatedStartDate) ||
                                    (newEstimatedStopDate != orig.EstimatedStopDate) ||
                                    (newBillingStartDate != orig.BillingStartDate) ||
                                    (newBillingEndDate != orig.BillingEndDate));
                }

                if (datesChanged)
                {
                    bool b = OrderFunc.UpdateOrderItemExtendedAllASync(this.AppConfig, this.UserSession, GetPrimaryKeys()[0].ToString(), e.SqlConnection).Result;
                }
            }

            if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                if (_changeRatesToNewCurrency)
                {
                    TSpStatusResponse resetCurrencyRatesResponse = OrderFunc.ResetOrderCurrencyRates(AppConfig, UserSession, PurchaseOrderId, e.SqlConnection).Result;
                    //if (!response.success)  // need an error message here
                    //{
                    //}
                }
            }



            //after save - do work in the database
            {
                TSpStatusResponse r = PurchaseOrderFunc.AfterSavePurchaseOrder(AppConfig, UserSession, this.GetPrimaryKeys()[0].ToString(), e.SqlConnection).Result;
            }

        }
        //------------------------------------------------------------------------------------
        public void OnBeforeSavePurchaseOrder(object sender, BeforeSaveDataRecordEventArgs e)
        {
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                bool x = purchaseOrder.SetNumber(e.SqlConnection).Result;
                StatusDate = FwConvert.ToShortDate(DateTime.Today);
                if (string.IsNullOrEmpty(TaxOptionId))
                {
                    TaxOptionId = AppFunc.GetLocationAsync(AppConfig, UserSession, OfficeLocationId, "taxoptionid", e.SqlConnection).Result;
                }
            }
            else
            {
                if (e.Original != null)
                {
                    DealOrderRecord lOrig = ((DealOrderRecord)e.Original);

                    if ((tax.TaxId == null) || (tax.TaxId.Equals(string.Empty)))
                    {
                        tax.TaxId = lOrig.TaxId;
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------
        public virtual void OnAfterSavePurchaseOrder(object sender, AfterSaveDataRecordEventArgs e)
        {
            //bool saved = false;
            //billToAddress.UniqueId1 = dealOrder.OrderId;
            //saved = dealOrder.SavePoASync(PoNumber, PoAmount).Result;


            // justin hoffman 03/11/2020
            // this is really stupid
            // I am deleting the record that dbwIU_dealorder is giving us, so I can add my own and avoid a unique index error
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                DealOrderDetailRecord detailRec = new DealOrderDetailRecord();
                detailRec.SetDependencies(AppConfig, UserSession);
                detailRec.OrderId = GetPrimaryKeys()[0].ToString();
                bool b = detailRec.DeleteAsync(e.SqlConnection).Result;
            }

            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smUpdate)
            {
                if (e.Original != null)
                {
                    TaxId = ((DealOrderRecord)e.Original).TaxId;
                }
            }
        }
        //------------------------------------------------------------------------------------
        public void OnAfterSaveTax(object sender, AfterSaveDataRecordEventArgs e)
        {
            if ((!string.IsNullOrEmpty(TaxOptionId)) && (!string.IsNullOrEmpty(TaxId)))
            {
                bool b = false;
                b = AppFunc.UpdateTaxFromTaxOptionASync(this.AppConfig, this.UserSession, TaxOptionId, TaxId, e.SqlConnection).Result;
                b = OrderFunc.UpdateOrderItemExtendedAllASync(this.AppConfig, this.UserSession, GetPrimaryKeys()[0].ToString(), e.SqlConnection).Result;
            }
        }
        //------------------------------------------------------------------------------------
        public async Task<bool> ApplyBottomLineDaysPerWeek(ApplyBottomLineDaysPerWeekRequest request)
        {
            bool success = await purchaseOrder.ApplyBottomLineDaysPerWeek(request);
            return success;
        }
        //------------------------------------------------------------------------------------
        public async Task<bool> ApplyBottomLineDiscountPercent(ApplyBottomLineDiscountPercentRequest request)
        {
            bool success = await purchaseOrder.ApplyBottomLineDiscountPercent(request);
            return success;
        }
        //------------------------------------------------------------------------------------
        public async Task<bool> ApplyBottomLineTotal(ApplyBottomLineTotalRequest request)
        {
            bool success = await purchaseOrder.ApplyBottomLineTotal(request);
            return success;
        }
        //------------------------------------------------------------------------------------
        public async Task<VoidPurchaseOrderResponse> Void()
        {
            return await purchaseOrder.Void();
        }
        //------------------------------------------------------------------------------------ 
    }
}
