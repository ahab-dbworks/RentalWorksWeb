class RwHome {
    getHomeScreen(viewModel, properties) {
        var combinedViewModel, screen, applicationOptions;
        var self = this;
        applicationOptions = program.getApplicationOptions();
        combinedViewModel = jQuery.extend({

        }, viewModel);
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

    renderBar() {
        var canvas = <HTMLCanvasElement> document.getElementById("myChart");
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
            } catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, jQuery(canvas));
    };

    renderPie() {
        var canvas = <HTMLCanvasElement> document.getElementById("myPieChart");
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
            } catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, jQuery(canvas));
    }

    renderHorizontal() {
        var canvas = <HTMLCanvasElement> document.getElementById("myHorizontalChart");
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
            } catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, jQuery(canvas));
    };
    renderGroup() {
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


        var canvas = <HTMLCanvasElement> document.getElementById("myGroupChart");
        FwAppData.apiMethod(true, 'GET', 'api/v1/widget/loadbyname/billingbyagentbymonth', {}, FwServices.defaultTimeout, function onSuccess(response) {
            try {
                var chart = new Chart(canvas, response);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, jQuery(canvas));


    };
};

(window as any).RwHomeController = new RwHome();