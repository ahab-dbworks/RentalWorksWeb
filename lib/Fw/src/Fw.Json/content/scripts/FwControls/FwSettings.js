//----------------------------------------------------------------------------------------------
var FwSettings = {};
//----------------------------------------------------------------------------------------------
FwSettings.init = function () {

};
//----------------------------------------------------------------------------------------------
FwSettings.renderRuntimeHtml = function ($control) {
    var html = [];
    

    html.push('<div class="fwsettingsheader">');
      html.push('<div class="input-group pull-right">');
        html.push('<input type="text" id="moduleSearch" class="form-control" placeholder="Module...">');
          html.push('<span class="input-group-addon">');
            html.push('<i class="material-icons">search</i>');
          html.push('</span>');
      html.push('</div>');
      html.push('<div class="input-group pull-right">');
        html.push('<input type="text" id="settingsSearch" class="form-control" placeholder="Settings...">');
          html.push('<span class="input-group-addon">');
            html.push('<i class="material-icons">search</i>');
          html.push('</span>');
      html.push('</div>');
    html.push('</div>');
      
    html.push('<div class="well">');

    html.push('</div>');
        // < div class="input-group pull-right" > <input type="text" class="form-control" placeholder="Settings..."><span class="input-group-addon"><i class="material-icons">search</i></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

    $control.html(html.join(''));


};
//----------------------------------------------------------------------------------------------
FwSettings.saveForm = function (module, $form, closetab, navigationpath, $control, browseKeys, rowId, moduleName, callback) {
    var $tabpage, fields, ids, mode, isValid, $tab, request;
    mode = $form.attr('data-mode');
    $tabpage = $form.parent();
    $tab = jQuery('#' + $tabpage.attr('data-tabid'));
    isValid = FwModule.validateForm($form);
    var controllername = $form.attr('data-controller');
    var controller = window[controllername];

    if (isValid) {
        ids = FwModule.getFormUniqueIds($form);
        fields = FwModule.getFormFields($form, false);
        if (typeof controller.apiurl !== 'undefined') {
            request = FwModule.getFormModel($form, false);
        }
        else {
            request = {
                module: module,
                mode: mode,
                ids: ids,
                fields: fields
            };
        }
        FwServices.module.method(request, module, 'Save', $form, function (response) {
            var $formfields, issubmodule, $browse;

            if (typeof controller.apiurl !== 'undefined') {
                if (!closetab) {
                    var $body = $control.find('#' + moduleName + '.panel-body');

                    var html = [], $moduleRows;
                    html.push('<div class="panel-record">')
                      html.push('<div class="panel panel-info container-fluid">');
                        html.push('<div class="row-heading">');
                          html.push('<i class="material-icons menu">more_horiz</i>')
                            for (var j = 0; j < browseKeys.length; j++) {
                                if (browseKeys.length === 1) {
                                    html.push('<label style="width:25%">' + browseKeys[j] + '</label>');
                                    html.push('<label style="width:25%">' + response[browseKeys[j]] + '</label>');
                                    html.push('<label style="width:25%"></label>');
                                    html.push('<label style="width:25%"></label>');
                                }
                                if (browseKeys.length === 2) {
                                    html.push('<label style="width:25%">' + browseKeys[j] + '</label>');
                                    html.push('<label style="width:25%">' + response[browseKeys[j]] + '</label>');
                                }
                                if (browseKeys.length === 3) {
                                    html.push('<label style="width:15%">' + browseKeys[j] + '</label>');
                                    html.push('<label style="width:15%">' + response[browseKeys[j]] + '</label>');
                                }
                            }
                          html.push('<div class="pull-right save"><i class="material-icons">save</i>Save</div>');
                          html.push('</div>');
                        html.push('</div>');
                      html.push('<div class="panel-body" style="display:none;" id="' + response[rowId] + '"></div>');
                    html.push('</div>');
                    $moduleRows = jQuery(html.join(''));
                    $moduleRows.data('recorddata', response);
                    console.log($body);
                    $body.append($moduleRows);

                    callback();
                    //Refresh the browse window on saving a record.
                    $browse = jQuery('.fwbrowse[data-controller="' + controllername + '"]')
                    if ($browse.length > 0) {
                        FwBrowse.databind($browse);
                    }

                    //If form is submodule
                    //issubmodule = $form.parent().hasClass('submodule');
                    //if (!issubmodule) {
                    //    jQuery('#' + $form.parent().attr('data-tabid')).find('.caption').html(response.tabname);
                    //} else {
                    //    jQuery('#' + $form.parent().attr('data-tabid')).find('.form-caption').html(response.tabname);
                    //}

                    jQuery('#' + $form.parent().attr('data-tabid')).find('.modified').html('');
                    if ($form.attr('data-mode') === 'NEW') {
                        $form.attr('data-mode', 'EDIT');
                        $formfields = jQuery().add($form.data('uniqueids')).add($form.data('fields'));
                    } else {
                        $formfields = $form.data('fields');
                    }
                    FwFormField.loadForm($formfields, response);
                    $form.attr('data-modified', false);
                    if (typeof controller['afterLoad'] === 'function') {
                        controller['afterLoad']($form);
                    }
                    if (typeof controller['afterSave'] === 'function') {
                        controller['afterSave']($form);
                    }
                } else if (closetab) {
                    FwModule.closeFormTab($tab);
                    if ((typeof navigationpath !== 'undefined') && (navigationpath !== '')) {
                        program.getModule(navigationpath);
                    }
                }
                FwNotification.renderNotification('SUCCESS', 'Record saved.');
            }
            else if (response.saved === true) {
                console.log('hi');
                if (!closetab) {
                    //Refresh the browse window on saving a record.
                    $browse = jQuery('.fwbrowse[data-controller="' + controllername + '"]')
                    if ($browse.length > 0) {
                        FwBrowse.databind($browse);
                    }

                    //If form is submodule
                    issubmodule = $form.parent().hasClass('submodule');
                    if (!issubmodule) {
                        jQuery('#' + $form.parent().attr('data-tabid')).find('.caption').html(response.tabname);
                    } else {
                        jQuery('#' + $form.parent().attr('data-tabid')).find('.form-caption').html(response.tabname);
                    }

                    jQuery('#' + $form.parent().attr('data-tabid')).find('.modified').html('');
                    if ($form.attr('data-mode') === 'NEW') {
                        $form.attr('data-mode', 'EDIT');
                        $formfields = jQuery().add($form.data('uniqueids')).add($form.data('fields'));
                    } else {
                        $formfields = $form.data('fields');
                    }
                    FwFormField.loadForm($formfields, response.tables);
                    $form.attr('data-modified', false);
                    if (typeof controller['afterLoad'] === 'function') {
                        controller['afterLoad']($form);
                    }
                    if (typeof controller['afterSave'] === 'function') {
                        controller['afterSave']($form);
                    }
                } else if (closetab) {
                    FwModule.closeFormTab($tab);
                    if ((typeof navigationpath !== 'undefined') && (navigationpath !== '')) {
                        program.getModule(navigationpath);
                    }
                }
                FwNotification.renderNotification('SUCCESS', 'Record saved.');
            } else if (response.saved == false) {
                if ((typeof response.message !== 'undefined') && (response.message != '')) {
                    FwNotification.renderNotification('ERROR', response.message);
                } else {
                    FwNotification.renderNotification('ERROR', 'There is an error on the form.');
                }
            }
        });
    }
};
//----------------------------------------------------------------------------------------------
FwSettings.renderModuleHtml = function ($control, title, description, color) {
    var html = [], $settingsPageModules, $rowBody;
    


    html.push('<div class="panel-group" id="' + description+ '" >');
      
      if (color) {
          html.push('<div class="panel panel-primary" style="border-color:' + color + '">');
          html.push('<div data-toggle="collapse" data-target="' + description + '" href="' + description + '" class="panel-heading" style="background-color:' + color + '">');
      } else {
          html.push('<div class="panel panel-primary">');
          html.push('<div data-toggle="collapse" data-target="' + description + '" href="' + description + '" class="panel-heading">');
      }
        
          html.push('<h4 class="panel-title">');
            html.push('<a id="title" data-toggle="collapse">' + title )
              html.push('<i class="material-icons arrow-selector">keyboard_arrow_down</i>');
            html.push('</a>');
            html.push('<i class="material-icons heading-menu">more_vert</i>');
            html.push('<div id="myDropdown" class="dropdown-content">')
            html.push('<a class="new-row">New Item</a>');
            html.push('</div>'); 
          html.push('</h4>');
          html.push('<small>' + description + '</small>')
        html.push('</div>');
        html.push('<div class="panel-collapse collapse" style="display:none; "><div class="panel-body" id="' + description + '"></div></div>');
      html.push('</div>');
    html.push('</div>');


   

    $settingsPageModules = jQuery(html.join(''));
    $control.find('.well').append($settingsPageModules);

    $settingsPageModules.find('.new-row').on('click', function (e) {

        $settingsPageModules.find('.heading-menu').next().css('display', 'none');
        var browseKeys = [], rowId, formKeys = [], newRowHtml = [];
        e.stopPropagation();
        var $this = jQuery(this);
        var moduleName = $this.closest('.panel-group').attr('id');
        var $modulecontainer = $control.find('#' + moduleName);
        var apiurl = window[moduleName + 'Controller'].apiurl;
        var $body = $control.find('#' + moduleName + '.panel-body');
        var $form = jQuery(jQuery('#tmpl-modules-' + moduleName + 'Form').html());
       
        var getRows = function () {
            FwAppData.apiMethod(true, 'GET', applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + apiurl, null, null, function onSuccess(response) {
                
                $settings = jQuery(jQuery('#tmpl-modules-' + moduleName + 'Browse').html());
                var keys = $settings.find('.field');
                rowId = jQuery(keys[0]).attr('data-browsedatafield');
                for (var i = 1; i < keys.length; i++) {
                    browseKeys.push(jQuery(keys[i]).attr('data-browsedatafield'));
                }
                for (var i = 0; i < response.length; i++) {
                    var html = [], $moduleRows;
                    html.push('<div class="panel-record">')
                    html.push('<div class="panel panel-info container-fluid">');
                    html.push('<div class="row-heading">');
                    html.push('<i class="material-icons menu">more_horiz</i>')
                    //html.push('<label>' + moduleName + '</label>')
                    //html.push('<label>' + row[moduleName] + '</label>');
                    for (var j = 0; j < browseKeys.length; j++) {
                        if (browseKeys.length === 1) {
                            html.push('<label style="width:25%">' + browseKeys[j] + '</label>');
                            html.push('<label style="width:25%">' + response[i][browseKeys[j]] + '</label>');
                            html.push('<label style="width:25%"></label>');
                            html.push('<label style="width:25%"></label>');
                        }
                        if (browseKeys.length === 2) {
                            html.push('<label style="width:25%">' + browseKeys[j] + '</label>');
                            html.push('<label style="width:25%">' + response[i][browseKeys[j]] + '</label>');
                        }
                        if (browseKeys.length === 3) {
                            html.push('<label style="width:15%">' + browseKeys[j] + '</label>');
                            html.push('<label style="width:15%">' + response[i][browseKeys[j]] + '</label>');
                        }
                    }
                    html.push('<div class="pull-right save"><i class="material-icons">save</i>Save</div>');
                    html.push('</div>');
                    html.push('</div>');
                    html.push('<div class="panel-body" style="display:none;" id="' + response[i][rowId] + '"></div>');
                    html.push('</div>');
                    $moduleRows = jQuery(html.join(''));
                    $moduleRows.data('recorddata', response[i]);
                    console.log($body);
                    $body.append($moduleRows);
                }

                $control.on('click', '.row-heading', function (e) {
                    e.stopPropagation();
                    var formKeys = [], formData = [];
                    var recordData = jQuery(this).parent().parent().data('recorddata');
                    var $rowBody = $control.find('#' + recordData[moduleName + 'Id'] + '.panel-body');
                    var $form = jQuery(jQuery('#tmpl-modules-' + moduleName + 'Form').html());
                    if ($rowBody.is(':empty')) {
                        $rowBody.append($form);
                        FwModule.openForm($form, 'EDIT')

                        for (var key in recordData) {
                            var value = recordData[key];
                            if ($form.find('[data-datafield="' + key + '"]').length > 0) {
                                FwFormField.setValue($form, '[data-datafield="' + key + '"]', value);
                            }

                        }
                    }
                }).find('.save').on('click', function (e) {
                    e.stopPropagation();
                    var $form = jQuery(this).closest('.panel-record').find('.fwform');
                    FwSettings.saveForm(window[moduleName + 'Controller'].Module, $form, false, '');                    
                })

            }, null, $modulecontainer);
        }

        if ($body.is(':empty')) {
            getRows();
        }
        if ($settingsPageModules.find('.panel-collapse').css('display') === 'none') {
            $settingsPageModules.find('.arrow-selector').html('keyboard_arrow_up')
            $settingsPageModules.find('.panel-collapse').show("fast");
        }

        if ($body.find('.new-row').length === 0) {
            newRowHtml.push('<div class="new-row">')
            newRowHtml.push('<div class="panel-record">')
            newRowHtml.push('<div class="panel panel-info container-fluid">');
            newRowHtml.push('<div class="new-row-heading">');
            newRowHtml.push('<label style="width:100%">New Record</label>');
            newRowHtml.push('<div class="pull-right save-new-row"><i class="material-icons">save</i>Save</div>');
            newRowHtml.push('</div>');
            newRowHtml.push('</div>');
            newRowHtml.push('</div>');
            newRowHtml.push('</div>');

            $body.prepend($form);
            $body.prepend(jQuery(newRowHtml.join('')));

            FwModule.openForm($form, 'NEW');
            $form.find('.buttonbar').hide();
        }
        

        $body.find('.save-new-row').on('click', function (e) {
            e.stopPropagation();
            var $form = jQuery(this).closest('.panel-body').find('.fwform');
            var save = $form.find('.btn')
            console.log(jQuery(this).closest('.panel-record'))
            FwSettings.saveForm(window[moduleName + 'Controller'].Module, $form, false, '', $control, browseKeys, rowId, moduleName, function () {
                $body.find('.new-row').hide();
               
                $body.empty();
                getRows();
                });            
        });
    })

    $settingsPageModules.on('click', '.panel-heading', function (e) {
        var browseKeys = [], rowId, formKeys = [];
        
        var $this = jQuery(this);
        var moduleName = $this.closest('.panel-group').attr('id');
        var $modulecontainer = $control.find('#' + moduleName);
        var apiurl = window[moduleName + 'Controller'].apiurl;
        var $body = $control.find('#' + moduleName + '.panel-body')
        console.log($body)
        if ($body.is(':empty')) {
            FwAppData.apiMethod(true, 'GET', applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + apiurl, null, null, function onSuccess(response) { 
            $settings = jQuery(jQuery('#tmpl-modules-' + moduleName + 'Browse').html());
            var keys = $settings.find('.field');
            rowId = jQuery(keys[0]).attr('data-browsedatafield');
            for (var i = 1; i < keys.length; i++) {                
                browseKeys.push(jQuery(keys[i]).attr('data-browsedatafield'));                
            }                
            for(var i = 0; i < response.length; i++) {
                var html = [], $moduleRows;
                html.push('<div class="panel-record">')
                    html.push('<div class="panel panel-info container-fluid">');
                      html.push('<div class="row-heading">');
                        html.push('<i class="material-icons menu">more_horiz</i>')
                        //html.push('<label>' + moduleName + '</label>')
                        //html.push('<label>' + row[moduleName] + '</label>');
                        for (var j = 0; j < browseKeys.length; j++) {
                            if (browseKeys.length === 1) {
                                html.push('<label style="width:25%">' + browseKeys[j] + '</label>');
                                html.push('<label style="width:25%">' + response[i][browseKeys[j]] + '</label>');
                                html.push('<label style="width:25%"></label>');
                                html.push('<label style="width:25%"></label>');
                            }
                            if (browseKeys.length === 2) {
                                html.push('<label style="width:25%">' + browseKeys[j] + '</label>');
                                html.push('<label style="width:25%">' + response[i][browseKeys[j]] + '</label>');
                            }
                            if (browseKeys.length === 3) {
                                html.push('<label style="width:15%">' + browseKeys[j] + '</label>');
                                html.push('<label style="width:15%">' + response[i][browseKeys[j]] + '</label>');
                            }
                        }
                        html.push('<div class="pull-right save"><i class="material-icons">save</i>Save</div>'); 
                      html.push('</div>');
                     html.push('</div>');
                    html.push('<div class="panel-body" style="display:none;" id="' + response[i][rowId] + '"></div>');
                html.push('</div>');
                $moduleRows = jQuery(html.join(''));
                $moduleRows.data('recorddata', response[i]);
                $body.append($moduleRows);
            }

            $control.on('click', '.row-heading', function (e) {

                

                var formKeys = [], formData = [];
                var recordData = jQuery(this).parent().parent().data('recorddata');
                var $rowBody = $control.find('#' + recordData[moduleName + 'Id'] + '.panel-body');
                var $form = jQuery(jQuery('#tmpl-modules-' + moduleName + 'Form').html());
                if ($rowBody.is(':empty')) {
                    $rowBody.append($form);
                    FwModule.openForm($form, 'EDIT')

                    for (var key in recordData) {
                        var value = recordData[key];
                        if ($form.find('[data-datafield="' + key + '"]').length > 0) {
                            FwFormField.setValue($form, '[data-datafield="' + key + '"]', value);
                        }
                    
                    }
                }
                
                if ($rowBody.css('display') === 'none' || $rowBody.css('display') === undefined)  {
                    $rowBody.show("fast");
                } else {
                    $rowBody.hide("fast");
                }

                

            }).find('.save').on('click', function(e) {
                e.stopPropagation();
                var $form = jQuery(this).closest('.panel-record').find('.fwform');
                FwSettings.saveForm(window[moduleName + 'Controller'].Module, $form, false, '', $control, browseKeys, rowId, moduleName, function () {
                    $body.find('.new-row').hide();

                    $body.empty();
                    getRows();
                });    
            });
            
            }, null, $modulecontainer);
        }
        

        if ($settingsPageModules.find('.panel-collapse').css('display') === 'none') {
            $settingsPageModules.find('.arrow-selector').html('keyboard_arrow_up') 
            $settingsPageModules.find('.panel-collapse').show("fast");
        } else {
            $settingsPageModules.find('.arrow-selector').html('keyboard_arrow_down')  
            $settingsPageModules.find('.panel-collapse').hide('fast');
        }
        }).find('.heading-menu').on('click', function (e) { 
            e.stopPropagation(); 
            if (jQuery(this).next().css('display') === 'none') { 
                jQuery(this).next().css('display', 'block'); 
            } else { 
                jQuery(this).next().css('display', 'none'); 
            }
        })


        
        

    var $settings = jQuery('a#title');

    $control.on('keyup', '#settingsSearch', function (e) {
        console.log($settings)
        var val = jQuery.trim(this.value).toUpperCase();
        if (val === "") {
            $settings.parent().show();
        }
        else {
            $settings.closest('div.panel-group').hide();
            $settings.filter(function () {
                return -1 != jQuery(this).text().toUpperCase().indexOf(val);
            }).closest('div.panel-group').show();
        }
    });


    return $settingsPageModules;

};
//----------------------------------------------------------------------------------------------
FwSettings.renderRecordHtml = function ($control, moduleName, values) {
    var $browse;
    

        
        //$browse = jQuery(jQuery('#tmpl-modules-' + moduleName + 'Browse').html());
    //$control.find('#' + moduleName).empty().append($browse);

    //FwBrowse.init($browse);
    //FwBrowse.renderRuntimeHtml($browse);
    //api then loop then reference
};
//----------------------------------------------------------------------------------------------
//FwSettings.openModule() {
//    var moduleName;

//    $moduleRecords = jQuery(jQuery('#tmpl-modules-' + ).html());
//    $moduleGrid.empty().append($moduleRecords);
//    //$moduleRecords.data('ondatabind', function (request) {
//    //    request.module = 'AccidentEstimate';
//    //    request.uniqueids = {
//    //        accidentid: $form.find('div.fwformfield[data-datafield="accident.accidentid"] input').val()
//    //    };
//    //});
    

    
//}
//----------------------------------------------------------------------------------------------