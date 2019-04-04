using FwStandard.Models;
using FwStandard.SqlServer;

namespace WebLibrary
{
    public static class RwGlobals
    {
        //QUOTE/ORDER LINE-ITEMS
        public static string COMPLETE_COLOR { get; set; }
        public static string KIT_COLOR { get; set; }
        public static string MISCELLANEOUS_COLOR { get; set; }
        public static string ITEM_COLOR { get; set; }
        public static string ACCESSORY_COLOR { get; set; }
        public static string CONTAINER_COLOR { get; set; }
        public static string SUB_COLOR { get; set; }
        public static string CONSIGNMENT_COLOR { get; set; }


        //QUOTE/ORDER 
        public static string QUOTE_ORDER_LOCKED_COLOR { get; set; }
        public static string QUOTE_ORDER_ON_HOLD_COLOR { get; set; }
        public static string QUOTE_RESERVED_COLOR { get; set; }
        public static string QUOTE_ORDER_NO_CHARGE_COLOR { get; set; }
        public static string QUOTE_ORDER_MULTI_WAREHOUSE_COLOR { get; set; }
        public static string QUOTE_REQUEST_COLOR { get; set; }
        public static string ORDER_LATE_COLOR { get; set; }
        public static string ORDER_REPAIR_COLOR { get; set; }
        public static string ORDER_LOSS_AND_DAMAGE_COLOR { get; set; }

        //RECEIPT
        public static string RECEIPT_RECTYPE_OVERPAYMENT_COLOR { get; set; }
        public static string RECEIPT_RECTYPE_DEPLETING_DEPOSIT_COLOR { get; set; }
        public static string RECEIPT_RECTYPE_REFUND_CHECK_COLOR { get; set; }
        public static string RECEIPT_RECTYPE_NSF_ADJUSTMENT_COLOR { get; set; }
        public static string RECEIPT_RECTYPE_WRITE_OFF_COLOR { get; set; }
        public static string RECEIPT_RECTYPE_CREDIT_MEMO_COLOR { get; set; }


        //CONTACTS
        public static string COMPANY_TYPE_LEAD_COLOR { get; set; }
        public static string COMPANY_TYPE_PROSPECT_COLOR { get; set; }
        public static string COMPANY_TYPE_CUSTOMER_COLOR { get; set; }
        public static string COMPANY_TYPE_DEAL_COLOR { get; set; }
        public static string COMPANY_TYPE_VENDOR_COLOR { get; set; }


        //CURRENCY
        public static string FOREIGN_CURRENCY_COLOR { get; set; }

        //PURCHASE ORDER
        public static string PO_DROP_SHIP_COLOR { get; set; }
        public static string PO_NEEDS_APPROVAL_COLOR { get; set; }
        public static string PO_ITEMS_NEED_BARCODE_COLOR { get; set; }
        public static string PO_ITEMS_IN_HOLDING_COLOR { get; set; }

        //INVENTORY
        public static string INVENTORY_AVAILABLE_FOR_SALE_COLOR { get; set; }
        public static string INVENTORY_AVAILABLE_FOR_PARTS_COLOR { get; set; }

        //STAGING / CHECK-OUT
        public static string STAGING_PENDING_ITEMS_MISSING_COLOR { get; set; }


        //CONTAINER
        public static string CONTAINER_READY_COLOR { get; set; }
        public static string CONTAINER_INCOMPLETE_COLOR { get; set; }


        //GLOBAL
        public static string NEGATIVE_VARIANCE_COLOR { get; set; }





