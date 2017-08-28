using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.SecurityTree.RentalWorksWeb.Modules.Settings
{
    public class CustomerMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public CustomerMenu() : base("{4A76FD04-998B-4F0E-8BF4-E72938924C21}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{1D71DE36-DF85-4DF1-8D50-9366D1BFCEAC}", MODULEID);
                var nodeBrowseMenuBar = tree.AddMenuBar("{3BC5FE3A-075D-4CE9-9C66-6484308FB8A9}", nodeBrowse.Id);
                    var nodeBrowseSubMenu = tree.AddSubMenu("{3159351C-8411-47B9-B310-93238C7A993D}", nodeBrowseMenuBar.Id);
                        var nodeBrowseView = tree.AddSubMenuGroup("View", "{7921CDF8-6AEB-4ADA-A6C0-AAAEE5FE0FA6}", nodeBrowseSubMenu.Id);
                            tree.AddSubMenuItem("A/R Aging...",         "{006F95B4-4771-4521-A7EC-D272030DC370}", nodeBrowseView.Id);
                            tree.AddSubMenuItem("Contacts...",          "{35707616-9B96-4F9C-BE2B-0ACFB2CFB577}", nodeBrowseView.Id);
                            tree.AddSubMenuItem("Deal...",              "{7B6F8E94-A91E-4CB9-8508-ECD21EDA0C19}", nodeBrowseView.Id);
                            tree.AddSubMenuItem("Security Deposit...",  "{6C732CFF-52FB-4924-A67C-E5ABC7DE953F}", nodeBrowseView.Id);
                            tree.AddSubMenuItem("Vendor...",            "{2704D2B4-9119-4B20-AFC8-D0249BD5594A}", nodeBrowseView.Id);
                        var nodeBrowseHistory = tree.AddSubMenuGroup("History", "{C7CDD9D1-8822-4604-9872-097A9BD3B79D}", nodeBrowseSubMenu.Id);
                            tree.AddSubMenuItem("Invoice History...",   "{4960499D-8383-4042-8F51-8179FC107F51}", nodeBrowseHistory.Id);
                            tree.AddSubMenuItem("Order History",        "{C831BB38-ACC3-4A7A-AC9C-BF24DF8280A3}", nodeBrowseHistory.Id);
                            tree.AddSubMenuItem("Revenue...",           "{345CC9F2-17BE-47EA-975B-50D277F89EC1}", nodeBrowseHistory.Id);
                        var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{9CF6C8F4-CBA6-4B3F-A776-2E53348078DE}", nodeBrowseSubMenu.Id);
                            tree.AddDownloadExcelSubMenuItem("{80FCFAB5-B0A0-4C7E-B04D-E4EEA037C0B4}", nodeBrowseExport.Id);
                    tree.AddNewMenuBarButton("{4B774C0A-A200-4D1D-A74B-DF9C4D105663}", nodeBrowseMenuBar.Id);
                    tree.AddViewMenuBarButton("{5A2B0E52-0B7D-48BB-8530-C0868E397C10}", nodeBrowseMenuBar.Id);
                    tree.AddEditMenuBarButton("{600CD116-3E82-47C2-AE80-1E820F0BF425}", nodeBrowseMenuBar.Id);
                    tree.AddDeleteMenuBarButton("{E3B5509E-C64C-4D7B-919B-FB45B615ACF1}", nodeBrowseMenuBar.Id);


            // Form
            var nodeForm = tree.AddForm("{B717BA49-7D86-4A4A-A7A9-A06F07F9CABD}", MODULEID);
                var nodeFormMenuBar = tree.AddMenuBar("{609D9229-82C9-48BD-AF76-978CC1D97BFE}", nodeForm.Id);
                    var nodeFormSubMenu = tree.AddSubMenu("{B0D55CC2-DEE3-493A-B76C-C19D994C01EE}", nodeFormMenuBar.Id);
                tree.AddSaveMenuBarButton("{EABE2DF2-90A5-4738-9F99-F2F2019FE264}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}