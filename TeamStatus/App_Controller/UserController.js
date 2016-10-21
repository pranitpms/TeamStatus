(function () {

    angular.module('TeamStatus')
        .controller('UserController', function (loginService, userList, toastr, entityService) {

            var user = this;

            user.isVisible = false;
            user.isDisableAdd = true;
            user.Resources = [{}];
            user.UserId = '';
            user.roleId = '';
            user.persistObject = {};

            if (!_.isUndefined(userList) && !_.isNull(userList)) {
                user.Resources = userList;
            }

            user.roles = [
                { code: '1', description: 'Admin' },
                { code: '2', description: 'User' }
            ];


            user.config = {
                columns: [
                    { name: 'userId', lable: '', type: 'text', isPrimary: true, isVisible: false },
                    { name: 'name', lable: 'Name', type: 'text', isVisible: true },
                    { name: 'userName', lable: 'UserName', type: 'text', isVisible: true },
                    { name: 'password', lable: 'Password', type: 'text', isVisible: true },
                    { name: 'role', lable: 'Role', type: 'text', isDropdown: true, isVisible: true}
                ]
            };

            user.SetFieldValue = function (list, data, columnName) {
                data[columnName] = list.description;
                if (!_.isUndefined(list.code))
                    user.roleId = list.code;

            }

            user.toggleAddMode = function () {
                user.addMode = !user.addMode;
                user.object = { userId: '', userName: '', password: '', role: '', name: '' };
            }

            user.toggleEditMode = function (object) {
                user.persistObject = {};
                object.editMode = !object.editMode;
                user.persistObject = angular.copy(object);
            };

            user.addObject = function () {
                if (_.isUndefined(user.object)) return;
                user.object.role = user.roleId;

                var promise = entityService.Add(user.object, 'Login');

                promise.then(function (result) {
                    user.Resources.push(result);
                }).catch(function (error) {
                    toastr.error(error);
                });
            }


            user.Ischanged = function (entity) {

                if (JSON.stringify(entity) === JSON.stringify(user.persistObject.resourceName))
                    return false;
                return true;
            };

            user.updateObject = function (userData) {

                if (!user.Ischanged(userData)) return;

                userData.role = user.roleId;

                var promise = entityService.Update(userData, 'Login');

                promise.then(function (result) {
                    var index = _.findIndex(user.Resources, 'userId', result.userId);
                    user.Resources[index] = result;
                }).catch(function (error) {
                    toastr.error(error);
                });
            }

            user.deleteObject = function (userData) {
                var promise = entityService.Remove(userData.userId, 'Login');

                promise.then(function (result) {
                    var index = _.findIndex(user.Resources, 'userId', result);
                    user.Resources.splice(index, 1);
                }).catch(function (error) {
                    toastr.error(error);
                });
            }

        });
})();