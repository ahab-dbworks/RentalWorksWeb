using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class WardrobeMaterialMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WardrobeMaterialMenu() : base("{25895901-C700-4618-9ADA-00A7CB4B83B9}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{6926D88F-6A08-4814-9EE2-473EF68407C6}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{EB30ED77-9BC2-4D76-A008-7F5F54EEF08A}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{D336A305-0F14-44CE-B5CC-187541228EFC}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{0044FC42-969E-4CB9-BDA8-CA32C0BBEB78}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{4276319D-71CB-4FB2-8B18-A00F915CF354}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{356F39AD-2416-404B-851E-29F41AB250D8}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{4FAE1747-8964-49ED-AC43-BC2081935ACF}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{654533E9-8C50-4BDB-AFDA-91DF3924186A}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{C49A7EEB-D4FC-4928-B857-8BF7FFEDDC12}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{8DA4EAF9-7BDE-463E-A78E-E91D47CBB117}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{07F4718F-0409-4A0D-9D33-0CF5FD11589F}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{D11542E3-FFF5-40BD-8C3F-3A0D764C52A8}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{055E1361-6A61-419B-AC29-E7E03A506E2D}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}