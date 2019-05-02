class RwMaster extends WebMaster {
    //----------------------------------------------------------------------------------------------
    navigation: any;
    //----------------------------------------------------------------------------------------------
    initMainMenu() {
        this.navigation = [
            {
                caption: 'TrakitWorks',
                id: 'B05953D7-DC85-486C-B9A4-7743875DFABC',
                children: [ContactController, CustomerController, DealController, OrderController, PurchaseOrderController, QuoteController, VendorController]
            },
            {
                caption: 'Inventory',
                id: 'CA7EDF90-F08A-4E5C-BA6B-87DB6A14D485',
                children: [AssetController, RepairController, InventoryItemController, ExpendableItemController/*, SalesInventoryController, PartsInventoryController, ContainerController*/]
          },
            {
                caption: 'Warehouse',
                id: '293A157D-EA8E-48F6-AE97-15F9DE53041A',
                children: [AssignBarCodesController, CheckInController, ContractController, ExchangeController, OrderStatusController, PickListController, ReceiveFromVendorController, ReturnToVendorController, StagingCheckoutController/*, InvoiceController*/]
            },
            {
                caption: 'Administrator',
                id: 'A3EE3EE9-4C98-4315-B08D-2FAD67C04E07',
                children: [ControlController, CustomFieldController, CustomFormController, DuplicateRuleController, GroupController, HotfixController, SettingsController, ReportsController, UserController]
            }
        ];
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
        var $usercontrol = FwFileMenu.UserControl_render($context);

        this.buildSystemBar($context);
        this.buildOfficeLocation($context);

        // Add SystemBarControl: User Name
        var usertype = sessionStorage.getItem('userType');
        var username = sessionStorage.getItem('fullname')
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
                program.navigate('logoff');
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    buildOfficeLocation($usercontrol: JQuery<HTMLElement>) {
        var userlocation = JSON.parse(sessionStorage.getItem('location'));
        var userid       = JSON.parse(sessionStorage.getItem('userid'));

        var $officelocation = jQuery(`<div class="officelocation">
                                        <div class="locationcolor" style="background-color:${userlocation.locationcolor}"></div>
                                        <div class="value">${userlocation.location}</div>
                                      </div>`);

        FwFileMenu.UserControl_addSystemBarControl('officelocation', $officelocation, $usercontrol);

        $officelocation.on('click', function () {
            try {
                var userlocation   = JSON.parse(sessionStorage.getItem('location'));
                var userwarehouse  = JSON.parse(sessionStorage.getItem('warehouse'));
                var userdepartment = JSON.parse(sessionStorage.getItem('department'));
                var $confirmation  = FwConfirmation.renderConfirmation('Select an Office Location', '');
                var $select        = FwConfirmation.addButton($confirmation, 'Select', false);
                var $cancel        = FwConfirmation.addButton($confirmation, 'Cancel', true);

                FwConfirmation.addControls($confirmation, `<div class="fwform" data-controller="UserController" style="background-color: transparent;">
                                                             <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                                                               <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="OfficeLocationId" data-validationname="OfficeLocationValidation"></div>
                                                             </div>
                                                             <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                                                               <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-formbeforevalidate="beforeValidateWarehouse" data-datafield="WarehouseId" data-validationname="WarehouseValidation" data-boundfields="OfficeLocationId"></div>
                                                             </div>
                                                             <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                                                               <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="Department" data-validationname="DepartmentValidation" data-boundfields="WarehouseId"></div>
                                                             </div>
                                                           </div>`);

                FwFormField.setValue($confirmation, 'div[data-datafield="OfficeLocationId"]', userlocation.locationid, userlocation.location);
                FwFormField.setValue($confirmation, 'div[data-datafield="WarehouseId"]', userwarehouse.warehouseid, userwarehouse.warehouse);
                FwFormField.setValue($confirmation, 'div[data-datafield="Department"]', userdepartment.departmentid, userdepartment.department);

                $confirmation.find('[data-datafield="OfficeLocationId"]').data('onchange', e => {
                    FwFormField.setValue($confirmation, 'div[data-datafield="WarehouseId"]', '', '');
                });

                $select.on('click', function () {
                    try {
                        var valid      = true;
                        var location   = FwFormField.getValue($confirmation, 'div[data-datafield="OfficeLocationId"]');
                        var warehouse  = FwFormField.getValue($confirmation, 'div[data-datafield="WarehouseId"]');
                        var department = FwFormField.getValue($confirmation, 'div[data-datafield="Department"]');
                        if (location === '') {
                            $confirmation.find('div[data-datafield="OfficeLocationId"]').addClass('error');
                            valid = false;
                        }
                        if (warehouse === '') {
                            $confirmation.find('div[data-datafield="WarehouseId"]').addClass('error');
                            valid = false;
                        }
                        if (department === '') {
                            $confirmation.find('div[data-datafield="Department"]').addClass('error');
                            valid = false;
                        }
                        if (valid) {
                            var request = {
                                location:   location,
                                warehouse:  warehouse,
                                department: department,
                                userid:     userid.webusersid
                            };
                            RwServices.session.updatelocation(request, function (response) {
                                sessionStorage.setItem('authToken', response.authToken);
                                sessionStorage.setItem('location', JSON.stringify(response.location));
                                sessionStorage.setItem('warehouse', JSON.stringify(response.warehouse));
                                sessionStorage.setItem('department', JSON.stringify(response.department));
                                sessionStorage.setItem('userid', JSON.stringify(response.webusersid));
                                FwConfirmation.destroyConfirmation($confirmation);

                                program.navigate('home');
                                $usercontrol.find('.officelocation .locationcolor').css('background-color', response.location.locationcolor);
                                $usercontrol.find('.officelocation .value').text(response.location.location);
                            });
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
        const $dashboard = jQuery('<i class="material-icons dashboard" title="Dashboard">insert_chart</i>');
        $dashboard.on('click', function () {
            try {
                program.getModule('module/dashboard');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwFileMenu.UserControl_addSystemBarControl('dashboard', $dashboard, $usercontrol)

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
    //----------------------------------------------------------------------------------------------
}
var masterController: RwMaster = new RwMaster();
