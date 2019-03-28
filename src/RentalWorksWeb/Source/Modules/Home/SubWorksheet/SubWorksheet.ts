﻿routes.push({ pattern: /^module\/subworksheet$/, action: function (match: RegExpExecArray) { return SubWorksheetController.getModuleScreen(); } });

class SubWorksheet {
    Module: string = 'SubWorksheet';
    OrderId: string;
    SessionId: string;
    RecType: string;
    caption: string = 'Sub Worksheet';
    gridTotalFields: Array<string> = ["VendorWeeklyTotal", "VendorWeeklyDiscount", "VendorWeeklySubTotal", "VendorWeeklyTax", "VendorWeeklyExtended", "VendorMonthlyTotal", "VendorMonthlyDiscount", "VendorMonthlySubTotal", "VendorMonthlyTax", "VendorMonthlyExtended", "VendorPeriodTotal", "VendorPeriodDiscount", "VendorPeriodSubTotal", "VendorPeriodTax", "VendorPeriodExtended"];


    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $form = this.openForm('EDIT');

        screen.load = () =>  {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
            this.SessionId = '';
            this.OrderId = '';
        };
        screen.unload = function () { };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        this.OrderId = parentmoduleinfo.OrderId;
        this.RecType = parentmoduleinfo.RecType;
        $form.find('div[data-datafield="CreateNew"] input').prop('checked', true);
        $form.off('change keyup', '.fwformfield[data-isuniqueid!="true"][data-enabled="true"][data-datafield!=""]');

        FwFormField.setValueByDataField($form, 'RequiredDate', parentmoduleinfo.EstimatedStartDate);
        FwFormField.setValueByDataField($form, 'FromDate', parentmoduleinfo.EstimatedStartDate);
        FwFormField.setValueByDataField($form, 'ToDate', parentmoduleinfo.EstimatedStopDate);
        FwFormField.setValueByDataField($form, 'RequiredTime', parentmoduleinfo.EstimatedStartTime);

