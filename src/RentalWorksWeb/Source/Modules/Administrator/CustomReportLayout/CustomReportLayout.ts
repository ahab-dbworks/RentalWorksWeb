class CustomReportLayout {
    Module: string = 'CustomReportLayout';
    apiurl: string = 'api/v1/customreportlayout';
    caption: string = Constants.Modules.Administrator.children.CustomReportLayout.caption;
    nav: string = Constants.Modules.Administrator.children.CustomReportLayout.nav;
    id: string = Constants.Modules.Administrator.children.CustomReportLayout.id;
    codeMirror: any;
    html: string;
    TotalColumnCount = 0;
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
                $form.data('columnsmoved', { oldIndex: e.oldIndex, newIndex: e.newIndex });
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
            const newHTML = $tr.get(0).innerHTML.trim();
            if (sectionToUpdate == 'tableheader') {
                const rowSelector = 'tbody tr';
                const $rows = $wrapper.find(rowSelector);

                //move columns to match column order in the header
                if (typeof $form.data('columnsmoved') != 'undefined' && typeof $th != 'undefined') {
                    const valuefield = $th.attr('data-valuefield');
                    if (typeof valuefield != 'undefined') {
                        const oldIndex = $form.data('columnsmoved').oldIndex;
                        const newIndex = $form.data('columnsmoved').newIndex;
                        for (let i = 0; i < $rows.length; i++) {
                            const $row = jQuery($rows[i]);
                            const rowType = $row.attr('data-row');
                            if (rowType == 'detail') {
                                const $tds = $row.find('td');
                                let $movedTd = $row.find(`[data-value="<!--{{${valuefield}}}-->"]`);
                                if ($movedTd.length) {
                                    const $designerTd = $table.find(`${rowSelector}[data-row="${rowType}"] [data-value="{{${valuefield}}}"]`);
                                    const $designerTds = $table.find(`${rowSelector}[data-row="${rowType}"] td`);
                                    if (oldIndex > newIndex) {
                                        if (newIndex != 0) {
                                            $movedTd.insertBefore($tds[newIndex]);
                                            $designerTd.insertBefore($designerTds[newIndex]);
                                        } else {
                                            $movedTd.insertBefore($tds[newIndex]);
                                            $designerTd.insertBefore($designerTds[newIndex]);
                                        }
                                    } else {
                                        if ((newIndex + 1) == this.TotalColumnCount) {
                                            $movedTd.insertAfter($tds[newIndex]);
                                            $designerTd.insertAfter($designerTds[newIndex]);
                                        } else {
                                            $movedTd.insertAfter($tds[newIndex]);
                                            $designerTd.insertAfter($designerTds[newIndex]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    $form.removeData('columnsmoved');
                }

                if (typeof $form.data('addcolumn') != 'undefined') {
                    const newColumnData = $form.data('addcolumn');
                    const valueField = `NewColumn${newColumnData.newcolumnnumber}`;
                    for (let i = 0; i < $rows.length; i++) {
                        const $row = jQuery($rows[i]);
                        const rowType = $row.attr('data-row');
                        if (rowType == 'detail') {
                            const $td = jQuery(`<td data-value="<!--{{${valueField}}}-->"></td>`);
                            $row.append($td);  //add to row in wrapper (memory)
                            $table.find(`${rowSelector}[data-row="${rowType}"]`).append(`<td data-value="{{${valueField}}}"></td>`); //add to row on designer
                        } else {   //to-do: add extra options on property section for adding new columns to header/footer rows 
                            const $designerTableRow = jQuery($table.find(`${rowSelector}`)[i]);
                            this.matchColumnCount($form, $table, $row, $designerTableRow);
                        }
                    }
                    $form.removeData('addcolumn');
                }

                //delete
                if (typeof $form.data('deletefield') != 'undefined') {
                    const valuefield = $form.data('deletefield').field;
                    for (let i = 0; i < $rows.length; i++) {
                        const $row = jQuery($rows[i]);
                        const $td = $row.find(`[data-value="<!--{{${valuefield}}}-->"]`);
                        const tdIsInRow = $td.length;
                        if (tdIsInRow) { //
                            const rowType = $row.attr('data-row');
                            $td.remove();
                            $table.find(`${rowSelector}[data-row="${rowType}"] [data-value="{{${valuefield}}}"]`).remove();
                        } else {
                            //if it doesn't exist in this row, then total colspan needs to be reduced to match the TotalColumnCount
                            const $designerTableRow = jQuery($table.find(`${rowSelector}`)[i]);
                            this.matchColumnCount($form, $table, $row, $designerTableRow);
                        }
                    }
                    $form.removeData('deletefield');
                }
                $wrapper.find('table #columnHeader tr').html(newHTML);                     //replace old headers
            } else if (sectionToUpdate == 'headerrow') {
                const valuefield = $tr.attr('data-valuefield');
                const $column = $wrapper.find(`table .header-row[data-valuefield="${valuefield}"]`);
                if ($column.length) {
                    $column.html(newHTML);
                }
            } else if (sectionToUpdate == 'footerrow') {
                const valuefield = $tr.attr('data-valuefield');
                const $column = $wrapper.find(`table .total-name[data-valuefield="${valuefield}"]`);
                if ($column.length) {
                    $column.html(newHTML);
                }
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
        this.TotalColumnCount = this.getTotalColumnCount($table, true);
        const $addColumn = $form.find('.addColumn');
        $addColumn.show();
        let $column;
        let newColumnNumber = 1;

        //add header column
        $addColumn.on('click', e => {
            $column = jQuery(`<th data-valuefield="NewColumn${newColumnNumber}">New Column</th>`);
            $table.find('#columnHeader tr').append($column);
            this.TotalColumnCount++;
            $form.data('sectiontoupdate', 'tableheader');
            $form.data('addcolumn', { 'newcolumnnumber': newColumnNumber, 'tdcolspan': 1 });
            this.updateHTML($form, $table.find('#columnHeader tr'));
            newColumnNumber++;
        });

        $form.on('click', '#reportDesigner table thead#columnHeader tr th', e => {
            $column = jQuery(e.currentTarget);
            $form.data('sectiontoupdate', 'tableheader');
            $form.find('#controlProperties').empty().append(this.addControlProperties($column));
        });

        //control properties events
        $form.on('change', '#controlProperties .propval', e => {
            const $property = jQuery(e.currentTarget);
            const fieldname = $property.attr('data-field');
            const value = $property.find('input').val();
            switch (fieldname) {
                case 'columnname':
                    $column.text(value);
                    break;
                case 'valuefield': //to-do: add valuefield properties to all headers
                    $column.attr('data-valuefield', value);
                    break;
            }
            const section = $form.data('sectiontoupdate');
            if (section == 'tableheader') {
                this.updateHTML($form, $table.find('#columnHeader tr'));
            } else if (section == 'headerrow' || section == 'footerrow') {
                this.updateHTML($form, $column);
            }
        });
        //hover over header fields
        $form.find('#columnHeader tr th').hover(e => { //mouseenter
            const valueFieldName = jQuery(e.currentTarget).attr('data-valuefield');
            $table.find(`tbody td[data-value="{{${valueFieldName}}}"]`).addClass('hover-cell');
        }, e => { // mouseleave
            const valueFieldName = jQuery(e.currentTarget).attr('data-valuefield');
            $table.find(`tbody td[data-value="{{${valueFieldName}}}"]`).removeClass('hover-cell');
        });

        //delete table header column
        $form.on('click', '.delete-column', e => {
            if (typeof $column != 'undefined') {
                const valuefield = jQuery($column).attr('data-valuefield');
                const colspan = parseInt(jQuery($column).attr('colspan')) || 1;
                $column.remove();
                this.TotalColumnCount -= colspan;

                $form.data('sectiontoupdate', 'tableheader');
                if (typeof valuefield != 'undefined') {
                    $form.data('deletefield', { 'field': valuefield, 'tdcolspan': colspan });
                }
                this.updateHTML($form, $table.find('#columnHeader tr'));
            }
        });

        //header row
        $form.on('click', '.header-row', e => {
            $column = jQuery(e.currentTarget);
            $form.find('#controlProperties').empty().append(this.addControlProperties($column));

            $form.data('sectiontoupdate', 'headerrow');
            //this.updateHTML($form, $table.find('#columnHeader tr'));
        });

        $form.on('click', '.total-name', e => {
            $column = jQuery(e.currentTarget);
            $form.find('#controlProperties').empty().append(this.addControlProperties($column));
            $form.data('sectiontoupdate', 'footerrow');
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
                                    <div style="text-align:center; margin:1em;">
                                        <div class="fwformcontrol delete-column" data-type="button">Delete Column</div>
                                    </div>
                                 </div>`);
        return $properties;
    }
    //----------------------------------------------------------------------------------------------
    getTotalColumnCount($table: JQuery, isTableHeader: boolean, $row?: JQuery) {
        let count = 0;
        let $columns;
        if (isTableHeader) {
            $columns = $table.find(`#columnHeader tr th`);
        } else {
            $columns = $row.find('td');
        }

        for (let i = 0; i < $columns.length; i++) {
            const $column = jQuery($columns[i]);
            let colspan = parseInt($column.attr('colspan')) || 1;
            if (typeof $column.attr('colspan') != 'undefined') {
                colspan = parseInt($column.attr('colspan'));
            }
            count += colspan;
        }

        return count;
    }
    //----------------------------------------------------------------------------------------------
    matchColumnCount($form: JQuery, $table: JQuery, $row: JQuery, $designerTableRow: JQuery) {
        const rowColumnCount = this.getTotalColumnCount($table, false, $row);
        if (rowColumnCount != this.TotalColumnCount) {
            //need to decrease by tdColumnSpan
            const rowType = $row.attr('data-row');
            let colspanDelta = 0;
            let actionType;
            if (typeof $form.data('deletefield') != 'undefined') {
                colspanDelta = $form.data('deletefield').tdcolspan;
                actionType = 'delete';
            } else if (typeof $form.data('addcolumn') != 'undefined') {
                colspanDelta = 1;
                actionType = 'add';
            }

            let selector;
            switch (rowType) {
                case 'header':
                    selector = '.header-row';
                    break;
                case 'footer':
                    selector = '.total-name';
                    break;
                case 'detail':
                    //
                    break;
            }

            const $td = $row.find(selector);
            let colspan: any = $td.attr('colspan');
            if (typeof colspan != 'undefined') {
                colspan = parseInt(colspan);
                //if (actionType == 'delete') {
                //    if (rowColumnCount > this.TotalColumnCount) {
                //        colspan -= colspanDelta;
                //    }
                //} else if (actionType == 'add') {
                //    if (rowColumnCount < this.TotalColumnCount) {
                //        colspan += colspanDelta;
                //    }
                //}

                if (rowColumnCount > this.TotalColumnCount) {
                    colspan -= colspanDelta;
                } else if (rowColumnCount < this.TotalColumnCount) {
                    colspan += colspanDelta;
                }

                $td.attr('colspan', colspan);
                $designerTableRow.find(selector).attr('colspan', colspan);

                this.matchColumnCount($form, $table, $row, $designerTableRow);
            }
        }
    }
    //----------------------------------------------------------------------------------------------
};
//----------------------------------------------------------------------------------------------
var CustomReportLayoutController = new CustomReportLayout();