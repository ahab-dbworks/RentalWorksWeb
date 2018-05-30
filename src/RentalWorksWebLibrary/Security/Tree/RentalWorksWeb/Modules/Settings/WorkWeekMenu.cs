using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
  public class WorkWeekMenu : FwSecurityTreeBranch
  {
    //---------------------------------------------------------------------------------------------
    public WorkWeekMenu() : base("{AF91AE34-ADED-4A5A-BD03-113ED817F575}") { }
    //---------------------------------------------------------------------------------------------
    public override void BuildBranch(FwSecurityTree tree)
    {
      // Browse
      var nodeBrowse = tree.AddBrowse("{C2C3DD2F-B6C7-46BC-9058-36D3C35B0D49}", MODULEID);
      var nodeBrowseMenuBar = tree.AddMenuBar("{B99F183C-3A20-4D72-AA82-B7899AF38CEC}", nodeBrowse.Id);
      var nodeBrowseSubMenu = tree.AddSubMenu("{6E0BBBD5-3B59-4E28-85EF-CDDB0BF2C62F}", nodeBrowseMenuBar.Id);
      var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{3FC17DAC-252C-4BEA-AB2A-275F9BA7DBB6}", nodeBrowseSubMenu.Id);
      tree.AddDownloadExcelSubMenuItem("{28AEC4E1-DE03-41F3-9494-69D412D1A2BD}", nodeBrowseExport.Id);
      tree.AddNewMenuBarButton("{9B409DC4-B689-4C8A-B2B6-BB650552A45C}", nodeBrowseMenuBar.Id);
      tree.AddViewMenuBarButton("{07FD678F-731C-4A3E-8D75-90FCADAE48B8}", nodeBrowseMenuBar.Id);
      tree.AddEditMenuBarButton("{0DCCE5FF-DA5B-42B9-9C0F-999A306F9212}", nodeBrowseMenuBar.Id);
      tree.AddDeleteMenuBarButton("{694EF9F0-F354-45C0-8488-5BAA625A9C76}", nodeBrowseMenuBar.Id);

      // Form
      var nodeForm = tree.AddForm("{0018861C-A25C-4567-9BA8-3CE82573C824}", MODULEID);
      var nodeFormMenuBar = tree.AddMenuBar("{ABF96997-6D3D-4DAA-979E-D7018FDE67F1}", nodeForm.Id);
      var nodeFormSubMenu = tree.AddSubMenu("{7C7DA8F2-51A6-4D41-98C4-B56C952F3595}", nodeFormMenuBar.Id);
      tree.AddSaveMenuBarButton("{A85D8640-2678-4E4A-B2CB-41F058374454}", nodeFormMenuBar.Id);
    }

    //---------------------------------------------------------------------------------------------
  }
}




