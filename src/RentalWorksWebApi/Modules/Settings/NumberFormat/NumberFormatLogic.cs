using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.NumberFormat
{
    public class NumberFormatLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        NumberFormatLoader nmberFormatLoader = new NumberFormatLoader();
        public NumberFormatLogic()
        {
            dataLoader = nmberFormatLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isReadOnly: true, isPrimaryKey: true)]
        public string NumberFormat { get; set; }
        [FwBusinessLogicField(isReadOnly: true)]
        public string Mask { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}