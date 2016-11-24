app.controller("loginController", loginController);

function loginController($scope, $location, authService) {

    $scope.title = "Войти";
    $scope.activeLoader = false;

    $scope.loginData = {
        email: "",
        password: ""
    };

    $scope.message = "";

    $scope.login = function () {
        if ($scope.loginForm.$valid) {
            $scope.activeLoader = true;
            authService.login($scope.loginData, function (result) {
                if (result == "OK") {
                    $scope.message = "Вход прошел успешно.";
                    $scope.activeLoader = false;
                    $state.go("profile");
                } else {
                    $scope.message = result;
                }
            });
        }
    };
}