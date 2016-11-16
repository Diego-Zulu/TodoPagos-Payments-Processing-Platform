(function () {
    'use strict';

    var todoPagosApp = angular.module('TodoPagos');

    todoPagosApp.controller('Login.Controller', function ($scope, $http, $window, $location) {

        var ctrl = this;

        $scope.IsLoggedIn = function () {

            return $window.sessionStorage.token !== undefined;
        }
        
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
                $('#alert_placeholder').html('<div id="alertMessage"  class="alert alert-info"><a class="close" data-dismiss="alert">×</a><span>Bienvenido</span></div>')
                $scope.LimitAccessByRole();
                $location.path("/");
            })
              .error(function (data, status, headers, config) {

                  delete $window.sessionStorage.token;
                  $(".control-access").hide();
                  $('#alert_placeholder').html('<div id="alertMessage"  class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Email o Pass inválida</span></div>')
              });
            $('#alertMessage').fadeOut(2000, null);
        };

        $scope.Logout = function () {
            
            delete $window.sessionStorage.token;
            $(".control-access").hide();
            $location.path("/");
        };

        $scope.LimitAccessByRole = function () {

            $http.get('/api/v1/users/getRoles?userEmail=' + ctrl.namelogin)
            .success(function (result) {
                for (var i = 0; i < result.length; i++) {
                    $("." + result[i]).show();
                }
            })
            .error(function (data, status) {
                $('#alert_placeholder').html('<div id="alertMessage" class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error: No se pudo traer a los usuarios. Código: ' + status + '</span></div>')
                $('#alertMessage').fadeOut(2000, null);
            });
        }
    })
})();