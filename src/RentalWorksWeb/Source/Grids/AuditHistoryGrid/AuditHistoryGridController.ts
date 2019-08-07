class AuditHistoryGrid {
    Module: string = 'AuditHistoryGrid';
    apiurl: string = 'api/v1/webauditjson';

    generateRow($control, $generatedtr) {
        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            let $oldElement = $tr.find('[data-browsedatafield="Json"]');
            let changes = JSON.parse($oldElement.attr('data-originalvalue'));
            let html: Array<string> = [];
            let htmlAnchor = `< <a style="color:blue;">HTML</a> >`; 
            html.push(`<ul class="audit-header">
                         <span class="field-name-header">Field Name</span>
                         <span class="value-header">Old Value</span>
                         <span class="value-header">New Value</span>
                       </ul>`);


            if (changes.length > 0) {
                for (let i = 0; i < changes.length; i++) {
                    const fieldName = changes[i].FieldName.replace(/([A-Z]+)/g, " $1").replace(/([A-Z][a-z])/g, " $1").trim();
                    if (typeof changes[i].OldValue === 'string') {
                        if (changes[i].OldValue.substring(0, 4) !== 'rgb(') { // For color related fields
                            if (changes[i].FieldName === 'Html') { // add anchor tag with HTML
                                if (changes[i].NewValue === " ") {
                                    changes[i].NewValue = "";
                                }
                                if (changes[i].OldValue === " ") {
                                    changes[i].OldValue = "";
                                }
                                html.push(`<ul>
                                                <span class="field-name">${fieldName}:</span>
                                                <span class="old-html old-value">
                                                    <textarea class="value">${changes[i].OldValue}</textarea>
                                                    <span>${changes[i].OldValue === "" ? "&#160;" : htmlAnchor}</span>
                                                </span>
                                                <span class="new-html new-value">
                                                    <textarea class="value">${changes[i].NewValue}</textarea>
                                                    <span>${changes[i].NewValue === "" ? "&#160;" : htmlAnchor}</span>
                                                </span>
                                                <span class="audit-spacer"></span>
                                           </ul>`);

                            } else {
                                html.push(`<ul>
                                         <span class="field-name">${fieldName}:</span>
                                         <span class="old-value">${changes[i].NewValue === "" ? "&#160;" : changes[i].NewValue}</span>
                                         <span class="new-value">${changes[i].OldValue === "" ? "&#160;" : changes[i].OldValue}</span>
                                         <span class="audit-spacer"></span>
                                       </ul>`);
                            }
                        } else {
                            html.push(`<ul>
                                         <span class="field-name">${fieldName}:</span>
                                         <span class="old-value">${changes[i].OldValue === "" ? "&#160;" : changes[i].OldValue}<span style="border-radius:3px;padding-left:18px;margin-left:5px;border:.75px solid black;background-color:${changes[i].OldValue}"></span></span>
                                         <span class="new-value">${changes[i].NewValue === "" ? "&#160;" : changes[i].NewValue}<span style="border-radius:3px;padding-left:18px;margin-left:5px;border:.75px solid black;background-color:${changes[i].NewValue}"></span></span>
                                         <span class="audit-spacer"></span>
                                       </ul>`);
                        }
                    } else {
                        html.push(`<ul>
                                     <span class="field-name">${fieldName}:</span>
                                     <span class="old-value">${changes[i].OldValue === "" ? "&#160;" : changes[i].OldValue}</span>
                                     <span class="new-value">${changes[i].NewValue === "" ? "&#160;" : changes[i].NewValue}</span>
                                     <span class="audit-spacer"></span>
                                   </ul>`);
                    }
                }
            }
            let $newElement = jQuery(html.join(''));
            jQuery($oldElement).replaceWith($newElement);

            $newElement.on('click', '.old-html span, .new-html span', e => {
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