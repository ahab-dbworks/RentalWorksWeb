routes.push({ pattern: /^module\/customform$/, action: function (match: RegExpExecArray) { return CustomFormController.getModuleScreen(); } });
class CustomForm {
    Module: string = 'CustomForm';
    apiurl: string = 'api/v1/customform';
    caption: string = 'Custom Forms';
    nav: string = 'module/customform';
    id: string = 'CB2EF8FF-2E8D-4AD0-B880-07037B839C5E';
    codeMirror: any;
    doc: any;
    datafields: any;
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Custom Form', false, 'BROWSE', true);
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
        var $browse;

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        let userid = JSON.parse(sessionStorage.getItem('userid'));
        FwFormField.setValueByDataField($form, 'WebUserId', userid.webusersid);

        if (mode == 'NEW') {
            FwFormField.enable($form.find('[data-datafield="BaseForm"]'));
        }

        this.loadModules($form);
        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="CustomFormId"] input').val(uniqueids.CustomFormId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        $form.find('#codeEditor').change();

        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    afterSave($form: any) {
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
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        //Loads html for code editor
        let html = $form.find('[data-datafield="Html"] textarea').val();
        if (typeof html !== 'undefined') {
            this.codeMirror.setValue(html);
        } else {
            this.codeMirror.setValue('');
        }
        let controller: any = $form.find('[data-datafield="BaseForm"] option:selected').attr('data-controllername');
        this.addValidFields($form, controller);
        this.renderTab($form, 'Designer');

        //Sets form to modified upon changing code in editor
        this.codeMirror.on('change', function (codeMirror, change) {
            $form.attr('data-modified', 'true');
            $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
        });
    }
    //----------------------------------------------------------------------------------------------
    codeMirrorEvents($form) {
        //Creates an instance of CodeMirror
        let textArea = $form.find('#codeEditor').get(0);
        var codeMirror = CodeMirror.fromTextArea(textArea,
            {
                mode: 'xml'
                , lineNumbers: true
                , foldGutter: true
                , gutters: ["CodeMirror-linenumbers", "CodeMirror-foldgutter"]
            });
        this.codeMirror = codeMirror;
        this.doc = codeMirror.getDoc();

        //Select module event
        $form.find('div.modules').on('change', e => {
            let $this = $form.find('[data-datafield="BaseForm"] option:selected');
            let moduleName = $this.val();
            let type = $this.attr('data-type');
            let controller: any = $this.attr('data-controllername');
            let modulehtml;

            //get the html from the template and set it as codemirror's value
            switch (type) {
                case 'Browse':
                    modulehtml = jQuery(`#tmpl-modules-${moduleName}`).html();

                    //For modules where the html is in the TS file
                    if (typeof modulehtml == 'undefined') {
                        modulehtml = window[controller].getBrowseTemplate();
                    }
                    break;

                case 'Form':
                    modulehtml = jQuery(`#tmpl-modules-${moduleName}`).html();

                    //For modules where the html is in the TS file
                    if (typeof modulehtml == 'undefined') {
                        modulehtml = window[controller].getFormTemplate();
                    }
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
        let moduleNav = controller.slice(0, -10);
        let apiurl = $form.find('[data-datafield="BaseForm"] option:selected').attr('data-apiurl');
        modulefields.empty();
        switch (moduleType) {
            case 'Grid':
            case 'Browse':
                //let request: any = {};
                //request = {
                //    emptyobject: true
                //};
                if (apiurl !== "undefined") {
                    FwAppData.apiMethod(true, 'GET', `${apiurl}/emptyobject`, /*request*/null , FwServices.defaultTimeout, function onSuccess(response) {
                        let columnNames = Object.keys(response);
                        let customFields = response._Custom.map(obj => ({ fieldname: obj.FieldName, fieldtype: obj.FieldType }));
                        let allValidFields: any = [];
                        for (let i = 0; i < columnNames.length; i++) {
                            if (columnNames[i] != 'DateStamp' && columnNames[i] != 'RecordTitle' && columnNames[i] != '_Custom') {
                                allValidFields.push({
                                    'Field': columnNames[i]
                                    , 'IsCustom': 'false'
                                });
                            }
                        }

                        for (let i = 0; i < customFields.length; i++) {
                            allValidFields.push({
                                'Field': customFields[i].fieldname
                                , 'IsCustom': 'true'
                                , 'FieldType': customFields[i].fieldtype.toLowerCase()
                            });
                        }

                        self.datafields = allValidFields.sort(compare);

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
                    let customFields = response._Custom.map(obj => ({ fieldname: obj.FieldName, fieldtype: obj.FieldType}));
                    let allValidFields:any =[];
                    for (let i = 0; i < columnNames.length; i++) {
                        if (columnNames[i] != 'DateStamp' && columnNames[i] != 'RecordTitle' && columnNames[i] != '_Custom') {
                            allValidFields.push({
                                'Field': columnNames[i]
                                , 'IsCustom': 'false'
                            });
                        }
                    }

                    for (let i = 0; i < customFields.length; i++) {
                        allValidFields.push({
                            'Field': customFields[i].fieldname
                            , 'IsCustom': 'true'
                            , 'FieldType': customFields[i].fieldtype.toLowerCase()
                        });
                    }

                    self.datafields = allValidFields.sort(compare);

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
    loadModules($form) {
        let $moduleSelect
            , node
            , mainModules
            , settingsModules
            , modules
            , allModules;

        //Traverse security tree to find all browses and forms
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
            };
        }

        //Traverse security tree to find all grids
        const gridNode = FwApplicationTree.getNodeById(FwApplicationTree.tree, '43765919-4291-49DD-BE76-F69AA12B13E8');
        let gridModules = FwApplicationTree.getChildrenByType(gridNode, 'Grid');
        for (let i = 0; i < gridModules.length; i++) {
            addModulesToList(gridModules[i], 'Grid');
        };

        //Adds values to select dropdown
        function addModulesToList(module, type: string) {
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

        //Sort modules alphabetically
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

        this.codeMirrorEvents($form);
    }
    //----------------------------------------------------------------------------------------------
    events($form) {
        //Load preview on click
        $form.on('click', '[data-type="tab"][data-caption="Preview"]', e => {
            //if ($form.attr('data-propertieschanged') !== "true") {
            this.renderTab($form, 'Preview');
            //}
        });

        //Load Design Tab
        $form.on('click', '[data-type="tab"][data-caption="Designer"]', e => {
            this.renderTab($form, 'Designer');
        });

        //Refreshes and shows CodeMirror upon clicking HTML tab
        $form.on('click', '[data-type="tab"][data-caption="HTML"]', e => {
            this.codeMirror.refresh();
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
                    div.css('min-height', '50px');
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
            let $gridControl = jQuery(jQuery(`#tmpl-grids-${gridName}Browse`).html());
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
            } else if (type === 'Form') {
                $form.find('.addColumn[data-type="btnmenu"]')
                    .css({ 'width': '177px', 'margin-left': '27%' })
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
                let datafieldOptions = $form.find('#controlProperties .propval .datafields');
                for (let z = 0; z < datafieldOptions.length; z++) {
                    let field = jQuery(datafieldOptions[z]);
                    field.append(`<option value="" disabled>Select field</option>`)
                    for (let i = 0; i < self.datafields.length; i++) {
                        let $this = self.datafields[i];
                        field.append(`<option data-iscustomfield=${$this.IsCustom} value="${$this.Field}" data-type="${$this.FieldType}">${$this.Field}</option>`);
                    }
                    let value = jQuery(field).attr('value');
                    if (value) {
                        jQuery(field).find(`option[value=${value}]`).prop('selected', true);
                    } else {
                        jQuery(field).find(`option[disabled]`).prop('selected', true);
                    };
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
                `   <div class="addproperties" style="width:100%; display:flex;">
                        <div class="addpropname" style="border:.5px solid #efefef; width:50%; float:left; font-size:.9em;"><input placeholder="Add new property"></div>
                        <div class="addpropval" style="border:.5px solid #efefef; width:50%; float:left; font-size:.9em;"><input placeholder="Add value"></div>
                    </div>
                 </div>`; //closing div for propertyContainer

            let deleteComponentHtml = '<div class="fwformcontrol deleteObject" data-type="button" style="margin-left:27%; margin-top:15px;">Delete Component</div>';

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
                    .on('dragover','td.placeholder', e => {
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
                        $thisContainer.css('min-height:50px;');
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
                    .off('dragover', 'div.fwformfield')
                    .on('dragover', 'div.fwformfield', e => {
                        e.preventDefault();
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
                                $parent.css('min-height', '50px');
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
                        let properties = e.currentTarget.attributes;
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
                                    html.push(`<div class="properties">
                                      <div class="propname">${name === "" ? "&#160;" : name}</div>
                                      <div class="propval"><select style="width:94%" class="datafields" value="${value}"></select></div>
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
                                      <div class="propval"><select style="width:94%" class="valueOptions" value="${value}"></select></div>
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

                        //delete object
                        $form.find('#controlProperties').append(deleteComponentHtml);

                        //disables grids and browses in forms
                        if (type === 'Form') {
                            let isGrid = jQuery(originalHtml).parents('[data-type="Grid"]');
                            if (isGrid.length !== 0) {
                                $form.find('#controlProperties .propval >').attr('disabled', 'disabled');
                                $form.find('#controlProperties .addproperties, #controlProperties .deleteObject').remove();
                            }
                        }
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
                                    jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`${attribute}`, `${value}`);
                                    jQuery(originalHtml).attr(`${attribute}`, `${value}`);
                                    //update caption and datatypes
                                    let datatype = $form.find(`option[value=${value}]`).attr('data-type');
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
                                    jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`data-caption`, `${value}`);
                                    jQuery(originalHtml).attr(`data-caption`, `${value}`);
                                    $form.find(`#controlProperties .propname:contains('data-caption')`).siblings('.propval').find('input').val(value);
                                    jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`data-datatype`, datatype);
                                    jQuery(originalHtml).attr(`data-datatype`, datatype);
                                    $form.find(`#controlProperties .propname:contains('data-datatype')`).siblings('.propval').find('select').val(datatype);

                                    break;
                                default:
                                    jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`${attribute}`, `${value}`);
                                    jQuery(originalHtml).attr(`${attribute}`, `${value}`);
                            }
                        } else if (type === 'Form') {
                            if (attribute === 'data-datafield') {
                                isCustomField = $form.find(`option[value=${value}]`).attr('data-iscustomfield');

                                //update caption when datafield is changed
                                jQuery(originalHtml).attr('data-caption', value);
                                $form.find(`#controlProperties .propname:contains('data-caption')`).siblings('.propval').find('input').val(value);

                                if (isCustomField === "true") {
                                    //update datatype
                                    let datatype = $form.find(`option[value=${value}]`).attr('data-type');
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

                                    jQuery(originalHtml).attr('data-customfield', 'true');
                                    jQuery($customFormClone).find(`div[data-index="${index}"]`).attr('data-customfield', 'true');
                                }
                                jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`data-caption`, `${value}`);
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
                        case 'Form': let a = 0;
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
                        } else {
                            if ($element.siblings().length === 0 && $element.parent().hasClass('flexrow' || 'flexcolumn')) {
                                $element.parent().css('min-height', '50px');
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
                        fields = ['data-datafield', 'data-datatype', 'data-sort', 'data-width', 'data-visible', 'data-caption', 'class'];
                        for (let i = 0; i < fields.length; i++) {
                            var value;
                            var field = fields[i];
                            switch (field) {
                                case 'data-datafield':
                                    value = ""
                                    break;
                                case 'data-datatype':
                                    value = "text"
                                    break;
                                case 'data-sort':
                                    value = "off"
                                    break;
                                case 'data-width':
                                    value = "100px"
                                    break;
                                case 'data-visible':
                                    value = "true"
                                    break;
                                case 'data-caption':
                                    value = "New Column"
                                    break;
                                case 'class':
                                    value = 'field';
                            }
                            propertyHtml.push(
                                `<div class="properties">
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
                            .append(propertyHtml.join(''), deleteComponentHtml)
                            .find('.properties:even')
                            .css('background-color', '#f7f7f7');

                        //replace input field with select
                        $form.find('#controlProperties .propname:contains("data-datafield")')
                            .siblings('.propval')
                            .find('input')
                            .replaceWith(`<select style="width:94%" class="datafields" value="">`);

                        addDatafields();

                        $form.find('#controlProperties .propname:contains("data-datatype")')
                            .siblings('.propval')
                            .find('input')
                            .replaceWith(`<select style="width:94%" class="valueOptions" value="text">`);

                        addValueOptions();

                        $form.find('#controlProperties input').change();

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
                            var value;
                            var field = fields[i];
                            switch (field) {
                                case 'data-datafield':
                                    value = ""
                                    break;
                                case 'data-control':
                                    value = "FwFormField"
                                    break;
                                case 'data-type':
                                    value = "text"
                                    break;
                                case 'data-caption':
                                    value = "New Field"
                                    break;
                                case 'class':
                                    value = 'fwcontrol fwformfield';
                            }
                            propertyHtml.push(
                                `<div class="properties">
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
                            .append(propertyHtml.join(''), deleteComponentHtml)
                            .find('.properties:even')
                            .css('background-color', '#f7f7f7');

                        //replace input field with select
                        $form.find('#controlProperties .propname:contains("data-datafield")')
                            .siblings('.propval')
                            .find('input')
                            .replaceWith(`<select style="width:94%" class="datafields" value="">`);

                        addDatafields();

                        $form.find('#controlProperties .propname:contains("data-type")')
                            .siblings('.propval')
                            .find('input')
                            .replaceWith(`<select style="width:94%" class="valueOptions" value="text">`);

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
                    //closes the menu w/ event listener add when creating button menu
                    jQuery(document).trigger('click');

                    e.stopPropagation();
                    let $tabpage = $customForm.find('[data-type="tabpage"]:visible');
                    let tabpageIndex = $tabpage.attr('data-index');

                    let newContainerIndex = lastIndex + 1;
                    let html: any = [];
                    html.push(`<div class="emptyContainer" data-index="${newContainerIndex}"></div>`);

                    originalHtml = jQuery(html.join(''));

                    //build properties column
                    let propertyHtml: any = [];
                    let fields: any = [];

                    propertyHtml.push(propertyContainerHtml);
                    fields = ['class', 'style'];
                    for (let i = 0; i < fields.length; i++) {
                        var value;
                        var field = fields[i];
                        switch (field) {
                            case 'style':
                                value = 'min-height:50px';
                                break;
                            case 'class':
                                value = 'flexrow';
                                break;
                        }
                        propertyHtml.push(
                            `<div class="properties">
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
                    //closes the menu w/ event listener add when creating button menu
                    jQuery(document).trigger('click');

                    let newIndex = lastIndex + 1;
                    let $tabControl = $customForm.find('[data-control="FwTabs"]');
                    let newTabIds = FwTabs.addTab($tabControl, 'New Tab', '', '', true); //contains tabid and tabpageid

                    originalHtml = $customForm.find(`#${newTabIds.tabid}`);
                    originalHtml.attr('data-index', newIndex);

                    originalHtml.click();

                    let html: any = [];
                    html.push(`<div class="flexrow emptyContainer" data-index="${++newIndex}" style="min-height:50px;"></div>`);

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
                });


        }
    }
    getValueOptions(fieldname: string) {
        var values: any = [];
        switch (fieldname) {
            case 'data-browsedatatype':
            case 'data-formdatatype':
            case 'data-datatype':
            case 'data-type':
                values = [
                    'checkbox'
                    , 'checkboxlist'
                    , 'color'
                    , 'combobox'
                    , 'date'
                    , 'datetime'
                    , 'decimal'
                    , 'email'
                    , 'key'
                    , 'money'
                    , 'note'
                    , 'number'
                    , 'percent'
                    , 'phone'
                    , 'radio'
                    , 'searchbox'
                    , 'select'
                    , 'tab'
                    , 'text'
                    , 'textarea'
                    , 'time'
                    , 'timepicker'
                    , 'validation'
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