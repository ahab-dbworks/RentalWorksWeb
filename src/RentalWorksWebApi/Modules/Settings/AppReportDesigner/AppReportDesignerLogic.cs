using FwStandard.AppManager;
using WebApi.Logic; 
namespace WebApi.Modules.Settings.AppReportDesigner 
{ 
[FwLogic(Id:"erUz2vPGJgg")]
public class AppReportDesignerLogic : AppBusinessLogic 
{ 
//------------------------------------------------------------------------------------ 
AppReportDesignerRecord appReportDesigner = new AppReportDesignerRecord(); 
AppReportDesignerLoader appReportDesignerLoader = new AppReportDesignerLoader(); 
public AppReportDesignerLogic() 
{ 
dataRecords.Add(appReportDesigner); 
dataLoader = appReportDesignerLoader; 
} 
//------------------------------------------------------------------------------------ 
[FwLogicProperty(Id:"aYsJokYBvyh", IsPrimaryKey:true)]
public string AppReportDesignerId { get { return appReportDesigner.AppReportDesignerId; } set { appReportDesigner.AppReportDesignerId = value; } } 

[FwLogicProperty(Id:"xMbiWXWdMXzr")]
public string Category { get { return appReportDesigner.Category; } set { appReportDesigner.Category = value; } } 

[FwLogicProperty(Id:"c6dxbw7nB7G", IsRecordTitle:true)]
public string Description { get { return appReportDesigner.Description; } set { appReportDesigner.Description = value; } } 

[FwLogicProperty(Id:"rsaVo3BCrpVP")]
public bool? Inactive { get { return appReportDesigner.Inactive; } set { appReportDesigner.Inactive = value; } } 

//------------------------------------------------------------------------------------ 
} 
} 
