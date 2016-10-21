(function() {

    angular.module('TeamStatus')
        .factory('entityService', function($http, $q) {

            var baseUrl = 'TeamStatus/api/';
            var apiUri = baseUrl;

            function add(entity,entityName) {
                
                var deferred = $q.defer();

                $http({
                    method: 'POST',
                    url: apiUri + entityName,
                    data: entity
                }).success(function (result) {
                    deferred.resolve(result);
                }).error(function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
            }

            function fetch(entityName) {

                var deferred = $q.defer();

                $http({
                    method: 'GET',
                    url: apiUri + entityName
                }).success(function (result) {
                    deferred.resolve(result);
                }).error(function (err) {
                    deferred.reject(err);
                });

                return deferred.promise;
            }

            function update(entity, entityName) {
                
                var deferred = $q.defer();

                $http({
                    method: 'PUT',
                    url: apiUri + entityName,
                    data: entity
                }).success(function (result) {
                    deferred.resolve(result);
                }).error(function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
            }

            function remove(id, entityName)
            {
                var deferred = $q.defer();

                $http({
                    method: 'DELETE',
                    url: apiUri + entityName,
                    params: { id: id }
                }).success(function (result) {
                    deferred.resolve(result);
                }).error(function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
            }

            function fetchById(id, entityName) {

                var deferred = $q.defer();

                $http({
                    method: 'GET',
                    url: apiUri + entityName,
                    params: { id: id }
                }).success(function (result) {
                    deferred.resolve(result);
                }).error(function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
            }

            return {
                Add: add,
                Fetch: fetch,
                Update: update,
                Remove: remove,
                FetchById : fetchById
            }
        });
})();