class PhysicalInventoryQuantityInventoryGrid {
    Module: string = 'PhysicalInventoryQuantityInventoryGrid';
    apiurl: string = 'api/v1/physicalinventoryquantityinventory';
    errorSoundFileName: string;
    successSoundFileName: string;
    addItemRequest: any = {};
    $form: any;
    errorMsg: any;
    errorSound: any;
    successSound: any;
    $trForAddItem: any;
    //----------------------------------------------------------------------------------------------
    generateRow($control, $generatedtr) {
        this.$form = $control.closest('.fwform');
        const $quantityColumn = $generatedtr.find('.quantity');
        this.errorSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).errorSoundFileName;
        this.errorSound = new Audio(this.errorSoundFileName);
        this.successSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).successSoundFileName;
        this.successSound = new Audio(this.successSoundFileName);
        this.errorMsg = this.$form.find('.error-msg.qty');

        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            const originalquantity = $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue');
            const $oldElement = $quantityColumn.find('div');
            const html: any = [];

            html.push('<button class="decrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">-</button>');
            html.push('<div style="position:relative">');
            html.push(`     <input class="fieldvalue" type="number" style="height:1.5em; width:40px; text-align:center;" value="${originalquantity}">`);
            html.push('</div>');
            html.push('<button class="incrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">+</button>');
            jQuery($oldElement).replaceWith(html.join(''));

            $quantityColumn.data({
                interval: {},
                increment: function () {
                    const $value = $quantityColumn.find('.fieldvalue');
                    let oldval = jQuery.isNumeric(parseFloat($value.val())) ? parseFloat($value.val()) : 0;
                    $value.val(++oldval);
                },
                decrement: function () {
                    const $value = $quantityColumn.find('.fieldvalue');
                    let oldval = jQuery.isNumeric(parseFloat($value.val())) ? parseFloat($value.val()) : 0;
                    if (oldval > 0) {
                        $value.val(--oldval);
                    }
                }
            });

            if (jQuery('html').hasClass('desktop')) {
                $quantityColumn
                    .on('click', '.incrementQuantity', () => {
                        $quantityColumn.data('increment')();
                        $quantityColumn.find('.fieldvalue').change();
                    })
                    .on('click', '.decrementQuantity', () => {
                        $quantityColumn.data('decrement')();
                        $quantityColumn.find('.fieldvalue').change();
                    });
            };

            $quantityColumn.on('change', '.fieldvalue', e => {
                let request: any = {};
                const code = $tr.find('[data-browsedatafield="ICode"]').attr('data-originalvalue');
                const physicalInventoryId = $tr.find('[data-browsedatafield="PhysicalInventoryId"]').attr('data-originalvalue');
                const newValue = jQuery(e.currentTarget).val();
                const oldValue = $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue');
                const quantity = Number(newValue) - Number(oldValue);

                request = {
                    ICode: code,
                    Quantity: quantity,
                    PhysicalInventoryId: physicalInventoryId,
                }
                if (quantity != 0) {
                    FwAppData.apiMethod(true, 'POST', "api/v1/physicalinventory/countquantity", request, FwServices.defaultTimeout, response => {
                        if (response.success) {
                            $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue', Number(newValue));
                        }
                    },
                        function onError(response) {
                            $tr.find('[data-browsedatafield="Quantity"] input').val(Number(oldValue));
                        }, this.$form);
                }
            });
        });
    }
}

var PhysicalInventoryQuantityInventoryGridController = new PhysicalInventoryQuantityInventoryGrid();
//----------------------------------------------------------------------------------------------