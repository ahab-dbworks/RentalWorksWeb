using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalworkWeb.Modules.Administrator
{
    public class GroupMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public GroupMenu() : base("{9BE101B6-B406-4253-B2C6-D0571C7E5916}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{E005ADD8-776D-49BC-ABE8-0E7C51002E3E}", MODULEID);
                var nodeBrowseMenuBar = tree.AddMenuBar("{3A52B884-A503-458C-AF6E-A7954F339374}", nodeBrowse.Id);
                    var nodeBrowseSubMenu = tree.AddSubMenu("{47D4BA00-EBB4-4D32-A6CC-2D8A09971AD9}", nodeBrowseMenuBar.Id);
                        var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{15F55648-3D88-4FEB-9127-EAAA2A6DF617}", nodeBrowseSubMenu.Id);
                            //tree.AddSubMenuItem("Import Security Tree...", "{28C16B0D-70CD-461B-A78E-967135300B56}", nodeBrowseExport.Id);
                            //tree.AddSubMenuItem("Export Security Tree...", "{0324DFE7-D8E5-4BE7-8F3C-3D18B6AF8469}", nodeBrowseExport.Id);
                            tree.AddDownloadExcelSubMenuItem("{E35B847D-4E51-4218-AADD-B79ACD6725C0}", nodeBrowseExport.Id);
                    tree.AddNewMenuBarButton("{201DF079-608D-49CB-9237-1446B700D206}",    nodeBrowseMenuBar.Id);
                    tree.AddViewMenuBarButton("{F61F79E5-98B1-426A-A0B8-89C80CB9B771}",   nodeBrowseMenuBar.Id);
                    tree.AddEditMenuBarButton("{80211450-9476-4662-96FE-D488B959550A}",   nodeBrowseMenuBar.Id);
                    tree.AddDeleteMenuBarButton("{337EF960-9B29-45B3-994D-65A9D0368293}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{E7DCC9A9-8C1B-469B-8F3D-E24D55A3D4AE}", MODULEID);
                var nodeFormMenuBar = tree.AddMenuBar("{7AAB97C6-B13C-4884-8501-5C437A469402}", nodeForm.Id);
                    tree.AddSaveMenuBarButton("{21310FEB-56C1-4768-B6C4-01BBC4748CEF}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}