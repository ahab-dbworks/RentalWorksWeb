class Holiday {
    Module: string = 'Holiday';
    apiurl: string = 'api/v1/holiday';
    caption: string = Constants.Modules.Settings.children.HolidaySettings.children.Holiday.caption;
    nav: string = Constants.Modules.Settings.children.HolidaySettings.children.Holiday.nav;
    id: string = Constants.Modules.Settings.children.HolidaySettings.children.Holiday.id;

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }

    openBrowse() {
        var $browse;

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }

    openForm(mode: string) {
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);

        if (mode === 'NEW') {
            $form.find('.fixeddate').show();
            $form.find('.ifnew').attr('data-enabled', 'true')

            var type = $form.find('[data-datafield="Type"] .fwformfield-value:checked').val();
            if (type === 'F') {
                $form.find('.fixeddate').show();
            }
            if (type === 'M') {
                $form.find('.specificmonth').show();
            }
            if (type === 'S') {
                $form.find('.mondaybefore').show()
            }
            if (type === 'O') {
                $form.find('.offset').show();
            }
        }
        $form = FwModule.openForm($form, mode);

        $form.find('[data-datafield="Type"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this).val();
            $form.find('.fixeddate').hide();
            $form.find('.specificmonth').hide();
            $form.find('.mondaybefore').hide();
            $form.find('.offset').hide();
            if ($this === 'F') {
                $form.find('.fixeddate').show();
            }
            if ($this === 'M') {
                $form.find('.specificmonth').show();
            }
            if ($this === 'S') {
                $form.find('.mondaybefore').show()
            }
            if ($this === 'O') {
                $form.find('.offset').show();
            }
        });

        $form.find('[data-datafield="FixedMonth"] .fwformfield-value').on('change', function () {
            $form.find('[data-datafield="FixedMonth"] input').val(jQuery(this).val())
        });

        $form.find('[data-datafield="Adjustment"] .fwformfield-value').on('change', function () {
            $form.find('[data-datafield="Adjustment"] input').val(jQuery(this).val())
        });

        $form.find('.datepicker').change(function () {
            var month = jQuery('div.datepicker').find('input').val().toString().slice(0, 2);
            var day = jQuery('div.datepicker').find('input').val().toString().slice(3, 5);
            var year = jQuery('div.datepicker').find('input').val().toString().slice(6);
            FwFormField.setValueByDataField($form, 'FixedMonth', month, null, false);
            FwFormField.setValueByDataField($form, 'FixedDay', day, null, false);
            FwFormField.setValueByDataField($form, 'FixedYear', year, null, false);
        });

        FwFormField.loadItems($form.find('.specificfixedmonth'), [
            { value: '1', text: '1' },
            { value: '2', text: '2' },
            { value: '3', text: '3' },
            { value: '4', text: '4' },
            { value: '5', text: '5' },
            { value: '6', text: '6' },
            { value: '7', text: '7' },
            { value: '8', text: '8' },
            { value: '9', text: '9' },
            { value: '10', text: '10' },
            { value: '11', text: '11' },
            { value: '12', text: '12' }
        ], true);

        FwFormField.loadItems($form.find('.weekofmonth'), [
            { value: '0', text: '0' },
            { value: '1', text: '1' },
            { value: '2', text: '2' },
            { value: '3', text: '3' },
            { value: '4', text: '4' }
        ], true);

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="HolidayId"] input').val(uniqueids.HolidayId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    afterLoad($form: any) {
        var type = $form.find('[data-datafield="Type"] .fwformfield-value:checked').val();
        if (type === 'F') {
            $form.find('.fixeddate').show();
        }
        if (type === 'M') {
            $form.find('.specificmonth').show();
        }
        if (type === 'S') {
            $form.find('.mondaybefore').show()
        }
        if (type === 'O') {
            $form.find('.offset').show();
        }
    }
    //-------------------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'CountryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecountry`);
                break;
        }
    }
}

var HolidayController = new Holiday();