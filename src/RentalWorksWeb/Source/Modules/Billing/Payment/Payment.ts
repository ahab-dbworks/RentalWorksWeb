routes.push({ pattern: /^module\/payment$/, action: function (match: RegExpExecArray) { return PaymentController.getModuleScreen(); } });

class Payment {
    Module: string = 'Payment';
    apiurl: string = 'api/v1/payment';
    caption: string = Constants.Modules.Billing.children.Payment.caption;
    nav: string = Constants.Modules.Billing.children.Payment.nav;
    id: string = Constants.Modules.Billing.children.Payment.id;
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    thisModule: Payment;
    currencySymbol: string = '';
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        FwMenu.addBrowseMenuButtons(options);

        //Location Filter
        const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
        const location = JSON.parse(sessionStorage.getItem('location'));
        const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);

        if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
            this.ActiveViewFields.LocationId = [location.locationid];
        }

        const viewLocation = [];
        viewLocation.push($userLocation, $allLocations);
        FwMenu.addViewBtn(options.$menu, 'Location', viewLocation, true, "LocationId");
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $browse: any = this.openBrowse();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
            const chartFilters = JSON.parse(sessionStorage.getItem('chartfilter'));
            if (!chartFilters) {
                //const today = FwFunc.getDate();
                //$browse.find('div[data-browsedatafield="PaymentDate"]').find('input').val(today).change();
                FwBrowse.databind($browse);
                FwBrowse.screenload($browse);
            }
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse: any = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        const chartFilters = JSON.parse(sessionStorage.getItem('chartfilter'));
        if (!chartFilters) {
            $browse.data('ondatabind', request => {
                request.activeviewfields = this.ActiveViewFields;
            });
        } else {
            $browse.data('ondatabind', request => {
                request.activeviewfields = '';
            });
        }

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
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentModuleInfo?: any) {
        let $form: any = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            const location = JSON.parse(sessionStorage.getItem('location'));
            FwFormField.setValueByDataField($form, 'LocationId', location.locationid, location.location);
            FwFormField.setValueByDataField($form, 'RecType', 'P');
            FwFormField.setValueByDataField($form, 'CurrencyId', location.defaultcurrencyid, location.defaultcurrencycode);
            const today = FwFunc.getDate();
            FwFormField.setValueByDataField($form, 'PaymentDate', today);
            FwFormField.enable($form.find('div[data-datafield="PaymentDate"]'));
            const usersid = sessionStorage.getItem('usersid');  // J. Pace 7/09/18  C4E0E7F6-3B1C-4037-A50C-9825EDB47F44
            const name = sessionStorage.getItem('name');
            FwFormField.setValue($form, 'div[data-datafield="AppliedById"]', usersid, name);
            // Deal and Customer fields
            $form.find('div[data-datafield="VendorId"]').data('onchange', $tr => {
                const currencyId = $tr.find('.field[data-formdatafield="DefaultCurrencyId"]').attr('data-originalvalue');
                const currencySymbol = $tr.find('.field[data-formdatafield="DefaultCurrencySymbol"]').attr('data-originalvalue');
                if (currencySymbol) {
                    this.currencySymbol = currencySymbol;
                }
                if (currencyId) { // default currency to Vendor but only if one is indicated
                    FwFormField.setValueByDataField($form, 'CurrencyId', currencyId, $tr.find('.field[data-formdatafield="DefaultCurrencyCode"]').attr('data-originalvalue'));
                }

                this.loadPaymentVendorInvoiceGrid($form);
            });
        }
        $form.find('div[data-datafield="PaymentAmount"] input').inputmask({ alias: "currency", prefix: '' }); // temp until we fix FW money prefix to render based on country
        this.events($form);

        // Adds receipt invoice or credit datatable to request
        $form.data('beforesave', request => {
            request.InvoiceDataList = this.getFormTableData($form);
        });

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form: any = this.openForm('EDIT');
        FwFormField.setValueByDataField($form, 'PaymentId', uniqueids.PaymentId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any): void {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    deleteRecord($browse): void {
        FwModule.deleteRecord(this.Module, $browse);
        let observer;
        const app = document.getElementById('application');
        observer = new MutationObserver(() => {
            const message = jQuery(app).find('.advisory .fwconfirmationbox .body .message').text();
            if (message.startsWith("Payment has already been exported.  Are you sure you want to delete?")) {
                this.overrideDelete($browse);
            }
        });
        observer.observe(app, { attributes: true, childList: true, subtree: true });

        $browse.data('onscreenunload', () => { observer.disconnect(); });
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: JQuery): void {
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
                    PaymentId: FwFormField.getValueByDataField($form, 'PaymentId')
                };
                request.totalfields = ["Debit", "Credit"];
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                //FwFormField.setValue2($form.find('.gldistribution-totals [data-totalfield="Debit"]'), dt.Totals.Debit);
                //FwFormField.setValue2($form.find('.gldistribution-totals [data-totalfield="Credit"]'), dt.Totals.Credit);
            },
        });
        // ----------
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: JQuery): void {
        // Disable form for certain RecType
        const recType = FwFormField.getValueByDataField($form, 'RecType');
        if (recType === 'O' || recType === 'D' || recType === 'C' || recType === 'R') {
            FwModule.setFormReadOnly($form);
            $form.find('.invoice-row').hide();
        }
        FwFormField.disable($form.find('div[data-datafield="CurrencyId"]'));
        // tab clicks
        $form.on('click', '[data-type="tab"][data-enabled!="false"]', e => {
            const tabname = jQuery(e.currentTarget).attr('id');
            const lastIndexOfTab = tabname.lastIndexOf('tab');
            const tabpage = `${tabname.substring(0, lastIndexOfTab)}tabpage${tabname.substring(lastIndexOfTab + 3)}`;

            const $gridControls = $form.find(`#${tabpage} [data-type="Grid"]`);
            if ($gridControls.length > 0) {
                for (let i = 0; i < $gridControls.length; i++) {
                    const $gridcontrol = jQuery($gridControls[i]);
                    FwBrowse.search($gridcontrol);
                }
            }

            const $browseControls = $form.find(`#${tabpage} [data-type="Browse"]`);
            if ($browseControls.length > 0) {
                for (let i = 0; i < $browseControls.length; i++) {
                    const $browseControl = jQuery($browseControls[i]);
                    FwBrowse.search($browseControl);
                }
            }
        });

        this.loadPaymentVendorInvoiceGrid($form);

        const formCurrencySymbol = FwFormField.getValueByDataField($form, 'CurrencySymbol');
        this.currencySymbol = formCurrencySymbol || '';
        this.events($form);
    }
    //----------------------------------------------------------------------------------------------
    events($form: JQuery): void {
        //// ----------
        //$form.find('div[data-datafield="PaymentTypeId"]').data('onchange', $tr => {
        //    FwFormField.setValue($form, 'div[data-datafield="PaymentTypeType"]', $tr.find('.field[data-formdatafield="PaymentTypeType"]').attr('data-originalvalue'));
        //    this.paymentTypes($form);
        //});
        // ----------
        $form.find('div[data-datafield="CurrencyId"]').data('onchange', $tr => {
            const currencySymbol = $tr.find('.field[data-formdatafield="CurrencySymbol"]').attr('data-originalvalue');
            if (currencySymbol) {
                this.currencySymbol = currencySymbol;
            }
            this.loadPaymentVendorInvoiceGrid($form);
        });
    }
    ////----------------------------------------------------------------------------------------------
    //paymentTypes($form) {
    //    const paymentTypeType = FwFormField.getValueByDataField($form, 'PaymentTypeType');


    //    let isOverDepletingMemo = false;
    //    if (paymentTypeType === 'DEPLETING DEPOSIT' || paymentTypeType === 'CREDIT MEMO' || paymentTypeType === 'OVERPAYMENT') {
    //        isOverDepletingMemo = true;
    //    }
    //    this.spendPaymentTypes($form, paymentTypeType, isOverDepletingMemo);

    //        this.loadPaymentVendorInvoiceGrid($form);

    //}
    ////----------------------------------------------------------------------------------------------
    //spendPaymentTypes($form, paymentTypeType, isOverDepletingMemo) {
    //const paymentBy = FwFormField.getValueByDataField($form, 'PaymentBy');

    //if (isOverDepletingMemo) {
    //    $form.find('div[data-datafield="CheckNumber"]').hide();
    //    $form.find('div[data-datafield="CheckNumber"]').attr('data-required', 'false');

    //    if (paymentBy === 'DEAL') {
    //        $form.find('div[data-validationname="DealCreditValidation"]').show();
    //        $form.find('div[data-validationname="DealCreditValidation"]').attr('data-required', 'true').attr('data-enabled', 'true');
    //        $form.find('div[data-validationname="CustomerCreditValidation"]').hide();
    //        $form.find('div[data-validationname="CustomerCreditValidation"]').attr('data-required', 'false').attr('data-enabled', 'false');
    //    } else {
    //        $form.find('div[data-validationname="CustomerCreditValidation"]').show();
    //        $form.find('div[data-validationname="CustomerCreditValidation"]').attr('data-required', 'true').attr('data-enabled', 'true');
    //        $form.find('div[data-validationname="DealCreditValidation"]').hide();
    //        $form.find('div[data-validationname="DealCreditValidation"]').attr('data-required', 'false').attr('data-enabled', 'false');
    //    }

    //    $form.find('div[data-datafield="PaymentAmount"] .fwformfield-caption').text('Amount Remaining');
    //    FwFormField.disable($form.find('div[data-datafield="PaymentAmount"]'));

    //    if (paymentTypeType === 'DEPLETING DEPOSIT') {
    //        $form.find('div[data-datafield="OverPaymentId"] .fwformfield-caption').text('Deposit Reference');
    //    }
    //    if (paymentTypeType === 'CREDIT MEMO') {
    //        $form.find('div[data-datafield="OverPaymentId"] .fwformfield-caption').text('Credit Reference');
    //    }
    //    if (paymentTypeType === 'OVERPAYMENT') {
    //        $form.find('div[data-datafield="OverPaymentId"] .fwformfield-caption').text('Overpayment Reference');
    //    }
    //}
    //else {
    //    $form.find('.deal-cust-validate').hide();
    //    $form.find('.deal-cust-validate').attr('data-required', 'false');
    //    $form.find('.deal-cust-validate').attr('data-enabled', 'false');
    //    $form.find('div[data-datafield="CheckNumber"]').show();
    //    $form.find('div[data-datafield="CheckNumber"]').attr('data-required', 'true');

    //    $form.find('div[data-datafield="PaymentAmount"] .fwformfield-caption').text('Amount To Apply');
    //    FwFormField.enable($form.find('div[data-datafield="PaymentAmount"]'));
    //}
    //// Adust Amount Remaining field value for chosen payment value
    //$form.find('div[data-datafield="VendorId"]').data('onchange', $tr => {
    //    //FwFormField.setValue($form, 'div[data-datafield="PaymentAmount"]', $tr.find('.field[data-formdatafield="Remaining"]').attr('data-originalvalue'));
    //    //this.loadPaymentInvoiceGrid($form);
    //});
    //}
    //----------------------------------------------------------------------------------------------
    // refundCheck($form: JQuery) {
    // hide invoice grid - disable algo
    // show credits grid
    // after save or if not NEW, disable credit grid
    // }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {

        switch (datafield) {
            case 'AppliedById':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateappliedby`);
                break;
            case 'PaymentTypeId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatepaymenttype`);
                break;
        };
    }
    //----------------------------------------------------------------------------------------------
    //calculateCreditTotals($form, datafield, id) {
    //    const officeLocation = FwFormField.getValueByDataField($form, 'LocationId')
    //    let query: string;
    //    if (datafield === 'DealId') {
    //        query = `remainingdepositamounts?CustomerId=&DealId=${id}&OfficeLocationId=${officeLocation}`
    //    } else {
    //        query = `remainingdepositamounts?CustomerId=${id}&DealId=&OfficeLocationId=${officeLocation}`
    //    }

    //    FwAppData.apiMethod(true, 'GET', `${this.apiurl}/${query}`, null, FwServices.defaultTimeout, response => {
    //        if (response.Overpayments !== 0) {
    //            $form.find(`span[data-creditfield="Overpayments"]`).text(`Overpayments: ${this.currencySymbol}${response.OverpaymentsFormatted}`);
    //            $form.find(`span[data-creditfield="Overpayments"]`).show();
    //        } else {
    //            $form.find(`span[data-creditfield="Overpayments"]`).hide();
    //        }
    //        if (response.CreditMemos !== 0) {
    //            $form.find(`span[data-creditfield="CreditMemos"]`).text(`Credit Memos: ${this.currencySymbol}${response.CreditMemosFormatted}`);
    //            $form.find(`span[data-creditfield="CreditMemos"]`).show();
    //        } else {
    //            $form.find(`span[data-creditfield="CreditMemos"]`).hide();
    //        }
    //        if (response.DepletingDeposits !== 0) {
    //            $form.find(`span[data-creditfield="DepletingDeposits"]`).text(`Depleting Deposits: ${this.currencySymbol}${response.DepletingDepositsFormatted}`);
    //            $form.find(`span[data-creditfield="DepletingDeposits"]`).show();
    //        } else {
    //            $form.find(`span[data-creditfield="DepletingDeposits"]`).hide();
    //        }

    //    }, function onError(response) {
    //        FwFunc.showError(response);
    //    }, null)

    //}
    //----------------------------------------------------------------------------------------------
    loadPaymentVendorInvoiceGrid($form: JQuery): void {
        const currencyId = FwFormField.getValueByDataField($form, 'CurrencyId')

        if ($form.attr('data-mode') === 'NEW') {
            $form.find('.table-rows').html('<tr class="empty-row" style="height:33px;"><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>');
        }
        const calculateInvoiceTotals = ($form, event?) => {
            let amountValBefore;
            if (event != undefined) {
                const $this = jQuery(event.currentTarget);
                amountValBefore = $this.data('payAmountOnFocus');
                if (amountValBefore) {
                    amountValBefore = this.parseNum(amountValBefore);
                    //console.log('amountValBeforeinTOTAL', amountValBefore)
                }
            }
            let totalTotal = new Decimal(0);
            let appliedTotal = new Decimal(0);
            let dueTotal = new Decimal(0);
            let amountTotal = new Decimal(0);
            let recurse = false;
            const $totalFields = $form.find('td[data-invoicefield="InvoiceTotal"]');
            const $appliedFields = $form.find('td[data-invoicefield="InvoiceApplied"]');
            const $dueFields = $form.find('td[data-invoicefield="InvoiceDue"]');
            const $amountFields = $form.find('td[data-invoicefield="InvoiceAmount"] input');
            const amountToApply = +this.parseNum(FwFormField.getValueByDataField($form, 'PaymentAmount'));
            let unappliedTotalPrior = this.parseNum($form.find(`div[data-totalfield="UnappliedInvoiceTotal"] input`).val()).trim();
            if (unappliedTotalPrior === '') { unappliedTotalPrior = '0.00'; }
            for (let i = 0; i < $amountFields.length; i++) {
                // ----- Bottom line totaling
                let amountValOnLine = this.parseNum($amountFields.eq(i).val());
                if (amountValOnLine === '') { amountValOnLine = '0.00'; } // possibly unecessary
                // Amount Column
                amountTotal = amountTotal.plus(amountValOnLine);
                // Total Column
                let totalValOnLine = this.parseNum($totalFields.eq(i).text());
                totalTotal = totalTotal.plus(totalValOnLine);
                // Applied Column
                let appliedValOnLine = this.parseNum($appliedFields.eq(i).text());
                appliedTotal = appliedTotal.plus(appliedValOnLine);
                // Due Column
                let dueValOnLine = this.parseNum($dueFields.eq(i).text());
                dueTotal = dueTotal.plus(dueValOnLine);

                // ----- Line Totaling for Applied and Due fields
                if (event) {
                    const element = jQuery(event.currentTarget);
                    // Button
                    if (element.attr('data-type') === 'button') {
                        if (+(element.attr('row-index')) === i) {
                            let amountInput = this.parseNum($amountFields.eq(i).val());
                            if (amountInput === '') { amountInput = '0.00'; }
                            let amountTotal = new Decimal(0);
                            amountTotal = amountTotal.plus(amountInput);
                            let dueTotal = new Decimal(0);
                            const dueValOnLine = this.parseNum($dueFields.eq(i).text());
                            dueTotal = dueTotal.plus(dueValOnLine);
                            let unappliedTotalPriorDecimal = new Decimal(0);
                            unappliedTotalPriorDecimal = unappliedTotalPriorDecimal.plus(unappliedTotalPrior);
                            let amountVal;

                            //console.log('unappliedTotalPrior', unappliedTotalPrior)
                            //console.log('amountInput', amountInput)
                            //console.log('amountTotal', amountTotal)
                            //console.log('dueValOnLine', dueValOnLine)
                            //console.log('dueTotal', dueTotal)
                            //console.log('unappliedTotalPriorDecimal', unappliedTotalPriorDecimal)

                            // If Unapplied Amount >= "Due"  increase the "Amount" value by the "Due" value on the line
                            if (unappliedTotalPriorDecimal.greaterThanOrEqualTo(dueTotal)) {
                                amountVal = dueTotal.plus(amountTotal);
                                //  console.log('amountVal', amountVal)

                                $amountFields.eq(i).val(amountVal.toFixed(2));
                            }
                            // If Unapplied Amount < "Due"  increase the "Amount" value by the Unapplied Amount value on the line
                            if (unappliedTotalPriorDecimal.lessThan(dueTotal)) {
                                amountVal = amountTotal.plus(unappliedTotalPriorDecimal)
                                // console.log('amountVal', amountVal)

                                $amountFields.eq(i).val(amountVal.toFixed(2));
                            }
                            let amountDifference = new Decimal(0);
                            amountDifference = amountVal.minus(amountTotal)
                            let appliedLineTotal = new Decimal(0);
                            appliedLineTotal = appliedLineTotal.plus(appliedValOnLine).plus(amountDifference);
                            let dueLineTotal = new Decimal(0);
                            dueLineTotal = dueLineTotal.plus(dueValOnLine).minus(amountDifference);
                            let applied = appliedLineTotal.toFixed(2);
                            applied = applied.replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
                            $appliedFields.eq(i).text(`${this.currencySymbol}${applied}`);
                            let due = dueLineTotal.toFixed(2);
                            due = due.replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
                            $dueFields.eq(i).text(`${this.currencySymbol}${due}`);
                            recurse = true;
                            break;
                        }
                    }
                    // Amount input field
                    const currentAmountField = $amountFields.eq(i);
                    if (element.is(currentAmountField)) {
                        let amountInput = this.parseNum($amountFields.eq(i).val());
                        if (amountInput === '') {
                            amountInput = '0.00';
                        }
                        let amountTotal = new Decimal(0);
                        amountTotal = amountTotal.plus(amountInput);
                        let amountDifference = new Decimal(0);
                        amountDifference = amountTotal.minus(amountValBefore)
                        let appliedLineTotal = new Decimal(0);
                        appliedLineTotal = appliedLineTotal.plus(appliedValOnLine).plus(amountDifference);
                        let dueLineTotal = new Decimal(0);
                        dueLineTotal = dueLineTotal.plus(dueValOnLine).minus(amountDifference);
                        let applied = appliedLineTotal.toFixed(2);
                        applied = applied.replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
                        $appliedFields.eq(i).text(`${this.currencySymbol}${applied}`);
                        let due = dueLineTotal.toFixed(2);
                        due = due.replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
                        $dueFields.eq(i).text(`${this.currencySymbol}${due}`);
                        recurse = true;
                        break;
                    }
                }
            }
            if (recurse) {
                calculateInvoiceTotals($form);
                return;
            }
            const amount: any = amountTotal.toFixed(2);
            const total = totalTotal.toFixed(2);
            const due = dueTotal.toFixed(2);
            const applied = appliedTotal.toFixed(2);
            const unappliedTotal = amountToApply - amount;

            $form.find(`div[data-totalfield="UnappliedInvoiceTotal"] input`).val(unappliedTotal);
            $form.find(`div[data-totalfield="InvoiceTotal"] input`).val(total);
            $form.find(`div[data-totalfield="InvoiceApplied"] input`).val(applied);
            $form.find(`div[data-totalfield="InvoiceDue"] input`).val(due);
            $form.find(`div[data-totalfield="InvoiceAmountTotal"] input`).val(amount);
        }
        const getInvoiceData = ($form, currencyId?) => {
            const request: any = {};
            const officeLocationId = JSON.parse(sessionStorage.getItem('location')).locationid;
            const paymentId = FwFormField.getValueByDataField($form, 'PaymentId');
            const paymentDate = FwFormField.getValueByDataField($form, 'PaymentDate');
            const vendorId = FwFormField.getValueByDataField($form, 'VendorId');

            request.uniqueids = {
                OfficeLocationId: officeLocationId,
                PaymentId: paymentId,
                PaymentDate: paymentDate,
                VendorId: vendorId,
            }
            request.orderby = 'VendorInvoiceDate'
            if (currencyId) {
                request.uniqueids.CurrencyId = currencyId;
            }

            FwAppData.apiMethod(true, 'POST', 'api/v1/paymentvendorinvoice/browse', request, FwServices.defaultTimeout, res => {
                const rows = res.Rows;
                console.log('RES', res)
                const htmlRows: Array<string> = [];
                $form.find('div[data-type="money"] input').inputmask({ alias: "currency", prefix: this.currencySymbol });
                $form.find('div[data-datafield="PaymentAmount"] input').inputmask({ alias: "currency", prefix: this.currencySymbol });
                if (rows.length) {
                    for (let i = 0; i < rows.length; i++) {
                        htmlRows.push(`<tr class="row"><td data-validationname="Vendor" data-datafield="${rows[i][res.ColumnIndex.VendorId]}" data-displayfield="${rows[i][res.ColumnIndex.Vendor]}" class="text">${rows[i][res.ColumnIndex.Vendor]}<i class="material-icons btnpeek">more_horiz</i></td><td data-validationname="Deal" data-datafield="${rows[i][res.ColumnIndex.DealId]}" data-displayfield="${rows[i][res.ColumnIndex.Deal]}" class="text">${rows[i][res.ColumnIndex.Deal]}<i class="material-icons btnpeek">more_horiz</i></td><td class="text VendorInvoiceId" style="display:none;">${rows[i][res.ColumnIndex.VendorInvoiceId]}</td><td class="text VendorInvoicePaymentId" style="display:none;">${rows[i][res.ColumnIndex.VendorInvoicePaymentId]}</td><td data-validationname="VendorInvoice" data-peekform="VendorInvoice" data-datafield="${rows[i][res.ColumnIndex.VendorInvoiceId]}" data-displayfield="${rows[i][res.ColumnIndex.VendorInvoiceNumber]}" class="text">${rows[i][res.ColumnIndex.VendorInvoiceNumber]}<i class="material-icons btnpeek">more_horiz</i></td><td class="text">${rows[i][res.ColumnIndex.VendorInvoiceDate]}</td><td data-validationname="PurchaseOrder" data-datafield="${rows[i][res.ColumnIndex.PurchaseOrderId]}" data-displayfield="${rows[i][res.ColumnIndex.PurchaseOrderDescription]}" class="text">${rows[i][res.ColumnIndex.PurchaseOrderNumber]}<i class="material-icons btnpeek">more_horiz</i></td><td class="text">${rows[i][res.ColumnIndex.PurchaseOrderDescription]}</td><td style="text-align:right;" data-invoicefield="InvoiceTotal" class="decimal static-amount">${rows[i][res.ColumnIndex.Total]}</td><td style="text-align:right;" data-invoicefield="InvoiceApplied" class="decimal static-amount">${rows[i][res.ColumnIndex.Applied]}</td><td style="text-align:right;" data-invoicefield="InvoiceDue" class="decimal static-amount">${rows[i][res.ColumnIndex.Due]}</td><td data-enabled="true" data-isuniqueid="false" data-datafield="InvoiceAmount" data-invoicefield="InvoiceAmount" class="decimal fwformfield pay-amount invoice-amount"><input class="decimal fwformfield fwformfield-value" style="font-size:inherit;" type="text" autocapitalize="none" row-index="${i}" value="${rows[i][res.ColumnIndex.Amount]}"></td><td><div class="fwformcontrol apply-btn" row-index="${i}" data-type="button" style="height:27px;padding:.3rem;line-height:13px;font-size:14px;">Apply All</div></td></tr>`);
                    }
                    $form.find('.table-rows').html('');
                    $form.find('.table-rows').html(htmlRows.join(''));
                    $form.find('.invoice-amount input').inputmask({ alias: "currency", prefix: this.currencySymbol });
                    $form.find('.static-amount:not(input)').inputmask({ alias: "currency", prefix: this.currencySymbol });

                    (function () {
                        const $amountFields = $form.find('.invoice-amount input');
                        for (let i = 0; i < $amountFields.length; i++) {
                            const amount: any = $amountFields.eq(i).val();
                            if (amount === '0.00' || amount === '') {
                                $amountFields.eq(i).css('background-color', 'white');
                            } else {
                                $amountFields.eq(i).css('background-color', '#F4FFCC');
                            }
                        }
                        calculateInvoiceTotals($form);
                    })();
                    // Amount column listener
                    $form.find('.pay-amount input').on('change', ev => {
                        ev.stopPropagation();
                        const el = jQuery(ev.currentTarget);
                        let val = el.val();
                        if (el.hasClass('decimal')) {
                            if (val === '0.00' || val === '') {
                                el.css('background-color', 'white');
                                val = '0.00';
                            } else {
                                el.css('background-color', '#F4FFCC');
                            }
                        }
                        calculateInvoiceTotals($form, ev);
                        el.data('payAmountOnFocus', val); // reset line before total in case user doesnt leave input and changes again
                        // console.log('payAmountOnChange', el.data('payAmountOnFocus'))
                    });
                    // Store intial amount value for calculations after change
                    $form.find('.pay-amount input').on('focus', ev => {
                        ev.stopPropagation();
                        const el = jQuery(ev.currentTarget);
                        let val = el.val();
                        if (val === '') {
                            val = '0.00'
                        }
                        el.data('payAmountOnFocus', val);
                        //console.log('payAmountOnFocusOUTSIDE', el.data('payAmountOnFocus'))
                    });
                    // Amount to Apply listener
                    $form.find('.amount-to-apply input').on('change', ev => {
                        ev.stopPropagation();
                        calculateInvoiceTotals($form);
                    });

                    $form.find('.apply-btn').click((ev: JQuery.ClickEvent) => {
                        calculateInvoiceTotals($form, ev);
                    });
                    // btnpeek
                    $form.find('tbody tr .btnpeek').on('click', function (e: JQuery.Event) {
                        try {
                            const $td = jQuery(this).parent();
                            FwValidation.validationPeek($form, $td.attr('data-validationname'), $td.attr('data-datafield'), $td.attr('data-datafield'), $form, $td.attr('data-displayfield'));
                        } catch (ex) {
                            FwFunc.showError(ex)
                        }
                        e.stopPropagation();
                    });
                }
            }, null, $form);
        }
        getInvoiceData($form, currencyId);
    }
    //----------------------------------------------------------------------------------------------
    getFormTableData($form: JQuery): any {
        const invoiceRowHtml = $form.find('.invoice-row');
        if (invoiceRowHtml.attr('data-visible') === 'true') {

        }
        const $vendorInvoiceIdFields = $form.find('.VendorInvoiceId');
        const $vendorInvoicePaymentIds = $form.find('.VendorInvoicePaymentId');
        const $amountFields = $form.find('.invoice-amount input');
        const VendorInvoiceDataList: any = [];
        for (let i = 0; i < $vendorInvoiceIdFields.length; i++) {
            const vendorInvoiceId = $vendorInvoiceIdFields.eq(i).text();
            const vendorInvoicePaymentId = $vendorInvoicePaymentIds.eq(i).text();
            let amount: any = $amountFields.eq(i).val();
            amount = this.parseNum(amount);

            const fields: any = {}
            fields.VendorInvoicePaymentId = vendorInvoicePaymentId;
            fields.VendorInvoiceId = vendorInvoiceId;
            fields.Amount = +amount;
            VendorInvoiceDataList.push(fields);
        }

        return VendorInvoiceDataList;
    }
    //----------------------------------------------------------------------------------------------
    parseNum(number: string) {
        // remove all non-digit characters except for a period
        if (typeof number === 'string') {
            return number.replace(/[^\d\.]/g, '')
        } else {
            console.error(`input ${number} is not a string`)
        }
    }
    //----------------------------------------------------------------------------------------------
    overrideDelete($browse) {
        jQuery('#application').find('.advisory .fwconfirmationbox .fwconfirmation-button').click();

        const $confirmation = FwConfirmation.renderConfirmation(`Delete Payment`, '');
        $confirmation.find('.fwconfirmationbox').css('width', '490px');

        const html: Array<string> = [];
        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push(`    <div>Payment has already been exported.  Are you sure you want to delete?</div>`);
        html.push('  </div>');
        html.push('</div>');
        FwConfirmation.addControls($confirmation, html.join(''));
        const $yes = FwConfirmation.addButton($confirmation, 'Delete', false);
        FwConfirmation.addButton($confirmation, 'Cancel');
        $yes.focus();
        $yes.on('click', () => {
            FwConfirmation.destroyConfirmation($confirmation);
            //delete 
            const selectedRow = FwBrowse.getSelectedRow($browse);
            const id = FwBrowse.getRowBrowseUniqueIds($browse, selectedRow);
            FwAppData.apiMethod(true, 'DELETE', `api/v1/receipt/overridedelete/${id.PaymentId}`, null, FwServices.defaultTimeout,
                res => {
                    FwBrowse.databind($browse);
                }
                , ex => FwFunc.showError(ex), $browse);
        });
    }
    //----------------------------------------------------------------------------------------------
}
//----------------------------------------------------------------------------------------------
var PaymentController = new Payment();