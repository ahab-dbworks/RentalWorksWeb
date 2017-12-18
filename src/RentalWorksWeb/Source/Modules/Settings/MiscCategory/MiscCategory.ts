declare var FwModule: any;
declare var FwBrowse: any;

    class MiscCategory {
        Module: string;
        apiurl: string;

        constructor() {
            this.Module = 'MiscCategory';
            this.apiurl = 'api/v1/misccategory';
        }

        getModuleScreen() {
            var screen, $browse;

            screen = {};
            screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
            screen.viewModel = {};
            screen.properties = {};

            $browse = this.openBrowse();

            screen.load = function () {
                FwModule.openModuleTab($browse, 'Misc Category', false, 'BROWSE', true);
                FwBrowse.databind($browse);
                FwBrowse.screenload($browse);
            };
            screen.unload = function () {
                FwBrowse.screenunload($browse);
            };

            return screen;
        }

        events($form: JQuery): void {

            $form.on('change', '.overridecheck input[type=checkbox]', (e) => {
                var $overrideCheck = jQuery(e.currentTarget), $categoryValidation = $form.find('.catvalidation');

                this.toggleEnabled($overrideCheck, $categoryValidation);
            });

        }

        toggleEnabled($checkbox: JQuery, $validation: JQuery): void {
            if ($checkbox.is(':checked')) {
                $validation.attr('data-enabled', 'true');
            } else {
                $validation.attr('data-enabled', 'false');
            }
        }

        renderGrids($form: any) {
            var $subCategoryGrid, $subCategoryControl;

            $subCategoryGrid = $form.find('div[data-grid="SubCategoryGrid"]');
            $subCategoryControl = jQuery(jQuery('#tmpl-grids-SubCategoryGridBrowse').html());
            $subCategoryGrid.empty().append($subCategoryControl);
            $subCategoryControl.data('ondatabind', function (request) {
                request.uniqueids = {
                    CategoryId: $form.find('div.fwformfield[data-datafield="CategoryId"] input').val()
                }
            });
            $subCategoryControl.data('beforesave', function (request) {
                request.CategoryId = FwFormField.getValueByDataField($form, 'CategoryId');
            })
            FwBrowse.init($subCategoryControl);
            FwBrowse.renderRuntimeHtml($subCategoryControl);

        }

        openBrowse() {
            var $browse;

            $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
            $browse = FwModule.openBrowse($browse);
            FwBrowse.init($browse);

            return $browse;
        }

        openForm(mode: string) {
            var $form;

            $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
            $form = FwModule.openForm($form, mode);

            this.events($form);

            this.toggleEnabled($form.find('.overridecheck input[type=checkbox]'), $form.find('.catvalidation'));

            return $form;
        }

        loadForm(uniqueids: any) {
            var $form;

            $form = this.openForm('EDIT');
            $form.find('div.fwformfield[data-datafield="CategoryId"] input').val(uniqueids.CategoryId);
            FwModule.loadForm(this.Module, $form);

                return $form;
        }

        saveForm($form: any, closetab: boolean, navigationpath: string)
            {
                FwModule.saveForm(this.Module, $form, closetab, navigationpath);
            }

        loadAudit($form: any) {
                var uniqueid;
                uniqueid = $form.find('div.fwformfield[data-datafield="MiscCategoryId"] input').val();
                FwModule.loadAudit($form, uniqueid);
        }

        afterLoad($form: any) {
            var $laborCategoryGrid;
            $laborCategoryGrid = $form.find('[data-name="SubCategoryGrid"]');
            FwBrowse.search($laborCategoryGrid);
        }

    }

(<any>window).MiscCategoryController = new MiscCategory();