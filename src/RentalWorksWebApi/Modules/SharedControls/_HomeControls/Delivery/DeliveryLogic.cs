using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using System.Reflection;
using System.Text;
using WebApi.Logic;
using WebApi;

namespace WebApi.Modules.HomeControls.Delivery
{
    [FwLogic(Id:"SOiV5LvU6mHn")]
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
        [FwLogicProperty(Id:"F8OmKbDNG2Bu", IsPrimaryKey:true)]
        public string DeliveryId { get { return delivery.DeliveryId; } set { delivery.DeliveryId = value; } }

        [FwLogicProperty(Id:"xAsLHYYk0UMi")]
        public string OrderId { get { return delivery.OrderId; } set { delivery.OrderId = value; } }

        [FwLogicProperty(Id:"r2482K7u5LKi")]
        public string DeliveryType { get { return delivery.DeliveryType; } set { delivery.DeliveryType = value; } }

        [FwLogicProperty(Id:"ii0mEmx4Ic39")]
        public string RequiredDate { get { return delivery.RequiredDate; } set { delivery.RequiredDate = value; } }

        [FwLogicProperty(Id:"V49Uy0cAaZJF")]
        public string RequiredTime { get { return delivery.RequiredTime; } set { delivery.RequiredTime = value; } }

        [FwLogicProperty(Id:"nfgxBq3zHNZp")]
        public string TargetShipDate { get { return delivery.TargetShipDate; } set { delivery.TargetShipDate = value; } }

        [FwLogicProperty(Id:"cbx7DbSe5Tte")]
        public string TargetShipTime { get { return delivery.TargetShipTime; } set { delivery.TargetShipTime = value; } }

        [FwLogicProperty(Id:"diouzGsxaEIR")]
        public string Direction { get { return delivery.Direction; } set { delivery.Direction = value; } }

        [FwLogicProperty(Id:"h5Dn8PrqtAc9")]
        public string AddressType { get { return delivery.AddressType; } set { delivery.AddressType = value; } }


        [FwLogicProperty(Id:"dDnl00uIuIYt")]
        public string FromLocation { get { return delivery.FromLocation; } set { delivery.FromLocation = value; } }

        [FwLogicProperty(Id:"r8vQuHnB37bD")]
        public string FromContact { get { return delivery.FromContact; } set { delivery.FromContact = value; } }

        [FwLogicProperty(Id:"tz1qPRt2JTbR")]
        public string FromContactPhone { get { return delivery.FromContactPhone; } set { delivery.FromContactPhone = value; } }

        [FwLogicProperty(Id:"NUGj0dh1Fi5k")]
        public string FromAlternateContact { get { return delivery.FromAlternateContact; } set { delivery.FromAlternateContact = value; } }

        [FwLogicProperty(Id:"xRqwBQrYXJD7")]
        public string FromAlternateContactPhone { get { return delivery.FromAlternateContactPhone; } set { delivery.FromAlternateContactPhone = value; } }

        [FwLogicProperty(Id:"shqXXTrtn5XB")]
        public string FromAttention { get { return delivery.FromAttention; } set { delivery.FromAttention = value; } }

        [FwLogicProperty(Id:"EAbMMphQUlPk")]
        public string FromAddress1 { get { return delivery.FromAddress1; } set { delivery.FromAddress1 = value; } }

        [FwLogicProperty(Id:"jpKUkJmeEMoh")]
        public string FromAdd2ress { get { return delivery.FromAdd2ress; } set { delivery.FromAdd2ress = value; } }

        [FwLogicProperty(Id:"XXxqpYrTfXYh")]
        public string FromCity { get { return delivery.FromCity; } set { delivery.FromCity = value; } }

        [FwLogicProperty(Id:"PvumVOPLjLa9")]
        public string FromState { get { return delivery.FromState; } set { delivery.FromState = value; } }

        [FwLogicProperty(Id:"Kc21C87PhBv0")]
        public string FromZipCode { get { return delivery.FromZipCode; } set { delivery.FromZipCode = value; } }

        [FwLogicProperty(Id:"VyELYni22S7y", IsReadOnly:true)]
        public string FromCountry { get; set; }

        [FwLogicProperty(Id:"WTGWIbwsoBsY")]
        public string FromCountryId { get { return delivery.FromCountryId; } set { delivery.FromCountryId = value; } }

        [FwLogicProperty(Id:"Orw0oFAv2KSW")]
        public string FromCrossStreets { get { return delivery.FromCrossStreets; } set { delivery.FromCrossStreets = value; } }



        [FwLogicProperty(Id:"Cr1wJDVCQvO3")]
        public string ToLocation { get { return delivery.Location; } set { delivery.Location = value; } }

        [FwLogicProperty(Id:"cXMuDghpP2yk")]
        public string ToContact { get { return delivery.Contact; } set { delivery.Contact = value; } }

        [FwLogicProperty(Id:"sncFG7BkCLXL")]
        public string ToContactPhone { get { return delivery.ContactPhone; } set { delivery.ContactPhone = value; } }

        [FwLogicProperty(Id:"pedZZ4njdhjC")]
        public string ToAlternateContact { get { return delivery.AlternateContact; } set { delivery.AlternateContact = value; } }

