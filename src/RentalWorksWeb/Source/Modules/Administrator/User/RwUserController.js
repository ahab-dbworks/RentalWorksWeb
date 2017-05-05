//----------------------------------------------------------------------------------------------
var RwUserController = {
    Module: 'User'
};
//----------------------------------------------------------------------------------------------
RwUserController.getModuleScreen = function(viewModel, properties) {
    var screen, $browse;

    screen            = {};
    screen.$view      = FwModule.getModuleControl('RwUserController');
    screen.viewModel  = viewModel;
    screen.properties = properties;

    $browse = RwUserController.openBrowse();

    screen.load = function () {
        FwModule.openModuleTab($browse, 'Users', false, 'BROWSE', true);
        FwBrowse.databind($browse);
        FwBrowse.screenload($browse);
    };
    screen.unload = function () {
        FwBrowse.screenunload($browse);
    };

    return screen;
};
//----------------------------------------------------------------------------------------------
RwUserController.openBrowse = function() {
    var $browse;
    $browse = jQuery(jQuery('#tmpl-modules-UserBrowse').html());
    $browse = FwModule.openBrowse($browse);

    return $browse;
};
//----------------------------------------------------------------------------------------------
RwUserController.openForm = function(mode) {
    var $form;

    $form = jQuery(jQuery('#tmpl-modules-UserForm').html());
    $form = FwModule.openForm($form, mode);

    $form
        .on('change', '.cbSecurityExpirePassword, .cbNetExpirePassword', function() {
            RwUserController.setFormProperties($form);
        })
        .on('change', 'div[data-datafield="users.inactive"]', function() {
            var $this, $invaliddate, date;
            $this        = jQuery(this);
            $invaliddate = $form.find('div[data-datafield="users.inactivedate"]');
            if ($this.find('input.fwformfield-value').prop('checked')) {
                date = FwFunc.getDate();
                $invaliddate.find('input.fwformfield-value').val(date);
            } else {
                $invaliddate.find('input.fwformfield-value').val('');
            }
        })
        .on('change', 'div[data-datafield="webusers.webpassword"]', function() {
            var $this, request;
            $this = jQuery(this);
            request = {
                method: 'CheckPasswordComplexity',
                value:  FwFormField.getValue2($this),
                first:  FwFormField.getValue2($form.find('div[data-datafield="users.firstname"]')),
                last:   FwFormField.getValue2($form.find('div[data-datafield="users.lastname"]'))
            }
            FwModule.getData($form, request, function(response) {
                try {
                    if (response.passwordcomplexity.error == true) {
                        $this.addClass('error');
                        FwNotification.renderNotification('ERROR', response.passwordcomplexity.errmsg);
                    } else {
                        $this.removeClass('error');
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            }, $form);
        })
    ;

    return $form;
};
//----------------------------------------------------------------------------------------------
RwUserController.loadForm = function(uniqueids) {
    var $form;
    
    $form = RwUserController.openForm('EDIT');
    $form.find('div.fwformfield[data-datafield="users.usersid"] input').val(uniqueids.usersid);
    FwModule.loadForm(RwUserController.Module, $form);

    return $form;
};
//----------------------------------------------------------------------------------------------
RwUserController.saveForm = function($form, closetab, navigationpath) {
    FwModule.saveForm(RwUserController.Module, $form, closetab, navigationpath);
};
//----------------------------------------------------------------------------------------------
RwUserController.loadAudit = function($form) {
    var uniqueid;
    uniqueid = $form.find('div.fwformfield[data-datafield="users.usersid"] input').val();
    FwModule.loadAudit($form, uniqueid);
};
//----------------------------------------------------------------------------------------------
RwUserController.setFormProperties = function($form) {
    var $cbSecurityExpirePassword, $txtSecurityExpire, $cbNetExpirePassword, $txtNetExpire;

    $cbSecurityExpirePassword = $form.find('.cbSecurityExpirePassword');
    $txtSecurityExpire        = $form.find('.txtSecurityExpire');
    $cbNetExpirePassword      = $form.find('.cbNetExpirePassword');
    $txtNetExpire             = $form.find('.txtNetExpire');

    if ($cbSecurityExpirePassword.find('input').prop('checked')) {
        FwFormField.enable($txtSecurityExpire);
    } else {
        FwFormField.disable($txtSecurityExpire);
    }

    if ($cbNetExpirePassword.find('input').prop('checked')) {
        FwFormField.enable($txtNetExpire);
    } else {
        FwFormField.disable($txtNetExpire);
    }
};
//----------------------------------------------------------------------------------------------