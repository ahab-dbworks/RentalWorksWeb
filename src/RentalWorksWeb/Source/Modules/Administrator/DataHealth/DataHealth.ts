routes.push({ pattern: /^module\/datahealth$/, action: function (match: RegExpExecArray) { return DataHealthController.getModuleScreen(); } });

class DataHealth {
    Module:     string = 'DataHealth';
    apiurl:     string = 'api/v1/datahealth';
    caption:    string = Constants.Modules.Administrator.children.DataHealth.caption;
    nav:        string = Constants.Modules.Administrator.children.DataHealth.nav;
    id:         string = Constants.Modules.Administrator.children.DataHealth.id;
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
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
    }
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        let $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="DataHealthId"] input').val(uniqueids.DataHealthId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: JQuery, response: any) {
        this.renderJsonData($form, response);
    }
    //----------------------------------------------------------------------------------------------
    renderJsonData($form: JQuery, response: any) {
        const data = JSON.parse(response.Json);
        if (data.length) {
            const $table = jQuery(`<table class="form-table">
                                        <thead></thead>
                                   </table>`);
            const fields: Array<string> = Object.keys(data[0]);
            const $thead = jQuery(`<tr class="header"></tr>`);
            for (let i = 0; i < fields.length; i++) {
                const th = `<th>${fields[i]}</th>`;
                $thead.append(th);
            }
            $table.find('thead').append($thead);

            const $tbody = jQuery(`<tbody></tbody>`);
            for (let i = 0; i < data.length; i++) {
                const $row = jQuery(`<tr class="data-row"></tr>`);
                for (let j = 0; j < fields.length; j++) {
                    const field = fields[j];
                    let value = data[i][field] || '';
                    value = value.toString().trim();
                    const td = `<td data-fieldname="${field}">${value}</td>`;
                    $row.append(td);
                }
                $tbody.append($row);
            }
            $table.find('thead').after($tbody);
            $form.find('.data-health').empty().append($table);
        }
    }
    //----------------------------------------------------------------------------------------------
    events($form: JQuery) {

    }
    //----------------------------------------------------------------------------------------------
};
//----------------------------------------------------------------------------------------------
var DataHealthController = new DataHealth();