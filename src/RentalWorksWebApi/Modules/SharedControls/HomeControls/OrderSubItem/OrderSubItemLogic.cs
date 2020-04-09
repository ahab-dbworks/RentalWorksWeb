using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.HomeControls.OrderSubItem
{
    [FwLogic(Id: "KcMdiB7E70zTE")]
    public class OrderSubItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderSubItemLoader orderSubItemLoader = new OrderSubItemLoader();
        public OrderSubItemLogic()
        {
            dataLoader = orderSubItemLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "KcONvaYIAvHnK", IsPrimaryKey: true, IsReadOnly: true)]
        public int? OrderSubItemId { get; set; }
        [FwLogicProperty(Id: "KcS3RCQUDx4EE", IsReadOnly: true)]
        public string OfficeLocationId { get; set; }
        [FwLogicProperty(Id: "KDgfcwyBXkd5q", IsReadOnly: true)]
        public string OrderLocation { get; set; }
        [FwLogicProperty(Id: "kdKpcxb7IkW3O", IsReadOnly: true)]
        public string DepartmentId { get; set; }
        [FwLogicProperty(Id: "KDl4cOrzlfKMN", IsReadOnly: true)]
        public string Department { get; set; }
        [FwLogicProperty(Id: "KDN4PSWFXPipP", IsReadOnly: true)]
        public string CustomerId { get; set; }
        [FwLogicProperty(Id: "kE1JsmFriQOuQ", IsReadOnly: true)]
        public string Customer { get; set; }
        [FwLogicProperty(Id: "keAVqbjA6jtZK", IsReadOnly: true)]
        public string DealId { get; set; }
        [FwLogicProperty(Id: "KeBUxb24xZjkB", IsReadOnly: true)]
        public string Deal { get; set; }
        [FwLogicProperty(Id: "KF9WKmshIYoUs", IsReadOnly: true)]
        public string DealNumber { get; set; }
        [FwLogicProperty(Id: "KFtPPukHqktPg", IsReadOnly: true)]
        public string OrderId { get; set; }
        [FwLogicProperty(Id: "KFVs3b8p25QUr", IsReadOnly: true)]
        public string OrderNumber { get; set; }
        [FwLogicProperty(Id: "kgnNIu8acBtxY", IsReadOnly: true)]
        public string OrderDescription { get; set; }
        [FwLogicProperty(Id: "KgUK6wQXGXJbo", IsReadOnly: true)]
        public string OrderNumberAndDescription { get; set; }
        [FwLogicProperty(Id: "KHYkieKzBO1HY", IsReadOnly: true)]
        public string OrderDate { get; set; }
        [FwLogicProperty(Id: "KIpw2I0Xl1h0g", IsReadOnly: true)]
        public string Status { get; set; }
        [FwLogicProperty(Id: "KiXCitm9CG9KH", IsReadOnly: true)]
        public string OrderAgentId { get; set; }
        [FwLogicProperty(Id: "KIzZ6juKZWd04", IsReadOnly: true)]
        public string OrderAgent { get; set; }
        [FwLogicProperty(Id: "Kj3yZC7mnP2Lm", IsReadOnly: true)]
        public string OrderEstimatedStartDate { get; set; }
        [FwLogicProperty(Id: "kKisw8AzfgwpG", IsReadOnly: true)]
        public string OrderEstimatedStopDate { get; set; }
        [FwLogicProperty(Id: "KkLpI2gpaY1x7", IsReadOnly: true)]
        public string OrderEstimatedDateRange { get; set; }
        [FwLogicProperty(Id: "KKvdaAVTgOmEI", IsReadOnly: true)]
        public string OrderBillingStartDate { get; set; }
        [FwLogicProperty(Id: "klBZClvpIJyOs", IsReadOnly: true)]
        public string OrderBillingPeriodEnd { get; set; }
        [FwLogicProperty(Id: "klLlsoyhacpk4", IsReadOnly: true)]
        public string OrderRateType { get; set; }
        [FwLogicProperty(Id: "kmtgb9YYX6OfN", IsReadOnly: true)]
        public string OrderHiatusDiscountFrom { get; set; }
        [FwLogicProperty(Id: "KnFuHGKN1A3yl", IsReadOnly: true)]
        public string OrderBillingCycleId { get; set; }
        [FwLogicProperty(Id: "KNhjD2TOth73K", IsReadOnly: true)]
        public string OrderBillingCycleType { get; set; }
        [FwLogicProperty(Id: "KNLKHIQaJ7Pe3", IsReadOnly: true)]
        public string OrderItemId { get; set; }
        [FwLogicProperty(Id: "KnlRuB6YKygl9", IsReadOnly: true)]
        public string NestedOrderItemId { get; set; }
        [FwLogicProperty(Id: "knvZW57lZcTOB", IsReadOnly: true)]
        public string ICode { get; set; }
        [FwLogicProperty(Id: "kOJY16tDn2shc", IsReadOnly: true)]
        public string RecType { get; set; }
        [FwLogicProperty(Id: "KoRpBDcfLOvJV", IsReadOnly: true)]
        public string Description { get; set; }
        [FwLogicProperty(Id: "KPJgNZRdpS7RK", IsReadOnly: true)]
        public decimal? SubQuantity { get; set; }
        [FwLogicProperty(Id: "KqA491FeXxqsn", IsReadOnly: true)]
        public string ItemClass { get; set; }
        [FwLogicProperty(Id: "kQBN1j2b8OXt3", IsReadOnly: true)]
        public string InventoryId { get; set; }
        [FwLogicProperty(Id: "kr2ojMNg0IHAl", IsReadOnly: true)]
        public string WarehouseId { get; set; }
        [FwLogicProperty(Id: "kRBD5mJxnMp3z", IsReadOnly: true)]
        public string ReturnToWarehouseId { get; set; }
        [FwLogicProperty(Id: "KrEuXHdPrdf7y", IsReadOnly: true)]
        public bool? IsRecurring { get; set; }
        [FwLogicProperty(Id: "kRFPtUtqjrukv", IsReadOnly: true)]
        public bool? IsProrateWeeks { get; set; }
        [FwLogicProperty(Id: "KSMV0Qn6QlNlp", IsReadOnly: true)]
        public bool? IsOrderRecurringRateType { get; set; }
        [FwLogicProperty(Id: "ksRvR3SSlYIwP", IsReadOnly: true)]
        public decimal? OrderRate { get; set; }
        [FwLogicProperty(Id: "kSxcl8bhBOKe2", IsReadOnly: true)]
        public decimal? OrderDaysPerWeek { get; set; }
        [FwLogicProperty(Id: "kSYKYfi4QBaXj", IsReadOnly: true)]
        public decimal? OrderDiscountPercent { get; set; }
        [FwLogicProperty(Id: "KT4pZBHii1OCF", IsReadOnly: true)]
        public decimal? OrderQuantityOrdered { get; set; }
        [FwLogicProperty(Id: "ku0v2ZNnk2bU6", IsReadOnly: true)]
        public decimal? OrderQuantityCoefficient { get; set; }
        [FwLogicProperty(Id: "KU4tmObc2ntez", IsReadOnly: true)]
        public decimal? OrderWeeklyExtended { get; set; }
        [FwLogicProperty(Id: "kul0UCosmjnYX", IsReadOnly: true)]
        public decimal? OrderMonthlyExtended { get; set; }
        [FwLogicProperty(Id: "kuLFZsjo4X0PB", IsReadOnly: true)]
        public decimal? OrderBillablePeriods { get; set; }
        [FwLogicProperty(Id: "kUmwJl3QrDa1Q", IsReadOnly: true)]
        public decimal? OrderPeriodExtended { get; set; }
        [FwLogicProperty(Id: "KUnFaG6kpmOkE", IsReadOnly: true)]
        public string PurchaseOrderId { get; set; }
        [FwLogicProperty(Id: "KUVdPdo0vAZCp", IsReadOnly: true)]
        public string PuchaseOrderItemId { get; set; }
        [FwLogicProperty(Id: "kv6l9KWaoNCI3", IsReadOnly: true)]
        public string PurchaseOrderNumber { get; set; }
        [FwLogicProperty(Id: "kvPX4ABxF0867", IsReadOnly: true)]
        public string PurchaseOrderDescription { get; set; }
        [FwLogicProperty(Id: "kVzMs4scyMkxI", IsReadOnly: true)]
        public string PurchaseOrderNumberAndDescription { get; set; }
        [FwLogicProperty(Id: "KW6JCvFWaklJH", IsReadOnly: true)]
        public string PurchaseOrderStatus { get; set; }
        [FwLogicProperty(Id: "kwDWQSS9sja3w", IsReadOnly: true)]
        public string PurchaseOrderDate { get; set; }
        [FwLogicProperty(Id: "Kwm7KtqE4M8Pf", IsReadOnly: true)]
        public string PurchaseOrderEstimatedStartDate { get; set; }
        [FwLogicProperty(Id: "kWzxrvqMNO6eB", IsReadOnly: true)]
        public string PurchaseOrderEstimatedStopDate { get; set; }
        [FwLogicProperty(Id: "KX2iJt7lshBJU", IsReadOnly: true)]
        public string PurchaseOrderEstimatedDateRange { get; set; }
        [FwLogicProperty(Id: "kxl9wEZJpi0jo", IsReadOnly: true)]
        public string PurchaseOrderBillingStartDate { get; set; }
        [FwLogicProperty(Id: "KXnznrmubNmWl", IsReadOnly: true)]
        public string PurchaseOrderBillingEndDate { get; set; }
        [FwLogicProperty(Id: "KYb386Q6opM72", IsReadOnly: true)]
        public string PurchaseOrderRateType { get; set; }
        [FwLogicProperty(Id: "KYxxq1hqkomxm", IsReadOnly: true)]
        public string PurchaseOrderHiatusDiscountFrom { get; set; }
        [FwLogicProperty(Id: "kZCOC2jjVsa98", IsReadOnly: true)]
        public string PurchaseOrderBillingCycleId { get; set; }
        [FwLogicProperty(Id: "KZpA6ElPc0e8F", IsReadOnly: true)]
        public string PurchaseOrderBillingCycleType { get; set; }
        [FwLogicProperty(Id: "L0lIACMcUPIjW", IsReadOnly: true)]
        public string PurchaseOrderClassificationId { get; set; }
        [FwLogicProperty(Id: "L1wcviIDsORy9", IsReadOnly: true)]
        public string PurchaseOrderClassification { get; set; }
        [FwLogicProperty(Id: "L2V0gYm3uUwnC", IsReadOnly: true)]
        public string VendorId { get; set; }
        [FwLogicProperty(Id: "L35dJOBTungKk", IsReadOnly: true)]
        public string VendorNumber { get; set; }
        [FwLogicProperty(Id: "l4jeiVoPeXQRH", IsReadOnly: true)]
        public string Vendor { get; set; }
        [FwLogicProperty(Id: "L4KdcPX6mJ1zd", IsReadOnly: true)]
        public bool? IsPurchaseOrderRecurringRateType { get; set; }
        [FwLogicProperty(Id: "l5vA5pUkOZjvU", IsReadOnly: true)]
        public decimal? PurchaseOrderRate { get; set; }
        [FwLogicProperty(Id: "L5vxeuc0xpprR", IsReadOnly: true)]
        public decimal? PurchaseOrderDaysPerWeek { get; set; }
        [FwLogicProperty(Id: "l64gdginzhrmi", IsReadOnly: true)]
        public decimal? PurchaseOrderDiscountPercent { get; set; }
        [FwLogicProperty(Id: "L6iyZam6PyYKO", IsReadOnly: true)]
        public decimal? PurchaseOrderQuantityOrdered { get; set; }
        [FwLogicProperty(Id: "l6xxJrBwA0qGY", IsReadOnly: true)]
        public decimal? PurchaseOrderQuantityCoefficient { get; set; }
        [FwLogicProperty(Id: "l7A4yDLNwedWT", IsReadOnly: true)]
        public decimal? PurchaseOrderWeeklyExtended { get; set; }
        [FwLogicProperty(Id: "L88yYbTE9P5AD", IsReadOnly: true)]
        public decimal? PurchaseOrderMonthlyExtended { get; set; }
        [FwLogicProperty(Id: "LAaJNkM59lgj3", IsReadOnly: true)]
        public decimal? PurchaseOrderBillablePeriods { get; set; }
        [FwLogicProperty(Id: "laANYrjqTL2X4", IsReadOnly: true)]
        public decimal? PurchaseOrderPeriodExtended { get; set; }
        [FwLogicProperty(Id: "LaTJKjaKrFH0b", IsReadOnly: true)]
        public decimal? Profit { get; set; }
        [FwLogicProperty(Id: "lBaS12J8MTm9N", IsReadOnly: true)]
        public decimal? MarkupPercent { get; set; }
        [FwLogicProperty(Id: "lBlP0Ng1aEnrT", IsReadOnly: true)]
        public decimal? MarginPercent { get; set; }
        [FwLogicProperty(Id: "lDcFFgd4i17O8", IsReadOnly: true)]
        public string ReceiveDate { get; set; }
        [FwLogicProperty(Id: "LdKd7YAKMFiBz", IsReadOnly: true)]
        public bool? IsReceiveDateEstimated { get; set; }
        [FwLogicProperty(Id: "LDN1pgVQMgjS8", IsReadOnly: true)]
        public bool? IsReceiveSuspended { get; set; }
        [FwLogicProperty(Id: "LDqP2ur3EdEVV", IsReadOnly: true)]
        public string OutDate { get; set; }
        [FwLogicProperty(Id: "Ldw276eePRFnc", IsReadOnly: true)]
        public bool? IsOutDateEstimated { get; set; }
        [FwLogicProperty(Id: "lE1YCvU5gij3X", IsReadOnly: true)]
        public bool? IsOutSuspended { get; set; }
        [FwLogicProperty(Id: "Lfrn7frVXS0Zv", IsReadOnly: true)]
        public string InDate { get; set; }
        [FwLogicProperty(Id: "LfYDiJilMZqwI", IsReadOnly: true)]
        public bool? IsInDateEstimated { get; set; }
        [FwLogicProperty(Id: "lHd1ucgIzQiBH", IsReadOnly: true)]
        public bool? IsInSuspended { get; set; }
        [FwLogicProperty(Id: "LheQ5iTCv8rXV", IsReadOnly: true)]
        public bool? IsLost { get; set; }
        [FwLogicProperty(Id: "LHLQGaKU8Nq6T", IsReadOnly: true)]
        public string ReturnDate { get; set; }
        [FwLogicProperty(Id: "LIaAIB0WQwXZN", IsReadOnly: true)]
        public bool? IsReturnDateEstimated { get; set; }
        [FwLogicProperty(Id: "lIG8PWzciRUyE", IsReadOnly: true)]
        public bool? IsReturnSuspended { get; set; }
        [FwLogicProperty(Id: "lINKUG8JstBvO", IsReadOnly: true)]
        public string ItemOrder { get; set; }
        [FwLogicProperty(Id: "LiPtknvpi5INN", IsReadOnly: true)]
        public string OrderBy { get; set; }
        [FwLogicProperty(Id: "LIsHlWsD9Uwta", IsReadOnly: true)]
        public bool? Bold { get; set; }
        [FwLogicProperty(Id: "LItZVlPfLyAbR", IsReadOnly: true)]
        public bool? OptionColor { get; set; }
        [FwLogicProperty(Id: "lixfdiv470b6c", IsReadOnly: true)]
        public string ActualReceiveDate { get; set; }
        [FwLogicProperty(Id: "LIYCai0MsCeMh", IsReadOnly: true)]
        public string ActualOutDate { get; set; }
        [FwLogicProperty(Id: "LkCDHyllY0NEU", IsReadOnly: true)]
        public string ActualInDate { get; set; }
        [FwLogicProperty(Id: "LklqjaFSCIPUY", IsReadOnly: true)]
        public string ActualReturnDate { get; set; }
        [FwLogicProperty(Id: "lKQuM2Fk9UZnh", IsReadOnly: true)]
        public string BillingDates { get; set; }
        [FwLogicProperty(Id: "lMRK0juEcjJeW", IsReadOnly: true)]
        public string ReceiveContractBillingDate { get; set; }
        [FwLogicProperty(Id: "lO97i3fxn1aj2", IsReadOnly: true)]
        public string OutContractBillingDate { get; set; }
        [FwLogicProperty(Id: "LogNGKi7rLDuz", IsReadOnly: true)]
        public string InContractBillingDaet { get; set; }
        [FwLogicProperty(Id: "lOH8b5mAQEalO", IsReadOnly: true)]
        public string ReturnContractBillingDate { get; set; }
        [FwLogicProperty(Id: "LoQQRZ9KSQxpy", IsReadOnly: true)]
        public string Type { get; set; }
        [FwLogicProperty(Id: "LPtYVfgFWunnt", IsReadOnly: true)]
        public string ConsignorId { get; set; }
        [FwLogicProperty(Id: "LPUba3RVcRgUH", IsReadOnly: true)]
        public string ConsignorAgreementId { get; set; }
        [FwLogicProperty(Id: "LQQGS5XbU46Sv", IsReadOnly: true)]
        public string OrderBillablePeriodsFromDate { get; set; }
        [FwLogicProperty(Id: "lQZqcgJGOuueb", IsReadOnly: true)]
        public string OrderBillablePeriodsToDate { get; set; }
        [FwLogicProperty(Id: "lR60bS7SHYpUj", IsReadOnly: true)]
        public string PurchaseOrderBillablePeriodsFromDate { get; set; }
        [FwLogicProperty(Id: "lRhyiSpFa7H7G", IsReadOnly: true)]
        public string PurchaseOrderBillablePeriodsToDate { get; set; }
        //------------------------------------------------------------------------------------ 
    }
}
