var RwHome = (function () {
    function RwHome() {
    }
    RwHome.prototype.getHomeScreen = function (viewModel, properties) {
        var combinedViewModel, screen, applicationOptions;
        var self = this;
        applicationOptions = program.getApplicationOptions();
        combinedViewModel = jQuery.extend({}, viewModel);
        combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-pages-Home').html(), combinedViewModel);
        screen = {};
        screen.$view = RwMasterController.getMasterView(combinedViewModel);
        screen.viewModel = viewModel;
        screen.properties = properties;
        screen.load = function () {
            self.renderBar();
            self.renderPie();
            self.renderHorizontal();
            self.renderGroup();
        };
        return screen;
    };
    ;
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
window.RwHomeController = new RwHome();
//# sourceMappingURL=RwHomeController.js.map