using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Administrator
{
    public class CustomReportLayoutMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CustomReportLayoutMenu() : base("{B89CDAF3-53B2-4FE8-97C6-39DC98E98DBA}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{FD2CFB62-5CC2-42C5-AEE0-A634BECCFF08}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{E30EA04D-CEB1-41BC-8939-ACF5D2FED7C0}", nodeBrowse.Id);
            tree.AddNewMenuBarButton("{86C6EB6E-64F7-4AFE-BC5B-2DFFEC883635}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{744D8F08-FC53-4E46-B347-705B75B6C2E2}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{FC337741-C4EB-487A-803D-C2E404F6D9FE}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{9C40E830-E557-40AB-8A7E-3D3867E62F8A}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{1722B50B-4A6D-42AB-9BF4-5BB9305865F7}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{4AC91750-B6E3-480D-9E53-93BA172272D6}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{301256B9-6C16-430B-B9AE-E39894BFE83C}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{7979A492-9A7A-445A-AEAC-22BA2C64668D}", nodeFormSubMenu.Id);
            tree.AddSaveMenuBarButton("{714A9A18-6C0C-4BD3-A432-0F4D8F5C12FC}", nodeFormMenuBar.Id);
            
        }
        //---------------------------------------------------------------------------------------------
    }
}