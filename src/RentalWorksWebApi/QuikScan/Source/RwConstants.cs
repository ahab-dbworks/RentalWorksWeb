namespace RentalWorksQuikScan.Source
{
    public static class RwConstants
    {
        public static class TrackedBy
        {
            public const string BARCODE    = "BARCODE";
            public const string QUANTITY   = "QUANTITY";
            public const string SERIALNO   = "SERIALNO";
            public const string RFID       = "RFID";
            public const string NONE       = "";
        }

        public static class ItemTypes
        {
            public const string BarCoded    = "BarCoded";
            public const string NonBarCoded = "NonBarCoded";
            public const string None        = "None";
        }

        public static class AvailFrom
        {
            public const string WAREHOUSE = "W";
            public const string CATALOG   = "C";
        }
        
        public static class ModuleTypes
        {
            public const string Order       = "Order";
            public const string SubReceive  = "SubReceive";
            public const string SubReturn   = "SubReturn";
            public const string PhyInv      = "PhyInv";
            public const string Transfer    = "Transfer";
            public const string Truck       = "Truck";
        }

        public static class ActivityTypes
        {
            public const string CheckIn     = "CheckIn";
            public const string Staging     = "Staging";
            public const string SubReceive  = "SubReceive";
            public const string SubReturn   = "SubReturn";
            public const string AssetDisposition = "AssetDisposition";
        }

        public static class CheckInModes
        {
            public const string SingleOrder  = "SingleOrder";
            public const string MultiOrder   = "MultiOrder";
            public const string Session      = "Session";
            public const string Deal         = "Deal";
        }

        public static class Exchange
        {
            public const int EXCHANGE_STATUS_ITEM_DIFFERENT_ICODE_BARCODE        = 601;
            public const int EXCHANGE_STATUS_ITEM_DIFFERENT_ICODE_QUANTITY       = 602;
            public const int EXCHANGE_STATUS_ITEM_DIFFERENT_ICODE_PENDING        = 603;
        }

        public static class RepairModes
        {
            public const string Complete  = "Complete";
            public const string Release   = "Release";
        }

        public static class RentalStatusType
        {
            public const string IN          = "IN";
            public const string INCONTAINER = "INCONTAINER";
            public const string INREPAIR    = "INREPAIR";
            public const string INTRANSIT   = "INTRANSIT";
            public const string LOANEDOUT   = "LOANEDOUT";
            public const string ONTRUCK     = "ONTRUCK";
            public const string OUT         = "OUT";
            public const string RETIRED     = "RETIRED";
            public const string STAGED      = "STAGED";
        }

        public static class OrderType
        {
            public const string QUIK_HOLD      = "H";
            public const string QUOTE          = "Q";
            public const string ORDER          = "O";
            public const string TRANSFER       = "T";
            public const string TRUCK          = "P";
            public const string TEMPLATE       = "M";
            public const string REPAIR         = "R";
            public const string PO             = "C";
            public const string TRUCK_USAGE    = "U";
            public const string LEAD           = "LEAD";
            public const string CONTAINER      = "N";
            public const string PROJECT        = "PROJECT";
            public const string EVENT          = "EVENT";
        }

        public enum OrderModes { Quote, Order, Transfer, Truck, Po, Lead, QuikIn, Container, Event, Any }

        public static class ItemStatus
        {
            public const string IN               = "I";    
            public const string OUT              = "O";
            public const string STAGED           = "S";    
            public const string RETURNED         = "T";    
            public const string UNASSIGNED       = "U";    
            public const string RECONCILED       = "C";    
            public const string RETTOINV         = "V";
            public const string FINISHED         = "X";
            public const string HOLDING          = "H";
            public const string LOST             = "L";
            public const string PENDING_EXCHANGE = "P";
        }
    }
}
