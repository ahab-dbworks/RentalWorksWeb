routes.push({ pattern: /^module\/vendorinvoice$/, action: function (match: RegExpExecArray) { return VendorInvoiceController.getModuleScreen(); } });
//----------------------------------------------------------------------------------------------
class VendorInvoice {
    Module: string = 'VendorInvoice';
    apiurl: string = 'api/v1/vendorinvoice';
    caption: string = Constants.Modules.Billing.children.VendorInvoice.caption;
    nav: string = Constants.Modules.Billing.children.VendorInvoice.nav;
    id: string = Constants.Modules.Billing.children.VendorInvoice.id;
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        FwMenu.addBrowseMenuButtons(options);

        FwMenu.addSubMenuItem(options.$groupOptions, `Approve`, `qGQ28sAtqVz4`, (e: JQuery.ClickEvent) => {
            try {
                this.approveVendorInvoice(options.$browse, 'browse');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, `Unpprove`, `qGQ28sAtqVz4`, (e: JQuery.ClickEvent) => {
            try {
                this.browseUnapproveVendorInvoice(options.$browse);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        const $all: JQuery = FwMenu.generateDropDownViewBtn('All', true, "ALL");
        const $new: JQuery = FwMenu.generateDropDownViewBtn('New', true, "NEW");
        const $approved: JQuery = FwMenu.generateDropDownViewBtn('Approved', false, "APPROVED");
        const $processed: JQuery = FwMenu.generateDropDownViewBtn('Processed', false, "PROCESSED");
        const $closed: JQuery = FwMenu.generateDropDownViewBtn('Closed', false, "CLOSED");

        let viewSubitems: Array<JQuery> = [];
        viewSubitems.push($all, $new, $approved, $processed, $closed);
        FwMenu.addViewBtn(options.$menu, 'View', viewSubitems, true, "Status");

        //Location Filter
        const location = JSON.parse(sessionStorage.getItem('location'));
        const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
        const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);

        if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
            this.ActiveViewFields.LocationId = [location.locationid];
        }

        const viewLocation: Array<JQuery> = [];
        viewLocation.push($userLocation, $allLocations);
        FwMenu.addViewBtn(options.$menu, 'Location', viewLocation, true, "LocationId");
    }
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        FwMenu.addFormMenuButtons(options);

        FwMenu.addSubMenuItem(options.$groupOptions, `Approve`, `qGQ28sAtqVz4`, (e: JQuery.ClickEvent) => {
            try {
                this.approveVendorInvoice(options.$form, 'form');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, `Unpprove`, `qGQ28sAtqVz4`, (e: JQuery.ClickEvent) => {
            try {
                this.formUnapproveVendorInvoice(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: any) {
        const screen: any = {};
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

        try {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, function onSuccess(response) {
                for (let key in response) {
                    FwBrowse.addLegend($browse, key, response[key]);
                }
            }, function onError(response) {
                FwFunc.showError(response);
            }, $browse)
        } catch (ex) {
            FwFunc.showError(ex);
        }

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode, parentModuleInfo?: any) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            const today = FwFunc.getDate();
            FwFormField.setValueByDataField($form, 'InvoiceDate', today);

            const location = JSON.parse(sessionStorage.getItem('location'));
            FwFormField.setValueByDataField($form, 'LocationId', location.locationid, location.location);
            FwFormField.setValueByDataField($form, 'CurrencyId', location.defaultcurrencyid, location.defaultcurrencycode);
            $form.find('.continue').show();
        }


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
        //const $vendorInvoiceItemGrid: JQuery = $form.find('div[data-grid="VendorInvoiceItemGrid"]');
        //const $vendorInvoiceItemGridControl = FwBrowse.loadGridFromTemplate('VendorInvoiceItemGrid');
        //$vendorInvoiceItemGrid.empty().append($vendorInvoiceItemGridControl);
        //$vendorInvoiceItemGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        VendorInvoiceId: FwFormField.getValueByDataField($form, 'VendorInvoiceId')
        //        , PurchaseOrderId: FwFormField.getValueByDataField($form, 'PurchaseOrderId')
        //    }
        //    request.totalfields = ["LineTotal", "Tax", "LineTotalWithTax"]
        //
        //})
        //FwBrowse.addEventHandler($vendorInvoiceItemGridControl, 'afterdatabindcallback', ($vendorInvoiceItemGridControl, response) => {
        //    FwFormField.setValue($form, 'div[data-totalfield="InvoiceItemGrossTotal"]', response.Totals.LineTotal);
        //    FwFormField.setValue($form, 'div[data-totalfield="InvoiceItemTax"]', response.Totals.Tax);
        //    FwFormField.setValue($form, 'div[data-totalfield="InvoiceItemTotal"]', response.Totals.LineTotalWithTax);
        //})
        //FwBrowse.init($vendorInvoiceItemGridControl);
        //FwBrowse.renderRuntimeHtml($vendorInvoiceItemGridControl);
        FwBrowse.renderGrid({
            nameGrid: 'VendorInvoiceItemGrid',
            gridSecurityId: 'mEYOByOhi5yT0',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    VendorInvoiceId: FwFormField.getValueByDataField($form, 'VendorInvoiceId')
                };
                request.totalfields = ["LineTotal", "Tax1", "Tax2", "LineTotalWithTax"];
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                FwFormField.setValue($form, 'div[data-totalfield="InvoiceItemGrossTotal"]', dt.Totals.LineTotal);
                FwFormField.setValue($form, 'div[data-totalfield="InvoiceItemTax"]', dt.Totals.Tax1);
                FwFormField.setValue($form, 'div[data-totalfield="InvoiceItemTax2"]', dt.Totals.Tax2);
                FwFormField.setValue($form, 'div[data-totalfield="InvoiceItemTotal"]', dt.Totals.LineTotalWithTax);
            },
        });
        // ----------
        //const $glDistributionGrid = $form.find('div[data-grid="GlDistributionGrid"]');
        //const $glDistributionGridControl = FwBrowse.loadGridFromTemplate('GlDistributionGrid');
        //$glDistributionGrid.empty().append($glDistributionGridControl);
        //$glDistributionGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        VendorInvoiceId: FwFormField.getValueByDataField($form, 'VendorInvoiceId')
        //    };
        //});
        //FwBrowse.addEventHandler($glDistributionGridControl, 'afterdatabindcallback', () => {
        //})
        //FwBrowse.init($glDistributionGridControl);
        //FwBrowse.renderRuntimeHtml($glDistributionGridControl);
        FwBrowse.renderGrid({
            nameGrid: 'GlDistributionGrid',
            gridSecurityId: '5xgHiF8dduf',
            moduleSecurityId: this.id,
            getBaseApiUrl: () => `${this.apiurl}/gldistribution`,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasEdit = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    VendorInvoiceId: FwFormField.getValueByDataField($form, 'VendorInvoiceId')
                };
                request.totalfields = ["Debit", "Credit"];
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                //FwFormField.setValue2($form.find('.gldistribution-totals [data-totalfield="Debit"]'), dt.Totals.Debit);
                //FwFormField.setValue2($form.find('.gldistribution-totals [data-totalfield="Credit"]'), dt.Totals.Credit);
            },
        });
        // ----------
        //const $vendorInvoicePaymentGrid = $form.find('div[data-grid="VendorInvoicePaymentGrid"]');
        //const $vendorInvoicePaymentGridControl = FwBrowse.loadGridFromTemplate('VendorInvoicePaymentGrid');
        //$vendorInvoicePaymentGrid.empty().append($vendorInvoicePaymentGridControl);
        //$vendorInvoicePaymentGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        VendorInvoiceId: FwFormField.getValueByDataField($form, 'VendorInvoiceId')
        //    };
        //});
        //FwBrowse.init($vendorInvoicePaymentGridControl);
        //FwBrowse.renderRuntimeHtml($vendorInvoicePaymentGridControl);
        FwBrowse.renderGrid({
            nameGrid: 'VendorInvoicePaymentGrid',
            gridSecurityId: 'cD51xfgax4oY',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    VendorInvoiceId: FwFormField.getValueByDataField($form, 'VendorInvoiceId')
                };
            },
        });
        // ----------
        //const $vendorInvoiceNoteGrid = $form.find('div[data-grid="VendorInvoiceNoteGrid"]');
        //const $vendorInvoiceNoteGridControl = FwBrowse.loadGridFromTemplate('VendorInvoiceNoteGrid');
        //$vendorInvoiceNoteGrid.empty().append($vendorInvoiceNoteGridControl);
        //$vendorInvoiceNoteGridControl.data('ondatabind', request => {
        //request.uniqueids = {
        //UniqueId1: FwFormField.getValueByDataField($form, 'VendorInvoiceId')
        //};
        //});
        //FwBrowse.init($vendorInvoiceNoteGridControl);
        //FwBrowse.renderRuntimeHtml($vendorInvoiceNoteGridControl);
        FwBrowse.renderGrid({
            nameGrid: 'VendorInvoiceNoteGrid',
            gridSecurityId: '8YECGu7qFOty',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    UniqueId1: FwFormField.getValueByDataField($form, 'VendorInvoiceId')
                };
            },
            beforeSave: (request: any) => {
                request.UniqueId1 = FwFormField.getValueByDataField($form, 'VendorInvoiceId');
            },
        });
        // ----------
        //const $vendorInvoiceHistoryGrid = $form.find('div[data-grid="VendorInvoiceStatusHistoryGrid"]');
        //const $vendorInvoiceHistoryGridControl = FwBrowse.loadGridFromTemplate('VendorInvoiceStatusHistoryGrid');
        //$vendorInvoiceHistoryGrid.empty().append($vendorInvoiceHistoryGridControl);
        //$vendorInvoiceHistoryGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        VendorInvoiceId: FwFormField.getValueByDataField($form, 'VendorInvoiceId')
        //    };
        //});
        //FwBrowse.init($vendorInvoiceHistoryGridControl);
        //FwBrowse.renderRuntimeHtml($vendorInvoiceHistoryGridControl);
        FwBrowse.renderGrid({
            nameGrid: 'VendorInvoiceStatusHistoryGrid',
            gridSecurityId: 'laMVsOwWI4Wkj',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    VendorInvoiceId: FwFormField.getValueByDataField($form, 'VendorInvoiceId')
                };
            },
        });
        // ----------
        //const $exportBatchGrid = $form.find('div[data-grid="VendorInvoiceExportBatchGrid"]');
        //const $exportBatchGridControl = FwBrowse.loadGridFromTemplate('VendorInvoiceExportBatchGrid');
        //$exportBatchGrid.empty().append($exportBatchGridControl);
        //$exportBatchGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        VendorInvoiceId: FwFormField.getValueByDataField($form, 'VendorInvoiceId')
        //    };
        //});
        //FwBrowse.init($exportBatchGridControl);
        //FwBrowse.renderRuntimeHtml($exportBatchGridControl);
        FwBrowse.renderGrid({
            nameGrid: 'VendorInvoiceExportBatchGrid',
            gridSecurityId: 'QriRQnYpPbxn',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    VendorInvoiceId: FwFormField.getValueByDataField($form, 'VendorInvoiceId')
                };
            },
        });
        // ----------
        //const $additionalItemsGrid = $form.find('div[data-grid="AdditionalItemsGrid"]');
        //const $additionalItemsGridControl = FwBrowse.loadGridFromTemplate('AdditionalItemsGrid');
        //$additionalItemsGrid.empty().append($additionalItemsGridControl);
        //$additionalItemsGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        VendorInvoiceId: FwFormField.getValueByDataField($form, 'VendorInvoiceId')
        //        , PurchaseOrderId: ""
        //    };
        //    request.totalfields = ["LineTotal", "Tax", "LineTotalWithTax"];
        //});
        //FwBrowse.addEventHandler($additionalItemsGridControl, 'afterdatabindcallback', ($additionalItemsGridControl, response) => {
        //    FwFormField.setValue($form, 'div[data-totalfield="AdditionalItemGrossTotal"]', response.Totals.LineTotal);
        //    FwFormField.setValue($form, 'div[data-totalfield="AdditionalItemTax"]', response.Totals.Tax);
        //    FwFormField.setValue($form, 'div[data-totalfield="AdditionalItemTotal"]', response.Totals.LineTotalWithTax);
        //})
        //$additionalItemsGridControl.data('beforesave', request => {
        //    request.VendorInvoiceId = FwFormField.getValueByDataField($form, 'VendorInvoiceId');
        //});
        //FwBrowse.init($additionalItemsGridControl);
        //FwBrowse.renderRuntimeHtml($additionalItemsGridControl);
        FwBrowse.renderGrid({
            nameGrid: 'AdditionalItemsGrid',
            gridSecurityId: 'mEYOByOhi5yT0',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    VendorInvoiceId: FwFormField.getValueByDataField($form, 'VendorInvoiceId'),
                    PurchaseOrderId: "",
                };
                request.totalfields = ["LineTotal", "Tax1", "Tax2", "LineTotalWithTax"];
            },
            beforeSave: (request: any) => {
                request.VendorInvoiceId = FwFormField.getValueByDataField($form, 'VendorInvoiceId');
                request.PurchaseOrderId = "";
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                FwFormField.setValue($form, 'div[data-totalfield="AdditionalItemGrossTotal"]', dt.Totals.LineTotal);
                FwFormField.setValue($form, 'div[data-totalfield="AdditionalItemTax"]', dt.Totals.Tax1);
                FwFormField.setValue($form, 'div[data-totalfield="AdditionalItemTax2"]', dt.Totals.Tax2);
                FwFormField.setValue($form, 'div[data-totalfield="AdditionalItemTotal"]', dt.Totals.LineTotalWithTax);
            },
        });
        // ----------
    };
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'TaxOptionId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatetaxoption`);
                break;
        }
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: JQuery, response: any) {
        //Disables editing when STATUS is CLOSED or PROCESSED 
        let status = FwFormField.getValueByDataField($form, 'Status');
        if ((status === 'CLOSED') || (status === 'PROCESSED')) {
            FwModule.setFormReadOnly($form);
        }

        //Click Event on tabs to load grids/browses
        $form.find('.tabGridsLoaded[data-type="tab"]').removeClass('tabGridsLoaded');
        $form.on('click', '[data-type="tab"][data-enabled!="false"]', e => {
            const $tab = jQuery(e.currentTarget);
            const tabname = $tab.attr('id');
            const lastIndexOfTab = tabname.lastIndexOf('tab');  // for cases where "tab" is included in the name of the tab
            const tabpage = `${tabname.substring(0, lastIndexOfTab)}tabpage${tabname.substring(lastIndexOfTab + 3)}`;
            const $gridControls = $form.find(`#${tabpage} [data-type="Grid"]`);
            if (($tab.hasClass('tabGridsLoaded') === false) && $gridControls.length > 0) {
                for (let i = 0; i < $gridControls.length; i++) {
                    try {
                        const $gridcontrol = jQuery($gridControls[i]);
                        FwBrowse.search($gridcontrol);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                }
            }

            const $browseControls = $form.find(`#${tabpage} [data-type="Browse"]`);
            if (($tab.hasClass('tabGridsLoaded') === false) && $browseControls.length > 0) {
                for (let i = 0; i < $browseControls.length; i++) {
                    const $browseControl = jQuery($browseControls[i]);
                    FwBrowse.search($browseControl);
                }
            }
            $tab.addClass('tabGridsLoaded');
        });

        const $vendorInvoiceItemGrid = $form.find('[data-name="VendorInvoiceItemGrid"]');
        FwBrowse.search($vendorInvoiceItemGrid);

        this.applyTaxOptions($form, response);
        this.applyCurrencySymbolToTotalFields($form, response);
    };
    //----------------------------------------------------------------------------------------------
    applyCurrencySymbolToTotalFields($form: JQuery, response: any) {
        const $totalFields = $form.find('.rental-totals [data-type="money"]');

        $totalFields.each((index, element) => {
            let $fwformfield, currencySymbol;
            $fwformfield = jQuery(element);
            currencySymbol = response[$fwformfield.attr('data-currencysymbol')];
            if (typeof currencySymbol == 'undefined' || currencySymbol === '') {
                currencySymbol = '$';
            }

            $fwformfield.attr('data-currencysymboldisplay', currencySymbol);

            $fwformfield
                .find('.fwformfield-value')
                .inputmask('currency', {
                    prefix: currencySymbol + ' ',
                    placeholder: "0.00",
                    min: ((typeof $fwformfield.attr('data-minvalue') !== 'undefined') ? $fwformfield.attr('data-minvalue') : undefined),
                    max: ((typeof $fwformfield.attr('data-maxvalue') !== 'undefined') ? $fwformfield.attr('data-maxvalue') : undefined),
                    digits: ((typeof $fwformfield.attr('data-digits') !== 'undefined') ? $fwformfield.attr('data-digits') : 2),
                    radixPoint: '.',
                    groupSeparator: ','
                });
        });
    }
    //----------------------------------------------------------------------------------------------
    applyTaxOptions($form: JQuery, response: any) {
        const $taxFields = $form.find('[data-totalfield="InvoiceItemTax"], [data-totalfield="AdditionalItemTax"]');
        const tax1Name = response.Tax1Name;
        const tax2Name = response.Tax2Name;

        const updateCaption = ($fields, taxName, count) => {
            for (let i = 0; i < $fields.length; i++) {
                const $field = jQuery($fields[i]);
                const taxType = $field.attr('data-taxtype');
                if (typeof taxType != 'undefined') {
                    const taxRateName = taxType + 'TaxRate' + count;
                    const taxRatePercentage = response[taxRateName];
                    if (typeof taxRatePercentage == 'number') {
                        const caption = taxName + ` (${taxRatePercentage.toFixed(3) + '%'})`;
                        $field.find('.fwformfield-caption').text(caption);
                    }
                }
            }

            const $billingTabTaxFields = $form.find(`[data-datafield="RentalTaxRate${count}"], [data-datafield="SalesTaxRate${count}"], [data-datafield="LaborTaxRate${count}"]`);
            for (let i = 0; i < $billingTabTaxFields.length; i++) {
                const $field = jQuery($billingTabTaxFields[i]);
                const taxType = $field.attr('data-taxtype');
                if (typeof taxType != 'undefined') {
                    const newCaption = taxType + ' ' + taxName;
                    $field.find('.fwformfield-caption').text(newCaption);
                }
                $field.show();
            }
        }

        if (tax1Name != "") {
            updateCaption($taxFields, tax1Name, 1);
        }

        const $tax2Fields = $form.find('[data-totalfield="InvoiceItemTax2"], [data-totalfield="AdditionalItemTax2"]');
        if (tax2Name != "") {
            $tax2Fields.show();
            updateCaption($tax2Fields, tax2Name, 2);
        } else {
            $tax2Fields.hide();
            $form.find(`[data-datafield="RentalTaxRate2"], [data-datafield="SalesTaxRate2"], [data-datafield="LaborTaxRate2"]`).hide();
        }
    }
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
                FwFormField.setValueByDataField($form, 'CurrencyId', response.CurrencyId, response.CurrencyCode);

                //add days to date to get invoice due date
                let invoiceDate = FwFormField.getValueByDataField($form, 'InvoiceDate');
                if (response.PaymentTermsDueInDays != 0) {
                    invoiceDate = new Date(invoiceDate);
                    let dueDate = moment(invoiceDate).add(response.PaymentTermsDueInDays, 'days').calendar();
                    FwFormField.setValueByDataField($form, 'InvoiceDueDate', dueDate);
                } else {
                    FwFormField.setValueByDataField($form, 'InvoiceDueDate', invoiceDate);
                }

                VendorInvoiceController.applyCurrencySymbolToTotalFields($form, response);
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
    //----------------------------------------------------------------------------------------------
    approveVendorInvoice($element: JQuery, state: string) {
        var approve = ($formToBlock: JQuery, vendorInvoiceId: string, onApproveSuccess: (response: any) => void, onApproveFailure?: (response: any) => void, request?: any): void => {
            try {
                if ((vendorInvoiceId == null) || (vendorInvoiceId == '') || (typeof vendorInvoiceId === 'undefined')) {
                    FwNotification.renderNotification('WARNING', 'No Vendor Invoice Selected');
                }
                else {
                    if (request) {
                        request.VendorInvoiceId = vendorInvoiceId;
                    } else {
                        request = {
                            VendorInvoiceId: vendorInvoiceId,
                            PushChangesToPurchaseOrder: null,
                        }
                    }
                    FwAppData.apiMethod(true, 'POST', `api/v1/vendorinvoice/toggleapproved/`, request, FwServices.defaultTimeout, function onSuccess(response) {
                        if (response.success === true) {
                            if ((onApproveSuccess) && (typeof onApproveSuccess === 'function')) {
                                onApproveSuccess(response);
                            }
                        } else if (response.success === false) {
                            const promptToUpdate = response.PromptToUpdatePurchaseOrder;
                            if (promptToUpdate) {
                                const $confirmation = FwConfirmation.renderConfirmation('Update Vendor Invoice', '');
                                const $promptmsg = response.msg;
                                FwConfirmation.addControls($confirmation, `<div style="text-align:center;"></div><div style="margin:10px 0 0 0;text-align:center;">${$promptmsg}<div>`);
                                const $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
                                const $no = FwConfirmation.addButton($confirmation, 'No', false);
                                const $cancel = FwConfirmation.addButton($confirmation, 'Cancel');
                                //----------
                                $yes.on('click', e => {
                                    FwConfirmation.destroyConfirmation($confirmation);
                                    const request: any = { PushChangesToPurchaseOrder: true };
                                    if (state === 'browse') {
                                        const vendorInvoiceId: string = $element.find('.selected [data-browsedatafield="VendorInvoiceId"]').attr('data-originalvalue');
                                        approve($element, vendorInvoiceId, function onSuccess(response) { FwBrowse.databind($element); }, null, request);
                                    } else if (state === 'form') {
                                        const vendorInvoiceId: string = FwFormField.getValueByDataField($element, 'VendorInvoiceId');
                                        approve($element, vendorInvoiceId, function onSuccess(response) { FwModule.refreshForm($element); }, null, request);
                                    }
                                });
                                //----------
                                $no.on('click', e => {
                                    FwConfirmation.destroyConfirmation($confirmation);
                                    const request: any = { PushChangesToPurchaseOrder: false };
                                    if (state === 'browse') {
                                        const vendorInvoiceId: string = $element.find('.selected [data-browsedatafield="VendorInvoiceId"]').attr('data-originalvalue');
                                        approve($element, vendorInvoiceId, function onSuccess(response) { FwBrowse.databind($element); }, null, request);
                                    } else if (state === 'form') {
                                        const vendorInvoiceId: string = FwFormField.getValueByDataField($element, 'VendorInvoiceId');
                                        approve($element, vendorInvoiceId, function onSuccess(response) { FwModule.refreshForm($element); }, null, request);
                                    }
                                });
                            } else {
                                FwNotification.renderNotification('WARNING', response.msg);
                            }
                        }
                    }, null, $formToBlock);
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        }

        if (state === 'browse') {
            const vendorInvoiceId: string = $element.find('.selected [data-browsedatafield="VendorInvoiceId"]').attr('data-originalvalue');
            approve($element, vendorInvoiceId, function onSuccess(response) { FwBrowse.databind($element); });
        } else if (state === 'form') {
            const vendorInvoiceId: string = FwFormField.getValueByDataField($element, 'VendorInvoiceId');
            approve($element, vendorInvoiceId, function onSuccess(response) { FwModule.refreshForm($element); });
        }
    }
    ////----------------------------------------------------------------------------------------------
    //formApproveVendorInvoice($form: JQuery) {
    //}
    ////----------------------------------------------------------------------------------------------
    //approveVendorInvoice($formToBlock: JQuery, vendorInvoiceId: string, onApproveSuccess: (response: any) => void, onApproveFailure?: (response: any) => void): void {
    //    try {
    //        if ((vendorInvoiceId == null) || (vendorInvoiceId == '') || (typeof vendorInvoiceId === 'undefined')) {
    //            FwNotification.renderNotification('WARNING', 'No Vendor Invoice Selected');
    //        }
    //        else {
    //            FwAppData.apiMethod(true, 'POST', `api/v1/vendorinvoice/toggleapproved/${vendorInvoiceId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
    //                if (response.success === true) {
    //                    if ((onApproveSuccess) && (typeof onApproveSuccess === 'function')) {
    //                        onApproveSuccess(response);
    //                    }
    //                } else {
    //                    FwNotification.renderNotification('WARNING', response.msg);
    //                    if ((onApproveFailure) && (typeof onApproveFailure === 'function')) {
    //                        onApproveFailure(response);
    //                    }
    //                }
    //            }, null, $formToBlock);
    //        }
    //    } catch (ex) {
    //        FwFunc.showError(ex);
    //    }
    //}
    //----------------------------------------------------------------------------------------------
    browseUnapproveVendorInvoice($browse: JQuery) {
        const vendorInvoiceId: string = $browse.find('.selected [data-browsedatafield="VendorInvoiceId"]').attr('data-originalvalue');
        this.unApproveVendorInvoice($browse, vendorInvoiceId, function onSuccess(response) { FwBrowse.databind($browse); });
    }
    //----------------------------------------------------------------------------------------------
    formUnapproveVendorInvoice($form: JQuery) {
        const vendorInvoiceId: string = FwFormField.getValueByDataField($form, 'VendorInvoiceId');
        this.unApproveVendorInvoice($form, vendorInvoiceId, function onSuccess(response) { FwModule.refreshForm($form); });
    }
    //----------------------------------------------------------------------------------------------
    unApproveVendorInvoice($formToBlock: JQuery, vendorInvoiceId: string, onUnapproveSuccess: (response: any) => void, onUnapproveFailure?: (response: any) => void): void {
        try {
            let request: any;
            if ((vendorInvoiceId == null) || (vendorInvoiceId == '') || (typeof vendorInvoiceId === 'undefined')) {
                FwNotification.renderNotification('WARNING', 'No Vendor Invoice Selected');
            }
            else {
                request = {
                    VendorInvoiceId: vendorInvoiceId
                }

                FwAppData.apiMethod(true, 'POST', `api/v1/vendorinvoice/toggleapproved/`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    if (response.success === true) {
                        if ((onUnapproveSuccess) && (typeof onUnapproveSuccess === 'function')) {
                            onUnapproveSuccess(response);
                        }
                    } else {
                        FwNotification.renderNotification('WARNING', response.msg);
                        if ((onUnapproveFailure) && (typeof onUnapproveFailure === 'function')) {
                            onUnapproveFailure(response);
                        }
                    }
                }, null, $formToBlock);
            }
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
}

var VendorInvoiceController = new VendorInvoice();