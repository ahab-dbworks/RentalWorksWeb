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
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
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

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }

    openForm(mode: string) {
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
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

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="PoTypeId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    renderGrids($form: any) {
        //----------
        var $orderTypeCoverLetterGrid: any;
        var $orderTypeCoverLetterGridControl: any;

        $orderTypeCoverLetterGrid = $form.find('div[data-grid="OrderTypeCoverLetterGrid"]');
        $orderTypeCoverLetterGridControl = jQuery(jQuery('#tmpl-grids-OrderTypeCoverLetterGridBrowse').html());
        $orderTypeCoverLetterGrid.empty().append($orderTypeCoverLetterGridControl);
        $orderTypeCoverLetterGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderTypeId: $form.find('div.fwformfield[data-datafield="PoTypeId"] input').val()
            };
        })
        $orderTypeCoverLetterGridControl.data('beforesave', function (request) {
            request.OrderTypeId = FwFormField.getValueByDataField($form, 'PoTypeId');
        });
        FwBrowse.init($orderTypeCoverLetterGridControl);
        FwBrowse.renderRuntimeHtml($orderTypeCoverLetterGridControl);
        //----------
        var $orderTypeTermsAndConditionsGrid: any;
        var $orderTypeTermsAndConditionsGridControl: any;

        $orderTypeTermsAndConditionsGrid = $form.find('div[data-grid="OrderTypeTermsAndConditionsGrid"]');
        $orderTypeTermsAndConditionsGridControl = jQuery(jQuery('#tmpl-grids-OrderTypeTermsAndConditionsGridBrowse').html());
        $orderTypeTermsAndConditionsGrid.empty().append($orderTypeTermsAndConditionsGridControl);
        $orderTypeTermsAndConditionsGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderTypeId: $form.find('div.fwformfield[data-datafield="PoTypeId"] input').val()
            };
        })
        $orderTypeTermsAndConditionsGridControl.data('beforesave', function (request) {
            request.OrderTypeId = FwFormField.getValueByDataField($form, 'PoTypeId');
        });
        FwBrowse.init($orderTypeTermsAndConditionsGridControl);
        FwBrowse.renderRuntimeHtml($orderTypeTermsAndConditionsGridControl);
        //----------
        var $orderTypeActivityDatesGrid: any;
        var $orderTypeActivityDatesGridControl: any;

        $orderTypeActivityDatesGrid = $form.find('div[data-grid="OrderTypeActivityDatesGrid"]');
        $orderTypeActivityDatesGridControl = jQuery(jQuery('#tmpl-grids-OrderTypeActivityDatesGridBrowse').html());
        $orderTypeActivityDatesGrid.empty().append($orderTypeActivityDatesGridControl);
        $orderTypeActivityDatesGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderTypeId: $form.find('div.fwformfield[data-datafield="PoTypeId"] input').val()
            };
        })
        $orderTypeActivityDatesGridControl.data('beforesave', function (request) {
            request.OrderTypeId = FwFormField.getValueByDataField($form, 'PoTypeId');
        });
        FwBrowse.init($orderTypeActivityDatesGridControl);
        FwBrowse.renderRuntimeHtml($orderTypeActivityDatesGridControl);
    }

    afterLoad($form: any) {
        var $orderTypeCoverLetterGrid: any;

        $orderTypeCoverLetterGrid = $form.find('[data-name="OrderTypeCoverLetterGrid"]');
        FwBrowse.search($orderTypeCoverLetterGrid);

        var $orderTypeTermsAndConditionsGrid: any;

        $orderTypeTermsAndConditionsGrid = $form.find('[data-name="OrderTypeTermsAndConditionsGrid"]');
        FwBrowse.search($orderTypeTermsAndConditionsGrid);

        var $orderTypeActivityDatesGrid: any;
        $orderTypeActivityDatesGrid = $form.find('[data-name="OrderTypeActivityDatesGrid"]');
        FwBrowse.search($orderTypeActivityDatesGrid);

        $orderTypeActivityDatesGrid.find('.eventType').remove();
    }
}

var POTypeController = new POType();