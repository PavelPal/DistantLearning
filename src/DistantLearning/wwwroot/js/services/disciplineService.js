app.factory("disciplineService", disciplineService);

function disciplineService($http) {
    var disciplines = [];
    return {
        getDisciplines: function (callback) {
            $http.get("/api/discipline")
                .success(function (data) {
                    callback(data);
                })
                .error(function () {
                    console.error("Problem with getting disciplines from the server");
                });
        }
    };
}