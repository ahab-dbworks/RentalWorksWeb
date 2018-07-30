using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class CheckedInItemGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CheckedInItemGrid() : base("{5845B960-827B-4A89-9FC4-E41108C27C21}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{846F3D1F-0E58-4122-BE33-801064A01B85}", MODULEID);
        }
        //---------------------------------------------------------------------------------------------
    }
}
