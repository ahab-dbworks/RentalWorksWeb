routes.push({ pattern: /^module\/customform$/, action: function (match: RegExpExecArray) { return CustomFormController.getModuleScreen(); } });
class CustomForm {
    Module: string = 'CustomForm';
    apiurl: string = 'api/v1/customform';
    caption: string = 'Custom Forms';
    nav: string = 'module/customform';
    id: string = 'CB2EF8FF-2E8D-4AD0-B880-07037B839C5E';
    codeMirror: any;
    doc: any;
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
    }
    //----------------------------------------------------------------------------------------------
    codeMirrorEvents($form) {
        //Creates an instance of CodeMirror
        let textArea = $form.find('#codeEditor');
        var codeMirror = CodeMirror.fromTextArea(textArea.get(0),
            {
                mode: 'xml'
                , lineNumbers: true
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

        //Sets form to modified upon changing code in editor
        codeMirror.on('change', function (cm, change) {
            $form.attr('data-modified', 'true');
            $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
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
            this.renderTab($form, 'Preview');
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
        $form.find('#codeEditor').change();     // 10/25/2018 Jason H - updates the textarea formfield with the code editor html

        tabName === 'Designer' ? renderFormHere = 'designerContent' : renderFormHere = 'previewWebForm';

        let html = FwFormField.getValueByDataField($form, 'Html');
        $form.find(`#${renderFormHere}`).empty().append(html);

        //render forms
        let $customForm = $form.find(`#${renderFormHere}`);
        let $fwcontrols = $customForm.find('.fwcontrol');
        FwControl.init($fwcontrols);
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
        //disable controls 
        FwFormField.disable($customForm.find(`[data-type="validation"], [data-control="FwAppImage"]`));
        $customForm.find('[data-type="Browse"], [data-type="Grid"]').find('.pager').hide();
        FwFormField.disable($customForm.find('[data-type="Browse"], [data-type="Grid"]'));
        $customForm.find('tr.fieldnames .column >').off('click');

        //Design mode borders & events
        if (tabName == 'Designer') {
            $form.find('#controlProperties')
                .empty();  //clear properties upon loading design tab

            $customForm.find('.tabpages .formpage').css('overflow', 'auto');
            //element borders
            let $tableHeaders = $customForm.find('[data-type="Browse"], [data-type="Grid"]')
                .find('thead tr.fieldnames .column >');

            let $fwContainers = $customForm.find('.fwform-body [data-control="FwContainer"]')
                .css({ 'border': '1px solid darkblue', 'margin': '5px' });

            let $flexContainers = $customForm.find('div.flexrow, div.flexcolumn')
                .css({ 'border': '1px solid lightblue', 'margin': '5px' });

            let $fwformfields = $customForm.find('[data-control="FwFormField"]')
                .css({ 'border': '1px solid #dcdcdc', 'margin': '5px' });

            let originalHtml;

            $customForm
                .on('click', '[data-control="FwContainer"], [data-control="FwFormField"], div.flexrow, div.flexcolumn', e => {
                    e.stopPropagation();
                    originalHtml = e.currentTarget;

                    let properties = e.currentTarget.attributes;
                    let html: any = [];
                    html.push(`
                        <div style="border: 1px solid #bbbbbb; word-break: break-word;">
                            <div style="text-indent:5px;">
                                <div style="font-weight:bold; background-color:#dcdcdc; width:50%; float:left;">Name</div>
                                <div style="font-weight:bold; background-color:#dcdcdc; width:50%; float:left;">Value</div>
                            </div>
                        `);
                    for (let i = 0; i < properties.length; i++) {
                        let value = properties[i].value;
                        let name = properties[i].name;

                        if (name == "data-originalvalue") {
                            break;
                        }

                        value = value.replace('focused', '');
                        html.push(`<div class="properties" style="width:100%; display:flex;">
                                      <div class="propname" style="border:.5px solid #efefef; width:50%; float:left;">${name === "" ? "&#160;" : name}</div>
                                      <div class="propval" style="border:.5px solid #efefef; width:50%; float:left;"><input value="${value}"></div>
                                   </div>
                                  `);
                    }
                    html.push(`</div>`);
                    $form.find('#controlProperties')
                        .empty()
                        .append(html.join(''))
                        .find('.properties:even')
                        .css('background-color', '#f7f7f7');
                });

            $form
                .on('keydown', '#controlProperties .propval', e => {
                    if (e.which === 13) {
                        let attribute = jQuery(e.currentTarget).siblings('.propname').text();
                        let value = jQuery(e.currentTarget).find('input').val();


                    
                        ////remove children added by the FW to get the original html
                        //let removeChildrenFromOriginal = jQuery.extend({}, originalHtml);
                        //while (removeChildrenFromOriginal.firstChild) {
                        //    removeChildrenFromOriginal.removeChild(removeChildrenFromOriginal.firstChild);
                        //}

                        jQuery(originalHtml).attr(`${attribute}`, `${value}`);

                        ////remove children from the modified to get replacement html
                        //let removeChildrenFromModified = jQuery.extend({}, originalHtml);
                        //while (removeChildrenFromModified.firstChild) {
                        //    removeChildrenFromModified.removeChild(removeChildrenFromModified.firstChild);
                        //}

                        //let test2 = html.replace(removeChildrenFromOriginal.outerHTML, removeChildrenFromModified.outerHTML);

                        //need to move border styling to rentalworks.css 
                    }
                });


        }
    }
};
//----------------------------------------------------------------------------------------------
var CustomFormController = new CustomForm();