class AuditHistoryGrid {
    Module: string = 'AuditHistoryGrid';
    apiurl: string = 'api/v1/webauditjson';

    generateRow($control, $generatedtr) {
        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            let $oldElement = $tr.find('[data-browsedatafield="Json"]');
            let changes = JSON.parse($oldElement.attr('data-originalvalue'));
            let html: Array<string> = [];
            let htmlAnchor = `< <a style="color:blue;">HTML</a> >`; 
            html.push(`<ul class="auditHeader" style="font-size:14px; display:flex;">
                         <span style="font-weight:bold; float:left; min-width:350px; text-decoration:underline; padding-right:1em;">Field Name</span>
                         <span style="font-weight:bold; float:left; min-width:200px; text-decoration:underline; padding-right:1em;">Old Value</span>
                         <span style="font-weight:bold; float:left; min-width:200px; text-decoration:underline;">New Value</span>
                       </ul>`);


            if (changes.length > 0) {
                for (let i = 0; i < changes.length; i++) {
                    if (typeof changes[i].OldValue === 'string') {
                        if (changes[i].OldValue.substring(0, 4) !== 'rgb(') { // For color related fields
                            if (changes[i].FieldName === 'Html') { // add anchor tag with HTML
                                if (changes[i].NewValue === " ") {
                                    changes[i].NewValue = "";
                                }
                                if (changes[i].OldValue === " ") {
                                    changes[i].OldValue = "";
                                }
                                html.push(`<ul style="font-size:14px; display:flex;">
                                                <span style="font-weight:bold; float:left; min-width:350px; padding-right:1em;">${changes[i].FieldName}:</span>
                                                <span class="oldHtml" style="min-width:200px; padding-right:1em;">
                                                    <textarea class="value" style="display:none;">${changes[i].OldValue}</textarea>
                                                    <span style="font-weight:bolder; cursor:pointer; display:contents;">${changes[i].OldValue === "" ? "&#160;" : htmlAnchor}</span>
                                                </span>
                                                <span class="newHtml" style="min-width:200px; padding-right:4em;">
                                                    <textarea class="value" style="display:none;">${changes[i].NewValue}</textarea>
                                                    <span style="font-weight:bolder; cursor:pointer; display:contents;">${changes[i].NewValue === "" ? "&#160;" : htmlAnchor}</span>
                                                </span>
                                                <span class="auditSpacer" style="flex:1 1 0"></span>
                                           </ul>`);

                            } else {
                                html.push(`<ul style="font-size:14px; display:flex;">
                                         <span style="font-weight:bold; float:left; min-width:350px; padding-right:1em;">${changes[i].FieldName}:</span>
                                         <span style="min-width:200px; padding-right:1em;">${changes[i].OldValue === "" ? "&#160;" : changes[i].OldValue}</span>
                                         <span style="min-width:200px; padding-right:4em;">${changes[i].NewValue === "" ? "&#160;" : changes[i].NewValue}</span>
                                         <span class="auditSpacer" style="flex:1 1 0"></span>
                                       </ul>`);
                            }
                        } else {
                            html.push(`<ul style="font-size:14px; display:flex;">
                                         <span style="font-weight:bold; float:left; min-width:350px; padding-right:1em;">${changes[i].FieldName}:</span>
                                         <span style="min-width:200px; padding-right:1em;">${changes[i].OldValue === "" ? "&#160;" : changes[i].OldValue}<span style="border-radius:3px;padding-left:18px;margin-left:5px;border:.75px solid black;background-color:${changes[i].OldValue}"></span></span>
                                         <span style="min-width:200px; padding-right:4em;">${changes[i].NewValue === "" ? "&#160;" : changes[i].NewValue}<span style="border-radius:3px;padding-left:18px;margin-left:5px;border:.75px solid black;background-color:${changes[i].NewValue}"></span></span>
                                         <span class="auditSpacer" style="flex:1 1 0"></span>
                                       </ul>`);
                        }
                    } else {
                        html.push(`<ul style="font-size:14px; display:flex;">
                                     <span style="font-weight:bold; float:left; min-width:350px; padding-right:1em;">${changes[i].FieldName}:</span>
                                     <span style="min-width:200px; padding-right:1em;">${changes[i].OldValue === "" ? "&#160;" : changes[i].OldValue}</span>
                                     <span style="min-width:200px; padding-right:4em;">${changes[i].NewValue === "" ? "&#160;" : changes[i].NewValue}</span>
                                     <span class="auditSpacer" style="flex:1 1 0"></span>
                                   </ul>`);
                    }
                }
            }
            let $newElement = jQuery(html.join(''));
            jQuery($oldElement).replaceWith($newElement);

            $newElement.on('click', '.oldHtml span, .newHtml span', e => {
                e.stopPropagation();
                let $confirmation, controlhtml;
                let $htmlChanges = jQuery(e.currentTarget).siblings('textarea.value')[0].textContent;
                $confirmation = FwConfirmation.renderConfirmation('HTML', '');
                FwConfirmation.addButton($confirmation, 'Close', true);
                controlhtml = [];
                controlhtml.push('<div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield html" data-caption="HTML" data-enabled="false" data-datafield=""></div>');
                FwConfirmation.addControls($confirmation, controlhtml.join('\n'));
                FwFormField.setValue($confirmation, '.html', $htmlChanges);
                $confirmation.find('.html textarea')
                    .css({
                        'width': '1200px',
                        'max-width': '1500px',
                        'height': '700px',
                        'resize': 'both'
                    });
            });
        });
    }
}

var AuditHistoryGridController = new AuditHistoryGrid();
//----------------------------------------------------------------------------------------------