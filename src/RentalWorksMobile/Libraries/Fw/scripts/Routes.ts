class Route {
    pattern: RegExp;
    action: (match: RegExpExecArray) => {};
}
var routes: Route[] = [];