
namespace WebLibrary
{
    public static class RwConstants
    {



        public const string RATE_AVAILABLE_FOR_MISC = "M";
        public const string RATE_AVAILABLE_FOR_LABOR = "L";


        public const string DEPARTMENT_TYPE_RENTAL = "R";
        public const string DEPARTMENT_TYPE_SALES = "S";
        public const string DEPARTMENT_TYPE_PARTS = "P";
        public const string DEPARTMENT_TYPE_TRANSPORTATION = "T";
        public const string DEPARTMENT_TYPE_FACILITIES = "SP";
        public const string DEPARTMENT_TYPE_MISC = "M";
        public const string DEPARTMENT_TYPE_LABOR = "L";


        //QOTE/ORDER
        public const string MODULE_QUOTE = "QUOTE";
        public const string MODULE_REPAIR = "REPAIR";

        public const string ORDER_TYPE_QUOTE = "Q";
        public const string ORDER_TYPE_ORDER = "O";

        public const string QUOTE_STATUS_PROSPECT = "PROSPECT";
        public const string QUOTE_STATUS_ACTIVE = "ACTIVE";

        public const string ORDER_STATUS_CONFIRMED = "CONFIRMED";

        public const string ORDER_STATUS_FILTER_STAGED_ONLY = "STAGEDONLY";
        public const string ORDER_STATUS_FILTER_NOT_YET_STAGED = "NOTYETSTAGED";
        public const string ORDER_STATUS_FILTER_STILL_OUT = "STILLOUT";
        public const string ORDER_STATUS_FILTER_IN_ONLY = "INONLY";

        public const string SEARCH_MODE_PREFERENCE_LIST = "LIST";
        public const string SEARCH_MODE_PREFERENCE_HYBRID = "HYBRID";
        public const string SEARCH_MODE_PREFERENCE_GRID = "GRID";

        public const string RATE_TYPE_DAILY = "DAILY";
        public const string RATE_TYPE_WEEKLY = "WEEKLY";
        public const string RATE_TYPE_3WEEK = "3WEEK";
        public const string RATE_TYPE_MONTHLY = "MONTHLY";

        public const string RECTYPE_RENT = "R";
        public const string RECTYPE_SALE = "S";
        public const string RECTYPE_MISC = "M";
        public const string RECTYPE_LABOR = "L";

        public const string TOTAL_TYPE_WEEKLY = "W";
        public const string TOTAL_TYPE_MONTHLY = "M";
        public const string TOTAL_TYPE_EPISODIC = "E";
        public const string TOTAL_TYPE_PERIOD = "P";
        

        //DELIVERY
        public const string ONLINE_DELIVERY_STATUS_PARTIAL = "PARTIAL";
        public const string ONLINE_DELIVERY_STATUS_COMPLETE = "COMPLETE";



        //REPAIR
        public const string REPAIR_STATUS_NEW = "NEW";

        public const string REPAIR_PRIORITY_HIGH = "HIG";
        public const string REPAIR_PRIORITY_MEDIUM = "MED";
        public const string REPAIR_PRIORITY_LOW = "LOW";

        public const string REPAIR_TYPE_OWNED = "OWNED";
        public const string REPAIR_TYPE_CONSIGNED = "CONSIGNED";
        public const string REPAIR_TYPE_OUTSIDE = "OUTSIDE";

        //INVENTORY
        public const string INVENTORY_AVAILABLE_FOR_RENT = "R";
        public const string INVENTORY_AVAILABLE_FOR_SALE = "S";
        public const string INVENTORY_AVAILABLE_FOR_PARTS = "P";
        public const string INVENTORY_AVAILABLE_FOR_VEHICLE = "V";

        public const string INVENTORY_STATUS_TYPE_IN = "IN";
        public const string INVENTORY_STATUS_TYPE_OUT = "OUT";

        public const string INVENTORY_CLASSIFICATION_KIT = "K";
        public const string INVENTORY_CLASSIFICATION_COMPLETE = "C";

        public const string INVENTORY_SORT_BY_ICODE = "ICODE";
        public const string INVENTORY_SORT_BY_DESCRIPTION = "DESCRIPTION";
        public const string INVENTORY_SORT_BY_PART_NUMBER = "PARTNO";
        public const string INVENTORY_SORT_BY_INVENTORY_MANAGEMENT = "INVENTORY";



        //ADDRESSES
        public const string ADDRESS_TYPE_BILLING = "BILL";

        public const string BILLING_ADDRESS_TYPE_CUSTOMER = "CUSTOMER";
        public const string BILLING_ADDRESS_TYPE_OTHER = "OTHER";


    }

    public class QuoteOrderCopyRequest
    {
        public string CopyToType;
        public string CopyToDealId;
        public bool CopyRatesFromInventory;
        public bool CopyDates;
        public bool CopyLineItemNotes;
        public bool CombineSubs;
        public bool CopyDocuments;
    }

    public class BottomLineDiscountPercentRequest
    {
        public string OrderId;
        public string RecType;
        public decimal DiscountPercent;
    }

    public class BottomLineDaysPerWeekRequest
    {
        public string OrderId;
        public string RecType;
        public decimal DaysPerWeek;
    }

    public class BottomLineTotalRequest
    {
        public string OrderId;
        public string RecType;
        public string TotalType;
        public decimal Total;
        public bool? IncludeTaxInTotal;
    }


}
