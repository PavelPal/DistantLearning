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
        }
    };
}