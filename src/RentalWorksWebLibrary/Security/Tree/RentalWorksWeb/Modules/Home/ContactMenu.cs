using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Home
{
    public class ContactMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContactMenu() : base("{9DC167B7-3313-4783-8A97-03C55B6AD5F2}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{CFCBDC18-8217-46FD-8324-E4995A19E6AC}", MODULEID);
                var nodeBrowseMenuBar = tree.AddMenuBar("{57A6B891-0F49-4DA1-A583-CA0A3F3E73FE}", nodeBrowse.Id);
                    var nodeBrowseSubMenu = tree.AddSubMenu("{460EEFE3-C08B-4347-807B-0AF79E3C97BD}", nodeBrowseMenuBar.Id);
                        var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{BC4BADA7-1891-4481-841A-E5E8E23238D8}", nodeBrowseSubMenu.Id);
                            tree.AddDownloadExcelSubMenuItem("{5B1C6ED4-8190-4B3F-9363-1E2E66DB0E28}", nodeBrowseExport.Id);
                    tree.AddNewMenuBarButton("{ED3FFCE5-6344-4E42-9C58-AC2589D96962}",    nodeBrowseMenuBar.Id);
                    tree.AddViewMenuBarButton("{1D37AB5D-66FC-47FD-B838-9CC32C07AA29}",   nodeBrowseMenuBar.Id);
                    tree.AddEditMenuBarButton("{C9672F64-D5EB-4FD9-A612-6B5DD9A8CBD8}",   nodeBrowseMenuBar.Id);
                    tree.AddDeleteMenuBarButton("{A6810213-4490-4865-A335-1565B8C50061}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{B91748C3-9B82-4327-A949-13F56DB7BF9C}", MODULEID);
                var nodeFormMenuBar = tree.AddMenuBar("{0834FFE1-CAB9-48D9-9A45-06CB73737916}", nodeForm.Id);
                    //tree.AddSubMenu("{656A78A8-94C4-4E27-B4F2-BA130FBF084B}",           nodeFormMenuBar.Id);
                    tree.AddSaveMenuBarButton("{47384883-38DD-4C63-A7CE-E0DDDA990071}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}