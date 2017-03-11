(function (app) {
    'use strict';
    app.controller('addressCustomerCtrl', addressCustomerCtrl);
    addressCustomerCtrl.$inject = ['$scope', '$uibModalInstance', 'apiService', 'notificationService'];

    function addressCustomerCtrl($scope, $uibModalInstance, apiService, notificationService) {
        $scope.isEnabled = true;
        $scope.cancel = cancel;

        var config = {
            params: {
                id: $scope.Address.id
            }
        }

        apiService.get('api/address/showCustomer/', config, AddressLoadCompleted, AddressLoadFailed);

        function AddressLoadCompleted(result) {
            $scope.customers = result.data;

            //debugger;

            notificationService.displaySuccess('Customer loaded');
        }

        function AddressLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function cancel() {
            $uibModalInstance.dismiss();
        }
    }
})(angular.module('DoorTrackerApp'));