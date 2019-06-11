using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Administrator
{
    public class CustomFieldsMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CustomFieldsMenu() : base("{99D56DA6-5779-44A5-8BA6-E033F343C6D0}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{3593A85B-916A-48E0-9F47-145C1CBFDB03}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{79B8623D-D740-4B11-89A4-B6C930B4A7A0}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{7D73D120-190F-4354-A96B-FFC849D37A76}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{BF74B7C4-107E-488E-877B-7D334A19FD33}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{EE53A22E-A71D-4BF4-AD99-DA4637C3A6C7}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{EB617751-CDAE-4BA0-A59C-BAA3997D275D}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{A5090FAC-28B4-44AA-A8AC-56DA8A3A5277}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{A5DC2AA5-30A9-4A33-B9B6-43CD39FEF7EE}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{67E12D97-E7FB-4AEA-8383-ED1AD2A6F9EE}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{26EA872C-8F5D-4EA6-B90B-960FEC04E663}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{BEE4E57C-CB59-4060-8049-DEDB9390CEC9}", nodeForm.Id);
            //var nodeFormSubMenu = tree.AddSubMenu("{7661D8CC-A731-4FD0-8BE0-06CFEB755279}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{8A03371E-CB5D-4445-9709-462DCD5E188C}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
