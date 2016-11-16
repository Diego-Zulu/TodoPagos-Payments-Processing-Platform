(function () {
    'use strict';

    var todoPagosApp = angular.module('TodoPagos', ['ngRoute']);

    todoPagosApp.factory('authInterceptor', function ($q, $window) {
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

    todoPagosApp.config(function ($httpProvider, $routeProvider) {
        $httpProvider.interceptors.push('authInterceptor');

        $routeProvider.when('/providers/post', {
            controller: 'Providers.Controller',
            controllerAs: 'ctrl',
            templateUrl: 'scripts/app/providers/ProvidersCreateTemplate.html'
        })
            .when('/providers/get', {
                controller: 'Providers.Controller',
                controllerAs: 'ctrl',
                templateUrl: 'scripts/app/providers/ProvidersListTemplate.html'
            })

            .when('/providers/put', {
                controller: 'Providers.Controller',
                controllerAs: 'ctrl',
                templateUrl: 'scripts/app/providers/ProvidersUpdateTemplate.html'
            })

            .when('/providers/delete', {
                controller: 'Providers.Controller',
                controllerAs: 'ctrl',
                templateUrl: 'scripts/app/providers/ProvidersDeleteTemplate.html'
            })

        .when('/clients/get', {
            controller: 'Clients.Controller',
            controllerAs: 'ctrl',
            templateUrl: 'scripts/app/clients/ClientsListTemplate.html'
        })

        .when('/clients/post', {
            controller: 'Clients.Controller',
            controllerAs: 'ctrl',
            templateUrl: 'scripts/app/clients/ClientsCreateTemplate.html'
        })

        .when('/clients/put', {
            controller: 'Clients.Controller',
            controllerAs: 'ctrl',
            templateUrl: 'scripts/app/clients/ClientsUpdateTemplate.html'
        })

        .when('/clients/delete', {
            controller: 'Clients.Controller',
            controllerAs: 'ctrl',
            templateUrl: 'scripts/app/clients/ClientsDeleteTemplate.html'
        })

        .when('/users/get', {
            controller: 'Users.Controller',
            controllerAs: 'ctrl',
            templateUrl: 'scripts/app/users/UsersListTemplate.html'
        })

        .when('/users/post', {
            controller: 'Users.Controller',
            controllerAs: 'ctrl',
            templateUrl: 'scripts/app/users/UsersCreateTemplate.html'
        })

        .when('/users/put', {
            controller: 'Users.Controller',
            controllerAs: 'ctrl',
            templateUrl: 'scripts/app/users/UsersUpdateTemplate.html'
        })

        .when('/users/delete', {
            controller: 'Users.Controller',
            controllerAs: 'ctrl',
            templateUrl: 'scripts/app/users/UsersDeleteTemplate.html'
        })

        .when('/payments/get', {
            controller: 'Payments.Controller',
            controllerAs: 'ctrl',
            templateUrl: 'scripts/app/payments/PaymentsListTemplate.html'
        })

        .when('/payments/post', {
            controller: 'Payments.Controller',
            controllerAs: 'ctrl',
            templateUrl: 'scripts/app/payments/PaymentsCreateTemplate.html'
        })

        .when('/query/earnings/earningsPerProvider', {
            controller: 'EarningQueries.Controller',
            controllerAs: 'ctrl',
            templateUrl: 'scripts/app/earningQueries/PerProviderEarningsTemplate.html'
        })

        .when('/query/earnings/allEarnings', {
            controller: 'EarningQueries.Controller',
            controllerAs: 'ctrl',
            templateUrl: 'scripts/app/earningQueries/TotalEarningsTemplate.html'
        })

        .when('/login/', {
                controller: 'Login.Controller',
                controllerAs: 'ctrl',
                templateUrl: 'scripts/app/login/LoginTemplate.html'
        })

        .otherwise({
            redirectTo: '/login/'
        })
    });
})();