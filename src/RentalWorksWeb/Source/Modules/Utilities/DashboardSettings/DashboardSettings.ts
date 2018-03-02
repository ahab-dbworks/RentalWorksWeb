routes.push({ pattern: /^module\/dashboardsettings$/, action: function (match: RegExpExecArray) { return DashboardSettingsController.getModuleScreen(); } });

class DashboardSettings {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'DashboardSettings';
        this.apiurl = 'api/v1/userwidget'; 
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('NEW');

        screen.load = function () {
            FwModule.openModuleTab($form, 'Dashboard Settings', false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        var $form;
        var widgets = [];
        var userId = JSON.parse(sessionStorage.getItem('userid'));

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);

        FwAppData.apiMethod(true, 'GET', "api/v1/widget/", null, FwServices.defaultTimeout, function onSuccess(response) {
            console.log(response)
            for (var i = 0; i < response.length; i++) {
                widgets.push({
                    'orderbydirection': 'asc',
                    'selected': 'F',
                    'text': response[i].Widget,
                    'value': response[i].WidgetId
                })
            }

            FwFormField_checkboxlist.loadItems($form.find('.widgetorder'), widgets);
        }, null, $form)

        
        $form.find('div.fwformfield[data-datafield="UserId"] input').val(userId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, { closetab: closetab, navigationpath: navigationpath });
    }
    //----------------------------------------------------------------------------------------------
    //afterLoad($form: any) {

    //}
    //----------------------------------------------------------------------------------------------

}
var DashboardSettingsController = new DashboardSettings();