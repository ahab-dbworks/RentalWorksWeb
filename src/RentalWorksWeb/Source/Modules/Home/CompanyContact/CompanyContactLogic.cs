using FwStandard.BusinessLogic.Attributes; 
using RentalWorksWebApi.Logic; 
namespace RentalWorksWebApi.Modules..CompanyContact 
{ 
public class CompanyContactLogic : RwBusinessLogic 
{ 
//------------------------------------------------------------------------------------ 
CompanyContactRecord companyContact = new CompanyContactRecord(); 
CompanyContactLoader companyContactLoader = new CompanyContactLoader(); 
public CompanyContactLogic() 
{ 
dataRecords.Add(companyContact); 
dataLoader = companyContactLoader; 
} 
//------------------------------------------------------------------------------------ 
[FwBusinessLogicField(isPrimaryKey: true)] 
public string CompanyContactId { get { return companyContact.CompanyContactId; } set { companyContact.CompanyContactId = value; } } 
[FwBusinessLogicField(isReadOnly: true)] 
public string Company { get; set; } 
public string Email { get { return companyContact.Email; } set { companyContact.Email = value; } } 
public string Mobilephone { get { return companyContact.Mobilephone; } set { companyContact.Mobilephone = value; } } 
public string Officephone { get { return companyContact.Officephone; } set { companyContact.Officephone = value; } } 
public string Ext { get { return companyContact.Ext; } set { companyContact.Ext = value; } } 
public string Pager { get { return companyContact.Pager; } set { companyContact.Pager = value; } } 
public string Pagerpin { get { return companyContact.Pagerpin; } set { companyContact.Pagerpin = value; } } 
public string Fax { get { return companyContact.Fax; } set { companyContact.Fax = value; } } 
public string Faxext { get { return companyContact.Faxext; } set { companyContact.Faxext = value; } } 
public string Directphone { get { return companyContact.Directphone; } set { companyContact.Directphone = value; } } 
public string Directext { get { return companyContact.Directext; } set { companyContact.Directext = value; } } 
public string Activedate { get { return companyContact.Activedate; } set { companyContact.Activedate = value; } } 
public string Inactivedate { get { return companyContact.Inactivedate; } set { companyContact.Inactivedate = value; } } 
public bool Primaryflag { get { return companyContact.Primaryflag; } set { companyContact.Primaryflag = value; } } 
public bool Authorized { get { return companyContact.Authorized; } set { companyContact.Authorized = value; } } 
public string ContacttitleId { get { return companyContact.ContacttitleId; } set { companyContact.ContacttitleId = value; } } 
[FwBusinessLogicField(isReadOnly: true)] 
public string Contacttitle { get; set; } 
public string Jobtitle { get { return companyContact.Jobtitle; } set { companyContact.Jobtitle = value; } } 
[FwBusinessLogicField(isReadOnly: true)] 
public string Contactrecordtype { get; set; } 
[FwBusinessLogicField(isReadOnly: true)] 
public string Contactrecordtypecolor { get; set; } 
public string ContactId { get { return companyContact.ContactId; } set { companyContact.ContactId = value; } } 
public string CompanyId { get { return companyContact.CompanyId; } set { companyContact.CompanyId = value; } } 
public bool Inactive { get { return companyContact.Inactive; } set { companyContact.Inactive = value; } } 
//------------------------------------------------------------------------------------ 
} 
} 
