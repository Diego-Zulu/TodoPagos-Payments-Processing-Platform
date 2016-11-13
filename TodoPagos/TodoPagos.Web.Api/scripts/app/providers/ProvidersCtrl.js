(function () {
    'use strict';

    var todoPagosApp = angular.module('TodoPagos');

    todoPagosApp.controller('Providers.Controller', function ($http, $scope) {

        var ctrl = this;

        $scope.$on('GetAllProviders', function (e) {

            $scope.GetAllProviders();
        });

        $scope.$on('GetActiveProviders', function (e) {

            $scope.GetProviders(true);
        });

        $scope.GetAllProviders = function () {

            $http.get('/api/v1/providers')
            .success(function (result) {
                if (result.length == 0) {
                    ctrl.allProviders = [{ ID: '*', Name: '-Ninguno-', Active: false, Commission: 0, Fields: [] }];
                } else {
                    ctrl.allProviders = result;
                }
            })
            .error(function (data, status) {
                $('#alert_placeholder').html('<div class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error: No se pudo traer a los proveedores. Código: ' + status + '</span></div>')
            });
        }

        $scope.GetProviders = function (active) {

            console.log(active);
            $http.get('/api/v1/providers?getActiveProviders=' + active)
            .success(function (result) {
                if (result.length == 0) {
                    ctrl.activeProviders = { ID: '*', Name: '-Ninguno-', Commission: 0, Fields: [] };
                } else {
                    ctrl.activeProviders = result;
                }
                console.log(ctrl.activeProviders);
            })
            .error(function (data, status) {
                $('#alert_placeholder').html('<div class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error: No se pudo traer a los proveedores. Código: ' + status + '</span></div>')
            });
        }

        $scope.DeleteProvider = function () {

            var idOfSelectedProvider = $("#deleteProviderSelect option:selected").val();

            $http({
                url: 'api/v1/providers/' + idOfSelectedProvider,
                method: 'DELETE'
            })
            .success(function (result) {
                $scope.CleanForm();
                $scope.GetAllClients();
                $('#alert_placeholder').html('<div class="alert alert-success"><a class="close" data-dismiss="alert">×</a><span>Proveedor ' + idOfSelectedProvider + ' marcado como eliminado</span></div>')
            })
            .error(function (data, status) {
                $('#alert_placeholder').html('<div class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error ' + status + ': ' + data.Message + '</span></div>')
            });
        }
    })
})();