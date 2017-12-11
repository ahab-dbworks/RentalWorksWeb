using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class OrderTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderTypeMenu() : base("{CF3E22CB-F836-4277-9589-998B0BEC3500}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{F1DFB275-CFC3-4C40-BB36-2BAA2996962E}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{8ADA2869-C097-4273-9BDF-F363928D98CC}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{33B53A05-9666-412D-A46D-BE9A1E85B250}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{3BC14005-9142-446A-9420-609DF4CF5915}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{B8992933-473A-4CED-874C-7A0FE0F45F8A}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{98C49AC3-116C-4994-8E06-52F7F7333A59}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{80422AD3-E4AD-4924-8A07-A7AE3DDA41F3}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{1BC7DFAE-1552-43E2-AA52-6502868BFD30}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{2970CE8C-F428-4969-AA2D-1595125F77D1}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{D826F4AE-30AE-48CB-94B3-1D75FDDC05DF}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{FBAB585E-EDBF-436B-8295-E8A986838CDB}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{B4B3FCAF-1B6C-4E30-B0AD-9D265E148F53}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{658B5760-D5E5-4E1F-9E2D-85107A6B66A2}", nodeFormMenuBar.Id);
        }

        //---------------------------------------------------------------------------------------------
    }
}