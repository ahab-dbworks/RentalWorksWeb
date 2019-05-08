routes.push({ pattern: /^module\/vendorinvoice$/, action: function (match: RegExpExecArray) { return VendorInvoiceController.getModuleScreen(); } });
//----------------------------------------------------------------------------------------------
class VendorInvoice {
    Module: string = 'VendorInvoice';
    apiurl: string = 'api/v1/vendorinvoice';
    caption: string = Constants.Modules.Home.VendorInvoice.caption;
	nav: string = Constants.Modules.Home.VendorInvoice.nav;
	id: string = Constants.Modules.Home.VendorInvoice.id;
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: any) {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};
        const $browse = this.openBrowse();
        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);

            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };
        return screen;
    };
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        const self = this;
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = self.ActiveViewFields;
        });

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems($menuObject) {
        const $all: JQuery = FwMenu.generateDropDownViewBtn('All', true, "ALL");
        const $new: JQuery = FwMenu.generateDropDownViewBtn('New', true, "NEW");
        const $approved: JQuery = FwMenu.generateDropDownViewBtn('Approved', false, "APPROVED");
        const $processed: JQuery = FwMenu.generateDropDownViewBtn('Processed', false, "PROCESSED");
        const $closed: JQuery = FwMenu.generateDropDownViewBtn('Closed', false, "CLOSED");

        let viewSubitems: Array<JQuery> = [];
        viewSubitems.push($all, $new, $approved, $processed, $closed);
        FwMenu.addViewBtn($menuObject, 'View', viewSubitems, true, "Status");

        //Location Filter
        const location = JSON.parse(sessionStorage.getItem('location'));
        const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
        const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);

        if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
            this.ActiveViewFields.LocationId = [location.locationid];
        }

        const viewLocation: Array<JQuery> = [];
        viewLocation.push($userLocation, $allLocations);
        FwMenu.addViewBtn($menuObject, 'Location', viewLocation, true, "LocationId");

        return $menuObject;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode, parentModuleInfo?: any) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            const today = FwFunc.getDate();
            FwFormField.setValueByDataField($form, 'InvoiceDate', today);
            $form.find('.continue').show();
        }

        const location = JSON.parse(sessionStorage.getItem('location'));
        FwFormField.setValueByDataField($form, 'LocationId', location.locationid, location.location);

        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any): JQuery {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="VendorInvoiceId"] input').val(uniqueids.VendorInvoiceId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    saveForm($form: JQuery, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    };
    //----------------------------------------------------------------------------------------------
    renderGrids($form: JQuery): void {
        // ----------
        const $vendorInvoiceItemGrid: JQuery = $form.find('div[data-grid="VendorInvoiceItemGrid"]');
        const $vendorInvoiceItemGridControl = FwBrowse.loadGridFromTemplate('VendorInvoiceItemGrid');
        $vendorInvoiceItemGrid.empty().append($vendorInvoiceItemGridControl);
        $vendorInvoiceItemGridControl.data('ondatabind', request => {
            request.uniqueids = {
                VendorInvoiceId: FwFormField.getValueByDataField($form, 'VendorInvoiceId')
            }
            request.totalfields = ["LineTotal", "Tax", "LineTotalWithTax"]

        })
        FwBrowse.addEventHandler($vendorInvoiceItemGridControl, 'afterdatabindcallback', ($vendorInvoiceItemGridControl, response) => {
            FwFormField.setValue($form, 'div[data-totalfield="InvoiceItemGrossTotal"]', response.Totals.LineTotal);
            FwFormField.setValue($form, 'div[data-totalfield="InvoiceItemTax"]', response.Totals.Tax);
            FwFormField.setValue($form, 'div[data-totalfield="InvoiceItemTotal"]', response.Totals.LineTotalWithTax);
        })
        FwBrowse.init($vendorInvoiceItemGridControl);
        FwBrowse.renderRuntimeHtml($vendorInvoiceItemGridControl);
        // ----------
        const $glDistributionGrid = $form.find('div[data-grid="GlDistributionGrid"]');
        const $glDistributionGridControl = FwBrowse.loadGridFromTemplate('GlDistributionGrid');
        $glDistributionGrid.empty().append($glDistributionGridControl);
        $glDistributionGridControl.data('ondatabind', request => {
            request.uniqueids = {
                VendorInvoiceId: FwFormField.getValueByDataField($form, 'VendorInvoiceId')
            };
        });
        FwBrowse.addEventHandler($glDistributionGridControl, 'afterdatabindcallback', () => {
        })
        FwBrowse.init($glDistributionGridControl);
        FwBrowse.renderRuntimeHtml($glDistributionGridControl);
        // ----------
        const $vendorInvoicePaymentGrid = $form.find('div[data-grid="VendorInvoicePaymentGrid"]');
        const $vendorInvoicePaymentGridControl = FwBrowse.loadGridFromTemplate('VendorInvoicePaymentGrid');
        $vendorInvoicePaymentGrid.empty().append($vendorInvoicePaymentGridControl);
        $vendorInvoicePaymentGridControl.data('ondatabind', request => {
            request.uniqueids = {
                VendorInvoiceId: FwFormField.getValueByDataField($form, 'VendorInvoiceId')
            };
        });
        FwBrowse.init($vendorInvoicePaymentGridControl);
        FwBrowse.renderRuntimeHtml($vendorInvoicePaymentGridControl);
        // ----------
        const $vendorInvoiceNoteGrid = $form.find('div[data-grid="VendorInvoiceNoteGrid"]');
        const $vendorInvoiceNoteGridControl = FwBrowse.loadGridFromTemplate('VendorInvoiceNoteGrid');
        $vendorInvoiceNoteGrid.empty().append($vendorInvoiceNoteGridControl);
        $vendorInvoiceNoteGridControl.data('ondatabind', request => {
            request.uniqueids = {
                UniqueId1: FwFormField.getValueByDataField($form, 'VendorInvoiceId')
            };
        });
        FwBrowse.init($vendorInvoiceNoteGridControl);
        FwBrowse.renderRuntimeHtml($vendorInvoiceNoteGridControl);
        // ----------
        const $vendorInvoiceHistoryGrid = $form.find('div[data-grid="VendorInvoiceStatusHistoryGrid"]');
        const $vendorInvoiceHistoryGridControl = FwBrowse.loadGridFromTemplate('VendorInvoiceStatusHistoryGrid');
        $vendorInvoiceHistoryGrid.empty().append($vendorInvoiceHistoryGridControl);
        $vendorInvoiceHistoryGridControl.data('ondatabind', request => {
            request.uniqueids = {
                VendorInvoiceId: FwFormField.getValueByDataField($form, 'VendorInvoiceId')
            };
        });
        FwBrowse.init($vendorInvoiceHistoryGridControl);
        FwBrowse.renderRuntimeHtml($vendorInvoiceHistoryGridControl);
        // ----------
        const $exportBatchGrid = $form.find('div[data-grid="VendorInvoiceExportBatchGrid"]');
        const $exportBatchGridControl = FwBrowse.loadGridFromTemplate('VendorInvoiceExportBatchGrid');
        $exportBatchGrid.empty().append($exportBatchGridControl);
        $exportBatchGridControl.data('ondatabind', request => {
            request.uniqueids = {
                VendorInvoiceId: FwFormField.getValueByDataField($form, 'VendorInvoiceId')
            };
        });
        FwBrowse.init($exportBatchGridControl);
        FwBrowse.renderRuntimeHtml($exportBatchGridControl);
        // ----------
        const $additionalItemsGrid = $form.find('div[data-grid="AdditionalItemsGrid"]');
        const $additionalItemsGridControl = FwBrowse.loadGridFromTemplate('AdditionalItemsGrid');
        $additionalItemsGrid.empty().append($additionalItemsGridControl);
        $additionalItemsGridControl.data('ondatabind', request => {
            request.uniqueids = {
                VendorInvoiceId: FwFormField.getValueByDataField($form, 'VendorInvoiceId')
                , PurchaseOrderId: ""
            };
            request.totalfields = ["LineTotal", "Tax", "LineTotalWithTax"];
        });
        FwBrowse.addEventHandler($additionalItemsGridControl, 'afterdatabindcallback', ($additionalItemsGridControl, response) => {
            FwFormField.setValue($form, 'div[data-totalfield="AdditionalItemGrossTotal"]', response.Totals.LineTotal);
            FwFormField.setValue($form, 'div[data-totalfield="AdditionalItemTax"]', response.Totals.Tax);
            FwFormField.setValue($form, 'div[data-totalfield="AdditionalItemTotal"]', response.Totals.LineTotalWithTax);
        })
        $additionalItemsGridControl.data('beforesave', request => {
            request.VendorInvoiceId = FwFormField.getValueByDataField($form, 'VendorInvoiceId');
        });
        FwBrowse.init($additionalItemsGridControl);
        FwBrowse.renderRuntimeHtml($additionalItemsGridControl);
        // ----------
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form: JQuery) {
        //Disables editing when STATUS is CLOSED or PROCESSED 
        let status = FwFormField.getValueByDataField($form, 'Status');
        if ((status === 'CLOSED') || (status === 'PROCESSED')) {
            FwModule.setFormReadOnly($form);
        }

        FwFormField.disable($form.find('[data-datafield="PurchaseOrderId"]'));
        const $vendorInvoiceItemGridControl = $form.find('[data-name="VendorInvoiceItemGrid"]');
        FwBrowse.search($vendorInvoiceItemGridControl);

        const $glDistributionGridControl = $form.find('[data-name="GlDistributionGrid"]');
        FwBrowse.search($glDistributionGridControl);

        const $vendorInvoicePaymentGridControl = $form.find('[data-name="VendorInvoicePaymentGrid"]');
        FwBrowse.search($vendorInvoicePaymentGridControl);

        const $vendorInvoiceNoteGridControl = $form.find('[data-name="VendorInvoiceNoteGrid"]');
        FwBrowse.search($vendorInvoiceNoteGridControl);

        const $vendorInvoiceHistoryGridControl = $form.find('[data-name="VendorInvoiceStatusHistoryGrid"]');
        FwBrowse.search($vendorInvoiceHistoryGridControl);

        const $exportBatchGridControl = $form.find('[data-name="VendorInvoiceExportBatchGrid"]');
        FwBrowse.search($exportBatchGridControl);

        const $additionalItemsGridControl = $form.find('[data-name="AdditionalItemsGrid"]');
        FwBrowse.search($additionalItemsGridControl);
    };
    //----------------------------------------------------------------------------------------------
    afterSave($form: JQuery) {
        $form.find('.continue').hide();
    };
    //----------------------------------------------------------------------------------------------
    getVendorInvoiceItemGridTotals($form: JQuery): void {
        //FwAppData.apiMethod(true, 'GET', `api/v1/`, null, FwServices.defaultTimeout, function onSuccess(response) {
        //    $form.find(`div[data-totalfield="SubTotal"] input`).val(response.SubTotal);
        //    $form.find(`div[data-totalfield="Discount"] input`).val(response.Discount);
        //    $form.find(`div[data-totalfield="Tax"] input`).val(response.Tax);
        //    $form.find(`div[data-totalfield="GrossTotal"] input`).val(response.GrossTotal);
        //    $form.find(`div[data-totalfield="Total"] input`).val(response.Total);
        //}, function onError(response) {
        //    FwFunc.showError(response);
        //}, $form);
    };
    //----------------------------------------------------------------------------------------------
    events($form: JQuery) {
        //populate fields with PO info
        $form.on('change', '[data-datafield="PurchaseOrderId"] input', e => {
            let purchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            FwAppData.apiMethod(true, 'GET', `api/v1/purchaseorder/${purchaseOrderId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                FwFormField.setValueByDataField($form, 'VendorId', response.VendorId, response.Vendor);
                FwFormField.setValueByDataField($form, 'DepartmentId', response.DepartmentId, response.Department);
                FwFormField.setValueByDataField($form, 'WarehouseId', response.WarehouseId, response.Warehouse);
                FwFormField.setValueByDataField($form, 'PaymentTermsId', response.PaymentTermsId, response.PaymentTerms);
                FwFormField.setValueByDataField($form, 'OrderDescription', response.Description);
                FwFormField.setValueByDataField($form, 'PurchaseOrderBillingCycleId', response.BillingCycleId, response.BillingCycle);
                FwFormField.setValueByDataField($form, 'PurchaseOrderDate', response.PurchaseOrderDate);
                FwFormField.setValueByDataField($form, 'PurchaseOrderEstimatedStartDate', response.EstimatedStartDate);
                FwFormField.setValueByDataField($form, 'PurchaseOrderEstimatedStopDate', response.EstimatedStopDate);

                //add days to date to get invoice due date
                let invoiceDate = FwFormField.getValueByDataField($form, 'InvoiceDate');
                if (response.PaymentTermsDueInDays != 0) {
                    invoiceDate = new Date(invoiceDate);
                    let dueDate = moment(invoiceDate).add(response.PaymentTermsDueInDays, 'days').calendar();
                    FwFormField.setValueByDataField($form, 'InvoiceDueDate', dueDate);
                } else {
                    FwFormField.setValueByDataField($form, 'InvoiceDueDate', invoiceDate);
                }
            }, null, $form);

            if ($form.attr('data-mode') === "NEW") {
                FwAppData.apiMethod(true, 'GET', `api/v1/purchaseorder/nextvendorinvoicedefaultdates/${purchaseOrderId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                    let startDate = moment(response.BillingStartDate).format('MM-DD-YYYY');
                    let endDate = moment(response.BillingEndDate).format('MM-DD-YYYY');
                    FwFormField.setValueByDataField($form, 'BillingStartDate', startDate);
                    FwFormField.setValueByDataField($form, 'BillingEndDate', endDate);
                }, null, $form);
            }
        });

        $form.find('[data-datafield="PaymentTermsId"]').data('onchange', $tr => {
            let invoiceDate = FwFormField.getValueByDataField($form, 'InvoiceDate');
            invoiceDate = new Date(invoiceDate);
            let dueInDays = $tr.find('[data-browsedatafield="DueInDays"]').attr('data-originalvalue');
            let dueDate = moment(invoiceDate).add(dueInDays, 'days').calendar();
            FwFormField.setValueByDataField($form, 'InvoiceDueDate', dueDate);
        });

        $form.on('click', '.continue', e => {
            let isValid = FwModule.validateForm($form);
            if (isValid) {
                $form.find('.btn[data-type="SaveMenuBarButton"]').click();
            };
        });

        //Populate tax info fields with validation
        $form.find('div[data-datafield="TaxOptionId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="RentalTaxRate1"]', $tr.find('.field[data-browsedatafield="RentalTaxRate1"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="SalesTaxRate1"]', $tr.find('.field[data-browsedatafield="SalesTaxRate1"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="LaborTaxRate1"]', $tr.find('.field[data-browsedatafield="LaborTaxRate1"]').attr('data-originalvalue'));
        });
    };
};
//----------------------------------------------------------------------------------------------
//form approve
FwApplicationTree.clickEvents[Constants.Modules.Home.VendorInvoice.form.menuItems.Approve.id] = function (event) {
    var $form, vendorInvoiceId;
    $form = jQuery(this).closest('.fwform');
    vendorInvoiceId = FwFormField.getValueByDataField($form, 'VendorInvoiceId');
    FwAppData.apiMethod(true, 'POST', `api/v1/vendorinvoice/toggleapproved/${vendorInvoiceId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
        if (response.success === true) {
            FwModule.refreshForm($form, VendorInvoiceController);
        } else {
            FwNotification.renderNotification('WARNING', response.msg);
        }
    }, null, $form);
};
//----------------------------------------------------------------------------------------------
//form unapprove
FwApplicationTree.clickEvents[Constants.Modules.Home.VendorInvoice.form.menuItems.Unapprove.id] = function (event) {
    var $form, vendorInvoiceId;
    $form = jQuery(this).closest('.fwform');
    vendorInvoiceId = FwFormField.getValueByDataField($form, 'VendorInvoiceId');
    FwAppData.apiMethod(true, 'POST', `api/v1/vendorinvoice/toggleapproved/${vendorInvoiceId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
        if (response.success === true) {
            FwModule.refreshForm($form, VendorInvoiceController);
        } else {
            FwNotification.renderNotification('WARNING', response.msg);
        }
    }, null, $form);
};
//----------------------------------------------------------------------------------------------
//browse approve
FwApplicationTree.clickEvents[Constants.Modules.Home.VendorInvoice.browse.menuItems.Approve.id] = function (event) {
    try {
        let $browse = jQuery(this).closest('.fwbrowse');
        let vendorInvoiceId = $browse.find('.selected [data-browsedatafield="VendorInvoiceId"]').attr('data-originalvalue');
        if (typeof vendorInvoiceId !== 'undefined') {
            FwAppData.apiMethod(true, 'POST', `api/v1/vendorinvoice/toggleapproved/${vendorInvoiceId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                if (response.success === true) {
                    FwBrowse.search($browse);
                } else {
                    FwNotification.renderNotification('WARNING', response.msg);
                }
            }, null, $browse);
        } else {
            FwNotification.renderNotification('WARNING', 'No Vendor Invoice Selected');
        }
    } catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
//browse unapprove
FwApplicationTree.clickEvents[Constants.Modules.Home.VendorInvoice.form.menuItems.Unapprove.id] = function (event) {
    try {
        let $browse = jQuery(this).closest('.fwbrowse');
        let vendorInvoiceId = $browse.find('.selected [data-browsedatafield="VendorInvoiceId"]').attr('data-originalvalue');
        if (typeof vendorInvoiceId !== 'undefined') {
            FwAppData.apiMethod(true, 'POST', `api/v1/vendorinvoice/toggleapproved/${vendorInvoiceId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                if (response.success === true) {
                    FwBrowse.search($browse);
                } else {
                    FwNotification.renderNotification('WARNING', response.msg);
                }
            }, null, $browse);
        } else {
            FwNotification.renderNotification('WARNING', 'No Vendor Invoice Selected');
        }
    } catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
var VendorInvoiceController = new VendorInvoice();