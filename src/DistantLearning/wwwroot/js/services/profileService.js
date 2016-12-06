app.factory("profileService", profileService);

function profileService($http) {
    return {
        getProfile: function (profileId, callback) {
            $http.get("/api/profile/" + profileId)
                .success(function (data) {
                    callback(data);
                })
                .error(function (data) {
                    console.error("Problem with getting profile data from the server");
                });
        },
        uploadImage: function (image, callback) {
            $http({
                url: "/api/image/uploadProfileImage",
                method: "POST",
                data: image,
                headers: {'Content-Type': 'application/x-www-form-urlencoded'}
            })
                .success(function (data) {
                    callback(data);
                })
                .error(function () {
                    console.error("Problem with getting profile data from the server");
                });
        }
    };
}