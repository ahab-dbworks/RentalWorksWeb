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
            tree.AddEditMenuBarButton("{D2A9476F-9273-469E-9193-122CDC3F274A}", nodeGridMenuBar.Id);
            //tree.AddNewMenuBarButton("{ABE2F5BD-D414-4E7A-8C18-E21C5EF3DC25}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{A3002AF7-C2A8-408F-98AF-414D4D678926}", nodeGridMenuBar.Id);
        }
        //--------------------------------------------------------------------------------------------- 
    }
}