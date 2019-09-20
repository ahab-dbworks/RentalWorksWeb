using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class AlternativeDescriptionGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public AlternativeDescriptionGridMenu() : base("{2275D52D-9021-4E28-97E0-703176E73DAD}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{C8035811-728D-4F30-9878-1F6CF8CA8B5B}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{FF0F674C-2D1D-4125-9314-8893160A682C}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{505B28AA-F1B0-4F1E-8F96-FB9858209884}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{451F6665-F596-4E50-9ADD-6E30E828DB80}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{E0A8BB9E-F182-4B30-ADF7-3B9099934E91}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{3F6B6E6D-D534-4B52-AB27-016764A056F5}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{44AAF9DA-2500-445E-984B-7C8A48149A14}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}