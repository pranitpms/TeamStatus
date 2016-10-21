/// <reference path="../App_Views/Login.html" />
/// <reference path="~/bower_components/angular/angular.js" />
(function() {
    'use-strict';

    angular.module('TeamStatus')
        .config(function ($stateProvider) {

            $stateProvider.state('login', {
                url: '/',
                templateUrl: 'TeamStatus/App_Views/User/Login.html',
                controller: 'loginController',
                controllerAs: 'login',
                authenticate: false,
                resolve: {
                    userList: function (loginService) {
                        return loginService.GetUsers();
                    }
                }
            });
        });
})();