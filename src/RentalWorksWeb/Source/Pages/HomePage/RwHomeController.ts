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

        jQuery('title').html('RentalWorks');

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

    buildWidgetSettings($chartSettings, userWidgetId) {
        var self = this;
        $chartSettings.on('click', function () {
            try {
                FwAppData.apiMethod(true, 'GET', 'api/v1/userwidget/' + userWidgetId, null, FwServices.defaultTimeout, function onSuccess(response) {
                    let $confirmation = FwConfirmation.renderConfirmation('Chart Options <div style="font-size:0.8em;">' + response.Widget + '</div>', '');
                    let $select = FwConfirmation.addButton($confirmation, 'Confirm', false);
                    let $cancel = FwConfirmation.addButton($confirmation, 'Cancel', true);

                    let html = [];
                    html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                        html.push('<div class="flexrow">');
                        html.push('<div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield widgettype" data-caption="Chart Type" data-datafield="Widget"></div>');
                        html.push('</div>');
                        html.push('<div class="flexrow">');
                        html.push('<div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield officelocation" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" style="float:left;max-width:400px;"></div>');
                        html.push('</div>');
                        html.push('<div class="flexrow">');
                        html.push('<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield datebehavior" data-caption="Date Behavior" data-datafield="DateBehaviorId" data-displayfield="DateBehavior" data-validationname="WidgetDateBehaviorValidation" style="float:left;width:200px;"></div>');
                        html.push('</div>');
                        html.push('<div class="flexrow">');
                        html.push('<div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield datefield" data-caption="Date Field" data-datafield="DateField" style="display:none;"></div>');
                        html.push('</div>');
                        html.push('<div class="flexrow">');
                        html.push('<div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield fromdate" data-caption="From Date" data-datafield="FromDate" style="display:none;"></div>');
                        html.push('</div>');
                        html.push('<div class="flexrow">');
                        html.push('<div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield todate" data-caption="To Date" data-datafield="ToDate" style="display:none;"></div>');
                        html.push('</div>');
                        html.push('<div class="flexrow">');
                        html.push('<div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield defaultpoints" data-caption="Number of Data Points" data-datafield="DefaultDataPoints"></div>');
                        html.push('</div>');
                        html.push('<div class="flexrow">');
                        html.push('<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield axisformat" data-caption="Axis Number Format" data-datafield="AxisNumberFormatId" data-displayfield="AxisNumberFormat" data-validationname="WidgetNumberFormatValidation" style="float:left;width:200px;"></div>');
                        html.push('</div>');
                        html.push('<div class="flexrow">');
                        html.push('<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield dataformat" data-caption="Data Number Format" data-datafield="DataNumberFormatId" data-displayfield="DataNumberFormat" data-validationname="WidgetNumberFormatValidation" style="float:left;width:200px;"></div>');
                        html.push('</div>');
                    html.push('</div>');
                    FwConfirmation.addControls($confirmation, html.join(''));

                    FwFormField.loadItems($confirmation.find('.widgettype'), [
                        { value: 'bar', text: 'Bar' },
                        { value: 'horizontalBar', text: 'Horizontal Bar' },
                        { value: 'pie', text: 'Pie' }
                    ], true);

                    $confirmation.find('div[data-datafield="DefaultDataPoints"] input').val(response.DataPoints);

                    if (response.AxisNumberFormat !== '') {
                        FwFormField.setValueByDataField($confirmation, 'AxisNumberFormatId', response.AxisNumberFormatId, response.AxisNumberFormat);
                    } else {
                        FwFormField.setValueByDataField($confirmation, 'AxisNumberFormatId', response.DefaultAxisNumberFormatId, response.DefaultAxisNumberFormat);
                    }

                    if (response.DataNumberFormat !== '') {
                        FwFormField.setValueByDataField($confirmation, 'DataNumberFormatId', response.DataNumberFormatId, response.DataNumberFormat);
                    } else {
                        FwFormField.setValueByDataField($confirmation, 'DataNumberFormatId', response.DefaultDataNumberFormatId, response.DefaultDataNumberFormat);
                    }

                    if (response.WidgetType !== '') {
                        FwFormField.setValue($confirmation, '.widgettype', response.WidgetType);
                    } else {
                        FwFormField.setValue($confirmation, '.widgettype', response.DefaultType);
                    }

                    if (response.FromDate !== '') {
                        let fromDate = $confirmation.find('.fromdate');
                        fromDate.show();
                        FwFormField.setValue2(fromDate, response.FromDate)
                    } else if (response.FromDate === '' && response.DefaultFromDate !== '') {
                        let fromDate = $confirmation.find('.fromdate');
                        fromDate.show();
                        FwFormField.setValue2(fromDate, response.DefaultFromDate);
                    }

                    if (response.ToDate !== '') {
                        let toDate = $confirmation.find('.todate');
                        toDate.show();
                        FwFormField.setValue2(toDate, response.ToDate)
                    } else if (response.ToDate === '' && response.DefaultToDate !== '') {
                        let toDate = $confirmation.find('.todate');
                        toDate.show();
                        FwFormField.setValue2(toDate, response.DefaultToDate);
                    }

                    $confirmation.find('div[data-datafield="DateBehaviorId"]').on('change', function () {
                        let selected = FwFormField.getValue2(jQuery(this));
                        self.setDateBehaviorFields($confirmation, selected);
                    });
                    FwFormField.setValueByDataField($confirmation, 'DateBehaviorId', response.DateBehaviorId, response.DateBehavior);
                    FwFormField.setValueByDataField($confirmation, 'OfficeLocationId', response.OfficeLocationId, response.OfficeLocation);

                    let dateFields = response.DateFields.split(',');
                    let dateFieldDisplays = response.DateFieldDisplayNames.split(',');
                    let dateFieldSelectArray = [];
                    
                    for (var i = 0; i < dateFields.length; i++) {
                        dateFieldSelectArray.push({
                            'value': dateFields[i],
                            'text': dateFieldDisplays[i]
                        })
                    }
                    FwFormField.loadItems($confirmation.find('.datefield'), dateFieldSelectArray, true);

                    $select.on('click', function () {
                        try {
                            var request: any = {};
                            request.UserWidgetId = userWidgetId;
                            request.WidgetType = FwFormField.getValue($confirmation, '.widgettype');
                            request.DataPoints = FwFormField.getValue($confirmation, '.defaultpoints');
                            request.AxisNumberFormatId = FwFormField.getValue($confirmation, '.axisformat');
                            request.DataNumberFormatId = FwFormField.getValue($confirmation, '.dataformat');
                            request.DateBehaviorId = FwFormField.getValue($confirmation, '.datebehavior');
                            request.DateField = FwFormField.getValue($confirmation, '.datefield');
                            request.FromDate = FwFormField.getValue($confirmation, '.fromdate');
                            request.ToDate = FwFormField.getValue($confirmation, '.todate');
                            request.OfficeLocationId = FwFormField.getValue($confirmation, '.officelocation');
                            FwAppData.apiMethod(true, 'POST', 'api/v1/userwidget/', request, FwServices.defaultTimeout, function onSuccess(response) {
                                FwNotification.renderNotification('SUCCESS', 'Widget Chart Type Updated');
                                FwConfirmation.destroyConfirmation($confirmation);
                                program.navigate('home');
                            }, function onError(response) {
                                FwFunc.showError(response);
                                FwFormField.enable($confirmation.find('.fwformfield'));
                            }, $chartSettings);
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    })
                }, null, null);
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
            let hiddenCounter = 0;
            let dashboardButton = '<div class="flexrow" style="max-width:none;justify-content:center"><div class="fwformcontrol dashboardsettings" data-type="button" style="flex:0 1 350px;margin:75px 0 0 10px;text-align:center;">You have no widgets yet - Add some now!</div></div>';
            if (hiddenCounter === response.UserWidgets.length) {
                jQuery($control).append(dashboardButton);
                jQuery($control).find('.dashboardsettings').on('click', e => {
                    program.navigate('module/dashboardsettings');
                });
            }
            for (var i = 0; i < response.UserWidgets.length; i++) {
                if (response.UserWidgets[i].selected) {
                    response.UserWidgets[i].width = Math.floor(100 / response.WidgetsPerRow).toString() + '%',
                    self.renderWidget($dashboard, response.UserWidgets[i]);
                } else {
                    hiddenCounter++;
                }
            }
        }, null, $control);
    }

    renderWidget($control, widgetData) {
        var self = this;
        var refresh = '<i id="' + widgetData.userWidgetId + 'refresh" class="chart-refresh material-icons">refresh</i>';
        var settings = '<i id="' + widgetData.userWidgetId + 'settings" class="chart-settings material-icons">settings</i>';
        var fullscreen = '<i id="' + widgetData.userWidgetId + 'fullscreen" class="chart-settings material-icons">fullscreen</i>';
        var dataPointCount = 0;

        jQuery($control).append('<div data-chart="' + widgetData.apiname + '" class="chart-container ' + widgetData.userWidgetId + '" style="height:' + widgetData.width + ';width:' + widgetData.width + ';"><canvas style="display:inline-block;width:100%;padding:5px;" id="' + widgetData.userWidgetId + '"></canvas><div class="officebar">' + widgetData.OfficeLocationCode + '</div><div class="toolbar">' + fullscreen + refresh + settings + '</div></div>');
        self.buildWidgetSettings(jQuery($control).find('#' + widgetData.userWidgetId + 'settings'), widgetData.userWidgetId);

        if (widgetData.dataPoints > 0) {
            dataPointCount = widgetData.dataPoints
        }

        jQuery($control).on('click', '#' + widgetData.userWidgetId + 'refresh', function () {
            FwAppData.apiMethod(true, 'GET', `api/v1/widget/loadbyname/${widgetData.apiname}?dataPoints=${dataPointCount}&locationId=${widgetData.OfficeLocationId}&warehouseId=${JSON.parse(sessionStorage.getItem('warehouse')).warehouseid}&departmentId=${JSON.parse(sessionStorage.getItem('department')).departmentid}&DateBehaviorId=${widgetData.dateBehaviorId}&fromDate=${widgetData.fromDate}&toDate=${widgetData.toDate}&datefield=${widgetData.dateField}`, {}, FwServices.defaultTimeout, function onSuccess(response) {
                try {
                    let titleArray = [];
                    titleArray.push(response.options.title.text);
                    if (response.fromDate !== undefined && response.fromDate === response.toDate) {
                        titleArray.push(moment(response.fromDate).format('l'));
                    } else if (response.fromDate !== undefined && response.fromDate !== response.toDate && widgetData.dateBehaviorId === 'SINGLEDATESPECIFICDATE') {
                        titleArray.push(moment(response.fromDate).format('l'));
                    } else if (response.fromDate !== undefined && response.fromDate !== response.toDate) {
                        titleArray.push(moment(response.fromDate).format('l') + ' - ' + moment(response.toDate).format('l'));
                    }

                    if (response.dateBehaviorId === 'NONE' || response.dateBehaviorId === undefined) {
                        titleArray.pop();
                    }

                    response.options.title.text = titleArray;

                    switch (widgetData.axisNumberFormatId) {
                        case 'TWODGDEC':
                            if (response.type !== 'horizontalBar') {
                                response.options.scales.yAxes[0].ticks.userCallback = self.commaTwoDecimal
                            } else {
                                response.options.scales.xAxes[0].ticks.userCallback = self.commaTwoDecimal
                            }
                            break;
                        case 'TWDIGPCT':
                            if (response.type !== 'horizontalBar') {
                                response.options.scales.yAxes[0].ticks.userCallback = self.commaTwoDecimalPercent
                            } else {
                                response.options.scales.xAxes[0].ticks.userCallback = self.commaTwoDecimalPercent
                            }
                            break;
                        case 'WHOLENBR':
                            if (response.type !== 'horizontalBar') {
                                response.options.scales.yAxes[0].ticks.userCallback = self.commaDelimited
                            } else {
                                response.options.scales.xAxes[0].ticks.userCallback = self.commaDelimited
                            }
                            break;
                        case 'WHNUMPCT':
                            if (response.type !== 'horizontalBar') {
                                response.options.scales.yAxes[0].ticks.userCallback = self.commaDelimitedPercent
                            } else {
                                response.options.scales.xAxes[0].ticks.userCallback = self.commaDelimitedPercent
                            }
                            break;
                    }

                    if (widgetData.widgettype !== '') {
                        response.type = widgetData.widgettype
                    }

                    if (response.type === 'pie') {
                        delete response.options.legend;
                        delete response.options.scales;
                    } else {
                        response.options.scales.xAxes[0].ticks.autoSkip = false;
                    }

                    if (response.type !== 'pie') {
                        switch (widgetData.dataNumberFormatId) {
                            case 'TWODGDEC':
                                response.options.tooltips = {
                                    'callbacks': {
                                        'label': self.commaTwoDecimalData
                                    }
                                };
                                break;
                            case 'TWDIGPCT':
                                response.options.tooltips = {
                                    'callbacks': {
                                        'label': self.commaTwoDecimalPercentData
                                    }
                                };
                                break;
                            case 'WHOLENBR':
                                response.options.tooltips = {
                                    'callbacks': {
                                        'label': self.commaDelimitedData
                                    }
                                };
                                break;
                            case 'WHNUMPCT':
                                response.options.tooltips = {
                                    'callbacks': {
                                        'label': self.commaDelimitedPercentData
                                    }
                                };
                                break;
                        }
                    }

                    if (response.data.labels.length > 10 && response.type !== 'pie') {
                        response.options.scales.xAxes[0].ticks.minRotation = 70;
                        response.options.scales.xAxes[0].ticks.maxRotation = 70;
                    }
                    var chart = new Chart(widgetcanvas, response);
                    jQuery(widgetcanvas).on('click', function (evt) {
                        var activePoint = chart.getElementAtEvent(evt)[0];
                        var data = activePoint._chart.data;
                        var datasetIndex = activePoint._datasetIndex;
                        var label = data.labels[activePoint._index];
                        var value = data.datasets[datasetIndex].data[activePoint._index];

                        program.getModule(widgetData.clickpath + label.replace(/ /g, '%20').replace(/\//g, '%2F'));
                    });
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            }, null, jQuery(widgetcanvas));
        });

        var widgetcanvas = $control.find('#' + widgetData.userWidgetId);   

        jQuery($control).on('click', '#' + widgetData.userWidgetId + 'fullscreen', function () {
            try {
                var $confirmation = FwConfirmation.renderConfirmation(widgetData.text, '');
                var $cancel = FwConfirmation.addButton($confirmation, 'Close', true);
                var html = [];
                html.push('<div data-chart="' + widgetData.apiname + '" class="chart-container" style="overflow:hidden;"><canvas style="padding:5px;" id="' + widgetData.apiname + 'fullscreen"></canvas><div class="fullscreenofficebar">' + widgetData.OfficeLocationCode + '</div></div>');
                FwConfirmation.addControls($confirmation, html.join(''));
                $confirmation.find('.fwconfirmationbox').css('width', '80%')

                var widgetfullscreen = $confirmation.find('#' + widgetData.apiname + 'fullscreen');  

                FwAppData.apiMethod(true, 'GET', `api/v1/widget/loadbyname/${widgetData.apiname}?dataPoints=${dataPointCount}&locationId=${widgetData.OfficeLocationId}&warehouseId=${JSON.parse(sessionStorage.getItem('warehouse')).warehouseid}&departmentId=${JSON.parse(sessionStorage.getItem('department')).departmentid}&DateBehaviorId=${widgetData.dateBehaviorId}&fromDate=${widgetData.fromDate}&toDate=${widgetData.toDate}&datefield=${widgetData.dateField}`, {}, FwServices.defaultTimeout, function onSuccess(response) {
                    try {
                        let titleArray = [];
                        titleArray.push(response.options.title.text);
                        if (response.fromDate !== undefined && response.fromDate === response.toDate) {
                            titleArray.push(moment(response.fromDate).format('l'));
                        } else if (response.fromDate !== undefined && response.fromDate !== response.toDate && widgetData.dateBehaviorId === 'SINGLEDATESPECIFICDATE') {
                            titleArray.push(moment(response.fromDate).format('l'));
                        } else if (response.fromDate !== undefined && response.fromDate !== response.toDate) {
                            titleArray.push(moment(response.fromDate).format('l') + ' - ' + moment(response.toDate).format('l'));
                        }

                        if (response.dateBehaviorId === 'NONE' || response.dateBehaviorId === undefined) {
                            titleArray.pop();
                        }

                        response.options.title.text = titleArray;

                        if (widgetData.axisNumberFormatId === 'TWODGDEC') {
                            response.options.scales.yAxes[0].ticks.userCallback = self.commaTwoDecimal
                        } else {
                            response.options.scales.yAxes[0].ticks.userCallback = self.commaDelimited
                        }
                        response.options.responsive = true;
                        if (widgetData.widgettype !== '') {
                            response.type = widgetData.widgettype
                        }
                        if (response.type === 'pie') {
                            delete response.options.legend;
                            delete response.options.scales;
                        }
                        if (response.type !== 'pie') {
                            response.options.scales.xAxes[0].ticks.autoSkip = false;
                            if (widgetData.dataNumberFormatId === 'TWODGDEC') {
                                response.options.tooltips = {
                                    'callbacks': {
                                        'label': self.commaTwoDecimalData
                                    }
                                };
                            } else {
                                response.options.tooltips = {
                                    'callbacks': {
                                        'label': self.commaDelimitedData
                                    }
                                };
                            }
                        }
                        var chart = new Chart(widgetfullscreen, response);
                        jQuery(widgetfullscreen).on('click', function (evt) {
                            var activePoint = chart.getElementAtEvent(evt)[0];
                            var data = activePoint._chart.data;
                            var datasetIndex = activePoint._datasetIndex;
                            var label = data.labels[activePoint._index];
                            var value = data.datasets[datasetIndex].data[activePoint._index];
                            FwConfirmation.destroyConfirmation($confirmation);
                            program.getModule(widgetData.clickpath + label.replace(/ /g, '%20').replace(/\//g, '%2F'));
                        });
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                }, null, jQuery(widgetfullscreen));

            } catch (ex) {
                FwFunc.showError(ex);
            }
        })

        FwAppData.apiMethod(true, 'GET', `api/v1/widget/loadbyname/${widgetData.apiname}?dataPoints=${dataPointCount}&locationId=${widgetData.OfficeLocationId}&warehouseId=${JSON.parse(sessionStorage.getItem('warehouse')).warehouseid}&departmentId=${JSON.parse(sessionStorage.getItem('department')).departmentid}&DateBehaviorId=${widgetData.dateBehaviorId}&fromDate=${widgetData.fromDate}&toDate=${widgetData.toDate}&datefield=${widgetData.dateField}`, {}, FwServices.defaultTimeout, function onSuccess(response) {
            try {
                let titleArray = [];
                titleArray.push(response.options.title.text);
                if (response.fromDate !== undefined && response.fromDate === response.toDate) {
                    titleArray.push(moment(response.fromDate).format('l'));
                } else if (response.fromDate !== undefined && response.fromDate !== response.toDate && widgetData.dateBehaviorId === 'SINGLEDATESPECIFICDATE') {
                    titleArray.push(moment(response.fromDate).format('l'));
                } else if (response.fromDate !== undefined && response.fromDate !== response.toDate) {
                    titleArray.push(moment(response.fromDate).format('l') + ' - ' + moment(response.toDate).format('l'));
                }

                if (response.dateBehaviorId === 'NONE' || response.dateBehaviorId === undefined) {
                    titleArray.pop();
                }

                response.options.title.text = titleArray;

                switch (widgetData.axisNumberFormatId) {
                    case 'TWODGDEC':
                        if (response.type !== 'horizontalBar') {
                            response.options.scales.yAxes[0].ticks.userCallback = self.commaTwoDecimal
                        } else {
                            response.options.scales.xAxes[0].ticks.userCallback = self.commaTwoDecimal
                        }
                        break;
                    case 'TWDIGPCT':
                        if (response.type !== 'horizontalBar') {
                            response.options.scales.yAxes[0].ticks.userCallback = self.commaTwoDecimalPercent
                        } else {
                            response.options.scales.xAxes[0].ticks.userCallback = self.commaTwoDecimalPercent
                        }
                        break;
                    case 'WHOLENBR':
                        if (response.type !== 'horizontalBar') {
                            response.options.scales.yAxes[0].ticks.userCallback = self.commaDelimited
                        } else {
                            response.options.scales.xAxes[0].ticks.userCallback = self.commaDelimited
                        }
                        break;
                    case 'WHNUMPCT':
                        if (response.type !== 'horizontalBar') {
                            response.options.scales.yAxes[0].ticks.userCallback = self.commaDelimitedPercent
                        } else {
                            response.options.scales.xAxes[0].ticks.userCallback = self.commaDelimitedPercent
                        }
                        break;
                }

                if (widgetData.widgettype !== '') {
                    response.type = widgetData.widgettype
                }

                if (response.type === 'pie') {
                    delete response.options.legend;
                    delete response.options.scales;
                } else {
                    response.options.scales.xAxes[0].ticks.autoSkip = false;
                }

                if (response.type !== 'pie') {
                    switch (widgetData.dataNumberFormatId) {
                        case 'TWODGDEC':
                            response.options.tooltips = {
                                'callbacks': {
                                    'label': self.commaTwoDecimalData
                                }
                            };
                            break;
                        case 'TWDIGPCT':
                            response.options.tooltips = {
                                'callbacks': {
                                    'label': self.commaTwoDecimalPercentData
                                }
                            };
                            break;
                        case 'WHOLENBR':
                            response.options.tooltips = {
                                'callbacks': {
                                    'label': self.commaDelimitedData
                                }
                            };
                            break;
                        case 'WHNUMPCT':
                            response.options.tooltips = {
                                'callbacks': {
                                    'label': self.commaDelimitedPercentData
                                }
                            };
                            break;
                    }
                }
                if (response.data.labels.length > 10 && response.type !== 'pie') {
                    response.options.scales.xAxes[0].ticks.minRotation = 70;
                    response.options.scales.xAxes[0].ticks.maxRotation = 70;
                }
                var chart = new Chart(widgetcanvas, response);
                jQuery(widgetcanvas).on('click', function (evt) {
                    var activePoint = chart.getElementAtEvent(evt)[0];
                    var data = activePoint._chart.data;
                    var datasetIndex = activePoint._datasetIndex;
                    var label = data.labels[activePoint._index];
                    var value = data.datasets[datasetIndex].data[activePoint._index];

                    program.getModule(widgetData.clickpath + label.replace(/ /g, '%20').replace(/\//g, '%2F'));
                });
            } catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, jQuery(widgetcanvas));
    };
    //----------------------------------------------------------------------------------------------
    setDateBehaviorFields($control, DateBehavior) {
        let fromDate = $control.find('.fromdate');
        let toDate = $control.find('.todate');
        let fromDateField = $control.find('div[data-datafield="FromDate"] > .fwformfield-caption');
        let dateField = $control.find('.datefield');

        if (DateBehavior === 'NONE') {
            fromDate.hide();
            toDate.hide();
            dateField.hide();
        } else if (DateBehavior === 'SINGLEDATEYESTERDAY' || DateBehavior === 'SINGLEDATETODAY' || DateBehavior === 'SINGLEDATETOMORROW' || DateBehavior === 'DATERANGEPRIORWEEK' || DateBehavior === 'DATERANGECURRENTWEEK' || DateBehavior === 'DATERANGENEXTWEEK' || DateBehavior === 'DATERANGEPRIORMONTH' || DateBehavior === 'DATERANGECURRENTMONTH' || DateBehavior === 'DATERANGENEXTMONTH' || DateBehavior === 'DATERANGEPRIORYEAR' || DateBehavior === 'DATERANGECURRENTYEAR' || DateBehavior === 'DATERANGENEXTYEAR' || DateBehavior === 'DATERANGEYEARTODATE') {
            fromDate.hide();
            toDate.hide();
            dateField.show();
        } else if (DateBehavior === 'SINGLEDATESPECIFICDATE') {
            fromDateField.text('Date');
            fromDate.show();
            toDate.hide();
            dateField.show();
        } else if (DateBehavior === 'DATERANGESPECIFICDATES') {
            fromDateField.text('From Date');
            fromDate.show();
            toDate.show();
            dateField.show();
        }
    }
    //----------------------------------------------------------------------------------------------
    // TO DO - format these all functions below into more modularized code to input to widgets.

    commaDelimited = function (value, index, values) {
        value = value.toString();
        return value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
    };

    commaDelimitedData = function (tooltipItem, data) {
        var value = data.datasets[0].data[tooltipItem.index];
        value = value.toString();
        return value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
    }

    commaTwoDecimal = function (value, index, values) {
        value = value.toString();
        if (value.charAt(value.length - 3) !== '.' && !isNaN(value)) {
            return value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,") + '.00'
        } else {
            return value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
        }
    };

    commaTwoDecimalData = function (tooltipItem, data) {
        var value = data.datasets[0].data[tooltipItem.index];
        value = value.toString();
        if (value.charAt(value.length - 3) === '.') {
            return value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
        } else {
            return value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,") + '.00'
        }
    }

    commaTwoDecimalPercent = function (value, index, values) {
        value = value.toString();
        if (value.charAt(value.length - 3) !== '.' && !isNaN(value)) {
            return value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,") + '.00%'
        } else {
            return value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
        }
    };

    commaTwoDecimalPercentData = function (tooltipItem, data) {
        var value = data.datasets[0].data[tooltipItem.index];
        value = value.toString();
        if (value.charAt(value.length - 3) === '.') {
            return value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
        } else {
            return value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,") + '.00%'
        }
    }

    commaDelimitedPercent = function (value, index, values) {
        value = value.toString();
        if (value.charAt(value.length - 3) !== '.' && !isNaN(value)) {
            return value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,") + '%'
        } else {
            return value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
        }
    };

    commaDelimitedPercentData = function (tooltipItem, data) {
        var value = data.datasets[0].data[tooltipItem.index];
        value = value.toString();
        return value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,") + '%'
    }

};

var RwHomeController = new RwHome();