app.controller("profileController", profileController);

function profileController($scope, $state, $stateParams, profileService, consultationService, FileUploader, $mdToast) {
    $scope.profile = {};
    $scope.consultations = [];
    $scope.image = null;

    var profileId = $stateParams.profileId;

    profileService.getProfile(profileId, function (data) {
        if (data != "Пользователь не найден.") {
            $scope.profile = data;
            if ($scope.isTeacher()) {
                consultationService.getConsultations($scope.profile.id, function (data) {
                    $scope.consultations = data;
                });
            }
        } else {
            $mdToast.show($mdToast.simple().textContent(data).position('bottom right').hideDelay(3000));
            $state.go("users");
        }
    });

    $scope.dayOfWeekAsString = function (index) {
        return ["Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс"][index];
    };

    $scope.getTime = function (timeString) {
        var timeTokens = timeString.split(':');
        return new Date(1970, 0, 1, timeTokens[0], timeTokens[1], timeTokens[2]);
    };

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

    $scope.add = function () {
        var image = $scope.uploader.queue[0]._file;
        profileService.uploadImage(image, function (data) {
            console.log(data);
        });
    };

    $scope.isTeacher = function () {
        var isTeacher = false;
        angular.forEach($scope.profile.roles, function (role) {
            if (role == "Teacher") {
                isTeacher = true;
            }
        });
        return isTeacher;
    };

    $scope.uploader = new FileUploader();

    document.querySelector('#ngProgress-container').style.top = 48 + 'px';
}