using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class SetConditionMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public SetConditionMenu() : base("{0FFC8940-C060-49E4-BC24-688E25250C5F}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{B32A9473-FF21-4D18-8460-4553177B7366}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{B0AEC392-E7F0-407C-B47B-4C68C9938560}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{F364C729-61A0-4DBD-B396-606FB7B79046}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{B6DA30F5-29B5-4A67-9473-31B7F09CEC0E}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{E7CEB510-4A94-4785-8124-1C5DC6FACC13}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{EBBA3CBE-19E8-4CCB-8BE9-BE08FF67C8CA}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{5845C2A0-62F4-4654-878D-15087E090D6A}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{FC0603D3-CAA7-4A67-8415-45C842D5DDFF}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{35586168-7BA8-48B3-A9C0-A53D1DBA733B}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{176D8868-D865-43CD-A874-DCF853646FF0}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{47DD7D2A-B89C-4E36-ABA5-9FD87E7781B6}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{6075D393-735E-408A-85ED-1202045CAB8C}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{8C35EC64-405F-4435-9B71-18E92B8728DC}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}