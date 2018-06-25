using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class ProjectMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ProjectMenu() : base("{C6C8167A-C3B5-4915-8290-4520AF7EDB35}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{653CA61D-9F2E-44FB-8101-325BC7BCE5DB}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{4EB8543D-3066-493D-839B-758F7CF8BA61}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{71CFD348-F630-4423-A5F5-006B2E9590CD}", nodeBrowseMenuBar.Id);
            tree.AddNewMenuBarButton("{2476F127-ECA5-413A-A527-4FC7564C1177}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{5F6C501D-F157-4267-AD90-47EE9A3D9777}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{BF673CDF-7E5D-4750-B8BD-52DFE8722837}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{78816966-D5AB-404C-BBA8-26DFF78384AC}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{F1DD9999-83A8-49B2-99EC-5C1457AE880E}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{BC94C13D-9EA3-45C8-945A-E9F525663BA7}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{800A9968-D864-4D3C-B84C-2E19440594C8}", nodeFormSubMenu.Id);
            tree.AddSubMenuItem("Create Quote", "{92B78408-298F-431C-A535-2ADC7C4DD2F7}", nodeFormOptions.Id);
            tree.AddSaveMenuBarButton("{C09D0277-7A7C-4DB7-B197-CD7C5C02A785}", nodeFormMenuBar.Id);
        }

        //---------------------------------------------------------------------------------------------
    }
}