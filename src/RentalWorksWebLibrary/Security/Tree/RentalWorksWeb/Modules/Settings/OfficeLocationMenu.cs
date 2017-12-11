using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class OfficeLocationMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OfficeLocationMenu() : base("{8A8EE5CC-458E-4E4B-BA09-9C514588D3BD}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{AE195001-3C4F-40EF-AB2B-40987FB21CC2}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{3371A7D0-9371-4585-B288-9E71C09E863F}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{C2B2DD41-6C07-43CD-AFE7-0556B4E07D7E}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{F091D315-F1A7-4778-9827-EDBAF021540D}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{A3C3B0B3-D8A2-4C4F-B5ED-F207980351D1}", nodeBrowseExport.Id);
            //tree.AddNewMenuBarButton("{84052DD3-70B6-448C-9272-F335FE4C93B1}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{2DAD6592-2332-4D5D-A364-5AEE62796015}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{3B73F851-F8C3-4787-9BB9-F7E0B74A53C4}", nodeBrowseMenuBar.Id);
            //tree.AddDeleteMenuBarButton("{6BEAB3AB-BA07-4A9D-9AD6-17970749CEDB}", nodeBrowseMenuBar.Id);


            // Form
            var nodeForm = tree.AddForm("{0E855D3E-7E10-49CD-851C-EE1330E78EA0}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{50F9ABE0-6578-42DE-8341-994CFEAC4BAF}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{0221F88E-33C0-4AA4-BDA1-04275D496813}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{6255CAF5-1D0B-4BF6-A3FD-5BABB57766DF}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}


    

