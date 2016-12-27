using Newtonsoft.Json;
using RentalWorksQuikScan.Source;

namespace RentalWorksQuikScan.Modules.ExchangeModels
{
    public enum RequestTypes { None, GetItemInfo, ExchangeItem }
    public enum ValidDlgTypes { None, InNonBC, InSerial, OutNonBc, PendingInBC, PendingInNonBc, PendingOutNonBc, PendingOutSerial }

    public class ExchangeRequest
    {
        public string itemstatus { get;set; } = string.Empty;
        //public RequestTypes requesttype { get;set; } = RequestTypes.None;
    }

    public class ExchangeResponse
    {
        public bool success { get;set; } = false;
        public string msg { get;set; } = string.Empty;

        public bool confirmallowcrossicode { get;set; } = false;
        public bool confirmallowcrosswarehouse { get;set; } = false;

        public bool resetall { get;set; } = false;
        public bool resetinitem { get;set; } = false;
        public bool resetoutitem { get;set; } = false;
    }

    public class UserContext
    {
        public string usersid { get;set; } = string.Empty;
        public string primarylocationid { get;set; } = string.Empty;
        public string primarywarehouseid { get;set; } = string.Empty;
        public string rentalinventorydepartmentid { get;set; } = string.Empty;
        public bool hasquiklocate { get;set; } = false;
    }

    public class ExchangeModel
    {
        public string orderid { get;set; } = string.Empty;
        public string dealid { get;set; } = string.Empty;
        public string departmentid { get;set; } = string.Empty;
        public int qty { get;set; } = 0;
        public bool completingpending { get;set; } = false;
        public bool removingFromContainer { get;set; } = false;
        public string ordermode { get;set; } = string.Empty;
        public string exchangecontractid { get;set; } = string.Empty;
        public bool allowcrossicode { get;set; } = false;
        public bool allowcrosswarehouse { get;set; } = false;

        public ValidDlgTypes validDlgType { get;set; } = ValidDlgTypes.None;
        public ValidDlg validDlg { get;set; } = new ValidDlg();
        public ValidDlgResults validDlgResult { get;set; } = new ValidDlgResults();

        public ExchangeRequest request { get;set; } = new ExchangeRequest();
        public ExchangeResponse response { get;set; } = new ExchangeResponse();

        public ExchangeItem frmInExchangeItem { get;set; } = new ExchangeItem();
        public ExchangeItem frmOutExchangeItem { get;set; } = new ExchangeItem();

        public ExchangeModel()
        {
            frmInExchangeItem.itemstatus = RwConstants.ItemStatus.IN;
            frmOutExchangeItem.itemstatus = RwConstants.ItemStatus.OUT;
        }

        public static ExchangeModel ConvertToExchangeModel(dynamic exchange)
        {
            string json = JsonConvert.SerializeObject(exchange);
            ExchangeModel exchangeModel = JsonConvert.DeserializeObject<ExchangeModel>(json);
            return exchangeModel;
        }
    }

    public class ExchangeItem
    {
        public string barcode { get;set; } = string.Empty;
        public string itemstatus { get;set; } = string.Empty;
        public TBCItem item { get;set; } = new TBCItem();

        public bool showbtnremovefromcontainer { get;set; } = false;
        public bool clearitem { get;set; } = false;
    }

