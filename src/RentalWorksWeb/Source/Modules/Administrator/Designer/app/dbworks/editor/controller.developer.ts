namespace dbworks.editor.controllers{

    export class developer {

        _editor: editor.controllers.main;
        code_mirror: any;        

        constructor(editor_controller: editor.controllers.main) {
            this._editor = editor_controller;
            this.code_mirror = null;
        }

        init(): void {
            this.setup();
            this.events();
        }

        setup(): void {            
            this.configure_codemirror('cs');
        }       

        events(): void {

            jQuery(window).resize((e) => {
                this.code_mirror.setSize('100%', jQuery('#main_content_body').height() + 'px');
            });

        }

        configure_codemirror(lang: string): void {
            jQuery('.CodeMirror').remove();
            var config = null;

            switch (lang) {
                case 'cs':
                    config = {
                        lineNumbers: true,
                        matchBrackets: true,
                        mode: "text/x-csharp",
                        extraKeys: { "Ctrl-Q": (cm) => { console.log(cm); cm.foldCode(cm.getCursor()); } },
                        foldGutter: true,
                        gutters: ["CodeMirror-linenumbers", "CodeMirror-foldgutter"]
                    };
                    break;
                case 'ts':
                    config = {
                        mode: "text/typescript",
                        lineNumbers: true,
                        matchBrackets: true,
                        extraKeys: { "Ctrl-Q": (cm) => { console.log(cm); cm.foldCode(cm.getCursor()); } },
                        foldGutter: true,
                        gutters: ["CodeMirror-linenumbers", "CodeMirror-foldgutter"]
                    };
                    break;
                case 'js':
                    config = {
                        mode: "javascript",
                        lineNumbers: true,
                        lineWrapping: true,
                        extraKeys: { "Ctrl-Q": (cm) => { console.log(cm); cm.foldCode(cm.getCursor()); } },
                        foldGutter: true,
                        gutters: ["CodeMirror-linenumbers", "CodeMirror-foldgutter"]
                    }
                    break;
                case 'htm':
                    config = {                        
                        name: "htmlmixed",
                        scriptTypes: [{
                            matches: '/\/x-handlebars-template|\/x-mustache/i',
                            mode: null
                        },
                        {
                            matches: '/(text|application)\/(x-)?vb(a|script)/i',
                            mode: "vbscript"
                        }],
                        lineNumbers: true,
                        lineWrapping: true,
                        extraKeys: { "Ctrl-Q": (cm) => { console.log(cm); cm.foldCode(cm.getCursor()); } },
                        foldGutter: true,
                        gutters: ["CodeMirror-linenumbers", "CodeMirror-foldgutter"]
                    };
                    break;                
            }
            var textarea = <HTMLTextAreaElement> document.getElementById('code_editor');
            this.code_mirror = CodeMirror.fromTextArea(textarea, config);            

            this.code_mirror.on('keyup', () => {
                var content = this.code_mirror.getValue(),
                    folderIndex = jQuery('#main_content_body').data('activefolderindex'),
                    fileIndex = jQuery('#main_content_body').data('activefileindex'),
                    file = this._editor.get_file_from_folder(folderIndex, fileIndex);

                file.fileContents = content;
                file.hasChanged = true;                                
            });
            //this.code_mirror.foldCode(CodeMirror.Pos(13, 0));
            this.code_mirror.setSize('100%', jQuery('#main_content_body').height() + 'px');

        }

        edit_file(file: editor.models.file): void {                        
            //jQuery('#codeview').data('index', index);
            jQuery('#code_editor').empty();
            this.code_mirror.getDoc().setValue(file.fileContents);            
        }

        clear_editor(): void {
            jQuery('#code_editor').empty();
            this.code_mirror.getDoc().setValue("");   
        }

        get_editor_content(): string {
            return this.code_mirror.getValue();
        }

        get_file_html(): void {

        }
    }

}