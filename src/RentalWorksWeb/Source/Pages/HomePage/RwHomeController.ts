class RwHome {

    Module: string = 'RwHome';
    charts: any = [];
    widgets: any = {};
    ordersbyagent: boolean = false;
    dealsbytype: boolean = false;
    billingbyagentbymonth: boolean = false;

    constructor() {
        this.charts = [];
    }

    getHomeScreen() {
        var self = this;
        var applicationOptions = program.getApplicationOptions();
        var screen: any = {};
        screen.$view = jQuery(jQuery('#tmpl-pages-Home').html());

        screen.load = function () {
            var redirectPath = sessionStorage.getItem('redirectPath');
            if (typeof redirectPath === 'string' && redirectPath.length > 0) {
                setTimeout(() => {
                    sessionStorage.removeItem('redirectPath');
                    program.navigate(redirectPath);
                }, 0);
            } else {
                self.loadSettings(screen.$view);
            }
        };

        return screen;
    };

    buildWidgetSettings($chartSettings) {
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
                }, null, $chartSettings);

                $select.on('click', function () {
                    try {
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            } catch (ex) {
                FwFunc.showError(ex);
            }
        })
        return $chartSettings

    }

    loadSettings($control) {
        var self = this;
        var $dashboard = $control.find('.programlogo');
        var userId = JSON.parse(sessionStorage.getItem('userid')).webusersid;

        FwAppData.apiMethod(true, 'GET', 'api/v1/userdashboardsettings/' + userId, null, FwServices.defaultTimeout, function onSuccess(response) {
            for (var i = 0; i < response.Widgets.length; i++) {
                if (response.Widgets[i].selected) {
                    self.renderWidget($dashboard, response.Widgets[i].apiname, response.Widgets[i].defaulttype, response.Widgets[i].clickpath)
                }
            }
        }, null, $control)
        
    }

    renderWidget($control, apiname, type, chartpath) {
        var self = this;
        var refresh = '<i id="' + apiname + 'refresh" class="chart-refresh material-icons">refresh</i>';
        var settings = '<i id="' + apiname + 'settings" class="chart-settings material-icons">settings</i>';

        jQuery($control).append('<div data-chart="' + apiname + '" class="chart-container"><canvas style="padding:5px;" id="' + apiname + '"></canvas>' + refresh + settings + '</div>');
        self.buildWidgetSettings(jQuery($control).find('#' + apiname +'settings'));

        jQuery($control).on('click', '#' + apiname + 'refresh', function () {
            FwAppData.apiMethod(true, 'GET', 'api/v1/widget/loadbyname/' + apiname, {}, FwServices.defaultTimeout, function onSuccess(response) {
                try {
                    if (type !== '') {
                        response.type = type
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
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            }, null, jQuery(widgetcanvas));
        });

        var widgetcanvas = <HTMLCanvasElement>$control.find('#' + apiname);   

        FwAppData.apiMethod(true, 'GET', 'api/v1/widget/loadbyname/' + apiname, {}, FwServices.defaultTimeout, function onSuccess(response) {
            try {
                if (type !== '') {
                    response.type = type
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
            } catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, jQuery(widgetcanvas));
    };
};

var RwHomeController = new RwHome();