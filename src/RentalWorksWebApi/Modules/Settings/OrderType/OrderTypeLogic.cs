using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic;
namespace RentalWorksWebApi.Modules.Settings.OrderType
{
    public class OrderTypeLogic : RwBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderTypeRecord orderType = new OrderTypeRecord();
        OrderTypeLoader orderTypeLoader = new OrderTypeLoader();
        public OrderTypeLogic()
        {
            dataRecords.Add(orderType);
            dataLoader = orderTypeLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwBusinessLogicField(isPrimaryKey: true)]
        public string OrderTypeId { get { return orderType.OrderTypeId; } set { orderType.OrderTypeId = value; } }
        [FwBusinessLogicField(isRecordTitle: true)]
        public string OrderType { get { return orderType.OrderType; } set { orderType.OrderType = value; } }
        public string OrdType { get { return orderType.Ordtype; } set { orderType.Ordtype = value; } }
        public string DefaultPickTime { get { return orderType.Picktime; } set { orderType.Picktime = value; } }
        public string DefaultFromTime { get { return orderType.Fromtime; } set { orderType.Fromtime = value; } }
        public string DefaultToTime { get { return orderType.Totime; } set { orderType.Totime = value; } }
        public string DailyScheduleDefaultStartTime { get { return orderType.Defaultdaystarttime; } set { orderType.Defaultdaystarttime = value; } }
        public string DailyScheduleDefaultStopTime { get { return orderType.Defaultdaystoptime; } set { orderType.Defaultdaystoptime = value; } }
        public bool IsMasterSubOrderType { get { return orderType.Ismastersuborder; } set { orderType.Ismastersuborder = value; } }

        //rental fields
        public bool AllowRoundTripRentals { get { return orderType.Roundtriprentals; } set { orderType.Roundtriprentals = value; } }

        //sales fields
        public bool SalesInventoryPrice { get { return orderType.Selectsalesprice; } set { orderType.Selectsalesprice = value; } }
        public bool SalesInventoryCost { get { return orderType.Selectsalescost; } set { orderType.Selectsalescost = value; } }

        //facilities fields
        public string FacilityDescription { get { return orderType.Spacedescription; } set { orderType.Spacedescription = value; } }

        //transportation fields
        //crew fields

        public bool HideCrewBreaks { get { return orderType.Hidecrewbreaks; } set { orderType.Hidecrewbreaks = value; } }
        public bool Break1Paid { get { return orderType.Break1paId; } set { orderType.Break1paId = value; } }
        public bool Break2Paid { get { return orderType.Break2paId; } set { orderType.Break2paId = value; } }
        public bool Break3Paid { get { return orderType.Break3paId; } set { orderType.Break3paId = value; } }


        //misc fields





        public bool AddInstallationAndStrikeFee { get { return orderType.Installstrikefee; } set { orderType.Installstrikefee = value; } }
        public string InstallationAndStrikeFeeRateId { get { return orderType.InstallstrikemasterId; } set { orderType.InstallstrikemasterId = value; } }
        public decimal? InstallationAndStrikeFeePercent { get { return orderType.Installstrikepct; } set { orderType.Installstrikepct = value; } }
        public string InstallationAndStrikeFeeBasedOn { get { return orderType.Installstrikebasedon; } set { orderType.Installstrikebasedon = value; } }
        public bool AddManagementAndServiceFee { get { return orderType.Managementservicefee; } set { orderType.Managementservicefee = value; } }
        public string ManagementAndServiceFeeRateId { get { return orderType.ManagementservicemasterId; } set { orderType.ManagementservicemasterId = value; } }
        public decimal? ManagementAndServiceFeePercent { get { return orderType.Managementservicepct; } set { orderType.Managementservicepct = value; } }
        public string ManagementAndServiceFeeBasedOn { get { return orderType.Managementservicebasedon; } set { orderType.Managementservicebasedon = value; } }


        public string DefaultUsedSalePrice { get { return orderType.Selectrentalsaleprice; } set { orderType.Selectrentalsaleprice = value; } }


        public bool QuikPayDiscount { get { return orderType.Quikpaydiscount; } set { orderType.Quikpaydiscount = value; } }
        public string QuikPayDiscountType { get { return orderType.Quikpaydiscounttype; } set { orderType.Quikpaydiscounttype = value; } }
        public int? QuikPayDiscountDays { get { return orderType.Quikpaydiscountdays; } set { orderType.Quikpaydiscountdays = value; } }
        public decimal? QuikPayDiscountPercent { get { return orderType.Quikpaydiscountpct; } set { orderType.Quikpaydiscountpct = value; } }
        public bool QuikPayDiscountExcludeSubs { get { return orderType.Quikpayexcludesubs; } set { orderType.Quikpayexcludesubs = value; } }


