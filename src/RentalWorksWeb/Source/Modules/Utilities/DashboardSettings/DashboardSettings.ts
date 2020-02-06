routes.push({ pattern: /^module\/dashboardsettings$/, action: function (match: RegExpExecArray) { return DashboardSettingsController.getModuleScreen(); } });

class DashboardSettings {
    Module:  string = 'DashboardSettings';
    apiurl:  string = 'api/v1/dashboardsettings';
    caption: string = Constants.Modules.Utilities.children.DashboardSettings.caption;
    nav:     string = Constants.Modules.Utilities.children.DashboardSettings.nav;
    id:      string = Constants.Modules.Utilities.children.DashboardSettings.id;
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $form = this.openForm('EDIT');

        screen.load = () => {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {

        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        const userId = JSON.parse(sessionStorage.getItem('userid'));
        $form.find('div.fwformfield[data-datafield="UserId"] input').val(userId.webusersid);
        FwModule.loadForm(this.Module, $form);

        //first sortable list (not sure if it can be combined)
        Sortable.create($form.find('.sortable').get(0), {
            onEnd: function (evt) {
                $form.attr('data-modified', 'true');
                $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
            }
        });
        //second sortable list
        Sortable.create($form.find('.sortable').get(1), {
            onEnd: function (evt) {
                $form.attr('data-modified', 'true');
                $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
            }
        });

        FwFormField.loadItems($form.find('.widgettype'), [
            { value: 'bar', text: 'Bar' },
            { value: 'horizontalBar', text: 'Horizontal Bar' },
            { value: 'pie', text: 'Pie' }
        ], true);

        $form.data('beforesave', request => {
            for (var i = 0; i < request.UserWidgets.length; i++) {
                if (request.UserWidgets[i].UserWidgetId !== undefined) {
                    FwAppData.apiMethod(true, 'POST', 'api/v1/userwidget/', request.UserWidgets[i], FwServices.defaultTimeout, null, function onError(response) {
                        FwFunc.showError(response);
                    }, null);
                }
            }
        });

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        //FwModule.saveForm(this.Module, $form, { closetab: closetab, navigationpath: 'home' });
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        let self = this;
        $form.find('.settings').on('click', function () {
            let li = jQuery(this).closest('li');
            let widgetId = li.data('value');
            let userWidgetId = li.data('userwidgetid');
            try {
                FwAppData.apiMethod(true, 'GET', 'api/v1/userwidget/' + userWidgetId, null, FwServices.defaultTimeout, function onSuccess(response) {
                    let $confirmation = FwConfirmation.renderConfirmation('Chart Options <div style="font-size:0.8em;">' + response.Widget + '</div>', '');
                    let $select = FwConfirmation.addButton($confirmation, 'Confirm', false);
                    let $cancel = FwConfirmation.addButton($confirmation, 'Cancel', true);
                    let html = [];

                    html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                    html.push('<div class="flexrow">');
                    html.push('<div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield widgettype" data-caption="Chart Type" data-datafield="Widget"></div>');
                    html.push('</div>');
                    html.push('<div class="flexrow">');
                    html.push('<div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield stacked" data-caption="Stacked" data-datafield="Stacked"></div>');
                    html.push('</div>');
                    html.push('<div class="flexrow">');
                    html.push('<div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield officelocation" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" style="float:left;max-width:400px;"></div>');
                    html.push('</div>');
                    html.push('<div class="flexrow">');
                    html.push('<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield datebehavior" data-caption="Date Behavior" data-datafield="DateBehaviorId" data-displayfield="DateBehavior" data-validationname="WidgetDateBehaviorValidation" data-validationpeek="false" style="float:left;width:200px;"></div>');
                    html.push('</div>');
                    html.push('<div class="flexrow">');
                    html.push('<div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield datefield" data-caption="Date Field" data-datafield="DateField" style="display:none;"></div>');
                    html.push('</div>');
                    html.push('<div class="flexrow">');
                    html.push('<div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield fromdate" data-caption="From Date" data-datafield="FromDate" style="display:none;"></div>');
                    html.push('</div>');
                    html.push('<div class="flexrow">');
                    html.push('<div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield todate" data-caption="To Date" data-datafield="ToDate" style="display:none;"></div>');
                    html.push('</div>');
                    html.push('<div class="flexrow">');
                    html.push('<div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield defaultpoints" data-caption="Number of Data Points" data-datafield="DefaultDataPoints"></div>');
                    html.push('</div>');
                    html.push('<div class="flexrow">');
                    html.push('<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield axisformat" data-caption="Axis Number Format" data-datafield="AxisNumberFormatId" data-displayfield="AxisNumberFormat" data-validationname="WidgetNumberFormatValidation" data-validationpeek="false" style="float:left;width:200px;"></div>');
                    html.push('</div>');
                    html.push('<div class="flexrow">');
                    html.push('<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield dataformat" data-caption="Data Number Format" data-datafield="DataNumberFormatId" data-displayfield="DataNumberFormat" data-validationname="WidgetNumberFormatValidation" data-validationpeek="false" style="float:left;width:200px;"></div>');
                    html.push('</div>');
                    html.push('</div>');
                    FwConfirmation.addControls($confirmation, html.join(''));

                    FwFormField.loadItems($confirmation.find('.widgettype'), [
                        { value: 'bar', text: 'Bar' },
                        { value: 'horizontalBar', text: 'Horizontal Bar' },
                        { value: 'pie', text: 'Pie' }
                    ], true);
                    $confirmation.find('div[data-datafield="DefaultDataPoints"] input').val(response.DataPoints);

                    if (response.AxisNumberFormat !== '') {
                        FwFormField.setValueByDataField($confirmation, 'AxisNumberFormatId', response.AxisNumberFormatId, response.AxisNumberFormat);
                    } else {
                        FwFormField.setValueByDataField($confirmation, 'AxisNumberFormatId', response.DefaultAxisNumberFormatId, response.DefaultAxisNumberFormat);
                    }

                    if (response.Stacked !== null) {
                        FwFormField.setValueByDataField($confirmation, 'Stacked', response.Stacked);
                    } else {
                        FwFormField.setValueByDataField($confirmation, 'Stacked', response.DefaultStacked);
                    }

                    if (response.DataNumberFormat !== '') {
                        FwFormField.setValueByDataField($confirmation, 'DataNumberFormatId', response.DataNumberFormatId, response.DataNumberFormat);
                    } else {
                        FwFormField.setValueByDataField($confirmation, 'DataNumberFormatId', response.DefaultDataNumberFormatId, response.DefaultDataNumberFormat);
                    }

                    if (response.WidgetType !== '') {
                        FwFormField.setValue($confirmation, '.widgettype', response.WidgetType);
                    } else {
                        FwFormField.setValue($confirmation, '.widgettype', response.DefaultType);
                    }

                    if (response.FromDate !== '') {
                        let fromDate = $confirmation.find('.fromdate');
                        fromDate.show();
                        FwFormField.setValue2(fromDate, response.FromDate)
                    } else if (response.FromDate === '' && response.DefaultFromDate !== '') {
                        let fromDate = $confirmation.find('.fromdate');
                        fromDate.show();
                        FwFormField.setValue2(fromDate, response.DefaultFromDate);
                    }

                    if (response.ToDate !== '') {
                        let toDate = $confirmation.find('.todate');
                        toDate.show();
                        FwFormField.setValue2(toDate, response.ToDate)
                    } else if (response.ToDate === '' && response.DefaultToDate !== '') {
                        let toDate = $confirmation.find('.todate');
                        toDate.show();
                        FwFormField.setValue2(toDate, response.DefaultToDate);
                    }

                    $confirmation.find('div[data-datafield="DateBehaviorId"]').on('change', function () {
                        let selected = FwFormField.getValue2(jQuery(this));
                        self.setDateBehaviorFields($confirmation, selected);
                    });

                    FwFormField.setValueByDataField($confirmation, 'DateBehaviorId', response.DateBehaviorId, response.DateBehavior);
                    FwFormField.setValueByDataField($confirmation, 'OfficeLocationId', response.OfficeLocationId, response.OfficeLocation);

                    let dateFields = response.DateFields.split(',');
                    let dateFieldDisplays = response.DateFieldDisplayNames.split(',');
                    let dateFieldSelectArray = [];

                    for (var i = 0; i < dateFields.length; i++) {
                        dateFieldSelectArray.push({
                            'value': dateFields[i],
                            'text': dateFieldDisplays[i]
                        })
                    }
                    FwFormField.loadItems($confirmation.find('.datefield'), dateFieldSelectArray, true);

                    if (response.DateBehaviorId !== '' && response.DateBehaviorId !== undefined) {
                        self.setDateBehaviorFields($confirmation, response.DateBehaviorId);
                        FwFormField.setValueByDataField($confirmation, 'DateField', response.DateField);
                    }

                    if (li.data('request') !== undefined) {
                        let request = li.data('request');
                        if (request.FromDate !== '') {
                            $confirmation.find('.fromdate').show();
                        }
                        if (request.ToDate !== '') {
                            $confirmation.find('.todate').show();
                        }
                        self.setDateBehaviorFields($confirmation, request.DateBehavior);
                        FwFormField.setValue2($confirmation.find('.defaultpoints'), request.DataPoints);
                        FwFormField.setValue2($confirmation.find('.axisformat'), request.AxisNumberFormatId, request.AxisNumberFormat);
                        FwFormField.setValue2($confirmation.find('.dataformat'), request.DataNumberFormatId, request.DataNumberFormat);
                        FwFormField.setValue2($confirmation.find('.widgettype'), request.WidgetType);
                        FwFormField.setValue2($confirmation.find('.stacked'), request.Stacked);
                        FwFormField.setValue2($confirmation.find('.fromdate'), request.FromDate);
                        FwFormField.setValue2($confirmation.find('.todate'), request.ToDate);
                        FwFormField.setValue2($confirmation.find('.datefield'), request.DateField);
                        FwFormField.setValue2($confirmation.find('div[data-datafield="DateBehaviorId"]'), request.DateBehaviorId, request.DateBehavior);
                    }

                    $select.on('click', function () {
                        try {
                            var request: any = {};
                            let label = li.find('label');
                            request.UserWidgetId = userWidgetId;
                            request.WidgetType = FwFormField.getValue($confirmation, '.widgettype');
                            (FwFormField.getValue($confirmation, '.stacked') === 'T') ? request.Stacked = true : request.Stacked = false;
                            request.DataPoints = FwFormField.getValue($confirmation, '.defaultpoints');
                            request.AxisNumberFormatId = FwFormField.getValue($confirmation, '.axisformat');
                            request.DataNumberFormatId = FwFormField.getValue($confirmation, '.dataformat');
                            request.AxisNumberFormat = FwFormField.getText($confirmation, '.axisformat');
                            request.DataNumberFormat = FwFormField.getText($confirmation, '.dataformat');
                            request.DateBehaviorId = FwFormField.getValue($confirmation, '.datebehavior');
                            request.DateBehavior = FwFormField.getText($confirmation, '.datebehavior');
                            request.DateField = FwFormField.getValue($confirmation, '.datefield');
                            request.FromDate = FwFormField.getValue($confirmation, '.fromdate');
                            request.ToDate = FwFormField.getValue($confirmation, '.todate');
                            request.OfficeLocationId = FwFormField.getValue($confirmation, '.officelocation');
                            request.OfficeLocation = FwFormField.getText($confirmation, '.officelocation');

                            li.data('request', request);
                            if (label.text().charAt(label.text().length - 1) !== '*') {
                                label.append('*');
                            }

                            $form.attr('data-modified', 'true');
                            $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
                            FwNotification.renderNotification('SUCCESS', 'Widget Updated');
                            FwConfirmation.destroyConfirmation($confirmation);
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    })

                }, null, null);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
    }
    //----------------------------------------------------------------------------------------------
    setDateBehaviorFields($control, DateBehavior) {
        let fromDate = $control.find('.fromdate');
        let toDate = $control.find('.todate');
        let fromDateField = $control.find('div[data-datafield="FromDate"] > .fwformfield-caption');
        let dateField = $control.find('.datefield');

        if (DateBehavior === 'NONE') {
            fromDate.hide();
            toDate.hide();
            dateField.hide();
        } else {
            dateField.show();
            if (DateBehavior === 'SINGLEDATESPECIFICDATE') {
                fromDateField.text('Date');
                fromDate.show();
                toDate.hide();
            } else if (DateBehavior === 'DATERANGESPECIFICDATES') {
                fromDateField.text('From Date');
                fromDate.show();
                toDate.show();
            } else {
                fromDate.hide();
                toDate.hide();
            }
        }
    }
    //----------------------------------------------------------------------------------------------
}
var DashboardSettingsController = new DashboardSettings();