var DuplicateRules = (function () {
    function DuplicateRules() {
        this.Module = 'DuplicateRules';
        this.apiurl = 'api/v1/duplicaterule';
    }
    DuplicateRules.prototype.getModuleScreen = function () {
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
    };
    DuplicateRules.prototype.openBrowse = function () {
        var $browse;
        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);
        FwBrowse.init($browse);
        return $browse;
    };
    DuplicateRules.prototype.openForm = function (mode) {
        var $form, $moduleSelect;
        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);
        if (mode === 'NEW') {
            FwFormField.enable($form.find('.ifnew'));
        }
        else {
            FwFormField.disable($form.find('.ifnew'));
        }
        var node = FwApplicationTree.getNodeById(FwApplicationTree.tree, '0A5F2584-D239-480F-8312-7C2B552A30BA');
        var modules = FwApplicationTree.getChildrenByType(node, 'Module');
        var allModules = [];
        for (var i = 0; i < modules.length; i++) {
            var moduleNav = modules[i].properties.controller.slice(0, -10);
            var moduleCaption = modules[i].properties.caption;
            allModules.push({ value: moduleNav, text: moduleCaption });
        }
        ;
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
        return $form;
    };
    DuplicateRules.prototype.loadForm = function (uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="DuplicateRuleId"] input').val(uniqueids.DuplicateRuleId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    };
    DuplicateRules.prototype.saveForm = function ($form, closetab, navigationpath) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    };
    DuplicateRules.prototype.renderGrids = function ($form) {
        var $duplicateRuleFieldGrid;
        var $duplicateRuleFieldGridControl;
        $duplicateRuleFieldGrid = $form.find('div[data-grid="DuplicateRuleFieldGrid"]');
        $duplicateRuleFieldGridControl = jQuery(jQuery('#tmpl-grids-DuplicateRuleFieldGridBrowse').html());
        $duplicateRuleFieldGrid.empty().append($duplicateRuleFieldGridControl);
        $duplicateRuleFieldGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                DuplicateRuleId: $form.find('div.fwformfield[data-datafield="DuplicateRuleId"] input').val()
            };
        });
        $duplicateRuleFieldGridControl.data('beforesave', function (request) {
            request.DuplicateRuleId = FwFormField.getValueByDataField($form, 'DuplicateRuleId');
        });
        FwBrowse.init($duplicateRuleFieldGridControl);
        FwBrowse.renderRuntimeHtml($duplicateRuleFieldGridControl);
    };
    DuplicateRules.prototype.afterLoad = function ($form) {
        var $duplicateRuleFieldGrid;
        $duplicateRuleFieldGrid = $form.find('[data-name="DuplicateRuleFieldGrid"]');
        FwBrowse.search($duplicateRuleFieldGrid);
    };
    return DuplicateRules;
}());
window.DuplicateRulesController = new DuplicateRules();
//# sourceMappingURL=DuplicateRules.js.map