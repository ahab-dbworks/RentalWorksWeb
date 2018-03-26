class User {
    Module: string;
    apiurl: string;
    caption: string;
    ActiveView: string;

    constructor() {
        this.Module = 'User';
        this.apiurl = 'api/v1/user';
        this.caption = 'User';
        this.ActiveView = 'ALL';
    }

    getModuleScreen() {
        var self = this;
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, self.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }

    openBrowse() {
        var self = this;
        var $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        var location = JSON.parse(sessionStorage.getItem('location'));
        self.ActiveView = 'OfficeLocationId=' + location.locationid;

        $browse.data('ondatabind', function (request) {
            request.activeview = self.ActiveView;
        });

        return $browse;
    }

    addBrowseMenuItems($menuObject) {
        var self = this;
        var location = JSON.parse(sessionStorage.getItem('location'));
        var $allLocations = FwMenu.generateDropDownViewBtn('ALL Offices', true);
        var $userLocation = FwMenu.generateDropDownViewBtn(location.location, false);
        $allLocations.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'OfficeLocationId=ALL';
            FwBrowse.databind($browse);
        });
        $userLocation.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'OfficeLocationId=' + location.locationid;
            FwBrowse.databind($browse);
        });
        var viewLocation = [];
        viewLocation.push($userLocation);
        viewLocation.push($allLocations);
        var $view;
        $view = FwMenu.addViewBtn($menuObject, 'Location', viewLocation);
        return $menuObject;
    };

    openForm(mode: string) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);

        $form.find('[data-datafield="LimitDiscount"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="DiscountRule"]'));
                FwFormField.enable($form.find('[data-datafield="MaximumDiscount"]'));
            }
            else {
                FwFormField.disable($form.find('[data-datafield="DiscountRule"]'));
                FwFormField.disable($form.find('[data-datafield="MaximumDiscount"]'));
            }
        });

        $form.find('[data-datafield="LimitSubDiscount"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="DiscountRule"]'));
                FwFormField.enable($form.find('[data-datafield="MaximumSubDiscount"]'));
            }
            else {
                FwFormField.disable($form.find('[data-datafield="DiscountRule"]'));
                FwFormField.disable($form.find('[data-datafield="MaximumSubDiscount"]'));
            }
        });

        $form.find('[data-datafield="PasswordExpires"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="PasswordExpireDays"]'));
            }
            else {
                FwFormField.disable($form.find('[data-datafield="PasswordExpireDays"]'));
            }
        });



        //$form
        //    .on('change', '.cbSecurityExpirePassword, .cbNetExpirePassword', function () {
        //        this.setFormProperties($form);
        //    })
        //    .on('change', 'div[data-datafield="Inactive"]', function () {
        //        var $this, $invaliddate, date;
        //        $this = jQuery(this);
        //        $invaliddate = $form.find('div[data-datafield="users.inactivedate"]');
        //        if ($this.find('input.fwformfield-value').prop('checked')) {
        //            date = FwFunc.getDate();
        //            $invaliddate.find('input.fwformfield-value').val(date);
        //        } else {
        //            $invaliddate.find('input.fwformfield-value').val('');
        //        }
        //    })
        //    .on('change', 'div[data-datafield="webusers.webpassword"]', function () {
        //        var $this, request;
        //        $this = jQuery(this);
        //        request = {
        //            method: 'CheckPasswordComplexity',
        //            value: FwFormField.getValue2($this),
        //            first: FwFormField.getValue2($form.find('div[data-datafield="users.firstname"]')),
        //            last: FwFormField.getValue2($form.find('div[data-datafield="users.lastname"]'))
        //        }
        //        FwModule.getData($form, request, function (response) {
        //            try {
        //                if (response.passwordcomplexity.error == true) {
        //                    $this.addClass('error');
        //                    FwNotification.renderNotification('ERROR', response.passwordcomplexity.errmsg);
        //                } else {
        //                    $this.removeClass('error');
        //                }
        //            } catch (ex) {
        //                FwFunc.showError(ex);
        //            }
        //        }, $form);
        //    })
        //    ;

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="UserId"] input').val(uniqueids.UserId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, { closetab: closetab, navigationpath: navigationpath });
    }

    afterLoad($form: any) {
        var $discount = $form.find('div.fwformfield[data-datafield="LimitDiscount"] input').prop('checked');
        var $subDiscount = $form.find('div.fwformfield[data-datafield="LimitSubDiscount"] input').prop('checked');
        var $passwordExpires = $form.find('div.fwformfield[data-datafield="PasswordExpires"] input').prop('checked');

        if ($discount === true) {
            FwFormField.enable($form.find('[data-datafield="DiscountRule"]'));
            FwFormField.enable($form.find('[data-datafield="MaximumDiscount"]'));
        } else {
            FwFormField.disable($form.find('[data-datafield="DiscountRule"]'));
            FwFormField.disable($form.find('[data-datafield="MaximumDiscount"]'));
        }

        if ($subDiscount === true) {
            FwFormField.enable($form.find('[data-datafield="DiscountRule"]'));
            FwFormField.enable($form.find('[data-datafield="MaximumSubDiscount"]'));
        } else {
            FwFormField.disable($form.find('[data-datafield="DiscountRule"]'));
            FwFormField.disable($form.find('[data-datafield="MaximumSubDiscount"]'));
        }

        if ($passwordExpires === true) {
            FwFormField.enable($form.find('[data-datafield="PasswordExpireDays"]'));
        } else {
            FwFormField.disable($form.find('[data-datafield="PasswordExpireDays"]'));
        }
    }

    //setFormProperties = function ($form) {
    //    var $cbSecurityExpirePassword, $txtSecurityExpire, $cbNetExpirePassword, $txtNetExpire;

    //    $cbSecurityExpirePassword = $form.find('.cbSecurityExpirePassword');
    //    $txtSecurityExpire = $form.find('.txtSecurityExpire');
    //    $cbNetExpirePassword = $form.find('.cbNetExpirePassword');
    //    $txtNetExpire = $form.find('.txtNetExpire');

    //    if ($cbSecurityExpirePassword.find('input').prop('checked')) {
    //        FwFormField.enable($txtSecurityExpire);
    //    } else {
    //        FwFormField.disable($txtSecurityExpire);
    //    }

    //    if ($cbNetExpirePassword.find('input').prop('checked')) {
    //        FwFormField.enable($txtNetExpire);
    //    } else {
    //        FwFormField.disable($txtNetExpire);
    //    }
    //};

    beforeValidate = function ($browse, $grid, request, datafield) {


        switch (datafield) {
            case 'RentalInventoryTypeId':
                request.uniqueids = {
                    Rental: true
                };
                break;
            case 'SalesInventoryTypeId':
                request.uniqueids = {
                    Sales: true
                };
                break;
            case 'PartsInventoryTypeId':
                request.uniqueids = {
                    Parts: true
                };
                break;
            case 'TransportationTypeId':
                request.uniqueids = {
                    Transportation: true
                };
                break;
        };
    }

}

var UserController = new User();