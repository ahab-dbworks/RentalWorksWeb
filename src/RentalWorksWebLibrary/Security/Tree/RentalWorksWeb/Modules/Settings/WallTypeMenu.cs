using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class WallTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WallTypeMenu() : base("{4C9D2D20-D129-461D-9589-ABC896DD9BC6}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{CCAF6BB9-FBA1-4165-AFF3-828B6765FAD3}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{B0AA6965-9DDB-454E-9989-995DBA945E0A}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{61AF3D0D-4BFF-4722-9E04-0CDD69D77768}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{B741C079-A97D-4429-8F4A-E3CADEF07227}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{1C497EC8-442D-4FF1-9D86-333892AE8772}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{7B8468E0-1783-4EAD-B156-93256D190366}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{B89BF873-915C-4D56-B45E-60A3158086D2}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{604CDDFA-F9E6-4E9C-981F-20BD12549D5A}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{44089162-129A-4FE7-A9DE-18C9DFED7B2B}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{829C8ADF-1B6F-46E6-974E-630998C9B7D1}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{B86B6616-A179-4988-B221-1574C1F96411}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{5F8C8911-1B4D-4470-B1D8-78D99727838E}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{D284311D-B3BA-44F0-BD24-958E34AF0B26}", nodeFormMenuBar.Id);
        }

        //---------------------------------------------------------------------------------------------
    }
}