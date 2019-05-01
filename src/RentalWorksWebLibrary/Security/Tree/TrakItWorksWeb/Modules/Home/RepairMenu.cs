using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Settings
{
    public class RepairMenu : FwSecurityTreeBranch
    {
      //---------------------------------------------------------------------------------------------
        public RepairMenu() : base("{D567EC42-E74C-47AB-9CA8-764DC0F02D3B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{E25D5E77-8D61-4FF7-9230-682136CE1BB2}", MODULEID);
                var nodeBrowseMenuBar = tree.AddMenuBar("{26655CA5-B1A9-46D7-9191-13A5CA486CEE}", nodeBrowse.Id);
                    var nodeBrowseSubMenu = tree.AddSubMenu("{28C802EF-D59C-4FB5-B310-B219E3A2D71D}", nodeBrowseMenuBar.Id);
                        var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{B76B5921-665D-435A-8B68-3FC435791B35}", nodeBrowseSubMenu.Id);
                            tree.AddSubMenuItem("Void", "{4F0A3AF7-5CDF-4CCB-B7DF-8DFAF14AA516}", nodeBrowseOptions.Id);
                        var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{EC60DF7D-4C56-4B79-A31B-194AE9CB8C92}", nodeBrowseSubMenu.Id);
                            tree.AddDownloadExcelSubMenuItem("{AAD04C2D-EF17-4D22-AB66-707078C3CDF1}", nodeBrowseExport.Id);
                    tree.AddNewMenuBarButton("{F03DECD2-2C0E-40CA-94AC-C71D0C984E0B}", nodeBrowseMenuBar.Id);
                    tree.AddViewMenuBarButton("{2CB3B758-E82B-4E07-94D9-9BA893B421A1}", nodeBrowseMenuBar.Id);
                    tree.AddEditMenuBarButton("{45E2F304-74F2-4D6E-BBEC-2814894CBC24}", nodeBrowseMenuBar.Id);


            // Form
            var nodeForm = tree.AddForm("{D75632A8-F8D6-4AB4-954D-94C2FCA3F4F5}", MODULEID);
                var nodeFormMenuBar = tree.AddMenuBar("{BA2725C6-D3E1-44E3-8E46-E7A39C8D9DB5}", nodeForm.Id);
                    var nodeFormSubMenu = tree.AddSubMenu("{9BEE278D-6AE9-47DB-904B-16E881450A3B}", nodeFormMenuBar.Id);
                        var nodeFormOptions = tree.AddSubMenuGroup("Options", "{9FB550CC-2439-4465-9C2D-75657BBBB88C}", nodeFormSubMenu.Id);
                            tree.AddSubMenuItem("Estimate", "{8733EA9A-790E-4DF1-BFF2-13302A7DCD26}", nodeFormOptions.Id);
                            tree.AddSubMenuItem("Complete", "{136159CD-A50A-4BCA-AA28-4AB3A1BDC1CB}", nodeFormOptions.Id);
                            tree.AddSubMenuItem("Void", "{B048566D-9A69-488E-B7AA-BF243821E4B0}", nodeFormOptions.Id);
                            tree.AddSubMenuItem("Release Items", "{7A6B5CFD-1DFA-44BB-8B60-3FEE1E347654}", nodeFormOptions.Id);
                    tree.AddSaveMenuBarButton("{563749F4-4B3B-4A1A-B682-7AC82FB42A09}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}