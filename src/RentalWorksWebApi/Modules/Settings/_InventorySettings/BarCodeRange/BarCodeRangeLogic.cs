using FwStandard.AppManager;
using WebApi.Logic;
namespace WebApi.Modules.Settings.BarCodeRange
{
    [FwLogic(Id:"8sHokpTjH77")]
    public class BarCodeRangeLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        BarCodeRangeRecord barCodeRange = new BarCodeRangeRecord();
        BarCodeRangeLoader barCodeRangeLoader = new BarCodeRangeLoader();
        public BarCodeRangeLogic()
        {
            dataRecords.Add(barCodeRange);
            dataLoader = barCodeRangeLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id:"cK8ZrQY5qjq", IsPrimaryKey:true)]
        public string BarCodeRangeId { get { return barCodeRange.BarCodeRangeId; } set { barCodeRange.BarCodeRangeId = value; } }

        [FwLogicProperty(Id:"Ku1Y6GJgNPj", IsRecordTitle:true, IsReadOnly:true)]
        public string Description { get; set; }

        [FwLogicProperty(Id:"aRFPM6E4Zwuv")]
        public string Prefix { get { return barCodeRange.Prefix; } set { barCodeRange.Prefix = value; } }

        [FwLogicProperty(Id:"GthdYz2rktLo")]
        public int? BarcodeFrom { get { return barCodeRange.BarcodeFrom; } set { barCodeRange.BarcodeFrom = value; } }

        [FwLogicProperty(Id:"uto5XsCTGvf6")]
        public int? BarcodeTo { get { return barCodeRange.BarcodeTo; } set { barCodeRange.BarcodeTo = value; } }

        [FwLogicProperty(Id:"SvphU1NrQ9P2")]
        public string DateStamp { get { return barCodeRange.DateStamp; } set { barCodeRange.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
    }
}
