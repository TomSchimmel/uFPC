angular.module('umbraco.resources').factory('uFPCDashboardService',
    function ($q, $http) {
        var serviceRoot = 'umbraco/uFPC/resources/';

        return {
            update: function () {
                return $http.get(serviceRoot + 'Update');
            },
    }
});