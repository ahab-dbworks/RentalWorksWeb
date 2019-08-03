class PhysicalInventoryQuantityItemGrid {
    Module: string = 'PhysicalInventoryQuantityItemGrid';
    apiurl: string = 'api/v1/physicalinventoryquantityitem';
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
            const trackedByValue = $tr.find('[data-browsedatafield="TrackedBy"]').attr('data-originalvalue');
            const itemClassValue = $tr.find('[data-browsedatafield="ItemClass"]').attr('data-originalvalue');
            const $oldElement = $quantityColumn.find('div');
            const html: any = [];
            const $grid = $tr.parents('[data-grid="PhysicalInventoryQuantityItemGrid"]');

            //if (trackedByValue === 'QUANTITY' && itemClassValue !== 'K') {
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
                    const type = $grid.attr('data-moduletype');

                    let request: any = {},
                        code = $tr.find('[data-browsedatafield="ICode"]').attr('data-originalvalue'),
                        physicalInventoryItemId = $tr.find('[data-browsedatafield="PhysicalInventoryItemId"]').attr('data-originalvalue'),
                        newValue = jQuery(e.currentTarget).val(),
                        oldValue = $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue'),
                        quantity = Number(newValue) - Number(oldValue);

                    request = {
                        Code: code,
                        Quantity: quantity,
                        PhysicalInventoryItemId: physicalInventoryItemId,
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
            //} else {
            //    $tr.find('.quantity').text('');
            //    $tr.find('[data-browsedatafield="Quantity"]').attr('data-formreadonly', 'true');
            //} //end of trackedBy conditional

        });
    }
}

var PhysicalInventoryQuantityItemGridController = new PhysicalInventoryQuantityItemGrid();
//----------------------------------------------------------------------------------------------