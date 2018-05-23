class SearchInterface {
    renderSearchPopup($form, id, type) {
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
        var $popup = FwPopup.renderPopup($popupHtml, { ismodal: true });
        FwPopup.showPopup($popup);
        //FwConfirmation.addControls($popup, $popupHtml);

        var searchhtml = [];
        searchhtml.push('<div id="searchFormHtml" class="fwform fwcontrol">');
        searchhtml.push('<div id="inventoryView" style="display:none"></div>');
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
        //searchhtml.push('                      <div data-type="button" class="fwformcontrol preview" style="width:70px; float:left; margin:15px;">Preview</div>');
        if (type == 'Order') {
            searchhtml.push('                      <div data-type="button" class="fwformcontrol addToOrder" style="width:120px; float:left; margin:15px;">Add to Order</div>');
        } else {
            searchhtml.push('                      <div data-type="button" class="fwformcontrol addToOrder" style="width:120px; float:left; margin:15px;">Add to Quote</div>');
        }

        searchhtml.push('                  </div>');
        searchhtml.push('                 <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        searchhtml.push('                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Search" data-datafield="SearchBox" style="width:570px; float:left;"></div>');
        searchhtml.push('                      <div data-type="button" class="fwformcontrol listbutton" style="margin: 12px 6px 12px 22px; padding:0px 7px 0px 7px;"><i class="material-icons" style="margin-top: 5px;">&#xE8EE;</i></div>');
        searchhtml.push('                      <div data-type="button" class="fwformcontrol listgridbutton" style="margin: 12px 6px 12px 6px; padding:0px 7px 0px 7px;"><i class="material-icons" style="margin-top: 5px;">&#xE8EF;</i></div>');
        searchhtml.push('                      <div data-type="button" class="fwformcontrol gridbutton" style="margin: 12px 6px 12px 6px; padding:0px 7px 0px 7px;"><i class="material-icons" style="margin-top: 5px;">&#xE8F0;</i></div>');
        searchhtml.push('                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield fwformcontrol toggleAccessories" data-caption="Disable Accessory Refresh" style="width:200px;"></div>');
        searchhtml.push('                 </div>');

        searchhtml.push('                 <div class="inventory" style="overflow:auto">');

        searchhtml.push('                 </div>');
        searchhtml.push('            </div>');

        searchhtml.push('            </div>');
        searchhtml.push('            </div>');
        var $searchform = searchhtml.join('');

        var formid = program.uniqueId(8);

        var $moduleTabControl = jQuery('#searchTabs');
        var newtabids = FwTabs.addTab($moduleTabControl, 'Search', false, 'FORM', true);
        $moduleTabControl.find('#' + newtabids.tabpageid).append(jQuery($searchform));
        //FwTabs.init($moduleTabControl);
        var $fwcontrols = jQuery($searchform).find('.fwcontrol');
        //FwControl.init($fwcontrols);
        //FwControl.renderRuntimeHtml($fwcontrols);
        //FwControl.setIds($fwcontrols, formid);
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
        } else {
            previewhtml.push('                      <div data-type="button" class="fwformcontrol addToOrder" style="width:120px; float:right; margin:15px;">Add to Quote</div>');
        }

        previewhtml.push('            </div>');
        previewhtml.push('     </div>');
        previewhtml.push('</div>');

        var $previewform = previewhtml.join('');
        var newtabids2 = FwTabs.addTab($moduleTabControl, 'Preview', false, 'FORM', false);
        $moduleTabControl.find('#' + newtabids2.tabpageid).append(jQuery($previewform));
        FwTabs.init($moduleTabControl);
        //var $fwcontrols = jQuery($previewform).find('.fwcontrol');
        var $previewTabControl = jQuery('#previewHtml');
        FwConfirmation.addControls($previewTabControl, $previewform);

        var startDate = FwFormField.getValueByDataField($form, 'EstimatedStartDate');
        FwFormField.setValueByDataField($popup, 'FromDate', startDate);

        var stopDate = FwFormField.getValueByDataField($form, 'EstimatedStopDate');
        FwFormField.setValueByDataField($popup, 'ToDate', stopDate);

        var toDate = FwFormField.getValueByDataField($popup, 'ToDate');
        var fromDate = FwFormField.getValueByDataField($popup, 'FromDate');
        var warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');

        var $previewGrid;
        var $previewGridControl;
        $previewGrid = $previewTabControl.find('[data-grid="SearchPreviewGrid"]');
        $previewGridControl = jQuery(jQuery('#tmpl-grids-SearchPreviewGridBrowse').html());
        $previewGrid.empty().append($previewGridControl);
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

        FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/preview", previewrequest, FwServices.defaultTimeout, function onSuccess(response) {
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
                        $popup.find('.inventory').empty();
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
        $popup.find('#inventoryType, #category, #subCategory, .inventory, .type, .category, .subcategory').empty();
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
            for (let i = 0; i < response.Rows.length; i++) {
                if (types.indexOf(response.Rows[i][inventoryTypeIndex]) == -1) {
                    types.push(response.Rows[i][inventoryTypeIndex]);
                    $popup.find('#inventoryType').append('<ul class="fitText" style="cursor:pointer; padding:10px 10px 10px 15px; margin:1px;" data-value="' + response.Rows[i][inventoryTypeIdIndex] + '"><span>' + response.Rows[i][inventoryTypeIndex] + '</span></ul>');
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
                //'border-left': '0px white',
                'box-shadow': '0 0px 0px 0 rgba(0, 0, 0, 0.2)'
            });

            invType = jQuery(e.currentTarget).text();
            $popup.find('#inventoryType ul').removeClass('selected');
            jQuery(e.currentTarget).addClass('selected');
            breadcrumb = $popup.find('#breadcrumbs .type');
            $popup.find("#breadcrumbs .category, #breadcrumbs .subcategory").empty();
            $popup.find("#breadcrumbs .category, #breadcrumbs .subcategory").attr('data-value', '');
            breadcrumb.text(invType);
            breadcrumb.append('<div style="float:right;">&#160; &#160; &#47; &#160; &#160;</div>');

            jQuery(e.currentTarget).css({ 'background-color': '#bdbdbd', 'color': 'white',/* 'border-left': '5px solid #939393', */'box-shadow': '0 6px 14px 0 rgba(0, 0, 0, 0.2)' });
            inventoryTypeId = jQuery(e.currentTarget).attr('data-value');
            breadcrumb.attr('data-value', inventoryTypeId);

            //delete request.SubCategoryId;
            //delete request.CategoryId;
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
                $popup.find('.inventory').empty();

                var categories = [];

                for (var i = 0; i < response.Rows.length; i++) {
                    if (categories.indexOf(response.Rows[i][categoryIndex]) == -1) {
                        categories.push(response.Rows[i][categoryIndex]);
                        $popup.find('#category').append('<ul class="fitText" style="cursor:pointer; padding:10px 10px 10px 15px; margin:1px;" data-value="' + response.Rows[i][categoryIdIndex] + '"><span>' + response.Rows[i][categoryIndex] + '</span></ul>');
                    }
                }
                self.fitToParent('#category .fitText span');
            }, null, $searchpopup);
            self.categoryOnClickEvents($popup, request, categoryType);
        });
    }

    categoryOnClickEvents($popup, request, categoryType) {
        var $searchpopup = jQuery('#searchpopup');
        var self = this;
        $popup.off('click', '#category ul');
        $popup.on('click', '#category ul', function (e) {
            var category, breadcrumb, categoryId, inventoryTypeId;
            $popup.find('#category ul').css({
                'background-color': '',
                'color': 'black',
                //'border-left': '0px white',
                'box-shadow': '0 0px 0px 0 rgba(0, 0, 0, 0.2)'
            });

            category = jQuery(e.currentTarget).text();
            $popup.find('#category ul').removeClass('selected');
            jQuery(e.currentTarget).addClass('selected');
            breadcrumb = $popup.find('#breadcrumbs .category');
            $popup.find("#breadcrumbs .subcategory").empty();
            $popup.find("#breadcrumbs .subcategory").attr('data-value', '');
            breadcrumb.text(category);
            breadcrumb.append('<div style="float:right;">&#160; &#160; &#47; &#160; &#160;</div>');
            jQuery(e.currentTarget).css({ 'background-color': '#bdbdbd', 'color': 'white', /*'border-left': '5px solid #939393',*/ 'box-shadow': '0 6px 14px 0 rgba(0, 0, 0, 0.2)' });
            categoryId = jQuery(e.currentTarget).attr('data-value');
            inventoryTypeId = $popup.find('#breadcrumbs .type').attr('data-value');
            breadcrumb.attr('data-value', categoryId);

            var subCatListRequest: any = {};
            subCatListRequest.uniqueids = {
                CategoryId: categoryId,
                TypeId: inventoryTypeId
            }

            switch (categoryType) {
                case 'rentalcategory':
                    subCatListRequest.RecType = "R";
                    break;
                case 'salescategory':
                    subCatListRequest.RecType = "S";
                    break;
                case 'laborcategory':
                    subCatListRequest.RecType = "L";
                    break;
                case 'misccategory':
                    subCatListRequest.RecType = "M";
                    break;
                case 'partscategory':
                    subCatListRequest.RecType = "P";
                    break;
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
                for (var i = 0; i < response.Rows.length; i++) {
                    if (subCategories.indexOf(response.Rows[i][subCategoryIndex]) == -1) {
                        subCategories.push(response.Rows[i][subCategoryIndex]);
                        $popup.find('#subCategory').append('<ul class="fitText" style="cursor:pointer; padding:10px 10px 10px 15px; margin:1px;" data-value="' + response.Rows[i][subCategoryIdIndex] + '"><span>' + response.Rows[i][subCategoryIndex] + '</span></ul>');
                    }
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
                    FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/search", request, FwServices.defaultTimeout, function onSuccess(response) {
                        $popup.find('.inventory').empty();
                        SearchInterfaceController.renderInventory($popup, response, false);
                    }, null, $searchpopup);

                } else {
                    $popup.find('.inventory').empty();
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
                //'border-left': '0px white',
                'box-shadow': '0 0px 0px 0 rgba(0, 0, 0, 0.2)'
            });

            subCategory = jQuery(e.currentTarget).text();
            $popup.find('#subCategory ul').removeClass('selected');
            jQuery(e.currentTarget).addClass('selected');
            breadcrumb = $popup.find('#breadcrumbs .subcategory');
            breadcrumb.text(subCategory);
            subCategoryId = jQuery(e.currentTarget).attr('data-value');
            breadcrumb.attr('data-value', subCategoryId);
            jQuery(e.currentTarget).css({ 'background-color': '#bdbdbd', 'color': 'white', /*'border-left': '5px solid #939393',*/ 'box-shadow': '0 6px 14px 0 rgba(0, 0, 0, 0.2)' });

            categoryId = $popup.find('#breadcrumbs .category').attr('data-value');
            inventoryTypeId = $popup.find('#breadcrumbs .type').attr('data-value');

            request.SubCategoryId = subCategoryId;
            request.CategoryId = categoryId;
            request.InventoryTypeId = inventoryTypeId;
            request.SortBy = $popup.find('.sortby select').val();
            request.Classification = $popup.find('.select select').val();

            FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/search", request, FwServices.defaultTimeout, function onSuccess(response) {
                $popup.find('.inventory').empty();
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
            classificationColor = response.ColumnIndex.ClassificationColor;

        if (response.Rows.length == 0) {
            $popup.find('.inventory').append('<span style="font-weight: bold; font-size=1.3em">No Results</span>');
        }

        for (var i = 0; i < response.Rows.length; i++) {

            var html = [];
            html.push('<div class="cardContainer">');
            html.push('<div class="card">');
            html.push('<div class="cornerTriangle"></div>');
            html.push('<div data-control="FwFormField" data-type="key" data-datafield="InventoryId" data-caption="InventoryId" class="fwcontrol fwformfield" data-isuniqueid="true" data-enabled="false"></div>');
            html.push('<div class="desccontainer">')
            html.push('<div class="invdescription">' + response.Rows[i][descriptionIndex] + '</div>');
            html.push('<div class="invimage">');
            html.push('<img src="' + response.Rows[i][thumbnail] + '" data-value="' + response.Rows[i][appImageId] + '" alt="Image" class="image">');
            html.push('</div>');
            html.push('</div>')
            html.push('<div data-control="FwFormField" data-type="number" data-datafield="QuantityAvailable" data-caption="Available" class="fwcontrol fwformfield" data-datafield="QuantityAvailable" data-enabled="false"></div>');
            html.push('<div data-control="FwFormField" data-type="text" data-caption="Conflict Date" data-datafield="ConflictDate" class="fwcontrol fwformfield" data-enabled="false"></div>');
            html.push('<div data-control="FwFormField" data-type="text" data-caption="All WH" data-datafield="AllWH" class="fwcontrol fwformfield" data-enabled="false"></div>');
            html.push('<div class="quantitycontainer">');
            html.push('<div data-control="FwFormField" data-type="number" data-datafield="QuantityIn" data-caption="In" class="fwcontrol fwformfield" data-enabled="false"></div>');
            html.push('<div data-control="FwFormField" data-type="number" data-datafield="QuantityQcRequired" data-caption="QC" class="fwcontrol fwformfield" data-enabled="false"></div>');
            html.push('</div>');
            html.push('<div class="accessories" style="width:80px;">');
            var test = response.Rows[i][classificationIndex];
            if (response.Rows[i][classificationIndex] == "K" || response.Rows[i][classificationIndex] == "C") {
                html.push('<div class="accList">Accessories</div>');
            }
            else {
                html.push('<div>&#160;</div>');
            }
            html.push('</div>');
            html.push('<div data-control="FwFormField" data-type="number" data-digits="2" data-datafield="DailyRate" data-caption="Rate" class="fwcontrol fwformfield rate" data-enabled="false"></div>');
            html.push('<div data-control="FwFormField" data-type="number" data-datafield="Quantity" data-caption="Qty" class="fwcontrol fwformfield"></div>');
            html.push('</div>');

            if (response.Rows[i][classificationIndex] == "K" || response.Rows[i][classificationIndex] == "C") {
                html.push('<div class="accContainer" data-classification="' + response.Rows[i][classificationIndex] + '" style="float:left; width:90%; display:none">');

                html.push('</div>');
            }
            html.push('</div>');
            var item = html.join('');
            $popup.find('.inventory').append(item);
            var $card = $popup.find('.inventory > div:last');

            FwConfirmation.addControls($card, item);
            FwFormField.setValueByDataField($card, 'InventoryId', response.Rows[i][inventoryId]);
            FwFormField.setValueByDataField($card, 'QuantityAvailable', response.Rows[i][quantityAvailable]);
            FwFormField.setValueByDataField($card, 'ConflictDate', response.Rows[i][conflictDate]);
            FwFormField.setValueByDataField($card, 'QuantityIn', response.Rows[i][quantityIn]);
            FwFormField.setValueByDataField($card, 'QuantityQcRequired', response.Rows[i][quantityQcRequired]);
            FwFormField.setValueByDataField($card, 'Quantity', response.Rows[i][quantity]);
            var rate = Number(response.Rows[i][dailyRate]).toFixed(2);
            FwFormField.setValueByDataField($card, 'DailyRate', rate);

            let $cornerTriangle = $card.find('.cornerTriangle');

            var color;
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
        var $inventory = $popup.find('div.card');

        var css = {
            'box-shadow': '0 4px 8px 0 rgba(0,0,0,0.2)',
            'transition': '0.3s'
        }
        $inventory.css(css);

        var view = $popup.find('#inventoryView').val();
        this.listGridView($inventory, view);
    }

    listGridView($inventory, viewType) {
        var card = $inventory.find('.card'),
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
                //cardContainer.css({ 'float': 'left', 'width': 'auto' });
                allWH.hide();
                $inventory.css({ 'cursor': 'pointer', 'width': '225px', 'height': '315px', 'float': 'left', 'padding': '10px', 'margin': '8px', 'position': 'relative' });
                //card.css({ 'cursor': 'pointer', 'width': '225px', 'height': '315px', 'float': 'left', 'padding': '10px', 'margin': '8px',  });
                descContainer.css({ 'width': '', 'float': '' });
                description.css({ 'height': '15%', 'width': '', 'padding-top': '', 'padding-bottom': '5px', 'float': '' });
                imageFrame.show();
                imageFrame.css({ 'float': 'left', 'width': '125px', 'height': '175px', 'line-height': '175px', 'display': 'inline-block', 'position': 'relative' });
                image.css({ 'max-height': '100%', 'max-width': '100%', 'width': 'auto', 'height': 'auto', 'position': 'absolute', 'top': '0', 'bottom': '0', 'left': '0', 'right': '0', 'margin': 'auto' });
                quantityAvailable.css({ 'float': 'right', 'width': '90px' });
                conflictDate.css({ 'float': 'right', 'width': '90px' });
                quantityIn.css({ 'float': 'left', 'width': '45px' });
                quantityQcRequired.css({ 'float': 'right', 'width': '45px' });
                accessories.css({ 'float': 'right', 'padding': '10px 5px 10px 0', 'font-size': '.9em', 'color': 'blue' });
                rate.css({ 'float': 'left', 'padding-top': '20px', 'width': '90px', 'position': 'absolute', 'bottom': '10px' });
                quantity.css({ 'float': 'right', 'width': '90px', 'position': 'absolute', 'bottom': '10px', 'right': '10px' });
                quantityContainer.css({ 'float': 'right' });
                //accessoryContainer.css({ 'float': 'left', 'width': '225px', 'height': 'auto', 'padding-top': '20px', 'box-shadow': '0 4px 8px 0 rgba(0,0,0,0.2)', 'transition':'0.3s' });
                //$inventory.removeClass('listView', 'listGridView');
                //$inventory.addClass('gridView');
                break;
            case 'LIST':
                //cardContainer.css({ 'float': 'left', 'width': 'auto' });
                $inventory.css({ 'cursor': 'pointer', 'width': '95%', 'height': 'auto', 'float': 'left', 'padding': '10px', 'margin': '8px', 'position': 'relative' });
                //card.css({ 'cursor': 'pointer', 'width': '95%', 'height': 'auto', 'float': 'left', 'padding': '10px', 'margin': '8px', 'position': 'relative' });
                descContainer.css({ 'width': '', 'float': '' });
                description.css({ 'float': 'left', 'padding-top': '15px', 'width': '35%', 'padding-bottom': '' });
                imageFrame.hide();
                //image.css({ 'max-height': '100%', 'max-width': '100%', 'width': 'auto', 'height': 'auto', 'position': 'absolute', 'top': '0', 'bottom': '0', 'left': '0', 'right': '0', 'margin': 'auto' });
                quantityAvailable.css({ 'float': 'left', 'width': '75px' });
                conflictDate.css({ 'float': 'left', 'width': '90px' });
                allWH.show();
                allWH.css({ 'float': 'left', 'width': '75px' });
                quantityIn.css({ 'float': 'left', 'width': '45px' });
                quantityQcRequired.css({ 'float': 'left', 'width': '45px' });
                accessories.css({ 'float': 'left', 'padding': '20px 15px 10px 15px', 'font-size': '.9em', 'color': 'blue' });
                rate.css({ 'float': 'left', 'width': '90px', 'padding-top': '', 'position': '', 'bottom': '', 'right': '' });
                quantity.css({ 'float': 'right', 'width': '90px', 'position': '', 'bottom': '', 'right': '' });
                quantityContainer.css({ 'float': 'left' });
                //$inventory.removeClass('gridView', 'listGridView');
                //$inventory.addClass('listView');
                break;
            case 'HYBRID':
                //cardContainer.css({ 'float': 'left', 'width': 'auto' });
                $inventory.css({ 'cursor': 'pointer', 'width': '95%', 'height': 'auto', 'float': 'left', 'padding': '10px', 'margin': '8px', 'position': 'relative' });
                //card.css({ 'cursor': 'pointer', 'width': '95%', 'height': 'auto', 'float': 'left', 'padding': '10px', 'margin': '8px', 'position': 'relative' });
                descContainer.css({ 'width': '40%', 'float': 'left' });
                description.css({ 'float': 'right', 'padding-top': '15px', 'width': '75%', 'padding-bottom': '' });
                imageFrame.show();
                imageFrame.css({ 'float': 'left', 'width': '60px', 'height': '70px', 'line-height': '100px', 'display': 'inline-block', 'position': 'relative' });
                image.css({ 'max-height': '100%', 'max-width': '100%', 'width': 'auto', 'height': 'auto', 'position': 'absolute', 'top': '0', 'bottom': '0', 'left': '0', 'right': '0', 'margin': 'auto' });
                quantityAvailable.css({ 'float': 'left', 'width': '75px' });
                conflictDate.css({ 'float': 'left', 'width': '90px' });
                allWH.show();
                allWH.css({ 'float': 'left', 'width': '75px' });
                quantityIn.css({ 'float': 'left', 'width': '45px' });
                quantityQcRequired.css({ 'float': 'left', 'width': '45px' });
                accessories.css({ 'float': 'left', 'padding': '20px 15px 10px 15px', 'font-size': '.9em', 'color': 'blue' });
                rate.css({ 'float': 'left', 'width': '80px', 'padding-top': '', 'position': '', 'bottom': '', 'right': '' });
                quantity.css({ 'float': 'right', 'width': '80px', 'position': '', 'bottom': '', 'right': '' });
                quantityContainer.css({ 'float': 'left' });
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

                $popup.find('.inventory').empty();
                $popup.find('#category, #subCategory').empty();

                var categories = [];
                for (var i = 0; i < response.Rows.length; i++) {
                    if (categories.indexOf(response.Rows[i][categoryIndex]) == -1) {
                        categories.push(response.Rows[i][categoryIndex]);
                        $popup.find('#category').append('<ul class="fitText" style="cursor:pointer; padding:10px 10px 10px 15px; margin:1px;" data-value="' + response.Rows[i][categoryIdIndex] + '"><span>' + response.Rows[i][categoryIndex] + '</span></ul>');
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
                $popup.find('.inventory').empty();
                SearchInterfaceController.renderInventory($popup, response, false);
            }, null, $searchpopup);
        });
    };

    events($popup, $form, id) {
        var warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
        var request: any = {};
        var self = this;
        $popup.on('mouseenter', '.inventory > .cardContainer > .card', function (e) {
            let selected = jQuery(e.currentTarget).hasClass('selected');

            if (!selected) {
                jQuery(this).css('box-shadow', '0 10px 18px 0 rgba(0, 0, 0, 0.2)');
            }
        });

        $popup.on('mouseleave', '.inventory > .cardContainer > .card', function (e) {
            var selected = jQuery(e.currentTarget).hasClass('selected');
            if (selected) {
                jQuery(e.currentTarget).css('box-shadow', '0 12px 20px 0 rgba(0,0,153,0.2)');
            } else {
                jQuery(e.currentTarget).css('box-shadow', '0 4px 8px 0 rgba(0,0,0,0.2)');
            }

        });

        $popup.on('click', '.inventory > .cardContainer > .card', function (e) {
            $popup.find('.inventory > .cardContainer > .card').removeClass('selected');
            $popup.find('.inventory > .cardContainer > .card').css('box-shadow', '0 4px 8px 0 rgba(0,0,0,0.2)');

            jQuery(e.currentTarget).addClass('selected');

            jQuery(e.currentTarget).css('box-shadow', '0 12px 20px 0 rgba(0,0,153,0.2)');
        });


        var $searchpopup = jQuery('#searchpopup');
        $popup.on('change', '.inventory [data-datafield="Quantity"] input', function (e) {
            var quantity = jQuery(e.currentTarget).val();
            var inventoryId = jQuery(e.currentTarget).parents('.card').find('[data-datafield="InventoryId"] input').val();
            var request = {
                OrderId: id,
                SessionId: id,
                InventoryId: inventoryId,
                WarehouseId: warehouseId,
                Quantity: quantity
            }

            var $accContainer = jQuery(e.currentTarget).parents('.cardContainer').find('.accContainer');
            var $accButton = jQuery(e.currentTarget).parents('.card').find('.accList');
            var accessoryRefresh = $popup.find('.toggleAccessories input').prop('checked');
            FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/", request, FwServices.defaultTimeout, function onSuccess(response) {
                if (!accessoryRefresh) {
                    if ($accContainer.css('display') == 'none') {
                        $accContainer.css('display', '');
                    }
                }
                self.refreshAccessoryQuantity($popup, id, warehouseId, inventoryId, e);

            }, null, $searchpopup);
        });



        $popup.on('click', '.tab[data-caption="Preview"]', function () {
            self.refreshPreviewGrid($popup, id);
        });

        $popup.on('click', '.image', function (e) {
            var $confirmation, $cancel;
            var image = jQuery(e.currentTarget).attr('src');
            var imageId = jQuery(e.currentTarget).attr('data-value');

            $confirmation = FwConfirmation.renderConfirmation('Image Viewer', '<div style="white-space:pre;">\n' +
                '<img src="' + applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'fwappimage.ashx?method=GetAppImage&appimageid=' + imageId + '&thumbnail=false' + '\" data-value="' + imageId + '" alt="No Image" class="image" style="max-width:100%;">');

            $cancel = FwConfirmation.addButton($confirmation, 'Close');
        });

        var highlight = {
            //'border-left': '6px solid #bdbdbd',
            'box-shadow': '0 6px 14px 0 rgba(0, 0, 0, 0.2)'
        }

        var unhighlight = {
            //'border-left': '0px white',
            'box-shadow': '0 0px 0px 0 rgba(0, 0, 0, 0.2)'
        }
        $popup.on('mouseenter', '#inventoryType ul, #category ul, #subCategory ul', function (e) {
            var selected = jQuery(e.currentTarget).hasClass('selected');

            if (selected) {
                jQuery(e.currentTarget).css({
                    //'border-left': '6px solid #939393',
                    'box-shadow': '0 6px 14px 0 rgba(0, 0, 0, 0.2)'
                })
            } else {
                jQuery(e.currentTarget).css(highlight);
            }

        });

        $popup.on('mouseleave', '#inventoryType ul, #category ul, #subCategory ul', function (e) {
            var selected = jQuery(e.currentTarget).hasClass('selected');
            if (selected) {
                jQuery(e.currentTarget).css({ 'background-color': '#bdbdbd', 'color': 'white', /*'border-left': '6px solid #939393', */'box-shadow': '0 6px 14px 0 rgba(0, 0, 0, 0.2)' })
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

        $popup.on('click', '.addToOrder', function () {
            var request = {
                OrderId: id,
                SessionId: id
            }
            FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/addto", request, FwServices.defaultTimeout, function onSuccess(response) {
                FwPopup.destroyPopup(jQuery(document).find('.fwpopup'));
                var $orderItemGridRental = $form.find('.rentalgrid [data-name="OrderItemGrid"]');
                FwBrowse.search($orderItemGridRental);
                var $orderItemGridSales = $form.find('.salesgrid [data-name="OrderItemGrid"]');
                FwBrowse.search($orderItemGridSales);
                var $orderItemGridLabor = $form.find('.laborgrid [data-name="OrderItemGrid"]');
                FwBrowse.search($orderItemGridLabor);
                var $orderItemGridMisc = $form.find('.miscgrid [data-name="OrderItemGrid"]');
                FwBrowse.search($orderItemGridMisc);
            }, null, $searchpopup);
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


        $popup.on('click', '.accList', function (e) {
            let accessoryContainer = jQuery(e.currentTarget).parents('.cardContainer').find('.accContainer');
            let inventoryId = jQuery(e.currentTarget).parents('.card').find('[data-datafield="InventoryId"] input').val();
            self.refreshAccessoryQuantity($popup, id, warehouseId, inventoryId, e);
            accessoryContainer.slideToggle();

        });

        $popup.on('change', '.accItem [data-datafield="AccQuantity"] input', function (e) {
            let inventoryId = jQuery(e.currentTarget).parents('.accItem').find('[data-datafield="InventoryId"] input').val();
            let quantity = jQuery(e.currentTarget).val();
            let parentId = jQuery(e.currentTarget).parents('.cardContainer').find('.card [data-datafield="InventoryId"] input').val();
            let accRequest: any = {};
            accRequest = {
                SessionId: id,
                ParentId: parentId,
                InventoryId: inventoryId,
                WarehouseId: warehouseId,
                Quantity: quantity
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

            var fromDate = FwFormField.getValueByDataField($popup, 'FromDate');
            var toDate = FwFormField.getValueByDataField($popup, 'ToDate');
            if (fromDate != "") {
                request.FromDate = fromDate;
            }
            if (toDate != "") {
                request.ToDate = toDate;
            }

            let inventoryTypeId = $popup.find('#breadcrumbs .type').attr('data-value');
            let categoryId = $popup.find('#breadcrumbs .category').attr('data-value');
            let subCategoryId = $popup.find('#breadcrumbs .subcategory').attr('data-value');

            if (inventoryTypeId != "") {
                request.InventoryTypeId = inventoryTypeId;
            }
            if (categoryId != "") {
                request.CategoryId = categoryId;
            }
            if (subCategoryId != "") {
                request.SubCategoryId = subCategoryId;
            }


            FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/search", request, FwServices.defaultTimeout, function onSuccess(response) {
                $popup.find('.inventory').empty();
                SearchInterfaceController.renderInventory($popup, response, true);
            }, null, $searchpopup);

        });

        $popup.on('click', '.toggleAccessories input', function () {
            var accessoryRefresh = $popup.find('.toggleAccessories input').prop('checked');
            if (accessoryRefresh) {
                $popup.find('.accContainer').css('display', 'none');
            }
        });

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

        FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/preview", previewrequest, FwServices.defaultTimeout, function onSuccess(response) {
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

        var html = [];
        if (!(accessoryContainer.find('.accItem').length)) {
            html.push('<div style="width:100%">');
            html.push(' <div class="accList" style="font-size: 1.2em; color: blue; text-align:center; text-decoration: underline; cursor:pointer;">Accessories</div>');
            html.push('     <div style="width:50%; float:left; font-weight:bold;">Description</div>');
            html.push('     <div style="text-align:center; width:12%; float:left; font-weight:bold;"> Qty </div>');
            html.push('     <div style="text-align:center; width:12%; float:left; font-weight:bold;"> In </div>');
            html.push('     <div style="text-align:center; width:12%; float:left; font-weight:bold;"> Avail</div>');
            html.push('     <div style="text-align:center; width:12%; float:left; font-weight:bold;"> Conflict </div>');
            //html.push('     <div style="width:10%; float:left; font-weight:bold;"> Note</div>');
            html.push(' </div>');
            html.push('</div>');
            accessoryContainer.append(html.join(''));
            accessoryContainer.css({ 'float': 'left', 'height': 'auto', 'padding': '10px', 'margin': '10px', 'box-shadow': '0 12px 20px 0 rgba(0,0,153,0.2)', 'transition': '0.3s' });
            //accessoryContainer.slideToggle();
        }
        //else {
        //    accessoryContainer.slideToggle();
        //}

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

            for (var i = 0; i < response.Rows.length; i++) {
                let accHtml = [];
                accHtml.push('<div class="accItem" style="width:100%">');
                accHtml.push('  <div data-control="FwFormField" style="display:none" data-type="text" data-datafield="InventoryId" class="fwcontrol fwformfield"></div>');
                accHtml.push('  <div style="text-indent: 1em; float:left; width:50%; position:relative;"><div class="descriptionColor"></div>' + response.Rows[i][descriptionIndex] + '</div>');
                accHtml.push('  <div data-control="FwFormField" style="text-align:center; float:left; width:12%; padding:5px 10px 0 0; position:relative;" data-type="number" data-datafield="AccQuantity" class="fwcontrol fwformfield qtyColor"></div>');
                accHtml.push('  <div style="text-align:center; float:left; width:12%; padding-left:5px;">' + response.Rows[i][qtyInIndex] + '</div>');
                accHtml.push('  <div style="text-align:center; float:left; width:12%; padding-left:5px;">' + response.Rows[i][qtyAvailIndex] + '</div>');
                accHtml.push('  <div style="text-align:center; float:left; width:12%; padding-left:5px;">' + response.Rows[i][conflictIndex] + '</div>');
                accHtml.push('</div>');

                let item = accHtml.join('');
                accessoryContainer.append(item);
                let $acc = accessoryContainer.find('.accItem:last');
                FwConfirmation.addControls($acc, item);
                $popup.find('.accItem .fwformfield-caption').hide();
                FwFormField.setValueByDataField($acc, 'AccQuantity', response.Rows[i][qtyIndex]);
                FwFormField.setValueByDataField($acc, 'InventoryId', response.Rows[i][inventoryIdIndex]);

                let $descriptionColor = $acc.find('.descriptionColor');

                var desccolor;
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
                    'left': '0',
                    'top': '6px',
                    'height': '0',
                    'width': '0',
                    'position': 'absolute',
                    'right': '0px',
                    'border-left-color': qtycolor,
                    'z-index': '2'
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