interface JQuery {
    [x: string]: any; // allow any jQuery plugin
    intlTelInput(arg1?: any, arg2?: any);
    ckeditor();
    clockpicker(options: any);
    colResizable();
    colpick(options: any);
    colpickSetColor(col: string | object, setCurrent?: boolean);
    colpickHide();
    colpickShow(e: JQuery.Event);
    datepicker(options: any);
    inputmask(arg1: any, arg2?: any);
}