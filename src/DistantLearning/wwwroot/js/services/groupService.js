app.factory("groupService", groupService);

function groupService($http) {
    return {
        getGroups: function (callback) {
            $http.get("/api/group")
                .then(
                    function successCallback(response) {
                        callback(response.data);
                    }, function errorCallback(error) {
                        console.error("Problem with getting groups from the server" + error);
                    }
                );
        },
        getStudentsGroup: function (studentId, callback) {
            $http.get("/api/group/studentsGroup/" + studentId)
                .then(
                    function successCallback(response) {
                        callback(response.data);
                    }, function errorCallback(error) {
                        console.error("Problem with getting students group from the server" + error);
                    }
                );
        }
    };
}