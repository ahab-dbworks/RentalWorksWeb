using Fw.Json.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RwQBO.Source.Grids
{
    public class MasterItemMenu : FwApplicationTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public MasterItemMenu() : base("{F21525ED-EDAC-4627-8791-0B410C74DAAE}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwApplicationTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{7F3EF9E7-10CF-4E28-96BE-40A805744CBD}", MODULEID);
                var nodeGridSubMenu = tree.AddSubMenu("{5EC60393-F6E0-44AD-AF46-0A3CE7D893E0}", nodeGridMenuBar.Id);
                    var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{6F080A64-D1F5-4E5D-BA07-F6ECAA099131}", nodeGridSubMenu.Id);
                        tree.AddSubMenuItem("Toggle Active / Inactive", "{5D89ED05-AEA9-4375-805E-A21DAD502430}", nodeBrowseOptions.Id);
                tree.AddNewMenuBarButton("{EB4E05E9-60E3-41FE-9D18-E3339FCCC645}",    nodeGridMenuBar.Id);
                tree.AddEditMenuBarButton("{9E98E081-0174-4822-B571-EF7D2C9DB288}",   nodeGridMenuBar.Id);
                tree.AddDeleteMenuBarButton("{262C7184-362A-40C6-87E4-F006CB3EF6B4}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}