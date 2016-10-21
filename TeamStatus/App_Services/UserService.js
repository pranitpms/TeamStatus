(function() {

    angular.module('TeamStatus')
        .service('userService', function ($cacheFactory,$localStorage) {

            var userHelper = this;

            var userId = '';
            var userName = '';
            var userRole = '';
            var name = '';
            var img = '';

            var isAuthenticated = '';

            userHelper.cacheName = 'UserCache';

            userHelper.$storage = $localStorage.$default({
                userId: '',
                userName: '',
                userRole : '',
                name : '',
                img : ''
            });


            Object.defineProperties(this, {
                UserId : {
                    get: function () {
                        if (_.isNull(userId) || _.isEmpty(userId)) {
                            return userHelper.GetFromCache('userId');
                        }
                        return userId;
                    },
                    set:function(value) {
                        userId = value;
                        userHelper.AddToCache('userId', userId);
                    }
                },
                UserName : {
                    get: function () {
                        if (_.isNull(userName) || _.isEmpty(userName)) {
                            return userHelper.GetFromCache('userName');
                        }
                        return userName;
                    },
                    set: function (value) {
                        userName = value;
                        userHelper.AddToCache('userName', userName);
                    }
                },
                UserRole : {
                    get: function () {
                        if (_.isNull(userRole) || _.isEmpty(userRole)) {
                            return userHelper.GetFromCache('userRole');
                        }
                        return userRole;
                    },
                    set: function (value) {
                        userRole = value;
                        userHelper.AddToCache('userRole', userRole);
                    }
                },
                Name: {
                    get: function () {
                        if (_.isNull(name) || _.isEmpty(name)) {
                            return userHelper.GetFromCache('name');
                        }
                        return name;
                    },
                    set: function (value) {
                        name = value;
                        userHelper.AddToCache('name', name);
                    }
                },
                IsAuthenticate: {
                    get: function () {
                        if (_.isNull(isAuthenticated) || _.isEmpty(isAuthenticated)) {
                            return userHelper.GetFromCache('isAuthenticated');
                        }
                        return isAuthenticated;
                    },
                    set: function (value) {
                        isAuthenticated = value;
                        userHelper.AddToCache('isAuthenticated', isAuthenticated);
                    }
                },
                Image: {
                    get: function () {
                        if (_.isNull(img) || _.isEmpty(img)) {
                            return userHelper.GetFromCache('img');
                        }
                        return isAuthenticated;
                    },
                    set: function (value) {
                        img = value;
                        userHelper.AddToCache('img', img);
                    }
                }
            });

            if (angular.isUndefined(userHelper.cache)) {
                userHelper.cache = $cacheFactory(userHelper.cacheName);
            }

            userHelper.AddToCache = function (key, value) {
                userHelper.cache.put(key, value);
                userHelper.$storage[key] = value;
            }

            userHelper.GetFromCache = function (key) {

                if (_.includes(userHelper.cache, key))
                    return userHelper.cache.get(key);
                else
                    return userHelper.$storage[key];

            }

        });
})();