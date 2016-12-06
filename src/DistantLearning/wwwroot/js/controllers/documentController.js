app.controller("documentController", documentController);

function documentController($scope, documentService) {
    $scope.documeents = [];

    documentService.getDocuments(function (data) {
        $scope.documeents = data;
    });
}