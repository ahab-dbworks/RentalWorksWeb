using FwStandard.Security;

namespace WebLibrary.Security.Tree.TrakitWorksWeb.Modules.Administrator
{
    public class HotfixMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public HotfixMenu() : base("{B7336B5E-4BA4-4A99-97D6-60385045238B}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{1CA5CEB0-3C0F-498E-834A-EC72CF1982CD}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{9F56C313-0254-463C-80A3-F892521F79EA}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{6B2ACFAE-47C6-4145-B31A-7647CC2D821E}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{98945D05-7C62-4835-9EB2-961E98234B02}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{DF4F9368-5353-4570-87F5-F4DEF23FECEB}", nodeBrowseExport.Id);
            tree.AddViewMenuBarButton("{69F58886-1B65-4283-A484-DF526BE3FF87}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{09458418-3652-47F1-819C-C1ADC951CA71}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{2DCBA9FC-839D-40BE-BE83-E256B49D447B}", nodeForm.Id);
            //var nodeFormSubMenu = tree.AddSubMenu("{C798190C-01B7-487D-834A-E4416CF63D3B}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}
