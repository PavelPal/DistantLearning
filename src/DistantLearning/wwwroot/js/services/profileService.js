app.factory("profileService", profileService);

function profileService($http) {
    return {
        getProfile: function (callback) {
            $http.get("/api/profile")
                .success(function (data) {
                    callback(data);
                })
                .error(function () {
                    console.error("Problem with getting profile data from the server");
                });
        }
    };
}