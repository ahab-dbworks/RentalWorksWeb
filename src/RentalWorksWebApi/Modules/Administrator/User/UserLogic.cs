using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Administrator.User
{
    public class UserLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        UserRecord user = new UserRecord();
        UserLoader userLoader = new UserLoader();
        public UserLogic()
        {
            dataRecords.Add(user);
            dataLoader = userLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string UserId { get { return user.UserId; } set { user.UserId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string ContactId { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string DealId { get; set; }
        public string Name { get { return user.Name; } set { user.Name = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string UserName { get; set; }
        [FwBusinessLogicField(isRecordTitle: true, isReadOnly: true)]
        public string FullName { get; set; }

        public string FirstName { get { return user.FirstName; } set { user.FirstName = value; } }
        public string MiddleInitial { get { return user.MiddleInitial; } set { user.MiddleInitial = value; } }
        public string LastName { get { return user.LastName; } set { user.LastName = value; } }
        public string LoginName { get { return user.LoginName; } set { user.LoginName = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Password { get { return "?????????"; } }
        public string GroupId { get { return user.GroupId; } set { user.GroupId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string GroupName { get; set; }
        public string ScheduleColor { get { return user.ScheduleColor; } set { user.ScheduleColor = value; } }
        public string UserTitleId { get { return user.UserTitleId; } set { user.UserTitleId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string UserTitle { get; set; }
        public string Email { get { return user.Email; } set { user.Email = value; } }
        public string OfficeLocationId { get { return user.OfficeLocationId; } set { user.OfficeLocationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OfficeLocation { get; set; }
        public string WarehouseId { get { return user.WarehouseId; } set { user.WarehouseId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Warehouse { get; set; }
        public string Address1 { get { return user.Address1; } set { user.Address1 = value; } }
        public string Address2 { get { return user.Address2; } set { user.Address2 = value; } }
        public string City { get { return user.City; } set { user.City = value; } }
        public string State { get { return user.State; } set { user.State = value; } }
        public string ZipCode { get { return user.ZipCode; } set { user.ZipCode = value; } }
        public string CountryId { get { return user.CountryId; } set { user.CountryId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Country { get; set; }
        public string OfficePhone { get { return user.OfficePhone; } set { user.OfficePhone = value; } }
        public string OfficeExtension { get { return user.OfficeExtension; } set { user.OfficeExtension = value; } }
        public string Fax { get { return user.Fax; } set { user.Fax = value; } }
        public string DirectPhone { get { return user.DirectPhone; } set { user.DirectPhone = value; } }
        public string Pager { get { return user.Pager; } set { user.Pager = value; } }
        public string PagerPin { get { return user.PagerPin; } set { user.PagerPin = value; } }
        public string Cellular { get { return user.Cellular; } set { user.Cellular = value; } }
        public string HomePhone { get { return user.HomePhone; } set { user.HomePhone = value; } }

        public string DefaultDepartmentType { get { return user.DefaultDepartmentType; } set { user.DefaultDepartmentType = value; } }
        public string RentalDepartmentId { get { return user.RentalDepartmentId; } set { user.RentalDepartmentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string RentalDepartment { get; set; }
        public string SalesDepartmentId { get { return user.SalesDepartmentId; } set { user.SalesDepartmentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string SalesDepartment { get; set; }
        public string PartsDepartmentId { get { return user.PartsDepartmentId; } set { user.PartsDepartmentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PartsDepartment { get; set; }
        public string MiscDepartmentId { get { return user.MiscDepartmentId; } set { user.MiscDepartmentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string MiscDepartment { get; set; }
        public string LaborDepartmentId { get { return user.LaborDepartmentId; } set { user.LaborDepartmentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string LaborDepartment { get; set; }
        public string FacilityDepartmentId { get { return user.FacilityDepartmentId; } set { user.FacilityDepartmentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string FacilityDepartment { get; set; }
        public string TransportationDepartmentId { get { return user.TransportationDepartmentId; } set { user.TransportationDepartmentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string TransportationDepartment { get; set; }




        //public bool? Webaccess { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Webadministrator { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Webadministratortext { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Webpassword { get; set; }
        //public bool? Lockaccount { get { return user.Lockaccount; } set { user.Lockaccount = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Registered { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Registerdate { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Tmppassword { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Changepasswordatlogin { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Resetpassword { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string PrimarydepartmentId { get; set; }
        //public string RentaldepartmentId { get { return user.RentaldepartmentId; } set { user.RentaldepartmentId = value; } }
        //public string SalesdepartmentId { get { return user.SalesdepartmentId; } set { user.SalesdepartmentId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string RentalagentusersId { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string SalesagentusersId { get; set; }
        //public string PartsdepartmentId { get { return user.PartsdepartmentId; } set { user.PartsdepartmentId = value; } }
        //public string LabordepartmentId { get { return user.LabordepartmentId; } set { user.LabordepartmentId = value; } }
        //public string MiscdepartmentId { get { return user.MiscdepartmentId; } set { user.MiscdepartmentId = value; } }
        //public string SpacedepartmentId { get { return user.SpacedepartmentId; } set { user.SpacedepartmentId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Sourceid1 { get; set; }
        //public string Department { get { return user.Department; } set { user.Department = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string DepartmentId { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Webreports { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Webquoterequest { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string DefaultpoordertypeId { get; set; }
        //public string WebcatalogId { get { return user.WebcatalogId; } set { user.WebcatalogId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Iscrew { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Usertype { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string WebusersId { get; set; }
        public bool? Inactive { get { return user.Inactive; } set { user.Inactive = value; } }
        //------------------------------------------------------------------------------------ 
    }
}