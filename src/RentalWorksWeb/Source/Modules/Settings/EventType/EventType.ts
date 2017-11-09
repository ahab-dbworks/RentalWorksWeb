declare var FwModule: any;
declare var FwBrowse: any;

class EventType {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'EventType';
        this.apiurl = 'api/v1/eventtype';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Event Type', false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }

    renderGrids($form: any) {        
        var nameOrderTypeInvoiceExportGrid: string = 'EventTypePersonnelTypeGrid';
        var $orderTypeInvoiceExportGrid: any = $orderTypeInvoiceExportGrid = $form.find('div[data-grid="' + nameOrderTypeInvoiceExportGrid + '"]');
        var $orderTypeInvoiceExportGridControl: any = FwBrowse.loadGridFromTemplate(nameOrderTypeInvoiceExportGrid);

        $orderTypeInvoiceExportGrid.empty().append($orderTypeInvoiceExportGridControl);
        $orderTypeInvoiceExportGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                EventTypeId: FwFormField.getValueByDataField($form, 'EventTypeId')
            };
        });
        $orderTypeInvoiceExportGridControl.data('beforesave', function (request) {
            request.EventTypeId = FwFormField.getValueByDataField($form, 'EventTypeId')
        });
        FwBrowse.init($orderTypeInvoiceExportGridControl);
        FwBrowse.renderRuntimeHtml($orderTypeInvoiceExportGridControl);
        // --------------
        var $orderTypeActivityDatesGrid: any;
        var $orderTypeActivityDatesGridControl: any;

        $orderTypeActivityDatesGrid = $form.find('div[data-grid="OrderTypeActivityDatesGrid"]');
        $orderTypeActivityDatesGridControl = jQuery(jQuery('#tmpl-grids-OrderTypeActivityDatesGridBrowse').html());
        $orderTypeActivityDatesGrid.empty().append($orderTypeActivityDatesGridControl);
        $orderTypeActivityDatesGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderTypeId: $form.find('div.fwformfield[data-datafield="EventTypeId"] input').val()
            };
        })
        $orderTypeActivityDatesGridControl.data('beforesave', function (request) {
            request.OrderTypeId = FwFormField.getValueByDataField($form, 'EventTypeId');
        });
        FwBrowse.init($orderTypeActivityDatesGridControl);
        FwBrowse.renderRuntimeHtml($orderTypeActivityDatesGridControl);
    }

    openBrowse() {
        var $browse;

        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);
        FwBrowse.init($browse);

        return $browse;
    }

    openForm(mode: string) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="EventTypeId"] input').val(uniqueids.EventTypeId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="EventTypeId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    afterLoad($form: any) {
        var $resaleGrid;

        $resaleGrid = $form.find('[data-name="EventTypePersonnelTypeGrid"]');
        FwBrowse.search($resaleGrid);

        var $orderTypeActivityDatesGrid: any;
        $orderTypeActivityDatesGrid = $form.find('[data-name="OrderTypeActivityDatesGrid"]');
        FwBrowse.search($orderTypeActivityDatesGrid);
    }
}

(<any>window).EventTypeController = new EventType();