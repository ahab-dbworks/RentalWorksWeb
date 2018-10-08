routes.push({ pattern: /^module\/webform$/, action: function (match) { return WebFormController.getModuleScreen(); } });
class WebForm {
    constructor() {
        this.Module = 'WebForm';
        this.apiurl = 'api/v1/webform';
    }
    getModuleScreen() {
        var screen, $browse;
        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};
        $browse = this.openBrowse();
        screen.load = function () {
            FwModule.openModuleTab($browse, 'Web Form', false, 'BROWSE', true);
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
    openForm(mode) {
        var $form;
        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        let userid = JSON.parse(sessionStorage.getItem('userid'));
        FwFormField.setValueByDataField($form, 'WebUserId', userid.webusersid);
        if (mode == 'NEW') {
            this.addCodeEditor($form);
            FwFormField.enable($form.find('[data-datafield="BaseForm"]'));
        }
        this.loadModules($form);
        return $form;
    }
    loadForm(uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="WebFormId"] input').val(uniqueids.WebFormId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    saveForm($form, parameters) {
        $form.find('#codeEditor').change();
        FwModule.saveForm(this.Module, $form, parameters);
    }
    afterSave($form) {
        FwFormField.disable($form.find('[data-datafield="BaseForm"]'));
        $form.attr('data-modified', 'false');
        $form.find('.btn[data-type="SaveMenuBarButton"]').addClass('disabled');
        if (FwFormField.getValueByDataField($form, 'Active') == true) {
            let type = $form.find('[data-datafield="BaseForm"] option:selected').attr('data-type');
            let baseform = FwFormField.getValueByDataField($form, 'BaseForm');
            let html = FwFormField.getValueByDataField($form, 'Html');
            switch (type) {
                case 'Grid':
                    jQuery(`#tmpl-grids-${baseform}`).html(html);
                    break;
                case 'Browse':
                case 'Form':
                    jQuery(`#tmpl-modules-${baseform}`).html(html);
                    break;
            }
        }
    }
    afterLoad($form) {
        this.addCodeEditor($form);
        $form.find('div.modules').change();
    }
    addCodeEditor($form) {
        let textArea = $form.find('#codeEditor');
        let html = $form.find('[data-datafield="Html"] textarea').val();
        var myCodeMirror = CodeMirror.fromTextArea(textArea.get(0), {
            mode: 'text/html',
            lineNumbers: true
        });
        myCodeMirror.setSize(1350, 850);
        $form.find('.CodeMirror').css('max-width', '1350px');
        let doc = myCodeMirror.getDoc();
        if (typeof html !== 'undefined') {
            myCodeMirror.setValue(html);
        }
        else {
            myCodeMirror.setValue(' ');
        }
        $form.find('div.modules').on('change', e => {
            let moduleName = jQuery(e.currentTarget).find(':selected').val();
            let type = jQuery(e.currentTarget).find('option:selected').attr('data-type');
            let controller = jQuery(e.currentTarget).find('option:selected').attr('data-controllername');
            let apiurl = jQuery(e.currentTarget).find('option:selected').attr('data-apiurl');
            const modulefields = $form.find('.modulefields');
            let modulehtml, request;
            let moduleNav = controller.slice(0, -10);
            request = {
                module: moduleNav,
                top: 1
            };
            modulefields.empty();
            switch (type) {
                case 'Browse':
                    modulehtml = jQuery(`#tmpl-modules-${moduleName}`).html();
                    if (apiurl !== "undefined") {
                        FwAppData.apiMethod(true, 'POST', `${apiurl}/browse`, request, FwServices.defaultTimeout, function onSuccess(response) {
                            let columnNames = response.Columns;
                            columnNames = columnNames.filter(obj => {
                                return obj.DataField !== 'DateStamp';
                            });
                            for (let i = 0; i < columnNames.length; i++) {
                                modulefields.append(`${columnNames[i].DataField}<br />`);
                            }
                        }, null, $form);
                    }
                    if (typeof modulehtml == 'undefined') {
                        modulehtml = window[controller].getBrowseTemplate();
                    }
                    break;
                case 'Form':
                    modulehtml = jQuery(`#tmpl-modules-${moduleName}`).html();
                    if (apiurl !== "undefined") {
                        FwAppData.apiMethod(true, 'POST', `${apiurl}/browse`, request, FwServices.defaultTimeout, function onSuccess(response) {
                            let columnNames = response.Columns;
                            columnNames = columnNames.filter(obj => {
                                return obj.DataField !== 'DateStamp';
                            });
                            for (let i = 0; i < columnNames.length; i++) {
                                modulefields.append(`${columnNames[i].DataField}<br />`);
                            }
                        }, null, $form);
                    }
                    if (typeof modulehtml == 'undefined') {
                        modulehtml = window[controller].getFormTemplate();
                    }
                    break;
                case 'Grid':
                    modulehtml = jQuery(`#tmpl-grids-${moduleName}`).html();
                    if (apiurl !== "undefined") {
                        FwAppData.apiMethod(true, 'POST', `${apiurl}/browse`, request, FwServices.defaultTimeout, function onSuccess(response) {
                            let columnNames = response.Columns;
                            columnNames = columnNames.filter(obj => {
                                return obj.DataField !== 'DateStamp';
                            });
                            for (let i = 0; i < columnNames.length; i++) {
                                modulefields.append(`${columnNames[i].DataField}<br />`);
                            }
                        }, null, $form);
                    }
                    break;
            }
            if (typeof modulehtml !== "undefined") {
                myCodeMirror.setValue(modulehtml);
            }
            else {
                myCodeMirror.setValue(`There is no ${type} available for this selection.`);
            }
            doc.markClean();
        });
        myCodeMirror.on('change', function (cm, change) {
            $form.attr('data-modified', 'true');
            $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
        });
        $form.find('#codeEditor').on('change', e => {
            myCodeMirror.save();
            let html = $form.find('textarea#codeEditor').val();
            FwFormField.setValueByDataField($form, 'Html', html);
        });
        $form.on('click', '[data-type="tab"][data-caption="Preview"]', e => {
            $form.find('#codeEditor').change();
            let type = $form.find('[data-datafield="BaseForm"] option:selected').attr('data-type');
            $form.find('#previewWebForm').empty();
            let html = FwFormField.getValueByDataField($form, 'Html');
            $form.find('#previewWebForm').append(html);
            switch (type) {
                case 'Browse':
                case 'Form':
                case 'Grid':
                    let $previewForm = $form.find('#previewWebForm');
                    let $fwcontrols = $previewForm.find('.fwcontrol');
                    FwControl.init($fwcontrols);
                    FwControl.renderRuntimeHtml($fwcontrols);
                    let $grids = $previewForm.find('[data-control="FwGrid"]');
                    for (let i = 0; i < $grids.length; i++) {
                        let $this = jQuery($grids[i]);
                        let gridName = $this.attr('data-grid');
                        let $gridControl = jQuery(jQuery(`#tmpl-grids-${gridName}Browse`).html());
                        $this.empty().append($gridControl);
                        FwBrowse.init($gridControl);
                        FwBrowse.renderRuntimeHtml($gridControl);
                    }
                    break;
            }
        });
    }
    loadModules($form) {
        let $moduleSelect, node, mainModules, settingsModules, modules, allModules;
        node = FwApplicationTree.getNodeById(FwApplicationTree.tree, '0A5F2584-D239-480F-8312-7C2B552A30BA');
        mainModules = FwApplicationTree.getChildrenByType(node, 'Module');
        settingsModules = FwApplicationTree.getChildrenByType(node, 'SettingsModule');
        modules = mainModules.concat(settingsModules);
        allModules = [];
        for (let i = 0; i < modules.length; i++) {
            let moduleChildren = modules[i].children;
            let browseNodePosition = moduleChildren.map(function (x) { return x.properties.nodetype; }).indexOf('Browse');
            let formNodePosition = moduleChildren.map(function (x) { return x.properties.nodetype; }).indexOf('Form');
            if (browseNodePosition != -1) {
                addModulesToList(modules[i], 'Browse');
            }
            if (formNodePosition != -1) {
                addModulesToList(modules[i], 'Form');
            }
            ;
        }
        const gridNode = FwApplicationTree.getNodeById(FwApplicationTree.tree, '43765919-4291-49DD-BE76-F69AA12B13E8');
        let gridModules = FwApplicationTree.getChildrenByType(gridNode, 'Grid');
        for (let i = 0; i < gridModules.length; i++) {
            addModulesToList(gridModules[i], 'Grid');
        }
        ;
        function addModulesToList(module, type) {
            let controller = module.properties.controller;
            let moduleNav = controller.slice(0, -10);
            let moduleCaption = module.properties.caption;
            let moduleUrl;
            if (typeof window[controller] !== 'undefined') {
                if (window[controller].hasOwnProperty('apiurl')) {
                    moduleUrl = window[controller].apiurl;
                }
            }
            switch (type) {
                case 'Browse':
                    allModules.push({ value: `${moduleNav}Browse`, text: `${moduleCaption} Browse`, type: type, controllername: module.properties.controller, apiurl: moduleUrl });
                    break;
                case 'Form':
                    allModules.push({ value: `${moduleNav}Form`, text: `${moduleCaption} Form`, type: type, controllername: module.properties.controller, apiurl: moduleUrl });
                    break;
                case 'Grid':
                    allModules.push({ value: `${moduleNav}Browse`, text: `${moduleCaption} Grid`, type: type, controllername: module.properties.controller, apiurl: moduleUrl });
                    break;
            }
        }
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
    }
}
;
var WebFormController = new WebForm();
//# sourceMappingURL=WebForm.js.map