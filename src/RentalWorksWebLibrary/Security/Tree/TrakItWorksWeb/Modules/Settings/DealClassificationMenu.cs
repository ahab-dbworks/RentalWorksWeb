using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class DealClassificationMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public DealClassificationMenu() : base("{BC795D50-F8D8-40C8-9F9E-63A60B07E514}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{B1BD9A7C-B371-4466-9BD3-79365A268DC9}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{3049B49A-6201-4D56-B8F3-8007D383C500}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{A3B54EAF-9BB2-499A-BA3C-A219B99AE6E7}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{3C41BDA9-7C85-47B8-B66B-EB4611E65F48}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{FECE2D21-7523-459B-8378-A65409CE1D23}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{700084A0-7B2B-4782-8CBB-72EC3DBCC87C}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{55561F67-28F8-4AEE-8259-BA9381B2354E}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{3AF57E59-E085-47C2-BCA8-E95BB0BDE5D1}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{B8F47792-5792-4E45-9839-4F7CA4E7FE81}", nodeBrowseMenuBar.Id);


            // Form
            var nodeForm = tree.AddForm("{068F6B37-0161-4936-BDE8-C292B2E3125B}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{51E1DF87-73A9-45DB-ABBA-2F55BDCB230A}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{8AE9C095-8F1C-4A0C-B2DE-C113C04D8E69}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{098C2670-A905-4023-A1B1-71B18C544B62}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
