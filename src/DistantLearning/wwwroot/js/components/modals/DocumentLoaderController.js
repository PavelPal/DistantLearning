app.controller("DocumentLoaderController", DocumentLoaderController);

function DocumentLoaderController($scope, $mdDialog, $mdToast, FileUploader) {
    $scope.file = {};

    $scope.uploader = new FileUploader({
        url: '/api/document/uploadDocument',
        onAfterAddingFile: function (item) {
            $scope.file = item._file;
        },
        onSuccessItem: function (item, response) {
            if (response == "Uploaded") {
                $mdToast.show($mdToast.simple()
                    .textContent("Загружено")
                    .position('bottom right')
                    .hideDelay(3000));
            } else {
                $mdToast.show($mdToast.simple()
                    .textContent("При загрузке произошла ошибка")
                    .position('bottom right')
                    .hideDelay(3000));
            }
        },
        onErrorItem: function () {
            $mdToast.show($mdToast.simple()
                .textContent("При загрузке произошла ошибка")
                .position('bottom right')
                .hideDelay(3000));
        }
    });

    $scope.cancel = function () {
        $scope.uploader.clearQueue();
        $mdDialog.cancel();
    };

    $scope.answer = function () {
        $scope.uploader.queue[0].upload();
        $mdDialog.hide($scope.file);
    };
}