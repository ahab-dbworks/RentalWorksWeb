using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic; 
namespace RentalWorksWebApi.Modules.Settings.AppReportDesigner 
{ 
public class AppReportDesignerLogic : RwBusinessLogic 
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
[FwBusinessLogicField(isPrimaryKey: true)] 
public string AppReportDesignerId { get { return appReportDesigner.AppReportDesignerId; } set { appReportDesigner.AppReportDesignerId = value; } } 
public string Category { get { return appReportDesigner.Category; } set { appReportDesigner.Category = value; } } 
[FwBusinessLogicField(isRecordTitle: true)] 
public string Description { get { return appReportDesigner.Description; } set { appReportDesigner.Description = value; } } 
public bool Inactive { get { return appReportDesigner.Inactive; } set { appReportDesigner.Inactive = value; } } 
//------------------------------------------------------------------------------------ 
} 
} 
