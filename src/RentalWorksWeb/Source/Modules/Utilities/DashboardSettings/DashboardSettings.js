routes.push({ pattern: /^module\/dashboardsettings$/, action: function (match) { return DashboardSettingsController.getModuleScreen(); } });
var DashboardSettings = (function () {
    function DashboardSettings() {
        this.Module = 'DashboardSettings';
        this.apiurl = 'api/v1/userdashboardsettings';
    }
    DashboardSettings.prototype.getModuleScreen = function () {
        var screen = {};
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
    };
    DashboardSettings.prototype.openForm = function (mode) {
        var $form;
        var widgets = [];
        var userId = JSON.parse(sessionStorage.getItem('userid'));
        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);
        $form.find('div.fwformfield[data-datafield="UserId"] input').val(userId.webusersid);
        FwModule.loadForm(this.Module, $form);
        var newsort = Sortable.create($form.find('.sortable').get(0), {
            onEnd: function (evt) {
                $form.attr('data-modified', 'true');
                $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
            }
        });
        return $form;
    };
    DashboardSettings.prototype.saveForm = function ($form, closetab, navigationpath) {
        FwModule.saveForm(this.Module, $form, { closetab: closetab, navigationpath: 'home' });
    };
    return DashboardSettings;
}());
var DashboardSettingsController = new DashboardSettings();
//# sourceMappingURL=DashboardSettings.js.map