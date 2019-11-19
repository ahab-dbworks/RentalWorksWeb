using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.PoSettings.VendorInvoiceApprover
{
    [FwLogic(Id:"ygHVXqCX1oRho")]
    public class VendorInvoiceApproverLogic : AppBusinessLogic
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
        [FwLogicProperty(Id:"DSRSBYGffEDBO", IsPrimaryKey:true)]
        public string VendorInvoiceApproverId { get { return vendorInvoiceApprover.VendorInvoiceApproverId; } set { vendorInvoiceApprover.VendorInvoiceApproverId = value; } }

        [FwLogicProperty(Id:"BPQQlXz4idAT")]
        public string LocationId { get { return vendorInvoiceApprover.LocationId; } set { vendorInvoiceApprover.LocationId = value; } }

        [FwLogicProperty(Id:"hN2QwJn6KXKDv", IsReadOnly:true)]
        public string Location { get; set; }

        [FwLogicProperty(Id:"urf7tzrX1Cre")]
        public string DepartmentId { get { return vendorInvoiceApprover.DepartmentId; } set { vendorInvoiceApprover.DepartmentId = value; } }

        [FwLogicProperty(Id:"R5VKtPhJuOpEA", IsReadOnly:true)]
        public string Department { get; set; }

        [FwLogicProperty(Id:"ErZ2CtuTkuKA")]
        public string UsersId { get { return vendorInvoiceApprover.UsersId; } set { vendorInvoiceApprover.UsersId = value; } }

        [FwLogicProperty(Id:"xDgS3eTmGFVcZ", IsRecordTitle:true, IsReadOnly:true)]
        public string Username { get; set; }

        [FwLogicProperty(Id:"NtAdXLKoniqb")]
        public bool? Rental { get { return vendorInvoiceApprover.Rental; } set { vendorInvoiceApprover.Rental = value; } }

        [FwLogicProperty(Id:"wovtiDc80c4p")]
        public bool? Sales { get { return vendorInvoiceApprover.Sales; } set { vendorInvoiceApprover.Sales = value; } }

        [FwLogicProperty(Id:"PqpuFQqqk6gD")]
        public bool? Parts { get { return vendorInvoiceApprover.Parts; } set { vendorInvoiceApprover.Parts = value; } }

        [FwLogicProperty(Id:"mXFUOryQI4GY")]
        public bool? Misc { get { return vendorInvoiceApprover.Misc; } set { vendorInvoiceApprover.Misc = value; } }

        [FwLogicProperty(Id:"ef06qyPGrfQS")]
        public bool? Labor { get { return vendorInvoiceApprover.Labor; } set { vendorInvoiceApprover.Labor = value; } }

        [FwLogicProperty(Id:"ejAPlTfK7f6a")]
        public bool? Vehicle { get { return vendorInvoiceApprover.Vehicle; } set { vendorInvoiceApprover.Vehicle = value; } }

        [FwLogicProperty(Id:"wzFTUQHH3eBQ")]
        public bool? SubRent { get { return vendorInvoiceApprover.SubRent; } set { vendorInvoiceApprover.SubRent = value; } }

        [FwLogicProperty(Id:"os6rqhM9bg4X")]
        public bool? SubSale { get { return vendorInvoiceApprover.SubSale; } set { vendorInvoiceApprover.SubSale = value; } }

        [FwLogicProperty(Id:"L83jUrM9Ejpj")]
        public bool? Repair { get { return vendorInvoiceApprover.Repair; } set { vendorInvoiceApprover.Repair = value; } }

        [FwLogicProperty(Id:"8tnpyigswnXq")]
        public bool? SubMisc { get { return vendorInvoiceApprover.SubMisc; } set { vendorInvoiceApprover.SubMisc = value; } }

        [FwLogicProperty(Id:"KXniyoKdShXV")]
        public bool? SubLabor { get { return vendorInvoiceApprover.SubLabor; } set { vendorInvoiceApprover.SubLabor = value; } }

        [FwLogicProperty(Id:"wO9a4yJInQ1Y")]
        public bool? SubVehicle { get { return vendorInvoiceApprover.SubVehicle; } set { vendorInvoiceApprover.SubVehicle = value; } }

        [FwLogicProperty(Id:"OXp3tGAkZkq2")]
        public string DateStamp { get { return vendorInvoiceApprover.DateStamp; } set { vendorInvoiceApprover.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
