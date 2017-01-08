app.controller("documentController", documentController);

function documentController($scope, documentService, ngProgressFactory) {
    $scope.progressbar = ngProgressFactory.createInstance();
    $scope.progressbar.setParent(document.querySelector('.search-input-block'));
    $scope.progressbar.setAbsolute();
    $scope.progressbar.start();
    $scope.documents = [];
    $scope.isLoading = true;
    $scope.canGetElements = true;
    $scope.searchParams = {searchString: null, skip: 0, take: 20};

    documentService.getDocuments($scope.searchParams, function (data) {
        if (data.length < $scope.searchParams.take) $scope.canGetElements = false;
        $scope.documents = data;
        $scope.isLoading = false;
        $scope.progressbar.complete();
    });

    $scope.findDocuments = function () {
        if ($scope.isLoading)
            return;
        $scope.isLoading = true;
        $scope.progressbar.start();
        $scope.searchParams.skip = 0;
        documentService.getDocuments($scope.searchParams, function (data) {
            if (data.length < $scope.searchParams.take) $scope.canGetElements = false;
            $scope.documents = data;
            $scope.isLoading = false;
            $scope.progressbar.complete();
        });
    };

    $scope.getMoreDocuments = function () {
        if ($scope.isLoading) return;
        $scope.isLoading = true;
        $scope.progressbar.start();
        $scope.searchParams.skip += $scope.searchParams.take;
        if ($scope.canGetElements) {
            documentService.getDocuments($scope.searchParams, function (data) {
                if (data.length < $scope.searchParams.take) $scope.canGetElements = false;
                angular.forEach(data, function (element) {
                    $scope.documents.push(element);
                });
                $scope.progressbar.complete();
            });
        }
        $scope.isLoading = false;
    }
}