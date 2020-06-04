routes.push({ pattern: /^module\/dashboard$/, action: function (match: RegExpExecArray) { return DashboardController.getModuleScreen(); } });

class Dashboard {
    Module: string = 'Dashboard';
    caption: string = Constants.Modules.Utilities.children.Dashboard.caption;
    nav: string = Constants.Modules.Utilities.children.Dashboard.nav;
    id: string = Constants.Modules.Utilities.children.Dashboard.id;
    charts: any = [];
    widgets: any = {};
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: { datafield: string, search: string }) {
        const screen: any = {};
        //  screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.$view = jQuery(jQuery('#tmpl-pages-Home').html());
        screen.viewModel = {};
        screen.properties = {};

        screen.load = () => {
            this.loadSettings(screen.$view);
        };
        screen.unload = function () { };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    buildWidgetSettings($chartSettings, userWidgetId) {
        $chartSettings.off('click').on('click', e => {
            try {
                FwAppData.apiMethod(true, 'GET', 'api/v1/userwidget/' + userWidgetId, null, FwServices.defaultTimeout, response => {
                    const $confirmation = FwConfirmation.renderConfirmation('Chart Options <div style="font-size:0.8em;">' + response.Widget + '</div>', '');
                    const $select = FwConfirmation.addButton($confirmation, 'Confirm', false);
                    const $cancel = FwConfirmation.addButton($confirmation, 'Cancel', true);
                    FwConfirmation.addControls($confirmation, this.getSettingsHtml());
                    this.loadSettingsFields($confirmation, response);

                    $select.on('click', e => {
                        try {
                            const request: any = {};
                            request.UserWidgetId = userWidgetId;
                            request.WidgetType = FwFormField.getValue($confirmation, '.widgettype');
                            (FwFormField.getValue($confirmation, '.stacked') === 'T') ? request.Stacked = true : request.Stacked = false;
                            request.DataPoints = FwFormField.getValue($confirmation, '.defaultpoints');
                            request.AxisNumberFormatId = FwFormField.getValue($confirmation, '.axisformat');
                            request.DataNumberFormatId = FwFormField.getValue($confirmation, '.dataformat');
                            request.DateBehaviorId = FwFormField.getValue($confirmation, '.datebehavior');
                            request.DateField = FwFormField.getValue($confirmation, '.datefield');
                            request.FromDate = FwFormField.getValue($confirmation, '.fromdate');
                            request.ToDate = FwFormField.getValue($confirmation, '.todate');
                            request.OfficeLocationId = FwFormField.getValue($confirmation, '.officelocation');
                            FwAppData.apiMethod(true, 'PUT', `api/v1/userwidget/${userWidgetId}`, request, FwServices.defaultTimeout, response => {
                                FwNotification.renderNotification('SUCCESS', 'Widget Chart Type Updated');
                                FwConfirmation.destroyConfirmation($confirmation);
                                FwAppData.apiMethod(true, 'GET', 'api/v1/dashboardsettings/' + response.UserId, null, FwServices.defaultTimeout, widgetResponse => {
                                    const $dashboard = $chartSettings.closest('.programlogo');
                                    for (let i = 0; i < widgetResponse.UserWidgets.length; i++) {
                                        if (widgetResponse.UserWidgets[i].selected && widgetResponse.UserWidgets[i].userWidgetId === response.UserWidgetId) {
                                            widgetResponse.UserWidgets[i].width = Math.floor(100 / widgetResponse.WidgetsPerRow).toString() + '%',
                                                this.renderWidget($dashboard, widgetResponse.UserWidgets[i]);
                                        }
                                    }
                                }, null, null);
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
    //----------------------------------------------------------------------------------------------
    loadSettings($control) {
        const webusersid = sessionStorage.getItem('webusersid');
        FwAppData.apiMethod(true, 'GET', `api/v1/dashboardsettings/${webusersid}`, null, FwServices.defaultTimeout, response => {
            const $dashboard = $control.find('.programlogo');
            let hiddenCounter = 0;
            const dashboardButton = '<div class="flexrow dashboard-btn-row"><div class="fwformcontrol dashboardsettings btn-container"><i class="material-icons dashboard systembarcontrol" title="Settings" data-id="settings">settings</i><span>Dashboard Settings</span></div></div>';
            //       if (hiddenCounter === response.UserWidgets.length) {
            jQuery($control).append(dashboardButton);
            jQuery($control).find('.dashboardsettings').on('click', e => {
                program.navigate('module/dashboardsettings');
            });
            //}
            for (let i = 0; i < response.UserWidgets.length; i++) {
                if (response.UserWidgets[i].selected) {
                    response.UserWidgets[i].width = Math.floor(100 / response.WidgetsPerRow).toString() + '%',
                        this.renderWidget($dashboard, response.UserWidgets[i]);
                } else {
                    hiddenCounter++;
                }
            }
        }, null, $control);
    }
    //----------------------------------------------------------------------------------------------
    renderWidget($control, widgetData) {
        let self = this;
        let refresh = '<i id="' + widgetData.userWidgetId + 'refresh" class="chart-refresh material-icons">refresh</i>';
        let settings = '<i id="' + widgetData.userWidgetId + 'settings" class="chart-settings material-icons">settings</i>';
        let fullscreen = '<i id="' + widgetData.userWidgetId + 'fullscreen" class="chart-settings material-icons">fullscreen</i>';
        let dataPointCount = 0;

        let container = jQuery($control).append('<div data-chart="' + widgetData.apiname + '" class="chart-container ' + widgetData.userWidgetId + '" style="height:' + widgetData.width + ';width:' + widgetData.width + ';"><canvas style="display:inline-block;width:100%;padding:5px;" id="' + widgetData.userWidgetId + '"></canvas><div class="officebar">' + widgetData.officeLocationCode + '</div><div class="toolbar">' + fullscreen + refresh + settings + '</div></div>');
        this.buildWidgetSettings(jQuery($control).find('#' + widgetData.userWidgetId + 'settings'), widgetData.userWidgetId);

        if (widgetData.dataPoints > 0) {
            dataPointCount = widgetData.dataPoints
        }

        jQuery($control).off('click', '#' + widgetData.userWidgetId + 'refresh').on('click', '#' + widgetData.userWidgetId + 'refresh', e => {
            FwAppData.apiMethod(true, 'GET', `api/v1/dashboard/loadwidgetbyname/${widgetData.apiname}?dataPoints=${dataPointCount}&locationId=${widgetData.officeLocationId}&warehouseId=${JSON.parse(sessionStorage.getItem('warehouse')).warehouseid}&departmentId=${JSON.parse(sessionStorage.getItem('department')).departmentid}&DateBehaviorId=${widgetData.dateBehaviorId}&fromDate=${widgetData.fromDate}&toDate=${widgetData.toDate}&datefield=${widgetData.dateField}&stacked=${widgetData.stacked}`, {}, FwServices.defaultTimeout, response => {
                try {
                    widgetcanvas.siblings('.officebar').text(response.locationCodes);
                    let titleArray = [];
                    let fromDate = moment(response.fromDate).format('l');
                    let toDate = moment(response.toDate).format('l');
                    titleArray.push(response.options.title.text);
                    if (fromDate !== undefined && fromDate === toDate) {
                        titleArray.push(toDate);
                    } else if (fromDate !== undefined && fromDate !== toDate && widgetData.dateBehaviorId === 'SINGLEDATESPECIFICDATE') {
                        titleArray.push(fromDate);
                    } else if (fromDate !== undefined && fromDate !== toDate) {
                        titleArray.push(fromDate + ' - ' + toDate);
                    }

                    if (response.dateBehaviorId === 'NONE' || response.dateBehaviorId === undefined) {
                        titleArray.pop();
                    }

                    response.options.title.text = titleArray;

                    if (widgetData.widgettype !== '') {
                        response.type = widgetData.widgettype
                    }

                    response = this.formatAxis(response, widgetData.axisNumberFormatId);
                    response = this.formatData(response, widgetData.dataNumberFormatId);

                    if (response.data.labels.length > 10 && response.type !== 'pie') {
                        response.options.scales.xAxes[0].ticks.minRotation = 70;
                        response.options.scales.xAxes[0].ticks.maxRotation = 70;
                    }

                    Chart.helpers.each(Chart.instances, function (instance) {
                        if (instance.chart.canvas.id === widgetData.userWidgetId) { instance.chart.destroy() }
                    })

                    var chart = new Chart(widgetcanvas, response);
                    jQuery(widgetcanvas).on('click', function (evt) {
                        var activePoint = chart.getElementAtEvent(evt)[0];
                        if (activePoint) {
                            var data = activePoint._chart.data;
                            var datefield = widgetData.dateField;
                            var labelfield1 = chart.config["label1FieldName"].toUpperCase();
                            var labelvalue = data.datasets[activePoint._datasetIndex].label;
                            if (labelvalue === null) {
                                labelvalue = data.labels[activePoint._index]
                            }

                            let chartFilters: any = [];
                            if (labelvalue != '') {
                                chartFilters.push({
                                    'datafield': labelfield1,
                                    'value': labelvalue,
                                    'type': 'field'
                                });
                            };
                            if (response.dateBehaviorId != 'NONE' || response.dateBehaviorId != undefined) {
                                if (fromDate != 'Invalid date') {
                                    chartFilters.push({
                                        'datafield': datefield,
                                        'value': fromDate,
                                        'type': 'fromdate'
                                    });
                                }
                                if (toDate != 'Invalid date') {
                                    chartFilters.push({
                                        'datafield': datefield,
                                        'value': toDate,
                                        'type': 'todate'
                                    });
                                }
                            };

                            if (chartFilters.length > 0) {
                                chartFilters = JSON.stringify(chartFilters);
                                sessionStorage.setItem('chartfilter', chartFilters);
                            };
                            program.getModule(`module/${widgetData.modulename}`);
                        }
                    });
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            }, null, jQuery(widgetcanvas).parent());
        });

        var widgetcanvas = $control.find('#' + widgetData.userWidgetId);

        jQuery($control).off('click', '#' + widgetData.userWidgetId + 'fullscreen').on('click', '#' + widgetData.userWidgetId + 'fullscreen', e => {
            try {
                var $confirmation: JQuery = FwConfirmation.renderConfirmation(widgetData.text, '');
                var $cancel = FwConfirmation.addButton($confirmation, 'Close', true);
                var html = [];
                html.push('<div class="flexrow" style="max-width:unset"><div class="flexcolumn" style="flex:5 1 0;"><div data-chart="' + widgetData.apiname + '" class="chart-container" style="overflow:hidden;"><canvas style="padding:5px;" id="' + widgetData.apiname + 'fullscreen"></canvas><div class="fullscreenofficebar">' + widgetData.officeLocationCode + '</div></div></div><div class="flexcolumn fullscreen-fields">');
                html.push('<div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Settings">')
                html.push(this.getSettingsHtml());
                // html.push('<div class="flexrow" style="max-width:none;justify-content:center"><div class="fwformcontrol apply-fullscreen" data-type="button" style="flex:0 1 350px;margin:75px 0 0 10px;text-align:center;">Apply</div></div>') // button was removed in lieu of change events on all fields
                html.push('</div></div></div>');
                FwConfirmation.addControls($confirmation, html.join(''));
                $confirmation.find('.fwconfirmationbox').css('width', '80%');

                function applyFullscreenChanges(): void {
                    let fullscreenStacked;
                    let fullscreenWidgetType = FwFormField.getValue($confirmation, '.widgettype');
                    (FwFormField.getValue($confirmation, '.stacked') === 'T') ? fullscreenStacked = true : fullscreenStacked = false;
                    let fullscreenDataPointCount = FwFormField.getValue($confirmation, '.defaultpoints');
                    let fullscreenAxisNumberFormatId = FwFormField.getValue($confirmation, '.axisformat');
                    let fullscreenDataNumberFormatId = FwFormField.getValue($confirmation, '.dataformat');
                    let fullscreenDateBehaviorId = FwFormField.getValue($confirmation, '.datebehavior');
                    let fullscreenDateField = FwFormField.getValue($confirmation, '.datefield');
                    let fullscreenFromDate = FwFormField.getValue($confirmation, '.fromdate');
                    let fullscreenToDate = FwFormField.getValue($confirmation, '.todate');
                    let fullscreenOfficeLocationId = FwFormField.getValue($confirmation, '.officelocation');

                    FwAppData.apiMethod(true, 'GET', `api/v1/dashboard/loadwidgetbyname/${widgetData.apiname}?dataPoints=${fullscreenDataPointCount}&locationId=${fullscreenOfficeLocationId}&warehouseId=${JSON.parse(sessionStorage.getItem('warehouse')).warehouseid}&departmentId=${JSON.parse(sessionStorage.getItem('department')).departmentid}&DateBehaviorId=${fullscreenDateBehaviorId}&fromDate=${fullscreenFromDate}&toDate=${fullscreenToDate}&datefield=${fullscreenDateField}&stacked=${fullscreenStacked}`, {}, FwServices.defaultTimeout, function onSuccess(response) {
                        try {
                            $confirmation.find('.fullscreenofficebar').text(response.locationCodes);
                            let titleArray = [];
                            let fromDate = moment(response.fromDate).format('l');
                            let toDate = moment(response.toDate).format('l');
                            titleArray.push(response.options.title.text);
                            if (fromDate !== undefined && fromDate === toDate) {
                                titleArray.push(toDate);
                            } else if (fromDate !== undefined && fromDate !== toDate && fullscreenDateBehaviorId === 'SINGLEDATESPECIFICDATE') {
                                titleArray.push(fromDate);
                            } else if (fromDate !== undefined && fromDate !== toDate) {
                                titleArray.push(fromDate + ' - ' + toDate);
                            }

                            if (response.dateBehaviorId === 'NONE' || response.dateBehaviorId === undefined) {
                                titleArray.pop();
                            }

                            response.options.title.text = titleArray;

                            if (fullscreenWidgetType !== '') {
                                response.type = fullscreenWidgetType
                            }

                            response = self.formatAxis(response, fullscreenAxisNumberFormatId);
                            response = self.formatData(response, fullscreenDataNumberFormatId);

                            Chart.helpers.each(Chart.instances, function (instance) {
                                if (instance.chart.canvas.id === widgetData.apiname + 'fullscreen') { instance.chart.destroy() }
                            })

                            var chart = new Chart(<any>widgetfullscreen, response);
                            jQuery(widgetfullscreen).on('click', function (evt) {
                                var activePoint = chart.getElementAtEvent(evt)[0];
                                if (activePoint) {
                                    var data = activePoint._chart.data;
                                    var datefield = widgetData.dateField;
                                    var labelfield1 = chart.config["label1FieldName"].toUpperCase();
                                    var labelvalue = data.datasets[activePoint._datasetIndex].label;
                                    if (labelvalue === null) {
                                        labelvalue = data.labels[activePoint._index]
                                    }

                                    let chartFilters: any = [];
                                    if (labelvalue != '') {
                                        chartFilters.push({
                                            'datafield': labelfield1,
                                            'value': labelvalue,
                                            'type': 'field'
                                        });
                                    };
                                    if (response.dateBehaviorId != 'NONE' || response.dateBehaviorId != undefined) {
                                        if (fromDate != 'Invalid date') {
                                            chartFilters.push({
                                                'datafield': datefield,
                                                'value': fromDate,
                                                'type': 'fromdate'
                                            });
                                        }
                                        if (toDate != 'Invalid date') {
                                            chartFilters.push({
                                                'datafield': datefield,
                                                'value': toDate,
                                                'type': 'todate'
                                            });
                                        }
                                    };

                                    if (chartFilters.length > 0) {
                                        chartFilters = JSON.stringify(chartFilters);
                                        sessionStorage.setItem('chartfilter', chartFilters);
                                    };
                                    FwConfirmation.destroyConfirmation($confirmation);
                                    program.getModule(`module/${widgetData.modulename}`);
                                }
                            });
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }, null, jQuery(widgetfullscreen).parent());

                }
                $confirmation.find('.apply-fullscreen').on('change', () => {
                    applyFullscreenChanges();
                });

                var widgetfullscreen = $confirmation.find('#' + widgetData.apiname + 'fullscreen');

                FwAppData.apiMethod(true, 'GET', 'api/v1/userwidget/' + widgetData.userWidgetId, null, FwServices.defaultTimeout, function onSuccess(response) {
                    self.loadSettingsFields($confirmation, response);
                }, null, null);
                FwAppData.apiMethod(true, 'GET', `api/v1/dashboard/loadwidgetbyname/${widgetData.apiname}?dataPoints=${dataPointCount}&locationId=${widgetData.officeLocationId}&warehouseId=${JSON.parse(sessionStorage.getItem('warehouse')).warehouseid}&departmentId=${JSON.parse(sessionStorage.getItem('department')).departmentid}&DateBehaviorId=${widgetData.dateBehaviorId}&fromDate=${widgetData.fromDate}&toDate=${widgetData.toDate}&datefield=${widgetData.dateField}&stacked=${widgetData.stacked}`, {}, FwServices.defaultTimeout, function onSuccess(response) {
                    try {
                        widgetfullscreen.siblings('.officebar').text(response.locationCodes);
                        let titleArray = [];
                        let fromDate = moment(response.fromDate).format('l');
                        let toDate = moment(response.toDate).format('l');
                        titleArray.push(response.options.title.text);
                        if (fromDate !== undefined && fromDate === toDate) {
                            titleArray.push(toDate);
                        } else if (fromDate !== undefined && fromDate !== toDate && widgetData.dateBehaviorId === 'SINGLEDATESPECIFICDATE') {
                            titleArray.push(fromDate);
                        } else if (fromDate !== undefined && fromDate !== toDate) {
                            titleArray.push(fromDate + ' - ' + toDate);
                        }


                        if (response.dateBehaviorId === 'NONE' || response.dateBehaviorId === undefined) {
                            titleArray.pop();
                        }

                        response.options.title.text = titleArray;

                        if (widgetData.widgettype !== '') {
                            response.type = widgetData.widgettype
                        }

                        response = self.formatAxis(response, widgetData.axisNumberFormatId);
                        response = self.formatData(response, widgetData.dataNumberFormatId);

                        var chart = new Chart(<any>widgetfullscreen, response);
                        jQuery(widgetfullscreen).on('click', function (evt) {
                            var activePoint = chart.getElementAtEvent(evt)[0];
                            if (activePoint) {
                                var data = activePoint._chart.data;
                                var datefield = widgetData.dateField;
                                var labelfield1 = chart.config["label1FieldName"].toUpperCase();
                                var labelvalue = data.datasets[activePoint._datasetIndex].label;
                                if (labelvalue === null) {
                                    labelvalue = data.labels[activePoint._index]
                                }

                                let chartFilters: any = [];
                                if (labelvalue != '') {
                                    chartFilters.push({
                                        'datafield': labelfield1,
                                        'value': labelvalue,
                                        'type': 'field'
                                    });
                                };
                                if (response.dateBehaviorId != 'NONE' || response.dateBehaviorId != undefined) {
                                    if (fromDate != 'Invalid date') {
                                        chartFilters.push({
                                            'datafield': datefield,
                                            'value': fromDate,
                                            'type': 'fromdate'
                                        });
                                    }
                                    if (toDate != 'Invalid date') {
                                        chartFilters.push({
                                            'datafield': datefield,
                                            'value': toDate,
                                            'type': 'todate'
                                        });
                                    }
                                };

                                if (chartFilters.length > 0) {
                                    chartFilters = JSON.stringify(chartFilters);
                                    sessionStorage.setItem('chartfilter', chartFilters);
                                };
                                FwConfirmation.destroyConfirmation($confirmation);
                                program.getModule(`module/${widgetData.modulename}`);
                            }
                        });
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                }, null, jQuery(widgetfullscreen).parent());

            } catch (ex) {
                FwFunc.showError(ex);
            }
        })

        FwAppData.apiMethod(true, 'GET', `api/v1/dashboard/loadwidgetbyname/${widgetData.apiname}?dataPoints=${dataPointCount}&locationId=${widgetData.officeLocationId}&warehouseId=${JSON.parse(sessionStorage.getItem('warehouse')).warehouseid}&departmentId=${JSON.parse(sessionStorage.getItem('department')).departmentid}&DateBehaviorId=${widgetData.dateBehaviorId}&fromDate=${widgetData.fromDate}&toDate=${widgetData.toDate}&datefield=${widgetData.dateField}&stacked=${widgetData.stacked}`, {}, FwServices.defaultTimeout, function onSuccess(response) {
            try {
                widgetcanvas.siblings('.officebar').text(response.locationCodes);
                let titleArray = [];
                let fromDate = moment(response.fromDate).format('l');
                let toDate = moment(response.toDate).format('l');
                titleArray.push(response.options.title.text);
                if (fromDate !== undefined && fromDate === toDate) {
                    titleArray.push(toDate);
                } else if (fromDate !== undefined && fromDate !== toDate && widgetData.dateBehaviorId === 'SINGLEDATESPECIFICDATE') {
                    titleArray.push(fromDate);
                } else if (fromDate !== undefined && fromDate !== toDate) {
                    titleArray.push(fromDate + ' - ' + toDate);
                }

                if (response.dateBehaviorId === 'NONE' || response.dateBehaviorId === undefined) {
                    titleArray.pop();
                }

                response.options.title.text = titleArray;

                if (widgetData.widgettype !== '') {
                    response.type = widgetData.widgettype
                }

                response = self.formatAxis(response, widgetData.axisNumberFormatId);
                response = self.formatData(response, widgetData.dataNumberFormatId);

                if (response.data.labels.length > 10 && response.type !== 'pie') {
                    response.options.scales.xAxes[0].ticks.minRotation = 70;
                    response.options.scales.xAxes[0].ticks.maxRotation = 70;
                }

                Chart.helpers.each(Chart.instances, function (instance) {
                    if (instance.chart.canvas.id === widgetData.userWidgetId) { instance.chart.destroy() }
                })

                var chart = new Chart(widgetcanvas, response);
                jQuery(widgetcanvas).off('click').on('click', function (evt) {
                    var activePoint = chart.getElementAtEvent(evt)[0];
                    if (activePoint) {
                        var data = activePoint._chart.data;
                        var datefield = widgetData.dateField;
                        var labelfield1 = chart.config["label1FieldName"].toUpperCase();
                        var labelvalue = data.datasets[activePoint._datasetIndex].label;
                        if (labelvalue === null) {
                            labelvalue = data.labels[activePoint._index]
                        }

                        let chartFilters: any = [];
                        if (labelvalue != '') {
                            chartFilters.push({
                                'datafield': labelfield1,
                                'value': labelvalue,
                                'type': 'field'
                            });
                        };
                        if (response.dateBehaviorId != 'NONE' || response.dateBehaviorId != undefined) {
                            if (fromDate != 'Invalid date') {
                                chartFilters.push({
                                    'datafield': datefield,
                                    'value': fromDate,
                                    'type': 'fromdate'
                                });
                            }
                            if (toDate != 'Invalid date') {
                                chartFilters.push({
                                    'datafield': datefield,
                                    'value': toDate,
                                    'type': 'todate'
                                });
                            }
                        };

                        if (chartFilters.length > 0) {
                            chartFilters = JSON.stringify(chartFilters);
                            sessionStorage.setItem('chartfilter', chartFilters);
                        };

                        program.getModule(`module/${widgetData.modulename}`);
                    }
                });
            } catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, jQuery(widgetcanvas).parent());
    };
    //----------------------------------------------------------------------------------------------
    getSettingsHtml() {
        let html = [];
        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('<div class="flexrow">');
        html.push('<div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield widgettype apply-fullscreen" data-caption="Chart Type" data-datafield="Widget"></div>');
        html.push('</div>');
        html.push('<div class="flexrow">');
        html.push('<div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield stacked apply-fullscreen" data-caption="Stacked" data-datafield="Stacked"></div>');
        html.push('</div>');
        html.push('<div class="flexrow">');
        html.push('<div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield officelocation apply-fullscreen" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" style="float:left;max-width:400px;"></div>');
        html.push('</div>');
        html.push('<div class="flexrow">');
        html.push('<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield datebehavior apply-fullscreen" data-caption="Date Behavior" data-datafield="DateBehaviorId" data-displayfield="DateBehavior" data-validationname="WidgetDateBehaviorValidation" data-validationpeek="false" style="float:left;width:200px;"></div>');
        html.push('</div>');
        html.push('<div class="flexrow">');
        html.push('<div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield datefield apply-fullscreen" data-caption="Date Field" data-datafield="DateField" style="display:none;"></div>');
        html.push('</div>');
        html.push('<div class="flexrow">');
        html.push('<div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield fromdate apply-fullscreen" data-caption="From Date" data-datafield="FromDate" style="display:none;"></div>');
        html.push('</div>');
        html.push('<div class="flexrow">');
        html.push('<div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield todate apply-fullscreen" data-caption="To Date" data-datafield="ToDate" style="display:none;"></div>');
        html.push('</div>');
        html.push('<div class="flexrow">');
        html.push('<div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield defaultpoints apply-fullscreen" data-caption="Number of Data Points" data-datafield="DefaultDataPoints"></div>');
        html.push('</div>');
        html.push('<div class="flexrow">');
        html.push('<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield axisformat apply-fullscreen" data-caption="Axis Number Format" data-datafield="AxisNumberFormatId" data-displayfield="AxisNumberFormat" data-validationname="WidgetNumberFormatValidation" data-validationpeek="false" style="float:left;width:200px;"></div>');
        html.push('</div>');
        html.push('<div class="flexrow">');
        html.push('<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield dataformat apply-fullscreen" data-caption="Data Number Format" data-datafield="DataNumberFormatId" data-displayfield="DataNumberFormat" data-validationname="WidgetNumberFormatValidation" data-validationpeek="false" style="float:left;width:200px;"></div>');
        html.push('</div>');
        html.push('</div>');

        return html.join('')
    }
    //----------------------------------------------------------------------------------------------
    loadSettingsFields($control, data) {
        let self = this;

        FwFormField.loadItems($control.find('.widgettype'), [
            { value: 'bar', text: 'Bar' },
            { value: 'horizontalBar', text: 'Horizontal Bar' },
            { value: 'pie', text: 'Pie' }
        ], true);

        $control.find('div[data-datafield="DefaultDataPoints"] input').val(data.DataPoints);

        if (data.AxisNumberFormat !== '') {
            FwFormField.setValueByDataField($control, 'AxisNumberFormatId', data.AxisNumberFormatId, data.AxisNumberFormat);
        } else {
            FwFormField.setValueByDataField($control, 'AxisNumberFormatId', data.DefaultAxisNumberFormatId, data.DefaultAxisNumberFormat);
        }

        if (data.DataNumberFormat !== '') {
            FwFormField.setValueByDataField($control, 'DataNumberFormatId', data.DataNumberFormatId, data.DataNumberFormat);
        } else {
            FwFormField.setValueByDataField($control, 'DataNumberFormatId', data.DefaultDataNumberFormatId, data.DefaultDataNumberFormat);
        }

        if (data.Stacked !== null) {
            FwFormField.setValue($control, '.stacked', data.Stacked);
        } else {
            FwFormField.setValue($control, '.stacked', data.DefaultStacked);
        }

        if (data.WidgetType !== '') {
            FwFormField.setValue($control, '.widgettype', data.WidgetType);
        } else {
            FwFormField.setValue($control, '.widgettype', data.DefaultType);
        }

        if (data.FromDate !== '') {
            let fromDate = $control.find('.fromdate');
            fromDate.show();
            FwFormField.setValue2(fromDate, data.FromDate)
        } else if (data.FromDate === '' && data.DefaultFromDate !== '') {
            let fromDate = $control.find('.fromdate');
            fromDate.show();
            FwFormField.setValue2(fromDate, data.DefaultFromDate);
        }

        if (data.ToDate !== '') {
            let toDate = $control.find('.todate');
            toDate.show();
            FwFormField.setValue2(toDate, data.ToDate)
        } else if (data.ToDate === '' && data.DefaultToDate !== '') {
            let toDate = $control.find('.todate');
            toDate.show();
            FwFormField.setValue2(toDate, data.DefaultToDate);
        }

        $control.find('div[data-datafield="DateBehaviorId"]').on('change', function () {
            let selected = FwFormField.getValue2(jQuery(this));
            self.setDateBehaviorFields($control, selected);
        });
        FwFormField.setValueByDataField($control, 'DateBehaviorId', data.DateBehaviorId, data.DateBehavior);
        FwFormField.setValueByDataField($control, 'OfficeLocationId', data.OfficeLocationId, data.OfficeLocation);

        if (data.DateBehaviorId !== '' && data.DateBehaviorId !== undefined) {
            self.setDateBehaviorFields($control, data.DateBehaviorId);
        }

        let dateFields = data.DateFields.split(',');
        let dateFieldDisplays = data.DateFieldDisplayNames.split(',');
        let dateFieldSelectArray = [];

        for (var i = 0; i < dateFields.length; i++) {
            dateFieldSelectArray.push({
                'value': dateFields[i],
                'text': dateFieldDisplays[i]
            })
        }

        FwFormField.loadItems($control.find('.datefield'), dateFieldSelectArray, true);
        FwFormField.setValue2($control.find('.datefield'), data.DateField);
    }
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
        } else {
            dateField.show();
            if (DateBehavior === 'SINGLEDATESPECIFICDATE') {
                fromDateField.text('Date');
                fromDate.show();
                toDate.hide();
            } else if (DateBehavior === 'DATERANGESPECIFICDATES') {
                fromDateField.text('From Date');
                fromDate.show();
                toDate.show();
            } else {
                fromDate.hide();
                toDate.hide();
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    formatAxis(chartData, axisFormat) {
        switch (axisFormat) {
            case 'TWODGDEC':
                if (chartData.type !== 'horizontalBar') {
                    chartData.options.scales.yAxes[0].ticks.userCallback = this.commaTwoDecimal
                } else {
                    chartData.options.scales.xAxes[0].ticks.userCallback = this.commaTwoDecimal
                }
                break;
            case 'TWDIGPCT':
                if (chartData.type !== 'horizontalBar') {
                    chartData.options.scales.yAxes[0].ticks.userCallback = this.commaTwoDecimalPercent
                } else {
                    chartData.options.scales.xAxes[0].ticks.userCallback = this.commaTwoDecimalPercent
                }
                break;
            case 'WHOLENBR':
                if (chartData.type !== 'horizontalBar') {
                    chartData.options.scales.yAxes[0].ticks.userCallback = this.commaDelimited
                } else {
                    chartData.options.scales.xAxes[0].ticks.userCallback = this.commaDelimited
                }
                break;
            case 'WHNUMPCT':
                if (chartData.type !== 'horizontalBar') {
                    chartData.options.scales.yAxes[0].ticks.userCallback = this.commaDelimitedPercent
                } else {
                    chartData.options.scales.xAxes[0].ticks.userCallback = this.commaDelimitedPercent
                }
                break;
        }

        if (chartData.type === 'pie') {
            delete chartData.options.legend;
            delete chartData.options.scales;
        } else {
            chartData.options.scales.xAxes[0].ticks.autoSkip = false;
        }
        return chartData;
    }
    //----------------------------------------------------------------------------------------------
    formatData(chartData, dataFormat) {
        switch (dataFormat) {
            case 'TWODGDEC':
                if (chartData.type !== 'pie') {
                    chartData.options.tooltips = {
                        'callbacks': {
                            'label': this.commaTwoDecimalData
                        }
                    };
                } else {
                    chartData.options.tooltips = {
                        'callbacks': {
                            'label': this.commaTwoDecimalDataPie
                        }
                    };
                }
                break;
            case 'TWDIGPCT':
                if (chartData.type !== 'pie') {
                    chartData.options.tooltips = {
                        'callbacks': {
                            'label': this.commaTwoDecimalPercentData
                        }
                    };
                } else {
                    chartData.options.tooltips = {
                        'callbacks': {
                            'label': this.commaTwoDecimalPercentDataPie
                        }
                    };
                }
                break;
            case 'WHOLENBR':
                if (chartData.type !== 'pie') {
                    chartData.options.tooltips = {
                        'callbacks': {
                            'label': this.commaDelimitedData
                        }
                    };
                } else {
                    chartData.options.tooltips = {
                        'callbacks': {
                            'label': this.commaDelimitedDataPie
                        }
                    };
                }
                break;
            case 'WHNUMPCT':
                if (chartData.type !== 'pie') {
                    chartData.options.tooltips = {
                        'callbacks': {
                            'label': this.commaDelimitedPercentData
                        }
                    };
                } else {
                    chartData.options.tooltips = {
                        'callbacks': {
                            'label': this.commaDelimitedPercentDataPie
                        }
                    };
                }
                break;
        }
        return chartData;
    }
    //----------------------------------------------------------------------------------------------
    // TO DO - format these all functions below into more modularized code to input to widgets.

    commaDelimited = function (value, index, values) {
        (value.toString().indexOf('.') !== -1) ? value = value.toFixed(1).toString() : value = value.toString();
        return value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
    };

    commaDelimitedData = function (tooltipItem, data) {
        var value = data.datasets[tooltipItem.datasetIndex].data[tooltipItem.index];
        value = value.toString();
        return value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
    }

    commaDelimitedDataPie = function (tooltipItem, data) {
        var value = data.datasets[tooltipItem.datasetIndex].data[tooltipItem.index];
        value = value.toString();
        return `${data.labels[tooltipItem.index]} : ${value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")}`
    }

    commaTwoDecimal = function (value, index, values) {
        value = (Math.round(value * 10) / 10).toString();
        if (value.indexOf('.') === -1 && !isNaN(value)) {
            return value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,") + '.00'
        } else {
            return value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
        }
    };

    commaTwoDecimalData = function (tooltipItem, data) {
        var value = data.datasets[tooltipItem.datasetIndex].data[tooltipItem.index];
        value = value.toString();
        if (value.indexOf('.') === -1) {
            return value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,") + '.00'
        } else {
            return value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
        }
    }

    commaTwoDecimalDataPie = function (tooltipItem, data) {
        var value = data.datasets[tooltipItem.datasetIndex].data[tooltipItem.index];
        value = value.toString();
        if (value.indexOf('.') === -1) {
            return `${data.labels[tooltipItem.index]} : ${value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")}.00`
        } else {
            return `${data.labels[tooltipItem.index]} : ${value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")}`
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
        var value = data.datasets[tooltipItem.datasetIndex].data[tooltipItem.index];
        value = value.toString();
        if (value.indexOf('.') === -1) {
            return value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
        } else {
            return value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,") + '.00%'
        }
    }

    commaTwoDecimalPercentDataPie = function (tooltipItem, data) {
        var value = data.datasets[tooltipItem.datasetIndex].data[tooltipItem.index];
        value = value.toString();
        if (value.indexOf('.') === -1) {
            return `${data.labels[tooltipItem.index]} : ${value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")}`
        } else {
            return `${data.labels[tooltipItem.index]} : ${value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")}.00%`
        }
    }

    commaDelimitedPercent = function (value, index, values) {
        (value.toString().indexOf('.') !== -1) ? value = value.toFixed(1).toString() : value = value.toString();
        if (value.charAt(value.length - 3) !== '.' && !isNaN(value)) {
            return value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,") + '%'
        } else {
            return value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")
        }
    };

    commaDelimitedPercentData = function (tooltipItem, data) {
        var value = data.datasets[tooltipItem.datasetIndex].data[tooltipItem.index];
        value = value.toString();
        return value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,") + '%'
    }

    commaDelimitedPercentDataPie = function (tooltipItem, data) {
        var value = data.datasets[tooltipItem.datasetIndex].data[tooltipItem.index];
        value = value.toString();
        return `${data.labels[tooltipItem.index]} : ${value.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,")}%`
    }

};



var DashboardController = new Dashboard();