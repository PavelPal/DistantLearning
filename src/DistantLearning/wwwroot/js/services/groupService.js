app.factory("groupService", groupService);

function groupService($http) {
    var groups = [];
    return {
        getGroups: function (callback) {
            $http.get("/api/group")
                .success(function (data) {
                    callback(data);
                })
                .error(function () {
                    console.error("Problem with getting groups from the server");
                });
        }
    };
}