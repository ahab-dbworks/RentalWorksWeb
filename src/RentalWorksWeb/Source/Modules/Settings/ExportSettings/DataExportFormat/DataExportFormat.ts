class DataExportFormat {
    Module: string = 'DataExportFormat';
    apiurl: string = 'api/v1/dataexportformat';
    caption: string = Constants.Modules.Settings.children.ExportSettings.children.DataExportFormat.caption;
    nav: string = Constants.Modules.Settings.children.ExportSettings.children.DataExportFormat.nav;
    id: string = Constants.Modules.Settings.children.ExportSettings.children.DataExportFormat.id;
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

        if (mode == 'NEW') {
            FwFormField.enable($form.find('[data-datafield="ExportType"]'));
            FwFormField.setValue($form, 'div[data-datafield="Active"]', true);
        }

        //removes field propagation
        $form.off('change', '.fwformfield[data-enabled="true"][data-datafield!=""]:not(.find-field)');


        this.loadExportTypes($form);
        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="DataExportFormatId"] input').val(uniqueids.DataExportFormatId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        $form.find('#codeEditor').change();

        //for retaining position in code editor after saving
        $form.find('[data-datafield="ExportString"]').addClass('reload');

        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    afterSave($form: any) {
        FwFormField.disable($form.find('[data-datafield="ExportType"]'));
        $form.attr('data-modified', 'false');
        $form.find('.btn[data-type="SaveMenuBarButton"]').addClass('disabled');
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        //sets code editor value
        if (!$form.find('[data-datafield="ExportString"]').hasClass('reload')) {
            let exportString = $form.find('[data-datafield="ExportString"] textarea').val();
            if (typeof exportString !== 'undefined') {
                this.codeMirror.setValue(exportString);
            } else {
                this.codeMirror.setValue('');
            }
        }

        let exportType: any = FwFormField.getValueByDataField($form, 'ExportType');
        this.addValidFields($form, exportType);
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
                mode: "xml"
                , lineNumbers: true
                , foldGutter: true
                , gutters: ["CodeMirror-linenumbers", "CodeMirror-foldgutter"]
            });
        this.codeMirror = codeMirror;

        //Select export type event
        $form.find('div.export-types').on('change', e => {
            let $this = $form.find('[data-datafield="ExportType"] option:selected');
            const exportType = $this.val()
            this.addValidFields($form, exportType);
        });

        //Updates value for form fields
        $form.find('#codeEditor').on('change', e => {
            codeMirror.save();
            let exportString = $form.find('textarea#codeEditor').val();
            FwFormField.setValueByDataField($form, 'ExportString', exportString);
        });
    }
    //----------------------------------------------------------------------------------------------
    addValidFields($form, exportType) {
        //Get valid field names and sort them
        const modulefields = $form.find('.modulefields');
        modulefields.empty();
        FwAppData.apiMethod(true, 'GET', `api/v1/${exportType}/emptyobject`, null, FwServices.defaultTimeout,
            response => {
                let customFields = response._Custom.map(obj => ({ fieldname: obj.FieldName, fieldtype: obj.FieldType }));
                let allValidFields: any = [];
                for (const key of Object.keys(response)) {
                    if (key != 'DateStamp' && key != 'RecordTitle' && key != '_Custom' && key != '_Fields') {
                        if (Array.isArray(response[key])) {
                            const unorderedItems = response[key][0];
                            const orderedItems = {};
                            Object.keys(unorderedItems).sort().forEach(key => {
                                orderedItems[key] = unorderedItems[key];
                            });
                            allValidFields.push({
                                'Field': key,
                                'IsCustom': 'false',
                                'NestedItems': orderedItems
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
                                if (Array.isArray(allValidFields[i].NestedItems[key])) {
                                    for (const j of Object.keys(allValidFields[i].NestedItems[key][0])) {
                                        modulefields.append(`<div data-iscustomfield="false" data-isnested="true" data-parentfield="${key}" style="text-indent:3em;">${j}</div>`);
                                    }
                                }
                            }
                        }
                    }
                }
            }, ex => FwFunc.showError(ex), $form);
    }
    //----------------------------------------------------------------------------------------------
    loadExportTypes($form) {
        let $moduleSelect = $form.find('.export-types');

        const exports = (<any>window).Constants.Modules.Exports.children;
        const modules: any = [];
        for (var key of Object.keys(exports)) {
            modules.push({ value: key, text: exports[key].caption })
        }

        FwFormField.loadItems($moduleSelect, modules);

        this.codeMirrorEvents($form);
    }
    //----------------------------------------------------------------------------------------------
    events($form) {
        //Refreshes and shows CodeMirror upon clicking HTML tab
        $form.on('click', '[data-type="tab"][data-caption="HTML"]', e => {
            this.codeMirror.refresh();
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
};
//----------------------------------------------------------------------------------------------
var DataExportFormatController = new DataExportFormat();