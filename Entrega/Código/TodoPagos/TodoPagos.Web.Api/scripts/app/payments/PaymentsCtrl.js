(function () {
    'use strict';

    var todoPagosApp = angular.module('TodoPagos');

    todoPagosApp.controller('Payments.Controller', function ($http, $scope) {

        var ctrl = this;

        ctrl.NewPaymentReceiptAmount = [];

        ctrl.SelectedProviderFields = [];

        ctrl.selectedProvider = []    

        $scope.GetAllPayments = function () {

            $http.get('/api/v1/payments')
            .success(function (result) {
                if (result.length == 0) {
                    ctrl.payments = [{ ID: '*', PayMethod: {}, PaidWith: 0, Change: 0, Total: 0, Receipts: [] }];
                } else {
                    ctrl.payments = result;
                }
            })
            .error(function (data, status) {
                $('#alert_placeholder').html('<div id="alertMessage" class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error: No se pudo traer a los pagos. Código: ' + status + '</span></div>')
                $('#alertMessage').fadeOut(2000, null);
            });
        }

        $scope.DisplayReceiptOnDialog = function (aReceipt) {

            var receiptInHtml = "";
            receiptInHtml += "<h5><strong>Proveedor:</strong> " + aReceipt.ReceiptProvider.Name + "(Comisión: " + aReceipt.ReceiptProvider.Commission + "%)</h5>";
            receiptInHtml += "<h5><strong>Costo:</strong> $" + aReceipt.Amount + "</h5>";
            receiptInHtml += "<h5><strong>Campos completados:</strong></h5>";

            for (var i = 0; i < aReceipt.CompletedFields.length; i++) {
                receiptInHtml += "<p>" + (i + 1) + ". " + aReceipt.CompletedFields[i].Name + " (<u>Tipo:</u> " + aReceipt.CompletedFields[i].Type + ") => <u>Dato:</u> " + aReceipt.CompletedFields[i].Data + "</p>";
            }

            $("#PaymentReceiptModalTitle").text("Factura de ID " + aReceipt.ID + ":");
            $("#PaymentReceiptModalText").html(receiptInHtml);
            $("#PaymentReceiptModal").modal("toggle");
        }

        $scope.CreatePayment = function () {

            console.log("hihi");
            var newPaymentReceipts = [];
            var thisPayDate = new Date($('#SelectPayDate').val()).toISOString().split('.')[0] + "Z";
            var chosenPayMethod = { Type: $("#paymentMethodSelect").val(), PayDate: thisPayDate };

            for (var i = 0; i < ctrl.NewPaymentReceiptAmount.length; i++) {
                newPaymentReceipts.push({ Amount: ctrl.NewPaymentReceiptAmount[i], ReceiptProviderID: ctrl.selectedProvider[i].ID, CompletedFields: ctrl.selectedProvider[i].Fields });
            }

            var info = { AmountPaid: ctrl.NewPaymentPaidWith, PayMethod: chosenPayMethod, Receipts: newPaymentReceipts };
            console.log(info);
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
                $('#alert_placeholder').html('<div id="alertMessage" class="alert alert-success"><a class="close" data-dismiss="alert">×</a><span>Pago por $' + parseInt(result.Total) + ' creado. Vuelto: $' + parseInt(result.Change) + '</span></div>')
                console.log(result);
            })
            .error(function (data, status) {
                $('#alert_placeholder').html('<div id="alertMessage" class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error ' + status + ': ' + data.Message + '</span></div>')
                console.log("chau");
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

       
        $scope.CleanForm = function () {

            $('form').trigger("reset");
        };

        $('#PayDateDatetimepicker').datetimepicker({
            defaultDate: new Date(),
            maxDate: 'now'
        });
    })
})();