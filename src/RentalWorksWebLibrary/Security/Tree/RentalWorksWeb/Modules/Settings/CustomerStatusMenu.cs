using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class CustomerStatusMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CustomerStatusMenu() : base("{B689A0AA-9FCC-450B-AF0F-AD85483531FA}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{8B863FD4-8093-47CD-B756-018DAD93DC93}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{797B89B0-169D-473D-9BFA-D8CABD8EA194}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{231EE3C8-6A8B-4C3D-81CA-AF340EBF520D}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{C3B48A9D-BEB5-4694-9955-D4A8E7F0B035}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{25C7E71F-DE71-48E6-8243-6F000C63A50D}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{628553D1-2EA3-4C4A-9709-0E6BCC3BF344}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{C22FEEBC-36DA-4A8D-BEE0-05A33F61199E}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{22C93B0A-EC91-431B-9785-D4EC3BDBE0B6}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{8DC67DC4-4751-47E1-8638-BF4E1F4B2288}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{2756AED8-79DE-409A-8F6E-20D0FE2F2EFC}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{231F2810-25BD-4D70-A293-EA5B1E04B2F6}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{5CA4ECEB-C1A1-4670-BF58-D7F8B4766596}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{DDA33CC3-DB65-4D94-82F7-7519ACD1DBAD}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}