        //---------------------------------------------------------------------------
        //this gets called one time at system startup
        //can be called when events occur in the system that should change global colors
        public static void SetGlobalColors(SqlServerConfig databaseSettings)
        {

            COMPLETE_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.COMPLETE_COLOR);
            KIT_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.KIT_COLOR);
            MISCELLANEOUS_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.MISCELLANEOUS_COLOR);
            ITEM_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.ITEM_COLOR);
            ACCESSORY_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.ACCESSORY_COLOR);

            INVENTORY_AVAILABLE_FOR_SALE_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.INVENTORY_AVAILABLE_FOR_SALE_COLOR);
            INVENTORY_AVAILABLE_FOR_PARTS_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.INVENTORY_AVAILABLE_FOR_PARTS_COLOR);




            int containerColorInt = 0;
            using (FwSqlConnection conn = new FwSqlConnection(databaseSettings.ConnectionString))
            {
                containerColorInt = FwConvert.ToInt32(FwSqlCommand.GetDataAsync(conn, databaseSettings.QueryTimeout, "rentalstatus", "statustype", RwConstants.INVENTORY_STATUS_TYPE_INCONTAINER, "color").Result.ToString().TrimEnd());
            }
            CONTAINER_COLOR = FwConvert.OleColorToHtmlColor(containerColorInt);
            SUB_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.SUB_COLOR);

            int consignmentColorInt = 0;
            using (FwSqlConnection conn = new FwSqlConnection(databaseSettings.ConnectionString))
            {
                consignmentColorInt = FwConvert.ToInt32(FwSqlCommand.GetDataAsync(conn, databaseSettings.QueryTimeout, "syscontrol", "controlid", "1", "consignmentcolor").Result.ToString().TrimEnd());
            }
            CONSIGNMENT_COLOR = FwConvert.OleColorToHtmlColor(consignmentColorInt);


            QUOTE_ORDER_LOCKED_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.QUOTE_ORDER_LOCKED_COLOR);
            QUOTE_ORDER_ON_HOLD_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.QUOTE_ORDER_ON_HOLD_COLOR);
            QUOTE_RESERVED_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.QUOTE_RESERVED_COLOR);
            QUOTE_ORDER_NO_CHARGE_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.QUOTE_ORDER_NO_CHARGE_COLOR);
            QUOTE_ORDER_MULTI_WAREHOUSE_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.QUOTE_ORDER_MULTI_WAREHOUSE_COLOR);
            QUOTE_REQUEST_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.QUOTE_REQUEST_COLOR);
            ORDER_LATE_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.ORDER_LATE_COLOR);
            ORDER_REPAIR_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.ORDER_REPAIR_COLOR);
            ORDER_LOSS_AND_DAMAGE_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.ORDER_LOSS_AND_DAMAGE_COLOR);



            COMPANY_TYPE_LEAD_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.COMPANY_TYPE_LEAD_COLOR);
            COMPANY_TYPE_PROSPECT_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.COMPANY_TYPE_PROSPECT_COLOR);
            COMPANY_TYPE_CUSTOMER_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.COMPANY_TYPE_CUSTOMER_COLOR);
            COMPANY_TYPE_DEAL_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.COMPANY_TYPE_DEAL_COLOR);
            COMPANY_TYPE_VENDOR_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.COMPANY_TYPE_VENDOR_COLOR);

            FOREIGN_CURRENCY_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.FOREIGN_CURRENCY_COLOR);

            PO_DROP_SHIP_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.PO_DROP_SHIP_COLOR);
            PO_NEEDS_APPROVAL_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.PO_NEEDS_APPROVAL_COLOR);
            PO_ITEMS_NEED_BARCODE_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.PO_ITEMS_NEED_BARCODE_COLOR);
            PO_ITEMS_IN_HOLDING_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.PO_ITEMS_IN_HOLDING_COLOR);


            CONTAINER_READY_COLOR = CONTAINER_COLOR;
            CONTAINER_INCOMPLETE_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.CONTAINER_INCOMPLETE_COLOR);


            RECEIPT_RECTYPE_OVERPAYMENT_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.RECEIPT_RECTYPE_OVERPAYMENT_COLOR);
            RECEIPT_RECTYPE_DEPLETING_DEPOSIT_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.RECEIPT_RECTYPE_DEPLETING_DEPOSIT_COLOR);
            RECEIPT_RECTYPE_REFUND_CHECK_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.RECEIPT_RECTYPE_REFUND_CHECK_COLOR);
            RECEIPT_RECTYPE_NSF_ADJUSTMENT_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.RECEIPT_RECTYPE_NSF_ADJUSTMENT_COLOR);
            RECEIPT_RECTYPE_WRITE_OFF_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.RECEIPT_RECTYPE_WRITE_OFF_COLOR);
            RECEIPT_RECTYPE_CREDIT_MEMO_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.RECEIPT_RECTYPE_CREDIT_MEMO_COLOR);

            NEGATIVE_VARIANCE_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.NEGATIVE_VARIANCE_COLOR);

            STAGING_PENDING_ITEMS_MISSING_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.STAGING_PENDING_ITEMS_MISSING_COLOR);


        }
        //---------------------------------------------------------------------------
    }
}
