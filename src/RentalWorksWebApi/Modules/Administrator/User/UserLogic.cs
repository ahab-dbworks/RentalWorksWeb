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
        public string Fullname { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
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
        //public string Email { get { return user.Email; } set { user.Email = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Userloginname { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Userpassword { get; set; }
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
        //public string Titletype { get; set; }
        //public string Title { get { return user.Title; } set { user.Title = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Sourceid1 { get; set; }
        //public string GroupsId { get { return user.GroupsId; } set { user.GroupsId = value; } }
        //public string Department { get { return user.Department; } set { user.Department = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string DepartmentId { get; set; }
        //public string LocationId { get { return user.LocationId; } set { user.LocationId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Location { get; set; }
        //public string WarehouseId { get { return user.WarehouseId; } set { user.WarehouseId = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Warehouse { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Webreports { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public bool? Webquoterequest { get; set; }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string DefaultpoordertypeId { get; set; }
        //public string WebcatalogId { get { return user.WebcatalogId; } set { user.WebcatalogId = value; } }
        //public string Office { get { return user.Office; } set { user.Office = value; } }
        //public string Phoneextension { get { return user.Phoneextension; } set { user.Phoneextension = value; } }
        //public string Fax { get { return user.Fax; } set { user.Fax = value; } }
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