class RwMaster extends WebMaster {
    initMainMenu() {
        this.navigation = [
            {
                caption: 'Agent',
                id: '91D2F0CF-2063-4EC8-B38D-454297E136A8',
                children: [QuoteController, OrderController, CustomerController, DealController, VendorController, ContactController, PurchaseOrderController, ProjectController]
            },
            {
                caption: 'Inventory',
                id: '8AA0C4A4-B583-44CD-BB47-09C43961CE99',
                children: [RentalInventoryController, SalesInventoryController, PartsInventoryController, PhysicalInventoryController, AssetController, ContainerController, RepairController, CompleteQcController]
            },
            {
                caption: 'Warehouse',
                id: '22D67715-9C24-4A06-A009-CB10A1EC746B',
                children: [OrderStatusController, PickListController, ContractController, StagingCheckoutController, ExchangeController, CheckInController, ReceiveFromVendorController,
                    ReturnToVendorController, AssignBarCodesController, TransferStatusController, TransferOrderController, ManifestController, TransferReceiptController, TransferOutController, TransferInController,
                    FillContainerController, EmptyContainerController, RemoveFromContainerController]
            },
            {
                caption: 'Billing',
                id: '9BC99BDA-4C94-4D7D-8C22-31CA5205B1AA',
                children: [BillingController, InvoiceController, ReceiptController, VendorInvoiceController]
            },
            {
                caption: 'Utilities',
                id: '81609B0E-4B1F-4C13-8BE0-C1948557B82D',
                children: [DashboardController, DashboardSettingsController, InvoiceProcessBatchController, ReceiptProcessBatchController, VendorInvoiceProcessBatchController, QuikActivityCalendarController]
            },
            {
                caption: 'Administrator',
                id: 'F188CB01-F627-4DD3-9B91-B6486F0977DC',
                children: [ControlController, CustomFieldController, CustomFormController, DuplicateRuleController, EmailHistoryController, GroupController, HotfixController, UserController, SettingsController, ReportsController]
            }
        ];
    }
    buildMainMenu($view) {
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
    getUserControl($context) {
        const $usercontrol = FwFileMenu.UserControl_render($context);
        this.buildSystemBar($context);
        this.buildOfficeLocation($context);
        var usertype = sessionStorage.getItem('userType');
        var username = sessionStorage.getItem('fullname');
        var $controlUserName = jQuery(`<div title="User Type: ${usertype}">${username}</div>`);
        FwFileMenu.UserControl_addSystemBarControl('username', $controlUserName, $usercontrol);
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
    buildOfficeLocation($usercontrol) {
        const userlocation = JSON.parse(sessionStorage.getItem('location'));
        const userid = JSON.parse(sessionStorage.getItem('userid'));
        const defaultLocation = JSON.parse(sessionStorage.getItem('defaultlocation'));
        const $officelocation = jQuery(`<div class="officelocation">
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
                        let valid = true;
                        const location = FwFormField.getValue($confirmation, 'div[data-datafield="OfficeLocationId"]');
                        const warehouse = FwFormField.getValue($confirmation, 'div[data-datafield="WarehouseId"]');
                        const department = FwFormField.getValue($confirmation, 'div[data-datafield="Department"]');
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
                            const request = {
                                location: location,
                                warehouse: warehouse,
                                department: department,
                                userid: userid.webusersid
                            };
                            RwServices.session.updatelocation(request, function (response) {
                                sessionStorage.setItem('authToken', response.authToken);
                                sessionStorage.setItem('location', JSON.stringify(response.location));
                                sessionStorage.setItem('warehouse', JSON.stringify(response.warehouse));
                                sessionStorage.setItem('department', JSON.stringify(response.department));
                                sessionStorage.setItem('userid', JSON.stringify(response.webusersid));
                                FwConfirmation.destroyConfirmation($confirmation);
                                const activeViewRequest = {};
                                activeViewRequest.uniqueids = {
                                    WebUserId: userid.webusersid
                                };
                                FwAppData.apiMethod(true, 'POST', `api/v1/browseactiveviewfields/browse`, activeViewRequest, FwServices.defaultTimeout, function onSuccess(r) {
                                    const moduleNameIndex = r.ColumnIndex.ModuleName;
                                    const officeLocationIdIndex = r.ColumnIndex.OfficeLocationId;
                                    const activeViewsToReset = r.Rows
                                        .filter(x => x[officeLocationIdIndex] === userlocation.locationid)
                                        .map(x => x[moduleNameIndex]);
                                    for (let i = 0; i < activeViewsToReset.length; i++) {
                                        const controller = `${activeViewsToReset[i]}Controller`;
                                        window[controller].ActiveViewFields = {};
                                        window[controller].ActiveViewFieldsId = undefined;
                                    }
                                    const activeViewFieldsIndex = r.ColumnIndex.ActiveViewFields;
                                    const idIndex = r.ColumnIndex.Id;
                                    const activeViewsToApply = r.Rows.filter(x => x[officeLocationIdIndex] === response.location.locationid);
                                    for (let i = 0; i < activeViewsToApply.length; i++) {
                                        const item = activeViewsToApply[i];
                                        const controller = `${item[moduleNameIndex]}Controller`;
                                        window[controller].ActiveViewFields = JSON.parse(item[activeViewFieldsIndex]);
                                        window[controller].ActiveViewFieldsId = item[idIndex];
                                    }
                                    program.getModule('home');
                                    $usercontrol.find('.officelocation .locationcolor').css('background-color', response.location.locationcolor);
                                    $usercontrol.find('.officelocation .value').text(response.location.location);
                                    if (response.location.location !== defaultLocation.location) {
                                        const nonDefaultStyles = { borderTop: `.3em solid ${response.location.locationcolor}`, borderBottom: `.3em solid ${response.location.locationcolor}` };
                                        jQuery('#master-header').find('div[data-control="FwFileMenu"]').css(nonDefaultStyles);
                                    }
                                    else {
                                        const defaultStyles = { borderTop: `transparent`, borderBottom: `1px solid #9E9E9E` };
                                        jQuery('#master-header').find('div[data-control="FwFileMenu"]').css(defaultStyles);
                                    }
                                }, function onError(r) {
                                    FwFunc.showError(r);
                                }, null);
                            });
                        }
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    buildSystemBar($usercontrol) {
        const $dashboard = jQuery('<i class="material-icons dashboard" title="Dashboard">insert_chart</i>');
        $dashboard.on('click', function () {
            try {
                program.getModule('module/dashboard');
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwFileMenu.UserControl_addSystemBarControl('dashboard', $dashboard, $usercontrol);
        const $settings = jQuery('<i class="material-icons dashboard" title="Settings">settings</i>');
        $settings.on('click', function () {
            try {
                program.getModule('module/settings');
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwFileMenu.UserControl_addSystemBarControl('dashboard', $settings, $usercontrol);
        const $reports = jQuery('<i class="material-icons dashboard" title="Reports">assignment</i>');
        $reports.on('click', function () {
            try {
                program.getModule('module/reports');
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwFileMenu.UserControl_addSystemBarControl('dashboard', $reports, $usercontrol);
    }
}
var masterController = new RwMaster();
//# sourceMappingURL=RwMaster.js.map