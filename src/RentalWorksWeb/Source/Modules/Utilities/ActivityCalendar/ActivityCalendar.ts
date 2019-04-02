routes.push({ pattern: /^module\/activitycalendar$/, action: function (match: RegExpExecArray) { return ActivityCalendarController.getModuleScreen(); } });

class ActivityCalendar {
    Module: string = 'ActivityCalendar';
    caption: string = 'Activity Calendar';
    nav: string = 'module/activitycalendar';
    id: string = '897BCF55-6CE7-412C-82CB-557B045F8C0A';
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
        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    populateCheckboxes($form: any) {
        const request = {};
        FwAppData.apiMethod(true, 'POST', `api/v1/orderactivitytype/browse`, request, FwServices.defaultTimeout, response => {
            try {
                const $activities = $form.find('.activities');
                const descriptionIndex = response.ColumnIndex.Description;
                const activityType = response.ColumnIndex.ActivityType;
                const isSystemTypeIndex = response.ColumnIndex.IsSystemType;
                const colorIndex = response.ColumnIndex.Color;
                for (let i = 0; i < response.Rows.length; i++) {
                    const self = response.Rows[i];
                    const item = `<div class="flexrow">
                                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="${self[descriptionIndex]}" data-datafield="${self[activityType]}"></div>
                                    <div style="background-color:${self[colorIndex]}; max-width:30px; margin:16px 0px; border:1px solid black;"></div>
                                  </div>`;
                    $activities.append(item);
                }
                const $fwcontrols = $activities.find('.fwcontrol');
                FwControl.renderRuntimeControls($fwcontrols);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, $form);
    }
    //----------------------------------------------------------------------------------------------
    events($form: any) {
        $form.find('.calendarmenu').css('border-left', '1px solid #a9a9a9');
    };
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
                <div id="activitycalendarform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Activity Calendar" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="ActivityCalendarController">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                        <div class="flexrow">
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Filter Warehouse" data-datafield="WarehouseId" data-validationname="WarehouseValidation" data-displayfield="WarehouseCode"></div>
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="All Warehouses" data-datafield="AllWarehouses"></div>
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-validationname="DepartmentValidation" data-displayfield="Department"></div>
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-validationname="DealValidation" data-displayfield="Deal"></div>
                            <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield toggle" data-caption="" data-datafield="" style="flex:0 1 125px;margin-left:15px;">
                              <div data-value="QUOTE" data-caption="Quote"></div>
                              <div data-value="ORDER" data-caption="Order"></div>
                            </div>
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="" data-datafield="QuoteId" data-validationname="QuoteValidation" data-displayfield="QuoteNumber"></div>
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="" data-datafield="OrderId" data-validationname="OrderValidation" data-displayfield="OrderNumber"></div>
                        </div>
                    </div>
                    <div class="flexrow" style="max-width:none;">
                        <div class="flexcolumn" style="max-width:250px;">
                            <div class="fwcontrol fwcontainer fwform-section activities" data-control="FwContainer" data-type="section" data-caption="Activities">
                            </div>
                        </div>
                        <div class="flexcolumn">
                            <div data-control="FwScheduler" class="fwcontrol fwscheduler calendar"></div>
                            <div data-control="FwSchedulerDetailed" class="fwcontrol fwscheduler realscheduler"></div>
                        </div>
                    </div>
                </div>`;
    }
}
var ActivityCalendarController = new ActivityCalendar();