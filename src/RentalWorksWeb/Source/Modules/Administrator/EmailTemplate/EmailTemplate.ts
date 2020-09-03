routes.push({ pattern: /^module\/emailtemplate/, action: function (match: RegExpExecArray) { return EmailTemplateController.getModuleScreen(); } });

class EmailTemplate {
    Module: string = 'EmailTemplate';
    apiurl: string = 'api/v1/emailtemplate';
    caption: string = Constants.Modules.Administrator.children.EmailTemplate.caption;
    nav: string = Constants.Modules.Administrator.children.EmailTemplate.nav;
    id: string = Constants.Modules.Administrator.children.EmailTemplate.id;
    datafields: any = [];
    codeMirror: any;
    doc: any;
    //----------------------------------------------------------------------------------------------
    getModuleScreen = () => {
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
        screen.unload = () => {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        $browse.data('ondatabind', (request: any) => {
            let campusid = JSON.parse(sessionStorage.getItem('campus')).CampusId;
            request.filterfields = {
                CampusId: campusid
            };
        });

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            FwFormField.enable($form.find('.ifnew'));
        } else {
            FwFormField.disable($form.find('.ifnew'));
        }

        FwAppData.apiMethod(true, 'POST', `${this.apiurl}/templatecategories`, null, FwServices.defaultTimeout, function onSuccess(response) {
            const $categoryDataField = $form.find('div[data-datafield="Category"]');
            const $bodyformatDataField = $form.find('div[data-datafield="BodyFormat"]');
            let categoriesList = [];
            let bodyformatList = [
                { text: "HTML", value: "HTML" },
                { text: "TEXT", value: "TEXT" }
            ]
            for (let i = 0; i < response.categories.length; i++) {
                const category = {
                    text: response.categories[i],
                    value: response.categories[i]
                }
                categoriesList.push(category);
            }
            // need to manually load bodyformat list i.e. text/html
            FwFormField.loadItems($categoryDataField, categoriesList);
            FwFormField.loadItems($bodyformatDataField, bodyformatList);

        }, null, $form);


        $form.off('change', '.fwformfield[data-enabled="true"][data-datafield!=""]:not(.find-field)');

        $form.data('beforesave', request => {
            //request.AlertConditionList = this.getConditionData($form);
        });

        this.events($form);
        this.initializeCodeMirror($form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        let $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="AppEmailId"] input').val(uniqueids.AppEmailId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        $form.find('#codeEditor').change();
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form) {


    }
    //----------------------------------------------------------------------------------------------
    initializeCodeMirror($form) {
        let textArea = $form.find('#codeEditor').get(0);
        var codeMirror = CodeMirror.fromTextArea(textArea,
            {
                mode: 'xml',
                lineNumbers: true,
                foldGutter: true,
                gutters: ["CodeMirror-linenumbers", "CodeMirror-foldgutter"]
            });
        this.codeMirror = codeMirror;
    }
    //----------------------------------------------------------------------------------------------
    addValidFields($form) {
        let fieldsList = $form.find('.field-list');
        let category = FwFormField.getValueByDataField($form, 'Category');
        let fieldsRequest = {
            category: category
        }

        if ($form.find('.field-name').length === 0) {
            FwAppData.apiMethod(true, 'POST', `${this.apiurl}/templatefields`, fieldsRequest, FwServices.defaultTimeout, function onSuccess(response) {
                for (let i = 0; i < response.fields.length; i++) {
                    fieldsList.append(`
                    <div class="field-name" style="cursor:pointer">[${response.fields[i].toUpperCase()}]</div>
                     `);
                }
            }, null, $form)
        }
    }
    //----------------------------------------------------------------------------------------------
    afterSave($form: any): void {
        FwFormField.disable($form.find('div[data-datafield="ModuleName"]'));
        const $tabpage = $form.parent();
        const $tab = jQuery('#' + $tabpage.attr('data-tabid'));
        $tab.find('.modified').html('');
        $form.attr('data-modified', 'false');
        $form.find('.btn[data-type="SaveMenuBarButton"]').addClass('disabled');
        $form.find('.btn[data-type="RefreshMenuBarButton"]').addClass('disabled');
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        $form.find('div.modules').change();

        //set form as unmodified after saving
        const $tabpage = $form.parent();
        const $tab = jQuery('#' + $tabpage.attr('data-tabid'));
        $tab.find('.modified').html('');
        $form.attr('data-modified', 'false');
        $form.find('.btn[data-type="SaveMenuBarButton"]').addClass('disabled');
        $form.find('.btn[data-type="RefreshMenuBarButton"]').addClass('disabled');

        this.addValidFields($form);
        this.codeMirrorEvents($form);


        //Sets form to modified upon changing code in editor
        this.codeMirror.on('change', function (codeMirror, change) {
            $form.attr('data-modified', 'true');
            $form.find('.btn[data-type="SaveMenuBarButton"]').removeClass('disabled');
        });



        //const $alertWebUsersGrid = $form.find('[data-name="AlertWebUsersGrid"]');
        //FwBrowse.search($alertWebUsersGrid);

        //const $alertHistoryGrid = $form.find('[data-name="WebAlertLogGrid"]');
        //FwBrowse.search($alertHistoryGrid);
    }
    //----------------------------------------------------------------------------------------------
    events($form) {
        $form.on('click', '.field-name', e => {
            const $this = jQuery(e.currentTarget);
            const doc = this.codeMirror.getDoc();
            const cursor = doc.getCursor();
            doc.replaceRange($this.text(), cursor);
            $form.find('#codeEditor').change();
        });
    }
    //----------------------------------------------------------------------------------------------
    codeMirrorEvents($form) {
        this.doc = this.codeMirror.getDoc();
        //Loads html for code editor
        if (!$form.find('[data-datafield="EmailText"]').hasClass('reload')) {
            let html = $form.find('[data-datafield="EmailText"] textarea').val();
            if (typeof html !== 'undefined') {
                this.codeMirror.setValue(html);
            } else {
                this.codeMirror.setValue('');
            }
        }

        //Updates value for form fields
        $form.find('#codeEditor').on('change', e => {
            this.codeMirror.save();
            let html = $form.find('textarea#codeEditor').val();
            FwFormField.setValueByDataField($form, 'EmailText', html);
        });

    }
    //----------------------------------------------------------------------------------------------
}
//----------------------------------------------------------------------------------------------
var EmailTemplateController = new EmailTemplate();