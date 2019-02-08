using FwStandard.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    class GeneratorTypeWarehouseGrid : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public GeneratorTypeWarehouseGrid() : base("{A310B3F4-2B34-433A-8F24-04400B45670A}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{433E33F6-7F73-4E7A-B68D-D93ED3981C84}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{66C67530-2C7A-459A-8CA0-92B6D254479A}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{9264B26D-9940-4933-A811-C51BE193A212}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{FB4F0377-523C-4196-8640-84941E8AF68C}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{679BC592-A2DE-4AED-907F-B346F858EC2B}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{699226AC-F9BD-48DB-9668-6FFF86C4F9E9}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{D4A52549-0002-4785-9CAE-9579DBCF9E30}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
