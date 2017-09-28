using FwStandard.Options;
using FwStandard.Security;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace RentalWorksWebLibrary
{
    public class SecurityTree : FwSecurityTree
    {
        //---------------------------------------------------------------------------------------------
        public SecurityTree(SqlServerOptions sqlServerOptions, string currentApplicationId) : base(sqlServerOptions, currentApplicationId)
        {
            var system = AddSystem("RentalWorks", "{4AC8B3C9-A2C2-4085-8F7F-EE005CCEB535}");
            BuildRentalWorksWebTree(system);
            BuildRentalWorksWebApiTree(system);
            BuildRentalWorksQuikScanTree(system);

            // Build all branches in this assembly
            List<Type> appTypes = typeof(SecurityTree).Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(FwStandard.Security.FwSecurityTreeBranch))).ToList<Type>();
            foreach (Type type in appTypes)
            {
                FwSecurityTreeBranch branch = (FwSecurityTreeBranch)Activator.CreateInstance(type);
                branch.BuildBranch(this);
            }
        }
        //---------------------------------------------------------------------------------------------
        private void BuildRentalWorksWebTree(FwSecurityTreeNode system)
        {
            string fileiconbaseurl          = "theme/images/icons/file/";
            string settingsiconbaseurl      = "theme/images/icons/settings/";
            //string reportsiconbaseurl       = "theme/images/icons/reports/";
            //string utilitiesiconbaseurl     = "theme/images/icons/utilities/";
            string administratoriconbaseurl = "theme/images/icons/administrator/";

            var application = AddApplication("RentalWorks Web", "{0A5F2584-D239-480F-8312-7C2B552A30BA}", system.Id);
            var lv1menuRentalWorks   = AddLv1ModuleMenu("RentalWorks",     "{91D2F0CF-2063-4EC8-B38D-454297E136A8}", application.Id);
            var lv1menuSettings      = AddLv1ModuleMenu("Settings",        "{730C9659-B33B-493E-8280-76A060A07DCE}", application.Id);
            var lv1menuReports       = AddLv1ModuleMenu("Reports",         "{7FEC9D55-336E-44FE-AE01-96BF7B74074C}", application.Id);
            var lv1menuUtilities     = AddLv1ModuleMenu("Utilities",       "{81609B0E-4B1F-4C13-8BE0-C1948557B82D}", application.Id);
            var lv1menuAdministrator = AddLv1ModuleMenu("Administrator",   "{F188CB01-F627-4DD3-9B91-B6486F0977DC}", application.Id);
            var lv1menuSubModules    = AddLv1SubModulesMenu("Sub-Modules", "{B8E34B04-EB99-4068-AD9E-BDC32D02967A}", application.Id);
            var lv1menuGrids         = AddLv1GridsMenu("Grids",            "{43765919-4291-49DD-BE76-F69AA12B13E8}", application.Id);

            //RentalWorks
            AddModule("Contact", "{3F803517-618A-41C0-9F0B-2C96B8BDAFC4}", lv1menuRentalWorks.Id, "ContactController", "module/contact", fileiconbaseurl + "contact.png");
            AddModule("Vendor", "{AE4884F4-CB21-4D10-A0B5-306BD0883F19}", lv1menuRentalWorks.Id, "VendorController", "module/vendor", fileiconbaseurl + "placeholder.png");
            AddModule("Customer", "{214C6242-AA91-4498-A4CC-E0F3DCCCE71E}", lv1menuRentalWorks.Id, "CustomerController", "module/customer", fileiconbaseurl + "contact.png");
            AddModule("Deal", "{C67AD425-5273-4F80-A452-146B2008B41C}", lv1menuRentalWorks.Id, "DealController", "module/deal", fileiconbaseurl + "contact.png");
            AddModule("Rental Inventory", "{FCDB4C86-20E7-489B-A8B7-D22EE6F85C06}", lv1menuRentalWorks.Id, "RentalInventoryController", "module/rentalinventory", fileiconbaseurl + "contact.png");
            AddModule("Sales Inventory", "{B0CF2E66-CDF8-4E58-8006-49CA68AE38C2}", lv1menuRentalWorks.Id, "SalesInventoryController", "module/salesinventory", fileiconbaseurl + "contact.png");
            

            //Settings 
            var lv2menuAccountingSettings = AddLv2ModuleMenu("Accounting Settings", "{BAF9A442-BA44-4DD1-9119-905C1A8FF199}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                               AddModule("Chart of Accounts", "{F03CA227-99EE-42EF-B615-94540DCB21B3}", lv2menuAccountingSettings.Id, "GlAccountController", "module/glaccount", settingsiconbaseurl + "placeholder.png", "","", "", "#838b83");

            var lv2menuAddressSettings = AddLv2ModuleMenu("Address Settings", "{2ABD806F-D059-4CCC-87C0-C4AE01B46EBC}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                            AddModule("Country"        , "{D6E787E6-502B-4D36-B0A6-FA691E6D10CF}", lv2menuAddressSettings.Id, "CountryController", "module/country", settingsiconbaseurl + "placeholder.png", "", "", "", "#eed5b7");
                                            AddModule("State/Province" , "{B70B4B88-51EB-4635-971B-1F676243B810}", lv2menuAddressSettings.Id, "StateController"  , "module/state"  , settingsiconbaseurl + "placeholder.png", "", "", "", "#eed5b7");

            AddModule("Bar Code Range", "{9A52C5B8-98AB-49A0-A392-69DB0873F943}", lv1menuSettings.Id, "BarCodeRangeController", "module/barcoderange", settingsiconbaseurl + "placeholder.png");

            AddModule("Billing Cycle", "{5736D549-CEA7-4FCF-86DA-0BCD4C87FA04}", lv1menuSettings.Id, "BillingCycleController", "module/billingcycle", settingsiconbaseurl + "placeholder.png");

            AddModule("Company Department", "{A6CC5F50-F9DE-4158-B627-6FDC1044C22A}", lv1menuSettings.Id, "DepartmentController", "module/department", settingsiconbaseurl + "placeholder.png");

            var lv2menuContactSettings = AddLv2ModuleMenu("Contact Settings", "{3FFAC65E-83B5-45DC-87DC-8383C5BD228C}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                            AddModule("Contact Event", "{25ad258e-db9d-4e94-a500-0382e7a2024a}", lv2menuContactSettings.Id, "ContactEventController", "module/contactevent", settingsiconbaseurl + "placeholder.png", "", "", "", "#8b8989");
                                            AddModule("Contact Title", "{1b9183b2-add9-416d-a5e1-59fe68104e4a}", lv2menuContactSettings.Id, "ContactTitleController", "module/contacttitle", settingsiconbaseurl + "placeholder.png", "", "", "", "#8b8989");
                                            AddModule("Mail List", "{255ceb68-fb87-4248-ab99-37c18a192300}", lv2menuContactSettings.Id, "MailListController", "module/maillist", settingsiconbaseurl + "placeholder.png", "", "", "", "#8b8989");

            AddModule("Currency", "{672145d0-9b37-4f6f-a216-9ae1e7728168}", lv1menuSettings.Id, "CurrencyController", "module/currency", settingsiconbaseurl + "placeholder.png");

            AddModule("Fiscal Year", "{6F87E62B-F17A-48CB-B673-16D12B6DFFB9}", lv1menuSettings.Id, "FiscalYearController", "module/fiscalyear", settingsiconbaseurl + "placeholder.png");

            //AddModule("Tax Option", "{5895CA39-5EF8-405B-9E97-2FEB83939EE5}", lv1menuSettings.Id, "TaxOptionsController", "module/taxoptions", settingsiconbaseurl + "placeholder.png");

            //AddModule("Ship Via", "{F9E01296-D240-4E16-B267-898787B29509}", lv1menuSettings.Id, "ShipViaController", "module/shipvia", settingsiconbaseurl + "placeholder.png");

            var lv2menuCustomerSettings = AddLv2ModuleMenu("Customer Settings", "{E2D6AE9E-9131-475A-AB42-0F34356760A6}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                             AddModule("Credit Status", "{A28D0CC9-B922-4259-BA4A-A5DE474ADFA4}", lv2menuCustomerSettings.Id, "CreditStatusController", "module/creditstatus", settingsiconbaseurl + "placeholder.png", "", "", "", "#cdcdc1");
                                             AddModule("Customer Category", "{8FB6C746-AB6E-4CA5-9BD4-4E9AD88A3BC5}", lv2menuCustomerSettings.Id, "CustomerCategoryController", "module/customercategory", settingsiconbaseurl + "placeholder.png", "", "", "", "#cdcdc1");
                                             AddModule("Customer Status", "{B689A0AA-9FCC-450B-AF0F-AD85483531FA}", lv2menuCustomerSettings.Id, "CustomerStatusController", "module/customerstatus", settingsiconbaseurl + "placeholder.png", "", "", "", "#cdcdc1");
                                             AddModule("Customer Type"  , "{314EDC6F-478A-40E2-B17E-349886A85EA0}", lv2menuCustomerSettings.Id, "CustomerTypeController"  , "module/customertype"  , settingsiconbaseurl + "placeholder.png", "", "", "", "#cdcdc1");

            var lv2menuDealSettings = AddLv2ModuleMenu("Deal Settings", "{C78B1F90-46B2-4FA1-AC35-139A8B5473FD}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                         AddModule("Deal Classification", "{D1035FCC-D92B-4A3A-B985-C7E02CBE3DFD}", lv2menuDealSettings.Id, "DealClassificationController", "module/dealclassification", settingsiconbaseurl + "placeholder.png");
                                         AddModule("Deal Type", "{A021AE67-0F33-4C97-9149-4CD5560EE10A}", lv2menuDealSettings.Id, "DealTypeController", "module/dealtype", settingsiconbaseurl + "placeholder.png");
                                         AddModule("Deal Status", "{543F8F83-20AB-4001-8283-1E73A9D795DF}", lv2menuDealSettings.Id, "DealStatusController", "module/dealstatus", settingsiconbaseurl + "placeholder.png");
                                         AddModule("Production Type", "{993EBF0C-EDF0-47A2-8507-51492502088B}", lv2menuDealSettings.Id, "ProductionTypeController", "module/productiontype", settingsiconbaseurl + "placeholder.png");
                                         AddModule("Schedule Type", "{8646d7bb-9676-4fdd-b9ea-db98045390f4}", lv2menuDealSettings.Id, "ScheduleTypeController", "module/scheduletype", settingsiconbaseurl + "placeholder.png");

            var lv2menuDocumentSettings = AddLv2ModuleMenu("Document Settings", "{CE6E0F99-8A5E-4359-B1A1-ECBDCCC43659}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                             AddModule("Document Type", "{358fbe63-83a7-4ab4-973b-1a5520573674}", lv2menuDocumentSettings.Id, "DocumentTypeController", "module/documenttype", settingsiconbaseurl + "placeholder.png");
                                             AddModule("Cover Letter", "{BE13DA09-E3AA-4520-A16C-F43F1A207EA5}", lv2menuDocumentSettings.Id, "CoverLetterController", "module/coverletter", settingsiconbaseurl + "placeholder.png");
                                             AddModule("Terms & Conditions", "{5C09A4C3-4272-458A-80DA-A5DF6B098D02}", lv2menuDocumentSettings.Id, "TermsConditionsController", "module/termsconditions", settingsiconbaseurl + "placeholder.png");

            var lv2menuEventSettings = AddLv2ModuleMenu("Event Settings", "{AC96C86F-C229-4E35-8978-859BDAC5865B}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                          AddModule("Event Category", "{3912b3cc-b35f-434d-aeeb-c45fed537e29}", lv2menuEventSettings.Id, "EventCategoryController", "module/eventcategory", settingsiconbaseurl + "placeholder.png");
                                          AddModule("Personnel Type", "{46339c9c-c663-4041-aeb4-a7f85783996f}", lv2menuEventSettings.Id, "PersonnelTypeController", "module/personneltype", settingsiconbaseurl + "placeholder.png");
                                          AddModule("Photography Type", "{66bff7f0-8bca-4d32-bd94-6b5f13623bec}", lv2menuEventSettings.Id, "PhotographyTypeController", "module/photographytype", settingsiconbaseurl + "placeholder.png");

            var lv2menuFacilitiesSettings = AddLv2ModuleMenu("Facilities Settings", "{CEDB17C6-BD90-4469-B104-B9492A5C4E96}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                               AddModule("Facility Type", "{197BBE51-28A8-4D00-BD0C-098C0F88DD0E}", lv2menuFacilitiesSettings.Id, "FacilityTypeController", "module/facilitytype", settingsiconbaseurl + "placeholder.png");
                                               AddModule("Facility Schedule Status", "{A693C2F7-DF16-4492-9DE5-FC672375C44E}", lv2menuFacilitiesSettings.Id, "FacilityScheduleStatusController", "module/facilityschedulestatus", settingsiconbaseurl + "placeholder.png");
                                               AddModule("Facility Status", "{DB2C8448-9287-4885-952F-BE3D0E4BFEF1}", lv2menuFacilitiesSettings.Id, "FacilityStatusController", "module/facilitystatus", settingsiconbaseurl + "placeholder.png");
                                               AddModule("Facility Category", "{67A9BEC5-4865-409C-9327-B2B8714DDAA8}", lv2menuFacilitiesSettings.Id, "FacilityCategoryController", "module/facilitycategory", settingsiconbaseurl + "placeholder.png");

            var lv2menuGeneratorSettings = AddLv2ModuleMenu("Generator Settings", "{711E8D44-E71F-4D10-B704-855E1018D20B}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                              AddModule("Generator Fuel Type", "{8A331FE0-B92A-4DD2-8A59-29E4E6D6EA4F}", lv2menuGeneratorSettings.Id, "GeneratorFuelTypeController", "module/generatorfueltype", settingsiconbaseurl + "placeholder.png");
                                              AddModule("Generator Make", "{D7C38A54-A198-4304-8EC2-CE8038D3BE9C}", lv2menuGeneratorSettings.Id, "GeneratorMakeController", "module/generatormake", settingsiconbaseurl + "placeholder.png");
                                              AddModule("Generator Rating", "{140E6997-1BA9-49B7-AA79-CD5EF6444C72}", lv2menuGeneratorSettings.Id, "GeneratorRatingController", "module/generatorrating", settingsiconbaseurl + "placeholder.png");
                                              AddModule("Generator Watts", "{503349D6-711A-4F45-8891-4B3203008441}", lv2menuGeneratorSettings.Id, "GeneratorWattsController", "module/generatorwatts", settingsiconbaseurl + "placeholder.png");

            var lv2menuHolidaySettings = AddLv2ModuleMenu("Holiday Settings", "{8A1C54ED-01B6-4EF5-AEBD-5E3F9F2563E0}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                            AddModule("Blackout Status", "{43D7C88D-8D8C-424E-94D3-A2C537F0C76E}", lv2menuHolidaySettings.Id, "BlackoutStatusController", "module/blackoutstatus", settingsiconbaseurl + "placeholder.png");

            var lv2menuInventorySettings = AddLv2ModuleMenu("Inventory Settings", "{A3FB2C11-082B-4602-B189-54B4B1B3E510}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                              AddModule("Inventory Condition", "{BF711CAC-1E69-4C92-B509-4CBFA29FAED3}", lv2menuInventorySettings.Id, "InventoryConditionController", "module/inventorycondition", settingsiconbaseurl + "placeholder.png");
                                              AddModule("Inventory Type", "{D62E0D20-AFF4-46A7-A767-FF32F6EC4617}", lv2menuInventorySettings.Id, "InventoryTypeController", "module/inventorytype", settingsiconbaseurl + "placeholder.png");
                                              AddModule("Inventory Adjustment Reason", "{B3156707-4D41-481C-A66E-8951E5233CDA}", lv2menuInventorySettings.Id, "InventoryAdjustmentReasonController", "module/inventoryadjustmentreason", settingsiconbaseurl + "placeholder.png");
                                              AddModule("Inventory Attribute", "{2777dd37-daca-47ff-aa44-29677b302745}", lv2menuInventorySettings.Id, "InventoryAttributeController", "module/inventoryattribute", settingsiconbaseurl + "placeholder.png");
                                              AddModule("Props Condition", "{86C769E8-F8E6-4C59-BC0B-8F2D563C698F}", lv2menuInventorySettings.Id, "PropsConditionController", "module/propscondition", settingsiconbaseurl + "placeholder.png");
                                              AddModule("Rental Status", "{E8E24D94-A07D-4388-9F2F-58FE028F24BB}", lv2menuInventorySettings.Id, "RentalStatusController", "module/rentalstatus", settingsiconbaseurl + "placeholder.png");
                                              AddModule("Retired Reason", "{1DE1DD87-47FD-4079-B7D8-B5DE61FCB280}", lv2menuInventorySettings.Id, "RetiredReasonController", "module/retiredreason", settingsiconbaseurl + "placeholder.png");
                                              AddModule("Unit of Measure", "{EE9F1081-BD9F-4004-A0CA-3813E2360642}", lv2menuInventorySettings.Id, "UnitController", "module/unit", settingsiconbaseurl + "placeholder.png");
                                              AddModule("Unretired Reason", "{C8E7F77B-52BC-435C-9971-331CF99284A0}", lv2menuInventorySettings.Id, "UnretiredReasonController", "module/unretiredreason", settingsiconbaseurl + "placeholder.png");
                                              AddModule("Parts Category", "{4750DFBD-6C60-41EF-83FE-49C8340D6062}", lv2menuInventorySettings.Id, "PartsCategoryController", "module/partscategory", settingsiconbaseurl + "placeholder.png");
                                              AddModule("Sales Category", "{428619B5-ABDE-48C4-9B2F-CF6D2A3AC574}", lv2menuInventorySettings.Id, "SalesCategoryController", "module/salescategory", settingsiconbaseurl + "placeholder.png");
                                              AddModule("Rental Category", "{91079439-A188-4637-B733-A7EF9A9DFC22}", lv2menuInventorySettings.Id, "RentalCategoryController", "module/rentalcategory", settingsiconbaseurl + "placeholder.png");
                                              AddModule("Warehouse Catalog", "{9045B118-A790-44FB-9867-3E8035EFEE69}", lv2menuInventorySettings.Id, "WarehouseCatalogController", "module/warehousecatalog", settingsiconbaseurl + "placeholder.png");

            var lv2menuLaborSettings = AddLv2ModuleMenu("Labor Settings", "{EE5CF882-B484-41C9-AE82-53D6AFFB3F25}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                              AddModule("Labor Type", "{6757DFC2-360A-450A-B2E8-0B8232E87D6A}", lv2menuLaborSettings.Id, "LaborTypeController", "module/labortype", settingsiconbaseurl + "placeholder.png");
                                              AddModule("Labor Category", "{2A5190B9-B0E8-4B93-897B-C91FC4807FA6}", lv2menuLaborSettings.Id, "LaborCategoryController", "module/laborcategory", settingsiconbaseurl + "placeholder.png");
                                              AddModule("Crew Schedule Status", "{E4E11656-0783-4327-A374-161BCFDF0F24}", lv2menuLaborSettings.Id, "CrewScheduleStatusController", "module/crewschedulestatus", settingsiconbaseurl + "placeholder.png");
                                              AddModule("Crew Status", "{73A6D9E3-E3BE-4B7A-BB3B-0AFE571C944E}", lv2menuLaborSettings.Id, "CrewStatusController", "module/crewstatus", settingsiconbaseurl + "placeholder.png");

            var lv2menuMiscSettings = AddLv2ModuleMenu("Misc Settings", "{2ED700C1-2D45-4307-9F92-41281185BD15}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                         AddModule("Misc Type", "{EAFEE5C7-84BB-419E-905A-3AE86E18DFAB}", lv2menuMiscSettings.Id, "MiscTypeController", "module/misctype", settingsiconbaseurl + "placeholder.png");
                                         AddModule("Misc Category", "{D5318A2F-ECB8-498A-9D9A-0846F4B9E4DF}", lv2menuMiscSettings.Id, "MiscCategoryController", "module/misccategory", settingsiconbaseurl + "placeholder.png");

            var lv2menuProjectSettings = AddLv2ModuleMenu("Project Settings", "{AE6366FC-48CD-496F-9DF7-B55E3EF27F63}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                        AddModule("As Build", "{A3BFF1F7-0951-4F3A-A6DE-1A62BEDF45E6}", lv2menuProjectSettings.Id, "AsBuildController", "module/asbuild", settingsiconbaseurl + "placeholder.png");
                                        AddModule("Items Ordered", "{25507FAD-E140-4A19-8FED-2C381DA653D9}", lv2menuProjectSettings.Id, "ItemsOrderedController", "module/itemsordered", settingsiconbaseurl + "placeholder.png");

            AddModule("Office Location", "{8A8EE5CC-458E-4E4B-BA09-9C514588D3BD}", lv1menuSettings.Id, "OfficeLocationController", "module/officelocation", settingsiconbaseurl + "placeholder.png");

            var lv2menuOrderSettings = AddLv2ModuleMenu("Order Settings", "{3D7A8032-9D56-4C89-BB53-E25799BE91BE}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                          AddModule("Discount Reason", "{CBBBFA51-DE2D-4A24-A50E-F7F4774016F6}", lv2menuOrderSettings.Id, "DiscountReasonController", "module/discountreason", settingsiconbaseurl + "placeholder.png");
                                          AddModule("Order Set No.", "{4960D9A7-D1E0-4558-B571-DF1CE1BB8245}", lv2menuOrderSettings.Id, "OrderSetNoController", "module/ordersetno", settingsiconbaseurl + "placeholder.png");
                                          AddModule("Order Location", "{CF58D8C9-95EE-4617-97B9-CAFE200719CC}", lv2menuOrderSettings.Id, "OrderLocationController", "module/orderlocation", settingsiconbaseurl + "placeholder.png");

            var lv2menuPaymentSettings = AddLv2ModuleMenu("Payment Settings", "{031F8E86-A1A6-482F-AB4F-8DD015AB7642}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                            AddModule("Payment Terms", "{44FD799A-1572-4B34-9943-D94FFBEF89D4}", lv2menuPaymentSettings.Id, "PaymentTermsController", "module/paymentterms", settingsiconbaseurl + "placeholder.png");
                                            AddModule("Payment Type", "{E88C4957-3A3E-4258-8677-EB6FB61F9BA3}", lv2menuPaymentSettings.Id, "PaymentTypeController", "module/paymenttype", settingsiconbaseurl + "placeholder.png");

            var lv2menuPOSettings = AddLv2ModuleMenu("PO Settings", "{55EDE544-A603-467D-AFA2-EC9C2A650810}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                       AddModule("PO Approver Role", "{992314B6-A24F-468C-A8B6-5EAC8F14BE16}", lv2menuPOSettings.Id, "POApproverRoleController", "module/poapproverrole", settingsiconbaseurl + "placeholder.png");
                                       AddModule("PO Classification", "{58ef51c5-a97b-43c6-9298-08b064a84a48}", lv2menuPOSettings.Id, "POClassificationController", "module/poclassification", settingsiconbaseurl + "placeholder.png");
                                       AddModule("PO Importance", "{82BF3B3E-0EF8-4A6E-8577-33F23EA9C4FB}", lv2menuPOSettings.Id, "POImportanceController", "module/poimportance", settingsiconbaseurl + "placeholder.png");
                                       AddModule("PO Reject Reason", "{2C6910A8-51BC-421E-898F-C23938B624B4}", lv2menuPOSettings.Id, "PORejectReasonController", "module/prorejectreason", settingsiconbaseurl + "placeholder.png");

            var lv2menuPresentationSettings = AddLv2ModuleMenu("Presentation Settings", "{471FF4FC-094B-4D20-B326-C2D7997F5424}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                              AddModule("Form Design", "{4DFEC75D-C33A-4358-9EF1-4D1F5F9C5D73}", lv2menuPresentationSettings.Id, "FormDesignController", "module/formdesign", settingsiconbaseurl + "placeholder.png");
                                              AddModule("Presentation Layer", "{BBEF0AFD-B46A-46B0-8046-113834736060}", lv2menuPresentationSettings.Id, "PresentationLayerController", "module/presentationlayer", settingsiconbaseurl + "placeholder.png");

            AddModule("Region", "{A50C7F59-AF91-44D5-8253-5C4A4D5DFB8B}", lv1menuSettings.Id, "RegionController", "module/region", settingsiconbaseurl + "placeholder.png");
            AddModule("Repair Item Status", "{D952672A-DCF6-47C8-9B99-47561C79B3F8}", lv1menuSettings.Id, "RepairItemStatusController", "module/repairitemstatus", settingsiconbaseurl + "placeholder.png");

            var lv2menuSetSettings = AddLv2ModuleMenu("Set Settings", "{210AB6DE-159D-4979-B321-3BC1EA6574D7}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                     AddModule("Set Condition", "{0FFC8940-C060-49E4-BC24-688E25250C5F}", lv2menuSetSettings.Id, "SetConditionController", "module/setcondition", settingsiconbaseurl + "placeholder.png");
                                     AddModule("Set Surface", "{EC55E743-0CB1-4A74-9D10-6C4C6045AAAB}", lv2menuSetSettings.Id, "SetSurfaceController", "module/setsurface", settingsiconbaseurl + "placeholder.png");
                                     AddModule("Set Opening", "{15E52CA3-475D-4BDA-B940-525E5EEAF8CD}", lv2menuSetSettings.Id, "SetOpeningController", "module/setopening", settingsiconbaseurl + "placeholder.png");

            AddModule("Ship Via", "{F9E01296-D240-4E16-B267-898787B29509}", lv1menuSettings.Id, "ShipViaController", "module/shipvia", settingsiconbaseurl + "placeholder.png");

            AddModule("Source", "{6D6165D1-51F2-4616-A67C-DCC803B549AF}", lv1menuSettings.Id, "SourceController", "module/source", settingsiconbaseurl + "placeholder.png");

            AddModule("Tax Options", "{5895CA39-5EF8-405B-9E97-2FEB83939EE5}", lv1menuSettings.Id, "TaxOptionsController", "module/taxoptions", settingsiconbaseurl + "placeholder.png");

            var lv2menuUserSettings = AddLv2ModuleMenu("User Settings", "{13E1A9A9-1096-447E-B4AE-E538BEF5BCB5}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                         AddModule("User Status", "{E19916C6-A844-4BD1-A338-FAB0F278122C}", lv2menuUserSettings.Id, "UserStatusController", "module/userstatus", settingsiconbaseurl + "placeholder.png");

            var lv2menuVehicleSettings = AddLv2ModuleMenu("Vehicle Settings", "{6081E168-E3BF-439E-82B0-34AF3680C444}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                            AddModule("License Class", "{422F777F-B57F-43DF-8485-F12F3F7BF662}", lv2menuVehicleSettings.Id, "LicenseClassController", "module/licenseclass", settingsiconbaseurl + "placeholder.png");
                                            AddModule("Vehicle Color", "{F7A34B70-509A-422F-BFD1-5F30BE2C8186}", lv2menuVehicleSettings.Id, "VehicleColorController", "module/vehiclecolor", settingsiconbaseurl + "placeholder.png");
                                            AddModule("Vehicle Fuel Type", "{D9140FB3-084D-4615-8E7A-95731670E682}", lv2menuVehicleSettings.Id, "VehicleFuelTypeController", "module/vehiclefueltype", settingsiconbaseurl + "placeholder.png");
                                            AddModule("Vehicle Make", "{299DECA3-B427-49ED-B6AC-2E11F6AA1C4D}", lv2menuVehicleSettings.Id, "VehicleMakeController", "module/vehiclemake", settingsiconbaseurl + "placeholder.png");
                                            AddModule("Vehicle Rating", "{09913CDB-68FB-4F18-BBAA-DCA8A8F926E5}", lv2menuVehicleSettings.Id, "VehicleRatingController", "module/vehiclerating", settingsiconbaseurl + "placeholder.png");
                                            AddModule("Vehicle Schedule Status", "{A001473B-1FB4-4E85-8093-37A92057CD93}", lv2menuVehicleSettings.Id, "VehicleScheduleStatusController", "module/vehicleschedulestatus", settingsiconbaseurl + "placeholder.png");
                                            AddModule("Vehicle Status", "{FB12061D-E6AF-4C09-95A0-8647930C289A}", lv2menuVehicleSettings.Id, "VehicleStatusController", "module/vehiclestatus", settingsiconbaseurl + "placeholder.png");

            var lv2menuVendorSettings = AddLv2ModuleMenu("Vendor Settings", "{93376B75-2771-474A-8C25-2BBE53B50F5C}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                           AddModule("Organization Type", "{fe3a764c-ab55-4ce5-8d7f-bfc86f174c11}", lv2menuVendorSettings.Id, "OrganizationTypeController", "module/organizationtype", settingsiconbaseurl + "placeholder.png");
                                           AddModule("Vendor Catalog", "{BDA5E2DC-0FD2-4227-B80F-8414F3F912B8}", lv2menuVendorSettings.Id, "VendorCatalogController", "module/vendorcatalog", settingsiconbaseurl + "placeholder.png");
                                           AddModule("Vendor Class", "{8B2C9EE3-AE87-483F-A651-8BA633E6C439}", lv2menuVendorSettings.Id, "VendorClassController", "module/vendorclass", settingsiconbaseurl + "placeholder.png");



            var lv2menuWardrobeSettings = AddLv2ModuleMenu("Wardrobe Settings", "{910DAD78-B2AA-4220-89D3-B6A0FA3E0BA1}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                             AddModule("Wardrobe Care", "{BE6E4F7C-5D81-4437-A343-8F4933DD6545}", lv2menuWardrobeSettings.Id, "WardrobeCareController", "module/wardrobecare", settingsiconbaseurl + "placeholder.png");
                                             AddModule("Wardrobe Color", "{32238B26-3635-4637-AFE0-0D5B12CAAEE4}", lv2menuWardrobeSettings.Id, "WardrobeColorController", "module/wardrobecolor", settingsiconbaseurl + "placeholder.png");
                                             AddModule("Wardrobe Condition", "{4EEBE71C-139A-4D09-B589-59DA576C83FD}", lv2menuWardrobeSettings.Id, "WardrobeConditionController", "module/wardrobecondition", settingsiconbaseurl + "placeholder.png");
                                             AddModule("Wardrobe Gender", "{28574D17-D2FF-41A0-8117-5F252013E7B1}", lv2menuWardrobeSettings.Id, "WardrobeGenderController", "module/wardrobegender", settingsiconbaseurl + "placeholder.png");
                                             AddModule("Wardrobe Label", "{9C1B5157-C983-44EE-817F-171B4448401A}", lv2menuWardrobeSettings.Id, "WardrobeLabelController", "module/wardrobelabel", settingsiconbaseurl + "placeholder.png");
                                             AddModule("Wardrobe Pattern", "{2BE7072A-5588-4205-8DCD-0FFE6F0C48F7}", lv2menuWardrobeSettings.Id, "WardrobePatternController", "module/wardrobepattern", settingsiconbaseurl + "placeholder.png");
                                             AddModule("Wardrobe Period", "{BF51623D-ABA6-471A-BC00-4729067C64CF}", lv2menuWardrobeSettings.Id, "WardrobePeriodController", "module/wardrobeperiod", settingsiconbaseurl + "placeholder.png");
                                             AddModule("Wardrobe Source", "{6709D1A1-3319-435C-BF0E-15D2602575B0}", lv2menuWardrobeSettings.Id, "WardrobeSourceController", "module/wardrobesource", settingsiconbaseurl + "placeholder.png");

            AddModule("Warehouse", "{931D3E75-68CB-4280-B12F-9A955444AA0C}", lv1menuSettings.Id, "WarehouseController", "module/warehouse", settingsiconbaseurl + "placeholder.png");


            //Reports 
            //var lv2menuDealReports = AddLv2ModuleMenu("Deal Reports",     "{B14EC8FA-15B6-470C-B871-FB83E7C24CB2}", lv1menuReports.Id,                                                              reportsiconbaseurl + "placeholder.png", "Deal Reports");
            //                                AddModule("Deal Outstanding", "{007F72D4-8767-472D-9706-8CDE8C8A9981}", lv2menuDealReports.Id, "RwDealOutstandingController", "module/dealoutstanding", reportsiconbaseurl + "placeholder.png", "Deal<br/>Outstanding", "", "");

            // Add Utilities 
            //var lv2menuChargeProcessing= AddLv2ModuleMenu("Charge Processing",       "{11349784-B621-468E-B0AD-899A22FCA9AE}", lv1menuUtilities.Id,                                                                                 utilitiesiconbaseurl + "placeholder.png", "Charge Processing");
            //                                    AddModule("Process Deal Invoices",   "{5DB3FB9C-6F86-4696-867A-9B99AB0D6647}", lv2menuChargeProcessing.Id, "RwChargeProcessingController",        "module/chargeprocessing",        utilitiesiconbaseurl + "placeholder.png", "", "", "");
            //                                    AddModule("Process Receipts",        "{0BB9B45C-57FA-47E1-BC02-39CEE720792C}", lv2menuChargeProcessing.Id, "RwReceiptProcessingController",       "module/receiptprocessing",       utilitiesiconbaseurl + "placeholder.png", "", "", "");
            //                                    AddModule("Process Vendor Invoices", "{4FA8A060-F2DF-4E59-8F9D-4A6A62A0D240}", lv2menuChargeProcessing.Id, "RwVendorInvoiceProcessingController", "module/vendorinvoiceprocessing", utilitiesiconbaseurl + "placeholder.png", "", "", "");

            // Add Administrator 
            AddModule("Group",       "{9BE101B6-B406-4253-B2C6-D0571C7E5916}", lv1menuAdministrator.Id, "GroupController",       "module/group",       administratoriconbaseurl + "group.png",                                    "USER");
            //AddModule("Integration", "{518B038E-F22A-4B23-AA47-F4F56709ADC3}", lv1menuAdministrator.Id, "RwIntegrationController", "module/integration", administratoriconbaseurl + "placeholder.png", "Integration", "quickbooks", "USER");
            AddModule("User",        "{79E93B21-8638-483C-B377-3F4D561F1243}", lv1menuAdministrator.Id, "UserController",        "module/user",        administratoriconbaseurl + "user.png",                                     "USER");
            AddModule("Settings Page", "{57150967-486A-42DE-978D-A2B0F843341A}", lv1menuAdministrator.Id, "SettingsPageController", "module/settingspage", administratoriconbaseurl + "placeholder.png");

            // Add Submodules
            AddSubModule("User Settings", "{A6704904-01E1-4C6B-B75A-C1D3FCB50C01}", lv1menuSubModules.Id, "UserSettingsController");

            // Add Grids
            AddGrid("Audit History",                  "{FA958D9E-7863-4B03-94FE-A2D2B9599FAB}", lv1menuGrids.Id, "FwAuditHistoryGridController");
            AddGrid("Billing Cycle Events",           "{8AAD752A-74B8-410D-992F-08398131EBA7}", lv1menuGrids.Id, "BillingCycleEventsGridController");
            AddGrid("Contact",                        "{B6A0CAFC-35E8-4490-AEED-29F4E3426758}", lv1menuGrids.Id, "RwContactGridController");
            AddGrid("Contact Company",                "{7E1840AE-9832-4E0E-9B1F-A2A115575852}", lv1menuGrids.Id, "FwContactCompanyGridController");
            AddGrid("Contact Document",               "{CC8F52FF-D968-4CE6-BF7A-3AC859D25280}", lv1menuGrids.Id, "FwContactDocumentGridController");
            AddGrid("Contact Email History",          "{DAA5E81D-353C-4AAA-88A8-B4E7046B5FF0}", lv1menuGrids.Id, "FwContactEmailHistoryGridController");
            AddGrid("Contact Note",                   "{A9CB5D4D-4AC0-46D4-A084-19039CF8C654}", lv1menuGrids.Id, "FwContactNoteGridController");
            AddGrid("Contact Personal Event",         "{C40394BA-E805-4A49-A4D0-938B2A84D9A7}", lv1menuGrids.Id, "FwContactPersonalEventGridController");
            AddGrid("Customer Note",                  "{50EB024E-6D9A-440A-8161-458A2E89EFB8}", lv1menuGrids.Id, "CustomerNoteGridController");
            AddGrid("Customer Resale",                "{571F090C-D7EC-4D95-BA7B-84D09B609F39}", lv1menuGrids.Id, "CustomerResaleGridController");
            AddGrid("Document Version",               "{397FF02A-BF19-4C1F-8E5F-9DBE786D77EC}", lv1menuGrids.Id, "FwAppDocumentVersionGridController");
            AddGrid("Generator Make Model",           "{12109673-165E-4620-8121-AF4259C7F367}", lv1menuGrids.Id, "GeneratorMakeModelGridController");
            AddGrid("Inventory Attribute Value",      "{D591CCE2-920C-440D-A6D7-6F4F21FC01B8}", lv1menuGrids.Id, "PresentationLayerActivityGridController");
            AddGrid("Master Item",                    "{F21525ED-EDAC-4627-8791-0B410C74DAAE}", lv1menuGrids.Id, "RwMasterItemGridController");
            AddGrid("Order Activity Dates",           "{E00980E5-7A1C-4438-AB06-E8B7072A7595}", lv1menuGrids.Id, "RwOrderActivityDatesGridController");
            AddGrid("Order Contract Note",            "{2018FEB8-D15D-4F1C-B09D-9BCBD5491B52}", lv1menuGrids.Id, "RwOrderContractNoteGridController");
            AddGrid("Order Dates",                    "{D4B28F52-5C9D-4D8C-B58C-42924428DE93}", lv1menuGrids.Id, "RwOrderDatesGridController");
            AddGrid("Order Note",                     "{45573B9C-B39D-4975-BC36-4A41362E1AF0}", lv1menuGrids.Id, "RwOrderNoteGridController");
            AddGrid("Presentation Layer Activity",    "{AA12FF6E-DE89-4C9A-8DB6-E42542BB1689}", lv1menuGrids.Id, "PresentationLayerActivityGridController");
            AddGrid("Quik Entry Accessories Options", "{27317105-BA68-417A-A592-86EEB977CA32}", lv1menuGrids.Id, "RwQuikEntryAccessoriesOptionsGridController");
            AddGrid("Quik Entry Category",            "{01604AEC-2127-4756-BF92-3A340EF000E1}", lv1menuGrids.Id, "RwQuikEntryCategoryGridController");
            AddGrid("Quik Entry Department",          "{2AC10F3D-FC50-4454-87C2-54ABBCCD08AB}", lv1menuGrids.Id, "RwQuikEntryDepartmentGridController");
            AddGrid("Quik Entry Items",               "{1289FF25-5C86-43CC-8557-173E7EA69696}", lv1menuGrids.Id, "RwQuikEntryItemsGridController");
            AddGrid("Quik Entry Sub Category",        "{26576DCB-4141-477A-9A3D-4F76D862C581}", lv1menuGrids.Id, "RwQuikEntrySubCategoryGridController");
            AddGrid("Company Tax",                    "{0679DED3-7CDF-468D-8513-7271024403A6}", lv1menuGrids.Id, "CompanyTaxGridController");
            AddGrid("Vehicle Make Model",             "{C10EC66E-AA26-4BF6-93BF-35307715FE44}", lv1menuGrids.Id, "VehicleMakeModelGridController");
            AddGrid("Vendor Note",                    "{60704925-2642-4864-A5E8-272313978CE3}", lv1menuGrids.Id, "VendorNoteGridController");
            AddGrid("Sub Category",                   "{070EBAE0-903E-48CE-9285-BDC3ECC07C68}", lv1menuGrids.Id, "SubCategoryGridController");
        }

        //---------------------------------------------------------------------------------------------
        private void BuildRentalWorksWebApiTree(FwSecurityTreeNode system)
        {
            var application = AddApplication("RentalWorks Web Api", "{94FBE349-104E-420C-81E9-1636EBAE2836}", system.Id);
            var lv1menuRentalWorks   = AddLv1ModuleMenu("RentalWorks",     "{B8783E09-72C9-4EA0-A692-4581CBEF1FD5}", application.Id);
            var lv1menuSettings      = AddLv1ModuleMenu("Settings",        "{39EF57C5-F704-472C-8509-A11DDF77C6A0}", application.Id);

            // Home
            AddController("Contact", "{DFCB870E-62CF-439E-A0FA-493A876F43B5}", lv1menuRentalWorks.Id);

            // Settings
            AddController("BillingCycle", "{4CE12A3E-1831-4D35-95AF-01F1BF0A5214}", lv1menuSettings.Id);
            AddController("BillPeriod", "{39AC96F1-2834-491C-BCDE-92CD8E2B11AA}", lv1menuSettings.Id);
            AddController("BlackoutStatus", "{DCA313A2-92A3-41CA-B203-38BE725743A8}", lv1menuSettings.Id);
            AddController("ContactEvent", "{CC37FF90-094F-4A5D-B4D4-3552B5B8D65A}", lv1menuSettings.Id);
            AddController("ContactTitle", "{D756CEE2-AE67-48BD-88A7-FFE855A8E590}", lv1menuSettings.Id);
            AddController("Country", "{251CEBD0-5EF5-4025-B3FF-84840F0D2525}", lv1menuSettings.Id);
            AddController("CreditStatus", "{CD2569F2-6BEB-488C-AB9F-3A6C6D821ED3}", lv1menuSettings.Id);
            AddController("Currency", "{04D83434-3298-4187-8327-84942CF1CB03}", lv1menuSettings.Id);
            AddController("CustomerCategory", "{545C1F2F-BB9C-422C-8E41-A69DAF9DAC9E}", lv1menuSettings.Id);
            AddController("CustomerStatus", "{D9FEADB4-F079-4437-834C-4D8F5C0ACFA5}", lv1menuSettings.Id);
            AddController("CustomerType", "{8B861232-B0CC-4CDA-AD86-4B72AA721B73}", lv1menuSettings.Id);
            AddController("DealClassification", "{3CABAC95-7BEC-433F-9D32-11E76AAE229A}", lv1menuSettings.Id);
            AddController("DealStatus", "{01C4EC0C-F4C0-4E8C-A079-D2CA37A5EDA1}", lv1menuSettings.Id);
            AddController("DealType", "{44BFDDB3-D93B-4A99-A38F-A0CE8162413F}", lv1menuSettings.Id);
            AddController("Department", "{0DF9DBC3-8755-460C-B830-FF169CC3D859}", lv1menuSettings.Id);
            AddController("DocumentType", "{343FD936-D5A3-420A-8B72-8A10248C39E4}", lv1menuSettings.Id);
            AddController("EventCategory", "{7D659770-FEC6-4580-A93B-29C766B9DA4B}", lv1menuSettings.Id);
            AddController("FacilityScheduleStatus", "{3249CECE-FCDC-4B51-96B6-13609CB9F642}", lv1menuSettings.Id);
            AddController("FacilityStatus", "{61C5A81A-AAB3-43C7-9239-4E08CE8F3F34}", lv1menuSettings.Id);
            AddController("GeneratorFuelType", "{FC2DA3CA-B5CC-4490-8D20-C121A1D3F7D6}", lv1menuSettings.Id);
            AddController("GeneratorRating", "{03C0E24C-BB02-4924-A7B2-8B1DDF171827}", lv1menuSettings.Id);
            AddController("GLAccount", "{04730651-F4CD-47BC-ABAD-3B0B0F929AFD}", lv1menuSettings.Id);
            AddController("MailList", "{1ED190A8-051D-46C9-823E-825021CCCDBE}", lv1menuSettings.Id);
            AddController("OfficeLocation", "{C78B7368-C42E-4546-83CC-B7B6A1006FF0}", lv1menuSettings.Id);
            AddController("OrganizationType", "{850B2B9F-A3A8-4162-809D-DFBD52262B76}", lv1menuSettings.Id);
            AddController("PaymentTerms", "{E37D94D8-FA1F-41F2-A0E5-8562ED76F015}", lv1menuSettings.Id);
            AddController("PaymentType", "{326750A2-B9DA-4245-9D44-485724CD6669}", lv1menuSettings.Id);
            AddController("PersonnelType", "{874B18DE-2BA6-4427-9D20-54F64072C003}", lv1menuSettings.Id);
            AddController("PhotographyType", "{B3FCE468-13A0-47F4-A1A4-E16D3264E544}", lv1menuSettings.Id);
            AddController("PoClassification", "{65EE7A12-3879-4B4A-892E-AE1C204599F8}", lv1menuSettings.Id);
            AddController("ProductionType", "{309300EC-4CCB-421A-BAF6-64005D2C9DE9}", lv1menuSettings.Id);
            AddController("Region", "{105E6E96-475E-4747-92C9-164F2E8F2DD7}", lv1menuSettings.Id);
            AddController("ScheduleType", "{06DA0B03-A6B0-4719-B2AB-9482991F5867}", lv1menuSettings.Id);
            AddController("State", "{053B9AE1-160F-4FC7-AEEC-A5669E8879D6}", lv1menuSettings.Id);
            AddController("VendorClass", "{61ABD143-5A80-424F-998A-A264F5806709}", lv1menuSettings.Id);
            AddController("Warehouse", "{997CF52E-BA31-44BA-A3A3-D8684FFFB15B}", lv1menuSettings.Id);
        }
        //---------------------------------------------------------------------------------------------
        private void BuildRentalWorksQuikScanTree(FwSecurityTreeNode system)
        {
            var iconbaseurl = "theme/images/icons/128/";
            AddApplication("RentalWorks QuikScan", "{8D0A5ECF-72D2-4428-BDC8-7E3CC56EDD3A}", system.Id);
            var lv1menuHome   = AddLv1ModuleMenu("Home", "{512418CD-7977-4B7A-B773-F7FC0A05397C}", "{8D0A5ECF-72D2-4428-BDC8-7E3CC56EDD3A}");
            //AddModule("RFID Staging",        "{3C8C0600-38F1-4CB8-B10A-D0BBE368B16B}", lv1menuHome.Id, "", "rfidstaging",                     iconbaseurl + "rfidstaging.001.png",      "RFID<br>Staging",  "rfid",          "USER");
            //AddModule("RFID Check-In",       "{3D75B8A9-E828-4915-91D6-E8810F74655B}", lv1menuHome.Id, "", "rfidcheckin",                     iconbaseurl + "rfidstaging.001.png",      "RFID<br>Check-In", "rfid",          "USER");
            AddModule("Staging",             "{D8FC5192-8AC0-431D-96FA-451E70A07471}", lv1menuHome.Id, "", "staging",                         iconbaseurl + "staging.png",              "",                 "",              "USER");
            AddModule("Check-In",            "{E83AE9DA-8394-4DB9-8D33-FC826D0C55B8}", lv1menuHome.Id, "", "order/checkinmenu",               iconbaseurl + "checkin.png",              "",                 "",              "USER");
            AddModule("Receive On Set",      "{78013147-1A63-4FF1-865E-783D907FFDBA}", lv1menuHome.Id, "", "receiveonset",                    iconbaseurl + "receiveset.png",           "",                 "production",    "USER");
            AddModule("Item Set Location",   "{30AD950B-A415-484F-AAD6-DAED0827B78C}", lv1menuHome.Id, "", "assetsetlocation",                iconbaseurl + "setlocation.png",          "",                 "production",    "USER");
            //AddModule("Exchange",            "{D4A70546-CC08-49F3-9089-35BD86B3EED6}", lv1menuHome.Id, "", "utilities/exchange",            iconbaseurl + "exchange.png",              "",                 "",              "USER");
            AddModule("PO Receive",          "{0F89A039-5853-4921-A384-E70403667C14}", lv1menuHome.Id, "", "inventory/subreceive",            iconbaseurl + "subreceive.png",           "PO<br>Receive",    "",              "USER");
            AddModule("PO Return",           "{7AFB6196-0484-4581-8813-402A1B7F21BF}", lv1menuHome.Id, "", "inventory/subreturn",             iconbaseurl + "subreturn.png",            "PO<br>Return",     "",              "USER");
            AddModule("Item Status",         "{EBC4AA8F-33E0-4D8B-AE16-14117211E70B}", lv1menuHome.Id, "", "order/itemstatus",                iconbaseurl + "orderstatus.png",          "",                 "",              "USER");
            AddModule("QC",                  "{F005C702-4FCA-4002-B45A-ED6B0B676452}", lv1menuHome.Id, "", "inventory/qc",                    iconbaseurl + "qc.png",                   "",                 "",              "USER");
            AddModule("Transfer Out",        "{E007E8AB-897E-4422-90CA-F6E545BFA425}", lv1menuHome.Id, "", "transferout",                     iconbaseurl + "transferout.png",          "",                 "",              "USER");
            AddModule("Transfer In",         "{57340E4F-D5CA-4EBF-A25D-6A1FE265A986}", lv1menuHome.Id, "", "order/transferin",                iconbaseurl + "transferin.png",           "",                 "",              "USER");
            AddModule("Repair",              "{B93B03F3-F281-4A4E-81C5-5F6A3CA6B7B5}", lv1menuHome.Id, "", "inventory/repairmenu",            iconbaseurl + "repair.png",               "",                 "",              "USER");
            AddModule("Asset Disposition",   "{1573C03C-5ADA-407C-8B7E-D2158920D1CC}", lv1menuHome.Id, "", "inventory/assetdisposition",      iconbaseurl + "assetdisposition.001.png", "",                 "production",    "USER");
            AddModule("Package Truck",       "{2F83A5EC-5423-4520-9B3D-8845C7D5F1B6}", lv1menuHome.Id, "", "order/packagetruck",              iconbaseurl + "package-truck.001.png",    "",                 "packagetruck",  "USER");
            AddModule("QuikPick",            "{1C95FD02-4D0E-4C29-91B7-1166D7690831}", lv1menuHome.Id, "", "quote/quotemenu",                 iconbaseurl + "quikpick.png",             "",                 "",              "USER");
            AddModule("Time Log",            "{0B2BB33B-C463-45C4-9131-05A78CD217F4}", lv1menuHome.Id, "", "timelog",                         iconbaseurl + "timelog.png",              "",                 "crew",          "USER,CREW");
            AddModule("Fill Container",      "{59187AA1-8F90-4AEC-B771-A84EC59A83F1}", lv1menuHome.Id, "", "order/fillcontainer",             iconbaseurl + "fillcontainer.png",        "",                 "container",     "USER");
            AddModule("Inventory Web Image", "{9E49037B-331B-47AC-88C9-C4DE5EABD4DD}", lv1menuHome.Id, "", "inventory/inventorywebimage",     iconbaseurl + "webimage.png",             "",                 "",              "USER");
            AddModule("Physical Inventory",  "{36A96F73-AAF1-465B-9A60-34F1160AEDAD}", lv1menuHome.Id, "", "utilities/physicalinventory",     iconbaseurl + "physicalinv.png",          "",                 "",              "USER");
            AddModule("Move To Aisle/Shelf", "{80B6DE16-DE6E-4D49-869F-DA14BCE3422E}", lv1menuHome.Id, "", "inventory/movebclocation",        iconbaseurl + "moveto.png",               "",                 "",              "USER");
            AddModule("Assign Items",        "{0383B8A9-EB64-4C8A-B6F2-9E3528C093DB}", lv1menuHome.Id, "", "assignitems",                     iconbaseurl + "assignitems.001.png",      "",                 "",              "USER");
            AddModule("Barcode Label",       "{05B4FAF1-9329-43E9-9697-BE461E41D85F}", lv1menuHome.Id, "", "barcodelabel",                    iconbaseurl + "barcodelabel.001.png",     "",                 "",              "USER");
        }
        //---------------------------------------------------------------------------------------------
    }
}