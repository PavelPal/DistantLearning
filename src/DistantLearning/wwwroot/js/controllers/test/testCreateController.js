app.controller("testCreateController", testCreateController);

function testCreateController($scope, testService, disciplineService, authService, $mdToast) {
    var profileId = authService.authentication.id,
        currentDate = new Date();

    $scope.disciplines = [];
    $scope.newTest = {
        name: '',
        isLocked: false,
        startedDate: null,
        closedDate: null,
        disciplineId: 0,
        questions: []
    };
    $scope.minDate = new Date(
        currentDate.getFullYear(),
        currentDate.getMonth() + 1,
        currentDate.getDate());
    $scope.validation = {
        seconds: /^\d+$/
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

    $scope.removeAnswer = function (qestion, index) {
        qestion.answers.splice(index, 1);
    };

    $scope.isInRole = function (role) {
        return authService.isInRole(role);
    };

    $scope.createTest = function (testForm) {
        if (testForm.$valid) {
            if ($scope.newTest.questions.length > 0) {
                var containZeroAnswers = false,
                    questionIndexes = [],
                    isValid = false,
                    invalidIndexes = [];
                angular.forEach($scope.newTest.questions, function (question, index) {
                    if (question.answers.length == 0) {
                        containZeroAnswers = true;
                        questionIndexes.push(index + 1);
                    } else {
                        var correctIsExist = false;
                        angular.forEach(question.answers, function (answer) {
                            if (answer.isCorrect)
                                correctIsExist = true;
                        });
                        if (correctIsExist)
                            isValid = true;
                        else
                            invalidIndexes.push(index + 1);
                    }
                });
                var result = 'Невозможно создать тест, так в вопрос';
                if (containZeroAnswers) {
                    result += questionIndexes.length > 1 ? 'ах ' : 'е ';
                    result += questionIndexes.join() + ' не содержатся ответы';
                    $mdToast.show($mdToast.simple().textContent(result).position('bottom right').hideDelay(3000));
                } else if (!isValid) {
                    result += invalidIndexes.length > 1 ? 'ах ' : 'е ';
                    result += invalidIndexes.join() + ' не содержатся верные ответы';
                    $mdToast.show($mdToast.simple().textContent(result).position('bottom right').hideDelay(3000));
                } else if (isValid) {
                    testService.createTest($scope.newTest, function (data) {
                        $mdToast.show($mdToast.simple().textContent("Тест успешно создан").position('bottom right').hideDelay(3000));
                    });
                }
            } else {
                $mdToast.show($mdToast.simple().textContent('Невозможно создать тест, так как отсутствуют вопросы').position('bottom right').hideDelay(3000));
            }
        }
    }
}