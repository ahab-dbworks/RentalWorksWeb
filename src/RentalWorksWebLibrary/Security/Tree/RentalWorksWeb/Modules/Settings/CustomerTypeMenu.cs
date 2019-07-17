using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class CustomerTypeMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CustomerTypeMenu() : base("{314EDC6F-478A-40E2-B17E-349886A85EA0}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{A0918D90-FE65-45C1-BA12-CD325D86A0A0}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{792164D3-AA20-4845-9615-D1EF6B77A17D}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{AD64B540-A1ED-4433-9F05-6EE776A09C01}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{C5BA1B79-642F-4994-BDED-02BD416F54C3}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{4E53A163-3BA9-4D42-A952-974DD447FB40}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{2EEDE9F9-1F9F-422A-B153-C2D3A9035254}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{17443106-8A8A-4560-8850-C87670742240}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{D043518C-9D05-460E-B691-3BFD6E7BEE14}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{9AF9741E-2FF7-40D1-89B0-D1F8DAF477D0}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{203C34AB-3167-40A1-8BF9-FD9DEBB4C86C}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{E21672CB-CD5A-4FCB-AC90-59A270D2579B}", nodeForm.Id);
            // var nodeFormSubMenu = tree.AddSubMenu("{803BE37C-3B75-4B3F-92C2-3E601C262672}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{A95D5641-E85D-417A-B199-BC1F9875E710}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}