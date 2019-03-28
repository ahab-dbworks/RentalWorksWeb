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
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
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

        FwFormField.loadItems($form.find('.datefield'), [
            { value: 'SOMEOTHERDATE', text: 'Some Other Date' },
            { value: 'INVOICEDATE', text: 'Invoice Date' },
            { value: 'BILLINGSTARTDATE', text: 'Billing Start Date' }
        ], true);

        $form.find('div[data-datafield="DefaultDateBehaviorId"]').on('change', function() {
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
            } else if (selected === 'SINGLEDATEYESTERDAY' || selected === 'SINGLEDATETODAY' || selected === 'SINGLEDATETOMORROW' || selected === 'DATERANGEPRIORWEEK' || selected === 'DATERANGECURRENTWEEK' || selected === 'DATERANGENEXTWEEK' || selected === 'DATERANGEPRIORMONTH' || selected === 'DATERANGECURRENTMONTH' || selected === 'DATERANGENEXTMONTH' || selected === 'DATERANGEPRIORYEAR' || selected === 'DATERANGECURRENTYEAR' || selected === 'DATERANGENEXTYEAR' || selected === 'DATERANGEYEARTODATE') {
                dateField.show();
                specificDate.hide();
                fromDate.hide();
                toDate.hide();
            } else if (selected === 'SINGLEDATESPECIFICDATE') {
                dateField.show();
                specificDate.show();
                fromDateField.text('Date');
                fromDate.show();
                toDate.hide();
            } else if (selected === 'DATERANGESPECIFICDATES') {
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
        let dateSelectField = $form.find('.datefield');
        let dateSelected = FwFormField.getValue2(dateSelectField);
        let dateFieldDisplay = FwFormField.getValueByDataField($form, 'DateFieldDisplayNames');
        let dateFieldValues = FwFormField.getValueByDataField($form, 'DateFields');
        let defaultDayBehavior = FwFormField.getValueByDataField($form, 'DefaultDateBehaviorId');
        let dateField = $form.find('.date-field');
        let specificDate = $form.find('.specific-date');
        let fromDate = $form.find('.from-date');
        let toDate = $form.find('.to-date');
        let fromDateField = $form.find('div[data-datafield="DefaultFromDate"] > .fwformfield-caption')
        let selectArray = [];

        if (defaultDayBehavior === 'SINGLEDATEYESTERDAY' || defaultDayBehavior === 'SINGLEDATETODAY' || defaultDayBehavior === 'SINGLEDATETOMORROW' || defaultDayBehavior === 'DATERANGEPRIORWEEK' || defaultDayBehavior === 'DATERANGECURRENTWEEK' || defaultDayBehavior === 'DATERANGENEXTWEEK' || defaultDayBehavior === 'DATERANGEPRIORMONTH' || defaultDayBehavior === 'DATERANGECURRENTMONTH' || defaultDayBehavior === 'DATERANGENEXTMONTH' || defaultDayBehavior === 'DATERANGEPRIORYEAR' || defaultDayBehavior === 'DATERANGECURRENTYEAR' || defaultDayBehavior === 'DATERANGENEXTYEAR' || defaultDayBehavior === 'DATERANGEYEARTODATE') {
           dateField.show();
        } else if (defaultDayBehavior === 'SINGLEDATESPECIFICDATE') {
            dateField.show();
            specificDate.show();
            fromDateField.text('Date');
            fromDate.show();
        } else if (defaultDayBehavior === 'DATERANGESPECIFICDATES') {
            dateField.show();
            specificDate.show();
            fromDateField.text('From Date');
            fromDate.show();
            toDate.show();
        }

        if (dateFieldDisplay !== undefined) {
            let dateFieldDisplayArray = dateFieldDisplay.split(',');
            let dateFieldValueArray = dateFieldValues.split(',');
            for (var i = 0; i < dateFieldDisplayArray.length; i++) {
                selectArray.push({
                    'value': dateFieldDisplayArray[i],
                    'text': dateFieldValueArray[i]
                })
            }
        }

        window['FwFormField_select'].loadItems(dateSelectField, selectArray, true);
        window['FwFormField_select'].setValue(dateSelectField, dateSelected);
    }
}

var WidgetController = new Widget();