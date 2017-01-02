app.controller("mainController", mainController);

function mainController($scope, $mdSidenav, $state, authService) {
    $scope.title = "Дистанционное обучение";
    $scope.toggleSideNav = toggleSideNav("sideNav");
    $scope.authentication = authService.authentication;

    $scope.isInRole = function (role) {
        var isInRole = false;
        angular.forEach($scope.authentication, function (userRole) {
            if (userRole == role) {
                isInRole = true;
            }
        });
        return isInRole;
    };

    function toggleSideNav() {
        return function () {
            $mdSidenav('sideNav')
                .toggle();
        };
    }

    $scope.close = function () {
        $mdSidenav('sideNav').close();
    };

    $scope.logOut = function () {
        $mdSidenav('sideNav').close();
        authService.logOut();
        $state.go("login");
    };

    $scope.$on("$stateChangeError", function () {
        $state.go("login");
    });
}