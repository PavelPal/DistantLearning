app.config(routes);

function routes($stateProvider, $httpProvider, $urlRouterProvider) {
    $httpProvider.interceptors.push("authInterceptorService");

    var Auth = function ($q, authService) {
        authService.fillAuthData();
        if (authService.authentication.isAuth) {
            return $q.when(authService.authentication);
        } else {
            return $q.reject({authenticated: false});
        }
    };

    $urlRouterProvider.otherwise("/");

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
            templateUrl: "../app/profile.html",
            controller: "profileController",
            resolve: {
                auth: Auth
            }
        })
        .state("users", {
            url: "/users",
            templateUrl: "../app/users.html",
            controller: "userController",
            resolve: {
                auth: Auth
            }
        })
        .state("tests", {
            url: "/tests",
            templateUrl: "../app/tests.html",
            resolve: {
                auth: Auth
            }
        })
        .state("journal", {
            url: "/journal",
            templateUrl: "../app/journal.html",
            resolve: {
                auth: Auth
            }
        })
        .state("results", {
            url: "/results",
            templateUrl: "../app/results.html",
            resolve: {
                auth: Auth
            }
        })
        .state("documents", {
            url: "/documents",
            templateUrl: "../app/documents.html",
            controller: "documentController",
            resolve: {
                auth: Auth
            }
        })
        .state("settings", {
            url: "/settings",
            templateUrl: "../app/settings.html",
            resolve: {
                auth: Auth
            }
        })
        .state("help", {
            url: "/help",
            templateUrl: "../app/help.html",
            resolve: {
                auth: Auth
            }
        });
}