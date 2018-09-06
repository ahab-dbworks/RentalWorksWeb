using FwStandard.BusinessLogic.Attributes;
using WebApi.Logic;
namespace WebApi.Modules.Reports.CrewSignInReport

{
    public class CrewSignInReportLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        CrewSignInReportLoader crewSignInReportLoader = new CrewSignInReportLoader();
        public CrewSignInReportLogic()
        {
            dataLoader = crewSignInReportLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string RowType { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public int? RecCount { get; set; } = 1;
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderId { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string LocationId { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string MasterId { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string DepartmentId { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderLocation { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string Location { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string Department { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string RentFromDate { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string RentToDate { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string RentFromTime { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string RentToTime { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderNo { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string OrderDescription { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string DealId { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string DealNo { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string Deal { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string Position { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string EmployeeId { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string Person { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true)]
        public string CrewContactId { get; set; } = string.Empty;
        //------------------------------------------------------------------------------------ 


        
    }
}