    public class TBCItem
    {
        public enum StatusTypes { Found, Error, Exchange, None }
        public string barcode { get;set; } = string.Empty;
        public string serialno { get;set; } = string.Empty;
        public string bctype { get;set; } = string.Empty; //O=Order,T=Transfer
        public string orderid { get;set; } = string.Empty;
        public string dealid { get;set; } = string.Empty;
        public string deptid { get;set; } = string.Empty;
        public string masterid { get;set; } = string.Empty;
        public string masteritemid { get;set; } = string.Empty;
        public string parentid { get;set; } = string.Empty;
        public string rentalitemid { get;set; } = string.Empty;
        public string warehouseid { get;set; } = string.Empty;
        public string whcode { get;set; } = string.Empty;
        public int ordertranid { get;set; } = 0;
        public string internalchar { get;set; } = string.Empty; 
        public string masterno { get;set; } = string.Empty;
        public string description { get;set; } = string.Empty;
        public string vendor { get;set; } = string.Empty;
        public string vendorid { get;set; } = string.Empty;
        public string consignorid { get;set; } = string.Empty;
        public string pono { get;set; } = string.Empty;
        public string poid { get;set; } = string.Empty;
        public string orderno { get;set; } = string.Empty;
        public string qtyordered { get;set; } = string.Empty;
        public string subqty { get;set; } = string.Empty;
        public string totalin { get;set; } = string.Empty;
        public string sessionin { get;set; } = string.Empty;
        public string stillout { get;set; } = string.Empty;
        public int statusno { get;set; } = 0;
        public bool neworder { get;set; } = false;
        public string trackedby { get;set; } = string.Empty;
        public double qty  { get;set; } = 0;
        public bool pending  { get;set; } = false;
        public string msg { get;set; } = string.Empty;
        public StatusTypes status { get;set; } = StatusTypes.None;
        public int statuscode { get;set; } = 0;
        public bool isbarcode { get;set; } = false;
        public bool iscontainer{ get;set; } = false;
        public string contractid { get;set; } = string.Empty;
        public string spaceid { get;set; } = string.Empty;
        public string spacetypeid { get;set; } = string.Empty;
        public string facilitiestypeid { get;set; } = string.Empty;
        public string spacelocation { get;set; } = string.Empty;
        public string aisle { get;set; } = string.Empty;
        public string shelf { get;set; } = string.Empty;
        public bool qcrequired { get;set; } = false;
        public bool itemincontainer { get;set; } = false;
        public string pendingorderid { get;set; } = string.Empty;
        public string pendingmasteritemid { get;set; } = string.Empty;
        public string outorderid { get;set; } = string.Empty;
        public string statustype { get;set; } = string.Empty;
        public string containerid { get;set; } = string.Empty;
        public string containeritemid { get;set; } = string.Empty;
        public string scannablemasterid { get;set; } = string.Empty;
        public string containeroutcontractid { get;set; } = string.Empty;
        public string containerno { get;set; } = string.Empty;
        public string contracttype { get;set; } = string.Empty;
        public string itemclass { get;set; } = string.Empty;
        public string orderby { get;set; } = string.Empty;
        public string packageitemid { get;set; } = string.Empty;
        public string nestedmasteritemid { get;set; } = string.Empty;
        public string quikincontractid { get;set; } = string.Empty;
        public int quikinitemid { get;set; } = 0;
        public string quikininternalchar { get;set; } = string.Empty;
        public bool hasmultiplecontainers { get;set; } = false;
        public bool usecontainerno { get;set; } = false;
        public string availfromdatetime { get;set; } = null;
        public string availtodatetime { get;set; } = null;
        public string pendingrepairid { get;set; } = string.Empty;  
    }

    public class ValidDlg
    {
        public string title { get;set; } = string.Empty;
        public string message { get;set; } = string.Empty;
        public string orderid { get;set; } = string.Empty;
        public string dealid { get;set; } = string.Empty;
        public string departmentid { get;set; } = string.Empty;
        public string masterid { get;set; } = string.Empty;
        public string locationid { get;set; } = string.Empty;
        public bool vendoronly { get;set; } = false;
        public string serialno { get;set; } = string.Empty;
        public string availfromdatetime { get;set; } = string.Empty;
        public string availtodatetime { get;set; } = string.Empty;
        public string pendingrepairid { get;set; } = string.Empty;
    }

    public class ValidDlgResults
    {
        public string masterno { get;set; } = string.Empty;
        public string orderid { get;set; } = string.Empty;
        public string orderno { get;set; } = string.Empty;
        public string dealid { get;set; } = string.Empty;
        public string departmentid { get;set; } = string.Empty;
        public string warehouseid { get;set; } = string.Empty;
        public string description { get;set; } = string.Empty;
        public string poid { get;set; } = string.Empty;
        public string pono { get;set; } = string.Empty;
        public string vendorid { get;set; } = string.Empty;
        public string vendor { get;set; } = string.Empty;
        public string masteritemid { get;set; } = string.Empty;
        public string masterid { get;set; } = string.Empty;
        public string rentalitemid { get;set; } = string.Empty;
        public int ordertranid { get;set; } = 0;
        public string internalchar { get;set; } = string.Empty;
        public string availfromdatetime { get;set; } = string.Empty;
        public string availtodatetime { get;set; } = string.Empty;
        public string pendingrepairid { get;set; } = string.Empty;
    }
}
