//routes.push({ pattern: /^module\/vendor$/, action: function (match: RegExpExecArray) { return VendorController.getModuleScreen(); } });
//---------------------------------------------------------------------------------
class Vendor {
    Module: string = 'Vendor';
    apiurl: string = 'api/v1/vendor';
    caption: string = Constants.Modules.Agent.children.Vendor.caption;
    nav: string = Constants.Modules.Agent.children.Vendor.nav;
    id: string = Constants.Modules.Agent.children.Vendor.id;
    //---------------------------------------------------------------------------------
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
    //---------------------------------------------------------------------------------
    openBrowse() {
        let $browse = jQuery(this.getBrowseTemplate());
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }
    //---------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        //FwTabs.hideTab($form.find('.purchaseordertab'));
        //FwTabs.hideTab($form.find('.vendorinvoicetab'));

        this.events($form);

        if (mode == 'NEW') {
            const officeLocation = JSON.parse(sessionStorage.getItem('location'));
            FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', officeLocation.locationid, officeLocation.location);
            FwFormField.setValueByDataField($form, 'DefaultSubRentDaysPerWeek', 0);
            FwFormField.setValueByDataField($form, 'DefaultSubRentDiscountPercent', 0);
            FwFormField.setValueByDataField($form, 'DefaultSubSaleDiscountPercent', 0);
            $form.find('div[data-datafield="Vendor"]').attr('data-required', 'true');
            FwFormField.setValue($form, 'div[data-datafield="DefaultCurrencyId"]', officeLocation.defaultcurrencyid, officeLocation.defaultcurrencycode);
        }

        let userassignedvendorno = JSON.parse(sessionStorage.getItem('controldefaults')).userassignedvendornumber;
        if (userassignedvendorno) {
            FwFormField.enable($form.find('[data-datafield="VendorNumber"]'));
            $form.find('[data-datafield="VendorNumber"]').attr(`data-required`, `true`);
        }
        else {
            FwFormField.disable($form.find('[data-datafield="VendorNumber"]'));
            $form.find('[data-datafield="VendorNumber"]').attr(`data-required`, `false`);
        }


        //Toggle Buttons - Vendor tab - Vendor Type Company or Person
        FwFormField.loadItems($form.find('div[data-datafield="VendorNameType"]'), [
            { value: 'COMPANY', caption: 'Company', checked: true },
            { value: 'PERSON', caption: 'Person' }
        ]);

        //Toggle Buttons - Deliver/Ship tab - Default Delivery Address
        FwFormField.loadItems($form.find('div[data-datafield="DefaultOutgoingDeliveryType"]'), [
            { value: 'DELIVER', caption: 'Vendor Deliver' },
            { value: 'SHIP', caption: 'Ship' },
            { value: 'PICK UP', caption: 'Pick Up' }
        ]);

        //Toggle Buttons - Deliver/Ship tab - Default Return Delivery Address
        FwFormField.loadItems($form.find('div[data-datafield="DefaultIncomingDeliveryType"]'), [
            { value: 'DELIVER', caption: 'Deliver' },
            { value: 'SHIP', caption: 'Ship' },
            { value: 'PICK UP', caption: 'Vendor Pick Up' }
        ]);

