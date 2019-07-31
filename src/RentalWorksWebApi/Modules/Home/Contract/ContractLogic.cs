using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using FwStandard.SqlServer;
using WebApi.Logic;
using WebApi.Modules.Home.Delivery;

namespace WebApi.Modules.Home.Contract
{
    [FwLogic(Id: "EHxKFjXfUroP")]
    public class ContractLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ContractRecord contract = new ContractRecord();
        DeliveryRecord delivery = new DeliveryRecord();


        ContractLoader contractLoader = new ContractLoader();
        ContractBrowseLoader contractBrowseLoader = new ContractBrowseLoader();
        public ContractLogic()
        {
            dataRecords.Add(contract);
            dataRecords.Add(delivery);
            dataLoader = contractLoader;
            browseLoader = contractBrowseLoader;

            AfterSave += OnAfterSave;
            ForceSave = true;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "WON1VkgytZI8", IsPrimaryKey: true)]
        public string ContractId { get { return contract.ContractId; } set { contract.ContractId = value; } }

        [FwLogicProperty(Id: "sPqsdANcVxby", IsRecordTitle: true)]
        public string ContractNumber { get { return contract.ContractNumber; } set { contract.ContractNumber = value; } }

        [FwLogicProperty(Id: "8CCs4HaqLkdv")]
        public string ContractType { get { return contract.ContractType; } set { contract.ContractType = value; } }

        [FwLogicProperty(Id: "HD0vDJtf1yb0")]
        public string ContractDate { get { return contract.ContractDate; } set { contract.ContractDate = value; } }

        [FwLogicProperty(Id: "zfj1WvUia9Sz")]
        public string ContractTime { get { return contract.ContractTime; } set { contract.ContractTime = value; } }

        [FwLogicProperty(Id: "Woa2oSxOr91V")]
        public string LocationId { get { return contract.LocationId; } set { contract.LocationId = value; } }

        [FwLogicProperty(Id: "4HAv2CyRKKRy", IsReadOnly: true)]
        public string LocationCode { get; set; }

        [FwLogicProperty(Id: "4HAv2CyRKKRy", IsReadOnly: true)]
        public string Location { get; set; }

        [FwLogicProperty(Id: "j9Sh3vz9n07M")]
        public string WarehouseId { get { return contract.WarehouseId; } set { contract.WarehouseId = value; } }

        [FwLogicProperty(Id: "bne2ZRjKFwhT", IsReadOnly: true)]
        public string WarehouseCode { get; set; }

        [FwLogicProperty(Id: "bne2ZRjKFwhT", IsReadOnly: true)]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id: "0qFLtPV8K3nK", IsReadOnly: true)]
        public string CustomerId { get; set; }

        [FwLogicProperty(Id: "yEiu8kbykGAl")]
        public string DealId { get { return contract.DealId; } set { contract.DealId = value; } }

        [FwLogicProperty(Id: "sWr8J7HvRgRZ", IsReadOnly: true)]
        public string Deal { get; set; }

        [FwLogicProperty(Id: "icwdkXZuYp9v")]
        public string DepartmentId { get { return contract.DepartmentId; } set { contract.DepartmentId = value; } }

        [FwLogicProperty(Id: "B8wBpIJcoXQY", IsReadOnly: true)]
        public string Department { get; set; }

        [FwLogicProperty(Id: "CIhOMykCQJ0u", IsReadOnly: true)]
        public string PurchaseOrderId { get; set; }

        [FwLogicProperty(Id: "rClktjVOH3ly", IsReadOnly: true)]
        public string PurchaseOrderNumber { get; set; }

        [FwLogicProperty(Id: "iB3dKP1QeaMT", IsReadOnly: true)]
        public string RequisitionNumber { get; set; }

        [FwLogicProperty(Id: "bKlSBC97Q4qY", IsReadOnly: true)]
        public string VendorId { get; set; }

        [FwLogicProperty(Id: "bKlSBC97Q4qY", IsReadOnly: true)]
        public string Vendor { get; set; }

        [FwLogicProperty(Id: "FCu67vj1XIsA")]
        public bool? IsMigrated { get { return contract.IsMigrated; } set { contract.IsMigrated = value; } }

        [FwLogicProperty(Id: "4nBZnSSPPAPB")]
        public bool? NeedReconcile { get { return contract.NeedReconcile; } set { contract.NeedReconcile = value; } }

        [FwLogicProperty(Id: "ImFcBBWRLy2o")]
        public bool? PendingExchange { get { return contract.PendingExchange; } set { contract.PendingExchange = value; } }

        [FwLogicProperty(Id: "aZdsfF1nTHAy")]
        public string ExchangeContractId { get { return contract.ExchangeContractId; } set { contract.ExchangeContractId = value; } }

        [FwLogicProperty(Id: "VUdYKlRGNfeZ")]
        public bool? Rental { get { return contract.Rental; } set { contract.Rental = value; } }

        [FwLogicProperty(Id: "ems5FF5k8ez4")]
        public bool? Sales { get { return contract.Sales; } set { contract.Sales = value; } }

        [FwLogicProperty(Id: "j98nLoKdIU2Z", IsReadOnly: true)]
        public bool? Exchange { get; set; }

        [FwLogicProperty(Id: "8UM0lpArkPZJ")]
        public string InputByUserId { get { return contract.InputByUserId; } set { contract.InputByUserId = value; } }

        [FwLogicProperty(Id: "jRemMOly8JHJ", IsReadOnly: true)]
        public string InputByUser { get; set; }

        [FwLogicProperty(Id: "sWr8J7HvRgRZ", IsReadOnly: true)]
        public bool? DealInactive { get; set; }

        [FwLogicProperty(Id: "AaaWyLJXAAwK")]
        public bool? Truck { get { return contract.Truck; } set { contract.Truck = value; } }

        //[FwLogicProperty(Id: "8lD6XTb9EbO6")]
        //public string BillingDate { get { return contract.BillingDate; } set { contract.BillingDate = value; } }

        [FwLogicProperty(Id: "8lD6XTb9EbO6", IsReadOnly: true)]
        public string BillingDate { get; set; }

        [FwLogicProperty(Id: "E6hatAiI8fe18", IsReadOnly: true)]
        public string BillingDateChangeReason { get; set; }

        [FwLogicProperty(Id: "apwnKePW27b2", IsReadOnly: true)]
        public bool? BillingDateAdjusted { get; set; }

        [FwLogicProperty(Id: "fQoqFYvaaesY", IsReadOnly: true)]
        public bool? HasVoId { get; set; }

        [FwLogicProperty(Id: "eb7KgfrVo0Or")]
        public string SessionId { get { return contract.SessionId; } set { contract.SessionId = value; } }

        [FwLogicProperty(Id: "Bz8PoIrzCKb3", IsReadOnly: true)]
        public string OrderDescription { get; set; }

        [FwLogicProperty(Id: "Bz8PoIrzCKb3", IsReadOnly: true)]
        public string PoOrderDescription { get; set; }

        [FwLogicProperty(Id: "MCBigdZOp7K0U")]
        public string DeliveryId { get { return contract.DeliveryId; } set { contract.DeliveryId = value; delivery.DeliveryId = value; } }

        [FwLogicProperty(Id: "Ljp8kvZM8ps2D")]
        public string DeliveryDeliveryType { get { return delivery.DeliveryType; } set { delivery.DeliveryType = value; } }

        [FwLogicProperty(Id: "4LPYj8Zhld2y9")]
        public string DeliveryRequiredDate { get { return delivery.RequiredDate; } set { delivery.RequiredDate = value; } }

        [FwLogicProperty(Id: "wfkhxXtfW1RRw")]
        public string DeliveryRequiredTime { get { return delivery.RequiredTime; } set { delivery.RequiredTime = value; } }

        [FwLogicProperty(Id: "jVDiT1ao7Q169")]
        public string DeliveryTargetShipDate { get { return delivery.TargetShipDate; } set { delivery.TargetShipDate = value; } }

        [FwLogicProperty(Id: "ZA1a7wgCn1rgG")]
        public string DeliveryTargetShipTime { get { return delivery.TargetShipTime; } set { delivery.TargetShipTime = value; } }

        [FwLogicProperty(Id: "5QplzThP7FOQO")]
        public string DeliveryDirection { get { return delivery.Direction; } set { delivery.Direction = value; } }

        [FwLogicProperty(Id: "0ZPZRD8yAm8gK")]
        public string DeliveryAddressType { get { return delivery.AddressType; } set { delivery.AddressType = value; } }

        [FwLogicProperty(Id: "Q7j8NKfbIHLAx")]
        public string DeliveryFromLocation { get { return delivery.FromLocation; } set { delivery.FromLocation = value; } }

        [FwLogicProperty(Id: "2SOffmd9Ojb8u")]
        public string DeliveryFromContact { get { return delivery.FromContact; } set { delivery.FromContact = value; } }

        [FwLogicProperty(Id: "KJk55dUdXjmvV")]
        public string DeliveryFromContactPhone { get { return delivery.FromContactPhone; } set { delivery.FromContactPhone = value; } }

        [FwLogicProperty(Id: "7DSh6OxicjkgC")]
        public string DeliveryFromAlternateContact { get { return delivery.FromAlternateContact; } set { delivery.FromAlternateContact = value; } }

        [FwLogicProperty(Id: "ITLRecY1Xeib6")]
        public string DeliveryFromAlternateContactPhone { get { return delivery.FromAlternateContactPhone; } set { delivery.FromAlternateContactPhone = value; } }

        [FwLogicProperty(Id: "3UCXfgwK4r5hr")]
        public string DeliveryFromAttention { get { return delivery.FromAttention; } set { delivery.FromAttention = value; } }

        [FwLogicProperty(Id: "UMxPKoGxc5OLk")]
        public string DeliveryFromAddress1 { get { return delivery.FromAddress1; } set { delivery.FromAddress1 = value; } }

        [FwLogicProperty(Id: "A5BiUReuw4lL2")]
        public string DeliveryFromAdd2ress { get { return delivery.FromAdd2ress; } set { delivery.FromAdd2ress = value; } }

        [FwLogicProperty(Id: "ICyYSjfx7Tu6C")]
        public string DeliveryFromCity { get { return delivery.FromCity; } set { delivery.FromCity = value; } }

        [FwLogicProperty(Id: "Uwi3BlNoPEq2h")]
        public string DeliveryFromState { get { return delivery.FromState; } set { delivery.FromState = value; } }

        [FwLogicProperty(Id: "C6P8OD8NZIZGX")]
        public string DeliveryFromZipCode { get { return delivery.FromZipCode; } set { delivery.FromZipCode = value; } }

        [FwLogicProperty(Id: "FgfEYDA4R3YsO", IsReadOnly: true)]
        public string DeliveryFromCountry { get; set; }

        [FwLogicProperty(Id: "BR1ThoPILAHC5")]
        public string DeliveryFromCountryId { get { return delivery.FromCountryId; } set { delivery.FromCountryId = value; } }

        [FwLogicProperty(Id: "8wNtnJr6pWOeU")]
        public string DeliveryFromCrossStreets { get { return delivery.FromCrossStreets; } set { delivery.FromCrossStreets = value; } }

        [FwLogicProperty(Id: "br0FXRJrHJpOJ")]
        public string DeliveryToLocation { get { return delivery.Location; } set { delivery.Location = value; } }

        [FwLogicProperty(Id: "IkdOwIB9t1H1S")]
        public string DeliveryToContact { get { return delivery.Contact; } set { delivery.Contact = value; } }

        [FwLogicProperty(Id: "5a5nEJ4Wm66u1")]
        public string DeliveryToContactPhone { get { return delivery.ContactPhone; } set { delivery.ContactPhone = value; } }

        [FwLogicProperty(Id: "Uy2VgoMBZF9WB")]
        public string DeliveryToAlternateContact { get { return delivery.AlternateContact; } set { delivery.AlternateContact = value; } }

        [FwLogicProperty(Id: "OgEc9CyMth2oL")]
        public string DeliveryToAlternateContactPhone { get { return delivery.AlternateContactPhone; } set { delivery.AlternateContactPhone = value; } }

        [FwLogicProperty(Id: "ZD9qwpP1w1Noj")]
        public string DeliveryToAttention { get { return delivery.Attention; } set { delivery.Attention = value; } }

        [FwLogicProperty(Id: "pUlAQNl1I2JX5")]
        public string DeliveryToAddress1 { get { return delivery.Address1; } set { delivery.Address1 = value; } }

        [FwLogicProperty(Id: "gHpQRSc36ND7q")]
        public string DeliveryToAddress2 { get { return delivery.Address2; } set { delivery.Address2 = value; } }

        [FwLogicProperty(Id: "KxrgTvZQWMAC9")]
        public string DeliveryToCity { get { return delivery.City; } set { delivery.City = value; } }

        [FwLogicProperty(Id: "mXMyn0gTTnvbp")]
        public string DeliveryToState { get { return delivery.State; } set { delivery.State = value; } }

        [FwLogicProperty(Id: "PsMa8PrnsFogR")]
        public string DeliveryToZipCode { get { return delivery.ZipCode; } set { delivery.ZipCode = value; } }

        [FwLogicProperty(Id: "QTUAXAXWecTCC")]
        public string DeliveryToCountryId { get { return delivery.CountryId; } set { delivery.CountryId = value; } }

        [FwLogicProperty(Id: "C2a98GoHRf7Wn", IsReadOnly: true)]
        public string DeliveryToCountry { get; set; }

        [FwLogicProperty(Id: "HKHdMO3JWVjVa")]
        public string DeliveryToContactFax { get { return delivery.ContactFax; } set { delivery.ContactFax = value; } }

        [FwLogicProperty(Id: "zJ0Jg3pVT4Y4d")]
        public string DeliveryToCrossStreets { get { return delivery.CrossStreets; } set { delivery.CrossStreets = value; } }

        [FwLogicProperty(Id: "MScavXDbsk0RS")]
        public string DeliveryDeliveryNotes { get { return delivery.DeliveryNotes; } set { delivery.DeliveryNotes = value; } }

        [FwLogicProperty(Id: "ftMG0J65mG8PY")]
        public string DeliveryCarrierId { get { return delivery.CarrierId; } set { delivery.CarrierId = value; } }

        [FwLogicProperty(Id: "tXE1ta9zHqbZs", IsReadOnly: true)]
        public string DeliveryCarrier { get; set; }

        [FwLogicProperty(Id: "sHI4zauuUWlx3")]
        public string DeliveryCarrierAccount { get { return delivery.CarrierAccount; } set { delivery.CarrierAccount = value; } }

        [FwLogicProperty(Id: "OLvRqfJALFFnT")]
        public string DeliveryShipViaId { get { return delivery.ShipViaId; } set { delivery.ShipViaId = value; } }

        [FwLogicProperty(Id: "s20oX8efet0qF", IsReadOnly: true)]
        public string DeliveryShipVia { get; set; }

        [FwLogicProperty(Id: "rS2DohfpH81Lz")]
        public string DeliveryInvoiceId { get { return delivery.InvoiceId; } set { delivery.InvoiceId = value; } }

        [FwLogicProperty(Id: "sZrP5GbrIPyD9")]
        public string DeliveryVendorInvoiceId { get { return delivery.VendorInvoiceId; } set { delivery.VendorInvoiceId = value; } }

        [FwLogicProperty(Id: "eepwvrVM6oIvm")]
        public decimal? DeliveryEstimatedFreight { get { return delivery.EstimatedFreight; } set { delivery.EstimatedFreight = value; } }

        [FwLogicProperty(Id: "T8yEsBicunuVl")]
        public decimal? DeliveryFreightInvoiceAmount { get { return delivery.FreightInvoiceAmount; } set { delivery.FreightInvoiceAmount = value; } }

        [FwLogicProperty(Id: "5BGTjEDc7tioT")]
        public string DeliveryChargeType { get { return delivery.ChargeType; } set { delivery.ChargeType = value; } }

        [FwLogicProperty(Id: "ks9a0eF5nSXJb")]
        public string DeliveryFreightTrackingNumber { get { return delivery.FreightTrackingNumber; } set { delivery.FreightTrackingNumber = value; } }

        [FwLogicProperty(Id: "4LKJeHPt0MGoc", IsReadOnly: true)]
        public string DeliveryFreightTrackingUrl { get; set; }

        [FwLogicProperty(Id: "yeQHa8Q45ABXz")]
        public bool? DeliveryDropShip { get { return delivery.DropShip; } set { delivery.DropShip = value; } }

        [FwLogicProperty(Id: "W8idfoZ5MlyY4")]
        public string DeliveryPackageCode { get { return delivery.PackageCode; } set { delivery.PackageCode = value; } }

        [FwLogicProperty(Id: "U5dqenWOAC300")]
        public bool? DeliveryBillPoFreightOnOrder { get { return delivery.BillPoFreightOnOrder; } set { delivery.BillPoFreightOnOrder = value; } }

        [FwLogicProperty(Id: "ouPHcn4coMfWt")]
        public string DeliveryOnlineOrderNumber { get { return delivery.OnlineOrderNumber; } set { delivery.OnlineOrderNumber = value; } }

        [FwLogicProperty(Id: "hLcQPDb8i2o9A")]
        public string DeliveryOnlineOrderStatus { get { return delivery.OnlineOrderStatus; } set { delivery.OnlineOrderStatus = value; } }

        [FwLogicProperty(Id: "nlru0JZhiNUkX")]
        public string DeliveryDateStamp { get { return delivery.DateStamp; } set { delivery.DateStamp = value; } }

        [FwLogicProperty(Id: "7lTe6d93tfl6")]
        public string DateStamp { get { return contract.DateStamp; } set { contract.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
        public void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
            if (e.SaveMode.Equals(TDataRecordSaveMode.smUpdate))
            {
                ContractLogic orig = (ContractLogic)e.Original;
                if (!BillingDate.Equals(orig.BillingDate))
                {
                    ChangeContractBillingDateRequest request = new ChangeContractBillingDateRequest();
                    request.ContractId = ContractId;
                    request.OldBillingDate = FwConvert.ToDateTime(orig.BillingDate);
                    request.NewBillingDate = FwConvert.ToDateTime(BillingDate);
                    request.Reason = BillingDateChangeReason;  //#jhtodo: make this required once the front-end is done
                    TSpStatusResponse response = ContractFunc.UpdateContractBillingDate(AppConfig, UserSession, request, e.SqlConnection).Result;
                }
            }
        }
        //------------------------------------------------------------------------------------ 
    }
}