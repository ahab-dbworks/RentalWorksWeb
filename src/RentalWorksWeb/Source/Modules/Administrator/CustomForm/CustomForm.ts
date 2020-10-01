routes.push({ pattern: /^module\/customform$/, action: function (match: RegExpExecArray) { return CustomFormController.getModuleScreen(); } });

class CustomForm {
    Module:     string = 'CustomForm';
    apiurl:     string = 'api/v1/customform';
    caption:    string = Constants.Modules.Administrator.children.CustomForm.caption;
    nav:        string = Constants.Modules.Administrator.children.CustomForm.nav;
    id:         string = Constants.Modules.Administrator.children.CustomForm.id;
    codeMirror: any;
    doc: any;
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
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

        let userid = JSON.parse(sessionStorage.getItem('userid'));
        FwFormField.setValueByDataField($form, 'WebUserId', userid.webusersid);

        if (mode == 'NEW') {
            FwFormField.enable($form.find('[data-datafield="BaseForm"]'));
            $form.find('.userGrid, .groupGrid').hide();
            FwFormField.setValue($form, 'div[data-datafield="Active"]', true);
        }

        //removes field propagation
        $form.off('change', '.fwformfield[data-enabled="true"][data-datafield!=""]:not(.find-field)');

        this.loadModules($form);
        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        let $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="CustomFormId"] input').val(uniqueids.CustomFormId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    enableSave($form: any) {
        FwFormField.enable($form.data('fields'));
        FwFormField.disable($form.find('[data-datafield="BaseForm"]'));
        $form.find('[data-caption="Assign To"]').hide();
        $form.find('[data-type="RefreshMenuBarButton"]').hide();

        if (FwApplicationTree.isVisibleInSecurityTree('ddXtKGS07Iko')) {
            const $saveButton = $form.find('[data-type="SaveMenuBarButton"]');
            $saveButton.removeClass('disabled').show();
            $saveButton.off('click');
            $saveButton.on('click', e => {
                this.saveForm($form, { closetab: false });
            });
        }
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        $form.find('#codeEditor').change();
        const $customForm = $form.find(`#designerContent`);
        const $fields = $customForm.find('.fwformfield');
        //const $errorMsg = $form.find('.error-msg');
        let hasDuplicates: boolean = false;
        const duplicateFields: any = [];
        $fields.each(function (i, e) {
            const $fwFormField = jQuery(e);
            const dataField = $fwFormField.attr('data-datafield');
            if (dataField != "") {
                const $fieldFound = $customForm.find(`[data-datafield="${dataField}"][data-enabled="true"]`);
                if ($fieldFound.length > 1) {
                    $fieldFound.addClass('error');
                    hasDuplicates = true;
                    if (duplicateFields.indexOf(dataField) === -1) {
                        duplicateFields.push(dataField);
                    }
                    //return false;
                } else {
                    $customForm.find(`[data-datafield="${dataField}"]`).removeClass('error');
                }
            }
        });

        //for retaining position in code editor after saving
        $form.find('[data-datafield="Html"]').addClass('reload');

        //Removes fields from the Designer tab so they are ignored in isValid check.
        $form.data('uniqueids', $form.find('.fwformfield[data-isuniqueid="true"]').not('#designerContent .fwformfield, #previewWebForm .fwformfield'));
        $form.data('fields', $form.find('.fwformfield:not([data-isuniqueid="true"])').not('#designerContent .fwformfield,  #previewWebForm .fwformfield'));

        if (typeof $form.data('selfassign') != 'undefined') {
            const request: any = {
                BaseForm: FwFormField.getValueByDataField($form, 'BaseForm'),
                SelfAssign: $form.data('selfassign'),
                Html: FwFormField.getValueByDataField($form, 'Html'),
                Description: FwFormField.getValueByDataField($form, 'Description'),
                Active: FwFormField.getValueByDataField($form, 'Active'),
                WebUserId: FwFormField.getValueByDataField($form, 'WebUserId'),
                AssignTo: 'USERS'
            };
            const $tab = FwTabs.getTabByElement($form);
            const saveMode = $form.attr('data-mode');
            if (saveMode == 'NEW') {
                FwAppData.apiMethod(true, 'POST', `api/v1/customform/selfassign`, request, FwServices.defaultTimeout, response => {
                    FwFormField.setValueByDataField($form, 'CustomFormId', response.CustomFormId);
                    this.afterSave($form);
                }, ex => FwFunc.showError(ex), $form);
            } else if (saveMode == 'EDIT') {
                const customFormId = FwFormField.getValueByDataField($form, 'CustomFormId');
                request.CustomFormId = customFormId;
                FwAppData.apiMethod(true, 'PUT', `api/v1/customform/selfassign/${customFormId}`, request, FwServices.defaultTimeout, response => {
                    FwFormField.setValueByDataField($form, 'CustomFormId', customFormId);
                    this.afterSave($form);
                }, ex => FwFunc.showError(ex), $form);
            }
        } else {
            if (hasDuplicates) {
                //$errorMsg.text(`Duplicate fields: ${duplicateFields.join(', ')}`);
                //FwNotification.renderNotification('ERROR', 'Only one duplicate field can be active on a form.  Set the data-enabled property to false on duplicates.');
                let duplicatedFields: string = duplicateFields.join(', ');
                FwNotification.renderNotification(`ERROR`, `The following data fields are duplicated on this form: ${duplicatedFields}.<br><br>Set the "data-enabled" property to false on duplicates.`);
            } else {
                //$errorMsg.text('');
                FwModule.saveForm(this.Module, $form, parameters);
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    afterSave($form: any) {
        FwFormField.disable($form.find('[data-datafield="BaseForm"]'));
        $form.attr('data-modified', 'false');
        $form.find('.btn[data-type="SaveMenuBarButton"]').addClass('disabled');

        const baseForm = FwFormField.getValueByDataField($form, 'BaseForm');
        const html = FwFormField.getValueByDataField($form, 'Html');
        if (typeof $form.data('selfassign') != 'undefined') {
            jQuery('head').prepend(`<template id="tmpl-custom-${baseForm}">${html}</template>`);

            const newCustomForm: any = {
                BaseForm: baseForm,
                CustomFormId: FwFormField.getValueByDataField($form, 'CustomFormId'),
                Description: FwFormField.getValueByDataField($form, 'Description'),
                ThisUserOnly: true
            }

            let customForms = JSON.parse(sessionStorage.getItem('customForms'));
            if (customForms) {
                customForms.unshift(newCustomForm);
            } else {
                customForms = [newCustomForm];
            }

            sessionStorage.setItem('customForms', JSON.stringify(customForms));
            $form.removeData('selfassign');
            const controller = $form.find('[data-datafield="BaseForm"] option:selected').data('controllername');
            if (controller != 'undefined') {
                const nav = (<any>window)[controller].nav;
                program.navigate(nav);
            }
        }

        this.displayHiddenElements($form);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        //toggles "Assign To" grids
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

        //Loads html for code editor
        if (!$form.find('[data-datafield="Html"]').hasClass('reload')) {
            let html = $form.find('[data-datafield="Html"] textarea').val();
            if (typeof html !== 'undefined') {
                this.codeMirror.setValue(html);
            } else {
                this.codeMirror.setValue('');
            }
        }

        let controller: any = $form.find('[data-datafield="BaseForm"] option:selected').attr('data-controllername');
        this.addValidFields($form, controller);
        this.renderTab($form, 'Designer');

        //Sets form to modified upon changing code in editor
        this.codeMirror.on('change', function (codeMirror, change) {
            $form.attr('data-modified', 'true');
            $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
        });


        //check if this form is for this user only
        //const customFormId = FwFormField.getValueByDataField($form, 'CustomFormId');
        //const $customForms = JSON.parse(sessionStorage.getItem('customForms'));
        //const matchingForm = $customForms.find(obj => obj.CustomFormId == customFormId);
        //if (typeof matchingForm != 'undefined') {
        //    if (matchingForm.ThisUserOnly) {
        //        $form.data('selfassign', true);
        //    }
        //}
    }
    //----------------------------------------------------------------------------------------------
    codeMirrorEvents($form) {
        //Creates an instance of CodeMirror
        let textArea = $form.find('#codeEditor').get(0);
        var codeMirror = CodeMirror.fromTextArea(textArea,
            {
                mode: 'xml',
                lineNumbers: true,
                foldGutter: true,
                gutters: ["CodeMirror-linenumbers", "CodeMirror-foldgutter"]
            });
        this.codeMirror = codeMirror;
        this.doc = codeMirror.getDoc();

        //Select module event
        $form.find('div.modules').on('change', e => {
            const $this = $form.find('[data-datafield="BaseForm"] option:selected');
            const moduleName = $this.val();
            const moduleCaption = $this.text();
            const type = $this.attr('data-type');
            const controller: any = $this.attr('data-controllername');
            let modulehtml;

            //get the html from the template and set it as codemirror's value
            switch (type) {
                case 'Browse':
                    typeof (<any>window)[controller].getBrowseTemplate == "function" ? modulehtml = (<any>window)[controller].getBrowseTemplate() : modulehtml = jQuery(`#tmpl-modules-${moduleName}`).html();
                    break;
                case 'Form':
                    typeof (<any>window)[controller].getFormTemplate == "function" ? modulehtml = (<any>window)[controller].getFormTemplate() : modulehtml = jQuery(`#tmpl-modules-${moduleName}`).html();
                    break;
                case 'Grid':
                    modulehtml = jQuery(`#tmpl-grids-${moduleName}`).html();
                    break;
            }

            if (typeof modulehtml !== "undefined") {
                codeMirror.setValue(modulehtml);
            } else {
                codeMirror.setValue(`There is no ${type} available for this selection.`);
            }

            this.addValidFields($form, controller);
            this.renderTab($form, 'Designer');

            const fullName = sessionStorage.getItem('fullname');
            FwFormField.setValueByDataField($form, 'Description', `${fullName}'s ${moduleCaption}`);
        });

        //Updates value for form fields
        $form.find('#codeEditor').on('change', e => {
            codeMirror.save();
            let html = $form.find('textarea#codeEditor').val();
            FwFormField.setValueByDataField($form, 'Html', html);
        });

    }
    //----------------------------------------------------------------------------------------------
    addValidFields($form, controller) {
        let self = this;
        //Get valid field names and sort them
        const modulefields = $form.find('.modulefields');
        let moduleType = $form.find('[data-datafield="BaseForm"] option:selected').attr('data-type');
        let apiurl = $form.find('[data-datafield="BaseForm"] option:selected').attr('data-apiurl');
        modulefields.empty();
        switch (moduleType) {
            case 'Grid':
            case 'Browse':
                if (apiurl !== "undefined") {
                    FwAppData.apiMethod(true, 'GET', `${apiurl}/emptybrowseobject`, null, FwServices.defaultTimeout, function onSuccess(response) {
                        let columnNames = Object.keys(response);
                        let customFields = response._Custom.map(obj => ({ fieldname: obj.FieldName, fieldtype: obj.FieldType }));
                        let allValidFields: any = [];
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

                        $form.data('validdatafields', allValidFields.sort((a, b) => a.Field < b.Field ? -1 : 1));

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
                    let allValidFields: any = [];
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

                    $form.data('validdatafields', allValidFields.sort((a, b) => a.Field < b.Field ? -1 : 1));

                    for (let i = 0; i < allValidFields.length; i++) {
                        modulefields.append(`
                                <div data-iscustomfield=${allValidFields[i].IsCustom}>${allValidFields[i].Field}</div>
                                `);
                    }
                }, null, $form);
                break;
        }
    }
    //----------------------------------------------------------------------------------------------
    addButtonMenu($form) {
        let $buttonmenu = $form.find('.addColumn[data-type="btnmenu"]');
        let $addContainer = FwMenu.generateButtonMenuOption('ADD NEW CONTAINER')
            , $addTab = FwMenu.generateButtonMenuOption('ADD NEW TAB');

        let menuOptions = [];
        menuOptions.push($addContainer, $addTab);

        FwMenu.addButtonMenuOptions($buttonmenu, menuOptions);
    }
    //----------------------------------------------------------------------------------------------
    loadModules($form: JQuery) {
        // Load Modules dropdown with sorted list of Modules and Grids
        const modules = FwApplicationTree.getAllModules(false, false, (modules: any[], moduleCaption: string, moduleName: string, category: string, currentNode: any, nodeModule: IGroupSecurityNode, hasView: boolean, hasNew: boolean, hasEdit: boolean, moduleController: any) => {
            if (moduleController.hasOwnProperty('apiurl') && moduleController.Module != 'BlankHomePage') {
                modules.push({ value: `${moduleName}Browse`, text: `${moduleCaption} Browse`, type: 'Browse', controllername: moduleName + 'Controller', apiurl: moduleController.apiurl });
                if (hasView || hasNew || hasEdit) {
                    modules.push({ value: `${moduleName}Form`, text: `${moduleCaption} Form`, type: 'Form', controllername: moduleName + 'Controller', apiurl: moduleController.apiurl });
                }
            }
        });
        const grids = FwApplicationTree.getAllGrids(false, (modules: any[], moduleCaption: string, moduleName: string, category: string, currentNode: any, nodeModule: IGroupSecurityNode, hasNew: boolean, hasEdit: boolean, moduleController: any) => {
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
    //----------------------------------------------------------------------------------------------
    renderGrids($form) {
        FwBrowse.renderGrid({
            nameGrid:         'CustomFormGroupGrid',
            gridSecurityId:   '11txpzVKVGi2',
            moduleSecurityId: this.id,
            $form:            $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    CustomFormId: FwFormField.getValueByDataField($form, 'CustomFormId')
                };
            },
            beforeSave: (request: any) => {
                request.CustomFormId = FwFormField.getValueByDataField($form, 'CustomFormId')
            }
        });

        FwBrowse.renderGrid({
            nameGrid:         'CustomFormUserGrid',
            gridSecurityId:   'nHNdXDBX6m6cp',
            moduleSecurityId: this.id,
            $form:            $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    CustomFormId: FwFormField.getValueByDataField($form, 'CustomFormId')
                };
            },
            beforeSave: (request: any) => {
                request.CustomFormId = FwFormField.getValueByDataField($form, 'CustomFormId')
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    events($form) {
        //Load preview on click
        $form.on('click', '[data-type="tab"][data-caption="Preview"]', e => {
            this.renderTab($form, 'Preview');
        });

        //Load Design Tab
        $form.on('click', '[data-type="tab"][data-caption="Designer"]', e => {
            this.renderTab($form, 'Designer');
            this.displayHiddenElements($form);
        });

        //Refreshes and shows CodeMirror upon clicking HTML tab
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

        //add field on click
        $form.on('click', '.modulefields div', e => {
            const $this = jQuery(e.currentTarget);
            const doc = this.codeMirror.getDoc();
            const cursor = doc.getCursor();
            doc.replaceRange($this.text(), cursor);
            $form.find('#codeEditor').change();
        });
    }
    //----------------------------------------------------------------------------------------------
    renderTab($form, tabName: string) {
        let renderFormHere;
        let self = this;
        let type = $form.find('[data-datafield="BaseForm"] option:selected').attr('data-type');
        $form.find('#codeEditor').change();     // 10/25/2018 Jason H - updates the textarea formfield with the code editor html

        tabName === 'Designer' ? renderFormHere = 'designerContent' : renderFormHere = 'previewWebForm';

        let html = FwFormField.getValueByDataField($form, 'Html');
        $form.find(`#${renderFormHere}`).empty().append(html);

        //render forms
        let $customForm = $form.find(`#${renderFormHere}`);
        let $customFormClone;

        //add indexes for all divs
        if (tabName == 'Designer') {
            let $divs = $customForm.find('div');
            for (let i = 0; i < $divs.length; i++) {
                let div = jQuery($divs[i]);
                div.attr('data-index', i);

                //add browse and grid column container attributes
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

        //render grids
        let $grids = $customForm.find('[data-control="FwGrid"]');
        for (let i = 0; i < $grids.length; i++) {
            let $this = jQuery($grids[i]);
            let gridName = $this.attr('data-grid');
            let $gridControl = FwBrowse.loadGridFromTemplate(gridName);
            if ($gridControl.length) {
                $this.empty().append($gridControl);
                FwBrowse.init($gridControl);
                FwBrowse.renderRuntimeHtml($gridControl);
            } else {
                $this.text(`[${gridName}]`);
            }
        }

        function disableControls() {
            FwFormField.disable($customForm.find(`[data-type="validation"], [data-control="FwAppImage"]`));
            $customForm.find('[data-type="Browse"], [data-type="Grid"]').find('.pager').hide();
            $customForm.find('[data-type="Browse"] tbody, [data-type="Browse"] tfoot, [data-type="Grid"] tbody, [data-type="Grid"] tfoot').hide();
            FwFormField.disable($customForm.find('[data-type="Browse"], [data-type="Grid"]'));
            $customForm.find('tr.fieldnames .column >, .submenu-btn').off('click');

            $customForm.find(`[data-control="FwAppImage"]`).replaceWith('[Image Control]');

            //disables availability calendar
            $customForm.find('[data-control="FwSchedulerDetailed"]').unbind('onactivatetab');
            $customForm.find('[data-control="FwScheduler"]').unbind('onactivatetab');

            //disable and add placeholder text for togglebutton controls
            if (type === 'Form') {
                const $toggleButtons = $customForm.find('[data-type="togglebuttons"]');
                FwFormField.disable($toggleButtons);
                $toggleButtons.text('[Toggle Buttons]');
            }
        }
        disableControls();

        //Design mode borders & events
        if (tabName == 'Designer') {
            let originalHtml;
            let controlType;
            $form.find('#controlProperties')
                .empty();  //clear properties upon loading design tab

            $customForm.find('.tabpages .formpage').css('overflow', 'auto');

            //displays hidden columns in grids/browses
            function showHiddenColumns($control) {
                let hiddenColumns = $control.find('td[data-visible="false"]');
                for (let i = 0; i < hiddenColumns.length; i++) {
                    let self = jQuery(hiddenColumns[i]);
                    let caption = self.find('.caption')[0].textContent;

                    if (caption !== 'undefined') {
                        self.find('.caption')[0].textContent = `${caption} [Hidden]`;
                    } else {
                        if (type === 'Grid') {
                            let datafield = self.find('.field').attr('data-datafield');
                            self.find('.caption')[0].textContent = `${datafield} [Hidden]`;
                        } else if (type === 'Browse') {
                            let datafield = self.find('.field').attr('data-browsedatafield');
                            self.find('.caption')[0].textContent = `${datafield} [Hidden]`;
                        }
                    }
                    self.find('.fieldcaption').css(`background-color`, `#f9f9f9`);
                    //self.css('display', 'table-cell');
                    self.find('.caption').css('color', 'red');
                }
            }

            if (type === 'Grid' || type === 'Browse') {
                showHiddenColumns($customForm);
                $form.find('.addColumn[data-type="button"]')
                    .css('margin-left', '27%')
                    .show();
                $form.find('.addColumn[data-type="btnmenu"]').hide();
            } else if (type === 'Form') {
                $form.find('.addColumn[data-type="btnmenu"]')
                    //.css({ 'width': '177px', 'margin-left': '27%' })
                    .css({ 'display': 'flex', 'margin-left': '27%' })
                    .show();
                $form.find('.addColumn[data-type="button"]').hide();
                $form.find('.addColumn .btnmenuoption:contains("ADD NEW CONTAINER")').addClass('addNewContainer');
                $form.find('.addColumn .btnmenuoption:contains("ADD NEW TAB")').addClass('addNewTab');
            };

            //updates information for HTML tab
            function updateHtml() {
                let $modifiedClone = $customFormClone.cloneNode(true);
                jQuery($modifiedClone).find('div').removeAttr('data-index');
                jQuery($modifiedClone).find('.emptyContainer').removeClass('emptyContainer');
                FwFormField.setValueByDataField($form, 'Html', $modifiedClone.innerHTML);
                self.codeMirror.setValue($modifiedClone.innerHTML);
            };

            //adds select options for datafields
            function addDatafields() {
                const validFields = $form.data('validdatafields');
                if (typeof validFields === 'object') {
                    let datafieldOptions = $form.find('#controlProperties .propval .datafields');
                    for (let z = 0; z < datafieldOptions.length; z++) {
                        let field = jQuery(datafieldOptions[z]);
                        field.append(`<option value="" disabled>Select field</option>`)

                        for (let i = 0; i < validFields.length; i++) {
                            let $this = validFields[i];
                            field.append(`<option data-iscustomfield=${$this.IsCustom} value="${$this.Field}" data-type="${$this.FieldType}">${$this.Field}</option>`);
                        }
                        let value = jQuery(field).attr('value');
                        if (value) {
                            jQuery(field).find(`option[value="${value}"]`).prop('selected', true);
                        } else {
                            jQuery(field).find(`option[disabled]`).prop('selected', true);
                        };
                    }
                }
            };

            //limit values that can be selected for certain fields
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
                    } else {
                        jQuery($this).find(`option[disabled]`).prop('selected', true);
                    };
                }
            }

            let propertyContainerHtml =
                `<div class="propertyContainer" style="border: 1px solid #bbbbbb; word-break: break-word;">
                     <div style="text-indent:5px;">
                         <div style="font-weight:bold; background-color:#dcdcdc; width:50%; float:left;">Name</div>
                         <div style="font-weight:bold; background-color:#dcdcdc; width:50%; float:left;">Value</div>
                     </div>
                        `;

            let addPropertiesHtml =
                `   <div class="addproperties adv-property" style="width:100%; display:flex;">
                        <div class="addpropname" style="padding:3px; border:.5px solid #efefef; width:50%; float:left; font-size:.9em;"><input placeholder="Add new property"></div>
                        <div class="addpropval" style="padding:3px; border:.5px solid #efefef; width:50%; float:left; font-size:.9em;"><input placeholder="Add value"></div>
                    </div>
                 </div>`; //closing div for propertyContainer

            const showAdvancedPropertiesHtml = `<div style="text-align:right;">
                                                    <span class="show-advanced-properties">Show Advanced Properties</span>
                                                </div>`;

            const deleteComponentHtml = () => {
                let caption;
                if (type == 'Grid' || type == 'Browse') {
                    caption = $form.find('.propname:contains("data-caption")').siblings('.propval').find('input').val();
                    if (caption == 'New Column') {
                        caption = 'Delete New Column';
                    } else {
                        caption = `Delete ${caption} Column`;
                    }
                } else {
                    caption = 'Delete Component';
                }

                const btn = `<div style="text-align:center;margin-top: 1em;">
                                <div class="fwformcontrol deleteObject" data-type="button">${caption}</div>
                             </div>`;
                return btn;
            }

            let lastIndex = Number(jQuery($customFormClone).find('div:last').attr('data-index'));

            //drag and drop
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
                            } else if (domIndexDrag < domIndexDrop) {
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

                        //for updating the formfield and codemirror
                        let firstColumn = jQuery($customFormClone).find(`[data-index="${indexDrag}"]`).parent();
                        let secondColumn = jQuery($customFormClone).find(`[data-index="${indexDrop}"]`).parent();
                        $this.replaceWith($elementDragged.parent());
                        if (domIndexDrag < domIndexDrop) {
                            firstColumn.insertAfter(secondColumn);
                        } else if (domIndexDrag > domIndexDrop) {
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
            } else if (type === 'Form') {
                let indexDrag;
                let indexDrop;
                let $elementDragged;
                let $preview;
                let $parent;
                $draggableElements = $customForm.find('div.fwformfield, div.flexrow, div.flexcolumn, div[data-type="tab"]');
                $draggableElements.attr('draggable', 'true');

                //find empty flexrows and add min-heights and allow dropping into
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
                                } else if (x > halfElementWidth) {
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
                                } else if (y > halfElementHeight) {
                                    $preview.insertAfter($this);
                                    $preview.addClass('addedAfter');
                                }
                            }
                        } else {
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

                        //for updating the formfield and codemirror
                        let firstColumn = jQuery($customFormClone).find(`[data-index="${indexDrag}"]`);
                        let secondColumn = jQuery($customFormClone).find(`[data-index="${indexDrop}"]`);

                        //check to see if dropping into empty div
                        if ($this.parent().hasClass('emptyContainer')) {
                            secondColumn.append(firstColumn);
                            $this.parent().removeClass('emptyContainer');
                        } else {
                            if ($this.hasClass('addedBefore')) {
                                firstColumn.insertBefore(secondColumn);
                            } else if ($this.hasClass('addedAfter')) {
                                firstColumn.insertAfter(secondColumn);
                            }
                        };

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
            //changes cursor when element is dragged anywhere in the document
            let $page: any = jQuery(document);
            $page
                .off('dragover')
                .on('dragover', e => {
                    e.originalEvent.dataTransfer.dropEffect = "none";
                });

            $customForm
                //build properties section
                .off('click')
                .on('click',
                    '[data-control="FwGrid"], [data-type="Browse"] thead tr.fieldnames .column >, [data-type="Grid"] thead tr.fieldnames .column >, [data-control="FwContainer"], [data-control="FwFormField"], div.flexrow, div.flexcolumn, div[data-type="tab"]',
                    e => {
                        e.stopPropagation();
                        originalHtml = e.currentTarget;
                        controlType = jQuery(originalHtml).attr('data-control');
                        let properties = jQuery(e.currentTarget.attributes).sort((a, b) => (a.name > b.name) ? 1 : ((b.name > a.name) ? -1 : 0));  //sorts attributes list
                        const basicProperties: any = ['data-browsedatafield', 'data-caption', 'data-datafield'];
                        let html: any = [];
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
                                    html.push(`<div class="properties ${basicProperties.includes(name) ? 'basic-property' : 'adv-property'}">
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
                                    html.push(`<div class="properties adv-property">
                                      <div class="propname">${name === "" ? "&#160;" : name}</div>
                                      <div class="propval"><select style="width:92%" class="valueOptions" value="${value}"></select></div>
                                   </div>
                                  `);
                                    break;
                                case 'data-caption':
                                    html.push(`<div class="properties basic-property">
                                      <div class="propname">${name === "" ? "&#160;" : name}</div>
                                      <div class="propval"><input value="${value}"></div>
                                   </div>
                                  `);
                                    break;
                                case "class":
                                    value = value.replace('focused', '');
                                default:
                                    html.push(`<div class="properties adv-property">
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

                        //delete object
                        $form.find('#controlProperties').append(showAdvancedPropertiesHtml, deleteComponentHtml());

                        //disables grids and browses in forms
                        if (type === 'Form') {
                            let isGrid = jQuery(originalHtml).parents('[data-type="Grid"]');
                            if (isGrid.length !== 0) {
                                $form.find('#controlProperties .propval >').attr('disabled', 'disabled');
                                $form.find('#controlProperties .addproperties, #controlProperties .deleteObject').remove();
                            }
                        }

                        this.showAdvancedProperties($form);
                    });

            $form
                //updates designer content with new attributes and updates code editor
                .off('change', '#controlProperties .propval')
                .on('change', '#controlProperties .propval', e => {
                    e.stopImmediatePropagation();
                    let value;
                    let isCustomField;
                    let attribute = jQuery(e.currentTarget).siblings('.propname').text();
                    let $this = jQuery(e.currentTarget);
                    if ($this.find('select').hasClass('datafields') || $this.find('select').hasClass('valueOptions')) {
                        value = jQuery(e.currentTarget).find('select').val();
                    } else {
                        value = jQuery(e.currentTarget).find('input').val();
                    }

                    let index = jQuery(originalHtml).attr('data-index');

                    if (value) {
                        if (type === 'Grid' || type === 'Browse') {
                            switch (attribute) {
                                case 'data-visible':
                                    if (value === 'false') {
                                        jQuery(originalHtml).parent('.column').attr('style', 'display:none;');
                                    } else {
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
                                    const $field = jQuery($customFormClone).find(`div[data-index="${index}"]`);
                                    if ($field.length > 0) {
                                        //jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`${attribute}`, `${value}`);
                                        $field.attr('data-datafield', value);
                                        $field.attr('data-browsedatafield', value);
                                        jQuery(originalHtml).attr('data-datafield', value);
                                        jQuery(originalHtml).attr('data-browsedatafield', value);
                                        //jQuery(originalHtml).attr(`${attribute}`, `${value}`);
                                        $form.find(`#controlProperties .propname:contains('data-caption')`).siblings('.propval').find('input').val(value);
                                        isCustomField = $form.find(`option[value="${value}"]`).attr('data-iscustomfield');
                                        if (isCustomField === "true") {
                                            //update caption and datatypes
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
                                            jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`data-datatype`, datatype);
                                            jQuery(originalHtml).attr(`data-datatype`, datatype);
                                            $form.find(`#controlProperties .propname:contains('data-datatype')`).siblings('.propval').find('select').val(datatype);
                                        }
                                        jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`data-caption`, `${value}`);
                                    }
                                    break;
                                case 'data-datatype':
                                case 'data-formdatatype':
                                    $form.find(`#controlProperties .propname:contains('data-browsedatatype')`).siblings('.propval').find('select').val(value);
                                case 'data-browsedatatype':
                                    $form.find(`#controlProperties .propname:contains('data-formdatatype')`).siblings('.propval').find('select').val(value);
                                    $form.find(`#controlProperties .propname:contains('data-datatype')`).siblings('.propval').find('select').val(value);
                                    jQuery($customFormClone).find(`div[data-index="${index}"]`).attr({ 'data-datatype': value, 'data-formdatatype': value, 'data-browsedatatype': value });
                                    jQuery(originalHtml).attr({ 'data-datatype': value, 'data-formdatatype': value, 'data-browsedatatype': value });
                                    break;
                                default:
                                    jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`${attribute}`, `${value}`);
                                    jQuery(originalHtml).attr(`${attribute}`, `${value}`);
                            }
                        } else if (type === 'Form') {
                            switch (attribute) {
                                case 'data-datafield':
                                    isCustomField = $form.find(`option[value="${value}"]`).attr('data-iscustomfield');

                                    //update caption when datafield is changed
                                    jQuery(originalHtml).attr('data-caption', value);
                                    jQuery(originalHtml).find(`.fwformfield-caption`).text(value);
                                    $form.find(`#controlProperties .propname:contains('data-caption')`).siblings('.propval').find('input').val(value);

                                    if (isCustomField === "true") {
                                        //update datatype
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
                                    break;
                                case 'data-caption':
                                    if (jQuery(originalHtml).attr('data-type') === 'section') {
                                        jQuery(originalHtml).attr('data-caption', value);
                                        jQuery(originalHtml).find('.fwform-section-title').text(value);
                                    } else {
                                        jQuery(originalHtml).find(`.fwformfield-caption`).text(value);
                                    }
                                    break;
                                case 'data-type':
                                    jQuery(originalHtml).attr('data-type', value);
                                    if (typeof jQuery(originalHtml).data('rendered') === 'boolean') {
                                        jQuery(originalHtml).data('rendered', false)
                                    }
                                    break;
                            }

                            let isTab = jQuery(originalHtml).attr('data-type');
                            if (isTab === "tab") {
                                //for changing tab captions
                                jQuery(originalHtml).find('.caption').text(value);
                            } else {
                                jQuery(originalHtml).attr(`${attribute}`, `${value}`);
                            };

                            jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`${attribute}`, `${value}`);
                        }
                    } else {
                        if (attribute !== "data-datafield") { //for adding new fields
                            jQuery(e.currentTarget).parents('.properties').hide();
                            jQuery($customFormClone).find(`div[data-index="${index}"]`).removeAttr(`${attribute}`);
                            jQuery(originalHtml).removeAttr(`${attribute}`);
                        } else {
                            jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`${attribute}`, `${value}`);
                            jQuery(originalHtml).attr(`${attribute}`, `${value}`);
                        }
                    }

                    switch (type) {
                        case 'Form':
                            if (controlType == 'FwFormField' || controlType == 'FwContainer') {
                                FwControl.init(jQuery(originalHtml));
                                FwControl.renderRuntimeHtml(jQuery(originalHtml));

                                if (attribute === 'data-type' && value === 'radio') {
                                    const $radioControl = jQuery(originalHtml).find('> .fwformfield-control')
                                    $radioControl.find('.fwformfield-caption').remove();
                                    $radioControl.find('label').text('Option');
                                }
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
                            //have to reinitialize after adding a new column
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

                //Add new properties 
                .off('change', '#controlProperties .addpropval, #controlProperties .addpropname')
                .on('change', '#controlProperties .addpropval, #controlProperties .addpropname', e => {
                    e.stopImmediatePropagation();
                    let newProp, newPropVal;
                    if (jQuery(e.currentTarget).hasClass('addpropval')) {
                        newProp = jQuery(e.currentTarget).siblings('.addpropname').find('input').val();
                        newPropVal = jQuery(e.currentTarget).find('input').val();
                    } else {
                        newProp = jQuery(e.currentTarget).find('input').val();
                        newPropVal = jQuery(e.currentTarget).siblings('.addpropval').find('input').val();
                    }
                    let index = jQuery(originalHtml).attr('data-index');
                    if (newProp && newPropVal) {
                        let html: any = [];
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
                //delete button
                .off('click', '.deleteObject')
                .on('click', '.deleteObject', e => {
                    let caption;
                    if (type == 'Grid' || type == 'Browse') {
                        caption = $form.find('.propname:contains("data-caption")').siblings('.propval').find('input').val();
                        if (caption == 'New Column') {
                            caption = 'Delete New Column?';
                        } else {
                            caption = `Delete ${caption} Column?`;
                        }
                    } else {
                        caption = 'Delete this component?';
                    }

                    let $confirmation = FwConfirmation.renderConfirmation('Delete', caption);
                    let $yes = FwConfirmation.addButton($confirmation, 'Delete', false);
                    FwConfirmation.addButton($confirmation, 'Cancel');

                    $yes.off('click');
                    $yes.on('click', e => {
                        let index = jQuery(originalHtml).attr('data-index');
                        FwConfirmation.destroyConfirmation($confirmation);

                        let $element = jQuery($customForm).find(`div[data-index="${index}"]`);
                        let $elementClone = jQuery($customFormClone).find(`div[data-index="${index}"]`);
                        if (type === 'Grid' || type === 'Browse') {
                            $elementClone.parent('div').remove();
                            $element.parent('td').remove();
                        } else {
                            if ($element.siblings().length === 0 && $element.parent().hasClass('flexrow' || 'flexcolumn')) {
                                $element.parent().addClass('emptyContainer');
                            }
                            $elementClone.remove();
                            $element.remove();

                            //remove tabpages when tabs are removed
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
                //add new column/field button
                .off('click', '.addColumn')
                .on('click', '.addColumn', e => {
                    e.stopPropagation();
                    if (type === 'Browse' || type === 'Grid') {
                        let $control = jQuery($customFormClone).find(`[data-type="${type}"]`);
                        let hasSpacer = $control.find('div:last').hasClass('spacer');
                        let newTdIndex = lastIndex + 1;
                        let newFieldIndex = newTdIndex + 1;
                        //build column base
                        let html: any = [];
                        html.push
                            (`<div class="column" data-index="${newTdIndex}">
    <div class="field" data-index="${newFieldIndex}"></div>
  </div>
`); //needs to be formatted this way so it looks nice in the code editor

                        let newColumn = jQuery(html.join(''));

                        hasSpacer === true ? newColumn.insertBefore($control.find('div.spacer')) : $control.append(newColumn);

                        originalHtml = newColumn.find('.field');

                        //build properties column
                        let propertyHtml: any = [];
                        let fields: any = [];

                        propertyHtml.push(propertyContainerHtml);
                        fields = ['data-datafield', 'data-caption', 'data-datatype', 'data-sort', 'data-width', 'data-visible', 'class'];
                        for (let i = 0; i < fields.length; i++) {
                            let value, isBasicProp;
                            const field = fields[i];
                            switch (field) {
                                case 'data-datafield':
                                    value = "";
                                    isBasicProp = true;
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
                                    isBasicProp = true;
                                    break;
                                case 'class':
                                    value = 'field';
                            }
                            propertyHtml.push(
                                `<div class="properties ${isBasicProp ? 'basic-property' : 'adv-property'}">
                                <div class="propname" style="border:.5px solid #efefef;">${field}</div>
                                <div class="propval" style="border:.5px solid #efefef;"><input value="${value}"></div>
                             </div>
                             `);

                            jQuery(originalHtml).attr(`${field}`, `${value}`);
                        };
                        propertyHtml.push(addPropertiesHtml);

                        let newProperties = $form.find('#controlProperties');
                        newProperties
                            .empty()
                            .append(propertyHtml.join(''))
                            .find('.properties:even')
                            .css('background-color', '#f7f7f7');

                        //replace input field with select
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
                        newProperties.append(showAdvancedPropertiesHtml, deleteComponentHtml());
                        $form.find('#controlProperties input').change();
                        this.showAdvancedProperties($form);
                        lastIndex = newFieldIndex
                    } else if (type === 'Form') {
                        let $tabpage = $customForm.find('[data-type="tabpage"]:visible');
                        let tabpageIndex = $tabpage.attr('data-index');

                        let newFieldIndex = lastIndex + 1;
                        let html: any = [];
                        html.push(`<div data-index="${newFieldIndex}"></div>`);

                        originalHtml = jQuery(html.join(''));

                        //build properties column
                        let propertyHtml: any = [];
                        let fields: any = [];

                        propertyHtml.push(propertyContainerHtml);
                        fields = ['data-datafield', 'data-type', 'data-caption', 'class', 'data-control'];
                        for (let i = 0; i < fields.length; i++) {
                            let value, isBasicProp;
                            const field = fields[i];
                            switch (field) {
                                case 'data-datafield':
                                    value = ""
                                    isBasicProp = true;
                                    break;
                                case 'data-control':
                                    value = "FwFormField"
                                    break;
                                case 'data-type':
                                    value = "text"
                                    break;
                                case 'data-caption':
                                    value = "New Field";
                                    isBasicProp = true;
                                    break;
                                case 'class':
                                    value = 'fwcontrol fwformfield';
                            }
                            propertyHtml.push(
                                `<div class="properties  ${isBasicProp ? 'basic-property' : 'adv-property'}">
                                <div class="propname" style="border:.5px solid #efefef;">${field}</div>
                                <div class="propval" style="border:.5px solid #efefef;"><input value="${value}"></div>
                             </div>
                             `);

                            jQuery(originalHtml).attr(`${field}`, `${value}`);
                        };
                        propertyHtml.push(addPropertiesHtml);

                        let newProperties = $form.find('#controlProperties');
                        newProperties
                            .empty()
                            .append(propertyHtml.join(''), showAdvancedPropertiesHtml, deleteComponentHtml())
                            .find('.properties:even')
                            .css('background-color', '#f7f7f7');

                        //replace input field with select
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
                        this.showAdvancedProperties($form);
                    }
                })
                .off('click', '.addNewContainer')
                .on('click', '.addNewContainer', e => {
                    //closes the menu w/ event listener add when creating button menu
                    jQuery(document).trigger('click');

                    e.stopPropagation();
                    let $tabpage = $customForm.find('[data-type="tabpage"]:visible');
                    let tabpageIndex = $tabpage.attr('data-index');

                    let newContainerIndex = lastIndex + 1;
                    let html: any = [];
                    html.push(`<div data-index="${newContainerIndex}"></div>`);

                    originalHtml = jQuery(html.join(''));

                    //build properties column
                    let propertyHtml: any = [];
                    let fields: any = [];

                    propertyHtml.push(propertyContainerHtml);
                    fields = ['class'/*, 'style'*/];
                    for (let i = 0; i < fields.length; i++) {
                        var value;
                        var field = fields[i];
                        switch (field) {
                            //case 'style':
                            //    value = 'min-height:50px';
                            //    break;
                            case 'class':
                                value = 'flexrow emptyContainer';
                                break;
                        }
                        propertyHtml.push(
                            `<div class="properties basic-prop">
                                <div class="propname" style="border:.5px solid #efefef;">${field}</div>
                                <div class="propval" style="border:.5px solid #efefef;"><input value="${value}"></div>
                             </div>
                             `);

                        jQuery(originalHtml).attr(`${field}`, `${value}`);
                    };
                    propertyHtml.push(addPropertiesHtml);

                    let newProperties = $form.find('#controlProperties');
                    newProperties
                        .empty()
                        .append(propertyHtml.join(''))
                        .find('.properties:even')
                        .css('background-color', '#f7f7f7');

                    lastIndex = newContainerIndex;
                    jQuery($customForm).find(`[data-index=${tabpageIndex}]`).append(originalHtml);
                    jQuery($customFormClone).find(`[data-index=${tabpageIndex}]`).append(originalHtml[0].cloneNode(true));
                    $draggableElements = $customForm.find('div.fwformfield, div.flexrow, div.flexcolumn, div[data-type="tab"]');
                    $draggableElements.attr('draggable', 'true');
                    $form.find('#controlProperties input').change();
                    newProperties.append(showAdvancedPropertiesHtml, deleteComponentHtml());
                    this.showAdvancedProperties($form);
                })
                .off('click', '.addNewTab')
                .on('click', '.addNewTab', e => {
                    e.stopPropagation();
                    //closes the menu w/ event listener add when creating button menu
                    jQuery(document).trigger('click');

                    let newIndex = lastIndex + 1;
                    let $tabControl = $customForm.find('[data-control="FwTabs"]');
                    let newTabIds = FwTabs.addTab($tabControl, 'New Tab', '', '', true); //contains tabid and tabpageid

                    originalHtml = $customForm.find(`#${newTabIds.tabid}`);
                    originalHtml.attr('data-index', newIndex);

                    originalHtml.click();

                    let html: any = [];
                    html.push(`<div class="flexrow emptyContainer" data-index="${++newIndex}"></div>`);

                    let newTabPage = $customForm.find(`#${newTabIds.tabpageid}`);
                    newTabPage.attr('data-index', ++newIndex);
                    newTabPage.append(html.join(''));

                    //update html for code editor
                    let tabClone = originalHtml.cloneNode(true);
                    jQuery(tabClone).empty();
                    jQuery($customFormClone).find('.tabs').append(tabClone);
                    jQuery($customFormClone).find('.tabpages').append(newTabPage[0].cloneNode(true));
                    lastIndex = newIndex;
                    updateHtml();

                    $draggableElements = $customForm.find('div.fwformfield, div.flexrow, div.flexcolumn, div[data-type="tab"]');
                    $draggableElements.attr('draggable', 'true');
                })
                .off('click', '.show-advanced-properties')
                .on('click', '.show-advanced-properties', e => {
                    if (typeof $form.data('show-advanced') == 'undefined') {
                        $form.data('show-advanced', true);
                    } else {
                        $form.data('show-advanced') ? $form.data('show-advanced', false) : $form.data('show-advanced', true)
                    }
                    this.showAdvancedProperties($form);
                })
                .off('change', '[data-datafield="ShowHidden"]')
                .on('change', '[data-datafield="ShowHidden"]', e => {
                    this.displayHiddenElements($form);
                });
        }
    }
    //----------------------------------------------------------------------------------------------
    displayHiddenElements($form: JQuery) {
        let $hiddenElements, showClassName;
        const moduleType = $form.find('[data-datafield="BaseForm"] option:selected').attr('data-type');
        const showHidden = FwFormField.getValueByDataField($form, 'ShowHidden');
        const $designer = $form.find('#designerContent');
        if (moduleType === 'Form') {
            showClassName = 'dsgn-show-hidden';
            $hiddenElements = jQuery($designer).find('div[data-datafield][style*="display:none"], div.flexrow[style*="display:none"], div.flexcolumn[style*="display:none"], div.flexrow[style*="display:none"] div[data-datafield], div.flexcolumn[style*="display:none"] div[data-datafield]');
        } else {
            showClassName = 'dsgn-show-hidden-browse';
            $hiddenElements = jQuery($designer).find('td[data-visible="false"]');
        }

        if (showHidden) {
            $hiddenElements.addClass(showClassName);
        } else {
            $hiddenElements.removeClass(showClassName);
        }
    }
    //----------------------------------------------------------------------------------------------
    showAdvancedProperties($form: JQuery) {
        const $this = $form.find('.show-advanced-properties');

        if (typeof $form.data('show-advanced') == 'undefined') {
            $form.data('show-advanced', false);
        }

        if ($form.data('show-advanced')) {
            $this.text('Hide Advanced Properties');
            $form.find('#controlProperties div.adv-property').css('display', 'flex');
        } else {
            $this.text('Show Advanced Properties');
            $form.find('#controlProperties div.adv-property').css('display', 'none');
        }
    }
    //----------------------------------------------------------------------------------------------
    getValueOptions(fieldname: string) {
        var values: any = [];
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
};
//----------------------------------------------------------------------------------------------
var CustomFormController = new CustomForm();