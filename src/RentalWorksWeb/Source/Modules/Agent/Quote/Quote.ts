//routes.push({ pattern: /^module\/quote$/, action: function (match: RegExpExecArray) { return QuoteController.getModuleScreen(); } });
//routes.push({ pattern: /^module\/quote\/(\S+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { 'datafield': match[1], 'search': match[2] }; return QuoteController.getModuleScreen(filter); } });

//----------------------------------------------------------------------------------------------
class Quote extends OrderBase {
    Module: string = 'Quote';
    apiurl: string = 'api/v1/quote';
    caption: string = Constants.Modules.Agent.children.Quote.caption;
    nav: string = Constants.Modules.Agent.children.Quote.nav;
    id: string = Constants.Modules.Agent.children.Quote.id;
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        options.hasInactive = false;
        FwMenu.addBrowseMenuButtons(options);

        FwMenu.addSubMenuItem(options.$groupOptions, 'Cancel / Uncancel', 'dpH0uCuEp3E89', (e: JQuery.ClickEvent) => {
            try {
                this.browseCancelOption(options.$browse);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        const $all: JQuery = FwMenu.generateDropDownViewBtn('All', true, "ALL");
        const $new: JQuery = FwMenu.generateDropDownViewBtn('New', false, "NEW");
        const $request: JQuery = FwMenu.generateDropDownViewBtn('Request', false, "REQUEST");
        const $prospect: JQuery = FwMenu.generateDropDownViewBtn('Prospect', true, "PROSPECT");
        const $active: JQuery = FwMenu.generateDropDownViewBtn('Active', false, "ACTIVE");
        const $reserved: JQuery = FwMenu.generateDropDownViewBtn('Reserved', false, "RESERVED");
        const $ordered: JQuery = FwMenu.generateDropDownViewBtn('Ordered', false, "ORDERED");
        const $cancelled: JQuery = FwMenu.generateDropDownViewBtn('Cancelled', false, "CANCELLED");
        const $closed: JQuery = FwMenu.generateDropDownViewBtn('Closed', false, "CLOSED");

        FwMenu.addVerticleSeparator(options.$menu);

        let viewSubitems: Array<JQuery> = [];
        viewSubitems.push($all, $new, $request, $prospect, $active, $reserved, $ordered, $cancelled, $closed);
        FwMenu.addViewBtn(options.$menu, 'View', viewSubitems, true, "Status");

        if (sessionStorage.getItem('userType') === 'USER') {
            //Location Filter
            const location = JSON.parse(sessionStorage.getItem('location'));
            const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
            const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);

            if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
                this.ActiveViewFields.LocationId = [location.locationid];
            }

            let viewLocation: Array<JQuery> = [];
            viewLocation.push($userLocation, $allLocations);
            FwMenu.addViewBtn(options.$menu, 'Location', viewLocation, true, "LocationId");
        } else if (sessionStorage.getItem('userType') === 'CONTACT') {
            //Location Filter
            const deal = JSON.parse(sessionStorage.getItem('deal'));
            //const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
            //const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);

            if (typeof this.ActiveViewFields["DealId"] == 'undefined') {
                this.ActiveViewFields.DealId = [deal.dealid];
            }

            //let viewLocation: Array<JQuery> = [];
            //viewLocation.push($userLocation, $allLocations);
            //FwMenu.addViewBtn($menuObject, 'Location', viewLocation, true, "LocationId");
        }
    }
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        FwMenu.addFormMenuButtons(options);

