routes.push({ pattern: /^module\/quikactivitycalendar$/, action: function (match: RegExpExecArray) { return QuikActivityCalendarController.getModuleScreen(); } });

class QuikActivityCalendar {
    Module: string = 'QuikActivityCalendar';
    apiurl: string = 'api/v1/quikactivity'
    caption: string = Constants.Modules.Utilities.children.QuikActivityCalendar.caption;
    nav: string = Constants.Modules.Utilities.children.QuikActivityCalendar.nav;
    id: string = Constants.Modules.Utilities.children.QuikActivityCalendar.id;
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions) {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
    }
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
        let request: any = {};
        request.searchfieldoperators = ["<>"];
        request.searchfields = ["Inactive"];
        request.searchfieldvalues = ["T"];
        request.OrderBy = 'DescriptionDisplay';
        FwAppData.apiMethod(true, 'POST', `api/v1/activitytype/browse`, request, FwServices.defaultTimeout, response => {
            try {
                const $activities = $form.find('.activities .fwform-section-body');
                const descriptionIndex = response.ColumnIndex.DescriptionDisplay;
                const activityTypeIdIndex = response.ColumnIndex.ActivityTypeId;
                const activityTypeIndex = response.ColumnIndex.ActivityType;
                //const isSystemTypeIndex = response.ColumnIndex.IsSystemType;
                const colorIndex = response.ColumnIndex.Color;
                for (let i = 0; i < response.Rows.length; i++) {
                    const self = response.Rows[i];
                    const item = `<div class="flexrow" style="max-height:2em; margin-top:.5em;">
                                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="${self[descriptionIndex]}" data-datafield="${self[activityTypeIdIndex]}"></div>
                                    <div style="background-color:${self[colorIndex]}; max-width:30px; margin:16px 0px; border:1px solid black;"></div>
                                  </div>`;
                    $activities.append(item);
                }
                const $fwcontrols = $activities.find('.fwcontrol');
                FwControl.renderRuntimeControls($fwcontrols);
                $fwcontrols.find('.checkbox-caption').css('white-space', 'normal');
                $form.find('.activities [data-type="checkbox"] input').prop('checked', true);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, $form);
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'WarehouseId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatewarehouse`);
        }
    }
    //----------------------------------------------------------------------------------------------
    calendarEvents($form: any) {
        const $calendar = $form.find('.calendar');
        let activityTypes = '';

        let $popup = FwPopup.renderPopup(this.quikActivityCalendarPopupContent(), { ismodal: true });
        FwControl.renderRuntimeControls($popup.find('.fwcontrol'));
        FwBrowse.renderGrid({
            nameGrid: 'QuikActivityGrid',
            gridSecurityId: 'yhYOLhLE92IT',
            moduleSecurityId: this.id,
            $form: $popup,
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                const isSummary = FwFormField.getValueByDataField($popup, 'Summary');
                const $detailColumns = $quikActivityGrid
                    .find('[data-browsedatafield="InventoryId"], [data-browsedatafield="Description"]')
                    .parents('.column');
                const detailReadOnlyColumns = $quikActivityGrid.find('[data-browsedatafield="ActivityStatusId"], [data-browsedatafield="AssignedToUserId"]');
                if (isSummary == 'true') {
                    $detailColumns.hide();
                    detailReadOnlyColumns.attr('data-formreadonly', 'false');
                } else {
                    $detailColumns.show();
                    detailReadOnlyColumns.attr('data-formreadonly', 'true');
                }
            }
        });
        const $quikActivityGrid = $popup.find('div[data-grid="QuikActivityGrid"]');
        const $quikActivityGridControl = $popup.find('div[data-name="QuikActivityGrid"]');

        $form.data('onscreenunload', () => { FwPopup.destroyPopup($popup); });
        this.quikActivityCalendarInit($form, $popup);
        $popup.on('click', '.close-modal', e => {
            FwPopup.detachPopup($popup);
        });
 
        $calendar
            .data('ongetevents', request => {
                const startOfMonth = moment(request.start.value).format('MM/DD/YYYY');
                const endOfMonth = moment(request.start.value).add(request.days, 'd').format('MM/DD/YYYY');
                const summary = FwFormField.getValueByDataField($popup, 'Summary');
                let officeLocationId: string = '';//FwFormField.getValueByDataField($form, 'OfficeLocationId');
                let departmentId: string = FwFormField.getValueByDataField($form, 'DepartmentId');
                let warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
                if (FwFormField.getValueByDataField($form, 'AllWarehouses')) {
                    warehouseId = '';
                }
                let includeTimes: boolean = (request.mode === 'Day' || request.mode === 'Week');
                let calendarRequest: any = {
                    FromDate: startOfMonth,
                    ToDate: endOfMonth,
                    OfficeLocationId: officeLocationId,
                    WarehouseId: warehouseId,
                    DepartmentId: departmentId,
                    ActivityTypeId: activityTypes,
                    IncludeTimes: includeTimes,
                    AssignedToUserId: FwFormField.getValueByDataField($form, 'MyActivitiesOnly') ? JSON.parse(sessionStorage.getItem('userid')).usersid : '',
                    IncludeCompleted: FwFormField.getValueByDataField($form, 'IncludeCompleted')
                }

                FwAppData.apiMethod(true, 'POST', `api/v1/quikactivity/calendardata`, calendarRequest, FwServices.defaultTimeout, response => {
                    const calendarEvents = response.QuikActivityCalendarEvents;
                    //FwScheduler.loadYearEventsCallback($calendar, [{ id: '1', name: '' }], calendarEvents);
                    for (var i = 0; i < calendarEvents.length; i++) {
                        if (calendarEvents[i].textColor !== 'rgb(0,0,0)') {
                            calendarEvents[i].html = `<div style="color:${calendarEvents[i].textColor}">${calendarEvents[i].text}</div>`
                        }
                    }
                    FwScheduler.loadEventsCallback($calendar, [{ id: '1', name: '' }], calendarEvents);

                    $quikActivityGridControl.data('ondatabind', request => {
                        request.uniqueids = {
                            FromDate: startOfMonth,
                            ToDate: endOfMonth,
                            OfficeLocationId: officeLocationId,
                            WarehouseId: warehouseId,
                            DepartmentId: departmentId,
                            ActivityTypeId: activityTypes,
                            Summary: summary,
                            AssignedToUserId: FwFormField.getValueByDataField($form, 'MyActivitiesOnly') ? JSON.parse(sessionStorage.getItem('userid')).usersid : '',
                            IncludeCompleted: FwFormField.getValueByDataField($form, 'IncludeCompleted')
                        };
                    });

                    FwBrowse.search($quikActivityGridControl);

                }, ex => {
                    FwFunc.showError(ex);
                }, $calendar)
            })
            .data('ontimerangeselect', event => {
                try {
                    const $overlay = FwOverlay.showPleaseWaitOverlay($form, null);
                    const fromDate = moment(event.start.value).format('MM/DD/YYYY');
                    const toDate = moment(event.start.value).format('MM/DD/YYYY');
                    const summary = FwFormField.getValueByDataField($popup, 'Summary');
                    let warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
                    if (FwFormField.getValueByDataField($form, 'AllWarehouses')) {
                        warehouseId = '';
                    }
                    let officeLocationId: string = '';//FwFormField.getValueByDataField($form, 'OfficeLocationId');
                    let departmentId: string = FwFormField.getValueByDataField($form, 'DepartmentId');
                    $popup.find('.activities-header .fwform-section-title').text(`Activities for ${fromDate}`);
                    FwPopup.showPopup($popup);
                    $quikActivityGridControl.data('ondatabind', request => {
                        request.uniqueids = {
                            FromDate: fromDate,
                            ToDate: toDate,
                            OfficeLocationId: officeLocationId,
                            WarehouseId: warehouseId,
                            DepartmentId: departmentId,
                            ActivityTypeId: activityTypes,
                            Summary: summary,
                            AssignedToUserId: FwFormField.getValueByDataField($form, 'MyActivitiesOnly') ? JSON.parse(sessionStorage.getItem('userid')).usersid : '',
                            IncludeCompleted: FwFormField.getValueByDataField($form, 'IncludeCompleted')
                        };
                    });
                    FwBrowse.search($quikActivityGridControl);
                    FwOverlay.hideOverlay($overlay);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .data('ontimerangedoubleclicked', event => {
                try {
                    if (typeof $calendar.data('ontimerangeselect') === 'function') {
                        $calendar.data('ontimerangeselect')(event);
                    }
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
                    if (FwFormField.getValueByDataField($form, 'AllWarehouses')) {
                        warehouseId = '';
                    }
                    let officeLocationId: string = '';//FwFormField.getValueByDataField($form, 'OfficeLocationId');
                    let departmentId: string = FwFormField.getValueByDataField($form, 'DepartmentId');
                    $popup.find('.activities-header .fwform-section-title').text(`Activities for ${date}`);
                    FwPopup.showPopup($popup);
                    $quikActivityGridControl.data('ondatabind', request => {
                        request.uniqueids = {
                            FromDate: date,
                            ToDate: date,
                            OfficeLocationId: officeLocationId,
                            WarehouseId: warehouseId,
                            DepartmentId: departmentId,
                            ActivityTypeId: activityTypes,
                            Summary: summary,
                            AssignedToUserId: FwFormField.getValueByDataField($form, 'MyActivitiesOnly') ? JSON.parse(sessionStorage.getItem('userid')).usersid : '',
                            IncludeCompleted: FwFormField.getValueByDataField($form, 'IncludeCompleted')
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
                    if (FwFormField.getValueByDataField($form, 'AllWarehouses')) {
                        warehouseId = '';
                    }
                    let officeLocationId: string = '';//FwFormField.getValueByDataField($form, 'OfficeLocationId');
                    let departmentId: string = FwFormField.getValueByDataField($form, 'DepartmentId');
                    $popup.find('.activities-header .fwform-section-title').text(`${activityType} Activities for ${fromDate}`);
                    FwPopup.showPopup($popup);
                    $quikActivityGridControl.data('ondatabind', request => {
                        request.uniqueids = {
                            FromDate: fromDate,
                            ToDate: toDate,
                            OfficeLocationId: officeLocationId,
                            WarehouseId: warehouseId,
                            DepartmentId: departmentId,
                            ActivityTypeId: activityTypes,
                            Summary: summary,
                            AssignedToUserId: FwFormField.getValueByDataField($form, 'MyActivitiesOnly') ? JSON.parse(sessionStorage.getItem('userid')).usersid : '',
                            IncludeCompleted: FwFormField.getValueByDataField($form, 'IncludeCompleted')
                        };
                    });
                    FwBrowse.search($quikActivityGridControl);
                    FwOverlay.hideOverlay($overlay);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .data('oneventdoubleclick', event => {
                if (typeof $calendar.data('oneventclick') === 'function') {
                    $calendar.data('oneventclick')(event);
                }
            })
            .data('oneventdoubleclicked', event => {
                if (typeof $calendar.data('oneventclick') === 'function') {
                    $calendar.data('oneventclick')(event);
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
            'border-right': '1px solid #a9a9a9'
        });

        const $calendar = $form.find('.calendar');
        $form.on('change', '[data-datafield="WarehouseId"]', e => {
            FwScheduler.refresh($calendar);
        });

        //Initial settings load
        const id = JSON.parse(sessionStorage.getItem('userid')).webusersid;
        const $loadSettings = $form.find('[data-datafield="Load"]');
        const loadSettings = (loadSavedId?: string) => {
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
                if (loadSavedId) {
                    FwFormField.setValueByDataField($form, 'Load', loadSavedId, null, false);
                };
            }, ex => {
                FwFunc.showError(ex);
            }, $calendar);
        };
        loadSettings();

        //Save settings
        $form.find('.save-settings').on('click', e => {
            const isNew = jQuery(e.currentTarget).hasClass('new');
            const settingId = FwFormField.getValueByDataField($form, 'Load');

            if (!isNew && !settingId) {
                FwNotification.renderNotification("ERROR", "Select a saved setting to update.");
                return;
            }

            const request: any = {};
            request.WebUsersId = id;
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
                        //, Text: FwFormField.getText2($this)
                    });
                }
            }
            request.Settings = JSON.stringify($settingsObj);

            if (isNew) {
                request.Description = FwFormField.getValueByDataField($form, 'Description');
                FwAppData.apiMethod(true, 'POST', `api/v1/quikactivitysettings/`, request, FwServices.defaultTimeout, response => {
                    loadSettings(response.Id);
                    $form.find('.add-new-settings').hide();
                    FwFormField.setValueByDataField($form, 'Description', '');
                }, ex => {
                    FwFunc.showError(ex);
                }, $form);
            } else {
                request.Description = $form.find('[data-datafield="Load"] option:selected').text();
                request.Id = settingId;
                FwAppData.apiMethod(true, 'PUT', `api/v1/quikactivitysettings/${settingId}`, request, FwServices.defaultTimeout, response => {
                    loadSettings(settingId);
                }, ex => {
                    FwFunc.showError(ex);
                }, $form);
            }
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
                    FwFormField.setValueByDataField($form, item.DataField, item.Value);
                }
                FwNotification.renderNotification('SUCCESS', 'Settings Successfully Loaded.');
                $form.find('.activities [data-type="checkbox"]').eq(0).change();
            }
        });

        $form.find('.add-settings').on('click', e => {
            $form.find('.add-new-settings').show();
        });

        $form.find('.delete-settings').on('click', e => {
            const settingId = FwFormField.getValueByDataField($form, 'Load');
            FwAppData.apiMethod(true, 'DELETE', `api/v1/quikactivitysettings/${settingId}`, null, FwServices.defaultTimeout, response => {
                loadSettings();
            }, ex => {
                FwFunc.showError(ex);
            }, $calendar);
        });

        $form.find('.cancel-settings').on('click', e => {
            $form.find('.add-new-settings').hide();
        });

        //filter events
        $form.on('change', '.filters', e => {
            FwScheduler.refresh($calendar);
        });

        $form.find('[data-datafield="AllWarehouses"]').on('change', e => {
            let $this = jQuery(e.target);
            if ($this.prop('checked')) {
                FwFormField.disable($form.find('[data-datafield="WarehouseId"]'));
            } else {
                FwFormField.enable($form.find('[data-datafield="WarehouseId"]'));
            }
            FwScheduler.refresh($calendar);
        });
    };
    //----------------------------------------------------------------------------------------------
    quikActivityCalendarPopupContent() {
        let $content = jQuery(`
                <div id="quikActivityPopup" class="fwform fwcontrol fwcontainer" data-caption="QuikActivity Calendar Browse" data-control="FwContainer" data-type="form" style="max-height:90vh;max-width:90vw;background-color:white; padding:10px; border:2px solid gray;">
                    <div class="popup-buttons" style="display:flex; justify-content:flex-end;">
                        <div class="pop-out" style="cursor:pointer; margin-right:1.5em;"><i class="material-icons" title="Pop Out">open_in_new</i><div class="btn-text" style="float:right;">Pop-Out</div></div>
                        <div class="close-modal" style="position:static;"><i class="material-icons">clear</i><div class="btn-text">Close</div></div>
                    </div>
                    <div class="flexcolumn">
                      <div class="flexrow" style="max-width:inherit;">
                         <div class="fwcontrol fwcontainer fwform-section activities-header" data-control="FwContainer" data-type="section" data-caption="Activities">
                            <div class="flexrow">
                                <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="" data-datafield="Summary" style="flex:1 1 600px"></div>
                                <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="My Activities only" data-datafield="MyActivity" style="margin:.5em;"></div>
                                <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Complete Activities" data-datafield="CompleteActivity" style="margin:.5em;"></div>
                            </div>                        
                         </div>
                      </div>
                      <div class="flexrow" style="max-width:inherit;">
                        <div data-control="FwGrid" data-grid="QuikActivityGrid" data-securitycaption="QuikActivity" style="overflow:auto;max-height:80vh;"></div>
                      </div>
                    </div>
                </div>`);
        return $content;
    }
    //----------------------------------------------------------------------------------------------
    quikActivityCalendarInit($form: JQuery, $popup: JQuery) {
        if ($popup.attr('data-poppedouttab') != 'true') {
            FwFormField.loadItems($popup.find('div[data-datafield="Summary"]'), [
                { value: 'true', caption: 'Summary', selected: true },
                { value: 'false', caption: 'Detail' }
            ]);
            FwFormField.setValueByDataField($popup, 'Summary', 'true');
        }

        //pop-out button
        $popup.on('click', '.pop-out', e => {
            const $quikActivityGridControl = $popup.find('div[data-name="QuikActivityGrid"]');
            const $gridClone = $quikActivityGridControl.clone();
            const griddatabind = $quikActivityGridControl.data('ondatabind');
            setTimeout(() => {
                const $popoutContent = this.quikActivityCalendarPopupContent();
                FwControl.renderRuntimeControls($popoutContent.find('.fwcontrol'));
                FwModule.openSubModuleTab($form, $popoutContent);
                $popoutContent.find('.popup-buttons').remove();
                $popoutContent.css('border', 'none');
                const myActivity = FwFormField.getValueByDataField($popup, 'MyActivity');
                const completeActivity = FwFormField.getValueByDataField($popup, 'CompleteActivity');
                FwFormField.setValueByDataField($popoutContent, 'MyActivity', myActivity === 'T' ? true : false);
                FwFormField.setValueByDataField($popoutContent, 'CompleteActivity', completeActivity === 'T' ? true : false);
                this.quikActivityCalendarInit($form, $popoutContent); //attaches events to new popuptab 
                const activityHeader = $popup.find('.activities-header .fwform-section-title').text();
                const tabid = $popoutContent.closest('.tabpage').attr('data-tabid');
                jQuery(`#${tabid} .caption`).text(activityHeader);
                const $popOutGrid = $popoutContent.find('[data-grid="QuikActivityGrid"]');
                $popOutGrid.empty().append($gridClone);
                $gridClone.data('ondatabind', griddatabind);
                FwBrowse.search($gridClone);
                FwPopup.detachPopup($popup);
            });
        });

        $popup.on('change', '[data-datafield="Summary"]', e => {
            const isSummary = FwFormField.getValueByDataField($popup, 'Summary');
            const $quikActivityGridControl = $popup.find('div[data-name="QuikActivityGrid"]');
            const onDataBind = $quikActivityGridControl.data('ondatabind');
            if (typeof onDataBind == 'function') {
                $quikActivityGridControl.data('ondatabind', request => {
                    onDataBind(request);
                    request.uniqueids.Summary = isSummary;
                });
            }
            FwBrowse.search($quikActivityGridControl);
        });

        $popup.on('change', '[data-datafield="MyActivity"], [data-datafield="CompleteActivity"]', e => {
            const myActivity = FwFormField.getValueByDataField($popup, 'MyActivity');
            const completeActivity = FwFormField.getValueByDataField($popup, 'CompleteActivity');
            const $quikActivityGridControl = $popup.find('div[data-name="QuikActivityGrid"]');
            const onDataBind = $quikActivityGridControl.data('ondatabind');
            if (typeof onDataBind == 'function') {
                $quikActivityGridControl.data('ondatabind', request => {
                    onDataBind(request);
                    request.uniqueids.AssignedToUserId = myActivity == 'T' ? JSON.parse(sessionStorage.getItem('userid')).usersid : '';
                    request.uniqueids.IncludeCompleted = completeActivity == 'T' ? true : false;
                });
            }
            FwBrowse.search($quikActivityGridControl);
        });
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
                <div id="quikactivitycalendarform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="QuikActivity Calendar" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="QuikActivityCalendarController">
                  <div class="flexrow" style="max-width:none;">
                    <div class="flexcolumn activitieslist" style="max-width:250px">
                      <div class="flexrow">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="QuikActivity Saved Settings">
                            <div class="flexrow">                            
                                <div data-control="FwFormField" data-type="select" data-savesetting="false" class="fwcontrol fwformfield" data-caption="QuikActivity Setting" data-datafield="Load" style="max-width:200px;"></div>
                                <i class="material-icons save-settings" style="cursor:pointer; max-width:25px; margin:25px 0px;">save</i>
                                <i class="material-icons add-settings" style="cursor:pointer; max-width:25px; margin:25px 0px; color:#4caf50;">add_circle</i>
                                <i class="material-icons delete-settings" style="cursor:pointer; max-width:25px; margin:25px 0px;">delete</i>
                            </div>
                            <div class="flexrow add-new-settings" style="display:none;">                            
                                <div data-control="FwFormField" data-type="text" data-savesetting="false" class="fwcontrol fwformfield" data-caption="Save QuikActivity Settings As" data-datafield="Description" style="max-width:250px;"></div>            
                                <i class="material-icons save-settings new" style="cursor:pointer; max-width:25px; margin:25px 0px;">save</i>
                                <i class="material-icons cancel-settings" style="cursor:pointer; max-width:25px; margin:25px 0px;">highlight_off</i>   
                            </div>
                         </div>
                      </div>
                      <div class="flexrow">
                        <div class="fwcontrol fwcontainer fwform-section activities" data-control="FwContainer" data-type="section" data-caption="Activities"></div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="margin:30px 0px 0px 50px; min-width:1210px;max-width:1210px;">
                      <div data-control="FwScheduler" class="fwcontrol fwscheduler calendar" data-shownav="false"></div>
                    </div>
                    <div class="flexcolumn" style="z-index:0;">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Calendar View Filters">
                            <div data-control="FwFormField" data-type="validation" class="filters fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-validationname="WarehouseValidation" data-displayfield="WarehouseCode" style="max-width:250px;"></div>
                            <div data-control="FwFormField" data-type="checkbox" class="filters fwcontrol fwformfield" data-caption="All Warehouses" data-datafield="AllWarehouses" style="min-width:150px;max-width:150px;"></div>
                            <div data-control="FwFormField" data-type="validation" class="filters fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-validationname="DepartmentValidation" data-displayfield="Department" style="max-width:250px;"></div>
                            <!--<div data-control="FwFormField" data-type="validation" class="filters fwcontrol fwformfield" data-caption="Office Location" data-datafield="OfficeLocationId" data-validationname="OfficeLocationValidation" data-displayfield="OfficeLocation" style="max-width:250px;"></div>-->
                            <div data-control="FwFormField" data-type="checkbox" class="filters fwcontrol fwformfield" data-caption="My Activities Only" data-datafield="MyActivitiesOnly" style="min-width:150px;max-width:150px;"></div>
                            <div data-control="FwFormField" data-type="checkbox" class="filters fwcontrol fwformfield" data-caption="Show Complete Activities" data-datafield="IncludeCompleted" style="min-width:150px;max-width:150px;"></div>
                            <!--<div data-control="FwFormField" data-type="checkbox" class="filters fwcontrol fwformfield" data-caption="Auto Refresh every" data-datafield="" style="min-width:150px;max-width:150px;"></div>-->
                        </div>
                    </div>
                  </div>
`;
    }
}
var QuikActivityCalendarController = new QuikActivityCalendar();