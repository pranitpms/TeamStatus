

(function () {
    angular.module('TeamStatus')
        .factory('statusService', function($http, $q) {
            var baseUrl = 'TeamStatus/api/';

            function getStatusByTeam(userId) {

                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: baseUrl + 'Status',
                    params: { userId: userId, date: new Date().toDateString() }
                }).success(function (result) {
                    deferred.resolve(result);
                }).error(function (err) {
                    deferred.reject(err);
                });

                return deferred.promise;
            }

            function getStatusByDate(teamId,category,date) {

                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: baseUrl + 'Status' + '/StatusByDate',
                    params: { teamId: teamId.toString(), category: category.toString(), statusDate: date.toDateString() }
                }).success(function (result) {
                    deferred.resolve(result);
                }).error(function (err) {
                    deferred.reject(err);
                });

                return deferred.promise;
            }

            return {
                GetStatusByTeam: getStatusByTeam,
                GetStatusByDate: getStatusByDate
            };

        });
})();