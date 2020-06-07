angular.module('umbraco').controller('uFPCDashboardController',
    function ($scope, $http, uFPCDashboardService) {
        $scope.update = function () {
            $scope.clearError();

            uFPCDashboardService.update()
            .then(function (response) {
                   
            }, function (error) {
          
            });
        }
    }
);