class RwHome {
    constructor() {
        this.Module = 'RwHome';
        this.charts = [];
        this.widgets = {};
        this.ordersbyagent = false;
        this.dealsbytype = false;
        this.billingbyagentbymonth = false;
        this.commaDelimited = function (value, index, values) {
            if (typeof value !== 'string' && value > 1) {
                value = value.toString();
                value = value.split(/(?=(?:...)*$)/);
                value = value.join(',');
                return value;
            }
            else if (value < 1) {
                return Math.round(value * 10) / 10;
            }
            else {
                return value;
            }
        };
        this.commaDelimited2 = function (tooltipItem, data) {
            var value = data.datasets[0].data[tooltipItem.index];
            value = value.toString();
            value = value.split(/(?=(?:...)*$)/);
            value = value.join(',');
            return value;
        };
        this.periodDelimited = function (value, index, values) {
            if (typeof value !== 'string' && value > 1) {
                value = value.toString();
                value = value.split(/(?=(?:...)*$)/);
                value = value.join('.');
                return value;
            }
            else if (value < 1) {
                return Math.round(value * 10) / 10;
            }
            else {
                return value;
            }
        };
        this.periodDelimited2 = function (tooltipItem, data) {
            var value = data.datasets[0].data[tooltipItem.index];
            value = value.toString();
            value = value.split(/(?=(?:...)*$)/);
            value = value.join('.');
            return value;
        };
        this.charts = [];
    }
    getHomeScreen() {
        var self = this;
        var applicationOptions = program.getApplicationOptions();
        var screen = {};
        screen.$view = jQuery(jQuery('#tmpl-pages-Home').html());
        screen.load = function () {
            var redirectPath = sessionStorage.getItem('redirectPath');
            if (typeof redirectPath === 'string' && redirectPath.length > 0) {
                setTimeout(() => {
                    sessionStorage.removeItem('redirectPath');
                    program.navigate(redirectPath);
                }, 0);
            }
            else {
                self.loadSettings(screen.$view);
            }
        };
        return screen;
    }
    ;
    buildWidgetSettings($chartSettings, userWidgetId) {
        var self = this;
        $chartSettings.on('click', function () {
            try {
                var $confirmation = FwConfirmation.renderConfirmation('Chart Options', '');
                var $select = FwConfirmation.addButton($confirmation, 'Confirm', false);
                var $cancel = FwConfirmation.addButton($confirmation, 'Cancel', true);
                var widgetName = jQuery(this).parent().data('chart');
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
                    html.push('</div>');
                    FwConfirmation.addControls($confirmation, html.join(''));
                    FwFormField.loadItems($confirmation.find('.widgettype'), [
                        { value: 'bar', text: 'Bar' },
                        { value: 'horizontalBar', text: 'Horizontal Bar' },
                        { value: 'pie', text: 'Pie' }
                    ], true);
                    $confirmation.find('div[data-datafield="DefaultDataPoints"] input').val(response.DataPoints);
                    if (response.WidgetType !== '') {
                        FwFormField.setValue($confirmation, '.widgettype', response.WidgetType);
                    }
                    else {
                        FwFormField.setValue($confirmation, '.widgettype', response.DefaultType);
                    }
                }, null, null);
                $select.on('click', function () {
                    try {
                        var request = {};
                        request.UserWidgetId = userWidgetId;
                        request.WidgetType = FwFormField.getValue($confirmation, '.widgettype');
                        request.DataPoints = FwFormField.getValue($confirmation, '.defaultpoints');
                        FwAppData.apiMethod(true, 'POST', 'api/v1/userwidget/', request, FwServices.defaultTimeout, function onSuccess(response) {
                            FwNotification.renderNotification('SUCCESS', 'Widget Chart Type Updated');
                            FwConfirmation.destroyConfirmation($confirmation);
                            program.navigate('home');
                        }, function onError(response) {
                            FwFunc.showError(response);
                            FwFormField.enable($confirmation.find('.fwformfield'));
                        }, $chartSettings);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        return $chartSettings;
    }
    loadSettings($control) {
        var self = this;
        var $dashboard = $control.find('.programlogo');
        var userId = JSON.parse(sessionStorage.getItem('userid')).webusersid;
        FwAppData.apiMethod(true, 'GET', 'api/v1/userdashboardsettings/' + userId, null, FwServices.defaultTimeout, function onSuccess(response) {
            for (var i = 0; i < response.Widgets.length; i++) {
                if (response.Widgets[i].selected) {
                    self.renderWidget($dashboard, response.Widgets[i].apiname, response.Widgets[i].widgettype, response.Widgets[i].clickpath, response.Widgets[i].userWidgetId, Math.floor(100 / response.WidgetsPerRow).toString() + '%', response.Widgets[i].text, response.Widgets[i].dataPoints);
                }
            }
        }, null, $control);
    }
    renderWidget($control, apiname, type, chartpath, userWidgetId, width, text, dataPoints) {
        var self = this;
        var refresh = '<i id="' + apiname + 'refresh" class="chart-refresh material-icons">refresh</i>';
        var settings = '<i id="' + apiname + 'settings" class="chart-settings material-icons">settings</i>';
        var fullscreen = '<i id="' + apiname + 'fullscreen" class="chart-settings material-icons">fullscreen</i>';
        var dataPointCount = 0;
        jQuery($control).append('<div data-chart="' + apiname + '" class="chart-container" style="height:' + width + ';width:' + width + ';"><canvas style="display:inline-block;width:100%;padding:5px;" id="' + apiname + '"></canvas><div class="toolbar">' + fullscreen + refresh + settings + '</div></div>');
        self.buildWidgetSettings(jQuery($control).find('#' + apiname + 'settings'), userWidgetId);
        if (dataPoints > 0) {
            dataPointCount = dataPoints;
        }
        jQuery($control).on('click', '#' + apiname + 'refresh', function () {
            FwAppData.apiMethod(true, 'GET', `api/v1/widget/loadbyname/${apiname}?dataPoints=${dataPointCount}&locationId=${JSON.parse(sessionStorage.location).locationid}&warehouseId=${JSON.parse(sessionStorage.warehouse).warehouseid}&departmentId=${JSON.parse(sessionStorage.department).departmentid}`, {}, FwServices.defaultTimeout, function onSuccess(response) {
                try {
                    response.options.scales.yAxes[0].ticks.userCallback = self.commaDelimited;
                    if (type !== '') {
                        response.type = type;
                    }
                    if (response.type === 'pie') {
                        delete response.options.legend;
                        delete response.options.scales;
                    }
                    if (response.type !== 'pie') {
                        response.options.scales.xAxes[0].ticks.autoSkip = false;
                        response.options.tooltips = {
                            'callbacks': {
                                'label': self.commaDelimited2
                            }
                        };
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
                }
                catch (ex) {
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
                $confirmation.find('.fwconfirmationbox').css('width', '80%');
                var widgetfullscreen = $confirmation.find('#' + apiname + 'fullscreen');
                FwAppData.apiMethod(true, 'GET', `api/v1/widget/loadbyname/${apiname}?dataPoints=${dataPointCount}&locationId=${JSON.parse(sessionStorage.location).locationid}&warehouseId=${JSON.parse(sessionStorage.warehouse).warehouseid}&departmentId=${JSON.parse(sessionStorage.department).departmentid}`, {}, FwServices.defaultTimeout, function onSuccess(response) {
                    try {
                        response.options.responsive = true;
                        response.options.scales.yAxes[0].ticks.userCallback = self.commaDelimited;
                        if (type !== '') {
                            response.type = type;
                        }
                        if (response.type === 'pie') {
                            delete response.options.legend;
                            delete response.options.scales;
                        }
                        if (response.type !== 'pie') {
                            response.options.scales.xAxes[0].ticks.autoSkip = false;
                            response.options.tooltips = {
                                'callbacks': {
                                    'label': self.commaDelimited2
                                }
                            };
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
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                }, null, jQuery(widgetfullscreen));
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwAppData.apiMethod(true, 'GET', `api/v1/widget/loadbyname/${apiname}?dataPoints=${dataPointCount}&locationId=${JSON.parse(sessionStorage.location).locationid}&warehouseId=${JSON.parse(sessionStorage.warehouse).warehouseid}&departmentId=${JSON.parse(sessionStorage.department).departmentid}`, {}, FwServices.defaultTimeout, function onSuccess(response) {
            try {
                response.options.scales.yAxes[0].ticks.userCallback = self.commaDelimited;
                if (type !== '') {
                    response.type = type;
                }
                if (response.type === 'pie') {
                    delete response.options.legend;
                    delete response.options.scales;
                }
                if (response.type !== 'pie') {
                    response.options.scales.xAxes[0].ticks.autoSkip = false;
                    response.options.tooltips = {
                        'callbacks': {
                            'label': self.commaDelimited2
                        }
                    };
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
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, jQuery(widgetcanvas));
    }
    ;
}
;
var RwHomeController = new RwHome();
//# sourceMappingURL=RwHomeController.js.map