        public bool QuikConfirmDiscount { get { return orderType.Quikconfirmdiscount; } set { orderType.Quikconfirmdiscount = value; } }
        public decimal? QuikConfirmDiscountPercent { get { return orderType.Quikconfirmdiscountpct; } set { orderType.Quikconfirmdiscountpct = value; } }
        public int? QuikConfirmDiscountDays { get { return orderType.Quikconfirmdiscountdays; } set { orderType.Quikconfirmdiscountdays = value; } }


        public bool DisableCostGl { get { return orderType.Disablecostgl; } set { orderType.Disablecostgl = value; } }
        public bool ExcludeFromTopSalesDashboard { get { return orderType.Excludefromtopsales; } set { orderType.Excludefromtopsales = value; } }


        //public bool Combineactivitytabs { get { return orderType.Combineactivitytabs; } set { orderType.Combineactivitytabs = value; } }
        //public bool Combinetabseparateitems { get { return orderType.Combinetabseparateitems; } set { orderType.Combinetabseparateitems = value; } }
        //public string Suborderbillby { get { return orderType.Suborderbillby; } set { orderType.Suborderbillby = value; } }
        //public string Suborderavailabilityrule { get { return orderType.Suborderavailabilityrule; } set { orderType.Suborderavailabilityrule = value; } }
        //public string Suborderorderqty { get { return orderType.Suborderorderqty; } set { orderType.Suborderorderqty = value; } }
        //public string SuborderdefaultordertypeId { get { return orderType.SuborderdefaultordertypeId; } set { orderType.SuborderdefaultordertypeId = value; } }
        //public string SuborderordertypefieldsId { get { return orderType.SuborderordertypefieldsId; } set { orderType.SuborderordertypefieldsId = value; } }
        //public string Invoiceclass { get { return orderType.Invoiceclass; } set { orderType.Invoiceclass = value; } }
        //public string RentalordertypefieldsId { get { return orderType.RentalordertypefieldsId; } set { orderType.RentalordertypefieldsId = value; } }
        //public string SalesordertypefieldsId { get { return orderType.SalesordertypefieldsId; } set { orderType.SalesordertypefieldsId = value; } }
        //public string SpaceordertypefieldsId { get { return orderType.SpaceordertypefieldsId; } set { orderType.SpaceordertypefieldsId = value; } }
        //public string SubrentalordertypefieldsId { get { return orderType.SubrentalordertypefieldsId; } set { orderType.SubrentalordertypefieldsId = value; } }
        //public string SubsalesordertypefieldsId { get { return orderType.SubsalesordertypefieldsId; } set { orderType.SubsalesordertypefieldsId = value; } }
        //public string PurchaseordertypefieldsId { get { return orderType.PurchaseordertypefieldsId; } set { orderType.PurchaseordertypefieldsId = value; } }
        //public string LaborordertypefieldsId { get { return orderType.LaborordertypefieldsId; } set { orderType.LaborordertypefieldsId = value; } }
        //public string SublaborordertypefieldsId { get { return orderType.SublaborordertypefieldsId; } set { orderType.SublaborordertypefieldsId = value; } }
        //public string MiscordertypefieldsId { get { return orderType.MiscordertypefieldsId; } set { orderType.MiscordertypefieldsId = value; } }
        //public string SubmiscordertypefieldsId { get { return orderType.SubmiscordertypefieldsId; } set { orderType.SubmiscordertypefieldsId = value; } }
        //public string RepairordertypefieldsId { get { return orderType.RepairordertypefieldsId; } set { orderType.RepairordertypefieldsId = value; } }
        //public string VehicleordertypefieldsId { get { return orderType.VehicleordertypefieldsId; } set { orderType.VehicleordertypefieldsId = value; } }
        //public string RentalsaleordertypefieldsId { get { return orderType.RentalsaleordertypefieldsId; } set { orderType.RentalsaleordertypefieldsId = value; } }
        //public string LdordertypefieldsId { get { return orderType.LdordertypefieldsId; } set { orderType.LdordertypefieldsId = value; } }

        public decimal? Orderby { get { return orderType.Orderby; } set { orderType.Orderby = value; } }
        public bool Inactive { get { return orderType.Inactive; } set { orderType.Inactive = value; } }
        public string DateStamp { get { return orderType.DateStamp; } set { orderType.DateStamp = value; } }
        //------------------------------------------------------------------------------------ 
    }
} 
