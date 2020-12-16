routes.push({ pattern: /^module\/dealcredit$/, action: function (match: RegExpExecArray) { return DealCreditController.getModuleScreen(); } });

class DealCredit {
    Module: string = 'DealCredit';
    apiurl: string = 'api/v1/dealcredit';
    caption: string = Constants.Modules.Home.children.DealCredit.caption;
	nav: string = Constants.Modules.Home.children.DealCredit.nav;
	id: string = Constants.Modules.Home.children.DealCredit.id;
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    //---------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        options.hasInactive = false;
        FwMenu.addBrowseMenuButtons(options);

        // Type
        const $all = FwMenu.generateDropDownViewBtn('ALL', true, "ALL");
        const $depletingdeposits = FwMenu.generateDropDownViewBtn('Depleting Deposits', false, "D");
        const $creditmemos = FwMenu.generateDropDownViewBtn('Credit Memos', false, "C");
        const $overpayments = FwMenu.generateDropDownViewBtn('Overpayments', false, "O");

        const viewType: Array<JQuery> = [];
        viewType.push($all, $depletingdeposits, $creditmemos, $overpayments);
        FwMenu.addViewBtn(options.$menu, 'Type', viewType, true, "RecType");

        // Status
        const $allStatus = FwMenu.generateDropDownViewBtn('ALL', true, "ALL");
        const $amountRemaining = FwMenu.generateDropDownViewBtn('Amount Remaining', false, "R"); // needs value here

        const viewStatus: Array<JQuery> = [];
        viewStatus.push($allStatus, $amountRemaining);
        FwMenu.addViewBtn(options.$menu, 'Status', viewStatus, true, "Status");

