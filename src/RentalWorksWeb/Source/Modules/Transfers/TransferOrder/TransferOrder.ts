routes.push({ pattern: /^module\/transferorder$/, action: function (match: RegExpExecArray) { return TransferOrderController.getModuleScreen(); } });
//----------------------------------------------------------------------------------------------
class TransferOrder {
    Module: string = 'TransferOrder';
    apiurl: string = 'api/v1/transferorder';
    caption: string = Constants.Modules.Transfers.children.TransferOrder.caption;
    nav: string = Constants.Modules.Transfers.children.TransferOrder.nav;
    id: string = Constants.Modules.Transfers.children.TransferOrder.id;
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        options.hasDelete = false;
        FwMenu.addBrowseMenuButtons(options);

        //warehouse filter
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        const $userWarehouse: JQuery = FwMenu.generateDropDownViewBtn(warehouse.warehouse, true, warehouse.warehouseid);
        const $allWarehouses = FwMenu.generateDropDownViewBtn('ALL Warehouses', false, "ALL");

        if (typeof this.ActiveViewFields["WarehouseId"] == 'undefined') {
            this.ActiveViewFields.WarehouseId = [warehouse.warehouseid];
        }

        let viewLocation: Array<JQuery> = [];
        viewLocation.push($userWarehouse, $allWarehouses);
        FwMenu.addViewBtn(options.$menu, 'Warehouse', viewLocation, true, "WarehouseId");

