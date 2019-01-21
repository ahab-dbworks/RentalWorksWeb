routes.push({ pattern: /^module\/vendorinvoice$/, action: function (match: RegExpExecArray) { return VendorInvoiceController.getModuleScreen(); } });
//----------------------------------------------------------------------------------------------
class VendorInvoice {
    Module: string = 'VendorInvoice';
    apiurl: string = 'api/v1/vendorinvoice';
    caption: string = 'Vendor Invoice';
    nav: string = 'module/vendorinvoice';
    id: string = '854B3C59-7040-47C4-A8A3-8A336FC970FE';
    ActiveView: string = 'ALL';
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: any) {
        var self = this;
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};
        var $browse = this.openBrowse();
        screen.load = function () {
            FwModule.openModuleTab($browse, self.caption, false, 'BROWSE', true);

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
        var self = this;
        var $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        var location = JSON.parse(sessionStorage.getItem('location'));
        self.ActiveView = 'LocationId=' + location.locationid;

        $browse.data('ondatabind', function (request) {
            request.activeview = self.ActiveView;
        });

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems($menuObject) {
        var self = this;
        //Location Filter
        var location = JSON.parse(sessionStorage.getItem('location'));
        var $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false);
        var $userLocation = FwMenu.generateDropDownViewBtn(location.location, true);
        $allLocations.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'LocationId=ALL';
            FwBrowse.search($browse);
        });
        $userLocation.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'LocationId=' + location.locationid;
            FwBrowse.search($browse);
        });
        var viewLocation = [];
        viewLocation.push($userLocation, $allLocations);
        FwMenu.addViewBtn($menuObject, 'Location', viewLocation);
        return $menuObject;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode, parentModuleInfo?: any) {
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        this.events($form);
        if (mode === 'NEW') {
            let today = FwFunc.getDate();
            FwFormField.setValueByDataField($form, 'InvoiceDate', today);
            $form.find('.continue').show();
        }

        let location = JSON.parse(sessionStorage.getItem('location'));
        FwFormField.setValueByDataField($form, 'LocationId', location.locationid, location.location);

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="VendorInvoiceId"] input').val(uniqueids.VendorInvoiceId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    };
    //----------------------------------------------------------------------------------------------
    renderGrids($form) {
        let $vendorInvoiceItemGrid: any,
            $vendorInvoiceItemGridControl: any;

        $vendorInvoiceItemGrid = $form.find('div[data-grid="VendorInvoiceItemGrid"]');
        $vendorInvoiceItemGridControl = jQuery(jQuery('#tmpl-grids-VendorInvoiceItemGridBrowse').html());
        $vendorInvoiceItemGrid.empty().append($vendorInvoiceItemGridControl);
        $vendorInvoiceItemGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                VendorInvoiceId: FwFormField.getValueByDataField($form, 'VendorInvoiceId')
            }
        })
        FwBrowse.init($vendorInvoiceItemGridControl);
        FwBrowse.renderRuntimeHtml($vendorInvoiceItemGridControl);
        //----------------------------------------------------------------------------------------------
        let $glDistributionGrid;
        let $glDistributionGridControl;
        $glDistributionGrid = $form.find('div[data-grid="GlDistributionGrid"]');
        $glDistributionGridControl = FwBrowse.loadGridFromTemplate('GlDistributionGrid');
        $glDistributionGrid.empty().append($glDistributionGridControl);
        $glDistributionGridControl.data('ondatabind', request => {
            request.uniqueids = {
                VendorInvoiceId: FwFormField.getValueByDataField($form, 'VendorInvoiceId')
            };
        });
        FwBrowse.init($glDistributionGridControl);
        FwBrowse.renderRuntimeHtml($glDistributionGridControl);
        //----------------------------------------------------------------------------------------------
        let $vendorInvoicePaymentGrid;
        let $vendorInvoicePaymentGridControl;
        $vendorInvoicePaymentGrid = $form.find('div[data-grid="VendorInvoicePaymentGrid"]');
        $vendorInvoicePaymentGridControl = FwBrowse.loadGridFromTemplate('VendorInvoicePaymentGrid');
        $vendorInvoicePaymentGrid.empty().append($vendorInvoicePaymentGridControl);
        $vendorInvoicePaymentGridControl.data('ondatabind', request => {
            request.uniqueids = {
                VendorInvoiceId: FwFormField.getValueByDataField($form, 'VendorInvoiceId')
            };
        });
        FwBrowse.init($vendorInvoicePaymentGridControl);
        FwBrowse.renderRuntimeHtml($vendorInvoicePaymentGridControl);
        //----------------------------------------------------------------------------------------------
        let $vendorInvoiceNoteGrid;
        let $vendorInvoiceNoteGridControl;
        $vendorInvoiceNoteGrid = $form.find('div[data-grid="VendorInvoiceNoteGrid"]');
        $vendorInvoiceNoteGridControl = FwBrowse.loadGridFromTemplate('VendorInvoiceNoteGrid');
        $vendorInvoiceNoteGrid.empty().append($vendorInvoiceNoteGridControl);
        $vendorInvoiceNoteGridControl.data('ondatabind', request => {
            request.uniqueids = {
                UniqueId1: FwFormField.getValueByDataField($form, 'VendorInvoiceId')
            };
        });
        FwBrowse.init($vendorInvoiceNoteGridControl);
        FwBrowse.renderRuntimeHtml($vendorInvoiceNoteGridControl);
        //----------------------------------------------------------------------------------------------
        let $vendorInvoiceHistoryGrid;
        let $vendorInvoiceHistoryGridControl;
        $vendorInvoiceHistoryGrid = $form.find('div[data-grid="VendorInvoiceStatusHistoryGrid"]');
        $vendorInvoiceHistoryGridControl = FwBrowse.loadGridFromTemplate('VendorInvoiceStatusHistoryGrid');
        $vendorInvoiceHistoryGrid.empty().append($vendorInvoiceHistoryGridControl);
        $vendorInvoiceHistoryGridControl.data('ondatabind', request => {
            request.uniqueids = {
                VendorInvoiceId: FwFormField.getValueByDataField($form, 'VendorInvoiceId')
            };
        });
        FwBrowse.init($vendorInvoiceHistoryGridControl);
        FwBrowse.renderRuntimeHtml($vendorInvoiceHistoryGridControl);
        //----------------------------------------------------------------------------------------------

    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
        //Disables editing when STATUS is CLOSED or PROCESSED 
        let status = FwFormField.getValueByDataField($form, 'Status');
        if ((status === 'CLOSED') || (status === 'PROCESSED')) {
            FwModule.setFormReadOnly($form);
        }

        FwFormField.disable($form.find('[data-datafield="PurchaseOrderId"]'));
        let $vendorInvoiceItemGridControl = $form.find('[data-name="VendorInvoiceItemGrid"]');
        FwBrowse.search($vendorInvoiceItemGridControl);

        let $glDistributionGridControl = $form.find('[data-name="GlDistributionGrid"]');
        FwBrowse.search($glDistributionGridControl);

        let $vendorInvoicePaymentGridControl = $form.find('[data-name="VendorInvoicePaymentGrid"]');
        FwBrowse.search($vendorInvoicePaymentGridControl);

        let $vendorInvoiceNoteGridControl = $form.find('[data-name="VendorInvoiceNoteGrid"]');
        FwBrowse.search($vendorInvoiceNoteGridControl);

        let $vendorInvoiceHistoryGridControl = $form.find('[data-name="VendorInvoiceStatusHistoryGrid"]');
        FwBrowse.search($vendorInvoiceHistoryGridControl);
    };
    //----------------------------------------------------------------------------------------------
    afterSave($form) {
        $form.find('.continue').hide();
    };
    //----------------------------------------------------------------------------------------------
    events($form) {
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
FwApplicationTree.clickEvents['{79ABAD41-19F1-42C1-A88B-41479DE13B3B}'] = function (event) {
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
FwApplicationTree.clickEvents['{FB248072-C14C-4EEC-8B99-5ED8E950CE8A}'] = function (event) {
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
FwApplicationTree.clickEvents['{4A8CEF38-F59F-4306-8A9B-9B43FF6D127D}'] = function (event) {
    let $browse;
    let vendorInvoiceId;
    $browse = jQuery(this).closest('.fwbrowse');
    try {
        vendorInvoiceId = $browse.find('.selected [data-browsedatafield="VendorInvoiceId"]').attr('data-originalvalue');
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
FwApplicationTree.clickEvents['{9378DA62-F7FD-4FD2-8310-3277BBD155BF}'] = function (event) {
    let $browse;
    let vendorInvoiceId;
    $browse = jQuery(this).closest('.fwbrowse');
    try {
        vendorInvoiceId = $browse.find('.selected [data-browsedatafield="VendorInvoiceId"]').attr('data-originalvalue');
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