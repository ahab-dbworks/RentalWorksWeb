class User {
    constructor() {
        this.beforeValidate = function ($browse, $grid, request, datafield) {
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
            }
            ;
        };
        this.Module = 'User';
        this.apiurl = 'api/v1/user';
        this.caption = 'User';
        this.ActiveView = 'ALL';
    }
    getModuleScreen() {
        var self = this;
        var screen = {};
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
        var $userLocation = FwMenu.generateDropDownViewBtn(location.location, true);
        var $allLocations = FwMenu.generateDropDownViewBtn('ALL Offices', false);
        $allLocations.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'ALL';
            FwBrowse.search($browse);
        });
        $userLocation.on('click', function () {
            var $browse;
            $browse = jQuery(this).closest('.fwbrowse');
            self.ActiveView = 'OfficeLocationId=' + location.locationid;
            FwBrowse.search($browse);
        });
        var viewLocation = [];
        viewLocation.push($userLocation);
        viewLocation.push($allLocations);
        var $view;
        $view = FwMenu.addViewBtn($menuObject, 'Location', viewLocation);
        return $menuObject;
    }
    ;
    openForm(mode) {
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
        return $form;
    }
    loadForm(uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="UserId"] input').val(uniqueids.UserId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    saveForm($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    afterLoad($form) {
        var $discount = $form.find('div.fwformfield[data-datafield="LimitDiscount"] input').prop('checked');
        var $subDiscount = $form.find('div.fwformfield[data-datafield="LimitSubDiscount"] input').prop('checked');
        var $passwordExpires = $form.find('div.fwformfield[data-datafield="PasswordExpires"] input').prop('checked');
        if ($discount === true) {
            FwFormField.enable($form.find('[data-datafield="DiscountRule"]'));
            FwFormField.enable($form.find('[data-datafield="MaximumDiscount"]'));
        }
        else {
            FwFormField.disable($form.find('[data-datafield="DiscountRule"]'));
            FwFormField.disable($form.find('[data-datafield="MaximumDiscount"]'));
        }
        if ($subDiscount === true) {
            FwFormField.enable($form.find('[data-datafield="DiscountRule"]'));
            FwFormField.enable($form.find('[data-datafield="MaximumSubDiscount"]'));
        }
        else {
            FwFormField.disable($form.find('[data-datafield="DiscountRule"]'));
            FwFormField.disable($form.find('[data-datafield="MaximumSubDiscount"]'));
        }
        if ($passwordExpires === true) {
            FwFormField.enable($form.find('[data-datafield="PasswordExpireDays"]'));
        }
        else {
            FwFormField.disable($form.find('[data-datafield="PasswordExpireDays"]'));
        }
        $form.find('[data-datafield="OfficeLocationId"]').data('onchange', e => {
            $form.find('[data-datafield="WarehouseId"] input.fwformfield-value').val('');
            $form.find('[data-datafield="WarehouseId"] input.fwformfield-text').val('');
        });
    }
    beforeValidateWarehouse($browse, $form, request) {
        let locationId;
        request.uniqueids = {};
        locationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');
        if (locationId) {
            request.uniqueids.LocationId = locationId;
        }
    }
    ;
}
var UserController = new User();
//# sourceMappingURL=User.js.map