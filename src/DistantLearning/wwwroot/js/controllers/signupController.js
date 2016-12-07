app.controller("signupController", signupController);

function signupController($scope, $state, $element, authService, disciplineService, groupService) {
    $scope.title = "Зарегистрироваться";
    $scope.savedSuccessfully = false;
    $scope.message = "";
    $scope.activeLoader = false;
    $scope.searchTerm = "";
    $scope.disciplines = [];
    $scope.groups = [];

    groupService.getGroups(function (data) {
        $scope.groups = data;
    });

    disciplineService.getDisciplines(function (data) {
        $scope.disciplines = data;
    });

    $scope.clearSearchTerm = function () {
        $scope.searchTerm = "";
    };

    $element.find("input").on("keydown",
        function (e) {
            e.stopPropagation();
        });

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
            $scope.activeLoader = true;
            authService.signUp($scope.registration,
                function (result) {
                    if (result == "OK") {
                        $scope.message = "Регистрация прошла успешно.";
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