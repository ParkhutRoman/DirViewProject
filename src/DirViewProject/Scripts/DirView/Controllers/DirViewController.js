(function () {
    'use strict';

    angular
        .module('dirViewApp')
        .controller('dirController', dirController);

    dirController.$inject = ['$scope', '$http', 'Items'];

    function dirController($scope, $http, Items) {
        $scope.path = "";
        $scope.open = function (i) {
            $http.post('/api/values', { path: i || $scope.path }).success(function (data) {
                $scope.Items = data;
            })
        };
        $scope.open();
     
    }
})();