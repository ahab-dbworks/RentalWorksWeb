﻿using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class DiscountItemSalesGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DiscountItemSalesGridMenu() : base("{85AB2907-07FE-43CF-B16D-DDAE781F64ED}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{A170008B-AA54-40C0-A964-4B37BF4FD19D}", MODULEID);
            tree.AddEditMenuBarButton("{047FBE69-1352-4A62-99D8-4FCE171F6736}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{D7BE42D8-6527-47C6-BC8D-1DF09E06A45C}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{37FA5CD0-553B-45E8-82C8-39A800A87409}", nodeGridMenuBar.Id);

        }
        //---------------------------------------------------------------------------------------------
    }
}