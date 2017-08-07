using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fw.Json.ValueTypes;

namespace RentalWorksWeb.Source.Modules
{
    public class EventCategoryMenu : FwApplicationTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public EventCategoryMenu() : base("{3912b3cc-b35f-434d-aeeb-c45fed537e29}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwApplicationTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{e0d87b79-5271-459a-8922-74df3469acd1}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{0cc86ac4-c2dc-41f1-914b-1af4bdc584da}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{8043a704-5d37-4fce-a360-dc5eb54467b4}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{6f0cb4df-891a-43ca-b28b-1b1b0d56d248}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{ee32daf4-42f1-47aa-8d63-115b2afe49f4}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{d13ef154-6fa4-4c31-b8cd-19e9147f35ba}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{073bdc47-acb0-4392-a8f0-bf87b4be8673}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{5e8c85af-1da2-42a6-80b8-4876e71e861e}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{73586402-9c19-4610-9696-21bbb4b76e70}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{4884add3-606d-4b74-81a0-c642f24e4b32}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{c5330729-236a-4554-99b6-12674b364ab1}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{93237fa9-2e64-4b6c-af4f-aac3cc86ca2a}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{92e05eca-104f-4e31-a584-0e4df98baec6}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}