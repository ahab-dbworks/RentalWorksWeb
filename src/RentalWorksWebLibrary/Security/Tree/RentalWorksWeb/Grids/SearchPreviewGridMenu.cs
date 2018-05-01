using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class SearchPreviewGrid : FwSecurityTreeBranch
    {
        //--------------------------------------------------------------------------------------------- 
        public SearchPreviewGrid() : base("{A6C93317-0DDC-4781-9B01-2EFC78ECED40}") { }
        //--------------------------------------------------------------------------------------------- 
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{C8EE8BBE-DE0C-451F-85C2-1980813F1685}", MODULEID);
        }
        //--------------------------------------------------------------------------------------------- 
    }
}