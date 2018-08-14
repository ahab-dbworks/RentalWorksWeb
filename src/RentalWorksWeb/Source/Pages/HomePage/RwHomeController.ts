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
                }, null, null);

                $select.on('click', function () {
                    try {
                        var request: any = {};
                        request.UserWidgetId = userWidgetId;
                        request.WidgetType = FwFormField.getValue($confirmation, '.widgettype');
                        request.DataPoints = FwFormField.getValue($confirmation, '.defaultpoints')
                        request.AxisNumberFormatId = FwFormField.getValue($confirmation, '.axisformat')
                        request.DataNumberFormatId = FwFormField.getValue($confirmation, '.dataformat')
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
            for (var i = 0; i < response.Widgets.length; i++) {
                if (response.Widgets[i].selected) {
                    self.renderWidget($dashboard, response.Widgets[i].apiname, response.Widgets[i].widgettype, response.Widgets[i].clickpath, response.Widgets[i].userWidgetId, Math.floor(100 / response.WidgetsPerRow).toString() + '%', response.Widgets[i].text, response.Widgets[i].dataPoints, response.Widgets[i].axisNumberFormat, response.Widgets[i].dataNumberFormat)
                }
            }
        }, null, $control);

        //justin 08/14/2018 user's sound settings
        FwAppData.apiMethod(true, 'GET', 'api/v1/usersettings/' + userId, null, FwServices.defaultTimeout, function onSuccess(response) {
            var sounds = { successSound: "", errorSound: "", notificationSound: "" };
            sounds.successSound = response.SuccessSoundFileName;
            sounds.errorSound = response.ErrorSoundFileName;
            sounds.notificationSound = response.NotificationSoundFileName;
            sessionStorage.setItem('sounds', JSON.stringify(sounds));
        }, null, null);
        
    }

    renderWidget($control, apiname, type, chartpath, userWidgetId, width, text, dataPoints, axisFormat, dataFormat) {
        var self = this;
        var refresh = '<i id="' + apiname + 'refresh" class="chart-refresh material-icons">refresh</i>';
        var settings = '<i id="' + apiname + 'settings" class="chart-settings material-icons">settings</i>';
        var fullscreen = '<i id="' + apiname + 'fullscreen" class="chart-settings material-icons">fullscreen</i>';
        var dataPointCount = 0;

        jQuery($control).append('<div data-chart="' + apiname + '" class="chart-container" style="height:' + width + ';width:' + width + ';"><canvas style="display:inline-block;width:100%;padding:5px;" id="' + apiname + '"></canvas><div class="toolbar">' + fullscreen + refresh + settings + '</div></div>');
        self.buildWidgetSettings(jQuery($control).find('#' + apiname + 'settings'), userWidgetId);

        if (dataPoints > 0) {
            dataPointCount = dataPoints
        }

        jQuery($control).on('click', '#' + apiname + 'refresh', function () {
            FwAppData.apiMethod(true, 'GET', `api/v1/widget/loadbyname/${apiname}?dataPoints=${dataPointCount}&locationId=${JSON.parse(sessionStorage.location).locationid}&warehouseId=${JSON.parse(sessionStorage.warehouse).warehouseid}&departmentId=${JSON.parse(sessionStorage.department).departmentid}`, {}, FwServices.defaultTimeout, function onSuccess(response) {
                try {
                    if (axisFormat === 'TWO DIGIT DECIMAL') {
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
                        if (dataFormat === 'TWO DIGIT DECIMAL') {
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

        var widgetcanvas = $control.find('#' + apiname);   

        jQuery($control).on('click', '#' + apiname + 'fullscreen', function () {
            try {
                var $confirmation = FwConfirmation.renderConfirmation(text, '');
                var $cancel = FwConfirmation.addButton($confirmation, 'Close', true);
                var html = [];
                html.push('<div data-chart="' + apiname + '" class="chart-container" style="overflow:hidden;"><canvas style="padding:5px;" id="' + apiname + 'fullscreen"></canvas></div>');
                FwConfirmation.addControls($confirmation, html.join(''));
                $confirmation.find('.fwconfirmationbox').css('width', '80%')

                var widgetfullscreen = $confirmation.find('#' + apiname + 'fullscreen');  

                FwAppData.apiMethod(true, 'GET', `api/v1/widget/loadbyname/${apiname}?dataPoints=${dataPointCount}&locationId=${JSON.parse(sessionStorage.location).locationid}&warehouseId=${JSON.parse(sessionStorage.warehouse).warehouseid}&departmentId=${JSON.parse(sessionStorage.department).departmentid}`, {}, FwServices.defaultTimeout, function onSuccess(response) {
                    try {
                        if (axisFormat === 'TWO DIGIT DECIMAL') {
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
                            if (dataFormat === 'TWO DIGIT DECIMAL') {
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

        FwAppData.apiMethod(true, 'GET', `api/v1/widget/loadbyname/${apiname}?dataPoints=${dataPointCount}&locationId=${JSON.parse(sessionStorage.location).locationid}&warehouseId=${JSON.parse(sessionStorage.warehouse).warehouseid}&departmentId=${JSON.parse(sessionStorage.department).departmentid}`, {}, FwServices.defaultTimeout, function onSuccess(response) {
            try {
                if (axisFormat === 'TWO DIGIT DECIMAL') {
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
                    if (dataFormat === 'TWO DIGIT DECIMAL') {
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