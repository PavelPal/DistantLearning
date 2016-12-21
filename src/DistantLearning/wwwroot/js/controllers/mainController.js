app.controller("mainController", mainController);

function mainController($scope, $mdSidenav, $state, authService) {
    $scope.title = "Дистанционное обучение";
    $scope.toggleSideNav = toggleSideNav("sideNav");

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

    $scope.authentication = authService.authentication;

    $scope.isAdmin = function () {
        var isAdmin = false;
        angular.forEach($scope.authentication.roles, function (role) {
            if (role == "Admin")
                isAdmin = true;
        });
        return isAdmin;
    };

    $scope.$on("$stateChangeError", function () {
        $state.go("login");
    });
}