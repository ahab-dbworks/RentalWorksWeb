class RwMaster extends WebMaster {
    //----------------------------------------------------------------------------------------------
    navigation: any;
    //----------------------------------------------------------------------------------------------
    initMainMenu() {
        let userType = sessionStorage.getItem('userType');
        this.navigation = [];
        
        // Agent Menu
        let menuAgent = {
            caption: 'Agent',
            id: '91D2F0CF-2063-4EC8-B38D-454297E136A8',
            children: []
        };
        if (userType == 'USER' || userType == 'CONTACT') {
            menuAgent.children.push(Constants.Modules.Home.Quote);
        }
        if (userType == 'USER') {
            menuAgent.children.push(Constants.Modules.Home.Order);
            menuAgent.children.push(Constants.Modules.Home.Customer);
            menuAgent.children.push(Constants.Modules.Home.Deal);
            menuAgent.children.push(Constants.Modules.Home.Vendor);
            menuAgent.children.push(Constants.Modules.Home.Contact);
            menuAgent.children.push(Constants.Modules.Home.PurchaseOrder);
            menuAgent.children.push(Constants.Modules.Home.Project);
        }
        this.navigation.push(menuAgent);
        
        if (userType == 'USER') {
            // Inventory Menu
            let menuInventory = {
                caption: 'Inventory',
                id: '8AA0C4A4-B583-44CD-BB47-09C43961CE99',
                children: [
                    Constants.Modules.Home.RentalInventory,
                    Constants.Modules.Home.SalesInventory,
                    Constants.Modules.Home.PartsInventory,
                    Constants.Modules.Home.Asset,
                    Constants.Modules.Home.Container,
                    Constants.Modules.Home.Repair,
                    Constants.Modules.Home.CompleteQc,
                    Constants.Modules.Home.PhysicalInventory
                ]
            };
            this.navigation.push(menuInventory);

            // Warehouse Menu
            let menuWarehouse = {
                caption: 'Warehouse',
                id: '22D67715-9C24-4A06-A009-CB10A1EC746B',
                children: [
                    Constants.Modules.Home.OrderStatus,
                    Constants.Modules.Home.PickList,
                    Constants.Modules.Home.Contract,
                    Constants.Modules.Home.StagingCheckout,
                    Constants.Modules.Home.Exchange,
                    Constants.Modules.Home.CheckIn,
                    Constants.Modules.Home.ReceiveFromVendor,
                    Constants.Modules.Home.ReturnToVendor,
                    Constants.Modules.Home.AssignBarCodes,
                    Constants.Modules.Home.TransferStatus,
                    Constants.Modules.Home.TransferOrder,
                    Constants.Modules.Home.Manifest,
                    Constants.Modules.Home.TransferReceipt,
                    Constants.Modules.Home.TransferOut,
                    Constants.Modules.Home.TransferIn,
                    Constants.Modules.Home.ContainerStatus,
                    Constants.Modules.Home.FillContainer,
                    Constants.Modules.Home.EmptyContainer,
                    Constants.Modules.Home.RemoveFromContainer
                ]
            };
            this.navigation.push(menuWarehouse);
        
            // Billing Menu
            let menuBilling = {
                caption: 'Billing',
                id: '9BC99BDA-4C94-4D7D-8C22-31CA5205B1AA',
                children: [
                    Constants.Modules.Home.Billing,
                    Constants.Modules.Home.Invoice,
                    Constants.Modules.Home.Receipt,
                    Constants.Modules.Home.VendorInvoice
                ]
            };
            this.navigation.push(menuBilling);

            // Utilities Menu
            let menuUtilities = {
                caption: 'Utilities',
                id: '81609B0E-4B1F-4C13-8BE0-C1948557B82D',
                children: [
                    Constants.Modules.Utilities.Dashboard,
                    Constants.Modules.Utilities.DashboardSettings,
                    Constants.Modules.Utilities.InvoiceProcessBatch,
                    Constants.Modules.Utilities.ReceiptProcessBatch,
                    Constants.Modules.Utilities.VendorInvoiceProcessBatch,
                    Constants.Modules.Utilities.QuikActivityCalendar
                ]
            };
            this.navigation.push(menuUtilities);

            // Administrator Menu
            let menuAdministrator = {
                caption: 'Administrator',
                id: 'F188CB01-F627-4DD3-9B91-B6486F0977DC',
                children: [
                    Constants.Modules.Administrator.Control,
                    Constants.Modules.Administrator.CustomField,
                    Constants.Modules.Administrator.CustomForm,
                    Constants.Modules.Administrator.DuplicateRule,
                    Constants.Modules.Administrator.EmailHistory,
                    Constants.Modules.Administrator.Group,
                    Constants.Modules.Administrator.Hotfix,
                    Constants.Modules.Administrator.User,
                    Constants.Modules.Administrator.Settings,
                    Constants.Modules.Administrator.Reports
                ]
            };
            this.navigation.push(menuAdministrator);
        }
    }
    //----------------------------------------------------------------------------------------------
    buildMainMenu($view: JQuery) {
        this.initMainMenu();
        var nodeApplication;
        var nodeSystem = FwApplicationTree.getMyTree();
        for (var appno = 0; appno < nodeSystem.children.length; appno++) {
            if (nodeSystem.children[appno].id === FwApplicationTree.currentApplicationId) {
                nodeApplication = nodeSystem.children[appno];
            }
        }
        if (nodeApplication === null) {
            sessionStorage.clear();
            window.location.reload(true);
        }

        for (var i = 0; i < this.navigation.length; i++) {
            var categorySecurityObject = FwFunc.getObjects(nodeApplication, 'id', this.navigation[i].id);
            if (categorySecurityObject !== undefined && categorySecurityObject.length > 0 && categorySecurityObject[0].properties.visible === 'T') {
                var $menu = FwFileMenu.addMenu($view, this.navigation[i].caption);
                for (var j = 0; j < this.navigation[i].children.length; j++) {
                    var moduleSecurityObject = FwFunc.getObjects(nodeApplication, 'id', this.navigation[i].children[j].id);
                    if (moduleSecurityObject !== undefined && moduleSecurityObject.length > 0 && moduleSecurityObject[0].properties.visible === 'T') {
                        FwFileMenu.generateStandardModuleBtn($menu, this.navigation[i].children[j].id, this.navigation[i].children[j].caption, this.navigation[i].children[j].nav, '');
                    }
                }
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    getUserControl($context: JQuery) {
        var usertype = sessionStorage.getItem('userType');
        var username = sessionStorage.getItem('fullname')

        const $usercontrol = FwFileMenu.UserControl_render($context);

        this.buildSystemBar($context);
        if (usertype === 'USER') {
            this.buildOfficeLocation($context);
        }
        else if (usertype === 'CONTACT') {
            this.buildDealPicker($context);
        }

        // Add SystemBarControl: User Name
        var $controlUserName = jQuery(`<div title="User Type: ${usertype}">${username}</div>`);
        FwFileMenu.UserControl_addSystemBarControl('username', $controlUserName, $usercontrol);
        // Add DropDownMenuItem: User Settings
        var $miUserSettings = jQuery(`<div>${RwLanguages.translate('User Settings')}</div>`);
        FwFileMenu.UserControl_addDropDownMenuItem('usersettings', $miUserSettings, $usercontrol);
        $miUserSettings.on('click', (event) => {
            try {
                program.getModule('module/usersettings');
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        // Add DropDownMenuItem: Sign Out
        var $miSignOut = jQuery(`<div>${RwLanguages.translate('Sign Out')}</div>`);
        FwFileMenu.UserControl_addDropDownMenuItem('signout', $miSignOut, $usercontrol);
        $miSignOut.on('click', (event) => {
            try {
                program.getModule('logoff');
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    buildOfficeLocation($usercontrol: JQuery<HTMLElement>) {
        const userlocation = JSON.parse(sessionStorage.getItem('location'));
        const userid = JSON.parse(sessionStorage.getItem('userid'));
        const defaultLocation = JSON.parse(sessionStorage.getItem('defaultlocation'));
        const $officelocation = jQuery(`<div class="officelocation">
                                        <div class="locationcolor" style="background-color:${userlocation.locationcolor}"></div>
                                        <div class="value">${userlocation.location}</div>
                                      </div>`);

        FwFileMenu.UserControl_addSystemBarControl('officelocation', $officelocation, $usercontrol);


        // navigation header location icon
        $officelocation.on('click', function () {
            try {
                var userlocation = JSON.parse(sessionStorage.getItem('location'));
                var userwarehouse = JSON.parse(sessionStorage.getItem('warehouse'));
                var userdepartment = JSON.parse(sessionStorage.getItem('department'));
                var $confirmation = FwConfirmation.renderConfirmation('Select an Office Location', '');
                const $select = FwConfirmation.addButton($confirmation, 'Select', false);
                var $cancel = FwConfirmation.addButton($confirmation, 'Cancel', true);

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

                $confirmation.find('[data-datafield="OfficeLocationId"]').data('onchange', e => {
                    FwFormField.setValue($confirmation, 'div[data-datafield="WarehouseId"]', '', '');
                });
                // select button within location confirmation prompt
                $select.on('click', async () => {
                    try {
                        let valid = FwModule.validateForm($confirmation);
                        if (valid) {
                            const locationid = FwFormField.getValueByDataField($confirmation, 'OfficeLocationId');
                            const warehouseid = FwFormField.getValueByDataField($confirmation, 'WarehouseId');
                            const departmentid = FwFormField.getValueByDataField($confirmation, 'DepartmentId');

                            // Ajax: Get Office Location Info
                            const responseGetOfficeLocationInfo = await FwAjax.callWebApi<any>({
                                httpMethod: 'GET',
                                url: `${applicationConfig.apiurl}api/v1/account/officelocation?locationid=${locationid}&warehouseid=${warehouseid}&departmentid=${departmentid}`,
                                $elementToBlock: $confirmation
                            });

                            sessionStorage.setItem('location', JSON.stringify(responseGetOfficeLocationInfo.location));
                            sessionStorage.setItem('warehouse', JSON.stringify(responseGetOfficeLocationInfo.warehouse));
                            sessionStorage.setItem('department', JSON.stringify(responseGetOfficeLocationInfo.department));
                            FwConfirmation.destroyConfirmation($confirmation);
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
    }
    //----------------------------------------------------------------------------------------------
    buildDealPicker($usercontrol: JQuery<HTMLElement>) {
        var deal = JSON.parse(sessionStorage.getItem('deal'));
        var $btnDealPicker = jQuery(`<div class="dealpicker" style="display:flex; align-items:center;">
                                       <div class="caption" style="font-size:.8em;color:#b71c1c;flex:0 0 auto; margin:0 .4em 0 0;">Job:</div> 
                                       <div class="value" style="flex:1 1 0;">${deal.deal}<i class="material-icons" style="font-size:inherit;">&#xE5CF</i></div>
                                     </div>`)
                .css('cursor','pointer')
                .attr('title', 'Switch Jobs');


        FwFileMenu.UserControl_addSystemBarControl('dealpicker', $btnDealPicker, $usercontrol);

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
    }
    //----------------------------------------------------------------------------------------------
    buildSystemBar($usercontrol: JQuery<HTMLElement>) {
        if (sessionStorage.getItem('userType') === 'USER') {
            if (sessionStorage['toolbar']) {
                const toolbarModules = JSON.parse(sessionStorage.getItem('toolbar'));
                for (let i = 0; i < toolbarModules.length; i++) {
                    const $this = toolbarModules[i];
                    const $module = jQuery(`<div class="toolbar-module dashboard" style="display:flex;"><i class="material-icons" title="${$this.text}">star</i><span>${$this.text}</span></div>`);
                    $module.on('click', function () {
                        try {
                            program.getModule($this.value);
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    });
                    FwFileMenu.UserControl_addSystemBarControl($this.text, $module, $usercontrol);
                }
            }
            const $dashboard = jQuery('<i class="material-icons dashboard" title="Dashboard">insert_chart</i>');
            $dashboard.on('click', function () {
                try {
                    program.getModule('module/dashboard');
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
            FwFileMenu.UserControl_addSystemBarControl('dashboard', $dashboard, $usercontrol);

            const $settings = jQuery('<i class="material-icons dashboard" title="Settings">settings</i>');
            $settings.on('click', function () {
                try {
                    program.getModule('module/settings');
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
            FwFileMenu.UserControl_addSystemBarControl('dashboard', $settings, $usercontrol)

            const $reports = jQuery('<i class="material-icons dashboard" title="Reports">assignment</i>');
            $reports.on('click', function () {
                try {
                    program.getModule('module/reports');
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });
            FwFileMenu.UserControl_addSystemBarControl('dashboard', $reports, $usercontrol)
        }
    }
    //----------------------------------------------------------------------------------------------
}
var masterController: RwMaster = new RwMaster();
