app.controller("mainController", mainController);

function mainController($scope, $mdSidenav, $location, localStorageService, authService) {
    $scope.title = "Дистанционное обучение";

    $scope.toggleSideNav = toggleSideNav("sideNav");

    function toggleSideNav(componentId) {
        return function() {
            $mdSidenav(componentId)
                .toggle();
        };
    }

    $scope.logOut = function() {
        authService.logOut();
        $location.path("/main");
    };

    $scope.authentication = authService.authentication;
}