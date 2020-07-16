using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Settings.DepartmentInventoryType
{
    [FwLogic(Id: "tHwJRd4Nmw19c")]
    public class DepartmentInventoryTypeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        DepartmentInventoryTypeRecord departmentInventoryType = new DepartmentInventoryTypeRecord();
        DepartmentInventoryTypeLoader departmentInventoryTypeLoader = new DepartmentInventoryTypeLoader();
        public DepartmentInventoryTypeLogic()
        {
            dataRecords.Add(departmentInventoryType);
            dataLoader = departmentInventoryTypeLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "THzriPwaHstPO", IsPrimaryKey: true)]
        public int? Id { get { return departmentInventoryType.Id; } set { departmentInventoryType.Id = value; } }
        [FwLogicProperty(Id: "TIOAuddD6MQA1")]
        public string DepartmentId { get { return departmentInventoryType.DepartmentId; } set { departmentInventoryType.DepartmentId = value; } }
        [FwLogicProperty(Id: "TJbkBF10tOSjz")]
        public string InventoryTypeId { get { return departmentInventoryType.InventoryTypeId; } set { departmentInventoryType.InventoryTypeId = value; } }
        [FwLogicProperty(Id: "TJFaM6i9u1Frs", IsReadOnly: true)]
        public string InventoryType { get; set; }
        [FwLogicProperty(Id: "tJHD6Fp974iwR", IsReadOnly: true)]
        public bool? IsInventory { get; set; }
        [FwLogicProperty(Id: "TjMGSohvuyzFT", IsReadOnly: true)]
        public bool? IsRate { get; set; }
        [FwLogicProperty(Id: "TjNEj3AqMweto", IsReadOnly: true)]
        public bool? IsFacilities { get; set; }
        [FwLogicProperty(Id: "TjSP8L6aTXiqx", IsReadOnly: true)]
        public int? OrderBy { get; set; }
        [FwLogicProperty(Id: "TK7wAU5adZeJZ")]
        public string DateStamp { get { return departmentInventoryType.DateStamp; } set { departmentInventoryType.DateStamp = value; } }
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
