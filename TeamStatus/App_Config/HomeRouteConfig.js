/// <reference path="../App_Views/Admin/AdminHome.html" />
/// <reference path="../App_Views/Login.html" />
/// <reference path="~/bower_components/angular/angular.js" />
(function () {
    'use-strict';

    angular.module('TeamStatus')
        .config(function ($stateProvider) {

            $stateProvider.state('Home', {
                url: '/home',
                templateUrl: 'TeamStatus/App_Views/Home/Home.html',
                controller: 'HomeController',
                controllerAs: 'home',
                authenticate: true,
                data: {
                    displayName: 'Home'
                }
            })
            .state('Home.Teams', {
                url: '/home/Team',
                templateUrl: 'TeamStatus/App_Views/Home/Team.html',
                controller: 'TeamController',
                controllerAs: 'team',
                authenticate: true,
                resolve: {
                    teamList: function (adminService) {
                        return adminService.GetTeamList();
                    }
                },
                data: {
                    displayName: 'Team'
                }
            })
                .state('Home.Members', {
                url: '/home/Members',
                templateUrl: 'TeamStatus/App_Views/Home/Members.html',
                controller: 'MemberController',
                controllerAs: 'member',
                authenticate: true,
                resolve: {
                        teamList: function(adminService) {
                            return adminService.GetTeamLookup();
                        },
                        memberList:function(entityService) {
                            return entityService.Fetch('Member');
                        },
                        userLookup : function(adminService) {
                            return adminService.GetUserLookup();
                        }
                },
                data: {
                    displayName: 'Members'
                }
                })
            .state('Home.User', {
                url: '/home/User',
                templateUrl: 'TeamStatus/App_Views/Home/Users.html',
                    controller: 'UserController',
                    controllerAs: 'user',
                    authenticate: true,
                    resolve: {
                        userList: function (loginService) {
                            return loginService.GetUsers();
                        }
                    },
                    data: {
                        displayName: 'Users'
                    }
            })
            .state('Home.UserStatus', {
                url: '/UserStatus',
                templateUrl: 'TeamStatus/App_Views/Statuses.html',
                controller: 'StatusController',
                controllerAs: 'status',
                authenticate: true,
                resolve: {
                    statusJsonData : function(userService, statusService) {
                        return statusService.GetStatusByTeam(userService.UserId);
                    }
                },
                data: {
                    displayName: 'Status'
                }
            })
            .state('Home.View', {
                url: '/Ui-Views',
                templateUrl: 'TeamStatus/App_Views/Ui-Views.html',
                controller: 'UiviewController',
                controllerAs: 'sample',
                authenticate: true,
                data: {
                    displayName: 'Ui-Views'
                }
            })
            .state('Home.Profile', {
                url: 'home/profile',
                templateUrl: 'TeamStatus/App_Views/User/Profile/profile.html',
                controller: 'ProfileController',
                controllerAs: 'profile',
                authenticate: true,
                resolve: {
                    userdata: function (userService, entityService) {
                        return entityService.FetchById(userService.UserId, 'Profile');
                    }

                }
            });
        });
})();