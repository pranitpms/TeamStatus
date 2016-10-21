(function () {

    angular.module('TeamStatus')
        .factory('adminService', function ($http, $q) {

            var baseUrl = 'TeamStatus/api/';

        var adminUrl = baseUrl + 'Admin';
        var teamUrl = baseUrl + 'Team';
        var memberUrl = baseUrl + 'Member';
        var lookupUrl = baseUrl + 'lookup';

        function getData() {

            var promiseGet = $http.get(adminUrl);

            promiseGet.success(function(json) {
                    return json;
                })
                .error(function() {
                    return null;
                });
        };


        function getTeamList() {
            
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: teamUrl
            }).success(function (result) {
                deferred.resolve(result);
            }).error(function (err) {
                deferred.reject(err);
            });

            return deferred.promise;
        }

        function addTeam(teamEntity) {

                var deferred = $q.defer();

                $http({
                    method: 'POST',
                    url: teamUrl,
                    data: teamEntity
                }).success(function (result) {
                    deferred.resolve(result);
                }).error(function (error) {
                    deferred.reject(error);
                });

           
                return deferred.promise;
        }

        function updateTeam(teamEntity) {

            var deferred = $q.defer();

            $http({
                method: 'PUT',
                url: teamUrl,
                data: teamEntity
            }).success(function (result) {
                deferred.resolve(result);
            }).error(function (error) {
                deferred.reject(error);
            });

            return deferred.promise;
        }

            function deleteTeam(id) {
                var deferred = $q.defer();

                $http({
                    method: 'DELETE',
                    url: teamUrl + '/' + id
                }).success(function (result) {
                    deferred.resolve(result);
                }).error(function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
            }

            function getResources(teamId) {
                
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: memberUrl,
                    params: { teamId: teamId }
                }).success(function (result) {
                    deferred.resolve(result);
                }).error(function (err) {
                    deferred.reject(err);
                });

                return deferred.promise;
            }

            function getUserLookup() {

                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: lookupUrl,
                    params: { entityName: 'LoginEntity' }
                }).success(function (result) {
                    deferred.resolve(result);
                }).error(function (err) {
                    deferred.reject(err);
                });

                return deferred.promise;
            }

            function getTeamLookup() {
                
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: lookupUrl,
                    params: { entityName: 'TeamEntity' }
                }).success(function (result) {
                    deferred.resolve(result);
                }).error(function (err) {
                    deferred.reject(err);
                });

                return deferred.promise;

            }
            

        return {
            GetData: getData,
            GetTeamList: getTeamList,
            AddTeam: addTeam,
            UpdateTeam: updateTeam,
            DeleteTeam: deleteTeam,
            GetResources: getResources,
            GetUserLookup: getUserLookup,
            GetTeamLookup: getTeamLookup
        }
        
    });
})();