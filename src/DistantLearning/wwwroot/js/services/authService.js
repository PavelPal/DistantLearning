﻿app.factory("authService", authService);

function authService($http, $cookies, localStorageService) {
    var authServiceFactory = {};

    var authentication = {
        id: "",
        isAuth: false,
        email: "",
        roles: []
    };

    var logOutFromServer = function () {
        return $http({
            url: "/api/account/logout",
            method: "POST"
        }).then(
            function successCallback(response) {
            }, function errorCallback(response) {
            }
        );
    };

    var logOut = function () {
        localStorageService.remove("authorizationData");
        if (authentication.isAuth)
            logOutFromServer();
        authentication.isAuth = false;
        authentication.id = "";
        authentication.email = "";
        authentication.roles = [];
    };

    var signUp = function (model, callback) {
        logOut();
        return $http({
            url: "/api/account/register",
            dataType: "json",
            method: "POST",
            data: JSON.stringify(model),
            headers: {
                "Content-Type": "application/json"
            }
        }).then(
            function successCallback(response) {
                if (response.data != "Неверные данные." && response.data != "Неверный тип." && response.data != "При регистрации произошла ошибка.") {
                    $cookies.put("access_token", response.data.email);
                    localStorageService.set("authorizationData", {
                        token: response.access_token,
                        email: response.data.email,
                        roles: response.data.roles
                    });
                    authentication.isAuth = true;
                    authentication.id = response.data.id;
                    authentication.email = response.data.email;
                    authentication.roles = response.data.roles;
                    callback("OK");
                } else {
                    callback(response.data);
                }
            }, function errorCallback(error) {
                logOut();
                callback(error);
            }
        );
    };

    var login = function (model, callback) {
        logOut();
        $http({
            url: "/api/account/login",
            dataType: "json",
            method: "POST",
            data: JSON.stringify(model),
            headers: {
                "Content-Type": "application/json"
            }
        }).then(
            function successCallback(response) {
                if (response.data != "Неверные данные." && response.data != "При входе произошла ошибка.") {
                    $cookies.put("access_token", response.data.email);
                    localStorageService.set("authorizationData", {
                        token: response.access_token,
                        id: response.data.id,
                        email: response.data.email,
                        roles: response.data.roles
                    });
                    authentication.isAuth = true;
                    authentication.id = response.data.id;
                    authentication.email = response.data.email;
                    authentication.roles = response.data.roles;
                    callback("OK");
                } else {
                    callback(response.data);
                }
            }, function errorCallback(error) {
                logOut();
                callback(error);
            }
        );
    };

    var fillAuthData = function () {
        var authData = localStorageService.get("authorizationData");
        var authCookie = $cookies.get("access_token");
        if (authData && authCookie) {
            authentication.isAuth = true;
            authentication.id = authData.id;
            authentication.email = authData.email;
            authentication.roles = authData.roles;
        }
    };

    authServiceFactory.signUp = signUp;
    authServiceFactory.login = login;
    authServiceFactory.logOut = logOut;
    authServiceFactory.fillAuthData = fillAuthData;
    authServiceFactory.authentication = authentication;

    return authServiceFactory;
}