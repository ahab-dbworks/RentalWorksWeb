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

        //temp 
        FwFormField.setValueByDataField($form, 'BaseReport', 'OutContractReport', null, true);

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
        if ($form.data('usereportlayout')) {
            const customReportLayoutId = FwFormField.getValueByDataField($form, 'CustomReportLayoutId');
            const customReportLayoutDesc = FwFormField.getValueByDataField($form, 'Description');
            FwFormField.setValueByDataField($form.data('$reportfrontend'), 'CustomReportLayoutId', customReportLayoutId, customReportLayoutDesc);
            FwPopup.destroyPopup($form.closest('.fwpopup'));
        } else {
            FwFormField.disable($form.find('[data-datafield="BaseReport"]'));
            $form.attr('data-modified', 'false');
            $form.find('.btn[data-type="SaveMenuBarButton"]').addClass('disabled');
        }
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
            const reportCaption = $this.text();
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
                const fullName = sessionStorage.getItem('fullname');
                FwFormField.setValueByDataField($form, 'Description', `${fullName}'s ${reportCaption} Report`);
            } else {
                $form.find('.modulefields, #reportDesigner').empty();
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
        const $headerFields = $form.find('.header-fields-drag');
        $headerFields.empty();
        FwAppData.apiMethod(true, 'GET', `api/v1/${reportName}/emptyobject`, null, FwServices.defaultTimeout,
            response => {
                let customFields = response._Custom.map(obj => ({ fieldname: obj.FieldName, fieldtype: obj.FieldType }));
                let allValidFields: any = [];
                const fieldsToExclude = ['DateStamp', 'RecordTitle', '_Custom', '_Fields', 'DateFields'];
                for (let key of Object.keys(response)) {
                    //if (!fieldsToExclude.includes(key)) {
                    if (fieldsToExclude.indexOf(key) <0 ) {
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
                    $headerFields.append(`<span>${allValidFields[i].value}</span>`);
                    if (allValidFields[i].hasOwnProperty("NestedItems")) {
                        for (const key of Object.keys(allValidFields[i].NestedItems)) {
                            if (key != '_Custom') {
                                modulefields.append(`<div data-iscustomfield="false" data-isnested="true" data-parentfield="${allValidFields[i].value}" style="text-indent:1em;">${key}</div>`);
                                $headerFields.append(`<span data-parentfield="${allValidFields[i].value}" style="text-indent:1em;">${key}</span>`);
                            }
                        }
                    }
                }

                //add draggable fields to designer
                this.addNestedSorting($form, $headerFields, true);

                FwFormField.loadItems($form.find('[data-datafield="ValueField"]'), $form.data('validdatafields'));

            }, ex => FwFunc.showError(ex), $form);
    }
    //----------------------------------------------------------------------------------------------
    //addNestedItemArrayToValueFields($form: JQuery, itemArray: string) {
    //    const $validFields = $form.data('validdatafields');
    //    const $valueField = $form.find('[data-datafield="ValueField"]');
    //    const $nestedItems = $validFields.filter(obj => { return obj.Value == itemArray });
    //}
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
        FwBrowse.renderGrid({
            nameGrid: 'CustomReportLayoutGroupGrid',
            gridSecurityId: 'N5ZpGhzZvahV2',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    CustomReportLayoutId: FwFormField.getValueByDataField($form, 'CustomReportLayoutId')
                };
            },
            beforeSave: (request: any) => {
                request.CustomReportLayoutId = FwFormField.getValueByDataField($form, 'CustomReportLayoutId')
            }
        });

        FwBrowse.renderGrid({
            nameGrid: 'CustomReportLayoutUserGrid',
            gridSecurityId: 'JjgsAURBr00RK',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    CustomReportLayoutId: FwFormField.getValueByDataField($form, 'CustomReportLayoutId')
                };
            },
            beforeSave: (request: any) => {
                request.CustomReportLayoutId = FwFormField.getValueByDataField($form, 'CustomReportLayoutId')
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    events($form) {
        //Refreshes and shows CodeMirror upon clicking HTML tab
        $form.on('click', '[data-type="tab"][data-caption="HTML"]', e => {
            this.codeMirror.refresh();
        });

        //Reload General Tab
        $form.on('click', '[data-type="tab"][data-caption="General"]', e => {
            this.renderTab($form, 'Designer');
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
                        this.addReportHeaderSorting($form, $section.find('.rpt-flexrow'));
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
                        tableList.push({ value: tableName, text: tableName });

                        const $columnHeaderRows = $table.find('#columnHeader tr');
                        for (let i = 0; i < $columnHeaderRows.length; i++) {
                            const $row = $columnHeaderRows[i];
                            this.addRowColumnSorting($form, $table, tableName, $row, 'columnheader');
                        }

                        const $sortableRows = $table.find('tr[data-sort="true"]');
                        for (let i = 0; i < $sortableRows.length; i++) {
                            const $row = $sortableRows[i];
                            this.addRowColumnSorting($form, $table, tableName, $row, jQuery($row).attr('data-row') + `-${i}`);
                        }

                        break;
                    case 'footer':
                        $wrapper = jQuery(`<div class="footer-section">
                                               <span>Footer</span>
                                               <div class="footer-wrapper"></div>
                                           </div>`);
                        $section.contents().filter(function () { return (this.nodeType == 3) }).remove(); //removes text nodes (handlebars)
                        this.addReportHeaderSorting($form, $section.find('.rpt-flexrow'));
                        $wrapper.find('.footer-wrapper').append($section);
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
                    linkedRow: $tr.attr('data-linkedrow'),
                    toRowIndex: e.item.parentElement.rowIndex,
                    rowType: $tr.attr('data-row'),
                    theadIndex: $tr.parent('thead').index() //jasonh - 08/07/20 experimental support for multiple theads (so that first thead can be used as a label to separate columns into sections)
                });
                $form.data('updatetype', 'tableheader');
                this.updateHTML($form, $table, $tr, $column);

                $form.attr('data-modified', 'true');
                $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
                $table.find('.highlight').removeClass('highlight');
                $table.find(`[data-linkedcolumn="${linkedColumnName}"]`).addClass('highlight');
            },
            animation: 100
        });
    }
    //----------------------------------------------------------------------------------------------
    addReportHeaderSorting($form: JQuery, $elements: JQuery) {
        //for sorting cols within rows
        for (let i = 0; i < $elements.length; i++) {
            const $element = jQuery($elements[i]);
            Sortable.create($element[0], {
                group: 'row',
                onStart: e => {
                    //
                },
                onEnd: e => {
                    const $reportHeaderSection = jQuery(e.item).closest('[data-section]');
                    this.updateReportHeader($form, $reportHeaderSection);
                },
                delay: 500,
                animation: 100,
                dragoverBubble: true
            });

            if ($element.find('.rpt-flexcolumn').length > 0){
                this.addReportHeaderColSorting($form, $element.find('.rpt-flexcolumn'));
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    addReportHeaderColSorting($form: JQuery, $elements: JQuery) {
        //for sorting rows within columns
        for (let i = 0; i < $elements.length; i++) {
            const $element = jQuery($elements[i]);
            Sortable.create($element[0], {
                group: 'column',
                onStart: e => {
                    //
                },
                onEnd: e => {
                    const $reportHeaderSection = jQuery(e.item).closest('[data-section]');
                    this.updateReportHeader($form, $reportHeaderSection);
                },
                delay: 500,
                animation: 100,
                dragoverBubble: true
            });

            if ($element.find('.rpt-nested-flexrow').length > 0) {
                const $nestedElements = $element.find('.rpt-nested-flexrow');
                this.addNestedSorting($form, $nestedElements, false);
            }
        }
      
    }
    //----------------------------------------------------------------------------------------------
    addNestedSorting($form: JQuery, $nestedElements: JQuery, clone?: boolean) {
        for (let j = 0; j < $nestedElements.length; j++) {
            const $nestedElement = jQuery($nestedElements[j]);
            const group: any = { name: 'nested' };
            if (clone) {
                group.pull = 'clone';
                group.put = false;
            };

            Sortable.create($nestedElement[0], {
                group: group,
                onStart: e => {
                    //
                },
                onEnd: e => {
                    if (jQuery(e.item).parent().hasClass('rpt-nested-flexrow')) {
                        if (jQuery(e.currentTarget).hasClass('header-fields-drag')) {
                            jQuery(e.item).text(`{{${jQuery(e.item).text()}}}`);
                        }
                        const $reportHeaderSection = jQuery(e.item).closest('[data-section]');
                        this.updateReportHeader($form, $reportHeaderSection);
                    }
                },
                delay: 500,
                animation: 100,
                fallbackOnBody: true,
                dragoverBubble: true
            });
        }
    }
    //----------------------------------------------------------------------------------------------
    addButtonMenu($form: JQuery) {
        let $buttonmenu = $form.find('.add-text-field[data-type="btnmenu"]');
        let $addEmptyContainer = FwMenu.generateButtonMenuOption('ADD CONTAINER'),
            $addEmptyCol = FwMenu.generateButtonMenuOption('ADD COLUMN'),
            $addEmptyRow = FwMenu.generateButtonMenuOption('ADD ROW');

        $addEmptyContainer.addClass('add-empty-container');
        $addEmptyCol.addClass('add-empty-col');
        $addEmptyRow.addClass('add-empty-row');

        let menuOptions = [];
        menuOptions.push($addEmptyRow, $addEmptyCol, $addEmptyContainer);

        FwMenu.addButtonMenuOptions($buttonmenu, menuOptions);
    }
    //----------------------------------------------------------------------------------------------
    updateReportHeader($form: JQuery, $headerSection: JQuery) {
        const headerFor = $headerSection.attr('data-headerfor') || '';

        if ($headerSection.attr('data-section') === 'footer') {
            $form.data('isfooter', true);
        }
        $form.data('updatetype', 'reportheader');
        $form.data('reportheaderfor', headerFor);
        this.updateHTML($form, null, null);
    }
    //----------------------------------------------------------------------------------------------
    updateHTML($form: JQuery, $table: JQuery, $tr: JQuery, $th?) {
        let $cachedRow, $cachedColumn, newHTML, rowIndex;
        const updateType = $form.data('updatetype');
        if (typeof updateType != 'undefined' && typeof updateType == 'string') {
            const $wrapper = jQuery('<div class="custom-report-wrapper"></div>');
            this.html = this.html.split('{{').join('<!--{{').split('}}').join('}}-->');      //comments out handlebars as a work-around for the displacement by the HTML parser  
            $wrapper.append(this.html);                                                      //append the original HTML to the wrapper.  this is done to combine the loose elements.
            if (updateType == 'reportheader') {
                let sectionType;
                const headerFor = $form.data('reportheaderfor');
                if ($form.data('isfooter')) {
                    sectionType = 'footer';
                    $form.removeData('isfooter');
                } else {
                    sectionType = 'header';
                }

                if (headerFor == '') {
                    newHTML = $form.find(`#reportDesigner .${sectionType}-wrapper [data-section="${sectionType}"]`).get(0).innerHTML;
                    $wrapper.find(`[data-section="${sectionType}"]`).html(newHTML);
                } else {
                    newHTML = $form.find(`#reportDesigner .${sectionType}-wrapper [data-headerfor="${headerFor}"]`).get(0).outerHTML;
                    $wrapper.find(`[data-section="${sectionType}"][data-headerfor="${headerFor}"]`).html(newHTML);
                }
                $form.removeData('reportheaderfor');
            } else {
                const tableName = $table.attr('data-tablename') || '';
                let tableNameSelector = tableName == '' ? '' : `table[data-tablename="${tableName}"]`;
                switch (updateType) {
                    case 'tableheader':
                        const rowSelector = `${tableNameSelector} tbody tr`;
                        const $cachedRows = $wrapper.find(rowSelector);

                        //move columns to match column order in the header
                        if (typeof $form.data('columnsmoved') != 'undefined' && typeof $th != 'undefined') {
                            this.moveColumns($form, $wrapper, tableNameSelector, $table, $cachedRows, $th);
                        }

                        //change value field
                        if (typeof $form.data('changevaluefield') != 'undefined') {
                            this.changeValueField($form, $wrapper, tableNameSelector, $table, $cachedRows);
                        }

                        //change sub-header caption
                        if (typeof $form.data('updatesubheader') !== 'undefined') {
                            const captionData = $form.data('updatesubheader');
                            const linkedColumn = captionData.linkedcolumn;
                            const caption = captionData.caption;
                            const $cachedTd = jQuery($wrapper.find(`${tableNameSelector} [data-row="${captionData.rowtype}"] [data-linkedcolumn="${linkedColumn}"]`));
                            $cachedTd.removeClass('highlight').text(caption);
                            $form.removeData('updatesubheader');
                        }

                        //add column
                        if (typeof $form.data('addcolumn') != 'undefined') {
                            this.addColumn($form, $table, $cachedRows);
                        }

                        //delete column
                        if (typeof $form.data('deletefield') != 'undefined') {
                            this.deleteColumn($form, $table, $cachedRows);
                        }

                        const $columnHeaders = $table.find('#columnHeader').clone();
                        $columnHeaders.find('.new-column').removeClass('new-column');
                        $columnHeaders.find('.highlight').removeClass('highlight');
                        newHTML = $columnHeaders.get(0).innerHTML.trim();
                        jQuery($wrapper.find(`${tableNameSelector} #columnHeader`)).html(newHTML);                     //replace old headers
                        break;
                    case 'headerrow':
                    case 'footerrow':
                        newHTML = $tr.get(0).innerHTML.trim();
                        rowIndex = $form.data('rowindex');
                        $cachedRow = jQuery($wrapper.find(`${tableNameSelector} tr`)[rowIndex]);
                        if ($cachedRow.length) {
                            $cachedRow.html(newHTML);
                        }
                        $form.removeData('rowindex');
                        break;
                    case 'addrow':
                        const $newHeaderRow = $tr.clone();
                        const $lastHeaderRow = $wrapper.find(`${tableNameSelector} #columnHeader tr:last`);
                        $newHeaderRow.find('.new-column').removeClass('new-column');
                        $newHeaderRow.insertAfter($lastHeaderRow);
                        const $lastDetailRow = $wrapper.find(`${tableNameSelector} tr[data-row="detail"]:last`);
                        const $newDetailRow = $table.find(`.new-row[data-row="detail"]`);
                        const $newDetailRowClone = $newDetailRow.clone();
                        $newDetailRowClone.find('.new-column').removeClass('new-column');
                        $newDetailRowClone.insertAfter($lastDetailRow);
                        this.addRowColumnSorting($form, $table, tableName, $tr.get(0), 'columnheader');
                        break;
                    case 'deleterow':
                        const linkedColumn = $th.attr('data-linkedcolumn');
                        const $cachedRowsToDelete = $wrapper.find(`${tableNameSelector} tr [data-linkedcolumn="${linkedColumn}"]`).parents('tr');
                        const $designerRowsToDelete = $table.find(`[data-linkedcolumn="${linkedColumn}"]`).parents('tr');
                        $cachedRowsToDelete.remove();
                        $designerRowsToDelete.remove();
                        break;
                    case 'style':
                        this.updateElementStyle($form, $wrapper, tableNameSelector, $tr, $th);
                        break;
                }
            }

            this.html = $wrapper.get(0).innerHTML;                                           //get new report HTML
            this.html = this.html.split('<!--{{').join('{{').split('}}-->').join('}}');      //un-comment handlebars
            FwFormField.setValueByDataField($form, 'Html', this.html);
            this.codeMirror.setValue(this.html);                                             //update codemirror (HTML tab) with new HTML
            $form.removeData('updatetype');
        }
    }
    //----------------------------------------------------------------------------------------------
    designerEvents($form: JQuery) {
        let $table, $row, $column, $headerField, $reportSection;
        const $addColumn = $form.find('.addColumn');
        const $addRow = $form.find('.addRow');

        //control properties events
        $form.on('change', '#controlProperties [data-datafield]', e => {
            let $row, linkedColumn, rowType, linkedRow;
            const $property = jQuery(e.currentTarget);
            const fieldname = $property.attr('data-datafield');
            const value = FwFormField.getValue2($property);

            if (typeof $column != 'undefined') {
                $row = $column.parents('tr');
                rowType = $row.attr('data-row');
                linkedRow = $row.attr('data-linkedrow');
                if ($column.data('newcolumn')) {
                    $form.data('updatetype', 'tableheader');
                }
            }

            switch (fieldname) {
                case 'HeaderField':
                    if (typeof $headerField != 'undefined') {
                        const headerFor = jQuery($headerField.parents('[data-section="header"]')).attr('data-headerfor') || '';
                        $headerField.text(value);
                        if ($reportSection.attr('data-section') === 'footer') {
                            $form.data('isfooter', true);
                        }
                        $form.data('updatetype', 'reportheader');
                        $form.data('reportheaderfor', headerFor);
                    }
                    break;
                case 'HeaderFieldStyle':
                    if (typeof $headerField != 'undefined') {
                        const headerFor = jQuery($headerField.parents('[data-section="header"]')).attr('data-headerfor') || '';
                        $headerField.attr('style', value);
                        if ($reportSection.attr('data-section') === 'footer') {
                            $form.data('isfooter', true);
                        }
                        $form.data('updatetype', 'reportheader');
                        $form.data('reportheaderfor', headerFor);
                    }
                    break;
                case 'ValueField':
                    if (typeof $column != 'undefined') {
                        linkedColumn = $column.attr('data-linkedcolumn');
                        const oldField = $column.attr('data-valuefield');
                        $column.removeClass('new-column');
                        $form.data('updatetype', 'tableheader');
                        $column.attr('data-valuefield', value);
                        FwFormField.setValueByDataField($form, 'CaptionField', value);
                        $column.text(value);
                        $form.data('changevaluefield',
                            {
                                linkedcolumn: linkedColumn,
                                linkedrow: linkedRow,
                                rowtype: rowType,
                                oldfield: oldField,
                                newfield: value
                            });
                    }
                    break;
                case 'CaptionField':
                    if (typeof $column != 'undefined') {
                        switch (rowType) {
                            case 'main-header':
                                $form.data('updatetype', 'tableheader');
                                break;
                            case 'header':
                                $form.data('updatetype', 'headerrow');
                                $form.data('rowindex', $row.get(0).rowIndex);
                                break;
                            case 'sub-header':
                            case 'linked-sub-header':
                                linkedColumn = $column.attr('data-linkedcolumn');
                                $form.data('updatesubheader', {
                                    linkedcolumn: linkedColumn,
                                    caption: value,
                                    rowtype: rowType,
                                    linkedrow: linkedRow
                                });
                                $form.data('updatetype', 'tableheader');
                                break;
                            case 'footer':
                                $form.data('updatetype', 'footerrow');
                                $form.data('rowindex', $row.get(0).rowIndex);
                                break;
                        }
                        $column.text(value);
                    }
                    break;
                case 'CellStyleField':
                    if (typeof $column != 'undefined') {
                        $column.attr('style', value);
                        $form.data('updatetype', 'style');
                    }
                    break;
            }
            const updateType = $form.data('updatetype');

            switch (updateType) {
                case 'tableheader':
                    if (typeof $row != 'undefined') {
                        this.updateHTML($form, $table, $row);
                    }
                    break;
                case 'headerrow':
                case 'footerrow':
                    this.updateHTML($form, $table, $row, $column);
                    break;
                case 'style':
                    this.updateHTML($form, $table, $row, $column);
                    break;
                default:
                    this.updateHTML($form, null, null);
                    break;
            }
        });

        //add table header column
        $addColumn.on('click', e => {
            const newId = program.uniqueId(8);
            $column = jQuery(`<th data-linkedcolumn="${newId}" class="new-column"></th>`);
            const $tableHeaderRows = jQuery($table.find('#columnHeader tr'));
            $tableHeaderRows.append($column);

            //update linkedcolumn when adding heading columns to multiple header rows
            for (let i = 1; i < $tableHeaderRows.length; i++) {
                const $addedColumn = jQuery($tableHeaderRows[i]).find(`[data-linkedcolumn="${newId}"]`);
                $addedColumn.attr('data-linkedcolumn', newId + '-' + i);
            }

            this.setControlValues($form, $column);
            $column.data('newcolumn', true);
            $form.data('updatetype', 'tableheader');
            $form.data('addcolumn', { newcolumnid: newId, tdcolspan: 1 });
            this.updateHTML($form, $table, $table.find('#columnHeader tr:first'));
            this.showHideControlProperties($form, 'table');
        });

        //add table header rows
        $addRow.on('click', e => {
            $row = this.addNewHeaderRow($form);
            $form.data('updatetype', 'addrow');
            this.updateHTML($form, $table, $row);
        });

        //delete table header column
        $form.on('click', '.delete-column', e => {
            if (typeof $column != 'undefined') {
                const linkedColumn = jQuery($column).attr('data-linkedcolumn');
                const colspan = parseInt(jQuery($column).attr('colspan')) || 1;
                const $row = $column.parent('tr');
                const rowType = $row.attr('data-row');
                const rowIndex = $row.index();
                $column.remove();
                $form.data('updatetype', 'tableheader');
                if (typeof linkedColumn != 'undefined' || rowType === 'sub-detail') {
                    $form.data('deletefield', { linkedcolumn: linkedColumn, tdcolspan: colspan, rowindex: rowIndex, deletedfromrowtype: rowType });
                }
                this.updateHTML($form, $table, $row);
                this.showHideControlProperties($form, 'hide');
            }
        });

        //delete table row
        $form.on('click', '.delete-row', e => {
            if (typeof $column !== 'undefined') {
                $row = $column.parents('tr');
                let rowIndex = $row.index();
                if (rowIndex) {
                    const linkedColumn = jQuery($column).attr('data-linkedcolumn');
                    const $linkedColumns = $table.find(`[data-linkedcolumn="${linkedColumn}"]`);
                    $linkedColumns.siblings().addClass('highlight');
                    const $confirmation = FwConfirmation.renderConfirmation(`Delete Row`, `Delete the highlighted row(s)?`);
                    const $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
                    FwConfirmation.addButton($confirmation, 'No', true);

                    $yes.on('click', () => {
                        FwConfirmation.destroyConfirmation($confirmation);
                        //delete row 
                        $form.data('updatetype', 'deleterow');
                        $form.data('deleterow', { rowindex1: rowIndex })
                        this.updateHTML($form, $table, $row, $column);
                    });
                }
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

        $form.on('click', '#reportDesigner table tr th, #reportDesigner table tr[data-row="sub-detail"] td', e => {
            e.stopPropagation();
            $column = jQuery(e.currentTarget);
            $table = $column.parents('table');
            $form.data('updatetype', 'tableheader');
            this.setControlValues($form, $column);
            const linkedColumn = $column.attr('data-linkedColumn');
            this.highlightElement($form, $table.find(`[data-linkedcolumn="${linkedColumn}"]`));
            this.showHideControlProperties($form, 'table');
            if ($column.parents('tr').index()) {
                $form.find('.delete-row').parent('div').show();
            } else {
                $form.find('.delete-row').parent('div').hide();
            }
        });

        //allow td styling
        $form.on('click', '#reportDesigner table tr td', e => {
            e.stopPropagation();
            $column = jQuery(e.currentTarget);
            $table = $column.parents('table');
            this.highlightElement($form, $column);
            const tableName = $table.parents('.table-wrapper').attr('data-tablename');
            FwFormField.setValueByDataField($form, 'TableName', tableName, tableName, true);
            FwFormField.setValueByDataField($form, 'CellStyleField', $column.attr('style') || '');
            this.showHideControlProperties($form, 'td');
        });

        //header row
        $form.on('click', '.header-row', e => {
            e.stopPropagation();
            $column = jQuery(e.currentTarget);
            this.setControlValues($form, $column);
            this.showHideControlProperties($form, 'footerrow');
            $form.data('updatetype', 'headerrow');
        });

        //footer row
        $form.on('click', '.total-name', e => {
            e.stopPropagation();
            $column = jQuery(e.currentTarget);
            this.setControlValues($form, $column);
            this.showHideControlProperties($form, 'headerrow');
            $form.data('updatetype', 'footerrow');
        });


        //  Report Header Events
        //-----------------------------------------------------------------------------------------------

        //Add empty report container row
        $form.on('click', '.add-empty-container', e => {
            e.stopPropagation();
            if (typeof $reportSection != 'undefined') {
                const $emptyContainer = jQuery(`<div class="rpt-flexrow" style="min-height:100px;"></div>`);
                this.addReportHeaderSorting($form, $emptyContainer);
                jQuery($reportSection).append($emptyContainer);
                this.updateReportHeader($form, $reportSection);
                this.highlightElement($form, $emptyContainer);
            }
        });

        //Add empty report header column
        $form.on('click', '.add-empty-col', e => {
            e.stopPropagation();
            if (typeof $reportSection != 'undefined') {
                const $emptyCol = jQuery(`<div class="rpt-flexcolumn"></div>`);
                if ($reportSection.find('.rpt-flexrow').length > 0) {
                    this.addReportHeaderColSorting($form, $emptyCol);
                    jQuery($reportSection.find('.rpt-flexrow:last-of-type')).append($emptyCol);
                    this.updateReportHeader($form, $reportSection);
                    this.highlightElement($form, $emptyCol);
                } else {
                    FwFunc.showError('Container not found.  A container must be added before adding a new column.');
                }
            }
        });

        //Add empty report header row
        $form.on('click', '.add-empty-row', e => {
            e.stopPropagation();
            if (typeof $reportSection != 'undefined') {
                const $emptyRow = jQuery(`<div class="rpt-nested-flexrow"></div>`);
                if ($reportSection.find('.rpt-flexcolumn').length > 0) {
                    this.addNestedSorting($form, $emptyRow, false);
                    jQuery($reportSection.find('.rpt-flexcolumn:last-of-type')).append($emptyRow);
                    this.updateReportHeader($form, $reportSection);
                    this.highlightElement($form, $emptyRow);
                } else {
                    FwFunc.showError('Column not found.  A column must be added before adding a new row.');
                }
            }
        });

        //Add empty text field
        $form.on('click', '.add-text-field', e => {
            if (typeof $reportSection != 'undefined') {
                const $emptyText = jQuery(`<span>New Text Field</span>`);
                if ($reportSection.find('.rpt-nested-flexrow').length > 0) {
                    jQuery($reportSection.find('.rpt-nested-flexrow:last')).append($emptyText);
                    this.updateReportHeader($form, $reportSection);
                    $emptyText.click();
                } else {
                    FwFunc.showError('Row not found.  A row must be added before adding a new text field.');
                }
            }
        });

        $form.on('click', '#reportDesigner .header-wrapper, #reportDesigner .footer-wrapper', e => {
            const $this = jQuery(e.currentTarget);
            $form.find('#reportDesigner .selected').removeClass('selected');
            $this.addClass('selected');
            $reportSection = $this.find('[data-section]');
            this.showHideControlProperties($form, 'headerwrapper');
        });

        $form.on('click', '#reportDesigner .header-wrapper span, #reportDesigner .footer-wrapper span', e => {
            e.stopPropagation();
            $headerField = jQuery(e.currentTarget);
            $reportSection = $headerField.closest('[data-section]');
            this.highlightElement($form, $headerField);
            this.showHideControlProperties($form, 'header');
            $form.find('[data-datafield="HeaderField"]').show();
            const value = $headerField.text();
            FwFormField.setValueByDataField($form, 'HeaderField', value);
            const styling = $headerField.attr('style') || '';
            FwFormField.setValueByDataField($form, 'HeaderFieldStyle', styling);
        });

        $form.on('click', '#reportDesigner [data-section="header"] div, #reportDesigner [data-section="footer"] div', e => {
            e.stopPropagation();
            const $this = jQuery(e.currentTarget);
            $headerField = $this;
            $reportSection = $this.closest('[data-section]');
            this.highlightElement($form, $this);
            this.showHideControlProperties($form, 'header');
            $form.find('[data-datafield="HeaderField"]').hide();
            const styling = $headerField.attr('style') || '';
            FwFormField.setValueByDataField($form, 'HeaderFieldStyle', styling);
        });

        //Delete header element
        $form.on('click', '.delete-component', e => {
            if (typeof $reportSection != 'undefined') {
                const $element = $reportSection.find('.highlight');
                if ($element.length > 0) {
                    $element.remove();
                    this.updateReportHeader($form, $reportSection);
                }
            }
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
            this.showHideControlProperties($form, 'tablewrapper');
        });

    }
    //----------------------------------------------------------------------------------------------
    setControlValues($form: JQuery, $column: JQuery) {
        const tableName = $column.parents('.table-wrapper').attr('data-tablename');
        FwFormField.setValueByDataField($form, 'TableName', tableName, tableName, true);
        FwFormField.setValueByDataField($form, 'CaptionField', $column.text(), $column.text());
        FwFormField.setValueByDataField($form, 'ValueField', $column.attr('data-valuefield'), $column.attr('data-valuefield'));
        FwFormField.setValueByDataField($form, 'CellStyleField', $column.attr('style') || '');
    }
    //----------------------------------------------------------------------------------------------
    highlightElement($form: JQuery, $elements: JQuery) {
        $form.find('#reportDesigner .highlight').removeClass('highlight');
        for (let i = 0; i < $elements.length; i++) {
            jQuery($elements[i]).addClass('highlight');
        }
    }
    //----------------------------------------------------------------------------------------------
    addNewHeaderRow($form: JQuery) {
        let $table, $row, $row2, $newHeaderRow, $newDetailRow;
        const tableName = FwFormField.getValueByDataField($form, 'TableName');

        if (tableName === '' || tableName === 'Default') {
            $table = $form.find(`[data-section="table"] table`);
        } else {
            $table = $form.find(`table[data-tablename="${tableName}"]`);
        }

        if ($table.length > 0) {
            const colspan = this.getTotalColumnCount($table, true);
            //build header and detail rows with linkedcolumns
            const html = [];
            const detailRowHtml = [];
            html.push(`<tr data-row="main-header" class="new-row">`);
            detailRowHtml.push(`<tr class="new-row" data-row="detail" data-rowtype="{{RowType}}">`);
            for (let i = 0; i < colspan; i++) {
                const newId = program.uniqueId(8);
                html.push(`<th class="new-column" data-linkedcolumn="${newId}"></th>`);
                detailRowHtml.push(`<td class="new-column" data-linkedcolumn="${newId}"></td>`);
            }
            html.push(`</tr>`);
            detailRowHtml.push(`</tr>`);
            $newHeaderRow = jQuery(html.join(''));
            $newDetailRow = jQuery(detailRowHtml.join(''));
            $row = $table.find('thead tr:last');

            //detail row
            $row2 = $table.find('tr[data-row="detail"]:last');

            $newDetailRow.insertAfter($row2);
            $newHeaderRow.insertAfter($row);
        }
        return $newHeaderRow;
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
    showHideControlProperties($form: JQuery, section: string) {
        const $controlProperties = $form.find('#controlProperties');
        switch (section) {
            case 'headerwrapper':
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
            case 'headerrow':
            case 'footerrow':
                $controlProperties.children(`:not('[data-datafield="CellStyleField"]'):not('[data-datafield="CaptionField"]')`).hide();
                $controlProperties.children(`[data-datafield="CellStyleField"], [data-datafield="CaptionField"]`).show();
                $controlProperties.show();
                break;
            case 'td':
                $controlProperties.children(`:not('[data-datafield="CellStyleField"]')`).hide();
                $controlProperties.children(`[data-datafield="CellStyleField"]`).show();
                $controlProperties.show();
                break;
            case 'hide':
                $controlProperties.hide();
                break;
        }
    }
    //----------------------------------------------------------------------------------------------
    changeValueField($form: JQuery, $wrapper: JQuery, tableName: string, $table: JQuery, $cachedRows: JQuery) {
        let $designerRow, $cachedRow, $cachedTd, $designerTd, html, detailRowIndex = 0, subHeaderRowIndex = 0, subDetailRowIndex = 0, linkedSubHeaderRowIndex = 0;
        const changes = $form.data('changevaluefield');
        for (let i = 0; i < $cachedRows.length; i++) {
            const $row = jQuery($cachedRows[i]);
            const rowType = $row.attr('data-row');

            switch (rowType) {
                case 'detail':
                    $cachedTd = $row.find(`[data-linkedcolumn="${changes.linkedcolumn}"]`);
                    $designerTd = jQuery($table.find(`tbody tr[data-row="${rowType}"]`)[detailRowIndex]).find(`[data-linkedcolumn="${changes.linkedcolumn}"]`);
                    if ($cachedTd.length && $designerTd.length) {
                        $cachedTd.attr('data-value', `<!--{{${changes.newfield}}}-->`);
                        $designerTd.removeClass('new-column');
                        $designerTd.attr('data-value', `{{${changes.newfield}}}`);
                    }
                    detailRowIndex++;
                    break;
                case 'linked-sub-header':
                    $cachedTd = $row.find(`[data-linkedcolumn="${changes.linkedcolumn}"]`);
                    $designerTd = jQuery($table.find(`tbody tr[data-row="${rowType}"][data-linkedrow="${changes.linkedrow}"]`)[linkedSubHeaderRowIndex]).find(`[data-linkedcolumn="${changes.linkedcolumn}"]`);
                    if ($cachedTd.length && $designerTd.length) {
                        $cachedTd.attr('data-valuefield', changes.newfield);
                        $cachedTd.text(changes.newfield);
                        $designerTd.attr('data-valuefield', changes.newfield);
                        $designerTd.text(changes.newfield);
                        $designerTd.removeClass('new-column');
                        $designerRow = jQuery($table.find('tr[data-row="linked-sub-header"]')[linkedSubHeaderRowIndex]).clone();
                        $designerRow.find('.highlight').removeClass('highlight');
                        html = $designerRow.get(linkedSubHeaderRowIndex).innerHTML.trim();
                        $cachedRow = jQuery($wrapper.find(`${tableName} tr[data-row="linked-sub-header"]`)[linkedSubHeaderRowIndex]);
                        $cachedRow.html(html);
                    }
                    linkedSubHeaderRowIndex++;
                    break;
                case 'sub-header':
                    $designerRow = jQuery($table.find('tr[data-row="sub-header"]')[subHeaderRowIndex]).clone();
                    $designerRow.find('.highlight').removeClass('highlight');
                    html = $designerRow.get(subHeaderRowIndex).innerHTML.trim();
                    $cachedRow = jQuery($wrapper.find(`${tableName} tr[data-row="sub-header"]`)[subHeaderRowIndex]);
                    $cachedRow.html(html);
                    subHeaderRowIndex++;
                    break;
                case 'sub-detail':
                    $cachedTd = $row.find(`[data-linkedcolumn="${changes.linkedcolumn}"]`);
                    $designerTd = jQuery($table.find(`tbody tr[data-row="${rowType}"]`)[subDetailRowIndex]).find(`[data-linkedcolumn="${changes.linkedcolumn}"]`);
                    if ($cachedTd.length && $designerTd.length) {
                        $cachedTd.attr('data-value', `<!--{{${changes.newfield}}}-->`);
                        $designerTd.attr('data-value', `{{${changes.newfield}}}`);
                        $designerTd.removeClass('new-column');
                        $designerRow = jQuery($table.find('tr[data-row="sub-detail"]')[subDetailRowIndex]).clone();
                        $designerRow.find('.highlight').removeClass('highlight');
                        html = $designerRow.get(subDetailRowIndex).innerHTML.trim();
                        $cachedRow = jQuery($wrapper.find(`${tableName} tr[data-row="sub-detail"]`)[subDetailRowIndex]);
                        $cachedRow.html(html);
                    }
                    subDetailRowIndex++;
                    break;
            }
        }

        //update linked main header
        if (changes.rowtype === 'linked-sub-header') {
            const $headerRow = jQuery($table.find(`tr[data-row="main-header"][data-linkedrow="${changes.linkedrow}"]`));
            const $headerTh = $headerRow.find(`[data-linkedcolumn="${changes.linkedcolumn}"]`);
            $headerRow.find('.highlight').removeClass('highlight');
            $headerTh.attr('data-valuefield', changes.newfield);
            $headerTh.text(changes.newfield);
            html = $headerRow.get(0).innerHTML.trim();
            $cachedRow = jQuery($wrapper.find(`${tableName} tr[data-row="main-header"][data-linkedrow="${changes.linkedrow}"]`));
            $cachedRow.html(html);
        }

        $form.removeData('changevaluefield');
    }
    //----------------------------------------------------------------------------------------------
    addColumn($form: JQuery, $table: JQuery, $cachedRows: JQuery) {
        let $newColumn, detailRowIndex = 0, linkedSubHeaderRowIndex = 0, subHeaderRowIndex = 0, subDetailRowIndex = 0, footerRowIndex = 0, detailLinkedCol;
        const newColumnData = $form.data('addcolumn');
        const linkedColumn = newColumnData.newcolumnid;
        for (let i = 0; i < $cachedRows.length; i++) {
            const $row = jQuery($cachedRows[i]);
            const rowType = $row.attr('data-row');

            switch (rowType) {
                case 'detail':
                    detailLinkedCol = linkedColumn;
                    if (detailRowIndex > 0) {
                        detailLinkedCol += `-${detailRowIndex}`;
                    }
                    $newColumn = jQuery(`<td data-linkedcolumn="${detailLinkedCol}"></td>`);
                    $row.append($newColumn);
                    jQuery($table.find(`tbody tr[data-row="${rowType}"]`)[detailRowIndex]).append($newColumn.clone().addClass('new-column')); //add to row on designer
                    detailRowIndex++;
                    break;
                case 'linked-sub-header':
                    $newColumn = jQuery(`<th data-linkedcolumn="${linkedColumn}"></th>`);
                    $row.append($newColumn);
                    jQuery($table.find(`tbody tr[data-row="${rowType}"]`)[linkedSubHeaderRowIndex]).append($newColumn.clone().addClass('new-column')); //add to row on designer
                    linkedSubHeaderRowIndex++;
                    break;
                case 'sub-header':
                    $newColumn = jQuery(`<th data-linkedcolumn="${linkedColumn}-sub${subHeaderRowIndex}"></th>`);
                    $row.append($newColumn);
                    jQuery($table.find(`tbody tr[data-row="${rowType}"]`)[subHeaderRowIndex]).append($newColumn.clone().addClass('new-column')); //add to row on designer
                    subHeaderRowIndex++;
                    break;
                case 'sub-detail':
                    $newColumn = jQuery(`<td data-linkedcolumn="${linkedColumn}-sub${subDetailRowIndex}"></td>`);
                    $row.append($newColumn);
                    jQuery($table.find(`tbody tr[data-row="${rowType}"]`)[subDetailRowIndex]).append($newColumn.clone().addClass('new-column')); //add to row on designer
                    subDetailRowIndex++;
                    break;
                case 'footer':
                    jQuery($table.find(`tbody tr[data-row="${rowType}"]`)[footerRowIndex]).append(`<td class="empty-td" data-linkedcolumn="${linkedColumn}"></td>`); //add to row on designer
                    $row.append(jQuery(`<td class="empty-td" data-linkedcolumn="${linkedColumn}"></td>`));
                    footerRowIndex++;
                    break;
            }
        }

        $form.removeData('addcolumn');
    }
    //----------------------------------------------------------------------------------------------
    deleteColumn($form: JQuery, $table: JQuery, $cachedRows: JQuery) {
        let html, index = 0, detailRowIndex = 0, subHeaderRowIndex = 0, subDetailRowIndex = 0, footerRowIndex = 0;
        const linkedColumn = $form.data('deletefield').linkedcolumn;
        const rowIndex = $form.data('deletefield').rowindex;
        const deletedFromRowType = $form.data('deletefield').deletedfromrowtype;

        for (let i = 0; i < $cachedRows.length; i++) {
            const $row = jQuery($cachedRows[i]);
            const rowType = $row.attr('data-row');
            switch (rowType) {
                case 'detail':
                    index = detailRowIndex;
                    break;
                case 'sub-header':
                    index = subHeaderRowIndex;
                    break;
                case 'sub-detail':
                    index = subDetailRowIndex;
                    break;
                case 'footer':
                    index = footerRowIndex;
                    break;
                default:
                    index = 0;
                    break;
            }

            const $designerRow = jQuery($table.find(`tbody tr[data-row="${rowType}"]`)[index]);
            const $designerTd = $designerRow.find(`[data-linkedcolumn="${linkedColumn}"]`);
            const $cachedTd = $row.find(`[data-linkedcolumn="${linkedColumn}"]`);

            switch (rowType) {
                case 'detail':
                    $cachedTd.remove();
                    $designerTd.remove();
                    detailRowIndex++;
                    break;
                case 'sub-header':
                    if (deletedFromRowType === 'sub-header') {
                        //update cached sub-header html
                        html = $table.find('tr[data-row="sub-header"]').get(subHeaderRowIndex).innerHTML.trim();
                        $row.html(html);
                    }
                    subHeaderRowIndex++;
                    break;
                case 'sub-detail':
                    index = subDetailRowIndex;
                    if (deletedFromRowType === 'sub-header' || deletedFromRowType === 'sub-detail') {
                        if (deletedFromRowType === 'sub-header') {
                            $designerTd.remove();
                            $cachedTd.remove();
                        }
                        //update cached sub-detail html
                        html = $table.find('tr[data-row="sub-detail"]').get(subDetailRowIndex).innerHTML.trim();
                        $row.html(html);
                    }
                    subDetailRowIndex++;
                    break;
                case 'footer':
                    if ($designerTd.length) {
                        $designerTd.remove();
                        $cachedTd.remove();
                    } else if (rowIndex === 0) {  //if there is no total column, only adjust total-name colspan if a column from the main (first) row is deleted
                        const colspan = parseInt(jQuery($designerRow.find('.total-name')).attr('colspan'));
                        jQuery($row.find('.total-name')).attr('colspan', colspan - 1);
                        jQuery($designerRow.find('.total-name')).attr('colspan', colspan - 1);
                    }
                    footerRowIndex++;
                    break;
            }
        }

        $form.removeData('deletefield');
    }
    //----------------------------------------------------------------------------------------------
    updateElementStyle($form: JQuery, $cachedReport: JQuery, tableNameSelector: string, $designerRow: JQuery, $column: JQuery) {
        let style, linkedColumn, rowType, $cachedTd;
        if (typeof $designerRow == 'undefined') {
            $designerRow = $column.parents('tr');
        }
        if (typeof $column !== 'undefined') {
            rowType = $designerRow.attr('data-row');
            linkedColumn = $column.attr('data-linkedcolumn');
            style = $column.attr('style');
            $cachedTd = $cachedReport.find(`${tableNameSelector} [data-row="${rowType}"] [data-linkedcolumn="${linkedColumn}"]`);
            if ($cachedTd.length) {
                $cachedTd.attr('style', style);
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    moveColumns($form: JQuery, $wrapper: JQuery, tableNameSelector: string, $table: JQuery, $cachedRows: JQuery, $th: JQuery) {
        let html, cellType, $detailRowTds, rowIndex = 0, footerRowIndex = 0, detailRowIndex = 0, linkedSubHeaderRowIndex = 0, subHeaderRowIndex = 0, subDetailRowIndex = 0, skipDetailRows = false;
        const linkedColumn = $th.attr('data-linkedcolumn');
        const sortIndex = $form.data('columnsmoved');
        const oldIndex = sortIndex.oldIndex;
        const newIndex = sortIndex.newIndex;
        const oldRowIndex = sortIndex.fromRowIndex;
        const newRowIndex = sortIndex.toRowIndex;
        const columnMovedRowType = sortIndex.rowType;
        const linkedRow = sortIndex.linkedRow;

        switch (columnMovedRowType) {
            case 'main-header':
                for (let i = 0; i < $cachedRows.length; i++) {
                    const $row = jQuery($cachedRows[i]);
                    const rowType = $row.attr('data-row');

                    switch (rowType) {
                        case 'detail':
                            cellType = 'td';
                            rowIndex = detailRowIndex;
                            break;
                        case 'linked-sub-header':
                            cellType = 'th';
                            rowIndex = linkedSubHeaderRowIndex;
                            break;
                        case 'sub-header':
                            cellType = 'th';
                            rowIndex = subHeaderRowIndex;
                            break;
                        case 'sub-detail':
                            cellType = 'td';
                            rowIndex = subDetailRowIndex;
                            break;
                        case 'footer':
                            cellType = 'td';
                            rowIndex = footerRowIndex;
                            break;
                        default:
                            cellType = 'td';
                            rowIndex = 0;
                            break;
                    }

                    const $designerRow = jQuery($table.find(`tbody tr[data-row="${rowType}"]`)[rowIndex]);
                    const $designerTd = $designerRow.find(`[data-linkedcolumn="${linkedColumn}"]`);
                    let $designerTds = $designerRow.find(cellType);
                    let $cachedTds = $row.find(cellType);

                    if (rowType == 'detail') {
                        if (!skipDetailRows) {
                            if (oldRowIndex === newRowIndex) {
                                if (detailRowIndex === newRowIndex || sortIndex.theadIndex) {
                                    $detailRowTds = $designerTds;
                                    const $movedTd = $row.find(`[data-linkedcolumn="${linkedColumn}"]`);
                                    if ($movedTd.length) {
                                        if (oldIndex > newIndex) {
                                            $movedTd.insertBefore($cachedTds[newIndex]);
                                            $designerTd.insertBefore($designerTds[newIndex]);
                                        } else {
                                            $movedTd.insertAfter($cachedTds[newIndex]);
                                            $designerTd.insertAfter($designerTds[newIndex]);
                                        }
                                    }
                                    detailRowIndex++;
                                } else {
                                    detailRowIndex++;
                                }
                            } else if (oldRowIndex !== newRowIndex) {
                                const $movedTdRow = jQuery($cachedRows.filter(`[data-row="${rowType}"]`)[oldRowIndex]);
                                const $movedTd = $movedTdRow.find(`[data-linkedcolumn="${linkedColumn}"]`);
                                const $movedTdToRow = jQuery($cachedRows.filter(`[data-row="${rowType}"]`)[newRowIndex]);
                                const $movedTdToRowTds = $movedTdToRow.find(cellType);

                                const $designerTdRow = jQuery($table.find(`tbody tr[data-row="${rowType}"]`)[oldRowIndex]);
                                const $designerTdToRow = jQuery($table.find(`tbody tr[data-row="${rowType}"]`)[newRowIndex]);
                                const $designerTdToRowTds = $designerTdToRow.find(cellType);
                                const $designerTd = $designerTdRow.find(`[data-linkedcolumn="${linkedColumn}"]`);

                                if (newIndex === 0) {
                                    $movedTd.insertBefore($movedTdToRowTds[newIndex]);
                                    $designerTd.insertBefore($designerTdToRowTds[newIndex]);
                                } else {
                                    $movedTd.insertAfter($cachedTds[newIndex - 1]);
                                    $designerTd.insertAfter($designerTdToRowTds[newIndex - 1]);
                                }
                                skipDetailRows = true;
                            }
                        }
                    } else if (rowType === 'sub-header') {
                        subHeaderRowIndex++;
                    } else if (rowType === 'linked-sub-header') {
                        if (typeof linkedRow != 'undefined' && linkedRow === $designerRow.attr('data-linkedrow')) {
                            const $movedTd = $row.find(`[data-linkedcolumn="${linkedColumn}"]`);
                            if ($movedTd.length) {
                                if (oldIndex > newIndex) {
                                    $movedTd.insertBefore($cachedTds[newIndex]);
                                    $designerTd.insertBefore($designerTds[newIndex]);
                                } else {
                                    $movedTd.insertAfter($cachedTds[newIndex]);
                                    $designerTd.insertAfter($designerTds[newIndex]);
                                }
                            }
                        }
                        linkedSubHeaderRowIndex++;
                    } else if (rowType === 'sub-detail') {
                        subDetailRowIndex++;
                    } else if (rowType == 'footer') {
                        if (newRowIndex === 0 || sortIndex.theadIndex) {
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
                                                $cachedTds.filter('.total-name').attr('colspan', totalNameColSpan + 1);
                                                $movedTd.remove();
                                            } else {
                                                $movedTd.insertBefore($cachedTds[newIndex - (totalNameColSpan - 1)]);
                                                $designerTd.insertBefore($designerTds[newIndex - (totalNameColSpan - 1)]);
                                            }
                                        } else if ((newIndex > totalNameIndex) && (newIndex <= endTotalNameColumnIndex)) { //moved within the totalName column
                                            $designerTds.filter('.total-name').attr('colspan', totalNameColSpan + 1); //merge
                                            $designerTd.remove();
                                            $cachedTds.filter('.total-name').attr('colspan', totalNameColSpan + 1);
                                            $movedTd.remove();
                                        } else { //moved to the left of totalName column
                                            if (newIndex <= totalNameIndex) { //merge
                                                $designerTds.filter('.total-name').attr('colspan', totalNameColSpan + 1);
                                                $designerTd.remove();
                                                $cachedTds.filter('.total-name').attr('colspan', totalNameColSpan + 1);
                                                $movedTd.remove();
                                            } else {
                                                $movedTd.insertBefore($cachedTds[newIndex]);
                                                $designerTd.insertBefore($designerTds[newIndex]);
                                            }
                                        }
                                    } else if (newIndex > oldIndex) { //moving RIGHT
                                        if (newIndex > endTotalNameColumnIndex) { //moved to the right of the totalName column
                                            $movedTd.insertAfter($cachedTds[newIndex - (totalNameColSpan - 1)]);
                                            $designerTd.insertAfter($designerTds[newIndex - (totalNameColSpan - 1)]);
                                        } else if ((newIndex >= totalNameIndex) && (newIndex <= endTotalNameColumnIndex)) { //moved within the totalName column
                                            $designerTds.filter('.total-name').attr('colspan', totalNameColSpan + 1); //merge
                                            $designerTd.remove();
                                            $cachedTds.filter('.total-name').attr('colspan', totalNameColSpan + 1);
                                            $movedTd.remove();
                                        } else { //moved to the left of totalName column
                                            $movedTd.insertAfter($cachedTds[newIndex]);
                                            $designerTd.insertAfter($designerTds[newIndex]);
                                        }
                                    }
                                } else { //move totaled columns
                                    const $movedTd = $row.find(`[data-linkedcolumn="${linkedColumn}"]`);
                                    if (oldIndex > newIndex) { //moving LEFT
                                        if (newIndex <= totalNameIndex) {//to the LEFT of totalname col
                                            $movedTd.insertBefore($cachedTds[newIndex]);
                                            $designerTd.insertBefore($designerTds[newIndex]);
                                        } else if (newIndex > endTotalNameColumnIndex) {//to the RIGHT of totalname col
                                            $movedTd.insertAfter($cachedTds[newIndex - totalNameColSpan]);
                                            $designerTd.insertAfter($designerTds[newIndex - totalNameColSpan]);
                                        } else if ((newIndex > totalNameIndex) && (newIndex <= endTotalNameColumnIndex)) { //INTO the totalname col
                                            let columnsToUnmerge = totalNameColSpan - (newIndex - totalNameIndex);
                                            const newTotalNameColSpan = totalNameColSpan - columnsToUnmerge;
                                            $designerTds.filter('.total-name').attr('colspan', newTotalNameColSpan);
                                            $cachedTds.filter('.total-name').attr('colspan', newTotalNameColSpan);
                                            $movedTd.insertAfter($cachedTds[totalNameIndex]);
                                            $designerTd.insertAfter($designerTds[totalNameIndex]);

                                            $designerTds = $designerRow.find(cellType);  //reassign with new element order
                                            $cachedTds = $row.find(cellType);

                                            //split tds to the right into empty tds
                                            for (let j = 1; j <= columnsToUnmerge; j++) {
                                                const columnToLinkIndex = endTotalNameColumnIndex - (j - 1);
                                                const linkedCol = jQuery($detailRowTds[columnToLinkIndex]).attr('data-linkedcolumn');
                                                const $newTd = jQuery(`<td class="empty-td" data-linkedcolumn="${linkedCol}"></td>`);

                                                $newTd.clone().insertAfter($cachedTds[newIndex + 1 - newTotalNameColSpan]);
                                                $newTd.insertAfter($designerTds[newIndex + 1 - newTotalNameColSpan]);
                                            }
                                        }
                                    } else if (oldIndex < newIndex) { //moving RIGHT
                                        if (newIndex > endTotalNameColumnIndex) { //to the RIGHt of totalname col
                                            $movedTd.insertAfter($cachedTds[newIndex - totalNameColSpan + 1]);
                                            $designerTd.insertAfter($designerTds[newIndex - totalNameColSpan + 1]);
                                        } else if (newIndex <= totalNameIndex) {//to the LEFT of totalname col
                                            $movedTd.insertAfter($cachedTds[newIndex]);
                                            $designerTd.insertAfter($designerTds[newIndex]);
                                        } else if ((newIndex > totalNameIndex) && (newIndex <= endTotalNameColumnIndex)) { //INTO the totalname col
                                            let columnsToUnmerge = endTotalNameColumnIndex - newIndex;
                                            const newTotalNameColSpan = totalNameColSpan - columnsToUnmerge;
                                            $designerTds.filter('.total-name').attr('colspan', newTotalNameColSpan);
                                            $cachedTds.filter('.total-name').attr('colspan', newTotalNameColSpan);
                                            $movedTd.insertAfter($cachedTds[totalNameIndex]);
                                            $designerTd.insertAfter($designerTds[totalNameIndex]);

                                            $designerTds = $designerRow.find(cellType);  //reassign with new element order
                                            $cachedTds = $row.find(cellType);

                                            //split tds to the right into empty tds
                                            for (let j = 1; j <= columnsToUnmerge; j++) {
                                                const columnToLinkIndex = endTotalNameColumnIndex - (j - 1);
                                                const linkedCol = jQuery($detailRowTds[columnToLinkIndex]).attr('data-linkedcolumn');
                                                const $newTd = jQuery(`<td class="empty-td" data-linkedcolumn="${linkedCol}"></td>`);

                                                $newTd.clone().insertAfter($cachedTds[newIndex + 1 - newTotalNameColSpan]);
                                                $newTd.insertAfter($designerTds[newIndex + 1 - newTotalNameColSpan]);
                                            }
                                        }
                                    }
                                }
                            } else { //it wont find a designertd if it merged into a footer total-name column
                                const $newTd = jQuery(`<td class="empty-td" data-linkedcolumn="${linkedColumn}"></td>`);
                                let footerRowIndex = newIndex;
                                if (oldRowIndex === 0) {
                                    if (oldIndex < newIndex) { //moving RIGHT
                                        if (newIndex > endTotalNameColumnIndex) { //add new empty tds
                                            $designerTds.filter('.total-name').attr('colspan', totalNameColSpan - 1);
                                            $cachedTds.filter('.total-name').attr('colspan', totalNameColSpan - 1);
                                            footerRowIndex = newIndex - (totalNameColSpan - 1);
                                            $newTd.clone().insertAfter($cachedTds[footerRowIndex]);
                                            $newTd.insertAfter($designerTds[footerRowIndex]);
                                        }
                                    } else if (oldIndex > newIndex) { //MOVING LEFT
                                        if (newIndex < totalNameIndex) {
                                            $designerTds.filter('.total-name').attr('colspan', totalNameColSpan - 1);
                                            $cachedTds.filter('.total-name').attr('colspan', totalNameColSpan - 1);
                                            $newTd.clone().insertBefore($cachedTds[newIndex]);
                                            $newTd.insertBefore($designerTds[newIndex]);
                                        }
                                    }
                                } else {
                                    if (newIndex > endTotalNameColumnIndex) { //add new empty tds
                                        footerRowIndex = newIndex - totalNameColSpan;
                                        $newTd.clone().insertAfter($cachedTds[footerRowIndex]);
                                        $newTd.insertAfter($designerTds[footerRowIndex]);
                                    } else if (newIndex < totalNameIndex) {
                                        $newTd.clone().insertBefore($cachedTds[newIndex]);
                                        $newTd.insertBefore($designerTds[newIndex]);
                                    } else {
                                        $designerTds.filter('.total-name').attr('colspan', totalNameColSpan + 1);
                                        $cachedTds.filter('.total-name').attr('colspan', totalNameColSpan + 1);
                                    }
                                }
                            }
                            //after moving column, check both sides of totalName col and merge if empty tds 
                            const mergeTds = () => {
                                let count = 0;
                                $designerTds = $designerRow.find('td');  //reassign with new element order
                                $cachedTds = $row.find('td');
                                let newTotalNameIndex = $designerTds.filter('.total-name').index();
                                const $tdBefore = jQuery($designerTds[newTotalNameIndex - 1]);
                                const $tdAfter = jQuery($designerTds[newTotalNameIndex + 1]);
                                if ($tdBefore.length) {
                                    if ($tdBefore.hasClass('empty-td')) { //merge
                                        $tdBefore.remove();
                                        jQuery($cachedTds[newTotalNameIndex - 1]).remove();
                                        const colspan = parseInt(jQuery($designerTds[newTotalNameIndex]).attr('colspan'));
                                        jQuery($designerTds[newTotalNameIndex]).attr('colspan', colspan + 1);
                                        jQuery($cachedTds[newTotalNameIndex]).attr('colspan', colspan + 1);
                                        count++;
                                    }
                                }
                                if ($tdAfter.length) {
                                    if ($tdAfter.hasClass('empty-td')) { //merge
                                        $tdAfter.remove();
                                        jQuery($cachedTds[newTotalNameIndex + 1]).remove();
                                        const colspan = parseInt(jQuery($designerTds[newTotalNameIndex]).attr('colspan'));
                                        jQuery($designerTds[newTotalNameIndex]).attr('colspan', colspan + 1);
                                        jQuery($cachedTds[newTotalNameIndex]).attr('colspan', colspan + 1);
                                        count++;
                                    }
                                }
                                if (count != 0) {
                                    mergeTds();
                                }
                            }
                            mergeTds();
                            footerRowIndex++;
                        } else { //remove empty columns when moving column out of main header row
                            $row.find(`.empty-td[data-linkedcolumn="${linkedColumn}"]`).remove();
                            $designerRow.find(`.empty-td[data-linkedcolumn="${linkedColumn}"]`).remove();
                            footerRowIndex++;
                        }
                    }
                }
                break;
            case 'linked-sub-header':
                if (typeof linkedRow != 'undefined') {
                    const $linkedRows = $table.find(`tr[data-linkedrow="${linkedRow}"]:not([data-row="linked-sub-header"])`);
                    if ($linkedRows.length) {
                        let colsMoved, rowIndex;
                        for (let i = 0; i < $linkedRows.length; i++) {
                            const $row = jQuery($linkedRows[i]);
                            const rowType = $row.attr('data-row');
                            const validRowTypes: any = ['main-header', 'sub-header', 'sub-detail'];
                            if (validRowTypes.includes(rowType)) {
                                $form.data('columnsmoved')['rowType'] = rowType;
                                if (rowType === 'main-header') {
                                    rowIndex = $row.index();
                                    $form.data('columnsmoved')['toRowIndex'] = rowIndex;
                                    $form.data('columnsmoved')['fromRowIndex'] = rowIndex;
                                    colsMoved = $form.data('columnsmoved');

                                    const $headerColumn = $row.find(`[data-linkedcolumn="${linkedColumn}"]`);
                                    const $headerColumns = $row.children();
                                    if (oldIndex > newIndex) {
                                        $headerColumn.insertBefore($headerColumns[newIndex]);
                                    } else {
                                        $headerColumn.insertAfter($headerColumns[newIndex]);
                                    }
                                }
                                $form.data('columnsmoved', colsMoved);
                                this.moveColumns($form, $wrapper, tableNameSelector, $table, $cachedRows, $th);
                            }
                        }
                    }
                }
                break;
            case 'sub-header':
                //in designer
                const $linkedSubDetailColumn = $table.find(`[data-row="sub-detail"] td[data-linkedcolumn="${linkedColumn}"]`);
                const $subDetailRow = $linkedSubDetailColumn.parent('tr');
                const $subDetailTds = $subDetailRow.find('td');

                //in cache
                const $cachedSubDetailColumn = $wrapper.find(`${tableNameSelector} tbody tr[data-row="sub-detail"] td[data-linkedcolumn="${linkedColumn}"]`);
                const $cachedSubDetailRow = $cachedSubDetailColumn.parent('tr');
                const $cachedSubDetailTds = $cachedSubDetailRow.find('td');

                if (oldIndex > newIndex) { //moving left
                    $linkedSubDetailColumn.insertBefore($subDetailTds[newIndex]);
                    $cachedSubDetailColumn.insertBefore($cachedSubDetailTds[newIndex]).after('\n');
                } else if (oldIndex < newIndex) { //moving right
                    $linkedSubDetailColumn.insertAfter($subDetailTds[newIndex]);
                    $cachedSubDetailColumn.insertAfter($cachedSubDetailTds[newIndex]).after('\n');
                }

                html = $table.find('tr[data-row="sub-header"]').get(0).innerHTML.trim();
                jQuery($wrapper.find(`${tableNameSelector} tr[data-row="sub-header"]`)).html(html);                     //replace old sub-headers
                break;
            case 'sub-detail':
                html = $table.find('tr[data-row="sub-detail"]').get(0).innerHTML.trim();
                jQuery($wrapper.find(`${tableNameSelector} tr[data-row="sub-detail"]`)).html(html);                     //replace old sub-details
                break;
        }
        $form.removeData('columnsmoved');
    }
    //----------------------------------------------------------------------------------------------
};
//----------------------------------------------------------------------------------------------
var CustomReportLayoutController = new CustomReportLayout();