app.factory("userService", userService);

function userService($http) {
    var users = [];
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
            })
                .success(function (data) {
                    users = data;
                    callback(data);
                })
                .error(function () {
                    console.error("Problem with getting users from the server");
                });
        }
    };
}