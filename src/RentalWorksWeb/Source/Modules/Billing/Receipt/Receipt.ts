routes.push({ pattern: /^module\/receipt$/, action: function (match: RegExpExecArray) { return ReceiptController.getModuleScreen(); } });

class Receipt {
    Module: string = 'Receipt';
    apiurl: string = 'api/v1/receipt';
    caption: string = Constants.Modules.Billing.children.Receipt.caption;
    nav: string = Constants.Modules.Billing.children.Receipt.nav;
    id: string = Constants.Modules.Billing.children.Receipt.id;
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    thisModule: Receipt;
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
                //$browse.find('div[data-browsedatafield="ReceiptDate"]').find('input').val(today).change();
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

        $form.find(`.credit-amounts`).hide();
        // Hidden fields used for Overpayment and Depleting Deposit actions in NEW Records ( this.createDepletingDeposit, etc. )
        FwFormField.setValueByDataField($form, 'CreateOverpayment', false);
        FwFormField.setValueByDataField($form, 'CreateDepletingDeposit', false);

        if (mode === 'NEW') {
            const location = JSON.parse(sessionStorage.getItem('location'));
            FwFormField.setValueByDataField($form, 'LocationId', location.locationid, location.location);
            FwFormField.setValueByDataField($form, 'RecType', 'P');
            const today = FwFunc.getDate();
            FwFormField.setValueByDataField($form, 'ReceiptDate', today);
            FwFormField.enable($form.find('div[data-datafield="PaymentBy"]'));
            FwFormField.enable($form.find('div[data-datafield="DealId"]'));
            FwFormField.enable($form.find('div[data-datafield="CustomerId"]'));
            FwFormField.enable($form.find('div[data-datafield="ReceiptDate"]'));
            const usersid = sessionStorage.getItem('usersid');  // J. Pace 7/09/18  C4E0E7F6-3B1C-4037-A50C-9825EDB47F44
            const name = sessionStorage.getItem('name');
            FwFormField.setValue($form, 'div[data-datafield="AppliedById"]', usersid, name);
            // Deal and Customer fields
            $form.find('.deal-customer').data('onchange', () => {
                $form.find('span.credit-amounts').hide();
                const paymentTypeType = FwFormField.getValueByDataField($form, 'PaymentTypeType');
                if (paymentTypeType !== '' && paymentTypeType === 'REFUND CHECK') {
                    this.loadReceiptCreditGrid($form);

                } else {
                    this.loadReceiptInvoiceGrid($form);
                }
                const $submoduleCreditBrowse = this.openCreditBrowse($form);
                $form.find('.credits-page').html($submoduleCreditBrowse);
                FwFormField.setValue($form, '.deal-cust-validate', '');
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

        //$form.find('.braintree-btn').click(() => {
        //    let braintreeScipt = `<script>
        //      let button = document.querySelector('#braintree-btn');

        //      braintree.dropin.create({
        //        authorization: 'sandbox_bzjwnpg3_k3fb9p88pvbfjfg9',
        //        container: '#dropin-container'
        //      }, function (createErr, instance) {
        //        button.addEventListener('click', function () {
        //          instance.requestPaymentMethod(function (requestPaymentMethodErr, payload) {
        //            // When the user clicks on the 'Submit payment' button this code will send the
        //            // encrypted payment information in a variable called a payment method nonce
        //            $.ajax({
        //              type: 'POST',
        //              url: '/checkout',
        //              data: {'paymentMethodNonce': payload.nonce}
        //            }).done(function(result) {
        //              // Tear down the Drop-in UI
        //              instance.teardown(function (teardownErr) {
        //                if (teardownErr) {
        //                  console.error('Could not tear down Drop-in UI!');
        //                } else {
        //                  console.info('Drop-in UI has been torn down!');
        //                  // Remove the 'Submit payment' button
        //                  button.remove();
        //                }
        //              });

        //              if (result.success) {
        //                $('#checkout-message').html('<h1>Success</h1><p>Your Drop-in UI is working! Check your <a href="https://sandbox.braintreegateway.com/login">sandbox Control Panel</a> for your test transactions.</p><p>Refresh to try another transaction.</p>');
        //              } else {
        //                console.log(result);
        //                $('#checkout-message').html('<h1>Error</h1><p>Check your console.</p>');
        //              }
        //            });
        //          });
        //        });
        //      });
        //    </script>`;

        //    //let braintreeScipt = `<script> let button = document.querySelector('#braintree-btn'); braintree.dropin.create({authorization: 'sandbox_bzjwnpg3_k3fb9p88pvbfjfg9',container: '#dropin-container'}, function (createErr, instance) {button.addEventListener('click', function () {instance.requestPaymentMethod(function (err, payload) {});});});</script>`;
        //    $form.find('.braintree-row').prepend(braintreeScipt);
        //    //let $popup = FwPopup.renderPopup(jQuery(braintreeScipt), { ismodal: true });
        //    //FwPopup.showPopup($popup);
        //})
        // toggle buttons receipt tab
        FwFormField.loadItems($form.find('div[data-datafield="PaymentBy"]'), [
            { value: 'CUSTOMER', caption: 'Customer' },
            { value: 'DEAL', caption: 'Deal', checked: true },
        ]);
        // Adds receipt invoice or credit datatable to request
        $form.data('beforesave', request => {
            const invoiceRowHtml = $form.find('.invoice-row');
            const creditRowHtml = $form.find('.credits-row');

            if (invoiceRowHtml.attr('data-visible') === 'true') {
                request.InvoiceDataList = this.getFormTableData($form);
            } else if (creditRowHtml.attr('data-visible') === 'true') {
                request.CreditDataList = this.getCreditFormTableData($form);
            }
        });

        return $form;
    }
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
        let observer;
        // Listen for DOM element creation for Overpayment workflow for new Receipts
        if ($form.attr('data-mode') === 'NEW') {
            const app = document.getElementById('application');
            observer = new MutationObserver(() => {
                const message = jQuery(app).find('.advisory .fwconfirmationbox .body .message').text();
                if (message.startsWith("Amount to Apply exceeds the Invoice Amounts provided")) {
                    this.createOverPayment($form);
                }
                else if (message.startsWith("No Invoice Amounts have been provided")) {
                    this.createDepletingDeposit($form);
                }
            });
            // Start observing the target node for configured mutations
            observer.observe(app, { attributes: true, childList: true, subtree: true });
        }
        if (observer) {
            setTimeout(() => { observer.disconnect(); }, 3000)
        }
    }
    //----------------------------------------------------------------------------------------------
    deleteRecord($browse): void {
        FwModule.deleteRecord(this.Module, $browse);
        let observer;
        const app = document.getElementById('application');
        observer = new MutationObserver(() => {
            const message = jQuery(app).find('.advisory .fwconfirmationbox .body .message').text();
            if (message.startsWith("Receipt has already been exported.  Are you sure you want to delete?")) {
                this.overrideDelete($browse);
            }
        });
        observer.observe(app, { attributes: true, childList: true, subtree: true });

        $browse.data('onscreenunload', () => { observer.disconnect(); });
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: JQuery): void {
        //const $glDistributionGrid = $form.find('div[data-grid="GlDistributionGrid"]');
        //const $glDistributionGridControl = FwBrowse.loadGridFromTemplate('GlDistributionGrid');
        //$glDistributionGrid.empty().append($glDistributionGridControl);
        //$glDistributionGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        ReceiptId: FwFormField.getValueByDataField($form, 'ReceiptId')
        //    };
        //});
        //FwBrowse.init($glDistributionGridControl);
        //FwBrowse.renderRuntimeHtml($glDistributionGridControl);
        FwBrowse.renderGrid({
            nameGrid: 'GlDistributionGrid',
            gridSecurityId: '5xgHiF8dduf',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    ReceiptId: FwFormField.getValueByDataField($form, 'ReceiptId')
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
        //justin 10/25/2019 disabling this for now to avoid confusion. Terry was thinking he had to click the Make Payment button to save a Receipt
        //if (paymentTypeType === 'CREDIT CARD') {
        //    $form.find('.braintree-row').show();
        //} else {
        //    $form.find('.braintree-row').hide();
        //}

        if (paymentTypeType === 'DEPLETING DEPOSIT' || paymentTypeType === 'CREDIT MEMO' || paymentTypeType === 'OVERPAYMENT') {
            const paymentBy = FwFormField.getValueByDataField($form, 'PaymentBy');
            FwFormField.disable($form.find('div[data-datafield="PaymentTypeId"]'));
            FwFormField.disable($form.find('div[data-datafield="PaymentAmount"]'));
            $form.find('div[data-datafield="CheckNumber"]').hide();
            if (paymentBy === 'DEAL') {
                FwFormField.disable($form.find('div[data-validationname="DealCreditValidation"]'));
                $form.find('div[data-validationname="DealCreditValidation"]').show();
            } else {
                FwFormField.disable($form.find('div[data-validationname="CustomerCreditValidation"]'));
                $form.find('div[data-validationname="CustomerCreditValidation"]').show();
            }
        }
        // Click Event on tabs to load grids/browses
        this.paymentByRadioBehavior($form);
        if (paymentTypeType === 'REFUND CHECK') {
            this.loadReceiptCreditGrid($form);
        } else {
            this.loadReceiptInvoiceGrid($form);
        }
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
            //justin 10/25/2019 disabling this for now to avoid confusion. Terry was thinking he had to click the Make Payment button to save a Receipt
            //paymentTypeType === 'CREDIT CARD' ? $form.find('.braintree-row').show() : $form.find('.braintree-row').hide();

            let isOverDepletingMemo = false;
            if (paymentTypeType === 'DEPLETING DEPOSIT' || paymentTypeType === 'CREDIT MEMO' || paymentTypeType === 'OVERPAYMENT') {
                isOverDepletingMemo = true;
            }
            this.spendPaymentTypes($form, paymentTypeType, isOverDepletingMemo);
            if (paymentTypeType === 'REFUND CHECK') {
                this.loadReceiptCreditGrid($form);
            } else {
                this.loadReceiptInvoiceGrid($form);
            }

        });
        // ------
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
    spendPaymentTypes($form, paymentTypeType, isOverDepletingMemo) {
        const paymentBy = FwFormField.getValueByDataField($form, 'PaymentBy');

        if (isOverDepletingMemo) {
            $form.find('div[data-datafield="CheckNumber"]').hide();
            $form.find('div[data-datafield="CheckNumber"]').attr('data-required', 'false');

            if (paymentBy === 'DEAL') {
                $form.find('div[data-validationname="DealCreditValidation"]').show();
                $form.find('div[data-validationname="DealCreditValidation"]').attr('data-required', 'true').attr('data-enabled', 'true');
                $form.find('div[data-validationname="CustomerCreditValidation"]').hide();
                $form.find('div[data-validationname="CustomerCreditValidation"]').attr('data-required', 'false').attr('data-enabled', 'false');
            } else {
                $form.find('div[data-validationname="CustomerCreditValidation"]').show();
                $form.find('div[data-validationname="CustomerCreditValidation"]').attr('data-required', 'true').attr('data-enabled', 'true');
                $form.find('div[data-validationname="DealCreditValidation"]').hide();
                $form.find('div[data-validationname="DealCreditValidation"]').attr('data-required', 'false').attr('data-enabled', 'false');
            }

            $form.find('div[data-datafield="PaymentAmount"] .fwformfield-caption').text('Amount Remaining');
            FwFormField.disable($form.find('div[data-datafield="PaymentAmount"]'));

            if (paymentTypeType === 'DEPLETING DEPOSIT') {
                $form.find('div[data-datafield="OverPaymentId"] .fwformfield-caption').text('Deposit Reference');
            }
            if (paymentTypeType === 'CREDIT MEMO') {
                $form.find('div[data-datafield="OverPaymentId"] .fwformfield-caption').text('Credit Reference');
            }
            if (paymentTypeType === 'OVERPAYMENT') {
                $form.find('div[data-datafield="OverPaymentId"] .fwformfield-caption').text('Overpayment Reference');
            }
        }
        else {
            $form.find('.deal-cust-validate').hide();
            $form.find('.deal-cust-validate').attr('data-required', 'false');
            $form.find('.deal-cust-validate').attr('data-enabled', 'false');
            $form.find('div[data-datafield="CheckNumber"]').show();
            $form.find('div[data-datafield="CheckNumber"]').attr('data-required', 'true');

            $form.find('div[data-datafield="PaymentAmount"] .fwformfield-caption').text('Amount To Apply');
            FwFormField.enable($form.find('div[data-datafield="PaymentAmount"]'));
        }
        // Adust Amount Remaining field value for chosen receipt value
        $form.find('.deal-cust-validate').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="PaymentAmount"]', $tr.find('.field[data-formdatafield="Remaining"]').attr('data-originalvalue'));
            this.loadReceiptInvoiceGrid($form);
        });
    }
    //----------------------------------------------------------------------------------------------
    refundCheck($form: JQuery) {
        // hide invoice grid - disable algo
        // show credits grid
        // after save or if not NEW, disable credit grid
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        const validationName = request.module;
        const paymentTypeType = FwFormField.getValueByDataField($form, 'PaymentTypeType');
        const dealId = FwFormField.getValueByDataField($form, 'DealId');
        const customerId = FwFormField.getValueByDataField($form, 'CustomerId');
        let payType;
        if (paymentTypeType === 'DEPLETING DEPOSIT') {
            payType = 'D';
        }
        if (paymentTypeType === 'CREDIT MEMO') {
            payType = 'C';
        }
        if (paymentTypeType === 'OVERPAYMENT') {
            payType = 'O';
        }

        request.uniqueids = { RemainingOnly: true };

        switch (validationName) {
            case 'DealCreditValidation':
                if (payType !== "") {
                    request.uniqueids.RecType = payType;
                }
                if (dealId !== '') {
                    request.uniqueids.DealId = dealId;
                }
                break;
            case 'CustomerCreditValidation':
                if (payType !== "") {
                    request.uniqueids.RecType = payType;
                }
                if (customerId !== '') {
                    request.uniqueids.CustomerId = customerId;
                }
                break;
        };
    }
    //----------------------------------------------------------------------------------------------
    createOverPayment($form) {
        jQuery('#application').find('.advisory .fwconfirmationbox .fwconfirmation-button').click();

        const unappliedTotal = $form.find(`div[data-totalfield="UnappliedInvoiceTotal"] input`).val()
        const $confirmation = FwConfirmation.renderConfirmation(`Overpayment of ${unappliedTotal}`, '');
        $confirmation.find('.fwconfirmationbox').css('width', '490px');

        const html: Array<string> = [];
        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push(`    <div>Save this Receipt with ${unappliedTotal} Overpayment?</div>`);
        html.push('  </div>');
        html.push('</div>');

        FwConfirmation.addControls($confirmation, html.join(''));
        const $yes = FwConfirmation.addButton($confirmation, 'Save', false);
        const $no = FwConfirmation.addButton($confirmation, 'Cancel');
        $yes.focus();
        $yes.on('click', () => {
            FwConfirmation.destroyConfirmation($confirmation);
            // add datafield with flag for server
            FwFormField.setValueByDataField($form, 'CreateOverpayment', true);
            // automate save
            $form.find('.btn[data-type="SaveMenuBarButton"]').click();
        });
    }
    //----------------------------------------------------------------------------------------------
    createDepletingDeposit($form) {
        jQuery('#application').find('.advisory .fwconfirmationbox .fwconfirmation-button').click();

        const unappliedTotal = $form.find(`div[data-totalfield="UnappliedInvoiceTotal"] input`).val()
        const $confirmation = FwConfirmation.renderConfirmation(`Depleting Deposit of ${unappliedTotal}`, '');
        $confirmation.find('.fwconfirmationbox').css('width', '490px');

        const html: Array<string> = [];
        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push(`    <div>Create Depleting Deposit of ${unappliedTotal}?</div>`);
        html.push('  </div>');
        html.push('</div>');
        FwConfirmation.addControls($confirmation, html.join(''));
        const $yes = FwConfirmation.addButton($confirmation, 'Save', false);
        const $no = FwConfirmation.addButton($confirmation, 'Cancel');
        $yes.focus();
        $yes.on('click', () => {
            FwConfirmation.destroyConfirmation($confirmation);
            // add datafield with flag for server
            FwFormField.setValueByDataField($form, 'CreateDepletingDeposit', true);
            // automate save
            $form.find('.btn[data-type="SaveMenuBarButton"]').click();
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
        $form.find('.credits-row').hide();
        $form.find('.credits-row').attr('data-visible', 'false');
        $form.find('.invoice-row').show();
        $form.find('.invoice-row').attr('data-visible', 'true');

        if ($form.attr('data-mode') === 'NEW') {
            $form.find('.table-rows').html('<tr class="empty-row" style="height:33px;"><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>');
        }
        const calculateInvoiceTotals = ($form, event?) => {
            let amountValBefore;
            if (event != undefined) {
                const $this = jQuery(event.currentTarget);
                amountValBefore = $this.data('payAmountOnFocus');
                if (amountValBefore) {
                    amountValBefore = amountValBefore.replace(/,/g, '');
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
                            $appliedFields.eq(i).text(applied);
                            let due = dueLineTotal.toFixed(2);
                            due = due.replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
                            $dueFields.eq(i).text(due);
                            recurse = true;
                            break;
                        }
                    }
                    // Amount input field
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
            request.orderby = 'InvoiceDate,InvoiceNumber'
            const paymentBy = FwFormField.getValueByDataField($form, 'PaymentBy');
            if (paymentBy === 'DEAL') {
                request.uniqueids.DealId = FwFormField.getValueByDataField($form, 'DealId');
            } else if (paymentBy === 'CUSTOMER') {
                request.uniqueids.CustomerId = FwFormField.getValueByDataField($form, 'CustomerId');
            }

            FwAppData.apiMethod(true, 'POST', 'api/v1/receiptinvoice/browse', request, FwServices.defaultTimeout, res => {
                const rows = res.Rows;
                const htmlRows: Array<string> = [];
                if (rows.length) {
                    for (let i = 0; i < rows.length; i++) {
                        htmlRows.push(`<tr class="row"><td data-validationname="Deal" data-datafield="${rows[i][res.ColumnIndex.DealId]}" data-displayfield="${rows[i][res.ColumnIndex.Deal]}" class="text">${rows[i][res.ColumnIndex.Deal]}<i class="material-icons btnpeek">more_horiz</i></td><td class="text InvoiceId" style="display:none;">${rows[i][res.ColumnIndex.InvoiceId]}</td><td class="text InvoiceReceiptId" style="display:none;">${rows[i][res.ColumnIndex.InvoiceReceiptId]}</td><td data-validationname="Invoice" data-datafield="${rows[i][res.ColumnIndex.InvoiceId]}" data-displayfield="${rows[i][res.ColumnIndex.InvoiceNumber]}" class="text">${rows[i][res.ColumnIndex.InvoiceNumber]}<i class="material-icons btnpeek">more_horiz</i></td><td class="text">${rows[i][res.ColumnIndex.InvoiceDate]}</td><td data-validationname="Order" data-datafield="${rows[i][res.ColumnIndex.OrderId]}" data-displayfield="${rows[i][res.ColumnIndex.Description]}" class="text">${rows[i][res.ColumnIndex.OrderNumber]}<i class="material-icons btnpeek">more_horiz</i></td><td class="text">${rows[i][res.ColumnIndex.Description]}</td><td style="text-align:right;" data-invoicefield="InvoiceTotal" class="decimal static-amount">${rows[i][res.ColumnIndex.Total]}</td><td style="text-align:right;" data-invoicefield="InvoiceApplied" class="decimal static-amount">${rows[i][res.ColumnIndex.Applied]}</td><td style="text-align:right;" data-invoicefield="InvoiceDue" class="decimal static-amount">${rows[i][res.ColumnIndex.Due]}</td><td data-enabled="true" data-isuniqueid="false" data-datafield="InvoiceAmount" data-invoicefield="InvoiceAmount" class="decimal fwformfield pay-amount invoice-amount"><input class="decimal fwformfield fwformfield-value" style="font-size:inherit;" type="text" autocapitalize="none" row-index="${i}" value="${rows[i][res.ColumnIndex.Amount]}"></td><td><div class="fwformcontrol apply-btn" row-index="${i}" data-type="button" style="height:27px;padding:.3rem;line-height:13px;font-size:14px;">Apply All</div></td></tr>`);
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
        getInvoiceData($form);
    }
    //----------------------------------------------------------------------------------------------
    getFormTableData($form: JQuery): any {
        const invoiceRowHtml = $form.find('.invoice-row');
        if (invoiceRowHtml.attr('data-visible') === 'true') {

        }
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
    loadReceiptCreditGrid($form: JQuery): void {
        $form.find('.invoice-row').hide();
        $form.find('.invoice-row').attr('data-visible', 'false');
        $form.find('.credits-row').show();
        $form.find('.credits-row').attr('data-visible', 'true');
        let isNew = false;
        if ($form.attr('data-mode') === 'NEW') {
            $form.find('.credit-table-rows').html('<tr class="credit-empty-row" style="height:33px;"><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>');
            isNew = true;
        }
        const calculateInvoiceCreditTotals = ($form, event?) => {
            let amountValBefore;
            if (event != undefined) {
                const $this = jQuery(event.currentTarget);
                amountValBefore = $this.data('payAmountOnFocus');
                if (amountValBefore) {
                    amountValBefore = amountValBefore.replace(/,/g, '');
                    //console.log('amountValBeforeinTOTAL', amountValBefore)
                }
            }

            let remainingTotal = new Decimal(0);
            let amountTotal = new Decimal(0);
            let recurse = false;

            const $remainingFields = $form.find('td[data-creditfield="CreditRemaining"]');
            const $amountFields = $form.find('td[data-creditfield="CreditAmount"] input');
            const amountToApply = FwFormField.getValueByDataField($form, 'PaymentAmount').replace(/,/g, '');
            let unappliedTotalPrior = $form.find(`div[data-totalfield="UnappliedCreditTotal"] input`).val().replace(/[$ ,]+/g, "").trim();
            if (unappliedTotalPrior === '') { unappliedTotalPrior = '0.00'; }
            for (let i = 0; i < $amountFields.length; i++) {
                // ----- Bottom line totaling
                let amountValOnLine = $amountFields.eq(i).val().replace(/,/g, '');
                if (amountValOnLine === '') { amountValOnLine = '0.00'; } // possibly unecessary
                // Amount Column
                amountTotal = amountTotal.plus(amountValOnLine);
                // Remaining Column
                let remainingValOnLine = $remainingFields.eq(i).text().replace(/,/g, '');
                remainingTotal = remainingTotal.plus(remainingValOnLine);

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
                            let remainingTotal = new Decimal(0);
                            const remainingValOnLine = $remainingFields.eq(i).text().replace(/,/g, '');
                            remainingTotal = remainingTotal.plus(remainingValOnLine);
                            let unappliedTotalPriorDecimal = new Decimal(0);
                            unappliedTotalPriorDecimal = unappliedTotalPriorDecimal.plus(unappliedTotalPrior);
                            let amountVal;

                            //console.log('unappliedTotalPrior', unappliedTotalPrior)
                            //console.log('amountInput', amountInput)
                            //console.log('amountTotal', amountTotal)
                            //console.log('remainingValOnLine', remainingValOnLine)
                            //console.log('remainingTotal', remainingTotal)
                            //console.log('unappliedTotalPriorDecimal', unappliedTotalPriorDecimal)

                            // If Unapplied Amount >= "Due"  increase the "Amount" value by the "Due" value on the line
                            if (unappliedTotalPriorDecimal.greaterThanOrEqualTo(remainingTotal)) {
                                amountVal = remainingTotal.plus(amountTotal);
                                //console.log('amountVal', amountVal);

                                $amountFields.eq(i).val(amountVal.toFixed(2));
                            }
                            // If Unapplied Amount < "Due"  increase the "Amount" value by the Unapplied Amount value on the line
                            if (unappliedTotalPriorDecimal.lessThan(remainingTotal)) {
                                amountVal = amountTotal.plus(unappliedTotalPriorDecimal);
                                //console.log('amountVal', amountVal);

                                $amountFields.eq(i).val(amountVal.toFixed(2));
                            }
                            let amountDifference = new Decimal(0);
                            amountDifference = amountVal.minus(amountTotal);

                            let remainingLineTotal = new Decimal(0);
                            remainingLineTotal = remainingLineTotal.plus(remainingValOnLine).minus(amountDifference);

                            let remaining = remainingLineTotal.toFixed(2);
                            remaining = remaining.replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
                            $remainingFields.eq(i).text(remaining);
                            recurse = true;
                            break;
                        }
                    }
                    // Amount input field
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

                        let remainingLineTotal = new Decimal(0);
                        remainingLineTotal = remainingLineTotal.plus(remainingValOnLine).minus(amountDifference);
                        let remaining = remainingLineTotal.toFixed(2);
                        remaining = remaining.replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
                        $remainingFields.eq(i).text(remaining);
                        recurse = true;
                        break;
                    }
                }
            }
            if (recurse) {
                calculateInvoiceCreditTotals($form);
                return;
            }
            const amount: any = amountTotal.toFixed(2);
            const unappliedTotal = amountToApply - amount;

            $form.find(`div[data-totalfield="UnappliedCreditTotal"] input`).val(unappliedTotal);
            $form.find(`div[data-totalfield="CreditAmountTotal"] input`).val(amount);
        }
        const getInvoiceCreditData = ($form) => {
            const request: any = {};
            const officeLocationId = JSON.parse(sessionStorage.getItem('location')).locationid;
            const receiptId = FwFormField.getValueByDataField($form, 'ReceiptId');
            request.uniqueids = {
                LocationId: officeLocationId,
                ReceiptId: receiptId,
            }

            const paymentBy = FwFormField.getValueByDataField($form, 'PaymentBy');
            let validationName;
            if (paymentBy === 'DEAL') {
                request.uniqueids.DealId = FwFormField.getValueByDataField($form, 'DealId');
                validationName = 'DealCredit';
            } else if (paymentBy === 'CUSTOMER') {
                request.uniqueids.CustomerId = FwFormField.getValueByDataField($form, 'CustomerId');
                validationName = 'CustomerCredit';
            }
            request.orderby = 'ReceiptDate,CheckNumber'

            FwAppData.apiMethod(true, 'POST', `api/v1/receiptcredit/browse`, request, FwServices.defaultTimeout, res => {

                const rows = res.Rows;
                console.log('ROWS', rows)
                const htmlRows: Array<string> = [];
                if (rows.length) {
                    for (let i = 0; i < rows.length; i++) {

                        let buttonPeek;
                        const isWebAdmin = JSON.parse(sessionStorage.getItem('userid')).webadministrator;
                        const isHomeModule = (FwValidation.homeModules.indexOf('PaymentType') > -1); // could be used later on to be more exact for other peek in the "grid"
                        if (isWebAdmin === 'true') {
                            buttonPeek = '<i class="material-icons btnpeek">more_horiz</i>';
                        }
                        else if (isWebAdmin === 'false') {
                            buttonPeek = '';
                        }
                        htmlRows.push(`<tr class="row"><td class="text">${rows[i][res.ColumnIndex.ReceiptDate]}</td><td data-validationname="Deal" data-datafield="${rows[i][res.ColumnIndex.DealId]}" data-displayfield="${rows[i][res.ColumnIndex.Deal]}" class="text">${rows[i][res.ColumnIndex.Deal]}<i class="material-icons btnpeek">more_horiz</i></td><td class="text" data-creditfield="ReceiptId" style="display:none;">${rows[i][res.ColumnIndex.ReceiptId]}</td><td class="text" data-creditfield="CreditReceiptId" style="display:none;">${rows[i][res.ColumnIndex.CreditReceiptId]}</td><td data-validationname="PaymentType" data-datafield="${rows[i][res.ColumnIndex.PaymentTypeId]}" data-displayfield="${rows[i][res.ColumnIndex.PaymentType]}" class="text">${rows[i][res.ColumnIndex.PaymentType]}${buttonPeek}</td><td data-validationname="${validationName}" data-datafield="${rows[i][res.ColumnIndex.ReceiptId]}" data-displayfield="${rows[i][res.ColumnIndex.CheckNumber]}" class="text"><span style="padding: 0px 2px 1px 2px;border:1px solid black;border-radius:2px;background-color:${rows[i][res.ColumnIndex.RecTypeColor]};">${rows[i][res.ColumnIndex.CheckNumber]}</span><i class="material-icons btnpeek">more_horiz</i></td><td style="text-align:right;" data-creditfield="CreditRemaining" class="decimal">${rows[i][res.ColumnIndex.Remaining]}</td><td data-enabled="true" data-isuniqueid="false" data-datafield="CreditAmount" data-creditfield="CreditAmount" class="decimal fwformfield"><input class="decimal fwformfield fwformfield-value" style="font-size:inherit;" type="text" autocapitalize="none" row-index="${i}" value="${isNew ? "0.00" : rows[i][res.ColumnIndex.Amount]}"></td><td><div class="fwformcontrol credit-apply-btn" row-index="${i}" data-type="button" style="height:27px;padding:.3rem;line-height:13px;font-size:14px;">Apply All</div></td></tr>`);
                    }


                    $form.find('.credit-table-rows').html('');
                    $form.find('.credit-table-rows').html(htmlRows.join(''));
                    $form.find('[data-creditfield="CreditAmount"] input').inputmask({ alias: "currency", prefix: '' });
                    $form.find('[data-creditfield="CreditRemaining"]:not(input)').inputmask({ alias: "currency", prefix: '' });

                    (function () {
                        const $amountFields = $form.find('[data-creditfield="CreditAmount"] input');
                        for (let i = 0; i < $amountFields.length; i++) {
                            const amount: any = $amountFields.eq(i).val();
                            if (amount === '0.00' || amount === '') {
                                $amountFields.eq(i).css('background-color', 'white');
                            } else {
                                $amountFields.eq(i).css('background-color', '#F4FFCC');
                            }
                        }
                        calculateInvoiceCreditTotals($form);
                    })();
                    // Amount column listener
                    $form.find('[data-creditfield="CreditAmount"] input').on('change', ev => {
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
                        calculateInvoiceCreditTotals($form, ev);
                        el.data('payAmountOnFocus', val); // reset line before total in case user doesnt leave input and changes again
                        //console.log('payAmountOnChange', el.data('payAmountOnFocus'))
                    });
                    // Store intial amount value for calculations after change
                    $form.find('[data-creditfield="CreditAmount"] input').on('focus', ev => {
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
                        calculateInvoiceCreditTotals($form);
                    });

                    $form.find('.credit-apply-btn').click((ev: JQuery.ClickEvent) => {
                        calculateInvoiceCreditTotals($form, ev);
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
        getInvoiceCreditData($form);
    }
    //----------------------------------------------------------------------------------------------
    getCreditFormTableData($form: JQuery): any {
        const $creditReceiptIds = $form.find('[data-creditfield="CreditReceiptId"]');
        const $receiptIds = $form.find('[data-creditfield="ReceiptId"]');

        const $amountFields = $form.find('[data-creditfield="CreditAmount"] input');
        const CreditDataList: any = [];
        for (let i = 0; i < $receiptIds.length; i++) {
            const creditId = $receiptIds.eq(i).text();
            const receiptCreditId = $creditReceiptIds.eq(i).text();
            let amount: any = $amountFields.eq(i).val();
            amount = amount.replace(/,/g, '');

            const fields: any = {}
            if (receiptCreditId !== '') {
                fields.CreditReceiptId = receiptCreditId;
            }
            fields.CreditId = creditId;
            fields.Amount = +amount;
            CreditDataList.push(fields);
        }

        return CreditDataList;
    }
    //----------------------------------------------------------------------------------------------
    paymentByRadioBehavior($form: JQuery): void {
        const paymentTypeType = FwFormField.getValueByDataField($form, 'PaymentTypeType');

        let isOverDepletingMemo = false;
        if (paymentTypeType === 'DEPLETING DEPOSIT' || paymentTypeType === 'CREDIT MEMO' || paymentTypeType === 'OVERPAYMENT') {
            isOverDepletingMemo = true;
        }
        if (FwFormField.getValueByDataField($form, 'PaymentBy') === 'DEAL') {
            $form.find('div[data-datafield="CustomerId"]').hide();
            $form.find('div[data-datafield="CustomerId"]').attr('data-required', 'false');
            FwFormField.setValueByDataField($form, 'CustomerId', '');
            $form.find('div[data-datafield="DealId"]').show();
            $form.find('div[data-datafield="DealId"]').attr('data-required', 'true');
            if (isOverDepletingMemo && ($form.attr('data-mode') === 'NEW')) {
                FwFormField.setValue($form, '.deal-cust-validate', '');
                $form.find('div[data-validationname="DealCreditValidation"]').show();
                $form.find('div[data-validationname="DealCreditValidation"]').attr('data-required', 'true').attr('data-enabled', 'true');
                $form.find('div[data-validationname="CustomerCreditValidation"]').hide();
                $form.find('div[data-validationname="CustomerCreditValidation"]').attr('data-required', 'false').attr('data-enabled', 'false');
            }

        } else {
            $form.find('div[data-datafield="DealId"]').hide();
            $form.find('div[data-datafield="DealId"]').attr('data-required', 'false');
            FwFormField.setValueByDataField($form, 'DealId', '');
            $form.find('div[data-datafield="CustomerId"]').show();
            $form.find('div[data-datafield="CustomerId"]').attr('data-required', 'true');
            if (isOverDepletingMemo && ($form.attr('data-mode') === 'NEW')) {
                FwFormField.setValue($form, '.deal-cust-validate', '');
                $form.find('div[data-validationname="CustomerCreditValidation"]').show();
                $form.find('div[data-validationname="CustomerCreditValidation"]').attr('data-required', 'true').attr('data-enabled', 'true');
                $form.find('div[data-validationname="DealCreditValidation"]').hide();
                $form.find('div[data-validationname="DealCreditValidation"]').attr('data-required', 'false').attr('data-enabled', 'false');
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    overrideDelete($browse) {
        jQuery('#application').find('.advisory .fwconfirmationbox .fwconfirmation-button').click();

        const $confirmation = FwConfirmation.renderConfirmation(`Delete Receipt`, '');
        $confirmation.find('.fwconfirmationbox').css('width', '490px');

        const html: Array<string> = [];
        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push(`    <div>Receipt has already been exported.  Are you sure you want to delete?</div>`);
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
            FwAppData.apiMethod(true, 'DELETE', `api/v1/receipt/overridedelete/${id.ReceiptId}`, null, FwServices.defaultTimeout,
                res => {
                    FwBrowse.databind($browse);
                }
                , ex => FwFunc.showError(ex), $browse);
        });
    }
    //----------------------------------------------------------------------------------------------
}
//----------------------------------------------------------------------------------------------
var ReceiptController = new Receipt();