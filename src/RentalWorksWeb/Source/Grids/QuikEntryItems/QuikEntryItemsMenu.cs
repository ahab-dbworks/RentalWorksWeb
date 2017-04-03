using Fw.Json.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalWorksWeb.Source.Grids
{
    public class QuikEntryItemsMenu : FwApplicationTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public QuikEntryItemsMenu() : base("{1289FF25-5C86-43CC-8557-173E7EA69696}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwApplicationTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{3FC13083-4D4E-413E-A649-FB229CB1F063}", MODULEID);
                var nodeGridSubMenu = tree.AddSubMenu("{E568DE0F-B7C0-4F3F-9074-2E94447D8746}", nodeGridMenuBar.Id);
                    var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{9881AEFE-EE4D-49E3-A731-1AE885433F37}", nodeGridSubMenu.Id);
                        tree.AddSubMenuItem("Toggle Active / Inactive", "{29BED4DD-0062-4BE2-9A79-7423499E27E4}", nodeBrowseOptions.Id);
                tree.AddNewMenuBarButton("{E32D7DB3-D307-49FB-BD1F-3851F6704D60}",    nodeGridMenuBar.Id);
                tree.AddEditMenuBarButton("{34C80156-DE44-4915-9271-0DC186A97AD4}",   nodeGridMenuBar.Id);
                tree.AddDeleteMenuBarButton("{B995B4A8-7276-4687-8DCF-5893422423A6}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}