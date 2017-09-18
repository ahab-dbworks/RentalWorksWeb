declare var FwModule: any;
declare var FwBrowse: any;

    class Vendor {
        Module: string;
        apiurl: string;

        constructor() {
            this.Module = 'Vendor';
            this.apiurl = 'api/v1/vendor';
        }

        getModuleScreen() {
            var screen, $browse;

            screen = {};
            screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
            screen.viewModel = {};
            screen.properties = {};

            $browse = this.openBrowse();            

            this.events();

            screen.load = function () {
                FwModule.openModuleTab($browse, 'Vendor', false, 'BROWSE', true);
                FwBrowse.databind($browse);
                FwBrowse.screenload($browse);
            };
            screen.unload = function () {
                FwBrowse.screenunload($browse);
            };            

            return screen;
        }

        setupEvents(): void {
            this.toggleRequiredFields(jQuery('.tabpages'));
        }

        events(): void {

            var $parent = jQuery(document);
            
            $parent.on('click', '.vendertyperadio input[type=radio]', (e) => {
                var $tab = this.getTab(jQuery(e.currentTarget)), value = jQuery(e.currentTarget).val();
                
                this.togglePanels($tab, value);
                this.toggleRequiredFields($tab);
            });

            $parent.on('click', '#companytaxgrid .selected', (e) => {
                this.updateExternalInputsWithGridValues(e.currentTarget);
            });

            $parent.on('click', '#vendornotegrid .selected', (e) => {
                this.updateExternalInputsWithGridValues(e.currentTarget);
            });
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
            var $person = $tab.find('#person_panel'), isRequired = null;

            $person.is(':hidden') ? isRequired = 'false' : isRequired = 'true';

            $person.each((i, e) => {
                var $field = jQuery(e).find('.fwformfield');
                if ($person.is(':hidden')) $field.removeClass('error');
                $field.attr('data-required', isRequired);                
            });            
        }

        updateExternalInputsWithGridValues(target: Element): void {
            var $row = jQuery(target);
            $row.find('.column > .field').each((i, e) => {
                var $column = jQuery(e), id = $column.attr('data-browsedatafield'), value = $column.attr('data-originalvalue');
                console.log(id);
                console.log(value);
                if (value == undefined || null) {
                    jQuery('.' + id).find(':input').val(0);
                } else {
                    jQuery('.' + id).find(':input').val(value);
                }
                
            });
        }

        renderGrids($form: any) {
            var $companyTaxGrid, $companyTaxControl, $vendorNoteGrid, $vendorNoteControl;

            // load companytax Grid
            $companyTaxGrid = $form.find('div[data-grid="CompanyTaxGrid"]');
            $companyTaxControl = jQuery(jQuery('#tmpl-grids-CompanyTaxGridBrowse').html());
            $companyTaxGrid.empty().append($companyTaxControl);
            $companyTaxControl.data('ondatabind', function (request) {
                request.uniqueids = {
                    CompanyId: $form.find('div.fwformfield[data-datafield="VendorId"] input').val()
                }
            });
            FwBrowse.init($companyTaxControl);
            FwBrowse.renderRuntimeHtml($companyTaxControl);

            // load vendornote Grid
            $vendorNoteGrid = $form.find('div[data-grid="VendorNoteGrid"]');
            $vendorNoteControl = jQuery(jQuery('#tmpl-grids-VendorNoteGridBrowse').html());
            $vendorNoteGrid.empty().append($vendorNoteControl);
            $vendorNoteControl.data('ondatabind', function (request) {
                request.uniqueids = {
                    VendorNoteId: $form.find('div.fwformfield[data-datafield="VendorId"] input').val()
                }
            });
            FwBrowse.init($vendorNoteControl);
            FwBrowse.renderRuntimeHtml($vendorNoteControl);

        }

        openBrowse() {
            var $browse;

            $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
            $browse = FwModule.openBrowse($browse);            

            FwBrowse.init($browse);

            return $browse;
        }

        openForm(mode: string) {
            var $form, $defaultrate;

            $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
            $form = FwModule.openForm($form, mode);

            $defaultrate = $form.find('.defaultrate');
            FwFormField.loadItems($defaultrate, [
                  { value: 'DAILY', text: 'Daily Rate' }
                , { value: 'WEEKLY', text: 'Weekly Rate' }
                , { value: 'MONTHLY', text: 'Monthly Rate' }
            ]);

            return $form;
        }

        loadForm(uniqueids: any) {
            var $form;

            $form = this.openForm('EDIT');
            $form.find('div.fwformfield[data-datafield="VendorId"] input').val(uniqueids.VendorId);
            FwModule.loadForm(this.Module, $form);

                return $form;
        }

        saveForm($form: any, closetab: boolean, navigationpath: string)
            {
                FwModule.saveForm(this.Module, $form, closetab, navigationpath);
            }

        loadAudit($form: any) {
                var uniqueid;
                uniqueid = $form.find('div.fwformfield[data-datafield="VendorId"] input').val();
                FwModule.loadAudit($form, uniqueid);
        }

        afterLoad($form: any) {
            var $companyTaxGrid, $vendorNoteGrid;

            $companyTaxGrid = $form.find('[data-name="CompanyTaxGrid"]');
            FwBrowse.search($companyTaxGrid);

            $vendorNoteGrid = $form.find('[data-name="VendorNoteGrid"]');
            FwBrowse.search($vendorNoteGrid);

            this.setupEvents();

        }

    }

(window as any).VendorController = new Vendor();