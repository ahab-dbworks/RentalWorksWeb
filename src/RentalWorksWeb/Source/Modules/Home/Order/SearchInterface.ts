class SearchInterface {
    renderSearchPopup($form, id, type, gridInventoryType?) {
        let html = []
            , $popupHtml
            , $popup
            , searchhtml
            , $searchform
            , $moduleTabControl
            , newtabids
            , $searchTabControl
            , $select
            , $sortby
            , previewhtml
            , $previewform
            , newtabids2
            , $previewTabControl;

        //Build and invoke popup
        html.push('<div id="searchpopup">');
        html.push('     <div id="searchTabs" class="fwcontrol fwtabs" data-rendermode="runtime" data-version="1" data-control="FwTabs">');
        html.push('         <div class="tabs"><div class="tabcontainer"></div></div>');
        html.push('         <div class="tabpages"></div>');
        html.push('     </div>');
        html.push('     <div class="close-modal"><i class="material-icons">clear</i><div class="btn-text">Close</div></div>');
        html.push('</div>');

        $popupHtml = html.join('');
        $popup = FwPopup.renderPopup(jQuery($popupHtml), { ismodal: true });
        FwPopup.showPopup($popup);

        searchhtml = [];
        searchhtml.push(`
<div id="searchFormHtml" class="fwform fwcontrol">
  <div class="flexpage">
    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield fwformcontrol" data-datafield="ParentFormId" style="display:none"></div>
    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield fwformcontrol" data-datafield="WarehouseId" style="display:none"></div>
    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield fwformcontrol" data-datafield="ColumnsToHide" style="display:none"></div>
    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield fwformcontrol" data-datafield="ColumnOrder" style="display:none"></div>
    <div id="type" style="display:none">${type}</div>
    <div class="fwmenu default"></div>
    <div class="flexrow" style="max-width:1650px;">
    <div data-type="button" class="fwformcontrol expandcategorycolumns" style="display:none;">&#8646; Expand</div>
      <div class="flexrow" style="min-width:100%;">
        <div data-control="FwFormField" class="fwcontrol fwformfield fwformcontrol" data-caption="" data-datafield="InventoryType" data-type="radio">
          <div data-value="R" data-caption="Rental"></div>
          <div data-value="S" data-caption="Sales"></div>
          <div data-value="L" data-caption="Labor"></div>
          <div data-value="M" data-caption="Misc"></div>`);

        if (type === 'PurchaseOrder') {
            searchhtml.push(`<div data-value="P" data-caption="Parts"></div>`);
        }
        searchhtml.push(`</div>`);

        let addToButton;
        switch (type) {
            case 'Transfer':
                addToButton = `<div data-type="button" class="fwformcontrol addToOrder" style="flex:0 0 135px;">Add to Transfer</div>`;
                break;
            case 'Order':
                addToButton = `<div data-type="button" class="fwformcontrol addToOrder" style="flex:0 0 120px;">Add to Order</div>`;
                break;
            case 'Quote':
                addToButton = `<div data-type="button" class="fwformcontrol addToOrder" style="flex:0 0 120px;">Add to Quote</div>`;
                break;
            case 'PurchaseOrder':
                addToButton = `<div data-type="button" class="fwformcontrol addToOrder" style="flex:0 0 195px;">Add to Purchase Order</div>`;
                break;
            case 'Template':
                addToButton = `<div data-type="button" class="fwformcontrol addToOrder" style="flex:0 0 140px;">Add to Template</div>`;
                break;
        }
        searchhtml.push(addToButton);
        searchhtml.push(`
     </div>
      <div class="flexrow" style="min-width:100%;">
        <div class="flexcolumn" style="max-width:220px; overflow-x:hidden; max-height:73vh;">
          <div class="flexrow">
            <div id="categorycolumns">
              <div id="typeName"></div>
              <div id="inventoryType"></div>
              <div id="category"></div>
              <div id="subCategory"></div>
            </div>
          </div>
        </div>
        <div class="flexcolumn formoptions" style="max-width:1450px; overflow:hidden; min-height:800px;">
          <div class="flexrow" style="max-width:100%;">
            <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield fwformcontrol" data-caption="Est. Start" data-datafield="FromDate" style="flex: 0 1 135px;"></div>
            <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield fwformcontrol" data-caption="Est. Stop" data-datafield="ToDate" style="flex: 0 1 135px;"></div>
            <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield fwformcontrol select" data-caption="Select" data-datafield="Select" style="flex: 0 1 150px;"></div>
            <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield fwformcontrol sortby" data-caption="Sort By" data-datafield="SortBy" style="flex: 0 1 255px;"></div>
          </div>
          <div class="flexrow" style="max-width:100%; z-index:2;">
            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield fwformcontrol" data-caption="Search" data-datafield="SearchBox" style="flex: 0 1 400px;"></div>
            <div data-type="button" class="invviewbtn fwformcontrol" data-buttonview="LIST"><i class="material-icons" style="margin-top: 5px;">&#xE8EE;</i></div>
            <div data-type="button" class="invviewbtn fwformcontrol" data-buttonview="HYBRID"><i class="material-icons" style="margin-top: 5px;">&#xE8EF;</i></div>
            <div data-type="button" class="invviewbtn fwformcontrol" data-buttonview="GRID"><i class="material-icons" style="margin-top: 5px;">&#xE8F0;</i></div>
            <div class="optiontoggle fwformcontrol" data-type="button">
              Options &#8675;
              <div class="options" style="display:none;">
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
            <div data-type="button" class="fwformcontrol refresh-availability" style="max-width:165px;margin:12px 0px 0px 10px;display:none;">Refresh Availability</div>
          </div>
          <div class="flexrow" style="max-width:100%;">
            <div class="flexcolumn">
              <div id="breadcrumbs" class="fwmenu default">
                <div class="basetype"></div>
                <div class="type"></div>
                <div class="category"></div>
                <div class="subcategory"></div>
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
              <div class="columnorder hideColumns" data-column="AllWh">All Wh</div>
              <div class="columnorder hideColumns" data-column="In">In</div>
              <div class="columnorder hideColumns" data-column="QC">QC</div>
              <div class="columnorder" data-column="Rate">Rate</div>
            </div>
            <div id="inventory"></div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>`);
        $searchform = searchhtml.join('');
        $moduleTabControl = jQuery('#searchTabs');
        newtabids = FwTabs.addTab($moduleTabControl, 'Search', false, 'FORM', true);
        $moduleTabControl.find(`#${newtabids.tabpageid}`).append(jQuery($searchform));
        $searchTabControl = jQuery('#searchFormHtml');
        FwConfirmation.addControls($searchTabControl, $searchform);

        //Populate the select and sort by fields
        $select = $popup.find('.select');
        $sortby = $popup.find('.sortby');
        FwFormField.loadItems($select, [
            { value: '', text: 'All' },
            { value: 'CK', text: 'Complete/Kit' },
            { value: 'N', text: 'Container' },
            { value: 'I', text: 'Item' },
            { value: 'A', text: 'Accessory' }], true);

        FwFormField.loadItems($sortby, [
            { value: 'INVENTORY', text: 'Type / Category / Sub-Category' },
            { value: 'ICODE', text: 'I-Code' },
            { value: 'DESCRIPTION', text: 'Description' },
            { value: 'PARTNO', text: 'Part No.' }], true);

        //Build preview tab
        switch (type) {
            case 'Transfer':
                previewhtml = `<div data-type="button" class="fwformcontrol addToOrder" style="max-width:135px;">Add to Transfer</div>`;
                break;
            case 'Order':
                previewhtml = `<div data-type="button" class="fwformcontrol addToOrder" style="max-width:120px;">Add to Order</div>`;
                break;
            case 'Quote':
                previewhtml = `<div data-type="button" class="fwformcontrol addToOrder" style="max-width:120px;">Add to Quote</div>`;
                break;
            case 'PurchaseOrder':
                previewhtml = `<div data-type="button" class="fwformcontrol addToOrder" style="max-width:195px;">Add to Purchase Order</div>`;
                break;
            case 'Template':
                previewhtml = `<div data-type="button" class="fwformcontrol addToOrder" style="max-width:140px;">Add to Template</div>`;
                break;
        };

        previewhtml = `<div id="previewHtml" class="fwform fwcontrol" style="overflow: scroll; height:-webkit-fill-available;">
                            <div class="fwmenu default"></div>
                            <div class="flexrow" style="max-width:1800px;">
                                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                                   <div data-control="FwGrid" data-grid="SearchPreviewGrid" data-securitycaption="Preview"></div>
                                </div>
                            </div>
                            <div class="flexrow" style="max-width:1800px; justify-content: flex-end; margin-bottom:55px;">
                           ${previewhtml}
                            </div>
                       </div>`;
        $previewform = jQuery(previewhtml);
        newtabids2 = FwTabs.addTab($moduleTabControl, 'Preview', false, 'FORM', false);
        $moduleTabControl.find('#' + newtabids2.tabpageid).append($previewform);
        FwTabs.init($moduleTabControl);
        $previewTabControl = jQuery('#previewHtml');
        FwConfirmation.addControls($previewTabControl, $previewform);

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
        let warehouseId;
        if (type == 'Transfer') {
            let warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
            warehouseId = warehouse.warehouseid;
        } else {
            warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
        }
        FwFormField.setValueByDataField($popup, 'WarehouseId', warehouseId);

        let userId = JSON.parse(sessionStorage.getItem('userid'));
        let $itemlist = $popup.find('#itemlist');
        FwAppData.apiMethod(true, 'GET', `api/v1/usersearchsettings/${userId.webusersid}`, null, FwServices.defaultTimeout, function onSuccess(response) {
            let columns;

            //Render options sortable column list
            if (response.ResultFields) {
                columns = JSON.parse(response.ResultFields);
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
                    [{ value: 'Description', text: 'Description', selected: 'T' },
                    { value: 'Type', text: 'Type', selected: 'T' },
                    { value: 'Category', text: 'Category', selected: 'T' },
                    { value: 'SubCategory', text: 'Sub Category', selected: 'T' },
                    { value: 'Quantity', text: 'Quantity', selected: 'T' },
                    { value: 'Available', text: 'Available Quantity', selected: 'T' },
                    { value: 'ConflictDate', text: 'Conflict Date', selected: 'T' },
                    { value: 'AllWh', text: 'Available Quantity (All Warehouses)', selected: 'T' },
                    { value: 'In', text: 'In Quantity', selected: 'T' },
                    { value: 'QC', text: 'QC Required Quantity', selected: 'T' },
                    { value: 'Rate', text: 'Rate', selected: 'T' }]);
            }

            if (response.DisableAccessoryAutoExpand) {
                FwFormField.setValueByDataField($popup, 'DisableAccessoryAutoExpand', true);
            }

            if (response.HideZeroQuantity) {
                FwFormField.setValueByDataField($popup, 'HideZeroQuantity', true);
            }

            if (response.Mode != null) {
                $itemlist.attr('data-view', response.Mode)
            } else {
                $itemlist.attr('data-view', 'GRID');
            }
        }, null, null);


        //Render preview grid
        const $previewGrid = $previewTabControl.find('[data-grid="SearchPreviewGrid"]');
        const $previewGridControl = FwBrowse.loadGridFromTemplate('SearchPreviewGrid');
        $previewGrid.empty().append($previewGridControl);
        $previewGridControl.data('ondatabind', request => {
            request.SessionId = id;
            request.ShowAvailability = true;
            request.FromDate = FwFormField.getValueByDataField($popup, 'FromDate');
            request.ToDate = FwFormField.getValueByDataField($popup, 'ToDate');
            request.ShowImages = true;
        });
        FwBrowse.init($previewGridControl);
        FwBrowse.renderRuntimeHtml($previewGridControl);

        //Load Type list
        let inventoryTypeRequest: any
            , availableFor
            , inventoryType
            , categoryType;
        inventoryTypeRequest = {}
        inventoryTypeRequest.searchfieldoperators = ["<>"];
        inventoryTypeRequest.searchfields = ["Inactive"];
        inventoryTypeRequest.searchfieldvalues = ["T"];
        availableFor = FwFormField.getValueByDataField($popup, 'InventoryType');
        inventoryType = $popup.find('[data-datafield="InventoryType"]');

        let mainTypeBreadCrumb = $popup.find('#breadcrumbs .basetype');

        //Sets inventory type by active tab
        if (typeof gridInventoryType == 'undefined') {
            gridInventoryType = $form.find('.tabs .active[data-type="tab"]').attr('data-caption');
            if (gridInventoryType == 'Miscellaneous') gridInventoryType = 'Misc';
        }

        switch (gridInventoryType) {
            default:
            case 'Rental':
                inventoryTypeRequest.uniqueids = {
                    Rental: true
                }
                categoryType = 'rentalcategory';
                inventoryType.find('[value="R"]').prop('checked', true);
                mainTypeBreadCrumb.text('RENTAL');
                break;
            case 'Sales':
                inventoryTypeRequest.uniqueids = {
                    Sales: true
                }
                categoryType = 'salescategory';
                inventoryType.find('[value="S"]').prop('checked', true);
                mainTypeBreadCrumb.text('SALES');
                break;
            case 'Labor':
                inventoryTypeRequest.uniqueids = {
                    Labor: true
                }
                categoryType = 'laborcategory';
                inventoryType.find('[value="L"]').prop('checked', true);
                mainTypeBreadCrumb.text('LABOR');
                break;
            case 'Misc':
                inventoryTypeRequest.uniqueids = {
                    Misc: true
                }
                categoryType = 'misccategory';
                inventoryType.find('[value="M"]').prop('checked', true);
                mainTypeBreadCrumb.text('MISC');
                break;
        };
        mainTypeBreadCrumb.append('<div style="float:right;">&#160; &#160; &#47; &#160; &#160;</div>');

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

        this.populateTypeMenu($popup, inventoryTypeRequest, categoryType);
        this.breadCrumbs($popup);
        this.events($popup, $form, id);
        return $popup;
    }

    populateTypeMenu($popup, inventoryTypeRequest, categoryType) {
        let $searchpopup = jQuery('#searchpopup')
            , self = this;

        $popup.find('#typeName, #inventoryType, #category, #subCategory, #inventory, .type, .category, .subcategory').empty();

        FwAppData.apiMethod(true, 'POST', `api/v1/${categoryType}/browse`, inventoryTypeRequest, FwServices.defaultTimeout, function onSuccess(response) {
            let inventoryTypeIndex
                , inventoryTypeIdIndex
                , types: any
                , inventoryTypeColumn;

            switch (categoryType) {
                case 'misccategory':
                    inventoryTypeIndex = response.ColumnIndex.MiscType;
                    inventoryTypeIdIndex = response.ColumnIndex.MiscTypeId;
                    break;
                case 'laborcategory':
                    inventoryTypeIndex = response.ColumnIndex.LaborType;
                    inventoryTypeIdIndex = response.ColumnIndex.LaborTypeId;
                    break;
                default:
                    inventoryTypeIndex = response.ColumnIndex.InventoryType;
                    inventoryTypeIdIndex = response.ColumnIndex.InventoryTypeId;
                    break;
            }
            //Checks for duplicate inventory types. This loops through EVERY individual item and picks out the different inventory types.
            //Could be sped up by calling into an endpoint with JUST the inventory types.
            types = [];
            inventoryTypeColumn = $popup.find('#inventoryType');
            inventoryTypeColumn.empty();
            for (let i = 0; i < response.Rows.length; i++) {
                if (types.indexOf(response.Rows[i][inventoryTypeIndex]) == -1) {
                    types.push(response.Rows[i][inventoryTypeIndex]);
                    inventoryTypeColumn.append(`<ul class="fitText" data-value="${response.Rows[i][inventoryTypeIdIndex]}"><span>${response.Rows[i][inventoryTypeIndex]}</span></ul>`);
                }
            }

            //Resizes text to fit into the div
            self.fitToParent('#inventoryType .fitText span');
        }, null, $searchpopup);
        this.typeOnClickEvents($popup, categoryType);
    }

    typeOnClickEvents($popup, categoryType) {
        const $searchpopup = jQuery('#searchpopup');
        let self = this;

        let $typeName = $popup.find('#typeName');
        let inventoryTypeValue = $popup.find('[data-datafield="InventoryType"] input:checked').val();

        switch (inventoryTypeValue) {
            case 'R':
                inventoryTypeValue = "RENTAL";
                break;
            case 'S':
                inventoryTypeValue = "SALES";
                break;
            case 'L':
                inventoryTypeValue = "LABOR";
                break;
            case 'M':
                inventoryTypeValue = "MISC";
                break;
            case 'P':
                inventoryTypeValue = "PARTS";
                break;

        }
        $typeName.append(`<ul class="inventoryTypeNav fitText" data-value="${inventoryTypeValue}"><span class="downArrowNav"><i class="material-icons">keyboard_arrow_down</i></span><span>${inventoryTypeValue}</span></ul>`);
        //Click event for main inv type nav to refresh type list
        $typeName.find('.inventoryTypeNav').on('click', e => {
            $popup.find('[data-type="radio"]').change();
        });

        $popup.off('click', '#inventoryType ul');
        $popup.on('click', '#inventoryType ul', e => {
            let inventoryTypeId
                , breadcrumb
                , typeRequest: any = {}
                , categoryBreadCrumbs;

            const $this = jQuery(e.currentTarget);
            $popup.find('#inventoryType ul').removeClass('selected');
            $this.addClass('selected');

            //Clear out existing bread crumbs and start new one
            breadcrumb = $popup.find('#breadcrumbs .type');
            categoryBreadCrumbs = $popup.find("#breadcrumbs .category, #breadcrumbs .subcategory");
            categoryBreadCrumbs.empty();
            categoryBreadCrumbs.attr('data-value', '');
            breadcrumb.text($this.find('span:first-of-type').text());
            breadcrumb.append('<div style="float:right;">&#160; &#160; &#47; &#160; &#160;</div>');
            inventoryTypeId = $this.attr('data-value');
            breadcrumb.attr('data-value', inventoryTypeId);

            //Jason H - 10/09/18 layout changes to left-side category columns
            $popup.find('#inventoryType ul').not('.selected').hide();
            if ($this.find('span').hasClass('downArrowNav') == false) {
                $this.append(`<span class="downArrowNav"><i class="material-icons">keyboard_arrow_down</i></span>`);
            }

            switch (categoryType) {
                case 'misccategory':
                    typeRequest.uniqueids = {
                        MiscTypeId: inventoryTypeId
                    }
                    break;
                case 'laborcategory':
                    typeRequest.uniqueids = {
                        LaborTypeId: inventoryTypeId
                    }
                    break;
                default:
                    typeRequest.uniqueids = {
                        InventoryTypeId: inventoryTypeId
                    }
                    break;
            }
            typeRequest.searchfieldoperators = ["<>"];
            typeRequest.searchfields = ["Inactive"];
            typeRequest.searchfieldvalues = ["T"];

            FwAppData.apiMethod(true, 'POST', `api/v1/${categoryType}/browse`, typeRequest, FwServices.defaultTimeout, function onSuccess(response) {
                let categoryIdIndex
                    , categoryIndex
                    , categories: any
                    , categoryColumn;
                categoryIdIndex = response.ColumnIndex.CategoryId;
                categoryIndex = response.ColumnIndex.Category;

                $popup.find('#category, #subCategory').empty();
                $popup.find('#inventory').empty();

                categories = [];
                categoryColumn = $popup.find('#category');
                for (let i = 0; i < response.Rows.length; i++) {
                    if (categories.indexOf(response.Rows[i][categoryIndex]) == -1) {
                        categories.push(response.Rows[i][categoryIndex]);
                        categoryColumn.append(`<ul class="fitText" data-value="${response.Rows[i][categoryIdIndex]}"><span>${response.Rows[i][categoryIndex]}</span></ul>`);
                    }
                }
                if (response.Rows.length === 1) {
                    $popup.find("#category > ul").trigger('click');
                }

                self.fitToParent('#category .fitText span');
            }, null, $searchpopup);
            this.categoryOnClickEvents($popup);
        });
    }

    categoryOnClickEvents($popup) {
        const $searchpopup = jQuery('#searchpopup');
        let self = this
            , toDate
            , fromDate;

        $popup.off('click', '#category ul');
        $popup.on('click', '#category ul', e => {
            let breadcrumb
                , categoryId
                , inventoryTypeId
                , parentFormId
                , request: any
                , hasSubCategories;

            const $this = jQuery(e.currentTarget);
            parentFormId = FwFormField.getValueByDataField($popup, 'ParentFormId');
            toDate = FwFormField.getValueByDataField($popup, 'ToDate');
            fromDate = FwFormField.getValueByDataField($popup, 'FromDate');
            $popup.find('#category ul').removeClass('selected');
            $this.addClass('selected');

            //Clear and set new breadcrumbs
            breadcrumb = $popup.find('#breadcrumbs .category');
            $popup.find("#breadcrumbs .subcategory").empty();
            $popup.find("#breadcrumbs .subcategory").attr('data-value', '');
            breadcrumb.text($this.find('span:first-of-type').text());
            breadcrumb.append('<div style="float:right;">&#160; &#160; &#47; &#160; &#160;</div>');
            categoryId = $this.attr('data-value');
            inventoryTypeId = $popup.find('#breadcrumbs .type').attr('data-value');
            breadcrumb.attr('data-value', categoryId);

            let subCatListRequest: any = {};
            subCatListRequest.uniqueids = {
                CategoryId: categoryId,
                TypeId: inventoryTypeId,
                RecType: FwFormField.getValueByDataField($popup, 'InventoryType')
            }

            FwAppData.apiMethod(true, 'POST', "api/v1/subcategory/browse", subCatListRequest, FwServices.defaultTimeout, function onSuccess(response) {
                let subCategoryIdIndex
                    , subCategoryIndex
                    , subCategories: any
                    , subCategoryColumn;

                subCategoryIdIndex = response.ColumnIndex.SubCategoryId;
                subCategoryIndex = response.ColumnIndex.SubCategory;
                $popup.find('#subCategory').empty();

                subCategories = [];
                subCategoryColumn = $popup.find('#subCategory');
                for (let i = 0; i < response.Rows.length; i++) {
                    if (subCategories.indexOf(response.Rows[i][subCategoryIndex]) == -1) {
                        subCategories.push(response.Rows[i][subCategoryIndex]);
                        subCategoryColumn.append(`<ul class="fitText" data-value="${response.Rows[i][subCategoryIdIndex]}"><span>${response.Rows[i][subCategoryIndex]}</span></ul>`);
                    }
                }
                if (response.Rows.length == 1) {
                    $popup.find("#subCategory > ul").trigger('click');
                }
                hasSubCategories = false;
                if (response.Rows.length > 0) {
                    hasSubCategories = true;
                }
                //Load the Inventory items if selected category doesn't have any sub-categories
                if (hasSubCategories == false) {
                    let hideZeroQuantity = FwFormField.getValue2($popup.find('[data-datafield="HideZeroQuantity"]'));
                    hideZeroQuantity == "T" ? hideZeroQuantity = true : hideZeroQuantity = false;
                    request = {
                        OrderId: parentFormId,
                        SessionId: parentFormId,
                        CategoryId: categoryId,
                        InventoryTypeId: inventoryTypeId,
                        AvailableFor: FwFormField.getValueByDataField($popup, 'InventoryType'),
                        WarehouseId: FwFormField.getValueByDataField($popup, 'WarehouseId'),
                        ShowAvailability: $popup.find('[data-datafield="Columns"] li[data-value="Available"]').attr('data-selected') === 'T' ? true : false,
                        SortBy: FwFormField.getValueByDataField($popup, 'SortBy'),
                        Classification: FwFormField.getValueByDataField($popup, 'Select'),
                        HideInventoryWithZeroQuantity: hideZeroQuantity,
                        ShowImages: true
                    }
                    if (fromDate != "") {
                        request.FromDate = fromDate;
                    }
                    if (toDate != "") {
                        request.ToDate = toDate;
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
                self.fitToParent('#subCategory .fitText span');
            }, null, $searchpopup);
            this.subCategoryOnClickEvents($popup);
        });
    };

    subCategoryOnClickEvents($popup) {
        const $searchpopup = jQuery('#searchpopup');

        $popup.off('click', '#subCategory ul');
        $popup.on('click', '#subCategory ul', function (e) {
            let breadcrumb
                , subCategoryId
                , categoryId
                , inventoryTypeId
                , request: any
                , parentFormId
                , toDate
                , fromDate;
            const $this = jQuery(e.currentTarget);

            $popup.find('#subCategory ul').removeClass('selected');
            $this.addClass('selected');
            parentFormId = FwFormField.getValueByDataField($popup, 'ParentFormId');
            toDate = FwFormField.getValueByDataField($popup, 'ToDate');
            fromDate = FwFormField.getValueByDataField($popup, 'FromDate');

            //Clear and set breadcrumbs
            breadcrumb = $popup.find('#breadcrumbs .subcategory');
            breadcrumb.text($this.text());
            subCategoryId = $this.attr('data-value');
            breadcrumb.attr('data-value', subCategoryId);
            categoryId = $popup.find('#breadcrumbs .category').attr('data-value');
            inventoryTypeId = $popup.find('#breadcrumbs .type').attr('data-value');

            let hideZeroQuantity = FwFormField.getValue2($popup.find('[data-datafield="HideZeroQuantity"]'));
            hideZeroQuantity == "T" ? hideZeroQuantity = true : hideZeroQuantity = false;
            request = {
                OrderId: parentFormId,
                SessionId: parentFormId,
                CategoryId: categoryId,
                SubCategoryId: subCategoryId,
                InventoryTypeId: inventoryTypeId,
                AvailableFor: FwFormField.getValueByDataField($popup, 'InventoryType'),
                WarehouseId: FwFormField.getValueByDataField($popup, 'WarehouseId'),
                ShowAvailability: $popup.find('[data-datafield="Columns"] li[data-value="Available"]').attr('data-selected') === 'T' ? true : false,
                SortBy: FwFormField.getValueByDataField($popup, 'SortBy'),
                Classification: FwFormField.getValueByDataField($popup, 'Select'),
                HideInventoryWithZeroQuantity: hideZeroQuantity,
                ShowImages: true
            }
            if (fromDate != "") {
                request.FromDate = fromDate;
            }
            if (toDate != "") {
                request.ToDate = toDate;
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

    renderInventory($popup, response) {
        let descriptionIndex      = response.ColumnIndex.Description
            , quantityAvailable   = response.ColumnIndex.QuantityAvailable
            , conflictDate        = response.ColumnIndex.ConflictDate
            , quantityIn          = response.ColumnIndex.QuantityIn
            , quantityQcRequired  = response.ColumnIndex.QuantityQcRequired
            , quantity            = response.ColumnIndex.Quantity
            , dailyRate           = response.ColumnIndex.DailyRate
            , inventoryId         = response.ColumnIndex.InventoryId
            , thumbnail           = response.ColumnIndex.Thumbnail
            , appImageId          = response.ColumnIndex.ImageId
            , classificationIndex = response.ColumnIndex.Classification
            , classificationColor = response.ColumnIndex.ClassificationColor
            , typeIndex           = response.ColumnIndex.InventoryType
            , categoryIndex       = response.ColumnIndex.Category
            , subCategoryIndex    = response.ColumnIndex.SubCategory
            , availableColor      = response.ColumnIndex.QuantityAvailableColor
            , qtyIsStaleIndex     = response.ColumnIndex.QuantityAvailableIsStale
            , $inventoryContainer
            , $cornerTriangle
            , color
            , $itemcontainer;

        $inventoryContainer = $popup.find('#inventory');
        if (response.Rows.length == 0) {
            $inventoryContainer.append('<span style="font-weight: bold; font-size=1.3em">No Results</span>');
        }

        for (let i = 0; i < response.Rows.length; i++) {
            let imageThumbnail = response.Rows[i][thumbnail]  ? response.Rows[i][thumbnail]  : './theme/images/no-image.jpg';
            let imageId        = response.Rows[i][appImageId] ? response.Rows[i][appImageId] : '';
            let rate           = Number(response.Rows[i][dailyRate]).toFixed(2);
            let conflictdate   = response.Rows[i][conflictDate] ? moment(response.Rows[i][conflictDate]).format('L') : "";

            let html = [];
            html.push(`
                <div class="item-container" data-classification=="${response.Rows[i][classificationIndex]}">
                  <div class="item-info">
                    <div class="cornerTriangle"></div>
                    <div data-control="FwFormField" data-type="key" data-datafield="InventoryId" data-caption="InventoryId" class="fwcontrol fwformfield" data-isuniqueid="true" data-enabled="false">
                      <input value="${response.Rows[i][inventoryId]}">
                    </div>
                    <div data-column="ItemImage"><img src="${imageThumbnail}" data-value="${imageId}" alt="Image" class="image"></div>
                    <div data-column="Description" data-datafield="Description" class="columnorder"><div>${response.Rows[i][descriptionIndex]}</div></div>
                    <div data-column="Type" data-datafield="Type" data-caption="Type" class="columnorder showOnSearch">${response.Rows[i][typeIndex]} </div>
                    <div data-column="Category" data-datafield="Category" data-caption="Category" class="columnorder showOnSearch">${response.Rows[i][categoryIndex]}</div>
                    <div data-column="SubCategory" data-datafield="SubCategory" data-caption="SubCategory" class="columnorder showOnSearch">${response.Rows[i][subCategoryIndex]}</div>
                    <div data-column="Available" data-datafield="QuantityAvailable" data-caption="Available" class="columnorder hideColumns"><div class="gridcaption">Available</div><div class="available-color value">${response.Rows[i][quantityAvailable]}</div></div>
                    <div data-column="ConflictDate" data-datafield="ConflictDate" class="columnorder hideColumns"><div class="gridcaption">Conflict</div><div class="value">${conflictdate}</div></div>
                    <div data-column="AllWh" data-datafield="AllWh" class="columnorder hideColumns">&#160;</div>
                    <div data-column="In" data-datafield="QuantityIn" class="columnorder hideColumns"><div class="gridcaption">In</div><div class="value">${response.Rows[i][quantityIn]}</div></div>
                    <div data-column="QC" data-datafield="QuantityQcRequired" class="columnorder hideColumns"><div class="gridcaption">QC</div><div class="value">${response.Rows[i][quantityQcRequired]}</div></div>
                    <div data-column="Rate" data-datafield="DailyRate" class="columnorder rate"><div class="gridcaption">Rate</div><div class="value">${rate}</div> </div>
                    <div data-column="Quantity" data-datafield="Quantity" class="columnorder">
                      <div class="gridcaption">Qty</div>
                      <div style="float:left; border:1px solid #bdbdbd;">
                        <button class="decrementQuantity" tabindex="-1">-</button>
                        <input type="number" style="padding: 5px 0px; float:left; width:50%; border:none; text-align:center;" value="${response.Rows[i][quantity]}">
                        <button class="incrementQuantity" tabindex="-1">+</button>
                      </div>
                    </div>
                  </div>
                </div>`);
            $itemcontainer = jQuery(html.join(''));

            $inventoryContainer.append($itemcontainer);

            if (response.Rows[i][quantity] != 0) {
                $itemcontainer.find('[data-datafield="Quantity"] input').addClass('lightBlue');
            }

            $cornerTriangle = $itemcontainer.find('.cornerTriangle');
            if (response.Rows[i][classificationColor] == "") {
                color = 'transparent';
            } else {
                color = response.Rows[i][classificationColor];
            };
            $cornerTriangle.css({
                'border-left-color': color
            });

            if (response.Rows[i][qtyIsStaleIndex] === true) {
                $itemcontainer.find('div[data-datafield="QuantityAvailable"]').attr('data-state', 'STALE');
            } else if (response.Rows[i][quantityAvailable] > 0) {
                $itemcontainer.find('div[data-datafield="QuantityAvailable"]').attr('data-state', 'AVAILABLE');
            } else if (response.Rows[i][quantityAvailable] <= 0) {
                $itemcontainer.find('div[data-datafield="QuantityAvailable"]').attr('data-state', 'NOTAVAILABLE');
            }

            if (response.Rows[i][classificationIndex] == "K" || response.Rows[i][classificationIndex] == "C") {
                $itemcontainer.find('div[data-datafield="Description"]').append('<div class="toggleaccessories">Show Accessories</div>');
                $itemcontainer.append(`<div class="item-accessories" data-classification="${response.Rows[i][classificationIndex]}" style="display:none;"></div>`);
            }
        }

        let $inventory = $popup.find('.item-info');
        let view       = $popup.find('#itemlist').attr('data-view');

        let type = $popup.find('#type').text();
        if (type === 'PurchaseOrder' || type === 'Template') {
            $popup.find('.hideColumns').css('display', 'none');
        }

        this.listGridView($inventory, view);
    }

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

        switch (viewType) {
            case 'GRID':
                $searchpopup.find('.item-info .showOnSearch').hide();
                $searchpopup.find('.accColumns').css('display', 'flex');
                break;
            case 'LIST':
                $searchpopup.find('.accColumns').hide();
                break;
            case 'HYBRID':
                $searchpopup.find('.accColumns').hide();
                break;
        }
    };

    breadCrumbs($popup) {
        $popup.on('click', '#breadcrumbs .basetype', e => {
            $popup.find('[data-type="radio"]').change();
        });

        $popup.on('click', '#breadcrumbs .type', e => {
            let inventoryTypeId = jQuery(e.currentTarget).attr('data-value');
            $popup.find(`#inventoryType ul[data-value="${inventoryTypeId}"]`).click();
        });

        $popup.on('click', '#breadcrumbs .category', e => {
            let categoryId = jQuery(e.currentTarget).attr('data-value');
            $popup.find(`#category ul[data-value="${categoryId}"]`).click();
        });
    };

    events($popup, $form, id) {
        let hasItemInGrids
            , warehouseId
            , request: any
            , self = this
            , availableFor
            , inventoryTypeRequest: any = {}
            , categoryType
            , $searchpopup
            , userId;

        const $options = $popup.find('.options');
        userId = JSON.parse(sessionStorage.getItem('userid'));
        hasItemInGrids = false;
        warehouseId = FwFormField.getValueByDataField($popup, 'WarehouseId');
        request = {};
        $searchpopup = jQuery('#searchpopup');

        //Close the Search Interface popup
        $popup.find('.close-modal').one('click', function (e) {
            FwPopup.destroyPopup($popup);
            jQuery(document).find('.fwpopup').off('click');
            jQuery(document).off('keydown');
        });

        //Toggle Options menu
        $popup.on('click', '.optiontoggle', e => {
            e.stopPropagation();
            if ($popup.find('.options').css('display') === 'none') {
                $popup.find('.options').css('display', 'flex');
            } else {
                $popup.find('.options').css('display', 'none');
            }

            //hides options when clicked outside of div
            jQuery(document).one('click', function closeMenu(e) {
                $popup.find('.options').css('display', 'none');
            });

            $options.on('click', e => {
                if (!jQuery(e.target).is('div.applyOptions, div.restoreDefaults')) {
                    e.stopPropagation();
                }
            });
        });

        //Apply options and close options menu
        $popup.on('click', '.applyOptions', e => {
            let request: any = {};
            let expandAccessories = FwFormField.getValue2($popup.find('[data-datafield="DisableAccessoryAutoExpand"]'));
            expandAccessories == "T" ? expandAccessories = true : expandAccessories = false;
            let hideZeroQuantity = FwFormField.getValue2($popup.find('[data-datafield="HideZeroQuantity"]'));
            hideZeroQuantity == "T" ? hideZeroQuantity = true : hideZeroQuantity = false;
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

            request.ResultFields = JSON.stringify(selectedColumns);
            request.WebUserId = userId.webusersid;
            request.DisableAccessoryAutoExpand = expandAccessories;
            request.HideZeroQuantity = hideZeroQuantity;
            FwAppData.apiMethod(true, 'POST', "api/v1/usersearchsettings/", request, FwServices.defaultTimeout, function onSuccess(response) {
                let columnsToHide = notSelectedColumns.map(a => a.value);
                FwFormField.setValueByDataField($popup, 'ColumnsToHide', columnsToHide.join(','));

                //perform search again with new settings
                let request: any = {
                    OrderId: id,
                    SessionId: id,
                    AvailableFor: FwFormField.getValueByDataField($popup, 'InventoryType'),
                    WarehouseId: warehouseId,
                    ShowAvailability: $popup.find('[data-datafield="Columns"] li[data-value="Available"]').attr('data-selected') === 'T' ? true : false,
                    ShowImages: true,
                    SortBy: FwFormField.getValueByDataField($popup, 'SortBy'),
                    Classification: FwFormField.getValueByDataField($popup, 'Select'),
                    HideInventoryWithZeroQuantity: hideZeroQuantity,
                    SearchText: FwFormField.getValueByDataField($popup, 'SearchBox'),
                }

                const toDate = FwFormField.getValueByDataField($popup, 'ToDate');
                if (toDate != "") request.ToDate = toDate;
                const fromDate = FwFormField.getValueByDataField($popup, 'FromDate');
                if (fromDate != "") request.FromDate = fromDate;
                const categoryId = $popup.find('#breadcrumbs .category').attr('data-value');
                if (typeof categoryId !== "undefined") request.CategoryId = categoryId;
                const inventoryTypeId = $popup.find('#breadcrumbs .type').attr('data-value');
                if (typeof inventoryTypeId !== "undefined") request.InventoryTypeId = inventoryTypeId;

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

                if (expandAccessories) {
                    $popup.find('.item-accessories').css('display', 'none');
                }
            }, null, $searchpopup);
        });

        //Reset options to defaults
        $popup.on('click', '.restoreDefaults', e => {
            e.stopPropagation();
            FwFormField.loadItems($popup.find('div[data-datafield="Columns"]'),
                [{ value: 'Description', text: 'Description', selected: 'T' },
                { value: 'Quantity', text: 'Quantity', selected: 'T' },
                { value: 'Type', text: 'Type', selected: 'F' },
                { value: 'Category', text: 'Category', selected: 'F' },
                { value: 'SubCategory', text: 'Sub Category', selected: 'F' },
                { value: 'Available', text: 'Available Quantity', selected: 'T' },
                { value: 'ConflictDate', text: 'Conflict Date', selected: 'T' },
                { value: 'AllWh', text: 'Available Quantity (All Warehouses)', selected: 'T' },
                { value: 'In', text: 'In Quantity', selected: 'T' },
                { value: 'QC', text: 'QC Required Quantity', selected: 'T' },
                { value: 'Rate', text: 'Rate', selected: 'T' }]);

            FwFormField.setValueByDataField($popup, 'DisableAccessoryAutoExpand', false);
            FwFormField.setValueByDataField($popup, 'HideZeroQuantity', false);
            let gridView = $popup.find('#itemlist').attr('data-view');
            let $inventory = $popup.find('div.item-info');
            self.listGridView($inventory, gridView);
        });

        //Inventory Type radio change events
        $popup.find('[data-type="radio"]').on('change', function () {
            availableFor = $popup.find('[data-type="radio"] input:checked').val();
            let mainTypeBreadCrumb = $popup.find('#breadcrumbs .basetype');

            switch (availableFor) {
                case 'R':
                    inventoryTypeRequest.uniqueids = {
                        Rental: true
                    }
                    categoryType = 'rentalcategory';
                    mainTypeBreadCrumb.text('RENTAL');
                    break;
                case 'S':
                    inventoryTypeRequest.uniqueids = {
                        Sales: true
                    }
                    categoryType = 'salescategory';
                    mainTypeBreadCrumb.text('SALES');
                    break;
                case 'L':
                    inventoryTypeRequest.uniqueids = {
                        Labor: true
                    }
                    categoryType = 'laborcategory';
                    mainTypeBreadCrumb.text('LABOR');
                    break;
                case 'M':
                    inventoryTypeRequest.uniqueids = {
                        Misc: true
                    }
                    categoryType = 'misccategory';
                    mainTypeBreadCrumb.text('MISC');
                    break;

                case 'P':
                    inventoryTypeRequest.uniqueids = {
                        Parts: true
                    }
                    categoryType = 'partscategory';
                    mainTypeBreadCrumb.text('PARTS');
                    break;
            }
            request.AvailableFor = availableFor;
            self.populateTypeMenu($popup, inventoryTypeRequest, categoryType);
            mainTypeBreadCrumb.append('<div style="float:right;">&#160; &#160; &#47; &#160; &#160;</div>');

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
                    let hideZeroQuantity = FwFormField.getValue2($popup.find('[data-datafield="HideZeroQuantity"]'));
                    hideZeroQuantity == "T" ? hideZeroQuantity = true : hideZeroQuantity = false;
                    let request: any = {
                        OrderId: id,
                        SessionId: id,
                        AvailableFor: FwFormField.getValueByDataField($popup, 'InventoryType'),
                        WarehouseId: warehouseId,
                        ShowAvailability: $popup.find('[data-datafield="Columns"] li[data-value="Available"]').attr('data-selected') === 'T' ? true : false,
                        ShowImages: true,
                        SortBy: FwFormField.getValueByDataField($popup, 'SortBy'),
                        Classification: FwFormField.getValueByDataField($popup, 'Select'),
                        HideInventoryWithZeroQuantity: hideZeroQuantity,
                        SearchText: FwFormField.getValueByDataField($popup, 'SearchBox')
                    }

                    let toDate = FwFormField.getValueByDataField($popup, 'ToDate')
                        , fromDate = FwFormField.getValueByDataField($popup, 'FromDate');
                    if (fromDate != "") {
                        request.FromDate = fromDate;
                    }
                    if (toDate != "") {
                        request.ToDate = toDate;
                    }

                    FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/search", request, FwServices.defaultTimeout, function onSuccess(response) {
                        $popup.find('#inventory').empty();
                        $popup.find('#breadcrumbs div:not(.basetype)').empty().attr('data-value', '');
                        $popup.find("#breadcrumbs .basetype").append('<div style="float:right;">&#160; &#160; &#47; &#160; &#160;</div>');
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
                let inventoryId = $iteminfo.find('[data-datafield="InventoryId"] input').val();
                self.refreshAccessoryQuantity($popup, id, warehouseId, inventoryId, e);
            }

            if ((jQuery('#itemlist').attr('data-view')) == 'GRID') {
                jQuery('.accColumns').show();
            }

            $accessoryContainer.slideToggle('slow');
        });

        //On Quantity input change
        $popup.on('change', '#inventory [data-datafield="Quantity"] input', e => {
            e.stopPropagation();
            let element
                , quantity
                , inventoryId
                , request: any
                , $accContainer
                , accessoryRefresh;

            element = jQuery(e.currentTarget);
            quantity = element.val();
            inventoryId = element.parents('.item-info').find('[data-datafield="InventoryId"] input').val();
            request = {
                OrderId: id,
                SessionId: id,
                InventoryId: inventoryId,
                WarehouseId: warehouseId,
                Quantity: quantity
            }

            if (quantity > 0) {
                hasItemInGrids = true;
            }

            quantity != 0 ? element.addClass('lightBlue') : element.removeClass('lightBlue');

            $accContainer = element.parents('.item-container').find('.item-accessories');
            accessoryRefresh = $popup.find('.toggleAccessories input').prop('checked');
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
            let type = $popup.find('#type').text();
            if (type === 'PurchaseOrder' || type === 'Template') {
                $popup.find('[data-type="Grid"] .hideColumns').closest('td').css('display', 'none');
            }
        });

        //Image preview confirmation box
        $popup.on('click', '.image', e => {
            e.stopPropagation();
            let $confirmation
                , $cancel
                , image
                , imageId;
            image = jQuery(e.currentTarget).attr('src');
            imageId = jQuery(e.currentTarget).attr('data-value');
            if (imageId !== '') {
                $confirmation = FwConfirmation.renderConfirmation('Image Viewer',
                    `<div style="white-space:pre;">\n<img src="${applicationConfig.appbaseurl}${applicationConfig.appvirtualdirectory}fwappimage.ashx?method=GetAppImage&appimageid=${imageId}&thumbnail=false" data-value="${imageId}" alt="No Image" class="image" style="max-width:100%;">`);
                $cancel = FwConfirmation.addButton($confirmation, 'Close');
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
                view  = $this.attr('data-buttonview'),
                viewrequest: any;

            $popup.find('#itemlist').attr('data-view', view);
            self.listGridView($popup.find('div.item-info'), view);

            viewrequest = {
                WebUserId: userId.webusersid,
                Mode:      view
            };
            FwAppData.apiMethod(true, 'POST', "api/v1/usersearchsettings/", viewrequest, FwServices.defaultTimeout, function onSuccess(response) { }, null, null);
        });

        //Accessory quantity change
        $popup.on('change', '.item-accessory-info [data-datafield="AccQuantity"] input', e => {
            const element = jQuery(e.currentTarget),
                inventoryId = element.parents('.item-accessory-info').find('[data-datafield="InventoryId"] input').val(),
                quantity = element.val(),
                parentId = element.parents('.item-container').find('.item-info [data-datafield="InventoryId"] input').val();

            let accRequest: any = {};
            accRequest = {
                SessionId: id,
                ParentId: parentId,
                InventoryId: inventoryId,
                WarehouseId: warehouseId,
                Quantity: quantity
            }

            quantity != "0" ? element.addClass('lightBlue') : element.removeClass('lightBlue');

            FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch", accRequest, FwServices.defaultTimeout, function onSuccess(response) {
                //Updates preview tab with total # of items
                $popup.find('.tab[data-caption="Preview"] .caption').text(`Preview (${response.TotalQuantityInSession})`);
            }, null, null);
        });

        //Sorting option events
        $popup.on('change', '.select, .sortby', e => {
            let hideZeroQuantity = FwFormField.getValue2($popup.find('[data-datafield="HideZeroQuantity"]'));
            hideZeroQuantity == "T" ? hideZeroQuantity = true : hideZeroQuantity = false;
            let request: any = {
                OrderId: id,
                SessionId: id,
                AvailableFor: FwFormField.getValueByDataField($popup, 'InventoryType'),
                WarehouseId: warehouseId,
                ShowAvailability: $popup.find('[data-datafield="Columns"] li[data-value="Available"]').attr('data-selected') === 'T' ? true : false,
                SortBy: FwFormField.getValueByDataField($popup, 'SortBy'),
                Classification: FwFormField.getValueByDataField($popup, 'Select'),
                HideInventoryWithZeroQuantity: hideZeroQuantity,
                ShowImages: true
            }
                , fromDate = FwFormField.getValueByDataField($popup, 'FromDate')
                , toDate = FwFormField.getValueByDataField($popup, 'ToDate')
                , inventoryTypeId = $popup.find('#breadcrumbs .type').attr('data-value')
                , categoryId = $popup.find('#breadcrumbs .category').attr('data-value')
                , subCategoryId = $popup.find('#breadcrumbs .subcategory').attr('data-value')
                , searchQuery = FwFormField.getValueByDataField($popup, 'SearchBox');

            if (fromDate != "") {
                request.FromDate = fromDate;
            }
            if (toDate != "") {
                request.ToDate = toDate;
            }
            if (inventoryTypeId != "") {
                request.InventoryTypeId = inventoryTypeId;
            }
            if (categoryId != "") {
                request.CategoryId = categoryId;
            }
            if (subCategoryId != "") {
                request.SubCategoryId = subCategoryId;
            }
            if (searchQuery != "") {
                request.SearchText = searchQuery;
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
            let hideZeroQuantity = FwFormField.getValue2($popup.find('[data-datafield="HideZeroQuantity"]'));
            hideZeroQuantity == "T" ? hideZeroQuantity = true : hideZeroQuantity = false;
            let request: any = {
                OrderId: id,
                SessionId: id,
                AvailableFor: FwFormField.getValueByDataField($popup, 'InventoryType'),
                WarehouseId: warehouseId,
                ShowAvailability: $popup.find('[data-datafield="Columns"] li[data-value="Available"]').attr('data-selected') === 'T' ? true : false,
                ShowImages: true,
                SortBy: FwFormField.getValueByDataField($popup, 'SortBy'),
                Classification: FwFormField.getValueByDataField($popup, 'Select'),
                HideInventoryWithZeroQuantity: hideZeroQuantity,
                SearchText: FwFormField.getValueByDataField($popup, 'SearchBox'),
                RefreshAvailability: true
            }

            const toDate = FwFormField.getValueByDataField($popup, 'ToDate');
            if (toDate != "") request.ToDate = toDate;
            const fromDate = FwFormField.getValueByDataField($popup, 'FromDate');
            if (fromDate != "") request.FromDate = fromDate;
            const categoryId = $popup.find('#breadcrumbs .category').attr('data-value');
            if (typeof categoryId !== "undefined") request.CategoryId = categoryId;
            const inventoryTypeId = $popup.find('#breadcrumbs .type').attr('data-value');
            if (typeof inventoryTypeId !== "undefined") request.InventoryTypeId = inventoryTypeId;
            const subCategoryId = $popup.find('#breadcrumbs .subcategory').attr('data-value');
            if (typeof subCategoryId !== "undefined") request.SubCategoryId = subCategoryId;

            FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/search", request, FwServices.defaultTimeout, function onSuccess(response) {
                $popup.find('#inventory').empty();
                $popup.find('.refresh-availability').hide();
                SearchInterfaceController.renderInventory($popup, response);
            }, null, $searchpopup);
        });

        $popup.on('click', '.acc-refresh-avail', e => {
            const inventoryId = jQuery(e.currentTarget).parents('.item-container').find('.item-info').find('[data-datafield="InventoryId"] input').val();
            $popup.data('refreshaccessories', true);
            this.refreshAccessoryQuantity($popup, id, warehouseId, inventoryId, e);
        })

        //Expand Categories button
        //$popup.on('click', '.expandcategorycolumns', e => {
        //    let $inventory = $popup.find('div.item-info');
        //    let view = $popup.find('#inventoryView').val();
        //    $popup.find('#inventory').removeClass('expandedInventoryView');
        //    $popup.find('.showWhenExpanded').hide();
        //    this.listGridView($inventory, view);
        //    $popup.find('[data-datafield="SearchBox"] input').val('');
        //    $popup.find('#breadcrumbs > div').show();
        //});

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
    };

    refreshPreviewGrid($popup, id) {
        let previewrequest: any
            , $searchpopup = jQuery('#searchpopup')
            , toDate = FwFormField.getValueByDataField($popup, 'ToDate')
            , fromDate = FwFormField.getValueByDataField($popup, 'FromDate');
        previewrequest = {
            SessionId: id,
            ShowAvailability: $popup.find('[data-datafield="Columns"] li[data-value="Available"]').attr('data-selected') === 'T' ? true : false,
            ShowImages: true
        };
        if (fromDate != "") {
            previewrequest.FromDate = fromDate;
        }
        if (toDate != "") {
            previewrequest.ToDate = toDate;
        }
        FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearchpreview/browse", previewrequest, FwServices.defaultTimeout, function onSuccess(response) {
            let $grid = $popup.find('[data-name="SearchPreviewGrid"]');
            //FwBrowse.databindcallback($grid, response);
            FwBrowse.search($grid);
        }, null, $searchpopup);
    };

    refreshAccessoryQuantity($popup, id, warehouseId, inventoryId, e) {
        let request: any = {}
            , accessoryContainer = jQuery(e.currentTarget).parents('.item-container').find('.item-accessories')
            , toDate = FwFormField.getValueByDataField($popup, 'ToDate')
            , fromDate = FwFormField.getValueByDataField($popup, 'FromDate')
            , html: any;
        request = {
            SessionId: id,
            OrderId: id,
            ParentId: inventoryId,
            WarehouseId: warehouseId,
            ShowAvailability: $popup.find('[data-datafield="Columns"] li[data-value="Available"]').attr('data-selected') === 'T' ? true : false,
            ShowImages: true
        }
        if (fromDate != "") {
            request.FromDate = fromDate;
        }
        if (toDate != "") {
            request.ToDate = toDate;
        }

        if ($popup.data('refreshaccessories') == true) {
            request.RefreshAvailability = true;
            $popup.data('refreshaccessories', false)
        }

        html = [];
        if (!(accessoryContainer.find('.accColumns').length)) {
            html.push(`
             <div class="accColumns" style="width:100%; display:none">
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
              </div>`);
            accessoryContainer.append(html.join(''));
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
            const descriptionIndex      = response.ColumnIndex.Description;
            const qtyIndex              = response.ColumnIndex.Quantity;
            const qtyInIndex            = response.ColumnIndex.QuantityIn;
            const qtyAvailIndex         = response.ColumnIndex.QuantityAvailable;
            const conflictIndex         = response.ColumnIndex.ConflictDate;
            const inventoryIdIndex      = response.ColumnIndex.InventoryId;
            const descriptionColorIndex = response.ColumnIndex.DescriptionColor;
            const quantityColorIndex    = response.ColumnIndex.QuantityColor;
            const qtyIsStaleIndex       = response.ColumnIndex.QuantityAvailableIsStale;

            for (var i = 0; i < response.Rows.length; i++) {
                let conflictdate   = response.Rows[i][conflictIndex] ? moment(response.Rows[i][conflictIndex]).format('L') : "N/A";

                let accHtml = [];
                accHtml.push(`
                <div class="item-accessory-info">
                  <div data-control="FwFormField" data-type="key" data-datafield="InventoryId" data-caption="InventoryId" class="fwcontrol fwformfield" data-isuniqueid="true" data-enabled="false" style="display:none">
                    <input value="${response.Rows[i][inventoryIdIndex]}"></input></div>
                  <div data-column="Description" class="columnorder"><span class="descriptionColor">${response.Rows[i][descriptionIndex]}</span></div>
                  <div data-column="Quantity" data-datafield="AccQuantity" class="columnorder fwcontrol fwformfield">
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
                </div>`);
                let $itemaccessoryinfo = jQuery(accHtml.join(''));

                accessoryContainer.append($itemaccessoryinfo);

                let $descriptionColor
                    , $qty
                    , $quantityColorDiv
                    , qtycolor
                    , desccolor;

                if (response.Rows[i][qtyIndex] != 0) {
                    $itemaccessoryinfo.find('[data-datafield="AccQuantity"] input').css('background-color', '#c5eefb');
                }

                $descriptionColor = $itemaccessoryinfo.find('.descriptionColor')
                if (response.Rows[i][descriptionColorIndex] == "" || response.Rows[i][descriptionColorIndex] == null) {
                    desccolor = 'transparent';
                } else {
                    desccolor = response.Rows[i][descriptionColorIndex];
                };
                $descriptionColor.css('border-left-color', desccolor);

                $qty = $itemaccessoryinfo.find('[data-datafield="AccQuantity"]');
                $qty.append('<div class="quantityColor"></div>');
                $quantityColorDiv = $qty.find('.quantityColor');

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

                let type = $popup.find('#type').text();
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
}

var SearchInterfaceController = new SearchInterface();