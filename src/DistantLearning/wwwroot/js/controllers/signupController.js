app.controller("signupController", signupController);

function signupController($scope, $location, $timeout, authService) {

    $scope.title = "Зарегистрироваться";
    $scope.savedSuccessfully = false;
    $scope.message = "";

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

    var startTimer = function() {
        var timer = $timeout(function() {
                $timeout.cancel(timer);
                $location.path("/login");
            },
            2000);
    };

    $scope.signUp = function() {
        if ($scope.signUpForm.$valid) {
            authService.saveRegistration($scope.registration,
                function(result) {
                    $scope.savedSuccessfully = true;
                    $scope.message =
                        "User has been registered successfully, you will be redicted to login page in 2 seconds.";
                    startTimer();
                });
        }
    };
}