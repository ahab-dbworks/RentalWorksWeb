var gulp = require("gulp");
var sass = require('gulp-sass');

var reportsSource = "./Modules/Reports/";
var reportsDest = "./wwwroot/Reports";

//var sassSource = "./sass/";
//var sassDest = "./wwwroot/Content/css";

gulp.task("html", function ()
{
    gulp.src(reportsSource + "**/*.html")
        .pipe(gulp.dest(reportsDest));
});

gulp.task("js", function ()
{
    gulp.src(reportsSource + "**/*.js")
        .pipe(gulp.dest(reportsDest));
    gulp.src(reportsSource + "**/*.js.map")
        .pipe(gulp.dest(reportsDest));
});


gulp.task("sass", function ()
{
    return gulp.src(reportsSource + "**/*.scss")
    .pipe(sass().on("error", sass.logError))
    .pipe(gulp.dest(reportsDest));
});

gulp.task("watch", function()
{
    gulp.watch(reportsSource + "**/*.html", ["html"]);
    gulp.watch(reportsSource + "**/*.js", ["js"]);
    gulp.watch(reportsSource + "**/*.scss", ["sass"]);
});

gulp.task("default", ["sass", "js", "html", /*"watch"*/]);