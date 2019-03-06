﻿
namespace WebLibrary
{
    public static class RwConstants
    {
        //MISC/LABOR RATES
        public const string RATE_AVAILABLE_FOR_MISC = "M";
        public const string RATE_AVAILABLE_FOR_LABOR = "L";

        //DEPARTMENT TYPES
        public const string DEPARTMENT_TYPE_RENTAL = "R";
        public const string DEPARTMENT_TYPE_SALES = "S";
        public const string DEPARTMENT_TYPE_PARTS = "P";
        public const string DEPARTMENT_TYPE_TRANSPORTATION = "T";
        public const string DEPARTMENT_TYPE_FACILITIES = "SP";
        public const string DEPARTMENT_TYPE_MISC = "M";
        public const string DEPARTMENT_TYPE_LABOR = "L";

        //SELECT ALL/NONE
        public const string SELECT_ALL = "A";
        public const string SELECT_NONE = "N";

        //MODULES
        public const string MODULE_CUSTOMER = "CUSTOMER";
        public const string MODULE_DEAL = "DEAL";
        public const string MODULE_QUOTE = "QUOTE";
        public const string MODULE_REPAIR = "REPAIR";
        public const string MODULE_PROJECT = "PROJECT";
        public const string MODULE_PURCHASE_ORDER = "PO";
        public const string MODULE_TRANSFER = "TRANSFER";

        //QUOTE/ORDER
        public const string ORDER_TYPE_QUOTE = "Q";
        public const string ORDER_TYPE_ORDER = "O";
        public const string ORDER_TYPE_PROJECT = "PROJECT";
        public const string ORDER_TYPE_PURCHASE_ORDER = "C";
        public const string ORDER_TYPE_TRANSFER = "T";

        public const string QUOTE_STATUS_PROSPECT = "PROSPECT";
        public const string QUOTE_STATUS_RESERVED = "RESERVED";
        public const string QUOTE_STATUS_ACTIVE = "ACTIVE";
        public const string QUOTE_STATUS_HOLD = "HOLD";
        public const string QUOTE_STATUS_ORDERED = "ORDERED";
        public const string QUOTE_STATUS_CLOSED = "CLOSED";
        public const string QUOTE_STATUS_CANCELLED = "CANCELLED";

        public const string ORDER_STATUS_CONFIRMED = "CONFIRMED";
        public const string ORDER_STATUS_ACTIVE = "ACTIVE";
        public const string ORDER_STATUS_HOLD = "HOLD";
        public const string ORDER_STATUS_COMPLETE = "COMPLETE";
        public const string ORDER_STATUS_CLOSED = "CLOSED";
        public const string ORDER_STATUS_CANCELLED = "CANCELLED";
        public const string ORDER_STATUS_SNAPSHOT = "SNAPSHOT";

        public const string TRANSFER_STATUS_NEW = "NEW";
        public const string TRANSFER_STATUS_CONFIRMED = "CONFIRMED";
        public const string TRANSFER_STATUS_ACTIVE = "ACTIVE";
        public const string TRANSFER_STATUS_COMPLETE = "COMPLETE";
        public const string TRANSFER_STATUS_CLOSED = "CLOSED";
        public const string TRANSFER_STATUS_CANCELLED = "CANCELLED";

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

        public const string RECTYPE_RENTAL = "R";
        public const string RECTYPE_SALE = "S";
        public const string RECTYPE_MISCELLANEOUS = "M";
        public const string RECTYPE_LABOR = "L";
        public const string RECTYPE_LOSS_AND_DAMAGE = "F";

        public const string TOTAL_TYPE_WEEKLY = "W";
        public const string TOTAL_TYPE_MONTHLY = "M";
        public const string TOTAL_TYPE_EPISODIC = "E";
        public const string TOTAL_TYPE_PERIOD = "P";


        //CONTRACT
        public const string CONTRACT_TYPE_RECEIVE = "RECEIVE";
        public const string CONTRACT_TYPE_OUT = "OUT";
        public const string CONTRACT_TYPE_EXCHANGE = "EXCHANGE";
        public const string CONTRACT_TYPE_IN = "IN";
        public const string CONTRACT_TYPE_RETURN = "RETURN";

        //INVOICE
        public const string INVOICE_DATE_TYPE_INVOICE_DATE = "INVOICE_DATE";
        public const string INVOICE_DATE_TYPE_BILLING_START_DATE = "BILLING_START_DATE";
        public const string INVOICE_STATUS_NEW = "NEW";
        public const string INVOICE_STATUS_APPROVED = "APPROVED";
        public const string INVOICE_STATUS_PROCESSED = "PROCESSED";
        public const string INVOICE_STATUS_CLOSED = "CLOSED";
        public const string INVOICE_STATUS_VOID = "VOID";

        //BATCH PROCESSING
        public const string BATCH_TYPE_INVOICE = "INVOICE";
        public const string BATCH_TYPE_VENDOR_INVOICE = "VENDORINVOICE";
        public const string BATCH_TYPE_RECEIPT = "AR";

