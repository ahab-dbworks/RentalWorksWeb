class User {
    Module: string = 'User';
    apiurl: string = 'api/v1/user';
    caption: string = 'User';
    nav: string = 'module/user';
    id: string = '79E93B21-8638-483C-B377-3F4D561F1243';
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    //----------------------------------------------------------------------------------------------
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
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        const self = this;
        //var $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        let $browse = jQuery(this.getBrowseTemplate());
        $browse = FwModule.openBrowse($browse);

        $browse.data('ondatabind', function (request) {
            request.activeviewfields = self.ActiveViewFields;
        });

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems($menuObject) {
        const location = JSON.parse(sessionStorage.getItem('location'));
        const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);
        const $allLocations = FwMenu.generateDropDownViewBtn('ALL Offices', false, "ALL");

        if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
            this.ActiveViewFields.LocationId = [location.locationid];
        }
        let viewLocation = [];
        viewLocation.push($userLocation, $allLocations);
        FwMenu.addViewBtn($menuObject, 'Location', viewLocation, true, "LocationId");
        return $menuObject;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        //var $form;

        //$form = FwModule.loadFormFromTemplate(this.Module);
        let $form = jQuery(this.getFormTemplate());
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
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="UserId"] input').val(uniqueids.UserId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
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

        $form.find('[data-datafield="OfficeLocationId"]').data('onchange', e => {
            $form.find('[data-datafield="WarehouseId"] input.fwformfield-value').val('');
            $form.find('[data-datafield="WarehouseId"] input.fwformfield-text').val('');
        });
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
    //----------------------------------------------------------------------------------------------
    beforeValidateWarehouse($browse: any, $form: any, request: any) {
        let locationId;

        request.uniqueids = {};
        locationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');

        if (locationId) {
            request.uniqueids.LocationId = locationId;
        }
    };
