class SearchInterface {
    renderSearchPopup($form, id) {
        var self = this;

        var html = [];
        html.push('<div id="searchpopup" style="background-color: white; box-shadow: 0 25px 44px rgba(0, 0, 0, 0.30), 0 20px 15px rgba(0, 0, 0, 0.22); width: 85vw; height: 85vh; overflow:scroll; position:relative;">');

        html.push(' <div id="searchTabs" class="fwcontrol fwtabs" data-rendermode="runtime" data-version="1" data-control="FwTabs">');
        html.push(' <div class="tabs"></div>');
        html.push(' <div class="tabpages"></div>');
        html.push('</div>');
        //html.push('     <div id="breadcrumbs" class="fwmenu default" style="width:100%;height:5%; padding-left: 20px;">');
        //html.push('         <div class="type" style="float:left; cursor: pointer; font-weight: bold;"></div>');
        //html.push('         <div class="category" style="float:left; cursor: pointer; font-weight: bold;"></div>');
        //html.push('         <div class="subcategory" style="float:left; cursor: pointer; font-weight: bold;"></div>');
        //html.push('     </div>');

        //html.push('     <div class="formrow" style="width:100%; position:absolute;">');
        //html.push('              <div data-control="FwFormField" class="fwcontrol fwformfield" data-caption=" "  data-datafield="InventoryType" data-type="radio" style="width:30%; margin: 5px 0px 25px 35px; float:clear;">');
        //html.push('                  <div data-value="R" data-caption="Rental" style="float:left; width:20%;"></div>');
        //html.push('                  <div data-value="S" data-caption="Sales" style="float:left; width:20%;"></div>');
        //html.push('                  <div data-value="L" data-caption="Labor" style="float:left; width:20%;"></div>');
        //html.push('                  <div data-value="M" data-caption="Misc" style="float:left; width:20%;"></div>');
        //html.push('                  <div data-value="P" data-caption="Parts" style="float:left; width:20%;"></div>');
        //html.push('              </div>');

        //html.push('              <div id="inventoryType" style="width:10%; margin: 5px 0px 0px 5px; float:left;">');
        //html.push('              </div>');

        //html.push('             <div id="category" style="width:10%; margin: 5px 0px 0px 5px; float:left;">');
        //html.push('             </div>');

        //html.push('             <div id="subCategory" style="width:10%; margin: 5px 0px 0px 5px; float:left;">');
        //html.push('             </div>');

        //html.push('            <div style="width:65%; position:absolute; left: 35%; right: 5%;">')
        //html.push('                 <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        //html.push('                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Est. Start" data-datafield="FromDate" style="width:120px; float:left;"></div>');
        //html.push('                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Est. Stop" data-datafield="ToDate" style="width:120px;float:left;"></div>');
        //html.push('                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Select" data-datafield="" style="width:150px;float:left;"></div>');
        //html.push('                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Sort By" data-datafield="" style="width:150px;float:left;"></div>');
        //html.push('                      <div data-type="button" class="fwformcontrol preview" style="width:70px; float:left; margin:15px;">Preview</div>');
        //html.push('                      <div data-type="button" class="fwformcontrol addToOrder" style="width:120px; float:left; margin:15px;">Add to Order</div>');
        //html.push('                  </div>');
        //html.push('                 <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        //html.push('                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Search" data-datafield="SearchBox" style="width:540px; float:left;"></div>');
        //html.push('                      <div data-type="button" class="fwformcontrol listbutton" style="margin: 12px 6px 12px 60px; padding:0px 7px 0px 7px;"><i class="material-icons" style="margin-top: 5px;">&#xE8EE;</i></div>');
        //html.push('                      <div data-type="button" class="fwformcontrol listgridbutton" style="margin: 12px 6px 12px 6px; padding:0px 7px 0px 7px;"><i class="material-icons" style="margin-top: 5px;">&#xE8EF;</i></div>');
        //html.push('                      <div data-type="button" class="fwformcontrol gridbutton" style="margin: 12px 6px 12px 6px; padding:0px 7px 0px 7px;"><i class="material-icons" style="margin-top: 5px;">&#xE8F0;</i></div>');
        //html.push('                 </div>');


        //html.push('                 <div class="inventory" style="overflow:auto">');

        //html.push('                 </div>');
        //html.push('            </div>');

        html.push('     <div class="close-modal" style="display:flex; position:absolute; top:10px; right:15px; cursor:pointer;"><i class="material-icons">clear</i><div class="btn-text">Close</div></div>');
        html.push('</div>');
        var $popupHtml = html.join('');
        var $popup = FwPopup.renderPopup($popupHtml, { ismodal: true });
        FwPopup.showPopup($popup);
        //FwConfirmation.addControls($popup, $popupHtml);

        var searchhtml = [];
        searchhtml.push('<div id="searchFormHtml" class="fwform fwcontrol">');

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
        searchhtml.push('                      <div data-type="button" class="fwformcontrol addToOrder" style="width:120px; float:left; margin:15px;">Add to Order</div>');
        searchhtml.push('                  </div>');
        searchhtml.push('                 <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        searchhtml.push('                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Search" data-datafield="SearchBox" style="width:570px; float:left;"></div>');
        searchhtml.push('                      <div data-type="button" class="fwformcontrol listbutton" style="margin: 12px 6px 12px 22px; padding:0px 7px 0px 7px;"><i class="material-icons" style="margin-top: 5px;">&#xE8EE;</i></div>');
        searchhtml.push('                      <div data-type="button" class="fwformcontrol listgridbutton" style="margin: 12px 6px 12px 6px; padding:0px 7px 0px 7px;"><i class="material-icons" style="margin-top: 5px;">&#xE8EF;</i></div>');
        searchhtml.push('                      <div data-type="button" class="fwformcontrol gridbutton" style="margin: 12px 6px 12px 6px; padding:0px 7px 0px 7px;"><i class="material-icons" style="margin-top: 5px;">&#xE8F0;</i></div>');
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
            { value: '', text: 'Complete/Kit' },
            { value: '', text: 'Container' },
            { value: '', text: 'Item' },
            { value: '', text: 'Accessory' }], true);

