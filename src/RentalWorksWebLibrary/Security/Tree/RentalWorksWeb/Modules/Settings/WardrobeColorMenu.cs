using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class WardrobeColorMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public WardrobeColorMenu() : base("{32238B26-3635-4637-AFE0-0D5B12CAAEE4}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{7F669EBB-D78D-4F8B-B31D-34A43FA01D01}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{C8CEF4FE-4689-4EFB-ADF7-232C6F1EF34A}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{5CD2100D-5AA7-4789-9236-CD7F2AD620AE}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{A3E70CF7-F03D-4508-A96E-AD9763C6E66B}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{DACAEDCC-CA7F-40E0-9B79-C59A05FF8539}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{27C800C7-3CA4-45CA-81E0-CB58EA194A20}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{465A2218-2707-4681-946B-28C0F94D3FC0}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{9D74421B-5C71-4AB0-9B26-8B3661D0266A}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{33BD46FA-AC7C-4BF4-828E-6A0E56F59039}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{AB8EE3FA-3387-4AA7-9F42-B3EC94B4EFCF}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{AB8C0EE7-36B3-48F9-93C0-C28DB7BA75C7}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{DE9F9544-1D9A-43DE-AD8C-3690AE3E4A90}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{4A497B15-320C-4C50-822D-48233FA17F35}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}


    

