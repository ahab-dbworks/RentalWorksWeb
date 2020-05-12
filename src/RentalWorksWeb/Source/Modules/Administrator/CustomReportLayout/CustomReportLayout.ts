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
                                'value': key,
                                'text': key,
                                'IsCustom': 'false',
                                'NestedItems': orderedItems
                            });
                        } else {
                            allValidFields.push({
                                'value': key,
                                'text': key,
                                'IsCustom': 'false'
                            });
                        }
                    }
                }

                for (let i = 0; i < customFields.length; i++) {
                    allValidFields.push({
                        'value': customFields[i].fieldname,
                        'text': customFields[i].fieldname,
                        'IsCustom': 'true',
                        'FieldType': customFields[i].fieldtype.toLowerCase()
                    });
                }

                $form.data('validdatafields', allValidFields.sort((a, b) => a.value < b.value ? -1 : 1));

                for (let i = 0; i < allValidFields.length; i++) {
                    modulefields.append(`<div data-iscustomfield=${allValidFields[i].IsCustom}>${allValidFields[i].value}</div>`);
                    if (allValidFields[i].hasOwnProperty("NestedItems")) {
                        for (const key of Object.keys(allValidFields[i].NestedItems)) {
                            if (key != '_Custom') {
                                modulefields.append(`<div data-iscustomfield="false" data-isnested="true" data-parentfield="${allValidFields[i].value}" style="text-indent:1em;">${key}</div>`);
                            }
                        }
                    }
                }

                FwFormField.loadItems($form.find('[data-datafield="ValueField"]'), $form.data('validdatafields'));

            }, ex => FwFunc.showError(ex), $form);
    }
    //----------------------------------------------------------------------------------------------
    loadModules($form) {
        let $moduleSelect = $form.find('.modules');

        const reports = FwApplicationTree.getAllReports(false, false, (modules: any[], moduleCaption: string, moduleName: string, category: string, currentNode: any, nodeModule: IGroupSecurityNode, hasView: boolean, hasNew: boolean, hasEdit: boolean, moduleController: any) => {
            if (moduleController.hasOwnProperty('apiurl')) {
                modules.push({ value: moduleName, text: moduleCaption, apiurl: moduleController.apiurl, designer: moduleController.designerProvisioned ? true : false });
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
        //designer notes - jason hoang 05/12/2020
        //- currently supports ONE table within report HTML
        //- everything outside of the table element is ignored
        //
        //- important attributes - 
        //  - THEAD with id = "columnHeader"                       //we could probably take the id out if there is only one thead within the table
        //      - TH data-valuefields should match with the tds.   //this value will be used for linking columns together with the data-linkedcolumn attribute.
        //                                                         //when the valuefield is changed, the linkedcolumn remains the same to keep track of which tds we need to move. 
        //  - TBODY
        //      -- TRs need the data-row attribute 
        //      -- values: ['header', 'detail', 'footer'].                //multiple header and footer rows, one detail row.
        //      -- FOOTER rows need one td with the 'total-name' class.   //the colspan should be correct based on total colspan of THs in the THEAD
        //
        //
        //- basic table structure -
        //<table>
        //  <thead id="columnHeader">
        //      <tr> 
        //        <th data-valuefield="ExampleField">Example Field Caption</th>
        //        <th data-valuefield="ExampleField2">Example Field2 Caption</th>
        //        <th data-valuefield="ExampleTotalField">Example Total Field Caption</th>
        //      </tr>
        //      ..
        //  </thead>
        //  <tbody>
        //      <tr data-row="header">
        //          <td colspan="3" data-valuefield="ExampleHeaderField">Example Header Caption</td>
        //      </tr>
        //      ..
        //      <tr data-row="detail">
        //          <td data-value="ExampleField"></td>         //maybe we should change data-value to data-valuefield for consistency?  
        //          <td data-value="ExampleField2"></td>       
        //          <td data-value="ExampleTotalField"></td>  
        //      </tr>
        //      <tr data-row="footer">
        //          <td colspan="2" class="total-name"></td>    //assumes only one total-name column.  it's important that the colspan is correct
        //          <td data-value="ExampleTotalField"></td>
        //      </tr>
        //      ..
        //  </tbody>
        //</table>

        $form.find('#codeEditor').change();     // 10/25/2018 Jason H - updates the textarea formfield with the code editor html
        this.html = FwFormField.getValueByDataField($form, 'Html');

        const designerProvisioned = $form.find('[data-datafield="BaseReport"] :selected').attr('data-designer');
        if (designerProvisioned == 'true') {
            const $table = jQuery(this.html).find('table');
            $form.find(`#reportDesigner`).empty().append($table);

            //create sortable for headers
            Sortable.create($table.find('#columnHeader tr').get(0), {
                onStart: e => {
                    $table.find('tbody td[data-linkedcolumn]').removeClass('highlight-cells');
                    const linkedColumnName = jQuery(e.item).attr('data-linkedcolumn');
                    $table.find(`tbody td[data-linkedcolumn="${linkedColumnName}"]`).addClass('highlight-cells');
                },
                onEnd: e => {
                    const $th = jQuery(e.item);
                    $th.removeAttr('draggable');
                    const linkedColumnName = $th.attr('data-linkedcolumn');

                    const $tr = jQuery(e.currentTarget);
                    $form.data('columnsmoved', { oldIndex: e.oldIndex, newIndex: e.newIndex });
                    $form.data('sectiontoupdate', 'tableheader');
                    this.updateHTML($form, $tr, $th);

                    $form.attr('data-modified', 'true');
                    $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
                    $table.find(`tbody td[data-linkedcolumn="${linkedColumnName}"]`).removeClass('highlight-cells').addClass('highlight-cells');
                }
            });
            this.linkColumns($form, $table);
            this.designerEvents($form, $table);
        } else {
            $form.find(`#reportDesigner`).empty().append(`<div>This report is not currently Designer-provisioned.  Please use the HTML tab to make changes.</div>`);
        }
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
                    const linkedColumn = $th.attr('data-linkedcolumn');
                    if (typeof valuefield != 'undefined') {
                        const oldIndex = $form.data('columnsmoved').oldIndex;
                        const newIndex = $form.data('columnsmoved').newIndex;
                        let $detailRowTds;
                        let footerCount = 0;
                        for (let i = 0; i < $rows.length; i++) {
                            const $row = jQuery($rows[i]);
                            const rowType = $row.attr('data-row');
                            const $designerRow = jQuery($table.find(`${rowSelector}[data-row="${rowType}"]`)[footerCount]);
                            let $designerTds = $designerRow.find('td');
                            const $designerTd = $designerRow.find(`[data-linkedcolumn="${linkedColumn}"]`);
                            let $tds = $row.find('td');
                            if (rowType == 'detail') {
                                $detailRowTds = $designerTds;
                                const $movedTd = $row.find(`[data-value="<!--{{${valuefield}}}-->"]`);
                                if ($movedTd.length) {
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
                            } else if (rowType == 'footer') {
                                const totalNameColSpan = parseInt($designerTds.filter('.total-name').attr('colspan'));
                                const totalNameIndex = $designerTds.filter('.total-name').index();
                                const endTotalNameColumnIndex = totalNameIndex + (totalNameColSpan - 1);
                                if ($designerTd.length) {
                                    if ($designerTd.hasClass('empty-td')) { //move newly added columns
                                        const $movedTd = $row.find(`[data-linkedcolumn="${linkedColumn}"]`);
                                        if (oldIndex > newIndex) { //moving LEFT
                                            if (newIndex > endTotalNameColumnIndex) { //moved to the right of the totalName column
                                                if (newIndex == endTotalNameColumnIndex + 1) { //merge with totalName column
                                                    $designerTds.filter('.total-name').attr('colspan', totalNameColSpan + 1);
                                                    $designerTd.remove();
                                                    $tds.filter('.total-name').attr('colspan', totalNameColSpan + 1);
                                                    $movedTd.remove();
                                                } else {
                                                    $movedTd.insertBefore($tds[newIndex - (totalNameColSpan - 1)]);
                                                    $designerTd.insertBefore($designerTds[newIndex - (totalNameColSpan - 1)]);
                                                }
                                            } else if ((newIndex > totalNameIndex) && (newIndex <= endTotalNameColumnIndex)) { //moved within the totalName column
                                                $designerTds.filter('.total-name').attr('colspan', totalNameColSpan + 1); //merge
                                                $designerTd.remove();
                                                $tds.filter('.total-name').attr('colspan', totalNameColSpan + 1);
                                                $movedTd.remove();
                                            } else { //moved to the left of totalName column
                                                if (newIndex <= totalNameIndex) { //merge
                                                    $designerTds.filter('.total-name').attr('colspan', totalNameColSpan + 1);
                                                    $designerTd.remove();
                                                    $tds.filter('.total-name').attr('colspan', totalNameColSpan + 1);
                                                    $movedTd.remove();
                                                } else {
                                                    $movedTd.insertBefore($tds[newIndex]);
                                                    $designerTd.insertBefore($designerTds[newIndex]);
                                                }
                                            }
                                        } else if (newIndex > oldIndex) { //moving RIGHT
                                            if (newIndex > endTotalNameColumnIndex) { //moved to the right of the totalName column
                                                $movedTd.insertAfter($tds[newIndex - (totalNameColSpan - 1)]);
                                                $designerTd.insertAfter($designerTds[newIndex - (totalNameColSpan - 1)]);
                                            } else if ((newIndex >= totalNameIndex) && (newIndex <= endTotalNameColumnIndex)) { //moved within the totalName column
                                                $designerTds.filter('.total-name').attr('colspan', totalNameColSpan + 1); //merge
                                                $designerTd.remove();
                                                $tds.filter('.total-name').attr('colspan', totalNameColSpan + 1);
                                                $movedTd.remove();
                                            } else { //moved to the left of totalName column
                                                $movedTd.insertAfter($tds[newIndex]);
                                                $designerTd.insertAfter($designerTds[newIndex]);
                                            }
                                        }
                                    } else { //move totaled columns
                                        const $movedTd = $row.find(`[data-value="<!--{{${valuefield}}}-->"]`);
                                        if (oldIndex > newIndex) { //moving LEFT
                                            if (newIndex <= totalNameIndex) {//to the LEFT of totalname col
                                                $movedTd.insertBefore($tds[newIndex]);
                                                $designerTd.insertBefore($designerTds[newIndex]);
                                            } else if (newIndex > endTotalNameColumnIndex) {//to the RIGHT of totalname col
                                                $movedTd.insertAfter($tds[newIndex - totalNameColSpan]);
                                                $designerTd.insertAfter($designerTds[newIndex - totalNameColSpan]);
                                            } else if ((newIndex > totalNameIndex) && (newIndex <= endTotalNameColumnIndex)) { //INTO the totalname col
                                                let columnsToUnmerge = totalNameColSpan - (newIndex - totalNameIndex);
                                                const newTotalNameColSpan = totalNameColSpan - columnsToUnmerge;
                                                $designerTds.filter('.total-name').attr('colspan', newTotalNameColSpan);
                                                $tds.filter('.total-name').attr('colspan', newTotalNameColSpan);
                                                $movedTd.insertAfter($tds[totalNameIndex]);
                                                $designerTd.insertAfter($designerTds[totalNameIndex]);

                                                $designerTds = $designerRow.find('td');  //reassign with new element order
                                                $tds = $row.find('td');

                                                //split tds to the right into empty tds
                                                for (let j = 1; j <= columnsToUnmerge; j++) {
                                                    const columnToLinkIndex = endTotalNameColumnIndex - (j - 1);
                                                    const linkedCol = jQuery($detailRowTds[columnToLinkIndex]).attr('data-linkedcolumn');
                                                    const $newTd = jQuery(`<td class="empty-td" data-linkedcolumn="${linkedCol}"></td>`);

                                                    $newTd.clone().insertAfter($tds[newIndex + 1 - newTotalNameColSpan]);
                                                    $newTd.insertAfter($designerTds[newIndex + 1 - newTotalNameColSpan]);
                                                }
                                            }
                                        } else if (oldIndex < newIndex) { //moving RIGHT
                                            if (newIndex > endTotalNameColumnIndex) { //to the RIGHt of totalname col
                                                $movedTd.insertAfter($tds[newIndex - totalNameColSpan + 1]);
                                                $designerTd.insertAfter($designerTds[newIndex - totalNameColSpan + 1]);
                                            } else if (newIndex <= totalNameIndex) {//to the LEFT of totalname col
                                                $movedTd.insertAfter($tds[newIndex]);
                                                $designerTd.insertAfter($designerTds[newIndex]);
                                            } else if ((newIndex > totalNameIndex) && (newIndex <= endTotalNameColumnIndex)) { //INTO the totalname col
                                                let columnsToUnmerge = endTotalNameColumnIndex - newIndex;
                                                const newTotalNameColSpan = totalNameColSpan - columnsToUnmerge;
                                                $designerTds.filter('.total-name').attr('colspan', newTotalNameColSpan);
                                                $tds.filter('.total-name').attr('colspan', newTotalNameColSpan);
                                                $movedTd.insertAfter($tds[totalNameIndex]);
                                                $designerTd.insertAfter($designerTds[totalNameIndex]);

                                                $designerTds = $designerRow.find('td');  //reassign with new element order
                                                $tds = $row.find('td');

                                                //split tds to the right into empty tds
                                                for (let j = 1; j <= columnsToUnmerge; j++) {
                                                    const columnToLinkIndex = endTotalNameColumnIndex - (j - 1);
                                                    const linkedCol = jQuery($detailRowTds[columnToLinkIndex]).attr('data-linkedcolumn');
                                                    const $newTd = jQuery(`<td class="empty-td" data-linkedcolumn="${linkedCol}"></td>`);

                                                    $newTd.clone().insertAfter($tds[newIndex + 1 - newTotalNameColSpan]);
                                                    $newTd.insertAfter($designerTds[newIndex + 1 - newTotalNameColSpan]);
                                                }
                                            }
                                        }
                                    }
                                } else { //it wont find a designertd if it merged into a footer total-name column
                                    const $newTd = jQuery(`<td class="empty-td" data-linkedcolumn="${linkedColumn}"></td>`);
                                    let footerRowIndex = newIndex;
                                    if (oldIndex < newIndex) { //moving RIGHT
                                        if (newIndex > endTotalNameColumnIndex) { //add new empty tds
                                            $designerTds.filter('.total-name').attr('colspan', totalNameColSpan - 1);
                                            $tds.filter('.total-name').attr('colspan', totalNameColSpan - 1);
                                            footerRowIndex = newIndex - (totalNameColSpan - 1);
                                            $newTd.clone().insertAfter($tds[footerRowIndex]);
                                            $newTd.insertAfter($designerTds[footerRowIndex]);
                                        }
                                    } else if (oldIndex > newIndex) { //MOVING LEFT
                                        if (newIndex < totalNameIndex) {
                                            $designerTds.filter('.total-name').attr('colspan', totalNameColSpan - 1);
                                            $tds.filter('.total-name').attr('colspan', totalNameColSpan - 1);
                                            const $newTd = jQuery(`<td class="empty-td" data-linkedcolumn="${linkedColumn}"></td>`);
                                            $newTd.clone().insertBefore($tds[newIndex]);
                                            $newTd.insertBefore($designerTds[newIndex]);
                                        }
                                    }
                                }

                                //after moving column, check both sides of totalName col and merge if empty tds 
                                //may need to make this recursive to continue checking for adjacent empty tds
                                $designerTds = $designerRow.find('td');  //reassign with new element order
                                $tds = $row.find('td');

                                const newTotalNameIndex = $designerTds.filter('.total-name').index();
                                const $tdBefore = jQuery($designerTds[newTotalNameIndex - 1]);
                                const $tdAfter = jQuery($designerTds[newTotalNameIndex + 1]);
                                if ($tdBefore.length) {
                                    if ($tdBefore.hasClass('empty-td')) { //merge
                                        $tdBefore.remove();
                                        const colspan = parseInt(jQuery($designerTds[newTotalNameIndex]).attr('colspan'));
                                        jQuery($designerTds[newTotalNameIndex]).attr('colspan', colspan + 1);
                                        jQuery($tds[newTotalNameIndex]).attr('colspan', colspan + 1);
                                    }
                                }

                                if ($tdAfter.length) {
                                    if ($tdAfter.hasClass('empty-td')) { //merge
                                        $tdAfter.remove();
                                        const colspan = parseInt(jQuery($designerTds[newTotalNameIndex]).attr('colspan'));
                                        jQuery($designerTds[newTotalNameIndex]).attr('colspan', colspan + 1);
                                        jQuery($tds[newTotalNameIndex]).attr('colspan', colspan + 1);
                                    }
                                }
                                footerCount++;
                            }
                        }
                    }
                    $form.removeData('columnsmoved');
                }

                //change value field
                if (typeof $form.data('changevaluefield') != 'undefined') {
                    let footerCount = 0;
                    for (let i = 0; i < $rows.length; i++) {
                        const $row = jQuery($rows[i]);
                        const rowType = $row.attr('data-row');
                        const oldNewFields = $form.data('changevaluefield');
                        if (rowType == 'detail' || rowType == 'footer') {
                            const $td = $row.find(`[data-value="<!--{{${oldNewFields.oldfield}}}-->"]`);
                            $td.attr('data-value', `<!--{{${oldNewFields.newfield}}}-->`);
                            jQuery($table.find(`${rowSelector}[data-row="${rowType}"]`)[footerCount]).find(`[data-value="{{${oldNewFields.oldfield}}}"]`)
                                .attr('data-value', `{{${oldNewFields.newfield}}}`);

                            if (rowType == 'footer') {
                                footerCount++;
                            }
                        }
                    }
                    $form.removeData('changevaluefield');
                }

                //add
                if (typeof $form.data('addcolumn') != 'undefined') {
                    const newColumnData = $form.data('addcolumn');
                    const valueField = `NewColumn${newColumnData.newcolumnnumber}`;
                    let footerCount = 0;
                    for (let i = 0; i < $rows.length; i++) {
                        const $row = jQuery($rows[i]);
                        const rowType = $row.attr('data-row');
                        if (rowType == 'detail') {
                            const $td = jQuery(`<td data-value="<!--{{${valueField}}}-->"></td>`);
                            $row.append($td);  //add to row in wrapper (memory)
                            $table.find(`${rowSelector}[data-row="${rowType}"]`).append(`<td data-linkedcolumn="${valueField}" data-value="{{${valueField}}}"></td>`); //add to row on designer
                        } else if (rowType == 'header') {
                            const $designerTableRow = jQuery($table.find(`${rowSelector}`)[i]);
                            this.matchColumnCount($form, $table, $row, $designerTableRow);
                        } else if (rowType == 'footer') {
                            jQuery($table.find(`${rowSelector}[data-row="${rowType}"]`)[footerCount]).append(`<td class="empty-td" data-linkedcolumn="${valueField}"></td>`); //add to row on designer
                            $row.append(jQuery(`<td class="empty-td" data-linkedcolumn="${valueField}"></td>`));
                            footerCount++;
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
                        if (tdIsInRow) {
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
            $column = jQuery(`<th data-linkedcolumn="NewColumn${newColumnNumber}" data-valuefield="NewColumn${newColumnNumber}">New Column</th>`);
            $table.find('#columnHeader tr').append($column);
            this.setControlValues($form, $column);
            $column.data('newcolumn', true);
            this.TotalColumnCount++;
            $form.data('sectiontoupdate', 'tableheader');
            $form.data('addcolumn', { newcolumnnumber: newColumnNumber, tdcolspan: 1 });
            this.updateHTML($form, $table.find('#columnHeader tr'));
            newColumnNumber++;
        });

        $form.on('click', '#reportDesigner table thead#columnHeader tr th', e => {
            $column = jQuery(e.currentTarget);
            $form.data('sectiontoupdate', 'tableheader');
            $form.find('#controlProperties').show();
            this.setControlValues($form, $column);
            const linkedColumn = $column.attr('data-linkedColumn');
            $table.find('tbody td[data-linkedcolumn]').removeClass('highlight-cells');
            $table.find(`tbody td[data-linkedcolumn="${linkedColumn}"]`).addClass('highlight-cells');
        });

        //control properties events
        $form.on('change', '#controlProperties [data-datafield]', e => {  //todo - update when value fields are changed too.
            const $property = jQuery(e.currentTarget);
            const fieldname = $property.attr('data-datafield');
            const value = FwFormField.getValue2($property);

            if ($column.data('newcolumn')) {
                $form.data('sectiontoupdate', 'tableheader');
            }

            switch (fieldname) {
                case 'CaptionField':
                    $column.text(value);
                    break;
                case 'ValueField':
                    const oldField = $column.attr('data-valuefield');
                    $column.attr('data-valuefield', value);
                    $form.data('changevaluefield', { oldfield: oldField, newfield: value });
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
            $form.find('#controlProperties').show();
            this.setControlValues($form, $column);
            $form.data('sectiontoupdate', 'headerrow');
        });

        $form.on('click', '.total-name', e => {
            $column = jQuery(e.currentTarget);
            $form.find('#controlProperties').show();
            this.setControlValues($form, $column);
            $form.data('sectiontoupdate', 'footerrow');
        });

    }
    //----------------------------------------------------------------------------------------------
    //----------------------------------------------------------------------------------------------
    //updateColumnOrder($row: JQuery, $designerRow: JQuery, oldIndex: Number, newIndex: Number) {

    //}
    //----------------------------------------------------------------------------------------------
    setControlValues($form: JQuery, $column: JQuery) {
        FwFormField.setValueByDataField($form, 'CaptionField', $column.text(), $column.text());
        FwFormField.setValueByDataField($form, 'ValueField', $column.attr('data-valuefield'), $column.attr('data-valuefield'));
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
    linkColumns($form: JQuery, $table: JQuery) {                                                //added additional attribute (designer-only) to link table headers with tds 
        const $ths = $table.find('#columnHeader tr th');                                        //because blank footer total tds need to be linked without actually having
        for (let i = 0; i < $ths.length; i++) {                                                 //to add a new total field
            const $th = jQuery($ths[i]);
            const linkedColumn = $th.attr('data-valuefield');
            $th.attr('data-linkedcolumn', linkedColumn);
            $table.find(`[data-value="{{${linkedColumn}}}"]`)
                .attr('data-linkedcolumn', linkedColumn);
        }
    }
    //----------------------------------------------------------------------------------------------
};
//----------------------------------------------------------------------------------------------
var CustomReportLayoutController = new CustomReportLayout();