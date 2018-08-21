using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class POReceiveBarCodeGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public POReceiveBarCodeGrid() : base("{27703F1E-8F2A-44E3-93AF-F46BADC3D4B1}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{BEAD6B3E-44EF-49FC-B0AB-7AB955B5249C}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{CB808AEE-5972-4A58-B238-E0161ADEE4BB}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{F4CD027A-C86C-496B-BB26-C662B507C713}", nodeGridSubMenu.Id);
            tree.AddEditMenuBarButton("{A55E7B19-79EE-4B3A-AFA1-FB1A92BD7466}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{34F28CBE-D11C-419E-B794-B962068559D8}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