        FwMenu.addSubMenuItem(options.$groupOptions, 'Copy Quote', '8eK9AJhpOq8c4', (e: JQuery.ClickEvent) => {
            try {
                this.copyOrderOrQuote(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Print Quote', '', (e: JQuery.ClickEvent) => {
            try {
                this.printQuoteOrder(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'QuikSearch', '', (e: JQuery.ClickEvent) => {
            try {
                this.search(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Create Order', 'jzLmFvzdy5hE1', (e: JQuery.ClickEvent) => {
            try {
                this.createOrder(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'New Version', '6KMadUFDT4cX4', (e: JQuery.ClickEvent) => {
            try {
                this.createNewVersionQuote(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Make Quote Active', '7mrZ4Q8ShsJ', (e: JQuery.ClickEvent) => {
            try {
                this.makeQuoteActive(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Reserve / Unreserve', '1oBE7m2rBjxhm', (e: JQuery.ClickEvent) => {
            try {
                this.reserveQuote(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Cancel / Uncancel', 'dpH0uCuEp3E89', (e: JQuery.ClickEvent) => {
            try {
                this.cancelUncancel(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Change Office Location', 'eu2FcQiK9adgk', (e: JQuery.ClickEvent) => {
            try {
                this.changeOfficeLocation(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
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
            const chartFilters = JSON.parse(sessionStorage.getItem('chartfilter'));
            if (!chartFilters) {
                FwBrowse.databind($browse);
                FwBrowse.screenload($browse);
            }
        };

        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentModuleInfo?: any) {
        let $form = super.openForm(mode, parentModuleInfo);
        //FwTabs.hideTab($form.find('.activitytab'));
        let userType = sessionStorage.getItem('userType');

        if (mode === 'NEW') {
            if (userType === 'CONTACT') {
                let deal = JSON.parse(sessionStorage.getItem('deal'));
                FwFormField.setValueByDataField($form, 'DealId', deal.dealid, deal.deal);
                FwFormField.setValueByDataField($form, 'PendingPo', true);
                FwFormField.setValueByDataField($form, 'PoNumber', 'PENDING');
            }
        }
        if (userType === 'CONTACT') {
            FwFormField.disableDataField($form, 'DealId');
        }

        //Toggle Buttons - Manifest tab - View Items
        FwFormField.loadItems($form.find('div[data-datafield="manifestItems"]'), [
            { value: 'SUMMARY', caption: 'Summary', checked: 'checked' },
            { value: 'DETAIL',  caption: 'Detail' }
        ]);
        //Toggle Buttons - Manifest tab - Filter By
        FwFormField.loadItems($form.find('div[data-datafield="manifestFilter"]'), [
            { value: 'ALL',   caption: 'All', checked: 'checked' },
            { value: 'SHORT', caption: 'Short' }
        ]);
        //Toggle Buttons - Manifest tab - Rental Valuation
        FwFormField.loadItems($form.find('div[data-datafield="rentalValueSelector"]'), [
            { value: 'UNIT VALUE',       text: 'Unit Value', selected: 'checked' },
            { value: 'REPLACEMENT COST', text: 'Replacement Cost' }
        ], true);
        //Toggle Buttons - Manifest tab - Sales Valuation
        FwFormField.loadItems($form.find('div[data-datafield="salesValueSelector"]'), [
            { value: 'SELL PRICE',   text: 'Sell Price', selected: 'checked' },
            { value: 'DEFAULT COST', text: 'Default Cost' },
            { value: 'AVERAGE COST', text: 'Average Cost' }
        ], true);
        //Toggle Buttons - Manifest tab - Weight Type
        FwFormField.loadItems($form.find('div[data-datafield="weightSelector"]'), [
            { value: 'IMPERIAL', caption: 'Imperial', checked: 'checked' },
            { value: 'METRIC',   caption: 'Metric' }
        ]);

        $form
            .on('change', 'div[data-datafield="manifestItems"], div[data-datafield="manifestFilter"], div[data-datafield="rentalValueSelector"], div[data-datafield="salesValueSelector"]', event => {
                let $OrderManifestGrid = $form.find('div[data-name="OrderManifestGrid"]');
    
                FwBrowse.search($OrderManifestGrid);
            })
            .on('change', 'div[data-datafield="weightSelector"]', e => {
                if (FwFormField.getValueByDataField($form, 'weightSelector') === 'IMPERIAL') {
                    $form.find('div[data-datafield="ExtendedWeightTotalLbs"]').show();
                    $form.find('div[data-datafield="ExtendedWeightTotalOz"]').show();
                    $form.find('div[data-datafield="ExtendedWeightTotalKg"]').hide();
                    $form.find('div[data-datafield="ExtendedWeightTotalGm"]').hide();
                } else {
                    $form.find('div[data-datafield="ExtendedWeightTotalLbs"]').hide();
                    $form.find('div[data-datafield="ExtendedWeightTotalOz"]').hide();
                    $form.find('div[data-datafield="ExtendedWeightTotalKg"]').show();
                    $form.find('div[data-datafield="ExtendedWeightTotalGm"]').show();
                }
            })
        ;

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT', uniqueids);
        $form.find('div.fwformfield[data-datafield="QuoteId"] input').val(uniqueids.QuoteId);

        let nodeActivity = FwApplicationTree.getNodeById(FwApplicationTree.tree, 'hb52dbhX1mNLZ');
        if (nodeActivity !== undefined && nodeActivity.properties.visible === 'T') {
            FwTabs.showTab($form.find('.activitytab'));
        }
        
        // Documents Grid - Need to put this here, because renderGrids is called from openForm and uniqueid is not available yet on the form
        FwAppDocumentGrid.renderGrid({
            $form: $form,
            caption: 'Documents',
            nameGrid: 'QuoteDocumentGrid',
            getBaseApiUrl: () => {
                return `${this.apiurl}/${uniqueids.QuoteId}/document`;
            },
            gridSecurityId: 'xCSRqSpYe73d',
            moduleSecurityId: this.id,
            parentFormDataFields: 'QuoteId',
            uniqueid1Name: 'QuoteId',
            getUniqueid1Value: () => uniqueids.QuoteId,
            uniqueid2Name: '',
            getUniqueid2Value: () => ''
        });

        FwModule.loadForm(this.Module, $form);

        let userType = sessionStorage.getItem('userType');
        if (userType === 'CONTACT') {
            let PoNumber = FwFormField.getValueByDataField($form, 'PoNumber');
            if (PoNumber === '' || PoNumber === 'PENDING') {
                FwFormField.setValueByDataField($form, 'PendingPo', true);
                FwFormField.setValueByDataField($form, 'PoNumber', 'PENDING');
            } else {
                FwFormField.setValueByDataField($form, 'PendingPo', false);
            }
        }

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        super.renderGrids($form);

        FwBrowse.renderGrid({
            nameGrid:         'OrderManifestGrid',
            gridSecurityId:   '8uhwXXJ95d3o',
            moduleSecurityId: this.id,
            $form:            $form,
            //getBaseApiUrl: () => `${this.apiurl}/manifest`,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId:     FwFormField.getValueByDataField($form, 'QuoteId'),
                    RentalValue: FwFormField.getValueByDataField($form, 'rentalValueSelector'),
                    SalesValue:  FwFormField.getValueByDataField($form, 'salesValueSelector'),
                    FilterBy:    FwFormField.getValueByDataField($form, 'manifestFilter'),
                    Mode:        FwFormField.getValueByDataField($form, 'manifestItems')
                };
                request.totalfields = ['OrderValueTotal', 'OrderReplacementTotal', 'OwnedValueTotal', 'OwnedReplacementTotal', 'SubValueTotal', 'SubReplacementTotal', 'ShippingContainerTotal', 'ShippingItemTotal', 'PieceCountTotal', 'StandAloneItemTotal', 'TotalExtendedWeightLbs', 'TotalExtendedWeightOz', 'TotalExtendedWeightKg', 'TotalExtendedWeightGr'];
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                var $barcodecells = $browse.find('div[data-browsedatafield="Barcode"]');
                for (var i = 0; i < $barcodecells.length; i++) {
                    let cell = jQuery($barcodecells[i]).parent();
                    if (FwFormField.getValueByDataField($form, 'manifestItems') == 'SUMMARY') {
                        jQuery(cell).hide();
                    } else {
                        jQuery(cell).show();
                    }
                }

                FwFormField.setValue($form, 'div[data-datafield="OrderValueTotal"]', dt.Totals.OrderValueTotal);
                FwFormField.setValue($form, 'div[data-datafield="OrderReplacementTotal"]', dt.Totals.OrderReplacementTotal);
                FwFormField.setValue($form, 'div[data-datafield="OwnedValueTotal"]', dt.Totals.OwnedValueTotal);
                FwFormField.setValue($form, 'div[data-datafield="OwnedReplacementTotal"]', dt.Totals.OwnedReplacementTotal);
                FwFormField.setValue($form, 'div[data-datafield="SubValueTotal"]', dt.Totals.SubValueTotal);
                FwFormField.setValue($form, 'div[data-datafield="SubReplacementTotal"]', dt.Totals.SubReplacementTotal);
                FwFormField.setValue($form, 'div[data-datafield="ShippingContainerTotal"]', dt.Totals.ShippingContainerTotal);
                FwFormField.setValue($form, 'div[data-datafield="ShippingItemTotal"]', dt.Totals.ShippingItemTotal);
                FwFormField.setValue($form, 'div[data-datafield="PieceCountTotal"]', dt.Totals.PieceCountTotal);
                FwFormField.setValue($form, 'div[data-datafield="StandAloneItemTotal"]', dt.Totals.StandAloneItemTotal);

                FwFormField.setValue($form, 'div[data-datafield="ExtendedWeightTotalLbs"]', dt.Totals.TotalExtendedWeightLbs);
                FwFormField.setValue($form, 'div[data-datafield="ExtendedWeightTotalOz"]', dt.Totals.TotalExtendedWeightOz);
                FwFormField.setValue($form, 'div[data-datafield="ExtendedWeightTotalKg"]', dt.Totals.TotalExtendedWeightKg);
                FwFormField.setValue($form, 'div[data-datafield="ExtendedWeightTotalGm"]', dt.Totals.TotalExtendedWeightGr);
            }
        });
        FwBrowse.addLegend($form.find('div[data-name="OrderManifestGrid"]'), 'Shipping Container', '#ffeb3b');
        FwBrowse.addLegend($form.find('div[data-name="OrderManifestGrid"]'), 'Stand-Alone Item', '#2196f3');
    }
    //----------------------------------------------------------------------------------------------
    getBrowseTemplate(): string {
        return `
        <div data-name="Quote" data-control="FwBrowse" data-type="Browse" id="QuoteBrowse" class="fwcontrol fwbrowse" data-controller="QuoteController">
          <div class="column flexcolumn" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="true" data-datafield="QuoteId" data-datatype="key" data-sort="off"></div>
          </div>
          <div class="column" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="true" data-datafield="OrderTypeId" data-browsedatatype="key"></div>
          </div>
          <!--<div class="column" data-width="0" data-visible="false">
            <div class="field" data-datafield="Inactive" data-datatype="text"  data-visible="false"></div>
          </div>-->
          <div class="column flexcolumn" max-width="75px" data-visible="true">
            <div class="field" data-caption="Quote No" data-datafield="QuoteNumber" data-cellcolor="NumberColor" data-datatype="text" data-sort="desc" data-sortsequence="2" data-searchfieldoperators="startswith"></div>
          </div>
          <div class="column flexcolumn" max-width="75px" data-visible="true">
            <div class="field" data-caption="Date" data-datafield="QuoteDate" data-browsedatatype="date" data-sortsequence="1" data-sort="desc"></div>
          </div>
          <div class="column flexcolumn" max-width="20px" data-visible="true">
            <div class="field" data-caption="Version" data-datafield="VersionNumber" data-datatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="350px" data-visible="true">
            <div class="field" data-caption="Description" data-datafield="Description" data-cellcolor="DescriptionColor" data-datatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="250px" data-visible="true">
            <div class="field" data-caption="Deal" data-datafield="Deal" data-datatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="75px" data-visible="true">
            <div class="field" data-caption="Deal No" data-datafield="DealNumber" data-datatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="75px" data-visible="true">
            <div class="field" data-caption="Status" data-datafield="Status" data-cellcolor="StatusColor" data-datatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="75px" data-visible="true">
            <div class="field" data-caption="Status As Of" data-datafield="StatusDate" data-datatype="date" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="100px" data-visible="true">
            <div class="field" data-caption="Total" data-datafield="Total" data-datatype="money" data-cellcolor="CurrencyColor" data-formatnumeric="true" data-digits="2" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="75px" data-visible="true">
            <div class="field" data-caption="PO No." data-datafield="PoNumber" data-cellcolor="PoNumberColor" data-datatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="180px" data-visible="true">
            <div class="field" data-caption="Agent" data-datafield="Agent" data-multiwordseparator="|" data-datatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="150px" data-visible="true">
            <div class="field" data-caption="Warehouse" data-datafield="WarehouseCode" data-cellcolor="WarehouseColor" data-datatype="text" data-sort="off"></div>
          </div>
        </div>
        `;
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
        <div id="quoteform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Quote" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="QuoteController">
          <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="QuoteId"></div>
          <div id="quoteform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
              <div data-type="tab" id="generaltab" class="tab generaltab " data-tabpageid="generaltabpage" data-caption="Quote"></div>
              <div data-type="tab" id="rentaltab" class="tab rentaltab notcombinedtab " data-tabpageid="rentaltabpage" data-notOnNew="true" data-caption="Rental"></div>
              <div data-type="tab" id="salestab" class="tab salestab notcombinedtab" data-tabpageid="salestabpage" data-notOnNew="true" data-caption="Sales"></div>
              <div data-type="tab" id="labortab" class="tab labortab notcombinedtab" data-tabpageid="labortabpage" data-notOnNew="true" data-caption="Labor"></div>
              <div data-type="tab" id="misctab" class="tab misctab notcombinedtab" data-tabpageid="misctabpage" data-notOnNew="true" data-caption="Miscellaneous"></div>
              <div data-type="tab" id="usedsaletab" class="tab usedsaletab notcombinedtab" data-tabpageid="usedsaletabpage" data-notOnNew="true" data-caption="Used Sale"></div>
              <div data-type="tab" id="alltab" class="tab combinedtab" data-tabpageid="alltabpage" data-notOnNew="true" data-caption="Items"></div>
              <div data-type="tab" id="billingtab" class="tab billingtab" data-tabpageid="billingtabpage" data-caption="Billing"></div>
              <div data-type="tab" id="profitlosstab" class="tab profitlosstab" data-tabpageid="profitlosstabpage" data-caption="Profit &amp; Loss"></div>
              <div data-type="tab" id="contactstab" class="tab contactstab" data-tabpageid="contactstabpage" data-caption="Contacts"></div> 
              <div data-type="tab" id="activitytab" class="tab activitytab" data-tabpageid="activitytabpage" data-caption="Activities"></div>
              <div data-type="tab" id="delivershiptab" class="tab delivershiptab" data-tabpageid="delivershiptabpage" data-caption="Deliver/Ship"></div>
              <div data-type="tab" id="manifesttab" class="tab manifesttab" data-tabpageid="manifesttabpage" data-caption="Manifest"></div>
              <div data-type="tab" id="documentstab" class="tab documentstab" data-tabpageid="documentstabpage" data-caption="Documents"></div>
              <div data-type="tab" id="notetab" class="tab notestab" data-tabpageid="notetabpage" data-caption="Notes"></div>
              <div data-type="tab" id="historytab" class="tab historytab" data-tabpageid="historytabpage" data-caption="History"></div>
              <div data-type="tab" id="emailhistorytab" class="tab emailhistorytab" data-tabpageid="emailhistorytabpage" data-caption="Email History"></div>
            </div>
            <div class="tabpages">
              <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
                <div class="formpage">
                  <div class="flexrow">
                    <!-- Customer section -->
                    <div class="flexcolumn" style="flex:1 1 300px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quote">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Quote No." data-datafield="QuoteNumber" data-enabled="false" style="flex:1 1 200px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Version" data-datafield="VersionNumber" data-enabled="false" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" data-required="true" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" data-required="true" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield dealnumber" data-caption="Deal No." data-datafield="DealNumber" data-enabled="false" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation" data-formbeforevalidate="beforeValidateDeal" data-required="false" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Customer" data-datafield="CustomerId" data-displayfield="Customer" data-validationname="CustomerValidation" style="flex:1 1 0;display:none;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield RateType" data-caption="Rate" data-datafield="RateType" data-displayfield="RateType" data-validationname="RateTypeValidation" data-validationpeek="false" data-required="true" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Type" data-datafield="OrderTypeId" data-displayfield="OrderType" data-validationname="OrderTypeValidation" data-required="true" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Reference No." data-datafield="ReferenceNumber" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Location" data-datafield="Location" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow" style="display:none;">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Rental" data-datafield="DisableEditingRentalRate" style="float:left;width:150px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Sales" data-datafield="DisableEditingSalesRate" style="float:left;width:150px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Miscellaneous" data-datafield="DisableEditingMiscellaneousRate" style="float:left;width:150px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Labor" data-datafield="DisableEditingLaborRate" style="float:left;width:150px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Used Sale" data-datafield="DisableEditingUsedSaleRate" style="float:left;width:150px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Loss and Damage" data-datafield="DisableEditingLossAndDamageRate" style="float:left;width:150px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Is Manual Sort" data-datafield="IsManualSort"></div>                       
                        </div>
                      </div>
                      <!-- Activity section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Activity">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield activity" data-caption="Combine Activity" data-datafield="CombineActivity" style="display:none"></div>
                        </div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield anti-LD activity" data-caption="Rental" data-datafield="Rental" style="flex:1 1 100px;"></div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield anti-LD activity" data-caption="Sales" data-datafield="Sales" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield activity" data-caption="Miscellaneous" data-datafield="Miscellaneous" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield activity" data-caption="Labor" data-datafield="Labor" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield anti-LD activity" data-caption="Used Sale" data-datafield="RentalSale" style="flex:1 1 100px;"></div>
                        </div>
                        <!--
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield activity" data-caption="Facilities" data-datafield="Facilities" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield activity" data-caption="Transportation" data-datafield="Transportation" style="flex:1 1 150px;"></div>
                        </div>
                        -->
                        <div class="flexrow" style="display:none;">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasRentalItem" data-datafield="HasRentalItem" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasSalesItem" data-datafield="HasSalesItem" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasMiscellaneousItem" data-datafield="HasMiscellaneousItem" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasLaborItem" data-datafield="HasLaborItem" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasLossAndDamageItem" data-datafield="HasLossAndDamageItem" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasRentalSaleItem" data-datafield="HasRentalSaleItem" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasNotes" data-datafield="HasNotes" style="flex:1 1 100px;"></div>
                        </div>
                      </div>
                    </div>

                    <!-- Dates & Times section -->
                    <div class="flexcolumn" style="flex:1 1 300px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Schedule">
                        <div class="flexrow schedule-date-fields">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield pick-date-validation og-datetime" data-caption="Pick Date" data-dateactivitytype="PICK" data-datafield="PickDate" style="flex:1 1 115px;"></div>
                          <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="fwcontrol fwformfield og-datetime" data-caption="Pick Time" data-timeactivitytype="PICK" data-datafield="PickTime" style="flex:1 1 84px;"></div>
                        </div>
                        <div class="flexrow schedule-date-fields">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield pick-date-validation og-datetime" data-caption="From Date" data-dateactivitytype="START" data-datafield="EstimatedStartDate" style="flex:1 1 115px;"></div>
                          <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="fwcontrol fwformfield og-datetime" data-caption="From Time" data-timeactivitytype="START" data-datafield="EstimatedStartTime" style="flex:1 1 84px;"></div>
                        </div>
                        <div class="flexrow schedule-date-fields">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield pick-date-validation og-datetime" data-caption="To Date" data-dateactivitytype="STOP" data-datafield="EstimatedStopDate" style="flex:1 1 115px;"></div>
                          <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="fwcontrol fwformfield og-datetime" data-caption="To Time" data-timeactivitytype="STOP" data-datafield="EstimatedStopTime" style="flex:1 1 84px;"></div>
                        </div>
                        <div class="activity-dates" style="display:none;"></div>
                        <!--<div class="activity-dates-toggle"></div>-->
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section itemsection" data-control="FwContainer" data-type="section" data-caption="Documents">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Cover Letter" data-datafield="CoverLetterId" data-displayfield="CoverLetter" data-enabled="true" data-validationname="CoverLetterValidation" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Terms &#038; Conditions" data-datafield="TermsConditionsId" data-displayfield="TermsConditions" data-enabled="true" data-validationname="TermsConditionsValidation" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Presentation Layer" data-datafield="PresentationLayerId" data-displayfield="PresentationLayer" data-enabled="true" data-validationname="PresentationLayerValidation" style="flex:1 1 225px;"></div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Market">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Type" data-datafield="MarketTypeId" data-displayfield="MarketType" data-validationpeek="true" data-validationname="MarketTypeValidation" style="flex:1 1 150px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Segment" data-datafield="MarketSegmentId" data-displayfield="MarketSegment" data-validationpeek="true" data-formbeforevalidate="beforeValidateMarketSegment" data-validationname="MarketSegmentValidation" style="flex:1 1 150px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Job" data-datafield="MarketSegmentJobId" data-displayfield="MarketSegmentJob" data-validationpeek="true" data-formbeforevalidate="beforeValidateMarketSegment" data-validationname="MarketSegmentJobValidation" style="flex:1 1 150px;"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Status section -->
                    <div class="flexcolumn" style="flex:1 1 250px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Status">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Status" data-datafield="Status" data-enabled="false" style="flex:1 0 115px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="As of" data-datafield="StatusDate" data-enabled="false" style="flex:1 0 115px;"></div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="PO">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Pending" data-datafield="PendingPo" style="flex:1 1 90px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Flat PO" data-datafield="FlatPo" style="flex:1 1 90px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="PO No." data-datafield="PoNumber" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="PO Amount" data-datafield="PoAmount" style="flex:1 1 100px;"></div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Personnel">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Agent" data-datafield="AgentId" data-displayfield="Agent" data-enabled="true" data-required="true" data-validationname="UserValidation" style="flex:1 1 185px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Project Manager" data-datafield="ProjectManagerId" data-displayfield="ProjectManager" data-enabled="true" data-required="false" data-validationname="UserValidation" style="flex:1 1 185px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Outside Sales Representative" data-datafield="OutsideSalesRepresentativeId" data-displayfield="OutsideSalesRepresentative" data-enabled="true" data-validationname="ContactValidation" style="flex:1 1 185px;"></div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Location">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" data-enabled="false" style="flex:1 1 150px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" data-enabled="false" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="WarehouseCode" data-datafield="WarehouseCode" data-formreadonly="true" data-enabled="false" style="display:none"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- P&L TAB -->
              <div data-type="tabpage" id="profitlosstabpage" class="tabpage" data-tabid="profitlosstab" data-render="false">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Profit &amp; Loss Summary">
                  <div class="wideflexrow">
                     <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield profit-loss-total" data-caption="View" data-datafield="totalTypeProfitLoss" style="flex:0 1 275px;"></div>
                  </div>
                  <div class="wideflexrow">
                    <!-- Profitability Summary section -->
                    <div class="flexcolumn" style="flex:0 1 200px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Total" data-datafield="" data-framedatafield="TotalPrice"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Discount" data-datafield="" data-framedatafield="TotalDiscount"></div>
                        </div>
                        <div class="flexrow totalColors">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Sub-Total" data-datafield="" data-framedatafield="TotalTotal"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Cost" data-datafield="" data-framedatafield="TotalCost"></div>
                        </div>
                        <div class="flexrow profitframes" >
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Profit" data-datafield="" data-framedatafield="TotalProfit"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Markup" data-datafield="" data-framedatafield="TotalMarkup"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Margin" data-datafield="" data-framedatafield="TotalMargin"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Rental Profitability -->
                    <div class="flexcolumn rental-pl" style="flex:0 1 200px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Rentals" style="color:#">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Total" data-datafield="" data-framedatafield="RentalPrice"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Discount" data-datafield="" data-framedatafield="RentalDiscount"></div>
                        </div>
                        <div class="flexrow totalColors">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Sub-Total" data-datafield="" data-framedatafield="RentalTotal"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Cost" data-datafield="" data-framedatafield="RentalCost"></div>
                        </div>
                        <div class="flexrow profitframes">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Profit" data-datafield="" data-framedatafield="RentalProfit"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Markup" data-datafield="" data-framedatafield="RentalMarkup"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Margin" data-datafield="" data-framedatafield="RentalMargin"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Sales Profitability -->
                    <div class="flexcolumn sales-pl" style="flex:0 1 200px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sales">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Total" data-datafield="" data-framedatafield="SalesPrice"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Discount" data-datafield="" data-framedatafield="SalesDiscount"></div>
                        </div>
                        <div class="flexrow totalColors">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Sub-Total" data-datafield="" data-framedatafield="SalesTotal"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Cost" data-datafield="" data-framedatafield="SalesCost"></div>
                        </div>
                        <div class="flexrow profitframes">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Profit" data-datafield="" data-framedatafield="SalesProfit"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Markup" data-datafield="" data-framedatafield="SalesMarkup"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Margin" data-datafield="" data-framedatafield="SalesMargin"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Facilities Profitability 
                    <div class="flexcolumn" style="flex:0 1 200px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Facilities">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Total" data-datafield="" data-framedatafield="FacilitiesPrice"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Discount" data-datafield="" data-framedatafield="FacilitiesDiscount"></div>
                        </div>
                        <div class="flexrow totalColors">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Sub-Total" data-datafield="" data-framedatafield="FacilitiesTotal"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Cost" data-datafield="" data-framedatafield="FacilitiesCost"></div>
                        </div>
                        <div class="flexrow profitframes">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Profit" data-datafield="" data-framedatafield="FacilitiesProfit"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Markup" data-datafield="" data-framedatafield="FacilitiesMarkup"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Margin" data-datafield="" data-framedatafield="FacilitiesMargin"></div>
                        </div>
                      </div>
                    </div>
                    -->
                    <!-- Transportation Profitability
                    <div class="flexcolumn" style="flex:0 1 200px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Transportation">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Total" data-datafield="" data-framedatafield="TransportationPrice"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Discount" data-datafield="" data-framedatafield="TransportationDiscount"></div>
                        </div>
                        <div class="flexrow totalColors">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Sub-Total" data-datafield="" data-framedatafield="TransportationTotal"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Cost" data-datafield="" data-framedatafield="TransportationCost"></div>
                        </div>
                        <div class="flexrow profitframes">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Profit" data-datafield="" data-framedatafield="TransportationProfit"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Markup" data-datafield="" data-framedatafield="TransportationMarkup"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Margin" data-datafield="" data-framedatafield="TransportationMargin"></div>
                        </div>
                      </div>
                    </div> 
                    -->
                    <!-- Labor Profitability -->
                    <div class="flexcolumn labor-pl" style="flex:0 1 200px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Labor">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Total" data-datafield="" data-framedatafield="LaborPrice"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Discount" data-datafield="" data-framedatafield="LaborDiscount"></div>
                        </div>
                        <div class="flexrow totalColors">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Sub-Total" data-datafield="" data-framedatafield="LaborTotal"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Cost" data-datafield="" data-framedatafield="LaborCost"></div>
                        </div>
                        <div class="flexrow profitframes">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Profit" data-datafield="" data-framedatafield="LaborProfit"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Markup" data-datafield="" data-framedatafield="LaborMarkup"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Margin" data-datafield="" data-framedatafield="LaborMargin"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Miscellaneous Profitability -->
                    <div class="flexcolumn misc-pl" style="flex:0 1 200px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Miscellaneous">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Total" data-datafield="" data-framedatafield="MiscPrice"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Discount" data-datafield="" data-framedatafield="MiscDiscount"></div>
                        </div>
                        <div class="flexrow totalColors">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Sub-Total" data-datafield="" data-framedatafield="MiscTotal"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Cost" data-datafield="" data-framedatafield="MiscCost"></div>
                        </div>
                        <div class="flexrow profitframes">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Profit" data-datafield="" data-framedatafield="MiscProfit"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Markup" data-datafield="" data-framedatafield="MiscMarkup"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Margin" data-datafield="" data-framedatafield="MiscMargin"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Used Sale Profitability -->
                    <div class="flexcolumn usedsale-pl" style="flex:0 1 200px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Used Sales">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Total" data-datafield="" data-framedatafield="RentalSalePrice"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Discount" data-datafield="" data-framedatafield="RentalSaleDiscount"></div>
                        </div>
                        <div class="flexrow totalColors">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Sub-Total" data-datafield="" data-framedatafield="RentalSaleTotal"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Cost" data-datafield="" data-framedatafield="RentalSaleCost"></div>
                        </div>
                        <div class="flexrow profitframes">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Profit" data-datafield="" data-framedatafield="RentalSaleProfit"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Markup" data-datafield="" data-framedatafield="RentalSaleMarkup"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Margin" data-datafield="" data-framedatafield="RentalSaleMargin"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Parts Profitability 
                    <div class="flexcolumn" style="flex:0 1 200px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Parts">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Total" data-datafield="" data-framedatafield="PartsPrice"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Discount" data-datafield="" data-framedatafield="PartsDiscount"></div>
                        </div>
                        <div class="flexrow totalColors">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Sub-Total" data-datafield="" data-framedatafield="PartsTotal"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Cost" data-datafield="" data-framedatafield="PartsCost"></div>
                        </div>
                        <div class="flexrow profitframes">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Gross Profit" data-datafield="" data-framedatafield="PartsProfit"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Markup" data-datafield="" data-framedatafield="PartsMarkup"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield frame" data-caption="Margin" data-datafield="" data-framedatafield="PartsMargin"></div>
                        </div>
                      </div>
                    </div>
                    -->
                    <!-- Sales Tax -->
                    <div class="flexcolumn salestax-pl" style="flex:0 1 200px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sales Tax">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="" style="visibility:hidden;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="" data-datafield="" data-framedatafield="" style="visibility:hidden"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Total" data-datafield="" data-framedatafield="TotalTax"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield frame" data-caption="Cost" data-datafield="" data-framedatafield="TaxCost"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Activity Summary">
                  <div class="flexrow">
                    <div class="rwGrid" data-control="FwGrid" data-grid="OrderActivitySummaryGrid" data-securitycaption="Activity Summary"></div>
                  </div>
                </div>
              </div>

              <!-- CONTACTS TAB -->
              <div data-type="tabpage" id="contactstabpage" class="tabpage" data-tabid="contactstab">
                <div class="flexrow">
                  <div data-control="FwGrid" data-grid="OrderContactGrid" data-securitycaption="Contacts"></div>
                </div>
              </div>

            <!-- ACTIVITY TAB -->
              <div data-type="tabpage" id="activitytabpage" class="tabpage" data-tabid="activitytab">
                <div class="wideflexrow">
                    <div class="flexcolumn">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quote Activities">
                            <div class="rwGrid" data-control="FwGrid" data-grid="ActivityGrid" data-securitycaption="Activity"></div>
                        </div>
                    </div>
                    <div class="flexcolumn" style="flex:0 0 0;">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filter Activities">
                            <div data-control="FwFormField" data-type="date" class="activity-filters fwcontrol fwformfield" data-caption="From" data-datafield="ActivityFromDate"></div>
                            <div data-control="FwFormField" data-type="date" class="activity-filters fwcontrol fwformfield" data-caption="To" data-datafield="ActivityToDate"></div>
                            <div data-control="FwFormField" data-type="multiselectvalidation" class="activity-filters fwcontrol fwformfield" data-caption="Activity" data-datafield="ActivityTypeId" data-validationname="ActivityTypeValidation"></div>
                            <div data-control="FwFormField" data-type="checkbox" class="activity-filters fwcontrol fwformfield" data-caption="Show Shipping Activities" data-datafield="ShowShipping"></div>
                            <div data-control="FwFormField" data-type="checkbox" class="activity-filters fwcontrol fwformfield" data-caption="Show Sub-PO Activities" data-datafield="ShowSubPo"></div>
                            <div data-control="FwFormField" data-type="checkbox" class="activity-filters fwcontrol fwformfield" data-caption="Show Complete Activities" data-datafield="ShowComplete"></div>
                        </div>
                    </div>
                </div>
              </div>

              <!-- RENTAL TAB -->
              <div data-type="tabpage" id="rentaltabpage" class="rentalgrid notcombined tabpage" data-tabid="rentaltab">
                <div class="formpage">
                  <div class="wideflexrow">
                    <div class="flexcolumn" style="flex:1 1 625px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Rental Items">
                        <div class="wideflexrow">
                          <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Rental Items"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:0 0 auto;">
                    <div class="flexcolumn summarySection" style="flex:0 0 200px;padding-right:10px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Rental Totals">
                        <div class="flexrow rentaltotals">
                          <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield totals totalType" data-gridtype="rental" data-caption="View" data-datafield="totalTypeRental" style="flex:1 1 150px;"></div>
                        </div>
                        <div class="flexrow rentaltotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow rentaltotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow rentaltotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow rentaltotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow rentaltotals">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 125px;"></div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Rental Adjustments">
                        <div class="flexrow rentalAdjustments">
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield totals RentalDaysPerWeek" data-caption="D/W" data-datafield="RentalDaysPerWeek" data-digits="3" data-digitsoptional="false" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow rentalAdjustments">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield bottom_line_discount" data-caption="Disc. %" data-rectype="R" data-datafield="RentalDiscountPercent" data-digits="2" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow rentalAdjustments">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals rentalOrderItemTotal bottom_line_total_tax rentalAdjustmentsPeriod" data-caption="Total" data-rectype="R" data-datafield="PeriodRentalTotal" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow rentalAdjustments">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield rentalTotalWithTax bottom_line_total_tax rentalAdjustmentsPeriod" data-caption="Include Tax in Total" data-rectype="R" data-datafield="PeriodRentalTotalIncludesTax" style="flex:1 1 70px;margin-top:10px;"></div>                      </div>
                        <div class="flexrow rentalAdjustments">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals rentalOrderItemTotal bottom_line_total_tax rentalAdjustmentsWeekly" data-caption="Total" data-rectype="R" data-datafield="WeeklyRentalTotal" style="flex:1 1 125px; display:none;"></div>
                        </div>
                        <div class="flexrow rentalAdjustments">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield rentalTotalWithTax bottom_line_total_tax rentalAdjustmentsWeekly" data-caption="Include Tax in Total" data-rectype="R" data-datafield="WeeklyRentalTotalIncludesTax" style="flex:1 1 125px;margin-top:10px; display:none;"></div>
                        </div>
                        <div class="flexrow rentalAdjustments">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals rentalOrderItemTotal bottom_line_total_tax rentalAdjustmentsMonthly" data-caption="Total" data-rectype="R" data-datafield="MonthlyRentalTotal" style="flex:1 1 125px; display:none;"></div>
                        </div>
                        <div class="flexrow rentalAdjustments">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield rentalTotalWithTax bottom_line_total_tax rentalAdjustmentsMonthly" data-caption="Include Tax in Total" data-rectype="R" data-datafield="MonthlyRentalTotalIncludesTax" style="flex:1 1 125px;margin-top:10px; display:none;"></div>
                        </div>
                      </div>
                    </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- SALES TAB -->
              <div data-type="tabpage" id="salestabpage" class="salesgrid notcombined tabpage" data-tabid="salestab" data-render="false">
                <div class="wideflexrow">
                  <div class="flexcolumn" style="flex:1 1 625px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sales Items">
                      <div class="wide-flexrow">
                        <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Sales Items"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:0 0 auto;">
                  <div class="flexcolumn summarySection" style="flex:0 0 200px;padding-right:10px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sales Totals">
                      <div class="flexrow salestotals">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow salestotals">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow salestotals">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow salestotals">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                      </div>
                      <div class="flexrow salestotals">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sales Adjustments">
                      <div class="flexrow salesAdjustments">
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom_line_discount" data-caption="Disc. %" data-rectype="S" data-datafield="SalesDiscountPercent" style="flex:1 1 50px;"></div>
                      </div>
                      <div class="flexrow salesAdjustments">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals salesOrderItemTotal bottom_line_total_tax" data-caption="Total" data-rectype="S" data-datafield="SalesTotal" style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow salesAdjustments">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield salesTotalWithTax bottom_line_total_tax" data-caption="Include Tax in Total" data-rectype="S" data-datafield="SalesTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                      </div>
                    </div>
                  </div>
                  </div>
                </div>
              </div>

              <!-- LABOR TAB -->
              <div data-type="tabpage" id="labortabpage" class="laborgrid notcombined tabpage" data-tabid="labortab" data-render="false">
                <div class="wideflexrow">
                  <div class="flexcolumn" style="flex:1 1 625px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Labor Items">
                      <div class="wide-flexrow">
                        <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Labor Items"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:0 0 auto;">
                  <div class="flexcolumn labortotals laboradjustments summarySection" style="flex:0 0 200px;padding-right:10px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Labor Totals">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield totals totalType" data-gridtype="labor" data-caption="View" data-datafield="totalTypeLabor" style="flex:1 1 150px;"></div>
                      </div>
                      <div class="flexrow labortotals">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:2 1 100px;"></div>
                      </div>
                      <div class="flexrow labortotals">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow labortotals">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:2 1 100px;"></div>
                      </div>
                      <div class="flexrow labortotals">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                      </div>
                      <div class="flexrow labortotals">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:2 1 100px;"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Labor Adjustments">
                      <div class="flexrow laborAdjustments">
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom_line_discount" data-caption="Disc. %" data-rectype="L" data-datafield="LaborDiscountPercent" style="flex:1 1 50px;"></div>
                      </div>
                      <div class="flexrow laborAdjustments">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals laborOrderItemTotal bottom_line_total_tax laborAdjustmentsPeriod" data-caption="Total" data-rectype="L" data-datafield="PeriodLaborTotal" style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow laborAdjustments">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield laborTotalWithTax bottom_line_total_tax laborAdjustmentsPeriod" data-caption="Include Tax in Total" data-rectype="L" data-datafield="PeriodLaborTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                      </div>
                      <div class="flexrow laborAdjustments">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals laborOrderItemTotal bottom_line_total_tax laborAdjustmentsWeekly" data-caption="Total" data-rectype="L" data-datafield="WeeklyLaborTotal" style="flex:1 1 100px; display:none;"></div>
                      </div>
                      <div class="flexrow laborAdjustments">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield laborTotalWithTax bottom_line_total_tax laborAdjustmentsWeekly" data-caption="Include Tax in Total" data-rectype="L" data-datafield="WeeklyLaborTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                      </div>
                      <div class="flexrow laborAdjustments">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals laborOrderItemTotal bottom_line_total_tax laborAdjustmentsMonthly" data-caption="Total" data-rectype="L" data-datafield="MonthlyLaborTotal" style="flex:1 1 100px; display:none;"></div>
                      </div>
                      <div class="flexrow laborAdjustments">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield laborTotalWithTax bottom_line_total_tax laborAdjustmentsMonthly" data-caption="Include Tax in Total" data-rectype="L" data-datafield="MonthlyLaborTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                      </div>
                    </div>
                  </div>
                  </div>
                </div>
              </div>

              <!-- MISC TAB -->
              <div data-type="tabpage" id="misctabpage" class="miscgrid notcombined tabpage" data-tabid="misctab" data-render="false">
                <div class="wideflexrow">
                  <div class="flexcolumn" style="flex:1 1 625px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Miscellaneous Items">
                      <div class="wide-flexrow">
                        <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Miscellaneous Items"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:0 0 auto;">
                  <div class="flexcolumn summarySection" style="flex:0 0 200px;padding-right:10px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Misc Totals">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield totals totalType" data-gridtype="misc" data-caption="View" data-datafield="totalTypeMisc" style="flex:1 1 150px;"></div>
                      </div>
                      <div class="flexrow misctotals">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow misctotals">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow misctotals">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow misctotals">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                      </div>
                      <div class="flexrow misctotals">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Misc Adjustments">
                      <div class="flexrow miscAdjustments">
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom_line_discount" data-caption="Disc. %" data-rectype="M" data-datafield="MiscDiscountPercent" style="flex:1 1 50px;"></div>
                      </div>
                      <div class="flexrow miscAdjustments">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals miscOrderItemTotal bottom_line_total_tax miscAdjustmentsPeriod" data-caption="Total" data-rectype="M" data-datafield="PeriodMiscTotal" style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow miscAdjustments">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield miscTotalWithTax bottom_line_total_tax miscAdjustmentsPeriod" data-caption="Include Tax in Total" data-rectype="M" data-datafield="PeriodMiscTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                      </div>
                      <div class="flexrow miscAdjustments">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals miscOrderItemTotal bottom_line_total_tax miscAdjustmentsWeekly" data-caption="Total" data-rectype="M" data-datafield="WeeklyMiscTotal" style="flex:1 1 100px; display:none;"></div>
                      </div>
                      <div class="flexrow miscAdjustments">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield miscTotalWithTax bottom_line_total_tax miscAdjustmentsWeekly" data-caption="Include Tax in Total" data-rectype="M" data-datafield="WeeklyMiscTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                      </div>
                      <div class="flexrow miscAdjustments">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals miscOrderItemTotal bottom_line_total_tax miscAdjustmentsMonthly" data-caption="Total" data-rectype="M" data-datafield="MonthlyMiscTotal" style="flex:1 1 100px; display:none;"></div>
                      </div>
                      <div class="flexrow miscAdjustments">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield miscTotalWithTax bottom_line_total_tax miscAdjustmentsMonthly" data-caption="Include Tax in Total" data-rectype="M" data-datafield="MonthlyMiscTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                      </div>
                    </div>
                  </div>
                  </div>
                </div>
              </div>

              <!-- USED SALE TAB -->
              <div data-type="tabpage" id="usedsaletabpage" class="usedsalegrid notcombined tabpage" data-tabid="usedsaletab" data-render="false">
                <div class="wideflexrow">
                  <div class="flexcolumn" style="flex:1 1 625px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Used Sale Items">
                      <div class="wideflexrow">
                        <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Used Sale Items"></div>
                      </div>
                    </div>                  
                  </div>
                  <div class="flexcolumn" style="flex:0 0 auto;">
                  <div class="flexcolumn usedsaletotals usedsaleadjustments summarySection" style="flex:0 0 200px;padding-right:10px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Used Sales Totals">
                      <div class="flexrow usedsaletotals">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:2 1 175px;"></div>
                      </div>
                      <div class="flexrow usedsaletotals">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 175px;"></div>
                      </div>
                      <div class="flexrow usedsaletotals">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:2 1 175px;"></div>
                      </div>
                      <div class="flexrow usedsaletotals">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 175px;"></div>
                      </div>
                      <div class="flexrow usedsaletotals">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:2 1 175px;"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Used Sales Adjustments">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom_line_discount" data-caption="Disc. %" data-rectype="RS" data-datafield="UsedSaleDiscountPercent" style="flex:1 1 175px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals lossOrderItemTotal bottom_line_total_tax" data-caption="Total" data-rectype="RS" data-datafield="UsedSaleTotal" style="flex:1 1 175px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield lossTotalWithTax bottom_line_total_tax" data-caption="Include Tax in Total" data-rectype="RS" data-datafield="UsedSaleTotalIncludesTax" style="flex:1 1 175px;margin-top:10px;"></div>
                      </div>
                    </div>
                  </div>
                  </div>
                </div>
              </div>

              <!-- ALL TAB -->
              <div data-type="tabpage" id="alltabpage" class="combinedgrid combined tabpage" data-tabid="alltab" data-render="false">
                <div class="wideflexrow">
                  <div class="flexcolumn" style="flex:1 1 625px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Items">
                      <div class="wideflexrow">
                        <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Order Items"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:0 0 auto;">
                  <div class="flexcolumn summarySection" style="flex:0 0 200px;padding-right:10px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Totals">
                      <div class="flexrow combinedAdjustments">
                        <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield totals totalType" data-caption="View" data-datafield="totalTypeAll" style="flex:1 1 150px;"></div>
                      </div>
                      <div class="flexrow combinedAdjustments">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow combinedAdjustments">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow combinedAdjustments">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow combinedAdjustments">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                      </div>
                      <div class="flexrow combinedAdjustments">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Adjustments">
                      <div class="flexrow combinedtotals">
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield combineddw" data-caption="D/W" data-datafield="CombinedDaysPerWeek" data-digits="3" data-digitsoptional="false" style="flex:1 1 50px;display:none;" data-enabled="true"></div>
                      </div>
                      <div class="flexrow combinedtotals">
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom_line_discount" data-caption="Disc. %" data-rectype="" data-datafield="CombinedDiscountPercent" style="flex:1 1 50px;"></div>
                      </div>
                      <div class="flexrow combinedtotals">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals combinedOrderItemTotal bottom_line_total_tax combinedAdjustmentsPeriod" data-caption="Total" data-rectype="" data-datafield="PeriodCombinedTotal" style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow combinedtotals">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield combinedTotalWithTax bottom_line_total_tax combinedAdjustmentsPeriod" data-caption="w/ Tax" data-rectype="" data-datafield="PeriodCombinedTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                      </div>
                      <div class="flexrow combinedtotals">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals combinedOrderItemTotal bottom_line_total_tax combinedAdjustmentsWeekly" data-caption="Total" data-rectype="" data-datafield="WeeklyCombinedTotal" style="flex:1 1 100px; display:none;"></div>
                      </div>
                      <div class="flexrow combinedtotals">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield combinedTotalWithTax bottom_line_total_tax combinedAdjustmentsWeekly" data-caption="w/ Tax" data-rectype="" data-datafield="WeeklyCombinedTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                      </div>
                      <div class="flexrow combinedtotals">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals combinedOrderItemTotal bottom_line_total_tax combinedAdjustmentsMonthly" data-caption="Total" data-rectype="" data-datafield="MonthlyCombinedTotal" style="flex:1 1 100px; display:none;"></div>
                      </div>
                      <div class="flexrow combinedtotals">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield combinedTotalWithTax bottom_line_total_tax combinedAdjustmentsMonthly" data-caption="w/ Tax" data-rectype="" data-datafield="MonthlyCombinedTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                      </div>
                    </div>
                  </div>
                  </div>
                </div>
              </div>

              <!-- BILLING TAB PAGE -->
              <div data-type="tabpage" id="billingtabpage" class="tabpage" data-tabid="billingtab">
                <div class="flexrow">
                  <!-- Left column -->
                  <div class="flexcolumn" style="flex:1 1 325px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billing">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield billing_start_date date-types-disable" data-caption="Start" data-datafield="BillingStartDate" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield billing_end_date date-types-disable" data-caption="Stop" data-datafield="BillingEndDate" style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield BillingWeeks week_or_month_field date-types-disable" data-caption="Weeks" data-datafield="BillingWeeks" style="flex:1 1 50px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield BillingMonths week_or_month_field date-types-disable" data-caption="Months" data-datafield="BillingMonths" style="flex:1 1 50px;"></div>
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Delay Billing Search Until" data-datafield="DelayBillingSearchUntil" style="flex:1 1 100px;"></div>          
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Lock Billing Dates" data-datafield="LockBillingDates" style="flex:1 1 150px;margin-top:10px;"></div>
                      </div>
                     <div class="flexrow">
                         <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Specify Billing Dates by Type" data-datafield="SpecifyBillingDatesByType"></div>
                     </div>
                    <div class="flexrow date-types" style="display:none;">
                      <div class="flexcolumn">
                          <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Rental Billing Period">
                              <div class="flexrow">                                    
                                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-enabled="false" data-caption="Start" data-datafield="RentalBillingStartDate"></div>
                                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-enabled="false" data-caption="Stop" data-datafield="RentalBillingEndDate"></div>
                              </div> 
                          </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Facilities Billing Period">
                          <div class="flexrow">                                    
                              <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-enabled="false" data-caption="Start" data-datafield="FacilitiesBillingStartDate"></div>
                              <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-enabled="false" data-caption="Stop" data-datafield="FacilitiesBillingEndDate"></div>
                          </div> 
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Vehicle Billing Period">
                          <div class="flexrow">                                    
                              <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-enabled="false" data-caption="Start" data-datafield="VehicleBillingStartDate"></div>
                              <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-enabled="false" data-caption="Stop" data-datafield="VehicleBillingEndDate"></div>
                          </div> 
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Crew Billing Period">
                          <div class="flexrow">                                    
                              <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-enabled="false" data-caption="Start" data-datafield="LaborBillingStartDate"></div>
                              <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-enabled="false" data-caption="Stop" data-datafield="LaborBillingEndDate"></div>
                          </div> 
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Misc Billing Period">
                          <div class="flexrow">                                    
                              <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-enabled="false" data-caption="Start" data-datafield="MiscellaneousBillingStartDate"></div>
                              <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-enabled="false" data-caption="Stop" data-datafield="MiscellaneousBillingEndDate"></div>
                          </div> 
                      </div>
                     </div>
                    </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" data-validationname="BillingCycleValidation" class="fwcontrol fwformfield" data-caption="Billing Cycle" data-datafield="BillingCycleId" data-displayfield="BillingCycle" style="flex:1 1 250px;" data-required="true"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" data-validationname="PaymentTermsValidation" class="fwcontrol fwformfield" data-caption="Payment Terms" data-datafield="PaymentTermsId" data-displayfield="PaymentTerms" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" data-validationname="PaymentTypeValidation" class="fwcontrol fwformfield" data-caption="Pay Type" data-datafield="PaymentTypeId" data-displayfield="PaymentType" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" data-validationname="CurrencyValidation" class="fwcontrol fwformfield" data-caption="Currency Code" data-datafield="CurrencyId" data-displayfield="CurrencyCode" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" data-validationname="TaxOptionValidation" class="fwcontrol fwformfield" data-caption="Tax Option" data-datafield="TaxOptionId" data-displayfield="TaxOption" style="flex:1 1 250px"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="percent" data-digits="4" class="fwcontrol fwformfield" data-caption="Rental Tax" data-datafield="RentalTaxRate1" data-enabled="false" style="flex:1 1 75px;"></div>
                        <div data-control="FwFormField" data-type="percent" data-digits="4" class="fwcontrol fwformfield" data-caption="Sales Tax" data-datafield="SalesTaxRate1" data-enabled="false" style="flex:1 1 75px;"></div>
                        <div data-control="FwFormField" data-type="percent" data-digits="4" class="fwcontrol fwformfield" data-caption="Labor Tax" data-datafield="LaborTaxRate1" data-enabled="false" style="flex:1 1 75px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="Bill Quantities From" data-datafield="DetermineQuantitiesToBillBasedOn"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield totals" data-caption="Add Prep Fees" data-datafield="IncludePrepFeesInRentalRate" style="flex:1 1 150px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield  billing-type-radio" data-caption="Use Hiatus Schedule From" data-datafield="HiatusDiscountFrom"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Discount Reason" data-datafield="DiscountReasonId" data-displayfield="DiscountReason" data-validationname="DiscountReasonValidation" style="flex:1 1 250px; float:left;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Add to Group" data-datafield="InGroup" style="flex:1 1 125px;margin-top:10px;"></div>
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Group No" data-datafield="GroupNumber" style="flex:1 1 125px;"></div>
                      </div>
                    </div>
                  </div>
                  <!-- Center column -->
                  <div class="flexcolumn" style="flex:2 1 325px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quote Issued To">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield  billing-type-radio" data-caption="Issue To" data-datafield="PrintIssuedToAddressFrom"></div>
                      </div>
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
                        <!--<div data-control="FwFormField" data-type="validation" data-validationname="StateValidation" class="fwcontrol fwformfield" data-caption="State/Province" data-datafield="IssuedToState" style="flex:1 1 100px;"></div>-->
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State" data-datafield="IssuedToState" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="IssuedToZipCode" style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" data-validationname="CountryValidation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="IssuedToCountryId" data-displayfield="IssuedToCountry" style="flex:1 1 250px;"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billing Options">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Return Items without increasing Order quantity" data-datafield="RoundTripRentals"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Require Contact Confirmation" data-datafield="RequireContactConfirmation" style="flex:1 1 125px;"></div>
                      </div>
                    </div>
                  </div>
                  <!-- Right Column -->
                  <div class="flexcolumn" style="flex:2 1 275px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Bill To">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Use Other Billing Address" data-datafield="BillToAddressDifferentFromIssuedToAddress" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="Name" data-datafield="BillToName" data-enabled="false" style="flex:1 1 250px;disp"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="Attention" data-datafield="BillToAttention" data-enabled="false" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="Attention 2" data-datafield="BillToAttention2" data-enabled="false" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="Address" data-datafield="BillToAddress1" data-enabled="false" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="Address 2" data-datafield="BillToAddress2" data-enabled="false" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="City" data-datafield="BillToCity" data-enabled="false" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <!--<div data-control="FwFormField" data-type="validation" data-validationname="StateValidation" class="differentaddress fwcontrol fwformfield" data-caption="State/Province" data-datafield="BillToState" data-enabled="false" style="flex:1 1 100px;"></div>-->
                        <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="State" data-datafield="BillToState" data-enabled="false" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="BillToZipCode" data-enabled="false" style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" data-validationname="CountryValidation" class="differentaddress fwcontrol fwformfield" data-caption="Country" data-datafield="BillToCountryId" data-displayfield="BillToCountry" data-enabled="false" style="flex:1 1 250px;"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="No Charge Order">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="No Charge Order" data-datafield="NoCharge" style="flex:1 1 75px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="No Charge Reason" data-datafield="NoChargeReason" style="flex:1 1 300px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- DELIVER/SHIP TAB -->
              <div data-type="tabpage" id="delivershiptabpage" class="tabpage" data-tabid="delivershiptab">
                <div class="flexpage">
                  <div class="flexrow">
                    <!-- Outgoing section -->
                    <div class="flexcolumn" style="flex:1 1 425px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Outgoing">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield outtype delivery-delivery" data-caption="Type" data-datafield="OutDeliveryDeliveryType" style="flex:1 1 150px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="On Date" data-datafield="OutDeliveryTargetShipDate" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="time" data-timeformat="24" class="fwcontrol fwformfield" data-caption="On Time" data-datafield="OutDeliveryTargetShipTime" style="flex:1 1 75px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Required By" data-datafield="OutDeliveryRequiredDate" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="time" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Required Time" data-datafield="OutDeliveryRequiredTime" style="flex:1 1 75px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Contact" data-datafield="OutDeliveryToContact" style="flex:1 1 250px;"></div>
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Phone" data-datafield="OutDeliveryToContactPhone" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" data-validationname="VendorValidation" class="fwcontrol fwformfield" data-caption="Carrier" data-datafield="OutDeliveryCarrierId" data-displayfield="OutDeliveryCarrier" style="flex:1 1 200px;"></div>
                          <div data-control="FwFormField" data-type="validation" data-validationname="ShipViaValidation" class="fwcontrol fwformfield" data-caption="Ship Via" data-datafield="OutDeliveryShipViaId" data-displayfield="OutDeliveryShipVia" style="flex:1 1 200px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Tracking URL" data-datafield="OutDeliveryFreightTrackingUrl" data-allcaps="false" style="display:none;flex:1 1 200px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Tracking Number" data-datafield="OutDeliveryFreightTrackingNumber" data-allcaps="false" style="flex:1 1 200px;"></div>
                          <div class="fwformcontrol track-shipment-out" data-type="button" data-enabled="false" style="flex:1 1 150px;margin:16px 10px 0px 5px;text-align:center;">Track Shipment</div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Outgoing Address">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield delivery-type-radio" data-caption="" data-datafield="OutDeliveryAddressType"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Location" data-datafield="OutDeliveryToLocation"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention" data-datafield="OutDeliveryToAttention"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 1" data-datafield="OutDeliveryToAddress1"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 2" data-datafield="OutDeliveryToAddress2"></div>
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
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Online Order">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Order No" data-datafield="OutDeliveryOnlineOrderNumber"></div>
                          <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield online" data-caption="Status" data-datafield="OutDeliveryOnlineOrderStatus"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Incoming section -->
                    <div class="flexcolumn" style="flex:1 1 425px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Incoming">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield intype delivery-delivery" data-caption="Type" data-datafield="InDeliveryDeliveryType" style="flex:1 1 150px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="On Date" data-datafield="InDeliveryTargetShipDate" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="time" data-timeformat="24" class="fwcontrol fwformfield" data-caption="On Time" data-datafield="InDeliveryTargetShipTime" style="flex:1 1 75px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Required By" data-datafield="InDeliveryRequiredDate" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="time" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Required Time" data-datafield="InDeliveryRequiredTime" style="flex:1 1 75px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Contact" data-datafield="InDeliveryToContact" style="flex:1 1 250px;"></div>
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Phone" data-datafield="InDeliveryToContactPhone" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" data-validationname="VendorValidation" class="fwcontrol fwformfield" data-caption="Carrier" data-datafield="InDeliveryCarrierId" data-displayfield="InDeliveryCarrier" style="flex:1 1 200px;"></div>
                          <div data-control="FwFormField" data-type="validation" data-validationname="ShipViaValidation" class="fwcontrol fwformfield" data-caption="Ship Via" data-datafield="InDeliveryShipViaId" data-displayfield="InDeliveryShipVia" style="flex:1 1 200px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Tracking URL" data-datafield="InDeliveryFreightTrackingUrl" data-allcaps="false" style="display:none;flex:1 1 200px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Tracking Number" data-datafield="InDeliveryFreightTrackingNumber" data-allcaps="false" style="flex:1 1 200px;"></div>
                          <div class="fwformcontrol track-shipment-in" data-enabled="false" data-type="button" style="flex:1 1 150px;margin:16px 10px 0px 5px;text-align:center;">Track Shipment</div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Incoming Address">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield delivery-type-radio" data-caption="" data-datafield="InDeliveryAddressType"></div>
                          <div class="addresscopy fwformcontrol" data-type="button" style="flex:0 1 40px;margin:16px 5px 0px 15px;text-align:center;">Copy</div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Location" data-datafield="InDeliveryToLocation"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention" data-datafield="InDeliveryToAttention"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 1" data-datafield="InDeliveryToAddress1"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 2" data-datafield="InDeliveryToAddress2"></div>
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

              <!-- MANIFEST TAB -->
              <div data-type="tabpage" id="manifesttabpage" class="tabpage rentalgrid notcombined" data-tabid="manifesttab" data-render="false">
                <div class="wideflexrow">
                  <div class="flexcolumn" style="flex:0 1 175px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Total">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Value" data-datafield="OrderValueTotal" data-enabled="false"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Replacement Cost" data-datafield="OrderReplacementTotal" data-enabled="false"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Owned Total">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Value" data-datafield="OwnedValueTotal" data-enabled="false"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Replacement Cost" data-datafield="OwnedReplacementTotal" data-enabled="false"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sub-Rental Total">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Value" data-datafield="SubValueTotal" data-enabled="false"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Replacement Cost" data-datafield="SubReplacementTotal" data-enabled="false"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Valuation">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield" data-caption="Rental" data-datafield="rentalValueSelector"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield" data-caption="Sales" data-datafield="salesValueSelector"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:1 1 450px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Manifest Items">
                      <div class="wideflexrow">
                        <div class="flexcolumn">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="View Items" data-datafield="manifestItems"></div>
                          </div>
                        </div>
                        <div class="flexcolumn">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="Filter By" data-datafield="manifestFilter"></div>
                          </div>
                        </div>
                      </div>
                      <div class="wideflexrow">
                        <div data-control="FwGrid" data-grid="OrderManifestGrid" data-securitycaption=""></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:0 1 200px;padding-right:10px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Piece Count">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Shipping Containers" data-datafield="ShippingContainerTotal" data-enabled="false"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Stand-Alone Items" data-datafield="StandAloneItemTotal" data-enabled="false"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Piece Count" data-datafield="PieceCountTotal" data-enabled="false"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Total Items">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Shipping Items" data-datafield="ShippingItemTotal" data-enabled="false"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Weight">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="" data-datafield="weightSelector"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Pounds" data-datafield="ExtendedWeightTotalLbs" data-enabled="false"></div>
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Kilograms" data-datafield="ExtendedWeightTotalKg" data-enabled="false" style="display:none;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Ounces" data-datafield="ExtendedWeightTotalOz" data-enabled="false"></div>
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Grams" data-datafield="ExtendedWeightTotalGm" data-enabled="false" style="display:none;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- DOCUMENTS TAB -->
              <div data-type="tabpage" id="documentstabpage" class="tabpage" data-tabid="documentstab">
                <div class="wideflexrow">
                  <div class="rwGrid" data-control="FwGrid" data-grid="QuoteDocumentGrid"></div>
                </div>
              </div>

              <!-- NOTES TAB -->
              <div data-type="tabpage" id="notetabpage" class="tabpage" data-tabid="notetab">
                <div class="wideflexrow">
                  <div class="rwGrid" data-control="FwGrid" data-grid="OrderNoteGrid" data-securitycaption="Notes"></div>
                </div>
              </div>

              <!-- HISTORY TAB -->
              <div data-type="tabpage" id="historytabpage" class="tabpage" data-tabid="historytab">
                <div class="flexrow">
                  <div data-control="FwGrid" data-grid="OrderStatusHistoryGrid" data-securitycaption="Quote Status History"></div>
                </div>
              </div>

              <!-- EMAIL HISTORY TAB -->
             <div data-type="tabpage" id="emailhistorytabpage" class="tabpage submodule emailhistory-page rwSubModule" data-tabid="emailhistorytab">
             </div>

            </div>
          </div>
        </div>
        `;
    }
    //----------------------------------------------------------------------------------------------
    events($form: any) {
        super.events($form);

        // Market Type Change
        $form.find('[data-datafield="MarketTypeId"] input').on('change', event => {
            FwFormField.setValueByDataField($form, 'MarketSegmentId', '');
            FwFormField.setValueByDataField($form, 'MarketSegmentJobId', '');
        });
        $form.find('[data-datafield="MarketSegmentId"] input').on('change', event => {
            FwFormField.setValueByDataField($form, 'MarketSegmentJobId', '');
        });
    };
    //----------------------------------------------------------------------------------------------
    createNewVersionQuote($form) {
        const quoteNumber = FwFormField.getValueByDataField($form, 'QuoteNumber');
        const quoteId = FwFormField.getValueByDataField($form, 'QuoteId');

        const $confirmation = FwConfirmation.renderConfirmation('Create New Version', '');
        $confirmation.find('.fwconfirmationbox').css('width', '450px');
        const html: Array<string> = [];
        html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
        html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push(`    <div>Create New Version for Quote ${quoteNumber}?</div>`);
        html.push('  </div>');
        html.push('</div>');

        FwConfirmation.addControls($confirmation, html.join(''));

        const $yes = FwConfirmation.addButton($confirmation, 'Create New Version', false);
        const $no = FwConfirmation.addButton($confirmation, 'Cancel');

        $yes.on('click', createNewVersion);
        var $confirmationbox = jQuery('.fwconfirmationbox');
        function createNewVersion() {
            FwAppData.apiMethod(true, 'POST', `api/v1/quote/createnewversion/${quoteId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', 'New Version Successfully Created.');
                FwConfirmation.destroyConfirmation($confirmation);
                let uniqueids: any = {};
                uniqueids.QuoteId = response.QuoteId;
                var $quoteform = QuoteController.loadForm(uniqueids);
                FwModule.openModuleTab($quoteform, "", true, 'FORM', true);

                FwModule.refreshForm($form);
            }, null, $confirmationbox);
        }
    };
    //----------------------------------------------------------------------------------------------
    reserveQuote($form: JQuery): void {
        const status = FwFormField.getValueByDataField($form, 'Status');
        let $confirmation, $yes;
        if (status === 'ACTIVE' || status === 'RESERVED') {
            if ($form.attr('data-modified') === 'false') {
                if (status === 'ACTIVE') {
                    $confirmation = FwConfirmation.renderConfirmation('Reserve', '');
                    $confirmation.find('.fwconfirmationbox').css('width', '450px');
                    const html: Array<string> = [];
                    html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                    html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                    html.push('    <div>Reserve this Quote?</div>');
                    html.push('  </div>');
                    html.push('</div>');

                    FwConfirmation.addControls($confirmation, html.join(''));
                    $yes = FwConfirmation.addButton($confirmation, 'Reserve', false);
                    const $no = FwConfirmation.addButton($confirmation, 'Cancel');

                    $yes.on('click', reserve);

                } else if (status === 'RESERVED') {
                    $confirmation = FwConfirmation.renderConfirmation('Unreserve', '');
                    $confirmation.find('.fwconfirmationbox').css('width', '450px');
                    const html: Array<string> = [];
                    html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                    html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                    html.push('    <div>Unreserve this Quote?</div>');
                    html.push('  </div>');
                    html.push('</div>');

                    FwConfirmation.addControls($confirmation, html.join(''));
                    $yes = FwConfirmation.addButton($confirmation, 'Unreserve', false);
                    const $no = FwConfirmation.addButton($confirmation, 'Cancel');

                    $yes.on('click', reserve);
                }
            } else {
                FwNotification.renderNotification('WARNING', 'Save this Quote before attempting to Reserve.');
            }
        } else {
            FwNotification.renderNotification('WARNING', 'This can be completed only for Quotes with status of either ACTIVE or RESERVED.');
        }
        // ----------
        function reserve() {

            FwFormField.disable($confirmation.find('.fwformfield'));
            FwFormField.disable($yes);
            $yes.text('Completing...');
            $yes.off('click');
            const topLayer = '<div class="top-layer" data-controller="none" style="background-color: transparent;z-index:1"></div>';
            const $realConfirm = jQuery($confirmation.find('.fwconfirmationbox')).prepend(topLayer);

            const quoteId = FwFormField.getValueByDataField($form, 'QuoteId');
            FwAppData.apiMethod(true, 'POST', `api/v1/quote/reserve/${quoteId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                FwNotification.renderNotification('SUCCESS', 'Operation Completed');
                FwConfirmation.destroyConfirmation($confirmation);
                FwModule.refreshForm($form);
            }, function onError(response) {
                $yes.on('click', reserve);
                $yes.text('Complete');
                FwFunc.showError(response);
                FwFormField.enable($confirmation.find('.fwformfield'));
                FwFormField.enable($yes);
                FwModule.refreshForm($form);
            }, $realConfirm);
        };
    }
    //-----------------------------------------------------------------------------------------------------
    afterLoad($form, response) {
        super.afterLoad($form, response);

        const quoteId = FwFormField.getValueByDataField($form, 'QuoteId');
        this.checkMessages($form, 'quote', quoteId);
        const status = FwFormField.getValueByDataField($form, 'Status');
        if (status != 'CLOSED') {
            //const makeQuoteActiveOptionId = this.menuMakeQuoteActiveId;
            //$form.find(`.submenu-btn[data-securityid="${makeQuoteActiveOptionId}"]`).attr('data-enabled', 'false');
        }
    }
    //-----------------------------------------------------------------------------------------------------
    cancelUncancel($form) {
        let $confirmation, $yes, $no;
        const quoteId = FwFormField.getValueByDataField($form, 'QuoteId');
        const quoteStatus = FwFormField.getValueByDataField($form, 'Status');
        if (quoteId != null) {
            if (quoteStatus === "CANCELLED") {
                $confirmation = FwConfirmation.renderConfirmation('Cancel', '');
                $confirmation.find('.fwconfirmationbox').css('width', '450px');
                const html: Array<string> = [];
                html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                html.push('    <div>Would you like to un-cancel this Quote?</div>');
                html.push('  </div>');
                html.push('</div>');

                FwConfirmation.addControls($confirmation, html.join(''));
                $yes = FwConfirmation.addButton($confirmation, 'Un-Cancel Quote', false);
                $no = FwConfirmation.addButton($confirmation, 'Cancel');

                $yes.on('click', uncancelQuote);
            } else {
                $confirmation = FwConfirmation.renderConfirmation('Cancel', '');
                $confirmation.find('.fwconfirmationbox').css('width', '450px');
                let html = [];
                html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                html.push('    <div>Would you like to cancel this Quote?</div>');
                html.push('  </div>');
                html.push('</div>');

                FwConfirmation.addControls($confirmation, html.join(''));
                $yes = FwConfirmation.addButton($confirmation, 'Cancel Quote', false);
                $no = FwConfirmation.addButton($confirmation, 'Cancel');

                $yes.on('click', cancelQuote);
            }

            function cancelQuote() {
                let request: any = {};

                FwFormField.disable($confirmation.find('.fwformfield'));
                FwFormField.disable($yes);
                $yes.text('Canceling...');
                $yes.off('click');

                FwAppData.apiMethod(true, 'POST', `api/v1/quote/cancel/${quoteId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    FwNotification.renderNotification('SUCCESS', 'Quote Successfully Cancelled');
                    FwConfirmation.destroyConfirmation($confirmation);
                    FwModule.refreshForm($form);
                }, function onError(response) {
                    $yes.on('click', cancelQuote);
                    $yes.text('Cancel');
                    FwFunc.showError(response);
                    FwFormField.enable($confirmation.find('.fwformfield'));
                    FwFormField.enable($yes);
                    FwModule.refreshForm($form);
                }, $confirmation);
            };

            function uncancelQuote() {
                let request: any = {};

                FwFormField.disable($confirmation.find('.fwformfield'));
                FwFormField.disable($yes);
                $yes.text('Retrieving...');
                $yes.off('click');

                FwAppData.apiMethod(true, 'POST', `api/v1/quote/uncancel/${quoteId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                    FwNotification.renderNotification('SUCCESS', 'Quote Successfully Retrieved');
                    FwConfirmation.destroyConfirmation($confirmation);
                    FwModule.refreshForm($form);
                }, function onError(response) {
                    $yes.on('click', uncancelQuote);
                    $yes.text('Cancel');
                    FwFunc.showError(response);
                    FwFormField.enable($confirmation.find('.fwformfield'));
                    FwFormField.enable($yes);
                    FwModule.refreshForm($form);
                }, $confirmation);
            };
        } else {
            FwNotification.renderNotification('WARNING', 'Select a Quote to perform this action.');
        }
    }
    //-----------------------------------------------------------------------------------------------------
    search($form) {
        const quoteId = FwFormField.getValueByDataField($form, 'QuoteId');
        if ($form.attr('data-mode') === 'NEW') {
            QuoteController.saveForm($form, { closetab: false });
            return;
        }
        if (quoteId == "") {
            FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
        } else {
            const search = new SearchInterface();
            search.renderSearchPopup($form, quoteId, 'Quote');
        }
    }
    //-----------------------------------------------------------------------------------------------------
    createOrder($form) {
        const status = FwFormField.getValueByDataField($form, 'Status');

        if ((status === 'ACTIVE') || (status === 'RESERVED')) {
            const quoteNumber = FwFormField.getValueByDataField($form, 'QuoteNumber');
            const $confirmation = FwConfirmation.renderConfirmation('Create Order', `<div>Create Order from Quote ${quoteNumber}?</div>`);
            const $yes = FwConfirmation.addButton($confirmation, 'Create Order', false);
            const $no = FwConfirmation.addButton($confirmation, 'Cancel');

            $yes.on('click', function () {
                const quoteId = FwFormField.getValueByDataField($form, 'QuoteId');
                const topLayer = '<div class="top-layer" data-controller="none" style="background-color: transparent;z-index:1"></div>';
                const $realConfirm = jQuery($confirmation.find('.fwconfirmationbox')).prepend(topLayer);
                FwAppData.apiMethod(true, 'POST', `api/v1/quote/createorder/${quoteId}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                    FwConfirmation.destroyConfirmation($confirmation);
                    const $quoteTab = jQuery(`#${$form.closest('.tabpage').attr('data-tabid')}`);
                    FwTabs.removeTab($quoteTab);
                    const uniqueids: any = {
                        OrderId: response.OrderId
                    };
                    const $orderform = OrderController.loadForm(uniqueids);
                    FwModule.openModuleTab($orderform, "", true, 'FORM', true);
                    FwNotification.renderNotification('SUCCESS', 'Order Successfully Created.');
                }, null, $realConfirm);
            });
        } else {
            FwNotification.renderNotification('WARNING', 'Can only convert an "ACTIVE" or "RESERVED" Quote to an Order.');
        }
    }
    //-----------------------------------------------------------------------------------------------------
    makeQuoteActive($form) {
        const quoteNumber = FwFormField.getValueByDataField($form, 'QuoteNumber');
        const $confirmation = FwConfirmation.renderConfirmation(`Make Quote Active`, `Make Quote ${quoteNumber} Active?`);
        const $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
        FwConfirmation.addButton($confirmation, 'No', true);
        const topLayer = '<div class="top-layer" data-controller="none" style="background-color: transparent;z-index:1"></div>';
        const $realConfirm = jQuery($confirmation.find('.fwconfirmationbox')).prepend(topLayer);
        $yes.on('click', () => {
            const quoteId = FwFormField.getValueByDataField($form, 'QuoteId');
            FwAppData.apiMethod(true, 'POST', `api/v1/quote/makequoteactive/${quoteId}`, null, FwServices.defaultTimeout,
                response => {
                    FwNotification.renderNotification('SUCCESS', 'Quote Status Successfully Changed to Active.');
                    FwConfirmation.destroyConfirmation($confirmation);
                    //FwModule.refreshForm($form, QuoteController);
                    FwModule.refreshForm($form);
                },
                ex => FwFunc.showError(ex), $realConfirm);
        });
    }
    //-----------------------------------------------------------------------------------------------------
    browseCancelOption($browse: JQuery) {
        try {
            let $confirmation, $yes, $no;
            const quoteId = $browse.find('.selected [data-browsedatafield="QuoteId"]').attr('data-originalvalue');
            const quoteStatus = $browse.find('.selected [data-formdatafield="Status"]').attr('data-originalvalue');
            if (quoteId != null) {
                if (quoteStatus === "CANCELLED") {
                    $confirmation = FwConfirmation.renderConfirmation('Cancel', '');
                    $confirmation.find('.fwconfirmationbox').css('width', '450px');
                    const html: Array<string> = [];
                    html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                    html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                    html.push('    <div>Would you like to un-cancel this Quote?</div>');
                    html.push('  </div>');
                    html.push('</div>');

                    FwConfirmation.addControls($confirmation, html.join(''));
                    $yes = FwConfirmation.addButton($confirmation, 'Un-Cancel Quote', false);
                    $no = FwConfirmation.addButton($confirmation, 'Cancel');

                    $yes.on('click', uncancelQuote);
                }
                else {
                    $confirmation = FwConfirmation.renderConfirmation('Cancel', '');
                    $confirmation.find('.fwconfirmationbox').css('width', '450px');
                    let html = [];
                    html.push('<div class="fwform" data-controller="none" style="background-color: transparent;">');
                    html.push('  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
                    html.push('    <div>Would you like to cancel this Quote?</div>');
                    html.push('  </div>');
                    html.push('</div>');

                    FwConfirmation.addControls($confirmation, html.join(''));
                    $yes = FwConfirmation.addButton($confirmation, 'Cancel Quote', false);
                    $no = FwConfirmation.addButton($confirmation, 'Cancel');

                    $yes.on('click', cancelQuote);
                }

                function cancelQuote() {
                    let request: any = {};

                    FwFormField.disable($confirmation.find('.fwformfield'));
                    FwFormField.disable($yes);
                    $yes.text('Canceling...');
                    $yes.off('click');

                    FwAppData.apiMethod(true, 'POST', `api/v1/quote/cancel/${quoteId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                        FwNotification.renderNotification('SUCCESS', 'Quote Successfully Cancelled');
                        FwConfirmation.destroyConfirmation($confirmation);
                        FwBrowse.databind($browse);
                    }, function onError(response) {
                        $yes.on('click', cancelQuote);
                        $yes.text('Cancel');
                        FwFunc.showError(response);
                        FwFormField.enable($confirmation.find('.fwformfield'));
                        FwFormField.enable($yes);
                        FwBrowse.databind($browse);
                    }, $browse);
                };

                function uncancelQuote() {
                    let request: any = {};

                    FwFormField.disable($confirmation.find('.fwformfield'));
                    FwFormField.disable($yes);
                    $yes.text('Retrieving...');
                    $yes.off('click');

                    FwAppData.apiMethod(true, 'POST', `api/v1/quote/uncancel/${quoteId}`, request, FwServices.defaultTimeout, function onSuccess(response) {
                        FwNotification.renderNotification('SUCCESS', 'Quote Successfully Retrieved');
                        FwConfirmation.destroyConfirmation($confirmation);
                        FwBrowse.databind($browse);
                    }, function onError(response) {
                        $yes.on('click', uncancelQuote);
                        $yes.text('Cancel');
                        FwFunc.showError(response);
                        FwFormField.enable($confirmation.find('.fwformfield'));
                        FwFormField.enable($yes);
                        FwBrowse.databind($browse);
                    }, $browse);
                };
            } else {
                FwNotification.renderNotification('WARNING', 'Select a Quote to perform this action.');
            }
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //-----------------------------------------------------------------------------------------------------
}
//-----------------------------------------------------------------------------------------------------
var QuoteController = new Quote();