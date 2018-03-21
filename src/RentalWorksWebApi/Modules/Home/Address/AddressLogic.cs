using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Home.Address
{
    public class AddressLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        AddressRecord address = new AddressRecord();
        AddressLoader addressLoader = new AddressLoader();
        public AddressLogic()
        {
            dataRecords.Add(address);
            dataLoader = addressLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string AddressId { get { return address.AddressId; } set { address.AddressId = value; } }
        public string Name { get { return address.Name; } set { address.Name = value; } }
        public string Attention { get { return address.Attention; } set { address.Attention = value; } }
        public string Attention2 { get { return address.Attention2; } set { address.Attention2 = value; } }
        public string Address1 { get { return address.Address1; } set { address.Address1 = value; } }
        public string Address2 { get { return address.Address2; } set { address.Address2 = value; } }
        public string City { get { return address.City; } set { address.City = value; } }
        public string State { get { return address.State; } set { address.State = value; } }
        public string CountryId { get { return address.CountryId; } set { address.CountryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Country { get; set; }
        public string ZipCode { get { return address.ZipCode; } set { address.ZipCode = value; } }
        public string Phone { get { return address.Phone; } set { address.Phone = value; } }
        public string Fax { get { return address.Fax; } set { address.Fax = value; } }
        public string Phone800 { get { return address.Phone800; } set { address.Phone800 = value; } }
        public string PhoneOther { get { return address.PhoneOther; } set { address.PhoneOther = value; } }
        public string UniqueId1 { get { return address.UniqueId1; } set { address.UniqueId1 = value; } }
        public string UniqueId2 { get { return address.UniqueId2; } set { address.UniqueId2 = value; } }
        public string UniqueId3 { get { return address.UniqueId3; } set { address.UniqueId3 = value; } }
        public string DateStamp { get { return address.DateStamp; } set { address.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}
