using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class CompanyTaxOptionGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CompanyTaxOptionGridMenu() : base("{B7E9F2F8-D28C-43C6-A91F-40B9B530C8A1}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{A55B4DCB-546D-43BB-889A-2DFF27F3D2FB}", MODULEID);
            tree.AddEditMenuBarButton("{E1854A32-B9D8-45E4-ACEF-2AEC3C236D27}", nodeGridMenuBar.Id);            
        }
        //---------------------------------------------------------------------------------------------
    }
}
