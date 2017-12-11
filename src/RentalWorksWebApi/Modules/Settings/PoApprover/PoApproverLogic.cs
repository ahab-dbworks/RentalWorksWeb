using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.PoApprover
{
    public class PoApproverLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        PoApproverRecord poApprover = new PoApproverRecord();
        PoApproverLoader poApproverLoader = new PoApproverLoader();
        public PoApproverLogic()
        {
            dataRecords.Add(poApprover);
            dataLoader = poApproverLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string PoApproverId { get { return poApprover.PoApproverId; } set { poApprover.PoApproverId = value; } }
        public string LocationId { get { return poApprover.LocationId; } set { poApprover.LocationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Location { get; set; }
        public string DepartmentId { get { return poApprover.DepartmentId; } set { poApprover.DepartmentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Department { get; set; }
        public string ProjectId { get { return poApprover.ProjectId; } set { poApprover.ProjectId = value; } }
        public string UsersId { get { return poApprover.UsersId; } set { poApprover.UsersId = value; } }
        [FwBusinessLogicField(isReadOnly: true, isRecordTitle: true)]
        public string UserName { get; set; }
        public string AppRoleId { get { return poApprover.AppRoleId; } set { poApprover.AppRoleId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string AppRole { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PoApproverType { get; set; }
        public bool? IsBackup { get { return poApprover.IsBackup; } set { poApprover.IsBackup = value; } }
        public bool? HasLimit { get { return poApprover.HasLimit; } set { poApprover.HasLimit = value; } }
        public decimal? LimitRental { get { return poApprover.LimitRental; } set { poApprover.LimitRental = value; } }
        public decimal? LimitSales { get { return poApprover.LimitSales; } set { poApprover.LimitSales = value; } }
        public decimal? LimitParts { get { return poApprover.LimitParts; } set { poApprover.LimitParts = value; } }
        public decimal? LimitVehicle { get { return poApprover.LimitVehicle; } set { poApprover.LimitVehicle = value; } }
        public decimal? LimitMisc { get { return poApprover.LimitMisc; } set { poApprover.LimitMisc = value; } }
        public decimal? LimitLabor { get { return poApprover.LimitLabor; } set { poApprover.LimitLabor = value; } }
        public decimal? LimitSubRent { get { return poApprover.LimitSubRent; } set { poApprover.LimitSubRent = value; } }
        public decimal? LimitSubSale { get { return poApprover.LimitSubSale; } set { poApprover.LimitSubSale = value; } }
        public decimal? LimitSubMisc { get { return poApprover.LimitSubMisc; } set { poApprover.LimitSubMisc = value; } }
        public decimal? LimitSubLabor { get { return poApprover.LimitSubLabor; } set { poApprover.LimitSubLabor = value; } }
        public decimal? LimitSubVehicle { get { return poApprover.LimitSubVehicle; } set { poApprover.LimitSubVehicle = value; } }
        public decimal? LimitRepair { get { return poApprover.LimitRepair; } set { poApprover.LimitRepair = value; } }
        public string DateStamp { get { return poApprover.DateStamp; } set { poApprover.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}