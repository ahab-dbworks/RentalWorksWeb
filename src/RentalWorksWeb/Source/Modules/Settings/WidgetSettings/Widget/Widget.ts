class Widget {
    Module: string = 'Widget';
    apiurl: string = 'api/v1/widget';
    caption: string = Constants.Modules.Settings.children.WidgetSettings.children.Widget.caption;
    nav: string = Constants.Modules.Settings.children.WidgetSettings.children.Widget.nav;
    id: string = Constants.Modules.Settings.children.WidgetSettings.children.Widget.id;
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        options.hasNew = false;
        FwMenu.addBrowseMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
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
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        var $browse;

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        FwBrowse.init($browse);

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        FwFormField.loadItems($form.find('.datefield'), [
            { value: 'SOMEOTHERDATE', text: 'Some Other Date' },
            { value: 'INVOICEDATE', text: 'Invoice Date' },
            { value: 'BILLINGSTARTDATE', text: 'Billing Start Date' }
        ], true);

        $form.find('div[data-datafield="DefaultDateBehaviorId"]').on('change', function () {
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
        });

        //Toggles 'Assign To' grids
        $form.find('[data-datafield="AssignTo"]').on('change', e => {
            const assignTo = FwFormField.getValueByDataField($form, 'AssignTo');
            let $gridControl;
            switch (assignTo) {
                case 'GROUPS':
                    $form.find('.groupGrid').show();
                    $form.find('.userGrid').hide();
                    $gridControl = $form.find('[data-name="WidgetGroupGrid"]');
                    FwBrowse.search($gridControl);
                    break;
                case 'USERS':
                    $form.find('.userGrid').show();
                    $form.find('.groupGrid').hide();
                    $gridControl = $form.find('[data-name="WidgetUserGrid"]');
                    FwBrowse.search($gridControl);
                    break;
                case 'ALL':
                default:
                    $form.find('.userGrid').hide();
                    $form.find('.groupGrid').hide();
                    break;
            }
        });

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="WidgetId"] input').val(uniqueids.WidgetId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form) {
        //const $widgetGroupGrid = $form.find('div[data-grid="WidgetGroupGrid"]');
        //const $widgetGroupGridControl = FwBrowse.loadGridFromTemplate('WidgetGroupGrid');
        //$widgetGroupGrid.empty().append($widgetGroupGridControl);
        //$widgetGroupGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        WidgetId: FwFormField.getValueByDataField($form, 'WidgetId')
        //    };
        //});
        //$widgetGroupGridControl.data('beforesave', request => {
        //    request.WidgetId = FwFormField.getValueByDataField($form, 'WidgetId')
        //});
        //FwBrowse.init($widgetGroupGridControl);
        //FwBrowse.renderRuntimeHtml($widgetGroupGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'WidgetGroupGrid',
            gridSecurityId: 'BXv7mQIbXokIW',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    WidgetId: FwFormField.getValueByDataField($form, 'WidgetId')
                };
            },
            beforeSave: (request: any) => {
                request.WidgetId = FwFormField.getValueByDataField($form, 'WidgetId');
            }
        });

        //const $widgetUserGrid = $form.find('div[data-grid="WidgetUserGrid"]');
        //const $widgetUserGridControl = FwBrowse.loadGridFromTemplate('WidgetUserGrid');
        //$widgetUserGrid.empty().append($widgetUserGridControl);
        //$widgetUserGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        WidgetId: FwFormField.getValueByDataField($form, 'WidgetId')
        //    };
        //});
        //$widgetUserGridControl.data('beforesave', request => {
        //    request.WidgetId = FwFormField.getValueByDataField($form, 'WidgetId')
        //});
        //FwBrowse.init($widgetUserGridControl);
        //FwBrowse.renderRuntimeHtml($widgetUserGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'WidgetUserGrid',
            gridSecurityId: 'CTzXYDyNzi8ET',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    WidgetId: FwFormField.getValueByDataField($form, 'WidgetId')
                };
            },
            beforeSave: (request: any) => {
                request.WidgetId = FwFormField.getValueByDataField($form, 'WidgetId');
            }
        });
    }
    //----------------------------------------------------------------------------------------------
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

        FwFormField_select.loadItems(dateSelectField, selectArray, true);
        FwFormField_select.setValue(dateSelectField, dateSelected, '', false);

        //shows/hides "Assign To" grids
        const assignTo = FwFormField.getValueByDataField($form, 'AssignTo');
        switch (assignTo) {
            case 'GROUPS':
                $form.find('.groupGrid').show();
                $form.find('.userGrid').hide();
                break;
            case 'USERS':
                $form.find('.groupGrid').hide();
                $form.find('.userGrid').show();
                break;
            case 'ALL':
                $form.find('.groupGrid').hide();
                $form.find('.userGrid').hide();
        }
    }
}

var WidgetController = new Widget();