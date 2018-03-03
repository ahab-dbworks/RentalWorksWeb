class RwHome {
    Module: string = 'RwHome';
    getHomeScreen() {
        var self = this;
        var applicationOptions = program.getApplicationOptions();
        var properties = {};
        var screen: any = {};
        var $filemenu = jQuery('.fwfilemenu');
        if ($filemenu.length === 0) {
            var viewModel = {
                htmlPageBody: jQuery('#tmpl-pages-Home').html()
            };
            screen.$view = RwMasterController.getMasterView(viewModel, properties);
        } else {
            screen.$view = jQuery(jQuery('#tmpl-pages-Home').html());
        }
        screen.viewModel = viewModel;
        screen.properties = properties;

        self.buildWidgetSettings(screen.$view);

        screen.load = function () {
            var redirectPath = sessionStorage.getItem('redirectPath');
            if (typeof redirectPath === 'string' && redirectPath.length > 0) {
                setTimeout(() => {
                    sessionStorage.removeItem('redirectPath');
                    program.navigate(redirectPath);
                }, 0);
            } else {
                self.renderBar();
                self.renderPie();
                self.renderHorizontal();
                self.renderGroup();
            }
        };

        return screen;
    };

    buildWidgetSettings($control) {
        var $chartSettings = $control.find('.chart-settings');

        $chartSettings.on('click', function () {
            try {
                var $confirmation = FwConfirmation.renderConfirmation('Chart Options', '');
                var $select = FwConfirmation.addButton($confirmation, 'Confirm', false);
                var $cancel = FwConfirmation.addButton($confirmation, 'Cancel', true);
                var widgetName = jQuery(this).parent().data('chart')
                
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
                }, null, $control)


                //$confirmation.find('div[data-datafield="Location"] input.fwformfield-text').val(userlocation.location);
                //$confirmation.find('div[data-datafield="Location"] input.fwformfield-value').val(userlocation.locationid);
                //$confirmation.find('div[data-datafield="Warehouse"] input.fwformfield-text').val(userwarehouse.warehouse);
                //$confirmation.find('div[data-datafield="Warehouse"] input.fwformfield-value').val(userwarehouse.warehouseid);
                //$confirmation.find('div[data-datafield="Department"] input.fwformfield-text').val(userdepartment.department);
                //$confirmation.find('div[data-datafield="Department"] input.fwformfield-value').val(userdepartment.departmentid);



                $select.on('click', function () {
                    try {
                        //var valid = true;
                        //var location = $confirmation.find('div[data-datafield="Location"] .fwformfield-value').val();
                        //var warehouse = $confirmation.find('div[data-datafield="Warehouse"] .fwformfield-value').val();
                        //var department = $confirmation.find('div[data-datafield="Department"] .fwformfield-value').val();
                        //if (location == '') {
                        //    $confirmation.find('div[data-datafield="Location"]').addClass('error');
                        //    valid = false;
                        //}
                        //if (warehouse == '') {
                        //    $confirmation.find('div[data-datafield="Warehouse"]').addClass('error');
                        //    valid = false;
                        //}
                        //if (department == '') {
                        //    $confirmation.find('div[data-datafield="Department"]').addClass('error');
                        //    valid = false;
                        //}
                        //if (valid) {
                        //    var request = {
                        //        location: location,
                        //        warehouse: warehouse,
                        //        department: department
                        //    };
                        //    RwServices.session.updatelocation(request, function (response) {
                        //        sessionStorage.setItem('authToken', response.authToken);
                        //        sessionStorage.setItem('location', JSON.stringify(response.location));
                        //        sessionStorage.setItem('warehouse', JSON.stringify(response.warehouse));
                        //        sessionStorage.setItem('department', JSON.stringify(response.department));
                        //        sessionStorage.setItem('userid', JSON.stringify(response.webusersid));
                        //        FwConfirmation.destroyConfirmation($confirmation);
                        //        program.navigate('home');
                        //    });
                        //}
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })


    }

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

var RwHomeController = new RwHome();