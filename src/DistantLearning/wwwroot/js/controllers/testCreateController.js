app.controller("testCreateController", testCreateController);

function testCreateController($scope, disciplineService, authService, $element) {
    var profileId = authService.authentication.id;

    $scope.disciplines = [];
    $scope.newTest = {
        name: '',
        isLocked: false,
        startedDate: '',
        closedDate: '',
        disciplineId: 0,
        questions: []
    };

    disciplineService.getTeachersDisciplines(profileId, function (data) {
        $scope.disciplines = data;
    });

    $scope.addQuestion = function () {
        var question = {body: '', seconds: 15, answers: []};
        $scope.newTest.questions.push(question);
    };

    $scope.removeQuestion = function (index) {
        $scope.newTest.questions.splice(index, 1);
    };

    $scope.addAnswer = function (index) {
        var answer = {body: '', isCorrect: false};
        $scope.newTest.questions[index].answers.push(answer);
    };

    $scope.isInRole = function (role) {
        return authService.isInRole(role);
    }
}