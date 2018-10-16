﻿using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class OrderItemGrid : FwSecurityTreeBranch
    {
        //--------------------------------------------------------------------------------------------- 
        public OrderItemGrid() : base("{C8A77000-43DD-4E49-A226-1E0DC4196F12}") { }
        //--------------------------------------------------------------------------------------------- 
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{D8762DD6-1603-46AB-ADBD-118D68BC63DA}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{00944CE3-2EDF-4E27-B7FB-38ADBA46AC2E}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{1A932D18-69F0-44CA-80AE-F1F10D3AAB9F}", nodeGridSubMenu.Id);
            tree.AddSubMenuItem("Summary View", "{D27AD4E7-E924-47D1-AF6E-992B92F5A647}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Search", "{77E511EC-5463-43A0-9C5D-B54407C97B15}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Lock / Unlock Selected", "{BC467EF9-F255-4F51-A6F2-57276D8824A3}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Bold / Unbold Selected", "{E2DF5CB4-CD18-42A0-AE7C-18C18E6C4646}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Sub Worksheet", "{007C4F21-7526-437C-AD1C-4BBB1030AABA}", nodeBrowseOptions.Id);
            tree.AddSubMenuItem("Add Loss and Damage Items", "{427FCDFE-7E42-4081-A388-150D3D7FAE36}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{5A3352C6-F1D5-4A8C-BD75-045AF7B9988F}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{4113AFA5-29EA-4026-831E-685B904C5ADD}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{89AD5560-637A-4ECF-B7EA-33A462F6B137}", nodeGridMenuBar.Id);
        }
        //--------------------------------------------------------------------------------------------- 
    }
}