//routes.push({ pattern: /^module\/item$/, action: function (match: RegExpExecArray) { return AssetController.getModuleScreen(); } });

class RwAsset {
    Module: string = 'Asset';
    apiurl: string = 'api/v1/item';
    caption: string = Constants.Modules.Inventory.children.Asset.caption;
    nav: string = Constants.Modules.Inventory.children.Asset.nav;
    id: string = Constants.Modules.Inventory.children.Asset.id;
    nameItemAttributeValueGrid: string = 'ItemAttributeValueGrid';
    nameItemQcGrid: string = 'ItemQcGrid';
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    //---------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        options.hasNew = false;
        FwMenu.addBrowseMenuButtons(options);

        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        const $all: JQuery = FwMenu.generateDropDownViewBtn('ALL Warehouses', false, "ALL");
        const $userWarehouse: JQuery = FwMenu.generateDropDownViewBtn(warehouse.warehouse, true, warehouse.warehouseid);

        if (typeof this.ActiveViewFields["WarehouseId"] == 'undefined') {
            this.ActiveViewFields.WarehouseId = [warehouse.warehouseid];
        }

        let viewSubitems: Array<JQuery> = [];
        viewSubitems.push($userWarehouse, $all);
        FwMenu.addViewBtn(options.$menu, 'Warehouse', viewSubitems, true, "WarehouseId");

        //Tracked By Filter
        const $trackAll = FwMenu.generateDropDownViewBtn('ALL', true, "ALL");
        const $trackBarcode = FwMenu.generateDropDownViewBtn('Bar Code', false, "BARCODE");
        const $trackSerialNumber = FwMenu.generateDropDownViewBtn('Serial Number', false, "SERIALNO");
        const $trackRFID = FwMenu.generateDropDownViewBtn('RFID', false, "RFID");

