
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
    }
}
