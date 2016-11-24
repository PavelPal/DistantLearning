app.controller("signupController", signupController);

function signupController($scope, $state, $element, authService) {

    $scope.title = "Зарегистрироваться";
    $scope.savedSuccessfully = false;
    $scope.message = "";
    $scope.activeLoader = false;
    $scope.searchTerm = '';

    $scope.clearSearchTerm = function () {
        $scope.searchTerm = '';
    };

    $element.find('input').on('keydown', function (e) {
        e.stopPropagation();
    });

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

    $scope.disciplines = [
        {
            id: 1,
            name: "Русский язык"
        }, {
            id: 2,
            name: "Математика"
        }, {
            id: 3,
            name: "Физика"
        }, {
            id: 7,
            name: "Химия"
        }];

    $scope.groups = [
        {
            id: 1,
            name: "9А"
        }, {
            id: 2,
            name: "8А"
        }, {
            id: 3,
            name: "7А"
        }, {
            id: 4,
            name: "6А"
        }];

    $scope.validation = {
        email: /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/
    };

    $scope.signUp = function () {
        if ($scope.signUpForm.$valid) {
            $scope.activeLoader = true;
            authService.signUp($scope.registration, function (result) {
                if (result == "OK") {
                    $scope.message = "Регистрация прошла успешно.";
                    $scope.activeLoader = false;
                    $state.go("profile");
                } else {
                    $scope.message = result;
                }
            });
        }
    };
}