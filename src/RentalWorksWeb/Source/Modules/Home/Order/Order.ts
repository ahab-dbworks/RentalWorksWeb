//routes.push({ pattern: /^module\/order$/, action: function (match: RegExpExecArray) { return OrderController.getModuleScreen(); } });
//routes.push({ pattern: /^module\/order\/(\w+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { datafield: match[1], search: match[2] }; return OrderController.getModuleScreen(filter); } });

//----------------------------------------------------------------------------------------------
class Order extends OrderBase {
    Module = 'Order';
    apiurl: string = 'api/v1/order';
    caption: string = 'Order';
    nav: string = 'module/order';
    id: string = '64C46F51-5E00-48FA-94B6-FC4EF53FEA20';
    lossDamageSessionId: string = '';
    successSoundFileName: string;
    errorSoundFileName: string;
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    //-----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: any) {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};
        const $browse = this.openBrowse();
        screen.load = function () {
            FwModule.openModuleTab($browse, 'Order', false, 'BROWSE', true);

            if (typeof filter !== 'undefined' && filter.datafield === 'agent') {
                filter.search = filter.search.split('%20').reverse().join(', ');
            }

            if (typeof filter !== 'undefined') {
                filter.datafield = filter.datafield.charAt(0).toUpperCase() + filter.datafield.slice(1);
                $browse.find(`div[data-browsedatafield="${filter.datafield}"]`).find('input').val(filter.search);
            }

            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };
        return screen;
    };

    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = jQuery(this.getBrowseTemplate());
        $browse = FwModule.openBrowse($browse);

        FwBrowse.setAfterRenderRowCallback($browse, function ($tr, dt, rowIndex) {
            if (dt.Rows[rowIndex][dt.ColumnIndex['Status']] === 'CANCELLED') {
                $tr.css('color', '#aaaaaa');
            }
        });

        $browse.data('ondatabind', request =>  {
            request.activeviewfields = this.ActiveViewFields;
        });

        try {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, function onSuccess(response) {
                for (var key in response) {
                    FwBrowse.addLegend($browse, key, response[key]);
                }
            }, function onError(response) {
                FwFunc.showError(response);
            }, $browse)
        } catch (ex) {
            FwFunc.showError(ex);
        }

        const department = JSON.parse(sessionStorage.getItem('department'));;
        const location = JSON.parse(sessionStorage.getItem('location'));;

        FwAppData.apiMethod(true, 'GET', `api/v1/departmentlocation/${department.departmentid}~${location.locationid}`, null, FwServices.defaultTimeout, response => {
            this.DefaultOrderType = response.DefaultOrderType;
            this.DefaultOrderTypeId = response.DefaultOrderTypeId;
        }, null, null);

        return $browse;
    };

    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems($menuObject) {
        const $all = FwMenu.generateDropDownViewBtn('All', true, "ALL");
        const $confirmed = FwMenu.generateDropDownViewBtn('Confirmed', false, "CONFIRMED");
        const $active = FwMenu.generateDropDownViewBtn('Active', false, "ACTIVE");
        const $hold = FwMenu.generateDropDownViewBtn('Hold', false, "HOLD");
        const $complete = FwMenu.generateDropDownViewBtn('Complete', false, "COMPLETE");
        const $cancelled = FwMenu.generateDropDownViewBtn('Cancelled', false, "CANCELLED");
        const $closed = FwMenu.generateDropDownViewBtn('Closed', false, "CLOSED");
      
        let viewSubitems: Array<JQuery> = [];
        viewSubitems.push($all, $confirmed, $active, $hold, $complete, $cancelled, $closed);
        FwMenu.addViewBtn($menuObject, 'View', viewSubitems, true, "Status");

        //Location Filter
        const location = JSON.parse(sessionStorage.getItem('location'));
        const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
        const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);

        if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
            this.ActiveViewFields.LocationId = [location.locationid];
        }

        let viewLocation: Array<JQuery> = [];
        viewLocation.push($userLocation, $allLocations);
        FwMenu.addViewBtn($menuObject, 'Location', viewLocation, true, "LocationId");
        return $menuObject;
    };

    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentModuleInfo?: any) {
        let $form = super.openForm(mode, parentModuleInfo);

        if (mode === 'NEW') {
            $form.find('[data-type="tab"][data-caption="Loss and Damage"]').hide();
            FwFormField.disable($form.find('[data-datafield="LossAndDamage"]'));
        };

        const $submodulePickListBrowse = this.openPickListBrowse($form);
        $form.find('.picklist').append($submodulePickListBrowse);

        const $submoduleContractBrowse = this.openContractBrowse($form);
        $form.find('.contract').append($submoduleContractBrowse);

        const $orderItemGridLossDamage = $form.find('.lossdamagegrid [data-name="OrderItemGrid"]');

        // Hides Add, Search, and Sub-Worksheet buttons on grid
        $orderItemGridLossDamage.find('.submenu-btn').filter('[data-securityid="77E511EC-5463-43A0-9C5D-B54407C97B15"], [data-securityid="007C4F21-7526-437C-AD1C-4BBB1030AABA"]').hide();
        $orderItemGridLossDamage.find('.buttonbar').hide();

        this.getSoundUrls($form);

        return $form;
    };

    //----------------------------------------------------------------------------------------------
    openPickListBrowse($form) {
        const $browse = PickListController.openBrowse();

        $browse.data('ondatabind', function (request) {
            request.activeviewfields = PickListController.ActiveViewFields;
            request.uniqueids = {
                OrderId: $form.find('[data-datafield="OrderId"] input.fwformfield-value').val()
            }
        });

        return $browse;
    };

    //----------------------------------------------------------------------------------------------
    openContractBrowse($form) {
        const $browse = ContractController.openBrowse();

        $browse.data('ondatabind', function (request) {
            request.activeviewfields = ContractController.ActiveViewFields;
            request.uniqueids = {
                OrderId: $form.find('[data-datafield="OrderId"] input.fwformfield-value').val()
            }
        });

        return $browse;
    };

    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids) {
        const $form = this.openForm('EDIT', uniqueids);
        $form.find('div.fwformfield[data-datafield="OrderId"] input').val(uniqueids.OrderId);
        FwModule.loadForm(this.Module, $form);

        let $submodulePurchaseOrderBrowse = this.openPurchaseOrderBrowse($form);
        $form.find('.subPurchaseOrderSubModule').append($submodulePurchaseOrderBrowse);
        let $submoduleInvoiceBrowse = this.openInvoiceBrowse($form);
        $form.find('.invoiceSubModule').append($submoduleInvoiceBrowse);

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    openPurchaseOrderBrowse($form) {
        const $browse = PurchaseOrderController.openBrowse();
        const orderId = FwFormField.getValueByDataField($form, 'OrderId');
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = PurchaseOrderController.ActiveViewFields;
            request.uniqueids = {
                OrderId: orderId
            };
        });
        return $browse;
    }
   //---------------------------------------------------------------------------------------------
    openInvoiceBrowse($form) {
        const $browse = InvoiceController.openBrowse();
        const orderId = FwFormField.getValueByDataField($form, 'OrderId');
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = InvoiceController.ActiveViewFields;
            request.uniqueids = {
                OrderId: orderId
            };
        });
        return $browse;
    }
   //---------------------------------------------------------------------------------------------
    renderGrids($form) {
        super.renderGrids($form);
        // ----------
        const $orderPickListGrid = $form.find('div[data-grid="OrderPickListGrid"]');
        const $orderPickListGridControl = FwBrowse.loadGridFromTemplate('OrderPickListGrid');
        $orderPickListGrid.empty().append($orderPickListGridControl);
        $orderPickListGridControl.data('ondatabind', request => { 
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId')
            };
        });
        FwBrowse.init($orderPickListGridControl);
        FwBrowse.renderRuntimeHtml($orderPickListGridControl);
        // ----------
        const $orderSnapshotGrid = $form.find('div[data-grid="OrderSnapshotGrid"]');
        const $orderSnapshotGridControl = FwBrowse.loadGridFromTemplate('OrderSnapshotGrid');
        $orderSnapshotGrid.empty().append($orderSnapshotGridControl);
        $orderSnapshotGridControl.data('ondatabind', request => { 
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId')
            };
        });
        FwBrowse.init($orderSnapshotGridControl);
        FwBrowse.renderRuntimeHtml($orderSnapshotGridControl);
        // ----------
        const $orderItemGridLossDamage = $form.find('.lossdamagegrid div[data-grid="OrderItemGrid"]');
        const $orderItemGridLossDamageControl = FwBrowse.loadGridFromTemplate('OrderItemGrid');
        $orderItemGridLossDamage.empty().append($orderItemGridLossDamageControl);
        $orderItemGridLossDamageControl.data('isSummary', false);
        $orderItemGridLossDamage.addClass('F');
        $orderItemGridLossDamage.find('div[data-datafield="InventoryId"]').attr('data-formreadonly', 'true'); 
        $orderItemGridLossDamage.find('div[data-datafield="Description"]').attr('data-formreadonly', 'true');
        $orderItemGridLossDamage.find('div[data-datafield="ItemId"]').attr('data-formreadonly', 'true');
        $orderItemGridLossDamage.find('div[data-datafield="Price"]').attr('data-digits', '3'); 
        $orderItemGridLossDamage.find('div[data-datafield="Price"]').attr('data-digitsoptional', 'false'); 

        $orderItemGridLossDamageControl.data('ondatabind', request => { 
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                RecType: 'F'
            };
            request.totalfields = this.totalFields;
        });
        $orderItemGridLossDamageControl.data('beforesave', request => { 
            request.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
            request.RecType = 'F';
        }
        );
        FwBrowse.addEventHandler($orderItemGridLossDamageControl, 'afterdatabindcallback', ($control, dt) => {
            this.calculateOrderItemGridTotals($form, 'lossdamage', dt.Totals);

            let lossDamageItems = $form.find('.lossdamagegrid tbody').children();
            lossDamageItems.length > 0 ? FwFormField.disable($form.find('[data-datafield="LossAndDamage"]')) : FwFormField.enable($form.find('[data-datafield="LossAndDamage"]'));
        });

        FwBrowse.init($orderItemGridLossDamageControl);
        FwBrowse.renderRuntimeHtml($orderItemGridLossDamageControl);


    };

    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
        super.afterLoad($form);
        let lossDamageTab = $form.find('[data-type="tab"][data-caption="Loss and Damage"]');

        if ($form.find('[data-datafield="CombineActivity"] input').val() === 'false') {
            // show / hide tabs
            if (!FwFormField.getValueByDataField($form, 'LossAndDamage')) { lossDamageTab.hide(), FwFormField.disable($form.find('[data-datafield="Rental"]')); }
        }

        if (FwFormField.getValueByDataField($form, 'HasLossAndDamageItem')) {
            FwFormField.disable(FwFormField.getDataField($form, 'LossAndDamage'));
        }
        if (!FwFormField.getValueByDataField($form, 'LossAndDamage')) { $form.find('[data-type="tab"][data-caption="Loss And Damage"]').hide() }
    };
    //----------------------------------------------------------------------------------------------
    getBrowseTemplate(): string {
        return `
        <div data-name="Order" data-control="FwBrowse" data-type="Browse" id="OrderBrowse" class="fwcontrol fwbrowse" data-orderby="OrderNumber" data-controller="OrderController" data-hasinactive="false">
          <div class="column" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="true" data-datafield="OrderId" data-browsedatatype="key"></div>
          </div>
          <div class="column" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="true" data-datafield="OrderTypeId" data-browsedatatype="key"></div>
          </div>
          <div class="column" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="false" data-datafield="OfficeLocationId" data-browsedatatype="key"></div>
            <div class="field" data-isuniqueid="false" data-datafield="WarehouseId" data-browsedatatype="key"></div>
            <div class="field" data-isuniqueid="false" data-datafield="DepartmentId" data-browsedatatype="key"></div>
            <div class="field" data-isuniqueid="false" data-datafield="CustomerId" data-browsedatatype="key"></div>
            <div class="field" data-isuniqueid="false" data-datafield="DealId" data-browsedatatype="key"></div>
          </div>
          <div class="column" data-width="100px" data-visible="true">
            <div class="field" data-caption="Order No." data-datafield="OrderNumber" data-cellcolor="OrderNumberColor" data-browsedatatype="text" data-sort="desc" data-sortsequence="2" data-searchfieldoperators="startswith"></div>
          </div>
          <div class="column" data-width="100px" data-visible="true">
            <div class="field" data-caption="Date" data-datafield="OrderDate" data-browsedatatype="date" data-sortsequence="1" data-sort="desc"></div>
          </div>
          <div class="column" data-width="350px" data-visible="true">
            <div class="field" data-caption="Description" data-datafield="Description" data-cellcolor="DescriptionColor" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="250px" data-visible="true">
            <div class="field" data-caption="Deal" data-datafield="Deal" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="100px" data-visible="true">
            <div class="field" data-caption="Deal No." data-datafield="DealNumber" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="0" data-visible="true">
            <div class="field" data-caption="Status" data-datafield="Status" data-cellcolor="StatusColor" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="100px" data-visible="true">
            <div class="field" data-caption="Status As Of" data-datafield="StatusDate" data-browsedatatype="date" data-sort="off"></div>
          </div>
          <div class="column" data-width="100px" data-visible="true">
            <div class="field" data-caption="PO No." data-datafield="PoNumber" data-cellcolor="PoNumberColor" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="100px" data-visible="true">
            <div class="field" data-caption="Total" data-datafield="Total" data-cellcolor="CurrencyColor" data-browsedatatype="number" data-digits="2" data-formatnumeric="true" data-sort="off"></div>
          </div>
          <div class="column" data-width="180px" data-visible="true">
            <div class="field" data-caption="Agent" data-datafield="Agent" data-multiwordseparator="|" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="50px" data-visible="true">
            <div class="field" data-caption="Warehouse" data-datafield="WarehouseCode" data-cellcolor="WarehouseColor" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column spacer" data-width="auto" data-visible="true"></div>
        </div>`;
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
      return `
      <div id="orderform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Order" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="OrderController">
        <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield OrderId" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="OrderId"></div>
        <div id="orderform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
          <div class="tabs">
            <div data-type="tab" id="generaltab" class="generaltab tab" data-tabpageid="generaltabpage" data-caption="Order"></div>
            <div data-type="tab" id="rentaltab" class="notcombinedtab tab" data-tabpageid="rentaltabpage" data-caption="Rental"></div>
            <div data-type="tab" id="salestab" class="notcombinedtab tab" data-tabpageid="salestabpage" data-caption="Sales"></div>
            <div data-type="tab" id="misctab" class="notcombinedtab tab" data-tabpageid="misctabpage" data-caption="Miscellaneous"></div>
            <div data-type="tab" id="labortab" class="notcombinedtab tab" data-tabpageid="labortabpage" data-caption="Labor"></div>
            <div data-type="tab" id="usedsaletab" class="notcombinedtab tab" data-tabpageid="usedsaletabpage" data-caption="Used Sale"></div>
            <div data-type="tab" id="lossdamagetab" class="tab" data-tabpageid="lossdamagetabpage" data-caption="Loss and Damage"></div>
            <div data-type="tab" id="alltab" class="combinedtab tab" data-tabpageid="alltabpage" data-caption="Items"></div>
            <div data-type="tab" id="billingtab" class="tab" data-tabpageid="billingtabpage" data-caption="Billing"></div>
            <div data-type="tab" id="contactstab" class="tab" data-tabpageid="contactstabpage" data-caption="Contacts"></div>
            <div data-type="tab" id="picklisttab" class="tab submodule" data-tabpageid="picklisttabpage" data-caption="Pick List"></div>
            <div data-type="tab" id="contracttab" class="tab submodule" data-tabpageid="contracttabpage" data-caption="Contract"></div>
            <div data-type="tab" id="delivershiptab" class="tab" data-tabpageid="delivershiptabpage" data-caption="Deliver/Ship"></div>
            <div data-type="tab" id="subpurchaseordertab" class="tab submodule" data-tabpageid="subpurchaseordertabpage" data-caption="Sub Purchase Order"></div>
            <div data-type="tab" id="invoicetab" class="tab submodule" data-tabpageid="invoicetabpage" data-caption="Invoice"></div>        
            <div data-type="tab" id="notetab" class="notestab tab" data-tabpageid="notetabpage" data-caption="Notes"></div>
            <div data-type="tab" id="historytab" class="tab" data-tabpageid="historytabpage" data-caption="History"></div>
            <div data-type="tab" id="emailhistorytab" class="tab" data-tabpageid="emailhistorytabpage" data-caption="Email History"></div>
          </div>
          <div class="tabpages">
            <!-- ORDER TAB -->
            <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
              <div class="formpage">
                <!-- Order / Status section-->
                <div class="flexrow">
                  <div class="flexcolumn" style="flex:2 1 700px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Order No." data-datafield="OrderNumber" data-enabled="false" style="flex:0 1 100px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" data-required="true" style="flex:1 1 250px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" data-required="true" style="flex:1 1 175px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield dealnumber" data-caption="Deal No." data-datafield="DealNumber" data-enabled="false" style="flex:0 1 100px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation" data-formbeforevalidate="beforeValidate" data-required="true" style="flex:1 1 225px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield RateType" data-caption="Rate" data-datafield="RateType" data-displayfield="RateType" data-validationname="RateTypeValidation" data-validationpeek="false" data-required="true" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Type" data-datafield="OrderTypeId" data-displayfield="OrderType" data-validationname="OrderTypeValidation" data-required="true" style="flex:1 1 125px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield pick_date_validation" data-caption="Pick Date" data-datafield="PickDate" style="flex:1 1 115px;"></div>
                        <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Pick Time" data-datafield="PickTime" style="flex:1 1 84px;"></div>
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield pick_date_validation" data-caption="From Date" data-datafield="EstimatedStartDate" style="flex:1 1 115px;"></div>
                        <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="fwcontrol fwformfield" data-caption="From Time" data-datafield="EstimatedStartTime" data-required="false" style="flex:1 1 84px;"></div>
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield pick_date_validation" data-caption="To Date" data-datafield="EstimatedStopDate" style="flex:1 1 115px;"></div>
                        <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="fwcontrol fwformfield" data-caption="To Time" data-datafield="EstimatedStopTime" data-required="false" style="flex:1 1 84px;"></div>
                      </div>
                      <div class="flexrow" style="display:none;">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Rental" data-datafield="DisableEditingRentalRate" style="float:left;width:150px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Sales" data-datafield="DisableEditingSalesRate" style="float:left;width:150px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Miscellaneous" data-datafield="DisableEditingMiscellaneousRate" style="float:left;width:150px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Labor" data-datafield="DisableEditingLaborRate" style="float:left;width:150px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Used Sale" data-datafield="DisableEditingUsedSaleRate" style="float:left;width:150px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Loss and Damage" data-datafield="DisableEditingLossAndDamageRate" style="float:left;width:150px;"></div>
                      </div>
                    </div>
                  </div>
                  <!-- Status section -->
                  <div class="flexcolumn" style="flex:1 1 150px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Status">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Status" data-datafield="Status" data-enabled="false" style="flex:1 0 125px;"></div>
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="As of" data-datafield="StatusDate" data-enabled="false" style="flex:1 0 125px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Reference No." data-datafield="ReferenceNumber" style="flex:1 0 125px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
                <!-- Location / PO section -->
                <div class="flexrow">
                  <div class="flexcolumn" style="flex:1 1 350px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Location">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Location" data-datafield="Location"></div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:1 1 400px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="PO">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Pending" data-datafield="PendingPo" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Flat PO" data-datafield="FlatPo" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="PO No." data-datafield="PoNumber" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="PO Amount" data-datafield="PoAmount" style="flex:1 1 100px;"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:0 1 115px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Print">
                      <div class="print fwformcontrol" data-type="button" style="flex:1 1 50px;margin:15px 0 0 10px;">Print</div>
                    </div>
                  </div>
                </div>
               <!-- Summary Details -->
              <div class="flexrow">
                <div class="fwcontrol fwcontainer fwform-section summarySection" data-control="FwContainer" data-type="section" data-caption="Summary" style="flex:0 1 65%;">
                  <div class="flexrow totalRowFrames">
                    <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Charge</div>
                    <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Discount</div>
                    <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Total</div>
                    <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Cost</div>
                    <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Gross Profit</div>
                    <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Markup</div>
                    <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Margin</div>
                  </div>
                  <div class="flexrow totalRowFrames" style="margin-top:-10px;">
                    <div class="flexcolumn" style="width:7%;">
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalPrice"></div>
                    </div>
                    <div class="flexcolumn" style="width:7%;">
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalDiscount"></div>
                    </div>
                    <div class="flexcolumn totalColors" style="width:7%;">
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalTotal"></div>
                    </div>
                    <div class="flexcolumn" style="width:7%;">
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalCost"></div>
                    </div>
                    <div class="flexcolumn profitframes" style="width:7%;">
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalProfit"></div>
                    </div>
                    <div class="flexcolumn" style="width:7%;">
                      <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalMarkup"></div>
                    </div>
                    <div class="flexcolumn" style="width:7%;">
                      <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalMargin"></div>
                    </div>
                  </div>
                  <div class="allFrames">
                    <div class="flexrow" style="float:right;">
                      <div class="summaryweekly fwformcontrol" data-type="button" style="display:none; max-width: 56px;margin:5px 0 15px 10px;">Weekly</div>
                      <div class="summarymonthly fwformcontrol" data-type="button" style="display:none; max-width:72px; margin:5px 0 15px 10px;">Monthly</div>
                      <div class="summaryperiod fwformcontrol" data-type="button" style="max-width: 57px;margin:5px 0 15px 10px;">Period</div>
                    </div>
                    <div class="flexrow" style="flex:1 1 1200px; clear:right;">
                      <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">&#160;</div>
                      <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Rental</div>
                      <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Sales</div>
                      <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Facilities</div>
                      <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Transportation</div>
                      <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Crew</div>
                      <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Miscellaneous</div>
                      <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Used Sale</div>
                      <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Parts</div>
                      <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Sales Tax</div>
                      <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Total</div>
                    </div>
                    <div class="flexrow" style="flex:1 1 auto;">
                      <div class="flexcolumn" style="flex:1 1 7%;margin-top:25px;">
                        <div>Charge</div>
                      </div>
                      <div class="flexcolumn" style="flex:1 1 7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="RentalPrice"></div>
                      </div>
                      <div class="flexcolumn" style="flex:1 1 7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="SalesPrice"></div>
                      </div>
                      <div class="flexcolumn" style="flex:1 1 7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="FacilitiesPrice"></div>
                      </div>
                      <div class="flexcolumn" style="flex:1 1 7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TransportationPrice"></div>
                      </div>
                      <div class="flexcolumn" style="flex:1 1 7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="LaborPrice"></div>
                      </div>
                      <div class="flexcolumn" style="flex:1 1 7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="MiscPrice"></div>
                      </div>
                      <div class="flexcolumn" style="flex:1 1 7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="RentalSalePrice"></div>
                      </div>
                      <div class="flexcolumn" style="flex:1 1 7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="PartsPrice"></div>
                      </div>
                      <div class="flexcolumn" style="flex:1 1 7%;">
                        <div>&#160;</div>
                      </div>
                      <div class="flexcolumn" style="flex:1 1 7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalPrice"></div>
                      </div>
                    </div>
                    <div class="flexrow">
                      <div class="flexcolumn" style="width:7%;margin-top:25px;">
                        <div>Discount</div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="RentalDiscount"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="SalesDiscount"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="FacilitiesDiscount"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TransportationDiscount"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="LaborDiscount"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="MiscDiscount"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="RentalSaleDiscount"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="PartsDiscount"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div>&#160;</div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalDiscount"></div>
                      </div>
                    </div>
                    <div class="flexrow totalColors">
                      <div class="flexcolumn" style="width:7%;margin-top:25px;">
                        <div>Total</div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="RentalTotal"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="SalesTotal"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="FacilitiesTotal"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TransportationTotal"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="LaborTotal"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="MiscTotal"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="RentalSaleTotal"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="PartsTotal"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalTax"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalTotal"></div>
                      </div>
                    </div>
                    <div class="flexrow">
                      <div class="flexcolumn" style="width:7%;margin-top:25px;">
                        <div>Cost</div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="RentalCost"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="SalesCost"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="FacilitiesCost"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TransportationCost"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="LaborCost"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="MiscCost"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="RentalSaleCost"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="PartsCost"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TaxCost"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalCost"></div>
                      </div>
                    </div>
                    <div class="flexrow profitframes">
                      <div class="flexcolumn" style="width:7%;margin-top:25px;">
                        <div>Gross Profit</div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="RentalProfit"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="SalesProfit"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="FacilitiesProfit"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TransportationProfit"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="LaborProfit"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="MiscProfit"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="RentalSaleProfit"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="PartsProfit"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div>&#160;</div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalProfit"></div>
                      </div>
                    </div>
                    <div class="flexrow">
                      <div class="flexcolumn" style="width:7%;margin-top:25px;">
                        <div>Markup %</div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="RentalMarkup"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="SalesMarkup"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="FacilitiesMarkup"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TransportationMarkup"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="LaborMarkup"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="MiscMarkup"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="RentalSaleMarkup"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="PartsMarkup"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div>&#160;</div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalMarkup"></div>
                      </div>
                    </div>
                    <div class="flexrow">
                      <div class="flexcolumn" style="width:7%;margin-top:25px;">
                        <div>Margin %</div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="RentalMargin"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="SalesMargin"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="FacilitiesMargin"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TransportationMargin"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="LaborMargin"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="MiscMargin"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="RentalSaleMargin"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="PartsMargin"></div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div>&#160;</div>
                      </div>
                      <div class="flexcolumn" style="width:7%;">
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="TotalMargin"></div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn totalRowFrames">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Value / Replacement Cost" style="flex:0 1 30%;">
                    <div class="flexrow">
                      <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Total Value</div>
                      <div class="flexcolumn" style="text-align:center;flex:1 1 7%;font-size:.85em;">Total Replacement</div>
                    </div>
                    <div class="flexrow" style="margin-top:-10px;">
                      <div data-control="FwFormField" data-type="money" class="formcolumn fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="ValueTotal" data-formreadonly="true" style="flex: 1 1 125px;"></div>
                      <div data-control="FwFormField" data-type="money" class="formcolumn fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="ReplacementCostTotal" data-formreadonly="true" style="flex:1 1 125px;"></div>
                    </div>
                  </div>
                </div>
                <div style="flex:0 1 2%; margin-top:10px; margin-left: -5px;">
                  <i class="material-icons expandArrow expandFrames" style="cursor:pointer; background-color:#37474F; color:white">keyboard_arrow_right</i>
                  <i class="material-icons expandArrow hideFrames" style="cursor:pointer; background-color:#37474F; color:white">keyboard_arrow_down</i>
                </div>
              </div>
              <!-- Value/Cost // Weight // Office/Warehouse -->
              <div class="flexrow allFrames">
                <!-- Value / Cost -->
                <div class="flexcolumn">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Value / Replacement Cost">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="money" class="formcolumn fwcontrol fwformfield frame" data-caption="Total Value" data-datafield="" data-framedatafield="ValueTotal" data-formreadonly="true" style="flex: 1 1 125px;font-size:.85em;"></div>
                      <div data-control="FwFormField" data-type="money" class="formcolumn fwcontrol fwformfield frame" data-caption="Total Replacement" data-datafield="" data-framedatafield="ReplacementCostTotal" data-formreadonly="true" style="flex:1 1 125px;font-size:.85em;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="money" class="formcolumn fwcontrol fwformfield frame" data-caption="Owned Value" data-datafield="" data-framedatafield="ValueOwned" data-formreadonly="true" style="flex:1 1 125px;"></div>
                      <div data-control="FwFormField" data-type="money" class="formcolumn fwcontrol fwformfield frame" data-caption="Owned Replacement" data-datafield="" data-framedatafield="ReplacementCostOwned" data-formreadonly="true" style="flex:1 1 125px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="money" class="formcolumn fwcontrol fwformfield frame" data-caption="Sub Value" data-datafield="" data-framedatafield="ValueSubs" data-formreadonly="true" style="flex:1 1 125px;"></div>
                      <div data-control="FwFormField" data-type="money" class="formcolumn fwcontrol fwformfield frame" data-caption="Sub Replacement" data-datafield="" data-framedatafield="ReplacementCostSubs" data-formreadonly="true" style="flex:1 1 125px;"></div>
                    </div>
                  </div>
                </div>
                <!-- US Customary Weight -->
                <div class="flexcolumn">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="U.S. Customary Weight">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Pounds" data-datafield="" data-framedatafield="WeightPounds" data-formreadonly="true" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Ounces" data-datafield="" data-framedatafield="WeightOunces" data-formreadonly="true" style="flex:1 1 70px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Pounds (In Case)" data-datafield="" data-framedatafield="WeightInCasePounds" data-formreadonly="true" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Ounces (In Case)" data-datafield="" data-framedatafield="WeightInCaseOunces" data-formreadonly="true" style="flex:1 1 70px;"></div>
                    </div>
                  </div>
                </div>
                <!-- Metric Weight -->
                <div class="flexcolumn">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Metric Weight">
                  <div class="flexrow">
                    <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Kilograms" data-datafield="" data-framedatafield="WeightKilograms" data-formreadonly="true" style="flex:1 1 100px;"></div>
                    <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Grams" data-datafield="" data-framedatafield="WeightGrams" data-formreadonly="true" style="flex:1 1 70px;"></div>
                  </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Kikograms (In Case)" data-datafield="" data-framedatafield="WeightInCaseKilograms" data-formreadonly="true" style="flex:1 1     100px;"></div>
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Grams (In Case)" data-datafield="" data-framedatafield="WeightInCaseGrams" data-formreadonly="true" style="flex:1 1 70px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
                <!-- Activity section -->
                <div class="flexrow">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Activity" style="flex:1 1 770px">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Combine Activity" data-datafield="CombineActivity" style="display:none"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield anti-LD" data-caption="Rental" data-datafield="Rental" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield anti-LD" data-caption="Sales" data-datafield="Sales" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Miscellaneous" data-datafield="Miscellaneous" style="flex:1 1 125px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Labor" data-datafield="Labor" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield anti-LD" data-caption="Used Sale" data-datafield="RentalSale" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Loss and Damage" data-datafield="LossAndDamage" style="flex:1 1 125px;"></div>
                      <!--
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Facilities"      data-datafield="Facilities"     style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Transportation"  data-datafield="Transportation" style="flex:1 1 150px;"></div>
                      -->
                    </div>
                    <div class="flexrow" style="display:none;">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasRentalItem" data-datafield="HasRentalItem" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasSalesItem" data-datafield="HasSalesItem" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasMiscellaneousItem" data-datafield="HasMiscellaneousItem" style="flex:1 1 125px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasLaborItem" data-datafield="HasLaborItem" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasRentalSaleItem" data-datafield="HasRentalSaleItem" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasLossAndDamageItem" data-datafield="HasLossAndDamageItem" style="flex:1 1 125px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasNotes" data-datafield="HasNotes" style="flex:1 1 100px;"></div>
                    </div>
                  </div>
                </div>
                <!-- Personnel -->
                <div class="flexrow">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Personnel">
                    <div class="flexrow">
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Agent" data-datafield="AgentId" data-displayfield="Agent" data-enabled="true" data-required="true" data-validationname="UserValidation" style="flex:1 1 150px;"></div>
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Project Manager" data-datafield="ProjectManagerId" data-displayfield="ProjectManager" data-enabled="true" data-required="true" data-validationname="UserValidation" style="flex:1 1 150px;"></div>
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Outside Sales Representative" data-datafield="OutsideSalesRepresentativeId" data-displayfield="OutsideSalesRepresentative" data-enabled="true" data-validationname="ContactValidation" style="flex:1 1 150px;"></div>
                    </div>
                  </div>
                </div>

                <!-- Market Segment -->
                <div class="flexrow">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Market Segment">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Market Type" data-datafield="MarketTypeId" data-displayfield="MarketType" data-validationpeek="true" data-validationname="MarketTypeValidation" style="flex:1 1 150px;"></div>
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Market Segment" data-datafield="MarketSegmentId" data-displayfield="MarketSegment" data-validationpeek="true" data-formbeforevalidate="beforeValidateMarketSegment" data-validationname="MarketSegmentValidation" style="flex:1 1 150px;"></div>
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Job" data-datafield="MarketSegmentJobId" data-displayfield="MarketSegmentJob" data-validationpeek="true" data-formbeforevalidate="beforeValidateMarketSegment" data-validationname="MarketSegmentJobValidation" style="flex:1 1 150px;"></div>
                    </div>
                  </div>
                </div>

                <!--Documents -->
                <div class="flexrow">
                  <div class="flexcolumn" style="flex:1 1 500px;">
                    <div class="fwcontrol fwcontainer fwform-section itemsection" data-control="FwContainer" data-type="section" data-caption="Documents">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Cover Letter" data-datafield="CoverLetterId" data-displayfield="CoverLetter" data-enabled="true" data-validationname="CoverLetterValidation" style="flex:1 1 225px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Terms &#038; Conditions" data-datafield="TermsConditionsId" data-displayfield="TermsConditions" data-enabled="true" data-validationname="TermsConditionsValidation" style="flex:1 1 225px;"></div>
                      </div>
                    </div>
                  </div>

                  <!-- Office Location / Warehouse -->
                  <div class="flexcolumn" style="flex:1 1 300px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Office Location / Warehouse">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" data-enabled="false" style="flex:1 1 150px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" data-enabled="false" style="flex:1 1 150px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="WarehouseCode" data-datafield="WarehouseCode" data-formreadonly="true" data-enabled="false" style="display:none"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <!-- RENTAL TAB -->
            <div data-type="tabpage" id="rentaltabpage" class="rentalgrid notcombined tabpage" data-tabid="rentaltab" data-render="false">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Rental Items">
                <div class="wide-flexrow">
                  <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Rental Items"></div>
                </div>
              </div>
              <div class="flexrow" style="float:right;">
                <div class="flexcolumn rentalAdjustments" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield totals RentalDaysPerWeek" data-caption="D/W" data-datafield="RentalDaysPerWeek" data-digits="3" data-digitsoptional="false" style="flex:1 1 60px;"></div>
                      <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield bottom_line_discount" data-caption="Disc. %" data-rectype="R" data-datafield="RentalDiscountPercent" data-digits="2" style="flex:1 1 60px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals rentalOrderItemTotal bottom_line_total_tax rentalAdjustmentsPeriod" data-caption="Total" data-rectype="R" data-datafield="PeriodRentalTotal" style="flex:2 1 100px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield rentalTotalWithTax bottom_line_total_tax rentalAdjustmentsPeriod" data-caption="w/ Tax" data-rectype="R" data-datafield="PeriodRentalTotalIncludesTax" style="flex:1 1 70px;margin-top:10px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals rentalOrderItemTotal bottom_line_total_tax rentalAdjustmentsWeekly" data-caption="Total" data-rectype="R" data-datafield="WeeklyRentalTotal" style="flex:1 1 90px; display:none;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield rentalTotalWithTax bottom_line_total_tax rentalAdjustmentsWeekly" data-caption="w/ Tax" data-rectype="R" data-datafield="WeeklyRentalTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals rentalOrderItemTotal bottom_line_total_tax rentalAdjustmentsMonthly" data-caption="Total" data-rectype="R" data-datafield="MonthlyRentalTotal" style="flex:1 1 90px; display:none;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield rentalTotalWithTax bottom_line_total_tax rentalAdjustmentsMonthly" data-caption="w/ Tax" data-rectype="R" data-datafield="MonthlyRentalTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn rentaltotals" style="flex:2 1 550px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 85px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield totals totalType" data-caption="" data-gridtype="rental" data-datafield="" style="flex:0 1 100px;">
                        <div data-value="W" class="weeklyType" data-caption="Weekly" style="margin-top:5px;"></div>
                        <div data-value="M" class="monthlyType" data-caption="Monthly" style="margin-top:5px;"></div>
                        <div data-value="P" class="periodType" data-caption="Period"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <!-- SALES TAB -->
            <div data-type="tabpage" id="salestabpage" class="salesgrid notcombined tabpage" data-tabid="salestab" data-render="false">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sales Items">
                <div class="wide-flexrow">
                  <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Sales Items"></div>
                </div>
              </div>
              <div class="flexrow" style="float:right;">
                <div class="flexcolumn salesAdjustments" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom_line_discount" data-caption="Disc. %" data-rectype="S" data-datafield="SalesDiscountPercent" style="flex:1 1 50px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals salesOrderItemTotal bottom_line_total_tax" data-caption="Total" data-rectype="S" data-datafield="SalesTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield salesTotalWithTax bottom_line_total_tax" data-caption="w/ Tax" data-rectype="S" data-datafield="SalesTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn salestotals" style="flex:2 1 550px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <!-- LABOR TAB -->
            <div data-type="tabpage" id="labortabpage" class="laborgrid notcombined tabpage" data-tabid="labortab" data-render="false">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Labor Items">
                <div class="wide-flexrow">
                  <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Labor Items"></div>
                </div>
              </div>
              <div class="flexrow" style="float:right;">
                <div class="flexcolumn laborAdjustments" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom_line_discount" data-caption="Disc. %" data-rectype="L" data-datafield="LaborDiscountPercent" style="flex:1 1 50px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals laborOrderItemTotal bottom_line_total_tax laborAdjustmentsPeriod" data-caption="Total" data-rectype="L" data-datafield="PeriodLaborTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield laborTotalWithTax bottom_line_total_tax laborAdjustmentsPeriod" data-caption="w/ Tax" data-rectype="L" data-datafield="PeriodLaborTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>

                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals laborOrderItemTotal bottom_line_total_tax laborAdjustmentsWeekly" data-caption="Total" data-rectype="L" data-datafield="WeeklyLaborTotal" style="flex:1 1 100px; display:none;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield laborTotalWithTax bottom_line_total_tax laborAdjustmentsWeekly" data-caption="w/ Tax" data-rectype="L" data-datafield="WeeklyLaborTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals laborOrderItemTotal bottom_line_total_tax laborAdjustmentsMonthly" data-caption="Total" data-rectype="L" data-datafield="MonthlyLaborTotal" style="flex:1 1 100px; display:none;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield laborTotalWithTax bottom_line_total_tax laborAdjustmentsMonthly" data-caption="w/ Tax" data-rectype="L" data-datafield="MonthlyLaborTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn labortotals" style="flex:2 1 550px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:2 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:2 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:2 1 100px;"></div>
                      <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield totals totalType" data-caption="" data-gridtype="labor" data-datafield="" style="flex:0 1 100px;">
                        <div data-value="W" class="weeklyType" data-caption="Weekly" style="margin-top:5px;"></div>
                        <div data-value="M" class="monthlyType" data-caption="Monthly" style="margin-top:5px;"></div>
                        <div data-value="P" class="periodType" data-caption="Period"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <!-- MISC TAB -->
            <div data-type="tabpage" id="misctabpage" class="miscgrid notcombined tabpage" data-tabid="misctab" data-render="false">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Misc Items">
                <div class="wide-flexrow">
                  <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Misc Items"></div>
                </div>
              </div>
              <div class="flexrow" style="float:right;">
                <div class="flexcolumn" style="flex:1 1 125px;">
                </div>
                <div class="flexcolumn miscAdjustments" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom_line_discount" data-caption="Disc. %" data-rectype="M" data-datafield="MiscDiscountPercent" style="flex:1 1 50px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals miscOrderItemTotal bottom_line_total_tax miscAdjustmentsPeriod" data-caption="Total" data-rectype="M" data-datafield="PeriodMiscTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield miscTotalWithTax bottom_line_total_tax miscAdjustmentsPeriod" data-caption="w/ Tax" data-rectype="M" data-datafield="PeriodMiscTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals miscOrderItemTotal bottom_line_total_tax miscAdjustmentsWeekly" data-caption="Total" data-rectype="M" data-datafield="WeeklyMiscTotal" style="flex:1 1 100px; display:none;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield miscTotalWithTax bottom_line_total_tax miscAdjustmentsWeekly" data-caption="w/ Tax" data-rectype="M" data-datafield="WeeklyMiscTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals miscOrderItemTotal bottom_line_total_tax miscAdjustmentsMonthly" data-caption="Total" data-rectype="M" data-datafield="MonthlyMiscTotal" style="flex:1 1 100px; display:none;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield miscTotalWithTax bottom_line_total_tax miscAdjustmentsMonthly" data-caption="w/ Tax" data-rectype="M" data-datafield="MonthlyMiscTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn misctotals" style="flex:2 1 550px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield totals totalType" data-caption="" data-gridtype="misc" data-datafield="" style="flex:0 1 100px;">
                        <div data-value="W" class="weeklyType" data-caption="Weekly" style="margin-top:5px;"></div>
                        <div data-value="M" class="monthlyType" data-caption="Monthly" style="margin-top:5px;"></div>
                        <div data-value="P" class="periodType" data-caption="Period"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <!-- USED SALE TAB -->
            <div data-type="tabpage" id="usedsaletabpage" class="usedsalegrid notcombined tabpage" data-tabid="usedsaletab" data-render="false">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Used Sale Items">
                <div class="wide-flexrow">
                  <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Used Sale Items"></div>
                </div>
              </div>
            </div>
            <!-- LOSS AND DAMAGE TAB -->
            <div data-type="tabpage" id="lossdamagetabpage" class="lossdamagegrid tabpage" data-tabid="lossdamagetab">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Loss and Damage">
                <div class="wide-flexrow">
                  <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Loss Damage Items"></div>
                </div>
              </div>
              <div class="flexrow" style="float:right;">
                <div class="flexcolumn" style="flex:1 1 125px;">
                </div>
                <div class="flexcolumn lossdamageAdjustments" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom_line_discount" data-caption="Disc. %" data-rectype="F" data-datafield="LossAndDamageDiscountPercent" style="flex:1 1 50px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals lossOrderItemTotal bottom_line_total_tax" data-caption="Total" data-rectype="F" data-datafield="LossAndDamageTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield lossTotalWithTax bottom_line_total_tax" data-caption="w/ Tax" data-rectype="F" data-datafield="LossAndDamageTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn lossdamagetotals" style="flex:2 1 550px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <!-- ALL TAB -->
            <div data-type="tabpage" id="alltabpage" class="combinedgrid combined tabpage" data-tabid="alltab" data-render="false">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Items">
                <div class="fwcontrol fwcontainer fwform-fieldrow combinedgrid" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Order Items"></div>
                </div>
              </div>
              <div class="flexrow" style="float:right;">
                <div class="flexcolumn" style="flex:1 1 125px;">
                </div>
                <div class="flexcolumn combinedAdjustments" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield combineddw" data-caption="D/W" data-datafield="CombinedDaysPerWeek" data-digits="3" data-digitsoptional="false" style="flex:1 1 50px;display:none;" data-enabled="true"></div>
                      <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom_line_discount" data-caption="Disc. %" data-rectype="" data-datafield="CombinedDiscountPercent" style="flex:1 1 50px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals combinedOrderItemTotal bottom_line_total_tax combinedAdjustmentsPeriod" data-caption="Total" data-rectype="" data-datafield="PeriodCombinedTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield combinedTotalWithTax bottom_line_total_tax combinedAdjustmentsPeriod" data-caption="w/ Tax" data-rectype="" data-datafield="PeriodCombinedTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals combinedOrderItemTotal bottom_line_total_tax combinedAdjustmentsWeekly" data-caption="Total" data-rectype="" data-datafield="WeeklyCombinedTotal" style="flex:1 1 100px; display:none;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield combinedTotalWithTax bottom_line_total_tax combinedAdjustmentsWeekly" data-caption="w/ Tax" data-rectype="" data-datafield="WeeklyCombinedTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals combinedOrderItemTotal bottom_line_total_tax combinedAdjustmentsMonthly" data-caption="Total" data-rectype="" data-datafield="MonthlyCombinedTotal" style="flex:1 1 100px; display:none;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield combinedTotalWithTax bottom_line_total_tax combinedAdjustmentsMonthly" data-caption="w/ Tax" data-rectype="" data-datafield="MonthlyCombinedTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn combinedtotals" style="flex:2 1 550px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield totals totalType" data-caption="" data-gridtype="combined" data-datafield="" style="flex:0 1 100px;">
                        <div data-value="W" class="weeklyType" data-caption="Weekly" style="margin-top:5px;"></div>
                        <div data-value="M" class="monthlyType" data-caption="Monthly" style="margin-top:5px;"></div>
                        <div data-value="P" class="periodType" data-caption="Period"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <!--  PICK LIST TAB  -->
            <div data-type="tabpage" id="picklisttabpage" class="tabpage submodule picklist" data-tabid="picklisttab">
              <!--<div class="formcolumn" style="width:100%;">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="OrderPickListGrid" data-securitycaption="Pick List"></div>
                </div>
              </div>-->
            </div>
            <!-- BILLING TAB PAGE -->
            <div data-type="tabpage" id="billingtabpage" class="tabpage" data-tabid="billingtab">
              <!-- Billing Period -->
              <div class="flexrow">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billing Details">
                  <div class="flexrow">
                    <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield billing_start_date" data-caption="Start" data-datafield="BillingStartDate" style="flex:1 1 150px;"></div>
                    <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield billing_end_date" data-caption="Stop" data-datafield="BillingEndDate" style="flex:1 1 150px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield BillingWeeks week_or_month_field" data-caption="Weeks" data-datafield="BillingWeeks" style="flex:1 1 150px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield BillingMonths week_or_month_field" data-caption="Months" data-datafield="BillingMonths" style="flex:1 1 150px;"></div>
                    <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Delay Billing Search Until" data-datafield="DelayBillingSearchUntil" style="flex:1 1 150px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Lock Billing Dates" data-datafield="LockBillingDates" style="flex:1 1 150px;padding-left:25px;margin-top:10px;"></div>
                  </div>
                  <div class="flexrow">
                    <div data-control="FwFormField" data-type="validation" data-validationname="BillingCycleValidation" class="fwcontrol fwformfield" data-caption="Billing Cycle" data-datafield="BillingCycleId" data-displayfield="BillingCycle" style="flex:1 1 250px;" data-required="true"></div>
                    <div data-control="FwFormField" data-type="validation" data-validationname="PaymentTermsValidation" class="fwcontrol fwformfield" data-caption="Payment Terms" data-datafield="PaymentTermsId" data-displayfield="PaymentTerms" style="flex:1 1 250px;"></div>
                    <div data-control="FwFormField" data-type="validation" data-validationname="PaymentTypeValidation" class="fwcontrol fwformfield" data-caption="Pay Type" data-datafield="PaymentTypeId" data-displayfield="PaymentType" style="flex:1 1 250px;"></div>
                    <div data-control="FwFormField" data-type="validation" data-validationname="CurrencyValidation" class="fwcontrol fwformfield" data-caption="Currency Code" data-datafield="CurrencyId" data-displayfield="CurrencyCode" style="flex:1 1 250px;"></div>
                  </div>
                </div>
              </div>
              <!-- Bill Based On / Labor Fees / Contact Confirmation -->
              <div class="flexrow">
                <div class="flexcolumn" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Determine Quantities to Bill Based on">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="DetermineQuantitiesToBillBasedOn" style="flex:1 1 250px;">
                        <div data-value="CONTRACT" data-caption="Contract Activity"></div>
                        <div data-value="ORDER" data-caption="Order Quantity"></div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn" style="flex:1 1 25px;">
                  &#32;
                </div>
                <div class="flexcolumn" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Labor Prep Fees">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="IncludePrepFeesInRentalRate" style="flex:1 1 400px;">
                        <div data-value="false" data-caption="Add Prep Fees as Labor Charges"></div>
                        <div data-value="true" data-caption="Add Prep Fees into the Rental Item Rate"></div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn" style="flex:1 1 25px;">
                  &#32;
                </div>
                <div class="flexcolumn" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contact Confirmation">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Require Contact Confirmation" data-datafield="RequireContactConfirmation" style="flex:1 1 125px;"></div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- Tax Rates / Order Group / Contact Confirmation -->
              <div class="flexrow">
                <div class="flexcolumn" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Tax Rates">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" data-validationname="TaxOptionValidation" class="fwcontrol fwformfield" data-caption="Tax Option" data-datafield="TaxOptionId" data-displayfield="TaxOption" style="flex:1 1 250px"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="percent" data-digits="4" class="fwcontrol fwformfield" data-caption="Rental" data-datafield="RentalTaxRate1" style="flex:1 1 75px;"></div>
                      <div data-control="FwFormField" data-type="percent" data-digits="4" class="fwcontrol fwformfield" data-caption="Sales" data-datafield="SalesTaxRate1" style="flex:1 1 75px;"></div>
                      <div data-control="FwFormField" data-type="percent" data-digits="4" class="fwcontrol fwformfield" data-caption="Labor" data-datafield="LaborTaxRate1" style="flex:1 1 75px;"></div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn" style="flex:1 1 25px;">
                  &#32;
                </div>
                <div class="flexcolumn" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Hiatus Schedule">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="HiatusDiscountFrom" style="flex:1 1 200px;">
                        <div data-value="DEAL" data-caption="Deal" style="flex:1 1 100px;"></div>
                        <div data-value="ORDER" data-caption="This Order" style="flex:1 1 100px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn" style="flex:1 1 25px;">
                  &#32;
                </div>
                <div class="flexcolumn" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Group">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="In Group?" data-datafield="InGroup" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Group No" data-datafield="GroupNumber" style="flex:1 1 125px;"></div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- Issue To / Bill To Address -->
              <div class="flexrow">
                <div class="flexcolumn" style="flex:1 1 15%;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quote Address">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="PrintIssuedToAddressFrom" style="flex:1 1 150px;">
                        <div data-value="DEAL" data-caption="Deal" style="flex:1 1 100px;"></div>
                        <div data-value="CUSTOMER" data-caption="Customer" style="flex:1 1 100px;"></div>
                        <div data-value="ORDER" data-caption="Order" style="flex:1 1 100px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn" style="flex:1 1 42.5%;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Issue To">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Name" data-datafield="IssuedToName" style="flex:1 1 250px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention" data-datafield="IssuedToAttention" style="flex:1 1 250px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="IssuedToAttention2" style="flex:1 1 250px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address" data-datafield="IssuedToAddress1" style="flex:1 1 250px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="IssuedToAddress2" style="flex:1 1 250px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="IssuedToCity" style="flex:1 1 250px;"></div>
                      <div data-control="FwFormField" data-type="validation" data-validationname="StateValidation" class="fwcontrol fwformfield" data-caption="State/Province" data-datafield="IssuedToState" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="IssuedToZipCode" style="flex:1 1 100px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" data-validationname="CountryValidation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="IssuedToCountryId" data-displayfield="IssuedToCountry" style="flex:1 1 250px;"></div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn" style="flex:1 1 42.5%;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Bill To">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Different Than Issue To Address" data-datafield="BillToAddressDifferentFromIssuedToAddress" style="flex:1 1 250px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="Name" data-datafield="BillToName" data-enabled="false" style="flex:1 1 250px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="Attention" data-datafield="BillToAttention" data-enabled="false" style="flex:1 1 250px;"></div>
                      <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="" data-datafield="BillToAttention2" data-enabled="false" style="flex:1 1 250px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="Address" data-datafield="BillToAddress1" data-enabled="false" style="flex:1 1 250px;"></div>
                      <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="" data-datafield="BillToAddress2" data-enabled="false" style="flex:1 1 250px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="City" data-datafield="BillToCity" data-enabled="false" style="flex:1 1 250px;"></div>
                      <div data-control="FwFormField" data-type="validation" data-validationname="StateValidation" class="differentaddress fwcontrol fwformfield" data-caption="State/Province" data-datafield="BillToState" data-enabled="false" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="BillToZipCode" data-enabled="false" style="flex:1 1 100px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" data-validationname="CountryValidation" class="differentaddress fwcontrol fwformfield" data-caption="Country" data-datafield="BillToCountryId" data-displayfield="BillToCountry" data-enabled="false" style="flex:1 1 250px;"></div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- Options -->
              <div class="flexrow">
                <div class="flexcolumn" style="flex:1 1 400px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="No Charge">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="No Charge" data-datafield="NoCharge" style="flex:1 1 75px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Reason" data-datafield="NoChargeReason" style="flex:1 1 350px;"></div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn" style="flex:1 1 675px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Rental Items to go Out again after being Checked-In without increasing the Order quantity" data-datafield="RoundTripRentals" style="flex:1 1 650px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Discount Reason" data-datafield="DiscountReasonId" data-displayfield="DiscountReason" data-validationname="DiscountReasonValidation" style="flex:1 1 250px; float:left;"></div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <!-- DELIVER/SHIP TAB -->
            <div data-type="tabpage" id="delivershiptabpage" class="tabpage" data-tabid="delivershiptab">
              <div class="flexpage">
                <div class="flexrow">
                  <div class="flexcolumn" style="flex:1 1 550px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Outgoing">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield outtype delivery-delivery" data-caption="Type" data-datafield="OutDeliveryDeliveryType" style="flex:1 1 150px;"></div>
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="On" data-datafield="OutDeliveryTargetShipDate" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Required By" data-datafield="OutDeliveryRequiredDate" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="time" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Required Time" data-datafield="OutDeliveryRequiredTime" style="flex:1 1 75px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Contact" data-datafield="OutDeliveryToContact" style="flex:1 1 250px;"></div>
                        <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Phone" data-datafield="OutDeliveryToContactPhone" style="flex:1 1 250px;"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Ship Via">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" data-validationname="VendorValidation" class="fwcontrol fwformfield" data-caption="Carrier" data-datafield="OutDeliveryCarrierId" data-displayfield="OutDeliveryCarrier" data-formbeforevalidate="beforeValidateCarrier"></div>
                        <div data-control="FwFormField" data-type="validation" data-validationname="ShipViaValidation" class="fwcontrol fwformfield" data-caption="Ship Via" data-datafield="OutDeliveryShipViaId" data-displayfield="OutDeliveryShipVia" data-formbeforevalidate="beforeValidateOutShipVia"></div>
                      </div>
                    </div>
                    <div class="flexrow">
                      <div class="flexcolumn" style="width:25%;flex:0 1 auto;">
                        <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield OutDeliveryAddressType delivery-type-radio" data-caption="Address" data-datafield="OutDeliveryAddressType">
                          <div data-value="DEAL" data-caption="Deal"></div>
                          <div data-value="OTHER" data-caption="Other"></div>
                          <div data-value="VENUE" data-caption="Venue"></div>
                          <div data-value="WAREHOUSE" data-caption="Warehouse"></div>
                        </div>
                      </div>
                      <div class="flexcolumn" style="width:75%;">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Location" data-datafield="OutDeliveryToLocation"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention" data-datafield="OutDeliveryToAttention"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address" data-datafield="OutDeliveryToAddress1"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="OutDeliveryToAddress2"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="OutDeliveryToCity"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State/Province" data-datafield="OutDeliveryToState"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="OutDeliveryToZipCode"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-validationname="CountryValidation" data-datafield="OutDeliveryToCountryId" data-displayfield="OutDeliveryToCountry"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Cross Streets" data-datafield="OutDeliveryToCrossStreets"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Notes" data-datafield="OutDeliveryDeliveryNotes"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Order No" data-datafield="OutDeliveryOnlineOrderNumber"></div>
                          <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield online" data-caption="Status" data-datafield="OutDeliveryOnlineOrderStatus"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:1 1 550px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Incoming">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield intype delivery-delivery" data-caption="Type" data-datafield="InDeliveryDeliveryType" style="flex:1 1 150px;"></div>
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="On" data-datafield="InDeliveryTargetShipDate" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Required By" data-datafield="InDeliveryRequiredDate" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="time" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Required Time" data-datafield="InDeliveryRequiredTime" style="flex:1 1 75px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Contact" data-datafield="InDeliveryToContact" style="flex:1 1 250px;"></div>
                        <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Phone" data-datafield="InDeliveryToContactPhone" style="flex:1 1 250px;"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Ship Via">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" data-validationname="VendorValidation" class="fwcontrol fwformfield" data-caption="Carrier" data-datafield="InDeliveryCarrierId" data-displayfield="InDeliveryCarrier" data-formbeforevalidate="beforeValidateCarrier"></div>
                        <div data-control="FwFormField" data-type="validation" data-validationname="ShipViaValidation" class="fwcontrol fwformfield" data-caption="Ship Via" data-datafield="InDeliveryShipViaId" data-displayfield="InDeliveryShipVia" data-formbeforevalidate="beforeValidateInShipVia"></div>
                      </div>
                    </div>
                    <div class="flexrow">
                      <div class="flexcolumn" style="width:25%;flex:0 1 auto;">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield InDeliveryAddressType delivery-type-radio" data-caption="Address" data-datafield="InDeliveryAddressType">
                            <div data-value="DEAL" data-caption="Deal"></div>
                            <div data-value="OTHER" data-caption="Other"></div>
                            <div data-value="VENUE" data-caption="Venue"></div>
                            <div data-value="WAREHOUSE" data-caption="Warehouse"></div>
                          </div>
                        </div>
                        <div class="flexrow">
                          <div class="copy fwformcontrol" data-type="button" style="flex:0 1 50px;margin:15px 0 0 10px;text-align:center;">Copy</div>
                        </div>
                      </div>
                      <div class="flexcolumn" style="width:75%;">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Location" data-datafield="InDeliveryToLocation"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention" data-datafield="InDeliveryToAttention"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address" data-datafield="InDeliveryToAddress1"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="InDeliveryToAddress2"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="InDeliveryToCity"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State/Province" data-datafield="InDeliveryToState"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="InDeliveryToZipCode"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-validationname="CountryValidation" data-datafield="InDeliveryToCountryId" data-displayfield="InDeliveryToCountry"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Cross Streets" data-datafield="InDeliveryToCrossStreets"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Notes" data-datafield="InDeliveryDeliveryNotes"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
           <!-- SUB PURCHASE ORDER TAB -->
           <div data-type="tabpage" id="subpurchaseordertabpage" class="tabpage subPurchaseOrderSubModule" data-tabid="subpurchaseordertab">
              </div>
            <!-- INVOICE tab -->
           <div data-type="tabpage" id="invoicetabpage" class="tabpage invoiceSubModule" data-tabid="invoicetab">
              </div>
            <!-- CONTACTS TAB -->
            <div data-type="tabpage" id="contactstabpage" class="tabpage" data-tabid="contactstab">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contacts">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="OrderContactGrid" data-securitycaption="Contacts"></div>
                </div>
              </div>
            </div>
            <!-- NOTES TAB -->
            <div data-type="tabpage" id="notetabpage" class="tabpage" data-tabid="notetab">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Notes">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="OrderNoteGrid" data-securitycaption="Notes"></div>
                </div>
              </div>
            </div>
            <!--  CONTRACT TAB  -->
            <div data-type="tabpage" id="contracttabpage" class="tabpage submodule contract" data-tabid="contracttab">
              &#32;
            </div>
            <!-- HISTORY TAB -->
            <div data-type="tabpage" id="historytabpage" class="tabpage" data-tabid="historytab">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Status History">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="OrderStatusHistoryGrid" data-securitycaption="Order Status History"></div>
                </div>
              </div>
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Snapshot">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="OrderSnapshotGrid" data-securitycaption="Order Snapshot"></div>
                </div>
              </div>
            </div>
           <div data-type="tabpage" id="emailhistorytabpage" class="tabpage submodule emailhistory-page" data-tabid="emailhistorytab"></div>
          </div>
        </div>
      </div>`;
    }
    //----------------------------------------------------------------------------------------------
    getSoundUrls = ($form): void => {
        this.successSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).successSoundFileName;
        this.errorSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).errorSoundFileName;
    }
    //----------------------------------------------------------------------------------------------
    cancelPickList(pickListId, pickListNumber, $form) {
        var $confirmation, $yes, $no;
        var orderId = FwFormField.getValueByDataField($form, 'OrderId');
        $confirmation = FwConfirmation.renderConfirmation('Cancel Pick List', '<div style="white-space:pre;">\n' +
            'Cancel Pick List ' + pickListNumber + '?</div>');
        $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
        $no = FwConfirmation.addButton($confirmation, 'No');
        $yes.on('click', function () {
            FwAppData.apiMethod(true, 'DELETE', 'api/v1/picklist/' + pickListId, {}, FwServices.defaultTimeout, function onSuccess(response) {
                try {
                    FwNotification.renderNotification('SUCCESS', 'Pick List Cancelled');
                    FwConfirmation.destroyConfirmation($confirmation);
                    var $pickListGridControl = $form.find('[data-name="OrderPickListGrid"]');
                    $pickListGridControl.data('ondatabind', function (request) {
                        request.uniqueids = {
                            OrderId: orderId
                        };
                    });
                    FwBrowse.search($pickListGridControl);
                }
                catch (ex) {
                    FwFunc.showError(ex);
                }
            }, null, $form);
        });
    };
    //----------------------------------------------------------------------------------------------
    // Form menu item -- corresponding grid menu item function in OrderSnapshotGrid controller
    viewSnapshotOrder($form, event) {
        let $orderForm, $selectedCheckBoxes, $orderSnapshotGrid, snapshotId, orderNumber;

        $orderSnapshotGrid = $form.find(`[data-name="OrderSnapshotGrid"]`);
        $selectedCheckBoxes = $orderSnapshotGrid.find('.cbselectrow:checked');

        try {
            if ($selectedCheckBoxes.length !== 0) {
                for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                    snapshotId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="SnapshotId"]').attr('data-originalvalue');
                    orderNumber = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderNumber"]').attr('data-originalvalue');
                    var orderInfo: any = {};
                    orderInfo.OrderId = snapshotId;
                    $orderForm = OrderController.openForm('EDIT', orderInfo);
                    FwModule.openSubModuleTab($form, $orderForm);
                    jQuery('.tab.submodule.active').find('.caption').html(`Snapshot for Order ${orderNumber}`);
                }
            } else {
                FwNotification.renderNotification('WARNING', 'Select rows in Order Snapshot Grid in order to perform this function.');
            }
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
    };
    //----------------------------------------------------------------------------------------------
    addLossDamage($form: JQuery, event: any): void {
        let sessionId, $lossAndDamageItemGridControl;
        const userWarehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
        const dealId = FwFormField.getValueByDataField($form, 'DealId');
        const errorSound = new Audio(this.errorSoundFileName);
        const successSound = new Audio(this.successSoundFileName);
        const HTML: Array<string> = [];
        HTML.push(
            `<div class="fwcontrol fwcontainer fwform popup" data-control="FwContainer" data-type="form" data-caption="Loss and Damage">
              <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
                <div style="float:right;" class="close-modal"><i class="material-icons">clear</i><div class="btn-text">Close</div></div>
                <div class="tabpages">
                  <div class="formpage">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Loss and Damage">
                      <div class="formrow">
                        <div class="formcolumn" style="width:100%;margin-top:50px;">
                          <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                            <div class="fwform-section-title" style="margin-bottom:20px;">Loss and Damage</div>
                            <div class="formrow error-msg"></div>
                            <div class="formrow sub-header" style="margin-left:8px;font-size:16px;"><span>Select one or more Orders with Lost or Damaged items, then click Continue</span></div>
                            <div data-control="FwGrid" class="container"></div>
                          </div>
                        </div>
                      </div>
                      <div class="formrow add-button">
                        <div class="select-items fwformcontrol" data-type="button" style="float:right;">Continue</div>
                      </div>
                      <div class="formrow session-buttons" style="display:none;">
                        <div class="options-button fwformcontrol" data-type="button" style="float:left">Options &#8675;</div>
                        <div class="selectall fwformcontrol" data-type="button" style="float:left; margin-left:10px;">Select All</div>
                        <div class="selectnone fwformcontrol" data-type="button" style="float:left; margin-left:10px;">Select None</div>
                        <div class="complete-session fwformcontrol" data-type="button" style="float:right;">Add To Order</div>
                      </div>
                      <div class="formrow option-list" style="display:none;">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Placeholder..." data-datafield=""></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>`
        );

        const addOrderBrowse = () => {
            let $browse = jQuery(this.getBrowseTemplate());
            $browse.attr('data-hasmultirowselect', 'true');
            $browse.attr('data-type', 'Browse');
            $browse.attr('data-showsearch', 'false');
            FwBrowse.setAfterRenderRowCallback($browse, function ($tr, dt, rowIndex) {
                if (dt.Rows[rowIndex][dt.ColumnIndex['Status']] === 'CANCELLED') {
                    $tr.css('color', '#aaaaaa');
                }
            });

            $browse = FwModule.openBrowse($browse);
            $browse.find('.fwbrowse-menu').hide();

            $browse.data('ondatabind', function (request) {
                request.ActiveViewFields = OrderController.ActiveViewFields;
                request.pagesize = 15;
                request.orderby = 'OrderDate desc';
                request.miscfields = {
                    LossAndDamage: true,
                    LossAndDamageWarehouseId: userWarehouseId,
                    LossAndDamageDealId: dealId
                }
            });
            return $browse;
        }

        const startLDSession = (): void => {
            let $browse = jQuery($popup).children().find('.fwbrowse');
            let orderId, $selectedCheckBoxes: any, orderIds: string = '';
            $selectedCheckBoxes = $browse.find('.cbselectrow:checked');
            if ($selectedCheckBoxes.length !== 0) {
                for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                    orderId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderId"]').attr('data-originalvalue');
                    if (orderId) {
                        orderIds = orderIds.concat(', ', orderId);
                    }
                }
                orderIds = orderIds.substring(2);

                const request: any = {};
                request.OrderIds = orderIds;
                request.DealId = dealId;
                request.WarehouseId = userWarehouseId;
                FwAppData.apiMethod(true, 'POST', `api/v1/lossanddamage/startsession`, request, FwServices.defaultTimeout, response => {
                    sessionId = response.SessionId
                    this.lossDamageSessionId = sessionId;
                    if (sessionId) {
                        $popup.find('.container').html('<div class="formrow"><div data-control="FwGrid" data-grid="LossAndDamageItemGrid" data-securitycaption=""></div></div>');
                        $popup.find('.add-button').hide();
                        $popup.find('.sub-header').hide();
                        $popup.find('.session-buttons').show();
                        const $lossAndDamageItemGrid = $popup.find('div[data-grid="LossAndDamageItemGrid"]');
                        $lossAndDamageItemGridControl = jQuery(jQuery('#tmpl-grids-LossAndDamageItemGridBrowse').html());
                        $lossAndDamageItemGrid.data('sessionId', sessionId);
                        $lossAndDamageItemGrid.data('orderId', orderId);
                        $lossAndDamageItemGrid.empty().append($lossAndDamageItemGridControl);
                        $lossAndDamageItemGridControl.data('ondatabind', function (request) {
                            request.uniqueids = {
                                SessionId: sessionId
                            };
                        });
                        FwBrowse.init($lossAndDamageItemGridControl);
                        FwBrowse.renderRuntimeHtml($lossAndDamageItemGridControl);

                        FwBrowse.search($lossAndDamageItemGridControl);
                    }
                }, null, $browse);
            } else {
                FwNotification.renderNotification('WARNING', 'Select rows in order to perform this function.');
            }
        }
        const events = () => {
            let $orderItemGridLossDamage = $form.find('.lossdamagegrid [data-name="OrderItemGrid"]');
            let gridContainer = $popup.find('.container');
            //Close the popup
            $popup.find('.close-modal').one('click', e => {
                FwPopup.destroyPopup($popup);
                jQuery(document).find('.fwpopup').off('click');
                jQuery(document).off('keydown');
            });
            // Starts LD session
            $popup.find('.select-items').on('click', event => {
                startLDSession();
            });
            // Complete Session
            $popup.find('.complete-session').on('click', event => {
            let $lossAndDamageItemGrid = $popup.find('div[data-grid="LossAndDamageItemGrid"]');
            $lossAndDamageItemGrid = jQuery($lossAndDamageItemGrid);
                let request: any = {};
                request.SourceOrderId = FwFormField.getValueByDataField($form, 'OrderId');
                request.SessionId = this.lossDamageSessionId
                FwAppData.apiMethod(true, 'POST', `api/v1/lossanddamage/completesession`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    if (response.success === true) {
                        FwPopup.destroyPopup($popup);
                        FwBrowse.search($orderItemGridLossDamage);
                    } else {
                        FwNotification.renderNotification('ERROR', response.msg); //justin 01/31/2019
                    }
                }, null, $lossAndDamageItemGrid)
            });
            // Select All
            $popup.find('.selectall').on('click', e => {
                let $lossAndDamageItemGrid = $popup.find('div[data-grid="LossAndDamageItemGrid"]');
                $lossAndDamageItemGrid = jQuery($lossAndDamageItemGrid);

                const request: any = {};
                request.SessionId = this.lossDamageSessionId; //justin 01/31/2019
                FwAppData.apiMethod(true, 'POST', `api/v1/lossanddamage/selectall`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    $popup.find('.error-msg').html('');
                    if (response.success === false) {
                        errorSound.play();
                        $popup.find('div.error-msg').html(`<div><span>${response.msg}</span></div>`);
                    } else {
                        successSound.play();
                        FwBrowse.search($lossAndDamageItemGridControl);
                    }
                }, function onError(response) {
                    FwFunc.showError(response);
                    }, $lossAndDamageItemGrid);
            });
            // Select None
            $popup.find('.selectnone').on('click', e => {
                let $lossAndDamageItemGrid = $popup.find('div[data-grid="LossAndDamageItemGrid"]');
                $lossAndDamageItemGrid = jQuery($lossAndDamageItemGrid);

                const request: any = {};
                request.SessionId = this.lossDamageSessionId; //justin 01/31/2019
                FwAppData.apiMethod(true, 'POST', `api/v1/lossanddamage/selectnone`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    $popup.find('.error-msg').html('');
                    if (response.success === false) {
                        errorSound.play();
                        FwBrowse.search($lossAndDamageItemGridControl);
                        $popup.find('div.error-msg').html(`<div><span">${response.msg}</span></div>`);
                    } else {
                        successSound.play();
                        FwBrowse.search($lossAndDamageItemGridControl); //justin 01/31/2019
                    }
                }, function onError(response) {
                    FwFunc.showError(response);
                    }, $lossAndDamageItemGrid);
            });
            //Options button
            $popup.find('.options-button').on('click', e => {
                $popup.find('div.formrow.option-list').toggle();
            });
        }
        const $popupHtml = HTML.join('');
        const $popup = FwPopup.renderPopup(jQuery($popupHtml), { ismodal: true });
        FwPopup.showPopup($popup);
        const $orderBrowse = addOrderBrowse();
        $popup.find('.container').append($orderBrowse);
        FwBrowse.search($orderBrowse);
        events();
    }
    //----------------------------------------------------------------------------------------------
    retireLossDamage($form: JQuery): void {
        const $confirmation = FwConfirmation.renderConfirmation('Confirm?', '');
        $confirmation.find('.fwconfirmationbox').css('width', '450px');

        const html: Array<string> = [];;
        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('    <div>Create a Lost Contract and Retire the Items?</div>');
        html.push('  </div>');
        html.push('</div>');

        FwConfirmation.addControls($confirmation, html.join(''));
        const $yes = FwConfirmation.addButton($confirmation, 'Retire', false);
        const $no = FwConfirmation.addButton($confirmation, 'Cancel');

        $yes.on('click', retireLD);

        function retireLD() {
            const orderId = FwFormField.getValueByDataField($form, 'OrderId');
            const request: any = {}
            request.OrderId = orderId;
            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            FwFormField.disable($no);
            $yes.text('Retiring...');
            $yes.off('click');

            FwAppData.apiMethod(true, 'POST', `api/v1/lossanddamage/retire`, request, FwServices.defaultTimeout, function onSuccess(response) {
                if (response.success === true) {
                    const uniqueids: any = {};
                    uniqueids.ContractId = response.ContractId;
                    FwNotification.renderNotification('SUCCESS', 'Order Successfully Retired');
                    FwConfirmation.destroyConfirmation($confirmation);
                    const $contractForm = ContractController.loadForm(uniqueids);
                    FwModule.openModuleTab($contractForm, "", true, 'FORM', true)
                    FwModule.refreshForm($form, OrderController);
                }
            }, function onError(response) {
                $yes.on('click', retireLD);
                $yes.text('Retire');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
                FwModule.refreshForm($form, OrderController);
                }, $form);
        }
    }
    //----------------------------------------------------------------------------------------------
    createSnapshotOrder($form: JQuery, event: any): void {
        let orderNumber, orderId, $orderSnapshotGrid;
        orderNumber = FwFormField.getValueByDataField($form, 'OrderNumber');
        orderId = FwFormField.getValueByDataField($form, 'OrderId');
        $orderSnapshotGrid = $form.find('[data-name="OrderSnapshotGrid"]');

        let $confirmation, $yes, $no;

        $confirmation = FwConfirmation.renderConfirmation('Create Snapshot', '');
        $confirmation.find('.fwconfirmationbox').css('width', '450px');
        let html = [];
        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push(`    <div>Create a Snapshot for Order ${orderNumber}?</div>`);
        html.push('  </div>');
        html.push('</div>');

        FwConfirmation.addControls($confirmation, html.join(''));

        $yes = FwConfirmation.addButton($confirmation, 'Create Snapshot', false);
        $no = FwConfirmation.addButton($confirmation, 'Cancel');

        $yes.on('click', createSnapshot);
        let $confirmationbox = jQuery('.fwconfirmationbox');
        function createSnapshot() {
            FwAppData.apiMethod(true, 'POST', `api/v1/order/createsnapshot/${orderId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', 'Snapshot Successfully Created.');
                FwConfirmation.destroyConfirmation($confirmation);

                $orderSnapshotGrid.data('ondatabind', request => {
                    request.uniqueids = {
                        OrderId: orderId,
                    }
                    request.pagesize = 10;
                    request.orderby = "OrderDate"
                });

                $orderSnapshotGrid.data('beforesave', request => {
                    request.OrderId = orderId;
                });

                FwBrowse.search($orderSnapshotGrid);
            }, null, $confirmationbox);
        }
    };

    //----------------------------------------------------------------------------------------------
};
//---------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{427FCDFE-7E42-4081-A388-150D3D7FAE36}'] = function (event) {
    let $form;
    $form = jQuery(this).closest('.fwform');
    if ($form.attr('data-mode') !== 'NEW') {
        try {
            OrderController.addLossDamage($form, event);
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
    } else {
        FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
    }
};
//---------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{78ED6DE2-D2A2-4D0D-B4A6-16F1C928C412}'] = function (event) {
    let $form;
    $form = jQuery(this).closest('.fwform');
    if ($form.attr('data-mode') !== 'NEW') {
        try {
            OrderController.retireLossDamage($form);
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
    } else {
        FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
    }
};
//---------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{AB1D12DC-40F6-4DF2-B405-54A0C73149EA}'] = function (event) {
    let $form;
    $form = jQuery(this).closest('.fwform');

    try {
        OrderController.createSnapshotOrder($form, event);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//---------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{03000DCC-3D58-48EA-8BDF-A6D6B30668F5}'] = function (event) {
    //View Snapshot
    let $form;
    $form = jQuery(this).closest('.fwform');

    try {
        OrderController.viewSnapshotOrder($form, event);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};

//---------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{91C9FD3E-ADEE-49CE-BB2D-F00101DFD93F}'] = function (event) {
    var $form, $pickListForm;
    try {
        $form = jQuery(this).closest('.fwform');
        var mode = 'EDIT';
        var orderInfo: any = {};
        orderInfo.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
        $pickListForm = CreatePickListController.openForm(mode, orderInfo);
        FwModule.openSubModuleTab($form, $pickListForm);
        jQuery('.tab.submodule.active').find('.caption').html('New Pick List');
        var $pickListUtilityGrid;
        $pickListUtilityGrid = $pickListForm.find('[data-name="PickListUtilityGrid"]');
        FwBrowse.search($pickListUtilityGrid);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};

//----------------------------------------------------------------------------------------------
//Confirmation for cancelling Pick List
FwApplicationTree.clickEvents['{C6CC3D94-24CE-41C1-9B4F-B4F94A50CB48}'] = function (event) {
    var $form, pickListId, pickListNumber;
    $form = jQuery(this).closest('.fwform');
    pickListId = $form.find('tr.selected > td.column > [data-formdatafield="PickListId"]').attr('data-originalvalue');
    pickListNumber = $form.find('tr.selected > td.column > [data-formdatafield="PickListNumber"]').attr('data-originalvalue');
    try {
        OrderController.cancelPickList(pickListId, pickListNumber, $form);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};

//----------------------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{E25CB084-7E7F-4336-9512-36B7271AC151}'] = function (event) {
    var $form;
    $form = jQuery(this).closest('.fwform');

    try {
        OrderController.copyOrderOrQuote($form);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};

//----------------------------------------------------------------------------------------------
//Form Cancel Option
FwApplicationTree.clickEvents['{6B644862-9030-4D42-A29B-30C8DAC29D3E}'] = function (event) {
    let $form
    $form = jQuery(this).closest('.fwform');

    try {
        OrderController.cancelUncancelOrder($form);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};

//----------------------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{CF245A59-3336-42BC-8CCB-B88807A9D4EA}'] = function (e) {
    var $form, $orderStatusForm;
    try {
        $form = jQuery(this).closest('.fwform');
        var mode = 'EDIT';
        var orderInfo: any = {};
        orderInfo.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
        orderInfo.OrderNumber = FwFormField.getValueByDataField($form, 'OrderNumber');
        $orderStatusForm = OrderStatusController.openForm(mode, orderInfo);
        FwModule.openSubModuleTab($form, $orderStatusForm);
        jQuery('.tab.submodule.active').find('.caption').html('Order Status');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
//Check In Option
FwApplicationTree.clickEvents['{380318B6-7E4D-446D-A018-1EB7720F4338}'] = function (e) {
    var $form, $checkinForm;
    try {
        $form = jQuery(this).closest('.fwform');
        var mode = 'EDIT';
        var orderInfo: any = {};
        orderInfo.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
        orderInfo.OrderNumber = FwFormField.getValueByDataField($form, 'OrderNumber');
        $checkinForm = CheckInController.openForm(mode, orderInfo);
        FwModule.openSubModuleTab($form, $checkinForm);
        jQuery('.tab.submodule.active').find('.caption').html('Check-In');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{771DCE59-EB57-48B2-B189-177B414A4ED3}'] = function (event) {
    // Stage Item/ Check Out
    let $form, $stagingCheckoutForm;
    try {
        $form = jQuery(this).closest('.fwform');
        var mode = 'EDIT';
        var orderInfo: any = {};
        orderInfo.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
        orderInfo.OrderNumber = FwFormField.getValueByDataField($form, 'OrderNumber');
        orderInfo.WarehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
        orderInfo.Warehouse = $form.find('div[data-datafield="WarehouseId"] input.fwformfield-text').val();
        orderInfo.DealId = FwFormField.getValueByDataField($form, 'DealId');
        orderInfo.Deal = $form.find('div[data-datafield="DealId"] input.fwformfield-text').val();
        $stagingCheckoutForm = StagingCheckoutController.openForm(mode, orderInfo);
        FwModule.openSubModuleTab($form, $stagingCheckoutForm);
        jQuery('.tab.submodule.active').find('.caption').html('Staging / Check-Out');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};

//----------------------------------------------------------------------------------------------
//Open Search Interface
FwApplicationTree.clickEvents['{B2D127C6-A1C2-4697-8F3B-9A678F3EAEEE}'] = function (e) {
    let search, $form, orderId;
    $form = jQuery(this).closest('.fwform');
    orderId = FwFormField.getValueByDataField($form, 'OrderId');
    if ($form.attr('data-mode') === 'NEW') {
        OrderController.saveForm($form, { closetab: false });
        return;
    }
    if (orderId == "") {
        FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
    } else {
        search = new SearchInterface();
        search.renderSearchPopup($form, orderId, 'Order');
    }
};

//----------------------------------------------------------------------------------------------
FwApplicationTree.clickEvents['{F2FD2F4C-1AB7-4627-9DD5-1C8DB96C5509}'] = function (e) {
    var $form;
    try {
        $form = jQuery(this).closest('.fwform');
        $form.find('.print').trigger('click');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//---------------------------------------------------------------------------------
//Browse Cancel Option
FwApplicationTree.clickEvents['{DAE6DC23-A2CA-4E36-8214-72351C4E1449}'] = function (event) {
    let $confirmation, $yes, $no, $browse, orderId, orderStatus;

    $browse = jQuery(this).closest('.fwbrowse');
    orderId = $browse.find('.selected [data-browsedatafield="OrderId"]').attr('data-originalvalue');
    orderStatus = $browse.find('.selected [data-formdatafield="Status"]').attr('data-originalvalue');

    try {
        if (orderId != null) {
            if (orderStatus === "CANCELLED") {
                $confirmation = FwConfirmation.renderConfirmation('Cancel', '');
                $confirmation.find('.fwconfirmationbox').css('width', '450px');
                let html = [];
                html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                html.push('    <div>Would you like to un-cancel this Order?</div>');
                html.push('  </div>');
                html.push('</div>');

                FwConfirmation.addControls($confirmation, html.join(''));
                $yes = FwConfirmation.addButton($confirmation, 'Un-Cancel Order', false);
                $no = FwConfirmation.addButton($confirmation, 'Cancel');

                $yes.on('click', uncancelOrder);
            }
            else {
                $confirmation = FwConfirmation.renderConfirmation('Cancel', '');
                $confirmation.find('.fwconfirmationbox').css('width', '450px');
                let html = [];
                html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                html.push('    <div>Would you like to cancel this Order?</div>');
                html.push('  </div>');
                html.push('</div>');

                FwConfirmation.addControls($confirmation, html.join(''));
                $yes = FwConfirmation.addButton($confirmation, 'Cancel Order', false);
                $no = FwConfirmation.addButton($confirmation, 'Cancel');

                $yes.on('click', cancelOrder);
            }

            function cancelOrder() {
                let request: any = {};

                FwFormField.disable($confirmation.find('.fwformfield'));
                FwFormField.disable($yes);
                $yes.text('Canceling...');
                $yes.off('click');

                FwAppData.apiMethod(true, 'POST', `api/v1/order/cancel/${orderId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    FwNotification.renderNotification('SUCCESS', 'Order Successfully Cancelled');
                    FwConfirmation.destroyConfirmation($confirmation);
                    FwBrowse.databind($browse);
                }, function onError(response) {
                    $yes.on('click', cancelOrder);
                    $yes.text('Cancel');
                    FwFunc.showError(response);
                    FwFormField.enable($confirmation.find('.fwformfield'));
                    FwFormField.enable($yes);
                    FwBrowse.databind($browse);
                }, $browse);
            };

            function uncancelOrder() {
                let request: any = {};

                FwFormField.disable($confirmation.find('.fwformfield'));
                FwFormField.disable($yes);
                $yes.text('Retrieving...');
                $yes.off('click');

                FwAppData.apiMethod(true, 'POST', `api/v1/order/uncancel/${orderId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    FwNotification.renderNotification('SUCCESS', 'Order Successfully Retrieved');
                    FwConfirmation.destroyConfirmation($confirmation);
                    FwBrowse.databind($browse);
                }, function onError(response) {
                    $yes.on('click', uncancelOrder);
                    $yes.text('Cancel');
                    FwFunc.showError(response);
                    FwFormField.enable($confirmation.find('.fwformfield'));
                    FwFormField.enable($yes);
                    FwBrowse.databind($browse);
                }, $browse);
            };
        } else {
            FwNotification.renderNotification('WARNING', 'Select an Order to perform this action.');
        }
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};

//----------------------------------------------------------------------------------------------
var OrderController = new Order();