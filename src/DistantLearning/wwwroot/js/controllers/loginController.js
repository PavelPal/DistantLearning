app.controller("loginController", loginController);

function loginController($scope, $state, $mdToast, authService) {
    document.querySelector('#ngProgress-container').style.top = 0;

    authService.logOut();

    $scope.title = "Войти";
    $scope.activeLoader = false;
    $scope.loginData = {email: "", password: ""};
    $scope.validation = {
        email: /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/,
        password: ''
    };

    $scope.login = function () {
        if ($scope.loginForm.$valid) {
            $scope.activeLoader = true;
            authService.login($scope.loginData,
                function (result) {
                    if (result == "OK") {
                        $scope.message = "Вход прошел успешно.";
                        $scope.activeLoader = false;
                        $state.go("profile", {profileId: authService.authentication.id});
                    } else {
                        $mdToast.show($mdToast.simple().textContent(result).position('bottom right').hideDelay(3000));
                        $scope.activeLoader = false;
                    }
                });
        }
    };
}