app.factory("authService", authService);

function authService($http, localStorageService) {
    var authServiceFactory = {};

    var authentication = {
        isAuth: false,
        email: "",
        emailConfirmed: false,
        userName: "",
        phoneNumber: "",
        firstName: "",
        lastName: "",
        type: ""
    };

    var logOut = function () {
        localStorageService.remove("authorizationData");

        authentication.isAuth = false;
        authentication.email = "";
        authentication.emailConfirmed = false;
        authentication.userName = "";
        authentication.phoneNumber = "";
        authentication.firstName = "";
        authentication.lastName = "";
        authentication.type = "";
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
        })
            .success(function (response) {
                localStorageService.set("authorizationData", {
                    token: response.access_token,
                    email: response.email,
                    emailConfirmed: response.emailConfirmed,
                    userName: response.userName,
                    phoneNumber: response.phoneNumber,
                    firstName: response.firstName,
                    lastName: response.lastName,
                    type: response.type
                });

                authentication.isAuth = true;
                authentication.email = response.email;
                authentication.emailConfirmed = response.emailConfirmed;
                authentication.userName = response.userName;
                authentication.phoneNumber = response.phoneNumber;
                authentication.firstName = response.firstName;
                authentication.lastName = response.lastName;
                authentication.type = response.type;

                callback("OK")
            })
            .error(function (error) {
                logOut();
                callback(error);
            });
    };

    var login = function (model, callback) {

        $http({
            url: "/api/account/login",
            dataType: "json",
            method: "POST",
            data: JSON.stringify(model),
            headers: {
                "Content-Type": "application/json"
            }
        })
            .success(function (response) {
                localStorageService.set("authorizationData", {
                    token: response.access_token,
                    email: response.email,
                    emailConfirmed: response.emailConfirmed,
                    userName: response.userName,
                    phoneNumber: response.phoneNumber,
                    firstName: response.firstName,
                    lastName: response.lastName,
                    type: response.type
                });

                authentication.isAuth = true;
                authentication.email = response.email;
                authentication.emailConfirmed = response.emailConfirmed;
                authentication.userName = response.userName;
                authentication.phoneNumber = response.phoneNumber;
                authentication.firstName = response.firstName;
                authentication.lastName = response.lastName;
                authentication.type = response.type;

                callback("OK");
            })
            .error(function (error) {
                logOut();
                callback(error);
            });
    };

    var fillAuthData = function () {
        var authData = localStorageService.get("authorizationData");
        if (authData) {
            authentication.isAuth = true;
            authentication.email = authData.email;
            authentication.emailConfirmed = authData.emailConfirmed;
            authentication.userName = authData.userName;
            authentication.phoneNumber = authData.phoneNumber;
            authentication.firstName = authData.firstName;
            authentication.lastName = authData.lastName;
            authentication.type = authData.type;
        }
    };

    authServiceFactory.signUp = signUp;
    authServiceFactory.login = login;
    authServiceFactory.logOut = logOut;
    authServiceFactory.fillAuthData = fillAuthData;
    authServiceFactory.authentication = authentication;

    return authServiceFactory;
}