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
        html.push('         <div class="tabs"></div>');
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
          <div data-value="M" data-caption="Misc"></div>
          <div data-value="P" data-caption="Parts"></div>
        </div>

            `);
        let addToButton;
        switch (type) {
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
        <div class="flexcolumn hideOnExpand" style="max-width:500px;">
          <div class="flexrow">
            <div id="categorycolumns">
              <div id="inventoryType"></div>
              <div id="category"></div>
              <div id="subCategory"></div>
            </div>
          </div>
        </div>
        <div class="flexcolumn formoptions" style="max-width:1150px; overflow:hidden;">
          <div class="flexrow" style="max-width:100%;">
            <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield fwformcontrol" data-caption="Est. Start" data-datafield="FromDate" style="flex: 0 1 125px;"></div>
            <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield fwformcontrol" data-caption="Est. Stop" data-datafield="ToDate" style="flex: 0 1 125px;"></div>
            <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield fwformcontrol select" data-caption="Select" data-datafield="Select" style="flex: 0 1 150px;"></div>
            <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield fwformcontrol sortby" data-caption="Sort By" data-datafield="SortBy" style="flex: 0 1 224px;"></div>
          </div>
          <div class="flexrow" style="max-width:100%;">
            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield fwformcontrol" data-caption="Search" data-datafield="SearchBox" style="flex: 0 1 400px;"></div>
            <div data-type="button" class="invviewbtn fwformcontrol listbutton"><i class="material-icons" style="margin-top: 5px;">&#xE8EE;</i></div>
            <div data-type="button" class="invviewbtn fwformcontrol listgridbutton"><i class="material-icons" style="margin-top: 5px;">&#xE8EF;</i></div>
            <div data-type="button" class="invviewbtn fwformcontrol gridbutton"><i class="material-icons" style="margin-top: 5px;">&#xE8F0;</i></div>
            <div class="optiontoggle fwformcontrol" data-type="button">
              Options &#8675;
              <div class="options" style="display:none;">
                <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield fwformcontrol toggleAccessories" data-caption="Disable Auto-Expansion of Accessories" style="width:290px;"></div>
              </div>
            </div>
          </div>
          <div class="flexrow" style="max-width:100%;">
            <div class="flexcolumn">
              <div id="breadcrumbs" class="fwmenu default">
                <div class="type"></div>
                <div class="category"></div>
                <div class="subcategory"></div>
              </div>
              <div class="columnDescriptions hideOnExpand" style="width:95%; padding:5px; margin:5px; display:none">
                <div style="width:38%;">Description</div>
                <div style="width:10%">Qty</div>
                <div class="hideColumns" style="width:8%;">Available</div>
                <div class="hideColumns" style="width:10%;">Conflict <div>Date</div></div>
                <div class="hideColumns" style="width:8%;">All WH</div>
                <div class="hideColumns" style="width:8%;">In</div>
                <div class="hideColumns" style="width:8%;">QC</div>
                <div style="width:8%;">Rate</div>
              </div>
             <div class="columnDescriptions showWhenExpanded" style="width:95%; padding:5px; margin:5px; display:none">
                <div style="width:28%;">Description</div>
                <div style="width:6%">Qty</div>
                <div style="width:9%;">Type</div> 
                <div style="width:9%;">Category</div>
                <div style="width:9%;">Sub Category</div>
                <div class="hideColumns" style="width:8%;">Available</div>
                <div class="hideColumns" style="width:6%;">Conflict <div>Date</div></div>
                <div class="hideColumns" style="width:6%;">All WH</div>
                <div class="hideColumns" style="width:6%;">In</div>
                <div class="hideColumns" style="width:6%;">QC</div>
                <div style="width:6%;">Rate</div>
              </div>
            </div>
          </div>
          <div class="flexrow" style="max-width:100%;">
            <div id="inventory" style="overflow:auto"></div>
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

        //Default/Set values
        if (type === 'Order' || type === 'Quote') {
            let startDate = FwFormField.getValueByDataField($form, 'EstimatedStartDate')
                , stopDate = FwFormField.getValueByDataField($form, 'EstimatedStopDate');
            FwFormField.setValueByDataField($popup, 'FromDate', startDate);
            FwFormField.setValueByDataField($popup, 'ToDate', stopDate);
        } else if (type === 'PurchaseOrder') {
            let startDate = FwFormField.getValueByDataField($form, 'PurchaseOrderDate');
            FwFormField.setValueByDataField($popup, 'FromDate', startDate);
        }

        FwFormField.setValueByDataField($popup, 'ParentFormId', id);
        let warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
        FwFormField.setValueByDataField($popup, 'WarehouseId', warehouseId);

        let userId = JSON.parse(sessionStorage.getItem('userid'));
        let $inventoryView = $popup.find('#inventoryView');
        FwAppData.apiMethod(true, 'GET', `api/v1/usersearchsettings/${userId.webusersid}`, null, FwServices.defaultTimeout, function onSuccess(response) {
            if (response.SearchModePreference != "") {
                $inventoryView.val(response.SearchModePreference)
            } else {
                $inventoryView.val('GRID');
            }
        }, null, null);

        //Render preview grid
        let $previewGrid
            , $previewGridControl
            , previewrequest: any
            , toDate
            , fromDate;
        $previewGrid = $previewTabControl.find('[data-grid="SearchPreviewGrid"]');
        $previewGridControl = jQuery(jQuery('#tmpl-grids-SearchPreviewGridBrowse').html());
        $previewGrid.empty().append($previewGridControl);
        $previewGridControl.data('ondatabind', function (request) {
            request.SessionId = id;
        });
        FwBrowse.init($previewGridControl);
        FwBrowse.renderRuntimeHtml($previewGridControl);

        toDate = FwFormField.getValueByDataField($popup, 'ToDate');
        fromDate = FwFormField.getValueByDataField($popup, 'FromDate');

        previewrequest = {};
        previewrequest = {
            SessionId: id,
            ShowAvailablity: true,
            ShowImages: true
        };
        if (fromDate != "") {
            previewrequest.FromDate = fromDate;
        }
        if (toDate != "") {
            previewrequest.ToDate = toDate;
        }
        FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearchpreview/browse", previewrequest, FwServices.defaultTimeout, function onSuccess(response) {
            FwBrowse.databindcallback($previewGrid, response);
        }, null, $previewTabControl);

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

        switch (gridInventoryType) {
            default:
            case 'Rental':
                inventoryTypeRequest.uniqueids = {
                    Rental: true
                }
                categoryType = 'rentalcategory';
                inventoryType.find('[value="R"]').prop('checked', true);
                break;
            case 'Sales':
                inventoryTypeRequest.uniqueids = {
                    Sales: true
                }
                categoryType = 'salescategory';
                inventoryType.find('[value="S"]').prop('checked', true);
                break;
            case 'Labor':
                inventoryTypeRequest.uniqueids = {
                    Labor: true
                }
                categoryType = 'laborcategory';
                inventoryType.find('[value="L"]').prop('checked', true);
                break;
            case 'Misc':
                inventoryTypeRequest.uniqueids = {
                    Misc: true
                }
                categoryType = 'misccategory';
                inventoryType.find('[value="M"]').prop('checked', true);
                break;
        };

        //Hide columns based on type
        if (type === 'PurchaseOrder' || type === 'Template') {
            $popup.find('.hideColumns').css('visibility', 'hidden');
        }

        this.populateTypeMenu($popup, inventoryTypeRequest, categoryType);
        this.breadCrumbs($popup);
        this.events($popup, $form, id);
        return $popup;
    }

    populateTypeMenu($popup, inventoryTypeRequest, categoryType) {
        let $searchpopup = jQuery('#searchpopup')
            , self = this;

        $popup.find('#inventoryType, #category, #subCategory, #inventory, .type, .category, .subcategory').empty();

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
            breadcrumb.text($this.text());
            breadcrumb.append('<div style="float:right;">&#160; &#160; &#47; &#160; &#160;</div>');
            inventoryTypeId = $this.attr('data-value');
            breadcrumb.attr('data-value', inventoryTypeId);

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
            breadcrumb.text($this.text());
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
                self.fitToParent('#subCategory .fitText span');

                hasSubCategories = false;
                if (response.Rows.length > 0) {
                    hasSubCategories = true;
                }
                //Load the Inventory items if selected category doesn't have any sub-categories
                if (hasSubCategories == false) {
                    request = {
                        OrderId: parentFormId,
                        SessionId: parentFormId,
                        CategoryId: categoryId,
                        InventoryTypeId: inventoryTypeId,
                        AvailableFor: FwFormField.getValueByDataField($popup, 'InventoryType'),
                        WarehouseId: FwFormField.getValueByDataField($popup, 'WarehouseId'),
                        ShowAvailability: true,
                        SortBy: FwFormField.getValueByDataField($popup, 'SortBy'),
                        Classification: FwFormField.getValueByDataField($popup, 'Select'),
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
                        SearchInterfaceController.renderInventory($popup, response);
                    }, null, $searchpopup);
                } else {
                    $popup.find('#inventory').empty();
                }
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

            request = {
                OrderId: parentFormId,
                SessionId: parentFormId,
                CategoryId: categoryId,
                SubCategoryId: subCategoryId,
                InventoryTypeId: inventoryTypeId,
                AvailableFor: FwFormField.getValueByDataField($popup, 'InventoryType'),
                WarehouseId: FwFormField.getValueByDataField($popup, 'WarehouseId'),
                ShowAvailability: true,
                SortBy: FwFormField.getValueByDataField($popup, 'SortBy'),
                Classification: FwFormField.getValueByDataField($popup, 'Select'),
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
                    <div class="desccontainer">
                      <div class="invdescription">${response.Rows[i][descriptionIndex]}</div>
                      <div class="invimage">
                        <img src="${response.Rows[i][thumbnail]}" data-value="${response.Rows[i][appImageId]}" alt="Image" class="image">
                      </div>
                    </div>
                    <div data-control="FwFormField" data-type="number" data-datafield="Quantity" data-caption="Qty" class="fwcontrol fwformfield" style="text-align:center">
                      <span>Qty</span>
                      <div style="float:left; border:1px solid #bdbdbd;">
                        <button class="decrementQuantity" tabindex="-1">-</button>
                        <input type="number" style="padding: 5px 0px; float:left; width:50%; border:none; text-align:center;" value="${response.Rows[i][quantity]}">
                        <button class="incrementQuantity" tabindex="-1">+</button>
                      </div>
                    </div>
                    <div data-control="FwFormField" data-type="text" data-datafield="Type" data-caption="Type" class="showWhenExpanded fwcontrol fwformfield" data-enabled="false" style="display:none;text-align:center"><span>Type</span><br />${response.Rows[i][typeIndex]} </div>
                    <div data-control="FwFormField" data-type="text" data-datafield="Category" data-caption="Type" class="showWhenExpanded fwcontrol fwformfield" data-enabled="false" style="display:none;text-align:center"><span>Category</span><br />${response.Rows[i][categoryIndex]}</div>
                    <div data-control="FwFormField" data-type="text" data-datafield="SubCategory" data-caption="Type" class="showWhenExpanded fwcontrol fwformfield" data-enabled="false" style="display:none;text-align:center"><span>Sub Category</span><br />${response.Rows[i][subCategoryIndex]}</div>
                    <div data-control="FwFormField" data-type="number" data-datafield="QuantityAvailable" data-caption="Available" class="hideColumns fwcontrol fwformfield" data-enabled="false" style="text-align:center"><span>Available</span><br />${response.Rows[i][quantityAvailable]}</div>
                    <div data-control="FwFormField" data-type="text" data-caption="Conflict Date" data-datafield="ConflictDate" class="hideColumns fwcontrol fwformfield" data-enabled="false" style="text-align:center"><span>Conflict</span><br />${response.Rows[i][conflictDate] ? response.Rows[i][conflictDate] : "N/A"}</div>
                    <div data-control="FwFormField" data-type="text" data-caption="All WH" data-datafield="AllWH" class="hideColumns fwcontrol fwformfield" data-enabled="false" style="white-space:pre"><span>All WH</span><br />&#160;</div>
                    <div class="quantitycontainer">
                      <div data-control="FwFormField" data-type="number" data-datafield="QuantityIn" data-caption="In" class="hideColumns fwcontrol fwformfield" data-enabled="false" style="text-align:center"><span>In</span><br />${response.Rows[i][quantityIn]}</div>
                      <div data-control="FwFormField" data-type="number" data-datafield="QuantityQcRequired" data-caption="QC" class="hideColumns fwcontrol fwformfield" data-enabled="false" style="text-align:center"><span>QC</span><br />${response.Rows[i][quantityQcRequired]}</div>
                    </div>
             `);

            rate = Number(response.Rows[i][dailyRate]).toFixed(2);

            html.push(`
                        <div data-control="FwFormField" data-type="number" data-digits="2" data-datafield="DailyRate" data-caption="Rate" class="fwcontrol fwformfield rate" data-enabled="false" style="text-align:center"><span>Rate</span><br />${rate}</div>
                  </div>
            `);
             if (response.Rows[i][classificationIndex] == "K" || response.Rows[i][classificationIndex] == "C") {
                html.push(`<div class="accContainer" data-classification="${response.Rows[i][classificationIndex]}" style="float:left; width:95%; display:none"></div>`);
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
                'border-left-color': color,
            });
        }

        $inventory = $popup.find('div.card');
        view = $popup.find('#inventoryView').val();

        let type = $popup.find('#type').text();
        if (type === 'PurchaseOrder' || type === 'Template') {
            $popup.find('.hideColumns').css('visibility', 'hidden');
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
            accessories = $inventory.find('.accessories'),
            rate = $inventory.find('[data-datafield="DailyRate"]'),
            quantity = $inventory.find('[data-datafield="Quantity"]'),
            allWH = $inventory.find('[data-datafield="AllWH"]'),
            descContainer = $inventory.find('.desccontainer'),
            quantityContainer = $inventory.find('.quantitycontainer'),
            invType = $inventory.find('[data-datafield="Type"]'),
            category = $inventory.find('[data-datafield="Category"]'),
            subCategory = $inventory.find('[data-datafield="SubCategory"]'),
            $searchpopup = jQuery('#searchpopup'),
            expandedInventoryView = $searchpopup.find('#inventory').hasClass('expandedInventoryView');
        if (expandedInventoryView) {
            $searchpopup.find('.hideOnExpand').hide();
            $searchpopup.find('.formoptions').css({ 'max-width': '1650px' });
            $searchpopup.find('.formoptions .flexrow').css({ 'padding-left': '1em' });
            $searchpopup.find('.expandcategorycolumns').show();
            $searchpopup.find('.showWhenExpanded').show();
            $searchpopup.find('#breadcrumbs > div').hide();

        } else {
            $searchpopup.find('.hideOnExpand').show();
            $searchpopup.find('.formoptions').css({ 'max-width': '1150px' });
            $searchpopup.find('.formoptions .flexrow').css({ 'padding-left': '' });
            $searchpopup.find('.expandcategorycolumns').hide();
        }


        switch (viewType) {
            case 'GRID':
                $inventory.find('span, br').show();
                $searchpopup.find('.columnDescriptions').hide();
                $searchpopup.find('.showWhenExpanded').hide();
                allWH.hide();
                $searchpopup.find('.accColumns').show();
                $inventory.css({ 'cursor': 'pointer', 'width': '225px', 'height': '265px', 'float': 'left', 'padding': '10px', 'margin': '8px', 'position': 'relative' });
                descContainer.css({ 'width': '', 'float': '' });
                description.css({ 'height': '15%', 'width': '', 'padding-top': '', 'padding-bottom': '15px', 'float': '' });
                imageFrame.show();
                imageFrame.css({ 'float': 'left', 'width': '125px', 'height': '175px', 'line-height': '175px', 'display': 'inline-block', 'position': 'relative' });
                image.css({ 'max-height': '100%', 'max-width': '100%', 'width': 'auto', 'height': 'auto', 'position': 'absolute', 'top': '0', 'bottom': '0', 'left': '0', 'right': '0', 'margin': 'auto' });
                quantityAvailable.css({ 'float': 'right', 'width': '90px' });
                conflictDate.css({ 'float': 'right', 'width': '90px' });
                quantityIn.css({ 'float': 'left', 'width': '45px' });
                quantityQcRequired.css({ 'float': 'right', 'width': '45px' });
                accessories.css({ 'float': 'right', 'padding': '10px 5px 10px 0', 'width': '', 'font-size': '.9em', 'color': 'blue' });
                rate.css({ 'float': 'left', 'padding-top': '20px', 'width': '90px', 'position': 'absolute', 'bottom': '10px' });
                quantity.css({ 'float': 'right', 'width': '90px', 'position': 'absolute', 'bottom': '10px', 'right': '10px' });
                quantityContainer.css({ 'float': 'right', 'width': '' });
                break;
            case 'LIST':
                $searchpopup.find('.accColumns').hide();
                $inventory.find('span, br').hide();
                $inventory.css({ 'cursor': 'pointer', 'width': '95%', 'height': 'auto', 'float': 'left', 'padding': '5px', 'margin': '5px', 'position': 'relative' });
                descContainer.css({ 'width': expandedInventoryView?'28%':'38%', 'float': '' });
                description.css({ 'float': 'left', 'width': '100%', 'padding-bottom': '', 'padding-top': '.5em' });
                imageFrame.hide();
                quantityAvailable.css({ 'float': 'left', 'width': '8%', 'padding-top': '.5em'});
                conflictDate.css({ 'float': 'left', 'width': expandedInventoryView ? '6%' : '10%', 'padding-top': '.5em'});
                allWH.show();
                allWH.css({ 'float': 'left', 'width': '6%', 'padding-top': '.5em'});
                quantityContainer.css({ 'float': 'left', 'width': '12%' });
                quantityIn.css({ 'float': 'left', 'width': '50%', 'padding-top': '.5em'});
                quantityQcRequired.css({ 'float': 'left', 'width': '50%', 'padding-top': '.5em'});
                accessories.css({ 'float': 'left', 'padding': '', 'width': '8%', 'font-size': '.9em', 'color': 'blue' });
                rate.css({ 'float': 'left', 'width': expandedInventoryView ? '6%' : '8%', 'position': '', 'bottom': '', 'right': '', 'padding-top': '.5em'  });
                quantity.css({ 'float': 'left', 'width': expandedInventoryView ? '6%' : '10%', 'position': '', 'bottom': '', 'right': '', 'padding-top': '.5em'});
                invType.css({ 'float': 'left', 'width': '9%', 'min-height': '1px', 'padding-top': '.5em'});
                category.css({ 'float': 'left', 'width': '9%', 'min-height': '1px', 'padding-top': '.5em'});
                subCategory.css({ 'float': 'left', 'width': '9%', 'min-height': '1px', 'padding-top': '.5em'});
                break;
            case 'HYBRID':
                $inventory.find('span, br').hide();
                $searchpopup.find('.accColumns').hide();
                $inventory.css({ 'cursor': 'pointer', 'width': '95%', 'height': 'auto', 'float': 'left', 'padding': '5px', 'margin': '5px', 'position': 'relative' });
                descContainer.css({ 'width': expandedInventoryView ? '28%' : '38%', 'float': 'left' });
                description.css({ 'float': 'right', 'padding-top': '1em', 'width': '75%', 'padding-bottom': '' });
                imageFrame.show();
                imageFrame.css({ 'float': 'left', 'width': '25%', 'height': '4em', 'line-height': '4em', 'display': 'inline-block', 'position': 'relative' });
                image.css({ 'max-height': '100%', 'max-width': '100%', 'width': 'auto', 'height': 'auto', 'position': 'absolute', 'top': '0', 'bottom': '0', 'left': '0', 'right': '0', 'margin': 'auto' });
                quantityAvailable.css({ 'float': 'left', 'width': '8%', 'padding-top': '1em' });
                conflictDate.css({ 'float': 'left', 'width': expandedInventoryView ? '6%' : '10%', 'padding-top': '1em'});
                allWH.show();
                allWH.css({ 'float': 'left', 'width': '6%', 'padding-top': '1em' });
                quantityContainer.css({ 'float': 'left', 'width':'12%' });
                quantityIn.css({ 'float': 'left', 'width': '50%', 'padding-top': '1em' });
                quantityQcRequired.css({ 'float': 'left', 'width': '50%', 'padding-top': '1em' });
                accessories.css({ 'float': 'left', 'width': '8%', 'padding': '', 'font-size': '.9em', 'color': 'blue' });
                rate.css({ 'float': 'left', 'width': expandedInventoryView ?'6%':'8%', 'position': '', 'bottom': '', 'right': '', 'padding-top': '1em' });
                quantity.css({ 'float': 'left', 'width': expandedInventoryView ? '6%' : '10%', 'position': '', 'bottom': '', 'right': '', 'padding-top': '1em' });
                invType.css({ 'float': 'left', 'width': '9%', 'min-height': '1px', 'padding-top': '1em' });
                category.css({ 'float': 'left', 'width': '9%', 'min-height': '1px', 'padding-top': '1em'});
                subCategory.css({ 'float': 'left', 'width': '9%', 'min-height': '1px', 'padding-top': '1em' });
                break;
        }
    };

    breadCrumbs($popup) {
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
            , $searchpopup;
        hasItemInGrids = false;
        warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
        request = {};
        $searchpopup = jQuery('#searchpopup');

        //Close the Search Interface popup
        $popup.find('.close-modal').one('click', function (e) {
            FwPopup.destroyPopup($popup);
            jQuery(document).find('.fwpopup').off('click');
            jQuery(document).off('keydown');
        });
        //Toggle Options
        $popup.on('click', '.optiontoggle', e => {
            e.stopPropagation();
            $popup.find('.options').toggle();
            jQuery(document).one('click', function closeMenu(e) {
                $popup.find('.options').toggle();
            });
        });
        //Inventory Type radio change events
        $popup.find('[data-type="radio"]').on('change', function () {
            availableFor = $popup.find('[data-type="radio"] input:checked').val();
            switch (availableFor) {
                case 'R':
                    inventoryTypeRequest.uniqueids = {
                        Rental: true
                    }
                    categoryType = 'rentalcategory';
                    break;
                case 'S':
                    inventoryTypeRequest.uniqueids = {
                        Sales: true
                    }
                    categoryType = 'salescategory';
                    break;
                case 'L':
                    inventoryTypeRequest.uniqueids = {
                        Labor: true
                    }
                    categoryType = 'laborcategory';
                    break;
                case 'M':
                    inventoryTypeRequest.uniqueids = {
                        Misc: true
                    }
                    categoryType = 'misccategory';
                    break;

                case 'P':
                    inventoryTypeRequest.uniqueids = {
                        Parts: true
                    }
                    categoryType = 'partscategory';
                    break;
            }
            request.AvailableFor = availableFor;
            self.populateTypeMenu($popup, inventoryTypeRequest, categoryType);

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
                        OrderId: id,
                        SessionId: id,
                        AvailableFor: FwFormField.getValueByDataField($popup, 'InventoryType'),
                        WarehouseId: warehouseId,
                        ShowAvailability: true,
                        ShowImages: true,
                        SortBy: FwFormField.getValueByDataField($popup, 'SortBy'),
                        Classification: FwFormField.getValueByDataField($popup, 'Select'),
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
                        $popup.find('#inventory').empty().addClass('expandedInventoryView');
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
                        $card.css('box-shadow', '0 6px 10px 0 rgba(0,0,153,0.2)');

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
                    if ($form.find('.combinedtab').css('display') != 'none') {
                        FwBrowse.search($combinedGrid);
                    }

                    if ($form.find('.notcombinedtab').css('display') != 'none') {
                        FwBrowse.search($orderItemGridRental);
                        FwBrowse.search($orderItemGridMisc);
                        FwBrowse.search($orderItemGridLabor);
                        FwBrowse.search($orderItemGridSales);
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
                , viewrequest: any
                , userId;
            if ($this.hasClass('listbutton')) {
                view = "LIST";
            } else if ($this.hasClass('listgridbutton')) {
                view = "HYBRID";
            } else {
                view = "GRID";
            };
            $popup.find('#inventoryView').val(view);

            userId = JSON.parse(sessionStorage.getItem('userid'));
            viewrequest = {};
            viewrequest.UserId = userId.webusersid;
            viewrequest.SearchModePreference = view;
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
            }, null, null);
        });

        //Sorting option events
        $popup.on('change', '.select, .sortby', e => {
            let request: any = {
                OrderId: id,
                SessionId: id,
                AvailableFor: FwFormField.getValueByDataField($popup, 'InventoryType'),
                WarehouseId: warehouseId,
                ShowAvailability: true,
                SortBy: FwFormField.getValueByDataField($popup, 'SortBy'),
                Classification: FwFormField.getValueByDataField($popup, 'Select'),
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
                self.renderInventory($popup, response);
            }, null, $searchpopup);
        });

        //Disable Accessory Refresh checkbox
        $popup.on('click', '.toggleAccessories input', function () {
            let accessoryRefresh = $popup.find('.toggleAccessories input').prop('checked');
            if (accessoryRefresh) {
                $popup.find('.accContainer').css('display', 'none');
            }
        });

        //Expand Categories button
        $popup.on('click', '.expandcategorycolumns', e => {
            let $inventory = $popup.find('div.card');
            let view = $popup.find('#inventoryView').val();
            $popup.find('#inventory').removeClass('expandedInventoryView');
            $popup.find('.showWhenExpanded').hide();
            this.listGridView($inventory, view);
            $popup.find('[data-datafield="SearchBox"] input').val('');
            $popup.find('#breadcrumbs > div').show();
        });

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
            ShowAvailablity: true,
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
            FwBrowse.databindcallback($grid, response);
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
            ShowAvailability: true,
            ShowImages: true
        }
        if (fromDate != "") {
            request.FromDate = fromDate;
        }
        if (toDate != "") {
            request.ToDate = toDate;
        }

        html = [];
        if (!(accessoryContainer.find('.accColumns').length)) {
            html.push('<div class="accColumns" style="width:100%; display:none;">');
            html.push('     <div class="accList"></div>');
            html.push('     <div style="float:left; width:38%;">Description</div>');
            html.push('     <div style="float:left; width:10%;">Qty</div>');
            html.push('     <div style="float:left; width:8%;" class="hideColumns">Available</div>');
            html.push('     <div style="float:left; width:10%;" class="hideColumns">Conflict Date</div>');
            html.push('     <div style="float:left; width:8%;" class="hideColumns">All WH</div>');
            html.push('     <div style="float:left; width:8%;" class="hideColumns">In</div>');
            html.push('     <div style="float:left; width:8%;" class="hideColumns">QC</div>');
            html.push('     <div style="float:left; width:8%;">Rate</div>');
            html.push('</div>');
            accessoryContainer.append(html.join(''));
        }
        if ((jQuery('#inventoryView').val()) == 'GRID') {
            jQuery('.accColumns').show();
        }

        jQuery(e.currentTarget).parents('.cardContainer').find('.accContainer .accItem').remove();
        FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/accessories", request, FwServices.defaultTimeout, function onSuccess(response) {
            const descriptionIndex = response.ColumnIndex.Description,
                qtyIndex = response.ColumnIndex.Quantity,
                qtyInIndex = response.ColumnIndex.QuantityIn,
                qtyAvailIndex = response.ColumnIndex.QuantityAvailable,
                conflictIndex = response.ColumnIndex.ConflictDate,
                inventoryIdIndex = response.ColumnIndex.InventoryId,
                descriptionColorIndex = response.ColumnIndex.DescriptionColor,
                quantityColorIndex = response.ColumnIndex.QuantityColor;

            for (var i = 0; i < response.Rows.length; i++) {
                let accHtml = [];
                accHtml.push('<div class="accItem" style="width:100%; float:left; padding:5px 0px;">');
                accHtml.push('      <div data-control="FwFormField" data-type="key" data-datafield="InventoryId" data-caption="InventoryId" class="fwcontrol fwformfield" data-isuniqueid="true" data-enabled="false" style="display:none"><input value="' + response.Rows[i][inventoryIdIndex] + '"></input></div>');
                accHtml.push(`      <div style="float:left; width:38%; position:relative; text-indent:1em;"><div class="descriptionColor"></div>${response.Rows[i][descriptionIndex]}</div>`);
                accHtml.push('      <div data-control="FwFormField" data-type="number" data-datafield="AccQuantity" data-caption="Qty" class="fwcontrol fwformfield" style="position:relative; text-align:center; float:left; width:10%;">');
                accHtml.push('          <div style="float:left; border:1px solid #bdbdbd;">');
                accHtml.push('              <button class="decrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">-</button>');
                accHtml.push(`              <input type="number" style="padding: 5px 0px; float:left; width:50%; border:none; text-align:center;" value="${response.Rows[i][qtyIndex]}">`);
                accHtml.push('              <button class="incrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">+</button>');
                accHtml.push('          </div>');
                accHtml.push('      </div>');
                accHtml.push(`      <div class="hideColumns" style="text-align:center; float:left; width:8%;">${response.Rows[i][qtyAvailIndex]}</div>`);
                accHtml.push(`      <div class="hideColumns" data-datafield="ConflictDate" style="text-align:center; float:left; width:10%;">${response.Rows[i][conflictIndex] ? response.Rows[i][conflictIndex] : "N/A"}</div>`);
                accHtml.push('      <div style="text-align:center; float:left; width:8%; white-space:pre;">&#160;</div>');
                accHtml.push(`      <div class="hideColumns" style="text-align:center; float:left; width:8%;">${response.Rows[i][qtyInIndex]}</div>`);
                accHtml.push('      <div style="text-align:center; float:left; width:8%; white-space:pre;">&#160;</div>');
                accHtml.push('      <div style="text-align:center; float:left; width:8%; white-space:pre;">&#160;</div>');
                accHtml.push('</div>');

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
                if (response.Rows[i][descriptionColorIndex] == "") {
                    desccolor = 'transparent';
                } else {
                    desccolor = response.Rows[i][descriptionColorIndex];
                };

                $descriptionColor.css('border-left-color', desccolor);

                $qty = $acc.find('[data-datafield="AccQuantity"]');
                $qty.append('<div class="quantityColor"></div>');
                $quantityColorDiv = $qty.find('.quantityColor');

                if (response.Rows[i][quantityColorIndex] == "") {
                    qtycolor = 'transparent';
                } else {
                    qtycolor = response.Rows[i][quantityColorIndex];
                };

                $quantityColorDiv.css('border-left-color', qtycolor);

                let type = $popup.find('#type').text();
                if (type === 'PurchaseOrder' || type === 'Template') {
                    $popup.find('.hideColumns').css('visibility', 'hidden');
                }
            }
        }, null, null);
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