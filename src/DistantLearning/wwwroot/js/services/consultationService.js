app.factory("consultationService", consultationService);

function consultationService($http) {
    var consultations = [];
    return {
        getConsultations: function (teacherId, callback) {
            $http.get("/api/consultation/byTeacher/" + teacherId)
                .success(function (data) {
                    callback(data);
                })
                .error(function () {
                    console.error("Problem with getting consultations from the server");
                });
        }
    };
}