        FwFormField.loadItems($sortby, [
            { value: '', text: 'I-Code' },
            { value: '', text: 'Description' },
            { value: '', text: 'Part No.' },
            { value: '', text: 'Inventory Management' }], true);

        var previewhtml = [];
        previewhtml.push('<div id="previewHtml" class="fwform fwcontrol">');
        previewhtml.push('         <div class="fwmenu default" style="width:100%;height:7%; padding-left: 20px;">');
        previewhtml.push('         </div>');
        previewhtml.push('     <div class="formrow" style="width:100%; position:absolute;">');
        previewhtml.push('            <div>');
        previewhtml.push('                 <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        previewhtml.push('                      <div data-control="FwGrid" data-grid="SearchPreviewGrid" data-securitycaption="Preview"></div>');
        previewhtml.push('                </div>');
        previewhtml.push('                      <div data-type="button" class="fwformcontrol addToOrder" style="width:120px; float:right; margin:15px;">Add to Order</div>');
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
            FromDate: toDate,
            ShowImages: true,
            ToDate: fromDate
        };


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
        //$descriptionField.on('blur', function () {

        //    var request: any = {
        //        OrderId: id,
        //        SessionId: id,
        //        AvailableFor: availableFor,
        //        WarehouseId: warehouseId,
        //        ShowAvailability: true,
        //        FromDate: fromDate,
        //        ShowImages: true,
        //        ToDate: toDate,
        //        SearchText: $popup.find('[data-datafield="SearchBox"] input.fwformfield-value').val()
        //    }

        //    FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/search", request, FwServices.defaultTimeout, function onSuccess(response) {
        //        $popup.find('.inventory').empty();
        //        SearchInterfaceController.renderInventory($popup, response, false);
        //    }, null, $searchpopup);

