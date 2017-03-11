(function (app) {
    'use strict';
    app.controller('addressEditCtrl', addressEditCtrl);
    addressEditCtrl.$inject = ['$scope', '$uibModalInstance', '$timeout', 'apiService', 'notificationService'];

    function addressEditCtrl($scope, $uibModalInstance, $timeout, apiService, notificationService) {

        $scope.cancelEdit = cancelEdit;
        $scope.updateAddress = updateAddress;

        function updateAddress() {
            apiService.post('api/address/update', $scope.EditedAddress, updateAddressCompleted, updateAddressLoadFailed);
        }

        function updateAddressCompleted(response) {
            notificationService.displaySuccess($scope.EditedAddress.streetName + ' has been updated');
            $uibModalInstance.dismiss();
            $scope.EditedAddress = {};
        }

        function updateAddressLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function cancelEdit() {
            $scope.isEnabled = false;
            $uibModalInstance.dismiss();
        }
    }
})(angular.module('DoorTrackerApp'));