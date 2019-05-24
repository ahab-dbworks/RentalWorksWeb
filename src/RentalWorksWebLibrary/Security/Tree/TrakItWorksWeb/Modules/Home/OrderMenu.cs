using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class OrderMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderMenu() : base("{68B3710E-FE07-4461-9EFD-04E0DBDAF5EA}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{711DE7B1-084B-4B97-BA21-FC75BCDC0BF5}", MODULEID);
                var nodeBrowseMenuBar = tree.AddMenuBar("{9E06F136-E937-4F4F-804B-124F5F9F826B}", nodeBrowse.Id);
                    var nodeBrowseSubMenu = tree.AddSubMenu("{FD80BA88-27C8-4BCD-9503-E935C1CF0C7E}", nodeBrowseMenuBar.Id);
                        var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{D8DBA19B-BEB8-436B-B1D5-E23E404D11DF}", nodeBrowseSubMenu.Id);
                            tree.AddSubMenuItem("Cancel / Uncancel", "{CCD05127-481F-4352-A706-FEC6575DBEAF}", nodeBrowseOptions.Id);
                        var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{82686D42-E42C-4B6B-B913-CD7455670352}", nodeBrowseSubMenu.Id);
                            tree.AddDownloadExcelSubMenuItem("{05184C2F-3D49-497F-A6AF-6F48975CB032}", nodeBrowseExport.Id);
                    tree.AddNewMenuBarButton("{D0763467-CE4E-4F93-90F0-C2AD2E2D8366}", nodeBrowseMenuBar.Id);
                    tree.AddViewMenuBarButton("{DE20C380-86A6-4ADB-9917-E7B825215AC5}", nodeBrowseMenuBar.Id);
                    tree.AddEditMenuBarButton("{8A14B657-B25C-4B17-8881-2CF870ABF816}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{DEA009DA-B0AC-47EE-B478-9E1C2E32BEBA}", MODULEID);
                var nodeFormMenuBar = tree.AddMenuBar("{238BC8F6-51CC-46A0-8A83-6A18384D8DA8}", nodeForm.Id);
                    var nodeFormSubMenu = tree.AddSubMenu("{5B1D16AD-9D33-45C6-BBED-2A5F1F14524E}", nodeFormMenuBar.Id);
                        var nodeFormOptions = tree.AddSubMenuGroup("Options", "{988E1B38-3B40-4F0F-B313-C65057F4780B}", nodeFormSubMenu.Id);
                            //tree.AddSubMenuItem("Search Inventory", "{0C8F88D0-F945-4B95-9E91-8704B2D04C30}", nodeFormOptions.Id);
                            tree.AddSubMenuItem("Copy Order", "{FFD9C063-FCF6-4A14-846D-4BD2887CF523}", nodeFormOptions.Id);
                            tree.AddSubMenuItem("Print Order", "{B2A04C34-45BF-440E-B588-DD070CD65E59}", nodeFormOptions.Id);
                            tree.AddSubMenuItem("Create Pick List", "{223FC05F-ABE5-4427-A459-CC66336400EC}", nodeFormOptions.Id);
                            //tree.AddSubMenuItem("Create Snapshot", "{EC3921FE-3BF7-48E7-AD2F-EEB9BE8F9795}", nodeFormOptions.Id);
                            //tree.AddSubMenuItem("View Snapshot", "{D68D49D7-9F04-4846-8A0C-C81F619D4942}", nodeFormOptions.Id);
                            tree.AddSubMenuItem("Cancel / Uncancel", "{127B392C-EF2C-4684-AE59-5A8B0ED6B518}", nodeFormOptions.Id);
                            tree.AddSubMenuItem("Order Status", "{ECFE0CE4-3424-44EB-B213-29409CE3D595}", nodeFormOptions.Id);
                            tree.AddSubMenuItem("Check-Out", "{3C9AF5C2-F7FB-44C8-B3B9-FF09F40CC58F}", nodeFormOptions.Id);
                            tree.AddSubMenuItem("Check-In", "{790B9193-AAFC-4EEE-9D5E-34D1F8DDD603}", nodeFormOptions.Id);
                    tree.AddSaveMenuBarButton("{2DBD47A8-6BC8-44B4-8D7A-B43F46B676E7}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}