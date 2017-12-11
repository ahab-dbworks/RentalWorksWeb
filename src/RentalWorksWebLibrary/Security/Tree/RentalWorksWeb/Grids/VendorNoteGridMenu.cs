using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class VendorNoteGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public VendorNoteGridMenu() : base("{60704925-2642-4864-A5E8-272313978CE3}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{FC205895-1068-4FE5-9B66-3C57BE1478DA}", MODULEID);
            tree.AddNewMenuBarButton("{275BA516-03A1-4EDF-B94C-A9D242E20308}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{FFECFFDB-3B13-4EC7-A083-1FEF7686171A}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{FD4D3648-8764-44D8-82FA-8F6BC6738ACD}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
