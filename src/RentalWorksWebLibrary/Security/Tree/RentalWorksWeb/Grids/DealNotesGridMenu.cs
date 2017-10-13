using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class DealNotesGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DealNotesGridMenu() : base("{562D88B4-7CFB-4239-B445-C30BE8F8BAC9}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{140E8B7E-5EAA-40CE-9C69-99E68A670BD9}", MODULEID);
            tree.AddEditMenuBarButton("{889FE98B-AA4E-44D5-9606-DC8173F5E365}", nodeGridMenuBar.Id);            
        }
        //---------------------------------------------------------------------------------------------
    }
}
