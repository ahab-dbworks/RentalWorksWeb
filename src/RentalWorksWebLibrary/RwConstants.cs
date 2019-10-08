namespace WebLibrary
{
    public static class RwConstants
    {
        //CONTROL
        public const string CONTROL_ID = "1";

        //USER
        public const string WEB_USER_TYPE_USER = "USER";
        public const string WEB_USER_TYPE_CONTACT = "CONTACT";

        //GL ACCOUNTS
        public const string GL_ACCOUNT_TYPE_ASSET = "ASSET";
        public const string GL_ACCOUNT_TYPE_INCOME = "INCOME";
        public const string GL_ACCOUNT_TYPE_LIABILITY = "LIABILITY";
        public const string GL_ACCOUNT_TYPE_EXPENSE = "EXPENSE";

        //MISC/LABOR RATES
        public const string RATE_AVAILABLE_FOR_MISC = "M";
        public const string RATE_AVAILABLE_FOR_LABOR = "L";
        public const string RATE_TYPE_SINGLE = "SINGLE";
        public const string RATE_TYPE_RECURRING = "RECURRING";

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
        public const string MODULE_INVOICE = "INVOICE";
        public const string MODULE_PHYSICAL_INVENTORY = "PHYSICAL INVENTORY";

        //QUOTE/ORDER
        public const string ORDER_TYPE_QUOTE = "Q";
        public const string ORDER_TYPE_ORDER = "O";
        public const string ORDER_TYPE_PROJECT = "PROJECT";
        public const string ORDER_TYPE_PURCHASE_ORDER = "C";
        public const string ORDER_TYPE_TRANSFER = "T";
        public const string ORDER_TYPE_CONTAINER = "N";
        public const string ORDER_TYPE_REPAIR = "R";

        public const string ORDER_TYPE_DESCRIPTION_QUOTE = "QUOTE";
        public const string ORDER_TYPE_DESCRIPTION_ORDER = "ORDER";
        public const string ORDER_TYPE_DESCRIPTION_PROJECT = "PROJECT";
        public const string ORDER_TYPE_DESCRIPTION_PURCHASE_ORDER = "PO";
        public const string ORDER_TYPE_DESCRIPTION_TRANSFER = "TRANSFER";
        public const string ORDER_TYPE_DESCRIPTION_CONTAINER = "CONTAINER";
        public const string ORDER_TYPE_DESCRIPTION_REPAIR = "REPAIR";

        public const string QUOTE_STATUS_NEW = "NEW";
        public const string QUOTE_STATUS_PROSPECT = "PROSPECT";
        public const string QUOTE_STATUS_RESERVED = "RESERVED";
        public const string QUOTE_STATUS_ACTIVE = "ACTIVE";
        public const string QUOTE_STATUS_HOLD = "HOLD";
        public const string QUOTE_STATUS_ORDERED = "ORDERED";
        public const string QUOTE_STATUS_CLOSED = "CLOSED";
        public const string QUOTE_STATUS_CANCELLED = "CANCELLED";
        public const string QUOTE_STATUS_REQUEST = "REQUEST";

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

        public const int QUOTE_ORDER_LOCKED_COLOR = 5075199;               // light red
        public const int QUOTE_ORDER_ON_HOLD_COLOR = 995562;               // red
        public const int QUOTE_RESERVED_COLOR = 16748574;                  // light blue
        public const int QUOTE_ORDER_NO_CHARGE_COLOR = 4227327;            // orange/red
        public const int QUOTE_ORDER_MULTI_WAREHOUSE_COLOR = 8446422;      // mustard/light-green
        public const int QUOTE_REQUEST_COLOR = 65280;                      // bright green
        public const int ORDER_LATE_COLOR = 14267391;                      // pink/light purple
        public const int ORDER_REPAIR_COLOR = 11447902;                    // teal
        public const int ORDER_LOSS_AND_DAMAGE_COLOR = 4194368;            // dark purple

        public const string QUOTE_ORDER_DATE_TYPE_ORDER_DATE = "ORDER_DATE";
        public const string QUOTE_ORDER_DATE_TYPE_ESTIMATED_START_DATE = "ESTIMATED_START_DATE";

        public const string ACTIVITY_TYPE_OUT = "OUT";
        public const string ACTIVITY_TYPE_IN = "IN";
        public const string ACTIVITY_TYPE_LOADIN = "LOADIN";
        public const string ACTIVITY_TYPE_PICK = "PICK";
        public const string ACTIVITY_TYPE_PICKUP = "PICKUP";
        public const string ACTIVITY_TYPE_PREP = "PREP";
        public const string ACTIVITY_TYPE_RECEIVE = "RECEIVE";
        public const string ACTIVITY_TYPE_RETURN = "RETURN";
        public const string ACTIVITY_TYPE_STRIKE = "STRIKE";
        public const string ACTIVITY_TYPE_TEST = "TEST";
        public const string ACTIVITY_TYPE_REPAIRDUE = "REPAIRDUE";

        //QUOTE/ORDER LINE-ITEMS
        public const string RECTYPE_RENTAL = "R";
        public const string RECTYPE_SALE = "S";
        public const string RECTYPE_MISCELLANEOUS = "M";
        public const string RECTYPE_PARTS = "P";
        public const string RECTYPE_LABOR = "L";
        public const string RECTYPE_LOSS_AND_DAMAGE = "F";
        public const string RECTYPE_USED_SALE = "RS";
        public const string RECTYPE_VEHICLE = "V";

        public const string RECTYPE_RENTAL_DESCRIPTION = "RENTAL";
        public const string RECTYPE_SALE_DESCRIPTION = "SALES";
        public const string RECTYPE_PARTS_DESCRIPTION = "PARTS";


        public const string TOTAL_TYPE_WEEKLY = "W";
        public const string TOTAL_TYPE_MONTHLY = "M";
        public const string TOTAL_TYPE_EPISODIC = "E";
        public const string TOTAL_TYPE_PERIOD = "P";

        public const string ITEMCLASS_COMPLETE = "C";
        public const string ITEMCLASS_COMPLETE_ITEM = "CI";
        public const string ITEMCLASS_COMPLETE_OPTION = "CO";
        public const string ITEMCLASS_KIT = "K";
        public const string ITEMCLASS_KIT_ITEM = "KI";
        public const string ITEMCLASS_KIT_OPTION = "KO";
        public const string ITEMCLASS_CONTAINER = "N";
        public const string ITEMCLASS_CONTAINER_ITEM = "NI";
        public const string ITEMCLASS_CONTAINER_OPTION = "NO";
        public const string ITEMCLASS_MISCELLANEOUS = "M";
        public const string ITEMCLASS_SUFFIX_ITEM = "I";
        public const string ITEMCLASS_SUFFIX_OPTION = "O";

        public const int COMPLETE_COLOR = 16746632;     // purple
        public const int KIT_COLOR = 5101142;           // green
        public const int MISCELLANEOUS_COLOR = 6382847; // pink/coral
        public const int ITEM_COLOR = 16777215;         // white
        public const int ACCESSORY_COLOR = 65535;       // yellow

        public const int SUB_COLOR = 9220607;           // salmon


        //CONTRACT
        public const string CONTRACT_TYPE_RECEIVE = "RECEIVE";
        public const string CONTRACT_TYPE_OUT = "OUT";
        public const string CONTRACT_TYPE_EXCHANGE = "EXCHANGE";
        public const string CONTRACT_TYPE_IN = "IN";
        public const string CONTRACT_TYPE_RETURN = "RETURN";
        public const string CONTRACT_TYPE_MANIFEST = "MANIFEST";
        public const string CONTRACT_TYPE_RECEIPT = "RECEIPT";
        public const string CONTRACT_TYPE_FILL = "FILL";
        public const string CONTRACT_TYPE_LOST = "LOST";

        //CONTRACT
        public const int CONTRACT_ITEM_VOIDED_COLOR = 16776960;          //aqua
        public const int CONTRACT_BILLING_DATE_ADJUSTED_COLOR = 7303167; // light red
        public const int CONTRACT_MIGRATED_COLOR = 16746632;             // blue


        //INVOICE
        public const string INVOICE_DATE_TYPE_INPUT_DATE = "INPUT_DATE";
        public const string INVOICE_DATE_TYPE_INVOICE_DATE = "INVOICE_DATE";
        public const string INVOICE_DATE_TYPE_BILLING_START_DATE = "BILLING_START_DATE";
        public const string INVOICE_STATUS_NEW = "NEW";
        public const string INVOICE_STATUS_APPROVED = "APPROVED";
        public const string INVOICE_STATUS_PROCESSED = "PROCESSED";
        public const string INVOICE_STATUS_CLOSED = "CLOSED";
        public const string INVOICE_STATUS_VOID = "VOID";

        public const string INVOICE_TYPE_BILLING = "BILLING";
        public const string INVOICE_TYPE_CREDIT = "CREDIT";
        public const string INVOICE_TYPE_ESTIMATE = "ESTIMATE";
        public const string INVOICE_TYPE_WORKSHEET = "WORKSHEET";
        public const string INVOICE_TYPE_PREVIEW = "PREVIEW";

        public const int INVOICE_LOCKED_COLOR = 767;              // red
        public const int INVOICE_NO_CHARGE_COLOR = 7303167;       // light red
        public const int INVOICE_ADJUSTED_COLOR = 16744703;       // light purple
        public const int INVOICE_HIATUS_COLOR = 6076420;          // green
        public const int INVOICE_FLAT_PO_COLOR = 16746633;        // blue
        public const int INVOICE_CREDIT_COLOR = 16768221;         // lighter purple
        public const int INVOICE_ALTERED_DATES_COLOR = 8453892;   // light green
        public const int INVOICE_REPAIR_COLOR = 11447902;         // darkish green
        public const int INVOICE_ESTIMATE_COLOR = 98559;          // orange 
        public const int INVOICE_LOSS_AND_DAMAGE_COLOR = 4194368; // dark purple

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

        //CURRENCY
        public const int FOREIGN_CURRENCY_COLOR = 13303701;   // mint green

        //PURCHASE ORDER
        public const string PURCHASE_ORDER_STATUS_NEW = "NEW";
        public const string PURCHASE_ORDER_STATUS_OPEN = "OPEN";
        public const string PURCHASE_ORDER_STATUS_RECEIVED = "RECEIVED";
        public const string PURCHASE_ORDER_STATUS_COMPLETE = "COMPLETE";
        public const string PURCHASE_ORDER_STATUS_CLOSED = "CLOSED";
        public const string PURCHASE_ORDER_STATUS_VOID = "VOID";
        public const string PURCHASE_ORDER_STATUS_SNAPSHOT = "SNAPSHOT";

        public const int PO_NEEDS_APPROVAL_COLOR = 5075199;         // light red
        public const int PO_DROP_SHIP_COLOR = 11447902;             // teal
        public const int PO_ITEMS_NEED_BARCODE_COLOR = 8388736;     // purple
        public const int PO_ITEMS_IN_HOLDING_COLOR = 12320443;      // light green



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
        public const string RECEIPT_RECTYPE_OVERPAYMENT = "O";
        public const string RECEIPT_RECTYPE_DEPLETING_DEPOSIT = "D";
        public const string RECEIPT_RECTYPE_REFUND = "R";
        public const string RECEIPT_RECTYPE_NSF_ADJUSTMENT = "A";
        public const string RECEIPT_RECTYPE_WRITE_OFF = "W";
        public const string RECEIPT_RECTYPE_CREDIT_MEMO = "C";

        public const string PAYMENT_TYPE_TYPE_WRITE_OFF = "WRITE OFF";

        public const int RECEIPT_RECTYPE_OVERPAYMENT_COLOR = 8454143;            // yellow
        public const int RECEIPT_RECTYPE_DEPLETING_DEPOSIT_COLOR = 250679;       // green
        public const int RECEIPT_RECTYPE_REFUND_CHECK_COLOR = 16740207;          // blue 
        public const int RECEIPT_RECTYPE_NSF_ADJUSTMENT_COLOR = 7303167;         // light red
        public const int RECEIPT_RECTYPE_WRITE_OFF_COLOR = 4227327;              // light orange
        public const int RECEIPT_RECTYPE_CREDIT_MEMO_COLOR = 14068651;           // light purple


        //REPAIR
        public const string REPAIR_STATUS_NEW = "NEW";
        public const string REPAIR_PRIORITY_HIGH = "HIG";
        public const string REPAIR_PRIORITY_MEDIUM = "MED";
        public const string REPAIR_PRIORITY_LOW = "LOW";
        public const string REPAIR_TYPE_OWNED = "OWNED";
        public const string REPAIR_TYPE_CONSIGNED = "CONSIGNED";
        public const string REPAIR_TYPE_OUTSIDE = "OUTSIDE";

        //INVENTORY
        public const string INVENTORY_AVAILABLE_FROM_WAREHOUSE = "W";
        public const string INVENTORY_AVAILABLE_FROM_CATALOG = "C";

        public const string INVENTORY_AVAILABLE_FOR_RENT = "R";
        public const string INVENTORY_AVAILABLE_FOR_SALE = "S";
        public const string INVENTORY_AVAILABLE_FOR_PARTS = "P";
        public const string INVENTORY_AVAILABLE_FOR_VEHICLE = "V";

        public const int INVENTORY_AVAILABLE_FOR_SALE_COLOR = 6504703;    // reddish
        public const int INVENTORY_AVAILABLE_FOR_PARTS_COLOR = 16543686;  // purplish

        public const string INVENTORY_STATUS_TYPE_IN = "IN";
        public const string INVENTORY_STATUS_TYPE_OUT = "OUT";
        public const string INVENTORY_STATUS_TYPE_INCONTAINER = "INCONTAINER";
        public const string INVENTORY_STATUS_TYPE_STAGED = "STAGED";
        public const string INVENTORY_STATUS_TYPE_IN_REPAIR = "INREPAIR";
        public const string INVENTORY_STATUS_TYPE_IN_TRANSIT = "INTRANSIT";

        public const string INVENTORY_CLASSIFICATION_KIT = "K";
        public const string INVENTORY_CLASSIFICATION_COMPLETE = "C";
        public const string INVENTORY_CLASSIFICATION_CONTAINER = "N";
        public const string INVENTORY_CLASSIFICATION_ITEM = "I";
        public const string INVENTORY_CLASSIFICATION_ACCESSORY = "A";
        public const string INVENTORY_CLASSIFICATION_MISCELLAENOUS = "M";
        public const string INVENTORY_CLASSIFICATION_VEHICLE = "V";

        public const string INVENTORY_SORT_BY_ICODE = "ICODE";
        public const string INVENTORY_SORT_BY_DESCRIPTION = "DESCRIPTION";
        public const string INVENTORY_SORT_BY_PART_NUMBER = "PARTNO";
        public const string INVENTORY_SORT_BY_INVENTORY_MANAGEMENT = "INVENTORY";

        public const string INVENTORY_RANK_A = "A";
        public const string INVENTORY_RANK_B = "B";
        public const string INVENTORY_RANK_C = "C";
        public const string INVENTORY_RANK_D = "D";
        public const string INVENTORY_RANK_E = "E";
        public const string INVENTORY_RANK_F = "F";
        public const string INVENTORY_RANK_G = "G";

        public const string INVENTORY_WARDROBE_DESCRIPTION = "WARDDESC";
        public const string INVENTORY_WEB_DESCRIPTION = "WEB_DESC";

        public const string INVENTORY_PACKAGE_REVENUE_CALCULATION_FORMULA_USE_REPLACEMENT_COST = "REPLACEMENT";
        public const string INVENTORY_PACKAGE_REVENUE_CALCULATION_FORMULA_USE_UNIT_VALUE = "COST";

        public const string INVENTORY_CONFLICT_TYPE_POSITIVE = "P";
        public const string INVENTORY_CONFLICT_TYPE_NEGATIVE = "N";
        public const string INVENTORY_CONFLICT_TYPE_POSITIVE_DESCRIPTION = "POSITIVE";
        public const string INVENTORY_CONFLICT_TYPE_NEGATIVE_DESCRIPTION = "NEGATIVE";


        public const string INVENTORY_PACKAGE_PRICE_COMPLETEKIT_PRICE = "CP";
        public const string INVENTORY_PACKAGE_PRICE_ITEM_PRICE = "IP";
        public const string INVENTORY_PACKAGE_PRICE_SPECIAL_ITEM_PRICE = "SP";



        // ORDERTRAN
        public const string ORDERTRAN_ITEMSTATUS_STAGED = "S";
        public const string ORDERTRAN_ITEMSTATUS_OUT = "O";
        public const string ORDERTRAN_ITEMSTATUS_IN = "I";

        //CONTRACT
        public const int SUSPENDED_CONTRACT_COLOR = 10485760;


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
        public const string CONTAINER_STATUS_READY = "READY";
        public const string CONTAINER_STATUS_INCOMPLETE = "INCOMPLETE";
        public const int CONTAINER_INCOMPLETE_COLOR = 262399; // red


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


        //INVENTORY
        public const int QC_REQUIRED_COLOR = 33023;

        //AVAILABILITY
        public const string NO_AVAILABILITY_CAPTION = "No Availability";
        public const int AVAILABILITY_COLOR_NO_AVAILABILITY = 16772044;      //aqua
        public const int AVAILABILITY_TEXT_COLOR_NO_AVAILABILITY = 0;        //black
        public const int AVAILABILITY_COLOR_NEGATIVE = 255;                  //red
        public const int AVAILABILITY_COLOR_POSITIVE = 3381504;              //green
        public const int AVAILABILITY_COLOR_LOW = 65535;                     //yellow
        public const int AVAILABILITY_TEXT_COLOR_NEGATIVE = 16777215;        //white
        public const int AVAILABILITY_TEXT_COLOR_POSITIVE = 16777215;        //white
        public const int AVAILABILITY_TEXT_COLOR_LOW = 0;                    //black
        public const int AVAILABILITY_COLOR_RESERVED = 15132390;             //gray
        public const int AVAILABILITY_TEXT_COLOR_RESERVED = 0;               //black
        public const int AVAILABILITY_COLOR_RETURNING = 15101961;            //blue
        public const int AVAILABILITY_TEXT_COLOR_RETURNING = 16777215;       //white
        public const int AVAILABILITY_COLOR_NEEDRECALC = 13697073;           //dark blue
        public const int AVAILABILITY_TEXT_COLOR_NEEDRECALC = 16777215;      //white
        public const int AVAILABILITY_COLOR_HISTORICAL_DATE = 15132390;      //gray
        public const int AVAILABILITY_TEXT_COLOR_HISTORICAL_DATE = 0;        //black
        public const int AVAILABILITY_COLOR_LATE = 4227327;                  //orange
        public const int AVAILABILITY_TEXT_COLOR_LATE = 0;                   //black

        public const string AVAILABILITY_STATE_NO_AVAILABILITY_CHECK = "noavailcheck";
        public const string AVAILABILITY_STATE_STALE = "stale";
        public const string AVAILABILITY_STATE_NEGATIVE = "negative";
        public const string AVAILABILITY_STATE_LOW = "low";
        public const string AVAILABILITY_STATE_ZERO = "zero";
        public const string AVAILABILITY_STATE_ENOUGH = "enough";
        public const string AVAILABILITY_STATE_HISTORY = "history";
        public const string AVAILABILITY_STATE_POSITIVE_CONFLICT = "positiveconflict";


        //CONTACTS / COMPANY
        public const string COMPANY_TYPE_LEAD = "LEAD";
        public const string COMPANY_TYPE_PROSPECT = "PROSPECT";
        public const string COMPANY_TYPE_CUSTOMER = "CUSTOMER";
        public const string COMPANY_TYPE_DEAL = "DEAL";
        public const string COMPANY_TYPE_VENDOR = "VENDOR";
        public const int COMPANY_TYPE_LEAD_COLOR = 4227327;          // orange
        public const int COMPANY_TYPE_PROSPECT_COLOR = 8388863;      //pink/purple
        public const int COMPANY_TYPE_CUSTOMER_COLOR = 8454143;      // light yellow
        public const int COMPANY_TYPE_DEAL_COLOR = 253498;           //green;
        public const int COMPANY_TYPE_VENDOR_COLOR = 16758576;       //light blue


        //GLOBAL
        public const int NEGATIVE_VARIANCE_COLOR = 262399;             // red


        //STAGING / CHECK-OUT
        public const int STAGING_PENDING_ITEMS_MISSING_COLOR = 262399; // red


        //CUSTOM FORM
        public const string CUSTOM_FORM_ASSIGN_TO_ALL = "ALL";
        public const string CUSTOM_FORM_ASSIGN_TO_GROUPS = "GROUPS";
        public const string CUSTOM_FORM_ASSIGN_TO_USERS = "USERS";

        //WIDGET
        public const string WIDGET_ASSIGN_TO_ALL = "ALL";
        public const string WIDGET_ASSIGN_TO_GROUPS = "GROUPS";
        public const string WIDGET_ASSIGN_TO_USERS = "USERS";

        //PHYSICAL INVENTORY
        public const string PHYSICAL_INVENTORY_STATUS_NEW = "NEW";
        public const string PHYSICAL_INVENTORY_STATUS_ACTIVE = "ACTIVE";


        //BILLING CYCLE
        public const string BILLING_CYCLE_TYPE_WEEKLY = "WEEKLY";
        public const string BILLING_CYCLE_TYPE_BIWEEKLY = "BIWEEKLY";
        public const string BILLING_CYCLE_TYPE_4WEEKLY = "4WEEKLY";
        public const string BILLING_CYCLE_TYPE_MONTHLY = "MONTHLY";
        public const string BILLING_CYCLE_TYPE_CALENDARMONTH = "CALMONTH";
        public const string BILLING_CYCLE_TYPE_EPISODIC = "EPISODIC";
        public const string BILLING_CYCLE_TYPE_IMMEDIATE = "IMMEDIATE";
        public const string BILLING_CYCLE_TYPE_RATECHANGE = "RATECHANGE";
        public const string BILLING_CYCLE_TYPE_EVENTS = "EVENTS";
        public const string BILLING_CYCLE_TYPE_ONDEMAND = "ONDEMAND";
        public const string BILLING_CYCLE_TYPE_ATCLOSE = "ATCLOSE";
        public const string BILLING_CYCLE_TYPE_DECREASING = "DECREASING";

        public const string BILLING_CYCLE_BILL_ON_PERIOD_START = "START";
        public const string BILLING_CYCLE_BILL_ON_PERIOD_END = "END";



        //DEPARTMENT
        public const string DEPARTMENT_SALES_BILLING_RULE_BILL_WHEN_STAGED = "STAGED";
        public const string DEPARTMENT_SALES_BILLING_RULE_BILL_WHEN_CHECKED_OUT = "OUT";
        public const string DEPARTMENT_SALES_BILLING_RULE_BILL_ON_CONTRACT_BILLING_START_DATE = "BILLINGDATE";


        //CUSTOMER
        public const string CUSTOMER_STATUS_TYPE_OPEN = "O";
        public const string CUSTOMER_STATUS_TYPE_CLOSED = "O";
        public const string CUSTOMER_STATUS_TYPE_HOLD = "H";
        public const string CUSTOMER_STATUS_TYPE_INACTIVE = "I";


        //DEAL
        public const string DEAL_STATUS_TYPE_OPEN = "O";
        public const string DEAL_STATUS_TYPE_CLOSED = "O";
        public const string DEAL_STATUS_TYPE_HOLD = "H";
        public const string DEAL_STATUS_TYPE_INACTIVE = "I";


        //TAX OPTION
        public const string TAX_COUNTRY_USA = "U";
        public const string TAX_COUNTRY_CANADA = "C";

        //PHYSICAL INVENTORY
        public const string PHYSICAL_INVENTORY_COUNT_ADD = "A";
        public const string PHYSICAL_INVENTORY_COUNT_REPLACE = "R";

        //COLOR TYPES
        public const string COLOR_TYPE_WARDROBE = "W";
        public const string COLOR_TYPE_VEHICLE = "V";

        //SCHEDULE STATUS
        public const string SCHEDULE_STATUS_TYPE_VEHICLE = "V";
        public const string SCHEDULE_STATUS_TYPE_CREW = "C";
        public const string SCHEDULE_STATUS_TYPE_FACILITY = "S";

        //RESOURCE STATUS
        public const string RESOURCE_STATUS_TYPE_VEHICLE = "V";

        //SOUNDS
        public const string DEFAULT_SOUND_FILE_NAME = "./THEME/AUDIO/APPOINTED.MP3";


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
