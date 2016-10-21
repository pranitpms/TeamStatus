
(function() {

    angular.module('TeamStatus')
        .controller('StatusController', function (statusJsonData,userService, statusService, entityService) {

            var status = this;

            status.UserId = userService.UserId;
            status.UserName = userService.UserName;
            status.UserRole = userService.UserRole;
            status.Name = userService.Name;

            status.StatusDate = new Date();
            status.statusList = [{}];
            status.ResourceLookup = [{}];
            status.Category = '';
            status.TeamID = '';
            status.persistObject = {};
            status.minDate = null;
            status.opened = false;

            /*********************Initialize Variable*********************/
             
            if (!_.isNull(statusJsonData) && !_.isUndefined(statusJsonData) && !_.isEmpty(statusJsonData)) {
                status.TeamID = statusJsonData.teamID;
                status.Category = statusJsonData.category;
                status.ResourceLookup = statusJsonData.resourceLookup;
                status.statusList = statusJsonData.statusList;
            }


            /************************************************************/

            status.config = {
                columns: [
                    { name: 'resourceID', lable: '', type: 'text', isVisible: false },
                    { name: 'statusID', lable: '', type: 'text', isVisible: false },
                    { name: 'resourceName', lable: 'Resource Name', type: 'text', isVisible: true, isDropdown: true, style: 'width: 21%;' },
                    { name: 'jiraiId', lable: 'Jira ID', type: 'text', isVisible: true, style: 'width :12%;' },
                    { name: 'description', lable: 'Description', type: 'textarea', isVisible: true, style: 'width:21%;' },
                    { name: 'status', lable: 'Status', type: 'text', isVisible: true, style: 'width:14%;' },
                    { name: 'remark', lable: 'Remark', type: 'textarea', isVisible: true, style: 'width:21%;' }
                ]
            };

            status.SetFieldValue = function (list, data, columnName) {
                data[columnName] = list.description;
                if (!_.isUndefined(list.code))
                    data['resourceID'] = list.code;
            }
            /******************Controller Function************************/

            status.onDateSelected = function() {
                if (_.isNull(status.Category) || _.isUndefined(status.Category)) {
                    status.Category = '';
                }
                var promise = statusService.GetStatusByDate(status.UserId, status.Category, status.StatusDate);

                promise.then(function (result) {
                    status.statusList = result;
                }).catch(function (error) {
                    toastr.error(error);
                });

            }


            /******************Entity Functions***************************/

            status.toggleAddMode = function () {
                status.addMode = !status.addMode;
                status.object = { resourceName: '', jiraiId: '', description: '', status: '' ,remark:'',resourceID:''};
            }

            status.toggleEditMode = function (object) {
                status.persistObject = {};
                object.editMode = !object.editMode;
                status.persistObject = angular.copy(object);
            };

            status.addObject = function () {
                if (_.isUndefined(status.object)) return;

                //if (!status.validate()) return;

                var jsonResourceStatus = {
                    resourceID: status.object.resourceID,
                    jiraiId: status.object.jiraiId,
                    description: status.object.description,
                    status: status.object.status,
                    remark: status.object.remark,
                    startDate:null,
                    resourceName: status.object.resourceName,
                    statusDate: status.StatusDate
                }

                var promise = entityService.Add(jsonResourceStatus, 'Status');

                promise.then(function (result) {
                    status.statusList.push(result);
                }).catch(function (error) {
                    toastr.error(error);
                });
            }


            status.Ischanged = function (entity) {

                if (JSON.stringify(entity) === JSON.stringify(status.persistObject.resourceName))
                    return false;
                return true;
            };

            status.updateObject = function (statusData) {

                if (!status.Ischanged(statusData)) return;

                var jsonResourceStatus = {
                    resourceID: statusData.resourceID,
                    statusID: statusData.statusID,
                    jiraiId: statusData.jiraiId,
                    description: statusData.description,
                    status: statusData.status,
                    remark: statusData.remark,
                    startDate: null,
                    resourceName: statusData.resourceName,
                    statusDate: status.StatusDate
                }

                var promise = entityService.Update(jsonResourceStatus, 'Status');

                promise.then(function (result) {
                    var index = _.findIndex(status.statusList, 'statusID', result.statusID);
                    status.statusList[index] = result;
                }).catch(function (error) {
                    toastr.error(error);
                });
            }

            status.deleteObject = function (statusData) {
                var promise = entityService.Remove(statusData.statusID, 'Status');

                promise.then(function (result) {
                    var index = _.findIndex(status.statusList.toString(), 'statusID', result.toString());
                    status.statusList.splice(index, 1);
                }).catch(function (error) {
                    toastr.error(error);
                });
            }

            /******************Calender Functions************************/

            status.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            status.disabled = function (date, mode) {
                if (mode === 'day') {
                    return ((date.getDay() === 0 || date.getDay() === 6) || (date.getDate() > new Date().getDate()));
                }
               return (date.getDate() > new Date().getDate());
            }

            status.formats = ['dd-MMMM-yyyy'];

            status.minDate = status.minDate ? null : new Date(2015, 1, 1);

            status.maxDate = new Date(2020, 5, 22);

            status.open = function ($event) {
               return status.opened = true;
            };
        });


})();