using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class SubCategoryGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public SubCategoryGridMenu() : base("{3B4ECB50-EFC4-47B6-842E-6E459C8C60C5}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{6697133E-21CF-435A-811A-B41AD00A95B5}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{E08BD659-BF32-4C09-9807-75EF986CD712}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{2DE893EB-2BF0-4FC4-AE3A-63FEEBC64D3A}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{14785DD7-6928-484F-88B0-64E7A5E360CC}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Toggle Active / Inactive", "{5188A6C8-BCD6-4944-8EB3-0BD967A162C5}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{06D39CBB-D7B7-46BA-94BA-28EAD11570D6}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{C3293B0E-CDAD-4C0D-BDFA-ED3313FF12A8}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{37497C2A-5F6D-4EA6-B715-626B2CEAA3BE}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}