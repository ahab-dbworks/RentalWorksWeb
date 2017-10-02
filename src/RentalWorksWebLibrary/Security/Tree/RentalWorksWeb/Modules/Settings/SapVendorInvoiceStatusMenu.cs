using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.Tree.RentalWorksWeb.Modules.Settings
{
    public class SapVendorInvoiceStatusMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public SapVendorInvoiceStatusMenu() : base("{1C8E14A3-73A8-4BB6-9B33-65D827B3ED0C}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{DC360463-43F1-437F-89E8-C78132BD622D}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{8FCB2ED1-14C9-4039-89E8-3D96943E32B6}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{59F95ECC-ED36-4AB4-A66D-C6EFE14B2406}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{3287E314-54EC-4485-BC29-3EF85D9A0D49}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{E6716C7D-A454-44A9-8CA6-525D3CFEE354}", nodeBrowseExport.Id);
            //tree.AddNewMenuBarButton("{F0D121C6-2B6F-4706-A21F-38EA61FC9779}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{50DA354A-CAD4-4F7B-86A8-47826CA9A0F3}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{F995BD7C-577C-4BCA-9B2A-09419BC10192}", nodeBrowseMenuBar.Id);
            //tree.AddDeleteMenuBarButton("{39C53D79-BE5F-4F21-82B1-34BD0828BBFE}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{CE53BDE5-693F-46EB-AE62-2B95BAEC5B64}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{BF0ACEB9-9BA9-416A-8F32-FEB8D2934540}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{1460C45C-3CB9-4F66-AA7B-9ECBD5EA052F}", nodeFormMenuBar.Id);
            tree.AddSaveMenuBarButton("{06EC005B-3FF0-4A6B-9F08-555E4E479D1D}", nodeFormMenuBar.Id);
        }

        //---------------------------------------------------------------------------------------------
    }
}