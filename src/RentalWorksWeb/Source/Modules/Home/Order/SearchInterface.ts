class SearchInterface {
    //----------------------------------------------------------------------------------------------
    renderSearchPopup($form, id, type, gridInventoryType?) {
        let html: any = [];
        html.push('<div id="searchpopup" class="fwform fwcontrol">');
        html.push('  <div id="searchTabs" class="fwcontrol fwtabs" data-control="FwTabs" data-version="1">');
        html.push('    <div class="tabs">');
        html.push('      <div data-type="tab" id="itemsearchtab" class="tab" data-tabpageid="itemsearchtabpage" data-caption="Search"></div>');
        html.push('      <div data-type="tab" id="previewtab"    class="tab" data-tabpageid="previewtabpage"    data-caption="Preview"></div>');
        html.push('    </div>');
        html.push('    <div class="tabpages">');
        html.push('      <div data-type="tabpage" id="itemsearchtabpage" class="tabpage" data-tabid="itemsearchtab"></div>');
        html.push('      <div data-type="tabpage" id="previewtabpage"    class="tabpage" data-tabid="previewtab"></div>');
        html.push('    </div>');
        html.push('  </div>');
        html.push('  <div class="close-modal"><i class="material-icons">clear</i><div class="btn-text">Close</div></div>');
        html.push('</div>');
        let $popupHtml = jQuery(html.join(''));

        let $popup     = FwPopup.renderPopup($popupHtml, { ismodal: true });
        FwPopup.showPopup($popup);

        let searchhtml = `<div id="itemsearch" data-moduletype="${type}">
                            <div class="flexpage">
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield fwformcontrol" data-datafield="ParentFormId" style="display:none"></div>
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield fwformcontrol" data-datafield="WarehouseId" style="display:none"></div>
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield fwformcontrol" data-datafield="ColumnsToHide" style="display:none"></div>
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield fwformcontrol" data-datafield="ColumnOrder" style="display:none"></div>
                              <div class="fwmenu default"></div>
                              <div style="display:flex;flex:0 0 auto;align-items:center;">
                                <div data-control="FwFormField" class="fwcontrol fwformfield fwformcontrol" data-caption="" data-datafield="InventoryType" data-type="radio">
                                  <div data-value="R" data-caption="Rental"></div>
                                  <div data-value="S" data-caption="Sales"></div>
                                  <div data-value="L" data-caption="Labor"></div>
                                  <div data-value="M" data-caption="Misc"></div>
                                  <div data-value="P" data-caption="Parts"></div>
                                </div>
                                <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield fwformcontrol" data-caption="Est. Start" data-datafield="FromDate" style="flex: 0 1 135px;"></div>
                                <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield fwformcontrol" data-caption="Est. Stop" data-datafield="ToDate" style="flex: 0 1 135px;"></div>
                                <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield fwformcontrol" data-caption="Select" data-datafield="Select" style="flex: 0 1 150px;"></div>
                                <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield fwformcontrol" data-caption="Sort By" data-datafield="SortBy" style="flex: 0 1 255px;"></div>
                                <div data-type="button" class="fwformcontrol addToOrder" style="max-width:140px;">Add to ${type}</div>
                                <div data-type="button" class="fwformcontrol refresh-availability" style="display:none;">Refresh Availability</div>
                              </div>
                              <div style="display:flex;flex: 0 0 auto;padding: .4em 0;">
                                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield fwformcontrol" data-caption="Search by Description" data-datafield="SearchBox" style="flex: 1 1 400px;"></div>
                              </div>
                              <div style="display:flex;flex:1 1 0;">
                                <div class="flexcolumn" style="flex:0 0 230px;display:flex;flex-direction:column;position:relative;">
                                  <div id="categorycolumns">
                                    <div id="typeName"></div>
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
                             <div class="flexrow" style="max-width:1800px;">
                               <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                                 <div data-control="FwGrid" data-grid="SearchPreviewGrid" data-securitycaption="Preview"></div>
                               </div>
                             </div>
                             <div class="flexrow" style="max-width:1800px; justify-content: flex-end; margin-bottom:55px;">
                               <div data-type="button" class="fwformcontrol addToOrder" style="max-width:140px;">Add to ${type}</div>
                             </div>
                           </div>`;
        $popup.find('#previewtabpage').append(jQuery(previewhtml));
        
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

        let startDate;
        let stopDate;
        switch (type) {
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

        FwFormField.setValueByDataField($popup, 'ParentFormId', id);
        let warehouseId = (type === 'Transfer') ? JSON.parse(sessionStorage.getItem('warehouse')).warehouseid : FwFormField.getValueByDataField($form, 'WarehouseId');
        FwFormField.setValueByDataField($popup, 'WarehouseId', warehouseId);

        let userId = JSON.parse(sessionStorage.getItem('userid'));
        FwAppData.apiMethod(true, 'GET', `api/v1/usersearchsettings/${userId.webusersid}`, null, FwServices.defaultTimeout, function onSuccess(response) {
            //Render options sortable column list
            if (response.ResultFields) {
                let columns = JSON.parse(response.ResultFields);
                FwFormField.loadItems($popup.find('div[data-datafield="Columns"]'), columns);

                let columnsToHide = [];
                let columnOrder = [];
                for (let i = 0; i < columns.length; i++) {
                    let $this = columns[i];
                    columnOrder.push($this.value);

                    if ($this.selected == 'F') {
                        columnsToHide.push($this.value);
                    }
                }
                FwFormField.setValueByDataField($popup, 'ColumnOrder', columnOrder.join(','));
                FwFormField.setValueByDataField($popup, 'ColumnsToHide', columnsToHide.join(','));
            } else {
                FwFormField.loadItems($popup.find('div[data-datafield="Columns"]'),
                    [{ value: 'Description', text: 'Description',                         selected: 'T' },
                    { value: 'Type',         text: 'Type',                                selected: 'T' },
                    { value: 'Category',     text: 'Category',                            selected: 'T' },
                    { value: 'SubCategory',  text: 'Sub Category',                        selected: 'T' },
                    { value: 'Quantity',     text: 'Quantity',                            selected: 'T' },
                    { value: 'Available',    text: 'Available Quantity',                  selected: 'T' },
                    { value: 'ConflictDate', text: 'Conflict Date',                       selected: 'T' },
                    { value: 'AllWh',        text: 'Available Quantity (All Warehouses)', selected: 'T' },
                    { value: 'In',           text: 'In Quantity',                         selected: 'T' },
                    { value: 'QC',           text: 'QC Required Quantity',                selected: 'T' },
                    { value: 'Rate',         text: 'Rate',                                selected: 'T' }]);
            }

            if (response.DisableAccessoryAutoExpand) {
                FwFormField.setValueByDataField($popup, 'DisableAccessoryAutoExpand', true);
            }

            if (response.HideZeroQuantity) {
                FwFormField.setValueByDataField($popup, 'HideZeroQuantity', true);
            }

            $popup.find('#itemlist').attr('data-view', response.Mode || 'GRID')
        }, null, null);


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

        //Display # of items from previous session in preview tab
        FwAppData.apiMethod(true, 'GET', `api/v1/inventorysearch/gettotal/${id}`, null, FwServices.defaultTimeout, function onSuccess(response) {
            if (response.TotalQuantityInSession) {
                $popup.find('.tab[data-caption="Preview"] .caption').text(`Preview (${response.TotalQuantityInSession})`);

                FwNotification.renderNotification('WARNING', 'There are items from a previous Search session that have not been added.  Click the Preview tab to view.');

            }
        }, null, null);

        //Hide columns based on type
        if (type === 'PurchaseOrder' || type === 'Template') {
            $popup.find('.hideColumns').css('display', 'none');
        }

        return $popup;
    }
    //----------------------------------------------------------------------------------------------
    populateTypeMenu($popup) {
        let self                      = this;
        let inventoryTypeRequest: any = {};
        let $breadcrumbs              = $popup.find('#breadcrumbs');
        let categoryType;

        $breadcrumbs.empty();
        switch (FwFormField.getValue($popup, '[data-datafield="InventoryType"]')) {
            case 'R':
                inventoryTypeRequest.uniqueids = { Rental: true };
                categoryType                   = 'rentalcategory';
                $breadcrumbs.append('<div class="basetype breadcrumb"><div class="value">RENTAL</div></div>');
                $popup.find('#itemsearch').attr('data-base', 'RENTAL');
                break;
            case 'S':
                inventoryTypeRequest.uniqueids = { Sales: true };
                categoryType                   = 'salescategory';
                $breadcrumbs.append('<div class="basetype breadcrumb"><div class="value">SALES</div></div>');
                $popup.find('#itemsearch').attr('data-base', 'SALES');
                break;
            case 'L':
                inventoryTypeRequest.uniqueids = { Labor: true };
                categoryType                   = 'laborcategory';
                $breadcrumbs.append('<div class="basetype breadcrumb"><div class="value">LABOR</div></div>');
                $popup.find('#itemsearch').attr('data-base', 'LABOR');
                break;
            case 'M':
                inventoryTypeRequest.uniqueids = { Misc: true };
                categoryType                   = 'misccategory';
                $breadcrumbs.append('<div class="basetype breadcrumb"><div class="value">MISC</div></div>');
                $popup.find('#itemsearch').attr('data-base', 'MISC');
                break;
            case 'P':
                inventoryTypeRequest.uniqueids = { Parts: true };
                categoryType                   = 'partscategory';
                $breadcrumbs.append('<div class="basetype breadcrumb"><div class="value">PARTS</div></div>');
                $popup.find('#itemsearch').attr('data-base', 'PARTS');
                break;
        }

        $popup.find('#typeName, #inventoryType, #category, #subCategory, #inventory').empty();

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
                    inventoryTypeColumn.append(`<ul class="fitText" data-value="${response.Rows[i][inventoryTypeIdIndex]}"><span>${response.Rows[i][inventoryTypeIndex]}</span></ul>`);
                }
            }

            //Resizes text to fit into the div
            //self.fitToParent('#inventoryType .fitText span');
        }, null, $popup.find('#searchpopup'));
        this.typeOnClickEvents($popup, categoryType);
    }
    //----------------------------------------------------------------------------------------------
    typeOnClickEvents($popup, categoryType) {
        const $searchpopup     = $popup.find('#searchpopup');
        let self               = this;
        let $typeName          = $popup.find('#typeName');
        let inventoryTypeValue = FwFormField.getValue($popup, '[data-datafield="InventoryType"]');

        switch (inventoryTypeValue) {
            case 'R': inventoryTypeValue = "RENTAL"; break;
            case 'S': inventoryTypeValue = "SALES";  break;
            case 'L': inventoryTypeValue = "LABOR";  break;
            case 'M': inventoryTypeValue = "MISC";   break;
            case 'P': inventoryTypeValue = "PARTS";  break;
        }
        $typeName.append(`<ul class="inventoryTypeNav fitText" data-value="${inventoryTypeValue}"><span class="downArrowNav"><i class="material-icons">keyboard_arrow_down</i></span><span>${inventoryTypeValue}</span></ul>`);
        //Click event for main inv type nav to refresh type list
        $typeName.find('.inventoryTypeNav').on('click', e => {
            $popup.find('[data-type="radio"]').change();
        });

        $popup.off('click', '#inventoryType ul');
        $popup.on('click', '#inventoryType ul', e => {
            const $this = jQuery(e.currentTarget);
            $popup.find('#inventoryType ul').removeClass('selected');
            $this.addClass('selected');

            //Clear out existing bread crumbs and start new one
            let $breadcrumbs = $popup.find('#breadcrumbs');
            $breadcrumbs.find('.type, .category, .subcategory').remove();
            $breadcrumbs.append(`<div class="type breadcrumb"><div class="value">${$this.find('span:first-of-type').text()}</div></div>`);

            $popup.find('#itemsearch').attr('data-inventorytypeid', $this.attr('data-value'));
            $popup.find('#itemsearch').attr('data-categoryid', undefined);
            $popup.find('#itemsearch').attr('data-subcategoryid', undefined);

            //Jason H - 10/09/18 layout changes to left-side category columns
            $popup.find('#inventoryType ul').not('.selected').hide();
            if ($this.find('span').hasClass('downArrowNav') == false) {
                $this.append(`<span class="downArrowNav"><i class="material-icons">keyboard_arrow_down</i></span>`);
            }

            
            let typeRequest: any = {
                searchfieldoperators: ["<>"],
                searchfields:         ["Inactive"],
                searchfieldvalues:    ["T"]
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
                        categoryColumn.append(`<ul class="fitText" data-value="${response.Rows[i][categoryIdIndex]}"><span>${response.Rows[i][categoryIndex]}</span></ul>`);
                    }
                }
                if (response.Rows.length === 1) {
                    $popup.find("#category > ul").trigger('click');
                }

                //self.fitToParent('#category .fitText span');
            }, null, $searchpopup);
            this.categoryOnClickEvents($popup);
        });
    }
    //----------------------------------------------------------------------------------------------
    categoryOnClickEvents($popup) {
        const $searchpopup = $popup.find('#searchpopup');
        let self           = this;

        $popup.off('click', '#category ul');
        $popup.on('click', '#category ul', e => {
            const $this = jQuery(e.currentTarget);

            $popup.find('#category ul').removeClass('selected');
            $this.addClass('selected');

            //Clear and set new breadcrumbs
            let $breadcrumbs = $popup.find('#breadcrumbs');
            $breadcrumbs.find('.category, .subcategory').remove();
            $breadcrumbs.append(`<div class="category breadcrumb"><div class="value">${$this.find('span:first-of-type').text()}</div></div>`);

            $popup.find('#itemsearch').attr('data-categoryid', $this.attr('data-value'));
            $popup.find('#itemsearch').attr('data-subcategoryid', undefined);

            let subCatListRequest: any = {};
            subCatListRequest.uniqueids = {
                CategoryId: $this.attr('data-value'),
                TypeId:     $popup.find('#itemsearch').attr('data-inventorytypeid'),
                RecType:    FwFormField.getValueByDataField($popup, 'InventoryType')
            }

            FwAppData.apiMethod(true, 'POST', "api/v1/subcategory/browse", subCatListRequest, FwServices.defaultTimeout, function onSuccess(response) {
                let subCategoryIdIndex = response.ColumnIndex.SubCategoryId;
                let subCategoryIndex   = response.ColumnIndex.SubCategory;
                $popup.find('#subCategory').empty();

                let subCategories: any = [];
                let subCategoryColumn  = $popup.find('#subCategory');
                for (let i = 0; i < response.Rows.length; i++) {
                    if (subCategories.indexOf(response.Rows[i][subCategoryIndex]) == -1) {
                        subCategories.push(response.Rows[i][subCategoryIndex]);
                        subCategoryColumn.append(`<ul class="fitText" data-value="${response.Rows[i][subCategoryIdIndex]}"><span>${response.Rows[i][subCategoryIndex]}</span></ul>`);
                    }
                }
                if (response.Rows.length == 1) {
                    $popup.find("#subCategory > ul").trigger('click');
                }
                let hasSubCategories = false;
                if (response.Rows.length > 0) {
                    hasSubCategories = true;
                }
                //Load the Inventory items if selected category doesn't have any sub-categories
                if (hasSubCategories == false) {
                    let parentFormId = FwFormField.getValueByDataField($popup, 'ParentFormId');
                    let request: any = {
                        OrderId:                       parentFormId,
                        SessionId:                     parentFormId,
                        CategoryId:                    $this.attr('data-value'),
                        InventoryTypeId:               $popup.find('#itemsearch').attr('data-inventorytypeid'),
                        AvailableFor:                  FwFormField.getValueByDataField($popup, 'InventoryType'),
                        WarehouseId:                   FwFormField.getValueByDataField($popup, 'WarehouseId'),
                        ShowAvailability:              $popup.find('[data-datafield="Columns"] li[data-value="Available"]').attr('data-selected') === 'T' ? true : false,
                        SortBy:                        FwFormField.getValueByDataField($popup, 'SortBy'),
                        Classification:                FwFormField.getValueByDataField($popup, 'Select'),
                        HideInventoryWithZeroQuantity: FwFormField.getValue2($popup.find('[data-datafield="HideZeroQuantity"]')) == "T" ? true : false,
                        ShowImages:                    true,
                        FromDate:                      FwFormField.getValueByDataField($popup, 'FromDate') || undefined,
                        ToDate:                        FwFormField.getValueByDataField($popup, 'ToDate') || undefined
                    }

                    FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/search", request, FwServices.defaultTimeout, function onSuccess(response) {
                        $popup.find('#inventory').empty();
                        if (response.Rows.length > 0) {
                            const qtyIsStaleIndex = response.ColumnIndex.QuantityAvailableIsStale;
                            let obj = response.Rows.find(x => x[qtyIsStaleIndex] == true);
                            if (typeof obj != 'undefined') {
                                $popup.find('.refresh-availability').show();
                            }
                        }
                        SearchInterfaceController.renderInventory($popup, response);
                    }, null, $searchpopup);
                } else {
                    $popup.find('#inventory').empty();

                    //Jason H - 10/09/18 layout changes to left-side category columns
                    $popup.find('#category ul').not('.selected').hide();
                    if ($this.find('span').hasClass('downArrowNav') == false) {
                        $this.append(`<span class="downArrowNav"><i class="material-icons">keyboard_arrow_down</i></span>`);
                    }
                }
                //self.fitToParent('#subCategory .fitText span');
            }, null, $searchpopup);
            this.subCategoryOnClickEvents($popup);
        });
    }
    //----------------------------------------------------------------------------------------------
    subCategoryOnClickEvents($popup) {
        const $searchpopup = $popup.find('#searchpopup');

        $popup.off('click', '#subCategory ul');
        $popup.on('click', '#subCategory ul', function (e) {
            let parentFormId;
            const $this = jQuery(e.currentTarget);

            $popup.find('#subCategory ul').removeClass('selected');
            $this.addClass('selected');
            parentFormId = FwFormField.getValueByDataField($popup, 'ParentFormId');

            //Clear and set breadcrumbs
            let $breadcrumbs = $popup.find('#breadcrumbs');
            $breadcrumbs.find('.subcategory').remove();
            $breadcrumbs.append(`<div class="subcategory breadcrumb"><div class="value">${$this.find('span:first-of-type').text()}</div></div>`);

            $popup.find('#itemsearch').attr('data-subcategoryid', $this.attr('data-value'));

            let request: any = {
                OrderId:                       parentFormId,
                SessionId:                     parentFormId,
                CategoryId:                    $popup.find('#itemsearch').attr('data-categoryid'),
                SubCategoryId:                 $this.attr('data-value'),
                InventoryTypeId:               $popup.find('#itemsearch').attr('data-inventorytypeid'),
                AvailableFor:                  FwFormField.getValueByDataField($popup, 'InventoryType'),
                WarehouseId:                   FwFormField.getValueByDataField($popup, 'WarehouseId'),
                ShowAvailability:              $popup.find('[data-datafield="Columns"] li[data-value="Available"]').attr('data-selected') === 'T' ? true : false,
                SortBy:                        FwFormField.getValueByDataField($popup, 'SortBy'),
                Classification:                FwFormField.getValueByDataField($popup, 'Select'),
                HideInventoryWithZeroQuantity: FwFormField.getValue2($popup.find('[data-datafield="HideZeroQuantity"]')) === 'T' ? true : false,
                ShowImages:                    true,
                FromDate:                      FwFormField.getValueByDataField($popup, 'FromDate') || undefined,
                ToDate:                        FwFormField.getValueByDataField($popup, 'ToDate') || undefined
            }

            FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/search", request, FwServices.defaultTimeout, function onSuccess(response) {
                $popup.find('#inventory').empty();
                if (response.Rows.length > 0) {
                    const qtyIsStaleIndex = response.ColumnIndex.QuantityAvailableIsStale;
                    let obj = response.Rows.find(x => x[qtyIsStaleIndex] == true);
                    if (typeof obj != 'undefined') {
                        $popup.find('.refresh-availability').show();
                    }
                }
                SearchInterfaceController.renderInventory($popup, response);
            }, null, $searchpopup);
        });
    }
    //----------------------------------------------------------------------------------------------
    renderInventory($popup, response) {
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
            qtyIsStaleIndex           = response.ColumnIndex.QuantityAvailableIsStale;

        let $inventoryContainer = $popup.find('#inventory');
        if (response.Rows.length == 0) {
            $inventoryContainer.append('<span style="font-weight: bold; font-size=1.3em">No Results</span>');
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

            if (response.Rows[i][qtyIsStaleIndex] === true) {
                $itemcontainer.find('div[data-column="Available"]').attr('data-state', 'STALE');
            } else if (response.Rows[i][quantityAvailable] > 0) {
                $itemcontainer.find('div[data-column="Available"]').attr('data-state', 'AVAILABLE');
            } else if (response.Rows[i][quantityAvailable] <= 0) {
                $itemcontainer.find('div[data-column="Available"]').attr('data-state', 'NOTAVAILABLE');
            }

            if (response.Rows[i][classificationIndex] == "K" || response.Rows[i][classificationIndex] == "C") {
                $itemcontainer.find('div[data-column="Description"] .descriptionrow').append(`<div class="classdescription">${response.Rows[i][classificationDescription]}</div>`)
                $itemcontainer.find('.classdescription').css({ 'background-color': response.Rows[i][classificationColor] });
                $itemcontainer.find('div[data-column="Description"]').append('<div class="toggleaccessories">Show Accessories</div>');
                $itemcontainer.append(`<div class="item-accessories" data-classification="${response.Rows[i][classificationIndex]}" style="display:none;"></div>`);
            }
        }

        let $inventory = $popup.find('.item-info');
        let view       = $popup.find('#itemlist').attr('data-view');

        let type = $popup.find('#itemsearch').attr('data-moduletype');
        if (type === 'PurchaseOrder' || type === 'Template') {
            $popup.find('.hideColumns').css('display', 'none');
        }

        this.listGridView($inventory, view);
    }
    //----------------------------------------------------------------------------------------------
    listGridView($inventory, viewType) {
        let $searchpopup = jQuery('#searchpopup') ;

        if (viewType !== 'GRID') {
            //custom display/sequencing for columns
            let columnsToHide = FwFormField.getValueByDataField($searchpopup, 'ColumnsToHide').split(',');
            $searchpopup.find('.columnDescriptions .columnorder, .item-info .columnorder').css('display', '');
            for (let i = 0; i < columnsToHide.length; i++) {
                $searchpopup.find(`.columnDescriptions [data-column="${columnsToHide[i]}"], .item-info [data-column="${columnsToHide[i]}"]`).hide();
            };

            let columnOrder = FwFormField.getValueByDataField($searchpopup, 'ColumnOrder').split(',');
            for (let i = 0; i < columnOrder.length; i++) {
                $searchpopup.find(`.columnDescriptions [data-column="${columnOrder[i]}"], .item-info [data-column="${columnOrder[i]}"]`).css('order', i);
            };

        }
    }
    //----------------------------------------------------------------------------------------------
    events($popup, $form, id) {
        let self           = this;
        const $options     = $popup.find('.options');
        let userId         = JSON.parse(sessionStorage.getItem('userid'));
        let hasItemInGrids = false;
        let warehouseId    = FwFormField.getValueByDataField($popup, 'WarehouseId');
        let $searchpopup   = $popup.find('#searchpopup');

        $popup.on('click', '#breadcrumbs .basetype', e => {
            self.populateTypeMenu($popup);
        });

        $popup.on('click', '#breadcrumbs .type', e => {
            let inventorytypeid = $popup.find('#itemsearch').attr('data-inventorytypeid');
            $popup.find(`#inventoryType ul[data-value="${inventorytypeid}"]`).click();
        });

        $popup.on('click', '#breadcrumbs .category', e => {
            let categoryid = $popup.find('#itemsearch').attr('data-categoryid');
            $popup.find(`#category ul[data-value="${categoryid}"]`).click();
        });

        //Close the Search Interface popup
        $popup.find('.close-modal').one('click', function (e) {
            FwPopup.destroyPopup($popup);
            jQuery(document).find('.fwpopup').off('click');
            jQuery(document).off('keydown');
        });

        $popup.on('click', '.options .optionsbutton', (event: JQuery.ClickEvent) => {
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
        });

        //Apply options and close options menu
        $popup.on('click', '.applyOptions', e => {
            let $columns = $popup.find('[data-datafield="Columns"] li');
            let selectedColumns = [];
            let notSelectedColumns = [];

            for (let i = 0; i < $columns.length; i++) {
                let $this = jQuery($columns[i]);
                let column: any = {};
                $searchpopup.find(`.columnorder[data-column="${$this.attr('data-value')}"]`).css('order', i);
                if ($this.attr('data-selected') == 'T') {
                    column.order = i;
                    column.value = $this.attr('data-value');
                    column.text = $this.find('label').text();
                    column.selected = 'T';
                    selectedColumns.push(column);
                } else {
                    column.value = $this.attr('data-value');
                    column.text = $this.find('label').text();
                    column.selected = 'F';
                    notSelectedColumns.push(column);
                }
            }
            Array.prototype.push.apply(selectedColumns, notSelectedColumns);
            let columnOrder = selectedColumns.map(a => a.value);
            FwFormField.setValueByDataField($popup, 'ColumnOrder', columnOrder.join(','));

            let request: any = {
                ResultFields:                JSON.stringify(selectedColumns),
                WebUserId:                   userId.webusersid,
                DisableAccessoryAutoExpand:  FwFormField.getValue2($popup.find('[data-datafield="DisableAccessoryAutoExpand"]')) == "T" ? true : false,
                HideZeroQuantity:            FwFormField.getValue2($popup.find('[data-datafield="HideZeroQuantity"]')) == "T" ? true : false
            }

            FwAppData.apiMethod(true, 'POST', "api/v1/usersearchsettings/", request, FwServices.defaultTimeout, function onSuccess(response) {
                let columnsToHide = notSelectedColumns.map(a => a.value);
                FwFormField.setValueByDataField($popup, 'ColumnsToHide', columnsToHide.join(','));
                $popup.find('.options').removeClass('active');
                $popup.find('.options .optionsmenu').css('z-index', '0');

                //perform search again with new settings
                let searchrequest: any = {
                    OrderId:                       id,
                    SessionId:                     id,
                    AvailableFor:                  FwFormField.getValueByDataField($popup, 'InventoryType'),
                    WarehouseId:                   warehouseId,
                    ShowAvailability:              $popup.find('[data-datafield="Columns"] li[data-value="Available"]').attr('data-selected') === 'T' ? true : false,
                    ShowImages:                    true,
                    SortBy:                        FwFormField.getValueByDataField($popup, 'SortBy'),
                    Classification:                FwFormField.getValueByDataField($popup, 'Select'),
                    HideInventoryWithZeroQuantity: request.HideZeroQuantity,
                    SearchText:                    FwFormField.getValueByDataField($popup, 'SearchBox'),
                    ToDate:                        FwFormField.getValueByDataField($popup, 'ToDate') || undefined,
                    FromDate:                      FwFormField.getValueByDataField($popup, 'FromDate') || undefined,
                    CategoryId:                    $popup.find('#itemsearch').attr('data-categoryid') || undefined,
                    InventoryTypeId:               $popup.find('#itemsearch').attr('data-inventorytypeid') || undefined
                }

                FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/search", searchrequest, FwServices.defaultTimeout, function onSuccess(response) {
                    $popup.find('#inventory').empty();
                    if (response.Rows.length > 0) {
                        const qtyIsStaleIndex = response.ColumnIndex.QuantityAvailableIsStale;
                        let obj = response.Rows.find(x => x[qtyIsStaleIndex] == true);
                        if (typeof obj != 'undefined') {
                            $popup.find('.refresh-availability').show();
                        }
                    }
                    SearchInterfaceController.renderInventory($popup, response);
                }, null, $searchpopup);

                if (request.DisableAccessoryAutoExpand) {
                    $popup.find('.item-accessories').css('display', 'none');
                }
            }, null, $searchpopup);
        });

        //Reset options to defaults
        $popup.on('click', '.restoreDefaults', e => {
            FwFormField.loadItems($popup.find('div[data-datafield="Columns"]'),
                [{ value: 'Description', text: 'Description',                         selected: 'T' },
                { value: 'Quantity',     text: 'Quantity',                            selected: 'T' },
                { value: 'Type',         text: 'Type',                                selected: 'F' },
                { value: 'Category',     text: 'Category',                            selected: 'F' },
                { value: 'SubCategory',  text: 'Sub Category',                        selected: 'F' },
                { value: 'Available',    text: 'Available Quantity',                  selected: 'T' },
                { value: 'ConflictDate', text: 'Conflict Date',                       selected: 'T' },
                { value: 'AllWh',        text: 'Available Quantity (All Warehouses)', selected: 'T' },
                { value: 'In',           text: 'In Quantity',                         selected: 'T' },
                { value: 'QC',           text: 'QC Required Quantity',                selected: 'T' },
                { value: 'Rate',         text: 'Rate',                                selected: 'T' }]);

            FwFormField.setValueByDataField($popup, 'DisableAccessoryAutoExpand', false);
            FwFormField.setValueByDataField($popup, 'HideZeroQuantity', false);
            let gridView = $popup.find('#itemlist').attr('data-view');
            let $inventory = $popup.find('div.item-info');
            self.listGridView($inventory, gridView);

            $popup.find('.options').removeClass('active');
            $popup.find('.options .optionsmenu').css('z-index', '0');
        });

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
                    e.preventDefault();
                    let request: any = {
                        OrderId:                       id,
                        SessionId:                     id,
                        AvailableFor:                  FwFormField.getValueByDataField($popup, 'InventoryType'),
                        WarehouseId:                   warehouseId,
                        ShowAvailability:              $popup.find('[data-datafield="Columns"] li[data-value="Available"]').attr('data-selected') === 'T' ? true : false,
                        ShowImages:                    true,
                        SortBy:                        FwFormField.getValueByDataField($popup, 'SortBy'),
                        Classification:                FwFormField.getValueByDataField($popup, 'Select'),
                        HideInventoryWithZeroQuantity: FwFormField.getValue2($popup.find('[data-datafield="HideZeroQuantity"]')) == "T" ? true : false,
                        SearchText:                    FwFormField.getValueByDataField($popup, 'SearchBox'),
                        FromDate:                      FwFormField.getValueByDataField($popup, 'FromDate') || undefined,
                        ToDate:                        FwFormField.getValueByDataField($popup, 'ToDate') || undefined
                    };

                    FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/search", request, FwServices.defaultTimeout, function onSuccess(response) {
                        $popup.find('#inventory').empty();
                        $popup.find('#breadcrumbs').find('.type, .category, .subcategory').remove();
                        if (response.Rows.length > 0) {
                            const qtyIsStaleIndex = response.ColumnIndex.QuantityAvailableIsStale;
                            let obj = response.Rows.find(x => x[qtyIsStaleIndex] == true);
                            if (typeof obj != 'undefined') {
                                $popup.find('.refresh-availability').show();
                            }
                        }
                        SearchInterfaceController.renderInventory($popup, response);
                    }, null, $searchpopup);
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
        $popup.on('change', '.item-info [data-column="Quantity"] input', e => {
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
        });

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
                let request = {
                    OrderId: id,
                    SessionId: id
                }
                $popup.find('.addToOrder').css('cursor', 'wait');
                $popup.off('click', '.addToOrder');
                FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/addto", request, FwServices.defaultTimeout, function onSuccess(response) {
                    FwPopup.destroyPopup(jQuery(document).find('.fwpopup'));
                    let $combinedGrid = $form.find('.combinedgrid [data-name="OrderItemGrid"]'),
                        $orderItemGridRental = $form.find('.rentalgrid [data-name="OrderItemGrid"]'),
                        $orderItemGridSales = $form.find('.salesgrid [data-name="OrderItemGrid"]'),
                        $orderItemGridLabor = $form.find('.laborgrid [data-name="OrderItemGrid"]'),
                        $orderItemGridMisc = $form.find('.miscgrid [data-name="OrderItemGrid"]');
                    let $transferItemGridRental = $form.find('.rentalItemGrid [data-name="TransferOrderItemGrid"]');
                    let $transferItemGridSales = $form.find('.salesItemGrid [data-name="TransferOrderItemGrid"]');
                    if ($form.find('.combinedtab').css('display') != 'none') {
                        FwBrowse.search($combinedGrid);
                    }

                    if ($form.find('.notcombinedtab').css('display') != 'none') {
                        FwBrowse.search($orderItemGridRental);
                        FwBrowse.search($orderItemGridMisc);
                        FwBrowse.search($orderItemGridLabor);
                        FwBrowse.search($orderItemGridSales);
                        FwBrowse.search($transferItemGridRental);
                        FwBrowse.search($transferItemGridSales);
                    }
                }, null, $searchpopup, id);
            }
        });

        //Saves the user's inventory view setting
        $popup.on('click', '.invviewbtn', e => {
            let $this = jQuery(e.currentTarget),
                view  = $this.attr('data-buttonview');

            $popup.find('#itemlist').attr('data-view', view);
            self.listGridView($popup.find('div.item-info'), view);

            let viewrequest: any = {
                WebUserId: userId.webusersid,
                Mode:      view
            };
            FwAppData.apiMethod(true, 'POST', "api/v1/usersearchsettings/", viewrequest, FwServices.defaultTimeout, function onSuccess(response) { }, null, null);
        });

        //Accessory quantity change
        $popup.on('change', '.item-accessory-info [data-column="Quantity"] input', e => {
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
        });

        //Sorting option events
        $popup.on('change', 'div[data-datafield="Select"], div[data-datafield="SortBy"]', e => {
            let request: any = {
                OrderId:                       id,
                SessionId:                     id,
                AvailableFor:                  FwFormField.getValueByDataField($popup, 'InventoryType'),
                WarehouseId:                   warehouseId,
                ShowAvailability:              $popup.find('[data-datafield="Columns"] li[data-value="Available"]').attr('data-selected') === 'T' ? true : false,
                SortBy:                        FwFormField.getValueByDataField($popup, 'SortBy'),
                Classification:                FwFormField.getValueByDataField($popup, 'Select'),
                HideInventoryWithZeroQuantity: FwFormField.getValue2($popup.find('[data-datafield="HideZeroQuantity"]')) == "T" ? true : false,
                ShowImages:                    true,
                FromDate:                      FwFormField.getValueByDataField($popup, 'FromDate') || undefined,
                ToDate:                        FwFormField.getValueByDataField($popup, 'ToDate') || undefined,
                InventoryTypeId:               $popup.find('#itemsearch').attr('data-inventorytypeid') || undefined,
                CategoryId:                    $popup.find('#itemsearch').attr('data-categoryid') || undefined,
                SubCategoryId:                 $popup.find('#itemsearch').attr('data-subcategoryid') || undefined,
                SearchText:                    FwFormField.getValueByDataField($popup, 'SearchBox') || undefined
            }

            FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/search", request, FwServices.defaultTimeout, function onSuccess(response) {
                $popup.find('#inventory').empty();
                if (response.Rows.length > 0) {
                    const qtyIsStaleIndex = response.ColumnIndex.QuantityAvailableIsStale;
                    let obj = response.Rows.find(x => x[qtyIsStaleIndex] == true);
                    if (typeof obj != 'undefined') {
                        $popup.find('.refresh-availability').show();
                    }
                }
                self.renderInventory($popup, response);
            }, null, $searchpopup);
        });

        //Refresh Availability button
        $popup.on('click', '.refresh-availability', e => {
            let request: any = {
                OrderId:                       id,
                SessionId:                     id,
                AvailableFor:                  FwFormField.getValueByDataField($popup, 'InventoryType'),
                WarehouseId:                   warehouseId,
                ShowAvailability:              $popup.find('[data-datafield="Columns"] li[data-value="Available"]').attr('data-selected') === 'T' ? true : false,
                ShowImages:                    true,
                SortBy:                        FwFormField.getValueByDataField($popup, 'SortBy'),
                Classification:                FwFormField.getValueByDataField($popup, 'Select'),
                HideInventoryWithZeroQuantity: FwFormField.getValue2($popup.find('[data-datafield="HideZeroQuantity"]')) == "T" ? true : false,
                SearchText:                    FwFormField.getValueByDataField($popup, 'SearchBox'),
                FromDate:                      FwFormField.getValueByDataField($popup, 'FromDate') || undefined,
                ToDate:                        FwFormField.getValueByDataField($popup, 'ToDate') || undefined,
                InventoryTypeId:               $popup.find('#itemsearch').attr('data-inventorytypeid') || undefined,
                CategoryId:                    $popup.find('#itemsearch').attr('data-categoryid') || undefined,
                SubCategoryId:                 $popup.find('#itemsearch').attr('data-subcategoryid') || undefined,
                RefreshAvailability:           true
            }

            FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/search", request, FwServices.defaultTimeout, function onSuccess(response) {
                $popup.find('#inventory').empty();
                $popup.find('.refresh-availability').hide();
                SearchInterfaceController.renderInventory($popup, response);
            }, null, $searchpopup);
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
            const thumbnail                      = response.ColumnIndex.Thumbnail
            const appImageId                     = response.ColumnIndex.ImageId

            for (var i = 0; i < response.Rows.length; i++) {
                let imageThumbnail = response.Rows[i][thumbnail]  ? response.Rows[i][thumbnail]  : './theme/images/no-image.jpg';
                let imageId        = response.Rows[i][appImageId] ? response.Rows[i][appImageId] : '';
                let conflictdate   = response.Rows[i][conflictIndex] ? moment(response.Rows[i][conflictIndex]).format('L') : '';

                let accessoryhtml = `<div class="item-accessory-info" data-inventoryid="${response.Rows[i][inventoryIdIndex]}">
                                       <div data-column="ItemImage"><img src="${imageThumbnail}" data-value="${imageId}" alt="Image" class="image"></div>
                                       <div data-column="Description" class="columnorder"><div class="descriptionrow">${response.Rows[i][descriptionIndex]}</div></div>
                                       <div data-column="Quantity" class="columnorder">
                                         <div style="float:left; border:1px solid #bdbdbd;">
                                           <button class="decrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">-</button>
                                           <input type="number" style="padding: 5px 0px; float:left; width:50%; border:none; text-align:center;" value="${response.Rows[i][qtyIndex]}">
                                           <button class="incrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">+</button>
                                         </div>
                                       </div>
                                       <div class="columnorder hideColumns" data-column="Available" data-datafield="QuantityAvailable"><div class="available-color">${response.Rows[i][qtyAvailIndex]}</div></div>
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

                let $qty = $itemaccessoryinfo.find('[data-column="Quantity"]');
                $qty.append('<div class="quantityColor"></div>');
                let $quantityColorDiv = $qty.find('.quantityColor');

                let qtycolor;
                if (response.Rows[i][quantityColorIndex] == "" || response.Rows[i][quantityColorIndex] == null) {
                    qtycolor = 'transparent';
                } else {
                    qtycolor = response.Rows[i][quantityColorIndex];
                };
                $quantityColorDiv.css('border-left-color', qtycolor);

                if (response.Rows[i][qtyIsStaleIndex] === true) {
                    $itemaccessoryinfo.find('div[data-datafield="QuantityAvailable"]').attr('data-state', 'STALE');
                } else if (response.Rows[i][qtyAvailIndex] > 0) {
                    $itemaccessoryinfo.find('div[data-datafield="QuantityAvailable"]').attr('data-state', 'AVAILABLE');
                } else if (response.Rows[i][qtyAvailIndex] <= 0) {
                    $itemaccessoryinfo.find('div[data-datafield="QuantityAvailable"]').attr('data-state', 'NOTAVAILABLE');
                }

                if (response.Rows[i][classificationIndex] == "K" || response.Rows[i][classificationIndex] == "C") {
                    $itemaccessoryinfo.find('div[data-column="Description"] .descriptionrow').append(`<div class="classdescription">${response.Rows[i][classificationDescriptionIndex]}</div>`)
                    $itemaccessoryinfo.find('.classdescription').css({ 'background-color': response.Rows[i][classificationColorIndex] });
                }

                let type = $popup.find('#itemsearch').attr('data-moduletype');
                if (type === 'PurchaseOrder' || type === 'Template') {
                    $popup.find('.hideColumns').css('display', 'none');
                }

                //custom display/sequencing for columns
                let columnsToHide = FwFormField.getValueByDataField($popup, 'ColumnsToHide').split(',');
                $popup.find('.item-accessories .columnorder').css('display', '');
                for (let i = 0; i < columnsToHide.length; i++) {
                    $popup.find(`.item-accessories [data-column="${columnsToHide[i]}"]`).hide();
                };

                let columnOrder = FwFormField.getValueByDataField($popup, 'ColumnOrder').split(',');
                for (let i = 0; i < columnOrder.length; i++) {
                    $popup.find(`.item-accessories [data-column="${columnOrder[i]}"]`).css('order', i);
                };
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
    fitToParent(selector) {
        let numIter
            , regexp
            , fontSize;
        numIter = 10;
        regexp = /\d+(\.\d+)?/;
        fontSize = function (elem) {
            let match = elem.css('font-size').match(regexp)
                , size = match == null ? 16 : parseFloat(match[0]);
            return isNaN(size) ? 16 : size;
        }
        let test = jQuery(selector);
        jQuery(selector).each(function () {
            let elem = jQuery(this)
                , parentWidth = elem.parent().width()
                , parentHeight = elem.parent().height();
            if (elem.width() > parentWidth || elem.height() > parentHeight) {
                let maxSize = fontSize(elem), minSize = 0.1;
                for (let i = 0; i < numIter; i++) {
                    var currSize = (minSize + maxSize) / 2;
                    elem.css('font-size', currSize);
                    if (elem.width() > parentWidth || elem.height() > parentHeight) {
                        maxSize = currSize;
                    } else {
                        minSize = currSize;
                    }
                }
                elem.css('font-size', minSize);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
}

var SearchInterfaceController = new SearchInterface();