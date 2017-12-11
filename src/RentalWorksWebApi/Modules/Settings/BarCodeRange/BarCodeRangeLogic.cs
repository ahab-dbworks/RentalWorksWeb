using FwStandard.BusinessLogic.Attributes; 
using WebApi.Logic;
namespace WebApi.Modules.Settings.BarCodeRange
{
    public class BarCodeRangeLogic : RwBusinessLogic
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
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string BarCodeRangeId { get { return barCodeRange.BarCodeRangeId; } set { barCodeRange.BarCodeRangeId = value; } }
        [FwBusinessLogicField(isReadOnly: true, isRecordTitle: true)]
        public string Description { get; set; }
        public string Prefix { get { return barCodeRange.Prefix; } set { barCodeRange.Prefix = value; } }
        public int? BarcodeFrom { get { return barCodeRange.BarcodeFrom; } set { barCodeRange.BarcodeFrom = value; } }
        public int? BarcodeTo { get { return barCodeRange.BarcodeTo; } set { barCodeRange.BarcodeTo = value; } }
        public string DateStamp { get { return barCodeRange.DateStamp; } set { barCodeRange.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
}