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

        screen.load = function () {
            FwModule.openModuleTab($browse, self.caption, false, 'BROWSE', true);

            if (typeof filter !== 'undefined') {
                var datafields = filter.datafield.split('%20');
                for (var i = 0; i < datafields.length; i++) {
                    datafields[i] = datafields[i].charAt(0).toUpperCase() + datafields[i].substr(1);
                }
                filter.datafield = datafields.join('')
                $browse.find('div[data-browsedatafield="' + filter.datafield + '"]').find('input').val(filter.search);
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
    }
    //----------------------------------------------------------------------------------------------
    loadAudit($form: any) {
        var uniqueid: string = FwFormField.getValueByDataField($form, 'ReceiptId');
        FwModule.loadAudit($form, uniqueid);
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any): void {
        // ----------


        // ----------


        // ----------


        // ----------

    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any): void {
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
    }
    //----------------------------------------------------------------------------------------------
    events($form: JQuery): void {

    }
}
//----------------------------------------------------------------------------------------------
var ReceiptController = new Receipt();