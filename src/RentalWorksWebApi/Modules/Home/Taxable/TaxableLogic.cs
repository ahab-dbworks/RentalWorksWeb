using FwStandard.BusinessLogic.Attributes;
using System.Reflection;
using WebApi.Logic;
namespace WebApi.Modules.Home.Taxable
{
    public class TaxableLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        TaxableLoader taxableLoader = new TaxableLoader();
        public TaxableLogic()
        {
            dataLoader = taxableLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string MasterId { get; set; }
        public string LocationId { get; set; }
        public string Location { get; set; }
        public bool? Taxable { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}