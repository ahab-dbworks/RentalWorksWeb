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

        $form.find('.tabpages').css('overflow', 'unset'); //override overflow:auto for sticky properties section

        this.loadModules($form);
        this.events($form);
        this.designerEvents($form);
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
        $form.find('#codeEditor').change();     // 10/25/2018 Jason H - updates the textarea formfield with the code editor html
        this.showHideControlProperties($form, 'hide');
        this.html = FwFormField.getValueByDataField($form, 'Html');

        const designerProvisioned = $form.find('[data-datafield="BaseReport"] :selected').attr('data-designer');
        if (designerProvisioned == 'true') {
            $form.find(`#reportDesigner`).empty();
            const $sections = jQuery(this.html).filter('[data-section]');
            let tableList: any = [];

            for (let i = 0; i < $sections.length; i++) {
                let $section = jQuery($sections[i]);
                const sectionType = $section.attr('data-section');
                let $wrapper;
                switch (sectionType) {
                    case 'header':
                        let headerFor = $section.attr('data-headerfor') || '';
                        $wrapper = jQuery(`<div class="header-section">
                                               <span>Report Header ${headerFor == '' ? '' : 'for ' + headerFor}</span>
                                               <div class="header-wrapper"></div>
                                           </div>`);
                        $section.contents().filter(function () { return (this.nodeType == 3) }).remove(); //removes text nodes (handlebars)
                        $wrapper.find('.header-wrapper').append($section);
                        break;
                    case 'table':
                        const tableName = jQuery($section).find('table').attr('data-tablename') || 'Default';
                        $wrapper = jQuery(`<div class="table-section">
                                            <span>Table: ${tableName}</span>
                                            <div class="table-wrapper" data-tablename="${tableName}">
                                        </div>`);
                        $section.contents().filter(function () { return (this.nodeType == 3) }).remove();
                        $wrapper.find('.table-wrapper').append($section);
                        const $table = $section.find('table');
                        const totalColumnCount = this.getTotalColumnCount($table, true);
                        $table.data('totalcolumncount', totalColumnCount);
                        tableList.push({ value: tableName, text: tableName });

                        const $columnHeaderRows = $table.find('#columnHeader tr');
                        for (let i = 0; i < $columnHeaderRows.length; i++) {
                            const $row = $columnHeaderRows[i];
                            this.addRowColumnSorting($form, $table, tableName, $row, 'columnheader');
                        }
                        break;
                    case 'footer':
                        break;
                }
                $form.find(`#reportDesigner`).append($wrapper);
            }
            FwFormField.loadItems($form.find('[data-datafield="TableName"]'), tableList);
        } else {
            let reportName: string = FwFormField.getValueByDataField($form, 'BaseReport');
            $form.find(`#reportDesigner`).empty().append(`<div>The ${reportName} is not yet provisioned for this Designer.  Use the HTML tab to make changes to this report layout.</div>`);
        }
    }
    //----------------------------------------------------------------------------------------------
    addRowColumnSorting($form, $table, tableName, $row, group) {
        Sortable.create($row, {
            group: group,
            onStart: e => {
                const $column = jQuery(e.item);
                const linkedColumnName = $column.attr('data-linkedcolumn');
                $form.find('#reportDesigner .highlight').removeClass('highlight');
                $table.find(`tbody td[data-linkedcolumn="${linkedColumnName}"]`).addClass('highlight');
                FwFormField.setValueByDataField($form, 'TableName', tableName);
                this.setControlValues($form, $column);
                this.showHideControlProperties($form, 'table');
            },
            onEnd: e => {
                const $column = jQuery(e.item);
                $column.removeAttr('draggable');
                const linkedColumnName = $column.attr('data-linkedcolumn');

                const $tr = jQuery(e.currentTarget);
                $form.data('columnsmoved', {
                    oldIndex: e.oldIndex,
                    newIndex: e.newIndex,
                    fromRowIndex: e.from.rowIndex,
                    toRowIndex: $column.parent().index()
                });
                $form.data('sectiontoupdate', 'tableheader');
                this.updateHTML($form, $table, $tr, $column);

                $form.attr('data-modified', 'true');
                $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
                $table.find('.highlight').removeClass('highlight');
                $table.find(`tbody td[data-linkedcolumn="${linkedColumnName}"]`).addClass('highlight');
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    updateHTML($form: JQuery, $table: JQuery, $tr: JQuery, $th?, rowIndex?) {
        let valuefield, $column;
        const sectionToUpdate = $form.data('sectiontoupdate');
        if (typeof sectionToUpdate != 'undefined' && typeof sectionToUpdate == 'string') {
            const $wrapper = jQuery('<div class="custom-report-wrapper"></div>');
            this.html = this.html.split('{{').join('<!--{{').split('}}').join('}}-->');      //comments out handlebars as a work-around for the displacement by the HTML parser  
            $wrapper.append(this.html);                                                      //append the original HTML to the wrapper.  this is done to combine the loose elements.
            if (sectionToUpdate == 'reportheader') {
                const headerFor = $form.data('reportheaderfor');
                if (headerFor == '') {
                    const newHTML = $form.find('#reportDesigner .header-wrapper').get(0).innerHTML;
                    $wrapper.find('[data-section="header"]').html(newHTML);
                } else {
                    const newHTML = $form.find(`#reportDesigner .header-wrapper [data-headerfor="${headerFor}"]`).get(0).outerHTML;
                    $wrapper.find(`[data-section="header"][data-headerfor="${headerFor}"]`).html(newHTML);
                }
                $form.removeData('reportheaderfor');
            } else {
                const tableName = $table.attr('data-tablename') || '';
                let tableNameSelector = tableName == '' ? '' : `table[data-tablename="${tableName}"]`;
                const totalColumnCount = $table.data('totalcolumncount');
                const newHTML = $tr.get(0).innerHTML.trim();
                switch (sectionToUpdate) {
                    case 'tableheader':
                        const rowSelector = `${tableNameSelector} tbody tr`;
                        const $rows = $wrapper.find(rowSelector);

                        //move columns to match column order in the header
                        if (typeof $form.data('columnsmoved') != 'undefined' && typeof $th != 'undefined') {
                            valuefield = $th.attr('data-valuefield');
                            const linkedColumn = $th.attr('data-linkedcolumn');
                            if (typeof valuefield != 'undefined') {
                                const sortIndex = $form.data('columnsmoved');
                                const oldIndex = sortIndex.oldIndex;
                                const newIndex = sortIndex.newIndex;
                                const oldRowIndex = sortIndex.oldRowIndex;
                                const newRowIndex = sortIndex.newRowIndex;
                                let $detailRowTds;
                                let rowIndex;
                                let footerRowIndex = 0;
                                let detailRowIndex = 0;
                                for (let i = 0; i < $rows.length; i++) {
                                    const $row = jQuery($rows[i]);
                                    const rowType = $row.attr('data-row');
                                    if (rowType == 'detail') {
                                        rowIndex = detailRowIndex;
                                    } else if (rowType == 'footer') {
                                        rowIndex = footerRowIndex;
                                    } else {
                                        rowIndex = 0;
                                    }
                                    const $designerRow = jQuery($table.find(`tbody tr[data-row="${rowType}"]`)[rowIndex]);
                                    let $designerTds = $designerRow.find('td');
                                    const $designerTd = $designerRow.find(`[data-linkedcolumn="${linkedColumn}"]`);
                                    let $tds = $row.find('td');
                                    if (rowType == 'detail') {
                                        $detailRowTds = $designerTds;
                                        const $movedTd = $row.find(`[data-linkedcolumn="${linkedColumn}"]`);
                                        if ($movedTd.length) {
                                            if (oldIndex > newIndex) {
                                                $movedTd.insertBefore($tds[newIndex]);
                                                $designerTd.insertBefore($designerTds[newIndex]);
                                            } else {
                                                $movedTd.insertAfter($tds[newIndex]);
                                                $designerTd.insertAfter($designerTds[newIndex]);
                                            }
                                        }
                                        detailRowIndex++;
                                    } else if (rowType == 'footer') {
                                        const totalNameColSpan = parseInt($designerTds.filter('.total-name').attr('colspan')) || 1;
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
                                                const $movedTd = $row.find(`[data-linkedcolumn="${linkedColumn}"]`);
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

                                        const mergeTds = () => {
                                            let count = 0;
                                            $designerTds = $designerRow.find('td');  //reassign with new element order
                                            $tds = $row.find('td');
                                            let newTotalNameIndex = $designerTds.filter('.total-name').index();
                                            const $tdBefore = jQuery($designerTds[newTotalNameIndex - 1]);
                                            const $tdAfter = jQuery($designerTds[newTotalNameIndex + 1]);
                                            if ($tdBefore.length) {
                                                if ($tdBefore.hasClass('empty-td')) { //merge
                                                    $tdBefore.remove();
                                                    jQuery($tds[newTotalNameIndex - 1]).remove();
                                                    const colspan = parseInt(jQuery($designerTds[newTotalNameIndex]).attr('colspan'));
                                                    jQuery($designerTds[newTotalNameIndex]).attr('colspan', colspan + 1);
                                                    jQuery($tds[newTotalNameIndex]).attr('colspan', colspan + 1);
                                                    count++;
                                                }
                                            }
                                            if ($tdAfter.length) {
                                                if ($tdAfter.hasClass('empty-td')) { //merge
                                                    $tdAfter.remove();
                                                    jQuery($tds[newTotalNameIndex + 1]).remove();
                                                    const colspan = parseInt(jQuery($designerTds[newTotalNameIndex]).attr('colspan'));
                                                    jQuery($designerTds[newTotalNameIndex]).attr('colspan', colspan + 1);
                                                    jQuery($tds[newTotalNameIndex]).attr('colspan', colspan + 1);
                                                    count++;
                                                }
                                            }
                                            if (count != 0) {
                                                mergeTds();
                                            }
                                        }
                                        mergeTds();
                                        footerRowIndex++;
                                    }
                                }
                            }
                            $form.removeData('columnsmoved');
                        }

                        //change value field
                        if (typeof $form.data('changevaluefield') != 'undefined') {
                            let detailRowCount = 0;
                            for (let i = 0; i < $rows.length; i++) {
                                const $row = jQuery($rows[i]);
                                const rowType = $row.attr('data-row');
                                const oldNewFields = $form.data('changevaluefield');
                                if (rowType == 'detail'/* || rowType == 'footer'*/) {
                                    const $td = $row.find(`[data-linkedcolumn="${oldNewFields.linkedcolumn}"]`);
                                    const $designerTd = jQuery($table.find(`tbody tr[data-row="${rowType}"]`)[detailRowCount]).find(`[data-linkedcolumn="${oldNewFields.linkedcolumn}"]`);
                                    $td.attr('data-value', `<!--{{${oldNewFields.newfield}}}-->`);
                                    $designerTd.attr('data-value', `{{${oldNewFields.newfield}}}`);
                                    detailRowCount++;
                                    //if (rowType == 'footer') {
                                    //    footerCount++;
                                    //}
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
                                    const $td = jQuery(`<td data-linkedcolumn="${valueField}" data-value="<!--{{${valueField}}}-->"></td>`);
                                    $row.append($td);  //add to row in wrapper (memory)
                                    $table.find(`tbody tr[data-row="${rowType}"]`).append(`<td data-linkedcolumn="${valueField}" data-value="{{${valueField}}}"></td>`); //add to row on designer
                                } else if (rowType == 'header') {
                                    const $designerTableRow = jQuery($table.find(`tbody tr`)[i]);
                                    this.matchColumnCount($form, $table, $row, $designerTableRow);
                                } else if (rowType == 'footer') {
                                    jQuery($table.find(`tbody tr[data-row="${rowType}"]`)[footerCount]).append(`<td class="empty-td" data-linkedcolumn="${valueField}"></td>`); //add to row on designer
                                    $row.append(jQuery(`<td class="empty-td" data-linkedcolumn="${valueField}"></td>`));
                                    footerCount++;
                                }
                            }
                            $form.removeData('addcolumn');
                        }

                        //delete
                        if (typeof $form.data('deletefield') != 'undefined') {
                            const linkedColumn = $form.data('deletefield').linkedcolumn;
                            let index = 0;
                            let detailIndex = 0;
                            let footerIndex = 0;
                            let rowRemoved = false;
                            for (let i = 0; i < $rows.length; i++) {
                                const $row = jQuery($rows[i]);
                                const rowType = $row.attr('data-row');
                                if (rowType == 'detail') {
                                    index = detailIndex;
                                } else if (rowType == 'footer') {
                                    index = footerIndex;
                                } else {
                                    index = 0;
                                }

                                const $designerRow = jQuery($table.find(`tbody tr[data-row="${rowType}"]`)[index]);
                                const $designerTd = $designerRow.find(`[data-linkedcolumn="${linkedColumn}"]`);
                                const $td = $row.find(`[data-linkedcolumn="${linkedColumn}"]`);
                                if (rowType == 'detail') {
                                    $td.remove();
                                    $designerTd.remove();
                                    detailIndex++;
                                } else if (rowType == 'footer') {
                                    if ($designerTd.length) {
                                        $designerTd.remove();
                                        $td.remove();
                                    } else {
                                        const colspan = parseInt(jQuery($designerRow.find('.total-name')).attr('colspan'));
                                        jQuery($row.find('.total-name')).attr('colspan', colspan - 1);
                                        jQuery($designerRow.find('.total-name')).attr('colspan', colspan - 1);
                                    }
                                    footerIndex++;
                                }
                            }
                            $form.removeData('deletefield');
                        }

                        if (typeof rowIndex == 'undefined') {
                            rowIndex = 0;
                        }

                        jQuery($wrapper.find(`${tableNameSelector} #columnHeader tr`)[rowIndex]).html(newHTML);                     //replace old headers
                        break;
                    case 'headerrow':
                        valuefield = $tr.attr('data-valuefield');
                        $column = $wrapper.find(`table[data-tablename="${tableName}"] .header-row[data-valuefield="${valuefield}"]`);
                        if ($column.length) {
                            $column.html(newHTML);
                        }
                        break;
                    case 'footerrow':
                        valuefield = $tr.attr('data-valuefield');
                        $column = $wrapper.find(`${tableNameSelector} .total-name[data-valuefield="${valuefield}"]`);
                        if ($column.length) {
                            $column.html(newHTML);
                        }
                        break;
                    case 'addrow-header':
                        $wrapper.find(`${tableNameSelector} #columnHeader`).append($tr.clone(true));
                        const $lastDetailRow = $wrapper.find(`${tableNameSelector} tr[data-row="detail"]:last`)
                        const $newDetailRow = $table.find(`.preview[data-row="detail"]`);
                        $newDetailRow.removeAttr('class');
                        $newDetailRow.children().text('').removeAttr('class');
                        $newDetailRow.clone(true).insertAfter($lastDetailRow);
                        this.addRowColumnSorting($form, $table, tableName, $tr.get(0), 'columnheader');
                        break;
                    case 'addrow-subheader':
                        // $wrapper.find(`${tableNameSelector}`).append($tr.clone(true));
                        break;
                    //case 'addrow-detail':
                    //const $lastDetailRow = $wrapper.find(`${tableNameSelector} tr[data-row="detail"]:last`)
                    //$tr.clone(true).insertAfter($lastDetailRow);
                    //break;
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
    designerEvents($form: JQuery) {
        let $table, $row, $column;
        let newColumnNumber = 1;
        const $addColumn = $form.find('.addColumn');
        const $addRow = $form.find('.addRow');
        let $headerField;

        $form.on('click', '#reportDesigner .header-wrapper', e => {
            $form.find('#reportDesigner .selected').removeClass('selected');
            jQuery(e.currentTarget).addClass('selected');
            this.showHideControlProperties($form, 'hide');
        });

        $form.on('click', '#reportDesigner .header-wrapper span', e => {
            e.stopPropagation();
            $headerField = jQuery(e.currentTarget);
            $form.find('#reportDesigner .highlight').removeClass('highlight');
            $headerField.addClass('highlight');
            this.showHideControlProperties($form, 'header');
            const value = $headerField.text();
            FwFormField.setValueByDataField($form, 'HeaderField', value);
            const styling = $headerField.attr('style') || '';
            FwFormField.setValueByDataField($form, 'HeaderFieldStyle', styling);
        });

        $form.on('change', '[data-datafield="TableName"]', e => {
            const tableName = FwFormField.getValueByDataField($form, 'TableName');
            $form.find('#reportDesigner .selected').removeClass('selected');
            $form.find(`#reportDesigner .table-wrapper[data-tablename="${tableName}"]`).addClass('selected');
            $table = $form.find('.table-wrapper.selected table');
        });

        $form.on('click', '#reportDesigner .table-wrapper', e => {
            $table = $form.find('.table-wrapper.selected table');
            const tableName = jQuery(e.currentTarget).attr('data-tablename');
            FwFormField.setValueByDataField($form, 'TableName', tableName, tableName, true);
            this.addRowLoadSelect($form, tableName);
            this.addColumnLoadSelect($form, tableName);
            this.showHideControlProperties($form, 'tablewrapper');
        });

        //control properties events
        $form.on('change', '#controlProperties [data-datafield]:not(.add-row-column)', e => {
            const $property = jQuery(e.currentTarget);
            const fieldname = $property.attr('data-datafield');
            const value = FwFormField.getValue2($property);

            if (typeof $column != 'undefined') {
                if ($column.data('newcolumn')) {
                    $form.data('sectiontoupdate', 'tableheader');
                }
            }

            switch (fieldname) {
                case 'HeaderField':
                    if (typeof $headerField != 'undefined') {
                        const headerFor = jQuery($headerField.parents('[data-section="header"]')).attr('data-headerfor') || '';
                        $headerField.text(value);
                        $form.data('sectiontoupdate', 'reportheader');
                        $form.data('reportheaderfor', headerFor);
                    }
                    break;
                case 'HeaderFieldStyle':
                    if (typeof $headerField != 'undefined') {
                        const headerFor = jQuery($headerField.parents('[data-section="header"]')).attr('data-headerfor') || '';
                        $headerField.attr('style', value);
                        $form.data('sectiontoupdate', 'reportheader');
                        $form.data('reportheaderfor', headerFor);
                    }
                    break;
                case 'CaptionField':
                    if (typeof $column != 'undefined') {
                        $form.data('sectiontoupdate', 'tableheader');
                        $column.text(value);
                    }
                    break;
                case 'ValueField':
                    if (typeof $column != 'undefined') {
                        const linkedColumn = $column.attr('data-linkedcolumn');
                        const oldField = $column.attr('data-valuefield');
                        $form.data('sectiontoupdate', 'tableheader');
                        $column.attr('data-valuefield', value);
                        FwFormField.setValueByDataField($form, 'CaptionField', value);
                        $column.text(value);
                        $form.data('changevaluefield', { linkedcolumn: linkedColumn, oldfield: oldField, newfield: value });
                    }
                    break;
            }
            const section = $form.data('sectiontoupdate');
            if (section == 'tableheader') {
                if (typeof $column != 'undefined') {
                    const $row = $column.parents('tr');
                    const rowIndex = $row.index();
                    this.updateHTML($form, $table, $row, null, rowIndex);
                }
            } else if (section == 'headerrow' || section == 'footerrow') {
                this.updateHTML($form, $table, $column);
            } else {
                this.updateHTML($form, null, null);
            }
        });

        //add header column
        $addColumn.on('click', e => {
            $column = jQuery(`<th data-linkedcolumn="NewColumn${newColumnNumber}" data-valuefield="NewColumn${newColumnNumber}">New Column</th>`);
            $table.find('#columnHeader tr').append($column);
            this.setControlValues($form, $column);
            $column.data('newcolumn', true);
            let totalColumnCount = this.getTotalColumnCount($form, true)
            $table.data('totalcolumncount', totalColumnCount);
            $form.data('sectiontoupdate', 'tableheader');
            $form.data('addcolumn', { newcolumnnumber: newColumnNumber, tdcolspan: 1 });
            this.updateHTML($form, $table, $table.find('#columnHeader tr'));
            newColumnNumber++;
            this.showHideControlProperties($form, 'table');
        });

        //add rows
        $addRow.on('click', e => {
            const rowType = FwFormField.getValueByDataField($form, 'AddRow');
            if (rowType != '') {
                switch (rowType) {
                    case 'header':
                        $form.data('sectiontoupdate', 'addrow-header');
                        break;
                    case 'sub-header':
                        $form.data('sectiontoupdate', 'addrow-subheader');
                        break;
                    //case 'detail':
                    //    $form.data('sectiontoupdate', 'addrow-detail');
                    //    break;
                }
                if (typeof $row != 'undefined') {
                    $row.removeAttr('class');
                    $row.children().text('New Column').removeAttr('class');
                    this.updateHTML($form, $table, $row);
                    const tableName = FwFormField.getValueByDataField($form, 'TableName');
                    this.addColumnLoadSelect($form, tableName);
                }
            } else {
                $form.find('[data-datafield="AddRow"]').addClass('error');
                FwNotification.renderNotification('WARNING', 'Select a Row Type to add.');
            }
        });

        //Select Row Type change events
        $form.on('change', '[data-datafield="AddRow"]', e => {
            const rowType = FwFormField.getValueByDataField($form, 'AddRow');
            $form.find('#reportDesigner table .preview').remove();
            if (rowType != '') {
                $form.find('[data-datafield="AddRow"]').removeClass('error');
                //add preview
                $row = this.addRowPreview($form, rowType);
            }
        });

        //delete table header column
        $form.on('click', '.delete-column', e => {
            if (typeof $column != 'undefined') {
                const linkedColumn = jQuery($column).attr('data-linkedcolumn');
                const colspan = parseInt(jQuery($column).attr('colspan')) || 1;
                $column.remove();
                //let totalColumnCount = $table.data('totalcolumncount');
                let totalColumnCount = this.getTotalColumnCount($table, true);
                $table.data('totalcolumncount', totalColumnCount);
                $form.data('sectiontoupdate', 'tableheader');
                if (typeof linkedColumn != 'undefined') {
                    $form.data('deletefield', { linkedcolumn: linkedColumn, tdcolspan: colspan });
                }
                this.updateHTML($form, $table, $table.find('#columnHeader tr'));
                this.showHideControlProperties($form, 'hide');
            }
        });

        //hover over header fields
        $form.find('#columnHeader tr th').hover(e => { //mouseenter
            $table = jQuery(e.currentTarget).parents('table');
            const valueFieldName = jQuery(e.currentTarget).attr('data-valuefield');
            $table.find(`tbody td[data-value="{{${valueFieldName}}}"]`).addClass('hover-cell');
        }, e => { // mouseleave
            $table = jQuery(e.currentTarget).parents('table');
            const valueFieldName = jQuery(e.currentTarget).attr('data-valuefield');
            $table.find(`tbody td[data-value="{{${valueFieldName}}}"]`).removeClass('hover-cell');
        });

        $form.on('click', '#reportDesigner table thead#columnHeader tr th', e => {
            e.stopPropagation();
            const tableName = jQuery(e.currentTarget).parents('.table-wrapper').attr('data-tablename');
            FwFormField.setValueByDataField($form, 'TableName', tableName, tableName, true);
            $table = jQuery(e.currentTarget).parents('table');
            $column = jQuery(e.currentTarget);
            $form.data('sectiontoupdate', 'tableheader');
            this.setControlValues($form, $column);
            const linkedColumn = $column.attr('data-linkedColumn');
            $form.find('#reportDesigner .highlight').removeClass('highlight');
            $table.find(`[data-linkedcolumn="${linkedColumn}"]`).addClass('highlight');
            this.showHideControlProperties($form, 'table');
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
    //moveColumns($row: JQuery, $designerRow: JQuery, oldIndex: Number, newIndex: Number) {

    //}
    //----------------------------------------------------------------------------------------------
    setControlValues($form: JQuery, $column: JQuery) {
        FwFormField.setValueByDataField($form, 'CaptionField', $column.text(), $column.text());
        FwFormField.setValueByDataField($form, 'ValueField', $column.attr('data-valuefield'), $column.attr('data-valuefield'));
    }
    //----------------------------------------------------------------------------------------------
    addRowLoadSelect($form, tableName) {
        const rowTypes = [
            { value: 'header', text: 'Header Row' },
            { value: 'sub-header', text: 'Sub-Header Row' },
            //{ value: 'detail', text: 'Detail Row' }
        ];
        FwFormField.loadItems($form.find('[data-datafield="AddRow"]'), rowTypes);
    }
    //----------------------------------------------------------------------------------------------
    addRowPreview($form, rowType) {
        let $table, $row, $row2, $newRow, $newDetailRow;
        const tableName = FwFormField.getValueByDataField($form, 'TableName');
        if (tableName === '' || tableName === 'Default') {
            $table = $form.find(`[data-section="table"] table`);
        } else {
            $table = $form.find(`table[data-tablename="${tableName}"]`);
        }

        if ($table.length > 0) {
            const colspan = $table.data('totalcolumncount') || 1;

            //build header and detail rows with linkedcolumns
            const html = [];
            const detailRowHtml = [];
            html.push(`<tr class="preview"`);
            if (rowType === 'sub-header') {
                html.push(` data-row="${rowType}">`);
            } else {
                html.push(`>`);
            }
            detailRowHtml.push(`<tr class="preview" data-row="detail" data-rowtype="{{RowType}}">`);
            for (let i = 0; i < colspan; i++) {
                const newId = program.uniqueId(8);
                html.push(`<th class="placeholder" data-linkedcolumn="${newId}" data-valuefield="NewColumn">PREVIEW</th>`);
                detailRowHtml.push(`<td class="placeholder" data-linkedcolumn="${newId}" data-value="{{New Column}}"></td>`);
            }
            html.push(`</tr>`);
            detailRowHtml.push(`</tr>`);
            $newRow = jQuery(html.join(''));
            $newDetailRow = jQuery(detailRowHtml.join(''));

            if (rowType === 'header') {
                $row = $table.find('thead tr:last');
            } else {
                $row = $table.find('tr[data-row="sub-header"]:last');
            }

            //detail row
            $row2 = $table.find('tr[data-row="detail"]:last');

            $newDetailRow.insertAfter($row2);
            $newRow.insertAfter($row);
        }
        return $newRow;
    }
    //----------------------------------------------------------------------------------------------
    addColumnLoadSelect($form, tableName) {
        const columnTypes = [];
        const $table = $form.find(`table[data-tablename="${tableName}"]`);
        const $headerRows = $table.find('thead tr');
        for (let i = 0; i < $headerRows.length; i++) {
            columnTypes.push({ value: `header|${i}`, text: `Header Row ${i + 1}` });
        }
        const $subheaderRows = $table.find('tbody tr[data-row="sub-header"]');
        for (let i = 0; i < $subheaderRows.length; i++) {
            columnTypes.push({ value: `subheader|${i}`, text: `Sub-Header Row ${i + 1}` });
        }
        //const $detailRows = $table.find('tbody tr[data-row="detail"]');
        //for (let i = 0; i < $detailRows.length; i++) {
        //    columnTypes.push({ value: `detail|${i}` + i, text: `Detail Row ${i + 1}`});
        //}
        FwFormField.loadItems($form.find('[data-datafield="AddColumn"]'), columnTypes);
    }
    //----------------------------------------------------------------------------------------------
    getTotalColumnCount($table: JQuery, isTableHeader: boolean, $row?: JQuery) {
        let count = 0;
        let $columns;
        if (isTableHeader) {
            $columns = $table.find(`#columnHeader tr:first th`);
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
        let totalColumnCount = $table.data('totalcolumncount');
        if (rowColumnCount != totalColumnCount) {
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
                if (rowColumnCount > totalColumnCount) {
                    colspan -= colspanDelta;
                } else if (rowColumnCount < totalColumnCount) {
                    colspan += colspanDelta;
                }

                $td.attr('colspan', colspan);
                $designerTableRow.find(selector).attr('colspan', colspan);

                this.matchColumnCount($form, $table, $row, $designerTableRow);
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    //linkColumns($form: JQuery, $table: JQuery) {                                                //added additional attribute (designer-only) to link table headers with tds 
    //    const $ths = $table.find('#columnHeader tr th');                                        //because blank footer total tds need to be linked without actually having
    //    for (let i = 0; i < $ths.length; i++) {                                                 //to add a new total field
    //        const $th = jQuery($ths[i]);
    //        if (typeof $th.attr('data-linkedcolumn') == 'undefined') {
    //            const linkedColumn = $th.attr('data-valuefield');
    //            $th.attr('data-linkedcolumn', linkedColumn);
    //            $table.find(`[data-value="{{${linkedColumn}}}"]`)                               //05-14-20 consider adding linkedcolumns as new required attribute on templates
    //                .attr('data-linkedcolumn', linkedColumn);                                   //for cases where users add a new column and set the value field identical to an existing column
    //        }
    //    }
    //}
    //----------------------------------------------------------------------------------------------
    showHideControlProperties($form: JQuery, section: string) {
        const $controlProperties = $form.find('#controlProperties');
        switch (section) {
            case 'header':
                $controlProperties.children(`:not('.header-controls')`).hide();
                $controlProperties.children('.header-controls').show();
                $controlProperties.show();
                break;
            case 'tablewrapper':
                $controlProperties.children(`:not('.table-controls')`).hide();
                $controlProperties.children('.table-controls').show();
                $controlProperties.show();
                break;
            case 'table':
                $controlProperties.children('.header-controls').hide();
                $controlProperties.children(`:not('.header-controls')`).show();
                $controlProperties.show();
                break;
            case 'footer':
                break;
            case 'hide':
                $controlProperties.hide();
                break;
        }
    }
    //----------------------------------------------------------------------------------------------
};
//----------------------------------------------------------------------------------------------
var CustomReportLayoutController = new CustomReportLayout();