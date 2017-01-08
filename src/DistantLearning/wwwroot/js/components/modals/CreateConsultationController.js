app.controller("CreateConsultationController", CreateConsultationController);

function CreateConsultationController($scope, $mdDialog) {
    $scope.dayOfWeek = ["Пн", "Вт", "Ср", "Чт", "Пт", "Сб"];
    $scope.consultation = {
        dayOfWeek: 0,
        hour: null,
        minutes: null
    };

    $scope.cancel = function () {
        $mdDialog.cancel();
    };

    $scope.answer = function () {
        $mdDialog.hide($scope.consultation);
    };
}