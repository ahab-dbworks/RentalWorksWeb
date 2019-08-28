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
            tree.AddSubMenuItem("Cancel / Uncancel", "{78ACB73C-23DD-46F0-B179-0571BAD3A17D}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{D7A32B86-AD5E-4664-93B9-998022EDFF37}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{9EA08E72-5FF9-48D5-AB92-12DA5F307D8D}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{EA5AE8E5-9837-43E5-B2E0-3677ADD8D6D9}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{3AD49F0C-7C98-4A57-BA81-88AC928675AC}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{11D2F84B-E8A9-4A3A-9A41-A1E7672F0A40}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{031B239C-8149-4029-9A7A-70CE95431719}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{CF47C42C-D9CE-442E-8C75-1F61CCE89CAB}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{4DC0D014-C444-4955-A59A-C29EA9B14E1F}", nodeFormSubMenu.Id);
            tree.AddSubMenuItem("Copy Quote", "{B918C711-32D7-4470-A8E5-B88AB5712863}", nodeFormOptions.Id);
            //tree.AddSubMenuItem("Search Inventory", "{BC3B1A5E-7270-4547-8FD1-4D14F505D452}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Print Quote", "{B20DDE47-A5D7-49A9-B980-8860CADBF7F6}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Create Order", "{E265DFD0-380F-4E8C-BCFD-FA5DCBA4A654}", nodeFormOptions.Id);
            tree.AddSubMenuItem("New Version", "{F79F8C21-66DF-4458-BBEB-E19B2BFCAEAA}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Reserve / Unreserve", "{C122C2C5-9D68-4CDF-86C9-E37CB70C57A0}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Cancel / Uncancel", "{BF633873-8A40-4BD6-8ED8-3EAC27059C84}", nodeFormOptions.Id);
        }

        //---------------------------------------------------------------------------------------------
    }
}