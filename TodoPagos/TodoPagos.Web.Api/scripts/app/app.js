(function () {
    'use strict';

    var todoPagosApp = angular.module('TodoPagos', []);

    todoPagosApp.config(function ($routeProvider) {

        $routeProvider.when('providers', {
            controller: 'ProvidersController',
            contrllerAs: 'ctrl',
            templateUrl: 'providers/providers.html'
        })

    })
    
    todoPagosApp.factory('authInterceptor', function ($rootScope, $q, $window) {
        return {
            request: function (config) {
                config.headers = config.headers || {};
                if ($window.sessionStorage.token) {
                    config.headers.Authorization = 'Bearer ' + $window.sessionStorage.token;
                }
                return config;
            },
            response: function (response) {
               
                return response || $q.when(response);
            }
        };
    });

    todoPagosApp.config(function ($httpProvider) {
        $httpProvider.interceptors.push('authInterceptor');
    });
})();