        let viewTrack: Array<JQuery> = [];
        viewTrack.push($trackAll, $trackBarcode, $trackSerialNumber, $trackRFID);
        FwMenu.addViewBtn(options.$menu, 'Tracked By', viewTrack, true, "TrackedBy");
    };
    //---------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        var $browse: JQuery = this.openBrowse();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    };
    //---------------------------------------------------------------------------------------------
    openBrowse() {
        // var $browse: JQuery = FwBrowse.loadBrowseFromTemplate(this.Module);
        let $browse = jQuery(this.getBrowseTemplate());
        $browse = FwModule.openBrowse($browse);

        $browse.data('ondatabind', request => {
            request.activeviewfields = this.ActiveViewFields;
        });

        //FwAppData.apiMethod(true, 'GET', "api/v1/inventorystatus", null, FwServices.defaultTimeout, function onSuccess(response) {
        //    for (let i = 0; i < response.length; i++) {
        //        FwBrowse.addLegend($browse, response[i].InventoryStatus, response[i].Color);
        //    }
        //}, null, $browse);


        try {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, function onSuccess(response) {
                for (let key in response) {
                    FwBrowse.addLegend($browse, key, response[key]);
                }
            }, function onError(response) {
                FwFunc.showError(response);
            }, $browse)
        } catch (ex) {
            FwFunc.showError(ex);
        }
        //Show menu dropdown caption change
        $browse.find('.ddviewbtn .ddviewbtn-dropdown .ddviewbtn-dropdown-btn .ddviewbtn-dropdown-btn-caption:contains("Inactive")').text('Retired');

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        FwMenu.addFormMenuButtons(options);

        // justin hoffman - temporarily disabled as Josh works on
        FwMenu.addSubMenuItem(options.$groupOptions, 'Retire Asset', '', (e: JQuery.ClickEvent) => {
            try {
                this.retireAsset(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //---------------------------------------------------------------------------------------------
    openForm(mode: string) {
        // var $form = FwModule.loadFormFromTemplate(this.Module);
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        return $form;
    };
    //---------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form = this.openForm('EDIT');
        FwFormField.setValueByDataField($form, 'ItemId', uniqueids.ItemId);
        FwModule.loadForm(this.Module, $form);

        $form.find('.repairOrderSubModule').append(this.openRepairOrderBrowse($form));
        $form.find('.orderSubModule').append(this.openOrderBrowse($form));
        $form.find('.transferSubModule').append(this.openTransferBrowse($form));
        $form.find('.invoiceSubModule').append(this.openInvoiceBrowse($form));
        $form.find('.retiredSubModule').append(this.openRetiredBrowse($form));
        return $form;
    };
    //---------------------------------------------------------------------------------------------
    openTransferBrowse($form) {
        let itemId = FwFormField.getValueByDataField($form, 'ItemId');
        let $browse;
        $browse = TransferOrderController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = TransferOrderController.ActiveViewFields;
            request.uniqueids = {
                ItemId: itemId
            };
        });
        return $browse;
    }
    //---------------------------------------------------------------------------------------------
    openRetiredBrowse($form) {
        let itemId = FwFormField.getValueByDataField($form, 'ItemId');
        let $browse;
        $browse = RetiredHistoryController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = RetiredHistoryController.ActiveViewFields;
            request.uniqueids = {
                ItemId: itemId
            };
        });
        return $browse;
    }
    //---------------------------------------------------------------------------------------------
    openInvoiceBrowse($form) {
        let itemId = FwFormField.getValueByDataField($form, 'ItemId');
        let $browse;
        $browse = InvoiceController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = InvoiceController.ActiveViewFields;
            request.uniqueids = {
                ItemId: itemId
            };
        });
        return $browse;
    }
    //---------------------------------------------------------------------------------------------
    openOrderBrowse($form) {
        let itemId = FwFormField.getValueByDataField($form, 'ItemId');
        let $browse;
        $browse = OrderController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = OrderController.ActiveViewFields;
            request.uniqueids = {
                ItemId: itemId
            };
        });

        return $browse;
    }
    //---------------------------------------------------------------------------------------------
    openRepairOrderBrowse($form) {
        let itemId = FwFormField.getValueByDataField($form, 'ItemId');
        let $browse;
        $browse = RepairController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = RepairController.ActiveViewFields;
            request.uniqueids = {
                ItemId: itemId
            };
        });
        //$browse.attr('data-activeinactiveview', 'all');
        jQuery($browse).find('.ddviewbtn-caption:contains("Show:")').siblings('.ddviewbtn-select').find('.ddviewbtn-dropdown-btn:contains("All")').click();
        return $browse;
    }
    //---------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    };
    //---------------------------------------------------------------------------------------------
    loadAudit($form: JQuery) {
        var uniqueid = FwFormField.getValueByDataField($form, 'ItemId');
        FwModule.loadAudit($form, uniqueid);
    };
    //---------------------------------------------------------------------------------------------
    renderGrids($form: JQuery) {
        //const $itemAttributeValueGrid: JQuery = $form.find(`div[data-grid="${this.nameItemAttributeValueGrid}"]`);
        //const $itemAttributeValueGridControl: JQuery = FwBrowse.loadGridFromTemplate(this.nameItemAttributeValueGrid)
        //$itemAttributeValueGrid.empty().append($itemAttributeValueGridControl);
        //$itemAttributeValueGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        ItemId: FwFormField.getValueByDataField($form, 'ItemId')
        //    };
        //});
        //$itemAttributeValueGridControl.data('beforesave', request => {
        //    request.ItemId = FwFormField.getValueByDataField($form, 'ItemId');
        //})
        //FwBrowse.init($itemAttributeValueGridControl);
        //FwBrowse.renderRuntimeHtml($itemAttributeValueGridControl);

        //Item Attribute Value Grid
        FwBrowse.renderGrid({
            nameGrid: 'ItemAttributeValueGrid',
            gridSecurityId: 'CntxgVXDQtQ7',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    ItemId: FwFormField.getValueByDataField($form, 'ItemId')
                };
            },
            beforeSave: (request: any) => {
                request.ItemId = FwFormField.getValueByDataField($form, 'ItemId');
            }
        });

        // ----------
        //const $itemQcGrid: JQuery = $form.find(`div[data-grid="${this.nameItemQcGrid}"]`);
        //const $itemQcGridControl: JQuery = FwBrowse.loadGridFromTemplate(this.nameItemQcGrid);
        //$itemQcGrid.empty().append($itemQcGridControl);
        //$itemQcGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        ItemId: FwFormField.getValueByDataField($form, 'ItemId')
        //    };
        //})
        //FwBrowse.init($itemQcGridControl);
        //FwBrowse.renderRuntimeHtml($itemQcGridControl);

        //Item QC Grid
        FwBrowse.renderGrid({
            nameGrid: 'ItemQcGrid',
            gridSecurityId: 'u4UHiW7AOeZ5',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    ItemId: FwFormField.getValueByDataField($form, 'ItemId')
                };
            },
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
                options.hasEdit = false;
            }
            //jh - user cannot sava data here
            //beforeSave: (request: any) => {
            //    request.ItemId = FwFormField.getValueByDataField($form, 'ItemId');
            //}
        });
        // ----------
        //const $vendorItemGrid: JQuery = $form.find(`div[data-grid="PurchaseVendorInvoiceItemGrid"]`);
        //const $vendorItemGridControl: JQuery = FwBrowse.loadGridFromTemplate('PurchaseVendorInvoiceItemGrid');
        //$vendorItemGrid.empty().append($vendorItemGridControl);
        //$vendorItemGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        PurchaseId: FwFormField.getValueByDataField($form, 'PurchaseId')
        //    };
        //    request.totalfields = ['Quantity', 'Extended'];
        //})
        //FwBrowse.addEventHandler($vendorItemGridControl, 'afterdatabindcallback', ($control, dt) => {
        //    FwFormField.setValueByDataField($form, 'VendorInvoiceTotalQuantity', dt.Totals.Quantity);
        //    FwFormField.setValueByDataField($form, 'VendorInvoiceTotalExtended', dt.Totals.Extended);
        //});
        //FwBrowse.init($vendorItemGridControl);
        //FwBrowse.renderRuntimeHtml($vendorItemGridControl);

        //Purchase Vendor Invoice Item Grid
        FwBrowse.renderGrid({
            nameGrid: 'PurchaseVendorInvoiceItemGrid',
            gridSecurityId: 'NlKSJj2fN0ly',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    PurchaseId: FwFormField.getValueByDataField($form, 'PurchaseId')
                };
            },
            //jh - user cannot sava data here
            //beforeSave: (request: any) => {
            //    request.ItemId = FwFormField.getValueByDataField($form, 'ItemId');
            //}
        });

        // Contract History Grid
        FwBrowse.renderGrid({
            nameGrid: 'ContractHistoryGrid',
            gridSecurityId: 'fY1Au6CjXlodD',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    ItemId: FwFormField.getValueByDataField($form, 'ItemId')
                };
            }
        });


        //Depreciation Grid
        FwBrowse.renderGrid({
            nameGrid: 'DepreciationGrid',
            gridSecurityId: 'Wi9NxgGglKjTN',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = true;
                options.hasEdit = true;
                options.hasDelete = true;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    PurchaseId: FwFormField.getValueByDataField($form, 'PurchaseId'),
                };
            },
            beforeSave: (request: any) => {
                request.PurchaseId = FwFormField.getValueByDataField($form, 'PurchaseId');
            }
        });


    };
    //---------------------------------------------------------------------------------------------
    afterLoad($form: JQuery) {
        const availFor = FwFormField.getValueByDataField($form, 'AvailFor');
        const $iCodeField = FwFormField.getDataField($form, 'InventoryId');
        switch (availFor) {
            case 'S':
                $form.find('[data-datafield="InventoryId"]').attr('data-validationname', 'SalesInventoryValidation');
                $iCodeField.attr('data-peekForm', 'SalesInventory');
                break;
            case 'P':
                $form.find('[data-datafield="InventoryId"]').attr('data-validationname', 'PartsInventoryValidation');
                $iCodeField.attr('data-peekForm', 'PartsInventory');
                break;
            case 'R':
                $form.find('[data-datafield="InventoryId"]').attr('data-validationname', 'RentalInventoryValidation');
                $iCodeField.attr('data-peekForm', 'RentalInventory');
                break;
        }

        const $itemAttributeValueGrid: JQuery = $form.find(`[data-name="${this.nameItemAttributeValueGrid}"]`);
        FwBrowse.search($itemAttributeValueGrid);

        const $itemQcGrid: JQuery = $form.find(`[data-name="${this.nameItemQcGrid}"]`);
        FwBrowse.search($itemQcGrid);

        const $vendorInvoiceItemGrid: JQuery = $form.find(`[data-name="PurchaseVendorInvoiceItemGrid"]`);
        FwBrowse.search($vendorInvoiceItemGrid);

        var status: string = FwFormField.getValueByDataField($form, 'InventoryStatus');
        if (status === "IN") {
            FwFormField.enable($form.find('.ifin'));
        }

        let isContainer = FwFormField.getValueByDataField($form, 'IsContainer');
        if (isContainer == 'true') {
            $form.find('.containertab').show();
        }

        const currentOrderType = FwFormField.getValueByDataField($form, 'CurrentOrderType');
        const $validationField = $form.find('[data-datafield="CurrentOrderId"]');
        let peekForm;
        switch (currentOrderType) {
            case 'T':
                peekForm = 'TransferOrder';
                break;
            case 'O':
                peekForm = 'Order';
                break;
            case 'N':
                peekForm = 'Container';
                break;
        }
        $validationField.attr('data-peekForm', peekForm);

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
        const itemId = FwFormField.getValueByDataField($form, 'ItemId');
        FwAppDocumentGrid.renderGrid({
            $form: $form,
            caption: 'Documents',
            nameGrid: 'AssetDocumentGrid',
            getBaseApiUrl: () => {
                return `${this.apiurl}/${itemId}/document`;
            },
            gridSecurityId: 'pasdUk6LtsQB',
            moduleSecurityId: this.id,
            parentFormDataFields: 'ItemId',
            uniqueid1Name: 'ItemId',
            getUniqueid1Value: () => itemId,
            uniqueid2Name: '',
            getUniqueid2Value: () => ''
        });

    };
    //---------------------------------------------------------------------------------------------
    beforeValidate(datafield, request, $validationbrowse, $form, $tr) {
        switch (datafield) {
            case 'ConditionId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecondition`);
                break;
            case 'InspectionVendorId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinspectionvendor`);
                break;
            case 'ManufacturerId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatemanufacturer`);
                break;
            case 'CountryOfOriginId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecountryoforigin`);
                break;
        }
    }
    //---------------------------------------------------------------------------------------------
    retireAsset($form) {
        const assetInfo: any = {};
        assetInfo.BarCode = FwFormField.getValueByDataField($form, 'BarCode');
        assetInfo.ICode = FwFormField.getTextByDataField($form, 'InventoryId');
        assetInfo.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
        assetInfo.SerialNumber = FwFormField.getValueByDataField($form, 'SerialNumber');
        assetInfo.Description = FwFormField.getValueByDataField($form, 'Description');
        assetInfo.ItemId = FwFormField.getValueByDataField($form, 'ItemId');

        const mode = 'EDIT';
        const $inventoryRetireForm = InventoryRetireUtilityController.openForm(mode, assetInfo);
        FwModule.openSubModuleTab($form, $inventoryRetireForm);
        const $tab = FwTabs.getTabByElement($inventoryRetireForm);
        $tab.find('.caption').html('Retire Asset');
    }
    //---------------------------------------------------------------------------------------------
    getBrowseTemplate(): string {
        return `
        <div data-name="Asset" data-control="FwBrowse" data-type="Browse" id="AssetBrowse" class="fwcontrol fwbrowse" data-orderby="StatusDate" data-controller="AssetController" data-hasinactive="true">
          <div class="column flexcolumn" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="true" data-datafield="ItemId" data-browsedatatype="key"></div>
          </div>
          <div class="column flexcolumn" data-width="auto" data-visible="true">
            <div class="field" data-caption="Bar Code" data-datafield="BarCode" data-cellcolor="BarCodeColor" data-browsedatatype="text" data-sort="asc"></div>
          </div>
          <div class="column flexcolumn" data-width="auto" data-visible="true">
            <div class="field" data-caption="Serial Number" data-datafield="SerialNumber" data-browsedatatype="text" data-sort="asc"></div>
          </div>
          <div class="column flexcolumn" data-width="auto" data-visible="true">
            <div class="field" data-caption="I-Code" data-datafield="ICode" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" data-width="auto" data-visible="true">
            <div class="field" data-caption="Description" data-datafield="Description" data-cellcolor="DescriptionColor" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" data-width="auto" data-visible="true">
            <div class="field" data-caption="Status" data-datafield="InventoryStatus" data-cellcolor="Color" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" data-width="auto" data-visible="true">
            <div class="field" data-caption="As Of" data-datafield="StatusDate" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" data-width="auto" data-visible="true">
            <div class="field" data-caption="Warehouse / Deal" data-datafield="CurrentLocation" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" data-width="auto" data-visible="true">
            <div class="field" data-caption="Warehouse" data-datafield="Warehouse" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" data-width="auto" data-visible="true">
            <div class="field" data-caption="Purchased" data-datafield="PurchaseDate" data-browsedatatype="date" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" data-width="auto" data-visible="true">
            <div class="field" data-caption="Received" data-datafield="ReceiveDate" data-browsedatatype="date" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" data-width="auto" data-visible="true">
            <div class="field" data-caption="Tracked By" data-datafield="TrackedBy" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" data-width="auto" data-visible="true">
            <div class="field" data-datafield="QcRequired"  data-caption="QC Req." data-browsedatatype="checkbox"></div>
          </div>
          <div class="column flexcolumn" data-width="auto" data-visible="true">
            <div class="field" data-caption="RFID" data-datafield="RfId" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" data-width="auto" data-visible="true">
            <div class="field" data-caption="Available For" data-datafield="AvailableForDisplay" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" data-width="0" data-visible="false">
            <div class="field" data-datafield="Inactive" data-browsedatatype="text" data-visible="false"></div>
          </div>
        </div>`;
    };
    //---------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
        <div id="assetform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Asset" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="AssetController">
          <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="ItemId"></div>
          <div id="assetform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
              <div data-type="tab" id="assettab" class="tab" data-tabpageid="assettabpage" data-caption="General"></div>
              <div data-type="tab" id="containertab" class="containertab tab" data-tabpageid="containertabpage" data-caption="Container" style="display:none;"></div>
              <div data-type="tab" id="locationtab" class="tab" data-tabpageid="locationtabpage" data-caption="Location"></div>
              <div data-type="tab" id="manufacturertab" class="tab" data-tabpageid="manufacturertabpage" data-caption="Manufacturer"></div>
              <div data-type="tab" id="purchasetab" class="tab" data-tabpageid="purchasetabpage" data-caption="Purchase"></div>
              <div data-type="tab" id="attributetab" class="tab" data-tabpageid="attributetabpage" data-caption="Attribute"></div>
              <div data-type="tab" id="qctab" class="tab" data-tabpageid="qctabpage" data-caption="Quality Control"></div>
              <div data-type="tab" id="repairordertab" class="tab submodule" data-tabpageid="repairordertabpage" data-caption="Repair Orders"></div>
              <div data-type="tab" id="ordertab" class="tab submodule" data-tabpageid="ordertabpage" data-caption="Orders"></div>
              <div data-type="tab" id="contracttab" class="tab" data-tabpageid="contracttabpage" data-caption="Contracts"></div>
              <div data-type="tab" id="transfertab" class="tab submodule" data-tabpageid="transfertabpage" data-caption="Transfers"></div>
              <div data-type="tab" id="invoicetab" class="tab submodule" data-tabpageid="invoicetabpage" data-caption="Invoices"></div>
              <div data-type="tab" id="retiredhistorytab" class="tab submodule" data-submodulename="RetiredHistory" data-tabpageid="retiredhistorytabpage" data-caption="Retired History"></div>
              <div data-type="tab" id="documentstab" class="tab documentstab" data-tabpageid="documentstabpage" data-caption="Documents"></div>
              <div data-type="tab" id="notestab" class="tab" data-tabpageid="notestabpage" data-caption="Notes"></div>
            </div>
            <div class="tabpages">
              <!-- General tab -->
                <div data-type="tabpage" id="assettabpage" class="tabpage" data-tabid="assettab">
                  <div class="formpage">
                    <div class="formrow">
                      <div class="formcolumn" style="width:700px;">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Asset">
                          <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Available For" data-datafield="AvailFor" data-enabled="false" style="display:none;"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-datafield="PurchaseId" data-enabled="false" style="display:none;"></div>
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="InventoryId" data-validationname="RentalInventoryValidation" data-displayfield="ICode" data-enabled="false" style="float:left;width:150px;"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" data-enabled="false" style="float:left;width:500px;"></div>
                          </div>
                          <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield ifin" data-caption="Bar Code" data-datafield="BarCode" data-maxlength="40" data-enabled="false" style="float:left;width:200px;"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield ifin" data-caption="Serial Number" data-datafield="SerialNumber" data-maxlength="40" data-enabled="false" style="float:left;width:200px;"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield ifin" data-caption="RFID" data-datafield="RfId" data-maxlength="24" data-enabled="false" style="float:left;width:200px;"></div>
                          </div>
                          <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Available For" data-datafield="AvailableForDisplay" data-enabled="false" style="float:left;width:150px;"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="Warehouse" data-enabled="false" style="float:left;width:250px;"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Ownership" data-datafield="Ownership" data-enabled="false" style="float:left;width:250px;"></div>
                          </div>
                        </div>
                      </div>
                      <div class="formcolumn" style="width:225px;">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Status" style="padding-left:1px;">
                          <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Status" data-datafield="InventoryStatus" data-enabled="false" style="width:175px;"></div>
                          </div>
                          <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Status Date" data-datafield="StatusDate" data-enabled="false" style="float:left;width:175px;"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="flexrow container-section" style="display:none;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Container">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="ContainerInventoryId" data-required="false" data-validationname="RentalInventoryValidation" data-displayfield="ContainerICode" data-enabled="false" style="float:left;width:200px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="ContainerDescription" data-required="false" data-enabled="false" style="float:left;width:500px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Status" data-datafield="ContainerStatus" data-required="false" data-enabled="false" style="float:left; width:175px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="ContainerItemId" data-datafield="ContainerItemId" data-required="false" data-enabled="false" style="display:none;"></div>
                        <div class="fwformcontrol container-status" data-type="button" style="margin:15px;">Container Status</div>
                      </div>
                    </div>
                  </div>
                </div>
              <!-- Container tab -->
            <div data-type="tabpage" id="containertabpage" class="tabpage" data-tabid="containertab">
              <div class="formpage">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Container">
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                     <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-datafield="IsContainer" style="display:none"></div>
                      <div data-control="FwFormField" data-type="validation" data-validationname="RentalInventoryValidation" data-displayfield="ContainerICode" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="ContainerInventoryId" data-enabled="false" style="float:left;width:200px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="ContainerDescription" style="float:left;width:400px;" data-enabled="false" ></div>
                    </div>
                  </div>
                <div class="formrow">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Status" data-datafield="ContainerStatus" style="float:left;width:200px;" data-enabled="false" ></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Status Date" data-datafield="ContainerStatusDate" style="float:left;width:200px;" data-enabled="false" ></div>
                  </div>
                  <div class="formrow" style="margin-top:1em;">
                    <div class="fwformcontrol containerstatus" data-type="button">Container Status</div>
                  </div>
                 </div>
                </div>
              </div>
            </div>
              <!-- Location tab -->
              <div data-type="tabpage" id="locationtabpage" class="tabpage" data-tabid="locationtab">
                <div class="formpage">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Current Customer / Deal / Order" style="width:825px;">
                    <div class="formrow">
                      <div class="formcolumn" style="width:400px;">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Customer" data-datafield="CurrentCustomerId" data-displayfield="CurrentCustomer" data-validationname="CustomerValidation" data-enabled="false" style="width:375px;"></div>
                        </div>
                      </div>
                      <div class="formcolumn" style="width:400px;">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="CurrentDealId" data-displayfield="CurrentDeal" data-validationname="DealValidation" data-enabled="false" style="width:375px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="formrow">
                      <div class="formcolumn" style="width:400px;">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Current Order Type" data-datafield="CurrentOrderType" style="display:none;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Order No." data-datafield="CurrentOrderId" data-displayfield="CurrentOrderNumber" data-validationname="OrderValidation" data-enabled="false" style="float:left;width:150px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Order Date" data-datafield="CurrentOrderDate" data-enabled="false" style="float:left;width:125px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="formrow">
                      <div class="formcolumn" style="width:600px">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow" style="margin-top:10px;">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Pick Date / Time" data-datafield="CurrentOrderPickDate" data-enabled="false" style="float:left;width:150px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Est. Start Date / Time" data-datafield="CurrentOrderFromDate" data-enabled="false" style="float:left;width:150px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Est. Stop Date / Time" data-datafield="CurrentOrderToDate" data-enabled="false" style="float:left;width:150px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="formrow">
                    <div class="formcolumn" style="width:400px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Location / Condition">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Aisle" data-datafield="AisleLocation" style="float:left;width:75px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Shelf" data-datafield="ShelfLocation" style="float:left;width:75px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Condition" data-datafield="ConditionId" data-displayfield="Condition" data-validationname="InventoryConditionValidation" style="float:left;width:225px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:124px;padding-left:1px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Usage">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Hours" data-datafield="AssetHours" style="float:left;width:75px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Inspection" style="width:525px;">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Inspection No." data-datafield="InspectionNo" style="float:left;width:125px;"></div>
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Vendor" data-datafield="InspectionVendorId" data-displayfield="InspectionVendor" data-validationname="VendorValidation" style="float:left;width:375px;"></div>
                    </div>
                  </div>
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Last Physical Inventory" style="width:525px;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date" data-datafield="PhysicalDate" data-enabled="false" style="float:left;width:125px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="By" data-datafield="PhysicalBy" data-enabled="false" style="float:left;width:375px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- Manufacturer tab -->
              <div data-type="tabpage" id="manufacturertabpage" class="tabpage" data-tabid="manufacturertab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Manufacturer" style="width:525px;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Name" data-datafield="ManufacturerId" data-displayfield="Manufacturer" data-validationname="VendorValidation" style="float:left;width:375px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Warranty Period" data-datafield="WarrantyPeriod" style="float:left;width:100px;"></div>
                        <p style="float:left;margin-top:22px;margin-bottom:-22px;font-size:10pt;">Days</p>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Model" data-datafield="ManufacturerModelNumber" style="float:left;width:150px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country of Origin" data-datafield="CountryOfOriginId" data-displayfield="CountryOfOrigin" data-validationname="CountryValidation" style="float:left;width:225px;"></div>
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Warranty Expiration" data-datafield="WarrantyExpiration" style="float:left;width:125px;"></div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Part No." data-datafield="ManufacturerPartNumber" style="float:left;width:375px;"></div>
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Mfg. Date" data-datafield="ManufactureDate" style="float:left;width:125px;"></div>
                      </div>
                    </div>
                  </div>
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Item Description" style="width:525px;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="" data-datafield="ItemDescription" style="float:left;width:500px;"></div>
                      </div>
                    </div>
                  </div>
                  <div class="formrow">
                    <div class="formcolumn" style="width:275px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Lot Number">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Lot Number" data-datafield="LotNumber" style="float:left;width:250px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="formcolumn" style="width:200px;padding-left:1px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Shelf Life">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Expiration Date" data-datafield="ShelfLifeExpirationDate" style="float:left;width:125px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- Purchase tab -->
              <div data-type="tabpage" id="purchasetabpage" class="tabpage" data-tabid="purchasetab">
                <div class="flexpage">
                  <div class="flexrow" style="width:1300px;">

                     <!-- Purchase column -->
                     <div class="flexcolumn" style="flex:1 1 300px;">
                       <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Purchase">
                         <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Purchase Date" data-datafield="PurchaseDate" data-enabled="false" style="flex:1 1 100px;"></div>
                         <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Receive Date" data-datafield="ReceiveDate" data-enabled="false" style="flex:1 1 100px;"></div>
                         <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Vendor" data-datafield="PurchaseVendorId" data-enabled="false" data-displayfield="PurchaseVendor" data-validationname="VendorValidation" style="flex:1 1 300px;"></div>
                         <div class="flexrow">
                           <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Purchase PO Number" data-datafield="PurchasePoId" data-enabled="false" data-displayfield="PurchasePoNumber" data-validationname="PurchaseOrderValidation" style="flex:1 1 150px;"></div>
                           <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Outside PO Number" data-datafield="OutsidePurchaseOrderNumber" data-enabled="false" style="flex:1 1 150px;"></div>
                         </div>
                       </div>
                     </div>

                     <!-- Vendor Invoice grid column -->
                     <div class="flexcolumn" style="flex:1 1 650px;">
                       <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Vendor Invoices">
                         <div class="flexrow">
                           <div data-control="FwGrid" data-grid="PurchaseVendorInvoiceItemGrid" data-securitycaption="Purchase Vendor Invoice Item"></div>
                         </div>
                       </div>
                     </div>

                     <!-- Vendor Invoice totals column -->
                     <div class="flexcolumn" style="flex:1 1 150px;">
                       <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Invoice Totals">
                         <div class="flexrow">
                           <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Qty" data-datafield="VendorInvoiceTotalQuantity" data-enabled="false" style="flex:1 1 100px;"></div>
                         </div>
                         <div class="flexrow">
                           <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Extended" data-datafield="VendorInvoiceTotalExtended" data-enabled="false" style="flex:1 1 100px;"></div>
                         </div>
                       </div>
                     </div>

                  </div>

                  <div class="flexrow" style="width:1300px;">

                     <!-- Cost/Value column -->
                     <div class="flexcolumn" style="flex:1 1 1300px;">
                       <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Cost / Value">
                         <div class="flexrow">
                           <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Unit Cost" data-datafield="UnitCostCurrencyConverted" data-currencysymbol="WarehouseCurrencySymbol" data-enabled="false" style="flex:1 1 75px;"></div>
                           <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Depreciation Months" data-datafield="DepreciationMonths" data-enabled="false" style="float:left;width:75px;"></div>
                           <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Accumulated Depreciation" data-datafield="Depreciation" data-currencysymbol="WarehouseCurrencySymbol" data-enabled="false" style="flex:1 1 75px;"></div>
                           <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Current Book Value" data-datafield="BookValue" data-currencysymbol="WarehouseCurrencySymbol" data-enabled="false" style="flex:1 1 75px;"></div>
                           <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Salvage Value" data-datafield="SalvageValue" data-currencysymbol="WarehouseCurrencySymbol" data-enabled="false" style="flex:1 1 75px;"></div>
                         </div>
                       </div>
                     </div>

                  </div>

                  <div class="flexrow" style="width:1300px;">

                     <!-- Depreciation grid -->
                     <div class="flexcolumn" style="flex:1 1 1300px;">
                       <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Depreciation">
                         <div class="flexrow">
                            <div data-control="FwGrid" data-grid="DepreciationGrid"></div>
                         </div>
                       </div>
                     </div>

                  </div>


                </div>
              </div>
              <!-- Attribute tab -->
              <div data-type="tabpage" id="attributetabpage" class="tabpage" data-tabid="attributetab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Attributes" style="width:500px;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwGrid" data-grid="ItemAttributeValueGrid" data-securitycaption="Item Attribute Value" style="min-width:250px;max-width:500px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- QC tab -->
              <div data-type="tabpage" id="qctabpage" class="tabpage" data-tabid="qctab">
                <div class="formpage">
                  <div class="formrow" style="width:100%;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quality Control" style="width:1300px;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwGrid" data-grid="ItemQcGrid" data-securitycaption="Item QC"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- Repair Order tab -->
              <div data-type="tabpage" id="repairordertabpage" class="tabpage repairOrderSubModule rwSubModule" data-tabid="repairordertab">
              </div>
              <!-- Order tab -->
              <div data-type="tabpage" id="ordertabpage" class="tabpage orderSubModule rwSubModule" data-tabid="ordertab">
              </div>
               <!-- Contract Tab-->
               <div data-type="tabpage" id="contracttabpage" class="tabpage submodule rwSubModule" data-tabid="contracttab">
                 <div class="wideflexrow">
                   <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contract">
                     <div data-control="FwGrid" data-grid="ContractHistoryGrid" data-securitycaption="Contract History"></div>
                   </div>
                 </div>
               </div>
              <!-- Transfer tab -->
              <div data-type="tabpage" id="transfertabpage" class="tabpage transferSubModule rwSubModule" data-tabid="transfertab">
              </div>
              <!-- Invoice tab -->
              <div data-type="tabpage" id="invoicetabpage" class="tabpage invoiceSubModule rwSubModule" data-tabid="invoicestab">
              </div>
              <!-- RETIRED HISTORY SUBMODULE-->
              <div data-type="tabpage" id="retiredhistorytabpage" class="tabpage submodule rwSubModule retiredSubModule" data-tabid="retiredhistorytab"></div>
              
              <!-- DOCUMENTS TAB -->
              <div data-type="tabpage" id="documentstabpage" class="tabpage" data-tabid="documentstab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div data-control="FwGrid" data-grid="AssetDocumentGrid"></div>
                  </div>
                </div>
              </div>

              <!-- Notes tab -->
              <div data-type="tabpage" id="notestabpage" class="tabpage" data-tabid="notestab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="flexcolumn">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Notes">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="" data-datafield="ItemNotes" data-height="600px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Photo">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwAppImage" data-type="" class="fwcontrol fwappimage" data-caption="Photo" data-uniqueid1field="ItemId" data-description="" data-rectype=""></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>`;
    };
}

var AssetController = new RwAsset();