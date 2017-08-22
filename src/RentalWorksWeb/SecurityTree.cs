using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fw.Json.ValueTypes;

namespace RentalWorksWeb
{
    public class SecurityTree : FwApplicationTree
    {
        //---------------------------------------------------------------------------------------------
        public SecurityTree() : base()
        {
            AddSystem("RentalWorks", "{4AC8B3C9-A2C2-4085-8F7F-EE005CCEB535}");
            BuildRentalWorksWebTree();
            BuildRentalWorksQuikScanTree();
        }
        //---------------------------------------------------------------------------------------------
        private void BuildRentalWorksWebTree()
        {
            string fileiconbaseurl          = "theme/images/icons/file/";
            string settingsiconbaseurl      = "theme/images/icons/settings/";
            string reportsiconbaseurl       = "theme/images/icons/reports/";
            string utilitiesiconbaseurl     = "theme/images/icons/utilities/";
            string administratoriconbaseurl = "theme/images/icons/administrator/";

            var application = AddApplication("RentalWorks Web", "{0A5F2584-D239-480F-8312-7C2B552A30BA}", "{4AC8B3C9-A2C2-4085-8F7F-EE005CCEB535}");
            var lv1menuRentalWorks   = AddLv1ModuleMenu("RentalWorks",     "{91D2F0CF-2063-4EC8-B38D-454297E136A8}", application.Id);
            var lv1menuSettings      = AddLv1ModuleMenu("Settings",        "{730C9659-B33B-493E-8280-76A060A07DCE}", application.Id);
            var lv1menuReports       = AddLv1ModuleMenu("Reports",         "{7FEC9D55-336E-44FE-AE01-96BF7B74074C}", application.Id);
            var lv1menuUtilities     = AddLv1ModuleMenu("Utilities",       "{81609B0E-4B1F-4C13-8BE0-C1948557B82D}", application.Id);
            var lv1menuAdministrator = AddLv1ModuleMenu("Administrator",   "{F188CB01-F627-4DD3-9B91-B6486F0977DC}", application.Id);
            var lv1menuSubModules    = AddLv1SubModulesMenu("Sub-Modules", "{B8E34B04-EB99-4068-AD9E-BDC32D02967A}", application.Id);
            var lv1menuGrids         = AddLv1GridsMenu("Grids",            "{43765919-4291-49DD-BE76-F69AA12B13E8}", application.Id);

            //RentalWorks
            AddModule("Contact", "{3F803517-618A-41C0-9F0B-2C96B8BDAFC4}", lv1menuRentalWorks.Id, "ContactController", "module/contact", fileiconbaseurl + "contact.png");
            
            //Settings 
            var lv2menuAccountingMaintenance = AddLv2ModuleMenu("Accounting Maintenance", "{BAF9A442-BA44-4DD1-9119-905C1A8FF199}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                               AddModule("Chart of Accounts", "{F03CA227-99EE-42EF-B615-94540DCB21B3}", lv2menuAccountingMaintenance.Id, "GlAccountController", "module/glaccount", settingsiconbaseurl + "placeholder.png");

            var lv2menuAddressMaintenance = AddLv2ModuleMenu("Address Maintenance", "{2ABD806F-D059-4CCC-87C0-C4AE01B46EBC}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                            AddModule("Country"        , "{D6E787E6-502B-4D36-B0A6-FA691E6D10CF}", lv2menuAddressMaintenance.Id, "CountryController", "module/country", settingsiconbaseurl + "placeholder.png");
                                            AddModule("State/Province" , "{B70B4B88-51EB-4635-971B-1F676243B810}", lv2menuAddressMaintenance.Id, "StateController"  , "module/state"  , settingsiconbaseurl + "placeholder.png");

            var lv2menuBillingMaintenance = AddLv2ModuleMenu("Billing Maintenance", "{E302CBDC-BA33-4100-8DCF-C2BC174002E9}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                            AddModule("Billing Cycle", "{5736D549-CEA7-4FCF-86DA-0BCD4C87FA04}", lv2menuBillingMaintenance.Id, "BillingCycleController", "module/billingcycle", settingsiconbaseurl + "placeholder.png");

            AddModule("Company Department", "{A6CC5F50-F9DE-4158-B627-6FDC1044C22A}", lv1menuSettings.Id, "DepartmentController", "module/department", settingsiconbaseurl + "placeholder.png");

            var lv2menuContactMaintenance = AddLv2ModuleMenu("Contact Maintenance", "{3FFAC65E-83B5-45DC-87DC-8383C5BD228C}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                            AddModule("Contact Event", "{25ad258e-db9d-4e94-a500-0382e7a2024a}", lv2menuContactMaintenance.Id, "ContactEventController", "module/contactevent", settingsiconbaseurl + "placeholder.png");
                                            AddModule("Contact Title", "{1b9183b2-add9-416d-a5e1-59fe68104e4a}", lv2menuContactMaintenance.Id, "ContactTitleController", "module/contacttitle", settingsiconbaseurl + "placeholder.png");
                                            AddModule("Mail List", "{255ceb68-fb87-4248-ab99-37c18a192300}", lv2menuContactMaintenance.Id, "MailListController", "module/maillist", settingsiconbaseurl + "placeholder.png");

            AddModule("Currency", "{672145d0-9b37-4f6f-a216-9ae1e7728168}", lv1menuSettings.Id, "CurrencyController", "module/currency", settingsiconbaseurl + "placeholder.png");

            var lv2menuCustomerMaintenance = AddLv2ModuleMenu("Customer Maintenance", "{E2D6AE9E-9131-475A-AB42-0F34356760A6}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                             AddModule("Credit Status", "{A28D0CC9-B922-4259-BA4A-A5DE474ADFA4}", lv2menuCustomerMaintenance.Id, "CreditStatusController", "module/creditstatus", settingsiconbaseurl + "placeholder.png");
                                             AddModule("Customer Category", "{8FB6C746-AB6E-4CA5-9BD4-4E9AD88A3BC5}", lv2menuCustomerMaintenance.Id, "CustomerCategoryController", "module/customercategory", settingsiconbaseurl + "placeholder.png");
                                             AddModule("Customer Status", "{B689A0AA-9FCC-450B-AF0F-AD85483531FA}", lv2menuCustomerMaintenance.Id, "CustomerStatusController", "module/customerstatus", settingsiconbaseurl + "placeholder.png");
                                             AddModule("Customer Type"  , "{314EDC6F-478A-40E2-B17E-349886A85EA0}", lv2menuCustomerMaintenance.Id, "CustomerTypeController"  , "module/customertype"  , settingsiconbaseurl + "placeholder.png");

            var lv2menuDealMaintenance = AddLv2ModuleMenu("Deal Maintenance", "{C78B1F90-46B2-4FA1-AC35-139A8B5473FD}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                         AddModule("Deal Classification", "{D1035FCC-D92B-4A3A-B985-C7E02CBE3DFD}", lv2menuDealMaintenance.Id, "DealClassificationController", "module/dealclassification", settingsiconbaseurl + "placeholder.png");
                                         AddModule("Deal Type", "{A021AE67-0F33-4C97-9149-4CD5560EE10A}", lv2menuDealMaintenance.Id, "DealTypeController", "module/dealtype", settingsiconbaseurl + "placeholder.png");
                                         AddModule("Deal Status", "{543F8F83-20AB-4001-8283-1E73A9D795DF}", lv2menuDealMaintenance.Id, "DealStatusController", "module/dealstatus", settingsiconbaseurl + "placeholder.png");
                                         AddModule("Production Type", "{993EBF0C-EDF0-47A2-8507-51492502088B}", lv2menuDealMaintenance.Id, "ProductionTypeController", "module/productiontype", settingsiconbaseurl + "placeholder.png");
                                         AddModule("Schedule Type", "{8646d7bb-9676-4fdd-b9ea-db98045390f4}", lv2menuDealMaintenance.Id, "ScheduleTypeController", "module/scheduletype", settingsiconbaseurl + "placeholder.png");

            var lv2menuDocumentMaintenance = AddLv2ModuleMenu("Document Maintenance", "{CE6E0F99-8A5E-4359-B1A1-ECBDCCC43659}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                             AddModule("Document Type", "{358fbe63-83a7-4ab4-973b-1a5520573674}", lv2menuDocumentMaintenance.Id, "DocumentTypeController", "module/documenttype", settingsiconbaseurl + "placeholder.png");
                                             AddModule("Cover Letter", "{BE13DA09-E3AA-4520-A16C-F43F1A207EA5}", lv2menuDocumentMaintenance.Id, "CoverLetterController", "module/coverletter", settingsiconbaseurl + "placeholder.png");
                                             AddModule("Terms & Conditions", "{5C09A4C3-4272-458A-80DA-A5DF6B098D02}", lv2menuDocumentMaintenance.Id, "TermsConditionsController", "module/termsconditions", settingsiconbaseurl + "placeholder.png");

            var lv2menuEventMaintenance = AddLv2ModuleMenu("Event Maintenance", "{AC96C86F-C229-4E35-8978-859BDAC5865B}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                          AddModule("Event Category", "{3912b3cc-b35f-434d-aeeb-c45fed537e29}", lv2menuEventMaintenance.Id, "EventCategoryController", "module/eventcategory", settingsiconbaseurl + "placeholder.png");
                                          AddModule("Personnel Type", "{46339c9c-c663-4041-aeb4-a7f85783996f}", lv2menuEventMaintenance.Id, "PersonnelTypeController", "module/personneltype", settingsiconbaseurl + "placeholder.png");
                                          AddModule("Photography Type", "{66bff7f0-8bca-4d32-bd94-6b5f13623bec}", lv2menuEventMaintenance.Id, "PhotographyTypeController", "module/photographytype", settingsiconbaseurl + "placeholder.png");

            var lv2menuFacilitiesMaintenance = AddLv2ModuleMenu("Facilities Maintenance", "{CEDB17C6-BD90-4469-B104-B9492A5C4E96}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                               AddModule("Facility Schedule Status", "{A693C2F7-DF16-4492-9DE5-FC672375C44E}", lv2menuFacilitiesMaintenance.Id, "FacilityScheduleStatusController", "module/facilityschedulestatus", settingsiconbaseurl + "placeholder.png");
                                                AddModule("Facility Status", "{DB2C8448-9287-4885-952F-BE3D0E4BFEF1}", lv2menuFacilitiesMaintenance.Id, "FacilityStatusController", "module/facilitystatus", settingsiconbaseurl + "placeholder.png");

            var lv2menuInventoryMaintenance = AddLv2ModuleMenu("Inventory Maintenance", "{A3FB2C11-082B-4602-B189-54B4B1B3E510}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                              AddModule("Inventory Adjustment Reason", "{B3156707-4D41-481C-A66E-8951E5233CDA}", lv2menuInventoryMaintenance.Id, "InventoryAdjustmentReasonController", "module/inventoryadjustmentreason", settingsiconbaseurl + "placeholder.png");
                                              AddModule("Inventory Attribute", "{2777dd37-daca-47ff-aa44-29677b302745}", lv2menuInventoryMaintenance.Id, "InventoryAttributeController", "module/inventoryattribute", settingsiconbaseurl + "placeholder.png");
                                              AddModule("Rental Status", "{E8E24D94-A07D-4388-9F2F-58FE028F24BB}", lv2menuInventoryMaintenance.Id, "RentalStatusController", "module/rentalstatus", settingsiconbaseurl + "placeholder.png");
                                              AddModule("Retired Reason", "{1DE1DD87-47FD-4079-B7D8-B5DE61FCB280}", lv2menuInventoryMaintenance.Id, "RetiredReasonController", "module/retiredreason", settingsiconbaseurl + "placeholder.png");
                                              AddModule("Unit of Measure", "{EE9F1081-BD9F-4004-A0CA-3813E2360642}", lv2menuInventoryMaintenance.Id, "UnitController", "module/unit", settingsiconbaseurl + "placeholder.png");
                                              AddModule("Unretired Reason", "{C8E7F77B-52BC-435C-9971-331CF99284A0}", lv2menuInventoryMaintenance.Id, "UnretiredReasonController", "module/unretiredreason", settingsiconbaseurl + "placeholder.png");
                                              //var lv3menuInventoryMaintenance = AddLv3ModuleMenu("Wardrobe Maintenance", "{5FF779B3-8F3D-4DC5-9C1A-176F3FA48154}", lv2menuInventoryMaintenance.Id, settingsiconbaseurl + "placeholder.png");
                                              //                                  AddModule("Wardrobe Care", "{BE6E4F7C-5D81-4437-A343-8F4933DD6545}", lv3menuInventoryMaintenance.Id, "WardrobeCareController", "module/wardrobecare", settingsiconbaseurl + "placeholder.png");
                                              //                                  AddModule("Wardrobe Color", "{32238B26-3635-4637-AFE0-0D5B12CAAEE4}", lv3menuInventoryMaintenance.Id, "WardrobeColorController", "module/wardrobecolor", settingsiconbaseurl + "placeholder.png");
                                              //                                  AddModule("Wardrobe Gender", "{28574D17-D2FF-41A0-8117-5F252013E7B1}", lv3menuInventoryMaintenance.Id, "WardrobeGenderController", "module/wardrobegender", settingsiconbaseurl + "placeholder.png");
                                              //                                  AddModule("Wardrobe Label", "{9C1B5157-C983-44EE-817F-171B4448401A}", lv3menuInventoryMaintenance.Id, "WardrobeLabelController", "module/wardrobelabel", settingsiconbaseurl + "placeholder.png");

            var lv2menuLaborMaintenance = AddLv2ModuleMenu("Labor Maintenance", "{EE5CF882-B484-41C9-AE82-53D6AFFB3F25}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                              AddModule("Crew Schedule Status", "{E4E11656-0783-4327-A374-161BCFDF0F24}", lv2menuLaborMaintenance.Id, "CrewScheduleStatusController", "module/crewschedulestatus", settingsiconbaseurl + "placeholder.png");
            

            AddModule("Office Location", "{8A8EE5CC-458E-4E4B-BA09-9C514588D3BD}", lv1menuSettings.Id, "OfficeLocationController", "module/officelocation", settingsiconbaseurl + "placeholder.png");

            var lv2menuOrderMaintenance = AddLv2ModuleMenu("Order Maintenance", "{3D7A8032-9D56-4C89-BB53-E25799BE91BE}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                          AddModule("Discount Reason", "{CBBBFA51-DE2D-4A24-A50E-F7F4774016F6}", lv2menuOrderMaintenance.Id, "DiscountReasonController", "module/discountreason", settingsiconbaseurl + "placeholder.png");

            var lv2menuPaymentMaintenance = AddLv2ModuleMenu("Payment Maintenance", "{031F8E86-A1A6-482F-AB4F-8DD015AB7642}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                            AddModule("Payment Terms", "{44FD799A-1572-4B34-9943-D94FFBEF89D4}", lv2menuPaymentMaintenance.Id, "PaymentTermsController", "module/paymentterms", settingsiconbaseurl + "placeholder.png");
                                            AddModule("Payment Type", "{E88C4957-3A3E-4258-8677-EB6FB61F9BA3}", lv2menuPaymentMaintenance.Id, "PaymentTypeController", "module/paymenttype", settingsiconbaseurl + "placeholder.png");

            var lv2menuPOMaintenance = AddLv2ModuleMenu("PO Maintenance", "{55EDE544-A603-467D-AFA2-EC9C2A650810}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                       AddModule("PO Classification", "{58ef51c5-a97b-43c6-9298-08b064a84a48}", lv2menuPOMaintenance.Id, "POClassificationController", "module/poclassification", settingsiconbaseurl + "placeholder.png");
                                       AddModule("PO Importance", "{82BF3B3E-0EF8-4A6E-8577-33F23EA9C4FB}", lv2menuPOMaintenance.Id, "POImportanceController", "module/poimportance", settingsiconbaseurl + "placeholder.png");

            AddModule("Region", "{A50C7F59-AF91-44D5-8253-5C4A4D5DFB8B}", lv1menuSettings.Id, "RegionController", "module/region", settingsiconbaseurl + "placeholder.png");

            var lv2menuVehicleMaintenance = AddLv2ModuleMenu("Vehicle Maintenance", "{6081E168-E3BF-439E-82B0-34AF3680C444}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                            AddModule("Vehicle Schedule Status", "{A001473B-1FB4-4E85-8093-37A92057CD93}", lv2menuVehicleMaintenance.Id, "VehicleScheduleStatusController", "module/vehicleschedulestatus", settingsiconbaseurl + "placeholder.png");
                                            AddModule("Vehicle Color", "{F7A34B70-509A-422F-BFD1-5F30BE2C8186}", lv2menuVehicleMaintenance.Id, "VehicleColorController", "module/vehiclecolor", settingsiconbaseurl + "placeholder.png");
                                            AddModule("Vehicle Status", "{FB12061D-E6AF-4C09-95A0-8647930C289A}", lv2menuVehicleMaintenance.Id, "VehicleStatusController", "module/vehiclestatus", settingsiconbaseurl + "placeholder.png");

            var lv2menuVendorMaintenance = AddLv2ModuleMenu("Vendor Maintenance", "{93376B75-2771-474A-8C25-2BBE53B50F5C}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                           AddModule("Vendor Class", "{8B2C9EE3-AE87-483F-A651-8BA633E6C439}", lv2menuVendorMaintenance.Id, "VendorClassController", "module/vendorclass", settingsiconbaseurl + "placeholder.png");
                                           AddModule("Organization Type", "{fe3a764c-ab55-4ce5-8d7f-bfc86f174c11}", lv2menuVendorMaintenance.Id, "OrganizationTypeController", "module/organizationtype", settingsiconbaseurl + "placeholder.png");


            AddModule("Warehouse", "{931D3E75-68CB-4280-B12F-9A955444AA0C}", lv1menuSettings.Id, "WarehouseController", "module/warehouse", settingsiconbaseurl + "placeholder.png");

            var lv2menuHolidayMaintenance = AddLv2ModuleMenu("Holiday Maintenance", "{8A1C54ED-01B6-4EF5-AEBD-5E3F9F2563E0}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                            AddModule("Blackout Status", "{43D7C88D-8D8C-424E-94D3-A2C537F0C76E}", lv2menuHolidayMaintenance.Id, "BlackoutStatusController", "module/blackoutstatus", settingsiconbaseurl + "placeholder.png");

            var lv2menuUserMaintenance = AddLv2ModuleMenu("User Maintenance", "{13E1A9A9-1096-447E-B4AE-E538BEF5BCB5}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                            AddModule("User Status", "{E19916C6-A844-4BD1-A338-FAB0F278122C}", lv2menuUserMaintenance.Id, "UserStatusController", "module/userstatus", settingsiconbaseurl + "placeholder.png");


            var lv2menuCrewMaintenance = AddLv2ModuleMenu("Crew Maintenance", "{D2453E27-FB2D-4B75-81CD-6966AD652ECE}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                            AddModule("Crew Status", "{73A6D9E3-E3BE-4B7A-BB3B-0AFE571C944E}", lv2menuCrewMaintenance.Id, "CrewStatusController", "module/crewstatus", settingsiconbaseurl + "placeholder.png");


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

            // Add Submodules
            AddSubModule("User Settings", "{A6704904-01E1-4C6B-B75A-C1D3FCB50C01}", lv1menuSubModules.Id, "UserSettingsController");

            // Add Grids
            AddGrid("Audit History",                  "{FA958D9E-7863-4B03-94FE-A2D2B9599FAB}", lv1menuGrids.Id, "FwAuditHistoryGridController");
            AddGrid("Contact",                        "{B6A0CAFC-35E8-4490-AEED-29F4E3426758}", lv1menuGrids.Id, "RwContactGridController");
            AddGrid("Contact Company",                "{7E1840AE-9832-4E0E-9B1F-A2A115575852}", lv1menuGrids.Id, "FwContactCompanyGridController");
            AddGrid("Contact Document",               "{CC8F52FF-D968-4CE6-BF7A-3AC859D25280}", lv1menuGrids.Id, "FwContactDocumentGridController");
            AddGrid("Contact Email History",          "{DAA5E81D-353C-4AAA-88A8-B4E7046B5FF0}", lv1menuGrids.Id, "FwContactEmailHistoryGridController");
            AddGrid("Contact Note",                   "{A9CB5D4D-4AC0-46D4-A084-19039CF8C654}", lv1menuGrids.Id, "FwContactNoteGridController");
            AddGrid("Contact Personal Event",         "{C40394BA-E805-4A49-A4D0-938B2A84D9A7}", lv1menuGrids.Id, "FwContactPersonalEventGridController");
            AddGrid("Document Version ",              "{397FF02A-BF19-4C1F-8E5F-9DBE786D77EC}", lv1menuGrids.Id, "FwAppDocumentVersionGridController");
            AddGrid("Master Item",                    "{F21525ED-EDAC-4627-8791-0B410C74DAAE}", lv1menuGrids.Id, "RwMasterItemGridController");
            AddGrid("Order Activity Dates",           "{E00980E5-7A1C-4438-AB06-E8B7072A7595}", lv1menuGrids.Id, "RwOrderActivityDatesGridController");
            AddGrid("Order Contract Note",            "{2018FEB8-D15D-4F1C-B09D-9BCBD5491B52}", lv1menuGrids.Id, "RwOrderContractNoteGridController");
            AddGrid("Order Dates",                    "{D4B28F52-5C9D-4D8C-B58C-42924428DE93}", lv1menuGrids.Id, "RwOrderDatesGridController");
            AddGrid("Order Note",                     "{45573B9C-B39D-4975-BC36-4A41362E1AF0}", lv1menuGrids.Id, "RwOrderNoteGridController");
            AddGrid("Quik Entry Accessories Options", "{27317105-BA68-417A-A592-86EEB977CA32}", lv1menuGrids.Id, "RwQuikEntryAccessoriesOptionsGridController");
            AddGrid("Quik Entry Category",            "{01604AEC-2127-4756-BF92-3A340EF000E1}", lv1menuGrids.Id, "RwQuikEntryCategoryGridController");
            AddGrid("Quik Entry Department",          "{2AC10F3D-FC50-4454-87C2-54ABBCCD08AB}", lv1menuGrids.Id, "RwQuikEntryDepartmentGridController");
            AddGrid("Quik Entry Items",               "{1289FF25-5C86-43CC-8557-173E7EA69696}", lv1menuGrids.Id, "RwQuikEntryItemsGridController");
            AddGrid("Quik Entry Sub Category",        "{26576DCB-4141-477A-9A3D-4F76D862C581}", lv1menuGrids.Id, "RwQuikEntrySubCategoryGridController");

            Type[] fwTypes  = typeof(FwApplicationTree).Assembly.GetTypes();
            Type[] appTypes = typeof(SecurityTree).Assembly.GetTypes();
            foreach (Type type in fwTypes)
            {
                bool isInModulesFolder, isFwApplicationTreeBranch;
                isInModulesFolder         = type.FullName.StartsWith("Fw.Json.Content.Source.Modules");
                isFwApplicationTreeBranch = type.IsSubclassOf(typeof(Fw.Json.ValueTypes.FwApplicationTreeBranch));
                if (isInModulesFolder && isFwApplicationTreeBranch)
                {
                    FwApplicationTreeBranch branch = (FwApplicationTreeBranch)Activator.CreateInstance(type);
                    branch.BuildBranch(this);
                }
            }
            foreach (Type type in appTypes)
            {
                bool isInModulesFolder, isFwApplicationTreeBranch;
                isInModulesFolder         = type.FullName.StartsWith("RentalWorksWeb.Source.Modules");
                isFwApplicationTreeBranch = type.IsSubclassOf(typeof(Fw.Json.ValueTypes.FwApplicationTreeBranch));
                if (isInModulesFolder && isFwApplicationTreeBranch)
                {
                    FwApplicationTreeBranch branch = (FwApplicationTreeBranch)Activator.CreateInstance(type);
                    branch.BuildBranch(this);
                }
            }
        }
        //---------------------------------------------------------------------------------------------
        private void BuildRentalWorksQuikScanTree()
        {
            var iconbaseurl = "theme/images/icons/128/";
            AddApplication("RentalWorks QuikScan", "{8D0A5ECF-72D2-4428-BDC8-7E3CC56EDD3A}", "{4AC8B3C9-A2C2-4085-8F7F-EE005CCEB535}");
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