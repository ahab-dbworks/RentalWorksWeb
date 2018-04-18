
namespace WebLibrary
{
    public static class RwConstants
    {
        public const string ORDER_TYPE_QUOTE = "Q";
        public const string ORDER_TYPE_ORDER = "O";

        public const string QUOTE_STATUS_PROSPECT = "PROSPECT";
        public const string QUOTE_STATUS_ACTIVE = "ACTIVE";

        public const string ORDER_STATUS_CONFIRMED = "CONFIRMED";


        public const string ORDER_STATUS_FILTER_STAGED_ONLY = "STAGEDONLY";
        public const string ORDER_STATUS_FILTER_NOT_YET_STAGED = "NOTYETSTAGED";
        public const string ORDER_STATUS_FILTER_STILL_OUT = "STILLOUT";
        public const string ORDER_STATUS_FILTER_IN_ONLY = "INONLY";


        public const string INVENTORY_AVAILABLE_FOR_RENT = "R";
        public const string INVENTORY_AVAILABLE_FOR_SALE = "S";
        public const string INVENTORY_AVAILABLE_FOR_PARTS = "P";
        public const string INVENTORY_AVAILABLE_FOR_VEHICLE = "V";

        public const string RATE_AVAILABLE_FOR_MISC = "M";
        public const string RATE_AVAILABLE_FOR_LABOR = "L";



        public const string MODULE_QUOTE = "QUOTE";
        public const string MODULE_REPAIR = "REPAIR";


        public const string ADDRESS_TYPE_BILLING = "BILL";


        public const string REPAIR_STATUS_NEW = "NEW";

        public const string REPAIR_PRIORITY_HIGH = "HIG";
        public const string REPAIR_PRIORITY_MEDIUM = "MED";
        public const string REPAIR_PRIORITY_LOW = "LOW";

        public const string REPAIR_TYPE_OWNED = "OWNED";
        public const string REPAIR_TYPE_CONSIGNED = "CONSIGNED";
        public const string REPAIR_TYPE_OUTSIDE = "OUTSIDE";

        public const string INVENTORY_STATUS_TYPE_IN = "IN";
        public const string INVENTORY_STATUS_TYPE_OUT = "OUT";


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
}
