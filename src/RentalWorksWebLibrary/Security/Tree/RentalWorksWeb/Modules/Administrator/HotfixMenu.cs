using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Administrator
{
    public class HotfixMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public HotfixMenu() : base("{9D29A5D9-744F-40CE-AE3B-09219611A680}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{3AC85073-AE4B-44F4-ABA3-661D1E1C946E}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{BEC32596-679F-41A1-90F8-29CEEDE6A0DE}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{377767C1-7B5F-4002-821C-DFA9AC3EE023}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{FA2D95B7-198E-4C07-997E-EE448DBC2D28}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{3A7477E0-1360-4981-BE6F-84FBAF874C26}", nodeBrowseExport.Id);
            tree.AddViewMenuBarButton("{B27DC4D5-3A8A-4AD3-B4E8-7D48346D1FD7}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{00A66F98-4BB5-490D-9C44-20CF4524CFDA}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{D7C9CD8E-9278-4714-BBB3-DF61CB0C9218}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{962ED01A-55A7-4AC5-B098-79F95DA47D0F}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}