        //DELIVERY
        public const string ONLINE_DELIVERY_STATUS_PARTIAL = "PARTIAL";
        public const string ONLINE_DELIVERY_STATUS_COMPLETE = "COMPLETE";

        //PROJECT
        public const string PROJECT_STATUS_NEW = "NEW";
        public const string PROJECT_STATUS_ACTIVE = "ACTIVE";
        public const string PROJECT_STATUS_CLOSED = "CLOSED";

        //PURCHASE ORDER
        public const string PURCHASE_ORDER_STATUS_NEW = "NEW";
        public const string PURCHASE_ORDER_STATUS_OPEN = "OPEN";
        public const string PURCHASE_ORDER_STATUS_RECEIVED = "RECEIVED";
        public const string PURCHASE_ORDER_STATUS_COMPLETE = "COMPLETE";
        public const string PURCHASE_ORDER_STATUS_CLOSED = "CLOSED";
        public const string PURCHASE_ORDER_STATUS_VOID = "VOID";
        public const string PURCHASE_ORDER_STATUS_SNAPSHOT = "SNAPSHOT";

        //VENDOR INVOICE
        public const string VENDOR_INVOICE_NUMBER_ACCRUAL = "ACCRUAL";

        public const string VENDOR_INVOICE_STATUS_NEW = "NEW";
        public const string VENDOR_INVOICE_STATUS_APPROVED = "APPROVED";
        public const string VENDOR_INVOICE_STATUS_PROCESSED = "PROCESSED";
        public const string VENDOR_INVOICE_STATUS_CLOSED = "CLOSED";


        //RECEIPT
        public const string RECEIPT_PAYMENT_BY_DEAL = "DEAL";
        public const string RECEIPT_PAYMENT_BY_CUSTOMER = "CUSTOMER";
        public const string RECEIPT_RECTYPE_PAYMENT = "P";

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
        public const string INVENTORY_CLASSIFICATION_CONTAINER = "N";
        public const string INVENTORY_CLASSIFICATION_ITEM = "I";
        public const string INVENTORY_CLASSIFICATION_ACCESSORY = "A";
        public const string INVENTORY_CLASSIFICATION_MISCELLAENOUS = "M";

        public const string INVENTORY_SORT_BY_ICODE = "ICODE";
        public const string INVENTORY_SORT_BY_DESCRIPTION = "DESCRIPTION";
        public const string INVENTORY_SORT_BY_PART_NUMBER = "PARTNO";
        public const string INVENTORY_SORT_BY_INVENTORY_MANAGEMENT = "INVENTORY";


        public const string RATE_TYPE_SINGLE = "SINGLE";
        public const string RATE_TYPE_RECURRING = "RECURRING";


        //ADDRESSES
        public const string ADDRESS_TYPE_BILLING = "BILL";

        public const string BILLING_ADDRESS_TYPE_CUSTOMER = "CUSTOMER";
        public const string BILLING_ADDRESS_TYPE_OTHER = "OTHER";

        //CHECK-IN
        public const string CHECK_IN_NEW_ORDER_ACTION_ADD_NEW_ORDER = "Y";
        public const string CHECK_IN_NEW_ORDER_ACTION_SWAP_ITEM = "S";



        //CONTAINER
        public const string CONTAINER_STAGING_ADD_ITEMS_RULE_AUTOMATICALLY_ADD = "AUTOADD";
        public const string CONTAINER_STAGING_ADD_ITEMS_RULE_WARN_BUT_ADD = "WARN";
        public const string CONTAINER_STAGING_ADD_ITEMS_RULE_DO_NOT_WARN_DO_NOT_ADD = "NOWARN";
        public const string CONTAINER_STAGING_ADD_ITEMS_RULE_DO_NOT_STAGE = "ERROR";

        public const string CONTAINER_PACKING_LIST_BEHAVIOR_AUTOMATICALLY_PRINT = "AUTOPRINT";
        public const string CONTAINER_PACKING_LIST_BEHAVIOR_PROMPT_TO_PRINT = "PROMPT";
        public const string CONTAINER_PACKING_LIST_BEHAVIOR_DO_NOTHING = "NONE";


        public const string VEHICLE_TYPE_VEHICLE = "VEHICLE";
        public const string VEHICLE_TYPE_GENERATOR = "GENERATOR";



