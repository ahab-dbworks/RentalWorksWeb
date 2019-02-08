using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class PartsInventoryCompatibilityGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PartsInventoryCompatibilityGridMenu() : base("{97DC0D58-2968-47F4-970A-0889AEFDC63B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{A3F11A4F-D68D-47F6-9996-8845661F8833}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{C31B8D73-D385-4D1F-B1F4-1B476632716B}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{88EAAA21-30E5-4B04-BF5E-140975993A68}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{6C4C05D4-B82B-48E3-A45E-772EAA62F0A4}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{67EAE20F-CA9D-4E73-93A5-0BD3FC654AD1}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{51C2B9A4-32EE-4CBD-8EBF-05683EE2585A}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{8DFB6054-2338-4D21-80B3-70A0A3DD292E}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}