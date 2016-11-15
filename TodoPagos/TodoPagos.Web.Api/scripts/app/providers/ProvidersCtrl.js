(function () {
    'use strict';

    var todoPagosApp = angular.module('TodoPagos');

    todoPagosApp.controller('Providers.Controller', function ($http, $scope) {

        var ctrl = this;

        ctrl.NewFieldName = [];

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
                $('#alert_placeholder').html('<div id="alertMessage" class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error: No se pudo traer a los proveedores. Código: ' + status + '</span></div>')
                $('#alertMessage').fadeOut(2000, null);
            });
        }

        $scope.GetProviders = function (active) {

            console.log(active);
            $http.get('/api/v1/providers?getActiveProviders=' + active)
            .success(function (result) {
                if (result.length == 0) {
                    ctrl.activeProviders = [{ ID: '*', Name: '-Ninguno-', Commission: 0, Fields: [] }];
                } else {
                    ctrl.activeProviders = result;
                }
                console.log(ctrl.activeProviders);
            })
            .error(function (data, status) {
                $('#alert_placeholder').html('<div id="alertMessage" class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error: No se pudo traer a los proveedores. Código: ' + status + '</span></div>')
                $('#alertMessage').fadeOut(2000, null);
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
                $scope.GetProviders(true);
                $('#alert_placeholder').html('<div id="alertMessage" class="alert alert-success"><a class="close" data-dismiss="alert">×</a><span>Proveedor ' + idOfSelectedProvider + ' marcado como eliminado</span></div>')
            })
            .error(function (data, status) {
                $('#alert_placeholder').html('<div id="alertMessage" class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error ' + status + ': ' + data.Message + '</span></div>')
            });
            $('#alertMessage').fadeOut(2000, null);
        }

        $scope.DisplayProviderFieldsOnDialog = function (aProvider) {

            var fieldsInHtml = "";

            for (var i = 0; i < aProvider.Fields.length; i++) {
                fieldsInHtml += "<p>" + (i+1) + ". " + aProvider.Fields[i].Name + " (Tipo: " + aProvider.Fields[i].Type + ")</p>";
            }

            $("#ProviderFieldsModalTitle").text("Campos de " + aProvider.Name);
            $("#ProviderFieldsModalText").html(fieldsInHtml);
            $("#ProviderFieldsModal").modal("toggle");
        }

        $scope.CreateProvider = function () {

            var newProviderFields = [];

            for (var i = 0; i < ctrl.NewFieldName.length; i++) {
                newProviderFields.push({ Name: ctrl.NewFieldName[i], Type: $("#NewProviderFieldType" + i).val() });
            }

            var info = { Name: ctrl.NewProviderName, Commission: ctrl.NewProviderComission, Fields: newProviderFields };

                $http({
                    url: 'api/v1/providers',
                    method: 'POST',
                    data: info,
                    headers: {
                        'Content-Type': 'application/json'
                    }
                })
                .success(function (result) {
                    $scope.CleanForm();
                    $('#alert_placeholder').html('<div id="alertMessage" class="alert alert-success"><a class="close" data-dismiss="alert">×</a><span>Proveedor ' + ctrl.NewProviderName + ' creado</span></div>')
                })
                .error(function (data, status) {
                    $('#alert_placeholder').html('<div id="alertMessage" class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error ' + status + ': ' + data.Message + '</span></div>')
                });
                $('#alertMessage').fadeOut(2000, null);
        }

        $scope.UpdateProvider = function () {

            var idOfSelectedProvider = $("#modifyProviderSelect option:selected").val();
            var updateProviderFields = [];

            for (var i = 0; i < ctrl.UpdateFieldName.length; i++) {
                updateProviderFields.push({ Name: ctrl.UpdateFieldName[i], Type: $("#UpdateProviderFieldType" + i).val() });
            }

            var info = {ID: idOfSelectedProvider, Name: ctrl.NewProviderName, Commission: ctrl.NewProviderComission, Fields: newProviderFields };

            $http({
                url: 'api/v1/providers/' + idOfSelectedProvider,
                method: 'POST',
                data: info,
                headers: {
                    'Content-Type': 'application/json'
                }
            })
            .success(function (result) {
                $scope.CleanForm();
                $scope.GetProviders(true);
                $('#alert_placeholder').html('<div id="alertMessage" class="alert alert-success"><a class="close" data-dismiss="alert">×</a><span>Proveedor ' + idOfSelectedProvider + ' actualizado</span></div>')
            })
            .error(function (data, status) {
                $('#alert_placeholder').html('<div id="alertMessage" class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error ' + status + ': ' + data.Message + '</span></div>')
            });
            $('#alertMessage').fadeOut(2000, null);
        }

        $scope.GetTargetLengthArray = function (number) {
            var numbersArray = [];

            for (var i = 0; i < number; i++) {
                numbersArray.push(i);
            }

            return numbersArray;
        }

        ctrl.PrintYesOrNoAcoordingToBool = function (aBoolean) {

            if (aBoolean) {
                return "Si";
            } else {
                return "No";
            }
        }

        $scope.CleanForm = function () {

            $('form').trigger("reset");
        };
    })
})();