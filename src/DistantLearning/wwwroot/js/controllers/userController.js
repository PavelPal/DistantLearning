app.controller("userController", userController);

function userController($scope, authService, userService, ngProgressFactory) {
    $scope.progressbar = ngProgressFactory.createInstance();
    $scope.progressbar.setParent(document.querySelector('.search-input-block'));
    $scope.progressbar.setAbsolute();
    $scope.progressbar.start();
    $scope.users = [];
    $scope.isLoading = true;
    $scope.canGetElements = true;
    $scope.searchParams = {
        searchString: null,
        skip: 0,
        take: 20
    };

    userService.getUsers($scope.searchParams, function (data) {
        $scope.users = data;
        $scope.isLoading = false;
        $scope.progressbar.complete();
    });

    $scope.getRole = function (role) {
        switch (role) {
            case "Admin":
                return "администратор";
            case "Teacher":
                return "учитель";
            case "Student":
                return "ученик";
            case "Parent":
                return "родитель";
            case "Moderator":
                return "модератор";
        }
    };

    $scope.$watch("searchParams.searchString", function () {
        if ($scope.isLoading) return;
        $scope.isLoading = true;
        $scope.progressbar.start();
        $scope.searchParams.skip = 0;
        userService.getUsers($scope.searchParams, function (data) {
            $scope.users = data;
            $scope.isLoading = false;
            $scope.progressbar.complete();
        });
    });

    $scope.getMoreUsers = function () {
        if ($scope.isLoading) return;
        $scope.isLoading = true;
        $scope.progressbar.start();
        $scope.searchParams.skip += $scope.searchParams.take;
        if ($scope.canGetElements) {
            userService.getUsers($scope.searchParams, function (data) {
                if (data.length < 20) $scope.canGetElements = false;
                angular.forEach(data, function (element) {
                    $scope.users.push(element);
                });
                $scope.progressbar.complete();
            });
        }
        $scope.isLoading = false;
    }
}