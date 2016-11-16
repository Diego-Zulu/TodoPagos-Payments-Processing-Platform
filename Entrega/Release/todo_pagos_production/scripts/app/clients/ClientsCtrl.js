(function () {
    'use strict';

    var todoPagosApp = angular.module('TodoPagos');

    todoPagosApp.controller('Clients.Controller', function ($http, $scope) {

        var ctrl = this;

        $scope.GetAllClients = function () {

            $http.get('/api/v1/clients')
            .success(function (result) {
                if (result.length == 0) {
                    ctrl.clients = [{ ID: '*', Name: '-Ninguno-', IDCard: '-Ninguna-', PhoneNumber: '-Ninguno-', Address: '-Ninguno-', Points: '0' }];
                } else {
                    ctrl.clients = result;
                }
            })
            .error(function (data, status) {
                $('#alert_placeholder').html('<div id="alertMessage" class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error: No se pudo traer a los clientes. Código: ' + status + '</span></div>')
                $('#alertMessage').fadeOut(2000, null);
            });
        }

        $scope.CreateClient = function () {

            var info = { Name: ctrl.namenewclient, IDCard: ctrl.idcardnewclient, PhoneNumber: ctrl.phonenewclient, Address: ctrl.addressnewclient};

            if (ctrl.idcardnewclient == ctrl.idcardconfirmnewclient) {
            $http({
                url: 'api/v1/clients',
                method: 'POST',
                data: info,
                headers: {
                    'Content-Type': 'application/json'
            }
            })
            .success(function (result) {
                $scope.CleanForm();
                $('#alert_placeholder').html('<div id="alertMessage" class="alert alert-success"><a class="close" data-dismiss="alert">×</a><span>Cliente ' + ctrl.idcardnewclient + ' creado</span></div>')
            })
            .error(function (data, status) {
                $('#alert_placeholder').html('<div id="alertMessage" class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error ' + status + ': ' + data.Message + '</span></div>')
            });
            } else {
                $('#alert_placeholder').html('<div id="alertMessage" class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error: La cédula confirmada y la cédula escrita no coinciden</span></div>')
            }
            $('#alertMessage').fadeOut(2000, null);
        }

        $scope.UpdateClient = function () {

            var idOfSelectedClient = $("#updateClientSelect option:selected").val();
            var info = { ID: idOfSelectedClient, Name: ctrl.nameupdateclient, IDCard: ctrl.idcardupdateclient, PhoneNumber: ctrl.phoneupdateclient, Address: ctrl.addressupdateclient };

            if (ctrl.idcardupdateclient == ctrl.idcardconfirmupdateclient) {
                $http({
                    url: 'api/v1/clients/' + idOfSelectedClient,
                    method: 'PUT',
                    data: info,
                    headers: {
                        'Content-Type': 'application/json'
                    }
                })
                .success(function (result) {
                    $scope.CleanForm();
                    $scope.GetAllClients();
                    $('#alert_placeholder').html('<div id="alertMessage" class="alert alert-success"><a class="close" data-dismiss="alert">×</a><span>Cliente ' + idOfSelectedClient + ' actualizado</span></div>')
                })
                .error(function (data, status) {
                    $('#alert_placeholder').html('<div id="alertMessage" class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error ' + status + ': ' + data.Message + '</span></div>')
                });
            } else {
                $('#alert_placeholder').html('<div id="alertMessage" class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error: La cédula confirmada y la cédula escrita no coinciden</span></div>')
            }
            $('#alertMessage').fadeOut(2000, null);
        }

        $scope.DeleteClient = function () {

            var idOfSelectedClient = $("#deleteClientSelect option:selected").val();

                $http({
                    url: 'api/v1/clients/' + idOfSelectedClient,
                    method: 'DELETE'
                })
                .success(function (result) {
                    $scope.CleanForm();
                    $scope.GetAllClients();
                    $('#alert_placeholder').html('<div id="alertMessage" class="alert alert-success"><a class="close" data-dismiss="alert">×</a><span>Cliente ' + idOfSelectedClient + ' eliminado</span></div>')
                })
                .error(function (data, status) {
                    $('#alert_placeholder').html('<div id="alertMessage" class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error ' + status + ': ' + data.Message + '</span></div>')
                });
                $('#alertMessage').fadeOut(2000, null);
        }

        $scope.CleanForm = function () {

            $('form').trigger("reset");
        };
    })
})();