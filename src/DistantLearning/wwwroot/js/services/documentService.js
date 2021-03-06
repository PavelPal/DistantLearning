app.factory("documentService", documentService);

function documentService($http) {
    return {
        getDocuments: function (searchParams, callback) {
            $http({
                url: "/api/document",
                method: "GET",
                params: {
                    searchString: searchParams.searchString,
                    skip: searchParams.skip,
                    take: searchParams.take
                }
            }).then(
                function successCallback(response) {
                    callback(response.data);
                }, function errorCallback(error) {
                    console.error("Problem with getting documents from the server " + error);
                }
            );
        },
        getDocumentsByTeacher: function (teacherId, callback) {
            $http.get("/api/document/byTeacher/" + teacherId)
                .then(
                    function successCallback(response) {
                        callback(response.data);
                    }, function errorCallback(error) {
                        console.error("Problem with getting teacher's documents from the server " + error);
                    }
                );
        },
        deleteDocument: function (id, callback) {
            $http({
                url: "/api/document/deleteDocument/" + id,
                method: "POST"
            }).then(
                function successCallback(response) {
                    callback(response.data);
                }, function errorCallback(error) {
                    console.error("Problem with deleting document " + error);
                }
            );
        }
    };
}