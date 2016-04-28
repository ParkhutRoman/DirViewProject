(function () {
    'use strict';

    var dirService = angular.module('dirService', ['ngResource']);
    dirService.factory('Items', ['$resource',
        function ($resource) {
            return $resource("/api/values");
        }]);
})();