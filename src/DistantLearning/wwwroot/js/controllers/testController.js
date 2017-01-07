app.controller("testController", testController);

function testController($scope, authService) {
    $scope.isInRole = function (role) {
        return authService.isInRole(role);
    }
}