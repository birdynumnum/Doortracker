(function (app) {
    'use strict';
    app.controller('customerRegCtrl', customersRegCtrl);
    customersRegCtrl.$inject = ['$scope', '$location', '$rootScope', 'apiService'];

    function customersRegCtrl($scope, $location, $rootScope, apiService) {

        $scope.newCustomer = {};
        $scope.Register = Register;

        $scope.registerCustomer = function registerCustomer(newCustomer) {
            console.log("Reg");

            console.log($scope.newCustomer);

            apiService.post('/api/customer/put', $scope.newCustomer,
            registerCustomerSucceded,
            registerCustomerFailed);
        }

        $scope.submission = {
            successMessages: ['Successfull submission will appear here.'],
            errorMessages: ['Submition errors will appear here.']
        };

        function Register() {

            console.log("update");
            console.log($scope.newCustomer);

           apiService.post('/api/customers/update', $scope.newCustomer,
           registerCustomerSucceded,
           registerCustomerFailed);
        }

        function registerCustomerSucceded(response) {
            $scope.submission.errorMessages = ['Submition errors will appear here.'];
            console.log(response);
            var customerRegistered = response.data;
            $scope.submission.successMessages = [];
            $scope.submission.successMessages.push($scope.newCustomer.LastName + ' has been successfully registed');
            $scope.submission.successMessages.push('Check ' + customerRegistered.UniqueKey + ' for reference number');
            $scope.newCustomer = {};
        }

        function registerCustomerFailed(response) {
            console.log(response);
            if (response.status == '400')
                $scope.submission.errorMessages = response.data;
            else
                $scope.submission.errorMessages = response.statusText;
        }
    }

})(angular.module('DoorTrackerApp'));