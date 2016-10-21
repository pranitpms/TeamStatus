(function() {
    var app = angular.module('TeamStatus');

    app.controller('HomeController', function (userService) {


        var home = this;
        home.isActive = true;
        home.Name = userService.Name;
        home.Role = userService.UserRole;
        
        home.isSuperAdmin = _.isEqual(userService.UserRole, '0');
        
        home.isAdmin = _.isEqual(userService.UserRole, '1');


    });
})();