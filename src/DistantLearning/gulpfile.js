"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    sass = require("gulp-sass"),
    order = require("gulp-order");

var paths = {
    webroot: "wwwroot/"
};

paths.js = paths.webroot + "js/**/**.js";
paths.minJs = paths.webroot + "js/**/*.min.js";
paths.css = paths.webroot + "css/**/*.css";
paths.minCss = paths.webroot + "css/**/*.min.css";
paths.concatJsDest = paths.webroot + "js/site.min.js";
paths.concatCssDest = paths.webroot + "css/main.min.css";
paths.scss = paths.webroot + "scss/**/*.scss";
paths.scssDest = paths.webroot + "css/";

gulp.task("clean:js",
    function (cb) {
        rimraf(paths.concatJsDest, cb);
    });

gulp.task("clean:css",
    function (cb) {
        rimraf(paths.concatCssDest, cb);
    });

gulp.task("clean", ["clean:js", "clean:css"]);

gulp.task("min:js",
    function () {
        return gulp.src([paths.js, "!" + paths.minJs], {base: "."})
            .pipe(concat(paths.concatJsDest))
            // .pipe(uglify())
            .pipe(gulp.dest("."));
    });

gulp.task("min:css",
    function () {
        return gulp.src([paths.css, "!" + paths.minCss])
            .pipe(concat(paths.concatCssDest))
            .pipe(cssmin())
            .pipe(gulp.dest("."));
    });

gulp.task("min", ["min:js", "min:css"]);

gulp.task("watch",
    function () {
        gulp.watch(paths.js, ["clean:js", "min:js"]);
        gulp.watch(paths.css, ["clean:css", "min:css"]);
        gulp.watch(paths.scss, ["sass"]);
    });

gulp.task("sass",
    function () {
        return gulp.src(paths.scss)
            .pipe(sass().on("error", sass.logError))
            .pipe(gulp.dest(paths.scssDest));
    });

gulp.task("default", ["watch", "clean", "min", "sass"]);