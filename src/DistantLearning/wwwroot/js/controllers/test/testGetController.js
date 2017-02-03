app.controller("testGetController", testGetController);

function testGetController($scope, $stateParams, $state, testService, authService, $mdToast) {
    var profileId = authService.authentication.id,
        testId = $stateParams.testId;

    $scope.test = {};
    $scope.index = 0;
    $scope.isLoading = true;
    $scope.isCompleting = false;

    testService.get(+testId, function (data) {
        if (data == "Invalid id") {
            $mdToast.show($mdToast.simple().textContent("Некорректный ID").position('bottom right').hideDelay(3000));
            $state.go("tests.list");
        } else if (data == "Test not found") {
            $mdToast.show($mdToast.simple().textContent("Тест не найден").position('bottom right').hideDelay(3000));
            $state.go("tests.list");
        } else if (data == "User not found") {
            $mdToast.show($mdToast.simple().textContent("Пользователь не найден").position('bottom right').hideDelay(3000));
            $state.go("tests.list");
        } else if (data == "Completed") {
            $mdToast.show($mdToast.simple().textContent("Тест был пройден ранее").position('bottom right').hideDelay(3000));
            $state.go("profile", {profileId: profileId});
        } else {
            $scope.test = data;
            $scope.$broadcast('timer-add-cd-seconds', $scope.test.questions[0].seconds);
            $scope.$broadcast('timer-reset');
        }
        $scope.isLoading = false;
    });

    $scope.$on('timer-stopped', function (event, data) {
        if (++$scope.index < $scope.test.questions.length) {
            $scope.$broadcast('timer-add-cd-seconds', $scope.test.questions[$scope.index].seconds);
            $scope.$broadcast('timer-reset');
        } else {
            $scope.isCompleting = true;
            $scope.$broadcast('timer-reset');
            testService.checkResult($scope.test, function (data) {
                if (data == "Done") {
                    $state.go("tests.list");
                } else if (data == "Invalid model") {
                    $mdToast.show($mdToast.simple().textContent("Неверные данные").position('bottom right').hideDelay(3000));
                    $state.go("tests.list");
                } else if (data == "User not found") {
                    $mdToast.show($mdToast.simple().textContent("Пользователь не найден").position('bottom right').hideDelay(3000));
                    $state.go("tests.list");
                } else if (data == "Test not found") {
                    $mdToast.show($mdToast.simple().textContent("Тест не найден").position('bottom right').hideDelay(3000));
                    $state.go("tests.list");
                }
                $scope.isCompleting = false;
            });
        }
    });

    $scope.goToNext = function () {
        if (++$scope.index < $scope.test.questions.length) {
            $scope.$broadcast('timer-add-cd-seconds', $scope.test.questions[$scope.index].seconds);
            $scope.$broadcast('timer-reset');
        } else {
            $scope.isCompleting = true;
            $scope.$broadcast('timer-reset');
            testService.checkResult($scope.test, function (data) {
                if (data == "Done") {
                    $state.go("tests.list");
                } else if (data == "Invalid model") {
                    $mdToast.show($mdToast.simple().textContent("Неверные данные").position('bottom right').hideDelay(3000));
                    $state.go("tests.list");
                } else if (data == "User not found") {
                    $mdToast.show($mdToast.simple().textContent("Пользователь не найден").position('bottom right').hideDelay(3000));
                    $state.go("tests.list");
                } else if (data == "Test not found") {
                    $mdToast.show($mdToast.simple().textContent("Тест не найден").position('bottom right').hideDelay(3000));
                    $state.go("tests.list");
                }
                $scope.isCompleting = false;
            });
        }
    };
}