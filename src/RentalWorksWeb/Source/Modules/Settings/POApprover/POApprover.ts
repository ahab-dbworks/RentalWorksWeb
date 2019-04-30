class POApprover {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'POApprover';
        this.apiurl = 'api/v1/poapprover';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'PO Approver', false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }

    openBrowse() {
        var $browse;

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }

    openForm(mode: string) {
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
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