class RwMaster extends WebMaster {
    //----------------------------------------------------------------------------------------------
    navigation: any;
    //----------------------------------------------------------------------------------------------
    initMainMenu() {
        let userType = sessionStorage.getItem('userType');
        this.navigation = [];
        
        // TrakitWorks Menu
        let menuTrakitWorks = {
            caption: 'TrakitWorks',
            id: 'B05953D7-DC85-486C-B9A4-7743875DFABC',
            children: []
        };
        if (userType == 'USER')
        {
            menuTrakitWorks.children.push(Constants.Modules.Home.Contact);
            menuTrakitWorks.children.push(Constants.Modules.Home.Deal);
            menuTrakitWorks.children.push(Constants.Modules.Home.Order);
            menuTrakitWorks.children.push(Constants.Modules.Home.PurchaseOrder);
        }
        if (userType == 'USER' || userType == 'CONTACT')
        {
            menuTrakitWorks.children.push(Constants.Modules.Home.Quote);
        }
        if (userType == 'USER')
        {
            menuTrakitWorks.children.push(Constants.Modules.Home.Vendor);
        }
        this.navigation.push(menuTrakitWorks);

        if (userType == 'USER')
        {
            // Inventory Menu
            let menuInventory = {
                caption: 'Inventory',
                id: 'CA7EDF90-F08A-4E5C-BA6B-87DB6A14D485',
                children: [
                    Constants.Modules.Home.Asset, 
                    Constants.Modules.Home.InventoryItem, 
                    Constants.Modules.Home.Repair
                ]
            };
            this.navigation.push(menuInventory);

            let menuWarehouse = {
                caption: 'Warehouse',
                id: '293A157D-EA8E-48F6-AE97-15F9DE53041A',
                children: [
                    Constants.Modules.Home.AssignBarCodes, 
                    Constants.Modules.Home.CheckIn, 
                    Constants.Modules.Home.Contract, 
                    Constants.Modules.Home.Exchange, 
                    Constants.Modules.Home.OrderStatus, 
                    Constants.Modules.Home.PickList, 
                    Constants.Modules.Home.ReceiveFromVendor, 
                    Constants.Modules.Home.ReturnToVendor, 
                    Constants.Modules.Home.StagingCheckout
                ]
            };
            this.navigation.push(menuWarehouse);

            let menuAdministrator = {
                caption: 'Administrator',
                id: 'A3EE3EE9-4C98-4315-B08D-2FAD67C04E07',
                children: [
                    Constants.Modules.Administrator.Control, 
                    Constants.Modules.Administrator.CustomField, 
                    Constants.Modules.Administrator.CustomForm, 
                    Constants.Modules.Administrator.DuplicateRule, 
                    Constants.Modules.Administrator.Group, 
                    Constants.Modules.Administrator.Hotfix, 
                    Constants.Modules.Administrator.Reports, 
                    Constants.Modules.Administrator.Settings, 
                    Constants.Modules.Administrator.User
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
        
        var $usercontrol = FwFileMenu.UserControl_render($context);

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
    }
    //----------------------------------------------------------------------------------------------
}
var masterController: RwMaster = new RwMaster();
