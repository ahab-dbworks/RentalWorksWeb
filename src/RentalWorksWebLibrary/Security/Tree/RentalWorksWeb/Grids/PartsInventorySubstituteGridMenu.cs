using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class PartsInventorySubstituteGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PartsInventorySubstituteGridMenu() : base("{F9B0308B-EBFC-4B37-B812-27E16897B115}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{3D72C2A0-7F84-4313-8273-08259A254AB5}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{EDDD7EB0-1AAD-4A00-A3AA-FB21AE3ADFA0}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{BC81CDA2-2CFA-4A68-9A9F-619E49D34592}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{3803CF1B-67FA-43B7-B083-5574D13AB2ED}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{B8B48179-06CA-4442-9046-733721727EE7}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{7860DA4A-DF22-42BE-80EA-D0B7E7310B9C}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{6FCF0571-0821-43C1-A7EE-3F529529536F}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}