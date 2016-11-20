app.controller("mainController", mainController);

function mainController($scope, $timeout, $mdSidenav, $location, authService) {
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
        $location.path("/home");
    };
    $scope.authentication = authService.authentication;
};