﻿using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class OrderDatesMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderDatesMenu() : base("{D4B28F52-5C9D-4D8C-B58C-42924428DE93}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{8EAAAD69-FAD1-4CC7-AF09-1505BEC233C7}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{3CAF1A6D-7685-4C3B-BFD9-2EB50135BDDE}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{B79A692B-7780-4C07-A6D8-26E1929A0C61}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{4E63219F-2A65-4162-98DA-8C27425055DB}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Toggle Active / Inactive", "{360F9508-DA48-4728-A596-6BC419CDB541}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{FC981AF7-B039-456F-95FC-42633178F74A}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{4DFC63AD-DF4B-4D47-9088-BBACAB6FC807}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{0331FC88-80D9-4F40-980D-7FBA6942D3FB}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}