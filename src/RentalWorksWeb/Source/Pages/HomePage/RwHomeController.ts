declare var RwMasterController: any;
declare var FwAppData: any;
declare var program: any;
declare var Chart: any;
//----------------------------------------------------------------------------------------------
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
        var ctx = document.getElementById("myChart");

        FwAppData.apiMethod(true, 'GET', 'api/v1/widget/orderbystatus', {}, FwServices.defaultTimeout, function onSuccess(response) {
            try {
                var myChart = new Chart(ctx, response);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    };

    renderPie() {
        var pie = document.getElementById("myPieChart");
        var myPie = new Chart(pie, {
            type: 'pie',
            data: {
                labels: ["Garrett, Tyler", "Guirguis, Ahab", "Hoffman, Justin"],
                datasets: [{
                    data: [33, 356, 623],
                    backgroundColor: [
                        'rgba(153, 102, 255, 0.2)',
                        'rgba(75, 192, 192, 0.2)',
                        'rgba(54, 162, 235, 0.2)',
                        'rgba(255, 206, 86, 0.2)',
                        'rgba(255, 159, 64, 0.2)'
                    ],
                    borderColor: [
                        'rgba(153, 102, 255, 1)',
                        'rgba(75, 192, 192, 1)',
                        'rgba(54, 162, 235, 1)',
                        'rgba(255, 206, 86, 1)',
                        'rgba(255, 159, 64, 1)'
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                title: {
                    display: true,
                    text: 'Orders by Agent' 
                },
                maintainAspectRatio: false
            }
        })
    }

    renderHorizontal() {
        var ctx = document.getElementById("myHorizontalChart");
        var myChart = new Chart(ctx, {
            type: 'horizontalBar',
            data: {
                labels: ["Original Show", "Movie", "Customer Rentals"],
                datasets: [{
                    data: [5,3,58],
                    backgroundColor: [
                        'rgba(75, 192, 192, 0.2)',
                        'rgba(153, 102, 255, 0.2)',
                        'rgba(255, 159, 64, 0.2)'
                    ],
                    borderColor: [
                        'rgba(75, 192, 192, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 159, 64, 1)'
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                title: {
                    display: true,
                    text: 'Deals by Type'
                },
                legend: {
                    display: false
                },
                scales: {
                    xAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                },
                responsive: true,
                maintainAspectRatio: false
            }
        });
    };
    renderGroup() {
        var ctx = document.getElementById("myGroupChart");
        var myChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: ["January", "February", "March", "April"],
                datasets: [{
                        label: "Hoffman, Justin",
                        backgroundColor: 'rgba(255, 99, 132, 0.7)',
                        data: [5000, 6000, 8000, 4000]
                    }, {
                        label: "Wang, Oliver",
                        backgroundColor: 'rgba(54, 162, 235, 0.7)',
                        data: [5000, 8000, 10000, 9000]
                    }, {
                        label: "Hoang, Jason",
                        backgroundColor: 'rgba(255, 206, 86, 0.7)',
                        data: [10000, 9000, 4500, 9000]
                    }, {
                        label: "Sandoval, Antonio",
                        backgroundColor: 'rgba(75, 192, 192, 0.7)',
                        data: [9000, 9000, 9000, 9000]
                    }]
            },
            options: {
                title: {
                    display: true,
                    text: 'Billing'
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                },
                responsive: true,
                maintainAspectRatio: false
            }
        });
    };
};

(window as any).RwHomeController = new RwHome();