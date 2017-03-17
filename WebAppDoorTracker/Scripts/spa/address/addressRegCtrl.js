(function (app) {
    'use strict';
    app.controller('addressRegCtrl', addressRegCtrl);
    addressRegCtrl.$inject = ['$scope', '$location', '$rootScope', 'apiService'];

    function addressRegCtrl($scope, $location, $rootScope, apiService) {

        $scope.newAddress = {};
        $scope.Register = Register;
        $scope.registerAddress = registerAddress;

        $scope.submission = {
            successMessages: ['Successfull submission will appear here.'],
            errorMessages: ['Submition errors will appear here.']
        };

        function registerAddress(newAddress) {

            console.log("register");

            apiService.post('/api/address/put', $scope.newAddress,
            registerAddressSucceded,
            registerAddressFailed);
        }

        function Register() {
           apiService.post('/api/address/update', $scope.newAddress,
           registerAddressSucceded,
           registerAddressFailed);
        }

        function registerAddressSucceded(response) {
            $scope.submission.errorMessages = ['Submition errors will appear here.'];
            $scope.submission.successMessages = [];
            $scope.submission.successMessages.push($scope.newAddress.postalCode + ' has been successfully registed');
            $scope.newAddress = {};
        }

        function registerAddressFailed(response) {
            if (response.status == '400')
                $scope.submission.errorMessages = response.data;
            else
                $scope.submission.errorMessages = response.statusText;
        }
    }

})(angular.module('DoorTrackerApp'));