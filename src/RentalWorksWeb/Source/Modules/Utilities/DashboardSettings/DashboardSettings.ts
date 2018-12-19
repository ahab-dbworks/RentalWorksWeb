routes.push({ pattern: /^module\/dashboardsettings$/, action: function (match: RegExpExecArray) { return DashboardSettingsController.getModuleScreen(); } });

class DashboardSettings {
    Module: string = 'DashboardSettings';
    apiurl: string = 'api/v1/userdashboardsettings';
    caption: string = 'Dashboard Settings';
    nav: string = 'module/dashboardsettings';
    id: string = '1B40C62A-1FA0-402E-BE52-9CBFDB30AD3F';
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('EDIT');

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
        var userId = JSON.parse(sessionStorage.getItem('userid'));

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        $form.find('div.fwformfield[data-datafield="UserId"] input').val(userId.webusersid);
        FwModule.loadForm(this.Module, $form);

        var newsort = Sortable.create($form.find('.sortable').get(0), {
            onEnd: function (evt) {
                $form.attr('data-modified', 'true');
                $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
            }
        });

        FwFormField.loadItems($form.find('.widgettype'), [
            { value: 'bar', text: 'Bar' },
            { value: 'horizontalBar', text: 'Horizontal Bar' },
            { value: 'pie', text: 'Pie' }
        ], true);

        FwFormField.loadItems($form.find('.datebehavior'), [
            { value: 'NONE', text: 'None' },
            { value: 'SINGLE DATE - YESTERDAY', text: 'Single Date - Yesterday' },
            { value: 'SINGLE DATE - TODAY', text: 'Single Date - Today' },
            { value: 'SINGLE DATE - TOMORROW', text: 'Single Date - Tomorrow' },
            { value: 'SINGLE DATE - SPECIFIC DATE', text: 'Single Date - Specific Date' },
            { value: 'DATE RANGE - PRIOR WEEK', text: 'Date Range - Prior Week' },
            { value: 'DATE RANGE - CURRENT WEEK', text: 'Date Range - Current Week' },
            { value: 'DATE RANGE - PRIOR MONTH', text: 'Date Range - Prior Month' },
            { value: 'DATE RANGE - CURRENT MONTH', text: 'Date Range - Current Month' },
            { value: 'DATE RANGE - NEXT MONTH', text: 'Date Range - Next Week' },
            { value: 'DATE RANGE - PRIOR YEAR', text: 'Date Range - Prior Year' },
            { value: 'DATE RANGE - CURRENT YEAR', text: 'Date Range - Current Year' },
            { value: 'DATE RANGE - YEAR TO DATE', text: 'Date Range - Year To Date' },
            { value: 'DATE RANGE - NEXT YEAR', text: 'Date Range - Next Year' },
            { value: 'DATE RANGE - SPECIFIC DATES', text: 'Date Range - Specific Dates' }
        ], true);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        //FwModule.saveForm(this.Module, $form, { closetab: closetab, navigationpath: 'home' });
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        $form.find('.settings').on('click', function () {
            let widgetId = jQuery(this).closest('li').data('value');
            let userWidgetId = jQuery(this).closest('li').data('userwidgetid');


        })
    }
    //----------------------------------------------------------------------------------------------
}
var DashboardSettingsController = new DashboardSettings();