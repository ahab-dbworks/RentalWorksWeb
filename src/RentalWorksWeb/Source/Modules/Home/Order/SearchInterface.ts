class SearchInterface {
    renderSearchPopup($form, id, type, gridInventoryType?) {
        console.log('type', type)
        var self = this;

        var html = [];
        html.push('<div id="searchpopup" style="background-color: white; box-shadow: 0 25px 44px rgba(0, 0, 0, 0.30), 0 20px 15px rgba(0, 0, 0, 0.22); width: 85vw; height: 85vh; overflow:scroll; position:relative;">');

        html.push(' <div id="searchTabs" class="fwcontrol fwtabs" data-rendermode="runtime" data-version="1" data-control="FwTabs">');
        html.push(' <div class="tabs"></div>');
        html.push(' <div class="tabpages"></div>');
        html.push('</div>');

        html.push('     <div class="close-modal" style="display:flex; position:absolute; top:10px; right:15px; cursor:pointer;"><i class="material-icons">clear</i><div class="btn-text">Close</div></div>');
        html.push('</div>');
        var $popupHtml = html.join('');
        var $popup = FwPopup.renderPopup(jQuery($popupHtml), { ismodal: true });
        FwPopup.showPopup($popup);

        var searchhtml = [];
        searchhtml.push('<div id="searchFormHtml" class="fwform fwcontrol">');
        searchhtml.push('   <div id="inventoryView" style="display:none"></div>');
        searchhtml.push('     <div id="breadcrumbs" class="fwmenu default" style="width:100%;height:2em; padding-left: 20px;">');
        searchhtml.push('         <div class="type" style="float:left; cursor: pointer; font-weight: bold;"></div>');
        searchhtml.push('         <div class="category" style="float:left; cursor: pointer; font-weight: bold;"></div>');
        searchhtml.push('         <div class="subcategory" style="float:left; cursor: pointer; font-weight: bold;"></div>');
        searchhtml.push('     </div>');

        searchhtml.push('     <div class="formrow" style="width:100%; position:absolute;">');
        searchhtml.push('              <div data-control="FwFormField" class="fwcontrol fwformfield fwformcontrol" data-caption=" "  data-datafield="InventoryType" data-type="radio" style="width:30%; margin: 5px 0px 25px 35px; float:clear;">');
        searchhtml.push('                  <div data-value="R" data-caption="Rental" style="float:left; width:20%;"></div>');
        searchhtml.push('                  <div data-value="S" data-caption="Sales" style="float:left; width:20%;"></div>');
        searchhtml.push('                  <div data-value="L" data-caption="Labor" style="float:left; width:20%;"></div>');
        searchhtml.push('                  <div data-value="M" data-caption="Misc" style="float:left; width:20%;"></div>');
        searchhtml.push('                  <div data-value="P" data-caption="Parts" style="float:left; width:20%;"></div>');
        searchhtml.push('              </div>');

        searchhtml.push('              <div id="inventoryType" style="width:10%; margin: 5px 0px 0px 5px; float:left;">');
        searchhtml.push('              </div>');
        searchhtml.push('             <div id="category" style="width:10%; margin: 5px 0px 0px 5px; float:left;">');
        searchhtml.push('             </div>');
        searchhtml.push('             <div id="subCategory" style="width:10%; margin: 5px 0px 0px 5px; float:left;">');
        searchhtml.push('             </div>');

        searchhtml.push('            <div style="width:65%; position:absolute; left: 35%; right: 5%;">')
        searchhtml.push('                 <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        searchhtml.push('                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield fwformcontrol" data-caption="Est. Start" data-datafield="FromDate" style="width:120px; float:left;"></div>');
        searchhtml.push('                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield fwformcontrol" data-caption="Est. Stop" data-datafield="ToDate" style="width:120px;float:left;"></div>');
        searchhtml.push('                      <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield fwformcontrol select" data-caption="Select" data-datafield="" style="width:150px;float:left;"></div>');
        searchhtml.push('                      <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield fwformcontrol sortby" data-caption="Sort By" data-datafield="" style="width:180px;float:left;"></div>');
        if (type == 'Order') {
            searchhtml.push('                      <div data-type="button" class="fwformcontrol addToOrder" style="width:120px; float:left; margin:15px;">Add to Order</div>');
        } if (type == 'Quote') {
            searchhtml.push('                      <div data-type="button" class="fwformcontrol addToOrder" style="width:120px; float:left; margin:15px;">Add to Quote</div>');
        } if (type == 'PurchaseOrder') {
            searchhtml.push('                      <div data-type="button" class="fwformcontrol addToOrder" style="width:195px; float:left; margin:15px;">Add to Purchase Order</div>');
        }
        searchhtml.push('                  </div>');
        searchhtml.push('                 <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        searchhtml.push('                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Search" data-datafield="SearchBox" style="width:570px; float:left;"></div>');
        searchhtml.push('                      <div data-type="button" class="fwformcontrol listbutton" style="margin: 12px 6px 12px 22px; padding:0px 7px 0px 7px;"><i class="material-icons" style="margin-top: 5px;">&#xE8EE;</i></div>');
        searchhtml.push('                      <div data-type="button" class="fwformcontrol listgridbutton" style="margin: 12px 6px 12px 6px; padding:0px 7px 0px 7px;"><i class="material-icons" style="margin-top: 5px;">&#xE8EF;</i></div>');
        searchhtml.push('                      <div data-type="button" class="fwformcontrol gridbutton" style="margin: 12px 6px 12px 6px; padding:0px 7px 0px 7px;"><i class="material-icons" style="margin-top: 5px;">&#xE8F0;</i></div>');
        searchhtml.push('                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield fwformcontrol toggleAccessories" data-caption="Disable Accessory Refresh" style="width:200px;"></div>');
        searchhtml.push('                         <div id="columnDescriptions" style="width:95%; padding:5px; margin:5px; display:none">');
        searchhtml.push('                           <div style="float:left; width:38%; text-align:center; font-weight:bold;">Description</div>');
        searchhtml.push('                           <div style="float:left; width:10%; text-align:center; font-weight:bold;">Qty</div>');
        searchhtml.push('                           <div style="float:left; width:8%; text-align:center; font-weight:bold;">Available</div>');
        searchhtml.push('                           <div style="float:left; width:10%; text-align:center; font-weight:bold;">Conflict Date</div>');
        searchhtml.push('                           <div style="float:left; width:8%; text-align:center; font-weight:bold;">All WH</div>');
        searchhtml.push('                           <div style="float:left; width:8%; text-align:center; font-weight:bold;">In</div>');
        searchhtml.push('                           <div style="float:left; width:8%; text-align:center; font-weight:bold;">QC</div>');
        searchhtml.push('                           <div style="float:left; width:8%; text-align:center; font-weight:bold;">Rate</div>');
        searchhtml.push('                            </div>');
        searchhtml.push('                 </div>');
        searchhtml.push('                 <div id="inventory" style="overflow:auto">');

        searchhtml.push('                 </div>');
        searchhtml.push('            </div>');

        searchhtml.push('  </div>');
        searchhtml.push('</div>');
        var $searchform = searchhtml.join('');

        var formid = program.uniqueId(8);

        var $moduleTabControl = jQuery('#searchTabs');
        var newtabids = FwTabs.addTab($moduleTabControl, 'Search', false, 'FORM', true);
        $moduleTabControl.find('#' + newtabids.tabpageid).append(jQuery($searchform));
        var $fwcontrols = jQuery($searchform).find('.fwcontrol');
        var $searchTabControl = jQuery('#searchFormHtml');
        FwConfirmation.addControls($searchTabControl, $searchform);

        var $select = $popup.find('.select');
        var $sortby = $popup.find('.sortby');

        FwFormField.loadItems($select, [
            { value: '', text: 'All' },
            { value: 'CK', text: 'Complete/Kit' },
            { value: 'N', text: 'Container' },
            { value: 'I', text: 'Item' },
            { value: 'A', text: 'Accessory' }], true);

        FwFormField.loadItems($sortby, [
            { value: 'ICODE', text: 'I-Code' },
            { value: 'DESCRIPTION', text: 'Description' },
            { value: 'PARTNO', text: 'Part No.' },
            { value: 'INVENTORY', text: 'Inventory Management' }], true);

        var previewhtml = [];
        previewhtml.push('<div id="previewHtml" class="fwform fwcontrol">');
        previewhtml.push('         <div class="fwmenu default" style="width:100%;height:7%; padding-left: 20px;">');
        previewhtml.push('         </div>');
        previewhtml.push('     <div class="formrow" style="width:100%; position:absolute;">');
        previewhtml.push('            <div>');
        previewhtml.push('                 <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        previewhtml.push('                      <div data-control="FwGrid" data-grid="SearchPreviewGrid" data-securitycaption="Preview"></div>');
        previewhtml.push('                </div>');
        if (type == 'Order') {
            previewhtml.push('                      <div data-type="button" class="fwformcontrol addToOrder" style="width:120px; float:right; margin:15px;">Add to Order</div>');
        } if (type == 'Quote') {
            previewhtml.push('                      <div data-type="button" class="fwformcontrol addToOrder" style="width:120px; float:right; margin:15px;">Add to Quote</div>');
        } if (type == 'PurchaseOrder') {
            previewhtml.push('                      <div data-type="button" class="fwformcontrol addToOrder" style="width:195px; float:right; margin-right:6px;">Add to Purchase Order</div>');
        }
        previewhtml.push('            </div>');
        previewhtml.push('     </div>');
        previewhtml.push('</div>');

        var $previewform = previewhtml.join('');
        var newtabids2 = FwTabs.addTab($moduleTabControl, 'Preview', false, 'FORM', false);
        $moduleTabControl.find('#' + newtabids2.tabpageid).append(jQuery($previewform));
        FwTabs.init($moduleTabControl);
        var $previewTabControl = jQuery('#previewHtml');
        FwConfirmation.addControls($previewTabControl, $previewform);

        if (type === 'Order' || type === 'Quote') {
            var startDate = FwFormField.getValueByDataField($form, 'EstimatedStartDate');
            var stopDate = FwFormField.getValueByDataField($form, 'EstimatedStopDate');
            FwFormField.setValueByDataField($popup, 'FromDate', startDate);
            FwFormField.setValueByDataField($popup, 'ToDate', stopDate);
        } else if (type === 'Purchase Order') {
            var startDate = FwFormField.getValueByDataField($form, 'PurchaseOrderDate');
            FwFormField.setValueByDataField($popup, 'FromDate', startDate);
        }

        var toDate = FwFormField.getValueByDataField($popup, 'ToDate');
        var fromDate = FwFormField.getValueByDataField($popup, 'FromDate');
        var warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');

        var $previewGrid;
        var $previewGridControl;
        $previewGrid = $previewTabControl.find('[data-grid="SearchPreviewGrid"]');
        $previewGridControl = jQuery(jQuery('#tmpl-grids-SearchPreviewGridBrowse').html());
        $previewGrid.empty().append($previewGridControl);
        $previewGridControl.data('ondatabind', function (request) {
            request.SessionId = id;
        });

        FwBrowse.init($previewGridControl);
        FwBrowse.renderRuntimeHtml($previewGridControl);
        var $grid = $previewTabControl.find('[data-name="SearchPreviewGrid"]');

        var previewrequest: any = {};
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
            FwBrowse.databindcallback($grid, response);
        }, null, $previewTabControl);

        $popup.find('.close-modal').one('click', function (e) {
            FwPopup.destroyPopup($popup);
            jQuery(document).find('.fwpopup').off('click');
            jQuery(document).off('keydown');
        });

        var inventoryTypeRequest: any = {};
        inventoryTypeRequest.uniqueids = {
            Rental: true
        }
        inventoryTypeRequest.searchfieldoperators = ["<>"];
        inventoryTypeRequest.searchfields = ["Inactive"];
        inventoryTypeRequest.searchfieldvalues = ["T"];
        var categoryType = 'rentalcategory',
            availableFor = FwFormField.getValueByDataField($popup, 'InventoryType');

        if (gridInventoryType != "") {
            let inventoryType = $popup.find('[data-datafield="InventoryType"]');
            switch (gridInventoryType) {
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
            }
        }

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
            self.populateTypeMenu($popup, inventoryTypeRequest, categoryType, request);
        });

        var $searchpopup = jQuery('#searchpopup');
        var $descriptionField = $popup.find('[data-datafield="SearchBox"] input.fwformfield-value');
        $descriptionField.on('keydown', function (e) {
            var code = e.keyCode || e.which;
            try {
                if (code === 13) { //Enter Key
                    e.preventDefault();

                    var request: any = {
                        OrderId: id,
                        SessionId: id,
                        AvailableFor: availableFor,
                        WarehouseId: warehouseId,
                        ShowAvailability: true,
                        ShowImages: true,
                        SortBy: $popup.find('.sortby select').val(),
                        Classification: $popup.find('.select select').val(),
                        SearchText: $popup.find('[data-datafield="SearchBox"] input.fwformfield-value').val()
                    }
                    if (fromDate != "") {
                        request.FromDate = fromDate;
                    }
                    if (toDate != "") {
                        request.ToDate = toDate;
                    }

                    FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/search", request, FwServices.defaultTimeout, function onSuccess(response) {
                        $popup.find('#inventory').empty();
                        SearchInterfaceController.renderInventory($popup, response, false);
                    }, null, $searchpopup);

                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        var request: any = {
            OrderId: id,
            SessionId: id,
            AvailableFor: availableFor,
            WarehouseId: warehouseId,
            ShowAvailability: true,
            SortBy: $popup.find('.sortby select').val(),
            Classification: $popup.find('.select select').val(),
            ShowImages: true
        }
        if (fromDate != "") {
            request.FromDate = fromDate;
        }
        if (toDate != "") {
            request.ToDate = toDate;
        }

        var userId = JSON.parse(sessionStorage.getItem('userid'));
        var $inventoryView = $popup.find('#inventoryView');
        FwAppData.apiMethod(true, 'GET', "api/v1/usersearchsettings/" + userId.webusersid, null, FwServices.defaultTimeout, function onSuccess(res) {
            if (res.SearchModePreference != "") {
                $inventoryView.val(res.SearchModePreference)
            } else {
                $inventoryView.val('GRID');
            }
        }, null, null);

        this.populateTypeMenu($popup, inventoryTypeRequest, categoryType, request);
        this.breadCrumbs($popup, $form, request);
        this.events($popup, $form, id);
        return $popup;
    }

    populateTypeMenu($popup, inventoryTypeRequest, categoryType, request) {
        const $searchpopup = jQuery('#searchpopup');
        var self = this;
        $popup.find('#inventoryType, #category, #subCategory, #inventory, .type, .category, .subcategory').empty();
        FwAppData.apiMethod(true, 'POST', "api/v1/" + categoryType + "/browse", inventoryTypeRequest, FwServices.defaultTimeout, function onSuccess(response) {
            var inventoryTypeIndex, inventoryTypeIdIndex;

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

            var types = [];
            var inventoryTypeColumn = $popup.find('#inventoryType');
            inventoryTypeColumn.empty();
            for (let i = 0; i < response.Rows.length; i++) {
                if (types.indexOf(response.Rows[i][inventoryTypeIndex]) == -1) {
                    types.push(response.Rows[i][inventoryTypeIndex]);
                    inventoryTypeColumn.append('<ul class="fitText" style="cursor:pointer; padding:5px 5px 5px 10px; margin:1px;" data-value="' + response.Rows[i][inventoryTypeIdIndex] + '"><span>' + response.Rows[i][inventoryTypeIndex] + '</span></ul>');
                }
            }
            self.fitToParent('#inventoryType .fitText span');
        }, null, $searchpopup);
        self.typeOnClickEvents($popup, request, categoryType);
    }

    typeOnClickEvents($popup, request, categoryType) {
        const $searchpopup = jQuery('#searchpopup');
        var self = this;
        $popup.off('click', '#inventoryType ul');
        $popup.on('click', '#inventoryType ul', function (e) {
            var invType, inventoryTypeId, breadcrumb, typeRequest: any = {};
            $popup.find('#inventoryType ul').css({
                'background-color': '',
                'color': 'black',
                'box-shadow': '0 0px 0px 0 rgba(0, 0, 0, 0.2)'
            });

            const $this = jQuery(e.currentTarget);
            invType = $this.text();
            $popup.find('#inventoryType ul').removeClass('selected');
            $this.addClass('selected');
            breadcrumb = $popup.find('#breadcrumbs .type');

            var categoryBreadCrumbs = $popup.find("#breadcrumbs .category, #breadcrumbs .subcategory");
            categoryBreadCrumbs.empty();
            categoryBreadCrumbs.attr('data-value', '');
            breadcrumb.text(invType);
            breadcrumb.append('<div style="float:right;">&#160; &#160; &#47; &#160; &#160;</div>');

            $this.css({ 'background-color': '#bdbdbd', 'color': 'white', 'box-shadow': '0 3px 7px 0 rgba(0, 0, 0, 0.2)' });
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

            FwAppData.apiMethod(true, 'POST', "api/v1/" + categoryType + "/browse", typeRequest, FwServices.defaultTimeout, function onSuccess(response) {
                var categoryIdIndex = response.ColumnIndex.CategoryId;
                var categoryIndex = response.ColumnIndex.Category;
                $popup.find('#category, #subCategory').empty();
                $popup.find('#inventory').empty();

                var categories = [];
                let categoryColumn = $popup.find('#category');
                for (var i = 0; i < response.Rows.length; i++) {
                    if (categories.indexOf(response.Rows[i][categoryIndex]) == -1) {
                        categories.push(response.Rows[i][categoryIndex]);
                        categoryColumn.append('<ul class="fitText" style="cursor:pointer; padding:5px 5px 5px 10px; margin:1px;" data-value="' + response.Rows[i][categoryIdIndex] + '"><span>' + response.Rows[i][categoryIndex] + '</span></ul>');
                    }
                }

                if (response.Rows.length == 1) {
                    $popup.find("#category > ul").trigger('click');
                }

                self.fitToParent('#category .fitText span');
            }, null, $searchpopup);
            self.categoryOnClickEvents($popup, request);
        });
    }

    categoryOnClickEvents($popup, request) {
        var $searchpopup = jQuery('#searchpopup');
        var self = this;
        $popup.off('click', '#category ul');
        $popup.on('click', '#category ul', function (e) {
            var category, breadcrumb, categoryId, inventoryTypeId;
            $popup.find('#category ul').css({
                'background-color': '',
                'color': 'black',
                'box-shadow': '0 0px 0px 0 rgba(0, 0, 0, 0.2)'
            });

            const $this = jQuery(e.currentTarget);
            category = $this.text();
            $popup.find('#category ul').removeClass('selected');
            $this.addClass('selected');
            breadcrumb = $popup.find('#breadcrumbs .category');
            $popup.find("#breadcrumbs .subcategory").empty();
            $popup.find("#breadcrumbs .subcategory").attr('data-value', '');
            breadcrumb.text(category);
            breadcrumb.append('<div style="float:right;">&#160; &#160; &#47; &#160; &#160;</div>');
            $this.css({ 'background-color': '#bdbdbd', 'color': 'white', 'box-shadow': '0 3px 7px 0 rgba(0, 0, 0, 0.2)' });
            categoryId = $this.attr('data-value');
            inventoryTypeId = $popup.find('#breadcrumbs .type').attr('data-value');
            breadcrumb.attr('data-value', categoryId);

            var subCatListRequest: any = {};
            subCatListRequest.uniqueids = {
                CategoryId: categoryId,
                TypeId: inventoryTypeId,
                RecType: $popup.find('[data-datafield="InventoryType"] input:checked').val()
            }

            //load sub-categories list
            delete request.SubCategoryId;
            request.CategoryId = categoryId;
            request.InventoryTypeId = inventoryTypeId;

            FwAppData.apiMethod(true, 'POST', "api/v1/subcategory/browse", subCatListRequest, FwServices.defaultTimeout, function onSuccess(response) {
                var subCategoryIdIndex = response.ColumnIndex.SubCategoryId;
                var subCategoryIndex = response.ColumnIndex.SubCategory;
                $popup.find('#subCategory').empty();

                let subCategories = [];
                let subCategoryColumn = $popup.find('#subCategory');
                for (var i = 0; i < response.Rows.length; i++) {
                    if (subCategories.indexOf(response.Rows[i][subCategoryIndex]) == -1) {
                        subCategories.push(response.Rows[i][subCategoryIndex]);
                        subCategoryColumn.append('<ul class="fitText" style="cursor:pointer; padding:5px 5px 5px 10px; margin:1px;" data-value="' + response.Rows[i][subCategoryIdIndex] + '"><span>' + response.Rows[i][subCategoryIndex] + '</span></ul>');
                    }
                }

                if (response.Rows.length == 1) {
                    $popup.find("#subCategory > ul").trigger('click');
                }

                self.fitToParent('#subCategory .fitText span');
                let hasSubCategories = false;
                if (response.Rows.length > 0) {
                    hasSubCategories = true;
                }

                if (hasSubCategories == false) {
                    //load categories inventory
                    request.SortBy = $popup.find('.sortby select').val();
                    request.Classification = $popup.find('.select select').val();
                    request.AvailableFor = $popup.find('[data-datafield="InventoryType"] input:checked').val()
                    FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/search", request, FwServices.defaultTimeout, function onSuccess(response) {
                        $popup.find('#inventory').empty();
                        SearchInterfaceController.renderInventory($popup, response, false);
                    }, null, $searchpopup);

                } else {
                    $popup.find('#inventory').empty();
                }
            }, null, $searchpopup);
            self.subCategoryOnClickEvents($popup, request);
        });
    };

    subCategoryOnClickEvents($popup, request) {
        var $searchpopup = jQuery('#searchpopup');

        $popup.off('click', '#subCategory ul');
        $popup.on('click', '#subCategory ul', function (e) {
            var subCategory, breadcrumb, subCategoryId, categoryId, inventoryTypeId;
            $popup.find('#subCategory ul').css({
                'background-color': '',
                'color': 'black',
                'box-shadow': '0 0px 0px 0 rgba(0, 0, 0, 0.2)'
            });
            const $this = jQuery(e.currentTarget);
            subCategory = $this.text();
            $popup.find('#subCategory ul').removeClass('selected');
            $this.addClass('selected');
            breadcrumb = $popup.find('#breadcrumbs .subcategory');
            breadcrumb.text(subCategory);
            subCategoryId = $this.attr('data-value');
            breadcrumb.attr('data-value', subCategoryId);
            $this.css({ 'background-color': '#bdbdbd', 'color': 'white', 'box-shadow': '0 3px 7px 0 rgba(0, 0, 0, 0.2)' });

            categoryId = $popup.find('#breadcrumbs .category').attr('data-value');
            inventoryTypeId = $popup.find('#breadcrumbs .type').attr('data-value');

            request.SubCategoryId = subCategoryId;
            request.CategoryId = categoryId;
            request.InventoryTypeId = inventoryTypeId;
            request.SortBy = $popup.find('.sortby select').val();
            request.Classification = $popup.find('.select select').val();

            FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/search", request, FwServices.defaultTimeout, function onSuccess(response) {
                $popup.find('#inventory').empty();
                SearchInterfaceController.renderInventory($popup, response, true);
            }, null, $searchpopup);
        });
    }

    renderInventory($popup, response, isSubCategory) {
        var self = this,
            descriptionIndex = response.ColumnIndex.Description,
            thumbnailIndex = response.ColumnIndex.Thumbnail,
            quantityAvailable = response.ColumnIndex.QuantityAvailable,
            conflictDate = response.ColumnIndex.ConflictDate,
            quantityIn = response.ColumnIndex.QuantityIn,
            quantityQcRequired = response.ColumnIndex.QuantityQcRequired,
            quantity = response.ColumnIndex.Quantity,
            dailyRate = response.ColumnIndex.DailyRate,
            inventoryId = response.ColumnIndex.InventoryId,
            thumbnail = response.ColumnIndex.Thumbnail,
            appImageId = response.ColumnIndex.ImageId,
            subCategoryIdIndex = response.ColumnIndex.SubCategoryId,
            subCategoryIndex = response.ColumnIndex.SubCategory,
            classificationIndex = response.ColumnIndex.Classification,
            classificationColor = response.ColumnIndex.ClassificationColor,
            $inventoryContainer,
            $cornerTriangle,
            color;

        $inventoryContainer = $popup.find('#inventory');
        if (response.Rows.length == 0) {
            $inventoryContainer.append('<span style="font-weight: bold; font-size=1.3em">No Results</span>');
        }

        for (var i = 0; i < response.Rows.length; i++) {
            let html = [];
            html.push('<div class="cardContainer">');
            html.push('     <div class="card">');
            html.push('         <div class="cornerTriangle"></div>');
            html.push('         <div data-control="FwFormField" data-type="key" data-datafield="InventoryId" data-caption="InventoryId" class="fwcontrol fwformfield" data-isuniqueid="true" data-enabled="false"><input value="' + response.Rows[i][inventoryId] + '"></input></div>');
            html.push('         <div class="desccontainer">');
            html.push('             <div class="invdescription">' + response.Rows[i][descriptionIndex] + '</div>');
            html.push('             <div class="invimage">');
            html.push('                 <img src="' + response.Rows[i][thumbnail] + '" data-value="' + response.Rows[i][appImageId] + '" alt="Image" class="image">');
            html.push('              </div>');
            html.push('             </div>');
            html.push('         <div data-control="FwFormField" data-type="number" data-datafield="Quantity" data-caption="Qty" class="fwcontrol fwformfield" style="text-align:center">');
            html.push('              <span style="text-decoration:underline; font-weight:bold; ">Qty</span>');
            html.push('              <div style="float:left; border:1px solid #bdbdbd;">');
            html.push('                  <button class="decrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">-</button>');
            html.push('                  <input type="number" style="padding: 5px 0px; float:left; width:50%; border:none; text-align:center;" value = "' + response.Rows[i][quantity] + '" > ');
            html.push('                  <button class="incrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">+</button>');
            html.push('              </div>');
            html.push('         </div>');
            html.push('         <div data-control="FwFormField" data-type="number" data-datafield="QuantityAvailable" data-caption="Available" class="fwcontrol fwformfield" data-datafield="QuantityAvailable" data-enabled="false" style="text-align:center"><span style="text-decoration:underline; font-weight:bold;">Available</span></br></div>');
            html.push('         <div data-control="FwFormField" data-type="text" data-caption="Conflict Date" data-datafield="ConflictDate" class="fwcontrol fwformfield" data-enabled="false" style="text-align:center"><span style="text-decoration:underline; font-weight:bold;">Conflict</span></br></div>');
            html.push('         <div data-control="FwFormField" data-type="text" data-caption="All WH" data-datafield="AllWH" class="fwcontrol fwformfield" data-enabled="false" style="white-space:pre"><span style="text-decoration:underline; font-weight:bold;">All WH</span></br>&#160;</div>');
            html.push('         <div class="quantitycontainer">');
            html.push('             <div data-control="FwFormField" data-type="number" data-datafield="QuantityIn" data-caption="In" class="fwcontrol fwformfield" data-enabled="false" style="text-align:center"><span style="text-decoration:underline; font-weight:bold;">In</span></br></div>');
            html.push('             <div data-control="FwFormField" data-type="number" data-datafield="QuantityQcRequired" data-caption="QC" class="fwcontrol fwformfield" data-enabled="false" style="text-align:center"><span style="text-decoration:underline; font-weight:bold;">QC</span></br></div>');
            html.push('         </div>');
            //html.push('         <div class="accessories" style="width:80px;">');
            //if (response.Rows[i][classificationIndex] == "K" || response.Rows[i][classificationIndex] == "C") {
            //    html.push('<div class="accList">Accessories</div>');
            //}
            //else {
            //    html.push('<div>&#160;</div>');
            //}
            //html.push('         </div>');
            html.push('         <div data-control="FwFormField" data-type="number" data-digits="2" data-datafield="DailyRate" data-caption="Rate" class="fwcontrol fwformfield rate" data-enabled="false" style="text-align:center"><span style="text-decoration:underline; font-weight:bold;">Rate</span></br></div>');
            html.push('     </div>');
            if (response.Rows[i][classificationIndex] == "K" || response.Rows[i][classificationIndex] == "C") {
                html.push('<div class="accContainer" data-classification="' + response.Rows[i][classificationIndex] + '" style="float:left; width:95%; display:none">');

                html.push('</div>');
            }
            html.push('</div>');
            let item = html.join('');
            $inventoryContainer.append(item);
            let $card = $popup.find('#inventory > div:last');

            if (response.Rows[i][quantity] != 0) {
                $card.find('[data-datafield="Quantity"] input').css('background-color', '#c5eefb');
            }

            $card.find('[data-datafield="QuantityAvailable"]').append(response.Rows[i][quantityAvailable]);
            if (response.Rows[i][conflictDate] == "") {
                response.Rows[i][conflictDate] = 'N/A'
            }
            $card.find('[data-datafield="ConflictDate"]').append(response.Rows[i][conflictDate]);

            $card.find('[data-datafield="QuantityIn"]').append(response.Rows[i][quantityIn]);
            $card.find('[data-datafield="QuantityQcRequired"]').append(response.Rows[i][quantityQcRequired]);
            let rate = Number(response.Rows[i][dailyRate]).toFixed(2);
            $card.find('[data-datafield="DailyRate"]').append(rate);

            $cornerTriangle = $card.find('.cornerTriangle');
            if (response.Rows[i][classificationColor] == "") {
                color = 'transparent';
            } else {
                color = response.Rows[i][classificationColor];
            };

            $cornerTriangle.css({
                'border-left': '20px solid',
                'border-right': '20px solid transparent',
                'border-bottom': '20px solid transparent',
                'left': '0',
                'top': '0',
                'height': '0',
                'width': '0',
                'position': 'absolute',
                'right': '0px',
                'border-left-color': color,
                'z-index': '2'
            });
        }
        let $inventory = $popup.find('div.card');

        let css = {
            'box-shadow': '0 2px 4px 0 rgba(0,0,0,0.2)',
            'transition': '0.3s'
        }
        $inventory.css(css);

        let view = $popup.find('#inventoryView').val();
        this.listGridView($inventory, view);
    }

    listGridView($inventory, viewType) {
        let card = $inventory.find('.card'),
            description = $inventory.find('.invdescription'),
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
            accessoryContainer = $inventory.find('.accContainer'),
            cardContainer = $inventory.find('.cardContainer');
        switch (viewType) {
            case 'GRID':
                $inventory.find('span, br').show();
                allWH.hide();
                jQuery('#columnDescriptions').hide();
                jQuery('.accColumns').show();
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
                //$inventory.removeClass('listView', 'listGridView');
                //$inventory.addClass('gridView');
                break;
            case 'LIST':
                jQuery('.accColumns').hide();
                $inventory.find('span, br').hide();
                jQuery('#columnDescriptions').show();
                $inventory.css({ 'cursor': 'pointer', 'width': '95%', 'height': 'auto', 'float': 'left', 'padding': '5px', 'margin': '5px', 'position': 'relative' });
                descContainer.css({ 'width': '38%', 'float': '' });
                description.css({ 'float': 'left', 'padding-top': '15px', 'width': '100%', 'padding-bottom': '' });
                imageFrame.hide();
                quantityAvailable.css({ 'float': 'left', 'width': '8%' });
                conflictDate.css({ 'float': 'left', 'width': '10%' });
                allWH.show();
                allWH.css({ 'float': 'left', 'width': '8%' });
                quantityIn.css({ 'float': 'left', 'width': '50%' });
                quantityQcRequired.css({ 'float': 'left', 'width': '50%' });
                accessories.css({ 'float': 'left', 'padding': '', 'width': '8%', 'font-size': '.9em', 'color': 'blue' });
                rate.css({ 'float': 'left', 'width': '8%', 'padding-top': '', 'position': '', 'bottom': '', 'right': '' });
                quantity.css({ 'float': 'left', 'width': '10%', 'position': '', 'bottom': '', 'right': '' });
                quantityContainer.css({ 'float': 'left', 'width': '16%' });
                //$inventory.removeClass('gridView', 'listGridView');
                //$inventory.addClass('listView');
                break;
            case 'HYBRID':
                //cardContainer.css({ 'float': 'left', 'width': 'auto' });
                $inventory.find('span, br').hide();
                jQuery('.accColumns').hide();
                jQuery('#columnDescriptions').show();
                $inventory.css({ 'cursor': 'pointer', 'width': '95%', 'height': 'auto', 'float': 'left', 'padding': '5px', 'margin': '5px', 'position': 'relative' });
                descContainer.css({ 'width': '38%', 'float': 'left' });
                description.css({ 'float': 'right', 'padding-top': '15px', 'width': '75%', 'padding-bottom': '' });
                imageFrame.show();
                imageFrame.css({ 'float': 'left', 'width': '25%', 'height': '70px', 'line-height': '100px', 'display': 'inline-block', 'position': 'relative' });
                image.css({ 'max-height': '100%', 'max-width': '100%', 'width': 'auto', 'height': 'auto', 'position': 'absolute', 'top': '0', 'bottom': '0', 'left': '0', 'right': '0', 'margin': 'auto' });
                quantityAvailable.css({ 'float': 'left', 'width': '8%' });
                conflictDate.css({ 'float': 'left', 'width': '10%' });
                allWH.show();
                allWH.css({ 'float': 'left', 'width': '8%' });
                quantityIn.css({ 'float': 'left', 'width': '50%' });
                quantityQcRequired.css({ 'float': 'left', 'width': '50%' });
                accessories.css({ 'float': 'left', 'width': '8%', 'padding': '', 'font-size': '.9em', 'color': 'blue' });
                rate.css({ 'float': 'left', 'width': '8%', 'padding-top': '', 'position': '', 'bottom': '', 'right': '' });
                quantity.css({ 'float': 'left', 'width': '10%', 'position': '', 'bottom': '', 'right': '' });
                quantityContainer.css({ 'float': 'left', 'width': '16%' });
                //$inventory.removeClass('listView', 'gridView');
                //$inventory.addClass('listGridView');
                break;
        }
    };

    breadCrumbs($popup, $form, request) {
        var $searchpopup = jQuery('#searchpopup');
        var self = this
        $popup.on('click', '#breadcrumbs .type', function (e) {
            $popup.find("#breadcrumbs .subcategory, #breadcrumbs .category").empty();
            $popup.find("#breadcrumbs .subcategory, #breadcrumbs .category").attr('data-value', '');
            delete request.CategoryId;

            var inventoryTypeId = jQuery(e.currentTarget).attr('data-value');
            request.InventoryTypeId = inventoryTypeId;

            FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/search", request, FwServices.defaultTimeout, function onSuccess(response) {
                var categoryIdIndex = response.ColumnIndex.CategoryId;
                var categoryIndex = response.ColumnIndex.Category;

                $popup.find('#inventory').empty();
                $popup.find('#category, #subCategory').empty();

                var categories = [];
                let categoryColumn = $popup.find('#category');
                for (var i = 0; i < response.Rows.length; i++) {
                    if (categories.indexOf(response.Rows[i][categoryIndex]) == -1) {
                        categories.push(response.Rows[i][categoryIndex]);
                        categoryColumn.append('<ul class="fitText" style="cursor:pointer; padding:5px 5px 5px 10px; margin:1px;" data-value="' + response.Rows[i][categoryIdIndex] + '"><span>' + response.Rows[i][categoryIndex] + '</span></ul>');
                    }
                }
                self.fitToParent('#category .fitText span');
            }, null, $searchpopup);
        })

        $popup.on('click', '#breadcrumbs .category', function (e) {
            $popup.find("#breadcrumbs .subcategory").empty();
            $popup.find("#breadcrumbs .subcategory").attr('data-value', '');
            delete request.SubCategoryId;
            var categoryId = jQuery(e.currentTarget).attr('data-value');
            var inventoryTypeId = $popup.find('#breadcrumbs .type').attr('data-value');
            request.CategoryId = categoryId;
            request.InventoryTypeId = inventoryTypeId;

            FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/search", request, FwServices.defaultTimeout, function onSuccess(response) {
                $popup.find('#subCategory').empty();
                $popup.find('#inventory').empty();
                SearchInterfaceController.renderInventory($popup, response, false);
            }, null, $searchpopup);
        });
    };

    events($popup, $form, id) {
        let hasItemInGrids = false;
        var warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
        var request: any = {};
        var self = this;
        $popup.on('mouseenter', '#inventory > .cardContainer > .card', function (e) {
            let selected = jQuery(e.currentTarget).hasClass('selected');
            if (!selected) {
                jQuery(this).css('box-shadow', '0 5px 9px 0 rgba(0, 0, 0, 0.2)');
            }
        });

        $popup.on('mouseleave', '#inventory > .cardContainer > .card', function (e) {
            var selected = jQuery(e.currentTarget).hasClass('selected');
            if (selected) {
                jQuery(e.currentTarget).css('box-shadow', '0 6px 10px 0 rgba(0,0,153,0.2)');
            } else {
                jQuery(e.currentTarget).css('box-shadow', '0 2px 4px 0 rgba(0,0,0,0.2)');
            }
        });

        $popup.on('click', '#inventory > .cardContainer > .card', function (e) {
            const $card = jQuery(e.currentTarget);
            $popup.find('#inventory > .cardContainer > .card').removeClass('selected');
            $popup.find('#inventory > .cardContainer > .card').css('box-shadow', '0 2px 4px 0 rgba(0,0,0,0.2)');
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
        });

        var $searchpopup = jQuery('#searchpopup');
        $popup.on('change', '#inventory [data-datafield="Quantity"] input', function (e) {
            e.stopPropagation();
            var element = jQuery(e.currentTarget);
            var quantity = element.val();
            var inventoryId = element.parents('.card').find('[data-datafield="InventoryId"] input').val();
            var request = {
                OrderId: id,
                SessionId: id,
                InventoryId: inventoryId,
                WarehouseId: warehouseId,
                Quantity: quantity
            }

            if (quantity > 0) {
                hasItemInGrids = true;
            }

            if (quantity != 0) {
                element.css('background-color', '#c5eefb');
            } else {
                element.css('background-color', 'white');
            }

            var $accContainer = element.parents('.cardContainer').find('.accContainer');
            var accessoryRefresh = $popup.find('.toggleAccessories input').prop('checked');
            FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/", request, FwServices.defaultTimeout, function onSuccess(response) {
                if (!accessoryRefresh) {
                    if ($accContainer.css('display') == 'none') {
                        $popup.find('.accContainer').not($accContainer).hide();
                        $accContainer.slideToggle();
                    }
                }

                if ($accContainer.length > 0) {
                    self.refreshAccessoryQuantity($popup, id, warehouseId, inventoryId, e);
                }
            }, null, $searchpopup);
        });

        $popup.on('click', '.tab[data-caption="Preview"]', function () {
            self.refreshPreviewGrid($popup, id);
        });

        $popup.on('click', '.image', function (e) {
            e.stopPropagation();
            var $confirmation, $cancel;
            var image = jQuery(e.currentTarget).attr('src');
            var imageId = jQuery(e.currentTarget).attr('data-value');
            $confirmation = FwConfirmation.renderConfirmation('Image Viewer', '<div style="white-space:pre;">\n' +
                '<img src="' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'fwappimage.ashx?method=GetAppImage&appimageid=' + imageId + '&thumbnail=false' + '\" data-value="' + imageId + '" alt="No Image" class="image" style="max-width:100%;">');
            $cancel = FwConfirmation.addButton($confirmation, 'Close');
        });

        var highlight = {
            'box-shadow': '0 3px 7px 0 rgba(0, 0, 0, 0.2)'
        }

        var unhighlight = {
            'box-shadow': '0 0px 0px 0 rgba(0, 0, 0, 0.2)'
        }
        $popup.on('mouseenter', '#inventoryType ul, #category ul, #subCategory ul', function (e) {
            var selected = jQuery(e.currentTarget).hasClass('selected');
            if (selected) {
                jQuery(e.currentTarget).css({
                    'box-shadow': '0 3px 7px 0 rgba(0, 0, 0, 0.2)'
                })
            } else {
                jQuery(e.currentTarget).css(highlight);
            }
        });

        $popup.on('mouseleave', '#inventoryType ul, #category ul, #subCategory ul', function (e) {
            var selected = jQuery(e.currentTarget).hasClass('selected');
            if (selected) {
                jQuery(e.currentTarget).css({ 'background-color': '#bdbdbd', 'color': 'white', 'box-shadow': '0 3px 7px 0 rgba(0, 0, 0, 0.2)' })
            } else {
                jQuery(e.currentTarget).css(unhighlight)
            }
        });

        $popup.on('mouseenter', '#breadcrumbs > div', function (e) {
            jQuery(e.currentTarget).css({ 'color': '#4c4cff' });
        });

        $popup.on('mouseleave', '#breadcrumbs > div', function (e) {
            jQuery(e.currentTarget).css({ 'color': 'black' });
        });

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
                var request = {
                    OrderId: id,
                    SessionId: id
                }
                $popup.find('.addToOrder').css('cursor', 'wait');
                $popup.off('click', '.addToOrder');
                FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/addto", request, FwServices.defaultTimeout, function onSuccess(response) {
                    FwPopup.destroyPopup(jQuery(document).find('.fwpopup'));
                    var $combinedGrid = $form.find('.combinedgrid [data-name="OrderItemGrid"]'),
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

        $popup.on('click', '.listbutton', function () {
            let $inventory = $popup.find('div.card');
            self.listGridView($inventory, 'LIST');
        });

        $popup.on('click', '.listgridbutton', function () {
            let $inventory = $popup.find('div.card');
            self.listGridView($inventory, 'HYBRID');
        });

        $popup.on('click', '.gridbutton', function () {
            let $inventory = $popup.find('div.card');
            self.listGridView($inventory, 'GRID');
        });

        $popup.on('change', '.accItem [data-datafield="AccQuantity"] input', function (e) {
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

            if (quantity != 0) {
                element.css('background-color', '#c5eefb');
            } else {
                element.css('background-color', 'white');
            }

            FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch", accRequest, FwServices.defaultTimeout, function onSuccess(response) {
            }, null, null);

        });

        $popup.on('click', '.listbutton, .listgridbutton, .gridbutton', function (e) {
            var view = $popup.find('#inventoryView').val();
            if (jQuery(e.currentTarget).hasClass('listbutton')) {
                view = "LIST";
            } else if (jQuery(e.currentTarget).hasClass('listgridbutton')) {
                view = "HYBRID";
            } else {
                view = "GRID";
            };
            $popup.find('#inventoryView').val(view);
            var viewrequest: any = {};
            var userId = JSON.parse(sessionStorage.getItem('userid'));
            viewrequest.UserId = userId.webusersid;
            viewrequest.SearchModePreference = view;
            FwAppData.apiMethod(true, 'POST', "api/v1/usersearchsettings/", viewrequest, FwServices.defaultTimeout, function onSuccess(response) {
            }, null, null);
        });

        $popup.on('change', '.select, .sortby', function (e) {
            var request: any = {
                OrderId: id,
                SessionId: id,
                AvailableFor: FwFormField.getValueByDataField($popup, 'InventoryType'),
                WarehouseId: warehouseId,
                ShowAvailability: true,
                SortBy: $popup.find('.sortby select').val(),
                Classification: $popup.find('.select select').val(),
                ShowImages: true
            }
            let fromDate = FwFormField.getValueByDataField($popup, 'FromDate');
            let toDate = FwFormField.getValueByDataField($popup, 'ToDate');
            let inventoryTypeId = $popup.find('#breadcrumbs .type').attr('data-value');
            let categoryId = $popup.find('#breadcrumbs .category').attr('data-value');
            let subCategoryId = $popup.find('#breadcrumbs .subcategory').attr('data-value');
            let searchQuery = $popup.find('[data-datafield="SearchBox"] input').val();
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
                SearchInterfaceController.renderInventory($popup, response, true);
            }, null, $searchpopup);
        });

        $popup.on('click', '.toggleAccessories input', function () {
            var accessoryRefresh = $popup.find('.toggleAccessories input').prop('checked');
            if (accessoryRefresh) {
                $popup.find('.accContainer').css('display', 'none');
            }
        });

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
            var interval = 0;
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
        var previewrequest: any = {};
        var $searchpopup = jQuery('#searchpopup');
        var toDate = FwFormField.getValueByDataField($popup, 'ToDate');
        var fromDate = FwFormField.getValueByDataField($popup, 'FromDate');
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
            var $grid = $popup.find('[data-name="SearchPreviewGrid"]');
            FwBrowse.databindcallback($grid, response);
        }, null, $searchpopup);


    };

    refreshAccessoryQuantity($popup, id, warehouseId, inventoryId, e) {
        var request: any = {};
        var accessoryContainer = jQuery(e.currentTarget).parents('.cardContainer').find('.accContainer');
        var toDate = FwFormField.getValueByDataField($popup, 'ToDate');
        var fromDate = FwFormField.getValueByDataField($popup, 'FromDate');
        request = {
            SessionId: id,
            OrderId: id,
            ParentId: inventoryId,
            WarehouseId: warehouseId,
            ShowAvailability: 'true',
            ShowImages: 'true'
        }
        if (fromDate != "") {
            request.FromDate = fromDate;
        }
        if (toDate != "") {
            request.ToDate = toDate;
        }

        accessoryContainer.css({ 'float': 'left', 'height': 'auto', 'padding': '5px', 'margin': '5px', 'box-shadow': '0 6px 10px 0 rgba(0,0,153,0.2)', 'transition': '0.3s' });
        var html = [];
        if (!(accessoryContainer.find('.accColumns').length)) {
            html.push('<div class="accColumns" style="width:100%; display:none;">');
            html.push(' <div class="accList"></div>');
            html.push('                           <div style="float:left; width:38%; text-align:center; font-weight:bold">Description</div>');
            html.push('                           <div style="float:left; width:10%; text-align:center; font-weight:bold;">Qty</div>');
            html.push('                           <div style="float:left; width:8%; text-align:center; font-weight:bold;">Available</div>');
            html.push('                           <div style="float:left; width:10%; text-align:center; font-weight:bold;">Conflict Date</div>');
            html.push('                           <div style="float:left; width:8%; text-align:center; font-weight:bold;">All WH</div>');
            html.push('                           <div style="float:left; width:8%; text-align:center; font-weight:bold;">In</div>');
            html.push('                           <div style="float:left; width:8%; text-align:center; font-weight:bold;">QC</div>');
            html.push('                           <div style="float:left; width:8%; text-align:center; font-weight:bold;">Rate</div>');
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
                accHtml.push('      <div style="float:left; width:38%; position:relative; text-indent:1em;"><div class="descriptionColor"></div>' + response.Rows[i][descriptionIndex] + '</div>');
                accHtml.push('      <div data-control="FwFormField" data-type="number" data-datafield="AccQuantity" data-caption="Qty" class="fwcontrol fwformfield" style="position:relative; text-align:center; float:left; width:10%;">');
                accHtml.push('          <div style="float:left; border:1px solid #bdbdbd;">');
                accHtml.push('              <button class="decrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">-</button>');
                accHtml.push('              <input type="number" style="padding: 5px 0px; float:left; width:50%; border:none; text-align:center; " value="' + response.Rows[i][qtyIndex] + '">');
                accHtml.push('              <button class="incrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">+</button>');
                accHtml.push('          </div>');
                accHtml.push('      </div>');
                accHtml.push('      <div style="text-align:center; float:left; width:8%;">' + response.Rows[i][qtyAvailIndex] + '</div>');
                accHtml.push('      <div data-datafield="ConflictDate" style="text-align:center; float:left; width:10%;"></div>');
                accHtml.push('      <div style="text-align:center; float:left; width:8%; white-space:pre;">&#160;</div>');
                accHtml.push('      <div style="text-align:center; float:left; width:8%;">' + response.Rows[i][qtyInIndex] + '</div>');
                accHtml.push('      <div style="text-align:center; float:left; width:8%; white-space:pre;">&#160;</div>');
                accHtml.push('      <div style="text-align:center; float:left; width:8%; white-space:pre;">&#160;</div>');
                accHtml.push('</div>');

                let item = accHtml.join('');
                accessoryContainer.append(item);
                let $acc = accessoryContainer.find('.accItem:last');
                $popup.find('.accItem .fwformfield-caption').hide();

                if (response.Rows[i][qtyIndex] != 0) {
                    $acc.find('[data-datafield="AccQuantity"] input').css('background-color', '#c5eefb');
                }

                if (response.Rows[i][conflictIndex] == "") {
                    response.Rows[i][conflictIndex] = 'N/A'
                }
                $acc.find('[data-datafield="ConflictDate"]').append(response.Rows[i][conflictIndex]);

                let $descriptionColor = $acc.find('.descriptionColor');

                let desccolor;
                if (response.Rows[i][descriptionColorIndex] == "") {
                    desccolor = 'transparent';
                } else {
                    desccolor = response.Rows[i][descriptionColorIndex];
                };

                $descriptionColor.css({
                    'border-left': '20px solid',
                    'border-right': '20px solid transparent',
                    'border-bottom': '20px solid transparent',
                    'left': '0',
                    'top': '0',
                    'height': '0',
                    'width': '0',
                    'position': 'absolute',
                    'right': '0px',
                    'border-left-color': desccolor,
                    'z-index': '2'
                });

                let $qty = $acc.find('[data-datafield="AccQuantity"]');
                $qty.append('<div class="quantityColor"></div>');
                var $quantityColorDiv = $qty.find('.quantityColor');
                var qtycolor;
                if (response.Rows[i][quantityColorIndex] == "") {
                    qtycolor = 'transparent';
                } else {
                    qtycolor = response.Rows[i][quantityColorIndex];
                };

                $quantityColorDiv.css({
                    'border-left': '20px solid',
                    'border-right': '20px solid transparent',
                    'border-bottom': '20px solid transparent',
                    'left': '25%',
                    'top': '0',
                    'height': '0',
                    'width': '0',
                    'position': 'absolute',
                    'right': '0px',
                    'border-left-color': qtycolor,
                    'z-index': '2',
                    'pointer-events': 'none',
                    'margin-left': '3px'
                });
            }
        }, null, null);
    }

    fitToParent(selector) {
        var numIter = 10;
        var regexp = /\d+(\.\d+)?/;
        var fontSize = function (elem) {
            var match = elem.css('font-size').match(regexp);
            var size = match == null ? 16 : parseFloat(match[0]);
            return isNaN(size) ? 16 : size;
        }
        var test = jQuery(selector);
        jQuery(selector).each(function () {
            var elem = jQuery(this);
            var parentWidth = elem.parent().width();
            var parentHeight = elem.parent().height();
            if (elem.width() > parentWidth || elem.height() > parentHeight) {
                var maxSize = fontSize(elem), minSize = 0.1;
                for (var i = 0; i < numIter; i++) {
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