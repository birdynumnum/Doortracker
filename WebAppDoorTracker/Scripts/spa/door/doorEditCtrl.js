(function (app) {
    'use strict';
    app.controller('doorEditCtrl', doorEditCtrl);
    doorEditCtrl.$inject = ['$scope', '$uibModalInstance', '$timeout', 'apiService', 'notificationService'];

    function doorEditCtrl($scope, $uibModalInstance, $timeout, apiService, notificationService) {

        $scope.cancelEdit = cancelEdit;
        $scope.updateDoor = updateDoor;

        $scope.openDatePicker = openDatePicker;
        $scope.openDatePickerInstallation = openDatePickerInstallation;

        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };

        $scope.datepicker = {};
        $scope.datepicker2 = {};

        $scope.datepicker.format = 'shortdate';
        $scope.datepicker2.format = 'shortdate';

        function updateDoor() {
            apiService.post('/api/door/Update/', $scope.EditedDoor,
            updateDoorCompleted,
            updateDoorLoadFailed);
        }

        function updateDoorCompleted(response) {
            notificationService.displaySuccess($scope.EditedDoor.make + ' ' + $scope.EditedDoor.model + ' has been updated');
            $uibModalInstance.dismiss();
            $scope.EditedDoor = {};

        }

        function updateDoorLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function cancelEdit() {
            $scope.isEnabled = false;
            $uibModalInstance.dismiss();
        }

        function openDatePicker($event) {

            console.log("Manufacture");

            $event.preventDefault();
            $event.stopPropagation();

            $timeout(function () {
                $scope.datepicker.opened = true;
            });

            $timeout(function () {
                $('ul[datepicker-popup-wrap]').css('z-index', '10000');
            }, 100);
        };


        function openDatePickerInstallation($event) {
            console.log("Installation");

            $event.preventDefault();
            $event.stopPropagation();

           

            $timeout(function () {
                $scope.datepicker2.opened = true;
            });

            $timeout(function () {
                $scope.datepicker2.opened = true;
            });

            $timeout(function () {
                $('ul[datepicker-popup-wrap]').css('z-index', '10000');
            }, 100);
        };
    }

})(angular.module('DoorTrackerApp'));