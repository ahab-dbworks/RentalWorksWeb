﻿using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class ActivityDatesMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ActivityDatesMenu() : base("{0C7E7F68-50C8-45A0-B6CA-BE11223D7806}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{51BD56FA-3217-4776-A51E-7083BC2286AD}", MODULEID);
            tree.AddEditMenuBarButton("{50186F2C-7C04-47AA-8976-2EF2A5DE345D}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{D8D19408-D0B2-402A-BD01-FC0BBE43D689}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{14B4CA04-0476-49B0-81B1-5EA6E822429C}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}