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
        screen.viewModel = {};
        screen.properties = {};
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
        let $browse = jQuery(this.getBrowseTemplate());
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
    ;
    openForm(mode) {
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
        request.uniqueids = {};
        const locationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');
        if (locationId) {
            request.uniqueids.LocationId = locationId;
        }
    }
    ;
    getBrowseTemplate() {
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
        </div>`;
    }
    getFormTemplate() {
        return `
        <div id="userform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="User" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="UserController">
          <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="UserId"></div>
          <div id="userform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
              <div data-type="tab" id="usertab" class="tab" data-tabpageid="usertabpage" data-caption="User"></div>
              <div data-type="tab" id="contacttab" class="tab" data-tabpageid="contacttabpage" data-caption="Contact"></div>
              <div data-type="tab" id="departmenttab" class="tab" data-tabpageid="departmenttabpage" data-caption="Department"></div>
              <div data-type="tab" id="permissionstab" class="tab" data-tabpageid="permissionstabpage" data-caption="Permissions"></div>
              <div data-type="tab" id="securitytab" class="tab" data-tabpageid="securitytabpage" data-caption="Security"></div>
              <div data-type="tab" id="notestab" class="tab" data-tabpageid="notestabpage" data-caption="Notes"></div>
              <div data-type="tab" id="picturetab" class="tab" data-tabpageid="picturetabpage" data-caption="Picture"></div>
            </div>
            <div class="tabpages">
              <!-- ##### USER tab ##### -->
              <div data-type="tabpage" id="usertabpage" class="tabpage" data-tabid="usertab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:2 1 700px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="User">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="First Name" data-datafield="FirstName" style="flex:2 1 300px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="MI" data-datafield="MiddleInitial" style="flex:1 1 50px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Last Name" data-datafield="LastName" style="flex:2 1 350px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Title" data-datafield="UserTitleId" data-displayfield="UserTitle" data-validationname="ContactTitleValidation" style="flex:1 1 250px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Email" data-datafield="Email" style="flex:2 1 450px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 150px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Status">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Inactive" data-datafield="Inactive" style="flex:1 1 100px;margin-top:10px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="color" class="fwcontrol fwformfield" data-caption="Schedule Color" data-datafield="ScheduleColor" data-required="true" style="flex:1 1 125px;margin-top:-9px;"></div>
                        </div>
                      </div>      
                    </div>
                  </div>
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Login">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Username" data-datafield="LoginName" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Group" data-datafield="GroupId" data-validationname="GroupValidation" data-displayfield="GroupName" style="flex:1 1 125px;" data-required="true"></div>
                        <div data-control="FwFormField" data-type="password" class="fwcontrol fwformfield" data-caption="Password" data-datafield="Password" data-required="true" style="flex:1 1 125px;"></div>
                        <!--<div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Single Sign On" data-datafield="" style="flex:1 1 125px;"></div>-->
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Barcode No" data-datafield="" style="flex:1 1 125px;"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Location / Warehouse">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" data-required="true" style="flex:1 1 250px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" data-formbeforevalidate="beforeValidateWarehouse" data-required="true" style="flex:1 1 250px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- ##### CONTACT tab ##### -->
              <div data-type="tabpage" id="contacttabpage" class="tabpage" data-tabid="contacttab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 550px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Address">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address" data-datafield="Address1" style="flex:1 1 275px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="Address2" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="City" style="flex:1 1 275px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="State" data-datafield="State" data-displayfield="State" data-validationname="StateValidation" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="zipcode" class="fwcontrol fwformfield" data-caption="Zip" data-datafield="ZipCode" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="CountryId" data-displayfield="Country" data-validationname="CountryValidation" style="flex:1 1 175px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="formcolumn" style="flex:1 1 300px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Phone">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Office" data-datafield="OfficePhone" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Ext" data-datafield="OfficeExtension" style="flex:1 1 150px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Direct" data-datafield="DirectPhone" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Fax" data-datafield="Fax" style="flex:1 1 150px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Home" data-datafield="HomePhone" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Cellular" data-datafield="Cellular" style="flex:1 1 150px;"></div>
                        </div>
                        <!--
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Pager No." data-datafield="Pager" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Pin No." data-datafield="PagerPin" style="flex:1 1 150px;"></div>
                        </div>
                        -->
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- ##### DEPARTMENT tab ##### -->
              <div data-type="tabpage" id="departmenttabpage" class="tabpage" data-tabid="departmenttab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 150px;">
                      <div class="flexrow">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Primary">
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="DefaultDepartmentType" style="flex:1 1 150px;">
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
                    </div>
                    <div class="flexcolumn" style="flex:2 1 350px;">
                      <div class="flexrow">     
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Company Departments">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Rental" data-datafield="RentalDepartmentId" data-displayfield="RentalDepartment" data-validationname="DepartmentValidation" style="flex:1 1 300px;"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Sales" data-datafield="SalesDepartmentId" data-displayfield="SalesDepartment" data-validationname="DepartmentValidation" style="flex:1 1 300px;"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Labor" data-datafield="LaborDepartmentId" data-displayfield="LaborDepartment" data-validationname="DepartmentValidation" style="flex:1 1 300px;"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Misc" data-datafield="MiscDepartmentId" data-displayfield="MiscDepartment" data-validationname="DepartmentValidation" style="flex:1 1 300px;"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Parts" data-datafield="PartsDepartmentId" data-displayfield="PartsDepartment" data-validationname="DepartmentValidation" style="flex:1 1 300px;"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Facilities" data-datafield="FacilityDepartmentId" data-displayfield="FacilityDepartment" data-validationname="DepartmentValidation" style="flex:1 1 300px;"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Transportation" data-datafield="TransportationDepartmentId" data-displayfield="TransportationDepartment" data-validationname="DepartmentValidation" style="flex:1 1 300px;"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:2 1 350px;">
                      <!-- Defatult Inventory Types -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Inventory Types">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Rental" data-datafield="RentalInventoryTypeId" data-displayfield="RentalInventoryType" data-formbeforevalidate="beforeValidate" data-validationname="InventoryTypeValidation" style="flex:1 1 300px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Sales" data-datafield="SalesInventoryTypeId" data-displayfield="SalesInventoryType" data-formbeforevalidate="beforeValidate" data-validationname="InventoryTypeValidation" style="flex:1 1 300px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Parts" data-datafield="PartsInventoryTypeId" data-displayfield="PartsInventoryType" data-formbeforevalidate="beforeValidate" data-validationname="InventoryTypeValidation" style="flex:1 1 300px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Transportation" data-datafield="TransportationTypeId" data-displayfield="TransportationType" data-formbeforevalidate="beforeValidate" data-validationname="InventoryTypeValidation" style="flex:1 1 300px;"></div>
                        </div>
                      </div>
                      <!-- Defatult Rate Departments -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Rate Departments">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Labor" data-datafield="LaborTypeId" data-displayfield="LaborType" data-validationname="LaborTypeValidation" style="flex:1 1 300px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Misc" data-datafield="MiscTypeId" data-displayfield="MiscType" data-validationname="MiscTypeValidation" style="flex:1 1 300px;"></div>
                        </div>
                      </div>
                      <!-- Defatult Facitlites Types -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Facilities Types">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Facilities" data-datafield="FacilityTypeId" data-displayfield="FacilityType" data-validationname="FacilityTypeValidation" style="flex:1 1 300px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- ##### PERMISSIONS tab ##### -->
              <div data-type="tabpage" id="permissionstabpage" class="tabpage" data-tabid="permissionstab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 550px;">
                      <div class="flexrow">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Do not allow Misc. Rental I-Codes on">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Quote" data-datafield="NoMiscellaneousOnQuotes" style="float:left;width:120px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Order" data-datafield="NoMiscellaneousOnOrders" style="float:left;width:120px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Purchase Order" data-datafield="NoMiscellaneousOnPurchaseOrders" style="float:left;width:180px;"></div>
                        </div>
                      </div>
                      <div class="flexrow">
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
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 300px;">
                      <div class="flexrow">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contract Session">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow user to cancel Contracts" data-datafield="AllowCancelContract" style="float:left;width:100%;"></div>
                        </div>
                      </div>
                      <div class="flexrow">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="RentalWorks Mobile/QuikScan">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow user to create Contracts" data-datafield="QuikScanAllowCreateContract" style="float:left;width:100%;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption='Allow "Apply All Quantity Items" button' data-datafield="QuikScanAllowApplyAll" style="float:left;width:100%;"></div>
                        </div>
                      </div>
                      <div class="flexrow">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Exchange">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Cross I-Code Exchange" data-datafield="AllowCrossICodeExchange" style="float:left;width:100%;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Cross I-Code Pending Exchange" data-datafield="AllowCrossICodePendingExchange" style="float:left;width:100%;"></div>
                        </div>                  
                      </div>
                      <div class="flexrow">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Availability">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow user to change Availability Priority" data-datafield="AllowChangeAvailabilityPriority" style="float:left;width:100%;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- ##### SECURITY tab ##### -->
              <div data-type="tabpage" id="securitytabpage" class="tabpage" data-tabid="securitytab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Login Security" style="flex:0 1 550px;">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="User must change password on next login" data-datafield="UserMustChangePassword" style="flex:1 1 350px;margin-left:10px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Login is locked out" data-datafield="AccountLocked" style="flex:1 1 200px;margin-left:10px;"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Password Security" style="flex:0 1 550px;">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield cbSecurityExpirePassword" data-caption="Expire password" data-datafield="PasswordExpires" style="flex:1 1 125px;margin-left:10px;"></div>
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield txtSecurityExpire" data-caption="Expire (days)" data-datafield="PasswordExpireDays" data-enabled="false" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Last Changed" data-datafield="PasswordUpdatedDateTime" data-enabled="false" style="flex:1 1 125px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- ##### OPTIONS tab ##### -->
              <!--<div data-type="tabpage" id="optionstabpage" class="tabpage" data-tabid="optionstab">
                  <div class="flexpage">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Email Signature">
                          <div class="flexrow">
                              <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Email Signature" data-datafield=""></div>
                          </div>
                      </div>
                  </div>
              </div>-->

              <!-- ##### NOTES tab ##### -->
              <div data-type="tabpage" id="notestabpage" class="tabpage" data-tabid="notestab">
                <div class="flexpage">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Note">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Notes" data-datafield="Memo"></div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- ##### PICTURE tab ##### -->
              <div data-type="tabpage" id="picturetabpage" class="tabpage" data-tabid="picturetab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="User Photo">
                      <div class="fwcontrol fwappimage" data-control="FwAppImage" data-type="" data-uniqueid1field="UserId" data-description="" data-rectype="F" style="margin:10px;"></div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>`;
    }
}
var UserController = new User();
//# sourceMappingURL=User.js.map