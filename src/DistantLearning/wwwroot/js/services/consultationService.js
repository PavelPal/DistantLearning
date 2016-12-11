app.factory("consultationService", consultationService);

function consultationService($http) {
    return {
        getConsultationsByTeacher: function (teacherId, callback) {
            $http.get("/api/consultation/byTeacher/" + teacherId)
                .then(
                    function successCallback(response) {
                        callback(response.data);
                    }, function errorCallback(error) {
                        console.error("Problem with getting consultations from the server" + error);
                    }
                );
        }
    };
}