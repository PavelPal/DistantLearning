app.factory("disciplineService", disciplineService);

function disciplineService($http) {
    var disciplines = [];
    return {
        getDisciplines: function (callback) {
            $http.get("/api/discipline").then(
                function successCallback(response) {
                    callback(response.data);
                }, function errorCallback(error) {
                    console.error("Problem with getting disciplines from the server " + error);
                }
            );
        },
        getDiscipline: function (id, callback) {
            $http.get("/api/discipline/" + id).then(
                function successCallback(response) {
                    callback(response.data);
                }, function errorCallback(error) {
                    console.error("Problem with getting discipline by id from the server " + error);
                }
            );
        },
        createDiscipline: function (disciplineName, callback) {
            $http({
                url: "/api/discipline/createDiscipline",
                method: "POST",
                data: disciplineName
            }).then(
                function successCallback(response) {
                    callback(response.data);
                }, function errorCallback(error) {
                    console.error("Problem with creating discipline " + error);
                }
            );
        },
        updateDiscipline: function (discipline, callback) {
            $http({
                url: "/api/discipline/updateDiscipline",
                method: "POST",
                data: discipline
            }).then(
                function successCallback(response) {
                    callback(response.data);
                }, function errorCallback(error) {
                    console.error("Problem with updating discipline " + error);
                }
            );
        },
        deleteDiscipline: function (id, callback) {
            $http({
                url: "/api/discipline/deleteDiscipline/" + id,
                method: "POST"
            }).then(
                function successCallback(response) {
                    callback(response.data);
                }, function errorCallback(error) {
                    console.error("Problem with deleting discipline " + error);
                }
            );
        },
        getTeachersDisciplines: function (teacherId, callback) {
            $http.get("/api/discipline/teachersDisciplines/" + teacherId).then(
                function successCallback(response) {
                    callback(response.data);
                }, function errorCallback(error) {
                    console.error("Problem with getting teachers disciplines from the server" + error);
                }
            );
        }
    };
}