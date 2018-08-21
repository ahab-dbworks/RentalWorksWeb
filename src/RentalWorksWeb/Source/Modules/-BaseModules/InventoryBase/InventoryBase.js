class InventoryBase {
    constructor() {
        this.ActiveView = 'ALL';
    }
    events($form) {
        let classificationValue, trackedByValue;
        $form.find('[data-datafield="OverrideProfitAndLossCategory"] .fwformfield-value').on('change', function () {
            let $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="ProfitAndLossCategoryId"]'));
            }
            else {
                FwFormField.disable($form.find('[data-datafield="ProfitAndLossCategoryId"]'));
            }
        });
        $form.find('div[data-datafield="InventoryTypeId"]').data('onchange', $tr => {
            if ($tr.find('.field[data-browsedatafield="Wardrobe"]').attr('data-originalvalue') === 'true') {
                $form.find('.wardrobetab').show();
            }
            else {
                $form.find('.wardrobetab').hide();
            }
        });
        $form.find('div[data-datafield="CategoryId"]').data('onchange', $tr => {
            FwFormField.disable($form.find('.subcategory'));
            if ($tr.find('.field[data-browsedatafield="SubCategoryCount"]').attr('data-originalvalue') > 0) {
                FwFormField.enable($form.find('.subcategory'));
            }
            else {
                FwFormField.setValueByDataField($form, 'SubCategoryId', '');
            }
        });
        $form.find('.class-tracked-radio input').on('change', () => {
            classificationValue = FwFormField.getValueByDataField($form, 'Classification');
            trackedByValue = FwFormField.getValueByDataField($form, 'TrackedBy');
            if (classificationValue === 'I' || classificationValue === 'A') {
                if (trackedByValue !== 'QUANTITY') {
                    $form.find('.asset-submodule').show();
                }
                else {
                    $form.find('.asset-submodule').hide();
                }
            }
            else {
                $form.find('.asset-submodule').hide();
            }
        });
    }
}
var InventoryBaseController = new InventoryBase();
//# sourceMappingURL=InventoryBase.js.map