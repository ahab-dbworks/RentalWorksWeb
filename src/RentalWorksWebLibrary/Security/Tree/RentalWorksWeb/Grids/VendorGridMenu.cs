using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class VendorGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public VendorGridMenu() : base("{BA43D0E0-119D-495B-B066-8E5E738CFC4C}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{F3848F08-D28E-4956-B45C-019A9FE180CA}", MODULEID);
            tree.AddNewMenuBarButton("{F106812B-C943-49F7-833D-25535CB8F3C9}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{F86FA6CE-923B-418C-9893-4729360647D7}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{ED57FDBD-8E6A-4335-AE68-40540419235A}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