        //EXPORTING
        public const string DATA_EXPORT_SETTINGS_TYPE_ONLINE_ORDER_TRACKING = "ONLINE ORDER TRACKING";
        public const string DATA_EXPORT_SETTINGS_TYPE_CUSTOMER = "CUSTOMER";
        public const string DATA_EXPORT_SETTINGS_TYPE_DEAL = "DEAL";
        public const string DATA_EXPORT_SETTINGS_TYPE_TIME_LOG = "TIMELOG";
        public const string DATA_EXPORT_SETTINGS_TYPE_VENDOR = "VENDOR";
        public const string DATA_EXPORT_SETTINGS_TYPE_ORDER = "ORDER";
        public const string DATA_EXPORT_SETTINGS_TYPE_DEAL_INVOICE_HEADER = "DEAL INVOICE HEADER";
        public const string DATA_EXPORT_SETTINGS_TYPE_DEAL_INVOICE_DETAIL = "DEAL INVOICE DETAIL";
        public const string DATA_EXPORT_SETTINGS_TYPE_DEAL_INVOICE_GL_SUMMARY = "DEAL INVOICE G/L SUMMARY";
        public const string DATA_EXPORT_SETTINGS_TYPE_DEAL_INVOICE_TAX = "DEAL INVOICE TAX";
        public const string DATA_EXPORT_SETTINGS_TYPE_DEAL_INVOICE_NOTE = "DEAL INVOICE NOTE";
        public const string DATA_EXPORT_SETTINGS_TYPE_RECEIPT_HEADER = "RECEIPT HEADER";
        public const string DATA_EXPORT_SETTINGS_TYPE_RECEIPT_DETAIL = "RECEIPT DETAIL";
        public const string DATA_EXPORT_SETTINGS_TYPE_VENDOR_INVOICE_HEADER = "VENDOR INVOICE HEADER";
        public const string DATA_EXPORT_SETTINGS_TYPE_VENDOR_INVOICE_DETAIL = "VENDOR INVOICE DETAIL";
        public const string DATA_EXPORT_SETTINGS_TYPE_VENDOR_INVOICE_GL_SUMMARY = "VENDOR INVOICE G/L SUMMARY";
        public const string DATA_EXPORT_SETTINGS_TYPE_VENDOR_INVOICE_TAX = "VENDOR INVOICE TAX";

        //AVAILABILITY
        public const int AVAILABILITY_COLOR_NO_AVAILABILITY = 13429759; //aqua
        public const int AVAILABILITY_TEXT_COLOR_NO_AVAILABILITY = 0; //black
        public const int AVAILABILITY_COLOR_NEGATIVE = 16711680; //red
        public const int AVAILABILITY_COLOR_POSITIVE = 1176137; //green
        public const int AVAILABILITY_TEXT_COLOR_NEGATIVE = 16777215; //white
        public const int AVAILABILITY_TEXT_COLOR_POSITIVE = 0; //black
        public const int AVAILABILITY_COLOR_RESERVED = 15132390; //gray
        public const int AVAILABILITY_TEXT_COLOR_RESERVED = 0; //black
        public const int AVAILABILITY_COLOR_RETURNING = 618726; //blue
        public const int AVAILABILITY_TEXT_COLOR_RETURNING = 16777215; //white
        public const int AVAILABILITY_COLOR_NEEDRECALC = 3211473; //dark blue
        public const int AVAILABILITY_TEXT_COLOR_NEEDRECALC = 16777215; //white

        public const int AVAILABILITY_COLOR_ORDER = 618726; //blue TEMPORARY


        //CONTACTS / COMPANY
        public const string COMPANY_TYPE_LEAD = "LEAD";
        public const string COMPANY_TYPE_PROSPECT = "PROSPECT";
        public const string COMPANY_TYPE_CUSTOMER = "CUSTOMER";
        public const string COMPANY_TYPE_DEAL = "DEAL";
        public const string COMPANY_TYPE_VENDOR = "VENDOR";
        public const int COMPANY_TYPE_LEAD_COLOR = 16744512; // orange
        public const int COMPANY_TYPE_PROSPECT_COLOR = 16711808; //pink/purple
        public const int COMPANY_TYPE_CUSTOMER_COLOR = 16777088; // light yellow
        public const int COMPANY_TYPE_DEAL_COLOR = 253498; //deal;
        public const int COMPANY_TYPE_VENDOR_COLOR = 2144255; //light blue

    }

    public class QuoteOrderCopyRequest
    {
        public string CopyToDealId;
        public bool CopyRatesFromInventory;
        public bool CopyDates;
        public bool CopyLineItemNotes;
        public bool CombineSubs;
        public bool CopyDocuments;
    }

    public class ApplyBottomLineDaysPerWeekRequest
    {
        public string OrderId;
        public string PurchaseOrderId;
        public string RecType;
        public bool? Subs;
        public decimal DaysPerWeek;
    }

    public class ApplyBottomLineDiscountPercentRequest
    {
        public string OrderId;
        public string PurchaseOrderId;
        public string RecType;
        public bool? Subs;
        public decimal DiscountPercent;
    }

    public class ApplyBottomLineTotalRequest
    {
        public string OrderId;
        public string PurchaseOrderId;
        public string RecType;
        public bool? Subs;
        public string TotalType;
        public decimal Total;
        public bool? IncludeTaxInTotal;
    }


}
