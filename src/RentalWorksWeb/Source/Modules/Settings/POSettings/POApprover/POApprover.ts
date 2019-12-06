class POApprover {
    Module: string = 'POApprover';
    apiurl: string = 'api/v1/poapprover';
    caption: string = Constants.Modules.Settings.children.POSettings.children.POApprover.caption;
    nav: string = Constants.Modules.Settings.children.POSettings.children.POApprover.nav;
    id: string = Constants.Modules.Settings.children.POSettings.children.POApprover.id;
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: { datafield: string, search: string }) {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $browse = this.openBrowse();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        $form.find('[data-datafield="HasLimit"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('.limits'))
            }
            else {
                FwFormField.disable($form.find('.limits'))
            }
        });

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="PoApproverId"] input').val(uniqueids.PoApproverId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    afterLoad($form: any) {
        var $limit = $form.find('div.fwformfield[data-datafield="HasLimit"] input').prop('checked');

        if ($limit === true) {
            FwFormField.enable($form.find('.limits'));
        } else {
            FwFormField.disable($form.find('.limits'));
        }
    }
}

var POApproverController = new POApprover();