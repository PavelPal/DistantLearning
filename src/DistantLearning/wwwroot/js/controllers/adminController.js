app.controller("adminController", adminController);

function adminController($scope, $mdDialog, $mdToast, disciplineService, groupService) {
    $scope.groups = [];
    $scope.disciplines = [];

    $scope.isLoadingGroups = true;
    $scope.isLoadingDisciplines = true;

    disciplineService.getDisciplines(function (data) {
        $scope.disciplines = data;
        $scope.isLoadingDisciplines = false;
    });

    groupService.getGroups(function (data) {
        $scope.groups = data;
        $scope.isLoadingGroups = false;
    });

    // todo change model view

    $scope.createGroupModal = function (ev) {
        var confirm = $mdDialog.prompt()
            .title('Создание новой группы')
            .textContent('Введите название группы.')
            .placeholder('Название')
            .ariaLabel('Название')
            .targetEvent(ev)
            .ok('Создать')
            .cancel('Отмена');

        $mdDialog.show(confirm).then(function (result) {
            groupService.createGroup(result, function (data) {
                if (data == "Created") {
                    $scope.groups.push({
                        id: 0,
                        name: result
                    });
                    $mdToast.show($mdToast.simple().textContent("Группа успешно создана").position('bottom right').hideDelay(3000));
                } else {
                    $mdToast.show($mdToast.simple().textContent("Некорректные данные").position('bottom right').hideDelay(3000));
                }
            });
        }, function () {
            $mdToast.show($mdToast.simple().textContent("Операция была отменена").position('bottom right').hideDelay(3000));
        });
    };

    // todo change model view

    $scope.createDisciplineModal = function (ev) {
        var confirm = $mdDialog.prompt()
            .title('Создание новой дисциплины')
            .textContent('Введите название дисциплины.')
            .placeholder('Название')
            .ariaLabel('Название')
            .targetEvent(ev)
            .ok('Создать')
            .cancel('Отмена');

        $mdDialog.show(confirm).then(function (result) {
            disciplineService.createDiscipline(result, function (data) {
                if (data == "Created") {
                    $scope.disciplines.push({
                        id: 0,
                        name: result
                    });
                    $mdToast.show($mdToast.simple().textContent("Дисциплина успешно создана").position('bottom right').hideDelay(3000));
                } else {
                    $mdToast.show($mdToast.simple().textContent("Некорректные данные").position('bottom right').hideDelay(3000));
                }
            });
        }, function () {
            $mdToast.show($mdToast.simple().textContent("Операция была отменена").position('bottom right').hideDelay(3000));
        });
    };
}