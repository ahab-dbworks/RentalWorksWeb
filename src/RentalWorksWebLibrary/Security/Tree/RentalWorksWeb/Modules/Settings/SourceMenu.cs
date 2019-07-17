using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class SourceMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public SourceMenu() : base("{6D6165D1-51F2-4616-A67C-DCC803B549AF}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{7425202D-9574-4A6E-9280-937C7D9B8B1E}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{B8491EAB-0666-45CD-9675-5FA8A236F947}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{AF73E9B6-4AC7-4EE0-927D-A776B93193FB}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{F41578EC-1970-4A67-BB2A-91C47750C579}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{7A3E6E4F-8408-4E2B-B1C2-68F5BE10583C}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{0B4B5BA3-B7E2-4637-9CCA-15BC449C6AE0}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{B0EDFFE4-F193-42A3-8C88-17AFB6DE30B9}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{5A7A1221-985B-4D42-BF8C-C27923187BCA}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{BDCE4AA1-3FFE-41E0-8797-A1503ACB4DEB}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{B9376A93-2B66-4B6D-9A67-ED8D62F776B0}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{1C6FDCDD-1158-4B05-93F6-AA534182E808}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{7A3C6DCE-E1BF-498D-B6F6-FF79BEDF6C77}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{E1DB00F1-2E81-4D97-9E3F-8655A3C08002}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}


    

