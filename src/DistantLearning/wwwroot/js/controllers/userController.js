app.controller("userController", userController);

function userController($scope, userService, ngProgressFactory) {
    $scope.progressbar = ngProgressFactory.createInstance();
    $scope.progressbar.setParent(document.querySelector('.search-input-block'));
    $scope.progressbar.setAbsolute();
    $scope.progressbar.start();

    $scope.users = [];

    $scope.searchParams = {
        searchString: null,
        skip: 0,
        take: 20
    };

    userService.getUsers($scope.searchParams, function (data) {
        $scope.users = data;
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
        $scope.progressbar.start();
        userService.getUsers($scope.searchParams, function (data) {
            $scope.users = data;
            $scope.progressbar.complete();
        });
    });
}