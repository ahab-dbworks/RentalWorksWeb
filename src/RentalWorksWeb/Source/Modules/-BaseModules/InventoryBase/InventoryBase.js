var InventoryBase = (function () {
    function InventoryBase() {
        this.ActiveView = 'ALL';
    }
    InventoryBase.prototype.events = function ($form) {
        $form.find('[data-datafield="OverrideProfitAndLossCategory"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="ProfitAndLossCategoryId"]'));
            }
            else {
                FwFormField.disable($form.find('[data-datafield="ProfitAndLossCategoryId"]'));
            }
        });
        $form.find('div[data-datafield="InventoryTypeId"]').data('onchange', function ($tr) {
            if ($tr.find('.field[data-browsedatafield="Wardrobe"]').attr('data-originalvalue') === 'true') {
                $form.find('.wardrobetab').show();
            }
            else {
                $form.find('.wardrobetab').hide();
            }
        });
        $form.find('div[data-datafield="CategoryId"]').data('onchange', function ($tr) {
            FwFormField.disable($form.find('.subcategory'));
            if ($tr.find('.field[data-browsedatafield="SubCategoryCount"]').attr('data-originalvalue') > 0) {
                FwFormField.enable($form.find('.subcategory'));
            }
            else {
                FwFormField.setValueByDataField($form, 'SubCategoryId', '');
            }
        });
    };
    return InventoryBase;
}());
var InventoryBaseController = new InventoryBase();
//# sourceMappingURL=InventoryBase.js.map