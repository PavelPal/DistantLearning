app.controller("mainController", mainController);

function mainController($scope, $mdSidenav, $state, authService, ngProgressFactory) {
    $scope.title = "Дистанционное обучение";

    $scope.toggleSideNav = toggleSideNav("sideNav");

    function toggleSideNav() {
        return function () {
            $mdSidenav('sideNav')
                .toggle();
        };
    }

    /*$scope.progressbar = ngProgressFactory.createInstance();
    $scope.progressbar.setParent(document.getElementById('main-container'));
    $scope.progressbar.setAbsolute();
    $scope.progressbar.start();
    $scope.progressbar.complete();*/

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