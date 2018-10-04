using FwStandard.BusinessLogic;
using FwStandard.BusinessLogic.Attributes;
using System.Reflection;
using System.Text;
using WebApi.Logic;
using WebLibrary;

namespace WebApi.Modules.Home.Delivery
{
    public class DeliveryLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        DeliveryRecord delivery = new DeliveryRecord();
        DeliveryLoader deliveryLoader = new DeliveryLoader();
        public DeliveryLogic()
        {
            dataRecords.Add(delivery);
            dataLoader = deliveryLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string DeliveryId { get { return delivery.DeliveryId; } set { delivery.DeliveryId = value; } }
        public string OrderId { get { return delivery.OrderId; } set { delivery.OrderId = value; } }
        public string DeliveryType { get { return delivery.DeliveryType; } set { delivery.DeliveryType = value; } }
        public string RequiredDate { get { return delivery.RequiredDate; } set { delivery.RequiredDate = value; } }
        public string RequiredTime { get { return delivery.RequiredTime; } set { delivery.RequiredTime = value; } }
        public string TargetShipDate { get { return delivery.TargetShipDate; } set { delivery.TargetShipDate = value; } }
        public string TargetShipTime { get { return delivery.TargetShipTime; } set { delivery.TargetShipTime = value; } }
        public string Direction { get { return delivery.Direction; } set { delivery.Direction = value; } }
        public string AddressType { get { return delivery.AddressType; } set { delivery.AddressType = value; } }

        public string FromLocation { get { return delivery.FromLocation; } set { delivery.FromLocation = value; } }
        public string FromContact { get { return delivery.FromContact; } set { delivery.FromContact = value; } }
        public string FromContactPhone { get { return delivery.FromContactPhone; } set { delivery.FromContactPhone = value; } }
        public string FromAlternateContact { get { return delivery.FromAlternateContact; } set { delivery.FromAlternateContact = value; } }
        public string FromAlternateContactPhone { get { return delivery.FromAlternateContactPhone; } set { delivery.FromAlternateContactPhone = value; } }
        public string FromAttention { get { return delivery.FromAttention; } set { delivery.FromAttention = value; } }
        public string FromAddress1 { get { return delivery.FromAddress1; } set { delivery.FromAddress1 = value; } }
        public string FromAdd2ress { get { return delivery.FromAdd2ress; } set { delivery.FromAdd2ress = value; } }
        public string FromCity { get { return delivery.FromCity; } set { delivery.FromCity = value; } }
        public string FromState { get { return delivery.FromState; } set { delivery.FromState = value; } }
        public string FromZipCode { get { return delivery.FromZipCode; } set { delivery.FromZipCode = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string FromCountry { get; set; }
        public string FromCountryId { get { return delivery.FromCountryId; } set { delivery.FromCountryId = value; } }
        public string FromCrossStreets { get { return delivery.FromCrossStreets; } set { delivery.FromCrossStreets = value; } }


        public string ToLocation { get { return delivery.Location; } set { delivery.Location = value; } }
        public string ToContact { get { return delivery.Contact; } set { delivery.Contact = value; } }
        public string ToContactPhone { get { return delivery.ContactPhone; } set { delivery.ContactPhone = value; } }
        public string ToAlternateContact { get { return delivery.AlternateContact; } set { delivery.AlternateContact = value; } }
        public string ToAlternateContactPhone { get { return delivery.AlternateContactPhone; } set { delivery.AlternateContactPhone = value; } }
        public string ToAttention { get { return delivery.Attention; } set { delivery.Attention = value; } }
        public string ToAddress1 { get { return delivery.Address1; } set { delivery.Address1 = value; } }
        public string ToAddress2 { get { return delivery.Address2; } set { delivery.Address2 = value; } }
        public string ToCity { get { return delivery.City; } set { delivery.City = value; } }
        public string ToState { get { return delivery.State; } set { delivery.State = value; } }
        public string ToZipCode { get { return delivery.ZipCode; } set { delivery.ZipCode = value; } }
        public string ToCountryId { get { return delivery.CountryId; } set { delivery.CountryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ToCountry { get; set; }
        public string ToContactFax { get { return delivery.ContactFax; } set { delivery.ContactFax = value; } }
        public string ToCrossStreets { get { return delivery.CrossStreets; } set { delivery.CrossStreets = value; } }

        public string DeliveryNotes { get { return delivery.DeliveryNotes; } set { delivery.DeliveryNotes = value; } }
        public string CarrierId { get { return delivery.CarrierId; } set { delivery.CarrierId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Carrier { get; set; }
        public string CarrierAccount { get { return delivery.CarrierAccount; } set { delivery.CarrierAccount = value; } }
        public string ShipViaId { get { return delivery.ShipViaId; } set { delivery.ShipViaId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string ShipVia { get; set; }
        public string InvoiceId { get { return delivery.InvoiceId; } set { delivery.InvoiceId = value; } }
        public string VendorInvoiceId { get { return delivery.VendorInvoiceId; } set { delivery.VendorInvoiceId = value; } }
        public decimal? EstimatedFreight { get { return delivery.EstimatedFreight; } set { delivery.EstimatedFreight = value; } }
        public decimal? FreightInvoiceAmount { get { return delivery.FreightInvoiceAmount; } set { delivery.FreightInvoiceAmount = value; } }
        public string ChargeType { get { return delivery.ChargeType; } set { delivery.ChargeType = value; } }
        public string FreightTrackingNumber { get { return delivery.FreightTrackingNumber; } set { delivery.FreightTrackingNumber = value; } }
        //public string Template { get { return delivery.Template; } set { delivery.Template = value; } }
        public bool? DropShip { get { return delivery.DropShip; } set { delivery.DropShip = value; } }
        public string PackageCode { get { return delivery.PackageCode; } set { delivery.PackageCode = value; } }
        public bool? BillPoFreightOnOrder { get { return delivery.BillPoFreightOnOrder; } set { delivery.BillPoFreightOnOrder = value; } }
        public string OnlineOrderNumber { get { return delivery.OnlineOrderNumber; } set { delivery.OnlineOrderNumber = value; } }
        public string OnlineOrderStatus { get { return delivery.OnlineOrderStatus; } set { delivery.OnlineOrderStatus = value; } }
        public string DateStamp { get { return delivery.DateStamp; } set { delivery.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;
            if (isValid)
            {
                PropertyInfo property = typeof(DeliveryLogic).GetProperty(nameof(DeliveryLogic.OnlineOrderStatus));
                string[] acceptableValues = { RwConstants.ONLINE_DELIVERY_STATUS_PARTIAL, RwConstants.ONLINE_DELIVERY_STATUS_COMPLETE };
                isValid = IsValidStringValue(property, acceptableValues, ref validateMsg);
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------
    }
}