        return $form;
    }
    //---------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');
        FwFormField.setValueByDataField($form, 'VendorId', uniqueids.VendorId);

        FwModule.loadForm(this.Module, $form);

        // Sub-Modules
        const nodePurchaseOrder = FwApplicationTree.getNodeById(FwApplicationTree.tree, '9a0xOMvBM7Uh9');
        if (nodePurchaseOrder !== undefined && nodePurchaseOrder.properties.visible === 'T') {
            FwTabs.showTab($form.find('#purchaseordertab'));
            const $submodulePurchaseOrderBrowse = this.openPurchaseOrderBrowse($form);
            $form.find('.purchaseOrderSubModule').append($submodulePurchaseOrderBrowse);
        }

        const nodeVendorInvoice = FwApplicationTree.getNodeById(FwApplicationTree.tree, 'Fq9aOe0yWfY');
        if (nodeVendorInvoice !== undefined && nodeVendorInvoice.properties.visible === 'T') {
            FwTabs.showTab($form.find('#vendorinvoicetab'));
            const $submoduleVendorInvoiceBrowse = this.openVendorInvoiceBrowse($form);
            $form.find('.vendorInvoiceSubModule').append($submoduleVendorInvoiceBrowse);
        }

        const nodePayment = FwApplicationTree.getNodeById(FwApplicationTree.tree, 'Y7YC6NpLqX8kx');
        if (nodePayment !== undefined && nodePayment.properties.visible === 'T') {
            FwTabs.showTab($form.find('#paymenttab'));
            const $submodulepaymentBrowse = this.openPaymentBrowse($form);
            $form.find('.paymentSubModule').append($submodulepaymentBrowse);
        }

        return $form;
    }
    //---------------------------------------------------------------------------------
    openPurchaseOrderBrowse($form) {
        const vendorId = FwFormField.getValueByDataField($form, 'VendorId');
        const $browse = PurchaseOrderController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = PurchaseOrderController.ActiveViewFields;
            request.uniqueids = {
                VendorId: vendorId
            };
        });
        FwBrowse.search($browse);
        return $browse;
    }
    //---------------------------------------------------------------------------------------------
    openVendorInvoiceBrowse($form) {
        const vendorId = FwFormField.getValueByDataField($form, 'VendorId');
        const $browse = VendorInvoiceController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = VendorInvoiceController.ActiveViewFields;
            request.uniqueids = {
                VendorId: vendorId
            };
        });
        FwBrowse.search($browse);
        return $browse;
    }
    //---------------------------------------------------------------------------------------------
    openPaymentBrowse($form) {
        const vendorId = FwFormField.getValueByDataField($form, 'VendorId');
        const $browse = PaymentController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = PaymentController.ActiveViewFields;
            request.uniqueids = {
                VendorId: vendorId
            };
        });
        FwBrowse.search($browse);
        return $browse;
    }
    //---------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //---------------------------------------------------------------------------------
    afterLoad($form: any) {

        this.togglePanels($form, FwFormField.getValueByDataField($form, 'VendorNameType'));
        this.toggleRequiredFields($form);

        //Click Event on tabs to load grids/browses
        $form.find('.tabGridsLoaded[data-type="tab"]').removeClass('tabGridsLoaded');
        $form.on('click', '[data-type="tab"][data-enabled!="false"]', e => {
            const $tab = jQuery(e.currentTarget);
            const tabname = $tab.attr('id');
            const lastIndexOfTab = tabname.lastIndexOf('tab');  // for cases where "tab" is included in the name of the tab
            const tabpage = `${tabname.substring(0, lastIndexOfTab)}tabpage${tabname.substring(lastIndexOfTab + 3)}`;
            if ($tab.hasClass('audittab') == false) {
                const $gridControls = $form.find(`#${tabpage} [data-type="Grid"]`);
                if (($tab.hasClass('tabGridsLoaded') === false) && $gridControls.length > 0) {
                    for (let i = 0; i < $gridControls.length; i++) {
                        try {
                            const $gridcontrol = jQuery($gridControls[i]);
                            FwBrowse.search($gridcontrol);
                        } catch (ex) {
                            FwFunc.showError(ex);
                        }
                    }
                }

                const $browseControls = $form.find(`#${tabpage} [data-type="Browse"]`);
                if (($tab.hasClass('tabGridsLoaded') === false) && $browseControls.length > 0) {
                    for (let i = 0; i < $browseControls.length; i++) {
                        const $browseControl = jQuery($browseControls[i]);
                        FwBrowse.search($browseControl);
                    }
                }
            }
            $tab.addClass('tabGridsLoaded');
        });

        // Documents Grid - Need to put this here, because renderGrids is called from openForm and uniqueid is not available yet on the form
        // Moved documents grid from loadForm to afterLoad so it loads on new records. - Jason H 04/20/20
        const vendorId = FwFormField.getValueByDataField($form, 'VendorId');
        FwAppDocumentGrid.renderGrid({
            $form: $form,
            caption: 'Documents',
            nameGrid: 'VendorDocumentGrid',
            getBaseApiUrl: () => {
                return `${this.apiurl}/${vendorId}/document`;
            },
            gridSecurityId: 'LGV6fYIyFsgT',
            moduleSecurityId: this.id,
            parentFormDataFields: 'VendorId',
            uniqueid1Name: 'VendorId',
            getUniqueid1Value: () => vendorId,
            uniqueid2Name: '',
            getUniqueid2Value: () => ''
        });

        // Disable currency field if MultipleCurrencies is checked
        if (FwFormField.getValueByDataField($form, 'MultipleCurrencies') === true) {
            FwFormField.disableDataField($form, 'DefaultCurrencyId');
        }


    }
    //---------------------------------------------------------------------------------
    events($form: JQuery): void {
        $form.on('click', '.vendertyperadio input[type=radio]', (e) => {
            var $tab: JQuery = this.getTab(jQuery(e.currentTarget));
            var value: string = jQuery(e.currentTarget).val().toString();
            this.togglePanels($tab, value);
            this.toggleRequiredFields($tab);
        });
        //Record uses Multiple Currencies
        $form.find('div[data-datafield="MultipleCurrencies"]').on('change', () => {
            this.multipleCurrencies($form);
        });

        $form.find('[data-name="CompanyTaxOptionGrid"]').data('onselectedrowchanged', ($control: JQuery, $tr: JQuery) => {
            try {
                this.updateExternalInputsWithGridValues($tr);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        //$form.on('click', '#companytaxgrid .selected', (e) => {
        //    this.updateExternalInputsWithGridValues(e.currentTarget);
        //});

        //$form.on('click', '#vendornotegrid .selected', (e) => {
        //    this.updateExternalInputsWithGridValues(e.currentTarget);
        //});
    }
    //---------------------------------------------------------------------------------
    getTab($target: JQuery): JQuery {
        return $target.closest('.tabpage');
    }
    //---------------------------------------------------------------------------------
    togglePanels($tab: JQuery, type: string): void {
        $tab.find('.type_panels').hide();
        switch (type) {
            case 'COMPANY':
                $tab.find('#company_panel').show();
                break;
            case 'PERSON':
                $tab.find('#person_panel').show();
                break;
            default:
                throw new Error(`${type} is not a known type.`);
        }
    }
    //---------------------------------------------------------------------------------
    toggleRequiredFields($form: JQuery): void {
        var $person = $form.find('#person_panel'), $company = $form.find('#company_panel')
        let personRequired: string = $person.is(':hidden') ? 'false' : 'true';
        let companyRequired: string = $company.is(':hidden') ? 'false' : 'true';

        FwFormField.getDataField($form, 'FirstName').attr('data-required', personRequired);
        FwFormField.getDataField($form, 'LastName').attr('data-required', personRequired);
        FwFormField.getDataField($form, 'Vendor').attr('data-required', companyRequired);
    }
    //---------------------------------------------------------------------------------
    updateExternalInputsWithGridValues($tr: JQuery): void {
        let TaxOption = $tr.find('.field[data-browsedatafield="TaxOptionId"]').attr('data-originaltext');

        $tr.find('.column > .field').each((i, e) => {
            let $column = jQuery(e), id = $column.attr('data-browsedatafield'), value = $column.attr('data-originalvalue');
            if (value === undefined || null) {
                jQuery(`.${id}`).find(':input').val(0);
            } else {
                jQuery(`.${id}`).find(':input').val(value);
            }
        });
        jQuery('.TaxOption').find(':input').val(TaxOption);
    }
    //---------------------------------------------------------------------------------
    beforeValidate(datafield, request, $validationbrowse, $form, $tr) {
        switch (datafield) {
            case 'OfficeLocationId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateofficelocation`);
                break;
            case 'VendorClassId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatevendorclass`);
                break;
            case 'CustomerId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecustomer`);
                break;
            case 'CountryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecountry`);
                break;
            case 'DefaultRate':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validaterate`);
                break;
            case 'BillingCycleId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatebillingcycle`);
                break;
            case 'PaymentTermsId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatepaymentterms`);
                break;
            case 'OrganizationTypeId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateorganizationtype`);
                break;
            case 'DefaultPoClassificationId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatepoclass`);
                break;
            case 'DefaultCurrencyId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecurrency`);
                break;
            case 'RemitCountryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateremitcountry`);
                break;
        }
    }
    renderGrids($form: JQuery) {
        // ----------
        //Company Tax Option Grid
        //const $companyTaxOptionGrid = $form.find('div[data-grid="CompanyTaxOptionGrid"]');
        //const $companyTaxOptionControl = FwBrowse.loadGridFromTemplate('CompanyTaxOptionGrid');
        //$companyTaxOptionGrid.empty().append($companyTaxOptionControl);
        //$companyTaxOptionControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        CompanyId: FwFormField.getValueByDataField($form, 'VendorId')
        //    }
        //});
        //$companyTaxOptionControl.data('beforesave', function (request) {
        //    request.CompanyId = FwFormField.getValueByDataField($form, 'VendorId');
        //});
        //FwBrowse.init($companyTaxOptionControl);
        //FwBrowse.renderRuntimeHtml($companyTaxOptionControl);

        FwBrowse.renderGrid({
            nameGrid: 'CompanyTaxOptionGrid',
            gridSecurityId: 'B9CzDEmYe1Zf',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;

            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    CompanyId: FwFormField.getValueByDataField($form, 'VendorId')
                };
            },
            beforeSave: (request: any) => {
                request.CompanyId = FwFormField.getValueByDataField($form, 'VendorId');
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                this.renderWideGridColumns($form.find('[data-name="CompanyTaxOptionGrid"]'));
            }
        });
        // ----------
        //Vendor Note Grid
        //const $vendorNoteGrid = $form.find('div[data-grid="VendorNoteGrid"]');
        //const $vendorNoteControl = FwBrowse.loadGridFromTemplate('VendorNoteGrid');
        //$vendorNoteGrid.empty().append($vendorNoteControl);
        //$vendorNoteControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        VendorId: FwFormField.getValueByDataField($form, 'VendorId')
        //    }
        //});
        //FwBrowse.init($vendorNoteControl);
        //FwBrowse.renderRuntimeHtml($vendorNoteControl);

        FwBrowse.renderGrid({
            nameGrid: 'VendorNoteGrid',
            gridSecurityId: 'zuywROD73X60O',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {

                request.uniqueids = {
                    VendorId: FwFormField.getValueByDataField($form, 'VendorId')
                };
            },
            //beforeSave: (request: any) => {
            //    request.VendorId = FwFormField.getValueByDataField($form, 'VendorId');
            //}
        });
        // ----------
        //const $companyContactGrid = $form.find('div[data-grid="CompanyContactGrid"]');
        //const $companyContactControl = FwBrowse.loadGridFromTemplate('CompanyContactGrid');
        //$companyContactGrid.empty().append($companyContactControl);
        //$companyContactControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        CompanyId: FwFormField.getValueByDataField($form, 'VendorId')
        //    }
        //});
        //$companyContactControl.data('beforesave', function (request) {
        //    request.CompanyId = FwFormField.getValueByDataField($form, 'VendorId');
        //});
        //FwBrowse.init($companyContactControl);
        //FwBrowse.renderRuntimeHtml($companyContactControl);

        FwBrowse.renderGrid({
            nameGrid: 'CompanyContactGrid',
            gridSecurityId: 'gQHuhVDA5Do2',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    CompanyId: FwFormField.getValueByDataField($form, 'VendorId')
                };
            },
            beforeSave: (request: any) => {
                request.CompanyId = FwFormField.getValueByDataField($form, 'VendorId');
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    renderWideGridColumns($grid: JQuery) {
        if ($grid.find('thead tr').length < 2) {
            const $thead = $grid.find('thead tr').clone(false);
            $thead.find('[data-sort]').removeAttr('data-sort');
            $thead.find('td div.divselectrow').hide();
            $thead.find('td div.field:not([data-sharedcolumn])').hide();
            const $sharedColumnTds = $thead.find('td div[data-widerow]').parents('td');
            for (let i = 0; i < $sharedColumnTds.length; i++) {
                const $td = jQuery($sharedColumnTds[i]);
                const caption = $td.find('div[data-sharedcolumn]').attr('data-sharedcolumn');
                $td.find('.caption').text(caption);
            }
            $sharedColumnTds.css('text-align', 'center');
            $grid.find('thead').prepend($thead);
        }
    }
    //----------------------------------------------------------------------------------------------
    multipleCurrencies($form) {
        const multipleCurrencies = FwFormField.getValueByDataField($form, 'MultipleCurrencies');

        if (multipleCurrencies) {
            FwFormField.setValueByDataField($form, 'DefaultCurrencyId', '', '');
            FwFormField.disable($form.find('div[data-datafield="DefaultCurrencyId"]'));
            $form.find('div[data-datafield="DefaultCurrencyId"]').attr('data-required', 'false');
            $form.find('div[data-datafield="DefaultCurrencyId"]').removeClass('error');
        } else {
            $form.find('div[data-datafield="DefaultCurrencyId"]').attr('data-required', 'true');
            FwFormField.enable($form.find('div[data-datafield="DefaultCurrencyId"]'));
        }
    }
    //---------------------------------------------------------------------------------
    getBrowseTemplate(): string {
        return `
      <div data-name="Vendor" data-control="FwBrowse" data-type="Browse" id="VendorBrowse" class="fwcontrol fwbrowse" data-orderby="" data-controller="VendorController" data-hasinactive="true">
        <div class="column" data-width="0" data-visible="false">
          <div class="field" data-isuniqueid="true" data-datafield="VendorId" data-browsedatatype="key" ></div>
        </div>
        <div class="column" data-width="0" data-visible="false">
          <div class="field" data-datafield="Inactive" data-browsedatatype="text"  data-visible="false"></div>
        </div>
        <div class="column" data-width="300px" data-visible="true">
          <div class="field" data-caption="Vendor" data-datafield="VendorDisplayName" data-datatype="text" data-sort="asc"></div>
        </div>
        <div class="column" data-width="40px" data-visible="true">
          <div class="field" data-caption="Vendor Number" data-datafield="VendorNumber" data-datatype="text" data-sort="off"></div>
        </div>
        <div class="column" data-width="100px" data-visible="true">
          <div class="field" data-caption="Main Phone" data-datafield="Phone" data-datatype="text" data-sort="off"></div>
        </div>
        <div class="column" data-width="200px" data-visible="true">
          <div class="field" data-caption="Address" data-datafield="Address1" data-datatype="text" data-sort="off"></div>
        </div>
        <div class="column" data-width="150px" data-visible="true">
          <div class="field" data-caption="City" data-datafield="City" data-datatype="text" data-sort="off"></div>
        </div>
        <div class="column" data-width="50px" data-visible="true">
          <div class="field" data-caption="State" data-datafield="State" data-datatype="text" data-sort="off"></div>
        </div>
        <div class="column" data-width="100px" data-visible="true">
          <div class="field" data-caption="Zipcode" data-datafield="ZipCode" data-datatype="text" data-sort="off"></div>
        </div>
        <div class="column spacer" data-width="auto" data-visible="true"></div>
      </div>`;
    }
    //---------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
      <div id="vendorform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Vendor" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="VendorController">
        <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="VendorId"></div>
        <div id="vendorform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
          <div class="tabs">
            <div data-type="tab" id="vendortab" class="tab vendortab" data-tabpageid="vendortabpage" data-caption="Vendor"></div>
            <div data-type="tab" id="billingtab" class="tab billingtab" data-tabpageid="billingtabpage" data-caption="Billing"></div>
            <div data-type="tab" id="taxtab" class="tab taxtab" data-tabpageid="taxtabpage" data-caption="Tax"></div>
            <div data-type="tab" id="shippingtab" class="tab shippingtab" data-tabpageid="shippingtabpage" data-caption="Shipping"></div>
            <div data-type="tab" id="contactstab" class="tab contactstab" data-tabpageid="contactstabpage" data-caption="Contacts"></div>
            <div data-type="tab" id="purchaseordertab" class="tab submodule" data-tabpageid="purchaseordertabpage" data-caption="Purchase Order"></div>
            <div data-type="tab" id="vendorinvoicetab" class="tab submodule" data-tabpageid="vendorinvoicetabpage" data-caption="Vendor Invoice"></div>
            <div data-type="tab" id="paymenttab" class="tab submodule" data-tabpageid="paymenttabpage" data-caption="Payment"></div>
            <div data-type="tab" id="documentstab" class="tab documentstab" data-tabpageid="documentstabpage" data-caption="Documents"></div>
            <div data-type="tab" id="notestab" class="tab notestab" data-tabpageid="notestabpage" data-caption="Notes"></div>
          </div>
          <div class="tabpages">
            <div data-type="tabpage" id="vendortabpage" class="tabpage" data-tabid="vendortab">
              <div class="flexpage">
                <div class="flexrow">
                  <!-- Vendor section -->
                  <div class="flexcolumn" style="flex:1 1 300px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Vendor">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield vendertyperadio" data-caption="Type" data-datafield="VendorNameType" style="flex:1 1 150px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Vendor No" data-datafield="VendorNumber" data-required="true" style="flex:1 1 125px;"></div>
                      </div>
                      <div id="company_panel" class="type_panels" style="flex:1 1 500px;">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Name" data-datafield="Vendor" style="flex:1 1 475px;"></div>
                        </div>
                      </div>
                      <div id="person_panel" class="type_panels" style="display:none;flex:1 1 500px;">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Salutation"  data-datafield="Salutation"     data-required="false" style="flex:0 1 70px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="First Name"  data-datafield="FirstName"      data-required="false" style="flex:1 1 175px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="M.I."        data-datafield="MiddleInitial"  data-required="false" style="flex:0 1 35px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Last Name"   data-datafield="LastName"       data-required="false" style="flex:1 1 200px;"></div>
                        </div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield officelocation" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" style="flex:1 1 200px;" data-required="true"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation"  class="fwcontrol fwformfield vendorclass" data-caption="Class" data-datafield="VendorClassId" data-displayfield="VendorClass" data-validationname="VendorClassValidation" style="flex:1 1 200px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield customer" data-caption="Rental Customer" data-datafield="CustomerId" data-displayfield="Customer" data-validationname="CustomerValidation" style="flex:1 1 300px"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Federal ID No" data-datafield="FederalIdNumber" style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="url" class="fwcontrol fwformfield" data-caption="Web Address" data-datafield="WebAddress" data-allcaps="false" style="flex:1 1 275px;"></div>
                      </div>
                    </div>
                  </div>
                  <!-- Address section -->
                  <div class="flexcolumn" style="flex:1 1 275px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Address">
                     <div class="flexrow">
                       <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address" data-datafield="Address1" style="flex:1 1 275px;"></div>
                     </div>
                     <div class="flexrow">
                       <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 2" data-datafield="Address2" style="flex:1 1 250px;"></div>
                     </div>
                     <div class="flexrow">
                       <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="City" style="flex:1 1 275px;"></div>
                     </div>
                     <div class="flexrow">
                       <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State" data-datafield="State" style="flex:1 1 150px;"></div>
                       <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="ZipCode" style="flex:1 1 100px;"></div>
                     </div>
                     <div class="flexrow">
                       <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="CountryId" data-displayfield="Country" data-validationname="CountryValidation" style="flex:1 1 175px;"></div>
                     </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contact">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="phoneinternational" class="fwcontrol fwformfield" data-caption="Main" data-datafield="Phone" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="phoneinternational" class="fwcontrol fwformfield" data-caption="Fax" data-datafield="Fax" style="flex:1 1 125px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="phoneinternational" class="fwcontrol fwformfield" data-caption="Phone Toll-Free" data-datafield="PhoneTollFree" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="phoneinternational" class="fwcontrol fwformfield" data-caption="Other" data-datafield="OtherPhone" style="flex:1 1 125px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="email" class="fwcontrol fwformfield" data-caption="Email" data-datafield="Email" data-allcaps="false" style="flex:1 1 275px;"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="PO Defaults">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Sub-Rental D/W" data-datafield="DefaultSubRentDaysPerWeek" data-required="false" style="flex:1 1 75px;"></div>
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield" data-caption="Sub-Rental Discount %" data-datafield="DefaultSubRentDiscountPercent" data-required="false" style="flex:1 1 75px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield" data-caption="Sub-Sale Discount %" data-datafield="DefaultSubSaleDiscountPercent" data-required="false" style="flex:1 1 75px;"></div>
                      </div>
                    </div>
                  </div>
                  <!-- Status section -->
                  <div class="flexcolumn" style="flex:1 1 275px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Status">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Active Date" data-datafield="ActiveDate" data-enabled="false" data-readonly="true" style="flex:1 1 75px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Inactive" data-datafield="Inactive" style="flex:1 1 75px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Inactive Date" data-datafield="InactiveDate" data-enabled="false" data-readonly="true" style="flex:1 1 75px;"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Activity Type">
                      <div class="flexrow">
                        <div class="flexcolumn">
                          <div class="flexrow activity">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Rental Inventory" data-datafield="RentalInventory" style="flex:1 1 125px;"></div>
                          </div>
                          <div class="flexrow activity">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sales / Parts" data-datafield="SalesPartsInventory" style="flex:1 1 125px;"></div>
                          </div>
                          <div class="flexrow activity">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Repair" data-datafield="Repair" style="flex:1 1 125px;"></div>
                          </div>
                          <div class="flexrow activity">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Consignment" data-datafield="Consignment" style="flex:1 1 125px;"></div>
                          </div>
                          <div class="flexrow activity">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Manufacturer" data-datafield="Manufacturer" style="flex:1 1 125px;"></div>
                          </div>
                          <div class="flexrow activity">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Freight" data-datafield="Freight" style="flex:1 1 125px;"></div>
                          </div>
                        </div>
                        <div class="flexcolumn">
                          <div class="flexrow activity">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Rent" data-datafield="SubRent" style="flex:1 1 125px;"></div>
                          </div>
                          <div class="flexrow activity">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Sales" data-datafield="SubSales" style="flex:1 1 125px;"></div>
                          </div>
                          <div class="flexrow activity">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Misc" data-datafield="SubMisc" style="flex:1 1 125px;"></div>
                          </div>
                          <div class="flexrow activity">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Labor" data-datafield="SubLabor" style="flex:1 1 125px;"></div>
                          </div>
                          <div class="flexrow activity">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Vehicle" data-datafield="SubVehicle" style="flex:1 1 125px;"></div>
                          </div>
                          <div class="flexrow activity">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Insurance" data-datafield="Insurance" style="flex:1 1 125px;"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <!-- BILLING TAB -->
            <div data-type="tabpage" id="billingtabpage" class="tabpage" data-tabid="billingtab">
              <div class="flexpage">
                <div class="flexrow">
                  <!-- Customer section -->
                  <div class="flexcolumn" style="flex:0 1 325px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billing">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Account Number" data-datafield="AccountNumber" style="flex:1 1 275px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Default Rate" data-datafield="DefaultRate" data-displayfield="DefaultRate" data-validationname="RateTypeValidation" style="flex:1 1 275px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Sub-Rental Billing Cycle" data-datafield="BillingCycleId" data-displayfield="BillingCycle" data-validationname="BillingCycleValidation" style="flex:1 1 275px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Payment Terms" data-datafield="PaymentTermsId" data-displayfield="PaymentTerms" data-validationname="PaymentTermsValidation" style="flex:1 1 275px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Organization Type" data-datafield="OrganizationTypeId" data-displayfield="OrganizationType" data-validationname="OrganizationTypeValidation" style="flex:1 1 275px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Default PO Class" data-datafield="DefaultPoClassificationId" data-displayfield="DefaultPoClassification" data-validationname="POClassificationValidation" style="flex:1 1 275px;"></div>
                      </div>>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Currency" data-datafield="DefaultCurrencyId" data-displayfield="DefaultCurrencyCode" data-validationname="CurrencyCodeValidation" data-required="true" style="flex:1 1 275px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-datafield="MultipleCurrencies" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="This Vendor uses multiple Currencies. The Office Location default Currency will be used when creating new Purchase Orders." style="flex:1 1 200px;"></div>
                      </div>
                    </div>
                  </div>
       
                  <!-- Address section -->
                  <div class="flexcolumn" style="flex:0 1 325px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Remit To Address">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention 1" data-datafield="RemitAttention1" style="flex:1 1 275px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention 2" data-datafield="RemitAttention2" style="flex:1 1 275px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 1" data-datafield="RemitAddress1" style="flex:1 1 275px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 2" data-datafield="RemitAddress2" style="flex:1 1 275px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="RemitCity" style="flex:1 1 275px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State" data-datafield="RemitState" style="flex:1 1 150px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="RemitZipCode"  style="flex:1 1 150px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="RemitCountryId" data-displayfield="RemitCountry" data-validationname="CountryValidation" style="flex:1 1 150px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Payee No" data-datafield="RemitPayeeNo" style="flex:1 1 150px;"></div>
                      </div>
                    </div>
                  </div>

                  <!-- External ID section -->
                  <div class="flexcolumn" style="flex:0 1 325px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="External ID">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Vendor External ID" data-datafield="ExternalId" style="flex:1 1 275px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <!-- TAX TAB -->
            <div data-type="tabpage" id="taxtabpage" class="tabpage" data-tabid="taxtab">
              <div class="flexpage">
                <div class="flexrow">
                  <div id="companytaxoptiongrid" data-control="FwGrid" data-grid="CompanyTaxOptionGrid" data-securitycaption="Company Tax Grid"></div>
                </div>
              </div>
            </div>

            <!-- SHIPPING TAB -->
            <div data-type="tabpage" id="shippingtabpage" class="tabpage" data-tabid="shippingtab">
              <div class="flexpage">
                <div class="flexrow">
                  <!-- Default section -->
                  <div class="flexcolumn" style="flex:0 1 425px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Delivery">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="Type" data-datafield="DefaultOutgoingDeliveryType" style="flex:1 1 150px;"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Return Delivery">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="Type" data-datafield="DefaultIncomingDeliveryType" style="flex:1 1 150px;"></div>
                      </div>
                    </div>
                  </div>
                  <!-- Tracking No. section -->
                  <div class="flexcolumn" style="flex:0 1 425px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Tracking No">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Tracking No. Hyperlink" data-datafield="ShippingTrackingLink"></div>
                      </div>
                      <div class="flexrow">
                        <div style="padding-left:5px;font-size:14px;" >Use the token @trackingno to indicate where the tracking number needs to be injected.</div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>




<!--
                    <div class="flexrow">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Delivery">
                        <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="DefaultOutgoingDeliveryType" style="flex: 1 1 265px;">
                          <div data-value="DELIVER" data-caption="Vendor Deliver"></div>
                          <div data-value="SHIP" data-caption="Ship"></div>
                          <div data-value="PICK UP" data-caption="Pick Up"></div>
                        </div>
                      </div>
                    </div>
-->
<!--
                    <div class="flexrow">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Return Delivery" style="padding-left:1px;">
                        <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="DefaultIncomingDeliveryType" style="flex: 1 1 265px;">
                          <div data-value="DELIVER" data-caption="Deliver"></div>
                          <div data-value="SHIP" data-caption="Ship"></div>
                          <div data-value="PICK UP" data-caption="Vendor Pick Up"></div>
                        </div>
                      </div>
                    </div>
-->

            <!-- CONTACTS TAB -->
            <div data-type="tabpage" id="contactstabpage" class="tabpage" data-tabid="contactstab">
              <div class="flexpage">
                <div class="flexrow">
                  <div data-control="FwGrid" data-grid="CompanyContactGrid" data-securitycaption="Vendor Contacts"></div>
                </div>
              </div>
            </div>

            <!-- PURCHASE ORDER TAB -->
            <div data-type="tabpage" id="purchaseordertabpage" class="tabpage purchaseOrderSubModule rwSubModule" data-tabid="purchaseordertab">
            </div>

            <!-- VENDOR INVOICE TAB -->
            <div data-type="tabpage" id="vendorinvoicetabpage" class="tabpage vendorInvoiceSubModule rwSubModule" data-tabid="vendorinvoicetab">
            </div>

            <!-- PAYMENT TAB -->
            <div data-type="tabpage" id="paymenttabpage" class="tabpage paymentSubModule rwSubModule" data-tabid="paymenttab">
            </div>

            <!-- DOCUMENTS TAB -->
            <div data-type="tabpage" id="documentstabpage" class="tabpage" data-tabid="documentstab">
              <div class="flexpage">
                <div class="flexrow">
                  <div data-control="FwGrid" data-grid="VendorDocumentGrid"></div>
                </div>
              </div>
            </div>

            <!-- NOTES TAB -->
            <div data-type="tabpage" id="notestabpage" class="tabpage" data-tabid="notestab">
              <div class="flexpage">
                <div class="flexrow">
                  <div data-control="FwGrid" data-grid="VendorNoteGrid" data-securitycaption="Vendor Note" style="flex:1 1 1200px;"></div>
                </div>
                <!--<div class="flexrow">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Vendor Updated" style="flex:0 1 525px;">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield NoteDate" data-caption="Opened" data-enabled="false" data-datafield="InputDate" style="flex: 1 1 150px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield NotesBy" data-caption="By" data-enabled="false" data-datafield="ShippingTrackingLink" style="flex:1 1 350px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield Datestamp" data-caption="Modified Last" data-enabled="false" data-datafield="DateStamp" style="flex:1 1 150px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield NotesBy" data-caption="By" data-enabled="false" data-datafield="ShippingTrackingLink" style="flex:1 1 350px;"></div>
                    </div>
                  </div>
                </div>-->
              </div>
            </div>

          </div>
        </div>
      </div>`;
    }
    //---------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------
var VendorController = new Vendor();