class RwMaster extends WebMaster {
    //----------------------------------------------------------------------------------------------
    navigation: any[];
    settings: any;
    //----------------------------------------------------------------------------------------------
    initMainMenu() {
        let userType = sessionStorage.getItem('userType');
        const controlDefaults = JSON.parse(sessionStorage.getItem('controldefaults'));

        if (controlDefaults === null) {
            program.navigate('logoff');
            return;
        }
        //const applicationOptions = JSON.parse(sessionStorage.getItem('applicationOptions'));
        this.navigation = [];

        // Agent Menu
        let menuAgent = {
            caption: 'Agent',
            id: 'Agent',
            icon: 'person',
            children: []
        };
        menuAgent.children.push(Constants.Modules.Agent.children.Contact);
        menuAgent.children.push(Constants.Modules.Agent.children.Customer);
        menuAgent.children.push(Constants.Modules.Agent.children.Deal);
        menuAgent.children.push(Constants.Modules.Agent.children.Order);
        menuAgent.children.push(Constants.Modules.Agent.children.Project);
        menuAgent.children.push(Constants.Modules.Agent.children.PurchaseOrder);
        menuAgent.children.push(Constants.Modules.Agent.children.Quote);
        menuAgent.children.push(Constants.Modules.Agent.children.Vendor);

        this.navigation.push(menuAgent);

        // Billing Menu
        let menuBilling = {
            caption: 'Billing',
            id: 'Billing',
            icon: 'attach_money',
            children: []
        };
        if (controlDefaults.enablepayments) {
            menuBilling.children.push(Constants.Modules.Billing.children.BankAccount);
        }
        menuBilling.children.push(Constants.Modules.Billing.children.Billing);
        menuBilling.children.push(Constants.Modules.Billing.children.BillingWorksheet);
        menuBilling.children.push(Constants.Modules.Billing.children.Invoice);
        if (controlDefaults.enablepayments) {
            menuBilling.children.push(Constants.Modules.Billing.children.Payment);
        }
        if (controlDefaults.enablereceipts) {
            menuBilling.children.push(Constants.Modules.Billing.children.Receipt);
        }
        menuBilling.children.push(Constants.Modules.Billing.children.VendorInvoice);
        
        this.navigation.push(menuBilling);

        // Inventory Menu
        let menuInventory = {
            caption: 'Inventory',
            id: 'Inventory',
            icon: 'storage',
            children: [
                Constants.Modules.Inventory.children.Asset,
                Constants.Modules.Inventory.children.AvailabilityConflicts,
                Constants.Modules.Inventory.children.CompleteQc,
                Constants.Modules.Inventory.children.InventorySummary,
                Constants.Modules.Inventory.children.PartsInventory,
                Constants.Modules.Inventory.children.PurchaseHistory,
                Constants.Modules.Inventory.children.PhysicalInventory,
                Constants.Modules.Inventory.children.QuikSearch,
                Constants.Modules.Inventory.children.RentalInventory,
                Constants.Modules.Inventory.children.Repair,
                Constants.Modules.Inventory.children.SalesInventory
            ]
        };
        if (JSON.parse(sessionStorage.getItem('controldefaults')).multiwarehouse) {
            menuInventory.children.push(Constants.Modules.Transfers.children.TransferIn)
            menuInventory.children.push(Constants.Modules.Transfers.children.Manifest)
            menuInventory.children.push(Constants.Modules.Transfers.children.TransferOrder)
            menuInventory.children.push(Constants.Modules.Transfers.children.TransferOut)
            menuInventory.children.push(Constants.Modules.Transfers.children.TransferReceipt)
            menuInventory.children.push(Constants.Modules.Transfers.children.TransferStatus)
        }
        this.navigation.push(menuInventory);
        // Warehouse Menu
        let menuWarehouse = {
            caption: 'Warehouse',
            id: 'Warehouse',
            icon: 'store',
            children: [
                Constants.Modules.Warehouse.children.AssignBarCodes,
                Constants.Modules.Warehouse.children.CheckIn,
                Constants.Modules.Warehouse.children.Contract,
                Constants.Modules.Warehouse.children.Exchange,
                Constants.Modules.Warehouse.children.OrderStatus,
                Constants.Modules.Warehouse.children.PickList,
                Constants.Modules.Warehouse.children.ReceiveFromVendor,
                Constants.Modules.Warehouse.children.ReturnToVendor,
                Constants.Modules.Warehouse.children.StagingCheckout,
                Constants.Modules.Container.children.Container,
                Constants.Modules.Container.children.ContainerStatus,
                Constants.Modules.Container.children.EmptyContainer,
                Constants.Modules.Container.children.FillContainer,
                Constants.Modules.Container.children.RemoveFromContainer
            ]
        };
        this.navigation.push(menuWarehouse);
        let menuReports = {
            caption:    Constants.Modules.Administrator.children.Reports.caption,
            navigation: Constants.Modules.Administrator.children.Reports.nav,
            id:         Constants.Modules.Administrator.children.Reports.id,
            icon:       'assignment'
        }
        this.navigation.push(menuReports);
        // Containers Menu
        //const menuContainer = {
        //    caption: 'Container',
        //    id: 'Containers',
        //    icon: 'new_releases',
        //    children: [
        //        Constants.Modules.Container.children.Container,
        //        Constants.Modules.Container.children.ContainerStatus,
        //        Constants.Modules.Container.children.EmptyContainer,
        //        Constants.Modules.Container.children.FillContainer,
        //        Constants.Modules.Container.children.RemoveFromContainer
        //    ]
        //};
        //jh 07/31/2019 #790: I just discovered that in v2019, we removed the "Container" application option.  Module is available for all sites now.
        //if ((applicationOptions.container != undefined) && (applicationOptions.container.enabled != null) && (applicationOptions.container.enabled)) {
        //this.navigation.push(menuContainer);
        //}
        // Transfers Menu
        //const menuTransfers = {
        //    caption: 'Transfers',
        //    id: 'Transfers',
        //    icon: 'new_releases',
        //    children: [
        //        Constants.Modules.Transfers.children.TransferIn,
        //        Constants.Modules.Transfers.children.Manifest,
        //        Constants.Modules.Transfers.children.TransferOrder,
        //        Constants.Modules.Transfers.children.TransferOut,
        //        Constants.Modules.Transfers.children.TransferReceipt,
        //        Constants.Modules.Transfers.children.TransferStatus,
        //    ]
        //};
        //if ((applicationOptions.multiwarehouse != undefined) && (applicationOptions.multiwarehouse.enabled != null) && (applicationOptions.multiwarehouse.enabled) && (applicationOptions.multiwarehouse.value !== 1)) {
        //if (JSON.parse(sessionStorage.getItem('controldefaults')).multiwarehouse) {
        //    this.navigation.push(menuTransfers);
        //}

        // Utilities Menu
        let menuUtilities = {
            caption: 'Utilities',
            id: 'Utilities',
            icon: 'build',
            children: [
                Constants.Modules.Utilities.children.ChangeICodeUtility,
                Constants.Modules.Utilities.children.ChangeOrderStatus,
                Constants.Modules.Utilities.children.CurrencyProvisioningUtility,
                Constants.Modules.Utilities.children.Dashboard,
                Constants.Modules.Utilities.children.InventoryPurchaseUtility,
                Constants.Modules.Utilities.children.InventoryRetireUtility,
                Constants.Modules.Utilities.children.InventorySequenceUtility,
                Constants.Modules.Utilities.children.MigrateOrders,
                Constants.Modules.Utilities.children.InvoiceProcessBatch,
                Constants.Modules.Utilities.children.ReceiptProcessBatch,
                Constants.Modules.Utilities.children.VendorInvoiceProcessBatch,
                Constants.Modules.Utilities.children.QuikActivityCalendar,
                Constants.Modules.Utilities.children.RateUpdateUtility,
                Constants.Modules.Utilities.children.RefreshGLHistory,
            ]
        };
        this.navigation.push(menuUtilities);

        let menuSettings = {
            caption:    Constants.Modules.Administrator.children.Settings.caption,
            navigation: Constants.Modules.Administrator.children.Settings.nav,
            id:         Constants.Modules.Administrator.children.Settings.id,
            icon:       'settings'
        }
        this.navigation.push(menuSettings);

        // Administrator Menu
        let menuAdministrator = {
            caption: 'Administrator',
            id: 'Administrator',
            icon: 'security',
            children: [
                Constants.Modules.Administrator.children.Alert,
                Constants.Modules.Administrator.children.CustomField,
                Constants.Modules.Administrator.children.CustomForm,
                Constants.Modules.Administrator.children.CustomReportLayout,
                Constants.Modules.Administrator.children.DataHealth,
                Constants.Modules.Administrator.children.DuplicateRule,
                Constants.Modules.Administrator.children.EmailHistory,
                Constants.Modules.Administrator.children.EmailTemplate,
                Constants.Modules.Administrator.children.Group,
                Constants.Modules.Administrator.children.Plugin,
                //Constants.Modules.Administrator.children.Reports,
                //Constants.Modules.Administrator.children.Settings,
                Constants.Modules.Administrator.children.SystemUpdate,
                Constants.Modules.Administrator.children.User
            ]
        };
        this.navigation.push(menuAdministrator);

        // Settings
        this.settings = Constants.Modules.Settings.children;
        //this.settings.push(
        //    Constatnts
        //    {
        //        caption: 'Accounting',
        //        id: 'JF6Fj2eEJY6c',
        //        children: [
        //            Constants.Modules.Settings.children.Account.children.AccountingSettings,
        //            Constants.Modules.Settings.children.Account.children.ChartOfAccounts,
        //            Constants.Modules.Settings.children.Account.children.GlDistribution,
        //        ]
        //    },
        //    {
        //        caption: 'Address',
        //        id: 'PCkcrN7fWLfL',
        //        children: [
        //            Constants.Modules.Settings.children.Address.children.Country,
        //            Constants.Modules.Settings.children.Address.children.StateProvince
        //        ]
        //    },
        //    {
        //        caption: 'Billing',
        //        id: 'sGPbm7rvSBqt',
        //        children: [
        //            Constants.Modules.Settings.children.Billing.children.BillingCycle
        //        ]
        //    },
        //    {
        //        caption: 'Company Department',
        //        id: 'DjG7ktoV3nCE',
        //        children: [
        //            Constants.Modules.Settings.children.CompanyDepartment.children.CompanyDepartment
        //        ]
        //    },
        //    {
        //        caption: 'Contact',
        //        id: 'RbtoIAj5hUUH',
        //        children: [
        //            Constants.Modules.Settings.children.Contact.ContactEvent,
        //            Constants.Modules.Settings.children.Contact.ContactTitle,
        //            Constants.Modules.Settings.children.Contact.MailList
        //        ]
        //    },
        //    {
        //        caption: 'Currency',
        //        id: '0IlJUgChYxN8',
        //        children: [
        //            Constants.Modules.Settings.children.Currency.Currency
        //        ]
        //    },
        //    {
        //        caption: 'Customer',
        //        id: 'Sxz7v8QTDAIe',
        //        children: [
        //            Constants.Modules.Settings.children.Customer.children.CreditStatus,
        //            Constants.Modules.Settings.children.Customer.children.CustomerCategory,
        //            Constants.Modules.Settings.children.Customer.children.CustomerStatus,
        //            Constants.Modules.Settings.children.Customer.children.CustomerType
        //        ]
        //    },
        //    {
        //        caption: 'Deal',
        //        id: 'mBstcBfAhOef',
        //        children: [
        //            Constants.Modules.Settings.children.Deal.DealClassification,
        //            Constants.Modules.Settings.children.Deal.DealType,
        //            Constants.Modules.Settings.children.Deal.DealStatus,
        //            Constants.Modules.Settings.children.Deal.ProductionType,
        //            Constants.Modules.Settings.children.Deal.ScheduleType,
        //        ]
        //    },
        //    {
        //        caption: 'Discount Template',
        //        id: 'Jn1E43g161dR',
        //        children: [
        //            Constants.Modules.Settings.children.DiscountTemplate.children.DiscountTemplate
        //        ]
        //    },
        //    {
        //        caption: 'Document',
        //        id: 'UhSkDzYb2osR',
        //        children: [
        //            Constants.Modules.Settings.children.Document.children.DocumentType,
        //            Constants.Modules.Settings.children.Document.children.CoverLetter,
        //            Constants.Modules.Settings.children.Document.children.TermsAndConditions
        //        ]
        //    },
        //    {
        //        caption: 'Event',
        //        id: 'jmFqqwQPAdBQ',
        //        children: [
        //            Constants.Modules.Settings.children.Event.EventCategory,
        //            Constants.Modules.Settings.children.Event.EventType,
        //            Constants.Modules.Settings.children.Event.PersonnelType,
        //            Constants.Modules.Settings.children.Event.PhotographyType
        //        ]
        //    },
        //    {
        //        caption: 'Facilities',
        //        id: 'aVn1wUS1Gjrj',
        //        children: [
        //            Constants.Modules.Settings.children.Facilities.Building,
        //            Constants.Modules.Settings.children.Facilities.FacilityType,
        //            Constants.Modules.Settings.children.Facilities.FacilityRate,
        //            Constants.Modules.Settings.children.Facilities.FacilityScheduleStatus,
        //            Constants.Modules.Settings.children.Facilities.FacilityStatus,
        //            Constants.Modules.Settings.children.Facilities.FacilityCategory,
        //            Constants.Modules.Settings.children.Facilities.FacilitySpaceType
        //        ]
        //    }
        //);
        
    }
    //----------------------------------------------------------------------------------------------
    buildMainMenu(): (MenuCategory | MenuModule)[] {
        this.initMainMenu();
        var nodeApplication = FwApplicationTree.getMyTree();

        var applicationMenu: (MenuCategory | MenuModule)[] = [];
        for (var node of this.navigation) {
            var categorySecurityObject = FwFunc.getObjects(nodeApplication, 'id', node.id);
            if (node.id === '' || (categorySecurityObject !== undefined && categorySecurityObject.length > 0 && categorySecurityObject[0].properties.visible === 'T')) {
                if (node.children) {
                    var modules: MenuCategoryModule[] = [];
                    for (var child of node.children) {
                        var moduleSecurityObject = FwFunc.getObjects(nodeApplication, 'id', child.id);
                        if (child.id === '' || (moduleSecurityObject !== undefined && moduleSecurityObject.length > 0 && moduleSecurityObject[0].properties.visible === 'T')) {
                            var module: MenuCategoryModule = {
                                title:       child.caption,
                                navigation:  child.nav,
                                securityid:  child.id
                            };
                            modules.push(module);
                        }
                    }
                    if (modules.length > 0) {
                        var category: MenuCategory = {
                            title:   node.caption,
                            icon:    node.icon,
                            modules: modules
                        };
                        applicationMenu.push(category);
                    }
                } else {
                    var lv1module: MenuModule = {
                        title:      node.caption,
                        icon:       node.icon,
                        securityid: node.id,
                        navigation: node.navigation
                    }
                    applicationMenu.push(lv1module);
                }
            }
        }
        return applicationMenu;
    }
    //----------------------------------------------------------------------------------------------
    getUserControls(): MenuUserControls {
        var usertype = sessionStorage.getItem('userType');
        var username = sessionStorage.getItem('fullname')

        var userControls: MenuUserControls = {};

        userControls.bookmarks = this.getBookmarks();

        userControls.controls = [
            { control: (usertype === 'USER') ? this.buildOfficeLocation() : this.buildDealPicker() },
            { control: jQuery(`<div class="username" title="User Type: ${usertype}">${username}</div>`) }
        ]

        userControls.links = [
            { title: 'User Profile', navigation: 'module/userprofile' },
            { title: 'Sign Out',     navigation: 'logoff' }
        ];

        return userControls;
    }
    //----------------------------------------------------------------------------------------------
    buildOfficeLocation(): JQuery {
        const userlocation = JSON.parse(sessionStorage.getItem('location'));
        const userwarehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        const userdepartment = JSON.parse(sessionStorage.getItem('department'));

        let locationCaption;
        if (userlocation.location === userwarehouse.warehouse) {
            locationCaption = userlocation.location
        } else {
            locationCaption = `${userlocation.location} / ${userwarehouse.warehouse}`;
        }

        const $officelocation = jQuery(`<div class="officelocation">
                                        <div class="locationcolor" style="background-color:${userlocation.locationcolor}"></div>
                                        <div class="value">${locationCaption}</div>
                                      </div>`);

        // navigation header location icon
        $officelocation.on('click', function () {
            try {
                const $confirmation = FwConfirmation.renderConfirmation('Select an Office Location', '');
                const $select = FwConfirmation.addButton($confirmation, 'Select', false);
                FwConfirmation.addButton($confirmation, 'Cancel', true);

                FwConfirmation.addControls($confirmation, `<div class="fwform" data-controller="UserController" style="background-color: transparent;">
                                                             <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                                                               <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="OfficeLocationId" data-validationname="OfficeLocationValidation" data-required="true"></div>
                                                             </div>
                                                             <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                                                               <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-formbeforevalidate="beforeValidateWarehouse" data-datafield="WarehouseId" data-validationname="WarehouseValidation" data-boundfields="OfficeLocationId" data-required="true"></div>
                                                             </div>
                                                             <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                                                               <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-validationname="DepartmentValidation" data-boundfields="WarehouseId" data-required="true"></div>
                                                             </div>
                                                           </div>`);

                FwFormField.setValueByDataField($confirmation, 'OfficeLocationId', userlocation.locationid, userlocation.location);
                FwFormField.setValueByDataField($confirmation, 'WarehouseId', userwarehouse.warehouseid, userwarehouse.warehouse);
                FwFormField.setValueByDataField($confirmation, 'DepartmentId', userdepartment.departmentid, userdepartment.department);

                $confirmation.find('[data-datafield="OfficeLocationId"] input').select();

                $confirmation.find('[data-datafield="OfficeLocationId"]').data('onchange', e => {
                    FwFormField.setValue($confirmation, 'div[data-datafield="WarehouseId"]', '', '');
                });
                const topLayer = '<div class="top-layer" data-controller="none" style="background-color: transparent;z-index:1"></div>';
                const $realConfirm = jQuery($confirmation.find('.fwconfirmationbox')).prepend(topLayer);
                // select button within location confirmation prompt
                $select.on('click', async () => {
                    try {
                        let valid = FwModule.validateForm($confirmation);
                        if (valid) {
                            const locationid = FwFormField.getValueByDataField($confirmation, 'OfficeLocationId');
                            const warehouseid = FwFormField.getValueByDataField($confirmation, 'WarehouseId');
                            const departmentid = FwFormField.getValueByDataField($confirmation, 'DepartmentId');

                            // Ajax: Get Office Location Info
                            const promiseGetOfficeLocationInfo = await FwAjax.callWebApi<any, any>({
                                httpMethod: 'GET',
                                url: `${applicationConfig.apiurl}api/v1/account/officelocation?locationid=${locationid}&warehouseid=${warehouseid}&departmentid=${departmentid}`,
                                $elementToBlock: $realConfirm
                            });
                            const promiseGetDepartment = FwAjax.callWebApi<any, any>({
                                httpMethod: 'GET',
                                url: `${applicationConfig.apiurl}api/v1/department/${departmentid}`,
                                $elementToBlock: $realConfirm
                            });

                            const promiseGetSystemNumbers = FwAjax.callWebApi<BrowseRequest, any>({
                                httpMethod: 'POST',
                                url: `${applicationConfig.apiurl}api/v1/systemnumber/browse`,
                                $elementToBlock: $realConfirm,
                                data: {
                                    uniqueids: {
                                        LocationId: locationid
                                    }
                                }
                            });


                            await Promise.all([
                                promiseGetOfficeLocationInfo,     // 00
                                promiseGetDepartment,             // 01
                                promiseGetSystemNumbers,          // 02
                            ])
                                .then((values: any) => {
                                    const responseGetOfficeLocationInfo = values[0];
                                    const responseGetDepartment = values[1];
                                    const responseGetSystemNumbers = values[2];

                                    sessionStorage.setItem('location', JSON.stringify(responseGetOfficeLocationInfo.location));
                                    sessionStorage.setItem('warehouse', JSON.stringify(responseGetOfficeLocationInfo.warehouse));

                                    // Include department's default activity selection in sessionStorage for use in Quote / Order
                                    const department = responseGetOfficeLocationInfo.department;
                                    const defaultActivities: Array<string> = [];
                                    for (let key in responseGetDepartment) {
                                        if (key.startsWith('DefaultActivity') && responseGetDepartment[key] === true) {
                                            defaultActivities.push(key.slice(15));
                                        }
                                    }
                                    department.activities = defaultActivities;
                                    sessionStorage.setItem('department', JSON.stringify(department));

                                    const systemNumberModuleIndex = responseGetSystemNumbers.ColumnIndex.Module;
                                    const systemNumberIsAssignedByUserIndex = responseGetSystemNumbers.ColumnIndex.IsAssignByUser;
                                    let userassignedcustnum: boolean = false;
                                    let userassigneddealnum: boolean = false;
                                    for (let i = 0; i < responseGetSystemNumbers.Rows.length; i++) {
                                        const moduleSystemNumber = responseGetSystemNumbers.Rows[i];
                                        const module = moduleSystemNumber[systemNumberModuleIndex];
                                        const isAssignedByUser = moduleSystemNumber[systemNumberIsAssignedByUserIndex];
                                        if (module === 'CUSTOMER') {
                                            userassignedcustnum = isAssignedByUser;
                                        }
                                        else if (module === 'DEAL') {
                                            userassigneddealnum = isAssignedByUser;
                                        }
                                    }
                                    let controlDefaults = JSON.parse(sessionStorage.getItem('controldefaults'));
                                    controlDefaults.userassignedcustomernumber = userassignedcustnum;
                                    controlDefaults.userassigneddealnumber = userassigneddealnum;
                                    sessionStorage.setItem('controldefaults', JSON.stringify(controlDefaults));

                                    FwConfirmation.destroyConfirmation($confirmation);
                                    window.location.reload(false);
                                })
                        }
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        return $officelocation;
    }
    //----------------------------------------------------------------------------------------------
    buildDealPicker(): JQuery {
        var deal = JSON.parse(sessionStorage.getItem('deal'));
        var $btnDealPicker = jQuery(`<div class="dealpicker" style="display:flex; align-items:center;">
                                       <div class="caption" style="font-size:.8em;color:#b71c1c;flex:0 0 auto; margin:0 .4em 0 0;">Job:</div> 
                                       <div class="value" style="flex:1 1 0;">${deal.deal}<i class="material-icons" style="font-size:inherit;">&#xE5CF</i></div>
                                     </div>`)
            .css('cursor', 'pointer')
            .attr('title', 'Switch Jobs');

        $btnDealPicker.hover((e: JQuery.Event) => {
            $btnDealPicker.css('background-color', '#eeeeee');
        }, (e: JQuery.Event) => {
            $btnDealPicker.css('background-color', 'inherit');
        });

        $btnDealPicker.on('click', function () {
            try {
                var $confirmation = FwConfirmation.renderConfirmation('Switch Deals...', '');
                const $select = FwConfirmation.addButton($confirmation, 'Select', false);
                var $cancel = FwConfirmation.addButton($confirmation, 'Cancel', true);

                FwConfirmation.addControls($confirmation, `<div class="fwform" data-controller="UserController" style="background-color: transparent;">
                                                             <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                                                               <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-validationname="ContactDealValidation" data-required="true"></div>
                                                             </div>
                                                           </div>`);

                const deal = JSON.parse(sessionStorage.getItem('deal'));
                FwFormField.setValueByDataField($confirmation, 'DealId', deal.dealid, deal.deal);

                $select.on('click', async () => {
                    try {
                        let valid = FwModule.validateForm($confirmation);
                        if (valid) {
                            const deal = {
                                dealid: FwFormField.getValueByDataField($confirmation, 'DealId'),
                                deal: FwFormField.getTextByDataField($confirmation, 'DealId')
                            };
                            sessionStorage.setItem('deal', JSON.stringify(deal));

                            FwConfirmation.destroyConfirmation($confirmation);
                            $btnDealPicker.find('.value').text(deal.deal);
                            window.location.reload(false);
                        }
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        return $btnDealPicker;
    }
    //----------------------------------------------------------------------------------------------
    getBookmarks(): UserControlBookmark[] {
        var bookmarks: UserControlBookmark[] = [];
        FwApplicationTree.getMyTree();

        if (sessionStorage.getItem('userType') === 'USER') {
            if (sessionStorage['favorites']) {
                const favoritesModules = JSON.parse(sessionStorage.getItem('favorites'));
                for (let i = 0; i < favoritesModules.length; i++) {
                    const $this = favoritesModules[i];
                    bookmarks.push({ title: $this.text, navigation: $this.value, type: 'userdefined' });
                }
            }

            bookmarks.push({ title: 'Dashboard', icon: 'dashboard', navigation: 'module/dashboard', type: 'system' });

            //const nodeSettings = FwApplicationTree.getNodeById(FwApplicationTree.tree, 'Settings');
            //if (typeof nodeSettings === 'object' && nodeSettings.properties.visible === 'T') {
            //    bookmarks.push({ title: 'Settings', icon: 'settings', navigation: 'module/settings', type: 'system' });
            //}

            //const nodeReports = FwApplicationTree.getNodeById(FwApplicationTree.tree, 'Reports');
            //if (typeof nodeReports === 'object' && nodeReports.properties.visible === 'T') {
            //    bookmarks.push({ title: 'Reports', icon: 'assignment', navigation: 'module/reports', type: 'system' });
            //}
        }

        return (bookmarks.length > 0) ? bookmarks : null;
    }
    //----------------------------------------------------------------------------------------------
    events($appmaster: JQuery) {
        // color nav header for non-default user location on app refresh. Event listener in RwMaster.buildOfficeLocation()
        if (sessionStorage.getItem('location') !== null && sessionStorage.getItem('defaultlocation') !== null) {
            const userLocation = JSON.parse(sessionStorage.getItem('location'));
            const defaultLocation = JSON.parse(sessionStorage.getItem('defaultlocation'));
            if (userLocation.location !== defaultLocation.location) {
                $appmaster.data('header').find('.header-wrapper').css('border-color', `${userLocation.locationcolor}`); 
            }
        }

        const userid = JSON.parse(sessionStorage.getItem('userid'));
        if (userid) {
            if (userid.mainmenupinned === false) {
                $appmaster.data('menu').removeAttr('pinned');
            }
            $appmaster.data('menu').find('.pin-unpin').off('click');
            $appmaster.data('menu').find('.pin-unpin').on('click', (e) => {
                let request: any = {};
                if ($appmaster.data('menu')[0].hasAttribute('pinned')) {
                    $appmaster.data('menu').removeAttr('pinned');
                    userid.mainmenupinned = false;
                    request.MainMenuPinned = false;
                } else {
                    $appmaster.data('menu').attr('pinned', '');
                    userid.mainmenupinned = true;
                    request.MainMenuPinned = true;
                }
                sessionStorage.setItem('userid', JSON.stringify(userid));

                const webusersid = sessionStorage.getItem('webusersid');
                if (webusersid) {
                    request.WebUserId = webusersid;
                    FwAppData.apiMethod(true, 'PUT', `api/v1/userprofile/${webusersid}`, request, FwServices.defaultTimeout, response => { }, ex => {
                        if (ex !== 'Forbidden') {
                            FwFunc.showError(ex)
                        }
                    }, null);
                }
            });
        }
    }
    //----------------------------------------------------------------------------------------------
}
var masterController: RwMaster = new RwMaster();
