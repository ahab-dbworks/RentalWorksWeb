using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class WarehouseMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WarehouseMenu() : base("{931D3E75-68CB-4280-B12F-9A955444AA0C}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{0281E67A-91E6-48AA-826C-8F31D69533DD}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{3D8A3076-300C-4F0C-8AC6-869379611495}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{E7D7A1EF-2335-4F27-BF0E-657355D506FB}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{1DB508C8-4E97-429E-B5A6-A7F20C6AE728}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{43EC3022-893F-4E46-9DF6-2D2DBD9A17D2}", nodeBrowseExport.Id);
            //tree.AddNewMenuBarButton("{827AEFF2-2A52-46B6-8C53-92BE94DF52C6}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{B8996B9C-50F9-45AC-8CBF-F6F991EC1147}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{D32A7370-0FC4-4F23-A91D-E064A76EA891}", nodeBrowseMenuBar.Id);
            //tree.AddDeleteMenuBarButton("{437D7A29-3B73-49F1-B823-BF38CCE26B4D}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{47E7E835-4A5D-452E-A3A8-624904DC1698}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{E377526B-13C2-4489-A21E-8C9890EFD71C}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{FD3265F6-1953-4883-B916-1B6034832474}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{84B0F6B4-EB5E-4E9D-B834-16B675CE65BA}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}