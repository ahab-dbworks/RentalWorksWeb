routes.push({ pattern: /^module\/returntovendor$/, action: function (match: RegExpExecArray) { return ReturnToVendorController.getModuleScreen(); } });

class ReturnToVendor {
    Module: string = 'ReturnToVendor';
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
            FwModule.openModuleTab($form, 'Return To Vendor', false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-ReturnToVendorForm').html());
        $form = FwModule.openForm($form, mode);

        $form.off('change keyup', '.fwformfield[data-isuniqueid!="true"][data-enabled="true"][data-datafield!=""]');
        //$form.find('div[data-datafield="PurchaseOrderId"] fwformfield-text input').focus();
        //$form.find('div[data-datafield="PurchaseOrderId"] input fwformfield-text').focus();
        //$form.find('div[data-displayfield="PurchaseOrderNumber"] input').focus();

        let date = new Date(),
            currentDate = date.toLocaleString(),
            currentTime = date.toLocaleTimeString();

        FwFormField.setValueByDataField($form, 'Date', currentDate);
        FwFormField.setValueByDataField($form, 'Time', currentTime);

        if (typeof parentmoduleinfo !== 'undefined') {
            FwFormField.setValueByDataField($form, 'PurchaseOrderId', parentmoduleinfo.PurchaseOrderId, parentmoduleinfo.PurchaseOrderNumber); 
            $form.find('[data-datafield="PurchaseOrderId"] input').change();
        }
        this.getSoundUrls($form);
        this.getItems($form);
        this.events($form);

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    getItems($form) {
        let successSound, self = this;
        successSound = new Audio(this.successSoundFileName);
        $form.find('[data-datafield="PurchaseOrderId"]').data('onchange', $tr => {
            FwFormField.disable($form.find('[data-datafield="PurchaseOrderId"]'));

            let purchaseOrderId = $tr.find('[data-browsedatafield="PurchaseOrderId"]').attr('data-originalvalue');

            FwFormField.setValueByDataField($form, 'VendorId', $tr.find('[data-browsedatafield="VendorId"]').attr('data-originalvalue'), $tr.find('[data-browsedatafield="Vendor"]').attr('data-originalvalue')); 
            //FwFormField.setValueByDataField($form, 'ReferenceNumber', $tr.find('[data-browsedatafield="ReferenceNumber"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'Description', $tr.find('[data-browsedatafield="Description"]').attr('data-originalvalue'));

            let request = {
                PurchaseOrderId: purchaseOrderId
            }

            FwAppData.apiMethod(true, 'POST', 'api/v1/purchaseorder/startreturncontract', request, FwServices.defaultTimeout, function onSuccess(response) {
                let contractId = response.ContractId,
                    $pOReturnItemGridControl: any,
                    max = 9999;

                FwFormField.setValueByDataField($form, 'ContractId', contractId);

                $pOReturnItemGridControl = $form.find('div[data-name="POReturnItemGrid"]');
                $pOReturnItemGridControl.data('ondatabind', function (request) {
                    request.uniqueids = {
                        ContractId: contractId,
                        PurchaseOrderId: purchaseOrderId
                    }
                    request.pagesize = max;
                })
                FwBrowse.search($pOReturnItemGridControl);
            }, null, null);
        });
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        //console.log('po', $form.find('div[data-datafield="PurchaseOrderId"] input'))
        //$form.find('div[data-datafield="PurchaseOrderId"] fwformfield-control input').focus();
        //$form.find('div[data-datafield="PurchaseOrderId"] fwformfield-control ').focus();
        //$form.find('div[data-datafield="PurchaseOrderId"] input').focus();
    };
    //----------------------------------------------------------------------------------------------
    getSoundUrls($form): void {
        this.successSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).successSoundFileName;
        this.errorSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).errorSoundFileName;
        this.notificationSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).notificationSoundFileName;
    };
    //----------------------------------------------------------------------------------------------
    renderGrids($form:any) {
        let $pOReturnItemGrid: any,
            $pOReturnItemGridControl: any;

        $pOReturnItemGrid = $form.find('div[data-grid="POReturnItemGrid"]');
        $pOReturnItemGridControl = jQuery(jQuery('#tmpl-grids-POReturnItemGridBrowse').html());
        $pOReturnItemGrid.empty().append($pOReturnItemGridControl);
        $pOReturnItemGridControl.data('ondatabind', function (request) {
        
        })
        FwBrowse.init($pOReturnItemGridControl);
        FwBrowse.renderRuntimeHtml($pOReturnItemGridControl);

        let $poReturnBarCodeGrid: any,
            $poReturnBarCodeGridControl: any;

        $poReturnBarCodeGrid = $form.find('div[data-grid="POReturnBarCodeGrid"]');
        $poReturnBarCodeGridControl = jQuery(jQuery('#tmpl-grids-POReturnBarCodeGridBrowse').html());
        $poReturnBarCodeGrid.empty().append($poReturnBarCodeGridControl);
        $poReturnBarCodeGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                PurchaseOrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                ReturnContractId: FwFormField.getValueByDataField($form, 'ContractId')
            }
        })
        FwBrowse.init($poReturnBarCodeGridControl);
        FwBrowse.renderRuntimeHtml($poReturnBarCodeGridControl);
    };
    //----------------------------------------------------------------------------------------------
    events($form: any): void {
        let self = this;

        // Create Contract
        $form.find('.createcontract').on('click', e => {
            let date = new Date(),
                currentDate = date.toLocaleString(),
                currentTime = date.toLocaleTimeString();
            let contractId = FwFormField.getValueByDataField($form, 'ContractId');
            FwAppData.apiMethod(true, 'POST', `api/v1/purchaseorder/completereturncontract/${contractId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                try {
                    let contractInfo: any = {}, $contractForm;
                    contractInfo.ContractId = contractId;
                    $contractForm = ContractController.loadForm(contractInfo);
                    FwModule.openSubModuleTab($form, $contractForm);

                    $form.find('.fwformfield').not('[data-type="date"], [data-type="time"]').find('input').val('');
                    let $pOReturnItemGridControl = $form.find('div[data-name="POReturnItemGrid"]');
                    $pOReturnItemGridControl.data('ondatabind', function (request) {
                        request.uniqueids = {
                            ContractId: contractId,
                            PurchaseOrderId: ''
                        }
                    })
                    FwBrowse.search($pOReturnItemGridControl);
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
                FwFormField.setValueByDataField($form, 'Date', currentDate);
                FwFormField.setValueByDataField($form, 'Time', currentTime);
            }, null, $form);
        });
        // Select None
        $form.find('.selectnone').on('click', e => {
            let request: any = {}, quantity;
            const $returnItemsGridControl = $form.find('div[data-name="POReturnItemGrid"]');
            const contractId = FwFormField.getValueByDataField($form, 'ContractId');
            const purchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            const purchaseOrderItemIdColumn: any = $form.find('.POReturnItemGrid [data-browsedatafield="PurchaseOrderItemId"]');
            const QuantityColumn: any = $form.find('.POReturnItemGrid [data-browsedatafield="Quantity"]');

            request.ContractId = contractId;
            request.PurchaseOrderId = purchaseOrderId;
            FwAppData.apiMethod(true, 'POST', `api/v1/purchaseorderreturnitem/selectnone`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.search($returnItemsGridControl);
            }, function onError(response) {
                FwFunc.showError(response);
            }, $form);
        });
        // Select All
        $form.find('.selectall').on('click', e => {
            let request: any = {};
            const $returnItemsGridControl = $form.find('div[data-name="POReturnItemGrid"]');
            const contractId = FwFormField.getValueByDataField($form, 'ContractId');
            const purchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');

            request.ContractId = contractId;
            request.PurchaseOrderId = purchaseOrderId;
            FwAppData.apiMethod(true, 'POST', `api/v1/purchaseorderreturnitem/selectall`, request, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.search($returnItemsGridControl);
            }, function onError(response) {
                FwFunc.showError(response);
                }, $form, contractId);
        });
        //Bar Code / Serial No. Input field
        $form.find('[data-datafield="BarCode"] input').on('keydown', e => {
            if (e.which === 13) {
                $form.find('.errormsg').html('');
                let request: any = {};
                const $returnItemsGridControl = $form.find('div[data-name="POReturnItemGrid"]');
                const $returnBarCodeGridControl = $form.find('div[data-name="POReturnBarCodeGrid"]');
                request = {
                    ContractId: FwFormField.getValueByDataField($form, 'ContractId')
                    , PurchaseOrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId')
                    , BarCode: FwFormField.getValueByDataField($form, 'BarCode')
                }
                FwAppData.apiMethod(true, 'POST', `api/v1/purchaseorderreturnitem/returnitems`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    if (response.success === 'true') {
                        FwBrowse.search($returnBarCodeGridControl);
                        FwBrowse.search($returnItemsGridControl);
                    } else {
                        $form.find('.errormsg').html(response.msg);
                    }

                    $form.find('[data-datafield="BarCode"] input').select();
                }, null, $form);
            }
           
        });
    };
    //----------------------------------------------------------------------------------------------
    beforeValidate($browse, $grid, request) {
        const validationName = request.module;
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));

        switch (validationName) {
            case 'PurchaseOrderValidation':
                request.miscfields = {
                    ReturnToVendor: true,
                    ReturningWarehouseId: warehouse.warehouseid,
                };
                break;
        };
    };
    //----------------------------------------------------------------------------------------------
};
var ReturnToVendorController = new ReturnToVendor();