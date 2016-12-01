app.factory("userService", userService);

function userService($http) {
    var users = [];
    return {
        getUsers: function (callback) {
            $http.get("/api/user")
                .success(function (data) {
                    callback(data);
                })
                .error(function () {
                    console.error("Problem with getting users from the server");
                });
        }
    };
}