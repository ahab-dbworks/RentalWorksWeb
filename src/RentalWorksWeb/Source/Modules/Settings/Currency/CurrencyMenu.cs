using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fw.Json.ValueTypes;

namespace RentalWorksWeb.Source.Modules
{
    public class CurrencyMenu : FwApplicationTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CurrencyMenu() : base("{672145d0-9b37-4f6f-a216-9ae1e7728168}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwApplicationTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{9b3191b9-52af-4da1-9ecc-1a6f2331218a}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{d71e7952-6abc-4039-a033-5c42e83dd47c}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{09756982-540b-429c-8421-b09d349fedaf}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{7ccb6cac-b691-4e14-8387-d79e9fc9c580}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{e7e33757-d73d-494d-9393-b8e862aba083}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{429a4bb4-d38a-4180-a777-210568f90beb}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{942c0e6f-8051-4038-a4d7-22f83ef7d37a}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{ca5b72bb-5baf-40e2-935a-4bb56cb7833f}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{09144739-ac47-4eb4-9da3-1f26e91cb3fe}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{9d3c65f6-80ac-498f-b38c-260a52cf5061}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{cc76a0d6-b1ee-4c2b-babe-a8bec7434925}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{41ff41d0-d89a-429c-b13a-40a1b785a12d}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{7368c5ef-0338-4cb2-9583-9417e25b2aaa}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}