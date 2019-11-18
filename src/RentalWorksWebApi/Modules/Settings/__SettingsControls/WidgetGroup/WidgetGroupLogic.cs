using WebApi.Logic;
using FwStandard.AppManager;
namespace WebApi.Modules.Administrator.WidgetGroup
{
    [FwLogic(Id: "uGSQERBTeuNda")]
    public class WidgetGroupLogic : AppBusinessLogic
    {
        //------------------------------------------------------------------------------------ 
        WidgetGroupRecord widgetGroup = new WidgetGroupRecord();
        WidgetGroupLoader widgetGroupLoader = new WidgetGroupLoader();
        public WidgetGroupLogic()
        {
            dataRecords.Add(widgetGroup);
            dataLoader = widgetGroupLoader;
        }
        //------------------------------------------------------------------------------------ 
        [FwLogicProperty(Id: "p0dkuIwP9ip4Y", IsPrimaryKey: true)]
        public string WidgetGroupId { get { return widgetGroup.WidgetGroupId; } set { widgetGroup.WidgetGroupId = value; } }
        [FwLogicProperty(Id: "IKtWder9NFooN")]
        public string WidgetId { get { return widgetGroup.WidgetId; } set { widgetGroup.WidgetId = value; } }
        [FwLogicProperty(Id: "NAyGo67NFRN9E", IsReadOnly: true)]
        public string WidgetDescription { get; set; }
        [FwLogicProperty(Id: "mXrcP5oT54ewr")]
        public string GroupId { get { return widgetGroup.GroupId; } set { widgetGroup.GroupId = value; } }
        [FwLogicProperty(Id: "gnkZ8DqMADmY8", IsReadOnly: true)]
        public string GroupName { get; set; }
        [FwLogicProperty(Id: "iZ3IF6vcREEyY")]
        public string DateStamp { get { return widgetGroup.DateStamp; } set { widgetGroup.DateStamp = value; } }
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
