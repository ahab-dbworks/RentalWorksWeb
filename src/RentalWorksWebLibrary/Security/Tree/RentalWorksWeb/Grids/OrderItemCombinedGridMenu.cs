using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class OrderItemCombinedGrid : FwSecurityTreeBranch
    {
        //--------------------------------------------------------------------------------------------- 
        public OrderItemCombinedGrid() : base("{B8E0EFB5-F175-46DE-92A7-32B45E6942FC}") { }
        //--------------------------------------------------------------------------------------------- 
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{BD05EC30-0415-445A-A133-F6E439399260}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{62E48074-2951-424B-850C-A58B6AC85069}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{9CCCD19E-2B8A-40D9-A6DF-83E0426454FE}", nodeGridSubMenu.Id);
            tree.AddSubMenuItem("Toggle Detail / Summary View", "{AC62B85E-F34C-4213-B82D-ADC476A32F5E}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{3DF5723F-C43A-4129-B44F-262714A4BFDE}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{EDDD4611-110B-4053-8345-9BE7404FA68D}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{C5E31A06-3B76-4D61-9DC7-1F4E8FCC6881}", nodeGridMenuBar.Id);
        }
        //--------------------------------------------------------------------------------------------- 
    }
}