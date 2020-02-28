routes.push({ pattern: /^module\/inventorysequenceutility$/, action: function (match: RegExpExecArray) { return InventorySequenceUtilityController.getModuleScreen(); } });

class InventorySequenceUtility {
    Module: string = 'InventorySequenceUtility';
    apiurl: string = 'api/v1/inventorysequenceutility';
    caption: string = Constants.Modules.Utilities.children.InventorySequenceUtility.caption;
    nav: string = Constants.Modules.Utilities.children.InventorySequenceUtility.nav;
    id: string = Constants.Modules.Utilities.children.InventorySequenceUtility.id;
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen = () => {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $form = this.openForm('EDIT');

        screen.load = () => {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
            this.afterLoad($form);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        //disables asterisk and save prompt
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');

        FwFormField.loadItems($form.find('div[data-datafield="InventoryType"]'), [
            { value: 'R', caption: 'Rental', checked: 'checked' },
            { value: 'S', caption: 'Sales' },
            { value: 'L', caption: 'Labor' },
            { value: 'M', caption: 'Misc' },
        ]);

        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    events($form) {
        const $categoryGrid = $form.find('[data-name="CategoryGrid"]');
        const $subCategoryGrid = $form.find('[data-name="SubCategoryGrid"]');
        const $inventoryTypeGrid = $form.find('[data-name="InventoryTypeGrid"]');

        // ---------- 
        $form.find('div[data-datafield="InventoryType"]').on('change', e => {
            const inventoryType = FwFormField.getValueByDataField($form, 'InventoryType');
            let type;
            switch (inventoryType) {
                case 'R':
                    type = 'Rental';
                    break;
                case 'S':
                    type = 'Sales';
                    break;
                case 'L':
                    type = 'Labor';
                    break;
                case 'M':
                    type = 'Misc';
                    break;
            }

            $inventoryTypeGrid.data('ondatabind', request => {
                request.uniqueids[type] = true;
                request.pagesize = 20;
            })
            FwBrowse.search($inventoryTypeGrid);

            $categoryGrid.data('ondatabind', request => {
                request.uniqueids[type] = true;
                request.pagesize = 20;
            })
            FwBrowse.search($categoryGrid);

            $subCategoryGrid.data('ondatabind', request => {
                request.uniqueids[type] = true;
                request.pagesize = 20;
            })
            FwBrowse.search($subCategoryGrid);
        });
        // InventoryType Grid
        // ---------- 
        $inventoryTypeGrid.data('onafterrowsort', ($control: JQuery, $tr: JQuery) => {
            try {
                const inventoryTypeId = jQuery($tr.find('.column > .field')[0]).attr('data-originalvalue');
                // Category
                $categoryGrid.data('ondatabind', request => {
                    request.uniqueids = {
                        InventoryTypeId: inventoryTypeId,
                    }
                    request.pagesize = 20;
                })
                $categoryGrid.data('beforesave', request => {
                    request.InventoryTypeId = inventoryTypeId;
                });
                FwBrowse.search($categoryGrid);
                // Sub-Category
                $subCategoryGrid.data('ondatabind', request => {
                    request.uniqueids = {
                        InventoryTypeId: inventoryTypeId,
                    }
                    request.pagesize = 20;
                })
                $subCategoryGrid.data('beforesave', request => {
                    request.InventoryTypeId = inventoryTypeId;
                });
                FwBrowse.search($subCategoryGrid);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $inventoryTypeGrid.data('onselectedrowchanged', ($control: JQuery, $tr: JQuery) => {
            try {
                const inventoryTypeId = jQuery($tr.find('.column > .field')[0]).attr('data-originalvalue');
                let here;
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        // Category Grid
        // ---------- 
        $categoryGrid.data('onafterrowsort', ($control: JQuery, $tr: JQuery) => {
            try {
                const categoryId = jQuery($tr.find('.column > .field')[0]).attr('data-originalvalue');

                $subCategoryGrid.data('ondatabind', request => {
                    request.uniqueids = {
                        CategoryId: categoryId,
                    }
                    request.pagesize = 20;
                })
                $subCategoryGrid.data('beforesave', request => {
                    request.CategoryId = categoryId;
                });
                FwBrowse.search($subCategoryGrid);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        // ----------
        $categoryGrid.data('onselectedrowchanged', ($control: JQuery, $tr: JQuery) => {
            try {
                const categoryId = jQuery($tr.find('.column > .field')[0]).attr('data-originalvalue');
                let here;
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
        const $inventoryTypeGrid = $form.find('[data-name="InventoryTypeGrid"]');
        const $categoryGrid = $form.find('[data-name="CategoryGrid"]');
        const $subCategoryGrid = $form.find('[data-name="SubCategoryGrid"]');
        FwBrowse.search($inventoryTypeGrid)
            .then(() => {
                const inventoryTypeId = $inventoryTypeGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                $categoryGrid.data('ondatabind', request => {
                    request.uniqueids = {
                        InventoryTypeId: inventoryTypeId,
                    }
                    request.pagesize = 20;
                })
                FwBrowse.search($categoryGrid)
                    .then(() => {
                        const categoryId = $categoryGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');

                        $subCategoryGrid.data('ondatabind', request => {
                            request.uniqueids = {
                                CategoryId: categoryId,
                            }
                            request.pagesize = 20;
                        })
                        FwBrowse.search($subCategoryGrid);
                    })
            })
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        FwBrowse.renderGrid({
            nameGrid: 'InventoryTypeGrid',
            gridSecurityId: 'aFLFxVNukHJt',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasEdit = false;
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    //  InventoryTypeId: FwFormField.getValueByDataField($form, 'InventoryTypeId'),
                    Rental: true,
                };
                request.pagesize = 20;
            },
            beforeSave: (request: any) => {
                //   request.InventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
            }
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'CategoryGrid',
            gridSecurityId: 'pWsHOgp1o7Obw',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasEdit = false;
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    //   CategoryId: FwFormField.getValueByDataField($form, 'CategoryId'),
                    Rental: true,
                };
                request.pagesize = 20;
            },
            beforeSave: (request: any) => {
                //  request.CategoryId = FwFormField.getValueByDataField($form, 'CategoryId');
            }
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'SubCategoryGrid',
            gridSecurityId: 'vHMa0l5PUysXo',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasEdit = false;
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    //     CategoryId: FwFormField.getValueByDataField($form, 'CategoryId'),
                    Rental: true,
                };
                request.pagesize = 20;
            },
            beforeSave: (request: any) => {
                //   request.CategoryId = FwFormField.getValueByDataField($form, 'CategoryId');
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
            <div id="inventorysequenceutilityform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Inventory Sequence Utility" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="InventorySequenceUtilityController">
              <div id="inventorysequenceutilityform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
                <div class="tabs"></div>
                <div class="tabpages">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Inventory Sequence Utility" style="max-width:700px">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="Type" data-datafield="InventoryType"></div>
                    </div>
                  </div>
                  <div class="flexrow" style="max-width:1300px;">
                    <div class="flexcolumn" style="flex:0 1 425px;">
                      <div data-control="FwGrid" data-grid="InventoryTypeGrid"></div>
                    </div>
                    <div class="flexcolumn" style="flex:0 1 425px;">
                      <div data-control="FwGrid" data-grid="CategoryGrid"></div>
                    </div>
                    <div class="flexcolumn" style="flex:0 1 425px;">
                      <div data-control="FwGrid" data-grid="SubCategoryGrid"></div>
                    </div>
                  </div>
                </div>
              </div>
            </div>`;
    }
    //----------------------------------------------------------------------------------------------
}
var InventorySequenceUtilityController = new InventorySequenceUtility();