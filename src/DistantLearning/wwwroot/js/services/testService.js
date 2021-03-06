app.factory("testService", testService);

function testService($http) {
    return {
        getTests: function (searchParams, callback) {
            $http({
                url: "/api/test",
                method: "GET",
                params: {
                    searchString: searchParams.searchString,
                    skip: searchParams.skip,
                    take: searchParams.take
                }
            }).then(
                function successCallback(response) {
                    callback(response.data);
                }, function errorCallback(error) {
                    console.error("Problem with getting tests from the server " + error);
                }
            );
        },
        deleteTest: function (id, callback) {
            $http({
                url: "/api/test/deleteTest/" + id,
                method: "POST"
            }).then(
                function successCallback(response) {
                    callback(response.data);
                }, function errorCallback(error) {
                    console.error("Problem with deleting test " + error);
                }
            );
        },
        createTest: function (test, callback) {
            $http({
                url: "/api/test/create",
                method: "POST",
                data: JSON.stringify(test),
                dataType: "json",
                headers: {
                    "Content-Type": "application/json"
                }
            }).then(
                function successCallback(response) {
                    callback(response.data);
                }, function errorCallback(error) {
                    console.error("Problem with creating test " + error);
                }
            );
        }
    };
}