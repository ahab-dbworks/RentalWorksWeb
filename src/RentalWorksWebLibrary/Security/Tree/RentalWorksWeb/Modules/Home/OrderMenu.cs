using FwStandard.Security;

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
            //tree.AddNewMenuBarButton("{0D5E1A34-90CE-4020-BEDD-4817D9D0305F}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{E1FEF449-73EA-4BE9-8CED-D0BA711D9CFC}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{8521DD2F-5F65-4528-9162-A262337B16AF}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{F85CF357-534F-4A66-8274-CA118EC6FD90}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{DD081953-1800-4754-B456-A463D2B1ACF5}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{E6F2E4A8-A0B1-4468-855C-20E0454E5DEE}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{FA642E0C-D1DC-42C7-B52B-C966A631FDE5}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{CEC1871C-DA65-418E-AF7E-C3DA25CA41A5}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}