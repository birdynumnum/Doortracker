(function (app) {
    'use strict';
    app.controller('customerEditCtrl', customerEditCtrl);
    customerEditCtrl.$inject = ['$scope', '$uibModalInstance', '$timeout', 'apiService', 'notificationService'];

    function customerEditCtrl($scope, $uibModalInstance, $timeout, apiService, notificationService) {

        $scope.cancelEdit = cancelEdit;
        $scope.updateCustomer = updateCustomer;

        $scope.openDatePicker = openDatePicker;
        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };
        $scope.datepicker = {};

        function updateCustomer() {
            apiService.post('/api/customer/Update/', $scope.EditedCustomer,
            updateCustomerCompleted,
            updateCustomerLoadFailed);
        }

        function updateCustomerCompleted(response) {
            notificationService.displaySuccess($scope.EditedCustomer.firstName + ' ' + $scope.EditedCustomer.lastName + ' has been updated');
            $uibModalInstance.dismiss();
            $scope.EditedCustomer = {};
        }

        function updateCustomerLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function cancelEdit() {
            $scope.isEnabled = false;
            $uibModalInstance.dismiss();
        }

        function openDatePicker($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $timeout(function () {
                $scope.datepicker.opened = true;
            });

            $timeout(function () {
                $('ul[datepicker-popup-wrap]').css('z-index', '1050', '!important');
            }, 100);
        };

    }

})(angular.module('DoorTrackerApp'));