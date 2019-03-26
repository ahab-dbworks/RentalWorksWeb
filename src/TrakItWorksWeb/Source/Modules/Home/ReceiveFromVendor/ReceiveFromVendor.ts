routes.push({ pattern: /^module\/receivefromvendor$/, action: function (match: RegExpExecArray) { return ReceiveFromVendorController.getModuleScreen(); } });

class ReceiveFromVendor {
    Module: string = 'ReceiveFromVendor';
    caption: string = 'Receive From Vendor';
    nav: string = 'module/receivefromvendor';
    id: string = 'EC4052D5-664E-4C34-8802-78E086920628';
    successSoundFileName: string;
    errorSoundFileName: string;
    notificationSoundFileName: string;

    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
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
        //var $form;

        //$form = FwModule.loadFormFromTemplate(this.Module);
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        $form.off('change keyup', '.fwformfield[data-isuniqueid!="true"][data-enabled="true"][data-datafield!=""]');

        let date = new Date(),
            currentDate = date.toLocaleString(),
            currentTime = date.toLocaleTimeString();

        FwFormField.setValueByDataField($form, 'Date', currentDate);
        FwFormField.setValueByDataField($form, 'Time', currentTime);


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
        let showSuspendedSessions = $form.attr('data-showsuspendedsessions');

