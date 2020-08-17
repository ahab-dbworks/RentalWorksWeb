class Route {
    pattern: RegExp;
    action: (match: RegExpExecArray) => any;
}
var routes: Route[] = [];