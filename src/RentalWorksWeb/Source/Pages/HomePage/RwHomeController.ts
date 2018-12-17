class RwHome {

    Module: string = 'RwHome';
    charts: any = [];
    widgets: any = {};
    ordersbyagent: boolean = false;
    dealsbytype: boolean = false;
    billingbyagentbymonth: boolean = false;

    constructor() {
        this.charts = [];
    }

    getHomeScreen() {
        var self = this;
        var applicationOptions = program.getApplicationOptions();
        var screen: any = {};
        screen.$view = jQuery(jQuery('#tmpl-pages-Home').html());

        jQuery('title').html('RentalWorks');

        screen.load = function () {
            var redirectPath = sessionStorage.getItem('redirectPath');
            if (typeof redirectPath === 'string' && redirectPath.length > 0) {
                setTimeout(() => {
                    sessionStorage.removeItem('redirectPath');
                    program.navigate(redirectPath);
                }, 0);
            } else {
                self.loadSettings(screen.$view);
            }
        };

        return screen;
    };

    commaDelimited = function(value, index, values) {
        if (typeof value !== 'string' && value > 1) {
            value = value.toString();
            value = value.split(/(?=(?:...)*$)/);
            value = value.join(',');
            return value;
        } else if (value < 1) {
            return Math.round(value * 10) / 10
        } else {
            return value;
        }
    };

    commaDelimited2 = function(tooltipItem, data) {
        var value = data.datasets[0].data[tooltipItem.index];
        value = value.toString();
        value = value.split(/(?=(?:...)*$)/);
        value = value.join(',');
        return value;
    }

    commaTwoDecimal = function (value, index, values) {
        if (typeof value !== 'string' && value > 1) {
            value = value.toString();
            value = value.split(/(?=(?:...)*$)/);
            value = value.join(',');
            value = value + '.00';
            return value;
        } else if (value < 1) {
            return (Math.round(value * 10) / 10) + '.00'
        } else {
            return value + '.00';
        }
    };

    commaTwoDecimal2 = function (tooltipItem, data) {
        var value = data.datasets[0].data[tooltipItem.index];
        value = value.toString();
        value = value.split(/(?=(?:...)*$)/);
        value = value.join(',');
        value = value + '.00';
        return value;
    }

    buildWidgetSettings($chartSettings, userWidgetId) {
        var self = this;
        $chartSettings.on('click', function () {
            try {
                var $confirmation = FwConfirmation.renderConfirmation('Chart Options', '');
                var $select = FwConfirmation.addButton($confirmation, 'Confirm', false);
                var $cancel = FwConfirmation.addButton($confirmation, 'Cancel', true);
                var widgetName = jQuery(this).parent().data('chart')
                var userId = JSON.parse(sessionStorage.getItem('userid')).webusersid;

                var html = [];
                FwAppData.apiMethod(true, 'GET', 'api/v1/userwidget/' + userWidgetId, null, FwServices.defaultTimeout, function onSuccess(response) {
                    html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                    html.push('<div class="flexrow">');
                    html.push('<div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield widgettype" data-caption="Chart Type" data-datafield="Widget"></div>');
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
                    html.push('<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield axisformat" data-caption="Axis Number Format" data-datafield="AxisNumberFormatId" data-displayfield="AxisNumberFormat" data-validationname="WidgetNumberFormatValidation" style="float:left;width:200px;"></div>');
                    html.push('</div>');
                    html.push('<div class="flexrow">');
                    html.push('<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield dataformat" data-caption="Data Number Format" data-datafield="DataNumberFormatId" data-displayfield="DataNumberFormat" data-validationname="WidgetNumberFormatValidation" style="float:left;width:200px;"></div>');
                    html.push('</div>');
                    //for (var i = 0; i < response.data.labels.length; i++) {
                    //    html.push('<div class="flexrow">');
                    //    html.push('  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="' + response.data.labels[i] + '" data-datafield="' + response.data.labels[i] + '"></div>');
                    //    html.push('</div>');
                    //}
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
                }, null, null);

                $select.on('click', function () {
                    try {
                        var request: any = {};
                        request.UserWidgetId = userWidgetId;
                        request.WidgetType = FwFormField.getValue($confirmation, '.widgettype');
                        request.DataPoints = FwFormField.getValue($confirmation, '.defaultpoints');
                        request.AxisNumberFormatId = FwFormField.getValue($confirmation, '.axisformat');
                        request.DataNumberFormatId = FwFormField.getValue($confirmation, '.dataformat');
                        request.FromDate = FwFormField.getValue($confirmation, '.fromdate');
                        request.ToDate = FwFormField.getValue($confirmation, '.todate');
                        FwAppData.apiMethod(true, 'POST', 'api/v1/userwidget/', request, FwServices.defaultTimeout, function onSuccess(response) {
                            FwNotification.renderNotification('SUCCESS', 'Widget Chart Type Updated');
                            FwConfirmation.destroyConfirmation($confirmation);
                            program.navigate('home');
                        }, function onError(response) {
                            FwFunc.showError(response);
                            FwFormField.enable($confirmation.find('.fwformfield'));
                        }, $chartSettings);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                })
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
        return $chartSettings

    }

    loadSettings($control) {
        var self = this;
        var $dashboard = $control.find('.programlogo');
        var userId = JSON.parse(sessionStorage.getItem('userid')).webusersid;

        FwAppData.apiMethod(true, 'GET', 'api/v1/userdashboardsettings/' + userId, null, FwServices.defaultTimeout, function onSuccess(response) {
            let hiddenCounter = 0;
            let dashboardButton = '<div class="flexrow" style="max-width:none;justify-content:center"><div class="fwformcontrol dashboardsettings" data-type="button" style="flex:0 1 350px;margin:75px 0 0 10px;text-align:center;">You have no widgets yet - Add some now!</div></div>';
            for (var i = 0; i < response.UserWidgets.length; i++) {
                if (response.UserWidgets[i].selected) {
                    // to do - clean up this function with an object instead of tons of variables 
                    self.renderWidget($dashboard, response.UserWidgets[i].apiname, response.UserWidgets[i].widgettype, response.UserWidgets[i].clickpath, response.UserWidgets[i].userWidgetId, Math.floor(100 / response.WidgetsPerRow).toString() + '%', response.UserWidgets[i].text, response.UserWidgets[i].dataPoints, response.UserWidgets[i].axisNumberFormatId, response.UserWidgets[i].dataNumberFormatId, response.UserWidgets[i].fromDate, response.UserWidgets[i].toDate)
                } else {
                    hiddenCounter++;
                }
                if (hiddenCounter === response.UserWidgets.length) {
                    jQuery($control).append(dashboardButton);
                    jQuery($control).find('.dashboardsettings').on('click', e => {
                        program.navigate('module/dashboardsettings');
                    });
                }
            }
        }, null, $control);
    }

    renderWidget($control, apiname, type, chartpath, userWidgetId, width, text, dataPoints, axisFormat, dataFormat, fromDate, toDate) {
        var self = this;
        var refresh = '<i id="' + userWidgetId + 'refresh" class="chart-refresh material-icons">refresh</i>';
        var settings = '<i id="' + userWidgetId + 'settings" class="chart-settings material-icons">settings</i>';
        var fullscreen = '<i id="' + userWidgetId + 'fullscreen" class="chart-settings material-icons">fullscreen</i>';
        var dataPointCount = 0;

        jQuery($control).append('<div data-chart="' + apiname + '" class="chart-container" style="height:' + width + ';width:' + width + ';"><canvas style="display:inline-block;width:100%;padding:5px;" id="' + userWidgetId + '"></canvas><div class="toolbar">' + fullscreen + refresh + settings + '</div></div>');
        self.buildWidgetSettings(jQuery($control).find('#' + userWidgetId + 'settings'), userWidgetId);

        if (dataPoints > 0) {
            dataPointCount = dataPoints
        }

        jQuery($control).on('click', '#' + userWidgetId + 'refresh', function () {
            FwAppData.apiMethod(true, 'GET', `api/v1/widget/loadbyname/${apiname}?dataPoints=${dataPointCount}&locationId=${JSON.parse(sessionStorage.getItem('location')).locationid}&warehouseId=${JSON.parse(sessionStorage.getItem('warehouse')).warehouseid}&departmentId=${JSON.parse(sessionStorage.getItem('department')).departmentid}&fromDate=${fromDate}&toDate=${toDate}`, {}, FwServices.defaultTimeout, function onSuccess(response) {
                try {
                    if (axisFormat === 'TWODGDEC') {
                        response.options.scales.yAxes[0].ticks.userCallback = self.commaTwoDecimal
                    } else {
                        response.options.scales.yAxes[0].ticks.userCallback = self.commaDelimited
                    }
                    if (type !== '') {
                        response.type = type
                    }
                    if (response.type === 'pie') {
                        delete response.options.legend;
                        delete response.options.scales;
                    }
                    if (response.type !== 'pie') {
                        response.options.scales.xAxes[0].ticks.autoSkip = false;
                        if (dataFormat === 'TWODGDEC') {
                            response.options.tooltips = {
                                'callbacks': {
                                    'label': self.commaTwoDecimal2
                                }
                            };
                        } else {
                            response.options.tooltips = {
                                'callbacks': {
                                    'label': self.commaDelimited2
                                }
                            };
                        }
                    }
                    if (response.data.labels.length > 10 && response.type !== 'pie') {
                        response.options.scales.xAxes[0].ticks.minRotation = 70;
                        response.options.scales.xAxes[0].ticks.maxRotation = 70;
                    }
                    var chart = new Chart(widgetcanvas, response);
                    jQuery(widgetcanvas).on('click', function (evt) {
                        var activePoint = chart.getElementAtEvent(evt)[0];
                        var data = activePoint._chart.data;
                        var datasetIndex = activePoint._datasetIndex;
                        var label = data.labels[activePoint._index];
                        var value = data.datasets[datasetIndex].data[activePoint._index];

                        program.getModule(chartpath + label.replace(/ /g, '%20').replace(/\//g, '%2F'));
                    });
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            }, null, jQuery(widgetcanvas));
        });

        var widgetcanvas = $control.find('#' + userWidgetId);   

        jQuery($control).on('click', '#' + userWidgetId + 'fullscreen', function () {
            try {
                var $confirmation = FwConfirmation.renderConfirmation(text, '');
                var $cancel = FwConfirmation.addButton($confirmation, 'Close', true);
                var html = [];
                html.push('<div data-chart="' + apiname + '" class="chart-container" style="overflow:hidden;"><canvas style="padding:5px;" id="' + apiname + 'fullscreen"></canvas></div>');
                FwConfirmation.addControls($confirmation, html.join(''));
                $confirmation.find('.fwconfirmationbox').css('width', '80%')

                var widgetfullscreen = $confirmation.find('#' + apiname + 'fullscreen');  

                FwAppData.apiMethod(true, 'GET', `api/v1/widget/loadbyname/${apiname}?dataPoints=${dataPointCount}&locationId=${JSON.parse(sessionStorage.getItem('location')).locationid}&warehouseId=${JSON.parse(sessionStorage.getItem('warehouse')).warehouseid}&departmentId=${JSON.parse(sessionStorage.getItem('department')).departmentid}&fromDate=${fromDate}&toDate=${toDate}`, {}, FwServices.defaultTimeout, function onSuccess(response) {
                    try {
                        if (axisFormat === 'TWODGDEC') {
                            response.options.scales.yAxes[0].ticks.userCallback = self.commaTwoDecimal
                        } else {
                            response.options.scales.yAxes[0].ticks.userCallback = self.commaDelimited
                        }
                        response.options.responsive = true;
                        if (type !== '') {
                            response.type = type
                        }
                        if (response.type === 'pie') {
                            delete response.options.legend;
                            delete response.options.scales;
                        }
                        if (response.type !== 'pie') {
                            response.options.scales.xAxes[0].ticks.autoSkip = false;
                            if (dataFormat === 'TWODGDEC') {
                                response.options.tooltips = {
                                    'callbacks': {
                                        'label': self.commaTwoDecimal2
                                    }
                                };
                            } else {
                                response.options.tooltips = {
                                    'callbacks': {
                                        'label': self.commaDelimited2
                                    }
                                };
                            }
                        }
                        var chart = new Chart(widgetfullscreen, response);
                        jQuery(widgetfullscreen).on('click', function (evt) {
                            var activePoint = chart.getElementAtEvent(evt)[0];
                            var data = activePoint._chart.data;
                            var datasetIndex = activePoint._datasetIndex;
                            var label = data.labels[activePoint._index];
                            var value = data.datasets[datasetIndex].data[activePoint._index];
                            FwConfirmation.destroyConfirmation($confirmation);
                            program.getModule(chartpath + label.replace(/ /g, '%20').replace(/\//g, '%2F'));
                        });
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                }, null, jQuery(widgetfullscreen));

            } catch (ex) {
                FwFunc.showError(ex);
            }
        })

        FwAppData.apiMethod(true, 'GET', `api/v1/widget/loadbyname/${apiname}?dataPoints=${dataPointCount}&locationId=${JSON.parse(sessionStorage.getItem('location')).locationid}&warehouseId=${JSON.parse(sessionStorage.getItem('warehouse')).warehouseid}&departmentId=${JSON.parse(sessionStorage.getItem('department')).departmentid}&fromDate=${fromDate}&toDate=${toDate}`, {}, FwServices.defaultTimeout, function onSuccess(response) {
            try {
                if (axisFormat === 'TWODGDEC') {
                    response.options.scales.yAxes[0].ticks.userCallback = self.commaTwoDecimal
                } else {
                    response.options.scales.yAxes[0].ticks.userCallback = self.commaDelimited
                }
                if (type !== '') {
                    response.type = type
                }
                if (response.type === 'pie') {
                    delete response.options.legend;
                    delete response.options.scales;
                }
                if (response.type !== 'pie') {
                    response.options.scales.xAxes[0].ticks.autoSkip = false;
                    if (dataFormat === 'TWODGDEC') {
                        response.options.tooltips = {
                            'callbacks': {
                                'label': self.commaTwoDecimal2
                            }
                        };
                    } else {
                        response.options.tooltips = {
                            'callbacks': {
                                'label': self.commaDelimited2
                            }
                        };
                    }
                }
                if (response.data.labels.length > 10 && response.type !== 'pie') {
                    response.options.scales.xAxes[0].ticks.minRotation = 70;
                    response.options.scales.xAxes[0].ticks.maxRotation = 70;
                }
                var chart = new Chart(widgetcanvas, response);
                jQuery(widgetcanvas).on('click', function (evt) {
                    var activePoint = chart.getElementAtEvent(evt)[0];
                    var data = activePoint._chart.data;
                    var datasetIndex = activePoint._datasetIndex;
                    var label = data.labels[activePoint._index];
                    var value = data.datasets[datasetIndex].data[activePoint._index];

                    program.getModule(chartpath + label.replace(/ /g, '%20').replace(/\//g, '%2F'));
                });
            } catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, jQuery(widgetcanvas));
    };
};

var RwHomeController = new RwHome();