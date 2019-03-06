﻿using FwStandard.Models;
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
        //---------------------------------------------------------------------------

        //CONTACTS
        public static string COMPANY_TYPE_LEAD_COLOR { get; set; }
        public static string COMPANY_TYPE_PROSPECT_COLOR { get; set; }
        public static string COMPANY_TYPE_CUSTOMER_COLOR { get; set; }
        public static string COMPANY_TYPE_DEAL_COLOR { get; set; }
        public static string COMPANY_TYPE_VENDOR_COLOR { get; set; }


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




            int containerColorInt = 0;
            using (FwSqlConnection conn = new FwSqlConnection(databaseSettings.ConnectionString))
            {
                containerColorInt = FwConvert.ToInt32(FwSqlCommand.GetDataAsync(conn, databaseSettings.QueryTimeout, "rentalstatus", "statustype", RwConstants.INVENTORY_STATUS_TYPE_INCONTAINER, "color").Result.ToString().TrimEnd());
            }
            CONTAINER_COLOR = FwConvert.OleColorToHtmlColor(containerColorInt);



            COMPANY_TYPE_LEAD_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.COMPANY_TYPE_LEAD_COLOR);
            COMPANY_TYPE_PROSPECT_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.COMPANY_TYPE_PROSPECT_COLOR);
            COMPANY_TYPE_CUSTOMER_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.COMPANY_TYPE_CUSTOMER_COLOR);
            COMPANY_TYPE_DEAL_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.COMPANY_TYPE_DEAL_COLOR);
            COMPANY_TYPE_VENDOR_COLOR = FwConvert.OleColorToHtmlColor(RwConstants.COMPANY_TYPE_VENDOR_COLOR);

        }
        //---------------------------------------------------------------------------
    }
}