        const location = JSON.parse(sessionStorage.getItem('location'));
        const $userLoc: JQuery = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);
        const $allLoc = FwMenu.generateDropDownViewBtn('ALL', false, "ALL");

        if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
            this.ActiveViewFields.LocationId = [location.locationid];
        }

        let view: Array<JQuery> = [];
        view.push($userLoc, $allLoc);
        FwMenu.addViewBtn(options.$menu, 'Location', view, true, "LocationId");
    }
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        FwMenu.addFormMenuButtons(options);

        FwMenu.addSubMenuItem(options.$groupOptions, 'QuikSearch', '', (e: JQuery.ClickEvent) => {
            try {
                this.Search(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Confirm', 'VHP1qrNmwB4', (e: JQuery.ClickEvent) => {
            try {
                this.ConfirmTransfer(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Create Pick List', '', (e: JQuery.ClickEvent) => {
            try {
                this.CreatePickList(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Transfer Status', '', (e: JQuery.ClickEvent) => {
            try {
                this.TransferStatus(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Transfer Out', '', (e: JQuery.ClickEvent) => {
            try {
                this.TransferOut(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Transfer In', '', (e: JQuery.ClickEvent) => {
            try {
                this.TransferIn(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: any) {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};
        const $browse = this.openBrowse();
        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };
        return screen;
    };
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        $browse.data('ondatabind', request => {
            request.activeviewfields = this.ActiveViewFields;
        });

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode, parentModuleInfo?: any) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        FwFormField.loadItems($form.find('.outtype'), [
            { value: 'DELIVER', text: 'Deliver' },
            { value: 'SHIP', text: 'Ship' },
            { value: 'PICK UP', text: 'Pick Up' }
        ], true);

        if (mode === 'NEW') {
            const department = JSON.parse(sessionStorage.getItem('department'));
            FwFormField.setValueByDataField($form, 'DepartmentId', department.departmentid, department.department);

            const userId = sessionStorage.getItem('usersid');
            const userName = sessionStorage.getItem('name');
            FwFormField.setValueByDataField($form, 'AgentId', userId, userName);

            const today = FwFunc.getDate();
            FwFormField.setValueByDataField($form, 'PickDate', today);
            FwFormField.setValueByDataField($form, 'ShipDate', today);

            $form.find('[data-datafield="Rental"] input').prop('checked', true);

            const location = JSON.parse(sessionStorage.getItem('location'));
            FwFormField.setValueByDataField($form, 'OfficeLocationId', location.locationid, location.location);

            $form.find('.manifestSubModule').append(this.openManifestBrowse($form));
            $form.find('.receiptSubModule').append(this.openReceiptBrowse($form));
            $form.find('.picklistSubModule').append(this.openPickListBrowse($form));
        }

        FwFormField.setValue($form, 'div[data-datafield="ShowShipping"]', true);  //justin hoffman 03/12/2020 - this is temporary until the Shipping tab is updated

        this.events($form);
        this.renderSearchButton($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any): JQuery {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="TransferId"] input').val(uniqueids.TransferId);
        FwModule.loadForm(this.Module, $form);

        $form.find('.manifestSubModule').append(this.openManifestBrowse($form));
        $form.find('.receiptSubModule').append(this.openReceiptBrowse($form));
        $form.find('.picklistSubModule').append(this.openPickListBrowse($form));

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    renderSearchButton($form: any) {
        const $search = FwMenu.addStandardBtn($form.find('.fwmenu:first'), 'QuikSearch', 'searchbtn');
        $search.prepend('<i class="material-icons">search</i>');
        $search.on('click', () => {
            try {
                this.Search($form);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    Search($form: JQuery) {
        try {
            const transferId = FwFormField.getValueByDataField($form, 'TransferId');
            if ($form.attr('data-mode') === 'NEW') {
                TransferOrderController.saveForm($form, { closetab: false });
                return;
            }
            if (transferId == "") {
                FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
            } else {
                const search = new SearchInterface();
                search.renderSearchPopup($form, transferId, 'Transfer');
            }
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    ConfirmTransfer($form: JQuery) {
        try {
            const status = FwFormField.getValueByDataField($form, 'Status');
            let action = 'Confirm';
            if (status === 'CONFIRMED') {
                action = 'Un-confirm';
            }

            const transferNumber = FwFormField.getValueByDataField($form, `TransferNumber`);
            const $confirmation = FwConfirmation.renderConfirmation(`${action} Transfer`, '');
            const html = `<div class="fwform" data-controller="none" style="background-color: transparent;">
                            <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                              <div>${action} Transfer Number ${transferNumber}?</div>
                            </div>
                          </div>`;
            FwConfirmation.addControls($confirmation, html);
            const $yes = FwConfirmation.addButton($confirmation, `${action}`, false);
            const $no = FwConfirmation.addButton($confirmation, 'Cancel');

            $yes.on('click', e => {
                const topLayer = '<div class="top-layer" data-controller="none" style="background-color: transparent;z-index:1"></div>';
                const $realConfirm = jQuery($confirmation.find('.fwconfirmationbox')).prepend(topLayer);

                const transferId = FwFormField.getValueByDataField($form, 'TransferId');
                FwAppData.apiMethod(true, 'POST', `api/v1/transferorder/confirm/${transferId}`, null, FwServices.defaultTimeout, response => {
                    FwNotification.renderNotification('SUCCESS', `Transfer Order Successfully ${action}ed.`);
                    FwConfirmation.destroyConfirmation($confirmation);
                    FwModule.refreshForm($form);
                }, null, $realConfirm);
            });
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    CreatePickList($form: JQuery) {
        try {
            const mode = 'EDIT';
            const orderInfo: any = {};
            orderInfo.Type = 'Transfer';
            orderInfo.OrderId = FwFormField.getValueByDataField($form, 'TransferId');
            const $pickListForm = CreatePickListController.openForm(mode, orderInfo);
            FwModule.openSubModuleTab($form, $pickListForm);
            const $tabPage = FwTabs.getTabPageByElement($pickListForm);
            const $tab = FwTabs.getTabByElement(jQuery($tabPage));
            $tab.find('.caption').html('New Pick List');
            const $pickListUtilityGrid = $pickListForm.find('[data-name="PickListUtilityGrid"]');
            FwBrowse.search($pickListUtilityGrid);
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    TransferStatus($form: JQuery) {
        try {
            const mode = 'EDIT';
            const transferInfo: any = {};
            transferInfo.OrderId = FwFormField.getValueByDataField($form, 'TransferId');
            transferInfo.OrderNumber = FwFormField.getValueByDataField($form, 'TransferNumber');
            transferInfo.Type = 'Transfer';
            const $transferStatusForm = TransferStatusController.openForm(mode, transferInfo);
            FwModule.openSubModuleTab($form, $transferStatusForm);
            const $tabPage = FwTabs.getTabPageByElement($transferStatusForm);
            const $tab = FwTabs.getTabByElement(jQuery($tabPage));
            $tab.find('.caption').html('Transfer Status');
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    TransferOut($form: JQuery) {
        try {
            const mode = 'EDIT';
            const orderInfo: any = {};
            orderInfo.TransferId = FwFormField.getValueByDataField($form, 'TransferId');
            orderInfo.WarehouseId = FwFormField.getValueByDataField($form, 'FromWarehouseId');
            // orderInfo.Warehouse = FwFormField.getValueByDataField($form, 'FromWarehouseId');
            orderInfo.TransferNumber = FwFormField.getValueByDataField($form, 'TransferNumber');
            const $stagingCheckoutForm = TransferOutController.openForm(mode, orderInfo);
            FwModule.openSubModuleTab($form, $stagingCheckoutForm);
            const $tabPage = FwTabs.getTabPageByElement($stagingCheckoutForm);
            const $tab = FwTabs.getTabByElement(jQuery($tabPage));
            $tab.find('.caption').html('Transfer Out');
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    TransferIn($form: JQuery) {
        try {
            const mode = 'EDIT';
            const orderInfo: any = {};
            orderInfo.TransferId = FwFormField.getValueByDataField($form, 'TransferId');
            orderInfo.TransferNumber = FwFormField.getValueByDataField($form, 'TransferNumber');
            const $checkinForm = TransferInController.openForm(mode, orderInfo);
            FwModule.openSubModuleTab($form, $checkinForm);
            const $tabPage = FwTabs.getTabPageByElement($checkinForm);
            const $tab = FwTabs.getTabByElement(jQuery($tabPage));
            $tab.find('.caption').html('Transfer In');
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    openPickListBrowse($form) {
        const $browse = PickListController.openBrowse();
        $browse.data('ondatabind', request => {
            request.activeviewfields = PickListController.ActiveViewFields;
            request.uniqueids = {
                OrderId: FwFormField.getValueByDataField($form, 'TransferId')
            }
        });

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    openManifestBrowse($form) {
        const $browse = ManifestController.openBrowse();
        $browse.data('ondatabind', request => {
            request.activeviewfields = ManifestController.ActiveViewFields;
            request.uniqueids = {
                ContractType: "MANIFEST"
                , TransferId: FwFormField.getValueByDataField($form, 'TransferId')
            };
        });
        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openReceiptBrowse($form) {
        const $browse = TransferReceiptController.openBrowse();
        $browse.data('ondatabind', request => {
            request.activeviewfields = TransferReceiptController.ActiveViewFields;
            request.uniqueids = {
                ContractType: "RECEIPT"
                , TransferId: FwFormField.getValueByDataField($form, 'TransferId')
            };
        });
        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: JQuery, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    };
    //----------------------------------------------------------------------------------------------
    renderGrids($form) {
        // ----------
        //const $picklistGrid = $form.find('div[data-grid="OrderPickListGrid"]');
        //const $picklistGridControl = FwBrowse.loadGridFromTemplate('OrderPickListGrid');
        //$picklistGrid.empty().append($picklistGridControl);
        //$picklistGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        OrderId: FwFormField.getValueByDataField($form, 'TransferId')
        //    };
        //});
        //FwBrowse.init($picklistGridControl);
        //FwBrowse.renderRuntimeHtml($picklistGridControl);

        //justin hoffman 12/07/2019 grid was replaced by Sub Module, this code is obsolete
        //FwBrowse.renderGrid({
        //    nameGrid: 'OrderPickListGrid',
        //    gridSecurityId: 'bggVQOivrIgi',
        //    moduleSecurityId: this.id,
        //    $form: $form,
        //    onDataBind: (request: any) => {
        //        request.uniqueids = {
        //            OrderId: FwFormField.getValueByDataField($form, 'TransferId')
        //        };
        //    }
        //});
        // ----------
        //const $orderItemRentalGrid = $form.find('.rentalItemGrid div[data-grid="TransferOrderItemGrid"]');
        //const $orderItemRentalGridControl = FwBrowse.loadGridFromTemplate('TransferOrderItemGrid');
        //$orderItemRentalGrid.empty().append($orderItemRentalGridControl);
        //$orderItemRentalGrid.addClass('R');
        //$orderItemRentalGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        OrderId: FwFormField.getValueByDataField($form, 'TransferId')
        //        , RecType: 'R'
        //    };
        //});
        //$orderItemRentalGridControl.data('beforesave', request => {
        //    request.OrderId = FwFormField.getValueByDataField($form, 'TransferId');
        //    request.RecType = 'R';
        //});
        //FwBrowse.init($orderItemRentalGridControl);
        //FwBrowse.renderRuntimeHtml($orderItemRentalGridControl);
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'TransferOrderItemGrid',
            gridSelector: '.rentalItemGrid div[data-grid="TransferOrderItemGrid"]',
            gridSecurityId: 'RFgCJpybXoEb',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = true;
                options.hasEdit = true;
                options.hasDelete = true;
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1')
                FwMenu.addSubMenuItem($optionsgroup, 'QuikSearch', '', (e: JQuery.ClickEvent) => {
                    try {
                        this.Search($form);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Copy Template', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.copyTemplate(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, 'TransferId')
                    , RecType: 'R'
                };
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, 'TransferId');
                request.RecType = 'R';
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('R');
            },
        });
        // ----------
        //const $orderItemSalesGrid = $form.find('.salesItemGrid div[data-grid="TransferOrderItemGrid"]');
        //const $orderItemSalesGridControl = FwBrowse.loadGridFromTemplate('TransferOrderItemGrid');
        //$orderItemSalesGrid.empty().append($orderItemSalesGridControl);
        //$orderItemSalesGrid.addClass('S');
        //$orderItemSalesGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        OrderId: FwFormField.getValueByDataField($form, 'TransferId')
        //        , RecType: 'S'
        //    };
        //});
        //$orderItemSalesGridControl.data('beforesave', request => {
        //    request.OrderId = FwFormField.getValueByDataField($form, 'TransferId');
        //    request.RecType = 'S';
        //});
        //FwBrowse.init($orderItemSalesGridControl);
        //FwBrowse.renderRuntimeHtml($orderItemSalesGridControl);
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'TransferOrderItemGrid',
            gridSelector: '.salesItemGrid div[data-grid="TransferOrderItemGrid"]',
            gridSecurityId: 'RFgCJpybXoEb',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = true;
                options.hasEdit = true;
                options.hasDelete = true;
                const $optionscolumn = FwMenu.addSubMenuColumn(options.$menu);
                const $optionsgroup = FwMenu.addSubMenuGroup($optionscolumn, 'Options', 'securityid1')
                FwMenu.addSubMenuItem($optionsgroup, 'QuikSearch', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.quikSearch(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwMenu.addSubMenuItem($optionsgroup, 'Copy Template', '', (e: JQuery.ClickEvent) => {
                    try {
                        OrderItemGridController.copyTemplate(e);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, 'TransferId')
                    , RecType: 'S'
                };
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, 'TransferId');
                request.RecType = 'S';
            },
            beforeInit: ($fwgrid: JQuery, $browse: JQuery) => {
                $fwgrid.addClass('S');
            },
        });

        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'ActivityGrid',
            gridSecurityId: 'hb52dbhX1mNLZ',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    OrderId: FwFormField.getValueByDataField($form, `TransferId`),
                    ShowShipping: FwFormField.getValueByDataField($form, 'ShowShipping')
                };
            },
            beforeSave: (request: any) => {
                request.OrderId = FwFormField.getValueByDataField($form, `TransferId`)
            },
        });
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form: JQuery) {
        $form.find('.submodule').show();
        const status = FwFormField.getValueByDataField($form, 'Status');

        if (status === 'CONFIRMED') {
            $form.find('div[data-securityid="A35F0AAD-81B5-4A0C-8970-D448A67D5A82"] .caption').text('Un-confirm');
        } else if (status !== 'NEW') {
            $form.find('div[data-securityid="A35F0AAD-81B5-4A0C-8970-D448A67D5A82"]').css({ 'pointer-events': 'none', 'color': '#E0E0E0' });
        }

        const $orderItemRentalGrid = $form.find('.rentalItemGrid [data-name="TransferOrderItemGrid"]');
        FwBrowse.search($orderItemRentalGrid);

        const $orderItemSalesGrid = $form.find('.salesItemGrid [data-name="TransferOrderItemGrid"]');
        FwBrowse.search($orderItemSalesGrid);

        const isRental = FwFormField.getValueByDataField($form, 'Rental');
        const rentalTab = $form.find('.rentalTab');
        isRental ? rentalTab.show() : rentalTab.hide();

        const isSales = FwFormField.getValueByDataField($form, 'Sales');
        const salesTab = $form.find('.salesTab');
        isSales ? salesTab.show() : salesTab.hide();

        //Load grids and browses upon clicking their tab
        $form.on('click', '[data-type="tab"][data-enabled!="false"]', e => {
            const tabname = jQuery(e.currentTarget).attr('id');
            const lastIndexOfTab = tabname.lastIndexOf('tab');
            const tabpage = `${tabname.substring(0, lastIndexOfTab)}tabpage${tabname.substring(lastIndexOfTab + 3)}`;

            const $gridControls = $form.find(`#${tabpage} [data-type="Grid"]`);
            if ($gridControls.length > 0) {
                for (let i = 0; i < $gridControls.length; i++) {
                    const $gridcontrol = jQuery($gridControls[i]);
                    FwBrowse.search($gridcontrol);
                }
            }

            const $browseControls = $form.find(`#${tabpage} [data-type="Browse"]`);
            if ($browseControls.length > 0) {
                for (let i = 0; i < $browseControls.length; i++) {
                    const $browseControl = jQuery($browseControls[i]);
                    FwBrowse.search($browseControl);
                }
            }
        });
    };
    //----------------------------------------------------------------------------------------------
    events($form: JQuery) {
        $form.on('change', '[data-datafield="Sales"] input', e => {
            const $this = jQuery(e.currentTarget);
            const salesTab = $form.find('.salesTab');
            $this.prop('checked') === true ? salesTab.show() : salesTab.hide();
        });

        //Activity Filters
        $form.on('change', '.activity-filters', e => {
            ActivityGridController.filterActivities($form);
        });
    };
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        const warehouse = FwFormField.getValueByDataField($form, 'FromWarehouseId');
        switch (datafield) {
            case 'DepartmentId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedepartment`);
                break;
            case 'FromWarehouseId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatefromwarehouse`);
                break;
            case 'ToWarehouseId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatetowarehouse`);
                break;
            case 'AgentId':
                request.uniqueids = {
                    WarehouseId: warehouse
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateagent`);
                break;
            case 'SendRequestId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatesendto`);
                break;
            case 'OutDeliveryCarrierId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateoutdeliverycarrier`);
                break;
            case 'OutDeliveryShipViaId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateoutdeliveryshipvia`);
                break;
            case 'OutDeliveryToCountryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateoutdeliverytocountry`);
                break;
        }
    }
};
//-----------------------------------------------------------------------------------------------------
var TransferOrderController = new TransferOrder();