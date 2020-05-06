using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Agent.OrderHiatusDiscount
{
    [FwLogic(Id: "qaCspp00zhP8q")]
    public class OrderHiatusDiscountLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        OrderHiatusDiscountRecord orderHiatusDiscount = new OrderHiatusDiscountRecord();
        OrderHiatusDiscountLoader orderHiatusDiscountLoader = new OrderHiatusDiscountLoader();
        public OrderHiatusDiscountLogic()
        {
            dataRecords.Add(orderHiatusDiscount);
            dataLoader = orderHiatusDiscountLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "qAgs0oCocpTKj", IsPrimaryKey: true)]
        public string OrderDiscountId { get { return orderHiatusDiscount.OrderDiscountId; } set { orderHiatusDiscount.OrderDiscountId = value; } }
        [FwLogicProperty(Id: "QcjVhNmwhpmvI")]
        public string OrderId { get { return orderHiatusDiscount.OrderId; } set { orderHiatusDiscount.OrderId = value; } }
        [FwLogicProperty(Id: "qaiQX0QD4r5eq")]
        public string FromDate { get { return orderHiatusDiscount.FromDate; } set { orderHiatusDiscount.FromDate = value; } }
        [FwLogicProperty(Id: "qAlJO43Hd73Ub")]
        public string ToDate { get { return orderHiatusDiscount.ToDate; } set { orderHiatusDiscount.ToDate = value; } }
        [FwLogicProperty(Id: "QARVbl6hlnNhy")]
        public bool? IsHiatus { get { return orderHiatusDiscount.IsHiatus; } set { orderHiatusDiscount.IsHiatus = value; } }
        [FwLogicProperty(Id: "QbOxurtJqNy7J")]
        public decimal? DiscountPercent { get { return orderHiatusDiscount.DiscountPercent; } set { orderHiatusDiscount.DiscountPercent = value; } }
        [FwLogicProperty(Id: "qBvBhlTtUGf4d")]
        public bool? IsProrated { get { return orderHiatusDiscount.IsProrated; } set { orderHiatusDiscount.IsProrated = value; } }
        [FwLogicProperty(Id: "qBXdxmHF59xEH")]
        public bool? DoChangeSchedule { get { return orderHiatusDiscount.DoChangeSchedule; } set { orderHiatusDiscount.DoChangeSchedule = value; } }
        //------------------------------------------------------------------------------------ 
        //protected override bool Validate(TDataRecordSaveMode saveMode, FwBusinessLogic original, ref string validateMsg) 
        //{ 
        //    //override this method on a derived class to implement custom validation logic 
        //    bool isValid = true; 
        //    return isValid; 
        //} 
        //------------------------------------------------------------------------------------ 
    }
}
