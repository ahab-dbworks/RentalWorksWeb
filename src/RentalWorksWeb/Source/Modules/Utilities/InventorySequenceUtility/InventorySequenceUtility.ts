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

        FwFormField.loadItems($form.find('div[data-datafield="RecType"]'), [
            { value: 'R', caption: 'Rental', checked: 'checked' },
            { value: 'S', caption: 'Sales' },
            { value: 'L', caption: 'Labor' },
            { value: 'M', caption: 'Misc' },
        ]);

        $form.find('div[data-control="FwTabs"] .tabs').hide();
        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    events($form) {
        const $inventoryTypeGrid = $form.find('[data-name="InventorySequenceTypeGrid"]');
        const $categoryGrid = $form.find('[data-name="InventorySequenceCategoryGrid"]');
        const $subCategoryGrid = $form.find('[data-name="InventorySequenceSubCategoryGrid"]');
        const $itemsGrid = $form.find('[data-name="InventorySequenceItemsGrid"]');
        // ---------- 
        // Type Toggle selector
        $form.find('div[data-datafield="RecType"]').on('change', e => {
            $inventoryTypeGrid.data('ondatabind', request => {
                request.uniqueids[this.getInventoryType($form)] = true;
                request.pagesize = 9999;
                request.searchfieldoperators = ["<>"];
                request.searchfields = ["Inactive"];
                request.searchfieldvalues = ["T"];
            });
            FwBrowse.search($inventoryTypeGrid)
                .then(() => {
                    const inventoryTypeId = $inventoryTypeGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                    $categoryGrid.data('ondatabind', request => {
                        request.uniqueids = {
                            InventoryTypeId: inventoryTypeId,
                            RecType: FwFormField.getValueByDataField($form, 'RecType'),
                        }
                        request.pagesize = 9999;
                        request.searchfieldoperators = ["<>"];
                        request.searchfields = ["Inactive"];
                        request.searchfieldvalues = ["T"];
                    });
                    FwBrowse.search($categoryGrid)
                        .then(() => {
                            let categoryId = $categoryGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                            if (categoryId === undefined || categoryId === 'undefined' || categoryId === '') {
                                categoryId = 'NONE';
                            }
                            $subCategoryGrid.data('ondatabind', request => {
                                request.uniqueids = {
                                    CategoryId: categoryId,
                                }
                                request.pagesize = 9999;
                                request.searchfieldoperators = ["<>"];
                                request.searchfields = ["Inactive"];
                                request.searchfieldvalues = ["T"];
                            });
                            FwBrowse.search($subCategoryGrid)
                                .then(() => {
                                    let categoryId = $categoryGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                                    if (categoryId === undefined || categoryId === 'undefined' || categoryId === '') {
                                        categoryId = 'NONE';
                                    }
                                    const subCategoryId = $subCategoryGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                                    const inventoryTypeId = $inventoryTypeGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                                    $itemsGrid.data('ondatabind', request => {
                                        request.uniqueids = {
                                            InventoryTypeId: inventoryTypeId,
                                            CategoryId: categoryId,
                                            SubCategoryId: subCategoryId,
                                        }
                                        request.pagesize = 9999;
                                        request.searchfieldoperators = ["<>"];
                                        request.searchfields = ["Inactive"];
                                        request.searchfieldvalues = ["T"];
                                    });
                                    FwBrowse.search($itemsGrid);
                                });
                        });
                });
        });
        // InventoryType Grid
        // ---------- 
        $inventoryTypeGrid.data('onafterrowsort', ($control: JQuery, $tr: JQuery) => {
            try {
                const inventoryTypeId = $inventoryTypeGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                $categoryGrid.data('ondatabind', request => {
                    request.uniqueids = {
                        InventoryTypeId: inventoryTypeId,
                        RecType: FwFormField.getValueByDataField($form, 'RecType'),
                    }
                    request.pagesize = 9999;
                    request.searchfieldoperators = ["<>"];
                    request.searchfields = ["Inactive"];
                    request.searchfieldvalues = ["T"];
                });
                FwBrowse.search($categoryGrid)
                    .then(() => {
                        let categoryId = $categoryGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                        if (categoryId === undefined || categoryId === 'undefined' || categoryId === '') {
                            categoryId = 'NONE';
                        }
                        $subCategoryGrid.data('ondatabind', request => {
                            request.uniqueids = {
                                CategoryId: categoryId,
                            }
                            request.pagesize = 9999;
                            request.searchfieldoperators = ["<>"];
                            request.searchfields = ["Inactive"];
                            request.searchfieldvalues = ["T"];
                        });
                        FwBrowse.search($subCategoryGrid)
                            .then(() => {
                                let categoryId = $categoryGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                                if (categoryId === undefined || categoryId === 'undefined' || categoryId === '') {
                                    categoryId = 'NONE';
                                }
                                const subCategoryId = $subCategoryGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                                const inventoryTypeId = $inventoryTypeGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                                $itemsGrid.data('ondatabind', request => {
                                    request.uniqueids = {
                                        InventoryTypeId: inventoryTypeId,
                                        CategoryId: categoryId,
                                        SubCategoryId: subCategoryId,
                                    }
                                    request.pagesize = 9999;
                                    request.searchfieldoperators = ["<>"];
                                    request.searchfields = ["Inactive"];
                                    request.searchfieldvalues = ["T"];
                                });
                                FwBrowse.search($itemsGrid);
                            });
                    });
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $inventoryTypeGrid.data('onselectedrowchanged', ($control: JQuery, $tr: JQuery) => {
            try {
                const inventoryTypeId = jQuery($tr.find('.column > .field')[0]).attr('data-originalvalue');
                $categoryGrid.data('ondatabind', request => {
                    request.uniqueids = {
                        InventoryTypeId: inventoryTypeId,
                        RecType: FwFormField.getValueByDataField($form, 'RecType'),
                    }
                    request.pagesize = 9999;
                    request.searchfieldoperators = ["<>"];
                    request.searchfields = ["Inactive"];
                    request.searchfieldvalues = ["T"];
                });
                FwBrowse.search($categoryGrid)
                    .then(() => {
                        let categoryId = $categoryGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                        if (categoryId === undefined || categoryId === 'undefined' || categoryId === '') {
                            categoryId = 'NONE';
                        }
                        $subCategoryGrid.data('ondatabind', request => {
                            request.uniqueids = {
                                CategoryId: categoryId,
                            }
                            request.pagesize = 9999;
                            request.searchfieldoperators = ["<>"];
                            request.searchfields = ["Inactive"];
                            request.searchfieldvalues = ["T"];
                        });
                        FwBrowse.search($subCategoryGrid)
                            .then(() => {
                                let categoryId = $categoryGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                                if (categoryId === undefined || categoryId === 'undefined' || categoryId === '') {
                                    categoryId = 'NONE';
                                }
                                const subCategoryId = $subCategoryGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                                const inventoryTypeId = $inventoryTypeGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                                $itemsGrid.data('ondatabind', request => {
                                    request.uniqueids = {
                                        InventoryTypeId: inventoryTypeId,
                                        CategoryId: categoryId,
                                        SubCategoryId: subCategoryId,
                                    }
                                    request.pagesize = 9999;
                                    request.searchfieldoperators = ["<>"];
                                    request.searchfields = ["Inactive"];
                                    request.searchfieldvalues = ["T"];
                                });
                                FwBrowse.search($itemsGrid);
                            });
                    })
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        // Category Grid
        // ---------- 
        $categoryGrid.data('onafterrowsort', ($control: JQuery, $tr: JQuery) => {
            try {
                let categoryId = $inventoryTypeGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                if (categoryId === undefined || categoryId === 'undefined' || categoryId === '') {
                    categoryId = 'NONE';
                }
                $subCategoryGrid.data('ondatabind', request => {
                    request.uniqueids = {
                        CategoryId: categoryId,
                    }
                    request.pagesize = 9999;
                    request.searchfieldoperators = ["<>"];
                    request.searchfields = ["Inactive"];
                    request.searchfieldvalues = ["T"];
                });
                FwBrowse.search($subCategoryGrid)
                    .then(() => {
                        let categoryId = $categoryGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                        if (categoryId === undefined || categoryId === 'undefined' || categoryId === '') {
                            categoryId = 'NONE';
                        }
                        const subCategoryId = $subCategoryGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                        const inventoryTypeId = $inventoryTypeGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                        $itemsGrid.data('ondatabind', request => {
                            request.uniqueids = {
                                InventoryTypeId: inventoryTypeId,
                                CategoryId: categoryId,
                                SubCategoryId: subCategoryId,
                            }
                            request.pagesize = 9999;
                            request.searchfieldoperators = ["<>"];
                            request.searchfields = ["Inactive"];
                            request.searchfieldvalues = ["T"];
                        });
                        FwBrowse.search($itemsGrid);
                    });
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        // ----------
        $categoryGrid.data('onselectedrowchanged', ($control: JQuery, $tr: JQuery) => {
            try {
                let categoryId = jQuery($tr.find('.column > .field')[0]).attr('data-originalvalue');
                if (categoryId === undefined || categoryId === 'undefined' || categoryId === '') {
                    categoryId = 'NONE';
                }
                $subCategoryGrid.data('ondatabind', request => {
                    request.uniqueids = {
                        CategoryId: categoryId,
                    }
                    request.pagesize = 9999;
                    request.searchfieldoperators = ["<>"];
                    request.searchfields = ["Inactive"];
                    request.searchfieldvalues = ["T"];
                });
                FwBrowse.search($subCategoryGrid)
                    .then(() => {
                        let categoryId = $categoryGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                        if (categoryId === undefined || categoryId === 'undefined' || categoryId === '') {
                            categoryId = 'NONE';
                        }
                        const subCategoryId = $subCategoryGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                        const inventoryTypeId = $inventoryTypeGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                        $itemsGrid.data('ondatabind', request => {
                            request.uniqueids = {
                                InventoryTypeId: inventoryTypeId,
                                CategoryId: categoryId,
                                SubCategoryId: subCategoryId,
                            }
                            request.pagesize = 9999;
                            request.searchfieldoperators = ["<>"];
                            request.searchfields = ["Inactive"];
                            request.searchfieldvalues = ["T"];
                        });
                        FwBrowse.search($itemsGrid)
                    });
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        // Sub-Category Grid
        // ---------- 
        $subCategoryGrid.data('onafterrowsort', ($control: JQuery, $tr: JQuery) => {
            try {
                let categoryId = $categoryGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                if (categoryId === undefined || categoryId === 'undefined' || categoryId === '') {
                    categoryId = 'NONE';
                }
                const subCategoryId = $subCategoryGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                const inventoryTypeId = $inventoryTypeGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                $itemsGrid.data('ondatabind', request => {
                    request.uniqueids = {
                        InventoryTypeId: inventoryTypeId,
                        CategoryId: categoryId,
                        SubCategoryId: subCategoryId,
                    }
                    request.pagesize = 9999;
                    request.searchfieldoperators = ["<>"];
                    request.searchfields = ["Inactive"];
                    request.searchfieldvalues = ["T"];
                });
                FwBrowse.search($itemsGrid);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        // ----------
        $subCategoryGrid.data('onselectedrowchanged', ($control: JQuery, $tr: JQuery) => {
            try {
                let categoryId = $categoryGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                if (categoryId === undefined || categoryId === 'undefined' || categoryId === '') {
                    categoryId = 'NONE';
                }
                const subCategoryId = $subCategoryGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                const inventoryTypeId = $inventoryTypeGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                $itemsGrid.data('ondatabind', request => {
                    request.uniqueids = {
                        InventoryTypeId: inventoryTypeId,
                        CategoryId: categoryId,
                        SubCategoryId: subCategoryId,
                    }
                    request.pagesize = 9999;
                    request.searchfieldoperators = ["<>"];
                    request.searchfields = ["Inactive"];
                    request.searchfieldvalues = ["T"];
                });
                FwBrowse.search($itemsGrid)
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
        const $inventoryTypeGrid = $form.find('[data-name="InventorySequenceTypeGrid"]');
        const $categoryGrid = $form.find('[data-name="InventorySequenceCategoryGrid"]');
        const $subCategoryGrid = $form.find('[data-name="InventorySequenceSubCategoryGrid"]');
        const $itemsGrid = $form.find('[data-name="InventorySequenceItemsGrid"]');

        FwBrowse.search($inventoryTypeGrid)
            .then(() => {
                const inventoryTypeId = $inventoryTypeGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                $categoryGrid.data('ondatabind', request => {
                    request.uniqueids = {
                        InventoryTypeId: inventoryTypeId,
                        RecType: FwFormField.getValueByDataField($form, 'RecType'),
                    }
                    request.pagesize = 9999;
                    request.searchfieldoperators = ["<>"];
                    request.searchfields = ["Inactive"];
                    request.searchfieldvalues = ["T"];
                });
                FwBrowse.search($categoryGrid)
                    .then(() => {
                        let categoryId = $categoryGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                        if (categoryId === undefined || categoryId === 'undefined' || categoryId === '') {
                            categoryId = 'NONE';
                        }
                        $subCategoryGrid.data('ondatabind', request => {
                            request.uniqueids = {
                                CategoryId: categoryId,
                            }
                            request.pagesize = 9999;
                            request.searchfieldoperators = ["<>"];
                            request.searchfields = ["Inactive"];
                            request.searchfieldvalues = ["T"];
                        });
                        FwBrowse.search($subCategoryGrid)
                            .then(() => {
                                let categoryId = $categoryGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                                if (categoryId === undefined || categoryId === 'undefined' || categoryId === '') {
                                    categoryId = 'NONE';
                                }
                                const subCategoryId = $subCategoryGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                                const inventoryTypeId = $inventoryTypeGrid.find('tbody tr').first().find('td .field').attr('data-originalvalue');
                                $itemsGrid.data('ondatabind', request => {
                                    request.uniqueids = {
                                        InventoryTypeId: inventoryTypeId,
                                        CategoryId: categoryId,
                                        SubCategoryId: subCategoryId,
                                    }
                                    request.pagesize = 9999;
                                    request.searchfieldoperators = ["<>"];
                                    request.searchfields = ["Inactive"];
                                    request.searchfieldvalues = ["T"];
                                });
                                FwBrowse.search($itemsGrid)
                            });
                    });
            });
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        FwBrowse.renderGrid({
            nameGrid: 'InventorySequenceTypeGrid',
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
                    Rental: true,
                };
                request.searchfieldoperators = ["<>"];
                request.searchfields = ["Inactive"];
                request.searchfieldvalues = ["T"];
                request.pagesize = 9999;
            },
            beforeSave: (request: any) => {
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                $browse.attr('data-tableheight', '800px')
            }
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'InventorySequenceCategoryGrid',
            gridSecurityId: 'pWsHOgp1o7Obw',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasEdit = false;
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                // defined in afterLoad
            },
            beforeSave: (request: any) => {
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                $browse.attr('data-tableheight', '800px')
            }
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'InventorySequenceSubCategoryGrid',
            gridSecurityId: 'vHMa0l5PUysXo',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasEdit = false;
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                // defined in afterLoad
            },
            beforeSave: (request: any) => {
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                $browse.attr('data-tableheight', '800px')
            }
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'InventorySequenceItemsGrid',
            gridSecurityId: 'UgfInM2AmF6B',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasEdit = false;
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                // defined in afterLoad
            },
            beforeSave: (request: any) => {
            },
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                $browse.attr('data-tableheight', '800px')
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    getInventoryType($form): string {
        const recType = FwFormField.getValueByDataField($form, 'RecType');
        let type;
        switch (recType) {
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
        return type;
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
            <div id="inventorysequenceutilityform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Inventory Sequence Utility" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="InventorySequenceUtilityController">
              <div id="inventorysequenceutilityform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
                <div class="tabpages">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Inventory Sequence Utility" style="max-width:700px">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="Type" data-datafield="RecType"></div>
                    </div>
                  </div>
                  <div class="flexrow" style="max-width:2105px;">
                    <div class="flexcolumn" style="flex:0 1 525px;">
                      <div data-control="FwGrid" data-grid="InventorySequenceTypeGrid"></div>
                    </div>
                    <div class="flexcolumn" style="flex:0 1 525px;">
                      <div data-control="FwGrid" data-grid="InventorySequenceCategoryGrid"></div>
                    </div>
                    <div class="flexcolumn" style="flex:0 1 525px;">
                      <div data-control="FwGrid" data-grid="InventorySequenceSubCategoryGrid"></div>
                    </div>
                    <div class="flexcolumn" style="flex:0 1 525px;">
                      <div data-control="FwGrid" data-grid="InventorySequenceItemsGrid"></div>
                    </div>
                  </div>
                </div>
              </div>
            </div>`;
    }
    //----------------------------------------------------------------------------------------------
}
var InventorySequenceUtilityController = new InventorySequenceUtility();