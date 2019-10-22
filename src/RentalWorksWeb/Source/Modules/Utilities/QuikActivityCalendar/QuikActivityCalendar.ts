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
            FwBrowse.screenunload($form);
        };

        return screen;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?: any) {
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        //disables asterisk and save prompt
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');

        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        FwFormField.setValueByDataField($form, 'WarehouseId', warehouse.warehouseid, warehouse.warehouse);

        FwFormField.setValueByDataField($form, `AllWarehouses`, false);

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

        let $popup = jQuery(`
                <div id="quikActivityPopup" class="fwform fwcontrol fwcontainer"  data-control="FwContainer" data-type="form" style="max-height:90vh;max-width:90vw;background-color:white; padding:10px; border:2px solid gray;">
                    <div class="close-modal"><i class="material-icons">clear</i><div class="btn-text">Close</div></div>
                    <div class="flexcolumn">
                      <div class="flexrow">
                         <div class="fwcontrol fwcontainer fwform-section activities-header" data-control="FwContainer" data-type="section" data-caption="Activities">
                            <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="" data-datafield="Summary"></div>
                        </div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwGrid" data-grid="QuikActivityGrid" data-securitycaption="QuikActivity" style="overflow:auto;max-height:70vh;"></div>
                      </div>
                    </div>
                </div>`);
        FwControl.renderRuntimeControls($popup.find('.fwcontrol'));
        $popup = FwPopup.renderPopup($popup, { ismodal: true });
        //render QuikActivity grid
        const $quikActivityGrid = $popup.find('div[data-grid="QuikActivityGrid"]');
        const $quikActivityGridControl = FwBrowse.loadGridFromTemplate('QuikActivityGrid');
        $quikActivityGrid.empty().append($quikActivityGridControl);
        FwBrowse.init($quikActivityGridControl);
        FwBrowse.renderRuntimeHtml($quikActivityGridControl);
        FwFormField.loadItems($popup.find('div[data-datafield="Summary"]'), [
            { value: 'true', caption: 'Summary', selected: true },
            { value: 'false', caption: 'Detail' }
        ]);
        FwFormField.setValueByDataField($popup, 'Summary', 'true');

        $form.data('onscreenunload', () => { FwPopup.destroyPopup($popup); });

        $popup.find('.close-modal').on('click', function (e) {
            FwPopup.detachPopup($popup);
            //$popup.hide();
        });

        $popup.find('[data-datafield="Summary"]').on('change', e => {
            const isSummary = FwFormField.getValueByDataField($popup, 'Summary');
            const $detailColumns = $quikActivityGrid
                .find('[data-browsedatafield="ICode"], [data-browsedatafield="Description"]')
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

        $calendar
            .data('ongetevents', request => {
                const startOfMonth = moment(request.start.value).format('MM/DD/YYYY');
                const endOfMonth = moment(request.start.value).add(request.days, 'd').format('MM/DD/YYYY');
                const summary = FwFormField.getValueByDataField($popup, 'Summary');
                let warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
                if (FwFormField.getValueByDataField($form, 'AllWarehouses') === 'T') {
                    warehouseId = '';
                }
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

                    $quikActivityGridControl.data('ondatabind', request => {
                        request.uniqueids = {
                            WarehouseId: warehouseId,
                            FromDate: startOfMonth,
                            ToDate: endOfMonth,
                            ActivityType: activityTypes,
                            Summary: summary,
                            SessionId: this.SessionId,
                        };
                    });
                    FwBrowse.search($quikActivityGridControl);

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
            .data('onheaderclick', event => {
                try {
                    const $overlay = FwOverlay.showPleaseWaitOverlay($form, null);
                    const date = event.header.name;
                    const summary = FwFormField.getValueByDataField($popup, 'Summary');
                    let warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
                    if (FwFormField.getValueByDataField($form, 'AllWarehouses') === 'T') {
                        warehouseId = '';
                    }
                    $popup.find('.activities-header .fwform-section-title').text(`Activities for ${date}`);
                    FwPopup.showPopup($popup);
                    $quikActivityGridControl.data('ondatabind', request => {
                        request.uniqueids = {
                            WarehouseId: warehouseId,
                            FromDate: date,
                            ToDate: date,
                            Summary: summary,
                            SessionId: this.SessionId,
                        };
                    });
                    FwBrowse.search($quikActivityGridControl);
                    FwOverlay.hideOverlay($overlay);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .data('ontimerangeselect', event => {
                try {
                    const $overlay = FwOverlay.showPleaseWaitOverlay($form, null);
                    const fromDate = moment(event.start.value).format('MM/DD/YYYY');
                    const toDate = moment(event.start.value).format('MM/DD/YYYY');
                    const summary = FwFormField.getValueByDataField($popup, 'Summary');
                    let warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
                    if (FwFormField.getValueByDataField($form, 'AllWarehouses') === 'T') {
                        warehouseId = '';
                    }
                    $popup.find('.activities-header .fwform-section-title').text(`Activities for ${fromDate}`);
                    FwPopup.showPopup($popup);
                    $quikActivityGridControl.data('ondatabind', request => {
                        request.uniqueids = {
                            WarehouseId: warehouseId,
                            FromDate: fromDate,
                            ToDate: toDate,
                            Summary: summary,
                            SessionId: this.SessionId,
                        };
                    });
                    FwBrowse.search($quikActivityGridControl);
                    FwOverlay.hideOverlay($overlay);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .data('oneventclick', e => {
                try {
                    const $overlay = FwOverlay.showPleaseWaitOverlay($form, null);
                    const data = e.e.data;
                    const fromDate = moment(data.start.value).format('MM/DD/YYYY');
                    const toDate = moment(data.end.value).format('MM/DD/YYYY');
                    const activityType = data.activityType;
                    const summary = FwFormField.getValueByDataField($popup, 'Summary');
                    let warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
                    if (FwFormField.getValueByDataField($form, 'AllWarehouses') === 'T') {
                        warehouseId = '';
                    }
                    $popup.find('.activities-header .fwform-section-title').text(`${activityType} Activities for ${fromDate}`);
                    FwPopup.showPopup($popup);
                    $quikActivityGridControl.data('ondatabind', request => {
                        request.uniqueids = {
                            WarehouseId: warehouseId,
                            FromDate: fromDate,
                            ToDate: toDate,
                            ActivityType: activityType,
                            Summary: summary,
                            SessionId: this.SessionId,
                        };
                    });
                    FwBrowse.search($quikActivityGridControl);
                    FwOverlay.hideOverlay($overlay);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
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
        $form.find('.calendarmenu').css({
            'border-left': '1px solid #a9a9a9',
            'border-right': '1px solid #a9a9a9'});

        const $calendar = $form.find('.calendar');
        $form.on('change', '[data-datafield="WarehouseId"]', e => {
            FwScheduler.refresh($calendar);
        });

        $form.find('[data-datafield="AllWarehouses"] .fwformfield-value').on('change', function () {
            let $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.disable($form.find('[data-datafield="WarehouseId"]'));
            } else {
                FwFormField.enable($form.find('[data-datafield="WarehouseId"]'));
            }
            FwScheduler.refresh($calendar);
        });

        //Initial settings load
        const id = JSON.parse(sessionStorage.getItem('userid')).webusersid;
        const $loadSettings = $form.find('[data-datafield="Load"]');
        const loadSettings = () => {
            const request: any = {};
            request.WebUsersId = id;
            FwAppData.apiMethod(true, 'GET', `api/v1/quikactivitysettings/`, request, FwServices.defaultTimeout, response => {
                let settingsObj: any = [];
                $loadSettings.data('settings', response);
                for (let i = 0; i < response.length; i++) {
                    let $this = response[i];
                    const setting = {
                        text: $this.Description,
                        value: $this.Id
                    }
                    settingsObj.push(setting);
                }
                FwFormField.loadItems($loadSettings, settingsObj);
            }, ex => {
                FwFunc.showError(ex);
            }, $calendar);
        };
        loadSettings();

        //Save settings
        $form.find('.save-settings').on('click', e => {
            const request: any = {};
            request.WebUsersId = id;
            request.Description = FwFormField.getValueByDataField($form, 'Description');
            const $settingsControls = $form.find('.fwformfield[data-savesetting!="false"]');
            let $settingsObj: any = [];
            if ($settingsControls.length > 0) {
                for (let i = 0; i < $settingsControls.length; i++) {
                    let $this = jQuery($settingsControls[i]);
                    const datafield = $this.attr('data-datafield')
                    const type = $this.attr('data-type');
                    $settingsObj.push({
                        DataField: datafield
                        , DataType: type
                        , Value: FwFormField.getValue2($this)
                        , Text: FwFormField.getText2($this)
                    });
                }
            }
            request.Settings = JSON.stringify($settingsObj);
            FwAppData.apiMethod(true, 'POST', `api/v1/quikactivitysettings/`, request, FwServices.defaultTimeout, response => {
                loadSettings();
            }, ex => {
                FwFunc.showError(ex);
            }, $calendar);
        });

        //Load settings
        $form.find('[data-datafield="Load"]').on('change', e => {
            let settingId = jQuery(e.target).val();
            let settings = $loadSettings.data('settings');
            settings = settings.filter(obj => { return obj.Id == settingId });
            if (settings.length > 0) {
                let savedSettings = JSON.parse(settings[0].Settings);
                for (let i = 0; i < savedSettings.length; i++) {
                    let item = savedSettings[i];
                    if (item.DataType === 'checkbox') {
                        item.Value == "T" ? item.Value = true : item.Value = false;
                    }
                    FwFormField.setValueByDataField($form, item.DataField, item.Value, item.Text);
                }
                FwNotification.renderNotification('SUCCESS', 'Settings Successfully Loaded.');
                $form.find('.activities [data-type="checkbox"]').eq(0).change();
            }
        });

    };
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
                <div id="quikactivitycalendarform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="QuikActivity Calendar" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="QuikActivityCalendarController">
                  <div class="flexrow" style="max-width:none;">
                    <div class="flexcolumn activitieslist" style="max-width:250px">
                      <div class="flexrow">
                        <div class="fwcontrol fwcontainer fwform-section activities" data-control="FwContainer" data-type="section" data-caption="Activities"></div>
                      </div>
                      <div class="flexrow">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Warehouse">
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Filter Warehouse" data-datafield="WarehouseId" data-validationname="WarehouseValidation" data-displayfield="WarehouseCode" style="max-width:250px;"></div>
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="All Warehouses" data-datafield="AllWarehouses" style="min-width:150px;max-width:150px;"></div>
                        </div>
                      </div>
                      <div class="flexrow">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Settings">
                            <div class="flexrow">                            
                                <div data-control="FwFormField" data-type="text" data-savesetting="false" class="fwcontrol fwformfield" data-caption="Save QuikActivity Settings as" data-datafield="Description" style="max-width:200px;"></div>
                                <i class="material-icons save-settings" style="max-width:25px; margin:25px 0px;">save</i>
                            </div>
                            <div data-control="FwFormField" data-type="select" data-savesetting="false" class="fwcontrol fwformfield" data-caption="Load QuikActivity Settings" data-datafield="Load" style="max-width:250px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="margin:30px 0px 0px 50px; min-width:1210px;max-width:1210px;">
                      <div data-control="FwScheduler" class="fwcontrol fwscheduler calendar" data-shownav="false"></div>
                    </div>
                  </div>
`;
    }
}
var QuikActivityCalendarController = new QuikActivityCalendar();