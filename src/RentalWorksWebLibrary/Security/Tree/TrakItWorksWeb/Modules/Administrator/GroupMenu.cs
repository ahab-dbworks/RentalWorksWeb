using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitworkWeb.Modules.Administrator
{
    public class GroupMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public GroupMenu() : base("{849D2706-72EC-48C0-B41C-0890297BF24B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{997305C6-A36D-4202-B846-520025244B2D}", MODULEID);
                var nodeBrowseMenuBar = tree.AddMenuBar("{7AE244E4-6CD4-4D2D-AC2F-8C61174CFDE4}", nodeBrowse.Id);
                    var nodeBrowseSubMenu = tree.AddSubMenu("{A4780F08-C00E-4897-8E2D-FD5C8EE55461}", nodeBrowseMenuBar.Id);
                        var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{3271EAEC-49CB-468D-8752-FEF85BB26E92}", nodeBrowseSubMenu.Id);
                            //tree.AddSubMenuItem("Import Security Tree...", "{F9267DCE-C46B-4576-9ED7-F4889DB438E4}", nodeBrowseExport.Id);
                            //tree.AddSubMenuItem("Export Security Tree...", "{F43CB2CC-4771-431C-8871-B5D95B274B35}", nodeBrowseExport.Id);
                            tree.AddDownloadExcelSubMenuItem("{56153B32-40FB-416E-A526-384264EDD7DC}", nodeBrowseExport.Id);
                    tree.AddNewMenuBarButton("{191AA7F9-818C-4C8A-A1B9-69D0008E0D95}",    nodeBrowseMenuBar.Id);
                    tree.AddViewMenuBarButton("{92A976AE-72BD-4E1C-B3B1-A6492533A99F}",   nodeBrowseMenuBar.Id);
                    tree.AddEditMenuBarButton("{3F6AAF13-80AE-4DF3-B952-7BF2BBFA9B2F}",   nodeBrowseMenuBar.Id);
                    tree.AddDeleteMenuBarButton("{AC132678-242F-4FAE-B587-4B8ACACEFCC6}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{D1B44191-B81D-47D4-9FC6-6859CFEC2ECD}", MODULEID);
                var nodeFormMenuBar = tree.AddMenuBar("{0DC287F6-B186-4C4B-B2F0-B613A2B03D4A}", nodeForm.Id);
                    tree.AddSaveMenuBarButton("{90988257-2F8D-48EF-AF64-535FB2C05F45}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
