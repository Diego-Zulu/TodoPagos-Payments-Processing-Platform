(function () {
    'use strict';

    var todoPagosApp = angular.module('TodoPagos');

    todoPagosApp.controller('Users.Controller', function ($http, $scope) {

        var ctrl = this;

        $scope.$on('GetUsers', function (e) {

            $scope.GetAllUsers();
        });

        $scope.GetAllUsers = function () {

            var path = window.location.hostname;

            $http.get('/api/v1/users')
            .success(function (result) {
                ctrl.users = result;
            })
            .error(function (data, status) {
                $('#alert_placeholder').html('<div class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error: No se pudo traer a los usuarios. Código: ' + status + '</span></div>')
            });
        }

    })
})();