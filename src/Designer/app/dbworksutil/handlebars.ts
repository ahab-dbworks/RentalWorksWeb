namespace dbworksutil {

    export class handlebars {        

        //static render(template: any, obj: any = {}, $container: JQuery = jQuery('#main_master_body')): JQueryDeferred<string> {
        //    var $promise = jQuery.Deferred();            
        //    $promise.resolve($container.html(template(obj)));
        //    return $promise;
        //}

        static render(source: any, obj: any = {}, $container: JQuery = jQuery('#main_master_body')): JQueryDeferred<string> {
            var $promise = jQuery.Deferred<any>(), template = Handlebars.compile(source), html = template(obj);
            $promise.resolve($container.html(html));
            return $promise;
        }
        
    }

}