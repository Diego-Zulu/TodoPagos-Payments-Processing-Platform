(function () {
    'use strict';

    var todoPagosApp = angular.module('TodoPagos');

    todoPagosApp.controller('Users.Controller', function ($http, $scope) {

        
        var ctrl = this;

        $scope.$on('GetUsers', function (e) {

            $scope.GetAllUsers();
        });

        $scope.GetAllUsers = function () {

            $http.get('/api/v1/users')
            .success(function (result) {
                if (result.length == 0) {
                    ctrl.users = [{ ID: '*', Name: '-Ninguno-', Email: '-Ninguno-', Roles: [] }];
                } else {
                    ctrl.users = result;
                }
            })
            .error(function (data, status) {
                $('#alert_placeholder').html('<div class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error: No se pudo traer a los usuarios. Código: ' + status + '</span></div>')
            });
        }

        $scope.CreateUser = function () {

            var rolesList = [];
            $('#CreateUserRolesCheckboxes input:checked').each(function () {
                rolesList.push($(this).val());
            })

            var info = { Name: ctrl.NewUserName, Email: ctrl.NewUserEmail, Password: ctrl.NewUserPassword, Roles: rolesList };

            if (ctrl.NewUserPassword == ctrl.NewUserPasswordConfirm && ctrl.NewUserEmail == ctrl.NewUserEmailConfirm) {
                $http({
                    url: 'api/v1/users',
                    method: 'POST',
                    data: info,
                    headers: {
                        'Content-Type': 'application/json'
                    }
                })
                .success(function (result) {
                    $scope.CleanForm();
                    $('#alert_placeholder').html('<div class="alert alert-success"><a class="close" data-dismiss="alert">×</a><span>Usuario ' + ctrl.NewUserEmail + ' creado</span></div>')
                })
                .error(function (data, status) {
                    $('#alert_placeholder').html('<div class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error ' + status + ': ' + data.Message + '</span></div>')
                });
            } else if (ctrl.NewUserPassword == ctrl.NewUserPasswordConfirm) {
                $('#alert_placeholder').html('<div class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error: La contraseña confirmada y la contraseña escrita no coinciden</span></div>')
            } else {
                $('#alert_placeholder').html('<div class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error: El email confirmado y el email escrito no coinciden</span></div>')
            }
        }

        $scope.UpdateUser = function () {

            var idOfSelectedUser = $("#updateUserSelect option:selected").val();
            var rolesList = [];
            $('#UpdateUserRolesCheckboxes input:checked').each(function () {
                rolesList.push($(this).val());
            })

            var info = { ID: idOfSelectedUser, Name: ctrl.UpdateUserName, Email: ctrl.UpdateUserEmail, Password: ctrl.UpdateUserPassword, Roles: rolesList };

            if (ctrl.UpdateUserPassword == ctrl.UpdateUserPasswordConfirm && ctrl.UpdateUserEmail == ctrl.UpdateUserEmailConfirm) {
                $http({
                    url: 'api/v1/users/' + idOfSelectedUser,
                    method: 'PUT',
                    data: info,
                    headers: {
                        'Content-Type': 'application/json'
                    }
                })
                .success(function (result) {
                    $scope.CleanForm();
                    $scope.GetAllUsers();
                    $('#alert_placeholder').html('<div class="alert alert-success"><a class="close" data-dismiss="alert">×</a><span>Usuario ' + idOfSelectedUser + ' actualizado</span></div>')
                })
                .error(function (data, status) {
                    $('#alert_placeholder').html('<div class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error ' + status + ': ' + data.Message + '</span></div>')
                });
            } else if (ctrl.NewUserPassword == ctrl.NewUserPasswordConfirm) {
                $('#alert_placeholder').html('<div class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error: La contraseña confirmada y la contraseña escrita no coinciden</span></div>')
            } else {
                $('#alert_placeholder').html('<div class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error: El email confirmado y el email escrito no coinciden</span></div>')
            }
        }

        $scope.DeleteUser = function () {

            var idOfSelectedUser = $("#deleteUserSelect option:selected").val();

            $http({
                url: 'api/v1/users/' + idOfSelectedUser,
                method: 'DELETE'
            })
            .success(function (result) {
                $scope.CleanForm();
                $scope.GetAllUsers();
                $('#alert_placeholder').html('<div class="alert alert-success"><a class="close" data-dismiss="alert">×</a><span>Usuario ' + idOfSelectedUser + ' eliminado</span></div>')
            })
            .error(function (data, status) {
                $('#alert_placeholder').html('<div class="alert alert-danger"><a class="close" data-dismiss="alert">×</a><span>Error ' + status + ': ' + data.Message + '</span></div>')
            });
        }

    })
})();