app.controller("ProfileImageLoaderController", ProfileImageLoaderController);

function ProfileImageLoaderController($scope, $mdDialog, $mdToast, FileUploader) {
    $scope.imageUrl = '';

    $scope.uploader = new FileUploader({
        url: '/api/image/uploadProfileImage',
        onAfterAddingFile: function (item) {
            $scope.imageUrl = URL.createObjectURL(item._file);
        },
        onSuccessItem: function (item, response) {
            if (response == "Uploaded") {
                $mdToast.show($mdToast.simple().textContent("Загружено").position('bottom right').hideDelay(3000));
            } else if (response == "Invalid file extension") {
                $mdToast.show($mdToast.simple().textContent("Некорректный формат файла").position('bottom right').hideDelay(3000));
            } else {
                $mdToast.show($mdToast.simple().textContent("При загрузке произошла ошибка").position('bottom right').hideDelay(3000));
            }
        },
        onErrorItem: function () {
            $mdToast.show($mdToast.simple().textContent("При загрузке произошла ошибка").position('bottom right').hideDelay(3000));
        }
    });

    $scope.cancel = function () {
        $scope.uploader.clearQueue();
        $mdDialog.cancel();
    };

    $scope.answer = function () {
        $scope.uploader.queue[0].upload();
        $mdDialog.hide($scope.imageUrl);
    };
}