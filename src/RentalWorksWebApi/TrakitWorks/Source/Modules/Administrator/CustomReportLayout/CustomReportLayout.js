class CustomReportLayout {
    constructor() {
        this.Module = 'CustomReportLayout';
        this.apiurl = 'api/v1/customreportlayout';
        this.caption = Constants.Modules.Administrator.children.CustomReportLayout.caption;
        this.nav = Constants.Modules.Administrator.children.CustomReportLayout.nav;
        this.id = Constants.Modules.Administrator.children.CustomReportLayout.id;
    }
    getModuleScreen() {
        var screen, $browse;
        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};
        $browse = this.openBrowse();
        screen.load = function () {
            FwModule.openModuleTab($browse, 'Custom Report Layout', false, 'BROWSE', true);
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
            FwFormField.enable($form.find('[data-datafield="BaseReport"]'));
            $form.find('.userGrid, .groupGrid').hide();
            FwFormField.setValue($form, 'div[data-datafield="Active"]', true);
        }
        $form.off('change', '.fwformfield[data-enabled="true"][data-datafield!=""]:not(.find-field)');
        this.loadModules($form);
        this.events($form);
        return $form;
    }
    loadForm(uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="CustomReportLayoutId"] input').val(uniqueids.CustomReportLayoutId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    saveForm($form, parameters) {
        $form.find('#codeEditor').change();
        $form.find('[data-datafield="Html"]').addClass('reload');
        FwModule.saveForm(this.Module, $form, parameters);
    }
    afterSave($form) {
        FwFormField.disable($form.find('[data-datafield="BaseReport"]'));
        $form.attr('data-modified', 'false');
        $form.find('.btn[data-type="SaveMenuBarButton"]').addClass('disabled');
    }
    afterLoad($form) {
        let assignTo = FwFormField.getValueByDataField($form, 'AssignTo');
        switch (assignTo) {
            case 'GROUPS':
                $form.find('.groupGrid').show();
                $form.find('.userGrid').hide();
                const $groupGrid = $form.find('[data-name="CustomReportLayoutGroupGrid"]');
                FwBrowse.search($groupGrid);
                break;
            case 'USERS':
                $form.find('.groupGrid').hide();
                $form.find('.userGrid').show();
                const $userGrid = $form.find('[data-name="CustomReportLayoutUserGrid"]');
                FwBrowse.search($userGrid);
                break;
            case 'ALL':
                $form.find('.groupGrid').hide();
                $form.find('.userGrid').hide();
                break;
        }
        if (!$form.find('[data-datafield="Html"]').hasClass('reload')) {
            let html = $form.find('[data-datafield="Html"] textarea').val();
            if (typeof html !== 'undefined') {
                this.codeMirror.setValue(html);
            }
            else {
                this.codeMirror.setValue('');
            }
        }
        let reportName = FwFormField.getValueByDataField($form, 'BaseReport');
        this.addValidFields($form, reportName);
        this.renderTab($form, 'Designer');
        this.codeMirror.on('change', function (codeMirror, change) {
            $form.attr('data-modified', 'true');
            $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
        });
    }
    codeMirrorEvents($form) {
        let textArea = $form.find('#codeEditor').get(0);
        var codeMirror = CodeMirror.fromTextArea(textArea, {
            mode: "xml",
            lineNumbers: true,
            foldGutter: true,
            gutters: ["CodeMirror-linenumbers", "CodeMirror-foldgutter"]
        });
        this.codeMirror = codeMirror;
        $form.find('div.modules').on('change', e => {
            let $this = $form.find('[data-datafield="BaseReport"] option:selected');
            let modulehtml;
            const reportName = $this.val();
            FwAppData.apiMethod(true, 'GET', `api/v1/customreportlayout/template/${reportName}`, null, FwServices.defaultTimeout, response => {
                modulehtml = response.ReportTemplate;
                if (typeof modulehtml !== "undefined") {
                    codeMirror.setValue(modulehtml);
                }
                this.renderTab($form, 'Designer');
            }, ex => FwFunc.showError(ex), $form);
            this.addValidFields($form, reportName);
        });
        $form.find('#codeEditor').on('change', e => {
            codeMirror.save();
            let html = $form.find('textarea#codeEditor').val();
            FwFormField.setValueByDataField($form, 'Html', html);
        });
    }
    addValidFields($form, reportName) {
        const modulefields = $form.find('.modulefields');
        modulefields.empty();
        FwAppData.apiMethod(true, 'GET', `api/v1/${reportName}/emptyobject`, null, FwServices.defaultTimeout, response => {
            let customFields = response._Custom.map(obj => ({ fieldname: obj.FieldName, fieldtype: obj.FieldType }));
            let allValidFields = [];
            for (const key of Object.keys(response)) {
                if (key != 'DateStamp' && key != 'RecordTitle' && key != '_Custom' && key != '_Fields') {
                    if (Array.isArray(response[key])) {
                        allValidFields.push({
                            'Field': key,
                            'IsCustom': 'false',
                            'NestedItems': [response[key][0]]
                        });
                    }
                    else {
                        allValidFields.push({
                            'Field': key,
                            'IsCustom': 'false'
                        });
                    }
                }
            }
            for (let i = 0; i < customFields.length; i++) {
                allValidFields.push({
                    'Field': customFields[i].fieldname,
                    'IsCustom': 'true',
                    'FieldType': customFields[i].fieldtype.toLowerCase()
                });
            }
            $form.data('validdatafields', allValidFields.sort(compare));
            for (let i = 0; i < allValidFields.length; i++) {
                modulefields.append(`<div data-iscustomfield=${allValidFields[i].IsCustom}>${allValidFields[i].Field}</div>`);
                if (allValidFields[i].hasOwnProperty("NestedItems")) {
                    for (const key of Object.keys(allValidFields[i].NestedItems[0])) {
                        if (key != '_Custom') {
                            modulefields.append(`<div data-iscustomfield="false" data-isnested="true" data-parentfield="${allValidFields[i].Field}" style="text-indent:1em;">${key}</div>`);
                        }
                    }
                }
            }
        }, ex => FwFunc.showError(ex), $form);
        function compare(a, b) {
            if (a.Field < b.Field)
                return -1;
            if (a.Field > b.Field)
                return 1;
            return 0;
        }
    }
    loadModules($form) {
        let $moduleSelect, node, reports, allReports;
        node = FwApplicationTree.getNodeById(FwApplicationTree.tree, '0A5F2584-D239-480F-8312-7C2B552A30BA');
        reports = FwApplicationTree.getChildrenByType(node, 'ReportsModule');
        reports.map(obj => obj.properties);
        allReports = [];
        for (let i = 0; i < reports.length; i++) {
            let reportChildren = reports[i].properties;
            allReports.push({
                text: reportChildren.caption,
                value: reportChildren.controller.slice(0, -10)
            });
        }
        function compare(a, b) {
            if (a.text < b.text)
                return -1;
            if (a.text > b.text)
                return 1;
            return 0;
        }
        allReports.sort(compare);
        $moduleSelect = $form.find('.modules');
        FwFormField.loadItems($moduleSelect, allReports);
        this.codeMirrorEvents($form);
    }
    renderGrids($form) {
        let $customReportLayoutGroupGrid;
        let $customReportLayoutGroupGridControl;
        $customReportLayoutGroupGrid = $form.find('div[data-grid="CustomReportLayoutGroupGrid"]');
        $customReportLayoutGroupGridControl = FwBrowse.loadGridFromTemplate('CustomReportLayoutGroupGrid');
        $customReportLayoutGroupGrid.empty().append($customReportLayoutGroupGridControl);
        $customReportLayoutGroupGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                CustomReportLayoutId: FwFormField.getValueByDataField($form, 'CustomReportLayoutId')
            };
        });
        $customReportLayoutGroupGridControl.data('beforesave', function (request) {
            request.CustomReportLayoutId = FwFormField.getValueByDataField($form, 'CustomReportLayoutId');
        });
        FwBrowse.init($customReportLayoutGroupGridControl);
        FwBrowse.renderRuntimeHtml($customReportLayoutGroupGridControl);
        let $customReportLayoutUserGrid;
        let $customReportLayoutUserGridControl;
        $customReportLayoutUserGrid = $form.find('div[data-grid="CustomReportLayoutUserGrid"]');
        $customReportLayoutUserGridControl = FwBrowse.loadGridFromTemplate('CustomReportLayoutUserGrid');
        $customReportLayoutUserGrid.empty().append($customReportLayoutUserGridControl);
        $customReportLayoutUserGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                CustomReportLayoutId: FwFormField.getValueByDataField($form, 'CustomReportLayoutId')
            };
        });
        $customReportLayoutUserGridControl.data('beforesave', function (request) {
            request.CustomReportLayoutId = FwFormField.getValueByDataField($form, 'CustomReportLayoutId');
        });
        FwBrowse.init($customReportLayoutUserGridControl);
        FwBrowse.renderRuntimeHtml($customReportLayoutUserGridControl);
    }
    events($form) {
        $form.on('click', '[data-type="tab"][data-caption="HTML"]', e => {
            this.codeMirror.refresh();
        });
        $form.find('[data-datafield="AssignTo"]').on('change', e => {
            let assignTo = FwFormField.getValueByDataField($form, 'AssignTo');
            let $gridControl;
            switch (assignTo) {
                case 'GROUPS':
                    $form.find('.groupGrid').show();
                    $form.find('.userGrid').hide();
                    $gridControl = $form.find('[data-name="CustomReportLayoutGroupGrid"]');
                    FwBrowse.search($gridControl);
                    break;
                case 'USERS':
                    $form.find('.userGrid').show();
                    $form.find('.groupGrid').hide();
                    $gridControl = $form.find('[data-name="CustomReportLayoutUserGrid"]');
                    FwBrowse.search($gridControl);
                    break;
                case 'ALL':
                default:
                    $form.find('.userGrid').hide();
                    $form.find('.groupGrid').hide();
                    break;
            }
        });
        $form.on('click', '.modulefields div', e => {
            const $this = jQuery(e.currentTarget);
            let textToInject;
            if ($this.attr('data-isnested') != 'true') {
                textToInject = `{{${$this.text()}}}`;
            }
            else {
                textToInject = `{{${$this.attr('data-parentfield')}.${$this.text()}}}`;
            }
            const doc = this.codeMirror.getDoc();
            const cursor = doc.getCursor();
            doc.replaceRange(textToInject, cursor);
            $form.find('#codeEditor').change();
        });
    }
    renderTab($form, tabName) {
        $form.find('#codeEditor').change();
        function addDatafields() {
            const validFields = $form.data('validdatafields');
            if (typeof validFields === 'object') {
                let datafieldOptions = $form.find('#controlProperties .propval .datafields');
                for (let z = 0; z < datafieldOptions.length; z++) {
                    let field = jQuery(datafieldOptions[z]);
                    field.append(`<option value="" disabled>Select field</option>`);
                    for (let i = 0; i < validFields.length; i++) {
                        let $this = validFields[i];
                        field.append(`<option data-iscustomfield=${$this.IsCustom} value="${$this.Field}" data-type="${$this.FieldType}">${$this.Field}</option>`);
                    }
                    let value = jQuery(field).attr('value');
                    if (value) {
                        jQuery(field).find(`option[value="${value}"]`).prop('selected', true);
                    }
                    else {
                        jQuery(field).find(`option[disabled]`).prop('selected', true);
                    }
                    ;
                }
            }
        }
        ;
        addDatafields();
    }
}
;
var CustomReportLayoutController = new CustomReportLayout();
//# sourceMappingURL=CustomReportLayout.js.map