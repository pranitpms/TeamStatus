
(function () {

    angular.module('TeamStatus').factory('ProfileService', function($http, $q) {


        var baseUrl = 'TeamStatus/api/profile';

        function saveProfilePicture(filedata) {

        var defered = $q.defer();

        $http({
            method: 'POST',
            url: baseUrl + '/UpdateProfilePicture',
            params: { file: filedata },
            withCredentials: true,
            headers: { 'Content-Type': undefined },
            transformRequest: angular.identity
            }).success(function (result) {
            defered.resolve(result);
            }).error(function(err) {
                defered.reject(err);
            });
        };


        return {
            SaveProfilePicture: saveProfilePicture
        };

    });
})()