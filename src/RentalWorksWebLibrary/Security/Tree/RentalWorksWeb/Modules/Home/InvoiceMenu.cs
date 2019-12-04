using FwStandard.Security;

namespace WebLibrary.Security.Tree.RentalWorksWeb.Modules.Home
{
    public class InvoiceMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public InvoiceMenu() : base("{9B79D7D8-08A1-4F6B-AC0A-028DFA9FE10F}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{D4669903-0F3E-431C-91AB-11EBEF63540F}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{7CFC54FC-1069-41F8-AE8C-581B86B38B56}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{8605C5D5-F2AE-43D0-811C-BB48A928E4A7}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{D180C720-4C8B-41A8-9821-613F5A9B7497}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{A727B974-FA3E-4FAB-9A67-205EB27201A1}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{B4437CE8-7298-4855-B419-94E049D55161}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{D8A8C55D-9A76-49F3-8018-A986A143EDEC}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{7AFA98E2-271C-465E-9244-1F45145383E9}", nodeBrowseMenuBar.Id);
            tree.AddSubMenuItem("Void", "{DACF4B06-DE63-4867-A684-4C77199D6961}", nodeBrowseExport.Id);
            tree.AddSubMenuItem("Approve", "{9D1A3607-EE4A-49E6-8EAE-DB3E0FF06EAE}", nodeBrowseExport.Id);
            tree.AddSubMenuItem("Unapprove", "{F9D43CB6-2666-4AE0-B35C-77735561B9B9}", nodeBrowseExport.Id);

            // Form
            var nodeForm = tree.AddForm("{64D39696-50EA-4CEE-B81A-B69E47D1BE3F}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{80456F89-4325-4C90-AE11-9757EEDEF5F3}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{CCF32B89-208E-4386-9371-FAB173B1D200}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{6D8A13F3-3E21-4286-89D0-0A6B62003A0E}", nodeFormSubMenu.Id);
            tree.AddSaveMenuBarButton("{FB73E264-2087-4ED5-9184-B625A69C2AD7}", nodeFormMenuBar.Id);
            tree.AddSubMenuItem("Void", "{DF6B0708-EC5A-475F-8EFB-B52E30BACAA3}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Print Invoice", "{3A693D4E-3B9B-4749-A9B6-C8302F1EDE6A}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Approve", "{117CCDFA-FFC3-49CE-B41B-0F6CE9A69518}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Unapprove", "{F8C5F06C-4B9D-4495-B589-B44B02AE7915}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Credit Invoice", "{CC80D0FC-2E28-4C3D-83EB-C8B5EE0CB4B5}", nodeFormOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}