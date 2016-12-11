app.factory("documentService", documentService);

function documentService($http) {
    return {
        getDocuments: function (callback) {
            $http.get("/api/document").then(
                function successCallback(response) {
                    callback(response.data);
                }, function errorCallback(error) {
                    console.error("Problem with getting documents from the server" + error);
                }
            );
        },
        getDocumentsByTeacher: function (teacherId, callback) {
            $http.get("/api/document/byTeacher/" + teacherId)
                .then(
                    function successCallback(response) {
                        callback(response.data);
                    }, function errorCallback(error) {
                        console.error("Problem with getting documents by teacher from the server" + error);
                    }
                );
        }
    };
}