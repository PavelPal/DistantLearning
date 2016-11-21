app.controller("loginController", loginController);

function loginController($scope, $location, authService) {

    $scope.title = "Войти";

    $scope.loginData = {
        email: "",
        password: ""
    };

    $scope.message = "";

    $scope.login = function() {
        authService.login($scope.loginData)
            .then(function(response) {
                    $location.path("/home");
                },
                function(err) {
                    $scope.message = err.error_description;
                });
    };
};