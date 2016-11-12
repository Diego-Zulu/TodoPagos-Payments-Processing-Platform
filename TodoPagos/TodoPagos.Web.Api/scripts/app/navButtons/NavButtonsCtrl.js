(function () {
    'use strict';

    var todoPagosApp = angular.module('TodoPagos');

    todoPagosApp.controller('NavButtons.Controller', function ($scope) {

        var ctrl = this;

        $scope.ShowListClients = function () {

            $(".well").not("#ListClientDiv").hide(100);

            $("#ListClientDiv").show(400);

            $('#alert_placeholder').html("");

            $scope.$broadcast('GetClients');
        };

        $scope.ShowUpdateClients = function () {

            $(".well").not("#UpdateClientDiv").hide(100);

            $("#UpdateClientDiv").show(400);

            $('#alert_placeholder').html("");

            $scope.$broadcast('GetClients');
        };

        $scope.ShowDeleteClients = function () {

            $(".well").not("#DeleteClientDiv").hide(100);

            $("#DeleteClientDiv").show(400);

            $('#alert_placeholder').html("");

            $scope.$broadcast('GetClients');
        };

        $scope.ShowCreateClients = function () {

            $(".well").not("#CreateClientDiv").hide(100);

            $("#CreateClientDiv").show(400);

            $('#alert_placeholder').html("");

            $scope.$broadcast('GetClients');
        };

        $scope.ShowListUsers = function () {

            $(".well").not("#ListUserDiv").hide(100);

            $("#ListUserDiv").show(400);

            $('#alert_placeholder').html("");

            $scope.$broadcast('GetUsers');
        };

        $scope.ShowListProviders = function () {

            $(".well").not("#ListProviderDiv").hide(100);

            $("#ListProviderDiv").show(400);

            $('#alert_placeholder').html("");

            $scope.$broadcast('GetProviders');
        };

        $scope.CleanForm = function () {

            $('form').trigger("reset");
        };
    })
})();