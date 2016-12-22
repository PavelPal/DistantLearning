app.factory("groupService", groupService);

function groupService($http) {
    return {
        getGroups: function (callback) {
            $http.get("/api/group")
                .then(
                    function successCallback(response) {
                        callback(response.data);
                    }, function errorCallback(error) {
                        console.error("Problem with getting groups from the server " + error);
                    }
                );
        },
        getGroup: function (id, callback) {
            $http.get("/api/group/" + id)
                .then(
                    function successCallback(response) {
                        callback(response.data);
                    }, function errorCallback(error) {
                        console.error("Problem with getting group by id from the server " + error);
                    }
                );
        },
        createGroup: function (group, callback) {
            $http({
                url: "/api/group/createGroup",
                method: "POST",
                dataType: "json",
                data: JSON.stringify(group),
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(
                function successCallback(response) {
                    callback(response.data);
                }, function errorCallback(error) {
                    console.error("Problem with creating group " + error);
                }
            );
        },
        updateGroup: function (group, callback) {
            $http({
                url: "/api/group/updateGroup",
                method: "POST",
                data: JSON.stringify(group),
                dataType: "json",
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(
                function successCallback(response) {
                    callback(response.data);
                }, function errorCallback(error) {
                    console.error("Problem with updating group " + error);
                }
            );
        },
        deleteGroup: function (id, callback) {
            $http({
                url: "/api/group/deleteGroup/" + id,
                method: "POST"
            }).then(
                function successCallback(response) {
                    callback(response.data);
                }, function errorCallback(error) {
                    console.error("Problem with deleting group " + error);
                }
            );
        },
        getStudentsGroup: function (studentId, callback) {
            $http.get("/api/group/studentsGroup/" + studentId)
                .then(
                    function successCallback(response) {
                        callback(response.data);
                    }, function errorCallback(error) {
                        console.error("Problem with getting student's group from the server" + error);
                    }
                );
        }
    };
}