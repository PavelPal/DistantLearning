app.controller("profileController", profileController);

function profileController($scope, $state, $stateParams, $mdToast, $mdDialog, profileService, consultationService, documentService, authService, groupService, disciplineService) {
    document.querySelector('#ngProgress-container').style.top = 48 + 'px';

    $scope.profile = {};
    $scope.group = '';
    $scope.disciplines = [];
    $scope.consultations = [];
    $scope.documents = [];
    $scope.image = null;
    $scope.documentsLoader = false;
    $scope.consultationsLoader = false;

    var profileId = $stateParams.profileId;

    profileService.getProfile(profileId, function (data) {
        if (data != "Пользователь не найден.") {
            $scope.profile = data;
            $scope.image = data.photo == null ? null : "data/profile_photos/" + data.photo;
            if ($scope.isTeacher()) {
                disciplineService.getTeachersDisciplines(profileId, function (data) {
                    $scope.disciplines = data;
                });
                $scope.documentsLoader = $scope.consultationsLoader = true;
                consultationService.getConsultationsByTeacher($scope.profile.id, function (data) {
                    $scope.consultations = data;
                    $scope.consultationsLoader = false;
                });
                documentService.getDocumentsByTeacher($scope.profile.id, function (data) {
                    $scope.documents = data;
                    $scope.documentsLoader = false;
                });
            } else if ($scope.isStudent()) {
                groupService.getStudentsGroup(profileId, function (data) {
                    $scope.group = data.name;
                })
            }
        } else {
            $state.go("users");
            $mdToast.show($mdToast.simple().textContent(data).position('bottom right').hideDelay(3000));
        }
    });

    $scope.isTeacher = function () {
        var isTeacher = false;
        angular.forEach($scope.profile.roles, function (role) {
            if (role == "Teacher") {
                isTeacher = true;
            }
        });
        return isTeacher;
    };

    $scope.isStudent = function () {
        var isStudent = false;
        angular.forEach($scope.profile.roles, function (role) {
            if (role == "Student") {
                isStudent = true;
            }
        });
        return isStudent;
    };

    $scope.isCurrent = function () {
        var currentProfileId = authService.authentication.id;
        return profileId == currentProfileId || profileId == "";
    };

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

    $scope.showProfileImageModal = function (ev) {
        if (!$scope.isCurrent) return;
        $mdDialog.show({
            controller: 'ProfileImageLoaderController',
            templateUrl: '../app/partials/profileImageLoaderView.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: false,
            fullscreen: false
        }).then(
            function (response) {
                $scope.image = response;
            },
            function (response) {
            });
    };

    $scope.showDocumentUploadingModal = function (ev) {
        if (!$scope.isTeacher) return;
        $mdDialog.show({
            controller: 'DocumentLoaderController',
            templateUrl: '../app/partials/documentUploadingView.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: false,
            fullscreen: false
        }).then(
            function (response) {
                $scope.documents.unshift({
                    id: 0,
                    name: response.name,
                    date: Date.now(),
                    isLocked: false
                });
            },
            function (response) {
            });
    };
}