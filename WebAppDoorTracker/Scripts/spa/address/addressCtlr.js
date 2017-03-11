(function (app) {
    'use strict';
    app.controller('addressCtrl', addressCtrl);
    addressCtrl.$inject = ['$scope', '$location', '$uibModal', '$log', 'apiService', 'notificationService'];

    function addressCtrl($scope, $location, $uibModal, $log, apiService, notificationService) {

        $scope.address = [];
        $scope.loadingAddress = true;
        $scope.openEditDialog = openEditDialog;
        $scope.open = open;
        $scope.EditedAddress = {};
        $scope.animationsEnabled = true;
        $scope.searchAddress = searchAddress;
        $scope.openShowCustomer = openShowCustomer;

        apiService.get('api/address/', null, addressLoadCompleted, addressLoadFailed);

        function addressLoadCompleted(result) {
            $scope.address = result.data;
            $scope.loadingAddress = false;
        }

        function addressLoadFailed(result) {
            notificationService.displayError(result.error);
        }

        function searchAddress(searchvalue) {
            var config = {
                params: {
                    filter: searchvalue
                }
            }
            apiService.get('api/address/get', config, addressLoadFailed, addressLoadCompleted);
        }

        //function openShowCustomer(address) {
        //    var config = {
        //        params: {
        //            id: address.id
        //        }
        //    }
        //    apiService.get('api/address/showCustomer/', config, addressLoadFailed, addressLoadCompleted);
        //}


        function openShowCustomer(address) {
            $scope.Address = address;

            var modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: '/Scripts/spa/address/addressCustomerModal.html',
                controller: 'addressCustomerCtrl',
                size: 'lg',
                scope: $scope
            });

            modalInstance.result.then(function (selectedItem) {
                $scope.selected = selectedItem;
            }, function () {
                $log.info('Modal dismissed at: ' + new Date());
            });
        };

        function openEditDialog(address) {
            $scope.EditedAddress = address;
            var modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: '/Scripts/spa/address/editAddressModal.html',
                controller: 'addressEditCtrl',
                size: 'lg',
                scope: $scope,
                EditedAddress: function () {
                    return {
                        EditedAddress: EditedAddress
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


