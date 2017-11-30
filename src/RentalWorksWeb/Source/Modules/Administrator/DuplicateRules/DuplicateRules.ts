declare var FwModule: any;
declare var FwBrowse: any;
declare var FwSettings: any;

class DuplicateRules {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'DuplicateRules';
        this.apiurl = 'api/v1/duplicaterule';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Duplicate Rules', false, 'BROWSE', true);
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
        var $form
            , $moduleSelect;

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            FwFormField.enable($form.find('.ifnew'))
        } else {
            FwFormField.disable($form.find('.ifnew'))
        }

        var node = FwApplicationTree.getNodeById(FwApplicationTree.tree, '0A5F2584-D239-480F-8312-7C2B552A30BA');
        var modules = FwApplicationTree.getChildrenByType(node, 'Module');
        var allModules = [];
        for (var i = 0; i < modules.length; i++) {
            var moduleNav = modules[i].properties.controller.slice(0, -10);
            var moduleCaption = modules[i].properties.caption;
            var moduleController = modules[i].properties.controller;
            var moduleUrl = window[moduleController].apiurl;
            allModules.push({ value: moduleNav, text: moduleCaption, apiurl: moduleUrl });
        };

        //Sort modules
        function compare(a, b) {
            if (a.text < b.text)
                return -1;
            if (a.text > b.text)
                return 1;
            return 0;
        }
        allModules.sort(compare);

        $moduleSelect = $form.find('.modules');
        FwFormField.loadItems($moduleSelect, allModules);

        this.events($form);

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="DuplicateRuleId"] input').val(uniqueids.DuplicateRuleId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    }

    renderGrids($form: any) {
        //var $duplicateRuleFieldGrid: any;
        //var $duplicateRuleFieldGridControl: any;

        //$duplicateRuleFieldGrid = $form.find('div[data-grid="DuplicateRuleFieldGrid"]');
        //$duplicateRuleFieldGridControl = jQuery(jQuery('#tmpl-grids-DuplicateRuleFieldGridBrowse').html());
        //$duplicateRuleFieldGrid.empty().append($duplicateRuleFieldGridControl);
        //$duplicateRuleFieldGridControl.data('ondatabind', function(request) {
        //    request.uniqueids = {
        //        DuplicateRuleId: $form.find('div.fwformfield[data-datafield="DuplicateRuleId"] input').val()
        //    };
        //})
        //$duplicateRuleFieldGridControl.data('beforesave', function(request) {
        //    request.DuplicateRuleId = FwFormField.getValueByDataField($form, 'DuplicateRuleId');
        //});
        //FwBrowse.init($duplicateRuleFieldGridControl);
        //FwBrowse.renderRuntimeHtml($duplicateRuleFieldGridControl);
    }

    events($form: JQuery): void {
        $form.find('div.modules').on("change", function () {
            var moduleName = jQuery(this).find(':selected').val();
            var request = {
                module: moduleName
            };
            var moduleUrl = jQuery(this).find(":selected").attr('data-apiurl');

            FwAppData.apiMethod(true, 'POST', moduleUrl + "/browse", request, FwServices.defaultTimeout, function onSuccess(response) {
                var columns = response.ColumnIndex;
                var fieldsHtml = [];
                var $fields = $form.find('.fields');
                for (var key in columns) {
                    fieldsHtml.push('<div data-control="FwFormField"');
                    fieldsHtml.push(' data-type="checkbox"');
                    fieldsHtml.push(' class="fwcontrol fwformfield"');
                    fieldsHtml.push(' data-caption="' + key + '"');
                    fieldsHtml.push(' data-datafield="' + key + '"');
                    fieldsHtml.push(' style="float:left;width:125px;"');
                    fieldsHtml.push('>');
                    fieldsHtml.push('</div>');
                }
                $fields.empty().append(fieldsHtml.join(''));

            });
        });
    }

    afterLoad($form: any) {
        //var $duplicateRuleFieldGrid: any;

        //$duplicateRuleFieldGrid = $form.find('[data-name="DuplicateRuleFieldGrid"]');
        //FwBrowse.search($duplicateRuleFieldGrid);
    }
}

(<any>window).DuplicateRulesController = new DuplicateRules();