app.factory("documentService", documentService);

function documentService($http) {
    return {
        getDocuments: function (callback) {
            $http.get("/api/document")
                .success(function (data) {
                    callback(data);
                })
                .error(function () {
                    console.error("Problem with getting documents from the server");
                });
        }
    };
}