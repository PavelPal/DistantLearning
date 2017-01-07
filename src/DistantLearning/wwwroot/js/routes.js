app.config(routes);

function routes($stateProvider, $httpProvider, $urlRouterProvider) {
    $httpProvider.interceptors.push("authInterceptorService");

    var auth = function ($q, authService) {
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
            url: "/profile/:profileId",
            templateUrl: "../app/profile.html",
            controller: "profileController",
            resolve: {
                auth: auth
            }
        })
        .state("users", {
            url: "/users",
            templateUrl: "../app/users.html",
            controller: "userController",
            resolve: {
                auth: auth
            }
        })
        .state("tests", {
            abstract: true,
            url: "/tests",
            templateUrl: "../app/test/tests.html",
            controller: "testController",
            resolve: {
                auth: auth
            }
        })
        .state("tests.list", {
            url: "/list",
            templateUrl: "../app/test/list.html",
            controller: "testListController"
        })
        .state("tests.create", {
            url: "/create",
            templateUrl: "../app/test/create.html",
            controller: "testCreateController"
        })
        .state("journal", {
            url: "/journal",
            templateUrl: "../app/journal.html",
            resolve: {
                auth: auth
            }
        })
        .state("results", {
            url: "/results",
            templateUrl: "../app/results.html",
            resolve: {
                auth: auth
            }
        })
        .state("documents", {
            url: "/documents",
            templateUrl: "../app/documents.html",
            controller: "documentController",
            resolve: {
                auth: auth
            }
        })
        .state("settings", {
            url: "/settings",
            templateUrl: "../app/settings.html",
            resolve: {
                auth: auth
            }
        })
        .state("help", {
            url: "/help",
            templateUrl: "../app/help.html",
            resolve: {
                auth: auth
            }
        })
        .state("admin", {
            url: "/admin",
            templateUrl: "../app/admin.html",
            controller: "adminController",
            resolve: {
                auth: auth
            }
        });
}