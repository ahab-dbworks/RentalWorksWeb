using Fw.Json.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksWeb.Source.Grids
{
    public class OrderDatesMenu : FwApplicationTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderDatesMenu() : base("{D4B28F52-5C9D-4D8C-B58C-42924428DE93}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwApplicationTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{8EAAAD69-FAD1-4CC7-AF09-1505BEC233C7}", MODULEID);
                var nodeGridSubMenu = tree.AddSubMenu("{3CAF1A6D-7685-4C3B-BFD9-2EB50135BDDE}", nodeGridMenuBar.Id);
                    var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{B79A692B-7780-4C07-A6D8-26E1929A0C61}", nodeGridSubMenu.Id);
                        tree.AddSubMenuItem("Toggle Active / Inactive", "{360F9508-DA48-4728-A596-6BC419CDB541}", nodeBrowseOptions.Id);
                tree.AddNewMenuBarButton("{FC981AF7-B039-456F-95FC-42633178F74A}",    nodeGridMenuBar.Id);
                tree.AddEditMenuBarButton("{4DFC63AD-DF4B-4D47-9088-BBACAB6FC807}",   nodeGridMenuBar.Id);
                tree.AddDeleteMenuBarButton("{0331FC88-80D9-4F40-980D-7FBA6942D3FB}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}