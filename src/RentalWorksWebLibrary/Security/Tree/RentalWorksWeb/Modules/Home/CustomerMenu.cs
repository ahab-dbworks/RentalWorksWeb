using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class CustomerMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CustomerMenu() : base("{214C6242-AA91-4498-A4CC-E0F3DCCCE71E}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{C61954C9-C424-4255-A696-000D14BE15DC}", MODULEID);
                var nodeBrowseMenuBar = tree.AddMenuBar("{00EE9896-0B9B-4664-B760-9B6F318B79D0}", nodeBrowse.Id);
                    var nodeBrowseSubMenu = tree.AddSubMenu("{D27F0107-1B53-44E6-8C44-F2AE859D4A4B}", nodeBrowseMenuBar.Id);
                        var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{0EB2DE1D-A2E3-4BF6-BD33-5F46E7293EAB}", nodeBrowseSubMenu.Id);
                            tree.AddDownloadExcelSubMenuItem("{700B9DF1-6C0B-4515-B406-A830BEDCCF5A}", nodeBrowseExport.Id);
                    tree.AddNewMenuBarButton("{0D0687D5-1C1C-4906-82A8-F452AEDBA90E}",    nodeBrowseMenuBar.Id);
                    tree.AddViewMenuBarButton("{13DC9546-6EE9-4242-94D6-28CF1424B792}",   nodeBrowseMenuBar.Id);
                    tree.AddEditMenuBarButton("{3248F8CF-3474-41A0-979D-55B489A35B60}",   nodeBrowseMenuBar.Id);
                    tree.AddDeleteMenuBarButton("{F79F9AB1-DB44-4A2E-99CA-5829ED0979E9}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{442BD379-10BD-48E1-AF9B-61DCCEC245C7}", MODULEID);
                var nodeFormMenuBar = tree.AddMenuBar("{2AB59332-2BD4-4B9D-868E-E2276EDA368C}", nodeForm.Id);
                    //tree.AddSubMenu("{D3028487-10D8-4447-937A-E157FC20BC41}",           nodeFormMenuBar.Id);
                    tree.AddSaveMenuBarButton("{4617F1BF-FCA7-4C0A-BC20-905200C9AEA4}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}