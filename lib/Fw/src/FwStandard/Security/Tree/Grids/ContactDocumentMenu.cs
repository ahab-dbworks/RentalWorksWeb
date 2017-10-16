namespace FwStandard.Security.Tree.Grids
{
    public class ContactDocumentMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public ContactDocumentMenu() : base("{CC8F52FF-D968-4CE6-BF7A-3AC859D25280}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{97DB2A1D-936D-45C5-99CB-3290810D56C2}", MODULEID);
                var nodeGridSubMenu = tree.AddSubMenu("{CB13DDC0-0034-4AC8-B50E-59A7C5A3FAB3}", nodeGridMenuBar.Id);
                    var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{72936FCB-3F79-4931-B775-D56167F1B568}", nodeGridSubMenu.Id);
                        tree.AddSubMenuItem("Toggle Active / Inactive",   "{CFF9AAC6-20B0-4030-AC36-DAE119F0E18F}", nodeBrowseOptions.Id);
                tree.AddNewMenuBarButton("{D1CA4F4D-990B-415B-8A54-AE5831E69C91}",    nodeGridMenuBar.Id);
                tree.AddEditMenuBarButton("{21ACCB07-6B4D-4892-B44E-E79347296896}",   nodeGridMenuBar.Id);
                tree.AddDeleteMenuBarButton("{4E7D9BAE-B7A2-4216-9F7C-1E3720A7A787}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}