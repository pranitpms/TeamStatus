(function() {

    angular.module('TeamStatus', [
            'ui.router',
            'toastr',
            'ui.select',
            'ngSanitize',
            'ngTouch',
            'ui.bootstrap',
            'ngStorage',
            'angularUtils.directives.uiBreadcrumbs',
            'ui-element',
            'flow'
        ])
        .config(function($urlRouterProvider, $anchorScrollProvider, $uiViewScrollProvider, $httpProvider, uiSelectConfig) {
            $urlRouterProvider.otherwise('/');

            $uiViewScrollProvider.useAnchorScroll();
            $anchorScrollProvider.disableAutoScrolling();

            $httpProvider.interceptors.push('myHttpInterceptor');

            uiSelectConfig.theme = 'bootstrap';
            uiSelectConfig.resetSearchInput = true;
            uiSelectConfig.appendToBody = true;

        })
        .run(function($rootScope, $state, userService) {
            $rootScope.$on('$stateChangeStart', function(event, toState, toParams, fromState, fromParams) {
                if (toState.authenticate && !userService.IsAuthenticate) {
                    $state.transitionTo("login");
                    event.preventDefault();
                }
            });
        })
        .factory('broadcastService', function ($rootScope) {
            
            function send(msg, data) {
                $rootScope.$broadcast(msg, data);
            }

            return {
                Send: send
            };

        })
    .factory('myHttpInterceptor', function ($q) {
        return {
            'request': function (config) {
                $("#spinner").show();
                return config;
            },

            'requestError': function (rejection) {

                if (canRecover(rejection)) {
                    return responseOrNewPromise
                }
                return $q.reject(rejection);
            },



            'response': function (response) {
                $("#spinner").hide();
                return response;
            },

            'responseError': function (rejection) {
                $("#spinner").hide();
                if (canRecover(rejection)) {
                    return responseOrNewPromise
                }
                return $q.reject(rejection);
            }
        };
        });
})();

