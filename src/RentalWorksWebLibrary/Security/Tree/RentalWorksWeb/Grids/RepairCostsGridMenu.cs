using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Grids
{
  public class RepairCostsGridMenu : FwSecurityTreeBranch
  {
    //---------------------------------------------------------------------------------------------
    public RepairCostsGridMenu() : base("{38219D4D-C8F6-4C8C-B86B-D86D5F645251}") { }
    //---------------------------------------------------------------------------------------------
    public override void BuildBranch(FwSecurityTree tree)
    {
      var nodeGridMenuBar = tree.AddMenuBar("{08D9E43C-033D-4656-B94A-BDFC550E5D67}", MODULEID);
      var nodeGridSubMenu = tree.AddSubMenu("{D2A12DCC-A47A-4107-8F1F-3E7D5B190C14}", nodeGridMenuBar.Id);
      var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{30650E90C-1902-4311-9C48-E00C55E421C8}", nodeGridSubMenu.Id);
      tree.AddNewMenuBarButton("{626EF8E0-42C4-4C57-803E-2B72C2269126}", nodeGridMenuBar.Id);
      tree.AddEditMenuBarButton("{A8BC2E60-4858-428E-9D45-D1DD459DD0C4}", nodeGridMenuBar.Id);
      tree.AddDeleteMenuBarButton("{5217E33B-C715-4A8A-8983-668FF19E1F1B}", nodeGridMenuBar.Id);
    }
      //---------------------------------------------------------------------------------------------
  }
}