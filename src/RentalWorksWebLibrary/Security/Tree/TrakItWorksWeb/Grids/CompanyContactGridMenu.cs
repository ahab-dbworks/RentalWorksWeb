using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class CompanyContactGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CompanyContactGridMenu() : base("{6D8B3D23-0954-4765-9FBD-BF3EC756AA97}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{0B688712-E6A1-450C-A5C9-4F9957A0EDD0}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{036DC657-9017-4B6D-AC91-867F8F0954D4}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{528A9C2D-C1A2-4D14-AF9F-DED358B5524C}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{5BBFFEE6-E40D-4229-BF8F-3147CB947A76}", nodeBrowseOptions.Id);
            tree.AddNewMenuBarButton("{5D05B0FB-5758-4508-BE7D-1B645FD67279}", nodeGridMenuBar.Id);
            tree.AddEditMenuBarButton("{AF203FA0-8619-47EE-9C44-3EE4710B59EF}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{E4D018EF-2C57-4260-9024-136B4D18F803}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}