using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class DepartmentMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DepartmentMenu() : base("{043F9EB4-F31C-441F-8011-B5263B88B16B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{265F9A63-786A-477D-AC8E-180F3D74D122}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{D53DF01B-AEF3-4C9A-9B40-2623A66A0BCE}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{91CC3E49-2ED6-46F7-9B87-BA9CC3917E01}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{B19295D7-9D5B-4104-B4E6-85215C4742BA}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{8FA878A8-2339-43BB-A168-EFE39BA9BE61}", nodeBrowseExport.Id);
            //tree.AddNewMenuBarButton("{CDC5B719-ED0B-413E-82E9-5D4FB943D73D}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{F1428A77-D3DF-4B66-89D8-D69DD07789FA}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{B0D292E5-E8B7-4F1A-B957-CAD710C003AC}", nodeBrowseMenuBar.Id);
            //tree.AddDeleteMenuBarButton("{58128833-FB65-42A8-B34A-A92BDF33146F}", nodeBrowseMenuBar.Id);


            // Form
            var nodeForm = tree.AddForm("{994F7E64-8B27-4EE3-B426-449164F8ACB0}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{4AE2D621-5452-429D-8EA4-9B7D0383BDF1}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{6B593CF3-4E8F-4797-A55D-C7A25C412D0D}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{321A3114-02ED-4163-BE5A-FE2284673178}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
