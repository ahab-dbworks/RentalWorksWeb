declare var FwModule: any;
declare var FwBrowse: any;

class Quote {
    Module: string;
    apiurl: string;
    ActiveView: string;

    constructor() {
        this.Module = 'Quote';
        this.apiurl = 'api/v1/quote';
        this.ActiveView = 'ALL';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Quote', false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }

    openBrowse() {
        var self = this;
        var $browse;

        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);
        FwBrowse.init($browse);

        self.ActiveView = 'ALL'; 

        $browse.data('ondatabind', function (request) {
            request.activeview = self.ActiveView;
        });
        FwBrowse.addLegend($browse, 'Prospect', '#ffffff');
        FwBrowse.addLegend($browse, 'Active', '#fffa00');
        FwBrowse.addLegend($browse, 'Reserved', '#0080ff');
        FwBrowse.addLegend($browse, 'Ordered', '#00c400');
        FwBrowse.addLegend($browse, 'Cancelled', '#ff0080');
        FwBrowse.addLegend($browse, 'Closed', '#ff8040');

        return $browse;
    }

    addBrowseMenuItems($menuObject: any) {
        var self = this;
        var $all: JQuery = FwMenu.generateDropDownViewBtn('All', true);
        var $prospect: JQuery = FwMenu.generateDropDownViewBtn('Prospect', true);
        var $active: JQuery = FwMenu.generateDropDownViewBtn('Active', false);
        var $reserved: JQuery = FwMenu.generateDropDownViewBtn('Reserved', false);
        var $ordered: JQuery = FwMenu.generateDropDownViewBtn('Ordered', false);
        var $cancelled: JQuery = FwMenu.generateDropDownViewBtn('Cancelled', false);
        var $closed: JQuery = FwMenu.generateDropDownViewBtn('Closed', false);

        $all.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ALL';
            FwBrowse.setPageNo($browse, 1);
            FwBrowse.databind($browse);
        });
        $prospect.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'PROSPECT';
            FwBrowse.setPageNo($browse, 1);
            FwBrowse.databind($browse);
        });
        $active.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ACTIVE';
            FwBrowse.setPageNo($browse, 1);
            FwBrowse.databind($browse);
        });
        $reserved.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'RESERVED';
            FwBrowse.setPageNo($browse, 1);
            FwBrowse.databind($browse);
        });
        $ordered.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ORDERED';
            FwBrowse.setPageNo($browse, 1);
            FwBrowse.databind($browse);
        });
        $cancelled.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'CANCELLED';
            FwBrowse.setPageNo($browse, 1);
            FwBrowse.databind($browse);
        });
        $closed.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'CLOSED';
            FwBrowse.setPageNo($browse, 1);
            FwBrowse.databind($browse);
        });

        FwMenu.addVerticleSeparator($menuObject);

        var viewSubitems: Array<JQuery> = [];
        viewSubitems.push($all);
        viewSubitems.push($prospect);
        viewSubitems.push($active);
        viewSubitems.push($reserved);
        viewSubitems.push($ordered);
        viewSubitems.push($cancelled);
        viewSubitems.push($closed);
        var $view;
        $view = FwMenu.addViewBtn($menuObject, 'View', viewSubitems);

        return $menuObject;
    };

    openForm(mode: string) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());

        if (mode === 'NEW') {
            $form.find('.ifnew').attr('data-enabled', 'true')
        }
        $form = FwModule.openForm($form, mode);

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="QuoteId"] input').val(uniqueids.QuoteId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="QuoteId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    afterLoad($form: any) {

    }
}

(<any>window).QuoteController = new Quote();