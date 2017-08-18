using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fw.Json.ValueTypes;

namespace RentalWorksWeb.Source.Modules
{
    public class Crew StatusMenu : FwApplicationTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public Crew StatusMenu() : base("{73A6D9E3-E3BE-4B7A-BB3B-0AFE571C944E}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwApplicationTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{696C6B29-9781-4284-9239-2026A3A82C40}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{3E97833B-3D02-44E5-83DC-63B753BBB84D}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{836A150C-2CB5-4DC4-9290-9F282A75B4B5}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{D5D19BE0-031F-4DA5-B0F6-4BFF901EC283}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{6EED566F-6BED-4D43-963A-EC2C21F806CC}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{525D3B9F-3688-4EA3-B8A6-88A6E5A8CE3E}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{4BA05FDD-5A7D-4FFA-B9F1-22636E042F71}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{2898D512-31EF-491D-8676-FEA562CE1A39}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{70CE7257-D45F-4AAB-A85A-45FBC3EAF649}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{0FF17739-DC05-41B1-84F2-1353505608D0}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{AA24F514-B961-4DDC-8CF0-734884AB54FE}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{49AB5753-66F8-4754-BA5C-486D8505F54E}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{7960714D-04AE-488F-8529-69CA31DBFCD0}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
