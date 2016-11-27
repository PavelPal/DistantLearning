app.controller("loginController", loginController);

function loginController($scope, $state, authService) {

    $scope.title = "Войти";
    $scope.message = "";
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
                        $scope.message = result;
                        $scope.activeLoader = false;
                    }
                });
        }
    };
}