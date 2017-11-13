using FwStandard.BusinessLogic.Attributes;
using Newtonsoft.Json;
using RentalWorksWebApi.Logic;
using RentalWorksWebApi.Modules.Home.Contact;

namespace RentalWorksWebApi.Modules.Settings.Crew
{
    public class CrewLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ContactRecord crew = new ContactRecord();
        CrewLoader crewLoader = new CrewLoader();
        public CrewLogic()
        {
            dataRecords.Add(crew);
            dataLoader = crewLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string CrewId { get { return crew.ContactId; } set { crew.ContactId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string UsersId { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool IsUser { get; set; }
        public string Salutation { get { return crew.Salutation; } set { crew.Salutation = value; } }
        public string NameFirstMiddleLast { get { return crew.NameFirstMiddleLast; } set { crew.NameFirstMiddleLast = value; } }
        public string Person { get { return crew.Person; } set { crew.Person = value; } }
        public string LastName { get { return crew.LastName; } set { crew.LastName = value; } }
        public string FirstName { get { return crew.FirstName; } set { crew.FirstName = value; } }
        public string Address1 { get { return crew.Address1; } set { crew.Address1 = value; } }
        public string Address2 { get { return crew.Address2; } set { crew.Address2 = value; } }
        public string City { get { return crew.City; } set { crew.City = value; } }
        public string State { get { return crew.State; } set { crew.State = value; } }
        public string CountryId { get { return crew.CountryId; } set { crew.CountryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Country { get; set; }
        public string ZipCode { get { return crew.ZipCode; } set { crew.ZipCode = value; } }
        public string MiddleInitial { get { return crew.MiddleInitial; } set { crew.MiddleInitial = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Location { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Position { get; set; }
        public string MobilePhone { get { return crew.MobilePhone; } set { crew.MobilePhone = value; } }
        public string Email { get { return crew.Email; } set { crew.Email = value; } }
        public string HomePhone { get { return crew.HomePhone; } set { crew.HomePhone = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public bool ContractEmployee { get; set; }
        [JsonIgnore]
        public string ContactRecordType { get { return crew.ContactRecordType; } set { crew.ContactRecordType = value; } }
        public bool Inactive { get { return crew.Inactive; } set { crew.Inactive = value; } }
        public string DateStamp { get { return crew.DateStamp; } set { crew.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
        public override void BeforeSave()
        {
            base.BeforeSave();
            ContactRecordType = "CREW";
        }
    }
}