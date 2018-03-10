var RwHome = (function () {
    function RwHome() {
        this.Module = 'RwHome';
        this.charts = [];
        this.ordersbystatus = {};
        this.ordersbyagent = {};
        this.dealsbytype = {};
        this.billingbyagentbymonth = {};
        this.charts = [];
    }
    RwHome.prototype.getHomeScreen = function () {
        var self = this;
        var applicationOptions = program.getApplicationOptions();
        var properties = {};
        var screen = {};
        var $filemenu = jQuery('.fwfilemenu');
        if ($filemenu.length === 0) {
            var viewModel = {
                htmlPageBody: jQuery('#tmpl-pages-Home').html()
            };
            screen.$view = RwMasterController.getMasterView(viewModel, properties);
        }
        else {
            screen.$view = jQuery(jQuery('#tmpl-pages-Home').html());
        }
        screen.viewModel = viewModel;
        screen.properties = properties;
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
        self.buildWidgetSettings(screen.$view);
        return screen;
    };
    ;
    RwHome.prototype.buildWidgetSettings = function ($control) {
        var $chartSettings = $control.find('.chart-settings');
        var self = this;
        $chartSettings.on('click', function () {
            try {
                var $confirmation = FwConfirmation.renderConfirmation('Chart Options', '');
                var $select = FwConfirmation.addButton($confirmation, 'Confirm', false);
                var $cancel = FwConfirmation.addButton($confirmation, 'Cancel', true);
                var widgetName = jQuery(this).parent().data('chart');
                var html = [];
                FwAppData.apiMethod(true, 'GET', 'api/v1/widget/loadbyname/' + widgetName, null, FwServices.defaultTimeout, function onSuccess(response) {
                    html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                    for (var i = 0; i < response.data.labels.length; i++) {
                        html.push('<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                        html.push('  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="' + response.data.labels[i] + '" data-datafield="' + response.data.labels[i] + '"></div>');
                        html.push('</div>');
                    }
                    html.push('</div>');
                    FwConfirmation.addControls($confirmation, html.join(''));
                }, null, $control);
                $select.on('click', function () {
                    try {
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
    };
    RwHome.prototype.loadSettings = function ($control) {
        var self = this;
        var $dashboard = $control.find('.programlogo');
        var userId = JSON.parse(sessionStorage.getItem('userid'));
        $dashboard.append('<div class="chart-row"></div><div class="chart-row"></div>');
        FwAppData.apiMethod(true, 'GET', 'api/v1/userdashboardsettings/' + userId, null, FwServices.defaultTimeout, function onSuccess(response) {
            for (var i = 0; i < response.Widgets.length; i++) {
                if (response.Widgets[i].text === "Orders By Status" && response.Widgets[i].selected) {
                    self.renderBar($dashboard);
                }
                if (response.Widgets[i].text === "Orders By Agent" && response.Widgets[i].selected) {
                    self.renderPie($dashboard);
                }
                if (response.Widgets[i].text === "Deals By Type" && response.Widgets[i].selected) {
                    self.renderHorizontal($dashboard);
                }
                if (response.Widgets[i].text === "Billing By Agent By Month" && response.Widgets[i].selected) {
                    self.renderGroup($dashboard);
                }
            }
        }, null, $control);
    };
    RwHome.prototype.renderBar = function ($control) {
        var self = this;
        for (var i = 0; i < $control.children().length; i++) {
            if (jQuery($control.children()[i]).children().length < 2) {
                jQuery($control.children()[i]).append('<div data-chart="ordersbystatus" class="chart-container"><canvas style="padding:5px;" id="myChart"></canvas><i class="chart-settings material-icons">settings</i></div>');
                break;
            }
        }
        var barCanvas = $control.find('#myChart');
        FwAppData.apiMethod(true, 'GET', 'api/v1/widget/loadbyname/ordersbystatus', {}, FwServices.defaultTimeout, function onSuccess(response) {
            self.ordersbystatus = response;
            try {
                var chart = new Chart(barCanvas, response);
                barCanvas.onclick = function (evt) {
                    var activePoint = chart.getElementAtEvent(evt)[0];
                    var data = activePoint._chart.data;
                    var datasetIndex = activePoint._datasetIndex;
                    var label = data.labels[activePoint._index];
                    var value = data.datasets[datasetIndex].data[activePoint._index];
                    program.getModule('module/order/status/' + label);
                };
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, jQuery(barCanvas));
    };
    ;
    RwHome.prototype.renderPie = function ($control) {
        var self = this;
        for (var i = 0; i < $control.children().length; i++) {
            if (jQuery($control.children()[i]).children().length < 2) {
                jQuery($control.children()[i]).append('<div data-chart="ordersbystatus" class="chart-container"><canvas style="padding:5px;" id="myPieChart"></canvas><i class="chart-settings material-icons">settings</i></div>');
                break;
            }
        }
        var pieCanvas = $control.find('#myPieChart');
        FwAppData.apiMethod(true, 'GET', 'api/v1/widget/loadbyname/ordersbyagent', {}, FwServices.defaultTimeout, function onSuccess(response) {
            self.ordersbyagent = response;
            try {
                delete response.options.legend;
                delete response.options.scales;
                var chart = new Chart(pieCanvas, response);
                pieCanvas.onclick = function (evt) {
                    var activePoint = chart.getElementAtEvent(evt)[0];
                    var data = activePoint._chart.data;
                    var datasetIndex = activePoint._datasetIndex;
                    var label = data.labels[activePoint._index];
                    var value = data.datasets[datasetIndex].data[activePoint._index];
                    program.getModule('module/order/agent/' + label.replace(/ /g, '%20'));
                };
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, jQuery(pieCanvas));
    };
    RwHome.prototype.renderHorizontal = function ($control) {
        var self = this;
        for (var i = 0; i < $control.children().length; i++) {
            if (jQuery($control.children()[i]).children().length < 2) {
                jQuery($control.children()[i]).append('<div data-chart="ordersbystatus" class="chart-container"><canvas style="padding:5px;" id="myHorizontalChart"></canvas><i class="chart-settings material-icons">settings</i></div>');
                break;
            }
        }
        var horizontalCanvas = $control.find('#myHorizontalChart');
        FwAppData.apiMethod(true, 'GET', 'api/v1/widget/loadbyname/dealsbytype', {}, FwServices.defaultTimeout, function onSuccess(response) {
            self.dealsbytype = response;
            try {
                var chart = new Chart(horizontalCanvas, response);
                horizontalCanvas.onclick = function (evt) {
                    var activePoint = chart.getElementAtEvent(evt)[0];
                    var data = activePoint._chart.data;
                    var datasetIndex = activePoint._datasetIndex;
                    var label = data.labels[activePoint._index];
                    var value = data.datasets[datasetIndex].data[activePoint._index];
                    program.getModule('module/deal/deal%20type/' + label.replace(/ /g, '%20'));
                };
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, jQuery(horizontalCanvas));
    };
    ;
    RwHome.prototype.renderGroup = function ($control) {
        var self = this;
        for (var i = 0; i < $control.children().length; i++) {
            if (jQuery($control.children()[i]).children().length < 2) {
                jQuery($control.children()[i]).append('<div data-chart="ordersbystatus" class="chart-container"><canvas style="padding:5px;" id="myGroupChart"></canvas><i class="chart-settings material-icons">settings</i></div>');
                break;
            }
        }
        var canvas = $control.find('#myGroupChart');
        FwAppData.apiMethod(true, 'GET', 'api/v1/widget/loadbyname/billingbyagentbymonth', {}, FwServices.defaultTimeout, function onSuccess(response) {
            self.billingbyagentbymonth = response;
            try {
                var chart = new Chart(canvas, response);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, jQuery(canvas));
    };
    ;
    return RwHome;
}());
;
var RwHomeController = new RwHome();
//# sourceMappingURL=RwHomeController.js.map