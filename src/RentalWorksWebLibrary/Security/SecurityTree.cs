using FwStandard.Models;
using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebLibrary.Security
{
    public class SecurityTree : FwSecurityTree
    {
        public enum SystemNames { RentalWorks, TrakitWorks }
        //---------------------------------------------------------------------------------------------
        public SecurityTree(SqlServerConfig sqlServerConfig, string currentApplicationId) : base(sqlServerConfig, currentApplicationId)
        {
            var system = AddSystem("Applications", "{4AC8B3C9-A2C2-4085-8F7F-EE005CCEB535}");
            BuildRentalWorksWebTree(system);
            BuildTrakitWorksWebTree(system);
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
            string homeiconbaseurl          = "theme/images/icons/home/";
            string settingsiconbaseurl      = "theme/images/icons/settings/";
            string reportsiconbaseurl       = "theme/images/icons/reports/";
            string utilitiesiconbaseurl     = "theme/images/icons/utilities/";
            string administratoriconbaseurl = "theme/images/icons/administrator/";

            var application = AddApplication("RentalWorks Web", "{0A5F2584-D239-480F-8312-7C2B552A30BA}", system.Id);
            var lv1menuAgent         = AddLv1ModuleMenu("Agent",           "{91D2F0CF-2063-4EC8-B38D-454297E136A8}", application.Id);
            var lv1menuInventory     = AddLv1ModuleMenu("Inventory",       "{8AA0C4A4-B583-44CD-BB47-09C43961CE99}", application.Id);
            var lv1menuWarehouse     = AddLv1ModuleMenu("Warehouse",       "{22D67715-9C24-4A06-A009-CB10A1EC746B}", application.Id);
            var lv1menuBilling       = AddLv1ModuleMenu("Billing",         "{9BC99BDA-4C94-4D7D-8C22-31CA5205B1AA}", application.Id);
            var lv1menuReports       = AddLv1ReportsMenu("Reports",        "{7FEC9D55-336E-44FE-AE01-96BF7B74074C}", application.Id);
            var lv1menuUtilities     = AddLv1ModuleMenu("Utilities",       "{81609B0E-4B1F-4C13-8BE0-C1948557B82D}", application.Id);
            var lv1menuSettings      = AddLvl1SettingsMenu("Settings",     "{730C9659-B33B-493E-8280-76A060A07DCE}", application.Id);
            var lv1menuAdministrator = AddLv1ModuleMenu("Administrator",   "{F188CB01-F627-4DD3-9B91-B6486F0977DC}", application.Id);
            var lv1menuSubModules    = AddLv1SubModulesMenu("Sub-Modules", "{B8E34B04-EB99-4068-AD9E-BDC32D02967A}", application.Id);
            var lv1menuGrids         = AddLv1GridsMenu("Grids",            "{43765919-4291-49DD-BE76-F69AA12B13E8}", application.Id);

            //RentalWorks
            AddModule("Quote",            "{4D785844-BE8A-4C00-B1FA-2AA5B05183E5}", lv1menuAgent.Id, "QuoteController",           "module/quote",           homeiconbaseurl + "placeholder.png");
            AddModule("Order",            "{64C46F51-5E00-48FA-94B6-FC4EF53FEA20}", lv1menuAgent.Id, "OrderController",           "module/order",           homeiconbaseurl + "placeholder.png");
            AddModule("Customer",         "{214C6242-AA91-4498-A4CC-E0F3DCCCE71E}", lv1menuAgent.Id, "CustomerController",        "module/customer",        homeiconbaseurl + "placeholder.png");
            AddModule("Deal",             "{C67AD425-5273-4F80-A452-146B2008B41C}", lv1menuAgent.Id, "DealController",            "module/deal",            homeiconbaseurl + "placeholder.png");
            AddModule("Vendor",           "{AE4884F4-CB21-4D10-A0B5-306BD0883F19}", lv1menuAgent.Id, "VendorController",          "module/vendor",          homeiconbaseurl + "placeholder.png");
            AddModule("Contact",          "{3F803517-618A-41C0-9F0B-2C96B8BDAFC4}", lv1menuAgent.Id, "ContactController",         "module/contact",         homeiconbaseurl + "placeholder.png");
            AddModule("Purchase Order",   "{67D8C8BB-CF55-4231-B4A2-BB308ADF18F0}", lv1menuAgent.Id, "PurchaseOrderController",   "module/purchaseorder",   homeiconbaseurl + "placeholder.png");
            AddModule("Project",          "{C6C8167A-C3B5-4915-8290-4520AF7EDB35}", lv1menuAgent.Id, "ProjectController",         "module/project",         homeiconbaseurl + "placeholder.png");

            AddModule("Rental Inventory",   "{FCDB4C86-20E7-489B-A8B7-D22EE6F85C06}", lv1menuInventory.Id, "RentalInventoryController",   "module/rentalinventory",   homeiconbaseurl + "placeholder.png");
            AddModule("Sales Inventory",    "{B0CF2E66-CDF8-4E58-8006-49CA68AE38C2}", lv1menuInventory.Id, "SalesInventoryController",    "module/salesinventory",    homeiconbaseurl + "placeholder.png");
            AddModule("Parts Inventory",    "{351B8A09-7778-4F06-A6A2-ED0920A5C360}", lv1menuInventory.Id, "PartsInventoryController",    "module/partsinventory",    homeiconbaseurl + "placeholder.png");
            AddModule("Asset",              "{1C45299E-F8DB-4AE4-966F-BE142295E3D6}", lv1menuInventory.Id, "AssetController",             "module/item",              homeiconbaseurl + "placeholder.png");
            AddModule("Container",          "{28A49328-FFBD-42D5-A492-EDF540DF7011}", lv1menuInventory.Id, "ContainerController",         "module/container",         homeiconbaseurl + "placeholder.png");
            AddModule("Repair Order",       "{2BD0DC82-270E-4B86-A9AA-DD0461A0186A}", lv1menuInventory.Id, "RepairController",            "module/repair",            homeiconbaseurl + "placeholder.png");
            AddModule("Complete Qc",        "{3F20813A-CC21-49D8-A5F8-9930B7F05404}", lv1menuInventory.Id, "CompleteQcController",        "module/completeqc",        homeiconbaseurl + "placeholder.png");
            AddModule("Physical Inventory", "{BABFE80E-8A52-49D4-81D9-6B6EBB518E89}", lv1menuInventory.Id, "PhysicalInventoryController", "module/physicalinventory", homeiconbaseurl + "placeholder.png");

            AddModule("Order Status",        "{F6AE5BC1-865D-467B-A201-95C93F8E8D0B}", lv1menuWarehouse.Id, "OrderStatusController",        "module/orderstatus",       homeiconbaseurl + "placeholder.png");
            AddModule("Pick List",           "{7B04E5D4-D079-4F3A-9CB0-844F293569ED}", lv1menuWarehouse.Id, "PickListController",           "module/picklist",          homeiconbaseurl + "placeholder.png");
            AddModule("Contract",            "{6BBB8A0A-53FA-4E1D-89B3-8B184B233DEA}", lv1menuWarehouse.Id, "ContractController",           "module/contract",          homeiconbaseurl + "placeholder.png");
            AddModule("Staging / Check-Out", "{C3B5EEC9-3654-4660-AD28-20DE8FF9044D}", lv1menuWarehouse.Id, "StagingCheckoutController",    "module/checkout",          homeiconbaseurl + "placeholder.png");
            AddModule("Exchange",            "{2AEDB175-7998-48BC-B2C4-D4794BF65342}", lv1menuWarehouse.Id, "ExchangeController",           "module/exchange",          homeiconbaseurl + "placeholder.png");
            AddModule("Check-In",            "{77317E53-25A2-4C12-8DAD-7541F9A09436}", lv1menuWarehouse.Id, "CheckInController",            "module/checkin",           homeiconbaseurl + "placeholder.png");
            AddModule("Receive From Vendor", "{00539824-6489-4377-A291-EBFE26325FAD}", lv1menuWarehouse.Id, "ReceiveFromVendorController",  "module/receivefromvendor", homeiconbaseurl + "placeholder.png");
            AddModule("Return To Vendor",    "{D54EAA01-A710-4F78-A1EE-5FC9EE9150D8}", lv1menuWarehouse.Id, "ReturnToVendorController",     "module/returntovendor",    homeiconbaseurl + "placeholder.png");
            AddModule("Assign Bar Codes",    "{4B9C17DE-7FC0-4C33-B953-26FC90F32EA0}", lv1menuWarehouse.Id, "AssignBarCodesController",     "module/assignbarcodes",    homeiconbaseurl + "placeholder.png");
            AddModule("Transfer Status",     "{58D5D354-136E-40D5-9675-B74FD7807D6F}", lv1menuWarehouse.Id, "TransferStatusController",     "module/transferstatus",    homeiconbaseurl + "placeholder.png");
            AddModule("Transfer Order",      "{F089C9A9-554D-40BF-B1FA-015FEDE43591}", lv1menuWarehouse.Id, "TransferOrderController",      "module/transferorder",     homeiconbaseurl + "placeholder.png");
            AddModule("Manifest",            "{1643B4CE-D368-4D64-8C05-6EF7C7D80336}", lv1menuWarehouse.Id, "ManifestController",           "module/manifest",          homeiconbaseurl + "placeholder.png");
            AddModule("Transfer Receipt",    "{2B60012B-ED6A-430B-B2CB-C1287FD4CE8B}", lv1menuWarehouse.Id, "TransferReceiptController",    "module/transferreceipt",   homeiconbaseurl + "placeholder.png");
            AddModule("Transfer Out",        "{91E79272-C1CF-4678-A28F-B716907D060C}", lv1menuWarehouse.Id, "TransferOutController",        "module/transferout",       homeiconbaseurl + "placeholder.png");
            AddModule("Transfer In",         "{D9F487C2-5DC1-45DF-88A2-42A05679376C}", lv1menuWarehouse.Id, "TransferInController",         "module/transferin",        homeiconbaseurl + "placeholder.png");
            AddModule("Fill Container",      "{0F1050FB-48DF-41D7-A969-37300B81B7B5}", lv1menuWarehouse.Id, "FillContainerController",      "module/fillcontainer",     homeiconbaseurl + "placeholder.png");

            // Billing
            AddModule("Billing",             "{34E0472E-9057-4C66-8CC2-1938B3222569}", lv1menuBilling.Id, "BillingController",       "module/billing",       homeiconbaseurl + "placeholder.png");
            AddModule("Invoice",             "{9B79D7D8-08A1-4F6B-AC0A-028DFA9FE10F}", lv1menuBilling.Id, "InvoiceController",       "module/invoice",       homeiconbaseurl + "placeholder.png");
            AddModule("Receipts",            "{57E34535-1B9F-4223-AD82-981CA34A6DEC}", lv1menuBilling.Id, "ReceiptController",       "module/receipt",       homeiconbaseurl + "placeholder.png");
            AddModule("Vendor Invoice",      "{854B3C59-7040-47C4-A8A3-8A336FC970FE}", lv1menuBilling.Id, "VendorInvoiceController", "module/vendorinvoice", homeiconbaseurl + "placeholder.png");

            AddModule("Create Pick List",        "{5013C4FF-FC42-4EFE-AE9D-AAF6857F17B8}", null, "CreatePickListController", "module/createpicklist", homeiconbaseurl + "placeholder.png");
            AddModule("Suspended Session",       "{5FBE7FF8-3770-48C5-855C-4320C961D95A}", null, "SuspendedSessionController", "module/suspendedsession", homeiconbaseurl + "placeholder.png");

            //Settings 
            var lv2menuAccountingSettings   = AddSettingsMenu("Accounting Settings",         "{BAF9A442-BA44-4DD1-9119-905C1A8FF199}", lv1menuSettings.Id,                                                                                        settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Chart of Accounts",           "{F03CA227-99EE-42EF-B615-94540DCB21B3}", lv2menuAccountingSettings.Id,   "GlAccountController",                 "module/glaccount",                 settingsiconbaseurl + "placeholder.png", description: "Asset, Income, Liability, and Expense Accounts for tracking revenue and expenses.");
                                                     AddSettingsModule("G/L Distribution",            "{7C249F59-B5E3-4DAE-933D-38D30858CF7C}", lv2menuAccountingSettings.Id,   "GlDistributionController",            "module/gldistribution",            settingsiconbaseurl + "placeholder.png", description: "Accounts to use for Accounts Receivable, Receipts, Payables, etc.");
            var lv2menuAddressSettings      = AddSettingsMenu("Address Settings",            "{2ABD806F-D059-4CCC-87C0-C4AE01B46EBC}", lv1menuSettings.Id,                                                                                        settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Country",                     "{D6E787E6-502B-4D36-B0A6-FA691E6D10CF}", lv2menuAddressSettings.Id,      "CountryController",                   "module/country",                   settingsiconbaseurl + "placeholder.png", description: "List Countries to relate to your Customers, Deals, and Vendors");
                                                     AddSettingsModule("State/Province",              "{B70B4B88-51EB-4635-971B-1F676243B810}", lv2menuAddressSettings.Id,      "StateController",                     "module/state",                     settingsiconbaseurl + "placeholder.png", description: "List States to relate to your Customers, Deals, and Vendors");
                                                     AddSettingsModule("Billing Cycle",               "{5736D549-CEA7-4FCF-86DA-0BCD4C87FA04}", lv1menuSettings.Id,             "BillingCycleController",              "module/billingcycle",              settingsiconbaseurl + "placeholder.png", description: "Define and configure Billing Cycles for your Quotes and Orders");
                                                     AddSettingsModule("Company Department",          "{A6CC5F50-F9DE-4158-B627-6FDC1044C22A}", lv1menuSettings.Id,             "DepartmentController",                "module/department",                settingsiconbaseurl + "placeholder.png");
            var lv2menuContactSettings      = AddSettingsMenu("Contact Settings",            "{3FFAC65E-83B5-45DC-87DC-8383C5BD228C}", lv1menuSettings.Id,                                                                                        settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Contact Event",               "{25ad258e-db9d-4e94-a500-0382e7a2024a}", lv2menuContactSettings.Id,      "ContactEventController",              "module/contactevent",              settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Contact Title",               "{1b9183b2-add9-416d-a5e1-59fe68104e4a}", lv2menuContactSettings.Id,      "ContactTitleController",              "module/contacttitle",              settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Mail List",                   "{255ceb68-fb87-4248-ab99-37c18a192300}", lv2menuContactSettings.Id,      "MailListController",                  "module/maillist",                  settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Currency",                    "{672145d0-9b37-4f6f-a216-9ae1e7728168}", lv1menuSettings.Id,             "CurrencyController",                  "module/currency",                  settingsiconbaseurl + "placeholder.png");
            var lv2menuCustomerSettings     = AddSettingsMenu("Customer Settings",           "{E2D6AE9E-9131-475A-AB42-0F34356760A6}", lv1menuSettings.Id,                                                                                        settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Credit Status",               "{A28D0CC9-B922-4259-BA4A-A5DE474ADFA4}", lv2menuCustomerSettings.Id,     "CreditStatusController",              "module/creditstatus",              settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Customer Category",           "{8FB6C746-AB6E-4CA5-9BD4-4E9AD88A3BC5}", lv2menuCustomerSettings.Id,     "CustomerCategoryController",          "module/customercategory",          settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Customer Status",             "{B689A0AA-9FCC-450B-AF0F-AD85483531FA}", lv2menuCustomerSettings.Id,     "CustomerStatusController",            "module/customerstatus",            settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Customer Type",               "{314EDC6F-478A-40E2-B17E-349886A85EA0}", lv2menuCustomerSettings.Id,     "CustomerTypeController",              "module/customertype",              settingsiconbaseurl + "placeholder.png");
            var lv2menuDealSettings         = AddSettingsMenu("Deal Settings",               "{C78B1F90-46B2-4FA1-AC35-139A8B5473FD}", lv1menuSettings.Id,                                                                                        settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Deal Classification",         "{D1035FCC-D92B-4A3A-B985-C7E02CBE3DFD}", lv2menuDealSettings.Id,         "DealClassificationController",        "module/dealclassification",        settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Deal Type",                   "{A021AE67-0F33-4C97-9149-4CD5560EE10A}", lv2menuDealSettings.Id,         "DealTypeController",                  "module/dealtype",                  settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Deal Status",                 "{543F8F83-20AB-4001-8283-1E73A9D795DF}", lv2menuDealSettings.Id,         "DealStatusController",                "module/dealstatus",                settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Production Type",             "{993EBF0C-EDF0-47A2-8507-51492502088B}", lv2menuDealSettings.Id,         "ProductionTypeController",            "module/productiontype",            settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Schedule Type",               "{8646d7bb-9676-4fdd-b9ea-db98045390f4}", lv2menuDealSettings.Id,         "ScheduleTypeController",              "module/scheduletype",              settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Discount Template",           "{258D920E-7024-4F68-BF1F-F07F3613829C}", lv1menuSettings.Id,             "DiscountTemplateController",          "module/discounttemplate",          settingsiconbaseurl + "placeholder.png");
            var lv2menuDocumentSettings     = AddSettingsMenu("Document Settings",           "{CE6E0F99-8A5E-4359-B1A1-ECBDCCC43659}", lv1menuSettings.Id,                                                                                        settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Document Type",               "{358fbe63-83a7-4ab4-973b-1a5520573674}", lv2menuDocumentSettings.Id,     "DocumentTypeController",              "module/documenttype",              settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Cover Letter",                "{BE13DA09-E3AA-4520-A16C-F43F1A207EA5}", lv2menuDocumentSettings.Id,     "CoverLetterController",               "module/coverletter",               settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Terms & Conditions",          "{5C09A4C3-4272-458A-80DA-A5DF6B098D02}", lv2menuDocumentSettings.Id,     "TermsConditionsController",           "module/termsconditions",           settingsiconbaseurl + "placeholder.png");
            var lv2menuEventSettings        = AddSettingsMenu("Event Settings",              "{AC96C86F-C229-4E35-8978-859BDAC5865B}", lv1menuSettings.Id,                                                                                        settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Event Category",              "{3912b3cc-b35f-434d-aeeb-c45fed537e29}", lv2menuEventSettings.Id,        "EventCategoryController",             "module/eventcategory",             settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Event Type",                  "{FE501F99-95D4-444C-A7B6-EA20ACE88879}", lv2menuEventSettings.Id,        "EventTypeController",                 "module/eventtype",                 settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Personnel Type",              "{46339c9c-c663-4041-aeb4-a7f85783996f}", lv2menuEventSettings.Id,        "PersonnelTypeController",             "module/personneltype",             settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Photography Type",            "{66bff7f0-8bca-4d32-bd94-6b5f13623bec}", lv2menuEventSettings.Id,        "PhotographyTypeController",           "module/photographytype",           settingsiconbaseurl + "placeholder.png");
            var lv2menuFacilitiesSettings   = AddSettingsMenu("Facilities Settings",         "{CEDB17C6-BD90-4469-B104-B9492A5C4E96}", lv1menuSettings.Id,                                                                                        settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Building",                    "{2D344845-7E77-40C9-BB9D-04A930D352EB}", lv2menuFacilitiesSettings.Id,   "BuildingController",                  "module/building",                  settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Facility Type",               "{197BBE51-28A8-4D00-BD0C-098C0F88DD0E}", lv2menuFacilitiesSettings.Id,   "FacilityTypeController",              "module/facilitytype",              settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Facility Rate",               "{5D49FC0B-F1BA-4FE4-889D-3C52B6202ACD}", lv2menuFacilitiesSettings.Id,   "FacilityRateController",              "module/facilityrate",              settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Facility Schedule Status",    "{A693C2F7-DF16-4492-9DE5-FC672375C44E}", lv2menuFacilitiesSettings.Id,   "FacilityScheduleStatusController",    "module/facilityschedulestatus",    settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Facility Status",             "{DB2C8448-9287-4885-952F-BE3D0E4BFEF1}", lv2menuFacilitiesSettings.Id,   "FacilityStatusController",            "module/facilitystatus",            settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Facility Category",           "{67A9BEC5-4865-409C-9327-B2B8714DDAA8}", lv2menuFacilitiesSettings.Id,   "FacilityCategoryController",          "module/facilitycategory",          settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Space Type / Activity",       "{EDF05CFB-9F6B-4771-88EB-6FD254CFE6C6}", lv2menuFacilitiesSettings.Id,   "SpaceTypeController",                 "module/spacetype",                 settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Fiscal Year",                 "{6F87E62B-F17A-48CB-B673-16D12B6DFFB9}", lv1menuSettings.Id,             "FiscalYearController",                "module/fiscalyear",                settingsiconbaseurl + "placeholder.png");
            var lv2menuGeneratorSettings    = AddSettingsMenu("Generator Settings",          "{711E8D44-E71F-4D10-B704-855E1018D20B}", lv1menuSettings.Id,                                                                                        settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Generator Fuel Type",         "{8A331FE0-B92A-4DD2-8A59-29E4E6D6EA4F}", lv2menuGeneratorSettings.Id,    "GeneratorFuelTypeController",         "module/generatorfueltype",         settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Generator Make",              "{D7C38A54-A198-4304-8EC2-CE8038D3BE9C}", lv2menuGeneratorSettings.Id,    "GeneratorMakeController",             "module/generatormake",             settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Generator Rating",            "{140E6997-1BA9-49B7-AA79-CD5EF6444C72}", lv2menuGeneratorSettings.Id,    "GeneratorRatingController",           "module/generatorrating",           settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Generator Watts",             "{503349D6-711A-4F45-8891-4B3203008441}", lv2menuGeneratorSettings.Id,    "GeneratorWattsController",            "module/generatorwatts",            settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Generator Type",              "{95D9D422-DCEB-4150-BCC2-79573B87AC4D}", lv2menuGeneratorSettings.Id,    "GeneratorTypeController",             "module/generatortype",             settingsiconbaseurl + "placeholder.png");
            var lv2menuHolidaySettings      = AddSettingsMenu("Holiday Settings",            "{8A1C54ED-01B6-4EF5-AEBD-5E3F9F2563E0}", lv1menuSettings.Id,                                                                                        settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Holiday",                     "{CFFEFF09-A083-478E-913C-945184B5DE94}", lv2menuHolidaySettings.Id,      "HolidayController",                   "module/holiday",                   settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Blackout Status",             "{43D7C88D-8D8C-424E-94D3-A2C537F0C76E}", lv2menuHolidaySettings.Id,      "BlackoutStatusController",            "module/blackoutstatus",            settingsiconbaseurl + "placeholder.png");
            var lv2menuInventorySettings    = AddSettingsMenu("Inventory Settings",          "{A3FB2C11-082B-4602-B189-54B4B1B3E510}", lv1menuSettings.Id,                                                                                        settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Bar Code Range",              "{9A52C5B8-98AB-49A0-A392-69DB0873F943}", lv2menuInventorySettings.Id,    "BarCodeRangeController",              "module/barcoderange",              settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Inventory Adjustment Reason", "{B3156707-4D41-481C-A66E-8951E5233CDA}", lv2menuInventorySettings.Id,    "InventoryAdjustmentReasonController", "module/inventoryadjustmentreason", settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Inventory Attribute",         "{2777dd37-daca-47ff-aa44-29677b302745}", lv2menuInventorySettings.Id,    "AttributeController",                 "module/attribute",                 settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Inventory Condition",         "{BF711CAC-1E69-4C92-B509-4CBFA29FAED3}", lv2menuInventorySettings.Id,    "InventoryConditionController",        "module/inventorycondition",        settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Inventory Group",             "{43AF2FBA-69FB-46A8-8E5A-2712486B66F3}", lv2menuInventorySettings.Id,    "InventoryGroupController",            "module/inventorygroup",            settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Inventory Rank",              "{963F5133-29CA-4675-9BE6-E5C47D38789A}", lv2menuInventorySettings.Id,    "InventoryRankController",             "module/inventoryrank",             settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Inventory Status",            "{E8E24D94-A07D-4388-9F2F-58FE028F24BB}", lv2menuInventorySettings.Id,    "InventoryStatusController",           "module/inventorystatus",           settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Inventory Type",              "{D62E0D20-AFF4-46A7-A767-FF32F6EC4617}", lv2menuInventorySettings.Id,    "InventoryTypeController",             "module/inventorytype",             settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Parts Category",              "{4750DFBD-6C60-41EF-83FE-49C8340D6062}", lv2menuInventorySettings.Id,    "PartsCategoryController",             "module/partscategory",             settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Rental Category",             "{91079439-A188-4637-B733-A7EF9A9DFC22}", lv2menuInventorySettings.Id,    "RentalCategoryController",            "module/rentalcategory",            settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Retired Reason",              "{1DE1DD87-47FD-4079-B7D8-B5DE61FCB280}", lv2menuInventorySettings.Id,    "RetiredReasonController",             "module/retiredreason",             settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Sales Category",              "{428619B5-ABDE-48C4-9B2F-CF6D2A3AC574}", lv2menuInventorySettings.Id,    "SalesCategoryController",             "module/salescategory",             settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Unit of Measure",             "{EE9F1081-BD9F-4004-A0CA-3813E2360642}", lv2menuInventorySettings.Id,    "UnitController",                      "module/unit",                      settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Unretired Reason",            "{C8E7F77B-52BC-435C-9971-331CF99284A0}", lv2menuInventorySettings.Id,    "UnretiredReasonController",           "module/unretiredreason",           settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Warehouse Catalog",           "{9045B118-A790-44FB-9867-3E8035EFEE69}", lv2menuInventorySettings.Id,    "WarehouseCatalogController",          "module/warehousecatalog",          settingsiconbaseurl + "placeholder.png");
            var lv2menuLaborSettings        = AddSettingsMenu("Labor Settings",              "{EE5CF882-B484-41C9-AE82-53D6AFFB3F25}", lv1menuSettings.Id,                                                                                        settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Crew",                        "{FF4C0AF2-0984-48FD-A108-68D93CB8FFE6}", lv2menuLaborSettings.Id,        "CrewController",                      "module/crew",                      settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Labor Rate",                  "{650305EC-0A53-490B-A8FB-E1AF636DA89B}", lv2menuLaborSettings.Id,        "LaborRateController",                 "module/laborrate",                 settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Position",                    "{6D3B3D4F-2DD8-436F-8942-8FF68B73F3B6}", lv2menuLaborSettings.Id,        "LaborPositionController",             "module/laborposition",             settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Labor Type",                  "{6757DFC2-360A-450A-B2E8-0B8232E87D6A}", lv2menuLaborSettings.Id,        "LaborTypeController",                 "module/labortype",                 settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Labor Category",              "{2A5190B9-B0E8-4B93-897B-C91FC4807FA6}", lv2menuLaborSettings.Id,        "LaborCategoryController",             "module/laborcategory",             settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Crew Schedule Status",        "{E4E11656-0783-4327-A374-161BCFDF0F24}", lv2menuLaborSettings.Id,        "CrewScheduleStatusController",        "module/crewschedulestatus",        settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Crew Status",                 "{73A6D9E3-E3BE-4B7A-BB3B-0AFE571C944E}", lv2menuLaborSettings.Id,        "CrewStatusController",                "module/crewstatus",                settingsiconbaseurl + "placeholder.png");
            var lv2menuMiscSettings         = AddSettingsMenu("Misc Settings",               "{2ED700C1-2D45-4307-9F92-41281185BD15}", lv1menuSettings.Id,                                                                                        settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Misc Rate",                   "{15B5AA83-4C3A-4136-B74B-574BDC0141B2}", lv2menuMiscSettings.Id,         "MiscRateController",                  "module/miscrate",                  settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Misc Type",                   "{EAFEE5C7-84BB-419E-905A-3AE86E18DFAB}", lv2menuMiscSettings.Id,         "MiscTypeController",                  "module/misctype",                  settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Misc Category",               "{D5318A2F-ECB8-498A-9D9A-0846F4B9E4DF}", lv2menuMiscSettings.Id,         "MiscCategoryController",              "module/misccategory",              settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Office Location",             "{8A8EE5CC-458E-4E4B-BA09-9C514588D3BD}", lv1menuSettings.Id,             "OfficeLocationController",            "module/officelocation",            settingsiconbaseurl + "placeholder.png");
            var lv2menuOrderSettings        = AddSettingsMenu("Order Settings",              "{3D7A8032-9D56-4C89-BB53-E25799BE91BE}", lv1menuSettings.Id,                                                                                        settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Order Type",                  "{CF3E22CB-F836-4277-9589-998B0BEC3500}", lv2menuOrderSettings.Id,        "OrderTypeController",                 "module/ordertype",                 settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Discount Reason",             "{CBBBFA51-DE2D-4A24-A50E-F7F4774016F6}", lv2menuOrderSettings.Id,        "DiscountReasonController",            "module/discountreason",            settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Market Segment",              "{53B627BE-6AC8-4C1F-BEF4-E8B0A5422E14}", lv2menuOrderSettings.Id,        "MarketSegmentController",             "module/marketsegment",             settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Market Type",                 "{77D7FD11-EBD2-40A2-A40D-C82D32528A01}", lv2menuOrderSettings.Id,        "MarketTypeController",                "module/markettype",                settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Order Set No.",               "{4960D9A7-D1E0-4558-B571-DF1CE1BB8245}", lv2menuOrderSettings.Id,        "OrderSetNoController",                "module/ordersetno",                settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Order Location",              "{CF58D8C9-95EE-4617-97B9-CAFE200719CC}", lv2menuOrderSettings.Id,        "OrderLocationController",             "module/orderlocation",             settingsiconbaseurl + "placeholder.png");
            var lv2menuPaymentSettings      = AddSettingsMenu("Payment Settings",            "{031F8E86-A1A6-482F-AB4F-8DD015AB7642}", lv1menuSettings.Id,                                                                                        settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Payment Terms",               "{44FD799A-1572-4B34-9943-D94FFBEF89D4}", lv2menuPaymentSettings.Id,      "PaymentTermsController",              "module/paymentterms",              settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Payment Type",                "{E88C4957-3A3E-4258-8677-EB6FB61F9BA3}", lv2menuPaymentSettings.Id,      "PaymentTypeController",               "module/paymenttype",               settingsiconbaseurl + "placeholder.png");
            var lv2menuPOSettings           = AddSettingsMenu("PO Settings",                 "{55EDE544-A603-467D-AFA2-EC9C2A650810}", lv1menuSettings.Id,                                                                                        settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("PO Approval Status",          "{22EF1328-FBB1-44D0-A965-4E96675B96CD}", lv2menuPOSettings.Id,           "POApprovalStatusController",          "module/poapprovalstatus",          settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("PO Approver Role",            "{992314B6-A24F-468C-A8B6-5EAC8F14BE16}", lv2menuPOSettings.Id,           "POApproverRoleController",            "module/poapproverrole",            settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("PO Classification",           "{58ef51c5-a97b-43c6-9298-08b064a84a48}", lv2menuPOSettings.Id,           "POClassificationController",          "module/poclassification",          settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("PO Importance",               "{82BF3B3E-0EF8-4A6E-8577-33F23EA9C4FB}", lv2menuPOSettings.Id,           "POImportanceController",              "module/poimportance",              settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("PO Reject Reason",            "{2C6910A8-51BC-421E-898F-C23938B624B4}", lv2menuPOSettings.Id,           "PORejectReasonController",            "module/porejectreason",            settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("PO Type",                     "{BB8D68B3-012A-4B05-BE7F-844EB5C96896}", lv2menuPOSettings.Id,           "POTypeController",                    "module/potype",                    settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("PO Approver",                 "{237B99DC-252D-4197-AB4A-01E795076447}", lv2menuPOSettings.Id,           "POApproverController",                "module/poapprover",                settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Vendor Invoice Approver",     "{4E34DB8F-84C0-4810-B49E-AE6640DD8E4B}", lv2menuPOSettings.Id,           "VendorInvoiceApproverController",     "module/vendorinvoiceapprover",     settingsiconbaseurl + "placeholder.png");
            var lv2menuPresentationSettings = AddSettingsMenu("Presentation Settings",       "{471FF4FC-094B-4D20-B326-C2D7997F5424}", lv1menuSettings.Id,                                                                                        settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Form Design",                 "{4DFEC75D-C33A-4358-9EF1-4D1F5F9C5D73}", lv2menuPresentationSettings.Id, "FormDesignController",                "module/formdesign",                settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Presentation Layer",          "{BBEF0AFD-B46A-46B0-8046-113834736060}", lv2menuPresentationSettings.Id, "PresentationLayerController",         "module/presentationlayer",         settingsiconbaseurl + "placeholder.png");
            var lv2menuProjectSettings      = AddSettingsMenu("Project Settings",            "{AE6366FC-48CD-496F-9DF7-B55E3EF27F63}", lv1menuSettings.Id,                                                                                        settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("As Build",                    "{A3BFF1F7-0951-4F3A-A6DE-1A62BEDF45E6}", lv2menuProjectSettings.Id,      "ProjectAsBuildController",            "module/projectasbuild",            settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Commissioning",               "{0EFE9BBA-0685-4046-A7D6-EC3D34AD01AA}", lv2menuProjectSettings.Id,      "ProjectCommissioningController",      "module/projectcommissioning",      settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Deposit",                     "{24E6F284-7457-4E75-B77D-25B3A6BE6A4D}", lv2menuProjectSettings.Id,      "ProjectDepositController",            "module/projectdeposit",            settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Drawings",                    "{7486859D-243F-4817-8177-6DCB81392C36}", lv2menuProjectSettings.Id,      "ProjectDrawingsController",           "module/projectdrawings",           settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Drop Ship Items",             "{20CD34E6-7E35-4EAF-B4D3-587870412C85}", lv2menuProjectSettings.Id,      "ProjectDropShipItemsController",      "module/projectdropshipitems",      settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Items Ordered",               "{25507FAD-E140-4A19-8FED-2C381DA653D9}", lv2menuProjectSettings.Id,      "ProjectItemsOrderedController",       "module/projectitemsordered",       settingsiconbaseurl + "placeholder.png");
            var lv2menuPropsSettings        = AddSettingsMenu("Props Settings",              "{B210CBF6-9CCE-442F-B11B-6B7AC60C3216}", lv1menuSettings.Id,                                                                                        settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Props Condition",             "{86C769E8-F8E6-4C59-BC0B-8F2D563C698F}", lv2menuPropsSettings.Id,        "PropsConditionController",            "module/propscondition",            settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Region",                      "{A50C7F59-AF91-44D5-8253-5C4A4D5DFB8B}", lv1menuSettings.Id,             "RegionController",                    "module/region",                    settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Repair Item Status",          "{D952672A-DCF6-47C8-9B99-47561C79B3F8}", lv1menuSettings.Id,             "RepairItemStatusController",          "module/repairitemstatus",          settingsiconbaseurl + "placeholder.png");
            var lv2menuSetSettings          = AddSettingsMenu("Set Settings",                "{210AB6DE-159D-4979-B321-3BC1EA6574D7}", lv1menuSettings.Id,                                                                                        settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Set Condition",               "{0FFC8940-C060-49E4-BC24-688E25250C5F}", lv2menuSetSettings.Id,          "SetConditionController",              "module/setcondition",              settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Set Surface",                 "{EC55E743-0CB1-4A74-9D10-6C4C6045AAAB}", lv2menuSetSettings.Id,          "SetSurfaceController",                "module/setsurface",                settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Set Opening",                 "{15E52CA3-475D-4BDA-B940-525E5EEAF8CD}", lv2menuSetSettings.Id,          "SetOpeningController",                "module/setopening",                settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Wall Description",            "{F34F1A9B-53C6-447C-B52B-7FF5BAE38AB5}", lv2menuSetSettings.Id,          "WallDescriptionController",           "module/walldescription",           settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Wall Type",                   "{4C9D2D20-D129-461D-9589-ABC896DD9BC6}", lv2menuSetSettings.Id,          "WallTypeController",                  "module/walltype",                  settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Ship Via",                    "{F9E01296-D240-4E16-B267-898787B29509}", lv1menuSettings.Id,             "ShipViaController",                   "module/shipvia",                   settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Source",                      "{6D6165D1-51F2-4616-A67C-DCC803B549AF}", lv1menuSettings.Id,             "SourceController",                    "module/source",                    settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Tax Option",                  "{5895CA39-5EF8-405B-9E97-2FEB83939EE5}", lv1menuSettings.Id,             "TaxOptionController",                 "module/taxoption",                 settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Template",                    "{BDDB1439-F128-4AB7-9657-B1CDFFA12721}", lv1menuSettings.Id,             "TemplateController",                  "module/template",                  settingsiconbaseurl + "placeholder.png");
            var lv2menuSystemSettings       = AddSettingsMenu("System Settings",             "{EF0A0F0D-F76B-4F25-8AF4-10F7934CFDC0}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Email Settings",              "{8C9613E0-E7E5-4242-9DF6-4F57F59CE2B9}", lv2menuSystemSettings.Id,       "EmailSettingsController",             "module/emailsettings",             settingsiconbaseurl + "placeholder.png");
            var lv2menuUserSettings         = AddSettingsMenu("User Settings",               "{13E1A9A9-1096-447E-B4AE-E538BEF5BCB5}", lv1menuSettings.Id,                                                                                        settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("User Status",                 "{E19916C6-A844-4BD1-A338-FAB0F278122C}", lv2menuUserSettings.Id,         "UserStatusController",                "module/userstatus",                settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Sounds",                      "{29C327DD-7734-4039-9CE2-B25D7CD6F9CB}", lv2menuUserSettings.Id,         "SoundController",                     "module/sound",                     settingsiconbaseurl + "placeholder.png");
            var lv2menuVehicleSettings      = AddSettingsMenu("Vehicle Settings",            "{6081E168-E3BF-439E-82B0-34AF3680C444}", lv1menuSettings.Id,                                                                                        settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("License Class",               "{422F777F-B57F-43DF-8485-F12F3F7BF662}", lv2menuVehicleSettings.Id,      "LicenseClassController",              "module/licenseclass",              settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Vehicle Color",               "{F7A34B70-509A-422F-BFD1-5F30BE2C8186}", lv2menuVehicleSettings.Id,      "VehicleColorController",              "module/vehiclecolor",              settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Vehicle Fuel Type",           "{D9140FB3-084D-4615-8E7A-95731670E682}", lv2menuVehicleSettings.Id,      "VehicleFuelTypeController",           "module/vehiclefueltype",           settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Vehicle Make",                "{299DECA3-B427-49ED-B6AC-2E11F6AA1C4D}", lv2menuVehicleSettings.Id,      "VehicleMakeController",               "module/vehiclemake",               settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Vehicle Rating",              "{09913CDB-68FB-4F18-BBAA-DCA8A8F926E5}", lv2menuVehicleSettings.Id,      "VehicleRatingController",             "module/vehiclerating",             settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Vehicle Schedule Status",     "{A001473B-1FB4-4E85-8093-37A92057CD93}", lv2menuVehicleSettings.Id,      "VehicleScheduleStatusController",     "module/vehicleschedulestatus",     settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Vehicle Status",              "{FB12061D-E6AF-4C09-95A0-8647930C289A}", lv2menuVehicleSettings.Id,      "VehicleStatusController",             "module/vehiclestatus",             settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Vehicle Type",                "{60187072-8990-40BA-8D80-43B451E5BC8B}", lv2menuVehicleSettings.Id,      "VehicleTypeController",               "module/vehicletype",               settingsiconbaseurl + "placeholder.png");
            var lv2menuVendorSettings       = AddSettingsMenu("Vendor Settings",             "{93376B75-2771-474A-8C25-2BBE53B50F5C}", lv1menuSettings.Id,                                                                                        settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Organization Type",           "{fe3a764c-ab55-4ce5-8d7f-bfc86f174c11}", lv2menuVendorSettings.Id,       "OrganizationTypeController",          "module/organizationtype",          settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Vendor Catalog",              "{BDA5E2DC-0FD2-4227-B80F-8414F3F912B8}", lv2menuVendorSettings.Id,       "VendorCatalogController",             "module/vendorcatalog",             settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Vendor Class",                "{8B2C9EE3-AE87-483F-A651-8BA633E6C439}", lv2menuVendorSettings.Id,       "VendorClassController",               "module/vendorclass",               settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("SAP Vendor Invoice Status",   "{1C8E14A3-73A8-4BB6-9B33-65D827B3ED0C}", lv2menuVendorSettings.Id,       "SapVendorInvoiceStatusController",    "module/sapvendorinvoicestatus",    settingsiconbaseurl + "placeholder.png");
            var lv2menuWardrobeSettings     = AddSettingsMenu("Wardrobe Settings",           "{910DAD78-B2AA-4220-89D3-B6A0FA3E0BA1}", lv1menuSettings.Id,                                                                                        settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Wardrobe Care",               "{BE6E4F7C-5D81-4437-A343-8F4933DD6545}", lv2menuWardrobeSettings.Id,     "WardrobeCareController",              "module/wardrobecare",              settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Wardrobe Color",              "{32238B26-3635-4637-AFE0-0D5B12CAAEE4}", lv2menuWardrobeSettings.Id,     "WardrobeColorController",             "module/wardrobecolor",             settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Wardrobe Condition",          "{4EEBE71C-139A-4D09-B589-59DA576C83FD}", lv2menuWardrobeSettings.Id,     "WardrobeConditionController",         "module/wardrobecondition",         settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Wardrobe Gender",             "{28574D17-D2FF-41A0-8117-5F252013E7B1}", lv2menuWardrobeSettings.Id,     "WardrobeGenderController",            "module/wardrobegender",            settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Wardrobe Label",              "{9C1B5157-C983-44EE-817F-171B4448401A}", lv2menuWardrobeSettings.Id,     "WardrobeLabelController",             "module/wardrobelabel",             settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Wardrobe Material",           "{25895901-C700-4618-9ADA-00A7CB4B83B9}", lv2menuWardrobeSettings.Id,     "WardrobeMaterialController",          "module/wardrobematerial",          settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Wardrobe Pattern",            "{2BE7072A-5588-4205-8DCD-0FFE6F0C48F7}", lv2menuWardrobeSettings.Id,     "WardrobePatternController",           "module/wardrobepattern",           settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Wardrobe Period",             "{BF51623D-ABA6-471A-BC00-4729067C64CF}", lv2menuWardrobeSettings.Id,     "WardrobePeriodController",            "module/wardrobeperiod",            settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Wardrobe Source",             "{6709D1A1-3319-435C-BF0E-15D2602575B0}", lv2menuWardrobeSettings.Id,     "WardrobeSourceController",            "module/wardrobesource",            settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Warehouse",                   "{931D3E75-68CB-4280-B12F-9A955444AA0C}", lv1menuSettings.Id,             "WarehouseController",                 "module/warehouse",                 settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Widget",                      "{0CAF7264-D1FB-46EC-96B9-68D242985812}", lv1menuSettings.Id,             "WidgetController",                    "module/widget",                    settingsiconbaseurl + "placeholder.png");
                                                     AddSettingsModule("Work Week",                   "{AF91AE34-ADED-4A5A-BD03-113ED817F575}", lv1menuSettings.Id,             "WorkWeekController",                  "module/workweek",                  settingsiconbaseurl + "placeholder.png");
            //Reports
            var lv2menuAccountingReports      = AddReportsMenu("Accounting Reports",                    "{51A99056-CDB7-48C4-AD5D-FADE02B925B6}", lv1menuReports.Id,                                                                                                          reportsiconbaseurl + "placeholder.png");
                                                       AddReportsModule("A/R Aging",                    "{03497C4E-CFDE-4156-8DDC-8125B84CA96C}", lv2menuAccountingReports.Id,      "RwArAgingReportController",                    "module/aragingreport",                   reportsiconbaseurl + "placeholder.png", "A/R Aging", description: "List unpaid Invoices, and their corresponding aging totals.  Report is subtotalled by Deal and Customer.");
                                                       AddReportsModule("G/L Distribution",             "{2A62BAAE-AC2D-418E-9A04-E46EFB0CDE71}", lv2menuAccountingReports.Id,      "RwGLDistributionReportController",             "module/gldistributionreport",            reportsiconbaseurl + "placeholder.png", "G/L Distribution", "", "", description: "Summarize transaction totals by Account over a date range.");
            var lv2menuBillingReports         = AddReportsMenu("Billing Reports",                       "{DEEA3261-CE48-45A0-B797-5A0E3BB796BF}", lv1menuReports.Id,                                                                                                          reportsiconbaseurl + "placeholder.png");
                                                       AddReportsModule("Agent Billing",                "{EE6CA8A3-C29E-40AD-8C85-EB52842F28A5}", lv2menuBillingReports.Id,         "RwAgentBillingReportController",               "module/agentbillingreport",              reportsiconbaseurl + "placeholder.png", "Agent Billing", "", "", description: "Shows Invoice Activity Totals, subtotalled by Agent.");
                                                       AddReportsModule("Billing Progress",             "{BBEB89E1-093F-4C7B-9827-2ACD63F6534A}", lv2menuBillingReports.Id,         "RwBillingProgressReportController",            "module/billingprogressreport",           reportsiconbaseurl + "placeholder.png", "Billing Progress", "", "", description: "List all Orders and their percentage of total Billing, subtotalled by Customer and Deal.");
                                                       AddReportsModule("Create Invoice Process",       "{071E33E3-E091-484A-A7BD-962A638F4E86}", lv2menuBillingReports.Id,         "RwCreateInvoiceProcessReportController",       "module/createinvoiceprocessreport",      reportsiconbaseurl + "placeholder.png", "Create Invoice Process", "", "", description: "List all Invoices and Exceptions based on a Creation Batch.");
                                                       AddReportsModule("Invoice Discount",             "{9C562281-F742-4261-8F68-420A3422AC68}", lv2menuBillingReports.Id,         "RwInvoiceDiscountReportController",            "module/invoicediscountreport",           reportsiconbaseurl + "placeholder.png", "Invoice Discount", "", "", description: "List all Invoices which have a Discount.");
                                                       AddReportsModule("Invoice Summary",              "{3373DC7D-24F2-4091-9129-5A959D002AB0}", lv2menuBillingReports.Id,         "RwInvoiceSummaryReportController",             "module/invoicesummaryreport",            reportsiconbaseurl + "placeholder.png", "Invoice Summary", "", "", description: "List all Invoices for a specific date range, subtotalled by Customer and Deal.");
                                                       AddReportsModule("Invoice Report",               "{B883BFB3-45E2-487A-9195-F7C8BB3B9535}", lv2menuBillingReports.Id,         "RwInvoiceReportController",                    "module/invoicereport",                   reportsiconbaseurl + "placeholder.png", "Invoice", "", "", description: "Print Invoice.");
                                                       AddReportsModule("Project Manager Billing",      "{E9652DDF-E325-485D-901C-EDAE30409FDB}", lv2menuBillingReports.Id,         "RwProjectManagerBillingReportController",      "module/projectmanagerbillingreport",     reportsiconbaseurl + "placeholder.png", "Project Manager Billing", "", "", description: "Shows Invoice Activity Totals, subtotalled by Project Manager.");
                                                       AddReportsModule("Sales Representative Billing", "{BF9FED34-7896-4A53-A3C8-EC5E8A7D13A3}", lv2menuBillingReports.Id,         "RwSalesRepresentativeBillingReportController", "module/salesrepresentativebillingreport", reportsiconbaseurl + "placeholder.png", "Sales Representative Billing", "", "", description: "Shows Invoice Activity Totals, subtotalled by Sales Representative.");
            var lv2menuChargeProcessingReports = AddReportsMenu("Charge Processing Reports",            "{D31A00DD-58C8-4B22-9017-269B20FF230B}", lv1menuReports.Id, reportsiconbaseurl + "placeholder.png");
                                                       AddReportsModule("Deal Invoice Batch",           "{4BDF8843-CE8D-4B21-BAB3-404A3B227FF6}", lv2menuChargeProcessingReports.Id, "RwDealInvoiceBatchReportController",          "module/dealinvoicebatchreport",          reportsiconbaseurl + "placeholder.png", description: "");
                                                       AddReportsModule("Receipt Batch",                "{D045C2C9-ABE6-4488-8A94-59257319ABC7}", lv2menuChargeProcessingReports.Id, "RwReceiptBatchReportController",              "module/receiptbatchreport",              reportsiconbaseurl + "placeholder.png", description: "");
                                                       AddReportsModule("Vendor Invoice Batch",         "{FF9E8B35-4150-4519-950F-BC92C14358F4}", lv2menuChargeProcessingReports.Id, "RwVendorInvoiceBatchReportController",        "module/vendorinvoicebatchreport",        reportsiconbaseurl + "placeholder.png", description: "");
            var lv2menuContractReports        = AddReportsMenu("Contract Reports",                      "{D537CAA9-6B77-492F-9052-6CCABAEEDBC5}", lv1menuReports.Id,                                                                                                          reportsiconbaseurl + "placeholder.png");
                                                       AddReportsModule("Out Contract",                 "{46DFBBE2-B47B-4D91-A959-39A980DB5130}", lv2menuContractReports.Id,        "RwOutContractReportController",                "reports/outcontractreport",              reportsiconbaseurl + "placeholder.png", description: "Check-Out Contract document.");
            var lv2menuCrewReports            = AddReportsMenu("Crew Reports",                          "{36F274CF-777F-45E3-B5D1-623E6D0E0963}", lv1menuReports.Id,                                                                                                          reportsiconbaseurl + "placeholder.png");
                                                       AddReportsModule("Crew Sign-In",                 "{4D3886A6-18AD-4CE5-9ECC-FB9EC8E104F3}", lv2menuCrewReports.Id,            "RwCrewSignInReportController",                 "module/crewsigninreport",                reportsiconbaseurl + "placeholder.png", "Crew Sign-In", "", "", description: "Lists the Sign In and Out activity for Crew members over a specific date range.");
            var lv2menuDealReports            = AddReportsMenu("Deal Reports",                          "{B14EC8FA-15B6-470C-B871-FB83E7C24CB2}", lv1menuReports.Id,                                                                                                          reportsiconbaseurl + "placeholder.png");
                                                       AddReportsModule("Credits On Account",           "{F6578383-C27F-4842-871F-673807A7C375}", lv2menuDealReports.Id,            "RwCreditsOnAccountReportController",           "module/creditsonaccountreport",          reportsiconbaseurl + "placeholder.png", "Credits on Account", "", "", description: "List each Deal that has an outstanding Credit Memo, Depleting Deposit, or Overpayment.");
                                                       AddReportsModule("Customer Revenue By Type",     "{7B328DAF-B45B-4D87-964C-37C2C17A42CD}", lv2menuDealReports.Id,            "RwCustomerRevenueByTypeReportController",      "module/customerrevenuebytypereport",     reportsiconbaseurl + "placeholder.png", "Customer Revenue By Type", "", "", description: "List each Invoice for a specific date range.  Revenue amounts are broken down by Activity Type (ie. Rentals, Sales, etc).  Revenue is subtotalled by Customer and Deal.");
                                                       AddReportsModule("Deal Outstanding Items",       "{007F72D4-8767-472D-9706-8CDE8C8A9981}", lv2menuDealReports.Id,            "RwDealOutstandingItemsReportController",       "module/dealoutstandingitemsreport",      reportsiconbaseurl + "placeholder.png", "Deal Outstanding Items", "", "", description: "List all items still Checked-Out to a specific Deal.");
            var lv2menuOrderReports           = AddReportsMenu("Order Reports",                         "{17D093EB-4EF0-4391-8DDA-5108D3B16CEB}", lv1menuReports.Id,                                                                                                          reportsiconbaseurl + "placeholder.png");
                                                       AddReportsModule("Late Return / Due Back",       "{7349F6BE-08EE-4202-A571-2A5DEEDB6982}", lv2menuOrderReports.Id,           "RwLateReturnDueBackReportController",          "module/latereturnduebackreport",         reportsiconbaseurl + "placeholder.png", "Late Return / Due Back", "", "", description: "List all items that are Late or Due Back on a specific date.  Data is subtotalled by Order and Deal.");
                                                       AddReportsModule("Pick List",                    "{37A26BC9-9509-4524-9368-3D1E575142BD}", lv2menuOrderReports.Id,           "RwPickListReportController",                   "module/picklistreport",                  reportsiconbaseurl + "placeholder.png", "Pick List", "", "", description: "Pick List document.");
                                                       AddReportsModule("Print Order",                  "{8A0B608D-6C48-4403-AAF0-937FAF33AC46}", lv2menuOrderReports.Id,           "RwOrderReportController",                      "module/orderreport",                     reportsiconbaseurl + "placeholder.png", "Print Order", "", "", description: "Print Order.");
                                                       AddReportsModule("Print Quote",                  "{F6C228BB-ECA0-4850-A620-E32F6406D782}", lv2menuOrderReports.Id,           "RwQuoteReportController",                      "module/orderreport",                     reportsiconbaseurl + "placeholder.png", "Print Quote", "", "", description: "Print Quote.");
            var lv2menuPartsInventoryReports  = AddReportsMenu("Parts Inventory Reports",               "{64C92B2E-CDEA-4D02-97E2-237052A1371C}", lv1menuReports.Id,                                                                                                          reportsiconbaseurl + "placeholder.png");
                                                       AddReportsModule("Parts Inventory Attributes",   "{2599963F-6676-4B5F-9850-864DB31C9427}", lv2menuPartsInventoryReports.Id,  "RwPartsInventoryAttributesReportController",   "module/partsinventoryattributesreport",  reportsiconbaseurl + "placeholder.png", "Parts Inventory Attributes", "", "", description: "List all Parts Inventory with their Attributes and Values.");
                                                       AddReportsModule("Parts Inventory Purchase History", "{7EC005CB-C3AE-4CF3-892A-78C6591449BE}", lv2menuPartsInventoryReports.Id, "RwPartsInventoryPurchaseHistoryReportController", "module/partsinventorypurchasehistoryreport", reportsiconbaseurl + "placeholder.png", "Parts Inventory Purchase History", "", "", description: "List all Parts Inventory Purchase History.");
                                                       AddReportsModule("Parts Inventory Reorder",      "{F1EC6BAE-4DBF-4B1D-9B91-CAC42E094C34}", lv2menuPartsInventoryReports.Id,  "RwPartsInventoryReorderReportController",      "module/partsinventoryreorderreport",     reportsiconbaseurl + "placeholder.png", "Parts Inventory Reorder", "", "", description: "List all Parts Inventory to reorder. Filter items with quantity levels at or below their Reorder Point.");
                                                       AddReportsModule("Parts Inventory Transactions", "{D2D997C9-ACCE-4F20-A192-9A8B8E232278}", lv2menuPartsInventoryReports.Id,  "RwPartsInventoryTransactionReportController",  "module/partsinventorytransactionreport", reportsiconbaseurl + "placeholder.png", "Parts Inventory Transactions", "", "", description: "List all Parts Inventory Transactions, including Cost and Price, over a specific date range.");
            var lv2menuRentalInventoryReports = AddReportsMenu("Rental Inventory Reports",              "{36E1319D-928A-47A5-8CDE-69EF2085D742}", lv1menuReports.Id,                                                                                                          reportsiconbaseurl + "placeholder.png");
                                                       AddReportsModule("Rental Inventory Attributes",  "{291E443F-66F5-4ACB-9811-1580E0F57900}", lv2menuRentalInventoryReports.Id, "RwRentalInventoryAttributesReportController",  "module/rentalinventoryattributesreport", reportsiconbaseurl + "placeholder.png", "Rental Inventory Attributes", "", "", description: "List all Rental Inventory with their Attributes and Values.");
                                                       AddReportsModule("Rental Inventory Catalog",     "{8840DE50-936E-4168-B061-A6092857A979}", lv2menuRentalInventoryReports.Id, "RwRentalInventoryCatalogReportController",     "module/rentalinventorycatalog",          reportsiconbaseurl + "placeholder.png", "Rental Inventory Catalog", "", "", description: "List all Rental Inventory, current Rates, Replacement Cost, and Owned Quantity.");
                                                       AddReportsModule("Rental Inventory Change",      "{0F628643-1F4E-447B-9CC9-8046C858EF19}", lv2menuRentalInventoryReports.Id, "RwRentalInventoryChangeReportController",      "module/RentalInventoryChangeReport",     reportsiconbaseurl + "placeholder.png", "Rental Inventory Change", "", "", description: "List all Rental Inventory changes.");
                                                       AddReportsModule("Rental Inventory Purchase History", "{883EFC41-E0AF-42CE-8C62-9387FCA19726}", lv2menuRentalInventoryReports.Id, "RwRentalInventoryPurchaseHistoryReportController", "module/rentalinventorypurchasehistoryreport", reportsiconbaseurl + "placeholder.png", "Rental Inventory Purchase History", "", "", description: "List all Rental Inventory Purchase History.");
                                                       AddReportsModule("Rental Inventory Value",       "{C01E92F1-8575-4C44-AC25-81F38B1EE1E0}", lv2menuRentalInventoryReports.Id, "RwRentalInventoryValueReportController",       "module/rentalinventoryvalue",            reportsiconbaseurl + "placeholder.png", "Rental Inventory Value", "", "", description: "List all Rental Inventory and get a current value, historical value, or change in value over a date range.");
                                                       AddReportsModule("Retired Rental Inventory",     "{E85E3E91-6DEF-4A4B-ACA3-21E3F7154101}", lv2menuRentalInventoryReports.Id, "RwRetiredRentalInventoryReportController",     "module/retiredrentalinventory",          reportsiconbaseurl + "placeholder.png", "Retired Rental Inventory", "", "", description: "List all Rental Inventory Retired during a specified date range.");
                                                       AddReportsModule("Return On Asset",              "{E80BD3B2-A855-4931-8195-419B5E531157}", lv2menuRentalInventoryReports.Id, "RwReturnOnAssetReportController",              "module/returnonassetreport",             reportsiconbaseurl + "placeholder.png", "Return on Asset", "", "", description: "Calculate the Revenue, Value, Utilization, and Return on Asset for all Rental Inventory for various date ranges.");
                                                       AddReportsModule("Unretired Rental Inventory",   "{AA8B5A75-16DF-4741-88AC-30DD438831D3}", lv2menuRentalInventoryReports.Id, "RwUnretiredRentalInventoryReportController",   "module/UnretiredRentalInventoryReport",  reportsiconbaseurl + "placeholder.png", "Unretired Rental Inventory", "", "", description: "List all Rental Inventory unretired during a specified date range.");

            var lv2menuSalesInventoryReports  = AddReportsMenu("Sales Inventory Reports",               "{63978CEE-0A14-4926-AB15-88378B2F3A7A}", lv1menuReports.Id,                                                                                                          reportsiconbaseurl + "placeholder.png");
                                                       AddReportsModule("Sales Inventory Attributes",   "{FAF802B8-0C63-47DF-91A1-B3E157714E3B}", lv2menuSalesInventoryReports.Id, "RwSalesInventoryAttributesReportController", "module/salesinventoryattributesreport", reportsiconbaseurl + "placeholder.png", "Sales Inventory Attributes", "", "", description: "List all Sales Inventory with their Attributes and Values.");
                                                       AddReportsModule("Sales Inventory Purchase History", "{B50E2E87-0BE2-4B5A-B3AA-6985424657AE}", lv2menuSalesInventoryReports.Id, "RwSalesInventoryPurchaseHistoryReportController", "module/salesinventorypurchasehistoryreport", reportsiconbaseurl + "placeholder.png", "Sales Inventory Purchase History", "", "", description: "List all Sales Inventory Purchase History.");
                                                       AddReportsModule("Sales Inventory Reorder",      "{7F0AF306-CA9A-489C-9F91-86AEF0FC75EB}", lv2menuSalesInventoryReports.Id,  "RwSalesInventoryReorderReportController",      "module/salesinventoryreorderreport",     reportsiconbaseurl + "placeholder.png", "Sales Inventory Reorder", "", "", description: "List all Sales Inventory to reorder. Filter items with quantity levels at or below their Reorder Point.");
                                                       AddReportsModule("Sales Inventory Transactions", "{DEC28EAB-7D64-4A8C-ACBB-CB72706B818F}", lv2menuSalesInventoryReports.Id,  "RwSalesInventoryTransactionReportController",  "module/salesinventorytransactionreport", reportsiconbaseurl + "placeholder.png", "Sales Inventory Transactions", "", "", description: "List all Sales Inventory Transactions, including Cost and Price, over a specific date range.");

            AddModule("Print Order",           "{8B69F4A5-5617-45D1-84BB-E5ED6B2F031D}", null, "RwPrintOrderController", "module/printorder", homeiconbaseurl + "placeholder.png", "Print Order", "", "");

            // Add Utilities 
            AddModule("Dashboard",                     "{DF8111F5-F022-40B4-BAE6-23B2C6CF3705}", lv1menuUtilities.Id, "DashboardController",                    "module/dashboard",                 utilitiesiconbaseurl + "placeholder.png");
            AddModule("Dashboard Settings",            "{1B40C62A-1FA0-402E-BE52-9CBFDB30AD3F}", lv1menuUtilities.Id, "DashboardSettingsController",            "module/dashboardsettings",         utilitiesiconbaseurl + "placeholder.png");
            //var lv2menuChargeProcessing              = AddLv2ModuleMenu("Charge Processing", "{11349784-B621-468E-B0AD-899A22FCA9AE}", lv1menuUtilities.Id, utilitiesiconbaseurl + "placeholder.png", "Charge Processing");
            AddModule("Process Invoices",              "{5DB3FB9C-6F86-4696-867A-9B99AB0D6647}", lv1menuUtilities.Id, "InvoiceProcessBatchController",          "module/invoiceprocessbatch",       utilitiesiconbaseurl + "placeholder.png", "", "", "");
            AddModule("Process Receipts",              "{0BB9B45C-57FA-47E1-BC02-39CEE720792C}", lv1menuUtilities.Id, "ReceiptProcessBatchController",          "module/receiptprocessbatch",       utilitiesiconbaseurl + "placeholder.png", "", "", "");
            AddModule("Process Vendor Invoices",       "{4FA8A060-F2DF-4E59-8F9D-4A6A62A0D240}", lv1menuUtilities.Id, "VendorInvoiceProcessBatchController",    "module/vendorinvoiceprocessbatch", utilitiesiconbaseurl + "placeholder.png", "", "", "");
            AddModule("Export Settings",               "{70CEC5BB-2FD9-4C68-9BE2-F8A3C6A17BB7}", lv1menuUtilities.Id, "ExportSettingsController",               "module/exportsettings",            utilitiesiconbaseurl + "placeholder.png", "", "", "");
            AddModule("Activity Calendar",             "{897BCF55-6CE7-412C-82CB-557B045F8C0A}", lv1menuUtilities.Id, "ActivityCalendarController",             "module/activitycalendar",          utilitiesiconbaseurl + "placeholder.png", "", "", "");

            // Add Administrator 
            AddModule("Control",             "{B3ADDF49-64EB-4740-AB41-4327E6E56242}", lv1menuAdministrator.Id, "ControlController",        "module/control",        administratoriconbaseurl + "placeholder.png");
            AddModule("Custom Field",        "{C98C4CB4-2036-4D70-BC29-8F5A2874B178}", lv1menuAdministrator.Id, "CustomFieldController",    "module/customfield",    administratoriconbaseurl + "placeholder.png");
            AddModule("Custom Forms",        "{CB2EF8FF-2E8D-4AD0-B880-07037B839C5E}", lv1menuAdministrator.Id, "CustomFormController",     "module/customform",     administratoriconbaseurl + "placeholder.png");
            AddModule("Duplicate Rules",     "{2E0EA479-AC02-43B1-87FA-CCE2ABA6E934}", lv1menuAdministrator.Id, "DuplicateRuleController",  "module/duplicaterule",  administratoriconbaseurl + "placeholder.png");
            AddModule("Email History",       "{3F44AC27-CE34-46BA-B4FB-A8AEBB214167}", lv1menuAdministrator.Id, "EmailHistoryController",   "module/emailhistory",   administratoriconbaseurl + "placeholder.png");
            AddModule("Group",               "{9BE101B6-B406-4253-B2C6-D0571C7E5916}", lv1menuAdministrator.Id, "GroupController",          "module/group",          administratoriconbaseurl + "group.png", "USER");
            AddModule("Hotfix",              "{9D29A5D9-744F-40CE-AE3B-09219611A680}", lv1menuAdministrator.Id, "HotfixController",         "module/hotfix",         administratoriconbaseurl + "placeholder.png");
            //AddModule("Integration",         "{518B038E-F22A-4B23-AA47-F4F56709ADC3}", lv1menuAdministrator.Id, "IntegrationController",  "module/integration",    administratoriconbaseurl + "placeholder.png", "Integration", "quickbooks", "USER");
            AddModule("User",                "{79E93B21-8638-483C-B377-3F4D561F1243}", lv1menuAdministrator.Id, "UserController",           "module/user",           administratoriconbaseurl + "user.png", "USER");
            AddModule("Settings",            "{57150967-486A-42DE-978D-A2B0F843341A}", lv1menuAdministrator.Id, "SettingsController",       "module/settings",       administratoriconbaseurl + "placeholder.png");
            AddModule("Reports",             "{3C5C7603-9E7B-47AB-A722-B29CA09B3B8C}", lv1menuAdministrator.Id, "ReportsController",        "module/reports",        administratoriconbaseurl + "placeholder.png");
            // Add Submodules
            AddSubModule("User Settings", "{A6704904-01E1-4C6B-B75A-C1D3FCB50C01}", lv1menuSubModules.Id, "UserSettingsController");
            AddSubModule("SubWorksheet", "{F24BDA2F-B37C-45C1-B08E-588D02D50B7C}", lv1menuSubModules.Id, "SubWorksheetController");

            // Add Grids
            AddGrid("Attribute Value",                        "{C11904A1-D612-469C-BFA6-E14534FC8E31}", lv1menuGrids.Id, "AttributeValueGridController");
            AddGrid("Audit History",                          "{FA958D9E-7863-4B03-94FE-A2D2B9599FAB}", lv1menuGrids.Id, "AuditHistoryGridController");
            AddGrid("Billing Cycle Events",                   "{8AAD752A-74B8-410D-992F-08398131EBA7}", lv1menuGrids.Id, "BillingCycleEventsGridController");
            AddGrid("Checked-In Item",                        "{5845B960-827B-4A89-9FC4-E41108C27C21}", lv1menuGrids.Id, "CheckedInItemGridController");
            AddGrid("Checked-Out Item",                       "{48CC9E19-7B73-4BA7-9531-20BEA3780193}", lv1menuGrids.Id, "CheckedOutItemGridController");
            AddGrid("Check-In Exception",                     "{E6A2B313-ADEC-41DD-824E-947097E63060}", lv1menuGrids.Id, "CheckInExceptionGridController");
            AddGrid("Check-In Order",                         "{F314F7FA-8740-4851-8CB5-DA15EC02A5E7}", lv1menuGrids.Id, "CheckInOrderGridController");
            AddGrid("Check-In Quantity Items",                "{2D2D0746-D66E-476E-9750-C11BA93A20C9}", lv1menuGrids.Id, "CheckInQuantityItemsGridController");
            AddGrid("Check-In Swap",                          "{47563A6D-1B0A-43C2-AE0E-8EF7AEB5D13B}", lv1menuGrids.Id, "CheckInSwapGridController");
            AddGrid("Contact",                                "{B6A0CAFC-35E8-4490-AEED-29F4E3426758}", lv1menuGrids.Id, "RwContactGridController");
            AddGrid("Contact Company",                        "{68E99935-E0AB-4552-BBFF-46ED2965E4F0}", lv1menuGrids.Id, "ContactCompanyGridController");
            AddGrid("Company Contact",                        "{4172C587-7968-4664-A836-83A14A5B2B48}", lv1menuGrids.Id, "CompanyContactGridController");
            AddGrid("Company Tax Option",                     "{B7E9F2F8-D28C-43C6-A91F-40B9B530C8A1}", lv1menuGrids.Id, "CompanyTaxOptionGridController");
            AddGrid("Contact Document",                       "{CC8F52FF-D968-4CE6-BF7A-3AC859D25280}", lv1menuGrids.Id, "FwContactDocumentGridController");
            AddGrid("Contact Email History",                  "{DAA5E81D-353C-4AAA-88A8-B4E7046B5FF0}", lv1menuGrids.Id, "FwContactEmailHistoryGridController");
            AddGrid("Contact Note",                           "{A9CB5D4D-4AC0-46D4-A084-19039CF8C654}", lv1menuGrids.Id, "ContactNoteGridController");
            AddGrid("Contact Personal Event",                 "{96B55326-31BB-46C1-BD11-DE1201A8CB51}", lv1menuGrids.Id, "ContactPersonalEventGridController");
            AddGrid("Container Warehouse",                    "{97F0F1B5-5C90-4861-A840-54FE35F58835}", lv1menuGrids.Id, "ContainerWarehouseGridController");
            AddGrid("Contract Detail",                        "{30A4330D-516A-4B84-90FE-C8DDCC54DF02}", lv1menuGrids.Id, "ContractDetailGridController");
            AddGrid("Contract Exchange Item",                 "{E91A6E7B-9F19-4368-ACD1-19693B273161}", lv1menuGrids.Id, "ContractExchangeItemGridController");
            AddGrid("Contract Summary",                       "{D545110F-65B3-43B7-BAA8-334E35975881}", lv1menuGrids.Id, "ContractSummaryGridController");
            AddGrid("Crew Location",                          "{FFF47B06-017C-417B-A05B-AD8670126E06}", lv1menuGrids.Id, "CrewLocationGridController");
            AddGrid("Crew Position",                          "{C87470C4-6D8A-4040-A7EF-E9B393B583CA}", lv1menuGrids.Id, "CrewPositionGridController");
            AddGrid("Customer Note",                          "{50EB024E-6D9A-440A-8161-458A2E89EFB8}", lv1menuGrids.Id, "CustomerNoteGridController");
            AddGrid("Custom Form Group",                      "{2D12FA3B-2BC3-4838-9B79-05303F7D3120}", lv1menuGrids.Id, "CustomFormGroupGridController");
            AddGrid("Custom Form User",                       "{FAAAE8F2-F68F-4B26-97E3-D143A80D1C18}", lv1menuGrids.Id, "CustomFormUserGridController");
            AddGrid("Company Resale",                         "{571F090C-D7EC-4D95-BA7B-84D09B609F39}", lv1menuGrids.Id, "CompanyResaleGridController");
            AddGrid("Deal Notes",                             "{562D88B4-7CFB-4239-B445-C30BE8F8BAC9}", lv1menuGrids.Id, "DealNoteGridController");
            AddGrid("Deal Shipper",                           "{032CBF05-9924-4244-AB5A-B5298E6F7498}", lv1menuGrids.Id, "DealShipperGridController");
            AddGrid("Discount Item",                          "{2EB32722-33D0-43C4-B799-ECD81EDF9C99}", lv1menuGrids.Id, "DiscountItemGridController");
            AddGrid("Discount Item Labor",                    "{B65A5839-0226-4BAD-99F9-64FA9D1C1E33}", lv1menuGrids.Id, "DiscountItemLaborGridController");
            AddGrid("Discount Item Misc",                     "{5974DBEF-1D45-4B11-BA85-CB05B725F54C}", lv1menuGrids.Id, "DiscountItemMiscGridController");
            AddGrid("Discount Item Rental",                   "{FF124304-4048-4A1F-A6DA-2F79343BCE87}", lv1menuGrids.Id, "DiscountItemRentalGridController");
            AddGrid("Discount Item Sales",                    "{85AB2907-07FE-43CF-B16D-DDAE781F64ED}", lv1menuGrids.Id, "DiscountItemSalesGridController");
            AddGrid("Document Version",                       "{397FF02A-BF19-4C1F-8E5F-9DBE786D77EC}", lv1menuGrids.Id, "FwAppDocumentVersionGridController");
            AddGrid("Duplicate Rule Field",                   "{0B65E7C7-E661-466A-BBFA-D2A32FB03FF7}", lv1menuGrids.Id, "DuplicateRuleFieldGridController");
            AddGrid("Event Type Personnel Type",              "{F14FB171-801C-4CD0-A589-DF9511B501F7}", lv1menuGrids.Id, "EventTypePersonnelTypeGridController");
            AddGrid("Exchange Item",                          "{B58D8E40-D6C1-45D4-97B8-18A1270822B9}", lv1menuGrids.Id, "ExchangeItemGridController");
            AddGrid("Fiscal Year",                            "{F273F7A6-357E-4616-B84E-28D4C321ACF4}", lv1menuGrids.Id, "FiscalYearGridController");
            AddGrid("Fiscal Month",                           "{EB2DCCD4-0747-4055-87A4-0C60D811AFB5}", lv1menuGrids.Id, "FiscalMonthGridController");
            AddGrid("Floor",                                  "{472B5E4A-57BB-4DFB-AD6A-D0F71915124B}", lv1menuGrids.Id, "FloorGridController");
            AddGrid("Generator Make Model",                   "{12109673-165E-4620-8121-AF4259C7F367}", lv1menuGrids.Id, "GeneratorMakeModelGridController");
            AddGrid("Generator Type Warehouse",               "{A310B3F4-2B34-433A-8F24-04400B45670A}", lv1menuGrids.Id, "GeneratorTypeWarehouseGridController");
            AddGrid("G/L Distribution",                       "{A41DF75D-A3A3-40B8-84E0-7B8F8DACDC35}", lv1menuGrids.Id, "GlDistributionGridController");
            AddGrid("Inventory Group Inventory",              "{2EE8822B-F83E-4D8B-B055-4DA5853080C8}", lv1menuGrids.Id, "InventoryGroupInvGridController");
            AddGrid("Inventory Attribute Value",              "{D591CCE2-920C-440D-A6D7-6F4F21FC01B8}", lv1menuGrids.Id, "InventoryAttributeValueGridController");
            AddGrid("Inventory Availability",                 "{8241ACB4-9346-43D6-8D3C-B6567FAA0270}", lv1menuGrids.Id, "InventoryAvailabilityGridController");
            AddGrid("Inventory Compatibility",                "{7416DAAE-2875-408B-AEEF-78481378C4C4}", lv1menuGrids.Id, "InventoryCompatibilityGridController");
            AddGrid("Inventory Compatibility",                "{69790C03-D7CC-4422-9122-674E2BCCA040}", lv1menuGrids.Id, "SalesInventoryCompatibilityGridController");
            AddGrid("Inventory Complete",                     "{3CB67F46-92B8-4F42-A04C-DB5BA6B52B29}", lv1menuGrids.Id, "InventoryCompleteGridController");
            AddGrid("Inventory Complete Kit",                 "{797339C1-79C3-4FC0-82E4-7DA2FE150DDA}", lv1menuGrids.Id, "InventoryCompleteKitGridController");
            AddGrid("Inventory Consignment",                  "{0D22AF5B-CF50-41EA-A8CC-D039C402E4CC}", lv1menuGrids.Id, "InventoryConsignmentGridController");
            AddGrid("Inventory Container",                    "{494F7DD0-0D32-4FE0-B84A-BC7CD71CE9EC}", lv1menuGrids.Id, "InventoryContainerGridController");
            AddGrid("Inventory Kit",                          "{989C0F67-5F4D-4BC2-832F-D8009256AF0F}", lv1menuGrids.Id, "InventoryKitGridController");
            AddGrid("Inventory Prep",                         "{338934FD-CA10-48F4-9498-2D5250F4E6FA}", lv1menuGrids.Id, "InventoryPrepGridController");
            AddGrid("Inventory Qc",                           "{C1EE89A8-2C6C-4709-AB0C-2BBC062160B5}", lv1menuGrids.Id, "InventoryQcGridController");
            AddGrid("Inventory Substitute",                   "{B715DFB4-5700-48DE-878A-F8D93F99ECA3}", lv1menuGrids.Id, "InventorySubstituteGridController");
            AddGrid("Inventory Vendor",                       "{C68281F9-0FC9-4FFE-8931-A5E501577AC3}", lv1menuGrids.Id, "InventoryVendorGridController");
            AddGrid("Inventory Warehouse Staging",            "{3D9F7C07-4B47-4E4C-B573-331D694B979E}", lv1menuGrids.Id, "InventoryWarehouseStagingGridController");
            AddGrid("Invoice Item",                           "{8D093CB9-1C37-449F-8E64-E76653488ABB}", lv1menuGrids.Id, "InvoiceItemGridController");
            AddGrid("Invoice Order",                          "{D4B2DBB4-FDB8-461E-A3BE-EE81F43A61C6}", lv1menuGrids.Id, "InvoiceOrderGridController");
            AddGrid("Invoice Receipt",                        "{1C5F43F4-7428-4246-95B2-F45CE950CAFF}", lv1menuGrids.Id, "InvoiceReceiptGridController");
            AddGrid("Invoice Revenue",                        "{8066A976-772F-4CCF-A227-EF4EE95CA137}", lv1menuGrids.Id, "InvoiceRevenueGridController");
            AddGrid("Invoice Status History",                 "{08E2713B-9B57-4B1F-8859-E7B10E116EAA}", lv1menuGrids.Id, "InvoiceStatusHistoryGridController");
            AddGrid("Invoice Notes",                          "{09E91168-0C59-4EC7-9DCD-2B65F0EB2A6C}", lv1menuGrids.Id, "InvoiceNoteGridController");
            AddGrid("Item Attribute Value",                   "{22D75843-E915-4956-9B25-C52E815F3C5E}", lv1menuGrids.Id, "ItemAttributeValueGridController");
            AddGrid("Inventory Location Tax",                 "{7DDD2E10-5A1E-4FE9-BBA5-FDBE99DF04F6}", lv1menuGrids.Id, "InventoryLocationTaxGridController");
            AddGrid("Item Qc",                                "{496FEE6D-FC41-47D7-8576-7EF95CAE1B18}", lv1menuGrids.Id, "ItemQcGridController");
            AddGrid("Loss and Damage Items",                  "{D9D02203-025E-47BD-ADF4-0436DC5593BB}", lv1menuGrids.Id, "LossAndDamageItemGridController");
            AddGrid("Master Item",                            "{F21525ED-EDAC-4627-8791-0B410C74DAAE}", lv1menuGrids.Id, "RwMasterItemGridController");
            AddGrid("Market Segment Job",                     "{6CB1FD8E-5E6E-45BC-B0E6-AC8E06A38990}", lv1menuGrids.Id, "MarketSegmentJobGridController");
            AddGrid("Order Activity Dates",                   "{E00980E5-7A1C-4438-AB06-E8B7072A7595}", lv1menuGrids.Id, "RwOrderActivityDatesGridController");
            AddGrid("Order Contact",                          "{33321573-EB0C-43BE-9C95-739A879FC81B}", lv1menuGrids.Id, "OrderContactGridController");
            AddGrid("Order Contract Note",                    "{2018FEB8-D15D-4F1C-B09D-9BCBD5491B52}", lv1menuGrids.Id, "RwOrderContractNoteGridController");
            AddGrid("Order Dates",                            "{D4B28F52-5C9D-4D8C-B58C-42924428DE93}", lv1menuGrids.Id, "RwOrderDatesGridController");
            // AddGrid("Order Item Combined",                    "{B8E0EFB5-F175-46DE-92A7-32B45E6942FC}", lv1menuGrids.Id, "OrderItemCombinedGridController");
            AddGrid("Order Item",                             "{C8A77000-43DD-4E49-A226-1E0DC4196F12}", lv1menuGrids.Id, "OrderItemGridController");
            AddGrid("Order Note",                             "{55248753-DF49-46E3-84AE-0532354F3550}", lv1menuGrids.Id, "OrderNoteGridController");
            AddGrid("Order Note",                             "{45573B9C-B39D-4975-BC36-4A41362E1AF0}", lv1menuGrids.Id, "RwOrderNoteGridController");
            AddGrid("Order Pick List",                        "{ABE29218-C144-4CA7-825F-3FDA7DC860A5}", lv1menuGrids.Id, "OrderPickListGridController");
            AddGrid("Order Snapshot",                         "{4259A144-C7C8-4382-BAB9-9FFA278AF294}", lv1menuGrids.Id, "OrderSnapshotGridController");
            AddGrid("Order Type Terms And Conditions",        "{CD65AB0D-A92D-4CA9-9EB3-1F789BC51717}", lv1menuGrids.Id, "OrderTypeTermsAndConditionsGridController");
            AddGrid("Order Status History",                   "{D5B97814-9FD7-4821-9553-28D276F67797}", lv1menuGrids.Id, "OrderStatusHistoryGridController");
            AddGrid("Order Status Summary",                   "{959E3D3C-B83D-4ACC-997D-A5508DE0A542}", lv1menuGrids.Id, "OrderStatusSummaryGridController");
            AddGrid("Order Status Rental Detail",             "{5B497696-B956-453D-A2A0-755B84F8E83D}", lv1menuGrids.Id, "OrderStatusRentalDetailGridController");
            AddGrid("Order Status Sales Detail",              "{220300EC-40A7-4374-8247-BE6BFC5CDF14}", lv1menuGrids.Id, "OrderStatusSalesDetailGridController");
            AddGrid("Order Type Activity Dates",              "{0C7E7F68-50C8-45A0-B6CA-BE11223D7806}", lv1menuGrids.Id, "OrderTypeActivityDatesGridController");
            AddGrid("Order Type Cover Letter",                "{7521D3CC-FF1C-44F5-8F93-9272B6CADC64}", lv1menuGrids.Id, "OrderTypeCoverLetterGridController");
            AddGrid("Order Type Invoice Export",              "{B24187E9-6B1D-4717-B9C2-F95C5543AE45}", lv1menuGrids.Id, "OrderTypeInvoiceExportGridController");
            AddGrid("Order Type Note",                        "{DD3B6D98-DBAC-467D-A3A8-244FCD4E750A}", lv1menuGrids.Id, "OrderTypeNoteGridController");
            AddGrid("Order Type Contact Title",               "{E104C48C-2579-4674-9BD1-41069AC6968B}", lv1menuGrids.Id, "OrderTypeContactTitleGridController");
            AddGrid("Parts Inventory Compatibility",          "{97DC0D58-2968-47F4-970A-0889AEFDC63B}", lv1menuGrids.Id, "PartsInventoryCompatibilityGridController");
            AddGrid("Parts Inventory Substitute",             "{F9B0308B-EBFC-4B37-B812-27E16897B115}", lv1menuGrids.Id, "PartsInventorySubstituteGridController");
            AddGrid("Pick List Item",                         "{F8514841-7652-469B-AF43-3520A34EA5F0}", lv1menuGrids.Id, "PickListItemGridController");
            AddGrid("Pick List Utility",                      "{0DAED562-2319-4569-AC4E-EF89198E54BC}", lv1menuGrids.Id, "PickListUtilityGridController");
            AddGrid("Pending Items",                          "{28DA22B8-D429-4751-B97D-8210D78C9402}", lv1menuGrids.Id, "CheckOutPendingItemGridController");
            AddGrid("PO Approver",                            "{314CEEC5-6E42-4539-BD10-8F680A0F70F4}", lv1menuGrids.Id, "POApproverGridController");
            AddGrid("PO Receive Bar Code",                    "{27703F1E-8F2A-44E3-93AF-F46BADC3D4B1}", lv1menuGrids.Id, "POReceiveBarCodeGridController");
            AddGrid("PO Receive Items",                       "{EF042B8D-23B8-4253-A6E8-11603E800629}", lv1menuGrids.Id, "POReceiveItemGridController");
            AddGrid("PO Return Bar Code",                     "{C25168A5-1741-4E77-83C9-CA52FBC2C794}", lv1menuGrids.Id, "POReturnBarCodeGridController");
            AddGrid("PO Return Items",                        "{10CF4A1A-3F85-4A8C-A4D7-ACEC1DB12CFC}", lv1menuGrids.Id, "POReturnItemGridController");
            AddGrid("Presentation Layer Activity",            "{AA12FF6E-DE89-4C9A-8DB6-E42542BB1689}", lv1menuGrids.Id, "PresentationLayerActivityGridController");
            AddGrid("Presentation Layer Activity Override",   "{ABA89B3D-AA83-4298-AAD4-AC5294BE7388}", lv1menuGrids.Id, "PresentationLayerActivityOverrideGridController");
            AddGrid("Presentation Layer Form",                "{88985C09-65AD-4480-830A-EFCE95C3940B}", lv1menuGrids.Id, "PresentationLayerFormGridController");
            AddGrid("Project Contact",                        "{F0D3B8C2-1CEC-41B6-81E4-D7B9C821684B}", lv1menuGrids.Id, "ProjectContactGridController");
            AddGrid("Project Note",                           "{686240FE-8276-4715-A7ED-44B4D4A7DC86}", lv1menuGrids.Id, "ProjectNoteGridController");
            AddGrid("Quik Entry Accessories Options",         "{27317105-BA68-417A-A592-86EEB977CA32}", lv1menuGrids.Id, "RwQuikEntryAccessoriesOptionsGridController");
            AddGrid("Quik Entry Category",                    "{01604AEC-2127-4756-BF92-3A340EF000E1}", lv1menuGrids.Id, "RwQuikEntryCategoryGridController");
            AddGrid("Quik Entry Department",                  "{2AC10F3D-FC50-4454-87C2-54ABBCCD08AB}", lv1menuGrids.Id, "RwQuikEntryDepartmentGridController");
            AddGrid("Quik Entry Items",                       "{1289FF25-5C86-43CC-8557-173E7EA69696}", lv1menuGrids.Id, "RwQuikEntryItemsGridController");
            AddGrid("Quik Entry Sub Category",                "{26576DCB-4141-477A-9A3D-4F76D862C581}", lv1menuGrids.Id, "RwQuikEntrySubCategoryGridController");
            AddGrid("Rate Location Tax",                      "{F1A613A6-FD31-4082-88CC-4F0252BF56AC}", lv1menuGrids.Id, "RateLocationTaxGridController");
            AddGrid("Rental Inventory Warehouse",             "{3AC00695-4130-4A34-B4B2-BC6E3E950FB1}", lv1menuGrids.Id, "RentalInventoryWarehouseGridController");
            AddGrid("Repair Cost",                            "{38219D4D-C8F6-4C8C-B86B-D86D5F645251}", lv1menuGrids.Id, "RepairCostGridController");
            AddGrid("Repair Release",                         "{06BFFEEF-632D-4DBE-9DFC-E64309784D44}", lv1menuGrids.Id, "RepairReleaseGridController");
            AddGrid("Repair Part",                            "{D3EB3232-9976-4607-A86F-7D64DF2AD4F8}", lv1menuGrids.Id, "RepairPartGridController");
            AddGrid("Rate Warehouse",                         "{2EC39399-B731-4B22-A5F3-1919A275AA56}", lv1menuGrids.Id, "RateWarehouseGridController");
            AddGrid("Report Settings",                        "{0B524E5D-0644-445D-B9FA-9E15A827F1B2}", lv1menuGrids.Id, "ReportSettingsGridController");
            AddGrid("Sales Inventory Substitute",             "{ED6DCEB4-2BB7-4B52-915A-10E1D94B083E}", lv1menuGrids.Id, "SalesInventorySubstituteGridController");
            AddGrid("Search Preview",                         "{A6C93317-0DDC-4781-9B01-2EFC78ECED40}", lv1menuGrids.Id, "SearchPreviewGridController");
            AddGrid("Sales Inventory Warehouse",              "{85ED5C98-37AF-4A68-B97B-68EE253A1FD4}", lv1menuGrids.Id, "SalesInventoryWarehouseGridController");
            AddGrid("Single Rate Warehouse",                  "{0E4E4B5D-5905-4BD5-AC57-2DE047EFEB5B}", lv1menuGrids.Id, "SingleRateWarehouseGridController");
            AddGrid("Space",                                  "{BF54AEF8-BECB-4069-A1E3-3FEA27301AE8}", lv1menuGrids.Id, "SpaceGridController");
            AddGrid("Space Rate",                             "{F0A6AFE7-3A31-4D2D-BC37-702D785C3734}", lv1menuGrids.Id, "SpaceRateGridController");
            AddGrid("Space Warehouse Rate",                   "{0F264871-A72C-48F7-9A6C-891208F52AD1}", lv1menuGrids.Id, "SpaceWarehouseRateGridController");
            AddGrid("Staged Items",                           "{132DEBAB-45F6-4977-A1A8-BAE5AC152780}", lv1menuGrids.Id, "StagedItemGridController");
            AddGrid("Stage Holding Items",                    "{48D4A52C-0B47-4C85-BAB7-8B0A20DF895F}", lv1menuGrids.Id, "StageHoldingItemGridController");
            AddGrid("Stage Quantity Items",                   "{3CCB3EB0-983F-4974-9F7F-8B12A8C7DDE9}", lv1menuGrids.Id, "StageQuantityItemGridController");
            AddGrid("Sub Category",                           "{070EBAE0-903E-48CE-9285-BDC3ECC07C68}", lv1menuGrids.Id, "SubCategoryGridController");
            AddGrid("Sub Purchase Order Item Grid",           "{27A93B3D-4E30-4854-88C0-292783E778B3}", lv1menuGrids.Id, "SubPurchaseOrderItemGridController");
            AddGrid("Transfer Order Item Grid",               "{521D83C6-DEA4-4723-A7F3-25C00F940B75}", lv1menuGrids.Id, "TransferOrderItemGridController");
            AddGrid("Vehicle Make Model",                     "{C10EC66E-AA26-4BF6-93BF-35307715FE44}", lv1menuGrids.Id, "VehicleMakeModelGridController");
            AddGrid("Vendor",                                 "{BA43D0E0-119D-495B-B066-8E5E738CFC4C}", lv1menuGrids.Id, "VendorGridController");
            AddGrid("Vendor Invoice Item",                    "{C4887310-2572-458F-9691-1ABECB622862}", lv1menuGrids.Id, "VendorInvoiceItemGridController");
            AddGrid("Vendor Invoice Note",                    "{D9DBA1D1-65E7-4CE5-99D0-6C79144DECAD}", lv1menuGrids.Id, "VendorInvoiceNoteGridController");
            AddGrid("Vendor Invoice Payment",                 "{8B63442E-DE46-47BD-B995-342B2A49E77E}", lv1menuGrids.Id, "VendorInvoicePaymentGridController");
            AddGrid("Vendor Invoice Status History",          "{4CCDDE3F-57CD-43B4-88F0-F8B59AF104F9}", lv1menuGrids.Id, "VendorInvoiceStatusHistoryGridController");
            AddGrid("Vendor Note",                            "{60704925-2642-4864-A5E8-272313978CE3}", lv1menuGrids.Id, "VendorNoteGridController");
            AddGrid("Vehicle Type Warehouse",                 "{51707760-645D-452C-A545-37A4C861B139}", lv1menuGrids.Id, "VehicleTypeWarehouseGridController");
            AddGrid("Wardrobe Inventory Color",               "{ED2BCE54-1255-4B65-976B-B24A6573F176}", lv1menuGrids.Id, "WardrobeInventoryColorGridController");
            AddGrid("Wardrobe Inventory Material",            "{8BE5E66E-35B8-444F-9F9A-E03F4667F67A}", lv1menuGrids.Id, "WardrobeInventoryMaterialGridController");
            AddGrid("Warehouse",                              "{EF27A7FE-26D8-4F3C-85CD-9CD2D6FE57A5}", lv1menuGrids.Id, "WarehouseGridController");
            AddGrid("Warehouse Department",                   "{CB4CE3A5-6DCC-497D-84D1-0B3FBAAEB19B}", lv1menuGrids.Id, "WarehouseDepartmentGridController");
            AddGrid("Warehouse Inventory Type",               "{D90C2659-F1FB-419D-89B6-738766DFCAD2}", lv1menuGrids.Id, "WarehouseInventoryTypeGridController");
            AddGrid("Warehouse Department User",              "{4B3FB84E-CC4D-4EAE-917A-1291B733AC89}", lv1menuGrids.Id, "WarehouseDepartmentUserGridController");
            AddGrid("Warehouse Availability Hour",            "{DF40BE8D-BAAA-45E8-A6AE-78057281C1EC}", lv1menuGrids.Id, "WarehouseAvailabilityHourGridController");
            AddGrid("Warehouse Office Location",              "{99C692AB-13CE-4113-88CF-6AC15821B9D4}", lv1menuGrids.Id, "WarehouseOfficeLocationGridController");
            AddGrid("Warehouse QuikLocate Approver",          "{597134F6-303E-4B69-A9B7-403082295AE1}", lv1menuGrids.Id, "WarehouseQuikLocateApproverGridController");
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
            AddController("BillingCycle",           "{4CE12A3E-1831-4D35-95AF-01F1BF0A5214}", lv1menuSettings.Id);
            AddController("BillPeriod",             "{39AC96F1-2834-491C-BCDE-92CD8E2B11AA}", lv1menuSettings.Id);
            AddController("BlackoutStatus",         "{DCA313A2-92A3-41CA-B203-38BE725743A8}", lv1menuSettings.Id);
            AddController("ContactEvent",           "{CC37FF90-094F-4A5D-B4D4-3552B5B8D65A}", lv1menuSettings.Id);
            AddController("ContactTitle",           "{D756CEE2-AE67-48BD-88A7-FFE855A8E590}", lv1menuSettings.Id);
            AddController("Country",                "{251CEBD0-5EF5-4025-B3FF-84840F0D2525}", lv1menuSettings.Id);
            AddController("CreditStatus",           "{CD2569F2-6BEB-488C-AB9F-3A6C6D821ED3}", lv1menuSettings.Id);
            AddController("Currency",               "{04D83434-3298-4187-8327-84942CF1CB03}", lv1menuSettings.Id);
            AddController("CustomerCategory",       "{545C1F2F-BB9C-422C-8E41-A69DAF9DAC9E}", lv1menuSettings.Id);
            AddController("CustomerStatus",         "{D9FEADB4-F079-4437-834C-4D8F5C0ACFA5}", lv1menuSettings.Id);
            AddController("CustomerType",           "{8B861232-B0CC-4CDA-AD86-4B72AA721B73}", lv1menuSettings.Id);
            AddController("DealClassification",     "{3CABAC95-7BEC-433F-9D32-11E76AAE229A}", lv1menuSettings.Id);
            AddController("DealStatus",             "{01C4EC0C-F4C0-4E8C-A079-D2CA37A5EDA1}", lv1menuSettings.Id);
            AddController("DealType",               "{44BFDDB3-D93B-4A99-A38F-A0CE8162413F}", lv1menuSettings.Id);
            AddController("Department",             "{0DF9DBC3-8755-460C-B830-FF169CC3D859}", lv1menuSettings.Id);
            AddController("DocumentType",           "{343FD936-D5A3-420A-8B72-8A10248C39E4}", lv1menuSettings.Id);
            AddController("EventCategory",          "{7D659770-FEC6-4580-A93B-29C766B9DA4B}", lv1menuSettings.Id);
            AddController("FacilityScheduleStatus", "{3249CECE-FCDC-4B51-96B6-13609CB9F642}", lv1menuSettings.Id);
            AddController("FacilityStatus",         "{61C5A81A-AAB3-43C7-9239-4E08CE8F3F34}", lv1menuSettings.Id);
            AddController("GeneratorFuelType",      "{FC2DA3CA-B5CC-4490-8D20-C121A1D3F7D6}", lv1menuSettings.Id);
            AddController("GeneratorRating",        "{03C0E24C-BB02-4924-A7B2-8B1DDF171827}", lv1menuSettings.Id);
            AddController("GLAccount",              "{04730651-F4CD-47BC-ABAD-3B0B0F929AFD}", lv1menuSettings.Id);
            AddController("MailList",               "{1ED190A8-051D-46C9-823E-825021CCCDBE}", lv1menuSettings.Id);
            AddController("OfficeLocation",         "{C78B7368-C42E-4546-83CC-B7B6A1006FF0}", lv1menuSettings.Id);
            AddController("OrganizationType",       "{850B2B9F-A3A8-4162-809D-DFBD52262B76}", lv1menuSettings.Id);
            AddController("PaymentTerms",           "{E37D94D8-FA1F-41F2-A0E5-8562ED76F015}", lv1menuSettings.Id);
            AddController("PaymentType",            "{326750A2-B9DA-4245-9D44-485724CD6669}", lv1menuSettings.Id);
            AddController("PersonnelType",          "{874B18DE-2BA6-4427-9D20-54F64072C003}", lv1menuSettings.Id);
            AddController("PhotographyType",        "{B3FCE468-13A0-47F4-A1A4-E16D3264E544}", lv1menuSettings.Id);
            AddController("PoClassification",       "{65EE7A12-3879-4B4A-892E-AE1C204599F8}", lv1menuSettings.Id);
            AddController("ProductionType",         "{309300EC-4CCB-421A-BAF6-64005D2C9DE9}", lv1menuSettings.Id);
            AddController("Region",                 "{105E6E96-475E-4747-92C9-164F2E8F2DD7}", lv1menuSettings.Id);
            AddController("ScheduleType",           "{06DA0B03-A6B0-4719-B2AB-9482991F5867}", lv1menuSettings.Id);
            AddController("State",                  "{053B9AE1-160F-4FC7-AEEC-A5669E8879D6}", lv1menuSettings.Id);
            AddController("VendorClass",            "{61ABD143-5A80-424F-998A-A264F5806709}", lv1menuSettings.Id);
            AddController("Warehouse",              "{997CF52E-BA31-44BA-A3A3-D8684FFFB15B}", lv1menuSettings.Id);
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
            //AddModule("Exchange",            "{76A62932-CBBA-403E-8BF2-0C2283BBAD8D}", lv1menuHome.Id, "", "utilities/exchange",              iconbaseurl + "exchange.001.png",         "",                 "",              "USER");
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
            AddModule("Physical Inventory",  "{36A96F73-AAF1-465B-9A60-34F1160AEDAD}", lv1menuHome.Id, "", "physicalinventory",               iconbaseurl + "physicalinv.png",          "",                 "",              "USER");
            AddModule("Move To Aisle/Shelf", "{80B6DE16-DE6E-4D49-869F-DA14BCE3422E}", lv1menuHome.Id, "", "inventory/movebclocation",        iconbaseurl + "moveto.png",               "",                 "",              "USER");
            AddModule("Assign Items",        "{0383B8A9-EB64-4C8A-B6F2-9E3528C093DB}", lv1menuHome.Id, "", "assignitems",                     iconbaseurl + "assignitems.001.png",      "",                 "",              "USER");
            AddModule("Barcode Label",       "{05B4FAF1-9329-43E9-9697-BE461E41D85F}", lv1menuHome.Id, "", "barcodelabel",                    iconbaseurl + "barcodelabel.001.png",     "",                 "",              "USER");
        }
        //---------------------------------------------------------------------------------------------
        private void BuildTrakitWorksWebTree(FwSecurityTreeNode system)
        {
            string homeiconbaseurl          = "theme/images/icons/home/";
            //string settingsiconbaseurl      = "theme/images/icons/settings/";
            //string reportsiconbaseurl       = "theme/images/icons/reports/";
            string utilitiesiconbaseurl     = "theme/images/icons/utilities/";
            string administratoriconbaseurl = "theme/images/icons/administrator/";

            var application          = AddApplication("TrakitWorks Web",   "{D901DE93-EC22-45A1-BB4A-DD282CAF59FB}", system.Id);
            var lv1menuModules       = AddLv1ModuleMenu("TrakitWorks",     "{B05953D7-DC85-486C-B9A4-7743875DFABC}", application.Id);
            var lv1menuSettings      = AddLv1ModuleMenu("Settings",        "{CA7EDF90-F08A-4E5C-BA6B-87DB6A14D485}", application.Id);
            var lv1menuReports       = AddLv1ModuleMenu("Reports",         "{F62D2B01-E4C4-4E97-BFAB-6CF2B872A4E4}", application.Id);
            var lv1menuUtilities     = AddLv1ModuleMenu("Utilities",       "{293A157D-EA8E-48F6-AE97-15F9DE53041A}", application.Id);
            var lv1menuAdministrator = AddLv1ModuleMenu("Administrator",   "{A3EE3EE9-4C98-4315-B08D-2FAD67C04E07}", application.Id);
            var lv1menuSubModules    = AddLv1SubModulesMenu("Sub-Modules", "{F866CB47-7045-480A-8BBD-0615FD0FC41C}", application.Id);
            var lv1menuGrids         = AddLv1GridsMenu("Grids",            "{154C8BA7-275E-4DDB-B019-0C8633FA8B59}", application.Id);

            //TrakitWorks
            AddModule("Asset",                  "{E1366299-0008-429C-93CC-B8ED8969B180}", lv1menuModules.Id, "AssetController",             "module/asset",             homeiconbaseurl + "placeholder.png");
            AddModule("Assign Barcodes",        "{81B0D93C-9765-4340-8B40-63040E0343B8}", lv1menuModules.Id, "AssignBarcodesController",    "module/assignbarcodes",    homeiconbaseurl + "placeholder.png");
            AddModule("Check-In",               "{3D1EB9C4-95E2-440C-A3EF-10927C4BDC65}", lv1menuModules.Id, "CheckInController",           "module/checkin",           homeiconbaseurl + "placeholder.png");
            AddModule("Contact",                "{9DC167B7-3313-4783-8A97-03C55B6AD5F2}", lv1menuModules.Id, "ContactController",           "module/contact",           homeiconbaseurl + "placeholder.png");
            AddModule("Create Pick List",       "{1407A536-B5C9-4363-8B54-A56DB8CE902D}", null,              "CreatePickListController",    "module/createpicklist",    homeiconbaseurl + "placeholder.png");
            AddModule("Contract",               "{F6D42CC1-FAC6-49A9-9BF2-F370FE408F7B}", lv1menuModules.Id, "ContractController",          "module/contract",          homeiconbaseurl + "placeholder.png");
            AddModule("Customer",               "{8237418B-923D-4044-951F-98938C1EC3DE}", lv1menuModules.Id, "CustomerController",          "module/customer",          homeiconbaseurl + "placeholder.png");
            AddModule("Deal",                   "{393DE600-2911-4753-85FD-ABBC4F0B1407}", lv1menuModules.Id, "DealController",              "module/deal",              homeiconbaseurl + "placeholder.png");
            AddModule("Exchange",               "{F9012ABC-B97E-433B-A604-F1DADFD6D7B7}", lv1menuModules.Id, "ExchangeController",          "module/exchange",          homeiconbaseurl + "placeholder.png");
            AddModule("Order",                  "{68B3710E-FE07-4461-9EFD-04E0DBDAF5EA}", lv1menuModules.Id, "OrderController",             "module/order",             homeiconbaseurl + "placeholder.png");
            AddModule("Order Status",           "{7BB8BB8C-8041-41F6-A2FA-E9FA107FF5ED}", lv1menuModules.Id, "OrderStatusController",       "module/orderstatus",       homeiconbaseurl + "placeholder.png");
            AddModule("Pick List",              "{744B371E-5478-42F9-9852-E143A1EC5DDA}", lv1menuModules.Id, "PickListController",          "module/picklist",          homeiconbaseurl + "placeholder.png");
            AddModule("Purchase Order",         "{DA900327-CEAC-4CB0-9911-CAA2C67059C2}", lv1menuModules.Id, "PurchaseOrderController",     "module/purchaseorder",     homeiconbaseurl + "placeholder.png");
            AddModule("Quote",                  "{9213AF53-6829-4276-9DF9-9DAA704C2CCF}", lv1menuModules.Id, "QuoteController",             "module/quote",             homeiconbaseurl + "placeholder.png");
            AddModule("Receive From Vendor",    "{EC4052D5-664E-4C34-8802-78E086920628}", lv1menuModules.Id, "ReceiveFromVendorController", "module/receivefromvendor", homeiconbaseurl + "placeholder.png");
            AddModule("Inventory Item",         "{803A2616-4DB6-4BAC-8845-ECAD34C369A8}", lv1menuModules.Id, "InventoryItemController",     "module/inventoryitem",     homeiconbaseurl + "placeholder.png");
            AddModule("Repair Order",           "{D567EC42-E74C-47AB-9CA8-764DC0F02D3B}", lv1menuModules.Id, "RepairController",            "module/repair",            homeiconbaseurl + "placeholder.png");
            AddModule("Return To Vendor",       "{79EAD1AF-3206-42F2-A62B-DA1C44092A7F}", lv1menuModules.Id, "ReturnToVendorController",    "module/returntovendor",    homeiconbaseurl + "placeholder.png");
            AddModule("Staging / Check-Out",    "{AD92E203-C893-4EB9-8CA7-F240DA855827}", lv1menuModules.Id, "StagingCheckoutController",   "module/checkout",          homeiconbaseurl + "placeholder.png");
            AddModule("Vendor",                 "{92E6B1BE-C9E1-46BD-91A0-DF257A5F909A}", lv1menuModules.Id, "VendorController",            "module/vendor",            homeiconbaseurl + "placeholder.png");

            // Utilities
            AddModule("Dashboard",                     "{E01F0032-CFAA-4556-9F24-E4C28C5B50A1}", lv1menuUtilities.Id, "DashboardController",                    "module/dashboard",                 utilitiesiconbaseurl + "placeholder.png");
            AddModule("Dashboard Settings",            "{AD262A8E-A487-4786-895D-6E3DA1DB13BD}", lv1menuUtilities.Id, "DashboardSettingsController",            "module/dashboardsettings",         utilitiesiconbaseurl + "placeholder.png");

            // Administrator
            AddModule("Group",                  "{849D2706-72EC-48C0-B41C-0890297BF24B}", lv1menuAdministrator.Id, "GroupController",       "module/group",             administratoriconbaseurl + "group.png", "USER");
            AddModule("User",                   "{CE9E187C-288F-44AB-A54A-27A8CFF6FF53}", lv1menuAdministrator.Id, "UserController",        "module/user",              administratoriconbaseurl + "user.png",                                     "USER");
           
            //// Add Submodules
            AddSubModule("User Settings",   "{2563927C-8D51-43C4-9243-6F69A52E2657}", lv1menuSubModules.Id, "UserSettingsController");
            AddSubModule("SubWorksheet",    "{2227B6C3-587D-48B1-98B6-B9125E0E4D9D}", lv1menuSubModules.Id, "SubWorksheetController");

            // Add Grids
            AddGrid("Audit History",                          "{977B65BB-DD67-4B5E-9B62-944E5DBECFD4}", lv1menuGrids.Id, "AuditHistoryGridController");
            AddGrid("Check-In Exception",                     "{81282AE6-63B5-4A97-A066-0592CE276D58}", lv1menuGrids.Id, "CheckInExceptionGridController");
            AddGrid("Checked-In Item",                        "{85778C01-2ACC-4ADF-97EC-7386E6F32415}", lv1menuGrids.Id, "CheckedInItemGridController");
            AddGrid("Checked-Out Item",                       "{A65E700E-486D-41DE-BBE1-485FCBF1E5A3}", lv1menuGrids.Id, "CheckedOutItemGridController");
            AddGrid("Check-In Order",                         "{C1752A81-400D-46F7-82BB-0B1CCD78C890}", lv1menuGrids.Id, "CheckInOrderGridController");
            AddGrid("Check-In Quantity Items",                "{457BBDD6-B4B4-4671-B651-728A6ABF2BF0}", lv1menuGrids.Id, "CheckInQuantityItemsGridController");
            AddGrid("Check-In Swap",                          "{CDEEA7D1-3738-4BD6-BBDC-75BD044DFE56}", lv1menuGrids.Id, "CheckInSwapGridController");
            AddGrid("Pending Items",                          "{560D1917-64B0-445D-9101-EED6D7C45811}", lv1menuGrids.Id, "CheckOutPendingItemGridController");
            AddGrid("Company Contact",                        "{6D8B3D23-0954-4765-9FBD-BF3EC756AA97}", lv1menuGrids.Id, "CompanyContactGridController");
            AddGrid("Contact Note",                           "{A6BEBACB-24AB-4A5A-9F65-7EF11BF49691}", lv1menuGrids.Id, "ContactNoteGridController");
            AddGrid("Contact Personal Event",                 "{EBEE1B5E-727D-4262-B045-906EC349A259}", lv1menuGrids.Id, "ContactPersonalEventGridController");
            AddGrid("Container Warehouse",                    "{F9766AB6-E3BC-4F3E-9394-9D28BF8C984B}", lv1menuGrids.Id, "ContainerWarehouseGridController");
            AddGrid("Contract Detail",                        "{A48C1102-249A-43B1-95E9-97A1DAEEE92C}", lv1menuGrids.Id, "ContractDetailGridController");
            AddGrid("Contract Exchange Item",                 "{02007E1B-9ED2-43E3-BDAA-3A1EA4A5ABFD}", lv1menuGrids.Id, "ContractExchangeItemGridController");
            AddGrid("Contract Summary",                       "{9CE13261-0A5D-4B21-BC4A-3E6A18E80492}", lv1menuGrids.Id, "ContractSummaryGridController");
            AddGrid("Deal Notes",                             "{CA1FA1E1-2BE8-473D-ABC3-24D741D2AD8E}", lv1menuGrids.Id, "DealNoteGridController");
            AddGrid("Deal Shipper",                           "{DC46B97F-6664-4ED6-8D1C-6E0EA8B3BC38}", lv1menuGrids.Id, "DealShipperGridController");
            AddGrid("Exchange Item",                          "{702D2298-6C34-4938-B97E-00ABACA7CA5C}", lv1menuGrids.Id, "ExchangeItemGridController");
            AddGrid("Item Attribute Value",                   "{122AC4CB-831E-4F20-BC83-F12AD96094DE}", lv1menuGrids.Id, "ItemAttributeValueGridController");
            AddGrid("Inventory Attribute Value",              "{ECEB623B-C84C-4D55-AE86-8E067E119244}", lv1menuGrids.Id, "InventoryAttributeValueGridController");
            AddGrid("Inventory Availability",                 "{548B5500-E8BD-4448-8B4C-2389D54DD803}", lv1menuGrids.Id, "InventoryAvailabilityGridController");
            AddGrid("Inventory Compatibility",                "{AD2D5238-317D-4FDC-950E-E935293CE2F8}", lv1menuGrids.Id, "InventoryCompatibilityGridController");
            AddGrid("Inventory Complete Kit",                 "{9F38BB28-4133-4291-91B8-3F234B5DB437}", lv1menuGrids.Id, "InventoryCompleteKitGridController");
            AddGrid("Inventory Consignment",                  "{2DC8659F-EB2B-43C3-BAD1-0769EB14351F}", lv1menuGrids.Id, "InventoryConsignmentGridController");
            AddGrid("Inventory QC",                           "{8A0A4336-2124-4274-BF9F-ED2CE8CFFE54}", lv1menuGrids.Id, "InventoryQcGridController");
            AddGrid("Inventory Substitute",                   "{AAF0CDF9-30DC-4A7A-883C-5363F694D843}", lv1menuGrids.Id, "InventorySubstituteGridController");
            AddGrid("Inventory Vendor",                       "{EFE6D013-E84D-4EF8-A19F-571B5A1353FE}", lv1menuGrids.Id, "InventoryVendorGridController");
            AddGrid("Inventory Warehouse Staging",            "{5D55C05C-3094-4656-B199-3DA137E5D311}", lv1menuGrids.Id, "InventoryWarehouseStagingGridController");
            AddGrid("Item QC",                                "{63A92198-F5D0-4A9C-AAF5-08BF052A1CAA}", lv1menuGrids.Id, "ItemQcGridController");
            AddGrid("Order Contact",                          "{0D80C755-0538-461D-A6E6-3A92D17217F2}", lv1menuGrids.Id, "OrderContactGridController");
            AddGrid("Order Item",                             "{E17AD193-28FB-4B92-BE62-B04AFC8C8A07}", lv1menuGrids.Id, "OrderItemGridController");
            AddGrid("Order Note",                             "{929AF93E-F07D-4E78-8FDD-1F0FEC90D9A4}", lv1menuGrids.Id, "OrderNoteGridController"); 
            AddGrid("Order Status History",                   "{A3683C2F-A5B4-42FC-A944-DAA65ED71E87}", lv1menuGrids.Id, "OrderStatusHistoryGridController");
            AddGrid("Order Status Rental Detail",             "{62B225DE-33AA-4EC1-88DE-C945AB6ECB0F}", lv1menuGrids.Id, "OrderStatusRentalDetailGridController");
            AddGrid("Order Status Sales Detail",              "{5B13583B-503D-4F1F-AA67-BDBE3B0AB59C}", lv1menuGrids.Id, "OrderStatusSalesDetailGridController");
            AddGrid("Order Status Summary",                   "{B155DFBC-F429-4A90-82C3-C26B4AF61E86}", lv1menuGrids.Id, "OrderStatusSummaryGridController");
            AddGrid("Pick List Item",                         "{FA382FA5-C187-481D-8A8D-4755C12D4936}", lv1menuGrids.Id, "PickListItemGridController");
            AddGrid("PO Receive Barcode",                     "{6781A0C0-14FB-4B3B-970F-EF9FC812E835}", lv1menuGrids.Id, "POReceiveBarCodeGridController");
            AddGrid("PO Receive Items",                       "{22FFEC79-6500-4C85-B446-E111C8CDAC7C}", lv1menuGrids.Id, "POReceiveItemGridController");
            AddGrid("PO Return Bar Code",                     "{4A0DBA79-DCA2-4A57-AF72-FB49A350777C}", lv1menuGrids.Id, "POReturnBarCodeGridController");
            AddGrid("PO Return Items",                        "{B5D69725-CB16-4E12-9457-1B6EB999FD41}", lv1menuGrids.Id, "POReturnItemGridController");
            AddGrid("Rental Inventory Warehouse",             "{9AC6FB16-BC42-42C3-91B7-9346D11CC405}", lv1menuGrids.Id, "RentalInventoryWarehouseGridController");
            AddGrid("Repair Release",                         "{33DACEA3-20F0-4D7F-A0E3-C2D21BF60BAC}", lv1menuGrids.Id, "RepairReleaseGridController");
            AddGrid("Staged Items",                           "{2A719A9F-F237-4C3A-92DF-FBC515204A38}", lv1menuGrids.Id, "StagedItemGridController");
            AddGrid("Stage Holding Items",                    "{1F06BAB4-5D64-43FC-B2A8-FF088064E4A0}", lv1menuGrids.Id, "StageHoldingItemGridController");
            AddGrid("Stage Quantity Items",                   "{162DCF5B-759A-42E9-82E9-88B628B6901D}", lv1menuGrids.Id, "StageQuantityItemGridController");
        }
        //---------------------------------------------------------------------------------------------
    }
}
