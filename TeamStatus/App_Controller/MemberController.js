(function() {

    angular.module('TeamStatus')
        .controller('MemberController', function (teamList,toastr,adminService,memberList,userLookup,entityService) {

            var member = this;

            member.isVisible = false;
            member.isDisableAdd = true;
            member.TeamList = {};
            member.Resources = [{}];
            member.UserLookup = {};
            member.TeamName = '';
            member.TeamId = '';
            member.UserId = '';
            member.persistObject = {};
            
            member.UserLookup = userLookup;

            if (!_.isUndefined(memberList) && !_.isNull(memberList)) {
                member.Resources = memberList;
            }

            if (!_.isUndefined(memberList) && !_.isNull(memberList) && !_.isEmpty(teamList)) {
                member.TeamList = teamList;
            }

            member.Category = [
                {code : 0, description: 'Development' },
                {code : 1, description: 'Testing' }
            ];

            member.onSelect = function (model) {
                member.TeamName = model.description;
                member.TeamId = model.code;
                member.isDisableAdd = false;
                if (_.findIndex(member.Resources, 'teamID', model.code.toString()) < 0) {
                    toastr.warning("No Record found !!!!");
                    member.isVisible = false;
                } else {
                    member.isVisible = true;
                }
            }

            member.SetFieldValue = function (model, data, columnName) {
                data[columnName] = model.description;
                if (!_.isUndefined(model.code))
                    data['userID'] = model.code;

            }

            member.ListView = function(columnName) {

                if (_.isEqual(columnName, 'catagory')) {
                    return member.Category;
                }

                if (_.isEqual(columnName, 'resourceName')) {
                    return member.UserLookup;
                }

                return {};
            };

            member.config = {
                columns: [
                    { name: 'resourceID', lable: '', type: 'text', isPrimary: true ,isVisible :false},
                    { name: 'catagory', lable: 'Catagory', type: 'text', isDropdown: true, isVisible: true },
                    { name: 'resourceName', lable: 'Name', type: 'text', isDropdown: true, isVisible: true },
                    { name: 'teamID', lable: '', type: 'text', isVisible: false },
                    { name: 'userID', lable: '', type: 'text', isVisible: false }
                ]
            };

            member.toggleAddMode = function () {
                member.addMode = !member.addMode;
                member.object = { catagory: '', resourceName:'',teamID:'',userID:''};
            }

            member.toggleEditMode = function (object) {
                member.persistObject = {};
                object.editMode = !object.editMode;
                member.persistObject = angular.copy(object);
            };

            member.addObject = function() {
                if (_.isUndefined(member.object)) return;

                if (!member.validate()) return;

                var resourceEntity = {
                    Catagory: member.object.catagory,
                    TeamID:member.TeamId,
                    UserID: member.object.userID,
                    ResourceName: member.object.resourceName
                }

                var promise = entityService.Add(resourceEntity, 'Member');

                promise.then(function (result) {
                    member.Resources.push(result);
                }).catch(function (error) {
                    toastr.error(error);
                });
            }


            member.Ischanged = function (entity) {

                if (JSON.stringify(entity) === JSON.stringify(member.persistObject.resourceName))
                    return false;
                return true;
            };

            member.updateObject = function (memberData) {

                if (!member.Ischanged(memberData)) return;

                if(!member.validate()) return ;

                var resourceEntity = {
                    ResourceID:memberData.resourceID,
                    Catagory: memberData.catagory,
                    TeamID: memberData.teamID,
                    UserID: memberData.userID,
                    ResourceName: memberData.resourceName
                }

                var promise = entityService.Update(resourceEntity, 'Member');

                promise.then(function (result) {
                    var index = _.findIndex(member.Resources, 'resourceID', result.resourceID);
                    member.Resources[index] = result;
                }).catch(function (error) {
                    toastr.error(error);
                });
            }

            member.deleteObject = function (memberData) {
                var promise = entityService.Remove(memberData.resourceID, 'Member');

                promise.then(function (result) {
                    var index = _.findIndex(member.Resources, 'resourceID', result);
                    member.Resources.splice(index, 1);
                }).catch(function (error) {
                    toastr.error(error);
                });
            }


            member.validate = function() {
                if (_.isUndefined(member.TeamId) && _.isNull(member.TeamId)) {
                    toastr.error("Please select Team first");
                    return false;
                }
                return true;
            }

            member.filterById = function (data) {
                return (data.teamID.toString() === member.TeamId.toString());
            };

        });
})();