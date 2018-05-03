class SearchInterface {
    renderSearchPopup($form, id) {
        var html = [];
        html.push('<div id="searchpopup" class="fwform" data-controller="none" style="background-color: white; box-shadow: 0 25px 44px rgba(0, 0, 0, 0.30), 0 20px 15px rgba(0, 0, 0, 0.22); width: 85vw; height: 85vh; overflow:scroll; position:relative;">');

        html.push('     <div id="breadcrumbs" class="fwmenu default" style="width:100%;height:5%; padding-left: 20px;">');
        html.push('         <div class="type" style="float:left; cursor: pointer; font-weight: bold;"></div>');
        html.push('         <div class="category" style="float:left; cursor: pointer; font-weight: bold;"></div>');
        html.push('         <div class="subcategory" style="float:left; cursor: pointer; font-weight: bold;"></div>');
        html.push('     </div>');

        html.push('     <div class="formrow" style="width:100%; position:absolute;">');
        html.push('              <div data-control="FwFormField" class="fwcontrol fwformfield" data-caption=" "  data-datafield="InventoryType" data-type="radio" style="width:30%; margin: 5px 0px 25px 35px; float:clear;">');
        html.push('                  <div data-value="R" data-caption="Rental" style="float:left; width:20%;"></div>');
        html.push('                  <div data-value="S" data-caption="Sales" style="float:left; width:20%;"></div>');
        html.push('                  <div data-value="L" data-caption="Labor" style="float:left; width:20%;"></div>');
        html.push('                  <div data-value="M" data-caption="Misc" style="float:left; width:20%;"></div>');
        html.push('                  <div data-value="P" data-caption="Parts" style="float:left; width:20%;"></div>');
        html.push('              </div>');

        html.push('              <div id="inventoryType" style="width:10%; margin: 5px 0px 0px 5px; float:left;">');
        html.push('              </div>');

        html.push('             <div id="category" style="width:10%; margin: 5px 0px 0px 5px; float:left;">');
        html.push('             </div>');

        html.push('             <div id="subCategory" style="width:10%; margin: 5px 0px 0px 5px; float:left;">');
        html.push('             </div>');

        html.push('            <div style="width:65%; position:absolute; left: 35%; right: 5%;">')
        html.push('                 <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Est. Start" data-datafield="FromDate" style="width:120px; float:left;"></div>');
        html.push('                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Est. Stop" data-datafield="ToDate" style="width:120px;float:left;"></div>');
        html.push('                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Select" data-datafield="" style="width:150px;float:left;"></div>');
        html.push('                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Sort By" data-datafield="" style="width:150px;float:left;"></div>');
        html.push('                      <div data-type="button" class="fwformcontrol preview" style="width:70px; float:left; margin:15px;">Preview</div>');
        html.push('                      <div data-type="button" class="fwformcontrol addToOrder" style="width:120px; float:left; margin:15px;">Add to Order</div>');
        html.push('                  </div>');
        html.push('                 <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Search" data-datafield="SearchBox" style="width:540px; float:left;"></div>');
        html.push('                      <div data-type="button" class="fwformcontrol listbutton" style="margin: 12px 6px 12px 60px; padding:0px 7px 0px 7px;"><i class="material-icons" style="margin-top: 5px;">&#xE8EE;</i></div>');
        html.push('                      <div data-type="button" class="fwformcontrol listgridbutton" style="margin: 12px 6px 12px 6px; padding:0px 7px 0px 7px;"><i class="material-icons" style="margin-top: 5px;">&#xE8EF;</i></div>');
        html.push('                      <div data-type="button" class="fwformcontrol gridbutton" style="margin: 12px 6px 12px 6px; padding:0px 7px 0px 7px;"><i class="material-icons" style="margin-top: 5px;">&#xE8F0;</i></div>');
        html.push('                 </div>');


        html.push('                 <div class="inventory" style="overflow:auto">');

        html.push('                 </div>');
        html.push('            </div>');

        html.push('     </div>');

        html.push('     <div class="close-modal" style="display:flex; position:absolute; top:10px; right:15px; cursor:pointer;"><i class="material-icons">clear</i><div class="btn-text">Close</div></div>');
        html.push('</div>');

        var $searchForm = html.join('');
        var $popup = FwPopup.renderPopup($searchForm, { ismodal: true });
        FwPopup.showPopup($popup);
        FwConfirmation.addControls($popup, $searchForm);

        $popup.find('.close-modal').one('click', function (e) {
            FwPopup.destroyPopup($popup);
            jQuery(document).find('.fwpopup').off('click');
            jQuery(document).off('keydown');
        });

        var startDate = FwFormField.getValueByDataField($form, 'EstimatedStartDate');
        FwFormField.setValueByDataField($popup, 'FromDate', startDate);

        var stopDate = FwFormField.getValueByDataField($form, 'EstimatedStopDate');
        FwFormField.setValueByDataField($popup, 'ToDate', stopDate);

        var warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');

        var inventoryTypeRequest: any = {};

        inventoryTypeRequest.uniqueids = {
            Rental: true
        }
        var availableFor = FwFormField.getValueByDataField($popup, 'InventoryType');
        $popup.find('[data-type="radio"]').on('change', function () {
            availableFor = $popup.find('[data-type="radio"] input:checked').val();

            switch (availableFor) {
                case 'R':
                    inventoryTypeRequest.uniqueids = {
                        Rental: true
                    }
                    break;
                case 'S':
                    inventoryTypeRequest.uniqueids = {
                        Sales: true
                    }
                    break;
                case 'L':
                    inventoryTypeRequest.uniqueids = {
                        Labor: true
                    }
                    break;
                case 'M':
                    inventoryTypeRequest.uniqueids = {
                        Misc: true
                    }
                    break;
                case 'P':
                    inventoryTypeRequest.uniqueids = {
                        Parts: true
                    }
                    break;
            }
        });

        var toDate = FwFormField.getValueByDataField($popup, 'ToDate');
        var fromDate = FwFormField.getValueByDataField($popup, 'FromDate');

        var request: any = {
            OrderId: id,
            SessionId: id,
            AvailableFor: availableFor,
            WarehouseId: warehouseId,
            ShowAvailability: true,
            FromDate: fromDate,
            ToDate: toDate
        }

        var $searchpopup = jQuery('#searchpopup');
        var $descriptionField = $popup.find('[data-datafield="SearchBox"] input.fwformfield-value');
        $descriptionField.on('blur', function () {

            var request: any = {
                OrderId: id,
                SessionId: id,
                AvailableFor: availableFor,
                WarehouseId: warehouseId,
                ShowAvailability: true,
                FromDate: fromDate,
                ToDate: toDate,
                SearchText: $popup.find('[data-datafield="SearchBox"] input.fwformfield-value').val()
            }

            FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/search", request, FwServices.defaultTimeout, function onSuccess(response) {
                $popup.find('.inventory').empty();
                SearchInterfaceController.renderInventory($popup, response, false);
            }, null, $searchpopup);

        });

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

        this.populateTypeMenu($popup, inventoryTypeRequest);
        this.typeOnClickEvents($popup, $form, request);
        this.categoryOnClickEvents($popup, $form, request);
        this.subCategoryOnClickEvents($popup, $form, request);
        this.breadCrumbs($popup, $form, request);
        this.events($popup, $form, id);
        return $popup;
    }

    populateTypeMenu($popup, request) {
        var $searchpopup = jQuery('#searchpopup');
        FwAppData.apiMethod(true, 'POST', "api/v1/inventoryType/browse", request, FwServices.defaultTimeout, function onSuccess(response) {
            var inventoryTypeIndex, inventoryTypeIdIndex;

            inventoryTypeIndex = response.ColumnIndex.InventoryType;
            inventoryTypeIdIndex = response.ColumnIndex.InventoryTypeId;

            for (var i = 0; i < response.Rows.length; i++) {
                $popup.find('#inventoryType').append('<ul style="cursor:pointer; padding:10px 10px 10px 15px; margin:1px;" data-value="' + response.Rows[i][inventoryTypeIdIndex] + '">' + response.Rows[i][inventoryTypeIndex] + '</ul>');

            }
        }, null, $searchpopup);
    }

    typeOnClickEvents($popup, $form, request) {
        var $searchpopup = jQuery('#searchpopup');
        $popup.on('click', '#inventoryType ul', function (e) {
            var invType, inventoryTypeId, breadcrumb;
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
            request.InventoryTypeId = inventoryTypeId;

            FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/search", request, FwServices.defaultTimeout, function onSuccess(response) {
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
        });

    }

    categoryOnClickEvents($popup, $form, request) {
        var $searchpopup = jQuery('#searchpopup');
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

            delete request.SubCategoryId;
            request.CategoryId = categoryId;
            request.InventoryTypeId = inventoryTypeId;

            FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/search", request, FwServices.defaultTimeout, function onSuccess(response) {
                $popup.find('#subCategory').empty();
                $popup.find('.inventory').empty();
                SearchInterfaceController.renderInventory($popup, response, false);
            }, null, $searchpopup);
        });
    };

    renderInventory($popup, response, isSubCategory) {
        var descriptionIndex = response.ColumnIndex.Description;
        var thumbnailIndex = response.ColumnIndex.Thumbnail;
        var quantityAvailable = response.ColumnIndex.QuantityAvailable;
        var conflictDate = response.ColumnIndex.ConflictDate;
        var quantityIn = response.ColumnIndex.QuantityIn;
        var quantityQcRequired = response.ColumnIndex.QuantityQcRequired;
        var quantity = response.ColumnIndex.Quantity;
        var dailyRate = response.ColumnIndex.DailyRate;
        var inventoryId = response.ColumnIndex.InventoryId;
        var thumbnail = response.ColumnIndex.Thumbnail;
        var appImageId = response.ColumnIndex.ImageId;
        var subCategoryIdIndex = response.ColumnIndex.SubCategoryId;
        var subCategoryIndex = response.ColumnIndex.SubCategory;

        let subCategories = [];
        for (var i = 0; i < response.Rows.length; i++) {
            if (!isSubCategory) {
                if (subCategories.indexOf(response.Rows[i][subCategoryIndex]) == -1) {
                    subCategories.push(response.Rows[i][subCategoryIndex]);
                    $popup.find('#subCategory').append('<ul style="cursor:pointer; padding:10px 10px 10px 15px; margin:1px;" data-value="' + response.Rows[i][subCategoryIdIndex] + '">' + response.Rows[i][subCategoryIndex] + '</ul>');
                }
            }

            var html = [];
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
            if (true) {
                html.push('<div class="accessories">Accessories</div>');
            }
            //else {
            //    html.push('<div style="padding: 10px 0 10px 0;"></div>');
            //}

            html.push('<div data-control="FwFormField" data-type="number" data-digits="2" data-datafield="DailyRate" data-caption="Rate" class="fwcontrol fwformfield rate" data-enabled="false"></div>');
            html.push('<div data-control="FwFormField" data-type="number" data-datafield="Quantity" data-caption="Qty" class="fwcontrol fwformfield"></div>');
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
        var $inventory = $popup.find('div.card');

        var css = {
            'box-shadow': '0 4px 8px 0 rgba(0,0,0,0.2)',
            'transition': '0.3s'
        }
        $inventory.css(css);

        this.listGridView($inventory, 'gridView');
    }

    listGridView($inventory, viewType) {
        var description = $inventory.find('.invdescription');
        var imageFrame = $inventory.find('.invimage');
        var image = $inventory.find('.image');
        var quantityAvailable = $inventory.find('[data-datafield="QuantityAvailable"]');
        var conflictDate = $inventory.find('[data-datafield="ConflictDate"]');
        var quantityIn = $inventory.find('[data-datafield="QuantityIn"]');
        var quantityQcRequired = $inventory.find('[data-datafield="QuantityQcRequired"]');
        var accessories = $inventory.find('.accessories');
        var rate = $inventory.find('[data-datafield="DailyRate"]');
        var quantity = $inventory.find('[data-datafield="Quantity"]');
        var allWH = $inventory.find('[data-datafield="AllWH"]');
        var descContainer = $inventory.find('.desccontainer');
        var quantityContainer = $inventory.find('.quantitycontainer');
        switch (viewType) {
            case 'gridView':
                allWH.hide();
                $inventory.css({ 'cursor': 'pointer', 'width': '225px', 'height': '300px', 'float': 'left', 'padding': '10px', 'margin': '8px' });
                descContainer.css({ 'width': '', 'float': '' });
                description.css({ 'height': '15%', 'width': '', 'padding-bottom': '5px', 'float': '' });
                imageFrame.show();
                imageFrame.css({ 'float': 'left', 'width': '125px', 'height': '175px', 'line-height': '175px', 'display': 'inline-block', 'position': 'relative' });
                image.css({'max-height': '100%', 'max-width': '100%', 'width': 'auto', 'height': 'auto', 'position': 'absolute', 'top': '0', 'bottom': '0', 'left': '0', 'right': '0', 'margin': 'auto'});
                quantityAvailable.css({ 'float': 'right', 'width': '90px' });
                conflictDate.css({ 'float': 'right', 'width': '90px' });
                quantityIn.css({ 'float': 'left', 'width': '45px' });
                quantityQcRequired.css({ 'float': 'right', 'width': '45px' });
                accessories.css({'float':'right', 'padding':'10px 5px 10px 0', 'font-size': '.9em', 'color':'blue'});
                rate.css({ 'float': 'left', 'padding-top':'20px', 'width': '90px' });
                quantity.css({ 'float': 'right', 'width': '90px' });
                quantityContainer.css({'float':'right'});
                //$inventory.removeClass('listView', 'listGridView');
                //$inventory.addClass('gridView');
                break;
            case 'listView':
                $inventory.css({ 'cursor': 'pointer', 'width': '95%', 'height': 'auto', 'float': 'left', 'padding': '10px', 'margin': '8px' });
                descContainer.css({'width':'','float':''});
                description.css({ 'float': 'left', 'padding-top': '15px', 'width': '35%' });
                imageFrame.hide();
                //image.css({ 'max-height': '100%', 'max-width': '100%', 'width': 'auto', 'height': 'auto', 'position': 'absolute', 'top': '0', 'bottom': '0', 'left': '0', 'right': '0', 'margin': 'auto' });
                quantityAvailable.css({ 'float': 'left', 'width': '75px' });
                conflictDate.css({ 'float': 'left', 'width': '90px' });
                allWH.show();
                allWH.css({ 'float': 'left', 'width': '75px' });
                quantityIn.css({ 'float': 'left', 'width': '45px' });
                quantityQcRequired.css({ 'float': 'left', 'width': '45px' });
                accessories.css({ 'float': 'left', 'padding': '20px 15px 10px 15px', 'font-size': '.9em', 'color': 'blue' });
                rate.css({ 'float': 'left', 'width': '90px', 'padding-top':'' });
                quantity.css({ 'float': 'right', 'width': '90px' });
                quantityContainer.css({ 'float': 'left' });
                //$inventory.removeClass('gridView', 'listGridView');
                //$inventory.addClass('listView');
                break;
            case 'listGridView':
                $inventory.css({ 'cursor': 'pointer', 'width': '95%', 'height': 'auto', 'float': 'left', 'padding': '10px', 'margin': '8px' });
                descContainer.css({ 'width': '40%', 'float': 'left' });
                description.css({ 'float': 'right', 'padding-top': '15px', 'width': '75%' });
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
                rate.css({ 'float': 'left', 'width': '90px', 'padding-top': '' });
                quantity.css({ 'float': 'right', 'width': '90px' });
                quantityContainer.css({ 'float': 'left' });
                //$inventory.removeClass('listView', 'gridView');
                //$inventory.addClass('listGridView');
                break;
        }


    };

    subCategoryOnClickEvents($popup, $form, request) {
        var $searchpopup = jQuery('#searchpopup');
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

    breadCrumbs($popup, $form, request) {
        var $searchpopup = jQuery('#searchpopup');

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
        $popup.on('mouseenter', '.inventory > div', function () {
            jQuery(this).css('box-shadow', '0 10px 18px 0 rgba(0, 0, 0, 0.2)');
        });

        $popup.on('mouseleave', '.inventory > div', function (e) {
            var selected = jQuery(e.currentTarget).hasClass('selected');
            if (selected) {
                jQuery(e.currentTarget).css('box-shadow', '0 12px 20px 0 rgba(0,0,153,0.2)');
            } else {
                jQuery(e.currentTarget).css('box-shadow', '0 4px 8px 0 rgba(0,0,0,0.2)');
            }

        });

        $popup.on('click', '.inventory > div', function (e) {
            $popup.find('.inventory > div').removeClass('selected');
            $popup.find('.inventory > div').css('box-shadow', '0 4px 8px 0 rgba(0,0,0,0.2)');

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

            }, null, $searchpopup);

        });

        $popup.on('click', '.image', function (e) {
            var $confirmation, $cancel;
            var image = jQuery(e.currentTarget).attr('src');
            var imageId = jQuery(e.currentTarget).attr('data-value');

            $confirmation = FwConfirmation.renderConfirmation('Image Viewer', '<div style="white-space:pre;">\n' +
                '<img src="' + image + '" data-value="' + imageId + '" alt="No Image" class="image" style="width:100%; height:100%;">');

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

        $popup.on('click', '.preview', function () {
            SearchInterfaceController.renderPreviewPopup($popup, id);

        });

    }

    renderPreviewPopup($popup, id) {
        var html = [];

        html.push('<div id="previewpopup" class="fwform" data-controller="none" style="background-color: white; box-shadow: 0 25px 44px rgba(0, 0, 0, 0.30), 0 20px 15px rgba(0, 0, 0, 0.22); width: 75vw; height: 75vh; overflow:scroll; position:relative;">');
        html.push('     <div class="fwmenu default" style="width:100%;height:7%; padding-left: 20px;">');
        html.push('     </div>');
        html.push('     <div class="formrow" style="width:100%; position:absolute;">');
        html.push('            <div>');
        html.push('                 <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">');
        html.push('                      <div data-control="FwGrid" data-grid="SearchPreviewGrid" data-securitycaption="Preview"></div>');
        html.push('                </div>');
        html.push('            </div>');
        html.push('     </div>');
        html.push('     <div class="close-modal" style="display:flex; position:absolute; top:10px; right:15px; cursor:pointer;"><i class="material-icons">clear</i><div class="btn-text">Close</div></div>');
        html.push('</div>');

        var $previewForm = html.join('');
        var $previewPopup = FwPopup.renderPopup($previewForm, { ismodal: true });
        FwPopup.showPopup($previewPopup);
        FwConfirmation.addControls($previewPopup, $previewForm);

        var $previewGrid;
        var $previewGridControl;
        $previewGrid = $previewPopup.find('[data-grid="SearchPreviewGrid"]');
        $previewGridControl = jQuery(jQuery('#tmpl-grids-SearchPreviewGridBrowse').html());
        $previewGrid.empty().append($previewGridControl);
        FwBrowse.init($previewGridControl);
        FwBrowse.renderRuntimeHtml($previewGridControl);
        var $grid = $previewPopup.find('[data-name="SearchPreviewGrid"]');

        var request: any = {};
        var toDate = FwFormField.getValueByDataField($popup, 'ToDate');
        var fromDate = FwFormField.getValueByDataField($popup, 'FromDate');
        request = {
            SessionId: id,
            ShowAvailablity: true,
            FromDate: toDate,
            ToDate: fromDate
        };

        var $preview = jQuery('#previewpopup');
        FwAppData.apiMethod(true, 'POST', "api/v1/inventorysearch/preview", request, FwServices.defaultTimeout, function onSuccess(response) {
            FwBrowse.databindcallback($grid, response);
        }, null, $preview);

        $previewPopup.find('.close-modal').on('click', function (e) {
            FwPopup.destroyPopup($previewPopup);
            $previewPopup.off('click');
        });
    }
}

var SearchInterfaceController = new SearchInterface();