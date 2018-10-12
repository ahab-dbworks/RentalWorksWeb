using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class InvoiceItemGrid : FwSecurityTreeBranch
    {
        //--------------------------------------------------------------------------------------------- 
        public InvoiceItemGrid() : base("{8D093CB9-1C37-449F-8E64-E76653488ABB}") { }
        //--------------------------------------------------------------------------------------------- 
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{48633D03-A710-481D-8B3D-85E8EC51846A}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{1F0F4535-BB85-439F-980B-6FBC8C95C3B5}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{508F0FEC-F4DB-40BE-B42E-5FE6ADA9263A}", nodeGridSubMenu.Id);
            tree.AddNewMenuBarButton("{4F230B19-C651-42B0-A3B7-24282ED5B821}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{774E77CA-9967-4A01-95DA-A76C024EE23B}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{27053421-85CC-46F4-ADB3-85CEC8A8090B}", nodeGridMenuBar.Id);
        }
        //--------------------------------------------------------------------------------------------- 
    }
}