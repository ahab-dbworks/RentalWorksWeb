using WebApi.Logic;
using FwStandard.AppManager;
using Newtonsoft.Json;
using FwStandard.BusinessLogic;

namespace WebApi.Modules.HomeControls.GlManual
{
    [FwLogic(Id: "04gQwzMY1sBKn")]
    public class GlManualLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        GlManualRecord glManual = new GlManualRecord();
        GlManualLoader glManualLoader = new GlManualLoader();
        public GlManualLogic()
        {
            dataRecords.Add(glManual);
            dataLoader = glManualLoader;
            IsManual = true;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "04hega34m9SOW", IsPrimaryKey: true)]
        public int? Id { get { return glManual.Id; } set { glManual.Id = value; } }
        [FwLogicProperty(Id: "05QoL9ClWAvox", IsPrimaryKey: true, IsPrimaryKeyOptional: true)]
        public string InternalChar { get { return glManual.InternalChar; } set { glManual.InternalChar = value; } }
        [FwLogicProperty(Id: "06D4oXfmCN7kz")]
        public string OfficeLocationId { get { return glManual.OfficeLocationId; } set { glManual.OfficeLocationId = value; } }
        [FwLogicProperty(Id: "06DrKXjWDW7M3", IsReadOnly: true)]
        public string OfficeLocation { get; set; }
        [FwLogicProperty(Id: "075h4ShAwRjVw")]
        public string InvoiceId { get { return glManual.InvoiceId; } set { glManual.InvoiceId = value; } }
        [FwLogicProperty(Id: "07rBpdnqDKz0Z")]
        public string ReceiptId { get { return glManual.ReceiptId ; } set { glManual.ReceiptId = value; } }
        [FwLogicProperty(Id: "084gYxl6EgoR6")]
        public string VendorInvoiceId { get { return glManual.VendorInvoiceId; } set { glManual.VendorInvoiceId = value; } }
        [FwLogicProperty(Id: "08PAO3ASIgU9p")]
        public string PaymentId { get { return glManual.PaymentId; } set { glManual.PaymentId = value; } }
        [FwLogicProperty(Id: "08ZXXv1udumag")]
        public string GlDate { get { return glManual.GlDate; } set { glManual.GlDate = value; } }
        [FwLogicProperty(Id: "09iGiNYBhcWCs")]
        public string DebitGlAccountId { get { return glManual.DebitGlAccountId; } set { glManual.DebitGlAccountId = value; } }
        [FwLogicProperty(Id: "0A88uwtDAcjvT", IsReadOnly: true)]
        public string DebitGlAccountNumber { get; set; }
        [FwLogicProperty(Id: "0aDOCipWDXbCi", IsReadOnly: true)]
        public string DebitGlAccountDescription { get; set; }
        [FwLogicProperty(Id: "0AuEMGWtPctnK")]
        public string CreditGlAccountId { get { return glManual.CreditGlAccountId; } set { glManual.CreditGlAccountId = value; } }
        [FwLogicProperty(Id: "0b4Yt9WXTn8Lx", IsReadOnly: true)]
        public string CreditGlAccountNumber { get; set; }
        [FwLogicProperty(Id: "0beXXM5LRFBiU", IsReadOnly: true)]
        public string CreditGlAccountDescription { get; set; }
        [FwLogicProperty(Id: "0bpkOegQ8kTry")]
        public decimal? Amount { get { return glManual.Amount; } set { glManual.Amount = value; } }
        [FwLogicProperty(Id: "0C4CQa5ZcEq9d")]
        public string GroupHeading { get { return glManual.GroupHeading; } set { glManual.GroupHeading = value; } }
        [JsonIgnore]
        [FwLogicProperty(Id: "0CdFAKQCWeOwB")]
        public bool? IsManual { get { return glManual.IsManual; } set { glManual.IsManual = value; } }
        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;
            if (saveMode.Equals(TDataRecordSaveMode.smInsert))
            {
                if (string.IsNullOrEmpty(InternalChar))
                {
                    InternalChar = AppFunc.GetInternalChar(AppConfig).Result;
                }
            }
            return isValid;
        }
        //------------------------------------------------------------------------------------ 
    }
}
