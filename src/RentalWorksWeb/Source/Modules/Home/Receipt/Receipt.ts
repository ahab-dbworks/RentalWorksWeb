routes.push({ pattern: /^module\/receipt$/, action: function (match: RegExpExecArray) { return ReceiptController.getModuleScreen(); } });
routes.push({ pattern: /^module\/receipt\/(\S+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { 'datafield': match[1], 'search': match[2].replace(/%20/g, ' ').replace(/%2f/g, '/') }; return ReceiptController.getModuleScreen(filter); } });

class Receipt {
    Module: string = 'Receipt';
    apiurl: string = 'api/v1/receipt';
    caption: string = Constants.Modules.Home.Receipt.caption;
    nav: string = Constants.Modules.Home.Receipt.nav;
    id: string = Constants.Modules.Home.Receipt.id;
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    thisModule: Receipt;
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: { datafield: string, search: string }) {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $browse: any = this.openBrowse();
        const today = FwFunc.getDate();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);

            if (typeof filter !== 'undefined') {
                const datafields = filter.datafield.split('%20');
                for (let i = 0; i < datafields.length; i++) {
                    datafields[i] = datafields[i].charAt(0).toUpperCase() + datafields[i].substr(1);
                }
                filter.datafield = datafields.join('')
                $browse.find(`div[data-browsedatafield="${filter.datafield}"]`).find('input').val(filter.search);
            } else {
                // if no filter passed in, default view to today's date
                $browse.find('div[data-browsedatafield="ReceiptDate"]').find('input').val(today);
                $browse.find('div[data-browsedatafield="ReceiptDate"]').find('input').change();
            }

            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
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

        $browse.data('ondatabind', request => {
            request.activeviewfields = this.ActiveViewFields;
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
    }
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems($menuObject) {
        //Location Filter
        const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
        const location = JSON.parse(sessionStorage.getItem('location'));
        const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);

        if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
            this.ActiveViewFields.LocationId = [location.locationid];
        }

        const viewLocation = [];
        viewLocation.push($userLocation, $allLocations);
        FwMenu.addViewBtn($menuObject, 'Location', viewLocation, true, "LocationId");
        return $menuObject;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentModuleInfo?: any) {
        let $form: any = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        $form.find(`.credit-amounts`).hide();
        if (mode === 'NEW') {
            const location = JSON.parse(sessionStorage.getItem('location'));
            FwFormField.setValueByDataField($form, 'LocationId', location.locationid, location.location);
            FwFormField.setValueByDataField($form, 'RecType', 'P');
            const today = FwFunc.getDate();
            FwFormField.setValueByDataField($form, 'ReceiptDate', today);
            FwFormField.enable($form.find('div[data-datafield="PaymentBy"]'));
            FwFormField.enable($form.find('div[data-datafield="DealId"]'));
            FwFormField.enable($form.find('div[data-datafield="CustomerId"]'));
            const usersid = sessionStorage.getItem('usersid');  // J. Pace 7/09/18  C4E0E7F6-3B1C-4037-A50C-9825EDB47F44
            const name = sessionStorage.getItem('name');
            FwFormField.setValue($form, 'div[data-datafield="AppliedById"]', usersid, name);
            // Deal and Customer fields
            $form.find('.deal-customer').data('onchange', () => {
                $form.find('span.credit-amounts').hide();
                this.loadReceiptInvoiceGrid($form);
                const $submoduleCreditBrowse = this.openCreditBrowse($form);
                $form.find('.credits-page').html($submoduleCreditBrowse);
            });

            $form.find('div[data-datafield="PaymentBy"]').change(() => {
                this.paymentByRadioBehavior($form);
                if (FwFormField.getValueByDataField($form, 'DealId') !== '' && FwFormField.getValueByDataField($form, 'CustomerId') !== '') {
                    this.loadReceiptInvoiceGrid($form);
                }
            });
        }
        $form.find('div[data-datafield="PaymentAmount"] input').inputmask({ alias: "currency", prefix: '' }); // temp until we fix FW money prefix to render based on country
        this.events($form);

        $form.find('.braintree-btn').click(() => {
            let braintreeScipt = `<script>
              let button = document.querySelector('#braintree-btn');

              braintree.dropin.create({
                authorization: 'sandbox_bzjwnpg3_k3fb9p88pvbfjfg9',
                container: '#dropin-container'
              }, function (createErr, instance) {
                button.addEventListener('click', function () {
                  instance.requestPaymentMethod(function (requestPaymentMethodErr, payload) {
                    // When the user clicks on the 'Submit payment' button this code will send the
                    // encrypted payment information in a variable called a payment method nonce
                    $.ajax({
                      type: 'POST',
                      url: '/checkout',
                      data: {'paymentMethodNonce': payload.nonce}
                    }).done(function(result) {
                      // Tear down the Drop-in UI
                      instance.teardown(function (teardownErr) {
                        if (teardownErr) {
                          console.error('Could not tear down Drop-in UI!');
                        } else {
                          console.info('Drop-in UI has been torn down!');
                          // Remove the 'Submit payment' button
                          button.remove();
                        }
                      });

                      if (result.success) {
                        $('#checkout-message').html('<h1>Success</h1><p>Your Drop-in UI is working! Check your <a href="https://sandbox.braintreegateway.com/login">sandbox Control Panel</a> for your test transactions.</p><p>Refresh to try another transaction.</p>');
                      } else {
                        console.log(result);
                        $('#checkout-message').html('<h1>Error</h1><p>Check your console.</p>');
                      }
                    });
                  });
                });
              });
            </script>`;

            //let braintreeScipt = `<script> let button = document.querySelector('#braintree-btn'); braintree.dropin.create({authorization: 'sandbox_bzjwnpg3_k3fb9p88pvbfjfg9',container: '#dropin-container'}, function (createErr, instance) {button.addEventListener('click', function () {instance.requestPaymentMethod(function (err, payload) {});});});</script>`;
            $form.find('.braintree-row').prepend(braintreeScipt);
            //let $popup = FwPopup.renderPopup(jQuery(braintreeScipt), { ismodal: true });
            //FwPopup.showPopup($popup);
        })

        // Adds receipt invoice datatable to request
        $form.data('beforesave', request => {
            request.InvoiceDataList = this.getFormTableData($form);
        });

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form: any = this.openForm('EDIT');
        FwFormField.setValueByDataField($form, 'ReceiptId', uniqueids.ReceiptId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any): void {
        FwModule.saveForm(this.Module, $form, parameters);
        FwFormField.disable($form.find('div[data-datafield="PaymentBy"]'));
        FwFormField.disable($form.find('div[data-datafield="DealId"]'));
        FwFormField.disable($form.find('div[data-datafield="CustomerId"]'));
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: JQuery): void {
        const $glDistributionGrid = $form.find('div[data-grid="GlDistributionGrid"]');
        const $glDistributionGridControl = FwBrowse.loadGridFromTemplate('GlDistributionGrid');
        $glDistributionGrid.empty().append($glDistributionGridControl);
        $glDistributionGridControl.data('ondatabind', request => {
            request.uniqueids = {
                ReceiptId: FwFormField.getValueByDataField($form, 'ReceiptId')
            };
        });
        FwBrowse.init($glDistributionGridControl);
        FwBrowse.renderRuntimeHtml($glDistributionGridControl);
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
        // Click Event on tabs to load grids/browses
        $form.on('click', '[data-type="tab"]', e => {
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
        // hide / show payment section for credit cards
        const paymentTypeType = FwFormField.getValueByDataField($form, 'PaymentTypeType');
        if (paymentTypeType === 'CREDIT CARD') {
            $form.find('.braintree-row').show();
        } else {
            $form.find('.braintree-row').hide();
        }
        this.paymentByRadioBehavior($form);
        this.loadReceiptInvoiceGrid($form);
        this.events($form);
        // Credit submodule
        setTimeout(() => {
            const $submoduleCreditBrowse = this.openCreditBrowse($form);
            $form.find('.credits-page').append($submoduleCreditBrowse);
        }, 100)
    }
    //----------------------------------------------------------------------------------------------
    events($form: JQuery): void {
        $form.find('div[data-datafield="PaymentTypeId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="PaymentTypeType"]', $tr.find('.field[data-formdatafield="PaymentTypeType"]').attr('data-originalvalue'));
            const paymentTypeType = FwFormField.getValueByDataField($form, 'PaymentTypeType');
            paymentTypeType === 'CREDIT CARD' ? $form.find('.braintree-row').show() : $form.find('.braintree-row').hide();
        });
        $form.find('div.credits-tab').on('click', e => {
            //Disable clicking  tab w/o a Deal / Customer
            const paymentBy = FwFormField.getValueByDataField($form, 'PaymentBy');
            let dealCustomer;
            if (paymentBy === 'DEAL') {
                dealCustomer = FwFormField.getValueByDataField($form, 'DealId');
            } else if (paymentBy === 'CUSTOMER') {
                dealCustomer = FwFormField.getValueByDataField($form, 'CustomerId');
            }
            if (dealCustomer === '') {
                e.stopPropagation();
                FwNotification.renderNotification('WARNING', 'Select a Deal or Customer first.')
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    openCreditBrowse($form) {
        let $browse;
        const paymentBy = FwFormField.getValueByDataField($form, 'PaymentBy');
        let dealCustomerId;
        let dealCustomer;
        if (paymentBy === 'DEAL') {
            dealCustomerId = FwFormField.getValueByDataField($form, 'DealId');
            dealCustomer = $form.find('div[data-datafield="DealId"]').attr('data-datafield');
            $browse = DealCreditController.openBrowse();
            $browse.data('ondatabind', request => {
                request.uniqueids = { DealId: dealCustomerId }
                request.activeviewfields = DealCreditController.ActiveViewFields;
            });
        } else if (paymentBy === 'CUSTOMER') {
            dealCustomerId = FwFormField.getValueByDataField($form, 'CustomerId');
            dealCustomer = $form.find('div[data-datafield="CustomerId"]').attr('data-datafield');
            $browse = CustomerCreditController.openBrowse();
            $browse.data('ondatabind', request => {
                request.uniqueids = { CustomerId: dealCustomerId }
                request.activeviewfields = CustomerCreditController.ActiveViewFields;
            });
        }

        FwBrowse.addEventHandler($browse, 'afterdatabindcallback', ($control, dt) => {
            this.calculateCreditTotals($form, dealCustomer, dealCustomerId);
        });
        FwBrowse.databind($browse);
        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    calculateCreditTotals($form, datafield, id) {
        const officeLocation = FwFormField.getValueByDataField($form, 'LocationId')
        let query: string;
        if (datafield === 'DealId') {
            query = `remainingdepositamounts?CustomerId=&DealId=${id}&OfficeLocationId=${officeLocation}`
        } else {
            query = `remainingdepositamounts?CustomerId=${id}&DealId=&OfficeLocationId=${officeLocation}`
        }

        FwAppData.apiMethod(true, 'GET', `${this.apiurl}/${query}`, null, FwServices.defaultTimeout, function onSuccess(response) {
            if (response.Overpayments !== 0) {
                $form.find(`span[data-creditfield="Overpayments"]`).text(`Overpayments: $${response.OverpaymentsFormatted}`);
                $form.find(`span[data-creditfield="Overpayments"]`).show();
            } else {
                $form.find(`span[data-creditfield="Overpayments"]`).hide();
            }
            if (response.CreditMemos !== 0) {
                $form.find(`span[data-creditfield="CreditMemos"]`).text(`Credit Memos: $${response.CreditMemosFormatted}`);
                $form.find(`span[data-creditfield="CreditMemos"]`).show();
            } else {
                $form.find(`span[data-creditfield="CreditMemos"]`).hide();
            }
            if (response.DepletingDeposits !== 0) {
                $form.find(`span[data-creditfield="DepletingDeposits"]`).text(`Depleting Deposits: $${response.DepletingDepositsFormatted}`);
                $form.find(`span[data-creditfield="DepletingDeposits"]`).show();
            } else {
                $form.find(`span[data-creditfield="DepletingDeposits"]`).hide();
            }

        }, function onError(response) {
            FwFunc.showError(response);
        }, null)

    }
    //----------------------------------------------------------------------------------------------
    loadReceiptInvoiceGrid($form: JQuery): void {
        if ($form.attr('data-mode') === 'NEW') {
            $form.find('.table-rows').html('<tr class="empty-row" style="height:33px;"><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>');
        }
        const calculateInvoiceTotals = ($form, event?) => {
            let amountValBefore = $form.data('payAmountOnFocus');
            if (amountValBefore) {
                amountValBefore = amountValBefore.replace(/,/g, '');
                console.log('amountValBefore', amountValBefore)
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
            const amountToApply = FwFormField.getValueByDataField($form, 'PaymentAmount').replace(/,/g, '');
            let unappliedTotalPrior = $form.find(`div[data-totalfield="UnappliedInvoiceTotal"] input`).val().replace(/[$ ,]+/g, "").trim();
            if (unappliedTotalPrior === '') { unappliedTotalPrior = '0.00'; }
            for (let i = 0; i < $amountFields.length; i++) {
                // ----- Bottom line totaling
                let amountValOnLine = $amountFields.eq(i).val().replace(/,/g, '');
                if (amountValOnLine === '') { amountValOnLine = '0.00'; } // possibly unecessary
                // Amount Column
                amountTotal = amountTotal.plus(amountValOnLine);
                // Total Column
                let totalValOnLine = $totalFields.eq(i).text().replace(/,/g, '');
                totalTotal = totalTotal.plus(totalValOnLine);
                // Applied Column
                let appliedValOnLine = $appliedFields.eq(i).text().replace(/,/g, '');
                appliedTotal = appliedTotal.plus(appliedValOnLine);
                // Due Column
                let dueValOnLine = $dueFields.eq(i).text().replace(/,/g, '');
                dueTotal = dueTotal.plus(dueValOnLine);

                // ----- Line Totaling for Applied and Due fields
                if (event) {
                    const element = jQuery(event.currentTarget);
                    // Button
                    if (element.attr('data-type') === 'button') {
                        if (+(element.attr('row-index')) === i) {
                            let amountInput = $amountFields.eq(i).val().replace(/,/g, '');
                            if (amountInput === '') { amountInput = '0.00'; }
                            let amountTotal = new Decimal(0);
                            amountTotal = amountTotal.plus(amountInput);
                            let dueTotal = new Decimal(0);
                            const dueValOnLine = $dueFields.eq(i).text().replace(/,/g, '');
                            dueTotal = dueTotal.plus(dueValOnLine);
                            let unappliedTotalPriorDecimal = new Decimal(0);
                            unappliedTotalPriorDecimal = unappliedTotalPriorDecimal.plus(unappliedTotalPrior);
                            let amountVal;

                            console.log('unappliedTotalPrior', unappliedTotalPrior)
                            console.log('amountInput', amountInput)
                            console.log('amountTotal', amountTotal)
                            console.log('dueValOnLine', dueValOnLine)
                            console.log('dueTotal', dueTotal)
                            console.log('unappliedTotalPriorDecimal', unappliedTotalPriorDecimal)



                            // If Unapplied Amount >= "Due"  increase the "Amount" value by the "Due" value on the line
                            if (unappliedTotalPriorDecimal.greaterThanOrEqualTo(dueTotal)) {
                                amountVal = dueTotal.plus(amountTotal);
                                console.log('amountVal', amountVal)

                                $amountFields.eq(i).val(amountVal.toFixed(2));
                            }
                            // If Unapplied Amount < "Due"  increase the "Amount" value by the Unapplied Amount value on the line
                            if (unappliedTotalPriorDecimal.lessThan(dueTotal)) {
                                amountVal = amountTotal.plus(unappliedTotalPriorDecimal)
                                console.log('amountVal', amountVal)

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
                            $appliedFields.eq(i).text(applied);
                            let due = dueLineTotal.toFixed(2);
                            due = due.replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
                            $dueFields.eq(i).text(due);
                            recurse = true;
                            break;
                        }
                    }
                    // Amount field
                    const currentAmountField = $amountFields.eq(i);
                    if (element.is(currentAmountField)) {
                        let amountInput = $amountFields.eq(i).val().replace(/,/g, '');
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
                        $appliedFields.eq(i).text(applied);
                        let due = dueLineTotal.toFixed(2);
                        due = due.replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
                        $dueFields.eq(i).text(due);
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
        const getInvoiceData = ($form) => {
            const request: any = {};
            const officeLocationId = JSON.parse(sessionStorage.getItem('location')).locationid;
            const receiptId = FwFormField.getValueByDataField($form, 'ReceiptId');
            const receiptDate = FwFormField.getValueByDataField($form, 'ReceiptDate');

            request.uniqueids = {
                OfficeLocationId: officeLocationId,
                ReceiptId: receiptId,
                ReceiptDate: receiptDate,
            }
            request.orderby = 'InvoiceDate'
            const paymentBy = FwFormField.getValueByDataField($form, 'PaymentBy');
            if (paymentBy === 'DEAL') {
                request.uniqueids.DealId = FwFormField.getValueByDataField($form, 'DealId');
            } else if (paymentBy === 'CUSTOMER') {
                request.uniqueids.CustomerId = FwFormField.getValueByDataField($form, 'CustomerId');
            }

            FwAppData.apiMethod(true, 'POST', 'api/v1/receiptinvoice/browse', request, FwServices.defaultTimeout, res => {
                const rows = res.Rows;
                console.log('rows: ', rows)
                const htmlRows: Array<string> = [];
                if (rows.length) {
                    for (let i = 0; i < rows.length; i++) {
                        htmlRows.push(`<tr class="row"><td data-validationname="Deal" data-fieldname="DealId" data-datafield="${rows[i][res.ColumnIndex.DealId]}" data-displayfield="${rows[i][res.ColumnIndex.Deal]}" class="text">${rows[i][res.ColumnIndex.Deal]}<i class="material-icons btnpeek">more_horiz</i></td><td class="text InvoiceId" style="display:none;">${rows[i][res.ColumnIndex.InvoiceId]}</td><td class="text InvoiceReceiptId" style="display:none;">${rows[i][res.ColumnIndex.InvoiceReceiptId]}</td><td data-validationname="Invoice" data-fieldname="InvoiceId" data-datafield="${rows[i][res.ColumnIndex.InvoiceId]}" data-displayfield="${rows[i][res.ColumnIndex.InvoiceNumber]}" class="text">${rows[i][res.ColumnIndex.InvoiceNumber]}<i class="material-icons btnpeek">more_horiz</i></td><td class="text">${rows[i][res.ColumnIndex.InvoiceDate]}</td><td data-validationname="Order" data-fieldname="OrderId" data-datafield="${rows[i][res.ColumnIndex.OrderId]}" data-displayfield="${rows[i][res.ColumnIndex.Description]}" class="text">${rows[i][res.ColumnIndex.OrderNumber]}<i class="material-icons btnpeek">more_horiz</i></td><td class="text">${rows[i][res.ColumnIndex.Description]}</td><td style="text-align:right;" data-invoicefield="InvoiceTotal" class="decimal static-amount">${rows[i][res.ColumnIndex.Total]}</td><td style="text-align:right;" data-invoicefield="InvoiceApplied" class="decimal static-amount">${rows[i][res.ColumnIndex.Applied]}</td><td style="text-align:right;" data-invoicefield="InvoiceDue" class="decimal static-amount">${rows[i][res.ColumnIndex.Due]}</td><td data-enabled="true" data-isuniqueid="false" data-datafield="InvoiceAmount" data-invoicefield="InvoiceAmount" class="decimal fwformfield pay-amount invoice-amount"><input class="decimal fwformfield fwformfield-value" style="font-size:inherit;" type="text" autocapitalize="none" row-index="${i}" value="${rows[i][res.ColumnIndex.Amount]}"></td><td><div class="fwformcontrol apply-btn" row-index="${i}" data-type="button" style="height:18px;padding:.3rem;line-height:17px;font-size:14px;">Apply</div></td></tr>`);
                    }
                    $form.find('.table-rows').html('');
                    $form.find('.table-rows').html(htmlRows.join(''));
                    $form.find('.invoice-amount input').inputmask({ alias: "currency", prefix: '' });
                    $form.find('.static-amount:not(input)').inputmask({ alias: "currency", prefix: '' });

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
                        $form.data('payAmountOnFocus', val);
                        console.log('payAmountOnFocusChange', $form.data('payAmountOnFocus'))
                    });
                    // Store intial amount value for calculations after change
                    $form.find('.pay-amount input').on('focus', ev => {
                        ev.stopPropagation();
                        const el = jQuery(ev.currentTarget);
                        let val = el.val();
                        if (val === '') {
                            val = '0.00'
                        }
                        $form.data('payAmountOnFocus', val);
                        console.log('payAmountOnFocus', $form.data('payAmountOnFocus'))
                    });
                    // Store intial amount value for calculations after change
                    $form.find('.pay-amount input').on('focus', ev => {
                        ev.stopPropagation();
                        const el = jQuery(ev.currentTarget);
                        let val = el.val();
                        if (val === '') {
                            val = '0.00'
                        }
                        $form.data('payAmountOnFocus', val);
                        console.log('payAmountOnFocus', $form.data('payAmountOnFocus'))
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
        getInvoiceData($form);
    }
    //----------------------------------------------------------------------------------------------
    getFormTableData($form: JQuery): any {
        const $invoiceIdFields = $form.find('.InvoiceId');
        const $invoiceReceiptIds = $form.find('.InvoiceReceiptId');
        const $amountFields = $form.find('.invoice-amount input');
        const InvoiceDataList: any = [];
        for (let i = 0; i < $invoiceIdFields.length; i++) {
            const invoiceId = $invoiceIdFields.eq(i).text();
            const invoiceReceiptId = $invoiceReceiptIds.eq(i).text();
            let amount: any = $amountFields.eq(i).val();
            amount = amount.replace(/,/g, '');

            const fields: any = {}
            fields.InvoiceReceiptId = invoiceReceiptId;
            fields.InvoiceId = invoiceId;
            fields.Amount = +amount;
            InvoiceDataList.push(fields);
        }

        return InvoiceDataList;
    }
    //----------------------------------------------------------------------------------------------
    paymentByRadioBehavior($form: JQuery): void {
        if (FwFormField.getValueByDataField($form, 'PaymentBy') === 'DEAL') {
            $form.find('div[data-datafield="CustomerId"]').hide();
            $form.find('div[data-datafield="CustomerId"]').attr('data-required', 'false');
            $form.find('div[data-datafield="DealId"]').show();
            $form.find('div[data-datafield="DealId"]').attr('data-required', 'true');
        } else {
            $form.find('div[data-datafield="DealId"]').hide();
            $form.find('div[data-datafield="DealId"]').attr('data-required', 'false');
            $form.find('div[data-datafield="CustomerId"]').show();
            $form.find('div[data-datafield="CustomerId"]').attr('data-required', 'true');
        }
    }
    //----------------------------------------------------------------------------------------------
}
//----------------------------------------------------------------------------------------------
var ReceiptController = new Receipt();