app.factory("disciplineService", disciplineService);

function disciplineService($http) {
    var disciplines = [];
    return {
        getDisciplines: function (callback) {
            $http.get("/api/discipline").then(
                function successCallback(response) {
                    callback(response.data);
                }, function errorCallback(error) {
                    console.error("Problem with getting disciplines from the server" + error);
                }
            );
        }
    };
}