//----------------------------------------------------------------------------------------------
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
    //----------------------------------------------------------------------------------------------
    getBrowseTemplate(): string {
        return `
        <div data-name="User" data-control="FwBrowse" data-type="Browse" id="UserBrowse" class="fwcontrol fwbrowse" data-orderby="Name" data-controller="UserController" data-hasinactive="true">
          <div class="column" data-width="0" data-visible="false">
              <div class="field" data-isuniqueid="true" data-datafield="UserId" data-browsedatatype="key"></div>
          </div>
          <div class="column" data-width="0" data-visible="false">
              <div class="field" data-isuniqueid="false" data-datafield="OfficeLocationId" data-browsedatatype="key"></div>
          </div>
          <div class="column" data-width="200px" data-visible="true">
              <div class="field" data-caption="User" data-isuniqueid="false" data-datafield="Name" data-browsedatatype="text" data-sort="asc"></div>
          </div>
          <div class="column" data-width="150px" data-visible="true">
              <div class="field" data-caption="Login Name" data-isuniqueid="false" data-datafield="LoginName" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="150px" data-visible="true">
              <div class="field" data-caption="Location" data-isuniqueid="false" data-datafield="OfficeLocation" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="150px" data-visible="true">
              <div class="field" data-caption="Department" data-isuniqueid="false" data-datafield="PrimaryDepartment" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="150px" data-visible="true">
              <div class="field" data-caption="Group" data-isuniqueid="false" data-datafield="GroupName" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column spacer" data-width="auto" data-visible="true"></div>
        </div>
        `;
    }
    getFormTemplate(): string {
        return `
        <div id="userform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="User" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="UserController">
          <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="UserId"></div>
          <div id="userform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
              <div data-type="tab" id="usertab" class="tab" data-tabpageid="usertabpage" data-caption="User"></div>
              <div data-type="tab" id="contacttab" class="tab" data-tabpageid="contacttabpage" data-caption="Contact"></div>
              <div data-type="tab" id="securitytab" class="tab" data-tabpageid="securitytabpage" data-caption="Security"></div>
              <div data-type="tab" id="departmenttab" class="tab" data-tabpageid="departmenttabpage" data-caption="Department"></div>
              <div data-type="tab" id="permissionstab" class="tab" data-tabpageid="permissionstabpage" data-caption="Permissions"></div>
              <div data-type="tab" id="notestab" class="tab" data-tabpageid="notestabpage" data-caption="Notes"></div>
              <div data-type="tab" id="picturetab" class="tab" data-tabpageid="picturetabpage" data-caption="Picture"></div>
            </div>
            <div class="tabpages">
              <div data-type="tabpage" id="usertabpage" class="tabpage" data-tabid="usertab">
                <div class="formpage">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="User">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="First Name" data-datafield="FirstName" style="float:left;width:250px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="MI" data-datafield="MiddleInitial" style="float:left;width:50px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Last Name" data-datafield="LastName" style="float:left;width:250px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Inactive" data-datafield="Inactive" style="float:left;width:120px;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Login">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Username" data-datafield="LoginName" style="float:left; width: 250px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Barcode No" data-datafield="" style="float:left; width:250px;"></div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="password" class="fwcontrol fwformfield" data-caption="Password" data-datafield="Password" data-required="true" style="float:left; width:250px;"></div>
                        <!--<div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Single Sign On" data-datafield="" style="float:left; width:250px;"></div>-->
                      </div>
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Group" data-datafield="GroupId" data-validationname="GroupValidation" data-displayfield="GroupName" style="float:left; width:340px;" data-required="true"></div>
                        <div data-control="FwFormField" data-type="color" class="fwcontrol fwformfield" data-caption="Schedule Color" data-datafield="ScheduleColor" data-required="true" style="float:left; width:160px;"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Working">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" data-required="true" style="float:left; width: 250px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" data-formbeforevalidate="beforeValidateWarehouse" data-required="true" style="float:left; width: 250px;"></div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Title" data-datafield="UserTitleId" data-displayfield="UserTitle" data-validationname="ContactTitleValidation" style="float:left; width: 250px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Email" data-datafield="Email" style="float:left; width: 250px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div data-type="tabpage" id="contacttabpage" class="tabpage" data-tabid="contacttab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="formcolumn">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Address">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address" data-datafield="Address1"></div>
                        </div>
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="Address2"></div>
                        </div>
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="City" style="float:left;width:100%;"></div>
                        </div>
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="State" data-datafield="State" data-displayfield="State" data-validationname="StateValidation" style="float:left; width:49%"></div>
                          <div data-control="FwFormField" data-type="zipcode" class="fwcontrol fwformfield" data-caption="Zip" data-datafield="ZipCode" style="float:left;width:50%;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="CountryId" data-displayfield="Country" data-validationname="CountryValidation" style="float:left; width:200px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="formcolumn">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Phone">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Office" data-datafield="OfficePhone" style="float:left;width:50%;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Ext" data-datafield="OfficeExtension" style="float:left;width:40%;"></div>
                        </div>
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Direct" data-datafield="DirectPhone" style="float:left;width:50%;"></div>
                        </div>
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Home" data-datafield="HomePhone" style="float:left;width:50%;"></div>
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Cellular" data-datafield="Cellular" style="float:left;width:50%;"></div>
                        </div>
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Pager No." data-datafield="Pager" style="float:left;width:50%;"></div>
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Pin No." data-datafield="PagerPin" style="float:left;width:50%;"></div>
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Fax" data-datafield="Fax" style="float:left;width:50%;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div data-type="tabpage" id="departmenttabpage" class="tabpage" data-tabid="departmenttab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="formcolumn" style="width:15%;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Primary">
                        <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="DefaultDepartmentType" style="width:150px;">
                          <div data-value="R" data-caption="Rental"></div>
                          <div data-value="S" data-caption="Sales"></div>
                          <div data-value="L" data-caption="Labor"></div>
                          <div data-value="M" data-caption="Misc"></div>
                          <div data-value="P" data-caption="Parts"></div>
                          <div data-value="SP" data-caption="Facilities"></div>
                          <div data-value="T" data-caption="Transportation"></div>
                        </div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:30%">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Company Departments">
                        <div class="formcolumn">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Rental" data-datafield="RentalDepartmentId" data-displayfield="RentalDepartment" data-validationname="DepartmentValidation" style="float:left; width:200px;"></div>
                        </div>
                        <div class="formcolumn">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Sales" data-datafield="SalesDepartmentId" data-displayfield="SalesDepartment" data-validationname="DepartmentValidation" style="float:left; width:200px;"></div>
                        </div>
                        <div class="formcolumn">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Labor" data-datafield="LaborDepartmentId" data-displayfield="LaborDepartment" data-validationname="DepartmentValidation" style="float:left; width:200px;"></div>
                        </div>
                        <div class="formcolumn">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Misc" data-datafield="MiscDepartmentId" data-displayfield="MiscDepartment" data-validationname="DepartmentValidation" style="float:left; width:200px;"></div>
                        </div>
                        <div class="formcolumn">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Parts" data-datafield="PartsDepartmentId" data-displayfield="PartsDepartment" data-validationname="DepartmentValidation" style="float:left; width:200px;"></div>
                        </div>
                        <div class="formcolumn">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Facilities" data-datafield="FacilityDepartmentId" data-displayfield="FacilityDepartment" data-validationname="DepartmentValidation" style="float:left; width:200px;"></div>
                        </div>
                        <div class="formcolumn">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Transportation" data-datafield="TransportationDepartmentId" data-displayfield="TransportationDepartment" data-validationname="DepartmentValidation" style="float:left; width:200px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:30%">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Inventory Types">
                        <div class="formcolumn">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Rental" data-datafield="RentalInventoryTypeId" data-displayfield="RentalInventoryType" data-formbeforevalidate="beforeValidate" data-validationname="InventoryTypeValidation" style="float:left; width:200px;"></div>
                        </div>
                        <div class="formcolumn">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Sales" data-datafield="SalesInventoryTypeId" data-displayfield="SalesInventoryType" data-formbeforevalidate="beforeValidate" data-validationname="InventoryTypeValidation" style="float:left; width:200px;"></div>
                        </div>
                        <div class="formcolumn">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Parts" data-datafield="PartsInventoryTypeId" data-displayfield="PartsInventoryType" data-formbeforevalidate="beforeValidate" data-validationname="InventoryTypeValidation" style="float:left; width:200px;"></div>
                        </div>
                        <div class="formcolumn">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Transportation" data-datafield="TransportationTypeId" data-displayfield="TransportationType" data-formbeforevalidate="beforeValidate" data-validationname="InventoryTypeValidation" style="float:left; width:200px;"></div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Rate Departments">
                        <div class="formcolumn">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Labor" data-datafield="LaborTypeId" data-displayfield="LaborType" data-validationname="LaborTypeValidation" style="float:left; width:200px;"></div>
                        </div>
                        <div class="formcolumn">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Misc" data-datafield="MiscTypeId" data-displayfield="MiscType" data-validationname="MiscTypeValidation" style="float:left; width:200px;"></div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Facilities Types">
                        <div class="formcolumn">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Facilities" data-datafield="FacilityTypeId" data-displayfield="FacilityType" data-validationname="FacilityTypeValidation" style="float:left; width:200px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div data-type="tabpage" id="permissionstabpage" class="tabpage" data-tabid="permissionstab">
                <div class="formpage">
                  <div class="formcolumn" style="width:40%;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quotes/Order">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Do not allow Misc. I-Codes on">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Quote" data-datafield="NoMiscellaneousOnQuotes" style="float:left;width:120px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Order" data-datafield="NoMiscellaneousOnOrders" style="float:left;width:120px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Purchase Order" data-datafield="NoMiscellaneousOnPurchaseOrders" style="float:left;width:180px;"></div>
                      </div>
                      <div class="formrow">
                        <div class="formcolumn">
                          <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Limit D/W">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Limit D/W" data-datafield="LimitDaysPerWeek" style="float:left;width:100%;"></div>
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Credit Limit Override" data-datafield="AllowCreditLimitOverride" style="float:left;width:100%;"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Allow User to enter D/W from:" data-datafield="MinimumDaysPerWeek" style="float:left;width:100%;"></div>
                          </div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Limit Discounting">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Limit Discount Percent" data-datafield="LimitDiscount" style="float:left;width:100%;"></div>
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield" data-caption="Allow User to enter Discount Percent up to:" data-datafield="MaximumDiscount" style="float:left;width:45%;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Limit Sub Discount Percent" data-datafield="LimitSubDiscount" style="float:left;width:100%;"></div>
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield" data-caption="Allow User to enter Discount Percent up to:" data-datafield="MaximumSubDiscount" style="float:left;width:45%;"></div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Discount Rule">
                        <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="DiscountRule" style="width:350px;">
                          <div data-value="DISALLOW" data-caption="Prevent discounts larger than this amount"></div>
                          <div data-value="ALLOW" data-caption="Allow larger discounts, but prevent printing"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="formcolumn" style="width:40%;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Staging / Check-Out">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow user to increase/decrease quantity or add to Order at Staging" data-datafield="StagingAllowIncreaseDecreaseOrderQuantity" style="float:left;width:100%;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow user to substitute items on Order at Staging" data-datafield="AllowSubstitutesAtStaging" style="float:left;width:100%;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Delete the original item from the Order when Substitute is Staged" data-datafield="DeleteOriginalOnSubstitution" style="float:left;width:100%;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Staging of Items when Reserved on other Orders/Quotes" data-datafield="AllowStagingOfItemsWhenReservedOnOtherOrdersQuotes" style="float:left;width:100%;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Contract if Deal requires a PO and Order has a Pending PO" data-datafield="AllowContractIfDealRequiresPOAndOrderHasPendingPO" style="float:left;width:100%;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Contract if Pending Items exist" data-datafield="AllowContractIfPendingItemsExist" style="float:left;width:100%;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Contract if Customer/Deal does not have Approved Credit" data-datafield="AllowContractIfCustomerDealDoesNotHaveApprovedCredit" style="float:left;width:100%;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Contract if Customer/Deal is over their Credit Limit" data-datafield="AllowContractIfCustomerDealIsOverTheirCreditLimit" style="float:left;width:100%;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Contract if Customer/Deal does not have valid Insurance Certificate" data-datafield="AllowContractIfCustomerDealDoesNotHaveValidInsuranceCertificate" style="float:left;width:100%;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Contract if Customer/Deal does not have valid Non-Tax Certificate" data-datafield="AllowContractIfCustomerDealDoesNotHaveValidNonTaxCertificate" style="float:left;width:100%;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow user to Receive Subs when Positive Conflict exists" data-datafield="AllowReceiveSubsWhenPositiveConflictExists" style="float:left;width:100%;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Staging of Unreserved Consigned Items" data-datafield="AllowStagingOfUnreservedConsignedItems" style="float:left;width:100%;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contract Session">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow user to cancel Contracts" data-datafield="AllowCancelContract" style="float:left;width:100%;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="RentalWorks Mobile/QuikScan">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow user to create Contracts" data-datafield="QuikScanAllowCreateContract" style="float:left;width:100%;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption='Allow "Apply All Quantity Items" button' data-datafield="QuikScanAllowApplyAll" style="float:left;width:100%;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Exchange">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Cross I-Code Exchange" data-datafield="AllowCrossICodeExchange" style="float:left;width:100%;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Cross I-Code Pending Exchange" data-datafield="AllowCrossICodePendingExchange" style="float:left;width:100%;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Availability">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow user to change Availability Priority" data-datafield="AllowChangeAvailabilityPriority" style="float:left;width:100%;"></div>
                    </div>
                  </div>
                </div>
              </div>
              <div data-type="tabpage" id="securitytabpage" class="tabpage" data-tabid="securitytab">
                <div class="formpage">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="User must change password on next login" data-datafield="UserMustChangePassword"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Login is locked out" data-datafield="AccountLocked"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield cbSecurityExpirePassword" data-caption="Expire password" data-datafield="PasswordExpires"></div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield txtSecurityExpire" data-caption="Expire (days)" data-datafield="PasswordExpireDays" data-enabled="false" style="float:left;width:49%"></div>
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Last Changed" data-datafield="PasswordUpdatedDateTime" data-enabled="false" style="float:left;width:49%"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!--<div data-type="tabpage" id="optionstabpage" class="tabpage" data-tabid="optionstab">
                  <div class="formpage">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Email Signature">
                          <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                              <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Email Signature" data-datafield=""></div>
                          </div>
                      </div>
                  </div>
              </div>-->
              <div data-type="tabpage" id="notestabpage" class="tabpage" data-tabid="notestab">
                <div class="formpage">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Note">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Notes" data-datafield="Memo"></div>
                    </div>
                  </div>
                </div>
              </div>
              <div data-type="tabpage" id="picturetabpage" class="tabpage" data-tabid="picturetab">
                <div class="formpage">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="User Photo">
                    <div class="fwcontrol fwappimage" data-control="FwAppImage" data-type="" data-uniqueid1field="UserId" data-description="" data-rectype="F"></div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        `;
    }
}
//----------------------------------------------------------------------------------------------
var UserController = new User();