class RwMaster extends WebMaster {
    //----------------------------------------------------------------------------------------------
    navigation: any;
    //----------------------------------------------------------------------------------------------
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
                children: [RentalInventoryController, SalesInventoryController, PartsInventoryController, AssetController, ContainerController, RepairController, CompleteQcController, PhysicalInventoryController]
            },
            {
                caption: 'Warehouse',
                id: '22D67715-9C24-4A06-A009-CB10A1EC746B',
                children: [OrderStatusController, PickListController, ContractController, StagingCheckoutController, ExchangeController, CheckInController, ReceiveFromVendorController,
                    ReturnToVendorController, AssignBarCodesController, TransferStatusController, TransferOrderController, ManifestController, TransferReceiptController, TransferOutController, TransferInController,
                    ContainerStatusController, FillContainerController, EmptyContainerController, RemoveFromContainerController]
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
        const $usercontrol = FwFileMenu.UserControl_render($context);

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
