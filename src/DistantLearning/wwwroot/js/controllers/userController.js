app.controller("userController", userController);

function userController($scope, userService) {
    $scope.users = [];

    userService.getUsers(function (data) {
        $scope.users = data;
    });
}