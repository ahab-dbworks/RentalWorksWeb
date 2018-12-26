routes.push({ pattern: /^module\/receipt$/, action: function (match: RegExpExecArray) { return ReceiptController.getModuleScreen(); } });
routes.push({ pattern: /^module\/receipt\/(\S+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { 'datafield': match[1], 'search': match[2].replace(/%20/g, ' ').replace(/%2f/g, '/') }; return ReceiptController.getModuleScreen(filter); } });

class Receipt {
    Module: string = 'Receipt';
    apiurl: string = 'api/v1/receipt';
    caption: string = 'Receipts';
    nav: string = 'module/receipt';
    id: string = '57E34535-1B9F-4223-AD82-981CA34A6DEC';
    ActiveView: string = 'ALL';
    thisModule: Receipt;
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: { datafield: string, search: string }) {
        var self = this;
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $browse: any = this.openBrowse();
        const today = FwFunc.getDate();

        screen.load = function () {
            FwModule.openModuleTab($browse, self.caption, false, 'BROWSE', true);

            if (typeof filter !== 'undefined') {
                var datafields = filter.datafield.split('%20');
                for (var i = 0; i < datafields.length; i++) {
                    datafields[i] = datafields[i].charAt(0).toUpperCase() + datafields[i].substr(1);
                }
                filter.datafield = datafields.join('')
                $browse.find('div[data-browsedatafield="' + filter.datafield + '"]').find('input').val(filter.search);
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

        let location = JSON.parse(sessionStorage.getItem('location'));
        this.ActiveView = 'LocationId=' + location.locationid;

        $browse.data('ondatabind', request => {
            request.activeview = this.ActiveView;
        });

        FwBrowse.addLegend($browse, 'Overpayment', '#80FFFF');
        FwBrowse.addLegend($browse, 'Depleting Deposit', '#37D303'); // no color in res
        FwBrowse.addLegend($browse, 'Refund Check', '#FF8888');
        FwBrowse.addLegend($browse, 'NSF Adjustment', '#6F6FFF');
        FwBrowse.addLegend($browse, 'Write Off', '#D6E180'); // no color in res
        FwBrowse.addLegend($browse, 'Credit Memo', '#D6ABAB');

        return $browse;
    }
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
    openForm(mode: string, parentModuleInfo?: any) {
        var $form: any = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            const usersid = sessionStorage.getItem('usersid');  // J. Pace 7/09/18  C4E0E7F6-3B1C-4037-A50C-9825EDB47F44
            const name = sessionStorage.getItem('name');
            const today = FwFunc.getDate();

            FwFormField.setValueByDataField($form, 'ReceiptDate', today);
            FwFormField.enable($form.find('div[data-datafield="PaymentBy"]'));
            FwFormField.enable($form.find('div[data-datafield="DealId"]'));
            FwFormField.enable($form.find('div[data-datafield="CustomerId"]'));
            FwFormField.setValue($form, 'div[data-datafield="AppliedById"]', usersid, name);
            $form.find('.deal-customer').change(() => {
                this.loadReceiptInvoiceGrid($form);
            });
            this.events($form);
        }
        // Adds receipt invoice datatable to request
        $form.data('beforesave', request => {
            request.InvoiceDataList = this.getFormTableData($form);
        });

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form: any = this.openForm('EDIT');
        FwFormField.setValueByDataField($form, 'ReceiptId', uniqueids.ReceiptId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
        FwFormField.disable($form.find('div[data-datafield="PaymentBy"]'));
        FwFormField.disable($form.find('div[data-datafield="DealId"]'));
        FwFormField.disable($form.find('div[data-datafield="CustomerId"]'));
    }
    //----------------------------------------------------------------------------------------------
    loadAudit($form: any) {
        var uniqueid: string = FwFormField.getValueByDataField($form, 'ReceiptId');
        FwModule.loadAudit($form, uniqueid);
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: JQuery): void {

        // ----------
        
        // ----------


        // ----------


        // ----------

    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: JQuery): void {
        // let $customerResaleGrid: any = $form.find('[data-name="CompanyResaleGrid"]');
        // FwBrowse.search($customerResaleGrid);

        // Click Event on tabs to load grids/browses
        $form.on('click', '[data-type="tab"]', e => {
            let tabname = jQuery(e.currentTarget).attr('id');
            let lastIndexOfTab = tabname.lastIndexOf('tab');
            let tabpage = tabname.substring(0, lastIndexOfTab) + 'tabpage' + tabname.substring(lastIndexOfTab + 3);

            let $gridControls = $form.find(`#${tabpage} [data-type="Grid"]`);
            if ($gridControls.length > 0) {
                for (let i = 0; i < $gridControls.length; i++) {
                    let $gridcontrol = jQuery($gridControls[i]);
                    FwBrowse.search($gridcontrol);
                }
            }

            let $browseControls = $form.find(`#${tabpage} [data-type="Browse"]`);
            if ($browseControls.length > 0) {
                for (let i = 0; i < $browseControls.length; i++) {
                    let $browseControl = jQuery($browseControls[i]);
                    FwBrowse.search($browseControl);
                }
            }
        });
        this.paymentByRadioBehavior($form);
        this.loadReceiptInvoiceGrid($form);
        this.events($form);
    }
    //----------------------------------------------------------------------------------------------
    events($form: JQuery): void {
        $form.find('div[data-datafield="PaymentBy"]').change(() => {
            this.paymentByRadioBehavior($form);
        });
 
    }
    //----------------------------------------------------------------------------------------------
    loadReceiptInvoiceGrid($form: JQuery): void {
        // called every save since in afterload but only refresh grid in if NEW
        // currently only loads for first time but needs to distinguish if new and after
        // shade amount if not 0
        // add peek?
        if (!$form.data('formtable')) {
            let request: any = {};
            let officeLocationId = JSON.parse(sessionStorage.getItem('location')).locationid;
            let receiptId = FwFormField.getValueByDataField($form, 'ReceiptId');
            let receiptDate = FwFormField.getValueByDataField($form, 'ReceiptDate');
            let dealCustomer = FwFormField.getValue($form, '.deal-customer:visible'); // send visible field

            request.uniqueids = {
                OfficeLocationId: officeLocationId,
                ReceiptId: receiptId,
                ReceiptDate: receiptDate,
                DealId: dealCustomer
            }
            FwAppData.apiMethod(true, 'POST', 'api/v1/receiptinvoice/browse', request, FwServices.defaultTimeout, function onSuccess(res) {
                let rows = res.Rows;
                let htmlRows: Array<string> = [];
                if (rows.length) {
                    for (let i = 0; i < rows.length; i++) {
                        htmlRows.push(`<tr class="row"><td class="text">${rows[i][res.ColumnIndex.Deal]}<i class="material-icons btnpeek">more_horiz</i></td><td class="text InvoiceId" style="display:none;">${rows[i][res.ColumnIndex.InvoiceId]}</td><td class="text">${rows[i][res.ColumnIndex.InvoiceNumber]}</td><td class="text">${rows[i][res.ColumnIndex.InvoiceDate]}</td><td class="text">${rows[i][res.ColumnIndex.OrderNumber]}</td>
                                       <td class="text">${rows[i][res.ColumnIndex.Description]}</td><td style="text-align:right;" class="decimal static-amount">${rows[i][res.ColumnIndex.Total]}</td><td style="text-align:right;" class="decimal static-amount">${rows[i][res.ColumnIndex.Applied]}</td><td style="text-align:right;" class="decimal static-amount">${rows[i][res.ColumnIndex.Due]}</td>
                                       <td class="decimal invoice-amount"><input class="decimal fwformfield-value" style="font-size:inherit" type="text" autocapitalize="none" value="${rows[i][res.ColumnIndex.Amount]}"></td></tr>`
                                      );
                    }
                    $form.find('.table-rows').html(htmlRows.join(''));
                    $form.find('.invoice-amount input').inputmask({ alias: "currency", prefix: '' });
                    $form.find('.static-amount:not(input)').inputmask({ alias: "currency", prefix: '' });
                    $form.find('.table-rows input').eq(0).focus();
                    $form.find('.invoice-amount input').on('change', e => {
                        let $tab, $tabpage;
                        e.stopPropagation();

                        $tabpage = $form.parent();
                        $tab = jQuery('#' + $tabpage.attr('data-tabid'));
                        $tab.find('.modified').html('*');
                        $form.attr('data-modified', 'true');
                        $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
                    });

                    $form.data('formtable', true);
                } 
            }, null, $form);
        }
    }
    //----------------------------------------------------------------------------------------------
    getFormTableData($form: JQuery) {
        let $invoiceIdFields = $form.find('.InvoiceId');
        let $amountFields = $form.find('.invoice-amount input');
        let InvoiceDataList: any = [];
        for (let i = 0; i < $invoiceIdFields.length; i++) {
            let fields: any = {}
            let invoiceId = $invoiceIdFields.eq(i).text();
            let amount: any = $amountFields.eq(i).val();
            amount = amount.replace(/,/g, '');
           
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