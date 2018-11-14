using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.NumberFormat
{
    [FwLogic(Id:"lSSjVLCR9bnP")]
    public class NumberFormatLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        NumberFormatLoader nmberFormatLoader = new NumberFormatLoader();
        public NumberFormatLogic()
        {
            dataLoader = nmberFormatLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"cZj1qhBbOoKk", IsPrimaryKey:true, IsReadOnly:true)]
        public string NumberFormatId { get; set; }

        [FwLogicProperty(Id:"cZj1qhBbOoKk", IsRecordTitle:true, IsReadOnly:true)]
        public string NumberFormat { get; set; }

        [FwLogicProperty(Id:"hGAuv8NEgGbi", IsReadOnly:true)]
        public string Mask { get; set; }

        //------------------------------------------------------------------------------------ 
    }
}
