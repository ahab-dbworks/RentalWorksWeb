class SearchInterface {
    DefaultColumns: any = [];
    id: string = Constants.Modules.Inventory.children.QuikSearch.id;
    //----------------------------------------------------------------------------------------------
    renderSearchPopup($form, id, type, gridInventoryType?) {
        let html: any = [];
        html.push('<div id="searchpopup" class="fwform fwcontrol">');
        html.push('  <div id="searchTabs" class="fwcontrol fwtabs" data-control="FwTabs" data-version="1">');
        html.push('    <div class="tabs">');
        html.push('          <div data-type="tab" id="itemsearchtab" class="tab" data-tabpageid="itemsearchtabpage" data-caption="Search"></div>');
        html.push('          <div data-type="tab" id="previewtab"    class="tab" data-tabpageid="previewtabpage"    data-caption="Preview"></div>');
        if (type === 'Main') {
            html.push('          <div data-type="tab" id="addtotab"      class="tab" data-tabpageid="addtotabpage"      data-caption="Add To"></div>');
        }
        html.push('    </div>');
        html.push('    <div class="tabpages">');
        html.push('      <div data-type="tabpage" id="itemsearchtabpage" class="tabpage" data-tabid="itemsearchtab"></div>');
        html.push('      <div data-type="tabpage" id="previewtabpage"    class="tabpage" data-tabid="previewtab"></div>');
        if (type === 'Main') {
            html.push('      <div data-type="tabpage" id="addtotabpage"      class="tabpage" data-tabid="addtotab"></div>');
        }
        html.push('    </div>');
        html.push('  </div>');
        html.push('  <div class="close-modal"><i class="material-icons">clear</i><div class="btn-text">Close</div></div>');
        html.push('</div>');
        let $popupHtml = jQuery(html.join(''));

        let $popup     = FwPopup.renderPopup($popupHtml, { ismodal: true });
        FwPopup.showPopup($popup);

        let buttonCaption;
        switch (type) {
            case 'PurchaseOrder':
                buttonCaption = 'Purchase Order';
                break;
            default:
                buttonCaption = type;
                break;
        }

        let searchhtml = `<div id="itemsearch" data-moduletype="${type}">
                            <div class="flexpage">
                              <div class="fwmenu default"></div>
                              <div style="display:flex;flex:0 0 auto;align-items:center;">
                                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield fwformcontrol" data-caption="Currency" data-datafield="CurrencyId" style="display:none;"></div>                                
                                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield fwformcontrol" data-caption="" data-datafield="CurrencySymbol" style="display:none;"></div>                                     
                                <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="" data-datafield="InventoryType"></div>
                                <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield fwformcontrol" data-caption="Pick Date" data-datafield="PickDate" style="flex: 0 1 135px;"></div>                                
                                <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="fwcontrol fwformfield fwformcontrol" data-caption="Pick Time" data-datafield="PickTime" style="flex: 0 1 100px;"></div>                                                     
                                <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield fwformcontrol" data-caption="From Date" data-datafield="FromDate" style="flex: 0 1 135px;"></div>
                                <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="fwcontrol fwformfield fwformcontrol" data-caption="From Time" data-datafield="FromTime" style="flex: 0 1 100px;"></div>                                
                                <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield fwformcontrol" data-caption="To Date" data-datafield="ToDate" style="flex: 0 1 135px;"></div>
                                <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="fwcontrol fwformfield fwformcontrol" data-caption="To Time" data-datafield="ToTime" style="flex: 0 1 100px;"></div>    
                                <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield fwformcontrol" data-caption="Select" data-datafield="Select" style="flex: 0 1 150px;"></div>
                               
                                <div data-type="button" class="fwformcontrol addToOrder">Add to ${buttonCaption}</div>
                                <div data-type="button" class="fwformcontrol insertAtLine" style="display:none; margin: 16px 7px 0px 7px;"></div>
                                <div data-type="button" class="fwformcontrol refresh-availability" style="display:none;">Refresh Availability</div>
                              </div>
                              <div style="display:flex;flex: 0 0 auto;padding: .4em 0;">
                                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield fwformcontrol" data-caption="Search by Description" data-datafield="SearchBox" style="flex: 1 1 400px;"></div>
                              </div>
                              <div style="display:flex;flex:1 1 0;">
                                <div class="flexcolumn" style="flex:0 0 230px;display:flex;flex-direction:column;position:relative;">
                                  <div id="categorycolumns">
                                    <div id="baseType"></div>
                                    <div id="inventoryType"></div>
                                    <div id="category"></div>
                                    <div id="subCategory"></div>
                                  </div>
                                </div>
                                <div class="formoptions">
                                  <div class="breadcrumbrow">
                                    <div id="breadcrumbs"></div>
                                    <i class="material-icons invviewbtn" data-buttonview="LIST">&#xE8EE;</i>
                                    <i class="material-icons invviewbtn" data-buttonview="HYBRID">&#xE8EF;</i>
                                    <i class="material-icons invviewbtn" data-buttonview="GRID">&#xE8F0;</i>
                                    <div class="options">
                                      <i class="material-icons optionsbutton">settings</i>
                                      <div class="optionsmenu">
                                        <div class="flexcolumn">
                                          <div data-datafield="Columns" data-control="FwFormField" data-type="checkboxlist" class="fwcontrol fwformfield columnOrder" data-caption="Select columns to display in Results" data-sortable="true" data-orderby="true"></div>
                                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield fwformcontrol toggleAccessories" data-caption="Disable Auto-Expansion of Complete/Kit Accessories" data-datafield="DisableAccessoryAutoExpand"></div>
                                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield fwformcontrol" data-caption="Hide Inventory with Zero Quantity" data-datafield="HideZeroQuantity"></div>
                                          <div>
                                            <div data-type="button" class="fwformcontrol restoreDefaults" style="width:45px; float:left; margin:10px;">Reset</div>
                                            <div data-type="button" class="fwformcontrol applyOptions" style="width:45px; float:right; margin:10px;">Apply</div>
                                          </div>
                                        </div>
                                      </div>
                                    </div>
                                  </div>
                                  <div id="itemlist" data-view="GRID">
                                    <div class="columnDescriptions">
                                      <div data-column="ItemImage"></div>
                                      <div class="columnorder" data-column="Description">Description</div>
                                      <div class="columnorder" data-column="Tags"></div>
                                      <div class="columnorder" data-column="Quantity">Qty</div>
                                      <div class="columnorder showOnSearch" data-column="Type">Type</div> 
                                      <div class="columnorder showOnSearch" data-column="Category">Category</div>
                                      <div class="columnorder showOnSearch" data-column="SubCategory">Sub Category</div>
                                      <div class="columnorder" style="display:none;" data-column="ICode">I-Code</div>
                                      <div class="columnorder" style="display:none;" data-column="PartNumber">Part Number</div>
                                      <div class="columnorder hideColumns" data-column="Available">Available</div>
                                      <div class="columnorder hideColumns" data-column="ConflictDate">Conflict Date</div>
                                      <div class="columnorder hideColumns" data-column="AllWh">All Warehouses</div>
                                      <div class="columnorder" data-column="In">In</div>
                                      <div class="columnorder" data-column="QC">QC</div>
                                      <div class="columnorder" style="display:none;" data-column="Note">Note</div>
                                      <div class="columnorder" data-column="Rate">Rate</div>
                                    </div>
                                    <div id="inventory"></div>
                                  </div>
                                </div>
                              </div>
                            </div>
                          </div>`;
        $popup.find('#itemsearchtabpage').append(jQuery(searchhtml));

        let previewhtml = `<div id="previewHtml" style="overflow:auto;">
                             <div class="fwmenu default"></div>
                             <div style="padding: 5px;text-align: right;">
                               <div data-type="button" class="fwformcontrol addToOrder">Add to ${buttonCaption}</div>
                               <div data-type="button" class="fwformcontrol insertAtLine" style="display:none; margin: 16px 7px 0px 7px;"></div>
                             </div>
                             <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                               <div data-control="FwGrid" data-grid="SearchPreviewGrid" data-securitycaption="Preview"></div>
                             </div>
                           </div>`;
        $popup.find('#previewtabpage').append(jQuery(previewhtml));

        if (type === 'Main') {
            jQuery('title').html('QuikSearch');
            $popup.find('.addToOrder').hide();
            this.renderAddToTab($popup);
        }

        FwControl.renderRuntimeControls($popup.find('#searchpopup .fwcontrol'));

        if ($form) {
            const $grid = $form.find('[data-name="OrderItemGrid"]:visible');
            if ($grid.length > 0) { 
                if ($form.data('ismanualsort') ) {
                    const $selectedtrs = $grid.find('tbody tr .tdselectrow input:checked').parents('tr');
                    if ($selectedtrs.length > 0) {
                        $popup.find('.insertAtLine').show();
                        const $firstRow = jQuery($selectedtrs[$selectedtrs.length - 1]);
                        const rowNumber = FwBrowse.getValueByDataField($grid, $firstRow, 'RowNumber');
                        $popup.find('.insertAtLine').text(`Insert at line ${rowNumber} of ${buttonCaption}`);
                        $popup.attr('data-insertatrownumber', rowNumber);
                    };
                }
            }
        }

        FwFormField.loadItems($popup.find('div[data-datafield="Select"]'), [
            { value: '',    text: 'All' },
            { value: 'CKN', text: 'Complete/Kit/Container', selected: true },
            { value: 'CK',  text: 'Complete/Kit' },
            { value: 'N',   text: 'Container' },
            { value: 'I',   text: 'Item' },
            { value: 'A',   text: 'Accessory' }
        ], true);

        //FwFormField.loadItems($popup.find('div[data-datafield="SortBy"]'), [
        //    { value: 'INVENTORY',   text: 'Type / Category / Sub-Category', selected: true }, //removed 3/26/20 bc of inventory sequence util will sort now
        //    { value: 'ICODE',       text: 'I-Code' },
        //    { value: 'DESCRIPTION', text: 'Description' },
        //    { value: 'PARTNO',      text: 'Part No.' }
        //], true);

        var inventoryTypes = [{ value: 'R', caption: 'Rental' },
        { value: 'S', caption: 'Sales' },
                              { value: 'L', caption: 'Labor' },
                              { value: 'M', caption: 'Misc' }];
        if (type === 'PurchaseOrder') {
            inventoryTypes.push({value: 'P', caption: 'Parts'});
        }

        if (type === 'Complete' || type === 'Kit' || type === 'Container') {
            if (gridInventoryType === 'Sales') {
                inventoryTypes = [{
                    value: 'S', caption: 'Sales'
                }]
            } else if (gridInventoryType === 'Rental') {
                inventoryTypes = [{
                    value: 'R', caption: 'Rental'
                }]
            }
        }
        FwFormField.loadItems($popup.find('div[data-datafield="InventoryType"]'), inventoryTypes);

        this.DefaultColumns = [
            { value: 'Description',  text: 'Description',                         selected: 'T' },
            { value: 'Tags',         text: 'Tags',                                selected: 'T' },
            { value: 'Quantity',     text: 'Quantity',                            selected: 'T' },
            { value: 'Type',         text: 'Type',                                selected: 'F' },
            { value: 'Category',     text: 'Category',                            selected: 'F' },
            { value: 'SubCategory',  text: 'Sub Category',                        selected: 'F' },
            { value: 'ICode',        text: 'I-Code',                              selected: 'F' },
            { value: 'PartNumber',   text: 'Part Number',                         selected: 'F' },
            { value: 'Available',    text: 'Available Quantity',                  selected: 'T' },
            { value: 'ConflictDate', text: 'Conflict Date',                       selected: 'T' },
            { value: 'AllWh',        text: 'Available Quantity (All Warehouses)', selected: 'T' },
            { value: 'In',           text: 'In Quantity',                         selected: 'T' },
            { value: 'QC',           text: 'QC Required Quantity',                selected: 'T' },
            { value: 'Rate',         text: 'Rate',                                selected: 'T' },
            { value: 'Note',         text: 'Note',                                selected: 'F' }
        ];

        let startDate;
        let stopDate;
        let pickDate;
        switch (type) {
            case 'Main':
                const department = JSON.parse(sessionStorage.getItem('department')); 
                const location = JSON.parse(sessionStorage.getItem('location')); 
                const today = FwFunc.getDate();
                const addToTypes = [{ value: 'Quote',    caption: 'Quote', checked: 'T' },
                                    { value: 'Order',    caption: 'Order' },
                                    { value: 'Purchase', caption: 'Purchase' },
                                    { value: 'Transfer', caption: 'Transfer' }];
                FwFormField.loadItems($popup.find('div[data-datafield="AddToType"]'), addToTypes);
                FwFormField.setValueByDataField($popup, 'DepartmentId', department.departmentid, department.department);
                FwFormField.setValueByDataField($popup, 'RateType', location.ratetype, location.ratetype);
                FwFormField.setValueByDataField($popup, 'PickDate', today);
                FwFormField.setValueByDataField($popup, 'FromDate', today);
                FwFormField.setValueByDataField($popup, 'ToDate', today);
                FwAppData.apiMethod(true, 'GET', `api/v1/officelocation/${location.locationid}`, null, FwServices.defaultTimeout, response => {
                    FwFormField.setValueByDataField($popup, 'PoTypeId', response.DefaultPurchasePoTypeId, response.DefaultPurchasePoType);
                }, ex => FwFunc.showError(ex), null);
                FwAppData.apiMethod(true, 'GET', 'api/v1/departmentlocation/' + department.departmentid + '~' + location.locationid, null, FwServices.defaultTimeout, response => {
                    FwFormField.setValueByDataField($popup, 'OrderTypeId', response.DefaultOrderTypeId, response.DefaultOrderType);
                }, ex => FwFunc.showError(ex), null);

                this.disableAddToModules($popup);
                break;
            case 'Order':
            case 'Quote':
                let pickStartStop: PickStartStop = OrderBaseController.getPickStartStop($form);
                FwFormField.setValueByDataField($popup, 'PickDate', pickStartStop.PickDate);
                FwFormField.setValueByDataField($popup, 'FromDate', pickStartStop.StartDate);
                FwFormField.setValueByDataField($popup, 'ToDate', pickStartStop.StopDate);
                FwFormField.setValueByDataField($popup, 'PickTime', pickStartStop.PickTime);
                FwFormField.setValueByDataField($popup, 'FromTime', pickStartStop.StartTime);
                FwFormField.setValueByDataField($popup, 'ToTime', pickStartStop.StopTime);
                FwFormField.setValueByDataField($popup, 'CurrencyId', FwFormField.getValueByDataField($form, 'CurrencyId'));
                FwFormField.setValueByDataField($popup, 'CurrencySymbol', FwFormField.getValueByDataField($form, 'CurrencySymbol'));
                $popup.data('ratetype', FwFormField.getValueByDataField($form, 'RateType'));
                break;
            case 'PurchaseOrder':
                $popup.find('[data-datafield="PickDate"], [data-datafield="FromDate"], [data-datafield="ToDate"]').hide();
                $popup.find('[data-datafield="PickTime"], [data-datafield="FromTime"], [data-datafield="ToTime"]').hide();
                startDate = FwFormField.getValueByDataField($form, 'PurchaseOrderDate');
                FwFormField.setValueByDataField($popup, 'CurrencyId', FwFormField.getValueByDataField($form, 'CurrencyId'));
                FwFormField.setValueByDataField($popup, 'CurrencySymbol', FwFormField.getValueByDataField($form, 'CurrencySymbol'));
                FwFormField.setValueByDataField($popup, 'FromDate', startDate);
                break;
            case 'Transfer':
                $popup.find('[data-datafield="InventoryType"] [value="M"]').parent('.togglebutton-item').hide();
                $popup.find('[data-datafield="InventoryType"] [value="L"]').parent('.togglebutton-item').hide();
                pickDate = FwFormField.getValueByDataField($form, 'PickDate');
                let shipDate = FwFormField.getValueByDataField($form, 'ShipDate');
                if (new Date(pickDate).getTime() == new Date(shipDate).getTime()) {
                    startDate = pickDate;
                } else if (new Date(pickDate).getTime() > new Date(shipDate).getTime()) {
                    startDate = shipDate;
                } else if (new Date(pickDate).getTime() < new Date(shipDate).getTime()) {
                    startDate = pickDate;
                }
                FwFormField.setValueByDataField($popup, 'FromDate', startDate);
                FwFormField.setValueByDataField($popup, 'PickDate', pickDate);
                break;
        }

        $popup.find('#itemsearch').data('parentformid', id);
        let warehouseId;
        let warehouseText;
        
        if (type === 'Transfer' || type === 'Complete' || type === 'Kit' || type === 'Container' || type === 'Main') {
            const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
            warehouseId = warehouse.warehouseid;
            warehouseText = warehouse.warehouse;
        } else {
            warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
            warehouseText = FwFormField.getTextByDataField($form, 'WarehouseId');
        }
        $popup.find('#itemsearch').data('warehouseid', warehouseId);
        $popup.find('#itemsearch').data('warehousetext', warehouseText);

        this.getViewSettings($popup);

        //Render preview grid
        FwBrowse.renderGrid({
            nameGrid:         'SearchPreviewGrid',
            gridSecurityId:   'JLDAuUcvHEx1',
            moduleSecurityId: this.id,
            $form:            $popup,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
            },
            onDataBind: (request: any) => {
                //request.SessionId        = id;
                //request.ShowAvailability = true;
                //request.FromDate         = FwFormField.getValueByDataField($popup, 'FromDate');
                //request.ToDate           = FwFormField.getValueByDataField($popup, 'ToDate');
                //request.ShowImages = true;

                let showAvailability: boolean = $popup.find('[data-datafield="Columns"] li[data-value="Available"]').attr('data-selected') === 'T' ? true : false;

                if (type === 'PurchaseOrder') {
                    showAvailability = false;
                }

                request.uniqueids = {
                    SessionId: id,
                    ShowAvailability: showAvailability,
                    ShowImages: true,
                    FromDate: FwFormField.getValueByDataField($popup, 'FromDate'),
                    ToDate: FwFormField.getValueByDataField($popup, 'ToDate'),
                }

            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                this.updatePreviewTabQuantity($popup, id, false);
            }
        });

        this.updatePreviewTabQuantity($popup, id, true);
        this.events($popup, $form, id);

        //Sets inventory type by active tab
        if (typeof gridInventoryType == 'undefined') {
            gridInventoryType = $form.find('.tabs .active[data-type="tab"]').attr('data-inventorytype');
        }

        switch (gridInventoryType) {
            default:
            case 'Rental':
                FwFormField.setValue($popup, 'div[data-datafield="InventoryType"]', 'R', '', true);
                break;
            case 'Sales':
                FwFormField.setValue($popup, 'div[data-datafield="InventoryType"]', 'S', '', true);
                break;
            case 'Labor':
                FwFormField.setValue($popup, 'div[data-datafield="InventoryType"]', 'L', '', true);
                break;
            case 'Misc':
                FwFormField.setValue($popup, 'div[data-datafield="InventoryType"]', 'M', '', true);
                break;
            case 'Parts':
                FwFormField.setValue($popup, 'div[data-datafield="InventoryType"]', 'P', '', true);
                break;
        };

        //Hide columns based on type
        if (type === 'PurchaseOrder' || type === 'Template' || gridInventoryType == 'Misc' || gridInventoryType == 'Labor') {
            $popup.find('.hideColumns').css('display', 'none');
        }

        return $popup;
    }
    //----------------------------------------------------------------------------------------------
    renderAddToTab($popup) {
        const $searchpopup = $popup.find('#searchpopup');
        const html = `<div id="addToTab">
                        <div class="fwmenu default"></div>
                        <div class="flexrow">
                            <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="" data-datafield="AddToType" style="flex:0 1 450px;"></div>
                        </div>
                        <div class="flexrow">
                           <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quote">
                               <div class="flexrow">
                                 <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" data-required="true" style="flex:1 1 250px;"></div>
                                 <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" data-required="true" style="flex:1 1 100px;"></div>
                                </div>
                                <div class="flexrow">
                                    <div data-control="FwFormField" data-type="text" class="deal-fields po-hide fwcontrol fwformfield" data-caption="Deal No." data-datafield="DealNumber" data-enabled="false" style="flex:1 1 100px;"></div>
                                    <div data-control="FwFormField" data-type="validation" class="deal-fields po-show fwcontrol fwformfield" data-caption="Vendor" data-datafield="VendorId" data-displayfield="Vendor" data-validationname="VendorValidation" data-required="true" style="flex:1 1 275px; display:none;"></div>                               
                                    <div data-control="FwFormField" data-type="validation" class="deal-fields po-hide fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation" style="flex:1 1 275px;"></div>
                                    <div data-control="FwFormField" data-type="validation" class="deal-fields fwcontrol fwformfield" data-caption="Rate" data-datafield="RateType" data-displayfield="RateType" data-validationname="RateTypeValidation" data-validationpeek="false" data-required="true" style="flex:1 1 175px;"></div>
                                    <div data-control="FwFormField" data-type="validation" class="po-hide deal-fields fwcontrol fwformfield" data-caption="Type" data-datafield="OrderTypeId" data-displayfield="OrderType" data-validationname="OrderTypeValidation" data-required="true" style="flex:1 1 175px;"></div>                                 
                                    <div data-control="FwFormField" data-type="validation" class="po-show fwcontrol fwformfield" data-caption="Type" data-datafield="PoTypeId" data-displayfield="PoType" data-validationname="POTypeValidation" data-required="true" style="display:none;flex:1 1 175px;"></div>                                 
                                </div>
                                <div class="flexrow">
                                    <div data-control="FwFormField" data-type="validation" class="transfer-show fwcontrol fwformfield" data-caption="From Warehouse" data-datafield="FromWarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" data-required="true" style="display:none; flex:1 1 175px;"></div>
                                    <div data-control="FwFormField" data-type="validation" class="transfer-show fwcontrol fwformfield" data-caption="To Warehouse" data-datafield="ToWarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" data-required="true" style="display:none; flex:1 1 175px;"></div>                                 
                                </div>
                                <div class="flexrow">
                                    <div data-control="FwFormField" data-type="date" class="po-hide fwcontrol fwformfield" data-caption="Pick Date" data-datafield="PickDate" style="flex:1 1 115px;"></div>
                                    <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="po-hide fwcontrol fwformfield" data-caption="Pick Time" data-datafield="PickTime" style="flex:1 1 84px;"></div>
                                    <div data-control="FwFormField" data-type="date" class="po-hide fwcontrol fwformfield transfer-hide" data-caption="From Date" data-datafield="FromDate" style="flex:1 1 115px;"></div>
                                    <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="po-hide fwcontrol fwformfield transfer-hide" data-caption="From Time" data-datafield="FromTime" style="flex:1 1 84px;"></div>
                                    <div data-control="FwFormField" data-type="date" class="po-hide fwcontrol fwformfield transfer-hide" data-caption="To Date" data-datafield="ToDate" style="flex:1 1 115px;"></div>
                                    <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="po-hide fwcontrol fwformfield transfer-hide" data-caption="To Time" data-datafield="ToTime" style="flex:1 1 84px;"></div>
                                    <div data-control="FwFormField" data-type="date" class="po-hide fwcontrol fwformfield transfer-show" data-caption="Ship Date" data-datafield="ShipDate" data-required="true" style="flex:1 1 115px; display:none;"></div>
                                    <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="po-hide fwcontrol fwformfield transfer-show" data-caption="Ship Time" data-datafield="ShipTime" style="display:none; flex:1 1 84px;"></div>
                                    <div data-control="FwFormField" data-type="date" class="po-hide fwcontrol fwformfield transfer-show" data-caption="Required Date" data-datafield="RequiredDate" style="flex:1 1 115px; display:none;"></div>
                                    <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="po-hide fwcontrol fwformfield transfer-show" data-caption="Required Time" data-datafield="RequiredTime" style="display:none; flex:1 1 84px;"></div>
                                 </div>
                            </div>
                        </div>
                        <div class="flexrow" style="justify-content:flex-end;">
                           <div data-type="button" class="fwformcontrol create-new" style="max-width:fit-content; margin-right:15px;">Create Quote</div>
                         </div>
                      </div>`;
        $popup.find('#addtotabpage').append(jQuery(html));

        //default dealnumber when selecting a deal
        $popup.find('#addToTab [data-datafield="DealId"]').data('onchange', $tr => {
            FwFormField.setValue2($popup.find('#addToTab [data-datafield="DealNumber"]'), $tr.find('[data-browsedatafield="DealNumber"]').attr('data-originalvalue'));
        });

        //toggle fields shown based on type selected
        $popup.find('[data-datafield="AddToType"]').on('change', e => {
            const addToType = jQuery(e.target).val();
            const $section = $popup.find('#addToTab [data-type="section"]');
            $popup.find('#addToTab .fwformfield:visible').removeClass('error');
            // show/hide fields.  could have separate html for each type instead if this gets too confusing
            switch (addToType) {
                case 'Quote':
                    $popup.find('#addToTab [data-datafield="DealId"]').attr('data-required', false);
                    $popup.find('#addToTab .po-hide').show();
                    $popup.find('#addToTab .deal-fields').show();
                    $popup.find('#addToTab .po-show').hide();
                    $popup.find('#addToTab .transfer-hide').show();
                    $popup.find('#addToTab .transfer-show').hide();
                    break;
                case 'Order':
                    $popup.find('#addToTab [data-datafield="DealId"]').attr('data-required', true);
                    $popup.find('#addToTab .po-hide').show();
                    $popup.find('#addToTab .deal-fields').show();
                    $popup.find('#addToTab .po-show').hide();
                    $popup.find('#addToTab .transfer-hide').show();
                    $popup.find('#addToTab .transfer-show').hide();
                    break;
                case 'Purchase':
                    $popup.find('#addToTab .deal-fields').show();
                    $popup.find('#addToTab .transfer-hide').show();
                    $popup.find('#addToTab .po-show').show();
                    $popup.find('#addToTab .transfer-show').hide();
                    $popup.find('#addToTab .po-hide').hide();
                    break;
                case 'Transfer':
                    const wh = JSON.parse(sessionStorage.getItem('warehouse'));
                    FwFormField.setValue2($popup.find('#addToTab [data-datafield="FromWarehouseId"]'), wh.warehouseid, wh.warehouse);
                    $popup.find('#addToTab .po-hide').show();
                    $popup.find('#addToTab .deal-fields').hide();
                    $popup.find('#addToTab .transfer-hide').hide();
                    $popup.find('#addToTab .transfer-show').show();
                    break;
            }
            $section.find('.fwform-section-title').text(addToType);
            $popup.find('div.create-new').html(`Create ${addToType}`);
        });

        //create new record, add items, then open form
        $popup.find('div.create-new').on('click', e => {
            const $addToTab = $popup.find('#addToTab');
            $addToTab.data('fields', $addToTab.find('.fwformfield:visible'));
            const isValid = FwModule.validateForm($addToTab);
            if (isValid) {
                let addToType = FwFormField.getValue2($addToTab.find('[data-datafield="AddToType"]'));
                let apiurl;
                let controller;
                const request: any       = {};
                request.WarehouseId      = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
                request.OfficeLocationId = JSON.parse(sessionStorage.getItem('location')).locationid;
                request.Description      = FwFormField.getValue2($addToTab.find('[data-datafield="Description"]'));
                request.DepartmentId     = FwFormField.getValue2($addToTab.find('[data-datafield="DepartmentId"]'));
                switch (addToType) {
                    case 'Quote':
                        controller                 = 'QuoteController';
                        request.DealId             = FwFormField.getValue2($addToTab.find('[data-datafield="DealId"]'));
                        request.RateType           = FwFormField.getValue2($addToTab.find('[data-datafield="RateType"]'));
                        request.OrderTypeId        = FwFormField.getValue2($addToTab.find('[data-datafield="OrderTypeId"]'));
                        request.PickDate           = FwFormField.getValue2($addToTab.find('[data-datafield="PickDate"]'));
                        request.PickTime           = FwFormField.getValue2($addToTab.find('[data-datafield="PickTime"]'));
                        request.EstimatedStartDate = FwFormField.getValue2($addToTab.find('[data-datafield="FromDate"]'));
                        request.EstimatedStartTime = FwFormField.getValue2($addToTab.find('[data-datafield="FromTime"]'));
                        request.EstimatedStopDate  = FwFormField.getValue2($addToTab.find('[data-datafield="ToDate"]'));
                        request.EstimatedStopTime  = FwFormField.getValue2($addToTab.find('[data-datafield="ToTime"]'));
                        break;
                    case 'Order':
                        controller                 = 'OrderController';
                        request.DealId             = FwFormField.getValue2($addToTab.find('[data-datafield="DealId"]'));
                        request.RateType           = FwFormField.getValue2($addToTab.find('[data-datafield="RateType"]'));
                        request.OrderTypeId        = FwFormField.getValue2($addToTab.find('[data-datafield="OrderTypeId"]'));
                        request.PickDate           = FwFormField.getValue2($addToTab.find('[data-datafield="PickDate"]'));
                        request.PickTime           = FwFormField.getValue2($addToTab.find('[data-datafield="PickTime"]'));
                        request.EstimatedStartDate = FwFormField.getValue2($addToTab.find('[data-datafield="FromDate"]'));
                        request.EstimatedStartTime = FwFormField.getValue2($addToTab.find('[data-datafield="FromTime"]'));
                        request.EstimatedStopDate  = FwFormField.getValue2($addToTab.find('[data-datafield="ToDate"]'));
                        request.EstimatedStopTime  = FwFormField.getValue2($addToTab.find('[data-datafield="ToTime"]'));
                        break;
                    case 'Purchase':
                        addToType                  = PurchaseOrderController.Module;
                        controller                 = 'PurchaseOrderController';
                        request.VendorId           = FwFormField.getValue2($addToTab.find('[data-datafield="VendorId"]'));
                        request.RateType           = FwFormField.getValue2($addToTab.find('[data-datafield="RateType"]'));
                        request.PoTypeId           = FwFormField.getValue2($addToTab.find('[data-datafield="PoTypeId"]'));
                        request.PickDate           = FwFormField.getValue2($addToTab.find('[data-datafield="PickDate"]'));
                        request.PickTime           = FwFormField.getValue2($addToTab.find('[data-datafield="PickTime"]'));
                        request.EstimatedStartDate = FwFormField.getValue2($addToTab.find('[data-datafield="FromDate"]'));
                        request.EstimatedStartTime = FwFormField.getValue2($addToTab.find('[data-datafield="FromTime"]'));
                        request.EstimatedStopDate  = FwFormField.getValue2($addToTab.find('[data-datafield="ToDate"]'));
                        request.EstimatedStopTime  = FwFormField.getValue2($addToTab.find('[data-datafield="ToTime"]'));
                        break;
                    case 'Transfer':
                        controller              = 'TransferOrderController';
                        request.ToWarehouseId   = FwFormField.getValue2($addToTab.find('[data-datafield="ToWarehouseId"]'));
                        request.FromWarehouseId = FwFormField.getValue2($addToTab.find('[data-datafield="FromWarehouseId"]'));
                        request.PickDate        = FwFormField.getValue2($addToTab.find('[data-datafield="PickDate"]'));
                        request.PickTime        = FwFormField.getValue2($addToTab.find('[data-datafield="PickTime"]'));
                        request.ShipDate        = FwFormField.getValue2($addToTab.find('[data-datafield="ShipDate"]'));
                        request.ShipTime        = FwFormField.getValue2($addToTab.find('[data-datafield="ShipTime"]'));
                        request.RequiredDate    = FwFormField.getValue2($addToTab.find('[data-datafield="RequiredDate"]'));
                        request.RequiredTime    = FwFormField.getValue2($addToTab.find('[data-datafield="RequiredTime"]'));
                        break;
                }
                apiurl = (<any>window)[controller].apiurl;
                FwAppData.apiMethod(true, 'POST', apiurl, request, FwServices.defaultTimeout,
                    response => {
                        const newRecordInfo: any = {
                            "Controller":    controller,
                            "UniqueIdField": `${addToType}Id`,
                            "UniqueId":      response[`${addToType}Id`],
                            "Caption":       addToType
                        }
                        $popup.find('#addToTab').data('newRecordInfo', newRecordInfo);
                        $popup.find('.addToOrder').click();
                    }, ex => FwFunc.showError(ex), $searchpopup);
            }
        });

        //synchronizes date fields on Add To tab
        $popup.on('change', '[data-datafield="PickDate"], [data-datafield="FromDate"], [data-datafield="ToDate"]', e => {
            const $this = jQuery(e.currentTarget);
            const datafield = $this.attr('data-datafield');
            const value = FwFormField.getValue2($this);
            FwFormField.setValueByDataField($popup, datafield, value);
        });

        //synchronizes time fields on Add To tab
        $popup.on('change', '[data-datafield="PickTime"], [data-datafield="FromTime"], [data-datafield="ToTime"]', e => {
            const $this = jQuery(e.currentTarget);
            const datafield = $this.attr('data-datafield');
            const value = FwFormField.getValue2($this);
            FwFormField.setValueByDataField($popup, datafield, value);
        });
    }
    //----------------------------------------------------------------------------------------------
    disableAddToModules($popup: JQuery) {
        //disables radio options based on security settings
        const nodeModules = ['Quote', 'Order', 'PurchaseOrder', 'TransferOrder'];
        const modules = FwApplicationTree.getChildrenByType(FwApplicationTree.tree, 'Module');
        let activeModule = "";
        for (let i = 0; i < nodeModules.length; i++) {
            let moduleName = nodeModules[i];
            const nodeModule = modules.filter((el) => { return el.caption === moduleName });
            if (nodeModule) {
                const nodeActions = FwApplicationTree.getNodeByFuncRecursive(nodeModule[0], {}, (node: any, args: any) => {
                    return (node.nodetype === 'ModuleActions');
                });
                const nodeNew = FwApplicationTree.getNodeByFuncRecursive(nodeActions, {}, (node: any, args: any) => {
                    return (node.nodetype === 'ModuleAction' && node.properties.action === 'New');
                });
                let hasNew = (nodeNew !== null && nodeNew.properties.visible === 'T');
                if (hasNew && activeModule == "") {
                    activeModule = moduleName;
                } else if (!hasNew) {
                    if (moduleName == 'PurchaseOrder') moduleName = 'Purchase';
                    if (moduleName == 'TransferOrder') moduleName = 'Transfer';
                    $popup.find(`#addToTab [data-datafield="AddToType"] input[value="${moduleName}"]`).parent('label').hide();
                }
            }
        }

        FwFormField.setValueByDataField($popup, 'AddToType', activeModule, null, true);
    }
    //----------------------------------------------------------------------------------------------
    populateTypeMenu($popup) {
        let inventoryTypeRequest: any = {};
        let $breadcrumbs              = $popup.find('#breadcrumbs');
        let categoryType;

        $breadcrumbs.empty();
        $popup.find('#itemsearch').attr('data-inventorytypeid', '').attr('data-categoryid', '').attr('data-subcategoryid', '');
        $popup.find('#baseType, #inventoryType, #category, #subCategory, #inventory').empty();

        switch (FwFormField.getValue($popup, '[data-datafield="InventoryType"]')) {
            case 'R':
                inventoryTypeRequest.uniqueids = { Rental: true };
                categoryType                   = 'rentalcategory';
                $breadcrumbs.append('<div class="basetype breadcrumb"><div class="value">RENTAL</div></div>');
                $popup.find('#baseType').append(`<ul class="selected"><span class="downArrowNav"><i class="material-icons">keyboard_arrow_down</i></span><span>RENTAL</span></ul>`);
                $popup.find('#itemsearch').attr('data-base', 'RENTAL');
                break;
            case 'S':
                inventoryTypeRequest.uniqueids = { Sales: true };
                categoryType                   = 'salescategory';
                $breadcrumbs.append('<div class="basetype breadcrumb"><div class="value">SALES</div></div>');
                $popup.find('#baseType').append(`<ul class="selected" data-value="SALES"><span class="downArrowNav"><i class="material-icons">keyboard_arrow_down</i></span><span>SALES</span></ul>`);
                $popup.find('#itemsearch').attr('data-base', 'SALES');
                break;
            case 'L':
                inventoryTypeRequest.uniqueids = { Labor: true };
                categoryType                   = 'laborcategory';
                $breadcrumbs.append('<div class="basetype breadcrumb"><div class="value">LABOR</div></div>');
                $popup.find('#baseType').append(`<ul class="selected" data-value="LABOR"><span class="downArrowNav"><i class="material-icons">keyboard_arrow_down</i></span><span>LABOR</span></ul>`);
                $popup.find('#itemsearch').attr('data-base', 'LABOR');
                break;
            case 'M':
                inventoryTypeRequest.uniqueids = { Misc: true };
                categoryType                   = 'misccategory';
                $breadcrumbs.append('<div class="basetype breadcrumb"><div class="value">MISC</div></div>');
                $popup.find('#baseType').append(`<ul class="selected" data-value="MISC"><span class="downArrowNav"><i class="material-icons">keyboard_arrow_down</i></span><span>MISC</span></ul>`);
                $popup.find('#itemsearch').attr('data-base', 'MISC');
                break;
            case 'P':
                inventoryTypeRequest.uniqueids = { Parts: true };
                categoryType                   = 'partscategory';
                $breadcrumbs.append('<div class="basetype breadcrumb"><div class="value">PARTS</div></div>');
                $popup.find('#baseType').append(`<ul class="selected" data-value="PARTS"><span class="downArrowNav"><i class="material-icons">keyboard_arrow_down</i></span><span>PARTS</span></ul>`);
                $popup.find('#itemsearch').attr('data-base', 'PARTS');
                break;
        }

        inventoryTypeRequest.searchfieldoperators = ["<>"];
        inventoryTypeRequest.searchfields         = ["Inactive"];
        inventoryTypeRequest.searchfieldvalues    = ["T"];
        inventoryTypeRequest.orderby              = "InventoryTypeOrderBy,OrderBy";

        FwAppData.apiMethod(true, 'POST', `api/v1/${categoryType}/browse`, inventoryTypeRequest, FwServices.defaultTimeout, function onSuccess(response) {
            let inventoryTypeIndex,
                inventoryTypeIdIndex;

            switch (categoryType) {
                case 'misccategory':
                    inventoryTypeIndex   = response.ColumnIndex.MiscType;
                    inventoryTypeIdIndex = response.ColumnIndex.MiscTypeId;
                    break;
                case 'laborcategory':
                    inventoryTypeIndex   = response.ColumnIndex.LaborType;
                    inventoryTypeIdIndex = response.ColumnIndex.LaborTypeId;
                    break;
                default:
                    inventoryTypeIndex   = response.ColumnIndex.InventoryType;
                    inventoryTypeIdIndex = response.ColumnIndex.InventoryTypeId;
                    break;
            }
            //Checks for duplicate inventory types. This loops through EVERY individual item and picks out the different inventory types.
            //Could be sped up by calling into an endpoint with JUST the inventory types.
            let types: any          = [];
            let inventoryTypeColumn = $popup.find('#inventoryType');
            inventoryTypeColumn.empty();
            for (let i = 0; i < response.Rows.length; i++) {
                if (types.indexOf(response.Rows[i][inventoryTypeIndex]) == -1) {
                    types.push(response.Rows[i][inventoryTypeIndex]);
                    inventoryTypeColumn.append(`<ul data-value="${response.Rows[i][inventoryTypeIdIndex]}" data-caption="${response.Rows[i][inventoryTypeIndex]}">
                                                  <span class="downArrowNav"><i class="material-icons">keyboard_arrow_down</i></span>
                                                  <span>${response.Rows[i][inventoryTypeIndex]}</span>
                                                </ul>`);
                }
            }
        }, null, $popup.find('#searchpopup'));
    }
    //----------------------------------------------------------------------------------------------
    renderInventory($popup, response) {
        let $inventoryContainer         = $popup.find('#inventory');
        let descriptionIndex            = response.ColumnIndex.Description,
            quantityAvailable           = response.ColumnIndex.QuantityAvailable,
            quantityAvailableAllWh      = response.ColumnIndex.QuantityAvailableAllWarehouses,
            conflictDate                = response.ColumnIndex.ConflictDate,
            quantityIn                  = response.ColumnIndex.QuantityIn,
            quantityQcRequired          = response.ColumnIndex.QuantityQcRequired,
            quantity                    = response.ColumnIndex.Quantity,
            priceIndex                  = response.ColumnIndex.Price,
            inventoryId                 = response.ColumnIndex.InventoryId,
            thumbnail                   = response.ColumnIndex.Thumbnail,
            appImageId                  = response.ColumnIndex.ImageId,
            classificationIndex         = response.ColumnIndex.Classification,
            classificationColor         = response.ColumnIndex.ClassificationColor,
            classificationDescription   = response.ColumnIndex.ClassificationDescription,
            textColor                   = response.ColumnIndex.TextColor,
            typeIndex                   = response.ColumnIndex.InventoryType,
            categoryIndex               = response.ColumnIndex.Category,
            subCategoryIndex            = response.ColumnIndex.SubCategory,
            availabilityStateIndex      = response.ColumnIndex.AvailabilityState,
            availabilityStateIndexAllWh = response.ColumnIndex.AvailabilityStateAllWarehouses,
            qtyIsStaleIndex             = response.ColumnIndex.QuantityAvailableIsStale,
            icode                       = response.ColumnIndex.ICode,
            partNumber                  = response.ColumnIndex.ManufacturerPartNumber,
            note                        = response.ColumnIndex.Note,
            dailyRateIndex              = response.ColumnIndex.DailyRate,
            weeklyRateIndex             = response.ColumnIndex.WeeklyRate,
            monthlyRateIndex            = response.ColumnIndex.MonthlyRate,
            laborMiscRateTypeIndex      = response.ColumnIndex.RateType;  

        $inventoryContainer.empty();
        $popup.find('.refresh-availability').hide();

        if (response.Rows.length == 0) {
            $inventoryContainer.append('<div style="flex: auto; font-weight: bold; font-size:1.3em;text-align: center;padding-top: 50px;">0 Items Found</div>');
        }

        const inventoryType = FwFormField.getValueByDataField($popup, 'InventoryType');

        const moduleType = $popup.find('#itemsearch').attr('data-moduletype');
        let rateType;
        if (moduleType === 'Main') {
            rateType = FwFormField.getValueByDataField($popup.find('#addToTab'), 'RateType');
        } else if (moduleType === 'Order' || moduleType === 'Quote') {
            if (typeof $popup.data('ratetype') != 'undefined') {
                rateType = $popup.data('ratetype');
            }
        }

        for (let i = 0; i < response.Rows.length; i++) {
            let imageThumbnail = response.Rows[i][thumbnail]  ? response.Rows[i][thumbnail]  : './theme/images/no-image.jpg';
            let imageId        = response.Rows[i][appImageId] ? response.Rows[i][appImageId] : '';
            let rate;
            switch (inventoryType) {
                case 'S':
                    rate = Number(response.Rows[i][priceIndex]).toFixed(2);
                        break;
                case 'R':
                    if (rateType == 'DAILY') {
                        rate = Number(response.Rows[i][dailyRateIndex]).toFixed(2);
                    } else if (rateType == 'WEEKLY' || rateType === '3WEEK') {
                        rate = Number(response.Rows[i][weeklyRateIndex]).toFixed(2);
                    } else if (rateType == 'MONTHLY') {
                        rate = Number(response.Rows[i][monthlyRateIndex]).toFixed(2);
                    } else {
                        rate = Number(response.Rows[i][priceIndex]).toFixed(2);
                    }
                    break;
                case 'L':
                case 'M':
                    const laborMiscRateType = response.Rows[i][laborMiscRateTypeIndex];
                    if (laborMiscRateType == 'RECURRING') {
                        if (rateType == 'DAILY') {
                            rate = Number(response.Rows[i][dailyRateIndex]).toFixed(2);
                        } else if (rateType == 'WEEKLY' || rateType === '3WEEK') {
                            rate = Number(response.Rows[i][weeklyRateIndex]).toFixed(2);
                        } else if (rateType == 'MONTHLY') {
                            rate = Number(response.Rows[i][monthlyRateIndex]).toFixed(2);
                        } else {
                            rate = Number(response.Rows[i][priceIndex]).toFixed(2);
                        }
                    } else if (laborMiscRateType == 'RECURRING') {
                        rate = Number(response.Rows[i][priceIndex]).toFixed(2);
                    }
                    break;
            }


            const location = JSON.parse(sessionStorage.getItem('location'));
            const defaultCurrencyId = location.defaultcurrencyid;
            const defaultCurrencySymbol = location.defaultcurrencysymbol;
            const currencyId = FwFormField.getValueByDataField($popup, 'CurrencyId');
            const currencySymbol = FwFormField.getValueByDataField($popup, 'CurrencySymbol');

            if (currencyId === '' && (typeof defaultCurrencyId != 'undefined') && (typeof defaultCurrencySymbol != 'undefined')) {
                rate = defaultCurrencySymbol + ' ' + rate;
            } else {
                rate = currencySymbol + ' ' + rate;
            }

            let conflictdate = response.Rows[i][conflictDate] ? moment(response.Rows[i][conflictDate]).format('L') : "";

            let itemhtml = `<div class="item-container" data-classification="${response.Rows[i][classificationIndex]}">
                              <div class="item-info" data-inventoryid="${response.Rows[i][inventoryId]}">
                                <div data-column="ItemImage"><img src="${imageThumbnail}" data-value="${imageId}" alt="Image" class="image"></div>
                                <div data-column="Description" class="columnorder">
                                    <div class="descriptionrow">
                                        <div class="description">
                                            ${response.Rows[i][descriptionIndex]}
                                        </div>
                                        <i class="material-icons opentab">more_horiz</i>
                                    </div>
                                </div>
                                <div data-column="Tags" class="columnorder"></div>
                                <div data-column="Type" class="columnorder showOnSearch">${response.Rows[i][typeIndex]}</div>
                                <div data-column="Category" class="columnorder showOnSearch">${response.Rows[i][categoryIndex]}</div>
                                <div data-column="SubCategory" class="columnorder showOnSearch">${response.Rows[i][subCategoryIndex]}</div>
                                <div data-column="Available" class="columnorder hideColumns"><div class="gridcaption">Available</div><div class="available-color value">${response.Rows[i][quantityAvailable]}</div></div>
                                <div data-column="ICode" class="columnorder"><div class="gridcaption">I-Code</div><div class="value">${response.Rows[i][icode]}</div></div>
                                <div data-column="PartNumber" class="columnorder"><div class="gridcaption">Part Number</div><div class="value">${response.Rows[i][partNumber]}</div></div>
                                <div data-column="ConflictDate" class="columnorder hideColumns"><div class="gridcaption">Conflict</div><div class="value">${conflictdate}</div></div>
                                <div data-column="AllWh" class="columnorder hideColumns"><div class="gridcaption">Available</div><div class="available-color value">${response.Rows[i][quantityAvailableAllWh]}</div></div>
                                <div data-column="In" class="columnorder"><div class="gridcaption">In</div><div class="value">${response.Rows[i][quantityIn]}</div></div>
                                <div data-column="QC" class="columnorder"><div class="gridcaption">QC</div><div class="value">${response.Rows[i][quantityQcRequired]}</div></div>
                                <div data-column="Note" class="columnorder note-button" style="display:none;"><div class="gridcaption">Note</div><textarea class="value">${response.Rows[i][note]}</textarea>${response.Rows[i][note].length > 0 ? '<i class="material-icons">insert_drive_file</i>' : ''}</div>
                                <div data-column="Rate" class="columnorder rate"><div class="gridcaption">Rate</div><div class="value">${rate}</div> </div>
                                <div data-column="Quantity" class="columnorder">
                                  <div class="gridcaption">Qty</div>
                                  <div style="float:left; border:1px solid #bdbdbd;">
                                    <button class="decrementQuantity" tabindex="-1">-</button>
                                    <input class="incrementvalue" type="number" min="0" style="padding: 5px 0px; float:left; width:50%; border:none; text-align:center;" value="${response.Rows[i][quantity]}">
                                    <button class="incrementQuantity" tabindex="-1">+</button>
                                  </div>
                                </div>
                              </div>
                            </div>`;
            let $itemcontainer = jQuery(itemhtml);
            $inventoryContainer.append($itemcontainer);

            if (response.Rows[i][quantity] != 0) {
                $itemcontainer.find('[data-column="Quantity"] input').addClass('lightBlue');
            }

            $itemcontainer.find('div[data-column="Available"]').attr('data-state', response.Rows[i][availabilityStateIndex]);
            $itemcontainer.find('div[data-column="AllWh"]').attr('data-state', response.Rows[i][availabilityStateIndexAllWh]);

            if (response.Rows[i][classificationIndex] == "K" || response.Rows[i][classificationIndex] == "C" || response.Rows[i][classificationIndex] == "N") {
                var $tag = jQuery('<div>').addClass('tag')
                    .html(response.Rows[i][classificationDescription])
                    .css({ 'background-color': response.Rows[i][classificationColor], 'color': response.Rows[i][textColor] });
                $itemcontainer.find('div[data-column="Tags"]').append($tag);
                $itemcontainer.find('div[data-column="Description"]').append('<div class="toggleaccessories">Show Accessories</div>');
                $itemcontainer.append(`<div class="item-accessories" data-classification="${response.Rows[i][classificationIndex]}" style="display:none;"></div>`);
            }

            if (response.Rows[i][qtyIsStaleIndex] === true) {
                $popup.find('.refresh-availability').show();
            }
        }

        let type = $popup.find('#itemsearch').attr('data-moduletype');
        if (type === 'PurchaseOrder' || type === 'Template' || inventoryType == 'M' || inventoryType == 'L') {
            $popup.find('.hideColumns').css('display', 'none');
        }

        this.listGridView($popup);
    }
    //----------------------------------------------------------------------------------------------
    listGridView($popup) {
        //custom display/sequencing for columns
        let columnsToHide = $popup.find('#itemsearch').data('columnstohide');
        if (typeof columnsToHide != 'undefined') {
            $popup.find('.columnDescriptions .columnorder, .item-info .columnorder').css('display', '');
            if (columnsToHide.length) {
                for (let i = 0; i < columnsToHide.length; i++) {
                    $popup.find(`.columnDescriptions [data-column="${columnsToHide[i]}"], .item-info [data-column="${columnsToHide[i]}"]`).hide();
                }
            }
        }

        const inventoryType = FwFormField.getValueByDataField($popup, 'InventoryType');
        let type = $popup.find('#itemsearch').attr('data-moduletype');
        if (type === 'PurchaseOrder' || type === 'Template' || inventoryType == 'M' || inventoryType == 'L') {
            $popup.find('.hideColumns').css('display', 'none');
        }

        let columnOrder = $popup.find('#itemsearch').data('columnorder');
        if (typeof columnOrder != 'undefined') {
            if (columnOrder.length) {
                for (let i = 0; i < columnOrder.length; i++) {
                    $popup.find(`.columnDescriptions [data-column="${columnOrder[i]}"], .item-info [data-column="${columnOrder[i]}"]`).css('order', i);
                }
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    setDefaultViewSettings($popup) {
        FwFormField.loadItems($popup.find('div[data-datafield="Columns"]'), this.DefaultColumns);

        FwFormField.setValueByDataField($popup, 'DisableAccessoryAutoExpand', false);
        FwFormField.setValueByDataField($popup, 'HideZeroQuantity', false);

        $popup.find('#itemlist').attr('data-view', 'GRID');

        //FwFormField.loadItems($popup.find('div[data-datafield="DefaultSelect"]'), [
        //    { value: '', text: 'All' },
        //    { value: 'CKN', text: 'Complete/Kit/Container', selected: true },
        //    { value: 'CK', text: 'Complete/Kit' },
        //    { value: 'N', text: 'Container' },
        //    { value: 'I', text: 'Item' },
        //    { value: 'A', text: 'Accessory' }
        //], true);

        //this.saveViewSettings($popup);
    }
    //----------------------------------------------------------------------------------------------
    getViewSettings($popup) {
        let userId = JSON.parse(sessionStorage.getItem('userid'));
        FwAppData.apiMethod(true, 'GET', `api/v1/usersearchsettings/${userId.webusersid}`, null, FwServices.defaultTimeout, response => {
            //Render options sortable column list
            if (response.ResultFields) {
                this.setViewSettings($popup, response);
                $popup.data('hassavedsettings', true);
            } else {
                this.setDefaultViewSettings($popup);
                $popup.data('hassavedsettings', false);
            }
        }, null, null);
    }
    //----------------------------------------------------------------------------------------------
    saveViewSettings($popup: JQuery, saveonly?: boolean) {
        let $searchpopup = $popup.find('#searchpopup');
        let $columns     = $popup.find('[data-datafield="Columns"] li');
        let columns      = [];
    
        for (let i = 0; i < $columns.length; i++) {
            let $this = jQuery($columns[i]);
            let column: any = {
                order:    i,
                value:    $this.attr('data-value'),
                text:     $this.find('label').text(),
                selected: $this.attr('data-selected') == 'T' ? 'T' : 'F'
            };
            columns.push(column);
        }
    
        let request: any = {
            ResultFields:                JSON.stringify(columns),
            WebUserId:                   JSON.parse(sessionStorage.getItem('userid')).webusersid,
            DisableAccessoryAutoExpand:  FwFormField.getValue2($popup.find('[data-datafield="DisableAccessoryAutoExpand"]')) == "T" ? true : false,
            HideZeroQuantity:            FwFormField.getValue2($popup.find('[data-datafield="HideZeroQuantity"]')) == "T" ? true : false,
            DefaultSelect:               FwFormField.getValue2($popup.find('[data-datafield="Select"]')),
            Mode:                        $popup.find('#itemlist').attr('data-view')
        };

        if (typeof $popup.data('hassavedsettings') != 'undefined' && $popup.data('hassavedsettings') === true) {
            FwAppData.apiMethod(true, 'PUT', `api/v1/usersearchsettings/${JSON.parse(sessionStorage.getItem('userid')).webusersid}`, request, FwServices.defaultTimeout,
                response => {
                    if (typeof saveonly == 'boolean' && saveonly) {
                        //do nothing
                    } else {
                        this.setViewSettings($popup, response);

                        if (request.DisableAccessoryAutoExpand) {
                            $popup.find('.item-accessories').css('display', 'none');
                        }
                    }
                }, null, $searchpopup);
        } else {
            FwAppData.apiMethod(true, 'POST', `api/v1/usersearchsettings/`, request, FwServices.defaultTimeout,
                response => {
                    if (typeof saveonly == 'boolean' && saveonly) {
                        //do nothing
                    } else {
                        this.setViewSettings($popup, response);
                        if (request.DisableAccessoryAutoExpand) {
                            $popup.find('.item-accessories').css('display', 'none');
                        }
                    }
                    $popup.data('hassavedsettings', true);
                }, null, $searchpopup);
        }
    }
    //----------------------------------------------------------------------------------------------
    setViewSettings($popup, response) {
        let savedColumns = JSON.parse(response.ResultFields);
        //compares to the default options and adds missing values for cases where we add new options
        if (savedColumns.length != this.DefaultColumns.length) {
            let columnsToAdd = this.DefaultColumns.filter(cols => {
                return !savedColumns.find(e => {
                    return e.value == cols.value;
                });
            });
            savedColumns = savedColumns.concat(columnsToAdd);
        }
        FwFormField.loadItems($popup.find('div[data-datafield="Columns"]'), savedColumns);

        let columnsToHide = [];
        let columnOrder   = [];
        for (let i = 0; i < savedColumns.length; i++) {
            let $this = savedColumns[i];
            columnOrder.push($this.value);
            if ($this.selected == 'F') {
                columnsToHide.push($this.value);
            }
        }
        $popup.find('#itemsearch').data('columnorder', columnOrder);
        $popup.find('#itemsearch').data('columnstohide', columnsToHide);

        //FwFormField.loadItems($popup.find('div[data-datafield="DefaultSelect"]'), [
        //    { value: '',    text: 'All' },
        //    { value: 'CKN', text: 'Complete/Kit/Container', selected: true },
        //    { value: 'CK',  text: 'Complete/Kit' },
        //    { value: 'N',   text: 'Container' },
        //    { value: 'I',   text: 'Item' },
        //    { value: 'A',   text: 'Accessory' }
        //], true);

        FwFormField.setValueByDataField($popup, 'DisableAccessoryAutoExpand', response.DisableAccessoryAutoExpand);
        FwFormField.setValueByDataField($popup, 'HideZeroQuantity', response.HideZeroQuantity);
        //FwFormField.setValueByDataField($popup, 'DefaultSelect', response.DefaultSelect);
        FwFormField.setValueByDataField($popup, 'Select', response.DefaultSelect/*, null, true*/); //jason h - 06/11/20 the "true" flag here was triggering the settings to save again, overwriting previously saved settings

        $popup.find('#itemlist').attr('data-view', response.Mode);
        this.listGridView($popup);
    }
    //----------------------------------------------------------------------------------------------
    events($popup, $form, id) {
        let hasItemInGrids = false;
        let warehouseId    = $popup.find('#itemsearch').data('warehouseid');
        let warehouseText  = $popup.find('#itemsearch').data('warehousetext');
        let $searchpopup   = $popup.find('#searchpopup');

        $popup
            .on('changeDate', '#itemsearch div[data-datafield="PickDate"], #itemsearch div[data-datafield="FromDate"], #itemsearch div[data-datafield="ToDate"]', e => {
                if ($popup.find('#inventory').children().length > 0) {
                    this.getInventory($popup);
                }
            })
            .on('change', '#itemsearch div[data-datafield="PickTime"], #itemsearch div[data-datafield="FromTime"], #itemsearch div[data-datafield="ToTime"]', e => {
                if ($popup.find('#inventory').children().length > 0) {
                    this.getInventory($popup);
                }
            })
            .on('click', '#breadcrumbs .basetype', e => {
                FwFormField.setValueByDataField($popup, 'SearchBox', '');
                $popup.find('.refresh-availability').hide();
                this.populateTypeMenu($popup);
            })
            .on('click', '#breadcrumbs .type', e => {
                let inventorytypeid = $popup.find('#itemsearch').attr('data-inventorytypeid');
                $popup.find(`#inventoryType ul[data-value="${inventorytypeid}"]`).click();
            })
            .on('click', '#breadcrumbs .category', e => {
                let categoryid = $popup.find('#itemsearch').attr('data-categoryid');
                $popup.find(`#category ul[data-value="${categoryid}"]`).click();
            })
            .on('click', '#baseType ul', e => {
                FwFormField.setValueByDataField($popup, 'SearchBox', '');
                $popup.find('.refresh-availability').hide();
                this.populateTypeMenu($popup);
            })
            .on('click', '#inventoryType ul', e => {
                const $this = jQuery(e.currentTarget);
                FwFormField.setValueByDataField($popup, 'SearchBox', '');
                $popup.find('.refresh-availability').hide();
                $popup.find('#inventoryType ul').removeClass('selected');
                $this.addClass('selected');
                $popup.find('#inventoryType ul').not('.selected').hide();

                //Clear out existing bread crumbs and start new one
                let $breadcrumbs = $popup.find('#breadcrumbs');
                $breadcrumbs.find('.type, .category, .subcategory').remove();
                $breadcrumbs.append(`<div class="type breadcrumb"><div class="value">${$this.attr('data-caption')}</div></div>`);

                $popup.find('#itemsearch').attr('data-inventorytypeid', $this.attr('data-value')).attr('data-categoryid', '').attr('data-subcategoryid', '');

                let categoryType;
                switch (FwFormField.getValue($popup, '[data-datafield="InventoryType"]')) {
                    case 'R': categoryType = 'rentalcategory'; break;
                    case 'S': categoryType = 'salescategory';  break;
                    case 'L': categoryType = 'laborcategory';  break;
                    case 'M': categoryType = 'misccategory';   break;
                    case 'P': categoryType = 'partscategory';  break;
                }

                let typeRequest: any = {
                    searchfieldoperators: ["<>"],
                    searchfields:         ["Inactive"],
                    searchfieldvalues:    ["T"],
                    orderby:              "OrderBy"
                };

                switch (categoryType) {
                    case 'misccategory':  typeRequest.uniqueids = { MiscTypeId:      $this.attr('data-value') }; break;
                    case 'laborcategory': typeRequest.uniqueids = { LaborTypeId:     $this.attr('data-value') }; break;
                    default:              typeRequest.uniqueids = { InventoryTypeId: $this.attr('data-value') }; break;
                }

                FwAppData.apiMethod(true, 'POST', `api/v1/${categoryType}/browse`, typeRequest, FwServices.defaultTimeout, function onSuccess(response) {
                    let categoryIdIndex     = response.ColumnIndex.CategoryId;
                    let categoryIndex       = response.ColumnIndex.Category;
                    let inventoryCountIndex = response.ColumnIndex.InventoryCount;

                    $popup.find('#category, #subCategory').empty();
                    $popup.find('#inventory').empty();

                    let categories: any = [];
                    let categoryColumn  = $popup.find('#category');
                    for (let i = 0; i < response.Rows.length; i++) {
                        if (categories.indexOf(response.Rows[i][categoryIndex]) == -1 && (response.Rows[i][inventoryCountIndex] != 0)) {
                            categories.push(response.Rows[i][categoryIndex]);
                            categoryColumn.append(`<ul data-value="${response.Rows[i][categoryIdIndex]}" data-caption="${response.Rows[i][categoryIndex]}">
                                                     <span class="downArrowNav"><i class="material-icons">keyboard_arrow_down</i></span>
                                                     <span>${response.Rows[i][categoryIndex]}</span>
                                                   </ul>`);
                        }
                    }
                    if (response.Rows.length === 1) {
                        $popup.find("#category > ul").trigger('click');
                    }
                }, null, $searchpopup);
            })
            .on('click', '#category ul', e => {
                const $this = jQuery(e.currentTarget);
                $popup.find('#category ul').removeClass('selected');
                $this.addClass('selected');

                //Clear and set new breadcrumbs
                let $breadcrumbs = $popup.find('#breadcrumbs');
                $breadcrumbs.find('.category, .subcategory').remove();
                $breadcrumbs.append(`<div class="category breadcrumb"><div class="value">${$this.attr('data-caption')}</div></div>`);

                $popup.find('#itemsearch').attr('data-categoryid', $this.attr('data-value')).attr('data-subcategoryid', '');

                let subCatListRequest: any = {};
                subCatListRequest.orderby = "OrderBy";
                subCatListRequest.uniqueids = {
                    CategoryId: $this.attr('data-value'),
                    TypeId:     $popup.find('#itemsearch').attr('data-inventorytypeid'),
                    RecType:    FwFormField.getValueByDataField($popup, 'InventoryType')
                }

                FwAppData.apiMethod(true, 'POST', "api/v1/subcategory/browse", subCatListRequest, FwServices.defaultTimeout,
                    response => {
                    let subCategoryIdIndex = response.ColumnIndex.SubCategoryId;
                    let subCategoryIndex   = response.ColumnIndex.SubCategory;
                    $popup.find('#subCategory').empty();

                    //Load the Inventory items if selected category doesn't have any sub-categories
                    if (response.Rows.length == 0) {
                        this.getInventory($popup);
                    } else {
                        $popup.find('#inventory').empty();
                        $popup.find('#category ul').not('.selected').hide();

                        let subCategories: any = [];
                        let subCategoryColumn  = $popup.find('#subCategory');
                        for (let i = 0; i < response.Rows.length; i++) {
                            if (subCategories.indexOf(response.Rows[i][subCategoryIndex]) == -1) {
                                subCategories.push(response.Rows[i][subCategoryIndex]);
                                subCategoryColumn.append(`<ul data-value="${response.Rows[i][subCategoryIdIndex]}" data-caption="${response.Rows[i][subCategoryIndex]}">
                                                            <span>${response.Rows[i][subCategoryIndex]}</span>
                                                          </ul>`);
                            }
                        }
                        if (response.Rows.length == 1) {
                            $popup.find("#subCategory > ul").trigger('click');
                        }
                    }
                }, null, $searchpopup);
            })
            .on('click', '#subCategory ul', e => {
                const $this = jQuery(e.currentTarget);

                $popup.find('#subCategory ul').removeClass('selected');
                $this.addClass('selected');

                //Clear and set breadcrumbs
                let $breadcrumbs = $popup.find('#breadcrumbs');
                $breadcrumbs.find('.subcategory').remove();
                $breadcrumbs.append(`<div class="subcategory breadcrumb"><div class="value">${$this.attr('data-caption')}</div></div>`);

                $popup.find('#itemsearch').attr('data-subcategoryid', $this.attr('data-value'));
                this.getInventory($popup);
            })
            ;

        //Close the Search Interface popup
        $popup.find('.close-modal').one('click', e => {
            FwPopup.destroyPopup($popup);
            jQuery(document).find('.fwpopup').off('click');
            jQuery(document).off('keydown');
        });

        $popup
            .on('click', '.options .optionsbutton', (event: JQuery.ClickEvent) => {
                try {
                    var $options = jQuery(event.currentTarget).parent();
                    if (!$options.hasClass('active')) {
                        var maxZIndex = FwFunc.getMaxZ('*');
                        $options.find('.optionsmenu').css('z-index', maxZIndex + 1);
                        $options.addClass('active');

                        jQuery(document).one('click', function closeMenu(e: JQuery.ClickEvent) {
                            try {
                                if (($options.has(<Element>e.target).length === 0)) {
                                    $options.removeClass('active');
                                    $options.find('.optionsmenu').css('z-index', '0');
                                } else if ($options.hasClass('active')) {
                                    jQuery(document).one('click', closeMenu);
                                }
                            } catch (ex) {
                                FwFunc.showError(ex);
                            }
                        });
                    } else {
                        $options.removeClass('active');
                        $options.find('.optionsmenu').css('z-index', '0');
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.options .applyOptions', e => {
                this.saveViewSettings($popup);
                $popup.find('.options').removeClass('active');
                $popup.find('.options .optionsmenu').css('z-index', '0');

                //perform search again with new settings
                if ($popup.find('#inventory').children().length > 0) {
                    this.getInventory($popup);
                }
            })
            .on('click', '.options .restoreDefaults', e => {
                this.setDefaultViewSettings($popup);
                $popup.find('.options').removeClass('active');
                $popup.find('.options .optionsmenu').css('z-index', '0');
                this.listGridView($popup);
            })
            ;

        $popup.on('change', 'div[data-datafield="InventoryType"]', e => {
            this.populateTypeMenu($popup);
            const searchValue = FwFormField.getValueByDataField($popup, 'SearchBox');
            const event = jQuery.Event("keydown", { keyCode: 13 });
            if (searchValue != '') {
                $popup.find('[data-datafield="SearchBox"] input.fwformfield-value').trigger(event);
            }

            const inventoryType = FwFormField.getValue($popup, '[data-datafield="InventoryType"]');
            if (inventoryType == 'L' || inventoryType == 'M') {
                $popup.find('[data-datafield="Select"]').hide();
            } else {
                $popup.find('[data-datafield="Select"]').show();
            }

        });

        //Filter results based on Search input field
        $popup.find('[data-datafield="SearchBox"]').on('keydown', 'input.fwformfield-value', e => {
            let code = e.keyCode || e.which;
            try {
                if (code === 13) { //Enter Key
                    this.populateTypeMenu($popup);
                    this.getInventory($popup);
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        //double-click to increment quantity
        //$popup.on('dblclick', '#inventory .item-container .item-info', e => {
        //    let $iteminfo = jQuery(e.currentTarget);
        //    $iteminfo.find('.incrementQuantity').click();
        //});

        $popup.on('click', '.toggleaccessories', e => {
            let $el = jQuery(e.currentTarget);
            let $iteminfo;
            let $accessoryContainer;
            let inventoryId;
            if ($el.attr('data-isnestedaccessory') === 'true') {
                $iteminfo = $el.closest('.item-accessory-info');
                inventoryId = $iteminfo.attr('data-inventoryid');
                $accessoryContainer = $iteminfo.siblings(`.nested-accessories[data-parentid="${inventoryId}"]`);
            } else {
                $iteminfo = $el.closest('.item-info');
                inventoryId = $iteminfo.attr('data-inventoryid');
                $accessoryContainer = $iteminfo.siblings('.item-accessories');
            }

            if ($accessoryContainer.is(':visible')) {
                jQuery(e.currentTarget).text('Show Accessories');
            } else {
                jQuery(e.currentTarget).text('Hide Accessories');
                this.refreshAccessoryQuantity($popup, id, warehouseId, inventoryId, $el);
            }

            if ((jQuery('#itemlist').attr('data-view')) == 'GRID') {
                jQuery('.accColumns').show();
            }

            $accessoryContainer.slideToggle('slow');
        });

        //On Quantity input change
        $popup
            .on('click', '.item-info [data-column="Quantity"] input', e => {
                jQuery(e.currentTarget).select();
            })
            .on('change', '.item-info [data-column="Quantity"] input', e => {
                e.stopPropagation();

                let $element      = jQuery(e.currentTarget);
                let quantity      = $element.val();
                const item        = $element.parents('.item-info');
                const inventoryId = item.attr('data-inventoryid');
                const fromDate    = FwFormField.getValueByDataField($popup, 'FromDate');
                const toDate      = FwFormField.getValueByDataField($popup, 'ToDate');
                const pickDate    = FwFormField.getValueByDataField($popup, 'PickDate');
                const currencyId  = FwFormField.getValueByDataField($popup, 'CurrencyId');
                const view        = $popup.find('#itemlist').attr('data-view');
                let request: any  = {
                    OrderId:     id,
                    SessionId:   id,
                    InventoryId: inventoryId,
                    WarehouseId: warehouseId,
                    Quantity:    quantity,
                    FromDate:    pickDate != "" ? pickDate : fromDate,
                    ToDate:      toDate
                }

                if (currencyId != '') {
                    request.CurrencyId = currencyId;
                }

                if (quantity > 0) {
                    hasItemInGrids = true;
                }

                quantity != 0 ? $element.addClass('lightBlue') : $element.removeClass('lightBlue');

                let $accContainer = $element.parents('.item-container').find('.item-accessories');
                let accessoryRefresh = $popup.find('.toggleAccessories input').prop('checked');
                FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/", request, FwServices.defaultTimeout,
                    response => {
                        const $showAccessoriesBtn = $element.parents('.item-info').find('[data-column="Description"] .toggleaccessories');
                        const accLinkText = $showAccessoriesBtn.text();
                        //opens accessory list based on Auto-Expansion option value
                        if (accessoryRefresh == false) {
                            if ($accContainer.length > 0) {
                                this.refreshAccessoryQuantity($popup, id, warehouseId, inventoryId, $showAccessoriesBtn);
                                if (view != 'GRID') {
                                    $accContainer.show();
                                    $showAccessoriesBtn.text('Hide Accessories');
                                }
                            }
                        } else if (accLinkText == 'Hide Accessories') { // if accessories are showing, update them
                            this.refreshAccessoryQuantity($popup, id, warehouseId, inventoryId, $showAccessoriesBtn);
                        }

                        item.find('[data-column="Available"]')
                            .attr('data-state', response.AvailabilityState)
                            .find('.value')
                            .text(response.QuantityAvailable);

                        item.find('[data-column="AllWh"]')
                            .attr('data-state', response.AvailabilityStateAllWarehouses)
                            .find('.value')
                            .text(response.QuantityAvailableAllWarehouses);

                        let conflictdate = response.ConflictDate ? moment(response.ConflictDate).format('L') : '';
                        item.find('[data-column="ConflictDate"] .value').text(conflictdate);

                        //Updates Preview tab with total # of items
                        $popup.find('.tab[data-caption="Preview"] .caption').text(`Preview (${response.TotalQuantityInSession} items)`);
                    }, ex => FwFunc.showError(ex), $searchpopup);
            })
            .on('click', '.item-accessory-info [data-column="Quantity"] input', e => {
                jQuery(e.currentTarget).select();
            })
            .on('change', '.item-accessory-info [data-column="Quantity"] input', e => {
                const $element      = jQuery(e.currentTarget);
                let quantity        = $element.val();
                const $item         = $element.parents('.item-accessory-info');
                const inventoryId   = $element.parents('.item-accessory-info').attr('data-inventoryid');
                const fromDate      = FwFormField.getValueByDataField($popup, 'FromDate');
                const toDate        = FwFormField.getValueByDataField($popup, 'ToDate');
                const pickDate = FwFormField.getValueByDataField($popup, 'PickDate');
                const currencyId = FwFormField.getValueByDataField($popup, 'CurrencyId');
                const view = $popup.find('#itemlist').attr('data-view');
                let accRequest: any = {
                    SessionId:   id,
                    InventoryId: inventoryId,
                    WarehouseId: warehouseId,
                    Quantity:    quantity,
                    FromDate:    pickDate != "" ? pickDate : fromDate,
                    ToDate:      toDate
                };

                if (currencyId != '') {
                    accRequest.CurrencyId = currencyId;
                }

                if ($element.parents('.nested-accessories').length) {
                    accRequest.ParentId = $element.parents('.nested-accessories').attr('data-parentid');
                    accRequest.GrandParentId = $element.parents('.item-container').find('.item-info').attr('data-inventoryid');
                } else {
                    accRequest.ParentId = $element.parents('.item-container').find('.item-info').attr('data-inventoryid');
                }

                accRequest.Quantity != "0" ? $element.addClass('lightBlue') : $element.removeClass('lightBlue');
                let $accContainer = $element.parents('.item-accessories').find('.nested-accessories');
                let accessoryRefresh = $popup.find('.toggleAccessories input').prop('checked');
                FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch", accRequest, FwServices.defaultTimeout,
                    response => {
                        const $showAccessoriesBtn = $item.find('[data-column="Description"] .toggleaccessories');
                        const accLinkText = $showAccessoriesBtn.text();
                        //opens accessory list based on Auto-Expansion option value
                        if (accessoryRefresh == false) {
                            if ($accContainer.length > 0) {
                                this.refreshAccessoryQuantity($popup, id, warehouseId, inventoryId, $showAccessoriesBtn);
                                if (view != 'GRID') {
                                    $accContainer.show();
                                    $showAccessoriesBtn.text('Hide Accessories');
                                }
                            }
                        } else if (accLinkText == 'Hide Accessories') {
                            this.refreshAccessoryQuantity($popup, id, warehouseId, inventoryId, $showAccessoriesBtn);
                        }

                        $item.find('[data-column="Available"]')
                            .attr('data-state', response.AvailabilityState)
                            .find('.value')
                            .text(response.QuantityAvailable);

                        $item.find('[data-column="AllWh"]')
                            .attr('data-state', response.AvailabilityStateAllWarehouses)
                            .find('.value')
                            .text(response.QuantityAvailableAllWarehouses);

                        $item.find('[data-column="ConflictDate"] value').text(response.ConflictDate || "");

                        //Updates Preview tab with total # of items
                        $popup.find('.tab[data-caption="Preview"] .caption').text(`Preview (${response.TotalQuantityInSession} items)`);
                    }, ex => FwFunc.showError(ex), $searchpopup);
            });

        //Update Preview grid tab
        $popup.on('click', '.tab[data-caption="Preview"]', e => {
            this.refreshPreviewGrid($popup, id);
            let type = $popup.find('#itemsearch').attr('data-moduletype');
            if (type === 'PurchaseOrder' || type === 'Template') {
                $popup.find('[data-type="Grid"] .hideColumns').closest('td').css('display', 'none');
            }
        });

        //Image preview confirmation box
        $popup.on('click', '.image', e => {
            e.stopPropagation();
            let imageId = jQuery(e.currentTarget).attr('data-value');
            if (imageId !== '') {
                let $confirmation = FwConfirmation.renderConfirmation('Image Viewer',
                    `<div style="white-space:pre;">\n<img src="${applicationConfig.apiurl}api/v1/appimage/getimage?appimageid=${imageId}&thumbnail=false" data-value="${imageId}" alt="No Image" class="image" style="max-width:100%;">`);
                $confirmation.find('.message').css({
                    'text-align': 'center'
                })
                FwConfirmation.addButton($confirmation, 'Close');
            }
        });

        //Add to Order click event
        $popup.on('click', '.addToOrder', e => {
            if (!hasItemInGrids) {
                let previewGridHasItems = $popup.find('[data-grid="SearchPreviewGrid"] tbody').children().length > 0;
                if (!previewGridHasItems) {
                    FwNotification.renderNotification('WARNING', 'No items have been added.');
                } else {
                    this.addToOrder($popup, $form, 'Add');
                }
            } else {
                this.addToOrder($popup, $form, 'Add');
            }
        });

        //insert at line
        $popup.on('click', '.insertAtLine', e => {
            this.addToOrder($popup, $form, 'Insert');
        });

        //Saves the user's inventory view setting
        $popup.on('click', '.invviewbtn', e => {
            let $this = jQuery(e.currentTarget),
                view  = $this.attr('data-buttonview');

            $popup.find('#itemlist').attr('data-view', view);
            this.saveViewSettings($popup);
        });

        $popup.on('change', 'div[data-datafield="Select"]', e => {
            this.getInventory($popup);
            this.saveViewSettings($popup, true);
        });

        //Refresh Availability button
        $popup.on('click', '.refresh-availability', e => {
            this.getInventory($popup);
        });

        $popup.on('click', '.acc-refresh-avail', e => {
            const inventoryId = jQuery(e.currentTarget).parents('.item-container').find('.item-info').attr('data-inventoryid');
            $popup.data('refreshaccessories', true);
            this.refreshAccessoryQuantity($popup, id, warehouseId, inventoryId, e);
        })

        //Increment and decrement buttons
        $popup.on('click', '.incrementQuantity, .decrementQuantity', e => {
            e.stopPropagation();
            const $button = jQuery(e.currentTarget),
                oldValue = $button.parent().find("input").val();

            if ($button.text() == "+") {
                var newVal = (+oldValue + 1);
            } else {
                if (oldValue > 0) {
                    newVal = (+oldValue - 1);
                } else {
                    newVal = 0;
                }
            }
            $button.parent().find("input").val(newVal).change();
        });

        //prevent negative numbers
        $popup.on('keydown', '.incrementvalue', (e) => {
            if (!((e.keyCode > 95 && e.keyCode < 106)
                || (e.keyCode > 47 && e.keyCode < 58)
                || e.keyCode == 8 || e.keyCode == 9
                || (e.keyCode > 36 && e.keyCode < 41))) {
                return false;
            }
        });

        $popup.on('click', '.opentab', e => {
            const type = FwFormField.getValueByDataField($popup, 'InventoryType');
            const uniqueids: any = {};
            let module;
            let inventoryId;
            if (jQuery(e.currentTarget).hasClass('is-accessory')) {
                inventoryId = jQuery(e.currentTarget).closest('.item-accessory-info').attr('data-inventoryid');
            } else {
                inventoryId = jQuery(e.currentTarget).closest('.item-info').attr('data-inventoryid');
            }
            switch (type) {
                case 'R':
                    module = 'RentalInventory';
                    uniqueids.InventoryId = inventoryId;
                    break;
                case 'S':
                    module = 'SalesInventory';
                    uniqueids.InventoryId = inventoryId;
                    break;
                case 'L':
                    module = 'LaborRate';
                    uniqueids.RateId = inventoryId;
                    break;
                case 'M':
                    module = 'MiscRate';
                    uniqueids.RateId = inventoryId;
                    break;
            }
            const title = jQuery(e.currentTarget).siblings().text().trim();
            const $popupForm = (<any>window)[`${module}Controller`].loadForm(uniqueids);
            FwPopup.showPopup(FwPopup.renderPopup($popupForm, undefined, title, inventoryId));
            let $fwcontrols = $popupForm.find('.fwcontrol');
            FwControl.loadControls($fwcontrols);
            $popupForm.find('.btnpeek').remove();
            $popupForm.css({ 'background-color': 'white', 'box-shadow': '0 25px 44px rgba(0, 0, 0, 0.30), 0 20px 15px rgba(0, 0, 0, 0.22)', 'width': '75vw', 'height': '75vh', 'overflow': 'scroll', 'position': 'relative' });
        });

        $popup.on('click', '.note-button', e => {
            e.stopPropagation();
            let $confirmation, controlhtml;
            let $notes = jQuery(e.currentTarget).find('textarea').text();
            $confirmation = FwConfirmation.renderConfirmation('Note', '');
            FwConfirmation.addButton($confirmation, 'Close', true);
            controlhtml = [];
            controlhtml.push('<div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield note" data-caption="Note" data-enabled="false" data-datafield=""></div>');
            FwConfirmation.addControls($confirmation, controlhtml.join('\n'));
            FwFormField.setValue($confirmation, '.note', $notes);
            $confirmation.find('.note textarea')
                .css({
                    'width': '500px',
                    'max-width': '500px',
                    'height': '400px',
                    'resize': 'both'
                });
        });

        if (jQuery('html').hasClass('desktop')) {
            let interval: any = 0;
            $popup
                .on('mousedown', '.incrementQuantity, .decrementQuantity', e => {
                    const $button = jQuery(e.currentTarget),
                        oldValue = $button.parent().find("input").val(),
                        $input = $button.parent().find("input");

                    if ($button.text() == "+") {
                        interval = setInterval(function () {
                            increment();
                        }, 300);
                    } else {
                        interval = setInterval(function () {
                            decrement();
                        }, 300)
                    }

                    let newValue = +oldValue;

                    function increment() {
                        $input.val(++newValue);
                        if (!($input.hasClass('changed'))) {
                            $input.addClass('changed');
                        }
                    }

                    function decrement() {
                        $input.val(--newValue);
                        if (!($input.hasClass('changed'))) {
                            $input.addClass('changed');
                        }
                    }
                })
                .on('mouseup mouseleave', '.incrementQuantity, .decrementQuantity', e => {
                    const $button = jQuery(e.currentTarget),
                        $input = $button.parent().find("input");

                    clearInterval(interval);
                    if ($input.hasClass('changed')) {
                        $input.removeClass('changed');
                        $input.change();
                    }
                })
        }

        $popup.find('[data-datafield="SearchBox"] input').focus();

        //availability calendar popup
        $popup.on('click', '#inventory [data-column="Available"] .value', e => {
            const $item = jQuery(e.currentTarget).parents('[data-inventoryid]');
            $item.data('warehouseid', warehouseId);
            $item.data('warehousetext', warehouseText);
            this.renderAvailabilityPopup($item);
        });
    }
    //----------------------------------------------------------------------------------------------
    renderAvailabilityPopup($item: JQuery) {
        const inventoryId = $item.attr('data-inventoryid');
        if (inventoryId) {
            let $popup = jQuery(`
                        <div>
                            <div class="close-modal" style="background-color:white;top:.8em;right:.1em; padding-right:.5em; border-radius:.2em;justify-content:flex-end;"><i class="material-icons">clear</i><div class="btn-text">Close</div></div>
                            <div id="availabilityCalendarPopup" class="fwform fwcontrol fwcontainer" data-control="FwContainer" data-type="form" style="overflow:auto;max-height:90vh;max-width:90vw;background-color:white; margin-top:2em; border:2px solid gray;">
                              <div class="flexrow" style="overflow:auto;">
                                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Availability">
                                  <div class="flexrow">
                                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="ICode" data-enabled="false" style="flex:0 1 100px;"></div>
                                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" data-enabled="false" style="flex:0 1 500px;"></div>
                                  </div>
                                        <div class="flexrow inv-data-totals">
                                            <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield warehousefilter" data-caption="Filter By Warehouse" data-datafield="WarehouseId" data-validationname="WarehouseValidation" data-displayfield="WarehouseCode" style="max-width:400px; margin-bottom:15px;"></div>
                                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="All Warehouses" data-datafield="AllWarehouses" style="flex:0 1 150px; margin-top:.5em; margin-left: 1em;"></div>                                      
                                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="Total" data-enabled="false" data-totalfield="Total" style="flex:0 1 85px"></div>
                                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield totals" data-caption="In" data-datafield="In" data-enabled="false" data-totalfield="In" style="flex:0 1 85px;"></div>
                                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield totals" data-caption="QC  Req'd" data-datafield="QcRequired" data-enabled="false" data-totalfield="QcRequired" style="flex:0 1 85px;"></div>
                                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield totals" data-caption="In Container" data-datafield="InContainer" data-enabled="false" data-totalfield="InContainer" style="flex:0 1 85px;"></div>
                                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield totals" data-caption="Staged" data-datafield="Staged" data-enabled="false" data-totalfield="Staged" style="flex:0 1 85px;"></div>
                                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield totals" data-caption="Out" data-datafield="Out" data-enabled="false" data-totalfield="Out" style="flex:0 1 85px;"></div>
                                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield totals" data-caption="In Repair" data-datafield="InRepair" data-enabled="false" data-totalfield="InRepair" style="flex:0 1 85px;"></div>
                                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield totals" data-caption="In Transit" data-datafield="InTransit" data-enabled="false" data-totalfield="InTransit" style="flex:0 1 85px;"></div>
                                        </div>
                                      <div class="flexrow" style="overflow:auto;">
                                        <div data-control="FwScheduler" class="fwcontrol fwscheduler calendar"></div>
                                      </div>
                                      <div class="flexrow schedulerrow" style="display:block;">
                                        <div data-control="FwSchedulerDetailed" class="fwcontrol fwscheduler realscheduler"></div>
                                        <div class="fwbrowse"><div class="legend"></div></div>
                                     </div>
                                    </div>
                                </div>
                              </div>
                            </div>`);
            FwControl.renderRuntimeControls($popup.find('.fwcontrol'));
            $popup = FwPopup.renderPopup($popup, { ismodal: true });
            FwPopup.showPopup($popup);
            //$form.data('onscreenunload', () => { FwPopup.destroyPopup($popup); });

            $popup.find('.close-modal').on('click', e => {
                FwPopup.detachPopup($popup);
            });

            $item.data('fromQuikSearch', true);
            const warehouseId = $item.data('warehouseid');
            const warehouseText = $item.data('warehousetext');
            FwFormField.setValue2($popup.find('.warehousefilter'), warehouseId, warehouseText);
            $item.data('warehousefilter', warehouseId);
            $item.data('allwarehousesfilter', 'F');
            let iCode;
            if ($item.hasClass('item-accessory-info')) {
                iCode = $item.find('[data-column="ICode"]').text();
            } else {
                iCode = $item.find('[data-column="ICode"] .value').text();
            }
            FwFormField.setValue2($popup.find('div[data-datafield="ICode"]'), iCode);
            const description = $item.find('.description').text().trim();
            FwFormField.setValue2($popup.find('div[data-datafield="Description"]'), description);


            const $calendar = $popup.find('.calendar');
            FwScheduler.renderRuntimeHtml($calendar);
            FwScheduler.init($calendar);
            RentalInventoryController.addCalSchedEvents($item, $calendar, inventoryId);
            FwScheduler.loadControl($calendar);
            const schddate = FwScheduler.getTodaysDate();
            FwScheduler.navigate($calendar, schddate);
            FwScheduler.refresh($calendar);
           
            const $scheduler = $popup.find('.realscheduler');
            FwSchedulerDetailed.renderRuntimeHtml($scheduler);
            FwSchedulerDetailed.init($scheduler);
            RentalInventoryController.addCalSchedEvents($item, $scheduler, inventoryId);
            FwSchedulerDetailed.loadControl($scheduler);
            FwSchedulerDetailed.navigate($scheduler, schddate, 35);
            FwSchedulerDetailed.refresh($scheduler);

            try {
                if ($scheduler.hasClass('legend-loaded') === false) {
                    FwAppData.apiMethod(true, 'GET', 'api/v1/rentalinventory/availabilitylegend', null, FwServices.defaultTimeout,
                        response => {
                            for (let key in response) {
                                FwBrowse.addLegend($popup.find('.schedulerrow .fwbrowse'), key, response[key]);
                            }
                            $scheduler.addClass('legend-loaded');
                        }, ex => {
                            FwFunc.showError(ex);
                        }, $scheduler);
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }

            $popup.on('change', '.warehousefilter', e => {
                const whFilter = FwFormField.getValue2($popup.find('.warehousefilter'));
                $item.data('warehousefilter', whFilter);
                FwScheduler.refresh($calendar);
                FwSchedulerDetailed.refresh($scheduler);
            });

            $popup.on('change', '[data-datafield="AllWarehouses"]', e => {
                const $this = jQuery(e.currentTarget);
                const allWh = FwFormField.getValue2($this);
                if (allWh == 'T') {
                    FwFormField.disable($popup.find('[data-datafield="WarehouseId"]'));
                } else {
                    FwFormField.enable($popup.find('[data-datafield="WarehouseId"]'));
                }
                $item.data('allwarehousesfilter', allWh);
                const $calendar = $popup.find('.calendar');
                const $realScheduler = $popup.find('.realscheduler');
                FwSchedulerDetailed.refresh($realScheduler);
                FwScheduler.refresh($calendar);
            });
        }
    }
    //----------------------------------------------------------------------------------------------
    addToOrder($popup: JQuery, $form: JQuery, actionType: string) {
        let request: any = {};
        let newRecordInfo;
        const $searchpopup = $popup.find('#searchpopup');
        const id = $popup.find('#itemsearch').data('parentformid');
        const type = $popup.find('#itemsearch').attr('data-moduletype');
        let apiUrl: string = "addtoorder";
        request.SessionId = id;
        if (type === 'Main') {
            newRecordInfo = $popup.find('#addToTab').data('newRecordInfo');
            request.OrderId = newRecordInfo.UniqueId;
        } else {
            request.OrderId = id;
        }
        if (type === "Complete" || type === "Kit" || type === "Container") {
            apiUrl = "addtopackage";
            request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
        }

        if (actionType === 'Insert') {
            const insertAtRowNumber = $popup.attr('data-insertatrownumber');
            if (insertAtRowNumber != undefined) {
                request.InsertAtIndex = insertAtRowNumber;
            }
        }

        $popup.find('.addToOrder').css('cursor', 'wait');
        $popup.off('click', '.addToOrder');
        FwAppData.apiMethod(true, 'POST', `api/v1/inventorysearch/${apiUrl}`, request, FwServices.defaultTimeout,
            response => {
                FwPopup.destroyPopup($popup);
                if (type === 'Main') {
                    const uniqueIds: any = {};
                    uniqueIds[newRecordInfo.UniqueIdField] = newRecordInfo.UniqueId;
                    let caption = newRecordInfo.Caption;
                    if (caption === 'PurchaseOrder') caption = 'Purchase Order';
                    const $newForm = (<any>window)[newRecordInfo.Controller].loadForm(uniqueIds);
                    FwModule.openModuleTab($newForm, caption, true, 'FORM', true);
                } else { //reloads grids on active tab
                    $form.find('.tabGridsLoaded[data-type="tab"]').removeClass('tabGridsLoaded');
                    const $activeGrid = $form.find('.active[data-type="tabpage"] [data-type="Grid"]');
                    for (let i = 0; i < $activeGrid.length; i++) {
                        const $gridcontrol = jQuery($activeGrid[i]);
                        FwBrowse.databind($gridcontrol);
                    }
                }

                if (type === 'Order' || type === 'Quote' || type === 'PurchaseOrder') {
                    FwAppData.apiMethod(true, 'GET', `api/v1/${type}/${id}`, request, FwServices.defaultTimeout,
                        response => {
                            FwFormField.setValueByDataField($form, 'Status', response.Status);
                            if (response.HasLaborItem) {
                                FwFormField.setValueByDataField($form, 'Labor', true, null, true);
                                FwTabs.setTabColor($form.find('.labortab'), '#FFFF8d');
                            }
                            if (response.HasMiscellaneousItem) {
                                FwFormField.setValueByDataField($form, 'Miscellaneous', true, null, true);
                                FwTabs.setTabColor($form.find('.misctab'), '#FFFF8d');
                            }
                            if (response.HasRentalItem) {
                                FwFormField.setValueByDataField($form, 'Rental', true, null, true);
                                FwTabs.setTabColor($form.find('.rentaltab'), '#FFFF8d');
                            }
                            if (response.HasSalesItem) {
                                FwFormField.setValueByDataField($form, 'Sales', true, null, true);
                                FwTabs.setTabColor($form.find('.salestab'), '#FFFF8d');
                            }
                        }, ex => FwFunc.showError(ex), $form);
                }
            }, ex => FwFunc.showError(ex), $searchpopup, (request.InventoryId ? null : id));
    }
    //----------------------------------------------------------------------------------------------
    updatePreviewTabQuantity($popup, id, initialLoad) {
        //Display # of items from previous session in preview tab
        FwAppData.apiMethod(true, 'GET', `api/v1/inventorysearch/gettotal/${id}`, null, FwServices.defaultTimeout,
            response => {
                if (typeof response.TotalQuantityInSession === 'number') {
                    $popup.find('.tab[data-caption="Preview"] .caption').text(`Preview (${response.TotalQuantityInSession} items)`);
                    if (initialLoad === true && response.TotalQuantityInSession > 0) {
                        FwNotification.renderNotification('WARNING', 'There are items from a previous Search session that have not been added.  Click the Preview tab to view.');
                    }
                }
            }, ex => FwFunc.showError(ex), null);
    }
    //----------------------------------------------------------------------------------------------
    getInventory($popup) {
        let $searchpopup = $popup.find('#searchpopup');
        let parentFormId = $popup.find('#itemsearch').data('parentformid');
        const inventoryType = FwFormField.getValueByDataField($popup, 'InventoryType');
        let classification;

        if (inventoryType == 'M' || inventoryType == 'L') {
            classification = '';
        } else {
            classification = FwFormField.getValueByDataField($popup, 'Select');
        };

        const fromDateAndTime: FromDateAndTime = this.getFromTimeValue($popup);

        let request: any = {
            OrderId:                       parentFormId,
            SessionId:                     parentFormId,
            ShowAvailability:              $popup.find('[data-datafield="Columns"] li[data-value="Available"]').attr('data-selected') === 'T' ? true : false,
            ShowImages:                    true,
            //SortBy:                        FwFormField.getValueByDataField($popup, 'SortBy'), //inv seq util will sort now
            Classification:                classification,
            AvailableFor:                  inventoryType,
            HideInventoryWithZeroQuantity: FwFormField.getValueByDataField($popup, 'HideZeroQuantity') == "T" ? true : false,
            WarehouseId:                   $popup.find('#itemsearch').data('warehouseid'),
            FromDate:                      fromDateAndTime.fromDate || undefined,
            FromTime:                      fromDateAndTime.fromTime || undefined,
            ToDate:                        FwFormField.getValueByDataField($popup, 'ToDate') || undefined,
            ToTime:                        FwFormField.getValueByDataField($popup, 'ToTime') || undefined,
            InventoryTypeId:               $popup.find('#itemsearch').attr('data-inventorytypeid') || undefined,
            CategoryId:                    $popup.find('#itemsearch').attr('data-categoryid') || undefined,
            SubCategoryId:                 $popup.find('#itemsearch').attr('data-subcategoryid') || undefined,
            SearchText:                    FwFormField.getValueByDataField($popup, 'SearchBox') || undefined,
            CurrencyId:                    FwFormField.getValueByDataField($popup, 'CurrencyId') || undefined
        }

        let type = $popup.find('#itemsearch').attr('data-moduletype');
        if (type === 'PurchaseOrder') {
            request.ShowAvailability = false;
        }

        FwAppData.apiMethod(true, 'POST', 'api/v1/inventorysearch/search', request, FwServices.defaultTimeout, response => {
            this.renderInventory($popup, response);
        }, null, $searchpopup);
    }
    //----------------------------------------------------------------------------------------------
    refreshPreviewGrid($popup, id) {
        //let $searchpopup = $popup.find('#searchpopup');
        //let previewrequest: any = {
        //    SessionId:        id,
        //    ShowAvailability: $popup.find('[data-datafield="Columns"] li[data-value="Available"]').attr('data-selected') === 'T' ? true : false,
        //    ShowImages:       true,
        //    FromDate:         FwFormField.getValueByDataField($popup, 'FromDate'),
        //    ToDate:           FwFormField.getValueByDataField($popup, 'ToDate')
        //};

        //let type = $popup.find('#itemsearch').attr('data-moduletype');
        //if (type === 'PurchaseOrder') {
        //    previewrequest.ShowAvailability = false;
        //}

        let $searchpopup = $popup.find('#searchpopup');
        let type = $popup.find('#itemsearch').attr('data-moduletype');
        let showAvailability: boolean = $popup.find('[data-datafield="Columns"] li[data-value="Available"]').attr('data-selected') === 'T' ? true : false;

        if (type === 'PurchaseOrder') {
            showAvailability = false;
        }

        let previewrequest: BrowseRequest = new BrowseRequest();
        previewrequest.uniqueids = {
            SessionId: id,
            ShowAvailability: showAvailability,
            ShowImages: true,
            FromDate: FwFormField.getValueByDataField($popup, 'FromDate'),
            ToDate: FwFormField.getValueByDataField($popup, 'ToDate'),
        }

        FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearchpreview/browse", previewrequest, FwServices.defaultTimeout, response => {
            let $grid = $popup.find('[data-name="SearchPreviewGrid"]');
            FwBrowse.search($grid);
        }, null, $searchpopup);
    }
    //----------------------------------------------------------------------------------------------
    refreshAccessoryQuantity($popup, id, warehouseId, inventoryId, $showAccessories: JQuery) {
        let accessoryContainer;
        const $el = jQuery($showAccessories);

        const fromDateAndTime: FromDateAndTime = this.getFromTimeValue($popup);

        let request: any = {
            SessionId:          id,
            OrderId:            id,
            ParentId:           inventoryId,
            WarehouseId:        warehouseId,
            ShowAvailability:   $popup.find('[data-datafield="Columns"] li[data-value="Available"]').attr('data-selected') === 'T' ? true : false,
            ShowImages:         true,
            FromDate:           fromDateAndTime.fromDate || undefined,
            FromTime:           fromDateAndTime.fromTime || undefined,
            ToDate:             FwFormField.getValueByDataField($popup, 'ToDate') || undefined,
            ToTime:             FwFormField.getValueByDataField($popup, 'ToTime') || undefined
        }

        const isNestedAccessory = $el.attr('data-isnestedaccessory');

        if (isNestedAccessory) {
            request.GrandParentId = $el.parents('.item-accessories').siblings('.item-info').attr('data-inventoryid');
            accessoryContainer = $el.parents('.item-accessory-info').siblings(`.nested-accessories[data-parentid="${inventoryId}"]`);
        } else {
            accessoryContainer = $el.parents('.item-container').find('.item-accessories');
        }

        let type = $popup.find('#itemsearch').attr('data-moduletype');
        if (type === 'PurchaseOrder') {
            request.ShowAvailability = false;
        }

        if ($popup.data('refreshaccessories') == true) {
            $popup.data('refreshaccessories', false)
        }

        //get id for nested toggle accessories
        const showAccessoriesInventoryId = accessoryContainer.find('.toggleaccessories:contains(Hide)').parents('.item-accessory-info').attr('data-inventoryid');

        if (!(accessoryContainer.find('.accColumns').length)) {
            let accessorycolumnshtml =  `<div class="accColumns" style="width:100%; display:none">
                                           <div class="columnorder" data-column="Description">Description</div>
                                           <div class="columnorder" data-column="Tags"></div>
                                           <div class="columnorder" data-column="Quantity">Qty</div>
                                           <div class="columnorder showOnSearch" data-column="Type"></div> 
                                           <div class="columnorder showOnSearch" data-column="Category"></div>
                                           <div class="columnorder showOnSearch" data-column="SubCategory"></div>
                                           <div class="columnorder" data-column="ICode">I-Code</div>
                                           <div class="columnorder" data-column="PartNumber">Part Number</div>
                                           <div class="columnorder hideColumns" data-column="Available">Available</div>
                                           <div class="columnorder hideColumns" data-column="ConflictDate">Conflict <div>Date</div></div>
                                           <div class="columnorder hideColumns" data-column="AllWh"></div>
                                           <div class="columnorder" data-column="In">In</div>
                                           <div class="columnorder" data-column="QC"></div>
                                           <div class="columnorder note-button" style="display:none;" data-column="Note"></div>
                                           <div class="columnorder" data-column="Rate"></div>
                                         </div>`;
            accessoryContainer.append(jQuery(accessorycolumnshtml));
        }
        // Show column names for grid view
        if (($popup.find('#itemlist').attr('data-view')) == 'GRID') {
            $popup.find('.accColumns').css('display', 'flex');
            $popup.find('.accColumns .showOnSearch').show();
        } else {
            $popup.find('.accColumns').css('display', 'none');
        }

        FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/accessories", request, FwServices.defaultTimeout,
            response => {
            const descriptionIndex                 = response.ColumnIndex.Description;
            const qtyIndex                         = response.ColumnIndex.Quantity;
            const qtyInIndex                       = response.ColumnIndex.QuantityIn;
            const quantityQcRequired               = response.ColumnIndex.QuantityQcRequired;
            const qtyAvailIndex                    = response.ColumnIndex.QuantityAvailable;
            const qtyAvailableAllWhIndex           = response.ColumnIndex.QuantityAvailableAllWarehouses;
            const conflictIndex                    = response.ColumnIndex.ConflictDate;
            const inventoryIdIndex                 = response.ColumnIndex.InventoryId;
            const classificationIndex              = response.ColumnIndex.Classification;
            const classificationColorIndex         = response.ColumnIndex.ClassificationColor;
            const classificationDescriptionIndex   = response.ColumnIndex.ClassificationDescription;
            const textColorIndex                   = response.ColumnIndex.TextColor;
            const quantityColorIndex               = response.ColumnIndex.QuantityColor;
            const thumbnail                        = response.ColumnIndex.Thumbnail;
            const appImageId                       = response.ColumnIndex.ImageId;
            const availabilityStateIndex           = response.ColumnIndex.AvailabilityState;
            const availabilityStateIndexAllWhIndex = response.ColumnIndex.AvailabilityStateAllWarehouses;
            const isOptionIndex                    = response.ColumnIndex.IsOption;
            const defaultQuantityIndex             = response.ColumnIndex.DefaultQuantity;
            const icodeIndex                       = response.ColumnIndex.ICode;
            const partNumberIndex                  = response.ColumnIndex.ManufacturerPartNumber;
            const note                             = response.ColumnIndex.Note;
            const isPrimaryIndex                   = response.ColumnIndex.IsPrimary;

                if (isNestedAccessory) {
                    accessoryContainer.find(`.item-accessory-info`).remove();
                } else {
                    $el.parents('.item-container').find('.item-accessories .item-accessory-info, .item-accessories .nested-accessories').remove();
                }

                for (var i = 0; i < response.Rows.length; i++) {
                let imageThumbnail = response.Rows[i][thumbnail]  ? response.Rows[i][thumbnail]  : './theme/images/no-image.jpg';
                let imageId        = response.Rows[i][appImageId] ? response.Rows[i][appImageId] : '';
                let conflictdate   = response.Rows[i][conflictIndex] ? moment(response.Rows[i][conflictIndex]).format('L') : '';

                let accessoryhtml = `<div class="item-accessory-info"  data-classification="${response.Rows[i][classificationIndex]}" data-inventoryid="${response.Rows[i][inventoryIdIndex]}" data-isprimary="${response.Rows[i][isPrimaryIndex]}">
                                       <div data-column="Description" class="columnorder">
                                            <div data-column="ItemImage"><img src="${imageThumbnail}" data-value="${imageId}" alt="Image" class="image"></div>
                                            <div class="descriptionrow">
                                                <div class="description">${response.Rows[i][descriptionIndex]}</div>
                                                <i class="material-icons opentab is-accessory">more_horiz</i>
                                            </div>
                                       </div>
                                       <div data-column="Tags" class="columnorder"></div>
                                       <div data-column="Quantity" class="columnorder">
                                         <div style="float:left; border:1px solid #bdbdbd;">
                                           <button class="decrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">-</button>
                                           <input class="incrementvalue" type="number" min="0" style="padding: 5px 0px; float:left; width:50%; border:none; text-align:center;" value="${response.Rows[i][qtyIndex]}">
                                           <button class="incrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">+</button>
                                         </div>
                                       </div>
                                       <div class="columnorder" data-column="ICode">${response.Rows[i][icodeIndex]}</div>
                                       <div class="columnorder" data-column="PartNumber">${response.Rows[i][partNumberIndex]}</div>
                                       <div class="columnorder hideColumns" data-column="Available"><div class="available-color value">${response.Rows[i][qtyAvailIndex]}</div></div>
                                       <div class="columnorder hideColumns" data-column="ConflictDate">${conflictdate}</div>
                                       <div class="columnorder hideColumns" data-column="AllWh"><div class="available-color value">${response.Rows[i][qtyAvailableAllWhIndex]}</div></div>
                                       <div class="columnorder" data-column="In">${response.Rows[i][qtyInIndex]}</div>
                                       <div class="columnorder" data-column="Type"></div>
                                       <div class="columnorder" data-column="Category"></div>
                                       <div class="columnorder" data-column="SubCategory"></div>
                                       <div class="columnorder note-button" data-column="Note"><textarea class="value">${response.Rows[i][note]}</textarea>${response.Rows[i][note].length > 0 ? '<i class="material-icons">insert_drive_file</i>' : ''}</div>
                                       <div class="columnorder" data-column="Rate"></div>
                                       <div class="columnorder" data-column="QC">${response.Rows[i][quantityQcRequired]}</div>
                                     </div>`;
                    let $itemaccessoryinfo = jQuery(accessoryhtml);
                    accessoryContainer.append($itemaccessoryinfo);

                    if (response.Rows[i][qtyIndex] != 0) {
                        $itemaccessoryinfo.find('[data-column="Quantity"] input').css('background-color', '#c5eefb');
                    }

                    $itemaccessoryinfo.find('div[data-column="Available"]').attr('data-state', response.Rows[i][availabilityStateIndex]);
                    $itemaccessoryinfo.find('div[data-column="AllWh"]').attr('data-state', response.Rows[i][availabilityStateIndexAllWhIndex]);

                    if (response.Rows[i][classificationIndex] == "K" || response.Rows[i][classificationIndex] == "C" || response.Rows[i][classificationIndex] == "N") {
                        var $tag = jQuery('<div>').addClass('tag')
                            .html(response.Rows[i][classificationDescriptionIndex])
                            .css({ 'background-color': response.Rows[i][classificationColorIndex], 'color': response.Rows[i][textColorIndex] });
                        $itemaccessoryinfo.find('div[data-column="Tags"]').append($tag);

                        $itemaccessoryinfo.find('div[data-column="Description"]').append('<div class="toggleaccessories" data-isnestedaccessory="true">Show Accessories</div>');
                        if (!isNestedAccessory) {
                            $itemaccessoryinfo.after(`<div class="nested-accessories" data-classification="${response.Rows[i][classificationIndex]}" style="display:none;" data-parentid="${response.Rows[i][inventoryIdIndex]}"></div>`);
                        }
                    }

                    if (response.Rows[i][isOptionIndex] === true) {
                        var $tag = jQuery('<div>').addClass('tag')
                            .html('Optional')
                            .css({ 'background-color': response.Rows[i][quantityColorIndex], 'color': '#000000' });
                        $itemaccessoryinfo.find('div[data-column="Tags"]').append($tag);
                    }

                    if (response.Rows[i][defaultQuantityIndex] > 1) {
                        var $tag = jQuery('<div>').addClass('tag')
                            .html(`x${response.Rows[i][defaultQuantityIndex]}`)
                            .css({ 'background-color': '#FF9800', 'color': '#000000' });
                        $itemaccessoryinfo.find('div[data-column="Tags"]').append($tag);
                    } else if (response.Rows[i][defaultQuantityIndex] < 1) {
                        var percentage = response.Rows[i][defaultQuantityIndex] * 100;
                        var $tag = jQuery('<div>').addClass('tag')
                            .html(`${percentage}%`)
                            .css({ 'background-color': '#FF9800', 'color': '#000000' });
                        $itemaccessoryinfo.find('div[data-column="Tags"]').append($tag);
                    }

                    //custom display/sequencing for columns
                    let columnsToHide = $popup.find('#itemsearch').data('columnstohide');
                    if (typeof columnsToHide != 'undefined') {
                        $popup.find('.item-accessories .columnorder').css('display', '');
                        for (let i = 0; i < columnsToHide.length; i++) {
                            $popup.find(`.item-accessories [data-column="${columnsToHide[i]}"]`).hide();
                        }
                    }

                    let columnOrder = $popup.find('#itemsearch').data('columnorder');
                    if (typeof columnOrder != 'undefined') {
                        for (let i = 0; i < columnOrder.length; i++) {
                            $popup.find(`.item-accessories [data-column="${columnOrder[i]}"]`).css('order', i);
                        }
                    }

                    const inventoryType = FwFormField.getValueByDataField($popup, 'InventoryType');
                    let type = $popup.find('#itemsearch').attr('data-moduletype');
                    if (type === 'PurchaseOrder' || type === 'Template' || inventoryType == 'L' || inventoryType == 'M') {
                        $popup.find('.hideColumns').css('display', 'none');
                    }
                }

                let accessoryRefresh = $popup.find('.toggleAccessories input').prop('checked');
                if (showAccessoriesInventoryId != undefined) { //open accessories that were visible before they were refreshed
                    accessoryContainer.find(`[data-inventoryid="${showAccessoriesInventoryId}"]`).find('.toggleaccessories').click();
                } else if (!accessoryRefresh) { //open accessories if disable auto-expansion is not checked
                    accessoryContainer.find('.toggleaccessories').click();
                }
            }, null, $popup.find('#searchpopup'));
    }
    //----------------------------------------------------------------------------------------------
    getFromTimeValue($popup: any) {
        let fromDateAndTime: FromDateAndTime = new FromDateAndTime();
        const pickDate = FwFormField.getValueByDataField($popup, 'PickDate');
        const pickTime = FwFormField.getValueByDataField($popup, 'PickTime');
        const fromDate = FwFormField.getValueByDataField($popup, 'FromDate');
        const fromTime = FwFormField.getValueByDataField($popup, 'FromTime');

        const parsedPickDateTime = Date.parse(`${pickDate} ${pickTime}`);
        const parsedFromDateTime = Date.parse(`${fromDate} ${fromTime}`);

        if (!Number.isNaN(parsedPickDateTime)) {
            if (!Number.isNaN(parsedFromDateTime)) {
                if (parsedPickDateTime < parsedFromDateTime) {
                    fromDateAndTime.fromDate = pickDate;
                    fromDateAndTime.fromTime = pickTime;
                } else {
                    fromDateAndTime.fromDate = fromDate;
                    fromDateAndTime.fromTime = fromTime;
                }
            } else {
                fromDateAndTime.fromDate = pickDate;
                fromDateAndTime.fromTime = pickTime;
            }
        } else {
            fromDateAndTime.fromDate = fromDate;
            fromDateAndTime.fromTime = fromTime;
        }

        return fromDateAndTime;
    }
    //----------------------------------------------------------------------------------------------
}

class FromDateAndTime {
    fromDate: string;
    fromTime: string;
}

var SearchInterfaceController = new SearchInterface();