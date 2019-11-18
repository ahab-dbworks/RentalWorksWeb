using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Home.Address
{
    [FwLogic(Id:"1i72El2vJT")]
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
        [FwLogicProperty(Id:"nm8uU7cVxM3", IsPrimaryKey:true)]
        public string AddressId { get { return address.AddressId; } set { address.AddressId = value; } }

        [FwLogicProperty(Id:"Z1OgdSbgU6GSN")]
        public string Name { get { return address.Name; } set { address.Name = value; } }

        [FwLogicProperty(Id:"Wrm0DEA2C3o9C")]
        public string Attention { get { return address.Attention; } set { address.Attention = value; } }

        [FwLogicProperty(Id:"qIpOTITe4za36")]
        public string Attention2 { get { return address.Attention2; } set { address.Attention2 = value; } }

        [FwLogicProperty(Id:"BLjrOfnVJcVah")]
        public string Address1 { get { return address.Address1; } set { address.Address1 = value; } }

        [FwLogicProperty(Id:"f6tncsye0fZl1")]
        public string Address2 { get { return address.Address2; } set { address.Address2 = value; } }

        [FwLogicProperty(Id:"waRyJD4WIhCBv")]
        public string City { get { return address.City; } set { address.City = value; } }

        [FwLogicProperty(Id:"MLAZ7QotIAK0k")]
        public string State { get { return address.State; } set { address.State = value; } }

        [FwLogicProperty(Id:"roptzb3WvOuEV")]
        public string CountryId { get { return address.CountryId; } set { address.CountryId = value; } }

        [FwLogicProperty(Id:"Iyo9iEgFDtB", IsReadOnly:true)]
        public string Country { get; set; }

        [FwLogicProperty(Id:"thJsYGjn5hZQH")]
        public string ZipCode { get { return address.ZipCode; } set { address.ZipCode = value; } }

        [FwLogicProperty(Id:"6aIsIY97knicA")]
        public string Phone { get { return address.Phone; } set { address.Phone = value; } }

        [FwLogicProperty(Id:"lC7zqyBqc6kOF")]
        public string Fax { get { return address.Fax; } set { address.Fax = value; } }

        [FwLogicProperty(Id:"xW49uJtaIv94r")]
        public string PhoneTollFree { get { return address.PhoneTollFree; } set { address.PhoneTollFree = value; } }

        [FwLogicProperty(Id:"pjsfw92l7vuJc")]
        public string PhoneOther { get { return address.PhoneOther; } set { address.PhoneOther = value; } }

        [FwLogicProperty(Id:"TmItqqUdRfZ1T")]
        public string UniqueId1 { get { return address.UniqueId1; } set { address.UniqueId1 = value; } }

        [FwLogicProperty(Id:"4CWCM0DTkgXdG")]
        public string UniqueId2 { get { return address.UniqueId2; } set { address.UniqueId2 = value; } }

        [FwLogicProperty(Id:"URqd9Uof3Rnhy")]
        public string UniqueId3 { get { return address.UniqueId3; } set { address.UniqueId3 = value; } }

        [FwLogicProperty(Id:"zJVdNSh39iqCM")]
        public string DateStamp { get { return address.DateStamp; } set { address.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
