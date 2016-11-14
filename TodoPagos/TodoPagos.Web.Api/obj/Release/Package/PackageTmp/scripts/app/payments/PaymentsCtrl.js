(function () {
    'use strict';

    var todoPagosApp = angular.module('TodoPagos');

    todoPagosApp.controller('Payments.Controller', function ($http, $scope) {

        var ctrl = this;

        ctrl.NewFieldName = [];

        /*$scope.$on('GetAllPayments', function (e) {

            $scope.GetAllPayments();
        });

        $scope.GetAllPayments = function () {

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

        $scope.DisplayProviderFieldsOnDialog = function (aProvider) {

            var fieldsInHtml = "";

            for (var i = 0; i < aProvider.Fields.length; i++) {
                fieldsInHtml += "<p>" + (i + 1) + ". " + aProvider.Fields[i].Name + " (Tipo: " + aProvider.Fields[i].FieldTypeName + ")</p>";
            }

            $("#ProviderFieldsModalTitle").text("Campos de " + aProvider.Name);
            $("#ProviderFieldsModalText").html(fieldsInHtml);
            $("#ProviderFieldsModal").modal("toggle");
        }*/

        $scope.CreatePayment = function () {

            var newPaymentReceipts = [];
            var thisPayDate = new Date($('#SelectPayDate').val()).toISOString().split('.')[0] + "Z";
            var chosenPayMethod = { Type: $("#paymentMethodSelect").val(), PayDate: thisPayDate };

            for (var i = 0; i < ctrl.NewFieldName.length; i++) {
                newPaymentReceipts.push({ Amount: ctrl.NewPaymentReceiptAmount[i], ReceiptProviderID: $("#CreatePaymentReceiptProvider" + receiptNumber).val().ID, CompletedFields: ctrl.SelectedProviderFields });
            }

            var info = { AmountPaid: ctrl.NewPaymentPaidWith, PayMethod: chosenPayMethod, Receipts: newPaymentReceipts };

            $http({
                url: 'api/v1/payments',
                method: 'POST',
                data: info,
                headers: {
                    'Content-Type': 'application/json'
                }
            })
            .success(function (result) {
                $scope.CleanForm();
                $('#alert_placeholder').html('<div class="alert alert-success"><a class="close" data-dismiss="alert">×</a><span>Pago por $' + ctrl.NewPaymentTotal + ' creado</span></div>')
            })
            .error(function (data, status) {
                $('#alert_placeholder').html('<div class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error ' + status + ': ' + data.Message + '</span></div>')
            });
        }

        $scope.GetTargetLengthArray = function (number) {
            var numbersArray = [];

            for (var i = 0; i < number; i++) {
                numbersArray.push(i);
            }

            return numbersArray;
        }

        $scope.LoadFieldsIntoReceiptFields = function (receiptNumber) {

            ctrl.SelectedProviderFields = $("#CreatePaymentReceiptProvider" + receiptNumber).val().Fields;
            
        }

        $scope.UpdatePaymentTotal = function () {
            var total = 0;

            for (var i = 0; i < ctrl.NewPaymentReceiptAmount.length; i++) {
                total += ctrl.NewPaymentReceiptAmount[i];
            }

            ctrl.NewPaymentTotal = total;
        }
    })
})();