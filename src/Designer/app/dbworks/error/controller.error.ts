namespace dbworks.error.controllers {

    export class error  {
    //export class error extends main.controllers.main {

        constructor() {
            
        }

        init(): void {
            this.bind();
        }

        bind(): void {
            $('#main_master_body').html(templates.error());
        }

    }

}