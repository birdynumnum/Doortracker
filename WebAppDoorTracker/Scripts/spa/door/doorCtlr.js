(function (app) {
    'use strict';
    app.controller('doorCtrl', doorCtrl);
    doorCtrl.$inject = ['$scope', '$uibModal', '$log', 'apiService', 'notificationService', 'bearerTokenContainer'];

    function doorCtrl($scope, $uibModal, $log, apiService, notificationService, bearerTokenContainer) {

        $scope.pageClass = 'page-customers';
        $scope.doors = [];
        $scope.loadingDoors = true;

        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.totalCount = 0;

        $scope.door = {};
        $scope.openEditDoor = openEditDoor;
        $scope.EditedDoor = {};
        $scope.animationsEnabled = true;
        $scope.searchDoor = searchDoor;

        function search(page) {


            var config = {
                params: {
                    page: page,
                    pageSize: 4,
                    filter: $scope.filterCustomers
                }
            };

            apiService.get('api/door/search', config, doorLoadCompleted, doorLoadFailed);
         //   apiService.get('api/door/', config, doorLoadCompleted, doorLoadFailed);
        };


        var config = {
            params: {
                page: 0,
                pageSize: 4,
                filter: $scope.filterCustomers
            }

        };

        apiService.get('api/door/search', config, doorLoadCompleted, doorLoadFailed);


        function doorLoadCompleted(result) {

            bearerTokenContainer.getToken();

            $scope.page = result.data.page;
            $scope.pagesCount = result.data.totalPages;
            $scope.totalCount = result.data.totalCount;
            $scope.doors = result.data.items;
            $scope.loadingDoors = false;
        }

        function doorLoadFailed(result) {
            notificationService.displayError(result.error);
        }

        function searchDoor(page) {

            console.log("page in searchdoor " +$scope.page);

            var config = {
                params: {
                    page: $scope.page,
                    pageSize: 4,
                    filter: $scope.filterCustomers
                }
            };

            apiService.get('api/door/search/', config, doorLoadFailed, doorLoadCompleted);
        }


        function openEditDoor(door) {
            $scope.EditedDoor = door;

            var modalInstance = $uibModal.open
                ({
                    animation: $scope.animationsEnabled,
                    templateUrl: '/Scripts/spa/door/editDoorModal.html',
                    controller: 'doorEditCtrl',
                    size: 'lg',
                    scope: $scope,
                    EditedDoor: function () {
                        return
                        {
                            EditedDoor: EditedDoor
                        };
                    }
                });

            modalInstance.result.then(function (selectedItem) {
                $scope.selected = selectedItem;
            }, function () {
                $log.info('Modal dismissed at: ' + new Date());
            });
        }
    }
})(angular.module('DoorTrackerApp'));


