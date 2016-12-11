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
        },
        uploadImage: function (image, callback) {
            $http({
                url: "/api/image/uploadProfileImage",
                method: "POST",
                data: image,
                headers: {'Content-Type': 'application/x-www-form-urlencoded'}
            }).then(
                function successCallback(response) {
                    callback(response.data);
                }, function errorCallback(error) {
                    console.error("Problem with getting profile data from the server" + error);
                }
            );
        }
    };
}