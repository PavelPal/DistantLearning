app.controller("loginController", loginController);

function loginController($scope, $state, $mdToast, authService) {
    $scope.title = "Войти";
    $scope.activeLoader = false;

    authService.logOut();

    $scope.loginData = {
        email: "",
        password: ""
    };

    $scope.login = function () {
        if ($scope.loginForm.$valid) {
            $scope.activeLoader = true;
            authService.login($scope.loginData,
                function (result) {
                    if (result == "OK") {
                        $scope.message = "Вход прошел успешно.";
                        $scope.activeLoader = false;
                        $state.go("profile");
                    } else {
                        $mdToast.show($mdToast.simple().textContent(result).position('bottom right').hideDelay(3000));
                        $scope.activeLoader = false;
                    }
                });
        }
    };

    document.querySelector('#ngProgress-container').style.top = 0;
}