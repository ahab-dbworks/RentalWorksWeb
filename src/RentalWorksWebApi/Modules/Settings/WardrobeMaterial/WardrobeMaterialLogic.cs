using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.WardrobeMaterial
{
    public class WardrobeMaterialLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WardrobeMaterialRecord wardrobeMaterial = new WardrobeMaterialRecord();
        public WardrobeMaterialLogic()
        {
            dataRecords.Add(wardrobeMaterial);
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string WardrobeMaterialId { get { return wardrobeMaterial.WardrobeMaterialId; } set { wardrobeMaterial.WardrobeMaterialId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string WardrobeMaterial { get { return wardrobeMaterial.WardrobeMaterial; } set { wardrobeMaterial.WardrobeMaterial = value; } }
        public bool? Inactive { get { return wardrobeMaterial.Inactive; } set { wardrobeMaterial.Inactive = value; } }
        public string DateStamp { get { return wardrobeMaterial.DateStamp; } set { wardrobeMaterial.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}