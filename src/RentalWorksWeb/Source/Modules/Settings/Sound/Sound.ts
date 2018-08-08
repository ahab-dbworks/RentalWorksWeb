routes.push({ pattern: /^module\/sound$/, action: function (match: RegExpExecArray) { return SoundController.getModuleScreen(); } });
class Sound {
    Module: string = 'Sound';
    apiurl: string = 'api/v1/sound';

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Sound', false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);

        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }

    openBrowse() {
        var $browse;

        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);

         FwBrowse.addLegend($browse, 'User Defined Sound', '#00FF00');

        return $browse;
    }

    openForm(mode: string) {
        var $form, $moduleSelect;

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);

        if ($form.find('div[data-datafield="SystemSound"]').attr('data-originalvalue') === "true") {
            FwFormField.disable($form.find('div[data-datafield="Sound"]'));
            FwFormField.disable($form.find('div[data-datafield="FileName"]'));
        }

        if (mode === 'NEW') {
            FwFormField.enable($form.find('.ifnew'));
        } else {
            FwFormField.disable($form.find('.ifnew'));
        }

        //var node = FwApplicationTree.getNodeById(FwApplicationTree.tree, '0A5F2584-D239-480F-8312-7C2B552A30BA');
        //let mainModules = FwApplicationTree.getChildrenByType(node, 'Module');
        //let settingsModules = FwApplicationTree.getChildrenByType(node, 'SettingsModule')
        //let modules = mainModules.concat(settingsModules);
        //var allModules = [];
        //for (var i = 0; i < modules.length; i++) {
        //    var moduleNav = modules[i].properties.controller.slice(0, -10);
        //    var moduleCaption = modules[i].properties.caption;
        //    if (moduleCaption === "Designer") {
        //        continue;
        //    } 
        //    var moduleController = modules[i].properties.controller;
        //    if (window[moduleController].hasOwnProperty('apiurl')) {
        //        var moduleUrl = window[moduleController].apiurl;
        //        allModules.push({ value: moduleNav, text: moduleCaption, apiurl: moduleUrl });
        //    } 
        //};
       

        ////Sort modules
        //function compare(a, b) {
        //    if (a.text < b.text)
        //        return -1;
        //    if (a.text > b.text)
        //        return 1;
        //    return 0;
        //}
        //allModules.sort(compare);

        //$moduleSelect = $form.find('.modules');
        //FwFormField.loadItems($moduleSelect, allModules);

        //this.getFields($form);

        //$form.find('[data-datafield="SystemRule"]').attr('data-required', false);

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="SoundId"] input').val(uniqueids.SoundId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    //getFields($form: JQuery): void {
    //    $form.find('div.modules').on("change", function () {
    //        let moduleName, moduleUrl, request;
    //        moduleName = jQuery(this).find(':selected').val();
    //        moduleUrl = jQuery(this).find(':selected').attr('data-apiurl');
    //        request = {
    //            module: moduleName,
    //            top: 1
    //        };

    //        FwAppData.apiMethod(true, 'POST', `${moduleUrl}/browse`, request, FwServices.defaultTimeout, function onSuccess(response) {
    //            var fieldColumns = response.Columns;

    //            var filteredColumns = fieldColumns.filter(function (obj) {
    //                return obj.DataField !== 'DateStamp';
    //            });

    //            var fieldNamesOnly = filteredColumns.map(a => a.DataField).sort();
    //            var sortedFields = fieldNamesOnly.sort(function (a, b) {
    //                return a.toLowerCase().localeCompare(b.toLowerCase());
    //            });
               
    //            var fieldsHtml = [];
    //            var $fields = $form.find('.fields');
    //            for (var i = 0; i < sortedFields.length; i++) {
    //                var uniqueId = FwApplication.prototype.uniqueId(10);
    //                fieldsHtml.push('<div data-control="FwFormField"');
    //                fieldsHtml.push(' data-type="checkbox"');
    //                fieldsHtml.push(' class="fwcontrol fwformfield"');
    //                fieldsHtml.push(' data-enabled="true"');
    //                fieldsHtml.push(' data-caption="' + sortedFields[i] + '"');
    //                fieldsHtml.push(' data-value="' + sortedFields[i] + '"');
    //                fieldsHtml.push(' style="float:left;width:300px; padding: 10px; 0px;"');
    //                fieldsHtml.push('>');
    //                fieldsHtml.push('<input id="' + uniqueId + '" class="fwformfield-control fwformfield-value" type="checkbox" name= "' + sortedFields[i] + '"');
    //                fieldsHtml.push(' />');
    //                fieldsHtml.push('<label class="fwformfield-caption" for="' + uniqueId + '">' + sortedFields[i] + '</label>');
    //                fieldsHtml.push('</div>');
    //            }
    //            $fields.empty().append(fieldsHtml.join('')).html();

    //            var fields = $form.find('[data-datafield="Fields"]').attr('data-originalvalue');
    //                var separateFields = fields.split(",");
    //                jQuery.each(separateFields, function (i, val) {
    //                    jQuery("input[name='" + val + "']").prop("checked", true)
    //                });

    //            $form.on('change', '[type="checkbox"]', e => {
    //                var field = jQuery(e.currentTarget).attr('name');
    //                if (separateFields.indexOf(field) === -1) {
    //                    separateFields.push(field);
    //                } else {
    //                  separateFields = separateFields.filter(item => { return item !== field})
    //                }
    //                //if (separateFields.length !== 1) {
    //                //    throw 'Expected 1 element, but got: ' + separateFields.length;
    //                //}
    //                var joinFields = separateFields.join(',');
    //                FwFormField.setValueByDataField($form, 'Fields', joinFields); 
    //            });
    //        }, null, $form);
    //    });
    //}

    afterLoad($form: any) {
        if ($form.find('div[data-datafield="SystemSound"]').attr('data-originalvalue') === "true") {
            FwFormField.disable($form.find('div[data-datafield="Sound"]'));
            FwFormField.disable($form.find('div[data-datafield="FileName"]'));
        }
    }
}

var SoundController = new Sound();