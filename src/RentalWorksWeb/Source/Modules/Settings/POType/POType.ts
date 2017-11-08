declare var FwModule: any;
declare var FwBrowse: any;

class POType {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'POType';
        this.apiurl = 'api/v1/potype';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'PO Type', false, 'BROWSE', true);
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

        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);
        FwBrowse.init($browse);

        return $browse;
    }

    openForm(mode: string) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);
       

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="PoTypeId"] input').val(uniqueids.PoTypeId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="PoTypeId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    renderGrids($form: any) {
        //----------
        var $coverLetterGrid: any;
        var $coverLetterGridControl: any;

        $coverLetterGrid = $form.find('div[data-grid="CoverLetterGrid"]');
        $coverLetterGridControl = jQuery(jQuery('#tmpl-grids-CoverLetterGridBrowse').html());
        $coverLetterGrid.empty().append($coverLetterGridControl);
        $coverLetterGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderTypeId: $form.find('div.fwformfield[data-datafield="PoTypeId"] input').val()
            };
        })
        $coverLetterGridControl.data('beforesave', function (request) {
            request.OrderTypeId = FwFormField.getValueByDataField($form, 'PoTypeId');
        });
        FwBrowse.init($coverLetterGridControl);
        FwBrowse.renderRuntimeHtml($coverLetterGridControl);
        //----------
        var $termsAndConditionsGrid: any;
        var $termsAndConditionsGridControl: any;

        $termsAndConditionsGrid = $form.find('div[data-grid="TermsAndConditionsGrid"]');
        $termsAndConditionsGridControl = jQuery(jQuery('#tmpl-grids-TermsAndConditionsGridBrowse').html());
        $termsAndConditionsGrid.empty().append($termsAndConditionsGridControl);
        $termsAndConditionsGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderTypeId: $form.find('div.fwformfield[data-datafield="PoTypeId"] input').val()
            };
        })
        $termsAndConditionsGridControl.data('beforesave', function (request) {
            request.OrderTypeId = FwFormField.getValueByDataField($form, 'PoTypeId');
        });
        FwBrowse.init($termsAndConditionsGridControl);
        FwBrowse.renderRuntimeHtml($termsAndConditionsGridControl);
        //----------
    }

    afterLoad($form: any) {
        var $coverLetterGrid: any;

        $coverLetterGrid = $form.find('[data-name="CoverLetterGrid"]');
        FwBrowse.search($coverLetterGrid);

        var $termsAndConditionsGrid: any;

        $termsAndConditionsGrid = $form.find('[data-name="TermsAndConditionsGrid"]');
        FwBrowse.search($termsAndConditionsGrid);
    }
}

(<any>window).POTypeController = new POType();