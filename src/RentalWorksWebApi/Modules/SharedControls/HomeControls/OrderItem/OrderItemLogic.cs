using FwStandard.AppManager;
using FwStandard.BusinessLogic;
using Newtonsoft.Json;
using System.Collections.Generic;
using WebApi.Logic;
using WebApi.Modules.Agent.Order;
using WebApi.Modules.HomeControls.InventoryAvailability;
using WebApi.Modules.HomeControls.MasterItem;
using WebApi.Modules.Home.MasterItemDetail;
using FwStandard.Models;

namespace WebApi.Modules.HomeControls.OrderItem
{
    [FwLogic(Id: "N05EVbv5HL3y")]
    public class OrderItemLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        MasterItemRecord orderItem = new MasterItemRecord();
        MasterItemDetailRecord orderItemDetail = new MasterItemDetailRecord();

        OrderItemLoader orderItemLoader = new OrderItemLoader();

        public OrderItemLogic()
        {
            dataRecords.Add(orderItem);
            dataRecords.Add(orderItemDetail);

            dataLoader = orderItemLoader;

            BeforeSave += OnBeforeSave;
            AfterSave += OnAfterSave;

            orderItem.AfterSave += OnAfterSaveMasterItem;

            AfterDelete += OnAfterDelete;
            UseTransactionToDelete = true;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "j5BoEx9ak5Ry", IsPrimaryKey: true)]
        public string OrderItemId { get { return orderItem.MasterItemId; } set { orderItem.MasterItemId = value; orderItemDetail.MasterItemId = value; } }

        [FwLogicProperty(Id: "OAKJ6N3eUpao")]
        public string OrderId { get { return orderItem.OrderId; } set { orderItem.OrderId = value; orderItemDetail.OrderId = value; } }

        [FwLogicProperty(Id: "AsMKdufgM74qt")]
        public bool? RowsRolledUp { get; set; }

        [FwLogicProperty(Id: "OeNeaMjQUYmLH")]
        public string RolledUpIds { get; set; }

        //this field is called IsPrimaryKeyOptional only to allow the FrameWork to pass it to the Loader.  Allows developer to foce the detail row to be loaded when desired
        [JsonIgnore]
        [FwLogicProperty(Id: "ovkYGK5CTBmcX", IsReadOnly: true, IsPrimaryKeyOptional: true)]
        public bool? DetailOnly { get; set; }

        [FwLogicProperty(Id: "6sOCOcNV2gVV")]
        public string RecType { get { return orderItem.RecType; } set { orderItem.RecType = value; } }

        [FwLogicProperty(Id: "BK5pcd3yhqHk", IsReadOnly: true)]
        public string RecTypeDisplay { get; set; }

        [FwLogicProperty(Id: "NtiA3zTf3Trc", IsReadOnly: true)]
        public int? RowNumber { get; set; }

        [FwLogicProperty(Id: "QD4tvO0cAUb7")]
        public string InventoryId { get { return orderItem.InventoryId; } set { orderItem.InventoryId = value; } }

        [FwLogicProperty(Id: "xIkgrGEWdaUB", IsReadOnly: true)]
        public string ICode { get; set; }

        [FwLogicProperty(Id: "lFFaCwPq4CVY")]
        public string ICodeColor { get; set; }

        [FwLogicProperty(Id: "2UNmRTEnzcLN")]
        public string Description { get { return orderItem.Description; } set { orderItem.Description = value; } }

        [FwLogicProperty(Id: "yBhefWjf1iXP")]
        public string DescriptionColor { get; set; }

        [FwLogicProperty(Id: "Fi6gMVn3kom1")]
        public string PickDate { get { return orderItem.PickDate; } set { orderItem.PickDate = value; } }

        [FwLogicProperty(Id: "SnnaBDaibQIM")]
        public string PickTime { get { return orderItem.PickTime; } set { orderItem.PickTime = value; } }

        [FwLogicProperty(Id: "p9q9hxotQsNZ")]
        public string FromDate { get { return orderItem.FromDate; } set { orderItem.FromDate = value; } }

        [FwLogicProperty(Id: "2ARsSnjZ9U6S")]
        public string FromTime { get { return orderItem.FromTime; } set { orderItem.FromTime = value; } }

        [FwLogicProperty(Id: "fzUKdQ5dcMVA")]
        public string ToDate { get { return orderItem.ToDate; } set { orderItem.ToDate = value; } }

        [FwLogicProperty(Id: "OMyyJjNQ4xB8")]
        public string ToTime { get { return orderItem.ToTime; } set { orderItem.ToTime = value; } }

        [FwLogicProperty(Id: "eODVDwIqgxZD", IsReadOnly: true)]
        public decimal? BillablePeriods { get; set; }

        [FwLogicProperty(Id: "4zAONsnFq94R")]
        public decimal? QuantityOrdered { get { return orderItem.QuantityOrdered; } set { orderItem.QuantityOrdered = value; } }

        [FwLogicProperty(Id: "sEGn6Ws6ZPJpz", IsReadOnly: true)]
        public string QuantityColor { get; set; }

        [FwLogicProperty(Id: "fzTd0fTQvjjS")]
        public decimal? SubQuantity { get { return orderItem.SubQuantity; } set { orderItem.SubQuantity = value; } }

        [FwLogicProperty(Id: "0bMbDmb0dtJT", IsReadOnly: true)]
        public string SubQuantityColor { get; set; }

        [FwLogicProperty(Id: "m19auC9SJFxP")]
        public int? ConsignQuantity { get { return orderItem.ConsignQuantity; } set { orderItem.ConsignQuantity = value; } }

        [FwLogicProperty(Id: "0DIiGv2TZoYQ", IsReadOnly: true)]
        public int? ReservedItemQuantity { get; set; }

        [FwLogicProperty(Id: "9GCGUd4nSEzY", IsReadOnly: true, IsNotAudited: true)]
        public decimal? AvailableQuantity { get; set; }

        [FwLogicProperty(Id: "eNrj2HGEqivOG", IsReadOnly: true, IsNotAudited: true)]
        public string AvailabilityState { get; set; }

        [FwLogicProperty(Id: "72nuyMc1ObMF", IsReadOnly: true, IsNotAudited: true)]
        public decimal? AvailableAllWarehousesQuantity { get; set; }

        [FwLogicProperty(Id: "uyqTEgf63XVZ", IsReadOnly: true, IsNotAudited: true)]
        public string ConflictDate { get; set; }

        [FwLogicProperty(Id: "uyqTEgf63XVZ", IsReadOnly: true, IsNotAudited: true)]
        public string ConflictDateAllWarehouses { get; set; }

        [FwLogicProperty(Id: "uyqTEgf63XVZ", IsReadOnly: true, IsNotAudited: true)]
        public string ConflictDateConsignment { get; set; }

        [FwLogicProperty(Id: "iveRLjAjbZfE")]
        public string UnitId { get { return orderItem.UnitId; } set { orderItem.UnitId = value; } }

        [FwLogicProperty(Id: "lOxm8tMSUg0H", IsReadOnly: true)]
        public string Unit { get; set; }

        [FwLogicProperty(Id: "afqSIpqM2zRl")]
        public decimal? UnitCost { get { return orderItem.UnitCost; } set { orderItem.UnitCost = value; } }

        [FwLogicProperty(Id: "oviFmLXKieOY")]
        public decimal? MarginPercent { get { return orderItem.MarginPercent; } set { orderItem.MarginPercent = value; } }

        [FwLogicProperty(Id: "sqHWUPCwXBV7")]
        public decimal? MarkupPercent { get { return orderItem.MarkupPercent; } set { orderItem.MarkupPercent = value; } }

        [FwLogicProperty(Id: "ax63RXYHuhIg")]
        public decimal? PremiumPercent { get { return orderItem.PremiumPercent; } set { orderItem.PremiumPercent = value; } }



        [FwLogicProperty(Id: "wHykSivokIui")]
        public string CrewContactId { get { return orderItem.CrewContactId; } set { orderItem.CrewContactId = value; } }

        [FwLogicProperty(Id: "hq5SJLid3NUW", IsReadOnly: true)]
        public string CrewName { get; set; }

        [FwLogicProperty(Id: "O6sFxr5sDp9g")]
        public decimal? Hours { get { return orderItem.Hours; } set { orderItem.Hours = value; } }

        [FwLogicProperty(Id: "pXJCm4PiQysJ")]
        public decimal? HoursOvertime { get { return orderItem.HoursOvertime; } set { orderItem.HoursOvertime = value; } }

        [FwLogicProperty(Id: "Gmv7ENhx95Vp")]
        public decimal? HoursDoubletime { get { return orderItem.HoursDoubletime; } set { orderItem.HoursDoubletime = value; } }



        [FwLogicProperty(Id: "942P4NHhJ4oI")]
        public decimal? Price { get { return orderItem.Price; } set { orderItem.Price = value; } }

        [FwLogicProperty(Id: "VP8DplnNZi1q")]
        public decimal? Price2 { get { return orderItem.Price2; } set { orderItem.Price2 = value; } }

        [FwLogicProperty(Id: "JyeG0VBB1DLv")]
        public decimal? Price3 { get { return orderItem.Price3; } set { orderItem.Price3 = value; } }

        [FwLogicProperty(Id: "UcLWk6jILxt1")]
        public decimal? Price4 { get { return orderItem.Price4; } set { orderItem.Price4 = value; } }

        [FwLogicProperty(Id: "VcaO5H1IJ4AB")]
        public decimal? Price5 { get { return orderItem.Price5; } set { orderItem.Price5 = value; } }

        [FwLogicProperty(Id: "4inUtg3xbuNn")]
        public decimal? DaysPerWeek { get { return orderItem.DaysPerWeek; } set { orderItem.DaysPerWeek = value; } }

        [FwLogicProperty(Id: "LGRvNm8smgXM")]
        public decimal? DiscountPercent { get { return orderItem.DiscountPercent; } set { orderItem.DiscountPercent = value; } }

        [FwLogicProperty(Id: "XveWv5CJHrHG", IsReadOnly: true)]
        public decimal? DiscountPercentDisplay { get; set; }





        [FwLogicProperty(Id: "lOxm8tMSUg0H", IsReadOnly: true)]
        public decimal? UnitExtendedNoDiscount { get; set; }

        [FwLogicProperty(Id: "lOxm8tMSUg0H", IsReadOnly: true)]
        public decimal? UnitDiscountAmount { get; set; }

        [FwLogicProperty(Id: "lOxm8tMSUg0H", IsReadOnly: true)]
        public decimal? UnitExtended { get; set; }

        [FwLogicProperty(Id: "VCuU57T6mUQT", IsReadOnly: true)]
        public decimal? WeeklyExtendedNoDiscount { get; set; }

        [FwLogicProperty(Id: "U6SQuOYSOav3", IsReadOnly: true)]
        public decimal? WeeklyDiscountAmount { get; set; }

        [FwLogicProperty(Id: "VCuU57T6mUQT", IsReadOnly: true)]
        public decimal? WeeklyExtended { get; set; }

        [FwLogicProperty(Id: "SjBEnC0Gw7W6", IsReadOnly: true)]
        public decimal? WeeklyCostExtended { get; set; }

        [FwLogicProperty(Id: "8FotF6KXPAc9", IsReadOnly: true)]
        public decimal? WeeklyTax { get; set; }

        [FwLogicProperty(Id: "FUB8yskSCpCVj", IsReadOnly: true)]
        public decimal? WeeklyTotal { get; set; }

        [FwLogicProperty(Id: "mBjGTUbEP0DD")]
        public decimal? Week2Extended { get; set; }

        [FwLogicProperty(Id: "5AQ6ngTbtAdt", IsReadOnly: true)]
        public decimal? Week3Extended { get; set; }

        [FwLogicProperty(Id: "zTEjmXFtaony", IsReadOnly: true)]
        public decimal? Weeks1Through3Extended { get; set; }

        [FwLogicProperty(Id: "XmTkwFvdRIfO", IsReadOnly: true)]
        public decimal? Weeks4PlusExtended { get; set; }

        [FwLogicProperty(Id: "SKi7wZVHuMLb", IsReadOnly: true)]
        public decimal? Week4Extended { get; set; }

        [FwLogicProperty(Id: "0f68TPtI5WWo", IsReadOnly: true)]
        public decimal? AverageWeeklyExtended { get; set; }

        [FwLogicProperty(Id: "0f68TPtI5WWo", IsReadOnly: true)]
        public decimal? AverageWeeklyExtendedNoDiscount { get; set; }

        [FwLogicProperty(Id: "ab4SmyGMUuyn", IsReadOnly: true)]
        public int? Episodes { get; set; }

        [FwLogicProperty(Id: "Khz9ko84K1xi", IsReadOnly: true)]
        public decimal? MonthlyExtendedNoDiscount { get; set; }

        [FwLogicProperty(Id: "huBd3kB8ekSm", IsReadOnly: true)]
        public decimal? MonthlyDiscountAmount { get; set; }

        [FwLogicProperty(Id: "Khz9ko84K1xi", IsReadOnly: true)]
        public decimal? MonthlyExtended { get; set; }

        [FwLogicProperty(Id: "9tJ4UHRdwI30", IsReadOnly: true)]
        public decimal? MonthlyCostExtended { get; set; }

        [FwLogicProperty(Id: "NnVFcSmTD2zF", IsReadOnly: true)]
        public decimal? MonthlyTax { get; set; }

        [FwLogicProperty(Id: "SvZ6addubgvnC", IsReadOnly: true)]
        public decimal? MonthlyTotal { get; set; }

        [FwLogicProperty(Id: "16fZQS3NTlTW")]
        public decimal? PeriodExtendedNoDiscount { get; set; }

        [FwLogicProperty(Id: "JNUQaXwAg2dB", IsReadOnly: true)]
        public decimal? PeriodCostExtended { get; set; }

        [FwLogicProperty(Id: "0olV7E3aEent", IsReadOnly: true)]
        public decimal? PeriodDiscountAmount { get; set; }

        [FwLogicProperty(Id: "n3nkqImWmZK1", IsReadOnly: true)]
        public decimal? PeriodExtended { get; set; }

        [FwLogicProperty(Id: "Fz0tQ5thjcfX", IsReadOnly: true)]
        public decimal? PeriodTax { get; set; }

        [FwLogicProperty(Id: "7N4hypXEFwSb7", IsReadOnly: true)]
        public decimal? PeriodTotal { get; set; }

        [FwLogicProperty(Id: "OqjfCDLbWBhp", IsReadOnly: true)]
        public decimal? PeriodVarianceExtended { get; set; }

        [FwLogicProperty(Id: "CZsuRPakxuvE", IsReadOnly: true)]
        public decimal? VariancePercent { get; set; }







        [FwLogicProperty(Id: "gvVBDhMHpiqR")]
        public bool? Bold { get { return orderItem.Bold; } set { orderItem.Bold = value; } }

        [FwLogicProperty(Id: "3t82OibLduM4")]
        public bool? Locked { get { return orderItem.Locked; } set { orderItem.Locked = value; } }

        [FwLogicProperty(Id: "4sm1QsukezOU")]
        public bool? Taxable { get { return orderItem.Taxable; } set { orderItem.Taxable = value; } }


        [FwLogicProperty(Id: "oYEkf28FoUTz")]
        public string WarehouseId { get { return orderItem.WarehouseId; } set { orderItem.WarehouseId = value; } }

        [FwLogicProperty(Id: "rT3jbk0lrJjR", IsReadOnly: true)]
        public string WarehouseCode { get; set; }

        [FwLogicProperty(Id: "PdTsToHIWi3c")]
        public string ReturnToWarehouseId { get { return orderItem.ReturnToWarehouseId; } set { orderItem.ReturnToWarehouseId = value; } }

        [FwLogicProperty(Id: "dWKczDxqA9Jh", IsReadOnly: true)]
        public string ReturnToWarehouseCode { get; set; }

        [FwLogicProperty(Id: "RT2U3nU2HIK0", IsReadOnly: true)]
        public string Notes { get; set; }

        [FwLogicProperty(Id: "Qlong658w5Mh", IsReadOnly: true)]
        public string ItemOrder { get; set; }


        [FwLogicProperty(Id: "wEauLrU45GML")]
        public string ParentId { get { return orderItem.ParentId; } set { orderItem.ParentId = value; } }

        [FwLogicProperty(Id: "KumU8jNjH7gBz")]
        public string NestedOrderItemId { get { return orderItem.NestedOrderItemId; } set { orderItem.NestedOrderItemId = value; } }

        [FwLogicProperty(Id: "VcghZbMujni2")]
        public string ItemClass { get { return orderItem.ItemClass; } set { orderItem.ItemClass = value; } }


        [FwLogicProperty(Id: "dN4KvdTOsjK0")]
        public string RetiredReasonId { get { return orderItem.RetiredReasonId; } set { orderItem.RetiredReasonId = value; } }

        [FwLogicProperty(Id: "hoawNFkayBP8", IsReadOnly: true)]
        public string RetiredReason { get; set; }

        [FwLogicProperty(Id: "cOAWsfYNbYIZ")]
        public string ItemId { get { return orderItem.ItemId; } set { orderItem.ItemId = value; } }

        [FwLogicProperty(Id: "evXOlAWq1ZkY", IsReadOnly: true)]
        public string BarCode { get; set; }

        [FwLogicProperty(Id: "8b3GnpOIQeTg", IsReadOnly: true)]
        public string SerialNumber { get; set; }


        [FwLogicProperty(Id: "qi5o8wEKKUsY")]
        public string ManufacturerPartNumber { get { return orderItem.ManufacturerPartNumber; } set { orderItem.ManufacturerPartNumber = value; } }



        [FwLogicProperty(Id: "kjUv2AElj7gT", IsReadOnly: true)]
        public string PoSubOrderId { get; set; }

        [FwLogicProperty(Id: "j5BoEx9ak5Ry", IsReadOnly: true)]
        public string PoSubOrderItemId { get; set; }

        [FwLogicProperty(Id: "xA0AyEdCclax", IsReadOnly: true)]
        public string PoSubOrderNumber { get; set; }


        //------------------------------------------------------------------------------------ 

        [FwLogicProperty(Id: "kjUv2AElj7gT", IsReadOnly: true)]
        public string LossAndDamageOrderId { get; set; }

        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "j5BoEx9ak5Ry", IsReadOnly: true)]
        public string LossAndDamageOrderItemId { get; set; }

        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "hv01bK2gv9jb")]
        public string LossAndDamageOrderNumber { get; set; }

        //------------------------------------------------------------------------------------ 


        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "FLcnFK2Oltf6Q", IsReadOnly: true)]
        public bool? ModifiedAtStaging { get; set; }
        //------------------------------------------------------------------------------------ 

        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "9eVO0B4Y0YfAm")]
        public bool? Mute { get { return orderItemDetail.Mute; } set { orderItemDetail.Mute = value; } }
        //------------------------------------------------------------------------------------ 



        //[FwLogicProperty(Id:"RT2U3nU2HIK0", IsReadOnly:true)]
        //public string NotesmasteritemId { get; set; }

        //[FwLogicProperty(Id:"UgxcJNtEtlf7")]
        //public string PrimarymasteritemId { get { return orderItem.PrimarymasteritemId; } set { orderItem.PrimarymasteritemId = value; } }

        //[FwLogicProperty(Id:"EeB74TOngS57")]
        //public string OrgmasteritemId { get { return orderItem.OrgmasteritemId; } set { orderItem.OrgmasteritemId = value; } }

        //[FwLogicProperty(Id:"EQK6y1y2iJX1")]
        //public string NestedmasteritemId { get { return orderItem.NestedmasteritemId; } set { orderItem.NestedmasteritemId = value; } }

        //[FwLogicProperty(Id:"wMQMfjm8r4xP")]
        //public string PackageitemId { get; set; }

        //[FwLogicProperty(Id:"NDwdbuYI6Y5h")]
        //public string CategoryId { get; set; }

        //[FwLogicProperty(Id:"gejVcKOnKBKA")]
        //public string Orderno { get; set; }

        //[FwLogicProperty(Id:"X7O86UKeRvub")]
        //public bool? SessionId { get; set; }

        //[FwLogicProperty(Id:"XcYWduYVHyGg")]
        //public bool? Sessionno { get; set; }

        //[FwLogicProperty(Id:"6tgoMpBuooFT")]
        //public bool? Sessionlocation { get; set; }

        //[FwLogicProperty(Id:"jaKcJP2o9C1Q")]
        //public bool? Sessionroom { get; set; }

        //[FwLogicProperty(Id:"lcfByp2hESN0")]
        //public int? Sessionorderby { get; set; }

        //[FwLogicProperty(Id:"BmtiG8ou9jBZ")]
        //public bool? Issession { get; set; }

        //[FwLogicProperty(Id:"GNvnkVMmfWPc")]
        //public bool? Rowtype { get; set; }

        //[FwLogicProperty(Id:"jJK7nfU6ux1O")]
        //public string Mfgpartno { get { return orderItem.Mfgpartno; } set { orderItem.Mfgpartno = value; } }

        //[FwLogicProperty(Id:"KQ7ninxT90v4")]
        //public decimal? SubQuantity { get { return orderItem.SubQuantity; } set { orderItem.SubQuantity = value; } }

        //[FwLogicProperty(Id:"T3Hx8CczzWzS")]
        //public int? InlocationQuantity { get; set; }

        //[FwLogicProperty(Id:"MQVi9DpraSya")]
        //public decimal? Price { get { return orderItem.Price; } set { orderItem.Price = value; } }

        //[FwLogicProperty(Id:"Z3dJamFNID6O")]
        //public decimal? Price2 { get { return orderItem.Price2; } set { orderItem.Price2 = value; } }

        //[FwLogicProperty(Id:"eDMI8N6kr3p3")]
        //public decimal? Price3 { get { return orderItem.Price3; } set { orderItem.Price3 = value; } }

        //[FwLogicProperty(Id:"SZjRV5spiRDc")]
        //public decimal? Price4 { get { return orderItem.Price4; } set { orderItem.Price4 = value; } }

        //[FwLogicProperty(Id:"4WOdNz2r9ea5")]
        //public decimal? Price5 { get { return orderItem.Price5; } set { orderItem.Price5 = value; } }

        //[FwLogicProperty(Id:"H7i5RGNUbHR4")]
        //public decimal? Cost { get { return orderItem.Cost; } set { orderItem.Cost = value; } }

        //[FwLogicProperty(Id:"2bjDCZX6OI1p")]
        //public decimal? Daysinwk { get { return orderItem.Daysinwk; } set { orderItem.Daysinwk = value; } }

        //[FwLogicProperty(Id:"r8R2N5hMqkMk")]
        //public decimal? Hours { get { return orderItem.Hours; } set { orderItem.Hours = value; } }

        //[FwLogicProperty(Id:"x8BFoGjrwB2i")]
        //public decimal? Hoursot { get { return orderItem.Hoursot; } set { orderItem.Hoursot = value; } }

        //[FwLogicProperty(Id:"U87cB1GRs1mZ")]
        //public decimal? Hoursdt { get { return orderItem.Hoursdt; } set { orderItem.Hoursdt = value; } }

        //[FwLogicProperty(Id:"WbGZpTx6w8e4")]
        //public string Pickdate { get { return orderItem.Pickdate; } set { orderItem.Pickdate = value; } }

        //[FwLogicProperty(Id:"wHcEkVft10dB")]
        //public string Picktime { get { return orderItem.Picktime; } set { orderItem.Picktime = value; } }

        //[FwLogicProperty(Id:"mkzJiGRN2way")]
        //public string Rentfromdate { get { return orderItem.Rentfromdate; } set { orderItem.Rentfromdate = value; } }

        //[FwLogicProperty(Id:"kzS0AbECfqxt")]
        //public string Rentfromtime { get { return orderItem.Rentfromtime; } set { orderItem.Rentfromtime = value; } }

        //[FwLogicProperty(Id:"98tZIXra63Rw")]
        //public string Renttodate { get { return orderItem.Renttodate; } set { orderItem.Renttodate = value; } }

        //[FwLogicProperty(Id:"aISaLOysncut")]
        //public string Renttotime { get { return orderItem.Renttotime; } set { orderItem.Renttotime = value; } }

        //[FwLogicProperty(Id:"uyyTfGzMHQNi")]
        //public bool? Varyingdatestimes { get; set; }

        //[FwLogicProperty(Id:"e153VMZETrmA")]
        //public decimal? Discountpctdisplay { get; set; }

        //[FwLogicProperty(Id:"yrjAB6x4Epcz")]
        //public decimal? Discountpct { get { return orderItem.Discountpct; } set { orderItem.Discountpct = value; } }

        //[FwLogicProperty(Id:"QX4Qkk1ErB7W")]
        //public decimal? Markuppctdisplay { get; set; }

        //[FwLogicProperty(Id:"B3hQywGr4sl2")]
        //public decimal? Markuppct { get { return orderItem.Markuppct; } set { orderItem.Markuppct = value; } }

        //[FwLogicProperty(Id:"CqnOeLVi0zlX")]
        //public decimal? Marginpctdisplay { get; set; }

        //[FwLogicProperty(Id:"Hj6YM6iQqS8w")]
        //public decimal? Marginpct { get { return orderItem.Marginpct; } set { orderItem.Marginpct = value; } }

        //[FwLogicProperty(Id:"8Crm7IG6Pel4")]
        //public decimal? Premiumpctdisplay { get; set; }

        //[FwLogicProperty(Id:"ih4gsGBT9UMa")]
        //public decimal? Premiumpct { get { return orderItem.Premiumpct; } set { orderItem.Premiumpct = value; } }

        //[FwLogicProperty(Id:"AwA12j6nEiLo")]
        //public int? Split { get { return orderItem.Split; } set { orderItem.Split = value; } }

        //[FwLogicProperty(Id:"LVtF1JliSYQX")]
        //public string Whcodesummary { get; set; }

        //[FwLogicProperty(Id:"opYdFgsFPjvw")]
        //public string Returntowhcode { get; set; }

        //[FwLogicProperty(Id:"6zYpoTAkUn9G")]
        //public string Returntowhcodesummary { get; set; }

        //[FwLogicProperty(Id:"LLAhnEfjGAkX")]
        //public string Warehouseidsummary { get; set; }

        //[FwLogicProperty(Id:"LH59KuUUfUZG")]
        //public string ReturntowarehouseId { get { return orderItem.ReturntowarehouseId; } set { orderItem.ReturntowarehouseId = value; } }

        //[FwLogicProperty(Id:"SGGYUg0vqK02")]
        //public string Returntowarehouseidsummary { get; set; }

        //[FwLogicProperty(Id:"xQvArv0M7DuN")]
        //public bool? Taxable { get { return orderItem.Taxable; } set { orderItem.Taxable = value; } }

        //[FwLogicProperty(Id:"0uQthxVPxF2I")]
        //public bool? Manualbillflg { get { return orderItem.Manualbillflg; } set { orderItem.Manualbillflg = value; } }

        //[FwLogicProperty(Id:"zfaUhISidS17")]
        //public string UnitId { get { return orderItem.UnitId; } set { orderItem.UnitId = value; } }

        //[FwLogicProperty(Id:"lOxm8tMSUg0H", IsReadOnly:true)]
        //public string Unit { get; set; }

        //[FwLogicProperty(Id:"lOxm8tMSUg0H", IsReadOnly:true)]
        //public string Unittype { get; set; }

        //public string Itemclass { get { return orderItem.Itemclass; } set { orderItem.Itemclass = value; } }
        //[FwBusinessLogicField(isReadOnly: true)]
        //public string Masterclass { get; set; }
        //[FwLogicProperty(Id:"mRbS2nESykyw")]
        //public bool? Masterinactive { get; set; }

        //[FwLogicProperty(Id:"JDLy5r0n2tLL")]
        //public string ParentmasterId { get; set; }

        //[FwLogicProperty(Id:"yy6VrbDMwIji")]
        //public string ParentparentId { get; set; }

        //[FwLogicProperty(Id:"RI1F71t3QRMS")]
        //public bool? Candiscount { get; set; }

        //[FwLogicProperty(Id:"4ZnV6Zi68SZj")]
        //public bool? Optioncolor { get; set; }

        //[FwLogicProperty(Id:"xG7qNXcr0eCx")]
        //public string LdorderId { get { return orderItem.LdorderId; } set { orderItem.LdorderId = value; } }

        //[FwLogicProperty(Id:"ZVfwLqsbhv47")]
        //public string LdmasteritemId { get { return orderItem.LdmasteritemId; } set { orderItem.LdmasteritemId = value; } }

        //[FwLogicProperty(Id:"zP6lx9tF2Evb")]
        //public string LdoutcontractId { get { return orderItem.LdoutcontractId; } set { orderItem.LdoutcontractId = value; } }

        //[FwLogicProperty(Id:"lRBvrxpIUiOj")]
        //public string LdpoId { get; set; }

        //[FwLogicProperty(Id:"xnd9Yve3Bomk")]
        //public string LdpoitemId { get; set; }

        //[FwLogicProperty(Id:"nGNquvPvKG0y")]
        //public string PoorderId { get { return orderItem.PoorderId; } set { orderItem.PoorderId = value; } }

        //[FwLogicProperty(Id:"On9UEN8rnubj")]
        //public string PomasteritemId { get { return orderItem.PomasteritemId; } set { orderItem.PomasteritemId = value; } }

        //[FwLogicProperty(Id:"C29DpEYytkCc")]
        //public string PoId { get; set; }

        //[FwLogicProperty(Id:"DCscXHCZLmQj")]
        //public string PoitemId { get; set; }

        //[FwLogicProperty(Id:"xIjN7SdFQvlp")]
        //public bool? Haspoitem { get; set; }

        //[FwLogicProperty(Id:"FFlFx9hI80il")]
        //public bool? Returnflg { get { return orderItem.Returnflg; } set { orderItem.Returnflg = value; } }

        //[FwLogicProperty(Id:"WtsC5K44lpfr")]
        //public string RepairId { get { return orderItem.RepairId; } set { orderItem.RepairId = value; } }

        //[FwLogicProperty(Id:"P5WMhMlZe8LS")]
        //public bool? Hastieredcost { get; set; }

        //[FwLogicProperty(Id:"qYr9DNJXYAqs")]
        //public decimal? Retail { get; set; }

        //[FwLogicProperty(Id:"jkOwRQrBsN9e")]
        //public string ManufacturerId { get { return orderItem.ManufacturerId; } set { orderItem.ManufacturerId = value; } }

        //[FwLogicProperty(Id:"Ujc77laYj24S")]
        //public string Manufacturer { get; set; }

        //[FwLogicProperty(Id:"vc5tBFT9Xq2A")]
        //public string Partnumber { get; set; }

        //[FwLogicProperty(Id:"KJUCWGR44JIf")]
        //public string Vendorpartno { get { return orderItem.Vendorpartno; } set { orderItem.Vendorpartno = value; } }

        //[FwLogicProperty(Id:"Z5ncjGAB7R4n")]
        //public string Vehicleno { get; set; }

        //[FwLogicProperty(Id:"JAwdpFaXMIE0")]
        //public string Barcode { get; set; }

        //[FwLogicProperty(Id:"4aDuw76Dgq4w")]
        //public string Serialno { get; set; }

        //[FwLogicProperty(Id:"UpGGiPJJ0WsY")]
        //public decimal? Taxrate1 { get; set; }

        //[FwLogicProperty(Id:"ZDVEqwfZ9Nj7")]
        //public decimal? Taxrate2 { get; set; }

        //[FwLogicProperty(Id:"BwEWydAuJdDK")]
        //public bool? Recurringratetype { get; set; }

        //[FwLogicProperty(Id:"X8U8dbHuIKm5")]
        //public string Ldorderno { get; set; }

        //[FwLogicProperty(Id:"Ah10EMwkxlN2")]
        //public string Poorderno { get; set; }

        //[FwLogicProperty(Id:"oQzW0jejCl0q")]
        //public string Repairorderno { get; set; }

        //[FwLogicProperty(Id:"pWprs2EOF0dj")]
        //public string Mfgmodel { get { return orderItem.Mfgmodel; } set { orderItem.Mfgmodel = value; } }

        //[FwLogicProperty(Id:"J8hFasiYWkJx")]
        //public string CountryoforiginId { get { return orderItem.CountryoforiginId; } set { orderItem.CountryoforiginId = value; } }

        //[FwLogicProperty(Id:"Wd4mG4QKtJcG")]
        //public string Countryoforigin { get; set; }

        //[FwLogicProperty(Id:"8hSan2ucc3Hb")]
        //public bool? Excludefromquikpaydiscount { get { return orderItem.Excludefromquikpaydiscount; } set { orderItem.Excludefromquikpaydiscount = value; } }

        //[FwLogicProperty(Id:"818Q8VlWtuty")]
        //public int? Availcolor { get; set; }

        //[FwLogicProperty(Id:"bWBYZfwoEkdC")]
        //public int? Availcolorsummary { get; set; }

        //[FwLogicProperty(Id:"4KORqiix7Y6B")]
        //public int? Availcolorallwh { get; set; }

        //[FwLogicProperty(Id:"3jGVnVVfcJ0v")]
        //public int? Availcolorconsign { get; set; }

        //[FwLogicProperty(Id:"NhprpGFTOyOO")]
        //public int? Availcolorconsignsummary { get; set; }

        //[FwLogicProperty(Id:"yyFj801cHZ6n")]
        //public string Rectype { get { return orderItem.Rectype; } set { orderItem.Rectype = value; } }

        //[FwLogicProperty(Id:"JlNpANWmgdsh")]
        //public string Itemorder { get { return orderItem.Itemorder; } set { orderItem.Itemorder = value; } }

        //[FwLogicProperty(Id:"2QpUaMDmWSIw")]
        //public string Primaryitemorder { get; set; }

        //[FwLogicProperty(Id:"GZh9zJCoGZRj")]
        //public string RatemasterId { get { return orderItem.RatemasterId; } set { orderItem.RatemasterId = value; } }

        //[FwLogicProperty(Id:"8G11v5NFUdII")]
        //public bool? Noavail { get; set; }

        //[FwLogicProperty(Id:"IlhAZQVUbxfv")]
        //public bool? Availbyhour { get; set; }

        //[FwLogicProperty(Id:"3y9ffrjjQL8E")]
        //public bool? Availbyasset { get; set; }

        //[FwLogicProperty(Id:"xutpab0uXrGI")]
        //public bool? Availbydeal { get; set; }

        //[FwLogicProperty(Id:"pe0rMqtf18ze")]
        //public int? Availcachedays { get; set; }

        //[FwLogicProperty(Id:"QeGq1YjHSG5c")]
        //public string Availsequence { get { return orderItem.Availsequence; } set { orderItem.Availsequence = value; } }

        //[FwLogicProperty(Id:"PX8Kowb8lWCp")]
        //public bool? Conflict { get { return orderItem.Conflict; } set { orderItem.Conflict = value; } }

        //[FwLogicProperty(Id:"TnF6oEB8xS5f")]
        //public bool? Forceconflictflg { get { return orderItem.Forceconflictflg; } set { orderItem.Forceconflictflg = value; } }

        //[FwLogicProperty(Id:"UQexwPOOPSiE")]
        //public bool? Positiveconflict { get; set; }

        //[FwLogicProperty(Id:"16GRcGp0AVkH")]
        //public bool? Issplit { get; set; }

        //[FwLogicProperty(Id:"UOMNvOgePOzl")]
        //public bool? Isrecurring { get; set; }

        //[FwLogicProperty(Id:"pGk5lZbklc4K")]
        //public bool? Ismultivendorinvoice { get; set; }

        //[FwLogicProperty(Id:"l02kXYz3VUrP")]
        //public string RentalitemId { get { return orderItem.RentalitemId; } set { orderItem.RentalitemId = value; } }

        //[FwLogicProperty(Id:"rq0IrWdo6nkJ")]
        //public string CrewcontactId { get { return orderItem.CrewcontactId; } set { orderItem.CrewcontactId = value; } }

        //[FwLogicProperty(Id:"38adLLTOl6it")]
        //public string Crewname { get; set; }

        //[FwLogicProperty(Id:"gjWmncVOy6MU")]
        //public string Discountoverride { get; set; }

        //[FwLogicProperty(Id:"OqhotBnLfbEW")]
        //public bool? Availfrom { get; set; }

        //[FwLogicProperty(Id:"86l8TZ9bxQ0M")]
        //public bool? Hasitemdiscountschedule { get; set; }

        //[FwLogicProperty(Id:"L2EOgpxm9i3e")]
        //public string LinkedmasteritemId { get { return orderItem.LinkedmasteritemId; } set { orderItem.LinkedmasteritemId = value; } }

        //[FwLogicProperty(Id:"h02Mbc4jWtkF")]
        //public bool? Isprep { get; set; }

        //[FwLogicProperty(Id:"xbEz8ZeJPQCm")]
        //public bool? Displaywhenrateiszero { get; set; }

        //[FwLogicProperty(Id:"4F3YCmCZDNCc")]
        //public string Ordertype { get; set; }

        //[FwLogicProperty(Id:"HfiiUYaazNRO")]
        //public string Availfromdatetime { get; set; }

        //[FwLogicProperty(Id:"11dXn7eNVFUb")]
        //public string Availtodatetime { get; set; }

        //[FwLogicProperty(Id:"Yrjj2FkwrPCp")]
        //public decimal? Billedamount { get; set; }

        //[FwLogicProperty(Id:"b5qmICJlmTE8")]
        //public string SalesmasterId { get; set; }

        //[FwLogicProperty(Id:"yvqXKkJv3WiQ")]
        //public string Activity { get; set; }

        //[FwLogicProperty(Id:"gGCHxR3JpI0j")]
        //public bool? Toomanystagedout { get; set; }

        //[FwLogicProperty(Id:"Bdopq8iUndHH")]
        //public bool? Salescheckedin { get; set; }

        //[FwLogicProperty(Id:"KccZw4L36GT5")]
        //public string Orderby { get; set; }

        //[FwLogicProperty(Id:"0laR1wJIv21Q")]
        //public bool? Ispending { get; set; }

        //[FwLogicProperty(Id:"D4CqibWRnAMi")]
        //public bool? Quoteprint { get { return orderItem.Quoteprint; } set { orderItem.Quoteprint = value; } }

        //[FwLogicProperty(Id:"UP6JR1BICRhn")]
        //public bool? Orderprint { get { return orderItem.Orderprint; } set { orderItem.Orderprint = value; } }

        //[FwLogicProperty(Id:"0yyjiau0nGcQ")]
        //public bool? Picklistprint { get { return orderItem.Picklistprint; } set { orderItem.Picklistprint = value; } }

        //[FwLogicProperty(Id:"DNW1i0sUEhuo")]
        //public bool? Contractoutprint { get { return orderItem.Contractoutprint; } set { orderItem.Contractoutprint = value; } }

        //[FwLogicProperty(Id:"GhfQJSEzfLcx")]
        //public bool? Contractinprint { get { return orderItem.Contractinprint; } set { orderItem.Contractinprint = value; } }

        //[FwLogicProperty(Id:"GkfUFbz3zeEC")]
        //public bool? Returnlistprint { get { return orderItem.Returnlistprint; } set { orderItem.Returnlistprint = value; } }

        //[FwLogicProperty(Id:"OAMGwCUvJFu6")]
        //public bool? Invoiceprint { get { return orderItem.Invoiceprint; } set { orderItem.Invoiceprint = value; } }

        //[FwLogicProperty(Id:"yAdecRLjo7UZ")]
        //public bool? Poprint { get { return orderItem.Poprint; } set { orderItem.Poprint = value; } }

        //[FwLogicProperty(Id:"MgfmsmoXIYBi")]
        //public bool? Contractreceiveprint { get { return orderItem.Contractreceiveprint; } set { orderItem.Contractreceiveprint = value; } }

        //[FwLogicProperty(Id:"cb97o8t8Ukx6")]
        //public bool? Contractreturnprint { get { return orderItem.Contractreturnprint; } set { orderItem.Contractreturnprint = value; } }

        //[FwLogicProperty(Id:"gTaTjalpFWo6")]
        //public bool? Poreceivelistprint { get { return orderItem.Poreceivelistprint; } set { orderItem.Poreceivelistprint = value; } }

        //[FwLogicProperty(Id:"GMWNSiGY64vN")]
        //public bool? Poreturnlistprint { get { return orderItem.Poreturnlistprint; } set { orderItem.Poreturnlistprint = value; } }

        //[FwLogicProperty(Id:"i0FvO7MfFbIk")]
        //public decimal? Billableperiods { get; set; }

        //[FwLogicProperty(Id:"hTjsAu8HPhed")]
        //public string Weeksanddays { get; set; }

        //[FwLogicProperty(Id:"SL8cNpsqKEBo")]
        //public string Monthsanddays { get; set; }

        //[FwLogicProperty(Id:"MAu34xUCeJEU")]
        //public bool? Weeksanddaysexcluded { get; set; }

        //[FwLogicProperty(Id:"lOxm8tMSUg0H", IsReadOnly:true)]
        //public decimal? Unitextendednodisc { get; set; }

        //[FwLogicProperty(Id:"lOxm8tMSUg0H", IsReadOnly:true)]
        //public decimal? Unitdiscountamt { get; set; }

        //[FwLogicProperty(Id:"lOxm8tMSUg0H", IsReadOnly:true)]
        //public decimal? Unitextended { get; set; }

        //[FwLogicProperty(Id:"QjAGOkzsJJm0")]
        //public decimal? Weeklyextendednodisc { get; set; }

        //[FwLogicProperty(Id:"zi0Lgok7y0da")]
        //public decimal? Weeklydiscountamt { get; set; }

        //[FwLogicProperty(Id:"wdjB4p6RcZCV")]
        //public decimal? Weeklyextended { get; set; }

        //[FwLogicProperty(Id:"kqdlsi7l2tXn")]
        //public decimal? Weeklycostextended { get; set; }

        //[FwLogicProperty(Id:"6J1WhrInkF1w")]
        //public decimal? Week2extended { get; set; }

        //[FwLogicProperty(Id:"65hrnKLz78Gi")]
        //public decimal? Week3extended { get; set; }

        //[FwLogicProperty(Id:"UXMbfol7BF7V")]
        //public decimal? Weeks1through3extended { get; set; }

        //[FwLogicProperty(Id:"k48fbsbtuRK6")]
        //public decimal? Weeks4plusextended { get; set; }

        //[FwLogicProperty(Id:"oENufnmu6GY0")]
        //public decimal? Week4extended { get; set; }

        //[FwLogicProperty(Id:"ODmptB3q6NBu")]
        //public decimal? Averageweekly { get; set; }

        //[FwLogicProperty(Id:"DbH1sze5aroR")]
        //public decimal? Averageweeklyextended { get; set; }

        //[FwLogicProperty(Id:"bU5VxFWVtx99")]
        //public decimal? Averageweeklyextendednodisc { get; set; }

        //[FwLogicProperty(Id:"ab4SmyGMUuyn", IsReadOnly:true)]
        //public int? Episodes { get; set; }

        //[FwLogicProperty(Id:"s5KnvMND9wbB")]
        //public decimal? Episodediscountamt { get; set; }

        //[FwLogicProperty(Id:"GDqeGgeVEIIR")]
        //public decimal? Episodeextended { get; set; }

        //[FwLogicProperty(Id:"YeTGFU2NJ2Ui")]
        //public decimal? Monthlyextendednodisc { get; set; }

        //[FwLogicProperty(Id:"vMmI9lTnE3Ct")]
        //public decimal? Monthlydiscountamt { get; set; }

        //[FwLogicProperty(Id:"2kDoVvGxfSfR")]
        //public decimal? Monthlyextended { get; set; }

        //[FwLogicProperty(Id:"z1CiOJuKeE0E")]
        //public decimal? Monthlycostextended { get; set; }

        //[FwLogicProperty(Id:"gvY8vpeiYLdy")]
        //public decimal? Periodextendednodisc { get; set; }

        //[FwLogicProperty(Id:"X6IflBAg23CK")]
        //public decimal? Perioddiscountamt { get; set; }

        //[FwLogicProperty(Id:"1NMwXcTtOZJS")]
        //public decimal? Periodextended { get; set; }

        //[FwLogicProperty(Id:"ogm6qzH2AeXh")]
        //public decimal? Periodcostextended { get; set; }

        //[FwLogicProperty(Id:"eaElUEFfzxfY")]
        //public decimal? Periodvarianceextended { get; set; }

        //[FwLogicProperty(Id:"uXyK54xuZER6")]
        //public decimal? Variancepct { get; set; }

        //[FwLogicProperty(Id:"yb0SBJkM8ZdC")]
        //public string Conflictdate { get; set; }

        //[FwLogicProperty(Id:"lYQoBzuB6qof")]
        //public string Conflictdatesummary { get; set; }

        //[FwLogicProperty(Id:"zcEFdul73Zi5")]
        //public string Conflictdateallwh { get; set; }

        //[FwLogicProperty(Id:"2Ewi0ZcBD2Eq")]
        //public string Conflictdateconsign { get; set; }

        //[FwLogicProperty(Id:"8t8Oto95uoX6")]
        //public string Conflictdateconsignsummary { get; set; }

        //[FwLogicProperty(Id:"wHGcb77b6ZUy")]
        //public string Conflictdateconsignallwh { get; set; }

        //[FwLogicProperty(Id:"nSZRWkUx1dAk")]
        //public bool? Availiscurrent { get; set; }

        //[FwLogicProperty(Id:"nI7xTjzzylI9")]
        //public bool? Availiscurrentallwh { get; set; }

        //[FwLogicProperty(Id:"3Ye9SrTfGFZq")]
        //public decimal? AvailQuantity { get; set; }

        //[FwLogicProperty(Id:"RkPuC96tYMuv")]
        //public decimal? AvailQuantitysummary { get; set; }

        //[FwLogicProperty(Id:"wUJHZWPFP2IO")]
        //public decimal? AvailQuantityallwh { get; set; }

        //[FwLogicProperty(Id:"0YYhfwOZcPKZ")]
        //public int? AvailQuantityconsign { get; set; }

        //[FwLogicProperty(Id:"nCx7tBKRDfg4")]
        //public int? AvailQuantityconsignsummary { get; set; }

        //[FwLogicProperty(Id:"hbSwn7Lur7vJ")]
        //public int? AvailQuantityconsignallwh { get; set; }

        //[FwLogicProperty(Id:"3Tdjrh1i5xQU")]
        //public int? LdoutQuantity { get; set; }

        //[FwLogicProperty(Id:"RT2U3nU2HIK0", IsReadOnly:true)]
        //public string Notes { get; set; }

        //[FwLogicProperty(Id:"mMsLDYWmL6pN")]
        //public int? Ownedordertrans { get; set; }

        //[FwLogicProperty(Id:"fzxnXHtJ5bPa")]
        //public decimal? InQuantity { get; set; }

        //[FwLogicProperty(Id:"5Ym2iD6TVPHX")]
        //public bool? Rowsummarized { get; set; }

        //[FwLogicProperty(Id:"EYGLXOZFYUg3")]
        //public bool? Isprimary { get; set; }

        //[FwLogicProperty(Id:"Oamib7IqtdJn")]
        //public decimal? SalesQuantityonhand { get; set; }

        //[FwLogicProperty(Id:"utVlXlh1SaBZ")]
        //public bool? Billedinfull { get; set; }

        //[FwLogicProperty(Id:"WseCfMXOq1WC")]
        //public bool? Quantityadjusted { get; set; }

        //[FwLogicProperty(Id:"2k9UslveT04T")]
        //public bool? Shortage { get; set; }

        //[FwLogicProperty(Id:"NsRO4e4kMava")]
        //public decimal? Accratio { get { return orderItem.Accratio; } set { orderItem.Accratio = value; } }

        //[FwLogicProperty(Id:"rscwaPsW0oEK")]
        //public string SpacetypeId { get { return orderItem.SpacetypeId; } set { orderItem.SpacetypeId = value; } }

        //[FwLogicProperty(Id:"rv2r0XJRHlUI")]
        //public string SchedulestatusId { get { return orderItem.SchedulestatusId; } set { orderItem.SchedulestatusId = value; } }

        //[FwLogicProperty(Id:"pBSpCZEzNdrG")]
        //public bool? Iteminactive { get; set; }

        //[FwLogicProperty(Id:"enRxb5Wu6TdY")]
        //public int? Reservedrentalitems { get; set; }

        //[FwLogicProperty(Id:"xqYwuVyzWPr5")]
        //public bool? Prorateweeks { get { return orderItem.Prorateweeks; } set { orderItem.Prorateweeks = value; } }

        //[FwLogicProperty(Id:"4NPAT5RVwM74")]
        //public string OriginalshowId { get; set; }

        //[FwLogicProperty(Id:"oECt4TTEjReZ")]
        //public bool? Proratemonths { get { return orderItem.Proratemonths; } set { orderItem.Proratemonths = value; } }

        //[FwLogicProperty(Id:"N8bvkuffBD0L")]
        //public bool? Includeonpicklist { get; set; }

        //[FwLogicProperty(Id:"9wtWe55Rx2m2")]
        //public bool? Totimeestimated { get; set; }

        //[FwLogicProperty(Id:"HS2i7sgnAYTu")]
        //public bool? Ismaxdayloc { get { return orderItem.Ismaxdayloc; } set { orderItem.Ismaxdayloc = value; } }

        //[FwLogicProperty(Id:"PtHtzs5uWR5C")]
        //public bool? Ismaxloc { get { return orderItem.Ismaxloc; } set { orderItem.Ismaxloc = value; } }

        //[FwLogicProperty(Id:"neEFrAht2Q1I")]
        //public string Transactionno { get; set; }

        //[FwLogicProperty(Id:"G88k8hNJjuXk")]
        //public string Sourcecode { get; set; }

        //[FwLogicProperty(Id:"OvT86pf5Tx6Q")]
        //public string Accountingcode { get; set; }

        //[FwLogicProperty(Id:"ZVX5pPkOXU8H")]
        //public string BuyerId { get; set; }

        //[FwLogicProperty(Id:"dmqPFMwtmkl2")]
        //public string Buyer { get; set; }

        //[FwLogicProperty(Id:"i1dsVeF0AfRY")]
        //public string Character { get; set; }

        //[FwLogicProperty(Id:"ZoOL8e3O1v6w")]
        //public decimal? Prepfees { get; set; }

        //[FwLogicProperty(Id:"V9nGpvB3oy5q")]
        //public bool? Periodextendedincludesprep { get; set; }

        //[FwLogicProperty(Id:"p0ZP3tEGLEEx")]
        //public string Orderactivity { get { return orderItem.Orderactivity; } set { orderItem.Orderactivity = value; } }

        //[FwLogicProperty(Id:"ogbpODYiV4Bf")]
        //public bool? Issubstitute { get; set; }

        //[FwLogicProperty(Id:"dSiy9LHYGFv9")]
        //public decimal? Quantitystaged { get; set; }

        //[FwLogicProperty(Id:"XmgqvQKRpJJo")]
        //public decimal? Quantityout { get; set; }

        //[FwLogicProperty(Id:"1pPtiuF79EdZ")]
        //public decimal? Quantityin { get; set; }

        //[FwLogicProperty(Id:"rGJs7JwDxb0L")]
        //public decimal? Quantityreceived { get; set; }

        //[FwLogicProperty(Id:"kJn7Dj3kLFrj")]
        //public decimal? Quantityreturned { get; set; }

        [FwLogicProperty(Id: "Wd2Vwb34Ijw3")]
        public string DateStamp { get { return orderItem.DateStamp; } set { orderItem.DateStamp = value; } }

        //------------------------------------------------------------------------------------ 
        protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg)
        {
            bool isValid = true;

            OrderItemLogic orig = null;
            if (original != null)
            {
                orig = (OrderItemLogic)original;
            }

            string itemClass = ItemClass;
            string inventoryId = InventoryId;

            if (orig != null)
            {
                if (itemClass == null)
                {
                    itemClass = orig.ItemClass;
                }
                if (inventoryId == null)
                {
                    inventoryId = orig.InventoryId;
                }
            }

            if (isValid)
            {
                if (string.IsNullOrEmpty(inventoryId))
                {
                    if (!(itemClass.Equals(RwConstants.ITEMCLASS_GROUP_HEADING) || itemClass.Equals(RwConstants.ITEMCLASS_TEXT) || itemClass.Equals(RwConstants.ITEMCLASS_SUBTOTAL)))
                    {
                        isValid = false;
                        validateMsg = "I-Code is required.";
                    }
                }
            }

            if (isValid)
            {
                //need to make sure the user doesn't change from package-type to non-package-type or vice-versa
                if ((saveMode.Equals(TDataRecordSaveMode.smUpdate)) && (orig != null))
                {
                    string origItemClass = orig.ItemClass;
                    string origInventoryId = orig.InventoryId;
                    string origDescription = orig.Description;
                    string newInventoryId = InventoryId ?? origInventoryId;

                    if (!newInventoryId.Equals(origInventoryId))
                    {

                        string[] inventoryData = AppFunc.GetStringDataAsync(AppConfig, "master", new string[] { "masterid" }, new string[] { inventoryId }, new string[] { "class", "masterno", "master" }).Result;
                        string newInventoryClass = inventoryData[0];
                        string newInventoryICode = inventoryData[1];
                        string newInventoryDescription = inventoryData[2];

                        if (isValid)
                        {
                            if (origItemClass.Equals(RwConstants.INVENTORY_CLASSIFICATION_KIT) || origItemClass.Equals(RwConstants.INVENTORY_CLASSIFICATION_COMPLETE) || origItemClass.Equals(RwConstants.INVENTORY_CLASSIFICATION_CONTAINER))
                            {
                                isValid = false;
                                validateMsg = $"Cannot modify {origDescription} here.  Instead, delete this and add {newInventoryDescription} as a new item.";
                            }
                        }

                        if (isValid)
                        {
                            if ((!(origItemClass.Equals(RwConstants.INVENTORY_CLASSIFICATION_KIT) || origItemClass.Equals(RwConstants.INVENTORY_CLASSIFICATION_COMPLETE) || origItemClass.Equals(RwConstants.INVENTORY_CLASSIFICATION_CONTAINER))) &&
                               ((newInventoryClass.Equals(RwConstants.INVENTORY_CLASSIFICATION_KIT) || newInventoryClass.Equals(RwConstants.INVENTORY_CLASSIFICATION_COMPLETE) || newInventoryClass.Equals(RwConstants.INVENTORY_CLASSIFICATION_CONTAINER))))
                            {
                                isValid = false;
                                validateMsg = $"Cannot change item to {newInventoryDescription} here.  Instead, delete {origDescription} and add the new item.";
                            }
                        }
                    }
                }
            }

            return isValid;
        }
        //------------------------------------------------------------------------------------
        public void OnBeforeSave(object sender, BeforeSaveEventArgs e)
        {
            OrderItemLogic orig = null;
            if (e.Original != null)
            {
                orig = (OrderItemLogic)e.Original;
            }

            string inventoryId = InventoryId;
            if (string.IsNullOrEmpty(inventoryId))
            {
                if (orig != null)
                {
                    inventoryId = orig.InventoryId;
                }
            }

            string inventoryClass = "";
            string inventoryDescription = "";
            if (!string.IsNullOrEmpty(inventoryId))
            {
                string[] inventoryData = AppFunc.GetStringDataAsync(AppConfig, "master", new string[] { "masterid" }, new string[] { inventoryId }, new string[] { "class", "master" }).Result;
                inventoryClass = inventoryData[0];
                inventoryDescription = inventoryData[1];
            }

            if (e.SaveMode == TDataRecordSaveMode.smInsert)
            {
                if ((inventoryClass.Equals(RwConstants.INVENTORY_CLASSIFICATION_KIT)) || (inventoryClass.Equals(RwConstants.INVENTORY_CLASSIFICATION_COMPLETE)))
                {
                    // inserting a new record, it is a Complete or Kit, so call this procedure to add the entire Complete/Kit
                    OrderItemId = OrderFunc.InsertPackage(AppConfig, UserSession, this).Result;
                    e.PerformSave = false;  // all framework save functions will be skipped
                }
                if (!inventoryClass.Equals(RwConstants.ITEMCLASS_MISCELLANEOUS))
                {
                    Description = inventoryDescription; // don't let user change the description on a new row, unless MISC
                }
                ItemClass = inventoryClass;
                ItemOrder = "";
                DetailOnly = true;
            }
            else  // updating
            {
                // don't let user change the description on existing row, unless MISC, GROUPHEADING, TEXT, or SUBTOTAL
                if (orig != null)
                {
                    if (!(orig.ItemClass.Equals(RwConstants.ITEMCLASS_MISCELLANEOUS) || orig.ItemClass.Equals(RwConstants.ITEMCLASS_GROUP_HEADING) || orig.ItemClass.Equals(RwConstants.ITEMCLASS_TEXT) || orig.ItemClass.Equals(RwConstants.ITEMCLASS_SUBTOTAL)))
                    {
                        Description = orig.Description;
                    }
                }

                if ((orig != null) && (orig.Locked.GetValueOrDefault(false)))
                {
                    Price = orig.Price;
                    Price2 = orig.Price2;
                    Price3 = orig.Price3;
                    Price4 = orig.Price4;
                    Price5 = orig.Price5;
                    DiscountPercent = orig.DiscountPercent;
                    DaysPerWeek = orig.DaysPerWeek;
                }

                if ((orig != null) && (Mute.GetValueOrDefault(false) || (orig.Mute.GetValueOrDefault(false))))
                {
                    Price = 0;
                    Price2 = 0;
                    Price3 = 0;
                    Price4 = 0;
                    Price5 = 0;
                }

                if ((orig != null) && ((orig.ItemClass.Equals(RwConstants.ITEMCLASS_GROUP_HEADING) || orig.ItemClass.Equals(RwConstants.ITEMCLASS_TEXT) || orig.ItemClass.Equals(RwConstants.ITEMCLASS_SUBTOTAL))))
                {
                    Price = 0;
                    Price2 = 0;
                    Price3 = 0;
                    Price4 = 0;
                    Price5 = 0;
                }

                if (RowsRolledUp.GetValueOrDefault(false))
                {
                    InsteadOfSave += SaveRolledUpRow;
                }
                else
                {
                    DetailOnly = true;
                }

                if ((orig != null) && (orig.ItemClass.Equals(RwConstants.INVENTORY_CLASSIFICATION_KIT) || orig.ItemClass.Equals(RwConstants.INVENTORY_CLASSIFICATION_COMPLETE)))
                {
                    if ((QuantityOrdered != null) && (orig.QuantityOrdered != QuantityOrdered))
                    {
                        bool b2 = OrderFunc.UpdatePackageQuantities(AppConfig, UserSession, this).Result;
                    }
                    if ((SubQuantity != null) && (orig.SubQuantity != SubQuantity))
                    {
                        bool b2 = OrderFunc.UpdatePackageSubQuantities(AppConfig, UserSession, this).Result;
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------
        public void OnAfterSave(object sender, AfterSaveEventArgs e)
        {
            if (Notes != null)
            {
                bool saved = orderItem.SaveNoteASync(Notes).Result;
            }
            if ((InventoryId != null) && (WarehouseId != null))
            {
                InventoryAvailabilityFunc.RequestRecalc(InventoryId, WarehouseId, ItemClass);
            }
        }
        //------------------------------------------------------------------------------------
        public virtual void OnAfterSaveMasterItem(object sender, AfterSaveDataRecordEventArgs e)
        {
            // justin hoffman 01/20/2020
            // this is really stupid
            // I am deleting the record that dbwIU_masteritem is giving us, so I can add my own and avoid a unique index error


            //if (e.SaveMode == FwStandard.BusinessLogic.TDataRecordSaveMode.smInsert)
            //{
            //    MasterItemDetailRecord detailRec = new MasterItemDetailRecord();
            //    detailRec.SetDependencies(AppConfig, UserSession);
            //    detailRec.MasterItemId = GetPrimaryKeys()[0].ToString();
            //    bool b = detailRec.DeleteAsync(e.SqlConnection).Result;
            //}

        }
        //------------------------------------------------------------------------------------
        public void SaveRolledUpRow(object sender, InsteadOfSaveEventArgs e)
        {

            void copyFieldsToSave(OrderItemLogic oiFrom, OrderItemLogic oiTo)
            {
                oiTo.RecType = oiFrom.RecType;
                oiTo.InventoryId = oiFrom.InventoryId;
                oiTo.Description = oiFrom.Description;
                oiTo.PickDate = oiFrom.PickDate;
                oiTo.PickTime = oiFrom.PickTime;
                oiTo.FromDate = oiFrom.FromDate;
                oiTo.FromTime = oiFrom.FromTime;
                oiTo.ToDate = oiFrom.ToDate;
                oiTo.ToTime = oiFrom.ToTime;
                //oiTo.QuantityOrdered = oiFrom.QuantityOrdered;
                //oiTo.SubQuantity = oiFrom.SubQuantity;
                //oiTo.ConsignQuantity = oiFrom.ConsignQuantity;
                oiTo.UnitId = oiFrom.UnitId;
                oiTo.UnitCost = oiFrom.UnitCost;
                oiTo.MarginPercent = oiFrom.MarginPercent;
                oiTo.MarkupPercent = oiFrom.MarkupPercent;
                oiTo.PremiumPercent = oiFrom.PremiumPercent;
                oiTo.CrewContactId = oiFrom.CrewContactId;
                oiTo.Hours = oiFrom.Hours;
                oiTo.HoursOvertime = oiFrom.HoursOvertime;
                oiTo.HoursDoubletime = oiFrom.HoursDoubletime;
                oiTo.Price = oiFrom.Price;
                oiTo.Price2 = oiFrom.Price2;
                oiTo.Price3 = oiFrom.Price3;
                oiTo.Price4 = oiFrom.Price4;
                oiTo.Price5 = oiFrom.Price5;
                oiTo.DaysPerWeek = oiFrom.DaysPerWeek;
                oiTo.DiscountPercent = oiFrom.DiscountPercent;
                oiTo.Bold = oiFrom.Bold;
                oiTo.Locked = oiFrom.Locked;
                oiTo.Taxable = oiFrom.Taxable;
                oiTo.WarehouseId = oiFrom.WarehouseId;
                oiTo.ReturnToWarehouseId = oiFrom.ReturnToWarehouseId;
                oiTo.ParentId = oiFrom.ParentId;
                oiTo.ItemClass = oiFrom.ItemClass;
                oiTo.RetiredReasonId = oiFrom.RetiredReasonId;
                oiTo.ItemId = oiFrom.ItemId;
                oiTo.ManufacturerPartNumber = oiFrom.ManufacturerPartNumber;
            }

            OrderItemLogic orig = (OrderItemLogic)e.Original;
            List<OrderItemLogic> rolledUpItems = new List<OrderItemLogic>();
            int rowsSaved = 0;

            //get all of the rolled-up OrderItems
            foreach (string id in orig.RolledUpIds.Split(','))
            {
                OrderItemLogic oi = new OrderItemLogic();
                oi.SetDependencies(AppConfig, UserSession);
                oi.OrderId = OrderId;
                oi.OrderItemId = id.Trim();
                oi.DetailOnly = true;
                bool b = oi.LoadAsync<OrderItemLogic>().Result;
                oi.DetailOnly = true;
                rolledUpItems.Add(oi);
            }

            //save all of the rolled-up OrderItems
            foreach (OrderItemLogic oi in rolledUpItems)
            {
                OrderItemLogic oiNew = new OrderItemLogic();
                oiNew.SetDependencies(AppConfig, UserSession);
                oiNew.DetailOnly = true;
                oiNew.OrderId = oi.OrderId;
                oiNew.OrderItemId = oi.OrderItemId;
                oiNew.Taxable = Taxable;
                copyFieldsToSave(this, oiNew);
                rowsSaved += oiNew.SaveAsync(oi, e.SqlConnection).Result;
            }

            e.SavePerformed = (rowsSaved > 0);
        }
        //------------------------------------------------------------------------------------
        public void OnAfterDelete(object sender, AfterDeleteEventArgs e)
        {
            
            string itemClass = ItemClass ?? "";
            if (itemClass.Equals(RwConstants.INVENTORY_CLASSIFICATION_KIT) || itemClass.Equals(RwConstants.INVENTORY_CLASSIFICATION_COMPLETE) || itemClass.Equals(RwConstants.INVENTORY_CLASSIFICATION_CONTAINER))
            {

                //find and delete all of the accessories
                BrowseRequest accessoryBrowseRequest = new BrowseRequest();
                accessoryBrowseRequest.uniqueids = new Dictionary<string, object>();
                accessoryBrowseRequest.uniqueids.Add("OrderId", OrderId);
                accessoryBrowseRequest.uniqueids.Add("ParentId", OrderItemId);

                OrderItemLogic acc = new OrderItemLogic();
                acc.SetDependencies(AppConfig, UserSession);
                List<OrderItemLogic> accessories = acc.SelectAsync<OrderItemLogic>(accessoryBrowseRequest).Result;

                foreach (OrderItemLogic a in accessories)
                {
                    a.SetDependencies(AppConfig, UserSession);
                    bool b = a.DeleteAsync(e.SqlConnection).Result;
                }


            }
        }
        //------------------------------------------------------------------------------------ 
    }
}
