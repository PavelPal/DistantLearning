app.controller("testController", testController);

function testController($scope, authService) {
    $scope.isInRole = function (role) {
        return authService.isInRole(role);
    };

    $scope.isApproved = function () {
        return authService.authentication.isApproved;
    };

    $scope.isCurrent = function (teacherId) {
        var currentProfileId = authService.authentication.id;
        return teacherId == currentProfileId;
    };
}