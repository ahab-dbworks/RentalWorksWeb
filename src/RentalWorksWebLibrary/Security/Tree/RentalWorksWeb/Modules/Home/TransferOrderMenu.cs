using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FwStandard.Security;

namespace Web.Source.Modules
{
    public class TransferOrderMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public TransferOrderMenu() : base("{F089C9A9-554D-40BF-B1FA-015FEDE43591}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{7912F35C-A6A1-4577-AA21-989F76A4C33E}", MODULEID);
            var nodeBrowseMenuBar = tree.AddMenuBar("{545ADFCD-2FF3-400C-8D61-2CF782D39CA0}", nodeBrowse.Id);
            var nodeBrowseSubMenu = tree.AddSubMenu("{6D97C7F9-D199-4BB3-BFB4-04543438967C}", nodeBrowseMenuBar.Id);
            var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{7238CA3B-EA78-4110-9F57-142E3F8670EB}", nodeBrowseSubMenu.Id);
            tree.AddDownloadExcelSubMenuItem("{6B202E80-A3F9-459E-83F2-E942774E7517}", nodeBrowseExport.Id);
            tree.AddNewMenuBarButton("{47A056D4-346B-4B96-A986-C5D1F20F9D4D}", nodeBrowseMenuBar.Id);
            tree.AddViewMenuBarButton("{C350869D-5A09-4D2A-A50B-D9B116F62D541}", nodeBrowseMenuBar.Id);
            tree.AddEditMenuBarButton("{3DF84BF8-D5E6-44FE-BBA7-8D2254A104E7}", nodeBrowseMenuBar.Id);
            tree.AddDeleteMenuBarButton("{8FE0BD8E-9CE1-4706-AA3F-EB6A1C37C3A4}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{4E657588-CDA6-4079-A120-E806774F0E66}", MODULEID);
            var nodeFormMenuBar = tree.AddMenuBar("{F64A8D6F-821F-4C37-AD54-B4F40CF0E23C}", nodeForm.Id);
            var nodeFormSubMenu = tree.AddSubMenu("{C35FFD5A-E220-4ED2-A339-B316B1D43AF2}", nodeFormMenuBar.Id);
            var nodeFormOptions = tree.AddSubMenuGroup("Options", "{3CCEAD12-3C4D-450E-893C-E8F67CF8683A}", nodeFormSubMenu.Id);
            tree.AddSaveMenuBarButton("{16B13392-2610-4BDF-8D19-62F1A425DCA3}", nodeFormMenuBar.Id);
            tree.AddSubMenuItem("Search", "{EE207266-01FC-4D0E-8469-48F5B099ED71}", nodeFormOptions.Id); // mv 5/1/19 - added this because it was implemented on the front-end, not sure if this works
            tree.AddSubMenuItem("Confirm", "{A35F0AAD-81B5-4A0C-8970-D448A67D5A82}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Create Pick List", "{5CA07E25-A93E-4FA0-9206-B3F556684B0C}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Transfer Status", "{A256288F-238F-4594-8A6A-3B70613925DA}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Transfer Out", "{D0AB3734-7F96-46A6-8297-331110A4854F}", nodeFormOptions.Id);
            tree.AddSubMenuItem("Transfer In", "{E362D71D-7597-4752-8BDD-72EE0CB7B2C4}", nodeFormOptions.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}