        //});

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
                        FromDate: fromDate,
                        ToDate: toDate,
                        ShowImages: true,
                        SearchText: $popup.find('[data-datafield="SearchBox"] input.fwformfield-value').val()
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
            FromDate: fromDate,
            ToDate: toDate,
            ShowImages: true
        }

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
                    $popup.find('#inventoryType').append('<ul style="cursor:pointer; padding:10px 10px 10px 15px; margin:1px;" data-value="' + response.Rows[i][inventoryTypeIdIndex] + '">' + response.Rows[i][inventoryTypeIndex] + '</ul>');
                }
            }
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
                        $popup.find('#category').append('<ul style="cursor:pointer; padding:10px 10px 10px 15px; margin:1px;" data-value="' + response.Rows[i][categoryIdIndex] + '">' + response.Rows[i][categoryIndex] + '</ul>');
                    }
                }
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

            //subCatListRequest.searchfieldoperators = ["<>"];
            //subCatListRequest.searchfields = ["Inactive"];
            //subCatListRequest.searchfieldvalues = ["T"];

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
                        $popup.find('#subCategory').append('<ul style="cursor:pointer; padding:10px 10px 10px 15px; margin:1px;" data-value="' + response.Rows[i][subCategoryIdIndex] + '">' + response.Rows[i][subCategoryIndex] + '</ul>');
                    }
                }

                let hasSubCategories = false;
                if (response.Rows.length > 0) {
                     hasSubCategories = true;
                }

                if (hasSubCategories == false) {
                    //load categories inventory
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

            FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/search", request, FwServices.defaultTimeout, function onSuccess(response) {
                $popup.find('.inventory').empty();
                SearchInterfaceController.renderInventory($popup, response, true);
            }, null, $searchpopup);
        });
    }

    renderInventory($popup, response, isSubCategory) {
        var descriptionIndex = response.ColumnIndex.Description,
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
            classificationIndex = response.ColumnIndex.Classification;

        if (response.Rows.length == 0) {
            $popup.find('.inventory').append('<span style="font-weight: bold; font-size=1.3em">No Results</span>');
        }

        for (var i = 0; i < response.Rows.length; i++) {

            var html = [];
            html.push('<div class="cardContainer">');
            html.push('<div class="card">');
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
                html.push('<div class="accContainer" style="float:left; width:90%; display:none">');

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
        }
        //var $inventory = $popup.find('div.cardContainer');
        var $inventory = $popup.find('div.card');

        var css = {
            'box-shadow': '0 4px 8px 0 rgba(0,0,0,0.2)',
            'transition': '0.3s'
        }
        $inventory.css(css);

        this.listGridView($inventory, 'gridView');
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
            case 'gridView':
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
            case 'listView':
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
            case 'listGridView':
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
                        $popup.find('#category').append('<ul style="cursor:pointer; padding:10px 10px 10px 15px; margin:1px;" data-value="' + response.Rows[i][categoryIdIndex] + '">' + response.Rows[i][categoryIndex] + '</ul>');
                    }
                }
            }, null, $searchpopup);
        })

        $popup.on('click', '#breadcrumbs .category', function (e) {
            $popup.find("#breadcrumbs .subcategory").empty();

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
        $popup.on('mouseenter', '.inventory > .card', function () {
            jQuery(this).css('box-shadow', '0 10px 18px 0 rgba(0, 0, 0, 0.2)');
        });

        $popup.on('mouseleave', '.inventory > .card', function (e) {
            var selected = jQuery(e.currentTarget).hasClass('selected');
            if (selected) {
                jQuery(e.currentTarget).css('box-shadow', '0 12px 20px 0 rgba(0,0,153,0.2)');
            } else {
                jQuery(e.currentTarget).css('box-shadow', '0 4px 8px 0 rgba(0,0,0,0.2)');
            }

        });

        $popup.on('click', '.inventory > .card', function (e) {
            $popup.find('.inventory > .card').removeClass('selected');
            $popup.find('.inventory > .card').css('box-shadow', '0 4px 8px 0 rgba(0,0,0,0.2)');

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
            FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/", request, FwServices.defaultTimeout, function onSuccess(response) {
                self.refreshPreviewGrid($popup, id);
            }, null, $searchpopup);

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
            self.listGridView($inventory, 'listView');
        });

        $popup.on('click', '.listgridbutton', function () {
            let $inventory = $popup.find('div.card');
            self.listGridView($inventory, 'listGridView');
        });

        $popup.on('click', '.gridbutton', function () {
            let $inventory = $popup.find('div.card');
            self.listGridView($inventory, 'gridView');
        });


        $popup.on('click', '.accList', function (e) {
            let accessoryContainer = jQuery(e.currentTarget).parents('.cardContainer').find('.accContainer');
            let cardContainer = jQuery(e.currentTarget).parents('.cardContainer').find('.card');
            let request: any = {};
            let inventoryId = jQuery(e.currentTarget).parents('.card').find('[data-datafield="InventoryId"] input').val();
            request.uniqueids = {
                PackageId: inventoryId
            };

            var html = [];
            if (!(accessoryContainer.find('.accItem').length)) {
                html.push('<div style="width:100%">');
                html.push(' <div class="accList" style="font-size: 1.2em; color: blue; text-align:center; text-decoration: underline">Accessories</div>');
                html.push('     <div style="width:50%; float:left;">Description</div>');
                html.push('     <div style="width:10%; float:left;"> Qty </div>');
                html.push('     <div style="width:10%; float:left;"> In </div>');
                html.push('     <div style="width:10%; float:left;"> Avail</div>');
                html.push('     <div style="width:10%; float:left;"> Conflict </div>');
                html.push('     <div style="width:10%; float:left;"> Note</div>');
                html.push(' </div>');
                html.push('</div>');
                accessoryContainer.append(html.join(''));

                FwAppData.apiMethod(true, 'POST', "api/v1/inventorypackageinventory/browse", request, FwServices.defaultTimeout, function onSuccess(response) {
                    const descriptionIndex = response.ColumnIndex.Description;
                    const qtyIndex = response.ColumnIndex.DefaultQuantity;
                    let accHtml = [];
                    //accHtml.push('<div >')

                    for (var i = 0; i < response.Rows.length; i++) {

                        accHtml.push('<div class="accItem" style="width:100%">');
                        accHtml.push('  <div style="float:left; width:50%">' + response.Rows[i][descriptionIndex] + '</div>');
                        accHtml.push('  <div style="float:left; width:10%">' + response.Rows[i][qtyIndex] + '</div>');
                        accHtml.push('  <div style="float:left; width:10%">' + response.Rows[i][qtyIndex] + '</div>');//placeholders
                        accHtml.push('  <div style="float:left; width:10%">' + response.Rows[i][qtyIndex] + '</div>');
                        accHtml.push('  <div style="float:left; width:10%">' + response.Rows[i][qtyIndex] + '</div>');
                        accHtml.push('  <div style="float:left; width:10%">' + response.Rows[i][qtyIndex] + '</div>');
                        accHtml.push('</div>');

                    }

                    accessoryContainer.append(accHtml.join(''));
                }, null, null);

                accessoryContainer.css({ 'float': 'left', 'height': 'auto', 'padding': '10px', 'margin-top': '20px', 'box-shadow': '0 4px 8px 0 rgba(0,0,0,0.2)', 'transition': '0.3s' });
                //cardContainer.slideToggle();
                accessoryContainer.slideToggle();
            } else {
                //cardContainer.slideToggle();
                accessoryContainer.slideToggle();
            }
        });



        //    $popup.on('click', '.preview', function () {
        //        //SearchInterfaceController.renderPreviewPopup($popup, id);
        //    });

    };

    refreshPreviewGrid($popup, id) {
        var previewrequest: any = {};
        var $searchpopup = jQuery('#searchpopup');
        var toDate = FwFormField.getValueByDataField($popup, 'ToDate');
        var fromDate = FwFormField.getValueByDataField($popup, 'FromDate');
        previewrequest = {
            SessionId: id,
            ShowAvailablity: true,
            FromDate: toDate,
            ShowImages: true,
            ToDate: fromDate
        };

        FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/preview", previewrequest, FwServices.defaultTimeout, function onSuccess(response) {
            var $grid = $popup.find('[data-name="SearchPreviewGrid"]');
            FwBrowse.databindcallback($grid, response);
        }, null, $searchpopup);
    };




    //renderPreviewPopup($popup, id) {
    //    var html = [];
    //    html.push('<div id="previewpopup" class="fwform" data-controller="none" style="background-color: white; box-shadow: 0 25px 44px rgba(0, 0, 0, 0.30), 0 20px 15px rgba(0, 0, 0, 0.22); width: 75vw; height: 75vh; overflow:scroll; position:relative;">');
    //    html.push('     <div class="fwmenu default" style="width:100%;height:7%; padding-left: 20px;">');
    //    html.push('     </div>');
    //    html.push('     <div class="formrow" style="width:100%; position:absolute;">');
    //    html.push('            <div>');
    //    html.push('                 <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
    //    html.push('                      <div data-control="FwGrid" data-grid="SearchPreviewGrid" data-securitycaption="Preview"></div>');
    //    html.push('                </div>');
    //    html.push('            </div>');
    //    html.push('     </div>');
    //    html.push('     <div class="close-modal" style="display:flex; position:absolute; top:10px; right:15px; cursor:pointer;"><i class="material-icons">clear</i><div class="btn-text">Close</div></div>');
    //    html.push('</div>');

    //    var $previewForm = html.join('');
    //    var $previewPopup = FwPopup.renderPopup($previewForm, { ismodal: true });
    //    FwPopup.showPopup($previewPopup);
    //    FwConfirmation.addControls($previewPopup, $previewForm);

    //    var $previewGrid;
    //    var $previewGridControl;
    //    $previewGrid = $previewPopup.find('[data-grid="SearchPreviewGrid"]');
    //    $previewGridControl = jQuery(jQuery('#tmpl-grids-SearchPreviewGridBrowse').html());
    //    $previewGrid.empty().append($previewGridControl);
    //    FwBrowse.init($previewGridControl);
    //    FwBrowse.renderRuntimeHtml($previewGridControl);
    //    var $grid = $previewPopup.find('[data-name="SearchPreviewGrid"]');

    //    var request: any = {};
    //    var toDate = FwFormField.getValueByDataField($popup, 'ToDate');
    //    var fromDate = FwFormField.getValueByDataField($popup, 'FromDate');
    //    request = {
    //        SessionId: id,
    //        ShowAvailablity: true,
    //        FromDate: toDate,
    //        ToDate: fromDate
    //    };

    //    var $preview = jQuery('#previewpopup');
    //    FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/preview", request, FwServices.defaultTimeout, function onSuccess(response) {
    //        FwBrowse.databindcallback($grid, response);
    //    }, null, $preview);

    //    $previewPopup.find('.close-modal').on('click', function (e) {
    //        FwPopup.destroyPopup($previewPopup);
    //        $previewPopup.off('click');
    //    });
    //}
}

var SearchInterfaceController = new SearchInterface();