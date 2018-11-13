routes.push({ pattern: /^module\/customform$/, action: function (match: RegExpExecArray) { return CustomFormController.getModuleScreen(); } });
class CustomForm {
    Module: string = 'CustomForm';
    apiurl: string = 'api/v1/customform';
    caption: string = 'Custom Forms';
    nav: string = 'module/customform';
    id: string = 'CB2EF8FF-2E8D-4AD0-B880-07037B839C5E';
    codeMirror: any;
    doc: any;
    datafields: string[];
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
                let request: any = {};
                request = {
                    module: moduleNav,
                    top: 1
                };
                if (apiurl !== "undefined") {
                    FwAppData.apiMethod(true, 'POST', `${apiurl}/browse`, request, FwServices.defaultTimeout, function onSuccess(response) {
                        let columnNames = response.Columns;
                        columnNames = columnNames.map(obj => {
                            return obj.DataField;
                        })
                        columnNames = columnNames.sort(compare);
                        self.datafields = columnNames;
                        for (let i = 0; i < columnNames.length; i++) {
                            modulefields.append(`${columnNames[i]}<br />`);
                        }
                    }, null, $form);
                }
                break;
            case 'Form':
                FwAppData.apiMethod(true, 'GET', `${apiurl}/emptyobject`, null, FwServices.defaultTimeout, function onSuccess(response) {
                    let columnNames = Object.keys(response);
                    let customFields = response._Custom.map(obj => obj.FieldName);
                    self.datafields = columnNames.concat(customFields).sort(compare);

                    for (let i = 0; i < customFields.length; i++) {
                        columnNames.push(`${customFields[i]} [Custom]`);
                    }
                    columnNames = columnNames.sort(compare);

                    for (let i = 0; i < columnNames.length; i++) {
                        if (columnNames[i] != 'DateStamp' && columnNames[i] != 'RecordTitle' && columnNames[i] != '_Custom') {
                            modulefields.append(`
                                <div data-iscustomfield=${customFields.indexOf(columnNames[i]) === -1 ? false : true}>
                                ${columnNames[i]}</div>
                                `);
                        }
                    }
                }, null, $form);
                break;
        }

        function compare(a, b) {
            if (a < b)
                return -1;
            if (a > b)
                return 1;
            return 0;
        }
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
            $customForm.find('tr.fieldnames .column >').off('click');
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
                $form.find('.addColumn').show();
                showHiddenColumns($customForm);
            } else {
                $form.find('.addColumn').hide();
            };

            //updates information for HTML tab
            function updateHtml() {
                let $modifiedClone = $customFormClone.cloneNode(true);
                jQuery($modifiedClone).find('div').removeAttr('data-index');
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
                        if (self.datafields[i] !== '_Custom') {
                            field.append(`<option value="${self.datafields[i]}">${self.datafields[i]}</option>`);
                        }
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

            let deleteComponentHtml = '<div class="fwformcontrol deleteObject" data-type="button" style="margin-left:27%; margin-top:15px;">Delete Component</div>'

            //drag and drop
            if (type === 'Grid' || type === 'Browse') {
                let index;
                let indexDrag;
                let indexDrop;
                let $elementDragged;
                let $draggableElements;
                let $dragContainer = $customForm.find('tr.fieldnames');
                $draggableElements = $dragContainer.find('td.column:not(.tdselectrow):not(.browsecontextmenucell)');
                $draggableElements.attr('draggable', 'true');
                $dragContainer
                    .off('dragstart', 'td.column:not(.tdselectrow):not(.browsecontextmenucell)')
                    .on('dragstart', 'td.column:not(.tdselectrow):not(.browsecontextmenucell)', e => {
                        index = jQuery(e.currentTarget).children('.field').attr('data-index');
                        e.originalEvent.dataTransfer.setData("index", index);
                        $elementDragged = $draggableElements
                            .find('.field')
                            .filter(function () {
                                return jQuery(this).attr("data-index") === index;
                            });
                    })
                    .off('dragenter', 'td.column:not(.tdselectrow):not(.browsecontextmenucell)')
                    .on('dragenter', 'td.column:not(.tdselectrow):not(.browsecontextmenucell)', e => {
                        let $this = jQuery(e.currentTarget);
                        indexDrag = $elementDragged.parents('td').index();
                        indexDrop = $this.index();
                        if (indexDrag > indexDrop) {
                            e.currentTarget.style.borderLeft = "2px dashed gray";
                        } else if (indexDrag < indexDrop) {
                            e.currentTarget.style.borderRight = "2px dashed gray";
                        } else if (indexDrag == indexDrop) {
                            e.currentTarget.style.borderRight = "2px dashed gray";
                            e.currentTarget.style.borderLeft = "2px dashed gray";
                        }
                    })
                    .off('dragleave', 'td.column:not(.tdselectrow):not(.browsecontextmenucell)')
                    .on('dragleave', 'td.column:not(.tdselectrow):not(.browsecontextmenucell)', e => {
                        e.currentTarget.style.borderLeft = "";
                        e.currentTarget.style.borderRight = "";
                    })
                    .off('dragover', 'td.column:not(.tdselectrow):not(.browsecontextmenucell)')
                    .on('dragover', 'td.column:not(.tdselectrow):not(.browsecontextmenucell)', e => {
                        e.preventDefault();
                    })
                    .off('drop', 'td.column:not(.tdselectrow):not(.browsecontextmenucell)')
                    .on('drop', 'td.column:not(.tdselectrow):not(.browsecontextmenucell)', e => {
                        let $this = jQuery(e.currentTarget);
                        let index = e.originalEvent.dataTransfer.getData("index");
                        let dropTargetHtmlIndex = $this.children('.field').attr('data-index');
                        if (index !== dropTargetHtmlIndex) {
                            let indexDrag = $elementDragged.parents('td').index();
                            let indexDrop = $this.index();

                            //for updating the formfield and codemirror
                            let firstColumn = jQuery($customFormClone).find(`[data-index="${index}"]`).parent();
                            let secondColumn = jQuery($customFormClone).find(`[data-index="${dropTargetHtmlIndex}"]`).parent();

                            if (indexDrag < indexDrop) {
                                $elementDragged.parents('td').insertAfter($this);
                                firstColumn.insertAfter(secondColumn);

                            } else if (indexDrag > indexDrop) {
                                $elementDragged.parents('td').insertBefore($this);
                                firstColumn.insertBefore(secondColumn);
                            }
                            updateHtml();
                        }
                        e.currentTarget.style.borderRight = "";
                        e.currentTarget.style.borderLeft = "";
                    });
            } else if (type === 'Form') {
                let index;
                let indexDrag;
                let indexDrop;
                let $elementDragged;
                let $draggableElements;
                let placeholderIndex;
                let $dragContainer = $customForm.find('div.flexpage');
                $draggableElements = $dragContainer.find('div.fwformfield');
                $draggableElements.attr('draggable', 'true');
                $dragContainer
                    .off('dragstart', 'div.fwformfield')
                    .on('dragstart', 'div.fwformfield', e => {
                        index = jQuery(e.currentTarget).attr('data-index');
                        e.originalEvent.dataTransfer.setData("index", index);
                        $elementDragged = $draggableElements
                            .filter(function () {
                                return jQuery(this).attr("data-index") === index;
                            });
                    })
                    .off('dragenter', 'div.fwformfield')
                    .on('dragenter', 'div.fwformfield', e => {
                        let $this = jQuery(e.currentTarget);
                        //where it is doesn't matter, just insertBefore for all?  cases for the first and last items.. insert before or after?
                        indexDrag = $elementDragged.index();
                        indexDrop = $this.index();
                        $dragContainer.find('div.placeholder').remove();
                        if (indexDrag !== indexDrop) {
                            let $preview = jQuery(`<div class="placeholder" style="min-height:50px; line-height:50px; vertical-align:middle; font-weight:bold; text-align:center; flex:1 1 100px; border: 2px dashed #4caf50">Drop here</div>`);
                            placeholderIndex = $this.attr('data-index');
                            if (indexDrag > indexDrop) {
                                $preview.insertBefore($this);
                            } else if (indexDrag < indexDrop) {
                                $preview.insertAfter($this);
                            }
                        }
                    })
                    .off('dragleave', 'div.fwformfield')
                    .on('dragleave', 'div.fwformfield', e => {
                    })
                    .off('dragover', 'div.fwformfield')
                    .on('dragover', 'div.fwformfield', e => {
                        e.preventDefault();
                    })
                    .off('drop', 'div.placeholder')
                    .on('drop', 'div.placeholder', e => {
                        let $this = jQuery(e.currentTarget);
                        let index = e.originalEvent.dataTransfer.getData("index");
                        //for updating the formfield and codemirror
                        let firstColumn = jQuery($customFormClone).find(`[data-index="${index}"]`);
                        let secondColumn = jQuery($customFormClone).find(`[data-index="${placeholderIndex}"]`);

                        $this.replaceWith($elementDragged);

                        if (indexDrag > indexDrop) {
                            firstColumn.insertAfter(secondColumn);
                        } else if (indexDrag < indexDrop) {
                            firstColumn.insertBefore(secondColumn);
                        }
                        updateHtml();
                        e.currentTarget.style.border = "";
                    })
                    .off('drop', 'div.fwformfield')
                    .on('drop', 'div.fwformfield', e => {
                        $dragContainer.find('div.placeholder').remove();
                    });
            }

            $customForm
                //build properties section
                .off('click')
                .on('click',
                    '[data-control="FwGrid"], [data-type="Browse"] thead tr.fieldnames .column >, [data-type="Grid"] thead tr.fieldnames .column >, [data-control="FwContainer"], [data-control="FwFormField"], div.flexrow, div.flexcolumn',
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

                            let a = 0;
                            a += (name == "data-originalvalue") ? 1 : 0;
                            a += (name == "data-index") ? 1 : 0;
                            a += (name == "data-rendermode") ? 1 : 0;
                            a += (name == "data-version") ? 1 : 0;
                            a += (name == "draggable") ? 1 : 0;
                            a += (name == "data-noduplicate") ? 1 : 0;
                            if (type === 'Browse') {
                                a += (name == "data-formdatafield") ? 1 : 0;
                                a += (name == "data-cssclass") ? 1 : 0;
                            }

                            if (a) {
                                continue;
                            }

                            value = value.replace('focused', '');
                            let b = 0;
                            b += (name == "data-datafield") ? 1 : 0;
                            b += (name == "data-browsedatafield") ? 1 : 0;
                            b += (name == "data-displayfield") ? 1 : 0;
                            b += (name == "data-browsedisplayfield") ? 1 : 0;

                            let c = 0;
                            c += (name == "data-browsedatatype") ? 1 : 0;
                            c += (name == "data-formdatatype") ? 1 : 0;
                            c += (name == "data-datatype") ? 1 : 0;
                            c += (name == "data-sort") ? 1 : 0;
                            c += (name == "data-visible") ? 1 : 0;
                            c += (name == "data-formreadonly") ? 1 : 0;
                            c += (name == "data-isuniqueid") ? 1 : 0;

                            if (b) {
                                html.push(`<div class="properties">
                                      <div class="propname">${name === "" ? "&#160;" : name}</div>
                                      <div class="propval"><select style="width:94%" class="datafields" value="${value}"></select></div>
                                   </div>
                                  `);
                            } else if (c) {
                                html.push(`<div class="properties">
                                      <div class="propname">${name === "" ? "&#160;" : name}</div>
                                      <div class="propval"><select style="width:94%" class="valueOptions" value="${value}"></select></div>
                                   </div>
                                  `);
                            } else {
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
                            if (isGrid.length !== 0 /*|| controlType === 'FwGrid'*/) {
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
                    let attribute = jQuery(e.currentTarget).siblings('.propname').text();
                    let value;
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
                                case 'data-browsedatafield':
                                    jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`data-datafield`, `${value}`);
                                    jQuery(originalHtml).attr(`${attribute}`, `${value}`);
                                    break;
                                default:
                                    jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`${attribute}`, `${value}`);
                                    jQuery(originalHtml).attr(`${attribute}`, `${value}`);
                            }
                        } else {
                            jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`${attribute}`, `${value}`);
                            jQuery(originalHtml).attr(`${attribute}`, `${value}`);
                        }
                    } else {
                        jQuery(e.currentTarget).parents('.properties').hide(); //.remove() triggers the if statement to run again (not sure why)
                        jQuery($customFormClone).find(`div[data-index="${index}"]`).removeAttr(`${attribute}`);
                        jQuery(originalHtml).removeAttr(`${attribute}`);
                        //FwConfirmation.renderConfirmation('Remove Property', 'Remove property?');
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
                        $form.find('#controlProperties .addproperties').before(html.join(''));

                        jQuery(originalHtml).attr(`${newProp}`, `${newPropVal}`);
                        jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`${newProp}`, `${newPropVal}`);
                        jQuery(e.currentTarget).siblings('.addpropname').find('input').val('');
                        jQuery(e.currentTarget).find('input').val('');

                        updateHtml();
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
                        } else {
                            if ($element.siblings().length === 0 && $element.parent().hasClass('flexrow' || 'flexcolumn')) {
                                $element.parent().css('min-height', '50px');
                            }
                            $elementClone.remove();
                            $element.remove();
                        }
                        $form.find('#controlProperties').empty();
                        updateHtml();
                    });
                })
                .off('click', '.addColumn')
                .on('click', '.addColumn', e => {
                    let $control = jQuery($customFormClone).find(`[data-type="${type}"]`);
                    let lastIndex = Number($control.find('div:last').attr('data-index'));
                    let hasSpacer = $control.find('div:last').hasClass('spacer');
                    //build column base
                    let html: any = [];
                    html.push
                        (`<div class="column" data-index="${++lastIndex}">
    <div class="field" data-index="${++lastIndex}"></div>
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

                    $form.find('#controlProperties input').change();
                });
        }
    }
    getValueOptions(fieldname) {
        var values: any = [];
        switch (fieldname) {
            case 'data-browsedatatype':
            case 'data-formdatatype':
            case 'data-datatype':
                values = [
                    'checkbox'
                    , 'checkboxlist'
                    , 'color'
                    , 'combobox'
                    , 'date'
                    , 'datetime'
                    , 'email'
                    , 'key'
                    , 'money'
                    , 'number'
                    , 'percent'
                    , 'phone'
                    , 'radio'
                    , 'searchbox'
                    , 'select'
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