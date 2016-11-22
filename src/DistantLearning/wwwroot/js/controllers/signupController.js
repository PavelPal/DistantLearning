app.controller("signupController", signupController);

function signupController($scope, $state, authService) {

    $scope.title = "Зарегистрироваться";
    $scope.savedSuccessfully = false;
    $scope.message = "";

    authService.logOut();

    $scope.registration = {
        firstName: "",
        lastName: "",
        email: "",
        password: "",
        confirmPassword: "",
        type: 0,
        disciplines: [],
        children: [],
        group: 0
    };

    $scope.validation = {
        email: /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/
    };

    $scope.signUp = function () {
        if ($scope.signUpForm.$valid) {
            authService.signUp($scope.registration, function (result) {
                if (result == "OK") {
                    $scope.message = "Регистрация прошла успешно.";
                    $state.go("profile");
                } else {
                    $scope.message = result;
                }
            });
        }
    };
}