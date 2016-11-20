app.factory("authService", authService);

function authService($http, $q, localStorageService) {
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

    var logOut = function() {
        localStorageService.remove("authorizationData");
        clearData();
    };

    var saveRegistration = function(model) {
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
            .success(function(response) {
                fromModel(response);
            })
            .error(function(error) {
                throw new Error("Error in authService. " + error);
            });
    };

    var login = function(loginData) {

        var deferred = $q.defer();

        $http({
                url: "/api/account/login",
                dataType: "json",
                method: "POST",
                data: JSON.stringify(loginData),
                headers: {
                    "Content-Type": "application/json"
                }
            })
            .success(function(response) {
                localStorageService.set("authorizationData",
                {
                    token: response.access_token,
                    email: loginData.email,
                    emailConfirmed: loginData.emailConfirmed,
                    userName: loginData.userName,
                    phoneNumber: loginData.phoneNumber,
                    firstName: loginData.firstName,
                    lastName: loginData.lastName,
                    type: loginData.type
                });

                fromModel(response);

                deferred.resolve(response);
            })
            .error(function(error) {
                logOut();
                deferred.reject(error);
            });

        return deferred.promise;
    };

    var fillAuthData = function() {
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

    function clearData() {
        authentication.isAuth = false;
        authentication.email = "";
        authentication.emailConfirmed = false;
        authentication.userName = "";
        authentication.phoneNumber = "";
        authentication.firstName = "";
        authentication.lastName = "";
        authentication.type = "";
    }

    function fromModel(model) {
        authentication.isAuth = true;
        authentication.email = model.email;
        authentication.emailConfirmed = model.emailConfirmed;
        authentication.userName = model.userName;
        authentication.phoneNumber = model.phoneNumber;
        authentication.firstName = model.firstName;
        authentication.lastName = model.lastName;
        authentication.type = model.type;
    }

    authServiceFactory.saveRegistration = saveRegistration;
    authServiceFactory.login = login;
    authServiceFactory.logOut = logOut;
    authServiceFactory.fillAuthData = fillAuthData;
    authServiceFactory.authentication = authentication;

    return authServiceFactory;
}