using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
  public class RepairPartGridMenu : FwSecurityTreeBranch
  {
    //---------------------------------------------------------------------------------------------
    public RepairPartGridMenu() : base("{D3EB3232-9976-4607-A86F-7D64DF2AD4F8}") { }
    //---------------------------------------------------------------------------------------------
    public override void BuildBranch(FwSecurityTree tree)
    {
      var nodeGridMenuBar = tree.AddMenuBar("{CB941EF8-ED90-451C-9A65-55CA55E6222A}", MODULEID);
        tree.AddNewMenuBarButton("{F0572D10-6C5F-4D23-AD87-D3342B6F7925}", nodeGridMenuBar.Id);
        tree.AddEditMenuBarButton("{E1663AC2-57E0-4323-94D9-F164342143EA}", nodeGridMenuBar.Id);
        tree.AddDeleteMenuBarButton("{ 28DC2900-A74C-4466-92A0-5D421EF00915 }", nodeGridMenuBar.Id);
    }
    //---------------------------------------------------------------------------------------------
  }
}