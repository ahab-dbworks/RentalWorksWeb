var RwHome = (function () {
    function RwHome() {
        this.Module = 'RwHome';
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
        self.buildWidgetSettings(screen.$view);
        screen.load = function () {
            var redirectPath = sessionStorage.getItem('redirectPath');
            if (typeof redirectPath === 'string' && redirectPath.length > 0) {
                setTimeout(function () {
                    sessionStorage.removeItem('redirectPath');
                    program.navigate(redirectPath);
                }, 0);
            }
            else {
                self.renderBar();
                self.renderPie();
                self.renderHorizontal();
                self.renderGroup();
            }
        };
        return screen;
    };
    ;
    RwHome.prototype.buildWidgetSettings = function ($control) {
        var $chartSettings = $control.find('.chart-settings');
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
    RwHome.prototype.renderBar = function () {
        var canvas = document.getElementById("myChart");
        FwAppData.apiMethod(true, 'GET', 'api/v1/widget/loadbyname/ordersbystatus', {}, FwServices.defaultTimeout, function onSuccess(response) {
            try {
                var chart = new Chart(canvas, response);
                canvas.onclick = function (evt) {
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
        }, null, jQuery(canvas));
    };
    ;
    RwHome.prototype.renderPie = function () {
        var canvas = document.getElementById("myPieChart");
        FwAppData.apiMethod(true, 'GET', 'api/v1/widget/loadbyname/ordersbyagent', {}, FwServices.defaultTimeout, function onSuccess(response) {
            try {
                delete response.options.legend;
                delete response.options.scales;
                var chart = new Chart(canvas, response);
                canvas.onclick = function (evt) {
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
        }, null, jQuery(canvas));
    };
    RwHome.prototype.renderHorizontal = function () {
        var canvas = document.getElementById("myHorizontalChart");
        FwAppData.apiMethod(true, 'GET', 'api/v1/widget/loadbyname/dealsbytype', {}, FwServices.defaultTimeout, function onSuccess(response) {
            try {
                var chart = new Chart(canvas, response);
                canvas.onclick = function (evt) {
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
        }, null, jQuery(canvas));
    };
    ;
    RwHome.prototype.renderGroup = function () {
        var canvas = document.getElementById("myGroupChart");
        FwAppData.apiMethod(true, 'GET', 'api/v1/widget/loadbyname/billingbyagentbymonth', {}, FwServices.defaultTimeout, function onSuccess(response) {
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