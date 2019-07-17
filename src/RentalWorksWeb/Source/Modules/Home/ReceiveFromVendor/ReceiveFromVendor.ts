//routes.push({ pattern: /^module\/receivefromvendor$/, action: function (match: RegExpExecArray) { return ReceiveFromVendorController.getModuleScreen(); } });

class ReceiveFromVendor {
    Module: string = 'ReceiveFromVendor';
    caption: string = Constants.Modules.Home.ReceiveFromVendor.caption;
    nav: string = Constants.Modules.Home.ReceiveFromVendor.nav;
    id: string = Constants.Modules.Home.ReceiveFromVendor.id;
    successSoundFileName: string;
    errorSoundFileName: string;
    notificationSoundFileName: string;

    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('EDIT');

        screen.load = function () {
            FwModule.openModuleTab($form, 'Receive From Vendor', false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        //disables asterisk and save prompt
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');

        let date = new Date(),
            currentDate = date.toLocaleString(),
            currentTime = date.toLocaleTimeString();

        FwFormField.setValueByDataField($form, 'Date', currentDate);
        FwFormField.setValueByDataField($form, 'Time', currentTime);

        const cancelMenuOptionId = Constants.Modules.Home.ReceiveFromVendor.form.menuItems.Cancel.id.replace('{', '').replace('}', '');
        $form.find(`.submenu-btn[data-securityid="${cancelMenuOptionId}"]`).attr('data-enabled', 'false');

        if (typeof parentmoduleinfo !== 'undefined') {
            FwFormField.setValueByDataField($form, 'PurchaseOrderId', parentmoduleinfo.PurchaseOrderId, parentmoduleinfo.PurchaseOrderNumber);
            $form.find('[data-datafield="PurchaseOrderId"] input').change();
            $form.attr('data-showsuspendedsessions', 'false');
        }

        this.getSuspendedSessions($form);
        this.getSoundUrls($form);
        this.getItems($form);
        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    getSuspendedSessions($form) {
        const showSuspendedSessions = $form.attr('data-showsuspendedsessions');
        if (showSuspendedSessions != "false") {
            FwAppData.apiMethod(true, 'GET', 'api/v1/purchaseorder/receivesuspendedsessionsexist', null, FwServices.defaultTimeout,
                response => {
                    $form.find('.buttonbar').append(`<div class="fwformcontrol suspendedsession" data-type="button" style="float:left;">Suspended Sessions</div>`);
                }, ex => FwFunc.showError(ex), $form);

            $form.on('click', '.suspendedsession', e => {
                const $browse = SuspendedSessionController.openBrowse();
                const $popup = FwPopup.renderPopup($browse, { ismodal: true }, 'Suspended Sessions');
                FwPopup.showPopup($popup);
                $browse.data('ondatabind', request => {
                    request.uniqueids = {
                        OfficeLocationId: JSON.parse(sessionStorage.getItem('location')).locationid
                        , SessionType: 'RECEIVE'
                        , OrderType: 'C'
                    }
                });
                FwBrowse.search($browse);

                $browse.on('dblclick', 'tr.viewmode', e => {
                    let $this = jQuery(e.currentTarget);
                    let id = $this.find(`[data-browsedatafield="OrderId"]`).attr('data-originalvalue');
                    let orderNumber = $this.find(`[data-browsedatafield="OrderNumber"]`).attr('data-originalvalue');
                    const cancelMenuOptionId = Constants.Modules.Home.ReceiveFromVendor.form.menuItems.Cancel.id.replace('{', '').replace('}', '');
                    $form.find(`.submenu-btn[data-securityid="${cancelMenuOptionId}"]`).attr('data-enabled', 'true');

                    FwFormField.setValueByDataField($form, 'PurchaseOrderId', id, orderNumber);
                    FwPopup.destroyPopup($popup);
                    $form.find('[data-datafield="PurchaseOrderId"] input').change();
                    $form.find('.suspendedsession').hide();
                });
            });
        }
    }
    //----------------------------------------------------------------------------------------------
    getItems($form) {
        $form.find('[data-datafield="PurchaseOrderId"]').data('onchange', $tr => {
            FwFormField.disable($form.find('[data-datafield="PurchaseOrderId"]'));

            let purchaseOrderId = $tr.find('[data-browsedatafield="PurchaseOrderId"]').attr('data-originalvalue');

            FwFormField.setValueByDataField($form, 'VendorId', $tr.find('[data-browsedatafield="VendorId"]').attr('data-originalvalue'), $tr.find('[data-browsedatafield="Vendor"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'ReferenceNumber', $tr.find('[data-browsedatafield="ReferenceNumber"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'Description', $tr.find('[data-browsedatafield="Description"]').attr('data-originalvalue'));

            let request = {
                PurchaseOrderId: purchaseOrderId
            }

            FwAppData.apiMethod(true, 'POST', 'api/v1/purchaseorder/startreceivecontract', request, FwServices.defaultTimeout, function onSuccess(response) {
                let contractId = response.ContractId,
                    $receiveItemsGridControl: any,
                    max = 9999;

                FwFormField.setValueByDataField($form, 'ContractId', contractId);

                const cancelMenuOptionId = Constants.Modules.Home.ReceiveFromVendor.form.menuItems.Cancel.id.replace('{', '').replace('}', '');
                $form.find(`.submenu-btn[data-securityid="${cancelMenuOptionId}"]`).attr('data-enabled', 'true');

                $receiveItemsGridControl = $form.find('div[data-name="POReceiveItemGrid"]');
                $receiveItemsGridControl.data('ondatabind', function (request) {
                    request.uniqueids = {
                        ContractId: contractId
                        , PurchaseOrderId: purchaseOrderId
                    }
                    request.pagesize = max;
                })
                FwBrowse.search($receiveItemsGridControl);
            }, null, null);

            FwAppData.apiMethod(true, 'GET', `api/v1/purchaseorder/${purchaseOrderId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                if ((response.SubRent == false) && (response.SubSale == false)) {
                    FwFormField.disable($form.find('[data-datafield="AutomaticallyCreateCheckOut"]'));
                    $form.find('[data-datafield="AutomaticallyCreateCheckOut"] input').prop('checked', false);
                }
            }, null, null);

            $form.find('.suspendedsession').hide();
        });
    }
    //----------------------------------------------------------------------------------------------
    getSoundUrls($form): void {
        this.successSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).successSoundFileName;
        this.errorSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).errorSoundFileName;
        this.notificationSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).notificationSoundFileName;
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        const $receiveItemsGrid = $form.find('div[data-grid="POReceiveItemGrid"]');
        const $receiveItemsGridControl = FwBrowse.loadGridFromTemplate('POReceiveItemGrid');
        $receiveItemsGrid.empty().append($receiveItemsGridControl);
        $receiveItemsGridControl.data('ondatabind', request => {
        })
        FwBrowse.init($receiveItemsGridControl);
        FwBrowse.renderRuntimeHtml($receiveItemsGridControl);
    }
    //----------------------------------------------------------------------------------------------
    events($form: any): void {
        // Create Contract
        $form.find('.createcontract').on('click', e => {
            let contractId = FwFormField.getValueByDataField($form, 'ContractId');
            let automaticallyCreateCheckOut = FwFormField.getValueByDataField($form, 'AutomaticallyCreateCheckOut');
            let requestBody: any = {};
            if (automaticallyCreateCheckOut == 'T') {
                requestBody = {
                    CreateOutContracts: true
                }
            }
            if (contractId) {
                FwAppData.apiMethod(true, 'POST', "api/v1/purchaseorder/completereceivecontract/" + contractId, requestBody, FwServices.defaultTimeout, response => {
                    try {
                        for (let i = 0; i < response.length; i++) {
                            let contractInfo: any = {}, $contractForm;
                            contractInfo.ContractId = response[i].ContractId;
                            $contractForm = ContractController.loadForm(contractInfo);
                            FwModule.openSubModuleTab($form, $contractForm);
                        }
                        this.resetForm($form);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                }, null, $form);
            } else {
                FwNotification.renderNotification('WARNING', 'Select a Purchase Order.');
            }
        });
        // Select None
        $form.find('.selectnone').on('click', e => {
            let request: any = {};
            const $receiveItemsGridControl = $form.find('div[data-name="POReceiveItemGrid"]');
            const contractId = FwFormField.getValueByDataField($form, 'ContractId');
            const purchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');

            request.ContractId = contractId;
            request.PurchaseOrderId = purchaseOrderId;
            FwAppData.apiMethod(true, 'POST', `api/v1/purchaseorderreceiveitem/selectnone`, request, FwServices.defaultTimeout, response => {
                FwBrowse.search($receiveItemsGridControl);
            }, ex => {
                FwFunc.showError(ex);
            }, $form, contractId);

            $form.find('.createcontract[data-type="button"]').show();
            $form.find('.createcontract[data-type="btnmenu"]').hide();
        });
        // Select All
        $form.find('.selectall').on('click', e => {
            let request: any = {};
            const $receiveItemsGridControl = $form.find('div[data-name="POReceiveItemGrid"]');
            const contractId = FwFormField.getValueByDataField($form, 'ContractId');
            const purchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');

            request.ContractId = contractId;
            request.PurchaseOrderId = purchaseOrderId;
            FwAppData.apiMethod(true, 'POST', `api/v1/purchaseorderreceiveitem/selectall`, request, FwServices.defaultTimeout, response => {
                FwBrowse.search($receiveItemsGridControl);
            }, ex => {
                FwFunc.showError(ex);
            }, $form, contractId);

            let $itemsTrackedByBarcode = $receiveItemsGridControl.find('[data-browsedatafield="TrackedBy"][data-originalvalue="BARCODE"]');
            if ($itemsTrackedByBarcode.length > 0) {
                $form.find('.createcontract[data-type="button"]').hide();
                $form.find('.createcontract[data-type="btnmenu"]').show();
            }
        });
        //Hide/Show Options
        const $optionToggle = $form.find('.optiontoggle');
        $form.find('.options').toggle();
        $optionToggle.on('click', () => {
            $form.find('.options').toggle();
        });
    };
    //----------------------------------------------------------------------------------------------
    beforeValidate($browse, $grid, request) {
        const validationName = request.module;
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));

        switch (validationName) {
            case 'PurchaseOrderValidation':
                request.miscfields = {
                    ReceiveFromVendor: true,
                    ReceivingWarehouseId: warehouse.warehouseid,
                };
                break;
        };
    };
    //----------------------------------------------------------------------------------------------
    addButtonMenu($form) {
        let $buttonmenu = $form.find('.createcontract[data-type="btnmenu"]');
        let $createContract = FwMenu.generateButtonMenuOption('Create Contract')
            , $createContractAndAssignBarCodes = FwMenu.generateButtonMenuOption('Create Contract and Assign Bar Codes');

        $createContract.on('click', e => {
            e.stopPropagation();
            $form.find('.createcontract').click();
        });

        $createContractAndAssignBarCodes.on('click', e => {
            e.stopPropagation();
            let request: any = {};
            const contractId = FwFormField.getValueByDataField($form, 'ContractId');
            FwAppData.apiMethod(true, 'POST', `api/v1/purchaseorder/completereceivecontract/${contractId}`, request, FwServices.defaultTimeout,
                response => {
                    let contractInfo: any = {}, $contractForm, $assignBarCodesForm;

                    contractInfo.ContractNumber = response[0].ContractNumber;
                    contractInfo.PurchaseOrderNumber = response[0].PurchaseOrderNumber;
                    contractInfo.PurchaseOrderId = response[0].PurchaseOrderId;
                    $assignBarCodesForm = AssignBarCodesController.openForm('EDIT', contractInfo);
                    FwModule.openSubModuleTab($form, $assignBarCodesForm);
                    jQuery('.tab.submodule.active').find('.caption').html('Assign Bar Codes');

                    contractInfo.ContractId = response[0].ContractId;
                    $contractForm = ContractController.loadForm(contractInfo);
                    FwModule.openSubModuleTab($form, $contractForm);

                    this.resetForm($form);
                }, null, $form);
        });

        let menuOptions = [];
        menuOptions.push($createContract, $createContractAndAssignBarCodes);

        FwMenu.addButtonMenuOptions($buttonmenu, menuOptions);
    }
    //----------------------------------------------------------------------------------------------
    resetForm($form) {
        $form.find('.fwformfield').not('[data-type="date"], [data-type="time"]').find('input').val('');
        let $receiveItemsGridControl = $form.find('div[data-name="POReceiveItemGrid"]');
        $receiveItemsGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContractId: ''
                , PurchaseOrderId: ''
            }
        })
        FwBrowse.search($receiveItemsGridControl);
        FwFormField.enable($form.find('[data-datafield="PurchaseOrderId"]'));
        let date = new Date(),
            currentDate = date.toLocaleString(),
            currentTime = date.toLocaleTimeString();
        FwFormField.setValueByDataField($form, 'Date', currentDate);
        FwFormField.setValueByDataField($form, 'Time', currentTime);
        $form.find('.createcontract[data-type="button"]').show();
        $form.find('.createcontract[data-type="btnmenu"]').hide();
        const cancelMenuOptionId = Constants.Modules.Home.ReceiveFromVendor.form.menuItems.Cancel.id.replace('{', '').replace('}', '');
        $form.find(`.submenu-btn[data-securityid="${cancelMenuOptionId}"]`).attr('data-enabled', 'false');

        $form.find('.suspendedsession').show();
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
        <div id="receivefromvendorform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Receive From Vendor" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="ReceiveFromVendorController">
            <div class="flexpage">
            <div class="flexrow">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Receive From Vendor">
                <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 450px;">
                    <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="PO No." data-datafield="PurchaseOrderId" data-displayfield="PurchaseOrderNumber" data-validationname="PurchaseOrderValidation" data-formbeforevalidate="beforeValidate" style="flex:0 1 175px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="ContractId" data-datafield="ContractId" style="display:none; flex:0 1 175px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Vendor" data-datafield="VendorId" data-displyfield="Vendor" data-validationname="VendorValidation" style="flex:1 1 300px;" data-enabled="false"></div>
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date" data-datafield="Date" style="flex:0 1 175px;" data-enabled="false"></div>
                    </div>
                    </div>
                </div>
                <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 450px;">
                    <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Packing Slip" data-datafield="PackingSlip" style="flex:0 1 175px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" style="flex:1 1 350px;" data-enabled="false"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Reference No." data-datafield="ReferenceNumber" style="flex:1 1 100px;" data-enabled="false"></div>
                        <div data-control="FwFormField" data-type="time" class="fwcontrol fwformfield" data-caption="Time" data-datafield="Time" style="flex:0 1 175px;" data-enabled="false"></div>
                    </div>
                    <div class="error-msg"></div>
                    </div>
                </div>
                <div class="flexrow POReceiveItemGrid">
                    <div data-control="FwGrid" data-grid="POReceiveItemGrid" data-securitycaption="Receive Items"></div>
                </div>
                <div class="formrow">
                    <div class="optiontoggle fwformcontrol" data-type="button" style="float:left">Options &#8675;</div>
                    <div class="selectall fwformcontrol" data-type="button" style="float:left; margin-left:10px;">Select All</div>
                    <div class="selectnone fwformcontrol" data-type="button" style="float:left; margin-left:10px;">Select None</div>
                    <div class="createcontract fwformcontrol" data-type="button" style="float:right;">Create Contract</div>
                    <div class="createcontract fwformcontrol" data-type="btnmenu" style="display:none; float:right;" data-caption="Create Contract"></div>
                </div>
                </div>
            </div>
            <div class="flexrow">
                <div class="flexcolumn" style="flex:1 1 450px;">
                <div class="flexrow">
                    <div data-control="FwFormField" data-type="checkbox" class="options fwcontrol fwformfield" data-caption="Automatically Create CHECK-OUT Contract for Sub Rental and Sub Sale items received" data-datafield="AutomaticallyCreateCheckOut" style="flex:1 1 150px;"></div>
                </div>
                </div>
            </div>
          </div>
        </div>
        `;
    }
}
//----------------------------------------------------------------------------------------------
//Cancel
FwApplicationTree.clickEvents[Constants.Modules.Home.ReceiveFromVendor.form.menuItems.Cancel.id] = function (event: JQuery.ClickEvent) {
    const $form = jQuery(this).closest('.fwform');
    const contractId = FwFormField.getValueByDataField($form, 'ContractId');
    try {
        const request: any = {};
        request.ContractId = contractId;
        FwAppData.apiMethod(true, 'POST', `api/v1/contract/cancelcontract`, request, FwServices.defaultTimeout,
            response => {
                ReceiveFromVendorController.resetForm($form);
                FwNotification.renderNotification('SUCCESS', 'Session succesfully cancelled.');
            },
            ex => FwFunc.showError(ex),
            null, $form);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
var ReceiveFromVendorController = new ReceiveFromVendor();