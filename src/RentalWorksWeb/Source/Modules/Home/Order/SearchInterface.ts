class SearchInterface {
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
                                <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="" data-datafield="InventoryType"></div>
                                <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield fwformcontrol" data-caption="Est. Start" data-datafield="FromDate" style="flex: 0 1 135px;"></div>
                                <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield fwformcontrol" data-caption="Est. Stop" data-datafield="ToDate" style="flex: 0 1 135px;"></div>
                                <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield fwformcontrol" data-caption="Select" data-datafield="Select" style="flex: 0 1 150px;"></div>
                                <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield fwformcontrol" data-caption="Sort By" data-datafield="SortBy" style="flex: 0 1 255px;"></div>
                                <div data-type="button" class="fwformcontrol addToOrder">Add to ${buttonCaption}</div>
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
                                <div class="formoptions" style="flex:1 0 auto;display:flex;flex-direction:column;">
                                  <div class="breadcrumbrow">
                                    <div id="breadcrumbs"></div>
                                    <i class="material-icons invviewbtn" data-buttonview="LIST">&#xE8EE;</i>
                                    <i class="material-icons invviewbtn" data-buttonview="HYBRID">&#xE8EF;</i>
                                    <i class="material-icons invviewbtn" data-buttonview="GRID">&#xE8F0;</i>
                                    <div class="options">
                                      <i class="material-icons optionsbutton">settings</i>
                                      <div class="optionsmenu">
                                        <div class="flexcolumn">
                                          <div data-datafield="Columns" data-control="FwFormField" data-type="checkboxlist" class="fwcontrol fwformfield columnOrder" data-caption="Select columns to display in Results" data-sortable="true" data-orderby="true" style="max-height:400px; margin-top: 10px;"></div>
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
                                      <div class="columnorder" data-column="Tags">Tags</div>
                                      <div class="columnorder" data-column="Quantity">Qty</div>
                                      <div class="columnorder showOnSearch" data-column="Type">Type</div> 
                                      <div class="columnorder showOnSearch" data-column="Category">Category</div>
                                      <div class="columnorder showOnSearch" data-column="SubCategory">Sub Category</div>
                                      <div class="columnorder hideColumns" data-column="Available">Available</div>
                                      <div class="columnorder hideColumns" data-column="ConflictDate">Conflict Date</div>
                                      <div class="columnorder hideColumns" data-column="AllWh">All Warehouse</div>
                                      <div class="columnorder hideColumns" data-column="In">In</div>
                                      <div class="columnorder hideColumns" data-column="QC">QC</div>
                                      <div class="columnorder" data-column="Rate">Rate</div>
                                    </div>
                                    <div id="inventory"></div>
                                  </div>
                                </div>
                              </div>
                            </div>
                          </div>`;
        $popup.find('#itemsearchtabpage').append(jQuery(searchhtml));

        let previewhtml = `<div id="previewHtml">
                             <div class="fwmenu default"></div>
                             <div style="padding: 5px;text-align: right;">
                               <div data-type="button" class="fwformcontrol addToOrder">Add to ${buttonCaption}</div>
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

        FwFormField.loadItems($popup.find('div[data-datafield="Select"]'), [
            { value: '',   text: 'All' },
            { value: 'CK', text: 'Complete/Kit' },
            { value: 'N',  text: 'Container' },
            { value: 'I',  text: 'Item' },
            { value: 'A',  text: 'Accessory' }
        ], true);

        FwFormField.loadItems($popup.find('div[data-datafield="SortBy"]'), [
            { value: 'INVENTORY',   text: 'Type / Category / Sub-Category' },
            { value: 'ICODE',       text: 'I-Code' },
            { value: 'DESCRIPTION', text: 'Description' },
            { value: 'PARTNO',      text: 'Part No.' }
        ], true);

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

        let startDate;
        let stopDate;
        switch (type) {
            case 'Main':
                const department = JSON.parse(sessionStorage.getItem('department')); 
                const location = JSON.parse(sessionStorage.getItem('location')); 
                const today = FwFunc.getDate();
                const addToTypes = [{ value: 'Quote', caption: 'Quote', checked: 'T' },
                { value: 'Order', caption: 'Order' },
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
                break;
            case 'Order':
            case 'Quote':
                startDate = FwFormField.getValueByDataField($form, 'EstimatedStartDate');
                stopDate = FwFormField.getValueByDataField($form, 'EstimatedStopDate');
                FwFormField.setValueByDataField($popup, 'FromDate', startDate);
                FwFormField.setValueByDataField($popup, 'ToDate', stopDate);
                break;
            case 'PurchaseOrder':
                startDate = FwFormField.getValueByDataField($form, 'PurchaseOrderDate');
                FwFormField.setValueByDataField($popup, 'FromDate', startDate);
                break;
            case 'Transfer':
                let pickDate = FwFormField.getValueByDataField($form, 'PickDate');
                let shipDate = FwFormField.getValueByDataField($form, 'ShipDate');
                if (new Date(pickDate).getTime() == new Date(shipDate).getTime()) {
                    startDate = pickDate;
                } else if (new Date(pickDate).getTime() > new Date(shipDate).getTime()) {
                    startDate = shipDate;
                } else if (new Date(pickDate).getTime() < new Date(shipDate).getTime()) {
                    startDate = pickDate;
                }
                FwFormField.setValueByDataField($popup, 'FromDate', startDate);
                break;
        }

        $popup.find('#itemsearch').data('parentformid', id);
        let warehouseId = (type === 'Transfer' || type === 'Complete' || type === 'Kit' || type === 'Container' || type === 'Main') ? JSON.parse(sessionStorage.getItem('warehouse')).warehouseid : FwFormField.getValueByDataField($form, 'WarehouseId');
        $popup.find('#itemsearch').data('warehouseid', warehouseId);

        this.getViewSettings($popup);

        //Render preview grid
        const $previewGrid        = $popup.find('[data-grid="SearchPreviewGrid"]');
        const $previewGridControl = FwBrowse.loadGridFromTemplate('SearchPreviewGrid');
        $previewGrid.empty().append($previewGridControl);
        $previewGridControl.data('ondatabind', request => {
            request.SessionId        = id;
            request.ShowAvailability = true;
            request.FromDate         = FwFormField.getValueByDataField($popup, 'FromDate');
            request.ToDate           = FwFormField.getValueByDataField($popup, 'ToDate');
            request.ShowImages       = true;
        });
        FwBrowse.init($previewGridControl);
        FwBrowse.renderRuntimeHtml($previewGridControl);
        FwBrowse.addEventHandler($previewGridControl, 'afterdatabindcallback', () => {
            this.updatePreviewTabQuantity($popup, id, false);
        });

        this.updatePreviewTabQuantity($popup, id, true);
        this.events($popup, $form, id);

        //Sets inventory type by active tab
        if (typeof gridInventoryType == 'undefined') {
            gridInventoryType = $form.find('.tabs .active[data-type="tab"]').attr('data-caption');
            if (gridInventoryType == 'Miscellaneous') gridInventoryType = 'Misc';
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
        };

        //Hide columns based on type
        if (type === 'PurchaseOrder' || type === 'Template') {
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
                                    <div data-control="FwFormField" data-type="validation" class="deal-fields fwcontrol fwformfield" data-caption="Rate" data-datafield="RateType" data-displayfield="RateType" data-validationname="RateTypeValidation" data-required="true" style="flex:1 1 175px;"></div>
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
                const request: any = {};
                request.WarehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
                request.OfficeLocationId = JSON.parse(sessionStorage.getItem('location')).locationid;
                request.Description = FwFormField.getValue2($addToTab.find('[data-datafield="Description"]'));
                request.DepartmentId = FwFormField.getValue2($addToTab.find('[data-datafield="DepartmentId"]'));
                switch (addToType) {
                    case 'Quote':
                        controller = 'QuoteController';
                        request.DealId = FwFormField.getValue2($addToTab.find('[data-datafield="DealId"]'));
                        request.RateType = FwFormField.getValue2($addToTab.find('[data-datafield="RateType"]'));
                        request.OrderTypeId = FwFormField.getValue2($addToTab.find('[data-datafield="OrderTypeId"]'));
                        request.PickDate = FwFormField.getValue2($addToTab.find('[data-datafield="PickDate"]'));
                        request.PickTime = FwFormField.getValue2($addToTab.find('[data-datafield="PickTime"]'));
                        request.EstimatedStartDate = FwFormField.getValue2($addToTab.find('[data-datafield="FromDate"]'));
                        request.EstimatedStartTime = FwFormField.getValue2($addToTab.find('[data-datafield="FromTime"]'));
                        request.EstimatedStopDate = FwFormField.getValue2($addToTab.find('[data-datafield="ToDate"]'));
                        request.EstimatedStopTime = FwFormField.getValue2($addToTab.find('[data-datafield="ToTime"]'));
                        break;
                    case 'Order':
                        controller = 'OrderController';
                        request.DealId = FwFormField.getValue2($addToTab.find('[data-datafield="DealId"]'));
                        request.RateType = FwFormField.getValue2($addToTab.find('[data-datafield="RateType"]'));
                        request.OrderTypeId = FwFormField.getValue2($addToTab.find('[data-datafield="OrderTypeId"]'));
                        request.PickDate = FwFormField.getValue2($addToTab.find('[data-datafield="PickDate"]'));
                        request.PickTime = FwFormField.getValue2($addToTab.find('[data-datafield="PickTime"]'));
                        request.EstimatedStartDate = FwFormField.getValue2($addToTab.find('[data-datafield="FromDate"]'));
                        request.EstimatedStartTime = FwFormField.getValue2($addToTab.find('[data-datafield="FromTime"]'));
                        request.EstimatedStopDate = FwFormField.getValue2($addToTab.find('[data-datafield="ToDate"]'));
                        request.EstimatedStopTime = FwFormField.getValue2($addToTab.find('[data-datafield="ToTime"]'));
                        break;
                    case 'Purchase':
                        addToType = PurchaseOrderController.Module;
                        controller = 'PurchaseOrderController';
                        request.VendorId = FwFormField.getValue2($addToTab.find('[data-datafield="VendorId"]'));
                        request.RateType = FwFormField.getValue2($addToTab.find('[data-datafield="RateType"]'));
                        request.PoTypeId = FwFormField.getValue2($addToTab.find('[data-datafield="PoTypeId"]'));
                        request.PickDate = FwFormField.getValue2($addToTab.find('[data-datafield="PickDate"]'));
                        request.PickTime = FwFormField.getValue2($addToTab.find('[data-datafield="PickTime"]'));
                        request.EstimatedStartDate = FwFormField.getValue2($addToTab.find('[data-datafield="FromDate"]'));
                        request.EstimatedStartTime = FwFormField.getValue2($addToTab.find('[data-datafield="FromTime"]'));
                        request.EstimatedStopDate = FwFormField.getValue2($addToTab.find('[data-datafield="ToDate"]'));
                        request.EstimatedStopTime = FwFormField.getValue2($addToTab.find('[data-datafield="ToTime"]'));
                        break;
                    case 'Transfer':
                        controller = 'TransferOrderController';
                        request.ToWarehouseId = FwFormField.getValue2($addToTab.find('[data-datafield="ToWarehouseId"]'));
                        request.FromWarehouseId = FwFormField.getValue2($addToTab.find('[data-datafield="FromWarehouseId"]'));
                        request.PickDate = FwFormField.getValue2($addToTab.find('[data-datafield="PickDate"]'));
                        request.PickTime = FwFormField.getValue2($addToTab.find('[data-datafield="PickTime"]'));
                        request.ShipDate = FwFormField.getValue2($addToTab.find('[data-datafield="ShipDate"]'));
                        request.ShipTime = FwFormField.getValue2($addToTab.find('[data-datafield="ShipTime"]'));
                        request.RequiredDate = FwFormField.getValue2($addToTab.find('[data-datafield="RequiredDate"]'));
                        request.RequiredTime = FwFormField.getValue2($addToTab.find('[data-datafield="RequiredTime"]'));
                        break;
                }
                apiurl = (<any>window)[controller].apiurl;
                FwAppData.apiMethod(true, 'POST', apiurl, request, FwServices.defaultTimeout,
                    response => {
                        const newRecordInfo: any = {
                            "Controller": controller
                            , "UniqueIdField": `${addToType}Id`
                            , "UniqueId": response[`${addToType}Id`]
                            , "Caption": addToType
                        }
                        $popup.find('#addToTab').data('newRecordInfo', newRecordInfo);
                        $popup.find('.addToOrder').click();
                    },
                    ex => FwFunc.showError(ex), $searchpopup);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    populateTypeMenu($popup) {
        let self                      = this;
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

        inventoryTypeRequest.orderby = "OrderBy";

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
        let $inventoryContainer       = $popup.find('#inventory');
        let descriptionIndex          = response.ColumnIndex.Description,
            quantityAvailable         = response.ColumnIndex.QuantityAvailable,
            conflictDate              = response.ColumnIndex.ConflictDate,
            quantityIn                = response.ColumnIndex.QuantityIn,
            quantityQcRequired        = response.ColumnIndex.QuantityQcRequired,
            quantity                  = response.ColumnIndex.Quantity,
            dailyRate                 = response.ColumnIndex.DailyRate,
            inventoryId               = response.ColumnIndex.InventoryId,
            thumbnail                 = response.ColumnIndex.Thumbnail,
            appImageId                = response.ColumnIndex.ImageId,
            classificationIndex       = response.ColumnIndex.Classification,
            classificationColor       = response.ColumnIndex.ClassificationColor,
            classificationDescription = response.ColumnIndex.ClassificationDescription,
            typeIndex                 = response.ColumnIndex.InventoryType,
            categoryIndex             = response.ColumnIndex.Category,
            subCategoryIndex          = response.ColumnIndex.SubCategory,
            availabilityStateIndex    = response.ColumnIndex.AvailabilityState,
            qtyIsStaleIndex           = response.ColumnIndex.QuantityAvailableIsStale;

        $inventoryContainer.empty();
        $popup.find('.refresh-availability').hide();

        if (response.Rows.length == 0) {
            $inventoryContainer.append('<div style="font-weight: bold; font-size:1.3em;text-align: center;padding-top: 50px;">0 Items Found</div>');
        }

        for (let i = 0; i < response.Rows.length; i++) {
            let imageThumbnail = response.Rows[i][thumbnail]  ? response.Rows[i][thumbnail]  : './theme/images/no-image.jpg';
            let imageId        = response.Rows[i][appImageId] ? response.Rows[i][appImageId] : '';
            let rate           = Number(response.Rows[i][dailyRate]).toFixed(2);
            let conflictdate   = response.Rows[i][conflictDate] ? moment(response.Rows[i][conflictDate]).format('L') : "";

            let itemhtml = `<div class="item-container" data-classification=="${response.Rows[i][classificationIndex]}">
                              <div class="item-info" data-inventoryid="${response.Rows[i][inventoryId]}">
                                <div data-column="ItemImage"><img src="${imageThumbnail}" data-value="${imageId}" alt="Image" class="image"></div>
                                <div data-column="Description" class="columnorder"><div class="descriptionrow"><div class="description">${response.Rows[i][descriptionIndex]}</div></div></div>
                                <div data-column="Tags" class="columnorder"></div>
                                <div data-column="Type" class="columnorder showOnSearch">${response.Rows[i][typeIndex]}</div>
                                <div data-column="Category" class="columnorder showOnSearch">${response.Rows[i][categoryIndex]}</div>
                                <div data-column="SubCategory" class="columnorder showOnSearch">${response.Rows[i][subCategoryIndex]}</div>
                                <div data-column="Available" class="columnorder hideColumns"><div class="gridcaption">Available</div><div class="available-color value">${response.Rows[i][quantityAvailable]}</div></div>
                                <div data-column="ConflictDate" class="columnorder hideColumns"><div class="gridcaption">Conflict</div><div class="value">${conflictdate}</div></div>
                                <div data-column="AllWh" class="columnorder hideColumns">&#160;</div>
                                <div data-column="In" class="columnorder hideColumns"><div class="gridcaption">In</div><div class="value">${response.Rows[i][quantityIn]}</div></div>
                                <div data-column="QC" class="columnorder hideColumns"><div class="gridcaption">QC</div><div class="value">${response.Rows[i][quantityQcRequired]}</div></div>
                                <div data-column="Rate" class="columnorder rate"><div class="gridcaption">Rate</div><div class="value">${rate}</div> </div>
                                <div data-column="Quantity" class="columnorder">
                                  <div class="gridcaption">Qty</div>
                                  <div style="float:left; border:1px solid #bdbdbd;">
                                    <button class="decrementQuantity" tabindex="-1">-</button>
                                    <input type="number" style="padding: 5px 0px; float:left; width:50%; border:none; text-align:center;" value="${response.Rows[i][quantity]}">
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

            if (response.Rows[i][classificationIndex] == "K" || response.Rows[i][classificationIndex] == "C") {
                var $tag = jQuery('<div>').addClass('tag')
                                          .html(response.Rows[i][classificationDescription])
                                          .css({ 'background-color': response.Rows[i][classificationColor] });
                $itemcontainer.find('div[data-column="Tags"]').append($tag);
                $itemcontainer.find('div[data-column="Description"]').append('<div class="toggleaccessories">Show Accessories</div>');
                $itemcontainer.append(`<div class="item-accessories" data-classification="${response.Rows[i][classificationIndex]}" style="display:none;"></div>`);
            }

            if (response.Rows[i][qtyIsStaleIndex] === true) {
                $popup.find('.refresh-availability').show();
            }
        }

        let type = $popup.find('#itemsearch').attr('data-moduletype');
        if (type === 'PurchaseOrder' || type === 'Template') {
            $popup.find('.hideColumns').css('display', 'none');
        }

        this.listGridView($popup);
    }
    //----------------------------------------------------------------------------------------------
    listGridView($popup) {
        let view = $popup.find('#itemlist').attr('data-view');

        if (view !== 'GRID') {
            //custom display/sequencing for columns
            let columnsToHide = $popup.find('#itemsearch').data('columnstohide');
            $popup.find('.columnDescriptions .columnorder, .item-info .columnorder').css('display', '');
            for (let i = 0; i < columnsToHide.length; i++) {
                $popup.find(`.columnDescriptions [data-column="${columnsToHide[i]}"], .item-info [data-column="${columnsToHide[i]}"]`).hide();
            }

            let columnOrder = $popup.find('#itemsearch').data('columnorder');
            for (let i = 0; i < columnOrder.length; i++) {
                $popup.find(`.columnDescriptions [data-column="${columnOrder[i]}"], .item-info [data-column="${columnOrder[i]}"]`).css('order', i);
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    setDefaultViewSettings($popup) {
        FwFormField.loadItems($popup.find('div[data-datafield="Columns"]'), [
            { value: 'Description',  text: 'Description',                         selected: 'T' },
            { value: 'Tags',         text: 'Tags',                                selected: 'T' },
            { value: 'Quantity',     text: 'Quantity',                            selected: 'T' },
            { value: 'Type',         text: 'Type',                                selected: 'F' },
            { value: 'Category',     text: 'Category',                            selected: 'F' },
            { value: 'SubCategory',  text: 'Sub Category',                        selected: 'F' },
            { value: 'Available',    text: 'Available Quantity',                  selected: 'T' },
            { value: 'ConflictDate', text: 'Conflict Date',                       selected: 'T' },
            { value: 'AllWh',        text: 'Available Quantity (All Warehouses)', selected: 'T' },
            { value: 'In',           text: 'In Quantity',                         selected: 'T' },
            { value: 'QC',           text: 'QC Required Quantity',                selected: 'T' },
            { value: 'Rate',         text: 'Rate',                                selected: 'T' }
        ]);
    
        FwFormField.setValueByDataField($popup, 'DisableAccessoryAutoExpand', false);
        FwFormField.setValueByDataField($popup, 'HideZeroQuantity', false);

        $popup.find('#itemlist').attr('data-view', 'GRID');

        this.saveViewSettings($popup);
    }
    //----------------------------------------------------------------------------------------------
    getViewSettings($popup) {
        let self = this;
        let userId = JSON.parse(sessionStorage.getItem('userid'));
        FwAppData.apiMethod(true, 'GET', `api/v1/usersearchsettings/${userId.webusersid}`, null, FwServices.defaultTimeout, function onSuccess(response) {
            //Render options sortable column list
            if (response.ResultFields) {
                self.setViewSettings($popup, response);
            } else {
                self.setDefaultViewSettings($popup);
            }
        }, null, null);
    }
    //----------------------------------------------------------------------------------------------
    saveViewSettings($popup) {
        let self         = this;
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
            Mode:                        $popup.find('#itemlist').attr('data-view')
        };
    
        FwAppData.apiMethod(true, 'POST', "api/v1/usersearchsettings/", request, FwServices.defaultTimeout, function onSuccess(response) {
            self.setViewSettings($popup, response);
    
            if (request.DisableAccessoryAutoExpand) {
                $popup.find('.item-accessories').css('display', 'none');
            }
        }, null, $searchpopup);
    }
    //----------------------------------------------------------------------------------------------
    setViewSettings($popup, response) {
        let columns = JSON.parse(response.ResultFields);
        FwFormField.loadItems($popup.find('div[data-datafield="Columns"]'), columns);

        let columnsToHide = [];
        let columnOrder   = [];
        for (let i = 0; i < columns.length; i++) {
            let $this = columns[i];
            columnOrder.push($this.value);

            if ($this.selected == 'F') {
                columnsToHide.push($this.value);
            }
        }
        $popup.find('#itemsearch').data('columnorder', columnOrder);
        $popup.find('#itemsearch').data('columnstohide', columnsToHide);

        FwFormField.setValueByDataField($popup, 'DisableAccessoryAutoExpand', response.DisableAccessoryAutoExpand);
        FwFormField.setValueByDataField($popup, 'HideZeroQuantity', response.HideZeroQuantity);

        $popup.find('#itemlist').attr('data-view', response.Mode);
        this.listGridView($popup);
    }
    //----------------------------------------------------------------------------------------------
    events($popup, $form, id) {
        let self           = this;
        const $options     = $popup.find('.options');
        let userId         = JSON.parse(sessionStorage.getItem('userid'));
        let hasItemInGrids = false;
        let warehouseId    = $popup.find('#itemsearch').data('warehouseid');
        let $searchpopup   = $popup.find('#searchpopup');

        $popup
            .on('change', '#itemsearch div[data-datafield="FromDate"], #itemsearch div[data-datafield="ToDate"]', function () {
                if ($popup.find('#inventory').children().length > 0) {
                    self.getInventory($popup, false);
                }
            })
            .on('click', '#breadcrumbs .basetype', e => {
                FwFormField.setValueByDataField($popup, 'SearchBox', '');
                $popup.find('.refresh-availability').hide();
                self.populateTypeMenu($popup);
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
                self.populateTypeMenu($popup);
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
                    let categoryIdIndex = response.ColumnIndex.CategoryId;
                    let categoryIndex   = response.ColumnIndex.Category;

                    $popup.find('#category, #subCategory').empty();
                    $popup.find('#inventory').empty();

                    let categories: any = [];
                    let categoryColumn  = $popup.find('#category');
                    for (let i = 0; i < response.Rows.length; i++) {
                        if (categories.indexOf(response.Rows[i][categoryIndex]) == -1) {
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

                FwAppData.apiMethod(true, 'POST', "api/v1/subcategory/browse", subCatListRequest, FwServices.defaultTimeout, function onSuccess(response) {
                    let subCategoryIdIndex = response.ColumnIndex.SubCategoryId;
                    let subCategoryIndex   = response.ColumnIndex.SubCategory;
                    $popup.find('#subCategory').empty();

                    //Load the Inventory items if selected category doesn't have any sub-categories
                    if (response.Rows.length == 0) {
                       self.getInventory($popup, false);
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
            .on('click', '#subCategory ul', function (e) {
                const $this = jQuery(e.currentTarget);

                $popup.find('#subCategory ul').removeClass('selected');
                $this.addClass('selected');

                //Clear and set breadcrumbs
                let $breadcrumbs = $popup.find('#breadcrumbs');
                $breadcrumbs.find('.subcategory').remove();
                $breadcrumbs.append(`<div class="subcategory breadcrumb"><div class="value">${$this.attr('data-caption')}</div></div>`);

                $popup.find('#itemsearch').attr('data-subcategoryid', $this.attr('data-value'));
                self.getInventory($popup, false);
            })
        ;

        //Close the Search Interface popup
        $popup.find('.close-modal').one('click', function (e) {
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
                self.saveViewSettings($popup);
                $popup.find('.options').removeClass('active');
                $popup.find('.options .optionsmenu').css('z-index', '0');
    
                //perform search again with new settings
                if ($popup.find('#inventory').children().length > 0) {
                    self.getInventory($popup, false);
                }
            })
            .on('click', '.options .restoreDefaults', e => {
                self.setDefaultViewSettings($popup);

                $popup.find('.options').removeClass('active');
                $popup.find('.options .optionsmenu').css('z-index', '0');
                self.listGridView($popup);
            })
        ;

        $popup.on('change', 'div[data-datafield="InventoryType"]', function () {
            self.populateTypeMenu($popup);

            let searchValue = FwFormField.getValueByDataField($popup, 'SearchBox');
            let e = jQuery.Event("keydown", { keyCode: 13 });
            if (searchValue != '') {
                $popup.find('[data-datafield="SearchBox"] input.fwformfield-value').trigger(e);
            }
        });

        //Filter results based on Search input field
        $popup.find('[data-datafield="SearchBox"]').on('keydown', 'input.fwformfield-value', e => {
            let code = e.keyCode || e.which;
            try {
                if (code === 13) { //Enter Key
                    self.populateTypeMenu($popup);
                    self.getInventory($popup, false);
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        $popup.on('dblclick', '#inventory .item-container .item-info', e => {
            let $iteminfo = jQuery(e.currentTarget);
            $iteminfo.find('.incrementQuantity').click();
        });

        $popup.on('click', '.toggleaccessories', e => {
            let $iteminfo           = jQuery(e.currentTarget).closest('.item-info');
            let $accessoryContainer = $iteminfo.siblings('.item-accessories');

            if ($accessoryContainer.is(":visible")) {
                jQuery(e.currentTarget).text('Show Accessories');
            } else {
                jQuery(e.currentTarget).text('Hide Accessories');
                let inventoryId = $iteminfo.attr('data-inventoryid');
                self.refreshAccessoryQuantity($popup, id, warehouseId, inventoryId, e);
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

                let element      = jQuery(e.currentTarget);
                let quantity     = element.val();
                let inventoryId  = element.parents('.item-info').attr('data-inventoryid');
                let request: any = {
                    OrderId:     id,
                    SessionId:   id,
                    InventoryId: inventoryId,
                    WarehouseId: warehouseId,
                    Quantity:    quantity
                }

                if (quantity > 0) {
                    hasItemInGrids = true;
                }

                quantity != 0 ? element.addClass('lightBlue') : element.removeClass('lightBlue');

                let $accContainer    = element.parents('.item-container').find('.item-accessories');
                let accessoryRefresh = $popup.find('.toggleAccessories input').prop('checked');
                FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/", request, FwServices.defaultTimeout, function onSuccess(response) {
                    if (accessoryRefresh == false) {
                        if ($accContainer.length > 0) {
                            self.refreshAccessoryQuantity($popup, id, warehouseId, inventoryId, e);
                        }
                    }

                    //Updates Preview tab with total # of items
                    $popup.find('.tab[data-caption="Preview"] .caption').text(`Preview (${response.TotalQuantityInSession})`);
                }, null, $searchpopup);
            })
            .on('click', '.item-accessory-info [data-column="Quantity"] input', e => {
                jQuery(e.currentTarget).select();
            })
            .on('change', '.item-accessory-info [data-column="Quantity"] input', e => {
                const element = jQuery(e.currentTarget);

                let accRequest: any = {
                    SessionId:   id,
                    ParentId:    element.parents('.item-container').find('.item-info').attr('data-inventoryid'),
                    InventoryId: element.parents('.item-accessory-info').attr('data-inventoryid'),
                    WarehouseId: warehouseId,
                    Quantity:    element.val()
                };

                accRequest.Quantity != "0" ? element.addClass('lightBlue') : element.removeClass('lightBlue');

                FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch", accRequest, FwServices.defaultTimeout, function onSuccess(response) {
                    //Updates preview tab with total # of items
                    $popup.find('.tab[data-caption="Preview"] .caption').text(`Preview (${response.TotalQuantityInSession})`);
                }, null, null);
            })
        ;

        //Update Preview grid tab
        $popup.on('click', '.tab[data-caption="Preview"]', e => {
            self.refreshPreviewGrid($popup, id);
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
                    `<div style="white-space:pre;">\n<img src="${applicationConfig.appbaseurl}${applicationConfig.appvirtualdirectory}fwappimage.ashx?method=GetAppImage&appimageid=${imageId}&thumbnail=false" data-value="${imageId}" alt="No Image" class="image" style="max-width:100%;">`);
                let $cancel = FwConfirmation.addButton($confirmation, 'Close');
            }
        });

        //Add to Order click event
        $popup.on('click', '.addToOrder', e => {
            if (!hasItemInGrids) {
                let previewGridHasItems = $popup.find('[data-grid="SearchPreviewGrid"] tbody').children().length > 0;
                if (!previewGridHasItems) {
                    FwNotification.renderNotification('WARNING', 'No items have been added.');
                } else {
                    addToOrder();
                }
            } else {
                addToOrder();
            }

            function addToOrder() {
                let request: any = {};
                let newRecordInfo;
                const type = $popup.find('#itemsearch').attr('data-moduletype');
                if (type === 'Main') {
                    newRecordInfo = $popup.find('#addToTab').data('newRecordInfo');
                    request.OrderId = newRecordInfo.UniqueId;
                    request.SessionId = id;
                } else {
                    request.OrderId = id;
                    request.SessionId = id;
                }
                if (type === "Complete" || type === "Kit" || type === "Container") {
                    request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
                }
                $popup.find('.addToOrder').css('cursor', 'wait');
                $popup.off('click', '.addToOrder');
                FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/addto", request, FwServices.defaultTimeout,
                    response => {
                        FwPopup.destroyPopup(jQuery(document).find('.fwpopup'));
                        //let $combinedGrid = $form.find('.combinedgrid [data-name="OrderItemGrid"]'),
                        //    $orderItemGridRental = $form.find('.rentalgrid [data-name="OrderItemGrid"]'),
                        //    $orderItemGridSales = $form.find('.salesgrid [data-name="OrderItemGrid"]'),
                        //    $orderItemGridLabor = $form.find('.laborgrid [data-name="OrderItemGrid"]'),
                        //    $orderItemGridMisc = $form.find('.miscgrid [data-name="OrderItemGrid"]');
                        //let $transferItemGridRental = $form.find('.rentalItemGrid [data-name="TransferOrderItemGrid"]');
                        //let $transferItemGridSales = $form.find('.salesItemGrid [data-name="TransferOrderItemGrid"]');

                        //if (type === "Complete") {
                        //    let $completeGrid = $form.find('[data-name="InventoryCompleteGrid"]');
                        //    FwBrowse.search($completeGrid);
                        //}
                        //if (type === "Kit") {
                        //    let $kitGrid = $form.find('[data-name="InventoryKitGrid"]');
                        //    FwBrowse.search($kitGrid);
                        //}
                        //if (type === "Container") {
                        //    let $containerGrid = $form.find('[data-name="InventoryContainerItemGrid"]');
                        //    FwBrowse.search($containerGrid);
                        //}

                        //if ($form.find('.combinedtab').css('display') != 'none') {
                        //    FwBrowse.search($combinedGrid);
                        //}

                        //if ($form.find('.notcombinedtab').css('display') != 'none') {
                        //    FwBrowse.search($orderItemGridRental);
                        //    FwBrowse.search($orderItemGridMisc);
                        //    FwBrowse.search($orderItemGridLabor);
                        //    FwBrowse.search($orderItemGridSales);
                        //    FwBrowse.search($transferItemGridRental);
                        //    FwBrowse.search($transferItemGridSales);
                        //}
                        if (type === 'Main') {
                            const uniqueIds: any = {};
                            uniqueIds[newRecordInfo.UniqueIdField] = newRecordInfo.UniqueId;
                            let caption = newRecordInfo.Caption;
                            if (caption === 'PurchaseOrder') caption = 'Purchase Order';
                            const $newForm = (<any>window)[newRecordInfo.Controller].loadForm(uniqueIds);
                            FwModule.openModuleTab($newForm, caption, true, 'FORM', true);
                        }
                        else { //reloads grids on active tab
                        $form.find('.tabGridsLoaded[data-type="tab"]').removeClass('tabGridsLoaded');
                        const $activeGrid = $form.find('.active[data-type="tabpage"] [data-type="Grid"]');
                        for (let i = 0; i < $activeGrid.length; i++) {
                            const $gridcontrol = jQuery($activeGrid[i]);
                            FwBrowse.search($gridcontrol);
                            }
                        }

                    }, ex => FwFunc.showError(ex), $searchpopup, (request.InventoryId ? null : id));
            }
        });

        //Saves the user's inventory view setting
        $popup.on('click', '.invviewbtn', e => {
            let $this = jQuery(e.currentTarget),
                view  = $this.attr('data-buttonview');

            $popup.find('#itemlist').attr('data-view', view);
            self.saveViewSettings($popup);
        });

        //Sorting option events
        $popup.on('change', 'div[data-datafield="Select"], div[data-datafield="SortBy"]', e => {
            self.getInventory($popup, false);
        });

        //Refresh Availability button
        $popup.on('click', '.refresh-availability', e => {
            self.getInventory($popup, true);
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

        if (jQuery('html').hasClass('desktop')) {
            let interval = 0;
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
    }
    //----------------------------------------------------------------------------------------------
    updatePreviewTabQuantity($popup, id, initialLoad) {
        //Display # of items from previous session in preview tab
        FwAppData.apiMethod(true, 'GET', `api/v1/inventorysearch/gettotal/${id}`, null, FwServices.defaultTimeout,
            response => {
                if (typeof response.TotalQuantityInSession === 'number') {
                    $popup.find('.tab[data-caption="Preview"] .caption').text(`Preview (${response.TotalQuantityInSession})`);
                    if (initialLoad === true && response.TotalQuantityInSession > 0) {
                        FwNotification.renderNotification('WARNING', 'There are items from a previous Search session that have not been added.  Click the Preview tab to view.');
                    }
                }
            }, ex => FwFunc.showError(ex), null);
    }
    //----------------------------------------------------------------------------------------------
     getInventory($popup, refreshAvailability) {
        var self         = this;
        let $searchpopup = $popup.find('#searchpopup');
        let parentFormId = $popup.find('#itemsearch').data('parentformid');

        let request: any = {
            OrderId:                       parentFormId,
            SessionId:                     parentFormId,
            ShowAvailability:              $popup.find('[data-datafield="Columns"] li[data-value="Available"]').attr('data-selected') === 'T' ? true : false,
            ShowImages:                    true,
            SortBy:                        FwFormField.getValueByDataField($popup, 'SortBy'),
            Classification:                FwFormField.getValueByDataField($popup, 'Select'),
            AvailableFor:                  FwFormField.getValueByDataField($popup, 'InventoryType'),
            HideInventoryWithZeroQuantity: FwFormField.getValueByDataField($popup, 'HideZeroQuantity') == "T" ? true : false,
            WarehouseId:                   $popup.find('#itemsearch').data('warehouseid'),
            FromDate:                      FwFormField.getValueByDataField($popup, 'FromDate') || undefined,
            ToDate:                        FwFormField.getValueByDataField($popup, 'ToDate') || undefined,
            InventoryTypeId:               $popup.find('#itemsearch').attr('data-inventorytypeid') || undefined,
            CategoryId:                    $popup.find('#itemsearch').attr('data-categoryid') || undefined,
            SubCategoryId:                 $popup.find('#itemsearch').attr('data-subcategoryid') || undefined,
            SearchText:                    FwFormField.getValueByDataField($popup, 'SearchBox') || undefined,
            RefreshAvailability:           refreshAvailability
        }

        FwAppData.apiMethod(true, 'POST', 'api/v1/inventorysearch/search', request, FwServices.defaultTimeout, function onSuccess(response) {
            self.renderInventory($popup, response);
        }, null, $searchpopup);
    }
    //----------------------------------------------------------------------------------------------
    refreshPreviewGrid($popup, id) {
        let $searchpopup = $popup.find('#searchpopup');
        let previewrequest: any = {
            SessionId:        id,
            ShowAvailability: $popup.find('[data-datafield="Columns"] li[data-value="Available"]').attr('data-selected') === 'T' ? true : false,
            ShowImages:       true,
            FromDate:         FwFormField.getValueByDataField($popup, 'FromDate'),
            ToDate:           FwFormField.getValueByDataField($popup, 'ToDate')
        };
        FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearchpreview/browse", previewrequest, FwServices.defaultTimeout, function onSuccess(response) {
            let $grid = $popup.find('[data-name="SearchPreviewGrid"]');
            //FwBrowse.databindcallback($grid, response);
            FwBrowse.search($grid);
        }, null, $searchpopup);
    }
    //----------------------------------------------------------------------------------------------
    refreshAccessoryQuantity($popup, id, warehouseId, inventoryId, e) {
        let accessoryContainer = jQuery(e.currentTarget).parents('.item-container').find('.item-accessories');
        let request: any = {
            SessionId:        id,
            OrderId:          id,
            ParentId:         inventoryId,
            WarehouseId:      warehouseId,
            ShowAvailability: $popup.find('[data-datafield="Columns"] li[data-value="Available"]').attr('data-selected') === 'T' ? true : false,
            ShowImages:       true,
            FromDate:         FwFormField.getValueByDataField($popup, 'FromDate') || undefined,
            ToDate:           FwFormField.getValueByDataField($popup, 'ToDate') || undefined
        }

        if ($popup.data('refreshaccessories') == true) {
            request.RefreshAvailability = true;
            $popup.data('refreshaccessories', false)
        }

        if (!(accessoryContainer.find('.accColumns').length)) {
            let accessorycolumnshtml =  `<div class="accColumns" style="width:100%; display:none">
                                           <div class="columnorder" data-column="Description">Description</div>
                                           <div class="columnorder" data-column="Tags">Tags</div>
                                           <div class="columnorder" data-column="Quantity">Qty</div>
                                           <div class="columnorder showOnSearch" data-column="Type"></div> 
                                           <div class="columnorder showOnSearch" data-column="Category"></div>
                                           <div class="columnorder showOnSearch" data-column="SubCategory"></div>
                                           <div class="columnorder hideColumns" data-column="Available">Available</div>
                                           <div class="columnorder hideColumns" data-column="ConflictDate">Conflict <div>Date</div></div>
                                           <div class="columnorder hideColumns" data-column="AllWh"></div>
                                           <div class="columnorder hideColumns" data-column="In">In</div>
                                           <div class="columnorder hideColumns" data-column="QC"></div>
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

        FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/accessories", request, FwServices.defaultTimeout, function onSuccess(response) {
            jQuery(e.currentTarget).parents('.item-container').find('.item-accessories .item-accessory-info').remove();
            const descriptionIndex               = response.ColumnIndex.Description;
            const qtyIndex                       = response.ColumnIndex.Quantity;
            const qtyInIndex                     = response.ColumnIndex.QuantityIn;
            const qtyAvailIndex                  = response.ColumnIndex.QuantityAvailable;
            const conflictIndex                  = response.ColumnIndex.ConflictDate;
            const inventoryIdIndex               = response.ColumnIndex.InventoryId;
            const classificationIndex            = response.ColumnIndex.Classification;
            const classificationColorIndex       = response.ColumnIndex.ClassificationColor;
            const classificationDescriptionIndex = response.ColumnIndex.ClassificationDescription;
            const quantityColorIndex             = response.ColumnIndex.QuantityColor;
            const qtyIsStaleIndex                = response.ColumnIndex.QuantityAvailableIsStale;
            const thumbnail                      = response.ColumnIndex.Thumbnail;
            const appImageId                     = response.ColumnIndex.ImageId;
            const availabilityStateIndex         = response.ColumnIndex.AvailabilityState;
            const isOptionIndex                  = response.ColumnIndex.IsOption;
            const defaultQuantityIndex           = response.ColumnIndex.DefaultQuantity;

            for (var i = 0; i < response.Rows.length; i++) {
                let imageThumbnail = response.Rows[i][thumbnail]  ? response.Rows[i][thumbnail]  : './theme/images/no-image.jpg';
                let imageId        = response.Rows[i][appImageId] ? response.Rows[i][appImageId] : '';
                let conflictdate   = response.Rows[i][conflictIndex] ? moment(response.Rows[i][conflictIndex]).format('L') : '';

                let accessoryhtml = `<div class="item-accessory-info" data-inventoryid="${response.Rows[i][inventoryIdIndex]}">
                                       <div data-column="ItemImage"><img src="${imageThumbnail}" data-value="${imageId}" alt="Image" class="image"></div>
                                       <div data-column="Description" class="columnorder"><div class="descriptionrow">${response.Rows[i][descriptionIndex]}</div></div>
                                       <div data-column="Tags" class="columnorder"></div>
                                       <div data-column="Quantity" class="columnorder">
                                         <div style="float:left; border:1px solid #bdbdbd;">
                                           <button class="decrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">-</button>
                                           <input type="number" style="padding: 5px 0px; float:left; width:50%; border:none; text-align:center;" value="${response.Rows[i][qtyIndex]}">
                                           <button class="incrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">+</button>
                                         </div>
                                       </div>
                                       <div class="columnorder hideColumns" data-column="Available"><div class="available-color">${response.Rows[i][qtyAvailIndex]}</div></div>
                                       <div class="columnorder hideColumns" data-column="ConflictDate">${conflictdate}</div>
                                       <div class="hideColumns columnorder" data-column="In">${response.Rows[i][qtyInIndex]}</div>
                                       <div class="columnorder" data-column="Type"></div>
                                       <div class="columnorder" data-column="Category"></div>
                                       <div class="columnorder" data-column="SubCategory"></div>
                                       <div class="columnorder" data-column="Rate"></div>
                                       <div class="columnorder" data-column="QC"></div>
                                       <div class="columnorder" data-column="AllWh"></div>
                                     </div>`;
                let $itemaccessoryinfo = jQuery(accessoryhtml);
                accessoryContainer.append($itemaccessoryinfo);

                if (response.Rows[i][qtyIndex] != 0) {
                    $itemaccessoryinfo.find('[data-column="Quantity"] input').css('background-color', '#c5eefb');
                }

                $itemaccessoryinfo.find('div[data-column="Available"]').attr('data-state', response.Rows[i][availabilityStateIndex]);

                if (response.Rows[i][classificationIndex] == "K" || response.Rows[i][classificationIndex] == "C") {
                    var $tag = jQuery('<div>').addClass('tag')
                                              .html(response.Rows[i][classificationDescriptionIndex])
                                              .css({ 'background-color': response.Rows[i][classificationColorIndex] });
                    $itemaccessoryinfo.find('div[data-column="Tags"]').append($tag);
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

                let type = $popup.find('#itemsearch').attr('data-moduletype');
                if (type === 'PurchaseOrder' || type === 'Template') {
                    $popup.find('.hideColumns').css('display', 'none');
                }

                //custom display/sequencing for columns
                let columnsToHide = $popup.find('#itemsearch').data('columnstohide');
                $popup.find('.item-accessories .columnorder').css('display', '');
                for (let i = 0; i < columnsToHide.length; i++) {
                    $popup.find(`.item-accessories [data-column="${columnsToHide[i]}"]`).hide();
                }

                let columnOrder = $popup.find('#itemsearch').data('columnorder');
                for (let i = 0; i < columnOrder.length; i++) {
                    $popup.find(`.item-accessories [data-column="${columnOrder[i]}"]`).css('order', i);
                }
            }

            let obj = response.Rows.find(x => x[qtyIsStaleIndex] == true);
            if (typeof obj != 'undefined') {
                if (accessoryContainer.find('.acc-refresh-avail').length < 1) {
                    accessoryContainer.append(`<div style="text-align:center;"><div data-type="button" class="fwformcontrol acc-refresh-avail" style="line-height:24px; height:24px; background-color:rgb(49, 0, 209);">Refresh Availability</div></div>`);
                }
                accessoryContainer.find('.acc-refresh-avail').show();
            } else {
                accessoryContainer.find('.acc-refresh-avail').hide();
            }
        }, null, $popup.find('#searchpopup'));
    }
    //----------------------------------------------------------------------------------------------
}

var SearchInterfaceController = new SearchInterface();