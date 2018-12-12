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

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        //FwModule.saveForm(this.Module, $form, { closetab: closetab, navigationpath: 'home' });
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        //$form.find('.defaultwidget').find('ol').sortable({
        //    connectWith: ".connectedSortable",
        //    remove: function (event, ui) {
        //        ui.item.clone().appendTo('#sortable2');
        //        $(this).sortable('cancel');
        //    }
        //}).disableSelection();
        //$form.find('.widget.order').find('ol').sortable({
        //    connectWith: ".connectedSortable"
        //}).disableSelection();
    }
    //----------------------------------------------------------------------------------------------
}
var DashboardSettingsController = new DashboardSettings();