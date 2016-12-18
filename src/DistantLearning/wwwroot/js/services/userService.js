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
        }
    };
}