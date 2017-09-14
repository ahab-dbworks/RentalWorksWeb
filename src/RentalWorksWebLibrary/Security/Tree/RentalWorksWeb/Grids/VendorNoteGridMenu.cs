using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class VendorNoteGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public VendorNoteGridMenu() : base("{60704925-2642-4864-A5E8-272313978CE3}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{FC205895-1068-4FE5-9B66-3C57BE1478DA}", MODULEID);
            tree.AddEditMenuBarButton("{C316F522-EA94-48EC-BA4A-7C0087D01810}", nodeGridMenuBar.Id);            
        }
        //---------------------------------------------------------------------------------------------
    }
}
