class EventType {
    Module: string = 'EventType';
    apiurl: string = 'api/v1/eventtype';
    caption: string = Constants.Modules.Settings.children.EventSettings.children.EventType.caption;
    nav: string = Constants.Modules.Settings.children.EventSettings.children.EventType.nav;
    id: string = Constants.Modules.Settings.children.EventSettings.children.EventType.id;

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
        FwBrowse.renderGrid({
            nameGrid: 'EventTypePersonnelTypeGrid',
            gridSecurityId: 'CbjLxfIjRyg',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    EventTypeId: FwFormField.getValueByDataField($form, 'EventTypeId')
                };
            },
            beforeSave: (request: any) => {
                request.EventTypeId = FwFormField.getValueByDataField($form, 'EventTypeId')
            }
        });
        // --------------
        FwBrowse.renderGrid({
            nameGrid: 'OrderTypeActivityDatesGrid',
            gridSecurityId: 'oMijD9WAL6Bl',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderTypeId: FwFormField.getValueByDataField($form, 'EventTypeId')
                };
            },
            beforeSave: (request: any) => {
                request.OrderTypeId = FwFormField.getValueByDataField($form, 'EventTypeId');
            }
        });
        // --------------
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

    afterLoad($form: any) {
        const $personnelTypeGrid = $form.find('[data-name="EventTypePersonnelTypeGrid"]');
        FwBrowse.search($personnelTypeGrid);

        const $orderTypeActivityDatesGrid = $form.find('[data-name="OrderTypeActivityDatesGrid"]');
        FwBrowse.search($orderTypeActivityDatesGrid);

        $orderTypeActivityDatesGrid.find('.eventType').parent().hide();
    }
}

var EventTypeController = new EventType();