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
            string settingsiconbaseurl      = "theme/images/icons/settings/";
            string reportsiconbaseurl       = "theme/images/icons/reports/";
            string utilitiesiconbaseurl     = "theme/images/icons/utilities/";
            string administratoriconbaseurl = "theme/images/icons/administrator/";

            var application = AddApplication("RentalWorks Web", "{0A5F2584-D239-480F-8312-7C2B552A30BA}", "{4AC8B3C9-A2C2-4085-8F7F-EE005CCEB535}");
            var lv1menuSettings      = AddLv1ModuleMenu("Settings",        "{730C9659-B33B-493E-8280-76A060A07DCE}", application.Id);
            var lv1menuReports       = AddLv1ModuleMenu("Reports",         "{7FEC9D55-336E-44FE-AE01-96BF7B74074C}", application.Id);
            var lv1menuUtilities     = AddLv1ModuleMenu("Utilities",       "{81609B0E-4B1F-4C13-8BE0-C1948557B82D}", application.Id);
            var lv1menuAdministrator = AddLv1ModuleMenu("Administrator",   "{F188CB01-F627-4DD3-9B91-B6486F0977DC}", application.Id);
            var lv1menuSubModules    = AddLv1SubModulesMenu("Sub-Modules", "{B8E34B04-EB99-4068-AD9E-BDC32D02967A}", application.Id);
            var lv1menuGrids         = AddLv1GridsMenu("Grids",            "{43765919-4291-49DD-BE76-F69AA12B13E8}", application.Id);

            //Settings 
            var lv2menuAccountingMaintenance = AddLv2ModuleMenu("Accounting Maintenance", "{BAF9A442-BA44-4DD1-9119-905C1A8FF199}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                               AddModule("Chart of Accounts", "{F03CA227-99EE-42EF-B615-94540DCB21B3}", lv2menuAccountingMaintenance.Id, "GlAccountController", "module/glaccount", settingsiconbaseurl + "placeholder.png");

            var lv2menuCustomerMaintenance = AddLv2ModuleMenu("Customer Maintenance", "{E2D6AE9E-9131-475A-AB42-0F34356760A6}", lv1menuSettings.Id, settingsiconbaseurl + "placeholder.png");
                                            AddModule("Customer Status", "{B689A0AA-9FCC-450B-AF0F-AD85483531FA}", lv2menuCustomerMaintenance.Id, "CustomerStatusController", "module/customerstatus", settingsiconbaseurl + "placeholder.png");
                                            AddModule("Customer Type", "{314EDC6F-478A-40E2-B17E-349886A85EA0}", lv2menuCustomerMaintenance.Id, "CustomerTypeController", "module/customertype", settingsiconbaseurl + "placeholder.png");                                          

            // Vendor Maintenance
            var lv2menuVendorMaintenance = AddLv2ModuleMenu("Vendor Maintenance", "{93376B75-2771-474A-8C25-2BBE53B50F5C}", lv1menuSettings.Id, settingsiconbaseurl + "placeholer.png");
            AddModule("Vendor Class", "{8B2C9EE3-AE87-483F-A651-8BA633E6C439}", lv2menuVendorMaintenance.Id, "VendorClassController", "module/vendorclass", settingsiconbaseurl + "placeholder.png");

            //Reports 
            var lv2menuDealReports = AddLv2ModuleMenu("Deal Reports",     "{B14EC8FA-15B6-470C-B871-FB83E7C24CB2}", lv1menuReports.Id,                                                              reportsiconbaseurl + "placeholder.png", "Deal Reports");
                                            AddModule("Deal Outstanding", "{007F72D4-8767-472D-9706-8CDE8C8A9981}", lv2menuDealReports.Id, "RwDealOutstandingController", "module/dealoutstanding", reportsiconbaseurl + "placeholder.png", "Deal<br/>Outstanding", "", "");

            // Add Utilities 
            var lv2menuChargeProcessing= AddLv2ModuleMenu("Charge Processing",       "{11349784-B621-468E-B0AD-899A22FCA9AE}", lv1menuUtilities.Id,                                                                                 utilitiesiconbaseurl + "placeholder.png", "Charge Processing");
                                                AddModule("Process Deal Invoices",   "{5DB3FB9C-6F86-4696-867A-9B99AB0D6647}", lv2menuChargeProcessing.Id, "RwChargeProcessingController",        "module/chargeprocessing",        utilitiesiconbaseurl + "placeholder.png", "", "", "");
                                                AddModule("Process Receipts",        "{0BB9B45C-57FA-47E1-BC02-39CEE720792C}", lv2menuChargeProcessing.Id, "RwReceiptProcessingController",       "module/receiptprocessing",       utilitiesiconbaseurl + "placeholder.png", "", "", "");
                                                AddModule("Process Vendor Invoices", "{4FA8A060-F2DF-4E59-8F9D-4A6A62A0D240}", lv2menuChargeProcessing.Id, "RwVendorInvoiceProcessingController", "module/vendorinvoiceprocessing", utilitiesiconbaseurl + "placeholder.png", "", "", "");

            // Add Administrator 
            AddModule("Group",       "{9BE101B6-B406-4253-B2C6-D0571C7E5916}", lv1menuAdministrator.Id, "RwGroupController",       "module/group",       administratoriconbaseurl + "group.png",                                    "USER");
            AddModule("Integration", "{518B038E-F22A-4B23-AA47-F4F56709ADC3}", lv1menuAdministrator.Id, "RwIntegrationController", "module/integration", administratoriconbaseurl + "placeholder.png", "Integration", "quickbooks", "USER");
            AddModule("User",        "{79E93B21-8638-483C-B377-3F4D561F1243}", lv1menuAdministrator.Id, "RwUserController",        "module/user",        administratoriconbaseurl + "user.png",                                     "USER");

            // Add Submodules
            AddSubModule("User Settings", "{A6704904-01E1-4C6B-B75A-C1D3FCB50C01}", lv1menuSubModules.Id, "RwUserSettingsController");

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
            AddModule("Asset Disposition",    "{1573C03C-5ADA-407C-8B7E-D2158920D1CC}", lv1menuHome.Id, "", "inventory/assetdisposition",      iconbaseurl + "assetdisposition.001.png", "",                 "production",    "USER");
            AddModule("Package Truck",       "{2F83A5EC-5423-4520-9B3D-8845C7D5F1B6}", lv1menuHome.Id, "", "order/packagetruck",              iconbaseurl + "package-truck.001.png",    "",                 "packagetruck",  "USER");
            AddModule("QuikPick",            "{1C95FD02-4D0E-4C29-91B7-1166D7690831}", lv1menuHome.Id, "", "quote/quotemenu",                 iconbaseurl + "quikpick.png",             "",                 "",              "USER");
            AddModule("Time Log",            "{0B2BB33B-C463-45C4-9131-05A78CD217F4}", lv1menuHome.Id, "", "timelog",                         iconbaseurl + "timelog.png",              "",                 "crew",          "USER,CREW");
            AddModule("Fill Container",      "{59187AA1-8F90-4AEC-B771-A84EC59A83F1}", lv1menuHome.Id, "", "order/fillcontainer",             iconbaseurl + "fillcontainer.png",        "",                 "container",     "USER");
            AddModule("Inventory Web Image", "{9E49037B-331B-47AC-88C9-C4DE5EABD4DD}", lv1menuHome.Id, "", "inventory/inventorywebimage",     iconbaseurl + "webimage.png",             "",                 "",              "USER");
            AddModule("Physical Inventory",  "{36A96F73-AAF1-465B-9A60-34F1160AEDAD}", lv1menuHome.Id, "", "utilities/physicalinventory",     iconbaseurl + "physicalinv.png",          "",                 "",              "USER");
            AddModule("Move To Aisle/Shelf", "{80B6DE16-DE6E-4D49-869F-DA14BCE3422E}", lv1menuHome.Id, "", "inventory/movebclocation",        iconbaseurl + "moveto.png",               "",                 "",              "USER");
            AddModule("Assign Items",        "{0383B8A9-EB64-4C8A-B6F2-9E3528C093DB}", lv1menuHome.Id, "", "assignitems",                     iconbaseurl + "assignitems.001.png",      "",                 "rfid",          "USER");
            AddModule("Barcode Label",       "{05B4FAF1-9329-43E9-9697-BE461E41D85F}", lv1menuHome.Id, "", "barcodelabel",                    iconbaseurl + "barcodelabel.001.png",     "",                 "",              "USER");
        }
        //---------------------------------------------------------------------------------------------
    }
}