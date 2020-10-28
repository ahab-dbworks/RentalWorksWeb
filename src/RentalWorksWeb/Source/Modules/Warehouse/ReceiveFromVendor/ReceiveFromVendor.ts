//routes.push({ pattern: /^module\/receivefromvendor$/, action: function (match: RegExpExecArray) { return ReceiveFromVendorController.getModuleScreen(); } });

class ReceiveFromVendor {
    Module: string = 'ReceiveFromVendor';
    apiurl: string = 'api/v1/receivefromvendor';
    caption: string = Constants.Modules.Warehouse.children.ReceiveFromVendor.caption;
    nav: string = Constants.Modules.Warehouse.children.ReceiveFromVendor.nav;
    id: string = Constants.Modules.Warehouse.children.ReceiveFromVendor.id;
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);

        FwMenu.addSubMenuItem(options.$groupOptions, 'Cancel Receive From Vendor', '', (e: JQuery.ClickEvent) => {
            try {
                this.CancelReceiveFromVendor(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $form = this.openForm('EDIT');

        screen.load = () => {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
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

        $form.find('div.caption:contains(Cancel Receive From Vendor)').parent().attr('data-enabled', 'false');

        if (typeof parentmoduleinfo !== 'undefined') {
            FwFormField.setValueByDataField($form, 'PurchaseOrderId', parentmoduleinfo.PurchaseOrderId, parentmoduleinfo.PurchaseOrderNumber);
            $form.find('[data-datafield="PurchaseOrderId"] input').change();
            $form.attr('data-showsuspendedsessions', 'false');
        }

        this.getSuspendedSessions($form);
        this.getItems($form);
        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    CancelReceiveFromVendor($form: JQuery): void {
        try {
            const contractId = FwFormField.getValueByDataField($form, 'ContractId');
            if (contractId != '') {
                const $confirmation = FwConfirmation.renderConfirmation('Cancel Receive From Vendor', 'Cancelling this Receive From Vendor Session will cause all transacted items to be cancelled. Continue?');
                const $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
                const $no = FwConfirmation.addButton($confirmation, 'No', true);

                $yes.on('click', () => {
                    try {
                        const request: any = {};
                        request.ContractId = contractId;
                        FwAppData.apiMethod(true, 'POST', `${this.apiurl}/cancelcontract`, request, FwServices.defaultTimeout,
                            response => {
                                FwConfirmation.destroyConfirmation($confirmation);
                                ReceiveFromVendorController.resetForm($form);
                                FwNotification.renderNotification('SUCCESS', 'Session succesfully cancelled.');
                            },
                            ex => FwFunc.showError(ex),
                            $confirmation.find('.fwconfirmationbox'));
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            }
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    getSuspendedSessions($form) {
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        const showSuspendedSessions = $form.attr('data-showsuspendedsessions');
        let sessionType = 'RECEIVE';
        if (showSuspendedSessions != "false") {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/suspendedsessionsexist?warehouseId=${warehouse.warehouseid}`, null, FwServices.defaultTimeout,
                response => {
                    if (response) {
                        $form.find('.buttonbar').append(`<div class="fwformcontrol suspendedsession" data-type="button" style="float:left;">Suspended Sessions</div>`);
                    }
                }, ex => FwFunc.showError(ex), $form);

            $form.on('click', '.suspendedsession', e => {
                SuspendedSessionController.sessionType = sessionType;
                const $browse = SuspendedSessionController.openBrowse();
                const $popup = FwPopup.renderPopup($browse, { ismodal: true }, 'Suspended Sessions');
                FwPopup.showPopup($popup);
                $browse.data('ondatabind', request => {
                    request.uniqueids = {
                        SessionType: sessionType,
                        WarehouseId: JSON.parse(sessionStorage.getItem('warehouse')).warehouseid
                    }
                });
                FwBrowse.search($browse);

                $browse.on('dblclick', 'tr.viewmode', e => {
                    let $this = jQuery(e.currentTarget);
                    let id = $this.find(`[data-browsedatafield="PurchaseOrderId"]`).attr('data-originalvalue');
                    let orderNumber = $this.find(`[data-browsedatafield="OrderNumber"]`).attr('data-originalvalue');
                    const contractId = $this.find(`[data-browsedatafield="ContractId"]`).attr('data-originalvalue');
                    FwFormField.setValueByDataField($form, 'ContractId', contractId);
                    $form.find('div.caption:contains(Cancel Receive From Vendor)').parent().attr('data-enabled', 'true');
                    FwFormField.setValueByDataField($form, 'PurchaseOrderId', id, orderNumber);
                    FwPopup.destroyPopup($popup);
                    $form.find('[data-datafield="PurchaseOrderId"] input').change();
                    $form.find('.suspendedsession').hide();

                    const $receiveItemsGrid = $form.find('div[data-name="POReceiveItemGrid"]');
                    FwBrowse.search($receiveItemsGrid);
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

            const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
            const contractId = FwFormField.getValueByDataField($form, 'ContractId');
            if (contractId.length === 0) {
                let request = {
                    PurchaseOrderId: purchaseOrderId,
                    WarehouseId: warehouse.warehouseid
                }

                FwAppData.apiMethod(true, 'POST', `${this.apiurl}/startsession`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    let contractId = response.ContractId,
                        $receiveItemsGridControl: any;

                    FwFormField.setValueByDataField($form, 'ContractId', contractId);
                    $form.find('.suspendedsession').hide();
                    $form.find('div.caption:contains(Cancel Receive From Vendor)').parent().attr('data-enabled', 'true');

                    $receiveItemsGridControl = $form.find('div[data-name="POReceiveItemGrid"]');
                    FwBrowse.search($receiveItemsGridControl);
                }, null, $form);

                FwAppData.apiMethod(true, 'GET', `api/v1/purchaseorder/${purchaseOrderId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    if ((response.SubRent == false) && (response.SubSale == false)) {
                        FwFormField.disable($form.find('[data-datafield="AutomaticallyCreateCheckOut"]'));
                        $form.find('[data-datafield="AutomaticallyCreateCheckOut"] input').prop('checked', false);
                    }
                }, null, $form);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        FwBrowse.renderGrid({
            nameGrid: 'POReceiveItemGrid',
            gridSecurityId: 'uYBpfQCZBM4V6',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 9999,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasEdit = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    ContractId: FwFormField.getValueByDataField($form, 'ContractId'),
                    PurchaseOrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                    WarehouseId: JSON.parse(sessionStorage.getItem('warehouse')).warehouseid,
                };
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    events($form: any): void {
        // Create Contract
        $form.find('.createcontract').on('click', e => {
            let contractId = FwFormField.getValueByDataField($form, 'ContractId');
            let automaticallyCreateCheckOut: boolean = FwFormField.getValueByDataField($form, 'AutomaticallyCreateCheckOut');
            let requestBody: any = {
                CreateOutContracts: automaticallyCreateCheckOut
            };
            if (contractId) {
                FwAppData.apiMethod(true, 'POST', `${this.apiurl}/completecontract/` + contractId, requestBody, FwServices.defaultTimeout, response => {
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
            request.WarehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
            FwAppData.apiMethod(true, 'POST', `${this.apiurl}/selectnone`, request, FwServices.defaultTimeout, response => {
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
            request.WarehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
            FwAppData.apiMethod(true, 'POST', `${this.apiurl}/selectall`, request, FwServices.defaultTimeout, response => {
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
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));

        switch (datafield) {
            case 'PurchaseOrderId':
                request.miscfields = {
                    ReceiveFromVendor: true,
                    ReceivingWarehouseId: warehouse.warehouseid,
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatepurchaseorder`);
                break;
        };
    };
    //----------------------------------------------------------------------------------------------
    addButtonMenu($form) {
        let $buttonmenu = $form.find('.createcontract[data-type="btnmenu"]');
        let $createContract = FwMenu.generateButtonMenuOption('Create Contract'),
            $createContractAndAssignBarCodes = FwMenu.generateButtonMenuOption('Create Contract and Assign Bar Codes');

        $createContract.on('click', e => {
            e.stopPropagation();
            $form.find('.createcontract').click();
        });

        $createContractAndAssignBarCodes.on('click', e => {
            e.stopPropagation();
            let request: any = {};
            const contractId = FwFormField.getValueByDataField($form, 'ContractId');
            FwAppData.apiMethod(true, 'POST', `${this.apiurl}/completecontract/${contractId}`, request, FwServices.defaultTimeout,
                response => {
                    let contractInfo: any = {}, $contractForm, $assignBarCodesForm;

                    contractInfo.ContractNumber = response[0].ContractNumber;
                    contractInfo.PurchaseOrderNumber = response[0].PurchaseOrderNumber;
                    contractInfo.PurchaseOrderId = response[0].PurchaseOrderId;
                    $assignBarCodesForm = AssignBarCodesController.openForm('EDIT', contractInfo);
                    FwModule.openSubModuleTab($form, $assignBarCodesForm);
                    const $tab = FwTabs.getTabByElement($assignBarCodesForm);
                    $tab.find('.caption').html('Assign Bar Codes');

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
        $receiveItemsGridControl.find('tbody').empty();

        FwFormField.enable($form.find('[data-datafield="PurchaseOrderId"]'));
        let date = new Date(),
            currentDate = date.toLocaleString(),
            currentTime = date.toLocaleTimeString();
        FwFormField.setValueByDataField($form, 'Date', currentDate);
        FwFormField.setValueByDataField($form, 'Time', currentTime);
        $form.find('.createcontract[data-type="button"]').show();
        $form.find('.createcontract[data-type="btnmenu"]').hide();
        $form.find('div.caption:contains(Cancel Receive From Vendor)').parent().attr('data-enabled', 'false');

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
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="PO No." data-datafield="PurchaseOrderId" data-displayfield="PurchaseOrderNumber" data-validationname="PurchaseOrderValidation" style="flex:0 1 175px;"></div>
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
                    <div class="createcontract fwformcontrol" data-type="btnmenu" style="display:none; float:right;min-width:205px;" data-caption="Create Contract"></div>
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
var ReceiveFromVendorController = new ReceiveFromVendor();