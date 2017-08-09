using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fw.Json.ValueTypes;

namespace RentalWorksWeb.Source.Modules
{
    public class RetiredReasonMenu : FwApplicationTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public RetiredReasonMenu() : base("{1DE1DD87-47FD-4079-B7D8-B5DE61FCB280}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwApplicationTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{B291E269-AB29-4571-BD76-9E9985EBDA40}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{08EA4444-B200-4458-93C0-1366469EFDD3}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{0FD3E64B-C30A-4141-98D1-D3ED84081803}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{B27BF18A-381D-4E48-9763-EFA2384036FA}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{BF0193C5-597B-4008-8E41-1301305E993D}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{8EBB306B-A91B-4980-8814-B1D07335FE4B}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{D0BC2FE1-E69B-4728-AF97-F2DB58492D70}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{C42CA99E-CC5C-433B-A988-23132F61BB2A}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{FC418337-AB9B-4DBE-AA9D-B5971BE86D4E}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{F784ED25-D4F7-437B-9830-7CCA2652F0ED}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{BB4A3196-0152-4D51-8141-5BA097E33D0D}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{9EE71331-6E9E-486E-BA68-CE237893F6DD}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{2A2FA4F5-9421-4A0B-89CD-D57418AEB8B3}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}