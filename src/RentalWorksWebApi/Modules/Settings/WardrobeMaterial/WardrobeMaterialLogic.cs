using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.WardrobeMaterial
{
    [FwLogic(Id:"HNVO8LUd28ca3")]
    public class WardrobeMaterialLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WardrobeMaterialRecord wardrobeMaterial = new WardrobeMaterialRecord();
        public WardrobeMaterialLogic()
        {
            dataRecords.Add(wardrobeMaterial);
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"eMbJIUGZuWcTT", IsPrimaryKey:true)]
        public string WardrobeMaterialId { get { return wardrobeMaterial.WardrobeMaterialId; } set { wardrobeMaterial.WardrobeMaterialId = value; } }

        [FwLogicProperty(Id:"eMbJIUGZuWcTT", IsRecordTitle:true)]
        public string WardrobeMaterial { get { return wardrobeMaterial.WardrobeMaterial; } set { wardrobeMaterial.WardrobeMaterial = value; } }

        [FwLogicProperty(Id:"9CUEYvgOnV")]
        public bool? Inactive { get { return wardrobeMaterial.Inactive; } set { wardrobeMaterial.Inactive = value; } }

        [FwLogicProperty(Id:"4nGXLklP26")]
        public string DateStamp { get { return wardrobeMaterial.DateStamp; } set { wardrobeMaterial.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
