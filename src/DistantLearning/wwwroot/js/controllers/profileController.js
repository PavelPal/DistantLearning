app.controller("profileController", profileController);

function profileController($scope, profileService, consultationService) {
    $scope.profile = {};
    $scope.consultations = [];

    profileService.getProfile(function (data) {
        $scope.profile = data;

        consultationService.getConsultations($scope.profile.id, function (data) {
            $scope.consultations = data;
        });
    });

    $scope.dayOfWeekAsString = function (index) {
        return ["Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс"][index];
    };

    $scope.getTime = function (timeString) {
        var timeTokens = timeString.split(':');
        return new Date(1970, 0, 1, timeTokens[0], timeTokens[1], timeTokens[2]);
    };

    document.querySelector('#ngProgress-container').style.top = 48 + 'px';
}