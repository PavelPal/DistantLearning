app.factory("profileService", profileService);

function profileService($http) {
    return {
        getProfile: function (profileId, callback) {
            $http.get("/api/profile/" + profileId).then(
                function successCallback(response) {
                    callback(response.data);
                }, function errorCallback(error) {
                    console.error("Problem with getting profile data from the server" + error);
                }
            );
        }
    };
}