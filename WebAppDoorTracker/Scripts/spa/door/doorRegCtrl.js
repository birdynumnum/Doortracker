(function (app) {
    'use strict';
    app.controller('doorRegCtrl', doorRegCtrl);
    doorRegCtrl.$inject = ['$scope', '$location', '$rootScope', 'apiService'];

    function doorRegCtrl($scope, $location, $rootScope, apiService) {
        $scope.newDoor = {};
        $scope.openDatePicker = openDatePicker;
        $scope.openDatePickerDOI = openDatePickerDOI;
        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };
        $scope.datepicker = {};
        $scope.datepicker2 = {};

        $scope.registerDoor = function registerDoor(newDoor) {
            apiService.post('/api/door/put', $scope.newDoor, registerDoorSucceded, registerDoorFailed);
        }

        $scope.submission = {
            successMessages: ['Successfull submission will appear here.'],
            errorMessages: ['Submition errors will appear here.']
        };

        function registerDoorSucceded(response) {
            $scope.submission.errorMessages = ['Submition errors will appear here.'];
            $scope.submission.successMessages = [];
            $scope.submission.successMessages.push($scope.newDoor.make + ' has been successfully registed');
            $scope.submission.successMessages.push('Check ' + doorRegistered.UniqueKey + ' for reference number');
            $scope.newDoor = {};
        }

        function registerDoorFailed(response) {
            if (response.status == '400')
                $scope.submission.errorMessages = response.data;
            else
                $scope.submission.errorMessages = response.statusText;
        }

        function openDatePicker($event) {
            $scope.datepicker.opened = true;
            $event.preventDefault();
            $event.stopPropagation();
        };

        function openDatePickerDOI($event) {
            $scope.datepicker2.opened = true;
            $event.preventDefault();
            $event.stopPropagation();
          
        };
    }
})(angular.module('DoorTrackerApp'));