(function () {
    'use strict';

    var todoPagosApp = angular.module('TodoPagos');

    todoPagosApp.controller('Users.Controller', ["$http", "$scope", "$log", function ($http, $scope, $log) {

        var ctrl = this;

        $scope.go = function () {

            var path = window.location.hostname;

            $http.get('/api/v1/users')
            .success(function (result) {
                ctrl.users = result;
                $('#alert_placeholder').html();
            })
            .error(function (data, status) {
                $('#alert_placeholder').html('<div class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error: No se pudo traer a los clientes. Código: '+status+'</span></div>')
            });
        }
        
    }])
})();