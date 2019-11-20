using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using Newtonsoft.Json;
using System;
using WebApi.Logic;
using WebApi.Modules.Agent.Order;
using WebApi.Modules.HomeControls.DealOrder;
using WebApi.Modules.HomeControls.DealOrderDetail;
using WebApi.Modules.HomeControls.Delivery;
using WebApi;

namespace WebApi.Modules.Transfers.TransferOrder
{
    [FwLogic(Id: "Fm4cDJJBjdhw")]
    public class TransferOrderLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        DealOrderRecord transferOrder = new DealOrderRecord();
        DealOrderDetailRecord transferOrderDetail = new DealOrderDetailRecord();
        DeliveryRecord outDelivery = new DeliveryRecord();
        DeliveryRecord inDelivery = new DeliveryRecord();

        TransferOrderLoader transferOrderLoader = new TransferOrderLoader();
        TransferOrderBrowseLoader transferOrderBrowseLoader = new TransferOrderBrowseLoader();
        public TransferOrderLogic()
        {
            dataRecords.Add(transferOrder);
            dataRecords.Add(transferOrderDetail);
            dataRecords.Add(outDelivery);
            dataRecords.Add(inDelivery);
            dataLoader = transferOrderLoader;
            browseLoader = transferOrderBrowseLoader;

            Type = RwConstants.ORDER_TYPE_TRANSFER;

            transferOrder.BeforeSave += OnBeforeSaveTransferOrder;
            transferOrder.AfterSave += OnAfterSaveTransferOrder;
            transferOrderDetail.AssignPrimaryKeys += TransferDetailAssignPrimaryKeys;

            BeforeSave += OnBeforeSave;
            AfterSave += OnAfterSave;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "SGlehEEeuOjZO", IsPrimaryKey: true)]
        public string TransferId { get { return transferOrder.OrderId; } set { transferOrder.OrderId = value; transferOrderDetail.OrderId = value; } }


        [JsonIgnore]
        [FwLogicProperty(Id: "WBixPxC9V26zF", DisableDirectModify: true)]
        public string Type { get { return transferOrder.Type; } set { transferOrder.Type = value; } }


        [FwLogicProperty(Id: "MLLWReJbTVSv", IsRecordTitle: true, DisableDirectAssign: true, DisableDirectModify: true)]
        public string TransferNumber { get { return transferOrder.OrderNumber; } set { transferOrder.OrderNumber = value; } }

        [FwLogicProperty(Id: "ZDlV73IYEcvy")]
        public string TransferDate { get { return transferOrder.OrderDate; } set { transferOrder.OrderDate = value; } }

        [FwLogicProperty(Id: "FGez2l5px1Dx")]
        public string Description { get { return transferOrder.Description; } set { transferOrder.Description = value; } }

        [FwLogicProperty(Id: "v6W1vhrrWm2k", DisableDirectAssign: true, DisableDirectModify: true)]
        public string Status { get { return transferOrder.Status; } set { transferOrder.Status = value; } }

        [FwLogicProperty(Id: "ATnCWGYBdRofG", DisableDirectAssign: true, DisableDirectModify: true)]
        public string StatusDate { get { return transferOrder.StatusDate; } set { transferOrder.StatusDate = value; } }

        [FwLogicProperty(Id: "PC4LY7FVRbyx")]
        public string FromWarehouseId { get { return transferOrder.FromWarehouseId; } set { transferOrder.FromWarehouseId = value; } }

        [FwLogicProperty(Id: "pAJbBxN0u0iu", IsReadOnly: true)]
        public string FromWarehouse { get; set; }

        [FwLogicProperty(Id: "9L1dLPqDOBuOQ", IsReadOnly: true)]
        public string FromWarehouseCode { get; set; }

        [FwLogicProperty(Id: "4Z71LHyG45F3j")]
        public string ToWarehouseId { get { return transferOrder.WarehouseId; } set { transferOrder.WarehouseId = value; } }

        [FwLogicProperty(Id: "hEaKt19UKcRz", IsReadOnly: true)]
        public string ToWarehouse { get; set; }

        [FwLogicProperty(Id: "UF8qZFNETI0x7", IsReadOnly: true)]
        public string ToWarehouseCode { get; set; }

        [FwLogicProperty(Id: "FJQLU7kwlJOR")]
        public string DepartmentId { get { return transferOrder.DepartmentId; } set { transferOrder.DepartmentId = value; } }

        [FwLogicProperty(Id: "Ei1ou8nWqGxd", IsReadOnly: true)]
        public string Department { get; set; }

        [FwLogicProperty(Id: "cAw9W17inqZBr")]
        public bool? Rental { get { return transferOrder.Rental; } set { transferOrder.Rental = value; } }

        [FwLogicProperty(Id: "2KGlQTb3nceEL")]
        public bool? Sales { get { return transferOrder.Sales; } set { transferOrder.Sales = value; } }

        [FwLogicProperty(Id: "6EcHrt37R4zIA")]
        public string AgentId { get { return transferOrder.AgentId; } set { transferOrder.AgentId = value; } }

        [FwLogicProperty(Id: "veS3S3InSvQDS", IsReadOnly: true)]
        public string Agent { get; set; }

        [FwLogicProperty(Id: "GKJHWdgxCEtUS", IsReadOnly: true)]
        public string RelatedToOrderId { get; set; }

        [FwLogicProperty(Id: "dCW5zlMgVqQs", IsReadOnly: true)]
        public string RelatedToOrderNumber { get; set; }

        [FwLogicProperty(Id: "SZosgj3Ktskim", DisableDirectModify: true)]
        public string OfficeLocationId { get { return transferOrder.OfficeLocationId; } set { transferOrder.OfficeLocationId = value; } }

        [FwLogicProperty(Id: "YqNC9HI9TjfyP", IsReadOnly: true)]
        public string OfficeLocation { get; set; }

        [FwLogicProperty(Id: "kBx2hobWvMgT", DisableDirectModify: true)]
        public bool? IsReturnTransferOrder { get { return transferOrderDetail.IsReturnTransferOrder; } set { transferOrderDetail.IsReturnTransferOrder = value; } }

        [FwLogicProperty(Id: "EgZDjXm8CZKGD")]
        public string PickDate { get { return transferOrder.PickDate; } set { transferOrder.PickDate = value; } }

        [FwLogicProperty(Id: "nfDtDF2adG84y")]
        public string PickTime { get { return transferOrder.PickTime; } set { transferOrder.PickTime = value; } }

        [FwLogicProperty(Id: "dgIJeuGQtj4l", IsReadOnly: true)]
        public string ShipDate { get { return transferOrder.EstimatedStartDate; } set { transferOrder.EstimatedStartDate = value; outDelivery.TargetShipDate = value; } }

        [FwLogicProperty(Id: "QW1dQRJ6eKsl5", IsReadOnly: true)]
        public string ShipTime { get { return transferOrder.EstimatedStartTime; } set { transferOrder.EstimatedStartTime = value; outDelivery.TargetShipTime = value; } }

        [FwLogicProperty(Id: "AjVkvBVuspGif", IsReadOnly: true)]
        public string RequiredDate { get { return transferOrderDetail.ReceiveDate; } set { transferOrderDetail.ReceiveDate = value; } }

        [FwLogicProperty(Id: "io1lEB0j5r3mo", IsReadOnly: true)]
        public string RequiredTime { get { return transferOrderDetail.ReceiveTime; } set { transferOrderDetail.ReceiveTime = value; } }


        [FwLogicProperty(Id: "CaQAkkrCbFg8g", DisableDirectAssign: true, DisableDirectModify: true)]
        public string OutDeliveryId { get { return transferOrder.OutDeliveryId; } set { transferOrder.OutDeliveryId = value; outDelivery.DeliveryId = value; } }

        [FwLogicProperty(Id: "5hz7xVp0qRtdD")]
        public string OutDeliveryDeliveryType { get { return outDelivery.DeliveryType; } set { outDelivery.DeliveryType = value; } }

        [FwLogicProperty(Id: "O9xNfkHu0bItB")]
        public string OutDeliveryRequiredDate { get { return outDelivery.RequiredDate; } set { outDelivery.RequiredDate = value; } }

        [FwLogicProperty(Id: "Akpl8YTGIrTmm")]
        public string OutDeliveryRequiredTime { get { return outDelivery.RequiredTime; } set { outDelivery.RequiredTime = value; } }

        [FwLogicProperty(Id: "gZgOPA1duznsr")]
        public string OutDeliveryTargetShipDate { get { return outDelivery.TargetShipDate; } set { outDelivery.TargetShipDate = value; transferOrder.EstimatedStartDate = value; } }

        [FwLogicProperty(Id: "0MSndu7T9XN3X")]
        public string OutDeliveryTargetShipTime { get { return outDelivery.TargetShipTime; } set { outDelivery.TargetShipTime = value; transferOrder.EstimatedStartTime = value; } }

        [FwLogicProperty(Id: "9E3elcad0u6v9")]
        public string OutDeliveryDirection { get { return outDelivery.Direction; } set { outDelivery.Direction = value; } }

        [FwLogicProperty(Id: "0K43r7Ey6KYiN")]
        public string OutDeliveryAddressType { get { return outDelivery.AddressType; } set { outDelivery.AddressType = value; } }

        [FwLogicProperty(Id: "uu7FF6KYRO9Az")]
        public string OutDeliveryFromLocation { get { return outDelivery.FromLocation; } set { outDelivery.FromLocation = value; } }

        [FwLogicProperty(Id: "jDPJVfTWb9N9S")]
        public string OutDeliveryFromContact { get { return outDelivery.FromContact; } set { outDelivery.FromContact = value; } }

        [FwLogicProperty(Id: "CF3xELzBrePvm")]
        public string OutDeliveryFromContactPhone { get { return outDelivery.FromContactPhone; } set { outDelivery.FromContactPhone = value; } }

        [FwLogicProperty(Id: "WJXk69uDnbSpZ")]
        public string OutDeliveryFromAlternateContact { get { return outDelivery.FromAlternateContact; } set { outDelivery.FromAlternateContact = value; } }

        [FwLogicProperty(Id: "QeX9yShzjDZiv")]
        public string OutDeliveryFromAlternateContactPhone { get { return outDelivery.FromAlternateContactPhone; } set { outDelivery.FromAlternateContactPhone = value; } }

        [FwLogicProperty(Id: "cgItjeWudViRM")]
        public string OutDeliveryFromAttention { get { return outDelivery.FromAttention; } set { outDelivery.FromAttention = value; } }

        [FwLogicProperty(Id: "oT2VgUxr7o5Aw")]
        public string OutDeliveryFromAddress1 { get { return outDelivery.FromAddress1; } set { outDelivery.FromAddress1 = value; } }

        [FwLogicProperty(Id: "9mmSHjCC8ZXVB")]
        public string OutDeliveryFromAdd2ress { get { return outDelivery.FromAdd2ress; } set { outDelivery.FromAdd2ress = value; } }

        [FwLogicProperty(Id: "c6CZ43w2xo3X8")]
        public string OutDeliveryFromCity { get { return outDelivery.FromCity; } set { outDelivery.FromCity = value; } }

        [FwLogicProperty(Id: "seCA7sN2TCQJS")]
        public string OutDeliveryFromState { get { return outDelivery.FromState; } set { outDelivery.FromState = value; } }

        [FwLogicProperty(Id: "MUc2V46LNpiTW")]
        public string OutDeliveryFromZipCode { get { return outDelivery.FromZipCode; } set { outDelivery.FromZipCode = value; } }

        [FwLogicProperty(Id: "nzh5nHtsIzHVr", IsReadOnly: true)]
        public string OutDeliveryFromCountry { get; set; }

        [FwLogicProperty(Id: "fyBsbPQa2lwC7")]
        public string OutDeliveryFromCountryId { get { return outDelivery.FromCountryId; } set { outDelivery.FromCountryId = value; } }

        [FwLogicProperty(Id: "eAjbnqEMXfHaT")]
        public string OutDeliveryFromCrossStreets { get { return outDelivery.FromCrossStreets; } set { outDelivery.FromCrossStreets = value; } }

        [FwLogicProperty(Id: "SoDX749jR1zqo")]
        public string OutDeliveryToLocation { get { return outDelivery.Location; } set { outDelivery.Location = value; } }

        [FwLogicProperty(Id: "dF2voNMOl9Epr")]
        public string OutDeliveryToContact { get { return outDelivery.Contact; } set { outDelivery.Contact = value; } }

        [FwLogicProperty(Id: "1O7jZ7RGORTF1")]
        public string OutDeliveryToContactPhone { get { return outDelivery.ContactPhone; } set { outDelivery.ContactPhone = value; } }

        [FwLogicProperty(Id: "iqwUvlfW5xpII")]
        public string OutDeliveryToAlternateContact { get { return outDelivery.AlternateContact; } set { outDelivery.AlternateContact = value; } }

        [FwLogicProperty(Id: "3w0ehoij9B3WV")]
        public string OutDeliveryToAlternateContactPhone { get { return outDelivery.AlternateContactPhone; } set { outDelivery.AlternateContactPhone = value; } }

        [FwLogicProperty(Id: "aEjFDTaXyNWQZ")]
        public string OutDeliveryToAttention { get { return outDelivery.Attention; } set { outDelivery.Attention = value; } }

        [FwLogicProperty(Id: "MS59fqI4CVCzf")]
        public string OutDeliveryToAddress1 { get { return outDelivery.Address1; } set { outDelivery.Address1 = value; } }

        [FwLogicProperty(Id: "hjFttmuSHqPyH")]
        public string OutDeliveryToAddress2 { get { return outDelivery.Address2; } set { outDelivery.Address2 = value; } }

        [FwLogicProperty(Id: "VmVhsIMi6tkUk")]
        public string OutDeliveryToCity { get { return outDelivery.City; } set { outDelivery.City = value; } }

        [FwLogicProperty(Id: "nJqzZuG7nTbQH")]
        public string OutDeliveryToState { get { return outDelivery.State; } set { outDelivery.State = value; } }

        [FwLogicProperty(Id: "wTSAwamlbLWDz")]
        public string OutDeliveryToZipCode { get { return outDelivery.ZipCode; } set { outDelivery.ZipCode = value; } }

        [FwLogicProperty(Id: "B82eDzE62Rxns")]
        public string OutDeliveryToCountryId { get { return outDelivery.CountryId; } set { outDelivery.CountryId = value; } }

        [FwLogicProperty(Id: "9nvzbdj8sG5EC", IsReadOnly: true)]
        public string OutDeliveryToCountry { get; set; }

        [FwLogicProperty(Id: "5RKQo5CGGeY3d")]
        public string OutDeliveryToContactFax { get { return outDelivery.ContactFax; } set { outDelivery.ContactFax = value; } }

        [FwLogicProperty(Id: "nkhxuEhGDXLIg")]
        public string OutDeliveryToCrossStreets { get { return outDelivery.CrossStreets; } set { outDelivery.CrossStreets = value; } }

        [FwLogicProperty(Id: "DCsI6nSqY9CaV")]
        public string OutDeliveryDeliveryNotes { get { return outDelivery.DeliveryNotes; } set { outDelivery.DeliveryNotes = value; } }

        [FwLogicProperty(Id: "ZuaOMNAODFPAw")]
        public string OutDeliveryCarrierId { get { return outDelivery.CarrierId; } set { outDelivery.CarrierId = value; } }

        [FwLogicProperty(Id: "zVeEmen4gIz6E", IsReadOnly: true)]
        public string OutDeliveryCarrier { get; set; }

        [FwLogicProperty(Id: "nCD6sUAcSFDJ3")]
        public string OutDeliveryCarrierAccount { get { return outDelivery.CarrierAccount; } set { outDelivery.CarrierAccount = value; } }

        [FwLogicProperty(Id: "FPs5MKuI3uNz8")]
        public string OutDeliveryShipViaId { get { return outDelivery.ShipViaId; } set { outDelivery.ShipViaId = value; } }

        [FwLogicProperty(Id: "dE5befBn1SpMW", IsReadOnly: true)]
        public string OutDeliveryShipVia { get; set; }

        [FwLogicProperty(Id: "hADMhJlJ6CIAL")]
        public string OutDeliveryInvoiceId { get { return outDelivery.InvoiceId; } set { outDelivery.InvoiceId = value; } }

        [FwLogicProperty(Id: "QdgOyYDXdBPbI")]
        public string OutDeliveryVendorInvoiceId { get { return outDelivery.VendorInvoiceId; } set { outDelivery.VendorInvoiceId = value; } }

        [FwLogicProperty(Id: "wKpcRVhmbxjYj")]
        public decimal? OutDeliveryEstimatedFreight { get { return outDelivery.EstimatedFreight; } set { outDelivery.EstimatedFreight = value; } }

        [FwLogicProperty(Id: "SIx2F4GQdzB4H")]
        public decimal? OutDeliveryFreightInvoiceAmount { get { return outDelivery.FreightInvoiceAmount; } set { outDelivery.FreightInvoiceAmount = value; } }

        [FwLogicProperty(Id: "cjQYwcoMxfeOc")]
        public string OutDeliveryChargeType { get { return outDelivery.ChargeType; } set { outDelivery.ChargeType = value; } }

        [FwLogicProperty(Id: "wKrmJmPmMNRFH")]
        public string OutDeliveryFreightTrackingNumber { get { return outDelivery.FreightTrackingNumber; } set { outDelivery.FreightTrackingNumber = value; } }

        [FwLogicProperty(Id: "fiffZlARjzLHL")]
        public bool? OutDeliveryDropShip { get { return outDelivery.DropShip; } set { outDelivery.DropShip = value; } }

        [FwLogicProperty(Id: "ujpmKZ14Ux8jf")]
        public string OutDeliveryPackageCode { get { return outDelivery.PackageCode; } set { outDelivery.PackageCode = value; } }

        [FwLogicProperty(Id: "6jPuuNnwyYGrk")]
        public bool? OutDeliveryBillPoFreightOnOrder { get { return outDelivery.BillPoFreightOnOrder; } set { outDelivery.BillPoFreightOnOrder = value; } }

        [FwLogicProperty(Id: "oqmnkQn9WFYId")]
        public string OutDeliveryOnlineOrderNumber { get { return outDelivery.OnlineOrderNumber; } set { outDelivery.OnlineOrderNumber = value; } }

        [FwLogicProperty(Id: "dtc8iGe2X9ByU")]
        public string OutDeliveryOnlineOrderStatus { get { return outDelivery.OnlineOrderStatus; } set { outDelivery.OnlineOrderStatus = value; } }

        [FwLogicProperty(Id: "RY8gqerc3GqRx")]
        public string OutDeliveryDateStamp { get { return outDelivery.DateStamp; } set { outDelivery.DateStamp = value; } }







        [FwLogicProperty(Id: "n4Y5DOxU2BFwr", DisableDirectAssign: true, DisableDirectModify: true)]
        public string InDeliveryId { get { return transferOrder.InDeliveryId; } set { transferOrder.InDeliveryId = value; inDelivery.DeliveryId = value; } }

        [FwLogicProperty(Id: "3TRnr2iBmMxnc")]
        public string InDeliveryDeliveryType { get { return inDelivery.DeliveryType; } set { inDelivery.DeliveryType = value; } }

        [FwLogicProperty(Id: "D3ns2T4KYnTr3")]
        public string InDeliveryRequiredDate { get { return inDelivery.RequiredDate; } set { inDelivery.RequiredDate = value; } }

        [FwLogicProperty(Id: "oxWCMwoU4y1Pj")]
        public string InDeliveryRequiredTime { get { return inDelivery.RequiredTime; } set { inDelivery.RequiredTime = value; } }

        [FwLogicProperty(Id: "l7N3zVFk3e9Uf")]
        public string InDeliveryTargetShipDate { get { return inDelivery.TargetShipDate; } set { inDelivery.TargetShipDate = value; } }

        [FwLogicProperty(Id: "fODLpaNKjoUVN")]
        public string InDeliveryTargetShipTime { get { return inDelivery.TargetShipTime; } set { inDelivery.TargetShipTime = value; } }

        [FwLogicProperty(Id: "g6PrIoM04y2eo")]
        public string InDeliveryDirection { get { return inDelivery.Direction; } set { inDelivery.Direction = value; } }

        [FwLogicProperty(Id: "EPVHTYyYgk7Iw")]
        public string InDeliveryAddressType { get { return inDelivery.AddressType; } set { inDelivery.AddressType = value; } }

        [FwLogicProperty(Id: "GfLKvhHBjxsat")]
        public string InDeliveryFromLocation { get { return inDelivery.FromLocation; } set { inDelivery.FromLocation = value; } }

        [FwLogicProperty(Id: "WPhGb2GrNwqea")]
        public string InDeliveryFromContact { get { return inDelivery.FromContact; } set { inDelivery.FromContact = value; } }

        [FwLogicProperty(Id: "HKmdZH12pDInF")]
        public string InDeliveryFromContactPhone { get { return inDelivery.FromContactPhone; } set { inDelivery.FromContactPhone = value; } }

        [FwLogicProperty(Id: "CmojXNfLnfwQC")]
        public string InDeliveryFromAlternateContact { get { return inDelivery.FromAlternateContact; } set { inDelivery.FromAlternateContact = value; } }

        [FwLogicProperty(Id: "vzLye138rjzxk")]
        public string InDeliveryFromAlternateContactPhone { get { return inDelivery.FromAlternateContactPhone; } set { inDelivery.FromAlternateContactPhone = value; } }

        [FwLogicProperty(Id: "Dwa544NmHiEFp")]
        public string InDeliveryFromAttention { get { return inDelivery.FromAttention; } set { inDelivery.FromAttention = value; } }

        [FwLogicProperty(Id: "lgWWMrssGff4X")]
        public string InDeliveryFromAddress1 { get { return inDelivery.FromAddress1; } set { inDelivery.FromAddress1 = value; } }

        [FwLogicProperty(Id: "6Pi0STWNC4zco")]
        public string InDeliveryFromAdd2ress { get { return inDelivery.FromAdd2ress; } set { inDelivery.FromAdd2ress = value; } }

        [FwLogicProperty(Id: "CrSVQQAO6rZJE")]
        public string InDeliveryFromCity { get { return inDelivery.FromCity; } set { inDelivery.FromCity = value; } }

        [FwLogicProperty(Id: "2GWqnQhehtt5g")]
        public string InDeliveryFromState { get { return inDelivery.FromState; } set { inDelivery.FromState = value; } }

        [FwLogicProperty(Id: "DMtTXJfm0iFjD")]
        public string InDeliveryFromZipCode { get { return inDelivery.FromZipCode; } set { inDelivery.FromZipCode = value; } }

        [FwLogicProperty(Id: "cDjsm4zA7amCSe", IsReadOnly: true)]
        public string InDeliveryFromCountry { get; set; }

        [FwLogicProperty(Id: "f26qoi9bZokzA")]
        public string InDeliveryFromCountryId { get { return inDelivery.FromCountryId; } set { inDelivery.FromCountryId = value; } }

        [FwLogicProperty(Id: "W8XqYYzu6Jj6x")]
        public string InDeliveryFromCrossStreets { get { return inDelivery.FromCrossStreets; } set { inDelivery.FromCrossStreets = value; } }

        [FwLogicProperty(Id: "feOXymWd1BMq1")]
        public string InDeliveryToLocation { get { return inDelivery.Location; } set { inDelivery.Location = value; } }

        [FwLogicProperty(Id: "4GMeEwnt6hIm3")]
        public string InDeliveryToContact { get { return inDelivery.Contact; } set { inDelivery.Contact = value; } }

        [FwLogicProperty(Id: "sXTzd2ogojZ9i")]
        public string InDeliveryToContactPhone { get { return inDelivery.ContactPhone; } set { inDelivery.ContactPhone = value; } }

        [FwLogicProperty(Id: "HdHKibs0qG9lh")]
        public string InDeliveryToAlternateContact { get { return inDelivery.AlternateContact; } set { inDelivery.AlternateContact = value; } }

        [FwLogicProperty(Id: "a8J6NrkJpp4Ml")]
        public string InDeliveryToAlternateContactPhone { get { return inDelivery.AlternateContactPhone; } set { inDelivery.AlternateContactPhone = value; } }

        [FwLogicProperty(Id: "4QqowW2vdugj2")]
        public string InDeliveryToAttention { get { return inDelivery.Attention; } set { inDelivery.Attention = value; } }

        [FwLogicProperty(Id: "QTXd6KhJEltLj")]
        public string InDeliveryToAddress1 { get { return inDelivery.Address1; } set { inDelivery.Address1 = value; } }

        [FwLogicProperty(Id: "HOfxi6D9PPbJl")]
        public string InDeliveryToAddress2 { get { return inDelivery.Address2; } set { inDelivery.Address2 = value; } }

        [FwLogicProperty(Id: "kkWFc2GwHiHoL")]
        public string InDeliveryToCity { get { return inDelivery.City; } set { inDelivery.City = value; } }

        [FwLogicProperty(Id: "8MVa2qvfsV6jo")]
        public string InDeliveryToState { get { return inDelivery.State; } set { inDelivery.State = value; } }

        [FwLogicProperty(Id: "ilD2aTpE2kce7")]
        public string InDeliveryToZipCode { get { return inDelivery.ZipCode; } set { inDelivery.ZipCode = value; } }

        [FwLogicProperty(Id: "5vV94JlT13VC1")]
        public string InDeliveryToCountryId { get { return inDelivery.CountryId; } set { inDelivery.CountryId = value; } }

        [FwLogicProperty(Id: "ETvmh3tLpYmk2", IsReadOnly: true)]
        public string InDeliveryToCountry { get; set; }

        [FwLogicProperty(Id: "uTlucMOpYN5IP")]
        public string InDeliveryToContactFax { get { return inDelivery.ContactFax; } set { inDelivery.ContactFax = value; } }

        [FwLogicProperty(Id: "yL11W6sJfrie0")]
        public string InDeliveryToCrossStreets { get { return inDelivery.CrossStreets; } set { inDelivery.CrossStreets = value; } }

        [FwLogicProperty(Id: "TYlEwXPLvpga1")]
        public string InDeliveryDeliveryNotes { get { return inDelivery.DeliveryNotes; } set { inDelivery.DeliveryNotes = value; } }

        [FwLogicProperty(Id: "eXMVqbd8yZFyz")]
        public string InDeliveryCarrierId { get { return inDelivery.CarrierId; } set { inDelivery.CarrierId = value; } }

        [FwLogicProperty(Id: "KZFWe5rSOYIEV", IsReadOnly: true)]
        public string InDeliveryCarrier { get; set; }

        [FwLogicProperty(Id: "0QYUw8420nYy3")]
        public string InDeliveryCarrierAccount { get { return inDelivery.CarrierAccount; } set { inDelivery.CarrierAccount = value; } }

        [FwLogicProperty(Id: "zzQHxUl8Jca3K")]
        public string InDeliveryShipViaId { get { return inDelivery.ShipViaId; } set { inDelivery.ShipViaId = value; } }

        [FwLogicProperty(Id: "ubnNssR8lwCuv", IsReadOnly: true)]
        public string InDeliveryShipVia { get; set; }

        [FwLogicProperty(Id: "RxmgRzqs071jy")]
        public string InDeliveryInvoiceId { get { return inDelivery.InvoiceId; } set { inDelivery.InvoiceId = value; } }

        [FwLogicProperty(Id: "6Re4X5PiP16BE")]
        public string InDeliveryVendorInvoiceId { get { return inDelivery.VendorInvoiceId; } set { inDelivery.VendorInvoiceId = value; } }

        [FwLogicProperty(Id: "Gyjw86vOHZRCN")]
        public decimal? InDeliveryEstimatedFreight { get { return inDelivery.EstimatedFreight; } set { inDelivery.EstimatedFreight = value; } }

        [FwLogicProperty(Id: "5gFGr5tsO38Xe")]
        public decimal? InDeliveryFreightInvoiceAmount { get { return inDelivery.FreightInvoiceAmount; } set { inDelivery.FreightInvoiceAmount = value; } }

        [FwLogicProperty(Id: "aty2HfI2rB3im")]
        public string InDeliveryChargeType { get { return inDelivery.ChargeType; } set { inDelivery.ChargeType = value; } }

        [FwLogicProperty(Id: "JfGmedL76ueqW")]
        public string InDeliveryFreightTrackingNumber { get { return inDelivery.FreightTrackingNumber; } set { inDelivery.FreightTrackingNumber = value; } }

        [FwLogicProperty(Id: "xsfqxlMsweyne")]
        public bool? InDeliveryDropShip { get { return inDelivery.DropShip; } set { inDelivery.DropShip = value; } }

        [FwLogicProperty(Id: "PqaDln2biVTdT")]
        public string InDeliveryPackageCode { get { return inDelivery.PackageCode; } set { inDelivery.PackageCode = value; } }

        [FwLogicProperty(Id: "Pi3JD2KuWSezi")]
        public bool? InDeliveryBillPoFreightOnOrder { get { return inDelivery.BillPoFreightOnOrder; } set { inDelivery.BillPoFreightOnOrder = value; } }

        [FwLogicProperty(Id: "cymuXhA9V1sUO")]
        public string InDeliveryOnlineOrderNumber { get { return inDelivery.OnlineOrderNumber; } set { inDelivery.OnlineOrderNumber = value; } }

        [FwLogicProperty(Id: "eShGQXMtANea0")]
        public string InDeliveryOnlineOrderStatus { get { return inDelivery.OnlineOrderStatus; } set { inDelivery.OnlineOrderStatus = value; } }

        [FwLogicProperty(Id: "eheKLWCxZC6Yv")]
        public string InDeliveryDateStamp { get { return inDelivery.DateStamp; } set { inDelivery.DateStamp = value; } }




        [FwLogicProperty(Id: "crH6wTINbMj9z")]
        public string DateStamp { get { return transferOrder.DateStamp; } set { transferOrder.DateStamp = value; transferOrderDetail.DateStamp = value; } }
        //------------------------------------------------------------------------------------
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg) 
        //{                   
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //}                    
        //-------------------- ---------------------------------------------------------------- 
        public void OnBeforeSaveTransferOrder(object sender, BeforeSaveDataRecordEventArgs e)
        {
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                bool x = transferOrder.SetNumber(e.SqlConnection).Result;
                Status = RwConstants.TRANSFER_STATUS_NEW;
                StatusDate = FwConvert.ToString(DateTime.Today);
                TransferDate = FwConvert.ToString(DateTime.Today);
            }
        }
        //------------------------------------------------------------------------------------
        public void OnAfterSaveTransferOrder(object sender, AfterSaveDataRecordEventArgs e)
        {
            // justin hoffman 11/15/2019 #1307
            // this is really stupid
            // I am deleting the record that dbwIU_dealorder is giving us, so I can add my own and avoid a unique index error
            if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            {
                DealOrderDetailRecord detailRec = new DealOrderDetailRecord();
                detailRec.SetDependencies(AppConfig, UserSession);
                detailRec.OrderId = TransferId;
                bool b = detailRec.DeleteAsync(e.SqlConnection).Result;
            }
        }
        //------------------------------------------------------------------------------------
        public void TransferDetailAssignPrimaryKeys(object sender, EventArgs e)
        {
            ((DealOrderDetailRecord)sender).OrderId = TransferId;
        }
        //------------------------------------------------------------------------------------ 
        public virtual void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            if (e.SaveMode == TDataRecordSaveMode.smInsert)
            {
                if (string.IsNullOrEmpty(AgentId))
                {
                    AgentId = UserSession.UsersId;
                }
            }

            if (e.SaveMode == TDataRecordSaveMode.smUpdate)
            {
                if (e.Original != null)
                {
                    TransferOrderLogic orig = ((TransferOrderLogic)e.Original);
                    OutDeliveryId = orig.OutDeliveryId;
                    InDeliveryId = orig.InDeliveryId;
                }
            }
        }
        //------------------------------------------------------------------------------------
        public void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
            if (e.SaveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                // this is a new Transfer.  OutDeliveryId and InDeliveryId were not known at time of insert.  Need to re-update the data with the known ID's
                transferOrder.OutDeliveryId = outDelivery.DeliveryId;
                transferOrder.InDeliveryId = inDelivery.DeliveryId;
                int i = transferOrder.SaveAsync(null).Result;
            }
            else
            {
                // this is a modfied Quote/Order
                if (e.Original != null)
                {
                    TransferOrderLogic orig = ((TransferOrderLogic)e.Original);

                    if (
                        ((PickDate != null) && (PickDate != orig.PickDate)) ||
                        ((PickTime != null) && (PickTime != orig.PickTime)) ||
                        ((ShipDate != null) && (ShipDate != orig.ShipDate)) ||
                        ((ShipTime != null) && (ShipTime != orig.ShipTime)) ||
                        ((RequiredDate != null) && (RequiredDate != orig.RequiredDate)) ||
                        ((RequiredTime != null) && (RequiredTime != orig.RequiredTime))
                        )
                    {
                        OrderDatesAndTimesChange change = new OrderDatesAndTimesChange();
                        change.OrderId = this.GetPrimaryKeys()[0].ToString();
                        change.OldPickDate = orig.PickDate;
                        change.NewPickDate = PickDate ?? orig.PickDate;
                        change.OldPickTime = orig.PickTime;
                        change.NewPickTime = PickTime ?? orig.PickTime;
                        change.OldEstimatedStartDate = orig.ShipDate;
                        change.NewEstimatedStartDate = ShipDate ?? orig.ShipDate;
                        change.OldEstimatedStartTime = orig.ShipTime;
                        change.NewEstimatedStartTime = ShipTime ?? orig.ShipTime;
                        change.OldEstimatedStopDate = orig.RequiredDate;
                        change.NewEstimatedStopDate = RequiredDate ?? orig.RequiredDate;
                        change.OldEstimatedStopTime = orig.RequiredTime;
                        change.NewEstimatedStopTime = RequiredTime ?? orig.RequiredTime;
                        bool b = OrderFunc.UpdateOrderItemDatesAndTimes(AppConfig, UserSession, change, e.SqlConnection).Result;
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------
    }
}
