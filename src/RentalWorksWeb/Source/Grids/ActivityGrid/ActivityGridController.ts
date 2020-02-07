class ActivityGrid {
    Module: string = 'ActivityGrid';
    apiurl: string = 'api/v1/activity';

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $gridbrowse: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'ActivityStatusId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateactivitystatus`);
                request.uniqueids.ActivityTypeId = FwBrowse.getValueByDataField($gridbrowse, $tr, 'ActivityTypeId');
                break;
            case 'AssignedToUserId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateuser`);
                break;
            case 'ActivityTypeId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateactivitytype`);
                break;
        }
    }

    filterActivities($form: JQuery) {
        const $activityGrid = $form.find('[data-name="ActivityGrid"]');
        const onDataBind = $activityGrid.data('ondatabind');
        if (typeof onDataBind == 'function') {
            const activityFromDate = FwFormField.getValueByDataField($form, 'ActivityFromDate');
            const activityToDate = FwFormField.getValueByDataField($form, 'ActivityToDate');
            const activityTypes = FwFormField.getValueByDataField($form, 'ActivityTypeId');
            const showShipping = FwFormField.getValueByDataField($form, 'ShowShipping');
            const showSubPo = FwFormField.getValueByDataField($form, 'ShowSubPo');
            const showComplete = FwFormField.getValueByDataField($form, 'ShowComplete');
            $activityGrid.data('ondatabind', request => {
                onDataBind(request);

                delete request.uniqueids.ActivityFromDate;
                delete request.uniqueids.ActivityToDate;
                delete request.uniqueids.ActivityTypeId;
                if (activityFromDate) {
                    request.uniqueids.ActivityFromDate = activityFromDate;
                }
                if (activityToDate) {
                    request.uniqueids.ActivityToDate = activityToDate;
                }
                if (activityTypes) {
                    request.uniqueids.ActivityTypeId = activityTypes;
                }
                request.uniqueids.ShowShipping = showShipping;
                request.uniqueids.ShowSubPo = showSubPo;
                request.uniqueids.ShowComplete = showComplete;
            });
            FwBrowse.search($activityGrid);
        }
    }
}
//----------------------------------------------------------------------------------------------
var ActivityGridController = new ActivityGrid();