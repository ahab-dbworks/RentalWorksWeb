declare var FwModule: any;
declare var FwBrowse: any;

    class Vendor {
        Module: string;
        apiurl: string;
        caption: string;

        constructor() {
            this.Module = 'Vendor';
            this.apiurl = 'api/v1/vendor';
            this.caption = 'Vendor';
        }

        getModuleScreen() {
            var screen, $browse;
            var self: Vendor = this;
            screen = {};
            screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
            screen.viewModel = {};
            screen.properties = {};

            $browse = this.openBrowse();

            screen.load = function () {
                FwModule.openModuleTab($browse, self.caption, false, 'BROWSE', true);
                FwBrowse.databind($browse);
                FwBrowse.screenload($browse);
            };
            screen.unload = function () {
                FwBrowse.screenunload($browse);
            };

            return screen;
        }

        setupEvents($form: JQuery): void {
            this.toggleRequiredFields($form.find('.tabpages'));
        }

        events($form: JQuery): void {
            $form.on('click', '.vendertyperadio input[type=radio]', (e) => {
                var $tab: JQuery = this.getTab(jQuery(e.currentTarget));
                var value: string = jQuery(e.currentTarget).val();
                this.togglePanels($tab, value);
                this.toggleRequiredFields($tab);
            });

            $form.find('[data-name="CompanyTaxOptionGrid"]').data('onselectedrowchanged', ($control: JQuery, $tr: JQuery) => {
                try {
                    this.updateExternalInputsWithGridValues($tr);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });

            //$form.on('click', '#companytaxgrid .selected', (e) => {
            //    this.updateExternalInputsWithGridValues(e.currentTarget);
            //});

            //$form.on('click', '#vendornotegrid .selected', (e) => {
            //    this.updateExternalInputsWithGridValues(e.currentTarget);
            //});
        }

        getTab($target: JQuery): JQuery {
            return $target.closest('.tabpage');
        }

        togglePanels($tab: JQuery, type: string): void {
            $tab.find('.type_panels').hide();
            switch (type) {
                case 'Company':
                    $tab.find('#company_panel').show();
                    break;
                case 'Person':
                    $tab.find('#person_panel').show();
                    break;
                default:
                    throw Error(type + ' is not a known type.');
            }
        }

        toggleRequiredFields($tab: JQuery): void {
            var $person = $tab.find('#person_panel'), $company = $tab.find('#company_panel'), personRequired = null, companyRequired = null;

            $person.is(':hidden') ? personRequired = 'false' : personRequired = 'true';
            $company.is(':hidden') ? companyRequired = 'false' : companyRequired = 'true';

            $person.each((i, e) => {
                var $field = jQuery(e).find('.fwformfield');
                if ($person.is(':hidden')) $field.removeClass('error');
                $field.attr('data-required', personRequired);
            });

            $company.each((i, e) => {
                var $field = jQuery(e).find('.fwformfield');
                if ($company.is(':hidden')) $field.removeClass('error');
                $field.attr('data-required', companyRequired);
            });
        }

        updateExternalInputsWithGridValues($tr: JQuery): void {
            $tr.find('.column > .field').each((i, e) => {
                var $column = jQuery(e), id = $column.attr('data-browsedatafield'), value = $column.attr('data-originalvalue');
                
                if (value == undefined || null) {
                    jQuery('.' + id).find(':input').val(0);
                } else {
                    jQuery('.' + id).find(':input').val(value);
                }
                
            });
        }

        renderGrids($form: JQuery) {
            // load companytax Grid
            var nameCompanyTaxOptionGrid = 'CompanyTaxOptionGrid';
            var $companyTaxOptionGrid: JQuery = $form.find('div[data-grid="' + nameCompanyTaxOptionGrid + '"]');
            var $companyTaxOptionControl: JQuery = FwBrowse.loadGridFromTemplate(nameCompanyTaxOptionGrid);
            $companyTaxOptionGrid.empty().append($companyTaxOptionControl);
            $companyTaxOptionControl.data('ondatabind', function (request) {
                request.uniqueids = {
                    CompanyId: FwFormField.getValueByDataField($form, 'VendorId')
                }
            });
            $companyTaxOptionControl.data('beforesave', function (request) {
                request.CompanyId = FwFormField.getValueByDataField($form, 'VendorId');
            });
            FwBrowse.init($companyTaxOptionControl);
            FwBrowse.renderRuntimeHtml($companyTaxOptionControl);

            // load vendornote Grid
            var nameVendorNoteGrid = 'VendorNoteGrid';
            var $vendorNoteGrid: JQuery = $form.find('div[data-grid="' + nameVendorNoteGrid + '"]');
            var $vendorNoteControl: JQuery = FwBrowse.loadGridFromTemplate(nameVendorNoteGrid);
            $vendorNoteGrid.empty().append($vendorNoteControl);
            $vendorNoteControl.data('ondatabind', function (request) {
                request.uniqueids = {
                    VendorId: FwFormField.getValueByDataField($form, 'VendorId')
                }
            });
            FwBrowse.init($vendorNoteControl);
            FwBrowse.renderRuntimeHtml($vendorNoteControl);

        }

        openBrowse() {
            var $browse;

            $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
            $browse = FwModule.openBrowse($browse);

            FwBrowse.init($browse);

            return $browse;
        }

        openForm(mode: string) {
            var $form, $defaultrate;

            $form = FwModule.loadFormFromTemplate(this.Module);
            $form = FwModule.openForm($form, mode);

            this.events($form);

            if (mode == 'NEW') {
                this.toggleRequiredFields($form);
                FwFormField.setValueByDataField($form, 'DefaultSubRentDaysInWeek', 0);
                FwFormField.setValueByDataField($form, 'DefaultSubRentDiscountPercent', 0);
                FwFormField.setValueByDataField($form, 'DefaultSubSaleDiscountPercent', 0);
            }            

            $defaultrate = $form.find('.defaultrate');
            FwFormField.loadItems($defaultrate, [
                  { value: 'DAILY', text: 'Daily Rate' }
                , { value: 'WEEKLY', text: 'Weekly Rate' }
                , { value: 'MONTHLY', text: 'Monthly Rate' }
            ]);

            return $form;
        }

        loadForm(uniqueids: any) {
            var $form = this.openForm('EDIT');
            FwFormField.setValueByDataField($form, 'VendorId', uniqueids.VendorId);
            FwModule.loadForm(this.Module, $form);

            return $form;
        }

        saveForm($form: any, closetab: boolean, navigationpath: string) {
            FwModule.saveForm(this.Module, $form, closetab, navigationpath);
        }

        loadAudit($form: any) {
            var uniqueid = FwFormField.getValueByDataField($form, 'VendorId');
            FwModule.loadAudit($form, uniqueid);
        }

        afterLoad($form: any) {
            var $companyTaxOptionGrid = $form.find('[data-name="CompanyTaxOptionGrid"]');
            FwBrowse.search($companyTaxOptionGrid);

            var $vendorNoteGrid = $form.find('[data-name="VendorNoteGrid"]');
            FwBrowse.search($vendorNoteGrid);

            //this.events($form);

            this.setupEvents($form);

        }

    }

(window as any).VendorController = new Vendor();