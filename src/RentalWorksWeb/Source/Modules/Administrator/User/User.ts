class User {
    Module: string = 'User';
    apiurl: string = 'api/v1/user';
    caption: string = Constants.Modules.Administrator.children.User.caption;
    nav: string = Constants.Modules.Administrator.children.User.nav;
    id: string = Constants.Modules.Administrator.children.User.id;
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        FwMenu.addBrowseMenuButtons(options);

        const location = JSON.parse(sessionStorage.getItem('location'));
        const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);
        const $allLocations = FwMenu.generateDropDownViewBtn('ALL Offices', false, "ALL");

        if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
            this.ActiveViewFields.LocationId = [location.locationid];
        }
        const viewLocation = [];
        viewLocation.push($userLocation, $allLocations);
        FwMenu.addViewBtn(options.$menu, 'Location', viewLocation, true, "LocationId");
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
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
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = jQuery(this.getBrowseTemplate());
        $browse = FwModule.openBrowse($browse);

        $browse.data('ondatabind', request => {
            request.activeviewfields = this.ActiveViewFields;
        });

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        //let $form = FwModule.loadFormFromTemplate(this.Module);
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

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
        //    });
        // all related to settings tab
        const $browsedefaultrows = $form.find('div[data-datafield="BrowseDefaultRows"]');
        FwFormField.loadItems($browsedefaultrows, [
            { value: '5', text: '5' },
            { value: '10', text: '10' },
            { value: '15', text: '15' },
            { value: '20', text: '20' },
            { value: '25', text: '25' },
            { value: '30', text: '30' },
            { value: '35', text: '35' },
            { value: '40', text: '40' },
            { value: '45', text: '45' },
            { value: '50', text: '50' },
            { value: '100', text: '100' },
            { value: '200', text: '200' },
            { value: '500', text: '500' },
            { value: '1000', text: '1000' }
        ], true);
        // First Day of Week select
        const $firstDayofWeek = $form.find('div[data-datafield="FirstDayOfWeek"]');
        FwFormField.loadItems($firstDayofWeek, [
            { value: '0', text: 'Sunday' },
            { value: '1', text: 'Monday' },
            { value: '2', text: 'Tuesday' },
            { value: '3', text: 'Wednesday' },
            { value: '4', text: 'Thursday' },
            { value: '5', text: 'Friday' },
            { value: '6', text: 'Saturday' },
        ], true);

        const $applicationtheme = $form.find('div[data-datafield="ApplicationTheme"]');
        FwFormField.loadItems($applicationtheme, [
            { value: 'theme-material', text: 'Material' }
        ], true);

        // Load Default Home Page Options, Exclude Settings Modules.
        const defaultHomePages = FwApplicationTree.getAllModules(false, false, (modules: any[], moduleCaption: string, moduleName: string, category: string, currentNode: any, nodeModule: IGroupSecurityNode, hasView: boolean, hasNew: boolean, hasEdit: boolean, moduleController: any) => {
            const settingsString = 'settings';
            if (moduleController.hasOwnProperty('nav') && moduleController.nav.indexOf(settingsString) === -1) {
                modules.push({ value: moduleController.id, text: moduleCaption, nav: moduleController.nav });
            }
        });
        FwApplicationTree.sortModules(defaultHomePages);
        const $defaultHomePage = $form.find('div[data-datafield="HomeMenuGuid"]');
        FwFormField.loadItems($defaultHomePage, defaultHomePages, true);


        if (mode === 'NEW') {
            FwFormField.setValue2($browsedefaultrows, '25', '25');
            FwFormField.setValue2($defaultHomePage, 'UdmOOUGqu0lKd', 'Dashboard');
            const dataNav = $defaultHomePage.find(':selected').attr('data-nav');
            FwFormField.setValueByDataField($form, 'HomeMenuPath', dataNav);
        }

        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');
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
        const $discount = $form.find('div.fwformfield[data-datafield="LimitDiscount"] input').prop('checked');
        if ($discount === true) {
            FwFormField.enable($form.find('[data-datafield="DiscountRule"]'));
            FwFormField.enable($form.find('[data-datafield="MaximumDiscount"]'));
        } else {
            FwFormField.disable($form.find('[data-datafield="DiscountRule"]'));
            FwFormField.disable($form.find('[data-datafield="MaximumDiscount"]'));
        }

        const $subDiscount = $form.find('div.fwformfield[data-datafield="LimitSubDiscount"] input').prop('checked');
        if ($subDiscount === true) {
            FwFormField.enable($form.find('[data-datafield="DiscountRule"]'));
            FwFormField.enable($form.find('[data-datafield="MaximumSubDiscount"]'));
        } else {
            FwFormField.disable($form.find('[data-datafield="DiscountRule"]'));
            FwFormField.disable($form.find('[data-datafield="MaximumSubDiscount"]'));
        }

        const $passwordExpires = $form.find('div.fwformfield[data-datafield="PasswordExpires"] input').prop('checked');
        if ($passwordExpires === true) {
            FwFormField.enable($form.find('[data-datafield="PasswordExpireDays"]'));
        } else {
            FwFormField.disable($form.find('[data-datafield="PasswordExpireDays"]'));
        }

        $form.find('[data-datafield="OfficeLocationId"]').data('onchange', e => {
            $form.find('[data-datafield="WarehouseId"] input.fwformfield-value').val('');
            $form.find('[data-datafield="WarehouseId"] input.fwformfield-text').val('');
        });

        //FirstDayOfWeek set to sessionStorage
        const userid = JSON.parse(sessionStorage.getItem('userid'));
        if (FwFormField.getValueByDataField($form, 'UserId') === userid.usersid) {
            userid.firstdayofweek = +FwFormField.getValueByDataField($form, 'FirstDayOfWeek');
            sessionStorage.setItem('userid', JSON.stringify(userid));

            const homePage: any = {
                guid: FwFormField.getValueByDataField($form, 'HomeMenuGuid'),
                path: FwFormField.getValueByDataField($form, 'HomeMenuPath'),
            };
            sessionStorage.setItem('homePage', JSON.stringify(homePage));
            sessionStorage.setItem('browsedefaultrows', FwFormField.getValueByDataField($form, 'BrowseDefaultRows'));
            sessionStorage.setItem('applicationtheme', FwFormField.getValueByDataField($form, 'ApplicationTheme'));
        }

        SoundController.soundsToUrl($form);


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
    }
    //----------------------------------------------------------------------------------------------
    events($form: JQuery): void {
        $form.find('[data-datafield="LimitDiscount"] .fwformfield-value').on('change', e => {
            const $this = jQuery(e.currentTarget);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="DiscountRule"]'));
                FwFormField.enable($form.find('[data-datafield="MaximumDiscount"]'));
            }
            else {
                FwFormField.disable($form.find('[data-datafield="DiscountRule"]'));
                FwFormField.disable($form.find('[data-datafield="MaximumDiscount"]'));
            }
        });

        $form.find('[data-datafield="LimitSubDiscount"] .fwformfield-value').on('change', e => {
            const $this = jQuery(e.currentTarget);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="DiscountRule"]'));
                FwFormField.enable($form.find('[data-datafield="MaximumSubDiscount"]'));
            }
            else {
                FwFormField.disable($form.find('[data-datafield="DiscountRule"]'));
                FwFormField.disable($form.find('[data-datafield="MaximumSubDiscount"]'));
            }
        });

        $form.find('[data-datafield="PasswordExpires"] .fwformfield-value').on('change', e => {
            const $this = jQuery(e.currentTarget);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="PasswordExpireDays"]'));
            }
            else {
                FwFormField.disable($form.find('[data-datafield="PasswordExpireDays"]'));
            }
        });
        // Sound Validation
        $form.find('div.soundid').data('onchange', ($tr, $field) => {
            let tag;
            if ($field.attr('data-datafield') === 'SuccessSoundId') {
                tag = 'Success'
            } else if ($field.attr('data-datafield') === 'ErrorSoundId') {
                tag = 'Error';
            } else if ($field.attr('data-datafield') === 'NotificationSoundId') {
                tag = 'Notification';
            }
            // loads the selected Base64Sound as blob on the $form and url attribute to be streamed with the .play-btn evt
            FwFormField.setValue($form, `div[data-datafield="${tag}Base64Sound"]`, $tr.find(`.field[data-formdatafield="Base64Sound"]`).attr('data-originalvalue'));
            const blob = FwFunc.b64SoundtoBlob($tr.find(`.field[data-formdatafield="Base64Sound"]`).attr('data-originalvalue'));
            const blobUrl = URL.createObjectURL(blob);
            $form.find(`div[data-datafield="${tag}Base64Sound"]`).attr(`data-${tag}SoundUrl`, blobUrl);
        });

        // Sound Preview
        $form.find('.play-btn').on('click', e => {
            const $this = jQuery(e.currentTarget);
            let tag;
            if ($this.prev().attr('data-datafield') === 'SuccessSoundId') {
                tag = 'Success'
            } else if ($this.prev().attr('data-datafield') === 'ErrorSoundId') {
                tag = 'Error';
            } else if ($this.prev().attr('data-datafield') === 'NotificationSoundId') {
                tag = 'Notification';
            }
            const soundUrl = $form.find(`div[data-datafield="${tag}Base64Sound"]`).attr(`data-${tag}SoundUrl`);
            const sound = new Audio(soundUrl);
            sound.play();
        });

        $form.find('div[data-datafield="HomeMenuGuid"]').on("change", e => {
            const dataNav = jQuery(e.currentTarget).find(':selected').attr('data-nav');
            FwFormField.setValueByDataField($form, 'HomeMenuPath', dataNav);
        });
    }
    //----------------------------------------------------------------------------------------------
    beforeValidateWarehouse($browse: any, $form: any, request: any) {
        request.uniqueids = {};
        const locationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');

        if (locationId) {
            request.uniqueids.LocationId = locationId;
        }
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'GroupId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validategroup`);
                break;
            case 'UserTitleId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateusertitle`);
                break;
            case 'OfficeLocationId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateofficelocation`);
                break;
            case 'WarehouseId':
                const locationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');
                if (locationId) {
                    request.uniqueids.LocationId = locationId;
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatewarehouselocation`);
                break;
            case 'State':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatestate`);
                break;
            case 'CountryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecountry`);
                break;
            case 'RentalDepartmentId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validaterentaldepartment`);
                break;
            case 'SalesDepartmentId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatesalesdepartment`);
                break;
            case 'LaborDepartmentId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatelabordepartment`);
                break;
            case 'MiscDepartmentId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatemiscdepartment`);
                break;
            case 'PartsDepartmentId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatepartsdepartment`);
                break;
            case 'FacilityDepartmentId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatefacilitydepartment`);
                break;
            case 'TransportationDepartmentId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatetransportationdepartment`);
                break;
            case 'RentalInventoryTypeId':
                request.uniqueids = {
                    Rental: true
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validaterentalinventory`);
                break;
            case 'SalesInventoryTypeId':
                request.uniqueids = {
                    Sales: true
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatesalesinventorytype`);
                break;
            case 'PartsInventoryTypeId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatepartsinventorytype`);
                break;
            case 'TransportationTypeId':
                request.uniqueids = {
                    Transportation: true
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatetransportationtype`);
                break;
            case 'LaborTypeId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatelabortype`);
                break;
            case 'MiscTypeId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatemisctype`);
                break;
            case 'FacilityTypeId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatefacilitytype`);
                break;
            case 'SuccessSoundId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatesuccesssound`);
                break;
            case 'ErrorSoundId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateerrorsound`);
                break;
            case 'NotificationSoundId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatenotificationsound`);
        }
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
         </div>`;
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
              <div data-type="tab" id="inventorytypestab" class="tab" data-tabpageid="inventorytypestabpage" data-caption="Inventory Types"></div>
              <div data-type="tab" id="permissionstab" class="tab" data-tabpageid="permissionstabpage" data-caption="Permissions"></div>
              <div data-type="tab" id="settingstab" class="tab" data-tabpageid="settingstabpage" data-caption="Settings"></div>
              <div data-type="tab" id="notestab" class="tab" data-tabpageid="notestabpage" data-caption="Notes"></div>
              <div data-type="tab" id="emailsigtab" class="tab" data-tabpageid="emailsigtabpage" data-caption="Email Signature"></div>
              <div data-type="tab" id="picturetab" class="tab" data-tabpageid="picturetabpage" data-caption="Picture"></div>
              <div data-type="tab" id="rwnettab" class="tab" data-tabpageid="rwnettabpage" data-caption="Rw.NET"></div>
            </div>
            <div class="tabpages">
              <div data-type="tabpage" id="usertabpage" class="tabpage" data-tabid="usertab">
                <div class="flexpage">
                  <div class="flexrow" style="display:none;">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="PrimaryDepartment" data-datafield="PrimaryDepartment"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Name" data-datafield="Name"></div>
                  </div>
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:0 1 550px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="User">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="First Name" data-datafield="FirstName" data-required="true" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="MI" data-datafield="MiddleInitial" style="flex:1 1 25px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Last Name" data-datafield="LastName" data-required="true" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Inactive" data-datafield="Inactive" style="flex:1 1 50px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:0 1 500px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Login">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Username" data-required="true" data-datafield="LoginName" style="flex:1 1 125px;" data-allcaps="false"></div>
                          <div data-control="FwFormField" data-type="password" class="fwcontrol fwformfield" data-caption="Password" data-datafield="Password" data-required="true" style="flex:1 1 125px;" data-allcaps="false"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Barcode No" data-datafield="BarCode" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Group" data-datafield="GroupId" data-validationname="GroupValidation" data-displayfield="GroupName" style="flex:1 1 125px;" data-required="true"></div>
                          <div data-control="FwFormField" data-type="color" class="fwcontrol fwformfield" data-caption="Schedule Color" data-datafield="ScheduleColor" data-required="true" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Title" data-datafield="UserTitleId" data-displayfield="UserTitle" data-validationname="ContactTitleValidation" style="flex:1 1 225px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Email" data-datafield="Email" style="flex:1 1 225px;" data-allcaps="false"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:0 1 500px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Office / Warehouse">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" data-required="true" style="flex:1 1 225px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" data-required="true" style="flex:1 1 225px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!--CONTACT PAGE-->
              <div data-type="tabpage" id="contacttabpage" class="tabpage" data-tabid="contacttab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 600px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Address">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address" data-datafield="Address1" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="Address2" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="City" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="State" data-datafield="State" data-displayfield="State" data-validationname="StateValidation" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="zipcode" class="fwcontrol fwformfield" data-caption="Zip" data-datafield="ZipCode" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="CountryId" data-displayfield="Country" data-validationname="CountryValidation" style="flex:1 1 100px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 600px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Phone">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="phoneinternational" class="fwcontrol fwformfield" data-caption="Office" data-datafield="OfficePhone" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Ext" data-datafield="OfficeExtension" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="phoneinternational" class="fwcontrol fwformfield" data-caption="Direct" data-datafield="DirectPhone" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="phoneinternational" class="fwcontrol fwformfield" data-caption="Home" data-datafield="HomePhone" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="phoneinternational" class="fwcontrol fwformfield" data-caption="Mobile" data-datafield="Cellular" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="phoneinternational" class="fwcontrol fwformfield" data-caption="Pager No." data-datafield="Pager" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="phoneinternational" class="fwcontrol fwformfield" data-caption="Pin No." data-datafield="PagerPin" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="phoneinternational" class="fwcontrol fwformfield" data-caption="Fax" data-datafield="Fax" style="flex:1 1 125px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!--SECURITY PAGE-->
              <div data-type="tabpage" id="securitytabpage" class="tabpage" data-tabid="securitytab">
                <div class="flexrow">
                  <div class="flexcolumn" style="flex:0 1 650px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Lock Login">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Login is locked out" data-datafield="AccountLocked" style="flex:1 1 125px;"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Password Security">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="User must change password on next login" data-datafield="UserMustChangePassword" style="flex:1 1 125px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield cbSecurityExpirePassword" data-caption="Expire password" data-datafield="PasswordExpires" style="flex:1 1 125px;"></div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield txtSecurityExpire" data-caption="Expire (days)" data-datafield="PasswordExpireDays" data-enabled="false" style="flex:0 1 125px;"></div>
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Last Changed" data-datafield="PasswordUpdatedDateTime" data-enabled="false" style="flex:0 1 125px;"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Administrator">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Administrator" data-datafield="WebAdministrator" style="float:left;width:150px;"></div>
                      </div>
                      <div class="flexrow">
                        <div>Administrators are notified when System Updates are available for install.</div>
                      </div>
                      <div class="flexrow">
                        <div>Administrators have access to all Settings peek buttons.</div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!--DEPARTMENT PAGE-->
              <div data-type="tabpage" id="departmenttabpage" class="tabpage" data-tabid="departmenttab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 125px;">
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
                    <div class="flexcolumn" style="flex:1 1 240px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Departments">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Rental" data-datafield="RentalDepartmentId" data-displayfield="RentalDepartment" data-validationname="DepartmentValidation" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Sales" data-datafield="SalesDepartmentId" data-displayfield="SalesDepartment" data-validationname="DepartmentValidation" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Labor" data-datafield="LaborDepartmentId" data-displayfield="LaborDepartment" data-validationname="DepartmentValidation" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Misc" data-datafield="MiscDepartmentId" data-displayfield="MiscDepartment" data-validationname="DepartmentValidation" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Parts" data-datafield="PartsDepartmentId" data-displayfield="PartsDepartment" data-validationname="DepartmentValidation" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Facilities" data-datafield="FacilityDepartmentId" data-displayfield="FacilityDepartment" data-validationname="DepartmentValidation" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Transportation" data-datafield="TransportationDepartmentId" data-displayfield="TransportationDepartment" data-validationname="DepartmentValidation" style="flex:1 1 125px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
             <!--INVENTORY TYPES PAGE-->
              <div data-type="tabpage" id="inventorytypestabpage" class="tabpage" data-tabid="inventorytypestab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 185px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Inventory Types">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Rental" data-datafield="RentalInventoryTypeId" data-displayfield="RentalInventoryType" data-validationname="InventoryTypeValidation" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Sales" data-datafield="SalesInventoryTypeId" data-displayfield="SalesInventoryType" data-validationname="InventoryTypeValidation" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Parts" data-datafield="PartsInventoryTypeId" data-displayfield="PartsInventoryType" data-validationname="InventoryTypeValidation" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Transportation" data-datafield="TransportationTypeId" data-displayfield="TransportationType" data-validationname="InventoryTypeValidation" style="flex:1 1 125px;"></div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 240px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Rate Types">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Labor" data-datafield="LaborTypeId" data-displayfield="LaborType" data-validationname="LaborTypeValidation" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Misc" data-datafield="MiscTypeId" data-displayfield="MiscType" data-validationname="MiscTypeValidation" style="flex:1 1 125px;"></div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 175px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Facilities Types">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Facilities" data-datafield="FacilityTypeId" data-displayfield="FacilityType" data-validationname="FacilityTypeValidation" style="flex:0 1 125px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!--PERMISSIONS PAGE-->
              <div data-type="tabpage" id="permissionstabpage" class="tabpage" data-tabid="permissionstab">
                <div class="flexrow">
                  <div class="flexcolumn" style="flex:1 1 350px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quotes/Order">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Do not allow Misc. I-Codes on">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Quote" data-datafield="NoMiscellaneousOnQuotes" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Order" data-datafield="NoMiscellaneousOnOrders" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Purchase Order" data-datafield="NoMiscellaneousOnPurchaseOrders" style="flex:1 1 125px;"></div>
                      </div>
                      <div class="flexrow">
                        <div class="flexcolumn" style="flex:1 1 350px;">
                          <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Limit D/W">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Limit D/W" data-datafield="LimitDaysPerWeek" style="flex:1 1 125px;"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Allow User to enter D/W from:" data-datafield="MinimumDaysPerWeek" style="flex:1 1 125px;"></div>
                          </div>
                        </div>
                      </div>
                      <div class="flexrow">
                        <div class="flexcolumn" style="flex:1 1 350px;">
                          <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Customer/Deal Credit Limit">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Credit Limit Override" data-datafield="AllowCreditLimitOverride" style="flex:1 1 125px;"></div>
                          </div>
                        </div>
                      </div>
                      <div class="flexcolumn" style="flex:1 1 350px;">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Limit Discounting">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Limit Discount Percent" data-datafield="LimitDiscount" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield" data-caption="Allow User to enter Discount Percent up to:" data-datafield="MaximumDiscount" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Limit Sub Discount Percent" data-datafield="LimitSubDiscount" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield" data-caption="Allow User to enter Discount Percent up to:" data-datafield="MaximumSubDiscount" style="flex:1 1 125px;"></div>
                        </div>
                      </div>
                      <div class="flexcolumn" style="flex:1 1 350px;">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Discount Rule">
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="DiscountRule" style="flex:1 1 125px;">
                            <div data-value="DISALLOW" data-caption="Prevent discounts larger than this amount"></div>
                            <div data-value="ALLOW" data-caption="Allow larger discounts, but prevent printing"></div>
                          </div>
                        </div>
                      </div>
                      <div class="flexcolumn" style="flex:1 1 350px;">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Multi-Locations">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Cross Location Edit and Delete" data-datafield="AllowCrossLocationEditAndDelete" style="flex:1 1 125px;">
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:0 1 515px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Staging / Check-Out">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow user to increase/decrease quantity or add to Order at Staging" data-datafield="StagingAllowIncreaseDecreaseOrderQuantity" style="flex:1 1 505px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow user to substitute items on Order at Staging" data-datafield="AllowSubstitutesAtStaging" style="flex:1 1 505px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Delete the original item from the Order when Substitute is Staged" data-datafield="DeleteOriginalOnSubstitution" style="flex:1 1 505px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Staging of Items when Reserved on other Orders/Quotes" data-datafield="AllowStagingOfItemsWhenReservedOnOtherOrdersQuotes" style="flex:1 1 505px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Contract if Deal requires a PO and Order has a Pending PO" data-datafield="AllowContractIfDealRequiresPOAndOrderHasPendingPO" style="flex:1 1 505px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Contract if Pending Items exist" data-datafield="AllowContractIfPendingItemsExist" style="flex:1 1 505px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Contract if Customer/Deal does not have Approved Credit" data-datafield="AllowContractIfCustomerDealDoesNotHaveApprovedCredit" style="flex:1 1 505px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Contract if Customer/Deal is over their Credit Limit" data-datafield="AllowContractIfCustomerDealIsOverTheirCreditLimit" style="flex:1 1 505px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Contract if Customer/Deal does not have valid Insurance Certificate" data-datafield="AllowContractIfCustomerDealDoesNotHaveValidInsuranceCertificate" style="flex:1 1 505px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Contract if Customer/Deal does not have valid Non-Tax Certificate" data-datafield="AllowContractIfCustomerDealDoesNotHaveValidNonTaxCertificate" style="flex:1 1 505px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow user to Receive Subs when Positive Conflict exists" data-datafield="AllowReceiveSubsWhenPositiveConflictExists" style="flex:1 1 505px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Staging of Unreserved Consigned Items" data-datafield="AllowStagingOfUnreservedConsignedItems" style="flex:1 1 505px;"></div>
                    </div>
                    <div class="flexcolumn">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="RentalWorks Mobile/QuikScan">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow user to create Contracts" data-datafield="QuikScanAllowCreateContract" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption='Allow "Apply All Quantity Items" button' data-datafield="QuikScanAllowApplyAll" style="flex:1 1 125px;"></div>
                      </div>
                    </div>
                    <div class="flexcolumn">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Exchange">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Cross I-Code Exchange" data-datafield="AllowCrossICodeExchange" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Cross I-Code Pending Exchange" data-datafield="AllowCrossICodePendingExchange" style="flex:1 1 125px;"></div>
                      </div>
                    </div>
                    <div class="flexcolumn">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Availability">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow user to change Availability Priority" data-datafield="AllowChangeAvailabilityPriority" style="flex:1 1 125px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!--SETTINGS PAGE-->
              <div data-type="tabpage" id="settingstabpage" class="tabpage" data-tabid="settingstab">
                <div class="flexrow">
                  <div class="flexcolumn" style="max-width:300px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="User Settings">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield" data-caption="Default Rows per Page (Browse)" data-datafield="BrowseDefaultRows" style="float:left;max-width:250px;">
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield" data-caption="Theme" data-datafield="ApplicationTheme" style="float:left;max-width:250px;"></div>
                      </div>
                      <div class="flexrow" style="width:243px;">
                        <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield" data-caption="Default Home Page" data-datafield="HomeMenuGuid" style="flex:1 1 350px;"></div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield soundid" data-caption="Success Sound" data-datafield="SuccessSoundId" data-displayfield="SuccessSound" data-validationname="SoundValidation" style="flex:1 1 225px;"></div>
                          <button type="submit" class="play-btn"><img src="theme/images/icons/settings/play_button.svg" alt="Play" /></button>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield soundid" data-caption="Error Sound" data-datafield="ErrorSoundId" data-displayfield="ErrorSound" data-validationname="SoundValidation" style="flex:1 1 225px;"></div>
                          <button type="submit" class="play-btn"><img src="theme/images/icons/settings/play_button.svg" /></button>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield soundid" data-caption="Notification Sound" data-datafield="NotificationSoundId" data-displayfield="NotificationSound" data-validationname="SoundValidation" style="flex:1 1 225px;"></div>
                          <button type="submit" class="play-btn"><img src="theme/images/icons/settings/play_button.svg" alt="Play" /></button>
                        </div>
                      </div>
                      <div class="flexrow" style="width:243px;">
                        <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield" data-caption="Calendar Start Day" data-datafield="FirstDayOfWeek" style="flex:1 1 350px;"></div>
                      </div>
                      <!--Hidden Sound fields-->
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Home Page Path" data-datafield="HomeMenuPath" style="flex:1 1 0; display:none;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="SuccessBase64Sound" data-SuccessSoundUrl="" data-datafield="SuccessBase64Sound" data-allcaps="false" style="float:left;width:455px;display:none;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="ErrorBase64Sound" data-ErrorSoundUrl="" data-datafield="ErrorBase64Sound" data-allcaps="false" style="float:left;width:455px;display:none;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="NotificationBase64Sound" data-NotificationSoundUrl="" data-datafield="NotificationBase64Sound" data-allcaps="false" style="float:left;width:455px;display:none;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!--NOTES PAGE-->
              <div data-type="tabpage" id="notestabpage" class="tabpage" data-tabid="notestab">
                <div class="flexrow">
                  <div class="flexcolumn" style="flex: 0 1 800px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Note">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Notes" data-datafield="Memo" data-allcaps="false"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!--EMAIL SIGNATURE PAGE-->
             <div data-type="tabpage" id="emailsigtabpage" class="tabpage" data-tabid="emailsigtab">
                <div class="flexrow">
                  <div data-control="FwFormField" data-type="editor" class="fwcontrol fwformfield" data-caption="Email Signature" data-datafield="EmailSignature" style="width:900px;"></div>
                </div>
              </div>
              <!--PICTURE TAB-->
              <div data-type="tabpage" id="picturetabpage" class="tabpage" data-tabid="picturetab">
                <div class="flexrow">
                  <div class="flexcolumn" style="flex: 0 1 800px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="User Photo">
                      <div class="fwcontrol fwappimage" data-control="FwAppImage" data-type="" data-uniqueid1field="UserId" data-description="" data-rectype=""></div>
                    </div>
                  </div>
                </div>
              </div>
              <!--RW.NET TAB-->
              <div data-type="tabpage" id="rwnettabpage" class="tabpage" data-tabid="rwnettab">
                <div class="flexrow">
                  <div class="flexcolumn" style="flex: 0 1 800px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Rw.NET Settings">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Enable Rw.NET Access" data-datafield="WebAccess"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Enable Quote Request" data-datafield="WebQuoteRequest"></div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>`;
    }
}
//----------------------------------------------------------------------------------------------
var UserController = new User();