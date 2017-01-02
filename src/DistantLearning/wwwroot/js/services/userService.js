app.factory("userService", userService);

function userService($http) {
    return {
        getUsers: function (searchParams, callback) {
            $http({
                url: "/api/user",
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
                    console.error("Problem with getting users from the server" + error);
                }
            );
        },
        getNotApprovedUsers: function (searchParams, callback) {
            $http({
                url: "/api/user/getNotApproved",
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
                    console.error("Problem with getting not approved users from the server" + error);
                }
            );
        },
        approveUser: function (id, callback) {
            $http({
                url: "/api/user/approveUser",
                method: "POST",
                data: JSON.stringify(id),
                dataType: "json",
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(
                function successCallback(response) {
                    callback(response.data);
                }, function errorCallback(error) {
                    console.error("Problem with getting not approved users from the server" + error);
                }
            );
        },
        deleteUser: function (id, callback) {
            $http({
                url: "/api/user/deleteUser",
                method: "POST",
                data: JSON.stringify(id),
                dataType: "json",
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(
                function successCallback(response) {
                    callback(response.data);
                }, function errorCallback(error) {
                    console.error("Problem with getting not approved users from the server" + error);
                }
            );
        }
    };
}