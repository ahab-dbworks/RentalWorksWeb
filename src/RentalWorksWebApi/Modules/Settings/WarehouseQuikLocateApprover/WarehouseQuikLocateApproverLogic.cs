using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.WarehouseQuikLocateApprover
{
    [FwLogic(Id:"qy2NNcY3i5sJZ")]
    public class WarehouseQuikLocateApproverLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WarehouseQuikLocateApproverRecord warehouseQuikLocateApprover = new WarehouseQuikLocateApproverRecord();
        WarehouseQuikLocateApproverLoader warehouseQuikLocateApproverLoader = new WarehouseQuikLocateApproverLoader();
        public WarehouseQuikLocateApproverLogic()
        {
            dataRecords.Add(warehouseQuikLocateApprover);
            dataLoader = warehouseQuikLocateApproverLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"UVh4eE5ixK8v5", IsPrimaryKey:true)]
        public string WarehouseQuikLocateApproverId { get { return warehouseQuikLocateApprover.WarehouseQuikLocateApproverId; } set { warehouseQuikLocateApprover.WarehouseQuikLocateApproverId = value; } }

        [FwLogicProperty(Id:"27oyC4GCgv3")]
        public string WarehouseId { get { return warehouseQuikLocateApprover.WarehouseId; } set { warehouseQuikLocateApprover.WarehouseId = value; } }

        [FwLogicProperty(Id:"UVh4eE5ixK8v5", IsReadOnly:true)]
        public string Warehouse { get; set; }

        [FwLogicProperty(Id:"UVh4eE5ixK8v5", IsReadOnly:true)]
        public string WarehouseCode { get; set; }

        [FwLogicProperty(Id:"gYslKt7D4nl")]
        public string UsersId { get { return warehouseQuikLocateApprover.UsersId; } set { warehouseQuikLocateApprover.UsersId = value; } }

        [FwLogicProperty(Id:"y4HVOWfjLW1Yz", IsReadOnly:true)]
        public string UserName { get; set; }

        [FwLogicProperty(Id:"6BOStHvACcA8R", IsReadOnly:true)]
        public string OfficePhone { get; set; }

        [FwLogicProperty(Id:"t90OiQchwi6XC", IsReadOnly:true)]
        public string PhoneExtension { get; set; }

        [FwLogicProperty(Id:"ktgAhzprQxyqR", IsReadOnly:true)]
        public string Cellular { get; set; }

        [FwLogicProperty(Id:"CIrQgmKK1Pg7J", IsReadOnly:true)]
        public string DirectPhone { get; set; }

        [FwLogicProperty(Id:"zhpNFA3S8kT34", IsReadOnly:true)]
        public string Email { get; set; }

        [FwLogicProperty(Id:"Au6HyfQtW7h")]
        public bool? SendEmail { get { return warehouseQuikLocateApprover.SendEmail; } set { warehouseQuikLocateApprover.SendEmail = value; } }

        [FwLogicProperty(Id:"SBq4TZYWisv")]
        public string DateStamp { get { return warehouseQuikLocateApprover.DateStamp; } set { warehouseQuikLocateApprover.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
