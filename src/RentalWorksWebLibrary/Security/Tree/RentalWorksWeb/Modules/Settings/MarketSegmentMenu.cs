using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
  public class MarketSegmentMenu : FwSecurityTreeBranch
  {
    //---------------------------------------------------------------------------------------------
    public MarketSegmentMenu() : base("{53B627BE-6AC8-4C1F-BEF4-E8B0A5422E14}") { }
    //---------------------------------------------------------------------------------------------
    public override void BuildBranch(FwSecurityTree tree)
    {
      // Browse
      var nodeBrowse = tree.AddBrowse("{7A37DB0E-8675-4D0E-B8BE-1265380CF97F}", MODULEID);
      var nodeBrowseMenuBar = tree.AddMenuBar("{3AB5676F-EF29-4C09-9B47-C28366FD5929}", nodeBrowse.Id);
      var nodeBrowseSubMenu = tree.AddSubMenu("{012B1732-C99F-48DE-88C2-AE67CE046DCF}", nodeBrowseMenuBar.Id);
      var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{17563F7E-B6AD-4A94-942E-211F8B2805F1}", nodeBrowseSubMenu.Id);
      tree.AddDownloadExcelSubMenuItem("{38C2EC1D-3438-4A86-A8C0-D171FD892394}", nodeBrowseExport.Id);
      tree.AddNewMenuBarButton("{F5ED7EA9-3775-4E2A-B541-71FA1048E7FF}", nodeBrowseMenuBar.Id);
      tree.AddViewMenuBarButton("{759DC1B8-138E-439C-A894-E04C1F073482}", nodeBrowseMenuBar.Id);
      tree.AddEditMenuBarButton("{6E0F96DA-F1E4-49D2-8E1C-EB0F0556DD09}", nodeBrowseMenuBar.Id);
      tree.AddDeleteMenuBarButton("{35D6725B-8C76-4CF5-A28B-3A8021F2DEB1}", nodeBrowseMenuBar.Id);

      // Form
      var nodeForm = tree.AddForm("{83A1407A-EA0E-4BA1-864B-8CA00786F106}", MODULEID);
      var nodeFormMenuBar = tree.AddMenuBar("{D47DACCC-C006-497B-9962-D829DCEF4AAE}", nodeForm.Id);
      var nodeFormSubMenu = tree.AddSubMenu("{9A1658C5-BDD2-4FF4-89EB-FBB2839673A4}", nodeFormMenuBar.Id);
      tree.AddSaveMenuBarButton("{81B8ACF2-8C09-4634-B993-EA5C6D9DEBB1}", nodeFormMenuBar.Id);
    }

    //---------------------------------------------------------------------------------------------
  }
}




