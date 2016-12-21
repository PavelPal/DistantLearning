app.run(run).config(config);

function run($rootScope, $window, $state, authService, ngProgressFactory) {
    $rootScope.progressbar = ngProgressFactory.createInstance();
    $rootScope.progressbar.setParent(document.getElementById('main-container'));
    $rootScope.progressbar.setAbsolute();

    $rootScope.$on("$stateChangeStart", function (event, toState) {
        $rootScope.progressbar.start();
        authService.fillAuthData();
        if (toState.name == "admin") {
            var isAdmin = false;
            angular.forEach(authService.authentication.roles, function (role) {
                if (role == "Admin")
                    isAdmin = true;
            });
            if (!isAdmin) {
                event.preventDefault();
                $state.go("profile", {profileId: authService.authentication.id});
            }
        }
        if (toState.external) {
            event.preventDefault();
            $window.open(toState.url, "_self");
        }
    });

    $rootScope.$on("$stateChangeSuccess", function (event, toState) {
        $rootScope.progressbar.complete();
        if (toState.external) {
            event.preventDefault();
            $window.open(toState.url, "_self");
        }
    });

    $rootScope.$on("$stateChangeError", function () {
        $rootScope.progressbar.reset();
    });

    authService.fillAuthData();
}

function config($provide, $mdThemingProvider) {
    $provide.decorator("$locale", function ($delegate) {
        return $delegate;
    });

    $mdThemingProvider.theme("default")
        .primaryPalette("light-blue", {
            'default': "800",
            'hue-1': "100",
            'hue-2': "600",
            'hue-3': "A100"
        })
        .accentPalette("pink", {
            'default': "400"
        })
        .warnPalette("red");
}