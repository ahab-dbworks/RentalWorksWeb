using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Grids
{
    public class InventoryKitGridMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InventoryKitGridMenu() : base("{1414AF19-51A3-4455-A554-4293484F7C28}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            var nodeGridMenuBar = tree.AddMenuBar("{670EBF38-0DCD-4658-95A6-20D9860442FC}", MODULEID);
            var nodeGridSubMenu = tree.AddSubMenu("{CA3AD534-C70D-4D26-845D-99E865CC99E5}", nodeGridMenuBar.Id);
            var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{48F6F88D-DFB9-41EB-8A42-0F52F35478A7}", nodeGridSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{3D8DE02A-7846-4186-BD21-50C13BA6F9F1}", nodeBrowseOptions.Id);
            tree.AddEditMenuBarButton("{9669CC66-0FB5-4D77-B7FA-C1C61EE9F29D}", nodeGridMenuBar.Id);
            tree.AddNewMenuBarButton("{0293B129-8664-45C0-AB02-7E87EDBC1B46}", nodeGridMenuBar.Id);
            tree.AddDeleteMenuBarButton("{E698826E-95B3-4B81-9F9F-42E009B9C86A}", nodeGridMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}