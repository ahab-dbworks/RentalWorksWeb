using FwStandard.BusinessLogic.Attributes;
using RentalWorksWebApi.Logic;

namespace RentalWorksWebApi.Modules.Settings.Vendor
{
    public class VendorLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------
        VendorRecord vendor = new VendorRecord();
        VendorLoader vendorLoader = new VendorLoader();
        public VendorLogic()
        {
            dataRecords.Add(vendor);
            dataLoader = vendorLoader;
        }
        //------------------------------------------------------------------------------------
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string VendorId { get { return vendor.VendorId; } set { vendor.VendorId = value; } }
        public string VendorNameType { get { return vendor.VendorNameType; } set { vendor.VendorNameType = value; } }
        public string VendorNumber { get { return vendor.VendorNumber; } set { vendor.VendorNumber = value; } }
        public string FederalIdNumber { get { return vendor.FederalIdNumber; } set { vendor.FederalIdNumber = value; } }
        public string OfficeLocationId { get { return vendor.OfficeLocationId; } set { vendor.OfficeLocationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OfficeLocation { get; set; }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string Vendor { get { return vendor.Vendor; } set { vendor.Vendor = value; } }
        public string Salutation { get { return vendor.Salutation; } set { vendor.Salutation = value; } }
        public string FirstName { get { return vendor.FirstName; } set { vendor.FirstName = value; } }
        public string MiddleInitial { get { return vendor.MiddleInitial; } set { vendor.MiddleInitial = value; } }
        public string LastName { get { return vendor.LastName; } set { vendor.LastName = value; } }
        public string Address1 { get { return vendor.Address1; } set { vendor.Address1 = value; } }
        public string Address2 { get { return vendor.Address2; } set { vendor.Address2 = value; } }
        public string City { get { return vendor.City; } set { vendor.City = value; } }
        public string State { get { return vendor.State; } set { vendor.State = value; } }
        public string CountryId { get { return vendor.CountryId; } set { vendor.CountryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Country { get; set; }
        public string ZipCode { get { return vendor.ZipCode; } set { vendor.ZipCode = value; } }
        public string RemitAddress1 { get { return vendor.RemitAddress1; } set { vendor.RemitAddress1 = value; } }
        public string RemitAddress2 { get { return vendor.RemitAddress2; } set { vendor.RemitAddress2 = value; } }
        public string RemitCity { get { return vendor.RemitCity; } set { vendor.RemitCity = value; } }
        public string RemitState { get { return vendor.RemitState; } set { vendor.RemitState = value; } }
        public string RemitCountryId { get { return vendor.RemitCountryId; } set { vendor.RemitCountryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RemitCountry { get; set; }
        public string RemitZipCode { get { return vendor.RemitZipCode; } set { vendor.RemitZipCode = value; } }
        public string Phone { get { return vendor.Phone; } set { vendor.Phone = value; } }
        public string Fax { get { return vendor.Fax; } set { vendor.Fax = value; } }
        public string Phone800 { get { return vendor.Phone800; } set { vendor.Phone800 = value; } }
        public string OtherPhone { get { return vendor.OtherPhone; } set { vendor.OtherPhone = value; } }
        public string PaymentTermsId { get { return vendor.PaymentTermsId; } set { vendor.PaymentTermsId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PaymentTerms { get; set; }
        public string WebAddress { get { return vendor.WebAddress; } set { vendor.WebAddress = value; } }
        public string Email { get { return vendor.Email; } set { vendor.Email = value; } }
        public bool Inactive { get { return vendor.Inactive; } set { vendor.Inactive = value; } }
        public string DateStamp { get { return vendor.DateStamp; } set { vendor.DateStamp = value; } }
        //------------------------------------------------------------------------------------
    }

}