        // Browse SubMenu
        // Options Group
        FwMenu.addSubMenuItem(options.$colOptions, 'Refund Remaining Amount', 'aVvPPeYAmQEN', (e) => {
            try {
                const $form = options.$browse.closest('.fwform');
                const controllerName = $form.attr('data-controller');
                const request: RequestRefund = {};

                if ($form.length > 0) {
                    const $tr = FwBrowse.getSelectedRow(options.$browse);
                    if ($tr.length > 0) {
                        request.receiptId = FwBrowse.getValueByDataField(options.$browse, $tr, 'ReceiptId');
                    }
                    if (request.receiptId === '') {
                        throw new Error('Please select a credit to refund!');
                    }
                    request.amount = parseFloat(FwBrowse.getValueByDataField(options.$browse, $tr, 'Amount'));
                    request.applied = parseFloat(FwBrowse.getValueByDataField(options.$browse, $tr, 'Applied'));
                    request.refund = parseFloat(FwBrowse.getValueByDataField(options.$browse, $tr, 'Refunded'));
                    request.remaining = parseFloat(FwBrowse.getValueByDataField(options.$browse, $tr, 'Remaining'));
                    
                    if (request.remaining <= 0.0) {
                        throw new Error('No remaining balace to refund.');
                    }
                    //need an object
                    if (controllerName === 'OrderController') {
                        request.orderId = FwFormField.getValueByDataField($form, 'OrderId');
                        request.orderDescription = FwFormField.getTextByDataField($form, 'Description');
                        request.dealId = FwFormField.getValueByDataField($form, 'DealId');
                        request.deal = FwFormField.getTextByDataField($form, 'DealId');
                    }
                    else if (controllerName === 'DealController') {
                        request.dealId = FwFormField.getValueByDataField($form, 'DealId');
                        request.deal = FwFormField.getValueByDataField($form, 'Deal');
                        request.customerId = FwFormField.getValueByDataField($form, 'CustomerId');
                        request.customer = FwFormField.getValueByDataField($form, 'CustomerId');
                    }
                    else {
                        throw new Error(`Refund is not implemented for: ${controllerName}`);
                    }
                }
                this.refund(options.$browse, request);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    };
    //---------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $browse: JQuery = this.openBrowse();

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
    //---------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = jQuery(this.getBrowseTemplate());
        $browse = FwModule.openBrowse($browse);

        $browse.data('ondatabind', request => {
            request.activeviewfields = this.ActiveViewFields;
        });

        FwBrowse.addLegend($browse, 'Credit Memo', '#ABABD6');
        FwBrowse.addLegend($browse, 'Depleting Deposit', '#37D303');
        FwBrowse.addLegend($browse, 'Overpayment', '#FFFF80');
        FwBrowse.addLegend($browse, 'Refund Check', '#6F6FFF');

        return $browse;
    };
    //---------------------------------------------------------------------------------------------
    openForm(mode: string) {
    };
    //---------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
    };
    //---------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
    };
    //---------------------------------------------------------------------------------------------
    loadAudit($form: JQuery) {
    };
    //---------------------------------------------------------------------------------------------
    getBrowseTemplate(): string {
        return `
           <div data-name="DealCredit" data-control="FwBrowse" data-type="Browse" id="DealCreditBrowse" class="fwcontrol fwbrowse" data-orderby="" data-controller="DealCreditController">
            <div class="column flexcolumn" data-width="0" data-visible="false">
               <div class="field" data-isuniqueid="true" data-datafield="ReceiptId" data-browsedatatype="key"></div>
             </div>
             <div class="column flexcolumn" max-width="150px" data-visible="true">
               <div class="field" data-caption="Date" data-datafield="ReceiptDate" data-browsedatatype="date" data-sort="desc"></div>
             </div>
             <div class="column flexcolumn" max-width="250px" data-visible="true">
               <div class="field" data-caption="Customer" data-datafield="Customer" data-browsedatatype="text" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" max-width="250px" data-visible="true">
               <div class="field" data-caption="Deal" data-datafield="Deal" data-browsedatatype="text" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" max-width="150px" data-visible="true">
               <div class="field" data-caption="Payment Type" data-datafield="PaymentType" data-cellcolor="RecTypeColor" data-browsedatatype="text" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" max-width="150px" data-visible="true">
               <div class="field" data-caption="Ref/Check" data-datafield="CheckNumber" data-browsedatatype="text" data-sort="desc"></div>
             </div>
             <div class="column flexcolumn" max-width="150px" data-visible="true">
               <div class="field" data-caption="Amount" data-datafield="Amount" data-browsedatatype="money" data-currencysymbol="CurrencySymbol" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" max-width="150px" data-visible="true">
               <div class="field" data-caption="Applied" data-datafield="Applied" data-browsedatatype="money" data-currencysymbol="CurrencySymbol" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" max-width="150px" data-visible="true">
               <div class="field" data-caption="Refunded" data-datafield="Refunded" data-browsedatatype="money" data-currencysymbol="CurrencySymbol" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" max-width="150px" data-visible="true">
               <div class="field" data-caption="Remaining" data-datafield="Remaining" data-browsedatatype="money" data-currencysymbol="CurrencySymbol" data-sort="off"></div>
             </div>
             <div class="column flexcolumn" data-width="0" data-visible="false">
               <div class="field" data-datafield="Inactive" data-browsedatatype="text" data-visible="false"></div>
             </div>
             <div class="column spacer" data-width="auto" data-visible="true"></div>
           </div>`;
    }
    //----------------------------------------------------------------------------------------------
    refund($browse: JQuery, request: RequestRefund) {
        try {
            Refund.showRefundPopup($browse, request);
        } catch (ex) {
            FwFunc.showError(ex);
        }

    }

    //---------------------------------------------------------------------------------------------
}

var DealCreditController = new DealCredit();

type RequestRefund = {
    receiptId?: string;
    orderId?: string;
    orderDescription?: string;
    dealId?: string;
    deal?: string;
    customerId?: string;
    customer?: string;
    amount?: number;
    applied?: number;
    refund?: number;
    remaining?: number;
}  
