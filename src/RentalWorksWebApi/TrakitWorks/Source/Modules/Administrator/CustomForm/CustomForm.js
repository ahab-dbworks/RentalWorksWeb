routes.push({ pattern: /^module\/customform$/, action: function (match) { return CustomFormController.getModuleScreen(); } });
class CustomForm {
    constructor() {
        this.Module = 'CustomForm';
        this.apiurl = 'api/v1/customform';
        this.caption = Constants.Modules.Administrator.children.CustomForm.caption;
        this.nav = Constants.Modules.Administrator.children.CustomForm.nav;
        this.id = Constants.Modules.Administrator.children.CustomForm.id;
    }
    getModuleScreen() {
        const screen = {};
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
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        return $browse;
    }
    openForm(mode) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        let userid = JSON.parse(sessionStorage.getItem('userid'));
        FwFormField.setValueByDataField($form, 'WebUserId', userid.webusersid);
        if (mode == 'NEW') {
            FwFormField.enable($form.find('[data-datafield="BaseForm"]'));
            $form.find('.userGrid, .groupGrid').hide();
            FwFormField.setValue($form, 'div[data-datafield="Active"]', true);
        }
        $form.off('change', '.fwformfield[data-enabled="true"][data-datafield!=""]:not(.find-field)');
        this.loadModules($form);
        this.events($form);
        return $form;
    }
    loadForm(uniqueids) {
        let $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="CustomFormId"] input').val(uniqueids.CustomFormId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    saveForm($form, parameters) {
        $form.find('#codeEditor').change();
        const $customForm = $form.find(`#designerContent`);
        const $fields = $customForm.find('.fwformfield');
        let hasDuplicates = false;
        $fields.each(function (i, e) {
            const $fwFormField = jQuery(e);
            const dataField = $fwFormField.attr('data-datafield');
            if (dataField != "") {
                const $fieldFound = $customForm.find(`[data-datafield="${dataField}"][data-enabled="true"]`);
                if ($fieldFound.length > 1) {
                    $fieldFound.addClass('error');
                    hasDuplicates = true;
                    FwNotification.renderNotification('ERROR', 'Only one duplicate field can be active on a form.  Set the data-enabled property to false on duplicates.');
                    return false;
                }
                else {
                    $customForm.find(`[data-datafield="${dataField}"]`).removeClass('error');
                }
            }
        });
        $form.find('[data-datafield="Html"]').addClass('reload');
        if (!hasDuplicates)
            FwModule.saveForm(this.Module, $form, parameters);
    }
    afterSave($form) {
        FwFormField.disable($form.find('[data-datafield="BaseForm"]'));
        $form.attr('data-modified', 'false');
        $form.find('.btn[data-type="SaveMenuBarButton"]').addClass('disabled');
    }
    afterLoad($form) {
        let assignTo = FwFormField.getValueByDataField($form, 'AssignTo');
        switch (assignTo) {
            case 'GROUPS':
                $form.find('.groupGrid').show();
                $form.find('.userGrid').hide();
                const $groupGrid = $form.find('[data-name="CustomFormGroupGrid"]');
                FwBrowse.search($groupGrid);
                break;
            case 'USERS':
                $form.find('.groupGrid').hide();
                $form.find('.userGrid').show();
                const $userGrid = $form.find('[data-name="CustomFormUserGrid"]');
                FwBrowse.search($userGrid);
                break;
            case 'ALL':
                $form.find('.groupGrid').hide();
                $form.find('.userGrid').hide();
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
        let controller = $form.find('[data-datafield="BaseForm"] option:selected').attr('data-controllername');
        this.addValidFields($form, controller);
        this.renderTab($form, 'Designer');
        this.codeMirror.on('change', function (codeMirror, change) {
            $form.attr('data-modified', 'true');
            $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
        });
    }
    codeMirrorEvents($form) {
        let textArea = $form.find('#codeEditor').get(0);
        var codeMirror = CodeMirror.fromTextArea(textArea, {
            mode: 'xml',
            lineNumbers: true,
            foldGutter: true,
            gutters: ["CodeMirror-linenumbers", "CodeMirror-foldgutter"]
        });
        this.codeMirror = codeMirror;
        this.doc = codeMirror.getDoc();
        $form.find('div.modules').on('change', e => {
            let $this = $form.find('[data-datafield="BaseForm"] option:selected');
            let moduleName = $this.val();
            let type = $this.attr('data-type');
            let controller = $this.attr('data-controllername');
            let modulehtml;
            switch (type) {
                case 'Browse':
                    typeof window[controller].getBrowseTemplate == "function" ? modulehtml = window[controller].getBrowseTemplate() : modulehtml = jQuery(`#tmpl-modules-${moduleName}`).html();
                    break;
                case 'Form':
                    typeof window[controller].getFormTemplate == "function" ? modulehtml = window[controller].getFormTemplate() : modulehtml = jQuery(`#tmpl-modules-${moduleName}`).html();
                    break;
                case 'Grid':
                    modulehtml = jQuery(`#tmpl-grids-${moduleName}`).html();
                    break;
            }
            if (typeof modulehtml !== "undefined") {
                codeMirror.setValue(modulehtml);
            }
            else {
                codeMirror.setValue(`There is no ${type} available for this selection.`);
            }
            this.addValidFields($form, controller);
            this.renderTab($form, 'Designer');
        });
        $form.find('#codeEditor').on('change', e => {
            codeMirror.save();
            let html = $form.find('textarea#codeEditor').val();
            FwFormField.setValueByDataField($form, 'Html', html);
        });
    }
    addValidFields($form, controller) {
        let self = this;
        const modulefields = $form.find('.modulefields');
        let moduleType = $form.find('[data-datafield="BaseForm"] option:selected').attr('data-type');
        let apiurl = $form.find('[data-datafield="BaseForm"] option:selected').attr('data-apiurl');
        modulefields.empty();
        switch (moduleType) {
            case 'Grid':
            case 'Browse':
                if (apiurl !== "undefined") {
                    FwAppData.apiMethod(true, 'GET', `${apiurl}/emptyobject`, null, FwServices.defaultTimeout, function onSuccess(response) {
                        let columnNames = Object.keys(response);
                        let customFields = response._Custom.map(obj => ({ fieldname: obj.FieldName, fieldtype: obj.FieldType }));
                        let allValidFields = [];
                        for (let i = 0; i < columnNames.length; i++) {
                            if (columnNames[i] != 'DateStamp' && columnNames[i] != 'RecordTitle' && columnNames[i] != '_Custom' && columnNames[i] != '_Fields') {
                                allValidFields.push({
                                    'Field': columnNames[i],
                                    'IsCustom': 'false'
                                });
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
                            modulefields.append(`
                                <div data-iscustomfield=${allValidFields[i].IsCustom}>${allValidFields[i].Field}</div>
                                `);
                        }
                    }, null, $form);
                }
                break;
            case 'Form':
                FwAppData.apiMethod(true, 'GET', `${apiurl}/emptyobject`, null, FwServices.defaultTimeout, function onSuccess(response) {
                    let columnNames = Object.keys(response);
                    let customFields = response._Custom.map(obj => ({ fieldname: obj.FieldName, fieldtype: obj.FieldType }));
                    let allValidFields = [];
                    for (let i = 0; i < columnNames.length; i++) {
                        if (columnNames[i] != 'DateStamp' && columnNames[i] != 'RecordTitle' && columnNames[i] != '_Custom' && columnNames[i] != '_Fields') {
                            allValidFields.push({
                                'Field': columnNames[i],
                                'IsCustom': 'false'
                            });
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
                        modulefields.append(`
                                <div data-iscustomfield=${allValidFields[i].IsCustom}>${allValidFields[i].Field}</div>
                                `);
                    }
                }, null, $form);
                break;
        }
        function compare(a, b) {
            if (a.Field < b.Field)
                return -1;
            if (a.Field > b.Field)
                return 1;
            return 0;
        }
    }
    addButtonMenu($form) {
        let $buttonmenu = $form.find('.addColumn[data-type="btnmenu"]');
        let $addContainer = FwMenu.generateButtonMenuOption('ADD NEW CONTAINER'), $addTab = FwMenu.generateButtonMenuOption('ADD NEW TAB');
        let menuOptions = [];
        menuOptions.push($addContainer, $addTab);
        FwMenu.addButtonMenuOptions($buttonmenu, menuOptions);
    }
    loadModules($form) {
        const modules = FwApplicationTree.getAllModules(false, false, (modules, moduleCaption, moduleName, category, currentNode, nodeModule, hasView, hasNew, hasEdit, moduleController) => {
            if (moduleController.hasOwnProperty('apiurl')) {
                modules.push({ value: `${moduleName}Browse`, text: `${moduleCaption} Browse`, type: 'Browse', controllername: moduleName + 'Controller', apiurl: moduleController.apiurl });
                if (hasView || hasNew || hasEdit) {
                    modules.push({ value: `${moduleName}Form`, text: `${moduleCaption} Form`, type: 'Form', controllername: moduleName + 'Controller', apiurl: moduleController.apiurl });
                }
            }
        });
        const grids = FwApplicationTree.getAllGrids(false, (modules, moduleCaption, moduleName, category, currentNode, nodeModule, hasNew, hasEdit, moduleController) => {
            if (moduleController.hasOwnProperty('apiurl')) {
                modules.push({ value: `${moduleName}Browse`, text: `${moduleCaption} Grid`, type: 'Grid', controllername: moduleName + 'Controller', apiurl: moduleController.apiurl });
            }
        });
        const allModules = modules.concat(grids);
        FwApplicationTree.sortModules(allModules);
        let $moduleSelect = $form.find('.modules');
        FwFormField.loadItems($moduleSelect, allModules);
        this.codeMirrorEvents($form);
    }
    renderGrids($form) {
        FwBrowse.renderGrid({
            nameGrid: 'CustomFormGroupGrid',
            gridSecurityId: '11txpzVKVGi2',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request) => {
                request.uniqueids = {
                    CustomFormId: FwFormField.getValueByDataField($form, 'CustomFormId')
                };
            },
            beforeSave: (request) => {
                request.CustomFormId = FwFormField.getValueByDataField($form, 'CustomFormId');
            }
        });
        FwBrowse.renderGrid({
            nameGrid: 'CustomFormUserGrid',
            gridSecurityId: 'nHNdXDBX6m6cp',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request) => {
                request.uniqueids = {
                    CustomFormId: FwFormField.getValueByDataField($form, 'CustomFormId')
                };
            },
            beforeSave: (request) => {
                request.CustomFormId = FwFormField.getValueByDataField($form, 'CustomFormId');
            }
        });
    }
    events($form) {
        $form.on('click', '[data-type="tab"][data-caption="Preview"]', e => {
            this.renderTab($form, 'Preview');
        });
        $form.on('click', '[data-type="tab"][data-caption="Designer"]', e => {
            this.renderTab($form, 'Designer');
        });
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
                    $gridControl = $form.find('[data-name="CustomFormGroupGrid"]');
                    FwBrowse.search($gridControl);
                    break;
                case 'USERS':
                    $form.find('.userGrid').show();
                    $form.find('.groupGrid').hide();
                    $gridControl = $form.find('[data-name="CustomFormUserGrid"]');
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
            const doc = this.codeMirror.getDoc();
            const cursor = doc.getCursor();
            doc.replaceRange($this.text(), cursor);
            $form.find('#codeEditor').change();
        });
    }
    renderTab($form, tabName) {
        let renderFormHere;
        let self = this;
        let type = $form.find('[data-datafield="BaseForm"] option:selected').attr('data-type');
        $form.find('#codeEditor').change();
        tabName === 'Designer' ? renderFormHere = 'designerContent' : renderFormHere = 'previewWebForm';
        let html = FwFormField.getValueByDataField($form, 'Html');
        $form.find(`#${renderFormHere}`).empty().append(html);
        let $customForm = $form.find(`#${renderFormHere}`);
        let $customFormClone;
        if (tabName == 'Designer') {
            let $divs = $customForm.find('div');
            for (let i = 0; i < $divs.length; i++) {
                let div = jQuery($divs[i]);
                div.attr('data-index', i);
                if (type === 'Grid' || type === 'Browse') {
                    if (div.hasClass('column')) {
                        let isVisible = div.attr('data-visible');
                        let width = div.attr('data-width');
                        div.find('.field').attr({ 'data-visible': isVisible, 'data-width': width });
                    }
                }
                if ((div.hasClass('flexrow') || div.hasClass('flexcolumn')) && div.children().length === 0) {
                    div.addClass('emptyContainer');
                }
            }
            $customFormClone = $customForm.get(0).cloneNode(true);
        }
        let $fwcontrols = $customForm.find('.fwcontrol');
        if (type === 'Form' || type === 'Browse') {
            FwControl.init($fwcontrols);
        }
        FwControl.renderRuntimeHtml($fwcontrols);
        let $grids = $customForm.find('[data-control="FwGrid"]');
        for (let i = 0; i < $grids.length; i++) {
            let $this = jQuery($grids[i]);
            let gridName = $this.attr('data-grid');
            let $gridControl = FwBrowse.loadGridFromTemplate(gridName);
            $this.empty().append($gridControl);
            FwBrowse.init($gridControl);
            FwBrowse.renderRuntimeHtml($gridControl);
        }
        function disableControls() {
            FwFormField.disable($customForm.find(`[data-type="validation"], [data-control="FwAppImage"]`));
            $customForm.find('[data-type="Browse"], [data-type="Grid"]').find('.pager').hide();
            $customForm.find('[data-type="Browse"] tbody, [data-type="Browse"] tfoot, [data-type="Grid"] tbody, [data-type="Grid"] tfoot').hide();
            FwFormField.disable($customForm.find('[data-type="Browse"], [data-type="Grid"]'));
            $customForm.find('tr.fieldnames .column >, .submenu-btn').off('click');
            $customForm.find('[data-control="FwSchedulerDetailed"]').unbind('onactivatetab');
            $customForm.find('[data-control="FwScheduler"]').unbind('onactivatetab');
            if (type === 'Form') {
                const $toggleButtons = $customForm.find('[data-type="togglebuttons"]');
                FwFormField.disable($toggleButtons);
                $toggleButtons.text('[Toggle Buttons]');
            }
        }
        disableControls();
        if (tabName == 'Designer') {
            let originalHtml;
            let controlType;
            $form.find('#controlProperties')
                .empty();
            $customForm.find('.tabpages .formpage').css('overflow', 'auto');
            function showHiddenColumns($control) {
                let hiddenColumns = $control.find('td[data-visible="false"]');
                for (let i = 0; i < hiddenColumns.length; i++) {
                    let self = jQuery(hiddenColumns[i]);
                    let caption = self.find('.caption')[0].textContent;
                    if (caption !== 'undefined') {
                        self.find('.caption')[0].textContent = `${caption} [Hidden]`;
                    }
                    else {
                        if (type === 'Grid') {
                            let datafield = self.find('.field').attr('data-datafield');
                            self.find('.caption')[0].textContent = `${datafield} [Hidden]`;
                        }
                        else if (type === 'Browse') {
                            let datafield = self.find('.field').attr('data-browsedatafield');
                            self.find('.caption')[0].textContent = `${datafield} [Hidden]`;
                        }
                    }
                    self.find('.fieldcaption').css(`background-color`, `#f9f9f9`);
                    self.css('display', 'table-cell');
                    self.find('.caption').css('color', 'red');
                }
            }
            if (type === 'Grid' || type === 'Browse') {
                showHiddenColumns($customForm);
                $form.find('.addColumn[data-type="button"]')
                    .css('margin-left', '27%')
                    .show();
                $form.find('.addColumn[data-type="btnmenu"]').hide();
            }
            else if (type === 'Form') {
                $form.find('.addColumn[data-type="btnmenu"]')
                    .css({ 'display': 'flex', 'margin-left': '27%' })
                    .show();
                $form.find('.addColumn[data-type="button"]').hide();
                $form.find('.addColumn .btnmenuoption:contains("ADD NEW CONTAINER")').addClass('addNewContainer');
                $form.find('.addColumn .btnmenuoption:contains("ADD NEW TAB")').addClass('addNewTab');
            }
            ;
            function updateHtml() {
                let $modifiedClone = $customFormClone.cloneNode(true);
                jQuery($modifiedClone).find('div').removeAttr('data-index');
                jQuery($modifiedClone).find('.emptyContainer').removeClass('emptyContainer');
                FwFormField.setValueByDataField($form, 'Html', $modifiedClone.innerHTML);
                self.codeMirror.setValue($modifiedClone.innerHTML);
            }
            ;
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
            function addValueOptions() {
                let addOptionsHere = $form.find('#controlProperties .propval .valueOptions');
                for (let z = 0; z < addOptionsHere.length; z++) {
                    let $this = jQuery(addOptionsHere[z]);
                    let fieldName = jQuery(addOptionsHere[z]).parents('.propval').siblings('.propname').text();
                    let valueOptions = self.getValueOptions(fieldName);
                    $this.append(`<option value="" disabled>Select value</option>`);
                    for (let i = 0; i < valueOptions.length; i++) {
                        $this.append(`<option value="${valueOptions[i]}">${valueOptions[i]}</option>`);
                    }
                    let value = jQuery($this).attr('value');
                    if (value) {
                        jQuery($this).find(`option[value=${value}]`).prop('selected', true);
                    }
                    else {
                        jQuery($this).find(`option[disabled]`).prop('selected', true);
                    }
                    ;
                }
            }
            let propertyContainerHtml = `<div class="propertyContainer" style="border: 1px solid #bbbbbb; word-break: break-word;">
                     <div style="text-indent:5px;">
                         <div style="font-weight:bold; background-color:#dcdcdc; width:50%; float:left;">Name</div>
                         <div style="font-weight:bold; background-color:#dcdcdc; width:50%; float:left;">Value</div>
                     </div>
                        `;
            let addPropertiesHtml = `   <div class="addproperties" style="width:100%; display:flex;">
                        <div class="addpropname" style="padding:3px; border:.5px solid #efefef; width:50%; float:left; font-size:.9em;"><input placeholder="Add new property"></div>
                        <div class="addpropval" style="padding:3px; border:.5px solid #efefef; width:50%; float:left; font-size:.9em;"><input placeholder="Add value"></div>
                    </div>
                 </div>`;
            let deleteComponentHtml = '<div class="fwformcontrol deleteObject" data-type="button" style="margin-left:27%; margin-top:15px;">Delete Component</div>';
            let lastIndex = Number(jQuery($customFormClone).find('div:last').attr('data-index'));
            let $draggableElements;
            if (type === 'Grid' || type === 'Browse') {
                let indexDrag;
                let indexDrop;
                let $elementDragged;
                let $preview;
                let domIndexDrag;
                let domIndexDrop;
                $draggableElements = $customForm.find('tr.fieldnames td.column:not(.tdselectrow):not(.browsecontextmenucell)');
                $draggableElements.attr('draggable', 'true');
                $customForm
                    .off('dragstart', 'td.column:not(.tdselectrow):not(.browsecontextmenucell)')
                    .on('dragstart', 'td.column:not(.tdselectrow):not(.browsecontextmenucell)', e => {
                    let $this = jQuery(e.currentTarget);
                    e.originalEvent.dataTransfer.effectAllowed = "move";
                    $draggableElements = $customForm.find('tr.fieldnames td.column:not(.tdselectrow):not(.browsecontextmenucell)');
                    indexDrag = $this.find('.field').attr('data-index');
                    $elementDragged = $draggableElements
                        .find('.field')
                        .filter(function () {
                        return jQuery(this).attr("data-index") === indexDrag;
                    });
                    domIndexDrag = $this.index();
                })
                    .off('dragenter', 'td.column:not(.tdselectrow):not(.browsecontextmenucell)')
                    .on('dragenter', 'td.column:not(.tdselectrow):not(.browsecontextmenucell)', e => {
                    let $this = jQuery(e.currentTarget);
                    domIndexDrop = $this.index();
                    indexDrop = $this.find('.field').attr('data-index');
                    $customForm.find('td.placeholder').remove();
                    if (domIndexDrag !== domIndexDrop) {
                        $preview = jQuery(`<td class="placeholder" style="min-width:100px; min-height:30px; line-height:50px; vertical-align:middle; font-weight:bold; text-align:center; flex:1 1 50px; border: 2px dashed gray">Drop here</td>`);
                        if (domIndexDrag > domIndexDrop) {
                            $preview.insertBefore($this);
                        }
                        else if (domIndexDrag < domIndexDrop) {
                            $preview.insertAfter($this);
                        }
                    }
                })
                    .off('dragover', 'td.placeholder')
                    .on('dragover', 'td.placeholder', e => {
                    e.preventDefault();
                    e.stopPropagation();
                    e.originalEvent.dataTransfer.dropEffect = "move";
                })
                    .off('drop', 'td.placeholder')
                    .on('drop', 'td.placeholder', e => {
                    let $this = jQuery(e.currentTarget);
                    let firstColumn = jQuery($customFormClone).find(`[data-index="${indexDrag}"]`).parent();
                    let secondColumn = jQuery($customFormClone).find(`[data-index="${indexDrop}"]`).parent();
                    $this.replaceWith($elementDragged.parent());
                    if (domIndexDrag < domIndexDrop) {
                        firstColumn.insertAfter(secondColumn);
                    }
                    else if (domIndexDrag > domIndexDrop) {
                        firstColumn.insertBefore(secondColumn);
                    }
                    updateHtml();
                })
                    .off('drop', 'td.column:not(.tdselectrow):not(.browsecontextmenucell)')
                    .on('drop', 'td.column:not(.tdselectrow):not(.browsecontextmenucell)', e => {
                    $customForm.find('td.placeholder').remove();
                })
                    .off('dragend')
                    .on('dragend', e => {
                    $customForm.find('td.placeholder').remove();
                });
            }
            else if (type === 'Form') {
                let indexDrag;
                let indexDrop;
                let $elementDragged;
                let $preview;
                let $parent;
                $draggableElements = $customForm.find('div.fwformfield, div.flexrow, div.flexcolumn, div[data-type="tab"]');
                $draggableElements.attr('draggable', 'true');
                let flexRows = $customForm.find('div.flexrow');
                for (let t = 0; t < flexRows.length; t++) {
                    let $thisContainer = jQuery(flexRows[t]);
                    if ($thisContainer.children().length === 0) {
                        $thisContainer.addClass('emptyContainer');
                    }
                }
                $customForm
                    .off('dragstart', 'div.fwformfield, div.flexrow, div.flexcolumn')
                    .on('dragstart', 'div.fwformfield, div.flexrow, div.flexcolumn', e => {
                    e.stopPropagation();
                    e.originalEvent.dataTransfer.effectAllowed = "move";
                    let $this = jQuery(e.currentTarget);
                    indexDrag = $this.attr('data-index');
                    $elementDragged = $draggableElements
                        .filter(function () {
                        return jQuery(this).attr("data-index") === indexDrag;
                    });
                    $parent = $elementDragged.parent();
                })
                    .off('dragover')
                    .on('dragover', e => {
                    e.preventDefault();
                    e.originalEvent.dataTransfer.dropEffect = "none";
                })
                    .off('dragover', 'div.fwformfield, div.flexrow, div.flexcolumn')
                    .on('dragover', 'div.fwformfield, div.flexrow, div.flexcolumn', e => {
                    e.preventDefault();
                    e.stopPropagation();
                    if ($elementDragged.attr('data-type') !== "tab") {
                        let $this = jQuery(e.currentTarget);
                        indexDrop = $this.attr('data-index');
                        $customForm.find('div.placeholder').remove();
                        $preview = jQuery(`<div class="placeholder" style="min-height:50px; line-height:50px; vertical-align:middle; font-weight:bold; text-align:center; flex:1 1 100px; border: 2px dashed #4caf50">Drop here</div>`);
                        let offset = $this.offset();
                        let halfElementWidth = e.currentTarget.offsetWidth / 2;
                        let x = e.pageX - offset.left;
                        if (indexDrag !== indexDrop) {
                            if (x < halfElementWidth) {
                                $preview.insertBefore($this);
                                $preview.addClass('addedBefore');
                            }
                            else if (x > halfElementWidth) {
                                $preview.insertAfter($this);
                                $preview.addClass('addedAfter');
                            }
                        }
                    }
                })
                    .off('dragover', 'div.placeholder')
                    .on('dragover', 'div.placeholder', e => {
                    e.preventDefault();
                    e.stopPropagation();
                    e.originalEvent.dataTransfer.dropEffect = "move";
                })
                    .off('dragover', '[data-type="tab"]')
                    .on('dragover', '[data-type="tab"]', e => {
                    e.originalEvent.dataTransfer.dropEffect = "none";
                    if ($elementDragged.attr('data-type') === "tab") {
                        let $this = jQuery(e.currentTarget);
                        indexDrop = $this.attr('data-index');
                        $customForm.find('div.placeholder').remove();
                        $preview = jQuery(`<div class="placeholder" style="min-height:40px; line-height:40px; vertical-align:middle; font-weight:bold; text-align:center; flex:1 1 100px; border: 2px dashed #4caf50">Drop here</div>`);
                        let offset = $this.offset();
                        let halfElementHeight = e.currentTarget.offsetHeight / 2;
                        let y = e.pageY - offset.top;
                        if (indexDrag !== indexDrop) {
                            if (y < halfElementHeight) {
                                $preview.insertBefore($this);
                                $preview.addClass('addedBefore');
                            }
                            else if (y > halfElementHeight) {
                                $preview.insertAfter($this);
                                $preview.addClass('addedAfter');
                            }
                        }
                    }
                    else {
                        let $displayedTab = $customForm.find('.active[data-type="tab"]');
                        let displayedTabId = $displayedTab.attr('data-tabpageid');
                        let tabToDisplayId = e.currentTarget.getAttribute('data-tabpageid');
                        if (displayedTabId !== tabToDisplayId) {
                            $displayedTab.removeClass('active').addClass('inactive');
                            $customForm.find(`#${displayedTabId}`).removeClass('active').addClass('inactive').hide();
                            jQuery(e.currentTarget).removeClass('inactive').addClass('active');
                            $customForm.find(`#${tabToDisplayId}`).removeClass('inactive').addClass('active').show();
                        }
                    }
                })
                    .off('dragover', 'div.emptyContainer')
                    .on('dragover', 'div.emptyContainer', e => {
                    let $this = jQuery(e.currentTarget);
                    $preview = jQuery(`<div class="placeholder" style="min-height:50px; line-height:50px; vertical-align:middle; font-weight:bold; text-align:center; flex:1 1 100px; border: 2px dashed #4caf50">Drop here</div>`);
                    if ($this.children().length === 0) {
                        $customForm.find('div.placeholder').remove();
                        $this.append($preview);
                        indexDrop = $this.attr('data-index');
                    }
                })
                    .off('drop', 'div.placeholder')
                    .on('drop', 'div.placeholder', e => {
                    let $this = jQuery(e.currentTarget);
                    let firstColumn = jQuery($customFormClone).find(`[data-index="${indexDrag}"]`);
                    let secondColumn = jQuery($customFormClone).find(`[data-index="${indexDrop}"]`);
                    if ($this.parent().hasClass('emptyContainer')) {
                        secondColumn.append(firstColumn);
                        $this.parent().removeClass('emptyContainer');
                    }
                    else {
                        if ($this.hasClass('addedBefore')) {
                            firstColumn.insertBefore(secondColumn);
                        }
                        else if ($this.hasClass('addedAfter')) {
                            firstColumn.insertAfter(secondColumn);
                        }
                    }
                    ;
                    $this.replaceWith($elementDragged);
                    if ($elementDragged.attr('data-type') !== "tab") {
                        if ($parent.children().length === 0) {
                            $parent.addClass('emptyContainer');
                        }
                    }
                    updateHtml();
                })
                    .off('drop', 'div.fwformfield')
                    .on('drop', 'div.fwformfield', e => {
                    $customForm.find('div.placeholder').remove();
                })
                    .off('dragend')
                    .on('dragend', e => {
                    $customForm.find('div.placeholder').remove();
                })
                    .off('dragstart', 'div[data-type="tab"]')
                    .on('dragstart', 'div[data-type="tab"]', e => {
                    e.stopPropagation();
                    let $this = jQuery(e.currentTarget);
                    indexDrag = $this.attr('data-index');
                    $elementDragged = $draggableElements
                        .filter(function () {
                        return jQuery(this).attr("data-index") === indexDrag;
                    });
                });
            }
            let $page = jQuery(document);
            $page
                .off('dragover')
                .on('dragover', e => {
                e.originalEvent.dataTransfer.dropEffect = "none";
            });
            $customForm
                .off('click')
                .on('click', '[data-control="FwGrid"], [data-type="Browse"] thead tr.fieldnames .column >, [data-type="Grid"] thead tr.fieldnames .column >, [data-control="FwContainer"], [data-control="FwFormField"], div.flexrow, div.flexcolumn, div[data-type="tab"]', e => {
                e.stopPropagation();
                originalHtml = e.currentTarget;
                controlType = jQuery(originalHtml).attr('data-control');
                let properties = e.currentTarget.attributes;
                let html = [];
                html.push(propertyContainerHtml);
                for (let i = 0; i < properties.length; i++) {
                    let value = properties[i].value;
                    let name = properties[i].name;
                    switch (name) {
                        case "data-originalvalue":
                        case "data-index":
                        case "data-rendermode":
                        case "data-version":
                        case "draggable":
                        case "data-noduplicate":
                        case "data-formdatafield":
                        case "data-cssclass":
                        case "data-mode":
                        case "data-tabtype":
                        case "data-customfield":
                            continue;
                        case "data-datafield":
                        case "data-browsedatafield":
                        case "data-displayfield":
                        case "data-browsedisplayfield":
                            html.push(`<div class="properties">
                                      <div class="propname">${name === "" ? "&#160;" : name}</div>
                                      <div class="propval"><select style="width:92%" class="datafields" value="${value}"></select></div>
                                   </div>
                                  `);
                            break;
                        case "data-browsedatatype":
                        case "data-formdatatype":
                        case "data-datatype":
                        case "data-sort":
                        case "data-visible":
                        case "data-formreadonly":
                        case "data-isuniqueid":
                        case "data-type":
                        case "data-formrequired":
                        case "data-required":
                        case "data-enabled":
                            html.push(`<div class="properties">
                                      <div class="propname">${name === "" ? "&#160;" : name}</div>
                                      <div class="propval"><select style="width:92%" class="valueOptions" value="${value}"></select></div>
                                   </div>
                                  `);
                            break;
                        case "class":
                            value = value.replace('focused', '');
                        default:
                            html.push(`<div class="properties">
                                      <div class="propname">${name === "" ? "&#160;" : name}</div>
                                      <div class="propval"><input value="${value}"></div>
                                   </div>
                                  `);
                    }
                }
                html.push(addPropertiesHtml);
                $form.find('#controlProperties')
                    .empty()
                    .append(html.join(''))
                    .find('.properties:even')
                    .css('background-color', '#f7f7f7');
                $form.find('#controlProperties input').css('text-indent', '3px');
                addDatafields();
                addValueOptions();
                $form.find('#controlProperties').append(deleteComponentHtml);
                if (type === 'Form') {
                    let isGrid = jQuery(originalHtml).parents('[data-type="Grid"]');
                    if (isGrid.length !== 0) {
                        $form.find('#controlProperties .propval >').attr('disabled', 'disabled');
                        $form.find('#controlProperties .addproperties, #controlProperties .deleteObject').remove();
                    }
                }
            });
            $form
                .off('change', '#controlProperties .propval')
                .on('change', '#controlProperties .propval', e => {
                e.stopImmediatePropagation();
                let value;
                let isCustomField;
                let attribute = jQuery(e.currentTarget).siblings('.propname').text();
                let $this = jQuery(e.currentTarget);
                if ($this.find('select').hasClass('datafields') || $this.find('select').hasClass('valueOptions')) {
                    value = jQuery(e.currentTarget).find('select').val();
                }
                else {
                    value = jQuery(e.currentTarget).find('input').val();
                }
                let index = jQuery(originalHtml).attr('data-index');
                if (value) {
                    if (type === 'Grid' || type === 'Browse') {
                        switch (attribute) {
                            case 'data-visible':
                                if (value === 'false') {
                                    jQuery(originalHtml).parent('.column').attr('style', 'display:none;');
                                }
                                else {
                                    jQuery(originalHtml).parent('.column').removeAttr(`style`);
                                }
                                jQuery(originalHtml).attr(`${attribute}`, `${value}`);
                                jQuery($customFormClone).find(`div[data-index="${index}"]`).parent('.column').attr(`${attribute}`, `${value}`);
                                break;
                            case 'data-width':
                                jQuery(originalHtml).find('.fieldcaption').attr(`style`, `min-width:${value}`);
                                jQuery($customFormClone).find(`div[data-index="${index}"]`).parent('.column').attr(`${attribute}`, `${value}`);
                                break;
                            case 'data-datafield':
                            case 'data-browsedatafield':
                                jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`${attribute}`, `${value}`);
                                jQuery(originalHtml).attr(`${attribute}`, `${value}`);
                                isCustomField = $form.find(`option[value="${value}"]`).attr('data-iscustomfield');
                                if (isCustomField === "true") {
                                    let datatype = $form.find(`option[value="${value}"]`).attr('data-type');
                                    switch (datatype) {
                                        case 'integer':
                                            datatype = "number";
                                            break;
                                        case 'float':
                                            datatype = "decimal";
                                            break;
                                        case 'date':
                                            datatype = "date";
                                            break;
                                        case 'true/false':
                                            datatype = "checkbox";
                                            break;
                                        default:
                                            datatype = "text";
                                            break;
                                    }
                                    jQuery(originalHtml).attr('data-customfield', 'true');
                                    jQuery($customFormClone).find(`div[data-index="${index}"]`).attr('data-customfield', 'true');
                                    jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`data-caption`, `${value}`);
                                    jQuery(originalHtml).attr(`data-caption`, `${value}`);
                                    $form.find(`#controlProperties .propname:contains('data-caption')`).siblings('.propval').find('input').val(value);
                                    jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`data-datatype`, datatype);
                                    jQuery(originalHtml).attr(`data-datatype`, datatype);
                                    $form.find(`#controlProperties .propname:contains('data-datatype')`).siblings('.propval').find('select').val(datatype);
                                }
                                break;
                            default:
                                jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`${attribute}`, `${value}`);
                                jQuery(originalHtml).attr(`${attribute}`, `${value}`);
                        }
                    }
                    else if (type === 'Form') {
                        if (attribute === 'data-datafield') {
                            isCustomField = $form.find(`option[value="${value}"]`).attr('data-iscustomfield');
                            jQuery(originalHtml).attr('data-caption', value);
                            jQuery(originalHtml).find(`.fwformfield-caption`).text(value);
                            $form.find(`#controlProperties .propname:contains('data-caption')`).siblings('.propval').find('input').val(value);
                            if (isCustomField === "true") {
                                let datatype = $form.find(`option[value="${value}"]`).attr('data-type');
                                switch (datatype) {
                                    case 'integer':
                                        datatype = "number";
                                        break;
                                    case 'float':
                                        datatype = "decimal";
                                        break;
                                    case 'date':
                                        datatype = "date";
                                        break;
                                    case 'true/false':
                                        datatype = "checkbox";
                                        break;
                                    default:
                                        datatype = "text";
                                        break;
                                }
                                jQuery(originalHtml).attr('data-type', datatype);
                                $form.find(`#controlProperties .propname:contains('data-type')`).siblings('.propval').find('select').val(datatype);
                                jQuery($customFormClone).find(`div[data-index="${index}"]`).attr('data-type', datatype);
                                jQuery(originalHtml).attr('data-customfield', 'true');
                                jQuery($customFormClone).find(`div[data-index="${index}"]`).attr('data-customfield', 'true');
                            }
                            jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`data-caption`, `${value}`);
                        }
                        if (attribute === 'data-caption') {
                            jQuery(originalHtml).find(`.fwformfield-caption`).text(value);
                        }
                        let isTab = jQuery(originalHtml).attr('data-type');
                        if (isTab === "tab") {
                            jQuery(originalHtml).find('.caption').text(value);
                        }
                        else {
                            jQuery(originalHtml).attr(`${attribute}`, `${value}`);
                        }
                        ;
                        jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`${attribute}`, `${value}`);
                    }
                }
                else {
                    if (attribute !== "data-datafield") {
                        jQuery(e.currentTarget).parents('.properties').hide();
                        jQuery($customFormClone).find(`div[data-index="${index}"]`).removeAttr(`${attribute}`);
                        jQuery(originalHtml).removeAttr(`${attribute}`);
                    }
                    else {
                        jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`${attribute}`, `${value}`);
                        jQuery(originalHtml).attr(`${attribute}`, `${value}`);
                    }
                }
                switch (type) {
                    case 'Form':
                        let a = 0;
                        a += (controlType == 'FwFormField') ? 1 : 0;
                        a += (controlType == 'FwContainer') ? 1 : 0;
                        if (a) {
                            FwControl.init(jQuery(originalHtml));
                            FwControl.renderRuntimeHtml(jQuery(originalHtml));
                        }
                        break;
                    case 'Browse':
                    case 'Grid':
                        let $control = $customFormClone.cloneNode(true);
                        $control = jQuery($control).find('.fwcontrol.fwbrowse');
                        $customForm
                            .empty()
                            .append($control);
                        if (type === 'Browse') {
                            FwControl.init($control);
                        }
                        FwControl.renderRuntimeHtml($control);
                        disableControls();
                        showHiddenColumns($control);
                        $draggableElements = $customForm.find('tr.fieldnames td.column:not(.tdselectrow):not(.browsecontextmenucell)');
                        $draggableElements.attr('draggable', 'true');
                        break;
                }
                $form.attr('data-propertieschanged', true);
                $form.attr('data-modified', 'true');
                $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
                updateHtml();
            })
                .off('keydown', '#controlProperties .propval')
                .on('keydown', '#controlProperties .propval', e => {
                e.stopImmediatePropagation();
                if (e.which === 13 || e.keyCode === 13) {
                    e.preventDefault();
                    jQuery(e.currentTarget).trigger('change');
                }
            })
                .off('change', '#controlProperties .addpropval, #controlProperties .addpropname')
                .on('change', '#controlProperties .addpropval, #controlProperties .addpropname', e => {
                e.stopImmediatePropagation();
                let newProp, newPropVal;
                if (jQuery(e.currentTarget).hasClass('addpropval')) {
                    newProp = jQuery(e.currentTarget).siblings('.addpropname').find('input').val();
                    newPropVal = jQuery(e.currentTarget).find('input').val();
                }
                else {
                    newProp = jQuery(e.currentTarget).find('input').val();
                    newPropVal = jQuery(e.currentTarget).siblings('.addpropval').find('input').val();
                }
                let index = jQuery(originalHtml).attr('data-index');
                if (newProp && newPropVal) {
                    let html = [];
                    html.push(` 
                                    <div class="properties">
                                      <div class="propname">${newProp}</div>
                                      <div class="propval"><input value="${newPropVal}"></div>
                                   </div>
                        `);
                    $form.find('#controlProperties .addproperties')
                        .before(html.join(''));
                    jQuery(originalHtml).attr(`${newProp}`, `${newPropVal}`);
                    jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`${newProp}`, `${newPropVal}`);
                    jQuery(e.currentTarget).siblings('.addpropname').find('input').val('');
                    jQuery(e.currentTarget).find('input').val('');
                    updateHtml();
                    $form.find('#controlProperties .properties:even')
                        .css('background-color', '#f7f7f7');
                }
            })
                .off('keydown', '#controlProperties .addpropval')
                .on('keydown', '#controlProperties .addpropval', e => {
                e.stopImmediatePropagation();
                if (e.which === 13 || e.keyCode === 13) {
                    jQuery(e.currentTarget).trigger('change');
                }
            })
                .off('click', '.deleteObject')
                .on('click', '.deleteObject', e => {
                let $confirmation = FwConfirmation.renderConfirmation('Delete', 'Delete this object?');
                let $yes = FwConfirmation.addButton($confirmation, 'Delete', false);
                let $no = FwConfirmation.addButton($confirmation, 'Cancel');
                $yes.off('click');
                $yes.on('click', e => {
                    let index = jQuery(originalHtml).attr('data-index');
                    FwConfirmation.destroyConfirmation($confirmation);
                    let $element = jQuery($customForm).find(`div[data-index="${index}"]`);
                    let $elementClone = jQuery($customFormClone).find(`div[data-index="${index}"]`);
                    if (type === 'Grid' || type === 'Browse') {
                        $elementClone.parent('div').remove();
                        $element.parent('td').remove();
                    }
                    else {
                        if ($element.siblings().length === 0 && $element.parent().hasClass('flexrow' || 'flexcolumn')) {
                            $element.parent().addClass('emptyContainer');
                        }
                        $elementClone.remove();
                        $element.remove();
                        if (jQuery(originalHtml).attr('data-type') === "tab") {
                            let tabPageId = jQuery(originalHtml).attr('data-tabpageid');
                            let tabPage = $customForm.find(`#${tabPageId}`);
                            let tabPageIndex = tabPage.attr('data-index');
                            jQuery($customForm).find(`div[data-index="${tabPageIndex}"]`).remove();
                            jQuery($customFormClone).find(`div[data-index="${tabPageIndex}"]`).remove();
                            let firstTab = $customForm.find('.tabs [data-type="tab"]:first');
                            FwTabs.setActiveTab($customForm, firstTab);
                        }
                    }
                    $form.find('#controlProperties').empty();
                    updateHtml();
                });
            })
                .off('click', '.addColumn')
                .on('click', '.addColumn', e => {
                e.stopPropagation();
                if (type === 'Browse' || type === 'Grid') {
                    let $control = jQuery($customFormClone).find(`[data-type="${type}"]`);
                    let hasSpacer = $control.find('div:last').hasClass('spacer');
                    let newTdIndex = lastIndex + 1;
                    let newFieldIndex = newTdIndex + 1;
                    let html = [];
                    html.push(`<div class="column" data-index="${newTdIndex}">
    <div class="field" data-index="${newFieldIndex}"></div>
  </div>
`);
                    let newColumn = jQuery(html.join(''));
                    hasSpacer === true ? newColumn.insertBefore($control.find('div.spacer')) : $control.append(newColumn);
                    originalHtml = newColumn.find('.field');
                    let propertyHtml = [];
                    let fields = [];
                    propertyHtml.push(propertyContainerHtml);
                    fields = ['data-datafield', 'data-datatype', 'data-sort', 'data-width', 'data-visible', 'data-caption', 'class'];
                    for (let i = 0; i < fields.length; i++) {
                        var value;
                        var field = fields[i];
                        switch (field) {
                            case 'data-datafield':
                                value = "";
                                break;
                            case 'data-datatype':
                                value = "text";
                                break;
                            case 'data-sort':
                                value = "off";
                                break;
                            case 'data-width':
                                value = "100px";
                                break;
                            case 'data-visible':
                                value = "true";
                                break;
                            case 'data-caption':
                                value = "New Column";
                                break;
                            case 'class':
                                value = 'field';
                        }
                        propertyHtml.push(`<div class="properties">
                                <div class="propname" style="border:.5px solid #efefef;">${field}</div>
                                <div class="propval" style="border:.5px solid #efefef;"><input value="${value}"></div>
                             </div>
                             `);
                        jQuery(originalHtml).attr(`${field}`, `${value}`);
                    }
                    ;
                    propertyHtml.push(addPropertiesHtml);
                    let newProperties = $form.find('#controlProperties');
                    newProperties
                        .empty()
                        .append(propertyHtml.join(''), deleteComponentHtml)
                        .find('.properties:even')
                        .css('background-color', '#f7f7f7');
                    $form.find('#controlProperties .propname:contains("data-datafield")')
                        .siblings('.propval')
                        .find('input')
                        .replaceWith(`<select style="width:92%" class="datafields" value="">`);
                    addDatafields();
                    $form.find('#controlProperties .propname:contains("data-datatype")')
                        .siblings('.propval')
                        .find('input')
                        .replaceWith(`<select style="width:92%" class="valueOptions" value="text">`);
                    addValueOptions();
                    $form.find('#controlProperties input').change();
                    lastIndex = newFieldIndex;
                }
                else if (type === 'Form') {
                    let $tabpage = $customForm.find('[data-type="tabpage"]:visible');
                    let tabpageIndex = $tabpage.attr('data-index');
                    let newFieldIndex = lastIndex + 1;
                    let html = [];
                    html.push(`<div data-index="${newFieldIndex}"></div>`);
                    originalHtml = jQuery(html.join(''));
                    let propertyHtml = [];
                    let fields = [];
                    propertyHtml.push(propertyContainerHtml);
                    fields = ['data-datafield', 'data-type', 'data-caption', 'class', 'data-control'];
                    for (let i = 0; i < fields.length; i++) {
                        var value;
                        var field = fields[i];
                        switch (field) {
                            case 'data-datafield':
                                value = "";
                                break;
                            case 'data-control':
                                value = "FwFormField";
                                break;
                            case 'data-type':
                                value = "text";
                                break;
                            case 'data-caption':
                                value = "New Field";
                                break;
                            case 'class':
                                value = 'fwcontrol fwformfield';
                        }
                        propertyHtml.push(`<div class="properties">
                                <div class="propname" style="border:.5px solid #efefef;">${field}</div>
                                <div class="propval" style="border:.5px solid #efefef;"><input value="${value}"></div>
                             </div>
                             `);
                        jQuery(originalHtml).attr(`${field}`, `${value}`);
                    }
                    ;
                    propertyHtml.push(addPropertiesHtml);
                    let newProperties = $form.find('#controlProperties');
                    newProperties
                        .empty()
                        .append(propertyHtml.join(''), deleteComponentHtml)
                        .find('.properties:even')
                        .css('background-color', '#f7f7f7');
                    $form.find('#controlProperties .propname:contains("data-datafield")')
                        .siblings('.propval')
                        .find('input')
                        .replaceWith(`<select style="width:92%" class="datafields" value="">`);
                    addDatafields();
                    $form.find('#controlProperties .propname:contains("data-type")')
                        .siblings('.propval')
                        .find('input')
                        .replaceWith(`<select style="width:92%" class="valueOptions" value="text">`);
                    addValueOptions();
                    lastIndex = newFieldIndex;
                    jQuery($customForm).find(`[data-index=${tabpageIndex}]`).append(originalHtml);
                    jQuery($customFormClone).find(`[data-index=${tabpageIndex}]`).append(originalHtml[0].cloneNode(true));
                    controlType = jQuery(originalHtml).attr('data-control');
                    $draggableElements = $customForm.find('div.fwformfield');
                    $draggableElements.attr('draggable', 'true');
                    $form.find('#controlProperties input').change();
                }
            })
                .off('click', '.addNewContainer')
                .on('click', '.addNewContainer', e => {
                jQuery(document).trigger('click');
                e.stopPropagation();
                let $tabpage = $customForm.find('[data-type="tabpage"]:visible');
                let tabpageIndex = $tabpage.attr('data-index');
                let newContainerIndex = lastIndex + 1;
                let html = [];
                html.push(`<div data-index="${newContainerIndex}"></div>`);
                originalHtml = jQuery(html.join(''));
                let propertyHtml = [];
                let fields = [];
                propertyHtml.push(propertyContainerHtml);
                fields = ['class'];
                for (let i = 0; i < fields.length; i++) {
                    var value;
                    var field = fields[i];
                    switch (field) {
                        case 'class':
                            value = 'flexrow emptyContainer';
                            break;
                    }
                    propertyHtml.push(`<div class="properties">
                                <div class="propname" style="border:.5px solid #efefef;">${field}</div>
                                <div class="propval" style="border:.5px solid #efefef;"><input value="${value}"></div>
                             </div>
                             `);
                    jQuery(originalHtml).attr(`${field}`, `${value}`);
                }
                ;
                propertyHtml.push(addPropertiesHtml);
                let newProperties = $form.find('#controlProperties');
                newProperties
                    .empty()
                    .append(propertyHtml.join(''), deleteComponentHtml)
                    .find('.properties:even')
                    .css('background-color', '#f7f7f7');
                lastIndex = newContainerIndex;
                jQuery($customForm).find(`[data-index=${tabpageIndex}]`).append(originalHtml);
                jQuery($customFormClone).find(`[data-index=${tabpageIndex}]`).append(originalHtml[0].cloneNode(true));
                $draggableElements = $customForm.find('div.fwformfield, div.flexrow, div.flexcolumn, div[data-type="tab"]');
                $draggableElements.attr('draggable', 'true');
                $form.find('#controlProperties input').change();
            })
                .off('click', '.addNewTab')
                .on('click', '.addNewTab', e => {
                e.stopPropagation();
                jQuery(document).trigger('click');
                let newIndex = lastIndex + 1;
                let $tabControl = $customForm.find('[data-control="FwTabs"]');
                let newTabIds = FwTabs.addTab($tabControl, 'New Tab', '', '', true);
                originalHtml = $customForm.find(`#${newTabIds.tabid}`);
                originalHtml.attr('data-index', newIndex);
                originalHtml.click();
                let html = [];
                html.push(`<div class="flexrow emptyContainer" data-index="${++newIndex}"></div>`);
                let newTabPage = $customForm.find(`#${newTabIds.tabpageid}`);
                newTabPage.attr('data-index', ++newIndex);
                newTabPage.append(html.join(''));
                let tabClone = originalHtml.cloneNode(true);
                jQuery(tabClone).empty();
                jQuery($customFormClone).find('.tabs').append(tabClone);
                jQuery($customFormClone).find('.tabpages').append(newTabPage[0].cloneNode(true));
                lastIndex = newIndex;
                updateHtml();
                $draggableElements = $customForm.find('div.fwformfield, div.flexrow, div.flexcolumn, div[data-type="tab"]');
                $draggableElements.attr('draggable', 'true');
            });
        }
    }
    getValueOptions(fieldname) {
        var values = [];
        switch (fieldname) {
            case 'data-browsedatatype':
            case 'data-formdatatype':
            case 'data-datatype':
            case 'data-type':
                values = [
                    'checkbox',
                    'checkboxlist',
                    'color',
                    'combobox',
                    'date',
                    'datetime',
                    'decimal',
                    'email',
                    'key',
                    'money',
                    'multiselectvalidation',
                    'note',
                    'number',
                    'percent',
                    'phone',
                    'radio',
                    'searchbox',
                    'select',
                    'tab',
                    'text',
                    'textarea',
                    'time',
                    'timepicker',
                    'validation'
                ];
                break;
            case 'data-sort':
                values = ['asc', 'desc', 'off'];
                break;
            case 'data-formrequired':
            case 'data-required':
            case 'data-enabled':
            case 'data-visible':
            case 'data-formreadonly':
            case 'data-isuniqueid':
                values = ['true', 'false'];
                break;
        }
        return values;
    }
}
;
var CustomFormController = new CustomForm();
//# sourceMappingURL=CustomForm.js.map