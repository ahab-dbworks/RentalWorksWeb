routes.push({ pattern: /^module\/refreshglhistory/, action: function (match: RegExpExecArray) { return RefreshGLHistoryController.getModuleScreen(); } });

class RefreshGLHistory {
    Module:  string = 'RefreshGLHistory';
    apiurl:  string = 'api/v1/gldistribution';
    caption: string = Constants.Modules.Utilities.children.RefreshGLHistory.caption;
    nav:     string = Constants.Modules.Utilities.children.RefreshGLHistory.nav;
    id:      string = Constants.Modules.Utilities.children.RefreshGLHistory.id;
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
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
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        //disables asterisk and save prompt
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');

        const today = FwFunc.getDate();
        FwFormField.setValueByDataField($form, 'ToDate', today);

        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    events($form) {
        $form
            .on('click', '.refresh', e => {
                const request: any = {};
                request.FromDate = FwFormField.getValueByDataField($form, 'FromDate');
                request.ToDate = FwFormField.getValueByDataField($form, 'ToDate');

                FwAppData.apiMethod(true, 'POST', `${this.apiurl}/refresh`, request, FwServices.defaultTimeout,
                    response => { },
                    ex => FwFunc.showError(ex), $form);
            });
    }
    //----------------------------------------------------------------------------------------------
}

var RefreshGLHistoryController = new RefreshGLHistory();