        this.events($form, parentmoduleinfo)
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    events($form: JQuery, parentmoduleinfo?): void {
        const createNew = $form.find('div[data-datafield="CreateNew"] input');
        const modifyExisting = $form.find('div[data-datafield="ModifyExisting"] input');
        const newPo = $form.find('.new');
        const existingPo = $form.find('.existing');
        // Create new checkbox
        createNew.on('change', e => {
            if (jQuery(e.currentTarget).prop('checked')) {
                modifyExisting.prop('checked', false);
                for (let i = 0; i < newPo.length; i++) {
                    FwFormField.enable(jQuery(newPo[i]));
                }
                FwFormField.disable(existingPo);
            } else {
                modifyExisting.prop('checked', true);
                for (let i = 0; i < newPo.length; i++) {
                    FwFormField.disable(jQuery(newPo[i]));
                }
                FwFormField.enable(existingPo);
            }
        });
        // Modify Existing checkbox
        modifyExisting.on('change', e => {
            if (jQuery(e.currentTarget).prop('checked')) {
                createNew.prop('checked', false);
                for (let i = 0; i < newPo.length; i++) {
                    FwFormField.disable(jQuery(newPo[i]));
                }
                FwFormField.enable(existingPo);
            } else {
                createNew.prop('checked', true);
                for (let i = 0; i < newPo.length; i++) {
                    FwFormField.enable(jQuery(newPo[i]));
                }
                FwFormField.disable(existingPo);
            }
        });
        // Open Worksheet button
        $form.find('.openworksheet').on('click', e => {
            if (FwFormField.getValueByDataField($form, 'CreateNew') === 'T') {
                this.createNewWorksheet($form, parentmoduleinfo);
            } else { 
                this.modifyWorksheet($form, parentmoduleinfo);
            }
        });

        $form.find('.create-modify-po').on('click', e => {
            try {
                const sessionRequest = { SessionId: this.SessionId };
                FwAppData.apiMethod(true, 'POST', "api/v1/order/completepoworksheetsession", sessionRequest, FwServices.defaultTimeout, response => {
                    if (response.success) {
                        try {
                            const $purchaseOrderForm = PurchaseOrderController.loadForm({
                                PurchaseOrderId: response.PurchaseOrderId
                            });
                            FwModule.openSubModuleTab($form, $purchaseOrderForm);

                            const fields = $form.find('.fwformfield');
                            for (let i = 0; i < fields.length; i++) {
                                FwFormField.setValue2(jQuery(fields[i]), '', '');
                            }
                            FwFormField.enable($form.find('.subworksheet'));
                            $form.find('div[data-grid="SubPurchaseOrderItemGrid"]').empty();
                            $form.find('.completeorder').hide();
                            $form.find('.openworksheet').show();
                            $form.find('div[data-datafield="CreateNew"] input').prop('checked', true);
                            this.SessionId = '';
                            this.OrderId = '';
                            FwFormField.setValueByDataField($form, 'RequiredDate', parentmoduleinfo.EstimatedStartDate);
                            FwFormField.setValueByDataField($form, 'FromDate', parentmoduleinfo.EstimatedStartDate);
                            FwFormField.setValueByDataField($form, 'ToDate', parentmoduleinfo.EstimatedStopDate);
                            FwFormField.setValueByDataField($form, 'RequiredTime', parentmoduleinfo.EstimatedStartTime);
                            FwFormField.disable($form.find('div[data-datafield="OfficePhone"]'));
                            FwFormField.disable($form.find('div[data-datafield="OfficeExtension"]'));
                            FwFormField.disable($form.find('div[data-datafield="PurchaseOrderId"]'));
                        }
                        catch (ex) {
                            FwFunc.showError(ex);
                        }
                    } else {
                    }
                }, null, $form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
            $form.attr('data-modified', 'false');
        })
        // Select All
        $form.find('.selectall').on('click', e => {
            try {
                const sessionRequest = { SessionId: this.SessionId };
                FwAppData.apiMethod(true, 'POST', "api/v1/subpurchaseorderitem/selectall", sessionRequest, FwServices.defaultTimeout, response => {
                    if (response.success) {
                        try {
                            const $subPurchaseOrderItemGridControl = $form.find('[data-name="SubPurchaseOrderItemGrid"]');
                            $subPurchaseOrderItemGridControl.data('ondatabind', request => {
                                request.uniqueids = {
                                    SessionId: this.SessionId
                                }
                            })
                            FwBrowse.search($subPurchaseOrderItemGridControl);
                        }
                        catch (ex) {
                            FwFunc.showError(ex);
                        }
                    } else {
                    }
                }, null, $form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
            $form.attr('data-modified', 'false');
        })
        // Select None
        $form.find('.selectnone').on('click', e => {
            try {
                const sessionRequest = { SessionId: this.SessionId };
                FwAppData.apiMethod(true, 'POST', "api/v1/subpurchaseorderitem/selectnone", sessionRequest, FwServices.defaultTimeout, response =>  {
                    if (response.success) {
                        try {
                            const $subPurchaseOrderItemGridControl = $form.find('[data-name="SubPurchaseOrderItemGrid"]');
                            $subPurchaseOrderItemGridControl.data('ondatabind', request =>  {
                                request.uniqueids = {
                                    SessionId: this.SessionId
                                }
                            })
                            FwBrowse.search($subPurchaseOrderItemGridControl);
                        }
                        catch (ex) {
                            FwFunc.showError(ex);
                        }
                    } else {
                    }
                }, null, $form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
            $form.attr('data-modified', 'false');
        })
        // Misc events
        $form.find('div[data-datafield="VendorId"]').data('onchange', function ($tr) {
            FwFormField.setValueByDataField($form, 'RateId', $tr.find('.field[data-browsedatafield="DefaultRate"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="DefaultRate"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'CurrencyId', $tr.find('.field[data-browsedatafield="DefaultCurrencyId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="DefaultCurrencyCode"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'BillingCycleId', $tr.find('.field[data-browsedatafield="BillingCycleId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="BillingCycle"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'ContactId', $tr.find('.field[data-browsedatafield="PrimaryContactId"]').attr('data-originalvalue'), $tr.find('.field[data-browsedatafield="PrimaryContact"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'OfficePhone', $tr.find('.field[data-browsedatafield="PrimaryContactPhone"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'OfficeExtension', $tr.find('.field[data-browsedatafield="PrimaryContactExtension"]').attr('data-originalvalue'));
        });

        $form.find('div[data-datafield="ContactId"]').data('onchange', function ($tr) {
            FwFormField.setValueByDataField($form, 'OfficePhone', $tr.find('.field[data-browsedatafield="OfficePhone"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'OfficeExtension', $tr.find('.field[data-browsedatafield="OfficeExtension"]').attr('data-originalvalue'));
        });

        // Totals radio group event to trigger refresh of totals
        $form.find(".totalType input").on('change', e => {
            const $subPurchaseOrderItemGridControl = $form.find('[data-name="SubPurchaseOrderItemGrid"]');
            FwBrowse.search($subPurchaseOrderItemGridControl);
        });
    }
    //----------------------------------------------------------------------------------------------
    createNewWorksheet($form: JQuery, parentmoduleinfo: any): void {
        try {
            const worksheetRequest: any = {
                OrderId: parentmoduleinfo.OrderId,
                RecType: parentmoduleinfo.RecType,
                VendorId: FwFormField.getValueByDataField($form, 'VendorId'),
                ContactId: FwFormField.getValueByDataField($form, 'ContactId'),
                RateType: FwFormField.getValueByDataField($form, 'RateId'),
                BillingCycleId: FwFormField.getValueByDataField($form, 'BillingCycleId'),
                RequiredTime: FwFormField.getValueByDataField($form, 'RequiredTime'),
                FromDate: FwFormField.getValueByDataField($form, 'FromDate'),
                ToDate: FwFormField.getValueByDataField($form, 'ToDate'),
                DeliveryId: '',
                AdjustContractDates: true,
            }

            if (FwFormField.getValueByDataField($form, 'ToDate') === '') {
                worksheetRequest.ToDate = undefined;
            }

            if (FwFormField.getValueByDataField($form, 'RequiredDate') !== '') {
                worksheetRequest.RequiredDate = FwFormField.getValueByDataField($form, 'RequiredDate')
            }

            FwAppData.apiMethod(true, 'POST', "api/v1/order/startcreatepoworksheetsession", worksheetRequest, FwServices.defaultTimeout, response => {
                if (response.success) {
                    this.SessionId = response.SessionId;
                    $form.find('.error-msg:not(.qty)').html('');
                    FwFormField.disable($form.find('.subworksheet'));
                    $form.find('.openworksheet').hide();
                    const gridUniqueIds: any = {
                        SessionId: response.SessionId
                    };
                    this.renderPOGrid($form);

                    const $subPurchaseOrderItemGridControl = $form.find('[data-name="SubPurchaseOrderItemGrid"]');

                    $subPurchaseOrderItemGridControl.data('ondatabind', request => {
                        request.uniqueids = gridUniqueIds;
                        request.totalfields = this.gridTotalFields;
                    })
                    FwBrowse.addEventHandler($subPurchaseOrderItemGridControl, 'afterdatabindcallback', ($subPurchaseOrderItemGridControl, response) => {
                        this.getSubPOItemGridTotals($form, response);
                    });
                    FwBrowse.search($subPurchaseOrderItemGridControl);

                    $form.find('.completeorder').show();
                    $form.find('.create-modify-po').text('Create Purchase Order');

                    FwFormField.getValueByDataField($form, 'RateId') === 'DAILY' ? $form.find('.daily').show() : $form.find('.daily').hide();
                } else {
                    $form.find('.error-msg:not(.qty)').html(`<div style="margin-left:5px;"><span>${response.msg}</span></div>`);
                }
            }, null, $form);

            $form.attr('data-modified', 'false');
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    modifyWorksheet($form: JQuery, parentmoduleinfo: any): void {
        try {
            const PurchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            if (PurchaseOrderId !== '') {
                    const worksheetRequest: any = {
                        OrderId: parentmoduleinfo.OrderId,
                        RecType: parentmoduleinfo.RecType,
                        PurchaseOrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                    }

                    FwAppData.apiMethod(true, 'POST', "api/v1/order/startmodifypoworksheetsession", worksheetRequest, FwServices.defaultTimeout, response => {
                        if (response.success) {
                            this.SessionId = response.SessionId;
                            $form.find('.error-msg:not(.qty)').html('');
                            FwFormField.disable($form.find('.subworksheet'));
                            $form.find('.openworksheet').hide();

                            // fill in form fields
                            FwFormField.setValueByDataField($form, 'RequiredDate', response.RequiredDate);
                            FwFormField.setValueByDataField($form, 'FromDate', response.FromDate);
                            FwFormField.setValueByDataField($form, 'ToDate', response.ToDate);
                            FwFormField.setValueByDataField($form, 'RequiredTime', response.RequiredTime);
                            FwFormField.setValueByDataField($form, 'VendorId', response.VendorId, response.Vendor);
                            FwFormField.setValueByDataField($form, 'BillingCycleId', response.BillingCycleId, response.BillingCycle);
                            FwFormField.setValueByDataField($form, 'ContactId', response.ContactId, response.Contact);
                            FwFormField.setValueByDataField($form, 'RateId', response.RateType);

                            const gridUniqueIds: any = { SessionId: response.SessionId };
                            this.renderPOGrid($form);

                            const $subPurchaseOrderItemGridControl = $form.find('[data-name="SubPurchaseOrderItemGrid"]');
                            $subPurchaseOrderItemGridControl.data('ondatabind', request => {
                                request.uniqueids = gridUniqueIds
                                request.totalfields = this.gridTotalFields;
                            })
                            FwBrowse.addEventHandler($subPurchaseOrderItemGridControl, 'afterdatabindcallback', ($subPurchaseOrderItemGridControl, response) => {
                                this.getSubPOItemGridTotals($form, response);
                            });
                            FwBrowse.search($subPurchaseOrderItemGridControl);
                   
                            $form.find('.completeorder').show();
                            $form.find('.create-modify-po').text('Update Purchase Order');
                            
                            FwFormField.getValueByDataField($form, 'RateId') === 'DAILY' ? $form.find('.daily').show() : $form.find('.daily').hide();
                        } else {
                            $form.find('.error-msg:not(.qty)').html(`<div style="margin-left:5px;"><span>${response.msg}</span></div>`);
                        }
                    }, null, $form);
                    $form.attr('data-modified', 'false');
            } else {
                FwNotification.renderNotification('WARNING', 'Select a PO Number first.')
            }
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    getSubPOItemGridTotals($form: JQuery, response: any): void {
        try {
            let subTotal, discount, salesTax, grossTotal, total;

            const rateValue = $form.find(`.totalType input:checked`).val();
            switch (rateValue) {
                case 'W':
                    subTotal = response.Totals.VendorWeeklyExtended;
                    discount = response.Totals.VendorWeeklyDiscount;
                    salesTax = response.Totals.VendorWeeklyTax;
                    grossTotal = response.Totals.VendorWeeklySubTotal;
                    total = response.Totals.VendorWeeklyTotal;
                    break;
                case 'P':
                    subTotal = response.Totals.VendorPeriodExtended;
                    discount = response.Totals.VendorPeriodDiscount;
                    salesTax = response.Totals.VendorPeriodTax;
                    grossTotal = response.Totals.VendorPeriodSubTotal;
                    total = response.Totals.VendorPeriodTotal;
                    break;
                case 'M':
                    subTotal = response.Totals.VendorMonthlyExtended;
                    discount = response.Totals.VendorMonthlyDiscount;
                    salesTax = response.Totals.VendorMonthlyTax;
                    grossTotal = response.Totals.VendorMonthlySubTotal;
                    total = response.Totals.VendorMonthlyTotal;
                    break;
                default:
                    subTotal = response.Totals.VendorPeriodExtended;
                    discount = response.Totals.VendorPeriodDiscount;
                    salesTax = response.Totals.VendorPeriodTax;
                    grossTotal = response.Totals.VendorPeriodSubTotal;
                    total = response.Totals.VendorPeriodTotal;
            }

            FwFormField.setValue($form, 'div[data-totalfield="Total"]', total);
            FwFormField.setValue($form, 'div[data-totalfield="Tax"]', salesTax);
            FwFormField.setValue($form, 'div[data-totalfield="SubTotal"]', subTotal);
            FwFormField.setValue($form, 'div[data-totalfield="GrossTotal"]', grossTotal);
            FwFormField.setValue($form, 'div[data-totalfield="Discount"]', discount);
        } catch (ex) {
            FwFunc.showError(ex)
        }
    }
    //----------------------------------------------------------------------------------------------
    beforeValidateContact($browse, $grid, request) {
        const vendorId = jQuery($grid.find('[data-validationname="VendorValidation"] input')).val();
        request.uniqueIds = { CompanyId: vendorId }
    }
    //----------------------------------------------------------------------------------------------
    beforeValidateVendor($browse, $grid, request) {
        request.uniqueIds = {};
        switch (this.RecType) {
            case 'R':
                request.uniqueIds['SubRent'] = true;
                break;
            case 'S':
                request.uniqueIds['SubSale'] = true;
                break;
            case 'M':
                request.uniqueIds['SubMisc'] = true;
                break;
            case 'L':
                request.uniqueIds['SubLabor'] = true;
                break;
        }
    }
    //----------------------------------------------------------------------------------------------
    renderPOGrid($form: JQuery): void {
        const $subPurchaseOrderItemGrid = $form.find('div[data-grid="SubPurchaseOrderItemGrid"]');
        const $subPurchaseOrderItemGridControl = FwBrowse.loadGridFromTemplate('SubPurchaseOrderItemGrid');
        $subPurchaseOrderItemGrid.empty().append($subPurchaseOrderItemGridControl);
        $subPurchaseOrderItemGridControl.data('ondatabind', request => {
            request.uniqueids = { OrderId: this.OrderId };
            request.totalfields = this.gridTotalFields;
        });
        $subPurchaseOrderItemGridControl.data('beforesave', request => {
            request.SessionId = this.SessionId;
            request.RecType = 'R';
        });
        FwBrowse.init($subPurchaseOrderItemGridControl);
        FwBrowse.renderRuntimeHtml($subPurchaseOrderItemGridControl);
    }
    //----------------------------------------------------------------------------------------------
}
var SubWorksheetController = new SubWorksheet();