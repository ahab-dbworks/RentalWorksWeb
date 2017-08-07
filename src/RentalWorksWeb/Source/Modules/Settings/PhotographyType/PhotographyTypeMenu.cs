using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fw.Json.ValueTypes;

namespace RentalWorksWeb.Source.Modules
{
    public class PhotographyTypeMenu : FwApplicationTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public PhotographyTypeMenu() : base("{66bff7f0-8bca-4d32-bd94-6b5f13623bec}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwApplicationTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{7200ba18-b58b-4600-93b9-6eae6b102519}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{7876bc74-3260-40b0-9688-430466c2bb2b}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{e4d572f9-ae24-470f-8360-f0ee6d92c90e}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{dbeb0072-be70-4f55-a14d-eaee4f656909}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{2598e5d9-f860-4fbe-ad9a-db2a83d87927}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{36df650f-a31f-4d1c-abf3-c27b6d17c170}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{ede23ce4-d13b-4be5-ada7-1ae062088e36}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{6d3abf74-8b69-4327-ace0-13fa49800806}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{71dff346-c143-420b-a7d9-7a67a108b263}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{6aa9358a-6f2d-48a9-98fd-ca24eb86ff1b}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{a18f3cc3-349e-4048-ac4c-905b3ebf4dcc}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{738bf7e1-95da-4cf5-99fc-165ad6f52fd8}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{c89216d4-8927-49a4-83ca-4087e10ca0a4}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}