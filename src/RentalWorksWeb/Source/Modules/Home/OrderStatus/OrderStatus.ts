declare var FwFormField;

class OrderStatus {
    Module: string;

    constructor() {
        this.Module = 'OrderStatus';
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('EDIT');

        screen.load = function () {
            FwModule.openModuleTab($form, 'Order Status', false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-OrderStatusForm').html());
        $form = FwModule.openForm($form, mode);

        $form.off('change keyup', '.fwformfield[data-isuniqueid!="true"][data-enabled="true"][data-datafield!=""]');

        this.getOrder($form);
        this.toggleView($form);

        $form.find('.rentalview').hide();
        $form.find('.salesview').hide();

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    getOrder($form: JQuery): void {
        var order = $form.find('[data-datafield="OrderId"]');
        order.on('change', function () {
            try {
                var orderId = $form.find('[data-datafield="OrderId"] .fwformfield-value').val();
                FwAppData.apiMethod(true, 'GET', "api/v1/order/" + orderId, null, FwServices.defaultTimeout, function onSuccess(response) {
                    FwFormField.setValueByDataField($form, 'Description', response.Description);
                    FwFormField.setValueByDataField($form, 'Deal', response.Deal);
                    FwFormField.setValueByDataField($form, 'Status', response.Status);
                    FwFormField.setValueByDataField($form, 'Warehouse', response.Warehouse);
                    FwFormField.setValueByDataField($form, 'PickDate', response.PickDate);
                    FwFormField.setValueByDataField($form, 'PickTime', response.PickTime);
                    FwFormField.setValueByDataField($form, 'EstimatedStartDate', response.EstimatedStartDate);
                    FwFormField.setValueByDataField($form, 'EstimatedStartTime', response.EstimatedStartTime);
                    FwFormField.setValueByDataField($form, 'EstimatedStopDate', response.EstimatedStopDate);
                    FwFormField.setValueByDataField($form, 'EstimatedStopTime', response.EstimatedStopTime);

                    var rental = response.Rental;
                    var sales = response.Sales;
                    if (rental === false && sales === false) {
                        $form.find('div[data-value="Details"]').hide();
                    } else {
                        $form.find('div[data-value="Details"]').show();
                    }

                    if (rental === true) {
                        $form.find('.rentalview').show();
                    } else {
                        $form.find('.rentalview').hide();
                    }

                    if (sales === true) {
                        $form.find('.salesview').show();
                    } else {
                        $form.find('.salesview').hide();
                    }

                    $form.find('.details').hide();
                });

                var $orderStatusSummaryGridControl: any;
                $orderStatusSummaryGridControl = $form.find('[data-name="OrderStatusSummaryGrid"]');
                $orderStatusSummaryGridControl.data('ondatabind', function (request) {
                    request.uniqueids = {
                        OrderId: orderId
                    }
                })
                FwBrowse.search($orderStatusSummaryGridControl);

                var $orderStatusRentalDetailGridControl: any;
                $orderStatusRentalDetailGridControl = $form.find('[data-name="OrderStatusRentalDetailGrid"]');
                $orderStatusRentalDetailGridControl.data('ondatabind', function (request) {
                    request.uniqueids = {
                        OrderId: orderId,
                        RecType: "R"
                    }
                })
                FwBrowse.search($orderStatusRentalDetailGridControl);

                var $orderStatusSalesDetailGridControl: any;
                $orderStatusSalesDetailGridControl = $form.find('[data-name="OrderStatusSalesDetailGrid"]');
                $orderStatusSalesDetailGridControl.data('ondatabind', function (request) {
                    request.uniqueids = {
                        OrderId: orderId,
                        RecType: "S"
                    }
                })
                FwBrowse.search($orderStatusSalesDetailGridControl);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        var $orderStatusSummaryGrid: any;
        var $orderStatusSummaryGridControl: any;
        var orderId = $form.find('[data-datafield="OrderId"] .fwformfield-value').val();


        $orderStatusSummaryGrid = $form.find('div[data-grid="OrderStatusSummaryGrid"]');
        $orderStatusSummaryGridControl = jQuery(jQuery('#tmpl-grids-OrderStatusSummaryGridBrowse').html());
        $orderStatusSummaryGrid.empty().append($orderStatusSummaryGridControl);
        $orderStatusSummaryGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: orderId
            };
        })
        FwBrowse.init($orderStatusSummaryGridControl);
        FwBrowse.renderRuntimeHtml($orderStatusSummaryGridControl);
        this.addLegend($form, $orderStatusSummaryGrid);

        var $orderStatusRentalDetailGrid: any;
        var $orderStatusRentalDetailGridControl: any;
        $orderStatusRentalDetailGrid = $form.find('div[data-grid="OrderStatusRentalDetailGrid"]');
        $orderStatusRentalDetailGridControl = jQuery(jQuery('#tmpl-grids-OrderStatusRentalDetailGridBrowse').html());
        $orderStatusRentalDetailGrid.empty().append($orderStatusRentalDetailGridControl);
        $orderStatusRentalDetailGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: orderId,
                RecType: "R"
            };
        })
        FwBrowse.init($orderStatusRentalDetailGridControl);
        FwBrowse.renderRuntimeHtml($orderStatusRentalDetailGridControl);
        this.addLegend($form, $orderStatusRentalDetailGrid);

        var $orderStatusSalesDetailGrid: any;
        var $orderStatusSalesDetailGridControl: any;
        $orderStatusSalesDetailGrid = $form.find('div[data-grid="OrderStatusSalesDetailGrid"]');
        $orderStatusSalesDetailGridControl = jQuery(jQuery('#tmpl-grids-OrderStatusSalesDetailGridBrowse').html());
        $orderStatusSalesDetailGrid.empty().append($orderStatusSalesDetailGridControl);
        $orderStatusSalesDetailGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                OrderId: orderId,
                RecType: "S"
            };
        })
        FwBrowse.init($orderStatusSalesDetailGridControl);
        FwBrowse.renderRuntimeHtml($orderStatusSalesDetailGridControl);
        this.addLegend($form, $orderStatusSalesDetailGrid);

        var $filter = $form.find('.filter[data-type="radio"]');
        $filter.on("change", function () {
            var orderId = $form.find('[data-datafield="OrderId"] .fwformfield-value').val();
            var filterValue = $form.find('.filter input[type="radio"]:checked').val().toUpperCase();

            $orderStatusSummaryGridControl.data('ondatabind', function (request) {
                request.uniqueids = {
                    OrderId: orderId
                };
                request.filterfields = {
                    Status: filterValue
                }
            })
            FwBrowse.search($orderStatusSummaryGridControl);

            $orderStatusRentalDetailGridControl.data('ondatabind', function (request) {
                request.uniqueids = {
                    OrderId: orderId,
                    RecType: "R"
                };
                request.filterfields = {
                    Status: filterValue
                }
            })
            FwBrowse.search($orderStatusRentalDetailGridControl);

            $orderStatusSalesDetailGridControl.data('ondatabind', function (request) {
                request.uniqueids = {
                    OrderId: orderId,
                    RecType: "S"
                };
                request.filterfields = {
                    Status: filterValue
                }
            })
            FwBrowse.search($orderStatusSalesDetailGridControl);

        });

        var $filterValidations = $form.find('#filters [data-type="validation"] input.fwformfield-value');

        $filterValidations.on("change", function () {
            var orderId = $form.find('[data-datafield="OrderId"] .fwformfield-value').val();
 
            var validationName = jQuery(jQuery(this).closest('[data-type="validation"]')).attr('data-validationname');

            var InventoryTypeId = $form.find('[data-type="validation"][data-datafield="InventoryTypeId"] input.fwformfield-value').val();
            var WarehouseId = $form.find('[data-type="validation"][data-datafield="WarehouseId"] input.fwformfield-value').val();
            var CategoryId = $form.find('[data-type="validation"][data-datafield="CategoryId"] input.fwformfield-value').val();
            var ICode = $form.find('[data-type="validation"][data-datafield="ICode"] input.fwformfield-value').val();
            var SubCategoryId = $form.find('[data-type="validation"][data-datafield="SubCategoryId"] input.fwformfield-value').val();

            $orderStatusSummaryGridControl.data('ondatabind', function (request) {
                request.uniqueids = {
                    OrderId: orderId
                };

                if (InventoryTypeId !== "") {
                    var invObj = { InventoryTypeId: InventoryTypeId }
                }
                if (WarehouseId !== "") {
                    var whObj = { WarehouseId: WarehouseId }
                }
                if (CategoryId !== "") {
                    var catObj = { CategoryId: CategoryId }
                }
                if (ICode !== "") {
                    var iObj = { ICode: ICode }
                }
                if (SubCategoryId !== "") {
                    var subObj = { SubCategoryId: SubCategoryId }
                }
                request.filterfields = jQuery.extend(invObj, whObj, catObj, iObj, subObj);

            })
            FwBrowse.search($orderStatusSummaryGridControl);

            $orderStatusRentalDetailGridControl.data('ondatabind', function (request) {
                request.uniqueids = {
                    OrderId: orderId,
                    RecType: "R"
                };
                if (InventoryTypeId !== "") {
                    var invObj = { InventoryTypeId: InventoryTypeId }
                }
                if (WarehouseId !== "") {
                    var whObj = { WarehouseId: WarehouseId }
                }
                if (CategoryId !== "") {
                    var catObj = { CategoryId: CategoryId }
                }
                if (ICode !== "") {
                    var iObj = { ICode: ICode }
                }
                if (SubCategoryId !== "") {
                    var subObj = { SubCategoryId: SubCategoryId }
                }
                request.filterfields = jQuery.extend(invObj, whObj, catObj, iObj, subObj);
            })
            FwBrowse.search($orderStatusRentalDetailGridControl);

            $orderStatusSalesDetailGridControl.data('ondatabind', function (request) {
                request.uniqueids = {
                    OrderId: orderId,
                    RecType: "S"
                };
                if (InventoryTypeId !== "") {
                    var invObj = { InventoryTypeId: InventoryTypeId }
                }
                if (WarehouseId !== "") {
                    var whObj = { WarehouseId: WarehouseId }
                }
                if (CategoryId !== "") {
                    var catObj = { CategoryId: CategoryId }
                }
                if (ICode !== "") {
                    var iObj = { ICode: ICode }
                }
                if (SubCategoryId !== "") {
                    var subObj = { SubCategoryId: SubCategoryId }
                }
                request.filterfields = jQuery.extend(invObj, whObj, catObj, iObj, subObj);
            })
            FwBrowse.search($orderStatusSalesDetailGridControl);
        });

        var $textFilter = $form.find('#filters [data-type="text"] input.fwformfield-value');
        $textFilter.on("blur", function () {
            var orderId = $form.find('[data-datafield="OrderId"] .fwformfield-value').val();

            var Description = $form.find('.textfilter[data-caption="Description"] input.fwformfield-value').val();
            var BarCode = $form.find('.textfilter[data-caption^="Bar Code"] input.fwformfield-value').val();

            $orderStatusSummaryGridControl.data('ondatabind', function (request) {
                request.uniqueids = {
                    OrderId: orderId
                };

                if (Description !== "" || null) {
                    request.searchfieldoperators.push("like");
                    request.searchfields.push("Description");
                    request.searchfieldvalues.push(Description);
                };

                //justin 02/11/2018 (commmented - Bar Code column not present in data set)
                //if (BarCode !== "" || null) {
                //    request.searchfieldoperators.push("like");
                //    request.searchfields.push("BarCode");
                //    request.searchfieldvalues.push(BarCode);
                //};


            })
            FwBrowse.search($orderStatusSummaryGridControl);

            $orderStatusRentalDetailGridControl.data('ondatabind', function (request) {
                request.uniqueids = {
                    OrderId: orderId,
                    RecType: "R"
                };
                if (Description !== "" || null) {
                    request.searchfieldoperators.push("like");
                    request.searchfields.push("Description");
                    request.searchfieldvalues.push(Description);
                };
                if (BarCode !== "" || null) {
                    request.searchfieldoperators.push("like");
                    //request.searchfields.push("BarCode");
                    request.searchfields.push("BarCodeSerialRfid");  //justin 02/11/2018 replaced with correct field name
                    request.searchfieldvalues.push(BarCode);
                };


            })
            FwBrowse.search($orderStatusRentalDetailGridControl);

            $orderStatusSalesDetailGridControl.data('ondatabind', function (request) {
                request.uniqueids = {
                    OrderId: orderId,
                    RecType: "S"
                };
                if (Description !== "" || null) {
                    request.searchfieldoperators.push("like");
                    request.searchfields.push("Description");
                    request.searchfieldvalues.push(Description);
                };
                if (BarCode !== "" || null) {
                    request.searchfieldoperators.push("like");
                    //request.searchfields.push("BarCode");
                    request.searchfields.push("BarCodeSerialRfid");  //justin 02/11/2018 replaced with correct field name
                    request.searchfieldvalues.push(BarCode);
                };

            })
            FwBrowse.search($orderStatusSalesDetailGridControl);

        });
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {

    }
    //----------------------------------------------------------------------------------------------
    toggleView($form: any) {
        var $toggle = $form.find('.toggle[data-type="radio"]');
        $toggle.on("change", function () {
            var view = $form.find('.toggle input[type="radio"]:checked').val();
            switch (view) {
                case 'Summary':
                    $form.find('.summaryview').show();
                    $form.find('.details').hide();
                    break;
                case 'Details':
                    $form.find('.details').show();
                    $form.find('.summaryview').hide();
                    break;
            }

        });
    }
    //----------------------------------------------------------------------------------------------
    addLegend($form: any, $grid) {
        FwBrowse.addLegend($grid, 'Complete', '#8888ff');
        FwBrowse.addLegend($grid, 'Kit', '#03d337');
        FwBrowse.addLegend($grid, 'Exchange', '#a0cdb4');
        FwBrowse.addLegend($grid, 'Sub Vendor', '#ffb18c');
        FwBrowse.addLegend($grid, 'Consignor', '#8080ff');
        FwBrowse.addLegend($grid, 'Truck', '#ffff00');
        FwBrowse.addLegend($grid, 'Suspended', '#0000a0');
        FwBrowse.addLegend($grid, 'Lost', '#ff8080');
        FwBrowse.addLegend($grid, 'Sales', '#ff0080');
        FwBrowse.addLegend($grid, 'Not Yet Staged or Still Out', '#ff0000');
        FwBrowse.addLegend($grid, 'Too Many Staged', '#00ff80');
    }
    //----------------------------------------------------------------------------------------------

}
(window as any).OrderStatusController = new OrderStatus();