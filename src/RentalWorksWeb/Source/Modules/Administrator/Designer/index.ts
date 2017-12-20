class index {

    main: dbworks.editor.controllers.main;

    constructor() {
        this.main = new dbworks.editor.controllers.main();
    }

    start(): void {        
        //this.default_start_page();
        this.main.init();
    }

    default_start_page(): void {
        var body = document.getElementById('master_designer_container');
        body.innerHTML = templates.editor.main();
    }

}