(function (app) {
    'use strict';
    app.controller('customerDoorsCtrl', customerDoorsCtrl);
    customerDoorsCtrl.$inject = ['$scope', '$uibModalInstance', 'apiService', 'notificationService'];

    function customerDoorsCtrl($scope, $uibModalInstance, apiService, notificationService) {

        $scope.isEnabled = true;
        $scope.cancel = cancel;

        var config = {
            params: {
                id: $scope.Customer.id
            }
        }

        apiService.get('api/customer/showdoors/', config, DoorsLoadCompleted, DoorsLoadFailed);

        function DoorsLoadCompleted(result) {
            $scope.doors = result.data;
            notificationService.displaySuccess('Doors loaded');
        }

        function DoorsLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function cancel() {
            $uibModalInstance.dismiss();
        }
    }

})(angular.module('DoorTrackerApp'));