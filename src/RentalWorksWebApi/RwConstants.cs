namespace WebApi
{
    public static class RwConstants
    {

        //NONE
        public const string INCLUDE = "INCLUDE";
        public const string EXCLUDE = "EXCLUDE";

        //NONE
        public const string NONE = "NONE";

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

        public const string MISC_CLASSIFICATION_FACILITIES = "SP";
        public const string LABOR_CLASSIFICATION_POSITION = "LP";

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
        public const string MODULE_ORDER = "ORDER";
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
        public const string ORDER_TYPE_TEMPLATE = "M";
        public const string ORDER_TYPE_PENDING_EXCHANGE = "PENDING";

        public const string MY_AGENT_ACTIVE_VIEW_VALUE = "AGENT";
        public const string MY_PROJECT_MANAGER_ACTIVE_VIEW_VALUE = "PROJECTMANAGER";

        public const string ORDER_TYPE_DESCRIPTION_QUOTE = "QUOTE";
        //public const string ORDER_TYPE_DESCRIPTION_RESERVED = "RESERVED";
        public const string ORDER_TYPE_DESCRIPTION_ORDER = "ORDER";
        public const string ORDER_TYPE_DESCRIPTION_PROJECT = "PROJECT";
        public const string ORDER_TYPE_DESCRIPTION_PURCHASE_ORDER = "PO";
        public const string ORDER_TYPE_DESCRIPTION_TRANSFER = "TRANSFER";
        public const string ORDER_TYPE_DESCRIPTION_CONTAINER = "CONTAINER";
        public const string ORDER_TYPE_DESCRIPTION_REPAIR = "REPAIR";
        public const string ORDER_TYPE_DESCRIPTION_PENDING_EXCHANGE = "PENDING EXCHANGE";

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

        public const string PURCHASE_ORDER_STATUS_FILTER_NOT_YET_RECEIVED = "NOTYETRECEIVED";
        public const string PURCHASE_ORDER_STATUS_FILTER_RECEIVED = "RECEIVED";
        public const string PURCHASE_ORDER_STATUS_FILTER_RETURNED = "RETURNED";
        public const string PURCHASE_ORDER_STATUS_FILTER_NOT_BARCODED = "NOTYETBARCODED";

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
        public const int ORDER_PENDING_PO_COLOR = 33023;                   // orange

        public const int ORDER_QUANTITY_ADJUSTED_AT_STAGING_COLOR = 7303167;       // light red

        public const int ORDER_SUB_ITEM_DATE_ESTIMATED_COLOR = 33023;      // orange


        public const string QUOTE_ORDER_DATE_TYPE_ORDER_DATE = "ORDER_DATE";
        public const string QUOTE_ORDER_DATE_TYPE_ESTIMATED_START_DATE = "ESTIMATED_START_DATE";

        public const string ORDER_DETERMINE_QUANTITIES_TO_BILL_BASED_ON_CONTRACT = "CONTRACT";
        public const string ORDER_DETERMINE_QUANTITIES_TO_BILL_BASED_ON_ORDER = "ORDER";

        public const string UPDATE_RATES_CONFIRMATION = "UPDATE RATES";

        public const int DEPRECIATION_ADJUSTMENT_COLOR = 7303167;       // light red

        //ACTIVITY
        public const string ACTIVITY_TYPE_PICK = "PICK";
        public const string ACTIVITY_TYPE_START = "START";
        public const string ACTIVITY_TYPE_STOP = "STOP";
        public const string ACTIVITY_TYPE_LOADIN = "LOADIN";
        public const string ACTIVITY_TYPE_PICKUP = "PICKUP";
        public const string ACTIVITY_TYPE_PREP = "PREP";
        public const string ACTIVITY_TYPE_RECEIVE = "RECEIVE";
        public const string ACTIVITY_TYPE_RETURN = "RETURN";
        public const string ACTIVITY_TYPE_STRIKE = "STRIKE";
        public const string ACTIVITY_TYPE_TEST = "TEST";
        public const string ACTIVITY_TYPE_REPAIRDUE = "REPAIRDUE";


        public const string ITEMCLASS_GROUP_HEADING = "GH";
        public const string ITEMCLASS_TEXT = "T";
        public const string ITEMCLASS_SUBTOTAL = "ST";


        //QUOTE/ORDER LINE-ITEMS
        public const string RECTYPE_RENTAL = "R";
        public const string RECTYPE_SALE = "S";
        public const string RECTYPE_MISCELLANEOUS = "M";
        public const string RECTYPE_PARTS = "P";
        public const string RECTYPE_LABOR = "L";
        public const string RECTYPE_LOSS_AND_DAMAGE = "F";
        public const string RECTYPE_USED_SALE = "RS";
        public const string RECTYPE_VEHICLE = "V";
        public const string RECTYPE_ADJUSTMENT = "A";

        public const string RECTYPE_RENTAL_DESCRIPTION = "RENTAL";
        public const string RECTYPE_SALE_DESCRIPTION = "SALES";
        public const string RECTYPE_PARTS_DESCRIPTION = "PARTS";
        public const string RECTYPE_LABOR_DESCRIPTION = "LABOR";
        public const string RECTYPE_MISC_DESCRIPTION = "MISC";


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
        public const string ITEMCLASS_MISCELLANEOUS_ITEM = "MI";
        public const string ITEMCLASS_SUFFIX_ITEM = "I";
        public const string ITEMCLASS_SUFFIX_OPTION = "O";

        public const string ITEMCLASS_COMPLETE_SUFFIX = "C";
        public const string ITEMCLASS_KIT_SUFFIX = "K";

        public const int COMPLETE_COLOR = 16746632;     // purple
        public const int KIT_COLOR = 5101142;           // green
        public const int MISCELLANEOUS_COLOR = 6382847; // pink/coral
        public const int ITEM_COLOR = 16777215;         // white
        public const int ACCESSORY_COLOR = 65535;       // yellow
        public const int OPTIONAL_ITEM_COLOR = 65535;   // yellow
        public const int PERCENTAGE_ITEM_COLOR = 42495; // orange

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
        public const string PAYMENT_TYPE_TYPE_REFUND_CHECK = "REFUND CHECK";

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

        public const string INVENTORY_TRACKED_BY_BAR_CODE = "BARCODE";
        public const string INVENTORY_TRACKED_BY_SERIAL_NO = "SERIALNO";
        public const string INVENTORY_TRACKED_BY_RFID = "RFID";
        public const string INVENTORY_TRACKED_BY_QUANTITY = "QUANTITY";

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

        public const string INVENTORY_OWNERSHIP_OWNED = "OWNED";
        public const string INVENTORY_OWNERSHIP_SUBBED = "SUBBED";  //  this is not really an "ownership". but we are using this as a selection on many report front-ends
        public const string INVENTORY_OWNERSHIP_CONSIGNED = "CONSIGNED";
        public const string INVENTORY_OWNERSHIP_LEASED = "LEASED";

        public const string INVENTORY_QUANTITY_TRANSACTION_TYPE_PURCHASE = "PURCHASE INVENTORY";

        public const string COST_CALCULATION_FIFO = "FIFO";
        public const string COST_CALCULATION_LIFO = "LIFO";
        public const string COST_CALCULATION_AVERAGE = "AVERAGEVALUE";
        public const string COST_CALCULATION_UNIT_VALUE = "UNITVALUE";  // will be removed

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
        public const int AVAILABILITY_COLOR_QC_REQUIRED = 33023;             //orange
        public const int AVAILABILITY_TEXT_COLOR_QC_REQUIRED = 0;            //black
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
        public const int AVAILABILITY_TEXT_COLOR_LATE = 16777215;  // white
        public const int AVAILABILITY_COLOR_LATE_BUT_RETURNING = 14914500;   //light purple #c493e3
        public const int AVAILABILITY_TEXT_COLOR_LATE_BUT_RETURNING = 16777215;  // white

        public const string AVAILABILITY_STATE_NO_AVAILABILITY_CHECK = "noavailcheck";
        public const string AVAILABILITY_STATE_STALE = "stale";
        public const string AVAILABILITY_STATE_NEGATIVE = "negative";
        public const string AVAILABILITY_STATE_LOW = "low";
        public const string AVAILABILITY_STATE_ZERO = "zero";
        public const string AVAILABILITY_STATE_ENOUGH = "enough";
        public const string AVAILABILITY_STATE_HISTORY = "history";
        public const string AVAILABILITY_STATE_POSITIVE_CONFLICT = "positiveconflict";

        public const string WAREHOUSEID_ALL = "ALL";


        //OFFICE LOCATION
        public const string LOCATION_INVOICE_MESSAGE_UNIQUEID2 = "INVMESSG";

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

        //CONTACT
        public const string CONTACT_RECORD_TYPE_CREW = "CREW";
        public const int CONTACT_TYPE_CREW_COLOR = 8454143;            // yellow


        //GLOBAL
        public const int NEGATIVE_VARIANCE_COLOR = 262399;             // red


        //STAGING / CHECK-OUT
        public const int STAGING_PENDING_ITEMS_MISSING_COLOR = 262399; // red

        //EXCHANGE
        public const int PENDING_EXCHANGE_COLOR = 8454143; // yellow
        public const string EXCHANGE_PENDING_ITEM_STATUS = "P";

        //CUSTOM FORM
        public const string CUSTOM_FORM_ASSIGN_TO_ALL = "ALL";
        public const string CUSTOM_FORM_ASSIGN_TO_GROUPS = "GROUPS";
        public const string CUSTOM_FORM_ASSIGN_TO_USERS = "USERS";

        //CUSTOM REPORT LAYOUT
        public const string CUSTOM_REPORT_LAYOUT_ASSIGN_TO_ALL = "ALL";
        public const string CUSTOM_REPORT_LAYOUT_ASSIGN_TO_GROUPS = "GROUPS";
        public const string CUSTOM_REPORT_LAYOUT_ASSIGN_TO_USERS = "USERS";

        //WIDGET
        public const string WIDGET_ASSIGN_TO_ALL = "ALL";
        public const string WIDGET_ASSIGN_TO_GROUPS = "GROUPS";
        public const string WIDGET_ASSIGN_TO_USERS = "USERS";

        //PHYSICAL INVENTORY
        public const string PHYSICAL_INVENTORY_STATUS_NEW = "NEW";
        public const string PHYSICAL_INVENTORY_STATUS_ACTIVE = "ACTIVE";
        public const string PHYSICAL_INVENTORY_STATUS_VOID = "VOID";


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


        //ORDER TYPE
        public const string ORDER_TYPE_TYPE_PO = "PO";
        public const string ORDER_TYPE_TYPE_EVENT = "EVENT";


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

        //SOUNDS (TONE.mp3)
        public const string DEFAULT_SOUND_BASE64 = "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2iYR8WIw8Q8yhRWqJoIyY2hvRiaYpGRGT2T0mqYI4acXzrl0Ro5NQihRax0TqKNj5IwkbWXSoQoJCciU512Ii0yqS5SJhogKmEC5Wax4smfPWGFsghSaYV05lr20lqp5cbasXEglZPnLR0J4IDxBFNchMY0gQsMIjy5ddzi5KhQoFm0aaYiH2FI7M4cXRHxQdPq0Ob05omEIiVJUXnCGrHGJtMsEkz6aFoiICeipgJxpZpnSo8mbAbhwyweRuSSOKs0zJbjiwWDT0ITppFFjiSXosJycc2dkpIsiRgGRpEFhUuKRShWRIiJ6qGCKCIVIkSIiFRMhrVWauNRQ4iRbHKylhUuFRf/7ksTtAtlmDPikpNfDMcGfAJMkUChjK45LVUJCiRbFUUsxIUUpS2MY+kSLbj7QokSLf6l1kTUkS6FCKVyVnFpoXLQRIkTUvFJCzX6pCSwjRETTQs54s9MKgiCIZghciFRMimFQRRNbSJr1skWxySIVE2EQBhMmhgFgSFOStDUlhUkTIiaaFDGP6qFn+Mf6WIhU1N/KMrq4Z9yUVk0llVlVjpw6cEQhEQyHhsYGxgaGRoZDw2ICMocOljpRdRdSDoZGstitZUdlayygllVlUk1F1Ek0k6uE4Q3NjK6TVWVWKnDpQ6VLFSx0oXKFypYqcXUXUXThPNisnVRkhWKnDpwsQkQhGg+MDYwCwKgsDoHA+IAXBwFgVBYHQOBcQCMHAeBUFgdDw2IBGIDhUsVOFyhGgY3///xldXV5slVk6nC1iIqWOqLqLpLFUk1AwMpsrwFK0wwNTEFNRTMuOTkuNVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVX/+5DE54PYDgbwBJknywW7zoCRp0lVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVU=";


        //BAR CODE STYLE
        public const string BAR_CODE_STYLE_1D = "1D";
        public const string BAR_CODE_STYLE_2D = "2D";

        //PRESENTATION LAYER
        public const string PRESENTATION_LAYER_ACTIVITY_REC_TYPE_USER_DEFINED = "UD";
        public const int PRESENTATION_LAYER_ACTIVITY_REC_TYPE_USER_DEFINED_COLOR = 16776960;   // aqua


        // ACTIVITY
        public const int USER_DEFINED_ACTIVITY_COLOR = 11447902;                    // teal

        // BUILDING / VENUE
        public const string BUILDING_TYPE_BUILDING = "BUILDING";
        public const string BUILDING_TYPE_VENUE = "VENUE";

        //RATE UPDATE UTILITY
        public const int RATE_UPDATE_UTILITY_PENDING_MODIFICATION_COLOR = 12511898;



        // USER TOOLBAR/FAVORITES
        public const string WEBUSER_NOTE_TYPE_FAVORITES = "TOOLBAR";
        public const string WEBUSER_NOTE_TYPE_EMAIL_SIGNATURE = "EMAILSIG";

    }

    public class QuoteOrderCopyRequest
    {
        public string CopyToDealId { get; set; }
        public string LocationId { get; set; }
        public string WarehouseId { get; set; }
        public bool CopyRatesFromInventory { get; set; }
        public bool CopyDates { get; set; }
        public bool CopyLineItemNotes { get; set; }
        public bool CombineSubs { get; set; }
        public bool CopyDocuments { get; set; }
    }

    public class ApplyBottomLineDaysPerWeekRequest
    {
        public string OrderId { get; set; }
        public string PurchaseOrderId { get; set; }
        public string RecType { get; set; }
        public bool? Subs { get; set; }
        public decimal DaysPerWeek { get; set; }
    }

    public class ApplyBottomLineDiscountPercentRequest
    {
        public string OrderId { get; set; }
        public string PurchaseOrderId { get; set; }
        public string RecType { get; set; }
        public bool? Subs { get; set; }
        public decimal DiscountPercent { get; set; }
    }

    public class ApplyBottomLineTotalRequest
    {
        public string OrderId { get; set; }
        public string PurchaseOrderId { get; set; }
        public string RecType { get; set; }
        public bool? Subs { get; set; }
        public string TotalType { get; set; }
        public decimal Total { get; set; }
        public bool? IncludeTaxInTotal { get; set; }
    }


    public enum IncludeExcludeAll { All, IncludeOnly, Exclude };

}
