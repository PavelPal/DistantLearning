app.factory("groupService", groupService);

function groupService($http) {
    var groups = [];
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
        }
    };
}