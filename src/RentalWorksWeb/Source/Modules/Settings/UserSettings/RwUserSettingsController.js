//----------------------------------------------------------------------------------------------
var RwUserSettingsController = {
    Module: 'UserSettings'
};
//----------------------------------------------------------------------------------------------
RwUserSettingsController.getModuleScreen = function(viewModel, properties) {
    var screen, $form;

    screen            = {};
    screen.$view      = FwModule.getModuleControl('RwUserSettingsController');
    screen.viewModel  = viewModel;
    screen.properties = properties;

    $form = RwUserSettingsController.loadForm();

    screen.load = function () {
        FwModule.openModuleTab($form, 'User Settings', false, 'FORM', true);
    };
    screen.unload = function () {
    };

    return screen;
};
//----------------------------------------------------------------------------------------------
RwUserSettingsController.openForm = function(mode) {
    var $form, $browsedefaultrows, $applicationtheme;

    $form = jQuery(jQuery('#tmpl-modules-UserSettingsForm').html());
    $form = FwModule.openForm($form, mode);

    $browsedefaultrows = $form.find('.browsedefaultrows');
    FwFormField.loadItems($browsedefaultrows, [
        {value:'5',    text:'5'},
        {value:'10',   text:'10'},
        {value:'15',   text:'15'},
        {value:'20',   text:'20'},
        {value:'25',   text:'25'},
        {value:'30',   text:'30'},
        {value:'35',   text:'35'},
        {value:'40',   text:'40'},
        {value:'45',   text:'45'},
        {value:'50',   text:'50'},
        {value:'100',  text:'100'},
        {value:'200',  text:'200'},
        {value:'500',  text:'500'},
        {value:'1000', text:'1000'}
    ]);

    $applicationtheme = $form.find('.applicationtheme');
    FwFormField.loadItems($applicationtheme, [
        {value:'theme-default',   text:'Default'},
        {value:'theme-classic',   text:'Classic'}
    ]);

    return $form;
};
//----------------------------------------------------------------------------------------------
RwUserSettingsController.loadForm = function() {
    var $form, request = {};
    
    $form = RwUserSettingsController.openForm('EDIT');

    request.method = 'LoadSettings';
    FwModule.getData($form, request, function(response) {
        FwFormField.setValue($form, 'div[data-datafield="#settings.browsedefaultrows"]', response.usersettings.browsedefaultrows);
        FwFormField.setValue($form, 'div[data-datafield="#settings.applicationtheme"]',  response.usersettings.applicationtheme);
    }, $form);

    return $form;
};
//----------------------------------------------------------------------------------------------
RwUserSettingsController.saveForm = function($form, closetab, navigationpath) {
    var request = {}, $fwformfields, fields;
    
    $fwformfields = $form.find('.fwformfield');
    fields        = {};
    $fwformfields.each(function(index, element) {
        var $fwformfield, originalValue, dataField, value, isValidDataField, getAllFields, isBlank, isCalculatedField;
        
        $fwformfield   = jQuery(element);
        dataField      = $fwformfield.attr('data-datafield');
        value          = FwFormField.getValue2($fwformfield);

        field = {
            datafield: dataField,
            value:     value
        };
        fields[dataField] = field;
    });

    request.method = 'SaveSettings';
    request.fields = fields;
    FwModule.getData($form, request, function(response) {
        $form.attr('data-modified', false);
        $form.find('.btn[data-type="SaveMenuBarButton"]').addClass('disabled');
        jQuery('#' + $form.parent().attr('data-tabid')).find('.modified').html('');
        sessionStorage.setItem('browsedefaultrows', response.usersettings.browsedefaultrows);
        sessionStorage.setItem('applicationtheme',  response.usersettings.applicationtheme);
        //jQuery('html').removeClassPrefix('theme-').addClass(response.usersettings.applicationtheme)
        location.reload();
    }, $form);
};
//----------------------------------------------------------------------------------------------