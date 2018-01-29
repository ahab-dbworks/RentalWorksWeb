using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.WarehouseQuikLocateApprover
{
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string WarehouseQuikLocateApproverId { get { return warehouseQuikLocateApprover.WarehouseQuikLocateApproverId; } set { warehouseQuikLocateApprover.WarehouseQuikLocateApproverId = value; } }
        public string WarehouseId { get { return warehouseQuikLocateApprover.WarehouseId; } set { warehouseQuikLocateApprover.WarehouseId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Warehouse { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string WarehouseCode { get; set; }
        public string UsersId { get { return warehouseQuikLocateApprover.UsersId; } set { warehouseQuikLocateApprover.UsersId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string UserName { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string OfficePhone { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string PhoneExtension { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Cellular { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string DirectPhone { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Email { get; set; }
        public bool? SendEmail { get { return warehouseQuikLocateApprover.SendEmail; } set { warehouseQuikLocateApprover.SendEmail = value; } }
        public string DateStamp { get { return warehouseQuikLocateApprover.DateStamp; } set { warehouseQuikLocateApprover.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}