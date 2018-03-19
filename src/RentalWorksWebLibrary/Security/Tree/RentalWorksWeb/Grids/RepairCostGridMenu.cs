using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
  public class RepairCostGridMenu : FwSecurityTreeBranch
  {
    //---------------------------------------------------------------------------------------------
    public RepairCostGridMenu() : base("{38219D4D-C8F6-4C8C-B86B-D86D5F645251}") { }
    //---------------------------------------------------------------------------------------------
    public override void BuildBranch(FwSecurityTree tree)
    {
      var nodeGridMenuBar = tree.AddMenuBar("{08D9E43C-033D-4656-B94A-BDFC550E5D67}", MODULEID);
        tree.AddNewMenuBarButton("{626EF8E0-42C4-4C57-803E-2B72C2269126}", nodeGridMenuBar.Id);
        tree.AddEditMenuBarButton("{A8BC2E60-4858-428E-9D45-D1DD459DD0C4}", nodeGridMenuBar.Id);
        tree.AddDeleteMenuBarButton("{9DA5CD25-C71E-491C-AF08-DA68F32A3B4F}", nodeGridMenuBar.Id);
    }
      //---------------------------------------------------------------------------------------------
  }
}