        if (showSuspendedSessions != "false") {
            FwAppData.apiMethod(true, 'GET', 'api/v1/purchaseorder/receivesuspendedsessionsexist', null, FwServices.defaultTimeout, function onSuccess(response) {
                $form.find('.buttonbar').append(`<div class="fwformcontrol suspendedsession" data-type="button" style="float:left;">Suspended Sessions</div>`);
            }, null, $form);

            $form.on('click', '.suspendedsession', e => {
                let html = `<div>
                 <div style="background-color:white; padding-right:10px; text-align:right;" class="close-modal"><i style="cursor:pointer;" class="material-icons">clear</i></div>
<div id="suspendedSessions" style="max-width:1400px; max-height:750px; overflow:auto;"></div>
            </div>`;

                let $popup = FwPopup.renderPopup(jQuery(html), { ismodal: true });

                let $browse = SuspendedSessionController.openBrowse();
                let officeLocationId = JSON.parse(sessionStorage.getItem('location'));
                officeLocationId = officeLocationId.locationid;
                $browse.data('ondatabind', function (request) {
                    request.uniqueids = {
                        OfficeLocationId: officeLocationId
                        , SessionType: 'RECEIVE'
                        , OrderType: 'C'
                    }
                });

                FwPopup.showPopup($popup);
                jQuery('#suspendedSessions').append($browse);
                FwBrowse.search($browse);

                $popup.find('.close-modal > i').one('click', function (e) {
                    FwPopup.destroyPopup($popup);
                    jQuery(document).find('.fwpopup').off('click');
                    jQuery(document).off('keydown');
                });

                $browse.on('dblclick', 'tr.viewmode', e => {
                    let $this = jQuery(e.currentTarget);
                    let id = $this.find(`[data-browsedatafield="OrderId"]`).attr('data-originalvalue');
                    let orderNumber = $this.find(`[data-browsedatafield="OrderNumber"]`).attr('data-originalvalue');
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
        let $receiveItemsGrid: any,
            $receiveItemsGridControl: any;

        $receiveItemsGrid = $form.find('div[data-grid="POReceiveItemGrid"]');
        $receiveItemsGridControl = jQuery(jQuery('#tmpl-grids-POReceiveItemGridBrowse').html());
        $receiveItemsGrid.empty().append($receiveItemsGridControl);
        $receiveItemsGridControl.data('ondatabind', function (request) {
        })
        FwBrowse.init($receiveItemsGridControl);
        FwBrowse.renderRuntimeHtml($receiveItemsGridControl);
    }
    //----------------------------------------------------------------------------------------------
    events($form: any): void {
        let self = this;

        // Create Contract
        $form.find('.createcontract').on('click', e => {
            let contractId = FwFormField.getValueByDataField($form, 'ContractId');
            let automaticallyCreateCheckOut = FwFormField.getValueByDataField($form, 'AutomaticallyCreateCheckOut');
            let date = new Date(),
                currentDate = date.toLocaleString(),
                currentTime = date.toLocaleTimeString();

            let requestBody: any = {};
            if (automaticallyCreateCheckOut == 'T') {
                requestBody = {
                    CreateOutContracts: true
                }
            }
            if (contractId) {
                FwAppData.apiMethod(true, 'POST', "api/v1/purchaseorder/completereceivecontract/" + contractId, requestBody, FwServices.defaultTimeout, function onSuccess(response) {
                    try {
                        for (let i = 0; i < response.length; i++) {
                            let contractInfo: any = {}, $contractForm;
                            contractInfo.ContractId = response[i].ContractId;
                            $contractForm = ContractController.loadForm(contractInfo);
                            FwModule.openSubModuleTab($form, $contractForm);
                        }
                        $form.find('.fwformfield').not('[data-type="date"], [data-type="time"]').find('input').val('');
                        let $receiveItemsGridControl = $form.find('div[data-name="POReceiveItemGrid"]');
                        $receiveItemsGridControl.data('ondatabind', function (request) {
                            request.uniqueids = {
                                ContractId: contractId
                                , PurchaseOrderId: ''
                            }
                        })
                        FwBrowse.search($receiveItemsGridControl);
                        FwFormField.enable($form.find('[data-datafield="PurchaseOrderId"]'));
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                    FwFormField.setValueByDataField($form, 'Date', currentDate);
                    FwFormField.setValueByDataField($form, 'Time', currentTime);
                    $form.find('.createcontract[data-type="button"]').show();
                    $form.find('.createcontract[data-type="btnmenu"]').hide();
                }, null, $form);
            } else {
                FwNotification.renderNotification('WARNING', 'Select a Purchase Order.');
            }
        });
        // Select None
        $form.find('.selectnone').on('click', e => {
            let request: any = {}, quantity;
            const $receiveItemsGridControl = $form.find('div[data-name="POReceiveItemGrid"]');
            const contractId = FwFormField.getValueByDataField($form, 'ContractId');
            const purchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');

            request.ContractId = contractId;
            request.PurchaseOrderId = purchaseOrderId;
            FwAppData.apiMethod(true, 'POST', `api/v1/purchaseorderreceiveitem/selectnone`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.search($receiveItemsGridControl);
            }, function onError(response) {
                FwFunc.showError(response);
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
            FwAppData.apiMethod(true, 'POST', `api/v1/purchaseorderreceiveitem/selectall`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.search($receiveItemsGridControl);
            }, function onError(response) {
                FwFunc.showError(response);
            }, $form, contractId);

            let $itemsTrackedByBarcode = $receiveItemsGridControl.find('[data-browsedatafield="TrackedBy"][data-originalvalue="BARCODE"]');
            if ($itemsTrackedByBarcode.length > 0) {
                $form.find('.createcontract[data-type="button"]').hide();
                $form.find('.createcontract[data-type="btnmenu"]').show();
            }
        });
        //Hide/Show Options
        var $optionToggle = $form.find('.optiontoggle');
        $form.find('.options').toggle();
        $optionToggle.on('click', function () {
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
        let $createContract = FwMenu.generateButtonMenuOption('Create Contract');
        let $createContractAndAssignBarcodes = FwMenu.generateButtonMenuOption('Create Contract and Assign Barcodes');

        $createContract.on('click', e => {
            e.stopPropagation();
            $form.find('.createcontract').click();
        });

        $createContractAndAssignBarcodes.on('click', e => {
            e.stopPropagation();
            let request: any = {};
            const contractId = FwFormField.getValueByDataField($form, 'ContractId');
            FwAppData.apiMethod(true, 'POST', `api/v1/purchaseorder/completereceivecontract/${contractId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                let contractInfo: any = {}, $contractForm, $assignBarCodesForm;

                contractInfo.ContractNumber = response[0].ContractNumber;
                contractInfo.PurchaseOrderNumber = response[0].PurchaseOrderNumber;
                contractInfo.PurchaseOrderId = response[0].PurchaseOrderId;
                $assignBarCodesForm = AssignBarcodesController.openForm('EDIT', contractInfo);
                FwModule.openSubModuleTab($form, $assignBarCodesForm);
                jQuery('.tab.submodule.active').find('.caption').html('Assign Bar Codes');

                contractInfo.ContractId = response[0].ContractId;
                $contractForm = ContractController.loadForm(contractInfo);
                FwModule.openSubModuleTab($form, $contractForm);

                $form.find('.fwformfield').not('[data-type="date"], [data-type="time"]').find('input').val('');
                let $receiveItemsGridControl = $form.find('div[data-name="POReceiveItemGrid"]');
                $receiveItemsGridControl.data('ondatabind', function (request) {
                    request.uniqueids = {
                        ContractId: contractId
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
            }, null, $form);
        });

        let menuOptions = [];
        menuOptions.push($createContract, $createContractAndAssignBarcodes);

        FwMenu.addButtonMenuOptions($buttonmenu, menuOptions);
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
        <div id="receivefromvendorform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Receive From Vendor" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="ReceiveFromVendorController">
          <div class="flexpage notabs">

            <div class="flexrow">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Receive From Vendor">
                <div class="flexrow">
                  <div class=flexcolumn" style="flex:1 1 150px;">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="PO No." data-datafield="PurchaseOrderId" data-displayfield="PurchaseOrderNumber" data-validationname="PurchaseOrderValidation" data-formbeforevalidate="beforeValidate" style="flex:1 1 150px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="ContractId" data-datafield="ContractId" style="display:none; flex:0 1 175px;"></div>
                    </div>                
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Packing Slip" data-datafield="PackingSlip" style="flex:1 1 150px;"></div>
                    </div>  
                  </div>    
                  <div class=flexcolumn" style="flex:2 1 575px;">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Vendor" data-datafield="VendorId" data-displyfield="Vendor" data-validationname="VendorValidation" style="flex:2 1 400px;" data-enabled="false"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Reference No." data-datafield="ReferenceNumber" style="flex:1 1 150px;" data-enabled="false"></div>                  
                    </div>                
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" style="flex:2 1 550px;" data-enabled="false"></div>              
                    </div> 
                  </div>    
                  <div class=flexcolumn" style="flex:1 1 125px;">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date" data-datafield="Date" style="flex:1 1 150px;" data-enabled="false"></div>              
                    </div>                
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="time" class="fwcontrol fwformfield" data-caption="Time" data-datafield="Time" style="flex:1 1 150px;" data-enabled="false"></div>              
                    </div> 
                  </div>
                </div>
                <div class="flexrow errormsg"></div>
              </div>
            </div>
            <div class="flexrow POReceiveItemGrid" style="border:1px solid black;min-height:250px;margin:10px;">##### ADD RECEIVE ITEMS GRID HERE #####
              <div data-control="FwGrid" data-grid="POReceiveItemGrid" data-securitycaption="Receive Items"></div>
            </div>
            <div class="flexrow">
              <div class="optiontoggle fwformcontrol" data-type="button" style="flex:0 1 90px;margin-left:10px;">Options &#8675;</div>
              <div class="selectall fwformcontrol" data-type="button" style="flex:0 1 90px;margin-left:10px;">Select All</div>
              <div class="selectnone fwformcontrol" data-type="button" style="flex:0 1 100px;margin-left:10px;">Select None</div>
              <div class="createcontract fwformcontrol" data-type="button" style="flex:0 1 140px;margin-left:10px;">Create Contract</div>
              <div class="createcontract fwformcontrol" data-type="btnmenu" style="display:none;flex:0 1 140px;" data-caption="Create Contract"></div>
            </div>
            <div class="flexrow">
              <div data-control="FwFormField" data-type="checkbox" class="options fwcontrol fwformfield" data-caption="Automatically Create CHECK-OUT Contract for Sub Rental and Sub Sale items received" data-datafield="AutomaticallyCreateCheckOut" style="flex:1 1 150px;"></div>
            </div>
          </div>
        </div>`;
    }
    //----------------------------------------------------------------------------------------------
}
var ReceiveFromVendorController = new ReceiveFromVendor();
