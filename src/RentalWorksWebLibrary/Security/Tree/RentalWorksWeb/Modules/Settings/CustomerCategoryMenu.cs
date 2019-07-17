using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class CustomerCategoryMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CustomerCategoryMenu() : base("{8FB6C746-AB6E-4CA5-9BD4-4E9AD88A3BC5}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{A09A3493-3BD8-4BFC-9A2C-A0E00ED2510F}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{5915682D-23B4-4F03-97A5-655601F0D26B}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{BC6E1D6D-2E0F-488E-947C-3478C4C58204}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{920FED21-059A-432E-8B7E-BFB44E5AC824}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{D88AD926-FEBC-4CE5-8EBA-EDA88E48767A}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{8ED80069-9FA2-41AF-AD4A-AAE5458693A6}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{1553BAC8-E1D4-4639-BAF7-4FB0B05ADF8C}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{4ED42A9C-A3E0-41D0-B895-0D81588CE7B7}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{5F5DA193-F709-40E7-B15A-71FD5390F940}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{CF310D22-903C-46E4-94C3-8A503F6A4633}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{1747009A-B2C5-4D93-A061-1FB317406C69}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{34402444-73F4-41D3-BFDC-D445E1B7478D}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{4BDE30F9-6C59-4539-B3A1-915C1945CD3C}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}


    

