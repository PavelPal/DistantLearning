app.config(routes);

function routes($stateProvider, $httpProvider, $urlRouterProvider) {

    $urlRouterProvider.otherwise('/');

    $stateProvider
        .state("main", {
            url: "/",
            templateUrl: "/home/index",
            controller: "mainController",
            abstract: true
        })
        .state("login", {
            url: "/login",
            templateUrl: "../app/account/login.html",
            controller: "loginController"
        })
        .state("signup", {
            url: "/signup",
            templateUrl: "../app/account/signup.html",
            controller: "signupController"
        })
        .state("profile", {
            url: "/profile",
            templateUrl: "../app/profile.html"
        })
        .state("users", {
            url: "/users",
            templateUrl: "../app/users.html"
        })
        .state("tests", {
            url: "/tests",
            templateUrl: "../app/tests.html"
        })
        .state("journal", {
            url: "/journal",
            templateUrl: "../app/journal.html"
        })
        .state("results", {
            url: "/results",
            templateUrl: "../app/results.html"
        })
        .state("documents", {
            url: "/documents",
            templateUrl: "../app/documents.html"
        })
        .state("settings", {
            url: "/settings",
            templateUrl: "../app/settings.html"
        })
        .state("help", {
            url: "/help",
            templateUrl: "../app/help.html"
        });

    $httpProvider.interceptors.push("authInterceptorService");
}