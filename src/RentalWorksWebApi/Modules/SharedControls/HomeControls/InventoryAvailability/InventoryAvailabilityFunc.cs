using System;
using FwStandard.Models;
using FwStandard.SqlServer;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using WebApi.Logic;
using System.Collections.Concurrent;
using WebApi.Modules.Settings.AvailabilityKeepFreshLog;
using System.Globalization;
using WebApi.Modules.Settings.SystemSettings.AvailabilitySettings;

//#jhtodo: note, userSession is not used in this file, but is still part of many method signatures for future use

//#jhtodo: these Warehouse settings need to be implemented:
//       Late:        AvailabilityLateHours
//       Conflicts:   AvailabilityPreserveConflicts
//       Consignment: AvailabilityExcludeConsigned, AvailabilityRequireConsignedReserved
//       QC:          AvailabilityEnableQcDelay, AvailabilityQcDelayExcludeWeekend, AvailabilityQcDelayExcludeHoliday, AvailabilityQcDelayIndefinite


namespace WebApi.Modules.HomeControls.InventoryAvailability
{
    //-------------------------------------------------------------------------------------------------------
    public class AvailabilityRecalcRequest
    {
        public string InventoryId { get; set; }
        public string WarehouseId { get; set; }
        public string Classification { get; set; }
    }
    //-------------------------------------------------------------------------------------------------------
    public class AvailabilityInventoryWarehouseRequest
    {
        public string InventoryId { get; set; }
        public string WarehouseId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool RefreshIfNeeded { get; set; }
    }
    //-------------------------------------------------------------------------------------------------------
    public class AvailabilityCalendarAndScheduleRequest
    {
        public string InventoryId { get; set; }
        public List<string> WarehouseId { get; set; } = new List<string>();
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool? IncludeHours { get; set; } = false;
        public bool? YearView { get; set; } = false;
        public string SortReservationsBy { get; set; } = "";  // OrderNumber (default), Start, End, *AvailabilityPriority (not yet implemented)
    }
    //-------------------------------------------------------------------------------------------------------
    public class AvailabilityConflictRequest
    {
        public DateTime? ToDate { get; set; }
        public string AvailableFor { get; set; }  // R, S, blank
        public string ConflictType { get; set; }  // P, N, blank
        public string WarehouseId { get; set; }
        public string InventoryTypeId { get; set; }
        public string CategoryId { get; set; }
        public string SubCategoryId { get; set; }
        public string InventoryId { get; set; }
        public string Description { get; set; }
        public string OrderId { get; set; }
        public string DealId { get; set; }
        public SelectedCheckBoxListItems Ranks { get; set; } = new SelectedCheckBoxListItems();
    }
    //-------------------------------------------------------------------------------------------------------
    public class AvailabilityConflictResponseItem
    {
        public string Warehouse { get; set; }
        public string WarehouseCode { get; set; }
        public string InventoryTypeId { get; set; }
        public string InventoryType { get; set; }
        public string CategoryId { get; set; }
        public string Category { get; set; }
        public string SubCategoryId { get; set; }
        public string SubCategory { get; set; }
        public string InventoryId { get; set; }
        public string ICode { get; set; }
        public string ItemDescription { get; set; }
        public string OrderId { get; set; }
        public string OrderType { get; set; }
        public string OrderTypeDescription { get { return AppFunc.GetOrderTypeDescription(OrderType); } }
        public string OrderNumber { get; set; }
        public string OrderDescription { get; set; }
        public string DealId { get; set; }
        public string Deal { get; set; }
        public float? QuantityReserved { get; set; }
        public float? QuantitySub { get; set; }
        public float? QuantityAvailable { get; set; }
        public string AvailabilityState { get; set; } = RwConstants.AVAILABILITY_STATE_STALE;
        public bool AvailabilityIsStale { get; set; }
        public float? QuantityLate { get; set; }
        public float? QuantityIn { get; set; }
        public float? QuantityQc { get; set; }
        public float? QuantityInRepair { get; set; }
        public DateTime? FromDateTime { get; set; }
        public DateTime? ToDateTime { get; set; }
        public string FromDateTimeString { get; set; }
        public string ToDateTimeString { get; set; }
    }
    //-------------------------------------------------------------------------------------------------------
    public class AvailabilityConflictResponse : List<AvailabilityConflictResponseItem> { }
    //-------------------------------------------------------------------------------------------------------
    public class TAvailabilityProgress
    {
        public int PercentComplete { get; set; } = 0;
    }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryWarehouseAvailabilityRequestItem
    {
        public string InventoryId { get; set; } = "";
        public string WarehouseId { get; set; } = "";
        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }
        public TInventoryWarehouseAvailabilityRequestItem(string inventoryId, string warehouseId, DateTime fromDateTime, DateTime toDateTime)
        {
            this.InventoryId = inventoryId;
            this.WarehouseId = warehouseId;

            if (fromDateTime.Equals(DateTime.MinValue))
            {
                fromDateTime = DateTime.Today;
            }

            if (toDateTime.Equals(DateTime.MinValue))
            {
                toDateTime = InventoryAvailabilityFunc.LateDateTime;
            }

            this.FromDateTime = fromDateTime;
            this.ToDateTime = toDateTime;
        }
    }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryWarehouseAvailabilityRequestItems : List<TInventoryWarehouseAvailabilityRequestItem> { }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryWarehouseAvailabilityKey
    {
        public TInventoryWarehouseAvailabilityKey(string inventoryId, string warehouseId)
        {
            this.InventoryId = inventoryId;
            this.WarehouseId = warehouseId;
        }
        public string InventoryId { get; set; } = "";
        public string WarehouseId { get; set; } = "";
        public string CombinedKey { get { return InventoryId + "-" + WarehouseId; } }
        public override string ToString()
        {
            return CombinedKey;
        }

        public override bool Equals(object obj)
        {
            bool isEqual = false;
            if (obj is TInventoryWarehouseAvailabilityKey)
            {
                TInventoryWarehouseAvailabilityKey testObj = (TInventoryWarehouseAvailabilityKey)obj;
                isEqual = testObj.CombinedKey.Equals(CombinedKey);
            }
            return isEqual;

        }
        public override int GetHashCode()
        {
            return CombinedKey.GetHashCode();
        }

    }
    //-------------------------------------------------------------------------------------------------------
    public class AvailabilityKeyEqualityComparer : IEqualityComparer<TInventoryWarehouseAvailabilityKey>
    {
        public bool Equals(TInventoryWarehouseAvailabilityKey k1, TInventoryWarehouseAvailabilityKey k2)
        {
            return ((k1.InventoryId.Equals(k2.InventoryId)) && (k1.WarehouseId.Equals(k2.WarehouseId)));
        }
        public int GetHashCode(TInventoryWarehouseAvailabilityKey k)
        {
            return k.CombinedKey.GetHashCode();
        }
    }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryWarehouseAvailabilityKeys : List<TInventoryWarehouseAvailabilityKey> { }
    //-------------------------------------------------------------------------------------------------------
    public struct TInventoryWarehouseAvailabilityQuantity
    {
        public float Owned;//{ get; set; } = 0;
        public float Subbed;// { get; set; } = 0;
        public float Consigned;// { get; set; } = 0;
        public float OwnedAndConsigned { get { return Owned + Consigned; } }
        //public float OwnedAndConsignedX() { return Owned + Consigned; }
        public float Total { get { return Owned + Subbed + Consigned; } }
        //public float TotalX() { return Owned + Subbed + Consigned; }


        //public void CloneFrom(TInventoryWarehouseAvailabilityQuantity source)
        //{
        //    this.Owned = source.Owned;
        //    this.Subbed = source.Subbed;
        //    this.Consigned = source.Consigned;
        //}

        public static TInventoryWarehouseAvailabilityQuantity MakeCopy(TInventoryWarehouseAvailabilityQuantity source)
        {
            TInventoryWarehouseAvailabilityQuantity newQ = new TInventoryWarehouseAvailabilityQuantity();
            newQ.Owned = source.Owned;
            newQ.Subbed = source.Subbed;
            newQ.Consigned = source.Consigned;
            return newQ;
        }

        public static TInventoryWarehouseAvailabilityQuantity operator +(TInventoryWarehouseAvailabilityQuantity q1, TInventoryWarehouseAvailabilityQuantity q2)
        {
            TInventoryWarehouseAvailabilityQuantity q3 = new TInventoryWarehouseAvailabilityQuantity();
            q3.Owned = q1.Owned + q2.Owned;
            q3.Subbed = q1.Subbed + q2.Subbed;
            q3.Consigned = q1.Consigned + q2.Consigned;
            return q3;
        }

        public static TInventoryWarehouseAvailabilityQuantity operator -(TInventoryWarehouseAvailabilityQuantity q1, TInventoryWarehouseAvailabilityQuantity q2)
        {
            TInventoryWarehouseAvailabilityQuantity q3 = new TInventoryWarehouseAvailabilityQuantity();
            q3.Owned = q1.Owned - q2.Owned;
            q3.Subbed = q1.Subbed - q2.Subbed;
            q3.Consigned = q1.Consigned - q2.Consigned;
            return q3;
        }
    }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryWarehouseAvailabilityReservation
    {
        public string InventoryId { get; set; }
        public string WarehouseId { get; set; }
        public string WarehouseCode { get; set; }
        public string Warehouse { get; set; }
        public string ReturnToWarehouseId { get; set; }
        public string ReturnToWarehouseCode { get; set; }
        public string ReturnToWarehouse { get; set; }
        public string OrderId { get; set; }  // can also be a RepairId
        public string OrderItemId { get; set; }
        public string RecType { get; set; }
        public string OrderType { get; set; }
        public string OrderTypeDescription { get { return AppFunc.GetOrderTypeDescription(OrderType); } }
        public string OrderNumber { get; set; }
        public string OrderDescription { get; set; }
        public string OrderStatus { get; set; }
        public string DepartmentId { get; set; }
        public string Department { get; set; }
        public string DealId { get; set; }
        public string Deal { get; set; }
        public DateTime FromDateTime { get; set; }
        public string FromDateTimeDisplay
        {
            get
            {
                string display = FwConvert.ToShortDate(FromDateTime);
                return display;
            }
        }
        public DateTime ToDateTime { get; set; }
        public string ToDateTimeDisplay
        {
            get
            {
                string display = FwConvert.ToShortDate(ToDateTime);
                //if (ToDateTime.Equals(InventoryAvailabilityFunc.LateDateTime))
                if (Late)
                {
                    display = "No End Date (Late)";
                }
                return display;
            }
        }
        public bool Late { get; set; }
        public bool LateButReturning { get; set; }
        public bool QcRequired { get; set; }
        public bool EnableQcDelay { get; set; }
        public int QcDelayDays { get; set; }
        public bool QcDelayExcludeWeekend { get; set; }
        public bool QcDelayExcludeHoliday { get; set; }
        public bool QcDelayIndefinite { get; set; }
        public DateTime? QcDelayFromDateTime { get; set; }
        public DateTime? QcDelayToDateTime { get; set; }
        public float QcQuantity { get; set; }
        public string ContainerBarCode { get; set; }
        public bool AvailableWhileInContainer { get; set; }
        public string ContractId { get; set; }
        public float QuantityOrdered { get; set; } = 0;
        public float QuantitySub { get; set; } = 0;
        public float QuantityConsigned { get; set; } = 0;
        public TInventoryWarehouseAvailabilityQuantity QuantityReserved;// { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity QuantityStaged;// { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity QuantityOut;// { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity QuantityIn;// { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity QuantityInRepair;// { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity QuantityLate;// { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public bool IsPositiveConflict { get; set; } = false;
        public bool IsNegativeConflict { get; set; } = false;
        public string SubPurchaseOrderId { get; set; }
        public string SubPurchaseOrderNumber { get; set; }
        public string SubPurchaseOrderDescription { get; set; }
        public string SubPurchaseOrderVendor { get; set; }
        [JsonIgnore]
        public bool countedReserved = false;  // used only while calculating future availability
        [JsonIgnore]
        public bool countedLate = false;  // used only while calculating future availability
        [JsonIgnore]
        public bool countedAvailableInContainer = false;  // used only while calculating future availability
        [JsonIgnore]
        public bool IsTransfer
        {
            get
            {
                bool isTran = false;

                if ((OrderType.Equals(RwConstants.ORDER_TYPE_TRANSFER)) || ((!string.IsNullOrEmpty(ReturnToWarehouseId)) && (!ReturnToWarehouseId.Equals(WarehouseId))))
                {
                    isTran = true;
                }

                return isTran;
            }
        }
        [JsonIgnore]
        public bool IsContainer
        {
            get
            {
                bool isContain = false;

                if (OrderType.Equals(RwConstants.ORDER_TYPE_CONTAINER))
                {
                    isContain = true;
                }

                return isContain;
            }
        }
        [JsonIgnore]
        public bool IsPendingExchange
        {
            get
            {
                return (OrderType.Equals(RwConstants.ORDER_TYPE_PENDING_EXCHANGE));
            }
        }
        [JsonIgnore]
        public string ScheduleResourceDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(OrderTypeDescription);
                sb.Append(" ");
                sb.Append(OrderNumber);
                return sb.ToString();
            }
        }
        [JsonIgnore]
        public string ReservationDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (!string.IsNullOrEmpty(OrderNumber))
                {
                    sb.Append(OrderNumber);
                    sb.Append(" ");
                }
                sb.Append(OrderDescription);
                if (!string.IsNullOrEmpty(Deal))
                {
                    sb.Append(" (");
                    sb.Append(Deal);
                    sb.Append(")");
                }
                if (IsTransfer)
                {
                    sb.Append(" (");
                    sb.Append(WarehouseCode);
                    sb.Append(" > ");
                    sb.Append(ReturnToWarehouseCode);
                    sb.Append(")");
                }
                if (!string.IsNullOrEmpty(SubPurchaseOrderId))
                {
                    sb.Append(" - PO ");
                    sb.Append(SubPurchaseOrderNumber);
                    if (!string.IsNullOrEmpty(SubPurchaseOrderVendor))
                    {
                        sb.Append(" (");
                        sb.Append(SubPurchaseOrderVendor);
                        sb.Append(")");
                    }
                }
                if (IsContainer)
                {
                    if (!string.IsNullOrEmpty(ContainerBarCode))
                    {
                        sb.Append(" ");
                        sb.Append(ContainerBarCode);
                    }
                }
                return sb.ToString();
            }
        }
        public void CloneFrom(TInventoryWarehouseAvailabilityReservation source)
        {
            this.WarehouseId = source.WarehouseId;
            this.WarehouseCode = source.WarehouseCode;
            this.Warehouse = source.Warehouse;
            this.ReturnToWarehouseId = source.ReturnToWarehouseId;
            this.ReturnToWarehouseCode = source.ReturnToWarehouseCode;
            this.ReturnToWarehouse = source.ReturnToWarehouse;
            this.OrderId = source.OrderId;
            this.OrderItemId = source.OrderItemId;
            this.OrderType = source.OrderType;
            this.OrderNumber = source.OrderNumber;
            this.OrderDescription = source.OrderDescription;
            this.OrderStatus = source.OrderStatus;
            this.DepartmentId = source.DepartmentId;
            this.Department = source.Department;
            this.DealId = source.DealId;
            this.Deal = source.Deal;
            this.FromDateTime = source.FromDateTime;
            this.ToDateTime = source.ToDateTime;
            this.Late = source.Late;
            this.LateButReturning = source.LateButReturning;
            this.QcRequired = source.QcRequired;
            this.EnableQcDelay = source.EnableQcDelay;
            this.QcDelayDays = source.QcDelayDays;
            this.QcDelayExcludeWeekend = source.QcDelayExcludeWeekend;
            this.QcDelayExcludeHoliday = source.QcDelayExcludeHoliday;
            this.QcDelayIndefinite = source.QcDelayIndefinite;
            this.QcDelayFromDateTime = source.QcDelayFromDateTime;
            this.QcDelayToDateTime = source.QcDelayToDateTime;
            this.QcQuantity = source.QcQuantity;
            this.ContainerBarCode = source.ContainerBarCode;
            this.AvailableWhileInContainer = source.AvailableWhileInContainer;
            this.ContractId = source.ContractId;
            this.QuantityOrdered = source.QuantityOrdered;
            this.QuantitySub = source.QuantitySub;
            this.QuantityConsigned = source.QuantityConsigned;
            //this.QuantityReserved.CloneFrom(source.QuantityReserved);
            //this.QuantityStaged.CloneFrom(source.QuantityStaged);
            //this.QuantityOut.CloneFrom(source.QuantityOut);
            //this.QuantityIn.CloneFrom(source.QuantityIn);
            //this.QuantityInRepair.CloneFrom(source.QuantityInRepair);
            //this.QuantityLate.CloneFrom(source.QuantityLate);
            //this.QuantityReserved.CloneFrom(source.QuantityReserved);

            this.QuantityReserved = TInventoryWarehouseAvailabilityQuantity.MakeCopy(source.QuantityReserved);
            this.QuantityStaged = TInventoryWarehouseAvailabilityQuantity.MakeCopy(source.QuantityStaged);
            this.QuantityOut = TInventoryWarehouseAvailabilityQuantity.MakeCopy(source.QuantityOut);
            this.QuantityIn = TInventoryWarehouseAvailabilityQuantity.MakeCopy(source.QuantityIn);
            this.QuantityInRepair = TInventoryWarehouseAvailabilityQuantity.MakeCopy(source.QuantityInRepair);
            this.QuantityLate = TInventoryWarehouseAvailabilityQuantity.MakeCopy(source.QuantityLate);
            this.QuantityReserved = TInventoryWarehouseAvailabilityQuantity.MakeCopy(source.QuantityReserved);

            this.IsPositiveConflict = source.IsPositiveConflict;
            this.IsNegativeConflict = source.IsNegativeConflict;
            this.SubPurchaseOrderId = source.SubPurchaseOrderId;
            this.SubPurchaseOrderNumber = source.SubPurchaseOrderNumber;
            this.SubPurchaseOrderDescription = source.SubPurchaseOrderDescription;
            this.SubPurchaseOrderVendor = source.SubPurchaseOrderVendor;
        }

    }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryWarehouseAvailabilityDateTime
    {
        public TInventoryWarehouseAvailabilityDateTime(DateTime availabilityDateTime)
        {
            this.AvailabilityDateTime = availabilityDateTime;
        }
        public DateTime AvailabilityDateTime { get; set; }
        public TInventoryWarehouseAvailabilityQuantity Available { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity Reserved { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity Returning { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity BecomingAvailable { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public List<TInventoryWarehouseAvailabilityReservation> Reservations { get; set; } = new List<TInventoryWarehouseAvailabilityReservation>();

        public void CloneFrom(TInventoryWarehouseAvailabilityDateTime source)
        {
            this.AvailabilityDateTime = source.AvailabilityDateTime;
            //this.Available.CloneFrom(source.Available);
            //this.BecomingAvailable.CloneFrom(source.BecomingAvailable);
            //this.Reserved.CloneFrom(source.Reserved);
            //this.Returning.CloneFrom(source.Returning);
            this.Available = TInventoryWarehouseAvailabilityQuantity.MakeCopy(source.Available);
            this.BecomingAvailable = TInventoryWarehouseAvailabilityQuantity.MakeCopy(source.BecomingAvailable);
            this.Reserved = TInventoryWarehouseAvailabilityQuantity.MakeCopy(source.Reserved);
            this.Returning = TInventoryWarehouseAvailabilityQuantity.MakeCopy(source.Returning);
            this.Reservations = new List<TInventoryWarehouseAvailabilityReservation>();

            foreach (TInventoryWarehouseAvailabilityReservation res in source.Reservations)
            {
                TInventoryWarehouseAvailabilityReservation res2 = new TInventoryWarehouseAvailabilityReservation();
                res2.CloneFrom(res);
                this.Reservations.Add(res2);
            }
        }
    }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryWarehouseAvailabilityMinimum
    {
        public TInventoryWarehouseAvailabilityQuantity MinimumAvailable { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public DateTime? FirstConfict { get; set; }
        public bool NoAvailabilityCheck { get; set; }
        public bool IsStale { get; set; }
        public string AvailabilityState { get; set; } = RwConstants.AVAILABILITY_STATE_STALE;
        public string Color { get; set; }
        public string TextColor { get; set; }
    }
    //-------------------------------------------------------------------------------------------------------
    public class TPackageAccessory
    {
        public TPackageAccessory(string inventoryId, float defaultQuantity)
        {
            this.InventoryId = inventoryId;
            this.DefaultQuantity = defaultQuantity;
        }
        public string InventoryId { get; set; } = "";
        public float DefaultQuantity { get; set; } = 0;
    }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryWarehouse
    {
        public TInventoryWarehouse(string inventoryId, string warehouseId)
        {
            this.InventoryId = inventoryId;
            this.WarehouseId = warehouseId;
        }
        public string InventoryTypeId { get; set; } = "";
        public string InventoryType { get; set; } = "";
        public string CategoryId { get; set; } = "";
        public string Category { get; set; } = "";
        public string SubCategoryId { get; set; } = "";
        public string SubCategory { get; set; } = "";
        public string InventoryId { get; set; } = "";
        public string WarehouseId { get; set; } = "";
        public string AvailableFor { get; set; } = "";
        public string ICode { get; set; } = "";
        public string Description { get; set; } = "";
        public string WarehouseCode { get; set; } = "";
        public string Warehouse { get; set; } = "";
        public string Classification { get; set; } = "";
        public bool HourlyAvailability { get; set; } = false;
        public bool NoAvailabilityCheck { get; set; } = false;
        public int LowAvailabilityPercent { get; set; } = 0;
        public int LowAvailabilityQuantity { get; set; } = 0;
        public string DailyRate { get; set; }
        public string WeeklyRate { get; set; }
        public string Week2Rate { get; set; }
        public string Week3Rate { get; set; }
        public string Week4Rate { get; set; }
        public string MonthlyRate { get; set; }
        public List<TPackageAccessory> Accessories { get; set; } = new List<TPackageAccessory>();

        public string CombinedKey { get { return InventoryId + "-" + WarehouseId; } }
        public override string ToString()
        {
            return CombinedKey;
        }

        public void CloneFrom(TInventoryWarehouse source)
        {
            InventoryTypeId = source.InventoryTypeId;
            InventoryType = source.InventoryType;
            CategoryId = source.CategoryId;
            Category = source.Category;
            SubCategoryId = source.SubCategoryId;
            SubCategory = source.SubCategory;
            InventoryId = source.InventoryId;
            WarehouseId = source.WarehouseId;
            AvailableFor = source.AvailableFor;
            ICode = source.ICode;
            Description = source.Description;
            WarehouseCode = source.WarehouseCode;
            Warehouse = source.Warehouse;
            Classification = source.Classification;
            HourlyAvailability = source.HourlyAvailability;
            NoAvailabilityCheck = source.NoAvailabilityCheck;
            LowAvailabilityPercent = source.LowAvailabilityPercent;
            LowAvailabilityQuantity = source.LowAvailabilityQuantity;
            DailyRate = source.DailyRate;
            WeeklyRate = source.WeeklyRate;
            Week2Rate = source.Week2Rate;
            Week3Rate = source.Week3Rate;
            Week4Rate = source.Week4Rate;
            MonthlyRate = source.MonthlyRate;
            //public List<TPackageAccessory> Accessories { get; set; } = new List<TPackageAccessory>();

        }

        public override bool Equals(object obj)
        {
            bool isEqual = false;
            if (obj is TInventoryWarehouse)
            {
                TInventoryWarehouse testObj = (TInventoryWarehouse)obj;
                isEqual = testObj.CombinedKey.Equals(CombinedKey);
            }
            return isEqual;

        }
        public override int GetHashCode()
        {
            return CombinedKey.GetHashCode();
        }
    }
    //-------------------------------------------------------------------------------------------------------
    public class InventoryWarehouseEqualityComparer : IEqualityComparer<TInventoryWarehouse>
    {
        public bool Equals(TInventoryWarehouse k1, TInventoryWarehouse k2)
        {
            return ((k1.InventoryId.Equals(k2.InventoryId)) && (k1.WarehouseId.Equals(k2.WarehouseId)));
        }
        public int GetHashCode(TInventoryWarehouse k)
        {
            return k.CombinedKey.GetHashCode();
        }
    }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryWarehouseAvailability
    {
        public TInventoryWarehouseAvailability(string inventoryId, string warehouseId)
        {
            this.InventoryWarehouse = new TInventoryWarehouse(inventoryId, warehouseId);
        }
        public TInventoryWarehouse InventoryWarehouse { get; set; }
        public DateTime CalculatedDateTime { get; set; }
        public TInventoryWarehouseAvailabilityQuantity Total;// { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity In;// { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity Staged;// { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity Out;// { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity InRepair;// { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity InTransit;// { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity InContainer;// { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity OnTruck;// { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity QcRequired;// { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public TInventoryWarehouseAvailabilityQuantity Late;// { get; set; } = new TInventoryWarehouseAvailabilityQuantity();
        public DateTime AvailDataFromDateTime { get; set; }
        public DateTime AvailDataToDateTime { get; set; }
        public bool EnableQcDelay { get; set; }
        public int QcDelayDays { get; set; }
        public DateTime QcToDateTime { get; set; }
        public List<TInventoryWarehouseAvailabilityReservation> Reservations { get; set; } = new List<TInventoryWarehouseAvailabilityReservation>();
        public ConcurrentDictionary<DateTime, TInventoryWarehouseAvailabilityDateTime> AvailabilityDatesAndTimes { get; set; } = new ConcurrentDictionary<DateTime, TInventoryWarehouseAvailabilityDateTime>();
        public bool HasPositiveConflict { get; set; } = false;
        public bool HasNegativeConflict { get; set; } = false;

        //-------------------------------------------------------------------------------------------------------
        public void CloneFrom(TInventoryWarehouseAvailability source)
        {
            this.InventoryWarehouse.CloneFrom(source.InventoryWarehouse);
            this.AvailDataFromDateTime = source.AvailDataFromDateTime;
            this.AvailDataToDateTime = source.AvailDataToDateTime;
            //this.Total.CloneFrom(source.Total);
            //this.In.CloneFrom(source.In);
            //this.Staged.CloneFrom(source.Staged);
            //this.Out.CloneFrom(source.Out);
            //this.InRepair.CloneFrom(source.InRepair);
            //this.InTransit.CloneFrom(source.InTransit);
            //this.InContainer.CloneFrom(source.InContainer);
            //this.OnTruck.CloneFrom(source.OnTruck);
            //this.QcRequired.CloneFrom(source.QcRequired);
            //this.Late.CloneFrom(source.Late);

            this.Total = TInventoryWarehouseAvailabilityQuantity.MakeCopy(source.Total);
            this.In = TInventoryWarehouseAvailabilityQuantity.MakeCopy(source.In);
            this.Staged = TInventoryWarehouseAvailabilityQuantity.MakeCopy(source.Staged);
            this.Out = TInventoryWarehouseAvailabilityQuantity.MakeCopy(source.Out);
            this.InRepair = TInventoryWarehouseAvailabilityQuantity.MakeCopy(source.InRepair);
            this.InTransit = TInventoryWarehouseAvailabilityQuantity.MakeCopy(source.InTransit);
            this.InContainer = TInventoryWarehouseAvailabilityQuantity.MakeCopy(source.InContainer);
            this.OnTruck = TInventoryWarehouseAvailabilityQuantity.MakeCopy(source.OnTruck);
            this.QcRequired = TInventoryWarehouseAvailabilityQuantity.MakeCopy(source.QcRequired);
            this.Late = TInventoryWarehouseAvailabilityQuantity.MakeCopy(source.Late);

            this.EnableQcDelay = source.EnableQcDelay;
            this.QcDelayDays = source.QcDelayDays;
            this.QcToDateTime = source.QcToDateTime;
            this.CalculatedDateTime = source.CalculatedDateTime;

            this.Reservations = new List<TInventoryWarehouseAvailabilityReservation>();
            this.Reservations.Clear();
            foreach (TInventoryWarehouseAvailabilityReservation reservation in source.Reservations)
            {
                this.Reservations.Add(reservation);
            }

            this.AvailabilityDatesAndTimes = new ConcurrentDictionary<DateTime, TInventoryWarehouseAvailabilityDateTime>();
            this.AvailabilityDatesAndTimes.Clear();
            foreach (KeyValuePair<DateTime, TInventoryWarehouseAvailabilityDateTime> date in source.AvailabilityDatesAndTimes)
            {
                TInventoryWarehouseAvailabilityDateTime dt = new TInventoryWarehouseAvailabilityDateTime(date.Key);
                dt.CloneFrom(date.Value);
                this.AvailabilityDatesAndTimes.TryAdd(date.Key, dt);
            }
            this.HasNegativeConflict = source.HasNegativeConflict;
            this.HasPositiveConflict = source.HasPositiveConflict;
        }
        //-------------------------------------------------------------------------------------------------------
        public TInventoryWarehouseAvailabilityMinimum GetMinimumAvailableQuantity(DateTime fromDateTime, DateTime toDateTime, float additionalQuantity = 0)
        {
            TInventoryWarehouseAvailabilityMinimum minAvail = new TInventoryWarehouseAvailabilityMinimum();
            bool isStale = false;
            bool isHistory = false;
            minAvail.AvailabilityState = RwConstants.AVAILABILITY_STATE_STALE;

            minAvail.NoAvailabilityCheck = InventoryWarehouse.NoAvailabilityCheck;

            DateTime currentAvailabilityDateTime = (InventoryWarehouse.HourlyAvailability ? InventoryAvailabilityFunc.GetCurrentAvailabilityHour() : DateTime.Today);

            if (!InventoryWarehouse.HourlyAvailability)
            {
                fromDateTime = fromDateTime.Date;
                toDateTime = toDateTime.Date;
            }


            //if (fromDateTime < DateTime.Today)
            //{
            //    fromDateTime = DateTime.Today;
            //}
            if (fromDateTime < currentAvailabilityDateTime)
            {
                fromDateTime = currentAvailabilityDateTime;
            }

            if (toDateTime.Equals(InventoryAvailabilityFunc.LateDateTime))
            {
                toDateTime = AvailDataToDateTime;
            }
            //else if (toDateTime < DateTime.Today)
            //{
            //    toDateTime = DateTime.Today;
            //    isHistory = true;
            //}
            else if (toDateTime < currentAvailabilityDateTime)
            {
                toDateTime = currentAvailabilityDateTime;
                isHistory = true;
            }


            if (minAvail.NoAvailabilityCheck)
            {
                minAvail.AvailabilityState = RwConstants.AVAILABILITY_STATE_NO_AVAILABILITY_CHECK;
                minAvail.Color = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_NO_AVAILABILITY);
                minAvail.TextColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_TEXT_COLOR_NEEDRECALC);
            }
            else if (isHistory)
            {
                minAvail.Color = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_HISTORICAL_DATE);
                minAvail.TextColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_TEXT_COLOR_HISTORICAL_DATE);
                minAvail.AvailabilityState = RwConstants.AVAILABILITY_STATE_HISTORY;
            }
            else
            {
                bool firstDateFound = false;
                bool lastDateFound = false;

                DateTime theDateTime = fromDateTime;
                while (theDateTime <= toDateTime)
                {
                    TInventoryWarehouseAvailabilityDateTime inventoryWarehouseAvailabilityDateTime = null;
                    if (AvailabilityDatesAndTimes.TryGetValue(theDateTime, out inventoryWarehouseAvailabilityDateTime))
                    {
                        TInventoryWarehouseAvailabilityQuantity avail = new TInventoryWarehouseAvailabilityQuantity();
                        //avail.CloneFrom(inventoryWarehouseAvailabilityDateTime.Available);
                        avail = TInventoryWarehouseAvailabilityQuantity.MakeCopy(inventoryWarehouseAvailabilityDateTime.Available);

                        avail.Owned = avail.Owned - additionalQuantity;
                        if (theDateTime.Equals(fromDateTime))
                        {
                            firstDateFound = true;
                            minAvail.MinimumAvailable = avail;
                        }
                        minAvail.MinimumAvailable = (minAvail.MinimumAvailable.OwnedAndConsigned < avail.OwnedAndConsigned) ? minAvail.MinimumAvailable : avail;

                        if ((minAvail.FirstConfict == null) && (minAvail.MinimumAvailable.OwnedAndConsigned < 0))
                        {
                            minAvail.FirstConfict = theDateTime;
                        }

                        if (theDateTime.Equals(toDateTime))
                        {
                            lastDateFound = true;
                        }
                    }
                    //theDateTime = theDateTime.AddDays(1);        //currently hard-coded for "daily" availability.  will need mods to work for "hourly"  //#jhtodo

                    if (InventoryWarehouse.HourlyAvailability)
                    {
                        theDateTime = theDateTime.AddHours(1);
                    }
                    else
                    {
                        theDateTime = theDateTime.AddDays(1);
                    }

                }


                if (!isStale)
                {
                    isStale = InventoryAvailabilityFunc.InventoryWarehouseNeedsRecalc(InventoryWarehouse.InventoryId, InventoryWarehouse.WarehouseId);
                }

                if (!isStale)
                {
                    if ((!firstDateFound) || (!lastDateFound))
                    {
                        isStale = true;
                    }
                }
                minAvail.IsStale = isStale;

                minAvail.Color = null;
                minAvail.TextColor = FwConvert.OleColorToHtmlColor(0); //black

                if (minAvail.IsStale)
                {
                    minAvail.AvailabilityState = RwConstants.AVAILABILITY_STATE_STALE;
                    minAvail.Color = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_NEEDRECALC);
                    minAvail.TextColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_TEXT_COLOR_NEEDRECALC);
                }
                else if (minAvail.MinimumAvailable.OwnedAndConsigned < 0)
                {
                    minAvail.AvailabilityState = RwConstants.AVAILABILITY_STATE_NEGATIVE;
                    minAvail.Color = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_COLOR_NEGATIVE);
                    minAvail.TextColor = FwConvert.OleColorToHtmlColor(RwConstants.AVAILABILITY_TEXT_COLOR_NEGATIVE);
                }
                else if (minAvail.MinimumAvailable.OwnedAndConsigned == 0)
                {
                    minAvail.AvailabilityState = RwConstants.AVAILABILITY_STATE_ZERO;
                }
                else if ((minAvail.MinimumAvailable.OwnedAndConsigned >= 0) && (InventoryWarehouse.LowAvailabilityPercent != 0) && (minAvail.MinimumAvailable.OwnedAndConsigned <= InventoryWarehouse.LowAvailabilityQuantity))
                {
                    minAvail.AvailabilityState = RwConstants.AVAILABILITY_STATE_LOW;
                }
                else if (minAvail.MinimumAvailable.OwnedAndConsigned > 0)
                {
                    minAvail.AvailabilityState = RwConstants.AVAILABILITY_STATE_ENOUGH;
                }
            }

            return minAvail;
        }
        //-------------------------------------------------------------------------------------------------------
        public static TInventoryWarehouseAvailability operator +(TInventoryWarehouseAvailability avail1, TInventoryWarehouseAvailability avail2)
        {
            TInventoryWarehouseAvailability avail3 = new TInventoryWarehouseAvailability(avail1.InventoryWarehouse.InventoryId, avail1.InventoryWarehouse.WarehouseId);
            avail3.CloneFrom(avail1);

            avail3.InventoryWarehouse.WarehouseId = avail1.InventoryWarehouse.WarehouseId + "," + avail2.InventoryWarehouse.WarehouseId;
            avail3.InventoryWarehouse.WarehouseCode = avail1.InventoryWarehouse.WarehouseCode + ", " + avail2.InventoryWarehouse.WarehouseCode;
            avail3.InventoryWarehouse.Warehouse = avail1.InventoryWarehouse.Warehouse + ", " + avail2.InventoryWarehouse.Warehouse;
            avail3.InventoryWarehouse.NoAvailabilityCheck = (avail1.InventoryWarehouse.NoAvailabilityCheck || avail2.InventoryWarehouse.NoAvailabilityCheck);
            avail3.AvailDataFromDateTime = avail2.AvailDataFromDateTime;
            avail3.AvailDataToDateTime = avail2.AvailDataToDateTime;
            avail3.CalculatedDateTime = avail2.CalculatedDateTime;
            avail3.EnableQcDelay = (avail3.EnableQcDelay || avail2.EnableQcDelay);
            avail3.HasNegativeConflict = (avail3.HasNegativeConflict || avail2.HasNegativeConflict);
            avail3.HasPositiveConflict = (avail3.HasPositiveConflict || avail2.HasPositiveConflict);
            avail3.In = (avail3.In + avail2.In);
            avail3.InContainer = (avail3.InContainer + avail2.InContainer);
            avail3.InRepair = (avail3.InRepair + avail2.InRepair);
            avail3.InTransit = (avail3.InTransit + avail2.InTransit);
            avail3.Late = (avail3.Late + avail2.Late);
            avail3.OnTruck = (avail3.OnTruck + avail2.OnTruck);
            avail3.Out = (avail3.Out + avail2.Out);
            avail3.Staged = (avail3.Staged + avail2.Staged);
            avail3.Total = (avail3.Total + avail2.Total);

            foreach (KeyValuePair<DateTime, TInventoryWarehouseAvailabilityDateTime> invWhDateTime2 in avail2.AvailabilityDatesAndTimes)
            {
                TInventoryWarehouseAvailabilityDateTime invWhDateTime3 = null;
                if (!avail3.AvailabilityDatesAndTimes.TryGetValue(invWhDateTime2.Key, out invWhDateTime3))
                {
                    invWhDateTime3 = new TInventoryWarehouseAvailabilityDateTime(invWhDateTime2.Key);
                }
                invWhDateTime3.Available += invWhDateTime2.Value.Available;
                invWhDateTime3.BecomingAvailable += invWhDateTime2.Value.BecomingAvailable;
                invWhDateTime3.Reserved += invWhDateTime2.Value.Reserved;
                invWhDateTime3.Returning += invWhDateTime2.Value.Returning;
                //invWhDateTime3.Reservations?

                avail3.AvailabilityDatesAndTimes.AddOrUpdate(invWhDateTime3.AvailabilityDateTime, invWhDateTime3, (key, existingValue) =>
                {
                    existingValue.CloneFrom(invWhDateTime3);
                    return existingValue;
                });
            }

            avail3.QcDelayDays = (avail3.QcDelayDays > avail2.QcDelayDays ? avail3.QcDelayDays : avail2.QcDelayDays);
            avail3.QcRequired = avail3.QcRequired + avail2.QcRequired;
            //avail3.QcToDateTime?
            //avail3.Reservations?

            return avail3;
        }
    }
    //-------------------------------------------------------------------------------------------------------
    public class TAvailabilityCache : ConcurrentDictionary<TInventoryWarehouseAvailabilityKey, TInventoryWarehouseAvailability>
    {
        public TAvailabilityCache() : base(new AvailabilityKeyEqualityComparer()) { }
    }
    //-------------------------------------------------------------------------------------------------------
    public class TAvailabilityNeedRecalcMetaData
    {
        public TAvailabilityNeedRecalcMetaData() { }
        public TAvailabilityNeedRecalcMetaData(string classification, bool preCache)
        {
            this.classification = classification;
            this.preCache = preCache;
        }
        public string classification = "";
        public bool preCache = false;
    }
    //-------------------------------------------------------------------------------------------------------
    public class TAvailabilityNeedRecalcDictionary : ConcurrentDictionary<TInventoryWarehouseAvailabilityKey, TAvailabilityNeedRecalcMetaData>
    {
        public TAvailabilityNeedRecalcDictionary() : base(new AvailabilityKeyEqualityComparer()) { }
    }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryNeedingAvailDictionary : ConcurrentDictionary<TInventoryWarehouseAvailabilityKey, string>
    {
        public TInventoryNeedingAvailDictionary() : base(new AvailabilityKeyEqualityComparer()) { }
    }
    //-------------------------------------------------------------------------------------------------------
    public class TInventoryAvailabilityCalendarDate
    {
        public DateTime TheDate { get; set; }
        public List<TInventoryWarehouseAvailabilityReservation> Reservations { get; set; } = new List<TInventoryWarehouseAvailabilityReservation>();
        public TInventoryAvailabilityCalendarDate(DateTime theDate, List<TInventoryWarehouseAvailabilityReservation> reservations)
        {
            this.TheDate = theDate;
            foreach (TInventoryWarehouseAvailabilityReservation reservation in reservations)
            {
                TInventoryWarehouseAvailabilityReservation newReservation = new TInventoryWarehouseAvailabilityReservation();
                newReservation.CloneFrom(reservation);
                this.Reservations.Add(newReservation);
            }
        }
    }
    //------------------------------------------------------------------------------------ 
    public class TInventoryAvailabilityCalendarData
    {
        public bool exists { get; set; } = false;
        public string caption { get; set; }
        public float? qty { get; set; }
        public string backColor { get; set; }
        public string barColor { get; set; }
        public string textColor { get; set; }
    }
    //------------------------------------------------------------------------------------ 
    public class TInventoryAvailabilityCalendarEvent
    {
        public string InventoryId { get; set; }
        public string WarehouseId { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string text { get; set; }
        public string backColor { get; set; }
        public string textColor { get; set; }
        public string id { get; set; } = "";
        public string resource { get; set; }
    }
    //------------------------------------------------------------------------------------ 
    public class TInventoryAvailabilityScheduleResource
    {
        public string name { get; set; } = "";
        public string id { get; set; } = "";
        public string backColor { get; set; } // this will color the background of the resource "name" cell, not the entire row
    }
    //------------------------------------------------------------------------------------ 
    public class TInventoryAvailabilityScheduleEvent
    {
        public string InventoryId { get; set; }
        public string WarehouseId { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string startdisplay { get; set; }
        public string enddisplay { get; set; }
        public string text { get; set; }
        public string total { get; set; } = "";
        public string backColor { get; set; }
        public string barColor { get; set; }
        public string textColor { get; set; }
        public string id { get; set; } = "";
        public string resource { get; set; }
        public string orderId { get; set; }
        public string orderNumber { get; set; }
        public string orderType { get; set; }
        public string orderStatus { get; set; }
        public string orderDescription { get; set; }
        public string deal { get; set; }
        public string subPoNumber { get; set; }
        public string subPoVendor { get; set; }
        public string contractId { get; set; }
        public bool isWarehouseTotal { get; set; } = false;
        public bool isGrandTotal { get; set; } = false;
    }
    //------------------------------------------------------------------------------------ 
    public class TInventoryAvailabilityCalendarAndScheduleResponse
    {
        public TInventoryWarehouseAvailability InventoryData { get; set; }
        public List<TInventoryAvailabilityCalendarDate> Dates { get; set; } = new List<TInventoryAvailabilityCalendarDate>();
        public List<TInventoryAvailabilityCalendarEvent> InventoryAvailabilityCalendarEvents { get; set; } = new List<TInventoryAvailabilityCalendarEvent>();
        public List<TInventoryAvailabilityScheduleResource> InventoryAvailabilityScheduleResources { get; set; } = new List<TInventoryAvailabilityScheduleResource>();
        public List<TInventoryAvailabilityScheduleEvent> InventoryAvailabilityScheduleEvents { get; set; } = new List<TInventoryAvailabilityScheduleEvent>();
    }

    //------------------------------------------------------------------------------------ 
    //------------------------------------------------------------------------------------ 
    //------------------------------------------------------------------------------------ 
    //------------------------------------------------------------------------------------ 

    public static class InventoryAvailabilityFunc
    {
        //-------------------------------------------------------------------------------------------------------
        //private const int AVAILABILITY_DAYS_TO_CACHE = 30;
        //-------------------------------------------------------------------------------------------------------
        private static TAvailabilityNeedRecalcDictionary AvailabilityNeedRecalc = new TAvailabilityNeedRecalcDictionary();
        private static int LastNeedRecalcId = 0;
        public static int availabilityDaysToCache = 0;
        public static DateTime LateDateTime = DateTime.MaxValue;  // This is a temporary value.  Actual value gets set in InitializeService
        public static DateTime TransferAvailabilityToDateTime = DateTime.MinValue;  // This is a temporary value.  Actual value gets set in InitializeService
        private static TAvailabilityCache AvailabilityCache = new TAvailabilityCache();
        private static bool hourlyInitialized = false;
        private static bool dailyInitialized = false;
        private static List<string> ActiveWarehouseIds = new List<string>();

        //-------------------------------------------------------------------------------------------------------
        public static async Task<bool> InitializeService(FwApplicationConfig appConfig)
        {
            bool success = true;

            AvailabilitySettingsLogic availSettings = new AvailabilitySettingsLogic();
            availSettings.SetDependencies(appConfig, null);
            availSettings.ControlId = RwConstants.CONTROL_ID;
            await availSettings.LoadAsync<AvailabilitySettingsLogic>();
            availabilityDaysToCache = availSettings.DaysToCache.GetValueOrDefault(0);

            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select needrecalcid = max(a.id)");
                qry.Add(" from  tmpavailneedrecalc a");
                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();
                LastNeedRecalcId = FwConvert.ToInt32(dt.Rows[0][dt.GetColumnNo("needrecalcid")]);
            }

            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select latedatetime = dbo.funcmaxavaildate() ");
                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();
                LateDateTime = FwConvert.ToDateTime(dt.Rows[0][dt.GetColumnNo("latedatetime")].ToString());
            }

            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                ActiveWarehouseIds = new List<string>();
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select w.warehouseid     ");
                qry.Add(" from  warehouse w       ");
                qry.Add(" where w.inactive <> 'T' ");
                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                foreach (List<object> row in dt.Rows)
                {
                    ActiveWarehouseIds.Add(row[dt.GetColumnNo("warehouseid")].ToString());
                }

            }

            return success;
        }
        //-------------------------------------------------------------------------------------------------------   
        public static string AvailabilityNumberToString(float number)
        {
            string str = "";

            if (number.Equals(Math.Floor(number)))  //number is an integer
            {
                str = ((int)number).ToString();
            }
            else
            {
                str = number.ToString();
            }

            return str;
        }
        //-------------------------------------------------------------------------------------------------------   
        public static DateTime GetCurrentAvailabilityHour()
        {
            DateTime currHour = DateTime.Today.AddHours(DateTime.Now.Hour);
            return currHour;
        }
        //-------------------------------------------------------------------------------------------------------   
        public static bool InventoryWarehouseNeedsRecalc(TInventoryWarehouseAvailabilityKey availKey)
        {
            return AvailabilityNeedRecalc.ContainsKey(availKey);
        }
        //-------------------------------------------------------------------------------------------------------        
        public static bool InventoryWarehouseNeedsRecalc(string inventoryId, string warehouseId)
        {
            return InventoryWarehouseNeedsRecalc(new TInventoryWarehouseAvailabilityKey(inventoryId, warehouseId));
        }
        //-------------------------------------------------------------------------------------------------------        
        public static bool RequestRecalc(string inventoryId, string warehouseId, string classification, bool preCache = true)
        {
            bool b = true;
            //Console.WriteLine("adding to AvailabilityNeedRecalc - " + inventoryId + ", " + warehouseId + ", " + classification);
            TAvailabilityNeedRecalcMetaData d = new TAvailabilityNeedRecalcMetaData(classification, preCache);
            AvailabilityNeedRecalc.AddOrUpdate(new TInventoryWarehouseAvailabilityKey(inventoryId, warehouseId), d, (key, existingValue) =>
            {
                existingValue.classification = d.classification;
                existingValue.preCache = d.preCache;
                return existingValue;
            });
            return b;
        }
        //-------------------------------------------------------------------------------------------------------        
        public static async Task<bool> InvalidateHourly(FwApplicationConfig appConfig)
        {
            bool success = true;
            //Console.WriteLine("invalidating cache for all inventory tracked by hourly availbility");
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {

                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select masterid, warehouseid, class, rank, qty, ispackageowned, isinownedpackage, noavail   ");
                qry.Add(" from  masterwhforavailview                                                                 ");
                qry.Add(" where availbyhour = 'T'                                                                    ");
                if (hourlyInitialized)
                {
                    //after the hourly dataset has been intialized once, then we only need to maintain the I-Codes that have Staged/Out items to update the "late" quantity
                    qry.Add(" and (qtystaged > 0 or qtyout > 0) ");
                }
                qry.Add(" option (recompile)  ");
                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                foreach (List<object> row in dt.Rows)
                {
                    string inventoryId = row[dt.GetColumnNo("masterid")].ToString();
                    string warehouseId = row[dt.GetColumnNo("warehouseid")].ToString();
                    string classification = row[dt.GetColumnNo("class")].ToString();
                    float qty = FwConvert.ToFloat(row[dt.GetColumnNo("qty")].ToString());
                    bool isPackageOwned = FwConvert.ToBoolean(row[dt.GetColumnNo("ispackageowned")].ToString());
                    bool isInOwnedPackage = FwConvert.ToBoolean(row[dt.GetColumnNo("isinownedpackage")].ToString());
                    bool noAvail = FwConvert.ToBoolean(row[dt.GetColumnNo("noavail")].ToString());
                    bool preCache = (((qty != 0) || isPackageOwned || isInOwnedPackage) && (!noAvail));
                    RequestRecalc(inventoryId, warehouseId, classification, preCache);
                }
            }
            hourlyInitialized = true;
            return success;
        }
        //-------------------------------------------------------------------------------------------------------        
        public static async Task<bool> InvalidateDaily(FwApplicationConfig appConfig)
        {
            bool success = true;
            //Console.WriteLine("invalidating cache for all inventory tracked by daily availbility");
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {

                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select masterid, warehouseid, class, rank, qty, ispackageowned, isinownedpackage, noavail   ");
                qry.Add(" from  masterwhforavailview                                                                 ");
                qry.Add(" where availbyhour <> 'T'                                                                   ");
                if (dailyInitialized)
                {
                    //after the hourly dataset has been intialized once, then we only need to maintain the I-Codes that have Staged/Out items to update the "late" quantity
                    qry.Add(" and (qtystaged > 0 or qtyout > 0) ");
                }
                qry.Add(" option (recompile)  ");
                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                foreach (List<object> row in dt.Rows)
                {
                    string inventoryId = row[dt.GetColumnNo("masterid")].ToString();
                    string warehouseId = row[dt.GetColumnNo("warehouseid")].ToString();
                    string classification = row[dt.GetColumnNo("class")].ToString();
                    float qty = FwConvert.ToFloat(row[dt.GetColumnNo("qty")].ToString());
                    bool isPackageOwned = FwConvert.ToBoolean(row[dt.GetColumnNo("ispackageowned")].ToString());
                    bool isInOwnedPackage = FwConvert.ToBoolean(row[dt.GetColumnNo("isinownedpackage")].ToString());
                    bool noAvail = FwConvert.ToBoolean(row[dt.GetColumnNo("noavail")].ToString());
                    bool preCache = (((qty != 0) || isPackageOwned || isInOwnedPackage) && (!noAvail));
                    RequestRecalc(inventoryId, warehouseId, classification, preCache);
                }
            }

            TransferAvailabilityToDateTime = DateTime.Today.AddDays(45);

            dailyInitialized = true;
            return success;
        }
        //-------------------------------------------------------------------------------------------------------        
        public static async Task<bool> CheckNeedRecalc(FwApplicationConfig appConfig)
        {
            bool success = true;

            //Console.WriteLine("about to query the availneedrecalc table");

            //Console.WriteLine("  before: AvailabilityNeedRecalc has " + AvailabilityNeedRecalc.Count.ToString() + " items");
            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select a.masterid, a.warehouseid, a.class, a.id   ");
                qry.Add(" from  tmpavailneedrecalcview a with (nolock)     ");
                qry.Add(" where a.id > @lastneedrecalcid                   ");
                qry.Add("order by a.id                                     ");
                qry.Add(" option (recompile)  ");
                qry.AddParameter("@lastneedrecalcid", LastNeedRecalcId);
                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                //Console.WriteLine("          found " + dt.TotalRows.ToString() + " records in the availneedrecalc table");

                string needRecalcId = string.Empty;
                foreach (List<object> row in dt.Rows)
                {
                    string inventoryId = row[dt.GetColumnNo("masterid")].ToString();
                    string warehouseId = row[dt.GetColumnNo("warehouseid")].ToString();
                    string classification = row[dt.GetColumnNo("class")].ToString();
                    needRecalcId = row[dt.GetColumnNo("id")].ToString();
                    RequestRecalc(inventoryId, warehouseId, classification, true);
                }
                if (!needRecalcId.Equals(string.Empty))
                {
                    LastNeedRecalcId = FwConvert.ToInt32(needRecalcId);
                }
            }

            //Console.WriteLine("  after:  AvailabilityNeedRecalc has " + AvailabilityNeedRecalc.Count.ToString() + " items");

            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        private static async Task<TAvailabilityCache> LoadReservations(FwApplicationConfig appConfig, FwUserSession userSession, TInventoryWarehouseAvailabilityRequestItems availRequestItems)
        {
            TAvailabilityCache availCache = new TAvailabilityCache();
            string sessionId = AppFunc.GetNextIdAsync(appConfig).Result;
            DateTime fromDateTime = DateTime.Today;
            DateTime toDateTime = DateTime.Today.AddDays(availabilityDaysToCache);

            foreach (TInventoryWarehouseAvailabilityRequestItem availRequestItem in availRequestItems)
            {
                TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(availRequestItem.InventoryId, availRequestItem.WarehouseId);
                TAvailabilityNeedRecalcMetaData d = new TAvailabilityNeedRecalcMetaData();
                AvailabilityNeedRecalc.TryRemove(availKey, out d);

                if (availRequestItem.ToDateTime > toDateTime)
                {
                    toDateTime = availRequestItem.ToDateTime;
                }
            }

            FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString);

            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("delete                        ");
                qry.Add(" from  tmpsearchsession       ");
                qry.Add(" where sessionid = @sessionid ");
                qry.Add(" option (recompile)  ");
                qry.AddParameter("@sessionid", sessionId);
                await qry.ExecuteNonQueryAsync();
            }

            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                int i = 0;
                int totalItemsAddedToSession = 0;
                int totalRequestItems = availRequestItems.Count;
                const int MAX_ITEMS_PER_ITERATION = 300;
                foreach (TInventoryWarehouseAvailabilityRequestItem availRequestItem in availRequestItems)
                {
                    qry.Add("if (not exists (select *                                                     ");
                    qry.Add("                 from  tmpsearchsession t                                    ");
                    qry.Add("                 where t.sessionid   = @sessionid                            ");
                    qry.Add("                 and   t.masterid    = @masterid" + i.ToString() + "))       ");
                    qry.Add("begin                                                                        ");
                    qry.Add("   insert into tmpsearchsession (sessionid, masterid)                        ");
                    qry.Add("    values (@sessionid, @masterid" + i.ToString() + ")                       ");
                    qry.Add("end                                                                          ");
                    qry.AddParameter("@masterid" + i.ToString(), availRequestItem.InventoryId);
                    //qry.AddParameter("@warehouseid" + i.ToString(), availRequestItem.WarehouseId);
                    i++;
                    totalItemsAddedToSession++;

                    if ((i >= MAX_ITEMS_PER_ITERATION) && (totalItemsAddedToSession < totalRequestItems))   // if we have already created a query with 300 items to add, and there are still more to do, then process the 300 and start a new query for the next group. this avoids exceeding the parameter limit on sql server
                    {
                        qry.AddParameter("@sessionid", sessionId);
                        await qry.ExecuteNonQueryAsync();
                        i = 0;
                        qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                    }
                }
                qry.AddParameter("@sessionid", sessionId);
                await qry.ExecuteNonQueryAsync();
            }

            Dictionary<TInventoryWarehouseAvailabilityKey, TInventoryWarehouse> packages = new Dictionary<TInventoryWarehouseAvailabilityKey, TInventoryWarehouse>();

            {

                FwSqlCommand qryAcc = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qryAcc.Add("select p.packageid, p.warehouseid, p.masterid, p.defaultqty              ");
                qryAcc.Add(" from  packagemasterwhforavailview p                                     ");
                qryAcc.Add("              join tmpsearchsession a on (p.packageid   = a.masterid)    ");
                qryAcc.Add(" where a.sessionid = @sessionid                                          ");
                qryAcc.Add("order by p.packageid, p.warehouseid                                      ");
                qryAcc.Add(" option (recompile)  ");
                qryAcc.AddParameter("@sessionid", sessionId);
                FwJsonDataTable dtAcc = await qryAcc.QueryToFwJsonTableAsync();

                if (dtAcc.Rows.Count > 0)
                {
                    string prevPackageId = string.Empty;
                    string prevWarehouseId = string.Empty;
                    string packageId = string.Empty;
                    string warehouseId = string.Empty;
                    TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(packageId, warehouseId);
                    TInventoryWarehouse inventoryWarehouse = new TInventoryWarehouse(packageId, warehouseId);

                    foreach (List<object> rowAcc in dtAcc.Rows)
                    {
                        packageId = rowAcc[dtAcc.GetColumnNo("packageid")].ToString();
                        warehouseId = rowAcc[dtAcc.GetColumnNo("warehouseid")].ToString();
                        if ((!packageId.Equals(prevPackageId)) || (!warehouseId.Equals(prevWarehouseId)))
                        {
                            if (!prevPackageId.Equals(string.Empty))
                            {
                                packages.Add(availKey, inventoryWarehouse);
                            }
                            availKey = new TInventoryWarehouseAvailabilityKey(packageId, warehouseId);
                            inventoryWarehouse = new TInventoryWarehouse(packageId, warehouseId);
                        }

                        string accInventoryId = rowAcc[dtAcc.GetColumnNo("masterid")].ToString();
                        float accDefaultQuantity = FwConvert.ToFloat(rowAcc[dtAcc.GetColumnNo("defaultqty")].ToString());
                        inventoryWarehouse.Accessories.Add(new TPackageAccessory(accInventoryId, accDefaultQuantity));

                        prevPackageId = packageId;
                        prevWarehouseId = warehouseId;
                    }

                    if (!packageId.Equals(string.Empty))
                    {
                        packages.Add(availKey, inventoryWarehouse);
                    }
                }
            }

            //add all of the accessories to the searchsession, too
            {
                FwSqlCommand qryAcc = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qryAcc.Add("insert into tmpsearchsession (sessionid, masterid)                       ");
                qryAcc.Add("select distinct a.sessionid, p.masterid                                  ");
                qryAcc.Add(" from  packagemasterwhforavailview p                                     ");
                qryAcc.Add("              join tmpsearchsession a on (p.packageid = a.masterid)      ");
                qryAcc.Add(" where a.sessionid = @sessionid                                          ");
                qryAcc.AddParameter("@sessionid", sessionId);
                qryAcc.Add(" option (recompile)  ");
                await qryAcc.ExecuteNonQueryAsync();
            }


            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select a.masterid, a.warehouseid, a.availfor,                                                                         ");
                qry.Add("       a.masterno, a.master, a.whcode, a.noavail, a.warehouse, a.class, a.availbyhour, a.availabilitygrace,           ");
                qry.Add("       a.inventorydepartmentid, a.inventorydepartment, a.categoryid, a.category, a.subcategoryid, a.subcategory,      ");
                qry.Add("       a.availenableqcdelay, a.availqcdelay,                                                                          ");
                qry.Add("       a.ownedqty, a.ownedqtyin, a.ownedqtystaged, a.ownedqtyout, a.ownedqtyintransit,                                ");
                qry.Add("       a.ownedqtyinrepair, a.ownedqtyontruck, a.ownedqtyincontainer, a.ownedqtyqcrequired,                            ");
                qry.Add("       a.consignedqty, a.consignedqtyin, a.consignedqtystaged, a.consignedqtyout, a.consignedqtyintransit,            ");
                qry.Add("       a.consignedqtyinrepair, a.consignedqtyontruck, a.consignedqtyincontainer, a.consignedqtyqcrequired,            ");
                qry.Add("       a.dailyrate, a.weeklyrate, a.week2rate, a.week3rate, a.week4rate, a.monthlyrate                                ");
                qry.Add(" from  availabilitymasterwhview a with (nolock)                                                                       ");
                qry.Add("             join tmpsearchsession t with (nolock) on (a.masterid = t.masterid)                                       ");
                qry.Add(" where t.sessionid = @sessionid                                                                                       ");
                qry.Add(" option (recompile)  ");
                qry.AddParameter("@sessionid", sessionId);
                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                foreach (List<object> row in dt.Rows)
                {
                    string inventoryId = row[dt.GetColumnNo("masterid")].ToString();
                    string warehouseId = row[dt.GetColumnNo("warehouseid")].ToString();
                    TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(inventoryId, warehouseId);

                    TInventoryWarehouseAvailability availData = new TInventoryWarehouseAvailability(inventoryId, warehouseId);

                    availData.AvailDataFromDateTime = fromDateTime;
                    availData.AvailDataToDateTime = toDateTime;
                    availData.InventoryWarehouse.ICode = row[dt.GetColumnNo("masterno")].ToString();
                    availData.InventoryWarehouse.Description = row[dt.GetColumnNo("master")].ToString();
                    availData.InventoryWarehouse.WarehouseCode = row[dt.GetColumnNo("whcode")].ToString();
                    availData.InventoryWarehouse.Warehouse = row[dt.GetColumnNo("warehouse")].ToString();
                    availData.InventoryWarehouse.Classification = row[dt.GetColumnNo("class")].ToString();
                    availData.InventoryWarehouse.InventoryTypeId = row[dt.GetColumnNo("inventorydepartmentid")].ToString();
                    availData.InventoryWarehouse.InventoryType = row[dt.GetColumnNo("inventorydepartment")].ToString();
                    availData.InventoryWarehouse.CategoryId = row[dt.GetColumnNo("categoryid")].ToString();
                    availData.InventoryWarehouse.Category = row[dt.GetColumnNo("category")].ToString();
                    availData.InventoryWarehouse.SubCategoryId = row[dt.GetColumnNo("subcategoryid")].ToString();
                    availData.InventoryWarehouse.SubCategory = row[dt.GetColumnNo("subcategory")].ToString();
                    availData.InventoryWarehouse.AvailableFor = row[dt.GetColumnNo("availfor")].ToString();

                    if (availData.InventoryWarehouse.Classification.Equals(RwConstants.INVENTORY_CLASSIFICATION_COMPLETE) || availData.InventoryWarehouse.Classification.Equals(RwConstants.INVENTORY_CLASSIFICATION_KIT))
                    {
                        TInventoryWarehouse package = null;
                        if (packages.TryGetValue(availKey, out package))
                        {
                            foreach (TPackageAccessory accessory in package.Accessories)
                            {
                                availData.InventoryWarehouse.Accessories.Add(new TPackageAccessory(accessory.InventoryId, accessory.DefaultQuantity));
                            }
                        }
                    }

                    NumberFormatInfo numberFormat = new NumberFormatInfo();
                    numberFormat.NumberDecimalDigits = 2;
                    availData.InventoryWarehouse.DailyRate = FwConvert.ToFloat(row[dt.GetColumnNo("dailyrate")].ToString()).ToString("N", numberFormat);
                    availData.InventoryWarehouse.WeeklyRate = FwConvert.ToFloat(row[dt.GetColumnNo("weeklyrate")].ToString()).ToString("N", numberFormat);
                    availData.InventoryWarehouse.Week2Rate = FwConvert.ToFloat(row[dt.GetColumnNo("week2rate")].ToString()).ToString("N", numberFormat);
                    availData.InventoryWarehouse.Week3Rate = FwConvert.ToFloat(row[dt.GetColumnNo("week3rate")].ToString()).ToString("N", numberFormat);
                    availData.InventoryWarehouse.Week4Rate = FwConvert.ToFloat(row[dt.GetColumnNo("week4rate")].ToString()).ToString("N", numberFormat);
                    availData.InventoryWarehouse.MonthlyRate = FwConvert.ToFloat(row[dt.GetColumnNo("monthlyrate")].ToString()).ToString("N", numberFormat);

                    availData.InventoryWarehouse.HourlyAvailability = FwConvert.ToBoolean(row[dt.GetColumnNo("availbyhour")].ToString());
                    availData.InventoryWarehouse.NoAvailabilityCheck = FwConvert.ToBoolean(row[dt.GetColumnNo("noavail")].ToString());

                    availData.Total.Owned = FwConvert.ToFloat(row[dt.GetColumnNo("ownedqty")].ToString());
                    availData.Total.Consigned = FwConvert.ToFloat(row[dt.GetColumnNo("consignedqty")].ToString());

                    availData.In.Owned = FwConvert.ToFloat(row[dt.GetColumnNo("ownedqtyin")].ToString());
                    availData.In.Consigned = FwConvert.ToFloat(row[dt.GetColumnNo("consignedqtyin")].ToString());

                    availData.Staged.Owned = FwConvert.ToFloat(row[dt.GetColumnNo("ownedqtystaged")].ToString());
                    availData.Staged.Consigned = FwConvert.ToFloat(row[dt.GetColumnNo("consignedqtystaged")].ToString());

                    availData.Out.Owned = FwConvert.ToFloat(row[dt.GetColumnNo("ownedqtyout")].ToString());
                    availData.Out.Consigned = FwConvert.ToFloat(row[dt.GetColumnNo("consignedqtyout")].ToString());

                    availData.InTransit.Owned = FwConvert.ToFloat(row[dt.GetColumnNo("ownedqtyintransit")].ToString());
                    availData.InTransit.Consigned = FwConvert.ToFloat(row[dt.GetColumnNo("consignedqtyintransit")].ToString());

                    availData.InRepair.Owned = FwConvert.ToFloat(row[dt.GetColumnNo("ownedqtyinrepair")].ToString());
                    availData.InRepair.Consigned = FwConvert.ToFloat(row[dt.GetColumnNo("consignedqtyinrepair")].ToString());

                    availData.OnTruck.Owned = FwConvert.ToFloat(row[dt.GetColumnNo("ownedqtyontruck")].ToString());
                    availData.OnTruck.Consigned = FwConvert.ToFloat(row[dt.GetColumnNo("consignedqtyontruck")].ToString());

                    availData.InContainer.Owned = FwConvert.ToFloat(row[dt.GetColumnNo("ownedqtyincontainer")].ToString());
                    availData.InContainer.Consigned = FwConvert.ToFloat(row[dt.GetColumnNo("consignedqtyincontainer")].ToString());

                    availData.QcRequired.Owned = FwConvert.ToFloat(row[dt.GetColumnNo("ownedqtyqcrequired")].ToString());
                    availData.QcRequired.Consigned = FwConvert.ToFloat(row[dt.GetColumnNo("consignedqtyqcrequired")].ToString());

                    availData.EnableQcDelay = FwConvert.ToBoolean(row[dt.GetColumnNo("availenableqcdelay")].ToString());
                    availData.QcDelayDays = FwConvert.ToInt32(row[dt.GetColumnNo("availqcdelay")].ToString());
                    if (availData.EnableQcDelay)
                    {
                        availData.QcToDateTime = DateTime.Today.AddDays(availData.QcDelayDays - 1);
                    }

                    availData.InventoryWarehouse.LowAvailabilityPercent = FwConvert.ToInt32(row[dt.GetColumnNo("availabilitygrace")].ToString());
                    availData.InventoryWarehouse.LowAvailabilityQuantity = (int)Math.Floor(((double)availData.Total.Total * (((double)availData.InventoryWarehouse.LowAvailabilityPercent) / 100.00)));


                    availCache.AddOrUpdate(availKey, availData, (key, existingValue) =>
                    {
                        existingValue.CloneFrom(availData);
                        return existingValue;
                    });
                }
            }

            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("delete t                                                                ");
                qry.Add(" from  tmpsearchsession t                                               ");
                qry.Add("           join master m with (nolock) on (t.masterid = m.masterid)     ");
                qry.Add(" where t.sessionid = @sessionid                                         ");
                qry.Add(" and   m.noavail   = 'T'                                                ");
                qry.Add(" option (recompile)  ");
                qry.AddParameter("@sessionid", sessionId);
                await qry.ExecuteNonQueryAsync();
            }


            bool hasConsignment = false;  //jh 02/28/2019 place-holder.  #jhtodo: need to add system-wide option for consignment here 

            {
                FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout);
                qry.Add("select a.masterid,                                                                     ");
                qry.Add("       a.warehouseid, a.whcode, a.warehouse,                                           ");
                qry.Add("       a.returntowarehouseid, a.returntowhcode, a.returntowarehouse,                   ");
                qry.Add("       a.orderid, a.masteritemid, a.rectype,                                           ");
                qry.Add("       a.availfromdate, a.availfromtime, a.availtodate, a.availtotime,                 ");
                qry.Add("       a.availlatedays, a.availlatehours,                                              ");
                qry.Add("       a.qcrequired, a.availenableqcdelay, a.availqcdelay,                             ");
                qry.Add("       a.availqcdelayexcludeweekend, a.availqcdelayexcludeholiday,                     ");
                qry.Add("       a.availqcdelayindefinite, a.availbyhour,                                        ");
                qry.Add("       a.excludecontainedfromavail, a.containerbarcode,                                ");
                qry.Add("       a.contractid,                                                                   ");
                qry.Add("       a.ordertype, a.orderno, a.orderdesc, a.orderstatus, a.dealid, a.deal,           ");
                qry.Add("       a.departmentid, a.department,                                                   ");
                qry.Add("       a.qtyordered, a.qtystagedowned, a.qtyoutowned, a.qtyinowned,                    ");
                qry.Add("       a.subqty, a.qtystagedsub, a.qtyoutsub, a.qtyinsub,                              ");
                qry.Add("       a.qtyinrepairowned, a.qtyinrepairconsigned,                                     ");
                qry.Add("       a.poid, a.pono, a.podesc, a.povendor,                                           ");
                if (hasConsignment)
                {
                    //jh 02/28/2019 this is a bottleneck as the query must join in ordertranextended to get the consignorid.  Consider moving consignorid to the ordetran table
                    qry.Add("       a.consignqty, a.qtystagedconsigned, a.qtyoutconsigned, a.qtyinconsigned ");
                }
                else
                {
                    qry.Add("       consignqty = 0, qtystagedconsigned = 0, qtyoutconsigned = 0, qtyinconsigned = 0 ");
                }
                qry.Add(" from  availabilityitemview a with (nolock)                                             ");
                qry.Add("             join tmpsearchsession t with (nolock) on (a.masterid = t.masterid)         ");
                qry.Add(" where t.sessionid = @sessionid                                                         ");
                qry.Add(" option (recompile)     ");
                qry.AddParameter("@sessionid", sessionId);
                FwJsonDataTable dt = await qry.QueryToFwJsonTableAsync();

                // load dt into availData.Reservations
                foreach (List<object> row in dt.Rows)
                {
                    string recType = row[dt.GetColumnNo("rectype")].ToString();
                    if (recType.Equals(RwConstants.RECTYPE_RENTAL) || recType.Equals(RwConstants.RECTYPE_SALE) || recType.Equals(RwConstants.RECTYPE_USED_SALE))
                    {
                        TInventoryWarehouseAvailabilityReservation reservation = new TInventoryWarehouseAvailabilityReservation();
                        reservation.InventoryId = row[dt.GetColumnNo("masterid")].ToString();
                        reservation.WarehouseId = row[dt.GetColumnNo("warehouseid")].ToString();
                        reservation.WarehouseCode = row[dt.GetColumnNo("whcode")].ToString();
                        reservation.Warehouse = row[dt.GetColumnNo("warehouse")].ToString();
                        reservation.ReturnToWarehouseId = row[dt.GetColumnNo("returntowarehouseid")].ToString();
                        reservation.ReturnToWarehouseCode = row[dt.GetColumnNo("returntowhcode")].ToString();
                        reservation.ReturnToWarehouse = row[dt.GetColumnNo("returntowarehouse")].ToString();

                        if (string.IsNullOrEmpty(reservation.ReturnToWarehouseId))
                        {
                            reservation.ReturnToWarehouseId = reservation.WarehouseId;
                            reservation.ReturnToWarehouseCode = reservation.WarehouseCode;
                            reservation.ReturnToWarehouse = reservation.Warehouse;
                        }

                        reservation.OrderId = row[dt.GetColumnNo("orderid")].ToString();
                        reservation.OrderItemId = row[dt.GetColumnNo("masteritemid")].ToString();
                        reservation.RecType = row[dt.GetColumnNo("rectype")].ToString();
                        reservation.OrderType = row[dt.GetColumnNo("ordertype")].ToString();
                        reservation.OrderNumber = row[dt.GetColumnNo("orderno")].ToString();
                        reservation.OrderDescription = row[dt.GetColumnNo("orderdesc")].ToString();
                        reservation.OrderStatus = row[dt.GetColumnNo("orderstatus")].ToString();
                        reservation.DepartmentId = row[dt.GetColumnNo("departmentid")].ToString();
                        reservation.Department = row[dt.GetColumnNo("department")].ToString();
                        reservation.DealId = row[dt.GetColumnNo("dealid")].ToString();
                        reservation.Deal = row[dt.GetColumnNo("deal")].ToString();
                        //reservation.FromDateTime = FwConvert.ToDateTime(row[dt.GetColumnNo("availfromdatetime")].ToString());
                        DateTime fromDate = FwConvert.ToDateTime(row[dt.GetColumnNo("availfromdate")].ToString());  // 03/25/2020
                        DateTime fromTime = FwConvert.ToDateTime(row[dt.GetColumnNo("availfromtime")].ToString());  // 10:15
                        reservation.FromDateTime = fromDate.AddHours(fromTime.Hour);                                // 03/25/2020 10:00

                        if (string.IsNullOrEmpty(row[dt.GetColumnNo("availtodate")].ToString()))
                        {
                            reservation.ToDateTime = InventoryAvailabilityFunc.LateDateTime;
                        }
                        else
                        {
                            //reservation.ToDateTime = FwConvert.ToDateTime(row[dt.GetColumnNo("availtodatetime")].ToString());
                            DateTime toDate = FwConvert.ToDateTime(row[dt.GetColumnNo("availtodate")].ToString());  // 03/25/2020
                            DateTime toTime = FwConvert.ToDateTime(row[dt.GetColumnNo("availtotime")].ToString());  // 10:15
                            reservation.ToDateTime = toDate.AddHours(toTime.Hour + (toTime.Minute == 0 ? 0 : 1));    // 03/25/2020 11:00
                        }

                        reservation.QcRequired = FwConvert.ToBoolean(row[dt.GetColumnNo("qcrequired")].ToString());
                        reservation.EnableQcDelay = FwConvert.ToBoolean(row[dt.GetColumnNo("availenableqcdelay")].ToString());
                        reservation.QcDelayDays = FwConvert.ToInt32(row[dt.GetColumnNo("availqcdelay")].ToString());
                        reservation.QcDelayExcludeWeekend = FwConvert.ToBoolean(row[dt.GetColumnNo("availqcdelayexcludeweekend")].ToString());
                        reservation.QcDelayExcludeHoliday = FwConvert.ToBoolean(row[dt.GetColumnNo("availqcdelayexcludeholiday")].ToString());
                        reservation.QcDelayIndefinite = FwConvert.ToBoolean(row[dt.GetColumnNo("availqcdelayindefinite")].ToString());

                        reservation.AvailableWhileInContainer = ((reservation.IsContainer) && (!FwConvert.ToBoolean(row[dt.GetColumnNo("excludecontainedfromavail")].ToString())));
                        reservation.ContainerBarCode = row[dt.GetColumnNo("containerbarcode")].ToString();

                        reservation.ContractId = row[dt.GetColumnNo("contractid")].ToString();

                        reservation.QuantityOrdered = FwConvert.ToFloat(row[dt.GetColumnNo("qtyordered")].ToString());
                        reservation.QuantitySub = FwConvert.ToFloat(row[dt.GetColumnNo("subqty")].ToString());
                        reservation.QuantityConsigned = FwConvert.ToFloat(row[dt.GetColumnNo("consignqty")].ToString());
                        reservation.SubPurchaseOrderId = row[dt.GetColumnNo("poid")].ToString();
                        reservation.SubPurchaseOrderNumber = row[dt.GetColumnNo("pono")].ToString();
                        reservation.SubPurchaseOrderDescription = row[dt.GetColumnNo("podesc")].ToString();
                        reservation.SubPurchaseOrderVendor = row[dt.GetColumnNo("povendor")].ToString();

                        TInventoryWarehouseAvailabilityQuantity reservationStaged = new TInventoryWarehouseAvailabilityQuantity();
                        TInventoryWarehouseAvailabilityQuantity reservationOut = new TInventoryWarehouseAvailabilityQuantity();
                        TInventoryWarehouseAvailabilityQuantity reservationIn = new TInventoryWarehouseAvailabilityQuantity();
                        TInventoryWarehouseAvailabilityQuantity reservationInRepair = new TInventoryWarehouseAvailabilityQuantity();

                        reservationStaged.Owned = FwConvert.ToFloat(row[dt.GetColumnNo("qtystagedowned")].ToString());
                        reservationStaged.Subbed = FwConvert.ToFloat(row[dt.GetColumnNo("qtystagedsub")].ToString());
                        reservationStaged.Consigned = FwConvert.ToFloat(row[dt.GetColumnNo("qtystagedconsigned")].ToString());

                        reservationOut.Owned = FwConvert.ToFloat(row[dt.GetColumnNo("qtyoutowned")].ToString());
                        reservationOut.Subbed = FwConvert.ToFloat(row[dt.GetColumnNo("qtyoutsub")].ToString());
                        reservationOut.Consigned = FwConvert.ToFloat(row[dt.GetColumnNo("qtyoutconsigned")].ToString());

                        reservationIn.Owned = FwConvert.ToFloat(row[dt.GetColumnNo("qtyinowned")].ToString());
                        reservationIn.Subbed = FwConvert.ToFloat(row[dt.GetColumnNo("qtyinsub")].ToString());
                        reservationIn.Consigned = FwConvert.ToFloat(row[dt.GetColumnNo("qtyinconsigned")].ToString());

                        reservationInRepair.Owned = FwConvert.ToFloat(row[dt.GetColumnNo("qtyinrepairowned")].ToString());
                        reservationInRepair.Subbed = 0;
                        reservationInRepair.Consigned = FwConvert.ToFloat(row[dt.GetColumnNo("qtyinrepairconsigned")].ToString());

                        reservation.QuantityStaged = reservationStaged;
                        reservation.QuantityOut = reservationOut;
                        reservation.QuantityIn = reservationIn;
                        reservation.QuantityInRepair = reservationInRepair;

                        if (reservation.OrderType.Equals(RwConstants.ORDER_TYPE_ORDER) || reservation.OrderType.Equals(RwConstants.ORDER_TYPE_TRANSFER) || ((reservation.OrderType.Equals(RwConstants.ORDER_TYPE_QUOTE) && (reservation.OrderStatus.Equals(RwConstants.QUOTE_STATUS_RESERVED)))))
                        {
                            reservation.QuantityReserved.Owned = (reservation.QuantityOrdered - reservation.QuantitySub - reservation.QuantityConsigned - reservation.QuantityStaged.Owned - reservation.QuantityOut.Owned - reservation.QuantityIn.Owned);
                            reservation.QuantityReserved.Subbed = (reservation.QuantitySub - reservation.QuantityStaged.Subbed - reservation.QuantityOut.Subbed - reservation.QuantityIn.Subbed);
                            reservation.QuantityReserved.Consigned = (reservation.QuantityConsigned - reservation.QuantityStaged.Consigned - reservation.QuantityOut.Consigned - reservation.QuantityIn.Consigned);
                        }
                        else if (reservation.OrderType.Equals(RwConstants.ORDER_TYPE_REPAIR))
                        {
                        }

                        {
                            bool hourlyAvailability = FwConvert.ToBoolean(row[dt.GetColumnNo("availbyhour")].ToString());

                            //if (reservation.ToDateTime < DateTime.Now)
                            if (((hourlyAvailability) && (reservation.ToDateTime < DateTime.Now)) ||
                                ((!hourlyAvailability) && (reservation.ToDateTime < DateTime.Today)))
                            {
                                reservation.QuantityReserved.Owned = 0;
                                reservation.QuantityReserved.Subbed = 0;
                                reservation.QuantityReserved.Consigned = 0;
                            }
                        }

                        string inventoryId = row[dt.GetColumnNo("masterid")].ToString();
                        string warehouseId = row[dt.GetColumnNo("warehouseid")].ToString();
                        string returnToWarehouseId = row[dt.GetColumnNo("returntowarehouseid")].ToString();
                        if (string.IsNullOrEmpty(returnToWarehouseId))
                        {
                            returnToWarehouseId = warehouseId;
                        }

                        //local private method to adjust the reservation "to date/time" and "qc date/time"
                        void adjustReservationToDateTime(ref TInventoryWarehouseAvailabilityReservation res, bool hourlyAvailability)
                        {
                            if (!hourlyAvailability)
                            {
                                res.FromDateTime = res.FromDateTime.Date;
                                //res.ToDateTime = (res.ToDateTime.Equals(res.ToDateTime.Date) ? res.ToDateTime.Date : res.ToDateTime.Date.AddDays(1));
                                res.ToDateTime = res.ToDateTime.Date;
                            }

                            //if ((res.ToDateTime < DateTime.Now) && ((res.QuantityStaged.Total + res.QuantityOut.Total + +res.QuantityInRepair.Total) > 0))
                            //{
                            //    int warehouseLateDays = FwConvert.ToInt32(row[dt.GetColumnNo("availlatedays")].ToString());
                            //    DateTime lateButReturningThroughDate = res.ToDateTime.AddDays(warehouseLateDays);
                            //    if ((warehouseLateDays > 0) && (res.QuantityOut.Total > 0) && (res.ToDateTime < lateButReturningThroughDate) && (lateButReturningThroughDate > DateTime.Now))
                            //    {
                            //        res.ToDateTime = lateButReturningThroughDate;
                            //        res.LateButReturning = true;
                            //    }
                            //    else
                            //    {
                            //        res.ToDateTime = InventoryAvailabilityFunc.LateDateTime;
                            //    }
                            //}

                            // adjust for Late and LateButReturning
                            if ((res.QuantityStaged.Total + res.QuantityOut.Total + +res.QuantityInRepair.Total) > 0)
                            {
                                bool isLate = false;
                                if (hourlyAvailability)
                                {
                                    //hourly availability
                                    isLate = (res.ToDateTime < DateTime.Now);

                                    if (isLate)
                                    {
                                        int warehouseLateHours = FwConvert.ToInt32(row[dt.GetColumnNo("availlatehours")].ToString());
                                        DateTime lateButReturningThroughDateTime = res.ToDateTime.AddHours(warehouseLateHours);
                                        if ((warehouseLateHours > 0) && (res.QuantityOut.Total > 0) && (res.ToDateTime < lateButReturningThroughDateTime) && (lateButReturningThroughDateTime > DateTime.Now))
                                        {
                                            res.ToDateTime = lateButReturningThroughDateTime;
                                            res.LateButReturning = true;
                                        }
                                        else
                                        {
                                            res.ToDateTime = InventoryAvailabilityFunc.LateDateTime;
                                            res.Late = true;
                                        }
                                    }

                                }
                                else
                                {
                                    //daily availability
                                    isLate = (res.ToDateTime < DateTime.Today);

                                    if (isLate)
                                    {
                                        int warehouseLateDays = FwConvert.ToInt32(row[dt.GetColumnNo("availlatedays")].ToString());
                                        DateTime lateButReturningThroughDate = res.ToDateTime.AddDays(warehouseLateDays);
                                        if ((warehouseLateDays > 0) && (res.QuantityOut.Total > 0) && (res.ToDateTime < lateButReturningThroughDate) && (lateButReturningThroughDate > DateTime.Today.AddDays(-1)))
                                        {
                                            res.ToDateTime = lateButReturningThroughDate;
                                            res.LateButReturning = true;
                                        }
                                        else
                                        {
                                            res.ToDateTime = InventoryAvailabilityFunc.LateDateTime;
                                            res.Late = true;
                                        }
                                    }
                                }

                            }



                            if ((res.QcRequired) && (res.OrderType.Equals(RwConstants.ORDER_TYPE_QUOTE) || res.OrderType.Equals(RwConstants.ORDER_TYPE_ORDER)) && ((res.QuantityReserved.Owned + res.QuantityStaged.Owned + res.QuantityOut.Owned) > 0) && (res.EnableQcDelay) && ((res.QcDelayDays > 0) || (res.QcDelayIndefinite)))
                            {
                                //if (!res.ToDateTime.Equals(InventoryAvailabilityFunc.LateDateTime))
                                if (!res.Late)
                                {
                                    res.QcDelayFromDateTime = res.ToDateTime.AddDays(1);
                                    res.QcDelayToDateTime = res.ToDateTime.AddDays(res.QcDelayDays);
                                    res.QcQuantity = (res.QuantityReserved.Owned + res.QuantityStaged.Owned + res.QuantityOut.Owned);
                                    //#jhtodo: qc delay for weekends and holidays, qc indefinite
                                }
                            }
                        }

                        // if the warehouse is different, then this is a Transfer.  Need to clone the reservation object to deal with the two separately.
                        TInventoryWarehouseAvailabilityReservation reservationForToWarehouse = new TInventoryWarehouseAvailabilityReservation();
                        if (!returnToWarehouseId.Equals(warehouseId))
                        {
                            reservationForToWarehouse.CloneFrom(reservation);
                        }

                        TInventoryWarehouseAvailabilityKey availKeyFrom = new TInventoryWarehouseAvailabilityKey(inventoryId, warehouseId);
                        TInventoryWarehouseAvailability availDataFrom = null;
                        if (availCache.TryGetValue(availKeyFrom, out availDataFrom))
                        {
                            // item is being transferred out, should show as out indefinitely from the outgoing warehouse only
                            if (reservation.IsTransfer)
                            {
                                reservation.ToDateTime = InventoryAvailabilityFunc.LateDateTime;
                            }

                            adjustReservationToDateTime(ref reservation, availDataFrom.InventoryWarehouse.HourlyAvailability);

                            availDataFrom.Reservations.Add(reservation);
                        }


                        if (!returnToWarehouseId.Equals(warehouseId))
                        {
                            TInventoryWarehouseAvailabilityKey availKeyTo = new TInventoryWarehouseAvailabilityKey(inventoryId, returnToWarehouseId);
                            TInventoryWarehouseAvailability availDataTo = null;

                            if (availCache.TryGetValue(availKeyTo, out availDataTo))
                            {
                                adjustReservationToDateTime(ref reservationForToWarehouse, availDataTo.InventoryWarehouse.HourlyAvailability);

                                availDataTo.Reservations.Add(reservationForToWarehouse);
                            }
                        }

                    }
                }
            }

            //#jhtodo copy the loop above for Completes and Kits, joining on parentid.  This will give a list of reservations that reference these packages
            //qry.Add("             join tmpsearchsession t on (a.parentid = t.masterid and a.warehouseid = t.warehouseid)");

            return availCache;
        }
        //-------------------------------------------------------------------------------------------------------
        private static void CalculateFutureAvailability(ref TAvailabilityCache availCache)
        {
            // first pass, just calculate future availability for stand-alone items
            foreach (KeyValuePair<TInventoryWarehouseAvailabilityKey, TInventoryWarehouseAvailability> availEntry in availCache)
            {
                TInventoryWarehouseAvailabilityKey availKey = availEntry.Key;
                TInventoryWarehouseAvailability availData = availEntry.Value;
                if (!AppFunc.InventoryClassIsPackage(availData.InventoryWarehouse.Classification))   // stand-alone item
                {
                    if (!availData.InventoryWarehouse.NoAvailabilityCheck)
                    {
                        DateTime currentAvailabilityDateTime = (availData.InventoryWarehouse.HourlyAvailability ? InventoryAvailabilityFunc.GetCurrentAvailabilityHour() : DateTime.Today);
                        DateTime fromDateTime = currentAvailabilityDateTime;
                        DateTime toDateTime = availData.AvailDataToDateTime;
                        availData.AvailabilityDatesAndTimes.Clear();
                        TInventoryWarehouseAvailabilityQuantity available = new TInventoryWarehouseAvailabilityQuantity();
                        //available.CloneFrom(availData.In);   // snapshot the current IN quantity.  use this as a running total
                        available = TInventoryWarehouseAvailabilityQuantity.MakeCopy(availData.In);   // snapshot the current IN quantity.  use this as a running total
                        if ((availData.EnableQcDelay) && (availData.QcToDateTime != null))
                        {
                            available -= availData.QcRequired;
                        }
                        TInventoryWarehouseAvailabilityQuantity late = new TInventoryWarehouseAvailabilityQuantity();

                        // use the availData.Reservations to calculate future availability for this Icode
                        DateTime theDateTime = fromDateTime;
                        while (theDateTime <= toDateTime)
                        {
                            TInventoryWarehouseAvailabilityDateTime inventoryWarehouseAvailabilityDateTime = new TInventoryWarehouseAvailabilityDateTime(theDateTime);
                            foreach (TInventoryWarehouseAvailabilityReservation reservation in availData.Reservations)
                            {
                                if (reservation.WarehouseId.Equals(availKey.WarehouseId))
                                {
                                    if ((reservation.FromDateTime <= theDateTime) && ((theDateTime <= reservation.ToDateTime) || ((reservation.QcDelayToDateTime != null) && (theDateTime <= reservation.QcDelayToDateTime))))
                                    {
                                        if ((reservation.QuantityLate.Total > 0) || (reservation.QuantityReserved.Total > 0) || (reservation.QuantityStaged.Total > 0) || (reservation.QuantityOut.Total > 0))
                                        {
                                            inventoryWarehouseAvailabilityDateTime.Reservations.Add(reservation);
                                        }
                                        inventoryWarehouseAvailabilityDateTime.Reserved += reservation.QuantityReserved;
                                        if (!reservation.countedReserved)
                                        {
                                            available -= reservation.QuantityReserved;
                                        }
                                        reservation.countedReserved = true;
                                    }

                                    if ((reservation.AvailableWhileInContainer) && (!reservation.countedAvailableInContainer))
                                    {
                                        available += reservation.QuantityStaged + reservation.QuantityOut;
                                        reservation.countedAvailableInContainer = true;
                                    }

                                    if (((available.OwnedAndConsigned) < 0) && ((reservation.QuantityReserved.OwnedAndConsigned) > 0))
                                    {
                                        reservation.IsNegativeConflict = true;
                                        availData.HasNegativeConflict = true;
                                    }
                                    else if ((reservation.QuantitySub > 0) && ((available.OwnedAndConsigned) >= reservation.QuantitySub))
                                    {
                                        reservation.IsPositiveConflict = true;
                                        availData.HasPositiveConflict = true;
                                    }
                                }

                                if (reservation.ReturnToWarehouseId.Equals(availKey.WarehouseId))
                                {
                                    if (availData.InventoryWarehouse.AvailableFor.Equals(RwConstants.INVENTORY_AVAILABLE_FOR_RENT))
                                    {
                                        if ((theDateTime == fromDateTime) && (reservation.ToDateTime == LateDateTime))  // items are late
                                        {
                                            if (!reservation.countedLate)
                                            {
                                                reservation.QuantityLate = reservation.QuantityStaged + reservation.QuantityOut;
                                                late += reservation.QuantityLate;
                                            }
                                            reservation.countedLate = true;
                                        }
                                    }

                                    if (reservation.ToDateTime == theDateTime)
                                    {
                                        inventoryWarehouseAvailabilityDateTime.Returning += reservation.QuantityReserved + reservation.QuantityStaged + reservation.QuantityOut + reservation.QuantityInRepair;
                                    }

                                    if (reservation.QcDelayToDateTime != null)
                                    {
                                        if (reservation.QcDelayToDateTime == theDateTime)
                                        {
                                            inventoryWarehouseAvailabilityDateTime.BecomingAvailable += reservation.QuantityReserved + reservation.QuantityStaged + reservation.QuantityOut + reservation.QuantityInRepair;
                                        }
                                    }
                                    else if (reservation.ToDateTime == theDateTime)
                                    {
                                        inventoryWarehouseAvailabilityDateTime.BecomingAvailable += reservation.QuantityReserved + reservation.QuantityStaged + reservation.QuantityOut + reservation.QuantityInRepair;
                                    }


                                }

                            }

                            inventoryWarehouseAvailabilityDateTime.Available = available;
                            //available += inventoryWarehouseAvailabilityDateTime.Returning;  // the amount returning in this date/hour slot will become available for the next date/hour slot
                            available += inventoryWarehouseAvailabilityDateTime.BecomingAvailable;  // the amount returning in this date/hour slot will become available for the next date/hour slot


                            // add in the items due to be QC'd by then
                            if ((availData.EnableQcDelay) && (availData.QcToDateTime == theDateTime))
                            {
                                available += availData.QcRequired;
                            }



                            availData.AvailabilityDatesAndTimes.TryAdd(theDateTime, inventoryWarehouseAvailabilityDateTime);

                            if (availData.InventoryWarehouse.HourlyAvailability)
                            {
                                theDateTime = theDateTime.AddHours(1);
                            }
                            else
                            {
                                theDateTime = theDateTime.AddDays(1);
                            }
                        }
                        availData.Late = late;
                    }
                    availData.CalculatedDateTime = DateTime.Now;
                }
            }

            // second pass, calculate future availability for completes and kits
            foreach (KeyValuePair<TInventoryWarehouseAvailabilityKey, TInventoryWarehouseAvailability> availEntry in availCache)
            {
                TInventoryWarehouseAvailabilityKey availKey = availEntry.Key;
                TInventoryWarehouseAvailability availData = availEntry.Value;
                if (AppFunc.InventoryClassIsPackage(availData.InventoryWarehouse.Classification))   // complete / kit
                {
                    if (!availData.InventoryWarehouse.NoAvailabilityCheck)
                    {
                        DateTime currentAvailabilityDateTime = (availData.InventoryWarehouse.HourlyAvailability ? InventoryAvailabilityFunc.GetCurrentAvailabilityHour() : DateTime.Today);
                        DateTime fromDateTime = currentAvailabilityDateTime;
                        DateTime toDateTime = availData.AvailDataToDateTime;
                        availData.AvailabilityDatesAndTimes.Clear();
                        DateTime theDateTime = fromDateTime;
                        while (theDateTime <= toDateTime)
                        {
                            TInventoryWarehouseAvailabilityQuantity packageAvailable = new TInventoryWarehouseAvailabilityQuantity();
                            bool initialPackageAvailableDetermined = false;
                            bool accessoryAvailabilityDataExists = true;

                            foreach (TPackageAccessory accessory in availData.InventoryWarehouse.Accessories)
                            {
                                if (accessory.DefaultQuantity != 0) // avoid divide-by-zero error.  value should not be zero anyway
                                {
                                    TInventoryWarehouseAvailabilityKey accKey = new TInventoryWarehouseAvailabilityKey(accessory.InventoryId, availKey.WarehouseId);
                                    TInventoryWarehouseAvailability accAvailCache = null;
                                    if (availCache.TryGetValue(accKey, out accAvailCache))
                                    {
                                        TInventoryWarehouseAvailabilityDateTime accAvailableDateTime = null;
                                        if (accAvailCache.AvailabilityDatesAndTimes.TryGetValue(theDateTime, out accAvailableDateTime))
                                        {
                                            TInventoryWarehouseAvailabilityQuantity accAvailable = accAvailableDateTime.Available;
                                            if (initialPackageAvailableDetermined)
                                            {
                                                //Compare with available calculation based on other accessories already looked at
                                                packageAvailable.Owned = Math.Min(packageAvailable.Owned, accAvailable.Owned / accessory.DefaultQuantity);
                                                packageAvailable.Consigned = Math.Min(packageAvailable.Consigned, (accAvailable.Consigned / accessory.DefaultQuantity));
                                            }
                                            else
                                            {
                                                packageAvailable.Owned = (accAvailable.Owned / accessory.DefaultQuantity);
                                                packageAvailable.Consigned = (accAvailable.Consigned / accessory.DefaultQuantity);
                                            }
                                            initialPackageAvailableDetermined = true;
                                        }
                                        else
                                        {
                                            // accessory availabiltiy data not found in cache
                                            accessoryAvailabilityDataExists = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        // accessory availabiltiy data not found in cache
                                        accessoryAvailabilityDataExists = false;
                                        break;
                                    }
                                }
                            }

                            //round the package available numbers to the next whole number (important when spare accessories are used)
                            packageAvailable.Owned = (float)Math.Floor(packageAvailable.Owned);
                            packageAvailable.Consigned = (float)Math.Floor(packageAvailable.Consigned);

                            if (accessoryAvailabilityDataExists)
                            {
                                TInventoryWarehouseAvailabilityDateTime inventoryWarehouseAvailabilityDateTime = new TInventoryWarehouseAvailabilityDateTime(theDateTime);
                                inventoryWarehouseAvailabilityDateTime.Available = packageAvailable;
                                availData.AvailabilityDatesAndTimes.TryAdd(theDateTime, inventoryWarehouseAvailabilityDateTime);
                                if (availData.InventoryWarehouse.HourlyAvailability)
                                {
                                    theDateTime = theDateTime.AddHours(1);
                                }
                                else
                                {
                                    theDateTime = theDateTime.AddDays(1);
                                }
                            }
                            else
                            {
                                // availabiltiy data not found in cache for at least one accessory for at least one date in the range
                                break;
                                // exit the date while loop.  leave package availability data incomplete
                            }
                        }
                    }
                    availData.CalculatedDateTime = DateTime.Now;
                }
            }


        }
        //-------------------------------------------------------------------------------------------------------
        private static void CalculateAllWarehouseAvailability(ref TAvailabilityCache availCache)
        {
            List<string> inventoryIds = new List<string>();
            List<string> warehouseIds = new List<string>();

            TAvailabilityCache allWhAvailCache = new TAvailabilityCache();

            foreach (KeyValuePair<TInventoryWarehouseAvailabilityKey, TInventoryWarehouseAvailability> availEntry in availCache)
            {
                TInventoryWarehouseAvailabilityKey allWhAvailKey = new TInventoryWarehouseAvailabilityKey(availEntry.Key.InventoryId, RwConstants.WAREHOUSEID_ALL);
                TInventoryWarehouseAvailability allWhAvail = null;
                if (!allWhAvailCache.TryGetValue(allWhAvailKey, out allWhAvail))
                {
                    allWhAvail = new TInventoryWarehouseAvailability(allWhAvailKey.InventoryId, allWhAvailKey.WarehouseId);
                }
                allWhAvail += availEntry.Value;

                allWhAvailCache.AddOrUpdate(allWhAvailKey, allWhAvail, (key, existingValue) =>
                {
                    existingValue.CloneFrom(allWhAvail);
                    return existingValue;
                });
            }

            foreach (KeyValuePair<TInventoryWarehouseAvailabilityKey, TInventoryWarehouseAvailability> allWhAvail in allWhAvailCache)
            {
                availCache.AddOrUpdate(allWhAvail.Key, allWhAvail.Value, (key, existingValue) =>
                {
                    existingValue.CloneFrom(allWhAvail.Value);
                    return existingValue;
                });
            }

        }
        //-------------------------------------------------------------------------------------------------------
        private static async Task<bool> RefreshAvailability(FwApplicationConfig appConfig, FwUserSession userSession, TInventoryWarehouseAvailabilityRequestItems availRequestItems)
        {
            bool success = true;
            if (availRequestItems.Count > 0)
            {
                TAvailabilityCache availCache = await LoadReservations(appConfig, userSession, availRequestItems);
                CalculateFutureAvailability(ref availCache);
                CalculateAllWarehouseAvailability(ref availCache);
                foreach (TInventoryWarehouseAvailabilityKey availKey in availCache.Keys)
                {
                    AvailabilityCache.AddOrUpdate(availKey, availCache[availKey], (key, existingValue) =>
                    {
                        existingValue.CloneFrom(availCache[availKey]);
                        return existingValue;
                    });
                }
            }
            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<bool> KeepAvailabilityCacheFresh(FwApplicationConfig appConfig)
        {
            const int AVAILABILITY_REQUEST_BATCH_SIZE = 5000;
            bool success = true;
            //Console.WriteLine("keeping availability fresh");
            DateTime fromDate = DateTime.Today;
            DateTime availabilityThroughDate = DateTime.Today.AddDays(availabilityDaysToCache);

            // initialize an empty request
            TInventoryWarehouseAvailabilityRequestItems availRequestItems = new TInventoryWarehouseAvailabilityRequestItems();

            // build up a list of Items and Accessories only from the global AvailabilityNeedRecalc list
            //Console.WriteLine("checking AvailabilityNeedRecalc");
            TAvailabilityNeedRecalcDictionary availNeedRecalcItem = new TAvailabilityNeedRecalcDictionary();
            TAvailabilityNeedRecalcDictionary availNeedRecalcPackage = new TAvailabilityNeedRecalcDictionary();

            lock (AvailabilityNeedRecalc)  // no external adds or deletes of the global AvailabilityNeedRecalc dictonary during this scope. We are emptying the entire dictionary to a local copy
            {
                //Console.WriteLine("locked AvailabilityNeedRecalc");
                if (AvailabilityNeedRecalc.IsEmpty)
                {
                    //Console.WriteLine("no records in AvailabilityNeedRecalc");
                }
                else
                {
                    //Console.WriteLine("pulling " + AvailabilityNeedRecalc.Count.ToString() + " records from AvailabilityNeedRecalc");
                    foreach (KeyValuePair<TInventoryWarehouseAvailabilityKey, TAvailabilityNeedRecalcMetaData> anc in AvailabilityNeedRecalc)
                    {
                        if (anc.Value.preCache)
                        {
                            if (AppFunc.InventoryClassIsPackage(anc.Value.classification))
                            {
                                availNeedRecalcPackage.AddOrUpdate(anc.Key, anc.Value, (key, existingValue) =>
                                {
                                    existingValue = anc.Value;
                                    return existingValue;
                                });
                            }
                            else
                            {
                                availNeedRecalcItem.AddOrUpdate(anc.Key, anc.Value, (key, existingValue) =>
                                {
                                    existingValue = anc.Value;
                                    return existingValue;
                                });
                            }

                            TAvailabilityNeedRecalcMetaData d = new TAvailabilityNeedRecalcMetaData();
                            AvailabilityNeedRecalc.TryRemove(anc.Key, out d);
                        }
                    }
                    //AvailabilityNeedRecalc.Clear();
                }
                //Console.WriteLine("unlocking AvailabilityNeedRecalc");
            }

            if (availNeedRecalcItem.Count > 0)
            {
                AvailabilityKeepFreshLogLogic log = new AvailabilityKeepFreshLogLogic();
                log.SetDependencies(appConfig, null);
                log.StartDateTime = DateTime.Now;
                //log.EndDateTime = DateTime.MaxValue;
                log.BatchSize = availNeedRecalcItem.Count;
                int x = await log.SaveAsync(null);

                // loop through this local list of Items and Accessories in batches
                //Console.WriteLine(availNeedRecalcItem.Count.ToString().PadLeft(7) + " master/warehouse item/accessory records need recalc");
                while (availNeedRecalcItem.Count > 0)
                {
                    // build up a request containing all known items needing recalc
                    availRequestItems.Clear();
                    foreach (KeyValuePair<TInventoryWarehouseAvailabilityKey, TAvailabilityNeedRecalcMetaData> anc in availNeedRecalcItem)
                    {
                        availRequestItems.Add(new TInventoryWarehouseAvailabilityRequestItem(anc.Key.InventoryId, anc.Key.WarehouseId, fromDate, availabilityThroughDate));
                        if (availRequestItems.Count >= AVAILABILITY_REQUEST_BATCH_SIZE)
                        {
                            break; // break out of this foreach loop
                        }
                    }

                    foreach (TInventoryWarehouseAvailabilityRequestItem availRequestItem in availRequestItems)
                    {
                        TAvailabilityNeedRecalcMetaData d = new TAvailabilityNeedRecalcMetaData();
                        availNeedRecalcItem.TryRemove(new TInventoryWarehouseAvailabilityKey(availRequestItem.InventoryId, availRequestItem.WarehouseId), out d);
                    }

                    // update the global cache of availability data
                    await GetAvailability(appConfig, null, availRequestItems, refreshIfNeeded: true, forceRefresh: true);

                    //Console.WriteLine(availNeedRecalcItem.Count.ToString().PadLeft(7) + " master/warehouse item/accessory records need recalc");
                }

                // loop through this local list of Completes and Kits in batches
                //Console.WriteLine(availNeedRecalcPackage.Count.ToString().PadLeft(7) + " master/warehouse complete/kit records need recalc");
                while (availNeedRecalcPackage.Count > 0)
                {
                    // build up a request containing all known items needing recalc
                    availRequestItems.Clear();
                    foreach (KeyValuePair<TInventoryWarehouseAvailabilityKey, TAvailabilityNeedRecalcMetaData> anc in availNeedRecalcPackage)
                    {
                        availRequestItems.Add(new TInventoryWarehouseAvailabilityRequestItem(anc.Key.InventoryId, anc.Key.WarehouseId, fromDate, availabilityThroughDate));
                        if (availRequestItems.Count >= AVAILABILITY_REQUEST_BATCH_SIZE)
                        {
                            break; // break out of this foreach loop
                        }
                    }

                    foreach (TInventoryWarehouseAvailabilityRequestItem availRequestItem in availRequestItems)
                    {
                        TAvailabilityNeedRecalcMetaData d = new TAvailabilityNeedRecalcMetaData();
                        availNeedRecalcPackage.TryRemove(new TInventoryWarehouseAvailabilityKey(availRequestItem.InventoryId, availRequestItem.WarehouseId), out d);
                    }

                    // update the static cache of availability data
                    await GetAvailability(appConfig, null, availRequestItems, refreshIfNeeded: true, forceRefresh: true);

                    //Console.WriteLine(availNeedRecalcPackage.Count.ToString().PadLeft(7) + " master/warehouse complete/kit records need recalc");
                }

                log.EndDateTime = DateTime.Now;
                x = await log.SaveAsync(null);
            }


            return success;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TAvailabilityCache> GetAvailability(FwApplicationConfig appConfig, FwUserSession userSession, TInventoryWarehouseAvailabilityRequestItems availRequestItems, bool refreshIfNeeded = false, bool forceRefresh = false)
        {
            TAvailabilityCache availCache = new TAvailabilityCache();
            TInventoryWarehouseAvailabilityRequestItems availRequestToRefresh = new TInventoryWarehouseAvailabilityRequestItems();

            foreach (TInventoryWarehouseAvailabilityRequestItem availRequestItem in availRequestItems)
            {
                if ((!string.IsNullOrEmpty(availRequestItem.InventoryId)) && (!availRequestItem.InventoryId.Equals("undefined")) && (!string.IsNullOrEmpty(availRequestItem.WarehouseId)) && (!availRequestItem.WarehouseId.Equals("undefined")))
                {
                    if (forceRefresh)
                    {
                        availRequestToRefresh.Add(availRequestItem);
                    }
                    else
                    {
                        bool foundInCache = false;
                        bool stale = false;
                        DateTime fromDateTime = availRequestItem.FromDateTime;
                        DateTime toDateTime = availRequestItem.ToDateTime;
                        TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(availRequestItem.InventoryId, availRequestItem.WarehouseId);

                        if (InventoryWarehouseNeedsRecalc(availKey))
                        {
                            stale = true;
                        }

                        TInventoryWarehouseAvailability availData = null;
                        if (AvailabilityCache.TryGetValue(availKey, out availData))
                        {
                            TInventoryWarehouseAvailability tmpAvailData = new TInventoryWarehouseAvailability(availKey.InventoryId, availKey.WarehouseId);
                            tmpAvailData.CloneFrom(availData);
                            foundInCache = true;

                            if (!availData.InventoryWarehouse.HourlyAvailability)
                            {
                                fromDateTime = fromDateTime.Date;
                                toDateTime = toDateTime.Date;
                            }


                            DateTime currentAvailabilityDateTime = (availData.InventoryWarehouse.HourlyAvailability ? InventoryAvailabilityFunc.GetCurrentAvailabilityHour() : DateTime.Today);

                            if (fromDateTime < currentAvailabilityDateTime)
                            {
                                fromDateTime = currentAvailabilityDateTime;
                            }

                            if (toDateTime < currentAvailabilityDateTime)
                            {
                                toDateTime = currentAvailabilityDateTime;
                            }


                            DateTime theDateTime = tmpAvailData.AvailDataFromDateTime;
                            while (theDateTime <= tmpAvailData.AvailDataToDateTime)
                            {
                                if ((theDateTime < fromDateTime) || (toDateTime < theDateTime))
                                {
                                    TInventoryWarehouseAvailabilityDateTime availDateTime = null;
                                    tmpAvailData.AvailabilityDatesAndTimes.TryRemove(theDateTime, out availDateTime);
                                }

                                if (tmpAvailData.InventoryWarehouse.HourlyAvailability)
                                {
                                    theDateTime = theDateTime.AddHours(1);
                                }
                                else
                                {
                                    theDateTime = theDateTime.AddDays(1);
                                }


                            }

                            theDateTime = fromDateTime;
                            while (theDateTime <= toDateTime)
                            {
                                if (!tmpAvailData.AvailabilityDatesAndTimes.ContainsKey(theDateTime))
                                {
                                    foundInCache = false;
                                    break;
                                }
                                if (tmpAvailData.InventoryWarehouse.HourlyAvailability)
                                {
                                    theDateTime = theDateTime.AddHours(1);
                                }
                                else
                                {
                                    theDateTime = theDateTime.AddDays(1);
                                }
                            }
                            availCache[availKey] = tmpAvailData;
                        }

                        if ((stale) || (!foundInCache))
                        {
                            if (refreshIfNeeded)
                            {
                                availRequestToRefresh.Add(availRequestItem);
                            }
                        }
                    }
                }
            }

            if (availRequestToRefresh.Count > 0)
            {
                await RefreshAvailability(appConfig, userSession, availRequestToRefresh);
                availCache = await GetAvailability(appConfig, userSession, availRequestItems, refreshIfNeeded: false, forceRefresh: false);   // this is a recursive call to grab the cache of all items again: originals and refreshes
            }

            return availCache;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TInventoryWarehouseAvailability> GetAvailability(FwApplicationConfig appConfig, FwUserSession userSession, string inventoryId, string warehouseId, DateTime fromDate, DateTime toDate, bool refreshIfNeeded = false, bool forceRefresh = false)

        {
            //TInventoryWarehouseAvailability availData = null;
            TInventoryWarehouseAvailability availData = new TInventoryWarehouseAvailability("", "");

            if ((!string.IsNullOrEmpty(inventoryId)) && (!inventoryId.Equals("undefined")) && (!string.IsNullOrEmpty(warehouseId)) && (!warehouseId.Equals("undefined")))
            {
                availData = new TInventoryWarehouseAvailability(inventoryId, warehouseId);

                TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(inventoryId, warehouseId);
                TInventoryWarehouseAvailabilityRequestItems availRequestItems = new TInventoryWarehouseAvailabilityRequestItems();
                availRequestItems.Add(new TInventoryWarehouseAvailabilityRequestItem(inventoryId, warehouseId, fromDate, toDate));

                TAvailabilityCache availCache = await GetAvailability(appConfig, userSession, availRequestItems, refreshIfNeeded, forceRefresh);
                availCache.TryGetValue(availKey, out availData);
            }

            return availData;
        }
        //-------------------------------------------------------------------------------------------------------
        public static async Task<TInventoryAvailabilityCalendarAndScheduleResponse> GetCalendarAndScheduleData(FwApplicationConfig appConfig, FwUserSession userSession, AvailabilityCalendarAndScheduleRequest request)

        {
            //-------------------------------------------------------------------------------------------------------
            //local method to build a calendar date event
            TInventoryAvailabilityCalendarEvent buildCalendarDateEvent(DateTime startDateTime, DateTime endDateTime, TInventoryAvailabilityCalendarData data, /* string caption, float? qty, string backColor, string textColor, */ref int eventId, string inventoryId, string warehouseId)
            {
                eventId++;
                TInventoryAvailabilityCalendarEvent calendarEvent = new TInventoryAvailabilityCalendarEvent();
                calendarEvent.id = eventId.ToString();
                calendarEvent.InventoryId = inventoryId;
                calendarEvent.WarehouseId = warehouseId;
                calendarEvent.start = startDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");   //"2019-02-28 12:00:00 AM"
                calendarEvent.end = endDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");
                calendarEvent.text = data.caption + " " + (data.qty == null ? "" : AvailabilityNumberToString(data.qty.GetValueOrDefault(0)));
                calendarEvent.backColor = data.backColor;
                calendarEvent.textColor = data.textColor;
                return calendarEvent;
            }
            //-------------------------------------------------------------------------------------------------------
            //local method to build a schedule resource
            TInventoryAvailabilityScheduleResource newScheduleResource(ref int resourceId, string description)
            {
                resourceId++;
                TInventoryAvailabilityScheduleResource resource = new TInventoryAvailabilityScheduleResource();
                resource.id = resourceId.ToString();
                resource.name = description;
                return resource;
            }
            //-------------------------------------------------------------------------------------------------------
            //local method to build a schedule event
            TInventoryAvailabilityScheduleEvent buildScheduleEvent(int resourceId, DateTime startDate, string startDisplay, DateTime endDate, string endDisplay, TInventoryAvailabilityCalendarData data, /*float? qty, string text, string backColor, string barColor, string textColor,*/ ref int eventId, string inventoryId, string warehouseId, bool isWarehouseTotal)
            {
                eventId++;
                TInventoryAvailabilityScheduleEvent scheduleEvent = new TInventoryAvailabilityScheduleEvent();
                scheduleEvent.id = eventId.ToString(); ;
                scheduleEvent.resource = resourceId.ToString();
                scheduleEvent.InventoryId = inventoryId;
                scheduleEvent.WarehouseId = warehouseId;
                scheduleEvent.start = startDate.ToString("yyyy-MM-ddTHH:mm:ss tt");   //"2019-02-28 12:00:00 AM"
                scheduleEvent.end = endDate.ToString("yyyy-MM-ddTHH:mm:ss tt");
                scheduleEvent.startdisplay = startDisplay;
                scheduleEvent.enddisplay = endDisplay;
                scheduleEvent.text = data.caption;
                scheduleEvent.backColor = data.backColor;
                scheduleEvent.barColor = data.barColor;
                scheduleEvent.textColor = data.textColor;
                if (data.qty != null)
                {
                    scheduleEvent.total = AvailabilityNumberToString(data.qty.GetValueOrDefault(0));
                }
                scheduleEvent.isWarehouseTotal = isWarehouseTotal;
                return scheduleEvent;
            }
            //-------------------------------------------------------------------------------------------------------
            //local method to build a schedule event
            List<TInventoryAvailabilityScheduleEvent> buildScheduleReservationEvents(int resourceId, TInventoryWarehouseAvailabilityReservation reservation, float qty, string backColor, string barColor, string textColor, ref int eventId, string inventoryId, string warehouseId, bool isSub, bool isWarehouseTotal)
            {
                List<TInventoryAvailabilityScheduleEvent> scheduleEvents = new List<TInventoryAvailabilityScheduleEvent>();

                eventId++;
                TInventoryAvailabilityScheduleEvent reservationEvent = new TInventoryAvailabilityScheduleEvent();
                reservationEvent.id = eventId.ToString(); ;
                reservationEvent.resource = resourceId.ToString();
                reservationEvent.InventoryId = inventoryId;
                reservationEvent.WarehouseId = warehouseId;

                DateTime reservationFromDateTime = reservation.FromDateTime;
                DateTime reservationToDateTime = reservation.ToDateTime;
                string startDisplay = "";
                string endDisplay = "";

                if (reservationToDateTime.Hour.Equals(0) && reservationToDateTime.Minute.Equals(0) && reservationToDateTime.Second.Equals(0))
                {
                    reservationToDateTime = reservationToDateTime.AddDays(1).AddSeconds(-1);
                }

                startDisplay = reservationFromDateTime.ToString();
                //if (reservation.ToDateTime.Equals(InventoryAvailabilityFunc.LateDateTime))
                if (reservation.Late)
                {
                    endDisplay = "No End Date (Late)";
                }
                else
                {
                    endDisplay = reservationToDateTime.ToString();
                    if (reservation.LateButReturning)
                    {
                        endDisplay += " (Late But Returning)";
                    }
                }

                if (reservation.LateButReturning)
                {
                    backColor = RwGlobals.AVAILABILITY_COLOR_LATE_BUT_RETURNING;
                    textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_LATE_BUT_RETURNING;
                }

                reservationEvent.start = reservationFromDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");   //"2019-02-28 12:00:00 AM"
                reservationEvent.end = reservationToDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");
                reservationEvent.startdisplay = startDisplay;
                reservationEvent.enddisplay = endDisplay;
                reservationEvent.text = reservation.ReservationDescription;
                reservationEvent.backColor = backColor;
                reservationEvent.barColor = barColor;
                reservationEvent.textColor = textColor;
                reservationEvent.isWarehouseTotal = isWarehouseTotal;
                reservationEvent.total = AvailabilityNumberToString(qty);

                reservationEvent.orderId = reservation.OrderId;
                reservationEvent.orderNumber = reservation.OrderNumber;
                reservationEvent.orderStatus = reservation.OrderStatus;
                reservationEvent.orderType = reservation.OrderType;
                reservationEvent.orderDescription = reservation.OrderDescription;
                reservationEvent.deal = reservation.Deal;
                reservationEvent.subPoNumber = reservation.SubPurchaseOrderNumber;
                reservationEvent.subPoVendor = reservation.SubPurchaseOrderVendor;
                reservationEvent.contractId = reservation.ContractId;

                scheduleEvents.Add(reservationEvent);

                if ((reservation.QcDelayFromDateTime != null) && (reservation.QcDelayToDateTime != null) && (reservation.QcQuantity > 0) && (!isSub))
                {
                    eventId++;
                    TInventoryAvailabilityScheduleEvent qcEvent = new TInventoryAvailabilityScheduleEvent();
                    qcEvent.id = eventId.ToString(); ;
                    qcEvent.resource = resourceId.ToString();
                    qcEvent.InventoryId = inventoryId;
                    qcEvent.WarehouseId = warehouseId;

                    DateTime qcFromDateTime = reservation.QcDelayFromDateTime.GetValueOrDefault(DateTime.MinValue);
                    DateTime qcToDateTime = reservation.QcDelayToDateTime.GetValueOrDefault(DateTime.MinValue);
                    startDisplay = "";
                    endDisplay = "";

                    if (qcToDateTime.Hour.Equals(0) && qcToDateTime.Minute.Equals(0) && qcToDateTime.Second.Equals(0))
                    {
                        qcToDateTime = qcToDateTime.AddDays(1).AddSeconds(-1);
                    }

                    startDisplay = qcFromDateTime.ToString();
                    //if (reservation.ToDateTime.Equals(InventoryAvailabilityFunc.LateDateTime))
                    if (reservation.Late)
                    {
                        endDisplay = "No End Date (Late)";
                    }
                    else
                    {
                        endDisplay = qcToDateTime.ToString();
                    }

                    qcEvent.start = qcFromDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");   //"2019-02-28 12:00:00 AM"
                    qcEvent.end = qcToDateTime.ToString("yyyy-MM-ddTHH:mm:ss tt");
                    qcEvent.startdisplay = startDisplay;
                    qcEvent.enddisplay = endDisplay;
                    qcEvent.text = reservation.ReservationDescription;
                    qcEvent.backColor = RwGlobals.AVAILABILITY_COLOR_QC_REQUIRED;
                    qcEvent.barColor = barColor;
                    qcEvent.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_QC_REQUIRED;
                    qcEvent.isWarehouseTotal = isWarehouseTotal;
                    qcEvent.total = AvailabilityNumberToString(qty);

                    qcEvent.orderId = reservation.OrderId;
                    qcEvent.orderNumber = reservation.OrderNumber;
                    qcEvent.orderStatus = reservation.OrderStatus;
                    qcEvent.orderType = reservation.OrderType;
                    qcEvent.orderDescription = reservation.OrderDescription;
                    qcEvent.deal = reservation.Deal;
                    qcEvent.contractId = reservation.ContractId;

                    scheduleEvents.Add(qcEvent);
                }


                return scheduleEvents;
            }
            //-------------------------------------------------------------------------------------------------------
            int CompareReservationsByOrderNumber(TInventoryWarehouseAvailabilityReservation reservation1, TInventoryWarehouseAvailabilityReservation reservation2)
            {
                return reservation1.OrderNumber.CompareTo(reservation2.OrderNumber);
            }
            //-------------------------------------------------------------------------------------------------------
            int CompareReservationsByStart(TInventoryWarehouseAvailabilityReservation reservation1, TInventoryWarehouseAvailabilityReservation reservation2)
            {
                return reservation1.FromDateTime.CompareTo(reservation2.FromDateTime);
            }
            //-------------------------------------------------------------------------------------------------------
            int CompareReservationsByEnd(TInventoryWarehouseAvailabilityReservation reservation1, TInventoryWarehouseAvailabilityReservation reservation2)
            {
                return reservation1.ToDateTime.CompareTo(reservation2.ToDateTime);
            }
            //-------------------------------------------------------------------------------------------------------



            TInventoryAvailabilityCalendarAndScheduleResponse response = new TInventoryAvailabilityCalendarAndScheduleResponse();

            DateTime currentAvailabilityDateTime = DateTime.Today;
            if (request.FromDate < currentAvailabilityDateTime)
            {
                request.FromDate = currentAvailabilityDateTime;
            }

            if (request.ToDate < currentAvailabilityDateTime)
            {
                request.ToDate = currentAvailabilityDateTime;
            }

            TInventoryWarehouseAvailability availData = null;
            List<TInventoryWarehouseAvailability> eachWhAvailData = new List<TInventoryWarehouseAvailability>();

            // remove blanks
            for (int i = request.WarehouseId.Count - 1; i >= 0; i--)
            {
                if (request.WarehouseId[i].Equals(string.Empty))
                {
                    request.WarehouseId.RemoveAt(i);
                }
            }

            if (request.WarehouseId.Count == 0)
            {
                request.WarehouseId = ActiveWarehouseIds;
            }

            //bool isFirstWarehouse = true;
            foreach (string whId in request.WarehouseId)
            {
                //TInventoryWarehouseAvailability whAvailData = await GetAvailability(appConfig, userSession, request.InventoryId, whId, request.FromDate, request.ToDate, refreshIfNeeded: true, forceRefresh: true);
                //TInventoryWarehouseAvailability whAvailData = await GetAvailability(appConfig, userSession, request.InventoryId, whId, request.FromDate, request.ToDate, refreshIfNeeded: true, forceRefresh: isFirstWarehouse);
                TInventoryWarehouseAvailability whAvailData = await GetAvailability(appConfig, userSession, request.InventoryId, whId, request.FromDate, request.ToDate, refreshIfNeeded: true, forceRefresh: false);
            
                if (whAvailData != null)
                {
                    TInventoryWarehouseAvailability whAvailData2 = new TInventoryWarehouseAvailability(request.InventoryId, whId);
                    whAvailData2.CloneFrom(whAvailData);
            
                    eachWhAvailData.Add(whAvailData2);
                    if (availData == null)
                    {
                        availData = new TInventoryWarehouseAvailability(request.InventoryId, whId);
                        availData.CloneFrom(whAvailData2);
                    }
                    else
                    {
                        availData += whAvailData2;
                    }
                }
                //isFirstWarehouse = false;
            }

            bool invHasHourlyAvail = false;
            bool includeHours = request.IncludeHours.GetValueOrDefault(false);
            bool yearView = request.YearView.GetValueOrDefault(false);
            bool canShowHours = true;
            foreach (TInventoryWarehouseAvailability whAvailData in eachWhAvailData)
            {
                if (whAvailData.InventoryWarehouse.HourlyAvailability)
                {
                    invHasHourlyAvail = true;
                }
                if (!whAvailData.InventoryWarehouse.HourlyAvailability)
                {
                    canShowHours = false;
                }
            }
            if (!canShowHours)
            {
                includeHours = false;
            }
            if (yearView)
            {
                includeHours = false;
            }

            if (includeHours)
            {
                currentAvailabilityDateTime = InventoryAvailabilityFunc.GetCurrentAvailabilityHour();
                if (request.FromDate < currentAvailabilityDateTime)
                {
                    request.FromDate = currentAvailabilityDateTime;
                }

                if (request.ToDate < currentAvailabilityDateTime)
                {
                    request.ToDate = currentAvailabilityDateTime;
                }

            }

            if (availData != null)
            {
                string warehouseId = string.Join(',', request.WarehouseId);
                response.InventoryData = availData;
                int eventId = 0;
                string backColor = "";
                string barColor = "";
                string textColor = "";
                float qty = 0;

                // build up the calendar events
                DateTime theDateTime = request.FromDate;  // start at midnight of the night preceeding the From Date
                while (theDateTime < request.ToDate.AddDays(1))  // continue until we pass the "To Date", then stop
                {
                    DateTime startDateTime = theDateTime.AddMinutes(1);
                    DateTime endDateTime = theDateTime;

                    if (includeHours)
                    {
                        endDateTime = theDateTime.AddHours(1).AddMinutes(-1);
                    }
                    else
                    {
                        endDateTime = theDateTime.AddDays(1).AddMinutes(-1);
                    }

                    if (availData.InventoryWarehouse.NoAvailabilityCheck)
                    {
                        TInventoryAvailabilityCalendarData noAvail = new TInventoryAvailabilityCalendarData();
                        noAvail.exists = true;
                        noAvail.caption = RwConstants.NO_AVAILABILITY_CAPTION;
                        noAvail.backColor = RwGlobals.AVAILABILITY_COLOR_NO_AVAILABILITY;
                        noAvail.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_POSITIVE;
                        response.InventoryAvailabilityCalendarEvents.Add(buildCalendarDateEvent(startDateTime, endDateTime, noAvail, ref eventId, request.InventoryId, warehouseId));
                    }
                    else
                    {

                        TInventoryAvailabilityCalendarData late = new TInventoryAvailabilityCalendarData();
                        TInventoryAvailabilityCalendarData avail = new TInventoryAvailabilityCalendarData();
                        TInventoryAvailabilityCalendarData reserved = new TInventoryAvailabilityCalendarData();
                        TInventoryAvailabilityCalendarData sub = new TInventoryAvailabilityCalendarData();
                        TInventoryAvailabilityCalendarData returning = new TInventoryAvailabilityCalendarData();
                        TInventoryAvailabilityCalendarData unknown = new TInventoryAvailabilityCalendarData();

                        if (invHasHourlyAvail && !includeHours)  // need to look at each hour individually and figure out min/max for the Date
                        {

                            List<TInventoryWarehouseAvailabilityReservation> reservations = new List<TInventoryWarehouseAvailabilityReservation>();

                            int hour = 0;
                            while (hour < 24)
                            {
                                DateTime availDateTime = theDateTime.AddHours(hour);
                                TInventoryWarehouseAvailabilityDateTime inventoryWarehouseAvailabilityDateTime = null;
                                if (availData.AvailabilityDatesAndTimes.TryGetValue(availDateTime, out inventoryWarehouseAvailabilityDateTime))
                                {
                                    //late
                                    if ((availDateTime.Equals(currentAvailabilityDateTime)) && (availData.Late.OwnedAndConsigned != 0))
                                    {
                                        late.exists = true;
                                        late.caption = "Late";
                                        late.backColor = RwGlobals.AVAILABILITY_COLOR_LATE;
                                        late.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_LATE;
                                        late.qty = availData.Late.OwnedAndConsigned;
                                    }

                                    //available
                                    avail.caption = "Available";
                                    avail.exists = true;
                                    avail.qty = Math.Min(avail.qty.GetValueOrDefault(inventoryWarehouseAvailabilityDateTime.Available.OwnedAndConsigned), inventoryWarehouseAvailabilityDateTime.Available.OwnedAndConsigned);
                                    if (avail.qty < 0)
                                    {
                                        avail.backColor = RwGlobals.AVAILABILITY_COLOR_NEGATIVE;
                                        avail.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_NEGATIVE;
                                    }
                                    else if ((avail.qty > 0) && (availData.InventoryWarehouse.LowAvailabilityPercent > 0) && (avail.qty <= availData.InventoryWarehouse.LowAvailabilityQuantity))
                                    {
                                        avail.backColor = RwGlobals.AVAILABILITY_COLOR_LOW;
                                        avail.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_LOW;
                                    }
                                    else
                                    {
                                        avail.backColor = RwGlobals.AVAILABILITY_COLOR_POSITIVE;
                                        avail.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_POSITIVE;
                                    }

                                    //reserved (owned)
                                    if (inventoryWarehouseAvailabilityDateTime.Reserved.OwnedAndConsigned != 0)
                                    {
                                        reserved.exists = true;
                                        reserved.caption = "Reserved";
                                        reserved.backColor = RwGlobals.AVAILABILITY_COLOR_RESERVED;
                                        reserved.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_RESERVED;
                                        reserved.qty = Math.Max(reserved.qty.GetValueOrDefault(inventoryWarehouseAvailabilityDateTime.Reserved.OwnedAndConsigned), inventoryWarehouseAvailabilityDateTime.Reserved.OwnedAndConsigned);
                                    }

                                    //reserved (subbed)
                                    if (inventoryWarehouseAvailabilityDateTime.Reserved.Subbed != 0)
                                    {
                                        sub.exists = true;
                                        sub.caption = "Sub";
                                        sub.backColor = RwGlobals.SUB_COLOR;
                                        sub.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_RESERVED;
                                        sub.qty = Math.Max(sub.qty.GetValueOrDefault(inventoryWarehouseAvailabilityDateTime.Reserved.Subbed), inventoryWarehouseAvailabilityDateTime.Reserved.Subbed);
                                    }

                                    //returning
                                    if (inventoryWarehouseAvailabilityDateTime.Returning.OwnedAndConsigned != 0)
                                    {
                                        returning.exists = true;
                                        returning.caption = "Returning";
                                        returning.backColor = RwGlobals.AVAILABILITY_COLOR_RETURNING;
                                        returning.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_RETURNING;
                                        returning.qty = Math.Max(returning.qty.GetValueOrDefault(inventoryWarehouseAvailabilityDateTime.Returning.OwnedAndConsigned), inventoryWarehouseAvailabilityDateTime.Returning.OwnedAndConsigned);
                                    }



                                    foreach (TInventoryWarehouseAvailabilityReservation res in inventoryWarehouseAvailabilityDateTime.Reservations)
                                    {
                                        bool alreadyAdded = false;
                                        foreach (TInventoryWarehouseAvailabilityReservation resCheck in reservations)
                                        {
                                            if (resCheck.OrderItemId.Equals(res.OrderItemId))
                                            {
                                                alreadyAdded = true;
                                            }
                                        }

                                        if (!alreadyAdded)
                                        {
                                            TInventoryWarehouseAvailabilityReservation resCopy = new TInventoryWarehouseAvailabilityReservation();
                                            resCopy.CloneFrom(res);
                                            reservations.Add(resCopy);
                                        }
                                    }
                                }
                                hour++;
                            }

                            response.Dates.Add(new TInventoryAvailabilityCalendarDate(theDateTime, reservations));

                        }
                        else
                        {
                            TInventoryWarehouseAvailabilityDateTime inventoryWarehouseAvailabilityDateTime = null;
                            if (availData.AvailabilityDatesAndTimes.TryGetValue(theDateTime, out inventoryWarehouseAvailabilityDateTime))
                            {
                                //late
                                if ((theDateTime.Equals(currentAvailabilityDateTime)) && (availData.Late.OwnedAndConsigned != 0))
                                {
                                    late.exists = true;
                                    late.caption = "Late";
                                    late.backColor = RwGlobals.AVAILABILITY_COLOR_LATE;
                                    late.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_LATE;
                                    late.qty = availData.Late.OwnedAndConsigned;
                                }

                                //available
                                avail.caption = "Available";
                                if (includeHours)
                                {
                                    avail.caption = "Avail";
                                }
                                avail.exists = true;
                                avail.qty = inventoryWarehouseAvailabilityDateTime.Available.OwnedAndConsigned;
                                if (avail.qty < 0)
                                {
                                    avail.backColor = RwGlobals.AVAILABILITY_COLOR_NEGATIVE;
                                    avail.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_NEGATIVE;
                                }
                                else if ((avail.qty > 0) && (availData.InventoryWarehouse.LowAvailabilityPercent > 0) && (avail.qty <= availData.InventoryWarehouse.LowAvailabilityQuantity))
                                {
                                    avail.backColor = RwGlobals.AVAILABILITY_COLOR_LOW;
                                    avail.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_LOW;
                                }
                                else
                                {
                                    avail.backColor = RwGlobals.AVAILABILITY_COLOR_POSITIVE;
                                    avail.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_POSITIVE;
                                }

                                //reserved (owned)
                                if (inventoryWarehouseAvailabilityDateTime.Reserved.OwnedAndConsigned != 0)
                                {
                                    reserved.exists = true;
                                    reserved.caption = "Reserved";
                                    if (includeHours)
                                    {
                                        reserved.caption = "Resvd";
                                    }
                                    reserved.backColor = RwGlobals.AVAILABILITY_COLOR_RESERVED;
                                    reserved.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_RESERVED;
                                    reserved.qty = inventoryWarehouseAvailabilityDateTime.Reserved.OwnedAndConsigned;
                                }

                                //reserved (subbed)
                                if (inventoryWarehouseAvailabilityDateTime.Reserved.Subbed != 0)
                                {
                                    sub.exists = true;
                                    sub.caption = "Sub";
                                    sub.backColor = RwGlobals.SUB_COLOR;
                                    sub.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_RESERVED;
                                    sub.qty = inventoryWarehouseAvailabilityDateTime.Reserved.Subbed;
                                }

                                //returning
                                if (inventoryWarehouseAvailabilityDateTime.Returning.OwnedAndConsigned != 0)
                                {
                                    returning.exists = true;
                                    returning.caption = "Returning";
                                    if (includeHours)
                                    {
                                        returning.caption = "Return";
                                    }
                                    returning.backColor = RwGlobals.AVAILABILITY_COLOR_RETURNING;
                                    returning.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_RETURNING;
                                    returning.qty = inventoryWarehouseAvailabilityDateTime.Returning.OwnedAndConsigned;
                                }

                                response.Dates.Add(new TInventoryAvailabilityCalendarDate(theDateTime, inventoryWarehouseAvailabilityDateTime.Reservations));

                            }
                            else
                            {
                                unknown.exists = true;
                                unknown.caption = "Unknown";
                                unknown.backColor = RwGlobals.AVAILABILITY_COLOR_NEEDRECALC;
                                unknown.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_NEEDRECALC;
                            }
                        }

                        // once the data is gathered, add rendering here
                        if (late.exists)
                        {
                            response.InventoryAvailabilityCalendarEvents.Add(buildCalendarDateEvent(startDateTime, endDateTime, late, ref eventId, request.InventoryId, warehouseId));
                        }
                        if (avail.exists)
                        {
                            response.InventoryAvailabilityCalendarEvents.Add(buildCalendarDateEvent(startDateTime, endDateTime, avail, ref eventId, request.InventoryId, warehouseId));
                        }
                        if (reserved.exists)
                        {
                            response.InventoryAvailabilityCalendarEvents.Add(buildCalendarDateEvent(startDateTime, endDateTime, reserved, ref eventId, request.InventoryId, warehouseId));
                        }
                        if (sub.exists)
                        {
                            response.InventoryAvailabilityCalendarEvents.Add(buildCalendarDateEvent(startDateTime, endDateTime, sub, ref eventId, request.InventoryId, warehouseId));
                        }
                        if (returning.exists)
                        {
                            response.InventoryAvailabilityCalendarEvents.Add(buildCalendarDateEvent(startDateTime, endDateTime, returning, ref eventId, request.InventoryId, warehouseId));
                        }
                        if (unknown.exists)
                        {
                            response.InventoryAvailabilityCalendarEvents.Add(buildCalendarDateEvent(startDateTime, endDateTime, unknown, ref eventId, request.InventoryId, warehouseId));
                        }

                    }
                    if (includeHours)
                    {
                        theDateTime = theDateTime.AddHours(1);
                    }
                    else
                    {
                        theDateTime = theDateTime.AddDays(1);
                    }
                }

                // build up the top-line schedule events (available quantity)
                int resourceId = 0;

                if (availData.InventoryWarehouse.NoAvailabilityCheck)
                {
                    TInventoryAvailabilityCalendarData noAvail = new TInventoryAvailabilityCalendarData();
                    noAvail.exists = true;
                    noAvail.caption = RwConstants.NO_AVAILABILITY_CAPTION;
                    noAvail.backColor = RwGlobals.AVAILABILITY_COLOR_NO_AVAILABILITY;
                    noAvail.barColor = "";
                    noAvail.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_POSITIVE;
                    response.InventoryAvailabilityScheduleResources.Add(newScheduleResource(ref resourceId, availData.InventoryWarehouse.WarehouseCode + " Available (of " + AvailabilityNumberToString(availData.Total.Total) + " Total)"));
                    response.InventoryAvailabilityScheduleEvents.Add(buildScheduleEvent(resourceId, request.FromDate, request.FromDate.ToString(), request.ToDate, request.ToDate.ToString(), noAvail, ref eventId, request.InventoryId, warehouseId, true));
                }
                else
                {
                    if (yearView)
                    {
                        theDateTime = request.FromDate;
                        while (theDateTime <= request.ToDate)
                        {
                            //if month not yet added to the resource list, then create it here.
                            string monthYear = theDateTime.ToString("MMMM") + " " + theDateTime.Year.ToString();
                            int tmpResourceId = -1;
                            foreach (TInventoryAvailabilityScheduleResource resource in response.InventoryAvailabilityScheduleResources)
                            {
                                if (resource.name.Equals(monthYear))
                                {
                                    tmpResourceId = FwConvert.ToInt32(resource.id);
                                    break;
                                }
                            }
                            if (tmpResourceId.Equals(-1))
                            {
                                response.InventoryAvailabilityScheduleResources.Add(newScheduleResource(ref resourceId, monthYear));
                            }
                            else
                            {
                                resourceId = tmpResourceId;
                            }

                            //DateTime availDateTime = theDateTime;
                            //if ((availDateTime.Equals(DateTime.Today)) && invHasHourlyAvail)
                            //{
                            //    //availDateTime = currentAvailabilityDateTime;
                            //    availDateTime = InventoryAvailabilityFunc.GetCurrentAvailabilityHour();
                            //}

                            TInventoryAvailabilityCalendarData avail = new TInventoryAvailabilityCalendarData();
                            if (invHasHourlyAvail && !includeHours)  // need to look at each hour individually and figure out min/max for the Date
                            {
                                int hour = 0;
                                while (hour < 24)
                                {
                                    DateTime availDateTime = theDateTime.AddHours(hour);
                                    TInventoryWarehouseAvailabilityDateTime inventoryWarehouseAvailabilityDateTime = null;
                                    if (availData.AvailabilityDatesAndTimes.TryGetValue(availDateTime, out inventoryWarehouseAvailabilityDateTime))
                                    {
                                        //available
                                        //avail.caption = "Available";
                                        avail.exists = true;
                                        avail.qty = Math.Min(avail.qty.GetValueOrDefault(inventoryWarehouseAvailabilityDateTime.Available.OwnedAndConsigned), inventoryWarehouseAvailabilityDateTime.Available.OwnedAndConsigned);
                                        avail.caption = AvailabilityNumberToString(avail.qty.GetValueOrDefault(0));
                                        if (avail.qty < 0)
                                        {
                                            avail.backColor = RwGlobals.AVAILABILITY_COLOR_NEGATIVE;
                                            avail.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_NEGATIVE;
                                        }
                                        else if ((avail.qty > 0) && (availData.InventoryWarehouse.LowAvailabilityPercent > 0) && (avail.qty <= availData.InventoryWarehouse.LowAvailabilityQuantity))
                                        {
                                            avail.backColor = RwGlobals.AVAILABILITY_COLOR_LOW;
                                            avail.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_LOW;
                                        }
                                        else
                                        {
                                            avail.backColor = RwGlobals.AVAILABILITY_COLOR_POSITIVE;
                                            avail.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_POSITIVE;
                                        }
                                    }
                                    hour++;
                                }
                            }
                            else
                            {
                                //add the "day" avail event here
                                TInventoryWarehouseAvailabilityDateTime inventoryWarehouseAvailabilityDateTime = null;
                                if (availData.AvailabilityDatesAndTimes.TryGetValue(theDateTime, out inventoryWarehouseAvailabilityDateTime))
                                {
                                    avail.exists = true;
                                    avail.barColor = "";
                                    avail.backColor = RwGlobals.AVAILABILITY_COLOR_POSITIVE;
                                    avail.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_POSITIVE;
                                    avail.qty = inventoryWarehouseAvailabilityDateTime.Available.OwnedAndConsigned;
                                    avail.caption = AvailabilityNumberToString(avail.qty.GetValueOrDefault(0));
                                    if (avail.qty < 0)
                                    {
                                        avail.backColor = RwGlobals.AVAILABILITY_COLOR_NEGATIVE;
                                        avail.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_NEGATIVE;
                                    }
                                    else if ((avail.qty > 0) && (availData.InventoryWarehouse.LowAvailabilityPercent > 0) && (avail.qty <= availData.InventoryWarehouse.LowAvailabilityQuantity))
                                    {
                                        avail.backColor = RwGlobals.AVAILABILITY_COLOR_LOW;
                                        avail.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_LOW;
                                    }
                                }
                            }

                            if (avail.exists)
                            {
                                //DateTime renderDateTime = new DateTime(request.FromDate.Year, request.FromDate.Month, theDateTime.Day);
                                DateTime renderDateTime = new DateTime(request.FromDate.Year, 1 /*January*/, theDateTime.Day);  // using January because it always has 31 days
                                response.InventoryAvailabilityScheduleEvents.Add(buildScheduleEvent(resourceId, renderDateTime, theDateTime.ToString(), renderDateTime, theDateTime.ToString(), avail, ref eventId, request.InventoryId, warehouseId, true));
                            }
                            theDateTime = theDateTime.AddDays(1);
                        }
                    }
                    else
                    {
                        if (eachWhAvailData.Count > 1)  // multiple warehouses being displayed, add a grand total line
                        {
                            response.InventoryAvailabilityScheduleResources.Add(newScheduleResource(ref resourceId, availData.InventoryWarehouse.WarehouseCode + " Available (of " + AvailabilityNumberToString(availData.Total.Total) + " Total)"));

                            theDateTime = request.FromDate;
                            while (theDateTime <= request.ToDate)
                            {
                                TInventoryAvailabilityCalendarData avail = new TInventoryAvailabilityCalendarData();

                                if (invHasHourlyAvail)  // need to look at each hour individually and figure out min/max for the Date
                                {
                                    int hour = 0;
                                    while (hour < 24)
                                    {
                                        DateTime availDateTime = theDateTime.AddHours(hour);
                                        TInventoryWarehouseAvailabilityDateTime inventoryWarehouseAvailabilityDateTime = null;
                                        if (availData.AvailabilityDatesAndTimes.TryGetValue(availDateTime, out inventoryWarehouseAvailabilityDateTime))
                                        {
                                            avail.exists = true;
                                            avail.barColor = "";
                                            avail.backColor = RwGlobals.AVAILABILITY_COLOR_POSITIVE;
                                            avail.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_POSITIVE;
                                            avail.qty = Math.Min(avail.qty.GetValueOrDefault(inventoryWarehouseAvailabilityDateTime.Available.OwnedAndConsigned), inventoryWarehouseAvailabilityDateTime.Available.OwnedAndConsigned);
                                            avail.caption = AvailabilityNumberToString(avail.qty.GetValueOrDefault(0));
                                            if (avail.qty < 0)
                                            {
                                                avail.backColor = RwGlobals.AVAILABILITY_COLOR_NEGATIVE;
                                                avail.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_NEGATIVE;
                                            }
                                            else if ((avail.qty > 0) && (availData.InventoryWarehouse.LowAvailabilityPercent > 0) && (avail.qty <= availData.InventoryWarehouse.LowAvailabilityQuantity))
                                            {
                                                avail.backColor = RwGlobals.AVAILABILITY_COLOR_LOW;
                                                avail.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_LOW;
                                            }
                                        }
                                        hour++;
                                    }
                                }
                                else
                                {
                                    TInventoryWarehouseAvailabilityDateTime inventoryWarehouseAvailabilityDateTime = null;
                                    if (availData.AvailabilityDatesAndTimes.TryGetValue(theDateTime, out inventoryWarehouseAvailabilityDateTime))
                                    {
                                        avail.exists = true;
                                        avail.barColor = "";
                                        avail.backColor = RwGlobals.AVAILABILITY_COLOR_POSITIVE;
                                        avail.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_POSITIVE;
                                        avail.qty = inventoryWarehouseAvailabilityDateTime.Available.OwnedAndConsigned;
                                        avail.caption = AvailabilityNumberToString(avail.qty.GetValueOrDefault(0));
                                        if (avail.qty < 0)
                                        {
                                            avail.backColor = RwGlobals.AVAILABILITY_COLOR_NEGATIVE;
                                            avail.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_NEGATIVE;
                                        }
                                        else if ((avail.qty > 0) && (availData.InventoryWarehouse.LowAvailabilityPercent > 0) && (avail.qty <= availData.InventoryWarehouse.LowAvailabilityQuantity))
                                        {
                                            avail.backColor = RwGlobals.AVAILABILITY_COLOR_LOW;
                                            avail.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_LOW;
                                        }
                                    }
                                }

                                if (avail.exists)
                                {
                                    response.InventoryAvailabilityScheduleEvents.Add(buildScheduleEvent(resourceId, theDateTime, theDateTime.ToString(), theDateTime, theDateTime.ToString(), avail, ref eventId, request.InventoryId, warehouseId, true));
                                }

                                theDateTime = theDateTime.AddDays(1);
                            }
                        }

                        foreach (TInventoryWarehouseAvailability whAvailData in eachWhAvailData)
                        {
                            response.InventoryAvailabilityScheduleResources.Add(newScheduleResource(ref resourceId, whAvailData.InventoryWarehouse.WarehouseCode + " Available (of " + AvailabilityNumberToString(whAvailData.Total.Total) + " Total)"));
                            theDateTime = request.FromDate;
                            while (theDateTime <= request.ToDate)
                            {
                                TInventoryAvailabilityCalendarData avail = new TInventoryAvailabilityCalendarData();

                                if (invHasHourlyAvail)  // need to look at each hour individually and figure out min/max for the Date
                                {
                                    int hour = 0;
                                    while (hour < 24)
                                    {
                                        DateTime availDateTime = theDateTime.AddHours(hour);
                                        TInventoryWarehouseAvailabilityDateTime inventoryWarehouseAvailabilityDateTime = null;
                                        if (whAvailData.AvailabilityDatesAndTimes.TryGetValue(availDateTime, out inventoryWarehouseAvailabilityDateTime))
                                        {
                                            avail.exists = true;
                                            avail.barColor = "";
                                            avail.backColor = RwGlobals.AVAILABILITY_COLOR_POSITIVE;
                                            avail.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_POSITIVE;
                                            avail.qty = Math.Min(avail.qty.GetValueOrDefault(inventoryWarehouseAvailabilityDateTime.Available.OwnedAndConsigned), inventoryWarehouseAvailabilityDateTime.Available.OwnedAndConsigned);
                                            avail.caption = AvailabilityNumberToString(avail.qty.GetValueOrDefault(0));
                                            if (avail.qty < 0)
                                            {
                                                avail.backColor = RwGlobals.AVAILABILITY_COLOR_NEGATIVE;
                                                avail.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_NEGATIVE;
                                            }
                                            else if ((avail.qty > 0) && (whAvailData.InventoryWarehouse.LowAvailabilityPercent > 0) && (avail.qty <= whAvailData.InventoryWarehouse.LowAvailabilityQuantity))
                                            {
                                                avail.backColor = RwGlobals.AVAILABILITY_COLOR_LOW;
                                                avail.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_LOW;
                                            }
                                        }
                                        hour++;
                                    }
                                }
                                else
                                {
                                    TInventoryWarehouseAvailabilityDateTime inventoryWarehouseAvailabilityDateTime = null;
                                    if (whAvailData.AvailabilityDatesAndTimes.TryGetValue(theDateTime, out inventoryWarehouseAvailabilityDateTime))
                                    {
                                        avail.exists = true;
                                        avail.barColor = "";
                                        avail.backColor = RwGlobals.AVAILABILITY_COLOR_POSITIVE;
                                        avail.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_POSITIVE;
                                        avail.qty = inventoryWarehouseAvailabilityDateTime.Available.OwnedAndConsigned;
                                        avail.caption = AvailabilityNumberToString(avail.qty.GetValueOrDefault(0));
                                        if (avail.qty < 0)
                                        {
                                            avail.backColor = RwGlobals.AVAILABILITY_COLOR_NEGATIVE;
                                            avail.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_NEGATIVE;
                                        }
                                        else if ((avail.qty > 0) && (whAvailData.InventoryWarehouse.LowAvailabilityPercent > 0) && (avail.qty <= whAvailData.InventoryWarehouse.LowAvailabilityQuantity))
                                        {
                                            avail.backColor = RwGlobals.AVAILABILITY_COLOR_LOW;
                                            avail.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_LOW;
                                        }
                                    }
                                }

                                if (avail.exists)
                                {
                                    response.InventoryAvailabilityScheduleEvents.Add(buildScheduleEvent(resourceId, theDateTime, theDateTime.ToString(), theDateTime, theDateTime.ToString(), avail, ref eventId, request.InventoryId, warehouseId, true));
                                }

                                theDateTime = theDateTime.AddDays(1); //daily availability
                            }

                            // add QC Required "reservation"
                            if ((whAvailData.QcRequired.OwnedAndConsigned > 0) && (whAvailData.EnableQcDelay) && (whAvailData.QcDelayDays > 0))
                            {

                                if (request.FromDate <= whAvailData.QcToDateTime)
                                {
                                    TInventoryAvailabilityCalendarData qc = new TInventoryAvailabilityCalendarData();

                                    DateTime qcToDateTime = whAvailData.QcToDateTime;
                                    if (qcToDateTime.Hour.Equals(0) && qcToDateTime.Minute.Equals(0) && qcToDateTime.Second.Equals(0))
                                    {
                                        qcToDateTime = qcToDateTime.AddDays(1).AddSeconds(-1);
                                    }

                                    response.InventoryAvailabilityScheduleResources.Add(newScheduleResource(ref resourceId, "QC Required"));
                                    qc.caption = "QC Required";
                                    qc.backColor = RwGlobals.AVAILABILITY_COLOR_QC_REQUIRED;
                                    qc.barColor = RwGlobals.AVAILABILITY_COLOR_QC_REQUIRED;
                                    qc.textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_QC_REQUIRED;
                                    qc.qty = whAvailData.QcRequired.OwnedAndConsigned;
                                    response.InventoryAvailabilityScheduleEvents.Add(buildScheduleEvent(resourceId, request.FromDate, request.FromDate.ToString(), qcToDateTime, qcToDateTime.ToString(), qc, ref eventId, request.InventoryId, warehouseId, false));
                                }
                            }


                            // build up the schedule resources and reservation events
                            eventId = 0;

                            // sort the Reservations the way we want them displayed
                            if (request.SortReservationsBy.Equals("Start"))
                            {
                                whAvailData.Reservations.Sort(CompareReservationsByStart);
                            }
                            else if (request.SortReservationsBy.Equals("End"))
                            {
                                whAvailData.Reservations.Sort(CompareReservationsByEnd);
                            }
                            //else if (request.SortReservationsBy.Equals("AvailabilityPriority"))
                            //{
                            //}
                            else // SortReservationsBy.Equals("OrderNumber")
                            {
                                whAvailData.Reservations.Sort(CompareReservationsByOrderNumber);
                            }

                            foreach (TInventoryWarehouseAvailabilityReservation reservation in whAvailData.Reservations)
                            {
                                //reserved (owned and consigned)
                                if ((reservation.QuantityReserved.OwnedAndConsigned != 0) && ((reservation.FromDateTime <= request.ToDate) && (reservation.ToDateTime >= request.FromDate)))
                                {
                                    response.InventoryAvailabilityScheduleResources.Add(newScheduleResource(ref resourceId, reservation.ScheduleResourceDescription));

                                    backColor = RwGlobals.AVAILABILITY_COLOR_RESERVED;
                                    barColor = RwGlobals.AVAILABILITY_COLOR_RESERVED;
                                    textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_RESERVED;
                                    qty = reservation.QuantityReserved.OwnedAndConsigned;

                                    if (reservation.IsTransfer)
                                    {
                                        barColor = RwGlobals.IN_TRANSIT_COLOR;
                                    }

                                    response.InventoryAvailabilityScheduleEvents.AddRange(buildScheduleReservationEvents(resourceId, reservation, qty, backColor, barColor, textColor, ref eventId, request.InventoryId, warehouseId, false, false));
                                }

                                //reserved (subbed)
                                if ((reservation.QuantityReserved.Subbed != 0) && ((reservation.FromDateTime <= request.ToDate) && (reservation.ToDateTime >= request.FromDate)))
                                {
                                    response.InventoryAvailabilityScheduleResources.Add(newScheduleResource(ref resourceId, reservation.ScheduleResourceDescription));

                                    backColor = RwGlobals.SUB_COLOR;
                                    barColor = RwGlobals.AVAILABILITY_COLOR_RESERVED;
                                    textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_RESERVED;
                                    qty = reservation.QuantityReserved.Subbed;
                                    response.InventoryAvailabilityScheduleEvents.AddRange(buildScheduleReservationEvents(resourceId, reservation, qty, backColor, barColor, textColor, ref eventId, request.InventoryId, warehouseId, true, false));
                                }

                                //staged (owned and consigned)
                                if ((reservation.QuantityStaged.OwnedAndConsigned != 0) && ((reservation.FromDateTime <= request.ToDate) && (reservation.ToDateTime >= request.FromDate)))
                                {
                                    response.InventoryAvailabilityScheduleResources.Add(newScheduleResource(ref resourceId, reservation.ScheduleResourceDescription));

                                    barColor = RwGlobals.STAGED_COLOR;
                                    backColor = RwGlobals.AVAILABILITY_COLOR_RESERVED;
                                    textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_RESERVED;
                                    qty = reservation.QuantityStaged.OwnedAndConsigned;
                                    response.InventoryAvailabilityScheduleEvents.AddRange(buildScheduleReservationEvents(resourceId, reservation, qty, backColor, barColor, textColor, ref eventId, request.InventoryId, warehouseId, false, false));
                                }

                                //staged (subbed)
                                if ((reservation.QuantityStaged.Subbed != 0) && ((reservation.FromDateTime <= request.ToDate) && (reservation.ToDateTime >= request.FromDate)))
                                {
                                    response.InventoryAvailabilityScheduleResources.Add(newScheduleResource(ref resourceId, reservation.ScheduleResourceDescription));

                                    barColor = RwGlobals.STAGED_COLOR;
                                    backColor = RwGlobals.SUB_COLOR;
                                    textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_RESERVED;
                                    qty = reservation.QuantityStaged.Subbed;
                                    response.InventoryAvailabilityScheduleEvents.AddRange(buildScheduleReservationEvents(resourceId, reservation, qty, backColor, barColor, textColor, ref eventId, request.InventoryId, warehouseId, true, false));
                                }

                                //out (owned and consigned)
                                if ((reservation.QuantityOut.OwnedAndConsigned != 0) && ((reservation.FromDateTime <= request.ToDate) && (reservation.ToDateTime >= request.FromDate)))
                                {
                                    response.InventoryAvailabilityScheduleResources.Add(newScheduleResource(ref resourceId, reservation.ScheduleResourceDescription));

                                    backColor = RwGlobals.AVAILABILITY_COLOR_RESERVED;
                                    barColor = RwGlobals.OUT_COLOR;
                                    textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_RESERVED;
                                    if (reservation.IsTransfer)
                                    {
                                        barColor = RwGlobals.IN_TRANSIT_COLOR;
                                    }
                                    else if (reservation.IsContainer)
                                    {
                                        barColor = RwGlobals.CONTAINER_COLOR;
                                    }
                                    else if (reservation.IsPendingExchange)
                                    {
                                        barColor = RwGlobals.PENDING_EXCHANGE_COLOR;
                                    }
                                    qty = reservation.QuantityOut.OwnedAndConsigned;
                                    response.InventoryAvailabilityScheduleEvents.AddRange(buildScheduleReservationEvents(resourceId, reservation, qty, backColor, barColor, textColor, ref eventId, request.InventoryId, warehouseId, false, false));
                                }

                                //out (subbed)
                                if ((reservation.QuantityOut.Subbed != 0) && ((reservation.FromDateTime <= request.ToDate) && (reservation.ToDateTime >= request.FromDate)))
                                {
                                    response.InventoryAvailabilityScheduleResources.Add(newScheduleResource(ref resourceId, reservation.ScheduleResourceDescription));

                                    barColor = RwGlobals.OUT_COLOR;
                                    backColor = RwGlobals.SUB_COLOR;
                                    textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_RESERVED;
                                    if (reservation.IsTransfer)
                                    {
                                        barColor = RwGlobals.IN_TRANSIT_COLOR;
                                    }
                                    else if (reservation.IsPendingExchange)
                                    {
                                        barColor = RwGlobals.PENDING_EXCHANGE_COLOR;
                                    }
                                    qty = reservation.QuantityOut.Subbed;
                                    response.InventoryAvailabilityScheduleEvents.AddRange(buildScheduleReservationEvents(resourceId, reservation, qty, backColor, barColor, textColor, ref eventId, request.InventoryId, warehouseId, true, false));
                                }

                                //repair
                                if ((reservation.QuantityInRepair.Total != 0) && ((reservation.FromDateTime <= request.ToDate) && (reservation.ToDateTime >= request.FromDate)))
                                {
                                    response.InventoryAvailabilityScheduleResources.Add(newScheduleResource(ref resourceId, reservation.ScheduleResourceDescription));

                                    barColor = RwGlobals.IN_REPAIR_COLOR;
                                    backColor = RwGlobals.AVAILABILITY_COLOR_RESERVED;
                                    textColor = RwGlobals.AVAILABILITY_TEXT_COLOR_RESERVED;
                                    qty = reservation.QuantityInRepair.Total;
                                    response.InventoryAvailabilityScheduleEvents.AddRange(buildScheduleReservationEvents(resourceId, reservation, qty, backColor, barColor, textColor, ref eventId, request.InventoryId, warehouseId, false, false));
                                }
                            }
                        }
                    }
                }
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------

        public static async Task<AvailabilityConflictResponse> GetConflicts(FwApplicationConfig appConfig, FwUserSession userSession, AvailabilityConflictRequest request)
        {
            AvailabilityConflictResponse response = new AvailabilityConflictResponse();
            FwJsonDataTable dt = null;

            using (FwSqlConnection conn = new FwSqlConnection(appConfig.DatabaseSettings.ConnectionString))
            {
                FwSqlSelect select = new FwSqlSelect();
                select.EnablePaging = false;
                select.UseOptionRecompile = true;
                using (FwSqlCommand qry = new FwSqlCommand(conn, appConfig.DatabaseSettings.QueryTimeout))
                {
                    qry.AddColumn("masterid");
                    qry.AddColumn("warehouseid");

                    select.Add("select a.masterid, a.warehouseid                    ");
                    select.Add(" from  availabilitymasterwhview a with (nolock)     ");

                    select.Parse();

                    if ((!string.IsNullOrEmpty(request.AvailableFor)) && (!request.AvailableFor.Equals("ALL")))
                    {
                        select.AddWhere("a.availfor = @availfor");
                        select.AddParameter("@availfor", request.AvailableFor);
                    }

                    select.AddWhereIn("warehouseid", request.WarehouseId);
                    select.AddWhereIn("inventorydepartmentid", request.InventoryTypeId);
                    select.AddWhereIn("categoryid", request.CategoryId);
                    select.AddWhereIn("subcategoryid", request.SubCategoryId);
                    select.AddWhereIn("masterid", request.InventoryId);
                    select.AddWhereIn("rank", request.Ranks);
                    if (!string.IsNullOrEmpty(request.Description))
                    {
                        select.AddWhere("a.master like @description");
                        select.AddParameter("@description", "%" + request.Description.Trim() + "%");
                    }

                    //if (!request.BooleanField.GetValueOrDefault(false)) 
                    //{ 
                    //    select.AddWhere("somefield ^<^> 'T'"); 
                    //} 
                    select.AddOrderBy("warehouse, inventorydepartment, category, subcategory, masterno, master");
                    dt = await qry.QueryToFwJsonTableAsync(select, false);
                }

            }

            DateTime fromDateTime = DateTime.Today;
            DateTime toDateTime = request.ToDate ?? fromDateTime.AddDays(availabilityDaysToCache);
            TInventoryWarehouseAvailabilityRequestItems availRequestItems = new TInventoryWarehouseAvailabilityRequestItems();
            //foreach (List<object> row in dt.Rows)
            //{
            //    string inventoryId = row[dt.GetColumnNo("masterid")].ToString();
            //    string warehouseId = row[dt.GetColumnNo("warehouseid")].ToString();
            //    availRequestItems.Add(new TInventoryWarehouseAvailabilityRequestItem(inventoryId, warehouseId, fromDateTime, toDateTime));
            //}
            bool refreshIfNeeded = false;// true; // user may want to make this true/false in some cases
            //TAvailabilityCache availCache = InventoryAvailabilityFunc.GetAvailability(appConfig, userSession, availRequestItems, refreshIfNeeded, forceRefresh: false).Result;


            foreach (List<object> row in dt.Rows)
            {
                string inventoryId = row[dt.GetColumnNo("masterid")].ToString();
                string warehouseId = row[dt.GetColumnNo("warehouseid")].ToString();

                //TInventoryWarehouseAvailabilityKey availKey = new TInventoryWarehouseAvailabilityKey(inventoryId, warehouseId);

                //TInventoryWarehouseAvailability availData = null;
                //if (availCache.TryGetValue(availKey, out availData))
                TInventoryWarehouseAvailability availData = InventoryAvailabilityFunc.GetAvailability(appConfig, userSession, inventoryId, warehouseId, fromDateTime, toDateTime, refreshIfNeeded, forceRefresh: false).Result;
                if (availData != null)
                {
                    bool hasConflict = (((string.IsNullOrEmpty(request.ConflictType) || (request.ConflictType.Equals("ALL"))) && (availData.HasNegativeConflict || availData.HasPositiveConflict)) ||
                                       (!string.IsNullOrEmpty(request.ConflictType) && request.ConflictType.Equals(RwConstants.INVENTORY_CONFLICT_TYPE_NEGATIVE) && availData.HasNegativeConflict) ||
                                       (!string.IsNullOrEmpty(request.ConflictType) && request.ConflictType.Equals(RwConstants.INVENTORY_CONFLICT_TYPE_POSITIVE) && availData.HasPositiveConflict));

                    if (hasConflict)
                    {
                        foreach (TInventoryWarehouseAvailabilityReservation reservation in availData.Reservations)
                        {
                            if (reservation.QuantityReserved.Total != 0)
                            {

                                bool isConflict = (((string.IsNullOrEmpty(request.ConflictType) || (request.ConflictType.Equals("ALL"))) && (reservation.IsNegativeConflict || reservation.IsPositiveConflict)) ||
                                                   (!string.IsNullOrEmpty(request.ConflictType) && request.ConflictType.Equals(RwConstants.INVENTORY_CONFLICT_TYPE_NEGATIVE) && reservation.IsNegativeConflict) ||
                                                   (!string.IsNullOrEmpty(request.ConflictType) && request.ConflictType.Equals(RwConstants.INVENTORY_CONFLICT_TYPE_POSITIVE) && reservation.IsPositiveConflict));

                                bool showOrderOrDeal = true;
                                if (showOrderOrDeal)
                                {
                                    if (!string.IsNullOrEmpty(request.OrderId))
                                    {
                                        showOrderOrDeal = reservation.OrderId.Equals(request.OrderId);
                                    }
                                }
                                if (showOrderOrDeal)
                                {
                                    if (!string.IsNullOrEmpty(request.DealId))
                                    {
                                        showOrderOrDeal = reservation.DealId.Equals(request.DealId);
                                    }
                                }

                                if (isConflict && showOrderOrDeal)
                                {
                                    AvailabilityConflictResponseItem responseItem = new AvailabilityConflictResponseItem();
                                    responseItem.Warehouse = availData.InventoryWarehouse.Warehouse;
                                    responseItem.WarehouseCode = availData.InventoryWarehouse.WarehouseCode;
                                    responseItem.InventoryTypeId = availData.InventoryWarehouse.InventoryTypeId;
                                    responseItem.InventoryType = availData.InventoryWarehouse.InventoryType;
                                    responseItem.CategoryId = availData.InventoryWarehouse.CategoryId;
                                    responseItem.Category = availData.InventoryWarehouse.Category;
                                    responseItem.SubCategoryId = availData.InventoryWarehouse.SubCategoryId;
                                    responseItem.SubCategory = availData.InventoryWarehouse.SubCategory;
                                    responseItem.InventoryId = availData.InventoryWarehouse.InventoryId;
                                    responseItem.ICode = availData.InventoryWarehouse.ICode;
                                    responseItem.ItemDescription = availData.InventoryWarehouse.Description;
                                    responseItem.QuantityIn = availData.In.Total;
                                    responseItem.QuantityInRepair = availData.InRepair.Total;
                                    responseItem.OrderId = reservation.OrderId;
                                    responseItem.OrderType = reservation.OrderType;
                                    responseItem.OrderNumber = reservation.OrderNumber;
                                    responseItem.OrderDescription = reservation.OrderDescription;
                                    responseItem.DealId = reservation.DealId;
                                    responseItem.Deal = reservation.Deal;
                                    responseItem.QuantityReserved = reservation.QuantityReserved.Total;
                                    responseItem.QuantitySub = reservation.QuantitySub;
                                    responseItem.FromDateTime = reservation.FromDateTime;
                                    responseItem.ToDateTime = reservation.ToDateTime;

                                    responseItem.FromDateTimeString = FwConvert.ToShortDate(reservation.FromDateTime);
                                    responseItem.ToDateTimeString = (reservation.ToDateTime.Equals(LateDateTime) ? "LATE" : FwConvert.ToShortDate(reservation.ToDateTime));

                                    TInventoryWarehouseAvailabilityMinimum minAvail = availData.GetMinimumAvailableQuantity(reservation.FromDateTime, reservation.ToDateTime);
                                    if ((!minAvail.IsStale) && (reservation.QuantitySub > 0) && (minAvail.MinimumAvailable.Total > reservation.QuantitySub))
                                    {
                                        minAvail.AvailabilityState = RwConstants.AVAILABILITY_STATE_POSITIVE_CONFLICT;
                                    }

                                    responseItem.QuantityAvailable = minAvail.MinimumAvailable.Total;
                                    responseItem.AvailabilityIsStale = minAvail.IsStale;
                                    responseItem.AvailabilityState = minAvail.AvailabilityState;
                                    responseItem.QuantityLate = availData.Late.Total;
                                    responseItem.QuantityQc = availData.QcRequired.Total;
                                    response.Add(responseItem);
                                }
                            }
                        }
                    }
                }
            }

            return response;
        }
        //-------------------------------------------------------------------------------------------------------
    }
}
