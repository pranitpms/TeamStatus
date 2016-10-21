
(function () {
    'use strict';

    angular.module('TeamStatus').factory('loginService', function ($http, $q) {

        var baseUrl = 'TeamStatus/api/login';
        
        function getAccess(loginEntity) {
            var promisePost = $http.post(baseUrl, loginEntity);

            promisePost.success(function(json) {
                return json;

            }).error(function (err) {
                Console.log("error :- " + err);
                return - 1;
            });
        }

        function getUsers() {

             var deferred = $q.defer();
            $http({
                method: 'GET',
                url: baseUrl
            }).success(function(result) {
                deferred.resolve(result);
            }).error(function (err) {
                deferred.reject(err);
            });

            return deferred.promise;
        }


        return {
            GetAccess: getAccess,
            GetUsers: getUsers
        };
    });

})();