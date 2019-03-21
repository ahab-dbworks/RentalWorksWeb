using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class OrderContactGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderContactGridMenu() : base("{0D80C755-0538-461D-A6E6-3A92D17217F2}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{E21C9229-2289-48C5-BE3B-19A0F057FE80}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{F842C73F-0411-4952-8D71-5506E9FF0F89}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{FD9589F2-D4D7-44FE-A9F1-9FF5D555E05B}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{462C5F15-8802-469D-8C54-C9ACA494EBCC}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{82D25AE9-2011-49F2-B1BD-106A4217DE7B}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{FF964141-96EF-4610-9FB6-D1E1818283DB}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{C3C0219B-8B9E-403D-B015-6772C3C7E621}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}