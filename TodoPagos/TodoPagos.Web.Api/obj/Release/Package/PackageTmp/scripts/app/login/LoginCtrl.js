(function () {
    'use strict';

    var todoPagosApp = angular.module('TodoPagos');

    todoPagosApp.controller('Login.Controller', function ($scope, $http, $window) {

        var ctrl = this;
        
        $scope.Login = function () {
            var info = { grant_type: 'password', username: ctrl.namelogin, password: ctrl.passlogin };
            $http({
                url: 'api/v1/login',
                method: 'POST',
                transformRequest: function (obj) {
                    var str = [];
                    for (var p in obj)
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                    return str.join("&");
                },
                data: info, 
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded' 
                }
            }).success(function (data, status, headers, config) {
                $window.sessionStorage.token = data.access_token;
                  $('#alert_placeholder').html('<div class="alert alert-info"><a class="close" data-dismiss="alert">×</a><span>Bienvenido</span></div>')
              })
              .error(function (data, status, headers, config) {

                  delete $window.sessionStorage.token;

                  $('#alert_placeholder').html('<div class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Email o Pass inválida</span></div>')
              });
        };

        $scope.Logout = function () {
            
            delete $window.sessionStorage.token;
        };
    })
})();