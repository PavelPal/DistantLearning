app.run(run).config(config);

function run($rootScope, $window, authService) {

    $rootScope.$on("$stateChangeStart",
        function(event, toState) {
            if (toState.external) {
                event.preventDefault();
                $window.open(toState.url, "_self");
            }
        });

    $rootScope.$on("$stateChangeSuccess",
        function(event, toState) {
            if (toState.external) {
                event.preventDefault();
                $window.open(toState.url, "_self");
            }
        });

    authService.fillAuthData();
}

function config($provide, $mdThemingProvider) {
    $provide.decorator("$locale",
        function($delegate) {
            return $delegate;
        });

    $mdThemingProvider.theme("default")
        .primaryPalette("light-blue",
        {
            'default': "800",
            'hue-1': "100",
            'hue-2': "600",
            'hue-3': "A100"
        })
        .accentPalette("pink",
        {
            'default': "400"
        })
        .warnPalette("red");
}