routes.push({ pattern: /^module\/user$/, action: function (match) { return UserController.getModuleScreen(); } });
class User {
    constructor() {
        this.Module = 'User';
        this.apiurl = 'api/v1/user';
        this.caption = 'User';
        this.nav = 'module/user';
        this.id = 'CE9E187C-288F-44AB-A54A-27A8CFF6FF53';
        this.ActiveViewFields = {};
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
    }
    getModuleScreen() {
        const screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
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
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        $browse.data('ondatabind', request => {
            request.activeviewfields = this.ActiveViewFields;
        });
        return $browse;
    }
    addBrowseMenuItems($menuObject) {
        const location = JSON.parse(sessionStorage.getItem('location'));
        const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);
        const $allLocations = FwMenu.generateDropDownViewBtn('ALL Offices', false, "ALL");
        if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
            this.ActiveViewFields.LocationId = [location.locationid];
        }
        const viewLocation = [];
        viewLocation.push($userLocation, $allLocations);
        FwMenu.addViewBtn($menuObject, 'Location', viewLocation, true, "LocationId");
        return $menuObject;
    }
    openForm(mode) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
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
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="UserId"] input').val(uniqueids.UserId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    saveForm($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    afterLoad($form) {
        var discount = $form.find('div.fwformfield[data-datafield="LimitDiscount"] input').prop('checked');
        var subDiscount = $form.find('div.fwformfield[data-datafield="LimitSubDiscount"] input').prop('checked');
        var passwordExpires = $form.find('div.fwformfield[data-datafield="PasswordExpires"] input').prop('checked');
        if (discount === true) {
            FwFormField.enable($form.find('[data-datafield="DiscountRule"]'));
            FwFormField.enable($form.find('[data-datafield="MaximumDiscount"]'));
        }
        else {
            FwFormField.disable($form.find('[data-datafield="DiscountRule"]'));
            FwFormField.disable($form.find('[data-datafield="MaximumDiscount"]'));
        }
        if (subDiscount === true) {
            FwFormField.enable($form.find('[data-datafield="DiscountRule"]'));
            FwFormField.enable($form.find('[data-datafield="MaximumSubDiscount"]'));
        }
        else {
            FwFormField.disable($form.find('[data-datafield="DiscountRule"]'));
            FwFormField.disable($form.find('[data-datafield="MaximumSubDiscount"]'));
        }
        if (passwordExpires === true) {
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
        request.uniqueids = {};
        const locationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');
        if (locationId) {
            request.uniqueids.LocationId = locationId;
        }
    }
    ;
}
var UserController = new User();
//# sourceMappingURL=User.js.map