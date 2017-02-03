app.controller("testListController", testListController);

function testListController($scope, $mdToast, testService, ngProgressFactory) {
    $scope.progressbar = ngProgressFactory.createInstance();
    $scope.progressbar.setParent(document.querySelector('.search-input-block'));
    $scope.progressbar.setAbsolute();
    $scope.progressbar.start();
    $scope.tests = [];
    $scope.isLoading = true;
    $scope.canGetElements = true;
    $scope.searchParams = {searchString: null, skip: 0, take: 20};

    testService.getTests($scope.searchParams, function (data) {
        if (data.length < $scope.searchParams.take) $scope.canGetElements = false;
        $scope.tests = data;
        $scope.isLoading = false;
        $scope.progressbar.complete();
    });

    $scope.deleteTest = function (index) {
        var testId = $scope.tests[index].id;
        testService.deleteTest(testId, function (data) {
            if (data == "Invalid id") {
                $mdToast.show($mdToast.simple().textContent("Некорректный ID").position('bottom right').hideDelay(3000));
            } else if (data == "Not found") {
                $mdToast.show($mdToast.simple().textContent("Документ не найден").position('bottom right').hideDelay(3000));
            } else if (data == "User not found") {
                $mdToast.show($mdToast.simple().textContent("Пользователь не найден").position('bottom right').hideDelay(3000));
            } else if (data == "Deleted") {
                $scope.tests.splice(index, 1);
                $mdToast.show($mdToast.simple().textContent("Тест удален").position('bottom right').hideDelay(3000));
            }
        });
    };

    $scope.findTests = function () {
        if ($scope.isLoading)
            return;
        $scope.isLoading = true;
        $scope.progressbar.start();
        $scope.searchParams.skip = 0;
        testService.getTests($scope.searchParams, function (data) {
            if (data.length < $scope.searchParams.take) $scope.canGetElements = false;
            $scope.tests = data;
            $scope.isLoading = false;
            $scope.progressbar.complete();
        });
    };

    $scope.getMoreTests = function () {
        if ($scope.isLoading) return;
        $scope.isLoading = true;
        $scope.progressbar.start();
        $scope.searchParams.skip += $scope.searchParams.take;
        if ($scope.canGetElements) {
            testService.getTests($scope.searchParams, function (data) {
                if (data.length < $scope.searchParams.take) $scope.canGetElements = false;
                angular.forEach(data, function (element) {
                    $scope.tests.push(element);
                });
                $scope.progressbar.complete();
            });
        }
        $scope.isLoading = false;
    };
}