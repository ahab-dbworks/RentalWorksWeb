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
        }


        this.events($form);

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
    }
    //----------------------------------------------------------------------------------------------
    events($form: JQuery): void {
        $form.find('div[data-datafield="PaymentBy"]').change(() => {
            this.paymentByRadioBehavior($form);
        })
    }
    loadReceiptInvoiceGrid($form: JQuery) {
        let request: any = {};
        let officeLocationId = JSON.parse(sessionStorage.getItem('location')).locationid;
        let receiptId = FwFormField.getValueByDataField($form, 'ReceiptId');
        let receiptDate = FwFormField.getValueByDataField($form, 'ReceiptDate');
        let dealId = FwFormField.getValueByDataField($form, 'DealId');
        let customerId = FwFormField.getValueByDataField($form, 'CustomerId');

        request.uniqueids = {
            OfficeLocationId: officeLocationId,
            ReceiptId: receiptId,
            ReceiptDate: receiptDate,
            DealId: dealId
        }
        FwAppData.apiMethod(true, 'POST', 'api/v1/receiptinvoice/browse', request, FwServices.defaultTimeout, function onSuccess(res) {
            console.log('res', res.Columns)
            let rows = res.Rows;
            let html: Array<string> = [];
            for (let i = 0; i < rows.length; i++) {
                html.push(`<tr class="row"><td class="text">${rows[i][8]}</td><td class="text">${rows[i][1]}</td><td class="text">${rows[i][2]}</td><td class="text">${rows[i][6]}</td><td class="text">${rows[i][3]}</td><td class="decimal">${rows[i][9]}</td><td class="decimal">${rows[i][12]}</td><td class="decimal">${rows[i][14]}</td><td class="decimal"><input class="decimal" type="number" value="${rows[i][13]}"></td></tr>`)
            }
            let formTable = jQuery(html.join(''))
            $form.find('.form-table').append(formTable);
        }, null, null);
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