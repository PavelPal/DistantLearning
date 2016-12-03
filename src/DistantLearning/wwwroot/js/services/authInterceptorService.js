app.factory("authInterceptorService", authInterceptorService);

function authInterceptorService($q, $location, localStorageService) {

    var authInterceptorServiceFactory = {};

    var request = function (config) {

        config.headers = config.headers || {};

        var authData = localStorageService.get("authorizationData");
        if (authData) {
            config.headers.Authorization = "Bearer " + authData.token;
        }

        return config;
    };

    var responseError = function (rejection) {
        if (rejection.status === 401) {
            $location.path("/");
        }
        return $q.reject(rejection);
    };

    authInterceptorServiceFactory.request = request;
    authInterceptorServiceFactory.responseError = responseError;

    return authInterceptorServiceFactory;
}