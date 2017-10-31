using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic; 
namespace RentalWorksWebApi.Modules..AppDocument 
{ 
public class AppDocumentLogic : RwBusinessLogic 
{ 
//------------------------------------------------------------------------------------ 
AppDocumentRecord appDocument = new AppDocumentRecord(); 
AppDocumentLoader appDocumentLoader = new AppDocumentLoader(); 
public AppDocumentLogic() 
{ 
dataRecords.Add(appDocument); 
dataLoader = appDocumentLoader; 
} 
//------------------------------------------------------------------------------------ 
//------------------------------------------------------------------------------------ 
} 
} 
