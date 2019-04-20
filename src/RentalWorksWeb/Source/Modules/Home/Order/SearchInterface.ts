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
    <div id="inventoryView" style="display:none"></div>
    <div id="type" style="display:none">${type}</div>
    <div class="fwmenu default">
    </div>
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
            <div data-type="button" class="invviewbtn fwformcontrol listbutton"><i class="material-icons" style="margin-top: 5px;">&#xE8EE;</i></div>
            <div data-type="button" class="invviewbtn fwformcontrol listgridbutton"><i class="material-icons" style="margin-top: 5px;">&#xE8EF;</i></div>
            <div data-type="button" class="invviewbtn fwformcontrol gridbutton"><i class="material-icons" style="margin-top: 5px;">&#xE8F0;</i></div>
            <div class="optiontoggle fwformcontrol" data-type="button">
              Options &#8675;
              <div class="options" style="display:none;">
                <div class="flexcolumn">
                    <div data-datafield="Columns" data-control="FwFormField" data-type="checkboxlist" class="fwcontrol fwformfield columnOrder" data-caption="Select columns to display in Results" data-sortable="true" data-orderby="true" style="max-height:400px; margin-top: 10px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield fwformcontrol toggleAccessories" data-caption="Disable Auto-Expansion of Complete/Kit Accessories" data-datafield="DisableAccessoryAutoExpand"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield fwformcontrol show-zero-quantity" data-caption="Show Inventory with Zero Quantity" data-datafield="ShowZeroQuantity"></div>
                    <div>
                       <div data-type="button" class="fwformcontrol restoreDefaults" style="width:45px; float:left; margin:10px;">Reset</div>
                       <div data-type="button" class="fwformcontrol applyOptions" style="width:45px; float:right; margin:10px;">Apply</div>
                    </div>
                </div>
              </div>
            </div>
            <div data-type="button" class="fwformcontrol refresh-availability" style="max-width:165px; float:left; margin:12px 0px 0px 10px; display:none; background-color:rgb(49, 0, 209);">Refresh Availability</div>
          </div>
          <div class="flexrow" style="max-width:100%;">
            <div class="flexcolumn">
              <div id="breadcrumbs" class="fwmenu default">
                <div class="basetype"></div>
                <div class="type"></div>
                <div class="category"></div>
                <div class="subcategory"></div>
              </div>
              <div class="columnDescriptions" style="width:98%; padding:5px; margin:5px; display:none">
                <div class="columnorder" data-column="Description" style="flex: 1 0 250px">Description</div>
                <div class="columnorder" data-column="Quantity" style="flex: 0 0 90px">Qty</div>
                <div class="columnorder showOnSearch" data-column="Type" style="flex: 1 0 100px">Type</div> 
                <div class="columnorder showOnSearch" data-column="Category" style="flex: 1 0 100px">Category</div>
                <div class="columnorder showOnSearch" data-column="SubCategory" style="flex: 1 0 100px">Sub Category</div>
                <div class="columnorder hideColumns" data-column="Available" style="flex: 1 0 80px">Available</div>
                <div class="columnorder hideColumns" data-column="ConflictDate" style="flex: 1 0 100px">Conflict <div>Date</div></div>
                <div class="columnorder hideColumns" data-column="AllWh" style="flex: 1 0 70px">All Wh</div>
                <div class="columnorder hideColumns" data-column="In" style="flex: 1 0 50px">In</div>
                <div class="columnorder hideColumns" data-column="QC" style="flex: 1 0 50px">QC</div>
                <div class="columnorder" data-column="Rate" style="flex: 1 0 100px">Rate</div>
              </div>
            </div>
          </div>
          <div class="flexrow" style="max-width:100%; max-height:49vh; overflow:scroll; overflow-x:hidden;">
            <div id="inventory"></div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>        `);
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
        previewhtml = [];
        previewhtml.push('<div id="previewHtml" class="fwform fwcontrol">');
        previewhtml.push('      <div class="fwmenu default" style="width:100%;height:7%; padding-left: 20px;"></div>');
        previewhtml.push('      <div class="formrow" style="width:100%; position:absolute;">');
        previewhtml.push('          <div>');
        previewhtml.push('              <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        previewhtml.push('                  <div data-control="FwGrid" data-grid="SearchPreviewGrid" data-securitycaption="Preview"></div>');
        previewhtml.push('              </div>');
        switch (type) {
            case 'Transfer':
                previewhtml.push('      <div data-type="button" class="fwformcontrol addToOrder" style="width:135px; float:right; margin:15px;">Add to Transfer</div>');
                break;
            case 'Order':
                previewhtml.push('      <div data-type="button" class="fwformcontrol addToOrder" style="width:120px; float:right; margin:15px;">Add to Order</div>');
                break;
            case 'Quote':
                previewhtml.push('      <div data-type="button" class="fwformcontrol addToOrder" style="width:120px; float:right; margin:15px;">Add to Quote</div>');
                break;
            case 'PurchaseOrder':
                previewhtml.push('      <div data-type="button" class="fwformcontrol addToOrder" style="width:195px; float:right; margin-right:6px;">Add to Purchase Order</div>');
                break;
            case 'Template':
                previewhtml.push('      <div data-type="button" class="fwformcontrol addToOrder" style="width:140px; float:right; margin-right:6px;">Add to Template</div>');
                break;
        };
        previewhtml.push('          </div>');
        previewhtml.push('     </div>');
        previewhtml.push('</div>');

        $previewform = previewhtml.join('');
        newtabids2 = FwTabs.addTab($moduleTabControl, 'Preview', false, 'FORM', false);
        $moduleTabControl.find('#' + newtabids2.tabpageid).append(jQuery($previewform));
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
        let $inventoryView = $popup.find('#inventoryView');
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

            if (response.ShowZeroQuantity) {
                FwFormField.setValueByDataField($popup, 'ShowZeroQuantity', true);
            }

            if (response.Mode != null) {
                $inventoryView.val(response.Mode)
            } else {
                $inventoryView.val('GRID');
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
                    let showZeroQuantity = FwFormField.getValue2($popup.find('[data-datafield="ShowZeroQuantity"]'));
                    showZeroQuantity == "T" ? showZeroQuantity = true : showZeroQuantity = false;
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
                        ShowInventoryWithZeroQuantity: showZeroQuantity,
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

            let showZeroQuantity = FwFormField.getValue2($popup.find('[data-datafield="ShowZeroQuantity"]'));
            showZeroQuantity == "T" ? showZeroQuantity = true : showZeroQuantity = false;
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
                ShowInventoryWithZeroQuantity: showZeroQuantity,
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
        let descriptionIndex = response.ColumnIndex.Description
            , quantityAvailable = response.ColumnIndex.QuantityAvailable
            , conflictDate = response.ColumnIndex.ConflictDate
            , quantityIn = response.ColumnIndex.QuantityIn
            , quantityQcRequired = response.ColumnIndex.QuantityQcRequired
            , quantity = response.ColumnIndex.Quantity
            , dailyRate = response.ColumnIndex.DailyRate
            , inventoryId = response.ColumnIndex.InventoryId
            , thumbnail = response.ColumnIndex.Thumbnail
            , appImageId = response.ColumnIndex.ImageId
            , classificationIndex = response.ColumnIndex.Classification
            , classificationColor = response.ColumnIndex.ClassificationColor
            , typeIndex = response.ColumnIndex.InventoryType
            , categoryIndex = response.ColumnIndex.Category
            , subCategoryIndex = response.ColumnIndex.SubCategory
            , availableColor = response.ColumnIndex.QuantityAvailableColor
            , $inventoryContainer
            , $cornerTriangle
            , color
            , html: any
            , $card
            , rate
            , view
            , $inventory;

        $inventoryContainer = $popup.find('#inventory');
        if (response.Rows.length == 0) {
            $inventoryContainer.append('<span style="font-weight: bold; font-size=1.3em">No Results</span>');
        }

        for (let i = 0; i < response.Rows.length; i++) {
            html = [];
            html.push(`
                <div class="cardContainer">
                  <div class="card">
                    <div class="cornerTriangle"></div>
                    <div data-control="FwFormField" data-type="key" data-datafield="InventoryId" data-caption="InventoryId" class="fwcontrol fwformfield" data-isuniqueid="true" data-enabled="false">
                      <input value="${response.Rows[i][inventoryId]}">
                    </div>
                    <div class="desccontainer columnorder" data-column="Description">
                    <div class="invdescription">${response.Rows[i][descriptionIndex]}</div>`
            );
            if (response.Rows[i][thumbnail]) {
                html.push(`<div class="invimage"><img src="${response.Rows[i][thumbnail]}" data-value="${response.Rows[i][appImageId]}" alt="Image" class="image"></div>`);
            } else {
                const noImageSrc = './theme/images/no-image.jpg';
                html.push(`<div class="invimage"><img src="${noImageSrc}" data-value="" alt="Image" class="image"></div>`);
            }
            html.push(`</div>
                    <div data-control="FwFormField" data-type="number" data-column="Quantity" data-datafield="Quantity" data-caption="Qty" class="columnorder fwcontrol fwformfield" style="text-align:center">
                      <span>Qty</span>
                      <div style="float:left; border:1px solid #bdbdbd;">
                        <button class="decrementQuantity" tabindex="-1">-</button>
                        <input type="number" style="padding: 5px 0px; float:left; width:50%; border:none; text-align:center;" value="${response.Rows[i][quantity]}">
                        <button class="incrementQuantity" tabindex="-1">+</button>
                      </div>
                    </div>
                    <div data-control="FwFormField" data-column="Type" data-type="text" data-datafield="Type" data-caption="Type" class="columnorder showOnSearch fwcontrol fwformfield" data-enabled="false" style="display:none;text-align:center"><span>Type</span><br />${response.Rows[i][typeIndex]} </div>
                    <div data-control="FwFormField" data-column="Category" data-type="text" data-datafield="Category" data-caption="Category" class="columnorder showOnSearch fwcontrol fwformfield" data-enabled="false" style="display:none;text-align:center"><span>Category</span><br />${response.Rows[i][categoryIndex]}</div>
                    <div data-control="FwFormField" data-column="SubCategory" data-type="text" data-datafield="SubCategory" data-caption="SubCategory" class="columnorder showOnSearch fwcontrol fwformfield" data-enabled="false" style="display:none;text-align:center"><span>Sub Category</span><br />${response.Rows[i][subCategoryIndex]}</div>
                    <div data-control="FwFormField" data-column="Available" data-type="number" data-datafield="QuantityAvailable" data-caption="Available" class="columnorder hideColumns fwcontrol fwformfield" data-enabled="false" style="text-align:center"><span>Available</span><br /><span class="available-color">${response.Rows[i][quantityAvailable]}</span></div>
                    <div data-control="FwFormField" data-column="ConflictDate" data-type="date" data-caption="Conflict Date" data-datafield="ConflictDate" class="columnorder hideColumns fwcontrol fwformfield" data-enabled="false" style="text-align:center"><span>Conflict</span><br />${response.Rows[i][conflictDate] ? moment(response.Rows[i][conflictDate]).format('L') : ""}</div>
                    <div data-control="FwFormField" data-column="AllWh" data-type="text" data-caption="All Wh" data-datafield="AllWh" class="columnorder hideColumns fwcontrol fwformfield" data-enabled="false" style="white-space:pre"><span>All Wh</span><br />&#160;</div>
                      <div data-control="FwFormField" data-type="number" data-column="In" data-datafield="QuantityIn" data-caption="In" class="columnorder hideColumns fwcontrol fwformfield" data-enabled="false" style="text-align:center"><span>In</span><br />${response.Rows[i][quantityIn]}</div>
                      <div data-control="FwFormField" data-type="number" data-column="QC" data-datafield="QuantityQcRequired" data-caption="QC" class="columnorder hideColumns fwcontrol fwformfield" data-enabled="false" style="text-align:center"><span>QC</span><br />${response.Rows[i][quantityQcRequired]}</div>
             `);
            rate = Number(response.Rows[i][dailyRate]).toFixed(2);

            html.push(`
                        <div data-control="FwFormField" data-type="number" data-column="Rate" data-digits="2" data-datafield="DailyRate" data-caption="Rate" class="columnorder fwcontrol fwformfield rate" data-enabled="false" style="text-align:center"><span>Rate</span><br />${rate}</div>
                  </div>
            `);
            if (response.Rows[i][classificationIndex] == "K" || response.Rows[i][classificationIndex] == "C") {
                html.push(`<div class="accContainer" data-classification="${response.Rows[i][classificationIndex]}" style="float:left; width:98%; display:none"></div>`);
            }
            html.push(`</div>`);

            $inventoryContainer.append(html.join(''));
            $card = $popup.find('#inventory > div:last');

            if (response.Rows[i][quantity] != 0) {
                $card.find('[data-datafield="Quantity"] input').addClass('lightBlue');
            }

            $cornerTriangle = $card.find('.cornerTriangle');
            if (response.Rows[i][classificationColor] == "") {
                color = 'transparent';
            } else {
                color = response.Rows[i][classificationColor];
            };
            $cornerTriangle.css({
                'border-left-color': color
            });

            const $availableColor = $card.find('.available-color');
            if ((response.Rows[i][availableColor] == null) || (response.Rows[i][availableColor] == "")) {
                color = 'transparent';
            } else {
                color = response.Rows[i][availableColor];
            };
            $availableColor.css({
                'border-left-color': color
            });
        }

        $inventory = $popup.find('div.card');
        view = $popup.find('#inventoryView').val();

        let type = $popup.find('#type').text();
        if (type === 'PurchaseOrder' || type === 'Template') {
            $popup.find('.hideColumns').css('display', 'none');
        }

        this.listGridView($inventory, view);
    }

    listGridView($inventory, viewType) {
        let description = $inventory.find('.invdescription'),
            imageFrame = $inventory.find('.invimage'),
            image = $inventory.find('.image'),
            quantityAvailable = $inventory.find('[data-datafield="QuantityAvailable"]'),
            conflictDate = $inventory.find('[data-datafield="ConflictDate"]'),
            quantityIn = $inventory.find('[data-datafield="QuantityIn"]'),
            quantityQcRequired = $inventory.find('[data-datafield="QuantityQcRequired"]'),
            rate = $inventory.find('[data-datafield="DailyRate"]'),
            quantity = $inventory.find('[data-datafield="Quantity"]'),
            allWh = $inventory.find('[data-datafield="AllWh"]'),
            descContainer = $inventory.find('.desccontainer'),
            invType = $inventory.find('[data-datafield="Type"]'),
            category = $inventory.find('[data-datafield="Category"]'),
            subCategory = $inventory.find('[data-datafield="SubCategory"]'),
            $searchpopup = jQuery('#searchpopup'),
            $columnDescriptions = $searchpopup.find('.columnDescriptions'),
            $availablecolor = $inventory.find('.available-color');

        if (viewType !== 'GRID') {
            $searchpopup.find('.card').css('display', 'flex');

            //custom display/sequencing for columns
            let columnsToHide = FwFormField.getValueByDataField($searchpopup, 'ColumnsToHide').split(',');
            $searchpopup.find('.columnDescriptions .columnorder, .card .columnorder').css('display', '');
            for (let i = 0; i < columnsToHide.length; i++) {
                $searchpopup.find(`.columnDescriptions [data-column="${columnsToHide[i]}"], .card [data-column="${columnsToHide[i]}"]`).hide();
            };

            let columnOrder = FwFormField.getValueByDataField($searchpopup, 'ColumnOrder').split(',');
            for (let i = 0; i < columnOrder.length; i++) {
                $searchpopup.find(`.columnDescriptions [data-column="${columnOrder[i]}"], .card [data-column="${columnOrder[i]}"]`).css('order', i);
            };

            if ($inventory.length > 0) {
                $columnDescriptions.show();
            }

        } else {
            $searchpopup.find('.columnDescription .columnorder, .card').css('display', 'block');
            $columnDescriptions.hide();
        }

        switch (viewType) {
            case 'GRID':
                $inventory.find('span, br').show();
                $columnDescriptions.hide();
                $searchpopup.find('.card .showOnSearch').hide();
                $searchpopup.find('.accColumns').css('display', 'flex');
                allWh.hide();
                $inventory.css({ 'cursor': 'pointer', 'width': '225px', 'height': '265px', 'float': 'left', 'padding': '10px', 'margin': '8px', 'position': 'relative' });
                //description
                descContainer.css({ 'flex': '', 'float': '' });
                description.css({ 'height': '15%', 'width': '', 'padding-top': '', 'padding-bottom': '15px', 'float': '', 'text-indent': '' });
                imageFrame.show();
                imageFrame.css({ 'float': 'left', 'width': '125px', 'height': '175px', 'line-height': '175px', 'display': 'inline-block', 'position': 'relative' });
                image.css({ 'max-height': '100%', 'max-width': '100%', 'width': 'auto', 'height': 'auto', 'position': 'absolute', 'top': '0', 'bottom': '0', 'left': '0', 'right': '0', 'margin': 'auto' });
                //quantity
                quantity.css({ 'float': 'right', 'width': '90px', 'position': 'absolute', 'bottom': '10px', 'right': '10px', 'padding-bottom': '' });
                quantityAvailable.css({ 'float': 'right', 'width': '90px' });
                $availablecolor.css({ 'left': '15px', 'top': '40px' });
                conflictDate.css({ 'float': 'right', 'width': '90px' });
                quantityIn.css({ 'float': 'left', 'width': '45px', 'padding-top': '' });
                quantityQcRequired.css({ 'float': 'right', 'width': '45px', 'padding-top': '' });
                rate.css({ 'float': 'left', 'padding-top': '20px', 'width': '90px', 'position': 'absolute', 'bottom': '10px' });

                break;
            case 'LIST':
                $inventory.length > 0 ? $columnDescriptions.css('display', 'flex') : $columnDescriptions.css('display', 'none');
                $searchpopup.find('.accColumns').hide();
                $inventory.find('span:not(.available-color), br').hide();
                $inventory.css({ 'cursor': 'pointer', 'width': '98%', 'height': 'auto', 'float': 'left', 'padding': '5px', 'margin': '5px', 'position': 'relative' });
                //description columns
                descContainer.css({ 'flex': '1 0 250px', 'float': '' });
                description.css({ 'float': 'left', 'width': '100%', 'padding-bottom': '', 'padding-top': '.5em', 'text-indent': '' });
                imageFrame.hide();
                //quantity
                quantity.css({ 'float': 'left', 'flex': '0 0 90px', 'position': '', 'bottom': '', 'right': '', 'padding-top': '0.4em', 'padding-bottom': '0.4em' });
                //type cat sub
                invType.css({ 'float': 'left', 'flex': '1 0 100px', 'min-height': '1px', 'padding-top': '.5em' });
                category.css({ 'float': 'left', 'flex': '1 0 100px', 'min-height': '1px', 'padding-top': '.5em' });
                subCategory.css({ 'float': 'left', 'flex': '1 0 100px', 'min-height': '1px', 'padding-top': '.5em' });
                //available
                $availablecolor.css({ 'left': '', 'top': '' });
                quantityAvailable.css({ 'float': 'left', 'flex': '1 0 80px', 'padding-top': '.5em' });
                //conflict date
                conflictDate.css({ 'float': 'left', 'flex': '1 0 100px', 'padding-top': '.5em' });
                //all wh
                allWh.css({ 'float': 'left', 'flex': '1 0 70px', 'padding-top': '.5em' });
                //quantity in
                quantityIn.css({ 'float': 'left', 'padding-top': '.5em', 'flex': '1 0 50px' });
                //quantity qc req
                quantityQcRequired.css({ 'float': 'left', 'padding-top': '.5em', 'flex': '1 0 50px' });
                //rate 
                rate.css({ 'float': 'left', 'flex': '1 0 100px', 'position': '', 'bottom': '', 'right': '', 'padding-top': '.5em' });
                break;
            case 'HYBRID':
                $inventory.length > 0 ? $columnDescriptions.css('display', 'flex') : $columnDescriptions.css('display', 'none');
                $inventory.find('span:not(.available-color), br').hide();
                $searchpopup.find('.accColumns').hide();
                $inventory.css({ 'cursor': 'pointer', 'width': '98%', 'height': 'auto', 'float': 'left', 'padding': '5px', 'margin': '5px', 'position': 'relative' });
                //description columns
                descContainer.css({ 'flex': '1 0 250px', 'float': '' });
                description.css({ 'float': 'right', 'padding-top': '1em', 'width': '75%', 'padding-bottom': '', 'text-indent': '' });
                imageFrame.show();
                imageFrame.css({ 'float': 'left', 'width': '25%', 'height': '4em', 'line-height': '4em', 'display': 'inline-block', 'position': 'relative' });
                image.css({ 'max-height': '100%', 'max-width': '100%', 'width': 'auto', 'height': 'auto', 'position': 'absolute', 'top': '0', 'bottom': '0', 'left': '0', 'right': '0', 'margin': 'auto' });
                //quantity
                quantity.css({ 'float': 'left', 'flex': '0 0 90px', 'position': '', 'bottom': '', 'right': '', 'padding-top': '1em' });
                //type cat sub
                invType.css({ 'float': 'left', 'flex': '1 0 100px', 'min-height': '1px', 'padding-top': '.5em' });
                category.css({ 'float': 'left', 'flex': '1 0 100px', 'min-height': '1px', 'padding-top': '.5em' });
                subCategory.css({ 'float': 'left', 'flex': '1 0 100px', 'min-height': '1px', 'padding-top': '.5em' });
                //available
                $availablecolor.css({ 'left': '', 'top': '' });
                quantityAvailable.css({ 'float': 'left', 'flex': '1 0 80px', 'padding-top': '1em' });
                //conflict date
                conflictDate.css({ 'float': 'left', 'flex': '1 0 100px', 'padding-top': '1em' });
                //all wh
                allWh.css({ 'float': 'left', 'flex': '1 0 70px', 'padding-top': '1em' });
                //quantity in
                quantityIn.css({ 'float': 'left', 'padding-top': '.5em', 'flex': '1 0 50px' });
                //quantity qc req
                quantityQcRequired.css({ 'float': 'left', 'padding-top': '.5em', 'flex': '1 0 50px' });
                //rate 
                rate.css({ 'float': 'left', 'flex': '1 0 100px', 'position': '', 'bottom': '', 'right': '', 'padding-top': '1em' });
                break;
        }

        $searchpopup.find('.card:even').css('background-color', '#F9F9F9');

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
            let showZeroQuantity = FwFormField.getValue2($popup.find('[data-datafield="ShowZeroQuantity"]'));
            showZeroQuantity == "T" ? showZeroQuantity = true : showZeroQuantity = false;
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
            request.ShowZeroQuantity = showZeroQuantity;
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
                    ShowInventoryWithZeroQuantity: showZeroQuantity,
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
                    $popup.find('.accContainer').css('display', 'none');
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
            FwFormField.setValueByDataField($popup, 'ShowZeroQuantity', false);
            let gridView = $popup.find('#inventoryView').val();
            let $inventory = $popup.find('div.card');
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
                    let showZeroQuantity = FwFormField.getValue2($popup.find('[data-datafield="ShowZeroQuantity"]'));
                    showZeroQuantity == "T" ? showZeroQuantity = true : showZeroQuantity = false;
                    let request: any = {
                        OrderId: id,
                        SessionId: id,
                        AvailableFor: FwFormField.getValueByDataField($popup, 'InventoryType'),
                        WarehouseId: warehouseId,
                        ShowAvailability: $popup.find('[data-datafield="Columns"] li[data-value="Available"]').attr('data-selected') === 'T' ? true : false,
                        ShowImages: true,
                        SortBy: FwFormField.getValueByDataField($popup, 'SortBy'),
                        Classification: FwFormField.getValueByDataField($popup, 'Select'),
                        ShowInventoryWithZeroQuantity: showZeroQuantity,
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

        //Expand Accessories section on single click, increment quantity on double click
        let clickCount = 0
            , timeout = 500;
        $popup.on('click', '#inventory > .cardContainer > .card', e => {
            e.stopPropagation();
            let $card = jQuery(e.currentTarget);
            clickCount++;
            if (clickCount == 1) {
                setTimeout(function () {
                    if (clickCount == 1) {
                        $popup.find('#inventory > .cardContainer > .card').removeClass('selected');
                        $card.addClass('selected');

                        let accessoryContainer = $card.siblings('.accContainer');
                        if (accessoryContainer.length > 0) {
                            if (!(accessoryContainer.find('.accList').length)) {
                                let inventoryId = $card.find('[data-datafield="InventoryId"] input').val();
                                self.refreshAccessoryQuantity($popup, id, warehouseId, inventoryId, e);
                            }
                            if ((jQuery('#inventoryView').val()) == 'GRID') {
                                jQuery('.accColumns').show();
                            }
                            $popup.find('.accContainer').not(accessoryContainer).hide();
                            accessoryContainer.slideToggle();
                        }
                    } else {
                        $card.find('.incrementQuantity').click();
                    }
                    clickCount = 0;
                }, timeout || 300);
            }
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
            inventoryId = element.parents('.card').find('[data-datafield="InventoryId"] input').val();
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

            $accContainer = element.parents('.cardContainer').find('.accContainer');
            accessoryRefresh = $popup.find('.toggleAccessories input').prop('checked');
            FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/", request, FwServices.defaultTimeout, function onSuccess(response) {
                if (accessoryRefresh == false) {
                    if ($accContainer.css('display') == 'none') {
                        $popup.find('.accContainer').not($accContainer).hide();
                        $accContainer.slideToggle();
                    }
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
            $confirmation = FwConfirmation.renderConfirmation('Image Viewer',
                `<div style="white-space:pre;">\n<img src="${applicationConfig.appbaseurl}${applicationConfig.appvirtualdirectory}fwappimage.ashx?method=GetAppImage&appimageid=${imageId}&thumbnail=false" data-value="${imageId}" alt="No Image" class="image" style="max-width:100%;">`);
            $cancel = FwConfirmation.addButton($confirmation, 'Close');
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

        //Change inventory view button events
        $popup.on('click', '.listbutton', e => {
            let $inventory = $popup.find('div.card');
            self.listGridView($inventory, 'LIST');
        });

        $popup.on('click', '.listgridbutton', e => {
            let $inventory = $popup.find('div.card');
            self.listGridView($inventory, 'HYBRID');
        });

        $popup.on('click', '.gridbutton', e => {
            let $inventory = $popup.find('div.card');
            self.listGridView($inventory, 'GRID');
        });

        //Saves the user's inventory view setting
        $popup.on('click', '.listbutton, .listgridbutton, .gridbutton', e => {
            let view = $popup.find('#inventoryView').val()
                , $this = jQuery(e.currentTarget)
                , viewrequest: any;
            if ($this.hasClass('listbutton')) {
                view = "LIST";
            } else if ($this.hasClass('listgridbutton')) {
                view = "HYBRID";
            } else {
                view = "GRID";
            };
            $popup.find('#inventoryView').val(view);

            viewrequest = {};
            viewrequest.WebUserId = userId.webusersid;
            viewrequest.Mode = view;
            FwAppData.apiMethod(true, 'POST', "api/v1/usersearchsettings/", viewrequest, FwServices.defaultTimeout, function onSuccess(response) {
            }, null, null);
        });

        //Accessory quantity change
        $popup.on('change', '.accItem [data-datafield="AccQuantity"] input', e => {
            const element = jQuery(e.currentTarget),
                inventoryId = element.parents('.accItem').find('[data-datafield="InventoryId"] input').val(),
                quantity = element.val(),
                parentId = element.parents('.cardContainer').find('.card [data-datafield="InventoryId"] input').val();

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
            let showZeroQuantity = FwFormField.getValue2($popup.find('[data-datafield="ShowZeroQuantity"]'));
            showZeroQuantity == "T" ? showZeroQuantity = true : showZeroQuantity = false;
            let request: any = {
                OrderId: id,
                SessionId: id,
                AvailableFor: FwFormField.getValueByDataField($popup, 'InventoryType'),
                WarehouseId: warehouseId,
                ShowAvailability: $popup.find('[data-datafield="Columns"] li[data-value="Available"]').attr('data-selected') === 'T' ? true : false,
                SortBy: FwFormField.getValueByDataField($popup, 'SortBy'),
                Classification: FwFormField.getValueByDataField($popup, 'Select'),
                ShowInventoryWithZeroQuantity: showZeroQuantity,
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
            let showZeroQuantity = FwFormField.getValue2($popup.find('[data-datafield="ShowZeroQuantity"]'));
            showZeroQuantity == "T" ? showZeroQuantity = true : showZeroQuantity = false;
            let request: any = {
                OrderId: id,
                SessionId: id,
                AvailableFor: FwFormField.getValueByDataField($popup, 'InventoryType'),
                WarehouseId: warehouseId,
                ShowAvailability: $popup.find('[data-datafield="Columns"] li[data-value="Available"]').attr('data-selected') === 'T' ? true : false,
                ShowImages: true,
                SortBy: FwFormField.getValueByDataField($popup, 'SortBy'),
                Classification: FwFormField.getValueByDataField($popup, 'Select'),
                ShowInventoryWithZeroQuantity: showZeroQuantity,
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
            const inventoryId = jQuery(e.currentTarget).parents('.cardContainer').find('.card').find('[data-datafield="InventoryId"] input').val();
            $popup.data('refreshaccessories', true);
            this.refreshAccessoryQuantity($popup, id, warehouseId, inventoryId, e);
        })

        //Expand Categories button
        //$popup.on('click', '.expandcategorycolumns', e => {
        //    let $inventory = $popup.find('div.card');
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
            , accessoryContainer = jQuery(e.currentTarget).parents('.cardContainer').find('.accContainer')
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
                <div class="columnorder" data-column="Description" style="float:left; flex: 1 0 250px">Description</div>
                <div class="columnorder" data-column="Quantity" style="float:left; flex: 0 0 90px">Qty</div>
                <div class="columnorder showOnSearch" data-column="Type" style="float:left; flex: 1 0 100px"></div> 
                <div class="columnorder showOnSearch" data-column="Category" style="float:left; flex: 1 0 100px"></div>
                <div class="columnorder showOnSearch" data-column="SubCategory" style="float:left; flex: 1 0 100px"></div>
                <div class="columnorder hideColumns" data-column="Available" style="float:left; flex: 1 0 80px">Available</div>
                <div class="columnorder hideColumns" data-column="ConflictDate" style="float:left; flex: 1 0 100px">Conflict <div>Date</div></div>
                <div class="columnorder hideColumns" data-column="AllWh" style="float:left; flex: 1 0 70px"></div>
                <div class="columnorder hideColumns" data-column="In" style="float:left; flex: 1 0 50px">In</div>
                <div class="columnorder hideColumns" data-column="QC" style="float:left; flex: 1 0 50px"></div>
                <div class="columnorder" data-column="Rate" style="float:left; flex: 1 0 100px"></div>
              </div>
                    `);
            accessoryContainer.append(html.join(''));
        }
        // Show column names for grid view
        if (($popup.find('#inventoryView').val()) == 'GRID') {
            $popup.find('.accColumns').css('display', 'flex');
            $popup.find('.accColumns .showOnSearch').show();
        } else {
            $popup.find('.accColumns').css('display', 'none');
        }

        jQuery(e.currentTarget).parents('.cardContainer').find('.accContainer .accItem').remove();
        FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/accessories", request, FwServices.defaultTimeout, function onSuccess(response) {
            const descriptionIndex = response.ColumnIndex.Description;
            const qtyIndex = response.ColumnIndex.Quantity;
            const qtyInIndex = response.ColumnIndex.QuantityIn;
            const qtyAvailIndex = response.ColumnIndex.QuantityAvailable;
            const conflictIndex = response.ColumnIndex.ConflictDate;
            const inventoryIdIndex = response.ColumnIndex.InventoryId;
            const descriptionColorIndex = response.ColumnIndex.DescriptionColor;
            const quantityColorIndex = response.ColumnIndex.QuantityColor;
            const accQuantityColorIndex = response.ColumnIndex.QuantityAvailableColor;
            const qtyIsStaleIndex = response.ColumnIndex.QuantityAvailableIsStale;

            for (var i = 0; i < response.Rows.length; i++) {
                let accHtml = [];
                accHtml.push(`
                <div class="accItem" style="width:100%; float:left; padding:5px 0px;">
                     <div data-control="FwFormField" data-type="key" data-datafield="InventoryId" data-caption="InventoryId" class="fwcontrol fwformfield" data-isuniqueid="true" data-enabled="false" style="display:none">
                        <input value="${response.Rows[i][inventoryIdIndex]}"></input></div>
                     <div data-column="Description" class="columnorder" style="float:left; flex: 1 0 250px; position:relative;"><span class="descriptionColor">${response.Rows[i][descriptionIndex]}</span></div>
                     <div data-control="FwFormField" data-column="Quantity" data-type="number" data-datafield="AccQuantity" data-caption="Qty" class="columnorder fwcontrol fwformfield" style="position:relative; text-align:center; float:left; flex: 0 0 90px;">
                         <div style="float:left; border:1px solid #bdbdbd;">
                             <button class="decrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">-</button>
                             <input type="number" style="padding: 5px 0px; float:left; width:50%; border:none; text-align:center;" value="${response.Rows[i][qtyIndex]}">
                             <button class="incrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">+</button>
                         </div>
                     </div>
                     <div class="columnorder hideColumns" data-column="Available" style="text-align:center; float:left; flex: 1 0 80px;"><span class="acc-avail-color">${response.Rows[i][qtyAvailIndex]}</span></div>
                     <div class="columnorder hideColumns" data-column="ConflictDate" data-datafield="ConflictDate" style="text-align:center; float:left; flex:1 0 100px ">${response.Rows[i][conflictIndex] ? response.Rows[i][conflictIndex] : "N/A"}</div>
                     <div class="hideColumns columnorder" data-column="In" style="text-align:center; float:left; flex:1 0 50px;">${response.Rows[i][qtyInIndex]}</div>
                     <div class="columnorder" data-column="Type" style="text-align:center; float:left; flex: 1 0 100px; min-height: 1px"></div>
                     <div class="columnorder" data-column="Category" style="text-align:center; float:left; flex: 1 0 100px; min-height: 1px"></div>
                     <div class="columnorder" data-column="SubCategory" style="text-align:center; float:left; flex: 1 0 100px; min-height: 1px"></div>
                     <div class="columnorder" data-column="Rate" style="text-align:center; float:left; flex: 1 0 100px; min-height: 1px"></div>
                     <div class="columnorder" data-column="QC" style="text-align:center; float:left; flex: 1 0 50px; min-height: 1px"></div>
                     <div class="columnorder" data-column="AllWh" style="text-align:center; float:left; flex: 1 0 70px; min-height: 1px"></div>
                </div>
                `);

                accessoryContainer.append(accHtml.join(''));
                let $acc
                    , $descriptionColor
                    , $qty
                    , $quantityColorDiv
                    , qtycolor
                    , desccolor;

                $acc = accessoryContainer.find('.accItem:last');
                $popup.find('.accItem .fwformfield-caption').hide();

                if (response.Rows[i][qtyIndex] != 0) {
                    $acc.find('[data-datafield="AccQuantity"] input').css('background-color', '#c5eefb');
                }

                $descriptionColor = $acc.find('.descriptionColor')
                if (response.Rows[i][descriptionColorIndex] == "" || response.Rows[i][descriptionColorIndex] == null) {
                    desccolor = 'transparent';
                } else {
                    desccolor = response.Rows[i][descriptionColorIndex];
                };
                $descriptionColor.css('border-left-color', desccolor);

                $qty = $acc.find('[data-datafield="AccQuantity"]');
                $qty.append('<div class="quantityColor"></div>');
                $quantityColorDiv = $qty.find('.quantityColor');

                if (response.Rows[i][quantityColorIndex] == "" || response.Rows[i][quantityColorIndex] == null) {
                    qtycolor = 'transparent';
                } else {
                    qtycolor = response.Rows[i][quantityColorIndex];
                };
                $quantityColorDiv.css('border-left-color', qtycolor);

                const $accColor = $acc.find('.acc-avail-color');
                let accQtyColor;
                if (response.Rows[i][accQuantityColorIndex] == "" || response.Rows[i][accQuantityColorIndex] == null) {
                    accQtyColor = 'transparent';
                } else {
                    accQtyColor = response.Rows[i][accQuantityColorIndex];
                };
                $accColor.css('border-left-color', accQtyColor);

                let type = $popup.find('#type').text();
                if (type === 'PurchaseOrder' || type === 'Template') {
                    $popup.find('.hideColumns').css('display', 'none');
                }

                $popup.find('.accItem').css('display', 'flex');

                //custom display/sequencing for columns
                let columnsToHide = FwFormField.getValueByDataField($popup, 'ColumnsToHide').split(',');
                $popup.find('.accContainer .columnorder').css('display', '');
                for (let i = 0; i < columnsToHide.length; i++) {
                    $popup.find(`.accContainer [data-column="${columnsToHide[i]}"]`).hide();
                };

                let columnOrder = FwFormField.getValueByDataField($popup, 'ColumnOrder').split(',');
                for (let i = 0; i < columnOrder.length; i++) {
                    $popup.find(`.accContainer [data-column="${columnOrder[i]}"]`).css('order', i);
                };

                $popup.find('.accItem:even').css('background-color', '#F9F9F9');
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