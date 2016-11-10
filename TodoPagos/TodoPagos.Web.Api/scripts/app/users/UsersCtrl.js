(function () {
    'use strict';

    var todoPagosApp = angular.module('TodoPagos');

    todoPagosApp.controller('Users.Controller', function ($http, $log) {

        $http.get('http://localhost:4128')
            .success(function(result){
                ctrl.users = result;
            })
            .error(function(data, status){
                $log.error(data);
            });
    })
})();