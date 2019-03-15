using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class PhysicalInventoryMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PhysicalInventoryMenu() : base("{BABFE80E-8A52-49D4-81D9-6B6EBB518E89}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{25CB607C-C35C-44E7-8C95-D3D923BF9D1D}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{9E750D53-2D39-4CBA-B456-4956FB8EF71F}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{6F7628C4-1093-484A-ADC1-95CD619B5AF1}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{83A6BF6F-44EF-4A4D-A721-13E3DF188F3C}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{536F7442-DA11-4C95-86D0-064AED49F6C0}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{A3267253-403A-4D30-9D45-7DCDEC9ED4EC}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{821A9903-FBAA-48E5-8A26-1015CE838330}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{C41010C1-ECA7-442F-A38A-FB3245FB445E}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{AC7D637B-7D2D-4638-A628-100F9F8491E3}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{49D998E5-CF27-4046-B63F-5E998C9958AA}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{CADCCB05-D2B5-4342-9472-81EF7BAC9072}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{1A7554E3-BDC3-4D7B-9517-2B5245F7FD7F}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}