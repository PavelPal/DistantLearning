app.factory("testService", testService);

function testService($http) {
    return {
        get: function (testId, callback) {
            $http({
                url: "/api/test/get/" + testId,
                method: "GET"
            }).then(
                function successCallback(response) {
                    callback(response.data);
                }, function errorCallback(error) {
                    console.error("Problem with getting test from the server " + error);
                }
            );
        },
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
                url: "/api/test/delete/" + id,
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
        },
        checkResult: function (testResult, callback) {
            var test = {
                id: testResult.id,
                questions: []
            };
            angular.forEach(testResult.questions, function (question, index) {
                test.questions.push({
                    id: question.id,
                    answers: []
                });
                angular.forEach(question.answers, function (answer) {
                    test.questions[index].answers.push({
                        id: answer.id,
                        isChecked: answer.isChecked
                    })
                });
            });
            $http({
                url: "/api/test/checkResult",
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
                    console.error("Problem with checking results " + error);
                }
            );
        },
        getResults: function (id, callback) {
            $http({
                url: "/api/test/testResults/" + id,
                method: "GET"
            }).then(
                function successCallback(response) {
                    callback(response.data);
                }, function errorCallback(error) {
                    console.error("Problem with getting test from the server " + error);
                }
            );
        }
    };
}