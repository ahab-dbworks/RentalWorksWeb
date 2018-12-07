class Widget {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'Widget';
        this.apiurl = 'api/v1/widget';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Widget', false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }

    openBrowse() {
        var $browse;

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        FwBrowse.init($browse);

        return $browse;
    }

    openForm(mode: string) {
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        $form.find('div[data-datafield="DefaultDateBehavior"]').on('change', function() {
            let selected = FwFormField.getValue2(jQuery(this));
            let dateField = $form.find('.date-field');
            let specificDate = $form.find('.specific-date');
            let fromDate = $form.find('.from-date');
            let toDate = $form.find('.to-date');
            let fromDateField = $form.find('div[data-datafield="DefaultFromDate"] > .fwformfield-caption')

            if (selected === 'NONE') {
                dateField.hide();
                specificDate.hide();
                fromDate.hide();
                toDate.hide();
            } else if (selected === 'SINGLE DATE - YESTERDAY' || selected === 'SINGLE DATE - TODAY' || selected === 'SINGLE DATE - TOMORROW' || selected === 'DATE RANGE - PRIOR WEEK' || selected === 'DATE RANGE - CURRENT WEEK' || selected === 'DATE RANGE - NEXT WEEK' || selected === 'DATE RANGE - PRIOR MONTH' || selected === 'DATE RANGE - CURRENT MONTH' || selected === 'DATE RANGE - NEXT MONTH' || selected === 'DATE RANGE - PRIOR YEAR' || selected === 'DATE RANGE - CURRENT YEAR' || selected === 'DATE RANGE - NEXT YEAR' || selected === 'DATE RANGE - YEAR TO DATE') {
                dateField.show();
                specificDate.hide();
                fromDate.hide();
                toDate.hide();
            } else if (selected === 'SINGLE DATE - SPECIFIC DATE') {
                dateField.show();
                specificDate.show();
                fromDateField.text('Date');
                fromDate.show();
                toDate.hide();
            } else if (selected === 'DATE RANGE - SPECIFIC DATES') {
                dateField.show();
                specificDate.show();
                fromDateField.text('From Date');
                fromDate.show();
                toDate.show();
            }
        })

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="WidgetId"] input').val(uniqueids.WidgetId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="WidgetId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    afterLoad($form: any) {
        FwFormField.disable($form.find('[data-datafield="ApiName"]'));
        let dateSelectField = $form.find('.datefield');
        let dateSelected = FwFormField.getValue2(dateSelectField);
        let dateFieldDisplay = FwFormField.getValueByDataField($form, 'DateFieldDisplayNames').split(',');
        let dateFieldValues = FwFormField.getValueByDataField($form, 'DateFields').split(',');
        let selectArray = [];

        for (var i = 0; i < dateFieldDisplay.length; i++) {
            selectArray.push({
                'value': dateFieldValues[i],
                'text': dateFieldDisplay[i]
            })
        }
        window['FwFormField_select'].loadItems(dateSelectField, selectArray, true);
        window['FwFormField_select'].setValue(dateSelectField, dateSelected);
    }
}

var WidgetController = new Widget();