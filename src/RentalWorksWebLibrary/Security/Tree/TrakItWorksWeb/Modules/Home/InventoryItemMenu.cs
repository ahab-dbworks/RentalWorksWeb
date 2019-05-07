using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class InventoryItemMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryItemMenu() : base("{803A2616-4DB6-4BAC-8845-ECAD34C369A8}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{4AA21C3C-853B-469E-BBA0-57AF4F6D8147}", MODULEID);
                var nodeBrowseMenuBar = tree.AddMenuBar("{D736CB7D-DB6F-4BCF-AC08-C3BC94385FB6}", nodeBrowse.Id);
                    var nodeBrowseSubMenu = tree.AddSubMenu("{99982E47-8ABC-47AA-A7A8-109F3B2A93FC}", nodeBrowseMenuBar.Id);
                        var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{B77FEEB1-52B2-463A-B314-96E3CA3C02FD}", nodeBrowseSubMenu.Id);
                            tree.AddDownloadExcelSubMenuItem("{AC6202CD-5CA3-4AF4-9DFC-40DE609F30BF}", nodeBrowseExport.Id);
                    tree.AddNewMenuBarButton("{D55F8A6B-28B7-4A3C-AC02-5BA3BEA432AC}", nodeBrowseMenuBar.Id);
                    tree.AddViewMenuBarButton("{D43C1A68-5FED-4F59-A74E-5E1DE1E2E91B}", nodeBrowseMenuBar.Id);
                    tree.AddEditMenuBarButton("{3659B722-2667-4880-95DD-29505F3981DB}", nodeBrowseMenuBar.Id);
                    tree.AddDeleteMenuBarButton("{EE7E0039-F1E7-4D4B-8E6B-DD91F2BD4039}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{D144AB36-BA45-496F-80E4-7462795D5C50}", MODULEID);
                var nodeFormMenuBar = tree.AddMenuBar("{7E3981A8-EC28-43D6-A2DB-BB8E664D0A9E}", nodeForm.Id);
                    //var nodeFormSubMenu = tree.AddSubMenu("{22B01277-DC78-47D9-871B-464E40E4156C}", nodeFormMenuBar.Id);
                    tree.AddSaveMenuBarButton("{E1616D54-4C9C-4986-99AA-08F45CE15C35}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}