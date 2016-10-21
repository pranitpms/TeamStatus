(function() {

    angular.module('TeamStatus')
        .controller('NavController', function($location,$scope) {

            var nav = this;
            nav.path = 'login';

            nav.loc = $location.path;

            nav.isActive = ! _.isEqual(nav.path, nav.loc);

           
        });

})();