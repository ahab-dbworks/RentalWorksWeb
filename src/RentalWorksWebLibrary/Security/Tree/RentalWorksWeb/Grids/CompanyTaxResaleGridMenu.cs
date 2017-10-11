using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class CompanyTaxResaleGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CompanyTaxResaleGridMenu() : base("{797FA2DB-87EC-4C60-8DA2-772E0010FA9E}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{CE1F56FF-CA38-4895-99A3-B29C17C050FF}", MODULEID);
            tree.AddEditMenuBarButton("{B5C96A0B-B980-42E8-BB38-33C4D1F9AD10}", nodeGridMenuBar.Id);            
        }
        //---------------------------------------------------------------------------------------------
    }
}
