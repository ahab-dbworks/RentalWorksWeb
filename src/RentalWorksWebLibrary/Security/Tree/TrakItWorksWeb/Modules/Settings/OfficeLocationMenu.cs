using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class OfficeLocationMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OfficeLocationMenu() : base("{D561C701-BF89-4FE4-B7F4-912AB0B6F7A2}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{879934E9-4A75-48BF-B7B1-94E643B52B72}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{D8B57698-0555-44F9-BBFB-773F42CFD26C}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{7172C956-3F63-4B35-A9EA-3AD358324872}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{E6CC312A-6C28-49C4-AA3F-9C304981005B}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{1BAFE56B-F565-4924-84B8-419505A68853}", nodeBrowseExport.Id);
            //tree.AddNewMenuBarButton("{0A52DF13-0210-45CB-9F11-5CDF6A92AB11}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{B3B85912-E1C7-4A20-A999-7A92EE051117}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{E5E53231-7250-4B4E-B843-38010F17B1E7}", nodeBrowseMenuBar.Id);
            //tree.AddDeleteMenuBarButton("{8EDF9354-B1AD-4334-A8D8-AF2C7FCE95C2}", nodeBrowseMenuBar.Id);


            // Form
            var nodeForm = tree.AddForm("{6CC96696-49B4-42EE-BED3-9485A7F9D41E}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{71BC2ECF-D808-4E47-98E1-8B7EFE9F42C6}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{C36C36B6-B741-4BD9-863A-350C380D9FEC}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{B4DA704F-59D9-4758-9F65-6FC003237808}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}


    


