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
        };

        $scope.ShowListUsers = function () {

            $(".well").not("#ListUserDiv").hide(100);

            $("#ListUserDiv").show(400);

            $('#alert_placeholder').html("");

            $scope.$broadcast('GetUsers');
        };

        $scope.ShowCreateUsers = function () {

            $(".well").not("#CreateUserDiv").hide(100);

            $("#CreateUserDiv").show(400);

            $('#alert_placeholder').html("");
        };

        $scope.ShowDeleteUsers = function () {

            $(".well").not("#DeleteUserDiv").hide(100);

            $("#DeleteUserDiv").show(400);

            $('#alert_placeholder').html("");

            $scope.$broadcast('GetUsers');
        };

        $scope.ShowUpdateUsers = function () {

            $(".well").not("#UpdateUserDiv").hide(100);

            $("#UpdateUserDiv").show(400);

            $('#alert_placeholder').html("");

            $scope.$broadcast('GetUsers');
        };

        $scope.ShowListProviders = function () {

            $(".well").not("#ListProviderDiv").hide(100);

            $("#ListProviderDiv").show(400);

            $('#alert_placeholder').html("");

            $scope.$broadcast('GetAllProviders');
        };

        $scope.ShowCreateProviders = function () {

            $(".well").not("#CreateProviderDiv").hide(100);

            $("#CreateProviderDiv").show(400);

            $('#alert_placeholder').html("");
        };

        $scope.ShowDeleteProviders = function () {

            $(".well").not("#DeleteProviderDiv").hide(100);

            $("#DeleteProviderDiv").show(400);

            $('#alert_placeholder').html("");

            $scope.$broadcast('GetActiveProviders');
        };

        $scope.ShowUpdateProviders = function () {

            $(".well").not("#UpdateProviderDiv").hide(100);

            $("#UpdateProviderDiv").show(400);

            $('#alert_placeholder').html("");

            $scope.$broadcast('GetActiveProviders');
        };

        $scope.ShowListPayments = function () {

            $(".well").not("#ListPaymentsDiv").hide(100);

            $("#ListPaymentsDiv").show(400);

            $('#alert_placeholder').html("");

            $scope.$broadcast('GetPayments');
        };

        $scope.ShowCreatePayment = function () {

            $(".well").not("#CreatePaymentDiv").hide(100);

            $("#CreatePaymentDiv").show(400);

            $('#alert_placeholder').html("");

            $scope.$broadcast('GetActiveProviders');
        };

        $scope.ShowEarningsPerProvider = function () {

            $(".well").not("#ReportPerProviderDiv").hide(100);

            $("#ReportPerProviderDiv").show(400);

            $('#alert_placeholder').html("");
        };

        $scope.ShowTotalEarnings = function () {

            $(".well").not("#ReportTotalDiv").hide(100);

            $("#ReportTotalDiv").show(400);

            $('#alert_placeholder').html("");
        };

        $scope.CleanForm = function () {

            $('form').trigger("reset");
        };
    })
})();