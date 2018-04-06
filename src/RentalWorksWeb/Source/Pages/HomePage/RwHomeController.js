var RwHome = (function () {
    function RwHome() {
        this.Module = 'RwHome';
        this.charts = [];
        this.widgets = {};
        this.ordersbyagent = false;
        this.dealsbytype = false;
        this.billingbyagentbymonth = false;
        this.charts = [];
    }
    RwHome.prototype.getHomeScreen = function () {
        var self = this;
        var applicationOptions = program.getApplicationOptions();
        var screen = {};
        screen.$view = jQuery(jQuery('#tmpl-pages-Home').html());
        screen.load = function () {
            var redirectPath = sessionStorage.getItem('redirectPath');
            if (typeof redirectPath === 'string' && redirectPath.length > 0) {
                setTimeout(function () {
                    sessionStorage.removeItem('redirectPath');
                    program.navigate(redirectPath);
                }, 0);
            }
            else {
                self.loadSettings(screen.$view);
            }
        };
        return screen;
    };
    ;
    RwHome.prototype.buildWidgetSettings = function ($chartSettings, userWidgetId) {
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
                    html.push('</div>');
                    FwConfirmation.addControls($confirmation, html.join(''));
                    FwFormField.loadItems($confirmation.find('.widgettype'), [
                        { value: 'bar', text: 'Bar' },
                        { value: 'horizontalBar', text: 'Horizontal Bar' },
                        { value: 'pie', text: 'Pie' }
                    ], true);
                }, null, null);
                $select.on('click', function () {
                    try {
                        var request = {};
                        request.UserWidgetId = userWidgetId;
                        request.WidgetType = FwFormField.getValue($confirmation, '.widgettype');
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
    };
    RwHome.prototype.loadSettings = function ($control) {
        var self = this;
        var $dashboard = $control.find('.programlogo');
        var userId = JSON.parse(sessionStorage.getItem('userid')).webusersid;
        FwAppData.apiMethod(true, 'GET', 'api/v1/userdashboardsettings/' + userId, null, FwServices.defaultTimeout, function onSuccess(response) {
            for (var i = 0; i < response.Widgets.length; i++) {
                if (response.Widgets[i].selected) {
                    self.renderWidget($dashboard, response.Widgets[i].apiname, response.Widgets[i].widgettype, response.Widgets[i].clickpath, response.Widgets[i].userWidgetId);
                }
            }
        }, null, $control);
    };
    RwHome.prototype.renderWidget = function ($control, apiname, type, chartpath, userWidgetId) {
        var self = this;
        var refresh = '<i id="' + apiname + 'refresh" class="chart-refresh material-icons">refresh</i>';
        var settings = '<i id="' + apiname + 'settings" class="chart-settings material-icons">settings</i>';
        jQuery($control).append('<div data-chart="' + apiname + '" class="chart-container"><canvas style="padding:5px;" id="' + apiname + '"></canvas>' + refresh + settings + '</div>');
        self.buildWidgetSettings(jQuery($control).find('#' + apiname + 'settings'), userWidgetId);
        jQuery($control).on('click', '#' + apiname + 'refresh', function () {
            FwAppData.apiMethod(true, 'GET', 'api/v1/widget/loadbyname/' + apiname, {}, FwServices.defaultTimeout, function onSuccess(response) {
                try {
                    if (type !== '') {
                        response.type = type;
                    }
                    if (response.type === 'pie') {
                        delete response.options.legend;
                        delete response.options.scales;
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
        FwAppData.apiMethod(true, 'GET', 'api/v1/widget/loadbyname/' + apiname, {}, FwServices.defaultTimeout, function onSuccess(response) {
            try {
                if (type !== '') {
                    response.type = type;
                }
                if (response.type === 'pie') {
                    delete response.options.legend;
                    delete response.options.scales;
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
    };
    ;
    return RwHome;
}());
;
var RwHomeController = new RwHome();
//# sourceMappingURL=RwHomeController.js.map