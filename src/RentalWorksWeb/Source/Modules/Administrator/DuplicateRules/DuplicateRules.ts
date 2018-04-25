class DuplicateRules {
    Module: string = 'DuplicateRules';
    apiurl: string = 'api/v1/duplicaterule';

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Duplicate Rules', false, 'BROWSE', true);
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

        return $browse;
    }

    openForm(mode: string) {
        var $form
            , $moduleSelect;

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            FwFormField.enable($form.find('.ifnew'));
        } else {
            FwFormField.disable($form.find('.ifnew'));
        }



        var node = FwApplicationTree.getNodeById(FwApplicationTree.tree, '0A5F2584-D239-480F-8312-7C2B552A30BA');
        var modules = FwApplicationTree.getChildrenByType(node, 'Module');
        var allModules = [];
        for (var i = 0; i < modules.length; i++) {
            var moduleNav = modules[i].properties.controller.slice(0, -10);
            var moduleCaption = modules[i].properties.caption;
            if (moduleCaption === "Designer") {
                continue;
            } 
            var moduleController = modules[i].properties.controller;
            if (window[moduleController].hasOwnProperty('apiurl')) {
                var moduleUrl = window[moduleController].apiurl;
                allModules.push({ value: moduleNav, text: moduleCaption, apiurl: moduleUrl });
            } 
        };

        //Sort modules
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

        this.getFields($form);

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="DuplicateRuleId"] input').val(uniqueids.DuplicateRuleId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    getFields($form: JQuery): void {
        $form.find('div.modules').on("change", function () {
            var moduleName = jQuery(this).find(':selected').val();
            var request = {
                module: moduleName
                //pageno: 1,   //justin 04/23/2018 - we just want the first row to harvest the column names  //reverting this change...
                //pagesize: 1
            };
            var moduleUrl = jQuery(this).find(":selected").attr('data-apiurl');

            FwAppData.apiMethod(true, 'POST', moduleUrl + "/browse", request, FwServices.defaultTimeout, function onSuccess(response) {
                var fieldColumns = response.Columns;

                var filteredColumns = fieldColumns.filter(function (obj) {
                    return obj.DataField !== 'DateStamp';
                });

                var fieldNamesOnly = filteredColumns.map(a => a.DataField).sort();
                var sortedFields = fieldNamesOnly.sort(function (a, b) {
                    return a.toLowerCase().localeCompare(b.toLowerCase());
                });
               
                var fieldsHtml = [];
                var $fields = $form.find('.fields');
                for (var i = 0; i < sortedFields.length; i++) {
                    var uniqueId = FwApplication.prototype.uniqueId(10);
                    fieldsHtml.push('<div data-control="FwFormField"');
                    fieldsHtml.push(' data-type="checkbox"');
                    fieldsHtml.push(' class="fwcontrol fwformfield SystemRuleTRUE"');
                    fieldsHtml.push(' data-enabled="true"');
                    fieldsHtml.push(' data-caption="' + sortedFields[i] + '"');
                    fieldsHtml.push(' data-value="' + sortedFields[i] + '"');
                    fieldsHtml.push(' style="float:left;width:300px; padding: 10px; 0px;"');
                    fieldsHtml.push('>');
                    fieldsHtml.push('<input id="' + uniqueId + '" class="fwformfield-control fwformfield-value" type="checkbox" name= "' + sortedFields[i] + '"');
                    fieldsHtml.push(' />');
                    fieldsHtml.push('<label class="fwformfield-caption" for="' + uniqueId + '">' + sortedFields[i] + '</label>');
                    fieldsHtml.push('</div>');
                }
                $fields.empty().append(fieldsHtml.join('')).html();

                var fields = $form.find('[data-datafield="Fields"]').attr('data-originalvalue');
                var separateFields = fields.split(",");
                jQuery.each(separateFields, function (i, val) {
                    jQuery("input[name='" + val + "']").prop("checked", true)
                });

                $form.on('change', '[type="checkbox"]', (e) => {
                    var field = jQuery(e.currentTarget).attr('name');
                    if (separateFields.indexOf(field) === -1) {
                        separateFields.push(field);
                    } else {
                        separateFields = separateFields.filter((item) => item !== field)
                    }
                    if (separateFields.length !== 1) {
                        throw 'Expected 1 element, but got: ' + separateFields.length;
                    }
                    var joinFields = separateFields.join(',');
                    FwFormField.setValueByDataField($form, 'Fields', joinFields); 
                });
            }, null, $form);
        });
    }

    afterLoad($form: any) {
        var moduleName = $form.find('.modules').attr('data-originalvalue');
        var moduleUrl = $form.find("select option[value='" + moduleName + "']").attr('data-apiurl');
        var request = {
            module: moduleName
        };

        FwAppData.apiMethod(true, 'POST', moduleUrl + "/browse", request, FwServices.defaultTimeout, function onSuccess(response) {
            var fieldColumns = response.Columns;

            var filteredColumns = fieldColumns.filter(function (obj) {
                return obj.DataField !== 'DateStamp';
            });

            var fieldNamesOnly = filteredColumns.map(a => a.DataField).sort();
            var sortedFields = fieldNamesOnly.sort(function (a, b) {
                return a.toLowerCase().localeCompare(b.toLowerCase());
            });

            var fieldsHtml = [];
            var $fields = $form.find('.fields');

          if (FwFormField.getValueByDataField($form, 'SystemRule') === 'true') {
              FwFormField.disable($form.find('.SystemRuleTRUE'));
              for (var i = 0; i < sortedFields.length; i++) {
                  let uniqueId = FwApplication.prototype.uniqueId(10);
                  fieldsHtml.push('<div data-control="FwFormField"');
                  fieldsHtml.push(' data-type="checkbox"');
                  fieldsHtml.push(' class="fwcontrol fwformfield check SystemRuleTRUE"');
                  fieldsHtml.push(' data-enabled="false"');
                  fieldsHtml.push(' data-caption="' + sortedFields[i] + '"');
                  fieldsHtml.push(' data-value="' + sortedFields[i] + '"');
                  fieldsHtml.push(' style="float:left;width:300px; padding: 10px; 0px;"');
                  fieldsHtml.push('>');
                  fieldsHtml.push('<input id="' + uniqueId + '" class="fwformfield-control fwformfield-value" type="checkbox" name="' + sortedFields[i] + '"');
                  fieldsHtml.push(' />');
                  fieldsHtml.push('<label class="fwformfield-caption" data-value="' + sortedFields[i] + '" for="' + uniqueId + '">' + sortedFields[i] + '</label>');
                  fieldsHtml.push('</div>');
              }
          } else {
              for (var i = 0; i < sortedFields.length; i++) {
                  let uniqueId = FwApplication.prototype.uniqueId(10);
                  fieldsHtml.push('<div data-control="FwFormField"');
                  fieldsHtml.push(' data-type="checkbox"');
                  fieldsHtml.push(' class="fwcontrol fwformfield check SystemRuleTRUE"');
                  fieldsHtml.push(' data-enabled="true"');
                  fieldsHtml.push(' data-caption="' + sortedFields[i] + '"');
                  fieldsHtml.push(' data-value="' + sortedFields[i] + '"');
                  fieldsHtml.push(' style="float:left;width:300px; padding: 10px; 0px;"');
                  fieldsHtml.push('>');
                  fieldsHtml.push('<input id="' + uniqueId + '" class="fwformfield-control fwformfield-value" type="checkbox" name="' + sortedFields[i] + '"');
                  fieldsHtml.push(' />');
                  fieldsHtml.push('<label class="fwformfield-caption" data-value="' + sortedFields[i] + '" for="' + uniqueId + '">' + sortedFields[i] + '</label>');
                  fieldsHtml.push('</div>');
              }
          }
            $fields.empty().append(fieldsHtml.join('')).html();

            var fields = $form.find('[data-datafield="Fields"]').attr('data-originalvalue');
            var separateFields = fields.split(",");
            jQuery.each(separateFields, function (i, val) {
                jQuery("input[name='" + val + "']").prop("checked", true)
            });

            $form.on('change', '[type="checkbox"]', (e) => {
                var field = jQuery(e.currentTarget).attr('name');
                if (separateFields.indexOf(field) === -1) {
                    separateFields.push(field);
                } else {
                    separateFields = separateFields.filter((item) => item !== field)
                }
                var joinFields = separateFields.join(',');
                FwFormField.setValueByDataField($form, 'Fields', joinFields); 
            });
        }, null, $form);
    }
}

var DuplicateRulesController = new DuplicateRules();