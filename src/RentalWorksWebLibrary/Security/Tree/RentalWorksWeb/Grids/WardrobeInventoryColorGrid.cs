using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class WardrobeInventoryColorGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WardrobeInventoryColorGrid() : base("{ED2BCE54-1255-4B65-976B-B24A6573F176}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{CDC777C8-96B4-4B77-98D3-E0830D06708E}", MODULEID);
            tree.AddNewMenuBarButton("{8D52FDCA-58E8-4E71-9B08-7C15820B6099}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{BCB9B466-8CBC-48FC-BC4F-808E91D2C651}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{5504A647-216D-45A9-93F9-24627B3A752F}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
