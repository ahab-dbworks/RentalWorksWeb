using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class OrderTypeInvoiceExportGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderTypeInvoiceExportGridMenu() : base("{B24187E9-6B1D-4717-B9C2-F95C5543AE45}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{8291B167-1521-4A70-9C76-740C69ED37FF}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{EB67D37E-DA4C-4A60-B46E-6046059DA18A}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{BACA18BF-C41A-43B8-861F-707A7FF36E8D}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{1166C0F1-01CA-4CF7-88E2-B8AE702673B7}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{A08931BF-F649-4946-AC3D-89B3B27F9BC5}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}