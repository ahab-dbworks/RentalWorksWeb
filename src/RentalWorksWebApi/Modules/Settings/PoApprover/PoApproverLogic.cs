using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.PoApprover
{
    [FwLogic(Id:"tEa2pO7Q7ipu")]
    public class PoApproverLogic : AppBusinessLogic
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
        [FwLogicProperty(Id:"QNyadJv8lcrQ", IsPrimaryKey:true)]
        public string PoApproverId { get { return poApprover.PoApproverId; } set { poApprover.PoApproverId = value; } }

        [FwLogicProperty(Id:"ovLG94QJ1kI9")]
        public string LocationId { get { return poApprover.LocationId; } set { poApprover.LocationId = value; } }

        [FwLogicProperty(Id:"BQ6BhhbNoN7h", IsReadOnly:true)]
        public string Location { get; set; }

        [FwLogicProperty(Id:"X2LjqPXfg6BF")]
        public string DepartmentId { get { return poApprover.DepartmentId; } set { poApprover.DepartmentId = value; } }

        [FwLogicProperty(Id:"d2dR8WplUpUT", IsReadOnly:true)]
        public string Department { get; set; }

        [FwLogicProperty(Id:"xKmP8EMCZ5QI")]
        public string ProjectId { get { return poApprover.ProjectId; } set { poApprover.ProjectId = value; } }

        [FwLogicProperty(Id:"v0SiWBTMzY8F")]
        public string UsersId { get { return poApprover.UsersId; } set { poApprover.UsersId = value; } }

        [FwLogicProperty(Id:"jYcqhieSyzZL", IsRecordTitle:true, IsReadOnly:true)]
        public string UserName { get; set; }

        [FwLogicProperty(Id:"6AXTv8iGh8e2")]
        public string AppRoleId { get { return poApprover.AppRoleId; } set { poApprover.AppRoleId = value; } }

        [FwLogicProperty(Id:"eNhGtF8GIhHb", IsReadOnly:true)]
        public string AppRole { get; set; }

        [FwLogicProperty(Id:"hLf5cenTPE6h", IsReadOnly:true)]
        public string PoApproverType { get; set; }

        [FwLogicProperty(Id:"0lfo4PYStrZu")]
        public bool? IsBackup { get { return poApprover.IsBackup; } set { poApprover.IsBackup = value; } }

        [FwLogicProperty(Id:"rE13BoZuyDen")]
        public bool? HasLimit { get { return poApprover.HasLimit; } set { poApprover.HasLimit = value; } }

        [FwLogicProperty(Id:"c1O1YKuUb3eV")]
        public decimal? LimitRental { get { return poApprover.LimitRental; } set { poApprover.LimitRental = value; } }

        [FwLogicProperty(Id:"5PBYKfnEVAPL")]
        public decimal? LimitSales { get { return poApprover.LimitSales; } set { poApprover.LimitSales = value; } }

        [FwLogicProperty(Id:"oXMUFM9Dcrpx")]
        public decimal? LimitParts { get { return poApprover.LimitParts; } set { poApprover.LimitParts = value; } }

        [FwLogicProperty(Id:"1CFFwrakCKwP")]
        public decimal? LimitVehicle { get { return poApprover.LimitVehicle; } set { poApprover.LimitVehicle = value; } }

        [FwLogicProperty(Id:"RLtls05Vn5US")]
        public decimal? LimitMisc { get { return poApprover.LimitMisc; } set { poApprover.LimitMisc = value; } }

        [FwLogicProperty(Id:"UWizAkQX2xaG")]
        public decimal? LimitLabor { get { return poApprover.LimitLabor; } set { poApprover.LimitLabor = value; } }

        [FwLogicProperty(Id:"DEg57TgFpv5f")]
        public decimal? LimitSubRent { get { return poApprover.LimitSubRent; } set { poApprover.LimitSubRent = value; } }

        [FwLogicProperty(Id:"j6W2JHvZYz3C")]
        public decimal? LimitSubSale { get { return poApprover.LimitSubSale; } set { poApprover.LimitSubSale = value; } }

        [FwLogicProperty(Id:"ZMh4OfuYKApg")]
        public decimal? LimitSubMisc { get { return poApprover.LimitSubMisc; } set { poApprover.LimitSubMisc = value; } }

        [FwLogicProperty(Id:"5Y94yPqGmYcO")]
        public decimal? LimitSubLabor { get { return poApprover.LimitSubLabor; } set { poApprover.LimitSubLabor = value; } }

        [FwLogicProperty(Id:"fqoQmKFhG4q2")]
        public decimal? LimitSubVehicle { get { return poApprover.LimitSubVehicle; } set { poApprover.LimitSubVehicle = value; } }

        [FwLogicProperty(Id:"kV7z7XAFhPYu")]
        public decimal? LimitRepair { get { return poApprover.LimitRepair; } set { poApprover.LimitRepair = value; } }

        [FwLogicProperty(Id:"ZDz4yq6Qu0R2")]
        public string DateStamp { get { return poApprover.DateStamp; } set { poApprover.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
