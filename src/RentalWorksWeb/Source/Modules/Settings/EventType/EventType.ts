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
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
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
        const $orderTypePersonnelTypeGrid = $form.find('div[data-grid="EventTypePersonnelTypeGrid"]');
        const $orderTypePersonnelTypeGridControl = FwBrowse.loadGridFromTemplate('EventTypePersonnelTypeGrid');
        $orderTypePersonnelTypeGrid.empty().append($orderTypePersonnelTypeGridControl);
        $orderTypePersonnelTypeGridControl.data('ondatabind', request => {
            request.uniqueids = {
                EventTypeId: FwFormField.getValueByDataField($form, 'EventTypeId')
            };
        });
        $orderTypePersonnelTypeGridControl.data('beforesave', request => {
            request.EventTypeId = FwFormField.getValueByDataField($form, 'EventTypeId')
        });
        FwBrowse.init($orderTypePersonnelTypeGridControl);
        FwBrowse.renderRuntimeHtml($orderTypePersonnelTypeGridControl);
        // --------------
        const $orderTypeActivityDatesGrid = $form.find('div[data-grid="OrderTypeActivityDatesGrid"]');
        const $orderTypeActivityDatesGridControl = FwBrowse.loadGridFromTemplate('OrderTypeActivityDatesGrid');
        $orderTypeActivityDatesGrid.empty().append($orderTypeActivityDatesGridControl);
        $orderTypeActivityDatesGridControl.data('ondatabind', request => {
            request.uniqueids = {
                OrderTypeId: FwFormField.getValueByDataField($form, 'EventTypeId')
            };
        })
        $orderTypeActivityDatesGridControl.data('beforesave', request => {
            request.OrderTypeId = FwFormField.getValueByDataField($form, 'EventTypeId');
        });
        FwBrowse.init($orderTypeActivityDatesGridControl);
        FwBrowse.renderRuntimeHtml($orderTypeActivityDatesGridControl);
    }

    openBrowse() {
        var $browse;

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }

    openForm(mode: string) {
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
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

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="EventTypeId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    afterLoad($form: any) {
        const $personnelTypeGrid = $form.find('[data-name="EventTypePersonnelTypeGrid"]');
        FwBrowse.search($personnelTypeGrid);

        const $orderTypeActivityDatesGrid = $form.find('[data-name="OrderTypeActivityDatesGrid"]');
        FwBrowse.search($orderTypeActivityDatesGrid);

        $orderTypeActivityDatesGrid.find('.eventType').parent().hide();
    }
}

var EventTypeController = new EventType();