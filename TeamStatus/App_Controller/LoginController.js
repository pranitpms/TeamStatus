(function () {

    angular.module('TeamStatus')
        .controller('loginController', function ($state, loginService, userList, toastr, userService) {

            var authentication = this;

            var _userName = '';
            var _password = '';

            authentication.isPassVisible = true;
            authentication.isGuestVisible = true;
            authentication.UserName = authentication.userName;
            authentication.Password = authentication.password;
            authentication.IsLogin = false;

            userService.IsAuthenticate = false;

            var isGuest = false;

            authentication.ShowPassword = function () {
                authentication.isPassVisible = false;
                authentication.isGuestuser = true;
            };

            authentication.Cancel = function () {
                authentication.isPassVisible = true;
                authentication.isGuestuser = false;
            };
            //----------------------------------------------------
            authentication.validateUser = function (isGuest) {
                var error = [];

                if (_.isUndefined(authentication.UserName) || _.isNull(authentication.UserName) || _.isEmpty(authentication.UserName)) {
                    error.push('UserName is required');
                }

                if (!isGuest && (_.isUndefined(authentication.Password) || _.isNull(authentication.Password) || _.isEmpty(authentication.Password))) {
                    error.push('Password is required');
                }

                if (!_.isEmpty(error)) {
                    toastr.warning(error);
                    error = null;
                    return false;
                }
                return true;
            }

            authentication.SetUserValues = function (user) {
                userService.UserId = user.userId;
                userService.UserName = user.userName;
                userService.UserRole = user.role;
                userService.Name = user.name;
                userService.IsAuthenticate = true;
            }

            //--------------------------------------------------
            authentication.SignIn = function () {

               if(authentication.validateUser(authentication.isGuestuser)){

                _.forEach(userList, function(user) {

                    if (authentication.isGuestuser) {
                        if (_.isEqual(authentication.UserName, user.userName)) {
                            authentication.SetUserValues(user);
                            $state.go('Home');
                            authentication.IsLogin = true;
                        }
                    } else if (_.isEqual(authentication.UserName, user.userName) && _.isEqual(authentication.Password, user.password)) {
                        authentication.SetUserValues(user);
                        $state.go('Home');
                        authentication.IsLogin = true;
                    }

                    // toastr.error('Login Failed !!!');
                });
               }
                /*----------Region Property--------------------*/

               

                Object.defineProperties(this, {
                    UserName: {
                        get: function () {
                            return _userName;
                        },
                        set:function(val) {
                            _userName = val;
                        }
                    },
                    Password: {
                        get: function () {
                            return _password;
                        },
                        set: function (val) {
                            _password = val;
                        }
                    }
                });
            };
        });
})();