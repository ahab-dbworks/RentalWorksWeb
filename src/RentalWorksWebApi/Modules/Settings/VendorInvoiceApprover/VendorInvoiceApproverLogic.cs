using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.VendorInvoiceApprover
{
    public class VendorInvoiceApproverLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        VendorInvoiceApproverRecord vendorInvoiceApprover = new VendorInvoiceApproverRecord();
        VendorInvoiceApproverLoader vendorInvoiceApproverLoader = new VendorInvoiceApproverLoader();
        public VendorInvoiceApproverLogic()
        {
            dataRecords.Add(vendorInvoiceApprover);
            dataLoader = vendorInvoiceApproverLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string VendorInvoiceApproverId { get { return vendorInvoiceApprover.VendorInvoiceApproverId; } set { vendorInvoiceApprover.VendorInvoiceApproverId = value; } }
        public string LocationId { get { return vendorInvoiceApprover.LocationId; } set { vendorInvoiceApprover.LocationId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Location { get; set; }
        public string DepartmentId { get { return vendorInvoiceApprover.DepartmentId; } set { vendorInvoiceApprover.DepartmentId = value; } }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Department { get; set; }
        public string UsersId { get { return vendorInvoiceApprover.UsersId; } set { vendorInvoiceApprover.UsersId = value; } }
        [FwBusinessLogicField(isReadOnly: true, isRecordTitle: true)]
        public string Username { get; set; }
        public bool? Rental { get { return vendorInvoiceApprover.Rental; } set { vendorInvoiceApprover.Rental = value; } }
        public bool? Sales { get { return vendorInvoiceApprover.Sales; } set { vendorInvoiceApprover.Sales = value; } }
        public bool? Parts { get { return vendorInvoiceApprover.Parts; } set { vendorInvoiceApprover.Parts = value; } }
        public bool? Misc { get { return vendorInvoiceApprover.Misc; } set { vendorInvoiceApprover.Misc = value; } }
        public bool? Labor { get { return vendorInvoiceApprover.Labor; } set { vendorInvoiceApprover.Labor = value; } }
        public bool? Vehicle { get { return vendorInvoiceApprover.Vehicle; } set { vendorInvoiceApprover.Vehicle = value; } }
        public bool? SubRent { get { return vendorInvoiceApprover.SubRent; } set { vendorInvoiceApprover.SubRent = value; } }
        public bool? SubSale { get { return vendorInvoiceApprover.SubSale; } set { vendorInvoiceApprover.SubSale = value; } }
        public bool? Repair { get { return vendorInvoiceApprover.Repair; } set { vendorInvoiceApprover.Repair = value; } }
        public bool? SubMisc { get { return vendorInvoiceApprover.SubMisc; } set { vendorInvoiceApprover.SubMisc = value; } }
        public bool? SubLabor { get { return vendorInvoiceApprover.SubLabor; } set { vendorInvoiceApprover.SubLabor = value; } }
        public bool? SubVehicle { get { return vendorInvoiceApprover.SubVehicle; } set { vendorInvoiceApprover.SubVehicle = value; } }
        public string DateStamp { get { return vendorInvoiceApprover.DateStamp; } set { vendorInvoiceApprover.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}