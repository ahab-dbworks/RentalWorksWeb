﻿using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class OrderMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderMenu() : base("{64C46F51-5E00-48FA-94B6-FC4EF53FEA20}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{9F23AC18-D743-4FE1-A402-3BECE439D43E}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{1D6FD475-4FDD-449F-B53F-DFAE1D4BF9A1}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{0E2A9234-314E-47EB-81EF-BE741C346903}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{EB58FFA8-DE71-434F-A4B4-D9D3786419D2}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{5A45F16F-D7FA-410F-BE8D-304E0CFC1327}", nodeBrowseExport.Id);
            tree.AddSubMenuItem("Cancel / Uncancel", "{DAE6DC23-A2CA-4E36-8214-72351C4E1449}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{82A3CF3D-485A-4F9C-8D2B-C9A0FF7E4F73}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{E1FEF449-73EA-4BE9-8CED-D0BA711D9CFC}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{8521DD2F-5F65-4528-9162-A262337B16AF}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{DD081953-1800-4754-B456-A463D2B1ACF5}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{E6F2E4A8-A0B1-4468-855C-20E0454E5DEE}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{FA642E0C-D1DC-42C7-B52B-C966A631FDE5}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{F1DE9D9F-5311-4AA3-B477-B1F6A144F8D4}", nodeFormSubMenu.Id);
            tree.AddSubMenuItem("Copy Order", "{E25CB084-7E7F-4336-9512-36B7271AC151}", nodeFormOptions.Id);
            tree.AddSaveMenuBarButton("{CEC1871C-DA65-418E-AF7E-C3DA25CA41A5}", nodeFormMenuBar.Id);
            tree.AddSubMenuItem("Create Pick List", "{91C9FD3E-ADEE-49CE-BB2D-F00101DFD93F}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Order Status", "{CF245A59-3336-42BC-8CCB-B88807A9D4EA}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Search", "{B2D127C6-A1C2-4697-8F3B-9A678F3EAEEE}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Print Order", "{F2FD2F4C-1AB7-4627-9DD5-1C8DB96C5509}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Create Snapshot", "{AB1D12DC-40F6-4DF2-B405-54A0C73149EA}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Cancel / Uncancel", "{6B644862-9030-4D42-A29B-30C8DAC29D3E}", nodeFormOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}