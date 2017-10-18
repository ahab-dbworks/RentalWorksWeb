using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic; 
namespace RentalWorksWebApi.Modules.Settings.DiscountItem 
{ 
public class DiscountItemLogic : RwBusinessLogic 
{ 
//------------------------------------------------------------------------------------ 
DiscountItemRecord discountItem = new DiscountItemRecord(); 
DiscountItemLoader discountItemLoader = new DiscountItemLoader(); 
public DiscountItemLogic() 
{ 
dataRecords.Add(discountItem); 
dataLoader = discountItemLoader; 
} 
//------------------------------------------------------------------------------------ 
//------------------------------------------------------------------------------------ 
} 
} 
