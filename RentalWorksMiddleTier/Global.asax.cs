using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Fw.Json.SqlServer;

namespace RentalWorksMiddleTier
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            FwSqlConnection.AppDatabase = FwDatabases.RentalWorks;
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("approle");
                SqlCacheManager.IndexCacheTable("approle","approleid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("asbuild");
                SqlCacheManager.IndexCacheTable("asbuild","asbuildid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("billperiod");
                SqlCacheManager.IndexCacheTable("billperiod","billperiodid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("building");
                SqlCacheManager.IndexCacheTable("building","buildingid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("category");
                SqlCacheManager.IndexCacheTable("category","categoryid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("color");
                SqlCacheManager.IndexCacheTable("color","colorid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("commissioning");
                SqlCacheManager.IndexCacheTable("commissioning","commissioningid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("condition");
                SqlCacheManager.IndexCacheTable("condition","conditionid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("contacttitle");
                SqlCacheManager.IndexCacheTable("contacttitle","contacttitleid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("control");
                SqlCacheManager.IndexCacheTable("control","controlid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("controlclient");
                SqlCacheManager.IndexCacheTable("controlclient","controlid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("country");
                SqlCacheManager.IndexCacheTable("country","countryid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("creditstatus");
                SqlCacheManager.IndexCacheTable("creditstatus","creditstatusid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("custcat");
                SqlCacheManager.IndexCacheTable("custcat","custcatid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("custtype");
                SqlCacheManager.IndexCacheTable("custtype","custtypeid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("custstatus");
                SqlCacheManager.IndexCacheTable("custstatus","custstatusid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("dealstatus");
                SqlCacheManager.IndexCacheTable("dealstatus","dealstatusid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("dealtype");
                SqlCacheManager.IndexCacheTable("dealtype","dealtypeid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("deposit");
                SqlCacheManager.IndexCacheTable("deposit","depositid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("drawing");
                SqlCacheManager.IndexCacheTable("drawing","drawingid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("dropship");
                SqlCacheManager.IndexCacheTable("dropship","dropshipid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("controluniversity");
                SqlCacheManager.IndexCacheTable("controluniversity","controlid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("currency");
                SqlCacheManager.IndexCacheTable("currency","currencyid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("dealclassification");
                SqlCacheManager.IndexCacheTable("dealclassification","dealclassificationid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("department");
                SqlCacheManager.IndexCacheTable("department","departmentid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("deptloc");
                SqlCacheManager.IndexCacheTable("deptloc","deptlocid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("eventcategory");
                SqlCacheManager.IndexCacheTable("eventcategory","eventcategoryid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("floor");
                SqlCacheManager.IndexCacheTable("floor","floorid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("fueltype");
                SqlCacheManager.IndexCacheTable("fueltype","fueltypeid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("inventorydepartment");
                SqlCacheManager.IndexCacheTable("inventorydepartment","inventorydepartmentid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("formdesign");
                SqlCacheManager.IndexCacheTable("formdesign","formdesignid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("itemordered");
                SqlCacheManager.IndexCacheTable("itemordered","itemorderedid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("licclass");
                SqlCacheManager.IndexCacheTable("licclass","licclassid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("location");
                SqlCacheManager.IndexCacheTable("location","locationid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("masterspaceview");
                SqlCacheManager.IndexCacheTable("masterspaceview","masterspaceviewid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("opening");
                SqlCacheManager.IndexCacheTable("opening","openingid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("ordertype");
                SqlCacheManager.IndexCacheTable("ordertype","ordertypeid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("ordertypedatetype");
                SqlCacheManager.IndexCacheTable("ordertypedatetype","ordertypedatetypeid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("ordertypefields");
                SqlCacheManager.IndexCacheTable("ordertypefields","ordertypefieldsid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("ordertypelocation");
                SqlCacheManager.IndexCacheTable("ordertypelocation","ordertypelocationid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("organizationtype");
                SqlCacheManager.IndexCacheTable("organizationtype","organizationtypeid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("payterms");
                SqlCacheManager.IndexCacheTable("payterms","paytermsid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("paytype");
                SqlCacheManager.IndexCacheTable("paytype","paytypeid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("personneltype");
                SqlCacheManager.IndexCacheTable("personneltype","personneltypeid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("photographytype");
                SqlCacheManager.IndexCacheTable("photographytype","photographytypeid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("poapprovalstatus");
                SqlCacheManager.IndexCacheTable("poapprovalstatus","poapprovalstatusid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("poclassification");
                SqlCacheManager.IndexCacheTable("poclassification","poclassificationid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("poimportance");
                SqlCacheManager.IndexCacheTable("poimportance","poimportanceid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("porejectreason");
                SqlCacheManager.IndexCacheTable("porejectreason","porejectreasonid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("prodtype");
                SqlCacheManager.IndexCacheTable("prodtype","prodtypeid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("region");
                SqlCacheManager.IndexCacheTable("region","regionid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("rentalstatus");
                SqlCacheManager.IndexCacheTable("rentalstatus","rentalstatusid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("resourcestatus");
                SqlCacheManager.IndexCacheTable("resourcestatus","resourcestatusid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("retiredreason");
                SqlCacheManager.IndexCacheTable("retiredreason","retiredreasonid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("rfidchannel");
                SqlCacheManager.IndexCacheTable("rfidchannel","rfidchannelid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("rfidservice");
                SqlCacheManager.IndexCacheTable("rfidservice","rfidserviceid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("schedulestatus");
                SqlCacheManager.IndexCacheTable("schedulestatus","schedulestatusid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("shipvia");
                SqlCacheManager.IndexCacheTable("shipvia","shipviaid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("state");
                SqlCacheManager.IndexCacheTable("state","stateid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("subcategory");
                SqlCacheManager.IndexCacheTable("subcategory","subcategoryid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("schedulevehicletypeview");
                SqlCacheManager.IndexCacheTable("schedulevehicletypeview","schedulevehicletypeviewid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("spacetype");
                SqlCacheManager.IndexCacheTable("spacetype","spacetypeid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("surface");
                SqlCacheManager.IndexCacheTable("surface","surfaceid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("syscontrol");
                SqlCacheManager.IndexCacheTable("syscontrol","syscontrolid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("taxoption");
                SqlCacheManager.IndexCacheTable("taxoption","taxoptionid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("termsconditions");
                SqlCacheManager.IndexCacheTable("termsconditions","termsconditionsid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("unit");
                SqlCacheManager.IndexCacheTable("unit","unitid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("unretiredreason");
                SqlCacheManager.IndexCacheTable("unretiredreason","unretiredreasonid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("vehiclemake");
                SqlCacheManager.IndexCacheTable("vehiclemake","vehiclemakeid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("vehiclerating");
                SqlCacheManager.IndexCacheTable("vehiclerating","vehicleratingid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("vendor");
                SqlCacheManager.IndexCacheTable("vendor","vendorid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("vendorclass");
                SqlCacheManager.IndexCacheTable("vendorclass","vendorclassid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("walldescription");
                SqlCacheManager.IndexCacheTable("walldescription","walldescriptionid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("walltype");
                SqlCacheManager.IndexCacheTable("walltype","walltypeid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("warehouse");
                SqlCacheManager.IndexCacheTable("warehouse","warehouseid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("watts");
                SqlCacheManager.IndexCacheTable("watts","wattsid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("webcontrolquoterequest");
                SqlCacheManager.IndexCacheTable("webcontrolquoterequest","webcontrolquoterequestid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("pattern");
                SqlCacheManager.IndexCacheTable("pattern","patternid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("material");
                SqlCacheManager.IndexCacheTable("material","materialid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("gender");
                SqlCacheManager.IndexCacheTable("gender","genderid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("period");
                SqlCacheManager.IndexCacheTable("period","periodid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("label");
                SqlCacheManager.IndexCacheTable("label","labelid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("dealorderlocation");
                SqlCacheManager.IndexCacheTable("dealorderlocation","dealorderlocationid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("coverletter");
                SqlCacheManager.IndexCacheTable("coverletter","coverletterid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("webcatalog");
                SqlCacheManager.IndexCacheTable("webcatalog","webcatalogid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("wardrobecare"  );
                SqlCacheManager.IndexCacheTable("wardrobecare","wardrobecareid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("wardrobesource");
                SqlCacheManager.IndexCacheTable("wardrobesource","wardrobesourceid");
            }));
            tasks.Add(Task.Run(()=> {
                SqlCacheManager.CacheTable("orderunit");
                SqlCacheManager.IndexCacheTable("orderunit","orderunitid");
            }));
        }
    }
}
