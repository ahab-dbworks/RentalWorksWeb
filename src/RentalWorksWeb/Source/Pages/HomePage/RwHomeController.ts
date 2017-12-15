declare var RwMasterController: any;
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
        };          

        return screen;
    };

    renderBar() {
        var ctx = document.getElementById("myChart");
        var myChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: ["Active", "Cancelled", "Closed", "Complete", "Confirmed", "Hold"],
                datasets: [{
                    data: [170, 408, 84, 102, 251, 1],
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.2)',
                        'rgba(54, 162, 235, 0.2)',
                        'rgba(255, 206, 86, 0.2)',
                        'rgba(75, 192, 192, 0.2)',
                        'rgba(153, 102, 255, 0.2)',
                        'rgba(255, 159, 64, 0.2)'
                    ],
                    borderColor: [
                        'rgba(255,99,132,1)',
                        'rgba(54, 162, 235, 1)',
                        'rgba(255, 206, 86, 1)',
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
                    text: 'Orders by Status'
                },
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
        });
    }
};

(window as any).RwHomeController = new RwHome();