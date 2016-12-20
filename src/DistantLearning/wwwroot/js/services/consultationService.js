app.factory("consultationService", consultationService);

function consultationService($http) {
    return {
        getConsultationsByTeacher: function (id, callback) {
            $http.get("/api/consultation/byTeacher/" + id)
                .then(
                    function successCallback(response) {
                        callback(response.data);
                    }, function errorCallback(error) {
                        console.error("Problem with getting consultations from the server " + error);
                    }
                );
        },
        createConsultation: function (consultation, callback) {
            $http({
                url: "/api/consultation/createConsultation",
                method: "POST",
                data: consultation
            }).then(
                function successCallback(response) {
                    callback(response.data);
                }, function errorCallback(error) {
                    console.error("Problem with creating consultation " + error);
                }
            );
        }
    };
}