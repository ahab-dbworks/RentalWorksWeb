routes.push({ pattern: /^module\/subworksheet$/, action: function (match: RegExpExecArray) { return SubWorksheetController.getModuleScreen(); } });

class SubWorksheet {
    Module: string = 'SubWorksheet';
    OrderId: string;

    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('EDIT');

        screen.load = function () {
            FwModule.openModuleTab($form, 'Sub Worksheet', false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        let $form, worksheetRequest: any = {}, createNew, modifyExisting;
        this.OrderId = parentmoduleinfo.OrderId;

        $form = jQuery(jQuery('#tmpl-modules-SubWorksheetForm').html());
        $form = FwModule.openForm($form, mode);

        $form.off('change keyup', '.fwformfield[data-isuniqueid!="true"][data-enabled="true"][data-datafield!=""]');

        createNew = $form.find('div[data-datafield="CreateNew"] input')
        modifyExisting = $form.find('div[data-datafield="ModifyExisting"] input');

        worksheetRequest.OrderId = parentmoduleinfo.OrderId;
        worksheetRequest.RecType = 'R';

        createNew.on('change', (e) => {
            if (jQuery(e.currentTarget).prop('checked')) {
                modifyExisting.prop('checked', false);
            } else {
                modifyExisting.prop('checked', true);

            }
        });

        modifyExisting.on('change', (e) => {
            if (jQuery(e.currentTarget).prop('checked')) {
                createNew.prop('checked', false);
            } else {
                createNew.prop('checked', true);
            }
        });

        $form.find('.openworksheet').on('click', function (e) {
            try {
                FwAppData.apiMethod(true, 'POST', "api/v1/order/startpoworksheetsession", worksheetRequest, FwServices.defaultTimeout, function onSuccess(response) {
                    if (response.success) {
                        let gridUniqueIds: any = {
                            OrderId: parentmoduleinfo.OrderId,
                            SessionId: response.SessionId,
                            VendorId: FwFormField.getValueByDataField($form, 'VendorId'),
                            CurrencyId: FwFormField.getValueByDataField($form, 'CurrencyId'),
                            RateType: FwFormField.getValueByDataField($form, 'RateId'),
                            TotalType: 'W',
                            RecType: 'R',
                            FromDate: FwFormField.getValueByDataField($form, 'RentalFrom'),
                            ToDate: FwFormField.getValueByDataField($form, 'RentalTo'),
                            PurchaseOrderId: FwFormField.getValueByDataField($form, 'POId')
                        };

                        var $subPurchaseOrderItemGridControl: any;
                        $subPurchaseOrderItemGridControl = $form.find('[data-name="SubPurchaseOrderItemGrid"]');
                        $subPurchaseOrderItemGridControl.data('ondatabind', function (request) {
                            request.uniqueids = gridUniqueIds
                        })
                        FwBrowse.search($subPurchaseOrderItemGridControl);
                    } else {
                        $form.find('div.errormsg').html(`<div style="margin:0px 0px 0px 8px;"><span style="padding:0px 4px 0px 4px;font-size:22px;border-radius:2px;background-color:red;color:white;">${response.msg}</span></div>`);
                    }
                }, null, $form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
        
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadPOGrid($form, worksheetRequest) {
        try {
            FwAppData.apiMethod(true, 'POST', "api/v1/exchange/startpoworksheetsession", worksheetRequest, FwServices.defaultTimeout, function onSuccess(response) {
                if (response.success) {

                } else {

                }

            }, null, $form);

        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form) {
        var $subPurchaseOrderItemGrid;
        var $subPurchaseOrderItemGridControl;
        var self = this;
        $subPurchaseOrderItemGrid = $form.find('div[data-grid="SubPurchaseOrderItemGrid"]');
        $subPurchaseOrderItemGridControl = jQuery(jQuery('#tmpl-grids-SubPurchaseOrderItemGridBrowse').html());
        $subPurchaseOrderItemGrid.empty().append($subPurchaseOrderItemGridControl);
        $subPurchaseOrderItemGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: self.OrderId
            };
        });
        FwBrowse.init($subPurchaseOrderItemGridControl);
        FwBrowse.renderRuntimeHtml($subPurchaseOrderItemGridControl);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
    }
    //----------------------------------------------------------------------------------------------

}
var SubWorksheetController = new SubWorksheet();