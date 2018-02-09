//----------------------------------------------------------------------------------------------
var RwHome = /** @class */ (function () {
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
        var ctx = document.getElementById("myChart");
        FwAppData.apiMethod(true, 'GET', 'api/v1/widget/loadbyname/ordersbystatus', {}, FwServices.defaultTimeout, function onSuccess(response) {
            try {
                var myChart = new Chart(ctx, response);
                ctx.onclick = function (evt) {
                    var activePoint = myChart.getElementAtEvent(evt)[0];
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
        });
    };
    ;
    RwHome.prototype.renderPie = function () {
        var pie = document.getElementById("myPieChart");
        FwAppData.apiMethod(true, 'GET', 'api/v1/widget/loadbyname/ordersbyagent', {}, FwServices.defaultTimeout, function onSuccess(response) {
            try {
                delete response.options.legend;
                delete response.options.scales;
                var myPie = new Chart(pie, response);
                pie.onclick = function (evt) {
                    var activePoint = myPie.getElementAtEvent(evt)[0];
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
        });
    };
    RwHome.prototype.renderHorizontal = function () {
        var horizontal = document.getElementById("myHorizontalChart");
        FwAppData.apiMethod(true, 'GET', 'api/v1/widget/loadbyname/dealsbytype', {}, FwServices.defaultTimeout, function onSuccess(response) {
            try {
                var myHoriz = new Chart(horizontal, response);
                horizontal.onclick = function (evt) {
                    var activePoint = myHoriz.getElementAtEvent(evt)[0];
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
        });
    };
    ;
    RwHome.prototype.renderGroup = function () {
        //var ctx = document.getElementById("myGroupChart");
        //var myChart = new Chart(ctx, {
        //    type: 'bar',
        //    data: {
        //        labels: ["January", "February", "March", "April"],
        //        datasets: [{
        //                label: "Hoffman, Justin",
        //                backgroundColor: 'rgba(#350C0C)',
        //                data: [5000, 6000, 8000, 4000]
        //            }, {
        //                label: "Wang, Oliver",
        //                backgroundColor: 'rgba(54, 162, 235, 0.7)',
        //                data: [5000, 8000, 10000, 9000]
        //            }, {
        //                label: "Hoang, Jason",
        //                backgroundColor: 'rgba(255, 206, 86, 0.7)',
        //                data: [10000, 9000, 4500, 9000]
        //            }, {
        //                label: "Sandoval, Antonio",
        //                backgroundColor: 'rgba(75, 192, 192, 0.7)',
        //                data: [9000, 9000, 9000, 9000]
        //            }]
        //    },
        //    options: {
        //        title: {
        //            display: true,
        //            text: 'Billing'
        //        },
        //        scales: {
        //            yAxes: [{
        //                ticks: {
        //                    beginAtZero: true
        //                }
        //            }]
        //        },
        //        responsive: true,
        //        maintainAspectRatio: false
        //    }
        //});
        var groupChart = document.getElementById("myGroupChart");
        FwAppData.apiMethod(true, 'GET', 'api/v1/widget/loadbyname/billingbyagentbymonth', {}, FwServices.defaultTimeout, function onSuccess(response) {
            try {
                var myGroup = new Chart(groupChart, response);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    };
    ;
    return RwHome;
}());
;
window.RwHomeController = new RwHome();
//# sourceMappingURL=RwHomeController.js.map