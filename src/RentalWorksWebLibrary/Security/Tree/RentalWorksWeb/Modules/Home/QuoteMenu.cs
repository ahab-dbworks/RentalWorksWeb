using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class QuoteMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public QuoteMenu() : base("{4D785844-BE8A-4C00-B1FA-2AA5B05183E5}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{6299C2C3-E2A7-44C0-93A6-01299D491C54}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{23A50442-DD0F-4DF8-80CB-03E63182343B}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{C515CB6E-C91C-4413-B9D2-24210624A298}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{0A9DCF82-55C7-42B6-A99A-E6CF471F3E72}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{5CD8864B-6492-44BD-89FC-269F01EB7EC1}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{5E99B26E-5A26-4AD1-99D1-C8F0E4436E4C}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{9EA08E72-5FF9-48D5-AB92-12DA5F307D8D}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{EA5AE8E5-9837-43E5-B2E0-3677ADD8D6D9}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{D517DC5B-05BF-4493-B6D3-5FD1E89A26B7}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{3AD49F0C-7C98-4A57-BA81-88AC928675AC}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{11D2F84B-E8A9-4A3A-9A41-A1E7672F0A40}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{031B239C-8149-4029-9A7A-70CE95431719}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{CF47C42C-D9CE-442E-8C75-1F61CCE89CAB}", nodeFormMenuBar.Id);
        }

        //---------------------------------------------------------------------------------------------
    }
}