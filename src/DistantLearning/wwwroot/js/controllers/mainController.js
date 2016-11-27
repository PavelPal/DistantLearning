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

    $scope.logOut = function () {
        $mdSidenav('sideNav').close();
        authService.logOut();
        $state.go("login");
    };

    $scope.authentication = authService.authentication;

    $scope.$on("$stateChangeError",
        function () {
            $state.go("login");
        });
}