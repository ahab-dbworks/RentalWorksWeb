class CustomReportLayout {
    Module: string = 'CustomReportLayout';
    apiurl: string = 'api/v1/customreportlayout';
    caption: string = Constants.Modules.Administrator.children.CustomReportLayout.caption;
    nav: string = Constants.Modules.Administrator.children.CustomReportLayout.nav;
    id: string = Constants.Modules.Administrator.children.CustomReportLayout.id;
    codeMirror: any;
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: { datafield: string, search: string }) {
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

        const webUsersId = JSON.parse(sessionStorage.getItem('userid')).webusersid;
        FwFormField.setValueByDataField($form, 'WebUserId', webUsersId);

        if (mode == 'NEW') {
            FwFormField.enable($form.find('[data-datafield="BaseReport"]'));
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
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="CustomReportLayoutId"] input').val(uniqueids.CustomReportLayoutId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        $form.find('#codeEditor').change();

        //for retaining position in code editor after saving
        $form.find('[data-datafield="Html"]').addClass('reload');

        //const $customForm = $form.find(`#designerContent`);
        //const $fields = $customForm.find('.fwformfield');
        //let hasDuplicates: boolean = false;
        //$fields.each(function (i, e) {
        //    const $fwFormField = jQuery(e);
        //    const dataField = $fwFormField.attr('data-datafield');
        //    if (dataField != "") {
        //        const $fieldFound = $customForm.find(`[data-datafield="${dataField}"][data-enabled="true"]`);
        //        if ($fieldFound.length > 1) {
        //            $fieldFound.addClass('error');
        //            hasDuplicates = true;
        //            FwNotification.renderNotification('ERROR', 'Only one duplicate field can be active on a form.  Set the data-enabled property to false on duplicates.');
        //            return false;
        //        } else {
        //            $customForm.find(`[data-datafield="${dataField}"]`).removeClass('error');
        //        }
        //    }
        //})

        //if (!hasDuplicates) FwModule.saveForm(this.Module, $form, parameters);
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    afterSave($form: any) {
        FwFormField.disable($form.find('[data-datafield="BaseReport"]'));
        $form.attr('data-modified', 'false');
        $form.find('.btn[data-type="SaveMenuBarButton"]').addClass('disabled');
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        //toggles "Assign To" grids
        const assignTo = FwFormField.getValueByDataField($form, 'AssignTo');
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

        //Loads html for code editor
        if (!$form.find('[data-datafield="Html"]').hasClass('reload')) {
            const html = $form.find('[data-datafield="Html"] textarea').val();
            if (typeof html !== 'undefined') {
                this.codeMirror.setValue(html);
            } else {
                this.codeMirror.setValue('');
            }
        }

        const reportName: any = FwFormField.getValueByDataField($form, 'BaseReport');
        this.addValidFields($form, reportName);
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
        const textArea = $form.find('#codeEditor').get(0);
        var codeMirror = CodeMirror.fromTextArea(textArea,
            {
                mode: "xml"
                , lineNumbers: true
                , foldGutter: true
                , gutters: ["CodeMirror-linenumbers", "CodeMirror-foldgutter"]
            });
        this.codeMirror = codeMirror;

        //Select module event
        $form.find('div.modules').on('change', e => {
            let $this = $form.find('[data-datafield="BaseReport"] option:selected');
            let modulehtml;
            const reportName = $this.val();
            if (reportName.length) {
                FwAppData.apiMethod(true, 'GET', `api/v1/customreportlayout/template/${reportName}`, null, FwServices.defaultTimeout,
                    response => {
                        //get the html from the template and set it as codemirror's value
                        modulehtml = response.ReportTemplate;
                        if (typeof modulehtml !== "undefined") {
                            codeMirror.setValue(modulehtml);
                        }
                        this.renderTab($form, 'Designer');
                    }, ex => FwFunc.showError(ex), $form);
                this.addValidFields($form, reportName);
            } else {
                $form.find('.modulefields').empty();
                codeMirror.setValue('');
            }
        });

        //Updates value for form fields
        $form.find('#codeEditor').on('change', e => {
            codeMirror.save();
            const html = $form.find('textarea#codeEditor').val();
            FwFormField.setValueByDataField($form, 'Html', html);
        });
    }
    //----------------------------------------------------------------------------------------------
    addValidFields($form, reportName) {
        //Get valid field names and sort them
        const modulefields = $form.find('.modulefields');
        modulefields.empty();
        FwAppData.apiMethod(true, 'GET', `api/v1/${reportName}/emptyobject`, null, FwServices.defaultTimeout,
            response => {
                let customFields = response._Custom.map(obj => ({ fieldname: obj.FieldName, fieldtype: obj.FieldType }));
                let allValidFields: any = [];
                for (let key of Object.keys(response)) {
                    if (key != 'DateStamp' && key != 'RecordTitle' && key != '_Custom' && key != '_Fields') {
                        if (Array.isArray(response[key])) {
                            const unorderedItems = response[key][0];
                            const orderedItems = {};
                            Object.keys(unorderedItems).sort().forEach(key => {
                                orderedItems[key] = unorderedItems[key];
                            });

                            allValidFields.push({
                                'Field': key
                                , 'IsCustom': 'false'
                                , 'NestedItems': orderedItems
                            });
                        } else {
                            allValidFields.push({
                                'Field': key
                                , 'IsCustom': 'false'
                            });
                        }
                    }
                }

                for (let i = 0; i < customFields.length; i++) {
                    allValidFields.push({
                        'Field': customFields[i].fieldname
                        , 'IsCustom': 'true'
                        , 'FieldType': customFields[i].fieldtype.toLowerCase()
                    });
                }

                $form.data('validdatafields', allValidFields.sort((a, b) => a.Field < b.Field ? -1 : 1));
                for (let i = 0; i < allValidFields.length; i++) {
                    modulefields.append(`<div data-iscustomfield=${allValidFields[i].IsCustom}>${allValidFields[i].Field}</div>`);
                    if (allValidFields[i].hasOwnProperty("NestedItems")) {
                        for (const key of Object.keys(allValidFields[i].NestedItems)) {
                            if (key != '_Custom') {
                                modulefields.append(`<div data-iscustomfield="false" data-isnested="true" data-parentfield="${allValidFields[i].Field}" style="text-indent:1em;">${key}</div>`);
                            }
                        }
                    }
                }
            }, ex => FwFunc.showError(ex), $form);
    }
    //----------------------------------------------------------------------------------------------
    //addButtonMenu($form) {
    //    let $buttonmenu = $form.find('.addColumn[data-type="btnmenu"]');
    //    let $addContainer = FwMenu.generateButtonMenuOption('ADD NEW CONTAINER')
    //        , $addTab = FwMenu.generateButtonMenuOption('ADD NEW TAB');

    //    let menuOptions = [];
    //    menuOptions.push($addContainer, $addTab);

    //    FwMenu.addButtonMenuOptions($buttonmenu, menuOptions);
    //}
    //----------------------------------------------------------------------------------------------
    loadModules($form) {
        let $moduleSelect = $form.find('.modules');

        const reports = FwApplicationTree.getAllReports(false, false, (modules: any[], moduleCaption: string, moduleName: string, category: string, currentNode: any, nodeModule: IGroupSecurityNode, hasView: boolean, hasNew: boolean, hasEdit: boolean, moduleController: any) => {
            if (moduleController.hasOwnProperty('apiurl')) {
                modules.push({ value: moduleName, text: moduleCaption, apiurl: moduleController.apiurl });
            }
        });

        FwApplicationTree.sortModules(reports);
        FwFormField.loadItems($moduleSelect, reports);

        this.codeMirrorEvents($form);
    }
    //----------------------------------------------------------------------------------------------
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
            request.CustomReportLayoutId = FwFormField.getValueByDataField($form, 'CustomReportLayoutId')
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
            request.CustomReportLayoutId = FwFormField.getValueByDataField($form, 'CustomReportLayoutId')
        });
        FwBrowse.init($customReportLayoutUserGridControl);
        FwBrowse.renderRuntimeHtml($customReportLayoutUserGridControl);
    }
    //----------------------------------------------------------------------------------------------
    events($form) {
        //Load preview on click
        //$form.on('click', '[data-type="tab"][data-caption="Preview"]', e => {
        //    this.renderTab($form, 'Preview');
        //});

        //Load Design Tab
        //$form.on('click', '[data-type="tab"][data-caption="Designer"]', e => {
        //    this.renderTab($form, 'Designer');
        //});

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

        //add field on click
        $form.on('click', '.modulefields div', e => {
            const $this = jQuery(e.currentTarget);
            let textToInject;
            if ($this.attr('data-isnested') != 'true') {
                textToInject = `{{${$this.text()}}}`;
            } else {
                textToInject = `{{${$this.attr('data-parentfield')}.${$this.text()}}}`; //for nested objects
            }
            const doc = this.codeMirror.getDoc();
            const cursor = doc.getCursor();
            doc.replaceRange(textToInject, cursor);
            $form.find('#codeEditor').change();
        });
    }
    //----------------------------------------------------------------------------------------------
    renderTab($form, tabName: string) {
        $form.find('#codeEditor').change();     // 10/25/2018 Jason H - updates the textarea formfield with the code editor html

        let html = FwFormField.getValueByDataField($form, 'Html');
        const $table = jQuery(html).find('table');
        $form.find(`#reportDesigner`).empty().append($table);
   
        //create sortable for headers
        Sortable.create($table.find('#columnHeader tr').get(0), {
            //onStart: e => {

            //},
            onEnd: e => {
                //removed for now due to handlebars being displaced (handlebars only work as attributes or in tds and are not valid in a table structure on the same level as theads/tbody)
                //to-do: try a global replace in the html string for the handlebar characters

                //let $wrapper = jQuery('<div class="custom-report-wrapper"></div>');         //create a wrapper      -- Jason H. 04/16/20
                //$wrapper.append(html);                                                      //append the original HTML to the wrapper.  this is done to combine the loose elements.
                //const newHeaderHTML = e.currentTarget.innerHTML;                            //get the new header HTML
                //$wrapper.find('table #columnHeader tr').html(newHeaderHTML);                //replace old headers
                //const newReportLayoutHTML = $wrapper.get(0).innerHTML;                      //get new report HTML
                //FwFormField.setValueByDataField($form, 'Html', newReportLayoutHTML);
                //this.codeMirror.setValue(newReportLayoutHTML);                              //update codemirror (HTML tab) with new HTML

                $form.attr('data-modified', 'true');
                $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
            }
        });

        ////render forms
        //let $customForm = $form.find(`#${renderFormHere}`);
        //let $customFormClone;

        ////add indexes for all divs
        //if (tabName == 'Designer') {
        //    let $divs = $customForm.find('div');
        //    for (let i = 0; i < $divs.length; i++) {
        //        let div = jQuery($divs[i]);
        //        div.attr('data-index', i);
        //    }
        //    $customFormClone = $customForm.get(0).cloneNode(true);
        //}

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
        addDatafields();
        //            //limit values that can be selected for certain fields
        //            function addValueOptions() {
        //                let addOptionsHere = $form.find('#controlProperties .propval .valueOptions');
        //                for (let z = 0; z < addOptionsHere.length; z++) {
        //                    let $this = jQuery(addOptionsHere[z]);
        //                    let fieldName = jQuery(addOptionsHere[z]).parents('.propval').siblings('.propname').text();
        //                    let valueOptions = self.getValueOptions(fieldName);

        //                    $this.append(`<option value="" disabled>Select value</option>`);
        //                    for (let i = 0; i < valueOptions.length; i++) {
        //                        $this.append(`<option value="${valueOptions[i]}">${valueOptions[i]}</option>`);
        //                    }
        //                    let value = jQuery($this).attr('value');
        //                    if (value) {
        //                        jQuery($this).find(`option[value=${value}]`).prop('selected', true);
        //                    } else {
        //                        jQuery($this).find(`option[disabled]`).prop('selected', true);
        //                    };
        //                }
        //            }

        //            let propertyContainerHtml =
        //                `<div class="propertyContainer" style="border: 1px solid #bbbbbb; word-break: break-word;">
        //                     <div style="text-indent:5px;">
        //                         <div style="font-weight:bold; background-color:#dcdcdc; width:50%; float:left;">Name</div>
        //                         <div style="font-weight:bold; background-color:#dcdcdc; width:50%; float:left;">Value</div>
        //                     </div>
        //                        `;

        //            let addPropertiesHtml =
        //                `   <div class="addproperties" style="width:100%; display:flex;">
        //                        <div class="addpropname" style="padding:3px; border:.5px solid #efefef; width:50%; float:left; font-size:.9em;"><input placeholder="Add new property"></div>
        //                        <div class="addpropval" style="padding:3px; border:.5px solid #efefef; width:50%; float:left; font-size:.9em;"><input placeholder="Add value"></div>
        //                    </div>
        //                 </div>`; //closing div for propertyContainer

        //            $customForm
        //                //build properties section
        //                .off('click')
        //                .on('click',
        //                    '[data-control="FwGrid"], [data-type="Browse"] thead tr.fieldnames .column >, [data-type="Grid"] thead tr.fieldnames .column >, [data-control="FwContainer"], [data-control="FwFormField"], div.flexrow, div.flexcolumn, div[data-type="tab"]',
        //                    e => {
        //                        e.stopPropagation();
        //                        originalHtml = e.currentTarget;
        //                        controlType = jQuery(originalHtml).attr('data-control');
        //                        let properties = e.currentTarget.attributes;
        //                        let html: any = [];
        //                        html.push(propertyContainerHtml);
        //                        for (let i = 0; i < properties.length; i++) {
        //                            let value = properties[i].value;
        //                            let name = properties[i].name;

        //                            switch (name) {
        //                                case "data-originalvalue":
        //                                case "data-index":
        //                                case "data-rendermode":
        //                                case "data-version":
        //                                case "draggable":
        //                                case "data-noduplicate":
        //                                case "data-formdatafield":
        //                                case "data-cssclass":
        //                                case "data-mode":
        //                                case "data-tabtype":
        //                                case "data-customfield":
        //                                    continue;
        //                                case "data-datafield":
        //                                case "data-browsedatafield":
        //                                case "data-displayfield":
        //                                case "data-browsedisplayfield":
        //                                    html.push(`<div class="properties">
        //                                      <div class="propname">${name === "" ? "&#160;" : name}</div>
        //                                      <div class="propval"><select style="width:92%" class="datafields" value="${value}"></select></div>
        //                                   </div>
        //                                  `);
        //                                    break;
        //                                case "data-browsedatatype":
        //                                case "data-formdatatype":
        //                                case "data-datatype":
        //                                case "data-sort":
        //                                case "data-visible":
        //                                case "data-formreadonly":
        //                                case "data-isuniqueid":
        //                                case "data-type":
        //                                case "data-formrequired":
        //                                case "data-required":
        //                                case "data-enabled":
        //                                    html.push(`<div class="properties">
        //                                      <div class="propname">${name === "" ? "&#160;" : name}</div>
        //                                      <div class="propval"><select style="width:92%" class="valueOptions" value="${value}"></select></div>
        //                                   </div>
        //                                  `);
        //                                    break;
        //                                case "class":
        //                                    value = value.replace('focused', '');
        //                                default:
        //                                    html.push(`<div class="properties">
        //                                      <div class="propname">${name === "" ? "&#160;" : name}</div>
        //                                      <div class="propval"><input value="${value}"></div>
        //                                   </div>
        //                                  `);
        //                            }
        //                        }
        //                        html.push(addPropertiesHtml);
        //                        $form.find('#controlProperties')
        //                            .empty()
        //                            .append(html.join(''))
        //                            .find('.properties:even')
        //                            .css('background-color', '#f7f7f7');

        //                        $form.find('#controlProperties input').css('text-indent', '3px');

        //                        addValueOptions();

        //                        //delete object
        //                        $form.find('#controlProperties').append(deleteComponentHtml);

        //                        //disables grids and browses in forms
        //                        if (type === 'Form') {
        //                            let isGrid = jQuery(originalHtml).parents('[data-type="Grid"]');
        //                            if (isGrid.length !== 0) {
        //                                $form.find('#controlProperties .propval >').attr('disabled', 'disabled');
        //                                $form.find('#controlProperties .addproperties, #controlProperties .deleteObject').remove();
        //                            }
        //                        }
        //                    });

        //            $form
        //                //updates designer content with new attributes and updates code editor
        //                .off('change', '#controlProperties .propval')
        //                .on('change', '#controlProperties .propval', e => {
        //                    e.stopImmediatePropagation();
        //                    let value;
        //                    let isCustomField;
        //                    let attribute = jQuery(e.currentTarget).siblings('.propname').text();
        //                    let $this = jQuery(e.currentTarget);
        //                    if ($this.find('select').hasClass('datafields') || $this.find('select').hasClass('valueOptions')) {
        //                        value = jQuery(e.currentTarget).find('select').val();
        //                    } else {
        //                        value = jQuery(e.currentTarget).find('input').val();
        //                    }

        //                    let index = jQuery(originalHtml).attr('data-index');

        //                    if (value) {
        //                        if (type === 'Grid' || type === 'Browse') {
        //                            switch (attribute) {
        //                                case 'data-visible':
        //                                    if (value === 'false') {
        //                                        jQuery(originalHtml).parent('.column').attr('style', 'display:none;');
        //                                    } else {
        //                                        jQuery(originalHtml).parent('.column').removeAttr(`style`);
        //                                    }
        //                                    jQuery(originalHtml).attr(`${attribute}`, `${value}`);
        //                                    jQuery($customFormClone).find(`div[data-index="${index}"]`).parent('.column').attr(`${attribute}`, `${value}`);
        //                                    break;
        //                                case 'data-width':
        //                                    jQuery(originalHtml).find('.fieldcaption').attr(`style`, `min-width:${value}`);
        //                                    jQuery($customFormClone).find(`div[data-index="${index}"]`).parent('.column').attr(`${attribute}`, `${value}`);
        //                                    break;
        //                                case 'data-datafield':
        //                                case 'data-browsedatafield':
        //                                    jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`${attribute}`, `${value}`);
        //                                    jQuery(originalHtml).attr(`${attribute}`, `${value}`);

        //                                    isCustomField = $form.find(`option[value="${value}"]`).attr('data-iscustomfield');
        //                                    if (isCustomField === "true") {
        //                                        //update caption and datatypes
        //                                        let datatype = $form.find(`option[value="${value}"]`).attr('data-type');
        //                                        switch (datatype) {
        //                                            case 'integer':
        //                                                datatype = "number";
        //                                                break;
        //                                            case 'float':
        //                                                datatype = "decimal";
        //                                                break;
        //                                            case 'date':
        //                                                datatype = "date";
        //                                                break;
        //                                            case 'true/false':
        //                                                datatype = "checkbox";
        //                                                break;
        //                                            default:
        //                                                datatype = "text";
        //                                                break;
        //                                        }
        //                                        jQuery(originalHtml).attr('data-customfield', 'true');
        //                                        jQuery($customFormClone).find(`div[data-index="${index}"]`).attr('data-customfield', 'true');
        //                                        jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`data-caption`, `${value}`);
        //                                        jQuery(originalHtml).attr(`data-caption`, `${value}`);
        //                                        $form.find(`#controlProperties .propname:contains('data-caption')`).siblings('.propval').find('input').val(value);
        //                                        jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`data-datatype`, datatype);
        //                                        jQuery(originalHtml).attr(`data-datatype`, datatype);
        //                                        $form.find(`#controlProperties .propname:contains('data-datatype')`).siblings('.propval').find('select').val(datatype);
        //                                    }
        //                                    break;
        //                                default:
        //                                    jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`${attribute}`, `${value}`);
        //                                    jQuery(originalHtml).attr(`${attribute}`, `${value}`);
        //                            }
        //                        } else if (type === 'Form') {
        //                            if (attribute === 'data-datafield') {
        //                                isCustomField = $form.find(`option[value="${value}"]`).attr('data-iscustomfield');

        //                                //update caption when datafield is changed
        //                                jQuery(originalHtml).attr('data-caption', value);
        //                                jQuery(originalHtml).find(`.fwformfield-caption`).text(value);
        //                                $form.find(`#controlProperties .propname:contains('data-caption')`).siblings('.propval').find('input').val(value);

        //                                if (isCustomField === "true") {
        //                                    //update datatype
        //                                    let datatype = $form.find(`option[value="${value}"]`).attr('data-type');
        //                                    switch (datatype) {
        //                                        case 'integer':
        //                                            datatype = "number";
        //                                            break;
        //                                        case 'float':
        //                                            datatype = "decimal";
        //                                            break;
        //                                        case 'date':
        //                                            datatype = "date";
        //                                            break;
        //                                        case 'true/false':
        //                                            datatype = "checkbox";
        //                                            break;
        //                                        default:
        //                                            datatype = "text";
        //                                            break;
        //                                    }
        //                                    jQuery(originalHtml).attr('data-type', datatype);
        //                                    $form.find(`#controlProperties .propname:contains('data-type')`).siblings('.propval').find('select').val(datatype);
        //                                    jQuery($customFormClone).find(`div[data-index="${index}"]`).attr('data-type', datatype);
        //                                    jQuery(originalHtml).attr('data-customfield', 'true');
        //                                    jQuery($customFormClone).find(`div[data-index="${index}"]`).attr('data-customfield', 'true');
        //                                }
        //                                jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`data-caption`, `${value}`);
        //                            }

        //                            if (attribute === 'data-caption') {
        //                                jQuery(originalHtml).find(`.fwformfield-caption`).text(value);
        //                            }

        //                            let isTab = jQuery(originalHtml).attr('data-type');
        //                            if (isTab === "tab") {
        //                                //for changing tab captions
        //                                jQuery(originalHtml).find('.caption').text(value);
        //                            } else {
        //                                jQuery(originalHtml).attr(`${attribute}`, `${value}`);
        //                            };

        //                            jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`${attribute}`, `${value}`);

        //                        }
        //                    } else {
        //                        if (attribute !== "data-datafield") { //for adding new fields
        //                            jQuery(e.currentTarget).parents('.properties').hide();
        //                            jQuery($customFormClone).find(`div[data-index="${index}"]`).removeAttr(`${attribute}`);
        //                            jQuery(originalHtml).removeAttr(`${attribute}`);
        //                        } else {
        //                            jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`${attribute}`, `${value}`);
        //                            jQuery(originalHtml).attr(`${attribute}`, `${value}`);
        //                        }
        //                    }

        //                    switch (type) {
        //                        case 'Form': let a = 0;
        //                            a += (controlType == 'FwFormField') ? 1 : 0;
        //                            a += (controlType == 'FwContainer') ? 1 : 0;

        //                            if (a) {
        //                                FwControl.init(jQuery(originalHtml));
        //                                FwControl.renderRuntimeHtml(jQuery(originalHtml));
        //                            }
        //                            break;
        //                        case 'Browse':
        //                        case 'Grid':
        //                            let $control = $customFormClone.cloneNode(true);
        //                            $control = jQuery($control).find('.fwcontrol.fwbrowse');
        //                            $customForm
        //                                .empty()
        //                                .append($control);
        //                            if (type === 'Browse') {
        //                                FwControl.init($control);
        //                            }
        //                            FwControl.renderRuntimeHtml($control);
        //                            disableControls();
        //                            showHiddenColumns($control);
        //                            //have to reinitialize after adding a new column
        //                            $draggableElements = $customForm.find('tr.fieldnames td.column:not(.tdselectrow):not(.browsecontextmenucell)');
        //                            $draggableElements.attr('draggable', 'true');
        //                            break;
        //                    }

        //                    $form.attr('data-propertieschanged', true);
        //                    $form.attr('data-modified', 'true');
        //                    $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');

        //                    updateHtml();
        //                })
        //                .off('keydown', '#controlProperties .propval')
        //                .on('keydown', '#controlProperties .propval', e => {
        //                    e.stopImmediatePropagation();
        //                    if (e.which === 13 || e.keyCode === 13) {
        //                        e.preventDefault();
        //                        jQuery(e.currentTarget).trigger('change');
        //                    }
        //                })

        //                //Add new properties 
        //                .off('change', '#controlProperties .addpropval, #controlProperties .addpropname')
        //                .on('change', '#controlProperties .addpropval, #controlProperties .addpropname', e => {
        //                    e.stopImmediatePropagation();
        //                    let newProp, newPropVal;
        //                    if (jQuery(e.currentTarget).hasClass('addpropval')) {
        //                        newProp = jQuery(e.currentTarget).siblings('.addpropname').find('input').val();
        //                        newPropVal = jQuery(e.currentTarget).find('input').val();
        //                    } else {
        //                        newProp = jQuery(e.currentTarget).find('input').val();
        //                        newPropVal = jQuery(e.currentTarget).siblings('.addpropval').find('input').val();
        //                    }
        //                    let index = jQuery(originalHtml).attr('data-index');
        //                    if (newProp && newPropVal) {
        //                        let html: any = [];
        //                        html.push(` 
        //                                    <div class="properties">
        //                                      <div class="propname">${newProp}</div>
        //                                      <div class="propval"><input value="${newPropVal}"></div>
        //                                   </div>
        //                        `);
        //                        $form.find('#controlProperties .addproperties')
        //                            .before(html.join(''));

        //                        jQuery(originalHtml).attr(`${newProp}`, `${newPropVal}`);
        //                        jQuery($customFormClone).find(`div[data-index="${index}"]`).attr(`${newProp}`, `${newPropVal}`);
        //                        jQuery(e.currentTarget).siblings('.addpropname').find('input').val('');
        //                        jQuery(e.currentTarget).find('input').val('');

        //                        updateHtml();

        //                        $form.find('#controlProperties .properties:even')
        //                            .css('background-color', '#f7f7f7');
        //                    }
        //                })
        //                .off('keydown', '#controlProperties .addpropval')
        //                .on('keydown', '#controlProperties .addpropval', e => {
        //                    e.stopImmediatePropagation();
        //                    if (e.which === 13 || e.keyCode === 13) {
        //                        jQuery(e.currentTarget).trigger('change');
        //                    }
        //                })
        //                //delete button
        //                .off('click', '.deleteObject')
        //                .on('click', '.deleteObject', e => {
        //                    let $confirmation = FwConfirmation.renderConfirmation('Delete', 'Delete this object?');
        //                    let $yes = FwConfirmation.addButton($confirmation, 'Delete', false);
        //                    let $no = FwConfirmation.addButton($confirmation, 'Cancel');

        //                    $yes.off('click');
        //                    $yes.on('click', e => {
        //                        let index = jQuery(originalHtml).attr('data-index');
        //                        FwConfirmation.destroyConfirmation($confirmation);

        //                        let $element = jQuery($customForm).find(`div[data-index="${index}"]`);
        //                        let $elementClone = jQuery($customFormClone).find(`div[data-index="${index}"]`);
        //                        if (type === 'Grid' || type === 'Browse') {
        //                            $elementClone.parent('div').remove();
        //                            $element.parent('td').remove();
        //                        } else {
        //                            if ($element.siblings().length === 0 && $element.parent().hasClass('flexrow' || 'flexcolumn')) {
        //                                $element.parent().addClass('emptyContainer');
        //                            }
        //                            $elementClone.remove();
        //                            $element.remove();

        //                            //remove tabpages when tabs are removed
        //                            if (jQuery(originalHtml).attr('data-type') === "tab") {
        //                                let tabPageId = jQuery(originalHtml).attr('data-tabpageid');
        //                                let tabPage = $customForm.find(`#${tabPageId}`);
        //                                let tabPageIndex = tabPage.attr('data-index');
        //                                jQuery($customForm).find(`div[data-index="${tabPageIndex}"]`).remove();
        //                                jQuery($customFormClone).find(`div[data-index="${tabPageIndex}"]`).remove();
        //                                let firstTab = $customForm.find('.tabs [data-type="tab"]:first');
        //                                FwTabs.setActiveTab($customForm, firstTab);
        //                            }
        //                        }
        //                        $form.find('#controlProperties').empty();
        //                        updateHtml();
        //                    });
        //                })
        //                //add new column/field button
        //                .off('click', '.addColumn')
        //                .on('click', '.addColumn', e => {
        //                    e.stopPropagation();
        //                    if (type === 'Browse' || type === 'Grid') {
        //                        let $control = jQuery($customFormClone).find(`[data-type="${type}"]`);
        //                        let hasSpacer = $control.find('div:last').hasClass('spacer');
        //                        let newTdIndex = lastIndex + 1;
        //                        let newFieldIndex = newTdIndex + 1;
        //                        //build column base
        //                        let html: any = [];
        //                        html.push
        //                            (`<div class="column" data-index="${newTdIndex}">
        //    <div class="field" data-index="${newFieldIndex}"></div>
        //  </div>
        //`); //needs to be formatted this way so it looks nice in the code editor

        //                        let newColumn = jQuery(html.join(''));

        //                        hasSpacer === true ? newColumn.insertBefore($control.find('div.spacer')) : $control.append(newColumn);

        //                        originalHtml = newColumn.find('.field');

        //                        //build properties column
        //                        let propertyHtml: any = [];
        //                        let fields: any = [];

        //                        propertyHtml.push(propertyContainerHtml);
        //                        fields = ['data-datafield', 'data-datatype', 'data-sort', 'data-width', 'data-visible', 'data-caption', 'class'];
        //                        for (let i = 0; i < fields.length; i++) {
        //                            var value;
        //                            var field = fields[i];
        //                            switch (field) {
        //                                case 'data-datafield':
        //                                    value = ""
        //                                    break;
        //                                case 'data-datatype':
        //                                    value = "text"
        //                                    break;
        //                                case 'data-sort':
        //                                    value = "off"
        //                                    break;
        //                                case 'data-width':
        //                                    value = "100px"
        //                                    break;
        //                                case 'data-visible':
        //                                    value = "true"
        //                                    break;
        //                                case 'data-caption':
        //                                    value = "New Column"
        //                                    break;
        //                                case 'class':
        //                                    value = 'field';
        //                            }
        //                            propertyHtml.push(
        //                                `<div class="properties">
        //                                <div class="propname" style="border:.5px solid #efefef;">${field}</div>
        //                                <div class="propval" style="border:.5px solid #efefef;"><input value="${value}"></div>
        //                             </div>
        //                             `);

        //                            jQuery(originalHtml).attr(`${field}`, `${value}`);
        //                        };
        //                        propertyHtml.push(addPropertiesHtml);

        //                        let newProperties = $form.find('#controlProperties');
        //                        newProperties
        //                            .empty()
        //                            .append(propertyHtml.join(''), deleteComponentHtml)
        //                            .find('.properties:even')
        //                            .css('background-color', '#f7f7f7');

        //                        //replace input field with select
        //                        $form.find('#controlProperties .propname:contains("data-datafield")')
        //                            .siblings('.propval')
        //                            .find('input')
        //                            .replaceWith(`<select style="width:92%" class="datafields" value="">`);

        //                        addDatafields();

        //                        $form.find('#controlProperties .propname:contains("data-datatype")')
        //                            .siblings('.propval')
        //                            .find('input')
        //                            .replaceWith(`<select style="width:92%" class="valueOptions" value="text">`);

        //                        addValueOptions();

        //                        $form.find('#controlProperties input').change();

        //                        lastIndex = newFieldIndex
        //                    } else if (type === 'Form') {
        //                        let $tabpage = $customForm.find('[data-type="tabpage"]:visible');
        //                        let tabpageIndex = $tabpage.attr('data-index');

        //                        let newFieldIndex = lastIndex + 1;
        //                        let html: any = [];
        //                        html.push(`<div data-index="${newFieldIndex}"></div>`);

        //                        originalHtml = jQuery(html.join(''));

        //                        //build properties column
        //                        let propertyHtml: any = [];
        //                        let fields: any = [];

        //                        propertyHtml.push(propertyContainerHtml);
        //                        fields = ['data-datafield', 'data-type', 'data-caption', 'class', 'data-control'];
        //                        for (let i = 0; i < fields.length; i++) {
        //                            var value;
        //                            var field = fields[i];
        //                            switch (field) {
        //                                case 'data-datafield':
        //                                    value = ""
        //                                    break;
        //                                case 'data-control':
        //                                    value = "FwFormField"
        //                                    break;
        //                                case 'data-type':
        //                                    value = "text"
        //                                    break;
        //                                case 'data-caption':
        //                                    value = "New Field"
        //                                    break;
        //                                case 'class':
        //                                    value = 'fwcontrol fwformfield';
        //                            }
        //                            propertyHtml.push(
        //                                `<div class="properties">
        //                                <div class="propname" style="border:.5px solid #efefef;">${field}</div>
        //                                <div class="propval" style="border:.5px solid #efefef;"><input value="${value}"></div>
        //                             </div>
        //                             `);

        //                            jQuery(originalHtml).attr(`${field}`, `${value}`);
        //                        };
        //                        propertyHtml.push(addPropertiesHtml);

        //                        let newProperties = $form.find('#controlProperties');
        //                        newProperties
        //                            .empty()
        //                            .append(propertyHtml.join(''), deleteComponentHtml)
        //                            .find('.properties:even')
        //                            .css('background-color', '#f7f7f7');

        //                        //replace input field with select
        //                        $form.find('#controlProperties .propname:contains("data-datafield")')
        //                            .siblings('.propval')
        //                            .find('input')
        //                            .replaceWith(`<select style="width:92%" class="datafields" value="">`);

        //                        addDatafields();

        //                        $form.find('#controlProperties .propname:contains("data-type")')
        //                            .siblings('.propval')
        //                            .find('input')
        //                            .replaceWith(`<select style="width:92%" class="valueOptions" value="text">`);

        //                        addValueOptions();
        //                        lastIndex = newFieldIndex;
        //                        jQuery($customForm).find(`[data-index=${tabpageIndex}]`).append(originalHtml);
        //                        jQuery($customFormClone).find(`[data-index=${tabpageIndex}]`).append(originalHtml[0].cloneNode(true));
        //                        controlType = jQuery(originalHtml).attr('data-control');
        //                        $draggableElements = $customForm.find('div.fwformfield');
        //                        $draggableElements.attr('draggable', 'true');
        //                        $form.find('#controlProperties input').change();
        //                    }
        //                })
        //                .off('click', '.addNewContainer')
        //                .on('click', '.addNewContainer', e => {
        //                    //closes the menu w/ event listener add when creating button menu
        //                    jQuery(document).trigger('click');

        //                    e.stopPropagation();
        //                    let $tabpage = $customForm.find('[data-type="tabpage"]:visible');
        //                    let tabpageIndex = $tabpage.attr('data-index');

        //                    let newContainerIndex = lastIndex + 1;
        //                    let html: any = [];
        //                    html.push(`<div data-index="${newContainerIndex}"></div>`);

        //                    originalHtml = jQuery(html.join(''));

        //                    //build properties column
        //                    let propertyHtml: any = [];
        //                    let fields: any = [];

        //                    propertyHtml.push(propertyContainerHtml);
        //                    fields = ['class'/*, 'style'*/];
        //                    for (let i = 0; i < fields.length; i++) {
        //                        var value;
        //                        var field = fields[i];
        //                        switch (field) {
        //                            //case 'style':
        //                            //    value = 'min-height:50px';
        //                            //    break;
        //                            case 'class':
        //                                value = 'flexrow emptyContainer';
        //                                break;
        //                        }
        //                        propertyHtml.push(
        //                            `<div class="properties">
        //                                <div class="propname" style="border:.5px solid #efefef;">${field}</div>
        //                                <div class="propval" style="border:.5px solid #efefef;"><input value="${value}"></div>
        //                             </div>
        //                             `);

        //                        jQuery(originalHtml).attr(`${field}`, `${value}`);
        //                    };
        //                    propertyHtml.push(addPropertiesHtml);

        //                    let newProperties = $form.find('#controlProperties');
        //                    newProperties
        //                        .empty()
        //                        .append(propertyHtml.join(''), deleteComponentHtml)
        //                        .find('.properties:even')
        //                        .css('background-color', '#f7f7f7');

        //                    lastIndex = newContainerIndex;
        //                    jQuery($customForm).find(`[data-index=${tabpageIndex}]`).append(originalHtml);
        //                    jQuery($customFormClone).find(`[data-index=${tabpageIndex}]`).append(originalHtml[0].cloneNode(true));
        //                    $draggableElements = $customForm.find('div.fwformfield, div.flexrow, div.flexcolumn, div[data-type="tab"]');
        //                    $draggableElements.attr('draggable', 'true');
        //                    $form.find('#controlProperties input').change();
        //                })
        //                .off('click', '.addNewTab')
        //                .on('click', '.addNewTab', e => {
        //                    e.stopPropagation();
        //                    //closes the menu w/ event listener add when creating button menu
        //                    jQuery(document).trigger('click');

        //                    let newIndex = lastIndex + 1;
        //                    let $tabControl = $customForm.find('[data-control="FwTabs"]');
        //                    let newTabIds = FwTabs.addTab($tabControl, 'New Tab', '', '', true); //contains tabid and tabpageid

        //                    originalHtml = $customForm.find(`#${newTabIds.tabid}`);
        //                    originalHtml.attr('data-index', newIndex);

        //                    originalHtml.click();

        //                    let html: any = [];
        //                    html.push(`<div class="flexrow emptyContainer" data-index="${++newIndex}"></div>`);

        //                    let newTabPage = $customForm.find(`#${newTabIds.tabpageid}`);
        //                    newTabPage.attr('data-index', ++newIndex);
        //                    newTabPage.append(html.join(''));

        //                    //update html for code editor
        //                    let tabClone = originalHtml.cloneNode(true);
        //                    jQuery(tabClone).empty();
        //                    jQuery($customFormClone).find('.tabs').append(tabClone);
        //                    jQuery($customFormClone).find('.tabpages').append(newTabPage[0].cloneNode(true));
        //                    lastIndex = newIndex;
        //                    updateHtml();

        //                    $draggableElements = $customForm.find('div.fwformfield, div.flexrow, div.flexcolumn, div[data-type="tab"]');
        //                    $draggableElements.attr('draggable', 'true');
        //                });


        //        }
    }
    //----------------------------------------------------------------------------------------------
    //getValueOptions(fieldname: string) {
    //    var values: any = [];
    //    switch (fieldname) {
    //        case 'data-browsedatatype':
    //        case 'data-formdatatype':
    //        case 'data-datatype':
    //        case 'data-type':
    //            values = [
    //                'checkbox'
    //                , 'checkboxlist'
    //                , 'color'
    //                , 'combobox'
    //                , 'date'
    //                , 'datetime'
    //                , 'decimal'
    //                , 'email'
    //                , 'key'
    //                , 'money'
    //                , 'multiselectvalidation'
    //                , 'note'
    //                , 'number'
    //                , 'percent'
    //                , 'phone'
    //                , 'radio'
    //                , 'searchbox'
    //                , 'select'
    //                , 'tab'
    //                , 'text'
    //                , 'textarea'
    //                , 'time'
    //                , 'timepicker'
    //                , 'validation'
    //            ];
    //            break;
    //        case 'data-sort':
    //            values = ['asc', 'desc', 'off'];
    //            break;
    //        case 'data-formrequired':
    //        case 'data-required':
    //        case 'data-enabled':
    //        case 'data-visible':
    //        case 'data-formreadonly':
    //        case 'data-isuniqueid':
    //            values = ['true', 'false'];
    //            break;
    //    }
    //    return values;
    //}
};
//----------------------------------------------------------------------------------------------
var CustomReportLayoutController = new CustomReportLayout();