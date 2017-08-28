using FwStandard.Security;

namespace RentalWorksWebLibrary.Security.SecurityTree.RentalWorksWeb.Modules.Settings
{
    public class QuoteMenu : FwSecurityTreeBranch
    {
        //---------------------------------------------------------------------------------------------
        public QuoteMenu() : base("{B430ACB9-FF8E-44C0-80B3-FFF82990745E}") { }
        //---------------------------------------------------------------------------------------------
        public override void BuildBranch(FwSecurityTree tree)
        {
            // Browse
            var nodeBrowse = tree.AddBrowse("{610922C6-3EFC-4F54-9A49-963509598441}", MODULEID);
                var nodeBrowseMenuBar = tree.AddMenuBar("{44DE93DE-C27B-4D70-AE18-14EAA7052AB4}", nodeBrowse.Id);
                    var nodeBrowseSubMenu = tree.AddSubMenu("{5EE3D9FC-B290-4ADA-80A8-DFEBD33212D4}", nodeBrowseMenuBar.Id);
                        var nodeBrowseOptions = tree.AddSubMenuGroup("Options", "{1E05FF65-89F1-438F-9F80-A3D2A8E323D8}", nodeBrowseSubMenu.Id);
                            tree.AddSubMenuItem("Cancel Quote...",              "{9724F8A1-9310-4589-8890-26A534922C9A}", nodeBrowseOptions.Id);
                            tree.AddSubMenuItem("Create Order",                 "{3B69EF6E-3E7D-408A-9793-CA12356CB774}", nodeBrowseOptions.Id);
                            tree.AddSubMenuItem("Lock Quote...",                "{41FE5788-2860-4D8F-ADA0-367F703680FA}", nodeBrowseOptions.Id);
                            tree.AddSubMenuItem("Make Quote Active...",         "{F8C128D1-5CB4-4766-9791-2546ADCA7AED}", nodeBrowseOptions.Id);
                            tree.AddSubMenuItem("New Version...",               "{89519868-3A0C-4365-98B9-93DA3855A55F}", nodeBrowseOptions.Id);
                            tree.AddSubMenuItem("Reserve Items...",             "{58A730B4-60B5-40BF-9D1E-95B5E8E64D88}", nodeBrowseOptions.Id);
                            tree.AddSubMenuItem("Calculate Total",              "{D389AD67-D401-4CB4-9E80-4FFF1F8483D2}", nodeBrowseOptions.Id);
                            tree.AddSubMenuItem("Purchase Orders...",           "{A7887B7C-FDED-4ECF-BAD1-3328358AAE9D}", nodeBrowseOptions.Id);
                            tree.AddSubMenuItem("Order Session...",             "{6A98D4D5-9C21-475C-B3C1-B0906C5073E7}", nodeBrowseOptions.Id);
                            tree.AddSubMenuItem("On Hold",                      "{BD3164CB-24B7-496F-8A12-320185BFC988}", nodeBrowseOptions.Id);
                            tree.AddSubMenuItem("Compare Quote/Order",          "{59673C75-3CFB-4767-B5A4-B853FED97368}", nodeBrowseOptions.Id);
                            tree.AddSubMenuItem("Copy Quote",                   "{3D1C0222-D98D-4DA6-B46D-BC4D1039E01B}", nodeBrowseOptions.Id);
                            tree.AddSubMenuItem("E-mail History",               "{2BE02C75-27FD-481B-B302-C2371398B4D4}", nodeBrowseOptions.Id);
                            tree.AddSubMenuItem("View Selected",                "{DED169A5-C893-4BA6-AD52-E44DF44AFA11}", nodeBrowseOptions.Id);
                            tree.AddSubMenuItem("View Unselected",              "{72B144E1-804D-4E0E-A073-743D6164B701}", nodeBrowseOptions.Id);
                            tree.AddSubMenuItem("Select",                       "{D4FF3823-0F55-4B2E-8176-DBFE1867BD8E}", nodeBrowseOptions.Id);
                            tree.AddSubMenuItem("Select All",                   "{D37CC217-E13B-4D43-B6B7-6B1011D7CC0B}", nodeBrowseOptions.Id);
                            tree.AddSubMenuItem("Clear Selected",               "{484440F7-D58D-4B85-B535-0D3E819BA492}", nodeBrowseOptions.Id);
                            tree.AddSubMenuItem("Search",                       "{A58BE5A2-8032-4092-8958-DEBA91FCBD91}", nodeBrowseOptions.Id);
                            tree.AddSubMenuItem("Refresh",                      "{95658EDC-7B9C-4AA4-9DA2-A76FC5BE143B}", nodeBrowseOptions.Id);
                        var nodeBrowseReports = tree.AddSubMenuGroup("Reports", "{5BD8223E-D318-497D-AC76-407C9B662B14}", nodeBrowseSubMenu.Id);
                            tree.AddSubMenuItem("Print Notes",                  "{F77BB7FE-2810-46DB-9B14-40B48B8B1214}", nodeBrowseReports.Id);
                            tree.AddSubMenuItem("Print Value Sheet",            "{FED31BF9-5E00-427F-A8D7-5857F6917EF6}", nodeBrowseReports.Id);
                            tree.AddSubMenuItem("Print Quote",                  "{22441CA4-CBCF-4F37-9643-822ABF7F2926}", nodeBrowseReports.Id);
                        var nodeBrowseExport = tree.AddSubMenuGroup("Export", "{0C3B8E15-452D-4352-BB73-E3E7E7718EEA}", nodeBrowseSubMenu.Id);
                            tree.AddDownloadExcelSubMenuItem("{07AFA5F3-2A4F-49DE-8317-958C0463DF99}", nodeBrowseExport.Id);
                    tree.AddNewMenuBarButton("{0B8C4C5F-727B-4CE1-ACA1-CB6FCFD74E7F}", nodeBrowseMenuBar.Id);
                    tree.AddViewMenuBarButton("{9EFF4447-6C0C-40AF-95F8-F753A8CD11D0}", nodeBrowseMenuBar.Id);
                    tree.AddEditMenuBarButton("{6337FE41-B413-42DE-B6D2-FE2FE978B892}", nodeBrowseMenuBar.Id);
                    tree.AddDeleteMenuBarButton("{6F530414-B73B-4862-9898-373D9101A93E}", nodeBrowseMenuBar.Id);

            // Form
            var nodeForm = tree.AddForm("{91484E0C-79E3-462A-ABD0-EF66A18100E2}", MODULEID);
                var nodeFormMenuBar = tree.AddMenuBar("{8EBDA89C-10C2-4B61-A541-14BB8D577400}", nodeForm.Id);
                    var nodeFormSubMenu = tree.AddSubMenu("{39797E19-66AE-4A91-AD11-8D9B4773D2D3}", nodeFormMenuBar.Id);
                        var nodeFormQuoteOptions = tree.AddSubMenuGroup("Quote Options", "{C05D902E-72BA-4A0A-B4CB-D8DC3D944E1B}", nodeFormSubMenu.Id);
                            tree.AddSubMenuItem("Cancel Quote...",                  "{EE609226-3079-4BAE-98B0-9DDD55926956}", nodeFormQuoteOptions.Id);
                            tree.AddSubMenuItem("Create Order",                     "{CC160B85-F234-4AE0-9B66-9A5BDF985F3A}", nodeFormQuoteOptions.Id);
                            tree.AddSubMenuItem("Lock Quote...",                    "{024E6015-7C75-40EB-908E-34BEE82F9D9F}", nodeFormQuoteOptions.Id);
                            tree.AddSubMenuItem("Make Quote Active...",             "{9DCA46A1-414B-4F14-8357-C9212A268C5E}", nodeFormQuoteOptions.Id);
                            tree.AddSubMenuItem("New Version...",                   "{CD335B5B-90A6-40D5-B558-73B1DF70C6F6}", nodeFormQuoteOptions.Id);
                            tree.AddSubMenuItem("Reserve Items...",                 "{9A797569-B553-48DD-A364-6B880F740BF0}", nodeFormQuoteOptions.Id);
                            tree.AddSubMenuItem("Copy Quote",                       "{3B39D9AF-8C1B-4002-8526-57C86598CC2B}", nodeFormQuoteOptions.Id);
                            tree.AddSubMenuItem("On Hold",                          "{B4236E72-59F5-4B05-96D8-42C85EE38577}", nodeFormQuoteOptions.Id);
                        var nodeFormView = tree.AddSubMenuGroup("View", "{95D440FC-58A9-48C9-82EA-79CCD354932F}", nodeFormSubMenu.Id);
                            tree.AddSubMenuItem("Deal Schedule...",                 "{0CDE49F3-DC69-405E-998C-2D6821841FF6}", nodeFormView.Id);
                            tree.AddSubMenuItem("Billing Schedule...",              "{7F32EEA4-2361-4205-9DD9-50861E661535}", nodeFormView.Id);
                            tree.AddSubMenuItem("Episodic Schedule...",             "{FE6EEC3A-80EE-4709-A4B9-31F77E94996A}", nodeFormView.Id);
                            tree.AddSubMenuItem("Hiatus Schedule...",               "{FE0DC35F-FA4E-4E23-94E7-D7B0C4732E56}", nodeFormView.Id);
                            tree.AddSubMenuItem("Itinerary...",                     "{48705055-062C-4702-95E2-D09C755CBD24}", nodeFormView.Id);
                            tree.AddSubMenuItem("Daily Schedule...",                "{F446CC1B-55E0-4375-8646-786A9CBE581D}", nodeFormView.Id);
                            tree.AddSubMenuItem("Order Sessions...",                "{679C4E2E-897A-48BE-9589-EDF81237174A}", nodeFormView.Id);
                            tree.AddSubMenuItem("Session...",                       "{63771D6B-94DA-496C-BD6F-D208111C9001}", nodeFormView.Id);
                            tree.AddSubMenuItem("Event...",                         "{2C0D4F89-38DD-4F16-AA27-0002F163EC56}", nodeFormView.Id);
                            tree.AddSubMenuItem("Order Group...",                   "{AA02E0E7-F6D9-4763-A482-962C0C58F00E}", nodeFormView.Id);
                            tree.AddSubMenuItem("Documents",                        "{5085D327-75E4-4FEF-9128-8D40F02F527E}", nodeFormView.Id);
                            tree.AddSubMenuItem("E-mail History",                   "{BDD5D44C-CCF6-408B-ADC1-767A6F1AC74F}", nodeFormView.Id);
                            tree.AddSubMenuItem("Associated Order",                 "{C1415313-98D6-4E1A-B313-FCA9FE95186D}", nodeFormView.Id);
                        var nodeFormReports = tree.AddSubMenuGroup("Reports", "{6EE3C153-5FEA-4976-92F1-14579A2C74DF}", nodeFormSubMenu.Id);
                            tree.AddSubMenuItem("Print Quote...",                   "{464950F7-206E-4245-A50F-917B5D5D122B}", nodeFormReports.Id);
                            tree.AddSubMenuItem("Print Notes...",                   "{807C5D7D-EBF4-4A19-A27E-CC1E589305AE}", nodeFormReports.Id);
                            tree.AddSubMenuItem("Print Value Sheet...",             "{DFEB67FD-0C73-43D8-A8A4-F4A63A1461FD}", nodeFormReports.Id);
                            tree.AddSubMenuItem("Print Deal Outstanding",           "{AE8146A0-29E4-45FD-B4BA-451B59501580}", nodeFormReports.Id);
                            tree.AddSubMenuItem("Print Quote/Order Comparison...",  "{59FE686E-DB98-42C1-AB72-F4E9D31AD61D}", nodeFormReports.Id);
                        var nodeFormTabs = tree.AddSubMenuGroup("Tabs", "{7CAEB228-BA4B-45C4-8589-2D13B63D063A}", nodeFormSubMenu.Id);
                            tree.AddSubMenuItem("Hide Billing Tab",                 "{1F41860B-0FDC-4F7E-B0B8-9C3141FEA6B3}", nodeFormTabs.Id);
                            tree.AddSubMenuItem("Show Deliver/Ship Tab",            "{E4F3D627-6F33-4437-A343-8924678E2D47}", nodeFormTabs.Id);
                            tree.AddSubMenuItem("Hide Contacts Tab",                "{80429E8D-628D-44E1-9756-9809B238445C}", nodeFormTabs.Id);
                            tree.AddSubMenuItem("Show Notes Tab",                   "{F5628D8E-E731-445B-AF37-16812D26FB14}", nodeFormTabs.Id);
                            tree.AddSubMenuItem("Show Contract Notes Tab",          "{0446B4CF-BD25-4549-89B7-7A6777D39639}", nodeFormTabs.Id);
                            tree.AddSubMenuItem("Hide Summary Tab",                 "{24531D93-5756-4E97-9EA9-832089E7AE16}", nodeFormTabs.Id);

                tree.AddSaveMenuBarButton("{10EA256F-98CE-4FD6-809A-B6AC1D1AC912}", nodeFormMenuBar.Id);
        }
        //---------------------------------------------------------------------------------------------
    }
}