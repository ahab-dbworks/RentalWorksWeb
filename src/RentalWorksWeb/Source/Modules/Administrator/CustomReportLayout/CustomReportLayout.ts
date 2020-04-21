class CustomReportLayout {
    Module: string = 'CustomReportLayout';
    apiurl: string = 'api/v1/customreportlayout';
    caption: string = Constants.Modules.Administrator.children.CustomReportLayout.caption;
    nav: string = Constants.Modules.Administrator.children.CustomReportLayout.nav;
    id: string = Constants.Modules.Administrator.children.CustomReportLayout.id;
    codeMirror: any;
    html: string;
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
        this.html = FwFormField.getValueByDataField($form, 'Html');

        const $table = jQuery(this.html).find('table');
        $form.find(`#reportDesigner`).empty().append($table);

        //create sortable for headers
        Sortable.create($table.find('#columnHeader tr').get(0), {
            onStart: e => {
                const valueFieldName = jQuery(e.item).attr('data-valuefield');
                $table.find(`tbody td[data-value="{{${valueFieldName}}}"]`).addClass('highlight-cells');
            },
            onEnd: e => {
                const $th = jQuery(e.item);
                $th.removeAttr('draggable');
                const valueFieldName = $th.attr('data-valuefield');
                $table.find(`tbody td[data-value="{{${valueFieldName}}}"]`).removeClass('highlight-cells');

                const $tr = jQuery(e.currentTarget);
                $tr.data('columnsmoved', { oldIndex: e.oldIndex, newIndex: e.newIndex });
                $form.data('sectiontoupdate', 'tableheader');
                this.updateHTML($form, $tr, $th);

                $form.attr('data-modified', 'true');
                $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
            }
        });
        this.designerEvents($form, $table);

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
    }
    //----------------------------------------------------------------------------------------------
    updateHTML($form: JQuery, $tr: JQuery, $th?) {
        const sectionToUpdate = $form.data('sectiontoupdate');
        if (typeof sectionToUpdate != 'undefined' && typeof sectionToUpdate == 'string') {
            const $wrapper = jQuery('<div class="custom-report-wrapper"></div>');
            const $table = $form.find('#reportDesigner table');
            this.html = this.html.split('{{').join('<!--{{').split('}}').join('}}-->');      //comments out handlebars as a work-around for the displacement by the HTML parser 
            $wrapper.append(this.html);                                                      //append the original HTML to the wrapper.  this is done to combine the loose elements.
            if (sectionToUpdate == 'tableheader') {
                const newHeaderHTML = $tr.get(0).innerHTML.trim();                               //get the new header HTML       
                
                //move corresponding detail columns wip
                if (typeof $tr.data('columnsmoved') != 'undefined' && typeof $th != 'undefined') {
                    const valuefield = $th.attr('data-valuefield');
                    if (typeof valuefield != 'undefined') {
                        const $columns = $table.find(`tbody tr [data-value="{{${valuefield}}}"]`);
                        //jason h to-do: get last index of tds with consideration to colspans and add to cached object
                        const oldIndex = $tr.data('columnsmoved').oldIndex;
                        const newIndex = $tr.data('columnsmoved').newIndex;
                        for (let i = 0; i < $columns.length; i++) {
                            //find total number of tds
                            //get index of td relative to tr
                            //take into account colspan attribute / check total cells of colspan + tds match
                        }
                    }
                }

                $wrapper.find('table #columnHeader tr').html(newHeaderHTML);                     //replace old headers
            } else if (sectionToUpdate == 'headerrow') {
                //wip
            }
            this.html = $wrapper.get(0).innerHTML;                                           //get new report HTML
            this.html = this.html.split('<!--{{').join('{{').split('}}-->').join('}}');      //un-comment handlebars
            FwFormField.setValueByDataField($form, 'Html', this.html);
            this.codeMirror.setValue(this.html);                                             //update codemirror (HTML tab) with new HTML
            $form.removeData('sectiontoupdate');
        }
    }
    //----------------------------------------------------------------------------------------------
    designerEvents($form: JQuery, $table: JQuery) {
        const $addColumn = $form.find('.addColumn');
        $addColumn.show();
        let $th;

        //add header column
        $addColumn.on('click', e => {
            $table.find('#columnHeader tr').append('<th data-columndatafield="">New Column</th>');
            $form.data('sectiontoupdate', 'tableheader');
            this.updateHTML($form, $table.find('#columnHeader tr'));
        });

        $form.on('click', '#reportDesigner table thead tr th', e => {
            $th = jQuery(e.currentTarget);
            $form.find('#controlProperties').empty().append(this.addControlProperties($th));
        });

        //control properties events
        $form.on('change', '#controlProperties .propval', e => {
            const $property = jQuery(e.currentTarget);
            const fieldname = $property.attr('data-field');
            const value = $property.find('input').val();
            switch (fieldname) {
                case 'columnname':
                    $th.text(value);
                    break;
                case 'valuefield': //to-do: add columndatafield properties to all headers
                    $th.attr('data-valuefield', value);
                    break;
            }
            $form.data('sectiontoupdate', 'tableheader');
            this.updateHTML($form, $table.find('#columnHeader tr'));
        });
        //hover over header fields
        $form.find('#columnHeader tr th').hover(e => { //mouseenter
            const valueFieldName = jQuery(e.currentTarget).attr('data-valuefield');
            $table.find(`tbody td[data-value="{{${valueFieldName}}}"]`).addClass('hover-cell');
        }, e => { // mouseleave
            const valueFieldName = jQuery(e.currentTarget).attr('data-valuefield');
            $table.find(`tbody td[data-value="{{${valueFieldName}}}"]`).removeClass('hover-cell');
        });

        //delete column
        $form.on('click', '.delete-column', e => {
            $th.remove();
            $form.data('sectiontoupdate', 'tableheader');
            this.updateHTML($form, $table.find('#columnHeader tr'));
        });

        //header row  //////wip -- need to update "addControlProperties" to work for controls besides the th
        $form.on('click', '.header-row', e => {
            $th = jQuery(e.currentTarget);
            $form.find('#controlProperties').empty().append(this.addControlProperties($th));


            $form.data('sectiontoupdate', 'headerrow');
            //this.updateHTML($form, $table.find('#columnHeader tr'));
        });

    }
    //----------------------------------------------------------------------------------------------
    addControlProperties($th: JQuery) {
        let $properties = jQuery(`<div class="propertyContainer" style="border: 1px solid #bbbbbb; word-break: break-word;">
                                    <div style="text-indent:5px;">
                                        <div style="font-weight:bold; background-color:#dcdcdc; width:50%; float:left;">Name</div>
                                        <div style="font-weight:bold; background-color:#dcdcdc; width:50%; float:left;">Value</div>
                                    </div>
                                    <div class="properties">
                                        <div class="propname">Column Name</div>
                                        <div class="propval" data-field="columnname"><input value="${$th.text()}"></div>
                                    </div>
                                    <div class="properties">
                                        <div class="propname">Value Field</div>
                                        <div class="propval" data-field="valuefield"><input placeholder="value field" value="${$th.attr('data-valuefield')}"></div>
                                    </div>
                                    <div style="text-align:center;">
                                        <div class="fwformcontrol delete-column" data-type="button">Delete Column</div>
                                    </div>
                                 </div>`);
        return $properties;
    }
    //----------------------------------------------------------------------------------------------
};
//----------------------------------------------------------------------------------------------
var CustomReportLayoutController = new CustomReportLayout();