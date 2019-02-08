using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
    public class OrderTypeTermsAndConditionsGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public OrderTypeTermsAndConditionsGridMenu() : base("{CD65AB0D-A92D-4CA9-9EB3-1F789BC51717}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {

            var nodeGridMenuBar = tree.AddMenuBar("{486E0C1A-5C27-4A19-ACA3-6734154ED6F9}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{8AA0475D-15F2-443F-9B5A-00314901D8B2}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{B56EEF93-1387-4131-8F4C-51B04BF38132}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{8A8156D1-E047-4F78-9C5C-E0CB80EB127D}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{A21ED764-DBD4-48FF-B4BB-02DB7B1C5E08}", nodeGridMenuBar.Id);
        }

        //---------------------------------------------------------------------------------------------
    }
}