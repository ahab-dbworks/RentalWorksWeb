class RwMaster extends WebMaster {
    //----------------------------------------------------------------------------------------------
    navigation: any;
    //----------------------------------------------------------------------------------------------
    initMainMenu() {
        this.navigation = [
            {
                caption: 'TrakitWorks',
                id: 'B05953D7-DC85-486C-B9A4-7743875DFABC',
                children: [ContactController, /*CustomerController,*/ DealController, OrderController, PurchaseOrderController, QuoteController, VendorController]
            },
            {
                caption: 'Inventory',
                id: 'CA7EDF90-F08A-4E5C-BA6B-87DB6A14D485',
                children: [AssetController, /*ExpendableItemController,*/ InventoryItemController, RepairController/*, SalesInventoryController, PartsInventoryController, ContainerController*/]
          },
            {
                caption: 'Warehouse',
                id: '293A157D-EA8E-48F6-AE97-15F9DE53041A',
                children: [AssignBarCodesController, CheckInController, ContractController, ExchangeController, OrderStatusController, PickListController, ReceiveFromVendorController, ReturnToVendorController, StagingCheckoutController/*, InvoiceController*/]
            },
            {
                caption: 'Administrator',
                id: 'A3EE3EE9-4C98-4315-B08D-2FAD67C04E07',
                children: [ControlController, CustomFieldController, CustomFormController, DuplicateRuleController, GroupController, HotfixController, ReportsController, SettingsController, UserController]
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
