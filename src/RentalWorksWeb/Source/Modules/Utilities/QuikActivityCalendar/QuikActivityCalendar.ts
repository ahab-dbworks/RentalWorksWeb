routes.push({ pattern: /^module\/quikactivitycalendar$/, action: function (match: RegExpExecArray) { return QuikActivityCalendarController.getModuleScreen(); } });

class QuikActivityCalendar {
    Module: string = 'QuikActivityCalendar';
    caption: string = Constants.Modules.Utilities.QuikActivityCalendar.caption;
    nav: string = Constants.Modules.Utilities.QuikActivityCalendar.nav;
    id: string = Constants.Modules.Utilities.QuikActivityCalendar.id;
    SessionId: string;
    //----------------------------------------------------------------------------------------------
    getModuleScreen = () => {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $form = this.openForm('EDIT');

        screen.load = () => {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?: any) {
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        //disables asterisk and save prompt
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');

        this.populateCheckboxes($form);
        this.calendarEvents($form);
        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    populateCheckboxes($form: any) {
        const request = {};
        FwAppData.apiMethod(true, 'POST', `api/v1/quikactivitytype/browse`, request, FwServices.defaultTimeout, response => {
            try {
                const $activities = $form.find('.activities');
                const descriptionIndex = response.ColumnIndex.Description;
                const activityType = response.ColumnIndex.ActivityType;
                const isSystemTypeIndex = response.ColumnIndex.IsSystemType;
                const colorIndex = response.ColumnIndex.Color;
                for (let i = 0; i < response.Rows.length; i++) {
                    const self = response.Rows[i];
                    const item = `<div class="flexrow" style="max-height:2em;">
                                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="${self[descriptionIndex]}" data-datafield="${self[activityType]}"></div>
                                    <div style="background-color:${self[colorIndex]}; max-width:30px; margin:16px 0px; border:1px solid black;"></div>
                                  </div>`;
                    $activities.append(item);
                }
                const $fwcontrols = $activities.find('.fwcontrol');
                FwControl.renderRuntimeControls($fwcontrols);
                $form.find('.activities [data-type="checkbox"] input').prop('checked', true);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, $form);
    }
    //----------------------------------------------------------------------------------------------
    calendarEvents($form: any) {
        const $calendar = $form.find('.calendar');
        let activityTypes = '';

        //render QuikActivity grid
        const $quikActivityGrid = $form.find('div[data-grid="QuikActivityGrid"]');
        const $quikActivityGridControl = FwBrowse.loadGridFromTemplate('QuikActivityGrid');
        $quikActivityGrid.empty().append($quikActivityGridControl);
        FwBrowse.init($quikActivityGridControl);
        FwBrowse.renderRuntimeHtml($quikActivityGridControl);

        $calendar
            .data('ongetevents', request => {
                const startOfMonth = moment(request.start.value).format('MM/DD/YYYY');
                const endOfMonth = moment(request.start.value).add(request.days, 'd').format('MM/DD/YYYY');
                const warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
                let apiURL = `api/v1/quikactivity/calendardata?WarehouseId=${warehouseId}&FromDate=${startOfMonth}&ToDate=${endOfMonth}`;
                apiURL += activityTypes != '' ? `&ActivityType=${activityTypes}` : '';
                apiURL += request.mode === 'Day' || request.mode === 'Week' ? '&IncludeTimes=true' : '';
                FwAppData.apiMethod(true, 'GET', apiURL, null, FwServices.defaultTimeout, response => {
                    const calendarEvents = response.QuikActivityCalendarEvents;
                    this.SessionId = response.SessionId;
                    //FwScheduler.loadYearEventsCallback($calendar, [{ id: '1', name: '' }], calendarEvents);
                    for (var i = 0; i < calendarEvents.length; i++) {
                        if (calendarEvents[i].textColor !== 'rgb(0,0,0)') {
                            calendarEvents[i].html = `<div style="color:${calendarEvents[i].textColor}">${calendarEvents[i].text}</div>`
                        }
                    }
                    FwScheduler.loadEventsCallback($calendar, [{ id: '1', name: '' }], calendarEvents);
                }, ex => {
                    FwFunc.showError(ex);
                }, $calendar)
            })
            .data('ontimerangedoubleclicked', event => {
                try {
                    const date = event.start.toString('MM/dd/yyyy');
                    FwScheduler.setSelectedDay($calendar, date);
                    $form.find('div[data-type="Browse"][data-name="Schedule"] .browseDate .fwformfield-value').val(date).change();
                    $form.find('div.tab.schedule').click();
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .data('ontimerangeselect', event => {
                try {
                    const fromDate = moment(event.start.value).format('MM/DD/YYYY');
                    const toDate = moment(event.start.value).format('MM/DD/YYYY');
                    const summary = FwFormField.getValueByDataField($form, 'Summary');
                    $quikActivityGridControl.data('ondatabind', request => {
                        request.uniqueids = {
                            WarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId')
                            , FromDate: fromDate
                            , ToDate: toDate
                            , Summary: summary
                            , SessionId: this.SessionId
                        };
                    });
                    FwBrowse.search($quikActivityGridControl);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .data('oneventclick', e => {
                const data = e.e.data;
                const fromDate = moment(data.start.value).format('MM/DD/YYYY');
                const toDate = moment(data.end.value).format('MM/DD/YYYY');
                const activityType = data.activityType;
                const summary = FwFormField.getValueByDataField($form, 'Summary');
                $quikActivityGridControl.data('ondatabind', request => {
                    request.uniqueids = {
                        WarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId')
                        , FromDate: fromDate
                        , ToDate: toDate
                        , ActivityType: activityType
                        , Summary: summary
                        , SessionId: this.SessionId
                    };
                });
                FwBrowse.search($quikActivityGridControl);
            });

        if ($calendar.length > 0) {
            setTimeout(() => {
                const date = FwScheduler.getTodaysDate();
                FwScheduler.navigate($calendar, date);
            }, 1);
        }

        //Activity checkbox filters
        $form.on('change', '.activities [data-type="checkbox"]', e => {
            const activities = $form.find(`.activities [data-type="checkbox"]`);
            const activityTypesArray: any = [];
            for (let i = 0; i < activities.length; i++) {
                const $this = jQuery(activities[i]);
                if ($this.find('input').prop('checked') === true) {
                    activityTypesArray.push($this.attr('data-datafield'));
                }
            }
            activityTypesArray.length > 1 ? activityTypes = activityTypesArray.join(',') : activityTypes = activityTypesArray.join('');
            FwScheduler.refresh($calendar);
        });
    }
    //----------------------------------------------------------------------------------------------
    events($form: any) {
        $form.find('.calendarmenu').css('border-left', '1px solid #a9a9a9');

        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        FwFormField.setValueByDataField($form, 'WarehouseId', warehouse.warehouseid, warehouse.warehouse);

        const $calendar = $form.find('.calendar');
        $form.on('change', '[data-datafield="WarehouseId"]', e => {
            FwScheduler.refresh($calendar);
        });

        const $quikActivityGrid = $form.find('div[data-grid="QuikActivityGrid"]');

        $form.find('[data-datafield="Summary"]').on('change', e => {
            const isSummary = FwFormField.getValueByDataField($form, 'Summary');
            const $detailColumns = $quikActivityGrid
                .find('[data-browsedatafield="ICode"], [data-browsedatafield="Description"], [data-browsedatafield="Quantity"]')
                .parents('.column');
            if (isSummary == 'true') {
                $detailColumns.hide();
            } else {
                $detailColumns.show();
            }
            const $quikActivityGridControl = $quikActivityGrid.find('[data-type="Grid"]');
            const onDataBind = $quikActivityGridControl.data('ondatabind');
            if (typeof onDataBind == 'function') {
                $quikActivityGridControl.data('ondatabind', request => {
                    onDataBind(request);
                    request.uniqueids.Summary = isSummary;
                });
            }
            FwBrowse.search($quikActivityGridControl);
        });

        //$form.on('click', '.month_default_cell_inner', e => {
        //    //const day = FwScheduler.getSelectedDay(e.currentTarget);
        //    const startEndDates = FwScheduler.getSelectedTimeRange(e.currentTarget);
        //    const fromDate = startEndDates.start;
        //    const toDate = startEndDates.end;

        //    $quikActivityGridControl.data('ondatabind', request => {
        //        request.uniqueids = {
        //            WarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId')
        //            , FromDate: fromDate
        //            , ToDate: toDate
        //            , Summary: FwFormField.getValueByDataField($form, 'Summary')
        //        };
        //    });
        //});

        //$form.on('click', '.month_default_event', e => {
        //    //const day = FwScheduler.getSelectedDay(e.currentTarget);
        //    const startEndDates = FwScheduler.getSelectedTimeRange(e.currentTarget);
        //    const fromDate = startEndDates.start;
        //    const toDate = startEndDates.end;
        //    const activityType = 'RETURN';

        //        $quikActivityGridControl.data('ondatabind', request => {
        //            request.uniqueids = {
        //                WarehouseId: FwFormField.getValueByDataField($form, 'WarehouseId')
        //                , FromDate: fromDate
        //                , ToDate: toDate
        //                , ActivityType: activityType
        //                , Summary: FwFormField.getValueByDataField($form, 'Summary')
        //            };
        //        });
        //});

        ////toggle validation type
        //$form.on('change', '[data-datafield="QuoteOrderToggle"]', e => {
        //    const validationType = FwFormField.getValueByDataField($form, 'QuoteOrderToggle');
        //    validationType === 'QUOTE' ?
        //        $form.find('[data-validationname="QuoteValidation"]').show() && $form.find('[data-validationname="OrderValidation"]').hide()
        //        : $form.find('[data-validationname="QuoteValidation"]').hide() && $form.find('[data-validationname="OrderValidation"]').show();
        //})

    };
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
                <div id="quikactivitycalendarform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="QuikActivity Calendar" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="QuikActivityCalendarController">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                        <div class="flexrow">
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Filter Warehouse" data-datafield="WarehouseId" data-validationname="WarehouseValidation" data-displayfield="WarehouseCode" style="max-width:250px;"></div>
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="All Warehouses" data-datafield="AllWarehouses"></div>
                        </div>
                    </div>
                    <div class="flexrow" style="max-width:none;">
                        <div class="flexcolumn" style="max-width:250px;">
                            <div class="fwcontrol fwcontainer fwform-section activities" data-control="FwContainer" data-type="section" data-caption="Activities">
                            </div>
                        </div>
                        <div class="flexcolumn">
                            <div data-control="FwScheduler" class="fwcontrol fwscheduler calendar"></div>
                        </div>
                    </div>
                    <div class="flexrow" style="max-width:none;">
                        <div class="flexcolumn" style="max-width:250px;">
                            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="View">
                                <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="Summary">
                                   <div data-value="true" data-caption="Summary"></div>
                                   <div data-value="false" data-caption="Detail"></div>
                                </div>
                            </div>
                        </div>
                        <div class="flexcolumn">
                            <div data-control="FwGrid" data-grid="QuikActivityGrid" data-securitycaption="QuikActivity"></div>
                        </div>
                    </div>
                </div>`;
    }
}
var QuikActivityCalendarController = new QuikActivityCalendar();