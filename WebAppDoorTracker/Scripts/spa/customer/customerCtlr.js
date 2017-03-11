(function (app) {
    'use strict';
    app.controller('customerCtrl', customerCtrl);
    customerCtrl.$inject = ['$scope', '$location', '$uibModal', '$log', 'apiService', 'notificationService'];

    function customerCtrl($scope, $location, $uibModal, $log, apiService, notificationService) {

        $scope.customers = [];
        $scope.loadingCustomers = true;
        $scope.openEditDialog = openEditDialog;
        $scope.open = open;
        $scope.EditedCustomer = {};
        $scope.animationsEnabled = true;
        $scope.searchCustomer = searchCustomer;
        $scope.showDoors = showDoors;

        apiService.get('api/customer/', null, customerLoadCompleted, customerLoadFailed);

        function customerLoadCompleted(result) {
            $scope.customers = result.data;
            $scope.loadingCustomer = false;
        }

        function customerLoadFailed(result) {
            notificationService.displayError(result.error);
        }

        function searchCustomer(searchvalue) {
            var config = {
                params: {
                    filter: searchvalue
                }
            }
            apiService.get('api/customer/get', config, customerLoadFailed, customerLoadCompleted);
        }
       
        function showDoors(customer) {
            $scope.Customer = customer;

            var modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: '/Scripts/spa/customer/customerDoorsModal.html',
                controller: 'customerDoorsCtrl',
                size: 'lg',
                scope: $scope
            });

            modalInstance.result.then(function (selectedItem) {
                $scope.selected = selectedItem;
            }, function () {
                $log.info('Modal dismissed at: ' + new Date());
            });
        };

        function openEditDialog(customer) {
            $scope.EditedCustomer = customer;
            var modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: '/Scripts/spa/customer/editCustomerModal.html',
                controller: 'customerEditCtrl',
                size: 'lg',
                scope: $scope,
                EditedCustomer: function () {
                    return {
                        EditedCustomer: EditedCustomer
                    };
                }
            });

            modalInstance.result.then(function (selectedItem) {
                $scope.selected = selectedItem;
            }, function () {
                $log.info('Modal dismissed at: ' + new Date());
            });
        };
    }

})(angular.module('DoorTrackerApp'));


