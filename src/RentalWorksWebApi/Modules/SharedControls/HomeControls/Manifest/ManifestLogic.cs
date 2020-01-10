using FwStandard.AppManager;
using WebApi.Logic;
using WebApi.Modules.Warehouse.Contract;

namespace WebApi.Modules.HomeControls.Manifest
{
    [FwLogic(Id: "6Roj1hKqs1bJL")]
    public class ManifestLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        ContractRecord manifest = new ContractRecord();
        ManifestLoader manifestLoader = new ManifestLoader();
        public ManifestLogic()
        {
            dataRecords.Add(manifest);
            dataLoader = manifestLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "IJ3ZnYDIoLPHP", IsPrimaryKey: true)]
        public string ManifestId { get { return manifest.ContractId; } set { manifest.ContractId = value; } }
        [FwLogicProperty(Id: "TLnYmoD4vbqt6", IsRecordTitle: true)]
        public string ManifestNumber { get { return manifest.ContractNumber; } set { manifest.ContractNumber = value; } }
        [FwLogicProperty(Id: "3UrCupiXKb1nl")]
        public string ManifestDate { get { return manifest.ContractDate; } set { manifest.ContractDate = value; } }
        [FwLogicProperty(Id: "qwkQ3Vy8jHAmf")]
        public string ContractType { get { return manifest.ContractType; } set { manifest.ContractType = value; } }
        [FwLogicProperty(Id: "jxhxoJZ9jp3mj")]
        public string DepartmentId { get { return manifest.DepartmentId; } set { manifest.DepartmentId = value; } }
        [FwLogicProperty(Id: "Tz6qJI9mmJClI", IsReadOnly: true)]
        public string Department { get; set; }
        [FwLogicProperty(Id: "gC5z7s1CtQC2n")]
        public string LocationId { get { return manifest.LocationId; } set { manifest.LocationId = value; } }
        [FwLogicProperty(Id: "4EDciAG7FxT", IsReadOnly: true)]
        public string Location { get; set; }
        [FwLogicProperty(Id: "YghUaoinLn2", IsReadOnly: true)]
        public string LocationCode { get; set; }
        [FwLogicProperty(Id: "sZUuZy8fgHI44", IsReadOnly: true)]
        public string NameFirstMiddleLast { get; set; }
        [FwLogicProperty(Id: "kb5LnRY7OYrtR", IsReadOnly: true)]
        public string TransferId { get; set; }
        [FwLogicProperty(Id: "1azRccTJi1CqG", IsReadOnly: true)]
        public string TransferNumber { get; set; }
        [FwLogicProperty(Id: "3lqNFmmJcSOc4", IsReadOnly: true)]
        public string TransferDescription { get; set; }
        [FwLogicProperty(Id: "FXEJGdQt135j", IsReadOnly: true)]
        public bool? HasVoid { get; set; }

        [FwLogicProperty(Id: "xxxxxxxxx")]
        public bool? Rental { get { return manifest.Rental; } set { manifest.Rental = value; } }

        [FwLogicProperty(Id: "xxxxxxxxx")]
        public bool? Sales { get { return manifest.Sales; } set { manifest.Sales = value; } }

        [FwLogicProperty(Id: "xxxxxxxxx", IsReadOnly: true)]
        public bool? Exchange { get { return manifest.Exchange; } set { manifest.Exchange = value; } }



        [FwLogicProperty(Id: "X4NvNTdyaAMP")]
        public string ManifestTime { get { return manifest.ContractTime; } set { manifest.ContractTime = value; } }
        [FwLogicProperty(Id: "pamwRfn1i1C")]
        public string WarehouseId { get { return manifest.WarehouseId; } set { manifest.WarehouseId = value; } }
        [FwLogicProperty(Id: "IIyVxZMgDRz", IsReadOnly: true)]
        public string Warehouse { get; set; }
        [FwLogicProperty(Id: "DFKQ1RIp8b2", IsReadOnly: true)]
        public string WarehouseCode { get; set; }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
        //------------------------------------------------------------------------------------ 
    }
}
