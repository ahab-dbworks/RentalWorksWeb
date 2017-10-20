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
            var nodeGridMenuBar = tree.AddMenuBar("{3C82C09C-D235-44E7-AD07-6EE85B0E1CB9}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{C30968F4-EFA1-465A-B7AD-134736C8352F}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{A7A8D7A1-A929-471F-A966-28D0D8A65C76}", nodeGridSubMenu.Id);
            tree.AddSubMenuItem("Toggle Active / Inactive", "{EE91F116-FC83-4DAD-8372-F8334CE72312}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{89E55484-96BB-4100-A2CD-774363088B60}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{50F06A9C-DBA9-411D-83C4-35F8E0DB0FBA}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{0D2E4E92-79ED-49A2-8F94-6AEF50572EBB}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
