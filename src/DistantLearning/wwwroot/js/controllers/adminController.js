app.controller("adminController", adminController);

function adminController($scope, $state, $mdToast, disciplineService, groupService, userService) {
    $scope.groups = [];
    $scope.notApprovedUsers = [];
    $scope.disciplines = [];
    $scope.newGroup = {prefix: 0, postfix: ''};
    $scope.newDiscipline = '';
    $scope.numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11];
    $scope.searchParams = {searchString: null, skip: 0, take: 20};
    $scope.isLoadingGroups = true;
    $scope.isLoadingDisciplines = true;
    $scope.isLoadingUsers = true;
    $scope.canGetUsers = true;

    disciplineService.getDisciplines(function (data) {
        $scope.disciplines = data;
        $scope.isLoadingDisciplines = false;
    });

    groupService.getGroups(function (data) {
        $scope.groups = data;
        $scope.isLoadingGroups = false;
    });

    userService.getNotApprovedUsers($scope.searchParams, function (data) {
        if (data.length < $scope.searchParams.take) $scope.canGetUsers = false;
        $scope.notApprovedUsers = data;
        $scope.isLoadingUsers = false;
    });

    $scope.createGroup = function (group) {
        if (group == undefined || group.prefix == 0 || group.postfix == '') {
            $mdToast.show($mdToast.simple().textContent("Некорректные данные").position('bottom right').hideDelay(3000));
        } else {
            groupService.createGroup(group, function (data) {
                if (data == "Invalid data") {
                    $mdToast.show($mdToast.simple().textContent("Некорректные данные").position('bottom right').hideDelay(3000));
                } else if (data == "Exist") {
                    $mdToast.show($mdToast.simple().textContent("Группа была создана ранее").position('bottom right').hideDelay(3000));
                } else if (data.message == "Created") {
                    $scope.groups.push({
                        id: data.id,
                        prefix: group.prefix,
                        postfix: group.postfix
                    });
                    $scope.newGroup = {
                        prefix: 0,
                        postfix: ''
                    };
                    $mdToast.show($mdToast.simple().textContent("Группа успешно создана").position('bottom right').hideDelay(3000));
                }
            });
        }
    };

    $scope.deleteGroup = function (index) {
        var groupId = $scope.groups[index].id;
        groupService.deleteGroup(groupId, function (data) {
            if (data == "Invalid id") {
                $mdToast.show($mdToast.simple().textContent("Некорректный ID").position('bottom right').hideDelay(3000));
            } else if (data == "Not found") {
                $mdToast.show($mdToast.simple().textContent("Группа не найдена").position('bottom right').hideDelay(3000));
            } else if (data == "Deleted") {
                $scope.groups.splice(index, 1);
                $mdToast.show($mdToast.simple().textContent("Группа удалена").position('bottom right').hideDelay(3000));
            }
        });
    };

    $scope.createDiscipline = function (name) {
        if (name == undefined || name == '' || name == null) {
            $mdToast.show($mdToast.simple().textContent("Некорректные данные").position('bottom right').hideDelay(3000));
        } else {
            disciplineService.createDiscipline(name, function (data) {
                if (data == "Invalid data") {
                    $mdToast.show($mdToast.simple().textContent("Некорректные данные").position('bottom right').hideDelay(3000));
                } else if (data == "Exist") {
                    $mdToast.show($mdToast.simple().textContent("Дисциплина была создана ранее").position('bottom right').hideDelay(3000));
                } else if (data.message == "Created") {
                    $scope.disciplines.push({
                        id: data.id,
                        name: name
                    });
                    $scope.newDiscipline = '';
                    $mdToast.show($mdToast.simple().textContent("Дисциплина успешно создана").position('bottom right').hideDelay(3000));
                }
            });
        }
    };

    $scope.deleteDiscipline = function (index) {
        var disciplineId = $scope.disciplines[index].id;
        disciplineService.deleteDiscipline(disciplineId, function (data) {
            if (data == "Invalid id") {
                $mdToast.show($mdToast.simple().textContent("Некорректный ID").position('bottom right').hideDelay(3000));
            } else if (data == "Not found") {
                $mdToast.show($mdToast.simple().textContent("Дисциплина не найдена").position('bottom right').hideDelay(3000));
            } else if (data == "Deleted") {
                $scope.disciplines.splice(index, 1);
                $mdToast.show($mdToast.simple().textContent("Дисциплина удалена").position('bottom right').hideDelay(3000));
            }
        });
    };

    $scope.findUsers = function () {
        if ($scope.isLoadingUsers)
            return;
        $scope.isLoadingUsers = true;
        $scope.searchParams.skip = 0;
        userService.getNotApprovedUsers($scope.searchParams, function (data) {
            if (data.length < $scope.searchParams.take) $scope.canGetUsers = false;
            $scope.notApprovedUsers = data;
            $scope.isLoadingUsers = false;
        });
    };

    $scope.goToUser = function (id) {
        var url = $state.href('profile', {'profileId': id});
        window.open(url, '_blank');
    };

    $scope.getMoreUsers = function () {
        if ($scope.isLoadingUsers) return;
        $scope.isLoadingUsers = true;
        if ($scope.canGetUsers) {
            $scope.searchParams.skip += $scope.searchParams.take;
            userService.getUsers($scope.searchParams, function (data) {
                if (data.length < $scope.searchParams.take) $scope.canGetUsers = false;
                angular.forEach(data, function (element) {
                    $scope.notApprovedUsers.push(element);
                });
                $scope.isLoadingUsers = false;
            });
        }
    };

    $scope.approveUser = function (index) {
        var userId = $scope.notApprovedUsers[index].id;
        userService.approveUser(userId, function (data) {
            if (data == "Invalid id") {
                $mdToast.show($mdToast.simple().textContent("Некорректный ID").position('bottom right').hideDelay(3000));
            } else if (data == "Not found") {
                $mdToast.show($mdToast.simple().textContent("Пользователь не найден").position('bottom right').hideDelay(3000));
            } else if (data == "Approved") {
                $scope.canGetUsers = true;
                $scope.notApprovedUsers.splice(index, 1);
                $mdToast.show($mdToast.simple().textContent("Пользователь одобрен").position('bottom right').hideDelay(3000));
            }
        });
    };

    $scope.deleteUser = function (index) {
        var userId = $scope.notApprovedUsers[index].id;
        userService.deleteUser(userId, function (data) {
            if (data == "Invalid id") {
                $mdToast.show($mdToast.simple().textContent("Некорректный ID").position('bottom right').hideDelay(3000));
            } else if (data == "Not found") {
                $mdToast.show($mdToast.simple().textContent("Пользователь не найден").position('bottom right').hideDelay(3000));
            } else if (data == "Deleted") {
                $scope.canGetUsers = true;
                $scope.notApprovedUsers.splice(index, 1);
                $mdToast.show($mdToast.simple().textContent("Пользователь удален").position('bottom right').hideDelay(3000));
            } else if (data == "Error") {
                $mdToast.show($mdToast.simple().textContent("При удалении произошла ошибка").position('bottom right').hideDelay(3000));
            }
        });
    }
}