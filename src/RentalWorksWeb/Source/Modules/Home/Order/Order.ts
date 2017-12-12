declare var FwModule: any;
declare var FwBrowse: any;
declare var FwServices: any;
declare var FwMenu: any;
declare var Mustache: any;
declare var FwFunc: any;
declare var FwNotification: any;

class Order {
    Module: string;
    apiurl: string;
    caption: string;
    ActiveView: string;

    constructor() {
        this.Module = 'Order';
        this.apiurl = 'api/v1/order';
        this.caption = 'Order';
        this.ActiveView = 'ALL';
    }

    getModuleScreen() {
        var self = this;
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $browse: JQuery = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, self.caption, false, 'BROWSE', true);
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
        var $browse: JQuery = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        FwBrowse.init($browse);

        $browse.data('ondatabind', function (request) {
            request.activeview = self.ActiveView;
        });
        FwBrowse.addLegend($browse, 'On Hold', '#ff8040'); //placeholder colors
        FwBrowse.addLegend($browse, 'No Charge', '#ff0080');
        FwBrowse.addLegend($browse, 'Late', '#ffff80');
        FwBrowse.addLegend($browse, 'Foreign Currency', '#03de3a');
        FwBrowse.addLegend($browse, 'Multi-Warehouse', '#20b7ff');
        FwBrowse.addLegend($browse, 'Repair', '#20b7ff');
        FwBrowse.addLegend($browse, 'L&D', '#20b7ff');
        return $browse;
       
    }

    addBrowseMenuItems($menuObject: any) {
        var self = this;
        var $all: JQuery = FwMenu.generateDropDownViewBtn('ALL', true);
        var $confirmed: JQuery = FwMenu.generateDropDownViewBtn('Confirmed', false);
        var $active: JQuery = FwMenu.generateDropDownViewBtn('Active', false);
        var $hold: JQuery = FwMenu.generateDropDownViewBtn('Hold', false);
        var $complete: JQuery = FwMenu.generateDropDownViewBtn('Complete', false);
        var $cancelled: JQuery = FwMenu.generateDropDownViewBtn('Cancelled', false);
        var $closed: JQuery = FwMenu.generateDropDownViewBtn('Closed', false);

        $all.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ALL';
            FwBrowse.databind($browse);
        });
        $confirmed.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'CONFIRMED';
            FwBrowse.databind($browse);
        });
        $active.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ACTIVE';
            FwBrowse.databind($browse);
        });
        $hold.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'HOLD';
            FwBrowse.databind($browse);
        });
        $complete.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'COMPLETE';
            FwBrowse.databind($browse);
        });
        $cancelled.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'CANCELLED';
            FwBrowse.databind($browse);
        });
        $closed.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'CLOSED';
            FwBrowse.databind($browse);
        });
      

        var viewSubitems: Array<JQuery> = [];
        viewSubitems.push($all);
        viewSubitems.push($confirmed);
        viewSubitems.push($active);
        viewSubitems.push($hold);
        viewSubitems.push($complete);
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
        $form.find('div.fwformfield[data-datafield="OrderId"] input').val(uniqueids.OrderId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="OrderId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    afterLoad($form: any) {

    }
}

(<any>window).OrderController = new Order();