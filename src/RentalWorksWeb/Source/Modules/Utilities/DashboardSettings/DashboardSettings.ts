routes.push({ pattern: /^module\/dashboardsettings$/, action: function (match: RegExpExecArray) { return DashboardSettingsController.getModuleScreen(); } });

class DashboardSettings {
    Module: string = 'DashboardSettings';
    apiurl: string = 'api/v1/userdashboardsettings';
    caption: string = 'Dashboard Settings';
    nav: string = 'module/dashboardsettings';
    id: string = '1B40C62A-1FA0-402E-BE52-9CBFDB30AD3F';
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('EDIT');

        screen.load = function () {
            FwModule.openModuleTab($form, 'Dashboard Settings', false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        var $form;
        var userId = JSON.parse(sessionStorage.getItem('userid'));

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        $form.find('div.fwformfield[data-datafield="UserId"] input').val(userId.webusersid);
        FwModule.loadForm(this.Module, $form);

        var newsort = Sortable.create($form.find('.sortable').get(0), {
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
        $form.find('.settings').on('click', function () {
            let li = jQuery(this).closest('li');
            let widgetId = li.data('value');
            let userWidgetId = li.data('userwidgetid');
            try {

                let $confirmation = FwConfirmation.renderConfirmation('Chart Options', '');
                let $select = FwConfirmation.addButton($confirmation, 'Confirm', false);
                let $cancel = FwConfirmation.addButton($confirmation, 'Cancel', true);

                let html = [];
                FwAppData.apiMethod(true, 'GET', 'api/v1/userwidget/' + userWidgetId, null, FwServices.defaultTimeout, function onSuccess(response) {
                    html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                    html.push('<div class="flexrow">');
                    html.push('<div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield widgettype" data-caption="Chart Type" data-datafield="Widget"></div>');
                    html.push('</div>');
                    html.push('<div class="flexrow">');
                    html.push('<div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield defaultpoints" data-caption="Number of Data Points" data-datafield="DefaultDataPoints"></div>');
                    html.push('</div>');
                    html.push('<div class="flexrow">');
                    html.push('<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield axisformat" data-caption="Axis Number Format" data-datafield="AxisNumberFormatId" data-displayfield="AxisNumberFormat" data-validationname="WidgetNumberFormatValidation" style="float:left;width:200px;"></div>');
                    html.push('</div>');
                    html.push('<div class="flexrow">');
                    html.push('<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield dataformat" data-caption="Data Number Format" data-datafield="DataNumberFormatId" data-displayfield="DataNumberFormat" data-validationname="WidgetNumberFormatValidation" style="float:left;width:200px;"></div>');
                    html.push('</div>');
                    html.push('<div class="flexrow">');
                    html.push('<div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield datebehavior" data-caption="Date Behavior" data-datafield="DateBehavior" style="float:left;width:200px;"></div>');
                    html.push('</div>');
                    html.push('<div class="flexrow">');
                    html.push('<div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield fromdate" data-caption="From Date" data-datafield="FromDate" style="display:none;"></div>');
                    html.push('</div>');
                    html.push('<div class="flexrow">');
                    html.push('<div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield todate" data-caption="To Date" data-datafield="ToDate" style="display:none;"></div>');
                    html.push('</div>');
                    html.push('</div>');
                    FwConfirmation.addControls($confirmation, html.join(''));
                    FwFormField.loadItems($confirmation.find('.widgettype'), [
                        { value: 'bar', text: 'Bar' },
                        { value: 'horizontalBar', text: 'Horizontal Bar' },
                        { value: 'pie', text: 'Pie' }
                    ], true);
                    FwFormField.loadItems($confirmation.find('.datebehavior'), [
                        { value: 'NONE', text: 'None' },
                        { value: 'SINGLE DATE - YESTERDAY', text: 'Single Date - Yesterday' },
                        { value: 'SINGLE DATE - TODAY', text: 'Single Date - Today' },
                        { value: 'SINGLE DATE - TOMORROW', text: 'Single Date - Tomorrow' },
                        { value: 'SINGLE DATE - SPECIFIC DATE', text: 'Single Date - Specific Date' },
                        { value: 'DATE RANGE - PRIOR WEEK', text: 'Date Range - Prior Week' },
                        { value: 'DATE RANGE - CURRENT WEEK', text: 'Date Range - Current Week' },
                        { value: 'DATE RANGE - PRIOR MONTH', text: 'Date Range - Prior Month' },
                        { value: 'DATE RANGE - CURRENT MONTH', text: 'Date Range - Current Month' },
                        { value: 'DATE RANGE - NEXT MONTH', text: 'Date Range - Next Week' },
                        { value: 'DATE RANGE - PRIOR YEAR', text: 'Date Range - Prior Year' },
                        { value: 'DATE RANGE - CURRENT YEAR', text: 'Date Range - Current Year' },
                        { value: 'DATE RANGE - YEAR TO DATE', text: 'Date Range - Year To Date' },
                        { value: 'DATE RANGE - NEXT YEAR', text: 'Date Range - Next Year' },
                        { value: 'DATE RANGE - SPECIFIC DATES', text: 'Date Range - Specific Dates' }
                    ], true);
                    if (response.DataPoints !== 0) {
                        FwFormField.setValue2($confirmation.find('.defaultpoints'), response.DataPoints);
                    } else {
                        FwFormField.setValue2($confirmation.find('.defaultpoints'), response.DefaultDataPoints);
                    }

                    if (response.AxisNumberFormat !== '') {
                        FwFormField.setValue2($confirmation.find('.axisformat'), response.AxisNumberFormatId, response.AxisNumberFormat);
                    } else {
                        FwFormField.setValue2($confirmation.find('.axisformat'), response.DefaultAxisNumberFormatId, response.DefaultAxisNumberFormat);
                    }

                    if (response.DataNumberFormat !== '') {
                        FwFormField.setValue2($confirmation.find('.dataformat'), response.DataNumberFormatId, response.DataNumberFormat);
                    } else {
                        FwFormField.setValue2($confirmation.find('.dataformat'), response.DefaultDataNumberFormatId, response.DefaultDataNumberFormat);
                    }

                    if (response.WidgetType !== '') {
                        FwFormField.setValue2($confirmation.find('.widgettype'), response.WidgetType);
                    } else {
                        FwFormField.setValue2($confirmation.find('.widgettype'), response.DefaultType);
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

                    $confirmation.find('div[data-datafield="DateBehavior"]').on('change', function () {
                        let selected = FwFormField.getValue2(jQuery(this));
                        //let dateField = $confirmation.find('.date-field');
                        let fromDate = $confirmation.find('.fromdate');
                        let toDate = $confirmation.find('.todate');
                        let fromDateField = $confirmation.find('div[data-datafield="FromDate"] > .fwformfield-caption');

                        if (selected === 'NONE') {
                            //dateField.hide();
                            fromDate.hide();
                            toDate.hide();
                        } else if (selected === 'SINGLE DATE - YESTERDAY' || selected === 'SINGLE DATE - TODAY' || selected === 'SINGLE DATE - TOMORROW' || selected === 'DATE RANGE - PRIOR WEEK' || selected === 'DATE RANGE - CURRENT WEEK' || selected === 'DATE RANGE - NEXT WEEK' || selected === 'DATE RANGE - PRIOR MONTH' || selected === 'DATE RANGE - CURRENT MONTH' || selected === 'DATE RANGE - NEXT MONTH' || selected === 'DATE RANGE - PRIOR YEAR' || selected === 'DATE RANGE - CURRENT YEAR' || selected === 'DATE RANGE - NEXT YEAR' || selected === 'DATE RANGE - YEAR TO DATE') {
                            //dateField.show();
                            fromDate.hide();
                            toDate.hide();
                        } else if (selected === 'SINGLE DATE - SPECIFIC DATE') {
                            //dateField.show();
                            fromDateField.text('Date');
                            fromDate.show();
                            toDate.hide();
                        } else if (selected === 'DATE RANGE - SPECIFIC DATES') {
                            //dateField.show();
                            fromDateField.text('From Date');
                            fromDate.show();
                            toDate.show();
                        }
                    });
                    if (li.data('request') !== undefined) {
                        let request = li.data('request');
                        if (request.FromDate !== '') {
                            $confirmation.find('.fromdate').show();
                        }
                        if (request.ToDate !== '') {
                            $confirmation.find('.todate').show();
                        }
                        FwFormField.setValue2($confirmation.find('.defaultpoints'), request.DataPoints);
                        FwFormField.setValue2($confirmation.find('.axisformat'), request.AxisNumberFormatId, request.AxisNumberFormat);
                        FwFormField.setValue2($confirmation.find('.dataformat'), request.DataNumberFormatId, request.DataNumberFormat);
                        FwFormField.setValue2($confirmation.find('.widgettype'), request.WidgetType);
                        FwFormField.setValue2($confirmation.find('.fromdate'), request.FromDate);
                        FwFormField.setValue2($confirmation.find('.todate'), request.ToDate);
                        FwFormField.setValue2($confirmation.find('div[data-datafield="DateBehavior"]'), request.DateBehavior);
                    }

                }, null, null);

                $select.on('click', function () {
                    try {
                        var request: any = {};
                        let label = li.find('label');
                        request.UserWidgetId = userWidgetId;
                        request.WidgetType = FwFormField.getValue($confirmation, '.widgettype');
                        request.DataPoints = FwFormField.getValue($confirmation, '.defaultpoints');
                        request.AxisNumberFormatId = FwFormField.getValue($confirmation, '.axisformat');
                        request.DataNumberFormatId = FwFormField.getValue($confirmation, '.dataformat');
                        request.AxisNumberFormat = FwFormField.getText($confirmation, '.axisformat');
                        request.DataNumberFormat = FwFormField.getText($confirmation, '.dataformat');
                        request.DateBehavior = FwFormField.getValue($confirmation, '.datebehavior');
                        request.FromDate = FwFormField.getValue($confirmation, '.fromdate');
                        request.ToDate = FwFormField.getValue($confirmation, '.todate');

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
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
    }
    //----------------------------------------------------------------------------------------------
}
var DashboardSettingsController = new DashboardSettings();