(function () {
    'use strict';

    var todoPagosApp = angular.module('TodoPagos');

    todoPagosApp.controller('EarningQueries.Controller', function ($scope, $http) {

        var ctrl = this;

        $scope.GetEarningsPerProvider = function () {

            console.log("Hola");
            var fromDate = new Date($('#SelectFromDatePerProvider').val()).toISOString().split('.')[0] + "Z";
            var toDate = new Date($('#SelectToDatePerProvider').val()).toISOString().split('.')[0] + "Z";

            console.log("Paso");
            $http.get('/api/v1/query/earnings/earningsPerProvider?from=' + fromDate + "&to=" + toDate)
            .success(function (result) {
                console.log(result);
                if (ctrl.PerProviderResultsAreEmpty(result)) {
                    ctrl.perProviderResults = { "-Ninguno-": "0" };
                } else {
                    ctrl.perProviderResults = result;
                }
                console.log(result);
                console.log(ctrl.PerProviderResultsAreEmpty(result));
                
            })
            .error(function (data, status) {
                $('#alert_placeholder').html('<div id="alertMessage" class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error: No se pudo traer a las ganancias por proveedor. Código: ' + status + '</span></div>')
                $('#alertMessage').fadeOut(2000, null);
            });
        }


        ctrl.PerProviderResultsAreEmpty = function (result) {
            for (var key in result) {
                if (result.hasOwnProperty(key)) {
                    return false;
                }
            }
            return true;
        }

        $scope.GetTotalEarnings = function () {
            var fromDate = new Date($('#SelectFromDateTotalEarnings').val()).toISOString().split('.')[0] + "Z";
            var toDate = new Date($('#SelectToDateTotalEarnings').val()).toISOString().split('.')[0] + "Z";
 
            $http.get('/api/v1/query/earnings/allEarnings?from=' + fromDate + "&to=" + toDate)
            .success(function (result) {
                ctrl.totalResult = result;
            })
            .error(function (data, status) {
                $('#alert_placeholder').html('<div id="alertMessage" class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error: No se pudo las ganancias totales. Código: ' + status + '</span></div>')
                $('#alertMessage').fadeOut(2000, null);
            });
        }

        $scope.CleanForm = function () {

            $('form').trigger("reset");
        };

        $('#FromDatetimepickerPerProvider').datetimepicker({
            defaultDate: new Date()
        });

        $('#ToDatetimepickerPerProvider').datetimepicker({
            defaultDate: new Date()
        });

        $('#FromDatetimepickerTotalEarnings').datetimepicker({
            defaultDate: new Date()
        });

        $('#ToDatetimepickerTotalEarnings').datetimepicker({
            defaultDate: new Date()
        });
    })
})();