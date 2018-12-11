using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class VendorInvoiceItemGrid : FwSecurityTreeBranch
    {
        //--------------------------------------------------------------------------------------------- 
        public VendorInvoiceItemGrid() : base("{C4887310-2572-458F-9691-1ABECB622862}") { }
        //--------------------------------------------------------------------------------------------- 
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{5AF024A0-50FA-4814-ABF2-F3D6B81EFAD4}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{DA80269F-7096-4D04-9C2E-1F0C8EBDF944}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{3ECA6432-5CA2-4A76-B0F8-AC9111DA4197}", nodeGridMenuBar.Id);
        }
        //--------------------------------------------------------------------------------------------- 
    }
}