        [FwLogicProperty(Id:"suITR7KjXX8f")]
        public string ToAlternateContactPhone { get { return delivery.AlternateContactPhone; } set { delivery.AlternateContactPhone = value; } }

        [FwLogicProperty(Id:"RDsowHW4901n")]
        public string ToAttention { get { return delivery.Attention; } set { delivery.Attention = value; } }

        [FwLogicProperty(Id:"7svK8TSpDXGf")]
        public string ToAddress1 { get { return delivery.Address1; } set { delivery.Address1 = value; } }

        [FwLogicProperty(Id:"6mrheEdVZvwP")]
        public string ToAddress2 { get { return delivery.Address2; } set { delivery.Address2 = value; } }

        [FwLogicProperty(Id:"byqcd18yplHA")]
        public string ToCity { get { return delivery.City; } set { delivery.City = value; } }

        [FwLogicProperty(Id:"Em3Wt5YMo1bZ")]
        public string ToState { get { return delivery.State; } set { delivery.State = value; } }

        [FwLogicProperty(Id:"F3vC63JLeCQz")]
        public string ToZipCode { get { return delivery.ZipCode; } set { delivery.ZipCode = value; } }

        [FwLogicProperty(Id:"XSxzddkDU1yn")]
        public string ToCountryId { get { return delivery.CountryId; } set { delivery.CountryId = value; } }

        [FwLogicProperty(Id:"SvWA7E6rOPNn", IsReadOnly:true)]
        public string ToCountry { get; set; }

        [FwLogicProperty(Id:"oHs2ugT5m0TO")]
        public string ToContactFax { get { return delivery.ContactFax; } set { delivery.ContactFax = value; } }

        [FwLogicProperty(Id:"oFFAwF9I8NKw")]
        public string ToCrossStreets { get { return delivery.CrossStreets; } set { delivery.CrossStreets = value; } }


        [FwLogicProperty(Id:"81XTgsySIPK3")]
        public string DeliveryNotes { get { return delivery.DeliveryNotes; } set { delivery.DeliveryNotes = value; } }

        [FwLogicProperty(Id:"MplOAr8ebID1")]
        public string CarrierId { get { return delivery.CarrierId; } set { delivery.CarrierId = value; } }

        [FwLogicProperty(Id:"MlwbRF7o8tZj", IsReadOnly:true)]
        public string Carrier { get; set; }

        [FwLogicProperty(Id:"yTYV1DURzpNL")]
        public string CarrierAccount { get { return delivery.CarrierAccount; } set { delivery.CarrierAccount = value; } }

        [FwLogicProperty(Id:"Vjb5TvMQqwIP")]
        public string ShipViaId { get { return delivery.ShipViaId; } set { delivery.ShipViaId = value; } }

        [FwLogicProperty(Id:"7iArsxfWFFIb", IsReadOnly:true)]
        public string ShipVia { get; set; }

        [FwLogicProperty(Id:"blQwCHcUON8C")]
        public string InvoiceId { get { return delivery.InvoiceId; } set { delivery.InvoiceId = value; } }

        [FwLogicProperty(Id:"hmOTELFE4BCh")]
        public string VendorInvoiceId { get { return delivery.VendorInvoiceId; } set { delivery.VendorInvoiceId = value; } }

        [FwLogicProperty(Id:"XhAKoWr9PQz6")]
        public decimal? EstimatedFreight { get { return delivery.EstimatedFreight; } set { delivery.EstimatedFreight = value; } }

        [FwLogicProperty(Id:"1IpKY3xmR1Zp")]
        public decimal? FreightInvoiceAmount { get { return delivery.FreightInvoiceAmount; } set { delivery.FreightInvoiceAmount = value; } }

        [FwLogicProperty(Id:"yrHPFt18HRRg")]
        public string ChargeType { get { return delivery.ChargeType; } set { delivery.ChargeType = value; } }

        [FwLogicProperty(Id:"awynsaNpbUQE")]
        public string FreightTrackingNumber { get { return delivery.FreightTrackingNumber; } set { delivery.FreightTrackingNumber = value; } }

        //[FwLogicProperty(Id:"cpN6MyzK3CFP")]
        //public string Template { get { return delivery.Template; } set { delivery.Template = value; } }

        [FwLogicProperty(Id:"25wJTPOceG99")]
        public bool? DropShip { get { return delivery.DropShip; } set { delivery.DropShip = value; } }

        [FwLogicProperty(Id:"NgOMHbXYypJu")]
        public string PackageCode { get { return delivery.PackageCode; } set { delivery.PackageCode = value; } }

        [FwLogicProperty(Id:"bZKIaqB93UuA")]
        public bool? BillPoFreightOnOrder { get { return delivery.BillPoFreightOnOrder; } set { delivery.BillPoFreightOnOrder = value; } }

        [FwLogicProperty(Id:"1Wgi7mhM6v2U")]
        public string OnlineOrderNumber { get { return delivery.OnlineOrderNumber; } set { delivery.OnlineOrderNumber = value; } }

        [FwLogicProperty(Id:"N0ZM3IGQqRl7")]
        public string OnlineOrderStatus { get { return delivery.OnlineOrderStatus; } set { delivery.OnlineOrderStatus = value; } }

        [FwLogicProperty(Id:"nOVdE5rY0c5N")]
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
