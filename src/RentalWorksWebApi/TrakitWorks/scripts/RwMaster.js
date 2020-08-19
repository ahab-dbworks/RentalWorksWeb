var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
class RwMaster extends WebMaster {
    initMainMenu() {
        let userType = sessionStorage.getItem('userType');
        this.navigation = [];
        let menuTrakitWorks = {
            caption: 'Agent',
            id: 'Agent',
            children: []
        };
        if (userType == 'USER') {
            menuTrakitWorks.children.push(Constants.Modules.Agent.children.Contact);
            menuTrakitWorks.children.push(Constants.Modules.Agent.children.Deal);
            menuTrakitWorks.children.push(Constants.Modules.Agent.children.Order);
            menuTrakitWorks.children.push(Constants.Modules.Agent.children.PurchaseOrder);
        }
        if (userType == 'USER' || userType == 'CONTACT') {
            menuTrakitWorks.children.push(Constants.Modules.Agent.children.Quote);
        }
        if (userType == 'USER') {
            menuTrakitWorks.children.push(Constants.Modules.Agent.children.Vendor);
        }
        this.navigation.push(menuTrakitWorks);
        if (userType == 'USER') {
            let menuInventory = {
                caption: 'Inventory',
                id: 'Inventory',
                children: [
                    Constants.Modules.Inventory.children.Asset,
                    Constants.Modules.Inventory.children.InventoryItem,
                    Constants.Modules.Inventory.children.Repair
                ]
            };
            this.navigation.push(menuInventory);
            let menuWarehouse = {
                caption: 'Warehouse',
                id: 'Warehouse',
                children: [
                    Constants.Modules.Warehouse.children.AssignBarCodes,
                    Constants.Modules.Warehouse.children.CheckIn,
                    Constants.Modules.Warehouse.children.Contract,
                    Constants.Modules.Warehouse.children.Exchange,
                    Constants.Modules.Warehouse.children.OrderStatus,
                    Constants.Modules.Warehouse.children.PickList,
                    Constants.Modules.Warehouse.children.ReceiveFromVendor,
                    Constants.Modules.Warehouse.children.ReturnToVendor,
                    Constants.Modules.Warehouse.children.StagingCheckout
                ]
            };
            this.navigation.push(menuWarehouse);
            let menuUtilities = {
                caption: 'Utilities',
                id: 'Utilities',
                children: [
                    Constants.Modules.Utilities.children.Dashboard,
                    Constants.Modules.Utilities.children.DashboardSettings,
                    Constants.Modules.Utilities.children.QuikActivityCalendar
                ]
            };
            this.navigation.push(menuUtilities);
            let menuAdministrator = {
                caption: 'Administrator',
                id: 'Administrator',
                children: [
                    Constants.Modules.Administrator.children.Alert,
                    Constants.Modules.Administrator.children.CustomField,
                    Constants.Modules.Administrator.children.CustomForm,
                    Constants.Modules.Administrator.children.CustomReportLayout,
                    Constants.Modules.Administrator.children.DuplicateRule,
                    Constants.Modules.Administrator.children.Group,
                    Constants.Modules.Administrator.children.Hotfix,
                    Constants.Modules.Administrator.children.Reports,
                    Constants.Modules.Administrator.children.Settings,
                    Constants.Modules.Administrator.children.User
                ]
            };
            this.navigation.push(menuAdministrator);
        }
    }
    buildMainMenu($view) {
        this.initMainMenu();
        var nodeApplication = FwApplicationTree.getMyTree();
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
        var usertype = sessionStorage.getItem('userType');
        var username = sessionStorage.getItem('fullname');
        var $usercontrol = FwFileMenu.UserControl_render($context);
        this.buildSystemBar($context);
        if (usertype === 'USER') {
            this.buildOfficeLocation($context);
        }
        else if (usertype === 'CONTACT') {
            this.buildDealPicker($context);
        }
        var $controlUserName = jQuery(`<div title="User Type: ${usertype}">${username}</div>`);
        FwFileMenu.UserControl_addSystemBarControl('username', $controlUserName, $usercontrol);
        var $miUserSettings = jQuery(`<div>${RwLanguages.translate('User Profile')}</div>`);
        FwFileMenu.UserControl_addDropDownMenuItem('userprofile', $miUserSettings, $usercontrol);
        $miUserSettings.on('click', (event) => {
            try {
                program.getModule('module/userprofile');
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
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
    buildOfficeLocation($usercontrol) {
        var userlocation = JSON.parse(sessionStorage.getItem('location'));
        var userid = JSON.parse(sessionStorage.getItem('userid'));
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
                $select.on('click', () => __awaiter(this, void 0, void 0, function* () {
                    try {
                        let valid = FwModule.validateForm($confirmation);
                        if (valid) {
                            const locationid = FwFormField.getValueByDataField($confirmation, 'OfficeLocationId');
                            const warehouseid = FwFormField.getValueByDataField($confirmation, 'WarehouseId');
                            const departmentid = FwFormField.getValueByDataField($confirmation, 'DepartmentId');
                            const responseGetOfficeLocationInfo = yield FwAjax.callWebApi({
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
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                }));
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    buildDealPicker($usercontrol) {
        var deal = JSON.parse(sessionStorage.getItem('deal'));
        var $btnDealPicker = jQuery(`<div class="dealpicker" style="display:flex; align-items:center;">
                                       <div class="caption" style="font-size:.8em;color:#b71c1c;flex:0 0 auto; margin:0 .4em 0 0;">Job:</div> 
                                       <div class="value" style="flex:1 1 0;">${deal.deal}<i class="material-icons" style="font-size:inherit;">&#xE5CF</i></div>
                                     </div>`)
            .css('cursor', 'pointer')
            .attr('title', 'Switch Jobs');
        FwFileMenu.UserControl_addSystemBarControl('dealpicker', $btnDealPicker, $usercontrol);
        $btnDealPicker.hover((e) => {
            $btnDealPicker.css('background-color', '#eeeeee');
        }, (e) => {
            $btnDealPicker.css('background-color', 'inherit');
        });
        $btnDealPicker.on('click', function () {
            try {
                var $confirmation = FwConfirmation.renderConfirmation('Switch Jobs...', '');
                const $select = FwConfirmation.addButton($confirmation, 'Select', false);
                var $cancel = FwConfirmation.addButton($confirmation, 'Cancel', true);
                FwConfirmation.addControls($confirmation, `<div class="fwform" data-controller="UserController" style="background-color: transparent;">
                                                             <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                                                               <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Job" data-datafield="DealId" data-validationname="ContactJobValidation" data-required="true"></div>
                                                             </div>
                                                           </div>`);
                const deal = JSON.parse(sessionStorage.getItem('deal'));
                FwFormField.setValueByDataField($confirmation, 'DealId', deal.dealid, deal.deal);
                $select.on('click', () => __awaiter(this, void 0, void 0, function* () {
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
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                }));
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    buildSystemBar($usercontrol) {
        if (sessionStorage.getItem('userType') === 'USER') {
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
}
var masterController = new RwMaster();
//# sourceMappingURL=RwMaster.js.map